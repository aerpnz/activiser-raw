Imports activiser
Imports activiser.WebService.Utilities
Imports System
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Reflection
Imports System.Data
Imports System.Text.RegularExpressions
Imports activiser.DataSchema
Imports activiser.activiserDataSet
Imports activiser.activiserDataSetTableAdapters
Imports activiser.ClientRegistrationDataSet
Imports activiser.ClientRegistrationDataSetTableAdapters

<System.Web.Services.WebService( _
    Name:="activiser" _
    , Description:="activiser™ Web Service version V4.0" _
    , Namespace:="http://www.activiser.com/activiser" _
    )> _
<System.Web.Services.WebServiceBinding(name:="activiser", ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<System.Runtime.InteropServices.ComVisible(False)> _
Public Class activiserClientWebService
    Inherits System.Web.Services.WebService

    Shared Sub New()
        ' load gateway list
        LoadGatewayList()
    End Sub

#Region "Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call
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

#Region "Consultant List Web Service Methods"

    <WebMethod()> _
    Public Function GetCurrentConsultantList(ByVal deviceId As String) As Utility.ActiveConsultantsDataTable
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Return (New UtilityTableAdapters.ActiveConsultantsTableAdapter).GetData()
        Catch ex As Exception
            LogError(Nothing, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting current consultant list", "Consultant", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetActiviserConsultantList(ByVal deviceId As String) As activiserDataSet.ConsultantDataTable
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Return (New ConsultantTableAdapter).GetDataForClient()
        Catch ex As Exception
            LogError(Nothing, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting current consultant list", "Consultant", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetConsultantDetails(ByVal deviceId As String, ByVal UserUid As Guid, ByVal ConsultantUid As Guid) As activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            If Not My.User.IsAuthenticated Then
                Throw New UnauthorizedAccessException("Anonymous access to restricted function denied")
            End If

            Dim result As New activiserDataSet
            result.EnforceConstraints = False
            Dim cta As New ConsultantTableAdapter
            If cta.FillByConsultantUid(result.Consultant, ConsultantUid) = 1 Then
                DataSchema.FillClientDataSet(result, DataSchema.SchemaClientMask.Console, deviceId, UserUid, result.Consultant.TableName, ConsultantUid)
                ' GetCustomData(deviceId, UserUid, result, NullDate, False)
                Return result
            Else
                Return Nothing
            End If

        Catch ex As Exception
            LogError(UserUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting client site details", "Consultant", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function


#End Region

#Region "Other Web Service Methods"

    <WebMethod()> Function TestEmail(ByVal deviceid As String, ByVal host As String, ByVal port As Integer, ByVal sender As String, ByVal recipients As String, ByVal subject As String, ByVal message As String) As String
        If Not My.User.IsAuthenticated Then Return My.Resources.RemoteAccessDenied
        If Not CheckDeviceId(deviceid) Then Return "Not authorised"
        Dim exceptionResult As Exception = Nothing
        JobEmail.SendMessage(host, port, sender, recipients, subject, message, exceptionResult)
        If exceptionResult IsNot Nothing Then
            Return exceptionResult.ToString
        Else
            Return "Message sent successfully"
        End If
    End Function

    Private Shared Sub SendEmail(ByVal emailQueue As Collections.Generic.Queue(Of Guid))
        Do While emailQueue.Count > 0
            Try
                JobEmail.SendEmailForJob(emailQueue.Dequeue)
            Catch ex As Exception
                LogError(WebServiceGuid, DateTime.UtcNow, WebServiceGuid.ToString, MethodBase.GetCurrentMethod.Name, "Error sending email", "<Internal eMail Queue>", WebServiceGuid, "", ex)
            End Try
        Loop
    End Sub

    ' Checks that the device-id of the PPC matches an id currently in the database
    ' This is used to prevent unauthorised PPCs accessing the web service methods, this
    ' includes events such as Pocket PCs being lost, the device-id can be de-registered
    ' from the database, and the Pocket PC will not be able to connect.
    Private _minClientVersion As Version = getMinClientVersion()

    Private Function getMinClientVersion() As Version
        Try
            Return New Version(My.Settings.MinimumClientVersion)
        Catch ex As Exception
            Return New Version(My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        End Try
    End Function

    <WebMethod(BufferResponse:=False, enableSession:=False, CacheDuration:=600)> _
    Public Function CheckDeviceId(ByVal DeviceIdString As String) As Boolean
        If Not MyBase.User.Identity.IsAuthenticated Then Return False ' enforce authenticated access.

        Try
            If String.IsNullOrEmpty(DeviceIdString) Then Return False
            Dim clientVersion As Version
            Dim deviceId As String

            'Dim result As Integer

            If DeviceIdString.Contains(";") Then
                Dim parts() As String = DeviceIdString.Split(";"c)
                clientVersion = New Version(parts(0))
                If clientVersion < _minClientVersion Then
                    Return False
                End If
                deviceId = parts(1)
            Else
                ' unknown version, assume previous version & fail
                Return False
                ' clientVersion = New Version(3, 4, 0)
            End If

            Try
                Dim deviceGuid, clientGuid As Guid

                If IsGuid(deviceId, deviceGuid) Then
                    Dim specialClientList As String = Utility.GetServerSetting("SpecialClients", String.Empty)
                    Dim specialClients As String() = specialClientList.Split(";"c)
                    For Each specialClient As String In specialClients
                        specialClient = specialClient.Trim
                        If IsGuid(specialClient, clientGuid) AndAlso deviceGuid.Equals(clientGuid) Then
                            Return True
                        End If
                    Next
                End If
            Catch ' don't care - fall through to normal device checks.

            End Try

            'Obtain license details from Server Profile
            Dim licenseKey As String = Utility.GetServerSetting("LicenseKey", "")
            Dim licensee As String = Utility.GetServerSetting("Licensee", "")

            If String.IsNullOrEmpty(licenseKey) Or String.IsNullOrEmpty(licensee) Then
                MyBase.Context.Response.StatusCode = Net.HttpStatusCode.Forbidden
                Return False ' unlicensed
            End If

            Dim licenseCode As New activiser.Licensing.LicenseInfo(licenseKey, licensee, WebServiceGuid)
            Dim dtCurrentDate As Date = Utility.GetServerTime()

            If dtCurrentDate > licenseCode.ExpiryDate Then
                LogError(WebServiceGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "License has expired", "ServerProfile", WebServiceGuid, licenseCode.ExpiryDate.ToString("u"), Nothing)
                MyBase.Context.Response.StatusCode = Net.HttpStatusCode.Forbidden
                'MyBase.Context.Response.Headers("WWW-Authenticate") = "A valid license is required to access this web service"
                Return False
            End If

            Dim top As Integer = licenseCode.Users
            Dim clients As New ClientDataTable
            Dim cdta As New ClientTableAdapter
            cdta.FillCurrent(clients, top)

            If clients.Count > top + 10 Then
                ' broken stored procedure!
                MyBase.Context.Response.StatusCode = Net.HttpStatusCode.Unauthorized
                'MyBase.Context.Response.Headers("WWW-Authenticate") = "A valid device id is required to access this web service"
                Return False
            End If

            top = clients.Count - 1
            For i As Integer = 0 To top
                Dim dr As ClientRow = clients(i)
                If dr.SystemId.ToLower = deviceId.ToLower Then
                    Return True
                End If
            Next

            'not found.
            Const vbNewLineAndTab As String = vbNewLine & vbTab
            Dim message As String = String.Format("Device Id Check Failed - unkown device or insufficient licenses.{0}Device ID={1}{0}License Count={2}{0}License Expiry={3}", _
                vbNewLineAndTab, deviceId, licenseCode.Users, licenseCode.ExpiryDate)
            LogError(WebServiceGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, message, "ServerProfile", WebServiceGuid, "", Nothing)
            'MyBase.Context.Response.StatusCode = Net.HttpStatusCode.Forbidden
            Return False

        Catch ex As Exception
            LogError(WebServiceGuid, DateTime.UtcNow, DeviceIdString, MethodBase.GetCurrentMethod.Name, "General error while checking Device Id", "ServerProfile", WebServiceGuid, "", ex)
            'MyBase.Context.Response.StatusCode = Net.HttpStatusCode.Forbidden
            Return False
        End Try

    End Function

    Private Shared Function UTCDateTime(ByVal Value As DateTime) As DateTime
        Return New DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, Value.Second, Value.Millisecond, DateTimeKind.Utc)
    End Function

    <WebMethod()> _
    Public Function GetServerTime(ByVal deviceId As String) As Date
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return Utility.GetServerTime()
        Catch ex As Exception
            LogError(WebServiceGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting server time", "Consultant", WebServiceGuid, "", ex)
            Return DateTime.UtcNow
        End Try
    End Function

    <WebMethod()> _
    Public Function GetServerVersion(ByVal deviceId As String) As String
        If Not Me.CheckDeviceId(deviceId) Then Return "-1"
        Try
            'Dim a As Assembly = Reflection.Assembly.GetExecutingAssembly()

            Dim fv As FileVersionInfo = FileVersionInfo.GetVersionInfo(IO.Path.Combine(My.Application.Info.DirectoryPath, My.Application.Info.AssemblyName) & ".dll")
            Dim v As New Version(fv.FileMajorPart, fv.FileMinorPart, fv.FileBuildPart, fv.FilePrivatePart)
            Return v.ToString(4) ' My.Application.Info.Version.ToString(4)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod(enableSession:=False)> _
    Public Function CheckConnection() As String
        Try
            If Not MyBase.Context.Request.IsLocal Then
                Return My.Resources.RemoteAccessDenied
            Else
                If Utility.PingServer() Then
                    Dim SQLServerTime As DateTime = Utility.GetServerTime()
                    Dim SQLUser As String = Utility.GetSqlLoginName()
                    Dim sc As New SqlClient.SqlConnection(My.Settings.activiserConnectionString)

                    Return String.Format(My.Resources.TestMessageFormat, SQLUser, SQLServerTime.ToString("u"), DateTime.Now.ToString("u"), sc.DataSource, sc.Database)
                Else
                    Return "Connection failed at " & DateTime.UtcNow.ToString("u")
                End If
            End If
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function

    <WebMethod()> _
    Public Function GetTerminology(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal ClientKey As Integer, ByVal LanguageId As Integer, ByVal ModifiedSince As Date) As LanguageDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Dim ds As New LanguageDataSet()
            Dim tacl As New LanguageDataSetTableAdapters.StringValueTableAdapter
            Dim langTa As New LanguageDataSetTableAdapters.LanguageTableAdapter

            If ModifiedSince = DateTime.MinValue Then
                ds.EnforceConstraints = False
                langTa.Fill(ds.Language)
                tacl.FillByClientKeyAndLanguage(ds.StringValue, ClientKey, LanguageId)
            Else
                ds.EnforceConstraints = False
                langTa.FillByModifiedDateTime(ds.Language, ModifiedSince)
                tacl.FillByModifiedDateTime(ds.StringValue, ClientKey, LanguageId, ModifiedSince)
            End If

            ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema

            Return ds

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting custom terminology", "Terminology", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod(BufferResponse:=False, enableSession:=False, CacheDuration:=600, Description:="Test authentication")> _
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

    '#Region "Other Privately used Methods"

    '    <DebuggerStepThrough()> _
    '    Private Shared Function ParseIdValue(ByVal Value As String) As Object
    '        If Value Is Nothing Then
    '            Return Nothing
    '        Else
    '            ' check for guid
    '            Dim format As Regex = New Regex( _
    '                    "^[A-Fa-f0-9]{32}$|" & _
    '                    "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" & _
    '                    "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$")
    '            Dim match As Match = format.Match(Value)
    '            If match.Success Then ' found a valid guid, return as guid
    '                Dim guidId As New Guid(Value)
    '                Return guidId
    '            Else ' see if it's an integer
    '                Dim integerId As Integer
    '                If Integer.TryParse(Value, integerId) Then
    '                    Return integerId
    '                Else
    '                    Return Value
    '                End If
    '            End If
    '        End If
    '    End Function

    '#End Region
End Class