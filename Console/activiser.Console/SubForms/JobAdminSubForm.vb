Imports System.ComponentModel
Imports activiser.Library.activiserWebService

Public Class JobAdminSubForm
    Const MODULENAME As String = "JobAdminSubForm"
    Const STR_StatusBarRequestNumber As String = "StatusBarRequestNumber"
    Const STR_DoNotHaveSignature As String = "DoNotHaveSignature"
    Const STR_HaveSignature As String = "HaveSignature"
    Const STR_RequestStatusID As String = "RequestStatusID"
    Const STR_Description As String = "Description"
    Const RES_NoRequestStatus As String = "NoRequestStatus"
    Const STR_JobUID As String = "JobUID"
    Const STR_NoClientInformation As String = "NoClientInformation"
    Private Const RES_FinishTimeBeforeStartTime As String = "FinishTimeBeforeStartTime"

    Private _currentJob As activiserDataSet.JobRow
    'Private _currentJobIndex As Integer
    Private _currentRequest As activiserDataSet.RequestRow
    Private haveInitialised As Boolean = False

    'Private _polling As Boolean
    Private _suspended As Boolean

    'Private modifiedCellStyle, normalCellStyle, modifiedTextCellStyle, normalTextCellStyle As DataGridViewCellStyle

    Private _giAddress As String
    Private _giNotes As String
    Private _giJobNumber As String
    Private _gi As activiser.Library.Gps.GpsPosition


    Private WithEvents _refreshTimer As System.Timers.Timer
    Private WithEvents _consoleData As New ConsoleData(MODULENAME)
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property ConsoleData() As ConsoleData
        Get
            Return _consoleData
        End Get
        Set(ByVal value As ConsoleData)
            _consoleData = value
        End Set
    End Property

    'Public Property Polling() As Boolean
    '    Get
    '        Return _polling
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _polling = value
    '    End Set
    'End Property

    Private requestList As New BusinessObjectCollection(MODULENAME & ":" & "requestList")
    Private consultantList As New BusinessObjectCollection(MODULENAME & ":" & "consultantList")
    Private assignToList As New BusinessObjectCollection(MODULENAME & ":" & "assignToList")
    Private clientList As New BusinessObjectCollection(MODULENAME & ":" & "clientList")
    Private requestStatusList As New CategoryObjectCollection(MODULENAME & ":" & "requestStatusList")
    Private consultantStatusList As New CategoryObjectCollection(MODULENAME & ":" & "consultantStatusList")

    'Private jobStatusList As New CategoryObjectCollection(MODULENAME & ":" & "jobStatusList")

    Public Sub ReloadStatusList()
        TraceVerbose(STR_Fired)
        CategoryObjectCollection.PopulateList(Me.ConsultantStatusIDComboBox, Me.consultantStatusList, "RequestStatus", STR_RequestStatusID, STR_Description, String.Empty, Nothing, My.Resources.None)
        CategoryObjectCollection.PopulateList(Me.RequestStatusIDComboBox, Me.requestStatusList, "RequestStatus", STR_RequestStatusID, STR_Description)
    End Sub

    Public Sub ReloadConsultantList()
        TraceVerbose(STR_Fired)
        BusinessObjectCollection.PopulateList(Me.RequestAssignedToComboBox, Me.assignToList, "Consultant", "ConsultantUid", "Name", String.Empty, Nothing, My.Resources.None)
        BusinessObjectCollection.PopulateList(Me.ConsultantUIDComboBox, Me.consultantList, "Consultant", "ConsultantUid", "Name")
    End Sub

    Public Sub ReloadClientList()
        TraceVerbose(STR_Fired)
        BusinessObjectCollection.PopulateList(Me.ClientSiteUIDComboBox, Me.clientList, "ClientSite", "ClientSiteUid", "SiteName", String.Empty, Nothing, My.Resources.NotAvailable)
    End Sub


    Private requestListFilter As String = "1=2"
    Public Sub ReloadRequestList(ByVal filter As String)
        TraceVerbose(STR_Fired)
        If filter IsNot Nothing Then
            If filter <> requestListFilter Then
                requestListFilter = filter
            Else
                Return
            End If
        End If
        Try
            'Me.JobBindingSource.SuspendBinding()
            Me.RequestUIDComboBox.SuspendLayout()
            Me.RequestUIDComboBox.DataBindings.Clear()
            BusinessObjectCollection.PopulateList(Me.RequestUIDComboBox, Me.requestList, "Request", "RequestUid", "RequestNumber", requestListFilter, Nothing, Nothing)
            Me.RequestUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.JobBindingSource, "RequestUID", True))
            Me.RequestUIDComboBox.ResumeLayout()
            'Me.JobBindingSource.ResumeBinding()
        Catch ex As Exception
            TraceError(ex)
        End Try
    End Sub
    Private Sub JobToolbarSaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobToolbarSaveButton.Click
        TraceVerbose(STR_Fired)
        Try
            Me.Validate()
            Me.JobBindingSource.EndEdit()
            Console.ConsoleData.SetEditorState(MODULENAME, False)
            Console.ConsoleData.Refresh()
            Me.SetCurrentJob()
        Catch ex As Exception
            TraceError(ex)
        End Try
    End Sub

    'Private Sub JobAdminSubForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
    '    My.Settings.Save()
    'End Sub

    'this is so that the initialisation can be driven manually from the main form load event, after the initial data set has loaded.
    Public Sub InitialiseForm()
        TraceVerbose(STR_Entered)

        If ConsoleUser IsNot Nothing Then
            haveInitialised = False
            Try

                Me.SuspendLayout()

                Terminology.LoadLabels(Me)
                TraceVerbose(MODULENAME & ": Labels loaded")
                Terminology.LoadToolTips(Me, ToolTipProvider)
                TraceVerbose(MODULENAME & ": ToolTips loaded")

                JobFlagGroup.AdminEnabled = ConsoleUser.Administration
                JobFlagGroup.ManagementEnabled = ConsoleUser.Management

                TraceVerbose("Enabling Request Status Combo")
                Me.RequestStatusIDComboBox.Enabled = ConsoleUser.Management OrElse ConsoleUser.Administration

                Me.CoreDataSet = Console.ConsoleData.CoreDataSet

                TraceVerbose("Re-binding datasources")
                'Me.LoadJobStatusList()
                Me.JobBindingSource.DataSource = Console.ConsoleData.CoreDataSet
                Me.JobBindingSource.DataMember = Console.ConsoleData.CoreDataSet.Job.TableName

                Me.RequestBindingSource.DataSource = Console.ConsoleData.CoreDataSet
                Me.RequestBindingSource.DataMember = Console.ConsoleData.CoreDataSet.Request.TableName

                TraceVerbose("Loading combo box lists")
                Me.ReloadStatusList()
                Me.ReloadClientList()
                Me.ReloadConsultantList()
                Me.ReloadRequestList(String.Empty)

                GpsSupport.LoadTemplate(GpsBrowser)

                Try
                    Utilities.RestoreColumnWidths(My.Settings.JobAdminGridColumnWidths, Me.JobDataGridView)
                Catch 'ex As Exception

                End Try
                Try
                    Utilities.RestoreColumnOrder(My.Settings.JobAdminGridColumnOrder, Me.JobDataGridView)
                Catch 'ex As Exception

                End Try
            Catch ex As Exception

            Finally
                Me.ResumeLayout()
                haveInitialised = True
            End Try
        End If
        TraceVerbose(STR_Done)
    End Sub

    Private Sub JobAdminSubForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TraceVerbose(STR_Entered)



        _refreshTimer = New System.Timers.Timer(500)
        _refreshTimer.AutoReset = False
        _refreshTimer.SynchronizingObject = Me

        Dim template As String = GpsSupport.GetDeviceTrackingHtml()

        If Not String.IsNullOrEmpty(template) Then
            Me.GpsBrowser.DocumentText = template
        End If

        TraceVerbose(STR_Done)
    End Sub

    Private CustomFormPanels As New List(Of CustomFormPanel)
    'TODO: Linq this.
    Public Sub LoadCustomForms()
        For Each cfr As FormDefinition.FormRow In Console.ConsoleData.FormDefs.Form
            If cfr.ParentEntityName.ToUpperInvariant = "JOB" Then
                Dim tp As New TabPage(cfr.FormName)
                Dim cfp As New CustomFormPanel(cfr)
                If cfp IsNot Nothing Then
                    Try
                        cfp.Dock = DockStyle.Fill
                        tp.Controls.Add(cfp)
                        tp.Tag = cfp
                        CustomFormPanels.Add(cfp)
                        Me.JobInfoTabControl.TabPages.Add(tp)
                    Catch

                    End Try
                End If
            End If
        Next
    End Sub

    Private Delegate Sub DisplayFlagStatesCallBack(ByVal Flag As Integer)

    Private Sub DisplayFlagStates(ByVal Flag As Integer)
        If JobFlagGroup.InvokeRequired Then
            Dim d As New DisplayFlagStatesCallBack(AddressOf DisplayFlagStates)
            Me.Invoke(d, New Object() {Flag})
        Else
            JobFlagGroup.Value = Flag
        End If
    End Sub

    Private Delegate Sub SetDateTimePickerValueCallBack(ByVal Control As DateTimePicker, ByVal Value As DateTime)

    Private Sub SetDateTimePickerValue(ByVal Control As DateTimePicker, ByVal Value As DateTime)
        If Control.InvokeRequired Then
            Dim d As New SetDateTimePickerValueCallBack(AddressOf SetDateTimePickerValue)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            Control.Value = Value
        End If
    End Sub

    Private Delegate Sub SetDateTimePickerCheckedCallBack(ByVal Control As DateTimePicker, ByVal Value As Boolean)
    Private Sub SetDateTimePickerChecked(ByVal Control As DateTimePicker, ByVal Value As Boolean)
        If Control.InvokeRequired Then
            Dim d As New SetDateTimePickerCheckedCallBack(AddressOf SetDateTimePickerChecked)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            Control.Checked = Value
        End If
    End Sub


    Private _dirty As Boolean
    Private Sub JobBindingSource_BindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles JobBindingSource.BindingComplete
        If Not haveInitialised Then Return
        If ConsoleUser Is Nothing Then Return
        If _suspended Then Return
        If Me._settingFilter Then Return

        If Not e.BindingCompleteState = BindingCompleteState.Success Then Return


        If e.BindingCompleteContext = BindingCompleteContext.DataSourceUpdate Then
            TraceInfo(STR_Entered)

            _dirty = False

            If Me._currentJob IsNot Nothing Then
                Dim i As Integer = Me.JobBindingSource.Find(STR_JobUID, _currentJob.JobUID)
                If i <> -1 Then
                    If i <> Me.JobBindingSource.Position Then Me.JobBindingSource.Position = i
                Else
                    Me.JobBindingSource.Position = 0
                End If
            Else
                Me.SetCurrentJob()
                Console.ConsoleData.SetEditorState(MODULENAME, False)
            End If
            Return
        ElseIf e.BindingCompleteContext = BindingCompleteContext.ControlUpdate Then
            If Me.JobBindingSource.Position <> -1 Then
                Dim drv As DataRowView = TryCast(Me.JobBindingSource.Current, DataRowView)
                If drv IsNot Nothing Then
                    If drv.IsEdit AndAlso ThreadingDelegates.ControlFocused(Me, e.Binding.Control) Then
                        TraceVerbose("control binding complete, control = {0}, field = {1}", e.Binding.Control.Name, e.Binding.BindingMemberInfo.BindingField)
                        Console.ConsoleData.SetEditorState(MODULENAME, True)
                    Else
                        Console.ConsoleData.SetEditorState(MODULENAME, False)
                    End If
                    _dirty = drv.Row.RowState <> DataRowState.Unchanged

                    If _dirty Then
                        Console.ConsoleData.NeedRefresh()
                    End If
                    ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, _dirty)
                Else
                    ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, False)
                    Console.ConsoleData.SetEditorState(MODULENAME, False)
                End If
            Else
                ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, False)
                Console.ConsoleData.SetEditorState(MODULENAME, False)
            End If
        End If


    End Sub

    Private _inCurrentChanged As Boolean
    Private Delegate Sub JobBindingSource_CurrentChangedDelegate()

    Private Sub JobBindingSource_CurrentChangedCallback()
        _inCurrentChanged = True
        Try
            If Me._currentJob Is Nothing Then
                TraceVerbose("No current Job")
                Return
            End If

            Me.Validate()
            TraceVerbose("Validate OK")
            Dim drv As DataRowView = TryCast(Me.JobBindingSource.Current, DataRowView)
            If drv IsNot Nothing Then
                If drv.IsEdit Then
                    _dirty = drv.Row.RowState <> DataRowState.Unchanged AndAlso DataRowHasChanges(drv.Row)
                    If _dirty Then
                        ThreadingDelegates.BindingSourceEndEdit(Me, Me.JobBindingSource)
                        TraceVerbose("Edit Ended")
                    Else
                        ThreadingDelegates.BindingSourceCancelEdit(Me, Me.JobBindingSource)
                        TraceVerbose("Edit Cancelled")
                    End If
                End If
            End If

            Console.ConsoleData.SetEditorState(MODULENAME, False)
        Catch ex As NoNullAllowedException

        Catch ex As Exception
            TraceError(ex)
        Finally
            _inCurrentChanged = False
        End Try
    End Sub

    Private Sub JobBindingSource_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobBindingSource.CurrentChanged
        If ConsoleUser Is Nothing OrElse Not haveInitialised Then Return
        If _inCurrentChanged Then Return
        Dim d As New JobBindingSource_CurrentChangedDelegate(AddressOf JobBindingSource_CurrentChangedCallback)
        Me.Invoke(d)

    End Sub


    Private Sub JobBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles JobBindingSource.ListChanged
        If _currentJob IsNot Nothing Then
            Dim i As Integer = Me.JobBindingSource.Find(STR_JobUID, _currentJob.JobUID)
            If i <> -1 Then
                Me.JobBindingSource.Position = i
            End If
        Else
            Me.JobBindingSource.MoveFirst()
        End If
    End Sub

    Private Sub JobBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobBindingSource.PositionChanged
        Me.SetCurrentJob()
    End Sub

    Public Sub FindConsultantRecord(ByVal consultantUid As Guid)
        For Each dgvr As DataGridViewRow In Me.JobDataGridView.Rows
            Dim drv As DataRowView = TryCast(dgvr.DataBoundItem, DataRowView)
            If drv IsNot Nothing Then
                Dim job As activiserDataSet.JobRow = TryCast(drv.Row, activiserDataSet.JobRow)
                If job.ConsultantUID = consultantUid Then
                    ' found one.
                    Me.JobDataGridView.FirstDisplayedCell = dgvr.Cells(0)
                    Me.JobDataGridView.CurrentCell = dgvr.Cells(0)
                    Me.JobDataGridView.CurrentCell = Nothing
                    dgvr.Selected = True
                    Return
                End If
            End If
        Next
    End Sub

    Private Sub RefreshGridSelection()
        If Me._settingFilter Then Return
        If Me.JobBindingSource.Position = -1 Then Return

        Dim drv As DataRowView = TryCast(Me.JobBindingSource.Current, DataRowView)
        Try
            If drv IsNot Nothing Then
                Me.JobDataGridView.ClearSelection()
                'If Me.JobDataGridView.SelectedRows.Count <> 1 OrElse (Me.JobDataGridView.SelectedRows(0).DataBoundItem IsNot drv) Then
                Dim dgrv As DataGridViewRow
                For i As Integer = 0 To Me.JobDataGridView.Rows.Count - 1
                    dgrv = Me.JobDataGridView.Rows(i)
                    If dgrv.DataBoundItem Is drv Then
                        Dim newCell As DataGridViewCell = Me.JobDataGridView.Item(0, i)
                        If Not newCell.Visible Then
                            If i > 3 Then
                                Me.JobDataGridView.FirstDisplayedScrollingRowIndex = i - 3
                            Else
                                Me.JobDataGridView.FirstDisplayedScrollingRowIndex = 0
                            End If

                            If Not newCell.Visible Then
                                Me.JobDataGridView.FirstDisplayedCell = newCell
                            End If
                        End If

                        If newCell.RowIndex <> i Then ' IsNot Me.JobDataGridView.CurrentCell Then
                            Me.JobDataGridView.CurrentCell = newCell
                            Me.JobDataGridView.CurrentCell = Nothing ' reset to force selection of whole row
                        End If

                        dgrv.Selected = True
                        Exit For
                    End If
                Next
                'End If
            End If
        Catch ex As Exception
            Me.JobDataGridView.CurrentCell = Nothing
        End Try
        'Me.JobDataGridView.CurrentCell = Me.JobDataGridView.Item(-1, -1)

    End Sub

