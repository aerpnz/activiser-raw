Friend Structure NotificationInfo
    Public ConsultantUid As Guid
    Public ConsultantName As String
    Public SyncTime As DateTime

    Public Sub New(ByVal consultantUid As Guid, ByVal consultantName As String, ByVal syncTime As DateTime)
        Me.ConsultantName = consultantName
        Me.ConsultantUid = consultantUid
        Me.SyncTime = syncTime
    End Sub
End Structure
