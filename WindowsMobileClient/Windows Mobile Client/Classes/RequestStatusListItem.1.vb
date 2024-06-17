Imports System.Runtime.CompilerServices

Friend Class RequestStatusListItem
    Public RequestStatusId As Integer
    Public Description As String
    Public Sub New(ByVal id As Integer, ByVal description As String)
        Me.RequestStatusId = id
        Me.Description = description
    End Sub

    Public Overrides Function ToString() As String
        Return Description
    End Function
End Class

Module RequestStatusListExtensions
    <Extension()> _
    Public Function IndexOf(ByVal list As Generic.List(Of RequestStatusListItem), ByVal status As Integer) As Integer
        For i As Integer = 0 To list.Count - 1
            If list(i).RequestStatusId = status Then
                Return i
            End If
        Next
        Return -1
    End Function
End Module