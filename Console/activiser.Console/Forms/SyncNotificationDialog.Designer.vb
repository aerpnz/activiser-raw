<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SyncNotificationDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SyncNotificationDialog))
        Me.SyncMessage = New System.Windows.Forms.LinkLabel
        Me.CloseButton = New System.Windows.Forms.Label
        Me.Logo = New System.Windows.Forms.PictureBox
        Me.FadeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.WaitTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SyncMessage
        '
        resources.ApplyResources(Me.SyncMessage, "SyncMessage")
        Me.SyncMessage.BackColor = System.Drawing.Color.Transparent
        Me.SyncMessage.Name = "SyncMessage"
        Me.SyncMessage.TabStop = True
        '
        'CloseButton
        '
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.CloseButton.BackColor = System.Drawing.Color.Transparent
        Me.CloseButton.Name = "CloseButton"
        '
        'Logo
        '
        Me.Logo.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Logo, "Logo")
        Me.Logo.Name = "Logo"
        Me.Logo.TabStop = False
        '
        'FadeTimer
        '
        '
        'WaitTimer
        '
        Me.WaitTimer.Interval = 4000
        '
        'SyncNotificationDialog
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        resources.ApplyResources(Me, "$this")
        Me.ControlBox = False
        Me.Controls.Add(Me.SyncMessage)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.CloseButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SyncNotificationDialog"
        Me.Opacity = 0
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.TopMost = True
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SyncMessage As System.Windows.Forms.LinkLabel
    Friend WithEvents CloseButton As System.Windows.Forms.Label
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
    Friend WithEvents FadeTimer As System.Windows.Forms.Timer
    Friend WithEvents WaitTimer As System.Windows.Forms.Timer
End Class
