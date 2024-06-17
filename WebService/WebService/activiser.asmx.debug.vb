Imports System.Data
Imports activiser.WebService.Utilities

#If DEBUG Then

Imports activiser
Imports System
Imports System.Diagnostics
Imports System.Web.Services

Partial Public Class activiserClientWebService
    <WebMethod()> _
    Public Function DebugGetClientDataSetSchema(ByVal deviceId As String, ByVal ConsultantUid As String) As DataSet
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing

        Return GetClientDataSetSchema(deviceId, consultantGuid)
    End Function

    <WebMethod()> _
    Public Function DebugGetClientDataSetUpdates(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal Since As String) As DataSet
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing
        Dim modifiedSinceDT As DateTime = DateTime.Parse(Since)

        Return GetClientDataSetUpdates(deviceId, consultantGuid, modifiedSinceDT)
    End Function

    ' This function takes any requests and jobs the consultant has created or modified, and enters them
    ' into the database. A dataset is returned which confirms that the database has been updated, and
    ' the dataset contains a Checksum string which will be used on the Pocket PC, to check against the data.
    <WebMethod()> _
    Public Function DebugUploadClientDataSetUpdates(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal activiserDataSet As String, ByVal CustomDataSet As String) As UploadResults
        Try
            Dim srpda As New IO.StringReader(activiserDataSet)
            Dim xrpda As New Xml.XmlTextReader(srpda)
            Dim dsPDA As New activiserDataSet '("PDADataSet")
            dsPDA.EnforceConstraints = False
            dsPDA.ReadXml(xrpda)

            Dim srcustom As New IO.StringReader(CustomDataSet)
            Dim xrcustom As New Xml.XmlTextReader(srcustom)
            Dim dsCustom As New DataSet("CustomData")
            If Not String.IsNullOrEmpty(CustomDataSet) Then
                dsCustom.ReadXml(xrcustom)
            End If

            Dim consultantGuid As Guid = New Guid(ConsultantUid)
            Return UploadClientDataSetUpdates(deviceId, consultantGuid, dsPDA)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function DebugUploadClientDataSetUpdatesAsXml(ByVal deviceId As String, ByVal consultantUid As String, ByVal clientDataXml As String) As Xml.XmlNode
        Dim clientData As New Xml.XmlDocument()
        clientData.LoadXml(clientDataXml)
        Return UploadClientDataSetUpdatesAsXml(deviceId, consultantUid, clientData)
    End Function

    <WebMethod()> _
    Public Function DebugGetSchemaMasked(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal clientMask As Integer) As activiserDataSet
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing
        Return GetClientDataSetSchemaMasked(deviceId, consultantGuid, clientMask)
    End Function

    <WebMethod()> _
    Public Function DebugGetSchema(ByVal deviceId As String, ByVal ConsultantUid As String) As activiserDataSet
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing
        Return GetClientDataSetSchema(deviceId, consultantGuid)
    End Function

    <WebMethod()> _
    Public Function DebugGetClientDataSet(ByVal deviceId As String, ByVal ConsultantUid As String) As activiserDataSet
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing
        Return GetClientDataSet(deviceId, consultantGuid)
    End Function

    <WebMethod()> _
    Public Function DebugGetClientDataSetAsXml(ByVal deviceId As String, ByVal ConsultantUid As String) As Xml.XmlNode
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing
        Return GetClientDataSetAsXml(deviceId, consultantGuid)
    End Function

    <WebMethod()> _
    Public Function DebugGetClientDataSetUpdatesAsXml(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal ModifiedSince As String) As Xml.XmlNode
        Dim consultantGuid As Guid
        If Not IsGuid(ConsultantUid, consultantGuid) Then Return Nothing
        Dim modifiedSinceDT As DateTime = DateTime.Parse(ModifiedSince)
        Return GetClientDataSetUpdatesAsXml(deviceId, ConsultantUid, modifiedSinceDT)
    End Function

    <WebMethod()> _
    Public Function DebugGetClientSiteDetails(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal clientSiteUid As String) As Xml.XmlNode
        Dim clientSiteGuid As Guid = New Guid(clientSiteUid)
        Dim consultantGuid As Guid = New Guid(ConsultantUid)
        Return SerializeDataSetToXmlDoc(GetClientSiteDetails(deviceId, consultantGuid, clientSiteGuid))
    End Function

    <WebMethod()> _
