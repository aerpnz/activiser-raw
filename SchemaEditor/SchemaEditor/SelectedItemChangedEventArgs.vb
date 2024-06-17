
Public Class SelectedItemChangedEventArgs
    Inherits EventArgs

    Private _pdaControl As CustomControl
    Sub New(ByVal Item As CustomControl)
        Me.PdaControl = Item
    End Sub

    Public Property PdaControl() As CustomControl
        Get
            Return _pdaControl
        End Get
        Private Set(ByVal value As CustomControl)
            _pdaControl = value
        End Set
    End Property
End Class
