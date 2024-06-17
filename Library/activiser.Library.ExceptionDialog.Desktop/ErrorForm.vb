Option Explicit On 
Option Strict On

Imports System.Reflection


Friend Class ErrorForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '  MyBase.New()

    '  'This call is required by the Windows Form Designer.
    '  InitializeComponent()

    '  'Add any initialization after the InitializeComponent() call

    'End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Friend WithEvents chdDescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtExceptionTypeValue As System.Windows.Forms.TextBox
    Friend WithEvents chdName As System.Windows.Forms.ColumnHeader
    Friend WithEvents tabInfo As System.Windows.Forms.TabControl
    Friend WithEvents tabGeneral As System.Windows.Forms.TabPage
    Friend WithEvents txtHelpLinkValue As System.Windows.Forms.TextBox
    Friend WithEvents lblHelpLink As System.Windows.Forms.Label
    Friend WithEvents txtExceptionSourceValue As System.Windows.Forms.TextBox
    Friend WithEvents txtExceptionTargetMethodValue As System.Windows.Forms.TextBox
    Friend WithEvents txtExceptionMessageValue As System.Windows.Forms.TextBox
    Friend WithEvents lblExceptionTargetMethod As System.Windows.Forms.Label
    Friend WithEvents lblExceptionSource As System.Windows.Forms.Label
    Friend WithEvents lblExceptionMessage As System.Windows.Forms.Label
    Friend WithEvents tabInner As System.Windows.Forms.TabPage
    Friend WithEvents trvInnerExceptions As System.Windows.Forms.TreeView
    Friend WithEvents tabStack As System.Windows.Forms.TabPage
    Friend WithEvents tabSpecific As System.Windows.Forms.TabPage
    Friend WithEvents lstOtherInfo As System.Windows.Forms.ListView
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents tabTip As System.Windows.Forms.TabPage
    Friend WithEvents txtTip As System.Windows.Forms.TextBox
    Friend WithEvents IconImageList As System.Windows.Forms.ImageList
    Friend WithEvents IconListView As System.Windows.Forms.ListView
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents ButtonImageList As System.Windows.Forms.ImageList
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents FormToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents txtUserFriendlyMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents ThePrintDocument As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog As System.Windows.Forms.PrintDialog
    Friend WithEvents tabSystem As System.Windows.Forms.TabPage
    Friend WithEvents txtTotalPhysicalMemory As System.Windows.Forms.TextBox
    Friend WithEvents txtAvailablePhysicalMemory As System.Windows.Forms.TextBox
    Friend WithEvents txtWindowsUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingSystemVersion As System.Windows.Forms.TextBox
    Friend WithEvents lblWindowsUserName As System.Windows.Forms.Label
    Friend WithEvents lblAvailablePhysicalMemory As System.Windows.Forms.Label
    Friend WithEvents lblTotalPhysicalMemory As System.Windows.Forms.Label
    Friend WithEvents lblOperatingSystemVersion As System.Windows.Forms.Label
    Friend WithEvents txtMachineName As System.Windows.Forms.TextBox
    Friend WithEvents lblMachineName As System.Windows.Forms.Label
    Friend WithEvents txtDomainName As System.Windows.Forms.TextBox
    Friend WithEvents lblDomainName As System.Windows.Forms.Label
    Friend WithEvents txtProcessorType As System.Windows.Forms.TextBox
    Friend WithEvents StackTrace As System.Windows.Forms.TextBox
    Friend WithEvents lblProcessorType As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ErrorForm))
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("", 0)
        Me.chdDescription = New System.Windows.Forms.ColumnHeader
        Me.txtExceptionTypeValue = New System.Windows.Forms.TextBox
        Me.chdName = New System.Windows.Forms.ColumnHeader
        Me.tabInfo = New System.Windows.Forms.TabControl
        Me.tabGeneral = New System.Windows.Forms.TabPage
        Me.txtHelpLinkValue = New System.Windows.Forms.TextBox
        Me.lblHelpLink = New System.Windows.Forms.Label
        Me.txtExceptionSourceValue = New System.Windows.Forms.TextBox
        Me.txtExceptionTargetMethodValue = New System.Windows.Forms.TextBox
        Me.txtExceptionMessageValue = New System.Windows.Forms.TextBox
        Me.lblExceptionTargetMethod = New System.Windows.Forms.Label
        Me.lblExceptionSource = New System.Windows.Forms.Label
        Me.lblExceptionMessage = New System.Windows.Forms.Label
        Me.tabStack = New System.Windows.Forms.TabPage
        Me.tabInner = New System.Windows.Forms.TabPage
        Me.trvInnerExceptions = New System.Windows.Forms.TreeView
        Me.tabTip = New System.Windows.Forms.TabPage
        Me.txtTip = New System.Windows.Forms.TextBox
        Me.tabSpecific = New System.Windows.Forms.TabPage
        Me.lstOtherInfo = New System.Windows.Forms.ListView
        Me.tabSystem = New System.Windows.Forms.TabPage
        Me.lblProcessorType = New System.Windows.Forms.Label
        Me.lblDomainName = New System.Windows.Forms.Label
        Me.lblMachineName = New System.Windows.Forms.Label
        Me.lblOperatingSystemVersion = New System.Windows.Forms.Label
        Me.lblWindowsUserName = New System.Windows.Forms.Label
        Me.txtProcessorType = New System.Windows.Forms.TextBox
        Me.txtDomainName = New System.Windows.Forms.TextBox
        Me.txtMachineName = New System.Windows.Forms.TextBox
        Me.txtOperatingSystemVersion = New System.Windows.Forms.TextBox
        Me.txtWindowsUserName = New System.Windows.Forms.TextBox
        Me.lblAvailablePhysicalMemory = New System.Windows.Forms.Label
        Me.txtAvailablePhysicalMemory = New System.Windows.Forms.TextBox
        Me.lblTotalPhysicalMemory = New System.Windows.Forms.Label
        Me.txtTotalPhysicalMemory = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblError = New System.Windows.Forms.Label
        Me.IconImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.IconListView = New System.Windows.Forms.ListView
        Me.txtUserFriendlyMessage = New System.Windows.Forms.TextBox
        Me.btnDetails = New System.Windows.Forms.Button
        Me.ButtonImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.btnCopy = New System.Windows.Forms.Button
        Me.FormToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnPrint = New System.Windows.Forms.Button
        Me.PrintDialog = New System.Windows.Forms.PrintDialog
        Me.ThePrintDocument = New System.Drawing.Printing.PrintDocument
        Me.StackTrace = New System.Windows.Forms.TextBox
        Me.tabInfo.SuspendLayout()
        Me.tabGeneral.SuspendLayout()
        Me.tabStack.SuspendLayout()
        Me.tabInner.SuspendLayout()
        Me.tabTip.SuspendLayout()
        Me.tabSpecific.SuspendLayout()
        Me.tabSystem.SuspendLayout()
        Me.SuspendLayout()
        '
        'chdDescription
        '
        Me.chdDescription.Text = "Description"
        Me.chdDescription.Width = 350
        '
        'txtExceptionTypeValue
        '
        Me.txtExceptionTypeValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExceptionTypeValue.BackColor = System.Drawing.SystemColors.Control
        Me.txtExceptionTypeValue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExceptionTypeValue.Location = New System.Drawing.Point(192, 7)
        Me.txtExceptionTypeValue.Name = "txtExceptionTypeValue"
        Me.txtExceptionTypeValue.ReadOnly = True
        Me.txtExceptionTypeValue.Size = New System.Drawing.Size(336, 21)
        Me.txtExceptionTypeValue.TabIndex = 24
        Me.txtExceptionTypeValue.TabStop = False
        Me.FormToolTip.SetToolTip(Me.txtExceptionTypeValue, "Exception Type")
        '
        'chdName
        '
        Me.chdName.Text = "Name"
        Me.chdName.Width = 120
        '
        'tabInfo
        '
        Me.tabInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabInfo.Controls.Add(Me.tabGeneral)
        Me.tabInfo.Controls.Add(Me.tabStack)
        Me.tabInfo.Controls.Add(Me.tabInner)
        Me.tabInfo.Controls.Add(Me.tabTip)
        Me.tabInfo.Controls.Add(Me.tabSpecific)
        Me.tabInfo.Controls.Add(Me.tabSystem)
        Me.tabInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabInfo.Location = New System.Drawing.Point(8, 184)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.Size = New System.Drawing.Size(520, 216)
        Me.tabInfo.TabIndex = 23
        Me.FormToolTip.SetToolTip(Me.tabInfo, "Additional Information")
        '
        'tabGeneral
        '
        Me.tabGeneral.Controls.Add(Me.txtHelpLinkValue)
        Me.tabGeneral.Controls.Add(Me.lblHelpLink)
        Me.tabGeneral.Controls.Add(Me.txtExceptionSourceValue)
        Me.tabGeneral.Controls.Add(Me.txtExceptionTargetMethodValue)
        Me.tabGeneral.Controls.Add(Me.txtExceptionMessageValue)
        Me.tabGeneral.Controls.Add(Me.lblExceptionTargetMethod)
        Me.tabGeneral.Controls.Add(Me.lblExceptionSource)
        Me.tabGeneral.Controls.Add(Me.lblExceptionMessage)
        Me.tabGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tabGeneral.Name = "tabGeneral"
        Me.tabGeneral.Size = New System.Drawing.Size(512, 190)
        Me.tabGeneral.TabIndex = 0
        Me.tabGeneral.Text = "General Information"
        '
        'txtHelpLinkValue
        '
        Me.txtHelpLinkValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHelpLinkValue.Location = New System.Drawing.Point(96, 160)
        Me.txtHelpLinkValue.Name = "txtHelpLinkValue"
        Me.txtHelpLinkValue.ReadOnly = True
        Me.txtHelpLinkValue.Size = New System.Drawing.Size(408, 21)
        Me.txtHelpLinkValue.TabIndex = 23
        Me.txtHelpLinkValue.TabStop = False
        '
        'lblHelpLink
        '
        Me.lblHelpLink.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblHelpLink.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblHelpLink.Location = New System.Drawing.Point(8, 160)
        Me.lblHelpLink.Name = "lblHelpLink"
        Me.lblHelpLink.Size = New System.Drawing.Size(80, 16)
        Me.lblHelpLink.TabIndex = 22
        Me.lblHelpLink.Text = "Help Link"
        Me.lblHelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtExceptionSourceValue
        '
        Me.txtExceptionSourceValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExceptionSourceValue.Location = New System.Drawing.Point(96, 112)
        Me.txtExceptionSourceValue.Name = "txtExceptionSourceValue"
        Me.txtExceptionSourceValue.ReadOnly = True
        Me.txtExceptionSourceValue.Size = New System.Drawing.Size(408, 21)
        Me.txtExceptionSourceValue.TabIndex = 21
        Me.txtExceptionSourceValue.TabStop = False
        '
        'txtExceptionTargetMethodValue
        '
        Me.txtExceptionTargetMethodValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExceptionTargetMethodValue.Location = New System.Drawing.Point(96, 136)
        Me.txtExceptionTargetMethodValue.Name = "txtExceptionTargetMethodValue"
        Me.txtExceptionTargetMethodValue.ReadOnly = True
        Me.txtExceptionTargetMethodValue.Size = New System.Drawing.Size(408, 21)
        Me.txtExceptionTargetMethodValue.TabIndex = 19
        Me.txtExceptionTargetMethodValue.TabStop = False
        '
        'txtExceptionMessageValue
        '
        Me.txtExceptionMessageValue.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExceptionMessageValue.Location = New System.Drawing.Point(96, 8)
        Me.txtExceptionMessageValue.Multiline = True
        Me.txtExceptionMessageValue.Name = "txtExceptionMessageValue"
        Me.txtExceptionMessageValue.ReadOnly = True
        Me.txtExceptionMessageValue.Size = New System.Drawing.Size(408, 96)
        Me.txtExceptionMessageValue.TabIndex = 18
        Me.txtExceptionMessageValue.TabStop = False
        '
        'lblExceptionTargetMethod
        '
        Me.lblExceptionTargetMethod.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblExceptionTargetMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblExceptionTargetMethod.Location = New System.Drawing.Point(8, 136)
        Me.lblExceptionTargetMethod.Name = "lblExceptionTargetMethod"
        Me.lblExceptionTargetMethod.Size = New System.Drawing.Size(80, 16)
        Me.lblExceptionTargetMethod.TabIndex = 16
        Me.lblExceptionTargetMethod.Text = "Target Method"
        Me.lblExceptionTargetMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExceptionSource
        '
        Me.lblExceptionSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblExceptionSource.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblExceptionSource.Location = New System.Drawing.Point(8, 112)
        Me.lblExceptionSource.Name = "lblExceptionSource"
        Me.lblExceptionSource.Size = New System.Drawing.Size(80, 16)
        Me.lblExceptionSource.TabIndex = 14
        Me.lblExceptionSource.Text = "Source"
        Me.lblExceptionSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExceptionMessage
        '
        Me.lblExceptionMessage.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblExceptionMessage.Location = New System.Drawing.Point(8, 8)
        Me.lblExceptionMessage.Name = "lblExceptionMessage"
        Me.lblExceptionMessage.Size = New System.Drawing.Size(80, 16)
        Me.lblExceptionMessage.TabIndex = 12
        Me.lblExceptionMessage.Text = "Message"
        Me.lblExceptionMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabStack
        '
        Me.tabStack.Controls.Add(Me.StackTrace)
        Me.tabStack.Location = New System.Drawing.Point(4, 22)
        Me.tabStack.Name = "tabStack"
        Me.tabStack.Size = New System.Drawing.Size(512, 190)
        Me.tabStack.TabIndex = 2
        Me.tabStack.Text = "Stack Trace"
        Me.tabStack.Visible = False
        '
        'tabInner
        '
        Me.tabInner.Controls.Add(Me.trvInnerExceptions)
        Me.tabInner.Location = New System.Drawing.Point(4, 22)
        Me.tabInner.Name = "tabInner"
        Me.tabInner.Size = New System.Drawing.Size(512, 190)
        Me.tabInner.TabIndex = 1
        Me.tabInner.Text = "Inner Exception Trace"
        Me.tabInner.Visible = False
        '
        'trvInnerExceptions
        '
        Me.trvInnerExceptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trvInnerExceptions.Location = New System.Drawing.Point(8, 8)
        Me.trvInnerExceptions.Name = "trvInnerExceptions"
        Me.trvInnerExceptions.Size = New System.Drawing.Size(496, 176)
        Me.trvInnerExceptions.TabIndex = 0
        '
        'tabTip
        '
        Me.tabTip.Controls.Add(Me.txtTip)
        Me.tabTip.Location = New System.Drawing.Point(4, 22)
        Me.tabTip.Name = "tabTip"
        Me.tabTip.Size = New System.Drawing.Size(512, 190)
        Me.tabTip.TabIndex = 4
        Me.tabTip.Text = "Tip"
        '
        'txtTip
        '
        Me.txtTip.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTip.Location = New System.Drawing.Point(8, 8)
        Me.txtTip.Multiline = True
        Me.txtTip.Name = "txtTip"
        Me.txtTip.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTip.Size = New System.Drawing.Size(496, 176)
        Me.txtTip.TabIndex = 0
        '
        'tabSpecific
        '
        Me.tabSpecific.Controls.Add(Me.lstOtherInfo)
        Me.tabSpecific.Location = New System.Drawing.Point(4, 22)
        Me.tabSpecific.Name = "tabSpecific"
        Me.tabSpecific.Size = New System.Drawing.Size(512, 190)
        Me.tabSpecific.TabIndex = 3
        Me.tabSpecific.Text = "Other Information"
        Me.tabSpecific.Visible = False
        '
        'lstOtherInfo
        '
        Me.lstOtherInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstOtherInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chdName, Me.chdDescription})
        Me.lstOtherInfo.Location = New System.Drawing.Point(8, 8)
        Me.lstOtherInfo.Name = "lstOtherInfo"
        Me.lstOtherInfo.Size = New System.Drawing.Size(496, 176)
        Me.lstOtherInfo.TabIndex = 0
        Me.lstOtherInfo.UseCompatibleStateImageBehavior = False
        Me.lstOtherInfo.View = System.Windows.Forms.View.Details
        '
        'tabSystem
        '
        Me.tabSystem.Controls.Add(Me.lblProcessorType)
        Me.tabSystem.Controls.Add(Me.lblDomainName)
        Me.tabSystem.Controls.Add(Me.lblMachineName)
        Me.tabSystem.Controls.Add(Me.lblOperatingSystemVersion)
        Me.tabSystem.Controls.Add(Me.lblWindowsUserName)
        Me.tabSystem.Controls.Add(Me.txtProcessorType)
        Me.tabSystem.Controls.Add(Me.txtDomainName)
        Me.tabSystem.Controls.Add(Me.txtMachineName)
        Me.tabSystem.Controls.Add(Me.txtOperatingSystemVersion)
        Me.tabSystem.Controls.Add(Me.txtWindowsUserName)
        Me.tabSystem.Controls.Add(Me.lblAvailablePhysicalMemory)
        Me.tabSystem.Controls.Add(Me.txtAvailablePhysicalMemory)
        Me.tabSystem.Controls.Add(Me.lblTotalPhysicalMemory)
        Me.tabSystem.Controls.Add(Me.txtTotalPhysicalMemory)
        Me.tabSystem.Location = New System.Drawing.Point(4, 22)
        Me.tabSystem.Name = "tabSystem"
        Me.tabSystem.Size = New System.Drawing.Size(512, 190)
        Me.tabSystem.TabIndex = 5
        Me.tabSystem.Text = "System"
        '
        'lblProcessorType
        '
        Me.lblProcessorType.Location = New System.Drawing.Point(8, 152)
        Me.lblProcessorType.Name = "lblProcessorType"
        Me.lblProcessorType.Size = New System.Drawing.Size(136, 23)
        Me.lblProcessorType.TabIndex = 13
        Me.lblProcessorType.Text = "Processor Type"
        Me.lblProcessorType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDomainName
        '
        Me.lblDomainName.Location = New System.Drawing.Point(8, 56)
        Me.lblDomainName.Name = "lblDomainName"
        Me.lblDomainName.Size = New System.Drawing.Size(136, 23)
        Me.lblDomainName.TabIndex = 12
        Me.lblDomainName.Text = "Domain Name"
        Me.lblDomainName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMachineName
        '
        Me.lblMachineName.Location = New System.Drawing.Point(8, 32)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.Size = New System.Drawing.Size(136, 23)
        Me.lblMachineName.TabIndex = 11
        Me.lblMachineName.Text = "Machine Name"
        Me.lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOperatingSystemVersion
        '
        Me.lblOperatingSystemVersion.Location = New System.Drawing.Point(8, 128)
        Me.lblOperatingSystemVersion.Name = "lblOperatingSystemVersion"
        Me.lblOperatingSystemVersion.Size = New System.Drawing.Size(136, 23)
        Me.lblOperatingSystemVersion.TabIndex = 10
        Me.lblOperatingSystemVersion.Text = "Operating System Version"
        Me.lblOperatingSystemVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblWindowsUserName
        '
        Me.lblWindowsUserName.Location = New System.Drawing.Point(16, 8)
        Me.lblWindowsUserName.Name = "lblWindowsUserName"
        Me.lblWindowsUserName.Size = New System.Drawing.Size(128, 23)
        Me.lblWindowsUserName.TabIndex = 9
        Me.lblWindowsUserName.Text = "Windows User Name"
        Me.lblWindowsUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtProcessorType
        '
        Me.txtProcessorType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProcessorType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProcessorType.Location = New System.Drawing.Point(152, 152)
        Me.txtProcessorType.Name = "txtProcessorType"
        Me.txtProcessorType.ReadOnly = True
        Me.txtProcessorType.Size = New System.Drawing.Size(352, 21)
        Me.txtProcessorType.TabIndex = 8
        Me.txtProcessorType.TabStop = False
        Me.txtProcessorType.Text = ""
        '
        'txtDomainName
        '
        Me.txtDomainName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDomainName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDomainName.Location = New System.Drawing.Point(152, 56)
        Me.txtDomainName.Name = "txtDomainName"
        Me.txtDomainName.ReadOnly = True
        Me.txtDomainName.Size = New System.Drawing.Size(352, 21)
        Me.txtDomainName.TabIndex = 7
        Me.txtDomainName.TabStop = False
        '
        'txtMachineName
        '
        Me.txtMachineName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMachineName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineName.Location = New System.Drawing.Point(152, 32)
        Me.txtMachineName.Name = "txtMachineName"
        Me.txtMachineName.ReadOnly = True
        Me.txtMachineName.Size = New System.Drawing.Size(352, 21)
        Me.txtMachineName.TabIndex = 6
        Me.txtMachineName.TabStop = False
        '
        'txtOperatingSystemVersion
        '
        Me.txtOperatingSystemVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOperatingSystemVersion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOperatingSystemVersion.Location = New System.Drawing.Point(152, 128)
        Me.txtOperatingSystemVersion.Name = "txtOperatingSystemVersion"
        Me.txtOperatingSystemVersion.ReadOnly = True
        Me.txtOperatingSystemVersion.Size = New System.Drawing.Size(352, 21)
        Me.txtOperatingSystemVersion.TabIndex = 5
        Me.txtOperatingSystemVersion.TabStop = False
        '
        'txtWindowsUserName
        '
        Me.txtWindowsUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWindowsUserName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWindowsUserName.Location = New System.Drawing.Point(152, 8)
        Me.txtWindowsUserName.Name = "txtWindowsUserName"
        Me.txtWindowsUserName.ReadOnly = True
        Me.txtWindowsUserName.Size = New System.Drawing.Size(352, 21)
        Me.txtWindowsUserName.TabIndex = 4
        Me.txtWindowsUserName.TabStop = False
        '
        'lblAvailablePhysicalMemory
        '
        Me.lblAvailablePhysicalMemory.Location = New System.Drawing.Point(8, 104)
        Me.lblAvailablePhysicalMemory.Name = "lblAvailablePhysicalMemory"
        Me.lblAvailablePhysicalMemory.Size = New System.Drawing.Size(136, 23)
        Me.lblAvailablePhysicalMemory.TabIndex = 3
        Me.lblAvailablePhysicalMemory.Text = "Available Physical Memory"
        Me.lblAvailablePhysicalMemory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAvailablePhysicalMemory
        '
        Me.txtAvailablePhysicalMemory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAvailablePhysicalMemory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAvailablePhysicalMemory.Location = New System.Drawing.Point(152, 104)
        Me.txtAvailablePhysicalMemory.Name = "txtAvailablePhysicalMemory"
        Me.txtAvailablePhysicalMemory.ReadOnly = True
        Me.txtAvailablePhysicalMemory.Size = New System.Drawing.Size(352, 21)
        Me.txtAvailablePhysicalMemory.TabIndex = 2
        Me.txtAvailablePhysicalMemory.TabStop = False
        '
        'lblTotalPhysicalMemory
        '
        Me.lblTotalPhysicalMemory.Location = New System.Drawing.Point(16, 80)
        Me.lblTotalPhysicalMemory.Name = "lblTotalPhysicalMemory"
        Me.lblTotalPhysicalMemory.Size = New System.Drawing.Size(128, 23)
        Me.lblTotalPhysicalMemory.TabIndex = 1
        Me.lblTotalPhysicalMemory.Text = "Total Physical Memory"
        Me.lblTotalPhysicalMemory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotalPhysicalMemory
        '
        Me.txtTotalPhysicalMemory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalPhysicalMemory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalPhysicalMemory.Location = New System.Drawing.Point(152, 80)
        Me.txtTotalPhysicalMemory.Name = "txtTotalPhysicalMemory"
        Me.txtTotalPhysicalMemory.ReadOnly = True
        Me.txtTotalPhysicalMemory.Size = New System.Drawing.Size(352, 21)
        Me.txtTotalPhysicalMemory.TabIndex = 0
        Me.txtTotalPhysicalMemory.TabStop = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(448, 152)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 24)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.FormToolTip.SetToolTip(Me.btnOK, "Close exception dialog")
        '
        'lblError
        '
        Me.lblError.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblError.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblError.Location = New System.Drawing.Point(8, 9)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(184, 16)
        Me.lblError.TabIndex = 22
        Me.lblError.Text = "The following exception has occurred"
        '
        'IconImageList
        '
        Me.IconImageList.ImageStream = CType(resources.GetObject("IconImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IconImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.IconImageList.Images.SetKeyName(0, "")
        Me.IconImageList.Images.SetKeyName(1, "")
        Me.IconImageList.Images.SetKeyName(2, "")
        Me.IconImageList.Images.SetKeyName(3, "")
        Me.IconImageList.Images.SetKeyName(4, "")
        Me.IconImageList.Images.SetKeyName(5, "")
        Me.IconImageList.Images.SetKeyName(6, "")
        Me.IconImageList.Images.SetKeyName(7, "")
        Me.IconImageList.Images.SetKeyName(8, "")
        Me.IconImageList.Images.SetKeyName(9, "")
        Me.IconImageList.Images.SetKeyName(10, "")
        Me.IconImageList.Images.SetKeyName(11, "")
        Me.IconImageList.Images.SetKeyName(12, "")
        Me.IconImageList.Images.SetKeyName(13, "")
        Me.IconImageList.Images.SetKeyName(14, "")
        Me.IconImageList.Images.SetKeyName(15, "")
        Me.IconImageList.Images.SetKeyName(16, "")
        Me.IconImageList.Images.SetKeyName(17, "")
        Me.IconImageList.Images.SetKeyName(18, "")
        Me.IconImageList.Images.SetKeyName(19, "")
        Me.IconImageList.Images.SetKeyName(20, "")
        Me.IconImageList.Images.SetKeyName(21, "")
        Me.IconImageList.Images.SetKeyName(22, "")
        Me.IconImageList.Images.SetKeyName(23, "")
        Me.IconImageList.Images.SetKeyName(24, "")
        Me.IconImageList.Images.SetKeyName(25, "")
        Me.IconImageList.Images.SetKeyName(26, "")
        Me.IconImageList.Images.SetKeyName(27, "")
        Me.IconImageList.Images.SetKeyName(28, "")
        Me.IconImageList.Images.SetKeyName(29, "")
        Me.IconImageList.Images.SetKeyName(30, "")
        Me.IconImageList.Images.SetKeyName(31, "")
        Me.IconImageList.Images.SetKeyName(32, "")
        Me.IconImageList.Images.SetKeyName(33, "")
        Me.IconImageList.Images.SetKeyName(34, "")
        Me.IconImageList.Images.SetKeyName(35, "")
        Me.IconImageList.Images.SetKeyName(36, "")
        Me.IconImageList.Images.SetKeyName(37, "")
        Me.IconImageList.Images.SetKeyName(38, "")
        Me.IconImageList.Images.SetKeyName(39, "")
        Me.IconImageList.Images.SetKeyName(40, "")
        Me.IconImageList.Images.SetKeyName(41, "")
        Me.IconImageList.Images.SetKeyName(42, "")
        Me.IconImageList.Images.SetKeyName(43, "")
        Me.IconImageList.Images.SetKeyName(44, "")
        Me.IconImageList.Images.SetKeyName(45, "")
        Me.IconImageList.Images.SetKeyName(46, "")
        Me.IconImageList.Images.SetKeyName(47, "")
        Me.IconImageList.Images.SetKeyName(48, "")
        Me.IconImageList.Images.SetKeyName(49, "")
        Me.IconImageList.Images.SetKeyName(50, "")
        Me.IconImageList.Images.SetKeyName(51, "")
        Me.IconImageList.Images.SetKeyName(52, "")
        Me.IconImageList.Images.SetKeyName(53, "")
        Me.IconImageList.Images.SetKeyName(54, "")
        Me.IconImageList.Images.SetKeyName(55, "")
        Me.IconImageList.Images.SetKeyName(56, "")
        Me.IconImageList.Images.SetKeyName(57, "")
        Me.IconImageList.Images.SetKeyName(58, "")
        Me.IconImageList.Images.SetKeyName(59, "")
        Me.IconImageList.Images.SetKeyName(60, "")
        '
        'IconListView
        '
        Me.IconListView.BackColor = System.Drawing.SystemColors.Control
        Me.IconListView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.IconListView.Enabled = False
        Me.IconListView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.IconListView.LargeImageList = Me.IconImageList
        Me.IconListView.Location = New System.Drawing.Point(8, 32)
        Me.IconListView.MultiSelect = False
        Me.IconListView.Name = "IconListView"
        Me.IconListView.Scrollable = False
        Me.IconListView.Size = New System.Drawing.Size(80, 112)
        Me.IconListView.TabIndex = 25
        Me.IconListView.TabStop = False
        Me.IconListView.UseCompatibleStateImageBehavior = False
        '
        'txtUserFriendlyMessage
        '
        Me.txtUserFriendlyMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserFriendlyMessage.BackColor = System.Drawing.SystemColors.Control
        Me.txtUserFriendlyMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUserFriendlyMessage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserFriendlyMessage.Location = New System.Drawing.Point(88, 32)
        Me.txtUserFriendlyMessage.Multiline = True
        Me.txtUserFriendlyMessage.Name = "txtUserFriendlyMessage"
        Me.txtUserFriendlyMessage.ReadOnly = True
        Me.txtUserFriendlyMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtUserFriendlyMessage.Size = New System.Drawing.Size(440, 112)
        Me.txtUserFriendlyMessage.TabIndex = 26
        Me.txtUserFriendlyMessage.TabStop = False
        Me.FormToolTip.SetToolTip(Me.txtUserFriendlyMessage, "User exception message")
        '
        'btnDetails
        '
        Me.btnDetails.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDetails.ImageIndex = 0
        Me.btnDetails.ImageList = Me.ButtonImageList
        Me.btnDetails.Location = New System.Drawing.Point(8, 152)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(80, 24)
        Me.btnDetails.TabIndex = 1
        Me.btnDetails.Text = "&Details"
        Me.FormToolTip.SetToolTip(Me.btnDetails, "Expand to see additional information")
        '
        'ButtonImageList
        '
        Me.ButtonImageList.ImageStream = CType(resources.GetObject("ButtonImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonImageList.Images.SetKeyName(0, "")
        Me.ButtonImageList.Images.SetKeyName(1, "")
        '
        'btnCopy
        '
        Me.btnCopy.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Location = New System.Drawing.Point(96, 152)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(80, 24)
        Me.btnCopy.TabIndex = 2
        Me.btnCopy.Text = "&Copy"
        Me.FormToolTip.SetToolTip(Me.btnCopy, "Copy exception information to clipboard")
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(184, 152)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(80, 24)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "&Print"
        Me.FormToolTip.SetToolTip(Me.btnPrint, "Print exception information")
        '
        'ThePrintDocument
        '
        '
        'StackTrace
        '
        Me.StackTrace.AcceptsReturn = True
        Me.StackTrace.AcceptsTab = True
        Me.StackTrace.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StackTrace.Location = New System.Drawing.Point(0, 0)
        Me.StackTrace.MaxLength = 0
        Me.StackTrace.Multiline = True
        Me.StackTrace.Name = "StackTrace"
        Me.StackTrace.ReadOnly = True
        Me.StackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.StackTrace.Size = New System.Drawing.Size(512, 190)
        Me.StackTrace.TabIndex = 0
        Me.StackTrace.WordWrap = False
        '
        'ErrorForm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(536, 406)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.txtUserFriendlyMessage)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.IconListView)
        Me.Controls.Add(Me.txtExceptionTypeValue)
        Me.Controls.Add(Me.tabInfo)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblError)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(544, 210)
        Me.Name = "ErrorForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Exception Information"
        Me.TopMost = True
        Me.tabInfo.ResumeLayout(False)
        Me.tabGeneral.ResumeLayout(False)
        Me.tabGeneral.PerformLayout()
        Me.tabStack.ResumeLayout(False)
        Me.tabStack.PerformLayout()
        Me.tabInner.ResumeLayout(False)
        Me.tabTip.ResumeLayout(False)
        Me.tabTip.PerformLayout()
        Me.tabSpecific.ResumeLayout(False)
        Me.tabSystem.ResumeLayout(False)
        Me.tabSystem.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Shared WithCulture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentUICulture
    Public Shared WithoutCulture As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

    Private m_TargetEx As Exception
    Private m_hasGeneralBeenCalled As Boolean
    Private m_hasInnerExceptionBeenCalled As Boolean
    Private m_hasStackTraceBeenCalled As Boolean
    Private m_hasOtherInformationBeenCalled As Boolean
    Private m_hasExtraInformationBeenCalled As Boolean
    Private m_hasExtraCollectionBeenCalled As Boolean
    Private m_enableVisualStyles As Boolean
    Private m_Tip As String
    Private m_Icon As Icons
    Private m_UserFriendlyMessage As String
    Private m_ExtraInformation() As String
    Private m_ExtraCollection As Collections.Specialized.NameValueCollection
    Private m_TimeStamp As String = Now.ToString("F")
    Private m_WaveFilePath As String = String.Empty
    Private m_TotalPhysicalMemory As ULong
    Private m_AvailablePhysicalMemory As ULong
    Private m_WindowsUserName As String
    Private m_OperatingSystemVersion As String
    Private m_MachineName As String
    Private m_DomainName As String
    Private m_ProcessorType As String

    Private myreader As System.IO.StringReader
    Private bolPrintMore As Boolean
    Private line As String = Nothing

    Const MESSAGE_HEIGHT As Integer = 210

    Public Property ProcessorType() As String
        Get
            Return m_ProcessorType
        End Get
        Set(ByVal Value As String)
            m_ProcessorType = Value
        End Set
    End Property

    Public Property OperatingSystemVersion() As String
        Get
            Return m_OperatingSystemVersion
        End Get
        Set(ByVal Value As String)
            m_OperatingSystemVersion = Value
        End Set
    End Property

    Public Property DomainName() As String
        Get
            Return m_DomainName
        End Get
        Set(ByVal Value As String)
            m_DomainName = Value
        End Set
    End Property

    Public Property MachineName() As String
        Get
            Return m_MachineName
        End Get
        Set(ByVal Value As String)
            m_MachineName = Value
        End Set
    End Property

    Public Property WindowsUserName() As String
        Get
            Return m_WindowsUserName
        End Get
        Set(ByVal Value As String)
            m_WindowsUserName = Value
        End Set
    End Property

    Public Property AvailablePhysicalMemory() As ULong
        Get
            Return m_AvailablePhysicalMemory
        End Get
        Set(ByVal Value As ULong)
            m_AvailablePhysicalMemory = Value
        End Set
    End Property

    Public Property TotalPhysicalMemory() As ULong
        Get
            Return m_TotalPhysicalMemory
        End Get
        Set(ByVal Value As ULong)
            m_TotalPhysicalMemory = Value
        End Set
    End Property

    Public Property ExtraCollection() As Collections.Specialized.NameValueCollection
        Get
            Return m_ExtraCollection
        End Get
        Set(ByVal Value As Collections.Specialized.NameValueCollection)
            m_ExtraCollection = Value
        End Set
    End Property

    Public Property ExtraInformation() As String()
        Get
            Return m_ExtraInformation
        End Get
        Set(ByVal Value As String())
            m_ExtraInformation = Value
        End Set
    End Property

    Public Property UserFriendlyMessage() As String
        Get
            Return m_UserFriendlyMessage
        End Get
        Set(ByVal Value As String)
            m_UserFriendlyMessage = Value
        End Set
    End Property

    Public Property Tip() As String
        Get
            Return m_Tip
        End Get
        Set(ByVal Value As String)
            m_Tip = Value
        End Set
    End Property

    Public WriteOnly Property DisplayIcon() As Icons
        Set(ByVal Value As Icons)
            m_Icon = Value
        End Set
    End Property

    Public WriteOnly Property WaveFilePath() As String
        Set(ByVal Value As String)
            m_WaveFilePath = Value
        End Set
    End Property

    Public Sub New(ByVal targetException As Exception, ByVal enableVisualStyles As Boolean)
        InitializeComponent()
        m_enableVisualStyles = enableVisualStyles
        m_TargetEx = targetException
    End Sub

    Public Sub New(ByVal targetException As Exception)
        InitializeComponent()
        m_TargetEx = targetException
    End Sub

    Private Sub ErrorForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If m_enableVisualStyles AndAlso Application.RenderWithVisualStyles Then
            ' Just cheat and set to white, since most of the tab is covered anyway.
            tabGeneral.BackColor = Color.White
            tabInfo.BackColor = Color.White
            tabInner.BackColor = Color.White
            tabSpecific.BackColor = Color.White
            tabStack.BackColor = Color.White
            tabTip.BackColor = Color.White
            tabSystem.BackColor = Color.White
        End If

        If Not m_TargetEx Is Nothing Then
            txtExceptionTypeValue.Text = m_TargetEx.GetType().FullName
            Me.StackTrace.Text = m_TargetEx.StackTrace
            DisplayGeneralInformation()
        Else
            tabInfo.Enabled = False
            txtExceptionTypeValue.Text = "Unknown"
        End If
        SetIcon()
        SetUserFriendlyMessage()
        Me.Height = MESSAGE_HEIGHT
        Me.MaximumSize = New Drawing.Size(1600, MESSAGE_HEIGHT)

        Try
            If Not m_WaveFilePath = String.Empty Then
                My.Computer.Audio.Play(m_WaveFilePath, AudioPlayMode.Background)
            End If
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(ex.Message & vbCrLf & _
            vbCrLf & _
            ex.GetType.FullName & vbCrLf & _
            vbCrLf & _
            "Location:" & ex.StackTrace, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code

        End Try
        Me.TopMost = True
        Me.BringToFront()
    End Sub

    Private Shared Function formatBytes(ByVal value As ULong) As String
        Const KiB As ULong = 1024
        Const MiB As ULong = KiB * KiB
        Const GiB As ULong = MiB * KiB
        Const TiB As ULong = GiB * KiB
        If value < KiB Then
            Return value.ToString("0 Bytes", WithoutCulture)
        ElseIf value < MiB Then
            Return (value / KiB).ToString("0.## KiB", WithoutCulture)
        ElseIf value < GiB Then
            Return (value / MiB).ToString("0.## MiB", WithoutCulture)
        ElseIf value < tib Then
            Return (value / GiB).ToString("0.## GiB", WithoutCulture)
        Else
            Return (value / TiB).ToString("0.## TiB", WithoutCulture)
        End If
    End Function

    Private Sub SetExtraCollection()
        Try
            If False = m_hasExtraCollectionBeenCalled Then
                If Not m_ExtraCollection Is Nothing Then

                    lstOtherInfo.BeginUpdate()

                    Dim lvi As ListViewItem

                    For Each key As String In m_ExtraCollection.Keys

                        lvi = New ListViewItem(key)
                        lvi.SubItems.Add(m_ExtraCollection(key))

                        lstOtherInfo.Items.Add(lvi)
                    Next

                    lstOtherInfo.EndUpdate()
                End If
            End If
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(ex.Message & vbCrLf & _
            vbCrLf & _
            ex.GetType.FullName & vbCrLf & _
            vbCrLf & _
            "Location:" & ex.StackTrace, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code
            m_hasExtraCollectionBeenCalled = True
        End Try
    End Sub

    Private Sub SetExtraInformation()
        Try
            If False = m_hasExtraInformationBeenCalled Then
                If Not m_ExtraInformation Is Nothing Then
                    lstOtherInfo.BeginUpdate()

                    Dim lvi As ListViewItem

                    For i As Integer = 0 To m_ExtraInformation.GetUpperBound(0)
                        lvi = New ListViewItem("")
                        lvi.SubItems.Add(m_ExtraInformation(i))

                        lstOtherInfo.Items.Add(lvi)
                    Next

                    lstOtherInfo.EndUpdate()
                End If
            End If
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(String.Format("{0}{1}{1}{2}{1}{1}Stack:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace), ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code
            m_hasExtraInformationBeenCalled = True
        End Try
    End Sub

    Private Sub SetUserFriendlyMessage()
        Try
            If m_UserFriendlyMessage = String.Empty Then
                Me.txtUserFriendlyMessage.Text = m_TargetEx.Message
            Else
                Me.txtUserFriendlyMessage.Text = m_UserFriendlyMessage
            End If
            If m_Icon = Icons.None Or Me.IconListView.Items(0).ImageIndex = Icons.None Then
                Me.txtUserFriendlyMessage.Location = New System.Drawing.Point(8, 32)
                Me.txtUserFriendlyMessage.Size = New System.Drawing.Size(520, 112)
            End If
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(String.Format("{0}{1}{1}{2}{1}{1}Stack:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace), ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code

        End Try
    End Sub

    Private Sub SetIcon()
        Try
            If m_Icon = Icons.Random Then
                Dim Icon As Icons
                Dim imax As Integer = -2

                Do
                    imax += 1
                Loop While [Enum].IsDefined(GetType(Icons), imax)
                imax -= 1

                Randomize()
                Icon = CType(Rnd() * imax, Icons)

                If Not [Enum].IsDefined(GetType(Icons), Icon) Then
                    Icon = Icons.None
                End If

                Me.IconListView.Items(0).ImageIndex = Icon
            Else
                Me.IconListView.Items(0).ImageIndex = m_Icon
            End If
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(ex.Message & vbCrLf & _
            vbCrLf & _
            ex.GetType.FullName & vbCrLf & _
            vbCrLf & _
            "Location:" & ex.StackTrace, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code

        End Try
    End Sub

    Private Sub DisplayGeneralInformation()
        If Not m_TargetEx Is Nothing AndAlso m_hasGeneralBeenCalled = False Then
            txtExceptionMessageValue.Text = m_TargetEx.Message
            txtExceptionSourceValue.Text = m_TargetEx.Source
            txtExceptionTargetMethodValue.Text = GetTargetMethodFormat(m_TargetEx)
            txtHelpLinkValue.Text = m_TargetEx.HelpLink
            m_hasGeneralBeenCalled = True
        End If
    End Sub

    Private Shared Function GetTargetMethodFormat(ByVal Ex As Exception) As String
        Return "[" & Ex.TargetSite.DeclaringType.Assembly.GetName().Name & "]" & Ex.TargetSite.DeclaringType.ToString & "::" & Ex.TargetSite.Name & "()"
    End Function

    Private Shared Function GetCustomExceptionInfo(ByVal Ex As Exception) As Hashtable

        Dim customInfo As New Hashtable

        Dim pi As PropertyInfo
        For Each pi In Ex.GetType().GetProperties()
            Dim baseEx As Type = GetType(System.Exception)
            If baseEx.GetProperty(pi.Name) Is Nothing Then
                customInfo.Add(pi.Name, pi.GetValue(Ex, Nothing))
            End If
        Next

        Return customInfo

    End Function

    Private Sub DisplayInnerExceptionTrace()

        If False = m_hasInnerExceptionBeenCalled Then
            If m_TargetEx.InnerException IsNot Nothing Then
                Dim innerEx As Exception = m_TargetEx.InnerException
                Dim parentNode As TreeNode = Nothing
                Dim childNode As TreeNode = Nothing
                Dim childMessage As TreeNode = Nothing
                Dim childTarget As TreeNode = Nothing

                trvInnerExceptions.BeginUpdate()

                While Not innerEx Is Nothing
                    childNode = New TreeNode(innerEx.GetType().FullName)
                    childNode.Tag = innerEx
                    '  For now, add just the message.
                    'childMessage = New TreeNode(innerEx.Message)
                    'childTarget = New TreeNode(GetTargetMethodFormat(innerEx))

                    'childNode.Nodes.Add(childMessage)
                    'childNode.Nodes.Add(childTarget)


                    If parentNode IsNot Nothing Then
                        parentNode.Nodes.Add(childNode)
                    Else
                        trvInnerExceptions.Nodes.Add(childNode)
                    End If

                    parentNode = childNode
                    innerEx = innerEx.InnerException
                End While

                trvInnerExceptions.EndUpdate()
            Else
                trvInnerExceptions.Nodes.Add("No inner exceptions to report")
            End If

            m_hasInnerExceptionBeenCalled = True
        End If

    End Sub

    Private Sub DisplayStackTrace()
        If False = m_hasStackTraceBeenCalled Then
            Me.StackTrace.Text = m_TargetEx.StackTrace
            'Dim stack As System.Diagnostics.StackTrace = New StackTrace(2) ' skip me.

            'Dim stackTrace As String() = m_TargetEx.StackTrace.Split(New Char() {Chr(10)})

            'Dim st As String

            'For Each st In stackTrace
            '    lstStackTrace.Items.Add(New ListViewItem(st))
            'Next st

            m_hasStackTraceBeenCalled = True

        End If

    End Sub

    Private Sub SetSystemInfomation()
        Try
            Me.txtWindowsUserName.Text = m_WindowsUserName
            Me.txtTotalPhysicalMemory.Text = formatBytes(m_TotalPhysicalMemory)
            Me.txtAvailablePhysicalMemory.Text = formatBytes(m_AvailablePhysicalMemory)
            Me.txtOperatingSystemVersion.Text = m_OperatingSystemVersion
            Me.txtMachineName.Text = m_MachineName
            Me.txtDomainName.Text = m_DomainName
            Me.txtProcessorType.Text = m_ProcessorType
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & _
            vbCrLf & _
            ex.GetType.FullName & vbCrLf & _
            vbCrLf & _
            "Location:" & ex.StackTrace, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code

        End Try
    End Sub

    Private Sub DisplayOtherInformation()

        If False = m_hasOtherInformationBeenCalled Then

            Dim ht As Hashtable = GetCustomExceptionInfo(m_TargetEx)
            Dim ide As IDictionaryEnumerator = ht.GetEnumerator()

            lstOtherInfo.Items.Clear()
            lstOtherInfo.BeginUpdate()

            Dim lvi As ListViewItem

            While ide.MoveNext()
                lvi = New ListViewItem(ide.Key.ToString())
                If Not ide.Value Is Nothing Then
                    lvi.SubItems.Add(ide.Value.ToString())
                End If
                lstOtherInfo.Items.Add(lvi)
            End While

            lstOtherInfo.EndUpdate()
            m_hasOtherInformationBeenCalled = True
        End If

    End Sub

    Private Sub DisplayTip()
        Me.txtTip.Text = m_Tip
    End Sub

    Private Sub DisplayTabInfo()
        Select Case tabInfo.SelectedIndex
            Case 0 ' General info
                DisplayGeneralInformation()
            Case 1 '  Stack trace info.
                'DisplayStackTrace()
            Case 2 '  Inner exception info.
                DisplayInnerExceptionTrace()
            Case 3 'Tip 
                DisplayTip()
            Case 4 '  Other information.
                DisplayOtherInformation()
                SetExtraInformation()
                SetExtraCollection()
            Case 5 '  System Info.
                SetSystemInfomation()
            Case Else
        End Select
    End Sub

    Private Sub tabInfo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabInfo.SelectedIndexChanged
        DisplayTabInfo()
    End Sub

    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetails.Click
        Try
            If Me.btnDetails.ImageIndex = 0 Then
                Me.btnDetails.ImageIndex = 1
                Me.MaximumSize = New Drawing.Size(1600, 1600)
                Me.Height = MESSAGE_HEIGHT * 2
            Else
                Me.btnDetails.ImageIndex = 0
                Me.MaximumSize = New Drawing.Size(1600, MESSAGE_HEIGHT)
                Me.Height = MESSAGE_HEIGHT
            End If

        Catch ex As Exception 'Catch all exceptions
            MessageBox.Show(String.Format("{0}{1}{1}{2}{1}{1}StackTrace:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace), ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'Clean up Code

        End Try
    End Sub

    Private Function GetExceptionString() As String
        Dim response As String = ""
        Try
            Dim sbCopyInformation As New System.Text.StringBuilder
            Dim stackTrace As String() = m_TargetEx.StackTrace.Split(New Char() {Chr(10), Chr(13)})
            Dim innerEx As Exception = m_TargetEx
            Dim OtherInformationString As String = OtherInfomationToString()
            Dim ExtraInformationString As String = ExtraInfomationToString()
            Dim ExtraCollectionString As String = ExtraCollectionToString()

            sbCopyInformation.AppendFormat("{0}{1}", Me.Text, vbNewLine)
            sbCopyInformation.AppendFormat("{0}{1}", m_TimeStamp, vbNewLine)
            sbCopyInformation.AppendFormat("{0}{1}{2}", m_TargetEx.GetType().FullName, vbNewLine, vbNewLine)

            If m_UserFriendlyMessage Is Nothing Then
                sbCopyInformation.AppendFormat("{0}: {1}{2}", "Message", m_TargetEx.Message, vbNewLine)
            Else
                sbCopyInformation.AppendFormat("{0}{1}{2}", m_UserFriendlyMessage, vbNewLine, vbNewLine)
                sbCopyInformation.AppendFormat("{0}: {1}{2}", "Message", m_TargetEx.Message, vbNewLine)
            End If

            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Source", m_TargetEx.Source, vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Target Method", GetTargetMethodFormat(m_TargetEx), vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}{3}", "Help Link", m_TargetEx.HelpLink, vbNewLine, vbNewLine)

            For Each st As String In stackTrace
                If st.Trim <> String.Empty Then
                    sbCopyInformation.AppendFormat("{0}: {1}{2}", "Stack Trace", st, vbNewLine)
                End If
            Next st

            If Not innerEx Is Nothing Then
                sbCopyInformation.Append(vbNewLine)
                sbCopyInformation.AppendFormat("{0}:{1}", "Inner Exception Trace", vbNewLine)
                Dim level As Integer = 0
                While Not innerEx Is Nothing
                    Dim indent As String = New String(" "c, level * 4)

                    sbCopyInformation.AppendFormat("{0}{1}{2}", indent, innerEx.GetType().FullName, vbNewLine)
                    sbCopyInformation.AppendFormat("{0}{1}{2}", indent, innerEx.Message, vbNewLine)
                    sbCopyInformation.AppendFormat("{0}{1}{2}", indent, GetTargetMethodFormat(innerEx), vbNewLine)

                    innerEx = innerEx.InnerException
                    level += 1
                End While
            End If

            If Not m_Tip = String.Empty Then
                sbCopyInformation.Append(vbNewLine)
                sbCopyInformation.AppendFormat("{0}:{1}", "Tip", vbNewLine)
                sbCopyInformation.AppendFormat("{0}{1}", m_Tip, vbNewLine)
            End If

            If OtherInformationString <> String.Empty Or ExtraInformationString <> String.Empty Or ExtraCollectionString <> String.Empty Then
                sbCopyInformation.Append(vbNewLine)
                sbCopyInformation.AppendFormat("{0}:{1}", "Other Information", vbNewLine)
                If OtherInformationString <> String.Empty Then
                    sbCopyInformation.Append(OtherInformationString)
                End If
                If ExtraInformationString <> String.Empty Then
                    If OtherInformationString <> String.Empty Then
                        sbCopyInformation.Append(vbNewLine)
                    End If
                    sbCopyInformation.Append(ExtraInformationString)
                End If

                If ExtraCollectionString <> String.Empty Then
                    If OtherInformationString <> String.Empty Or ExtraInformationString <> String.Empty Then
                        sbCopyInformation.Append(vbNewLine)
                    End If
                    sbCopyInformation.Append(ExtraCollectionString)
                End If

            End If

            'System Info
            If OtherInformationString <> String.Empty Or ExtraInformationString <> String.Empty Or ExtraCollectionString <> String.Empty Then
                sbCopyInformation.Append(vbNewLine)
            End If
            sbCopyInformation.Append(vbNewLine)
            sbCopyInformation.AppendFormat("{0}:{1}", "System Information", vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Windows User Name", m_WindowsUserName, vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Machine Name", m_MachineName, vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Domain Name", m_DomainName, vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Total Physical Memory", formatBytes(m_TotalPhysicalMemory), vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Available Physical Memory", formatBytes(m_AvailablePhysicalMemory), vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Operating System Version", m_OperatingSystemVersion, vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}", "Processor Type", m_ProcessorType)

            response = sbCopyInformation.ToString

        Catch ex As Exception 'Catch all exceptions
            MessageBox.Show(String.Format("{0}{1}{1}{2}{1}{1}StackTrace:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace), ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'Clean up Code
        End Try
        Return response
    End Function

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try

            Clipboard.SetText(GetExceptionString, TextDataFormat.UnicodeText)

        Catch ex As Exception 'Catch all exceptions
            MessageBox.Show(String.Format("{0}{1}{1}{2}{1}{1}StackTrace:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace), ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'Clean up Code

        End Try
    End Sub

    Private Function ExtraCollectionToString() As String
        Dim response As String = ""
        Try
            Dim sb As New System.Text.StringBuilder

            If Not m_ExtraCollection Is Nothing Then
                For Each key As String In m_ExtraCollection.Keys
                    If sb.ToString <> String.Empty Then
                        sb.Append(vbNewLine)
                    End If
                    sb.AppendFormat("{0}: {1}", key, m_ExtraCollection(key))
                Next
            End If
            response = sb.ToString

        Catch ex As Exception 'Catch all exceptions
            response = String.Format("{0}{1}{1}{2}{1}{1}StackTrace:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace)
            MessageBox.Show(response, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'Clean up Code
        End Try
        Return response
    End Function

    Private Function ExtraInfomationToString() As String
        Dim response As String = ""
        Try
            Dim sb As New System.Text.StringBuilder

            If Not m_ExtraInformation Is Nothing Then
                For i As Integer = 0 To m_ExtraInformation.GetUpperBound(0)
                    If sb.ToString <> String.Empty Then
                        sb.Append(vbNewLine)
                    End If
                    sb.Append(m_ExtraInformation(i))
                Next
            End If
            response = sb.ToString

        Catch ex As Exception 'Catch all exceptions
            response = String.Format("{0}{1}{1}{2}{1}{1}StackTrace:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace)
            MessageBox.Show(response, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'Clean up Code

        End Try
        Return response
    End Function

    Private Function OtherInfomationToString() As String
        Dim response As String = ""
        Try
            Dim ht As Hashtable = GetCustomExceptionInfo(m_TargetEx)
            Dim ide As IDictionaryEnumerator = ht.GetEnumerator()
            Dim sb As New System.Text.StringBuilder

            While ide.MoveNext()
                If sb.ToString <> String.Empty Then
                    sb.Append(vbNewLine)
                End If
                sb.AppendFormat("{0}: ", ide.Key.ToString())
                If Not ide.Value Is Nothing Then
                    sb.AppendFormat("{0}", ide.Value.ToString())
                End If
            End While

            response = sb.ToString

        Catch ex As Exception 'Catch all exceptions
            response = String.Format("{0}{1}{1}{2}{1}{1}StackTrace:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace)
            MessageBox.Show(response, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'Clean up Code
        End Try
        Return response
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintDialog.Document = ThePrintDocument
        Dim strText As String = GetExceptionString()

        myreader = New System.IO.StringReader(strText)
        If PrintDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            line = Nothing
            Me.ThePrintDocument.Print()

        End If
    End Sub

    Protected Sub ThePrintDocument_PrintPage(ByVal sender As Object, ByVal ev As System.Drawing.Printing.PrintPageEventArgs) Handles ThePrintDocument.PrintPage
        Dim linesPerPage As Single = 0
        Dim charactersPerPage As Single = 0
        Dim yPosition As Single = 0
        Dim count As Integer = 0
        Dim leftMargin As Single = ev.MarginBounds.Left
        Dim topMargin As Single = ev.MarginBounds.Top
        Dim printLine As String = Nothing
        Dim printFont As Font = Me.Font
        Dim myBrush As New SolidBrush(Color.Black)

        ' Work out the number of lines per page, using the MarginBounds.
        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)
        charactersPerPage = CSng(ev.MarginBounds.Width * 2.6 / printFont.GetHeight(ev.Graphics)) 'Approximate Calc, the 2.6 is a guess at a raitio value
        ' Iterate over the string using the StringReader, printing each line.

        While count < CInt(linesPerPage)
            If Not bolPrintMore Then
                line = myreader.ReadLine()
            End If
            If line Is Nothing Then
                Exit While
            End If

            Do
                If Len(line) > charactersPerPage Then
                    printLine = Microsoft.VisualBasic.Left(line, CInt(charactersPerPage))
                    line = Microsoft.VisualBasic.Right(line, Len(line) - Len(printLine))
                    bolPrintMore = True
                Else
                    printLine = line
                    bolPrintMore = False
                End If

                ' calculate the next line position based on
                ' the height of the font according to the printing device
                yPosition = topMargin + (count * printFont.GetHeight(ev.Graphics))
                ev.Graphics.DrawString(printLine, printFont, myBrush, leftMargin, yPosition, New StringFormat) 'Draw the next line in the rich edit control
                count += 1
                If bolPrintMore And count >= CInt(linesPerPage) Then
                    Exit While
                End If
            Loop While bolPrintMore
        End While

        ' If there are more lines, print another page. if (line != null)

        If (line <> String.Empty) Then
            ev.HasMorePages = True
        Else
            ev.HasMorePages = False
        End If

        myBrush.Dispose()
    End Sub 'ThePrintDocument_PrintPage 

    Private Sub trvInnerExceptions_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvInnerExceptions.NodeMouseDoubleClick
        If sender Is trvInnerExceptions Then
            Dim ex As Exception = TryCast(e.Node.Tag, Exception)
            If ex IsNot Nothing Then
                Using ed As New ErrorForm(ex)
                    ed.ShowDialog()
                End Using
            End If
        End If
    End Sub
End Class