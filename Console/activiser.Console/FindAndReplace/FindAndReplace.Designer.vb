<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FindAndReplace
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Close_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextToFindBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ReplacementTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.ReplaceButton = New System.Windows.Forms.Button
        Me.FindButton = New System.Windows.Forms.Button
        Me.ReplaceAllButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Close_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(165, 145)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Close_Button
        '
        Me.Close_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Close_Button.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close_Button.Location = New System.Drawing.Point(76, 3)
        Me.Close_Button.Name = "Close_Button"
        Me.Close_Button.Size = New System.Drawing.Size(67, 23)
        Me.Close_Button.TabIndex = 1
        Me.Close_Button.Text = "Close"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fi&nd what:"
        '
        'TextToFindBox
        '
        Me.TextToFindBox.Location = New System.Drawing.Point(15, 25)
        Me.TextToFindBox.Name = "TextToFindBox"
        Me.TextToFindBox.Size = New System.Drawing.Size(293, 20)
        Me.TextToFindBox.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Re&place with:"
        '
        'ReplacementTextBox
        '
        Me.ReplacementTextBox.Location = New System.Drawing.Point(15, 64)
        Me.ReplacementTextBox.Name = "ReplacementTextBox"
        Me.ReplacementTextBox.Size = New System.Drawing.Size(293, 20)
        Me.ReplacementTextBox.TabIndex = 4
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.Controls.Add(Me.ReplaceButton, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.FindButton, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ReplaceAllButton, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(18, 110)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(293, 29)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'ReplaceButton
        '
        Me.ReplaceButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ReplaceButton.Location = New System.Drawing.Point(100, 3)
        Me.ReplaceButton.Name = "ReplaceButton"
        Me.ReplaceButton.Size = New System.Drawing.Size(91, 23)
        Me.ReplaceButton.TabIndex = 2
        Me.ReplaceButton.Text = "&Replace"
        '
        'FindButton
        '
        Me.FindButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FindButton.Location = New System.Drawing.Point(3, 3)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(91, 23)
        Me.FindButton.TabIndex = 0
        Me.FindButton.Text = "&Find"
        '
        'ReplaceAllButton
        '
        Me.ReplaceAllButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ReplaceAllButton.Location = New System.Drawing.Point(198, 3)
        Me.ReplaceAllButton.Name = "ReplaceAllButton"
        Me.ReplaceAllButton.Size = New System.Drawing.Size(91, 23)
        Me.ReplaceAllButton.TabIndex = 1
        Me.ReplaceAllButton.Text = "Replace &All"
        '
        'FindAndReplace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Close_Button
        Me.ClientSize = New System.Drawing.Size(323, 186)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.ReplacementTextBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextToFindBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FindAndReplace"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FindAndReplace"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Close_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextToFindBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ReplacementTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ReplaceButton As System.Windows.Forms.Button
    Friend WithEvents FindButton As System.Windows.Forms.Button
    Friend WithEvents ReplaceAllButton As System.Windows.Forms.Button

End Class
