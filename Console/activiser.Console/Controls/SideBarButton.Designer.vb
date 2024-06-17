<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SideBarButton
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Caption = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Caption
        '
        Me.Caption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Caption.Image = Global.activiser.Console.My.Resources.Resources.SaveAllHS
        Me.Caption.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Caption.Location = New System.Drawing.Point(0, 0)
        Me.Caption.Name = "Caption"
        Me.Caption.Size = New System.Drawing.Size(180, 32)
        Me.Caption.TabIndex = 0
        Me.Caption.Text = "SideBarButton"
        Me.Caption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SideBarButton
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImage = Global.activiser.Console.My.Resources.Resources.ButtonBG
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.Caption)
        Me.Name = "SideBarButton"
        Me.Size = New System.Drawing.Size(180, 32)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Caption As System.Windows.Forms.Label

End Class
