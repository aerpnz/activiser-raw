<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class SetupForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetupForm))
        Me.ActiviserCredentialsHelp = New System.Windows.Forms.Label
        Me.FormMenu = New System.Windows.Forms.MainMenu
        Me.NextTestDoneButton = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.BackButton = New System.Windows.Forms.MenuItem
        Me.HttpTestButton = New System.Windows.Forms.MenuItem
        Me.ResetButton = New System.Windows.Forms.MenuItem
        Me.Cancel = New System.Windows.Forms.MenuItem
        Me.UseIntegratedAuthenticationCheckbox = New System.Windows.Forms.CheckBox
        Me.UserNameTextBox = New System.Windows.Forms.TextBox
        Me.EditContextMenu1 = New activiser.EditContextMenu
        Me.ActiviserCredentialsPanel = New System.Windows.Forms.Panel
        Me.PasswordTextBox = New System.Windows.Forms.TextBox
        Me.PasswordContextMenu1 = New activiser.PasswordContextMenu
        Me.UserNameLabel = New System.Windows.Forms.Label
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.ActiviserCredentialsLabel = New System.Windows.Forms.Label
        Me.ServiceCredentialsHelp = New System.Windows.Forms.Label
        Me.ActiviserUserTab = New System.Windows.Forms.TabPage
        Me.WebServiceCredentialsLabel = New System.Windows.Forms.Label
        Me.TestTab = New System.Windows.Forms.TabPage
        Me.UserInfoTextBox = New System.Windows.Forms.TextBox
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.DeviceIDPanel = New System.Windows.Forms.Panel
        Me.AuthenticationLabel = New System.Windows.Forms.Label
        Me.DeviceIDTextBox = New System.Windows.Forms.TextBox
        Me.DeviceIDLabel = New System.Windows.Forms.Label
        Me.NetUserTab = New System.Windows.Forms.TabPage
        Me.DomainUsernameTextBox = New System.Windows.Forms.TextBox
        Me.WebServiceUserNameLabel = New System.Windows.Forms.Label
        Me.SavePasswordCheckBox = New System.Windows.Forms.CheckBox
        Me.WebServicePasswordLabel = New System.Windows.Forms.Label
        Me.DomainNameTextBox = New System.Windows.Forms.TextBox
        Me.DomainLabel = New System.Windows.Forms.Label
        Me.DomainPasswordTextBox = New System.Windows.Forms.TextBox
        Me.UrlLabel = New System.Windows.Forms.Label
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.WebServiceTab = New System.Windows.Forms.TabPage
        Me.WebServiceCredentialsPanel = New System.Windows.Forms.Panel
        Me.ServerUrlHelp = New System.Windows.Forms.Label
        Me.IgnoreCertificateErrorsCheckbox = New System.Windows.Forms.CheckBox
        Me.SecondsLabel = New System.Windows.Forms.Label
        Me.ServerTimeoutLabel = New System.Windows.Forms.Label
        Me.ServerTimeoutValue = New System.Windows.Forms.NumericUpDown
        Me.ServerUrlTextBox = New System.Windows.Forms.TextBox
        Me.BrowserTab = New System.Windows.Forms.TabPage
        Me.WebBrowser = New System.Windows.Forms.WebBrowser
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.ActiviserCredentialsPanel.SuspendLayout()
        Me.ActiviserUserTab.SuspendLayout()
        Me.TestTab.SuspendLayout()
        Me.DeviceIDPanel.SuspendLayout()
        Me.NetUserTab.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.WebServiceTab.SuspendLayout()
        Me.WebServiceCredentialsPanel.SuspendLayout()
        Me.BrowserTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'ActiviserCredentialsHelp
        '
        resources.ApplyResources(Me.ActiviserCredentialsHelp, "ActiviserCredentialsHelp")
        Me.ActiviserCredentialsHelp.Name = "ActiviserCredentialsHelp"
        '
        'FormMenu
        '
        Me.FormMenu.MenuItems.Add(Me.NextTestDoneButton)
        Me.FormMenu.MenuItems.Add(Me.MenuItem1)
        '
        'NextTestDoneButton
        '
        resources.ApplyResources(Me.NextTestDoneButton, "NextTestDoneButton")
        '
        'MenuItem1
        '
        Me.MenuItem1.MenuItems.Add(Me.BackButton)
        Me.MenuItem1.MenuItems.Add(Me.HttpTestButton)
        Me.MenuItem1.MenuItems.Add(Me.ResetButton)
        Me.MenuItem1.MenuItems.Add(Me.Cancel)
        resources.ApplyResources(Me.MenuItem1, "MenuItem1")
        '
        'BackButton
        '
        resources.ApplyResources(Me.BackButton, "BackButton")
        '
        'HttpTestButton
        '
        resources.ApplyResources(Me.HttpTestButton, "HttpTestButton")
        '
        'ResetButton
        '
        resources.ApplyResources(Me.ResetButton, "ResetButton")
        '
        'Cancel
        '
        resources.ApplyResources(Me.Cancel, "Cancel")
        '
        'UseIntegratedAuthenticationCheckbox
        '
        resources.ApplyResources(Me.UseIntegratedAuthenticationCheckbox, "UseIntegratedAuthenticationCheckbox")
        Me.UseIntegratedAuthenticationCheckbox.Name = "UseIntegratedAuthenticationCheckbox"
        '
        'UserNameTextBox
        '
        resources.ApplyResources(Me.UserNameTextBox, "UserNameTextBox")
        Me.UserNameTextBox.ContextMenu = Me.EditContextMenu1
        Me.UserNameTextBox.Name = "UserNameTextBox"
        '
        'EditContextMenu1
        '
        'Me.EditContextMenu1.ShowCall = False
        '
        'ActiviserCredentialsPanel
        '
        Me.ActiviserCredentialsPanel.Controls.Add(Me.ActiviserCredentialsHelp)
        Me.ActiviserCredentialsPanel.Controls.Add(Me.UseIntegratedAuthenticationCheckbox)
        Me.ActiviserCredentialsPanel.Controls.Add(Me.UserNameTextBox)
        Me.ActiviserCredentialsPanel.Controls.Add(Me.PasswordTextBox)
        Me.ActiviserCredentialsPanel.Controls.Add(Me.UserNameLabel)
        Me.ActiviserCredentialsPanel.Controls.Add(Me.PasswordLabel)
        Me.ActiviserCredentialsPanel.Controls.Add(Me.ActiviserCredentialsLabel)
        resources.ApplyResources(Me.ActiviserCredentialsPanel, "ActiviserCredentialsPanel")
        Me.ActiviserCredentialsPanel.Name = "ActiviserCredentialsPanel"
        '
        'PasswordTextBox
        '
        resources.ApplyResources(Me.PasswordTextBox, "PasswordTextBox")
        Me.PasswordTextBox.ContextMenu = Me.PasswordContextMenu1
        Me.PasswordTextBox.Name = "PasswordTextBox"
        '
        'UserNameLabel
        '
        resources.ApplyResources(Me.UserNameLabel, "UserNameLabel")
        Me.UserNameLabel.Name = "UserNameLabel"
        '
        'PasswordLabel
        '
        resources.ApplyResources(Me.PasswordLabel, "PasswordLabel")
        Me.PasswordLabel.Name = "PasswordLabel"
        '
        'ActiviserCredentialsLabel
        '
        resources.ApplyResources(Me.ActiviserCredentialsLabel, "ActiviserCredentialsLabel")
        Me.ActiviserCredentialsLabel.Name = "ActiviserCredentialsLabel"
        '
        'ServiceCredentialsHelp
        '
        resources.ApplyResources(Me.ServiceCredentialsHelp, "ServiceCredentialsHelp")
        Me.ServiceCredentialsHelp.Name = "ServiceCredentialsHelp"
        '
        'ActiviserUserTab
        '
        Me.ActiviserUserTab.Controls.Add(Me.ActiviserCredentialsPanel)
        resources.ApplyResources(Me.ActiviserUserTab, "ActiviserUserTab")
        Me.ActiviserUserTab.Name = "ActiviserUserTab"
        '
        'WebServiceCredentialsLabel
        '
        resources.ApplyResources(Me.WebServiceCredentialsLabel, "WebServiceCredentialsLabel")
        Me.WebServiceCredentialsLabel.Name = "WebServiceCredentialsLabel"
        '
        'TestTab
        '
        Me.TestTab.Controls.Add(Me.UserInfoTextBox)
        Me.TestTab.Controls.Add(Me.Splitter1)
        Me.TestTab.Controls.Add(Me.DeviceIDPanel)
        resources.ApplyResources(Me.TestTab, "TestTab")
        Me.TestTab.Name = "TestTab"
        '
        'UserInfoTextBox
        '
        Me.UserInfoTextBox.BackColor = System.Drawing.SystemColors.Info
        Me.UserInfoTextBox.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.UserInfoTextBox, "UserInfoTextBox")
        Me.UserInfoTextBox.Name = "UserInfoTextBox"
        Me.UserInfoTextBox.ReadOnly = True
        '
        'ReadOnlyContextMenu1
        '
        'Me.ReadOnlyContextMenu1.ShowCall = False
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Name = "Splitter1"
        '
        'DeviceIDPanel
        '
        Me.DeviceIDPanel.BackColor = System.Drawing.SystemColors.Info
        Me.DeviceIDPanel.Controls.Add(Me.AuthenticationLabel)
        Me.DeviceIDPanel.Controls.Add(Me.DeviceIDTextBox)
        Me.DeviceIDPanel.Controls.Add(Me.DeviceIDLabel)
        resources.ApplyResources(Me.DeviceIDPanel, "DeviceIDPanel")
        Me.DeviceIDPanel.Name = "DeviceIDPanel"
        '
        'AuthenticationLabel
        '
        Me.AuthenticationLabel.BackColor = System.Drawing.SystemColors.Info
        Me.AuthenticationLabel.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.AuthenticationLabel, "AuthenticationLabel")
        Me.AuthenticationLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.AuthenticationLabel.Name = "AuthenticationLabel"
        '
        'DeviceIDTextBox
        '
        Me.DeviceIDTextBox.AcceptsReturn = True
        Me.DeviceIDTextBox.BackColor = System.Drawing.SystemColors.Info
        Me.DeviceIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DeviceIDTextBox.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.DeviceIDTextBox, "DeviceIDTextBox")
        Me.DeviceIDTextBox.Name = "DeviceIDTextBox"
        Me.DeviceIDTextBox.ReadOnly = True
        '
        'DeviceIDLabel
        '
        resources.ApplyResources(Me.DeviceIDLabel, "DeviceIDLabel")
        Me.DeviceIDLabel.Name = "DeviceIDLabel"
        '
        'NetUserTab
        '
        Me.NetUserTab.Controls.Add(Me.ServiceCredentialsHelp)
        Me.NetUserTab.Controls.Add(Me.WebServiceCredentialsLabel)
        Me.NetUserTab.Controls.Add(Me.DomainUsernameTextBox)
        Me.NetUserTab.Controls.Add(Me.WebServiceUserNameLabel)
        Me.NetUserTab.Controls.Add(Me.SavePasswordCheckBox)
        Me.NetUserTab.Controls.Add(Me.WebServicePasswordLabel)
        Me.NetUserTab.Controls.Add(Me.DomainNameTextBox)
        Me.NetUserTab.Controls.Add(Me.DomainLabel)
        Me.NetUserTab.Controls.Add(Me.DomainPasswordTextBox)
        resources.ApplyResources(Me.NetUserTab, "NetUserTab")
        Me.NetUserTab.Name = "NetUserTab"
        '
        'DomainUsernameTextBox
        '
        Me.DomainUsernameTextBox.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.DomainUsernameTextBox, "DomainUsernameTextBox")
        Me.DomainUsernameTextBox.Name = "DomainUsernameTextBox"
        '
        'WebServiceUserNameLabel
        '
        resources.ApplyResources(Me.WebServiceUserNameLabel, "WebServiceUserNameLabel")
        Me.WebServiceUserNameLabel.Name = "WebServiceUserNameLabel"
        '
        'SavePasswordCheckBox
        '
        resources.ApplyResources(Me.SavePasswordCheckBox, "SavePasswordCheckBox")
        Me.SavePasswordCheckBox.Name = "SavePasswordCheckBox"
        '
        'WebServicePasswordLabel
        '
        resources.ApplyResources(Me.WebServicePasswordLabel, "WebServicePasswordLabel")
        Me.WebServicePasswordLabel.Name = "WebServicePasswordLabel"
        '
        'DomainNameTextBox
        '
        Me.DomainNameTextBox.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.DomainNameTextBox, "DomainNameTextBox")
        Me.DomainNameTextBox.Name = "DomainNameTextBox"
        '
        'DomainLabel
        '
        resources.ApplyResources(Me.DomainLabel, "DomainLabel")
        Me.DomainLabel.Name = "DomainLabel"
        '
        'DomainPasswordTextBox
        '
        Me.DomainPasswordTextBox.ContextMenu = Me.PasswordContextMenu1
        resources.ApplyResources(Me.DomainPasswordTextBox, "DomainPasswordTextBox")
        Me.DomainPasswordTextBox.Name = "DomainPasswordTextBox"
        '
        'UrlLabel
        '
        resources.ApplyResources(Me.UrlLabel, "UrlLabel")
        Me.UrlLabel.Name = "UrlLabel"
        '
        'InputPanel
        '
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.WebServiceTab)
        Me.TabControl1.Controls.Add(Me.NetUserTab)
        Me.TabControl1.Controls.Add(Me.ActiviserUserTab)
        Me.TabControl1.Controls.Add(Me.TestTab)
        Me.TabControl1.Controls.Add(Me.BrowserTab)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'WebServiceTab
        '
        Me.WebServiceTab.Controls.Add(Me.WebServiceCredentialsPanel)
        resources.ApplyResources(Me.WebServiceTab, "WebServiceTab")
        Me.WebServiceTab.Name = "WebServiceTab"
        '
        'WebServiceCredentialsPanel
        '
        Me.WebServiceCredentialsPanel.Controls.Add(Me.ServerUrlHelp)
        Me.WebServiceCredentialsPanel.Controls.Add(Me.IgnoreCertificateErrorsCheckbox)
        Me.WebServiceCredentialsPanel.Controls.Add(Me.SecondsLabel)
        Me.WebServiceCredentialsPanel.Controls.Add(Me.ServerTimeoutLabel)
        Me.WebServiceCredentialsPanel.Controls.Add(Me.ServerTimeoutValue)
        Me.WebServiceCredentialsPanel.Controls.Add(Me.ServerUrlTextBox)
        Me.WebServiceCredentialsPanel.Controls.Add(Me.UrlLabel)
        resources.ApplyResources(Me.WebServiceCredentialsPanel, "WebServiceCredentialsPanel")
        Me.WebServiceCredentialsPanel.Name = "WebServiceCredentialsPanel"
        '
        'ServerUrlHelp
        '
        resources.ApplyResources(Me.ServerUrlHelp, "ServerUrlHelp")
        Me.ServerUrlHelp.Name = "ServerUrlHelp"
        '
        'IgnoreCertificateErrorsCheckbox
        '
        Me.IgnoreCertificateErrorsCheckbox.AutoCheck = False
        resources.ApplyResources(Me.IgnoreCertificateErrorsCheckbox, "IgnoreCertificateErrorsCheckbox")
        Me.IgnoreCertificateErrorsCheckbox.Name = "IgnoreCertificateErrorsCheckbox"
        '
        'SecondsLabel
        '
        resources.ApplyResources(Me.SecondsLabel, "SecondsLabel")
        Me.SecondsLabel.Name = "SecondsLabel"
        '
        'ServerTimeoutLabel
        '
        resources.ApplyResources(Me.ServerTimeoutLabel, "ServerTimeoutLabel")
        Me.ServerTimeoutLabel.Name = "ServerTimeoutLabel"
        '
        'ServerTimeoutValue
        '
        resources.ApplyResources(Me.ServerTimeoutValue, "ServerTimeoutValue")
        Me.ServerTimeoutValue.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ServerTimeoutValue.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.ServerTimeoutValue.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.ServerTimeoutValue.Name = "ServerTimeoutValue"
        Me.ServerTimeoutValue.Value = New Decimal(New Integer() {120, 0, 0, 0})
        '
        'ServerUrlTextBox
        '
        Me.ServerUrlTextBox.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.ServerUrlTextBox, "ServerUrlTextBox")
        Me.ServerUrlTextBox.Name = "ServerUrlTextBox"
        '
        'BrowserTab
        '
        Me.BrowserTab.Controls.Add(Me.WebBrowser)
        resources.ApplyResources(Me.BrowserTab, "BrowserTab")
        Me.BrowserTab.Name = "BrowserTab"
        '
        'WebBrowser
        '
        resources.ApplyResources(Me.WebBrowser, "WebBrowser")
        Me.WebBrowser.Name = "WebBrowser"
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'SetupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.Menu = Me.FormMenu
        Me.MinimizeBox = False
        Me.Name = "SetupForm"
        Me.ActiviserCredentialsPanel.ResumeLayout(False)
        Me.ActiviserUserTab.ResumeLayout(False)
        Me.TestTab.ResumeLayout(False)
        Me.DeviceIDPanel.ResumeLayout(False)
        Me.NetUserTab.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.WebServiceTab.ResumeLayout(False)
        Me.WebServiceCredentialsPanel.ResumeLayout(False)
        Me.BrowserTab.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ActiviserCredentialsHelp As System.Windows.Forms.Label
    Friend WithEvents FormMenu As System.Windows.Forms.MainMenu
    Friend WithEvents NextTestDoneButton As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents BackButton As System.Windows.Forms.MenuItem
    Friend WithEvents Cancel As System.Windows.Forms.MenuItem
    Friend WithEvents UseIntegratedAuthenticationCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents UserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ActiviserCredentialsPanel As System.Windows.Forms.Panel
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents ActiviserCredentialsLabel As System.Windows.Forms.Label
    Friend WithEvents ServiceCredentialsHelp As System.Windows.Forms.Label
    Friend WithEvents ActiviserUserTab As System.Windows.Forms.TabPage
    Friend WithEvents WebServiceCredentialsLabel As System.Windows.Forms.Label
    Friend WithEvents TestTab As System.Windows.Forms.TabPage
    Friend WithEvents NetUserTab As System.Windows.Forms.TabPage
    Friend WithEvents DomainUsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WebServiceUserNameLabel As System.Windows.Forms.Label
    Friend WithEvents SavePasswordCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents WebServicePasswordLabel As System.Windows.Forms.Label
    Friend WithEvents DomainNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DomainLabel As System.Windows.Forms.Label
    Friend WithEvents DomainPasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UrlLabel As System.Windows.Forms.Label
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents WebServiceTab As System.Windows.Forms.TabPage
    Friend WithEvents WebServiceCredentialsPanel As System.Windows.Forms.Panel
    Friend WithEvents ServerUrlHelp As System.Windows.Forms.Label
    Friend WithEvents IgnoreCertificateErrorsCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents SecondsLabel As System.Windows.Forms.Label
    Friend WithEvents ServerTimeoutLabel As System.Windows.Forms.Label
    Friend WithEvents ServerTimeoutValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents ServerUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents EditContextMenu1 As activiser.EditContextMenu
    Friend WithEvents PasswordContextMenu1 As activiser.PasswordContextMenu
    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
    Friend WithEvents ResetButton As System.Windows.Forms.MenuItem
    Friend WithEvents DeviceIDPanel As System.Windows.Forms.Panel
    Friend WithEvents DeviceIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DeviceIDLabel As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents BrowserTab As System.Windows.Forms.TabPage
    Friend WithEvents UserInfoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents HttpTestButton As System.Windows.Forms.MenuItem
    Friend WithEvents AuthenticationLabel As System.Windows.Forms.Label
End Class
