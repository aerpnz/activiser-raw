<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventLogSubForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EventLogSubForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Toolbar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolbarAddNewButton = New System.Windows.Forms.ToolStripButton
        Me.EventLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolbarCountLabel = New System.Windows.Forms.ToolStripLabel
        Me.ToolbarDeleteButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarMoveFirstButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarMovePreviousButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.ToolbarPositionLabel = New System.Windows.Forms.ToolStripTextBox
        Me.ToolbarSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolbarMoveNextButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarMoveLastButton = New System.Windows.Forms.ToolStripButton
        Me.ToolbarSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolbarSaveButton = New System.Windows.Forms.ToolStripButton
        Me.EventLogDataGridView = New System.Windows.Forms.DataGridView
        Me.EventTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventDescriptionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ConsultantNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SiteNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobDateColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FilterMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FilterByDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuAllDates = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuDateToday = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuYesterday = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuPastWeek = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuPastMonth = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterByConsultantToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuConsultantNone = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuConsultantInclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuConsultantExclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterByClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClientNone = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClientInclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuClientExclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterByRequestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuRequestNone = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuRequestInclude = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterMenuRequestExclude = New System.Windows.Forms.ToolStripMenuItem
        Me.PollTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.Toolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Toolbar.SuspendLayout()
        CType(Me.EventLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EventLogDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FilterMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Toolbar
        '
        Me.Toolbar.AddNewItem = Me.ToolbarAddNewButton
        Me.Toolbar.AllowItemReorder = True
        Me.Toolbar.BindingSource = Me.EventLogBindingSource
        Me.Toolbar.CountItem = Me.ToolbarCountLabel
        Me.Toolbar.DeleteItem = Me.ToolbarDeleteButton
        resources.ApplyResources(Me.Toolbar, "Toolbar")
        Me.Toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolbarMoveFirstButton, Me.ToolbarMovePreviousButton, Me.ToolbarSeparator, Me.ToolbarPositionLabel, Me.ToolbarCountLabel, Me.ToolbarSeparator1, Me.ToolbarMoveNextButton, Me.ToolbarMoveLastButton, Me.ToolbarSeparator2, Me.ToolbarAddNewButton, Me.ToolbarDeleteButton, Me.ToolbarSaveButton})
        Me.Toolbar.MoveFirstItem = Me.ToolbarMoveFirstButton
        Me.Toolbar.MoveLastItem = Me.ToolbarMoveLastButton
        Me.Toolbar.MoveNextItem = Me.ToolbarMoveNextButton
        Me.Toolbar.MovePreviousItem = Me.ToolbarMovePreviousButton
        Me.Toolbar.Name = "Toolbar"
        Me.Toolbar.PositionItem = Me.ToolbarPositionLabel
        '
        'ToolbarAddNewButton
        '
        Me.ToolbarAddNewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarAddNewButton, "ToolbarAddNewButton")
        Me.ToolbarAddNewButton.Name = "ToolbarAddNewButton"
        '
        'EventLogBindingSource
        '
        '
        'ToolbarCountLabel
        '
        Me.ToolbarCountLabel.Name = "ToolbarCountLabel"
        resources.ApplyResources(Me.ToolbarCountLabel, "ToolbarCountLabel")
        '
        'ToolbarDeleteButton
        '
        Me.ToolbarDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarDeleteButton, "ToolbarDeleteButton")
        Me.ToolbarDeleteButton.Name = "ToolbarDeleteButton"
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
        'ToolbarSaveButton
        '
        Me.ToolbarSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolbarSaveButton, "ToolbarSaveButton")
        Me.ToolbarSaveButton.Name = "ToolbarSaveButton"
        '
        'EventLogDataGridView
        '
        Me.EventLogDataGridView.AllowUserToAddRows = False
        Me.EventLogDataGridView.AllowUserToDeleteRows = False
        Me.EventLogDataGridView.AllowUserToOrderColumns = True
        Me.EventLogDataGridView.AutoGenerateColumns = False
        Me.EventLogDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EventTimeColumn, Me.EventDescriptionColumn, Me.ConsultantNameColumn, Me.SiteNameColumn, Me.JobDateColumn})
        Me.EventLogDataGridView.ContextMenuStrip = Me.FilterMenu
        Me.EventLogDataGridView.DataSource = Me.EventLogBindingSource
        resources.ApplyResources(Me.EventLogDataGridView, "EventLogDataGridView")
        Me.EventLogDataGridView.Name = "EventLogDataGridView"
        Me.EventLogDataGridView.ReadOnly = True
        Me.EventLogDataGridView.ShowEditingIcon = False
        '
        'EventTimeColumn
        '
        Me.EventTimeColumn.DataPropertyName = "UTCEventTime"
        DataGridViewCellStyle1.Format = "G"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.EventTimeColumn.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.EventTimeColumn, "EventTimeColumn")
        Me.EventTimeColumn.Name = "EventTimeColumn"
        Me.EventTimeColumn.ReadOnly = True
        '
        'EventDescriptionColumn
        '
        Me.EventDescriptionColumn.DataPropertyName = "EventDescription"
        resources.ApplyResources(Me.EventDescriptionColumn, "EventDescriptionColumn")
        Me.EventDescriptionColumn.Name = "EventDescriptionColumn"
        Me.EventDescriptionColumn.ReadOnly = True
        '
        'ConsultantNameColumn
        '
        Me.ConsultantNameColumn.DataPropertyName = "ConsultantName"
        resources.ApplyResources(Me.ConsultantNameColumn, "ConsultantNameColumn")
        Me.ConsultantNameColumn.Name = "ConsultantNameColumn"
        Me.ConsultantNameColumn.ReadOnly = True
        '
        'SiteNameColumn
        '
        Me.SiteNameColumn.DataPropertyName = "SiteName"
        resources.ApplyResources(Me.SiteNameColumn, "SiteNameColumn")
        Me.SiteNameColumn.Name = "SiteNameColumn"
        Me.SiteNameColumn.ReadOnly = True
        '
        'JobDateColumn
        '
        Me.JobDateColumn.DataPropertyName = "StartTime"
        DataGridViewCellStyle2.Format = "G"
        DataGridViewCellStyle2.NullValue = "<N/A>"
        Me.JobDateColumn.DefaultCellStyle = DataGridViewCellStyle2
        resources.ApplyResources(Me.JobDateColumn, "JobDateColumn")
        Me.JobDateColumn.Name = "JobDateColumn"
        Me.JobDateColumn.ReadOnly = True
        '
        'FilterMenu
        '
        Me.FilterMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterByDateToolStripMenuItem, Me.FilterByConsultantToolStripMenuItem, Me.FilterByClientToolStripMenuItem, Me.FilterByRequestToolStripMenuItem})
        Me.FilterMenu.Name = "ContextMenuStrip2"
        resources.ApplyResources(Me.FilterMenu, "FilterMenu")
        '
        'FilterByDateToolStripMenuItem
        '
        Me.FilterByDateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuAllDates, Me.FilterMenuDateToday, Me.FilterMenuYesterday, Me.FilterMenuPastWeek, Me.FilterMenuPastMonth})
        Me.FilterByDateToolStripMenuItem.Name = "FilterByDateToolStripMenuItem"
        resources.ApplyResources(Me.FilterByDateToolStripMenuItem, "FilterByDateToolStripMenuItem")
        '
        'FilterMenuAllDates
        '
        Me.FilterMenuAllDates.Checked = True
        Me.FilterMenuAllDates.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FilterMenuAllDates.Name = "FilterMenuAllDates"
        resources.ApplyResources(Me.FilterMenuAllDates, "FilterMenuAllDates")
        '
        'FilterMenuDateToday
        '
        Me.FilterMenuDateToday.CheckOnClick = True
        Me.FilterMenuDateToday.Name = "FilterMenuDateToday"
        resources.ApplyResources(Me.FilterMenuDateToday, "FilterMenuDateToday")
        '
        'FilterMenuYesterday
        '
        Me.FilterMenuYesterday.CheckOnClick = True
        Me.FilterMenuYesterday.Name = "FilterMenuYesterday"
        resources.ApplyResources(Me.FilterMenuYesterday, "FilterMenuYesterday")
        '
        'FilterMenuPastWeek
        '
        Me.FilterMenuPastWeek.CheckOnClick = True
        Me.FilterMenuPastWeek.Name = "FilterMenuPastWeek"
        resources.ApplyResources(Me.FilterMenuPastWeek, "FilterMenuPastWeek")
        '
        'FilterMenuPastMonth
        '
        Me.FilterMenuPastMonth.CheckOnClick = True
        Me.FilterMenuPastMonth.Name = "FilterMenuPastMonth"
        resources.ApplyResources(Me.FilterMenuPastMonth, "FilterMenuPastMonth")
        '
        'FilterByConsultantToolStripMenuItem
        '
        Me.FilterByConsultantToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuConsultantNone, Me.FilterMenuConsultantInclude, Me.FilterMenuConsultantExclude})
        Me.FilterByConsultantToolStripMenuItem.Name = "FilterByConsultantToolStripMenuItem"
        resources.ApplyResources(Me.FilterByConsultantToolStripMenuItem, "FilterByConsultantToolStripMenuItem")
        '
        'FilterMenuConsultantNone
        '
        Me.FilterMenuConsultantNone.Checked = True
        Me.FilterMenuConsultantNone.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FilterMenuConsultantNone.Name = "FilterMenuConsultantNone"
        resources.ApplyResources(Me.FilterMenuConsultantNone, "FilterMenuConsultantNone")
        '
        'FilterMenuConsultantInclude
        '
        Me.FilterMenuConsultantInclude.CheckOnClick = True
        Me.FilterMenuConsultantInclude.Name = "FilterMenuConsultantInclude"
        resources.ApplyResources(Me.FilterMenuConsultantInclude, "FilterMenuConsultantInclude")
        '
        'FilterMenuConsultantExclude
        '
        Me.FilterMenuConsultantExclude.CheckOnClick = True
        Me.FilterMenuConsultantExclude.Name = "FilterMenuConsultantExclude"
        resources.ApplyResources(Me.FilterMenuConsultantExclude, "FilterMenuConsultantExclude")
        '
        'FilterByClientToolStripMenuItem
        '
        Me.FilterByClientToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuClientNone, Me.FilterMenuClientInclude, Me.FilterMenuClientExclude})
        Me.FilterByClientToolStripMenuItem.Name = "FilterByClientToolStripMenuItem"
        resources.ApplyResources(Me.FilterByClientToolStripMenuItem, "FilterByClientToolStripMenuItem")
        '
        'FilterMenuClientNone
        '
        Me.FilterMenuClientNone.Checked = True
        Me.FilterMenuClientNone.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FilterMenuClientNone.Name = "FilterMenuClientNone"
        resources.ApplyResources(Me.FilterMenuClientNone, "FilterMenuClientNone")
        '
        'FilterMenuClientInclude
        '
        Me.FilterMenuClientInclude.CheckOnClick = True
        Me.FilterMenuClientInclude.Name = "FilterMenuClientInclude"
        resources.ApplyResources(Me.FilterMenuClientInclude, "FilterMenuClientInclude")
        '
        'FilterMenuClientExclude
        '
        Me.FilterMenuClientExclude.CheckOnClick = True
        Me.FilterMenuClientExclude.Name = "FilterMenuClientExclude"
        resources.ApplyResources(Me.FilterMenuClientExclude, "FilterMenuClientExclude")
        '
        'FilterByRequestToolStripMenuItem
        '
        Me.FilterByRequestToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterMenuRequestNone, Me.FilterMenuRequestInclude, Me.FilterMenuRequestExclude})
        Me.FilterByRequestToolStripMenuItem.Name = "FilterByRequestToolStripMenuItem"
        resources.ApplyResources(Me.FilterByRequestToolStripMenuItem, "FilterByRequestToolStripMenuItem")
        '
        'FilterMenuRequestNone
        '
        Me.FilterMenuRequestNone.Checked = True
        Me.FilterMenuRequestNone.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FilterMenuRequestNone.Name = "FilterMenuRequestNone"
        resources.ApplyResources(Me.FilterMenuRequestNone, "FilterMenuRequestNone")
        '
        'FilterMenuRequestInclude
        '
        Me.FilterMenuRequestInclude.CheckOnClick = True
        Me.FilterMenuRequestInclude.Name = "FilterMenuRequestInclude"
        resources.ApplyResources(Me.FilterMenuRequestInclude, "FilterMenuRequestInclude")
        '
        'FilterMenuRequestExclude
        '
        Me.FilterMenuRequestExclude.CheckOnClick = True
        Me.FilterMenuRequestExclude.Name = "FilterMenuRequestExclude"
        resources.ApplyResources(Me.FilterMenuRequestExclude, "FilterMenuRequestExclude")
        '
        'PollTimer
        '
        Me.PollTimer.Enabled = True
        Me.PollTimer.Interval = 1000
        '
        'EventLogSubForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.EventLogDataGridView)
        Me.Controls.Add(Me.Toolbar)
        Me.Name = "EventLogSubForm"
        CType(Me.Toolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Toolbar.ResumeLayout(False)
        Me.Toolbar.PerformLayout()
        CType(Me.EventLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EventLogDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FilterMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EventLogBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Toolbar As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolbarAddNewButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarCountLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolbarDeleteButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarMoveFirstButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarMovePreviousButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolbarPositionLabel As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolbarSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolbarMoveNextButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarMoveLastButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolbarSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolbarSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EventLogDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents PollTimer As System.Windows.Forms.Timer
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventDescriptionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConsultantNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SiteNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gvcRequestNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobDateColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FilterMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FilterByDateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuAllDates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuDateToday As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuYesterday As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuPastWeek As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuPastMonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterByConsultantToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuConsultantNone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuConsultantInclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuConsultantExclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterByClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClientNone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClientInclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuClientExclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterByRequestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuRequestNone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuRequestInclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterMenuRequestExclude As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip

End Class
