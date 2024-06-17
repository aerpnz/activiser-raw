Imports activiser.Terminology
Imports activiser.Library.Forms
Imports Microsoft.WindowsCE.Forms
Imports activiser.Library
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.WebService.FormDefinition

<System.Runtime.InteropServices.ComVisible(False)> Public Class RequestForm
    Private Const MODULENAME As String = "RequestForm"
    Private Const STR_Request As String = "Request"

    Private _localRequest As Boolean
    Private _newRequest As Boolean
    Private _dirty As Boolean
    Private _inRefresh As Boolean

    Private _Request As RequestRow
    Private _selectedJob As JobRow

    Public ReadOnly Property Dirty() As Boolean
        Get
            If _dirty Then Return True
            If _Request.HasChanges Then
                Return True
            End If
            For Each cfp As CustomFormPanel In CustomFormPanels
                If cfp.HasChanges Then
                    Return True
                End If
            Next
            Return False
        End Get
    End Property

    Public Sub New(ByVal owner As Form)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Owner = owner
        Me.SetWindowState()

        'Add any initialization after the InitializeComponent() call
        If Not gAllowNewRequests Then
            DisplayMessage(Me, MODULENAME, RES_NewRequestsNotAllowed, MessageBoxIcon.Asterisk)
            Me.DialogResult = Windows.Forms.DialogResult.Ignore
            Me.Close()
            Return
        End If

        Dim nr As RequestRow
        Terminology.LoadLabels(Me)

        nr = gClientDataSet.Request.NewRequestRow
        nr.RequestID = nr.ConsultantRID
        nr.RequestUID = Guid.NewGuid
        nr.CreatedDateTime = DateTime.UtcNow()
        nr.NextActionDate = nr.CreatedDateTime.Date

        Me._newRequest = True 'Establish that the request is new to prevent FieldNotes from causing nullreferenceexception
        nr.RequestNumber = nr.RequestID.ToString(WithCulture)

        'Me.ClientListButton.Enabled = False 'Disable shortcut to client list form for new request because the client has not been selected yet.
        Me.LoadCustomForms()
        Me.PopulateClientList()
        Me.Request = nr
        Me.Owner = owner
        'Me.mboolChanged = False
    End Sub

    Public Sub New(ByVal owner As Form, ByVal request As RequestRow, ByVal tab As RequestFormTab)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Owner = owner
        Me.SetWindowState()

        'Add any initialization after the InitializeComponent() call
        Terminology.LoadLabels(Me)
        Me.LoadCustomForms()
        Dim jobs() As JobRow = request.GetJobRows
        Me._newRequest = request.IsClientSiteUIDNull OrElse (request.RequestID < 0 AndAlso jobs IsNot Nothing AndAlso jobs.Length = 0)
        Me.Request = request
        Me.GetJobHistoryButton.Enabled = True
        If tab = RequestFormTab.Jobs Then Me.TabControl.SelectedIndex = Me.TabControl.TabPages.IndexOf(Me.JobTab)
        Me.ClientListComboBox.Visible = False
        Me.ClientSiteName.Visible = True
        Me.ClientInfoButton.BackColor = SystemColors.Control
        Me.Owner = owner
    End Sub

    Public Sub New(ByVal owner As Form, ByVal client As ClientSiteRow)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Owner = owner
        Me.SetWindowState()

        'Add any initialization after the InitializeComponent() call
        If Not gAllowNewRequests Then
            DisplayMessage(Me, MODULENAME, RES_NewRequestsNotAllowed, MessageBoxIcon.Asterisk)
            Me.DialogResult = Windows.Forms.DialogResult.Ignore
            Me.Close()
            Return
        End If

        Terminology.LoadLabels(Me)
        Dim nr As RequestRow
        nr = gClientDataSet.Request.NewRequestRow

        nr.RequestID = nr.ConsultantRID
        nr.ClientSiteRow = client
        nr.RequestUID = Guid.NewGuid
        nr.CreatedDateTime = Now()
        nr.NextActionDate = nr.CreatedDateTime.Date
        Me._newRequest = True
        Me.LoadCustomForms()
        Me.ClientListComboBox.Visible = False
        Me.ClientSiteName.Visible = True
        Me.ClientInfoButton.BackColor = SystemColors.Control
        Me.Request = nr
        Me.Owner = owner
    End Sub

    Function SaveRequest() As Boolean
        Const METHODNAME As String = "SaveRequest"
        Try
            Try
                _Request.EndEdit()
            Catch ex As InRowChangingEventException
                Return False
            End Try

            If Me.ClientListComboBox.SelectedIndex <> -1 Then ' ClientListComboBox.DataSource IsNot Nothing AndAlso Me.ClientListComboBox.SelectedIndex <> -1 Then
                If _SelectedClient Is Nothing Then
                    Dim listItem As ClientListItem = TryCast(Me.ClientListComboBox.SelectedItem, ClientListItem)
                    If listItem IsNot Nothing Then
                        _SelectedClient = gClientDataSet.ClientSite.FindByClientSiteUID(listItem.ClientSiteUID)
                    End If
                End If

                If _Request.IsClientSiteUIDNull OrElse _Request.ClientSiteUID <> _SelectedClient.ClientSiteUID Then
                    _Request.ClientSiteRow = _SelectedClient
                    ' _Request.ClientSiteUID = _SelectedClient.ClientSiteUID
                End If
            End If

            If _Request.IsClientSiteUIDNull Then
                Terminology.DisplayMessage(Me, MODULENAME, RES_ClientSelectionRequired, MessageBoxIcon.Asterisk)
                Return False
            End If

            If _Request.IsContactNull OrElse _Request.Contact <> Me.Contact.Text Then _Request.Contact = Me.Contact.Text
            If _Request.IsShortDescriptionNull OrElse _Request.ShortDescription <> Me.ShortDescription.Text Then _Request.ShortDescription = Me.ShortDescription.Text
            If _Request.IsLongDescriptionNull OrElse _Request.LongDescription <> Me.LongDescription.Text Then _Request.LongDescription = Me.LongDescription.Text

            If Me._localRequest Then
                If _Request.IsNextActionDateNull OrElse _Request.NextActionDate <> Me.RequestDate.Value.Date Then _Request.NextActionDate = Me.RequestDate.Value.Date
            End If

            If Me._newRequest Then
                If _Request.IsAssignedToUIDNull OrElse (_Request.AssignedToUID <> gConsultantUID) Then _Request.AssignedToUID = gConsultantUID
                If _Request.IsCreatedDateTimeNull Then _Request.CreatedDateTime = DateTime.UtcNow
                If _Request.IsRequestStatusIDNull Then _Request.RequestStatusID = RequestStatusCodes.[New]
                If _Request.IsConsultantStatusIDNull Then _Request.ConsultantStatusID = RequestStatusCodes.[New]
                If (_Request.RowState And (DataRowState.Added Or DataRowState.Detached)) <> 0 Then
                    _Request.ModifiedDateTime = DateTime.UtcNow
                End If
                _Request.ModifiedDateTime = DateTime.UtcNow
                ' If DataRowHasChanges(_Request) Then _Request.ModifiedDateTime = DateTime.UtcNow
                If Not gClientDataSet.Request.Rows.Contains(_Request.RequestUID) Then
                    gClientDataSet.Request.AddRequestRow(_Request)
                    Try
                        ConsultantConfig.AddItem(STR_Request, _Request.RequestUID, _Request.ModifiedDateTime)
                    Catch ex As ArgumentException
                        ' already in table
                    Catch ex As ConstraintException
                        Terminology.DisplayMessage(Me, MODULENAME, RES_ConstraintExceptionMessage, MessageBoxIcon.Exclamation)
                    Catch ex As NoNullAllowedException
                        Terminology.DisplayMessage(Me, MODULENAME, RES_NoNullAllowedMessage, MessageBoxIcon.Exclamation)
                    End Try
                End If
            End If

            SaveCustomFormData()
            SavePending(gClientDataSet, gMainDbFileName)

            _dirty = False

            Return True
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, RES_SaveError)
        End Try
    End Function

    Private Sub RefreshMe()
        Me.Activate()
        Me.BringToFront()
        _inRefresh = True

        'Set request ID in request form
        Me.RequestNumber.Text = _
            If(Not _Request.IsRequestNumberNull, _
                _Request.RequestNumber, _
                If(Not _Request.IsRequestIDNull, _Request.RequestID.ToString(WithCulture), Terminology.GetString(MODULENAME, RES_NoRequestNumber)))

        If Not _Request.IsNextActionDateNull Then
            Me.RequestDate.Value = _Request.NextActionDate.ToLocalTime
        Else
            Me.RequestDate.Text = String.Empty
        End If

        Me.Contact.Text = If(Not _Request.IsContactNull, _Request.Contact, String.Empty)
        Me.OwnerTextBox.Text = If(Not _Request.IsAssignedToNameNull, _Request.AssignedToName, String.Empty)
        Me.ShortDescription.Text = If(Not _Request.IsShortDescriptionNull, _Request.ShortDescription, String.Empty)
        Me.LongDescription.Text = If(Not _Request.IsLongDescriptionNull, _Request.LongDescription, String.Empty)
        Me.ClientSiteName.Text = If(Not _Request.ClientSiteRow Is Nothing, _Request.ClientSiteRow.SiteName, String.Empty)

        ShowClientInfo(_Request.ClientSiteRow)

        Me._localRequest = If(Not _Request.IsConsultantRIDNull, _Request.RequestID <= 0, False)

        Me.Contact.ReadOnly = Not Me._localRequest
        Me.RequestDate.Enabled = Me._localRequest

        Me.ShortDescription.ReadOnly = Not Me._localRequest
        Me.LongDescription.ReadOnly = Not Me._localRequest

        Dim tab As TabPage = TabControl.TabPages(TabControl.SelectedIndex)

        If tab Is Me.JobTab Then
            PopulateJobList()
        ElseIf tab Is Me.HistoryTab Then
            PopulateJobHistoryList()
        End If

        SetCustomFormParentFilters()

        Dim lallowNewJobs As Boolean = NewJobsAllowed(Me._Request)
        Me.NewJobButton.Enabled = lallowNewJobs
        Me.MainMenuNewJob.Enabled = lallowNewJobs

        Me.MainMenuNewRequest.Enabled = gAllowNewRequests AndAlso (NewRequestsAllowed(Me._Request.ClientSiteRow))

        _inRefresh = False
    End Sub

    Private Sub PopulateClientList()
        Const METHODNAME As String = "PopulateClientList"
        Try
            Dim clientList As New Generic.SortedList(Of String, ClientListItem)
            For Each csr As ClientSiteRow In gClientDataSet.ClientSite
                If csr.ClientSiteStatusRow.IsActive AndAlso Not csr.IsSiteNameNull AndAlso Not String.IsNullOrEmpty(csr.SiteName) Then
                    If gAllowRequestsForClientsOnHold OrElse Not csr.Hold Then
                        clientList.Add(csr.SiteName, New ClientListItem(csr.ClientSiteUID, csr.SiteName))
                    End If
                End If
            Next

            Me.ClientListComboBox.SuspendLayout()
            Me.ClientListComboBox.Items.Clear()
            For Each cli As ClientListItem In clientList.Values
                Me.ClientListComboBox.Items.Add(cli)
            Next

            Me.ClientListComboBox.Visible = True
            Me.ClientSiteName.Visible = False
            Me.ClientListComboBox.SelectedIndex = -1
            Me.ClientListComboBox.ResumeLayout()
            Me.ClientListComboBox.Focus()

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, RES_ClientListLoadError)
        End Try
    End Sub

    Private Sub PopulateJobList()
        Const METHODNAME As String = "PopulateJobList"
        Me.JobList.Items.Clear()
        Dim jrs() As JobRow = Me._Request.GetJobRows()

        Dim listViewItem As ListViewItem
        'Dim jc As JobRow = moCurrentJob

        Dim jis(jrs.GetUpperBound(0)) As Date
        Dim j As JobRow
        For i As Integer = 0 To jrs.GetUpperBound(0)
            If Not jrs(i).IsStartTimeNull Then
                jis(i) = jrs(i).StartTime.ToLocalTime
            ElseIf Not jrs(i).IsJobDateNull Then
                jis(i) = jrs(i).JobDate
            Else
                jis(i) = DateTime.UtcNow
            End If
        Next
        Array.Sort(jis, jrs, 0, jis.Length, System.Collections.Comparer.Default)
        Array.Reverse(jrs, 0, jrs.Length)

        Try
            For Each j In jrs
                Dim jobDate As String = String.Empty
                Dim jobStart As String = String.Empty
                Dim jobFinish As String = String.Empty
                Dim jobDetails As String = Terminology.GetString(MODULENAME, RES_NoDetails)

                If Not j.IsStartTimeNull Then
                    jobDate = FormatDate(j.StartTime)
                    jobStart = j.StartTime.ToLocalTime.ToShortTimeString()
                End If
                If Not j.IsFinishTimeNull Then jobFinish = j.FinishTime.ToLocalTime.ToShortTimeString()
                If Not j.IsJobDetailsNull AndAlso Not String.IsNullOrEmpty(j.JobDetails) Then jobDetails = j.JobDetails

                listViewItem = New ListViewItem(New String() {jobDate, jobStart, jobFinish, jobDetails})
                'Dim bm As Bitmap

                If j.ConsultantUID.Equals(gConsultantUID) Then
                    'listViewItem.ImageIndex = j.JobStatusID
                    listViewItem.ForeColor = GetStatusColor(j.JobStatusID)
                Else
                    listViewItem.ForeColor = Color.DarkRed
                    'listViewItem.ImageIndex = 5 'JobStatusColours.Brick
                End If
                listViewItem.Tag = j
                Me.JobList.Items.Add(listViewItem)
            Next
            If Not _selectedJob Is Nothing Then
                For Each listViewItem In Me.JobList.Items
                    If listViewItem.Tag Is _selectedJob Then
                        listViewItem.Selected = True
                        Exit For
                    End If
                Next
            Else
                Me.JobInformation.Text = String.Empty
            End If
        Catch ex As NullReferenceException
            LogError(MODULENAME, METHODNAME, ex, False, RES_ClientMissing)
        End Try
    End Sub

    Private Sub PopulateJobHistoryList()
        Const METHODNAME As String = "PopulateHistoryList"
        Me.JobHistoryList.Items.Clear()
        Dim jrs() As JobRow
        Try
            jrs = Me._Request.ClientSiteRow.GetJobRows
        Catch ex As Exception
            Exit Sub
        End Try

        Dim listViewItem As ListViewItem

        Dim jis(jrs.GetUpperBound(0)) As Date
        Dim j As JobRow
        For i As Integer = 0 To jrs.GetUpperBound(0)
            If Not jrs(i).IsStartTimeNull Then
                jis(i) = jrs(i).StartTime
            Else
                jis(i) = Today
            End If
        Next

        Try
            Array.Sort(jis, jrs, 0, jis.Length, System.Collections.Comparer.Default)
            Array.Reverse(jrs, 0, jrs.Length)
            For Each j In jrs
                If Not j.RequestRow Is _Request Then '.RequestUID.Equals(moRequest.RequestUID) Then
                    Dim jobDate As String = String.Empty
                    Dim jobDetails As String = Terminology.GetString(MODULENAME, RES_NoDetails)

                    If Not j.IsStartTimeNull Then jobDate = FormatDate(j.StartTime)
                    If Not j.IsJobDetailsNull AndAlso Not String.IsNullOrEmpty(j.JobDetails) Then jobDetails = j.JobDetails

                    listViewItem = New ListViewItem(New String() {jobDate, j.ConsultantRow.Name, jobDetails})
                    listViewItem.Tag = j
                    Me.JobHistoryList.Items.Add(listViewItem)
                End If
            Next
            If Not _selectedJob Is Nothing Then
                For Each listViewItem In Me.JobList.Items
                    If listViewItem.Tag Is _selectedJob Then
                        listViewItem.Selected = True
                        Exit For
                    End If
                Next
            Else
                Me.JobHistoryInformation.Text = String.Empty
            End If
        Catch ex As NullReferenceException
            LogError(MODULENAME, METHODNAME, ex, False, RES_ClientMissing)
        End Try
    End Sub


    Public Property Request() As RequestRow
        Get
            Return _Request
        End Get
        Set(ByVal Value As RequestRow)
            _Request = Value
            RefreshMe()
            Me._dirty = False
        End Set
    End Property

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Const METHODNAME As String = "Form_Load"
        Try
            'If _newRequest Then
            '    'Error log records show that this method has been causing null reference exception
            '    'Have added andalso condition to test that cmbClientlist is an valid object before calling visible method
            '    If Me.ClientListComboBox.Visible Then
            '        Me.ClientListComboBox.Focus()
            '    Else
            '        Me.Contact.Focus()
            '    End If
            'End If

            PopulateRequestMenu()

            LoadImage()


            Me.RequestDate.CustomFormat = gDateFormat

            Me.MainMenuNewRequest.Enabled = gAllowNewRequests