Public Function DebugGetClientSiteDetails2(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal clientSiteUid As String) As String
        Dim clientSiteGuid As Guid = New Guid(clientSiteUid)
        Dim consultantGuid As Guid = New Guid(ConsultantUid)
        Return GetClientSiteDetails(deviceId, consultantGuid, clientSiteGuid).GetXml()
    End Function

    <WebMethod()> _
    Public Function DebugGetConsultantDetails(ByVal deviceId As String, ByVal UserUid As String, ByVal ConsultantUid As String) As activiserDataSet
        Dim ConsultantGuid As Guid = New Guid(ConsultantUid)
        Dim UserGuid As Guid = New Guid(UserUid)
        Return GetConsultantDetails(deviceId, UserGuid, ConsultantGuid)
    End Function

    <WebMethod()> _
    Public Function DebugGetCustomDataSchema() As activiserDataSet
        Return GetClientDataSetSchema("42", Guid.Empty)
    End Function

    <WebMethod()> _
    Public Function DebugGetRequestDetails(ByVal deviceId As String, ByVal UserUid As String, ByVal requestUid As String) As DataSet
        Dim requestGuid As Guid = New Guid(requestUid)
        Dim UserGuid As Guid = New Guid(UserUid)
        Return GetRequestDetails(deviceId, UserGuid, requestGuid)
    End Function

    '<WebMethod()> _
    'Public Function DebugGetJobDetails(ByVal deviceId As String, ByVal UserUid As String, ByVal JobUid As String) As DataSet
    '    Dim JobGuid As Guid = New Guid(JobUid)
    '    Dim UserGuid As Guid = New Guid(UserUid)
    '    Return GetJobDetails(DeviceId, UserGuid, JobGuid)
    'End Function


    <WebMethod()> _
    Public Function DebugGetJobHistory(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal clientSiteUid As String, ByVal jobLimit As Integer, ByVal ageLimitDays As Integer) As DataSet
        Dim consultantGuid As Guid = New Guid(ConsultantUid)
        Dim clientSiteGuid As Guid = New Guid(clientSiteUid)
        Return GetJobHistory(deviceId, consultantGuid, clientSiteGuid, jobLimit, ageLimitDays)
    End Function

    <WebMethod()> _
    Public Function DebugGetTerminology(ByVal deviceId As String, ByVal ConsultantUid As String, ByVal ClientKey As Integer, ByVal LanguageId As Integer, ByVal ModifiedSince As DateTime) As DataSet
        Dim consultantGuid As Guid = New Guid(ConsultantUid)
        Return GetTerminology(deviceId, consultantGuid, ClientKey, LanguageId, ModifiedSince)
    End Function

    <WebMethod()> _
    Public Function DebugConsoleLogonNetworkUser(ByVal deviceid As String) As String
        If Not CheckDeviceId(deviceid) Then Return Nothing
        Try
            Dim cuta As New UtilityTableAdapters.UserTableAdapter
            Dim users As New Utility 'Me.User.Identity.Name
            cuta.Connection = New SqlClient.SqlConnection(My.Settings.activiserConnectionString)

            cuta.FillByDomainLogon(users.User, Me.User.Identity.Name)
            If users IsNot Nothing AndAlso users.User.Count = 1 Then
                Return users.GetXml
            Else
                cuta.Fill(users.User)
                Dim result As String
                result = Me.User.Identity.Name & vbNewLine & users.GetXml
                Return result
            End If
        Catch ex As Exception
            Dim ep As New ExceptionParser(ex)
            Return Me.User.Identity.Name & vbNewLine & ep.ToString()
        End Try
    End Function

End Class

#End If