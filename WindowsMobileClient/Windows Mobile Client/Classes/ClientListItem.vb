Friend Class ClientListItem
    Public ClientSiteUID As Guid
    Public SiteName As String
    Public Sub New(ByVal UID As Guid, ByVal Name As String)
        Me.ClientSiteUID = UID
        Me.SiteName = Name
    End Sub

    Public Overrides Function ToString() As String
        Return SiteName
    End Function
End Class
