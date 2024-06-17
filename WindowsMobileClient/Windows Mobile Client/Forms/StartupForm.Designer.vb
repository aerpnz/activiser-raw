<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class StartupForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartupForm))
        Me.StatusMessageBox = New System.Windows.Forms.Label
        Me.VersionNumber = New System.Windows.Forms.Label
        Me.Logo = New System.Windows.Forms.PictureBox
        Me.UserNameBox = New System.Windows.Forms.Label
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.CancelButton = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'StatusMessageBox
        '
        resources.ApplyResources(Me.StatusMessageBox, "StatusMessageBox")
        Me.StatusMessageBox.Name = "StatusMessageBox"
        '
        'VersionNumber
        '
        resources.ApplyResources(Me.VersionNumber, "VersionNumber")
        Me.VersionNumber.Name = "VersionNumber"
        '
        'Logo
        '
        resources.ApplyResources(Me.Logo, "Logo")
        Me.Logo.Name = "Logo"
        '
        'UserNameBox
        '
        resources.ApplyResources(Me.UserNameBox, "UserNameBox")
        Me.UserNameBox.Name = "UserNameBox"
        '
        'InputPanel
        '
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.Add(Me.CancelButton)
        '
        'CancelButton
        '
        resources.ApplyResources(Me.CancelButton, "CancelButton")
        '
        'StartupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me, "$this")
        Me.ControlBox = False
        Me.Controls.Add(Me.UserNameBox)
        Me.Controls.Add(Me.VersionNumber)
        Me.Controls.Add(Me.StatusMessageBox)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "StartupForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StatusMessageBox As System.Windows.Forms.Label
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
    Friend WithEvents UserNameBox As System.Windows.Forms.Label
    Friend WithEvents VersionNumber As System.Windows.Forms.Label
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents CancelButton As System.Windows.Forms.MenuItem

End Class
