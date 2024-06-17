Option Explicit On
Option Strict On

' Authors - Michael Coulam and Tim Khoo
' Modified - 12-October-2004
' Rewrite - Ryan C. Price, 3-December-2004
'   re-arranged stored procedures
'   switched to strongly typed datasets
'
' A web service that interfaces between a pocket pc application (RemoteT2K) and a SQL Server database.

Imports activiser
Imports activiser.DataSchema
Imports activiser.WebService.Utilities

Imports System
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Reflection
Imports System.Data
Imports activiser.activiserDataSet
Imports System.ComponentModel
Imports System.Text

Partial Public Class activiserClientWebService
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    <WebMethod()> _
    Public Function ConsoleChangeUserPassPhrase(ByVal deviceid As String, ByVal user As Utility.UserDataTable, ByVal oldPassPhrase As String, ByVal passPhrase As String) As Boolean
        If Not CheckDeviceId(deviceid) Then Return Nothing
        Try
            If user IsNot Nothing AndAlso user.Count = 1 Then
                Dim target As Utility.UserRow = user(0)
                Dim cuta As New UtilityTableAdapters.UserTableAdapter
                Dim util As New UtilityTableAdapters.Utilities
                'try authenticate logged on user first.
                Dim users As Utility.UserDataTable = cuta.GetDataByDomainLogon(My.User.Name)
                'if that fails, try logging on with the username/password supplied
                If users Is Nothing Then
                    users = cuta.GetDataByUsernameAndPassword(target.Name, Encrypt(WebServiceGuid, oldPassPhrase))
                End If
                If users IsNot Nothing Then
                    If users.Count = 1 Then ' authenticated
                        Dim changer As Utility.UserRow = users(0)
                        If changer.ConsultantUID = target.ConsultantUID Then ' someone trying to change their own password
                            If util.ConsoleChangeUserPassword(target.ConsultantUID, Encrypt(WebServiceGuid, oldPassPhrase), Encrypt(WebServiceGuid, passPhrase)) = 1 Then
                                Return True ' cuta.GetDataByConsultantUid(target.ConsultantUid)
                            End If
                        Else
                            'changer is someone else
                            If changer.Administration OrElse changer.Management Then
                                If util.ConsoleSetUserPassword(target.ConsultantUID, Encrypt(WebServiceGuid, passPhrase)) = 1 Then
                                    Return True ' cuta.GetDataByConsultantUid(target.ConsultantUid)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return False

        Catch ex As Exception
            LogError(WebServiceGuid, DateTime.UtcNow, deviceid, MethodBase.GetCurrentMethod.Name, "Error changing passphrase", "Consultant", Guid.Empty, My.User.Name, Nothing)
            Return False
        End Try
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    <WebMethod()> _
    Public Function ConsoleLinkNetworkUser(ByVal deviceid As String, ByVal user As Utility.UserDataTable, ByVal passPhrase As String) As Boolean
        If Not CheckDeviceId(deviceid) Then Return Nothing
        Try
            If user IsNot Nothing AndAlso user.Count = 1 Then
                Dim target As Utility.UserRow = user(0)
                Dim users As Utility.UserDataTable

                Using cuta As New UtilityTableAdapters.UserTableAdapter
                    ' Dim users As Utility.UserDataTable
                    If String.IsNullOrEmpty(passPhrase) Then
                        users = cuta.GetDataByUsernameAndPassword(target.Name, String.Empty)
                    ElseIf passPhrase = "*" Then ' * means linked only, no 'activiser' password, and access using 'activiser' password invalid.
                        users = cuta.GetDataByUsernameAndPassword(target.Name, passPhrase)
                    Else
                        users = cuta.GetDataByUsernameAndPassword(target.Name, Encrypt(WebServiceGuid, passPhrase))
                    End If
                    ' users = cuta.GetDataByUserAndPassword(target.Name, encryptPassPhrase(passPhrase))
                End Using

                If users IsNot Nothing AndAlso users.Count = 1 Then ' authenticated username/password
                    If users(0).ConsultantUID = target.ConsultantUID Then
                        Using util As New UtilityTableAdapters.Utilities
                            If util.ConsoleLinkNetworkUser(My.User.Name, target.ConsultantUID) = 1 Then
                                Return True
                            End If
                        End Using
                    End If
                End If
            End If
            Return False
        Catch ex As Exception
            LogError(WebServiceGuid, DateTime.UtcNow, deviceid, MethodBase.GetCurrentMethod.Name, "Error linking network user", "Consultant", Guid.Empty, My.User.Name, Nothing)
            Return False
        End Try
    End Function

    '#If DEBUG Then
    '    <WebMethod()> _
    '    Public Function ConsoleSetUserPassPhrase(ByVal deviceid As String, ByVal user As String, ByVal oldPassPhrase As String, ByVal passPhrase As String) As Boolean
    '        If Not checkdeviceid(deviceid) Then Return Nothing
    '        Try
    '            If Not String.IsNullOrEmpty(user) Then 'IsNot Nothing AndAlso user.Count = 1 Then
    '                Dim cuta As New UtilityTableAdapters.UserTableAdapter
    '                Dim util As New UtilityTableAdapters.Utilities
    '                'try authenticate logged on user first.
    '                Dim users As Utility.UserDataTable = cuta.GetDataByDomainLogon(My.User.Name)
    '                Dim target As Utility.UserRow ' = cuta.GetDataByUserAndPassword(user, encryptPassPhrase(oldPassPhrase))(0)
    '                Dim changer As Utility.UserRow

    '                If users Is Nothing Then
    '                    users = cuta.GetDataByUserAndPassword(user, EncryptPassPhrase(oldPassPhrase))
    '                    changer = users(0)
    '                    target = users(0)
    '                Else
    '                    changer = users(0)
    '                    users = cuta.GetDataByConsultantName(user)
    '                    target = users(0)
    '                End If
    '                If users IsNot Nothing Then
    '                    If users.Count = 1 Then ' authenticated
    '                        If changer.ConsultantUID = target.ConsultantUID Then ' someone trying to change their own password
    '                            If util.ConsoleChangeUserPassword(target.ConsultantUID, EncryptPassPhrase(oldPassPhrase), EncryptPassPhrase(passPhrase)) = 1 Then
    '                                Return True ' cuta.GetDataByConsultantUid(target.ConsultantUid)
    '                            End If
    '                        Else
    '                            'changer is someone else
    '                            If changer.Administration OrElse changer.Management Then
    '                                If util.ConsoleSetUserPassword(target.ConsultantUID, EncryptPassPhrase(passPhrase)) = 1 Then
    '                                    Return True ' cuta.GetDataByConsultantUid(target.ConsultantUid)
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '            Return False

    '        Catch ex As Exception

    '        End Try

    '    End Function

    '#End If

    <WebMethod()> _
     Public Function ConsoleGetDataSet(ByVal deviceId As String, ByVal ConsultantUid As Guid) As activiserDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Try
            Return DataSchema.GetClientDataSet(SchemaClientMask.ConsolePublic, deviceId, Nothing)

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <Obsolete("Use ConsoleGetDataSetAsXml instead."), _
    EditorBrowsable(EditorBrowsableState.Never), _
    WebMethod()> _
    Public Function ConsoleGetDataSetXml(ByVal deviceId As String, ByVal ConsultantUid As String) As Xml.XmlNode
        Return ConsoleGetDataSetAsXml(deviceId, ConsultantUid)
    End Function

    <WebMethod()> _
         Public Function ConsoleGetDataSetAsXml(ByVal deviceId As String, ByVal ConsultantUid As String) As Xml.XmlNode
        Dim pds As New activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            DataSchema.FillClientDataSet(pds, SchemaClientMask.ConsolePublic, deviceId, Nothing)

        Catch ex As Exception
            Dim consultantGuid As Guid
            If Not IsGuid(ConsultantUid, consultantGuid) Then
                consultantGuid = Guid.Empty
            End If
            LogError(consultantGuid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
        End Try
        Return SerializeDataSetToXmlDoc(pds)
    End Function


    <WebMethod()> _
    Public Function ConsoleGetDataSetCompressed(ByVal deviceId As String, ByVal ConsultantUid As String) As Byte()
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Try

            Dim resultSet As DataSet = DataSchema.GetClientDataSet(SchemaClientMask.ConsolePublic, deviceId, Nothing)
            Dim dsXml As String = resultSet.GetXml()

            Dim result As Byte() = CompressString(dsXml)

            Return result
        Catch ex As Exception
            Dim consultantGuid As Guid
            If Not IsGuid(ConsultantUid, consultantGuid) Then
                consultantGuid = Guid.Empty
            End If
            LogError(consultantGuid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetDataSetUpdates(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal ModifiedSince As DateTime) As activiserDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Return DataSchema.GetClientDataSet(SchemaClientMask.ConsolePublic, deviceId, ModifiedSince)
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetDataSetUpdatesAsXml(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal ModifiedSince As DateTime) As Xml.XmlNode
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Return SerializeDataSetToXmlDoc(DataSchema.GetClientDataSet(SchemaClientMask.ConsolePublic, deviceId, ModifiedSince))
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetDataSetUpdatesCompressed(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal ModifiedSince As DateTime) As Byte()
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim resultSet = DataSchema.GetClientDataSet(SchemaClientMask.ConsolePublic, deviceId, ModifiedSince)
            Return CompressString(resultSet.GetXml())
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function


    <WebMethod()> _
    Public Function ConsoleGetDeviceTrackingData(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal since As DateTime, ByVal until As DateTime) As DeviceTrackingDataSet.DeviceTrackingDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Using dtta As New DeviceTrackingDataSetTableAdapters.DeviceTrackingTableAdapter
                Dim nSince, nUntil As New Nullable(Of DateTime)
                If since > SqlMinDate Then
                    nSince = since
                End If
                If until < SqlMaxDate Then
                    nUntil = until
                End If

                If ConsultantUid = Guid.Empty Then 'fill
                    Return dtta.GetDataByTimestamp(nSince, nUntil)
                Else
                    Return dtta.GetDataByConsultantAndTimestamp(ConsultantUid, nSince, nUntil)
                End If

            End Using
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting tracking data", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetDeviceTrackingDataCompressed(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal since As DateTime, ByVal until As DateTime) As Byte()
        Dim data As DeviceTrackingDataSet.DeviceTrackingDataTable = _
            ConsoleGetDeviceTrackingData(deviceId, ConsultantUid, since, until)
        Return CompressString(data.DataSet.GetXml())
    End Function

    <WebMethod()> _
    Public Function ConsoleGetDeviceTrackingDataAsAt(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal asAt As DateTime) As DeviceTrackingDataSet.DeviceTrackingDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Using dtta As New DeviceTrackingDataSetTableAdapters.DeviceTrackingTableAdapter
                If consultantUid = Guid.Empty Then 'fill
                    Return dtta.GetAllConsultantsAsAt(asAt)
                Else
                    Return dtta.GetDataByConsultantAsAt(consultantUid, asAt)
                End If

            End Using
        Catch ex As Exception
            LogError(consultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting tracking data", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetSyncLog(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal Since As DateTime) As SyncLogDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim elta As New SyncLogDataSetTableAdapters.SyncLogTableAdapter
            Dim el As New SyncLogDataSet

            If Since.Equals(NullDate) Then
                elta.Fill(el.SyncLog)
            Else
                elta.FillLoggedSince(el.SyncLog, Since)
            End If

            Return el
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading client data", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetEventLog(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal Since As DateTime) As EventLogDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim elta As New EventLogDataSetTableAdapters.EventLogTableAdapter
            Dim el As New EventLogDataSet

            If Since.Equals(NullDate) Then
                elta.Fill(el.EventLog)
            Else
                elta.FillLoggedSince(el.EventLog, Since)
            End If

            Return el
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting event log data", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleUpdateEventLog(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal eventLog As EventLogDataSet) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim elta As New EventLogDataSetTableAdapters.EventLogTableAdapter

            elta.Update(eventLog)

            Return True
        Catch ex As Exception
            LogError(consultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading client data", "", WebServiceGuid, "", ex)
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetSettings(ByVal deviceId As String, ByVal ConsultantUid As Guid) As ClientSettingDataSet.ClientSettingDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Return (New ClientSettingDataSetTableAdapters.ClientSettingTableAdapter).GetDataForClient(deviceId, ConsultantUid, SchemaClientMask.Console, Nothing)
    End Function

    <WebMethod()> _
    Public Function ConsoleUpdateSettings(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal settings As ClientSettingDataSet.ClientSettingDataTable) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim spta As New ClientSettingDataSetTableAdapters.ClientSettingTableAdapter
            spta.Update(settings)
            Return True
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error updating Console Settings", "", WebServiceGuid, "", ex)
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetClientSettings(ByVal deviceId As String, ByVal ConsultantUid As Guid) As ClientSettingDataSet.ClientSettingDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Return (New ClientSettingDataSetTableAdapters.ClientSettingTableAdapter).GetData()
    End Function

    <WebMethod()> _
    Public Function ConsoleUpdateClientSettings(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal settings As ClientSettingDataSet.ClientSettingDataTable) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Try
            Dim spta As New ClientSettingDataSetTableAdapters.ClientSettingTableAdapter
            spta.Update(settings)
            Return True
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error updating Console Settings", "", WebServiceGuid, "", ex)
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetLastServerSettingChange(ByVal deviceId As String) As DateTime
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Return Utility.LastServerSettingChange
    End Function

    <WebMethod()> _
    Public Function ConsoleGetServerProfile(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal current As Boolean) As Utility.ServerSettingDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim spta As New UtilityTableAdapters.ServerSettingTableAdapter

            If current Then
                Return spta.GetCurrentData()
            Else
                Return spta.GetData()
            End If

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error fetching Server Profile", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleUpdateServerProfile(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal serverProfile As Utility.ServerSettingDataTable) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim spta As New UtilityTableAdapters.ServerSettingTableAdapter
            spta.Update(serverProfile)
            JobEmail.RefreshProfile()
            Return True
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error updating Server Profile", "", WebServiceGuid, "", ex)
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleGetClientDevices(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal current As Boolean) As ClientRegistrationDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim cdta As New ClientRegistrationDataSetTableAdapters.ClientTableAdapter
            Dim cdsta As New ClientRegistrationDataSetTableAdapters.ClientStatusTableAdapter

            Dim result As New ClientRegistrationDataSet

            If current Then
                cdsta.Fill(result.ClientStatus)
                cdta.FillCurrent(result.Client, Nothing)
            Else
                cdsta.Fill(result.ClientStatus)
                cdta.Fill(result.Client)
            End If

            Return result
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error fetching Server Profile", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function


    <WebMethod()> _
    Public Function ConsoleUpdateClientDevices(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal clientDevices As ClientRegistrationDataSet) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim cdtam As New ClientRegistrationDataSetTableAdapters.TableAdapterManager
            cdtam.ClientStatusTableAdapter = New ClientRegistrationDataSetTableAdapters.ClientStatusTableAdapter
            cdtam.ClientTableAdapter = New ClientRegistrationDataSetTableAdapters.ClientTableAdapter
            cdtam.Connection = New SqlClient.SqlConnection(My.Settings.activiserConnectionString)
            cdtam.UpdateOrder = ClientRegistrationDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.UpdateInsertDelete
            cdtam.UpdateAll(clientDevices)

            Return True
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error updating Server Profile", "", WebServiceGuid, "", ex)
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleUploadDataSetUpdates(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal consoleDataSet As activiserDataSet) As Integer
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            For Each r As RequestRow In consoleDataSet.Request
                CleanUploadedRequest(r, DateTime.UtcNow(), ConsultantUid, False)
            Next

            For Each j As JobRow In consoleDataSet.Job
                CleanUploadedJob(j, DateTime.UtcNow(), ConsultantUid, False)
            Next

            DataSchema.SaveClientDataSet(consoleDataSet, SchemaClientMask.Console, deviceId, ConsultantUid, DateTime.UtcNow)
            SaveToGateway(Guid.NewGuid(), consoleDataSet, ConsultantUid)

            If Utility.GetServerSetting(My.Resources.SendJobEmailSettingKey, False) Then
                Dim emailQueue As New Collections.Generic.Queue(Of Guid)
                For Each JobItem As activiserDataSet.JobRow In consoleDataSet.Job
                    EnqueueJobMail(emailQueue, JobItem)
                Next

                If emailQueue.Count > 0 Then
                    SendEmail(emailQueue)
                End If
            End If

            Return 0

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading client data", "", WebServiceGuid, "", ex)
            Return -1
        End Try
    End Function


    <WebMethod()> _
    Public Function ConsoleUploadDataSetUpdatesAsXml(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal clientData As Xml.XmlNode) As Integer
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing

        Dim uploadResults As New UploadResults

        Try
            Debug.WriteLine(My.User.IsAuthenticated & ":" & My.User.Name)

            Dim syncTime As DateTime = GetSyncTime(deviceId, consultantGuid)

            Dim clientDataSet As activiserDataSet = DataSchema.GetClientSchema(DataSchema.SchemaClientMask.Console)

            Dim srpda As New IO.StringReader(clientData.OuterXml)
            Dim xrpda As New Xml.XmlTextReader(srpda)

            clientDataSet.EnforceConstraints = False
            clientDataSet.ReadXml(xrpda)

            For Each r As RequestRow In clientDataSet.Request
                CleanUploadedRequest(r, DateTime.UtcNow(), consultantGuid, True)
            Next

            For Each j As JobRow In clientDataSet.Job
                CleanUploadedJob(j, DateTime.UtcNow(), consultantGuid, True)
            Next

            DataSchema.SaveClientDataSet(clientDataSet, DataSchema.SchemaClientMask.Console, deviceId, consultantGuid, DateTime.UtcNow)
            SaveToGateway(Guid.NewGuid(), clientDataSet, consultantGuid)

            If Utility.GetServerSetting(My.Resources.SendJobEmailSettingKey, False) Then
                Dim emailQueue As New Collections.Generic.Queue(Of Guid)
                For Each JobItem As activiserDataSet.JobRow In clientDataSet.Job
                    EnqueueJobMail(emailQueue, JobItem)
                Next

                If emailQueue.Count > 0 Then
                    SendEmail(emailQueue)
                End If
            End If


            Return 0
        Catch ex As Exception
            LogError(consultantGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading client data", "", WebServiceGuid, "", ex)
            Return -1
        End Try
    End Function


    <WebMethod()> _
    Public Function ConsoleUploadataSetUpdatesCompressed(ByVal deviceId As String, ByVal consultantUid As String, ByVal clientData As Byte()) As Integer
        Dim clientDataXml = New Xml.XmlDocument
        clientDataXml.LoadXml(DecompressString(clientData))

        Return ConsoleUploadDataSetUpdatesAsXml(deviceId, consultantUid, clientDataXml)
    End Function

    <WebMethod()> _
    Public Function ConsoleUpdateTerminology(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal updates As LanguageDataSet) As LanguageDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing
        If updates Is Nothing Then Return Nothing
        Try
            Dim labTa As New LanguageDataSetTableAdapters.StringValueTableAdapter
            Dim langTa As New LanguageDataSetTableAdapters.LanguageTableAdapter

            'Dim deletedStrings As DataRow() = updates.StringValue.Select(Nothing, Nothing, DataViewRowState.Deleted)
            'If deletedStrings.Length <> 0 Then
            '    labTa.Update(updates.StringValue)
            'End If

            If updates.Language.Count <> 0 Then
                langTa.Update(updates.Language)
            End If

            If updates.StringValue.Count <> 0 Then
                labTa.Update(updates.StringValue)
            End If

            Dim ds As New LanguageDataSet()
            langTa.Fill(ds.Language)
            labTa.Fill(ds.StringValue)

            Return ds

        Catch ex As Exception
            LogError(consultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error updating custom terminology", "CustomTerminology", consultantUid, "", ex)
            Return Nothing
        End Try
    End Function

    '<WebMethod(Description:="Get list of potential console users")> _
    'Public Function ConsoleGetUserList(ByVal deviceId As String) As Utility.UserDataTable ' DataSet
    '    If DeviceId <> "35060af8-9b60-45a9-ac93-5dc6711af0cd" Then Return Nothing ' console
    '    If Not My.User.IsAuthenticated Then
    '        Throw New UnauthorizedAccessException("Anonymous Access to Console Function Denied")
    '        Return Nothing
    '    End If

    '    Dim cuta As New UtilityTableAdapters.UserTableAdapter
    '    ' Dim ds As New DataSet("ConsoleUserList")
    '    ' ds.Tables.Add(cuta.GetData())
    '    Return cuta.GetData
    '    'Return ds
    'End Function

    ' hash the provided password - making one-way encryption for the password.
    'Private Shared Function EncryptPassPhrase(ByVal passPhrase As String) As String
    '    If String.IsNullOrEmpty(passPhrase) Then Return String.Empty
    '    Dim pwArray() As Byte = Convert.FromBase64String(passPhrase)
    '    Dim crypto As New System.Security.Cryptography.MD5CryptoServiceProvider()
    '    Try
    '        Dim encArray() As Byte = crypto.ComputeHash(pwArray)
    '        Return Convert.ToBase64String(encArray)
    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        crypto = Nothing
    '    End Try
    'End Function

    <WebMethod()> _
    Public Function ConsoleLogonConsultant(ByVal deviceId As String, ByVal userName As String, ByVal passPhrase As String) As Utility.UserDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Try
            Dim cuta As New UtilityTableAdapters.UserTableAdapter
            ' passphrase = system.Security.Cryptography.
            Dim users As Utility.UserDataTable
            If String.IsNullOrEmpty(passPhrase) Then
                users = cuta.GetDataByUsernameAndPassword(userName, String.Empty)
            Else
                users = cuta.GetDataByUsernameAndPassword(userName, Encrypt(WebServiceGuid, passPhrase))
            End If
            If users IsNot Nothing AndAlso users.Count = 1 Then
                Return users
            Else
                LogError(WebServiceGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error logging on activiser user", "Consultant", Guid.Empty, userName, Nothing)
                Return Nothing
            End If
        Catch ex As Exception
            LogError(WebServiceGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error logging on activiser user", "Consultant", Guid.Empty, userName, Nothing)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ConsoleLogonNetworkUser(ByVal deviceid As String) As Utility.UserDataTable
        If Not CheckDeviceId(deviceid) Then Return Nothing
        Try
            Dim cuta As New UtilityTableAdapters.UserTableAdapter

            Dim users As Utility.UserDataTable = cuta.GetDataByDomainLogon(Me.User.Identity.Name)
            If users IsNot Nothing AndAlso users.Count = 1 Then
                Return users
            Else
                LogError(WebServiceGuid, DateTime.UtcNow, deviceid, MethodBase.GetCurrentMethod.Name, "Error logging on network user", "Consultant", Guid.Empty, My.User.Name, Nothing)
                Return Nothing
            End If
        Catch ex As Exception
            LogError(WebServiceGuid, DateTime.UtcNow, deviceid, MethodBase.GetCurrentMethod.Name, "Error logging on network user", "Consultant", Guid.Empty, My.User.Name, ex)
            Return Nothing
        End Try
    End Function

    'Private Shared Sub ConsoleUploadDataSetUpdates_UpdateClientSiteStatusData(ByRef consoleDataSet As ClientDataSet, ByRef extantData As ClientDataSet, ByRef ta As ClientDataSetTableAdapters.ClientSiteStatusTableAdapter)
    '    If consoleDataSet.ClientSiteStatus.Count > 0 Then
    '        ta.Fill(extantData.ClientSiteStatus)
    '        For Each updatedRow As ClientDataSet.ClientSiteStatusRow In consoleDataSet.ClientSiteStatus
    '            Dim er As ClientDataSet.ClientSiteStatusRow = extantData.ClientSiteStatus.FindByClientSiteStatusID(updatedRow.ClientSiteStatusID)
    '            If er Is Nothing OrElse (er.Description <> updatedRow.Description OrElse er.IsActive <> updatedRow.IsActive) Then
    '                ta.Update(updatedRow)
    '            End If
    '        Next
    '    End If
    'End Sub

    'Private Shared Sub ConsoleUploadDataSetUpdates_UpdateRequestStatusData(ByRef consoleDataSet As ClientDataSet, ByRef extantData As ClientDataSet, ByRef ta As ClientDataSetTableAdapters.RequestStatusTableAdapter)
    '    If consoleDataSet.RequestStatus.Count > 0 Then
    '        ta.Fill(extantData.RequestStatus)
    '        For Each ur As ClientDataSet.RequestStatusRow In consoleDataSet.RequestStatus
    '            Dim er As ClientDataSet.RequestStatusRow = extantData.RequestStatus.FindByRequestStatusID(ur.RequestStatusID)
    '            If er Is Nothing Then
    '                ta.Update(ur)
    '            Else
    '                'paranoid code - make sure there is something different!
    '                For Each dc As DataColumn In consoleDataSet.RequestStatus.Columns
    '                    If dc.ColumnName = STR_CreatedDateTime OrElse dc.ColumnName = STR_ModifiedDateTime Then
    '                        Continue For
    '                    End If
    '                    If (er.IsNull(dc.ColumnName) AndAlso Not ur.IsNull(dc.ColumnName)) _
    '                    OrElse (Not er.IsNull(dc.ColumnName) AndAlso ur.IsNull(dc.ColumnName)) _
    '                    OrElse (Not er.Item(dc.ColumnName).Equals(ur.Item(dc.ColumnName))) _
    '                    Then
    '                        ta.Update(ur)
    '                        Exit For
    '                    End If
    '                Next
    '            End If
    '        Next
    '    End If
    'End Sub

    'Private Shared Sub ConsoleUploadDataSetUpdates_UpdateJobStatusData(ByRef consoleDataSet As ClientDataSet, ByRef extantData As ClientDataSet, ByRef ta As ClientDataSetTableAdapters.JobStatusTableAdapter)
    '    If consoleDataSet.JobStatus.Count > 0 Then
    '        ta.Fill(extantData.JobStatus)
    '        For Each ur As ClientDataSet.JobStatusRow In consoleDataSet.JobStatus
    '            Dim er As ClientDataSet.JobStatusRow = extantData.JobStatus.FindByJobStatusID(ur.JobStatusID)
    '            If er Is Nothing OrElse er.Description <> ur.Description Then
    '                ta.Update(ur)
    '            End If
    '        Next
    '    End If
    'End Sub

End Class
