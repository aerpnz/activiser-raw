Imports Microsoft.WindowsCE.Forms
Imports activiser.Terminology
Imports activiser.Library
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.WebService.FormDefinition

Public Class ClientForm
    Private Const MODULENAME As String = "ClientForm"
    Private Const STR_ClientSite As String = "ClientSite"

    Private _clientList As Generic.SortedList(Of String, SelectorItem(Of Guid))
    Private allowNewForms As Boolean = True

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
        Dim mainFont As Font = New Font(Me.Font.Name, fontSize, FontStyle.Regular)
        Dim labelFont As Font = New Font(Me.Font.Name, fontSize, FontStyle.Bold)
        Dim detailFont As New Font(Me.Font.Name, If(fontSize < 5, 4, fontSize - 1), FontStyle.Regular)

        SetControlFont(Me.ClientListComboBox, mainFont)
        'SetControlFont(Me.ClientSiteName, mainFont)
        SetControlFont(Me.Address, mainFont)
        SetControlFont(Me.Contact, mainFont)
        SetControlFont(Me.Phone1, mainFont)
        SetControlFont(Me.Phone2, mainFont)
        SetControlFont(Me.PhoneLabel, labelFont)
        Me.PhonePanel.Height = Me.Phone1.Height + (Me.Phone1.Top * 2)
    End Sub

    Public Sub New(ByVal owner As Form)
        MyBase.new()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Owner = owner
        Me.SetWindowState()

        Me.PopulateClientList()
        Me.LoadCustomForms()
        Terminology.LoadLabels(Me)
    End Sub

    Private _SelectedClient As ClientSiteRow
    Private _SelectedRequest As RequestRow
    Private _SelectedJob As JobRow

    Public Property ClientSite() As ClientSiteRow
        Get
            Return _SelectedClient
        End Get
        Set(ByVal Value As ClientSiteRow)
            _SelectedClient = Value
            PopulateForm()
        End Set
    End Property

    '
    Private Sub LocateSelectedClientInPicker(ByVal clientSite As ClientSiteRow)
        Me.ClientListComboBox.SelectedIndex = -1
        For i As Integer = 0 To Me.ClientListComboBox.Items.Count - 1
            Dim cli As SelectorItem(Of Guid) = TryCast(Me.ClientListComboBox.Items(i), SelectorItem(Of Guid))
            If cli.Value = clientSite.ClientSiteUID Then
                Me.ClientListComboBox.SelectedIndex = i
                Return
            End If
        Next
    End Sub

    Public Sub New(ByVal owner As Form, ByVal clientSite As ClientSiteRow, ByVal disableNewForms As Boolean)
        'MyBase.new()
        Me.New(owner)

        LocateSelectedClientInPicker(clientSite)
        'Me.ClientListComboBox.Enabled = False

        'Me.InitializeComponent()
        'Me.Owner = owner
        'Me.SetWindowState()

        'Me.ClientSiteName.Visible = True
        'Me.ClientSiteName.Text = clientSite.SiteName
        'LoadCustomForms()

        ''NB: Load Custom Forms before setting the client site!
        'Me.ClientSite = clientSite
        'Terminology.LoadLabels(Me)

        allowNewForms = Not disableNewForms
        Me.NewJobButton.Enabled = allowNewForms
        Me.NewRequestButton.Enabled = allowNewForms
        Me.OpenJobButton.Enabled = allowNewForms
        Me.OpenRequestButton.Enabled = allowNewForms
        Me.MainMenuNewJob.Enabled = allowNewForms
        Me.MainMenuOpenJob.Enabled = allowNewForms
        Me.MainMenuNewRequest.Enabled = allowNewForms
        Me.MainMenuOpenRequest.Enabled = allowNewForms
    End Sub

    Private Sub PopulateForm()
        'If Me.ClientSiteName IsNot Nothing AndAlso Me.ClientSiteName.Visible Then
        '    ClientSiteName.Text = String.Empty
        'End If

        If _SelectedClient Is Nothing Then
            Me.NewRequestButton.Enabled = False
            Me.MainMenuNewRequest.Enabled = False
            Me.NewJobButton.Enabled = False
            Me.MainMenuNewJob.Enabled = False
            Me.OpenRequestButton.Enabled = False
            Me.OpenJobButton.Enabled = False
            Me.MainMenuOpenJob.Enabled = False
            Return
        End If

        Dim allowNewRequests As Boolean = allowNewForms AndAlso gAllowNewRequests AndAlso (NewRequestsAllowed(_SelectedClient))
        Me.NewRequestButton.Enabled = allowNewRequests
        Me.MainMenuNewRequest.Enabled = allowNewRequests

        Try
            'If Me.ClientSiteName IsNot Nothing AndAlso Me.ClientSiteName.Visible Then
            '    ClientSiteName.Text = _SelectedClient.SiteName
            'End If

            Address.Text = If(Not _SelectedClient.IsSiteAddressNull, _SelectedClient.SiteAddress, String.Empty)
            Contact.Text = If(Not _SelectedClient.IsContactNull, _SelectedClient.Contact, String.Empty)
            Phone1.Text = If(Not _SelectedClient.IsContactPhone1Null, _SelectedClient.ContactPhone1, String.Empty)
            Phone2.Text = If(Not _SelectedClient.IsContactPhone2Null, _SelectedClient.ContactPhone2, String.Empty)
            SiteNotes.Text = If(Not _SelectedClient.IsSiteNotesNull, _SelectedClient.SiteNotes, String.Empty)
            ClientNumberBox.Text = If(Not _SelectedClient.IsClientSiteNumberNull, _SelectedClient.ClientSiteNumber, String.Empty)
            Email.Text = If(Not _SelectedClient.IsSiteContactEmailNull, _SelectedClient.SiteContactEmail, String.Empty)

            Me.GeneralTab.BackColor = If(IsClientActive(_SelectedClient), System.Drawing.SystemColors.Window, System.Drawing.SystemColors.Control)

            Me.SetCustomFormParents()

            If Me.TabControl.SelectedIndex = Me.TabControl.TabPages.IndexOf(Me.JobTab) Then
                Me.PopulateJobList()
            ElseIf Me.TabControl.SelectedIndex = Me.TabControl.TabPages.IndexOf(Me.RequestTab) Then
                Me.PopulateRequestList()
            End If
        Catch ex As Exception
            LogError(MODULENAME, "PopulateForm", ex, False, Nothing)
        End Try
    End Sub

    Private Sub PopulateClientList()
        Try
            _clientList = New Generic.SortedList(Of String, SelectorItem(Of Guid))()
            For Each csr As ClientSiteRow In gClientDataSet.ClientSite
                If csr.ClientSiteStatusRow.IsActive AndAlso Not csr.IsSiteNameNull AndAlso Not String.IsNullOrEmpty(csr.SiteName) Then
                    _clientList.Add(csr.SiteName, New SelectorItem(Of Guid)(csr.ClientSiteUID, csr.SiteName))
                End If
            Next

            Me.ClientListComboBox.SuspendLayout()
            Me.ClientListComboBox.Items.Clear()
            For Each cli As SelectorItem(Of Guid) In _clientList.Values
                Me.ClientListComboBox.Items.Add(cli)
            Next

            Me.ClientListComboBox.Visible = True
            'Me.ClientSiteName.Visible = False
            Me.ClientListComboBox.SelectedIndex = -1
            Me.ClientListComboBox.ResumeLayout()
            Me.ClientListComboBox.Focus()
        Catch ex As Exception
            LogError(MODULENAME, "PopulateClientList", ex, True, RES_ClientListLoadError)
        End Try
    End Sub

    Private Sub PopulateJobList()
        Try
            If _SelectedClient Is Nothing Then
                Exit Sub
            End If
            Dim listViewItem As ListViewItem
            Me.JobList.Items.Clear()
            For Each j As JobRow In Me._SelectedClient.GetJobRows
                Dim requestNumber As String = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                If j.RequestRow IsNot Nothing Then
                    If Not j.RequestRow.IsRequestNumberNull AndAlso Not String.IsNullOrEmpty(j.RequestRow.RequestNumber) Then
                        requestNumber = j.RequestRow.RequestNumber
                    ElseIf Not j.RequestRow.IsRequestIDNull Then
                        requestNumber = j.RequestRow.RequestID.ToString(WithoutCulture)
                    End If
                End If
                listViewItem = New ListViewItem(requestNumber)
                If Not j.IsStartTimeNull Then
                    listViewItem.SubItems.Add(FormatDate(j.StartTime))
                Else
                    listViewItem.SubItems.Add(String.Empty)
                End If
                If Not j.ConsultantRow Is Nothing Then
                    listViewItem.SubItems.Add(j.ConsultantRow.Name)
                Else
                    listViewItem.SubItems.Add(Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown))
                End If
                If j.ConsultantUID.Equals(gConsultantUID) Then
                    listViewItem.ForeColor = GetStatusColor(j.JobStatusID)
                Else
                    listViewItem.ForeColor = GetStatusColor(JobStatusCodes.OtherUser)
                End If
                listViewItem.Tag = j
                Me.JobList.Items.Add(listViewItem)
            Next

            _SelectedJob = Nothing
            Me.JobDetailsTextBox.Text = String.Empty
        Catch ex As Exception
            LogError(MODULENAME, "PopulateJobList", ex, False, Nothing)
        End Try

    End Sub

    Private Sub PopulateRequestList()
        Try
            If _SelectedClient Is Nothing Then
                Exit Sub
            End If
            Dim listViewItem As ListViewItem
            Me.RequestList.Items.Clear()
            For Each r As RequestRow In _SelectedClient.GetRequestRows
                Dim requestNumber As String
                If r.IsRequestNumberNull Then
                    If r.IsRequestIDNull Then
                        requestNumber = Terminology.GetString(MODULENAME, RES_NoRequestNumber)
                    Else
                        requestNumber = CStr(r.RequestID)
                    End If
                Else
                    requestNumber = r.RequestNumber
                End If

                listViewItem = New ListViewItem(requestNumber)

                If r.IsNextActionDateNull Then
                    listViewItem.SubItems.Add(Terminology.GetString(MODULENAME, RES_NoRequestDate))
                Else
                    listViewItem.SubItems.Add(FormatDate(r.NextActionDate))
                End If
                If r.IsShortDescriptionNull Then
                    If r.IsLongDescriptionNull Then
                        listViewItem.SubItems.Add(Terminology.GetString(MODULENAME, RES_NoDescription))
                    Else
                        listViewItem.SubItems.Add(r.LongDescription)
                    End If
                Else
                    listViewItem.SubItems.Add(r.ShortDescription)
                End If
                listViewItem.Tag = r
                Me.RequestList.Items.Add(listViewItem)
            Next

            _SelectedRequest = Nothing
            Me.RequestDetails.Text = String.Empty
        Catch ex As Exception
            LogError(MODULENAME, "PopulateRequestList", ex, False, Nothing)
        End Try

    End Sub

