Imports System.ComponentModel

Friend Class CategoryObjectCollection
    Inherits BindingList(Of CategoryObjectItem)

    Const STR_Fired As String = "Fired"
    Const STR_ObjectId As String = "ObjectId"
    Const STR_Description As String = "Description"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal name As String)
        MyBase.New()
        Me.Name = name
    End Sub

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Overloads Function Contains(ByVal item As CategoryObjectItem) As Boolean
        Return MyBase.Contains(item)
    End Function

    Public Function GetItem(ByVal objectId As Integer) As CategoryObjectItem
        For Each item As CategoryObjectItem In MyBase.Items
            If item.ObjectId.HasValue AndAlso item.ObjectId.Value = objectId Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Overloads Function Contains(ByVal objectId As Integer) As Boolean
        For Each o As CategoryObjectItem In MyBase.Items
            If o.ObjectId.HasValue AndAlso o.ObjectId.Value = objectId Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub PopulateList(ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Integer), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            MyBase.RaiseListChangedEvents = False
            MyBase.Clear()
            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                MyBase.Add(New CategoryObjectItem(CInt(dr(idColumnName)), CStr(dr(descriptionColumnName))))
            Next
            If nullValue.HasValue AndAlso Not String.IsNullOrEmpty(nullDescription) Then
                MyBase.Add(New CategoryObjectItem(nullValue, nullDescription))
            End If

        Catch ex As Exception
            Throw
        Finally
            MyBase.RaiseListChangedEvents = True
            MyBase.ResetBindings()
            Trace.Unindent()
        End Try
    End Sub

    Public Shared Sub PopulateList(ByVal sourceData As DataSet, ByVal listBox As ComboBox, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Integer), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            listBox.SuspendLayout()
            list.RaiseListChangedEvents = False
            list.Clear()

            Dim table As DataTable = sourceData.Tables(tableName)

            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                list.Add(New CategoryObjectItem(CType(dr(idColumnName), Integer), CStr(dr(descriptionColumnName))))
            Next

            If Not String.IsNullOrEmpty(nullDescription) Then
                list.Add(New CategoryObjectItem(nullValue, nullDescription))
            End If

            If Not listBox.DataSource Is list Then
                listBox.DataSource = list
                listBox.ValueMember = STR_ObjectId
                listBox.DisplayMember = STR_Description
            End If

            listBox.Refresh()
            listBox.ResumeLayout()

        Catch ex As Exception
            Throw
        Finally
            list.RaiseListChangedEvents = True
            list.ResetBindings()
            Trace.Unindent()
        End Try
    End Sub

    Public Shared Sub PopulateList(ByVal sourceData As DataSet, ByVal listBox As DataGridViewComboBoxColumn, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Integer), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            list.RaiseListChangedEvents = False
            list.Clear()

            Dim table As DataTable = sourceData.Tables(tableName)
            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                list.Add(New CategoryObjectItem(CType(dr(idColumnName), Integer), CStr(dr(descriptionColumnName))))
            Next

            If Not String.IsNullOrEmpty(nullDescription) Then
                list.Add(New CategoryObjectItem(nullValue, nullDescription))
            End If

            If Not listBox.DataSource Is list Then
                listBox.DataSource = list
                listBox.ValueMember = STR_ObjectId
                listBox.DisplayMember = STR_Description
            End If

        Catch ex As Exception
            Throw
        Finally
            list.RaiseListChangedEvents = True
            Trace.Unindent()
        End Try
    End Sub


    Public Sub PopulateList(ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        Me.PopulateList(table, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal sourceData As DataSet, ByVal listBox As ComboBox, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        PopulateList(sourceData, listBox, list, tableName, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal sourceData As DataSet, ByVal listBox As ComboBox, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String)
        PopulateList(sourceData, listBox, list, tableName, idColumnName, descriptionColumnName, String.Empty, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal sourceData As DataSet, ByVal listBox As DataGridViewComboBoxColumn, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        PopulateList(sourceData, listBox, list, tableName, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal sourceData As DataSet, ByVal listBox As DataGridViewComboBoxColumn, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String)
        PopulateList(sourceData, listBox, list, tableName, idColumnName, descriptionColumnName, String.Empty, Nothing, Nothing)
    End Sub
End Class