#If Not WINDOWSMOBILE Then
            EnableContextMenus(Me.Controls)
#End If

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub GeneralTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Contact.TextChanged, ShortDescription.TextChanged, LongDescription.TextChanged, RequestDate.TextChanged
        If Not _inRefresh Then
            _dirty = True
        End If
    End Sub

    Private Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuSave.Click
        SaveRequest()
        'WaitForSavePendingCompletion(gClientDataSet)
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Const METHODNAME As String = "Form_Closing"
        Try
            If gAllowNewRequests AndAlso _dirty Then '(OrElse cfRequest.Dirty) Then
                If Me.DialogResult = Windows.Forms.DialogResult.OK Then
                    If Not SaveRequest() Then ' will fail if the user hasn't supplied a client on a new request!
                        e.Cancel = True
                    End If
                Else
                    Select Case AskQuestion(Me, MODULENAME, RES_ConfirmSave, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1)
                        Case Windows.Forms.DialogResult.Yes
                            SaveRequest()
                        Case Windows.Forms.DialogResult.No
                            ' do nothing
                        Case Windows.Forms.DialogResult.Cancel
                            e.Cancel = True
                    End Select

                End If
            Else
                If Not SaveCustomFormData() Then
                    e.Cancel = True ' the save attempt should have popped up a message
                End If
                SavePending(gClientDataSet, gMainDbFileName)
            End If

            'If operation is cancelled, do not remove the client list
            'If Not gcmbClientList Is Nothing And Not e.Cancel Then
            '    Me.RequestHeaderPanel.Controls.Remove(gcmbClientList)
            'End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            'If Not e.Cancel AndAlso Me.Owner IsNot Nothing Then
            '    Me.Owner.Show()
            'End If
        End Try
    End Sub

    Private Sub tcRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.SelectedIndexChanged
        Dim tab As TabPage = TabControl.TabPages(TabControl.SelectedIndex)
        If tab Is Me.JobTab Then
            PopulateJobList()
        ElseIf tab Is Me.HistoryTab Then
            PopulateJobHistoryList()
        ElseIf tab Is Me.DescriptionTab Then
            If Me.ClientListComboBox.Visible Then
                Me.ClientListComboBox.Focus()
            Else
                Me.RequestNumber.Focus()
            End If
        End If
    End Sub

    Private Sub lvJobList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobList.SelectedIndexChanged
        Dim listViewItem As ListViewItem
        Try
            If JobList.SelectedIndices.Count = 1 Then
                listViewItem = JobList.Items(JobList.SelectedIndices.Item(0))
                If Not listViewItem Is Nothing Then
                    Me._selectedJob = TryCast(listViewItem.Tag, JobRow)
                    If Me._selectedJob IsNot Nothing Then
                        Dim details As String
                        If _selectedJob.IsJobDetailsNull Then
                            details = Terminology.GetString(MODULENAME, RES_NoDetails)
                        Else
                            details = _selectedJob.JobDetails
                        End If
                        Dim signatory As String = Terminology.GetString(MODULENAME, RES_NoSignatory)
                        If Not _selectedJob.IsSignatoryNull Then
                            signatory = _selectedJob.Signatory
                        End If
                        Dim email As String = Terminology.GetString(MODULENAME, RES_NoEmail)
                        If Not _selectedJob.IsEmailNull Then
                            email = _selectedJob.Email
                        End If
                        Me.JobHistoryInformation.Text = Terminology.GetFormattedString(MODULENAME, _
                            RES_JobHistoryInformation, _
                            _selectedJob.ConsultantRow.Name, details)
                        Me.JobInformation.Text = Terminology.GetFormattedString(MODULENAME, _
                            RES_JobInformation, _
                            _selectedJob.JobStatusRow.Description, _
                            _selectedJob.ConsultantRow.Name, _
                            details, signatory, email _
                            )

                        If Me._selectedJob.JobStatusID = JobStatusCodes.StatusChange Then
                            Me.OpenJobButton.Enabled = False
                            Me.OpenJobHistoryButton.Enabled = False
                        Else
                            Me.OpenJobButton.Enabled = True
                            Me.OpenJobHistoryButton.Enabled = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Me.JobInformation.Text = Terminology.GetString(MODULENAME, RES_JobDetailsMissing)
        End Try
    End Sub

    Private Sub lvHistory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobHistoryList.SelectedIndexChanged
        Dim listViewItem As ListViewItem
        Try
            If JobHistoryList.SelectedIndices.Count = 1 Then
                listViewItem = JobHistoryList.Items(JobHistoryList.SelectedIndices.Item(0))
                If Not listViewItem Is Nothing Then
                    Me._selectedJob = CType(listViewItem.Tag, JobRow)
                    Me.JobHistoryInformation.Text = Me._selectedJob.JobDetails
                    If Me._selectedJob.JobStatusID = JobStatusCodes.StatusChange Then
                        Me.OpenJobButton.Enabled = False
                        Me.OpenJobHistoryButton.Enabled = False
                    Else
                        Me.OpenJobButton.Enabled = True
                        Me.OpenJobHistoryButton.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            Me.JobInformation.Text = Terminology.GetString(MODULENAME, RES_JobDetailsMissing)
        End Try
    End Sub

    Private Sub NewJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewJobButton.Click, JobContextMenuNew.Click, MainMenuNewJob.Click
        Debug.WriteLine("MainMenuNewJob Clicked")
        OpenNewJob()
    End Sub

    Private _SelectedClient As ClientSiteRow
    Private Sub OpenNewJob()
        Const METHODNAME As String = "OpenNewJob"
        Try
            If _Request Is Nothing Then Return

            If Not (Me._newRequest OrElse Me._localRequest) Then
                If Not AllowNewJob(Me, _Request) Then
                    Return
                End If
            End If

            If Not SaveRequest() Then ' will popup message if key information missing
                Return
            End If

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Using f As New JobForm(Me, Me._Request)
                System.Windows.Forms.Cursor.Current = Cursors.Default
                f.ShowDialog()
            End Using
            RefreshMe()
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, Nothing)
        End Try
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Sub ClientListComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientListComboBox.SelectedIndexChanged
        Const METHODNAME As String = "ClientListComboBox_SelectedIndexChanged"
        Try
            If Me.ClientListComboBox.SelectedIndex = -1 Then
                _SelectedClient = Nothing
            Else
                _SelectedClient = gClientDataSet.ClientSite.FindByClientSiteUID(CType(Me.ClientListComboBox.SelectedItem, ClientListItem).ClientSiteUID)
            End If
            Me.ShowClientInfo(_SelectedClient)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub OpenJob()
        Const METHODNAME As String = "OpenJob"
        Try
            If Not _selectedJob Is Nothing Then
                Try
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                    Using f As New JobForm(Me, Me._selectedJob)
                        System.Windows.Forms.Cursor.Current = Cursors.Default
                        f.ShowDialog()
                    End Using
                    Me.PopulateJobList()
                    RefreshMe()
                Catch ex As Exception
                    LogError(MODULENAME, METHODNAME, ex, True, Nothing)
                Finally
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                End Try
            Else
                DisplayMessage(Me, MODULENAME, RES_NoJobSelected, MessageBoxIcon.Asterisk)
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, Nothing)
        End Try
    End Sub
    Private Sub OpenJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuOpenJob.Click, OpenJobButton.Click, JobContextMenuOpen.Click, JobHistoryContextMenuOpen.Click, OpenJobHistoryButton.Click
        If (_selectedJob IsNot Nothing) AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeOpenJob, RES_OpenJobCancelled) Then
            OpenJob()
        End If
    End Sub
    Private Sub chkWordWrap_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WordWrapCheckBox.Click
        Me.LongDescription.WordWrap = Me.WordWrapCheckBox.Checked
    End Sub

    Private Sub Sync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuSync.Click
        Using sf As New SyncForm(Me)
            Synchronisation.StartManualSync()
            sf.ShowDialog()
        End Using
        Me.RefreshMe()
    End Sub

    Private Sub NewRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuNewRequest.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Using f As New RequestForm(Me, _Request.ClientSiteRow)
            System.Windows.Forms.Cursor.Current = Cursors.Default
            f.ShowDialog()
        End Using
        Me.Show()
        Me.Refresh()
    End Sub

    Private Sub GetHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetJobHistoryButton.Click, MainMenuGetHistory.Click
        Const METHODNAME As String = "GetHistory_Click"
        Dim timerOn As Boolean = Synchronisation.SyncTimer.Enabled
        Try
            If Not _Request.ClientSiteRow Is Nothing Then
                Synchronisation.SyncTimer.Enabled = False
                Cursor.Current = Cursors.WaitCursor
                Synchronisation.SyncGetClientHistory(Me, _Request.ClientSiteRow)
                'Using fSync As New SyncForm(Me, _Request.ClientSiteRow, SyncType.ClientHistory)
                '    fSync.ShowDialog()
                'End Using
                PopulateJobHistoryList()
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Synchronisation.SyncTimer.Enabled = timerOn
            Cursor.Current = Cursors.Default
        End Try
    End Sub


