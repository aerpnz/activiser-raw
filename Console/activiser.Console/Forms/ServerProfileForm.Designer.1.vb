<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerProfileForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerProfileForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.SecurityTab = New System.Windows.Forms.TabPage
        Me.ClientRegistrationPanel = New System.Windows.Forms.Panel
        Me.ServerProfileDataGridView = New System.Windows.Forms.DataGridView
        Me.AssigneeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ClientStatusBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ClientRegistrationDataSet1 = New activiser.Library.activiserWebService.ClientRegistrationDataSet
        Me.ClientBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DeviceRegistrationExplanation = New System.Windows.Forms.TextBox
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.RegistrationTab = New System.Windows.Forms.TabPage
        Me.RegistrationPanel = New System.Windows.Forms.Panel
        Me.LicenseDetailsGroup = New System.Windows.Forms.GroupBox
        Me.RegistrationDetailsBox = New System.Windows.Forms.TextBox
        Me.LicenseEntryGroup = New System.Windows.Forms.GroupBox
        Me.LicenseeTextBox = New System.Windows.Forms.TextBox
        Me.checkLicenseKeyButton = New System.Windows.Forms.Button
        Me.lblLicense = New System.Windows.Forms.Label
        Me.ClientNameLabel = New System.Windows.Forms.Label
        Me.LicenseKeyTextBox = New System.Windows.Forms.TextBox
        Me.RegistrationExplanation = New System.Windows.Forms.TextBox
        Me.MailServerTab = New System.Windows.Forms.TabPage
        Me.MailServerPanel = New System.Windows.Forms.Panel
        Me.MailServerExplanation = New System.Windows.Forms.TextBox
        Me.TestGroup = New System.Windows.Forms.GroupBox
        Me.MailServerTestButton = New System.Windows.Forms.Button
        Me.MailServerTestResults = New System.Windows.Forms.TextBox
        Me.MailServerHeaderPanel = New System.Windows.Forms.Panel
        Me.MailServerPortNumber = New System.Windows.Forms.NumericUpDown
        Me.MailServerPortLabel = New System.Windows.Forms.Label
        Me.MailTestToLabel = New System.Windows.Forms.Label
        Me.MailServerLabel = New System.Windows.Forms.Label
        Me.MailServerAddressBox = New System.Windows.Forms.TextBox
        Me.MailTestToAddressBox = New System.Windows.Forms.TextBox
        Me.MailTestFromAddressBox = New System.Windows.Forms.TextBox
        Me.MailTestFromLabel = New System.Windows.Forms.Label
        Me.MobileAlertTab = New System.Windows.Forms.TabPage
        Me.MobileAlertPanel = New System.Windows.Forms.Panel
        Me.SmsTemplateGroup = New System.Windows.Forms.GroupBox
        Me.SmsMessageTemplateTextBox = New System.Windows.Forms.TextBox
        Me.SmsMessageExplanation = New System.Windows.Forms.TextBox
        Me.SmsAddressTemplateGroup = New System.Windows.Forms.GroupBox
        Me.SmsAddressTemplateTextBox = New System.Windows.Forms.TextBox
        Me.SmsAddressTemplateLabel = New System.Windows.Forms.Label
        Me.EnableSMSPanel = New System.Windows.Forms.Panel
        Me.SmsEnabledCheckBox = New System.Windows.Forms.CheckBox
        Me.MobileAlertInstructions = New System.Windows.Forms.TextBox
        Me.MailTemplateTab = New System.Windows.Forms.TabPage
        Me.JobMailMessageTemplateGroup = New System.Windows.Forms.GroupBox
        Me.MailTemplateBox = New System.Windows.Forms.RichTextBox
        Me.JobMailSubjectGroup = New System.Windows.Forms.GroupBox
        Me.SubjectTemplateBox = New System.Windows.Forms.TextBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.MailTemplateExplanation = New System.Windows.Forms.TextBox
        Me.okButton = New System.Windows.Forms.Button
        Me.ButtonPanel = New System.Windows.Forms.Panel
        Me.cancelDialogButton = New System.Windows.Forms.Button
        Me.ClientIdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SystemIdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AssignedToColumn = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.ClientStatusColumn = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.NotesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ValidAfterDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ValidBeforeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CreatedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CreatedByDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ModifiedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ModifiedByDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ApplyButton = New System.Windows.Forms.Button
        Me.TabControl.SuspendLayout()
        Me.SecurityTab.SuspendLayout()
        Me.ClientRegistrationPanel.SuspendLayout()
        CType(Me.ServerProfileDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AssigneeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClientStatusBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClientRegistrationDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClientBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.RegistrationTab.SuspendLayout()
        Me.RegistrationPanel.SuspendLayout()
        Me.LicenseDetailsGroup.SuspendLayout()
        Me.LicenseEntryGroup.SuspendLayout()
        Me.MailServerTab.SuspendLayout()
        Me.MailServerPanel.SuspendLayout()
        Me.TestGroup.SuspendLayout()
        Me.MailServerHeaderPanel.SuspendLayout()
        CType(Me.MailServerPortNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MobileAlertTab.SuspendLayout()
        Me.MobileAlertPanel.SuspendLayout()
        Me.SmsTemplateGroup.SuspendLayout()
        Me.SmsAddressTemplateGroup.SuspendLayout()
        Me.EnableSMSPanel.SuspendLayout()
        Me.MailTemplateTab.SuspendLayout()
        Me.JobMailMessageTemplateGroup.SuspendLayout()
        Me.JobMailSubjectGroup.SuspendLayout()
        Me.ButtonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.SecurityTab)
        Me.TabControl.Controls.Add(Me.RegistrationTab)
        Me.TabControl.Controls.Add(Me.MailServerTab)
        Me.TabControl.Controls.Add(Me.MobileAlertTab)
        Me.TabControl.Controls.Add(Me.MailTemplateTab)
        resources.ApplyResources(Me.TabControl, "TabControl")
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        '
        'SecurityTab
        '
        resources.ApplyResources(Me.SecurityTab, "SecurityTab")
        Me.SecurityTab.Controls.Add(Me.ClientRegistrationPanel)
        Me.SecurityTab.Name = "SecurityTab"
        Me.SecurityTab.UseVisualStyleBackColor = True
        '
        'ClientRegistrationPanel
        '
        Me.ClientRegistrationPanel.Controls.Add(Me.ServerProfileDataGridView)
        Me.ClientRegistrationPanel.Controls.Add(Me.DeviceRegistrationExplanation)
        Me.ClientRegistrationPanel.Controls.Add(Me.BindingNavigator1)
        resources.ApplyResources(Me.ClientRegistrationPanel, "ClientRegistrationPanel")
        Me.ClientRegistrationPanel.Name = "ClientRegistrationPanel"
        '
        'ServerProfileDataGridView
        '
        Me.ServerProfileDataGridView.AllowUserToDeleteRows = False
        Me.ServerProfileDataGridView.AutoGenerateColumns = False
        Me.ServerProfileDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ClientIdDataGridViewTextBoxColumn, Me.SystemIdDataGridViewTextBoxColumn, Me.AssignedToColumn, Me.ClientStatusColumn, Me.NotesDataGridViewTextBoxColumn, Me.ValidAfterDataGridViewTextBoxColumn, Me.ValidBeforeDataGridViewTextBoxColumn, Me.CreatedDataGridViewTextBoxColumn, Me.CreatedByDataGridViewTextBoxColumn, Me.ModifiedDataGridViewTextBoxColumn, Me.ModifiedByDataGridViewTextBoxColumn})
        Me.ServerProfileDataGridView.DataSource = Me.ClientBindingSource
        resources.ApplyResources(Me.ServerProfileDataGridView, "ServerProfileDataGridView")
        Me.ServerProfileDataGridView.Name = "ServerProfileDataGridView"
        '
        'ClientStatusBindingSource
        '
        Me.ClientStatusBindingSource.DataMember = "ClientStatus"
        Me.ClientStatusBindingSource.DataSource = Me.ClientRegistrationDataSet1
        '
        'ClientRegistrationDataSet1
        '
        Me.ClientRegistrationDataSet1.DataSetName = "ClientRegistrationDataSet"
        Me.ClientRegistrationDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ClientBindingSource
        '
        Me.ClientBindingSource.DataMember = "Client"
        Me.ClientBindingSource.DataSource = Me.ClientRegistrationDataSet1
        Me.ClientBindingSource.Filter = Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo
        '
        'DeviceRegistrationExplanation
        '
        Me.DeviceRegistrationExplanation.BackColor = System.Drawing.SystemColors.Control
        Me.DeviceRegistrationExplanation.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.DeviceRegistrationExplanation, "DeviceRegistrationExplanation")
        Me.DeviceRegistrationExplanation.Name = "DeviceRegistrationExplanation"
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.BindingNavigator1.BindingSource = Me.ClientBindingSource
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        resources.ApplyResources(Me.BindingNavigator1, "BindingNavigator1")
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem})
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
        'RegistrationTab
        '
        Me.RegistrationTab.Controls.Add(Me.RegistrationPanel)
        resources.ApplyResources(Me.RegistrationTab, "RegistrationTab")
        Me.RegistrationTab.Name = "RegistrationTab"
        Me.RegistrationTab.UseVisualStyleBackColor = True
        '
        'RegistrationPanel
        '
        Me.RegistrationPanel.Controls.Add(Me.LicenseDetailsGroup)
        Me.RegistrationPanel.Controls.Add(Me.LicenseEntryGroup)
        Me.RegistrationPanel.Controls.Add(Me.RegistrationExplanation)
        resources.ApplyResources(Me.RegistrationPanel, "RegistrationPanel")
        Me.RegistrationPanel.Name = "RegistrationPanel"
        '
        'LicenseDetailsGroup
        '
        Me.LicenseDetailsGroup.Controls.Add(Me.RegistrationDetailsBox)
        resources.ApplyResources(Me.LicenseDetailsGroup, "LicenseDetailsGroup")
        Me.LicenseDetailsGroup.Name = "LicenseDetailsGroup"
        Me.LicenseDetailsGroup.TabStop = False
        '
        'RegistrationDetailsBox
        '
        Me.RegistrationDetailsBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.RegistrationDetailsBox, "RegistrationDetailsBox")
        Me.RegistrationDetailsBox.Name = "RegistrationDetailsBox"
        Me.RegistrationDetailsBox.ReadOnly = True
        '
        'LicenseEntryGroup
        '
        Me.LicenseEntryGroup.Controls.Add(Me.LicenseeTextBox)
        Me.LicenseEntryGroup.Controls.Add(Me.checkLicenseKeyButton)
        Me.LicenseEntryGroup.Controls.Add(Me.lblLicense)
        Me.LicenseEntryGroup.Controls.Add(Me.ClientNameLabel)
        Me.LicenseEntryGroup.Controls.Add(Me.LicenseKeyTextBox)
        resources.ApplyResources(Me.LicenseEntryGroup, "LicenseEntryGroup")
        Me.LicenseEntryGroup.Name = "LicenseEntryGroup"
        Me.LicenseEntryGroup.TabStop = False
        '
        'LicenseeTextBox
        '
        resources.ApplyResources(Me.LicenseeTextBox, "LicenseeTextBox")
        Me.LicenseeTextBox.Name = "LicenseeTextBox"
        '
        'checkLicenseKeyButton
        '
        resources.ApplyResources(Me.checkLicenseKeyButton, "checkLicenseKeyButton")
        Me.checkLicenseKeyButton.Name = "checkLicenseKeyButton"
        Me.checkLicenseKeyButton.UseVisualStyleBackColor = True
        '
        'lblLicense
        '
        resources.ApplyResources(Me.lblLicense, "lblLicense")
        Me.lblLicense.Name = "lblLicense"
        '
        'ClientNameLabel
        '
        resources.ApplyResources(Me.ClientNameLabel, "ClientNameLabel")
        Me.ClientNameLabel.Name = "ClientNameLabel"
        '
        'LicenseKeyTextBox
        '
        resources.ApplyResources(Me.LicenseKeyTextBox, "LicenseKeyTextBox")
        Me.LicenseKeyTextBox.Name = "LicenseKeyTextBox"
        '
        'RegistrationExplanation
        '
        Me.RegistrationExplanation.BackColor = System.Drawing.SystemColors.Control
        Me.RegistrationExplanation.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.RegistrationExplanation, "RegistrationExplanation")
        Me.RegistrationExplanation.Name = "RegistrationExplanation"
        '
        'MailServerTab
        '
        Me.MailServerTab.Controls.Add(Me.MailServerPanel)
        resources.ApplyResources(Me.MailServerTab, "MailServerTab")
        Me.MailServerTab.Name = "MailServerTab"
        Me.MailServerTab.UseVisualStyleBackColor = True
        '
        'MailServerPanel
        '
        Me.MailServerPanel.Controls.Add(Me.MailServerExplanation)
        Me.MailServerPanel.Controls.Add(Me.TestGroup)
        Me.MailServerPanel.Controls.Add(Me.MailServerHeaderPanel)
        resources.ApplyResources(Me.MailServerPanel, "MailServerPanel")
        Me.MailServerPanel.Name = "MailServerPanel"
        '
        'MailServerExplanation
        '
        Me.MailServerExplanation.BackColor = System.Drawing.SystemColors.Control
        Me.MailServerExplanation.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.MailServerExplanation, "MailServerExplanation")
        Me.MailServerExplanation.Name = "MailServerExplanation"
        '
        'TestGroup
        '
        Me.TestGroup.Controls.Add(Me.MailServerTestButton)
        Me.TestGroup.Controls.Add(Me.MailServerTestResults)
        resources.ApplyResources(Me.TestGroup, "TestGroup")
        Me.TestGroup.Name = "TestGroup"
        Me.TestGroup.TabStop = False
        '
        'MailServerTestButton
        '
        resources.ApplyResources(Me.MailServerTestButton, "MailServerTestButton")
        Me.MailServerTestButton.Name = "MailServerTestButton"
        Me.MailServerTestButton.UseVisualStyleBackColor = True
        '
        'MailServerTestResults
        '
        resources.ApplyResources(Me.MailServerTestResults, "MailServerTestResults")
        Me.MailServerTestResults.Name = "MailServerTestResults"
        Me.MailServerTestResults.ReadOnly = True
        '
        'MailServerHeaderPanel
        '
        Me.MailServerHeaderPanel.Controls.Add(Me.MailServerPortNumber)
        Me.MailServerHeaderPanel.Controls.Add(Me.MailServerPortLabel)
        Me.MailServerHeaderPanel.Controls.Add(Me.MailTestToLabel)
        Me.MailServerHeaderPanel.Controls.Add(Me.MailServerLabel)
        Me.MailServerHeaderPanel.Controls.Add(Me.MailServerAddressBox)
        Me.MailServerHeaderPanel.Controls.Add(Me.MailTestToAddressBox)
        Me.MailServerHeaderPanel.Controls.Add(Me.MailTestFromAddressBox)
        Me.MailServerHeaderPanel.Controls.Add(Me.MailTestFromLabel)
        resources.ApplyResources(Me.MailServerHeaderPanel, "MailServerHeaderPanel")
        Me.MailServerHeaderPanel.Name = "MailServerHeaderPanel"
        '
        'MailServerPortNumber
        '
        resources.ApplyResources(Me.MailServerPortNumber, "MailServerPortNumber")
        Me.MailServerPortNumber.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.MailServerPortNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.MailServerPortNumber.Name = "MailServerPortNumber"
        Me.MailServerPortNumber.Value = New Decimal(New Integer() {25, 0, 0, 0})
        '
        'MailServerPortLabel
        '
        resources.ApplyResources(Me.MailServerPortLabel, "MailServerPortLabel")
        Me.MailServerPortLabel.Name = "MailServerPortLabel"
        '
        'MailTestToLabel
        '
        resources.ApplyResources(Me.MailTestToLabel, "MailTestToLabel")
        Me.MailTestToLabel.Name = "MailTestToLabel"
        '
        'MailServerLabel
        '
        resources.ApplyResources(Me.MailServerLabel, "MailServerLabel")
        Me.MailServerLabel.Name = "MailServerLabel"
        '
        'MailServerAddressBox
        '
        resources.ApplyResources(Me.MailServerAddressBox, "MailServerAddressBox")
        Me.MailServerAddressBox.Name = "MailServerAddressBox"
        '
        'MailTestToAddressBox
        '
        resources.ApplyResources(Me.MailTestToAddressBox, "MailTestToAddressBox")
        Me.MailTestToAddressBox.Name = "MailTestToAddressBox"
        '
        'MailTestFromAddressBox
        '
        resources.ApplyResources(Me.MailTestFromAddressBox, "MailTestFromAddressBox")
        Me.MailTestFromAddressBox.Name = "MailTestFromAddressBox"
        '
        'MailTestFromLabel
        '
        resources.ApplyResources(Me.MailTestFromLabel, "MailTestFromLabel")
        Me.MailTestFromLabel.Name = "MailTestFromLabel"
        '
        'MobileAlertTab
        '
        Me.MobileAlertTab.Controls.Add(Me.MobileAlertPanel)
        resources.ApplyResources(Me.MobileAlertTab, "MobileAlertTab")
        Me.MobileAlertTab.Name = "MobileAlertTab"
        Me.MobileAlertTab.UseVisualStyleBackColor = True
        '
        'MobileAlertPanel
        '
        Me.MobileAlertPanel.Controls.Add(Me.SmsTemplateGroup)
        Me.MobileAlertPanel.Controls.Add(Me.SmsAddressTemplateGroup)
        Me.MobileAlertPanel.Controls.Add(Me.EnableSMSPanel)
        Me.MobileAlertPanel.Controls.Add(Me.MobileAlertInstructions)
        resources.ApplyResources(Me.MobileAlertPanel, "MobileAlertPanel")
        Me.MobileAlertPanel.Name = "MobileAlertPanel"
        '
        'SmsTemplateGroup
        '
        Me.SmsTemplateGroup.Controls.Add(Me.SmsMessageTemplateTextBox)
        Me.SmsTemplateGroup.Controls.Add(Me.SmsMessageExplanation)
        resources.ApplyResources(Me.SmsTemplateGroup, "SmsTemplateGroup")
        Me.SmsTemplateGroup.Name = "SmsTemplateGroup"
        Me.SmsTemplateGroup.TabStop = False
        '
        'SmsMessageTemplateTextBox
        '
        resources.ApplyResources(Me.SmsMessageTemplateTextBox, "SmsMessageTemplateTextBox")
        Me.SmsMessageTemplateTextBox.Name = "SmsMessageTemplateTextBox"
        '
        'SmsMessageExplanation
        '
        Me.SmsMessageExplanation.BackColor = System.Drawing.SystemColors.Control
        Me.SmsMessageExplanation.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.SmsMessageExplanation, "SmsMessageExplanation")
        Me.SmsMessageExplanation.Name = "SmsMessageExplanation"
        '
        'SmsAddressTemplateGroup
        '
        Me.SmsAddressTemplateGroup.Controls.Add(Me.SmsAddressTemplateTextBox)
        Me.SmsAddressTemplateGroup.Controls.Add(Me.SmsAddressTemplateLabel)
        resources.ApplyResources(Me.SmsAddressTemplateGroup, "SmsAddressTemplateGroup")
        Me.SmsAddressTemplateGroup.Name = "SmsAddressTemplateGroup"
        Me.SmsAddressTemplateGroup.TabStop = False
        '
        'SmsAddressTemplateTextBox
        '
        resources.ApplyResources(Me.SmsAddressTemplateTextBox, "SmsAddressTemplateTextBox")
        Me.SmsAddressTemplateTextBox.Name = "SmsAddressTemplateTextBox"
        '
        'SmsAddressTemplateLabel
        '
        resources.ApplyResources(Me.SmsAddressTemplateLabel, "SmsAddressTemplateLabel")
        Me.SmsAddressTemplateLabel.Name = "SmsAddressTemplateLabel"
        '
        'EnableSMSPanel
        '
        Me.EnableSMSPanel.Controls.Add(Me.SmsEnabledCheckBox)
        resources.ApplyResources(Me.EnableSMSPanel, "EnableSMSPanel")
        Me.EnableSMSPanel.Name = "EnableSMSPanel"
        '
        'SmsEnabledCheckBox
        '
        resources.ApplyResources(Me.SmsEnabledCheckBox, "SmsEnabledCheckBox")
        Me.SmsEnabledCheckBox.Name = "SmsEnabledCheckBox"
        Me.SmsEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'MobileAlertInstructions
        '
        Me.MobileAlertInstructions.BackColor = System.Drawing.SystemColors.Control
        Me.MobileAlertInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.MobileAlertInstructions, "MobileAlertInstructions")
        Me.MobileAlertInstructions.Name = "MobileAlertInstructions"
        '
        'MailTemplateTab
        '
        Me.MailTemplateTab.Controls.Add(Me.JobMailMessageTemplateGroup)
        Me.MailTemplateTab.Controls.Add(Me.JobMailSubjectGroup)
        Me.MailTemplateTab.Controls.Add(Me.Splitter1)
        Me.MailTemplateTab.Controls.Add(Me.MailTemplateExplanation)
        resources.ApplyResources(Me.MailTemplateTab, "MailTemplateTab")
        Me.MailTemplateTab.Name = "MailTemplateTab"
        Me.MailTemplateTab.UseVisualStyleBackColor = True
        '
        'JobMailMessageTemplateGroup
        '
        Me.JobMailMessageTemplateGroup.Controls.Add(Me.MailTemplateBox)
        resources.ApplyResources(Me.JobMailMessageTemplateGroup, "JobMailMessageTemplateGroup")
        Me.JobMailMessageTemplateGroup.Name = "JobMailMessageTemplateGroup"
        Me.JobMailMessageTemplateGroup.TabStop = False
        '
        'MailTemplateBox
        '
        resources.ApplyResources(Me.MailTemplateBox, "MailTemplateBox")
        Me.MailTemplateBox.Name = "MailTemplateBox"
        Me.MailTemplateBox.Text = Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo
        '
        'JobMailSubjectGroup
        '
        Me.JobMailSubjectGroup.Controls.Add(Me.SubjectTemplateBox)
        resources.ApplyResources(Me.JobMailSubjectGroup, "JobMailSubjectGroup")
        Me.JobMailSubjectGroup.Name = "JobMailSubjectGroup"
        Me.JobMailSubjectGroup.TabStop = False
        '
        'SubjectTemplateBox
        '
        resources.ApplyResources(Me.SubjectTemplateBox, "SubjectTemplateBox")
        Me.SubjectTemplateBox.Name = "SubjectTemplateBox"
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.TabStop = False
        '
        'MailTemplateExplanation
        '
        Me.MailTemplateExplanation.BackColor = System.Drawing.SystemColors.Control
        Me.MailTemplateExplanation.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.MailTemplateExplanation, "MailTemplateExplanation")
        Me.MailTemplateExplanation.Name = "MailTemplateExplanation"
        '
        'okButton
        '
        resources.ApplyResources(Me.okButton, "okButton")
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okButton.Name = "okButton"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'ButtonPanel
        '
        Me.ButtonPanel.Controls.Add(Me.ApplyButton)
        Me.ButtonPanel.Controls.Add(Me.cancelDialogButton)
        Me.ButtonPanel.Controls.Add(Me.okButton)
        resources.ApplyResources(Me.ButtonPanel, "ButtonPanel")
        Me.ButtonPanel.Name = "ButtonPanel"
        '
        'cancelDialogButton
        '
        resources.ApplyResources(Me.cancelDialogButton, "cancelDialogButton")
        Me.cancelDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelDialogButton.Name = "cancelDialogButton"
        Me.cancelDialogButton.UseVisualStyleBackColor = True
        '
        'ClientIdDataGridViewTextBoxColumn
        '
        Me.ClientIdDataGridViewTextBoxColumn.DataPropertyName = "ClientId"
        resources.ApplyResources(Me.ClientIdDataGridViewTextBoxColumn, "ClientIdDataGridViewTextBoxColumn")
        Me.ClientIdDataGridViewTextBoxColumn.Name = "ClientIdDataGridViewTextBoxColumn"
        '
        'SystemIdDataGridViewTextBoxColumn
        '
        Me.SystemIdDataGridViewTextBoxColumn.DataPropertyName = "SystemId"
        resources.ApplyResources(Me.SystemIdDataGridViewTextBoxColumn, "SystemIdDataGridViewTextBoxColumn")
        Me.SystemIdDataGridViewTextBoxColumn.Name = "SystemIdDataGridViewTextBoxColumn"
        '
        'AssignedToColumn
        '
        Me.AssignedToColumn.DataPropertyName = "AssignedToId"
        Me.AssignedToColumn.DataSource = Me.AssigneeBindingSource
        resources.ApplyResources(Me.AssignedToColumn, "AssignedToColumn")
        Me.AssignedToColumn.Name = "AssignedToColumn"
        Me.AssignedToColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AssignedToColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ClientStatusColumn
        '
        Me.ClientStatusColumn.DataPropertyName = "ClientStatusId"
        Me.ClientStatusColumn.DataSource = Me.ClientStatusBindingSource
        Me.ClientStatusColumn.DisplayMember = "Description"
        resources.ApplyResources(Me.ClientStatusColumn, "ClientStatusColumn")
        Me.ClientStatusColumn.Name = "ClientStatusColumn"
        Me.ClientStatusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ClientStatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ClientStatusColumn.ValueMember = "ClientStatusId"
        '
        'NotesDataGridViewTextBoxColumn
        '
        Me.NotesDataGridViewTextBoxColumn.DataPropertyName = "Notes"
        resources.ApplyResources(Me.NotesDataGridViewTextBoxColumn, "NotesDataGridViewTextBoxColumn")
        Me.NotesDataGridViewTextBoxColumn.Name = "NotesDataGridViewTextBoxColumn"
        '
        'ValidAfterDataGridViewTextBoxColumn
        '
        Me.ValidAfterDataGridViewTextBoxColumn.DataPropertyName = "ValidAfter"
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.ValidAfterDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.ValidAfterDataGridViewTextBoxColumn, "ValidAfterDataGridViewTextBoxColumn")
        Me.ValidAfterDataGridViewTextBoxColumn.MaxInputLength = 20
        Me.ValidAfterDataGridViewTextBoxColumn.Name = "ValidAfterDataGridViewTextBoxColumn"
        '
        'ValidBeforeDataGridViewTextBoxColumn
        '
        Me.ValidBeforeDataGridViewTextBoxColumn.DataPropertyName = "ValidBefore"
        DataGridViewCellStyle2.Format = "d"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.ValidBeforeDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        resources.ApplyResources(Me.ValidBeforeDataGridViewTextBoxColumn, "ValidBeforeDataGridViewTextBoxColumn")
        Me.ValidBeforeDataGridViewTextBoxColumn.MaxInputLength = 20
        Me.ValidBeforeDataGridViewTextBoxColumn.Name = "ValidBeforeDataGridViewTextBoxColumn"
        '
        'CreatedDataGridViewTextBoxColumn
        '
        Me.CreatedDataGridViewTextBoxColumn.DataPropertyName = "Created"
        resources.ApplyResources(Me.CreatedDataGridViewTextBoxColumn, "CreatedDataGridViewTextBoxColumn")
        Me.CreatedDataGridViewTextBoxColumn.Name = "CreatedDataGridViewTextBoxColumn"
        '
        'CreatedByDataGridViewTextBoxColumn
        '
        Me.CreatedByDataGridViewTextBoxColumn.DataPropertyName = "CreatedBy"
        resources.ApplyResources(Me.CreatedByDataGridViewTextBoxColumn, "CreatedByDataGridViewTextBoxColumn")
        Me.CreatedByDataGridViewTextBoxColumn.Name = "CreatedByDataGridViewTextBoxColumn"
        '
        'ModifiedDataGridViewTextBoxColumn
        '
        Me.ModifiedDataGridViewTextBoxColumn.DataPropertyName = "Modified"
        resources.ApplyResources(Me.ModifiedDataGridViewTextBoxColumn, "ModifiedDataGridViewTextBoxColumn")
        Me.ModifiedDataGridViewTextBoxColumn.Name = "ModifiedDataGridViewTextBoxColumn"
        '
        'ModifiedByDataGridViewTextBoxColumn
        '
        Me.ModifiedByDataGridViewTextBoxColumn.DataPropertyName = "ModifiedBy"
        resources.ApplyResources(Me.ModifiedByDataGridViewTextBoxColumn, "ModifiedByDataGridViewTextBoxColumn")
        Me.ModifiedByDataGridViewTextBoxColumn.Name = "ModifiedByDataGridViewTextBoxColumn"
        '
        'ApplyButton
        '
        resources.ApplyResources(Me.ApplyButton, "ApplyButton")
        Me.ApplyButton.Name = "ApplyButton"
        Me.ApplyButton.UseVisualStyleBackColor = True
        '
        'ServerProfileForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.ButtonPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.Name = "ServerProfileForm"
        Me.TabControl.ResumeLayout(False)
        Me.SecurityTab.ResumeLayout(False)
        Me.ClientRegistrationPanel.ResumeLayout(False)
        Me.ClientRegistrationPanel.PerformLayout()
        CType(Me.ServerProfileDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AssigneeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClientStatusBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClientRegistrationDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClientBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.RegistrationTab.ResumeLayout(False)
        Me.RegistrationPanel.ResumeLayout(False)
        Me.RegistrationPanel.PerformLayout()
        Me.LicenseDetailsGroup.ResumeLayout(False)
        Me.LicenseDetailsGroup.PerformLayout()
        Me.LicenseEntryGroup.ResumeLayout(False)
        Me.LicenseEntryGroup.PerformLayout()
        Me.MailServerTab.ResumeLayout(False)
        Me.MailServerPanel.ResumeLayout(False)
        Me.MailServerPanel.PerformLayout()
        Me.TestGroup.ResumeLayout(False)
        Me.TestGroup.PerformLayout()
        Me.MailServerHeaderPanel.ResumeLayout(False)
        Me.MailServerHeaderPanel.PerformLayout()
        CType(Me.MailServerPortNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MobileAlertTab.ResumeLayout(False)
        Me.MobileAlertPanel.ResumeLayout(False)
        Me.MobileAlertPanel.PerformLayout()
        Me.SmsTemplateGroup.ResumeLayout(False)
        Me.SmsTemplateGroup.PerformLayout()
        Me.SmsAddressTemplateGroup.ResumeLayout(False)
        Me.SmsAddressTemplateGroup.PerformLayout()
        Me.EnableSMSPanel.ResumeLayout(False)
        Me.EnableSMSPanel.PerformLayout()
        Me.MailTemplateTab.ResumeLayout(False)
        Me.MailTemplateTab.PerformLayout()
        Me.JobMailMessageTemplateGroup.ResumeLayout(False)
        Me.JobMailSubjectGroup.ResumeLayout(False)
        Me.JobMailSubjectGroup.PerformLayout()
        Me.ButtonPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents RegistrationTab As System.Windows.Forms.TabPage
    Friend WithEvents SecurityTab As System.Windows.Forms.TabPage
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend WithEvents RegistrationPanel As System.Windows.Forms.Panel
    Friend WithEvents LicenseDetailsGroup As System.Windows.Forms.GroupBox
    Friend WithEvents RegistrationDetailsBox As System.Windows.Forms.TextBox
    Friend WithEvents LicenseKeyTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LicenseeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ClientNameLabel As System.Windows.Forms.Label
    Friend WithEvents lblLicense As System.Windows.Forms.Label
    Friend WithEvents ClientRegistrationPanel As System.Windows.Forms.Panel
    Friend WithEvents RegistrationExplanation As System.Windows.Forms.TextBox
    Friend WithEvents MobileAlertTab As System.Windows.Forms.TabPage
    Friend WithEvents SmsAddressTemplateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SmsAddressTemplateLabel As System.Windows.Forms.Label
    Friend WithEvents MobileAlertInstructions As System.Windows.Forms.TextBox
    Friend WithEvents MobileAlertPanel As System.Windows.Forms.Panel
    Friend WithEvents SmsMessageExplanation As System.Windows.Forms.TextBox
    Friend WithEvents SmsMessageTemplateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ServerProfileDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonPanel As System.Windows.Forms.Panel
    Friend WithEvents DeviceRegistrationExplanation As System.Windows.Forms.TextBox
    Friend WithEvents SmsEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents ClientBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cancelDialogButton As System.Windows.Forms.Button
    Friend WithEvents checkLicenseKeyButton As System.Windows.Forms.Button
    Friend WithEvents MailServerTab As System.Windows.Forms.TabPage
    Friend WithEvents MailServerPanel As System.Windows.Forms.Panel
    Friend WithEvents MailServerTestButton As System.Windows.Forms.Button
    Friend WithEvents MailServerTestResults As System.Windows.Forms.TextBox
    Friend WithEvents MailServerAddressBox As System.Windows.Forms.TextBox
    Friend WithEvents MailServerLabel As System.Windows.Forms.Label
    Friend WithEvents MailServerPortLabel As System.Windows.Forms.Label
    Friend WithEvents MailServerExplanation As System.Windows.Forms.TextBox
    Friend WithEvents MailServerPortNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents MailTestToAddressBox As System.Windows.Forms.TextBox
    Friend WithEvents MailTestToLabel As System.Windows.Forms.Label
    Friend WithEvents MailTestFromLabel As System.Windows.Forms.Label
    Friend WithEvents MailTestFromAddressBox As System.Windows.Forms.TextBox
    Friend WithEvents MailTemplateTab As System.Windows.Forms.TabPage
    Friend WithEvents MailTemplateExplanation As System.Windows.Forms.TextBox
    Friend WithEvents MailTemplateBox As System.Windows.Forms.RichTextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents TestGroup As System.Windows.Forms.GroupBox
    Friend WithEvents SmsAddressTemplateGroup As System.Windows.Forms.GroupBox
    Friend WithEvents EnableSMSPanel As System.Windows.Forms.Panel
    Friend WithEvents SmsTemplateGroup As System.Windows.Forms.GroupBox
    Friend WithEvents LicenseEntryGroup As System.Windows.Forms.GroupBox
    Friend WithEvents JobMailMessageTemplateGroup As System.Windows.Forms.GroupBox
    Friend WithEvents JobMailSubjectGroup As System.Windows.Forms.GroupBox
    Friend WithEvents SubjectTemplateBox As System.Windows.Forms.TextBox
    Friend WithEvents MailServerHeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents ClientRegistrationDataSet1 As activiser.Library.activiserWebService.ClientRegistrationDataSet
    Friend WithEvents ClientStatusBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AssigneeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ClientIdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SystemIdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AssignedToColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ClientStatusColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValidAfterDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValidBeforeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreatedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreatedByDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModifiedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModifiedByDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ApplyButton As System.Windows.Forms.Button
End Class
