Imports System.Collections
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Web.Mail
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web
Imports System.Xml.Serialization
Imports activiser.Gateway

'#Const DEBUG = True
#Const UseSharedStuff = False

<System.Web.Services.WebService( _
    name:="activiser Gateway" _
    , Description:="activiser™ ICT Gateway V3.4.0" _
    , namespace:="http://www.activiser.com/activiserGateway" _
    )> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
Public Class activiserGateway
    Inherits System.Web.Services.WebService

    Private app_RootFolderName As String
    Private app_CodeFolderName As String
    Private app_DataFolderName As String
    Private app_BinFolderName As String

    ' Assembly
    Private activiserEntityAssembly As System.Reflection.Assembly
    Private gatewayEntityAssembly As System.Reflection.Assembly

    ' connection manager
    Private activiserDBConnectionManager As Data.ConnectionManager
    'Private activiserDBConnectionManager As Data.ConnectionManager

    ' Data Schema
    Private dataTypeMappingList As DataTypeMapping

    ' Function Schema
    Private inputGatewayFunctionList As GatewayFunctionList
    Private outputGatewayFunctionList As GatewayFunctionList

    ' Input Gateway Function Mapping
    Private inputGatewayFunctionMapping As Hashtable

    ' GatewayEntity.GatewayTask table
    Private taskExecutable As Input.Executable
    Private taskSchema As DataBaseSchema.TableSchemaRow

    ' gatewayentity.GatewayDuplicatedTask table
    Private duplicatedTaskExecutable As Input.Executable
    Private duplicatedTaskSchema As DataBaseSchema.TableSchemaRow

    ' GatewayException table
    Private exceptionExecutable As Input.Executable
    Private exceptionSchema As DataBaseSchema.TableSchemaRow

    ' GatewayReturnValue table
    Private returnValueExecutable As Input.Executable
    Private returnValueSchema As DataBaseSchema.TableSchemaRow

    'TODO: Change this to dynamic collection.
    Private clientSiteExecutableWrapper As Input.ExecutableWrapper
    Private consultantExecutableWrapper As Input.ExecutableWrapper
    Private requestExecutableWrapper As Input.ExecutableWrapper
    Private jobExecutableWrapper As Input.ExecutableWrapper
    Private interestedConsultantExecutableWrapper As Input.ExecutableWrapper
    Private jobStatusExecutableWrapper As Input.ExecutableWrapper

    'Private appSettings As NameValueCollection
    'Private connectionStrings As System.Configuration.ConnectionStringSettingsCollection

    'Private crmJobTravelTimeField As String

    Private Shared Function FixPath(ByVal DefaultFolder As String, ByVal FileName As String) As String
        If FileName.IndexOfAny((System.IO.Path.DirectorySeparatorChar & System.IO.Path.AltDirectorySeparatorChar & System.IO.Path.VolumeSeparatorChar).ToCharArray) = -1 Then
            FileName = System.IO.Path.Combine(DefaultFolder, FileName)
        End If
        Return FileName
    End Function

#Region " constructor "
    Sub NewNotShared()
        app_RootFolderName = Me.Context.Request.PhysicalApplicationPath
        app_CodeFolderName = System.IO.Path.Combine(app_RootFolderName, "App_Code")
        app_DataFolderName = System.IO.Path.Combine(app_RootFolderName, "App_Data")
        app_BinFolderName = System.IO.Path.Combine(app_RootFolderName, "bin")

        'Dim connectionConfigurationFile As String
        Dim dataTypeMappingFilename As String
        Dim activiserTableSchemaFileName As String
        Dim gatewayTableSchemaFileName As String
        Dim inputGatewayFunctionSchemaFileName As String
        Dim outputGatewayFunctionSchemaFileName As String

        Try
            dataTypeMappingFilename = FixPath(app_DataFolderName, My.Settings.DataTypeMappingFile) ' appSettings("DataTypeMappingFile"))
            activiserTableSchemaFileName = FixPath(app_DataFolderName, My.Settings.activiserTableSchema) ' appSettings("ActiviserTableSchema"))
            gatewayTableSchemaFileName = FixPath(app_DataFolderName, My.Settings.GatewayTableSchema) ' appSettings("GatewayTableSchema"))
            inputGatewayFunctionSchemaFileName = FixPath(app_DataFolderName, My.Settings.InputGatewayFunctionList) ' appSettings("InputGatewayFunctionList"))
            outputGatewayFunctionSchemaFileName = FixPath(app_DataFolderName, My.Settings.OutputGatewayFunctionList) '  appSettings("OutputGatewayFunctionList"))
        Catch ex As Exception
            Throw New ApplicationException("Error reading configuration database", ex)
        End Try

        ' get Assembly info for activiser and gateway, we use any old member for the GetType.
        ' TODO: RCP 2006/05/19 - Figure out why!
        activiserEntityAssembly = System.Reflection.Assembly.GetAssembly(GetType(BusinessEntity.ClientSite))
        gatewayEntityAssembly = System.Reflection.Assembly.GetAssembly(GetType(GatewayEntity.GatewayTask))

        ' read the configuration information from the XML file, which we expect to be in the App_Data folder, but could be anywhere
        '

        Try
            activiserDBConnectionManager = New Data.ConnectionManager("activiserDatabaseConnection", My.Settings.activiserDatabase)
        Catch ex As Exception
            Throw New ApplicationException("Error creating database connection", ex)
        End Try

        ' data schema
        dataTypeMappingList = New DataTypeMapping ' Utility.RetrieveDataSchema(dataTypeMappingFilename)
        dataTypeMappingList.ReadXml(dataTypeMappingFilename)

        ' function schemas
        inputGatewayFunctionList = New GatewayFunctionList()
        inputGatewayFunctionList.ReadXml(inputGatewayFunctionSchemaFileName)

        outputGatewayFunctionList = New GatewayFunctionList()
        outputGatewayFunctionList.ReadXml(outputGatewayFunctionSchemaFileName)

        ' input gateway function mapping
        inputGatewayFunctionMapping = New Hashtable
        ' GatewayEntity.GatewayTask table

        Dim gatewaySchema As New DataBaseSchema
        gatewaySchema.ReadXml(gatewayTableSchemaFileName)
        For Each tableSchema As DataBaseSchema.TableSchemaRow In gatewaySchema.TableSchema
            Select Case tableSchema.TableName
                Case "GatewayTask"
                    taskSchema = tableSchema 'Utility.RetrieveTableSchema("GatewayTask", gatewayTableSchemaFileName)
                    taskExecutable = New Input.Executable '.NTextDataTypeSupportedExecutable
                    taskExecutable.DataSchema = dataTypeMappingList
                    taskExecutable.TableSchema = taskSchema

                Case "GatewayDuplicatedTask"
                    ' gatewayentity.GatewayDuplicatedTask table
                    duplicatedTaskSchema = tableSchema 'Utility.RetrieveTableSchema("GatewayDuplicatedTask", gatewayTableSchemaFileName)
                    duplicatedTaskExecutable = New Input.Executable '.NTextDataTypeSupportedExecutable
                    duplicatedTaskExecutable.DataSchema = dataTypeMappingList
                    duplicatedTaskExecutable.TableSchema = duplicatedTaskSchema

                Case "GatewayException"
                    ' GatewayException table
                    exceptionSchema = tableSchema 'Utility.RetrieveTableSchema("GatewayException", gatewayTableSchemaFileName)
                    exceptionExecutable = New Input.Executable '.NTextDataTypeSupportedExecutable
                    exceptionExecutable.DataSchema = dataTypeMappingList
                    exceptionExecutable.TableSchema = exceptionSchema

                Case "GatewayReturnValue"
                    ' GatewayReturnValue table
                    returnValueSchema = tableSchema 'Utility.RetrieveTableSchema("GatewayReturnValue", gatewayTableSchemaFileName)
                    returnValueExecutable = New Input.Executable '.NTextDataTypeSupportedExecutable
                    returnValueExecutable.DataSchema = dataTypeMappingList
                    returnValueExecutable.TableSchema = returnValueSchema
            End Select

        Next



        Dim activiserSchema As New DataBaseSchema
        activiserSchema.ReadXml(activiserTableSchemaFileName)
        ' activiser BusinessEntity.ClientSite table
        For Each tableSchema As DataBaseSchema.TableSchemaRow In activiserSchema.TableSchema
            'SetupExecutable(tableSchema)
            Select Case tableSchema.TableName
                Case "ClientSite"
                    clientSiteExecutableWrapper = SetupExecutable(tableSchema)
                Case "Consultant"
                    consultantExecutableWrapper = SetupExecutable(tableSchema)
                Case "Request"
                    requestExecutableWrapper = SetupExecutable(tableSchema)
                Case "Job"
                    jobExecutableWrapper = SetupExecutable(tableSchema)
                Case "JobStatus"
                    jobStatusExecutableWrapper = SetupExecutable(tableSchema)
                Case "InterestedConsultant"
                    interestedConsultantExecutableWrapper = SetupExecutable(tableSchema)
            End Select
        Next
    End Sub