#Region "Request Status"
    Dim RequestMenuItemList As New Collections.Generic.List(Of RequestStatusMenuItem)

    Private Sub PopulateRequestMenu()
        For Each dr As RequestStatusRow In gClientDataSet.RequestStatus
            If dr.IsClientMenuItem Then
                Dim MenuItemRequest As New MenuItem
                AddHandler MenuItemRequest.Click, AddressOf RequestListMenuChangeStatus_ItemClick
                MenuItemRequest.Text = dr.Description

                RequestMenuStatus.MenuItems.Add(MenuItemRequest)

                Dim miRequest As New RequestStatusMenuItem(dr, MenuItemRequest, Nothing, Nothing)
                RequestMenuItemList.Add(miRequest)
            End If
        Next
    End Sub
    Private Sub RequestListMenuChangeStatus_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Const METHODNAME As String = "RequestListMenuChangeStatus_ItemClick"
        Try
            If Me._SelectedRequest IsNot Nothing Then
                Dim newStatus As Integer
                Dim isReasonRequired As Boolean
                For Each miRequest As RequestStatusMenuItem In RequestMenuItemList
                    If sender Is miRequest.RequestMenuItem Then
                        newStatus = miRequest.RequestStatusID
                        isReasonRequired = miRequest.IsReasonRequired
                        Exit For
                    End If
                Next
                ChangeRequestStatus(Me, Me._SelectedRequest, newStatus, isReasonRequired)
            Else
                DisplayMessage(Me, MODULENAME, RES_NoRequestSelected, MessageBoxIcon.Asterisk)
            End If

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
        Me.PopulateRequestList()
        Me.Activate()
    End Sub

