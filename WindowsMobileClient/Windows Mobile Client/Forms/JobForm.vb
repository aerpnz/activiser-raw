Imports activiser.Terminology
Imports activiser.Library.Forms
Imports Microsoft.WindowsCE.Forms
Imports activiser.Library
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.WebService
Imports activiser.Library.WebService.FormDefinition

#If MINORPLANETCLIENT Then
Imports activiser.Library.MinorPlanet
#Else
Imports activiser.Library.Gps
#End If

Public Class JobForm
    Private Const MODULENAME As String = "JobForm"

    Const STR_FilterTemplate As String = "{0} <> 0"
    Private Const STR_Job As String = "Job"
    Private Shared _requestStatusFilter As String = String.Format(WithoutCulture, STR_FilterTemplate, gClientDataSet.RequestStatus.IsClientMenuItemColumn.ColumnName)
    Private Shared _sortRequestStatusBy As String = gClientDataSet.RequestStatus.DisplayOrderColumn.ColumnName

    Private _job As JobRow
    Private mboolNewJob As Boolean
    Private _allowJobsWithNoDetails As Boolean

    Private _statusList As Generic.List(Of SelectorItem(Of Integer))
    Private _originalConsultantStatusId As Integer?

    Private timePickerInterval As Integer = AppConfig.GetSetting(My.Resources.AppConfigTimePickerIntervalKey, 5)
    Private defaultJobDuration As Integer = AppConfig.GetSetting(My.Resources.AppConfigDefaultJobDurationKey, 15)
    Private defaultTravelTime As Integer = AppConfig.GetSetting(My.Resources.AppConfigDefaultTravelTimeKey, 15)

    Private mdtStartTime, mdtFinishTime As DateTime

#Region "Validate and Save"

    'HACK: force focus away from current control, and force a validate.
    Private Sub forceValidate()
        Dim selectedTab As TabPage = Me.TabControl.TabPages(Me.TabControl.SelectedIndex)
        If selectedTab Is Me.SignatureTab Then
            Me.StatusChangeLabel.Focus()
        ElseIf selectedTab Is Me.JobHeaderTab Then
            Me.RequestNumberTextBox.Focus()
        ElseIf selectedTab Is Me.NotesTab Then
            Me.Equipment.Focus()
            Me.JobNotes.Focus()
        ElseIf selectedTab Is Me.ActivityTab Then
            Me.ActivityFinishNowButton.Focus()
        Else ' custom page
            selectedTab.Controls(0).Controls(0).Focus()
        End If
        _job.EndEdit()
    End Sub

    Private Sub loadTextFieldValue(ByVal t As TextBox)
        Dim f As String = CStr(t.Tag)
        t.Text = If(_job.IsNull(f), String.Empty, CStr(_job(f)))
    End Sub

    Private Sub saveTextFieldValue(ByVal t As TextBox)
        Dim f As String = CStr(t.Tag)
        Dim formInfoEmpty As Boolean = String.IsNullOrEmpty(t.Text)
        Dim drInfoEmpty As Boolean = _job.IsNull(f) OrElse String.IsNullOrEmpty(CStr(_job.Item(f)))

        If formInfoEmpty AndAlso drInfoEmpty Then Return
        If Not formInfoEmpty AndAlso drInfoEmpty Then
            _job(f) = t.Text
            Return
        End If

        If formInfoEmpty AndAlso Not drInfoEmpty Then
            _job(f) = DBNull.Value
            Return
        End If

        If String.Compare(t.Text, CStr(_job.Item(f)), StringComparison.Ordinal) <> 0 Then
            _job(f) = t.Text
        End If
    End Sub

    Private Function isTextFieldDirty(ByVal t As TextBox, ByVal f As String) As Boolean
        Dim formInfoEmpty As Boolean = String.IsNullOrEmpty(t.Text)
        Dim drInfoEmpty As Boolean = _job.IsNull(f) OrElse String.IsNullOrEmpty(CStr(_job.Item(f)))

        If formInfoEmpty AndAlso drInfoEmpty Then Return False
        If Not formInfoEmpty AndAlso drInfoEmpty Then Return True
        If formInfoEmpty AndAlso Not drInfoEmpty Then Return True

        Return String.Compare(t.Text, CStr(_job.Item(f)), StringComparison.Ordinal) <> 0
    End Function

    Private Function isReturnDateDirty() As Boolean
        Dim formInfoEmpty As Boolean = Not Me.ReturnDateLabel.Checked
        Dim drInfoEmpty As Boolean = _job.IsReturnDateNull

        If formInfoEmpty AndAlso drInfoEmpty Then Return False
        If Not formInfoEmpty AndAlso drInfoEmpty Then Return True
        If formInfoEmpty AndAlso Not drInfoEmpty Then Return True

        Return Me.ReturnDatePicker.Value <> _job.ReturnDate
    End Function

    Public ReadOnly Property Dirty() As Boolean
        Get
            Dim c As Control = GetActiveControl(Me)

            Try
                forceValidate()
            Catch ex As InRowChangingEventException
                Return True
            Catch ex As ReadOnlyException
                Return False
            Catch ex As NoNullAllowedException
                Return True
            Catch ex As ConstraintException
                Return True
            Finally
                If c IsNot Nothing Then c.Focus()
            End Try
#If FRAMEWORK_VERSION_GE35 Then
            If _job.HasChanges() Then Return True
#Else
            If HasChanges(_job) Then Return True
#End If
            If isTextFieldDirty(Me.JobDetails, gClientDataSet.Job.JobDetailsColumn.ColumnName) Then Return True
            If isTextFieldDirty(Me.Equipment, gClientDataSet.Job.EquipmentColumn.ColumnName) Then Return True
            If isTextFieldDirty(Me.Email, gClientDataSet.Job.EmailColumn.ColumnName) Then Return True
            If isTextFieldDirty(Me.Signatory, gClientDataSet.Job.SignatoryColumn.ColumnName) Then Return True
            If isTextFieldDirty(Me.JobNotes, gClientDataSet.Job.JobNotesColumn.ColumnName) Then Return True
            If isTextFieldDirty(Me.JobDetails, gClientDataSet.Job.EquipmentColumn.ColumnName) Then Return True

            If isReturnDateDirty() Then Return True

            If _job.FinishTime <> Me.mdtFinishTime.ToUniversalTime Then Return True
            If _job.StartTime <> Me.mdtStartTime.ToUniversalTime Then Return True

            For Each cfp As CustomFormPanel In CustomFormPanels
                If cfp.HasChanges Then Return True
            Next

            Return False
        End Get
    End Property

