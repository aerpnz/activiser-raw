Imports System.Runtime.CompilerServices

Public Class SelectorItem(Of T)
    Public Sub New()
    End Sub

    Public Sub New(ByVal value As T, ByVal description As String)
        _value = value
        _description = description
    End Sub

    Private _value As T
    Public Property Value() As T
        Get
            Return _value
        End Get
        Set(ByVal value As T)
            _value = value
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _description
    End Function
End Class

Module SelectorItemListExtensions
#If FRAMEWORK_VERSION_GE35 Then
    <Extension()> _
    Public Function IndexOf(Of T)(ByVal list As Generic.List(Of SelectorItem(Of T)), ByVal value As T) As Integer
#Else
    Public Function IndexOf(Of T)(ByVal list As Generic.List(Of SelectorItem(Of T)), ByVal value As T) As Integer
#End If
        For i As Integer = 0 To list.Count - 1
            If list(i).Value.Equals(value) Then
                Return i
            End If
        Next
        Return -1
    End Function

#If FRAMEWORK_VERSION_GE35 Then
    <Extension()> _
    Public Function IndexOf(Of T)(ByVal list As DomainUpDown.DomainUpDownItemCollection, ByVal value As T) As Integer
#Else
    Public Function IndexOf(Of T)(ByVal list As DomainUpDown.DomainUpDownItemCollection, ByVal value As T) As Integer
#End If
        For i As Integer = 0 To list.Count - 1
            Dim thisItem As SelectorItem(Of T) = TryCast(list(i), SelectorItem(Of T))
            If (thisItem IsNot Nothing) AndAlso (thisItem.Value.Equals(value)) Then
                Return i
            End If
        Next
        Return -1
    End Function
End Module