Imports activiser.WebService
Imports System.Text

<System.Runtime.InteropServices.ComVisible(False)> _
Public Class SyncForm
    Const MODULENAME As String = "SyncForm"
    Const LogTimeFormat As String = "HH:mm:ss"

    'Friend Notification As Notification
    Const RES_ErrorGettingServerVersion As String = "Error getting server version"
    Private _syncOk As Boolean
    Private _ClientSite As WebService.ClientDataSet.ClientSiteRow
    Private _syncType As SyncType = SyncType.NormalSync
    Private _cancelSync As Boolean
    Private _startedAt As DateTime = DateTime.Now

    Private _autoClose As Boolean = ConfigurationSettings.GetBooleanValue(My.Resources.AutoCloseSyncFormKey, False)
    Private _verbose As Boolean = ConfigurationSettings.GetBooleanValue("VerboseSyncMessages", False)

    Private _gpsActive As Boolean

    Private _syncStatus As String = String.Empty
    Private _syncLog As String = String.Empty
    Private _syncLogBuilder As New StringBuilder(2000)
    Private _notificationText As String = String.Empty

    Private _notificationTemplate As String = Terminology.GetString(MODULENAME, "NotificationTemplate")

    Private Delegate Sub RefreshMeDelegate()
    Private Sub RefreshMe()
        If Me.InvokeRequired Then
            Dim d As New RefreshMeDelegate(AddressOf RefreshMe)
            Me.Invoke(d)
            Return
        End If
        Me.Refresh()
        Application.DoEvents()
    End Sub

#Region "Terminology Key Constants"
    Const RES_Authenticated As String = "Authenticated"
    Const RES_Authenticating As String = "Authenticating"
    Const RES_AuthenticationFailed As String = "AuthenticationFailed"
    Const RES_AutoSynchCompleteBanner As String = "AutoSynchCompleteBanner"
    Const RES_AutoSyncInProgressBanner As String = "AutoSyncInProgressBanner"
    Const RES_CancelConfirm As String = "ConfirmCancel"
    Const RES_Cancelled As String = "Cancelled"
    Const RES_CheckingSchema As String = "CheckingSchema"
    Const RES_CheckingSchemaNone As String = "CheckingSchemaNone"
    Const RES_ChecksumErrorMessage As String = "ChecksumErrorMessage"
    Const RES_CommsFailed As String = "CommsFailed"
    Const RES_CommsFailureMessage As String = "CommsFailureMessage"
    Const RES_CommsFailureQuestion As String = "CommsFailureQuestion"
    Const RES_Complete As String = "Complete"
    Const RES_DataErrorMessage As String = "DataErrorMessage"
    Const RES_DeviceAuthenticated As String = "DeviceAuthenticated"
    Const RES_DeviceAuthenticationFailed As String = "DeviceAuthenticationFailed"
    Const RES_Downloading As String = "Downloading"
    Const RES_ErrorSettingSystemTime As String = "Warning: Error setting system time"
    Const RES_GetServerVersion As String = "GettingServerVersion"
    Const RES_GettingClientDetails As String = "GettingClientDetails"
    Const RES_GettingClientHistory As String = "GettingClientHistory"
    Const RES_GettingClientRequests As String = "GettingClientRequests"
    Const RES_GotServerVersion As String = "GotServerVersion"
    Const RES_LoggingErrors As String = "LoggingErrors"
    Const RES_Merging As String = "Merging"
    Const RES_MessageLogFormat As String = "MessageLogFormat"
    Const RES_NoChecksumDataException As String = "NoChecksumDataMessage"
    Const RES_NoDataNote As String = "NoDataNote"
    Const RES_NoDataQuestion As String = "NoDataQuestion"
    Const RES_NotificationInProgress As String = "NotificationInProgress"
    Const RES_ProfileSyncing As String = "ProfileSyncing"
    Const RES_RefreshingClientListMessage As String = "RefreshingClientListMessage"
    Const RES_Saving As String = "Saving"
    Const RES_SaveCompleting As String = "SyncFormSaveCompleting"
    Const RES_SavingBackup As String = "SavingBackup"
    Const RES_ServerVersionOk As String = "ServerVersionOk"
    Const RES_ServerVersionTooOld As String = "ServerVersionTooOld"
    Const RES_SyncFormCancelling As String = "Cancelling"
    Const RES_SyncStarting As String = "SyncStarting"
    Const RES_SyncComplete As String = "SyncComplete"
    Const RES_SyncCompletedWithErrors As String = "SyncCompletedWithErrors"
    Const RES_ClientSyncStarting As String = "TimeSyncing"
    Const RES_TryAgain As String = "TryAgain"
    Const RES_UnhandledErrorMessage As String = "UnhandledErrorMessage"
    Const RES_UpdatingCustomTerms As String = "UpdatingCustomTerms"
    Const RES_CheckingCustomTerms As String = "CheckingCustomTerms"
    Const RES_UpdatingSchema As String = "UpdatingSchema"
    Const RES_UploadComplete As String = "UploadComplete"
    Const RES_Uploading As String = "Uploading"
    Const RES_UploadingErrorReports As String = "UploadingErrorReports"
    Const RES_UserAuthenticated As String = "UserAuthenticated"
    Const RES_ValidatingDataSet As String = "ValidatingDataSet"
    Const RES_ChecksumErrorxml As String = "ChecksumError.xml"
    Const RES_UploadingTrackingInformation As String = "UploadingTrackingInformation"