#Region "Refresh"
    Private _inRefresh As Boolean
    Private Delegate Sub RefreshMeDelegate()

    Private Sub RefreshMeSetSignature()
        If Not _currentJob.IsSignatureNull AndAlso Not String.IsNullOrEmpty(_currentJob.Signature) Then
            Me.SignatureViewer.SetValueAndJobGuid(_currentJob.Signature, _currentJob.JobUID)
            If Me.SignatureViewer.Image IsNot Nothing Then
                Me.StatusBarHaveSignature.Text = Terminology.GetString(MODULENAME, STR_HaveSignature) ' "Signed"
            Else
                Me.StatusBarHaveSignature.Text = Terminology.GetString(MODULENAME, STR_DoNotHaveSignature) ' "Signed"
            End If
        Else
            Me.SignatureViewer.SetValueAndJobGuid(Nothing, _currentJob.JobUID)
            Me.StatusBarHaveSignature.Text = Terminology.GetString(MODULENAME, STR_DoNotHaveSignature)
        End If
    End Sub

    Private Sub RefreshMeSetSiteText()
        Me.StatusBarSiteName.Text = Me._currentJob.RequestRow.ClientSiteRow.SiteName
    End Sub

    Private Sub RefreshMeSetConsultantText()
        If Me._currentJob.IsConsultantUIDNull Then
            Me.StatusBarConsultant.Text = My.Resources.NotAvailable
        Else
            Me.StatusBarConsultant.Text = Me._currentJob.ConsultantRow.Name
        End If
    End Sub

    Private Sub RefreshMeSetRequestText()
        If Me._currentRequest IsNot Nothing Then
            If Me._currentRequest.IsRequestNumberNull Then
                If Not Me._currentRequest.IsRequestIDNull Then
                    Me.StatusBarRequestNumber.Text = Terminology.GetFormattedString(MODULENAME, STR_StatusBarRequestNumber, Me._currentRequest.RequestID)
                Else
                    Me.StatusBarRequestNumber.Text = Terminology.GetFormattedString(MODULENAME, STR_StatusBarRequestNumber, My.Resources.Unassigned)
                End If
            Else
                Me.StatusBarRequestNumber.Text = Terminology.GetFormattedString(MODULENAME, STR_StatusBarRequestNumber, Me._currentRequest.RequestNumber)
            End If
            SetRequestStatusChangeLabel()
        Else
            Me.StatusBarRequestNumber.Text = Terminology.GetFormattedString(MODULENAME, STR_StatusBarRequestNumber, My.Resources.NotAvailable)
        End If
    End Sub

    Private Sub RefreshMeSetEmailStatus()
        Me.emailStatus.Enabled = True
        Select Case Me._currentJob.EmailStatus
            Case 0
                Me.emailStatus.CheckState = CheckState.Unchecked
            Case 1
                Me.emailStatus.CheckState = CheckState.Checked
            Case Else
                Me.emailStatus.CheckState = CheckState.Indeterminate
        End Select

        Me.ResendEmailMenu.Enabled = Me._currentJob.EmailStatus <> 0
    End Sub

    Private Sub RefreshMeEmptyForm()
        Me.SignatureViewer.SetValueAndJobGuid(Nothing, Guid.Empty)
        Me.StatusBarHaveSignature.Text = Terminology.GetString(MODULENAME, STR_DoNotHaveSignature)
        Me.StatusBarSiteName.Text = Terminology.GetString(MODULENAME, STR_NoClientInformation)
        Me.StatusBarConsultant.Text = My.Resources.NotAvailable
        Me.StatusBarRequestNumber.Text = My.Resources.NotAvailable
        Me.StatusBarHasNotes.Visible = False
        Me.StatusBarHasEquipment.Visible = False
        For Each cfp As CustomFormPanel In Me.CustomFormPanels
            cfp.Current = Nothing
        Next
        Me.emailStatus.Enabled = False
        EnableForm(False) ', False)
    End Sub

    Private Sub RefreshMeSetNotes()
        Me.StatusBarHasNotes.Visible = (Not _currentJob.IsJobNotesNull AndAlso Not String.IsNullOrEmpty(_currentJob.JobNotes))
    End Sub

    Private Sub RefreshMeSetEquipment()
        Me.StatusBarHasEquipment.Visible = (Not _currentJob.IsEquipmentNull AndAlso Not String.IsNullOrEmpty(_currentJob.Equipment))
    End Sub

    Private Sub RefreshMe()
        If _inRefresh Then Exit Sub
        Try
            _inRefresh = True
            'If _currentJob Is Nothing Then
            '    Exit Sub
            'End If
            If _currentJob IsNot Nothing Then
                Me.ConsultantUIDComboBox.Enabled = False
                Me.EnableConsultantChange.Checked = False
                Me.ClientSiteUIDComboBox.Enabled = False
                Me.EnableClientSiteChange.Checked = False
                Me.RequestUIDComboBox.Enabled = False
                Me.EnableRequestChange.Checked = False
                Me.ConsultantStatusIDComboBox.Enabled = ConsoleUser.Management OrElse ConsoleUser.Administration OrElse _currentJob.ConsultantUID = ConsoleUser.ConsultantUID

                RefreshMeSetSignature()

                RefreshMeSetSiteText()

                RefreshMeSetConsultantText()

                RefreshMeSetRequestText()

                RefreshMeSetNotes()

                RefreshMeSetEquipment()


                RefreshMeSetEmailStatus()

                DisplayFlagStates(If(Not _currentJob.IsFlagNull, _currentJob.Flag, 0))

                Dim JobEditable As Boolean
                Dim adminLocked As Boolean = False

                JobEditable = (Me._currentJob.IsJobStatusIDNull OrElse Me._currentJob.JobStatusID > 0) AndAlso _
                    (ConsoleUser.Administration OrElse ConsoleUser.Management OrElse _
                    (Not Me._currentJob.IsConsultantUIDNull AndAlso (Me._currentJob.ConsultantUID = ConsoleUser.ConsultantUID)))

                If My.Settings.LockJobOnAdminApproval AndAlso ((Me._currentJob.Flag And 2) = 2) Then
                    adminLocked = True
                ElseIf My.Settings.LockJobOnManagementApproval AndAlso ((Me._currentJob.Flag And 1) = 1) Then
                    adminLocked = True
                End If

                EnableForm((Not adminLocked) AndAlso JobEditable) ', (Not adminLocked))
                For Each cfp As CustomFormPanel In Me.CustomFormPanels
                    cfp.ReadOnly = adminLocked
                    cfp.Current = _currentJob
                Next
            Else
                RefreshMeEmptyForm()
            End If
        Catch
        Finally
            RefreshGridSelection()
            _inRefresh = False
        End Try
    End Sub
