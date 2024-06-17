Public Class ChangePasswordForm
    Private Sub NewPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmPasswordTextBox.TextChanged, NewPassword.TextChanged
        Me.DoneButton.Enabled = Me.NewPassword.Text = Me.ConfirmPasswordTextBox.Text
    End Sub

    Private Sub PasswordTextBox_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NewPassword.Validating
        Me.ConfirmPasswordTextBox.Clear()
    End Sub

    Private Sub PasswordConfirmTextBox_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If Me.ConfirmPasswordTextBox.Text <> Me.NewPassword.Text Then
            MessageBox.Show(My.Resources.ConsultantSubFormPasswordMismatch, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.NewPassword.Focus()
            Me.NewPassword.Clear()
            Me.ConfirmPasswordTextBox.Clear()
        End If
    End Sub

End Class