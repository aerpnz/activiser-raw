<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobAdminSubForm
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobAdminSubForm))
        Dim ReturnDateLabel As System.Windows.Forms.Label
        Dim SignatoryLabel As System.Windows.Forms.Label
        Dim EmailLabel As System.Windows.Forms.Label
        Dim ConsultantUIDLabel As System.Windows.Forms.Label
        Dim RequestUIDLabel As System.Windows.Forms.Label
        Dim FinishTimeLabel As System.Windows.Forms.Label
        Dim ClientSiteUIDLabel As System.Windows.Forms.Label
        Dim JobDateLabel As System.Windows.Forms.Label
        Dim MinsTravelledLabel As System.Windows.Forms.Label
        Dim RequestStatusIDLabel As System.Windows.Forms.Label
        Dim ConsultantStatusIDLabel As System.Windows.Forms.Label
        Dim NextActionDateLabel As System.Windows.Forms.Label
        Dim CompletedDateLabel As System.Windows.Forms.Label
        Dim RequestNumberLabel As System.Windows.Forms.Label
        Dim ReqSiteContactLabel As System.Windows.Forms.Label
        Dim RequestModifiedTimeLabel As System.Windows.Forms.Label
        Dim RequestCreatedTimeLabel As System.Windows.Forms.Label
        Dim RequestDescriptionLabel As System.Windows.Forms.Label
        Dim RequestShortDescriptionLabel As System.Windows.Forms.Label
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GeneralTabSplitContainer = New System.Windows.Forms.SplitContainer
        Me.SignatureGroup = New System.Windows.Forms.GroupBox
        Me.emailStatus = New System.Windows.Forms.CheckBox
        Me.ResendEmailMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ResendEmailMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReturnDatePicker = New activiser.Library.DateTimePicker
        Me.JobBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CoreDataSet = New activiser.Library.activiserWebService.activiserDataSet
        Me.SignatoryTextBox = New System.Windows.Forms.TextBox
        Me.SignatureViewer = New activiser.Console.SignatureViewer
        Me.EmailTextBox = New System.Windows.Forms.TextBox
        Me.JobInfoGroup = New System.Windows.Forms.GroupBox
        Me.JobFinishDateTimePicker = New activiser.Library.DateTimePicker
        Me.JobStartDateTimePicker = New activiser.Library.DateTimePicker
        Me.EnableRequestChange = New System.Windows.Forms.CheckBox
        Me.EnableClientSiteChange = New System.Windows.Forms.CheckBox
        Me.EnableConsultantChange = New System.Windows.Forms.CheckBox
        Me.RequestUIDComboBox = New System.Windows.Forms.ComboBox
        Me.ConsultantUIDComboBox = New System.Windows.Forms.ComboBox
        Me.ClientSiteUIDComboBox = New System.Windows.Forms.ComboBox
        Me.MinsTravelledNumericUpDown = New activiser.Console.DurationPicker
        Me.JobNotesGroup = New System.Windows.Forms.GroupBox
        Me.JobDetailsTextBox = New System.Windows.Forms.TextBox
        Me.ApprovalGroup = New System.Windows.Forms.GroupBox
        Me.JobFlagGroup = New activiser.Console.JobFlagGroup
        Me.RequestStatusAprrovalGroupBox = New System.Windows.Forms.GroupBox
        Me.RequestStatusChangeReasonLabel = New System.Windows.Forms.Label
        Me.RequestStatusChangeMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AcceptRequestStatusChange = New System.Windows.Forms.ToolStripMenuItem
        Me.RejectRequestStatusChange = New System.Windows.Forms.ToolStripMenuItem
        Me.RequestStatusIDComboBox = New System.Windows.Forms.ComboBox
        Me.RequestBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CompletedDateDateTimePicker = New activiser.Library.DateTimePicker
        Me.ConsultantStatusIDComboBox = New System.Windows.Forms.ComboBox
        Me.NextActionDateDateTimePicker = New activiser.Library.DateTimePicker
        Me.UpdateStatusButton = New System.Windows.Forms.Button
        Me.NoteTabSplitContainer = New System.Windows.Forms.SplitContainer
        Me.ConsultantNotesLabel = New System.Windows.Forms.GroupBox
        Me.ConsultantNotesTextBox = New System.Windows.Forms.TextBox
        Me.EquipmentNotesLabel = New System.Windows.Forms.GroupBox
        Me.EquipmentNotesTextBox = New System.Windows.Forms.TextBox
        Me.RequestHeaderSplitContainer = New System.Windows.Forms.SplitContainer
        Me.RequestAssignedToLabel = New System.Windows.Forms.Label
        Me.RequestNumberTextBox = New System.Windows.Forms.TextBox
        Me.RequestAssignedToComboBox = New System.Windows.Forms.ComboBox
        Me.ContactTextBox = New System.Windows.Forms.TextBox
        Me.RequestCreatedTime = New System.Windows.Forms.TextBox
        Me.RequestModifiedTime = New System.Windows.Forms.TextBox
        Me.JobDataGridView = New System.Windows.Forms.DataGridView
        Me.RequestNumberColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ConsultantNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ClientSiteNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobStartTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobFinishTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobDetailsColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ReturnDateColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobNumberColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobListFilterMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FilterByDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuAllDates = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuDateToday = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuYesterday = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuPastWeek = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuPastMonth = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuConsultant = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuConsultantNone = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuConsultantInclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuConsultantExclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClient = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClientNone = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClientInclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClientExclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuRequest = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuRequestNone = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuRequestOnlySelected = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuRequestExcludeSelected = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClearAll = New System.Windows.Forms.ToolStripMenuItem
        Me.JobInfoTabControl = New System.Windows.Forms.TabControl
        Me.JobActivitiesTab = New System.Windows.Forms.TabPage
        Me.JobNotesTab = New System.Windows.Forms.TabPage
        Me.RequestTab = New System.Windows.Forms.TabPage
        Me.RequestInformationLabel = New System.Windows.Forms.GroupBox
        Me.RequestDescriptionTextBox = New System.Windows.Forms.TextBox
        Me.ShortDescriptionPanel = New System.Windows.Forms.Panel
        Me.RequestShortDescriptionTextBox = New System.Windows.Forms.TextBox
        Me.GpsTab = New System.Windows.Forms.TabPage
        Me.GpsBrowser = New System.Windows.Forms.WebBrowser
        Me.JobToolbar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripUndoButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.JobToolbarSaveButton = New System.Windows.Forms.ToolStripButton
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton
        Me.SupportInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.StatusBarRequestNumber = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarSiteName = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarConsultant = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarHaveSignature = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarHasNotes = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarHasEquipment = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.JobGridTabSplitter = New System.Windows.Forms.Splitter
        Me.JobStatusContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.JobStatusChangeToDraftButton = New System.Windows.Forms.ToolStripMenuItem
        Me.JobStatusChangeToCompleteButton = New System.Windows.Forms.ToolStripMenuItem
        Me.JobStatusChangeToSignedButton = New System.Windows.Forms.ToolStripMenuItem
        Me.JobStatusChangeToStatusChangeButton = New System.Windows.Forms.ToolStripMenuItem
        ReturnDateLabel = New System.Windows.Forms.Label
        SignatoryLabel = New System.Windows.Forms.Label
        EmailLabel = New System.Windows.Forms.Label
        ConsultantUIDLabel = New System.Windows.Forms.Label
        RequestUIDLabel = New System.Windows.Forms.Label
        FinishTimeLabel = New System.Windows.Forms.Label
        ClientSiteUIDLabel = New System.Windows.Forms.Label
        JobDateLabel = New System.Windows.Forms.Label
        MinsTravelledLabel = New System.Windows.Forms.Label
        RequestStatusIDLabel = New System.Windows.Forms.Label
        ConsultantStatusIDLabel = New System.Windows.Forms.Label
        NextActionDateLabel = New System.Windows.Forms.Label
        CompletedDateLabel = New System.Windows.Forms.Label
        RequestNumberLabel = New System.Windows.Forms.Label
        ReqSiteContactLabel = New System.Windows.Forms.Label
        RequestModifiedTimeLabel = New System.Windows.Forms.Label
        RequestCreatedTimeLabel = New System.Windows.Forms.Label
        RequestDescriptionLabel = New System.Windows.Forms.Label
        RequestShortDescriptionLabel = New System.Windows.Forms.Label
        Me.GeneralTabSplitContainer.Panel1.SuspendLayout()
        Me.GeneralTabSplitContainer.Panel2.SuspendLayout()
        Me.GeneralTabSplitContainer.SuspendLayout()
        Me.SignatureGroup.SuspendLayout()
        Me.ResendEmailMenu.SuspendLayout()
        CType(Me.JobBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SignatureViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.JobInfoGroup.SuspendLayout()
        Me.JobNotesGroup.SuspendLayout()
        Me.ApprovalGroup.SuspendLayout()
        Me.RequestStatusAprrovalGroupBox.SuspendLayout()
        Me.RequestStatusChangeMenu.SuspendLayout()
        CType(Me.RequestBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NoteTabSplitContainer.Panel1.SuspendLayout()
        Me.NoteTabSplitContainer.Panel2.SuspendLayout()
        Me.NoteTabSplitContainer.SuspendLayout()
        Me.ConsultantNotesLabel.SuspendLayout()
        Me.EquipmentNotesLabel.SuspendLayout()
        Me.RequestHeaderSplitContainer.Panel1.SuspendLayout()
        Me.RequestHeaderSplitContainer.Panel2.SuspendLayout()
        Me.RequestHeaderSplitContainer.SuspendLayout()
        CType(Me.JobDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.JobListFilterMenu.SuspendLayout()
        Me.JobInfoTabControl.SuspendLayout()
        Me.JobActivitiesTab.SuspendLayout()
        Me.JobNotesTab.SuspendLayout()
        Me.RequestTab.SuspendLayout()
        Me.RequestInformationLabel.SuspendLayout()
        Me.ShortDescriptionPanel.SuspendLayout()
        Me.GpsTab.SuspendLayout()
        CType(Me.JobToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.JobToolbar.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.JobStatusContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'GeneralTabSplitContainer
        '
        resources.ApplyResources(Me.GeneralTabSplitContainer, "GeneralTabSplitContainer")
        Me.GeneralTabSplitContainer.Name = "GeneralTabSplitContainer"
        '
        'GeneralTabSplitContainer.Panel1
        '
        resources.ApplyResources(Me.GeneralTabSplitContainer.Panel1, "GeneralTabSplitContainer.Panel1")
        Me.GeneralTabSplitContainer.Panel1.Controls.Add(Me.SignatureGroup)
        Me.GeneralTabSplitContainer.Panel1.Controls.Add(Me.JobInfoGroup)
        '
        'GeneralTabSplitContainer.Panel2
        '
        Me.GeneralTabSplitContainer.Panel2.Controls.Add(Me.JobNotesGroup)
        Me.GeneralTabSplitContainer.Panel2.Controls.Add(Me.ApprovalGroup)
        '
        'SignatureGroup
        '
        Me.SignatureGroup.Controls.Add(Me.emailStatus)
        Me.SignatureGroup.Controls.Add(ReturnDateLabel)
        Me.SignatureGroup.Controls.Add(Me.ReturnDatePicker)
        Me.SignatureGroup.Controls.Add(Me.SignatoryTextBox)
        Me.SignatureGroup.Controls.Add(Me.SignatureViewer)
        Me.SignatureGroup.Controls.Add(SignatoryLabel)
        Me.SignatureGroup.Controls.Add(EmailLabel)
        Me.SignatureGroup.Controls.Add(Me.EmailTextBox)
        resources.ApplyResources(Me.SignatureGroup, "SignatureGroup")
        Me.SignatureGroup.Name = "SignatureGroup"
        Me.SignatureGroup.TabStop = False
        '
        'emailStatus
        '
        Me.emailStatus.AutoCheck = False
        resources.ApplyResources(Me.emailStatus, "emailStatus")
        Me.emailStatus.ContextMenuStrip = Me.ResendEmailMenu
        Me.emailStatus.Name = "emailStatus"
        Me.emailStatus.ThreeState = True
        Me.ToolTipProvider.SetToolTip(Me.emailStatus, resources.GetString("emailStatus.ToolTip"))
        Me.emailStatus.UseVisualStyleBackColor = True
        '
        'ResendEmailMenu
        '
        Me.ResendEmailMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResendEmailMenuItem})
        Me.ResendEmailMenu.Name = "ResendEmailMenu"
        resources.ApplyResources(Me.ResendEmailMenu, "ResendEmailMenu")
        '
        'ResendEmailMenuItem
        '
        Me.ResendEmailMenuItem.Name = "ResendEmailMenuItem"
        resources.ApplyResources(Me.ResendEmailMenuItem, "ResendEmailMenuItem")
        '
        'ReturnDateLabel
        '
        resources.ApplyResources(ReturnDateLabel, "ReturnDateLabel")
        ReturnDateLabel.Name = "ReturnDateLabel"
        '
        'ReturnDatePicker
        '
        resources.ApplyResources(Me.ReturnDatePicker, "ReturnDatePicker")
        Me.ReturnDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.JobBindingSource, "ReturnDate", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ReturnDatePicker.Name = "ReturnDatePicker"
        Me.ReturnDatePicker.ShowTime = False
        '
        'JobBindingSource
        '
        Me.JobBindingSource.DataMember = "Job"
        Me.JobBindingSource.DataSource = Me.CoreDataSet
        Me.JobBindingSource.Filter = "JobStatusID<5"
        '
        'CoreDataSet
        '
        Me.CoreDataSet.DataSetName = "activiserCoreDataSet"
        Me.CoreDataSet.Locale = New System.Globalization.CultureInfo(Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo)
        Me.CoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SignatoryTextBox
        '
        resources.ApplyResources(Me.SignatoryTextBox, "SignatoryTextBox")
        Me.SignatoryTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JobBindingSource, "Signatory", True, System.Windows.Forms.DataSourceUpdateMode.Never))
        Me.SignatoryTextBox.Name = "SignatoryTextBox"
        Me.SignatoryTextBox.ReadOnly = True
        '
        'SignatureViewer
        '
        resources.ApplyResources(Me.SignatureViewer, "SignatureViewer")
        Me.SignatureViewer.BackColor = System.Drawing.SystemColors.Info
        Me.SignatureViewer.BorderColor = System.Drawing.Color.Black
        Me.SignatureViewer.JobUid = New System.Guid("00000000-0000-0000-0000-000000000000")
        Me.SignatureViewer.Name = "SignatureViewer"
        Me.SignatureViewer.TabStop = False
        Me.SignatureViewer.Value = Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo
        '
        'SignatoryLabel
        '
        resources.ApplyResources(SignatoryLabel, "SignatoryLabel")
        SignatoryLabel.Name = "SignatoryLabel"
        '
        'EmailLabel
        '
        resources.ApplyResources(EmailLabel, "EmailLabel")
        EmailLabel.ContextMenuStrip = Me.ResendEmailMenu
        EmailLabel.Name = "EmailLabel"
        '
        'EmailTextBox
        '
        resources.ApplyResources(Me.EmailTextBox, "EmailTextBox")
        Me.EmailTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JobBindingSource, "Email", True))
        Me.EmailTextBox.Name = "EmailTextBox"
        '
        'JobInfoGroup
        '
        Me.JobInfoGroup.Controls.Add(Me.JobFinishDateTimePicker)
        Me.JobInfoGroup.Controls.Add(Me.JobStartDateTimePicker)
        Me.JobInfoGroup.Controls.Add(Me.EnableRequestChange)
        Me.JobInfoGroup.Controls.Add(Me.EnableClientSiteChange)
        Me.JobInfoGroup.Controls.Add(Me.EnableConsultantChange)
        Me.JobInfoGroup.Controls.Add(ConsultantUIDLabel)
        Me.JobInfoGroup.Controls.Add(RequestUIDLabel)
        Me.JobInfoGroup.Controls.Add(FinishTimeLabel)
        Me.JobInfoGroup.Controls.Add(ClientSiteUIDLabel)
        Me.JobInfoGroup.Controls.Add(Me.RequestUIDComboBox)
        Me.JobInfoGroup.Controls.Add(Me.ConsultantUIDComboBox)
        Me.JobInfoGroup.Controls.Add(JobDateLabel)
        Me.JobInfoGroup.Controls.Add(Me.ClientSiteUIDComboBox)
        Me.JobInfoGroup.Controls.Add(MinsTravelledLabel)
        Me.JobInfoGroup.Controls.Add(Me.MinsTravelledNumericUpDown)
        resources.ApplyResources(Me.JobInfoGroup, "JobInfoGroup")
        Me.JobInfoGroup.Name = "JobInfoGroup"
        Me.JobInfoGroup.TabStop = False
        '
        'JobFinishDateTimePicker
        '
        resources.ApplyResources(Me.JobFinishDateTimePicker, "JobFinishDateTimePicker")
        Me.JobFinishDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.JobBindingSource, "FinishTime", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.JobFinishDateTimePicker.Name = "JobFinishDateTimePicker"
        '
        'JobStartDateTimePicker
        '
        resources.ApplyResources(Me.JobStartDateTimePicker, "JobStartDateTimePicker")
        Me.JobStartDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.JobBindingSource, "StartTime", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.JobStartDateTimePicker.Name = "JobStartDateTimePicker"
        '
        'EnableRequestChange
        '
        resources.ApplyResources(Me.EnableRequestChange, "EnableRequestChange")
        Me.EnableRequestChange.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.activiser.Console.My.MySettings.Default, "jobsAllowEdits", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.EnableRequestChange.Name = "EnableRequestChange"
        Me.EnableRequestChange.UseVisualStyleBackColor = True
        Me.EnableRequestChange.Visible = Global.activiser.Console.My.MySettings.Default.JobsAllowEdits
        '
        'EnableClientSiteChange
        '
        resources.ApplyResources(Me.EnableClientSiteChange, "EnableClientSiteChange")
        Me.EnableClientSiteChange.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.activiser.Console.My.MySettings.Default, "jobsAllowEdits", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.EnableClientSiteChange.Name = "EnableClientSiteChange"
        Me.EnableClientSiteChange.UseVisualStyleBackColor = True
        Me.EnableClientSiteChange.Visible = Global.activiser.Console.My.MySettings.Default.JobsAllowEdits
        '
        'EnableConsultantChange
        '
        resources.ApplyResources(Me.EnableConsultantChange, "EnableConsultantChange")
        Me.EnableConsultantChange.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.activiser.Console.My.MySettings.Default, "jobsAllowEdits", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.EnableConsultantChange.Name = "EnableConsultantChange"
        Me.EnableConsultantChange.UseVisualStyleBackColor = True
        Me.EnableConsultantChange.Visible = Global.activiser.Console.My.MySettings.Default.JobsAllowEdits
        '
        'ConsultantUIDLabel
        '
        resources.ApplyResources(ConsultantUIDLabel, "ConsultantUIDLabel")
        ConsultantUIDLabel.Name = "ConsultantUIDLabel"
        '
        'RequestUIDLabel
        '
        resources.ApplyResources(RequestUIDLabel, "RequestUIDLabel")
        RequestUIDLabel.Name = "RequestUIDLabel"
        '
        'FinishTimeLabel
        '
        resources.ApplyResources(FinishTimeLabel, "FinishTimeLabel")
        FinishTimeLabel.Name = "FinishTimeLabel"
        '
        'ClientSiteUIDLabel
        '
        resources.ApplyResources(ClientSiteUIDLabel, "ClientSiteUIDLabel")
        ClientSiteUIDLabel.Name = "ClientSiteUIDLabel"
        '
        'RequestUIDComboBox
        '
        resources.ApplyResources(Me.RequestUIDComboBox, "RequestUIDComboBox")
        Me.RequestUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.JobBindingSource, "RequestUID", True))
        Me.RequestUIDComboBox.Name = "RequestUIDComboBox"
        '
        'ConsultantUIDComboBox
        '
        resources.ApplyResources(Me.ConsultantUIDComboBox, "ConsultantUIDComboBox")
        Me.ConsultantUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.JobBindingSource, "ConsultantUID", True))
        Me.ConsultantUIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ConsultantUIDComboBox.Name = "ConsultantUIDComboBox"
        '
        'JobDateLabel
        '
        resources.ApplyResources(JobDateLabel, "JobDateLabel")
        JobDateLabel.Name = "JobDateLabel"
        '
        'ClientSiteUIDComboBox
        '
        resources.ApplyResources(Me.ClientSiteUIDComboBox, "ClientSiteUIDComboBox")
        Me.ClientSiteUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.JobBindingSource, "ClientSiteUID", True))
        Me.ClientSiteUIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ClientSiteUIDComboBox.Name = "ClientSiteUIDComboBox"
        '
        'MinsTravelledLabel
        '
        resources.ApplyResources(MinsTravelledLabel, "MinsTravelledLabel")
        MinsTravelledLabel.Name = "MinsTravelledLabel"
        '
        'MinsTravelledNumericUpDown
        '
        resources.ApplyResources(Me.MinsTravelledNumericUpDown, "MinsTravelledNumericUpDown")
        Me.MinsTravelledNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.JobBindingSource, "MinutesTravelled", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.MinsTravelledNumericUpDown.MinimumSize = New System.Drawing.Size(140, 22)
        Me.MinsTravelledNumericUpDown.Name = "MinsTravelledNumericUpDown"
        Me.MinsTravelledNumericUpDown.Value = 0
        '
        'JobNotesGroup
        '
        Me.JobNotesGroup.Controls.Add(Me.JobDetailsTextBox)
        resources.ApplyResources(Me.JobNotesGroup, "JobNotesGroup")
        Me.JobNotesGroup.Name = "JobNotesGroup"
        Me.JobNotesGroup.TabStop = False
        '
        'JobDetailsTextBox
        '
        Me.JobDetailsTextBox.AcceptsReturn = True
        Me.JobDetailsTextBox.AcceptsTab = True
        Me.JobDetailsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.JobDetailsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "jobsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.JobDetailsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JobBindingSource, "JobDetails", True))
        Me.JobDetailsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("WordWrap", Global.activiser.Console.My.MySettings.Default, "WordWrapJobDetails", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.JobDetailsTextBox, "JobDetailsTextBox")
        Me.JobDetailsTextBox.Name = "JobDetailsTextBox"
        Me.JobDetailsTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.JobsReadOnly
        Me.JobDetailsTextBox.WordWrap = Global.activiser.Console.My.MySettings.Default.WordWrapJobDetails
        '
        'ApprovalGroup
        '
        Me.ApprovalGroup.BackColor = System.Drawing.SystemColors.Control
        Me.ApprovalGroup.Controls.Add(Me.JobFlagGroup)
        Me.ApprovalGroup.Controls.Add(Me.RequestStatusAprrovalGroupBox)
        Me.ApprovalGroup.Controls.Add(Me.UpdateStatusButton)
        resources.ApplyResources(Me.ApprovalGroup, "ApprovalGroup")
        Me.ApprovalGroup.Name = "ApprovalGroup"
        Me.ApprovalGroup.TabStop = False
        '
        'JobFlagGroup
        '
        resources.ApplyResources(Me.JobFlagGroup, "JobFlagGroup")
        Me.JobFlagGroup.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.JobBindingSource, "Flag", True))
        Me.JobFlagGroup.Name = "JobFlagGroup"
        '
        'RequestStatusAprrovalGroupBox
        '
        resources.ApplyResources(Me.RequestStatusAprrovalGroupBox, "RequestStatusAprrovalGroupBox")
        Me.RequestStatusAprrovalGroupBox.Controls.Add(Me.RequestStatusChangeReasonLabel)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(RequestStatusIDLabel)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(Me.RequestStatusIDComboBox)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(Me.CompletedDateDateTimePicker)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(ConsultantStatusIDLabel)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(NextActionDateLabel)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(Me.ConsultantStatusIDComboBox)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(Me.NextActionDateDateTimePicker)
        Me.RequestStatusAprrovalGroupBox.Controls.Add(CompletedDateLabel)
        Me.RequestStatusAprrovalGroupBox.Name = "RequestStatusAprrovalGroupBox"
        Me.RequestStatusAprrovalGroupBox.TabStop = False
        '
        'RequestStatusChangeReasonLabel
        '
        resources.ApplyResources(Me.RequestStatusChangeReasonLabel, "RequestStatusChangeReasonLabel")
        Me.RequestStatusChangeReasonLabel.BackColor = System.Drawing.SystemColors.Highlight
        Me.RequestStatusChangeReasonLabel.ContextMenuStrip = Me.RequestStatusChangeMenu
        Me.RequestStatusChangeReasonLabel.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.RequestStatusChangeReasonLabel.Name = "RequestStatusChangeReasonLabel"
        '
        'RequestStatusChangeMenu
        '
        Me.RequestStatusChangeMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AcceptRequestStatusChange, Me.RejectRequestStatusChange})
        Me.RequestStatusChangeMenu.Name = "RequestStatusChangeMenu"
        resources.ApplyResources(Me.RequestStatusChangeMenu, "RequestStatusChangeMenu")
        '
        'AcceptRequestStatusChange
        '
        Me.AcceptRequestStatusChange.Image = Global.activiser.Console.My.Resources.Resources.OK
        resources.ApplyResources(Me.AcceptRequestStatusChange, "AcceptRequestStatusChange")
        Me.AcceptRequestStatusChange.Name = "AcceptRequestStatusChange"
        '
        'RejectRequestStatusChange
        '
        Me.RejectRequestStatusChange.Image = Global.activiser.Console.My.Resources.Resources.NoAction
        resources.ApplyResources(Me.RejectRequestStatusChange, "RejectRequestStatusChange")
        Me.RejectRequestStatusChange.Name = "RejectRequestStatusChange"
        '
        'RequestStatusIDLabel
        '
        resources.ApplyResources(RequestStatusIDLabel, "RequestStatusIDLabel")
        RequestStatusIDLabel.Name = "RequestStatusIDLabel"
        '
        'RequestStatusIDComboBox
        '
        resources.ApplyResources(Me.RequestStatusIDComboBox, "RequestStatusIDComboBox")
        Me.RequestStatusIDComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.RequestStatusIDComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RequestStatusIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "RequestStatusID", True))
        Me.RequestStatusIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RequestStatusIDComboBox.Enabled = Global.activiser.Console.My.MySettings.Default.RequestsAllowEdits
        Me.RequestStatusIDComboBox.FormattingEnabled = True
        Me.RequestStatusIDComboBox.Name = "RequestStatusIDComboBox"
        '
        'RequestBindingSource
        '
        Me.RequestBindingSource.AllowNew = False
        Me.RequestBindingSource.DataMember = "Request"
        Me.RequestBindingSource.DataSource = Me.CoreDataSet
        '
        'CompletedDateDateTimePicker
        '
        resources.ApplyResources(Me.CompletedDateDateTimePicker, "CompletedDateDateTimePicker")
        Me.CompletedDateDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.RequestBindingSource, "CompletedDate", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CompletedDateDateTimePicker.Name = "CompletedDateDateTimePicker"
        Me.CompletedDateDateTimePicker.ShowTime = False
        '
        'ConsultantStatusIDLabel
        '
        resources.ApplyResources(ConsultantStatusIDLabel, "ConsultantStatusIDLabel")
        ConsultantStatusIDLabel.Name = "ConsultantStatusIDLabel"
        '
        'NextActionDateLabel
        '
        resources.ApplyResources(NextActionDateLabel, "NextActionDateLabel")
        NextActionDateLabel.Name = "NextActionDateLabel"
        '
        'ConsultantStatusIDComboBox
        '
        resources.ApplyResources(Me.ConsultantStatusIDComboBox, "ConsultantStatusIDComboBox")
        Me.ConsultantStatusIDComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ConsultantStatusIDComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ConsultantStatusIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "ConsultantStatusID", True, System.Windows.Forms.DataSourceUpdateMode.Never))
        Me.ConsultantStatusIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ConsultantStatusIDComboBox.Enabled = Global.activiser.Console.My.MySettings.Default.RequestsAllowEdits
        Me.ConsultantStatusIDComboBox.FormattingEnabled = True
        Me.ConsultantStatusIDComboBox.Name = "ConsultantStatusIDComboBox"
        '
        'NextActionDateDateTimePicker
        '
        resources.ApplyResources(Me.NextActionDateDateTimePicker, "NextActionDateDateTimePicker")
        Me.NextActionDateDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.RequestBindingSource, "NextActionDate", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.NextActionDateDateTimePicker.Name = "NextActionDateDateTimePicker"
        Me.NextActionDateDateTimePicker.ShowTime = False
        '
        'CompletedDateLabel
        '
        resources.ApplyResources(CompletedDateLabel, "CompletedDateLabel")
        CompletedDateLabel.Name = "CompletedDateLabel"
        '
        'UpdateStatusButton
        '
        resources.ApplyResources(Me.UpdateStatusButton, "UpdateStatusButton")
        Me.UpdateStatusButton.Image = Global.activiser.Console.My.Resources.Resources.document_check
        Me.UpdateStatusButton.Name = "UpdateStatusButton"
        Me.UpdateStatusButton.UseVisualStyleBackColor = True
        '
        'RequestNumberLabel
        '
        resources.ApplyResources(RequestNumberLabel, "RequestNumberLabel")
        RequestNumberLabel.Name = "RequestNumberLabel"
        '
        'ReqSiteContactLabel
        '
        resources.ApplyResources(ReqSiteContactLabel, "ReqSiteContactLabel")
        ReqSiteContactLabel.Name = "ReqSiteContactLabel"
        '
        'RequestModifiedTimeLabel
        '
        resources.ApplyResources(RequestModifiedTimeLabel, "RequestModifiedTimeLabel")
        RequestModifiedTimeLabel.Name = "RequestModifiedTimeLabel"
        '
        'RequestCreatedTimeLabel
        '
        resources.ApplyResources(RequestCreatedTimeLabel, "RequestCreatedTimeLabel")
        RequestCreatedTimeLabel.Name = "RequestCreatedTimeLabel"
        '
        'RequestDescriptionLabel
        '
        resources.ApplyResources(RequestDescriptionLabel, "RequestDescriptionLabel")
        RequestDescriptionLabel.Name = "RequestDescriptionLabel"
        '
        'RequestShortDescriptionLabel
        '
        resources.ApplyResources(RequestShortDescriptionLabel, "RequestShortDescriptionLabel")
        RequestShortDescriptionLabel.Name = "RequestShortDescriptionLabel"
        '
        'NoteTabSplitContainer
        '
        resources.ApplyResources(Me.NoteTabSplitContainer, "NoteTabSplitContainer")
        Me.NoteTabSplitContainer.Name = "NoteTabSplitContainer"
        '
        'NoteTabSplitContainer.Panel1
        '
        Me.NoteTabSplitContainer.Panel1.Controls.Add(Me.ConsultantNotesLabel)
        '
        'NoteTabSplitContainer.Panel2
        '
        Me.NoteTabSplitContainer.Panel2.Controls.Add(Me.EquipmentNotesLabel)
        '
        'ConsultantNotesLabel
        '
        Me.ConsultantNotesLabel.Controls.Add(Me.ConsultantNotesTextBox)
        resources.ApplyResources(Me.ConsultantNotesLabel, "ConsultantNotesLabel")
        Me.ConsultantNotesLabel.Name = "ConsultantNotesLabel"
        Me.ConsultantNotesLabel.TabStop = False
        '
        'ConsultantNotesTextBox
        '
        Me.ConsultantNotesTextBox.AcceptsReturn = True
        Me.ConsultantNotesTextBox.AcceptsTab = True
        Me.ConsultantNotesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ConsultantNotesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "jobsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ConsultantNotesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JobBindingSource, "JobNotes", True))
        Me.ConsultantNotesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("WordWrap", Global.activiser.Console.My.MySettings.Default, "WordWrapJobConsultantNotes", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.ConsultantNotesTextBox, "ConsultantNotesTextBox")
        Me.ConsultantNotesTextBox.Name = "ConsultantNotesTextBox"
        Me.ConsultantNotesTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.JobsReadOnly
        Me.ConsultantNotesTextBox.WordWrap = Global.activiser.Console.My.MySettings.Default.WordWrapJobConsultantNotes
        '
        'EquipmentNotesLabel
        '
        Me.EquipmentNotesLabel.Controls.Add(Me.EquipmentNotesTextBox)
        resources.ApplyResources(Me.EquipmentNotesLabel, "EquipmentNotesLabel")
        Me.EquipmentNotesLabel.Name = "EquipmentNotesLabel"
        Me.EquipmentNotesLabel.TabStop = False
        '
        'EquipmentNotesTextBox
        '
        Me.EquipmentNotesTextBox.AcceptsReturn = True
        Me.EquipmentNotesTextBox.AcceptsTab = True
        Me.EquipmentNotesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EquipmentNotesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JobBindingSource, "Equipment", True))
        Me.EquipmentNotesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "jobsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.EquipmentNotesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("WordWrap", Global.activiser.Console.My.MySettings.Default, "WordWrapJobRequipment", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.EquipmentNotesTextBox, "EquipmentNotesTextBox")
        Me.EquipmentNotesTextBox.Name = "EquipmentNotesTextBox"
        Me.EquipmentNotesTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.JobsReadOnly
        Me.EquipmentNotesTextBox.WordWrap = Global.activiser.Console.My.MySettings.Default.WordWrapJobRequipment
        '
        'RequestHeaderSplitContainer
        '
        resources.ApplyResources(Me.RequestHeaderSplitContainer, "RequestHeaderSplitContainer")
        Me.RequestHeaderSplitContainer.Name = "RequestHeaderSplitContainer"
        '
        'RequestHeaderSplitContainer.Panel1
        '
        Me.RequestHeaderSplitContainer.Panel1.Controls.Add(Me.RequestAssignedToLabel)
        Me.RequestHeaderSplitContainer.Panel1.Controls.Add(Me.RequestNumberTextBox)
        Me.RequestHeaderSplitContainer.Panel1.Controls.Add(Me.RequestAssignedToComboBox)
        Me.RequestHeaderSplitContainer.Panel1.Controls.Add(RequestNumberLabel)
        Me.RequestHeaderSplitContainer.Panel1.Controls.Add(Me.ContactTextBox)
        Me.RequestHeaderSplitContainer.Panel1.Controls.Add(ReqSiteContactLabel)
        '
        'RequestHeaderSplitContainer.Panel2
        '
        Me.RequestHeaderSplitContainer.Panel2.Controls.Add(Me.RequestCreatedTime)
        Me.RequestHeaderSplitContainer.Panel2.Controls.Add(RequestModifiedTimeLabel)
        Me.RequestHeaderSplitContainer.Panel2.Controls.Add(RequestCreatedTimeLabel)
        Me.RequestHeaderSplitContainer.Panel2.Controls.Add(Me.RequestModifiedTime)
        '
        'RequestAssignedToLabel
        '
        resources.ApplyResources(Me.RequestAssignedToLabel, "RequestAssignedToLabel")
        Me.RequestAssignedToLabel.Name = "RequestAssignedToLabel"
        '
        'RequestNumberTextBox
        '
        resources.ApplyResources(Me.RequestNumberTextBox, "RequestNumberTextBox")
        Me.RequestNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "RequestNumber", True))
        Me.RequestNumberTextBox.Name = "RequestNumberTextBox"
        Me.RequestNumberTextBox.ReadOnly = True
        '
        'RequestAssignedToComboBox
        '
        resources.ApplyResources(Me.RequestAssignedToComboBox, "RequestAssignedToComboBox")
        Me.RequestAssignedToComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.RequestAssignedToComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RequestAssignedToComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "AssignedToUID", True))
        Me.RequestAssignedToComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RequestAssignedToComboBox.FormattingEnabled = True
        Me.RequestAssignedToComboBox.Name = "RequestAssignedToComboBox"
        '
        'ContactTextBox
        '
        resources.ApplyResources(Me.ContactTextBox, "ContactTextBox")
        Me.ContactTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "requestsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ContactTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "Contact", True))
        Me.ContactTextBox.Name = "ContactTextBox"
        Me.ContactTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.RequestsReadOnly
        '
        'RequestCreatedTime
        '
        resources.ApplyResources(Me.RequestCreatedTime, "RequestCreatedTime")
        Me.RequestCreatedTime.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "CreatedDateTime", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "N/A", "f"))
        Me.RequestCreatedTime.Name = "RequestCreatedTime"
        Me.RequestCreatedTime.ReadOnly = True
        '
        'RequestModifiedTime
        '
        resources.ApplyResources(Me.RequestModifiedTime, "RequestModifiedTime")
        Me.RequestModifiedTime.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "ModifiedDateTime", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "N/A", "f"))
        Me.RequestModifiedTime.Name = "RequestModifiedTime"
        Me.RequestModifiedTime.ReadOnly = True
        '
        'JobDataGridView
        '
        Me.JobDataGridView.AllowUserToAddRows = False
        Me.JobDataGridView.AllowUserToDeleteRows = False
        Me.JobDataGridView.AllowUserToOrderColumns = True
        Me.JobDataGridView.AutoGenerateColumns = False
        Me.JobDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.JobDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RequestNumberColumn, Me.ConsultantNameColumn, Me.ClientSiteNameColumn, Me.JobStartTimeColumn, Me.JobFinishTimeColumn, Me.JobDetailsColumn, Me.ReturnDateColumn, Me.JobNumberColumn})
        Me.JobDataGridView.ContextMenuStrip = Me.JobListFilterMenu
        Me.JobDataGridView.DataSource = Me.JobBindingSource
        resources.ApplyResources(Me.JobDataGridView, "JobDataGridView")
        Me.JobDataGridView.MultiSelect = False
        Me.JobDataGridView.Name = "JobDataGridView"
        Me.JobDataGridView.ReadOnly = True
        Me.JobDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'RequestNumberColumn
        '
        Me.RequestNumberColumn.DataPropertyName = "RequestNumber"
        resources.ApplyResources(Me.RequestNumberColumn, "RequestNumberColumn")
        Me.RequestNumberColumn.Name = "RequestNumberColumn"
        Me.RequestNumberColumn.ReadOnly = True
        Me.RequestNumberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ConsultantNameColumn
        '
        Me.ConsultantNameColumn.DataPropertyName = "ConsultantName"
        resources.ApplyResources(Me.ConsultantNameColumn, "ConsultantNameColumn")
        Me.ConsultantNameColumn.Name = "ConsultantNameColumn"
        Me.ConsultantNameColumn.ReadOnly = True
        '
        'ClientSiteNameColumn
        '
        Me.ClientSiteNameColumn.DataPropertyName = "ClientSiteName"
        resources.ApplyResources(Me.ClientSiteNameColumn, "ClientSiteNameColumn")
        Me.ClientSiteNameColumn.Name = "ClientSiteNameColumn"
        Me.ClientSiteNameColumn.ReadOnly = True
        '
        'JobStartTimeColumn
        '
        Me.JobStartTimeColumn.DataPropertyName = "StartTime"
        DataGridViewCellStyle1.Format = "G"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.JobStartTimeColumn.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.JobStartTimeColumn, "JobStartTimeColumn")
        Me.JobStartTimeColumn.Name = "JobStartTimeColumn"
        Me.JobStartTimeColumn.ReadOnly = True
        '
        'JobFinishTimeColumn
        '
        Me.JobFinishTimeColumn.DataPropertyName = "FinishTime"
        DataGridViewCellStyle2.Format = "G"
        Me.JobFinishTimeColumn.DefaultCellStyle = DataGridViewCellStyle2
        resources.ApplyResources(Me.JobFinishTimeColumn, "JobFinishTimeColumn")
        Me.JobFinishTimeColumn.Name = "JobFinishTimeColumn"
        Me.JobFinishTimeColumn.ReadOnly = True
        '
        'JobDetailsColumn
        '
        Me.JobDetailsColumn.DataPropertyName = "JobDetails"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.NullValue = "<None>"
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.JobDetailsColumn.DefaultCellStyle = DataGridViewCellStyle3
        resources.ApplyResources(Me.JobDetailsColumn, "JobDetailsColumn")
        Me.JobDetailsColumn.Name = "JobDetailsColumn"
        Me.JobDetailsColumn.ReadOnly = True
        '
        'ReturnDateColumn
        '
        Me.ReturnDateColumn.DataPropertyName = "ReturnDate"
        resources.ApplyResources(Me.ReturnDateColumn, "ReturnDateColumn")
        Me.ReturnDateColumn.Name = "ReturnDateColumn"
        Me.ReturnDateColumn.ReadOnly = True
        '
        'JobNumberColumn
        '
        Me.JobNumberColumn.DataPropertyName = "JobNumber"
        resources.ApplyResources(Me.JobNumberColumn, "JobNumberColumn")
        Me.JobNumberColumn.Name = "JobNumberColumn"
        Me.JobNumberColumn.ReadOnly = True
        '
        'JobListFilterMenu
        '
        Me.JobListFilterMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterByDateToolStripMenuItem, Me.FilterMenuConsultant, Me.FilterMenuClient, Me.FilterMenuRequest, Me.FilterMenuClearAll})
        Me.JobListFilterMenu.Name = "ContextMenuStrip2"
        resources.ApplyResources(Me.JobListFilterMenu, "JobListFilterMenu")
        '
        'FilterByDateToolStripMenuItem
        '
        Me.FilterByDateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuAllDates, Me.FilterMenuDateToday, Me.FilterMenuYesterday, Me.FilterMenuPastWeek, Me.FilterMenuPastMonth})
        Me.FilterByDateToolStripMenuItem.Name = "FilterByDateToolStripMenuItem"
        resources.ApplyResources(Me.FilterByDateToolStripMenuItem, "FilterByDateToolStripMenuItem")
        '
        'FilterMenuAllDates
        '
        Me.FilterMenuAllDates.Name = "FilterMenuAllDates"
        resources.ApplyResources(Me.FilterMenuAllDates, "FilterMenuAllDates")
        '
        'FilterMenuDateToday
        '
        Me.FilterMenuDateToday.Name = "FilterMenuDateToday"
        resources.ApplyResources(Me.FilterMenuDateToday, "FilterMenuDateToday")
        '
        'FilterMenuYesterday
        '
        Me.FilterMenuYesterday.Name = "FilterMenuYesterday"
        resources.ApplyResources(Me.FilterMenuYesterday, "FilterMenuYesterday")
        '
        'FilterMenuPastWeek
        '
        Me.FilterMenuPastWeek.Name = "FilterMenuPastWeek"
        resources.ApplyResources(Me.FilterMenuPastWeek, "FilterMenuPastWeek")
        '
        'FilterMenuPastMonth
        '
        Me.FilterMenuPastMonth.Name = "FilterMenuPastMonth"
        resources.ApplyResources(Me.FilterMenuPastMonth, "FilterMenuPastMonth")
        '
        'FilterMenuConsultant
        '
        Me.FilterMenuConsultant.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuConsultantNone, Me.FilterMenuConsultantInclude, Me.FilterMenuConsultantExclude})
        Me.FilterMenuConsultant.Name = "FilterMenuConsultant"
        resources.ApplyResources(Me.FilterMenuConsultant, "FilterMenuConsultant")
        '
        'FilterMenuConsultantNone
        '
        Me.FilterMenuConsultantNone.Name = "FilterMenuConsultantNone"
        resources.ApplyResources(Me.FilterMenuConsultantNone, "FilterMenuConsultantNone")
        '
        'FilterMenuConsultantInclude
        '
        Me.FilterMenuConsultantInclude.Name = "FilterMenuConsultantInclude"
        resources.ApplyResources(Me.FilterMenuConsultantInclude, "FilterMenuConsultantInclude")
        '
        'FilterMenuConsultantExclude
        '
        Me.FilterMenuConsultantExclude.Name = "FilterMenuConsultantExclude"
        resources.ApplyResources(Me.FilterMenuConsultantExclude, "FilterMenuConsultantExclude")
        '
        'FilterMenuClient
        '
        Me.FilterMenuClient.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuClientNone, Me.FilterMenuClientInclude, Me.FilterMenuClientExclude})
        Me.FilterMenuClient.Name = "FilterMenuClient"
        resources.ApplyResources(Me.FilterMenuClient, "FilterMenuClient")
        '
        'FilterMenuClientNone
        '
        Me.FilterMenuClientNone.Name = "FilterMenuClientNone"
        resources.ApplyResources(Me.FilterMenuClientNone, "FilterMenuClientNone")
        '
        'FilterMenuClientInclude
        '
        Me.FilterMenuClientInclude.Name = "FilterMenuClientInclude"
        resources.ApplyResources(Me.FilterMenuClientInclude, "FilterMenuClientInclude")
        '
        'FilterMenuClientExclude
        '
        Me.FilterMenuClientExclude.Name = "FilterMenuClientExclude"
        resources.ApplyResources(Me.FilterMenuClientExclude, "FilterMenuClientExclude")
        '
        'FilterMenuRequest
        '
        Me.FilterMenuRequest.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuRequestNone, Me.FilterMenuRequestOnlySelected, Me.FilterMenuRequestExcludeSelected})
        Me.FilterMenuRequest.Name = "FilterMenuRequest"
        resources.ApplyResources(Me.FilterMenuRequest, "FilterMenuRequest")
        '
        'FilterMenuRequestNone
        '
        Me.FilterMenuRequestNone.Name = "FilterMenuRequestNone"
        resources.ApplyResources(Me.FilterMenuRequestNone, "FilterMenuRequestNone")
        '
        'FilterMenuRequestOnlySelected
        '
        Me.FilterMenuRequestOnlySelected.Name = "FilterMenuRequestOnlySelected"
        resources.ApplyResources(Me.FilterMenuRequestOnlySelected, "FilterMenuRequestOnlySelected")
        '
        'FilterMenuRequestExcludeSelected
        '
        Me.FilterMenuRequestExcludeSelected.Name = "FilterMenuRequestExcludeSelected"
        resources.ApplyResources(Me.FilterMenuRequestExcludeSelected, "FilterMenuRequestExcludeSelected")
        '
        'FilterMenuClearAll
        '
        Me.FilterMenuClearAll.Name = "FilterMenuClearAll"
        resources.ApplyResources(Me.FilterMenuClearAll, "FilterMenuClearAll")
        '
        'JobInfoTabControl
        '
        Me.JobInfoTabControl.Controls.Add(Me.JobActivitiesTab)
        Me.JobInfoTabControl.Controls.Add(Me.JobNotesTab)
        Me.JobInfoTabControl.Controls.Add(Me.RequestTab)
        Me.JobInfoTabControl.Controls.Add(Me.GpsTab)
        resources.ApplyResources(Me.JobInfoTabControl, "JobInfoTabControl")
        Me.JobInfoTabControl.MinimumSize = New System.Drawing.Size(750, 250)
        Me.JobInfoTabControl.Name = "JobInfoTabControl"
        Me.JobInfoTabControl.SelectedIndex = 0
        '
        'JobActivitiesTab
        '
        resources.ApplyResources(Me.JobActivitiesTab, "JobActivitiesTab")
        Me.JobActivitiesTab.Controls.Add(Me.GeneralTabSplitContainer)
        Me.JobActivitiesTab.Name = "JobActivitiesTab"
        Me.JobActivitiesTab.UseVisualStyleBackColor = True
        '
        'JobNotesTab
        '
        resources.ApplyResources(Me.JobNotesTab, "JobNotesTab")
        Me.JobNotesTab.Controls.Add(Me.NoteTabSplitContainer)
        Me.JobNotesTab.Name = "JobNotesTab"
        Me.JobNotesTab.UseVisualStyleBackColor = True
        '
        'RequestTab
        '
        Me.RequestTab.Controls.Add(Me.RequestInformationLabel)
        resources.ApplyResources(Me.RequestTab, "RequestTab")
        Me.RequestTab.Name = "RequestTab"
        Me.RequestTab.UseVisualStyleBackColor = True
        '
        'RequestInformationLabel
        '
        Me.RequestInformationLabel.Controls.Add(Me.RequestDescriptionTextBox)
        Me.RequestInformationLabel.Controls.Add(RequestDescriptionLabel)
        Me.RequestInformationLabel.Controls.Add(Me.ShortDescriptionPanel)
        Me.RequestInformationLabel.Controls.Add(Me.RequestHeaderSplitContainer)
        resources.ApplyResources(Me.RequestInformationLabel, "RequestInformationLabel")
        Me.RequestInformationLabel.Name = "RequestInformationLabel"
        Me.RequestInformationLabel.TabStop = False
        '
        'RequestDescriptionTextBox
        '
        resources.ApplyResources(Me.RequestDescriptionTextBox, "RequestDescriptionTextBox")
        Me.RequestDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "requestsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RequestDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "LongDescription", True))
        Me.RequestDescriptionTextBox.Name = "RequestDescriptionTextBox"
        Me.RequestDescriptionTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.RequestsReadOnly
        '
        'ShortDescriptionPanel
        '
        Me.ShortDescriptionPanel.Controls.Add(Me.RequestShortDescriptionTextBox)
        Me.ShortDescriptionPanel.Controls.Add(RequestShortDescriptionLabel)
        resources.ApplyResources(Me.ShortDescriptionPanel, "ShortDescriptionPanel")
        Me.ShortDescriptionPanel.Name = "ShortDescriptionPanel"
        '
        'RequestShortDescriptionTextBox
        '
        resources.ApplyResources(Me.RequestShortDescriptionTextBox, "RequestShortDescriptionTextBox")
        Me.RequestShortDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "requestsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RequestShortDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "ShortDescription", True))
        Me.RequestShortDescriptionTextBox.Name = "RequestShortDescriptionTextBox"
        Me.RequestShortDescriptionTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.RequestsReadOnly
        '
        'GpsTab
        '
        Me.GpsTab.Controls.Add(Me.GpsBrowser)
        resources.ApplyResources(Me.GpsTab, "GpsTab")
        Me.GpsTab.Name = "GpsTab"
        Me.GpsTab.UseVisualStyleBackColor = True
        '
        'GpsBrowser
        '
        resources.ApplyResources(Me.GpsBrowser, "GpsBrowser")
        Me.GpsBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.GpsBrowser.Name = "GpsBrowser"
        Me.GpsBrowser.Url = New System.Uri("about:blank", System.UriKind.Absolute)
        '
        'JobToolbar
        '
        Me.JobToolbar.AddNewItem = Nothing
        Me.JobToolbar.BindingSource = Me.JobBindingSource
        Me.JobToolbar.CountItem = Me.BindingNavigatorCountItem
        Me.JobToolbar.DeleteItem = Nothing
        resources.ApplyResources(Me.JobToolbar, "JobToolbar")
        Me.JobToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.ToolStripUndoButton, Me.ToolStripSeparator1, Me.JobToolbarSaveButton, Me.PrintToolStripButton, Me.ToolStripSplitButton1})
        Me.JobToolbar.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.JobToolbar.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.JobToolbar.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.JobToolbar.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.JobToolbar.Name = "JobToolbar"
        Me.JobToolbar.PositionItem = Me.BindingNavigatorPositionItem
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        resources.ApplyResources(Me.BindingNavigatorCountItem, "BindingNavigatorCountItem")
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.BindingNavigatorMoveFirstItem, "BindingNavigatorMoveFirstItem")
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.BindingNavigatorMovePreviousItem, "BindingNavigatorMovePreviousItem")
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        resources.ApplyResources(Me.BindingNavigatorSeparator, "BindingNavigatorSeparator")
        '
        'BindingNavigatorPositionItem
        '
        resources.ApplyResources(Me.BindingNavigatorPositionItem, "BindingNavigatorPositionItem")
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        resources.ApplyResources(Me.BindingNavigatorSeparator1, "BindingNavigatorSeparator1")
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.BindingNavigatorMoveNextItem, "BindingNavigatorMoveNextItem")
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.BindingNavigatorMoveLastItem, "BindingNavigatorMoveLastItem")
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        resources.ApplyResources(Me.BindingNavigatorSeparator2, "BindingNavigatorSeparator2")
        '
        'ToolStripUndoButton
        '
        Me.ToolStripUndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripUndoButton.Image = Global.activiser.Console.My.Resources.Resources.Edit_UndoHS
        resources.ApplyResources(Me.ToolStripUndoButton, "ToolStripUndoButton")
        Me.ToolStripUndoButton.Name = "ToolStripUndoButton"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'JobToolbarSaveButton
        '
        Me.JobToolbarSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.JobToolbarSaveButton, "JobToolbarSaveButton")
        Me.JobToolbarSaveButton.Name = "JobToolbarSaveButton"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = Global.activiser.Console.My.Resources.Resources.Web
        resources.ApplyResources(Me.PrintToolStripButton, "PrintToolStripButton")
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupportInfoToolStripMenuItem})
        resources.ApplyResources(Me.ToolStripSplitButton1, "ToolStripSplitButton1")
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        '
        'SupportInfoToolStripMenuItem
        '
        Me.SupportInfoToolStripMenuItem.Name = "SupportInfoToolStripMenuItem"
        resources.ApplyResources(Me.SupportInfoToolStripMenuItem, "SupportInfoToolStripMenuItem")
        '
        'StatusStrip
        '
        Me.StatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusBarRequestNumber, Me.StatusBarSiteName, Me.StatusBarConsultant, Me.StatusBarHaveSignature, Me.StatusBarHasNotes, Me.StatusBarHasEquipment})
        resources.ApplyResources(Me.StatusStrip, "StatusStrip")
        Me.StatusStrip.Name = "StatusStrip"
        '
        'StatusBarRequestNumber
        '
        Me.StatusBarRequestNumber.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarRequestNumber.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.StatusBarRequestNumber.DoubleClickEnabled = True
        Me.StatusBarRequestNumber.Name = "StatusBarRequestNumber"
        resources.ApplyResources(Me.StatusBarRequestNumber, "StatusBarRequestNumber")
        '
        'StatusBarSiteName
        '
        Me.StatusBarSiteName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarSiteName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.StatusBarSiteName.DoubleClickEnabled = True
        Me.StatusBarSiteName.Name = "StatusBarSiteName"
        resources.ApplyResources(Me.StatusBarSiteName, "StatusBarSiteName")
        '
        'StatusBarConsultant
        '
        Me.StatusBarConsultant.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarConsultant.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.StatusBarConsultant.DoubleClickEnabled = True
        Me.StatusBarConsultant.Name = "StatusBarConsultant"
        resources.ApplyResources(Me.StatusBarConsultant, "StatusBarConsultant")
        '
        'StatusBarHaveSignature
        '
        Me.StatusBarHaveSignature.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarHaveSignature.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.StatusBarHaveSignature.DoubleClickEnabled = True
        Me.StatusBarHaveSignature.Name = "StatusBarHaveSignature"
        resources.ApplyResources(Me.StatusBarHaveSignature, "StatusBarHaveSignature")
        '
        'StatusBarHasNotes
        '
        Me.StatusBarHasNotes.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarHasNotes.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.StatusBarHasNotes.DoubleClickEnabled = True
        Me.StatusBarHasNotes.Name = "StatusBarHasNotes"
        resources.ApplyResources(Me.StatusBarHasNotes, "StatusBarHasNotes")
        '
        'StatusBarHasEquipment
        '
        Me.StatusBarHasEquipment.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarHasEquipment.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.StatusBarHasEquipment.DoubleClickEnabled = True
        Me.StatusBarHasEquipment.Name = "StatusBarHasEquipment"
        resources.ApplyResources(Me.StatusBarHasEquipment, "StatusBarHasEquipment")
        '
        'JobGridTabSplitter
        '
        resources.ApplyResources(Me.JobGridTabSplitter, "JobGridTabSplitter")
        Me.JobGridTabSplitter.Name = "JobGridTabSplitter"
        Me.JobGridTabSplitter.TabStop = False
        '
        'JobStatusContextMenu
        '
        Me.JobStatusContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JobStatusChangeToDraftButton, Me.JobStatusChangeToCompleteButton, Me.JobStatusChangeToSignedButton, Me.JobStatusChangeToStatusChangeButton})
        Me.JobStatusContextMenu.Name = "JobStatusContextMenu"
        resources.ApplyResources(Me.JobStatusContextMenu, "JobStatusContextMenu")
        '
        'JobStatusChangeToDraftButton
        '
        Me.JobStatusChangeToDraftButton.Name = "JobStatusChangeToDraftButton"
        resources.ApplyResources(Me.JobStatusChangeToDraftButton, "JobStatusChangeToDraftButton")
        Me.JobStatusChangeToDraftButton.Tag = "0"
        '
        'JobStatusChangeToCompleteButton
        '
        Me.JobStatusChangeToCompleteButton.Name = "JobStatusChangeToCompleteButton"
        resources.ApplyResources(Me.JobStatusChangeToCompleteButton, "JobStatusChangeToCompleteButton")
        Me.JobStatusChangeToCompleteButton.Tag = "3"
        '
        'JobStatusChangeToSignedButton
        '
        Me.JobStatusChangeToSignedButton.Name = "JobStatusChangeToSignedButton"
        resources.ApplyResources(Me.JobStatusChangeToSignedButton, "JobStatusChangeToSignedButton")
        Me.JobStatusChangeToSignedButton.Tag = "4"
        '
        'JobStatusChangeToStatusChangeButton
        '
        Me.JobStatusChangeToStatusChangeButton.Name = "JobStatusChangeToStatusChangeButton"
        resources.ApplyResources(Me.JobStatusChangeToStatusChangeButton, "JobStatusChangeToStatusChangeButton")
        Me.JobStatusChangeToStatusChangeButton.Tag = "6"
        '
        'JobAdminSubForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.JobDataGridView)
        Me.Controls.Add(Me.JobGridTabSplitter)
        Me.Controls.Add(Me.JobInfoTabControl)
        Me.Controls.Add(Me.JobToolbar)
        Me.Controls.Add(Me.StatusStrip)
        Me.MinimumSize = New System.Drawing.Size(750, 540)
        Me.Name = "JobAdminSubForm"
        Me.GeneralTabSplitContainer.Panel1.ResumeLayout(False)
        Me.GeneralTabSplitContainer.Panel2.ResumeLayout(False)
        Me.GeneralTabSplitContainer.ResumeLayout(False)
        Me.SignatureGroup.ResumeLayout(False)
        Me.SignatureGroup.PerformLayout()
        Me.ResendEmailMenu.ResumeLayout(False)
        CType(Me.JobBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SignatureViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.JobInfoGroup.ResumeLayout(False)
        Me.JobInfoGroup.PerformLayout()
        Me.JobNotesGroup.ResumeLayout(False)
        Me.JobNotesGroup.PerformLayout()
        Me.ApprovalGroup.ResumeLayout(False)
        Me.ApprovalGroup.PerformLayout()
        Me.RequestStatusAprrovalGroupBox.ResumeLayout(False)
        Me.RequestStatusAprrovalGroupBox.PerformLayout()
        Me.RequestStatusChangeMenu.ResumeLayout(False)
        CType(Me.RequestBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NoteTabSplitContainer.Panel1.ResumeLayout(False)
        Me.NoteTabSplitContainer.Panel2.ResumeLayout(False)
        Me.NoteTabSplitContainer.ResumeLayout(False)
        Me.ConsultantNotesLabel.ResumeLayout(False)
        Me.ConsultantNotesLabel.PerformLayout()
        Me.EquipmentNotesLabel.ResumeLayout(False)
        Me.EquipmentNotesLabel.PerformLayout()
        Me.RequestHeaderSplitContainer.Panel1.ResumeLayout(False)
        Me.RequestHeaderSplitContainer.Panel1.PerformLayout()
        Me.RequestHeaderSplitContainer.Panel2.ResumeLayout(False)
        Me.RequestHeaderSplitContainer.Panel2.PerformLayout()
        Me.RequestHeaderSplitContainer.ResumeLayout(False)
        CType(Me.JobDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.JobListFilterMenu.ResumeLayout(False)
        Me.JobInfoTabControl.ResumeLayout(False)
        Me.JobActivitiesTab.ResumeLayout(False)
        Me.JobNotesTab.ResumeLayout(False)
        Me.RequestTab.ResumeLayout(False)
        Me.RequestInformationLabel.ResumeLayout(False)
        Me.RequestInformationLabel.PerformLayout()
        Me.ShortDescriptionPanel.ResumeLayout(False)
        Me.ShortDescriptionPanel.PerformLayout()
        Me.GpsTab.ResumeLayout(False)
        CType(Me.JobToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.JobToolbar.ResumeLayout(False)
        Me.JobToolbar.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.JobStatusContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CoreDataSet As Library.activiserWebService.activiserDataSet
    Friend WithEvents JobBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents JobToolbar As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents JobToolbarSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents JobDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents RequestBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RequestUIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ConsultantUIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ClientSiteUIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents MinsTravelledNumericUpDown As activiser.Console.DurationPicker
    Friend WithEvents SignatoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EmailTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JobInfoTabControl As System.Windows.Forms.TabControl
    Friend WithEvents JobActivitiesTab As System.Windows.Forms.TabPage
    Friend WithEvents JobNotesTab As System.Windows.Forms.TabPage
    Friend WithEvents ApprovalGroup As System.Windows.Forms.GroupBox
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents NextActionDateDateTimePicker As activiser.Library.DateTimePicker
    Friend WithEvents UpdateStatusButton As System.Windows.Forms.Button
    Friend WithEvents SignatureGroup As System.Windows.Forms.GroupBox
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusBarRequestNumber As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBarSiteName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBarConsultant As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBarHaveSignature As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SignatureViewer As activiser.Console.SignatureViewer
    Friend WithEvents JobInfoGroup As System.Windows.Forms.GroupBox
    Friend WithEvents GeneralTabSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents NoteTabSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents JobNotesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents StatusBarHasNotes As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBarHasEquipment As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents JobDetailsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EnableRequestChange As System.Windows.Forms.CheckBox
    Friend WithEvents EnableClientSiteChange As System.Windows.Forms.CheckBox
    Friend WithEvents EnableConsultantChange As System.Windows.Forms.CheckBox
    Friend WithEvents ConsultantNotesLabel As System.Windows.Forms.GroupBox
    Friend WithEvents ConsultantNotesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EquipmentNotesLabel As System.Windows.Forms.GroupBox
    Friend WithEvents EquipmentNotesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RequestStatusIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ConsultantStatusIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CompletedDateDateTimePicker As activiser.Library.DateTimePicker
    Friend WithEvents RequestStatusAprrovalGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents UIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobListFilterMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FilterByDateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuDateToday As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuPastWeek As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuYesterday As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuPastMonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuConsultant As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuConsultantInclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuConsultantExclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClient As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClientInclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClientExclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuAllDates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuConsultantNone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClientNone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RequestNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestTab As System.Windows.Forms.TabPage
    Friend WithEvents RequestInformationLabel As System.Windows.Forms.GroupBox
    Friend WithEvents RequestDescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ShortDescriptionPanel As System.Windows.Forms.Panel
    Friend WithEvents RequestShortDescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RequestHeaderSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents RequestNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ContactTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RequestCreatedTime As System.Windows.Forms.TextBox
    Friend WithEvents RequestModifiedTime As System.Windows.Forms.TextBox
    Friend WithEvents RequestAssignedToLabel As System.Windows.Forms.Label
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents JobFlagGroup As activiser.Console.JobFlagGroup
    Friend WithEvents RequestStatusChangeReasonLabel As System.Windows.Forms.Label
    Friend WithEvents RequestStatusChangeMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AcceptRequestStatusChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RejectRequestStatusChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JobGridTabSplitter As System.Windows.Forms.Splitter
    Friend WithEvents JobStartDateTimePicker As activiser.Library.DateTimePicker
    Friend WithEvents JobFinishDateTimePicker As activiser.library.DateTimePicker
    Friend WithEvents FilterMenuRequest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuRequestNone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuRequestOnlySelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuRequestExcludeSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClearAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReturnDatePicker As activiser.Library.DateTimePicker
    Friend WithEvents ToolStripUndoButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents JobStatusContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents JobStatusChangeToCompleteButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JobStatusChangeToDraftButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JobStatusChangeToSignedButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JobStatusChangeToStatusChangeButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RequestAssignedToComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents emailStatus As System.Windows.Forms.CheckBox
    Friend WithEvents ResendEmailMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ResendEmailMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SupportInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GpsTab As System.Windows.Forms.TabPage
    Friend WithEvents TrackingInfoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RequestNumberColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConsultantNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientSiteNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobStartTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobFinishTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobDetailsColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReturnDateColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobNumberColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents GpsBrowser As System.Windows.Forms.WebBrowser

End Class