#End Region

    Public Property CancelSync() As Boolean
        Get
            Return _cancelSync
        End Get
        Private Set(ByVal value As Boolean)
            _cancelSync = value
        End Set
    End Property

    Private Function ConfirmCancel() As Boolean
        Return Terminology.AskQuestion(Me, MODULENAME, RES_CancelConfirm, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes
    End Function

    Private Delegate Sub WakeUpDelegate()
    Public Sub Popup()
        Try
            If Me.InvokeRequired Then
                Dim wud As New WakeUpDelegate(AddressOf Popup)
                Me.Invoke(wud)
            Else
                Me.Visible = True
                Me.BringToFront()
                Me.Focus()
            End If
        Catch ex As ObjectDisposedException ' HOPPER found this ?!
            'don't care
        End Try
    End Sub

    Public Sub ShowBackgroundDialog()
        Try
            If Me.InvokeRequired Then
                Dim wud As New WakeUpDelegate(AddressOf ShowBackgroundDialog)
                Me.Invoke(wud)
            Else
                Me.ShowDialog()
            End If
        Catch ex As Exception ' ObjectDisposedException ' HOPPER found this ?!
            LogError(MODULENAME, "ShowBackgroundDialog", ex, False, "")
            'don't care
        End Try
    End Sub

    'Private Delegate Sub SetSyncStatusTextDelegate()
    'Private Sub SetSyncStatusText()
    '    If Me.InvokeRequired Then
    '        Dim d As New SetSyncStatusTextDelegate(AddressOf SetSyncStatusText)
    '        Me.Invoke(d)
    '        Return
    '    End If
    '    Me.StatusLabel.Text = _syncStatus
    '    Me.LogMessage(_syncStatus)
    'End Sub

    Public Property SyncStatus() As String
        Get
            Return _syncStatus
            'Try
            'Catch ex As System.ObjectDisposedException ' hopper
            '    Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.SyncStatus.Get: {1}", DateTime.Now, ex.StackTrace))
            '    Return String.Empty
            'End Try
        End Get
        Set(ByVal Value As String)
            _syncStatus = Value
            'Try
            '    SetSyncStatusText()
            'Catch ex As System.ObjectDisposedException ' hopper
            '    Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.SyncStatus.Set: {1}", DateTime.Now, ex.StackTrace))
            'End Try
        End Set
    End Property

    Private Delegate Sub SetNotificationTextDelegate(ByVal message As String)
    Private Sub SetNotificationText(ByVal message As String)
        _notificationText = String.Format(CultureInfo.InvariantCulture, _notificationTemplate, _startedAt.ToShortTimeString(), message)

        'Const METHODNAME As String = "SetNotificationText"
        'If Me.InvokeRequired Then
        '    Dim d As New SetNotificationTextDelegate(AddressOf SetNotificationText)
        '    Me.Invoke(d, message)
        '    Return
        'End If
        'Try
        '    If Me.Notification Is Nothing Then Throw New ObjectDisposedException("Notification")
        '    Me.Notification.Text = Terminology.GetFormattedString(MODULENAME, "NotificationTemplate", _startedAt.ToShortTimeString(), message)
        'Catch ex As ObjectDisposedException ' hopper trapped
        '    Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.SetNotificationText: {1}", DateTime.Now, ex.StackTrace))
        'Catch ex As Exception
        '    LogError(MODULENAME, METHODNAME, ex, True, "Error setting notification")
        'Finally
        '    Me.Refresh()
        'End Try

        'Me.Notification.Visible = Not String.IsNullOrEmpty(message)
    End Sub

    'Private Delegate Sub SetSyncStatusDelegate(ByVal messageId As String, ByVal setNotification As Boolean)
    Private Sub SetSyncStatus(ByVal messageId As String, ByVal setNotification As Boolean)
        Const METHODNAME As String = "SetSyncStatus"
        Try
            Dim message As String = Terminology.GetString(MODULENAME, messageId)
            Me.SyncStatus = message
            If setNotification Then SetNotificationText(message)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, "Error setting status text")
        End Try
    End Sub

    Private Sub SetSyncStatus(ByVal messageId As String, ByVal SetNotification As Boolean, ByVal ParamArray args() As String)
        Const METHODNAME As String = "SetSyncStatus"
        Try
            Dim messageArgs As New Generic.List(Of String)(args)
            messageArgs.Insert(0, Terminology.GetString(MODULENAME, RES_TryAgain))
            Dim message As String = String.Format(WithCulture, Terminology.GetString(MODULENAME, messageId), messageArgs.ToArray)
            Me.SyncStatus = message
            If SetNotification Then SetNotificationText(Me.SyncStatus)
        Catch ex As ObjectDisposedException ' hopper trapped
            Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.SetSyncStatus: {1}", DateTime.Now, ex.StackTrace))
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, "Error setting formatted status text")
        End Try
    End Sub

    Private Delegate Sub LogMessageDelegate(ByVal message As String)
    Public Sub LogMessage(ByVal message As String)
        _syncLogBuilder.Insert(0vbNewLine)
        _syncLogBuilder.Insert(0, message)
        'If Me.InvokeRequired Then
        '    Dim d As New LogMessageDelegate(AddressOf LogMessage)
        '    Me.BeginInvoke(d, message)
        '    Return
        'End If
        'Try
        '    Log.SelectionStart = 0
        '    Log.SelectionLength = 0
        '    Log.SelectedText = Terminology.GetFormattedString(MODULENAME, RES_MessageLogFormat, Date.Now.ToString(LogTimeFormat, WithoutCulture), message) & vbNewLine
        '    Log.Refresh()
        'Catch ex As ObjectDisposedException ' Hopper Special :)
        '    Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.LogMessage: {1}", DateTime.Now, ex.StackTrace))
        'End Try
    End Sub

#Region "Constructors"

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        _syncType = SyncType.NormalSync
    End Sub

    Public Sub New(ByVal owner As Form)
        MyBase.New()
        InitializeComponent()

        _syncType = SyncType.NormalSync
        Me.Owner = owner
    End Sub

    Public Sub New(ByVal owner As Form, ByVal drClientSite As WebService.ClientDataSet.ClientSiteRow, ByVal syncType As SyncType)
        MyBase.New()

        InitializeComponent()
        Me._ClientSite = drClientSite
        _syncType = syncType
        Me.Owner = owner
    End Sub