#Region "Request Status Change"
    Dim WithEvents MenuItemRequest As System.Windows.Forms.MenuItem
    Dim RequestMenuItemArrayList As New ArrayList
    Private Structure RequestMenuItem
        Friend RequestStatusID As Integer
        Friend IsClientMenuItem As Boolean
        Friend IsReasonRequired As Boolean
        Friend objRequestMenuItem As MenuItem

        Sub New(ByVal dr As RequestStatusRow, ByVal objRequestMenuItem As MenuItem)
            RequestStatusID = dr.RequestStatusID
            Me.objRequestMenuItem = objRequestMenuItem

            IsClientMenuItem = dr.IsClientMenuItem
            IsReasonRequired = dr.IsReasonRequired
        End Sub
    End Structure

    Private Sub ChangeRequestStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Const METHODNAME As String = "ChangeRequestStatus_Click"
        Try
            If Not _Request Is Nothing Then
                For Each miRequest As RequestMenuItem In RequestMenuItemArrayList
                    If sender Is miRequest.objRequestMenuItem Then
                        ChangeRequestStatus(Me, _Request, miRequest.RequestStatusID, miRequest.IsReasonRequired)
                    End If
                Next
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Me.PopulateJobList()
            Me.PopulateJobHistoryList()
        End Try
    End Sub

    Private Sub PopulateRequestMenu()
        For Each dr As RequestStatusRow In gClientDataSet.RequestStatus
            If dr.IsClientMenuItem Then
                MenuItemRequest = New MenuItem
                AddHandler MenuItemRequest.Click, AddressOf ChangeRequestStatus_Click
                MenuItemRequest.Text = dr.Description
                MainMenuChangeStatus.MenuItems.Add(MenuItemRequest)

                Dim miRequest As New RequestMenuItem(dr, MenuItemRequest)
                RequestMenuItemArrayList.Add(miRequest)
            End If
        Next
    End Sub

