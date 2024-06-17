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
        ConfigurationSettings.GetValue("JobStatusDraft", 0), _
        ConfigurationSettings.GetValue("JobStatusComplete", 1), _
        ConfigurationSettings.GetValue("JobStatusSigned", 2), _
        ConfigurationSettings.GetValue("JobStatusCompleteSynchronised", 3), _
        ConfigurationSettings.GetValue("JobStatusSignedSynchronised", 4), _
        ConfigurationSettings.GetValue("JobStatusHistory", 5), _
        ConfigurationSettings.GetValue("JobStatusRequestStatusChange", 6), _
        ConfigurationSettings.GetValue("JobStatusDeleted", 7), _
        ConfigurationSettings.GetValue("JobStatusOtherUser", -1) _
        )

    Public JobStatusColors() As Color = { _
        ConfigurationSettings.GetValue("JobStatusDraftColor", Color.Red), _
        ConfigurationSettings.GetValue("JobStatusCompleteColor", Color.Gold), _
        ConfigurationSettings.GetValue("JobStatusSignedColor", Color.LawnGreen), _
        ConfigurationSettings.GetValue("JobStatusCompleteSynchronisedColor", Color.Goldenrod), _
        ConfigurationSettings.GetValue("JobStatusSignedSynchronisedColor", Color.Green), _
        ConfigurationSettings.GetValue("JobStatusHistoryColor", Color.Gray), _
        ConfigurationSettings.GetValue("JobStatusRequestStatusChangeColor", Color.Chocolate), _
        ConfigurationSettings.GetValue("JobStatusDeletedColor", Color.Salmon), _
        ConfigurationSettings.GetValue("JobStatusOtherUserColor", Color.DarkRed) _
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
