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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CredentialDialog))
        Me.ContextMenuClear = New System.Windows.Forms.MenuItem
        Me.DomainNameBox = New System.Windows.Forms.TextBox
        Me.PasswordBox = New System.Windows.Forms.TextBox
        Me.UserNameBox = New System.Windows.Forms.TextBox
        Me.DomainLabel = New System.Windows.Forms.Label
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UserNameLabel = New System.Windows.Forms.Label
        Me.SavePasswordCheckBox = New System.Windows.Forms.CheckBox
        Me.EditMenu = New System.Windows.Forms.ContextMenu
        Me.ContextMenuCut = New System.Windows.Forms.MenuItem
        Me.ContextMenuCopy = New System.Windows.Forms.MenuItem
        Me.ContextMenuPaste = New System.Windows.Forms.MenuItem
        Me.ContextMenuSelectAll = New System.Windows.Forms.MenuItem
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Message = New System.Windows.Forms.Label
        Me.EntryPanel = New System.Windows.Forms.Panel
        Me.ButtonPanel = New System.Windows.Forms.Panel
        Me.EntryPanel.SuspendLayout()
        Me.ButtonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuClear
        '
        Me.ContextMenuClear.Index = 3
        resources.ApplyResources(Me.ContextMenuClear, "ContextMenuClear")
        '
        'DomainNameBox
        '
        resources.ApplyResources(Me.DomainNameBox, "DomainNameBox")
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
        'EditMenu
        '
        Me.EditMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ContextMenuCut, Me.ContextMenuCopy, Me.ContextMenuPaste, Me.ContextMenuClear, Me.ContextMenuSelectAll})
        '
        'ContextMenuCut
        '
        Me.ContextMenuCut.Index = 0
        resources.ApplyResources(Me.ContextMenuCut, "ContextMenuCut")
        '
        'ContextMenuCopy
        '
        Me.ContextMenuCopy.Index = 1
        resources.ApplyResources(Me.ContextMenuCopy, "ContextMenuCopy")
        '
        'ContextMenuPaste
        '
        Me.ContextMenuPaste.Index = 2
        resources.ApplyResources(Me.ContextMenuPaste, "ContextMenuPaste")
        '
        'ContextMenuSelectAll
        '
        Me.ContextMenuSelectAll.Index = 4
        resources.ApplyResources(Me.ContextMenuSelectAll, "ContextMenuSelectAll")
        '
        'Cancel
        '
        resources.ApplyResources(Me.Cancel, "Cancel")
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Name = "Cancel"
        '
        'OK
        '
        resources.ApplyResources(Me.OK, "OK")
        Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OK.Name = "OK"
        '
        'Message
        '
        resources.ApplyResources(Me.Message, "Message")
        Me.Message.MinimumSize = New System.Drawing.Size(0, 50)
        Me.Message.Name = "Message"
        '
        'EntryPanel
        '
        Me.EntryPanel.Controls.Add(Me.UserNameBox)
        Me.EntryPanel.Controls.Add(Me.UserNameLabel)
        Me.EntryPanel.Controls.Add(Me.PasswordLabel)
        Me.EntryPanel.Controls.Add(Me.DomainLabel)
        Me.EntryPanel.Controls.Add(Me.SavePasswordCheckBox)
        Me.EntryPanel.Controls.Add(Me.PasswordBox)
        Me.EntryPanel.Controls.Add(Me.DomainNameBox)
        resources.ApplyResources(Me.EntryPanel, "EntryPanel")
        Me.EntryPanel.Name = "EntryPanel"
        '
        'ButtonPanel
        '
        Me.ButtonPanel.Controls.Add(Me.Cancel)
        Me.ButtonPanel.Controls.Add(Me.OK)
        resources.ApplyResources(Me.ButtonPanel, "ButtonPanel")
        Me.ButtonPanel.Name = "ButtonPanel"
        '
        'CredentialDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.EntryPanel)
        Me.Controls.Add(Me.ButtonPanel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CredentialDialog"
        Me.TopMost = True
        Me.EntryPanel.ResumeLayout(False)
        Me.EntryPanel.PerformLayout()
        Me.ButtonPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DomainNameBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordBox As System.Windows.Forms.TextBox
    Friend WithEvents UserNameBox As System.Windows.Forms.TextBox
    Friend WithEvents DomainLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents SavePasswordCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents EditMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents ContextMenuCut As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuCopy As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuPaste As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuClear As System.Windows.Forms.MenuItem
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Message As System.Windows.Forms.Label
    Friend WithEvents EntryPanel As System.Windows.Forms.Panel
    Friend WithEvents ButtonPanel As System.Windows.Forms.Panel
End Class
