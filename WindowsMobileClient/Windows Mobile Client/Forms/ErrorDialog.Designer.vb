<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ErrorDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ErrorDialog))
        Me.Caption = New System.Windows.Forms.Label
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.OkButton = New System.Windows.Forms.MenuItem
        Me.SaveButton = New System.Windows.Forms.MenuItem
        Me.ErrorMessage = New System.Windows.Forms.TextBox
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.SuspendLayout()
        '
        'Caption
        '
        resources.ApplyResources(Me.Caption, "Caption")
        Me.Caption.Name = "Caption"
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.OkButton)
        Me.MenuStrip.MenuItems.Add(Me.SaveButton)
        '
        'OkButton
        '
        resources.ApplyResources(Me.OkButton, "OkButton")
        '
        'SaveButton
        '
        resources.ApplyResources(Me.SaveButton, "SaveButton")
        '
        'ErrorMessage
        '
        Me.ErrorMessage.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.ErrorMessage, "ErrorMessage")
        Me.ErrorMessage.Name = "ErrorMessage"
        Me.ErrorMessage.ReadOnly = True
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.FileName = "activiserError.txt"
        resources.ApplyResources(Me.SaveFileDialog, "SaveFileDialog")
        '
        'ErrorDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ErrorMessage)
        Me.Controls.Add(Me.Caption)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.MinimizeBox = False
        Me.Name = "ErrorDialog"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Caption As System.Windows.Forms.Label
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents OkButton As System.Windows.Forms.MenuItem
    Friend WithEvents SaveButton As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
End Class
