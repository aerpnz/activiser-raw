Imports activiser.Library
Imports activiser.Library.WebService
Imports activiser.Library.WebService.activiserDataSet
Imports System.ComponentModel
Imports System.Net

Partial Friend Class Synchronisation
    ' Private Const STR_ModifiedDateTime As String = "ModifiedDateTime"

    Friend Shared Function DataSetIsNullOrEmpty(ByVal ds As DataSet) As Boolean
        If ds Is Nothing Then Return True

        For Each dt As DataTable In ds.Tables
            If dt.Rows.Count <> 0 Then Return False
        Next
        Return True
    End Function


    Friend Shared Sub SetModifiedTimes(ByRef dsChanges As activiserDataSet, ByVal syncTime As DateTime)
        If dsChanges Is Nothing Then Return

        For Each dt As DataTable In dsChanges.Tables
            Dim c As Integer = dt.Columns.IndexOf(STR_ModifiedDateTime)
            If c = -1 Then Continue For ' error, strictly speaking.
            For Each dr As DataRow In dt.Rows
                If (dr.RowState And (DataRowState.Added Or DataRowState.Modified Or DataRowState.Unchanged)) <> 0 Then
                    dr.Item(c) = syncTime
                End If
            Next
        Next
        'Application.DoEvents()
    End Sub

    Friend Shared Sub ValidateRequestCheckSum(ByVal dsError As activiserDataSet, ByVal uid As Guid, ByVal id As Integer, ByVal number As String, ByVal checkSum As String, ByRef hasChecksumError As Boolean)
        Dim drPPCRequest As RequestRow = gClientDataSet.Request.FindByRequestUID(uid)
        Dim testCheckSum As String = RequestChecksum(drPPCRequest)
        If Not testCheckSum.Equals(checkSum) Then
            dsError.Request.ImportRow(drPPCRequest)
            hasChecksumError = True
            Debug.WriteLine(String.Format(WithoutCulture, "checksum error in Request: {0}", number))
        Else
            drPPCRequest.RequestID = id
            drPPCRequest.RequestNumber = number
            drPPCRequest.AcceptChanges()
        End If
    End Sub

    Friend Shared Sub ValidateJobCheckSum(ByVal dsError As activiserDataSet, ByVal uid As Guid, ByVal id As Integer, ByVal number As String, ByVal checkSum As String, ByRef hasChecksumError As Boolean)
        Dim drPPCJob As JobRow = gClientDataSet.Job.FindByJobUID(uid)
        Dim testCheckSum As String = JobChecksum(drPPCJob)
        If Not testCheckSum.Equals(checkSum) Then
            dsError.Job.ImportRow(drPPCJob)
            hasChecksumError = True
            Debug.WriteLine(String.Format(WithoutCulture, "checksum error in job: {0}", number))
        Else
            drPPCJob.JobID = id
            drPPCJob.JobNumber = number
            drPPCJob.AcceptChanges()
        End If
    End Sub

    Friend Shared Sub ValidateChecksums(ByVal returnDS As WebService.UploadResults, ByRef hasChecksumError As Boolean)
        LogSyncMessage(RES_ValidatingDataSet)
        Dim dsError As activiserDataSet = LoadExistingErrors()

        For Each dr As WebService.UploadResults.ResultRow In returnDS.Result
            'Application.DoEvents()
            Select Case Char.ToUpper(dr.Type(0), CultureInfo.InvariantCulture)
                Case "R"c
                    ValidateRequestCheckSum(dsError, dr.GUID, dr.ID, dr.Number, dr.Checksum, hasChecksumError)
                Case "J"c
                    ValidateJobCheckSum(dsError, dr.GUID, dr.ID, dr.Number, dr.Checksum, hasChecksumError)
            End Select
        Next dr
        'Application.DoEvents()
        If hasChecksumError Then
            LogSyncMessage(RES_LoggingErrors)
            dsError.WriteXml(IO.Path.Combine(gErrorFolder, My.Resources.ChecksumErrorXml))
        End If
        'Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Crude approach to avoiding bizarre errors with sync process, if there is some dodgy data in the dataset,
    ''' the sync will likely fail with a really ugly error
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Shared Sub CleanDataSet(ByVal ds As activiserDataSet)
        Serialisation.DealWithDataSetErrors(ds, True)
    End Sub

    Friend Shared Function StartSync() As DateTime
        Dim syncTime As DateTime

        Try
            LogSyncMessage(RES_ClientSyncStarting)
            syncTime = gWebServer.SyncStart(gDeviceIDString, gConsultantUID)
            SetSystemTime(syncTime)
        Catch ex As Win32Exception
            LogSyncMessage(RES_ErrorSettingSystemTime, ex.ErrorCode.ToString(CultureInfo.CurrentCulture), ex.NativeErrorCode.ToString(CultureInfo.CurrentCulture), ex.Message)
        End Try

        If gCancelSync Then Throw New SyncCanceledException()
        Return syncTime
    End Function

    Friend Shared Function CheckDeviceId() As Boolean
        Try
            If gWebServer.CheckDeviceId(gDeviceIDString) Then
                Return True
            Else
                Return False
            End If
        Catch ex As WebException
            Debug.WriteLine(String.Format("DeviceID Check Failed with Web Exception: {0}", ex.ToString()))
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Friend Shared Function CheckDeviceId(ByVal ws As WebService.activiser) As Boolean
        Try
            If ws.CheckDeviceId(gDeviceIDString) Then
                Return True
            Else
                Return False
            End If
        Catch ex As WebException
            Debug.WriteLine(String.Format("DeviceID Check Failed with Web Exception: {0}", ex.ToString()))
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