#If UseSharedStuff Then
    Private Shared Function SetupExecutable(ByVal Schema As DataBaseSchema.TableSchemaRow) As Input.ExecutableWrapper
#Else
    Private Function SetupExecutable(ByVal Schema As DataBaseSchema.TableSchemaRow) As Input.ExecutableWrapper
#End If
        Dim executable As New Input.Executable
        executable.TableSchema = Schema
        executable.DataSchema = dataTypeMappingList

        Dim result As New Input.ExecutableWrapper(executable)
        inputGatewayFunctionMapping.Add(String.Format("Create{0}", Schema.TableName), result)
        inputGatewayFunctionMapping.Add(String.Format("Update{0}", Schema.TableName), result)
        inputGatewayFunctionMapping.Add(String.Format("Delete{0}", Schema.TableName), result)
        inputGatewayFunctionMapping.Add(String.Format("Save{0}", Schema.TableName), result)

        Return result
    End Function
#End Region

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()


        'Add your own initialization code after the InitializeComponent() call
#If Not UseSharedStuff Then
        Me.NewNotShared()
#End If

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

#Region " main web service method "

    <WebMethod()> _
    Public Sub Submit(ByVal tasks() As Task, ByVal type As SubmitType)
        If tasks Is Nothing Then
            Return
        End If
        Try
            ' RCP 2006-06-08, brought in code from InsertGatewayTasksIntoDatabaseAndSetStartUp, since it was only ever referenced from here anyway.
            'InsertGatewayTasksIntoDatabaseAndSetStartUp(taskObjs) 'TODO: RCP 2006-06-07 what means set startup? - possibly just the task state ?

            Dim connectionWrapperObj As Data.ConnectionWrapper = activiserDBConnectionManager.Connection
            Dim taskState As GatewayTaskState = GatewayTaskState.NullState
            Dim exceptionObj As Exception = Nothing

            ' check duplicated tasks & update gateway task state
            If InsertGatewayTaskGroupIntoDatabase(connectionWrapperObj, tasks) Then
                Try
                    If CheckforDuplicateTasks(connectionWrapperObj, tasks) Then
                        taskState = GatewayTaskState.EndDueToTaskDuplication
                        'RCP 2006-06-08, removed exception - this isn't an error condition.
                        'exceptionObj = New DuplicateTaskException(taskObjs(0).TaskID)
                    Else
                        taskState = GatewayTaskState.NotExecutedYet
                    End If
                Catch ex As Exception
                    taskState = GatewayTaskState.EndDueToCheckDuplicatedTaskFailure
                    exceptionObj = New ApplicationException("Error during duplicate task check", ex)
                End Try
            Else
                taskState = GatewayTaskState.EndDueToInsertIntoDBFailure
                exceptionObj = New ApplicationException("One or more invalid tasks submitted.")
            End If
            ' update state
            For Each t As Task In tasks
                UpdateGatewayTaskState(connectionWrapperObj, t.TaskID, taskState)
            Next

            'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
            If Not exceptionObj Is Nothing Then
                Throw exceptionObj
            ElseIf IsGatewayTaskGroupSynchronous(tasks, type) Then
                ProcessRoutine(tasks(0))
            Else
                Dim threadObj As New _
                    Threading.Thread(AddressOf New CallableToProcessRoutine(Me, tasks(0)).Invoke)
                If threadObj IsNot Nothing Then
                    threadObj.Start()
                End If
            End If
        Catch ex As Exception
            Throw New Protocols.SoapException("Error processing request to activiser™ gateway", Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Sub

    <WebMethod()> _
    Public Sub Listen(ByVal taskID As Guid, ByVal isTaskExecutionSuccessful As Boolean, ByVal returnValue As String)
        Dim connectionWrapperObj As Data.ConnectionWrapper = activiserDBConnectionManager.Connection
        Dim gatewayTaskObj1 As GatewayEntity.GatewayTask = Nothing
        Dim gatewayTaskObj2 As GatewayEntity.GatewayTask = Nothing
        Dim taskObj1 As Task = Nothing
        Dim taskObj2 As Task = Nothing
        Dim isConnectionReturned As Boolean = False
        Try
            gatewayTaskObj1 = RetrieveGatewayTaskByTaskID(connectionWrapperObj.Connection, taskID)
            gatewayTaskObj2 = RetrieveNextGatewayTaskByTaskInstance(connectionWrapperObj.Connection, gatewayTaskObj1)
            taskObj1 = Deserialize(connectionWrapperObj, gatewayTaskObj1)
            If Not gatewayTaskObj2 Is Nothing Then
                taskObj2 = Deserialize(connectionWrapperObj, gatewayTaskObj2)
            End If
            If isTaskExecutionSuccessful Then
                ' insert into gateway duplicated task table
                ' assumption: 0 chance of error
                If TypeOf taskObj1 Is Input.InputGatewayTask Then
                    InsertIntoGatewayDuplicatedTaskTable(connectionWrapperObj, CType(taskObj1, Input.InputGatewayTask))
                End If
                ' insert into gateway return valueTable
                ' assumption: 0 chance of error
                If Not returnValue Is Nothing Then
                    InsertIntoGatewayReturnValueTable(connectionWrapperObj, taskObj1, returnValue)
                End If
                If gatewayTaskObj2 Is Nothing Then
                    Commit(connectionWrapperObj, gatewayTaskObj1)
                    'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
                    isConnectionReturned = True
                Else
                    'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
                    isConnectionReturned = True
                    ProcessRoutine(taskObj2)
                End If
            Else
                ' delete gateway duplicated task
                If TypeOf taskObj1 Is Output.OutputGatewayTask Then
                    DeleteGatewayDuplicatedTask(connectionWrapperObj, CType(taskObj1, Output.OutputGatewayTask))
                End If
                Me.Abort(connectionWrapperObj, gatewayTaskObj1)
                'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
                isConnectionReturned = True
            End If
        Catch ex As Exception
            LogError(Nothing, DateTime.UtcNow, "activiser gateway", "Sub Listen", "Error in Sub Listen", "", Guid.Empty, "", ex)
            'Throw New Protocols.SoapException("[Sub Listen]", Protocols.SoapException.ServerFaultCode, ex)
        Finally
            If Not isConnectionReturned Then
                Try
                    'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
                Catch ex As Exception
                    LogError(Nothing, DateTime.UtcNow, "activiser gateway", "Sub Listen", "Error releasing connection in Sub Listen", "", Guid.Empty, "", ex)
                End Try
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Gets the return value from a gateway function call
    ''' </summary>
    ''' <param name="taskID">task identifier for previously called gateway function</param>
    ''' <param name="purgeResult">delete the value from the database after retrieving it</param>
    ''' <returns>the result of the function call, as a string</returns>
    ''' <remarks></remarks>
    <WebMethod()> Public Function GetFunctionResult(ByVal taskID As Guid, ByVal purgeResult As Boolean) As String
        ' ConnectionWrapper
        Dim connectionWrapperObj As Data.ConnectionWrapper = activiserDBConnectionManager.Connection
        ' common.dbconnection
        Dim connection As Common.DbConnection = connectionWrapperObj.Connection()
        ' EntityWrapper
        Dim exceptionObj As Exception
        Dim returnValue As String = ""
        Try
            Dim gatewayReturnValueObj As New GatewayEntity.GatewayReturnValue(taskID)
            Dim ew As New EntityWrapper(gatewayReturnValueObj, taskID)
            Dim obj As Object = returnValueExecutable.Retrieve(connection, ew)
            If obj IsNot Nothing Then
                returnValue = CType(obj, GatewayEntity.GatewayReturnValue).ReturnValue
                exceptionObj = Nothing
                If purgeResult Then
                    Dim gatewayReturnValueObj2 As New GatewayEntity.GatewayReturnValue
                    gatewayReturnValueObj2.TaskID = taskID
                    ew.Entity = gatewayReturnValueObj2
                    Try
                        returnValueExecutable.Delete(connection, ew)
                    Catch ex As Exception
                        Dim gex As New GatewayException("Error removing function result", ex)
                        StoreGatewayExceptionToDatabase(gex)
                    End Try
                End If
            End If
        Catch ex As GatewayException
            StoreGatewayExceptionToDatabase(ex)
        End Try
        'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
        Return returnValue
    End Function

    <WebMethod()> _
    Public Function CheckActiviserConnection() As String
        Try
            Dim wrapperObj As Data.ConnectionWrapper = activiserDBConnectionManager.Connection
            Dim cmd As IDbCommand = Nothing
            Dim connection As Common.DbConnection = wrapperObj.Connection

            If TypeOf connection Is System.Data.OleDb.OleDbConnection Then
                cmd = New OleDb.OleDbCommand("SELECT GETUTCDATE()", CType(connection, OleDb.OleDbConnection))
            ElseIf TypeOf connection Is System.Data.Odbc.OdbcConnection Then
                cmd = New Odbc.OdbcCommand("SELECT GETUTCDATE()", CType(connection, Odbc.OdbcConnection))
            ElseIf TypeOf connection Is System.Data.SqlClient.SqlConnection Then
                cmd = New SqlClient.SqlCommand("SELECT GETUTCDATE()", CType(connection, SqlConnection))
            End If

            If cmd IsNot Nothing Then
                cmd.CommandType = CommandType.Text

                If cmd.Connection.State <> ConnectionState.Open Then cmd.Connection.Open()
                Dim dResult As Date = CDate(cmd.ExecuteScalar())
                'activiserDBConnectionManager.ReleaseConnection(wrapperObj)

                Return "Connection successfully established at " & dResult.ToString
            Else
                Return "Unable to create database command object: unspecified error"
            End If

        Catch ex As Exception
            Return "Unable to create database command object:" & ex.ToString
        End Try
    End Function

    ' deprecated
    '<WebMethod()> _
    'Public Function CheckCRMConnection() As String
    '    Return CheckOutputGatewayConnection()
    'End Function

    <WebMethod()> _
    Public Function CheckOutputGatewayConnection() As String
        Dim result As String = ""
        Dim drs As GatewayFunctionList.ActionRow() = outputGatewayFunctionList.Action.FindAllByName("CheckConnection")
        Dim checkFunction As GatewayFunctionList.ActionRow
        If drs IsNot Nothing AndAlso drs.Length > 0 Then
            For Each checkFunction In drs
                If checkFunction.Priority <> -1 Then ' -1 = disabled
                    Try
                        Dim assemblyName As String = checkFunction.AssemblyName
                        Dim className As String = checkFunction.ClassName
                        If Not System.IO.Path.IsPathRooted(assemblyName) Then
                            assemblyName = System.IO.Path.Combine(Me.Context.Request.PhysicalApplicationPath, assemblyName)
                        End If

                        Dim assemblyObj As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom(assemblyName)
                        'Dim args() As Object = {appSettings("OutputGatewayInitializer")}
                        Dim executableObj As Output.Executable = CType(Activator.CreateInstance(assemblyObj.GetType(className)), Output.Executable)
                        executableObj.ConnectionString = My.Settings.OutputGateway ' connectionStrings("OutputGateway").ConnectionString
                        executableObj.Initialise()

                        result &= "Result for target database: "
                        result &= vbTab & checkFunction.ClassName & vbNewLine
                        result &= executableObj.CheckConnection()

                    Catch ex As Exception
                        result &= "Failed to connect to target database: " & vbNewLine
                        result &= vbTab & checkFunction.ClassName & vbNewLine
                        result &= vbTab & ex.ToString
                    End Try
                End If
            Next
        End If
        If result = "" Then
            result = "No output gateways currently providing a connection checking mechanism"
        End If
        Return result
    End Function

    <WebMethod(Description:="Test authentication")> _
        Public Function WhoAmI() As String
        Dim result As String
        If Me.User.Identity.IsAuthenticated Then
            result = "Authenticated as: " & Me.User.Identity.Name
        Else
            result = "Not Authenticated"
        End If
        Return result
    End Function
#End Region

#Region " retrieve web service method "

    <WebMethod(), _
        XmlInclude(GetType(BusinessEntity.InterestedConsultant)), SoapInclude(GetType(BusinessEntity.InterestedConsultant)), _
        XmlInclude(GetType(BusinessEntity.ClientSite)), SoapInclude(GetType(BusinessEntity.ClientSite)), _
        XmlInclude(GetType(BusinessEntity.Request)), SoapInclude(GetType(BusinessEntity.Request)), _
        XmlInclude(GetType(BusinessEntity.Job)), SoapInclude(GetType(BusinessEntity.Job)), _
        XmlInclude(GetType(BusinessEntity.Consultant)), SoapInclude(GetType(BusinessEntity.Consultant))> _
    Public Function Retrieve(ByVal obj As Entity) As Entity
        If TypeOf obj Is BusinessEntity.Job Then
            Return RetrieveJob(CType(obj, BusinessEntity.Job))
        ElseIf TypeOf obj Is BusinessEntity.Request Then
            Return RetrieveRequest(CType(obj, BusinessEntity.Request))
        ElseIf TypeOf obj Is BusinessEntity.ClientSite Then
            Return RetrieveClientSite(CType(obj, BusinessEntity.ClientSite))
        ElseIf TypeOf obj Is BusinessEntity.Consultant Then
            Return RetrieveConsultant(CType(obj, BusinessEntity.Consultant))
        ElseIf TypeOf obj Is BusinessEntity.InterestedConsultant Then
            Return RetrieveInterestedConsultant(CType(obj, BusinessEntity.InterestedConsultant))
        Else
            Throw New Protocols.SoapException("Function Retrieve", _
                Protocols.SoapException.ServerFaultCode, _
                New ApplicationException("entity type not supported by activiser™ gateway "))
        End If
    End Function

    <WebMethod()> Public Function RetrieveJob(ByVal job As BusinessEntity.Job) As BusinessEntity.Job
        Try
            Dim ew As New EntityWrapper(job)
            Return CType(jobExecutableWrapper.Retrieve(activiserDBConnectionManager, ew), BusinessEntity.Job)
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveJob", _
                Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

    '<WebMethod()> Public Function RetrieveJobByUID(ByVal JobUID As Guid) As BusinessEntity.Job
    '    Try
    '        Dim job As New BusinessEntity.Job()
    '        job.JobUID = JobUID
    '        Dim ew As New EntityWrapper(job)
    '        Return CType(jobExecutableWrapper.Retrieve(activiserDBConnectionManager, ew), BusinessEntity.Job)
    '    Catch ex As Exception
    '        Throw New Protocols.SoapException("Function RetrieveJob", _
    '            Protocols.SoapException.ServerFaultCode, ex)
    '    End Try
    'End Function

    '<WebMethod()> Public Function DebugRetrieveJob(ByVal JobUID As String) As BusinessEntity.Job
    '    Try
    '        Dim job As New BusinessEntity.Job()
    '        job.JobUID = New Guid(JobUID)
    '        Dim ew As New EntityWrapper(job)
    '        Return CType(jobExecutableWrapper.Retrieve(activiserDBConnectionManager, ew), BusinessEntity.Job)
    '    Catch ex As Exception
    '        Throw New Protocols.SoapException("Function RetrieveJob", _
    '            Protocols.SoapException.ServerFaultCode, ex)
    '    End Try
    'End Function


    <WebMethod()> Public Function RetrieveRequest(ByVal obj As BusinessEntity.Request) As BusinessEntity.Request
        Try
            Dim ew As New EntityWrapper
            ew.Entity = obj
            Return CType(requestExecutableWrapper.Retrieve( _
                activiserDBConnectionManager, ew), BusinessEntity.Request)
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveRequest", _
                Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

    <WebMethod()> Public Function RetrieveClientSite(ByVal businessObject As BusinessEntity.ClientSite) As BusinessEntity.ClientSite
        Dim entityWrapper As New EntityWrapper
        entityWrapper.Entity = businessObject
        Return CType(clientSiteExecutableWrapper.Retrieve(activiserDBConnectionManager, entityWrapper), BusinessEntity.ClientSite)
    End Function

    <WebMethod()> Public Function RetrieveConsultant(ByVal bo As BusinessEntity.Consultant) As BusinessEntity.Consultant
        Try
            Dim ew As New EntityWrapper(bo)
            Return CType(consultantExecutableWrapper.Retrieve(activiserDBConnectionManager, ew), BusinessEntity.Consultant)
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveConsultant", Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

    <WebMethod()> Public Function RetrieveInterestedConsultant(ByVal obj As BusinessEntity.InterestedConsultant) As BusinessEntity.InterestedConsultant
        Try
            Dim ew As New EntityWrapper
            ew.Entity = obj
            Return CType(interestedConsultantExecutableWrapper.Retrieve( _
                activiserDBConnectionManager, ew), BusinessEntity.InterestedConsultant)
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveInterestedConsultant", _
                Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

#End Region

#Region " retrieve multiple web service method "

    <WebMethod(), _
    System.Xml.Serialization.XmlInclude(GetType(BusinessEntity.InterestedConsultant)), System.Xml.Serialization.SoapInclude(GetType(BusinessEntity.InterestedConsultant)), _
    System.Xml.Serialization.XmlInclude(GetType(BusinessEntity.ClientSite)), System.Xml.Serialization.SoapInclude(GetType(BusinessEntity.ClientSite)), _
    System.Xml.Serialization.XmlInclude(GetType(BusinessEntity.Request)), System.Xml.Serialization.SoapInclude(GetType(BusinessEntity.Request)), _
      System.Xml.Serialization.XmlInclude(GetType(BusinessEntity.Job)), System.Xml.Serialization.SoapInclude(GetType(BusinessEntity.Job)), _
      System.Xml.Serialization.XmlInclude(GetType(BusinessEntity.Consultant)), System.Xml.Serialization.SoapInclude(GetType(BusinessEntity.Consultant))> _
    Public Function RetrieveMultiple(ByVal obj As Entity, ByVal numbers As Integer) As Entity()
        If TypeOf obj Is BusinessEntity.Job Then
            Return RetrieveMultipleJob(CType(obj, BusinessEntity.Job), numbers)
        ElseIf TypeOf obj Is BusinessEntity.Request Then
            Return RetrieveMultipleRequest(CType(obj, BusinessEntity.Request), numbers)
        ElseIf TypeOf obj Is BusinessEntity.ClientSite Then
            Return RetrieveMultipleClientSite(CType(obj, BusinessEntity.ClientSite), numbers)
        ElseIf TypeOf obj Is BusinessEntity.Consultant Then
            Return RetrieveMultipleConsultant(CType(obj, BusinessEntity.Consultant), numbers)
        ElseIf TypeOf obj Is BusinessEntity.InterestedConsultant Then
            Return RetrieveMultipleInterestedConsultant(CType(obj, BusinessEntity.InterestedConsultant), numbers)
        Else
            Throw New Protocols.SoapException("Function Retrieve", _
                Protocols.SoapException.ServerFaultCode, _
                New ApplicationException("entity type not supported by activiser™ gateway "))
        End If
    End Function

    <WebMethod()> _
    Public Function RetrieveMultipleJob(ByVal obj As BusinessEntity.Job, _
        ByVal numbers As Integer) As BusinessEntity.Job()
        Try
            Dim ew As New EntityWrapper
            ew.Entity = obj
            Return CType(jobExecutableWrapper.RetrieveMultiple( _
                activiserDBConnectionManager, ew, _
                numbers), BusinessEntity.Job())
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveJob", _
                Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

    <WebMethod()> _
    Public Function RetrieveMultipleRequest(ByVal obj As BusinessEntity.Request, _
        ByVal numbers As Integer) As BusinessEntity.Request()
        Try
            Dim ew As New EntityWrapper
            ew.Entity = obj
            Return CType(requestExecutableWrapper.RetrieveMultiple( _
                activiserDBConnectionManager, ew, _
                numbers), BusinessEntity.Request())
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveRequest", _
                Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

    <WebMethod()> _
    Public Function RetrieveMultipleClientSite( _
        ByVal businessObject As BusinessEntity.ClientSite, ByVal numbers As Integer) _
        As BusinessEntity.ClientSite()
        Dim entityWrapper As New EntityWrapper
        entityWrapper.Entity = businessObject
        Return CType(clientSiteExecutableWrapper.RetrieveMultiple( _
            activiserDBConnectionManager, entityWrapper, numbers), _
            BusinessEntity.ClientSite())
    End Function

    <WebMethod()> _
    Public Function RetrieveMultipleConsultant( _
        ByVal obj As BusinessEntity.Consultant, ByVal numbers As Integer) _
        As BusinessEntity.Consultant()
        Try
            Dim ew As New EntityWrapper
            ew.Entity = obj
            Return CType(consultantExecutableWrapper.RetrieveMultiple( _
                activiserDBConnectionManager, ew, numbers), _
                BusinessEntity.Consultant())
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveConsultant", _
                Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

    <WebMethod()> _
    Public Function RetrieveMultipleInterestedConsultant( _
        ByVal obj As BusinessEntity.InterestedConsultant, ByVal numbers As Integer) _
        As BusinessEntity.InterestedConsultant()
        Try
            Dim ew As New EntityWrapper
            ew.Entity = obj
            Return CType(interestedConsultantExecutableWrapper.RetrieveMultiple( _
                activiserDBConnectionManager, ew, numbers), BusinessEntity.InterestedConsultant())
        Catch ex As Exception
            Throw New Protocols.SoapException("Function RetrieveInterestedConsultant", _
                Protocols.SoapException.ServerFaultCode, ex)
        End Try
    End Function

#End Region

#Region " submit web service methods' helper method "

    ' validate gateway task; throws exception if the content is not valid
    Protected Sub ValidateGatewayTask(ByRef taskObj As Task)
        If taskObj.TaskID.Equals(Guid.Empty) Then
            Throw New ApplicationException("TaskID missing")
        ElseIf taskObj.Equals(Guid.Empty) Then
            Throw New ApplicationException("TaskGroupID missing")
        ElseIf taskObj.Entity Is Nothing Then
            Throw New ApplicationException("activiser™ gateway entity missing")
        ElseIf taskObj.Key Is Nothing Then
            Throw New ApplicationException("Task Key missing")
        Else
            Dim functionSchemaDataObj As GatewayFunctionList.ActionRow = Nothing
            If TypeOf taskObj Is Input.InputGatewayTask Then
                If taskObj.TaskType = TaskType.Output Then
                    Throw New ApplicationException("Gateway Action type mismatch (input task object/output task type)")
                End If

                'Dim drs As GatewayFunctionList.ActionRow() = inputGatewayFunctionList.Action.Select(String.Format("Name='{0}'", taskObj.Key))
                'If drs IsNot Nothing AndAlso drs.Length > 0 Then
                '    functionSchemaDataObj = drs(0)
                'End If
                functionSchemaDataObj = inputGatewayFunctionList.Action.FindFirstByName(taskObj.Key) '
            Else
                If taskObj.TaskType = TaskType.Input Then
                    Throw New ApplicationException("Gateway Action type mismatch (output task object/input task type)")
                End If
                functionSchemaDataObj = outputGatewayFunctionList.Action.FindFirstByName(taskObj.Key)

                'Dim drs As GatewayFunctionList.ActionRow() = outputGatewayFunctionList.Action.Select(String.Format("Name='{0}'", taskObj.Key))
                'If drs IsNot Nothing AndAlso drs.Length > 0 Then
                '    functionSchemaDataObj = drs(0)
                'End If
                'functionSchemaDataObj = CType(outputGatewayFunctionSchema.Mapping.Item(taskObj.Key), FunctionSchema.Data)
            End If
            If functionSchemaDataObj Is Nothing Then
                Throw New ApplicationException(String.Format("Gateway Action definition for task '{0}' not found", taskObj.Key))
            End If
        End If
        If TypeOf taskObj Is Output.OutputGatewayTask Then
            Dim outputGatewayTaskObj As Output.OutputGatewayTask = CType(taskObj, Output.OutputGatewayTask)
            Dim taskIDs() As Guid = outputGatewayTaskObj.RelatedInputGatewayTask
            If Not taskIDs Is Nothing Then
                Dim maxIndex As Integer = taskIDs.Length - 1
                For i As Integer = 0 To maxIndex
                    If taskIDs(i).Equals(Guid.Empty) Then
                        Throw New ApplicationException("Items of RelatedInputGatewayTask cannot be null")
                    End If
                Next
            End If
        End If
    End Sub

    ' insert gateway tasks into database
    Protected Function InsertGatewayTaskGroupIntoDatabase(ByVal connectionWrapper As Data.ConnectionWrapper, ByRef taskObjs() As Task) As Boolean
        Dim ew As New EntityWrapper
        Dim maxIndex As Integer = taskObjs.Length - 1
        Dim taskGroupID As Guid = taskObjs(maxIndex).TaskGroupID
        Dim isExecutionWithoutErr As Boolean = True
        Dim taskState As GatewayTaskState = GatewayTaskState.InsertIntoDB
        Dim gatewayTaskObj As GatewayEntity.GatewayTask = Nothing
        Dim gatewayTaskObj2 As GatewayEntity.GatewayTask = Nothing
        For i As Integer = maxIndex To 0 Step -1
            Try
                ' validate gateway task
                ValidateGatewayTask(taskObjs(i))
                ' insert
                gatewayTaskObj = New GatewayEntity.GatewayTask
                gatewayTaskObj.TaskID = taskObjs(i).TaskID
                gatewayTaskObj.TaskGroupID = taskGroupID
                gatewayTaskObj.Task = taskObjs(i).Serialize
                gatewayTaskObj.State = taskState.ToString()
                gatewayTaskObj.UpdateTime = DateTime.UtcNow
                If i <> maxIndex Then
                    gatewayTaskObj.NextTaskID = taskObjs(i + 1).TaskID
                End If
                ew.Entity = gatewayTaskObj
                taskExecutable.Create(connectionWrapper.Connection, ew)

                'Catch ex As GatewayException

            Catch ex As Exception
                isExecutionWithoutErr = False
                ' insert with empty body & new id
                While True
                    ' 0 chance failure assumption - 2006-05-30 RCP - what dumbass thought this up ?
                    Try
                        gatewayTaskObj2 = New GatewayEntity.GatewayTask
                        taskObjs(i).TaskID = Guid.NewGuid
                        gatewayTaskObj2.TaskID = taskObjs(i).TaskID
                        gatewayTaskObj2.State = taskState.ToString()
                        gatewayTaskObj2.UpdateTime = DateTime.UtcNow
                        If i = maxIndex Then
                            taskGroupID = Guid.NewGuid
                        End If
                        gatewayTaskObj2.TaskGroupID = taskGroupID
                        ew.Entity = gatewayTaskObj2
                        taskExecutable.Create(connectionWrapper.Connection, ew)
                        Exit While
                    Catch ex2 As Exception
                        ' possible reason
                        ' 1. Primary key conflicts
                        ' 2. GatewayEntity.GatewayTask Entity object doesn't match the GatewayEntity.GatewayTask table
                        ' 3. schema is wrong
                        ' 4. connection setting-up failure
                        Throw New ApplicationException("Error inserting task group into database", ex2)
                        Exit While
                    End Try
                End While
                ' error handling
                Dim gatewayExceptionObj As GatewayException
                If Not TypeOf ex Is GatewayException Then
                    If gatewayTaskObj IsNot Nothing AndAlso gatewayTaskObj2 IsNot Nothing Then
                        gatewayExceptionObj = New GatewayException(ex.Message, ex, gatewayTaskObj2, gatewayTaskObj.TaskGroupID, _
                            Nothing, CType(taskState, Gateway.TaskExecutionState), GatewayTaskState.NullState, Reflection.MethodBase.GetCurrentMethod.Name)
                        StoreGatewayExceptionToDatabase(gatewayExceptionObj)
                    End If
                Else
                    gatewayExceptionObj = CType(ex, GatewayException)
                    gatewayExceptionObj.GatewayTaskState = taskState
                    ' we canno't know the task id until it is inserted to the database
                    gatewayExceptionObj.TaskID = gatewayTaskObj2.TaskID
                    StoreGatewayExceptionToDatabase(gatewayExceptionObj)
                End If
            End Try
        Next
        Return isExecutionWithoutErr
    End Function

    ' check if there is any duplicated tasks in the submitted task Ggroup
    ' return true if there is a duplicated task, otherwise, false

    Protected Function CheckforDuplicateTasks(ByVal connection As Data.ConnectionWrapper, ByRef tasks() As Task) As Boolean
        ' checking + deleting
        Dim ew As EntityWrapper
        Dim isDuplicated As Boolean = False
        Dim taskObj As Task = Nothing
        Dim duplicatedTask As GatewayEntity.GatewayDuplicatedTask
        Dim taskState As GatewayTaskState = GatewayTaskState.CheckDuplicatedTask
        Try
            For Each taskObj In tasks ' loop to go through all submitted task
                Dim inputTask As activiser.Gateway.Input.InputGatewayTask = TryCast(taskObj, Input.InputGatewayTask)
                If inputTask IsNot Nothing Then ' only check input gateway task
                    If inputTask.FeedbackExpected Then ' only check input gateway task if it's expecting a duplicate
                        duplicatedTask = New GatewayEntity.GatewayDuplicatedTask(taskObj.PropertySet.Serialize, Nothing) ' select from the dupplicated task table
                        ew = New EntityWrapper(duplicatedTask, taskObj.TaskID)
                        Dim dupTaskObjs() As Entity = _
                            duplicatedTaskExecutable.RetrieveMultiple(connection.Connection, ew, 0)
                        If Not dupTaskObjs Is Nothing Then
                            ', GatewayEntity.GatewayDuplicatedTask())
                            For Each duplicatedTask In dupTaskObjs ' found a duplicated task
                                Dim taskXml As String = taskObj.Entity.ActivePropertySet.Serialize
                                If duplicatedTask.EntityPropertySet = taskXml Then ' we've found a duplicate transaction.
                                    isDuplicated = True
                                    ' remove the duplicated task from the duplicated task table - the expected feedback has been received!
                                    duplicatedTaskExecutable.Delete(connection.Connection, New EntityWrapper(New GatewayEntity.GatewayDuplicatedTask(duplicatedTask.TaskID), taskObj.TaskID))
                                    ' Exit For ' RCP 2006-06-08 removed exit for, to allow for multiple entries - unlikely, but, like, you know.
                                End If
                            Next
                        End If
                    End If
                End If
            Next
        Catch ex As GatewayException
            ex.GatewayTaskState = taskState
            StoreGatewayExceptionToDatabase(ex)
        Catch ex As Exception
            ' error handling
            Dim gatewayExceptionObj As GatewayException
            If taskObj IsNot Nothing Then
                gatewayExceptionObj = New GatewayException(ex.Message, ex, taskObj.Entity, taskObj.TaskID, taskObj.PreviousEntity, CType(taskState, Gateway.TaskExecutionState), GatewayTaskState.ExecutionFailure, Reflection.MethodBase.GetCurrentMethod.Name)
            Else
                gatewayExceptionObj = New GatewayException(ex.Message, ex, Nothing, Nothing, Nothing, CType(taskState, Gateway.TaskExecutionState), GatewayTaskState.ExecutionFailure, Reflection.MethodBase.GetCurrentMethod.Name)
            End If
            StoreGatewayExceptionToDatabase(gatewayExceptionObj)
        End Try
        Return isDuplicated
    End Function

    ' check gateway task group synchronous; return true if it is synchronous, otherwise, false
    Protected Function IsGatewayTaskGroupSynchronous(ByRef taskObjs() As Task, ByVal type As SubmitType) As Boolean
        Dim isSynchronous As Boolean = True
        If type = SubmitType.Auto Then
            Dim functionSchemaDataObj As GatewayFunctionList.ActionRow = Nothing
            Dim maxIndex As Integer = taskObjs.Length - 1
            ' examine DoesThirdPartyCallback property
            For i As Integer = 0 To maxIndex
                If TypeOf taskObjs(i) Is Input.InputGatewayTask Then
                    functionSchemaDataObj = inputGatewayFunctionList.Action.FindFirstByName(taskObjs(i).Key)
                Else
                    functionSchemaDataObj = outputGatewayFunctionList.Action.FindFirstByName(taskObjs(i).Key)
                End If
                If functionSchemaDataObj IsNot Nothing AndAlso functionSchemaDataObj.DoesThirdPartyCallback Then
                    ' thrid party calls back; therefore, the gateway execution is asynchronous
                    isSynchronous = False
                    Exit For
                End If
            Next
        ElseIf type = SubmitType.Synchronous Then
            isSynchronous = True
        Else
            isSynchronous = False
        End If
        Return isSynchronous
    End Function

#End Region

#Region " listen web service methods' helper method "

    ' retrieve gateway task from database by task id
    Protected Function RetrieveGatewayTaskByTaskID( _
        ByVal connection As Common.DbConnection, ByVal taskID As Guid) _
        As GatewayEntity.GatewayTask
        ' Exception 
        Dim result As GatewayEntity.GatewayTask = Nothing
        Dim exceptionObj As GatewayException = Nothing
        Try
            Dim ew As New EntityWrapper
            Dim gatewayTaskObj As New GatewayEntity.GatewayTask
            gatewayTaskObj.TaskID = taskID

            ew.Entity = gatewayTaskObj
            Dim obj As Object
            obj = taskExecutable.Retrieve(connection, ew)
            If obj Is Nothing Then
                exceptionObj = New GatewayException( _
                        String.Format("Task ID = [{0}] not found", taskID), Nothing, _
                        Nothing, Nothing, Nothing, TaskExecutionState.NullState, GatewayTaskState.Listen, _
                        "Function RetrieveGatewayTaskByTaskID")
            Else
                result = CType(obj, GatewayEntity.GatewayTask)
            End If
        Catch ex As GatewayException
            ' 1. either user submits a wrong id or the task is deleted
            ex.GatewayTaskState = GatewayTaskState.Listen
            exceptionObj = ex
        End Try
        If Not exceptionObj Is Nothing Then
            ' store gateway exception to database
            StoreGatewayExceptionToDatabase(exceptionObj)
            Throw exceptionObj
        End If
        Return result
    End Function

    ' retrieve next gateway task from database by gateway task instance
    Protected Function RetrieveNextGatewayTaskByTaskInstance( _
        ByVal connection As Common.DbConnection, ByVal gatewayTaskObj As GatewayEntity.GatewayTask) _
        As GatewayEntity.GatewayTask

        Dim result As GatewayEntity.GatewayTask = Nothing
        If Not gatewayTaskObj.NextTaskID.Equals(Guid.Empty) Then
            Dim exceptionObj As GatewayException = Nothing
            Try
                Dim ew As New EntityWrapper(New GatewayEntity.GatewayTask(gatewayTaskObj.NextTaskID))
                Dim obj As Object = taskExecutable.Retrieve(connection, ew)
                If obj Is Nothing Then
                    exceptionObj = New GatewayException("Task not found", New TaskNotFoundException(gatewayTaskObj.NextTaskID), Nothing, Nothing, Nothing, TaskExecutionState.NullState, GatewayTaskState.Listen, Reflection.MethodBase.GetCurrentMethod.Name)
                Else
                    result = CType(obj, GatewayEntity.GatewayTask)
                End If
            Catch ex As GatewayException
                ' 1. the task is deleted from databse
                ex.GatewayTaskState = GatewayTaskState.Listen
                exceptionObj = ex

            End Try
            If Not exceptionObj Is Nothing Then
                ' store gateway exception to database
                StoreGatewayExceptionToDatabase(exceptionObj)
                Throw exceptionObj
            End If
        End If
        Return result
    End Function

    ' deseralize
    Protected Function Deserialize(ByVal connection As Data.ConnectionWrapper, _
        ByVal gatewayTaskObj As GatewayEntity.GatewayTask) As Task
        Try
            ' deserialize
            Return Task.Deserialize(gatewayTaskObj.Task, activiserEntityAssembly)
        Catch ex As Exception
            ' update gateway task state
            UpdateGatewayTaskState(connection, gatewayTaskObj.TaskID, GatewayTaskState.EndListenDueToSeriousErrors)

            ' convert exception to gatewayException
            Dim gatewayExceptionObj As New GatewayException( _
                    ex.Message, ex, _
                    Nothing, Nothing, Nothing, TaskExecutionState.NullState, GatewayTaskState.Listen, _
                    Reflection.MethodBase.GetCurrentMethod.Name)

            StoreGatewayExceptionToDatabase(gatewayExceptionObj)
            Throw gatewayExceptionObj
        End Try
    End Function

    ' insert into gateway duplicated task table
    Protected Sub InsertIntoGatewayDuplicatedTaskTable(ByVal connection As Data.ConnectionWrapper, _
        ByVal taskObj As Input.InputGatewayTask)
        Try
            If taskObj.FeedbackExpected Then
                ' EntityWrapper
                Dim ew As New EntityWrapper
                ' gatewayentity.GatewayDuplicatedTask
                Dim duplicatedTask As New GatewayEntity.GatewayDuplicatedTask
                duplicatedTask.TaskID = taskObj.TaskID
                duplicatedTask.TaskPropertySet = taskObj.PropertySet.Serialize
                duplicatedTask.EntityPropertySet = taskObj.Entity.ActivePropertySet.Serialize
                duplicatedTask.UpdateTime = DateTime.UtcNow
                ew.Entity = duplicatedTask
                ew.TaskID = duplicatedTask.TaskID
                ' insert
                duplicatedTaskExecutable.Create(connection.Connection, ew)
            End If
        Catch ex As Exception
            ' update gateway task state
            UpdateGatewayTaskState(connection, taskObj.TaskID, _
                GatewayTaskState.EndListenDueToSeriousErrors)
            ' convert exception to gatewayException
            Dim gatewayExceptionObj As GatewayException
            If TypeOf ex Is GatewayException Then
                gatewayExceptionObj = CType(ex, GatewayException)
                gatewayExceptionObj.GatewayTaskState = GatewayTaskState.Listen
            Else
                gatewayExceptionObj = New GatewayException("Error recording duplicated task", ex, taskObj.Entity, taskObj.TaskID, taskObj.PreviousEntity, TaskExecutionState.NullState, GatewayTaskState.Listen, Reflection.MethodBase.GetCurrentMethod.Name)
            End If
            ' store gateway exception to database
            StoreGatewayExceptionToDatabase(gatewayExceptionObj)
            Throw gatewayExceptionObj
        End Try
    End Sub

    ' insert into gateway return value table
    Protected Sub InsertIntoGatewayReturnValueTable(ByVal connection As Data.ConnectionWrapper, _
        ByVal taskObj As Task, ByVal returnValue As String)
        Try
            ' EntityWrapper
            Dim ew As New EntityWrapper
            ' GatewayReturnValue
            Dim gatewayReturnValueObj As New GatewayEntity.GatewayReturnValue
            gatewayReturnValueObj.TaskID = taskObj.TaskID
            gatewayReturnValueObj.ReturnValue = returnValue
            ew.Entity = gatewayReturnValueObj
            ' insert
            returnValueExecutable.Create(connection.Connection, ew)
        Catch ex As Exception
            ' update gateway task state
            UpdateGatewayTaskState(connection, taskObj.TaskID, _
                GatewayTaskState.EndListenDueToSeriousErrors)
            ' convert exception to gatewayException
            Dim gatewayExceptionObj As GatewayException = TryCast(ex, GatewayException)
            If gatewayExceptionObj IsNot Nothing Then
                gatewayExceptionObj.GatewayTaskState = GatewayTaskState.Listen
            Else
                gatewayExceptionObj = New GatewayException(ex.Message, ex, Nothing, Nothing, Nothing, Gateway.TaskExecutionState.NullState, Gateway.GatewayTaskState.Listen, System.Reflection.MethodBase.GetCurrentMethod.Name)

                'gatewayExceptionObj = ConvertExceptionToGatewayException(ex, _
                '    Nothing, GatewayTaskState.Listen, _
                '     TaskExecutionState.NullState, _
                '    "[Sub InsertIntoGatewayReturnValueTable]", _
                '    Nothing, taskObj.TaskID)
            End If
            ' store gateway exception to database
            StoreGatewayExceptionToDatabase(gatewayExceptionObj)
            Throw gatewayExceptionObj
        End Try
    End Sub

    ' delete feedback task
    Protected Sub DeleteGatewayDuplicatedTask(ByVal connection As Data.ConnectionWrapper, _
        ByVal taskObj As Output.OutputGatewayTask)
        Dim inputGatewayTaskIDs() As Guid = taskObj.RelatedInputGatewayTask
        If Not inputGatewayTaskIDs Is Nothing Then
            Try
                Dim maxIndex As Integer = inputGatewayTaskIDs.Length - 1
                Dim ew As New EntityWrapper
                For i As Integer = 0 To maxIndex
                    Dim duplicatedTask As New GatewayEntity.GatewayDuplicatedTask

                    duplicatedTask.TaskID = inputGatewayTaskIDs(i)

                    ew.Entity = duplicatedTask
                    ew.TaskID = duplicatedTask.TaskID
                    duplicatedTaskExecutable.Delete(connection.Connection, ew)
                Next
            Catch ex As GatewayException
                UpdateGatewayTaskState(connection, taskObj.TaskID, GatewayTaskState.EndListenDueToSeriousErrors)
                ex.GatewayTaskState = GatewayTaskState.Listen
                StoreGatewayExceptionToDatabase(ex)
                Throw ex
            End Try
        End If
    End Sub

    ' abort
    Protected Sub Abort(ByVal connection As Data.ConnectionWrapper, ByVal gatewayTaskObj As GatewayEntity.GatewayTask)
        Dim ew As New EntityWrapper
        Dim isFirstAbort As Boolean = True
        Try
            Do While Not gatewayTaskObj Is Nothing
                Dim taskObj As Task = Task.Deserialize(gatewayTaskObj.Task, activiserEntityAssembly)
                Try
                    If isFirstAbort Then
                        isFirstAbort = False
                    Else
                        If taskObj.AbortWithTaskGroup Then
                            ExecuteTask(taskObj, "AbortMethod")
                            UpdateGatewayTaskState(connection, taskObj.TaskID, GatewayTaskState.FinishAbort)
                        Else
                            UpdateGatewayTaskState(connection, taskObj.TaskID, GatewayTaskState.AbortWithoutRollback)
                        End If
                    End If
                Catch ex As Exception
                    UpdateGatewayTaskState(connection, gatewayTaskObj.TaskID, GatewayTaskState.AbortFailure)
                    ' convert exception to gateway exception
                    Dim gatewayExceptionObj As GatewayException = TryCast(ex, GatewayException)
                    If gatewayExceptionObj IsNot Nothing Then
                        gatewayExceptionObj.GatewayTaskState = GatewayTaskState.Abort
                    Else
                        gatewayExceptionObj = New GatewayException(ex.Message, ex, Nothing, gatewayTaskObj.TaskID, Nothing, Gateway.TaskExecutionState.NullState, Gateway.GatewayTaskState.Abort, System.Reflection.MethodBase.GetCurrentMethod.Name)
                    End If
                    ' store gateway exception to database
                    StoreGatewayExceptionToDatabase(gatewayExceptionObj)
                End Try
                ' retrieve by using next id
                Dim gatewayTaskObj2 As New GatewayEntity.GatewayTask

                gatewayTaskObj2.NextTaskID = taskObj.TaskID

                ew.Entity = gatewayTaskObj2
                ew.TaskID = gatewayTaskObj2.TaskID
                gatewayTaskObj = CType(taskExecutable.Retrieve(connection.Connection, ew), GatewayEntity.GatewayTask)
            Loop
        Catch ex As Exception
            ' update gateway task state
            UpdateGatewayTaskState(connection, gatewayTaskObj.TaskID, GatewayTaskState.EndAbortDueToSeriousErrors)
            ' convert exception to gatewayException
            Dim gatewayExceptionObj As GatewayException = TryCast(ex, GatewayException)
            If gatewayExceptionObj IsNot Nothing Then
                gatewayExceptionObj.GatewayTaskState = GatewayTaskState.Abort
            Else
                gatewayExceptionObj = New GatewayException(ex.Message, ex, Nothing, Nothing, Nothing, Gateway.TaskExecutionState.NullState, Gateway.GatewayTaskState.Abort, System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
            ' store gateway exception to database
            StoreGatewayExceptionToDatabase(gatewayExceptionObj)
            Throw gatewayExceptionObj
        End Try
    End Sub

    ' commit
    Protected Sub Commit(ByVal connection As Data.ConnectionWrapper, ByVal gatewayTaskObj As GatewayEntity.GatewayTask)
        Dim ew As New EntityWrapper
        Do While Not gatewayTaskObj Is Nothing
            Dim taskObj As Task
            Try
                taskObj = Task.Deserialize(gatewayTaskObj.Task, activiserEntityAssembly)
                If taskObj.CommitWithTaskGroup Then ' exeucte & update state
                    ExecuteTask(taskObj, "CommitMethod")
                    UpdateGatewayTaskState(connection, taskObj.TaskID, GatewayTaskState.FinishCommit)
                Else ' update state
                    UpdateGatewayTaskState(connection, taskObj.TaskID, GatewayTaskState.CommitWithoutCommitMethodExecution)
                End If

                ' retrieve by using next id
                Dim gatewayTaskObj2 As New GatewayEntity.GatewayTask

                gatewayTaskObj2.NextTaskID = taskObj.TaskID

                ew.Entity = gatewayTaskObj2
                ew.TaskID = gatewayTaskObj2.TaskID
                gatewayTaskObj = CType(taskExecutable.Retrieve(connection.Connection, ew), GatewayEntity.GatewayTask)

            Catch ex As GatewayException
                UpdateGatewayTaskState(connection, gatewayTaskObj.TaskID, GatewayTaskState.CommitFailure)
                ex.GatewayTaskState = GatewayTaskState.Commit
                StoreGatewayExceptionToDatabase(ex)

            Catch ex As Exception
                UpdateGatewayTaskState(connection, gatewayTaskObj.TaskID, GatewayTaskState.CommitFailure)
                StoreGatewayExceptionToDatabase(New GatewayException(ex.Message, ex, Nothing, gatewayTaskObj.TaskID, Nothing, Gateway.TaskExecutionState.NullState, Gateway.GatewayTaskState.Commit, System.Reflection.MethodBase.GetCurrentMethod.Name))
                gatewayTaskObj = Nothing
            End Try
        Loop
    End Sub

#End Region

#Region " shared help method "
    ' store gatewayException to database
    Protected Sub StoreGatewayExceptionToDatabase(ByVal sourceException As GatewayException)
        If sourceException Is Nothing Then Return
        Try
            If sourceException.Entity Is Nothing Then
                LogError(sourceException.ExceptionID, DateTime.UtcNow, "activiser gateway", sourceException.Source, sourceException.Message, "Unknown", sourceException.TaskID, "", sourceException)
            Else
                LogError(sourceException.ExceptionID, DateTime.UtcNow, "activiser gateway", sourceException.Source, sourceException.Message, sourceException.Entity.Type, sourceException.TaskID, "", sourceException)
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
        ' assumption: this method never fails
        'Dim ew As New EntityWrapper
        'Dim wrapper As Data.ConnectionWrapper = activiserDBConnectionManager.GetConnection()
        'Dim connection As Common.DbConnection = wrapper.Connection()
        'Try
        '    Dim gatewayExceptionObj As New GatewayEntity.GatewayException
        '    '            If TypeOf sourceException Is GatewayException Then ' Exception Then ' Gateway Then
        '    'Dim sourceException As GatewayEntity.GatewayException = CType(exceptionObj, GatewayEntity.GatewayException)

        '    gatewayExceptionObj.ExceptionID = sourceException.ExceptionID ' assumption: must have an valid ID
        '    gatewayExceptionObj.Source = sourceException.Source ' nullable
        '    gatewayExceptionObj.Message = sourceException.Message ' nullable
        '    gatewayExceptionObj.Stacktrace = sourceException.ToString() ' nullable
        '    If Not sourceException.Entity Is Nothing Then ' nullable
        '        Try
        '            gatewayExceptionObj.Entity = sourceException.Entity.Serialize()
        '        Catch ex As Exception
        '            gatewayExceptionObj.Entity = Nothing
        '        End Try
        '    End If
        '    gatewayExceptionObj.GatewayTaskState = sourceException.GatewayTaskState.ToString ' nullable
        '    gatewayExceptionObj.TaskExecutionState = sourceException.TaskExecutionState.ToString ' nullable
        '    gatewayExceptionObj.OccurrenceTime = sourceException.Time ' nullable
        '    gatewayExceptionObj.ExtraNote = sourceException.ExtraNote ' nullable
        '    gatewayExceptionObj.ClassType = sourceException.ClassType ' nullable
        '    gatewayExceptionObj.TaskID = sourceException.TaskID ' nullable
        '    ' connection setting-up failure can cause loop forever
        '    ' but gateway will never work at all if the connection setting fails
        '    ' therefore, we can assume that connection is working
        '    ew.Entity = gatewayExceptionObj
        '    exceptionExecutable.Create(connection, ew)
        '    'End If
        'Catch ex As Exception
        '    LogError(sourceException.ExceptionID, DateTime.UtcNow, "activiser gateway", "Exception Logger", "Error logging gateway exception", "", Guid.Empty, "", ex)
        '    ' chance is 0 unless the static constructor fails
        '    ' Possible reasons
        '    ' 1. GatewayException business Entity doesn't match the GatewayException table
        '    ' 2. Connection setting up is wrong
        '    ' 3. Primary key conflict [but it exist the loop in the next run after assign a new exception id]
        '    ' 4. fault implementation of schema files
        '    ' 5. fault implementation of Executable class
        'End Try
        'activiserDBConnectionManager.ReleaseConnection(wrapper)
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ' update gateway task state
    Protected Sub UpdateGatewayTaskState(ByVal connection As Data.ConnectionWrapper, ByVal taskID As Guid, ByVal state As GatewayTaskState)
        ' set defaultEntityWrapper
        Dim ew As New EntityWrapper
        ' set gatewayTaskObj (business Object)
        Dim gatewayTaskObj As New GatewayEntity.GatewayTask

        Try
            ' task id
            gatewayTaskObj.TaskID = taskID

            ' time
            gatewayTaskObj.UpdateTime = DateTime.UtcNow

            ' status
            gatewayTaskObj.State = state.ToString

            ew.Entity = gatewayTaskObj
            ew.TaskID = taskID
            taskExecutable.Update(connection.Connection, ew, taskExecutable.DataSchema, taskExecutable.TableSchema)
        Catch ex As Exception
            Throw New GatewayException("Error updating gateway task state", ex)
        End Try

        ' chance of exception:
        ' 1. the static constructor fails; fix this error by making sure the connection setting is correct
        ' 2. or the taskID is not in the DB; only if the users delete the gateway task accidentally/delibrately, which is not likely to happen
    End Sub

    ' execute gateway task based on the property & also performs error handling
    Protected Sub ProcessRoutine(ByVal taskObj As Task)
        Dim connectionWrapperObj As Data.ConnectionWrapper = Nothing
        Dim ew As New EntityWrapper
        Dim returnValue As String = Nothing
        Dim gatewayExceptionObj As GatewayException = Nothing
        Try
            connectionWrapperObj = activiserDBConnectionManager.Connection
            Try
                returnValue = ExecuteTask(taskObj, "ActionMethod")
            Catch ex As GatewayException
                gatewayExceptionObj = ex
            Catch ex As Exception
                gatewayExceptionObj = New GatewayException(ex.Message, ex, Nothing, taskObj.TaskID, Nothing, Gateway.TaskExecutionState.NullState, Gateway.GatewayTaskState.Execute, System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try

            If gatewayExceptionObj IsNot Nothing Then
                UpdateGatewayTaskState(connectionWrapperObj, taskObj.TaskID, GatewayTaskState.ExecutionFailure)
                StoreGatewayExceptionToDatabase(gatewayExceptionObj)
                Dim listener As New CallableToListen(Me, taskObj.TaskID, False, returnValue)
                listener.Invoke()
                'Dim threadObj As New Threading.Thread(AddressOf New CallableToListen( _
                '    Me, taskObj.TaskID, False, returnValue).Invoke)
                'threadObj.Start()
                Throw gatewayExceptionObj
            End If

            Try
                UpdateGatewayTaskState(connectionWrapperObj, taskObj.TaskID, GatewayTaskState.FinishExecution)
                'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
            Catch ex As GatewayException
                Throw
            Catch ex As Exception
                Throw New GatewayException(ex.Message, ex, Nothing, taskObj.TaskID, Nothing, Gateway.TaskExecutionState.NullState, Gateway.GatewayTaskState.FinishExecution, System.Reflection.MethodBase.GetCurrentMethod.Name)
            End Try

            Dim functionSchemaDataObj As GatewayFunctionList.ActionRow = Nothing
            If TypeOf taskObj Is Input.InputGatewayTask Then
                functionSchemaDataObj = inputGatewayFunctionList.Action.FindFirstByName(taskObj.Key)
            Else
                functionSchemaDataObj = outputGatewayFunctionList.Action.FindFirstByName(taskObj.Key)
            End If
            If functionSchemaDataObj IsNot Nothing AndAlso Not functionSchemaDataObj.DoesThirdPartyCallback Then
                Listen(taskObj.TaskID, True, returnValue)
            End If

        Catch ex As Exception
        Finally
            If connectionWrapperObj IsNot Nothing Then
                'activiserDBConnectionManager.ReleaseConnection(connectionWrapperObj)
            End If
        End Try
        'Dim connection As Common.DbConnection = connectionWrapperObj.Connection
    End Sub

    ' execute task
    Protected Function ExecuteTask(ByVal taskObj As Task, ByVal param As String) As String
        If TypeOf taskObj Is Input.InputGatewayTask Then
            ExecuteInputGatewayTask(taskObj, param)
            Return Nothing
        Else
            Return ExecuteOutputGatewayTask(taskObj, param)
        End If
    End Function

    ' execute input gateway task


    ''' <summary>
    '''  This function will execute and input gateway task (no kidding!)
    ''' 
    ''' </summary>
    ''' <param name="taskObj"></param>
    ''' <param name="param"></param>
    ''' <remarks></remarks>
    Protected Sub ExecuteInputGatewayTask(ByVal taskObj As Task, ByVal param As String)
        ' ExecutableWrapper
        Dim executableWrapperObj As Input.ExecutableWrapper = CType( _
            inputGatewayFunctionMapping.Item(taskObj.Key), _
            Input.ExecutableWrapper)
        ' FunctionSchema.Data

        Dim drs As GatewayFunctionList.ActionRow() = inputGatewayFunctionList.Action.FindAllByName(taskObj.Key)
        Dim inputFunction As GatewayFunctionList.ActionRow

        If drs IsNot Nothing AndAlso drs.Length > 0 Then
            For Each inputFunction In drs
                'functionSchemaDataObj = drs(0)
                Try
                    Dim propertyInfoObj As Reflection.PropertyInfo = inputFunction.GetType().GetProperty(param)
                    Dim method As String = propertyInfoObj.GetValue(inputFunction, Nothing).ToString()
                    Dim methodInfoObj As Reflection.MethodInfo = executableWrapperObj.GetType().GetMethod( _
                        method, New Type() {GetType(Data.ConnectionManager), GetType(EntityWrapper)})

                    Dim ew As New EntityWrapper(taskObj.Entity, taskObj.PreviousEntity, taskObj.TaskID)

                    methodInfoObj.Invoke(executableWrapperObj, New Object() {activiserDBConnectionManager, ew})
                Catch ex As Exception
                    Throw New GatewayException("Error executing Input Gateway Task", ex)
                End Try
            Next
        End If
    End Sub

    Protected Function ExecuteOutputGatewayTask(ByVal taskObj As Task, ByVal param As String) As String
        ' FunctionSchema.Data
        Dim result As String = Nothing
        Dim drs As GatewayFunctionList.ActionRow() = outputGatewayFunctionList.Action.FindAllByName(taskObj.Key)
        Dim outputFunction As GatewayFunctionList.ActionRow
        If drs IsNot Nothing AndAlso drs.Length > 0 Then
            For Each outputFunction In drs
                If outputFunction.Priority <> -1 Then ' -1 = disabled
                    Try
                        Dim assemblyName As String = outputFunction.AssemblyName
                        Dim className As String = outputFunction.ClassName
                        If Not System.IO.Path.IsPathRooted(assemblyName) Then
                            assemblyName = System.IO.Path.Combine(Me.Context.Request.PhysicalApplicationPath, assemblyName)
                        End If

                        Dim assemblyObj As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom(assemblyName)
                        'Dim args() As Object = {appSettings("OutputGatewayInitializer")}
                        Dim executableObj As Output.Executable = CType(Activator.CreateInstance(assemblyObj.GetType(className)), Output.Executable)
                        executableObj.ConnectionString = My.Settings.OutputGateway ' connectionStrings("OutputGateway").ConnectionString
                        executableObj.Initialise()

                        Dim propertyInfoObj As Reflection.PropertyInfo = outputFunction.GetType().GetProperty(param)
                        Dim method As String = propertyInfoObj.GetValue(outputFunction, Nothing).ToString()
                        Dim methodInfoObj As Reflection.MethodInfo = executableObj.GetType().GetMethod(method, _
                            New Type() {GetType(EntityWrapper)})

                        Dim ew As New EntityWrapper
                        ew.Entity = taskObj.Entity
                        ew.PreviousEntity = taskObj.PreviousEntity
                        ew.TaskID = taskObj.TaskID
                        ' execute
                        result &= CStr(methodInfoObj.Invoke(executableObj, New Object() {ew})) & vbCrLf
                    Catch ex As Exception
                        Throw New GatewayException("Error executing Output Gateway Task", ex)
                    End Try
                End If
            Next
        End If
        Return result
    End Function

    'TODO: Rename this.
    Protected Class CallableToProcessRoutine
        Private _service As activiserGateway
        Private _task As Task

        Public Sub New(ByVal service As Services.WebService, ByVal taskObj As Task)
            Me._service = CType(service, activiserGateway)
            Me._task = taskObj
        End Sub

        Public Sub Invoke()
            Me._service.ProcessRoutine(Me._task)
        End Sub
    End Class

    Protected Class CallableToListen
        Private _service As activiserGateway
        Private _taskID As Guid
        Private _isTaskExecutionSuccessful As Boolean
        Private _returnValue As String

        Private _thread As Threading.Thread

        Public Sub New(ByVal service As Services.WebService, ByVal taskID As Guid, ByVal isTaskExecutionSuccessful As Boolean, ByVal returnValue As String)
            Me._service = CType(service, activiserGateway)
            Me._taskID = taskID
            Me._isTaskExecutionSuccessful = isTaskExecutionSuccessful
            Me._returnValue = returnValue
        End Sub

        Public Sub Invoke()
            _thread = New Threading.Thread(AddressOf Listener)
            _thread.Start()
        End Sub

        Private Sub Listener()
            Me._service.Listen(Me._taskID, Me._isTaskExecutionSuccessful, Me._returnValue)

            '(AddressOf New CallableToListen(Me, taskObj.TaskID, False, returnValue).Invoke)
        End Sub

        Protected Overrides Sub Finalize()
            If _thread IsNot Nothing Then
                If _thread.ThreadState = Threading.ThreadState.Running Then
                    Threading.Thread.CurrentThread.Join()
                End If
            End If
            MyBase.Finalize()
        End Sub
    End Class

#End Region
End Class
