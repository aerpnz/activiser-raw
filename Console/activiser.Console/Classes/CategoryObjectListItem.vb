Imports System.ComponentModel

<DefaultProperty("ObjectId")> _
Public Class CategoryObjectListItem
    Implements IComparable(Of CategoryObjectListItem)

    Private _id As Nullable(Of Integer)
    Public Property ObjectId() As Nullable(Of Integer)
        Get
            Return _id
        End Get
        Set(ByVal value As Nullable(Of Integer))
            _id = value
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

    Public Sub New(ByVal id As Nullable(Of Integer), ByVal description As String)
        Me.ObjectId = id
        Me.Description = description
    End Sub

    Public Overloads Function Equals(ByVal target As CategoryObjectListItem) As Boolean
        If target.ObjectId.HasValue AndAlso Me.ObjectId.HasValue Then
            Return target.ObjectId.Value = Me.ObjectId.Value AndAlso target.Description = Me.Description
        Else
            Return (Not target.ObjectId.HasValue) AndAlso (Not Me.ObjectId.HasValue)
        End If
    End Function

    Public Overloads Function Equals(ByVal target As Nullable(Of Integer)) As Boolean
        If target.HasValue AndAlso Me.ObjectId.HasValue Then
            Return target.Value = Me.ObjectId.Value
        Else
            Return (Not target.HasValue) AndAlso (Not Me.ObjectId.HasValue)
        End If
    End Function

    Public Overloads Function Equals(ByVal target As String) As Boolean
        If target Is Nothing Then Return _description Is Nothing
        Return target = _description
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return False
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me._id.GetHashCode
    End Function

    Public Shared Operator =(ByVal left As CategoryObjectListItem, ByVal right As CategoryObjectListItem) As Boolean
        If (Object.ReferenceEquals(left, Nothing)) Then
            Return Object.ReferenceEquals(right, Nothing)
        Else
            Return left.Equals(right)
        End If
    End Operator

    Public Shared Operator <>(ByVal left As CategoryObjectListItem, ByVal right As CategoryObjectListItem) As Boolean
        If (Object.ReferenceEquals(left, Nothing)) Then
            Return Not Object.ReferenceEquals(right, Nothing)
        Else
            Return Not left.Equals(right)
        End If
    End Operator

    Public Shared Operator >(ByVal left As CategoryObjectListItem, ByVal right As CategoryObjectListItem) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then
            Return False
        End If
        Return left.Description > right.Description
    End Operator

    Public Shared Operator <(ByVal left As CategoryObjectListItem, ByVal right As CategoryObjectListItem) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then
            Return False
        End If
        Return left.Description < right.Description
    End Operator

    Public Overrides Function ToString() As String
        Return Description
    End Function

    Public Function CompareTo(ByVal other As CategoryObjectListItem) As Integer Implements System.IComparable(Of CategoryObjectListItem).CompareTo
        Dim result As Integer = String.Compare(Description, other.Description, StringComparison.Ordinal)
        If result = 0 Then
            If ObjectId.HasValue AndAlso other.ObjectId.HasValue Then
                result = ObjectId.Value.CompareTo(other.ObjectId.Value)
            Else
                If ObjectId.HasValue Then
                    result = 1
                Else
                    result = -1
                End If
            End If
        End If
        Return result
    End Function
End Class