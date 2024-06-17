<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class DialogBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogBox))
        Me.Caption = New System.Windows.Forms.Label
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.OkYesButton = New System.Windows.Forms.MenuItem
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.TextInput = New System.Windows.Forms.TextBox
        Me.EditContextMenu1 = New activiser.EditContextMenu
        Me.IconBox = New System.Windows.Forms.PictureBox
        Me.ImageList32 = New System.Windows.Forms.ImageList
        Me.ImageList64 = New System.Windows.Forms.ImageList
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
        '
        'OkYesButton
        '
        resources.ApplyResources(Me.OkYesButton, "OkYesButton")
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'InputPanel
        '
        '
        'TextInput
        '
        Me.TextInput.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.TextInput, "TextInput")
        Me.TextInput.Name = "TextInput"
        '
        'EditContextMenu1
        '
        'Me.EditContextMenu1.ShowCall = False
        '
        'IconBox
        '
        resources.ApplyResources(Me.IconBox, "IconBox")
        Me.IconBox.Name = "IconBox"
        '
        'ImageList32
        '
        resources.ApplyResources(Me.ImageList32, "ImageList32")
        Me.ImageList32.Images.Clear()
        Me.ImageList32.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Icon))
        Me.ImageList32.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Icon))
        Me.ImageList32.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Icon))
        Me.ImageList32.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Icon))
        Me.ImageList32.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Icon))
        Me.ImageList32.Images.Add(CType(resources.GetObject("resource5"), System.Drawing.Icon))
        Me.ImageList32.Images.Add(CType(resources.GetObject("resource6"), System.Drawing.Icon))
        '
        'ImageList64
        '
        resources.ApplyResources(Me.ImageList64, "ImageList64")
        Me.ImageList64.Images.Clear()
        Me.ImageList64.Images.Add(CType(resources.GetObject("resource7"), System.Drawing.Icon))
        Me.ImageList64.Images.Add(CType(resources.GetObject("resource8"), System.Drawing.Icon))
        Me.ImageList64.Images.Add(CType(resources.GetObject("resource9"), System.Drawing.Icon))
        Me.ImageList64.Images.Add(CType(resources.GetObject("resource10"), System.Drawing.Icon))
        Me.ImageList64.Images.Add(CType(resources.GetObject("resource11"), System.Drawing.Icon))
        Me.ImageList64.Images.Add(CType(resources.GetObject("resource12"), System.Drawing.Icon))
        Me.ImageList64.Images.Add(CType(resources.GetObject("resource13"), System.Drawing.Icon))
        '
        'DialogBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TextInput)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.Controls.Add(Me.Caption)
        Me.Controls.Add(Me.IconBox)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.MinimizeBox = False
        Me.Name = "DialogBox"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Caption As System.Windows.Forms.Label
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents OkYesButton As System.Windows.Forms.MenuItem

    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents TextInput As System.Windows.Forms.TextBox
    Friend WithEvents IconBox As System.Windows.Forms.PictureBox
    Friend WithEvents ImageList32 As System.Windows.Forms.ImageList
    Friend WithEvents ImageList64 As System.Windows.Forms.ImageList
    Friend WithEvents EditContextMenu1 As EditContextMenu
    Friend WithEvents ReadOnlyContextMenu1 As ReadOnlyContextMenu
End Class
