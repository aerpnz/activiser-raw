Public Class FindEventArgs
    Inherits EventArgs

    Private _findingFirst As Boolean

    Public Property FindingFirst() As Boolean
        Get
            Return _findingFirst
        End Get
        Set(ByVal value As Boolean)
            _findingFirst = value
        End Set
    End Property

    Public Sub New(ByVal findingFirst As Boolean)
        MyBase.New()
        Me.FindingFirst = findingFirst
    End Sub

End Class
