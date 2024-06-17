<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StatusCustomisation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim RequestStatusDisplayOrderLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StatusCustomisation))
        Dim RequestStatusColourLabel As System.Windows.Forms.Label
        Dim RequestStatusDescriptionLabel As System.Windows.Forms.Label
        Dim RequestStatusIDLabel As System.Windows.Forms.Label
        Dim RequestStatusBackColourLabel As System.Windows.Forms.Label
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ColorDialog = New System.Windows.Forms.ColorDialog
        Me.MainMenu = New System.Windows.Forms.MenuStrip
        Me.FileMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.FileSaveMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileMenuSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.FileExitMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpContentsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpIndexMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpSearchMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpMenuSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.HelpMenuAboutButton = New System.Windows.Forms.ToolStripMenuItem
        Me.ClientSiteStatusTab = New System.Windows.Forms.TabPage
        Me.ClientSiteStatusDataGridView = New System.Windows.Forms.DataGridView
        Me.ClientSiteStatusID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ClientSiteStatusIsActiveColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.ClientSiteStatusDescriptionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ClientSiteStatusBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CustomisationTables = New activiser.Library.activiserWebService.activiserDataSet
        Me.ClientSiteStatusNotesGroup = New System.Windows.Forms.GroupBox
        Me.ClientSiteStatusNotes = New System.Windows.Forms.Label
        Me.ClientSiteStatusToolbar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ClientSiteStatusToolbarAddNewButton = New System.Windows.Forms.ToolStripButton
        Me.ClientSiteStatusToolbarCountLabel = New System.Windows.Forms.ToolStripLabel
        Me.ClientSiteStatusToolbarDeleteButton = New System.Windows.Forms.ToolStripButton
        Me.ClientSiteStatusToolbarMoveFirstButton = New System.Windows.Forms.ToolStripButton
        Me.ClientSiteStatusToolbarMovePreviousButton = New System.Windows.Forms.ToolStripButton
        Me.ClientSiteStatusToolbarSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ClientSiteStatusToolbarPositionButton = New System.Windows.Forms.ToolStripTextBox
        Me.ClientSiteStatusToolbarSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ClientSiteStatusToolbarMoveNextButton = New System.Windows.Forms.ToolStripButton
        Me.ClientSiteStatusToolbarMoveLastButton = New System.Windows.Forms.ToolStripButton
        Me.ClientSiteStatusToolbarSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ClientSiteStatusToolbarSaveButton = New System.Windows.Forms.ToolStripButton
        Me.JobStatusTab = New System.Windows.Forms.TabPage
        Me.JobStatusNotesGroup = New System.Windows.Forms.GroupBox
        Me.JobStatusNotes = New System.Windows.Forms.Label
        Me.JobStatusDataGridView = New System.Windows.Forms.DataGridView
        Me.JobStatusDescriptionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobStatusBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JobStatusToolbar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.JobStatusToolbarCountLabel = New System.Windows.Forms.ToolStripLabel
        Me.JobStatusToolbarMoveFirstButton = New System.Windows.Forms.ToolStripButton
        Me.JobStatusToolbarMovePreviousButton = New System.Windows.Forms.ToolStripButton
        Me.JobStatusToolbarSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.JobStatusToolbarPositionButton = New System.Windows.Forms.ToolStripTextBox
        Me.JobStatusToolbarSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.JobStatusToolbarMoveNextButton = New System.Windows.Forms.ToolStripButton
        Me.JobStatusToolbarMoveLastButton = New System.Windows.Forms.ToolStripButton
        Me.JobStatusToolbarSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.JobStatusToolbarSaveButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusTab = New System.Windows.Forms.TabPage
        Me.RequestStatusDataGridView = New System.Windows.Forms.DataGridView
        Me.RequestStatusIDColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RequestStatusOrderColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RequestStatusDescriptionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RequestStatusBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RequestStatusDetailsPanel = New System.Windows.Forms.Panel
        Me.RequestStatusCategoryGroup = New System.Windows.Forms.GroupBox
        Me.RequestStatusIsCancelledStatusLabel = New System.Windows.Forms.RadioButton
        Me.RequestStatusIsCompleteStatusLabel = New System.Windows.Forms.RadioButton
        Me.RequestStatusIsInProgressStatusLabel = New System.Windows.Forms.RadioButton
        Me.RequestStatusIsNewStatusLabel = New System.Windows.Forms.RadioButton
        Me.RequestStatusBackColourPanel = New System.Windows.Forms.Panel
        Me.RequestStatusDescriptionTextBox = New System.Windows.Forms.TextBox
        Me.ActiviserClientSettingsGroupLabel = New System.Windows.Forms.GroupBox
        Me.RequestStatusIsReasonRequiredLabel = New System.Windows.Forms.CheckBox
        Me.RequestStatusIsClientMenuItemLabel = New System.Windows.Forms.CheckBox
        Me.RequestStatusIDNumberBox = New System.Windows.Forms.NumericUpDown
        Me.RequestStatusColourPanel = New System.Windows.Forms.Panel
        Me.RequestStatusDisplayOrderNumberBox = New System.Windows.Forms.NumericUpDown
        Me.RequestStatusToolbar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.RequestStatusToolbarAddNewButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusToolbarCountLabel = New System.Windows.Forms.ToolStripLabel
        Me.RequestStatusToolbarMoveFirstButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusToolbarMovePreviousButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusToolbarSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.RequestStatusToolbarPositionLabel = New System.Windows.Forms.ToolStripTextBox
        Me.RequestStatusToolbarSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.RequestStatusToolbarMoveNextButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusToolbarMoveLastButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusToolbarSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.RequestStatusToolbarDeleteButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusToolbarSaveButton = New System.Windows.Forms.ToolStripButton
        Me.RequestStatusNotesGroup = New System.Windows.Forms.GroupBox
        Me.RequestStatusNotes = New System.Windows.Forms.Label
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.ConsultantStatusTab = New System.Windows.Forms.TabPage
        Me.SyncMissedMoreThanOneLabel = New System.Windows.Forms.Label
        Me.SyncMissedOneLabel = New System.Windows.Forms.Label
        Me.SyncOnTimeColour = New System.Windows.Forms.Panel
        Me.SyncMissedNoneLabel = New System.Windows.Forms.Label
        Me.SyncMissedOneColour = New System.Windows.Forms.Panel
        Me.SyncMissedTwoColour = New System.Windows.Forms.Panel
        Me.ButtonPanel = New System.Windows.Forms.Panel
        Me.AbortButton = New System.Windows.Forms.Button
        Me.DoneButton = New System.Windows.Forms.Button
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        RequestStatusDisplayOrderLabel = New System.Windows.Forms.Label
        RequestStatusColourLabel = New System.Windows.Forms.Label
        RequestStatusDescriptionLabel = New System.Windows.Forms.Label
        RequestStatusIDLabel = New System.Windows.Forms.Label
        RequestStatusBackColourLabel = New System.Windows.Forms.Label
        Me.MainMenu.SuspendLayout()
        Me.ClientSiteStatusTab.SuspendLayout()
        CType(Me.ClientSiteStatusDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClientSiteStatusBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomisationTables, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ClientSiteStatusNotesGroup.SuspendLayout()
        CType(Me.ClientSiteStatusToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ClientSiteStatusToolbar.SuspendLayout()
        Me.JobStatusTab.SuspendLayout()
        Me.JobStatusNotesGroup.SuspendLayout()
        CType(Me.JobStatusDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JobStatusBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JobStatusToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.JobStatusToolbar.SuspendLayout()
        Me.RequestStatusTab.SuspendLayout()
        CType(Me.RequestStatusDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RequestStatusBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RequestStatusDetailsPanel.SuspendLayout()
        Me.RequestStatusCategoryGroup.SuspendLayout()
        Me.ActiviserClientSettingsGroupLabel.SuspendLayout()
        CType(Me.RequestStatusIDNumberBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RequestStatusDisplayOrderNumberBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RequestStatusToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RequestStatusToolbar.SuspendLayout()
        Me.RequestStatusNotesGroup.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.ConsultantStatusTab.SuspendLayout()
        Me.ButtonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'RequestStatusDisplayOrderLabel
        '
        resources.ApplyResources(RequestStatusDisplayOrderLabel, "RequestStatusDisplayOrderLabel")
        RequestStatusDisplayOrderLabel.Name = "RequestStatusDisplayOrderLabel"
        '
        'RequestStatusColourLabel
        '
        resources.ApplyResources(RequestStatusColourLabel, "RequestStatusColourLabel")
        RequestStatusColourLabel.Name = "RequestStatusColourLabel"
        '
        'RequestStatusDescriptionLabel
        '
        resources.ApplyResources(RequestStatusDescriptionLabel, "RequestStatusDescriptionLabel")
        RequestStatusDescriptionLabel.Name = "RequestStatusDescriptionLabel"
        '
        'RequestStatusIDLabel
        '
        resources.ApplyResources(RequestStatusIDLabel, "RequestStatusIDLabel")
        RequestStatusIDLabel.Name = "RequestStatusIDLabel"
        '
        'RequestStatusBackColourLabel
        '
        resources.ApplyResources(RequestStatusBackColourLabel, "RequestStatusBackColourLabel")
        RequestStatusBackColourLabel.Name = "RequestStatusBackColourLabel"
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.HelpMenu})
        resources.ApplyResources(Me.MainMenu, "MainMenu")
        Me.MainMenu.Name = "MainMenu"
        '
        'FileMenu
        '
        Me.FileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileSaveMenuItem, Me.FileMenuSeparator, Me.FileExitMenuItem})
        Me.FileMenu.Name = "FileMenu"
        resources.ApplyResources(Me.FileMenu, "FileMenu")
        '
        'FileSaveMenuItem
        '
        resources.ApplyResources(Me.FileSaveMenuItem, "FileSaveMenuItem")
        Me.FileSaveMenuItem.Name = "FileSaveMenuItem"
        '
        'FileMenuSeparator
        '
        Me.FileMenuSeparator.Name = "FileMenuSeparator"
        resources.ApplyResources(Me.FileMenuSeparator, "FileMenuSeparator")
        '
        'FileExitMenuItem
        '
        Me.FileExitMenuItem.Name = "FileExitMenuItem"
        resources.ApplyResources(Me.FileExitMenuItem, "FileExitMenuItem")
        '
        'HelpMenu
        '
        Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpContentsMenuItem, Me.HelpIndexMenuItem, Me.HelpSearchMenuItem, Me.HelpMenuSeparator, Me.HelpMenuAboutButton})
        Me.HelpMenu.Name = "HelpMenu"
        resources.ApplyResources(Me.HelpMenu, "HelpMenu")
        '
        'HelpContentsMenuItem
        '
        Me.HelpContentsMenuItem.Name = "HelpContentsMenuItem"
        resources.ApplyResources(Me.HelpContentsMenuItem, "HelpContentsMenuItem")
        '
        'HelpIndexMenuItem
        '
        Me.HelpIndexMenuItem.Name = "HelpIndexMenuItem"
        resources.ApplyResources(Me.HelpIndexMenuItem, "HelpIndexMenuItem")
        '
        'HelpSearchMenuItem
        '
        Me.HelpSearchMenuItem.Name = "HelpSearchMenuItem"
        resources.ApplyResources(Me.HelpSearchMenuItem, "HelpSearchMenuItem")
        '
        'HelpMenuSeparator
        '
        Me.HelpMenuSeparator.Name = "HelpMenuSeparator"
        resources.ApplyResources(Me.HelpMenuSeparator, "HelpMenuSeparator")
        '
        'HelpMenuAboutButton
        '
        Me.HelpMenuAboutButton.Name = "HelpMenuAboutButton"
        resources.ApplyResources(Me.HelpMenuAboutButton, "HelpMenuAboutButton")
        '
        'ClientSiteStatusTab
        '
        Me.ClientSiteStatusTab.Controls.Add(Me.ClientSiteStatusDataGridView)
        Me.ClientSiteStatusTab.Controls.Add(Me.ClientSiteStatusNotesGroup)
        Me.ClientSiteStatusTab.Controls.Add(Me.ClientSiteStatusToolbar)
        resources.ApplyResources(Me.ClientSiteStatusTab, "ClientSiteStatusTab")
        Me.ClientSiteStatusTab.Name = "ClientSiteStatusTab"
        Me.ClientSiteStatusTab.UseVisualStyleBackColor = True
        '
        'ClientSiteStatusDataGridView
        '
        Me.ClientSiteStatusDataGridView.AutoGenerateColumns = False
        Me.ClientSiteStatusDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ClientSiteStatusID, Me.ClientSiteStatusIsActiveColumn, Me.ClientSiteStatusDescriptionColumn})
        Me.ClientSiteStatusDataGridView.DataSource = Me.ClientSiteStatusBindingSource
        resources.ApplyResources(Me.ClientSiteStatusDataGridView, "ClientSiteStatusDataGridView")
        Me.ClientSiteStatusDataGridView.Name = "ClientSiteStatusDataGridView"
        '
        'ClientSiteStatusID
        '
        Me.ClientSiteStatusID.DataPropertyName = "ClientSiteStatusID"
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.ClientSiteStatusID.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.ClientSiteStatusID, "ClientSiteStatusID")
        Me.ClientSiteStatusID.MaxInputLength = 10
        Me.ClientSiteStatusID.Name = "ClientSiteStatusID"
        '
        'ClientSiteStatusIsActiveColumn
        '
        Me.ClientSiteStatusIsActiveColumn.DataPropertyName = "IsActive"
        Me.ClientSiteStatusIsActiveColumn.FalseValue = "0"
        resources.ApplyResources(Me.ClientSiteStatusIsActiveColumn, "ClientSiteStatusIsActiveColumn")
        Me.ClientSiteStatusIsActiveColumn.Name = "ClientSiteStatusIsActiveColumn"
        Me.ClientSiteStatusIsActiveColumn.TrueValue = "1"
        '
        'ClientSiteStatusDescriptionColumn
        '
        Me.ClientSiteStatusDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ClientSiteStatusDescriptionColumn.DataPropertyName = "Description"
        resources.ApplyResources(Me.ClientSiteStatusDescriptionColumn, "ClientSiteStatusDescriptionColumn")
        Me.ClientSiteStatusDescriptionColumn.Name = "ClientSiteStatusDescriptionColumn"
        '
        'ClientSiteStatusBindingSource
        '
        Me.ClientSiteStatusBindingSource.DataMember = "ClientSiteStatus"
        Me.ClientSiteStatusBindingSource.DataSource = Me.CustomisationTables
        '
        'CustomisationTables
        '
        Me.CustomisationTables.DataSetName = "CustomisationTables"
        Me.CustomisationTables.Locale = New System.Globalization.CultureInfo(Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo)
        Me.CustomisationTables.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ClientSiteStatusNotesGroup
        '
        Me.ClientSiteStatusNotesGroup.Controls.Add(Me.ClientSiteStatusNotes)
        resources.ApplyResources(Me.ClientSiteStatusNotesGroup, "ClientSiteStatusNotesGroup")
        Me.ClientSiteStatusNotesGroup.Name = "ClientSiteStatusNotesGroup"
        Me.ClientSiteStatusNotesGroup.TabStop = False
        '
        'ClientSiteStatusNotes
        '
        resources.ApplyResources(Me.ClientSiteStatusNotes, "ClientSiteStatusNotes")
        Me.ClientSiteStatusNotes.Name = "ClientSiteStatusNotes"
        '
        'ClientSiteStatusToolbar
        '
        Me.ClientSiteStatusToolbar.AddNewItem = Me.ClientSiteStatusToolbarAddNewButton
        Me.ClientSiteStatusToolbar.BindingSource = Me.ClientSiteStatusBindingSource
        Me.ClientSiteStatusToolbar.CountItem = Me.ClientSiteStatusToolbarCountLabel
        Me.ClientSiteStatusToolbar.DeleteItem = Me.ClientSiteStatusToolbarDeleteButton
        resources.ApplyResources(Me.ClientSiteStatusToolbar, "ClientSiteStatusToolbar")
        Me.ClientSiteStatusToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClientSiteStatusToolbarMoveFirstButton, Me.ClientSiteStatusToolbarMovePreviousButton, Me.ClientSiteStatusToolbarSeparator1, Me.ClientSiteStatusToolbarPositionButton, Me.ClientSiteStatusToolbarCountLabel, Me.ClientSiteStatusToolbarSeparator2, Me.ClientSiteStatusToolbarMoveNextButton, Me.ClientSiteStatusToolbarMoveLastButton, Me.ClientSiteStatusToolbarSeparator3, Me.ClientSiteStatusToolbarAddNewButton, Me.ClientSiteStatusToolbarDeleteButton, Me.ClientSiteStatusToolbarSaveButton})
        Me.ClientSiteStatusToolbar.MoveFirstItem = Me.ClientSiteStatusToolbarMoveFirstButton
        Me.ClientSiteStatusToolbar.MoveLastItem = Me.ClientSiteStatusToolbarMoveLastButton
        Me.ClientSiteStatusToolbar.MoveNextItem = Me.ClientSiteStatusToolbarMoveNextButton
        Me.ClientSiteStatusToolbar.MovePreviousItem = Me.ClientSiteStatusToolbarMovePreviousButton
        Me.ClientSiteStatusToolbar.Name = "ClientSiteStatusToolbar"
        Me.ClientSiteStatusToolbar.PositionItem = Me.ClientSiteStatusToolbarPositionButton
        '
        'ClientSiteStatusToolbarAddNewButton
        '
        Me.ClientSiteStatusToolbarAddNewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ClientSiteStatusToolbarAddNewButton, "ClientSiteStatusToolbarAddNewButton")
        Me.ClientSiteStatusToolbarAddNewButton.Name = "ClientSiteStatusToolbarAddNewButton"
        '
        'ClientSiteStatusToolbarCountLabel
        '
        Me.ClientSiteStatusToolbarCountLabel.Name = "ClientSiteStatusToolbarCountLabel"
        resources.ApplyResources(Me.ClientSiteStatusToolbarCountLabel, "ClientSiteStatusToolbarCountLabel")
        '
        'ClientSiteStatusToolbarDeleteButton
        '
        Me.ClientSiteStatusToolbarDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ClientSiteStatusToolbarDeleteButton, "ClientSiteStatusToolbarDeleteButton")
        Me.ClientSiteStatusToolbarDeleteButton.Name = "ClientSiteStatusToolbarDeleteButton"
        '
        'ClientSiteStatusToolbarMoveFirstButton
        '
        Me.ClientSiteStatusToolbarMoveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ClientSiteStatusToolbarMoveFirstButton, "ClientSiteStatusToolbarMoveFirstButton")
        Me.ClientSiteStatusToolbarMoveFirstButton.Name = "ClientSiteStatusToolbarMoveFirstButton"
        '
        'ClientSiteStatusToolbarMovePreviousButton
        '
        Me.ClientSiteStatusToolbarMovePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ClientSiteStatusToolbarMovePreviousButton, "ClientSiteStatusToolbarMovePreviousButton")
        Me.ClientSiteStatusToolbarMovePreviousButton.Name = "ClientSiteStatusToolbarMovePreviousButton"
        '
        'ClientSiteStatusToolbarSeparator1
        '
        Me.ClientSiteStatusToolbarSeparator1.Name = "ClientSiteStatusToolbarSeparator1"
        resources.ApplyResources(Me.ClientSiteStatusToolbarSeparator1, "ClientSiteStatusToolbarSeparator1")
        '
        'ClientSiteStatusToolbarPositionButton
        '
        resources.ApplyResources(Me.ClientSiteStatusToolbarPositionButton, "ClientSiteStatusToolbarPositionButton")
        Me.ClientSiteStatusToolbarPositionButton.Name = "ClientSiteStatusToolbarPositionButton"
        '
        'ClientSiteStatusToolbarSeparator2
        '
        Me.ClientSiteStatusToolbarSeparator2.Name = "ClientSiteStatusToolbarSeparator2"
        resources.ApplyResources(Me.ClientSiteStatusToolbarSeparator2, "ClientSiteStatusToolbarSeparator2")
        '
        'ClientSiteStatusToolbarMoveNextButton
        '
        Me.ClientSiteStatusToolbarMoveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ClientSiteStatusToolbarMoveNextButton, "ClientSiteStatusToolbarMoveNextButton")
        Me.ClientSiteStatusToolbarMoveNextButton.Name = "ClientSiteStatusToolbarMoveNextButton"
        '
        'ClientSiteStatusToolbarMoveLastButton
        '
        Me.ClientSiteStatusToolbarMoveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ClientSiteStatusToolbarMoveLastButton, "ClientSiteStatusToolbarMoveLastButton")
        Me.ClientSiteStatusToolbarMoveLastButton.Name = "ClientSiteStatusToolbarMoveLastButton"
        '
        'ClientSiteStatusToolbarSeparator3
        '
        Me.ClientSiteStatusToolbarSeparator3.Name = "ClientSiteStatusToolbarSeparator3"
        resources.ApplyResources(Me.ClientSiteStatusToolbarSeparator3, "ClientSiteStatusToolbarSeparator3")
        '
        'ClientSiteStatusToolbarSaveButton
        '
        Me.ClientSiteStatusToolbarSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ClientSiteStatusToolbarSaveButton, "ClientSiteStatusToolbarSaveButton")
        Me.ClientSiteStatusToolbarSaveButton.Name = "ClientSiteStatusToolbarSaveButton"
        '
        'JobStatusTab
        '
        Me.JobStatusTab.Controls.Add(Me.JobStatusNotesGroup)
        Me.JobStatusTab.Controls.Add(Me.JobStatusDataGridView)
        Me.JobStatusTab.Controls.Add(Me.JobStatusToolbar)
        resources.ApplyResources(Me.JobStatusTab, "JobStatusTab")
        Me.JobStatusTab.Name = "JobStatusTab"
        Me.JobStatusTab.UseVisualStyleBackColor = True
        '
        'JobStatusNotesGroup
        '
        Me.JobStatusNotesGroup.Controls.Add(Me.JobStatusNotes)
        resources.ApplyResources(Me.JobStatusNotesGroup, "JobStatusNotesGroup")
        Me.JobStatusNotesGroup.Name = "JobStatusNotesGroup"
        Me.JobStatusNotesGroup.TabStop = False
        '
        'JobStatusNotes
        '
        resources.ApplyResources(Me.JobStatusNotes, "JobStatusNotes")
        Me.JobStatusNotes.Name = "JobStatusNotes"
        '
        'JobStatusDataGridView
        '
        Me.JobStatusDataGridView.AllowUserToAddRows = False
        Me.JobStatusDataGridView.AllowUserToDeleteRows = False
        Me.JobStatusDataGridView.AutoGenerateColumns = False
        Me.JobStatusDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.JobStatusDescriptionColumn})
        Me.JobStatusDataGridView.DataSource = Me.JobStatusBindingSource
        resources.ApplyResources(Me.JobStatusDataGridView, "JobStatusDataGridView")
        Me.JobStatusDataGridView.Name = "JobStatusDataGridView"
        '
        'JobStatusDescriptionColumn
        '
        Me.JobStatusDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.JobStatusDescriptionColumn.DataPropertyName = "Description"
        resources.ApplyResources(Me.JobStatusDescriptionColumn, "JobStatusDescriptionColumn")
        Me.JobStatusDescriptionColumn.Name = "JobStatusDescriptionColumn"
        '
        'JobStatusBindingSource
        '
        Me.JobStatusBindingSource.DataMember = "JobStatus"
        Me.JobStatusBindingSource.DataSource = Me.CustomisationTables
        '
        'JobStatusToolbar
        '
        Me.JobStatusToolbar.AddNewItem = Nothing
        Me.JobStatusToolbar.BindingSource = Me.JobStatusBindingSource
        Me.JobStatusToolbar.CountItem = Me.JobStatusToolbarCountLabel
        Me.JobStatusToolbar.DeleteItem = Nothing
        resources.ApplyResources(Me.JobStatusToolbar, "JobStatusToolbar")
        Me.JobStatusToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JobStatusToolbarMoveFirstButton, Me.JobStatusToolbarMovePreviousButton, Me.JobStatusToolbarSeparator1, Me.JobStatusToolbarPositionButton, Me.JobStatusToolbarCountLabel, Me.JobStatusToolbarSeparator2, Me.JobStatusToolbarMoveNextButton, Me.JobStatusToolbarMoveLastButton, Me.JobStatusToolbarSeparator3, Me.JobStatusToolbarSaveButton})
        Me.JobStatusToolbar.MoveFirstItem = Me.JobStatusToolbarMoveFirstButton
        Me.JobStatusToolbar.MoveLastItem = Me.JobStatusToolbarMoveLastButton
        Me.JobStatusToolbar.MoveNextItem = Me.JobStatusToolbarMoveNextButton
        Me.JobStatusToolbar.MovePreviousItem = Me.JobStatusToolbarMovePreviousButton
        Me.JobStatusToolbar.Name = "JobStatusToolbar"
        Me.JobStatusToolbar.PositionItem = Me.JobStatusToolbarPositionButton
        '
        'JobStatusToolbarCountLabel
        '
        Me.JobStatusToolbarCountLabel.Name = "JobStatusToolbarCountLabel"
        resources.ApplyResources(Me.JobStatusToolbarCountLabel, "JobStatusToolbarCountLabel")
        '
        'JobStatusToolbarMoveFirstButton
        '
        Me.JobStatusToolbarMoveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.JobStatusToolbarMoveFirstButton, "JobStatusToolbarMoveFirstButton")
        Me.JobStatusToolbarMoveFirstButton.Name = "JobStatusToolbarMoveFirstButton"
        '
        'JobStatusToolbarMovePreviousButton
        '
        Me.JobStatusToolbarMovePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.JobStatusToolbarMovePreviousButton, "JobStatusToolbarMovePreviousButton")
        Me.JobStatusToolbarMovePreviousButton.Name = "JobStatusToolbarMovePreviousButton"
        '
        'JobStatusToolbarSeparator1
        '
        Me.JobStatusToolbarSeparator1.Name = "JobStatusToolbarSeparator1"
        resources.ApplyResources(Me.JobStatusToolbarSeparator1, "JobStatusToolbarSeparator1")
        '
        'JobStatusToolbarPositionButton
        '
        resources.ApplyResources(Me.JobStatusToolbarPositionButton, "JobStatusToolbarPositionButton")
        Me.JobStatusToolbarPositionButton.Name = "JobStatusToolbarPositionButton"
        '
        'JobStatusToolbarSeparator2
        '
        Me.JobStatusToolbarSeparator2.Name = "JobStatusToolbarSeparator2"
        resources.ApplyResources(Me.JobStatusToolbarSeparator2, "JobStatusToolbarSeparator2")
        '
        'JobStatusToolbarMoveNextButton
        '
        Me.JobStatusToolbarMoveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.JobStatusToolbarMoveNextButton, "JobStatusToolbarMoveNextButton")
        Me.JobStatusToolbarMoveNextButton.Name = "JobStatusToolbarMoveNextButton"
        '
        'JobStatusToolbarMoveLastButton
        '
        Me.JobStatusToolbarMoveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.JobStatusToolbarMoveLastButton, "JobStatusToolbarMoveLastButton")
        Me.JobStatusToolbarMoveLastButton.Name = "JobStatusToolbarMoveLastButton"
        '
        'JobStatusToolbarSeparator3
        '
        Me.JobStatusToolbarSeparator3.Name = "JobStatusToolbarSeparator3"
        resources.ApplyResources(Me.JobStatusToolbarSeparator3, "JobStatusToolbarSeparator3")
        '
        'JobStatusToolbarSaveButton
        '
        Me.JobStatusToolbarSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.JobStatusToolbarSaveButton, "JobStatusToolbarSaveButton")
        Me.JobStatusToolbarSaveButton.Name = "JobStatusToolbarSaveButton"
        '
        'RequestStatusTab
        '
        Me.RequestStatusTab.Controls.Add(Me.RequestStatusDataGridView)
        Me.RequestStatusTab.Controls.Add(Me.RequestStatusDetailsPanel)
        Me.RequestStatusTab.Controls.Add(Me.RequestStatusToolbar)
        Me.RequestStatusTab.Controls.Add(Me.RequestStatusNotesGroup)
        resources.ApplyResources(Me.RequestStatusTab, "RequestStatusTab")
        Me.RequestStatusTab.Name = "RequestStatusTab"
        Me.RequestStatusTab.UseVisualStyleBackColor = True
        '
        'RequestStatusDataGridView
        '
        Me.RequestStatusDataGridView.AllowUserToAddRows = False
        Me.RequestStatusDataGridView.AllowUserToDeleteRows = False
        Me.RequestStatusDataGridView.AutoGenerateColumns = False
        Me.RequestStatusDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RequestStatusIDColumn, Me.RequestStatusOrderColumn, Me.RequestStatusDescriptionColumn})
        Me.RequestStatusDataGridView.DataSource = Me.RequestStatusBindingSource
        resources.ApplyResources(Me.RequestStatusDataGridView, "RequestStatusDataGridView")
        Me.RequestStatusDataGridView.Name = "RequestStatusDataGridView"
        Me.RequestStatusDataGridView.ReadOnly = True
        '
        'RequestStatusIDColumn
        '
        Me.RequestStatusIDColumn.DataPropertyName = "RequestStatusID"
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.RequestStatusIDColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.RequestStatusIDColumn.FillWeight = 10.0!
        resources.ApplyResources(Me.RequestStatusIDColumn, "RequestStatusIDColumn")
        Me.RequestStatusIDColumn.MaxInputLength = 10
        Me.RequestStatusIDColumn.Name = "RequestStatusIDColumn"
        Me.RequestStatusIDColumn.ReadOnly = True
        '
        'RequestStatusOrderColumn
        '
        Me.RequestStatusOrderColumn.DataPropertyName = "DisplayOrder"
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.RequestStatusOrderColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.RequestStatusOrderColumn.FillWeight = 10.0!
        resources.ApplyResources(Me.RequestStatusOrderColumn, "RequestStatusOrderColumn")
        Me.RequestStatusOrderColumn.MaxInputLength = 10
        Me.RequestStatusOrderColumn.Name = "RequestStatusOrderColumn"
        Me.RequestStatusOrderColumn.ReadOnly = True
        '
        'RequestStatusDescriptionColumn
        '
        Me.RequestStatusDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.RequestStatusDescriptionColumn.DataPropertyName = "Description"
        resources.ApplyResources(Me.RequestStatusDescriptionColumn, "RequestStatusDescriptionColumn")
        Me.RequestStatusDescriptionColumn.Name = "RequestStatusDescriptionColumn"
        Me.RequestStatusDescriptionColumn.ReadOnly = True
        '
        'RequestStatusBindingSource
        '
        Me.RequestStatusBindingSource.DataMember = "RequestStatus"
        Me.RequestStatusBindingSource.DataSource = Me.CustomisationTables
        '
        'RequestStatusDetailsPanel
        '
        Me.RequestStatusDetailsPanel.Controls.Add(RequestStatusBackColourLabel)
        Me.RequestStatusDetailsPanel.Controls.Add(RequestStatusIDLabel)
        Me.RequestStatusDetailsPanel.Controls.Add(Me.RequestStatusCategoryGroup)
        Me.RequestStatusDetailsPanel.Controls.Add(Me.RequestStatusBackColourPanel)
        Me.RequestStatusDetailsPanel.Controls.Add(Me.RequestStatusDescriptionTextBox)
        Me.RequestStatusDetailsPanel.Controls.Add(Me.ActiviserClientSettingsGroupLabel)
        Me.RequestStatusDetailsPanel.Controls.Add(RequestStatusDescriptionLabel)
        Me.RequestStatusDetailsPanel.Controls.Add(RequestStatusColourLabel)
        Me.RequestStatusDetailsPanel.Controls.Add(Me.RequestStatusIDNumberBox)
        Me.RequestStatusDetailsPanel.Controls.Add(Me.RequestStatusColourPanel)
        Me.RequestStatusDetailsPanel.Controls.Add(Me.RequestStatusDisplayOrderNumberBox)
        Me.RequestStatusDetailsPanel.Controls.Add(RequestStatusDisplayOrderLabel)
        resources.ApplyResources(Me.RequestStatusDetailsPanel, "RequestStatusDetailsPanel")
        Me.RequestStatusDetailsPanel.Name = "RequestStatusDetailsPanel"
        '
        'RequestStatusCategoryGroup
        '
        resources.ApplyResources(Me.RequestStatusCategoryGroup, "RequestStatusCategoryGroup")
        Me.RequestStatusCategoryGroup.Controls.Add(Me.RequestStatusIsCancelledStatusLabel)
        Me.RequestStatusCategoryGroup.Controls.Add(Me.RequestStatusIsCompleteStatusLabel)
        Me.RequestStatusCategoryGroup.Controls.Add(Me.RequestStatusIsInProgressStatusLabel)
        Me.RequestStatusCategoryGroup.Controls.Add(Me.RequestStatusIsNewStatusLabel)
        Me.RequestStatusCategoryGroup.Name = "RequestStatusCategoryGroup"
        Me.RequestStatusCategoryGroup.TabStop = False
        '
        'RequestStatusIsCancelledStatusLabel
        '
        Me.RequestStatusIsCancelledStatusLabel.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.RequestStatusBindingSource, "IsCancelledStatus", True))
        resources.ApplyResources(Me.RequestStatusIsCancelledStatusLabel, "RequestStatusIsCancelledStatusLabel")
        Me.RequestStatusIsCancelledStatusLabel.Name = "RequestStatusIsCancelledStatusLabel"
        '
        'RequestStatusIsCompleteStatusLabel
        '
        Me.RequestStatusIsCompleteStatusLabel.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.RequestStatusBindingSource, "IsCompleteStatus", True))
        resources.ApplyResources(Me.RequestStatusIsCompleteStatusLabel, "RequestStatusIsCompleteStatusLabel")
        Me.RequestStatusIsCompleteStatusLabel.Name = "RequestStatusIsCompleteStatusLabel"
        '
        'RequestStatusIsInProgressStatusLabel
        '
        Me.RequestStatusIsInProgressStatusLabel.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.RequestStatusBindingSource, "IsInProgressStatus", True))
        resources.ApplyResources(Me.RequestStatusIsInProgressStatusLabel, "RequestStatusIsInProgressStatusLabel")
        Me.RequestStatusIsInProgressStatusLabel.Name = "RequestStatusIsInProgressStatusLabel"
        '
        'RequestStatusIsNewStatusLabel
        '
        Me.RequestStatusIsNewStatusLabel.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.RequestStatusBindingSource, "IsNewStatus", True))
        resources.ApplyResources(Me.RequestStatusIsNewStatusLabel, "RequestStatusIsNewStatusLabel")
        Me.RequestStatusIsNewStatusLabel.Name = "RequestStatusIsNewStatusLabel"
        '
        'RequestStatusBackColourPanel
        '
        resources.ApplyResources(Me.RequestStatusBackColourPanel, "RequestStatusBackColourPanel")
        Me.RequestStatusBackColourPanel.BackColor = System.Drawing.SystemColors.Window
        Me.RequestStatusBackColourPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RequestStatusBackColourPanel.Name = "RequestStatusBackColourPanel"
        '
        'RequestStatusDescriptionTextBox
        '
        resources.ApplyResources(Me.RequestStatusDescriptionTextBox, "RequestStatusDescriptionTextBox")
        Me.RequestStatusDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestStatusBindingSource, "Description", True))
        Me.RequestStatusDescriptionTextBox.Name = "RequestStatusDescriptionTextBox"
        '
        'ActiviserClientSettingsGroupLabel
        '
        resources.ApplyResources(Me.ActiviserClientSettingsGroupLabel, "ActiviserClientSettingsGroupLabel")
        Me.ActiviserClientSettingsGroupLabel.Controls.Add(Me.RequestStatusIsReasonRequiredLabel)
        Me.ActiviserClientSettingsGroupLabel.Controls.Add(Me.RequestStatusIsClientMenuItemLabel)
        Me.ActiviserClientSettingsGroupLabel.Name = "ActiviserClientSettingsGroupLabel"
        Me.ActiviserClientSettingsGroupLabel.TabStop = False
        '
        'RequestStatusIsReasonRequiredLabel
        '
        Me.RequestStatusIsReasonRequiredLabel.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.RequestStatusBindingSource, "IsReasonRequired", True))
        resources.ApplyResources(Me.RequestStatusIsReasonRequiredLabel, "RequestStatusIsReasonRequiredLabel")
        Me.RequestStatusIsReasonRequiredLabel.Name = "RequestStatusIsReasonRequiredLabel"
        '
        'RequestStatusIsClientMenuItemLabel
        '
        Me.RequestStatusIsClientMenuItemLabel.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.RequestStatusBindingSource, "IsClientMenuItem", True))
        resources.ApplyResources(Me.RequestStatusIsClientMenuItemLabel, "RequestStatusIsClientMenuItemLabel")
        Me.RequestStatusIsClientMenuItemLabel.Name = "RequestStatusIsClientMenuItemLabel"
        '
        'RequestStatusIDNumberBox
        '
        resources.ApplyResources(Me.RequestStatusIDNumberBox, "RequestStatusIDNumberBox")
        Me.RequestStatusIDNumberBox.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.RequestStatusBindingSource, "RequestStatusID", True))
        Me.RequestStatusIDNumberBox.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.RequestStatusIDNumberBox.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.RequestStatusIDNumberBox.Name = "RequestStatusIDNumberBox"
        '
        'RequestStatusColourPanel
        '
        resources.ApplyResources(Me.RequestStatusColourPanel, "RequestStatusColourPanel")
        Me.RequestStatusColourPanel.BackColor = System.Drawing.SystemColors.WindowText
        Me.RequestStatusColourPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RequestStatusColourPanel.Name = "RequestStatusColourPanel"
        '
        'RequestStatusDisplayOrderNumberBox
        '
        resources.ApplyResources(Me.RequestStatusDisplayOrderNumberBox, "RequestStatusDisplayOrderNumberBox")
        Me.RequestStatusDisplayOrderNumberBox.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.RequestStatusBindingSource, "DisplayOrder", True))
        Me.RequestStatusDisplayOrderNumberBox.Name = "RequestStatusDisplayOrderNumberBox"
        '
        'RequestStatusToolbar
        '
        Me.RequestStatusToolbar.AddNewItem = Me.RequestStatusToolbarAddNewButton
        Me.RequestStatusToolbar.BindingSource = Me.RequestStatusBindingSource
        Me.RequestStatusToolbar.CountItem = Me.RequestStatusToolbarCountLabel
        Me.RequestStatusToolbar.DeleteItem = Me.RequestStatusToolbarMoveFirstButton
        resources.ApplyResources(Me.RequestStatusToolbar, "RequestStatusToolbar")
        Me.RequestStatusToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RequestStatusToolbarMoveFirstButton, Me.RequestStatusToolbarMovePreviousButton, Me.RequestStatusToolbarSeparator1, Me.RequestStatusToolbarPositionLabel, Me.RequestStatusToolbarCountLabel, Me.RequestStatusToolbarSeparator2, Me.RequestStatusToolbarMoveNextButton, Me.RequestStatusToolbarMoveLastButton, Me.RequestStatusToolbarSeparator3, Me.RequestStatusToolbarAddNewButton, Me.RequestStatusToolbarDeleteButton, Me.RequestStatusToolbarSaveButton})
        Me.RequestStatusToolbar.MoveFirstItem = Me.RequestStatusToolbarMoveFirstButton
        Me.RequestStatusToolbar.MoveLastItem = Me.RequestStatusToolbarMoveLastButton
        Me.RequestStatusToolbar.MoveNextItem = Me.RequestStatusToolbarMoveNextButton
        Me.RequestStatusToolbar.MovePreviousItem = Me.RequestStatusToolbarMovePreviousButton
        Me.RequestStatusToolbar.Name = "RequestStatusToolbar"
        Me.RequestStatusToolbar.PositionItem = Me.RequestStatusToolbarPositionLabel
        '
        'RequestStatusToolbarAddNewButton
        '
        Me.RequestStatusToolbarAddNewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestStatusToolbarAddNewButton, "RequestStatusToolbarAddNewButton")
        Me.RequestStatusToolbarAddNewButton.Name = "RequestStatusToolbarAddNewButton"
        '
        'RequestStatusToolbarCountLabel
        '
        Me.RequestStatusToolbarCountLabel.Name = "RequestStatusToolbarCountLabel"
        resources.ApplyResources(Me.RequestStatusToolbarCountLabel, "RequestStatusToolbarCountLabel")
        '
        'RequestStatusToolbarMoveFirstButton
        '
        Me.RequestStatusToolbarMoveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestStatusToolbarMoveFirstButton, "RequestStatusToolbarMoveFirstButton")
        Me.RequestStatusToolbarMoveFirstButton.Name = "RequestStatusToolbarMoveFirstButton"
        '
        'RequestStatusToolbarMovePreviousButton
        '
        Me.RequestStatusToolbarMovePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestStatusToolbarMovePreviousButton, "RequestStatusToolbarMovePreviousButton")
        Me.RequestStatusToolbarMovePreviousButton.Name = "RequestStatusToolbarMovePreviousButton"
        '
        'RequestStatusToolbarSeparator1
        '
        Me.RequestStatusToolbarSeparator1.Name = "RequestStatusToolbarSeparator1"
        resources.ApplyResources(Me.RequestStatusToolbarSeparator1, "RequestStatusToolbarSeparator1")
        '
        'RequestStatusToolbarPositionLabel
        '
        resources.ApplyResources(Me.RequestStatusToolbarPositionLabel, "RequestStatusToolbarPositionLabel")
        Me.RequestStatusToolbarPositionLabel.Name = "RequestStatusToolbarPositionLabel"
        '
        'RequestStatusToolbarSeparator2
        '
        Me.RequestStatusToolbarSeparator2.Name = "RequestStatusToolbarSeparator2"
        resources.ApplyResources(Me.RequestStatusToolbarSeparator2, "RequestStatusToolbarSeparator2")
        '
        'RequestStatusToolbarMoveNextButton
        '
        Me.RequestStatusToolbarMoveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestStatusToolbarMoveNextButton, "RequestStatusToolbarMoveNextButton")
        Me.RequestStatusToolbarMoveNextButton.Name = "RequestStatusToolbarMoveNextButton"
        '
        'RequestStatusToolbarMoveLastButton
        '
        Me.RequestStatusToolbarMoveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestStatusToolbarMoveLastButton, "RequestStatusToolbarMoveLastButton")
        Me.RequestStatusToolbarMoveLastButton.Name = "RequestStatusToolbarMoveLastButton"
        '
        'RequestStatusToolbarSeparator3
        '
        Me.RequestStatusToolbarSeparator3.Name = "RequestStatusToolbarSeparator3"
        resources.ApplyResources(Me.RequestStatusToolbarSeparator3, "RequestStatusToolbarSeparator3")
        '
        'RequestStatusToolbarDeleteButton
        '
        Me.RequestStatusToolbarDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestStatusToolbarDeleteButton, "RequestStatusToolbarDeleteButton")
        Me.RequestStatusToolbarDeleteButton.Name = "RequestStatusToolbarDeleteButton"
        '
        'RequestStatusToolbarSaveButton
        '
        Me.RequestStatusToolbarSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestStatusToolbarSaveButton, "RequestStatusToolbarSaveButton")
        Me.RequestStatusToolbarSaveButton.Name = "RequestStatusToolbarSaveButton"
        '
        'RequestStatusNotesGroup
        '
        Me.RequestStatusNotesGroup.Controls.Add(Me.RequestStatusNotes)
        resources.ApplyResources(Me.RequestStatusNotesGroup, "RequestStatusNotesGroup")
        Me.RequestStatusNotesGroup.Name = "RequestStatusNotesGroup"
        Me.RequestStatusNotesGroup.TabStop = False
        '
        'RequestStatusNotes
        '
        resources.ApplyResources(Me.RequestStatusNotes, "RequestStatusNotes")
        Me.RequestStatusNotes.Name = "RequestStatusNotes"
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.RequestStatusTab)
        Me.TabControl.Controls.Add(Me.ClientSiteStatusTab)
        Me.TabControl.Controls.Add(Me.JobStatusTab)
        Me.TabControl.Controls.Add(Me.ConsultantStatusTab)
        resources.ApplyResources(Me.TabControl, "TabControl")
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        '
        'ConsultantStatusTab
        '
        Me.ConsultantStatusTab.Controls.Add(Me.SyncMissedMoreThanOneLabel)
        Me.ConsultantStatusTab.Controls.Add(Me.SyncMissedOneLabel)
        Me.ConsultantStatusTab.Controls.Add(Me.SyncOnTimeColour)
        Me.ConsultantStatusTab.Controls.Add(Me.SyncMissedNoneLabel)
        Me.ConsultantStatusTab.Controls.Add(Me.SyncMissedOneColour)
        Me.ConsultantStatusTab.Controls.Add(Me.SyncMissedTwoColour)
        resources.ApplyResources(Me.ConsultantStatusTab, "ConsultantStatusTab")
        Me.ConsultantStatusTab.Name = "ConsultantStatusTab"
        Me.ConsultantStatusTab.UseVisualStyleBackColor = True
        '
        'SyncMissedMoreThanOneLabel
        '
        Me.SyncMissedMoreThanOneLabel.DataBindings.Add(New System.Windows.Forms.Binding("ForeColor", Global.activiser.Console.My.MySettings.Default, "SyncMissedMoreColour", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SyncMissedMoreThanOneLabel.ForeColor = Global.activiser.Console.My.MySettings.Default.SyncMissedMoreColour
        resources.ApplyResources(Me.SyncMissedMoreThanOneLabel, "SyncMissedMoreThanOneLabel")
        Me.SyncMissedMoreThanOneLabel.Name = "SyncMissedMoreThanOneLabel"
        '
        'SyncMissedOneLabel
        '
        Me.SyncMissedOneLabel.DataBindings.Add(New System.Windows.Forms.Binding("ForeColor", Global.activiser.Console.My.MySettings.Default, "SyncMissedOneColour", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SyncMissedOneLabel.ForeColor = Global.activiser.Console.My.MySettings.Default.SyncMissedOneColour
        resources.ApplyResources(Me.SyncMissedOneLabel, "SyncMissedOneLabel")
        Me.SyncMissedOneLabel.Name = "SyncMissedOneLabel"
        '
        'SyncOnTimeColour
        '
        Me.SyncOnTimeColour.BackColor = Global.activiser.Console.My.MySettings.Default.SyncOnTimeColour
        Me.SyncOnTimeColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SyncOnTimeColour.DataBindings.Add(New System.Windows.Forms.Binding("BackColor", Global.activiser.Console.My.MySettings.Default, "SyncOnTimeColour", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.SyncOnTimeColour, "SyncOnTimeColour")
        Me.SyncOnTimeColour.Name = "SyncOnTimeColour"
        '
        'SyncMissedNoneLabel
        '
        Me.SyncMissedNoneLabel.DataBindings.Add(New System.Windows.Forms.Binding("ForeColor", Global.activiser.Console.My.MySettings.Default, "SyncOnTimeColour", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SyncMissedNoneLabel.ForeColor = Global.activiser.Console.My.MySettings.Default.SyncOnTimeColour
        resources.ApplyResources(Me.SyncMissedNoneLabel, "SyncMissedNoneLabel")
        Me.SyncMissedNoneLabel.Name = "SyncMissedNoneLabel"
        '
        'SyncMissedOneColour
        '
        Me.SyncMissedOneColour.BackColor = Global.activiser.Console.My.MySettings.Default.SyncMissedOneColour
        Me.SyncMissedOneColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SyncMissedOneColour.DataBindings.Add(New System.Windows.Forms.Binding("BackColor", Global.activiser.Console.My.MySettings.Default, "SyncMissedOneColour", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.SyncMissedOneColour, "SyncMissedOneColour")
        Me.SyncMissedOneColour.Name = "SyncMissedOneColour"
        '
        'SyncMissedTwoColour
        '
        Me.SyncMissedTwoColour.BackColor = Global.activiser.Console.My.MySettings.Default.SyncMissedMoreColour
        Me.SyncMissedTwoColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SyncMissedTwoColour.DataBindings.Add(New System.Windows.Forms.Binding("BackColor", Global.activiser.Console.My.MySettings.Default, "SyncMissedMoreColour", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.SyncMissedTwoColour, "SyncMissedTwoColour")
        Me.SyncMissedTwoColour.Name = "SyncMissedTwoColour"
        '
        'ButtonPanel
        '
        Me.ButtonPanel.Controls.Add(Me.AbortButton)
        Me.ButtonPanel.Controls.Add(Me.DoneButton)
        resources.ApplyResources(Me.ButtonPanel, "ButtonPanel")
        Me.ButtonPanel.Name = "ButtonPanel"
        '
        'AbortButton
        '
        resources.ApplyResources(Me.AbortButton, "AbortButton")
        Me.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.AbortButton.Name = "AbortButton"
        Me.AbortButton.UseVisualStyleBackColor = True
        '
        'DoneButton
        '
        resources.ApplyResources(Me.DoneButton, "DoneButton")
        Me.DoneButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.DoneButton.Name = "DoneButton"
        Me.DoneButton.UseVisualStyleBackColor = True
        '
        'StatusCustomisation
        '
        Me.AcceptButton = Me.DoneButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.AbortButton
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.ButtonPanel)
        Me.MainMenuStrip = Me.MainMenu
        Me.Name = "StatusCustomisation"
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ClientSiteStatusTab.ResumeLayout(False)
        Me.ClientSiteStatusTab.PerformLayout()
        CType(Me.ClientSiteStatusDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClientSiteStatusBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomisationTables, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ClientSiteStatusNotesGroup.ResumeLayout(False)
        CType(Me.ClientSiteStatusToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ClientSiteStatusToolbar.ResumeLayout(False)
        Me.ClientSiteStatusToolbar.PerformLayout()
        Me.JobStatusTab.ResumeLayout(False)
        Me.JobStatusTab.PerformLayout()
        Me.JobStatusNotesGroup.ResumeLayout(False)
        CType(Me.JobStatusDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JobStatusBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JobStatusToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.JobStatusToolbar.ResumeLayout(False)
        Me.JobStatusToolbar.PerformLayout()
        Me.RequestStatusTab.ResumeLayout(False)
        Me.RequestStatusTab.PerformLayout()
        CType(Me.RequestStatusDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RequestStatusBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RequestStatusDetailsPanel.ResumeLayout(False)
        Me.RequestStatusDetailsPanel.PerformLayout()
        Me.RequestStatusCategoryGroup.ResumeLayout(False)
        Me.ActiviserClientSettingsGroupLabel.ResumeLayout(False)
        CType(Me.RequestStatusIDNumberBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RequestStatusDisplayOrderNumberBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RequestStatusToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RequestStatusToolbar.ResumeLayout(False)
        Me.RequestStatusToolbar.PerformLayout()
        Me.RequestStatusNotesGroup.ResumeLayout(False)
        Me.TabControl.ResumeLayout(False)
        Me.ConsultantStatusTab.ResumeLayout(False)
        Me.ButtonPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AbortButton As System.Windows.Forms.Button
    Friend WithEvents ActiviserClientSettingsGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonPanel As System.Windows.Forms.Panel
    Friend WithEvents ClientSiteStatusBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ClientSiteStatusDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ClientSiteStatusDescriptionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientSiteStatusID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientSiteStatusIsActiveColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ClientSiteStatusTab As System.Windows.Forms.TabPage
    Friend WithEvents ClientSiteStatusToolbar As System.Windows.Forms.BindingNavigator
    Friend WithEvents ClientSiteStatusToolbarAddNewButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientSiteStatusToolbarCountLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ClientSiteStatusToolbarDeleteButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientSiteStatusToolbarMoveFirstButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientSiteStatusToolbarMoveLastButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientSiteStatusToolbarMoveNextButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientSiteStatusToolbarMovePreviousButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientSiteStatusToolbarPositionButton As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ClientSiteStatusToolbarSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientSiteStatusToolbarSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ClientSiteStatusToolbarSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ClientSiteStatusToolbarSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents RequestStatusColourPanel As System.Windows.Forms.Panel
    Friend WithEvents CustomisationTables As Library.activiserWebService.activiserDataSet
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestStatusDescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RequestStatusDisplayOrderNumberBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents DoneButton As System.Windows.Forms.Button
    Friend WithEvents FileExitMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileMenuSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileSaveMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpContentsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpIndexMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpMenuAboutButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpMenuSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpSearchMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RequestStatusIDNumberBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents RequestStatusIsCancelledStatusLabel As System.Windows.Forms.RadioButton
    Friend WithEvents RequestStatusIsClientMenuItemLabel As System.Windows.Forms.CheckBox
    Friend WithEvents RequestStatusNotes As System.Windows.Forms.Label
    Friend WithEvents RequestStatusIsCompleteStatusLabel As System.Windows.Forms.RadioButton
    Friend WithEvents RequestStatusIsInProgressStatusLabel As System.Windows.Forms.RadioButton
    Friend WithEvents RequestStatusIsNewStatusLabel As System.Windows.Forms.RadioButton
    Friend WithEvents RequestStatusIsReasonRequiredLabel As System.Windows.Forms.CheckBox
    Friend WithEvents JobStatusBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents JobStatusDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents JobStatusDescriptionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobStatusTab As System.Windows.Forms.TabPage
    Friend WithEvents JobStatusToolbar As System.Windows.Forms.BindingNavigator
    Friend WithEvents JobStatusToolbarCountLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents JobStatusToolbarMoveFirstButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents JobStatusToolbarMoveLastButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents JobStatusToolbarMoveNextButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents JobStatusToolbarMovePreviousButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents JobStatusToolbarPositionButton As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents JobStatusToolbarSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents JobStatusToolbarSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents JobStatusToolbarSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents JobStatusToolbarSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents RequestStatusBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RequestStatusCategoryGroup As System.Windows.Forms.GroupBox
    Friend WithEvents RequestStatusDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents RequestStatusDescriptionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestStatusDetailsPanel As System.Windows.Forms.Panel
    Friend WithEvents RequestStatusIDColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestStatusNotesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents RequestStatusOrderColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestStatusTab As System.Windows.Forms.TabPage
    Friend WithEvents RequestStatusToolbar As System.Windows.Forms.BindingNavigator
    Friend WithEvents RequestStatusToolbarAddNewButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestStatusToolbarCountLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents RequestStatusToolbarDeleteButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestStatusToolbarMoveFirstButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestStatusToolbarMoveLastButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestStatusToolbarMoveNextButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestStatusToolbarMovePreviousButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestStatusToolbarPositionLabel As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents RequestStatusToolbarSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestStatusToolbarSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RequestStatusToolbarSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RequestStatusToolbarSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents ClientSiteStatusNotesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ClientSiteStatusNotes As System.Windows.Forms.Label
    Friend WithEvents JobStatusNotesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents JobStatusNotes As System.Windows.Forms.Label
    Friend WithEvents ConsultantStatusTab As System.Windows.Forms.TabPage
    Friend WithEvents SyncMissedMoreThanOneLabel As System.Windows.Forms.Label
    Friend WithEvents SyncMissedOneLabel As System.Windows.Forms.Label
    Friend WithEvents SyncMissedNoneLabel As System.Windows.Forms.Label
    Friend WithEvents SyncMissedTwoColour As System.Windows.Forms.Panel
    Friend WithEvents SyncMissedOneColour As System.Windows.Forms.Panel
    Friend WithEvents SyncOnTimeColour As System.Windows.Forms.Panel
    Friend WithEvents RequestStatusBackColourPanel As System.Windows.Forms.Panel

End Class
