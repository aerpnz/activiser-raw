'<summary>
'Class to add a "Ascending" field to a ColumnHeader object so we can
'etermine if the column was last in ascending or descending order.
'</summary>

Public Enum ColumnType As Integer
    [String]
    Number
    DateTime
    Data
End Enum

Public Class SortableColumnHeader
    Inherits ColumnHeader

    Private _ascending As Boolean
    Private _columnName As String
    Private _columnType As ColumnType

    Public Sub New(ByVal text As String, ByVal width As Integer, ByVal align As HorizontalAlignment, ByVal columnName As String, ByVal columnType As ColumnType)
        Me.Text = text
        Me.Width = width
        Me.TextAlign = align
        Me.ColumnType = columnType
        Me.ColumnName = columnName
    End Sub

    Public Property Ascending() As Boolean
        Get
            Return _Ascending
        End Get
        Set(ByVal value As Boolean)
            _Ascending = value
        End Set
    End Property

    Public Property ColumnName() As String
        Get
            Return _ColumnName
        End Get
        Set(ByVal value As String)
            _ColumnName = value
        End Set
    End Property

    Public Property ColumnType() As ColumnType
        Get
            Return _ColumnType
        End Get
        Set(ByVal value As ColumnType)
            _ColumnType = value
        End Set
    End Property
End Class
