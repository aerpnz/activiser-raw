<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class CustomFormPanel
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
    Private components As System.ComponentModel.IContainer = New System.ComponentModel.Container()

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BindingNavigator1 = New System.Windows.Forms.Panel
        Me.DeleteRecordButton = New activiser.Library.Forms.ImageButton
        Me.AddRecordButton = New activiser.Library.Forms.ImageButton
        Me.RecordLocationLabel = New System.Windows.Forms.Label
        Me.MoveLastButton = New activiser.Library.Forms.ImageButton
        Me.MoveNextButton = New activiser.Library.Forms.ImageButton
        Me.MovePreviousButton = New activiser.Library.Forms.ImageButton
        Me.MoveFirstButton = New activiser.Library.Forms.ImageButton
        Me.CustomControlPanel = New System.Windows.Forms.Panel
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.DeleteRecordButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AddRecordButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MoveLastButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MoveNextButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MovePreviousButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MoveFirstButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.BindingNavigator1.Controls.Add(Me.DeleteRecordButton)
        Me.BindingNavigator1.Controls.Add(Me.AddRecordButton)
        Me.BindingNavigator1.Controls.Add(Me.RecordLocationLabel)
        Me.BindingNavigator1.Controls.Add(Me.MoveLastButton)
        Me.BindingNavigator1.Controls.Add(Me.MoveNextButton)
        Me.BindingNavigator1.Controls.Add(Me.MovePreviousButton)
        Me.BindingNavigator1.Controls.Add(Me.MoveFirstButton)
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 302)
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.Size = New System.Drawing.Size(240, 24)
        '
        'DeleteRecordButton
        '
        Me.DeleteRecordButton.BackColor = System.Drawing.SystemColors.Control
        Me.DeleteRecordButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DeleteRecordButton.ClickMaskColor = System.Drawing.SystemColors.Highlight
        Me.DeleteRecordButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.DeleteRecordButton.Image = Nothing
        Me.DeleteRecordButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.DeleteRecordButton.Location = New System.Drawing.Point(120, 0)
        Me.DeleteRecordButton.Name = "DeleteRecordButton"
        Me.DeleteRecordButton.Size = New System.Drawing.Size(24, 24)
        Me.DeleteRecordButton.TabIndex = 5
        Me.DeleteRecordButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.DeleteRecordButton.TextVisible = False
        '
        'AddRecordButton
        '
        Me.AddRecordButton.BackColor = System.Drawing.SystemColors.Control
        Me.AddRecordButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AddRecordButton.ClickMaskColor = System.Drawing.SystemColors.Highlight
        Me.AddRecordButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.AddRecordButton.Image = Nothing
        Me.AddRecordButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.AddRecordButton.Location = New System.Drawing.Point(96, 0)
        Me.AddRecordButton.Name = "AddRecordButton"
        Me.AddRecordButton.Size = New System.Drawing.Size(24, 24)
        Me.AddRecordButton.TabIndex = 4
        Me.AddRecordButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.AddRecordButton.TextVisible = False
        '
        'RecordLocationLabel
        '
        Me.RecordLocationLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RecordLocationLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.RecordLocationLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.RecordLocationLabel.Location = New System.Drawing.Point(150, 4)
        Me.RecordLocationLabel.Name = "RecordLocationLabel"
        Me.RecordLocationLabel.Size = New System.Drawing.Size(87, 16)
        Me.RecordLocationLabel.Tag = "{0} of {1}"
        Me.RecordLocationLabel.Text = "{0} of {1}"
        '
        'MoveLastButton
        '
        Me.MoveLastButton.BackColor = System.Drawing.SystemColors.Control
        Me.MoveLastButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MoveLastButton.ClickMaskColor = System.Drawing.SystemColors.Highlight
        Me.MoveLastButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MoveLastButton.Image = Nothing
        Me.MoveLastButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.MoveLastButton.Location = New System.Drawing.Point(72, 0)
        Me.MoveLastButton.Name = "MoveLastButton"
        Me.MoveLastButton.Size = New System.Drawing.Size(24, 24)
        Me.MoveLastButton.TabIndex = 3
        Me.MoveLastButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.MoveLastButton.TextVisible = False
        '
        'MoveNextButton
        '
        Me.MoveNextButton.BackColor = System.Drawing.SystemColors.Control
        Me.MoveNextButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MoveNextButton.ClickMaskColor = System.Drawing.SystemColors.Highlight
        Me.MoveNextButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MoveNextButton.Image = Nothing
        Me.MoveNextButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.MoveNextButton.Location = New System.Drawing.Point(48, 0)
        Me.MoveNextButton.Name = "MoveNextButton"
        Me.MoveNextButton.Size = New System.Drawing.Size(24, 24)
        Me.MoveNextButton.TabIndex = 2
        Me.MoveNextButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.MoveNextButton.TextVisible = False
        '
        'MovePreviousButton
        '
        Me.MovePreviousButton.BackColor = System.Drawing.SystemColors.Control
        Me.MovePreviousButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MovePreviousButton.ClickMaskColor = System.Drawing.SystemColors.Highlight
        Me.MovePreviousButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MovePreviousButton.Image = Nothing
        Me.MovePreviousButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.MovePreviousButton.Location = New System.Drawing.Point(24, 0)
        Me.MovePreviousButton.Name = "MovePreviousButton"
        Me.MovePreviousButton.Size = New System.Drawing.Size(24, 24)
        Me.MovePreviousButton.TabIndex = 1
        Me.MovePreviousButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.MovePreviousButton.TextVisible = False
        '
        'MoveFirstButton
        '
        Me.MoveFirstButton.BackColor = System.Drawing.SystemColors.Control
        Me.MoveFirstButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MoveFirstButton.ClickMaskColor = System.Drawing.SystemColors.Highlight
        Me.MoveFirstButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MoveFirstButton.Image = Nothing
        Me.MoveFirstButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.MoveFirstButton.Location = New System.Drawing.Point(0, 0)
        Me.MoveFirstButton.Name = "MoveFirstButton"
        Me.MoveFirstButton.Size = New System.Drawing.Size(24, 24)
        Me.MoveFirstButton.TabIndex = 0
        Me.MoveFirstButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.MoveFirstButton.TextVisible = False
        '
        'CustomControlPanel
        '
        Me.CustomControlPanel.AutoScroll = True
        Me.CustomControlPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomControlPanel.Location = New System.Drawing.Point(0, 0)
        Me.CustomControlPanel.Name = "CustomControlPanel"
        Me.CustomControlPanel.Size = New System.Drawing.Size(240, 302)
        '
        'CustomFormPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScrollMargin = New System.Drawing.Size(4, 4)
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Controls.Add(Me.CustomControlPanel)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.Name = "CustomFormPanel"
        Me.Size = New System.Drawing.Size(240, 326)
        Me.BindingNavigator1.ResumeLayout(False)
        CType(Me.DeleteRecordButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AddRecordButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MoveLastButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MoveNextButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MovePreviousButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MoveFirstButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.Panel
    Friend WithEvents MoveFirstButton As activiser.Library.Forms.ImageButton
    Friend WithEvents RecordLocationLabel As System.Windows.Forms.Label
    Friend WithEvents MoveLastButton As activiser.Library.Forms.ImageButton
    Friend WithEvents MoveNextButton As activiser.Library.Forms.ImageButton
    Friend WithEvents MovePreviousButton As activiser.Library.Forms.ImageButton
    Friend WithEvents CustomControlPanel As System.Windows.Forms.Panel
    Friend WithEvents DeleteRecordButton As activiser.Library.Forms.ImageButton
    Friend WithEvents AddRecordButton As activiser.Library.Forms.ImageButton

End Class
