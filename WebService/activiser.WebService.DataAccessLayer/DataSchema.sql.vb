Imports System.Text
Imports activiser.DataSchemaTableAdapters

Partial Class DataSchema
    Const STR_CreatedDateTime As String = "CreatedDateTime"
    Const STR_ModifiedDateTime As String = "ModifiedDateTime"

    Private Shared Function UpsertRow(Of T As DataTable, R As DataRow)(ByVal table As T, ByVal newRow As R) As Integer
        ' pseudo code:
        ' then attempt fetch of row from database
        ' if no row, then 
        '      insert new one
        ' else 
        '      compare existing row with new row
        '      if changes then
        '          update modified columns only
        '      else
        '          ignore and return
        Try
            'TODO: add concurrency checks

            Dim existingRow As R = FetchRow(table, newRow)

            If existingRow IsNot Nothing Then
                If UpdateRow(existingRow, newRow) Then
                    Return 1
                Else
                    Return 0
                End If
            Else
                If InsertRow(newRow) Then
                    Return 3
                Else
                    Return 2
                End If
            End If


        Catch ex As Exception
            Throw
        End Try


    End Function


    ''' <summary>
    ''' Fetches datatable from the database, using priamry key information from an existing row.
    ''' </summary>
    ''' <param name="keyRow"></param>
    ''' <returns></returns>
    ''' <remarks>if there is any null data in a primary key column, it will fail.
    ''' </remarks>
    ''' 
    Private Shared Function FetchRow(Of T As DataTable, R As DataRow)(ByVal table As T, ByVal keyRow As R) As R
        Try
            'Dim table As DataTable = keyRow.Table
            Dim columnList As New List(Of String)
            Dim filterList As New List(Of String)
            Dim filterValues As New Dictionary(Of String, Object)

            For i As Integer = 0 To table.PrimaryKey.Length - 1
                Dim column As DataColumn = table.PrimaryKey(i)
                If keyRow.IsNull(column) Then
                    Throw New ArgumentNullException(column.ColumnName, "Primary key filter value null")
                Else
                    Dim pName As String = String.Format("PK{0}", i)
                    filterList.Add(String.Format("([{0}]=@{1})", column.ColumnName, pName))
                    filterValues.Add(pName, keyRow(column))
                End If
            Next

            If filterList.Count = 0 Then
                Throw New ArgumentException("keyRow", "Primary key filter missing")
            End If

            Dim dt As New DataTable(table.TableName)
            For Each column As DataColumn In table.Columns
                If String.IsNullOrEmpty(column.Expression) Then
                    columnList.Add(String.Format("[{0}]", column.ColumnName))
                    dt.Columns.Add(column.ColumnName, column.DataType)
                End If
            Next

            Dim sql As String = String.Format("SELECT {0} FROM {1} WHERE {2}", _
                String.Join(",", columnList.ToArray()), _
                table.TableName, _
                String.Join(" AND ", filterList.ToArray()))


            Dim cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
            For Each filterName As String In filterValues.Keys
                cmd.SelectCommand.Parameters.AddWithValue(filterName, filterValues(filterName))
            Next
            columnList.Clear()
            filterList.Clear()
            filterValues.Clear()

            columnList = Nothing
            filterList = Nothing
            filterValues = Nothing

            Select Case cmd.Fill(dt)
                Case 1
                    Dim newRow As R = CType(table.NewRow, R)
                    Dim returnedRow As DataRow = dt.Rows(0)
                    For Each column As DataColumn In table.Columns
                        If String.IsNullOrEmpty(column.Expression) Then
                            newRow(column.ColumnName) = returnedRow(column.ColumnName)
                        End If
                    Next

                    Return newRow
                Case 0
                    dt = Nothing
                    Return Nothing
                Case Else
                    Throw New InvalidOperationException("FetchRow returned multiple rows for supplied primary key")
            End Select

        Catch ex As ArgumentException
            Throw
        Catch ex As InvalidOperationException
            Throw
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Update a single row in the database
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="originalRow"></param>
    ''' <param name="newRow"></param>
    ''' <returns>1 = row updated
    ''' 0 = now unchanged
    ''' </returns>
    ''' <remarks></remarks>
    ''' TODO: add concurrency conflict check/log facility
    Private Shared Function UpdateRow(Of T As DataRow)(ByVal originalRow As T, ByVal newRow As T) As Boolean
        Try
            Dim table As DataTable = newRow.Table

            'Dim columnList As New List(Of String)
            Dim filterList As New List(Of String)
            Dim filterValues As New Dictionary(Of String, Object)

            Dim parameterList As New List(Of String)
            Dim parameterValues As New Dictionary(Of String, Object)


            For i As Integer = 0 To table.PrimaryKey.Length - 1
                Dim column As DataColumn = table.PrimaryKey(i)
                If newRow.IsNull(column) Then
                    Throw New ArgumentNullException(column.ColumnName, "Primary key filter value null")
                Else
                    Dim pName As String = String.Format("PK{0}", i)
                    filterList.Add(String.Format("([{0}]=@{1})", column.ColumnName, pName))
                    filterValues.Add(pName, newRow(column))
                End If
            Next

            If filterList.Count = 0 Then
                Throw New ArgumentException("newRow", "Primary key filter missing")
            End If

            For i As Integer = 0 To table.Columns.Count - 1
                Dim column As DataColumn = table.Columns(i)
                If Not String.IsNullOrEmpty(column.Expression) Then Continue For ' can't update expression columns
                If column.AutoIncrement Then Continue For ' not gonna be updating auto-numbers.

                If column.ColumnName = STR_CreatedDateTime Then Continue For ' ignore createdTime column
                If column.ColumnName = STR_ModifiedDateTime Then Continue For ' ignore modifiedTime column
                ' if both are null, or if their values are equal, don't update 'em.
                If originalRow.IsNull(i) AndAlso newRow.IsNull(i) Then Continue For
                If Not originalRow.IsNull(i) AndAlso Not newRow.IsNull(i) AndAlso originalRow.Item(i).Equals(newRow.Item(i)) Then Continue For

                If newRow.IsNull(i) Then
                    parameterList.Add(String.Format("[{0}] = null", column.ColumnName))
                Else
                    Dim argName As String = String.Format("ARG{0}", column.ColumnName)
                    parameterList.Add(String.Format("[{0}] = @{1}", column.ColumnName, argName))
                    parameterValues.Add(argName, newRow(i))
                End If
            Next
            If parameterList.Count = 0 Then
                Return False ' nothing to update.
            End If

            Dim sql As String = String.Format("UPDATE {0} SET {1} WHERE {2}", _
                table.TableName, _
                String.Join(",", parameterList.ToArray()), _
                String.Join(" AND ", filterList.ToArray()))

            Dim cmd As New SqlClient.SqlCommand(sql, New SqlClient.SqlConnection(My.Settings.activiserConnectionString))
            For Each filterName As String In filterValues.Keys
                cmd.Parameters.AddWithValue(filterName, filterValues(filterName))
            Next

            For Each argName As String In parameterValues.Keys
                cmd.Parameters.AddWithValue(argName, parameterValues(argName))
            Next

            cmd.Connection.Open()
            Dim result As Integer = cmd.ExecuteNonQuery
            cmd.Connection.Close()
            Return result <> 0
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function InsertRow(Of T As DataRow)(ByVal newRow As T) As Boolean
        If newRow Is Nothing Then Throw New ArgumentNullException("newRow")
        If newRow.ItemArray.Length = 0 Then Throw New ArgumentException("Row has no data", "newRow")

        Try
            Dim table As DataTable = newRow.Table
            Dim columnList As New List(Of String)
            Dim parameterList As New List(Of String)
            Dim parameterValues As New Dictionary(Of String, Object)

            ' sanity check
            For i As Integer = 0 To table.PrimaryKey.Length - 1
                Dim column As DataColumn = table.PrimaryKey(i)
                If newRow.IsNull(column) Then
                    Throw New ArgumentNullException(column.ColumnName, "Primary key value is null")
                End If
            Next

            For i As Integer = 0 To table.Columns.Count - 1
                Dim column As DataColumn = table.Columns(i)
                If Not String.IsNullOrEmpty(column.Expression) Then Continue For ' can't update expression columns
                If column.AutoIncrement Then Continue For ' not gonna be updating auto-numbers.

                columnList.Add(String.Format("[{0}]", table.Columns(i).ColumnName))

                If newRow.IsNull(i) AndAlso ((column.ColumnName = STR_CreatedDateTime) OrElse (column.ColumnName = STR_ModifiedDateTime)) Then
                    parameterList.Add("GETUTCDATE()")
                Else
                    Dim argName As String = String.Format("ARG{0}", i)
                    parameterList.Add(String.Format("@{0}", argName))
                    If newRow.IsNull(i) Then
                        parameterValues.Add(argName, DBNull.Value)
                    Else
                        parameterValues.Add(argName, newRow(i))
                    End If
                End If
            Next

            Dim sql As String = String.Format("INSERT [{0}]({1}) VALUES({2})", _
                table.TableName, _
                String.Join(",", columnList.ToArray()), _
                String.Join(",", parameterList.ToArray()))

            Dim cmd As New SqlClient.SqlCommand(sql, New SqlClient.SqlConnection(My.Settings.activiserConnectionString))
            For Each argName As String In parameterValues.Keys
                cmd.Parameters.AddWithValue(argName, parameterValues(argName))
            Next

            cmd.Connection.Open()
            Dim result As Integer = cmd.ExecuteNonQuery
            cmd.Connection.Close()
            Return result <> 0
        Catch ex As Exception
            Throw
        End Try
    End Function

    <Serializable()> _
    Friend Class InsertException
        Inherits System.Exception

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub

        Protected Sub New( _
            ByVal info As System.Runtime.Serialization.SerializationInfo, _
            ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
    End Class

    <Serializable()> _
    Friend Class UpdateException
        Inherits System.Exception

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub

        Protected Sub New( _
            ByVal info As System.Runtime.Serialization.SerializationInfo, _
            ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
    End Class
End Class