#End Region


    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MainMenuNewRequest.Enabled = allowNewForms AndAlso gAllowNewRequests
            Me.NewRequestButton.Enabled = allowNewForms AndAlso gAllowNewRequests

        Catch ex As Exception
            LogError(MODULENAME, "Form_Load", ex, False, Nothing)
        End Try
#If WINDOWSCE Then
        EnableContextMenus(Me.Controls)
#End If
    End Sub

    Private Sub ClientListComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientListComboBox.SelectedIndexChanged
        Const METHODNAME As String = "ClientListComboBox_SelectedIndexChanged"
        Try
            If Me.ClientListComboBox.SelectedIndex = -1 Then
                _SelectedClient = Nothing
            Else
                _SelectedClient = gClientDataSet.ClientSite.FindByClientSiteUID(CType(Me.ClientListComboBox.SelectedItem, SelectorItem(Of Guid)).Value)
            End If
            PopulateForm()
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
    End Sub

    Private Sub Tab_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.SelectedIndexChanged
        Dim tab As TabPage = TabControl.TabPages(TabControl.SelectedIndex)
        Try
            If tab Is Me.RequestTab Then
                PopulateRequestList()
            ElseIf tab Is Me.JobTab Then
                PopulateJobList()
            End If

        Catch ex As NullReferenceException
            LogError(MODULENAME, "Tab_Changed", ex, False, Nothing)
        End Try
    End Sub

    Private Sub Request_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestList.SelectedIndexChanged
        Me.OpenRequestButton.Enabled = False
        Me.MainMenuOpenRequest.Enabled = False
        Try
            Dim listViewItem As ListViewItem
            Try
                If RequestList.SelectedIndices.Count = 1 Then
                    listViewItem = RequestList.Items(RequestList.SelectedIndices.Item(0))

                    If Not listViewItem Is Nothing Then
                        Me._SelectedRequest = CType(listViewItem.Tag, RequestRow)
                        Dim statusDescription As String
                        If _SelectedRequest.IsConsultantStatusIDNull OrElse (_SelectedRequest.ConsultantStatusID = 0 AndAlso _SelectedRequest.RequestStatusID <> 0) Then
                            statusDescription = mapStatus(_SelectedRequest.RequestStatusID)
                        Else
                            statusDescription = mapStatus(_SelectedRequest.ConsultantStatusID)
                        End If
                        Dim assignedTo As String = If(_SelectedRequest.IsAssignedToUIDNull, Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unassigned), _SelectedRequest.ConsultantRow.Name)

                        Me.RequestDetails.Text = Terminology.GetFormattedString(MODULENAME, RES_RequestDescriptionTemplate, statusDescription, _SelectedRequest.ShortDescription, assignedTo)
                        Me.OpenRequestButton.Enabled = allowNewForms
                        Me.MainMenuOpenRequest.Enabled = allowNewForms
                        Dim lallowNewJobs As Boolean = allowNewForms AndAlso NewJobsAllowed(Me._SelectedRequest)
                        Me.NewJobButton.Enabled = lallowNewJobs
                        Me.MainMenuNewJob.Enabled = lallowNewJobs
                        Me.RequestMenuNewJob.Enabled = lallowNewJobs
                    End If
                End If
            Catch ex As Exception
                Me.RequestDetails.Text = Terminology.GetString(MODULENAME, RES_NoDetails)
            End Try
        Catch ex As Exception
            LogError(MODULENAME, "Request_Changed", ex, False, Nothing)
        End Try

    End Sub

    Private Sub Job_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobList.SelectedIndexChanged
        Dim listViewItem As ListViewItem
        Me.OpenJobButton.Enabled = False
        Me.MainMenuOpenJob.Enabled = False
        Try
            If JobList.SelectedIndices.Count = 1 Then
                listViewItem = JobList.Items(JobList.SelectedIndices.Item(0))
                If Not listViewItem Is Nothing Then
                    Me._SelectedJob = CType(listViewItem.Tag, JobRow)
                    Me.JobDetailsTextBox.Text = Me._SelectedJob.JobDetails
                    Me.OpenJobButton.Enabled = allowNewForms
                    Me.MainMenuOpenJob.Enabled = allowNewForms
                End If
            End If
        Catch ex As Exception
            Me.JobDetailsTextBox.Text = Terminology.GetString(MODULENAME, RES_NoDetails)
        End Try
    End Sub

    Private Sub OpenRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenRequestButton.Click, RequestOpen.Click, MainMenuOpenRequest.Click
        If _SelectedRequest IsNot Nothing AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeOpenRequest, RES_OpenRequestCancelled) Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Using f As activiser.RequestForm = New RequestForm(Me, _SelectedRequest, RequestFormTab.Main)
                System.Windows.Forms.Cursor.Current = Cursors.Default
                f.ShowDialog()
            End Using
            Me.PopulateRequestList()
        End If
    End Sub

    Private Sub NewJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewJobButton.Click, MainMenuNewJob.Click
        If Not AllowNewJob(Me, _SelectedRequest) Then
            Return
        End If

        If _SelectedRequest IsNot Nothing AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeCreateJob, RES_CreateJobCancelled) Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Using f As activiser.JobForm = New JobForm(Me, Me._SelectedRequest)
                System.Windows.Forms.Cursor.Current = Cursors.Default
                f.ShowDialog()
            End Using
            Me.PopulateJobList()
        End If
    End Sub

    Private Sub OpenJob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenJobButton.Click, JobMenuOpen.Click, MainMenuOpenJob.Click
        If Not _SelectedJob Is Nothing AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeOpenJob, RES_OpenJobCancelled) Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Using f As activiser.JobForm = New JobForm(Me, Me._SelectedJob)
                System.Windows.Forms.Cursor.Current = Cursors.Default
                f.ShowDialog()
            End Using
            Me.PopulateJobList()
        End If
    End Sub

    Private Sub NewRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuNewRequest.Click, NewRequestButton.Click
        If Not allowNewForms Then Return

        If Not _SelectedClient Is Nothing Then
            If AllowNewRequest(Me, _SelectedClient) Then
                If gAllowRequestsForClientsOnHold Then
                    If AskQuestion(Me, MODULENAME, RES_ClientOnHoldMessage, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                        Return
                    End If
                Else
                    DisplayMessage(Me, MODULENAME, RES_ClientOnHoldBlockedMessage, MessageBoxIcon.Hand)
                    Return
                End If
            End If

            If Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeCreateRequest, RES_CreateRequestCancelled) Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Using f As activiser.RequestForm = New RequestForm(Me, _SelectedClient)
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                    f.ShowDialog()
                End Using
                Me.PopulateRequestList()
                Me.PopulateJobList()
            End If
        End If
    End Sub

    Private Sub GetHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GetHistoryButton.Click, MainMenuGetJobHistory.Click
        Const METHODNAME As String = "GetHistory_Click"
        Dim timerOn As Boolean = Synchronisation.SyncTimer.Enabled
        Try
            If Not _SelectedClient Is Nothing Then
                Synchronisation.SyncTimer.Enabled = False
                Cursor.Current = Cursors.WaitCursor
                Synchronisation.SyncGetClientHistory(Me, _SelectedClient)
                'Using fSync As New SyncForm(Me, _SelectedClient, SyncType.ClientHistory)
                '    fSync.ShowDialog()
                'End Using
                Me.PopulateJobList()
            End If
            TabControl.SelectedIndex = TabControl.TabPages.IndexOf(Me.JobTab)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Synchronisation.SyncTimer.Enabled = timerOn
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub GetClientDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetailsButton.Click, MainMenuGetClientDetails.Click
        Const METHODNAME As String = "GetClientDetails_Click"
        Dim timerOn As Boolean = Synchronisation.SyncTimer.Enabled
        Try

            If Not _SelectedClient Is Nothing Then
                Synchronisation.SyncTimer.Enabled = False
                Cursor.Current = Cursors.WaitCursor
                Synchronisation.SyncGetClientDetails(Me, _SelectedClient, False)
                'Using fSync As New SyncForm(Me, _SelectedClient, If(Me.GetHistoryCheckBox.Checked, SyncType.ClientDetailAndHistory, SyncType.ClientDetail))
                '    fSync.ShowDialog()
                'End Using
                PopulateForm()
                'If GetHistoryCheckBox.Checked Then
                '    Me.PopulateJobList()
                'End If
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Synchronisation.SyncTimer.Enabled = timerOn
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub GetOpenRequests_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuGetOpenRequests.Click
        Const METHODNAME As String = "GetOpenRequests_Click"
        Dim timerOn As Boolean = Synchronisation.SyncTimer.Enabled
        Try
            If Not _SelectedClient Is Nothing Then
                Synchronisation.SyncTimer.Enabled = False
                Cursor.Current = Cursors.WaitCursor
                Synchronisation.SyncGetClientRequests(Me, _SelectedClient)
                'Using fSync As New SyncForm(Me, _SelectedClient, SyncType.ClientOpenRequests)
                '    fSync.ShowDialog()
                'End Using
                Me.PopulateJobList()
            End If
            TabControl.SelectedIndex = TabControl.TabPages.IndexOf(Me.RequestTab)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Synchronisation.SyncTimer.Enabled = timerOn
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub ClientMenuRequest_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim enableItems As Boolean = Me._SelectedRequest IsNot Nothing

        Me.MainMenuNewJob.Enabled = enableItems
        Me.MainMenuOpenRequest.Enabled = enableItems
    End Sub

    Private Sub ClientMenuJob_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim enableItems As Boolean = Me._SelectedJob IsNot Nothing

        Me.MainMenuOpenJob.Enabled = enableItems
        Me.MainMenuRemoveJob.Enabled = enableItems
    End Sub

    Private Sub Next_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click
        If Me.TabControl.SelectedIndex = Me.TabControl.TabPages.Count - 1 Then
            Me.TabControl.SelectedIndex = 0
        Else
            Me.TabControl.SelectedIndex += 1
        End If
    End Sub

    Private Sub RemoveJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobMenuRemove.Click
        If Me._SelectedJob IsNot Nothing Then
            RemoveJob(Me._SelectedJob)
            Me._SelectedJob = Nothing
            Me.PopulateJobList()
        End If
    End Sub

    Private Sub JobMenu_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobMenu.Popup
        Dim enableItems As Boolean = Me._SelectedJob IsNot Nothing

        Me.JobMenuOpen.Enabled = enableItems
        Me.JobMenuRemove.Enabled = enableItems
    End Sub

    Private Sub RemoveRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestMenuRemove.Click, MenuRequestRemove.Click
        If Me._SelectedRequest IsNot Nothing Then
            RemoveRequest(Me, Me._SelectedRequest)
            Me._SelectedRequest = Nothing
            PopulateRequestList()
        End If
    End Sub

    Private Sub ClientMenu_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenu.Popup
        Dim enableJobItems As Boolean = Me._SelectedJob IsNot Nothing
        Dim enableRequestItems As Boolean = Me._SelectedRequest IsNot Nothing
        Me.MainMenuNewJob.Enabled = enableRequestItems
        Me.MainMenuOpenJob.Enabled = enableJobItems
        Me.MainMenuOpenRequest.Enabled = enableRequestItems
        Me.MainMenuRemoveJob.Enabled = enableJobItems
    End Sub