#End Region

    Private Function isMyJob() As Boolean
        Return Me._job.ConsultantUID.Equals(gConsultantUID)
    End Function

    Private Function isSynced() As Boolean
        Return (_job.JobStatusID = JobStatusCodes.CompleteSynchronised) OrElse (_job.JobStatusID = JobStatusCodes.SignedSynchronised)
        '(_job.JobStatusID = JobStatusCodes.CompleteSynchronised) OrElse (_job.JobStatusID = JobStatusCodes.SignedSynchronised)
    End Function

    Private Sub SetCustomFormStates(ByVal signLocked As Boolean)
        Dim lockChildren As Boolean
        Dim lockWithParent As Boolean
        Dim lockWithFlag As Boolean
        lockChildren = signLocked
        lockWithParent = AppConfig.GetSetting(My.Resources.AppConfigLockCustomJobFormsWhenSyncedKey, False) AndAlso Not _job.IsJobStatusIDNull AndAlso (_job.JobStatusID > 2)
        lockWithFlag = AppConfig.GetSetting(My.Resources.AppConfigLockCustomJobFormsWhenApprovedKey, False) AndAlso Not _job.IsFlagNull AndAlso (_job.Flag <> 0)

        For Each cfp As CustomFormPanel In Me.CustomFormPanels
            If cfp.HasChanges Then cfp.Save()
            If lockChildren Then
                cfp.ReadOnly = (cfp.CustomForm.LockCode = LockCodes.LockWithParent)
            Else
                cfp.ReadOnly = False
                If lockWithParent Then
                    cfp.SetChildLock(LockCodes.LockedWhenSynchronised Or LockCodes.LockWithParent)
                Else
                    cfp.UnSetChildLock(LockCodes.LockedWhenSynchronised Or LockCodes.LockWithParent)
                End If
                If lockWithFlag Then
                    cfp.SetChildLock(LockCodes.LockedWhenFlagged)
                Else
                    cfp.UnSetChildLock(LockCodes.LockedWhenFlagged)
                End If

            End If
        Next
    End Sub

    Private Sub SetControlStates(ByVal leaveSignatureEnabled As Boolean)
        Dim locked As Boolean ' job is locked, all fields read-only
        Dim signed As Boolean   ' this job has been signed, some fields read-only

        locked = Not (isMyJob()) OrElse isSynced() OrElse RequestLocked(_job.RequestRow)
        signed = Not _job.IsSignatureNull

        Dim unlocked As Boolean = Not locked

        Dim draft As Boolean = Not locked AndAlso Not signed
        Dim signLocked As Boolean = locked Or signed

        Me.EmailLabel.Enabled = unlocked
        Me.ReturnDateLabel.Enabled = unlocked

        ' general tab
        Me.FinishDatePicker.Enabled = draft
        Me.FinishTimePicker.Enabled = draft
        Me.FinishNowButton.Enabled = draft

        Me.StartDatePicker.Enabled = draft
        Me.StartTimePicker.Enabled = draft
        Me.StartNowButton.Enabled = draft

        Me.TravelTimePicker.Enabled = draft AndAlso Not Me.NoTravelTimeLabel.Checked
        Me.NoTravelTimeLabel.Enabled = draft

        'activity tab
        Me.ActivityFinishDatePicker.Enabled = draft
        Me.ActivityFinishTimePicker.Enabled = draft
        Me.ActivityFinishNowButton.Enabled = draft
        Me.ActivityShortCutComboBox.Enabled = draft

        Me.JobDetails.ReadOnly = signLocked

        'notes tab
        Me.Equipment.ReadOnly = signLocked
        Me.JobNotes.ReadOnly = Not (isMyJob())

        'signature tab
        Me.Signatory.ReadOnly = locked
        Me.Signature.Enabled = leaveSignatureEnabled OrElse (draft AndAlso (_allowJobsWithNoDetails OrElse Me.JobDetails.Text.Length > 0))
        Me.ClearSignatureButton.Enabled = unlocked
        Me.JobCompleteLabel.Enabled = draft AndAlso (_allowJobsWithNoDetails OrElse Me.JobDetails.Text.Length > 0)
        Me.StatusChangeLabel.Enabled = isMyJob() AndAlso signed

        SetCustomFormStates(signLocked)
    End Sub

    Private _inSaveSignature As Boolean = False
    Private Sub saveSignature()
        _inSaveSignature = True
        Try
            Dim s As String = Signature.SignatureString ' SignatureToBase64()
            If s.Length <> 0 Then
                s = EncryptSignature(s)
                If _job.IsSignatureNull OrElse _job.Signature <> s Then
                    _job.Signature = s
                End If
                _job.JobStatusID = JobStatusCodes.Signed
                Dim SignedDate As DateTime
                SignedDate = Signature.Timestamp
                Signature.Text = FormatDate(SignedDate.ToLocalTime, True)
                JobCompleteLabel.Checked = True
            Else
                If Not _job.IsSignatureNull Then
                    _job.SetSignatureNull()
                End If
                If _job.JobStatusID = JobStatusCodes.Draft Then _job.JobStatusID = JobStatusCodes.Draft
                Signature.Text = String.Empty
            End If

            If Gps.HaveValidPosition Then
                If (_job.IsTrackingInfoNull OrElse String.IsNullOrEmpty(_job.TrackingInfo)) AndAlso Gps.HaveValidPosition Then
                    Dim jobPosition As GpsPosition = Gps.LastKnownValidPosition
                    _job.TrackingInfo = jobPosition.ToString(Gps.TrackingInfoFormat, WithoutCulture)
                    _job.TrackingTimeStamp = jobPosition.Time
                    Me.GpsLabel.Text = jobPosition.ToString("l", WithoutCulture)
                End If
            End If

        Catch ex As Exception
        Finally
            _inSaveSignature = False
            SetControlStates(True)
        End Try
    End Sub

    Private Sub saveReturnDate()
        If (Me.ReturnDateLabel.CheckState = CheckState.Checked) Then
            If _job.IsReturnDateNull OrElse _job.ReturnDate <> Me.ReturnDatePicker.Value Then
                _job.ReturnDate = Me.ReturnDatePicker.Value.Date
            End If
        Else
            If Not _job.IsReturnDateNull Then
                _job.SetReturnDateNull()
            End If
        End If
    End Sub

    Private Function finishEdit() As Boolean
        If _job.RowState = DataRowState.Detached Then ' new job
            Try
                _job.ModifiedDateTime = DateTime.UtcNow
                _job.EndEdit()
                gClientDataSet.Job.AddJobRow(Me._job)
                ConsultantConfig.AddItem(STR_Job, _job.JobUID, _job.ModifiedDateTime)
                mboolNewJob = False
                Return True
            Catch ex As Exception
                DisplayFormattedMessage(Me, MODULENAME, RES_SaveError, MessageBoxIcon.Exclamation, ex.Message)
                Return False
            End Try
        Else
            _job.EndEdit()
            Return True
        End If
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Function Save(ByVal refreshAfterSave As Boolean) As Boolean
        ' don't really expect to see these in the job 'form'
        If _job.JobStatusID = JobStatusCodes.StatusChange OrElse _job.JobStatusID = JobStatusCodes.History Then
            Return True
            Exit Function
        End If

        saveTextFieldValue(Me.JobNotes)
        ' to avoid unintended saving of job when we've already synchronised...
        If _job.JobStatusID = JobStatusCodes.CompleteSynchronised OrElse _job.JobStatusID = JobStatusCodes.SignedSynchronised Then
            _job.ModifiedDateTime = DateTime.UtcNow
            If Not finishEdit() Then Return False
            Return True
        End If
        saveTextFieldValue(Me.JobDetails)
        saveTextFieldValue(Me.Equipment)
        saveTextFieldValue(Me.Email)
        saveTextFieldValue(Me.Signatory)

        saveReturnDate()
        saveSignature()

        _job.ModifiedDateTime = DateTime.UtcNow

        If Not finishEdit() Then Return False

        If Not saveCustomFormData() Then Return False

        Try
            If Me.StatusChangeLabel.CheckState = CheckState.Checked AndAlso Me.RequestStatusPicker.SelectedIndex <> -1 Then
                Dim selectedStatusId As Integer = CType(Me.RequestStatusPicker.SelectedValue, SelectorItem(Of Integer)).Value
                Dim selectedStatus As RequestStatusRow = gClientDataSet.RequestStatus.FindByRequestStatusID(selectedStatusId)
                If _originalConsultantStatusId <> selectedStatusId Then
                    If _job.RequestRow.IsConsultantStatusIDNull OrElse _job.RequestRow.ConsultantStatusID <> selectedStatusId Then
                        If ChangeRequestStatus(Me, _job.RequestRow, selectedStatusId, selectedStatus.IsReasonRequired) Then
                            _originalConsultantStatusId = selectedStatusId
                            Me.StatusChangeLabel.CheckState = CheckState.Indeterminate
                        End If
                    End If
                End If
            End If

            SavePending(gClientDataSet, gMainDbFileName)
            Return True
        Catch ex As Exception
            Debug.WriteLine(Terminology.GetFormattedString(MODULENAME, RES_SaveError, ex.Message))
            Return False
        Finally
            If refreshAfterSave Then SetControlStates(False)
        End Try
    End Function

