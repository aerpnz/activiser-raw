Imports activiser.Library.activiserWebService

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventLogViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EventLogViewer))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ErrorLogToolBar = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorRefreshItem = New System.Windows.Forms.ToolStripButton
        Me.ErrorLogDataGridView = New System.Windows.Forms.DataGridView
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.MessageTab = New System.Windows.Forms.TabPage
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ExceptionDetailsTab = New System.Windows.Forms.TabPage
        Me.RecordInformationTab = New System.Windows.Forms.TabPage
        Me.DataViewer = New System.Windows.Forms.WebBrowser
        Me.NotesTab = New System.Windows.Forms.TabPage
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox
        Me.EventDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventClass = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LoggedBy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.HostName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SystemId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Notes = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LogDateTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SourceColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MessageColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ActiviserConsoleLogs = New activiser.Library.activiserWebService.EventLogDataSet
        CType(Me.ErrorLogToolBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ErrorLogToolBar.SuspendLayout()
        CType(Me.ErrorLogDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.MessageTab.SuspendLayout()
        Me.ExceptionDetailsTab.SuspendLayout()
        Me.RecordInformationTab.SuspendLayout()
        Me.NotesTab.SuspendLayout()
        CType(Me.EventLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ActiviserConsoleLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorLogToolBar
        '
        Me.ErrorLogToolBar.AddNewItem = Nothing
        Me.ErrorLogToolBar.BindingSource = Me.EventLogBindingSource
        Me.ErrorLogToolBar.CountItem = Me.BindingNavigatorCountItem
        Me.ErrorLogToolBar.DeleteItem = Nothing
        resources.ApplyResources(Me.ErrorLogToolBar, "ErrorLogToolBar")
        Me.ErrorLogToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorRefreshItem})
        Me.ErrorLogToolBar.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.ErrorLogToolBar.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.ErrorLogToolBar.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.ErrorLogToolBar.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.ErrorLogToolBar.Name = "ErrorLogToolBar"
        Me.ErrorLogToolBar.PositionItem = Me.BindingNavigatorPositionItem
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
        'BindingNavigatorRefreshItem
        '
        Me.BindingNavigatorRefreshItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorRefreshItem.Image = Global.activiser.Console.My.Resources.Resources.Refresh
        resources.ApplyResources(Me.BindingNavigatorRefreshItem, "BindingNavigatorRefreshItem")
        Me.BindingNavigatorRefreshItem.Name = "BindingNavigatorRefreshItem"
        '
        'ErrorLogDataGridView
        '
        Me.ErrorLogDataGridView.AllowUserToAddRows = False
        Me.ErrorLogDataGridView.AllowUserToOrderColumns = True
        Me.ErrorLogDataGridView.AutoGenerateColumns = False
        Me.ErrorLogDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LogDateTimeColumn, Me.EventDateTime, Me.EventClass, Me.LoggedBy, Me.HostName, Me.SystemId, Me.SourceColumn, Me.Notes, Me.Status, Me.MessageColumn})
        Me.ErrorLogDataGridView.DataSource = Me.EventLogBindingSource
        resources.ApplyResources(Me.ErrorLogDataGridView, "ErrorLogDataGridView")
        Me.ErrorLogDataGridView.Name = "ErrorLogDataGridView"
        Me.ErrorLogDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        '
        'RichTextBox1
        '
        Me.RichTextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.EventLogBindingSource, "EventData", True))
        resources.ApplyResources(Me.RichTextBox1, "RichTextBox1")
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Text = Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.TabStop = False
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.MessageTab)
        Me.TabControl1.Controls.Add(Me.ExceptionDetailsTab)
        Me.TabControl1.Controls.Add(Me.RecordInformationTab)
        Me.TabControl1.Controls.Add(Me.NotesTab)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        '
        'MessageTab
        '
        Me.MessageTab.Controls.Add(Me.TextBox1)
        resources.ApplyResources(Me.MessageTab, "MessageTab")
        Me.MessageTab.Name = "MessageTab"
        Me.MessageTab.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.EventLogBindingSource, "Message", True))
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        '
        'ExceptionDetailsTab
        '
        Me.ExceptionDetailsTab.Controls.Add(Me.RichTextBox1)
        resources.ApplyResources(Me.ExceptionDetailsTab, "ExceptionDetailsTab")
        Me.ExceptionDetailsTab.Name = "ExceptionDetailsTab"
        Me.ExceptionDetailsTab.UseVisualStyleBackColor = True
        '
        'RecordInformationTab
        '
        Me.RecordInformationTab.Controls.Add(Me.DataViewer)
        resources.ApplyResources(Me.RecordInformationTab, "RecordInformationTab")
        Me.RecordInformationTab.Name = "RecordInformationTab"
        Me.RecordInformationTab.UseVisualStyleBackColor = True
        '
        'DataViewer
        '
        Me.DataViewer.AllowWebBrowserDrop = False
        resources.ApplyResources(Me.DataViewer, "DataViewer")
        Me.DataViewer.MinimumSize = New System.Drawing.Size(20, 20)
        Me.DataViewer.Name = "DataViewer"
        Me.DataViewer.ScriptErrorsSuppressed = True
        '
        'NotesTab
        '
        Me.NotesTab.Controls.Add(Me.RichTextBox2)
        resources.ApplyResources(Me.NotesTab, "NotesTab")
        Me.NotesTab.Name = "NotesTab"
        Me.NotesTab.UseVisualStyleBackColor = True
        '
        'RichTextBox2
        '
        Me.RichTextBox2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.EventLogBindingSource, "Notes", True))
        resources.ApplyResources(Me.RichTextBox2, "RichTextBox2")
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Text = Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo
        '
        'EventDateTime
        '
        Me.EventDateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.EventDateTime.DataPropertyName = "EventDateTime"
        resources.ApplyResources(Me.EventDateTime, "EventDateTime")
        Me.EventDateTime.Name = "EventDateTime"
        Me.EventDateTime.ReadOnly = True
        '
        'EventClass
        '
        Me.EventClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.EventClass.DataPropertyName = "EventClass"
        resources.ApplyResources(Me.EventClass, "EventClass")
        Me.EventClass.Name = "EventClass"
        Me.EventClass.ReadOnly = True
        '
        'LoggedBy
        '
        Me.LoggedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.LoggedBy.DataPropertyName = "LoggedBy"
        resources.ApplyResources(Me.LoggedBy, "LoggedBy")
        Me.LoggedBy.Name = "LoggedBy"
        Me.LoggedBy.ReadOnly = True
        '
        'HostName
        '
        Me.HostName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.HostName.DataPropertyName = "HostName"
        resources.ApplyResources(Me.HostName, "HostName")
        Me.HostName.Name = "HostName"
        Me.HostName.ReadOnly = True
        '
        'SystemId
        '
        Me.SystemId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.SystemId.DataPropertyName = "SystemId"
        resources.ApplyResources(Me.SystemId, "SystemId")
        Me.SystemId.Name = "SystemId"
        Me.SystemId.ReadOnly = True
        '
        'Notes
        '
        Me.Notes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Notes.DataPropertyName = "Notes"
        resources.ApplyResources(Me.Notes, "Notes")
        Me.Notes.Name = "Notes"
        '
        'Status
        '
        Me.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Status.DataPropertyName = "Status"
        resources.ApplyResources(Me.Status, "Status")
        Me.Status.Name = "Status"
        '
        'LogDateTimeColumn
        '
        Me.LogDateTimeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.LogDateTimeColumn.DataPropertyName = "LogDateTime"
        DataGridViewCellStyle1.Format = "u"
        Me.LogDateTimeColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.LogDateTimeColumn.FillWeight = 30.0!
        resources.ApplyResources(Me.LogDateTimeColumn, "LogDateTimeColumn")
        Me.LogDateTimeColumn.Name = "LogDateTimeColumn"
        Me.LogDateTimeColumn.ReadOnly = True
        '
        'SourceColumn
        '
        Me.SourceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.SourceColumn.DataPropertyName = "Source"
        Me.SourceColumn.FillWeight = 50.0!
        resources.ApplyResources(Me.SourceColumn, "SourceColumn")
        Me.SourceColumn.Name = "SourceColumn"
        Me.SourceColumn.ReadOnly = True
        '
        'MessageColumn
        '
        Me.MessageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.MessageColumn.DataPropertyName = "Message"
        Me.MessageColumn.FillWeight = 200.0!
        resources.ApplyResources(Me.MessageColumn, "MessageColumn")
        Me.MessageColumn.Name = "MessageColumn"
        Me.MessageColumn.ReadOnly = True
        '
        'EventLogBindingSource
        '
        Me.EventLogBindingSource.DataMember = Me.ActiviserConsoleLogs.EventLog.TableName
        Me.EventLogBindingSource.DataSource = Me.ActiviserConsoleLogs
        '
        'ActiviserConsoleLogs
        '
        Me.ActiviserConsoleLogs.DataSetName = "activiserConsoleLogs"
        Me.ActiviserConsoleLogs.Locale = New System.Globalization.CultureInfo("en-NZ")
        Me.ActiviserConsoleLogs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'EventLogViewer
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ErrorLogDataGridView)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ErrorLogToolBar)
        Me.Name = "EventLogViewer"
        CType(Me.ErrorLogToolBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ErrorLogToolBar.ResumeLayout(False)
        Me.ErrorLogToolBar.PerformLayout()
        CType(Me.ErrorLogDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.MessageTab.ResumeLayout(False)
        Me.MessageTab.PerformLayout()
        Me.ExceptionDetailsTab.ResumeLayout(False)
        Me.RecordInformationTab.ResumeLayout(False)
        Me.NotesTab.ResumeLayout(False)
        CType(Me.EventLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ActiviserConsoleLogs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ActiviserConsoleLogs As EventLogDataSet
    Friend WithEvents EventLogBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ErrorLogToolBar As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ErrorLogDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BindingNavigatorRefreshItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents ExceptionDetailsTab As System.Windows.Forms.TabPage
    Friend WithEvents RecordInformationTab As System.Windows.Forms.TabPage
    Friend WithEvents DataViewer As System.Windows.Forms.WebBrowser
    Friend WithEvents MessageTab As System.Windows.Forms.TabPage
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ErrorLogIdColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConsultantUidColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ErrorDateTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeviceIDColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TableNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TableUidColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TableDataColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LogDateTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventDateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LoggedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HostName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SystemId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Notes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MessageColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesTab As System.Windows.Forms.TabPage
    Friend WithEvents RichTextBox2 As System.Windows.Forms.RichTextBox
End Class
