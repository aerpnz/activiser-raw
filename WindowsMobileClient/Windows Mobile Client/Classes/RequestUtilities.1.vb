'Imports WS = Activiser.WebService
'Imports activiser.WebService
Imports activiser.Library
Imports activiser.Library.WebService
Imports activiser.Library.WebService.activiserDataSet
Imports System.Collections.Generic
Imports activiser.Library.WebService.FormDefinition

Module RequestUtilities
    Const MODULENAME As String = "RequestUtilities"
    Public Structure RequestStatusMenuItem
        Friend RequestStatusID As Integer
        Friend IsClientMenuItem As Boolean
        Friend IsReasonRequired As Boolean
        Friend RequestMenuItem As MenuItem
        Friend FilterMenuItem As MenuItem
        Friend MainMenuItem As MenuItem

        Sub New(ByVal dr As RequestStatusRow, ByVal requestMenuItem As MenuItem, ByVal filterMenuItem As MenuItem, ByVal mainMenuItem As MenuItem)
            RequestStatusID = dr.RequestStatusID
            Me.RequestMenuItem = requestMenuItem
            Me.FilterMenuItem = filterMenuItem
            Me.MainMenuItem = mainMenuItem
            IsClientMenuItem = dr.IsClientMenuItem
            IsReasonRequired = dr.IsReasonRequired
        End Sub
    End Structure


    Public Function RequestStatus(ByVal Id As Integer) As RequestStatusRow
        Return gClientDataSet.RequestStatus.FindByRequestStatusID(Id)
    End Function

    Public Function RequestLocked(ByVal request As RequestRow) As Boolean
        Dim rs As RequestStatusRow = RequestStatus(request.RequestStatusID)
        Return (Not AppConfig.GetSetting(My.Resources.AppConfigAllowJobsForClosedRequestsKey, True)) _
                    AndAlso (rs.IsCompleteStatus OrElse rs.IsCancelledStatus)
    End Function

    Public Function IsMyRequest(ByVal drow As RequestRow) As Boolean
        Return ((Not drow.IsAssignedToUIDNull) AndAlso (drow.AssignedToUID.Equals(gConsultantUID)))
    End Function

    'Private Sub RemoveCustomData(ByVal Request As RequestRow)
    '    For Each cfr As WebService.FormDefinition.FormRow In gFormDefinitions.Form.Select("ParentEntity='Request'", gFormDefinitions.Form.PriorityColumn.ColumnName)
    '        If gClientDataSet.Tables.Contains(cfr.EntityName) Then
    '            Dim dt As DataTable = gClientDataSet.Tables(cfr.EntityName)
    '            If dt IsNot Nothing Then
    '                Const STR_FilterTemplate As String = "[{0}] = '{1}'"
    '                For Each customRow As DataRow In dt.Select(String.Format(WithoutCulture, STR_FilterTemplate, cfr.EntityParentFK, Request.RequestUID))
    '                    If Not customRow.RowState = DataRowState.Deleted Then customRow.Delete()
    '                Next
    '            End If
    '        End If
    '    Next
    'End Sub

    Private WithEvents _pb As PopupBox

    Public Sub RemoveRequest(ByVal owner As Form, ByVal Request As RequestRow)
        Const METHODNAME As String = "RemoveRequest"

        If Request.RowState = DataRowState.Deleted Then Return

        Dim currentCursor As Cursor = Cursor.Current

        If Not System.Windows.Forms.Cursor.Current Is Cursors.WaitCursor Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        End If

        'Get All related jobs with deleted request
        Dim requestJobs As JobRow() = Request.GetJobRows
        If requestJobs.Length > 0 Then
            For Each jobRow As JobRow In requestJobs
                If Not jobRow.RowState = DataRowState.Unchanged Then
                    Dim requestId As String
                    If Request.IsRequestNumberNull OrElse String.IsNullOrEmpty(Request.RequestNumber) Then
                        If Not Request.IsRequestIDNull Then
                            requestId = Request.RequestID.ToString(WithCulture)
                        Else
                            requestId = Terminology.GetString(My.Resources.SharedMessagesKey, RES_RequestNumberUnknown)
                        End If
                    Else
                        requestId = Request.RequestNumber
                    End If
                    Select Case Terminology.AskFormattedQuestion(owner, MODULENAME, RES_RequestConfirmRemoveDirty, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, requestId)
                        Case DialogResult.No
                            Exit Sub
                        Case DialogResult.Yes
                            Exit For
                    End Select
                End If
            Next

            For Each jr As JobRow In requestJobs
                RemoveJob(jr)
            Next
        End If
        Try
            DeleteCustomData("Request", Request.RequestUID)
            ConsultantConfig.RemoveItem("Request", Request.RequestUID)
            Request.Delete() ' delete the row so that its removal is part of the tx log
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            System.Windows.Forms.Cursor.Current = currentCursor
        End Try
        'gClientDataSet.Request.RemoveRequestRow(Request)
    End Sub


    Private Sub RemoveJobRows(ByVal rr As RequestRow)
        Dim jq = New Generic.List(Of JobRow)
        jq.AddRange(rr.GetJobRows)

        For Each jr As JobRow In jq
            RemoveCustomData("Job", jr.JobUID)
            ConsultantConfig.RemoveItem("Job", jr.JobUID)
            gClientDataSet.Job.RemoveJobRow(jr)
        Next
    End Sub

    '''
    ''' Note, unlike the individual remove request method, this one will do a full save at the end of the process, so
    ''' it isn't necessary to 'delete' the rows; we can just 'remove' them, which should be faster.
    '''
    Public Function RemoveClosedRequests(ByVal owner As Form) As Boolean

        Try
            If Terminology.AskQuestion(owner, MODULENAME, RES_ConfirmRequestCleanup, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Dim clientInfoToo As Boolean = False
                'Dim clientVictims As List(Of ClientSiteRow)
                Dim requestsSkipped As Integer = 0
                Dim requestsRemoved As Integer = 0

                'Select Case Terminology.AskQuestion(owner, MODULENAME, RES_RequestCleanupClientsToo, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button2)
                '    Case Windows.Forms.DialogResult.Yes
                '        clientInfoToo = True
                '        clientVictims = New List(Of ClientSiteRow)
                '    Case Windows.Forms.DialogResult.No
                '        clientInfoToo = False
                '    Case Windows.Forms.DialogResult.Cancel
                '        Return False
                'End Select

                _pb = New PopupBox(owner, Terminology.GetFormattedString(MODULENAME, "WaitMessage", requestsRemoved), 0) '"Removing redundant requests, please wait...", 0)
                _pb.OkYesButton.Enabled = False
                _pb.Show()
                _pb.Refresh()

                Cursor.Current = Cursors.WaitCursor

                ' get list of completed/cancelled request statuseseses
                Dim rsl As New List(Of Integer)
                Dim rq As New List(Of RequestRow)
                For Each rsr As RequestStatusRow In gClientDataSet.RequestStatus
                    If rsr.IsCancelledStatus OrElse rsr.IsCompleteStatus Then
                        rsl.Add(rsr.RequestStatusID)
                    End If
                Next

                'build list of requests using the previously collected status set.
                For Each rr As RequestRow In gClientDataSet.Request
                    If rsl.Contains(rr.RequestStatusID) Then rq.Add(rr)
                Next

                'Dim rq = (From rr As RequestRow In gClientDataSet.Request.Cast(Of RequestRow)() _
                '            Join rsr As RequestStatusRow In gClientDataSet.RequestStatus.Cast(Of RequestStatusRow)() _
                '                On rr.RequestStatusID Equals rsr.RequestStatusID _
                '            Where rsr.IsCompleteStatus OrElse rsr.IsCancelledStatus _
                '            Select rr).ToArray()

                For Each rr As RequestRow In rq
                    ' if we have incomplete jobs, we'll skip this one.
                    Dim abortThisRequest As Boolean = False
                    For Each jr As JobRow In rr.GetJobRows
                        If Not JobOkForDeletion(jr) Then 'If jr.JobStatusID = JobStatusCodes.Draft orelse 
                            abortThisRequest = True
                            requestsSkipped += 1
                            Exit For
                        End If
                    Next

                    If abortThisRequest Then
                        Continue For
                    End If

                    RemoveJobRows(rr)

                    RemoveCustomData("Request", rr.RequestUID)

                    'If clientInfoToo Then
                    '    If Not clientVictims.Contains(rr.ClientSiteRow) Then
                    '        clientVictims.Add(rr.ClientSiteRow)
                    '    End If
                    'End If

                    ConsultantConfig.RemoveItem("Request", rr.RequestUID)
                    gClientDataSet.Request.RemoveRequestRow(rr) ' note: not deleting them - that would potentially delete them on the server !
                    If _pb.Canceled Then
                        Exit For
                    End If
                    requestsRemoved += 1
                    _pb.Message = Terminology.GetFormattedString(MODULENAME, "WaitMessage", requestsRemoved)
                    _pb.Refresh()
                Next

                If requestsRemoved <> 0 Then
                    _pb.Message = "Saving database..."
                    _pb.Refresh()
                    SaveCommitted(gClientDataSet, gMainDbFileName)
                    SavePending(gClientDataSet, gMainDbFileName)

                    _pb.Message = "Refreshing main form..."
                    _pb.Refresh()
                    gMainForm.PopulateRequestList()
                End If

                _pb.Close()
                If requestsSkipped <> 0 Then
                    Terminology.DisplayFormattedMessage(owner, MODULENAME, RES_RequestsNotRemoved, MessageBoxIcon.Asterisk, requestsRemoved, requestsSkipped)
                Else
                    Terminology.DisplayFormattedMessage(owner, MODULENAME, RES_RequestsRemoved, MessageBoxIcon.Asterisk, requestsRemoved)
                End If

                Return True
            End If
        Catch ex As Exception

        Finally
            Cursor.Current = Cursors.Default

        End Try

    End Function
    'Public Function GetNextConsultantRID() As Integer
    '    Try
    '        For Each rr As WebService.ClientDataSet.RequestRow In gClientDataSet.Request.Select("NOT ConsultantRID IS NULL", "ConsultantRID ASC")
    '            Return rr.ConsultantRID - 1
    '        Next
    '    Catch ex As Exception
    '    End Try
    '    Return -1
    'End Function

    Function GetWIPJob(ByVal Request As RequestRow, ByRef result As JobRow) As Integer
        Dim jobRows As JobRow() = Request.GetJobRows
        result = Nothing
        If jobRows.Length = 0 Then
            Return 0
        Else
            Dim wipRows As Integer = 0
            For Each jobRow As JobRow In jobRows
                If jobRow.RowState = DataRowState.Deleted Then Continue For
                If Not jobRow.ConsultantUID.Equals(gConsultantUID) Then Continue For
                If jobRow.JobStatusID = JobStatusCodes.Draft Then
                    wipRows += 1
                    If result Is Nothing OrElse result.JobStatusID <> JobStatusCodes.Draft Then
                        result = jobRow
                    End If
#If FRAMEWORK_VERSION_GE35 Then
                ElseIf jobRow.JobStatusID.InSet(JobStatusCodes.Complete, JobStatusCodes.Signed) Then
#Else
                ElseIf InSet(jobRow.JobStatusID, JobStatusCodes.Complete, JobStatusCodes.Signed) Then
#End If
                    If result Is Nothing Then
                        result = jobRow
                    End If
                End If
            Next
            Return wipRows
        End If
    End Function

    Function hasIncompleteJob(ByVal Request As RequestRow) As Boolean
        For Each jobRow As JobRow In Request.GetJobRows
            If jobRow.RowState <> DataRowState.Deleted AndAlso jobRow.JobStatusID < JobStatusCodes.Signed Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function ChangeRequestStatus(ByVal owner As Form, ByVal Request As RequestRow, ByVal NewState As Integer, ByVal IsReasonRequired As Boolean) As Boolean
        If Request Is Nothing Then
            Return False
        End If

        If IsReasonRequired Then
            Dim newStateDescription As String = mapStatus(NewState)
            'Start Creating new job here
            Dim newJob As JobRow = gClientDataSet.Job.NewJobRow
            Dim changeReason As String = Terminology.AskQuestion(owner, MODULENAME, RES_RequestStatusChangeReasonPrompt, "", True, 3500)
            If changeReason.Trim.Length <> 0 Then
                newJob.JobDetails = "STATUS CHANGE ( " & newStateDescription & " ) : " & changeReason.Trim()
            Else
                Return False
            End If
            newJob.ClientSiteUID = Request.ClientSiteUID
            newJob.ConsultantUID = gConsultantUID
            newJob.JobDate = DateTime.Now
            newJob.StartTime = DateTime.UtcNow
            newJob.FinishTime = newJob.StartTime
            newJob.JobUID = Guid.NewGuid
            newJob.MinutesTravelled = Request.RequestStatusID
            newJob.Flag = NewState
            newJob.JobStatusID = JobStatusCodes.StatusChange
            newJob.RequestUID = Request.RequestUID
            newJob.CreatedDateTime = DateTime.UtcNow
            newJob.ModifiedDateTime = DateTime.UtcNow
            gClientDataSet.Job.Rows.Add(newJob)
        End If

        Request.ConsultantStatusID = NewState
        If Request.RequestID < 0 Then ' locally created request, not yet sync'd with server.
            Request.RequestStatusID = NewState
        End If
        Request.ModifiedDateTime = DateTime.UtcNow
        StartSavePending(gClientDataSet, gMainDbFileName)
        Return True
    End Function

    Public Function mapStatus(ByVal statusNo As Integer) As String
        Const METHODNAME As String = "mapStatus"
        Try
            Dim returnString As String
            returnString = gClientDataSet.RequestStatus.FindByRequestStatusID(statusNo).Description
            Return returnString
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Return String.Empty
        End Try
    End Function

    Public Function NewJobsAllowed(ByRef request As RequestRow) As Boolean
        If request Is Nothing Then Return False
        Dim rs As RequestStatusRow = request.RequestStatusRowByFK_Request_RequestStatus
        If rs IsNot Nothing Then
            If (rs.IsCancelledStatus OrElse rs.IsCompleteStatus) Then
                If AppConfig.GetSetting(My.Resources.AppConfigAllowJobsForClosedRequestsKey, True) Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Public Function AllowNewJob(ByVal owner As Form, ByRef request As RequestRow) As Boolean
        If request Is Nothing Then Return False
        Dim rs As RequestStatusRow = request.RequestStatusRowByFK_Request_RequestStatus
        If rs IsNot Nothing Then
            If (rs.IsCancelledStatus OrElse rs.IsCompleteStatus) Then
                If AppConfig.GetSetting(My.Resources.AppConfigAllowJobsForClosedRequestsKey, True) Then
                    If Terminology.AskQuestion(owner, MODULENAME, RES_ConfirmJobForClosedRequest, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                        Return False
                    End If
                Else
                    Terminology.DisplayMessage(owner, MODULENAME, RES_RequestLockedMessage, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Public Function AllowNewJob(ByVal owner As Form, ByRef request As RequestRow, ByVal question As String) As Boolean
        Dim rs As RequestStatusRow = request.RequestStatusRowByFK_Request_RequestStatus
        If rs IsNot Nothing Then
            If (rs.IsCancelledStatus OrElse rs.IsCompleteStatus) Then
                If AppConfig.GetSetting(My.Resources.AppConfigAllowJobsForClosedRequestsKey, True) Then
                    If Terminology.AskQuestion(owner, MODULENAME, question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                        Return False
                    End If
                Else
                    Terminology.DisplayMessage(owner, MODULENAME, RES_RequestLockedMessage, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub _pb_PopupCanceled(ByVal sender As Object, ByVal e As System.EventArgs) Handles _pb.PopupCanceled
        _pb.Message2 = "Canceling, please wait while current operation completes."
    End Sub
End Module