#End Region

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        If _currentJob IsNot Nothing Then
            Try
                Dim jsPrint As New JobSheet(_currentJob, Me.SignatureViewer.Image())
                Dim jf As String = jsPrint.GenerateJobHtml() ' Display the job sheet
                System.Diagnostics.Process.Start(jf) ' The html file to be used.
            Catch ex As Exception
                Library.DisplayException.Show(ex, My.Resources.UnableToPrintJob, Library.Icons.Printer)
            End Try
        End If
    End Sub

    Private Sub UpdateStatusButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateStatusButton.Click
        ' update flags of current job just before current job changes - this prevents the viewed job from 
        ' disappearing when you click the checkbox which is filtering the view.
        Try
            Dim drv As DataRowView = TryCast(Me.JobBindingSource.Current, DataRowView)
            If drv IsNot Nothing Then
                Dim currentPosition As Integer = Me.JobBindingSource.Position
                If drv.IsEdit Then
                    ThreadingDelegates.BindingSourceEndEdit(Me, Me.JobBindingSource)
                    Console.ConsoleData.NeedRefresh()

                ElseIf DataRowHasChanges(drv.Row) Then '.HasVersion(DataRowVersion.Proposed) Then
                    Console.ConsoleData.NeedRefresh()
                End If
                'Application.DoEvents()

                If currentPosition < (Me.JobBindingSource.Count) Then
                    Me.JobBindingSource.Position = currentPosition ' Me.JobBindingSource.MoveNext()
                    'Me.JobDataGridView.Rows(currentPosition).Selected = True
                Else
                    'HACK: when clearing last row in grid, it doesn't select the last row of the grid when you set focus to it??
                    'Me.JobBindingSource.MoveFirst()
                    Me.JobBindingSource.MoveLast()
                    If Me.JobBindingSource.Position <> -1 Then
                        Me.JobDataGridView.Rows(Me.JobBindingSource.Position).Selected = True
                    End If
                End If
                Me.SetCurrentJob()
                'Me.JobDataGridView.Refresh()
                'RefreshMe()
                'Application.DoEvents()
                Me.Refresh()
            End If
        Catch ex As Exception
            TraceError(ex)
        Finally
            If MainForm IsNot Nothing Then
                MainForm.UpdateApprovalTree()
            End If
        End Try
    End Sub

    Private Sub JobDataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles JobDataGridView.CellFormatting
        If sender Is Me.JobDataGridView Then
            Try
                If e.RowIndex = -1 Then Exit Try

                Dim r As DataGridViewRow = Me.JobDataGridView.Rows(e.RowIndex)
                If r Is Nothing Then Exit Try

                Dim lRow As System.Data.DataRowView = TryCast(r.DataBoundItem, DataRowView)
                If lRow Is Nothing Then Exit Try

                Dim ji As Integer = CoreDataSet.Job.Rows.IndexOf(lRow.Row)
                Dim jr As activiserDataSet.JobRow = Nothing

                If ji <> -1 Then
                    jr = CType(CoreDataSet.Job.Rows(ji), activiserDataSet.JobRow)
                End If

                If jr IsNot Nothing AndAlso jr.RowState <> DataRowState.Unchanged Then
                    If Not e.CellStyle.Font.Bold Then
                        e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                    End If
                Else
                    If e.CellStyle.Font.Bold Then
                        e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Regular)
                    End If
                End If
            Catch ex As IndexOutOfRangeException
                ' que ? probly row's been removed.
            Catch ex As Exception
                TraceError("Error setting job grid view cell style: {0}", ex.ToString())
            End Try

            Try
                Dim c As DataGridViewColumn = Me.JobDataGridView.Columns(e.ColumnIndex)
                If c Is Me.JobStartTimeColumn OrElse c Is Me.JobFinishTimeColumn Then
                    Dim dv As Date
                    If Not IsDBNull(e.Value) Then
                        dv = DateTime.SpecifyKind(CDate(e.Value), DateTimeKind.Utc)
                        e.Value = dv.ToLocalTime.ToString("G")
                        e.FormattingApplied = True
                    Else
                        e.Value = My.Resources.None
                        e.FormattingApplied = True
                    End If
                End If
            Catch ex As Exception
                e.Value = My.Resources.None
                e.FormattingApplied = True
            End Try
        End If
    End Sub

    Private Sub JobDataGridView_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles JobDataGridView.ColumnWidthChanged
        If haveInitialised Then
            If My.Settings.JobAdminGridColumnWidths Is Nothing Then
                My.Settings.JobAdminGridColumnWidths = New Collections.Specialized.StringCollection
            End If
            Utilities.SaveColumnWidthChange(My.Settings.JobAdminGridColumnWidths, e.Column.Name, e.Column.Width)
        End If
    End Sub

    Private Sub JobDataGridView_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles JobDataGridView.DataError
        TraceVerbose("JobDataGridView_DataError: {0};{1}", e.ToString, e.Exception.Message)
    End Sub

    Private Sub EnableConsultantChange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableConsultantChange.CheckedChanged
        Me.ConsultantUIDComboBox.Enabled = Me.EnableConsultantChange.Checked
    End Sub

    Private Sub EnableClientSiteChange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableClientSiteChange.CheckedChanged
        Me.ClientSiteUIDComboBox.Enabled = Me.EnableClientSiteChange.Checked
    End Sub

    Private Sub EnableRequestChange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableRequestChange.CheckedChanged
        Me.RequestUIDComboBox.Enabled = Me.EnableRequestChange.Checked

        If Me.RequestUIDComboBox.Enabled AndAlso Me._currentJob IsNot Nothing AndAlso Not Me._currentJob.IsClientSiteUIDNull Then
            Dim clientSiteRequestFilter As String = String.Format("ClientSiteUID='{0}'", Me._currentJob.ClientSiteUID)
            Me.ReloadRequestList(clientSiteRequestFilter)
        Else
            Me.ReloadRequestList(String.Empty)
        End If
    End Sub

    Private Sub _consoleData_Refreshed(ByVal sender As Object, ByVal e As ConsoleDataRefreshEventArgs) Handles _consoleData.Refreshed
        If _refreshTimer IsNot Nothing Then _refreshTimer.Start()
    End Sub

    Private Sub _refreshTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles _refreshTimer.Elapsed
        ResumeBindingSources()
    End Sub

    Private Sub _consoleData_Refreshing(ByVal sender As Object, ByVal e As System.EventArgs) Handles _consoleData.Refreshing
        SuspendBindingSources()
    End Sub

    Private Delegate Sub SuspendBindingSourcesCallback()
    Private Sub SuspendBindingSources()
        If Not haveInitialised Then Return
        If Me.InvokeRequired OrElse Me.JobDataGridView.InvokeRequired Then
            Dim d As New SuspendBindingSourcesCallback(AddressOf SuspendBindingSources)
            Me.Invoke(d)
        Else
            _suspended = True
        End If
    End Sub

    Private Delegate Sub ResumeBindingSourcesCallback()
    Private Sub ResumeBindingSources()
        If Not haveInitialised Then Return
        If Me.InvokeRequired OrElse Me.JobDataGridView.InvokeRequired Then
            Dim d As New ResumeBindingSourcesCallback(AddressOf ResumeBindingSources)
            Me.Invoke(d)
        Else
            TraceVerbose(STR_Entered)
            Trace.Indent()
            Try
                Me.SuspendLayout()
                Me.JobBindingSource.ResetBindings(False)
                Me.ReloadStatusList()
                Me.ReloadClientList()
                Me.ReloadConsultantList()
                Me.ReloadRequestList(Nothing)
                _suspended = False
            Catch ex As Exception
                TraceError(ex)
            Finally
                Me.ResumeLayout()
                Trace.Unindent()
            End Try
            TraceVerbose(STR_Done)
        End If
    End Sub

    Private Sub BindingNavigatorRefreshItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Console.ConsoleData.StartRefresh()
    End Sub

