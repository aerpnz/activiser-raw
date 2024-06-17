<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")> _
Public Structure ClientSiteDetails
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public ClientSiteUID As Guid
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public ClientSiteID As Integer
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public SiteName As String
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public SiteStatusID As Integer
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public SiteAddress As String
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public Contact As String
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public ContactPhone1 As String
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public ContactPhone2 As String
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public SiteContactEmail As String
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public SiteNotes As String
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public CreatedTime As DateTime
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public ModifiedTime As DateTime
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public Hold As Boolean

    Public Sub New(ByVal clientSiteUID As Guid, _
    ByVal clientSiteID As Integer, _
    ByVal siteName As String, _
    ByVal siteStatusID As Integer, _
    ByVal siteAddress As String, _
    ByVal contact As String, _
    ByVal contactPhone1 As String, _
    ByVal contactPhone2 As String, _
    ByVal siteContactEmail As String, _
    ByVal siteNotes As String, _
    ByVal modifiedTime As DateTime, _
    ByVal hold As Boolean)
        Me.ClientSiteUID = clientSiteUID
        Me.ClientSiteID = clientSiteID
        Me.SiteName = siteName
        Me.SiteStatusID = siteStatusID
        Me.SiteAddress = siteAddress
        Me.Contact = contact
        Me.ContactPhone1 = contactPhone1
        Me.ContactPhone2 = contactPhone2
        Me.SiteContactEmail = siteContactEmail
        Me.SiteNotes = siteNotes
        Me.ModifiedTime = modifiedTime
        Me.Hold = hold
    End Sub

    Public Sub New(ByVal clientSiteRow As ClientDataSet.ClientSiteRow)
        If clientSiteRow Is Nothing Then Throw New ArgumentNullException("clientSiteRow")
        Me.ClientSiteUID = clientSiteRow.ClientSiteUID
        Me.ClientSiteID = clientSiteRow.ClientSiteID
        Me.Contact = clientSiteRow.Contact
        Me.ContactPhone1 = clientSiteRow.ContactPhone1
        Me.ContactPhone2 = clientSiteRow.ContactPhone2
        Me.CreatedTime = clientSiteRow.CreatedDateTime
        Me.Hold = clientSiteRow.Hold
        Me.ModifiedTime = clientSiteRow.ModifiedDateTime
        Me.SiteAddress = clientSiteRow.SiteAddress
        Me.SiteContactEmail = clientSiteRow.SiteContactEmail
        Me.SiteName = clientSiteRow.SiteName
        Me.SiteNotes = clientSiteRow.SiteNotes
        Me.SiteStatusID = clientSiteRow.ClientSiteStatusID
    End Sub
End Structure
