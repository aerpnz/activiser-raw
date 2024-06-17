'<summary>
'Class to add a "Ascending" field to a ColumnHeader object so we can
'etermine if the column was last in ascending or descending order.
'</summary>
Public Class SortableColumnHeader
    Inherits ColumnHeader

    Public Enum ColumnTypes As Integer
        [String]
        Number
        DateTime
    End Enum

    Private _Ascending As Boolean
    Private _ColumnName As String
    Private _ColumnType As ColumnTypes

    Public Sub New(ByVal text As String, ByVal width As Integer, ByVal align As HorizontalAlignment, ByVal ascending As Boolean)
        Me.Text = text
        Me.Width = width
        Me.TextAlign = align
        Me.Ascending = ascending
        Me.ColumnType = ColumnTypes.String
    End Sub

    'Public Sub New(ByVal text As String, ByVal width As Integer, ByVal align As HorizontalAlignment, ByVal ascending As Boolean, ByVal ColumnType As ColumnTypeEnum)
    '    Me.Text = text
    '    Me.Width = width
    '    Me.TextAlign = align
    '    Me.Ascending = ascending
    '    Me.ColumnType = ColumnType
    'End Sub

    Public Sub New(ByVal text As String, ByVal width As Integer, ByVal align As HorizontalAlignment, ByVal ascending As Boolean, ByVal columnName As String, ByVal columnType As ColumnTypes)
        Me.Text = text
        Me.Width = width
        Me.TextAlign = align
        Me.Ascending = ascending
        Me.ColumnType = ColumnType
        Me.ColumnName = ColumnName
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

    Public Property ColumnType() As ColumnTypes
        Get
            Return _ColumnType
        End Get
        Set(ByVal value As ColumnTypes)
            _ColumnType = value
        End Set
    End Property

End Class
