Imports System.ComponentModel

Public Class CategoryObjectCollection
    Inherits BindingList(Of CategoryObjectListItem)

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

    Public Overloads Function Contains(ByVal objectId As Nullable(Of Integer)) As Boolean
        For Each listItem As CategoryObjectListItem In Me.Items
            If listItem.ObjectId.HasValue AndAlso objectId.HasValue AndAlso listItem.ObjectId.Value = objectId.Value Then
                Return True
            ElseIf (Not listItem.ObjectId.HasValue) AndAlso (Not objectId.HasValue) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Function IndexOf(ByVal item As CategoryObjectListItem) As Integer
        For i As Integer = 0 To Me.Items.Count - 1
            If Me.Items(i) Is item Then
                Return i
            End If
        Next
        Return -1
    End Function

    Public Overloads Function IndexOf(ByVal objectId As Nullable(Of Integer)) As Integer
        For i As Integer = 0 To Me.Items.Count - 1
            If Me.Items(i).ObjectId.Equals(objectId) Then
                Return i
            End If
        Next
        Return -1
    End Function

    Public Sub PopulateList(ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Integer), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            MyBase.RaiseListChangedEvents = False
            MyBase.Clear()
            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                MyBase.Add(New CategoryObjectListItem(CInt(dr(idColumnName)), CStr(dr(descriptionColumnName))))
            Next
            If nullValue.HasValue AndAlso Not String.IsNullOrEmpty(nullDescription) Then
                MyBase.Add(New CategoryObjectListItem(nullValue, nullDescription))
            End If

        Catch ex As Exception
            Throw
        Finally
            MyBase.RaiseListChangedEvents = True
            MyBase.ResetBindings()
            Trace.Unindent()
        End Try
    End Sub

    Public Sub PopulateList(ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        Me.PopulateList(table, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As ComboBox, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Integer), ByVal nullDescription As String)
        If listBox Is Nothing Then Throw New ArgumentNullException("listBox")
        Dim previousValue As Object = listBox.SelectedValue
        Dim previousIndex As Integer = listBox.SelectedIndex
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            listBox.SuspendLayout()
            list.RaiseListChangedEvents = False
            list.Clear()

            Dim table As DataTable = ConsoleData.CoreDataSet.Tables(tableName)

            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                list.Add(New CategoryObjectListItem(CType(dr(idColumnName), Integer), CStr(dr(descriptionColumnName))))
            Next

            If Not String.IsNullOrEmpty(nullDescription) Then
                list.Add(New CategoryObjectListItem(nullValue, nullDescription))
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
            If previousIndex = -1 Then
                listBox.SelectedIndex = -1
            Else
                listBox.SelectedValue = previousValue
            End If
            Trace.Unindent()
        End Try

    End Sub

    Public Shared Sub PopulateList(ByVal listBox As ComboBox, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        PopulateList(listBox, list, tableName, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As ComboBox, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String)
        PopulateList(listBox, list, tableName, idColumnName, descriptionColumnName, String.Empty, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DataGridViewComboBoxColumn, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Integer), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            list.RaiseListChangedEvents = False
            list.Clear()

            Dim table As DataTable = ConsoleData.CoreDataSet.Tables(tableName)
            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                list.Add(New CategoryObjectListItem(CType(dr(idColumnName), Integer), CStr(dr(descriptionColumnName))))
            Next

            If Not String.IsNullOrEmpty(nullDescription) Then
                list.Add(New CategoryObjectListItem(nullValue, nullDescription))
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

    Public Shared Sub PopulateList(ByVal listBox As DataGridViewComboBoxColumn, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        PopulateList(listBox, list, tableName, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DataGridViewComboBoxColumn, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String)
        PopulateList(listBox, list, tableName, idColumnName, descriptionColumnName, String.Empty, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DomainUpDown, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal sortColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Integer), ByVal nullDescription As String)
        If listBox Is Nothing Then Throw New ArgumentNullException("listBox")
        Dim previousValue As Object = listBox.SelectedItem
        Dim previousIndex As Integer = listBox.SelectedIndex
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            listBox.SuspendLayout()
            listBox.Items.Clear()

            list.RaiseListChangedEvents = False
            list.Clear()

            Dim table As DataTable = ConsoleData.CoreDataSet.Tables(tableName)

            For Each dr As DataRow In table.Select(filter, sortColumnName)
                list.Add(New CategoryObjectListItem(CType(dr(idColumnName), Integer), CStr(dr(descriptionColumnName))))
            Next

            If Not String.IsNullOrEmpty(nullDescription) Then
                list.Add(New CategoryObjectListItem(nullValue, nullDescription))
            End If

            For Each listItem As CategoryObjectListItem In list
                'Debug.Print(listBox.Items.Contains(listItem).ToString)
                listBox.Items.Add(listItem)
            Next

            'For Each listItem As CategoryObjectListItem In list
            '    For Each upDownItem As CategoryObjectListItem In listBox.Items
            '        Debug.Print(CStr(upDownItem Is listItem))
            '    Next
            '    'listBox.Items.Add(listItem)
            'Next

            listBox.Refresh()
            listBox.ResumeLayout()

        Catch ex As Exception
            Throw
        Finally
            list.RaiseListChangedEvents = True
            list.ResetBindings()
            If previousIndex = -1 Then
                listBox.SelectedIndex = -1
            Else
                listBox.SelectedItem = previousValue
            End If
            Trace.Unindent()
        End Try

    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DomainUpDown, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal sortColumnName As String, ByVal filter As String)
        PopulateList(listBox, list, tableName, idColumnName, descriptionColumnName, sortColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DomainUpDown, ByVal list As CategoryObjectCollection, ByVal tableName As String, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal sortColumnName As String)
        PopulateList(listBox, list, tableName, idColumnName, descriptionColumnName, sortColumnName, String.Empty, Nothing, Nothing)
    End Sub
End Class
