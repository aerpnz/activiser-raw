<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DurationPicker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DurationPicker))
        Me.HoursLabel = New System.Windows.Forms.Label
        Me.MinutesLabel = New System.Windows.Forms.Label
        Me.Minutes = New System.Windows.Forms.NumericUpDown
        Me.Hours = New System.Windows.Forms.NumericUpDown
        CType(Me.Minutes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Hours, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HoursLabel
        '
        resources.ApplyResources(Me.HoursLabel, "HoursLabel")
        Me.HoursLabel.Name = "HoursLabel"
        '
        'MinutesLabel
        '
        resources.ApplyResources(Me.MinutesLabel, "MinutesLabel")
        Me.MinutesLabel.Name = "MinutesLabel"
        '
        'Minutes
        '
        resources.ApplyResources(Me.Minutes, "Minutes")
        Me.Minutes.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.Minutes.Name = "Minutes"
        '
        'Hours
        '
        resources.ApplyResources(Me.Hours, "Hours")
        Me.Hours.Maximum = New Decimal(New Integer() {168, 0, 0, 0})
        Me.Hours.Name = "Hours"
        '
        'DurationPicker
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MinutesLabel)
        Me.Controls.Add(Me.Minutes)
        Me.Controls.Add(Me.HoursLabel)
        Me.Controls.Add(Me.Hours)
        Me.MinimumSize = New System.Drawing.Size(140, 22)
        Me.Name = "DurationPicker"
        CType(Me.Minutes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Hours, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Minutes As System.Windows.Forms.NumericUpDown
    Private WithEvents Hours As System.Windows.Forms.NumericUpDown
    Public WithEvents HoursLabel As System.Windows.Forms.Label
    Public WithEvents MinutesLabel As System.Windows.Forms.Label

End Class
