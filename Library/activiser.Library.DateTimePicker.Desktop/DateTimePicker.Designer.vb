<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DateTimePicker
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Me.DateCombo = New System.Windows.Forms.ComboBox
        Me.TimeCombo = New System.Windows.Forms.ComboBox
        Me.HaveValueCheckBox = New System.Windows.Forms.CheckBox
        Me.DateTimeSpacer = New System.Windows.Forms.Label
        Me.CheckBoxSpacer = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'DateCombo
        '
        Me.DateCombo.Dock = System.Windows.Forms.DockStyle.Left
        Me.DateCombo.DropDownHeight = 1
        Me.DateCombo.IntegralHeight = False
        Me.DateCombo.ItemHeight = 13
        Me.DateCombo.Location = New System.Drawing.Point(19, 0)
        Me.DateCombo.MaxDropDownItems = 1
        Me.DateCombo.Name = "DateCombo"
        Me.DateCombo.Size = New System.Drawing.Size(110, 21)
        Me.DateCombo.TabIndex = 2
        '
        'TimeCombo
        '
        Me.TimeCombo.Dock = System.Windows.Forms.DockStyle.Left
        Me.TimeCombo.DropDownHeight = 1
        Me.TimeCombo.IntegralHeight = False
        Me.TimeCombo.Location = New System.Drawing.Point(135, 0)
        Me.TimeCombo.MaxDropDownItems = 1
        Me.TimeCombo.MaxLength = 10
        Me.TimeCombo.Name = "TimeCombo"
        Me.TimeCombo.Size = New System.Drawing.Size(59, 21)
        Me.TimeCombo.TabIndex = 4
        '
        'HaveValueCheckBox
        '
        Me.HaveValueCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.HaveValueCheckBox.Checked = True
        Me.HaveValueCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HaveValueCheckBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.HaveValueCheckBox.Location = New System.Drawing.Point(0, 0)
        Me.HaveValueCheckBox.Name = "HaveValueCheckBox"
        Me.HaveValueCheckBox.Size = New System.Drawing.Size(15, 23)
        Me.HaveValueCheckBox.TabIndex = 0
        '
        'DateTimeSpacer
        '
        Me.DateTimeSpacer.Dock = System.Windows.Forms.DockStyle.Left
        Me.DateTimeSpacer.Location = New System.Drawing.Point(129, 0)
        Me.DateTimeSpacer.Name = "DateTimeSpacer"
        Me.DateTimeSpacer.Size = New System.Drawing.Size(6, 23)
        Me.DateTimeSpacer.TabIndex = 3
        Me.DateTimeSpacer.Text = " "
        '
        'CheckBoxSpacer
        '
        Me.CheckBoxSpacer.Dock = System.Windows.Forms.DockStyle.Left
        Me.CheckBoxSpacer.Location = New System.Drawing.Point(15, 0)
        Me.CheckBoxSpacer.Name = "CheckBoxSpacer"
        Me.CheckBoxSpacer.Size = New System.Drawing.Size(4, 23)
        Me.CheckBoxSpacer.TabIndex = 1
        Me.CheckBoxSpacer.Text = " "
        '
        'DateTimePicker
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.TimeCombo)
        Me.Controls.Add(Me.DateTimeSpacer)
        Me.Controls.Add(Me.DateCombo)
        Me.Controls.Add(Me.CheckBoxSpacer)
        Me.Controls.Add(Me.HaveValueCheckBox)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "DateTimePicker"
        Me.Size = New System.Drawing.Size(206, 23)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DateCombo As System.Windows.Forms.ComboBox
    Friend WithEvents TimeCombo As System.Windows.Forms.ComboBox
    Friend WithEvents HaveValueCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimeSpacer As System.Windows.Forms.Label
    Friend WithEvents CheckBoxSpacer As System.Windows.Forms.Label

End Class