#Region "Filters"
    Private baseFilter As String = String.Empty
    Private StartTimeFilter As String = String.Empty
    Private FinishTimeFilter As String = String.Empty
    Private ConsultantFilter As String = String.Empty
    Private ClientFilter As String = String.Empty
    Private RequestFilter As String = String.Empty

    Private _settingFilter As Boolean

    Private Sub BuildFilter()
        _settingFilter = True
        Dim filters As New Collections.Specialized.StringCollection
        'filters.Add("JobStatusID < 5")
        If Not String.IsNullOrEmpty(baseFilter) Then filters.Add(baseFilter)
        If Not String.IsNullOrEmpty(StartTimeFilter) Then filters.Add(StartTimeFilter)
        If Not String.IsNullOrEmpty(FinishTimeFilter) Then filters.Add(FinishTimeFilter)
        If Not String.IsNullOrEmpty(ConsultantFilter) Then filters.Add(ConsultantFilter)
        If Not String.IsNullOrEmpty(ClientFilter) Then filters.Add(ClientFilter)
        If Not String.IsNullOrEmpty(RequestFilter) Then filters.Add(RequestFilter)

        Dim aFilters(filters.Count - 1) As String
        filters.CopyTo(aFilters, 0)
        Dim newFilter As String = String.Join(" AND ", aFilters)
        'Dim previousJob As activiserDataSet.JobRow = CurrentJob
        Me.SuspendLayout()
        Me.JobBindingSource.Filter = newFilter
        'If previousJob IsNot Nothing Then
        '    Dim i As Integer = Me.JobBindingSource.Find("JobUID", previousJob.JobUID)
        '    If i <> -1 Then
        '        Me.JobBindingSource.Position = i
        '    End If
        'End If
        _settingFilter = False
        Me.ResumeLayout()
        Me.SetCurrentJob()
    End Sub

    Public Property Filter() As String
        Get
            Return Me.JobBindingSource.Filter
        End Get
        Set(ByVal value As String)
            Me.baseFilter = value
            BuildFilter()
        End Set
    End Property

    Private Sub FilterMenuClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuClearAll.Click
        StartTimeFilter = String.Empty
        FinishTimeFilter = String.Empty
        ConsultantFilter = String.Empty
        ClientFilter = String.Empty
        RequestFilter = String.Empty
        BuildFilter()
    End Sub

    Private Sub FilterMenuDateToday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuDateToday.Click
        Const FilterTemplate As String = "StartTime >= '{0:s}'"
        StartTimeFilter = String.Format(FilterTemplate, Date.Today.ToUniversalTime)
        BuildFilter()
    End Sub

    Private Sub FilterMenuAllDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuAllDates.Click
        StartTimeFilter = ""
        FinishTimeFilter = ""
        BuildFilter()
    End Sub

    Private Sub FilterMenuYesterday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilterMenuYesterday.Click
        Const startFilterTemplate As String = "StartTime >= '{0:s}'"
        Const finishFilterTemplate As String = "FinishTime < '{0:s}'"
        StartTimeFilter = String.Format(startFilterTemplate, Date.Today.ToUniversalTime.Subtract(TimeSpan.FromDays(1)))
        FinishTimeFilter = String.Format(finishFilterTemplate, Date.Today.ToUniversalTime)
        BuildFilter()
    End Sub

    Private Sub FilterMenuPastWeek_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilterMenuPastWeek.Click
        Const FilterTemplate As String = "StartTime >= '{0:s}'"
        StartTimeFilter = String.Format(FilterTemplate, Date.Today.ToUniversalTime.Subtract(TimeSpan.FromDays(7)))
        FinishTimeFilter = ""
        BuildFilter()
    End Sub

    Private Sub FilterMenuPastMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilterMenuPastMonth.Click
        Const FilterTemplate As String = "StartTime >= '{0:s}'"
        Dim days As Integer
        If Date.Today.Month > 1 Then
            days = DateTime.DaysInMonth(Date.Today.Year, Date.Today.Month - 1)
        Else
            days = DateTime.DaysInMonth(Date.Today.Year - 1, 12)
        End If
        StartTimeFilter = String.Format(FilterTemplate, Date.Today.ToUniversalTime.Subtract(TimeSpan.FromDays(days)))
        FinishTimeFilter = ""
        BuildFilter()
    End Sub

    Private Sub FilterMenuConsultantNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuConsultantNone.Click
        ConsultantFilter = ""
        BuildFilter()
    End Sub

    Private Sub FilterMenuConsultantInclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuConsultantInclude.Click
        If _currentJob.IsConsultantUIDNull Then
            ConsultantFilter = "(ConsultantUID IS NULL)"
        Else
            Const FilterTemplate As String = "ConsultantUID = '{0}'"
            ConsultantFilter = String.Format(FilterTemplate, _currentJob.ConsultantUID)
        End If
        BuildFilter()
    End Sub

    Private Sub FilterMenuConsultantExclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuConsultantExclude.Click
        If ConsultantFilter.Length <> 0 Then ConsultantFilter &= " AND "
        If _currentJob.IsConsultantUIDNull Then
            ConsultantFilter = "(NOT ConsultantUID IS NULL)"
        Else
            Const FilterTemplate As String = "ConsultantUID <> '{0}'"
            ConsultantFilter &= String.Format(FilterTemplate, _currentJob.ConsultantUID)
        End If
        BuildFilter()
    End Sub

    Private Sub FilterMenuClientNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuClientNone.Click
        ClientFilter = ""
        BuildFilter()
    End Sub

    Private Sub FilterMenuClientInclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuClientInclude.Click
        If _currentJob.IsClientSiteUIDNull Then
            ClientFilter = "(ClientSiteUID IS NULL)"
        Else
            Const FilterTemplate As String = "ClientSiteUID = '{0}'"
            ClientFilter = String.Format(FilterTemplate, _currentJob.ClientSiteUID)
        End If
        BuildFilter()
    End Sub

    Private Sub FilterMenuClientExclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuClientExclude.Click
        If ClientFilter.Length > 0 Then ClientFilter &= " AND "
        If _currentJob.IsClientSiteUIDNull Then
            ClientFilter = "(NOT ClientSiteUID IS NULL)"
        Else
            Const FilterTemplate As String = "(ClientSiteUID <> '{0}')"
            ClientFilter &= String.Format(FilterTemplate, _currentJob.ClientSiteUID)
        End If
        BuildFilter()
    End Sub

    Private Sub FilterMenuRequestNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuRequestNone.Click
        RequestFilter = ""
        BuildFilter()
    End Sub

    Private Sub FilterMenuRequestOnlySelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuRequestOnlySelected.Click
        If _currentJob.IsRequestUIDNull Then
            RequestFilter &= "(RequestUID IS NULL)"
        Else
            Const FilterTemplate As String = "RequestUID = '{0}'"
            RequestFilter &= String.Format(FilterTemplate, _currentJob.RequestUID)
        End If
        BuildFilter()
    End Sub

    Private Sub FilterMenuRequestExcludeSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuRequestExcludeSelected.Click
        If RequestFilter.Length <> 0 Then RequestFilter &= " AND "
        If _currentJob.IsRequestUIDNull Then
            RequestFilter &= "(NOT RequestUID IS NULL)"
        Else
            Const FilterTemplate As String = "RequestUID <> '{0}'"
            RequestFilter &= String.Format(FilterTemplate, _currentJob.RequestUID)
        End If
        BuildFilter()
    End Sub
