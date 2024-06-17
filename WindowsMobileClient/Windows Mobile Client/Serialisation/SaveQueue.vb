Public Class SaveQueue
    Private Shared _saveQueue As New Generic.Queue(Of SaveQueueItem)

    Public Shared Sub Enqueue(ByVal ds As DataSet, ByVal filename As String, ByVal committed As Boolean, ByVal pending As Boolean)
        Dim newItem As SaveQueueItem = New SaveQueueItem(ds, filename, committed, pending)
        newItem.EnqueueTime = DateTime.Now
        _saveQueue.Enqueue(newItem)
    End Sub
End Class