#End Region

#Region "ICT Project"



    Private Sub Form_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.Width > Me.Height Then
            Me.JobButtonPanel.Dock = DockStyle.Right
            Me.JobButtonPanel.Width = Me.OpenJobButton.Left * 2 + Me.OpenJobButton.Width
            Me.HistoryButtonPanel.Dock = DockStyle.Right
            Me.HistoryButtonPanel.Width = Me.JobButtonPanel.Width
            Me.NewJobButton.Location = New Point(4, Me.OpenJobButton.Top * 2 + Me.OpenJobButton.Height)
            Me.GetJobHistoryButton.Location = Me.NewJobButton.Location
        Else
            Me.JobButtonPanel.Dock = DockStyle.Bottom
            Me.JobButtonPanel.Height = Me.OpenJobButton.Top * 2 + Me.OpenJobButton.Height
            Me.HistoryButtonPanel.Dock = DockStyle.Bottom
            Me.HistoryButtonPanel.Height = Me.JobButtonPanel.Height
            Me.NewJobButton.Location = New Point(Me.Width - (4 + Me.NewJobButton.Width), 4)
            Me.GetJobHistoryButton.Location = New Point(Me.Width - (4 + Me.GetJobHistoryButton.Width), 4)
        End If
        Me.OpenJobButton.Location = New Point(4, 4)
        Me.OpenJobHistoryButton.Location = New Point(4, 4)
        Me.ClientSiteName.Location = Me.ClientListComboBox.Location
        Me.ClientSiteName.Size = Me.ClientListComboBox.Size
    End Sub


    Public Sub LoadImage()
