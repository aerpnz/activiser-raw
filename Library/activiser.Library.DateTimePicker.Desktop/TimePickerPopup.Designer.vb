<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimePickerPopup
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
        Me.TimeList = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'TimeList
        '
        Me.TimeList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TimeList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TimeList.FormattingEnabled = True
        Me.TimeList.Location = New System.Drawing.Point(0, 0)
        Me.TimeList.Name = "TimeList"
        Me.TimeList.ScrollAlwaysVisible = True
        Me.TimeList.Size = New System.Drawing.Size(112, 106)
        Me.TimeList.TabIndex = 0
        '
        'TimePickerPopup
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Magenta
        Me.ClientSize = New System.Drawing.Size(116, 113)
        Me.ControlBox = False
        Me.Controls.Add(Me.TimeList)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TimePickerPopup"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TimeList As System.Windows.Forms.ListBox
End Class
