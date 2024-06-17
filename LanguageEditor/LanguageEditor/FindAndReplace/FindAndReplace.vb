Imports System.Windows.Forms

Public Class FindAndReplace

    Public Event FindClicked As EventHandler(Of FindEventArgs)
    Public Event ReplaceClicked As EventHandler
    Public Event ReplaceAllClicked As EventHandler

    Public Property TextToFind() As String
        Get
            Return Me.TextToFindBox.Text
        End Get
        Set(ByVal value As String)
            Me.TextToFindBox.Text = value
        End Set
    End Property

    Public Property ReplacementText() As String
        Get
            Return Me.ReplacementTextBox.Text
        End Get
        Set(ByVal value As String)
            Me.ReplacementTextBox.Text = value
        End Set
    End Property

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Close_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FindButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindButton.Click
        RaiseEvent FindClicked(Me, New FindEventArgs(FindButton.Text = "&Find"))
        Me.FindButton.Text = "&Find Next"
    End Sub

    Private Sub ReplaceButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceButton.Click
        RaiseEvent ReplaceClicked(Me, e)
    End Sub

    Private Sub ReplaceAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceAllButton.Click
        RaiseEvent ReplaceAllClicked(Me, e)
    End Sub

    Private Sub FindAndReplace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FindButton.Text = "&Find"
    End Sub

    Private Sub TextToFindBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextToFindBox.TextChanged
        Me.FindButton.Text = "&Find"
    End Sub
End Class
