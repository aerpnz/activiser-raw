<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePasswordForm
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
        Dim OldPasswordLabel As System.Windows.Forms.Label
        Dim NewPasswordLabel As System.Windows.Forms.Label
        Me.PasswordGroup = New System.Windows.Forms.GroupBox
        Me.AbortButton = New System.Windows.Forms.Button
        Me.DoneButton = New System.Windows.Forms.Button
        Me.OldPassword = New System.Windows.Forms.TextBox
        Me.NewPassword = New System.Windows.Forms.TextBox
        Me.ConfirmPasswordLabel = New System.Windows.Forms.Label
        Me.ConfirmPasswordTextBox = New System.Windows.Forms.TextBox
        OldPasswordLabel = New System.Windows.Forms.Label
        NewPasswordLabel = New System.Windows.Forms.Label
        Me.PasswordGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'OldPasswordLabel
        '
        OldPasswordLabel.AutoSize = True
        OldPasswordLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        OldPasswordLabel.Location = New System.Drawing.Point(6, 25)
        OldPasswordLabel.Name = "OldPasswordLabel"
        OldPasswordLabel.Size = New System.Drawing.Size(75, 13)
        OldPasswordLabel.TabIndex = 30
        OldPasswordLabel.Text = "Old Password:"
        OldPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NewPasswordLabel
        '
        NewPasswordLabel.AutoSize = True
        NewPasswordLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        NewPasswordLabel.Location = New System.Drawing.Point(6, 51)
        NewPasswordLabel.Name = "NewPasswordLabel"
        NewPasswordLabel.Size = New System.Drawing.Size(81, 13)
        NewPasswordLabel.TabIndex = 23
        NewPasswordLabel.Text = "New Password:"
        NewPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasswordGroup
        '
        Me.PasswordGroup.Controls.Add(Me.AbortButton)
        Me.PasswordGroup.Controls.Add(Me.DoneButton)
        Me.PasswordGroup.Controls.Add(Me.OldPassword)
        Me.PasswordGroup.Controls.Add(OldPasswordLabel)
        Me.PasswordGroup.Controls.Add(Me.NewPassword)
        Me.PasswordGroup.Controls.Add(NewPasswordLabel)
        Me.PasswordGroup.Controls.Add(Me.ConfirmPasswordLabel)
        Me.PasswordGroup.Controls.Add(Me.ConfirmPasswordTextBox)
        Me.PasswordGroup.Location = New System.Drawing.Point(14, 12)
        Me.PasswordGroup.Name = "PasswordGroup"
        Me.PasswordGroup.Size = New System.Drawing.Size(287, 163)
        Me.PasswordGroup.TabIndex = 12
        Me.PasswordGroup.TabStop = False
        Me.PasswordGroup.Text = "activiser™ Password"
        '
        'AbortButton
        '
        Me.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.AbortButton.Location = New System.Drawing.Point(149, 119)
        Me.AbortButton.Name = "AbortButton"
        Me.AbortButton.Size = New System.Drawing.Size(75, 27)
        Me.AbortButton.TabIndex = 33
        Me.AbortButton.Text = "&Cancel"
        Me.AbortButton.UseVisualStyleBackColor = True
        '
        'DoneButton
        '
        Me.DoneButton.Enabled = False
        Me.DoneButton.Location = New System.Drawing.Point(62, 119)
        Me.DoneButton.Name = "DoneButton"
        Me.DoneButton.Size = New System.Drawing.Size(75, 27)
        Me.DoneButton.TabIndex = 32
        Me.DoneButton.Text = "&Ok"
        Me.DoneButton.UseVisualStyleBackColor = True
        '
        'OldPassword
        '
        Me.OldPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OldPassword.Enabled = False
        Me.OldPassword.Location = New System.Drawing.Point(109, 25)
        Me.OldPassword.MaxLength = 50
        Me.OldPassword.Name = "OldPassword"
        Me.OldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.OldPassword.Size = New System.Drawing.Size(172, 20)
        Me.OldPassword.TabIndex = 31
        '
        'NewPasswordTextBox
        '
        Me.NewPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewPassword.Enabled = False
        Me.NewPassword.Location = New System.Drawing.Point(109, 51)
        Me.NewPassword.MaxLength = 50
        Me.NewPassword.Name = "NewPasswordTextBox"
        Me.NewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.NewPassword.Size = New System.Drawing.Size(172, 20)
        Me.NewPassword.TabIndex = 24
        '
        'ConfirmPasswordLabel
        '
        Me.ConfirmPasswordLabel.AutoSize = True
        Me.ConfirmPasswordLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ConfirmPasswordLabel.Location = New System.Drawing.Point(6, 77)
        Me.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel"
        Me.ConfirmPasswordLabel.Size = New System.Drawing.Size(94, 13)
        Me.ConfirmPasswordLabel.TabIndex = 25
        Me.ConfirmPasswordLabel.Text = "Confirm Password:"
        Me.ConfirmPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ConfirmPasswordTextBox
        '
        Me.ConfirmPasswordTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmPasswordTextBox.Enabled = False
        Me.ConfirmPasswordTextBox.Location = New System.Drawing.Point(109, 77)
        Me.ConfirmPasswordTextBox.MaxLength = 50
        Me.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox"
        Me.ConfirmPasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ConfirmPasswordTextBox.Size = New System.Drawing.Size(172, 20)
        Me.ConfirmPasswordTextBox.TabIndex = 26
        '
        'ChangePassword
        '
        Me.AcceptButton = Me.DoneButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.AbortButton
        Me.ClientSize = New System.Drawing.Size(314, 186)
        Me.Controls.Add(Me.PasswordGroup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChangePassword"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "ChangePassword"
        Me.TopMost = True
        Me.PasswordGroup.ResumeLayout(False)
        Me.PasswordGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PasswordGroup As System.Windows.Forms.GroupBox
    Friend WithEvents OldPassword As System.Windows.Forms.TextBox
    Friend WithEvents NewPassword As System.Windows.Forms.TextBox
    Friend WithEvents ConfirmPasswordLabel As System.Windows.Forms.Label
    Friend WithEvents ConfirmPasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AbortButton As System.Windows.Forms.Button
    Friend WithEvents DoneButton As System.Windows.Forms.Button
End Class
