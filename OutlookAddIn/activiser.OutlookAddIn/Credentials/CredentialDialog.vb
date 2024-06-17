Public Class CredentialDialog
    Const MODULENAME As String = "CredentialDialog"

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Public Property Username() As String
        Get
            Return Me.UserNameBox.Text
        End Get
        Set(ByVal value As String)
            Me.UserNameBox.Text = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me.PasswordBox.Text
        End Get
        Set(ByVal value As String)
            Me.PasswordBox.Text = value
        End Set
    End Property

    Public Property DomainName() As String
        Get
            Return Me.DomainNameBox.Text
        End Get
        Set(ByVal value As String)
            Me.DomainNameBox.Text = value
        End Set
    End Property

    Public Property SavePassword() As Boolean
        Get
            Return Me.SavePasswordCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            Me.SavePasswordCheckBox.Checked = value
        End Set
    End Property

    Public Sub New(ByVal username As String, ByVal password As String, ByVal domainname As String)
        Me.InitializeComponent()
        Me.Username = username
        Me.Password = password
        Me.DomainName = domainname
    End Sub

    Private Sub ContextMenuPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuPaste.Click
        UIEventHandlers.ContextMenuPaste_Click(sender, e)
    End Sub

    Private Sub ContextMenuCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuCopy.Click
        UIEventHandlers.ContextMenuCopy_Click(sender, e)
    End Sub

    Private Sub ContextMenuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuClear.Click
        UIEventHandlers.ContextMenuDelete_Click(sender, e)
    End Sub

    Private Sub ContextMenuCut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuCut.Click
        UIEventHandlers.ContextMenuCut_Click(sender, e)
    End Sub

    Private Sub ContextMenuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuSelectAll.Click
        UIEventHandlers.ContextMenuSelectAll_Click(sender, e)
    End Sub
End Class
