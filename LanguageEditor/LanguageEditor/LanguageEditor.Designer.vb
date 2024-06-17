<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LanguageEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LanguageEditor))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.FormList = New System.Windows.Forms.TreeView
        Me.TreeNodeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.LabelList = New System.Windows.Forms.DataGridView
        Me.CustomLabelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CustomisationTables = New activiser.Library.activiserWebService.LanguageDataSet
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.MenuStrip = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileSaveMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileSaveAllMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileSaveAsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileSaveClientItemsAsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.FileExitMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.FindReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.CustomLabelTemplates = New activiser.LanguageEditor.CustomStringsDataSet
        Me.ModuleNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.StringNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DefaultValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ResetToDefaultButton = New System.Windows.Forms.DataGridViewButtonColumn
        Me.ValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ClientKeyColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LanguageIdColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CreatedDateTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ModifiedDateTimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        CType(Me.LabelList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomLabelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomisationTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        CType(Me.CustomLabelTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer
        '
        resources.ApplyResources(Me.SplitContainer, "SplitContainer")
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.FormList)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.LabelList)
        Me.SplitContainer.Panel2.Controls.Add(Me.Splitter1)
        Me.SplitContainer.Panel2.Controls.Add(Me.TextBox1)
        Me.SplitContainer.Panel2.Controls.Add(Me.BindingNavigator1)
        '
        'FormList
        '
        resources.ApplyResources(Me.FormList, "FormList")
        Me.FormList.HideSelection = False
        Me.FormList.ImageList = Me.TreeNodeImageList
        Me.FormList.LabelEdit = True
        Me.FormList.Name = "FormList"
        Me.FormList.ShowLines = False
        '
        'TreeNodeImageList
        '
        Me.TreeNodeImageList.ImageStream = CType(resources.GetObject("TreeNodeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TreeNodeImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TreeNodeImageList.Images.SetKeyName(0, "ClosedFolder")
        Me.TreeNodeImageList.Images.SetKeyName(1, "OpenFolder")
        '
        'LabelList
        '
        Me.LabelList.AllowUserToOrderColumns = True
        Me.LabelList.AutoGenerateColumns = False
        Me.LabelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.LabelList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ModuleNameColumn, Me.StringNameColumn, Me.DefaultValueColumn, Me.ResetToDefaultButton, Me.ValueColumn, Me.ClientKeyColumn, Me.LanguageIdColumn, Me.CreatedDateTimeColumn, Me.ModifiedDateTimeColumn})
        Me.LabelList.DataSource = Me.CustomLabelBindingSource
        resources.ApplyResources(Me.LabelList, "LabelList")
        Me.LabelList.Name = "LabelList"
        '
        'CustomLabelBindingSource
        '
        Me.CustomLabelBindingSource.DataMember = "StringValue"
        Me.CustomLabelBindingSource.DataSource = Me.CustomisationTables
        Me.CustomLabelBindingSource.Sort = "ClientKey, LanguageId, ModuleName, StringName"
        '
        'CustomisationTables
        '
        Me.CustomisationTables.DataSetName = "CustomisationTables"
        Me.CustomisationTables.EnforceConstraints = False
        Me.CustomisationTables.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CustomLabelBindingSource, "Value", True))
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.Name = "TextBox1"
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.BindingNavigator1.BindingSource = Me.CustomLabelBindingSource
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
        resources.ApplyResources(Me.BindingNavigator1, "BindingNavigator1")
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem})
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.BindingNavigatorAddNewItem, "BindingNavigatorAddNewItem")
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        resources.ApplyResources(Me.BindingNavigatorCountItem, "BindingNavigatorCountItem")
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.BindingNavigatorDeleteItem, "BindingNavigatorDeleteItem")
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
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
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolStripMenuItem1})
        resources.ApplyResources(Me.MenuStrip, "MenuStrip")
        Me.MenuStrip.Name = "MenuStrip"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileSaveMenuItem, Me.FileSaveAllMenuItem, Me.FileSaveAsMenuItem, Me.FileSaveClientItemsAsMenuItem, Me.ImportToolStripMenuItem, Me.ToolStripSeparator2, Me.FileExitMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'FileSaveMenuItem
        '
        Me.FileSaveMenuItem.Image = Global.activiser.LanguageEditor.My.Resources.Resources.saveHS
        resources.ApplyResources(Me.FileSaveMenuItem, "FileSaveMenuItem")
        Me.FileSaveMenuItem.Name = "FileSaveMenuItem"
        '
        'FileSaveAllMenuItem
        '
        Me.FileSaveAllMenuItem.Image = Global.activiser.LanguageEditor.My.Resources.Resources.SaveAllHS
        Me.FileSaveAllMenuItem.Name = "FileSaveAllMenuItem"
        resources.ApplyResources(Me.FileSaveAllMenuItem, "FileSaveAllMenuItem")
        '
        'FileSaveAsMenuItem
        '
        Me.FileSaveAsMenuItem.Image = Global.activiser.LanguageEditor.My.Resources.Resources.SaveAsWebPageHS
        Me.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem"
        resources.ApplyResources(Me.FileSaveAsMenuItem, "FileSaveAsMenuItem")
        '
        'FileSaveClientItemsAsMenuItem
        '
        Me.FileSaveClientItemsAsMenuItem.Image = Global.activiser.LanguageEditor.My.Resources.Resources.SaveAsWebPageHS
        Me.FileSaveClientItemsAsMenuItem.Name = "FileSaveClientItemsAsMenuItem"
        resources.ApplyResources(Me.FileSaveClientItemsAsMenuItem, "FileSaveClientItemsAsMenuItem")
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        resources.ApplyResources(Me.ImportToolStripMenuItem, "ImportToolStripMenuItem")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'FileExitMenuItem
        '
        Me.FileExitMenuItem.Name = "FileExitMenuItem"
        resources.ApplyResources(Me.FileExitMenuItem, "FileExitMenuItem")
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FindReplaceToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        resources.ApplyResources(Me.ToolStripMenuItem1, "ToolStripMenuItem1")
        '
        'FindReplaceToolStripMenuItem
        '
        Me.FindReplaceToolStripMenuItem.Name = "FindReplaceToolStripMenuItem"
        resources.ApplyResources(Me.FindReplaceToolStripMenuItem, "FindReplaceToolStripMenuItem")
        '
        'CustomLabelTemplates
        '
        Me.CustomLabelTemplates.DataSetName = "CustomLabelTemplate"
        Me.CustomLabelTemplates.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ModuleNameColumn
        '
        Me.ModuleNameColumn.DataPropertyName = "ModuleName"
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText
        Me.ModuleNameColumn.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.ModuleNameColumn, "ModuleNameColumn")
        Me.ModuleNameColumn.Name = "ModuleNameColumn"
        Me.ModuleNameColumn.ReadOnly = True
        '
        'StringNameColumn
        '
        Me.StringNameColumn.DataPropertyName = "StringName"
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.InfoText
        Me.StringNameColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.StringNameColumn.FillWeight = 50.0!
        resources.ApplyResources(Me.StringNameColumn, "StringNameColumn")
        Me.StringNameColumn.Name = "StringNameColumn"
        Me.StringNameColumn.ReadOnly = True
        '
        'DefaultValueColumn
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle3.NullValue = "<None>"
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DefaultValueColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.DefaultValueColumn.FillWeight = 50.0!
        resources.ApplyResources(Me.DefaultValueColumn, "DefaultValueColumn")
        Me.DefaultValueColumn.Name = "DefaultValueColumn"
        Me.DefaultValueColumn.ReadOnly = True
        '
        'ResetToDefaultButton
        '
        Me.ResetToDefaultButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle4.NullValue = Nothing
        Me.ResetToDefaultButton.DefaultCellStyle = DataGridViewCellStyle4
        Me.ResetToDefaultButton.FillWeight = 20.0!
        resources.ApplyResources(Me.ResetToDefaultButton, "ResetToDefaultButton")
        Me.ResetToDefaultButton.Name = "ResetToDefaultButton"
        Me.ResetToDefaultButton.Text = "Reset"
        Me.ResetToDefaultButton.UseColumnTextForButtonValue = True
        '
        'ValueColumn
        '
        Me.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ValueColumn.DataPropertyName = "Value"
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ValueColumn.DefaultCellStyle = DataGridViewCellStyle5
        resources.ApplyResources(Me.ValueColumn, "ValueColumn")
        Me.ValueColumn.MaxInputLength = 255
        Me.ValueColumn.Name = "ValueColumn"
        '
        'ClientKeyColumn
        '
        Me.ClientKeyColumn.DataPropertyName = "ClientKey"
        Me.ClientKeyColumn.FillWeight = 5.0!
        resources.ApplyResources(Me.ClientKeyColumn, "ClientKeyColumn")
        Me.ClientKeyColumn.Name = "ClientKeyColumn"
        Me.ClientKeyColumn.ReadOnly = True
        '
        'LanguageIdColumn
        '
        Me.LanguageIdColumn.DataPropertyName = "LanguageId"
        Me.LanguageIdColumn.FillWeight = 5.0!
        resources.ApplyResources(Me.LanguageIdColumn, "LanguageIdColumn")
        Me.LanguageIdColumn.Name = "LanguageIdColumn"
        Me.LanguageIdColumn.ReadOnly = True
        '
        'CreatedDateTimeColumn
        '
        Me.CreatedDateTimeColumn.DataPropertyName = "CreatedDateTime"
        resources.ApplyResources(Me.CreatedDateTimeColumn, "CreatedDateTimeColumn")
        Me.CreatedDateTimeColumn.Name = "CreatedDateTimeColumn"
        '
        'ModifiedDateTimeColumn
        '
        Me.ModifiedDateTimeColumn.DataPropertyName = "ModifiedDateTime"
        resources.ApplyResources(Me.ModifiedDateTimeColumn, "ModifiedDateTimeColumn")
        Me.ModifiedDateTimeColumn.Name = "ModifiedDateTimeColumn"
        '
        'LanguageEditor
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer)
        Me.Controls.Add(Me.MenuStrip)
        Me.Name = "LanguageEditor"
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        Me.SplitContainer.ResumeLayout(False)
        CType(Me.LabelList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomLabelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomisationTables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        CType(Me.CustomLabelTemplates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TreeNodeImageList As System.Windows.Forms.ImageList
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSaveMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileExitMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents FormList As System.Windows.Forms.TreeView
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents FileSaveAsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomisationTables As Library.activiserWebService.LanguageDataSet
    Friend WithEvents CustomLabelTemplates As CustomStringsDataSet
    Friend WithEvents FileSaveClientItemsAsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSaveAllMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomLabelBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LabelList As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ModuleNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StringNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefaultValueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResetToDefaultButton As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents ValueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientKeyColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LanguageIdColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreatedDateTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModifiedDateTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