#End Region

    Private Sub JobDataGridView_ColumnDisplayIndexChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles JobDataGridView.ColumnDisplayIndexChanged
        If Me.haveInitialised Then
            Utilities.SaveColumnOrderChange(My.Settings.JobAdminGridColumnOrder, e.Column.Name, e.Column.DisplayIndex)
        End If
    End Sub

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property CurrentJob() As activiserDataSet.JobRow
        Get
            Return _currentJob
        End Get
        Set(ByVal value As activiserDataSet.JobRow)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            Me.JobBindingSource.Position = Me.JobBindingSource.Find(STR_JobUID, value.JobUID)
        End Set
    End Property


    Private Delegate Sub SetCurrentJobDelegate()
    Private _inSetCurrentJob As Boolean

    Private Sub DisableForm()
        Me._currentRequest = Nothing
        Me.RequestBindingSource.Filter = "1=2"
        EnableForm(False) ', True)
    End Sub

    Private Sub EnableForm(ByVal enableEdits As Boolean) ', ByVal allowStatusChange As Boolean)
        If Me.EnableClientSiteChange.Enabled <> enableEdits Then Me.EnableClientSiteChange.Enabled = enableEdits
        If Me.EnableConsultantChange.Enabled <> enableEdits Then Me.EnableConsultantChange.Enabled = enableEdits
        If Me.EnableRequestChange.Enabled <> enableEdits Then Me.EnableRequestChange.Enabled = enableEdits
        If Me.RequestStatusIDComboBox.Enabled <> enableEdits Then Me.RequestStatusIDComboBox.Enabled = enableEdits
        If Me.ConsultantStatusIDComboBox.Enabled <> enableEdits Then Me.ConsultantStatusIDComboBox.Enabled = enableEdits
        If Me.EmailTextBox.Enabled <> enableEdits Then Me.EmailTextBox.Enabled = enableEdits
        If Me.JobDetailsTextBox.Enabled <> enableEdits Then Me.JobDetailsTextBox.Enabled = enableEdits
        Me.ConsultantNotesTextBox.Enabled = Me._currentJob IsNot Nothing ' consultant notes always enabled if there's a job.
        If Me.EquipmentNotesTextBox.Enabled <> enableEdits Then Me.EquipmentNotesTextBox.Enabled = enableEdits

        'If _currentJob IsNot Nothing AndAlso _currentJob.JobStatusID = 0 Then
        '    If Me.JobStatusPicker.Enabled <> allowStatusChange Then Me.JobStatusPicker.Enabled = allowStatusChange
        'Else
        '    Me.JobStatusPicker.Enabled = False
        'End If

        If Not Me.ApprovalGroup.Enabled = enableEdits Then Me.ApprovalGroup.Enabled = enableEdits
        If Not Me.JobInfoGroup.Enabled = enableEdits Then Me.JobInfoGroup.Enabled = enableEdits
        If Not Me.RequestInformationLabel.Enabled = enableEdits Then Me.RequestInformationLabel.Enabled = enableEdits
        If Not Me.SignatureGroup.Enabled = enableEdits Then Me.SignatureGroup.Enabled = enableEdits
        For Each cfp As CustomFormPanel In Me.CustomFormPanels
            If Not cfp.Enabled = enableEdits Then cfp.Enabled = enableEdits
        Next
    End Sub

    Private Sub SetRequestFilter()
        Dim requestFilter As String = String.Format("RequestUID='{0}'", _currentJob.RequestUID)
        If Me.RequestBindingSource.Filter <> requestFilter Then Me.RequestBindingSource.Filter = requestFilter
    End Sub

    Private Sub SetCurrentJob()
        If Me._settingFilter Then Return

        If Me.InvokeRequired Then
            Dim d As New SetCurrentJobDelegate(AddressOf SetCurrentJob)
            Me.Invoke(d)
        Else
            Me.SuspendLayout()
            Me._inSetCurrentJob = True
            Try
                Dim newCurrentJob As activiserDataSet.JobRow = Nothing

                If Me.JobBindingSource.Position <> -1 Then
                    Dim drv As DataRowView = TryCast(Me.JobBindingSource.Current, DataRowView)
                    If drv IsNot Nothing Then newCurrentJob = TryCast(drv.Row, activiserDataSet.JobRow)
                End If

                If newCurrentJob IsNot Nothing AndAlso newCurrentJob Is Me._currentJob Then
                    'Me.ReloadRequestList(Nothing)
                    Me.RefreshGridSelection()
                    Return
                ElseIf newCurrentJob IsNot Nothing AndAlso newCurrentJob IsNot Me._currentJob Then
                    Me._currentJob = Nothing
                    Me._currentJob = newCurrentJob
                Else
                    Me._currentJob = Nothing
                End If

                If Me._currentJob IsNot Nothing Then
                    Me.JobFinishDateTimePicker.MinValue = DateTime.MinValue ' reset

                    If Not Me._currentJob.IsStartTimeNull Then
                        If Not Me._currentJob.IsFinishTimeNull AndAlso Me._currentJob.FinishTime < Me._currentJob.StartTime Then
                            Terminology.DisplayMessage(MODULENAME, RES_FinishTimeBeforeStartTime, MessageBoxIcon.Warning)
                            'Me._currentJob.FinishTime = Me._currentJob.StartTime
                        Else
                            'Me.JobFinishDateTimePicker.MinValue = Me._currentJob.StartTime
                        End If
                    End If
                End If

                If Me._currentJob IsNot Nothing AndAlso Not Me._currentJob.IsRequestUIDNull Then
                    Me._currentRequest = _currentJob.RequestRow
                    Me.SetRequestFilter()
                    Me.RequestInformationLabel.Enabled = True

                    Me.ReloadRequestList(String.Empty)

                    SetGpsBrowserContent()

                Else
                    Me.RequestBindingSource.Filter = "1=2"
                    DisableForm()
                End If

                RefreshMe()
            Catch ex As Exception
            Finally
                Me.ResumeLayout(True)
                _inSetCurrentJob = False
            End Try
        End If
    End Sub

    Private Sub SetRequestStatusChangeLabel()
        Me.RequestStatusChangeReasonLabel.Visible = False
        If Me._currentJob Is Nothing OrElse Me._currentRequest Is Nothing Then Exit Sub
        If Not _currentRequest.IsConsultantStatusIDNull AndAlso Not _currentRequest.IsRequestStatusIDNull AndAlso Me._currentRequest.RequestStatusID <> Me._currentRequest.ConsultantStatusID Then
            Dim statusChangeFilter As String = String.Format("RequestUID='{0}' AND Flag={1} AND JobStatusID=6", _currentRequest.RequestUID, _currentRequest.ConsultantStatusID)
            Dim jrs() As activiserDataSet.JobRow = CType(Console.ConsoleData.CoreDataSet.Job.Select(statusChangeFilter, "ModifiedDateTime DESC"), activiserDataSet.JobRow())
            If jrs IsNot Nothing AndAlso jrs.Length <> 0 Then
                Me.RequestStatusChangeReasonLabel.Text = jrs(0).JobDetails
            Else
                Me.RequestStatusChangeReasonLabel.Text = Terminology.GetFormattedString(MODULENAME, "StatusChangeMessageTemplate", _currentRequest.RequestStatusRowByFK_Request_ConsultantStatus.Description)
            End If
            Me.RequestStatusChangeReasonLabel.Visible = True
        End If
    End Sub

    Private Sub RequestBindingSource_BindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles RequestBindingSource.BindingComplete
        If Not e.BindingCompleteState = BindingCompleteState.Success Then Exit Sub

        If e.BindingCompleteContext = BindingCompleteContext.DataSourceUpdate Then
            SetRequestStatusChangeLabel()
        End If
    End Sub

    Private Sub AcceptStatusChange()
        If Me._currentRequest IsNot Nothing Then
            TraceVerbose(STR_Entered)
            Trace.Indent()
            Me._currentRequest.RequestStatusID = Me._currentRequest.ConsultantStatusID
            Me.RequestStatusIDComboBox.SelectedItem = Me._currentRequest.RequestStatusID
            Me.ConsultantStatusIDComboBox.SelectedValue = Me._currentRequest.RequestStatusID
            Me.RequestStatusChangeReasonLabel.Visible = False
            Console.ConsoleData.NeedRefresh()
            Console.ConsoleData.MainForm.UpdateRequestStatusTree()
            Trace.Unindent()
            TraceVerbose(STR_Done)
        End If
    End Sub
    Private Sub RequestStatusChangeReasonLabel_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestStatusChangeReasonLabel.DoubleClick
        AcceptStatusChange()
    End Sub

    Private Sub AcceptRequestStatusChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcceptRequestStatusChange.Click
        AcceptStatusChange()
    End Sub

    Private Sub RejectRequestStatusChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectRequestStatusChange.Click
        If Me._currentRequest IsNot Nothing Then
            TraceVerbose(STR_Entered)
            Trace.Indent()
            Me._currentRequest.ConsultantStatusID = Me._currentRequest.RequestStatusID
            Me.ConsultantStatusIDComboBox.SelectedValue = Me._currentRequest.ConsultantStatusID
            Console.ConsoleData.NeedRefresh()
            Console.ConsoleData.MainForm.UpdateRequestStatusTree()
            Trace.Unindent()
            TraceVerbose(STR_Done)
        End If
    End Sub

    Private Sub RequestStatusIDComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestStatusIDComboBox.SelectionChangeCommitted
        If Not Me._currentRequest.IsConsultantStatusIDNull AndAlso (CInt(Me.RequestStatusIDComboBox.SelectedValue) <> Me._currentRequest.ConsultantStatusID) Then
            Dim target As activiserDataSet.RequestRow = _currentRequest
            target.RequestStatusID = CInt(Me.RequestStatusIDComboBox.SelectedValue)
            target.ConsultantStatusID = target.RequestStatusID

            MakeDirty()
            Console.ConsoleData.MainForm.UpdateRequestStatusTree()
            Console.ConsoleData.MainForm.UpdateRequestAdminButton()
        End If
    End Sub

    Private Sub StatusBarRefreshEnabled_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Console.ConsoleData.Polling = Not Console.ConsoleData.Polling
    End Sub

    Private Sub ClientSiteUIDComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.SelectionChangeCommitted
        If _currentJob Is Nothing Then Return
        Dim newUid As Guid = CType(Me.ClientSiteUIDComboBox.SelectedValue, Guid)

        Me.ReloadRequestList(String.Format("ClientSiteUID = '{0}'", newUid.ToString()))
        MakeDirty()
    End Sub

    Private _lastProgressBarClickTime As Integer
    Private Sub RefreshProgressBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim thisProgressBarClickTime As Integer = My.Computer.Clock.TickCount
        If thisProgressBarClickTime - _lastProgressBarClickTime < 500 Then ' double-click
            Console.ConsoleData.StartRefresh()
        End If
        _lastProgressBarClickTime = thisProgressBarClickTime
    End Sub

    ''' <summary>
    ''' HACK: because binding doesn't always pick up changed data!
    ''' </summary>
    ''' <remarks></remarks>  
    Private Sub MakeDirty()
        If Not haveInitialised Then Return
        Console.ConsoleData.SetEditorState(MODULENAME, True)
        Console.ConsoleData.NeedRefresh()
        _dirty = True
        ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, _dirty)
        'Application.DoEvents()
        Me.Refresh()
    End Sub

    Private Sub Control_Value_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsultantUIDComboBox.SelectionChangeCommitted
        MakeDirty()
    End Sub

    Private Sub Control_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles MinsTravelledNumericUpDown.PropertyChanged, JobFlagGroup.PropertyChanged
        Me.MakeDirty()
    End Sub

    Private Sub TextBox_ModifiedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailTextBox.ModifiedChanged, JobDetailsTextBox.ModifiedChanged, EquipmentNotesTextBox.ModifiedChanged, ConsultantNotesTextBox.ModifiedChanged
        If Not haveInitialised Then Return
        Dim tb As TextBoxBase = TryCast(sender, TextBoxBase)
        If tb IsNot Nothing Then
            If tb.Modified Then
                MakeDirty()
            End If
        End If
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailTextBox.TextChanged, JobDetailsTextBox.TextChanged, EquipmentNotesTextBox.TextChanged, ConsultantNotesTextBox.TextChanged
        If Not haveInitialised Then Return
        Dim tb As TextBoxBase = TryCast(sender, TextBoxBase)
        If tb IsNot Nothing Then
            If tb.Modified Then
                MakeDirty()
            End If
        End If
    End Sub

    'Private Sub SubForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
    '    If Me.Visible Then
    '        ResumeBindingSources()
    '    Else
    '        SuspendBindingSources()
    '    End If
    'End Sub

    Private Sub ToolStripUndoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripUndoButton.Click
        Dim drv As DataRowView = TryCast(Me.JobBindingSource.Current, DataRowView)
        If drv IsNot Nothing Then
            If drv.IsEdit Then
                If MessageBox.Show("Undo all changes made to this record?", My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, Nothing, False) = DialogResult.Yes Then
                    Me.JobBindingSource.CancelEdit()
                    drv.Row.RejectChanges()
                    _dirty = False
                    ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, False)
                End If
            End If
        End If
    End Sub

    'Private Sub EMailStatusPicker_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If CurrentJob IsNot Nothing Then
    '        CurrentJob.SetMinutesWorkedNull()
    '        'Me.EMailStatusPicker.ResetText()
    '    End If
    'End Sub

    Private Sub ConsultantStatusIDComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsultantStatusIDComboBox.SelectionChangeCommitted
        If _currentRequest IsNot Nothing Then
            If ConsultantStatusIDComboBox.SelectedValue IsNot Nothing Then
                _currentRequest.ConsultantStatusID = CInt(ConsultantStatusIDComboBox.SelectedValue)
            Else
                _currentRequest.SetConsultantStatusIDNull()
            End If
        End If
    End Sub

    Private Sub JobStatusChangeToCompleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobStatusChangeToCompleteButton.Click
        If CurrentJob IsNot Nothing Then
            CurrentJob.JobStatusID = 3 ' completed and signed.
        End If
    End Sub



    Private Sub RequestUIDComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestUIDComboBox.SelectionChangeCommitted
        Dim bo As BusinessObjectListItem = TryCast(Me.RequestUIDComboBox.SelectedItem, BusinessObjectListItem)
        If bo IsNot Nothing AndAlso bo.ObjectId.HasValue Then
            Me._currentJob.RequestUID = bo.ObjectId.Value
        ElseIf bo IsNot Nothing Then
            Me._currentJob.SetRequestUIDNull()
        End If
    End Sub

    'Private Sub emailStatus_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me._inSetCurrentJob Then Return
    '    If _currentJob IsNot Nothing Then
    '        Select Case Me.emailStatus.CheckState
    '            Case CheckState.Checked
    '                Me._currentJob.EmailStatus = 1 ' done
    '            Case CheckState.Indeterminate
    '                Me._currentJob.EmailStatus = 129 ' error
    '            Case CheckState.Unchecked
    '                Me._currentJob.EmailStatus = 0 ' not done.
    '        End Select
    '    End If
    'End Sub

    Private Sub emailStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles emailStatus.Click
        If Me._currentJob.EmailStatus <> 0 Then
            Me._currentJob.EmailStatus = 0
        Else
            Me._currentJob.EmailStatus = 1
        End If
        Me.emailStatus.Checked = Me._currentJob.EmailStatus = 1
    End Sub

    Private Sub ResendEmailMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResendEmailMenuItem.Click
        Me._currentJob.EmailStatus = 0
        Me.emailStatus.Checked = False
    End Sub

    Private Sub JobStartDateTimePicker_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobStartDateTimePicker.Validated
        If Me._inCurrentChanged Then Return
        If Me._inSetCurrentJob Then Return

        If Me.JobStartDateTimePicker.HasValue Then
            If Not Me._currentJob.IsFinishTimeNull AndAlso Me._currentJob.FinishTime < Me.JobStartDateTimePicker.Value Then
                Terminology.DisplayMessage(MODULENAME, RES_FinishTimeBeforeStartTime, MessageBoxIcon.Warning)
                'Me._currentJob.FinishTime = Me._currentJob.StartTime
                'Me.JobFinishDateTimePicker.MinValue = DateTime.MinValue
            Else
                'Me.JobFinishDateTimePicker.MinValue = Me.JobStartDateTimePicker.Value
            End If
        End If
    End Sub

    Private Sub JobFinishDateTimePicker_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobFinishDateTimePicker.Validated
        If Me._inCurrentChanged Then Return
        If Me._inSetCurrentJob Then Return

        If Me.JobFinishDateTimePicker.HasValue Then
            If Me.JobFinishDateTimePicker.Value < Me.JobStartDateTimePicker.Value Then
                Terminology.DisplayMessage(MODULENAME, RES_FinishTimeBeforeStartTime, MessageBoxIcon.Warning)
                'Me.JobFinishDateTimePicker.MinValue = DateTime.MinValue
            End If
        End If
    End Sub

    Private Sub TrackingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupportInfoToolStripMenuItem.Click
        Using jif As New JobSupportInfoForm(Me._currentJob)
            jif.ShowDialog()
        End Using
    End Sub

    Private Sub SetGpsBrowserContent()
        _loadMapWhenBrowserLoaded = False

        If _currentJob.IsTrackingInfoNull Then
            TraceVerbose("GPS Data missing")
            GpsSupport.ClearMap(Me.GpsBrowser)
            Return
        End If

        _gi = Nothing
        If Not (Library.Gps.GpsPosition.TryParse(_currentJob.TrackingInfo, _gi) AndAlso _gi.LatitudeValid AndAlso _gi.LongitudeValid) Then
            TraceWarning("GPS Data Invalid")
            GpsSupport.ClearMap(Me.GpsBrowser)
            Return
        End If

        Const STR_BR As String = "<BR>"

        _giAddress = If(Not _currentJob.ClientSiteRow.IsSiteAddressNull, _currentJob.ClientSiteRow.SiteAddress.Replace(vbCrLf, STR_BR).Replace(vbCr, STR_BR).Replace(vbLf, STR_BR), Terminology.GetString(MODULENAME, "Unknown"))
        _giNotes = String.Format("{0}<br>{1}<br>{2}<br>{3}", _currentJob.ClientSiteName, _currentJob.JobDate.ToString("g"), _gi.ToString("l"), _giAddress)
        _giJobNumber = _currentJob.JobNumber

        If Not _browserLoaded Then
            _loadMapWhenBrowserLoaded = True
            Return
        End If

        GpsSupport.DisplayMap(Me.GpsBrowser, _gi.Latitude, _gi.Longitude, _giJobNumber, _giNotes)
    End Sub

    Private _loadMapWhenBrowserLoaded As Boolean
    Private _browserLoaded As Boolean
    Private Sub GpsBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles GpsBrowser.DocumentCompleted
        TraceVerbose("GpsBrowser_DocumentCompleted: {0}", e.Url.ToString()) ' GpsBrowser.DocumentText)
        _browserLoaded = True
        If _loadMapWhenBrowserLoaded Then
            GpsSupport.DisplayMap(Me.GpsBrowser, _gi.Latitude, _gi.Longitude, _giJobNumber, _giNotes)
            _loadMapWhenBrowserLoaded = False
        End If
    End Sub

    Private Sub GpsBrowser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles GpsBrowser.Navigated
        TraceVerbose("GpsBrowser_Navigated to " & e.Url.ToString())
    End Sub
End Class
