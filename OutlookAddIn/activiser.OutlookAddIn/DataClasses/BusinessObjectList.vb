Imports System.ComponentModel

Public Class BusinessObjectList
    Inherits BindingList(Of BusinessObjectListItem)
    Implements IComponent

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

    Public Sub PopulateList(ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Guid), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            MyBase.RaiseListChangedEvents = False
            MyBase.Clear()
            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                MyBase.Add(New BusinessObjectListItem(CType(dr(idColumnName), Guid), CStr(dr(descriptionColumnName))))
            Next
            If nullValue.HasValue AndAlso Not String.IsNullOrEmpty(nullDescription) Then
                MyBase.Add(New BusinessObjectListItem(nullValue, nullDescription))
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

    Public Shared Sub PopulateList(ByVal listBox As ComboBox, ByVal list As BusinessObjectList, ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Guid), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            listBox.SuspendLayout()
            'listBox.DataSource = Nothing
            list.RaiseListChangedEvents = False
            list.Clear()

            'Dim table As DataTable = ConsoleData.CoreDataSet.Tables(tableName)
            Dim newRows() As System.Data.DataRow = table.Select(filter, descriptionColumnName)

            Using newList As New BusinessObjectList()

                For Each dr As DataRow In newRows
                    list.Add(New BusinessObjectListItem(CType(dr(idColumnName), Guid), CStr(dr(descriptionColumnName))))
                Next

                If Not String.IsNullOrEmpty(nullDescription) Then
                    Dim nullItem As BusinessObjectListItem = New BusinessObjectListItem(nullValue, nullDescription)
                    list.Add(nullItem)
                End If

                If listBox.DataSource IsNot list Then
                    listBox.DataSource = list
                    listBox.ValueMember = STR_ObjectId
                    listBox.DisplayMember = STR_Description
                End If
            End Using

        Catch ex As Exception
            Throw
        Finally
            list.RaiseListChangedEvents = True
            list.ResetBindings()
            listBox.Refresh()
            listBox.ResumeLayout()
            Trace.Unindent()
        End Try
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As ComboBox, ByVal list As BusinessObjectList, ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        PopulateList(listBox, list, table, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As ComboBox, ByVal list As BusinessObjectList, ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String)
        PopulateList(listBox, list, table, idColumnName, descriptionColumnName, String.Empty, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DataGridViewComboBoxColumn, ByVal list As BusinessObjectList, ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String, ByVal nullValue As Nullable(Of Guid), ByVal nullDescription As String)
        Try
            Trace.Indent()
            TraceVerbose(STR_Fired)

            list.RaiseListChangedEvents = False
            list.Clear()

            ' Dim table As DataTable = ConsoleData.CoreDataSet.Tables(tableName)
            For Each dr As DataRow In table.Select(filter, descriptionColumnName)
                list.Add(New BusinessObjectListItem(CType(dr(idColumnName), Guid), CStr(dr(descriptionColumnName))))
            Next

            If Not String.IsNullOrEmpty(nullDescription) Then
                list.Add(New BusinessObjectListItem(nullValue, nullDescription))
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
            list.ResetBindings()
            Trace.Unindent()
        End Try
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DataGridViewComboBoxColumn, ByVal list As BusinessObjectList, ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String, ByVal filter As String)
        PopulateList(listBox, list, table, idColumnName, descriptionColumnName, filter, Nothing, Nothing)
    End Sub

    Public Shared Sub PopulateList(ByVal listBox As DataGridViewComboBoxColumn, ByVal list As BusinessObjectList, ByVal table As DataTable, ByVal idColumnName As String, ByVal descriptionColumnName As String)
        PopulateList(listBox, list, table, idColumnName, descriptionColumnName, String.Empty, Nothing, Nothing)
    End Sub

#Region "IComponent support"

    Public Event Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Implements System.ComponentModel.IComponent.Disposed
    Private _site As ISite

    Public Property Site() As System.ComponentModel.ISite Implements System.ComponentModel.IComponent.Site
        Get
            Return _site
        End Get
        Set(ByVal value As System.ComponentModel.ISite)
            _site = value
        End Set
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
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

#End Region

End Class
