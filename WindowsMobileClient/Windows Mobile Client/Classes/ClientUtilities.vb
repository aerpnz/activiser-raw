Imports activiser.Library
Imports activiser.Library.WebService
Imports activiser.Terminology
Imports activiser.Library.WebService.activiserDataSet

Module ClientUtilities
    Const MODULENAME As String = "ClientUtilities"

    Public Function GetClientStatus(ByVal client As ClientSiteRow) As ClientSiteStatusRow
        If client Is Nothing Then Return Nothing

        If client.ClientSiteStatusRow IsNot Nothing Then Return client.ClientSiteStatusRow

        If client.IsClientSiteStatusIDNull Then Return Nothing

        Return gClientDataSet.ClientSiteStatus.FindByClientSiteStatusID(client.ClientSiteStatusID)
    End Function

    Public Function IsClientActive(ByVal client As ClientSiteRow) As Boolean
        If client Is Nothing Then Return False
        If client.Hold Then Return False
        If client.ClientSiteStatusRow IsNot Nothing Then Return client.ClientSiteStatusRow.IsActive

        If client.IsClientSiteStatusIDNull Then Return False

        Dim csr As ClientSiteStatusRow = gClientDataSet.ClientSiteStatus.FindByClientSiteStatusID(client.ClientSiteStatusID)
        If csr IsNot Nothing Then Return csr.IsActive
        Return False
    End Function

    Public Function IsClientInactiveOrOnHold(ByVal client As ClientSiteRow) As Boolean
        If client Is Nothing Then Return True
        If client.Hold Then Return True
        If client.ClientSiteStatusRow IsNot Nothing Then Return Not client.ClientSiteStatusRow.IsActive

        If client.IsClientSiteStatusIDNull Then Return True

        Dim csr As ClientSiteStatusRow = gClientDataSet.ClientSiteStatus.FindByClientSiteStatusID(client.ClientSiteStatusID)
        If csr IsNot Nothing Then Return Not csr.IsActive
        Return True
    End Function

    Public Function NewRequestsAllowed(ByVal client As ClientSiteRow) As Boolean
        If client Is Nothing Then Return False
        If gAllowRequestsForClientsOnHold Then Return True
        Return IsClientActive(client)
    End Function

    Public Function AllowNewRequest(ByVal owner As Form, ByVal client As ClientSiteRow) As Boolean
        If client Is Nothing Then Return False

        If IsClientInactiveOrOnHold(client) Then
            If gAllowRequestsForClientsOnHold Then
                Return AskQuestion(owner, MODULENAME, RES_ClientOnHoldMessage, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes
            Else
                DisplayMessage(owner, MODULENAME, RES_ClientOnHoldBlockedMessage, MessageBoxIcon.Hand)
                Return False
            End If
        Else
            Return True
        End If
    End Function

    'TODO
    Public Function CleanupClients() As Integer
        'Dim cq = From cr As ClientSiteRow In gClientDataSet.ClientSite.Cast(Of ClientSiteRow) Where cr.GetRequestRows.Count = 0



    End Function
End Module
