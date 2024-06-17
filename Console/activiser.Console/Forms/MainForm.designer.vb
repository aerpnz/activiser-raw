<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("activiser Users")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Other Users")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Request Status     .")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Consultant Request Status     .")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("New")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Approved")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Complete")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Incomplete")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Approval Status", New System.Windows.Forms.TreeNode() {TreeNode5, TreeNode6, TreeNode7, TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Consultant")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.MainFormStatusBar = New System.Windows.Forms.StatusStrip
        Me.ToolStripClock = New System.Windows.Forms.ToolStripStatusLabel
        Me.Springer = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarRefreshNeededLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarRefreshBlockedByEdit = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusBarRefreshEnabled = New System.Windows.Forms.ToolStripStatusLabel
        Me.RefreshProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.MainFormSplitContainer = New System.Windows.Forms.SplitContainer
        Me.NavigationPanel = New System.Windows.Forms.Panel
        Me.NavTreeConsultant = New System.Windows.Forms.TreeView
        Me.NavTreeRequest = New System.Windows.Forms.TreeView
        Me.NavTreeJob = New System.Windows.Forms.TreeView
        Me.NavTreeJobAdminButton = New System.Windows.Forms.Button
        Me.NavTreeRequestAdminButton = New System.Windows.Forms.Button
        Me.NavTreeUsersButton = New System.Windows.Forms.Button
        Me.NavTreeHeaderBG = New System.Windows.Forms.Panel
        Me.NavTreeHeaderLabel = New System.Windows.Forms.Label
        Me.MainPanel = New System.Windows.Forms.Panel
        Me.JobAdminSubForm = New activiser.Console.JobAdminSubForm
        Me.ConsultantSubForm = New activiser.Console.ConsultantSubForm
        Me.RequestAdminSubForm = New activiser.Console.RequestAdminSubForm
        Me.MainPanelHeaderBG = New System.Windows.Forms.Panel
        Me.MainFormHeader = New System.Windows.Forms.Label
        Me.MainMenu = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WordWrapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ServerSettingsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EventLogMenutItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemLogMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeviceTrackingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.CustomiseStatusFields = New System.Windows.Forms.ToolStripMenuItem
        Me.CustomiseLabels = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolMenuSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.OptionsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PollTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LicenseTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.CoreDataSet = New activiser.Library.activiserWebService.activiserDataSet
        Me.RequestStatusBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RefreshFlasher = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStripContainer1.BottomToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.MainFormStatusBar.SuspendLayout()
        Me.MainFormSplitContainer.Panel1.SuspendLayout()
        Me.MainFormSplitContainer.Panel2.SuspendLayout()
        Me.MainFormSplitContainer.SuspendLayout()
        Me.NavigationPanel.SuspendLayout()
        Me.NavTreeHeaderBG.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        Me.MainPanelHeaderBG.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RequestStatusBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.BottomToolStripPanel
        '
        Me.ToolStripContainer1.BottomToolStripPanel.Controls.Add(Me.MainFormStatusBar)
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.AutoScroll = True
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.MainFormSplitContainer)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(1035, 584)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(1035, 630)
        Me.ToolStripContainer1.TabIndex = 16
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.MainMenu)
        '
        'MainFormStatusBar
        '
        Me.MainFormStatusBar.Dock = System.Windows.Forms.DockStyle.None
        Me.MainFormStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripClock, Me.Springer, Me.StatusBarRefreshNeededLabel, Me.StatusBarRefreshBlockedByEdit, Me.StatusBarRefreshEnabled, Me.RefreshProgressBar})
        Me.MainFormStatusBar.Location = New System.Drawing.Point(0, 0)
        Me.MainFormStatusBar.Name = "MainFormStatusBar"
        Me.MainFormStatusBar.Size = New System.Drawing.Size(1035, 22)
        Me.MainFormStatusBar.TabIndex = 15
        '
        'ToolStripClock
        '
        Me.ToolStripClock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripClock.DoubleClickEnabled = True
        Me.ToolStripClock.IsLink = True
        Me.ToolStripClock.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ToolStripClock.LinkColor = System.Drawing.Color.Black
        Me.ToolStripClock.Name = "ToolStripClock"
        Me.ToolStripClock.Size = New System.Drawing.Size(55, 17)
        Me.ToolStripClock.Text = "HH:mm:ss"
        Me.ToolStripClock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Springer
        '
        Me.Springer.Name = "Springer"
        Me.Springer.Size = New System.Drawing.Size(773, 17)
        Me.Springer.Spring = True
        '
        'StatusBarRefreshNeededLabel
        '
        Me.StatusBarRefreshNeededLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarRefreshNeededLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.StatusBarRefreshNeededLabel.DoubleClickEnabled = True
        Me.StatusBarRefreshNeededLabel.Name = "StatusBarRefreshNeededLabel"
        Me.StatusBarRefreshNeededLabel.Size = New System.Drawing.Size(95, 17)
        Me.StatusBarRefreshNeededLabel.Text = "Refresh Required"
        Me.StatusBarRefreshNeededLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.StatusBarRefreshNeededLabel.Visible = False
        '
        'StatusBarRefreshBlockedByEdit
        '
        Me.StatusBarRefreshBlockedByEdit.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarRefreshBlockedByEdit.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.StatusBarRefreshBlockedByEdit.Name = "StatusBarRefreshBlockedByEdit"
        Me.StatusBarRefreshBlockedByEdit.Size = New System.Drawing.Size(85, 17)
        Me.StatusBarRefreshBlockedByEdit.Text = "Edit in Progress"
        Me.StatusBarRefreshBlockedByEdit.Visible = False
        '
        'StatusBarRefreshEnabled
        '
        Me.StatusBarRefreshEnabled.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBarRefreshEnabled.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.StatusBarRefreshEnabled.DoubleClickEnabled = True
        Me.StatusBarRefreshEnabled.Name = "StatusBarRefreshEnabled"
        Me.StatusBarRefreshEnabled.Size = New System.Drawing.Size(90, 17)
        Me.StatusBarRefreshEnabled.Text = "Refresh Enabled"
        Me.StatusBarRefreshEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RefreshProgressBar
        '
        Me.RefreshProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.RefreshProgressBar.Name = "RefreshProgressBar"
        Me.RefreshProgressBar.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.RefreshProgressBar.Size = New System.Drawing.Size(100, 16)
        '
        'MainFormSplitContainer
        '
        Me.MainFormSplitContainer.BackColor = System.Drawing.Color.Transparent
        Me.MainFormSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainFormSplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.MainFormSplitContainer.MinimumSize = New System.Drawing.Size(640, 100)
        Me.MainFormSplitContainer.Name = "MainFormSplitContainer"
        '
        'MainFormSplitContainer.Panel1
        '
        Me.MainFormSplitContainer.Panel1.Controls.Add(Me.NavigationPanel)
        Me.MainFormSplitContainer.Panel1.Controls.Add(Me.NavTreeHeaderBG)
        Me.MainFormSplitContainer.Panel1MinSize = 180
        '
        'MainFormSplitContainer.Panel2
        '
        Me.MainFormSplitContainer.Panel2.Controls.Add(Me.MainPanel)
        Me.MainFormSplitContainer.Panel2MinSize = 200
        Me.MainFormSplitContainer.Size = New System.Drawing.Size(1035, 584)
        Me.MainFormSplitContainer.SplitterDistance = 200
        Me.MainFormSplitContainer.TabIndex = 0
        '
        'NavigationPanel
        '
        Me.NavigationPanel.Controls.Add(Me.NavTreeConsultant)
        Me.NavigationPanel.Controls.Add(Me.NavTreeRequest)
        Me.NavigationPanel.Controls.Add(Me.NavTreeJob)
        Me.NavigationPanel.Controls.Add(Me.NavTreeJobAdminButton)
        Me.NavigationPanel.Controls.Add(Me.NavTreeRequestAdminButton)
        Me.NavigationPanel.Controls.Add(Me.NavTreeUsersButton)
        Me.NavigationPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavigationPanel.Location = New System.Drawing.Point(0, 35)
        Me.NavigationPanel.Name = "NavigationPanel"
        Me.NavigationPanel.Size = New System.Drawing.Size(200, 549)
        Me.NavigationPanel.TabIndex = 9
        '
        'NavTreeConsultant
        '
        Me.NavTreeConsultant.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NavTreeConsultant.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NavTreeConsultant.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavTreeConsultant.FullRowSelect = True
        Me.NavTreeConsultant.HideSelection = False
        Me.NavTreeConsultant.Location = New System.Drawing.Point(0, 273)
        Me.NavTreeConsultant.Name = "NavTreeConsultant"
        TreeNode1.BackColor = System.Drawing.SystemColors.Window
        TreeNode1.ForeColor = System.Drawing.SystemColors.Highlight
        TreeNode1.Name = "activiserUsersRootNode"
        TreeNode1.NodeFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        TreeNode1.Text = "activiser Users"
        TreeNode2.BackColor = System.Drawing.SystemColors.Window
        TreeNode2.ForeColor = System.Drawing.SystemColors.Highlight
        TreeNode2.Name = "otherUsersRootNode"
        TreeNode2.NodeFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        TreeNode2.Text = "Other Users"
        Me.NavTreeConsultant.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Me.NavTreeConsultant.ShowPlusMinus = False
        Me.NavTreeConsultant.ShowRootLines = False
        Me.NavTreeConsultant.Size = New System.Drawing.Size(200, 96)
        Me.NavTreeConsultant.TabIndex = 10
        Me.NavTreeConsultant.Visible = False
        '
        'NavTreeRequest
        '
        Me.NavTreeRequest.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NavTreeRequest.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NavTreeRequest.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavTreeRequest.FullRowSelect = True
        Me.NavTreeRequest.HideSelection = False
        Me.NavTreeRequest.Location = New System.Drawing.Point(0, 155)
        Me.NavTreeRequest.Name = "NavTreeRequest"
        TreeNode3.BackColor = System.Drawing.SystemColors.Window
        TreeNode3.ForeColor = System.Drawing.SystemColors.Highlight
        TreeNode3.Name = "requestStatusRoot"
        TreeNode3.NodeFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        TreeNode3.Tag = "requestStatusRoot"
        TreeNode3.Text = "Request Status     ."
        TreeNode4.BackColor = System.Drawing.SystemColors.Window
        TreeNode4.ForeColor = System.Drawing.SystemColors.Highlight
        TreeNode4.Name = "consultantStatusRoot"
        TreeNode4.NodeFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        TreeNode4.Tag = "consultantStatusRoot"
        TreeNode4.Text = "Consultant Request Status     ."
        Me.NavTreeRequest.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode4})
        Me.NavTreeRequest.ShowRootLines = False
        Me.NavTreeRequest.Size = New System.Drawing.Size(200, 118)
        Me.NavTreeRequest.TabIndex = 9
        Me.NavTreeRequest.Visible = False
        '
        'NavTreeJob
        '
        Me.NavTreeJob.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NavTreeJob.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NavTreeJob.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavTreeJob.FullRowSelect = True
        Me.NavTreeJob.HideSelection = False
        Me.NavTreeJob.Location = New System.Drawing.Point(0, 0)
        Me.NavTreeJob.Name = "NavTreeJob"
        TreeNode5.Name = "nodeNew"
        TreeNode5.Text = "New"
        TreeNode6.Name = "nodeApproved"
        TreeNode6.Text = "Approved"
        TreeNode7.Name = "nodeComplete"
        TreeNode7.Text = "All Complete"
        TreeNode8.Name = "nodeIncomplete"
        TreeNode8.Text = "Incomplete"
        TreeNode9.BackColor = System.Drawing.SystemColors.Window
        TreeNode9.ForeColor = System.Drawing.SystemColors.Highlight
        TreeNode9.Name = "approvalStatusRoot"
        TreeNode9.NodeFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        TreeNode9.Text = "Approval Status"
        TreeNode10.BackColor = System.Drawing.SystemColors.Window
        TreeNode10.ForeColor = System.Drawing.SystemColors.Highlight
        TreeNode10.Name = "consultantRoot"
        TreeNode10.NodeFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        TreeNode10.Text = "Consultant"
        Me.NavTreeJob.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode9, TreeNode10})
        Me.NavTreeJob.ShowRootLines = False
        Me.NavTreeJob.Size = New System.Drawing.Size(200, 155)
        Me.NavTreeJob.TabIndex = 7
        '
        'NavTreeJobAdminButton
        '
        Me.NavTreeJobAdminButton.BackgroundImage = CType(resources.GetObject("NavTreeJobAdminButton.BackgroundImage"), System.Drawing.Image)
        Me.NavTreeJobAdminButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NavTreeJobAdminButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.NavTreeJobAdminButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.NavTreeJobAdminButton.FlatAppearance.BorderSize = 0
        Me.NavTreeJobAdminButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NavTreeJobAdminButton.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.NavTreeJobAdminButton.Image = Global.activiser.Console.My.Resources.Resources.document_time
        Me.NavTreeJobAdminButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NavTreeJobAdminButton.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.NavTreeJobAdminButton.Location = New System.Drawing.Point(0, 450)
        Me.NavTreeJobAdminButton.Name = "NavTreeJobAdminButton"
        Me.NavTreeJobAdminButton.Size = New System.Drawing.Size(200, 33)
        Me.NavTreeJobAdminButton.TabIndex = 0
        Me.NavTreeJobAdminButton.Text = "Job Admin"
        Me.NavTreeJobAdminButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.NavTreeJobAdminButton.UseMnemonic = False
        '
        'NavTreeRequestAdminButton
        '
        Me.NavTreeRequestAdminButton.BackgroundImage = CType(resources.GetObject("NavTreeRequestAdminButton.BackgroundImage"), System.Drawing.Image)
        Me.NavTreeRequestAdminButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NavTreeRequestAdminButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.NavTreeRequestAdminButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.NavTreeRequestAdminButton.FlatAppearance.BorderSize = 0
        Me.NavTreeRequestAdminButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NavTreeRequestAdminButton.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.NavTreeRequestAdminButton.Image = Global.activiser.Console.My.Resources.Resources.document_time
        Me.NavTreeRequestAdminButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NavTreeRequestAdminButton.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.NavTreeRequestAdminButton.Location = New System.Drawing.Point(0, 483)
        Me.NavTreeRequestAdminButton.Name = "NavTreeRequestAdminButton"
        Me.NavTreeRequestAdminButton.Size = New System.Drawing.Size(200, 33)
        Me.NavTreeRequestAdminButton.TabIndex = 3
        Me.NavTreeRequestAdminButton.Text = "Request Admin"
        Me.NavTreeRequestAdminButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.NavTreeRequestAdminButton.UseMnemonic = False
        '
        'NavTreeUsersButton
        '
        Me.NavTreeUsersButton.BackgroundImage = CType(resources.GetObject("NavTreeUsersButton.BackgroundImage"), System.Drawing.Image)
        Me.NavTreeUsersButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NavTreeUsersButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.NavTreeUsersButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.NavTreeUsersButton.FlatAppearance.BorderSize = 0
        Me.NavTreeUsersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NavTreeUsersButton.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.NavTreeUsersButton.Image = Global.activiser.Console.My.Resources.Resources.users2
        Me.NavTreeUsersButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NavTreeUsersButton.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.NavTreeUsersButton.Location = New System.Drawing.Point(0, 516)
        Me.NavTreeUsersButton.Name = "NavTreeUsersButton"
        Me.NavTreeUsersButton.Size = New System.Drawing.Size(200, 33)
        Me.NavTreeUsersButton.TabIndex = 1
        Me.NavTreeUsersButton.Text = "Users"
        Me.NavTreeUsersButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.NavTreeUsersButton.UseMnemonic = False
        '
        'NavTreeHeaderBG
        '
        Me.NavTreeHeaderBG.BackgroundImage = Global.activiser.Console.My.Resources.Resources.ToolHeadBG
        Me.NavTreeHeaderBG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NavTreeHeaderBG.Controls.Add(Me.NavTreeHeaderLabel)
        Me.NavTreeHeaderBG.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavTreeHeaderBG.Location = New System.Drawing.Point(0, 0)
        Me.NavTreeHeaderBG.Name = "NavTreeHeaderBG"
        Me.NavTreeHeaderBG.Size = New System.Drawing.Size(200, 35)
        Me.NavTreeHeaderBG.TabIndex = 11
        '
        'NavTreeHeaderLabel
        '
        Me.NavTreeHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavTreeHeaderLabel.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.NavTreeHeaderLabel.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.NavTreeHeaderLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.NavTreeHeaderLabel.Location = New System.Drawing.Point(0, 0)
        Me.NavTreeHeaderLabel.Name = "NavTreeHeaderLabel"
        Me.NavTreeHeaderLabel.Size = New System.Drawing.Size(200, 35)
        Me.NavTreeHeaderLabel.TabIndex = 11
        Me.NavTreeHeaderLabel.Text = "Approval"
        Me.NavTreeHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.JobAdminSubForm)
        Me.MainPanel.Controls.Add(Me.ConsultantSubForm)
        Me.MainPanel.Controls.Add(Me.RequestAdminSubForm)
        Me.MainPanel.Controls.Add(Me.MainPanelHeaderBG)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(831, 584)
        Me.MainPanel.TabIndex = 11
        '
        'JobAdminSubForm
        '
        Me.JobAdminSubForm.AutoScroll = True
        Me.JobAdminSubForm.Filter = "JobStatusID < 5 AND JobStatusID < 5 AND JobStatusID < 5 AND JobStatusID < 5 AND J" & _
            "obStatusID < 5 AND JobStatusID<5 AND JobStatusID<5 AND JobStatusID<5 AND JobStat" & _
            "usID<5 AND JobStatusID<5"
        Me.JobAdminSubForm.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.JobAdminSubForm.Location = New System.Drawing.Point(196, 51)
        Me.JobAdminSubForm.MinimumSize = New System.Drawing.Size(750, 540)
        Me.JobAdminSubForm.Name = "JobAdminSubForm"
        Me.JobAdminSubForm.Size = New System.Drawing.Size(750, 540)
        Me.JobAdminSubForm.TabIndex = 0
        '
        'ConsultantSubForm
        '
        Me.ConsultantSubForm.AccessLevel = activiser.Console.AccessLevels.None
        Me.ConsultantSubForm.Filter = Nothing
        Me.ConsultantSubForm.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ConsultantSubForm.Location = New System.Drawing.Point(372, 51)
        Me.ConsultantSubForm.MinimumSize = New System.Drawing.Size(640, 480)
        Me.ConsultantSubForm.Name = "ConsultantSubForm"
        Me.ConsultantSubForm.Size = New System.Drawing.Size(640, 480)
        Me.ConsultantSubForm.TabIndex = 0
        Me.ConsultantSubForm.Visible = False
        '
        'RequestAdminSubForm
        '
        Me.RequestAdminSubForm.Filter = Nothing
        Me.RequestAdminSubForm.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.RequestAdminSubForm.Location = New System.Drawing.Point(28, 51)
        Me.RequestAdminSubForm.Name = "RequestAdminSubForm"
        Me.RequestAdminSubForm.Size = New System.Drawing.Size(150, 150)
        Me.RequestAdminSubForm.TabIndex = 0
        Me.RequestAdminSubForm.Visible = False
        '
        'MainPanelHeaderBG
        '
        Me.MainPanelHeaderBG.BackgroundImage = Global.activiser.Console.My.Resources.Resources.ToolHeadBG
        Me.MainPanelHeaderBG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MainPanelHeaderBG.Controls.Add(Me.MainFormHeader)
        Me.MainPanelHeaderBG.Dock = System.Windows.Forms.DockStyle.Top
        Me.MainPanelHeaderBG.Location = New System.Drawing.Point(0, 0)
        Me.MainPanelHeaderBG.Name = "MainPanelHeaderBG"
        Me.MainPanelHeaderBG.Size = New System.Drawing.Size(831, 35)
        Me.MainPanelHeaderBG.TabIndex = 8
        '
        'MainFormHeader
        '
        Me.MainFormHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainFormHeader.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MainFormHeader.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.MainFormHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MainFormHeader.Location = New System.Drawing.Point(0, 0)
        Me.MainFormHeader.Name = "MainFormHeader"
        Me.MainFormHeader.Size = New System.Drawing.Size(831, 35)
        Me.MainFormHeader.TabIndex = 7
        Me.MainFormHeader.Text = "Administration"
        Me.MainFormHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainMenu
        '
        Me.MainMenu.Dock = System.Windows.Forms.DockStyle.None
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(1035, 24)
        Me.MainMenu.TabIndex = 13
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.SaveToolStripMenuItem, Me.toolStripSeparator1, Me.PrintToolStripMenuItem, Me.PrintPreviewToolStripMenuItem, Me.toolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.NewToolStripMenuItem.Text = "&New"
        Me.NewToolStripMenuItem.Visible = False
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(145, 6)
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.PrintToolStripMenuItem.Text = "&Print"
        Me.PrintToolStripMenuItem.Visible = False
        '
        'PrintPreviewToolStripMenuItem
        '
        Me.PrintPreviewToolStripMenuItem.Image = CType(resources.GetObject("PrintPreviewToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        Me.PrintPreviewToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.PrintPreviewToolStripMenuItem.Text = "Print Pre&view"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(145, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.toolStripSeparator4, Me.SelectAllToolStripMenuItem})
        Me.EditToolStripMenuItem.Enabled = False
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        Me.EditToolStripMenuItem.Visible = False
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Image = CType(resources.GetObject("CutToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.CutToolStripMenuItem.Text = "Cu&t"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Image = CType(resources.GetObject("PasteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.PasteToolStripMenuItem.Text = "&Paste"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(125, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select &All"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.WordWrapToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeyDisplayString = "F5"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'WordWrapToolStripMenuItem
        '
        Me.WordWrapToolStripMenuItem.Name = "WordWrapToolStripMenuItem"
        Me.WordWrapToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.WordWrapToolStripMenuItem.Text = "Word Wrap"
        Me.WordWrapToolStripMenuItem.Visible = False
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServerSettingsMenuItem, Me.EventLogMenutItem, Me.SystemLogMenuItem, Me.DeviceTrackingToolStripMenuItem, Me.ToolMenuSeparator1, Me.CustomiseStatusFields, Me.CustomiseLabels, Me.ToolMenuSeparator2, Me.OptionsMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'ServerSettingsMenuItem
        '
        Me.ServerSettingsMenuItem.Enabled = False
        Me.ServerSettingsMenuItem.Name = "ServerSettingsMenuItem"
        Me.ServerSettingsMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.ServerSettingsMenuItem.Text = "&Server Settings..."
        '
        'EventLogMenutItem
        '
        Me.EventLogMenutItem.Name = "EventLogMenutItem"
        Me.EventLogMenutItem.Size = New System.Drawing.Size(241, 22)
        Me.EventLogMenutItem.Text = "Event Log..."
        '
        'SystemLogMenuItem
        '
        Me.SystemLogMenuItem.Name = "SystemLogMenuItem"
        Me.SystemLogMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.SystemLogMenuItem.Text = "View System Logs (Advanced)..."
        '
        'DeviceTrackingToolStripMenuItem
        '
        Me.DeviceTrackingToolStripMenuItem.Name = "DeviceTrackingToolStripMenuItem"
        Me.DeviceTrackingToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.DeviceTrackingToolStripMenuItem.Text = "Device Tracking (GPS)..."
        '
        'ToolMenuSeparator1
        '
        Me.ToolMenuSeparator1.Name = "ToolMenuSeparator1"
        Me.ToolMenuSeparator1.Size = New System.Drawing.Size(238, 6)
        '
        'CustomiseStatusFields
        '
        Me.CustomiseStatusFields.Name = "CustomiseStatusFields"
        Me.CustomiseStatusFields.Size = New System.Drawing.Size(241, 22)
        Me.CustomiseStatusFields.Text = "Customise Status Fields..."
        '
        'CustomiseLabels
        '
        Me.CustomiseLabels.Name = "CustomiseLabels"
        Me.CustomiseLabels.Size = New System.Drawing.Size(241, 22)
        Me.CustomiseLabels.Text = "Customise Labels..."
        Me.CustomiseLabels.Visible = False
        '
        'ToolMenuSeparator2
        '
        Me.ToolMenuSeparator2.Name = "ToolMenuSeparator2"
        Me.ToolMenuSeparator2.Size = New System.Drawing.Size(238, 6)
        '
        'OptionsMenuItem
        '
        Me.OptionsMenuItem.Name = "OptionsMenuItem"
        Me.OptionsMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.OptionsMenuItem.Text = "Options..."
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.IndexToolStripMenuItem, Me.toolStripSeparator5, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ContentsToolStripMenuItem.Text = "&Contents"
        Me.ContentsToolStripMenuItem.Visible = False
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        Me.IndexToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.IndexToolStripMenuItem.Text = "&Index"
        Me.IndexToolStripMenuItem.Visible = False
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        Me.toolStripSeparator5.Size = New System.Drawing.Size(126, 6)
        Me.toolStripSeparator5.Visible = False
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.AboutToolStripMenuItem.Text = "&About..."
        '
        'PollTimer
        '
        Me.PollTimer.Enabled = True
        Me.PollTimer.Interval = 1000
        '
        'LicenseTimer
        '
        Me.LicenseTimer.Enabled = True
        Me.LicenseTimer.Interval = 3600000
        '
        'CoreDataSet
        '
        Me.CoreDataSet.DataSetName = "activiserCoreDataSet"
        Me.CoreDataSet.Locale = New System.Globalization.CultureInfo(Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo)
        Me.CoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RequestStatusBindingSource
        '
        Me.RequestStatusBindingSource.DataMember = "RequestStatus"
        Me.RequestStatusBindingSource.DataSource = Me.CoreDataSet
        '
        'RefreshFlasher
        '
        Me.RefreshFlasher.Interval = 500
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 630)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "activiser™ Management Console"
        Me.ToolStripContainer1.BottomToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.BottomToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.MainFormStatusBar.ResumeLayout(False)
        Me.MainFormStatusBar.PerformLayout()
        Me.MainFormSplitContainer.Panel1.ResumeLayout(False)
        Me.MainFormSplitContainer.Panel2.ResumeLayout(False)
        Me.MainFormSplitContainer.ResumeLayout(False)
        Me.NavigationPanel.ResumeLayout(False)
        Me.NavTreeHeaderBG.ResumeLayout(False)
        Me.MainPanel.ResumeLayout(False)
        Me.MainPanelHeaderBG.ResumeLayout(False)
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RequestStatusBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NavTreeJob As System.Windows.Forms.TreeView
    Friend WithEvents NavTreeUsersButton As System.Windows.Forms.Button
    Friend WithEvents NavTreeJobAdminButton As System.Windows.Forms.Button
    Friend WithEvents MainFormHeader As System.Windows.Forms.Label
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents PollTimer As System.Windows.Forms.Timer
    Friend WithEvents NavigationPanel As System.Windows.Forms.Panel
    Friend WithEvents MainFormSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ServerSettingsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CoreDataSet As Library.activiserWebService.activiserDataSet
    Friend WithEvents ConsultantSubForm As activiser.Console.ConsultantSubForm
    Friend WithEvents CustomiseStatusFields As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RequestAdminSubForm As activiser.Console.RequestAdminSubForm
    Friend WithEvents RequestStatusBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NavTreeRequestAdminButton As System.Windows.Forms.Button
    Friend WithEvents JobAdminSubForm As activiser.Console.JobAdminSubForm
    Friend WithEvents LicenseTimer As System.Windows.Forms.Timer
    Friend WithEvents SystemLogMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomiseLabels As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents MainFormStatusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusBarRefreshEnabled As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents RefreshProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents StatusBarRefreshNeededLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Springer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBarRefreshBlockedByEdit As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripClock As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents RefreshFlasher As System.Windows.Forms.Timer
    Friend WithEvents WordWrapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NavTreeRequest As System.Windows.Forms.TreeView
    Friend WithEvents NavTreeConsultant As System.Windows.Forms.TreeView
    Friend WithEvents ToolMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolMenuSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EventLogMenutItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents MainPanelHeaderBG As System.Windows.Forms.Panel
    Friend WithEvents NavTreeHeaderBG As System.Windows.Forms.Panel
    Friend WithEvents NavTreeHeaderLabel As System.Windows.Forms.Label
    Friend WithEvents DeviceTrackingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