#If WINDOWSMOBILE Then
#Region "Phone Support"
    Private Sub CheckPhoneCapability(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not CanPhone() Then
            Me.MainMenu.MenuItems.Remove(Me.MainMenuCall)
        End If
    End Sub


    Private Sub MainMenuCallPhone1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuCallPhone1.Click
        MakePhoneCall(Me.Phone1)
    End Sub

    Private Sub MainMenuCallPhone2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuCallPhone2.Click
        MakePhoneCall(Me.Phone2)
    End Sub

    Private Sub MainMenuCallPhone1_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuCallPhone1.Popup
        Me.MainMenuCallPhone1.Enabled = CanPhone(Me.Phone1)
    End Sub

    Private Sub MainMenuCallPhone2_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuCallPhone2.Popup
        Me.MainMenuCallPhone2.Enabled = CanPhone(Me.Phone2)
    End Sub

#End Region

#Region "Orientation Support"
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

#Region "Input Panel Support"
    Private Sub Form_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        InputPanelSwitch(True)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        Me.InputPanel.Dispose()
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
            'Application.DoEvents()
            Me.Refresh()
        Catch ex As ObjectDisposedException
            ' who cares
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub
    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        InputPanelSwitch(False)
    End Sub
#End Region

#Region "Toolbar Support"
    Private Sub EnableToolbars(ByVal state As Boolean)
        Me.ShowToolbars.Checked = state
        AppConfig.SaveSetting(My.Resources.AppConfigClientFormShowToolBarsKey, state)
        Me.JobButtonPanel.Visible = state
        Me.OpenJobButton.Visible = state AndAlso allowNewForms
        Me.DetailPanel2b.Visible = state
        Me.DetailPanel2.Height = If(state, Me.PhonePanel.Height + Me.DetailPanel2b.Height, Me.PhonePanel.Height)
        Me.RequestButtonPanel.Visible = state AndAlso allowNewForms
    End Sub

    Private Sub ShowToolBars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolbars.Click
        EnableToolbars(Not Me.ShowToolbars.Checked)
    End Sub

    Private Sub EnableToolbarsOnFormLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EnableToolbars(AppConfig.GetSetting(My.Resources.AppConfigClientFormShowToolBarsKey, Me.Height > Me.Width))
    End Sub
#End Region

#Region "WindowsState Support"

    Private Sub ViewFullScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFullScreen.Click
        SetFormState(Me, Me.ViewFullScreen, Not Me.ViewFullScreen.Checked)
    End Sub

#End Region
#End If


#Region "Custom Form Support"
    Private CustomFormPanels As New Generic.List(Of CustomFormPanel)
    Private Sub LoadCustomForms()
        Dim clientForms As New Generic.SortedList(Of Integer, FormRow)
        For Each cfr As FormRow In gFormDefinitions.Form.Rows '.Select(Nothing, gFormDefinitions.Form.PriorityColumn.ColumnName)
            If cfr.ParentEntityName = STR_ClientSite Then
                clientForms.Add(cfr.Priority, cfr)
            End If
        Next

        If clientForms.Count = 0 Then Return

        For Each cfr As WebService.FormDefinition.FormRow In clientForms.Values
            'For Each cfr As WebService.FormDefinition.FormRow In gFormDefinitions.Form.Select("ParentEntity='ClientSite'", gFormDefinitions.Form.PriorityColumn.ColumnName)
            Dim tp As New TabPage
            tp.Text = cfr.FormLabel
            tp.Name = cfr.FormName
            Me.TabControl.TabPages.Add(tp)

            Dim cfp As New CustomFormPanel(cfr, tp, Me)
            cfp.Dock = DockStyle.Fill
            tp.Controls.Add(cfp)
            CustomFormPanels.Add(cfp)
        Next
    End Sub

    Private Sub SetCustomFormParents()
        For Each cfp As CustomFormPanel In CustomFormPanels
            Debug.WriteLine("Client Form setting custom form parent filter: " & Me._SelectedClient.ClientSiteUID.ToString)
            cfp.SetParentFilter(Me._SelectedClient.ClientSiteUID)
        Next
    End Sub

    'Private Sub SaveCustomFormData()
    '    For Each cfp As CustomFormPanel In CustomFormPanels
    '        Debug.WriteLine("Client Form saving custom form data: " & Me._SelectedClient.ClientSiteUID.ToString)
    '        cfp.Save()
    '    Next
    'End Sub
#End Region

    Private Sub MainMenuRemoveCompletedRequests_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuRemoveCompletedRequests.Click
        Dim clientRequests() As RequestRow = Me._SelectedClient.GetRequestRows
        If clientRequests Is Nothing Then Return
        For Each requestRow As RequestRow In clientRequests
            Dim requestStatus As RequestStatusRow = requestRow.RequestStatusRowByFK_Request_RequestStatus
            If requestStatus.IsCancelledStatus OrElse requestStatus.IsCompleteStatus Then
                Dim requestJobs() As JobRow = requestRow.GetJobRows
                Dim haveUnSyncedJobs As Boolean
                If requestJobs IsNot Nothing OrElse requestJobs.Length <> 0 Then
                    For Each jobRow As JobRow In requestJobs
                        If jobRow.JobStatusID < 3 Then
                            haveUnSyncedJobs = True
                        Else
                            RemoveJob(jobRow)
                        End If
                    Next
                End If
                If Not haveUnSyncedJobs Then
                    RemoveRequest(Me, requestRow)
                End If
            End If
        Next
        Me.PopulateRequestList()
        Me.PopulateJobList()
    End Sub

    Private Sub MainMenuRemoveJob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenuRemoveJob.Click
        If Me._SelectedJob IsNot Nothing Then
            Select Case Me._SelectedJob.JobStatusID
                Case 0
                    If Terminology.AskQuestion(Me, MODULENAME, "DeleteDraftJob", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_SelectedJob)
                    End If
                Case 1
                    If Terminology.AskQuestion(Me, MODULENAME, "DeleteCompletedJob", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_SelectedJob)
                    End If
                Case 2
                    Terminology.DisplayMessage(Me, MODULENAME, "MayNotDeleteSignedJob", MessageBoxIcon.Asterisk)
                Case 3, 4
                    If Terminology.AskQuestion(Me, MODULENAME, "RemoveSyncronisedJob", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_SelectedJob)
                    End If
                Case 5
                    If Terminology.AskQuestion(Me, MODULENAME, "RemoveHistory", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        RemoveJob(_SelectedJob)
                    End If
                Case 6
                    'If Terminology.AskQuestion(me,MODULENAME, "RemoveHistory", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    RemoveJob(_SelectedJob)
                    'End If

            End Select
            Me.PopulateJobList()
        End If
    End Sub

    Private Sub MainMenuClearSyncedJobs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClearSyncedJobs.Click
        If Terminology.AskQuestion(Me, MODULENAME, RES_RemoveAllSyncedJobs, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            For Each jr As JobRow In Me._SelectedClient.GetJobRows
                If jr.JobStatusID > 2 Then
                    RemoveJob(jr)
                End If
            Next
            Me.PopulateJobList()
            Me.BringToFront()
            Me.Activate()
        End If
    End Sub

    Private Sub Form_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If Not allowNewForms Then Return
        If (e.KeyCode = System.Windows.Forms.Keys.Enter) Then
            If Me.RequestList.Focused AndAlso (_SelectedRequest IsNot Nothing) AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeOpenRequest, RES_OpenRequestCancelled) Then
                'activiser.PlaySound()
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Using f As activiser.RequestForm = New RequestForm(Me, _SelectedRequest, RequestFormTab.Main)
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                    f.ShowDialog()
                End Using
            ElseIf Me.JobList.Focused AndAlso (_SelectedJob IsNot Nothing) AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeOpenJob, RES_OpenJobCancelled) Then
                'activiser.PlaySound()
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Using f As activiser.JobForm = New JobForm(Me, Me._SelectedJob)
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                    f.ShowDialog()
                End Using
            End If
            'ElseIf e.KeyCode = Keys.Up Then
            '    Dim startFrom As Control = GetActiveControl(Me)
            '    If startFrom Is Nothing Then Return
            '    Me.SelectNextControl(startFrom, True, True, True, True)
            '    e.Handled = True
            'ElseIf e.KeyCode = Keys.Down Then
            '    Dim startFrom As Control = GetActiveControl(Me)
            '    If startFrom Is Nothing Then Return
            '    Me.SelectNextControl(startFrom, False, True, True, True)
            '    e.Handled = True
        End If
    End Sub

    Private Sub Control_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SiteNotes.KeyUp, Phone2.KeyUp, Phone1.KeyUp, GetDetailsButton.KeyUp, Email.KeyUp, Contact.KeyUp, Address.KeyUp
        Dim lSender As Control = DirectCast(sender, Control)
        If e.KeyCode = Keys.Up Then
            Me.SelectNextControl(lSender, False, True, True, True)
        ElseIf e.KeyCode = Keys.Down Then
            Me.SelectNextControl(lSender, True, True, True, True)
        End If
    End Sub

    Private Sub MenuDownloadDetailWithHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDownloadDetailWithHistory.Click
        Const METHODNAME As String = "GetClientDetails_Click"
        Dim timerOn As Boolean = Synchronisation.SyncTimer.Enabled
        Try

            If Not _SelectedClient Is Nothing Then
                Synchronisation.SyncTimer.Enabled = False
                Cursor.Current = Cursors.WaitCursor
                Synchronisation.SyncGetClientDetails(Me, _SelectedClient, True)
                PopulateForm()
                Me.PopulateJobList()
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Synchronisation.SyncTimer.Enabled = timerOn
            Cursor.Current = Cursors.Default
        End Try

    End Sub
End Class