#End Region

    Private Sub SyncForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If Not _initialLoad Then Return
        _initialLoad = False
        Try
            Select Case _syncType
                Case SyncType.NormalSync
                    Me.BackgroundSyncFull()
                    LastSyncAttempt = DateTime.UtcNow
                Case SyncType.ClientDetail
                    Me.SyncGetClientDetails(_ClientSite, False)
                Case SyncType.ClientDetailAndHistory
                    Me.SyncGetClientDetails(_ClientSite, True)
                Case SyncType.ClientHistory
                    Me.SyncGetClientHistory(_ClientSite)
                Case SyncType.ClientOpenRequests
                    Me.SyncGetClientRequests(_ClientSite)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SyncForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If CurrentSyncForm Is Me Then
            CurrentSyncForm = Nothing
        End If
        If Me.Notification IsNot Nothing Then
            Me.Notification.Visible = False
            Me.Notification.Dispose()
            Me.Notification = Nothing
        End If
        EndSync()
    End Sub

    'Private Sub DoSelectedSync()

    'End Sub

    Private _autoSyncing As Boolean
    Private Delegate Sub SetAutoSyncLabelDelegate(ByVal value As Boolean)
    Private Sub SetAutoSyncLabel(ByVal value As Boolean)
        Me.AutoSyncLabel.Text = Terminology.GetString(MODULENAME, RES_AutoSyncInProgressBanner)
        Me.AutoSyncLabel.Visible = value
    End Sub

    Friend Property AutoSyncing() As Boolean
        Get
            Return _autoSyncing
        End Get
        Set(ByVal value As Boolean)
            _autoSyncing = value
            Dim d As New SetAutoSyncLabelDelegate(AddressOf SetAutoSyncLabel)
            Me.Invoke(d, value)
        End Set
    End Property

    Private Sub SyncForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        If Me.Notification IsNot Nothing Then
            Me.Notification.Visible = False
            Me.Notification.Dispose()
        End If
        Me.InputPanel.Dispose()
    End Sub

    Private _initialLoad As Boolean
    Private Sub Synchronise_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.InputPanel.Enabled = False 'force Input panel closed

        gHoldAutoSync = True
        gSyncInProgress = True
        ' re-initialise web service proxy, in case of network failure.
        Startup.InitialiseWebServiceProxy(Me, True)
        Url.Text = WebServer.Url

        If AutoSyncing Then
            Try
                Sound.Play(IO.Path.Combine(gDatabaseFolder, My.Resources.SyncStartSoundFile))
                _startedAt = DateTime.Now
                Me.BackgroundSyncFull()
            Catch ex As Exception

            End Try
            'Me.Notification.Visible = False
            If Me._syncOk Then
                CurrentSyncForm = Nothing
                Me.Close()
            ElseIf Not Me.Visible Then
                Me.Popup()
            End If
        Else
            AutoCloseCheckBox.Checked = _autoClose
            _initialLoad = True
            'Me.Show()
            '''Application.DoEvents()
            'DoSelectedSync()
            'OkCancelButton.Text = My.Resources.OkText
            'OkCancelButton.Enabled = True
            'If _autoClose Then
            '    Me._controlledClose = True
            '    Me.Close()
            'Else
            '    Me.Focus()
            'End If
        End If
    End Sub

    'Private _controlledClose As Boolean

    Private Sub OkCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkCancelButton.Click
        If sender IsNot OkCancelButton Then Return

        If OkCancelButton.Text = My.Resources.CancelText Then
            If Not Me.CancelSync Then
                Me.SetSyncStatus(RES_SyncFormCancelling, False)
                Me.CancelSync = True
            End If
            Return
        End If

        '_controlledClose = True
        Me.Close()
    End Sub

    Private Function CheckDeviceID() As Boolean
        Dim ar As IAsyncResult

        ar = WebServer.BeginCheckDeviceId(gDeviceIDString, Nothing, Nothing)
        While Not ar.IsCompleted
            RefreshMe()
            Threading.Thread.Sleep(0)
        End While

        Dim amAuthenticated As Boolean = WebServer.EndCheckDeviceId(ar)

        If Not amAuthenticated Then
            Me.SetSyncStatus(RES_DeviceAuthenticationFailed, True)
            RefreshMe()
            Return False
        Else
            Me.SetSyncStatus(RES_DeviceAuthenticated, False)
            RefreshMe()
            Return True
        End If

    End Function

    Private Sub UploadProfile()
        Dim dsProfile As WebService.ConsultantProfile = CType(ConsultantConfig.ConsultantProfileDataSet.GetChanges(DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted), WebService.ConsultantProfile)

        If dsProfile IsNot Nothing AndAlso dsProfile.ConsultantProfileEntry.Rows.Count > 0 Then
            ConsultantConfig.Save(False, False)
            'Me.SetSyncStatus(RES_ProfileSyncing, False)

            Dim dsChanges As ConsultantProfile = CType(dsProfile.GetChanges(), WebService.ConsultantProfile)
            For Each dr As ConsultantProfile.ConsultantProfileEntryRow In dsChanges.ConsultantProfileEntry.Select()
                If Not DataRowHasChanges(dr) Then
                    dsChanges.ConsultantProfileEntry.RemoveConsultantProfileEntryRow(dr)
                End If
            Next
            RefreshMe()
            If dsChanges.ConsultantProfileEntry.Count > 0 Then
                WebServer.UploadUserProfile(gDeviceIDString, gConsultantUID, dsChanges)
                ConsultantConfig.Save(True, True)
            End If
            RefreshMe()
        End If
    End Sub

    Private Shared Sub AddUpdatedRequests(ByRef dsChanges As WebService.ClientDataSet)
        Dim rrs() As WebService.ClientDataSet.RequestRow
        ' RCP 2007-02-15. This is generating too many unchanged records
        ' rrs = CType(gClientDataSet.Request.Select("RequestID < 0 OR (NOT ConsultantStatusID IS NULL AND ConsultantStatusID <> RequestStatusID)"), WebService.ClientDataSet.RequestRow())
        rrs = CType(gClientDataSet.Request.Select(String.Format("ModifiedDateTime > '{0:u}'", ConsultantConfig.LastSync)), WebService.ClientDataSet.RequestRow())
        If rrs Is Nothing OrElse rrs.Length = 0 Then
            Return
        End If
        If dsChanges Is Nothing Then dsChanges = CType(gClientDataSet.Clone, ClientDataSet)

        dsChanges.EnforceConstraints = False

        For Each rr As WebService.ClientDataSet.RequestRow In rrs
            If Not dsChanges.Request.Rows.Contains(rr.RequestUID) Then
                dsChanges.Request.ImportRow(rr)
                Threading.Thread.Sleep(0)
            End If
        Next
    End Sub

    Private Shared Sub AddUpdatedJobs(ByRef dsChanges As WebService.ClientDataSet)
        Dim jrs() As WebService.ClientDataSet.JobRow
        jrs = CType(gClientDataSet.Job.Select(String.Format("(JobStatusID < 3 OR ModifiedDateTime > '{0:u}') AND ConsultantUID = '{1}'", ConsultantConfig.LastSync, gConsultantUID)), WebService.ClientDataSet.JobRow()) ' draft, complete and signed, but not sync'ed
        If jrs Is Nothing OrElse jrs.Length = 0 Then
            Return
        End If
        If dsChanges Is Nothing Then dsChanges = CType(gClientDataSet.Clone, ClientDataSet)

        dsChanges.EnforceConstraints = False

        For Each jr As WebService.ClientDataSet.JobRow In jrs
            If Not dsChanges.Job.Rows.Contains(jr.JobUID) Then
                Dim r As WebService.ClientDataSet.RequestRow = dsChanges.Request.FindByRequestUID(jr.RequestUID)
                If r Is Nothing Then
                    dsChanges.Request.ImportRow(jr.RequestRow)
                End If
                dsChanges.Job.ImportRow(jr)
                Threading.Thread.Sleep(0)
            End If
        Next
    End Sub


    Private Shared Sub AddCustomDataTable(ByRef dsChanges As WebService.ClientDataSet, ByVal tableName As String, ByVal crs As DataRow())
        If crs IsNot Nothing AndAlso crs.Length <> 0 Then
            If dsChanges Is Nothing Then dsChanges = CType(gClientDataSet.Clone, ClientDataSet)
            dsChanges.EnforceConstraints = False
            Dim customTable As DataTable
            customTable = dsChanges.Tables(tableName)
            For Each cr As DataRow In crs
                customTable.ImportRow(cr)
                Threading.Thread.Sleep(0)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Add custom data that's been modified since last sync. 
    ''' </summary>
    ''' <param name="dsChanges"></param>
    ''' <remarks>This doesn't include lookups; just transaction tables.</remarks>
    Private Shared Sub AddUpdatedCustomData(ByRef dsChanges As WebService.ClientDataSet)
        Dim customTables As New Collections.Specialized.StringDictionary

        For Each cfr As WebService.ClientDataSet.CustomFormRow In gClientDataSet.CustomForm
            If Not customTables.ContainsKey(cfr.TableName) Then
                customTables.Add(cfr.TableName, cfr.ParentTableName)
            End If
        Next

        For Each tableName As String In customTables.Keys

            'TODO: cater for different requirements.
            '    Select Case customTables.Item(tableName)
            '        Case "Job"
            '        Case "Request"
            '        Case "ClientSite"
            '    End Select

            Dim crs() As DataRow = gClientDataSet.Tables(tableName).Select(String.Format("ModifiedDateTime > '{0:u}'", ConsultantConfig.LastSync))
            AddCustomDataTable(dsChanges, tableName, crs)
        Next
    End Sub

    Private Shared Sub RemoveForeignJobs(ByVal dsChanges As WebService.ClientDataSet)
        Const METHODNAME As String = "RemoveForeignJobs"
        If dsChanges Is Nothing Then Return
        Dim jrs() As WebService.ClientDataSet.JobRow
        jrs = CType(dsChanges.Job.Select(String.Format("ConsultantUID <> '{0}'", gConsultantUID)), WebService.ClientDataSet.JobRow())
        If jrs Is Nothing OrElse jrs.Length = 0 Then
            Return
        End If
        'Application.DoEvents()

        Try
            For Each jr As WebService.ClientDataSet.JobRow In jrs
                If dsChanges.Request.Rows.Contains(jr.RequestUID) Then
                    Dim r As WebService.ClientDataSet.RequestRow = dsChanges.Request.FindByRequestUID(jr.RequestUID)
                    If r IsNot Nothing Then
                        If r.GetJobRows.Length = 0 Then
                            dsChanges.Request.RemoveRequestRow(r)
                        End If
                    End If
                End If
                Threading.Thread.Sleep(0)
            Next
            For Each jr As WebService.ClientDataSet.JobRow In jrs
                dsChanges.Job.RemoveJobRow(jr)
            Next
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
        'Application.DoEvents()
    End Sub

    ''' <summary>
    ''' remove client and status data.
    ''' </summary>
    ''' <param name="dsChanges"></param>
    ''' <remarks></remarks>
    Private Shared Sub RemoveExtraData(ByVal dsChanges As WebService.ClientDataSet)
        Const METHODNAME As String = "RemoveExtraData"
        If dsChanges Is Nothing Then Return

        Try
            dsChanges.JobStatus.Clear()
            dsChanges.RequestStatus.Clear()
            dsChanges.ClientSite.Clear()    ' this could be really dangerous.
            dsChanges.ClientSiteStatus.Clear()

            ' because we may be updating the consultant tracking information, we need to include the current user in the upload.
            For Each dr As activiser.WebService.ClientDataSet.ConsultantRow In dsChanges.Consultant.Select()
                If dr.ConsultantUID <> gConsultantUID Then
                    dsChanges.Consultant.RemoveConsultantRow(dr)
                End If
            Next

            ' clear any lookup tables
            For Each t As DataTable In dsChanges.Tables
                If t.Rows.Count <> 0 AndAlso (t.PrimaryKey.Length <> 0) AndAlso (t.PrimaryKey(0).DataType IsNot GetType(Guid)) Then ' don't allow upload of non-transaction tables; this should get rid of any lookup tables.
                    t.Clear()
                End If
            Next
            'For Each cfr As WebService.ClientDataSet.CustomFormRow In gClientDataSet.CustomForm
            '    if 
            'Next

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
        'Application.DoEvents()
    End Sub

    Private Shared Sub SetModifiedTimes(ByRef dsChanges As WebService.ClientDataSet, ByVal syncTime As DateTime)
        If dsChanges Is Nothing Then Return

        For Each dt As DataTable In dsChanges.Tables
            Dim c As Integer = dt.Columns.IndexOf("ModifiedDateTime")
            If c = -1 Then Continue For ' error, strictly speaking.
            For Each dr As DataRow In dt.Rows
                dr.Item(c) = syncTime
            Next
        Next
        'Application.DoEvents()
    End Sub

    Public Shared Function DataSetIsNullOrEmpty(ByVal ds As DataSet) As Boolean
        If ds Is Nothing Then Return True

        For Each dt As DataTable In ds.Tables
            If dt.Rows.Count <> 0 Then Return False
        Next
        Return True
    End Function

    Private Shared Sub ValidateRequestCheckSum(ByVal dsError As WebService.ClientDataSet, ByVal uid As Guid, ByVal id As Integer, ByVal number As String, ByVal checkSum As String, ByRef hasChecksumError As Boolean)
        Dim drPPCRequest As WebService.ClientDataSet.RequestRow = gClientDataSet.Request.FindByRequestUID(uid)
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

    Private Shared Sub ValidateJobCheckSum(ByVal dsError As WebService.ClientDataSet, ByVal uid As Guid, ByVal id As Integer, ByVal number As String, ByVal checkSum As String, ByRef hasChecksumError As Boolean)
        Dim drPPCJob As WebService.ClientDataSet.JobRow = gClientDataSet.Job.FindByJobUID(uid)
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

    Private Sub ValidateChecksums(ByVal returnDS As WebService.UploadResults, ByRef hasChecksumError As Boolean)
        Me.SetSyncStatus(RES_ValidatingDataSet, False)
        Dim dsError As WebService.ClientDataSet = LoadExistingErrors()

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
            Me.SetSyncStatus(RES_LoggingErrors, False)
            dsError.WriteXml(gErrorFolder + RES_ChecksumErrorxml)
        End If
        'Application.DoEvents()
    End Sub

    Private Function UploadClientDataSetUpdates(ByRef dsChanges As WebService.ClientDataSet) As WebService.UploadResults
        'Return WebServer.UploadClientDataSetUpdates(gDeviceIDString, gConsultantUID, dsChanges)
        Dim ar As IAsyncResult
        ar = WebServer.BeginUploadClientDataSetUpdates(gDeviceIDString, gConsultantUID, dsChanges, Nothing, Nothing)
        While Not ar.IsCompleted
            'Application.DoEvents()
            If Me.CancelSync Then
                If ConfirmCancel() Then
                    WebServer.Abort()
                    Me.SetSyncStatus(RES_Cancelled, True)
                    Return Nothing
                Else
                    Me.CancelSync = False
                End If
            End If
            Threading.Thread.Sleep(0)
        End While
        Return WebServer.EndUploadClientDataSetUpdates(ar)
    End Function

    Private Shared Function LoadExistingErrors() As WebService.ClientDataSet
        Dim errorFileName As String = gErrorFolder & RES_ChecksumErrorxml
        Dim dsError As New WebService.ClientDataSet
        dsError.EnforceConstraints = False
        If New FileInfo(errorFileName).Exists Then
            Try
                dsError.ReadXml(errorFileName)
            Catch ex As Exception
            End Try
        End If
        Return dsError
    End Function

    Private Sub UpLoadErrorLog()
        Try
            Me.SetSyncStatus(RES_UploadingErrorReports, False)
            WebServer.UploadErrorLog(gDeviceIDString, gErrorLog)
        Catch ex As Exception
            '11.4.2006 Tim: If error log synchronisation fails, nuke the error log
            ' do nothing if error report generates an error !
        Finally
            ' clear the error log
            gErrorLog.Clear()
            ' Dim fileName As String = strErrorLog
            If System.IO.File.Exists(gErrorLogDbFileName) Then
                Try
                    System.IO.File.Delete(gErrorLogDbFileName)
                Catch ' ex As Exception
                    ' don't really care if it fails
                End Try
            End If
        End Try
    End Sub

    Private Sub UploadGpsLog()
        Try
            If TrackingData.DeviceTracking.Count = 0 Then Return ' nothing to upload.
            ' pause logging tracking info, we can be uploading data while it's updating.
            Gps.Pause()
            Me.SetSyncStatus(RES_UploadingTrackingInformation, True)
            'Dim uploadData As DeviceTrackingDataSet.DeviceTrackingDataTable = CType(TrackingData.DeviceTracking.Copy, DeviceTrackingDataSet.DeviceTrackingDataTable)
            If WebServer.UploadDeviceTrackingInfo(gDeviceIDString, TrackingData) Then
                TrackingData.Clear()
                Serialisation.SavePending(TrackingData, gGpsFileName)
            End If
        Catch ex As Exception

        Finally
            Debug.WriteLine("Gps upload complete")
            Gps.Resume()
        End Try
    End Sub

    ReadOnly HR As String = New String("-"c, 40) & vbNewLine
    Private Delegate Sub SyncBeginDelegate(ByVal allowCancel As Boolean, ByVal allowHide As Boolean)

    Private Sub SyncBegin(ByVal allowCancel As Boolean, ByVal allowHide As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SyncBeginDelegate(AddressOf SyncBegin)
            Me.Invoke(d, allowCancel, allowHide)
            Return
        End If

        Me._startedAt = DateTime.Now

        _gpsActive = Not Gps.Paused
        If _gpsActive Then Gps.Pause()

        If Owner IsNot Nothing Then
            Owner.ControlBox = False
        Else
            Me.ControlBox = False
        End If

        Me.MenuClose.Enabled = False
        Me.Notification.Visible = True

        If allowCancel Then
            Me.OkCancelButton.Text = My.Resources.CancelText
            Me.OkCancelButton.Enabled = True
        Else
            Me.OkCancelButton.Text = My.Resources.OkText
            Me.OkCancelButton.Enabled = False
        End If

        If allowHide Then
            Me.SyncButton.Text = My.Resources.SyncFormHideButton
            Me.SyncButton.Enabled = True
        Else
            Me.SyncButton.Text = My.Resources.SyncFormSyncButton
            Me.SyncButton.Enabled = False
        End If

        Me.CancelSync = False
        Me.BringToFront()
        RefreshMe()
    End Sub

    Private Delegate Sub SyncEndDelegate()
    Private Sub SyncEnd()
        If Me.InvokeRequired Then
            Try
                Dim d As New SyncEndDelegate(AddressOf SyncEnd)
                Me.Invoke(d)
                Return
            Catch ex As ObjectDisposedException
                Return
            Finally
                If Not gMainForm Is Nothing Then
                    If Not gMainForm.IsDisposed Then
                        gMainForm.PopulateRequestList()
                    End If
                End If
            End Try
        End If

        Try
            If Serialisation.SaveInProgress Then
                Me.SetSyncStatus(RES_SaveCompleting, False)
            End If
            WaitForSaveCommittedCompletion()
            WaitForSavePendingCompletion()

            'gMainForm.UpdateInfo()
            'If Not Me.Visible Then gMainForm.PopulateRequestList()

            If Me._syncOk Then
                Me.SetSyncStatus(RES_Complete, True)
            Else
                Me.SetSyncStatus(RES_SyncCompletedWithErrors, True)
            End If
            Log.SelectionStart = 0
            Log.SelectionLength = 0
            Log.SelectedText = HR
            Log.Refresh()
            'Me.LogMessage(HR)
            Me.OkCancelButton.Text = My.Resources.OkText
            Me.OkCancelButton.Enabled = True
            Me.AutoSyncLabel.Text = Terminology.GetString(MODULENAME, RES_AutoSynchCompleteBanner)
            Me.Notification.Visible = False
            Me.SyncButton.Text = My.Resources.SyncFormSyncButton
            Me.SyncButton.Enabled = True

            ConsultantConfig.LastSyncOK = Me._syncOk

            Try
                If Not Me._syncOk AndAlso Not Me.Visible Then
                    Me.Visible = True
                End If

                If Owner IsNot Nothing Then
                    Owner.ControlBox = True
                Else
                    Me.ControlBox = True
                End If

                Me.MenuClose.Enabled = True

            Catch ex As ObjectDisposedException ' triggered by hopper ?
                Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.SyncComplete: {1}", DateTime.Now, ex.StackTrace))
            End Try

            Dim syncCompleteFileName As String = IO.Path.Combine(gDatabaseRoot, My.Resources.SyncCompleteSoundFile)
            If IO.File.Exists(syncCompleteFileName) Then Sound.Play(syncCompleteFileName)

            If _autoClose Then
                'Me._controlledClose = True
                Me.Close()
            End If

            Application.DoEvents() ' allow event queue to flush.

            If Me.Visible Then
                Me.BringToFront()
                Me.Focus()
            End If

            Application.DoEvents() ' allow event queue to flush.
            Debug.WriteLine("SyncComplete fired")
        Catch ex As Exception
            Debug.WriteLine(String.Format("{0} Exception in SyncForm.SyncComplete: {1}", DateTime.Now, ex.ToString()))
        Finally
            If _gpsActive Then Gps.Resume()
        End Try

    End Sub

    Private Sub UploadChanges(ByVal dsChanges As WebService.ClientDataSet, ByRef hasChecksumError As Boolean)
        SavePending(gClientDataSet, gMainDbFileName)

        Try
            Me.SetSyncStatus(RES_Uploading, False)
            dsChanges.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema
            Dim returnDS As WebService.UploadResults = UploadClientDataSetUpdates(dsChanges)
            If returnDS Is Nothing Then
                If Me.CancelSync Then
                    Return
                Else
                    Throw New NoChecksumDataException
                End If
            Else
                ValidateChecksums(returnDS, hasChecksumError)
            End If

            Me.SetSyncStatus(RES_UploadComplete, False)
        Catch ex As Exception
            Throw
        Finally
            Debug.WriteLine("UploadChanges Complete")
        End Try
    End Sub

    ''' <summary>
    ''' Crude approach to avoiding bizarre errors with sync process, if there is some dodgy data in the dataset,
    ''' the sync will likely fail with a really ugly error
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Shared Sub CleanDataSet(ByVal ds As WebService.ClientDataSet)
        Serialisation.DealWithDataSetErrors(ds, True)
    End Sub

    Private Shared Sub CheckForNewRequests(ByVal dsReturnDataset As WebService.ClientDataSet)
        Dim newRequestSoundFile As String = IO.Path.Combine(gDatabaseRoot, My.Resources.NewRequestSoundFileName)
        If Not IO.File.Exists(newRequestSoundFile) Then Return

        Dim haveNewRequests As Boolean = False
        For Each nr As WebService.ClientDataSet.RequestRow In dsReturnDataset.Request
            Dim rs As WebService.ClientDataSet.RequestStatusRow = gClientDataSet.RequestStatus.FindByRequestStatusID(nr.RequestStatusID)
            If rs IsNot Nothing Then
                If rs.IsNewStatus Then
                    haveNewRequests = True
                    Exit For
                ElseIf Not gClientDataSet.Request.Rows.Contains(nr.RequestUID) Then
                    haveNewRequests = True
                    Exit For
                End If
            Else
                haveNewRequests = True
                Exit For
            End If
        Next
        If haveNewRequests Then
            Sound.Play(newRequestSoundFile)
        End If
    End Sub
    Private Sub UpdateTerminology()
        Const METHODNAME As String = "UpdateTerminology"
        Try
            Me.SetSyncStatus(RES_CheckingCustomTerms, False)
            Dim terminologyUpdateDs As activiser.WebService.TermsAndLabels = WebServer.GetTerminology(gDeviceIDString, gConsultantUID, False)
            If (terminologyUpdateDs.CustomLabel.Count <> 0 OrElse terminologyUpdateDs.CustomTerminology.Count <> 0) Then
                Me.SetSyncStatus(RES_UpdatingCustomTerms, False)
                Terminology.Merge(terminologyUpdateDs)
                'Terminology.Save()
            End If
        Catch ex As Exception
            'Catch the exception. This will catch the exception if the webservice does not exist.
            LogError(MODULENAME, METHODNAME, ex, False, "Error getting terminology updates")
        End Try
    End Sub

    Private Function DownloadChanges() As DataSet
        Try
            Me.SetSyncStatus(RES_Downloading, False)
            Dim ar As IAsyncResult
            ar = WebServer.BeginGetClientDataSetUpdates(gDeviceIDString, gConsultantUID, ConsultantConfig.LastSync, Nothing, Nothing)
            While Not ar.IsCompleted
                'Application.DoEvents()
                If Me.CancelSync Then
                    If ConfirmCancel() Then
                        WebServer.Abort()
                        Me.SetSyncStatus(RES_Cancelled, True)
                        Return Nothing
                    Else
                        Me.CancelSync = False
                    End If
                End If
                Threading.Thread.Sleep(0)
            End While

            Dim result As ClientDataSet = WebServer.EndGetClientDataSetUpdates(ar)
            If DataSetIsNullOrEmpty(result) Then
                result.Dispose()
                result = Nothing
            End If
            Return result
        Catch ex As Exception
            LogError(MODULENAME, "DownloadChanges", ex, False, String.Empty)
            Return Nothing
        Finally
            Debug.WriteLine("DownloadChanges complete.")
        End Try

    End Function

    Private syncFullInProgress As Boolean
    Private syncThread As Threading.Thread

    'Inform server of sync start & sync pda time with server

    Private Function StartSync() As DateTime
        Dim syncTime As DateTime

        SetSyncStatus(RES_ClientSyncStarting, False)
        syncTime = WebServer.SyncStart(gDeviceIDString, gConsultantUID)

        If Not SetSystemTime(syncTime) Then Me.SetSyncStatus(RES_ErrorSettingSystemTime, False)

        Return syncTime
    End Function

    Private Shared Function GetDataToUpload(ByVal syncTime As DateTime) As WebService.ClientDataSet
        Dim dsChanges As WebService.ClientDataSet
        dsChanges = TryCast(gClientDataSet.GetChanges(DataRowState.Added Or DataRowState.Modified), WebService.ClientDataSet)
        If dsChanges IsNot Nothing Then
            dsChanges.EnforceConstraints = False
        End If

        AddUpdatedRequests(dsChanges)
        AddUpdatedJobs(dsChanges)
        AddUpdatedCustomData(dsChanges)
        'HACK: RCP 2007-02-14: for some reason, jobs for other consultants are getting 'modified' and uploaded.
        'This forces the removal of other people's jobs.
        'The real fix is to not modify them in the first place, but I don't have time to figure that out right now.
        RemoveForeignJobs(dsChanges)
        RemoveExtraData(dsChanges)
        SetModifiedTimes(dsChanges, syncTime)

        If dsChanges IsNot Nothing Then CleanDataSet(dsChanges)

        If DataSetIsNullOrEmpty(dsChanges) Then
            dsChanges.Dispose()
            dsChanges = Nothing
        End If
        Return dsChanges
    End Function

    Private Sub CheckSchemaAndTerminology(ByRef schemaUpdated As Boolean)
        Dim dsReturnDataset As DataSet
        Dim lastSchemaCheck As Date = ConfigurationSettings.GetDateValue("LastSchemaCheck", DateTime.MinValue)
        Dim schemaCheckInterval As Integer = ConfigurationSettings.GetIntegerValue("SchemaCheckInterval", 1)
        schemaUpdated = False
        If lastSchemaCheck.Date.AddDays(schemaCheckInterval) < DateTime.Today Then
            Me.SetSyncStatus(RES_CheckingSchema, False)
            Dim schemaUpdateRequired As Boolean = WebServer.SchemaUpdateRequired(gDeviceIDString, gConsultantUID)
            If Me.CancelSync Then Return

            If schemaUpdateRequired Then
                Me.SetSyncStatus(RES_UpdatingSchema, False)
                dsReturnDataset = WebServer.GetCustomDataSchema(gDeviceIDString, gConsultantUID)
                'dsReturnClientDataset.Merge(dsReturnDataset, False, MissingSchemaAction.AddWithKey)
                gClientDataSet.Merge(dsReturnDataset, False, MissingSchemaAction.AddWithKey)
                schemaUpdated = True
            Else
                Me.SetSyncStatus(RES_CheckingSchemaNone, False)
            End If

            If Me.CancelSync Then Return

            UpdateTerminology()

            ConfigurationSettings.SetDateValue("LastSchemaCheck", DateTime.Now)
        End If
    End Sub
    Private Function MergeReturnedClientDataset(ByVal dsReturnDataset As DataSet) As WebService.ClientDataSet
        Dim dsReturnClientDataset As WebService.ClientDataSet
        Me.SetSyncStatus(RES_Merging, False)
        For Each dt As DataTable In gClientDataSet.Tables
            For Each dr As DataRow In dt.Select(Nothing, Nothing, DataViewRowState.Deleted)
                dr.AcceptChanges() ' flush deleted rows
            Next
        Next
        dsReturnClientDataset = New WebService.ClientDataSet
        dsReturnClientDataset.Merge(dsReturnDataset, False, MissingSchemaAction.AddWithKey)

        If ConfigurationSettings.GetBooleanValue("FlushCustomLookupsOnDefinitionChange", False) Then
            If dsReturnClientDataset.CustomControl.Rows.Count <> 0 Then
                ' possibly flush lookups.
                For Each ccr As WebService.ClientDataSet.CustomControlRow In dsReturnClientDataset.CustomControl
                    If ccr.ModifiedDateTime > ConsultantConfig.LastSync Then ' would be odd if this not true.
                        If Not ccr.IsListDataSourceNull AndAlso Not String.IsNullOrEmpty(ccr.ListDataSource) Then
                            If dsReturnClientDataset.Tables(ccr.ListDataSource).Rows.Count <> 0 Then
                                gClientDataSet.Tables(ccr.ListDataSource).Clear()
                            End If
                        End If
                    End If
                Next
            End If
        End If

        gClientDataSet.Merge(dsReturnClientDataset, False, MissingSchemaAction.AddWithKey)
        gClientDataSet.AcceptChanges()
        CheckForNewRequests(dsReturnClientDataset)
        Return dsReturnClientDataset
    End Function

    Private Sub BackgroundSyncFull()
        syncFullInProgress = True
        syncThread = New Threading.Thread(AddressOf SyncFull)
        syncThread.Name = "Full Sync started @ " & DateTime.Now.ToShortTimeString()
        syncThread.Start()
        Do
            RefreshMe()
        Loop While syncFullInProgress 'syncThread IsNot Nothing
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
        Private Sub SyncFull()
        Const METHODNAME As String = "Synchronise"

        'If syncThread Is Nothing Then ' move this to a background thread.
        '    RefreshMe()
        '    BackgroundSyncFull()
        '    Return
        'End If

        Dim hasChecksumError As Boolean

        Try
            SyncBegin(True, True)

            If Authenticate() <> AuthenticationStatus.Ok Then Return
            Me.SetSyncStatus(RES_Authenticated, False)

            If Me.CancelSync Then Throw New SyncCanceledException()

            Dim syncTime As DateTime
            syncTime = StartSync()

            If Me.CancelSync Then Throw New SyncCanceledException()

            Me.SetNotificationText(Terminology.GetString(MODULENAME, RES_NotificationInProgress))

            'Synchronise added requests and jobs
            Dim dsChanges As WebService.ClientDataSet = GetDataToUpload(syncTime)

            If Me.CancelSync Then Throw New SyncCanceledException()

            Dim somethingToUpload As Boolean

            If dsChanges IsNot Nothing Then
                somethingToUpload = True
                UploadChanges(dsChanges, hasChecksumError)
                If hasChecksumError Then
                    Me.SetSyncStatus(RES_ChecksumErrorMessage, True)
                End If
            End If

            If Me.CancelSync Then Throw New SyncCanceledException()

            Dim dsReturnDataset As DataSet 'WebService.ClientDataSet
            Dim dsReturnClientDataset As WebService.ClientDataSet = Nothing

            Dim schemaUpdated As Boolean
            CheckSchemaAndTerminology(schemaUpdated)
            If Me.CancelSync Then Throw New SyncCanceledException()

            dsReturnDataset = DownloadChanges()
            If Me.CancelSync Then Throw New SyncCanceledException()

            Dim profileUpdateThread As Threading.Thread = Nothing

            If dsReturnDataset Is Nothing Then
                Me.SetSyncStatus(RES_NoDataNote, False)
            Else
                dsReturnClientDataset = MergeReturnedClientDataset(dsReturnDataset)
                profileUpdateThread = New Threading.Thread(AddressOf ConsultantConfig.UpdateProfile)
                profileUpdateThread.Start()
            End If

            If Not Me.CancelSync Then
                If TrackingData.DeviceTracking.Rows.Count > 0 Then
                    Me.UploadGpsLog()
                End If
            End If

            If Not Me.CancelSync Then
                If Not DataSetIsNullOrEmpty(gErrorLog) Then
                    UpLoadErrorLog()
                End If
            End If

            If somethingToUpload OrElse schemaUpdated OrElse dsReturnDataset IsNot Nothing Then
                Me.SetSyncStatus(RES_Saving, False)
                gClientDataSet.AcceptChanges()
                StartSaveCommitted(gClientDataSet, gMainDbFileName)
                StartSavePending(gClientDataSet, gMainDbFileName)
            End If

            If Me.CancelSync Then Throw New SyncCanceledException()

            Me.SetSyncStatus(RES_SyncComplete, True)
            ConsultantConfig.LastSync = WebServer.SyncComplete(gDeviceIDString, gConsultantUID)

            Me.SetSyncStatus(RES_ProfileSyncing, True)
            If profileUpdateThread IsNot Nothing Then
                profileUpdateThread.Join()
            End If
            UploadProfile()

            Dim requiredSyncInterval As Integer = WebServer.GetAutoSyncInterval(gDeviceIDString, gConsultantUID)
            SetSyncInterval(requiredSyncInterval)
            ConfigurationSettings.Save()

            gMainForm.SetSyncColors(System.Drawing.SystemColors.ActiveCaptionText, System.Drawing.SystemColors.ActiveCaption)
            gSyncsMissed = 0

            If dsReturnClientDataset IsNot Nothing AndAlso dsReturnClientDataset.RequestStatus.Count <> 0 Then
                gMainForm.InitializeRequestList()
            End If

            If Not hasChecksumError Then
                _syncOk = True
            End If

        Catch ex As SyncCanceledException
            Me.SetSyncStatus(RES_Cancelled, True)
            Me._cancelSync = True
            Return

        Catch ex As NoChecksumDataException
            Me.SetSyncStatus(RES_NoChecksumDataException, False, ex.Message)
            Me._syncOk = False

        Catch ex As System.Net.WebException
            Me.SetSyncStatus(RES_CommsFailureMessage, True, ex.Message)
            Me._syncOk = False

        Catch ex As Exception
            Me.SetSyncStatus(RES_UnhandledErrorMessage, True, ex.Message)
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Me._syncOk = False

        Finally
            SyncEnd()
            Try
                ' reset forced sync time.
                If Not String.IsNullOrEmpty(ConfigurationSettings.GetStringValue(My.Resources.Resources.AppConfigForceLastSyncDateTimeKey, "")) Then
                    ConfigurationSettings.SetStringValue(My.Resources.Resources.AppConfigForceLastSyncDateTimeKey, "")
                End If
            Catch ' ignore errors here.
            End Try
            syncFullInProgress = False
        End Try

    End Sub

    Private Sub SyncGetClientDetails(ByVal clientSite As WebService.ClientDataSet.ClientSiteRow, ByVal syncHistory As Boolean)
        Const METHODNAME As String = "GetClientDetails"
        Dim boolRetry As Boolean
        Do
            boolRetry = False
            Try
                Me._syncOk = False
                SyncBegin(False, False)
                Me.SetSyncStatus(RES_GettingClientDetails, True)
                If Authenticate() <> AuthenticationStatus.Ok Then
                    Return
                End If
                Me.SetSyncStatus(RES_Authenticated, False)

                Dim csd As DataSet = WebServer.GetClientSiteDetails(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID)
                Merge(gClientDataSet, csd, True)

                If syncHistory Then
                    Me.SetSyncStatus(RES_GettingClientHistory, False)

                    Dim dsJob As DataSet = WebServer.GetJobHistory(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID, gintJobHistoryCount, gintJobHistoryAge)
                    If Not DataSetIsNullOrEmpty(dsJob) Then
                        Me.SetSyncStatus(RES_Merging, False)
                        Merge(gClientDataSet, dsJob, True)
                        ConsultantConfig.UpdateProfile() ' add downloaded requests and jobs to profile.
                        UploadProfile()
                    End If
                End If

                Me.SetSyncStatus(RES_Saving, False)
                StartSavePending(gClientDataSet, gMainDbFileName)

                Me._syncOk = True

            Catch ex As System.Net.WebException
                Select Case Terminology.AskQuestion(Me, MODULENAME, RES_CommsFailureQuestion, MessageBoxButtons.RetryCancel, MessageBoxDefaultButton.Button1)
                    Case Windows.Forms.DialogResult.Cancel
                        Me.SetSyncStatus(RES_Cancelled, True)
                        Exit Sub
                    Case Windows.Forms.DialogResult.Retry
                        boolRetry = True
                        'Application.DoEvents()
                End Select
            Catch ex As Exception
                Me.SetSyncStatus(RES_UnhandledErrorMessage, True, ex.ToString)
                LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Finally
                SyncEnd()
            End Try
        Loop While boolRetry

    End Sub

    Private Sub SyncGetClientHistory(ByVal clientSite As WebService.ClientDataSet.ClientSiteRow)
        Const METHODNAME As String = "GetClientHistory"
        Dim boolRetry As Boolean
        Do
            boolRetry = False
            Try
                Me._syncOk = False
                SyncBegin(False, False)
                Me.SetSyncStatus(RES_GettingClientHistory, True)
                If Authenticate() <> AuthenticationStatus.Ok Then
                    Return
                End If
                Me.SetSyncStatus(RES_Authenticated, False)


                Dim dsJob As DataSet = WebServer.GetJobHistory(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID, gintJobHistoryCount, gintJobHistoryAge)
                If Not DataSetIsNullOrEmpty(dsJob) Then
                    Me.SetSyncStatus(RES_Merging, False)
                    Merge(gClientDataSet, dsJob, True)
                    ConsultantConfig.UpdateProfile() ' add downloaded requests and jobs to profile.

                    Me.SetSyncStatus(RES_Saving, False)
                    StartSavePending(gClientDataSet, gMainDbFileName)
                    UploadProfile()
                End If

                Me._syncOk = True

            Catch ex As System.Net.WebException
                LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                Me.SetSyncStatus(RES_CommsFailureMessage, True, ex.Message)
                Select Case Terminology.AskQuestion(Me, MODULENAME, RES_CommsFailureQuestion, MessageBoxButtons.RetryCancel, MessageBoxDefaultButton.Button1)
                    Case Windows.Forms.DialogResult.Cancel
                        Me.SetSyncStatus(RES_Cancelled, True)
                        Exit Sub
                    Case Windows.Forms.DialogResult.Retry
                        boolRetry = True
                        'Application.DoEvents()
                End Select
            Catch ex As Exception
                Me.SetSyncStatus(RES_UnhandledErrorMessage, True, ex.ToString)
                LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Finally
                SyncEnd()
            End Try
        Loop While boolRetry

    End Sub

    Private Sub SyncGetClientRequests(ByVal clientSite As WebService.ClientDataSet.ClientSiteRow)
        Const METHODNAME As String = "GetClientRequests"
        Dim boolRetry As Boolean
        Do
            boolRetry = False
            Try
                Me._syncOk = False
                SyncBegin(False, False)
                Me.SetSyncStatus(RES_GettingClientRequests, True)
                If Authenticate() <> AuthenticationStatus.Ok Then
                    Return
                End If
                Me.SetSyncStatus(RES_Authenticated, False)


                Dim dsJob As DataSet = WebServer.GetClientSiteOpenRequests(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID)
                If Not DataSetIsNullOrEmpty(dsJob) Then
                    Me.SetSyncStatus(RES_Merging, False)
                    Merge(gClientDataSet, dsJob, True)
                    ConsultantConfig.UpdateProfile() ' add downloaded requests and jobs to profile.

                    Me.SetSyncStatus(RES_Saving, False)
                    StartSavePending(gClientDataSet, gMainDbFileName)
                    'UploadProfile()
                End If
                Me._syncOk = True

            Catch ex As System.Net.WebException
                LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                Me.SetSyncStatus(RES_CommsFailureMessage, True, ex.Message)
                Select Case Terminology.AskQuestion(Me, MODULENAME, RES_CommsFailureQuestion, MessageBoxButtons.RetryCancel, MessageBoxDefaultButton.Button1)
                    Case Windows.Forms.DialogResult.Cancel
                        Me.SetSyncStatus(RES_Cancelled, True)
                        Exit Sub
                    Case Windows.Forms.DialogResult.Retry
                        boolRetry = True
                        'Application.DoEvents()
                End Select
            Catch ex As Exception
                Me.SetSyncStatus(RES_UnhandledErrorMessage, True, ex.ToString)
                LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Finally
                SyncEnd()
            End Try
        Loop While boolRetry

    End Sub

    Private Sub SyncButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncButton.Click
        If Me.SyncButton.Text = My.Resources.SyncFormHideButton Then
            Me.Visible = False
            Me.Owner = Nothing
            Me.Notification.Visible = True
            Return
        Else
            Me.AutoSyncLabel.Visible = False
            Me.BackgroundSyncFull()
        End If
    End Sub

    Private Enum AuthenticationStatus
        Ok
        UserAuthenticationFailed
        DeviceAuthenticationFailed
        NoNetwork
        ServerTooOld
        NameResolutionFailure
    End Enum

    Private Function GetServerVersion() As Version
        Const METHODNAME As String = "GetServerVersion"
        Try
            'Dim ar As IAsyncResult
            If _verbose Then Me.SetSyncStatus(RES_GetServerVersion, False)

            Dim versionString As String = WebServer.GetServerVersion(gDeviceIDString)

            If String.IsNullOrEmpty(versionString) Then
                Throw New FormatException()
            ElseIf versionString = "-1" Then
                Throw New UnauthorizedAccessException()
            End If
            Return New Version(versionString)
        Catch ex As Net.Sockets.SocketException
            'LogError(MODULENAME, METHODNAME, ex, False, ex.Message)
            Throw
        Catch ex As System.Net.WebException
            'LogError(MODULENAME, METHODNAME, ex, False, ex.Message)
            Throw
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Throw
        Finally
            Debug.WriteLine("GetServerVersion Done.")
        End Try
    End Function

    Private Function Authenticate() As AuthenticationStatus
        Const METHODNAME As String = "Authenticate"

        Dim w As System.Net.HttpWebRequest
        Dim r As System.Net.WebResponse
        Dim hr As System.Net.HttpWebResponse
        Try

            If Not Connected() Then
                Me.SetSyncStatus(RES_CommsFailureMessage, True, "Not connected to a network")
                Return AuthenticationStatus.NoNetwork
            End If

            If Not CanResolveName(WebServer.Url) Then
                Me.SetSyncStatus(RES_CommsFailureMessage, True, "Unable to resolve host name")
                Return AuthenticationStatus.NameResolutionFailure
            End If

            If ConsultantConfig.LastSyncOK AndAlso ConsultantConfig.LastSync.ToLocalTime.Date = DateTime.Today Then
                Return AuthenticationStatus.Ok ' if we last successfully sync'd today, then assume authentication ok.
            End If

            Me.SetSyncStatus(RES_Authenticating, True)

            Try ' try checking the server version first, if this fails, then be a little more creative in order to get a more meaningful message
                Dim v As Version = GetServerVersion()
                Dim minVersion As New Version(CInt(My.Resources.MinimumServerVersionMajor), CInt(My.Resources.MinimumServerVersionMinor), CInt(My.Resources.MinimumServerVersionBuild), 0)
                If v >= minVersion Then ' v.Major = CInt(My.Resources.MinimumServerVersionMajor) AndAlso v.Minor >= CInt(My.Resources.MinimumServerVersionMinor) AndAlso v.Build >= CInt(My.Resources.MinimumServerVersionBuild) Then
                    Me.SetSyncStatus(RES_ServerVersionOk, False)
                    Return AuthenticationStatus.Ok
                Else
                    Me.SetSyncStatus(RES_ServerVersionTooOld, True, CStr(v.Major), CStr(v.Minor), CStr(v.Build), CStr(v.Revision))
                    Return AuthenticationStatus.ServerTooOld
                End If
            Catch ex As UnauthorizedAccessException ' -1 returned by GetServerVersion = deviceIdCheck failed
                Me.SetSyncStatus(RES_DeviceAuthenticationFailed, False)
                Return AuthenticationStatus.DeviceAuthenticationFailed
            Catch ex As FormatException
                Me.SetSyncStatus(RES_ServerVersionTooOld, False)
                Return AuthenticationStatus.ServerTooOld
            Catch ex As Net.Sockets.SocketException
