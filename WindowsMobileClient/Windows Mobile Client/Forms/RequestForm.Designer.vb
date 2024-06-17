<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class RequestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RequestForm))
        Me.JobListDetailsColumn = New System.Windows.Forms.ColumnHeader
        Me.JobContextMenu = New System.Windows.Forms.ContextMenu
        Me.JobContextMenuOpen = New System.Windows.Forms.MenuItem
        Me.JobContextMenuNew = New System.Windows.Forms.MenuItem
        Me.RemoveJobContextMenuItem = New System.Windows.Forms.MenuItem
        Me.JobListFinishTimeColumn = New System.Windows.Forms.ColumnHeader
        Me.JobListStartTimeColumn = New System.Windows.Forms.ColumnHeader
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.JobInformation = New System.Windows.Forms.TextBox
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.LongDescriptionTab = New System.Windows.Forms.TabPage
        Me.LongDescription = New System.Windows.Forms.TextBox
        Me.EditContextMenu1 = New activiser.EditContextMenu
        Me.JobListDateColumn = New System.Windows.Forms.ColumnHeader
        Me.JobTab = New System.Windows.Forms.TabPage
        Me.JobList = New System.Windows.Forms.ListView
        Me.JobButtonPanel = New System.Windows.Forms.Panel
        Me.NewJobButton = New System.Windows.Forms.Button
        Me.OpenJobButton = New System.Windows.Forms.Button
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.JobHistoryInformation = New System.Windows.Forms.TextBox
        Me.JobHistoryContextMenuOpen = New System.Windows.Forms.MenuItem
        Me.JobHistoryContextMenuRemove = New System.Windows.Forms.MenuItem
        Me.HistoryButtonPanel = New System.Windows.Forms.Panel
        Me.GetJobHistoryButton = New System.Windows.Forms.Button
        Me.OpenJobHistoryButton = New System.Windows.Forms.Button
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.HistoryTab = New System.Windows.Forms.TabPage
        Me.JobHistoryList = New System.Windows.Forms.ListView
        Me.JobHistoryListDateColumn = New System.Windows.Forms.ColumnHeader
        Me.JobHistoryListConsultantColumn = New System.Windows.Forms.ColumnHeader
        Me.JobHistoryListDetailsColumn = New System.Windows.Forms.ColumnHeader
        Me.JobHistoryContextMenu = New System.Windows.Forms.ContextMenu
        Me.DateLabel = New System.Windows.Forms.Label
        Me.MainMenuView = New System.Windows.Forms.MenuItem
        Me.ShowToolBars = New System.Windows.Forms.MenuItem
        Me.WordWrapCheckBox = New System.Windows.Forms.MenuItem
        Me.ViewRotate0 = New System.Windows.Forms.MenuItem
        Me.ViewRotate90 = New System.Windows.Forms.MenuItem
        Me.ViewRotate180 = New System.Windows.Forms.MenuItem
        Me.ViewRotate270 = New System.Windows.Forms.MenuItem
        Me.ViewFullScreen = New System.Windows.Forms.MenuItem
        Me.MainMenu = New System.Windows.Forms.MenuItem
        Me.MainMenuNewJob = New System.Windows.Forms.MenuItem
        Me.MainMenuOpenJob = New System.Windows.Forms.MenuItem
        Me.MainMenuSync = New System.Windows.Forms.MenuItem
        Me.MainMenuClientInfo = New System.Windows.Forms.MenuItem
        Me.MainMenuRequest = New System.Windows.Forms.MenuItem
        Me.MainMenuNewRequest = New System.Windows.Forms.MenuItem
        Me.MainMenuChangeStatus = New System.Windows.Forms.MenuItem
        Me.MainMenuGetHistory = New System.Windows.Forms.MenuItem
        Me.MainMenuClearSyncedJobs = New System.Windows.Forms.MenuItem
        Me.MainMenuClearHistory = New System.Windows.Forms.MenuItem
        Me.MainMenuSeparator2 = New System.Windows.Forms.MenuItem
        Me.MainMenuSeparator1 = New System.Windows.Forms.MenuItem
        Me.MainMenuCancel = New System.Windows.Forms.MenuItem
        Me.MainMenuSave = New System.Windows.Forms.MenuItem
        Me.MainMenuClose = New System.Windows.Forms.MenuItem
        Me.Contact = New System.Windows.Forms.TextBox
        Me.NextButton = New System.Windows.Forms.MenuItem
        Me.ClientLabel = New System.Windows.Forms.Label
        Me.RequestNumber = New System.Windows.Forms.TextBox
        Me.ShortDescription = New System.Windows.Forms.TextBox
        Me.RequestNumberLabel = New System.Windows.Forms.Label
        Me.ClientSiteName = New System.Windows.Forms.TextBox
        Me.DescriptionTab = New System.Windows.Forms.TabPage
        Me.ShortDescriptionPanel = New System.Windows.Forms.Panel
        Me.ShortDescriptionLabel = New System.Windows.Forms.Label
        Me.RequestHeaderPanel = New System.Windows.Forms.Panel
        Me.OwnerTextBox = New System.Windows.Forms.TextBox
        Me.OwnerLabel = New System.Windows.Forms.Label
        Me.ClientInfoButton = New activiser.Library.Forms.ImageButton
        Me.ClientListComboBox = New System.Windows.Forms.ComboBox
        Me.RequestDate = New System.Windows.Forms.DateTimePicker
        Me.ContactLabel = New System.Windows.Forms.Label
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.ClientTab = New System.Windows.Forms.TabPage
        Me.ClientInfoBox = New System.Windows.Forms.TextBox
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.LongDescriptionTab.SuspendLayout()
        Me.JobTab.SuspendLayout()
        Me.JobButtonPanel.SuspendLayout()
        Me.HistoryButtonPanel.SuspendLayout()
        Me.HistoryTab.SuspendLayout()
        Me.DescriptionTab.SuspendLayout()
        Me.ShortDescriptionPanel.SuspendLayout()
        Me.RequestHeaderPanel.SuspendLayout()
        CType(Me.ClientInfoButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl.SuspendLayout()
        Me.ClientTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'JobListDetailsColumn
        '
        resources.ApplyResources(Me.JobListDetailsColumn, "JobListDetailsColumn")
        '
        'JobContextMenu
        '
        Me.JobContextMenu.MenuItems.Add(Me.JobContextMenuOpen)
        Me.JobContextMenu.MenuItems.Add(Me.JobContextMenuNew)
        Me.JobContextMenu.MenuItems.Add(Me.RemoveJobContextMenuItem)
        '
        'JobContextMenuOpen
        '
        resources.ApplyResources(Me.JobContextMenuOpen, "JobContextMenuOpen")
        '
        'JobContextMenuNew
        '
        resources.ApplyResources(Me.JobContextMenuNew, "JobContextMenuNew")
        '
        'RemoveJobContextMenuItem
        '
        resources.ApplyResources(Me.RemoveJobContextMenuItem, "RemoveJobContextMenuItem")
        '
        'JobListFinishTimeColumn
        '
        resources.ApplyResources(Me.JobListFinishTimeColumn, "JobListFinishTimeColumn")
        '
        'JobListStartTimeColumn
        '
        resources.ApplyResources(Me.JobListStartTimeColumn, "JobListStartTimeColumn")
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Name = "Splitter1"
        '
        'JobInformation
        '
        Me.JobInformation.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.JobInformation, "JobInformation")
        Me.JobInformation.Name = "JobInformation"
        Me.JobInformation.ReadOnly = True
        '
        'ReadOnlyContextMenu1
        '
        'Me.ReadOnlyContextMenu1.ShowCall = False
        '
        'LongDescriptionTab
        '
        Me.LongDescriptionTab.Controls.Add(Me.LongDescription)
        resources.ApplyResources(Me.LongDescriptionTab, "LongDescriptionTab")
        Me.LongDescriptionTab.Name = "LongDescriptionTab"
        '
        'LongDescription
        '
        Me.LongDescription.AcceptsReturn = True
        Me.LongDescription.AcceptsTab = True
        Me.LongDescription.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.LongDescription, "LongDescription")
        Me.LongDescription.Name = "LongDescription"
        '
        'EditContextMenu1
        '
        'Me.EditContextMenu1.ShowCall = False
        '
        'JobListDateColumn
        '
        resources.ApplyResources(Me.JobListDateColumn, "JobListDateColumn")
        '
        'JobTab
        '
        Me.JobTab.Controls.Add(Me.JobList)
        Me.JobTab.Controls.Add(Me.Splitter1)
        Me.JobTab.Controls.Add(Me.JobInformation)
        Me.JobTab.Controls.Add(Me.JobButtonPanel)
        resources.ApplyResources(Me.JobTab, "JobTab")
        Me.JobTab.Name = "JobTab"
        '
        'JobList
        '
        Me.JobList.Columns.Add(Me.JobListDateColumn)
        Me.JobList.Columns.Add(Me.JobListStartTimeColumn)
        Me.JobList.Columns.Add(Me.JobListFinishTimeColumn)
        Me.JobList.Columns.Add(Me.JobListDetailsColumn)
        Me.JobList.ContextMenu = Me.JobContextMenu
        resources.ApplyResources(Me.JobList, "JobList")
        Me.JobList.FullRowSelect = True
        Me.JobList.Name = "JobList"
        Me.JobList.View = System.Windows.Forms.View.Details
        '
        'JobButtonPanel
        '
        Me.JobButtonPanel.Controls.Add(Me.NewJobButton)
        Me.JobButtonPanel.Controls.Add(Me.OpenJobButton)
        resources.ApplyResources(Me.JobButtonPanel, "JobButtonPanel")
        Me.JobButtonPanel.Name = "JobButtonPanel"
        '
        'NewJobButton
        '
        resources.ApplyResources(Me.NewJobButton, "NewJobButton")
        Me.NewJobButton.Name = "NewJobButton"
        '
        'OpenJobButton
        '
        resources.ApplyResources(Me.OpenJobButton, "OpenJobButton")
        Me.OpenJobButton.Name = "OpenJobButton"
        '
        'Splitter2
        '
        resources.ApplyResources(Me.Splitter2, "Splitter2")
        Me.Splitter2.Name = "Splitter2"
        '
        'JobHistoryInformation
        '
        Me.JobHistoryInformation.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.JobHistoryInformation, "JobHistoryInformation")
        Me.JobHistoryInformation.Name = "JobHistoryInformation"
        Me.JobHistoryInformation.ReadOnly = True
        '
        'JobHistoryContextMenuOpen
        '
        resources.ApplyResources(Me.JobHistoryContextMenuOpen, "JobHistoryContextMenuOpen")
        '
        'JobHistoryContextMenuRemove
        '
        resources.ApplyResources(Me.JobHistoryContextMenuRemove, "JobHistoryContextMenuRemove")
        '
        'HistoryButtonPanel
        '
        Me.HistoryButtonPanel.Controls.Add(Me.GetJobHistoryButton)
        Me.HistoryButtonPanel.Controls.Add(Me.OpenJobHistoryButton)
        resources.ApplyResources(Me.HistoryButtonPanel, "HistoryButtonPanel")
        Me.HistoryButtonPanel.Name = "HistoryButtonPanel"
        '
        'GetJobHistoryButton
        '
        resources.ApplyResources(Me.GetJobHistoryButton, "GetJobHistoryButton")
        Me.GetJobHistoryButton.Name = "GetJobHistoryButton"
        '
        'OpenJobHistoryButton
        '
        resources.ApplyResources(Me.OpenJobHistoryButton, "OpenJobHistoryButton")
        Me.OpenJobHistoryButton.Name = "OpenJobHistoryButton"
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'HistoryTab
        '
        Me.HistoryTab.Controls.Add(Me.JobHistoryList)
        Me.HistoryTab.Controls.Add(Me.Splitter2)
        Me.HistoryTab.Controls.Add(Me.JobHistoryInformation)
        Me.HistoryTab.Controls.Add(Me.HistoryButtonPanel)
        resources.ApplyResources(Me.HistoryTab, "HistoryTab")
        Me.HistoryTab.Name = "HistoryTab"
        '
        'JobHistoryList
        '
        Me.JobHistoryList.Columns.Add(Me.JobHistoryListDateColumn)
        Me.JobHistoryList.Columns.Add(Me.JobHistoryListConsultantColumn)
        Me.JobHistoryList.Columns.Add(Me.JobHistoryListDetailsColumn)
        Me.JobHistoryList.ContextMenu = Me.JobHistoryContextMenu
        resources.ApplyResources(Me.JobHistoryList, "JobHistoryList")
        Me.JobHistoryList.FullRowSelect = True
        Me.JobHistoryList.Name = "JobHistoryList"
        Me.JobHistoryList.View = System.Windows.Forms.View.Details
        '
        'JobHistoryListDateColumn
        '
        resources.ApplyResources(Me.JobHistoryListDateColumn, "JobHistoryListDateColumn")
        '
        'JobHistoryListConsultantColumn
        '
        resources.ApplyResources(Me.JobHistoryListConsultantColumn, "JobHistoryListConsultantColumn")
        '
        'JobHistoryListDetailsColumn
        '
        resources.ApplyResources(Me.JobHistoryListDetailsColumn, "JobHistoryListDetailsColumn")
        '
        'JobHistoryContextMenu
        '
        Me.JobHistoryContextMenu.MenuItems.Add(Me.JobHistoryContextMenuOpen)
        Me.JobHistoryContextMenu.MenuItems.Add(Me.JobHistoryContextMenuRemove)
        '
        'DateLabel
        '
        resources.ApplyResources(Me.DateLabel, "DateLabel")
        Me.DateLabel.Name = "DateLabel"
        '
        'MainMenuView
        '
        Me.MainMenuView.MenuItems.Add(Me.ShowToolBars)
        Me.MainMenuView.MenuItems.Add(Me.WordWrapCheckBox)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate0)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate90)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate180)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate270)
        resources.ApplyResources(Me.MainMenuView, "MainMenuView")
        '
        'ShowToolBars
        '
        Me.ShowToolBars.Checked = True
        resources.ApplyResources(Me.ShowToolBars, "ShowToolBars")
        '
        'WordWrapCheckBox
        '
        Me.WordWrapCheckBox.Checked = True
        resources.ApplyResources(Me.WordWrapCheckBox, "WordWrapCheckBox")
        '
        'ViewRotate0
        '
        resources.ApplyResources(Me.ViewRotate0, "ViewRotate0")
        '
        'ViewRotate90
        '
        resources.ApplyResources(Me.ViewRotate90, "ViewRotate90")
        '
        'ViewRotate180
        '
        resources.ApplyResources(Me.ViewRotate180, "ViewRotate180")
        '
        'ViewRotate270
        '
        resources.ApplyResources(Me.ViewRotate270, "ViewRotate270")
        '
        'ViewFullScreen
        '
        resources.ApplyResources(Me.ViewFullScreen, "ViewFullScreen")
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.Add(Me.MainMenuNewJob)
        Me.MainMenu.MenuItems.Add(Me.MainMenuOpenJob)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSync)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClientInfo)
        Me.MainMenu.MenuItems.Add(Me.MainMenuRequest)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSeparator2)
        Me.MainMenu.MenuItems.Add(Me.MainMenuView)
        Me.MainMenu.MenuItems.Add(Me.ViewFullScreen)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSeparator1)
        Me.MainMenu.MenuItems.Add(Me.MainMenuCancel)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSave)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClose)
        resources.ApplyResources(Me.MainMenu, "MainMenu")
        '
        'MainMenuNewJob
        '
        resources.ApplyResources(Me.MainMenuNewJob, "MainMenuNewJob")
        '
        'MainMenuOpenJob
        '
        resources.ApplyResources(Me.MainMenuOpenJob, "MainMenuOpenJob")
        '
        'MainMenuSync
        '
        resources.ApplyResources(Me.MainMenuSync, "MainMenuSync")
        '
        'MainMenuClientInfo
        '
        resources.ApplyResources(Me.MainMenuClientInfo, "MainMenuClientInfo")
        '
        'MainMenuRequest
        '
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuNewRequest)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuChangeStatus)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuGetHistory)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuClearSyncedJobs)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuClearHistory)
        resources.ApplyResources(Me.MainMenuRequest, "MainMenuRequest")
        '
        'MainMenuNewRequest
        '
        resources.ApplyResources(Me.MainMenuNewRequest, "MainMenuNewRequest")
        '
        'MainMenuChangeStatus
        '
        resources.ApplyResources(Me.MainMenuChangeStatus, "MainMenuChangeStatus")
        '
        'MainMenuGetHistory
        '
        resources.ApplyResources(Me.MainMenuGetHistory, "MainMenuGetHistory")
        '
        'MainMenuClearSyncedJobs
        '
        resources.ApplyResources(Me.MainMenuClearSyncedJobs, "MainMenuClearSyncedJobs")
        '
        'MainMenuClearHistory
        '
        resources.ApplyResources(Me.MainMenuClearHistory, "MainMenuClearHistory")
        '
        'MainMenuSeparator2
        '
        resources.ApplyResources(Me.MainMenuSeparator2, "MainMenuSeparator2")
        '
        'MainMenuSeparator1
        '
        resources.ApplyResources(Me.MainMenuSeparator1, "MainMenuSeparator1")
        '
        'MainMenuCancel
        '
        resources.ApplyResources(Me.MainMenuCancel, "MainMenuCancel")
        '
        'MainMenuSave
        '
        resources.ApplyResources(Me.MainMenuSave, "MainMenuSave")
        '
        'MainMenuClose
        '
        resources.ApplyResources(Me.MainMenuClose, "MainMenuClose")
        '
        'Contact
        '
        resources.ApplyResources(Me.Contact, "Contact")
        Me.Contact.ContextMenu = Me.ReadOnlyContextMenu1
        Me.Contact.Name = "Contact"
        Me.Contact.ReadOnly = True
        '
        'NextButton
        '
        resources.ApplyResources(Me.NextButton, "NextButton")
        '
        'ClientLabel
        '
        resources.ApplyResources(Me.ClientLabel, "ClientLabel")
        Me.ClientLabel.Name = "ClientLabel"
        '
        'RequestNumber
        '
        resources.ApplyResources(Me.RequestNumber, "RequestNumber")
        Me.RequestNumber.ContextMenu = Me.ReadOnlyContextMenu1
        Me.RequestNumber.Name = "RequestNumber"
        Me.RequestNumber.ReadOnly = True
        '
        'ShortDescription
        '
        Me.ShortDescription.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.ShortDescription, "ShortDescription")
        Me.ShortDescription.Name = "ShortDescription"
        Me.ShortDescription.ReadOnly = True
        '
        'RequestNumberLabel
        '
        resources.ApplyResources(Me.RequestNumberLabel, "RequestNumberLabel")
        Me.RequestNumberLabel.Name = "RequestNumberLabel"
        '
        'ClientSiteName
        '
        Me.ClientSiteName.ForeColor = System.Drawing.SystemColors.Highlight
        resources.ApplyResources(Me.ClientSiteName, "ClientSiteName")
        Me.ClientSiteName.Name = "ClientSiteName"
        Me.ClientSiteName.ReadOnly = True
        '
        'DescriptionTab
        '
        Me.DescriptionTab.Controls.Add(Me.ShortDescriptionPanel)
        Me.DescriptionTab.Controls.Add(Me.RequestHeaderPanel)
        resources.ApplyResources(Me.DescriptionTab, "DescriptionTab")
        Me.DescriptionTab.Name = "DescriptionTab"
        '
        'ShortDescriptionPanel
        '
        Me.ShortDescriptionPanel.Controls.Add(Me.ShortDescription)
        Me.ShortDescriptionPanel.Controls.Add(Me.ShortDescriptionLabel)
        resources.ApplyResources(Me.ShortDescriptionPanel, "ShortDescriptionPanel")
        Me.ShortDescriptionPanel.Name = "ShortDescriptionPanel"
        '
        'ShortDescriptionLabel
        '
        resources.ApplyResources(Me.ShortDescriptionLabel, "ShortDescriptionLabel")
        Me.ShortDescriptionLabel.Name = "ShortDescriptionLabel"
        '
        'RequestHeaderPanel
        '
        Me.RequestHeaderPanel.Controls.Add(Me.OwnerTextBox)
        Me.RequestHeaderPanel.Controls.Add(Me.OwnerLabel)
        Me.RequestHeaderPanel.Controls.Add(Me.ClientInfoButton)
        Me.RequestHeaderPanel.Controls.Add(Me.ClientListComboBox)
        Me.RequestHeaderPanel.Controls.Add(Me.ClientSiteName)
        Me.RequestHeaderPanel.Controls.Add(Me.ClientLabel)
        Me.RequestHeaderPanel.Controls.Add(Me.RequestNumber)
        Me.RequestHeaderPanel.Controls.Add(Me.RequestNumberLabel)
        Me.RequestHeaderPanel.Controls.Add(Me.Contact)
        Me.RequestHeaderPanel.Controls.Add(Me.RequestDate)
        Me.RequestHeaderPanel.Controls.Add(Me.ContactLabel)
        Me.RequestHeaderPanel.Controls.Add(Me.DateLabel)
        resources.ApplyResources(Me.RequestHeaderPanel, "RequestHeaderPanel")
        Me.RequestHeaderPanel.Name = "RequestHeaderPanel"
        '
        'OwnerTextBox
        '
        resources.ApplyResources(Me.OwnerTextBox, "OwnerTextBox")
        Me.OwnerTextBox.ContextMenu = Me.ReadOnlyContextMenu1
        Me.OwnerTextBox.Name = "OwnerTextBox"
        Me.OwnerTextBox.ReadOnly = True
        '
        'OwnerLabel
        '
        resources.ApplyResources(Me.OwnerLabel, "OwnerLabel")
        Me.OwnerLabel.Name = "OwnerLabel"
        '
        'ClientInfoButton
        '
        Me.ClientInfoButton.BackColor = System.Drawing.Color.Transparent
        Me.ClientInfoButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ClientInfoButton.ClickMaskColor = System.Drawing.SystemColors.Window
        Me.ClientInfoButton.Image = CType(resources.GetObject("ClientInfoButton.Image"), System.Drawing.Image)
        Me.ClientInfoButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        resources.ApplyResources(Me.ClientInfoButton, "ClientInfoButton")
        Me.ClientInfoButton.Name = "ClientInfoButton"
        Me.ClientInfoButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        '
        'ClientListComboBox
        '
        resources.ApplyResources(Me.ClientListComboBox, "ClientListComboBox")
        Me.ClientListComboBox.ContextMenu = Me.ReadOnlyContextMenu1
        Me.ClientListComboBox.DisplayMember = "SiteName"
        Me.ClientListComboBox.Name = "ClientListComboBox"
        Me.ClientListComboBox.ValueMember = "ClientSiteUID"
        '
        'RequestDate
        '
        resources.ApplyResources(Me.RequestDate, "RequestDate")
        Me.RequestDate.ContextMenu = Me.ReadOnlyContextMenu1
        Me.RequestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.RequestDate.MaxDate = New Date(9990, 12, 31, 0, 0, 0, 0)
        Me.RequestDate.Name = "RequestDate"
        Me.RequestDate.Value = New Date(2004, 12, 16, 1, 51, 28, 995)
        '
        'ContactLabel
        '
        resources.ApplyResources(Me.ContactLabel, "ContactLabel")
        Me.ContactLabel.Name = "ContactLabel"
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.DescriptionTab)
        Me.TabControl.Controls.Add(Me.LongDescriptionTab)
        Me.TabControl.Controls.Add(Me.JobTab)
        Me.TabControl.Controls.Add(Me.HistoryTab)
        Me.TabControl.Controls.Add(Me.ClientTab)
        resources.ApplyResources(Me.TabControl, "TabControl")
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        '
        'ClientTab
        '
        Me.ClientTab.Controls.Add(Me.ClientInfoBox)
        resources.ApplyResources(Me.ClientTab, "ClientTab")
        Me.ClientTab.Name = "ClientTab"
        '
        'ClientInfoBox
        '
        resources.ApplyResources(Me.ClientInfoBox, "ClientInfoBox")
        Me.ClientInfoBox.Name = "ClientInfoBox"
        Me.ClientInfoBox.ReadOnly = True
        '
        'InputPanel
        '
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.NextButton)
        Me.MenuStrip.MenuItems.Add(Me.MainMenu)
        '
        'RequestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.MinimizeBox = False
        Me.Name = "RequestForm"
        Me.LongDescriptionTab.ResumeLayout(False)
        Me.JobTab.ResumeLayout(False)
        Me.JobButtonPanel.ResumeLayout(False)
        Me.HistoryButtonPanel.ResumeLayout(False)
        Me.HistoryTab.ResumeLayout(False)
        Me.DescriptionTab.ResumeLayout(False)
        Me.ShortDescriptionPanel.ResumeLayout(False)
        Me.RequestHeaderPanel.ResumeLayout(False)
        CType(Me.ClientInfoButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl.ResumeLayout(False)
        Me.ClientTab.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents JobListDetailsColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents JobContextMenuOpen As System.Windows.Forms.MenuItem
    Friend WithEvents JobContextMenuNew As System.Windows.Forms.MenuItem
    Friend WithEvents JobListFinishTimeColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobListStartTimeColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents JobInformation As System.Windows.Forms.TextBox
    Friend WithEvents LongDescriptionTab As System.Windows.Forms.TabPage
    Friend WithEvents JobListDateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobTab As System.Windows.Forms.TabPage
    Friend WithEvents JobList As System.Windows.Forms.ListView
    Friend WithEvents JobButtonPanel As System.Windows.Forms.Panel
    Friend WithEvents NewJobButton As System.Windows.Forms.Button
    Friend WithEvents OpenJobButton As System.Windows.Forms.Button
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents JobHistoryInformation As System.Windows.Forms.TextBox
    Friend WithEvents JobHistoryContextMenuOpen As System.Windows.Forms.MenuItem
    Friend WithEvents JobHistoryContextMenuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents HistoryButtonPanel As System.Windows.Forms.Panel
    Friend WithEvents GetJobHistoryButton As System.Windows.Forms.Button
    Friend WithEvents OpenJobHistoryButton As System.Windows.Forms.Button
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents HistoryTab As System.Windows.Forms.TabPage
    Friend WithEvents JobHistoryList As System.Windows.Forms.ListView
    Friend WithEvents JobHistoryListDateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobHistoryListConsultantColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobHistoryListDetailsColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobHistoryContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents DateLabel As System.Windows.Forms.Label
    Friend WithEvents ViewRotate0 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate90 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate180 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate270 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuNewJob As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuOpenJob As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuGetHistory As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuNewRequest As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuChangeStatus As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSync As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSave As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClose As System.Windows.Forms.MenuItem
    Friend WithEvents Contact As System.Windows.Forms.TextBox
    Friend WithEvents NextButton As System.Windows.Forms.MenuItem
    Friend WithEvents ClientLabel As System.Windows.Forms.Label
    Friend WithEvents RequestNumber As System.Windows.Forms.TextBox
    Friend WithEvents ShortDescription As System.Windows.Forms.TextBox
    Friend WithEvents RequestNumberLabel As System.Windows.Forms.Label
    Friend WithEvents ClientSiteName As System.Windows.Forms.TextBox
    Friend WithEvents DescriptionTab As System.Windows.Forms.TabPage
    Friend WithEvents RequestHeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents ShortDescriptionPanel As System.Windows.Forms.Panel
    Friend WithEvents ShortDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents RequestDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ContactLabel As System.Windows.Forms.Label
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents ViewFullScreen As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSeparator2 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSeparator1 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuRequest As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuCancel As System.Windows.Forms.MenuItem
    Friend WithEvents RemoveJobContextMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClearSyncedJobs As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClearHistory As System.Windows.Forms.MenuItem
    Friend WithEvents ShowToolBars As System.Windows.Forms.MenuItem
    Friend WithEvents ClientListComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents LongDescription As System.Windows.Forms.TextBox
    Friend WithEvents EditContextMenu1 As activiser.EditContextMenu
    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
    Friend WithEvents MainMenuView As System.Windows.Forms.MenuItem
    Friend WithEvents WordWrapCheckBox As System.Windows.Forms.MenuItem
    Friend WithEvents ClientTab As System.Windows.Forms.TabPage
    Friend WithEvents ClientInfoBox As System.Windows.Forms.TextBox
    Friend WithEvents MainMenuClientInfo As System.Windows.Forms.MenuItem
    Friend WithEvents ClientInfoButton As activiser.Library.Forms.ImageButton
    Friend WithEvents OwnerTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OwnerLabel As System.Windows.Forms.Label
End Class
