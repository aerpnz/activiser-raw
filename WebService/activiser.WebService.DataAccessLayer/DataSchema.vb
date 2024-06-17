Option Strict Off

Imports System.Text
Imports activiser.DataSchemaTableAdapters

Partial Class DataSchema
    Private Const STR_Since As String = "@Since"

    'Private Shared _miscQueries As New DataSchemaTableAdapters.MiscellaneousQueries()
    'Private Shared _entityTableAdapter As New EntityTableAdapter()

    Partial Class AttributeDataTable
        Public Function FindByAttributeName(ByVal attributeName As String) As AttributeRow()
            Dim result As New List(Of AttributeRow)
            For Each ar As AttributeRow In Me.Rows
                If ar.AttributeName = attributeName Then
                    result.Add(ar)
                End If
            Next
            If result.Count = 0 Then
                result = Nothing
                Return Nothing
            Else
                Return result.ToArray()
            End If
        End Function
    End Class

    Public Enum AttributeType
        [Unknown] = -1
        [GuidPK] = 1
        [IntegerPK] = 2
        [BigIntPK] = 3
        [StringPK] = 4
        [GuidFK] = 11
        [IntegerFK] = 12
        [BigIntFK] = 13
        [StringFK] = 14
        [Guid] = 100
        [Integer] = 101
        [String] = 102
        [DateTime] = 103
        [Float] = 104
        [Boolean] = 105
        [Byte] = 106
        [Currency] = 107
        [BigInt] = 108
        [Decimal] = 109
        [Short] = 110
        [TimeStamp] = 111
        [DateOnly] = 112
        [Expression] = 113
        [IdentityInt] = 202
        [IdentityBigInt] = 203
        [EmailAddress] = 1010
        [WebAddress] = 1011
        [Address] = 1012
        [PhoneNumber] = 1013
        [Password] = 1014
    End Enum

    <Flags()> _
    Public Enum SchemaClientMask
        None = 0
        MobileClientPublic = 1
        MobileClientPrivate = 2
        ConsolePublic = 4
        ConsolePrivate = 8
        WindowsClientPublic = 16
        WindowsClientPrivate = 32
        OutlookPublic = 64
        OutlookPrivate = 128

        Mobile = MobileClientPublic Or MobileClientPrivate
        Console = ConsolePublic Or ConsolePrivate
        WindowsClient = WindowsClientPublic Or WindowsClientPrivate
        Outlook = OutlookPublic Or OutlookPrivate

        ConsoleOrOutlook = Console Or Outlook

        ConsoleOrMobilePublic = ConsolePublic Or MobileClientPublic
        ConsoleOrMobilePrivate = ConsolePrivate Or MobileClientPrivate

        DesktopPublic = ConsolePublic Or OutlookPublic Or WindowsClientPublic
        DesktopPrivate = ConsolePrivate Or OutlookPrivate Or WindowsClientPrivate
        Desktop = DesktopPublic Or DesktopPrivate

        ClientPublic = MobileClientPublic Or WindowsClientPublic
        ClientPrivate = MobileClientPrivate Or WindowsClientPrivate
        Client = ClientPublic Or ClientPrivate

        All = &HFFFFFFFF
    End Enum

    Private Shared _rowStatesWeCareAbout As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Unchanged

    Public Shared Function EntityExists(ByVal entityName As String) As Boolean
        Return CBool((New EntityTableAdapter())._EntityExists(entityName))
    End Function

    Public Shared Function LastSchemaChange() As DateTime
        Return CDate((New DataSchemaTableAdapters.MiscellaneousQueries())._LastSchemaChange().Value)
    End Function

    Private Shared Function sprocExists(ByVal procName As String) As Boolean
        Return CBool((New DataSchemaTableAdapters.MiscellaneousQueries())._findStoredProcedure(procName) <> 0)
    End Function

    'TODO: Add Schema 'Cache'

    ''' <summary>
    ''' Returns an DataSchema filled with the current schema configuration
    ''' </summary>
    ''' <returns>DataSchema</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSchema() As DataSchema

        Dim result As New DataSchema()

        Using _
            eta As New EntityTableAdapter, _
            ata As New AttributeTableAdapter

            eta.Fill(result.Entity)
            ata.Fill(result.Attribute)
        End Using

        Return result
    End Function


    ''' <summary>
    ''' Returns an DataSchema filled with the current schema configuration
    ''' </summary>
    ''' <returns>DataSchema</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSchema(ByVal clientMask As Int32) As DataSchema

        Dim result As New DataSchema()
        result.EnforceConstraints = False

        Using _
            eta As New EntityTableAdapter, _
            ata As New AttributeTableAdapter

            eta.FillByClientMask(result.Entity, clientMask)
            If result.Entity.Rows.Count = 0 Then
                result.Dispose()
                Return Nothing
            End If

            ' note: if the schema definition is corrupt, this could result in orphaned attribute rows.
            ' however, this should be trapped by the stored procedure that fills the data.
            ata.FillByClientMask(result.Attribute, clientMask)

            'Dim victims As New List(Of AttributeRow)
            'For Each ar As AttributeRow In result.Attribute
            '    If result.Entity.FindByEntityId(ar.EntityId) Is Nothing Then
            '        victims.Add(ar)
            '    End If
            'Next

            'For Each ar As AttributeRow In victims
            '    result.Attribute.RemoveAttributeRow(ar)
            'Next
        End Using

        Return result
    End Function

    ''' <summary>
    ''' Returns an DataSchema filled with the current schema configuration
    ''' </summary>
    ''' <returns>DataSchema</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSchema(ByVal entityName As String) As DataSchema

        Dim result As New DataSchema()

        Using _
            eta As New EntityTableAdapter, _
            ata As New AttributeTableAdapter

            eta.FillByEntityName(result.Entity, entityName)
            If result.Entity.Rows.Count <> 1 Then  ' should only be one!
                result.Dispose()
                Return Nothing
            End If

            ata.FillByEntityId(result.Attribute, result.Entity(0).EntityId)

        End Using

        Return result
    End Function

    Private Shared Function GetAttributeDataType(ByVal attribute As AttributeRow) As Type
        Dim dataType As Type
        Select Case attribute.AttributeTypeCode
            Case AttributeType.Boolean
                dataType = GetType(Boolean)
            Case AttributeType.Byte
                dataType = GetType(Byte)
            Case AttributeType.Currency
                dataType = GetType(Decimal)
            Case AttributeType.DateTime, AttributeType.DateOnly
                dataType = GetType(DateTime)
            Case _
                AttributeType.Address, _
                AttributeType.EmailAddress, _
                AttributeType.String, _
                AttributeType.PhoneNumber, _
                AttributeType.Password, _
                AttributeType.WebAddress
                dataType = GetType(String)
            Case AttributeType.Float
                dataType = GetType(Double)
            Case AttributeType.Guid, AttributeType.GuidPK, AttributeType.GuidFK
                dataType = GetType(Guid)
            Case AttributeType.Integer, AttributeType.IntegerPK, AttributeType.IntegerFK
                dataType = GetType(Int32)
            Case AttributeType.BigInt, AttributeType.BigIntPK, AttributeType.BigIntFK
                dataType = GetType(Int64)
            Case Else
                Throw New AttributeTypeInvalidException(attribute.AttributeName, attribute.AttributeTypeCode)
        End Select
        Return dataType
    End Function

    Private Shared Function GetAttributeSqlDataType(ByVal typeCode As Integer) As SqlDbType
        Select Case typeCode
            Case AttributeType.Boolean
                Return SqlDbType.Bit
            Case AttributeType.Byte
                Return SqlDbType.TinyInt
            Case AttributeType.Currency
                Return SqlDbType.Decimal
            Case AttributeType.DateTime, AttributeType.DateOnly
                Return SqlDbType.DateTime
            Case _
                AttributeType.Address, _
                AttributeType.EmailAddress, _
                AttributeType.String, _
                AttributeType.PhoneNumber, _
                AttributeType.Password, _
                AttributeType.WebAddress
                Return SqlDbType.NVarChar
            Case AttributeType.Float
                Return SqlDbType.Float
            Case AttributeType.Guid, AttributeType.GuidPK, AttributeType.GuidFK
                Return SqlDbType.UniqueIdentifier
            Case AttributeType.Integer, AttributeType.IntegerPK, AttributeType.IntegerFK
                Return SqlDbType.Int
            Case AttributeType.BigInt, AttributeType.BigIntPK, AttributeType.BigIntFK
                Return SqlDbType.BigInt
            Case Else
                Throw New AttributeTypeInvalidException(String.Format("{0} is not a valid type code", typeCode))
        End Select
        Return SqlDbType.Variant
    End Function


    Private Shared Sub ExpandClientSchema(Of T As DataSet)(ByVal result As T, ByVal ds As DataSchema)
        For Each entity As EntityRow In ds.Entity
            If Not result.Tables.Contains(entity.EntityName) Then
                result.Tables.Add(entity.EntityName)
            End If

            Using dt As DataTable = result.Tables(entity.EntityName)
                Dim PkList As New List(Of DataColumn)
                For Each attribute As AttributeRow In entity.GetAttributeRows
                    If Not dt.Columns.Contains(attribute.AttributeName) Then
                        Dim dc As DataColumn = dt.Columns.Add(attribute.AttributeName, GetAttributeDataType(attribute))
                        If attribute.IsPrimaryKeyAttribute Then PkList.Add(dc)
                    End If
                Next
                If dt.PrimaryKey.Length = 0 Then
                    dt.PrimaryKey = PkList.ToArray()
                End If
            End Using
        Next
    End Sub

    ''' <summary>
    ''' Returns a activiserDataSet object that is empty, but with the schema expanded to
    ''' include all the custom entities and attributes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetClientSchema() As activiserDataSet
        Dim result As New activiserDataSet()

        Using ds As DataSchema = GetDataSchema()
            ExpandClientSchema(result, ds)
        End Using

        result.SchemaSerializationMode = Data.SchemaSerializationMode.IncludeSchema
        Return result
    End Function

    Public Shared Function GetClientSchema(ByVal clientMask As Integer) As activiserDataSet
        Dim result As New activiserDataSet()

        Using ds As DataSchema = GetDataSchema(clientMask)
            ExpandClientSchema(result, ds)
        End Using

        result.SchemaSerializationMode = Data.SchemaSerializationMode.IncludeSchema
        Return result
    End Function

    Public Shared Function GetClientDataSet(ByVal deviceId As String) As activiserDataSet
        Dim result As activiserDataSet = GetClientSchema()

        FillClientDataSet(result, DataSchema.SchemaClientMask.All, deviceId, Nothing)

        result.SchemaSerializationMode = Data.SchemaSerializationMode.IncludeSchema
        Return result
    End Function

    Public Shared Function GetClientDataSet(ByVal clientMask As Int32, ByVal deviceId As String, ByVal since? As Date) As activiserDataSet
        Dim result As activiserDataSet = GetClientSchema()

        FillClientDataSet(result, clientMask, deviceId, since)

        result.SchemaSerializationMode = Data.SchemaSerializationMode.IncludeSchema
        Return result
    End Function


    Public Shared Function GetClientDataSet(ByVal clientMask As Int32, ByVal consultantUid As Guid, ByVal deviceId As String, ByVal since? As Date) As activiserDataSet
        Dim result As activiserDataSet = GetClientSchema()

        FillClientDataSet(result, clientMask, consultantUid, deviceId, since)

        result.SchemaSerializationMode = Data.SchemaSerializationMode.IncludeSchema
        Return result
    End Function

#Region "*ForConsultant Queries"
    Private Const sqlTypeUniqueidentifier As String = "uniqueidentifier"
    Private Const sqlTypeInt As String = "int"
    Private Const sqlTypeDateTime As String = "datetime"
    Private Const sqlTypeNVarChar As String = "nvarchar"
    Private Const consultantParmName As String = "@consultantUid"
    Private Const sinceParmName As String = "@since"
    Private Const callingClientIdParmName As String = "@callingClientId"
    Private Const callingClientTypeParmName As String = "@callingClientType"
    Private Const callingUserIdParmName As String = "@callingUserId"

    Private Shared Function isConsultantParm(ByVal pr As StoredProcedureParameterRow) As Boolean
        Return String.Compare(pr.ParameterType, sqlTypeUniqueidentifier, False) = 0 AndAlso _
            String.Compare(pr.ParameterName, consultantParmName, True) = 0
    End Function

    Private Shared Function isSinceParm(ByVal pr As StoredProcedureParameterRow) As Boolean
        Return String.Compare(pr.ParameterType, sqlTypeDateTime, False) = 0 AndAlso _
            String.Compare(pr.ParameterName, sinceParmName, True) = 0
    End Function

    Private Shared Function isClientIdParm(ByVal pr As StoredProcedureParameterRow) As Boolean
        ' note: 256 = 128 characters @ two bytes per character...
        Return String.Compare(pr.ParameterType, sqlTypeNVarChar, False) = 0 AndAlso (pr.MaxLength = 256) AndAlso _
            String.Compare(pr.ParameterName, callingClientIdParmName, True) = 0
    End Function

    Private Shared Function isCallerIdParm(ByVal pr As StoredProcedureParameterRow) As Boolean
        Return CType(pr.DataTypeId, SqlSysType) = SqlSysType.uniqueidentifier AndAlso _
            String.Compare(pr.ParameterName, callingUserIdParmName, True) = 0
    End Function

    Private Shared Function isClientTypeParm(ByVal pr As StoredProcedureParameterRow) As Boolean
        Return String.Compare(pr.ParameterType, sqlTypeInt, False) = 0 AndAlso _
            String.Compare(pr.ParameterName, callingClientTypeParmName, True) = 0
    End Function

    Private Shared Function GetForConsultantSproc(ByVal clientMask As Int32, ByVal deviceId As String, ByVal consultantUid As Guid, ByVal since As DateTime?, ByVal entity As EntityRow, ByVal sqlParameters As Dictionary(Of String, Object), ByVal sprocParmTA As StoredProcedureParameterTableAdapter) As String
        Using sprocArgs As New StoredProcedureParameterDataTable()
            Dim sprocName As String
            sprocName = String.Format("{0}_SelectForConsultant", entity.EntityName)
            sprocParmTA.Fill(sprocArgs, sprocName)

            If sprocArgs.Count = 5 Then ' need exactly five !
                Dim foundArgs As Integer = 0
                For Each pr As StoredProcedureParameterRow In sprocArgs
                    If isConsultantParm(pr) Then foundArgs += 1
                    If isSinceParm(pr) Then foundArgs += 1
                    If isClientIdParm(pr) Then foundArgs += 1
                    If isClientTypeParm(pr) Then foundArgs += 1
                    If isCallerIdParm(pr) Then foundArgs += 1
                Next
                If (foundArgs = 5) Then ' found them all.
                    sqlParameters.Add(callingClientTypeParmName, clientMask)
                    sqlParameters.Add(callingClientIdParmName, deviceId)
                    sqlParameters.Add(callingUserIdParmName, consultantUid)
                    sqlParameters.Add(consultantParmName, consultantUid)
                    If since.HasValue Then
                        sqlParameters.Add(sinceParmName, since.Value)
                    Else
                        sqlParameters.Add(sinceParmName, DBNull.Value)
                    End If
                    Return "activiser." & sprocName
                End If
            End If
        End Using
        Return Nothing
    End Function

    Private Shared Function GetSinceSproc(ByVal clientMask As Int32, ByVal deviceId As String, ByVal consultantUid As Guid, ByVal since As DateTime?, ByVal entity As EntityRow, ByVal sqlParameters As Dictionary(Of String, Object), ByVal sprocParmTA As StoredProcedureParameterTableAdapter) As String
        Using sprocArgs As New StoredProcedureParameterDataTable()
            Dim sprocName As String = String.Format("{0}_SelectModifiedSince", entity.EntityName)
            sprocParmTA.Fill(sprocArgs, sprocName)
            If sprocArgs.Count = 4 Then ' need exact number !
                Dim foundArgs As Integer = 0
                For Each pr As StoredProcedureParameterRow In sprocArgs
                    If isSinceParm(pr) Then foundArgs += 1
                    If isClientIdParm(pr) Then foundArgs += 1
                    If isClientTypeParm(pr) Then foundArgs += 1
                    If isCallerIdParm(pr) Then foundArgs += 1
                Next
                If (foundArgs = 4) Then ' found them all.
                    sqlParameters.Add(callingClientTypeParmName, clientMask)
                    sqlParameters.Add(callingClientIdParmName, deviceId)
                    sqlParameters.Add(callingUserIdParmName, consultantUid)
                    If since.HasValue Then
                        sqlParameters.Add(sinceParmName, since.Value)
                    Else
                        sqlParameters.Add(sinceParmName, DBNull.Value)
                    End If
                    Return "activiser." & sprocName
                End If
            End If
        End Using
        Return Nothing
    End Function

    Private Shared Function GetSelectSproc(ByVal clientMask As Int32, ByVal deviceId As String, ByVal consultantUid As Guid, ByVal entity As EntityRow, ByVal sqlParameters As Dictionary(Of String, Object), ByVal sprocParmTA As StoredProcedureParameterTableAdapter) As String
        Using sprocArgs As New StoredProcedureParameterDataTable()
            Dim sprocName As String = String.Format("{0}_Select", entity.EntityName)
            sprocParmTA.Fill(sprocArgs, sprocName)
            If sprocArgs.Count = 3 Then ' need exactly 3 !
                Dim foundArgs As Integer = 0
                For Each pr As StoredProcedureParameterRow In sprocArgs
                    If isClientIdParm(pr) Then
                        foundArgs += 1
                    ElseIf isClientTypeParm(pr) Then
                        foundArgs += 1
                    ElseIf isCallerIdParm(pr) Then
                        foundArgs += 1
                    End If
                Next
                If (foundArgs = 3) Then ' found them all.
                    sqlParameters.Add(callingClientTypeParmName, clientMask)
                    sqlParameters.Add(callingClientIdParmName, deviceId)
                    sqlParameters.Add(callingUserIdParmName, consultantUid)
                    Return "activiser." & sprocName
                End If
            End If
        End Using
        Return Nothing
    End Function

    Private Shared Function GetSelectItemSproc(ByVal clientMask As Int32, ByVal deviceId As String, ByVal consultantUid As Guid, ByVal entity As EntityRow, ByVal keyValue As Object, ByVal sqlParameters As Dictionary(Of String, Object), ByVal sprocParmTA As StoredProcedureParameterTableAdapter) As String
        Using sprocArgs As New StoredProcedureParameterDataTable()
            Dim sprocName As String = String.Format("{0}_SelectItem", entity.EntityName)
            Dim keyParmName As String = String.Format("@{0}", entity.PrimaryKeyAttributeName)
            sprocParmTA.Fill(sprocArgs, sprocName)
            If sprocArgs.Count = 4 Then ' need exact number!
                Dim foundArgs As Integer = 0
                For Each pr As StoredProcedureParameterRow In sprocArgs
                    If isClientIdParm(pr) Then
                        foundArgs += 1
                    ElseIf isClientTypeParm(pr) Then
                        foundArgs += 1
                    ElseIf isCallerIdParm(pr) Then
                        foundArgs += 1
                    ElseIf pr.ParameterName = keyParmName Then
                        foundArgs += 1
                    End If
                Next
                If (foundArgs = 4) Then ' found them all.
                    sqlParameters.Add(callingClientTypeParmName, clientMask)
                    sqlParameters.Add(callingClientIdParmName, deviceId)
                    sqlParameters.Add(callingUserIdParmName, consultantUid)
                    sqlParameters.Add(keyParmName, keyValue)
                    Return "activiser." & sprocName
                End If
            End If
        End Using
        Return Nothing
    End Function

    Private Shared Function GetAdHocSelectQuery(ByVal entity As EntityRow, ByVal sqlParameters As Dictionary(Of String, Object), ByVal dt As DataTable, ByVal keyValue As Object, ByVal since As DateTime?) As String
        Dim columnList As New List(Of String)
        For Each attribute As AttributeRow In entity.GetAttributeRows
            If dt.Columns.Contains(attribute.AttributeName) Then
                columnList.Add(attribute.AttributeName)
            End If
        Next

        Dim selectColumns As String = String.Join(",", columnList.ToArray())
        If keyValue IsNot Nothing Then
            sqlParameters.Add(String.Format("@{0}", entity.PrimaryKeyAttributeName), keyValue)
            Return String.Format("SELECT {0} FROM {1} WHERE {2} >= @{2}", selectColumns, entity.EntityName, entity.PrimaryKeyAttributeName)
        ElseIf since.HasValue Then
            sqlParameters.Add(STR_Since, since)
            Return String.Format("SELECT {0} FROM {1} WHERE ModifiedDateTime >= @Since", selectColumns, entity.EntityName)
        Else
            Return String.Format("SELECT {0} FROM {1}", selectColumns, entity.EntityName)
        End If
    End Function
#End Region

    ''' <summary>
    ''' Fills a activiserDataSet for a consultant, optionally date filtered.
    ''' </summary>
    ''' <param name="target">existing activiserDataSet to be filled</param>
    ''' <param name="clientMask">client software mask; valid values are described in the metadata.SchemaClients table.</param>
    ''' <param name="consultantUid">UniqueIdentifier for the consultant that data is being fetched for.</param>
    ''' <param name="since">Optional date value that specifies that you only want records modified after the supplied date.</param>
    ''' <returns>Number of rows added to the dataset.</returns>
    ''' <remarks>
    ''' This functions requires the presence of a stored procedure that can be used to perform the fetch for each table
    ''' in the dataset.
    ''' 
    ''' <list type="bullet">
    '''     <listheader>
    '''         <description>For a non-date-filtered fill, the stored procedure can be one of:</description>
    '''     </listheader>
    '''     <item><description>{entityName}_SelectForConsultant</description></item>
    '''     <item><description>{entityName}_Select</description></item>
    ''' </list>
    ''' 
    ''' <list type="bullet">
    '''     <listheader>
    '''         <description>For a date-filtered fill, the stored procedure can be one of:</description>
    '''     </listheader>
    '''     <item><description>{entityName}_SelectForConsultant</description></item>
    '''     <item><description>{entityName}_SelectModifiedSince</description></item>
    ''' </list>
    ''' 
    ''' <list type="table">
    '''     <listheader>
    '''         <description>{entityName}_Select must have these parameters (in any order, but the presented order is recommended):</description>
    '''     </listheader>
    '''     <item><term>@clientMask int</term><description>client mask to be applied to result set</description></item>
    '''     <item><term>@deviceId nvarchar(128)</term><description>device id of caller (can be used for access control and/or auditing)</description></item>
    ''' </list>
    ''' 
    ''' <list type="table">
    '''     <listheader>
    '''         <description>{entityName}_SelectModifiedSince must have these parameters (in any order, but the presented order is recommended):</description>
    '''     </listheader>
    '''     <item><term>@clientMask int</term><description>client mask to be applied to result set</description></item>
    '''     <item><term>@deviceId nvarchar(128)</term><description>device id of caller (can be used for access control and/or auditing)</description></item>
    '''     <item><term>@since datetime (optional)</term><description></description>fetch records modified since this date. If this value is null, then the effect should be the same as calling {EntityName}_Select</item>
    ''' </list>
    ''' 
    ''' <list type="table">
    '''     <listheader>
    '''         <description>{entityName}_SelectForConsultant must have these parameters (in any order, but the presented order is recommended):</description>
    '''     </listheader>
    '''     <item><term>@clientMask int</term><description>client mask to be applied to result set</description></item>
    '''     <item><term>@deviceId nvarchar(128)</term><description>device id of caller (can be used for access control and/or auditing)</description></item>
    '''     <item><term>@consultantUid uniqueidentifier</term><description>identifier of calling consultant</description></item>
    '''     <item><term>@since datetime (optional)</term><description></description>fetch records modified since this date</item>
    ''' </list>
    ''' </remarks>
    Public Shared Function FillClientDataSet(ByVal target As activiserDataSet, ByVal clientMask As Int32, ByVal consultantUid As Guid, ByVal deviceId As String, ByVal since As DateTime?) As Integer
        Dim result As Integer = 0
        Using ds As DataSchema = GetDataSchema(clientMask)

            ExpandClientSchema(target, ds)
            ' note: the fillsequence is important, get it wrong and there will be constraint violation issues.
            For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
                If String.IsNullOrEmpty(entity.EntityName) Then Continue For ' sanity check!
                If Not target.Tables.Contains(entity.EntityName) Then Continue For ' skip tables where clientmask doesn't fit.

                result += FillDataTableForEntity(clientMask, deviceId, entity, target.Tables(entity.EntityName), consultantUid, since)
            Next
        End Using
        Return result
    End Function

    Private Shared Function FillDataTableForEntity(ByVal clientMask As Int32, ByVal deviceId As String, ByVal entity As EntityRow, ByVal dt As DataTable, ByVal consultantUid As Guid, ByVal since As DateTime?) As Integer
        Dim usingSproc As Boolean = True
        Dim sql As String = String.Empty
        Dim sqlParameters As New Dictionary(Of String, Object)

        ' find stored procedure to fill data table.
        Dim sprocParmTA As New StoredProcedureParameterTableAdapter()
        sprocParmTA.ClearBeforeFill = True

        If consultantUid <> Guid.Empty Then
            sql = GetForConsultantSproc(clientMask, deviceId, consultantUid, since, entity, sqlParameters, sprocParmTA)
        ElseIf since.HasValue Then
            sql = GetSinceSproc(clientMask, deviceId, consultantUid, since, entity, sqlParameters, sprocParmTA)
        Else
            sql = GetSelectSproc(clientMask, deviceId, consultantUid, entity, sqlParameters, sprocParmTA)
        End If

        If String.IsNullOrEmpty(sql) Then ' still no luck, try building SQL instead.
            sql = GetAdHocSelectQuery(entity, sqlParameters, dt, Nothing, since)
            usingSproc = False
        End If

        Using cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
            cmd.SelectCommand.CommandType = If(usingSproc, CommandType.StoredProcedure, CommandType.Text)
            If sqlParameters.Count > 0 Then
                For Each nv As KeyValuePair(Of String, Object) In sqlParameters
                    cmd.SelectCommand.Parameters.AddWithValue(nv.Key, nv.Value)
                Next
            End If
            cmd.AcceptChangesDuringFill = True
            cmd.FillLoadOption = LoadOption.OverwriteChanges
            cmd.SelectCommand.CommandTimeout = My.Settings.CommandTimeout * 1000

            Return cmd.Fill(dt)
        End Using
        Return 0
    End Function

    Private Shared Function FillDataTableForEntity(ByVal clientMask As Int32, ByVal deviceId As String, ByVal consultantUid As Guid, ByVal entity As EntityRow, ByVal dt As DataTable, ByVal keyValue As Object) As Integer
        Dim usingSproc As Boolean = True
        Dim sql As String = String.Empty
        Dim sqlParameters As New Dictionary(Of String, Object)

        ' find stored procedure to fill data table.
        Dim sprocParmTA As New StoredProcedureParameterTableAdapter()
        sprocParmTA.ClearBeforeFill = True

        sql = GetSelectItemSproc(clientMask, deviceId, consultantUid, entity, keyValue, sqlParameters, sprocParmTA)

        If String.IsNullOrEmpty(sql) Then ' no luck, try building SQL instead.
            sql = GetAdHocSelectQuery(entity, sqlParameters, dt, keyValue, Nothing)
            usingSproc = False
        End If

        Using cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
            cmd.SelectCommand.CommandType = If(usingSproc, CommandType.StoredProcedure, CommandType.Text)
            If sqlParameters.Count > 0 Then
                For Each nv As KeyValuePair(Of String, Object) In sqlParameters
                    cmd.SelectCommand.Parameters.AddWithValue(nv.Key, nv.Value)
                Next
            End If
            cmd.AcceptChangesDuringFill = True
            cmd.FillLoadOption = LoadOption.OverwriteChanges
            cmd.SelectCommand.CommandTimeout = My.Settings.CommandTimeout * 1000

            Return cmd.Fill(dt)
        End Using
        Return 0
    End Function

    Public Shared Function FillClientDataSet(ByVal target As activiserDataSet, ByVal clientMask As Int32, ByVal deviceId As String, ByVal since As DateTime?) As Integer
        Dim result As Integer = 0
        Using _
            ds As DataSchema = GetDataSchema(clientMask)

            ExpandClientSchema(target, ds)
            ' note: the fillsequence is important, get it wrong and there will be constraint violation issues.
            For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
                If String.IsNullOrEmpty(entity.EntityName) Then Continue For ' sanity check!
                If Not target.Tables.Contains(entity.EntityName) Then Continue For ' skip tables where clientmask doesn't fit.
                result += FillDataTableForEntity(clientMask, deviceId, entity, target.Tables(entity.EntityName), Guid.Empty, since)
            Next
        End Using
        Return result
    End Function

    Public Shared Function FillClientDataSet(ByVal target As activiserDataSet, ByVal clientMask As Int32, ByVal deviceId As String, ByVal since As DateTime?, ByVal EntityName As String) As Integer
        Dim result As Integer = 0
        Using ds As DataSchema = GetDataSchema(clientMask)
            If Not target.Tables.Contains(EntityName) Then Return result

            For Each entity As EntityRow In ds.Entity
                If entity.EntityName = EntityName Then
                    result += FillDataTableForEntity(clientMask, deviceId, entity, target.Tables(entity.EntityName), Guid.Empty, since)
                End If
            Next
        End Using
        Return result
    End Function

    'TODO: Generate stored procedures for 'Parent'-filtered queries.
    Public Shared Function FillEntityDetail(ByVal target As activiserDataSet, ByVal parentEntityName As String, ByVal parentEntityKey As Object, ByVal ds As DataSchema, ByVal clientMask As Integer, ByVal deviceId As String) As Integer
        Dim result As Integer = 0
        For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
            If entity.IsParentEntityNameNull Then Continue For
            If Not entity.ParentEntityName = parentEntityName Then Continue For
            If Not target.Tables.Contains(entity.EntityName) Then Continue For ' skip tables where clientmask doesn't fit.
            Using dt As DataTable = target.Tables(entity.EntityName)
                ' result += FillDataTableForEntity(clientMask, deviceId, Nothing, entity, dt)

                Dim columnList As New List(Of String)
                For Each attribute As AttributeRow In entity.GetAttributeRows
                    If dt.Columns.Contains(attribute.AttributeName) Then
                        columnList.Add(attribute.AttributeName)
                    End If
                Next

                Dim sql As String = _
                    String.Format("SELECT {0} FROM {1} WHERE {2}=@parentKey", _
                        String.Join(",", columnList.ToArray()), _
                        entity.EntityName, entity.ParentAttributeName)

                Using cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
                    cmd.SelectCommand.Parameters.AddWithValue("@parentKey", parentEntityKey)
                    cmd.SelectCommand.CommandTimeout = My.Settings.CommandTimeout * 1000
                    cmd.FillLoadOption = LoadOption.OverwriteChanges
                    result += cmd.Fill(dt)
                End Using
            End Using
        Next
        Return result
    End Function

    Public Shared Function FillClientDataSet(ByVal target As activiserDataSet, ByVal clientMask As Int32, ByVal deviceId As String, ByVal consultantuid As Guid, ByVal entityName As String, ByVal entityKey As Object) As Integer
        Dim result As Integer = 0
        Using _
            ds As DataSchema = GetDataSchema(clientMask)

            Dim entity As EntityRow
            For Each entity In ds.Entity.Rows
                If entity.EntityName = entityName Then
                    If Not target.Tables.Contains(entity.EntityName) Then
                        Throw New ArgumentOutOfRangeException(entityName)
                    End If
                    result += FillDataTableForEntity(clientMask, deviceId, consultantuid, entity, target.Tables(entity.EntityName), entityKey)
                    result += FillEntityDetail(target, entityName, entityKey, ds, clientMask, deviceId)
                    Return result
                End If
            Next
        End Using
        Return -1
    End Function

    'TODO: Entity/Parent/Consultant versions of this.
    Public Shared Function FillClientDataSet(ByVal target As activiserDataSet, ByVal clientMask As Int32, ByVal deviceId As String) As Integer
        Return FillClientDataSet(target, clientMask, deviceId, Nothing)
    End Function

    Private Shared Function GetUpSertSelectQuery(ByVal entity As EntityRow) As String
        Dim columnList As New List(Of String)
        For Each attribute As AttributeRow In entity.GetAttributeRows
            columnList.Add(attribute.AttributeName)
        Next

        Dim selectColumns As String = String.Join(",", columnList.ToArray())
        Return String.Format("SELECT {0} FROM {1}", selectColumns, entity.EntityName)
    End Function

    ''' <summary>
    ''' Try to find a stored procedure for inserting records. If it fails, we'll fall back to ad-hoc sql.
    ''' </summary>
    ''' <param name="adapter"></param>
    ''' <param name="entity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetInsertSProc(ByVal adapter As SqlClient.SqlDataAdapter, ByVal tx As SqlClient.SqlTransaction, ByVal entity As EntityRow, ByVal dt As DataTable) As Boolean
        Dim sprocName As String = String.Format("{0}_Insert", entity.EntityName)
        If Not sprocExists(sprocName) Then Return False
        sprocName = "activiser." & sprocName

        Dim sProcArgTA As New StoredProcedureParameterTableAdapter()
        sProcArgTA.Connection = adapter.SelectCommand.Connection
        'sProcArgTA.Transaction = tx

        Dim sProcArgs As StoredProcedureParameterDataTable = sProcArgTA.GetData(sprocName)
        If Not sProcArgs Is Nothing Then
            Dim result As SqlClient.SqlCommand

            If adapter.InsertCommand Is Nothing Then
                adapter.InsertCommand = New SqlClient.SqlCommand()
            End If

            result = adapter.InsertCommand
            result.CommandText = sprocName
            result.CommandType = CommandType.StoredProcedure
            result.Connection = adapter.SelectCommand.Connection
            result.CommandTimeout = My.Settings.CommandTimeout * 1000
            ' result.Transaction = tx
            SqlClient.SqlCommandBuilder.DeriveParameters(result)
            For Each a As AttributeRow In entity.GetAttributeRows
                If Not dt.Columns.Contains(a.AttributeName) Then Continue For ' data set may have incomplete column set
                Dim sParmName As String = "@" & a.AttributeName
                If result.Parameters.Contains(sParmName) Then
                    result.Parameters(sParmName).SourceColumn = a.AttributeName
                End If
            Next

            Return True
        End If

        Return False
    End Function

    Private Shared Function GetUpdateSProc(ByVal adapter As SqlClient.SqlDataAdapter, ByVal tx As SqlClient.SqlTransaction, ByVal entity As EntityRow, ByVal dt As DataTable) As Boolean
        Dim sprocName As String = String.Format("{0}_Update", entity.EntityName)
        If Not sprocExists(sprocName) Then Return False
        sprocName = "activiser." & sprocName

        Dim sProcArgTA As New StoredProcedureParameterTableAdapter()
        sProcArgTA.Connection = adapter.SelectCommand.Connection
        sProcArgTA.Transaction = tx

        Dim sProcArgs As StoredProcedureParameterDataTable = sProcArgTA.GetData(sprocName)
        If Not sProcArgs Is Nothing Then
            Dim result As SqlClient.SqlCommand

            If adapter.UpdateCommand Is Nothing Then
                adapter.UpdateCommand = New SqlClient.SqlCommand()
            End If

            result = adapter.UpdateCommand
            result.CommandText = sprocName
            result.CommandType = CommandType.StoredProcedure
            result.Connection = adapter.SelectCommand.Connection
            result.CommandTimeout = My.Settings.CommandTimeout * 1000
            result.Transaction = tx
            SqlClient.SqlCommandBuilder.DeriveParameters(result)
            For Each a As AttributeRow In entity.GetAttributeRows
                If Not dt.Columns.Contains(a.AttributeName) Then Continue For ' data set may have incomplete column set
                Dim sParmName As String = "@" & a.AttributeName
                If result.Parameters.Contains(sParmName) Then
                    result.Parameters(sParmName).SourceColumn = a.AttributeName
                End If
            Next

            Return True
        End If

        Return False
    End Function

    Private Shared Function GetUpSertSProc(ByVal adapter As SqlClient.SqlDataAdapter, ByVal tx As SqlClient.SqlTransaction, ByVal entity As EntityRow, ByVal dt As DataTable) As Boolean
        Dim sprocName As String = String.Format("{0}_UpSert", entity.EntityName)
        If Not sprocExists(sprocName) Then Return False
        sprocName = "activiser." & sprocName

        Dim sProcArgTA As New StoredProcedureParameterTableAdapter()
        sProcArgTA.Connection = adapter.SelectCommand.Connection
        sProcArgTA.Transaction = tx

        Dim sProcArgs As StoredProcedureParameterDataTable = sProcArgTA.GetData(sprocName)
        If Not sProcArgs Is Nothing Then
            Dim result As SqlClient.SqlCommand

            If adapter.UpdateCommand Is Nothing AndAlso adapter.InsertCommand Is Nothing Then
                result = New SqlClient.SqlCommand()
                adapter.InsertCommand = result
                adapter.UpdateCommand = result
            ElseIf adapter.UpdateCommand IsNot Nothing Then
                result = adapter.UpdateCommand
                adapter.InsertCommand = result
            Else
                result = adapter.InsertCommand
                adapter.UpdateCommand = result
            End If

            result.CommandText = sprocName
            result.CommandType = CommandType.StoredProcedure
            result.Connection = adapter.SelectCommand.Connection
            result.CommandTimeout = My.Settings.CommandTimeout * 1000
            result.Transaction = tx
            SqlClient.SqlCommandBuilder.DeriveParameters(result)
            For Each a As AttributeRow In entity.GetAttributeRows
                If Not dt.Columns.Contains(a.AttributeName) Then Continue For ' data set may have incomplete column set
                Dim sParmName As String = "@" & a.AttributeName
                If result.Parameters.Contains(sParmName) Then
                    result.Parameters(sParmName).SourceColumn = a.AttributeName
                End If
            Next

            Return True
        End If

        Return False
    End Function


    Private Shared Function GetDeleteSProc(ByVal adapter As SqlClient.SqlDataAdapter, ByVal tx As SqlClient.SqlTransaction, ByVal entity As EntityRow, ByVal dt As DataTable) As Boolean
        Dim sprocName As String = String.Format("{0}_Delete", entity.EntityName)
        If Not sprocExists(sprocName) Then Return False
        sprocName = "activiser." & sprocName

        Dim sProcArgTA As New StoredProcedureParameterTableAdapter()
        sProcArgTA.Connection = adapter.SelectCommand.Connection
        sProcArgTA.Transaction = tx

        Dim sProcArgs As StoredProcedureParameterDataTable = sProcArgTA.GetData(sprocName)
        If Not sProcArgs Is Nothing Then
            Dim result As SqlClient.SqlCommand

            If adapter.DeleteCommand IsNot Nothing Then
                result = adapter.DeleteCommand
            Else
                result = New SqlClient.SqlCommand()
                adapter.DeleteCommand = result
            End If

            result.CommandText = sprocName
            result.CommandType = CommandType.StoredProcedure
            result.Connection = adapter.SelectCommand.Connection
            result.CommandTimeout = My.Settings.CommandTimeout * 1000
            result.Transaction = tx
            SqlClient.SqlCommandBuilder.DeriveParameters(result)
            For Each a As AttributeRow In entity.GetAttributeRows
                If Not dt.Columns.Contains(a.AttributeName) Then Continue For ' data set may have incomplete column set
                Dim sParmName As String = "@" & a.AttributeName
                If result.Parameters.Contains(sParmName) Then
                    result.Parameters(sParmName).SourceColumn = a.AttributeName
                End If
            Next

            Return True
        End If

        Return False
    End Function


    Private Shared Sub SetStandardParameters(ByVal command As System.Data.SqlClient.SqlCommand, ByVal deviceId As String, ByVal clientMask As Int32, ByVal consultantUid As Guid)
        If command.Parameters.Contains(callingClientIdParmName) Then
            command.Parameters(callingClientIdParmName).Value = deviceId
        End If
        If command.Parameters.Contains(callingClientTypeParmName) Then
            command.Parameters(callingClientTypeParmName).Value = clientMask
        End If
        If command.Parameters.Contains(callingUserIdParmName) Then
            command.Parameters(callingUserIdParmName).Value = consultantUid
        End If
    End Sub


    Private Shared Function GetUpSertDataAdapter( _
            ByVal clientMask As Int32, _
            ByVal deviceId As String, _
            ByVal consultantUid As Guid, _
            ByVal cbConnection As SqlClient.SqlConnection, _
            ByVal entity As EntityRow, _
            ByVal dt As DataTable) As SqlClient.SqlDataAdapter

        Dim upSertDa As New SqlClient.SqlDataAdapter(GetUpSertSelectQuery(entity), cbConnection)
        upSertDa.ContinueUpdateOnError = True

        Dim upSertCommandBuilder As New SqlClient.SqlCommandBuilder(upSertDa)

        Dim tx As SqlClient.SqlTransaction = Nothing
        If GetUpSertSProc(upSertDa, tx, entity, dt) Then
            ' since update and insert are the same command, we can just call these for one of them...
            SetStandardParameters(upSertDa.UpdateCommand, deviceId, clientMask, consultantUid)
        Else
            Debug.WriteLine(String.Format("Couldn't find compatible {0}_UpSert stored proc, looking for insert/update stored procs.", entity.EntityName))
            If Not GetUpdateSProc(upSertDa, tx, entity, dt) Then
                Debug.WriteLine(String.Format("Couldn't find compatible {0}_Update stored proc, falling back to ad-hoc SQL.", entity.EntityName))
                upSertDa.UpdateCommand = upSertCommandBuilder.GetUpdateCommand(True)
                SetStandardParameters(upSertDa.UpdateCommand, deviceId, clientMask, consultantUid)
            End If

            If Not GetInsertSProc(upSertDa, tx, entity, dt) Then
                Debug.WriteLine(String.Format("Couldn't find compatible {0}_Insert stored proc, falling back to ad-hoc SQL.", entity.EntityName))
                upSertDa.InsertCommand = upSertCommandBuilder.GetInsertCommand(True)
                SetStandardParameters(upSertDa.InsertCommand, deviceId, clientMask, consultantUid)
            End If
        End If

        If GetDeleteSProc(upSertDa, tx, entity, dt) Then
            SetStandardParameters(upSertDa.DeleteCommand, deviceId, clientMask, consultantUid)
        Else
            upSertDa.DeleteCommand = New SqlClient.SqlCommand("SET NOCOUNT ON;	SELECT 1 AS X INTO #X; SET NOCOUNT OFF; DELETE FROM #X;", cbConnection, tx) ' Note: no-op delete!
            upSertDa.DeleteCommand.CommandTimeout = My.Settings.CommandTimeout * 1000
        End If
        Return upSertDa
    End Function

    Public Shared Function SaveClientDataSet(ByVal source As activiserDataSet, ByVal clientMask As Int32, ByVal deviceId As String, ByVal consultantUid As Guid, ByVal syncTime As DateTime) As Integer
        If source Is Nothing Then Throw New ArgumentNullException("source")

        Dim result As Integer = 0

        Dim ds As DataSchema = GetDataSchema(clientMask)

        Dim cbConnection As New SqlClient.SqlConnection(My.Settings.activiserConnectionString)
        cbConnection.Open()

        Dim txConnection As New SqlClient.SqlConnection(My.Settings.activiserConnectionString)
        Dim tx As SqlClient.SqlTransaction
        txConnection.Open()
        tx = txConnection.BeginTransaction(String.Format("activiser.Save.{1:ddHHmmss}", deviceId, DateTime.UtcNow()))

        Try
            Dim eq = From qe As EntityRow In ds.Entity Order By qe.FillSequence
            For Each entity As EntityRow In eq
                If Not source.Tables.Contains(entity.EntityName) Then Continue For ' skip missing data.

                Dim dt As DataTable = source.Tables(entity.EntityName)
                Dim drq = From dr As DataRow In dt.Rows Where (dr.RowState = DataRowState.Added) Or (dr.RowState = DataRowState.Modified)

                If drq.Count = 0 Then Continue For

                Dim upSertDa As SqlClient.SqlDataAdapter = GetUpSertDataAdapter(clientMask, deviceId, consultantUid, cbConnection, entity, dt)
                upSertDa.DeleteCommand.Connection = txConnection
                upSertDa.DeleteCommand.Transaction = tx
                upSertDa.InsertCommand.Connection = txConnection
                upSertDa.InsertCommand.Transaction = tx
                upSertDa.InsertCommand.CommandTimeout = My.Settings.CommandTimeout * 1000
                upSertDa.UpdateCommand.Connection = txConnection
                upSertDa.UpdateCommand.Transaction = tx
                upSertDa.UpdateCommand.CommandTimeout = My.Settings.CommandTimeout * 1000

                For Each dr As DataRow In drq
                    'TODO: handle failure to insert/update correctly, as may happen if there is orphaned data.
                    result += upSertDa.Update(New DataRow() {dr})
                Next
            Next
            If tx.Connection IsNot Nothing Then
                tx.Commit()
            Else
                Throw New TransactionFailedException("Transaction implicitly closed. Use SQL Trace to identify possible causes.")
            End If
        Catch ex As TransactionFailedException
            Throw
        Catch ex As Exception
            If tx.Connection IsNot Nothing Then
                tx.Rollback()
            End If
            Throw
        Finally
            tx.Dispose()
            txConnection.Close()
            txConnection.Dispose()
        End Try

        'For Each dr As DataRow In dt.Rows
        '    ' plan 'B':
        '    'UpsertRow(dt, dr)

        '    Try
        '        '_rowStatesWeCareAbout = DataRowState.Added Or DataRowState.Modified Or DataRowState.Unchanged
        '        If (dr.RowState And _rowStatesWeCareAbout) <> 0 Then
        '            Dim drs() As DataRow = New DataRow() {dr}
        '            result += upSertDa.Update(drs)

        '            'Dim items As Integer

        '            ''cmdItemExists.Parameters.Clear()
        '            ''cmdItemExists.Parameters.AddWithValue("@PK", dr.Item(entity.PrimaryKeyAttributeName))
        '            ''items = CInt(cmdItemExists.ExecuteScalar())

        '            'If items = 1 Then
        '            '    dr(STR_ModifiedDateTime) = syncTime
        '            '    If dr.RowState <> DataRowState.Modified Then
        '            '        dr.AcceptChanges()
        '            '        dr.SetModified()
        '            '    End If
        '            '    'dr.SetModified()
        '            'ElseIf items = 0 Then
        '            '    If dr.RowState <> DataRowState.Added Then
        '            '        dr.AcceptChanges()
        '            '        dr.SetAdded()
        '            '    End If
        '            'Else ' panic; well, ignore...
        '            '    'TODO: Add Panic.
        '            'End If
        '            'result += upSertDa.Update(drs)
        '        ElseIf (dr.RowState = DataRowState.Deleted) Then
        '            ' 
        '        End If


        '    Catch ex As Exception

        '    End Try

        'Next




    End Function
#Region "Exceptions"
    <Serializable()> _
    Public Class AttributeTypeInvalidException
        Inherits System.Exception

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub

        Public Sub New( _
            ByVal info As System.Runtime.Serialization.SerializationInfo, _
            ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub

        Public Sub New(ByVal attributeName As String, ByVal attributeTypeCode As Integer)
            MyBase.New(String.Format("Attribute '{0}' has an invalid type code ({1})", attributeName, attributeTypeCode))
            Me.AttributeName = attributeName
            Me.AttributeTypeCode = attributeTypeCode
        End Sub

        Private _attributeTypeCode As Integer
        Public Property AttributeTypeCode() As Integer
            Get
                Return _attributeTypeCode
            End Get
            Set(ByVal value As Integer)
                _attributeTypeCode = value
            End Set
        End Property

        Private _propertyName As String
        Public Property AttributeName() As String
            Get
                Return _propertyName
            End Get
            Set(ByVal value As String)
                _propertyName = value
            End Set
        End Property
    End Class


    <Serializable()> _
    Public Class TransactionFailedException
        Inherits System.Exception

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub

        Public Sub New( _
            ByVal info As System.Runtime.Serialization.SerializationInfo, _
            ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
    End Class
#End Region

End Class

Namespace DataSchemaTableAdapters
    
    Partial Public Class EntityTableAdapter
        Private _commandTimeout As Integer
        Public Property CommandTimeout() As Integer
            Get
                Return _commandTimeout
            End Get
            Set(ByVal value As Integer)
                _commandTimeout = value
                For Each cmd As SqlClient.SqlCommand In CommandCollection
                    cmd.CommandTimeout = value
                Next
            End Set
        End Property

    End Class
End Namespace
