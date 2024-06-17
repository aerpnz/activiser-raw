<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class CredentialDialog
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
    Private menuStrip As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CredentialDialog))
        Me.menuStrip = New System.Windows.Forms.MainMenu
        Me.okButton = New System.Windows.Forms.MenuItem
        Me.cancelButton = New System.Windows.Forms.MenuItem
        Me.DomainNameBox = New System.Windows.Forms.TextBox
        Me.EditContextMenu1 = New activiser.EditContextMenu
        Me.PasswordBox = New System.Windows.Forms.TextBox
        Me.UserNameBox = New System.Windows.Forms.TextBox
        Me.DomainLabel = New System.Windows.Forms.Label
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UserNameLabel = New System.Windows.Forms.Label
        Me.SavePasswordCheckBox = New System.Windows.Forms.CheckBox
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.CredentialPanel = New System.Windows.Forms.Panel
        Me.PasswordContextMenu1 = New activiser.PasswordContextMenu
        Me.CredentialPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuStrip
        '
        Me.menuStrip.MenuItems.Add(Me.okButton)
        Me.menuStrip.MenuItems.Add(Me.cancelButton)
        '
        'okButton
        '
        resources.ApplyResources(Me.okButton, "okButton")
        '
        'cancelButton
        '
        resources.ApplyResources(Me.cancelButton, "cancelButton")
        '
        'DomainNameBox
        '
        resources.ApplyResources(Me.DomainNameBox, "DomainNameBox")
        Me.DomainNameBox.ContextMenu = Me.EditContextMenu1
        Me.DomainNameBox.Name = "DomainNameBox"
        '
        'PasswordBox
        '
        resources.ApplyResources(Me.PasswordBox, "PasswordBox")
        Me.PasswordBox.Name = "PasswordBox"
        '
        'UserNameBox
        '
        resources.ApplyResources(Me.UserNameBox, "UserNameBox")
        Me.UserNameBox.ContextMenu = Me.EditContextMenu1
        Me.UserNameBox.Name = "UserNameBox"
        '
        'DomainLabel
        '
        resources.ApplyResources(Me.DomainLabel, "DomainLabel")
        Me.DomainLabel.Name = "DomainLabel"
        '
        'PasswordLabel
        '
        resources.ApplyResources(Me.PasswordLabel, "PasswordLabel")
        Me.PasswordLabel.Name = "PasswordLabel"
        '
        'UserNameLabel
        '
        resources.ApplyResources(Me.UserNameLabel, "UserNameLabel")
        Me.UserNameLabel.Name = "UserNameLabel"
        '
        'SavePasswordCheckBox
        '
        resources.ApplyResources(Me.SavePasswordCheckBox, "SavePasswordCheckBox")
        Me.SavePasswordCheckBox.Name = "SavePasswordCheckBox"
        '
        'InputPanel
        '
        Me.InputPanel.Enabled = True
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'CredentialPanel
        '
        Me.CredentialPanel.Controls.Add(Me.UserNameBox)
        Me.CredentialPanel.Controls.Add(Me.UserNameLabel)
        Me.CredentialPanel.Controls.Add(Me.SavePasswordCheckBox)
        Me.CredentialPanel.Controls.Add(Me.PasswordLabel)
        Me.CredentialPanel.Controls.Add(Me.DomainNameBox)
        Me.CredentialPanel.Controls.Add(Me.DomainLabel)
        Me.CredentialPanel.Controls.Add(Me.PasswordBox)
        resources.ApplyResources(Me.CredentialPanel, "CredentialPanel")
        Me.CredentialPanel.Name = "CredentialPanel"
        '
        'CredentialDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.ContextMenu = Me.PasswordContextMenu1
        Me.Controls.Add(Me.CredentialPanel)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.Menu = Me.menuStrip
        Me.MinimizeBox = False
        Me.Name = "CredentialDialog"
        Me.TopMost = True
        Me.CredentialPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents okButton As System.Windows.Forms.MenuItem
    Friend WithEvents cancelButton As System.Windows.Forms.MenuItem
    Friend WithEvents DomainNameBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordBox As System.Windows.Forms.TextBox
    Friend WithEvents UserNameBox As System.Windows.Forms.TextBox
    Friend WithEvents DomainLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents SavePasswordCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents CredentialPanel As System.Windows.Forms.Panel
    Friend WithEvents EditContextMenu1 As activiser.EditContextMenu
    Friend WithEvents PasswordContextMenu1 As activiser.PasswordContextMenu
End Class
