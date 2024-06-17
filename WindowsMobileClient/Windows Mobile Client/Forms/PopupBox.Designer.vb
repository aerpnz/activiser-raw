<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PopupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PopupBox))
        Me.Caption = New System.Windows.Forms.Label
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.OkYesButton = New System.Windows.Forms.MenuItem
        Me.CancelAbortButton = New System.Windows.Forms.MenuItem
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.PopupTimer = New System.Windows.Forms.Timer
        Me.Caption2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Caption
        '
        Me.Caption.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.Caption, "Caption")
        Me.Caption.Name = "Caption"
        '
        'ReadOnlyContextMenu1
        '
        'Me.ReadOnlyContextMenu1.ShowCall = False
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.OkYesButton)
        Me.MenuStrip.MenuItems.Add(Me.CancelAbortButton)
        '
        'OkYesButton
        '
        resources.ApplyResources(Me.OkYesButton, "OkYesButton")
        '
        'CancelAbortButton
        '
        resources.ApplyResources(Me.CancelAbortButton, "CancelAbortButton")
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'InputPanel
        '
        '
        'PopupTimer
        '
        '
        'Caption2
        '
        Me.Caption2.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.Caption2, "Caption2")
        Me.Caption2.Name = "Caption2"
        '
        'PopupBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.Caption)
        Me.Controls.Add(Me.Caption2)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.MinimizeBox = False
        Me.Name = "PopupBox"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Caption As System.Windows.Forms.Label
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents OkYesButton As System.Windows.Forms.MenuItem
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents CancelAbortButton As System.Windows.Forms.MenuItem
    Friend WithEvents ReadOnlyContextMenu1 As ReadOnlyContextMenu
    Friend WithEvents PopupTimer As System.Windows.Forms.Timer
    Friend WithEvents Caption2 As System.Windows.Forms.Label
End Class