#Region "Signature"
    Function EncryptSignature(ByVal unencryptedBase64Signature As String) As String
        If Not String.IsNullOrEmpty(unencryptedBase64Signature) Then
            Dim sigData As String = unencryptedBase64Signature.Substring(activiser.Library.Forms.Signature.VersionLength)
            Dim result As String = unencryptedBase64Signature.Substring(0, activiser.Library.Forms.Signature.VersionLength)
            result &= Encrypt(XorGuid(_job.JobUID, gApplicationGuid), sigData)
            Return result
        Else
            Return String.Empty
        End If
    End Function

    Function DecryptSignature(ByVal encryptedBase64Signature As String) As String
        If Not String.IsNullOrEmpty(encryptedBase64Signature) Then
            Dim sigData As String = encryptedBase64Signature.Substring(activiser.Library.Forms.Signature.VersionLength)
            Dim result As String = encryptedBase64Signature.Substring(0, activiser.Library.Forms.Signature.VersionLength)
            result &= Decrypt(XorGuid(_job.JobUID, gApplicationGuid), sigData)
            Return result
        Else
            Return String.Empty
        End If
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Sub LoadSignature() ' As Byte()
        Const METHODNAME As String = "LoadSignature"
        Try
            If _job.IsSignatureNull OrElse _job.Signature.Length < 10 Then
                Return
            End If
            Dim s As String = _job.Signature
            If s = String.Empty OrElse Not _job.Signature.StartsWith(Library.Forms.Signature.Prefix, StringComparison.Ordinal) Then
                Return
            End If
            Signature.SignatureString = DecryptSignature(s)
            Signature.Text = FormatDate(Signature.Timestamp.ToLocalTime, True)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Return
        End Try
    End Sub

#End Region

    Private Sub SetJobStartTime(ByVal value As Date)
        If Me.mdtStartTime <> value Then
            Me.mdtStartTime = value
            Me.StartDatePicker.Value = Me.mdtStartTime.Date
            Me.StartTimePicker.Value = Me.mdtStartTime

            ' use JobDate to store 'local' date/time information, for later conversions at back-end
            '
            If _job.IsJobDateNull OrElse _job.JobDate <> mdtStartTime Then
                _job.JobDate = value
            End If

            If _job.IsStartTimeNull OrElse _job.StartTime <> mdtStartTime.ToUniversalTime Then
                _job.StartTime = mdtStartTime.ToUniversalTime
            End If

            If Me.mdtFinishTime < Me.mdtStartTime Then
                Me.SetJobFinishTime(Me.mdtStartTime)
            End If
            Me.FinishDatePicker.MinDate = Me.StartDatePicker.Value.Date
            Me.FinishTimePicker.MinDate = Me.StartTimePicker.Value
            Me.ActivityFinishDatePicker.MinDate = Me.StartTimePicker.Value.Date
            Me.ActivityFinishTimePicker.MinDate = Me.StartTimePicker.Value
        End If
    End Sub

    Private Sub SetJobFinishTime(ByVal value As Date)
        If Me.mdtFinishTime <> value Then
            'MakeDirty()
            If value < Me.mdtStartTime Then value = Me.mdtStartTime
            Me.mdtFinishTime = value
            Me.FinishDatePicker.Value = Me.mdtFinishTime.Date
            Me.FinishTimePicker.Value = Me.mdtFinishTime
            Me.ActivityFinishDatePicker.Value = Me.mdtFinishTime.Date
            Me.ActivityFinishTimePicker.Value = Me.mdtFinishTime

            If _job.IsFinishTimeNull OrElse _job.FinishTime <> mdtFinishTime.ToUniversalTime Then
                _job.FinishTime = mdtFinishTime.ToUniversalTime
            End If
        End If
    End Sub

#Region "Property Job Helpers"
    Private Function SaveIfGotDirtyJob() As Boolean
        If _job IsNot Nothing Then
            If Me.Dirty Then
                If Not Save(False) Then
                    Terminology.DisplayMessage(Me, MODULENAME, RES_JobSetFailed, MessageBoxIcon.Exclamation)
                    Return False
                End If
            Else
                _job.CancelEdit()
            End If
        End If
        Return True
    End Function

    Private Sub SetJobNumber()
        If Not _job.IsJobNumberNull Then
            Me.JobNumberLabel.Text = _job.JobNumber
        ElseIf Not _job.IsJobIDNull Then
            Me.JobNumberLabel.Text = CStr(_job.JobID)
        Else
            Me.JobNumberLabel.Text = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
        End If
    End Sub

    Private Sub SetClientDetail()
        Me.ClientSiteNameTextBox.Text = _job.ClientSiteRow.SiteName
    End Sub

    Private Sub SetRequestDetail()
        If Me._job.RequestRow.IsRequestNumberNull Then
            Me.RequestNumberTextBox.Text = If(Not _job.RequestRow.IsRequestIDNull, CStr(_job.RequestRow.RequestID), Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown))
        Else
            Me.RequestNumberTextBox.Text = _job.RequestRow.RequestNumber ' .RequestID.ToString
        End If

        If Not _job.RequestRow.IsShortDescriptionNull Then
            Me.RequestDescription.Text = _job.RequestRow.ShortDescription
        End If
    End Sub

    Private Sub SetStatusChange()
        If Not _job.RequestRow.IsConsultantStatusIDNull Then
            _originalConsultantStatusId = _job.RequestRow.ConsultantStatusID
        Else
            _originalConsultantStatusId = _job.RequestRow.RequestStatusID
        End If

        Dim currentRequestStatus As RequestStatusRow = RequestStatus(_originalConsultantStatusId.Value)
        Me.RequestStatusPicker.SelectedIndex = Me.RequestStatusPicker.Items.IndexOf(currentRequestStatus.RequestStatusID)
        Me.StatusChangeLabel.Checked = currentRequestStatus.IsNewStatus
        Me.RequestStatusPicker.Enabled = Me.StatusChangeLabel.Checked
    End Sub


    Private Sub SetJobData()
        loadTextFieldValue(Me.JobNotes)
        loadTextFieldValue(Me.JobDetails)
        loadTextFieldValue(Me.Equipment)
        loadTextFieldValue(Me.Email)
        loadTextFieldValue(Me.Signatory)

        Me.EmailLabel.Checked = Me.Email.Text <> String.Empty
        Me.JobCompleteLabel.Checked = _job.JobStatusID <> JobStatusCodes.Draft

        If Not _job.IsSignatureNull Then
            LoadSignature()
        Else
            Me.Signature.Clear()
        End If
    End Sub

    Private Sub SetTravelTime()
        Me.NoTravelTimeLabel.Checked = _job.IsMinutesTravelledNull OrElse _job.MinutesTravelled = 0
        If Not Me.NoTravelTimeLabel.Checked Then
            Dim timeTravelled As TimeSpan = TimeSpan.FromMinutes(_job.MinutesTravelled)
            Me.TravelTimePicker.Value = DateTime.Today + timeTravelled
        End If
    End Sub
    Private Sub SetGpsInfo()
        If _job.IsTrackingInfoNull OrElse String.IsNullOrEmpty(_job.TrackingInfo) Then
            Me.GpsLabel.Text = String.Empty
        Else
            Try
                Dim gpsPos As New GpsPosition(_job.TrackingInfo) ', _job.TrackingTimeStamp)
                Me.GpsLabel.Text = gpsPos.ToString("l", WithoutCulture)
            Catch ex As Exception
                Me.GpsLabel.Text = String.Empty
            End Try
        End If
    End Sub
    Private Sub SetStartFinishTimes()
        SetJobFinishTime(If(Not _job.IsFinishTimeNull, _job.FinishTime.ToLocalTime, DateTime.Now))

        ' set start time after finish time to trap data errors - if finish time is earlier than start time,
        ' then the set starttime will correct it.
        SetJobStartTime(If(Not _job.IsStartTimeNull, _job.StartTime.ToLocalTime, DateTime.Now))
    End Sub
    Private Sub SetReturnDate()
        Me.ReturnDateLabel.Checked = Not _job.IsReturnDateNull
        If Me.ReturnDateLabel.Checked Then
            Me.ReturnDatePicker.Value = _job.ReturnDate
        Else
            Me.ReturnDatePicker.Value = System.DateTime.Now
        End If
    End Sub
