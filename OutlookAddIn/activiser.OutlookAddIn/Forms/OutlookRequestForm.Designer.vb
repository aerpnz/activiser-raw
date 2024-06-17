<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1301:AvoidDuplicateAccelerators")> <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OutlookRequestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OutlookRequestForm))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.RequestTab = New System.Windows.Forms.TabPage
        Me.LongDescriptionBox = New System.Windows.Forms.TextBox
        Me.RequestBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ClientDataSet = New activiser.Library.activiserWebService.activiserDataSet
        Me.ReminderPanel = New System.Windows.Forms.Panel
        Me.ReminderCheckBox = New System.Windows.Forms.CheckBox
        Me.ShowTimeAsComboBox = New System.Windows.Forms.ComboBox
        Me.ShowTimeAsLabel = New System.Windows.Forms.Label
        Me.ReminderComboBox = New System.Windows.Forms.ComboBox
        Me.HorizontalLine2 = New System.Windows.Forms.Panel
        Me.TimePanel = New System.Windows.Forms.Panel
        Me.AllDayEventCheckBox = New System.Windows.Forms.CheckBox
        Me.StartTimeLabel = New System.Windows.Forms.Label
        Me.FinishTimeLabel = New System.Windows.Forms.Label
        Me.StartTimePicker = New activiser.Library.DateTimePicker
        Me.FinishTimePicker = New activiser.Library.DateTimePicker
        Me.HorizontalLine1 = New System.Windows.Forms.Panel
        Me.AppointmentHeaderPanel = New System.Windows.Forms.Panel
        Me.ConsultantStatusLabel = New System.Windows.Forms.Label
        Me.ConsultantStatusCombo = New System.Windows.Forms.ComboBox
        Me.RequestSelector = New System.Windows.Forms.ComboBox
        Me.RequestSelectorBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ClientCombo = New System.Windows.Forms.ComboBox
        Me.RequestNumberLabel = New System.Windows.Forms.Label
        Me.ClientLabel = New System.Windows.Forms.Label
        Me.ContactLabel = New System.Windows.Forms.Label
        Me.ContactBox = New System.Windows.Forms.TextBox
        Me.ConsultantLabel = New System.Windows.Forms.Label
        Me.ShortDescriptionLabel = New System.Windows.Forms.Label
        Me.ShortDescriptionBox = New System.Windows.Forms.TextBox
        Me.NextActionDatePicker = New activiser.Library.DateTimePicker
        Me.RequestStatusLabel = New System.Windows.Forms.Label
        Me.RequestStatusCombo = New System.Windows.Forms.ComboBox
        Me.NextActionDateLabel = New System.Windows.Forms.Label
        Me.ConsultantCombo = New System.Windows.Forms.ComboBox
        Me.PastAppointmentPanel = New System.Windows.Forms.Label
        Me.JobTab = New System.Windows.Forms.TabPage
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.JobConsultantColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobIDColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobNumberColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobStartTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobFinishTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.JobDetailsColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FKJobRequestBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RequestNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.RequestNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.RequestNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.RequestNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.RequestNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.RequestNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.RequestNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.RequestNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.RequestNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.RequestNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.SaveAndCloseButton = New System.Windows.Forms.ToolStripButton
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.TabControl1.SuspendLayout()
        Me.RequestTab.SuspendLayout()
        CType(Me.RequestBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClientDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ReminderPanel.SuspendLayout()
        Me.TimePanel.SuspendLayout()
        Me.AppointmentHeaderPanel.SuspendLayout()
        CType(Me.RequestSelectorBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.JobTab.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FKJobRequestBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RequestNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RequestNavigator.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.RequestTab)
        Me.TabControl1.Controls.Add(Me.JobTab)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'RequestTab
        '
        Me.RequestTab.Controls.Add(Me.LongDescriptionBox)
        Me.RequestTab.Controls.Add(Me.ReminderPanel)
        Me.RequestTab.Controls.Add(Me.HorizontalLine2)
        Me.RequestTab.Controls.Add(Me.TimePanel)
        Me.RequestTab.Controls.Add(Me.HorizontalLine1)
        Me.RequestTab.Controls.Add(Me.AppointmentHeaderPanel)
        Me.RequestTab.Controls.Add(Me.PastAppointmentPanel)
        resources.ApplyResources(Me.RequestTab, "RequestTab")
        Me.RequestTab.Name = "RequestTab"
        Me.RequestTab.UseVisualStyleBackColor = True
        '
        'LongDescriptionBox
        '
        Me.LongDescriptionBox.AllowDrop = True
        Me.LongDescriptionBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "LongDescription", True))
        resources.ApplyResources(Me.LongDescriptionBox, "LongDescriptionBox")
        Me.LongDescriptionBox.Name = "LongDescriptionBox"
        Me.ToolTipProvider.SetToolTip(Me.LongDescriptionBox, resources.GetString("LongDescriptionBox.ToolTip"))
        '
        'RequestBindingSource
        '
        Me.RequestBindingSource.DataMember = "Request"
        Me.RequestBindingSource.DataSource = Me.ClientDataSet
        Me.RequestBindingSource.Sort = "RequestNumber, RequestID"
        '
        'ClientDataSet
        '
        Me.ClientDataSet.DataSetName = "ClientDataSet"
        Me.ClientDataSet.Locale = New System.Globalization.CultureInfo("")
        Me.ClientDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReminderPanel
        '
        Me.ReminderPanel.Controls.Add(Me.ReminderCheckBox)
        Me.ReminderPanel.Controls.Add(Me.ShowTimeAsComboBox)
        Me.ReminderPanel.Controls.Add(Me.ShowTimeAsLabel)
        Me.ReminderPanel.Controls.Add(Me.ReminderComboBox)
        resources.ApplyResources(Me.ReminderPanel, "ReminderPanel")
        Me.ReminderPanel.Name = "ReminderPanel"
        '
        'ReminderCheckBox
        '
        resources.ApplyResources(Me.ReminderCheckBox, "ReminderCheckBox")
        Me.ReminderCheckBox.Name = "ReminderCheckBox"
        Me.ReminderCheckBox.UseVisualStyleBackColor = True
        '
        'ShowTimeAsComboBox
        '
        Me.ShowTimeAsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ShowTimeAsComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.ShowTimeAsComboBox, "ShowTimeAsComboBox")
        Me.ShowTimeAsComboBox.Name = "ShowTimeAsComboBox"
        '
        'ShowTimeAsLabel
        '
        resources.ApplyResources(Me.ShowTimeAsLabel, "ShowTimeAsLabel")
        Me.ShowTimeAsLabel.Name = "ShowTimeAsLabel"
        '
        'ReminderComboBox
        '
        Me.ReminderComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.ReminderComboBox, "ReminderComboBox")
        Me.ReminderComboBox.Name = "ReminderComboBox"
        '
        'HorizontalLine2
        '
        Me.HorizontalLine2.BackColor = System.Drawing.SystemColors.ControlDark
        resources.ApplyResources(Me.HorizontalLine2, "HorizontalLine2")
        Me.HorizontalLine2.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.HorizontalLine2.Name = "HorizontalLine2"
        '
        'TimePanel
        '
        Me.TimePanel.Controls.Add(Me.AllDayEventCheckBox)
        Me.TimePanel.Controls.Add(Me.StartTimeLabel)
        Me.TimePanel.Controls.Add(Me.FinishTimeLabel)
        Me.TimePanel.Controls.Add(Me.StartTimePicker)
        Me.TimePanel.Controls.Add(Me.FinishTimePicker)
        resources.ApplyResources(Me.TimePanel, "TimePanel")
        Me.TimePanel.Name = "TimePanel"
        '
        'AllDayEventCheckBox
        '
        resources.ApplyResources(Me.AllDayEventCheckBox, "AllDayEventCheckBox")
        Me.AllDayEventCheckBox.Name = "AllDayEventCheckBox"
        Me.AllDayEventCheckBox.UseVisualStyleBackColor = True
        '
        'StartTimeLabel
        '
        resources.ApplyResources(Me.StartTimeLabel, "StartTimeLabel")
        Me.StartTimeLabel.Name = "StartTimeLabel"
        '
        'FinishTimeLabel
        '
        resources.ApplyResources(Me.FinishTimeLabel, "FinishTimeLabel")
        Me.FinishTimeLabel.Name = "FinishTimeLabel"
        '
        'StartTimePicker
        '
        resources.ApplyResources(Me.StartTimePicker, "StartTimePicker")
        Me.StartTimePicker.Name = "StartTimePicker"
        Me.StartTimePicker.ShowCheckBox = False
        '
        'FinishTimePicker
        '
        resources.ApplyResources(Me.FinishTimePicker, "FinishTimePicker")
        Me.FinishTimePicker.Name = "FinishTimePicker"
        Me.FinishTimePicker.ShowCheckBox = False
        '
        'HorizontalLine1
        '
        Me.HorizontalLine1.BackColor = System.Drawing.SystemColors.ControlDark
        resources.ApplyResources(Me.HorizontalLine1, "HorizontalLine1")
        Me.HorizontalLine1.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.HorizontalLine1.Name = "HorizontalLine1"
        '
        'AppointmentHeaderPanel
        '
        Me.AppointmentHeaderPanel.Controls.Add(Me.ConsultantStatusLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ConsultantStatusCombo)
        Me.AppointmentHeaderPanel.Controls.Add(Me.RequestSelector)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ClientCombo)
        Me.AppointmentHeaderPanel.Controls.Add(Me.RequestNumberLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ClientLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ContactLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ContactBox)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ConsultantLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ShortDescriptionLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ShortDescriptionBox)
        Me.AppointmentHeaderPanel.Controls.Add(Me.NextActionDatePicker)
        Me.AppointmentHeaderPanel.Controls.Add(Me.RequestStatusLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.RequestStatusCombo)
        Me.AppointmentHeaderPanel.Controls.Add(Me.NextActionDateLabel)
        Me.AppointmentHeaderPanel.Controls.Add(Me.ConsultantCombo)
        resources.ApplyResources(Me.AppointmentHeaderPanel, "AppointmentHeaderPanel")
        Me.AppointmentHeaderPanel.Name = "AppointmentHeaderPanel"
        '
        'ConsultantStatusLabel
        '
        resources.ApplyResources(Me.ConsultantStatusLabel, "ConsultantStatusLabel")
        Me.ConsultantStatusLabel.Name = "ConsultantStatusLabel"
        '
        'ConsultantStatusCombo
        '
        Me.ConsultantStatusCombo.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "ConsultantStatusID", True))
        Me.ConsultantStatusCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.ConsultantStatusCombo, "ConsultantStatusCombo")
        Me.ConsultantStatusCombo.FormattingEnabled = True
        Me.ConsultantStatusCombo.Name = "ConsultantStatusCombo"
        '
        'RequestSelector
        '
        Me.RequestSelector.DataSource = Me.RequestSelectorBindingSource
        Me.RequestSelector.DisplayMember = "RequestNumber"
        resources.ApplyResources(Me.RequestSelector, "RequestSelector")
        Me.RequestSelector.FormattingEnabled = True
        Me.RequestSelector.Name = "RequestSelector"
        Me.RequestSelector.ValueMember = "RequestUID"
        '
        'RequestSelectorBindingSource
        '
        Me.RequestSelectorBindingSource.DataMember = "Request"
        Me.RequestSelectorBindingSource.DataSource = Me.ClientDataSet
        Me.RequestSelectorBindingSource.Sort = "RequestNumber, RequestID"
        '
        'ClientCombo
        '
        resources.ApplyResources(Me.ClientCombo, "ClientCombo")
        Me.ClientCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ClientCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ClientCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ClientCombo.FormattingEnabled = True
        Me.ClientCombo.Name = "ClientCombo"
        Me.ToolTipProvider.SetToolTip(Me.ClientCombo, resources.GetString("ClientCombo.ToolTip"))
        '
        'RequestNumberLabel
        '
        resources.ApplyResources(Me.RequestNumberLabel, "RequestNumberLabel")
        Me.RequestNumberLabel.Name = "RequestNumberLabel"
        '
        'ClientLabel
        '
        resources.ApplyResources(Me.ClientLabel, "ClientLabel")
        Me.ClientLabel.Name = "ClientLabel"
        '
        'ContactLabel
        '
        resources.ApplyResources(Me.ContactLabel, "ContactLabel")
        Me.ContactLabel.Name = "ContactLabel"
        '
        'ContactBox
        '
        resources.ApplyResources(Me.ContactBox, "ContactBox")
        Me.ContactBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "Contact", True))
        Me.ContactBox.Name = "ContactBox"
        '
        'ConsultantLabel
        '
        resources.ApplyResources(Me.ConsultantLabel, "ConsultantLabel")
        Me.ConsultantLabel.Name = "ConsultantLabel"
        '
        'ShortDescriptionLabel
        '
        resources.ApplyResources(Me.ShortDescriptionLabel, "ShortDescriptionLabel")
        Me.ShortDescriptionLabel.Name = "ShortDescriptionLabel"
        Me.ToolTipProvider.SetToolTip(Me.ShortDescriptionLabel, resources.GetString("ShortDescriptionLabel.ToolTip"))
        '
        'ShortDescriptionBox
        '
        Me.ShortDescriptionBox.AllowDrop = True
        resources.ApplyResources(Me.ShortDescriptionBox, "ShortDescriptionBox")
        Me.ShortDescriptionBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RequestBindingSource, "ShortDescription", True))
        Me.ShortDescriptionBox.Name = "ShortDescriptionBox"
        Me.ToolTipProvider.SetToolTip(Me.ShortDescriptionBox, resources.GetString("ShortDescriptionBox.ToolTip"))
        '
        'NextActionDatePicker
        '
        resources.ApplyResources(Me.NextActionDatePicker, "NextActionDatePicker")
        Me.NextActionDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.RequestBindingSource, "NextActionDate", True))
        Me.NextActionDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("ShowTime", Global.activiser.OutlookAddIn.MySettings.Default, "IncludeTimeInNextActiondate", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.NextActionDatePicker.Name = "NextActionDatePicker"
        Me.NextActionDatePicker.ShowTime = Global.activiser.OutlookAddIn.MySettings.Default.IncludeTimeInNextActiondate
        '
        'RequestStatusLabel
        '
        resources.ApplyResources(Me.RequestStatusLabel, "RequestStatusLabel")
        Me.RequestStatusLabel.Name = "RequestStatusLabel"
        '
        'RequestStatusCombo
        '
        Me.RequestStatusCombo.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "RequestStatusID", True))
        Me.RequestStatusCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RequestStatusCombo.FormattingEnabled = True
        resources.ApplyResources(Me.RequestStatusCombo, "RequestStatusCombo")
        Me.RequestStatusCombo.Name = "RequestStatusCombo"
        '
        'NextActionDateLabel
        '
        resources.ApplyResources(Me.NextActionDateLabel, "NextActionDateLabel")
        Me.NextActionDateLabel.Name = "NextActionDateLabel"
        '
        'ConsultantCombo
        '
        resources.ApplyResources(Me.ConsultantCombo, "ConsultantCombo")
        Me.ConsultantCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ConsultantCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ConsultantCombo.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.RequestBindingSource, "AssignedToUID", True))
        Me.ConsultantCombo.FormattingEnabled = True
        Me.ConsultantCombo.Name = "ConsultantCombo"
        '
        'PastAppointmentPanel
        '
        Me.PastAppointmentPanel.BackColor = System.Drawing.Color.Gray
        resources.ApplyResources(Me.PastAppointmentPanel, "PastAppointmentPanel")
        Me.PastAppointmentPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PastAppointmentPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PastAppointmentPanel.Name = "PastAppointmentPanel"
        '
        'JobTab
        '
        Me.JobTab.Controls.Add(Me.DataGridView1)
        resources.ApplyResources(Me.JobTab, "JobTab")
        Me.JobTab.Name = "JobTab"
        Me.JobTab.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.JobConsultantColumn, Me.JobIDColumn, Me.JobNumberColumn, Me.JobStartTimeColumn, Me.JobFinishTimeColumn, Me.JobDetailsColumn})
        Me.DataGridView1.DataSource = Me.FKJobRequestBindingSource
        resources.ApplyResources(Me.DataGridView1, "DataGridView1")
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        '
        'JobConsultantColumn
        '
        Me.JobConsultantColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.JobConsultantColumn.DataPropertyName = "ConsultantName"
        resources.ApplyResources(Me.JobConsultantColumn, "JobConsultantColumn")
        Me.JobConsultantColumn.Name = "JobConsultantColumn"
        Me.JobConsultantColumn.ReadOnly = True
        '
        'JobIDColumn
        '
        Me.JobIDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.JobIDColumn.DataPropertyName = "JobID"
        resources.ApplyResources(Me.JobIDColumn, "JobIDColumn")
        Me.JobIDColumn.Name = "JobIDColumn"
        Me.JobIDColumn.ReadOnly = True
        '
        'JobNumberColumn
        '
        Me.JobNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.JobNumberColumn.DataPropertyName = "JobNumber"
        resources.ApplyResources(Me.JobNumberColumn, "JobNumberColumn")
        Me.JobNumberColumn.Name = "JobNumberColumn"
        Me.JobNumberColumn.ReadOnly = True
        '
        'JobStartTimeColumn
        '
        Me.JobStartTimeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.JobStartTimeColumn.DataPropertyName = "StartTime"
        resources.ApplyResources(Me.JobStartTimeColumn, "JobStartTimeColumn")
        Me.JobStartTimeColumn.Name = "JobStartTimeColumn"
        Me.JobStartTimeColumn.ReadOnly = True
        '
        'JobFinishTimeColumn
        '
        Me.JobFinishTimeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.JobFinishTimeColumn.DataPropertyName = "FinishTime"
        resources.ApplyResources(Me.JobFinishTimeColumn, "JobFinishTimeColumn")
        Me.JobFinishTimeColumn.Name = "JobFinishTimeColumn"
        Me.JobFinishTimeColumn.ReadOnly = True
        '
        'JobDetailsColumn
        '
        Me.JobDetailsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.JobDetailsColumn.DataPropertyName = "JobDetails"
        resources.ApplyResources(Me.JobDetailsColumn, "JobDetailsColumn")
        Me.JobDetailsColumn.Name = "JobDetailsColumn"
        Me.JobDetailsColumn.ReadOnly = True
        '
        'FKJobRequestBindingSource
        '
        Me.FKJobRequestBindingSource.DataMember = "FK_Job_Request"
        Me.FKJobRequestBindingSource.DataSource = Me.RequestBindingSource
        Me.FKJobRequestBindingSource.Sort = "StartTime"
        '
        'RequestNavigator
        '
        Me.RequestNavigator.AddNewItem = Nothing
        Me.RequestNavigator.BindingSource = Me.RequestBindingSource
        Me.RequestNavigator.CountItem = Me.RequestNavigatorCountItem
        Me.RequestNavigator.DeleteItem = Nothing
        resources.ApplyResources(Me.RequestNavigator, "RequestNavigator")
        Me.RequestNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RequestNavigatorMoveFirstItem, Me.RequestNavigatorMovePreviousItem, Me.RequestNavigatorSeparator, Me.RequestNavigatorPositionItem, Me.RequestNavigatorCountItem, Me.RequestNavigatorSeparator1, Me.RequestNavigatorMoveNextItem, Me.RequestNavigatorMoveLastItem, Me.RequestNavigatorSeparator2})
        Me.RequestNavigator.MoveFirstItem = Me.RequestNavigatorMoveFirstItem
        Me.RequestNavigator.MoveLastItem = Me.RequestNavigatorMoveLastItem
        Me.RequestNavigator.MoveNextItem = Me.RequestNavigatorMoveNextItem
        Me.RequestNavigator.MovePreviousItem = Me.RequestNavigatorMovePreviousItem
        Me.RequestNavigator.Name = "RequestNavigator"
        Me.RequestNavigator.PositionItem = Me.RequestNavigatorPositionItem
        '
        'RequestNavigatorCountItem
        '
        Me.RequestNavigatorCountItem.Name = "RequestNavigatorCountItem"
        resources.ApplyResources(Me.RequestNavigatorCountItem, "RequestNavigatorCountItem")
        '
        'RequestNavigatorMoveFirstItem
        '
        Me.RequestNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestNavigatorMoveFirstItem, "RequestNavigatorMoveFirstItem")
        Me.RequestNavigatorMoveFirstItem.Name = "RequestNavigatorMoveFirstItem"
        '
        'RequestNavigatorMovePreviousItem
        '
        Me.RequestNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestNavigatorMovePreviousItem, "RequestNavigatorMovePreviousItem")
        Me.RequestNavigatorMovePreviousItem.Name = "RequestNavigatorMovePreviousItem"
        '
        'RequestNavigatorSeparator
        '
        Me.RequestNavigatorSeparator.Name = "RequestNavigatorSeparator"
        resources.ApplyResources(Me.RequestNavigatorSeparator, "RequestNavigatorSeparator")
        '
        'RequestNavigatorPositionItem
        '
        resources.ApplyResources(Me.RequestNavigatorPositionItem, "RequestNavigatorPositionItem")
        Me.RequestNavigatorPositionItem.Name = "RequestNavigatorPositionItem"
        '
        'RequestNavigatorSeparator1
        '
        Me.RequestNavigatorSeparator1.Name = "RequestNavigatorSeparator1"
        resources.ApplyResources(Me.RequestNavigatorSeparator1, "RequestNavigatorSeparator1")
        '
        'RequestNavigatorMoveNextItem
        '
        Me.RequestNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestNavigatorMoveNextItem, "RequestNavigatorMoveNextItem")
        Me.RequestNavigatorMoveNextItem.Name = "RequestNavigatorMoveNextItem"
        '
        'RequestNavigatorMoveLastItem
        '
        Me.RequestNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.RequestNavigatorMoveLastItem, "RequestNavigatorMoveLastItem")
        Me.RequestNavigatorMoveLastItem.Name = "RequestNavigatorMoveLastItem"
        '
        'RequestNavigatorSeparator2
        '
        Me.RequestNavigatorSeparator2.Name = "RequestNavigatorSeparator2"
        resources.ApplyResources(Me.RequestNavigatorSeparator2, "RequestNavigatorSeparator2")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ToolStripMenuItem1, Me.SaveToolStripMenuItem, Me.toolStripSeparator1, Me.DeleteToolStripMenuItem, Me.toolStripSeparator, Me.PrintToolStripMenuItem, Me.PrintPreviewToolStripMenuItem, Me.toolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'NewToolStripMenuItem
        '
        resources.ApplyResources(Me.NewToolStripMenuItem, "NewToolStripMenuItem")
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        resources.ApplyResources(Me.ToolStripMenuItem1, "ToolStripMenuItem1")
        '
        'SaveToolStripMenuItem
        '
        resources.ApplyResources(Me.SaveToolStripMenuItem, "SaveToolStripMenuItem")
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        resources.ApplyResources(Me.toolStripSeparator1, "toolStripSeparator1")
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = Global.activiser.OutlookAddIn.My.Resources.Resources.DeleteHS
        resources.ApplyResources(Me.DeleteToolStripMenuItem, "DeleteToolStripMenuItem")
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        resources.ApplyResources(Me.toolStripSeparator, "toolStripSeparator")
        '
        'PrintToolStripMenuItem
        '
        resources.ApplyResources(Me.PrintToolStripMenuItem, "PrintToolStripMenuItem")
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        '
        'PrintPreviewToolStripMenuItem
        '
        resources.ApplyResources(Me.PrintPreviewToolStripMenuItem, "PrintPreviewToolStripMenuItem")
        Me.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        resources.ApplyResources(Me.toolStripSeparator2, "toolStripSeparator2")
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.toolStripSeparator3, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.toolStripSeparator4, Me.SelectAllToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        resources.ApplyResources(Me.UndoToolStripMenuItem, "UndoToolStripMenuItem")
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        resources.ApplyResources(Me.RedoToolStripMenuItem, "RedoToolStripMenuItem")
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        resources.ApplyResources(Me.toolStripSeparator3, "toolStripSeparator3")
        '
        'CutToolStripMenuItem
        '
        resources.ApplyResources(Me.CutToolStripMenuItem, "CutToolStripMenuItem")
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        '
        'CopyToolStripMenuItem
        '
        resources.ApplyResources(Me.CopyToolStripMenuItem, "CopyToolStripMenuItem")
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        '
        'PasteToolStripMenuItem
        '
        resources.ApplyResources(Me.PasteToolStripMenuItem, "PasteToolStripMenuItem")
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        resources.ApplyResources(Me.toolStripSeparator4, "toolStripSeparator4")
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        resources.ApplyResources(Me.SelectAllToolStripMenuItem, "SelectAllToolStripMenuItem")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.IndexToolStripMenuItem, Me.SearchToolStripMenuItem, Me.toolStripSeparator5, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        resources.ApplyResources(Me.ContentsToolStripMenuItem, "ContentsToolStripMenuItem")
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        resources.ApplyResources(Me.IndexToolStripMenuItem, "IndexToolStripMenuItem")
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        resources.ApplyResources(Me.SearchToolStripMenuItem, "SearchToolStripMenuItem")
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        resources.ApplyResources(Me.toolStripSeparator5, "toolStripSeparator5")
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveAndCloseButton, Me.PrintToolStripButton, Me.toolStripSeparator6, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton, Me.toolStripSeparator7, Me.HelpToolStripButton})
        resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
        Me.ToolStrip1.Name = "ToolStrip1"
        '
        'SaveAndCloseButton
        '
        resources.ApplyResources(Me.SaveAndCloseButton, "SaveAndCloseButton")
        Me.SaveAndCloseButton.Name = "SaveAndCloseButton"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.PrintToolStripButton, "PrintToolStripButton")
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        '
        'toolStripSeparator6
        '
        Me.toolStripSeparator6.Name = "toolStripSeparator6"
        resources.ApplyResources(Me.toolStripSeparator6, "toolStripSeparator6")
        '
        'CutToolStripButton
        '
        Me.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.CutToolStripButton, "CutToolStripButton")
        Me.CutToolStripButton.Name = "CutToolStripButton"
        '
        'CopyToolStripButton
        '
        Me.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.CopyToolStripButton, "CopyToolStripButton")
        Me.CopyToolStripButton.Name = "CopyToolStripButton"
        '
        'PasteToolStripButton
        '
        Me.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.PasteToolStripButton, "PasteToolStripButton")
        Me.PasteToolStripButton.Name = "PasteToolStripButton"
        '
        'toolStripSeparator7
        '
        Me.toolStripSeparator7.Name = "toolStripSeparator7"
        resources.ApplyResources(Me.toolStripSeparator7, "toolStripSeparator7")
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.HelpToolStripButton, "HelpToolStripButton")
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        '
        'OutlookRequestForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.RequestNavigator)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "OutlookRequestForm"
        Me.TopMost = True
        Me.TabControl1.ResumeLayout(False)
        Me.RequestTab.ResumeLayout(False)
        Me.RequestTab.PerformLayout()
        CType(Me.RequestBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClientDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ReminderPanel.ResumeLayout(False)
        Me.ReminderPanel.PerformLayout()
        Me.TimePanel.ResumeLayout(False)
        Me.TimePanel.PerformLayout()
        Me.AppointmentHeaderPanel.ResumeLayout(False)
        Me.AppointmentHeaderPanel.PerformLayout()
        CType(Me.RequestSelectorBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.JobTab.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FKJobRequestBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RequestNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RequestNavigator.ResumeLayout(False)
        Me.RequestNavigator.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents RequestTab As System.Windows.Forms.TabPage
    Friend WithEvents JobTab As System.Windows.Forms.TabPage
    Friend WithEvents ClientCombo As System.Windows.Forms.ComboBox
    Friend WithEvents ConsultantCombo As System.Windows.Forms.ComboBox
    Friend WithEvents RequestBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LongDescriptionBox As System.Windows.Forms.TextBox
    Friend WithEvents ShortDescriptionBox As System.Windows.Forms.TextBox
    Friend WithEvents ShortDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents ContactBox As System.Windows.Forms.TextBox
    Friend WithEvents ContactLabel As System.Windows.Forms.Label
    Friend WithEvents ClientLabel As System.Windows.Forms.Label
    Friend WithEvents ConsultantLabel As System.Windows.Forms.Label
    Friend WithEvents RequestNumberLabel As System.Windows.Forms.Label
    Friend WithEvents RequestStatusCombo As System.Windows.Forms.ComboBox
    Friend WithEvents RequestStatusLabel As System.Windows.Forms.Label
    Friend WithEvents NextActionDateLabel As System.Windows.Forms.Label
    Friend WithEvents StartTimeLabel As System.Windows.Forms.Label
    Friend WithEvents FinishTimeLabel As System.Windows.Forms.Label
    Friend WithEvents FinishTimePicker As activiser.Library.DateTimePicker
    Friend WithEvents StartTimePicker As activiser.Library.DateTimePicker
    Friend WithEvents NextActionDatePicker As activiser.Library.DateTimePicker
    Friend WithEvents ClientDataSet As Library.activiserWebService.activiserDataSet
    Friend WithEvents AllDayEventCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents FKJobRequestBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RequestSelector As System.Windows.Forms.ComboBox
    Friend WithEvents RequestNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents RequestNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents RequestNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RequestNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents RequestNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RequestNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents RequestNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowTimeAsLabel As System.Windows.Forms.Label
    Friend WithEvents ShowTimeAsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ReminderComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ReminderCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents HorizontalLine1 As System.Windows.Forms.Panel
    Friend WithEvents HorizontalLine2 As System.Windows.Forms.Panel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveAndCloseButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CopyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PasteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PastAppointmentPanel As System.Windows.Forms.Label
    Friend WithEvents AppointmentHeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents TimePanel As System.Windows.Forms.Panel
    Friend WithEvents ReminderPanel As System.Windows.Forms.Panel
    Friend WithEvents RequestSelectorBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ConsultantStatusLabel As System.Windows.Forms.Label
    Friend WithEvents ConsultantStatusCombo As System.Windows.Forms.ComboBox
    Friend WithEvents JobConsultantColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobIDColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobNumberColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobStartTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobFinishTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobDetailsColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
