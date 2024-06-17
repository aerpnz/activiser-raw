Module JobStatusCodeModule
    Public Structure wsJobStatusCodes
        Public ReadOnly Draft As Integer
        Public ReadOnly Complete As Integer
        Public ReadOnly Signed As Integer
        Public ReadOnly CompleteSynchronised As Integer
        Public ReadOnly SignedSynchronised As Integer
        Public ReadOnly History As Integer
        Public ReadOnly StatusChange As Integer
        Public ReadOnly Deleted As Integer
        Public ReadOnly OtherUser As Integer

        Public Sub New(ByVal draft As Integer, ByVal complete As Integer, ByVal signed As Integer, ByVal completeSynchronised As Integer, ByVal signedSynchronised As Integer, ByVal history As Integer, ByVal statusChange As Integer, ByVal deleted As Integer, ByVal otherUser As Integer)
            Me.Draft = draft
            Me.Complete = complete
            Me.Signed = signed
            Me.CompleteSynchronised = completeSynchronised
            Me.SignedSynchronised = signedSynchronised
            Me.History = history
            Me.StatusChange = statusChange
            Me.Deleted = deleted
            Me.OtherUser = otherUser
        End Sub
    End Structure

    Public JobStatusCodes As New wsJobStatusCodes( _
        AppConfig.GetSetting("JobStatusDraft", 0), _
        AppConfig.GetSetting("JobStatusComplete", 1), _
        AppConfig.GetSetting("JobStatusSigned", 2), _
        AppConfig.GetSetting("JobStatusCompleteSynchronised", 3), _
        AppConfig.GetSetting("JobStatusSignedSynchronised", 4), _
        AppConfig.GetSetting("JobStatusHistory", 5), _
        AppConfig.GetSetting("JobStatusRequestStatusChange", 6), _
        AppConfig.GetSetting("JobStatusDeleted", 7), _
        AppConfig.GetSetting("JobStatusOtherUser", -1) _
        )

    Public JobStatusColors() As Color = { _
        AppConfig.GetSetting("JobStatusDraftColor", Color.Red), _
        AppConfig.GetSetting("JobStatusCompleteColor", Color.Gold), _
        AppConfig.GetSetting("JobStatusSignedColor", Color.LawnGreen), _
        AppConfig.GetSetting("JobStatusCompleteSynchronisedColor", Color.Goldenrod), _
        AppConfig.GetSetting("JobStatusSignedSynchronisedColor", Color.Green), _
        AppConfig.GetSetting("JobStatusHistoryColor", Color.Gray), _
        AppConfig.GetSetting("JobStatusRequestStatusChangeColor", Color.Chocolate), _
        AppConfig.GetSetting("JobStatusDeletedColor", Color.Salmon), _
        AppConfig.GetSetting("JobStatusOtherUserColor", Color.DarkRed) _
    }

    Public Function GetStatusColor(ByVal statusID As Integer) As Color
        Dim c As Color
        Select Case statusID
            Case JobStatusCodes.Draft : c = JobStatusColors(0)
            Case JobStatusCodes.Complete : c = JobStatusColors(1)
            Case JobStatusCodes.Signed : c = JobStatusColors(2)
            Case JobStatusCodes.CompleteSynchronised : c = JobStatusColors(3)
            Case JobStatusCodes.SignedSynchronised : c = JobStatusColors(4)
            Case JobStatusCodes.History : c = JobStatusColors(5)
            Case JobStatusCodes.StatusChange : c = JobStatusColors(6)
            Case JobStatusCodes.Deleted : c = JobStatusColors(7)
            Case JobStatusCodes.OtherUser : c = JobStatusColors(8)
        End Select
        Return c
    End Function

    'Public Sub LoadJobStatusCodes()
    '    JobStatusCodes = New wsJobStatusCodes(ConfigurationSettings.GetIntegerValue("JobStatusDraft", 0), ConfigurationSettings.GetIntegerValue("JobStatusComplete", 1), ConfigurationSettings.GetIntegerValue("JobStatusSigned ", 2), ConfigurationSettings.GetIntegerValue("JobStatusCompleteSynchronised", 3), ConfigurationSettings.GetIntegerValue("JobStatusSignedSynchronised", 4), ConfigurationSettings.GetIntegerValue("JobStatusHistory ", 5), ConfigurationSettings.GetIntegerValue("JobStatusRequestStatusChange", 6), ConfigurationSettings.GetIntegerValue("JobStatusDeleted", 7))
    'End Sub
End Module