#End Region

    Private _inJob_Set As Boolean
    Public Property Job() As JobRow
        Get
            Return _job
        End Get
        Set(ByVal value As JobRow)
            If value Is Nothing Then
                Throw New ArgumentNullException("value")
            End If

            Try
                _inJob_Set = True
                If Not SaveIfGotDirtyJob() Then Return
                If value.IsRequestUIDNull Then Throw New ArgumentException(Terminology.GetString(MODULENAME, RES_JobRequestUidIsNull))
                _job = value

                SetClientDetail()
                SetJobNumber()
                SetRequestDetail()
                SetStatusChange()

                If Not mboolNewJob Then SetJobData()

                SetStartFinishTimes()
                SetTravelTime()

                SetReturnDate()
                SetGpsInfo()

                SetCustomFormParents()
                SetControlStates(False)

                _job.BeginEdit()
            Catch ex As Exception
                Throw
            Finally
                _inJob_Set = False
            End Try
        End Set
    End Property


    Private Sub New(ByVal owner As Form)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Owner = owner
        SetWindowState()

        Me.LoadRequestStatusPicker()
        JobForms.Add(Me)
        Terminology.LoadLabels(Me)
        LoadCustomForms()
        LoadImage()

        Try
            Dim showShortcuts As Boolean = AppConfig.GetSetting("ShowJobShortCutList", True)
            Me.ActivityShortCutPanel.Visible = showShortcuts
            Me.ShowShortCutList.Checked = showShortcuts
            If showShortcuts Then
                LoadShortCutList()
            End If
        Catch
        End Try

        SignatureTimer = New Timer()
        SignatureTimer.Interval = 500
        SignatureTimer.Enabled = False

        Me.StartDatePicker.CustomFormat = gDateFormat
        Me.FinishDatePicker.CustomFormat = gDateFormat
        Me.ReturnDatePicker.CustomFormat = gDateFormat
        Me._allowJobsWithNoDetails = AppConfig.GetSetting("AllowJobsWithNoDetails", False)

    End Sub
    ' open an existing job
    Public Sub New(ByVal owner As Form, ByVal job As JobRow)
        Me.New(owner)
        'Me.LoadRequestStatusPicker()
        Me.Job = job
    End Sub

    ' create a new job for a request
    Public Sub New(ByVal owner As Form, ByVal request As RequestRow)
        Me.New(owner)
        If request Is Nothing Then
            Exit Sub
        End If

        'Me.LoadRequestStatusPicker()
        mboolNewJob = True
        Dim oJob As JobRow = gClientDataSet.Job.NewJobRow

        oJob.RequestRow = request
        oJob.ClientSiteRow = request.ClientSiteRow
        oJob.JobUID = Guid.NewGuid
        oJob.ConsultantUID = gConsultantUID
        oJob.MinutesTravelled = defaultTravelTime

        Dim startOffset As Integer = AppConfig.GetSetting(My.Resources.AppConfigDefaultJobStartKey, 0)
        oJob.StartTime = FixTime(DateTime.UtcNow.AddMinutes(startOffset), timePickerInterval)
        oJob.JobDate = oJob.StartTime.ToLocalTime()
        oJob.FinishTime = oJob.StartTime.AddMinutes(defaultJobDuration)
        oJob.JobStatusID = JobStatusCodes.Draft
        oJob.CreatedDateTime = DateTime.UtcNow

        Me.Job = oJob


    End Sub



    Private _RequestStatusWipIndex As Integer
    Private Sub LoadRequestStatusPicker()
        Const METHODNAME As String = "LoadRequestStatusPicker"
        Try
            Dim sortedList() As RequestStatusRow
            sortedList = CType(gClientDataSet.RequestStatus.Select(_requestStatusFilter, _sortRequestStatusBy), RequestStatusRow())

            _statusList = New Generic.List(Of SelectorItem(Of Integer))()
            _RequestStatusWipIndex = -1

            For Each rsr As RequestStatusRow In sortedList
                If rsr.RequestStatusID = RequestStatusCodes.WIP Then
                    _RequestStatusWipIndex = _statusList.Count
                End If
                _statusList.Add(New SelectorItem(Of Integer)(rsr.RequestStatusID, rsr.Description))
            Next
            Me.RequestStatusPicker.DataSource = _statusList

            Me.RequestStatusPicker.SelectedIndex = -1
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, RES_RequestStatusListLoadError)
        End Try
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Sub Job_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gHoldAutoSync = True
        Me.StartDatePicker.CustomFormat = gDateFormat
        Me.FinishDatePicker.CustomFormat = gDateFormat
        Me.ActivityFinishDatePicker.CustomFormat = gDateFormat
        Me.ReturnDatePicker.CustomFormat = gDateFormat

        JobForms.Add(Me)
#If MINORPLANETCLIENT Then
        EnableContextMenus(Me.Controls)
#Else
        If Gps.HideGps Then
            If Me.MainMenu.MenuItems.Contains(Me.MainMenuGps) Then
                Me.MainMenu.MenuItems.Remove(Me.MainMenuGps)
            End If
            Me.GpsLabel.Visible = False
        Else
            If DeviceState IsNot Nothing AndAlso DeviceState.DeviceState = GpsServiceState.On AndAlso DeviceState.ServiceState = GpsServiceState.On Then
                Me.SetGpsMenuState(GpsServiceState.On)
            Else
                Me.SetGpsMenuState(GpsServiceState.Off)
            End If
            Me.GpsLabel.Visible = True
        End If
