<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Caption = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.SuspendLayout()
        '
        'Caption
        '
        Me.Caption.Dock = System.Windows.Forms.DockStyle.Left
        Me.Caption.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Caption.ForeColor = System.Drawing.SystemColors.MenuText
        Me.Caption.Location = New System.Drawing.Point(0, 0)
        Me.Caption.Name = "Caption"
        Me.Caption.Size = New System.Drawing.Size(82, 22)
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Splitter1.Location = New System.Drawing.Point(82, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(6, 22)
        '
        'CustomControl
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Caption)
        Me.Name = "CustomControl"
        Me.Size = New System.Drawing.Size(204, 22)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Caption As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter

End Class
