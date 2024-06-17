Imports System.ComponentModel

<DefaultProperty("ObjectId")> _
Public Class BusinessObjectListItem
    Implements IComparable(Of BusinessObjectListItem)

    Private _id As Nullable(Of Guid)
    Public Property ObjectId() As Nullable(Of Guid)
        Get
            Return _id
        End Get
        Set(ByVal value As Nullable(Of Guid))
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

    Public Sub New(ByVal id As Nullable(Of Guid), ByVal description As String)
        Me.ObjectId = id
        Me.Description = description
    End Sub

    Public Overloads Function Equals(ByVal target As BusinessObjectListItem) As Boolean
        If target.ObjectId.HasValue AndAlso Me.ObjectId.HasValue Then
            Return target.ObjectId.Value.Equals(Me.ObjectId.Value) AndAlso target.Description = Me.Description
        Else
            Return (Not target.ObjectId.HasValue) AndAlso (Not Me.ObjectId.HasValue)
        End If
    End Function

    Public Overloads Function Equals(ByVal target As Nullable(Of Guid)) As Boolean
        If target.HasValue AndAlso Me.ObjectId.HasValue Then
            Return target.Value.Equals(Me.ObjectId.Value)
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

    Public Shared Operator =(ByVal left As BusinessObjectListItem, ByVal right As BusinessObjectListItem) As Boolean
        If (Object.ReferenceEquals(left, Nothing)) Then
            Return Object.ReferenceEquals(right, Nothing)
        Else
            Return left.Equals(right)
        End If
    End Operator

    Public Shared Operator <>(ByVal left As BusinessObjectListItem, ByVal right As BusinessObjectListItem) As Boolean
        If (Object.ReferenceEquals(left, Nothing)) Then
            Return Not Object.ReferenceEquals(right, Nothing)
        Else
            Return Not left.Equals(right)
        End If
    End Operator

    Public Shared Operator >(ByVal left As BusinessObjectListItem, ByVal right As BusinessObjectListItem) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then
            Return False
        End If
        Return left.Description > right.Description
    End Operator

    Public Shared Operator <(ByVal left As BusinessObjectListItem, ByVal right As BusinessObjectListItem) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then
            Return False
        End If
        Return left.Description < right.Description
    End Operator

    Public Overrides Function ToString() As String
        Return Description
    End Function

    Public Function CompareTo(ByVal other As BusinessObjectListItem) As Integer Implements System.IComparable(Of BusinessObjectListItem).CompareTo
        Dim result As Integer = String.Compare(Description, other.Description, StringComparison.CurrentCulture)
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
