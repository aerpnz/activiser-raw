<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RequestAdminSubForm
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
        Dim RequestNumberLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RequestAdminSubForm))
        Dim ClientSiteLabel As System.Windows.Forms.Label
        Dim AssignedToUIDLabel As System.Windows.Forms.Label
        Dim ContactLabel As System.Windows.Forms.Label
        Dim LongDescriptionLabel As System.Windows.Forms.Label
        Dim ShortDescriptionLabel As System.Windows.Forms.Label
        Dim NextActionDateLabel As System.Windows.Forms.Label
        Dim CompletedDateLabel As System.Windows.Forms.Label
        Dim RequestStatusIDLabel As System.Windows.Forms.Label
        Dim ConsultantStatusIDLabel As System.Windows.Forms.Label
        Dim RequestIDLabel As System.Windows.Forms.Label
        Dim RequestUIDLabel As System.Windows.Forms.Label
        Dim RequestNumberTrackingLabel As System.Windows.Forms.Label
        Dim ModifiedDateTimeLabel As System.Windows.Forms.Label
        Dim CreatedDateTimeLabel As System.Windows.Forms.Label
        Dim ConsultantUIDLabel As System.Windows.Forms.Label
        Dim ClientSiteUIDLabel As System.Windows.Forms.Label
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Toolbar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.RequestBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CoreDataSet = New activiser.Library.activiserWebService.activiserDataSet
        Me.ToolbarCountLabel = New System.Windows.Forms.ToolStripLabel
        Me.ToolbarMoveFirstButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarMovePreviousButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.ToolbarPositionLabel = New System.Windows.Forms.ToolStripTextBox
        Me.ToolbarSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolbarMoveNextButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarMoveLastButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripUndoButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolbarSaveButton = New System.Windows.Forms.ToolStripButton
        Me.ClientInformationGroupLabel = New System.Windows.Forms.GroupBox
        Me.ClientSiteUIDComboBox = New System.Windows.Forms.ComboBox
        Me.RequestInformationGroupLabel = New System.Windows.Forms.GroupBox
        Me.LongDescriptionTextBox = New System.Windows.Forms.TextBox
        Me.ShortDescriptionPanel = New System.Windows.Forms.Panel
        Me.ContactTextBox = New System.Windows.Forms.TextBox
        Me.RequestNumberTextBox = New System.Windows.Forms.TextBox
        Me.ShortDescriptionTextBox = New System.Windows.Forms.TextBox
        Me.StatusInformationGroupLabel = New System.Windows.Forms.GroupBox
        Me.StatusInformationSplitContainer = New System.Windows.Forms.SplitContainer
        Me.RequestStatusChangeReasonLabel = New System.Windows.Forms.Label
        Me.RequestStatusChangeMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AcceptRequestStatusChange = New System.Windows.Forms.ToolStripMenuItem
        Me.RejectRequestStatusChange = New System.Windows.Forms.ToolStripMenuItem
        Me.RequestStatusIDComboBox = New System.Windows.Forms.ComboBox
        Me.ConsultantStatusIDComboBox = New System.Windows.Forms.ComboBox
        Me.AssignedToUIDComboBox = New System.Windows.Forms.ComboBox
        Me.NextActionDatePicker = New activiser.Library.DateTimePicker
        Me.CompletedDatePicker = New activiser.Library.DateTimePicker
        Me.RequestDataGridView = New System.Windows.Forms.DataGridView
        Me.RequestNumberColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ClientSiteUIDColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AssignedToUIDColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NextActionDateColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ModifiedDateTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RequestUidColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RequestIDColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RequestFormSplitter = New System.Windows.Forms.Splitter
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.BasicInformationTab = New System.Windows.Forms.TabPage
        Me.RequestTrackingInfoTab = New System.Windows.Forms.TabPage
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.TrackingTabRequestNumberTextBox = New System.Windows.Forms.TextBox
        Me.RequestIDTextBox = New System.Windows.Forms.TextBox
        Me.UIDTextBox = New System.Windows.Forms.TextBox
        Me.ConsultantUIDTextBox = New System.Windows.Forms.TextBox
        Me.ClientSiteUIDTextBox = New System.Windows.Forms.TextBox
        Me.RequestCreatedTime = New System.Windows.Forms.TextBox
        Me.RequestModifiedTime = New System.Windows.Forms.TextBox
        Me.JobTab = New System.Windows.Forms.TabPage
        Me.JobDataGridView = New System.Windows.Forms.DataGridView
        Me.JobBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.ConsultantName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobStartTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobFinishTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobDetails = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobStatusID = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.JobUIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobDetailsTextBox = New System.Windows.Forms.TextBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        RequestNumberLabel = New System.Windows.Forms.Label
        ClientSiteLabel = New System.Windows.Forms.Label
        AssignedToUIDLabel = New System.Windows.Forms.Label
        ContactLabel = New System.Windows.Forms.Label
        LongDescriptionLabel = New System.Windows.Forms.Label
        ShortDescriptionLabel = New System.Windows.Forms.Label
        NextActionDateLabel = New System.Windows.Forms.Label
        CompletedDateLabel = New System.Windows.Forms.Label
        RequestStatusIDLabel = New System.Windows.Forms.Label
        ConsultantStatusIDLabel = New System.Windows.Forms.Label
        RequestIDLabel = New System.Windows.Forms.Label
        RequestUIDLabel = New System.Windows.Forms.Label
        RequestNumberTrackingLabel = New System.Windows.Forms.Label
        ModifiedDateTimeLabel = New System.Windows.Forms.Label
        CreatedDateTimeLabel = New System.Windows.Forms.Label
        ConsultantUIDLabel = New System.Windows.Forms.Label
        ClientSiteUIDLabel = New System.Windows.Forms.Label
        CType(Me.Toolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Toolbar.SuspendLayout()
        CType(Me.RequestBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ClientInformationGroupLabel.SuspendLayout()
        Me.RequestInformationGroupLabel.SuspendLayout()
        Me.ShortDescriptionPanel.SuspendLayout()
        Me.StatusInformationGroupLabel.SuspendLayout()
        Me.StatusInformationSplitContainer.Panel1.SuspendLayout()
        Me.StatusInformationSplitContainer.Panel2.SuspendLayout()
        Me.StatusInformationSplitContainer.SuspendLayout()
        Me.RequestStatusChangeMenu.SuspendLayout()
        CType(Me.RequestDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl.SuspendLayout()
        Me.BasicInformationTab.SuspendLayout()
        Me.RequestTrackingInfoTab.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.JobTab.SuspendLayout()
        CType(Me.JobDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JobBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RequestNumberLabel
        '
        resources.ApplyResources(RequestNumberLabel, "RequestNumberLabel")
        RequestNumberLabel.Name = "RequestNumberLabel"
        '
        'ClientSiteLabel
        '
        resources.ApplyResources(ClientSiteLabel, "ClientSiteLabel")
        ClientSiteLabel.Name = "ClientSiteLabel"
        '
        'AssignedToUIDLabel
        '
        resources.ApplyResources(AssignedToUIDLabel, "AssignedToUIDLabel")
        AssignedToUIDLabel.Name = "AssignedToUIDLabel"
        '
        'ContactLabel
        '
        resources.ApplyResources(ContactLabel, "ContactLabel")
        ContactLabel.Name = "ContactLabel"
        '
        'LongDescriptionLabel
        '
        resources.ApplyResources(LongDescriptionLabel, "LongDescriptionLabel")
        LongDescriptionLabel.Name = "LongDescriptionLabel"
        '
        'ShortDescriptionLabel
        '
        resources.ApplyResources(ShortDescriptionLabel, "ShortDescriptionLabel")
        ShortDescriptionLabel.Name = "ShortDescriptionLabel"
        '
        'NextActionDateLabel
        '
        resources.ApplyResources(NextActionDateLabel, "NextActionDateLabel")
        NextActionDateLabel.Name = "NextActionDateLabel"
        '
        'CompletedDateLabel
        '
        resources.ApplyResources(CompletedDateLabel, "CompletedDateLabel")
        CompletedDateLabel.Name = "CompletedDateLabel"
        '
        'RequestStatusIDLabel
        '
        resources.ApplyResources(RequestStatusIDLabel, "RequestStatusIDLabel")
        RequestStatusIDLabel.Name = "RequestStatusIDLabel"
        '
        'ConsultantStatusIDLabel
        '
        resources.ApplyResources(ConsultantStatusIDLabel, "ConsultantStatusIDLabel")
        ConsultantStatusIDLabel.Name = "ConsultantStatusIDLabel"
        '
        'RequestIDLabel
        '
        resources.ApplyResources(RequestIDLabel, "RequestIDLabel")
        RequestIDLabel.Name = "RequestIDLabel"
        '
        'RequestUIDLabel
        '
        resources.ApplyResources(RequestUIDLabel, "RequestUIDLabel")
        RequestUIDLabel.Name = "RequestUIDLabel"
        '
        'RequestNumberTrackingLabel
        '
        resources.ApplyResources(RequestNumberTrackingLabel, "RequestNumberTrackingLabel")
        RequestNumberTrackingLabel.Name = "RequestNumberTrackingLabel"
        '
        'ModifiedDateTimeLabel
        '
        resources.ApplyResources(ModifiedDateTimeLabel, "ModifiedDateTimeLabel")
        ModifiedDateTimeLabel.Name = "ModifiedDateTimeLabel"
        '
        'CreatedDateTimeLabel
        '
        resources.ApplyResources(CreatedDateTimeLabel, "CreatedDateTimeLabel")
        CreatedDateTimeLabel.Name = "CreatedDateTimeLabel"
        '
        'ConsultantUIDLabel
        '
        resources.ApplyResources(ConsultantUIDLabel, "ConsultantUIDLabel")
        ConsultantUIDLabel.Name = "ConsultantUIDLabel"
        '
        'ClientSiteUIDLabel
        '
        resources.ApplyResources(ClientSiteUIDLabel, "ClientSiteUIDLabel")
        ClientSiteUIDLabel.Name = "ClientSiteUIDLabel"
        '
        'Toolbar
        '
        Me.Toolbar.AddNewItem = Nothing
        Me.Toolbar.BindingSource = Me.RequestBindingSource
        Me.Toolbar.CountItem = Me.ToolbarCountLabel
        Me.Toolbar.DeleteItem = Nothing
        resources.ApplyResources(Me.Toolbar, "Toolbar")
        Me.Toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolbarMoveFirstButton, Me.ToolbarMovePreviousButton, Me.ToolbarSeparator, Me.ToolbarPositionLabel, Me.ToolbarCountLabel, Me.ToolbarSeparator1, Me.ToolbarMoveNextButton, Me.ToolbarMoveLastButton, Me.ToolbarSeparator2, Me.ToolStripUndoButton, Me.ToolStripSeparator1, Me.ToolbarSaveButton})
        Me.Toolbar.MoveFirstItem = Me.ToolbarMoveFirstButton
        Me.Toolbar.MoveLastItem = Me.ToolbarMoveLastButton
        Me.Toolbar.MoveNextItem = Me.ToolbarMoveNextButton
        Me.Toolbar.MovePreviousItem = Me.ToolbarMovePreviousButton
        Me.Toolbar.Name = "Toolbar"
        Me.Toolbar.PositionItem = Me.ToolbarPositionLabel
        '
        'RequestBindingSource
        '
        Me.RequestBindingSource.AllowNew = False
        Me.RequestBindingSource.DataMember = "Request"
        Me.RequestBindingSource.DataSource = Me.CoreDataSet
        Me.RequestBindingSource.Sort = "RequestNumber"
        '
        'CoreDataSet
        '
        Me.CoreDataSet.DataSetName = "activiserCoreDataSet"
        Me.CoreDataSet.Locale = New System.Globalization.CultureInfo(Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo)
        Me.CoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ToolbarCountLabel
        '
        Me.ToolbarCountLabel.Name = "ToolbarCountLabel"
        resources.ApplyResources(Me.ToolbarCountLabel, "ToolbarCountLabel")
        '
        'ToolbarMoveFirstButton
        '
        Me.ToolbarMoveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarMoveFirstButton, "ToolbarMoveFirstButton")
        Me.ToolbarMoveFirstButton.Name = "ToolbarMoveFirstButton"
        '
        'ToolbarMovePreviousButton
        '
        Me.ToolbarMovePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarMovePreviousButton, "ToolbarMovePreviousButton")
        Me.ToolbarMovePreviousButton.Name = "ToolbarMovePreviousButton"
        '
        'ToolbarSeparator
        '
        Me.ToolbarSeparator.Name = "ToolbarSeparator"
        resources.ApplyResources(Me.ToolbarSeparator, "ToolbarSeparator")
        '
        'ToolbarPositionLabel
        '
        resources.ApplyResources(Me.ToolbarPositionLabel, "ToolbarPositionLabel")
        Me.ToolbarPositionLabel.Name = "ToolbarPositionLabel"
        '
        'ToolbarSeparator1
        '
        Me.ToolbarSeparator1.Name = "ToolbarSeparator1"
        resources.ApplyResources(Me.ToolbarSeparator1, "ToolbarSeparator1")
        '
        'ToolbarMoveNextButton
        '
        Me.ToolbarMoveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarMoveNextButton, "ToolbarMoveNextButton")
        Me.ToolbarMoveNextButton.Name = "ToolbarMoveNextButton"
        '
        'ToolbarMoveLastButton
        '
        Me.ToolbarMoveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarMoveLastButton, "ToolbarMoveLastButton")
        Me.ToolbarMoveLastButton.Name = "ToolbarMoveLastButton"
        '
        'ToolbarSeparator2
        '
        Me.ToolbarSeparator2.Name = "ToolbarSeparator2"
        resources.ApplyResources(Me.ToolbarSeparator2, "ToolbarSeparator2")
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
        'ToolbarSaveButton
        '
        Me.ToolbarSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarSaveButton, "ToolbarSaveButton")
        Me.ToolbarSaveButton.Name = "ToolbarSaveButton"
        '
        'ClientInformationGroupLabel
        '
        Me.ClientInformationGroupLabel.Controls.Add(Me.ClientSiteUIDComboBox)
        Me.ClientInformationGroupLabel.Controls.Add(ClientSiteLabel)
        resources.ApplyResources(Me.ClientInformationGroupLabel, "ClientInformationGroupLabel")
        Me.ClientInformationGroupLabel.Name = "ClientInformationGroupLabel"
        Me.ClientInformationGroupLabel.TabStop = False
        '
        'ClientSiteUIDComboBox
        '
        resources.ApplyResources(Me.ClientSiteUIDComboBox, "ClientSiteUIDComboBox")
        Me.ClientSiteUIDComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ClientSiteUIDComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ClientSiteUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "ClientSiteUID", True))
        Me.ClientSiteUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Global.activiser.Console.My.MySettings.Default, "requestsAllowEdits", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ClientSiteUIDComboBox.DisplayMember = "Description"
        Me.ClientSiteUIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ClientSiteUIDComboBox.Enabled = Global.activiser.Console.My.MySettings.Default.RequestsAllowEdits
        Me.ClientSiteUIDComboBox.Name = "ClientSiteUIDComboBox"
        Me.ClientSiteUIDComboBox.ValueMember = "ObjectId"
        '
        'RequestInformationGroupLabel
        '
        Me.RequestInformationGroupLabel.Controls.Add(Me.LongDescriptionTextBox)
        Me.RequestInformationGroupLabel.Controls.Add(LongDescriptionLabel)
        Me.RequestInformationGroupLabel.Controls.Add(Me.ShortDescriptionPanel)
        resources.ApplyResources(Me.RequestInformationGroupLabel, "RequestInformationGroupLabel")
        Me.RequestInformationGroupLabel.Name = "RequestInformationGroupLabel"
        Me.RequestInformationGroupLabel.TabStop = False
        '
        'LongDescriptionTextBox
        '
        resources.ApplyResources(Me.LongDescriptionTextBox, "LongDescriptionTextBox")
        Me.LongDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "requestsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.LongDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "LongDescription", True))
        Me.LongDescriptionTextBox.Name = "LongDescriptionTextBox"
        Me.LongDescriptionTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.RequestsReadOnly
        '
        'ShortDescriptionPanel
        '
        Me.ShortDescriptionPanel.Controls.Add(Me.ContactTextBox)
        Me.ShortDescriptionPanel.Controls.Add(Me.RequestNumberTextBox)
        Me.ShortDescriptionPanel.Controls.Add(Me.ShortDescriptionTextBox)
        Me.ShortDescriptionPanel.Controls.Add(RequestNumberLabel)
        Me.ShortDescriptionPanel.Controls.Add(ContactLabel)
        Me.ShortDescriptionPanel.Controls.Add(ShortDescriptionLabel)
        resources.ApplyResources(Me.ShortDescriptionPanel, "ShortDescriptionPanel")
        Me.ShortDescriptionPanel.Name = "ShortDescriptionPanel"
        '
        'ContactTextBox
        '
        resources.ApplyResources(Me.ContactTextBox, "ContactTextBox")
        Me.ContactTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "requestsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ContactTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "Contact", True))
        Me.ContactTextBox.Name = "ContactTextBox"
        Me.ContactTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.RequestsReadOnly
        '
        'RequestNumberTextBox
        '
        resources.ApplyResources(Me.RequestNumberTextBox, "RequestNumberTextBox")
        Me.RequestNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "RequestNumber", True))
        Me.RequestNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "requestsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RequestNumberTextBox.Name = "RequestNumberTextBox"
        Me.RequestNumberTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.RequestsReadOnly
        '
        'ShortDescriptionTextBox
        '
        resources.ApplyResources(Me.ShortDescriptionTextBox, "ShortDescriptionTextBox")
        Me.ShortDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.Console.My.MySettings.Default, "requestsReadOnly", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ShortDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "ShortDescription", True))
        Me.ShortDescriptionTextBox.Name = "ShortDescriptionTextBox"
        Me.ShortDescriptionTextBox.ReadOnly = Global.activiser.Console.My.MySettings.Default.RequestsReadOnly
        '
        'StatusInformationGroupLabel
        '
        Me.StatusInformationGroupLabel.Controls.Add(Me.StatusInformationSplitContainer)
        resources.ApplyResources(Me.StatusInformationGroupLabel, "StatusInformationGroupLabel")
        Me.StatusInformationGroupLabel.Name = "StatusInformationGroupLabel"
        Me.StatusInformationGroupLabel.TabStop = False
        '
        'StatusInformationSplitContainer
        '
        resources.ApplyResources(Me.StatusInformationSplitContainer, "StatusInformationSplitContainer")
        Me.StatusInformationSplitContainer.Name = "StatusInformationSplitContainer"
        '
        'StatusInformationSplitContainer.Panel1
        '
        Me.StatusInformationSplitContainer.Panel1.Controls.Add(Me.RequestStatusChangeReasonLabel)
        Me.StatusInformationSplitContainer.Panel1.Controls.Add(Me.RequestStatusIDComboBox)
        Me.StatusInformationSplitContainer.Panel1.Controls.Add(ConsultantStatusIDLabel)
        Me.StatusInformationSplitContainer.Panel1.Controls.Add(Me.ConsultantStatusIDComboBox)
        Me.StatusInformationSplitContainer.Panel1.Controls.Add(RequestStatusIDLabel)
        '
        'StatusInformationSplitContainer.Panel2
        '
        Me.StatusInformationSplitContainer.Panel2.Controls.Add(Me.AssignedToUIDComboBox)
        Me.StatusInformationSplitContainer.Panel2.Controls.Add(Me.NextActionDatePicker)
        Me.StatusInformationSplitContainer.Panel2.Controls.Add(Me.CompletedDatePicker)
        Me.StatusInformationSplitContainer.Panel2.Controls.Add(CompletedDateLabel)
        Me.StatusInformationSplitContainer.Panel2.Controls.Add(AssignedToUIDLabel)
        Me.StatusInformationSplitContainer.Panel2.Controls.Add(NextActionDateLabel)
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
        'RequestStatusIDComboBox
        '
        resources.ApplyResources(Me.RequestStatusIDComboBox, "RequestStatusIDComboBox")
        Me.RequestStatusIDComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.RequestStatusIDComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RequestStatusIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "RequestStatusID", True))
        Me.RequestStatusIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Global.activiser.Console.My.MySettings.Default, "requestsAllowEdits", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RequestStatusIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RequestStatusIDComboBox.Enabled = Global.activiser.Console.My.MySettings.Default.RequestsAllowEdits
        Me.RequestStatusIDComboBox.FormattingEnabled = True
        Me.RequestStatusIDComboBox.Name = "RequestStatusIDComboBox"
        '
        'ConsultantStatusIDComboBox
        '
        resources.ApplyResources(Me.ConsultantStatusIDComboBox, "ConsultantStatusIDComboBox")
        Me.ConsultantStatusIDComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ConsultantStatusIDComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ConsultantStatusIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "ConsultantStatusID", True, System.Windows.Forms.DataSourceUpdateMode.Never))
        Me.ConsultantStatusIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Global.activiser.Console.My.MySettings.Default, "requestsAllowEdits", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ConsultantStatusIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ConsultantStatusIDComboBox.Enabled = Global.activiser.Console.My.MySettings.Default.RequestsAllowEdits
        Me.ConsultantStatusIDComboBox.FormattingEnabled = True
        Me.ConsultantStatusIDComboBox.Name = "ConsultantStatusIDComboBox"
        '
        'AssignedToUIDComboBox
        '
        resources.ApplyResources(Me.AssignedToUIDComboBox, "AssignedToUIDComboBox")
        Me.AssignedToUIDComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.AssignedToUIDComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AssignedToUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "AssignedToUID", True))
        Me.AssignedToUIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Global.activiser.Console.My.MySettings.Default, "requestsAllowEdits", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.AssignedToUIDComboBox.DisplayMember = "Description"
        Me.AssignedToUIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AssignedToUIDComboBox.Enabled = Global.activiser.Console.My.MySettings.Default.RequestsAllowEdits
        Me.AssignedToUIDComboBox.Name = "AssignedToUIDComboBox"
        Me.AssignedToUIDComboBox.ValueMember = "ObjectId"
        '
        'NextActionDatePicker
        '
        resources.ApplyResources(Me.NextActionDatePicker, "NextActionDatePicker")
        Me.NextActionDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.RequestBindingSource, "NextActionDate", True))
        Me.NextActionDatePicker.Name = "NextActionDatePicker"
        Me.NextActionDatePicker.ShowTime = False
        '
        'CompletedDatePicker
        '
        resources.ApplyResources(Me.CompletedDatePicker, "CompletedDatePicker")
        Me.CompletedDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.RequestBindingSource, "CompletedDate", True))
        Me.CompletedDatePicker.Name = "CompletedDatePicker"
        Me.CompletedDatePicker.ShowTime = False
        '
        'RequestDataGridView
        '
        Me.RequestDataGridView.AllowUserToAddRows = False
        Me.RequestDataGridView.AllowUserToDeleteRows = False
        Me.RequestDataGridView.AllowUserToResizeRows = False
        Me.RequestDataGridView.AutoGenerateColumns = False
        Me.RequestDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RequestDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RequestNumberColumn, Me.ClientSiteUIDColumn, Me.AssignedToUIDColumn, Me.NextActionDateColumn, Me.ModifiedDateTimeColumn, Me.RequestUidColumn, Me.RequestIDColumn})
        Me.RequestDataGridView.DataSource = Me.RequestBindingSource
        resources.ApplyResources(Me.RequestDataGridView, "RequestDataGridView")
        Me.RequestDataGridView.MultiSelect = False
        Me.RequestDataGridView.Name = "RequestDataGridView"
        Me.RequestDataGridView.RowHeadersVisible = False
        Me.RequestDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'RequestNumberColumn
        '
        Me.RequestNumberColumn.DataPropertyName = "RequestNumber"
        resources.ApplyResources(Me.RequestNumberColumn, "RequestNumberColumn")
        Me.RequestNumberColumn.MaxInputLength = 50
        Me.RequestNumberColumn.Name = "RequestNumberColumn"
        Me.RequestNumberColumn.ReadOnly = True
        '
        'ClientSiteUIDColumn
        '
        resources.ApplyResources(Me.ClientSiteUIDColumn, "ClientSiteUIDColumn")
        Me.ClientSiteUIDColumn.Name = "ClientSiteUIDColumn"
        Me.ClientSiteUIDColumn.ReadOnly = True
        Me.ClientSiteUIDColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'AssignedToUIDColumn
        '
        resources.ApplyResources(Me.AssignedToUIDColumn, "AssignedToUIDColumn")
        Me.AssignedToUIDColumn.Name = "AssignedToUIDColumn"
        Me.AssignedToUIDColumn.ReadOnly = True
        Me.AssignedToUIDColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'NextActionDateColumn
        '
        Me.NextActionDateColumn.DataPropertyName = "NextActionDate"
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.NextActionDateColumn.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.NextActionDateColumn, "NextActionDateColumn")
        Me.NextActionDateColumn.Name = "NextActionDateColumn"
        '
        'ModifiedDateTimeColumn
        '
        Me.ModifiedDateTimeColumn.DataPropertyName = "ModifiedDateTime"
        resources.ApplyResources(Me.ModifiedDateTimeColumn, "ModifiedDateTimeColumn")
        Me.ModifiedDateTimeColumn.Name = "ModifiedDateTimeColumn"
        Me.ModifiedDateTimeColumn.ReadOnly = True
        '
        'RequestUidColumn
        '
        Me.RequestUidColumn.DataPropertyName = "RequestUID"
        Me.RequestUidColumn.FillWeight = 1.0!
        resources.ApplyResources(Me.RequestUidColumn, "RequestUidColumn")
        Me.RequestUidColumn.Name = "RequestUidColumn"
        Me.RequestUidColumn.ReadOnly = True
        '
        'RequestIDColumn
        '
        Me.RequestIDColumn.DataPropertyName = "RequestID"
        resources.ApplyResources(Me.RequestIDColumn, "RequestIDColumn")
        Me.RequestIDColumn.Name = "RequestIDColumn"
        Me.RequestIDColumn.ReadOnly = True
        '
        'RequestFormSplitter
        '
        resources.ApplyResources(Me.RequestFormSplitter, "RequestFormSplitter")
        Me.RequestFormSplitter.Name = "RequestFormSplitter"
        Me.RequestFormSplitter.TabStop = False
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.BasicInformationTab)
        Me.TabControl.Controls.Add(Me.RequestTrackingInfoTab)
        Me.TabControl.Controls.Add(Me.JobTab)
        resources.ApplyResources(Me.TabControl, "TabControl")
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        '
        'BasicInformationTab
        '
        Me.BasicInformationTab.Controls.Add(Me.RequestInformationGroupLabel)
        Me.BasicInformationTab.Controls.Add(Me.StatusInformationGroupLabel)
        Me.BasicInformationTab.Controls.Add(Me.Toolbar)
        Me.BasicInformationTab.Controls.Add(Me.ClientInformationGroupLabel)
        resources.ApplyResources(Me.BasicInformationTab, "BasicInformationTab")
        Me.BasicInformationTab.Name = "BasicInformationTab"
        Me.BasicInformationTab.UseVisualStyleBackColor = True
        '
        'RequestTrackingInfoTab
        '
        Me.RequestTrackingInfoTab.Controls.Add(Me.TableLayoutPanel1)
        resources.ApplyResources(Me.RequestTrackingInfoTab, "RequestTrackingInfoTab")
        Me.RequestTrackingInfoTab.Name = "RequestTrackingInfoTab"
        Me.RequestTrackingInfoTab.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(ModifiedDateTimeLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackingTabRequestNumberTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(RequestNumberTrackingLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.RequestIDTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(RequestUIDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(RequestIDLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.UIDTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(CreatedDateTimeLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(ConsultantUIDLabel, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ConsultantUIDTextBox, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(ClientSiteUIDLabel, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.ClientSiteUIDTextBox, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.RequestCreatedTime, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.RequestModifiedTime, 1, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'TrackingTabRequestNumberTextBox
        '
        Me.TrackingTabRequestNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "RequestNumber", True))
        resources.ApplyResources(Me.TrackingTabRequestNumberTextBox, "TrackingTabRequestNumberTextBox")
        Me.TrackingTabRequestNumberTextBox.Name = "TrackingTabRequestNumberTextBox"
        Me.TrackingTabRequestNumberTextBox.ReadOnly = True
        '
        'RequestIDTextBox
        '
        Me.RequestIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "RequestID", True))
        resources.ApplyResources(Me.RequestIDTextBox, "RequestIDTextBox")
        Me.RequestIDTextBox.Name = "RequestIDTextBox"
        Me.RequestIDTextBox.ReadOnly = True
        '
        'UIDTextBox
        '
        Me.UIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "RequestUID", True))
        resources.ApplyResources(Me.UIDTextBox, "UIDTextBox")
        Me.UIDTextBox.Name = "UIDTextBox"
        Me.UIDTextBox.ReadOnly = True
        '
        'ConsultantUIDTextBox
        '
        Me.ConsultantUIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "AssignedToUID", True))
        resources.ApplyResources(Me.ConsultantUIDTextBox, "ConsultantUIDTextBox")
        Me.ConsultantUIDTextBox.Name = "ConsultantUIDTextBox"
        Me.ConsultantUIDTextBox.ReadOnly = True
        '
        'ClientSiteUIDTextBox
        '
        Me.ClientSiteUIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "ClientSiteUID", True))
        resources.ApplyResources(Me.ClientSiteUIDTextBox, "ClientSiteUIDTextBox")
        Me.ClientSiteUIDTextBox.Name = "ClientSiteUIDTextBox"
        Me.ClientSiteUIDTextBox.ReadOnly = True
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
        'JobTab
        '
        Me.JobTab.Controls.Add(Me.JobDataGridView)
        Me.JobTab.Controls.Add(Me.Splitter1)
        Me.JobTab.Controls.Add(Me.JobDetailsTextBox)
        resources.ApplyResources(Me.JobTab, "JobTab")
        Me.JobTab.Name = "JobTab"
        Me.JobTab.UseVisualStyleBackColor = True
        '
        'JobDataGridView
        '
        Me.JobDataGridView.AllowUserToAddRows = False
        Me.JobDataGridView.AllowUserToDeleteRows = False
        Me.JobDataGridView.AllowUserToOrderColumns = True
        Me.JobDataGridView.AutoGenerateColumns = False
        Me.JobDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.JobDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ConsultantName, Me.JobNumber, Me.JobStartTimeColumn, Me.JobFinishTimeColumn, Me.JobDetails, Me.JobStatusID, Me.JobUIDDataGridViewTextBoxColumn})
        Me.JobDataGridView.DataSource = Me.JobBindingSource
        resources.ApplyResources(Me.JobDataGridView, "JobDataGridView")
        Me.JobDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.JobDataGridView.MultiSelect = False
        Me.JobDataGridView.Name = "JobDataGridView"
        Me.JobDataGridView.ReadOnly = True
        Me.JobDataGridView.RowHeadersVisible = False
        Me.JobDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'JobBindingSource
        '
        Me.JobBindingSource.DataMember = "FK_Job_Request"
        Me.JobBindingSource.DataSource = Me.RequestBindingSource
        '
        'ConsultantName
        '
        Me.ConsultantName.DataPropertyName = "ConsultantName"
        resources.ApplyResources(Me.ConsultantName, "ConsultantName")
        Me.ConsultantName.Name = "ConsultantName"
        Me.ConsultantName.ReadOnly = True
        '
        'JobNumber
        '
        Me.JobNumber.DataPropertyName = "JobNumber"
        resources.ApplyResources(Me.JobNumber, "JobNumber")
        Me.JobNumber.Name = "JobNumber"
        Me.JobNumber.ReadOnly = True
        '
        'JobStartTimeColumn
        '
        Me.JobStartTimeColumn.DataPropertyName = "StartTime"
        DataGridViewCellStyle2.Format = "G"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.JobStartTimeColumn.DefaultCellStyle = DataGridViewCellStyle2
        resources.ApplyResources(Me.JobStartTimeColumn, "JobStartTimeColumn")
        Me.JobStartTimeColumn.Name = "JobStartTimeColumn"
        Me.JobStartTimeColumn.ReadOnly = True
        '
        'JobFinishTimeColumn
        '
        Me.JobFinishTimeColumn.DataPropertyName = "FinishTime"
        DataGridViewCellStyle3.Format = "G"
        Me.JobFinishTimeColumn.DefaultCellStyle = DataGridViewCellStyle3
        resources.ApplyResources(Me.JobFinishTimeColumn, "JobFinishTimeColumn")
        Me.JobFinishTimeColumn.Name = "JobFinishTimeColumn"
        Me.JobFinishTimeColumn.ReadOnly = True
        '
        'JobDetails
        '
        Me.JobDetails.DataPropertyName = "JobDetails"
        resources.ApplyResources(Me.JobDetails, "JobDetails")
        Me.JobDetails.Name = "JobDetails"
        Me.JobDetails.ReadOnly = True
        '
        'JobStatusID
        '
        Me.JobStatusID.DataPropertyName = "JobStatusID"
        resources.ApplyResources(Me.JobStatusID, "JobStatusID")
        Me.JobStatusID.Name = "JobStatusID"
        Me.JobStatusID.ReadOnly = True
        '
        'JobUIDDataGridViewTextBoxColumn
        '
        Me.JobUIDDataGridViewTextBoxColumn.DataPropertyName = "JobUID"
        resources.ApplyResources(Me.JobUIDDataGridViewTextBoxColumn, "JobUIDDataGridViewTextBoxColumn")
        Me.JobUIDDataGridViewTextBoxColumn.Name = "JobUIDDataGridViewTextBoxColumn"
        Me.JobUIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'JobDetailsTextBox
        '
        Me.JobDetailsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JobBindingSource, "JobDetails", True))
        resources.ApplyResources(Me.JobDetailsTextBox, "JobDetailsTextBox")
        Me.JobDetailsTextBox.Name = "JobDetailsTextBox"
        Me.JobDetailsTextBox.ReadOnly = True
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.TabStop = False
        '
        'RequestAdminSubForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.RequestFormSplitter)
        Me.Controls.Add(Me.RequestDataGridView)
        Me.Name = "RequestAdminSubForm"
        CType(Me.Toolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Toolbar.ResumeLayout(False)
        Me.Toolbar.PerformLayout()
        CType(Me.RequestBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ClientInformationGroupLabel.ResumeLayout(False)
        Me.ClientInformationGroupLabel.PerformLayout()
        Me.RequestInformationGroupLabel.ResumeLayout(False)
        Me.RequestInformationGroupLabel.PerformLayout()
        Me.ShortDescriptionPanel.ResumeLayout(False)
        Me.ShortDescriptionPanel.PerformLayout()
        Me.StatusInformationGroupLabel.ResumeLayout(False)
        Me.StatusInformationSplitContainer.Panel1.ResumeLayout(False)
        Me.StatusInformationSplitContainer.Panel2.ResumeLayout(False)
        Me.StatusInformationSplitContainer.ResumeLayout(False)
        Me.RequestStatusChangeMenu.ResumeLayout(False)
        CType(Me.RequestDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl.ResumeLayout(False)
        Me.BasicInformationTab.ResumeLayout(False)
        Me.BasicInformationTab.PerformLayout()
        Me.RequestTrackingInfoTab.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.JobTab.ResumeLayout(False)
        Me.JobTab.PerformLayout()
        CType(Me.JobDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JobBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CoreDataSet As Library.activiserWebService.activiserDataSet
    Friend WithEvents RequestBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents RequestTableAdapter As activiser.Console.activiserCoreDataSetTableAdapters.RequestTableAdapter
    Friend WithEvents Toolbar As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolbarCountLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolbarMoveFirstButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarMovePreviousButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolbarPositionLabel As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolbarSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolbarMoveNextButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarMoveLastButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolbarSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ClientSiteUIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AssignedToUIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ContactTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LongDescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ShortDescriptionTextBox As System.Windows.Forms.TextBox
    'Friend WithEvents RequestStatusTableAdapter As activiser.Console.activiserCoreDataSetTableAdapters.RequestStatusTableAdapter
    'Friend WithEvents ClientSiteTableAdapter As activiser.Console.activiserCoreDataSetTableAdapters.ClientSiteTableAdapter
    'Friend WithEvents ConsultantTableAdapter As activiser.Console.activiserCoreDataSetTableAdapters.ConsultantTableAdapter
    Friend WithEvents ClientInformationGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents RequestInformationGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents StatusInformationGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents RequestStatusIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ConsultantStatusIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents RequestDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ShortDescriptionPanel As System.Windows.Forms.Panel
    Friend WithEvents StatusInformationSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents RequestFormSplitter As System.Windows.Forms.Splitter
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents BasicInformationTab As System.Windows.Forms.TabPage
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents RequestStatusChangeReasonLabel As System.Windows.Forms.Label
    Friend WithEvents RequestStatusChangeMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AcceptRequestStatusChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RejectRequestStatusChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RequestTrackingInfoTab As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RequestIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TrackingTabRequestNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ClientSiteUIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConsultantUIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JobTab As System.Windows.Forms.TabPage
    Friend WithEvents JobBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents JobDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents NextActionDatePicker As activiser.Library.DateTimePicker
    Friend WithEvents CompletedDatePicker As activiser.Library.DateTimePicker
    Friend WithEvents RequestCreatedTime As System.Windows.Forms.TextBox
    Friend WithEvents RequestModifiedTime As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripUndoButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MinutesWorkedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PremiumMinutesWorkedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestNumberColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientSiteUIDColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AssignedToUIDColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NextActionDateColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModifiedDateTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestUidColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestIDColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConsultantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobStartTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobFinishTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobDetails As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobStatusID As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents JobUIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobDetailsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter

End Class