#If DEBUG Then
                LogError(MODULENAME, METHODNAME, ex, False, "Returning AuthenticationStatus.NoNetwork")
#End If
                Me.SetSyncStatus(RES_CommsFailureMessage, False, ex.Message)
                Return AuthenticationStatus.NoNetwork
                'Throw
            Catch ex As Net.WebException
#If DEBUG Then
                LogError(MODULENAME, METHODNAME, ex, False, "Returning AuthenticationStatus.NoNetwork")
#End If
                Dim message As String = ex.Message
                hr = TryCast(ex.Response, Net.HttpWebResponse)
                If hr IsNot Nothing Then
                    message = String.Format("HTTP Response: {0} - {1}", CInt(hr.StatusCode), hr.StatusDescription)
                End If
                Me.SetSyncStatus(RES_CommsFailureMessage, False, message)
                Return AuthenticationStatus.NoNetwork
                'Throw
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, My.Resources.Resources.SyncFormErrorGettingServerVersion)
                ' if we get here, something's gone wrong, we'll take the long-winded approach, and hopefully get more detail on the error
            End Try

            Dim creds As Net.NetworkCredential
            If Not String.IsNullOrEmpty(gDomainUsername) AndAlso Not String.IsNullOrEmpty(gDomainPassword) AndAlso Not String.IsNullOrEmpty(gDomain) Then
                creds = New Net.NetworkCredential(gDomainUsername, Decrypt(gDeviceID, gDomainPassword), gDomain)
            ElseIf Not String.IsNullOrEmpty(gDomainUsername) AndAlso Not String.IsNullOrEmpty(gDomainPassword) Then
                creds = New Net.NetworkCredential(gDomainUsername, Decrypt(gDeviceID, gDomainPassword))
            ElseIf Not String.IsNullOrEmpty(gDomainUsername) Then
                creds = New Net.NetworkCredential(gDomainUsername, String.Empty)
            Else
                creds = Nothing
            End If

            Dim Uri As New Uri(Globals.gServerUrl)
            w = CType(Net.WebRequest.Create(Uri), Net.HttpWebRequest)
            If w IsNot Nothing Then
                w.PreAuthenticate = creds IsNot Nothing
                w.Credentials = creds

                Try
                    r = w.GetResponse()
                    hr = TryCast(r, Net.HttpWebResponse)
                Catch ex As Net.WebException
                    If ex.Status = Net.WebExceptionStatus.ProtocolError Then
                        Me.SetSyncStatus(RES_AuthenticationFailed, True)
                        Return AuthenticationStatus.UserAuthenticationFailed
                    Else
                        Me.SetSyncStatus(RES_CommsFailureMessage, True, ex.Message)
                        Return AuthenticationStatus.NoNetwork
                    End If
                End Try

                If hr Is Nothing OrElse hr.StatusCode <> Net.HttpStatusCode.OK Then
                    Me.SetSyncStatus(RES_AuthenticationFailed, True)
                    Return AuthenticationStatus.UserAuthenticationFailed
                Else
                    If Not CheckDeviceID() Then
                        Me.SetSyncStatus(RES_DeviceAuthenticationFailed, True)
                        Return AuthenticationStatus.DeviceAuthenticationFailed
                    Else
                        Try
                            Dim v As Version = GetServerVersion()
                            Dim minVersion As New Version(CInt(My.Resources.MinimumServerVersionMajor), CInt(My.Resources.MinimumServerVersionMinor), CInt(My.Resources.MinimumServerVersionBuild), 0)
                            If v >= minVersion Then ' v.Major = CInt(My.Resources.MinimumServerVersionMajor) AndAlso v.Minor >= CInt(My.Resources.MinimumServerVersionMinor) AndAlso v.Build >= CInt(My.Resources.MinimumServerVersionBuild) Then
                                Me.SetSyncStatus(RES_ServerVersionOk, False)
                                Return AuthenticationStatus.Ok
                            Else
                                Me.SetSyncStatus(RES_ServerVersionTooOld, True, CStr(v.Major), CStr(v.Minor), CStr(v.Build), CStr(v.Revision))
                                Return AuthenticationStatus.ServerTooOld
                            End If
                        Catch ex As UnauthorizedAccessException ' -1 returned by GetServerVersion = deviceIdCheck failed
                            Return AuthenticationStatus.DeviceAuthenticationFailed
                        Catch ex As ArgumentNullException
                            Return AuthenticationStatus.ServerTooOld
                        Catch ex As Net.Sockets.SocketException
