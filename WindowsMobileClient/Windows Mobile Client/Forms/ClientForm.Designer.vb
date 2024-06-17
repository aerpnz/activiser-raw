<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ClientForm
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
        Me.JobRequestNumberColumn = New System.Windows.Forms.ColumnHeader
        Me.JobDateColumn = New System.Windows.Forms.ColumnHeader
        Me.JobTab = New System.Windows.Forms.TabPage
        Me.JobList = New System.Windows.Forms.ListView
        Me.JobConsultantColumn = New System.Windows.Forms.ColumnHeader
        Me.JobMenu = New System.Windows.Forms.ContextMenu
        Me.JobMenuOpen = New System.Windows.Forms.MenuItem
        Me.JobMenuRemove = New System.Windows.Forms.MenuItem
        Me.JobAndDetailSplitter = New System.Windows.Forms.Splitter
        Me.JobDetailsTextBox = New System.Windows.Forms.TextBox
        Me.JobButtonPanel = New System.Windows.Forms.Panel
        Me.OpenJobButton = New System.Windows.Forms.Button
        Me.GetHistoryButton = New System.Windows.Forms.Button
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.OpenRequestButton = New System.Windows.Forms.Button
        Me.RequestDateColumn = New System.Windows.Forms.ColumnHeader
        Me.RequestDescriptionColumn = New System.Windows.Forms.ColumnHeader
        Me.RequestNumberColumn = New System.Windows.Forms.ColumnHeader
        Me.NewRequestButton = New System.Windows.Forms.Button
        Me.RequestAndDetailSplitter = New System.Windows.Forms.Splitter
        Me.NewJobButton = New System.Windows.Forms.Button
        Me.RequestDetails = New System.Windows.Forms.TextBox
        Me.RequestButtonPanel = New System.Windows.Forms.Panel
        Me.RequestMenuStatus = New System.Windows.Forms.MenuItem
        Me.RequestMenuRemove = New System.Windows.Forms.MenuItem
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.InputPanelPanel = New System.Windows.Forms.Panel
        Me.RequestMenuCurrentJob = New System.Windows.Forms.MenuItem
        Me.RequestOpen = New System.Windows.Forms.MenuItem
        Me.RequestMenuListJobs = New System.Windows.Forms.MenuItem
        Me.AddressContactSplitter = New System.Windows.Forms.Splitter
        Me.AddressPanel = New System.Windows.Forms.Panel
        Me.Address = New System.Windows.Forms.TextBox
        Me.AddressLabel = New activiser.Library.Forms.ImageLabel
        Me.DetailMainPanel = New System.Windows.Forms.Panel
        Me.ContactPanel = New System.Windows.Forms.Panel
        Me.Contact = New System.Windows.Forms.TextBox
        Me.ContactLabel = New activiser.Library.Forms.ImageLabel
        Me.GeneralTab = New System.Windows.Forms.TabPage
        Me.HeaderDetailSplitter = New System.Windows.Forms.Panel
        Me.DetailPanel2 = New System.Windows.Forms.Panel
        Me.PhonePanel = New System.Windows.Forms.Panel
        Me.Phone1 = New System.Windows.Forms.TextBox
        Me.PhoneLabel = New activiser.Library.Forms.ImageLabel
        Me.Phone2 = New System.Windows.Forms.TextBox
        Me.DetailPanel2b = New System.Windows.Forms.Panel
        Me.GetDetailsButton = New System.Windows.Forms.Button
        Me.MainMenuGetJobHistory = New System.Windows.Forms.MenuItem
        Me.MainMenu = New System.Windows.Forms.MenuItem
        Me.MainMenuDownload = New System.Windows.Forms.MenuItem
        Me.MainMenuGetClientDetails = New System.Windows.Forms.MenuItem
        Me.MainMenuGetOpenRequests = New System.Windows.Forms.MenuItem
        Me.MenuDownloadDetailWithHistory = New System.Windows.Forms.MenuItem
        Me.MainMenuSplitter3 = New System.Windows.Forms.MenuItem
        Me.MainMenuRequest = New System.Windows.Forms.MenuItem
        Me.MainMenuOpenRequest = New System.Windows.Forms.MenuItem
        Me.MainMenuNewRequest = New System.Windows.Forms.MenuItem
        Me.MenuRequestRemove = New System.Windows.Forms.MenuItem
        Me.MainMenuRemoveCompletedRequests = New System.Windows.Forms.MenuItem
        Me.MainMenuJob = New System.Windows.Forms.MenuItem
        Me.MainMenuOpenJob = New System.Windows.Forms.MenuItem
        Me.MainMenuNewJob = New System.Windows.Forms.MenuItem
        Me.MainMenuRemoveJob = New System.Windows.Forms.MenuItem
        Me.MainMenuClearSyncedJobs = New System.Windows.Forms.MenuItem
        Me.MainMenuSplitter2 = New System.Windows.Forms.MenuItem
        Me.MainMenuCall = New System.Windows.Forms.MenuItem
        Me.MainMenuCallPhone1 = New System.Windows.Forms.MenuItem
        Me.MainMenuCallPhone2 = New System.Windows.Forms.MenuItem
        Me.MainMenuSplitter1 = New System.Windows.Forms.MenuItem
        Me.MainMenuView = New System.Windows.Forms.MenuItem
        Me.ShowToolbars = New System.Windows.Forms.MenuItem
        Me.ViewRotate0 = New System.Windows.Forms.MenuItem
        Me.ViewRotate90 = New System.Windows.Forms.MenuItem
        Me.ViewRotate180 = New System.Windows.Forms.MenuItem
        Me.ViewRotate270 = New System.Windows.Forms.MenuItem
        Me.ViewFullScreen = New System.Windows.Forms.MenuItem
        Me.MainMenuSplitter0 = New System.Windows.Forms.MenuItem
        Me.MainMenuClose = New System.Windows.Forms.MenuItem
        Me.NextButton = New System.Windows.Forms.MenuItem
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.NotesTab = New System.Windows.Forms.TabPage
        Me.SiteNotes = New System.Windows.Forms.TextBox
        Me.NotesLabel = New System.Windows.Forms.Label
        Me.Email = New System.Windows.Forms.TextBox
        Me.EmailLabel = New System.Windows.Forms.Label
        Me.ClientNumberPanel = New System.Windows.Forms.Panel
        Me.ClientNumberBox = New System.Windows.Forms.TextBox
        Me.ClientSiteNumberLabel = New System.Windows.Forms.Label
        Me.RequestTab = New System.Windows.Forms.TabPage
        Me.RequestList = New System.Windows.Forms.ListView
        Me.RequestMenu = New System.Windows.Forms.ContextMenu
        Me.RequestMenuNewJob = New System.Windows.Forms.MenuItem
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.SiteNamePanel = New System.Windows.Forms.Panel
        Me.ClientListComboBox = New System.Windows.Forms.ComboBox
        Me.JobTab.SuspendLayout()
        Me.JobButtonPanel.SuspendLayout()
        Me.RequestButtonPanel.SuspendLayout()
        Me.AddressPanel.SuspendLayout()
        CType(Me.AddressLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DetailMainPanel.SuspendLayout()
        Me.ContactPanel.SuspendLayout()
        CType(Me.ContactLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GeneralTab.SuspendLayout()
        Me.DetailPanel2.SuspendLayout()
        Me.PhonePanel.SuspendLayout()
        CType(Me.PhoneLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DetailPanel2b.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.NotesTab.SuspendLayout()
        Me.ClientNumberPanel.SuspendLayout()
        Me.RequestTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'JobRequestNumberColumn
        '
        Me.JobRequestNumberColumn.Text = "Request"
        Me.JobRequestNumberColumn.Width = 60
        '
        'JobDateColumn
        '
        Me.JobDateColumn.Text = "Date"
        Me.JobDateColumn.Width = 75
        '
        'JobTab
        '
        Me.JobTab.Controls.Add(Me.JobList)
        Me.JobTab.Controls.Add(Me.JobAndDetailSplitter)
        Me.JobTab.Controls.Add(Me.JobDetailsTextBox)
        Me.JobTab.Controls.Add(Me.JobButtonPanel)
        Me.JobTab.Location = New System.Drawing.Point(0, 0)
        Me.JobTab.Name = "JobTab"
        Me.JobTab.Size = New System.Drawing.Size(240, 198)
        Me.JobTab.Text = "Jobs"
        '
        'JobList
        '
        Me.JobList.Columns.Add(Me.JobRequestNumberColumn)
        Me.JobList.Columns.Add(Me.JobDateColumn)
        Me.JobList.Columns.Add(Me.JobConsultantColumn)
        Me.JobList.ContextMenu = Me.JobMenu
        Me.JobList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JobList.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.JobList.FullRowSelect = True
        Me.JobList.Location = New System.Drawing.Point(0, 0)
        Me.JobList.Name = "JobList"
        Me.JobList.Size = New System.Drawing.Size(240, 123)
        Me.JobList.TabIndex = 3
        Me.JobList.View = System.Windows.Forms.View.Details
        '
        'JobConsultantColumn
        '
        Me.JobConsultantColumn.Text = "Consultant"
        Me.JobConsultantColumn.Width = 120
        '
        'JobMenu
        '
        Me.JobMenu.MenuItems.Add(Me.JobMenuOpen)
        Me.JobMenu.MenuItems.Add(Me.JobMenuRemove)
        '
        'JobMenuOpen
        '
        Me.JobMenuOpen.Text = "Open Job"
        '
        'JobMenuRemove
        '
        Me.JobMenuRemove.Text = "Remove Job"
        '
        'JobAndDetailSplitter
        '
        Me.JobAndDetailSplitter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.JobAndDetailSplitter.Location = New System.Drawing.Point(0, 123)
        Me.JobAndDetailSplitter.Name = "JobAndDetailSplitter"
        Me.JobAndDetailSplitter.Size = New System.Drawing.Size(240, 3)
        '
        'JobDetailsTextBox
        '
        Me.JobDetailsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.JobDetailsTextBox.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.JobDetailsTextBox.Location = New System.Drawing.Point(0, 126)
        Me.JobDetailsTextBox.Multiline = True
        Me.JobDetailsTextBox.Name = "JobDetailsTextBox"
        Me.JobDetailsTextBox.ReadOnly = True
        Me.JobDetailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.JobDetailsTextBox.Size = New System.Drawing.Size(240, 40)
        Me.JobDetailsTextBox.TabIndex = 2
        '
        'JobButtonPanel
        '
        Me.JobButtonPanel.Controls.Add(Me.OpenJobButton)
        Me.JobButtonPanel.Controls.Add(Me.GetHistoryButton)
        Me.JobButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.JobButtonPanel.Location = New System.Drawing.Point(0, 166)
        Me.JobButtonPanel.Name = "JobButtonPanel"
        Me.JobButtonPanel.Size = New System.Drawing.Size(240, 32)
        '
        'OpenJobButton
        '
        Me.OpenJobButton.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.OpenJobButton.Location = New System.Drawing.Point(156, 4)
        Me.OpenJobButton.Name = "OpenJobButton"
        Me.OpenJobButton.Size = New System.Drawing.Size(80, 24)
        Me.OpenJobButton.TabIndex = 1
        Me.OpenJobButton.Text = "Open Job"
        '
        'GetHistoryButton
        '
        Me.GetHistoryButton.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.GetHistoryButton.Location = New System.Drawing.Point(4, 4)
        Me.GetHistoryButton.Name = "GetHistoryButton"
        Me.GetHistoryButton.Size = New System.Drawing.Size(80, 24)
        Me.GetHistoryButton.TabIndex = 0
        Me.GetHistoryButton.Text = "Get History"
        '
        'OpenRequestButton
        '
        Me.OpenRequestButton.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.OpenRequestButton.Location = New System.Drawing.Point(4, 4)
        Me.OpenRequestButton.Name = "OpenRequestButton"
        Me.OpenRequestButton.Size = New System.Drawing.Size(72, 24)
        Me.OpenRequestButton.TabIndex = 1
        Me.OpenRequestButton.Text = "Open"
        '
        'RequestDateColumn
        '
        Me.RequestDateColumn.Text = "Date"
        Me.RequestDateColumn.Width = 75
        '
        'RequestDescriptionColumn
        '
        Me.RequestDescriptionColumn.Text = "Description"
        Me.RequestDescriptionColumn.Width = 260
        '
        'RequestNumberColumn
        '
        Me.RequestNumberColumn.Text = "Request"
        Me.RequestNumberColumn.Width = 55
        '
        'NewRequestButton
        '
        Me.NewRequestButton.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.NewRequestButton.Location = New System.Drawing.Point(164, 4)
        Me.NewRequestButton.Name = "NewRequestButton"
        Me.NewRequestButton.Size = New System.Drawing.Size(72, 24)
        Me.NewRequestButton.TabIndex = 2
        Me.NewRequestButton.Text = "New Request"
        '
        'RequestAndDetailSplitter
        '
        Me.RequestAndDetailSplitter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RequestAndDetailSplitter.Location = New System.Drawing.Point(0, 123)
        Me.RequestAndDetailSplitter.Name = "RequestAndDetailSplitter"
        Me.RequestAndDetailSplitter.Size = New System.Drawing.Size(240, 3)
        '
        'NewJobButton
        '
        Me.NewJobButton.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.NewJobButton.Location = New System.Drawing.Point(84, 4)
        Me.NewJobButton.Name = "NewJobButton"
        Me.NewJobButton.Size = New System.Drawing.Size(72, 24)
        Me.NewJobButton.TabIndex = 0
        Me.NewJobButton.Text = "New Job"
        '
        'RequestDetails
        '
        Me.RequestDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RequestDetails.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.RequestDetails.Location = New System.Drawing.Point(0, 126)
        Me.RequestDetails.Multiline = True
        Me.RequestDetails.Name = "RequestDetails"
        Me.RequestDetails.ReadOnly = True
        Me.RequestDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.RequestDetails.Size = New System.Drawing.Size(240, 40)
        Me.RequestDetails.TabIndex = 3
        '
        'RequestButtonPanel
        '
        Me.RequestButtonPanel.Controls.Add(Me.NewRequestButton)
        Me.RequestButtonPanel.Controls.Add(Me.NewJobButton)
        Me.RequestButtonPanel.Controls.Add(Me.OpenRequestButton)
        Me.RequestButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RequestButtonPanel.Location = New System.Drawing.Point(0, 166)
        Me.RequestButtonPanel.Name = "RequestButtonPanel"
        Me.RequestButtonPanel.Size = New System.Drawing.Size(240, 32)
        '
        'RequestMenuStatus
        '
        Me.RequestMenuStatus.Text = "Status"
        '
        'RequestMenuRemove
        '
        Me.RequestMenuRemove.Text = "Remove Request"
        '
        'InputPanel
        '
        '
        'InputPanelPanel
        '
        Me.InputPanelPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.InputPanelPanel.Location = New System.Drawing.Point(0, 243)
        Me.InputPanelPanel.Name = "InputPanelPanel"
        Me.InputPanelPanel.Size = New System.Drawing.Size(240, 25)
        Me.InputPanelPanel.Visible = False
        '
        'RequestMenuCurrentJob
        '
        Me.RequestMenuCurrentJob.Text = "Open Current Job..."
        '
        'RequestOpen
        '
        Me.RequestOpen.Text = "Open..."
        '
        'RequestMenuListJobs
        '
        Me.RequestMenuListJobs.Text = "List Jobs..."
        '
        'AddressContactSplitter
        '
        Me.AddressContactSplitter.Dock = System.Windows.Forms.DockStyle.Top
        Me.AddressContactSplitter.Location = New System.Drawing.Point(0, 78)
        Me.AddressContactSplitter.Name = "AddressContactSplitter"
        Me.AddressContactSplitter.Size = New System.Drawing.Size(240, 3)
        '
        'AddressPanel
        '
        Me.AddressPanel.Controls.Add(Me.Address)
        Me.AddressPanel.Controls.Add(Me.AddressLabel)
        Me.AddressPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.AddressPanel.Location = New System.Drawing.Point(0, 0)
        Me.AddressPanel.Name = "AddressPanel"
        Me.AddressPanel.Size = New System.Drawing.Size(240, 78)
        '
        'Address
        '
        Me.Address.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Address.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Address.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Address.Location = New System.Drawing.Point(55, 0)
        Me.Address.Multiline = True
        Me.Address.Name = "Address"
        Me.Address.ReadOnly = True
        Me.Address.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Address.Size = New System.Drawing.Size(185, 78)
        Me.Address.TabIndex = 0
        '
        'AddressLabel
        '
        Me.AddressLabel.AutoSize = True
        Me.AddressLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AddressLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.AddressLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.AddressLabel.Image = Nothing
        Me.AddressLabel.Location = New System.Drawing.Point(0, 0)
        Me.AddressLabel.Name = "AddressLabel"
        Me.AddressLabel.Size = New System.Drawing.Size(55, 78)
        Me.AddressLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.AddressLabel.TabIndex = 1
        Me.AddressLabel.Text = "Address:"
        Me.AddressLabel.TextAlignment = activiser.Library.Forms.ContentAlignment.MiddleLeft
        '
        'DetailMainPanel
        '
        Me.DetailMainPanel.Controls.Add(Me.ContactPanel)
        Me.DetailMainPanel.Controls.Add(Me.AddressContactSplitter)
        Me.DetailMainPanel.Controls.Add(Me.AddressPanel)
        Me.DetailMainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DetailMainPanel.Location = New System.Drawing.Point(0, 2)
        Me.DetailMainPanel.Name = "DetailMainPanel"
        Me.DetailMainPanel.Size = New System.Drawing.Size(240, 146)
        '
        'ContactPanel
        '
        Me.ContactPanel.Controls.Add(Me.Contact)
        Me.ContactPanel.Controls.Add(Me.ContactLabel)
        Me.ContactPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContactPanel.Location = New System.Drawing.Point(0, 81)
        Me.ContactPanel.Name = "ContactPanel"
        Me.ContactPanel.Size = New System.Drawing.Size(240, 65)
        '
        'Contact
        '
        Me.Contact.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Contact.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Contact.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Contact.Location = New System.Drawing.Point(54, 0)
        Me.Contact.Multiline = True
        Me.Contact.Name = "Contact"
        Me.Contact.ReadOnly = True
        Me.Contact.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Contact.Size = New System.Drawing.Size(186, 65)
        Me.Contact.TabIndex = 0
        '
        'ContactLabel
        '
        Me.ContactLabel.AutoSize = True
        Me.ContactLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ContactLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.ContactLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ContactLabel.Image = Nothing
        Me.ContactLabel.Location = New System.Drawing.Point(0, 0)
        Me.ContactLabel.Name = "ContactLabel"
        Me.ContactLabel.Size = New System.Drawing.Size(54, 65)
        Me.ContactLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.ContactLabel.TabIndex = 1
        Me.ContactLabel.Text = "Contact:"
        Me.ContactLabel.TextAlignment = activiser.Library.Forms.ContentAlignment.MiddleLeft
        '
        'GeneralTab
        '
        Me.GeneralTab.Controls.Add(Me.DetailMainPanel)
        Me.GeneralTab.Controls.Add(Me.HeaderDetailSplitter)
        Me.GeneralTab.Controls.Add(Me.DetailPanel2)
        Me.GeneralTab.Location = New System.Drawing.Point(0, 0)
        Me.GeneralTab.Name = "GeneralTab"
        Me.GeneralTab.Size = New System.Drawing.Size(240, 198)
        Me.GeneralTab.Text = "General"
        '
        'HeaderDetailSplitter
        '
        Me.HeaderDetailSplitter.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderDetailSplitter.Location = New System.Drawing.Point(0, 0)
        Me.HeaderDetailSplitter.Name = "HeaderDetailSplitter"
        Me.HeaderDetailSplitter.Size = New System.Drawing.Size(240, 2)
        '
        'DetailPanel2
        '
        Me.DetailPanel2.Controls.Add(Me.PhonePanel)
        Me.DetailPanel2.Controls.Add(Me.DetailPanel2b)
        Me.DetailPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DetailPanel2.Location = New System.Drawing.Point(0, 148)
        Me.DetailPanel2.Name = "DetailPanel2"
        Me.DetailPanel2.Size = New System.Drawing.Size(240, 50)
        '
        'PhonePanel
        '
        Me.PhonePanel.BackColor = System.Drawing.SystemColors.Info
        Me.PhonePanel.Controls.Add(Me.Phone1)
        Me.PhonePanel.Controls.Add(Me.PhoneLabel)
        Me.PhonePanel.Controls.Add(Me.Phone2)
        Me.PhonePanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PhonePanel.Location = New System.Drawing.Point(0, 1)
        Me.PhonePanel.Name = "PhonePanel"
        Me.PhonePanel.Size = New System.Drawing.Size(240, 22)
        '
        'Phone1
        '
        Me.Phone1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Phone1.Location = New System.Drawing.Point(46, 0)
        Me.Phone1.Name = "Phone1"
        Me.Phone1.ReadOnly = True
        Me.Phone1.Size = New System.Drawing.Size(106, 21)
        Me.Phone1.TabIndex = 0
        '
        'PhoneLabel
        '
        Me.PhoneLabel.AutoSize = True
        Me.PhoneLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PhoneLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.PhoneLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.PhoneLabel.Image = Nothing
        Me.PhoneLabel.Location = New System.Drawing.Point(0, 0)
        Me.PhoneLabel.Name = "PhoneLabel"
        Me.PhoneLabel.Size = New System.Drawing.Size(46, 22)
        Me.PhoneLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.PhoneLabel.TabIndex = 0
        Me.PhoneLabel.Text = "Phone:"
        Me.PhoneLabel.TextAlignment = activiser.Library.Forms.ContentAlignment.TopLeft
        '
        'Phone2
        '
        Me.Phone2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Phone2.Location = New System.Drawing.Point(152, 0)
        Me.Phone2.Name = "Phone2"
        Me.Phone2.ReadOnly = True
        Me.Phone2.Size = New System.Drawing.Size(88, 21)
        Me.Phone2.TabIndex = 1
        '
        'DetailPanel2b
        '
        Me.DetailPanel2b.Controls.Add(Me.GetDetailsButton)
        Me.DetailPanel2b.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DetailPanel2b.Location = New System.Drawing.Point(0, 23)
        Me.DetailPanel2b.Name = "DetailPanel2b"
        Me.DetailPanel2b.Size = New System.Drawing.Size(240, 27)
        Me.DetailPanel2b.Visible = False
        '
        'GetDetailsButton
        '
        Me.GetDetailsButton.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.GetDetailsButton.Location = New System.Drawing.Point(54, 2)
        Me.GetDetailsButton.Name = "GetDetailsButton"
        Me.GetDetailsButton.Size = New System.Drawing.Size(88, 20)
        Me.GetDetailsButton.TabIndex = 0
        Me.GetDetailsButton.Text = "Get Details..."
        '
        'MainMenuGetJobHistory
        '
        Me.MainMenuGetJobHistory.Text = "Job History..."
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.Add(Me.MainMenuDownload)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSplitter3)
        Me.MainMenu.MenuItems.Add(Me.MainMenuRequest)
        Me.MainMenu.MenuItems.Add(Me.MainMenuJob)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSplitter2)
        Me.MainMenu.MenuItems.Add(Me.MainMenuCall)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSplitter1)
        Me.MainMenu.MenuItems.Add(Me.MainMenuView)
        Me.MainMenu.MenuItems.Add(Me.ViewFullScreen)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSplitter0)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClose)
        Me.MainMenu.Text = "Menu"
        '
        'MainMenuDownload
        '
        Me.MainMenuDownload.MenuItems.Add(Me.MainMenuGetClientDetails)
        Me.MainMenuDownload.MenuItems.Add(Me.MainMenuGetJobHistory)
        Me.MainMenuDownload.MenuItems.Add(Me.MainMenuGetOpenRequests)
        Me.MainMenuDownload.MenuItems.Add(Me.MenuDownloadDetailWithHistory)
        Me.MainMenuDownload.Text = "Download"
        '
        'MainMenuGetClientDetails
        '
        Me.MainMenuGetClientDetails.Text = "Client Details..."
        '
        'MainMenuGetOpenRequests
        '
        Me.MainMenuGetOpenRequests.Text = "Open Requests..."
        '
        'MenuDownloadDetailWithHistory
        '
        Me.MenuDownloadDetailWithHistory.Text = "Client Details && History"
        '
        'MainMenuSplitter3
        '
        Me.MainMenuSplitter3.Text = "-"
        '
        'MainMenuRequest
        '
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuOpenRequest)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuNewRequest)
        Me.MainMenuRequest.MenuItems.Add(Me.MenuRequestRemove)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuRemoveCompletedRequests)
        Me.MainMenuRequest.Text = "Request"
        '
        'MainMenuOpenRequest
        '
        Me.MainMenuOpenRequest.Text = "Open Request..."
        '
        'MainMenuNewRequest
        '
        Me.MainMenuNewRequest.Text = "New Request..."
        '
        'MenuRequestRemove
        '
        Me.MenuRequestRemove.Text = "Remove Request"
        '
        'MainMenuRemoveCompletedRequests
        '
        Me.MainMenuRemoveCompletedRequests.Text = "Remove All Complete"
        '
        'MainMenuJob
        '
        Me.MainMenuJob.MenuItems.Add(Me.MainMenuOpenJob)
        Me.MainMenuJob.MenuItems.Add(Me.MainMenuNewJob)
        Me.MainMenuJob.MenuItems.Add(Me.MainMenuRemoveJob)
        Me.MainMenuJob.MenuItems.Add(Me.MainMenuClearSyncedJobs)
        Me.MainMenuJob.Text = "Job"
        '
        'MainMenuOpenJob
        '
        Me.MainMenuOpenJob.Text = "Open Job..."
        '
        'MainMenuNewJob
        '
        Me.MainMenuNewJob.Text = "New Job..."
        '
        'MainMenuRemoveJob
        '
        Me.MainMenuRemoveJob.Text = "Remove Job"
        '
        'MainMenuClearSyncedJobs
        '
        Me.MainMenuClearSyncedJobs.Text = "Clear Sync'd Jobs"
        '
        'MainMenuSplitter2
        '
        Me.MainMenuSplitter2.Text = "-"
        '
        'MainMenuCall
        '
        Me.MainMenuCall.MenuItems.Add(Me.MainMenuCallPhone1)
        Me.MainMenuCall.MenuItems.Add(Me.MainMenuCallPhone2)
        Me.MainMenuCall.Text = "Call"
        '
        'MainMenuCallPhone1
        '
        Me.MainMenuCallPhone1.Text = "Primary Number"
        '
        'MainMenuCallPhone2
        '
        Me.MainMenuCallPhone2.Text = "Secondary Number"
        '
        'MainMenuSplitter1
        '
        Me.MainMenuSplitter1.Text = "-"
        '
        'MainMenuView
        '
        Me.MainMenuView.MenuItems.Add(Me.ShowToolbars)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate0)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate90)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate180)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate270)
        Me.MainMenuView.Text = "View"
        '
        'ShowToolbars
        '
        Me.ShowToolbars.Checked = True
        Me.ShowToolbars.Text = "Show Tool Bars"
        '
        'ViewRotate0
        '
        Me.ViewRotate0.Text = "Rotate 0°"
        '
        'ViewRotate90
        '
        Me.ViewRotate90.Text = "Rotate 90°"
        '
        'ViewRotate180
        '
        Me.ViewRotate180.Text = "Rotate 180°"
        '
        'ViewRotate270
        '
        Me.ViewRotate270.Text = "Rotate 270°"
        '
        'ViewFullScreen
        '
        Me.ViewFullScreen.Text = "Full Screen"
        '
        'MainMenuSplitter0
        '
        Me.MainMenuSplitter0.Text = "-"
        '
        'MainMenuClose
        '
        Me.MainMenuClose.Text = "Close"
        '
        'NextButton
        '
        Me.NextButton.Text = "Next"
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.GeneralTab)
        Me.TabControl.Controls.Add(Me.NotesTab)
        Me.TabControl.Controls.Add(Me.RequestTab)
        Me.TabControl.Controls.Add(Me.JobTab)
        Me.TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl.Location = New System.Drawing.Point(0, 22)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(240, 221)
        Me.TabControl.TabIndex = 0
        '
        'NotesTab
        '
        Me.NotesTab.Controls.Add(Me.SiteNotes)
        Me.NotesTab.Controls.Add(Me.NotesLabel)
        Me.NotesTab.Controls.Add(Me.Email)
        Me.NotesTab.Controls.Add(Me.EmailLabel)
        Me.NotesTab.Controls.Add(Me.ClientNumberPanel)
        Me.NotesTab.Location = New System.Drawing.Point(0, 0)
        Me.NotesTab.Name = "NotesTab"
        Me.NotesTab.Size = New System.Drawing.Size(240, 198)
        Me.NotesTab.Text = "More"
        '
        'SiteNotes
        '
        Me.SiteNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SiteNotes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.SiteNotes.Location = New System.Drawing.Point(0, 90)
        Me.SiteNotes.Multiline = True
        Me.SiteNotes.Name = "SiteNotes"
        Me.SiteNotes.ReadOnly = True
        Me.SiteNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.SiteNotes.Size = New System.Drawing.Size(240, 108)
        Me.SiteNotes.TabIndex = 1
        '
        'NotesLabel
        '
        Me.NotesLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.NotesLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.NotesLabel.Location = New System.Drawing.Point(0, 69)
        Me.NotesLabel.Name = "NotesLabel"
        Me.NotesLabel.Size = New System.Drawing.Size(240, 21)
        Me.NotesLabel.Text = "Site Notes:"
        '
        'Email
        '
        Me.Email.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Email.Dock = System.Windows.Forms.DockStyle.Top
        Me.Email.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Email.Location = New System.Drawing.Point(0, 37)
        Me.Email.Multiline = True
        Me.Email.Name = "Email"
        Me.Email.ReadOnly = True
        Me.Email.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Email.Size = New System.Drawing.Size(240, 32)
        Me.Email.TabIndex = 2
        '
        'EmailLabel
        '
        Me.EmailLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.EmailLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.EmailLabel.Location = New System.Drawing.Point(0, 21)
        Me.EmailLabel.Name = "EmailLabel"
        Me.EmailLabel.Size = New System.Drawing.Size(240, 16)
        Me.EmailLabel.Text = "Email:"
        '
        'ClientNumberPanel
        '
        Me.ClientNumberPanel.Controls.Add(Me.ClientNumberBox)
        Me.ClientNumberPanel.Controls.Add(Me.ClientSiteNumberLabel)
        Me.ClientNumberPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.ClientNumberPanel.Location = New System.Drawing.Point(0, 0)
        Me.ClientNumberPanel.Name = "ClientNumberPanel"
        Me.ClientNumberPanel.Size = New System.Drawing.Size(240, 21)
        '
        'ClientNumberBox
        '
        Me.ClientNumberBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ClientNumberBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ClientNumberBox.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.ClientNumberBox.Location = New System.Drawing.Point(81, 0)
        Me.ClientNumberBox.Name = "ClientNumberBox"
        Me.ClientNumberBox.ReadOnly = True
        Me.ClientNumberBox.Size = New System.Drawing.Size(159, 19)
        Me.ClientNumberBox.TabIndex = 5
        '
        'ClientSiteNumberLabel
        '
        Me.ClientSiteNumberLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClientSiteNumberLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ClientSiteNumberLabel.Location = New System.Drawing.Point(0, 0)
        Me.ClientSiteNumberLabel.Name = "ClientSiteNumberLabel"
        Me.ClientSiteNumberLabel.Size = New System.Drawing.Size(81, 21)
        Me.ClientSiteNumberLabel.Text = "Client #:"
        '
        'RequestTab
        '
        Me.RequestTab.Controls.Add(Me.RequestList)
        Me.RequestTab.Controls.Add(Me.RequestAndDetailSplitter)
        Me.RequestTab.Controls.Add(Me.RequestDetails)
        Me.RequestTab.Controls.Add(Me.RequestButtonPanel)
        Me.RequestTab.Location = New System.Drawing.Point(0, 0)
        Me.RequestTab.Name = "RequestTab"
        Me.RequestTab.Size = New System.Drawing.Size(240, 198)
        Me.RequestTab.Text = "Requests"
        '
        'RequestList
        '
        Me.RequestList.Columns.Add(Me.RequestNumberColumn)
        Me.RequestList.Columns.Add(Me.RequestDateColumn)
        Me.RequestList.Columns.Add(Me.RequestDescriptionColumn)
        Me.RequestList.ContextMenu = Me.RequestMenu
        Me.RequestList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RequestList.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.RequestList.FullRowSelect = True
        Me.RequestList.Location = New System.Drawing.Point(0, 0)
        Me.RequestList.Name = "RequestList"
        Me.RequestList.Size = New System.Drawing.Size(240, 123)
        Me.RequestList.TabIndex = 4
        Me.RequestList.View = System.Windows.Forms.View.Details
        '
        'RequestMenu
        '
        Me.RequestMenu.MenuItems.Add(Me.RequestMenuNewJob)
        Me.RequestMenu.MenuItems.Add(Me.RequestOpen)
        Me.RequestMenu.MenuItems.Add(Me.RequestMenuCurrentJob)
        Me.RequestMenu.MenuItems.Add(Me.RequestMenuListJobs)
        Me.RequestMenu.MenuItems.Add(Me.RequestMenuRemove)
        Me.RequestMenu.MenuItems.Add(Me.RequestMenuStatus)
        '
        'RequestMenuNewJob
        '
        Me.RequestMenuNewJob.Text = "New Job..."
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.NextButton)
        Me.MenuStrip.MenuItems.Add(Me.MainMenu)
        '
        'SiteNamePanel
        '
        Me.SiteNamePanel.Location = New System.Drawing.Point(0, 0)
        Me.SiteNamePanel.Name = "SiteNamePanel"
        Me.SiteNamePanel.Size = New System.Drawing.Size(100, 100)
        '
        'ClientListComboBox
        '
        Me.ClientListComboBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.ClientListComboBox.ForeColor = System.Drawing.SystemColors.Highlight
        Me.ClientListComboBox.Location = New System.Drawing.Point(0, 0)
        Me.ClientListComboBox.Name = "ClientListComboBox"
        Me.ClientListComboBox.Size = New System.Drawing.Size(240, 22)
        Me.ClientListComboBox.TabIndex = 1
        '
        'ClientForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.ClientListComboBox)
        Me.Controls.Add(Me.InputPanelPanel)
        Me.KeyPreview = True
        Me.Menu = Me.MenuStrip
        Me.MinimizeBox = False
        Me.Name = "ClientForm"
        Me.Text = "activiser™"
        Me.JobTab.ResumeLayout(False)
        Me.JobButtonPanel.ResumeLayout(False)
        Me.RequestButtonPanel.ResumeLayout(False)
        Me.AddressPanel.ResumeLayout(False)
        CType(Me.AddressLabel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DetailMainPanel.ResumeLayout(False)
        Me.ContactPanel.ResumeLayout(False)
        CType(Me.ContactLabel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GeneralTab.ResumeLayout(False)
        Me.DetailPanel2.ResumeLayout(False)
        Me.PhonePanel.ResumeLayout(False)
        CType(Me.PhoneLabel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DetailPanel2b.ResumeLayout(False)
        Me.TabControl.ResumeLayout(False)
        Me.NotesTab.ResumeLayout(False)
        Me.ClientNumberPanel.ResumeLayout(False)
        Me.RequestTab.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents JobRequestNumberColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobDateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobTab As System.Windows.Forms.TabPage
    Friend WithEvents JobList As System.Windows.Forms.ListView
    Friend WithEvents JobConsultantColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents JobMenuOpen As System.Windows.Forms.MenuItem
    Friend WithEvents JobMenuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents JobAndDetailSplitter As System.Windows.Forms.Splitter
    Friend WithEvents JobDetailsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JobButtonPanel As System.Windows.Forms.Panel
    Friend WithEvents OpenJobButton As System.Windows.Forms.Button
    Friend WithEvents GetHistoryButton As System.Windows.Forms.Button
    Friend WithEvents OpenRequestButton As System.Windows.Forms.Button
    Friend WithEvents RequestDateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents RequestDescriptionColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents RequestNumberColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents NewRequestButton As System.Windows.Forms.Button
    Friend WithEvents RequestAndDetailSplitter As System.Windows.Forms.Splitter
    Friend WithEvents NewJobButton As System.Windows.Forms.Button
    Friend WithEvents RequestDetails As System.Windows.Forms.TextBox
    Friend WithEvents RequestButtonPanel As System.Windows.Forms.Panel
    Friend WithEvents RequestMenuStatus As System.Windows.Forms.MenuItem
    Friend WithEvents RequestMenuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents InputPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents RequestMenuCurrentJob As System.Windows.Forms.MenuItem
    Friend WithEvents RequestOpen As System.Windows.Forms.MenuItem
    Friend WithEvents RequestMenuListJobs As System.Windows.Forms.MenuItem
    Friend WithEvents AddressContactSplitter As System.Windows.Forms.Splitter
    Friend WithEvents AddressPanel As System.Windows.Forms.Panel
    Friend WithEvents Address As System.Windows.Forms.TextBox
    Friend WithEvents AddressLabel As activiser.Library.Forms.ImageLabel
    Friend WithEvents DetailMainPanel As System.Windows.Forms.Panel
    Friend WithEvents ContactPanel As System.Windows.Forms.Panel
    Friend WithEvents Contact As System.Windows.Forms.TextBox
    Friend WithEvents ContactLabel As activiser.Library.Forms.ImageLabel
    Friend WithEvents GeneralTab As System.Windows.Forms.TabPage
    Friend WithEvents DetailPanel2 As System.Windows.Forms.Panel
    Friend WithEvents PhonePanel As System.Windows.Forms.Panel
    Friend WithEvents PhoneLabel As activiser.Library.Forms.ImageLabel
    Friend WithEvents Phone2 As System.Windows.Forms.TextBox
    Friend WithEvents Phone1 As System.Windows.Forms.TextBox
    Friend WithEvents DetailPanel2b As System.Windows.Forms.Panel
    Friend WithEvents GetDetailsButton As System.Windows.Forms.Button
    Friend WithEvents MainMenuGetJobHistory As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuNewRequest As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClose As System.Windows.Forms.MenuItem
    Friend WithEvents NextButton As System.Windows.Forms.MenuItem
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents NotesTab As System.Windows.Forms.TabPage
    Friend WithEvents SiteNotes As System.Windows.Forms.TextBox
    Friend WithEvents NotesLabel As System.Windows.Forms.Label
    Friend WithEvents Email As System.Windows.Forms.TextBox
    Friend WithEvents EmailLabel As System.Windows.Forms.Label
    Friend WithEvents RequestTab As System.Windows.Forms.TabPage
    Friend WithEvents RequestList As System.Windows.Forms.ListView
    Friend WithEvents RequestMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuStrip As System.Windows.Forms.MainMenu
    Friend WithEvents MainMenuOpenRequest As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuNewJob As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuOpenJob As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuGetClientDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuRemoveJob As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSplitter3 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuDownload As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuJob As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuRequest As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate0 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate90 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate180 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewRotate270 As System.Windows.Forms.MenuItem
    Friend WithEvents ViewFullScreen As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSplitter2 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSplitter0 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuRemoveCompletedRequests As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuClearSyncedJobs As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuGetOpenRequests As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuView As System.Windows.Forms.MenuItem
    Friend WithEvents ShowToolbars As System.Windows.Forms.MenuItem
    Friend WithEvents SiteNamePanel As System.Windows.Forms.Panel
    Friend WithEvents ClientListComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents RequestMenuNewJob As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuCall As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuCallPhone1 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuCallPhone2 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenuSplitter1 As System.Windows.Forms.MenuItem
    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
    Friend WithEvents HeaderDetailSplitter As System.Windows.Forms.Panel
    Friend WithEvents ClientNumberPanel As System.Windows.Forms.Panel
    Friend WithEvents ClientNumberBox As System.Windows.Forms.TextBox
    Friend WithEvents ClientSiteNumberLabel As System.Windows.Forms.Label
    Friend WithEvents MenuDownloadDetailWithHistory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuRequestRemove As System.Windows.Forms.MenuItem
End Class
