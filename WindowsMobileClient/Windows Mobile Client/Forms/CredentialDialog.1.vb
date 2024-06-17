Public Class CredentialDialog
    Const MODULENAME As String = "CredentialDialog"

    'Private _controlledClose As Boolean
    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Public Property UserName() As String
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

    Public Sub New(ByVal owner As Form, ByVal userName As String, ByVal password As String, ByVal domainName As String)
        Me.InitializeComponent()
        Me.UserName = userName
        Me.Password = password
        Me.DomainName = domainName
        Me.Owner = owner
    End Sub

    ''' <summary>
    ''' Activiser Credentials Constructor.
    ''' </summary>
    ''' <param name="owner"></param>
    ''' <param name="username"></param>
    ''' <param name="password"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal owner As Form, ByVal userName As String, ByVal password As String)
        Me.InitializeComponent()
        Me.UserName = userName
        Me.UserNameBox.ReadOnly = True
        Me.Password = password
        Me.DomainLabel.Visible = False
        Me.DomainNameBox.Visible = False
        Me.SavePasswordCheckBox.Visible = False
        Me.Owner = owner
    End Sub

    Public Sub CloseDialog()
        '_controlledClose = True
        Me.Close()
    End Sub

#If WINDOWSMOBILE Then

    Private Sub CredentialDialog_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        InputPanelSwitch(True)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        Me.InputPanel.Dispose()
    End Sub

    Private Sub CredentialDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InputPanelSwitch(True)
    End Sub

    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        InputPanelSwitch(False)
    End Sub

    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        Const METHODNAME As String = "InputPanelSwitch"
        Try
            If loadInitial Then
                InputPanel.Enabled = AppConfig.GetSetting(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.Height > Me.Width)
            Else
                AppConfig.SaveSetting(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.InputPanel.Enabled)
            End If
            'Application.DoEvents()
            Me.Refresh()
        Catch ex As ObjectDisposedException
            ' who cares
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub
#End If


#If WINDOWSCE Then
    Private Sub CredentialDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EnableContextMenus(Me.Controls)
    End Sub
#End If

End Class