#If DEBUG Then
                            LogError(MODULENAME, METHODNAME, ex, False, "Returning AuthenticationStatus.NoNetwork")
#End If
                            Return AuthenticationStatus.NoNetwork
                        Catch ex As Net.WebException
#If DEBUG Then
                            LogError(MODULENAME, METHODNAME, ex, False, "Returning AuthenticationStatus.NoNetwork")
#End If
                            Return AuthenticationStatus.NoNetwork
                        Catch ex As Exception
#If DEBUG Then
                            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
#End If
                            Me.SetSyncStatus(RES_ServerVersionTooOld, True)
                            Return AuthenticationStatus.ServerTooOld
                        End Try
                        'Return AuthenticationStatus.Ok
                    End If
                End If
            End If
        Catch ex As Net.WebException
            Me.LogMessage(ex.Message)
            Return AuthenticationStatus.NoNetwork
        Catch ex As Exception
            Me.LogMessage(My.Resources.GetUrlNavigationError)
            Return AuthenticationStatus.NoNetwork
        End Try
    End Function



    Private Delegate Function IsVisibleDelegate() As Boolean
    Public Function IsVisible() As Boolean
        Try
            If Me.InvokeRequired Then
                Dim d As New IsVisibleDelegate(AddressOf IsVisible)
                Return CBool(Me.Invoke(d))
            Else
                Return Me.Visible
            End If
        Catch ex As System.ObjectDisposedException ' hopper trapped.
            Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.IsVisible: {1}", DateTime.Now, ex.StackTrace))
        End Try
    End Function

    Private Sub Notification_ResponseSubmitted(ByVal sender As Object, ByVal e As Microsoft.WindowsCE.Forms.ResponseSubmittedEventArgs) Handles Notification.ResponseSubmitted
        'Notification.Visible = False
        Me.Popup()
    End Sub

    Private Sub AutoCloseCheckBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoCloseCheckBox.Click
        _autoClose = Not _autoClose
        AutoCloseCheckBox.Checked = _autoClose
        ConfigurationSettings.SetBooleanValue(My.Resources.AutoCloseSyncFormKey, _autoClose)
    End Sub

    Private Sub ResetServerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetServerButton.Click
        ' re-initialise web service proxy, in case of network failure.
        Startup.InitialiseWebServiceProxy(Me, True)
    End Sub

    Private Sub ScreenRefreshTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScreenRefreshTimer.Tick
        Me.StatusLabel.Text = _syncStatus
        Me.Log.Text = _syncLog
        Me.Notification.Text = _notificationText
    End Sub
End Class
