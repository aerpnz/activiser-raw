<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsForm))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.DoneButton = New System.Windows.Forms.Button
        Me.AbortButton = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.RefreshOptionsTab = New System.Windows.Forms.TabPage
        Me.AutomaticRefreshGroupLabel = New System.Windows.Forms.GroupBox
        Me.EnableRefreshCheckBox = New System.Windows.Forms.CheckBox
        Me.RefreshIntervalLabel = New System.Windows.Forms.Label
        Me.NotifyOnUpdatesCheckBox = New System.Windows.Forms.CheckBox
        Me.RefreshInterval = New System.Windows.Forms.NumericUpDown
        Me.EventDescriptionsTab = New System.Windows.Forms.TabPage
        Me.EventTypeDataGridView = New System.Windows.Forms.DataGridView
        Me.EventTypeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventDescriptionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EventLogDataSet = New Library.activiserWebService.EventLogDataSet
        Me.EventDescriptionNotesGroupLabel = New System.Windows.Forms.GroupBox
        Me.EventArgument0Definition = New System.Windows.Forms.Label
        Me.EventArgument3Definition = New System.Windows.Forms.Label
        Me.EventArgument1Definition = New System.Windows.Forms.Label
        Me.EventArgument2Definition = New System.Windows.Forms.Label
        Me.EventTypeToolbar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.EventTypeToolbarCountLabel = New System.Windows.Forms.ToolStripLabel
        Me.EventTypeToolbarMoveFirstButton = New System.Windows.Forms.ToolStripButton
        Me.EventTypeToolbarMovePreviousButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.EventTypeToolbarPositionButton = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.EventTypeToolbarMoveNextButton = New System.Windows.Forms.ToolStripButton
        Me.EventTypeToolbarMoveLastButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.EventTypeToolbarSaveButton = New System.Windows.Forms.ToolStripButton
        Me.ServerConnectionTab = New System.Windows.Forms.TabPage
        Me.TestResultGroupLabel = New System.Windows.Forms.GroupBox
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.ServerUrlGroupLabel = New System.Windows.Forms.GroupBox
        Me.ServerTimeoutUpDown = New System.Windows.Forms.DomainUpDown
        Me.ServerTimeoutLabel = New System.Windows.Forms.Label
        Me.ServerUrlTestButton = New System.Windows.Forms.Button
        Me.ServerUrlTextBox = New System.Windows.Forms.TextBox
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.IgnoreCertificateErrorsCheckBox = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.RefreshOptionsTab.SuspendLayout()
        Me.AutomaticRefreshGroupLabel.SuspendLayout()
        CType(Me.RefreshInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EventDescriptionsTab.SuspendLayout()
        CType(Me.EventTypeDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EventTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EventLogDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EventDescriptionNotesGroupLabel.SuspendLayout()
        CType(Me.EventTypeToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EventTypeToolbar.SuspendLayout()
        Me.ServerConnectionTab.SuspendLayout()
        Me.TestResultGroupLabel.SuspendLayout()
        Me.ServerUrlGroupLabel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.DoneButton, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AbortButton, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'DoneButton
        '
        resources.ApplyResources(Me.DoneButton, "DoneButton")
        Me.DoneButton.Name = "DoneButton"
        '
        'AbortButton
        '
        resources.ApplyResources(Me.AbortButton, "AbortButton")
        Me.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.AbortButton.Name = "AbortButton"
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.RefreshOptionsTab)
        Me.TabControl1.Controls.Add(Me.EventDescriptionsTab)
        Me.TabControl1.Controls.Add(Me.ServerConnectionTab)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'RefreshOptionsTab
        '
        Me.RefreshOptionsTab.Controls.Add(Me.AutomaticRefreshGroupLabel)
        resources.ApplyResources(Me.RefreshOptionsTab, "RefreshOptionsTab")
        Me.RefreshOptionsTab.Name = "RefreshOptionsTab"
        Me.RefreshOptionsTab.UseVisualStyleBackColor = True
        '
        'AutomaticRefreshGroupLabel
        '
        resources.ApplyResources(Me.AutomaticRefreshGroupLabel, "AutomaticRefreshGroupLabel")
        Me.AutomaticRefreshGroupLabel.Controls.Add(Me.EnableRefreshCheckBox)
        Me.AutomaticRefreshGroupLabel.Controls.Add(Me.RefreshIntervalLabel)
        Me.AutomaticRefreshGroupLabel.Controls.Add(Me.NotifyOnUpdatesCheckBox)
        Me.AutomaticRefreshGroupLabel.Controls.Add(Me.RefreshInterval)
        Me.AutomaticRefreshGroupLabel.Name = "AutomaticRefreshGroupLabel"
        Me.AutomaticRefreshGroupLabel.TabStop = False
        '
        'EnableRefreshCheckBox
        '
        resources.ApplyResources(Me.EnableRefreshCheckBox, "EnableRefreshCheckBox")
        Me.EnableRefreshCheckBox.Checked = Global.activiser.Console.My.MySettings.Default.EnableDatabasePolling
        Me.EnableRefreshCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableRefreshCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.Console.My.MySettings.Default, "EnableDatabasePolling", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.EnableRefreshCheckBox.Name = "EnableRefreshCheckBox"
        Me.EnableRefreshCheckBox.UseVisualStyleBackColor = True
        '
        'RefreshIntervalLabel
        '
        resources.ApplyResources(Me.RefreshIntervalLabel, "RefreshIntervalLabel")
        Me.RefreshIntervalLabel.Name = "RefreshIntervalLabel"
        '
        'NotifyOnUpdatesCheckBox
        '
        resources.ApplyResources(Me.NotifyOnUpdatesCheckBox, "NotifyOnUpdatesCheckBox")
        Me.NotifyOnUpdatesCheckBox.Checked = Global.activiser.Console.My.MySettings.Default.NotificationEnabled
        Me.NotifyOnUpdatesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.NotifyOnUpdatesCheckBox.Name = "NotifyOnUpdatesCheckBox"
        '
        'RefreshInterval
        '
        Me.RefreshInterval.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Global.activiser.Console.My.MySettings.Default, "EnableDatabasePolling", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RefreshInterval.Enabled = Global.activiser.Console.My.MySettings.Default.EnableDatabasePolling
        resources.ApplyResources(Me.RefreshInterval, "RefreshInterval")
        Me.RefreshInterval.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.RefreshInterval.Minimum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.RefreshInterval.Name = "RefreshInterval"
        Me.RefreshInterval.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'EventDescriptionsTab
        '
        Me.EventDescriptionsTab.Controls.Add(Me.EventTypeDataGridView)
        Me.EventDescriptionsTab.Controls.Add(Me.EventDescriptionNotesGroupLabel)
        Me.EventDescriptionsTab.Controls.Add(Me.EventTypeToolbar)
        resources.ApplyResources(Me.EventDescriptionsTab, "EventDescriptionsTab")
        Me.EventDescriptionsTab.Name = "EventDescriptionsTab"
        Me.EventDescriptionsTab.UseVisualStyleBackColor = True
        '
        'EventTypeDataGridView
        '
        Me.EventTypeDataGridView.AllowUserToAddRows = False
        Me.EventTypeDataGridView.AllowUserToDeleteRows = False
        Me.EventTypeDataGridView.AutoGenerateColumns = False
        Me.EventTypeDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EventTypeColumn, Me.EventDescriptionColumn})
        Me.EventTypeDataGridView.DataSource = Me.EventTypeBindingSource
        resources.ApplyResources(Me.EventTypeDataGridView, "EventTypeDataGridView")
        Me.EventTypeDataGridView.Name = "EventTypeDataGridView"
        Me.EventTypeDataGridView.RowHeadersVisible = False
        '
        'EventTypeColumn
        '
        Me.EventTypeColumn.DataPropertyName = "EventType"
        resources.ApplyResources(Me.EventTypeColumn, "EventTypeColumn")
        Me.EventTypeColumn.Name = "EventTypeColumn"
        '
        'EventDescriptionColumn
        '
        Me.EventDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.EventDescriptionColumn.DataPropertyName = "EventDescription"
        resources.ApplyResources(Me.EventDescriptionColumn, "EventDescriptionColumn")
        Me.EventDescriptionColumn.Name = "EventDescriptionColumn"
        '
        'EventLogDataSet
        '
        Me.EventLogDataSet.DataSetName = "EventLogDataSet"
        Me.EventLogDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'EventDescriptionNotesGroupLabel
        '
        Me.EventDescriptionNotesGroupLabel.Controls.Add(Me.EventArgument0Definition)
        Me.EventDescriptionNotesGroupLabel.Controls.Add(Me.EventArgument3Definition)
        Me.EventDescriptionNotesGroupLabel.Controls.Add(Me.EventArgument1Definition)
        Me.EventDescriptionNotesGroupLabel.Controls.Add(Me.EventArgument2Definition)
        resources.ApplyResources(Me.EventDescriptionNotesGroupLabel, "EventDescriptionNotesGroupLabel")
        Me.EventDescriptionNotesGroupLabel.Name = "EventDescriptionNotesGroupLabel"
        Me.EventDescriptionNotesGroupLabel.TabStop = False
        '
        'EventArgument0Definition
        '
        resources.ApplyResources(Me.EventArgument0Definition, "EventArgument0Definition")
        Me.EventArgument0Definition.Name = "EventArgument0Definition"
        '
        'EventArgument3Definition
        '
        resources.ApplyResources(Me.EventArgument3Definition, "EventArgument3Definition")
        Me.EventArgument3Definition.Name = "EventArgument3Definition"
        '
        'EventArgument1Definition
        '
        resources.ApplyResources(Me.EventArgument1Definition, "EventArgument1Definition")
        Me.EventArgument1Definition.Name = "EventArgument1Definition"
        '
        'EventArgument2Definition
        '
        resources.ApplyResources(Me.EventArgument2Definition, "EventArgument2Definition")
        Me.EventArgument2Definition.Name = "EventArgument2Definition"
        '
        'EventTypeToolbar
        '
        Me.EventTypeToolbar.AddNewItem = Nothing
        Me.EventTypeToolbar.BindingSource = Me.EventTypeBindingSource
        Me.EventTypeToolbar.CountItem = Me.EventTypeToolbarCountLabel
        Me.EventTypeToolbar.DeleteItem = Nothing
        resources.ApplyResources(Me.EventTypeToolbar, "EventTypeToolbar")
        Me.EventTypeToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EventTypeToolbarMoveFirstButton, Me.EventTypeToolbarMovePreviousButton, Me.ToolStripSeparator2, Me.EventTypeToolbarPositionButton, Me.EventTypeToolbarCountLabel, Me.ToolStripSeparator3, Me.EventTypeToolbarMoveNextButton, Me.EventTypeToolbarMoveLastButton, Me.ToolStripSeparator1, Me.EventTypeToolbarSaveButton})
        Me.EventTypeToolbar.MoveFirstItem = Me.EventTypeToolbarMoveFirstButton
        Me.EventTypeToolbar.MoveLastItem = Me.EventTypeToolbarMoveLastButton
        Me.EventTypeToolbar.MoveNextItem = Me.EventTypeToolbarMoveNextButton
        Me.EventTypeToolbar.MovePreviousItem = Me.EventTypeToolbarMovePreviousButton
        Me.EventTypeToolbar.Name = "EventTypeToolbar"
        Me.EventTypeToolbar.PositionItem = Me.EventTypeToolbarPositionButton
        '
        'EventTypeToolbarCountLabel
        '
        Me.EventTypeToolbarCountLabel.Name = "EventTypeToolbarCountLabel"
        resources.ApplyResources(Me.EventTypeToolbarCountLabel, "EventTypeToolbarCountLabel")
        '
        'EventTypeToolbarMoveFirstButton
        '
        Me.EventTypeToolbarMoveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EventTypeToolbarMoveFirstButton, "EventTypeToolbarMoveFirstButton")
        Me.EventTypeToolbarMoveFirstButton.Name = "EventTypeToolbarMoveFirstButton"
        '
        'EventTypeToolbarMovePreviousButton
        '
        Me.EventTypeToolbarMovePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EventTypeToolbarMovePreviousButton, "EventTypeToolbarMovePreviousButton")
        Me.EventTypeToolbarMovePreviousButton.Name = "EventTypeToolbarMovePreviousButton"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'EventTypeToolbarPositionButton
        '
        resources.ApplyResources(Me.EventTypeToolbarPositionButton, "EventTypeToolbarPositionButton")
        Me.EventTypeToolbarPositionButton.Name = "EventTypeToolbarPositionButton"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'EventTypeToolbarMoveNextButton
        '
        Me.EventTypeToolbarMoveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EventTypeToolbarMoveNextButton, "EventTypeToolbarMoveNextButton")
        Me.EventTypeToolbarMoveNextButton.Name = "EventTypeToolbarMoveNextButton"
        '
        'EventTypeToolbarMoveLastButton
        '
        Me.EventTypeToolbarMoveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EventTypeToolbarMoveLastButton, "EventTypeToolbarMoveLastButton")
        Me.EventTypeToolbarMoveLastButton.Name = "EventTypeToolbarMoveLastButton"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'EventTypeToolbarSaveButton
        '
        Me.EventTypeToolbarSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EventTypeToolbarSaveButton, "EventTypeToolbarSaveButton")
        Me.EventTypeToolbarSaveButton.Name = "EventTypeToolbarSaveButton"
        '
        'ServerConnectionTab
        '
        Me.ServerConnectionTab.Controls.Add(Me.TestResultGroupLabel)
        Me.ServerConnectionTab.Controls.Add(Me.ServerUrlGroupLabel)
        resources.ApplyResources(Me.ServerConnectionTab, "ServerConnectionTab")
        Me.ServerConnectionTab.Name = "ServerConnectionTab"
        Me.ServerConnectionTab.UseVisualStyleBackColor = True
        '
        'TestResultGroupLabel
        '
        Me.TestResultGroupLabel.Controls.Add(Me.WebBrowser1)
        resources.ApplyResources(Me.TestResultGroupLabel, "TestResultGroupLabel")
        Me.TestResultGroupLabel.Name = "TestResultGroupLabel"
        Me.TestResultGroupLabel.TabStop = False
        '
        'WebBrowser1
        '
        Me.WebBrowser1.AllowWebBrowserDrop = False
        resources.ApplyResources(Me.WebBrowser1, "WebBrowser1")
        Me.WebBrowser1.IsWebBrowserContextMenuEnabled = False
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Url = New System.Uri("about:blank", System.UriKind.Absolute)
        Me.WebBrowser1.WebBrowserShortcutsEnabled = False
        '
        'ServerUrlGroupLabel
        '
        Me.ServerUrlGroupLabel.Controls.Add(Me.IgnoreCertificateErrorsCheckBox)
        Me.ServerUrlGroupLabel.Controls.Add(Me.ServerTimeoutUpDown)
        Me.ServerUrlGroupLabel.Controls.Add(Me.ServerTimeoutLabel)
        Me.ServerUrlGroupLabel.Controls.Add(Me.ServerUrlTestButton)
        Me.ServerUrlGroupLabel.Controls.Add(Me.ServerUrlTextBox)
        resources.ApplyResources(Me.ServerUrlGroupLabel, "ServerUrlGroupLabel")
        Me.ServerUrlGroupLabel.Name = "ServerUrlGroupLabel"
        Me.ServerUrlGroupLabel.TabStop = False
        '
        'ServerTimeoutUpDown
        '
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items"))
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items1"))
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items2"))
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items3"))
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items4"))
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items5"))
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items6"))
        Me.ServerTimeoutUpDown.Items.Add(resources.GetString("ServerTimeoutUpDown.Items7"))
        resources.ApplyResources(Me.ServerTimeoutUpDown, "ServerTimeoutUpDown")
        Me.ServerTimeoutUpDown.Name = "ServerTimeoutUpDown"
        '
        'ServerTimeoutLabel
        '
        resources.ApplyResources(Me.ServerTimeoutLabel, "ServerTimeoutLabel")
        Me.ServerTimeoutLabel.Name = "ServerTimeoutLabel"
        '
        'ServerUrlTestButton
        '
        resources.ApplyResources(Me.ServerUrlTestButton, "ServerUrlTestButton")
        Me.ServerUrlTestButton.Name = "ServerUrlTestButton"
        Me.ToolTipProvider.SetToolTip(Me.ServerUrlTestButton, resources.GetString("ServerUrlTestButton.ToolTip"))
        Me.ServerUrlTestButton.UseVisualStyleBackColor = True
        '
        'ServerUrlTextBox
        '
        resources.ApplyResources(Me.ServerUrlTextBox, "ServerUrlTextBox")
        Me.ServerUrlTextBox.Name = "ServerUrlTextBox"
        '
        'IgnoreCertificateErrorsCheckBox
        '
        Me.IgnoreCertificateErrorsCheckBox.Checked = Global.activiser.Console.My.MySettings.Default.IgnoreServerCertificateErrors
        Me.IgnoreCertificateErrorsCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.Console.My.MySettings.Default, "IgnoreServerCertificateErrors", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.IgnoreCertificateErrorsCheckBox, "IgnoreCertificateErrorsCheckBox")
        Me.IgnoreCertificateErrorsCheckBox.Name = "IgnoreCertificateErrorsCheckBox"
        Me.ToolTipProvider.SetToolTip(Me.IgnoreCertificateErrorsCheckBox, resources.GetString("IgnoreCertificateErrorsCheckBox.ToolTip"))
        Me.IgnoreCertificateErrorsCheckBox.UseVisualStyleBackColor = True
        '
        'OptionsForm
        '
        Me.AcceptButton = Me.DoneButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.AbortButton
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.RefreshOptionsTab.ResumeLayout(False)
        Me.AutomaticRefreshGroupLabel.ResumeLayout(False)
        Me.AutomaticRefreshGroupLabel.PerformLayout()
        CType(Me.RefreshInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EventDescriptionsTab.ResumeLayout(False)
        Me.EventDescriptionsTab.PerformLayout()
        CType(Me.EventTypeDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EventTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EventLogDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EventDescriptionNotesGroupLabel.ResumeLayout(False)
        Me.EventDescriptionNotesGroupLabel.PerformLayout()
        CType(Me.EventTypeToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EventTypeToolbar.ResumeLayout(False)
        Me.EventTypeToolbar.PerformLayout()
        Me.ServerConnectionTab.ResumeLayout(False)
        Me.TestResultGroupLabel.ResumeLayout(False)
        Me.ServerUrlGroupLabel.ResumeLayout(False)
        Me.ServerUrlGroupLabel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DoneButton As System.Windows.Forms.Button
    Friend WithEvents AbortButton As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents RefreshOptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents AutomaticRefreshGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents EnableRefreshCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RefreshIntervalLabel As System.Windows.Forms.Label
    Friend WithEvents NotifyOnUpdatesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RefreshInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents EventDescriptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents EventTypeToolbar As System.Windows.Forms.BindingNavigator
    Friend WithEvents EventTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EventLogDataSet As Library.activiserWebService.EventLogDataSet
    Friend WithEvents EventTypeToolbarCountLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents EventTypeToolbarMoveFirstButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EventTypeToolbarMovePreviousButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EventTypeToolbarPositionButton As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EventTypeToolbarMoveNextButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EventTypeToolbarMoveLastButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EventTypeToolbarSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EventTypeDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents EventDescriptionNotesGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents EventArgument0Definition As System.Windows.Forms.Label
    Friend WithEvents EventArgument3Definition As System.Windows.Forms.Label
    Friend WithEvents EventArgument1Definition As System.Windows.Forms.Label
    Friend WithEvents EventArgument2Definition As System.Windows.Forms.Label
    Friend WithEvents ServerConnectionTab As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents ServerUrlGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents ServerUrlTestButton As System.Windows.Forms.Button
    Friend WithEvents ServerUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TestResultGroupLabel As System.Windows.Forms.GroupBox
    Friend WithEvents EventTypeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventDescriptionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents ServerTimeoutLabel As System.Windows.Forms.Label
    Friend WithEvents ServerTimeoutUpDown As System.Windows.Forms.DomainUpDown
    Friend WithEvents IgnoreCertificateErrorsCheckBox As System.Windows.Forms.CheckBox

End Class
