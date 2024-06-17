Public Class LoginForm
    Const MODULENAME As String = "LoginForm"

    Private Sub Login_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        If Not TryLogon(Me.ServerUrlTextBox.Text, Me.IntegratedAuthenticationCheckBox.Checked, Me.activiserUserNameTextBox.Text, Me.activiserPasswordTextBox.Text, Me.LinkUserCheckbox.Checked, Me) Then
            Me.activiserUserNameTextBox.Focus()
            Return
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub LoginForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.OK Then
            My.Settings.activiserServerUrl = Me.ServerUrlTextBox.Text
            My.Settings.ActiviserUserName = Me.activiserUserNameTextBox.Text
            userRegistryBase.SetValue(My.Resources.RegistryServerUrlValueName, Me.ServerUrlTextBox.Text)
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim machineRegistryServerUrl As String
        Dim userRegistryServerUrl As String

        userRegistryServerUrl = TryCast(userRegistryBase.GetValue(My.Resources.RegistryServerUrlValueName), String)
        if machineRegistryBase isnot nothing then 
            machineRegistryServerUrl = TryCast(machineRegistryBase.GetValue(My.Resources.RegistryServerUrlValueName), String)
        Else
            machineRegistryServerUrl = Nothing
        End If
        If String.IsNullOrEmpty(userRegistryServerUrl) AndAlso Not String.IsNullOrEmpty(machineRegistryServerUrl) Then
            My.Settings.activiserServerUrl = machineRegistryServerUrl
            Me.ServerUrlTextBox.Text = My.Settings.activiserServerUrl
        End If
        Me.BringToFront()
        Me.TopMost = True
        Me.activiserUserNameTextBox.Text = My.Settings.ActiviserUserName
        Me.IgnoreCertificateErrorsCheckBox.Enabled = Me.ServerUrlTextBox.Text.StartsWith("https://", StringComparison.Ordinal)
    End Sub

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "Mouse effects"

    Private _mouseDown As Boolean
    Private _mouseDownLocation As Point
    Private Sub Caption_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LogoPanel.MouseDown
        _mouseDown = True
        _mouseDownLocation = e.Location
    End Sub

    Private Sub Caption_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LogoPanel.MouseMove
        If _mouseDown Then
            Me.Location = New Point(Me.Location.X + (-_mouseDownLocation.X + e.X), Me.Location.Y + (-_mouseDownLocation.Y + e.Y))
        End If
    End Sub

    Private Sub Caption_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LogoPanel.MouseUp
        _mouseDown = False
    End Sub

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CloseButton.MouseDown, LinkUserCheckbox.MouseDown, ServerUrlTestButton.MouseDown, LoginButton.MouseDown
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.MidnightBlue
            l.ForeColor = Color.Transparent
        End If
    End Sub

    Private Sub Button_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CloseButton.MouseLeave, LinkUserCheckbox.MouseLeave, ServerUrlTestButton.MouseLeave, LoginButton.MouseLeave
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.Transparent
            l.ForeColor = Color.MidnightBlue
        End If
    End Sub

    Private Sub Button_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CloseButton.MouseUp, LinkUserCheckbox.MouseUp, ServerUrlTestButton.MouseUp, LoginButton.MouseUp
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.Transparent
            l.ForeColor = Color.MidnightBlue
        End If
    End Sub

    Private Sub Button_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.MouseHover, LinkUserCheckbox.MouseHover, ServerUrlTestButton.MouseHover, LoginButton.MouseHover
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.DodgerBlue
            l.ForeColor = Color.Transparent
        End If
    End Sub
#End Region

    Private Sub pnlTop_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoPanel.Resize
        Me.CloseButton.Location = New Point(Me.LogoPanel.Width - Me.CloseButton.Width, 0)
    End Sub

    Private Sub ServerUrlTestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerUrlTestButton.Click
        InteractiveUrlTest(Me.ServerUrlTextBox.Text, Me)
    End Sub

    Private Sub LoginForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub ServerUrlTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerUrlTextBox.TextChanged
        Me.IgnoreCertificateErrorsCheckBox.Enabled = Me.ServerUrlTextBox.Text.StartsWith("https://", StringComparison.Ordinal)
    End Sub

    Private Sub IgnoreCertificateErrorsCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IgnoreCertificateErrorsCheckBox.CheckedChanged
        If Not My.Settings.IgnoreServerCertificateErrors = Me.IgnoreCertificateErrorsCheckBox.Checked Then My.Settings.IgnoreServerCertificateErrors = Me.IgnoreCertificateErrorsCheckBox.Checked
    End Sub

    Private Sub IntegratedAuthenticationCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IntegratedAuthenticationCheckBox.CheckedChanged
        Me.activiserUserNameTextBox.ReadOnly = Me.IntegratedAuthenticationCheckBox.Checked
        Me.activiserPasswordTextBox.ReadOnly = Me.IntegratedAuthenticationCheckBox.Checked
    End Sub
End Class
