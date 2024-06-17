Public Class SaveQueueItem
    Public Sub New(ByVal ds As DataSet, ByVal filename As String, ByVal committed As Boolean, ByVal pending As Boolean)
        Me._data = ds
        Me._fileName = filename
        Me._SaveCommitted = committed
        Me._savePending = pending
    End Sub

    Private _enqueueTime As DateTime
    Public Property EnqueueTime() As DateTime
        Get
            Return _enqueueTime
        End Get
        Set(ByVal value As DateTime)
            _enqueueTime = value
        End Set
    End Property

    Private _fileName As String
    Public ReadOnly Property FileName() As String
        Get
            Return _fileName
        End Get
    End Property

    Private _saveCommitted As Boolean
    Public ReadOnly Property SaveCommitted() As Boolean
        Get
            Return _saveCommitted
        End Get
    End Property

    Private _savePending As Boolean
    Public ReadOnly Property SavePending() As Boolean
        Get
            Return _savePending
        End Get
    End Property

    Private _data As DataSet
    Public ReadOnly Property Data() As DataSet
        Get
            Return _data
        End Get
    End Property



End Class
