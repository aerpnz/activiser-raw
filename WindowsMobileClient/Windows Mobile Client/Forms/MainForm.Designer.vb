<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class MainForm
    Inherits Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.ViewStatusComplete = New System.Windows.Forms.MenuItem
        Me.ViewStatusAll = New System.Windows.Forms.MenuItem
        Me.MainMenuSync = New System.Windows.Forms.MenuItem
        Me.MainMenuSeparator = New System.Windows.Forms.MenuItem
        Me.MainMenuExit = New System.Windows.Forms.MenuItem
        Me.MainMenuClientList = New System.Windows.Forms.MenuItem
        Me.OpenMenu = New System.Windows.Forms.MenuItem
        Me.OpenMenuNewJob = New System.Windows.Forms.MenuItem
        Me.OpenMenuClientInfo = New System.Windows.Forms.MenuItem
        Me.OpenMenuJobList = New System.Windows.Forms.MenuItem
        Me.OpenMenuRequest = New System.Windows.Forms.MenuItem
        Me.MainMenuNewRequest = New System.Windows.Forms.MenuItem
        Me.ViewRotate180 = New System.Windows.Forms.MenuItem
        Me.ViewShowCheckBoxes = New System.Windows.Forms.MenuItem
        Me.ViewStatusSpecific = New System.Windows.Forms.MenuItem
        Me.ViewToolBar = New System.Windows.Forms.MenuItem
        Me.ViewMenuToolbarShowCaptions = New System.Windows.Forms.MenuItem
        Me.ViewMenuToolbarOff = New System.Windows.Forms.MenuItem
        Me.ViewMenuToolbarSmaller = New System.Windows.Forms.MenuItem
        Me.ViewMenuToolbarSmall = New System.Windows.Forms.MenuItem
        Me.ViewMenuToolbarMedium = New System.Windows.Forms.MenuItem
        Me.ViewMenuToolbarLarge = New System.Windows.Forms.MenuItem
        Me.ViewMenuToolbarLarger = New System.Windows.Forms.MenuItem
        Me.ViewRotate90 = New System.Windows.Forms.MenuItem
        Me.ViewRotate0 = New System.Windows.Forms.MenuItem
        Me.ViewRotate270 = New System.Windows.Forms.MenuItem
        Me.RequestList = New System.Windows.Forms.ListView
        Me.RequestListMenu = New System.Windows.Forms.ContextMenu
        Me.RequestListMenuNewJob = New System.Windows.Forms.MenuItem
        Me.RequestListMenuOpen = New System.Windows.Forms.MenuItem
        Me.RequestListMenuListJobs = New System.Windows.Forms.MenuItem
        Me.RequestListMenuOpenCurrentJob = New System.Windows.Forms.MenuItem
        Me.RequestListMenuClientInfo = New System.Windows.Forms.MenuItem
        Me.RequestListMenuChangeStatus = New System.Windows.Forms.MenuItem
        Me.RequestListMenuSeparator = New System.Windows.Forms.MenuItem
        Me.RequestListMenuRemoveRequest = New System.Windows.Forms.MenuItem
        Me.RequestListMenuSelectAll = New System.Windows.Forms.MenuItem
        Me.RequestListMenuSelectNone = New System.Windows.Forms.MenuItem
        Me.RequestListDetailSplitter = New System.Windows.Forms.Splitter
        Me.SyncInfoHighlight = New System.Windows.Forms.Panel
        Me.Toolbar = New System.Windows.Forms.Panel
        Me.ClientListButton = New activiser.Library.Forms.ImageButton
        Me.NewJobButton = New activiser.Library.Forms.ImageButton
        Me.OpenRequestButton = New activiser.Library.Forms.ImageButton
        Me.ViewPersonAll = New System.Windows.Forms.MenuItem
        Me.LastSyncPanel = New System.Windows.Forms.Panel
        Me.SyncButton = New activiser.Library.Forms.ImageButton
        Me.MainMenuFilterStatus = New System.Windows.Forms.MenuItem
        Me.ViewStatusOpen = New System.Windows.Forms.MenuItem
        Me.ViewMenuDivider1 = New System.Windows.Forms.MenuItem
        Me.ViewStatusNew = New System.Windows.Forms.MenuItem
        Me.ViewStatusIP = New System.Windows.Forms.MenuItem
        Me.ViewStatusCancelled = New System.Windows.Forms.MenuItem
        Me.ViewMenuDivider2 = New System.Windows.Forms.MenuItem
        Me.ViewStatusIncomplete = New System.Windows.Forms.MenuItem
        Me.ViewMenuDivider3 = New System.Windows.Forms.MenuItem
        Me.ViewPersonOthers = New System.Windows.Forms.MenuItem
        Me.ViewPersonMe = New System.Windows.Forms.MenuItem
        Me.MainMenuView = New System.Windows.Forms.MenuItem
        Me.ViewShowRequestedStatus = New System.Windows.Forms.MenuItem
        Me.ViewShowColumnHeaders = New System.Windows.Forms.MenuItem
        Me.ViewShowSyncInfo = New System.Windows.Forms.MenuItem
        Me.ViewShowTimeInDateColumn = New System.Windows.Forms.MenuItem
        Me.RefreshButton = New System.Windows.Forms.MenuItem
        Me.ViewFullScreen = New System.Windows.Forms.MenuItem
        Me.MainMenuSettings = New System.Windows.Forms.MenuItem
        Me.MainMenuAbout = New System.Windows.Forms.MenuItem
        Me.MainMenu = New System.Windows.Forms.MenuItem
        Me.MainMenuRequest = New System.Windows.Forms.MenuItem
        Me.MainMenuRemoveRequest = New System.Windows.Forms.MenuItem
        Me.MainMenuChangeStatus = New System.Windows.Forms.MenuItem
        Me.MenuEditSelectAll = New System.Windows.Forms.MenuItem
        Me.MenuEditSelectNone = New System.Windows.Forms.MenuItem
        Me.MainMenuSeparator2 = New System.Windows.Forms.MenuItem
        Me.MainMenuGPS = New System.Windows.Forms.MenuItem
        Me.MainMenuGpsInfo = New System.Windows.Forms.MenuItem
        Me.MainMenuGpsOn = New System.Windows.Forms.MenuItem
        Me.MainMenuGpsOff = New System.Windows.Forms.MenuItem
        Me.MainMenuFilterOwner = New System.Windows.Forms.MenuItem
        Me.MenuStrip = New System.Windows.Forms.MainMenu
        Me.backgroundLoaderTimer = New System.Windows.Forms.Timer
        Me.GpsLabel = New System.Windows.Forms.Label
        Me.RequestDetails = New System.Windows.Forms.TextBox
        Me.ReadOnlyContextMenu1 = New activiser.ReadOnlyContextMenu
        Me.Toolbar.SuspendLayout()
        CType(Me.ClientListButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NewJobButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OpenRequestButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LastSyncPanel.SuspendLayout()
        CType(Me.SyncButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ViewStatusComplete
        '
        resources.ApplyResources(Me.ViewStatusComplete, "ViewStatusComplete")
        '
        'ViewStatusAll
        '
        resources.ApplyResources(Me.ViewStatusAll, "ViewStatusAll")
        '
        'MainMenuSync
        '
        resources.ApplyResources(Me.MainMenuSync, "MainMenuSync")
        '
        'MainMenuSeparator
        '
        resources.ApplyResources(Me.MainMenuSeparator, "MainMenuSeparator")
        '
        'MainMenuExit
        '
        resources.ApplyResources(Me.MainMenuExit, "MainMenuExit")
        '
        'MainMenuClientList
        '
        resources.ApplyResources(Me.MainMenuClientList, "MainMenuClientList")
        '
        'OpenMenu
        '
        Me.OpenMenu.MenuItems.Add(Me.OpenMenuNewJob)
        Me.OpenMenu.MenuItems.Add(Me.OpenMenuClientInfo)
        Me.OpenMenu.MenuItems.Add(Me.OpenMenuJobList)
        Me.OpenMenu.MenuItems.Add(Me.OpenMenuRequest)
        resources.ApplyResources(Me.OpenMenu, "OpenMenu")
        '
        'OpenMenuNewJob
        '
        resources.ApplyResources(Me.OpenMenuNewJob, "OpenMenuNewJob")
        '
        'OpenMenuClientInfo
        '
        resources.ApplyResources(Me.OpenMenuClientInfo, "OpenMenuClientInfo")
        '
        'OpenMenuJobList
        '
        resources.ApplyResources(Me.OpenMenuJobList, "OpenMenuJobList")
        '
        'OpenMenuRequest
        '
        resources.ApplyResources(Me.OpenMenuRequest, "OpenMenuRequest")
        '
        'MainMenuNewRequest
        '
        resources.ApplyResources(Me.MainMenuNewRequest, "MainMenuNewRequest")
        '
        'ViewRotate180
        '
        resources.ApplyResources(Me.ViewRotate180, "ViewRotate180")
        '
        'ViewShowCheckBoxes
        '
        Me.ViewShowCheckBoxes.Checked = True
        resources.ApplyResources(Me.ViewShowCheckBoxes, "ViewShowCheckBoxes")
        '
        'ViewStatusSpecific
        '
        resources.ApplyResources(Me.ViewStatusSpecific, "ViewStatusSpecific")
        '
        'ViewToolBar
        '
        Me.ViewToolBar.MenuItems.Add(Me.ViewMenuToolbarShowCaptions)
        Me.ViewToolBar.MenuItems.Add(Me.ViewMenuToolbarOff)
        Me.ViewToolBar.MenuItems.Add(Me.ViewMenuToolbarSmaller)
        Me.ViewToolBar.MenuItems.Add(Me.ViewMenuToolbarSmall)
        Me.ViewToolBar.MenuItems.Add(Me.ViewMenuToolbarMedium)
        Me.ViewToolBar.MenuItems.Add(Me.ViewMenuToolbarLarge)
        Me.ViewToolBar.MenuItems.Add(Me.ViewMenuToolbarLarger)
        resources.ApplyResources(Me.ViewToolBar, "ViewToolBar")
        '
        'ViewMenuToolbarShowCaptions
        '
        resources.ApplyResources(Me.ViewMenuToolbarShowCaptions, "ViewMenuToolbarShowCaptions")
        '
        'ViewMenuToolbarOff
        '
        resources.ApplyResources(Me.ViewMenuToolbarOff, "ViewMenuToolbarOff")
        '
        'ViewMenuToolbarSmaller
        '
        resources.ApplyResources(Me.ViewMenuToolbarSmaller, "ViewMenuToolbarSmaller")
        '
        'ViewMenuToolbarSmall
        '
        resources.ApplyResources(Me.ViewMenuToolbarSmall, "ViewMenuToolbarSmall")
        '
        'ViewMenuToolbarMedium
        '
        resources.ApplyResources(Me.ViewMenuToolbarMedium, "ViewMenuToolbarMedium")
        '
        'ViewMenuToolbarLarge
        '
        resources.ApplyResources(Me.ViewMenuToolbarLarge, "ViewMenuToolbarLarge")
        '
        'ViewMenuToolbarLarger
        '
        resources.ApplyResources(Me.ViewMenuToolbarLarger, "ViewMenuToolbarLarger")
        '
        'ViewRotate90
        '
        resources.ApplyResources(Me.ViewRotate90, "ViewRotate90")
        '
        'ViewRotate0
        '
        resources.ApplyResources(Me.ViewRotate0, "ViewRotate0")
        '
        'ViewRotate270
        '
        resources.ApplyResources(Me.ViewRotate270, "ViewRotate270")
        '
        'RequestList
        '
        Me.RequestList.CheckBoxes = True
        Me.RequestList.ContextMenu = Me.RequestListMenu
        resources.ApplyResources(Me.RequestList, "RequestList")
        Me.RequestList.FullRowSelect = True
        Me.RequestList.Name = "RequestList"
        Me.RequestList.View = System.Windows.Forms.View.Details
        '
        'RequestListMenu
        '
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuNewJob)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuOpen)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuListJobs)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuOpenCurrentJob)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuClientInfo)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuChangeStatus)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuSeparator)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuRemoveRequest)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuSelectAll)
        Me.RequestListMenu.MenuItems.Add(Me.RequestListMenuSelectNone)
        '
        'RequestListMenuNewJob
        '
        resources.ApplyResources(Me.RequestListMenuNewJob, "RequestListMenuNewJob")
        '
        'RequestListMenuOpen
        '
        resources.ApplyResources(Me.RequestListMenuOpen, "RequestListMenuOpen")
        '
        'RequestListMenuListJobs
        '
        resources.ApplyResources(Me.RequestListMenuListJobs, "RequestListMenuListJobs")
        '
        'RequestListMenuOpenCurrentJob
        '
        resources.ApplyResources(Me.RequestListMenuOpenCurrentJob, "RequestListMenuOpenCurrentJob")
        '
        'RequestListMenuClientInfo
        '
        resources.ApplyResources(Me.RequestListMenuClientInfo, "RequestListMenuClientInfo")
        '
        'RequestListMenuChangeStatus
        '
        resources.ApplyResources(Me.RequestListMenuChangeStatus, "RequestListMenuChangeStatus")
        '
        'RequestListMenuSeparator
        '
        resources.ApplyResources(Me.RequestListMenuSeparator, "RequestListMenuSeparator")
        '
        'RequestListMenuRemoveRequest
        '
        resources.ApplyResources(Me.RequestListMenuRemoveRequest, "RequestListMenuRemoveRequest")
        '
        'RequestListMenuSelectAll
        '
        resources.ApplyResources(Me.RequestListMenuSelectAll, "RequestListMenuSelectAll")
        '
        'RequestListMenuSelectNone
        '
        resources.ApplyResources(Me.RequestListMenuSelectNone, "RequestListMenuSelectNone")
        '
        'RequestListDetailSplitter
        '
        Me.RequestListDetailSplitter.BackColor = System.Drawing.SystemColors.ActiveCaption
        resources.ApplyResources(Me.RequestListDetailSplitter, "RequestListDetailSplitter")
        Me.RequestListDetailSplitter.Name = "RequestListDetailSplitter"
        '
        'SyncInfoHighlight
        '
        resources.ApplyResources(Me.SyncInfoHighlight, "SyncInfoHighlight")
        Me.SyncInfoHighlight.Name = "SyncInfoHighlight"
        '
        'Toolbar
        '
        resources.ApplyResources(Me.Toolbar, "Toolbar")
        Me.Toolbar.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Toolbar.Controls.Add(Me.ClientListButton)
        Me.Toolbar.Controls.Add(Me.NewJobButton)
        Me.Toolbar.Controls.Add(Me.OpenRequestButton)
        Me.Toolbar.Name = "Toolbar"
        '
        'ClientListButton
        '
        Me.ClientListButton.AutoSize = True
        Me.ClientListButton.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientListButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ClientListButton.ClickMaskColor = System.Drawing.SystemColors.ScrollBar
        resources.ApplyResources(Me.ClientListButton, "ClientListButton")
        Me.ClientListButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientListButton.Image = Nothing
        Me.ClientListButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.Top
        Me.ClientListButton.InvertOnClick = False
        Me.ClientListButton.Name = "ClientListButton"
        Me.ClientListButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.ClientListButton.TextVisible = True
        Me.ClientListButton.TransparentBackground = True
        '
        'NewJobButton
        '
        Me.NewJobButton.AutoSize = True
        Me.NewJobButton.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.NewJobButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NewJobButton.ClickMaskColor = System.Drawing.SystemColors.ScrollBar
        resources.ApplyResources(Me.NewJobButton, "NewJobButton")
        Me.NewJobButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewJobButton.Image = Nothing
        Me.NewJobButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.Top
        Me.NewJobButton.InvertOnClick = False
        Me.NewJobButton.Name = "NewJobButton"
        Me.NewJobButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.NewJobButton.TextVisible = True
        Me.NewJobButton.TransparentBackground = True
        '
        'OpenRequestButton
        '
        Me.OpenRequestButton.AutoSize = True
        Me.OpenRequestButton.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.OpenRequestButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.OpenRequestButton.ClickMaskColor = System.Drawing.SystemColors.ScrollBar
        resources.ApplyResources(Me.OpenRequestButton, "OpenRequestButton")
        Me.OpenRequestButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.OpenRequestButton.Image = Nothing
        Me.OpenRequestButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.Top
        Me.OpenRequestButton.InvertOnClick = False
        Me.OpenRequestButton.Name = "OpenRequestButton"
        Me.OpenRequestButton.TextAlignment = activiser.Library.Forms.ContentAlignment.BottomCenter
        Me.OpenRequestButton.TextVisible = True
        Me.OpenRequestButton.TransparentBackground = True
        '
        'ViewPersonAll
        '
        Me.ViewPersonAll.Checked = True
        resources.ApplyResources(Me.ViewPersonAll, "ViewPersonAll")
        '
        'LastSyncPanel
        '
        Me.LastSyncPanel.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.LastSyncPanel.Controls.Add(Me.SyncButton)
        resources.ApplyResources(Me.LastSyncPanel, "LastSyncPanel")
        Me.LastSyncPanel.Name = "LastSyncPanel"
        '
        'SyncButton
        '
        resources.ApplyResources(Me.SyncButton, "SyncButton")
        Me.SyncButton.AutoSize = False
        Me.SyncButton.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SyncButton.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SyncButton.ClickMaskColor = System.Drawing.SystemColors.Control
        Me.SyncButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SyncButton.Image = CType(resources.GetObject("SyncButton.Image"), System.Drawing.Image)
        Me.SyncButton.ImageAlignment = activiser.Library.Forms.ContentAlignment.MiddleRight
        Me.SyncButton.InvertOnClick = False
        Me.SyncButton.Name = "SyncButton"
        Me.SyncButton.TextAlignment = activiser.Library.Forms.ContentAlignment.MiddleRight
        Me.SyncButton.TextVisible = True
        Me.SyncButton.TransparentBackground = True
        '
        'MainMenuFilterStatus
        '
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusAll)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusOpen)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewMenuDivider1)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusNew)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusIP)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusComplete)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusCancelled)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewMenuDivider2)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusIncomplete)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewMenuDivider3)
        Me.MainMenuFilterStatus.MenuItems.Add(Me.ViewStatusSpecific)
        resources.ApplyResources(Me.MainMenuFilterStatus, "MainMenuFilterStatus")
        '
        'ViewStatusOpen
        '
        resources.ApplyResources(Me.ViewStatusOpen, "ViewStatusOpen")
        '
        'ViewMenuDivider1
        '
        resources.ApplyResources(Me.ViewMenuDivider1, "ViewMenuDivider1")
        '
        'ViewStatusNew
        '
        resources.ApplyResources(Me.ViewStatusNew, "ViewStatusNew")
        '
        'ViewStatusIP
        '
        resources.ApplyResources(Me.ViewStatusIP, "ViewStatusIP")
        '
        'ViewStatusCancelled
        '
        resources.ApplyResources(Me.ViewStatusCancelled, "ViewStatusCancelled")
        '
        'ViewMenuDivider2
        '
        resources.ApplyResources(Me.ViewMenuDivider2, "ViewMenuDivider2")
        '
        'ViewStatusIncomplete
        '
        resources.ApplyResources(Me.ViewStatusIncomplete, "ViewStatusIncomplete")
        '
        'ViewMenuDivider3
        '
        resources.ApplyResources(Me.ViewMenuDivider3, "ViewMenuDivider3")
        '
        'ViewPersonOthers
        '
        resources.ApplyResources(Me.ViewPersonOthers, "ViewPersonOthers")
        '
        'ViewPersonMe
        '
        resources.ApplyResources(Me.ViewPersonMe, "ViewPersonMe")
        '
        'MainMenuView
        '
        Me.MainMenuView.MenuItems.Add(Me.ViewShowRequestedStatus)
        Me.MainMenuView.MenuItems.Add(Me.ViewShowCheckBoxes)
        Me.MainMenuView.MenuItems.Add(Me.ViewShowColumnHeaders)
        Me.MainMenuView.MenuItems.Add(Me.ViewShowSyncInfo)
        Me.MainMenuView.MenuItems.Add(Me.ViewShowTimeInDateColumn)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate0)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate90)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate180)
        Me.MainMenuView.MenuItems.Add(Me.ViewRotate270)
        Me.MainMenuView.MenuItems.Add(Me.RefreshButton)
        resources.ApplyResources(Me.MainMenuView, "MainMenuView")
        '
        'ViewShowRequestedStatus
        '
        Me.ViewShowRequestedStatus.Checked = True
        resources.ApplyResources(Me.ViewShowRequestedStatus, "ViewShowRequestedStatus")
        '
        'ViewShowColumnHeaders
        '
        Me.ViewShowColumnHeaders.Checked = True
        resources.ApplyResources(Me.ViewShowColumnHeaders, "ViewShowColumnHeaders")
        '
        'ViewShowSyncInfo
        '
        Me.ViewShowSyncInfo.Checked = True
        resources.ApplyResources(Me.ViewShowSyncInfo, "ViewShowSyncInfo")
        '
        'ViewShowTimeInDateColumn
        '
        resources.ApplyResources(Me.ViewShowTimeInDateColumn, "ViewShowTimeInDateColumn")
        '
        'RefreshButton
        '
        resources.ApplyResources(Me.RefreshButton, "RefreshButton")
        '
        'ViewFullScreen
        '
        resources.ApplyResources(Me.ViewFullScreen, "ViewFullScreen")
        '
        'MainMenuSettings
        '
        resources.ApplyResources(Me.MainMenuSettings, "MainMenuSettings")
        '
        'MainMenuAbout
        '
        resources.ApplyResources(Me.MainMenuAbout, "MainMenuAbout")
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.Add(Me.MainMenuSync)
        Me.MainMenu.MenuItems.Add(Me.MainMenuRequest)
        Me.MainMenu.MenuItems.Add(Me.MainMenuClientList)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSeparator2)
        Me.MainMenu.MenuItems.Add(Me.MainMenuGPS)
        Me.MainMenu.MenuItems.Add(Me.MainMenuFilterStatus)
        Me.MainMenu.MenuItems.Add(Me.MainMenuFilterOwner)
        Me.MainMenu.MenuItems.Add(Me.ViewToolBar)
        Me.MainMenu.MenuItems.Add(Me.MainMenuView)
        Me.MainMenu.MenuItems.Add(Me.ViewFullScreen)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSettings)
        Me.MainMenu.MenuItems.Add(Me.MainMenuSeparator)
        Me.MainMenu.MenuItems.Add(Me.MainMenuAbout)
        Me.MainMenu.MenuItems.Add(Me.MainMenuExit)
        resources.ApplyResources(Me.MainMenu, "MainMenu")
        '
        'MainMenuRequest
        '
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuNewRequest)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuRemoveRequest)
        Me.MainMenuRequest.MenuItems.Add(Me.MainMenuChangeStatus)
        Me.MainMenuRequest.MenuItems.Add(Me.MenuEditSelectAll)
        Me.MainMenuRequest.MenuItems.Add(Me.MenuEditSelectNone)
        resources.ApplyResources(Me.MainMenuRequest, "MainMenuRequest")
        '
        'MainMenuRemoveRequest
        '
        resources.ApplyResources(Me.MainMenuRemoveRequest, "MainMenuRemoveRequest")
        '
        'MainMenuChangeStatus
        '
        resources.ApplyResources(Me.MainMenuChangeStatus, "MainMenuChangeStatus")
        '
        'MenuEditSelectAll
        '
        resources.ApplyResources(Me.MenuEditSelectAll, "MenuEditSelectAll")
        '
        'MenuEditSelectNone
        '
        resources.ApplyResources(Me.MenuEditSelectNone, "MenuEditSelectNone")
        '
        'MainMenuSeparator2
        '
        resources.ApplyResources(Me.MainMenuSeparator2, "MainMenuSeparator2")
        '
        'MainMenuGPS
        '
        Me.MainMenuGPS.MenuItems.Add(Me.MainMenuGpsInfo)
        Me.MainMenuGPS.MenuItems.Add(Me.MainMenuGpsOn)
        Me.MainMenuGPS.MenuItems.Add(Me.MainMenuGpsOff)
        resources.ApplyResources(Me.MainMenuGPS, "MainMenuGPS")
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
        Me.MainMenuGpsOff.Checked = True
        resources.ApplyResources(Me.MainMenuGpsOff, "MainMenuGpsOff")
        '
        'MainMenuFilterOwner
        '
        Me.MainMenuFilterOwner.MenuItems.Add(Me.ViewPersonAll)
        Me.MainMenuFilterOwner.MenuItems.Add(Me.ViewPersonOthers)
        Me.MainMenuFilterOwner.MenuItems.Add(Me.ViewPersonMe)
        resources.ApplyResources(Me.MainMenuFilterOwner, "MainMenuFilterOwner")
        '
        'MenuStrip
        '
        Me.MenuStrip.MenuItems.Add(Me.OpenMenu)
        Me.MenuStrip.MenuItems.Add(Me.MainMenu)
        '
        'backgroundLoaderTimer
        '
        '
        'GpsLabel
        '
        Me.GpsLabel.BackColor = System.Drawing.SystemColors.ActiveCaption
        resources.ApplyResources(Me.GpsLabel, "GpsLabel")
        Me.GpsLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GpsLabel.Name = "GpsLabel"
        '
        'RequestDetails
        '
        Me.RequestDetails.ContextMenu = Me.ReadOnlyContextMenu1
        resources.ApplyResources(Me.RequestDetails, "RequestDetails")
        Me.RequestDetails.Name = "RequestDetails"
        Me.RequestDetails.ReadOnly = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.Controls.Add(Me.RequestList)
        Me.Controls.Add(Me.RequestListDetailSplitter)
        Me.Controls.Add(Me.RequestDetails)
        Me.Controls.Add(Me.SyncInfoHighlight)
        Me.Controls.Add(Me.LastSyncPanel)
        Me.Controls.Add(Me.Toolbar)
        Me.Controls.Add(Me.GpsLabel)
        Me.Menu = Me.MenuStrip
        Me.Name = "MainForm"
        Me.Toolbar.ResumeLayout(False)
        CType(Me.ClientListButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NewJobButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OpenRequestButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LastSyncPanel.ResumeLayout(False)
        CType(Me.SyncButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainMenuSync As Windows.Forms.MenuItem
    Friend WithEvents MainMenuSeparator As Windows.Forms.MenuItem
    Friend WithEvents MainMenuExit As Windows.Forms.MenuItem
    Friend WithEvents MainMenuClientList As Windows.Forms.MenuItem
    Friend WithEvents MainMenuNewRequest As Windows.Forms.MenuItem
    Friend WithEvents OpenMenuNewJob As Windows.Forms.MenuItem
    Friend WithEvents OpenMenuRequest As Windows.Forms.MenuItem
    Friend WithEvents ViewShowCheckBoxes As Windows.Forms.MenuItem
    Friend WithEvents ViewToolBar As Windows.Forms.MenuItem
    Friend WithEvents NewJobButton As activiser.Library.Forms.ImageButton
    Friend WithEvents OpenRequestButton As activiser.Library.Forms.ImageButton
    Friend WithEvents RequestList As Windows.Forms.ListView
    Friend WithEvents RequestListMenu As Windows.Forms.ContextMenu
    Friend WithEvents RequestListMenuOpen As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuNewJob As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuListJobs As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuClientInfo As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuRemoveRequest As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuChangeStatus As Windows.Forms.MenuItem
    Friend WithEvents RequestListDetailSplitter As Windows.Forms.Splitter
    Friend WithEvents RequestDetails As Windows.Forms.TextBox
    Friend WithEvents SyncInfoHighlight As Windows.Forms.Panel
    Friend WithEvents ClientListButton As activiser.Library.Forms.ImageButton
    Friend WithEvents Toolbar As Windows.Forms.Panel
    Friend WithEvents ViewPersonAll As Windows.Forms.MenuItem
    Friend WithEvents LastSyncPanel As Windows.Forms.Panel
    Friend WithEvents SyncButton As activiser.Library.Forms.ImageButton


    Friend WithEvents MainMenuView As Windows.Forms.MenuItem
    Friend WithEvents ViewPersonMe As Windows.Forms.MenuItem
    Friend WithEvents ViewPersonOthers As Windows.Forms.MenuItem
    Friend WithEvents MainMenuSettings As Windows.Forms.MenuItem
    Friend WithEvents MainMenuAbout As Windows.Forms.MenuItem
    Friend WithEvents MainMenu As Windows.Forms.MenuItem
    Friend WithEvents MenuStrip As Windows.Forms.MainMenu
    Friend WithEvents ViewMenuDivider1 As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusAll As Windows.Forms.MenuItem
    Friend WithEvents RefreshButton As Windows.Forms.MenuItem
    Friend WithEvents OpenMenuJobList As Windows.Forms.MenuItem
    Friend WithEvents OpenMenuClientInfo As Windows.Forms.MenuItem
    Friend WithEvents MainMenuChangeStatus As Windows.Forms.MenuItem
    Friend WithEvents MainMenuRemoveRequest As Windows.Forms.MenuItem
    Friend WithEvents MainMenuSeparator2 As Windows.Forms.MenuItem
    Friend WithEvents MainMenuRequest As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuSeparator As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuSelectAll As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusNew As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusIP As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusCancelled As Windows.Forms.MenuItem
    Friend WithEvents ViewMenuDivider3 As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusComplete As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusSpecific As Windows.Forms.MenuItem
    Friend WithEvents MainMenuFilterOwner As Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuSelectNone As Windows.Forms.MenuItem
    Friend WithEvents ViewShowTimeInDateColumn As Windows.Forms.MenuItem
    Friend WithEvents ReadOnlyContextMenu1 As activiser.ReadOnlyContextMenu
    Friend WithEvents MainMenuGpsInfo As Windows.Forms.MenuItem
    Friend WithEvents MainMenuGPS As Windows.Forms.MenuItem
    Friend WithEvents MainMenuGpsOn As Windows.Forms.MenuItem
    Friend WithEvents MainMenuGpsOff As Windows.Forms.MenuItem
    Friend WithEvents ViewShowColumnHeaders As Windows.Forms.MenuItem
    Friend WithEvents ViewShowSyncInfo As Windows.Forms.MenuItem
    Friend WithEvents ViewShowRequestedStatus As Windows.Forms.MenuItem
    Friend WithEvents backgroundLoaderTimer As Windows.Forms.Timer
    Friend WithEvents MenuEditSelectAll As Windows.Forms.MenuItem
    Friend WithEvents MenuEditSelectNone As Windows.Forms.MenuItem
    Friend WithEvents GpsLabel As Windows.Forms.Label
    Friend WithEvents ViewMenuToolbarSmall As Windows.Forms.MenuItem
    Friend WithEvents ViewMenuToolbarMedium As Windows.Forms.MenuItem
    Friend WithEvents ViewMenuToolbarLarge As Windows.Forms.MenuItem
    Friend WithEvents ViewMenuToolbarOff As Windows.Forms.MenuItem
    Friend WithEvents OpenMenu As Windows.Forms.MenuItem
    Friend WithEvents MainMenuFilterStatus As Windows.Forms.MenuItem
    Friend WithEvents ViewRotate0 As Windows.Forms.MenuItem
    Friend WithEvents ViewRotate90 As Windows.Forms.MenuItem
    Friend WithEvents ViewRotate180 As Windows.Forms.MenuItem
    Friend WithEvents ViewRotate270 As Windows.Forms.MenuItem
    Friend WithEvents ViewMenuDivider2 As Windows.Forms.MenuItem
    Friend WithEvents ViewFullScreen As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusOpen As Windows.Forms.MenuItem
    Friend WithEvents ViewStatusIncomplete As System.Windows.Forms.MenuItem
    Friend WithEvents ViewMenuToolbarSmaller As System.Windows.Forms.MenuItem
    Friend WithEvents ViewMenuToolbarLarger As System.Windows.Forms.MenuItem
    Friend WithEvents ViewMenuToolbarShowCaptions As System.Windows.Forms.MenuItem
    Friend WithEvents RequestListMenuOpenCurrentJob As System.Windows.Forms.MenuItem
End Class
