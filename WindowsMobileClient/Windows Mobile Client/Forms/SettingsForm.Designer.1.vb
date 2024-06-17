<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class SettingsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsForm))
        Me.ServerTab = New System.Windows.Forms.TabPage
        Me.ServerPanel = New System.Windows.Forms.Panel
        Me.WebServiceCredentialsButton = New System.Windows.Forms.Button
        Me.ActiviserPasswordButton = New System.Windows.Forms.Button
        Me.ChangeServerSettingsButton = New System.Windows.Forms.Button
        Me.EditContextMenu1 = New activiser.EditContextMenu
        Me.PasswordContextMenu1 = New activiser.PasswordContextMenu
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.DefaultsTab = New System.Windows.Forms.TabPage
        Me.JobHistoryPanel = New System.Windows.Forms.Panel
        Me.JobHistoryNumberLabel = New System.Windows.Forms.Label
        Me.JobHistoryLabel = New System.Windows.Forms.Label
        Me.HistoryNumberOptions = New System.Windows.Forms.DomainUpDown
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.HistoryAgeOptions = New System.Windows.Forms.DomainUpDown
        Me.JobHistoryAgeLabel = New System.Windows.Forms.Label
        Me.JobTimeDefaultsPanel = New System.Windows.Forms.Panel
        Me.JobTimesHeader = New System.Windows.Forms.Label
        Me.DefaultJobDurationLabel = New System.Windows.Forms.Label
        Me.DefaultTravelTimeOptions = New System.Windows.Forms.DomainUpDown
        Me.DefaultJobDurationOptions = New System.Windows.Forms.DomainUpDown
        Me.TravelTimeLabel = New System.Windows.Forms.Label
        Me.JobTimeIntervalLabel = New System.Windows.Forms.Label
        Me.DefaultJobStartOffsetOptions = New System.Windows.Forms.DomainUpDown
        Me.JobTimeIntervalOptions = New System.Windows.Forms.DomainUpDown
        Me.JobStartOffsetLabel = New System.Windows.Forms.Label
        Me.NextButton = New System.Windows.Forms.MenuItem
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.ShortCutTab = New System.Windows.Forms.TabPage
        Me.ShortCutList = New System.Windows.Forms.TextBox
        Me.SyncPage = New System.Windows.Forms.TabPage
        Me.DayPickerPanel = New System.Windows.Forms.Panel
        Me.DaysOfWeekLabel = New System.Windows.Forms.Label
        Me.ThursdayLabel = New System.Windows.Forms.Label
        Me.SaturdayLabel = New System.Windows.Forms.Label
        Me.SundayCheckbox = New System.Windows.Forms.CheckBox
        Me.FridayLabel = New System.Windows.Forms.Label
        Me.TuesdayCheckbox = New System.Windows.Forms.CheckBox
        Me.MondayCheckbox = New System.Windows.Forms.CheckBox
        Me.WednesdayCheckbox = New System.Windows.Forms.CheckBox
        Me.ThursdayCheckbox = New System.Windows.Forms.CheckBox
        Me.WednesdayLabel = New System.Windows.Forms.Label
        Me.SaturdayCheckbox = New System.Windows.Forms.CheckBox
        Me.TuesdayLabel = New System.Windows.Forms.Label
        Me.FridayCheckbox = New System.Windows.Forms.CheckBox
        Me.MondayLabel = New System.Windows.Forms.Label
        Me.SundayLabel = New System.Windows.Forms.Label
        Me.AutoSyncPanel = New System.Windows.Forms.Panel
        Me.MinuteLabel = New System.Windows.Forms.Label
        Me.SyncFinishLabel = New System.Windows.Forms.Label
        Me.SyncStartLabel = New System.Windows.Forms.Label
        Me.SyncFinishTimeSelector = New System.Windows.Forms.DomainUpDown
        Me.SyncStartTimeSelector = New System.Windows.Forms.DomainUpDown
        Me.SyncIntervalNumberBox = New System.Windows.Forms.NumericUpDown
        Me.SyncIntervalLabel = New System.Windows.Forms.Label
        Me.AutoSyncCheckBox = New System.Windows.Forms.CheckBox
        Me.DisplayTab = New System.Windows.Forms.TabPage
        Me.ExampleText = New System.Windows.Forms.Label
        Me.TextSizeExampleLabel = New System.Windows.Forms.Label
        Me.TextSizePanel = New System.Windows.Forms.Panel
        Me.TextSizeLarge = New System.Windows.Forms.Label
        Me.TextSizeSmall = New System.Windows.Forms.Label
        Me.TextSizePicker = New System.Windows.Forms.TrackBar
        Me.TextSizeLabel = New System.Windows.Forms.Label
        Me.MainMenuClose = New System.Windows.Forms.MenuItem
        Me.MainMenu = New System.Windows.Forms.MenuItem
        Me.MainMenuRemoveCompletedRequests = New System.Windows.Forms.MenuItem
        Me.MainMenuClearClientInfo = New System.Windows.Forms.MenuItem
        Me.MainMenuFetchUpdatedSchema = New System.Windows.Forms.MenuItem
        Me.MainMenuChangeWebServiceCredentials = New System.Windows.Forms.MenuItem
        Me.MainMenuChangeActiviserPassword = New System.Windows.Forms.MenuItem
        Me.MainMenuChangeServerSettings = New System.Windows.Forms.MenuItem
        Me.MainMenuView = New System.Windows.Forms.MenuItem
        Me.ViewFullScreen = New System.Windows.Forms.MenuItem
        Me.ViewRotate0 = New System.Windows.Forms.MenuItem
        Me.ViewRotate90 = New System.Windows.Forms.MenuItem
        Me.ViewRotate180 = New System.Windows.Forms.MenuItem
        Me.ViewRotate270 = New System.Windows.Forms.MenuItem
        Me.MainMenuCancel = New System.Windows.Forms.MenuItem
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.FormLabel = New System.Windows.Forms.Label
        Me.SettingsHeaderUnderline = New System.Windows.Forms.PictureBox
        Me.ServerTab.SuspendLayout()
        Me.ServerPanel.SuspendLayout()
        Me.DefaultsTab.SuspendLayout()
        Me.JobHistoryPanel.SuspendLayout()
        Me.JobTimeDefaultsPanel.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.ShortCutTab.SuspendLayout()
        Me.SyncPage.SuspendLayout()
        Me.DayPickerPanel.SuspendLayout()
        Me.AutoSyncPanel.SuspendLayout()
        Me.DisplayTab.SuspendLayout()
        Me.TextSizePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ServerTab
        '
        resources.ApplyResources(Me.ServerTab, "ServerTab")
        Me.ServerTab.Controls.Add(Me.ServerPanel)
        Me.ServerTab.Name = "ServerTab"
        '
        'ServerPanel
        '
        resources.ApplyResources(Me.ServerPanel, "ServerPanel")
        Me.ServerPanel.Controls.Add(Me.WebServiceCredentialsButton)
        Me.ServerPanel.Controls.Add(Me.ActiviserPasswordButton)
        Me.ServerPanel.Controls.Add(Me.ChangeServerSettingsButton)
        Me.ServerPanel.Name = "ServerPanel"
        '
        'WebServiceCredentialsButton
        '
        resources.ApplyResources(Me.WebServiceCredentialsButton, "WebServiceCredentialsButton")
        Me.WebServiceCredentialsButton.Name = "WebServiceCredentialsButton"
        '
        'ActiviserPasswordButton
        '
        resources.ApplyResources(Me.ActiviserPasswordButton, "ActiviserPasswordButton")
        Me.ActiviserPasswordButton.Name = "ActiviserPasswordButton"
        '
        'ChangeServerSettingsButton
        '
        resources.ApplyResources(Me.ChangeServerSettingsButton, "ChangeServerSettingsButton")
        Me.ChangeServerSettingsButton.Name = "ChangeServerSettingsButton"
        '
        'EditContextMenu1
        '
        'Me.EditContextMenu1.ShowCall = False
        '
        'InputPanel
        '
        '
        'DefaultsTab
        '
        resources.ApplyResources(Me.DefaultsTab, "DefaultsTab")
        Me.DefaultsTab.Controls.Add(Me.JobHistoryPanel)
        Me.DefaultsTab.Controls.Add(Me.JobTimeDefaultsPanel)
        Me.DefaultsTab.Name = "DefaultsTab"
        '
        'JobHistoryPanel
        '
        Me.JobHistoryPanel.BackColor = System.Drawing.SystemColors.Control
        Me.JobHistoryPanel.Controls.Add(Me.JobHistoryNumberLabel)
        Me.JobHistoryPanel.Controls.Add(Me.JobHistoryLabel)
        Me.JobHistoryPanel.Controls.Add(Me.HistoryNumberOptions)
        Me.JobHistoryPanel.Controls.Add(Me.HistoryAgeOptions)
        Me.JobHistoryPanel.Controls.Add(Me.JobHistoryAgeLabel)
        resources.ApplyResources(Me.JobHistoryPanel, "JobHistoryPanel")
        Me.JobHistoryPanel.Name = "JobHistoryPanel"
        '
        'JobHistoryNumberLabel
        '
        resources.ApplyResources(Me.JobHistoryNumberLabel, "JobHistoryNumberLabel")
        Me.JobHistoryNumberLabel.Name = "JobHistoryNumberLabel"
        '
        'JobHistoryLabel
        '
        Me.JobHistoryLabel.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.JobHistoryLabel, "JobHistoryLabel")
        Me.JobHistoryLabel.Name = "JobHistoryLabel"
        '
        'HistoryNumberOptions
        '
        resources.ApplyResources(Me.HistoryNumberOptions, "HistoryNumberOptions")
        Me.HistoryNumberOptions.ContextMenu = Me.ReadOnlyContextMenu1
        Me.HistoryNumberOptions.Items.Add(resources.GetString("HistoryNumberOptions.Items"))
        Me.HistoryNumberOptions.Items.Add(resources.GetString("HistoryNumberOptions.Items1"))
        Me.HistoryNumberOptions.Items.Add(resources.GetString("HistoryNumberOptions.Items2"))
        Me.HistoryNumberOptions.Items.Add(resources.GetString("HistoryNumberOptions.Items3"))
        Me.HistoryNumberOptions.Items.Add(resources.GetString("HistoryNumberOptions.Items4"))
        Me.HistoryNumberOptions.Items.Add(resources.GetString("HistoryNumberOptions.Items5"))
        Me.HistoryNumberOptions.Name = "HistoryNumberOptions"
        Me.HistoryNumberOptions.ReadOnly = True
        '
        'ReadOnlyContextMenu1
        '
        'Me.ReadOnlyContextMenu1.ShowCall = False
        '
        'HistoryAgeOptions
        '
        resources.ApplyResources(Me.HistoryAgeOptions, "HistoryAgeOptions")
        Me.HistoryAgeOptions.ContextMenu = Me.ReadOnlyContextMenu1
        Me.HistoryAgeOptions.Name = "HistoryAgeOptions"
        Me.HistoryAgeOptions.ReadOnly = True
        '
        'JobHistoryAgeLabel
        '
        resources.ApplyResources(Me.JobHistoryAgeLabel, "JobHistoryAgeLabel")
        Me.JobHistoryAgeLabel.Name = "JobHistoryAgeLabel"
        '
        'JobTimeDefaultsPanel
        '
        Me.JobTimeDefaultsPanel.Controls.Add(Me.JobTimesHeader)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.DefaultJobDurationLabel)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.DefaultTravelTimeOptions)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.DefaultJobDurationOptions)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.TravelTimeLabel)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.JobTimeIntervalLabel)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.DefaultJobStartOffsetOptions)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.JobTimeIntervalOptions)
        Me.JobTimeDefaultsPanel.Controls.Add(Me.JobStartOffsetLabel)
        resources.ApplyResources(Me.JobTimeDefaultsPanel, "JobTimeDefaultsPanel")
        Me.JobTimeDefaultsPanel.Name = "JobTimeDefaultsPanel"
        '
        'JobTimesHeader
        '
        Me.JobTimesHeader.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.JobTimesHeader, "JobTimesHeader")
        Me.JobTimesHeader.Name = "JobTimesHeader"
        '
        'DefaultJobDurationLabel
        '
        resources.ApplyResources(Me.DefaultJobDurationLabel, "DefaultJobDurationLabel")
        Me.DefaultJobDurationLabel.Name = "DefaultJobDurationLabel"
        '
        'DefaultTravelTimeOptions
        '
        resources.ApplyResources(Me.DefaultTravelTimeOptions, "DefaultTravelTimeOptions")
        Me.DefaultTravelTimeOptions.Items.Add(resources.GetString("DefaultTravelTimeOptions.Items"))
        Me.DefaultTravelTimeOptions.Items.Add(resources.GetString("DefaultTravelTimeOptions.Items1"))
        Me.DefaultTravelTimeOptions.Items.Add(resources.GetString("DefaultTravelTimeOptions.Items2"))
        Me.DefaultTravelTimeOptions.Items.Add(resources.GetString("DefaultTravelTimeOptions.Items3"))
        Me.DefaultTravelTimeOptions.Items.Add(resources.GetString("DefaultTravelTimeOptions.Items4"))
        Me.DefaultTravelTimeOptions.Items.Add(resources.GetString("DefaultTravelTimeOptions.Items5"))
        Me.DefaultTravelTimeOptions.Name = "DefaultTravelTimeOptions"
        Me.DefaultTravelTimeOptions.ReadOnly = True
        '
        'DefaultJobDurationOptions
        '
        resources.ApplyResources(Me.DefaultJobDurationOptions, "DefaultJobDurationOptions")
        Me.DefaultJobDurationOptions.Items.Add(resources.GetString("DefaultJobDurationOptions.Items"))
        Me.DefaultJobDurationOptions.Items.Add(resources.GetString("DefaultJobDurationOptions.Items1"))
        Me.DefaultJobDurationOptions.Items.Add(resources.GetString("DefaultJobDurationOptions.Items2"))
        Me.DefaultJobDurationOptions.Items.Add(resources.GetString("DefaultJobDurationOptions.Items3"))
        Me.DefaultJobDurationOptions.Items.Add(resources.GetString("DefaultJobDurationOptions.Items4"))
        Me.DefaultJobDurationOptions.Items.Add(resources.GetString("DefaultJobDurationOptions.Items5"))
        Me.DefaultJobDurationOptions.Name = "DefaultJobDurationOptions"
        Me.DefaultJobDurationOptions.ReadOnly = True
        '
        'TravelTimeLabel
        '
        resources.ApplyResources(Me.TravelTimeLabel, "TravelTimeLabel")
        Me.TravelTimeLabel.Name = "TravelTimeLabel"
        '
        'JobTimeIntervalLabel
        '
        resources.ApplyResources(Me.JobTimeIntervalLabel, "JobTimeIntervalLabel")
        Me.JobTimeIntervalLabel.Name = "JobTimeIntervalLabel"
        '
        'DefaultJobStartOffsetOptions
        '
        resources.ApplyResources(Me.DefaultJobStartOffsetOptions, "DefaultJobStartOffsetOptions")
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items1"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items2"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items3"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items4"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items5"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items6"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items7"))
        Me.DefaultJobStartOffsetOptions.Items.Add(resources.GetString("DefaultJobStartOffsetOptions.Items8"))
        Me.DefaultJobStartOffsetOptions.Name = "DefaultJobStartOffsetOptions"
        Me.DefaultJobStartOffsetOptions.ReadOnly = True
        '
        'JobTimeIntervalOptions
        '
        resources.ApplyResources(Me.JobTimeIntervalOptions, "JobTimeIntervalOptions")
        Me.JobTimeIntervalOptions.Items.Add(resources.GetString("JobTimeIntervalOptions.Items"))
        Me.JobTimeIntervalOptions.Items.Add(resources.GetString("JobTimeIntervalOptions.Items1"))
        Me.JobTimeIntervalOptions.Items.Add(resources.GetString("JobTimeIntervalOptions.Items2"))
        Me.JobTimeIntervalOptions.Items.Add(resources.GetString("JobTimeIntervalOptions.Items3"))
        Me.JobTimeIntervalOptions.Items.Add(resources.GetString("JobTimeIntervalOptions.Items4"))
        Me.JobTimeIntervalOptions.Name = "JobTimeIntervalOptions"
        Me.JobTimeIntervalOptions.ReadOnly = True
        '
        'JobStartOffsetLabel
        '
        resources.ApplyResources(Me.JobStartOffsetLabel, "JobStartOffsetLabel")
        Me.JobStartOffsetLabel.Name = "JobStartOffsetLabel"
        '
        'NextButton
        '
        resources.ApplyResources(Me.NextButton, "NextButton")
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.ShortCutTab)
        Me.TabControl.Controls.Add(Me.DefaultsTab)
        Me.TabControl.Controls.Add(Me.SyncPage)
        Me.TabControl.Controls.Add(Me.ServerTab)
        Me.TabControl.Controls.Add(Me.DisplayTab)
        resources.ApplyResources(Me.TabControl, "TabControl")
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        '
        'ShortCutTab
        '
        resources.ApplyResources(Me.ShortCutTab, "ShortCutTab")
        Me.ShortCutTab.Controls.Add(Me.ShortCutList)
        Me.ShortCutTab.Name = "ShortCutTab"
        '
        'ShortCutList
        '
        Me.ShortCutList.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.ShortCutList, "ShortCutList")
        Me.ShortCutList.Name = "ShortCutList"
        '
        'SyncPage
        '
        Me.SyncPage.Controls.Add(Me.DayPickerPanel)
        Me.SyncPage.Controls.Add(Me.AutoSyncPanel)
        resources.ApplyResources(Me.SyncPage, "SyncPage")
        Me.SyncPage.Name = "SyncPage"
        '
        'DayPickerPanel
        '
        Me.DayPickerPanel.Controls.Add(Me.DaysOfWeekLabel)
        Me.DayPickerPanel.Controls.Add(Me.ThursdayLabel)
        Me.DayPickerPanel.Controls.Add(Me.SaturdayLabel)
        Me.DayPickerPanel.Controls.Add(Me.SundayCheckbox)
        Me.DayPickerPanel.Controls.Add(Me.FridayLabel)
        Me.DayPickerPanel.Controls.Add(Me.TuesdayCheckbox)
        Me.DayPickerPanel.Controls.Add(Me.MondayCheckbox)
        Me.DayPickerPanel.Controls.Add(Me.WednesdayCheckbox)
        Me.DayPickerPanel.Controls.Add(Me.ThursdayCheckbox)
        Me.DayPickerPanel.Controls.Add(Me.WednesdayLabel)
        Me.DayPickerPanel.Controls.Add(Me.SaturdayCheckbox)
        Me.DayPickerPanel.Controls.Add(Me.TuesdayLabel)
        Me.DayPickerPanel.Controls.Add(Me.FridayCheckbox)
        Me.DayPickerPanel.Controls.Add(Me.MondayLabel)
        Me.DayPickerPanel.Controls.Add(Me.SundayLabel)
        resources.ApplyResources(Me.DayPickerPanel, "DayPickerPanel")
        Me.DayPickerPanel.Name = "DayPickerPanel"
        '
        'DaysOfWeekLabel
        '
        resources.ApplyResources(Me.DaysOfWeekLabel, "DaysOfWeekLabel")
        Me.DaysOfWeekLabel.Name = "DaysOfWeekLabel"
        '
        'ThursdayLabel
        '
        resources.ApplyResources(Me.ThursdayLabel, "ThursdayLabel")
        Me.ThursdayLabel.Name = "ThursdayLabel"
        '
        'SaturdayLabel
        '
        resources.ApplyResources(Me.SaturdayLabel, "SaturdayLabel")
        Me.SaturdayLabel.Name = "SaturdayLabel"
        '
        'SundayCheckbox
        '
        resources.ApplyResources(Me.SundayCheckbox, "SundayCheckbox")
        Me.SundayCheckbox.Name = "SundayCheckbox"
        '
        'FridayLabel
        '
        resources.ApplyResources(Me.FridayLabel, "FridayLabel")
        Me.FridayLabel.Name = "FridayLabel"
        '
        'TuesdayCheckbox
        '
        resources.ApplyResources(Me.TuesdayCheckbox, "TuesdayCheckbox")
        Me.TuesdayCheckbox.Name = "TuesdayCheckbox"
        '
        'MondayCheckbox
        '
        resources.ApplyResources(Me.MondayCheckbox, "MondayCheckbox")
        Me.MondayCheckbox.Name = "MondayCheckbox"
        '
        'WednesdayCheckbox
        '
        resources.ApplyResources(Me.WednesdayCheckbox, "WednesdayCheckbox")
        Me.WednesdayCheckbox.Name = "WednesdayCheckbox"
        '
        'ThursdayCheckbox
        '
        resources.ApplyResources(Me.ThursdayCheckbox, "ThursdayCheckbox")
        Me.ThursdayCheckbox.Name = "ThursdayCheckbox"
        '
        'WednesdayLabel
        '
        resources.ApplyResources(Me.WednesdayLabel, "WednesdayLabel")
        Me.WednesdayLabel.Name = "WednesdayLabel"
        '
        'SaturdayCheckbox
        '
        resources.ApplyResources(Me.SaturdayCheckbox, "SaturdayCheckbox")
        Me.SaturdayCheckbox.Name = "SaturdayCheckbox"
        '
        'TuesdayLabel
        '
        resources.ApplyResources(Me.TuesdayLabel, "TuesdayLabel")
        Me.TuesdayLabel.Name = "TuesdayLabel"
        '
        'FridayCheckbox
        '
        resources.ApplyResources(Me.FridayCheckbox, "FridayCheckbox")
        Me.FridayCheckbox.Name = "FridayCheckbox"
        '
        'MondayLabel
        '
        resources.ApplyResources(Me.MondayLabel, "MondayLabel")
        Me.MondayLabel.Name = "MondayLabel"
        '
        'SundayLabel
        '
        resources.ApplyResources(Me.SundayLabel, "SundayLabel")
        Me.SundayLabel.Name = "SundayLabel"
        '
        'AutoSyncPanel
        '
        Me.AutoSyncPanel.Controls.Add(Me.MinuteLabel)
        Me.AutoSyncPanel.Controls.Add(Me.SyncFinishLabel)
        Me.AutoSyncPanel.Controls.Add(Me.SyncStartLabel)
        Me.AutoSyncPanel.Controls.Add(Me.SyncFinishTimeSelector)
        Me.AutoSyncPanel.Controls.Add(Me.SyncStartTimeSelector)
        Me.AutoSyncPanel.Controls.Add(Me.SyncIntervalNumberBox)
        Me.AutoSyncPanel.Controls.Add(Me.SyncIntervalLabel)
        Me.AutoSyncPanel.Controls.Add(Me.AutoSyncCheckBox)
        resources.ApplyResources(Me.AutoSyncPanel, "AutoSyncPanel")
        Me.AutoSyncPanel.Name = "AutoSyncPanel"
        '
        'MinuteLabel
        '
        resources.ApplyResources(Me.MinuteLabel, "MinuteLabel")
        Me.MinuteLabel.Name = "MinuteLabel"
        '
        'SyncFinishLabel
        '
        resources.ApplyResources(Me.SyncFinishLabel, "SyncFinishLabel")
        Me.SyncFinishLabel.Name = "SyncFinishLabel"
        '
        'SyncStartLabel
        '
        resources.ApplyResources(Me.SyncStartLabel, "SyncStartLabel")
        Me.SyncStartLabel.Name = "SyncStartLabel"
        '
        'SyncFinishTimeSelector
        '
        resources.ApplyResources(Me.SyncFinishTimeSelector, "SyncFinishTimeSelector")
        Me.SyncFinishTimeSelector.Name = "SyncFinishTimeSelector"
        Me.SyncFinishTimeSelector.ReadOnly = True
        '
        'SyncStartTimeSelector
        '
        Me.SyncStartTimeSelector.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.SyncStartTimeSelector, "SyncStartTimeSelector")
        Me.SyncStartTimeSelector.Name = "SyncStartTimeSelector"
        Me.SyncStartTimeSelector.ReadOnly = True
        '
        'SyncIntervalNumberBox
        '
        resources.ApplyResources(Me.SyncIntervalNumberBox, "SyncIntervalNumberBox")
        Me.SyncIntervalNumberBox.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.SyncIntervalNumberBox.Maximum = New Decimal(New Integer() {240, 0, 0, 0})
        Me.SyncIntervalNumberBox.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.SyncIntervalNumberBox.Name = "SyncIntervalNumberBox"
        Me.SyncIntervalNumberBox.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'SyncIntervalLabel
        '
        resources.ApplyResources(Me.SyncIntervalLabel, "SyncIntervalLabel")
        Me.SyncIntervalLabel.Name = "SyncIntervalLabel"
        '
        'AutoSyncCheckBox
        '
        resources.ApplyResources(Me.AutoSyncCheckBox, "AutoSyncCheckBox")
        Me.AutoSyncCheckBox.Name = "AutoSyncCheckBox"
        '
        'DisplayTab
        '
        Me.DisplayTab.Controls.Add(Me.ExampleText)
        Me.DisplayTab.Controls.Add(Me.TextSizeExampleLabel)
        Me.DisplayTab.Controls.Add(Me.TextSizePanel)
        resources.ApplyResources(Me.DisplayTab, "DisplayTab")
        Me.DisplayTab.Name = "DisplayTab"
        '
        'ExampleText
        '
        resources.ApplyResources(Me.ExampleText, "ExampleText")
        Me.ExampleText.Name = "ExampleText"
        '
        'TextSizeExampleLabel
        '
        resources.ApplyResources(Me.TextSizeExampleLabel, "TextSizeExampleLabel")
        Me.TextSizeExampleLabel.Name = "TextSizeExampleLabel"
        '
        'TextSizePanel
        '
        Me.TextSizePanel.Controls.Add(Me.TextSizeLarge)
        Me.TextSizePanel.Controls.Add(Me.TextSizeSmall)
        Me.TextSizePanel.Controls.Add(Me.TextSizePicker)
        Me.TextSizePanel.Controls.Add(Me.TextSizeLabel)
        resources.ApplyResources(Me.TextSizePanel, "TextSizePanel")
        Me.TextSizePanel.Name = "TextSizePanel"
        '
        'TextSizeLarge
        '
        Me.TextSizeLarge.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.TextSizeLarge, "TextSizeLarge")
        Me.TextSizeLarge.Name = "TextSizeLarge"
        '
        'TextSizeSmall
        '
        Me.TextSizeSmall.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.TextSizeSmall, "TextSizeSmall")
        Me.TextSizeSmall.Name = "TextSizeSmall"
        '
        'TextSizePicker
        '
        resources.ApplyResources(Me.TextSizePicker, "TextSizePicker")
        Me.TextSizePicker.LargeChange = 2
        Me.TextSizePicker.Maximum = 12
        Me.TextSizePicker.Minimum = 4
        Me.TextSizePicker.Name = "TextSizePicker"
        Me.TextSizePicker.TickFrequency = 2
        Me.TextSizePicker.Value = 8
        '
        'TextSizeLabel
        '
        resources.ApplyResources(Me.TextSizeLabel, "TextSizeLabel")
        Me.TextSizeLabel.Name = "TextSizeLabel"
        '
        'MainMenuClose
        '
        resources.ApplyResources(Me.MainMenuClose, "MainMenuClose")
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.Add(Me.MainMenuRemoveCompletedRequests)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClearClientInfo)
        Me.MainMenu.MenuItems.Add(Me.MainMenuFetchUpdatedSchema)
        Me.MainMenu.MenuItems.Add(Me.MainMenuChangeWebServiceCredentials)
        Me.MainMenu.MenuItems.Add(Me.MainMenuChangeActiviserPassword)
        Me.MainMenu.MenuItems.Add(Me.MainMenuChangeServerSettings)
        Me.MainMenu.MenuItems.Add(Me.MainMenuView)
        Me.MainMenu.MenuItems.Add(Me.MainMenuCancel)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClose)
        resources.ApplyResources(Me.MainMenu, "MainMenu")
        '
        'MainMenuRemoveCompletedRequests
        '
        resources.ApplyResources(Me.MainMenuRemoveCompletedRequests, "MainMenuRemoveCompletedRequests")
        '
        'MainMenuClearClientInfo
        '
        resources.ApplyResources(Me.MainMenuClearClientInfo, "MainMenuClearClientInfo")
        '
        'MainMenuFetchUpdatedSchema
        '
        resources.ApplyResources(Me.MainMenuFetchUpdatedSchema, "MainMenuFetchUpdatedSchema")
        '
        'MainMenuChangeWebServiceCredentials
        '
        resources.ApplyResources(Me.MainMenuChangeWebServiceCredentials, "MainMenuChangeWebServiceCredentials")
        '
        'MainMenuChangeActiviserPassword
        '
        resources.ApplyResources(Me.MainMenuChangeActiviserPassword, "MainMenuChangeActiviserPassword")
        '
        'MainMenuChangeServerSettings
        '
        resources.ApplyResources(Me.MainMenuChangeServerSettings, "MainMenuChangeServerSettings")
        '
        'MainMenuView
        '
        Me.MainMenuView.MenuItems.Add(Me.ViewFullScreen)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate0)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate90)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate180)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate270)
        resources.ApplyResources(Me.MainMenuView, "MainMenuView")
        '
        'ViewFullScreen
        '
        resources.ApplyResources(Me.ViewFullScreen, "ViewFullScreen")
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
        'MainMenuCancel
        '
        resources.ApplyResources(Me.MainMenuCancel, "MainMenuCancel")
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.NextButton)
        Me.MenuStrip.MenuItems.Add(Me.MainMenu)
        '
        'FormLabel
        '
        resources.ApplyResources(Me.FormLabel, "FormLabel")
        Me.FormLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.FormLabel.Name = "FormLabel"
        '
        'SettingsHeaderUnderline
        '
        Me.SettingsHeaderUnderline.BackColor = System.Drawing.SystemColors.WindowFrame
        resources.ApplyResources(Me.SettingsHeaderUnderline, "SettingsHeaderUnderline")
        Me.SettingsHeaderUnderline.Name = "SettingsHeaderUnderline"
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.Controls.Add(Me.SettingsHeaderUnderline)
        Me.Controls.Add(Me.FormLabel)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.MinimizeBox = False
        Me.Name = "SettingsForm"
        Me.ServerTab.ResumeLayout(False)
        Me.ServerPanel.ResumeLayout(False)
        Me.DefaultsTab.ResumeLayout(False)
        Me.JobHistoryPanel.ResumeLayout(False)
        Me.JobTimeDefaultsPanel.ResumeLayout(False)
        Me.TabControl.ResumeLayout(False)
        Me.ShortCutTab.ResumeLayout(False)
        Me.SyncPage.ResumeLayout(False)
        Me.DayPickerPanel.ResumeLayout(False)
        Me.AutoSyncPanel.ResumeLayout(False)
        Me.DisplayTab.ResumeLayout(False)
        Me.TextSizePanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ServerTab As System.Windows.Forms.TabPage
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents DefaultsTab As System.Windows.Forms.TabPage
    Friend WithEvents NextButton As System.Windows.Forms.MenuItem
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents MainMenuClose As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuCancel As System.Windows.Forms.MenuItem
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents MainMenuChangeServerSettings As System.Windows.Forms.MenuItem
    Friend WithEvents ShortCutTab As System.Windows.Forms.TabPage
    Friend WithEvents ShortCutList As System.Windows.Forms.TextBox
    Friend WithEvents ServerPanel As System.Windows.Forms.Panel
    Friend WithEvents JobTimesHeader As System.Windows.Forms.Label
    Friend WithEvents DefaultJobDurationOptions As System.Windows.Forms.DomainUpDown
    Friend WithEvents DefaultJobDurationLabel As System.Windows.Forms.Label
    Friend WithEvents JobTimeIntervalOptions As System.Windows.Forms.DomainUpDown
    Friend WithEvents JobTimeIntervalLabel As System.Windows.Forms.Label
    Friend WithEvents DefaultJobStartOffsetOptions As System.Windows.Forms.DomainUpDown
    Friend WithEvents JobStartOffsetLabel As System.Windows.Forms.Label
    Friend WithEvents SyncPage As System.Windows.Forms.TabPage
    Friend WithEvents AutoSyncCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SyncIntervalNumberBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents SyncIntervalLabel As System.Windows.Forms.Label
    Friend WithEvents AutoSyncPanel As System.Windows.Forms.Panel
    Friend WithEvents SyncStartTimeSelector As System.Windows.Forms.DomainUpDown
    Friend WithEvents SyncFinishTimeSelector As System.Windows.Forms.DomainUpDown
    Friend WithEvents SundayCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents FridayCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents SaturdayCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents ThursdayCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents WednesdayCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents MondayCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents TuesdayCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents SaturdayLabel As System.Windows.Forms.Label
    Friend WithEvents FridayLabel As System.Windows.Forms.Label
    Friend WithEvents ThursdayLabel As System.Windows.Forms.Label
    Friend WithEvents WednesdayLabel As System.Windows.Forms.Label
    Friend WithEvents TuesdayLabel As System.Windows.Forms.Label
    Friend WithEvents MondayLabel As System.Windows.Forms.Label
    Friend WithEvents SundayLabel As System.Windows.Forms.Label
    Friend WithEvents DayPickerPanel As System.Windows.Forms.Panel
    Friend WithEvents SyncStartLabel As System.Windows.Forms.Label
    Friend WithEvents SyncFinishLabel As System.Windows.Forms.Label
    Friend WithEvents ViewFullScreen As System.Windows.Forms.MenuItem
    Friend WithEvents DefaultTravelTimeOptions As System.Windows.Forms.DomainUpDown
    Friend WithEvents TravelTimeLabel As System.Windows.Forms.Label
    Friend WithEvents EditContextMenu1 As activiser.EditContextMenu
    Friend WithEvents PasswordContextMenu1 As activiser.PasswordContextMenu
    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
    Friend WithEvents ChangeServerSettingsButton As System.Windows.Forms.Button
    Friend WithEvents WebServiceCredentialsButton As System.Windows.Forms.Button
    Friend WithEvents ActiviserPasswordButton As System.Windows.Forms.Button
    Friend WithEvents MainMenuChangeActiviserPassword As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuChangeWebServiceCredentials As System.Windows.Forms.MenuItem
    Friend WithEvents JobHistoryPanel As System.Windows.Forms.Panel
    Friend WithEvents JobHistoryNumberLabel As System.Windows.Forms.Label
    Friend WithEvents JobHistoryLabel As System.Windows.Forms.Label
    Friend WithEvents HistoryNumberOptions As System.Windows.Forms.DomainUpDown
    Friend WithEvents HistoryAgeOptions As System.Windows.Forms.DomainUpDown
    Friend WithEvents JobHistoryAgeLabel As System.Windows.Forms.Label
    Friend WithEvents MinuteLabel As System.Windows.Forms.Label
    Friend WithEvents DaysOfWeekLabel As System.Windows.Forms.Label
    Friend WithEvents JobTimeDefaultsPanel As System.Windows.Forms.Panel
    Friend WithEvents MainMenuFetchUpdatedSchema As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuRemoveCompletedRequests As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClearClientInfo As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuView As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate0 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate90 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate180 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate270 As System.Windows.Forms.MenuItem
    Friend WithEvents DisplayTab As System.Windows.Forms.TabPage
    Friend WithEvents TextSizeLabel As System.Windows.Forms.Label
    Friend WithEvents TextSizePicker As System.Windows.Forms.TrackBar
    Friend WithEvents TextSizeSmall As System.Windows.Forms.Label
    Friend WithEvents TextSizePanel As System.Windows.Forms.Panel
    Friend WithEvents TextSizeLarge As System.Windows.Forms.Label
    Friend WithEvents TextSizeExampleLabel As System.Windows.Forms.Label
    Friend WithEvents FormLabel As System.Windows.Forms.Label
    Friend WithEvents SettingsHeaderUnderline As System.Windows.Forms.PictureBox
    Friend WithEvents ExampleText As System.Windows.Forms.Label
End Class
