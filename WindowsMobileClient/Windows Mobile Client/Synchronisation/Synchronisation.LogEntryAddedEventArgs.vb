Public Class SyncLogEntryAddedEventArgs
    Inherits EventArgs

    Private Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal eventTime As DateTime, ByVal message As String)
        MyBase.New()
        Me.Message = message
        Me.EventTime = eventTime
    End Sub

    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Private Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Private _eventTime As DateTime
    Public Property EventTime() As DateTime
        Get
            Return _eventTime
        End Get
        Private Set(ByVal value As DateTime)
            _eventTime = value
        End Set
    End Property
End Class
