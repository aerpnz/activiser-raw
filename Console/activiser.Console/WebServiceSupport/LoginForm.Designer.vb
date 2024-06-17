<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.LoginButton = New System.Windows.Forms.Button
        Me.ActiviserPasswordLabel = New System.Windows.Forms.Label
        Me.LinkUserCheckbox = New System.Windows.Forms.CheckBox
        Me.LogoPanel = New System.Windows.Forms.Panel
        Me.CloseButton = New System.Windows.Forms.Label
        Me.UserDetailsPanel = New System.Windows.Forms.Panel
        Me.IntegratedAuthenticationCheckBox = New System.Windows.Forms.CheckBox
        Me.AdvancedOptionsGroup = New System.Windows.Forms.GroupBox
        Me.IgnoreCertificateErrorsCheckBox = New System.Windows.Forms.CheckBox
        Me.ServerUrlTextBox = New System.Windows.Forms.TextBox
        Me.ServerUrlLabel = New System.Windows.Forms.Label
        Me.ServerUrlTestButton = New System.Windows.Forms.Button
        Me.activiserPasswordTextBox = New System.Windows.Forms.TextBox
        Me.activiserUserNameTextBox = New System.Windows.Forms.TextBox
        Me.ActiviserUsernameLabel = New System.Windows.Forms.Label
        Me.BorderPanel = New System.Windows.Forms.Panel
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.LogoPanel.SuspendLayout()
        Me.UserDetailsPanel.SuspendLayout()
        Me.AdvancedOptionsGroup.SuspendLayout()
        Me.BorderPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'LoginButton
        '
        resources.ApplyResources(Me.LoginButton, "LoginButton")
        Me.LoginButton.BackColor = System.Drawing.Color.GhostWhite
        Me.LoginButton.Image = Global.activiser.Console.My.Resources.Resources.users2
        Me.LoginButton.Name = "LoginButton"
        Me.ToolTipProvider.SetToolTip(Me.LoginButton, resources.GetString("LoginButton.ToolTip"))
        Me.LoginButton.UseVisualStyleBackColor = False
        '
        'ActiviserPasswordLabel
        '
        resources.ApplyResources(Me.ActiviserPasswordLabel, "ActiviserPasswordLabel")
        Me.ActiviserPasswordLabel.BackColor = System.Drawing.Color.Transparent
        Me.ActiviserPasswordLabel.Name = "ActiviserPasswordLabel"
        Me.ActiviserPasswordLabel.TabStop = True
        '
        'LinkUserCheckbox
        '
        resources.ApplyResources(Me.LinkUserCheckbox, "LinkUserCheckbox")
        Me.LinkUserCheckbox.Name = "LinkUserCheckbox"
        Me.ToolTipProvider.SetToolTip(Me.LinkUserCheckbox, resources.GetString("LinkUserCheckbox.ToolTip"))
        Me.LinkUserCheckbox.UseVisualStyleBackColor = True
        '
        'LogoPanel
        '
        Me.LogoPanel.BackColor = System.Drawing.Color.White
        Me.LogoPanel.BackgroundImage = Global.activiser.Console.My.Resources.Resources.SplashLogo
        resources.ApplyResources(Me.LogoPanel, "LogoPanel")
        Me.LogoPanel.Controls.Add(Me.CloseButton)
        Me.LogoPanel.Name = "LogoPanel"
        '
        'CloseButton
        '
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseButton.ForeColor = System.Drawing.Color.MidnightBlue
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.UseMnemonic = False
        '
        'UserDetailsPanel
        '
        Me.UserDetailsPanel.BackColor = System.Drawing.Color.White
        Me.UserDetailsPanel.Controls.Add(Me.IntegratedAuthenticationCheckBox)
        Me.UserDetailsPanel.Controls.Add(Me.AdvancedOptionsGroup)
        Me.UserDetailsPanel.Controls.Add(Me.activiserPasswordTextBox)
        Me.UserDetailsPanel.Controls.Add(Me.LoginButton)
        Me.UserDetailsPanel.Controls.Add(Me.activiserUserNameTextBox)
        Me.UserDetailsPanel.Controls.Add(Me.ActiviserUsernameLabel)
        Me.UserDetailsPanel.Controls.Add(Me.ActiviserPasswordLabel)
        resources.ApplyResources(Me.UserDetailsPanel, "UserDetailsPanel")
        Me.UserDetailsPanel.Name = "UserDetailsPanel"
        '
        'IntegratedAuthenticationCheckBox
        '
        resources.ApplyResources(Me.IntegratedAuthenticationCheckBox, "IntegratedAuthenticationCheckBox")
        Me.IntegratedAuthenticationCheckBox.Checked = Global.activiser.Console.My.MySettings.Default.AutoDomainLogon
        Me.IntegratedAuthenticationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IntegratedAuthenticationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.Console.My.MySettings.Default, "AutoDomainLogon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.IntegratedAuthenticationCheckBox.Name = "IntegratedAuthenticationCheckBox"
        Me.IntegratedAuthenticationCheckBox.UseVisualStyleBackColor = True
        '
        'AdvancedOptionsGroup
        '
        Me.AdvancedOptionsGroup.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AdvancedOptionsGroup.Controls.Add(Me.IgnoreCertificateErrorsCheckBox)
        Me.AdvancedOptionsGroup.Controls.Add(Me.ServerUrlTextBox)
        Me.AdvancedOptionsGroup.Controls.Add(Me.LinkUserCheckbox)
        Me.AdvancedOptionsGroup.Controls.Add(Me.ServerUrlLabel)
        Me.AdvancedOptionsGroup.Controls.Add(Me.ServerUrlTestButton)
        resources.ApplyResources(Me.AdvancedOptionsGroup, "AdvancedOptionsGroup")
        Me.AdvancedOptionsGroup.Name = "AdvancedOptionsGroup"
        Me.AdvancedOptionsGroup.TabStop = False
        '
        'IgnoreCertificateErrorsCheckBox
        '
        Me.IgnoreCertificateErrorsCheckBox.Checked = Global.activiser.Console.My.MySettings.Default.IgnoreServerCertificateErrors
        Me.IgnoreCertificateErrorsCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.Console.My.MySettings.Default, "IgnoreServerCertificateErrors", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.IgnoreCertificateErrorsCheckBox, "IgnoreCertificateErrorsCheckBox")
        Me.IgnoreCertificateErrorsCheckBox.Name = "IgnoreCertificateErrorsCheckBox"
        Me.ToolTipProvider.SetToolTip(Me.IgnoreCertificateErrorsCheckBox, resources.GetString("IgnoreCertificateErrorsCheckBox.ToolTip"))
        Me.IgnoreCertificateErrorsCheckBox.UseVisualStyleBackColor = True
        '
        'ServerUrlTextBox
        '
        Me.ServerUrlTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.activiser.Console.My.MySettings.Default, "activiserServerUrl", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.ServerUrlTextBox, "ServerUrlTextBox")
        Me.ServerUrlTextBox.Name = "ServerUrlTextBox"
        Me.ServerUrlTextBox.Text = Global.activiser.Console.My.MySettings.Default.activiserServerUrl
        Me.ToolTipProvider.SetToolTip(Me.ServerUrlTextBox, resources.GetString("ServerUrlTextBox.ToolTip"))
        '
        'ServerUrlLabel
        '
        resources.ApplyResources(Me.ServerUrlLabel, "ServerUrlLabel")
        Me.ServerUrlLabel.BackColor = System.Drawing.Color.Transparent
        Me.ServerUrlLabel.Name = "ServerUrlLabel"
        '
        'ServerUrlTestButton
        '
        resources.ApplyResources(Me.ServerUrlTestButton, "ServerUrlTestButton")
        Me.ServerUrlTestButton.BackColor = System.Drawing.Color.GhostWhite
        Me.ServerUrlTestButton.Image = Global.activiser.Console.My.Resources.Resources.TaskHS
        Me.ServerUrlTestButton.Name = "ServerUrlTestButton"
        Me.ToolTipProvider.SetToolTip(Me.ServerUrlTestButton, resources.GetString("ServerUrlTestButton.ToolTip"))
        Me.ServerUrlTestButton.UseVisualStyleBackColor = False
        '
        'activiserPasswordTextBox
        '
        resources.ApplyResources(Me.activiserPasswordTextBox, "activiserPasswordTextBox")
        Me.activiserPasswordTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "AutoDomainLogon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.activiserPasswordTextBox.Name = "activiserPasswordTextBox"
        Me.activiserPasswordTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.AutoDomainLogon
        Me.ToolTipProvider.SetToolTip(Me.activiserPasswordTextBox, resources.GetString("activiserPasswordTextBox.ToolTip"))
        Me.activiserPasswordTextBox.UseSystemPasswordChar = True
        '
        'activiserUserNameTextBox
        '
        resources.ApplyResources(Me.activiserUserNameTextBox, "activiserUserNameTextBox")
        Me.activiserUserNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "AutoDomainLogon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.activiserUserNameTextBox.Name = "activiserUserNameTextBox"
        Me.activiserUserNameTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.AutoDomainLogon
        Me.ToolTipProvider.SetToolTip(Me.activiserUserNameTextBox, resources.GetString("activiserUserNameTextBox.ToolTip"))
        '
        'ActiviserUsernameLabel
        '
        resources.ApplyResources(Me.ActiviserUsernameLabel, "ActiviserUsernameLabel")
        Me.ActiviserUsernameLabel.BackColor = System.Drawing.Color.Transparent
        Me.ActiviserUsernameLabel.Name = "ActiviserUsernameLabel"
        '
        'BorderPanel
        '
        resources.ApplyResources(Me.BorderPanel, "BorderPanel")
        Me.BorderPanel.BackColor = System.Drawing.Color.White
        Me.BorderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BorderPanel.Controls.Add(Me.UserDetailsPanel)
        Me.BorderPanel.Controls.Add(Me.LogoPanel)
        Me.BorderPanel.ForeColor = System.Drawing.Color.MidnightBlue
        Me.BorderPanel.Name = "BorderPanel"
        '
        'LoginForm
        '
        Me.AcceptButton = Me.LoginButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BorderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "LoginForm"
        Me.LogoPanel.ResumeLayout(False)
        Me.UserDetailsPanel.ResumeLayout(False)
        Me.UserDetailsPanel.PerformLayout()
        Me.AdvancedOptionsGroup.ResumeLayout(False)
        Me.AdvancedOptionsGroup.PerformLayout()
        Me.BorderPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LoginButton As System.Windows.Forms.Button
    Friend WithEvents activiserUserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ActiviserPasswordLabel As System.Windows.Forms.Label
    Friend WithEvents LinkUserCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents LogoPanel As System.Windows.Forms.Panel
    Friend WithEvents UserDetailsPanel As System.Windows.Forms.Panel
    Friend WithEvents activiserPasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ActiviserUsernameLabel As System.Windows.Forms.Label
    Friend WithEvents CloseButton As System.Windows.Forms.Label
    Friend WithEvents BorderPanel As System.Windows.Forms.Panel
    Friend WithEvents ServerUrlLabel As System.Windows.Forms.Label
    Friend WithEvents ServerUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ServerUrlTestButton As System.Windows.Forms.Button
    Friend WithEvents AdvancedOptionsGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents IgnoreCertificateErrorsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents IntegratedAuthenticationCheckBox As System.Windows.Forms.CheckBox
End Class
