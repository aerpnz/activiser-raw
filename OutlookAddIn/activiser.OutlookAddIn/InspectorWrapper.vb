Friend Class InspectorWrapper
    Implements IDisposable

    Private WithEvents _Inspector As Outlook.Inspector
    Private _inspectorList As Generic.List(Of InspectorWrapper)

    Friend Sub New(ByVal inspector As Outlook.Inspector)
        Me._Inspector = inspector
        Debug.WriteLine(String.Format("New Inspector Added: {0}", inspector.Caption))
    End Sub

    Friend Property List() As Generic.List(Of InspectorWrapper)
        Get
            Return _inspectorList
        End Get
        Set(ByVal value As Generic.List(Of InspectorWrapper))
            If Me._inspectorList IsNot value Then
                If Me._inspectorList IsNot Nothing AndAlso Me._inspectorList.Contains(Me) Then
                    Me._inspectorList.Remove(Me)
                End If

                Me._inspectorList = value

                If Me._inspectorList IsNot Nothing Then
                    Me._inspectorList.Add(Me)
                End If
            End If
        End Set
    End Property

    Private Sub _Inspector_Close() Handles _Inspector.Close
        Debug.WriteLine(String.Format("Inspector closed: {0}", _Inspector.Caption))
        Me.List = Nothing
        Me._Inspector = Nothing
    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        Debug.WriteLine("Inspector disposed")
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class