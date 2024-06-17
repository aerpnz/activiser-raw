Imports System.Windows.Forms

Public Class GetNewForm

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub GetNewForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public ReadOnly Property ModuleName() As String
        Get
            Return Me.ModuleNamePicker.Text
        End Get
    End Property

    Public ReadOnly Property DisplayName() As String
        Get
            Return Me.DisplayNamePicker.Text
        End Get
    End Property

    Public ReadOnly Property ClientKey() As Integer
        Get
            Return Decimal.ToInt32(Me.ClientIdPicker.Value)
        End Get
    End Property

    Public ReadOnly Property LanguageId() As Integer
        Get
            Return Decimal.ToInt32(Me.LanguageIdPicker.Value)
        End Get
    End Property
End Class
