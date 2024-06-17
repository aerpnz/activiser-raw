<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class JobForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobForm))
        Me.ActivityFinishLabel = New System.Windows.Forms.Label
        Me.RequestStatusPanel = New System.Windows.Forms.Panel
        Me.RequestStatusPicker = New System.Windows.Forms.ComboBox
        Me.StatusChangeLabel = New System.Windows.Forms.CheckBox
        Me.JobCompleteLabel = New System.Windows.Forms.CheckBox
        Me.EmailPanel = New System.Windows.Forms.Panel
        Me.Email = New System.Windows.Forms.TextBox
        Me.EditContextMenu1 = New activiser.EditContextMenu
        Me.EmailLabel = New System.Windows.Forms.CheckBox
        Me.ReturnDatePicker = New System.Windows.Forms.DateTimePicker
        Me.ReturnDateLabel = New System.Windows.Forms.CheckBox
        Me.NamePanel = New System.Windows.Forms.Panel
        Me.Signatory = New System.Windows.Forms.TextBox
        Me.ClearSignatureButton = New System.Windows.Forms.Button
        Me.SignatoryLabel = New System.Windows.Forms.Label
        Me.SignaturePanel = New System.Windows.Forms.Panel
        Me.Signature = New activiser.Library.Forms.Signature
        Me.NotesSplitter = New System.Windows.Forms.Splitter
        Me.NotesTab = New System.Windows.Forms.TabPage
        Me.EquipmentPanel = New System.Windows.Forms.Panel
        Me.Equipment = New System.Windows.Forms.TextBox
        Me.EquipmentLabel = New System.Windows.Forms.Label
        Me.ConsultantNotesPanel = New System.Windows.Forms.Panel
        Me.JobNotes = New System.Windows.Forms.TextBox
        Me.ConsultantNotesLabel = New System.Windows.Forms.Label
        Me.SignatureTab = New System.Windows.Forms.TabPage
        Me.FollowUpPanel = New System.Windows.Forms.Panel
        Me.ReturnDatePanel = New System.Windows.Forms.Panel
        Me.JobNumberLabel = New System.Windows.Forms.Label
        Me.GpsLabel = New System.Windows.Forms.Label
        Me.ViewRotate180 = New System.Windows.Forms.MenuItem
        Me.ViewRotate270 = New System.Windows.Forms.MenuItem
        Me.ViewRotate90 = New System.Windows.Forms.MenuItem
        Me.ShowShortCutList = New System.Windows.Forms.MenuItem
        Me.ViewRotate0 = New System.Windows.Forms.MenuItem
        Me.NextButton = New System.Windows.Forms.MenuItem
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.MainMenuSave = New System.Windows.Forms.MenuItem
        Me.MainMenuClose = New System.Windows.Forms.MenuItem
        Me.MainMenuSynchronise = New System.Windows.Forms.MenuItem
        Me.ShowActivitiesFinishTimeMenuItem = New System.Windows.Forms.MenuItem
        Me.ViewFullScreen = New System.Windows.Forms.MenuItem
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.MainMenu = New System.Windows.Forms.MenuItem
        Me.MainMenuRequestDetails = New System.Windows.Forms.MenuItem
        Me.MainMenuClientDetails = New System.Windows.Forms.MenuItem
        Me.MainMenuSeparator2 = New System.Windows.Forms.MenuItem
        Me.MainMenuGps = New System.Windows.Forms.MenuItem
        Me.MainMenuGpsInfo = New System.Windows.Forms.MenuItem
        Me.MainMenuGpsOn = New System.Windows.Forms.MenuItem
        Me.MainMenuGpsOff = New System.Windows.Forms.MenuItem
        Me.MainMenuView = New System.Windows.Forms.MenuItem
        Me.MainMenuSeparator1 = New System.Windows.Forms.MenuItem
        Me.MainMenuCancel = New System.Windows.Forms.MenuItem
        Me.ClientLabel = New System.Windows.Forms.Label
        Me.HeaderSplitter = New System.Windows.Forms.Splitter
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.JobHeaderTab = New System.Windows.Forms.TabPage
        Me.HeaderPanel = New System.Windows.Forms.Panel
        Me.DescriptionPanel = New System.Windows.Forms.Panel
        Me.RequestDescription = New System.Windows.Forms.TextBox
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.RequestDescriptionLabel = New System.Windows.Forms.Label
        Me.ClientNamePanel = New System.Windows.Forms.Panel
        Me.ClientSiteNameTextBox = New System.Windows.Forms.TextBox
        Me.TimePanel = New System.Windows.Forms.Panel
        Me.FinishTimePicker = New System.Windows.Forms.DateTimePicker
        Me.StartDatePicker = New System.Windows.Forms.DateTimePicker
        Me.NoTravelTimeLabel = New System.Windows.Forms.CheckBox
        Me.FinishDatePicker = New System.Windows.Forms.DateTimePicker
        Me.FinishNowButton = New System.Windows.Forms.Button
        Me.TravelTimePicker = New System.Windows.Forms.DateTimePicker
        Me.FinishLabel = New System.Windows.Forms.Label
        Me.StartTimePicker = New System.Windows.Forms.DateTimePicker
        Me.TravelTimeLabel = New System.Windows.Forms.Label
        Me.StartNowButton = New System.Windows.Forms.Button
        Me.StartLabel = New System.Windows.Forms.Label
        Me.RequestNumberPanel = New System.Windows.Forms.Panel
        Me.RequestNumberTextBox = New System.Windows.Forms.TextBox
        Me.RequestLabel = New System.Windows.Forms.Label
        Me.ActivityTab = New System.Windows.Forms.TabPage
        Me.JobDetails = New System.Windows.Forms.TextBox
        Me.ActivityTimePanel = New System.Windows.Forms.Panel
        Me.ActivityFinishNowButton = New System.Windows.Forms.Button
        Me.ActivityFinishTimePicker = New System.Windows.Forms.DateTimePicker
        Me.ActivityFinishDatePicker = New System.Windows.Forms.DateTimePicker
        Me.ActivityShortCutPanel = New System.Windows.Forms.Panel
        Me.ActivityShortCutComboBox = New System.Windows.Forms.ComboBox
        Me.ActivityShortCutSetupButton = New activiser.Library.Forms.ImageButton
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.RequestStatusPanel.SuspendLayout()
        Me.EmailPanel.SuspendLayout()
        Me.NamePanel.SuspendLayout()
        Me.SignaturePanel.SuspendLayout()
        Me.NotesTab.SuspendLayout()
        Me.EquipmentPanel.SuspendLayout()
        Me.ConsultantNotesPanel.SuspendLayout()
        Me.SignatureTab.SuspendLayout()
        Me.FollowUpPanel.SuspendLayout()
        Me.ReturnDatePanel.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.JobHeaderTab.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        Me.DescriptionPanel.SuspendLayout()
        Me.ClientNamePanel.SuspendLayout()
        Me.TimePanel.SuspendLayout()
        Me.RequestNumberPanel.SuspendLayout()
        Me.ActivityTab.SuspendLayout()
        Me.ActivityTimePanel.SuspendLayout()
        Me.ActivityShortCutPanel.SuspendLayout()
        CType(Me.ActivityShortCutSetupButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ActivityFinishLabel
        '
        resources.ApplyResources(Me.ActivityFinishLabel, "ActivityFinishLabel")
        Me.ActivityFinishLabel.Name = "ActivityFinishLabel"
        '
        'RequestStatusPanel
        '
        Me.RequestStatusPanel.Controls.Add(Me.RequestStatusPicker)
        Me.RequestStatusPanel.Controls.Add(Me.StatusChangeLabel)
        resources.ApplyResources(Me.RequestStatusPanel, "RequestStatusPanel")
        Me.RequestStatusPanel.Name = "RequestStatusPanel"
        '
        'RequestStatusPicker
        '
        resources.ApplyResources(Me.RequestStatusPicker, "RequestStatusPicker")
        Me.RequestStatusPicker.Name = "RequestStatusPicker"
        '
        'StatusChangeLabel
        '
        resources.ApplyResources(Me.StatusChangeLabel, "StatusChangeLabel")
        Me.StatusChangeLabel.Name = "StatusChangeLabel"
        '
        'JobCompleteLabel
        '
        resources.ApplyResources(Me.JobCompleteLabel, "JobCompleteLabel")
        Me.JobCompleteLabel.Name = "JobCompleteLabel"
        '
        'EmailPanel
        '
        Me.EmailPanel.BackColor = System.Drawing.SystemColors.Control
        Me.EmailPanel.Controls.Add(Me.Email)
        Me.EmailPanel.Controls.Add(Me.EmailLabel)
        resources.ApplyResources(Me.EmailPanel, "EmailPanel")
        Me.EmailPanel.Name = "EmailPanel"
        '
        'Email
        '
        resources.ApplyResources(Me.Email, "Email")
        Me.Email.ContextMenu = Me.EditContextMenu1
        Me.Email.Name = "Email"
        Me.Email.ReadOnly = True
        Me.Email.Tag = "Email"
        '
        'EditContextMenu1
        '
        'Me.EditContextMenu1.ShowCall = False
        '
        'EmailLabel
        '
        resources.ApplyResources(Me.EmailLabel, "EmailLabel")
        Me.EmailLabel.Name = "EmailLabel"
        '
        'ReturnDatePicker
        '
        resources.ApplyResources(Me.ReturnDatePicker, "ReturnDatePicker")
        Me.ReturnDatePicker.ContextMenu = Me.EditContextMenu1
        Me.ReturnDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ReturnDatePicker.Name = "ReturnDatePicker"
        '
        'ReturnDateLabel
        '
        resources.ApplyResources(Me.ReturnDateLabel, "ReturnDateLabel")
        Me.ReturnDateLabel.Name = "ReturnDateLabel"
        '
        'NamePanel
        '
        Me.NamePanel.BackColor = System.Drawing.SystemColors.Info
        Me.NamePanel.Controls.Add(Me.Signatory)
        Me.NamePanel.Controls.Add(Me.ClearSignatureButton)
        Me.NamePanel.Controls.Add(Me.SignatoryLabel)
        resources.ApplyResources(Me.NamePanel, "NamePanel")
        Me.NamePanel.Name = "NamePanel"
        '
        'Signatory
        '
        resources.ApplyResources(Me.Signatory, "Signatory")
        Me.Signatory.ContextMenu = Me.EditContextMenu1
        Me.Signatory.Name = "Signatory"
        Me.Signatory.Tag = "Signatory"
        '
        'ClearSignatureButton
        '
        resources.ApplyResources(Me.ClearSignatureButton, "ClearSignatureButton")
        Me.ClearSignatureButton.Name = "ClearSignatureButton"
        '
        'SignatoryLabel
        '
        resources.ApplyResources(Me.SignatoryLabel, "SignatoryLabel")
        Me.SignatoryLabel.Name = "SignatoryLabel"
        '
        'SignaturePanel
        '
        Me.SignaturePanel.Controls.Add(Me.Signature)
        resources.ApplyResources(Me.SignaturePanel, "SignaturePanel")
        Me.SignaturePanel.Name = "SignaturePanel"
        '
        'Signature
        '
        resources.ApplyResources(Me.Signature, "Signature")
        Me.Signature.BackColor = System.Drawing.SystemColors.Info
        Me.Signature.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.Signature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Signature.Name = "Signature"
        Me.Signature.SignatureString = ""
        '
        'NotesSplitter
        '
        resources.ApplyResources(Me.NotesSplitter, "NotesSplitter")
        Me.NotesSplitter.Name = "NotesSplitter"
        '
        'NotesTab
        '
        Me.NotesTab.Controls.Add(Me.EquipmentPanel)
        Me.NotesTab.Controls.Add(Me.NotesSplitter)
        Me.NotesTab.Controls.Add(Me.ConsultantNotesPanel)
        resources.ApplyResources(Me.NotesTab, "NotesTab")
        Me.NotesTab.Name = "NotesTab"
        '
        'EquipmentPanel
        '
        resources.ApplyResources(Me.EquipmentPanel, "EquipmentPanel")
        Me.EquipmentPanel.Controls.Add(Me.Equipment)
        Me.EquipmentPanel.Controls.Add(Me.EquipmentLabel)
        Me.EquipmentPanel.Name = "EquipmentPanel"
        '
        'Equipment
        '
        Me.Equipment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Equipment.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.Equipment, "Equipment")
        Me.Equipment.Name = "Equipment"
        Me.Equipment.Tag = "Equipment"
        '
        'EquipmentLabel
        '
        Me.EquipmentLabel.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.EquipmentLabel, "EquipmentLabel")
        Me.EquipmentLabel.Name = "EquipmentLabel"
        '
        'ConsultantNotesPanel
        '
        Me.ConsultantNotesPanel.Controls.Add(Me.JobNotes)
        Me.ConsultantNotesPanel.Controls.Add(Me.ConsultantNotesLabel)
        resources.ApplyResources(Me.ConsultantNotesPanel, "ConsultantNotesPanel")
        Me.ConsultantNotesPanel.Name = "ConsultantNotesPanel"
        '
        'JobNotes
        '
        Me.JobNotes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.JobNotes.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.JobNotes, "JobNotes")
        Me.JobNotes.Name = "JobNotes"
        Me.JobNotes.Tag = "JobNotes"
        '
        'ConsultantNotesLabel
        '
        Me.ConsultantNotesLabel.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.ConsultantNotesLabel, "ConsultantNotesLabel")
        Me.ConsultantNotesLabel.Name = "ConsultantNotesLabel"
        '
        'SignatureTab
        '
        resources.ApplyResources(Me.SignatureTab, "SignatureTab")
        Me.SignatureTab.Controls.Add(Me.JobCompleteLabel)
        Me.SignatureTab.Controls.Add(Me.RequestStatusPanel)
        Me.SignatureTab.Controls.Add(Me.FollowUpPanel)
        Me.SignatureTab.Controls.Add(Me.JobNumberLabel)
        Me.SignatureTab.Controls.Add(Me.GpsLabel)
        Me.SignatureTab.Name = "SignatureTab"
        '
        'FollowUpPanel
        '
        Me.FollowUpPanel.Controls.Add(Me.ReturnDatePanel)
        Me.FollowUpPanel.Controls.Add(Me.EmailPanel)
        Me.FollowUpPanel.Controls.Add(Me.NamePanel)
        Me.FollowUpPanel.Controls.Add(Me.SignaturePanel)
        resources.ApplyResources(Me.FollowUpPanel, "FollowUpPanel")
        Me.FollowUpPanel.Name = "FollowUpPanel"
        '
        'ReturnDatePanel
        '
        Me.ReturnDatePanel.BackColor = System.Drawing.SystemColors.Control
        Me.ReturnDatePanel.Controls.Add(Me.ReturnDateLabel)
        Me.ReturnDatePanel.Controls.Add(Me.ReturnDatePicker)
        resources.ApplyResources(Me.ReturnDatePanel, "ReturnDatePanel")
        Me.ReturnDatePanel.Name = "ReturnDatePanel"
        '
        'JobNumberLabel
        '
        resources.ApplyResources(Me.JobNumberLabel, "JobNumberLabel")
        Me.JobNumberLabel.Name = "JobNumberLabel"
        '
        'GpsLabel
        '
        resources.ApplyResources(Me.GpsLabel, "GpsLabel")
        Me.GpsLabel.Name = "GpsLabel"
        '
        'ViewRotate180
        '
        resources.ApplyResources(Me.ViewRotate180, "ViewRotate180")
        '
        'ViewRotate270
        '
        resources.ApplyResources(Me.ViewRotate270, "ViewRotate270")
        '
        'ViewRotate90
        '
        resources.ApplyResources(Me.ViewRotate90, "ViewRotate90")
        '
        'ShowShortCutList
        '
        resources.ApplyResources(Me.ShowShortCutList, "ShowShortCutList")
        '
        'ViewRotate0
        '
        resources.ApplyResources(Me.ViewRotate0, "ViewRotate0")
        '
        'NextButton
        '
        resources.ApplyResources(Me.NextButton, "NextButton")
        '
        'InputPanel
        '
        '
        'MainMenuSave
        '
        resources.ApplyResources(Me.MainMenuSave, "MainMenuSave")
        '
        'MainMenuClose
        '
        resources.ApplyResources(Me.MainMenuClose, "MainMenuClose")
        '
        'MainMenuSynchronise
        '
        resources.ApplyResources(Me.MainMenuSynchronise, "MainMenuSynchronise")
        '
        'ShowActivitiesFinishTimeMenuItem
        '
        Me.ShowActivitiesFinishTimeMenuItem.Checked = True
        resources.ApplyResources(Me.ShowActivitiesFinishTimeMenuItem, "ShowActivitiesFinishTimeMenuItem")
        '
        'ViewFullScreen
        '
        resources.ApplyResources(Me.ViewFullScreen, "ViewFullScreen")
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.NextButton)
        Me.MenuStrip.MenuItems.Add(Me.MainMenu)
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.Add(Me.MainMenuRequestDetails)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClientDetails)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSynchronise)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSeparator2)
        Me.MainMenu.MenuItems.Add(Me.MainMenuGps)
        Me.MainMenu.MenuItems.Add(Me.MainMenuView)
        Me.MainMenu.MenuItems.Add(Me.ViewFullScreen)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSeparator1)
        Me.MainMenu.MenuItems.Add(Me.MainMenuCancel)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSave)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClose)
        resources.ApplyResources(Me.MainMenu, "MainMenu")
        '
        'MainMenuRequestDetails
        '
        resources.ApplyResources(Me.MainMenuRequestDetails, "MainMenuRequestDetails")
        '
        'MainMenuClientDetails
        '
        resources.ApplyResources(Me.MainMenuClientDetails, "MainMenuClientDetails")
        '
        'MainMenuSeparator2
        '
        resources.ApplyResources(Me.MainMenuSeparator2, "MainMenuSeparator2")
        '
        'MainMenuGps
        '
        Me.MainMenuGps.MenuItems.Add(Me.MainMenuGpsInfo)
        Me.MainMenuGps.MenuItems.Add(Me.MainMenuGpsOn)
        Me.MainMenuGps.MenuItems.Add(Me.MainMenuGpsOff)
        resources.ApplyResources(Me.MainMenuGps, "MainMenuGps")
        '
        'MainMenuGpsInfo
        '
        resources.ApplyResources(Me.MainMenuGpsInfo, "MainMenuGpsInfo")
        '
        'MainMenuGpsOn
        '
        resources.ApplyResources(Me.MainMenuGpsOn, "MainMenuGpsOn")
        '
        'MainMenuGpsOff
        '
        resources.ApplyResources(Me.MainMenuGpsOff, "MainMenuGpsOff")
        '
        'MainMenuView
        '
        Me.MainMenuView.MenuItems.Add(Me.ShowActivitiesFinishTimeMenuItem)
        Me.MainMenuView.MenuItems.Add(Me.ShowShortCutList)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate0)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate90)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate180)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate270)
        resources.ApplyResources(Me.MainMenuView, "MainMenuView")
        '
        'MainMenuSeparator1
        '
        resources.ApplyResources(Me.MainMenuSeparator1, "MainMenuSeparator1")
        '
        'MainMenuCancel
        '
        resources.ApplyResources(Me.MainMenuCancel, "MainMenuCancel")
        '
        'ClientLabel
        '
        resources.ApplyResources(Me.ClientLabel, "ClientLabel")
        Me.ClientLabel.Name = "ClientLabel"
        '
        'HeaderSplitter
        '
        resources.ApplyResources(Me.HeaderSplitter, "HeaderSplitter")
        Me.HeaderSplitter.Name = "HeaderSplitter"
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.JobHeaderTab)
        Me.TabControl.Controls.Add(Me.ActivityTab)
        Me.TabControl.Controls.Add(Me.NotesTab)
        Me.TabControl.Controls.Add(Me.SignatureTab)
        resources.ApplyResources(Me.TabControl, "TabControl")
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        '
        'JobHeaderTab
        '
        Me.JobHeaderTab.Controls.Add(Me.HeaderPanel)
        resources.ApplyResources(Me.JobHeaderTab, "JobHeaderTab")
        Me.JobHeaderTab.Name = "JobHeaderTab"
        '
        'HeaderPanel
        '
        resources.ApplyResources(Me.HeaderPanel, "HeaderPanel")
        Me.HeaderPanel.Controls.Add(Me.DescriptionPanel)
        Me.HeaderPanel.Controls.Add(Me.ClientNamePanel)
        Me.HeaderPanel.Controls.Add(Me.HeaderSplitter)
        Me.HeaderPanel.Controls.Add(Me.TimePanel)
        Me.HeaderPanel.Controls.Add(Me.RequestNumberPanel)
        Me.HeaderPanel.Name = "HeaderPanel"
        '
        'DescriptionPanel
        '
        resources.ApplyResources(Me.DescriptionPanel, "DescriptionPanel")
        Me.DescriptionPanel.Controls.Add(Me.RequestDescription)
        Me.DescriptionPanel.Controls.Add(Me.RequestDescriptionLabel)
        Me.DescriptionPanel.Name = "DescriptionPanel"
        '
        'RequestDescription
        '
        Me.RequestDescription.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.RequestDescription, "RequestDescription")
        Me.RequestDescription.Name = "RequestDescription"
        Me.RequestDescription.ReadOnly = True
        '
        'ReadOnlyContextMenu1
        '
        'Me.ReadOnlyContextMenu1.ShowCall = False
        '
        'RequestDescriptionLabel
        '
        resources.ApplyResources(Me.RequestDescriptionLabel, "RequestDescriptionLabel")
        Me.RequestDescriptionLabel.Name = "RequestDescriptionLabel"
        '
        'ClientNamePanel
        '
        Me.ClientNamePanel.Controls.Add(Me.ClientSiteNameTextBox)
        Me.ClientNamePanel.Controls.Add(Me.ClientLabel)
        resources.ApplyResources(Me.ClientNamePanel, "ClientNamePanel")
        Me.ClientNamePanel.Name = "ClientNamePanel"
        '
        'ClientSiteNameTextBox
        '
        Me.ClientSiteNameTextBox.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.ClientSiteNameTextBox, "ClientSiteNameTextBox")
        Me.ClientSiteNameTextBox.Name = "ClientSiteNameTextBox"
        Me.ClientSiteNameTextBox.ReadOnly = True
        '
        'TimePanel
        '
        resources.ApplyResources(Me.TimePanel, "TimePanel")
        Me.TimePanel.Controls.Add(Me.FinishTimePicker)
        Me.TimePanel.Controls.Add(Me.StartDatePicker)
        Me.TimePanel.Controls.Add(Me.NoTravelTimeLabel)
        Me.TimePanel.Controls.Add(Me.FinishDatePicker)
        Me.TimePanel.Controls.Add(Me.FinishNowButton)
        Me.TimePanel.Controls.Add(Me.TravelTimePicker)
        Me.TimePanel.Controls.Add(Me.FinishLabel)
        Me.TimePanel.Controls.Add(Me.StartTimePicker)
        Me.TimePanel.Controls.Add(Me.TravelTimeLabel)
        Me.TimePanel.Controls.Add(Me.StartNowButton)
        Me.TimePanel.Controls.Add(Me.StartLabel)
        Me.TimePanel.Name = "TimePanel"
        '
        'FinishTimePicker
        '
        Me.FinishTimePicker.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.FinishTimePicker, "FinishTimePicker")
        Me.FinishTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FinishTimePicker.Name = "FinishTimePicker"
        Me.FinishTimePicker.ShowUpDown = True
        '
        'StartDatePicker
        '
        resources.ApplyResources(Me.StartDatePicker, "StartDatePicker")
        Me.StartDatePicker.ContextMenu = Me.EditContextMenu1
        Me.StartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.StartDatePicker.Name = "StartDatePicker"
        '
        'NoTravelTimeLabel
        '
        resources.ApplyResources(Me.NoTravelTimeLabel, "NoTravelTimeLabel")
        Me.NoTravelTimeLabel.Name = "NoTravelTimeLabel"
        '
        'FinishDatePicker
        '
        Me.FinishDatePicker.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.FinishDatePicker, "FinishDatePicker")
        Me.FinishDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FinishDatePicker.Name = "FinishDatePicker"
        '
        'FinishNowButton
        '
        resources.ApplyResources(Me.FinishNowButton, "FinishNowButton")
        Me.FinishNowButton.Name = "FinishNowButton"
        '
        'TravelTimePicker
        '
        Me.TravelTimePicker.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.TravelTimePicker, "TravelTimePicker")
        Me.TravelTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TravelTimePicker.MaxDate = New Date(9990, 12, 31, 0, 0, 0, 0)
        Me.TravelTimePicker.Name = "TravelTimePicker"
        Me.TravelTimePicker.ShowUpDown = True
        Me.TravelTimePicker.Value = New Date(2004, 12, 1, 0, 0, 0, 0)
        '
        'FinishLabel
        '
        resources.ApplyResources(Me.FinishLabel, "FinishLabel")
        Me.FinishLabel.Name = "FinishLabel"
        '
        'StartTimePicker
        '
        Me.StartTimePicker.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.StartTimePicker, "StartTimePicker")
        Me.StartTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.StartTimePicker.Name = "StartTimePicker"
        Me.StartTimePicker.ShowUpDown = True
        '
        'TravelTimeLabel
        '
        resources.ApplyResources(Me.TravelTimeLabel, "TravelTimeLabel")
        Me.TravelTimeLabel.Name = "TravelTimeLabel"
        '
        'StartNowButton
        '
        resources.ApplyResources(Me.StartNowButton, "StartNowButton")
        Me.StartNowButton.Name = "StartNowButton"
        '
        'StartLabel
        '
        resources.ApplyResources(Me.StartLabel, "StartLabel")
        Me.StartLabel.Name = "StartLabel"
        '
        'RequestNumberPanel
        '
        Me.RequestNumberPanel.Controls.Add(Me.RequestNumberTextBox)
        Me.RequestNumberPanel.Controls.Add(Me.RequestLabel)
        resources.ApplyResources(Me.RequestNumberPanel, "RequestNumberPanel")
        Me.RequestNumberPanel.Name = "RequestNumberPanel"
        '
        'RequestNumberTextBox
        '
        Me.RequestNumberTextBox.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.RequestNumberTextBox, "RequestNumberTextBox")
        Me.RequestNumberTextBox.Name = "RequestNumberTextBox"
        Me.RequestNumberTextBox.ReadOnly = True
        '
        'RequestLabel
        '
        resources.ApplyResources(Me.RequestLabel, "RequestLabel")
        Me.RequestLabel.Name = "RequestLabel"
        Me.RequestLabel.Tag = ""
        '
        'ActivityTab
        '
        Me.ActivityTab.Controls.Add(Me.JobDetails)
        Me.ActivityTab.Controls.Add(Me.ActivityTimePanel)
        Me.ActivityTab.Controls.Add(Me.ActivityShortCutPanel)
        resources.ApplyResources(Me.ActivityTab, "ActivityTab")
        Me.ActivityTab.Name = "ActivityTab"
        '
        'JobDetails
        '
        Me.JobDetails.AcceptsReturn = True
        Me.JobDetails.AcceptsTab = True
        Me.JobDetails.ContextMenu = Me.EditContextMenu1
        resources.ApplyResources(Me.JobDetails, "JobDetails")
        Me.JobDetails.Name = "JobDetails"
        Me.JobDetails.Tag = "JobDetails"
        '
        'ActivityTimePanel
        '
        Me.ActivityTimePanel.Controls.Add(Me.ActivityFinishNowButton)
        Me.ActivityTimePanel.Controls.Add(Me.ActivityFinishTimePicker)
        Me.ActivityTimePanel.Controls.Add(Me.ActivityFinishDatePicker)
        Me.ActivityTimePanel.Controls.Add(Me.ActivityFinishLabel)
        resources.ApplyResources(Me.ActivityTimePanel, "ActivityTimePanel")
        Me.ActivityTimePanel.Name = "ActivityTimePanel"
        '
        'ActivityFinishNowButton
        '
        resources.ApplyResources(Me.ActivityFinishNowButton, "ActivityFinishNowButton")
        Me.ActivityFinishNowButton.Name = "ActivityFinishNowButton"
        '
        'ActivityFinishTimePicker
        '
        resources.ApplyResources(Me.ActivityFinishTimePicker, "ActivityFinishTimePicker")
        Me.ActivityFinishTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ActivityFinishTimePicker.MaxDate = New Date(9990, 12, 31, 0, 0, 0, 0)
        Me.ActivityFinishTimePicker.Name = "ActivityFinishTimePicker"
        Me.ActivityFinishTimePicker.ShowUpDown = True
        '
        'ActivityFinishDatePicker
        '
        resources.ApplyResources(Me.ActivityFinishDatePicker, "ActivityFinishDatePicker")
        Me.ActivityFinishDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ActivityFinishDatePicker.MaxDate = New Date(9990, 12, 31, 0, 0, 0, 0)
        Me.ActivityFinishDatePicker.Name = "ActivityFinishDatePicker"
        '
        'ActivityShortCutPanel
        '
        Me.ActivityShortCutPanel.BackColor = System.Drawing.SystemColors.Info
        Me.ActivityShortCutPanel.Controls.Add(Me.ActivityShortCutComboBox)
        Me.ActivityShortCutPanel.Controls.Add(Me.ActivityShortCutSetupButton)
        resources.ApplyResources(Me.ActivityShortCutPanel, "ActivityShortCutPanel")
        Me.ActivityShortCutPanel.Name = "ActivityShortCutPanel"
        '
        'ActivityShortCutComboBox
        '
        Me.ActivityShortCutComboBox.BackColor = System.Drawing.SystemColors.Info
        Me.ActivityShortCutComboBox.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.ActivityShortCutComboBox, "ActivityShortCutComboBox")
        Me.ActivityShortCutComboBox.Name = "ActivityShortCutComboBox"
        '
        'ActivityShortCutSetupButton
        '
        Me.ActivityShortCutSetupButton.BackColor = System.Drawing.SystemColors.Info
        Me.ActivityShortCutSetupButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ActivityShortCutSetupButton.ClickMaskColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.ActivityShortCutSetupButton, "ActivityShortCutSetupButton")
        Me.ActivityShortCutSetupButton.Image = Nothing
        Me.ActivityShortCutSetupButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.ActivityShortCutSetupButton.Name = "ActivityShortCutSetupButton"
        Me.ActivityShortCutSetupButton.TextAlignment = activiser.Library.Forms.ContentAlignment.MiddleCenter
        Me.ActivityShortCutSetupButton.TextVisible = False
        '
        'InputPanelPanel
        '
        resources.ApplyResources(Me.InputPanelPanel, "InputPanelPanel")
        Me.InputPanelPanel.Name = "InputPanelPanel"
        '
        'JobForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.MinimizeBox = False
        Me.Name = "JobForm"
        Me.RequestStatusPanel.ResumeLayout(False)
        Me.EmailPanel.ResumeLayout(False)
        Me.NamePanel.ResumeLayout(False)
        Me.SignaturePanel.ResumeLayout(False)
        Me.NotesTab.ResumeLayout(False)
        Me.EquipmentPanel.ResumeLayout(False)
        Me.ConsultantNotesPanel.ResumeLayout(False)
        Me.SignatureTab.ResumeLayout(False)
        Me.FollowUpPanel.ResumeLayout(False)
        Me.ReturnDatePanel.ResumeLayout(False)
        Me.TabControl.ResumeLayout(False)
        Me.JobHeaderTab.ResumeLayout(False)
        Me.HeaderPanel.ResumeLayout(False)
        Me.DescriptionPanel.ResumeLayout(False)
        Me.ClientNamePanel.ResumeLayout(False)
        Me.TimePanel.ResumeLayout(False)
        Me.RequestNumberPanel.ResumeLayout(False)
        Me.ActivityTab.ResumeLayout(False)
        Me.ActivityTimePanel.ResumeLayout(False)
        Me.ActivityShortCutPanel.ResumeLayout(False)
        CType(Me.ActivityShortCutSetupButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Email As System.Windows.Forms.TextBox
    Friend WithEvents EmailPanel As System.Windows.Forms.Panel
    Friend WithEvents EmailLabel As System.Windows.Forms.CheckBox
    Friend WithEvents ReturnDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ReturnDateLabel As System.Windows.Forms.CheckBox
    Friend WithEvents NamePanel As System.Windows.Forms.Panel
    Friend WithEvents Signatory As System.Windows.Forms.TextBox
    Friend WithEvents ClearSignatureButton As System.Windows.Forms.Button
    Friend WithEvents SignatoryLabel As System.Windows.Forms.Label
    Friend WithEvents SignaturePanel As System.Windows.Forms.Panel
    Friend WithEvents Signature As activiser.Library.Forms.Signature
    Friend WithEvents NotesSplitter As System.Windows.Forms.Splitter
    Friend WithEvents NotesTab As System.Windows.Forms.TabPage
    Friend WithEvents ConsultantNotesPanel As System.Windows.Forms.Panel
    Friend WithEvents JobNotes As System.Windows.Forms.TextBox
    Friend WithEvents ConsultantNotesLabel As System.Windows.Forms.Label
    Friend WithEvents SignatureTab As System.Windows.Forms.TabPage
    Friend WithEvents FollowUpPanel As System.Windows.Forms.Panel
    Friend WithEvents ViewRotate180 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate270 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate90 As System.Windows.Forms.MenuItem
    Friend WithEvents ShowShortCutList As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate0 As System.Windows.Forms.MenuItem
    Friend WithEvents NextButton As System.Windows.Forms.MenuItem
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents MainMenuSave As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClose As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSynchronise As System.Windows.Forms.MenuItem
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents MainMenu As System.Windows.Forms.MenuItem
    Friend WithEvents ClientLabel As System.Windows.Forms.Label
    Friend WithEvents RequestNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents HeaderSplitter As System.Windows.Forms.Splitter
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents JobHeaderTab As System.Windows.Forms.TabPage
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents DescriptionPanel As System.Windows.Forms.Panel
    Friend WithEvents ClientSiteNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RequestLabel As System.Windows.Forms.Label
    Friend WithEvents RequestDescription As System.Windows.Forms.TextBox
    Friend WithEvents RequestDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents TimePanel As System.Windows.Forms.Panel
    Friend WithEvents FinishTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents StartDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents NoTravelTimeLabel As System.Windows.Forms.CheckBox
    Friend WithEvents FinishDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents FinishNowButton As System.Windows.Forms.Button
    Friend WithEvents TravelTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents FinishLabel As System.Windows.Forms.Label
    Friend WithEvents StartTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents TravelTimeLabel As System.Windows.Forms.Label
    Friend WithEvents StartNowButton As System.Windows.Forms.Button
    Friend WithEvents StartLabel As System.Windows.Forms.Label
    Friend WithEvents ActivityTab As System.Windows.Forms.TabPage
    Friend WithEvents ActivityTimePanel As System.Windows.Forms.Panel
    Friend WithEvents ActivityFinishTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ActivityFinishDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ActivityFinishNowButton As System.Windows.Forms.Button
    Friend WithEvents ActivityShortCutPanel As System.Windows.Forms.Panel
    Friend WithEvents ActivityShortCutComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ActivityShortCutSetupButton As activiser.Library.Forms.ImageButton
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents MainMenuCancel As System.Windows.Forms.MenuItem
    Friend WithEvents ActivityFinishLabel As System.Windows.Forms.Label
    Friend WithEvents ShowActivitiesFinishTimeMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents ViewFullScreen As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuView As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSeparator2 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSeparator1 As System.Windows.Forms.MenuItem
    Friend WithEvents EquipmentPanel As System.Windows.Forms.Panel
    Friend WithEvents Equipment As System.Windows.Forms.TextBox
    Friend WithEvents EquipmentLabel As System.Windows.Forms.Label
    Friend WithEvents JobDetails As System.Windows.Forms.TextBox
    Friend WithEvents EditContextMenu1 As activiser.EditContextMenu
    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
    Friend WithEvents MainMenuGpsInfo As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuGps As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuGpsOn As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuGpsOff As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuRequestDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClientDetails As System.Windows.Forms.MenuItem
    Friend WithEvents RequestNumberPanel As System.Windows.Forms.Panel
    Friend WithEvents JobCompleteLabel As System.Windows.Forms.CheckBox
    Friend WithEvents RequestStatusPanel As System.Windows.Forms.Panel
    Friend WithEvents RequestStatusPicker As System.Windows.Forms.ComboBox
    Friend WithEvents StatusChangeLabel As System.Windows.Forms.CheckBox
    Friend WithEvents ClientNamePanel As System.Windows.Forms.Panel
    Friend WithEvents JobNumberLabel As System.Windows.Forms.Label
    Friend WithEvents GpsLabel As System.Windows.Forms.Label
    Friend WithEvents ReturnDatePanel As System.Windows.Forms.Panel
End Class
