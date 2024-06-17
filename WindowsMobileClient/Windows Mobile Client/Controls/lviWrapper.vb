'''<summary>
''' Class to wrap a listview item.  Wrapper class adds sorting functionality.
'''</summary>

Public Class listViewItemWrapper

    Private _listViewItem As ListViewItem
    Private _listViewColumnIndex As Integer

    Public Sub New(ByVal item As ListViewItem, ByVal columnIndex As Integer)
        ListViewItem = Item
        ListViewColumnIndex = ColumnIndex
    End Sub

    Public ReadOnly Property Text() As String
        Get
            Return ListViewItem.SubItems(ListViewColumnIndex).Text
        End Get

    End Property

    Public Property ListViewItem() As ListViewItem
        Get
            Return _listViewItem
        End Get
        Set(ByVal value As ListViewItem)
            _listViewItem = value
        End Set
    End Property

    Public Property ListViewColumnIndex() As Integer
        Get
            Return _listViewColumnIndex
        End Get
        Set(ByVal value As Integer)
            _listViewColumnIndex = value
        End Set
    End Property

    Public Shared Widening Operator CType(ByVal value As listViewItemWrapper) As ListViewItem
        Return value._listViewItem
    End Operator

End Class

Public Class listViewItemComparer
    Implements Generic.IComparer(Of listViewItemWrapper)

    Dim ascending As Boolean

    Public Sub New(ByVal ascending As Boolean)
        Me.ascending = ascending
    End Sub

    Public Function Compare(ByVal x As listViewItemWrapper, ByVal y As listViewItemWrapper) As Integer Implements System.Collections.Generic.IComparer(Of listViewItemWrapper).Compare
        Dim xText As String = x.ListViewItem.SubItems(x.ListViewColumnIndex).Text
        Dim yText As String = y.ListViewItem.SubItems(y.ListViewColumnIndex).Text

        If Me.ascending Then
            Return String.Compare(xText, yText, True, CultureInfo.CurrentUICulture)
        Else
            Return String.Compare(yText, xText, True, CultureInfo.CurrentUICulture)
        End If
    End Function
End Class

Public Class ListViewItemDateComparer
    Implements Generic.IComparer(Of listViewItemWrapper)

    Dim ascending As Boolean

    Public Sub New(ByVal ascending As Boolean)
        Me.ascending = ascending
    End Sub

    Public Function Compare(ByVal x As listViewItemWrapper, ByVal y As listViewItemWrapper) As Integer Implements System.Collections.Generic.IComparer(Of listViewItemWrapper).Compare
        Try
            Dim xDate As Date = DateTime.Parse(x.ListViewItem.SubItems(x.ListViewColumnIndex).Text, CultureInfo.CurrentCulture)
            Dim yDate As Date = DateTime.Parse(y.ListViewItem.SubItems(y.ListViewColumnIndex).Text, CultureInfo.CurrentCulture)

            If Me.ascending Then
                Return xDate.CompareTo(yDate)
            Else
                Return -xDate.CompareTo(yDate)
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Class

Public Class ListViewItemNumberComparer
    Implements Generic.IComparer(Of listViewItemWrapper)

    Dim ascending As Boolean

    Public Sub New(ByVal ascending As Boolean)
        Me.ascending = ascending
    End Sub

    Public Function Compare(ByVal x As listViewItemWrapper, ByVal y As listViewItemWrapper) As Integer Implements System.Collections.Generic.IComparer(Of listViewItemWrapper).Compare
        Dim xInt As Double = Double.Parse(x.ListViewItem.SubItems(x.ListViewColumnIndex).Text, CultureInfo.CurrentCulture)
        Dim yInt As Double = Double.Parse(y.ListViewItem.SubItems(y.ListViewColumnIndex).Text, CultureInfo.CurrentCulture)

        If Me.ascending Then
            Return xInt.CompareTo(yInt)
        Else
            Return -xInt.CompareTo(yInt)
        End If

    End Function
End Class

Public Class ListViewItemDataRowComparer
    Implements Generic.IComparer(Of listViewItemWrapper)

    Dim _ascending As Boolean
    Dim _column As String

    Public Sub New(ByVal ascending As Boolean, ByVal column As String)
        Me._ascending = ascending
        Me._column = column
    End Sub

    Public Function Compare(ByVal x As listViewItemWrapper, ByVal y As listViewItemWrapper) As Integer Implements System.Collections.Generic.IComparer(Of listViewItemWrapper).Compare
        If x Is Nothing Then
            Throw New ArgumentNullException("x")
        End If
        If y Is Nothing Then
            Throw New ArgumentNullException("y")
        End If

        Dim xrow As DataRow = TryCast(x.ListViewItem.Tag, DataRow)
        Dim yrow As DataRow = TryCast(y.ListViewItem.Tag, DataRow)

        If xrow Is Nothing Then
            Throw New ArgumentException("x has invalid or missing Tag")
        End If

        If yrow Is Nothing Then
            Throw New ArgumentException("y has invalid or missing Tag")
        End If

        If xrow(_column).Equals(yrow(_column)) Then
            Return 0
        Else
            Dim result As Integer = Comparer.Default.Compare(xrow(_column), yrow(_column))
            Return If(_ascending, result, -result)
        End If

    End Function
End Class
