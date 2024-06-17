<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class SyncForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        If disposing AndAlso _logMutex IsNot Nothing Then
            _logMutex.Close()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SyncForm))
        Me.StatusLabel = New System.Windows.Forms.Label
        Me.Url = New System.Windows.Forms.TextBox
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.Log = New System.Windows.Forms.TextBox
        Me.MainMenu = New System.Windows.Forms.MainMenu
        Me.OkCancelButton = New System.Windows.Forms.MenuItem
        Me.MenuButton = New System.Windows.Forms.MenuItem
        Me.SyncButton = New System.Windows.Forms.MenuItem
        Me.AutoCloseCheckBox = New System.Windows.Forms.MenuItem
        Me.ResetServerButton = New System.Windows.Forms.MenuItem
        Me.MenuClose = New System.Windows.Forms.MenuItem
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.LogRefreshTimer = New System.Windows.Forms.Timer
        Me.SuspendLayout()
        '
        'StatusLabel
        '
        resources.ApplyResources(Me.StatusLabel, "StatusLabel")
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.StatusLabel.Name = "StatusLabel"
        '
        'Url
        '
        Me.Url.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.Url, "Url")
        Me.Url.Name = "Url"
        Me.Url.ReadOnly = True
        '
        'ReadOnlyContextMenu1
        '
        Me.ReadOnlyContextMenu1.ShowCall = False
        '
        'Log
        '
        Me.Log.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.Log, "Log")
        Me.Log.Name = "Log"
        Me.Log.ReadOnly = True
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.Add(Me.OkCancelButton)
        Me.MainMenu.MenuItems.Add(Me.MenuButton)
        '
        'OkCancelButton
        '
        resources.ApplyResources(Me.OkCancelButton, "OkCancelButton")
        '
        'MenuButton
        '
        Me.MenuButton.MenuItems.Add(Me.SyncButton)
        Me.MenuButton.MenuItems.Add(Me.AutoCloseCheckBox)
        Me.MenuButton.MenuItems.Add(Me.ResetServerButton)
        Me.MenuButton.MenuItems.Add(Me.MenuClose)
        resources.ApplyResources(Me.MenuButton, "MenuButton")
        '
        'SyncButton
        '
        resources.ApplyResources(Me.SyncButton, "SyncButton")
        '
        'AutoCloseCheckBox
        '
        resources.ApplyResources(Me.AutoCloseCheckBox, "AutoCloseCheckBox")
        '
        'ResetServerButton
        '
        resources.ApplyResources(Me.ResetServerButton, "ResetServerButton")
        '
        'MenuClose
        '
        resources.ApplyResources(Me.MenuClose, "MenuClose")
        '
        'LogRefreshTimer
        '
        Me.LogRefreshTimer.Interval = 250
        '
        'SyncForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.Log)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.Url)
        Me.KeyPreview = True
        Me.Menu = Me.MainMenu
        Me.Name = "SyncForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents mnuSelectAllLog As System.Windows.Forms.MenuItem
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Url As System.Windows.Forms.TextBox
    Friend WithEvents mnuCopyLog As System.Windows.Forms.MenuItem
    Friend WithEvents Log As System.Windows.Forms.TextBox
    Friend WithEvents MainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents OkCancelButton As System.Windows.Forms.MenuItem
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel

    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
    Friend WithEvents MenuButton As System.Windows.Forms.MenuItem
    Friend WithEvents AutoCloseCheckBox As System.Windows.Forms.MenuItem
    Friend WithEvents MenuClose As System.Windows.Forms.MenuItem
    Friend WithEvents ResetServerButton As System.Windows.Forms.MenuItem
    Friend WithEvents SyncButton As System.Windows.Forms.MenuItem
    Friend WithEvents LogRefreshTimer As System.Windows.Forms.Timer
End Class
