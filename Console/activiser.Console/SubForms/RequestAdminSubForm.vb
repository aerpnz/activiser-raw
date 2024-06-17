Imports System.ComponentModel
Imports activiser.Library.activiserWebService

Public Class RequestAdminSubForm
    Const MODULENAME As String = "RequestAdminSubForm"
    Const STR_StandardTraceFormat As String = "{0} : {1} : {2} : {3}"
    Const STR_Description As String = "Description"
    Const STR_RequestStatusID As String = "RequestStatusID"
    Const RES_NoRequestStatus As String = "NoRequestStatus"
    Const STR_SiteName As String = "ClientSiteName"
    Const STR_AssignedToName As String = "AssignedToName"
    Const STR_Done As String = "Done"
    Const STR_Entered As String = "Entered"
    Const STR_Fired As String = "Fired"

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

    Private _initialised As Boolean

    'Private _polling As Boolean = False
    'Public Property Polling() As Boolean
    '    Get
    '        Return _polling
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _polling = value
    '    End Set
    'End Property

    Private consultantList As New BusinessObjectCollection(MODULENAME & ":" & "consultantList")
    Private clientList As New BusinessObjectCollection(MODULENAME & ":" & "clientList")

    Private requestStatusList As New CategoryObjectCollection(MODULENAME & ":" & "requestStatusList")
    Private consultantStatusList As New CategoryObjectCollection(MODULENAME & ":" & "consultantStatusList")
    Private jobStatusList As New CategoryObjectCollection(MODULENAME & ":" & "jobStatusList")

    Private Sub RequestToolbarSaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolbarSaveButton.Click
        Try
            TraceVerbose(STR_Entered)
            RequestNumberTextBox.Focus()
            Me.Validate()
            Me.RequestBindingSource.EndEdit()
            Console.ConsoleData.SetEditorState(MODULENAME, False)
            Console.ConsoleData.NeedRefresh()
            Console.ConsoleData.StartRefresh()
        Catch ex As Exception
            TraceError(ex)
        End Try
        TraceVerbose(STR_Done)
    End Sub

    Public Sub ReloadStatusList()
        CategoryObjectCollection.PopulateList(Me.ConsultantStatusIDComboBox, Me.consultantStatusList, "RequestStatus", STR_RequestStatusID, STR_Description, String.Empty, Nothing, My.Resources.None)
        CategoryObjectCollection.PopulateList(Me.RequestStatusIDComboBox, Me.requestStatusList, "RequestStatus", STR_RequestStatusID, STR_Description)
        CategoryObjectCollection.PopulateList(Me.JobStatusID, Me.jobStatusList, "JobStatus", "JobStatusID", STR_Description)
    End Sub

    Public Sub ReloadClientList()
        BusinessObjectCollection.PopulateList(Me.ClientSiteUIDComboBox, Me.clientList, "ClientSite", "ClientSiteUid", "SiteName")
    End Sub

    Public Sub ReloadConsultantList()
        BusinessObjectCollection.PopulateList(Me.AssignedToUIDComboBox, Me.consultantList, "Consultant", "ConsultantUid", "Name", String.Empty, Nothing, My.Resources.None)
    End Sub

    'this is so that the initialisation can be driven manually from the main form load event, after the initial data set has loaded.
    Friend Sub InitialiseForm()
        TraceInfo("Initialising")

        Terminology.LoadLabels(Me)
        TraceVerbose("Labels loaded")
        Terminology.LoadToolTips(Me, ToolTipProvider)
        TraceVerbose("ToolTips loaded")

        Me.CoreDataSet = Console.ConsoleData.CoreDataSet
        'Debug.Print(Me.RequestDataGridView.DataMember)
        Me.RequestBindingSource.SuspendBinding()

        Me.RequestDataGridView.DataSource = Nothing
        Me.RequestBindingSource.DataSource = Console.ConsoleData.CoreDataSet
        Me.RequestDataGridView.DataSource = Me.RequestBindingSource
        Me.RequestBindingSource.ResumeBinding()
        'Me.JobStatusBindingSource.DataSource = Console.ConsoleData.CoreDataSet
        Me.JobBindingSource.DataSource = Me.RequestBindingSource

        'HACK: keeps resetting itself!!!!!?!?!?!?! GRRRRRR
        Me.RequestUidColumn.Visible = False

        Me.LoadCustomForms()

        ReloadStatusList()
        ReloadClientList()
        ReloadConsultantList()
        Me.ClientSiteUIDComboBox.Enabled = ConsoleUser.Management OrElse ConsoleUser.Administration

        Me.ClientSiteUIDColumn.DataPropertyName = STR_SiteName
        Me.AssignedToUIDColumn.DataPropertyName = STR_AssignedToName


        _refreshTimer = New System.Timers.Timer(1000)
        _refreshTimer.AutoReset = False
        _refreshTimer.SynchronizingObject = Me
        _initialised = True
    End Sub

    Private Sub RequestAdminSubForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private _settingFilter As Boolean
    Public Property Filter() As String
        Get
            Return Me.RequestBindingSource.Filter
        End Get
        Set(ByVal value As String)
            _settingFilter = True
            If String.IsNullOrEmpty(value) Then
                Me.RequestBindingSource.RemoveFilter()
            Else
                Me.RequestBindingSource.Filter = value
            End If
            TraceVerbose("Resetting Current Request")

            If Me._currentRequest IsNot Nothing Then
                Dim i As Integer = Me.RequestBindingSource.Find("RequestUID", _currentRequest.RequestUID)
                If i <> -1 Then
                    If i <> Me.RequestBindingSource.Position Then Me.RequestBindingSource.Position = i
                Else
                    Me.RequestBindingSource.Position = 0
                End If
            End If
            Me.SetCurrentRequest()
            Console.ConsoleData.SetEditorState(MODULENAME, False)
            _settingFilter = False
        End Set
    End Property

    Private _suspended As Boolean

    Private Delegate Sub SuspendBindingSourcesCallback()
    Private Sub SuspendBindingSources()
        If Not _initialised Then Return
        If Me.InvokeRequired OrElse Me.JobDataGridView.InvokeRequired Then
            Dim d As New SuspendBindingSourcesCallback(AddressOf SuspendBindingSources)
            Me.Invoke(d)
        Else
            TraceInfo(STR_Entered)
            _suspended = True
            Me.RequestBindingSource.SuspendBinding()
            Me.RequestDataGridView.SuspendLayout()
            TraceInfo(STR_Done)
        End If
    End Sub

    Private Delegate Sub ResumeBindingSourcesCallback()
    Private Sub ResumeBindingSources()
        If Not _initialised Then Return
        If Me.InvokeRequired OrElse Me.JobDataGridView.InvokeRequired Then
            TraceInfo(STR_Fired)
            Dim d As New ResumeBindingSourcesCallback(AddressOf ResumeBindingSources)
            Me.Invoke(d)
        Else
            TraceInfo(STR_Entered)
            Try
                Me.SuspendLayout()
                Me.RequestBindingSource.ResumeBinding()
                Me.RequestBindingSource.ResetBindings(False) ' force datagrid to rebind.
                'HACK: keeps resetting itself!!!!!?!?!?!?! GRRRRRR
                Me.RequestUidColumn.Visible = False
                Me.RequestDataGridView.ResumeLayout()
                Me.ReloadClientList()
                Me.ReloadConsultantList()
                Me.ReloadStatusList()

                _suspended = False
            Catch ex As Exception
                TraceError(ex)
            Finally
                Trace.Unindent()
                Me.ResumeLayout()
            End Try
            TraceInfo(STR_Done)
        End If
    End Sub

    Private Sub _consoleData_Refreshing(ByVal sender As Object, ByVal e As System.EventArgs) Handles _consoleData.Refreshing
        SuspendBindingSources()
    End Sub

    Private Sub _consoleData_Refreshed(ByVal sender As Object, ByVal e As ConsoleDataRefreshEventArgs) Handles _consoleData.Refreshed
        If _refreshTimer IsNot Nothing Then _refreshTimer.Start()
    End Sub

    Private Sub _refreshTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles _refreshTimer.Elapsed
        TraceVerbose(STR_Fired)
        ResumeBindingSources()
    End Sub

    Private Delegate Sub RefreshBindingSourcesDelegate()
    Private Sub RefreshBindingSources()
        If Not _initialised Then Return
        If Me.InvokeRequired OrElse Me.RequestDataGridView.InvokeRequired Then
            Dim d As New RefreshBindingSourcesDelegate(AddressOf RefreshBindingSources)
            Me.BeginInvoke(d)
        Else
            TraceInfo("RequestAdminSubForm RefreshBindingSources")
            Trace.Indent()
            TraceVerbose("Refreshing RequestBindingSource")
            Me.RequestBindingSource.ResetBindings(False)

            'TraceVerbose("Refreshing JobStatusBindingSource")
            'Me.JobStatusBindingSource.ResetBindings(False)

            TraceVerbose("Refreshing JobBindingSource")
            Me.JobBindingSource.ResetBindings(False)

            Me.ReloadClientList()
            Me.ReloadConsultantList()
            Me.ReloadStatusList()

            Me.RequestDataGridView.Refresh()
            Trace.Unindent()
            TraceInfo("RequestAdminSubForm RefreshBindingSources Done ")
        End If
    End Sub

    Private _currentRequest As activiserDataSet.RequestRow

    Private _dirty As Boolean
    Private Sub RequestBindingSource_BindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles RequestBindingSource.BindingComplete
        If ConsoleUser Is Nothing Then Return
        If Not _initialised Then Return
        If _settingFilter Then Return
        If _suspended Then Return
        If Not e.BindingCompleteState = BindingCompleteState.Success Then Return

        If e.BindingCompleteContext = BindingCompleteContext.DataSourceUpdate Then
            TraceVerbose("DataSourceUpdate")

            _dirty = False

            If Me._currentRequest IsNot Nothing Then
                Dim i As Integer = Me.RequestBindingSource.Find("RequestUID", _currentRequest.RequestUID)
                If i <> -1 Then
                    If i <> Me.RequestBindingSource.Position Then Me.RequestBindingSource.Position = i
                Else
                    Me.RequestBindingSource.Position = 0
                End If
            Else
                Me.SetCurrentRequest()
                Console.ConsoleData.SetEditorState(MODULENAME, False)
            End If
            Return
        ElseIf e.BindingCompleteContext = BindingCompleteContext.ControlUpdate Then

            If Me.RequestBindingSource.Position <> -1 Then
                TraceVerbose("ControlUpdate: Control = {1}, RequestBindingSource.Position = {0}", Me.RequestBindingSource.Position, e.Binding.Control.Name)
                Dim drv As DataRowView = TryCast(Me.RequestBindingSource.Current, DataRowView)
                If drv IsNot Nothing Then
                    If drv.IsEdit AndAlso ThreadingDelegates.ControlFocused(Me, e.Binding.Control) Then
                        TraceVerbose("control binding complete, control = {0}, field = {1}", e.Binding.Control.Name, e.Binding.BindingMemberInfo.BindingField)
                        Console.ConsoleData.SetEditorState(MODULENAME, True)
                    Else
                        Console.ConsoleData.SetEditorState(MODULENAME, False)
                    End If
                    _dirty = drv.Row.RowState <> DataRowState.Unchanged AndAlso DataRowHasChanges(drv.Row)

                    If _dirty Then
                        Console.ConsoleData.NeedRefresh()
                    End If
                    ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, _dirty)
                Else
                    ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, False)
                    Console.ConsoleData.SetEditorState(MODULENAME, False)
                End If
            Else
                TraceVerbose("ControlUpdate: RequestBindingSource.Position = {0}", -1)
                ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, False)
                Console.ConsoleData.SetEditorState(MODULENAME, False)
            End If
        End If
    End Sub

    Private Delegate Sub ValidateMeDelegate()
    Private Sub ValidateMe()
        TraceVerbose(STR_Fired)
        Me.Validate()
    End Sub

    Private _inCurrentChanged As Boolean
    Private Sub RequestBindingSource_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestBindingSource.CurrentChanged
        If _inCurrentChanged Then
            TraceVerbose(STR_Fired & " (Ignored)")
            Return
        End If
        _inCurrentChanged = True
        TraceVerbose(STR_Entered)
        Try
            If Me._currentRequest Is Nothing Then
                TraceVerbose("No current request")
                Return
            End If
            Dim d As New ValidateMeDelegate(AddressOf ValidateMe)
            Me.Invoke(d)
            TraceVerbose("Validate OK")

            Dim drv As DataRowView = TryCast(Me.RequestBindingSource.Current, DataRowView)
            If drv IsNot Nothing Then
                If drv.IsEdit Then
                    _dirty = drv.Row.RowState <> DataRowState.Unchanged AndAlso DataRowHasChanges(drv.Row)
                    If _dirty Then
                        ThreadingDelegates.BindingSourceEndEdit(Me, Me.RequestBindingSource)
                        TraceVerbose("Edit Ended")
                    Else
                        ThreadingDelegates.BindingSourceCancelEdit(Me, Me.RequestBindingSource)
                        TraceVerbose("Edit Cancelled")
                    End If

                End If
            End If

        Catch ex As NoNullAllowedException
            TraceError(ex)

            Throw
        Catch ex As Exception
            TraceError(ex)
            'Debug.WriteLine(ex.ToString)
        Finally

            _inCurrentChanged = False
        End Try
        TraceVerbose(STR_Done)
    End Sub

    Private _inlistChanged As Boolean
    Private Sub RequestBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles RequestBindingSource.ListChanged
        If Not _initialised Then Return
        If _inlistChanged Then
            TraceVerbose(STR_Fired & " (Ignored)")
            Return
        End If
        Try
            TraceVerbose(STR_Entered)
            _inlistChanged = True
            If Me._currentRequest IsNot Nothing Then
                Dim i As Integer = Me.RequestBindingSource.Find("RequestUID", _currentRequest.RequestUID)
                If i <> -1 Then
                    Me.RequestBindingSource.Position = i
                Else
                    If Me.RequestBindingSource.Count > 0 Then
                        Me.RequestBindingSource.Position = 0
                    Else
                        Me.RequestBindingSource.Position = -1
                    End If
                End If
            Else
                If Me.RequestBindingSource.Count > 0 Then
                    Me.RequestBindingSource.Position = 0
                Else
                    Me.RequestBindingSource.Position = -1
                End If
            End If
            ConsoleData.MainForm.UpdateRequestStatusTree()
            ConsoleData.MainForm.UpdateRequestAdminButton()
        Catch ex As Exception
            TraceError(ex)
        Finally
            TraceVerbose(STR_Done)
            _inlistChanged = False
        End Try
    End Sub

    Private Sub RequestBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestBindingSource.PositionChanged
        If _suspended Then Return
        TraceVerbose("New position: {0}", Me.RequestBindingSource.Position)
        Me.SetCurrentRequest()
    End Sub

    Private Sub RequestDataGridView_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles RequestDataGridView.DataError
        e.ThrowException = False
    End Sub

    Private CustomFormPanels As New List(Of CustomFormPanel)
    Public Sub LoadCustomForms()
        For Each cfr As FormDefinition.FormRow In Console.ConsoleData.FormDefs.Form
            If cfr.ParentEntityName.ToUpperInvariant = "REQUEST" Then
                Dim tp As New TabPage(cfr.FormName)
                Dim cfp As New CustomFormPanel(cfr)
                If cfp IsNot Nothing Then
                    Try
                        cfp.Dock = DockStyle.Fill
                        tp.Controls.Add(cfp)
                        tp.Tag = cfp
                        CustomFormPanels.Add(cfp)
                        Me.TabControl.TabPages.Add(tp)
                    Catch

                    End Try
                End If

            End If
        Next
    End Sub

    Private Sub StatusChangeReasonLabel_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestStatusChangeReasonLabel.DoubleClick
        TraceVerbose("Statuschange accepted")
    End Sub

    Private Delegate Sub SetCurrentRequestDelegate()
    Private _settingCurrent As Boolean
    Private Sub SetCurrentRequest()
        If _settingCurrent Then Return
        If _suspended Then Return

        If Me.InvokeRequired Then
            TraceVerbose("Invoking on UI thread")
            Dim d As New SetCurrentRequestDelegate(AddressOf SetCurrentRequest)
            Me.Invoke(d)
            Return
        Else
            Try
                TraceInfo(STR_Entered)
                Trace.Indent()
                _settingCurrent = True
                Me.RequestStatusChangeReasonLabel.Visible = False
                Dim drv As DataRowView = TryCast(Me.RequestBindingSource.Current, DataRowView)
                If drv IsNot Nothing AndAlso drv.Row IsNot Nothing Then
                    _currentRequest = TryCast(drv.Row, activiserDataSet.RequestRow)
                    If _currentRequest IsNot Nothing Then
                        For Each cfp As CustomFormPanel In Me.CustomFormPanels
                            TraceVerbose("RequestAdminSubForm setting custom form parent for form {0}", cfp.Name)
                            cfp.Current = _currentRequest
                        Next

                        If Not _currentRequest.IsConsultantStatusIDNull AndAlso Not _currentRequest.IsRequestStatusIDNull AndAlso _currentRequest.ConsultantStatusID <> _currentRequest.RequestStatusID Then
                            'TraceVerbose("RequestAdminSubForm request status change {1}/{0}", _currentRequest.ConsultantStatusID, _currentRequest.RequestStatusID)
                            Dim statusChangeFilter As String = String.Format("RequestUID='{0}' AND Flag={1} AND JobStatusID=6", _currentRequest.RequestUID, _currentRequest.ConsultantStatusID)
                            Dim jrs() As activiserDataSet.JobRow = CType(Console.ConsoleData.CoreDataSet.Job.Select(statusChangeFilter, "ModifiedDateTime DESC"), activiserDataSet.JobRow())
                            If jrs IsNot Nothing AndAlso jrs.Length <> 0 Then
                                Me.RequestStatusChangeReasonLabel.Text = jrs(0).JobDetails
                            Else
                                Me.RequestStatusChangeReasonLabel.Text = Terminology.GetFormattedString(MODULENAME, "StatusChangeMessageTemplate", _currentRequest.RequestStatusRowByFK_Request_ConsultantStatus.Description)
                            End If
                            Me.RequestStatusChangeReasonLabel.Visible = True
                        Else
                            Me.RequestStatusChangeReasonLabel.Visible = False
                        End If
                    End If
                End If
            Catch ex As Exception
                TraceError(ex)
                Dim ed As New activiser.Library.DisplayException(ex, "Error setting current request")
                ed.Show()
            Finally
                Trace.Unindent()
                TraceInfo(STR_Done)
                _settingCurrent = False
            End Try
        End If
    End Sub

    Private Sub AcceptRequestStatusChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcceptRequestStatusChange.Click
        If Me._currentRequest IsNot Nothing Then
            Dim target As activiserDataSet.RequestRow = Me._currentRequest

            TraceVerbose(STR_Entered)
            Trace.Indent()
            target.RequestStatusID = target.ConsultantStatusID
            Console.ConsoleData.NeedRefresh()
            Console.ConsoleData.MainForm.UpdateRequestStatusTree()
            Console.ConsoleData.MainForm.UpdateRequestAdminButton()
            Me.RequestBindingSource.CurrencyManager.Refresh()
            SetCurrentRequest()
            ' Me.RequestBindingSource.ResetBindings(False)
            Dim i As Integer = Me.RequestBindingSource.Find("RequestUID", target.RequestUID)
            If i <> -1 Then
                If i <> Me.RequestBindingSource.Position Then Me.RequestBindingSource.Position = i
            Else
                If Me.RequestBindingSource.Count <> 0 Then
                    Me.RequestBindingSource.Position = 0
                Else
                    Me.RequestBindingSource.Position = -1
                End If
            End If
            'Me.Refresh()
            Trace.Unindent()
            TraceVerbose(STR_Done)
        End If
    End Sub

    Private Sub RejectRequestStatusChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectRequestStatusChange.Click
        If Me._currentRequest IsNot Nothing Then
            Dim target As activiserDataSet.RequestRow = _currentRequest
            target.ConsultantStatusID = target.RequestStatusID

            Console.ConsoleData.NeedRefresh()
            Console.ConsoleData.MainForm.UpdateRequestStatusTree()
            Console.ConsoleData.MainForm.UpdateRequestAdminButton()
        End If
    End Sub

    Private Sub RequestStatusIDComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestStatusIDComboBox.SelectionChangeCommitted
        If Not Me._currentRequest.IsConsultantStatusIDNull AndAlso (CInt(Me.RequestStatusIDComboBox.SelectedValue) <> Me._currentRequest.ConsultantStatusID) Then
            Dim target As activiserDataSet.RequestRow = _currentRequest
            target.RequestStatusID = CInt(Me.RequestStatusIDComboBox.SelectedValue)
            target.ConsultantStatusID = target.RequestStatusID

            Console.ConsoleData.NeedRefresh()
            Console.ConsoleData.MainForm.UpdateRequestStatusTree()
            Console.ConsoleData.MainForm.UpdateRequestAdminButton()
        End If
    End Sub

    Private Sub JobList_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles JobDataGridView.CellFormatting
        If sender Is Me.JobDataGridView Then
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

    Private Sub JobList_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles JobDataGridView.DataError
        Debug.WriteLine(e.Exception.ToString)
    End Sub

    Private Sub JobList_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles JobDataGridView.MouseDoubleClick
        Dim dgv As DataGridView = TryCast(sender, DataGridView)
        If dgv IsNot Nothing Then
            Dim drv As DataRowView = TryCast(dgv.SelectedRows(0).DataBoundItem, DataRowView)
            If drv IsNot Nothing Then
                Dim selectedJob As activiserDataSet.JobRow = TryCast(drv.Row, activiserDataSet.JobRow)
                If selectedJob IsNot Nothing Then
                    'mainform.jo
                    If selectedJob.JobStatusID = 0 Then
                        If MainForm.allIncompleteNode IsNot Nothing Then
                            MainForm.NavTreeJob.SelectedNode = MainForm.allIncompleteNode
                            MainForm.JobAdminSubForm.CurrentJob = selectedJob
                        End If
                    ElseIf selectedJob.JobStatusID < 5 Then
                        If MainForm.allCompleteNode IsNot Nothing Then
                            MainForm.NavTreeJob.SelectedNode = MainForm.allCompleteNode
                            MainForm.JobAdminSubForm.CurrentJob = selectedJob
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    'Private Sub RequestAdminSubForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
    '    'If Not _initialised Then Return

    '    'If Me.Visible Then
    '    '    Me.ReloadStatusList()
    '    '    RefreshBindingSources()
    '    'End If
    '    'Me.Enabled = Me.Visible
    'End Sub


    ''' <summary>
    ''' HACK: because binding doesn't always pick up changed data!
    ''' </summary>
    ''' <remarks></remarks>  
    Private Sub MakeDirty()
        If Not _initialised Then Return
        If _dirty Then Return ' already dirty!
        TraceVerbose("Marking request dirty")
        Console.ConsoleData.SetEditorState(MODULENAME, True)
        Console.ConsoleData.NeedRefresh()
        _dirty = True
        ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, _dirty)
        Me.Refresh()
        'Application.DoEvents()
    End Sub

    'Private Sub ClientSiteUIDComboBox_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.BindingContextChanged
    '    TraceVerbose("RequestAdminForm ClientSiteUIDComboBox_BindingContextChanged") ' : {0}", My.Application.Info.StackTrace)
    'End Sub

    'Private Sub ClientSiteUIDComboBox_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.DataSourceChanged
    '    TraceVerbose("RequestAdminForm ClientSiteUIDComboBox_DataSourceChanged : {0}", ClientSiteUIDComboBox.DataSource.ToString())
    'End Sub

    'Private Sub ClientSiteUIDComboBox_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.Disposed
    '    TraceVerbose("RequestAdminForm ClientSiteUIDComboBox_Disposed") ' : {0}", My.Application.Info.StackTrace)
    'End Sub

    'Private Sub ClientSiteUIDComboBox_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.EnabledChanged
    '    TraceVerbose("RequestAdminForm ClientSiteUIDComboBox_EnabledChanged : {0}", ClientSiteUIDComboBox.Enabled.ToString)
    'End Sub

    'Private Sub ClientSiteUIDComboBox_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.Move
    '    TraceVerbose("RequestAdminForm ClientSiteUIDComboBox_Move") ' : {0}", ClientSiteUIDComboBox.Location.ToString())
    'End Sub

    'Private Sub ClientSiteUIDComboBox_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.VisibleChanged
    '    TraceVerbose("RequestAdminForm ClientSiteUIDComboBox_VisibleChanged : {0}", Me.ClientSiteUIDComboBox.Visible)
    'End Sub

    Private Sub Control_Value_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientSiteUIDComboBox.SelectionChangeCommitted, AssignedToUIDComboBox.SelectionChangeCommitted
        Me.MakeDirty()
    End Sub

    Private Sub Control_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles NextActionDatePicker.PropertyChanged, CompletedDatePicker.PropertyChanged
        Me.MakeDirty()
    End Sub

    Private Sub DateTimePicker_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
        If _currentRequest IsNot Nothing Then
        End If
        Me.MakeDirty()
    End Sub

    ' Need to check ModifiedChanged and TextChanged for text boxes
    Private Sub TextBox_ModifiedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortDescriptionTextBox.ModifiedChanged, LongDescriptionTextBox.ModifiedChanged
        If Not _initialised Then Return
        Dim tb As TextBoxBase = TryCast(sender, TextBoxBase)
        If tb IsNot Nothing Then
            If tb.Modified Then
                Me.MakeDirty()
            End If
        End If
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortDescriptionTextBox.TextChanged, LongDescriptionTextBox.TextChanged
        If Not _initialised Then Return
        Dim tb As TextBoxBase = TryCast(sender, TextBoxBase)
        If tb IsNot Nothing Then
            If tb.Modified Then
                Me.MakeDirty()
            End If
        End If
    End Sub

    Private Sub ToolStripUndoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripUndoButton.Click
        Dim drv As DataRowView = TryCast(Me.RequestBindingSource.Current, DataRowView)
        If drv IsNot Nothing Then
            If drv.IsEdit Then
                If MessageBox.Show("Undo all changes made to this record?", My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, Nothing, False) = DialogResult.Yes Then
                    Me.RequestBindingSource.CancelEdit()
                    drv.Row.RejectChanges()
                    _dirty = False
                    ThreadingDelegates.ToolStripItemEnabled(Me, ToolStripUndoButton, False)
                End If
            End If
        End If

    End Sub

    Private Sub RequestBindingSource_CurrentItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestBindingSource.CurrentItemChanged
        If sender IsNot Me.RequestBindingSource Then Return
        TraceVerbose(STR_Fired)
    End Sub

    Private Sub ConsultantStatusIDComboBox_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsultantStatusIDComboBox.SelectedValueChanged
        If Not Me._initialised Then Return
        If Not Me.ConsultantStatusIDComboBox.Focused Then Return
        If _currentRequest IsNot Nothing Then
            Dim target As activiserDataSet.RequestRow = _currentRequest
            If ConsultantStatusIDComboBox.SelectedValue IsNot Nothing Then
                If target.IsConsultantStatusIDNull OrElse target.ConsultantStatusID <> CInt(ConsultantStatusIDComboBox.SelectedValue) Then
                    target.ConsultantStatusID = CInt(ConsultantStatusIDComboBox.SelectedValue)
                End If
            Else
                If Not target.IsConsultantStatusIDNull Then
                    target.SetConsultantStatusIDNull()
                End If
            End If

            Me.RequestBindingSource.CurrencyManager.Refresh()
            Me.SetCurrentRequest()
            Dim i As Integer = Me.RequestBindingSource.Find("RequestUID", target.RequestUID)
            If i <> -1 Then
                If i <> Me.RequestBindingSource.Position Then Me.RequestBindingSource.Position = i
            Else
                If Me.RequestBindingSource.Count <> 0 Then
                    Me.RequestBindingSource.Position = 0
                Else
                    Me.RequestBindingSource.Position = -1
                End If
            End If
        End If
    End Sub

    Private Sub ConsultantStatusIDComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsultantStatusIDComboBox.SelectionChangeCommitted
        If Not Me._initialised Then Return
        If _currentRequest IsNot Nothing Then
            Dim target As activiserDataSet.RequestRow = _currentRequest

            If ConsultantStatusIDComboBox.SelectedValue IsNot Nothing Then
                Dim newStatus As Integer = CInt(ConsultantStatusIDComboBox.SelectedValue)
                target.ConsultantStatusID = newStatus
                ' _currentRequest.ConsultantStatusID = CInt(ConsultantStatusIDComboBox.SelectedValue)
            Else
                target.SetConsultantStatusIDNull()
                ' _currentRequest.SetConsultantStatusIDNull()
            End If
        End If
    End Sub

End Class