#End If

    End Sub

    Friend Shared Function FixTime(ByVal victim As DateTime, ByVal granularity As Integer) As DateTime
        Dim iOddMinutes As Integer
        victim = victim.Subtract(New TimeSpan(0, 0, victim.Second))
        If granularity > 1 Then
            iOddMinutes = victim.Minute Mod granularity
            If iOddMinutes <= granularity \ 2 Then
                victim = victim.Subtract(New TimeSpan(0, iOddMinutes, 0))
            Else
                victim = victim.Add(New TimeSpan(0, granularity - iOddMinutes, 0))
            End If
        End If
        Return victim
    End Function

    Private Function GetRoundedTime() As DateTime
        Return FixTime(Now, Me.timePickerInterval)
    End Function

    Private Sub Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuSave.Click
        Save(True)
    End Sub

    Private Sub Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub JobComplete_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobCompleteLabel.CheckStateChanged, JobCompleteLabel.CheckStateChanged
        If Me._inJob_Set Then Return ' since this event fires when the state is changed from code; we don't want to be resetting the status...
        If Me._inSaveSignature Then Return
        If (Me.JobCompleteLabel.CheckState = CheckState.Checked) Then
            If _job.IsSignatureNull Then
                _job.JobStatusID = JobStatusCodes.Complete
            Else
                _job.JobStatusID = JobStatusCodes.Signed
            End If

            If Gps.HaveValidPosition Then
                Dim jobPosition As GpsPosition = Gps.LastKnownValidPosition
                _job.TrackingInfo = jobPosition.ToString(Gps.TrackingInfoFormat, WithoutCulture)
                _job.TrackingTimeStamp = jobPosition.Time
                Me.GpsLabel.Text = jobPosition.ToString("l", WithoutCulture)
            End If
        Else
            _job.JobStatusID = JobStatusCodes.Draft
        End If
        SetControlStates(True)
    End Sub

    Private Sub LoadShortCutList()
        Me.ActivityShortCutComboBox.Items.Clear()

        If ConsultantConfig.Shortcuts.Count > 0 Then
            For Each s As String In ConsultantConfig.Shortcuts
                Me.ActivityShortCutComboBox.Items.Add(s)
            Next
        Else
            Me.ActivityShortCutComboBox.Text = Terminology.GetString(MODULENAME, RES_JobFormNoShortCuts)
        End If
    End Sub

    Private Sub ShortcutSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivityShortCutSetupButton.Click
        Const METHODNAME As String = "ShortcutSetup_Click"
        Try
            'activiser.PlaySound()
            Using f As New SettingsForm(Me)
                f.TabControl.SelectedIndex = f.TabControl.TabPages.IndexOf(f.ShortCutTab)
                f.ShortCutList.Focus()
                'f.InputPanel.Enabled = True
                f.ShowDialog()
            End Using
            Me.Activate()
            LoadShortCutList()
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
        JobDetails.Focus()
    End Sub

    Private Sub Travel_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NoTravelTimeLabel.CheckStateChanged, NoTravelTimeLabel.CheckStateChanged
        If Me._inJob_Set Then Return
        Me.TravelTimePicker.Enabled = NoTravelTimeLabel.CheckState = CheckState.Unchecked

        If Not TravelTimePicker.Enabled Then
            Me.TravelTimePicker.Value = New DateTime(1900, 1, 1, 0, 0, 0)
            _job.MinutesTravelled = 0
        Else
            _job.MinutesTravelled = AppConfig.GetSetting(My.Resources.AppConfigDefaultTravelTimeKey, 15)
            Me.TravelTimePicker.Value = New DateTime(1900, 1, 1, 0, _job.MinutesTravelled, 0)
        End If
    End Sub

    Private Sub chkEmail_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmailLabel.CheckStateChanged
        If isMyJob() Then
            Me.Email.ReadOnly = Not Me.EmailLabel.CheckState = CheckState.Checked
        End If

        If _inJob_Set Then
            Return
        End If

        If EmailLabel.Checked Then
            If Not _job.ClientSiteRow.IsSiteContactEmailNull Then
                Me.Email.Text = _job.ClientSiteRow.SiteContactEmail
                Me.Email.Focus()
                Me.Email.SelectAll()
            End If
        Else
            Me.Email.Text = String.Empty
        End If

        'MakeDirty()
    End Sub

    Private Sub ReturnVisit_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReturnDateLabel.CheckStateChanged
        If isMyJob() Then
            Me.ReturnDatePicker.Enabled = Me.ReturnDateLabel.CheckState = CheckState.Checked
        End If
    End Sub

    Private Sub Sync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuSynchronise.Click
        If Not Save(True) Then
            Terminology.DisplayMessage(Me, MODULENAME, "SyncSaveFailed", MessageBoxIcon.Exclamation)
            Return
        End If
        Using sf As New SyncForm(Me)
            Synchronisation.StartManualSync()
            sf.ShowDialog()
        End Using
        'WaitForSync()
        Me.Refresh()
    End Sub

    'Private Sub mnuEditClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.ActivityDetails.Text = String.Empty
    'End Sub

    Private WithEvents SignatureTimer As Timer
    Private ResetTimer As Boolean
    Private Sub SignatureTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SignatureTimer.Tick
        If Not ResetTimer Then
            Try
                SignatureTimer.Enabled = False

                saveSignature()

                SetControlStates(True)
            Catch ex As ObjectDisposedException ' hopper found this !

            End Try

            Return
        End If
        ResetTimer = False
    End Sub

    Private Sub SignatureChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Signature.SignatureChanged, Signature.Click
        ResetTimer = True
        If Not SignatureTimer.Enabled Then
            SignatureTimer.Enabled = True
        End If
    End Sub

    Private Sub btnStartNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartNowButton.Click
        SetJobStartTime(FixTime(Now, Me.timePickerInterval))
    End Sub

    Private Sub FinishNow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FinishNowButton.Click, ActivityFinishNowButton.Click
        Me.SetJobFinishTime(FixTime(Now, Me.timePickerInterval))
    End Sub

    Private Sub ClearSignature_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearSignatureButton.Click
        If Terminology.AskQuestion(Me, MODULENAME, RES_ConfirmClearSignature, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            Me.Signature.Clear()
            Me.Signatory.Text = String.Empty
            Me.Signature.Text = String.Empty

            _job.SetSignatureNull()
            _job.JobStatusID = JobStatusCodes.Draft

            JobCompleteLabel.Checked = False
            JobCompleteLabel.Enabled = True
            StatusChangeLabel.Enabled = False
            StatusChangeLabel.Enabled = False

            SetControlStates(False)
        End If
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Const METHODNAME As String = "Form_Closing"
        Try
            If Me.SignatureTimer.Enabled Then '' signature not finished yet.
                Me.SignatureTimer.Enabled = False '' abort time, and force immediate capture.
                saveSignature()
            End If
            AppConfig.SaveSetting("ShowJobShortCutList", Me.ActivityShortCutComboBox.Visible.ToString)
            If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
                If Me.Dirty Then
                    Select Case AskQuestion(Me, MODULENAME, RES_ConfirmSave, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1)
                        Case Windows.Forms.DialogResult.Yes
                            If Not Save(False) Then
                                e.Cancel = True
                            Else
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                        Case Windows.Forms.DialogResult.Cancel
                            e.Cancel = True
                        Case Windows.Forms.DialogResult.No
                            _job.CancelEdit()
                            For Each cfp As CustomFormPanel In Me.CustomFormPanels
                                cfp.Cancel()
                            Next
                            Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    End Select
                End If
            Else

                If Me.Dirty Then
                    If Not Save(False) Then
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            'If Not e.Cancel AndAlso Me.Owner IsNot Nothing Then
            '    Me.Owner.Show()
            'End If
        End Try

    End Sub

    Private Sub ActivityShortCutComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivityShortCutComboBox.SelectedIndexChanged
        Me.JobDetails.SelectedText = Me.ActivityShortCutComboBox.Text & vbNewLine
        JobDetails.Focus()
    End Sub


    '======================================
    Public Sub LoadImage()
        If Screen.PrimaryScreen.Bounds.Width > 320 Then ' better than QVGA
            ActivityShortCutSetupButton.Image = My.Resources.EditIcon24
        Else
            ActivityShortCutSetupButton.Image = My.Resources.EditIcon16
        End If
    End Sub

    Private inDateTimePicker_Validated As Boolean
    Private inTimePicker_ValueChanged As Boolean
    Private inTimePickerKeyPress As Boolean

    Private Sub TimePicker_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ActivityFinishTimePicker.KeyDown, FinishTimePicker.KeyDown, StartTimePicker.KeyDown, TravelTimePicker.KeyDown
        inTimePickerKeyPress = True
    End Sub

    Private Sub TimePicker_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartTimePicker.ValueChanged, FinishTimePicker.ValueChanged, ActivityFinishTimePicker.ValueChanged, TravelTimePicker.ValueChanged
        If Not inTimePicker_ValueChanged AndAlso Not inTimePickerKeyPress Then
            inTimePicker_ValueChanged = True
            Dim dt As DateTimePicker = TryCast(sender, DateTimePicker)
            If dt IsNot Nothing Then
                Dim time As Date = FixTime(dt.Value, timePickerInterval) 'round time to nearest ? minutes
                If time < dt.Value Then
                    dt.Value = time.AddMinutes(timePickerInterval)
                ElseIf time > dt.Value Then
                    If dt.Value.Minute = 59 Then ' we're going down past the hour
                        dt.Value = time.AddMinutes(-(60 + timePickerInterval))
                    Else
                        dt.Value = time.AddMinutes(-timePickerInterval)
                    End If
                Else ' unlikely, but for completeness...
                    dt.Value = time
                End If
            End If
            inTimePicker_ValueChanged = False
        End If
        Debug.WriteLine("TimePicker_ValueChanged")
        inTimePickerKeyPress = False
    End Sub

    Private Sub StartDateTimePicker_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartDatePicker.Validated, StartTimePicker.Validated
        If Me._inJob_Set Then Return
        If Not inDateTimePicker_Validated Then
            inDateTimePicker_Validated = True
            Dim dt As DateTimePicker = TryCast(sender, DateTimePicker)
            If dt IsNot Nothing Then
                If dt Is StartTimePicker OrElse dt Is StartDatePicker Then
                    Me.SetJobStartTime(Me.StartDatePicker.Value.Date + StartTimePicker.Value.TimeOfDay)
                End If
            End If

            inDateTimePicker_Validated = False
        End If
    End Sub

    Private Sub FinishDateTimePicker_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FinishDatePicker.Validated, FinishTimePicker.Validated, ActivityFinishDatePicker.Validated, ActivityFinishTimePicker.Validated
        If Me._inJob_Set Then Return
        If Not inDateTimePicker_Validated Then
            inDateTimePicker_Validated = True
            Dim dt As DateTimePicker = TryCast(sender, DateTimePicker)
            If dt IsNot Nothing Then
                If dt Is FinishTimePicker OrElse dt Is FinishDatePicker Then
                    Me.SetJobFinishTime(Me.FinishDatePicker.Value.Date + FinishTimePicker.Value.TimeOfDay)
                ElseIf sender Is ActivityFinishTimePicker OrElse sender Is ActivityFinishDatePicker Then
                    Me.SetJobFinishTime(ActivityFinishDatePicker.Value.Date + Me.ActivityFinishTimePicker.Value.TimeOfDay)
                End If
            End If
            inDateTimePicker_Validated = False
        End If
    End Sub

    Private Sub TravelTimePicker_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TravelTimePicker.Validated
        If Me._inJob_Set Then Return
        If Not inDateTimePicker_Validated Then
            inDateTimePicker_Validated = True
            Dim dt As DateTimePicker = TryCast(sender, DateTimePicker)
            If dt IsNot Nothing Then
                Dim time As Date = FixTime(dt.Value, Me.timePickerInterval)
                If dt Is TravelTimePicker Then
                    dt.Value = time
                    If Not Me.NoTravelTimeLabel.CheckState = CheckState.Checked Then
                        If _job.IsMinutesTravelledNull OrElse _job.MinutesTravelled <> CInt(Me.TravelTimePicker.Value.TimeOfDay.TotalMinutes) Then
                            _job.MinutesTravelled = CInt(Me.TravelTimePicker.Value.TimeOfDay.TotalMinutes)
                        End If
                    Else
                        _job.MinutesTravelled = 0
                    End If
                End If
            End If
            inDateTimePicker_Validated = False
        End If
    End Sub

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.SelectedIndexChanged
        Debug.WriteLine("TabControl Selected Index Changed")
        'Me.InputPanelPanel.Visible = InputPanel.Enabled AndAlso Me.SignatureTab IsNot Me.TabControl.TabPages(Me.TabControl.SelectedIndex)
        'Me.InputPanelPanel.Height = InputPanel.Bounds.Height
    End Sub

    Private Sub ViewShowShortCutList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowShortCutList.Click
        Dim showShortcuts As Boolean = Not Me.ShowShortCutList.Checked
        Me.ActivityShortCutPanel.Visible = showShortcuts
        Me.ShowShortCutList.Checked = showShortcuts
        If showShortcuts Then
            LoadShortCutList()
        End If
        AppConfig.SaveSetting(My.Resources.JobFormShortShortCutListKey, showShortcuts)
    End Sub


    Private Sub NextTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click
        If Me.TabControl.SelectedIndex < Me.TabControl.TabPages.Count - 1 Then
            Me.TabControl.SelectedIndex += 1
        Else
            Me.TabControl.SelectedIndex = 0
        End If
    End Sub


#Region "Custom Form Support"
    Private CustomFormPanels As New Generic.List(Of CustomFormPanel)
    Private Sub LoadCustomForms()
        'Dim jobForms = From cfr As FormRow In gFormDefinitions.Form.Cast(Of FormRow)() _
        '               Where cfr.ParentEntityName = STR_Job _
        '               Order By cfr.Priority _
        '               Select cfr
        Dim jobForms As New Generic.SortedList(Of Integer, FormRow)
        For Each cfr As FormRow In gFormDefinitions.Form.Rows
            If cfr.ParentEntityName = STR_Job Then
                jobForms.Add(cfr.Priority, cfr)
            End If
        Next

        If jobForms.Count = 0 Then Return

        Me.SuspendLayout()

        Dim formsBeforeNotes As Boolean = AppConfig.GetSetting(My.Resources.JobFormCustomTabsBeforeNotes, True)
        For Each cfr As FormRow In jobForms.Values
            Dim tp As New TabPage
            tp.Text = cfr.FormLabel
            tp.Name = cfr.FormName
            If formsBeforeNotes Then
#If MINORPLANETCLIENT Then
                Me.TabControl.TabPages.Insert(Me.TabControl.TabPages.IndexOf(Me.EquipmentTab), tp)
#Else
                Me.TabControl.TabPages.Insert(Me.TabControl.TabPages.IndexOf(Me.NotesTab), tp)
#End If
            Else
                Me.TabControl.TabPages.Insert(Me.TabControl.TabPages.IndexOf(Me.SignatureTab), tp)
                'Me.TabControl.TabPages.Add(tp)
            End If

            Dim cfp As New CustomFormPanel(cfr, tp, Me)
            cfp.Dock = DockStyle.Fill
            tp.Controls.Add(cfp)
            AddHandler cfp.Validating, AddressOf CustomFormValidating
            CustomFormPanels.Add(cfp)
        Next
        Me.ResumeLayout()
    End Sub

    Private Sub CustomFormValidating(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs)
        Debug.WriteLine("CustomForm Validating")
    End Sub

    Private Sub SetCustomFormParents()
        For Each cfp As CustomFormPanel In CustomFormPanels
            Debug.WriteLine("Job Form setting custom form parent filter: " & _job.JobUID.ToString)
            cfp.SetParentFilter(_job.JobUID)
        Next
    End Sub

    Private Function saveCustomFormData() As Boolean
        Dim result As Boolean = True
        For Each cfp As CustomFormPanel In CustomFormPanels
            Debug.WriteLine("Job Form saving custom form data: " & _job.JobUID.ToString)
            If Not cfp.Save() Then
                result = False
            End If
        Next
        Return result
    End Function
#End Region

    Private Sub ValidateTextField(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Signatory.Validated, JobNotes.Validated, Equipment.Validated, Email.Validated, JobDetails.Validated

        If Not isMyJob() AndAlso (sender IsNot Me.JobNotes) Then Return

        Dim tb As TextBox = TryCast(sender, TextBox)
        If tb Is Nothing Then Return
        If tb.ReadOnly Then Return
        If tb.Tag Is Nothing Then Return

        Dim tag As String = CStr(tb.Tag)
        If String.IsNullOrEmpty(tag) Then Return

        If tb.Text.Length <> 0 Then
            If _job.IsNull(tag) OrElse CStr(_job.Item(tag)) <> tb.Text Then
                _job(tag) = tb.Text
            End If
        Else
            If Not _job.IsNull(tag) Then
                _job(tag) = DBNull.Value
            End If
        End If

        If sender Is JobDetails Then
            If _allowJobsWithNoDetails OrElse (Not _job.IsJobDetailsNull) Then
                Me.Signature.Enabled = Me.Job.IsSignatureNull
                Me.JobCompleteLabel.Enabled = Me.Job.IsSignatureNull
            End If
        End If
    End Sub

    Private Sub NextActionDatePicker_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReturnDatePicker.Validated

    End Sub

    Private Sub ViewShowFinishTimeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowActivitiesFinishTimeMenuItem.Click
        ShowActivitiesFinishTimeMenuItem.Checked = Not ShowActivitiesFinishTimeMenuItem.Checked
        Me.ActivityTimePanel.Visible = ShowActivitiesFinishTimeMenuItem.Checked
    End Sub

#If WINDOWSMOBILE Then
    Private Sub ViewFullScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFullScreen.Click
        SetFormState(Me, Me.ViewFullScreen, Not Me.ViewFullScreen.Checked)
    End Sub
#End If

    Private Sub JobForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            If JobForms.Contains(Me) Then
                JobForms.Remove(Me)
            End If
        Catch ex As Exception

        End Try
        gHoldAutoSync = False
    End Sub

    Private Sub JobForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        Try
            If JobForms.Contains(Me) Then
                JobForms.Remove(Me)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub JobForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'If Me.JobDetails.Focused OrElse Me.JobNotes.Focused OrElse Me.EquipmentList.Focused Then Return

        If e.KeyCode = Keys.Up Then
            Dim startFrom As Control = GetActiveControl(Me)
            If startFrom Is Nothing Then Return
            Me.SelectNextControl(startFrom, True, True, True, True)
            e.Handled = True
        ElseIf e.KeyCode = Keys.Down Then
            Dim startFrom As Control = GetActiveControl(Me)
            If startFrom Is Nothing Then Return
            Me.SelectNextControl(startFrom, False, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private _inStatusChangeLabel_CheckStateChanged As Boolean
    Private Sub StatusChangeLabel_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusChangeLabel.CheckStateChanged
        If _inStatusChangeLabel_CheckStateChanged Then Return
        _inStatusChangeLabel_CheckStateChanged = True
        Me.RequestStatusPicker.Enabled = Me.StatusChangeLabel.Checked
        _inStatusChangeLabel_CheckStateChanged = False
    End Sub

    'Private Sub StatusChangeLabel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusChangeLabel.Click
    '    Me.StatusChangeLabel.Checked = Not Me.StatusChangeLabel.Checked
    'End Sub

    Private Sub Control_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RequestNumberTextBox.KeyDown, Signatory.KeyDown, ReturnDateLabel.KeyDown, RequestDescription.KeyDown, StatusChangeLabel.KeyDown, ReturnDatePicker.KeyDown, EmailLabel.KeyDown, Email.KeyDown, ClientSiteNameTextBox.KeyDown, ClearSignatureButton.KeyDown, TabControl.KeyDown, StartNowButton.KeyDown, FinishNowButton.KeyDown, StartDatePicker.KeyDown, FinishDatePicker.KeyDown, ActivityFinishDatePicker.KeyDown
        If e.KeyCode = Keys.Up Then
            Me.SelectNextControl(CType(sender, Control), False, True, True, True)
            e.Handled = True
        ElseIf e.KeyCode = Keys.Down Then
            Me.SelectNextControl(CType(sender, Control), True, True, True, True)
            e.Handled = True
        End If
    End Sub

#If MINORPLANETCLIENT Then
    ' no GPS menus.
#Else
    Private Delegate Sub SetGpsMenuStateDelegate(ByVal GpsState As GpsServiceState)

    Friend Sub SetGpsMenuState(ByVal GpsState As GpsServiceState)
        If Me.InvokeRequired Then
            Dim d As New SetGpsMenuStateDelegate(AddressOf SetGpsMenuState)
            Me.Invoke(d, GpsState)
        Else
            Me.MainMenuGpsOn.Checked = GpsState = GpsServiceState.On
            Me.MainMenuGpsOff.Checked = Not Me.MainMenuGpsOn.Checked
            Me.MainMenuGpsOff.Enabled = Me.MainMenuGpsOn.Checked
            Me.MainMenuGpsOn.Enabled = Not Me.MainMenuGpsOn.Checked
        End If
    End Sub

    Private Sub MainMenuGPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuGpsInfo.Click
        Gps.DisplayGpsInfo(Me)
    End Sub

    Private Sub MainMenuGpsOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuGpsOn.Click
        Gps.StartGPS()
    End Sub

    Private Sub MainMenuGpsOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuGpsOff.Click
        Gps.StopGps()
    End Sub

#End If

    Private Sub MainMenuRequestDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuRequestDetails.Click
        If Me.Job.RequestRow Is Nothing Then Return
        Dim message As String
        If Me.Job.RequestRow.IsLongDescriptionNull OrElse String.IsNullOrEmpty(Me.Job.RequestRow.LongDescription) Then
            message = String.Format("{1}{0}{2}", vbCrLf, Job.RequestRow.RequestNumber, Job.RequestRow.ShortDescription)
        Else
            message = String.Format("{1}{0}{2}{0}--------------------{0}{3}", vbCrLf, Job.RequestRow.RequestNumber, Job.RequestRow.ShortDescription, Job.RequestRow.LongDescription)
        End If
        Using db As New DialogBox(Me, message, MessageBoxButtons.OK, MessageBoxIcon.None)
            db.CaptionAlign = Drawing.ContentAlignment.TopLeft
            db.ShowDialog()
        End Using
        Me.BringToFront()
        Me.Activate()
    End Sub

    Private Sub MainMenuClientDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClientDetails.Click
        'If Me.Job.RequestRow Is Nothing OrElse Me.Job.RequestRow.IsLongDescriptionNull Then Return
        If Me.Job.RequestRow.ClientSiteRow Is Nothing Then Return

        Dim c As ClientSiteRow = Me.Job.RequestRow.ClientSiteRow
        Dim message As String = Terminology.GetFormattedString(MODULENAME, RES_ClientDetailsFormat, _
                                    c.SiteName, _
                                    If(c.IsContactNull, Terminology.GetString(MODULENAME, RES_NoClientContactDetails), c.Contact), _
                                    If(c.IsContactPhone1Null, Terminology.GetString(MODULENAME, RES_NoClientPhone), c.ContactPhone1), _
                                    If(c.IsSiteAddressNull, Terminology.GetString(MODULENAME, RES_NoClientAddress), c.SiteAddress))

        Using db As New DialogBox(Me, message, MessageBoxButtons.OK, MessageBoxIcon.None)
            db.CaptionAlign = Drawing.ContentAlignment.TopLeft
            db.ShowDialog()
        End Using
        Me.BringToFront()
        Me.Activate()
    End Sub

    Private Sub SetWindowState()
        If Me.InvokeRequired Then
            Dim d As New SimpleSubDelegate(AddressOf SetWindowState)
            Me.Invoke(d)
            Return
        End If
#If WINDOWSMOBILE Then
        Me.WindowState = Owner.WindowState
        Me.ViewFullScreen.Checked = Me.WindowState = FormWindowState.Maximized
#End If

        Dim fontSize As Integer = AppConfig.GetSetting(My.Resources.AppConfigTextSizeKey, 8)
        Dim bigFont As Font = New Font(Me.Font.Name, If(fontSize < 8, fontSize + 1, fontSize), FontStyle.Regular)
        Dim mainFont As Font = New Font(Me.Font.Name, fontSize, FontStyle.Regular)
        Dim labelFont As Font = New Font(Me.Font.Name, fontSize, FontStyle.Bold)
        Dim detailFont As New Font(Me.Font.Name, If(fontSize < 5, 4, fontSize - 1), FontStyle.Regular)

        SetControlFont(Me.RequestNumberTextBox, bigFont)
        SetControlFont(Me.RequestLabel, labelFont)
        Me.RequestNumberPanel.Height = Me.RequestNumberTextBox.Height

        SetControlFont(Me.ClientSiteNameTextBox, bigFont)
        SetControlFont(Me.ClientLabel, labelFont)
        Me.ClientNamePanel.Height = Me.ClientSiteNameTextBox.Height

        SetControlFont(Me.RequestDescription, mainFont)
        SetControlFont(Me.JobDetails, mainFont)
        SetControlFont(Me.Equipment, mainFont)
        SetControlFont(Me.JobNotes, mainFont)
    End Sub

#If WINDOWSMOBILE Then
#Region "Input Panel Support"
    Private Sub EnableInputPanelOnFormLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InputPanelSwitch(True)
    End Sub

    Private Sub DisposeInputPanelOnFormDisposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        Try
            Me.InputPanel.Dispose()
            Me.InputPanel = Nothing
        Catch ex As Exception

        End Try
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        If Me.Owner IsNot Nothing Then
            InputPanelSwitch(False)
        End If
    End Sub

    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        Const METHODNAME As String = "InputPanelSwitch"
        Try
            If loadInitial Then
                InputPanel.Enabled = AppConfig.GetSetting(MODULENAME & My.Resources.InputPanelEnabledRegValueName, False)
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
            Else
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                AppConfig.SaveSetting(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.InputPanel.Enabled)
            End If
            Me.JobNotes.Height = (Me.ClientSize.Height - Me.InputPanelPanel.Height) \ 2 - Me.ConsultantNotesLabel.Height
            'Application.DoEvents()
            'Me.Refresh()
        Catch ex As ObjectDisposedException
            ' who cares
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Me.Form_Resize(Me, New System.EventArgs())
        End Try

    End Sub

#End Region

#Region "Window State Support"

    Private Sub ViewOrientation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewRotate0.Click, ViewRotate90.Click, ViewRotate180.Click, ViewRotate270.Click
        If sender Is Me.ViewRotate0 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle0
        ElseIf sender Is Me.ViewRotate90 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90
        ElseIf sender Is Me.ViewRotate180 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle180
        ElseIf sender Is Me.ViewRotate270 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle270
        End If
        FixOrientationMenu()
    End Sub

    Private Sub FixOrientationMenu()
        Me.ViewRotate0.Checked = False
        Me.ViewRotate90.Checked = False
        Me.ViewRotate180.Checked = False
        Me.ViewRotate270.Checked = False
        Select Case SystemSettings.ScreenOrientation
            Case ScreenOrientation.Angle0
                Me.ViewRotate0.Checked = True
            Case ScreenOrientation.Angle90
                Me.ViewRotate90.Checked = True
            Case ScreenOrientation.Angle180
                Me.ViewRotate180.Checked = True
            Case ScreenOrientation.Angle270
                Me.ViewRotate270.Checked = True
        End Select
    End Sub



    Private Sub Landscape()
        Me.NamePanel.BringToFront()
        Me.SignaturePanel.BringToFront()
        Me.JobCompleteLabel.Dock = DockStyle.Left
        Me.JobCompleteLabel.Width = Me.ClientSize.Width \ 2
        Me.RequestStatusPanel.Dock = DockStyle.Fill
        Me.ConsultantNotesLabel.Dock = DockStyle.Left
        Me.ConsultantNotesLabel.Width = Me.SignatoryLabel.Width
        Me.EquipmentLabel.Dock = DockStyle.Left
        Me.EquipmentLabel.Width = Me.SignatoryLabel.Width
        Me.GpsLabel.Visible = False
        Me.JobNumberLabel.Visible = False
    End Sub

    Private Sub Square()
        Me.NamePanel.BringToFront()
        Me.SignaturePanel.BringToFront()
        Me.JobCompleteLabel.Dock = DockStyle.Top
        'Me.JobCompleteLabel.Height = Me.NamePanel.Height
        Me.RequestStatusPanel.Dock = DockStyle.Top
        Me.ConsultantNotesLabel.Dock = DockStyle.Top
        Me.ConsultantNotesLabel.Height = Me.SignatoryLabel.Height
        Me.EquipmentLabel.Dock = DockStyle.Top
        Me.EquipmentLabel.Height = Me.SignatoryLabel.Height
        'Me.JobCompleteLabel.Visible = False ' not really enough space on a square screen.
        Me.GpsLabel.Visible = False
        Me.JobNumberLabel.Visible = False
    End Sub

    Private Sub Portrait()
        Me.NamePanel.SendToBack()
        Me.SignaturePanel.SendToBack()
        Me.JobCompleteLabel.Dock = DockStyle.Top
        'Me.JobCompleteLabel.Height = Me.NamePanel.Height
        Me.RequestStatusPanel.Dock = DockStyle.Top
        Me.ConsultantNotesLabel.Dock = DockStyle.Top
        Me.ConsultantNotesLabel.Height = Me.SignatoryLabel.Height
        Me.EquipmentLabel.Dock = DockStyle.Top
        Me.EquipmentLabel.Height = Me.SignatoryLabel.Height
        Me.GpsLabel.Visible = True
        Me.JobNumberLabel.Visible = True
    End Sub

    Private Sub Form_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Try
            Me.SuspendLayout()
            Dim screenSize As Rectangle = Screen.PrimaryScreen.Bounds
            If screenSize.Width > screenSize.Height Then ' landscape
                Landscape()
            ElseIf screenSize.Width = screenSize.Height Then ' square
                Square()
            Else   'portrait
                Portrait()
            End If
            Me.ConsultantNotesPanel.Height = Me.NotesTab.ClientSize.Height \ 2
            Me.ActivityTimePanel.Height = Me.ActivityFinishDatePicker.Height

            Me.ActivityShortCutPanel.Height = Me.ActivityShortCutComboBox.Height
            For Each cfp As CustomFormPanel In Me.CustomFormPanels
                cfp.ArrangeItems()
            Next

        Catch ex As Exception
            Const METHODNAME As String = "Form_Resize"
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Me.ResumeLayout()
            Me.Refresh()
        End Try
    End Sub
#End Region


#End If

    Private Sub RequestStatusPicker_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestStatusPicker.SelectedIndexChanged

    End Sub
End Class