#If WINDOWSMOBILE Then
        If Windows.Forms.Screen.PrimaryScreen.Bounds.Width < 400 Then ' activiser.MyDpiX = 96.0! And activiser.MyDpiY = 96.0! Then
            ClientInfoButton.Image = My.Resources.Info16
        Else
            ClientInfoButton.Image = My.Resources.Info32
        End If
#Else
        ClientInfoButton.Image = My.Resources.Info16
#End If
    End Sub

#End Region


    Private Sub NextTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click
        If Me.TabControl.SelectedIndex < Me.TabControl.TabPages.Count - 1 Then
            Me.TabControl.SelectedIndex += 1
        Else
            Me.TabControl.SelectedIndex = 0
        End If
    End Sub

    Private Sub RequestMenuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub MainMenuCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "Custom Form Support"
    Private CustomFormPanels As New Generic.List(Of CustomFormPanel)
    Private Sub LoadCustomForms()
        Dim requestForms As New Generic.SortedList(Of Integer, FormRow)
        For Each cfr As FormRow In gFormDefinitions.Form
            If cfr.ParentEntityName = STR_Request Then
                requestForms.Add(cfr.Priority, cfr)
            End If
        Next

        If requestForms.Count = 0 Then Return

        For Each cfr As WebService.FormDefinition.FormRow In requestForms.Values.Reverse
            Dim tp As New TabPage
            tp.Text = cfr.FormLabel
            tp.Name = cfr.FormName
            Me.TabControl.TabPages.Insert(2, tp)

            Dim cfp As New CustomFormPanel(cfr, tp, Me)
            cfp.Dock = DockStyle.Fill
            tp.Controls.Add(cfp)
            CustomFormPanels.Add(cfp)
        Next
    End Sub

    Private Sub SetCustomFormParentFilters()
        For Each cfp As CustomFormPanel In CustomFormPanels
            Debug.WriteLine("Request Form setting custom form parent filter: " & Me._Request.RequestUID.ToString)
            cfp.SetParentFilter(Me._Request.RequestUID)
            cfp.ReadOnly = (Not Me._localRequest) AndAlso (cfp.CustomForm.LockCode = LockCodes.LockWithParent)
        Next
    End Sub

    Private Function SaveCustomFormData() As Boolean
        Dim result As Boolean = True
        For Each cfp As CustomFormPanel In CustomFormPanels
            Debug.WriteLine("Request Form saving custom form data: " & Me._Request.RequestUID.ToString)
            If Not cfp.Save() Then
                result = False
            End If
        Next
        Return result
    End Function
