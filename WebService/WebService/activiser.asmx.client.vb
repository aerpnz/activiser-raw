Option Explicit On
Option Strict On

Imports activiser
Imports activiser.DataSchema
Imports activiser.WebService.Utilities

Imports System
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Reflection
Imports System.Data
Imports activiser.activiserDataSet
Imports activiser.activiserDataSetTableAdapters
Imports System.ComponentModel

Partial Public Class activiserClientWebService
    <WebMethod()> _
        Public Function ClientGetUserDetails(ByVal deviceId As String, ByVal userName As String, ByVal passPhrase As String) As activiserDataSet.ConsultantDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Dim uta As New UtilityTableAdapters.UserTableAdapter
        Dim users As Utility.UserDataTable
        Try
            If String.IsNullOrEmpty(userName) Then
                users = uta.GetDataByDomainLogon(Me.User.Identity.Name)
                userName = Me.User.Identity.Name ' for error log
            Else
                users = uta.GetDataByUsernameAndPassword(userName, Encrypt(WebServiceGuid, passPhrase))
            End If

            If users IsNot Nothing AndAlso users.Count = 1 Then
                Dim cta As New ConsultantTableAdapter
                Dim myUser As Utility.UserRow = users(0)
                Dim result As activiserDataSet.ConsultantDataTable = cta.GetDataByConsultantUid(myUser.ConsultantUID)
                Return result
            Else
                LogError(WebServiceGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "LogonError", "Consultant", Guid.Empty, userName, Nothing)
                Return Nothing
            End If

        Catch ex As Exception
            LogError(WebServiceGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "LogonError", "Consultant", Guid.Empty, userName, Nothing)
            Return Nothing
        End Try
    End Function
    ' Sets the time the Consultant synchronised, so that in future only new or modified information will be sent.
    <WebMethod()> _
    Public Function SyncStart(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Date
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return Utility.ConsultantStartSync(ConsultantUid)

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error starting sync", "Consultant", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function

    ' Sets the time the Consultant synchronised, so that in future only new or modified information will be sent.
    <WebMethod()> _
    Public Function SyncComplete(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Date
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return Utility.ConsultantCompleteSync(ConsultantUid)
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error completing sync", "Consultant", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetLastSyncTime(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Date
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return Utility.GetConsultantLastSyncTime(ConsultantUid)
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting last sync time", "Consultant", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetSyncTime(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Date
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return Utility.GetConsultantSyncTime(ConsultantUid)
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting sync time", "Consultant", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function


#Region "Client Site Web Service Methods"

    <WebMethod(BufferResponse:=False, enableSession:=False)> _
    Public Function GetClientSiteDetails(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal clientSiteUid As Guid) As activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Dim result As activiserDataSet = GetClientSchema(SchemaClientMask.ClientPublic)
            result.EnforceConstraints = False
            Dim cta As New ClientSiteTableAdapter
            If cta.FillByClientSiteUid(result.ClientSite, clientSiteUid) = 1 Then
                DataSchema.FillClientDataSet(result, SchemaClientMask.ClientPublic, deviceId, ConsultantUid, result.ClientSite.TableName, clientSiteUid)
                ' GetCustomData(deviceId, ConsultantUid, result, NullDate, False)
                Return result
            Else
                Return Nothing
            End If

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting client site details", "ClientSite", clientSiteUid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod(BufferResponse:=False, enableSession:=False)> _
    Public Function GetClientSiteOpenRequests(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal clientSiteUid As Guid) As activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Dim result As New activiserDataSet
            result.EnforceConstraints = False
            Dim rta As New RequestTableAdapter
            If rta.FillActiveByClientSiteForConsultant(result.Request, ConsultantUid, clientSiteUid) <> 0 Then
                Return result
            Else
                Return Nothing
            End If
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting client site details", "ClientSite", clientSiteUid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod(BufferResponse:=False, enableSession:=False)> _
    Public Function GetCurrentClientSiteList(ByVal deviceId As String) As activiserDataSet.ClientSiteDataTable
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Return (New ClientSiteTableAdapter).GetEssentialDataForActiveClients
        Catch ex As Exception
            LogError(Nothing, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting current client list", "ClientSite", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function
#End Region

#Region "Client Dataset Web Service Methods"

    <WebMethod(BufferResponse:=False, enableSession:=False)> _
    Public Function GetClientDataSet(ByVal deviceId As String, ByVal ConsultantUid As Guid) As activiserDataSet
        Dim pds As New activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            DataSchema.FillClientDataSet(pds, DataSchema.SchemaClientMask.Client, ConsultantUid, deviceId, Nothing)

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)

        End Try
        Return pds
    End Function

    <Obsolete("Use ConsoleGetDataSetAsXml instead."), _
        EditorBrowsable(EditorBrowsableState.Never), _
        WebMethod()> _
    Public Function GetClientDataSeXml(ByVal deviceId As String, ByVal ConsultantUid As String) As Xml.XmlNode
        Return ConsoleGetDataSetAsXml(deviceId, ConsultantUid)
    End Function

    <WebMethod(BufferResponse:=False, enableSession:=False)> _
    Public Function GetClientDataSetAsXml(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Xml.XmlNode
        Dim pds As New activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            DataSchema.FillClientDataSet(pds, DataSchema.SchemaClientMask.Client, ConsultantUid, deviceId, Nothing)

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
        End Try
        Return SerializeDataSetToXmlDoc(pds)
    End Function


    <WebMethod(BufferResponse:=False, enableSession:=False)> _
    Public Function GetClientDataSetCompressed(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Byte()
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return CompressString(DataSchema.GetClientDataSet(DataSchema.SchemaClientMask.Client, ConsultantUid, deviceId, Nothing).GetXml())
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function


    <WebMethod(BufferResponse:=False, enableSession:=False)> _
    Public Function GetClientDataSetUpdates(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal Since As DateTime) As activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return DataSchema.GetClientDataSet(DataSchema.SchemaClientMask.Client, ConsultantUid, deviceId, Since)
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetClientDataSetUpdatesAsXml(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal Since As DateTime) As Xml.XmlNode
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing

        Dim pds As New activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            DataSchema.FillClientDataSet(pds, DataSchema.SchemaClientMask.Client, consultantGuid, deviceId, Since)

        Catch ex As Exception
            LogError(consultantGuid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
        End Try
        Return SerializeDataSetToXmlDoc(pds)
    End Function

    <WebMethod()> _
    Public Function GetClientDataSetUpdatesCompressed(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal Since As DateTime) As Byte()
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing

        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            Return CompressString(DataSchema.GetClientDataSet(DataSchema.SchemaClientMask.Client, consultantGuid, deviceId, Since).GetXml())

        Catch ex As Exception
            LogError(consultantGuid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function UploadClientDataSetUpdates(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal clientDataSet As activiserDataSet) As UploadResults
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Dim uploadResults As New UploadResults

        Try
            Debug.WriteLine(My.User.IsAuthenticated & ":" & My.User.Name)

            Dim syncTime As DateTime = GetSyncTime(deviceId, consultantUid)

            For Each r As RequestRow In clientDataSet.Request
                CleanUploadedRequest(r, DateTime.UtcNow(), consultantUid, True)
            Next

            For Each j As JobRow In clientDataSet.Job
                CleanUploadedJob(j, DateTime.UtcNow(), consultantUid, True)
            Next

            DataSchema.SaveClientDataSet(clientDataSet, DataSchema.SchemaClientMask.Mobile Or SchemaClientMask.WindowsClient, deviceId, consultantUid, syncTime)

            For Each r As RequestRow In clientDataSet.Request
                Dim updatedRequest As RequestRow = FetchRow(r.Table, r)
                AddRequestToUploadResults(uploadResults, updatedRequest)
            Next

            For Each j As JobRow In clientDataSet.Job
                Dim updatedJob As JobRow = FetchRow(j.Table, j)
                AddJobToUploadResults(uploadResults, updatedJob)
            Next

            SaveToGateway(Guid.NewGuid(), clientDataSet, consultantUid)

            If Utility.GetServerSetting(My.Resources.SendJobEmailSettingKey, False) Then
                Dim emailQueue As New Collections.Generic.Queue(Of Guid)
                For Each JobItem As activiserDataSet.JobRow In clientDataSet.Job
                    EnqueueJobMail(emailQueue, JobItem)
                Next

                If emailQueue.Count > 0 Then
                    SendEmail(emailQueue)
                End If
            End If

            Return uploadResults
        Catch ex As Exception
            LogError(consultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading client data", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function UploadClientDataSetUpdatesAsXml(ByVal deviceId As String, ByVal consultantUid As String, ByVal clientData As Xml.XmlNode) As Xml.XmlNode
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Dim consultantGuid As Guid
        If Not IsGuid(consultantUid, consultantGuid) Then Return Nothing

        Dim uploadResults As New UploadResults

        Try
            Debug.WriteLine(My.User.IsAuthenticated & ":" & My.User.Name)

            Dim syncTime As DateTime = GetSyncTime(deviceId, consultantGuid)

            Dim clientDataSet As activiserDataSet = DataSchema.GetClientSchema(DataSchema.SchemaClientMask.Client)

            'Dim srpda As New IO.StringReader(clientData.OuterXml)
            Dim xrpda As New Xml.XmlNodeReader(clientData) ' Xml.XmlReader = Xml.XmlReader.Create(srpda)

            clientDataSet.EnforceConstraints = False
            clientDataSet.ReadXml(xrpda, XmlReadMode.IgnoreSchema)

            For Each r As RequestRow In clientDataSet.Request
                CleanUploadedRequest(r, DateTime.UtcNow(), consultantGuid, True)
            Next

            For Each j As JobRow In clientDataSet.Job
                CleanUploadedJob(j, DateTime.UtcNow(), consultantGuid, True)
            Next

            DataSchema.SaveClientDataSet(clientDataSet, DataSchema.SchemaClientMask.Mobile Or SchemaClientMask.WindowsClient, deviceId, consultantGuid, syncTime)

            For Each r As RequestRow In clientDataSet.Request
                Dim updatedRequest As RequestRow = FetchRow(r.Table, r)
                AddRequestToUploadResults(uploadResults, updatedRequest)
            Next

            For Each j As JobRow In clientDataSet.Job
                Dim updatedJob As JobRow = FetchRow(j.Table, j)
                AddJobToUploadResults(uploadResults, updatedJob)
            Next

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

            Return SerializeDataSetToXmlDoc(uploadResults)
        Catch ex As Exception
            LogError(consultantGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading client data", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function UploadClientDataSetUpdatesCompressed(ByVal deviceId As String, ByVal consultantUid As String, ByVal clientData As Byte()) As Byte()
        Dim consultantGuid As Guid
        If Not IsGuid(consultantUid, consultantGuid) Then Return Nothing
        Try
            Dim clientDataXml = New Xml.XmlDocument
            Dim uncompressedString As String = DecompressString(clientData)
            clientDataXml.LoadXml(uncompressedString)

            Dim resultXml = UploadClientDataSetUpdatesAsXml(deviceId, consultantUid, clientDataXml)
            Return CompressString(resultXml.OuterXml)
        Catch ex As Exception
            LogError(consultantGuid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading client data", "", WebServiceGuid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetClientSettings(ByVal deviceId As String, ByVal consultantUid As String, ByVal clientMask As Integer, ByVal since As DateTime?) As ClientSettingDataSet.ClientSettingDataTable
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Dim consultantGuid As Guid
        If Not IsGuid(consultantUid, consultantGuid) Then Return Nothing
        Return (New ClientSettingDataSetTableAdapters.ClientSettingTableAdapter).GetDataForClient(deviceId, consultantGuid, clientMask, since)
    End Function

    <WebMethod()> _
    Public Function GetShortRequestList(ByVal deviceId As String, ByVal ConsultantUid As String) As activiserDataSet
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing
        Dim pds As New activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Dim csta As New ClientSiteTableAdapter
            Dim cta As New ConsultantTableAdapter
            Dim rsta As New RequestStatusTableAdapter
            Dim rta As New RequestTableAdapter

            csta.FillEssentialDataForActiveClients(pds.ClientSite)
            cta.FillForClient(pds.Consultant)
            rsta.Fill(pds.RequestStatus)
            rta.FillShortRequestList(pds.Request)

            Return pds

        Catch ex As Exception
            LogError(consultantGuid, DateTime.UtcNow, deviceId, ex.Source, ex.Message, MethodBase.GetCurrentMethod.Name, WebServiceGuid, "", ex)
            Return pds
        End Try
    End Function

    <WebMethod()> _
    Public Function GetRequestDetails(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal RequestUid As Guid) As activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Dim result As activiserDataSet = DataSchema.GetClientSchema(-1)
            result.EnforceConstraints = False

            Dim ds As DataSchema = DataSchema.GetDataSchema
            DataSchema.FillClientDataSet(result, -1, deviceId, ConsultantUid, "Request", RequestUid)

            If result.Request.Count = 1 Then
                Dim cta As New ConsultantTableAdapter
                Dim rsta As New RequestStatusTableAdapter
                Dim csta As New ClientSiteTableAdapter

                cta.FillByRequestUid(result.Consultant, RequestUid)
                rsta.Fill(result.RequestStatus)
                csta.FillByRequestUid(result.ClientSite, RequestUid)

                Return result
            Else
                Return Nothing
            End If

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting request details", "Request", RequestUid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetNewRequestData(ByVal deviceId As String, ByVal ConsultantUid As Guid) As activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing

            Dim result As New activiserDataSet
            result.EnforceConstraints = False
            Dim cta As New ConsultantTableAdapter
            Dim rsta As New RequestStatusTableAdapter
            Dim csta As New ClientSiteTableAdapter

            Dim currentConsultants As Utility.ActiveConsultantsDataTable = (New UtilityTableAdapters.ActiveConsultantsTableAdapter).GetData()
            result.Consultant.Merge(currentConsultants)
            ' cta.FillForClient(result.Consultant)
            csta.FillEssentialDataForActiveClients(result.ClientSite)
            rsta.Fill(result.RequestStatus)

            Return result

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting new request data", "Request", Guid.Empty, "", ex)
            Return Nothing
        End Try
    End Function


    ' Function that returns the job history for a particular site
    ' jobCount parameter specifies the number of records to return.
    <WebMethod()> _
    Public Function GetJobHistory(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal ClientSiteUid As Guid, ByVal jobLimit As Integer, ByVal ageLimitDays As Integer) As DataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim jh As New activiserDataSet
            jh.EnforceConstraints = False
            Dim jhjta As New JobTableAdapter
            Dim jhrta As New RequestTableAdapter

            jhrta.FillClientSiteHistoryForConsultant(jh.Request, SchemaClientMask.Client, deviceId, ConsultantUid, ConsultantUid, ClientSiteUid, ageLimitDays, jobLimit)
            For Each rr As activiserDataSet.RequestRow In jh.Request
                DataSchema.FillClientDataSet(jh, SchemaClientMask.ClientPublic, deviceId, ConsultantUid, jh.Request.TableName, rr.RequestUID)
            Next
            jhjta.FillClientSiteHistoryForConsultant(jh.Job, SchemaClientMask.Client, deviceId, ConsultantUid, ConsultantUid, ClientSiteUid, ageLimitDays, jobLimit)
            For Each jr As activiserDataSet.JobRow In jh.Job
                DataSchema.FillClientDataSet(jh, SchemaClientMask.ClientPublic, deviceId, ConsultantUid, jh.Job.TableName, jr.JobUID)
            Next

            Return jh
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting job history", "ClientSite", ClientSiteUid, "", ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetJobDetails(ByVal deviceId As String, ByVal UserUid As Guid, ByVal JobUid As Guid) As activiserDataSet
        Try
            If Not CheckDeviceId(deviceId) Then Return Nothing
            'If Not My.User.IsAuthenticated Then
            '    Throw New UnauthorizedAccessException("Anonymous access to restricted function denied")
            'End If

            Dim result As New activiserDataSet
            result.EnforceConstraints = False
            Dim jta As New JobTableAdapter
            If jta.FillByJobUid(result.Job, JobUid) = 1 Then
                Dim job As activiserDataSet.JobRow = result.Job(0)
                Dim rta As New RequestTableAdapter
                rta.FillByRequestUid(result.Request, job.RequestUID)
                If result.Request.Count = 1 Then
                    Dim r As activiserDataSet.RequestRow = result.Request(0)
                    Dim sta As New ClientSiteTableAdapter
                    sta.FillByClientSiteUid(result.ClientSite, r.ClientSiteUID)
                End If

                Dim cta As New ConsultantTableAdapter
                cta.FillByConsultantUid(result.Consultant, job.ConsultantUID)
                DataSchema.FillClientDataSet(result, -1, deviceId, UserUid, result.Job.TableName, JobUid)
                Return result
            Else
                Return Nothing
            End If

        Catch ex As Exception
            LogError(UserUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting client site details", "Job", JobUid, "", ex)
            Return Nothing
        End Try
    End Function
#End Region


End Class
