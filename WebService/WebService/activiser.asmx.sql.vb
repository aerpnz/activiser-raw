Imports activiser
Imports activiser.WebService.Utilities
Imports System
Imports System.Reflection
Imports System.Data
Imports System.Collections.Generic

Partial Public Class activiserClientWebService

    Private Shared Function CleanUploadedRequest(ByVal RequestItem As activiserDataSet.RequestRow, ByVal syncTime As DateTime, ByVal consultantUid As Guid, ByVal isClientUpdate As Boolean) As Boolean
        If RequestItem.RowState <> DataRowState.Deleted Then
            Try
                Dim result As Boolean = False

                RequestItem.ModifiedDateTime = syncTime

                Dim existingRequest As activiserDataSet.RequestRow = FetchRow(RequestItem.Table, RequestItem)

                If existingRequest IsNot Nothing Then
                    'if a request has been added by a client, but there was a subsequent failure in the sync process before information was fed back to the PDA,
                    'then it is possible that the RequestID and RequestNumber are still null from the client's perspective.
                    If Not existingRequest.IsRequestNumberNull AndAlso _
                        (RequestItem.IsRequestNumberNull OrElse (RequestItem.RequestNumber <> existingRequest.RequestNumber)) Then
                        RequestItem.RequestNumber = existingRequest.RequestNumber
                    End If

                    If Not existingRequest.IsRequestIDNull AndAlso _
                        (RequestItem.IsRequestIDNull OrElse RequestItem.RequestID <> existingRequest.RequestID) Then
                        RequestItem.RequestID = existingRequest.RequestID
                    End If
                End If
            Catch ex As Exception
                Throw
            End Try
        End If
    End Function


    Private Shared Sub UpdateJobStatusId(ByVal JobItem As activiserDataSet.JobRow)
        If JobItem.JobStatusID = JobStatusCodes.Complete Then 'If the status is complete, change it to completed and synched
            JobItem.JobStatusID = JobStatusCodes.CompleteAndSynchronised
        ElseIf JobItem.JobStatusID = JobStatusCodes.Signed Then 'If the status is signed, change it to signed and synched
            JobItem.JobStatusID = JobStatusCodes.SignedAndSynchronised
        ElseIf JobItem.IsJobStatusIDNull Then
            JobItem.JobStatusID = 0
        End If 
    End Sub

    Private Shared Function CleanUploadedJob(ByVal JobItem As activiserDataSet.JobRow, ByVal syncTime As DateTime, ByVal consultantUid As Guid, ByVal isClientUpdate As Boolean) As Boolean
        If JobItem.RowState <> DataRowState.Deleted Then
            Try
                Dim result As Boolean = False

                JobItem.ModifiedDateTime = syncTime

                Dim existingJob As activiserDataSet.JobRow = FetchRow(JobItem.Table, JobItem)

                If existingJob IsNot Nothing Then
                    'if a job has been added by a client, but there was a subsequent failure in the sync process before information was fed back to the PDA,
                    'then it is possible that the RequestID and RequestNumber are still null from the client's perspective.
                    If Not existingJob.IsJobNumberNull AndAlso _
                        (JobItem.IsJobNumberNull OrElse (JobItem.JobNumber <> existingJob.JobNumber)) Then
                        JobItem.JobNumber = existingJob.JobNumber
                    End If

                    If Not existingJob.IsJobIDNull AndAlso _
                        (JobItem.IsJobIDNull OrElse JobItem.JobID <> existingJob.JobID) Then
                        JobItem.JobID = existingJob.JobID
                    End If
                End If

                If isClientUpdate AndAlso existingJob IsNot Nothing Then
                    ' note: JobStatusID 0 = draft, 1 = complete, 2 = signed. Anything else implies sync'd with server.
                    If ((existingJob.JobStatusID > 2) OrElse (existingJob.ConsultantUID <> consultantUid) OrElse ((Not existingJob.IsFlagNull) AndAlso existingJob.Flag <> 0)) Then ' job has previously moved out of draft or been 'approved'
                        If (existingJob.JobNotes <> JobItem.JobNotes) Then ' only allowable update
                            Dim jobNotes As String = JobItem.JobNotes
                            For Each c As DataColumn In existingJob.Table.Columns
                                If Not c.ReadOnly Then
                                    JobItem.Item(c) = existingJob.Item(c)
                                End If
                            Next
                            JobItem.JobNotes = jobNotes
                            JobItem.ModifiedDateTime = syncTime
                        End If
                    Else
                        UpdateJobStatusId(JobItem)
                    End If
                Else
                    UpdateJobStatusId(JobItem)
                End If

            Catch ex As Exception
                Throw
            End Try
        End If
    End Function


    Private Shared Function AddRequestToUploadResults(ByVal uploadResults As UploadResults, ByVal updatedRequest As activiserDataSet.RequestRow) As activiserDataSet.RequestRow
        Dim requestTimeReturn As Date = If(updatedRequest.IsModifiedDateTimeNull, DateTime.UtcNow, updatedRequest.ModifiedDateTime)
        Dim requestIdReturn As Integer = If(Not updatedRequest.IsRequestIDNull, updatedRequest.RequestID, -1)
        Dim requestNumberReturn As String = If(updatedRequest.IsRequestNumberNull OrElse String.IsNullOrEmpty(updatedRequest.RequestNumber), CStr(requestIdReturn), updatedRequest.RequestNumber)

        uploadResults.Result.AddResultRow("R", updatedRequest.RequestUID, requestIdReturn, requestNumberReturn, requestTimeReturn, RequestChecksum(updatedRequest))
        Return updatedRequest
    End Function

    Private Shared Function AddJobToUploadResults(ByVal uploadResults As UploadResults, ByVal updatedJob As activiserDataSet.JobRow) As activiserDataSet.JobRow
        Dim jobTimeReturn As Date = If(Not updatedJob.IsModifiedDateTimeNull, updatedJob.ModifiedDateTime, DateTime.UtcNow)
        Dim jobIdReturn As Integer = If(Not updatedJob.IsJobIDNull, updatedJob.JobID, -1)
        Dim jobNumberReturn As String = If(updatedJob.IsJobNumberNull OrElse String.IsNullOrEmpty(updatedJob.JobNumber), CStr(jobIdReturn), updatedJob.JobNumber)

        uploadResults.Result.AddResultRow("J", updatedJob.JobUID, jobIdReturn, jobNumberReturn, jobTimeReturn, JobChecksum(updatedJob))
        Return updatedJob
    End Function

    ''TODO: Add concurrency checks to this.
    'Private Shared Function SaveRequest(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal uploadResults As UploadResults, ByVal cpta As ConsultantProfileTableAdapters.ConsultantProfileEntryTableAdapter, ByVal RequestItem As activiserDataSet.RequestRow, ByVal syncTime As DateTime) As Boolean
    '    If RequestItem.RowState <> DataRowState.Deleted Then
    '        Try
    '            Dim result As Boolean = False

    '            RequestItem.ModifiedDateTime = syncTime

    '            Dim EventTypeId As Integer

    '            Dim existingRequest As activiserDataSet.RequestRow = FetchRow(RequestItem.Table, RequestItem)
    '            Dim updatedRequest As activiserDataSet.RequestRow

    '            If existingRequest IsNot Nothing Then
    '                'if a request has been added by a client, but there was a subsequent failure in the sync process before information was fed back to the PDA,
    '                'then it is possible that the RequestID and RequestNumber are still null from the client's perspective.
    '                If Not existingRequest.IsRequestNumberNull AndAlso (RequestItem.IsRequestNumberNull OrElse String.IsNullOrEmpty(RequestItem.RequestNumber) OrElse (Not RequestItem.IsRequestIDNull AndAlso (RequestItem.RequestNumber = CStr(RequestItem.RequestID)))) Then
    '                    RequestItem.RequestNumber = existingRequest.RequestNumber
    '                End If

    '                If Not existingRequest.IsRequestIDNull AndAlso (RequestItem.IsRequestIDNull OrElse RequestItem.RequestID < 0) Then
    '                    RequestItem.RequestID = existingRequest.RequestID
    '                End If

    '                EventTypeId = EventTypeId_RequestUpdated
    '                result = UpdateRow(existingRequest, RequestItem)
    '                If result Then
    '                    updatedRequest = FetchRow(RequestItem.Table, RequestItem)
    '                Else
    '                    updatedRequest = existingRequest
    '                End If
    '            Else
    '                EventTypeId = EventTypeId_RequestInserted
    '                result = InsertRow(RequestItem)
    '                If result Then
    '                    updatedRequest = FetchRow(RequestItem.Table, RequestItem)
    '                Else
    '                    Throw New InsertException("Error inserting new request row")
    '                End If
    '            End If

    '            AddRequestToUploadResults(uploadResults, updatedRequest)

    '            LogEvent(consultantUid, RequestItem.RequestUID, Nothing, RequestItem.ClientSiteUID, EventTypeId)
    '            Return result

    '        Catch ex As Exception
    '            LogError(consultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error saving request", "Request", RequestItem.RequestUID, SerialiseDataRow(RequestItem).OuterXml, ex)
    '        End Try
    '    End If
    'End Function

    ' Enqueue jobs for mailing; we actually enqueue all of them, and then allow the mailer process to decide
    ' whether or not to do any mailing. This will allow triggers to set or modify the emailaddress information prior
    ' to an attempt to send the email.
    Private Shared Sub EnqueueJobMail(ByRef emailQueue As Collections.Generic.Queue(Of Guid), ByVal job As activiserDataSet.JobRow)
        If Not Utility.GetServerSetting(My.Resources.SendJobEmailSettingKey, False) Then Return

        emailQueue.Enqueue(job.JobUID)
    End Sub

    'TODO: Add concurrency checks to this.
    'Private Shared Function SaveJob(ByVal deviceId As String, ByVal consultantUid As Guid, ByRef uploadResults As UploadResults, ByRef cpta As ConsultantProfileTableAdapters.ConsultantProfileEntryTableAdapter, ByRef JobItem As activiserDataSet.JobRow, ByRef jta As ClientDataSetTableAdapters.JobTableAdapter, ByRef emailQueue As Collections.Generic.Queue(Of Guid), ByVal syncTime As DateTime, ByVal isClientUpdate As Boolean) As Boolean
    '    If JobItem.RowState = DataRowState.Deleted Then
    '        JobItem.RejectChanges()
    '        If JobItem.JobStatusID = JobStatusCodes.Draft Then
    '            JobItem.JobStatusID = JobStatusCodes.Deleted
    '            JobItem.SetModified()
    '        Else
    '            Return False
    '        End If
    '    End If

    '    If (JobItem.RowState And (DataRowState.Added Or DataRowState.Modified Or DataRowState.Unchanged)) <> 0 Then
    '        Dim result As Boolean = False
    '        If JobItem.IsFlagNull Then
    '            JobItem.Flag = 0
    '        End If

    '        If JobItem.JobStatusID = JobStatusCodes.Complete Then 'If the status is complete, change it to completed and synched
    '            JobItem.JobStatusID = JobStatusCodes.CompleteAndSynchronised
    '        ElseIf JobItem.JobStatusID = JobStatusCodes.Signed Then 'If the status is signed, change it to signed and synched
    '            JobItem.JobStatusID = JobStatusCodes.SignedAndSynchronised
    '        ElseIf JobItem.IsJobStatusIDNull Then
    '            JobItem.JobStatusID = 0
    '        End If

    '        JobItem.ModifiedDateTime = syncTime

    '        Try
    '            Dim EventTypeId As Integer

    '            Dim existingJob As activiserDataSet.JobRow = FetchRow(JobItem.Table, JobItem)
    '            Dim updatedjob As activiserDataSet.JobRow

    '            If existingJob IsNot Nothing Then
    '                'if a Job has been added by a client, but there was a subsequent failure in the sync process before information was fed back to the PDA,
    '                'then it is possible that the JobID and JobNumber are still null from the client's perspective.
    '                If Not existingJob.IsJobNumberNull AndAlso JobItem.IsJobNumberNull Then ' OrElse String.IsNullOrEmpty(JobItem.JobNumber) OrElse (Not JobItem.IsJobIDNull AndAlso (JobItem.JobNumber = CStr(JobItem.JobID)))) Then
    '                    JobItem.JobNumber = existingJob.JobNumber
    '                End If

    '                If Not existingJob.IsJobIDNull AndAlso JobItem.IsJobIDNull Then ' OrElse JobItem.JobID < 0) Then
    '                    JobItem.JobID = existingJob.JobID
    '                End If

    '                EventTypeId = EventTypeId_JobUpdated

    '                If isClientUpdate AndAlso ((existingJob.Flag <> 0) OrElse (existingJob.JobStatusID <> 0)) Then ' job has previously moved out of draft or been 'approved'
    '                    updatedjob = existingJob ' assume most likely case
    '                    If (existingJob.JobNotes <> JobItem.JobNotes) Then ' only allowable update
    '                        Dim newJob As activiserDataSet.JobRow = CType(existingJob.Table.NewRow(), activiserDataSet.JobRow)
    '                        newJob.ItemArray = existingJob.ItemArray
    '                        newJob.JobNotes = JobItem.JobNotes
    '                        newJob.ModifiedDateTime = syncTime
    '                        result = UpdateRow(existingJob, newJob)
    '                        If result Then
    '                            updatedjob = FetchRow(existingJob.Table, newJob)
    '                        End If
    '                    End If
    '                Else
    '                    result = UpdateRow(existingJob, JobItem)
    '                    If result Then
    '                        updatedjob = FetchRow(JobItem.Table, JobItem)
    '                    Else
    '                        updatedjob = existingJob
    '                    End If
    '                End If
    '            Else
    '                EventTypeId = EventTypeId_JobInserted
    '                result = InsertRow(JobItem)
    '                If result Then
    '                    updatedjob = FetchRow(JobItem.Table, JobItem)
    '                Else
    '                    updatedjob = Nothing
    '                    Throw New InsertException("Error inserting new job row")
    '                End If
    '            End If

    '            EnqueueJobMail(emailQueue, updatedjob)
    '            AddJobToUploadResults(uploadResults, updatedjob)

    '            LogEvent(consultantUid, If(JobItem.IsRequestUIDNull, Nothing, JobItem.RequestUID), JobItem.JobUID, If(JobItem.RequestRow Is Nothing, Nothing, JobItem.RequestRow.ClientSiteUID), EventTypeId)
    '            Return result

    '        Catch ex As Exception
    '            LogError(consultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error saving job", "job", JobItem.JobUID, SerialiseDataRow(JobItem).OuterXml, ex)
    '        End Try

    '        'Else
    '        '    If cpta Is Nothing Then Return True
    '        '    Try
    '        '        JobItem.RejectChanges()
    '        '        If JobItem.JobStatusID = JobStatusCodes.Draft Then
    '        '            'TODO: Test & Enable this
    '        '            'DeleteCustomData(deviceId, consultantUid, "Job", "JobUID", JobItem.JobUID)
    '        '            jta.Delete(JobItem.JobUID)
    '        '        End If

    '        '        'Dim cp As ConsultantProfile.ConsultantProfileEntryDataTable = cpta.GetDataByConsultantUidAndItemUid(consultantUid, "J", JobItem.JobUID)
    '        '        'If cp.Count <> 0 Then
    '        '        '    For Each cpr As ConsultantProfile.ConsultantProfileEntryRow In cp
    '        '        '        cpr.ItemDeleted = True
    '        '        '        cpta.Update(cpr)
    '        '        '    Next
    '        '        'End If
    '        '    Catch ex As Exception
    '        '        LogError(consultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error processing deleted job", "Job", JobItem.JobUID, "", ex)
    '        '    End Try

    '    End If
    'End Function

    'Private Shared Function UpsertRow(Of T As DataTable, R As DataRow)(ByVal table As T, ByVal newRow As R) As Integer
    '    ' pseudo code:
    '    ' then attempt fetch of row from database
    '    ' if no row, then 
    '    '      insert new one
    '    ' else 
    '    '      compare existing row with new row
    '    '      if changes then
    '    '          update modified columns only
    '    '      else
    '    '          ignore and return
    '    Try
    '        'TODO: add concurrency checks
    '        'Dim originalTable As DataTable = newRow.Table.Clone
    '        'originalTable.RejectChanges()
    '        'Dim originalRow As DataRow
    '        'Dim table As DataTable = newRow.Table

    '        Dim existingRow As R = FetchRow(table, newRow)

    '        If existingRow IsNot Nothing Then
    '            If UpdateRow(existingRow, newRow) Then
    '                Return 1
    '            Else
    '                Return 0
    '            End If
    '        Else
    '            If InsertRow(newRow) Then
    '                Return 3
    '            Else
    '                Return 2
    '            End If
    '        End If


    '    Catch ex As Exception
    '        LogError(Guid.Empty, DateTime.UtcNow, WebServiceGuid.ToString, "UpsertRow", "General expection", newRow.Table.TableName, Guid.Empty, SerialiseDataRow(newRow).OuterXml, ex)
    '    End Try


    'End Function


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
    'Private Shared Function UpdateRow(Of T As DataRow)(ByVal originalRow As T, ByVal newRow As T) As Boolean
    '    Try
    '        Dim table As DataTable = newRow.Table

    '        'Dim columnList As New List(Of String)
    '        Dim filterList As New List(Of String)
    '        Dim filterValues As New Dictionary(Of String, Object)

    '        Dim parameterList As New List(Of String)
    '        Dim parameterValues As New Dictionary(Of String, Object)


    '        For i As Integer = 0 To table.PrimaryKey.Length - 1
    '            Dim column As DataColumn = table.PrimaryKey(i)
    '            If newRow.IsNull(column) Then
    '                Throw New ArgumentNullException(column.ColumnName, "Primary key filter value null")
    '            Else
    '                Dim pName As String = String.Format("PK{0}", i)
    '                filterList.Add(String.Format("([{0}]=@{1})", column.ColumnName, pName))
    '                filterValues.Add(pName, newRow(column))
    '            End If
    '        Next

    '        If filterList.Count = 0 Then
    '            Throw New ArgumentException("newRow", "Primary key filter missing")
    '        End If

    '        For i As Integer = 0 To table.Columns.Count - 1
    '            Dim column As DataColumn = table.Columns(i)
    '            If Not String.IsNullOrEmpty(column.Expression) Then Continue For ' can't update expression columns
    '            If column.AutoIncrement Then Continue For ' not gonna be updating auto-numbers.

    '            If column.ColumnName = STR_CreatedDateTime Then Continue For ' ignore createdTime column
    '            If column.ColumnName = STR_ModifiedDateTime Then Continue For ' ignore modifiedTime column
    '            ' if both are null, or if their values are equal, don't update 'em.
    '            If originalRow.IsNull(i) AndAlso newRow.IsNull(i) Then Continue For
    '            If Not originalRow.IsNull(i) AndAlso Not newRow.IsNull(i) AndAlso originalRow.Item(i).Equals(newRow.Item(i)) Then Continue For

    '            If newRow.IsNull(i) Then
    '                parameterList.Add(String.Format("[{0}] = null", column.ColumnName))
    '            Else
    '                Dim argName As String = String.Format("ARG{0}", column.ColumnName)
    '                parameterList.Add(String.Format("[{0}] = @{1}", column.ColumnName, argName))
    '                parameterValues.Add(argName, newRow(i))
    '            End If
    '        Next
    '        If parameterList.Count = 0 Then
    '            Return False ' nothing to update.
    '        End If

    '        Dim sql As String = String.Format("UPDATE {0} SET {1} WHERE {2}", _
    '            table.TableName, _
    '            String.Join(",", parameterList.ToArray()), _
    '            String.Join(" AND ", filterList.ToArray()))

    '        Dim cmd As New SqlClient.SqlCommand(sql, New SqlClient.SqlConnection(My.Settings.activiserConnectionString))
    '        For Each filterName As String In filterValues.Keys
    '            cmd.Parameters.AddWithValue(filterName, filterValues(filterName))
    '        Next

    '        For Each argName As String In parameterValues.Keys
    '            cmd.Parameters.AddWithValue(argName, parameterValues(argName))
    '        Next

    '        cmd.Connection.Open()
    '        Dim result As Integer = cmd.ExecuteNonQuery
    '        cmd.Connection.Close()
    '        Return result <> 0
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Private Shared Function InsertRow(Of T As DataRow)(ByVal newRow As T) As Boolean
    '    If newRow Is Nothing Then Throw New ArgumentNullException("newRow")
    '    If newRow.ItemArray.Length = 0 Then Throw New ArgumentException("Row has no data", "newRow")

    '    Try
    '        Dim table As DataTable = newRow.Table
    '        Dim columnList As New List(Of String)
    '        Dim parameterList As New List(Of String)
    '        Dim parameterValues As New Dictionary(Of String, Object)

    '        ' sanity check
    '        For i As Integer = 0 To table.PrimaryKey.Length - 1
    '            Dim column As DataColumn = table.PrimaryKey(i)
    '            If newRow.IsNull(column) Then
    '                Throw New ArgumentNullException(column.ColumnName, "Primary key value is null")
    '            End If
    '        Next

    '        For i As Integer = 0 To table.Columns.Count - 1
    '            Dim column As DataColumn = table.Columns(i)
    '            If Not String.IsNullOrEmpty(column.Expression) Then Continue For ' can't update expression columns
    '            If column.AutoIncrement Then Continue For ' not gonna be updating auto-numbers.

    '            columnList.Add(String.Format("[{0}]", table.Columns(i).ColumnName))

    '            If newRow.IsNull(i) AndAlso ((column.ColumnName = STR_CreatedDateTime) OrElse (column.ColumnName = STR_ModifiedDateTime)) Then
    '                parameterList.Add("GETUTCDATE()")
    '            Else
    '                Dim argName As String = String.Format("ARG{0}", i)
    '                parameterList.Add(String.Format("@{0}", argName))
    '                If newRow.IsNull(i) Then
    '                    parameterValues.Add(argName, DBNull.Value)
    '                Else
    '                    parameterValues.Add(argName, newRow(i))
    '                End If
    '            End If
    '        Next

    '        Dim sql As String = String.Format("INSERT [{0}]({1}) VALUES({2})", _
    '            table.TableName, _
    '            String.Join(",", columnList.ToArray()), _
    '            String.Join(",", parameterList.ToArray()))

    '        Dim cmd As New SqlClient.SqlCommand(sql, New SqlClient.SqlConnection(My.Settings.activiserConnectionString))
    '        For Each argName As String In parameterValues.Keys
    '            cmd.Parameters.AddWithValue(argName, parameterValues(argName))
    '        Next

    '        cmd.Connection.Open()
    '        Dim result As Integer = cmd.ExecuteNonQuery
    '        cmd.Connection.Close()
    '        Return result <> 0
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

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
