<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatePickerPopup
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
        Me.Calendar = New System.Windows.Forms.MonthCalendar
        Me.Border = New System.Windows.Forms.Panel
        Me.Border.SuspendLayout()
        Me.SuspendLayout()
        '
        'Calendar
        '
        Me.Calendar.Location = New System.Drawing.Point(1, 1)
        Me.Calendar.MaxSelectionCount = 1
        Me.Calendar.Name = "Calendar"
        Me.Calendar.TabIndex = 0
        '
        'Border
        '
        Me.Border.BackColor = System.Drawing.SystemColors.WindowFrame
        Me.Border.Controls.Add(Me.Calendar)
        Me.Border.Location = New System.Drawing.Point(0, 0)
        Me.Border.Name = "Border"
        Me.Border.Padding = New System.Windows.Forms.Padding(1)
        Me.Border.Size = New System.Drawing.Size(173, 157)
        Me.Border.TabIndex = 1
        '
        'DatePickerPopup
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Magenta
        Me.ClientSize = New System.Drawing.Size(173, 157)
        Me.ControlBox = False
        Me.Controls.Add(Me.Border)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DatePickerPopup"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.Border.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Calendar As System.Windows.Forms.MonthCalendar
    Friend WithEvents Border As System.Windows.Forms.Panel
End Class
