<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class AboutForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.CloseButton = New System.Windows.Forms.MenuItem
        Me.AboutMessage = New System.Windows.Forms.TextBox
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.Logo = New activiser.Library.Forms.ImageLabel
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.CloseButton)
        '
        'CloseButton
        '
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        '
        'AboutMessage
        '
        Me.AboutMessage.AcceptsReturn = True
        Me.AboutMessage.AcceptsTab = True
        Me.AboutMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AboutMessage.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.AboutMessage, "AboutMessage")
        Me.AboutMessage.Name = "AboutMessage"
        '
        'ReadOnlyContextMenu1
        '
        Me.ReadOnlyContextMenu1.ShowCall = False
        '
        'Logo
        '
        Me.Logo.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.Logo, "Logo")
        Me.Logo.Image = CType(resources.GetObject("Logo.Image"), System.Drawing.Image)
        Me.Logo.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.Logo.Name = "Logo"
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Logo.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.AboutMessage)
        Me.Controls.Add(Me.Logo)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.Name = "AboutForm"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AboutMessage As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents CloseButton As System.Windows.Forms.MenuItem
    Friend WithEvents ReadOnlyContextMenu1 As ReadOnlyContextMenu
    Friend WithEvents Logo As activiser.Library.Forms.ImageLabel
End Class