#End Region

    Private Sub RemoveJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveJobContextMenuItem.Click, JobHistoryContextMenuRemove.Click
        If Me._selectedJob IsNot Nothing Then
            Select Case Me._selectedJob.JobStatusID
                Case 0
                    If Terminology.AskQuestion(Me, MODULENAME, RES_DeleteDraftJob, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_selectedJob)
                    End If
                Case 1
                    If Terminology.AskQuestion(Me, MODULENAME, RES_DeleteCompletedJob, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_selectedJob)
                    End If
                Case 2
                    Terminology.DisplayMessage(Me, MODULENAME, RES_MayNotDeleteSignedJob, MessageBoxIcon.Asterisk)
                Case 3, 4
                    If Terminology.AskQuestion(Me, MODULENAME, RES_RemoveSyncronisedJob, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_selectedJob)
                    End If
                Case 5
                    If Terminology.AskQuestion(Me, MODULENAME, RES_RemoveHistory, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_selectedJob)
                    End If
                Case 6
                    RemoveJob(_selectedJob)
            End Select
            Me.PopulateJobHistoryList()
            Me.PopulateJobList()
            Me.BringToFront()
            Me.Activate()
        End If
    End Sub

    Private Sub MainMenuClearSyncedJobs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClearSyncedJobs.Click
        If Terminology.AskQuestion(Me, MODULENAME, RES_RemoveAllSyncedJobs, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            For Each jr As JobRow In Me._Request.GetJobRows
                If jr.JobStatusID > 2 Then
                    RemoveJob(jr)
                End If
            Next
            Me.PopulateJobList()
            Me.BringToFront()
            Me.Activate()
        End If
    End Sub

    Private Sub MainMenuClearHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuClearHistory.Click
        If Terminology.AskQuestion(Me, MODULENAME, RES_RemoveHistory, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            For Each jr As JobRow In Me._Request.ClientSiteRow.GetJobRows
                If jr.RequestRow Is Me._Request Then Continue For
                Dim rr As RequestRow = jr.RequestRow
                Dim rs As RequestStatusRow = rr.RequestStatusRowByFK_Request_RequestStatus
                If rs.IsCompleteStatus OrElse rs.IsCancelledStatus AndAlso jr.JobStatusID > 2 Then
                    RemoveJob(jr)
                End If
            Next
            For Each rr As RequestRow In Me._Request.ClientSiteRow.GetRequestRows
                If rr Is Me._Request Then Continue For
                Dim rs As RequestStatusRow = rr.RequestStatusRowByFK_Request_RequestStatus
                If rs.IsCompleteStatus OrElse rs.IsCancelledStatus AndAlso rr.GetJobRows.Length = 0 Then
                    RemoveRequest(Me, rr)
                End If
            Next
            Me.PopulateJobHistoryList()
            Me.BringToFront()
            Me.Activate()
        End If
    End Sub

    Private Sub Control_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RequestNumber.KeyDown, Contact.KeyDown, ClientSiteName.KeyDown, TabControl.KeyDown, ShortDescription.KeyDown
        Dim lSender As Control = DirectCast(sender, Control)
        If e.KeyCode = Keys.Up Then
            Me.SelectNextControl(lSender, False, True, True, True)
            e.Handled = True
        ElseIf e.KeyCode = Keys.Down Then
            Me.SelectNextControl(lSender, True, True, True, True)
            e.Handled = True
        End If
    End Sub

    'Private Sub JobHistoryList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles JobHistoryList.KeyDown
    '    If e.KeyCode = Keys.Enter AndAlso _selectedJob IsNot Nothing AndAlso (Me.JobList.Focused OrElse Me.JobHistoryList.Focused) Then
    '        Debug.WriteLine("Enter Key Depressed")
    '        'OpenJob()
    '    End If
    'End Sub

    'Private Sub JobHistoryList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles JobHistoryList.KeyPress

    'End Sub
    'Private Sub JobList_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles JobList.KeyUp, JobHistoryList.KeyUp
    '    'If e.KeyCode = Keys.Enter AndAlso _selectedJob IsNot Nothing AndAlso (Me.JobList.Focused OrElse Me.JobHistoryList.Focused) Then
    '    '    Debug.WriteLine("Enter Key Released")
    '    '    OpenJob()
    '    'End If
    'End Sub


    Private Sub WordWrapCheckBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WordWrapCheckBox.Click
        WordWrapCheckBox.Checked = Not WordWrapCheckBox.Checked
        Me.LongDescription.WordWrap = Me.WordWrapCheckBox.Checked
        Me.ShortDescription.WordWrap = Me.WordWrapCheckBox.Checked
        Me.ClientInfoBox.WordWrap = Me.WordWrapCheckBox.Checked
    End Sub

    Private Sub ShowClientInfo(ByVal client As ClientSiteRow)
        If client Is Nothing Then
            Me.ClientInfoBox.Text = String.Empty
        Else
            Try
                Dim lClientSiteName As String = client.SiteName

                Dim lClientSiteAddress As String
                If client.IsSiteAddressNull Then
                    lClientSiteAddress = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                Else
                    lClientSiteAddress = client.SiteAddress
                End If
                Dim lClientContact As String
                If client.IsContactNull Then
                    lClientContact = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                Else
                    lClientContact = client.Contact
                End If
                Dim lClientContactPhone1 As String
                If client.IsContactPhone1Null Then
                    lClientContactPhone1 = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                Else
                    lClientContactPhone1 = client.ContactPhone1
                End If
                Dim lClientContactPhone2 As String
                If client.IsContactPhone2Null Then
                    lClientContactPhone2 = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                Else
                    lClientContactPhone2 = client.ContactPhone2
                End If
                Dim lClientSiteContactEmail As String
                If client.IsSiteContactEmailNull Then
                    lClientSiteContactEmail = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                Else
                    lClientSiteContactEmail = client.SiteContactEmail
                End If
                Dim lClientSiteNotes As String
                If client.IsSiteNotesNull Then
                    lClientSiteNotes = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                Else
                    lClientSiteNotes = client.SiteNotes
                End If

                Me.ClientInfoBox.Text = Terminology.GetFormattedString(MODULENAME, RES_ClientTemplate, _
                    lClientSiteName, _
                    lClientSiteAddress, _
                    lClientContact, _
                    lClientContactPhone1, _
                    lClientContactPhone2, _
                    lClientSiteContactEmail, _
                    lClientSiteNotes)
            Catch ex As Exception
                Me.ClientInfoBox.Text = String.Empty
            End Try
        End If
    End Sub

    Private Sub OpenClientInfo()
        If _newRequest Then
            If Me._SelectedClient IsNot Nothing Then
                Using cf As New ClientForm(Me, _SelectedClient, True)
                    cf.ShowDialog()
                End Using
            Else
                Terminology.DisplayMessage(Me, MODULENAME, "MissingClientInfo", MessageBoxIcon.Exclamation)
            End If
        Else
            If _Request.ClientSiteRow IsNot Nothing Then
                Using cf As New ClientForm(Me, _Request.ClientSiteRow, True)
                    cf.ShowDialog()
                End Using
            Else
                Terminology.DisplayMessage(Me, MODULENAME, "MissingClientInfo", MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub MainMenuClientInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClientInfo.Click
        OpenClientInfo()
    End Sub

    Private Sub ClientInfoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientInfoButton.Click
        OpenClientInfo()
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

        Dim mainFont As Font = New Font(Me.Font.Name, ConfigurationSettings.GetValue(My.Resources.AppConfigTextSizeKey, 8), FontStyle.Regular)
        Dim detailFont As New Font(Me.Font.Name, ConfigurationSettings.GetValue(My.Resources.AppConfigTextSizeKey, 8) - 1, FontStyle.Regular)

        SetControlFont(Me.ShortDescription, mainFont)
        SetControlFont(Me.LongDescription, mainFont)
        SetControlFont(Me.JobInformation, mainFont)
        SetControlFont(Me.JobHistoryInformation, mainFont)
        SetControlFont(Me.ClientInfoBox, mainFont)
        SetControlFont(Me.JobList, mainFont)
        SetControlFont(Me.JobHistoryList, mainFont)
    End Sub

#If WINDOWSMOBILE Then
#Region "Input Panel Support"

    Private Sub EnableInputPanelOnFormLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InputPanelSwitch(True)
        EnableToolbars(ConfigurationSettings.GetValue(My.Resources.AppConfigRequestShowToolBarsKey, Me.Height > Me.Width))
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        Me.InputPanel.Dispose()
    End Sub

    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        If Me.Owner IsNot Nothing Then
            InputPanelSwitch(False)
        End If
    End Sub

    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        Const METHODNAME As String = "InputPanelSwitch"
        Try
            If loadInitial Then
                InputPanel.Enabled = ConfigurationSettings.GetValue(MODULENAME & My.Resources.InputPanelEnabledRegValueName, False)
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
            Else
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                ConfigurationSettings.SetValue(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.InputPanel.Enabled)
            End If

            'Application.DoEvents()
            Me.Refresh()
        Catch ex As ObjectDisposedException
            ' who cares
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, String.Empty)
        End Try
    End Sub
#End Region

#Region "Toolbar Support"

    Private Sub EnableToolbars(ByVal state As Boolean)
        Me.ShowToolBars.Checked = state
        Me.JobButtonPanel.Visible = state
        Me.HistoryButtonPanel.Visible = state
        ConfigurationSettings.SetValue(My.Resources.AppConfigRequestShowToolBarsKey, state)
    End Sub

    Private Sub ShowToolbars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolBars.Click
        EnableToolbars(Not Me.ShowToolBars.Checked)
    End Sub
#End Region

#Region "Window State Support"

    Private Sub ViewFullScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFullScreen.Click
        SetFormState(Me, Me.ViewFullScreen, Not Me.ViewFullScreen.Checked)
    End Sub

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
#End Region
#End If
End Class
