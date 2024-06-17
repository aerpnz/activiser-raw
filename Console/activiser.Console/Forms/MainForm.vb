'Need to enable this but cant deal with the issues right now
Option Strict On 'Implicit casting is not allowed
Option Explicit On 'All variable have to be declared with type

Imports System.Drawing
Imports activiser.Library
Imports activiser.Library.activiserWebService

Public Class MainForm
    Private Const MODULENAME As String = "MainForm"
    Private Const RES_RequestAdminButtonTemplate As String = "RequestAdminButtonTemplate"
    Private Const RES_RequestStatusRootNodeLabel As String = "RequestStatusRootNodeLabel"
    Private Const RES_ConsultantStatusRootNodeLabel As String = "ConsultantStatusRootNodeLabel"
    Private Const STR_RequestStatusSortOrder As String = "DisplayOrder, IsNewStatus, IsInProgressStatus, IsCompleteStatus, IsCancelledStatus, RequestStatusID"
    'Private moCurrentJob As activiserCoreDataSet.JobRow ' dsMainSchema.JobsRow
    'Private moCurrentRequest As activiserCoreDataSet.RequestRow
    'Private lastRefresh As DateTime = DateTime.UtcNow
    Private _initialised As Boolean
    Public Property Initialised() As Boolean
        Get
            Return _initialised
        End Get
        Set(ByVal value As Boolean)
            _initialised = value
        End Set
    End Property


    Private WithEvents _consoleData As New ConsoleData
    Public Property ConsoleData() As ConsoleData
        Get
            Return _consoleData
        End Get
        Set(ByVal value As ConsoleData)
            _consoleData = value
        End Set
    End Property

    Friend approvalRootNode, requestStatusRootNode, consultantStatusRootNode, consultantApprovalRootNode, activiserUsersRootNode, otherUsersRootNode As TreeNode
    Friend allCompleteNode As TreeNode
    Friend allIncompleteNode As TreeNode

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If ConsoleData.InitialLoadCancelled Then Return
        My.Settings.Save()
        'Application.Exit()
        If LoggedOn Then
            My.Application.ClosedByMainForm = True
            Console.ConsoleData.StopPolling()
            Dim f As New ShutdownDialog
            f.ShowDialog()
        End If
    End Sub

    ' Code executed on load.
    Private Sub SetupJobApprovalTree()
        approvalRootNode = NavTreeJob.Nodes("approvalStatusRoot")
        approvalRootNode.Text = Terminology.GetString(Me.Name, "ApprovalStatusRootNodeLabel")

        Dim nodeTagCollection As Collections.Specialized.StringCollection
        If ConsoleUser.Management AndAlso ConsoleUser.Administration Then
            nodeTagCollection = My.Settings.JobTreeUserNodes
        ElseIf ConsoleUser.Management Then
            nodeTagCollection = My.Settings.JobTreeManagementNodes
        ElseIf ConsoleUser.Administration Then
            nodeTagCollection = My.Settings.JobTreeAdminNodes
        Else
            nodeTagCollection = My.Settings.JobTreeUserNodes
        End If

        If Not NavTreeJob Is Nothing Then
            approvalRootNode.Nodes.Clear()
            For Each nodeTagString As String In nodeTagCollection
                Try
                    Dim nodeTag As New ApprovalNodeTag(nodeTagString.Split("|"c))
                    Dim statusnode As TreeNode
                    statusnode = approvalRootNode.Nodes.Add(nodeTag.Name, String.Format(nodeTag.LabelTemplate, 0))
                    statusnode.ToolTipText = nodeTag.ToolTip
                    statusnode.ForeColor = nodeTag.Color
                    statusnode.Tag = nodeTag
                    If nodeTag.Name = "AllComplete" Then Me.allCompleteNode = statusnode
                    If nodeTag.Name = "Incomplete" Then Me.allIncompleteNode = statusnode
                Catch ex As Exception
                    MessageBox.Show(String.Format(My.Resources.MainFormJobApprovalTreeLoadFailureMessageTemplate, ex.ToString), My.Resources.activiserFormTitle)
                    Throw
                End Try
            Next
        End If
    End Sub


    Public Sub UpdateApprovalTree()
        If Me.InvokeRequired Then
            Dim d As New UpdateTreeDelegate(AddressOf UpdateApprovalTree)
            Me.Invoke(d)
            Return
        End If
        Try
            Dim jobs() As DataRow
            Dim records As Integer
            Dim rsNode As TreeNode

            For Each rsNode In approvalRootNode.Nodes
                Dim nodeTag As ApprovalNodeTag = CType(rsNode.Tag, ApprovalNodeTag)
                jobs = Me.CoreDataSet.Job.Select(nodeTag.Filter)
                If jobs IsNot Nothing Then
                    records = jobs.Length
                Else
                    records = 0
                End If
                rsNode.Text = String.Format(nodeTag.LabelTemplate, records)
            Next
        Catch ex As Exception
            TraceError("Error updating Approval tree: {0}", ex.ToString())
        End Try
    End Sub

    Private Sub SetupConsultantTrees()
        activiserUsersRootNode = Me.NavTreeConsultant.Nodes("activiserUsersRootNode")
        otherUsersRootNode = Me.NavTreeConsultant.Nodes("otherUsersRootNode")
        consultantApprovalRootNode = NavTreeJob.Nodes("consultantRoot")
        consultantApprovalRootNode.Text = Terminology.GetString(Me.Name, "ConsultantRootNodeLabel")

        Dim cnode, jnode As TreeNode

        For Each drConsultant As activiserDataSet.ConsultantRow In _
                Console.ConsoleData.CoreDataSet.Consultant.Select("", "Name") ' Utility.ActiveConsultantsRow In cdt  ' console.ConsoleData Consultant.Select("Engineer=True AND Employee=True", "Name ASC")
            cnode = New TreeNode
            cnode.Text = drConsultant.Name
            cnode.Tag = drConsultant.ConsultantUID
            If drConsultant.IsActiviserUser Then
                jnode = New TreeNode
                jnode.Text = drConsultant.Name
                jnode.Tag = drConsultant.ConsultantUID
                consultantApprovalRootNode.Nodes.Add(jnode)
                activiserUsersRootNode.Nodes.Add(cnode)
            Else
                otherUsersRootNode.Nodes.Add(cnode)
            End If
        Next
        jnode = New TreeNode
        jnode.Text = "All"
        jnode.Tag = ""
        consultantApprovalRootNode.Nodes.Add(jnode)
        UpdateConsultantTrees()

        NavTreeConsultant.ExpandAll()
        consultantApprovalRootNode.ExpandAll()
    End Sub

    Private Shared Function getConsultantNode(ByVal consultantUid As Guid, ByVal base As TreeNode) As TreeNode
        For Each node As TreeNode In base.Nodes
            If TypeOf node.Tag Is Guid Then
                If CType(node.Tag, Guid) = consultantUid Then
                    Return node
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Sub UpdateConsultantTrees()
        If Me.InvokeRequired Then
            Dim d As New UpdateTreeDelegate(AddressOf UpdateConsultantTrees)
            Me.Invoke(d)
            Return
        End If
        Try
            Dim cr As activiserDataSet.ConsultantRow
            For Each cr In Console.ConsoleData.CoreDataSet.Consultant
                If cr.IsActiviserUser Then
                    Dim jnode, cnode As TreeNode

                    jnode = getConsultantNode(cr.ConsultantUID, consultantApprovalRootNode)
                    cnode = getConsultantNode(cr.ConsultantUID, activiserUsersRootNode)

                    Dim dtSync As DateTime
                    If cr.IsLastSyncTimeNull Then
                        dtSync = DateTime.MinValue
                    Else
                        dtSync = cr.LastSyncTime
                    End If
                    Dim dtCurrent As DateTime = DateTime.UtcNow

                    If (Not cr.IsMinSyncTimeNull AndAlso cr.MinSyncTime > 0) Then
                        If (dtCurrent > dtSync.AddMinutes(cr.MinSyncTime * 2)) Then ' If the engineer has missed 2 sync intervals.
                            If jnode IsNot Nothing Then jnode.ForeColor = My.Settings.SyncMissedMoreColour
                            If cnode IsNot Nothing Then cnode.ForeColor = My.Settings.SyncMissedMoreColour
                        ElseIf (dtCurrent > dtSync.AddMinutes(cr.MinSyncTime)) Then ' If the engineer has missed 1 sync interval.
                            If jnode IsNot Nothing Then jnode.ForeColor = My.Settings.SyncMissedOneColour
                            If cnode IsNot Nothing Then cnode.ForeColor = My.Settings.SyncMissedOneColour
                        Else
                            If jnode IsNot Nothing Then jnode.ForeColor = My.Settings.SyncOnTimeColour
                            If cnode IsNot Nothing Then cnode.ForeColor = My.Settings.SyncOnTimeColour
                        End If
                    ElseIf Not cr.IsMinSyncTimeNull AndAlso cr.MinSyncTime > 0 Then
                        If jnode IsNot Nothing Then jnode.ForeColor = My.Settings.SyncMissedMoreColour
                        If cnode IsNot Nothing Then cnode.ForeColor = My.Settings.SyncMissedMoreColour
                    End If
                End If
            Next

        Catch ex As Exception
            TraceError("Error updating consultant trees: {0}", ex.ToString())
        End Try
    End Sub

    Private Sub SetupRequestStatusTree()

        requestStatusRootNode = NavTreeRequest.Nodes("requestStatusRoot")
        requestStatusRootNode.Text = Terminology.GetString(Me.Name, RES_RequestStatusRootNodeLabel) & "      "
        requestStatusRootNode.Nodes.Clear()

        consultantStatusRootNode = NavTreeRequest.Nodes("consultantStatusRoot")
        Dim showConsultantTree As Boolean = My.Settings.ShowConsultantStatusTree
        If showConsultantTree Then
            consultantStatusRootNode.Text = Terminology.GetString(Me.Name, RES_ConsultantStatusRootNodeLabel) & "      "
            consultantStatusRootNode.Nodes.Clear()
        Else
            NavTreeRequest.Nodes.Remove(consultantStatusRootNode)
        End If
        Try
            Dim rsNode, crsNode As TreeNode
            ' Dim rsX As activiserDataSet.RequestStatusRow
            For Each rs As activiserDataSet.RequestStatusRow In Me.CoreDataSet.RequestStatus.Select(Nothing, STR_RequestStatusSortOrder)
                rsNode = requestStatusRootNode.Nodes.Add(String.Format("requestStatus:{0}", rs.RequestStatusID), rs.Description)
                rsNode.Tag = rs.RequestStatusID
                rsNode.ForeColor = Color.FromArgb(rs.Colour)
                rsNode.BackColor = Color.FromArgb(rs.BackColour)
                'If rsNode.ForeColor.GetBrightness > 0.75 Then
                '    rsNode.ForeColor = Color.Black
                'End If
                If showConsultantTree Then
                    crsNode = consultantStatusRootNode.Nodes.Add(String.Format("consultantStatus:{0}", rs.RequestStatusID), rs.Description)
                    crsNode.Tag = rs.RequestStatusID
                    crsNode.ForeColor = rsNode.ForeColor
                    crsNode.BackColor = rsNode.BackColor
                End If
            Next
            Me.NavTreeRequest.ExpandAll()
        Catch ex As Exception
            TraceError("Error setting up Request Status tree: {0}", ex.ToString())
        End Try
    End Sub

    Private Delegate Sub UpdateTreeDelegate()

    Public Sub UpdateRequestStatusTree()
        If Me.InvokeRequired Then
            Dim d As New UpdateTreeDelegate(AddressOf UpdateRequestStatusTree)
            Me.Invoke(d)
            Return
        End If

        Dim showConsultantTree As Boolean = My.Settings.ShowConsultantStatusTree

        requestStatusRootNode = NavTreeRequest.Nodes("requestStatusRoot")
        Try
            Dim rs As activiserDataSet.RequestStatusRow
            Dim records As Integer
            Dim rsNode As TreeNode

            For Each rsNode In requestStatusRootNode.Nodes
                rs = Me.CoreDataSet.RequestStatus.FindByRequestStatusID(CInt(rsNode.Tag))
                records = rs.GetRequestRowsByFK_Request_RequestStatus.Length
                rsNode.Text = String.Format("{0} ({1})", rs.Description, records.ToString)
                rsNode.ForeColor = Color.FromArgb(rs.Colour)
                rsNode.BackColor = Color.FromArgb(rs.BackColour)
                'If rsNode.ForeColor.GetBrightness > 0.75 Then
                '    rsNode.ForeColor = Color.Black
                'End If
            Next
            If showConsultantTree Then
                For Each rsNode In consultantStatusRootNode.Nodes
                    rs = Me.CoreDataSet.RequestStatus.FindByRequestStatusID(CInt(rsNode.Tag))
                    ' TODO: when status change confirmation working...
                    ' String.Format("ConsultantStatusID={0} AND RequestStatusID<>{0}", CStr(rs.ID))), 
                    Dim rrs() As activiserDataSet.RequestRow = _
                        CType(Me.CoreDataSet.Request.Select(String.Format("ConsultantStatusID={0} AND RequestSTatusID<>{0}", CStr(rs.RequestStatusID))),  _
                            activiserDataSet.RequestRow()) '  rs.GetRequestRowsByFK_Requests_ConsultantStatus
                    records = rrs.Length ' rs.GetRequestRowsByFK_Requests_ConsultantStatus.Length
                    rsNode.Text = String.Format("{0} ({1})", rs.Description, records.ToString)
                    rsNode.ForeColor = Color.FromArgb(rs.Colour)
                    rsNode.BackColor = Color.FromArgb(rs.BackColour)
                    'If rsNode.ForeColor.GetBrightness > 0.75 Then
                    '    rsNode.ForeColor = Color.Black
                    'End If
                Next
            End If
            RefreshMe()
            'Me.Refresh()
        Catch ex As Exception
            TraceError("Error Updating Request Status Tree: {0}", ex.ToString())
        End Try
    End Sub

    'Private _loggedOn As Boolean = False
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TraceVerbose(STR_Entered)
        If Not Logon() Then
            My.Application.ClosedByMainForm = True
            Application.Exit()
            Return
        End If

        TraceVerbose(STR_LoggedOn)
        LoadServerSettings() ' get server overrides for console settings

        Me.Refresh()
        If Not Me.DesignMode Then
            Me.ConsoleData.BeginInit()
            Console.ConsoleData.MainForm = Me
        End If

        Me.MinimumSize = Me.Size

        If ConsoleUser.Management Then
            Me.ConsultantSubForm.AccessLevel = AccessLevels.Management
            Me.ServerSettingsMenuItem.Enabled = True
        Else
            Me.ConsultantSubForm.AccessLevel = AccessLevels.Other
        End If

        Me.CoreDataSet = Console.ConsoleData.CoreDataSet

        TraceVerbose("Loading Data")
        Console.ConsoleData.LoadData()
        If ConsoleData.InitialLoadCancelled Then
            Application.Exit()
            Return
        End If

        TraceVerbose("Loading Labels")
        Terminology.LoadLabels(Me)
        ' Terminology.LoadToolTips(Me, Me.ToolTipProvider)
        Me.Refresh()

        SetupJobApprovalTree()
        SetupRequestStatusTree()
        SetupConsultantTrees()
        UpdateRequestAdminButton()

        Dim strTitle As String = My.Resources.MainFormTitleTemplate
        Me.Text = String.Format(strTitle, ConsoleUser.Name, New Uri(Console.ConsoleData.WebService.Url).Host, My.Application.Info.Version)

        If ConsoleUser.Management OrElse ConsoleUser.Administration Then
            'If the user is logged in as either Management or Administrator, the grid is filtered with NEW requests by default.
            NavTreeJob.SelectedNode = approvalRootNode.Nodes("nodeNew")
        Else
            NavTreeJob.SelectedNode = approvalRootNode.Nodes("nodeAllComplete")
        End If

        'HACK, because the JobAdminSubform loads before the console data is loaded, a whole bunch of stuff don't work.
        ' so we put the stuff that would otherwise be in the Form_Load event into an 'InitialiseForm' method.
        Me.JobAdminSubForm.InitialiseForm()
        Me.JobAdminSubForm.LoadCustomForms()

        Me.RequestAdminSubForm.InitialiseForm()
        Me.ConsultantSubForm.InitialiseForm()

        Me.JobAdminSubForm.Dock = DockStyle.Fill
        Me.RequestAdminSubForm.Dock = DockStyle.Fill
        Me.ConsultantSubForm.Dock = DockStyle.Fill

        Me.SetAutoRefreshText(False)

        If Not Me.DesignMode Then
            UpdateRequestStatusTree()
            UpdateApprovalTree()
            Me.ConsoleData.EndInit()
            Console.ConsoleData.PollInterval = My.Settings.PollTimerInterval
            If My.Settings.EnableDatabasePolling Then
                Console.ConsoleData.StartPolling()
            End If
        End If

        Me.NavTreeJob.Dock = DockStyle.Fill
        Me.NavTreeRequest.Dock = DockStyle.Fill
        Me.NavTreeConsultant.Dock = DockStyle.Fill
        NavTreeJob_AfterSelect(Me.NavTreeJob, New TreeViewEventArgs(approvalRootNode, TreeViewAction.Unknown))
        'HACK: this should not  be hard coded, but rather triggerd by something more sophisticated
        Me.NavTreeJobAdminButton.BackgroundImage = My.Resources.ButtonSelected
        Me.NavTreeJob.Focus()

        CheckLicense()
        TraceInfo("MainForm Load Complete. Activiser Console Version {0}", My.Application.Info.Version())
        Initialised = True
    End Sub

    ' Code associated with the job adminstration tab.
#Region "Job Administration"

    Private Delegate Sub FilterDelegate(ByVal itemUid As Guid)

    Public Sub FilterByEngineer(ByVal consultantUid As Guid)
        If Me.InvokeRequired Then
            Dim d As New FilterDelegate(AddressOf FilterByEngineer)
            Me.BeginInvoke(d, consultantUid)
        Else
            Try
                Me.SuspendLayout()

                If Me.consultantApprovalRootNode Is Nothing Then
                    Me.consultantApprovalRootNode = Me.NavTreeJob.Nodes("consultantRoot")
                    If Me.consultantApprovalRootNode Is Nothing Then
                        Return 'panic
                    End If
                End If
                Me.RequestAdminSubForm.Visible = False
                Me.ConsultantSubForm.Visible = False
                Me.JobAdminSubForm.Visible = True
                For Each n As TreeNode In Me.consultantApprovalRootNode.Nodes
                    Try
                        Dim nodeUid As Guid = CType(n.Tag, Guid)
                        If nodeUid = consultantUid Then
                            Me.NavTreeJob.SelectedNode = n
                            Me.JobAdminSubForm.Filter = "ConsultantUID ='" & nodeUid.ToString & "'"
                            Me.ResumeLayout()
                            Exit Sub
                        End If
                    Catch ex As InvalidCastException
                        Exit Sub
                    End Try
                Next
            Catch ex As Exception
                Me.JobAdminSubForm.Filter = Nothing
            Finally
                Me.ResumeLayout()
            End Try
        End If
    End Sub

    Public Sub FindFirstJobByEngineer(ByVal consultantUid As Guid)
        If Me.InvokeRequired Then
            Dim d As New FilterDelegate(AddressOf FindFirstJobByEngineer)
            Me.BeginInvoke(d, consultantUid)
        Else
            Try
                'Me.SuspendLayout()
                'Me.JobAdminSubForm.Filter = Nothing
                Me.JobAdminSubForm.FindConsultantRecord(consultantUid)
            Catch ex As Exception
                'Me.JobAdminSubForm.Filter = Nothing
            Finally
                'Me.ResumeLayout()
            End Try
        End If
    End Sub



#End Region


    Private Enum ProfileSortOrder
        Name
        IntervalSyncTime
        LastSyncTime
    End Enum


    Private Sub TreeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavTreeJobAdminButton.Click, NavTreeUsersButton.Click, NavTreeRequestAdminButton.Click
        Try
            Me.SuspendLayout()
            Me.NavTreeRequestAdminButton.BackgroundImage = My.Resources.ButtonBG
            Me.NavTreeJobAdminButton.BackgroundImage = My.Resources.ButtonBG
            Me.NavTreeUsersButton.BackgroundImage = My.Resources.ButtonBG
            Me.RequestAdminSubForm.Visible = False
            Me.ConsultantSubForm.Visible = False
            Me.JobAdminSubForm.Visible = False
            Me.NavTreeConsultant.Visible = False
            Me.NavTreeJob.Visible = False
            Me.NavTreeRequest.Visible = False
            Me.NavTreeUsersButton.Tag = False
            Me.NavTreeJobAdminButton.Tag = False
            Me.NavTreeRequestAdminButton.Tag = False

            If sender Is NavTreeJobAdminButton Then
                Me.NavTreeJobAdminButton.Tag = True
                Me.NavTreeJob.Visible = True
                Me.JobAdminSubForm.Visible = True
                Me.PrintPreviewToolStripMenuItem.Enabled = True
                Me.NavTreeJobAdminButton.BackgroundImage = My.Resources.ButtonSelected
                Me.NavTreeJob.Focus()
            ElseIf sender Is NavTreeUsersButton Then
                Me.NavTreeUsersButton.Tag = True
                Me.NavTreeConsultant.Visible = True
                Me.ConsultantSubForm.SetFilter(String.Empty)
                Me.NavTreeConsultant.SelectedNode = Me.activiserUsersRootNode.FirstNode
                Me.ConsultantSubForm.Visible = True
                Me.PrintPreviewToolStripMenuItem.Enabled = False
                Me.NavTreeUsersButton.BackgroundImage = My.Resources.ButtonSelected
                Me.NavTreeConsultant.Focus()
            ElseIf sender Is NavTreeRequestAdminButton Then
                Me.NavTreeRequestAdminButton.Tag = True
                Me.NavTreeRequest.Visible = True
                Me.RequestAdminSubForm.Visible = True
                Me.PrintPreviewToolStripMenuItem.Enabled = False
                Me.NavTreeRequestAdminButton.BackgroundImage = My.Resources.ButtonSelected
                Me.NavTreeRequest.Focus()
                If Me.NavTreeRequest.SelectedNode.Parent Is Me.requestStatusRootNode Then
                    If NavTreeRequest.SelectedNode.Tag IsNot Nothing Then
                        Me.RequestAdminSubForm.Filter = String.Format("RequestStatusID={0}", Val(NavTreeRequest.SelectedNode.Tag))
                    Else
                        Me.RequestAdminSubForm.Filter = Nothing
                    End If
                ElseIf Me.NavTreeRequest.SelectedNode.Parent Is Me.consultantStatusRootNode Then
                    If NavTreeRequest.SelectedNode.Tag IsNot Nothing Then
                        Me.RequestAdminSubForm.Filter = String.Format("ConsultantStatusID={0} and RequestStatusID<>{0}", Val(NavTreeRequest.SelectedNode.Tag)) ' AND RequestStatusID <> ConsultantStatusID
                    Else
                        Me.RequestAdminSubForm.Filter = Nothing
                    End If
                Else
                    If Me.requestStatusRootNode.Nodes.Count > 0 Then
                        Me.NavTreeRequest.SelectedNode = Me.requestStatusRootNode.FirstNode
                    Else
                        Me.NavTreeRequest.SelectedNode = Me.requestStatusRootNode
                    End If
                    Me.RequestAdminSubForm.Filter = Nothing
                End If
            End If

        Catch ex As Exception

        Finally
            Me.ResumeLayout()
        End Try

    End Sub

    Private Sub NavTreeRequest_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles NavTreeRequest.AfterSelect
        Me.ConsultantSubForm.Visible = False
        Me.JobAdminSubForm.Visible = False
        Me.RequestAdminSubForm.Visible = True
        Me.NavTreeRequestAdminButton.Tag = True
        Me.PrintPreviewToolStripMenuItem.Enabled = False

        Me.NavTreeJob.Visible = False
        Me.NavTreeConsultant.Visible = False
        Me.NavTreeRequest.Visible = True
        If e.Node Is requestStatusRootNode OrElse e.Node Is consultantStatusRootNode Then
            NavTreeRequest.SelectedNode = e.Node.Nodes(0)
        End If
        If NavTreeRequest.SelectedNode.Parent Is requestStatusRootNode Then
            If NavTreeRequest.SelectedNode.Tag IsNot Nothing Then
                Me.RequestAdminSubForm.Filter = String.Format("RequestStatusID={0}", Val(NavTreeRequest.SelectedNode.Tag))
            Else
                Me.RequestAdminSubForm.Filter = Nothing
            End If
        ElseIf NavTreeRequest.SelectedNode.Parent Is consultantStatusRootNode Then
            If NavTreeRequest.SelectedNode.Tag IsNot Nothing Then
                Me.RequestAdminSubForm.Filter = String.Format("ConsultantStatusID={0} and RequestStatusID<>{0}", Val(NavTreeRequest.SelectedNode.Tag)) ' AND RequestStatusID <> ConsultantStatusID
            Else
                Me.RequestAdminSubForm.Filter = Nothing
            End If
        End If
    End Sub

    Private Sub NavTreeJob_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles NavTreeJob.AfterSelect
        Me.RequestAdminSubForm.Visible = False
        Me.ConsultantSubForm.Visible = False
        Me.JobAdminSubForm.Visible = True
        Me.NavTreeJobAdminButton.Tag = True
        Me.PrintPreviewToolStripMenuItem.Enabled = True
        'Me.RequestAdminButton.Dock = DockStyle.Bottom
        'Me.ConsultantProfileButton.Dock = DockStyle.Bottom
        Me.NavTreeRequest.Visible = False
        Me.NavTreeConsultant.Visible = False
        Me.NavTreeJob.Visible = True
        If e.Node Is consultantApprovalRootNode OrElse e.Node Is approvalRootNode Then
            NavTreeJob.SelectedNode = e.Node.Nodes(0)
        End If
        If NavTreeJob.SelectedNode.Parent Is consultantApprovalRootNode Then
            If TypeOf NavTreeJob.SelectedNode.Tag Is Guid Then
                Dim consultantUID As Guid = CType(NavTreeJob.SelectedNode.Tag, Guid)
                Me.JobAdminSubForm.Filter = "ConsultantUID ='" & consultantUID.ToString & "'"
            Else
                Me.JobAdminSubForm.Filter = Nothing
            End If
        ElseIf NavTreeJob.SelectedNode.Parent Is approvalRootNode Then
            Dim nodeTag As ApprovalNodeTag = CType(NavTreeJob.SelectedNode.Tag, ApprovalNodeTag)
            Me.JobAdminSubForm.Filter = nodeTag.Filter ' CStr(NavigationTree.SelectedNode.Tag) ' Filter based on the apporval status
        End If
    End Sub

    Private Sub NavTreeConsultant_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles NavTreeConsultant.AfterSelect
        Me.RequestAdminSubForm.Visible = False
        Me.JobAdminSubForm.Visible = False
        Me.ConsultantSubForm.Visible = True
        Me.NavTreeUsersButton.Tag = True
        'Me.RequestAdminButton.Dock = DockStyle.Top
        'Me.ConsultantProfileButton.Dock = DockStyle.Top
        Me.PrintPreviewToolStripMenuItem.Enabled = False
        Me.NavTreeRequest.Visible = False
        Me.NavTreeJob.Visible = False
        Me.NavTreeConsultant.Visible = True
        If e.Node Is activiserUsersRootNode Then
            NavTreeConsultant.SelectedNode = e.Node.FirstNode
        ElseIf e.Node Is otherUsersRootNode Then
            NavTreeConsultant.SelectedNode = e.Node.FirstNode
        End If

        If TypeOf e.Node.Tag Is Guid Then
            Dim consultantUID As Guid = CType(e.Node.Tag, Guid)
            If Not Me.ConsultantSubForm.SetFilter("ConsultantUID ='" & consultantUID.ToString & "'") Then
                Dim node As TreeNode = getConsultantNode(Me.ConsultantSubForm.CurrentConsultant.ConsultantUID, Me.activiserUsersRootNode)
                If node Is Nothing Then node = getConsultantNode(Me.ConsultantSubForm.CurrentConsultant.ConsultantUID, Me.otherUsersRootNode)
                If node IsNot Nothing Then NavTreeConsultant.SelectedNode = node
            End If

        Else
            Me.ConsultantSubForm.SetFilter(String.Empty)
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim f As New SplashScreen
        f.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerSettingsMenuItem.Click
        Dim f As New ServerProfileForm()
        f.ShowDialog()
    End Sub

    Private Sub CustomizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'RequestStatusDialog.ShowDialog()
    End Sub

    Private Shared Sub CheckLicense()
        Try
            If Not Console.ConsoleData.Polling Then Return
            Dim spt As Utility.ServerSettingDataTable = ConsoleData.WebService.ConsoleGetServerProfile(deviceId, ConsoleUser.ConsultantUID, True)

            Dim licenseKey As String
            Dim licensee As String
            Dim webServiceGuid As String

            For Each spr As Utility.ServerSettingRow In spt
                If spr.Name = "LicenseKey" Then licenseKey = spr.Value
                If spr.Name = "Licensee" Then licensee = spr.Value
                If spr.Name = "WebServiceProductCode" Then webServiceGuid = spr.Value
            Next

            If String.IsNullOrEmpty(licensee) OrElse String.IsNullOrEmpty(licenseKey) OrElse String.IsNullOrEmpty(webServiceGuid) OrElse Not webServiceGuid.IsGuid Then
                Dim f As New LicenseNotificationDialog(My.Resources.UnlicensedWarning)
                f.Show()
            Else
                'ooo - evil hard-coded GUID for activiser web service.
                Dim activiserGUID As New Guid(webServiceGuid)
                Dim license As activiser.Licensing.LicenseInfo = New activiser.Licensing.LicenseInfo(licenseKey, licensee, activiserGUID)
                Dim expiryTicks As New TimeSpan(license.ExpiryDate.Ticks)
                Dim nowTicks As New TimeSpan(Now.Ticks)

                If expiryTicks.Subtract(nowTicks).Days <= 14 And expiryTicks.Subtract(nowTicks).Days > 0 Then 'warn expiry in 10 days
                    Dim f As New LicenseNotificationDialog(String.Format(My.Resources.LicenseWillExpireMessageTemplate, expiryTicks.Subtract(nowTicks).Days.ToString))
                    f.Show()
                ElseIf nowTicks.Ticks > expiryTicks.Ticks Then 'expired
                    Dim f As New LicenseNotificationDialog(My.Resources.LicenseExpiredWarning)
                    f.Show()
                End If
            End If

        Catch ex As Exception
            DisplayException.Show(ex, My.Resources.UnableToProcessActiviserLicenseMessage, Icons.Critical)
        End Try
    End Sub

    Public Sub New()
        TraceVerbose(STR_Entered)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TraceVerbose(STR_Done)
        'Me.JobAdminSubForm.MinimumSize = New System.Drawing.Size(750, 540)
        'Me.ConsultantSubForm.MinimumSize = New System.Drawing.Size(640, 480)
    End Sub

    Private Sub _consoleData_Refreshed(ByVal sender As Object, ByVal e As ConsoleDataRefreshEventArgs) Handles _consoleData.Refreshed
        RefreshMe()
    End Sub

    Private Sub CustomiseMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CustomiseStatusFields.Click
        Try
            StatusCustomisation.Show()
            Me.SetupRequestStatusTree()
            Me.UpdateRequestStatusTree()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LicenseTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LicenseTimer.Tick
        CheckLicense()
    End Sub

    Private Sub NavigationTree_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles NavTreeJob.NodeMouseClick, NavTreeRequest.NodeMouseClick
        If Not (e.Node Is consultantApprovalRootNode OrElse e.Node Is approvalRootNode OrElse e.Node Is requestStatusRootNode OrElse e.Node Is consultantStatusRootNode) Then
            Me.NavTreeJob.Focus()
            Me.NavTreeJob.SelectedNode = e.Node

        Else
            If e.Node.IsExpanded Then
                e.Node.Collapse()
            Else
                e.Node.Expand()
            End If
        End If
    End Sub

    Private Sub SystemLogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SystemLogMenuItem.Click
        EventLogViewer.Show()
    End Sub

    Friend Sub UpdateRequestAdminButton()
        If Me.InvokeRequired Then
            Dim d As New UpdateTreeDelegate(AddressOf UpdateRequestAdminButton)
            Me.Invoke(d)
            Return
        End If
        Dim rrs() As activiserDataSet.RequestRow = _
            CType(CoreDataSet.Request.Select("(NOT ConsultantStatusID IS NULL) AND (ConsultantStatusID <> RequestSTatusID)"), activiserDataSet.RequestRow())

        If rrs IsNot Nothing AndAlso rrs.Length <> 0 Then
            Me.NavTreeRequestAdminButton.Text = Terminology.GetFormattedString(MODULENAME, RES_RequestAdminButtonTemplate, rrs.Length)
        Else
            Me.NavTreeRequestAdminButton.Text = Terminology.GetFormattedString(MODULENAME, RES_RequestAdminButtonTemplate, 0)
        End If
    End Sub

    Private Delegate Sub RefreshMeDelegate()
    Private _inRefreshMe As Boolean
    Private Sub RefreshMe()
        If _inRefreshMe Then Return ' somehow, a loop/stack overflow triggered between here and updaterequestsstatustree
        If Me.InvokeRequired Then
            Dim d As New RefreshMeDelegate(AddressOf RefreshMe)
            Me.BeginInvoke(d)
        Else
            Try
                _inRefreshMe = True
                Me.SuspendLayout()
                Me.UpdateApprovalTree()
                Me.UpdateRequestStatusTree()
                Me.UpdateConsultantTrees()
                Me.UpdateRequestAdminButton()

                Me.Refresh()
            Catch ex As Exception
                TraceError("Error updating nav tree: {0}", ex.ToString)
            Finally
                _inRefreshMe = False
            End Try
            Me.ResumeLayout()
        End If
    End Sub

    Friend Sub RefreshNavTree()
        RefreshMe()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Console.ConsoleData.StartRefresh()
    End Sub

    Private Sub OptionsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsMenuItem.Click
        Using oForm As New OptionsForm
            oForm.ShowDialog()
        End Using
        If ConsoleData.Polling Then
            Console.ConsoleData.StartRefresh()
        End If
    End Sub

#Region "Approval node support"
    Private Structure ApprovalNodeTag
        Public Name As String
        Public LabelTemplate As String
        Public ToolTip As String
        Public Filter As String
        Public Color As Color
        'Public Sub New(ByVal name As String, ByVal labelTemplate As String, ByVal toolTip As String, ByVal filter As String, ByVal color As Color)
        '    Me.Name = name
        '    Me.LabelTemplate = labelTemplate
        '    Me.ToolTip = toolTip
        '    Me.Filter = filter
        '    Me.Color = color
        'End Sub

        Public Sub New(ByVal itemList() As String)
            If itemList Is Nothing Then Exit Sub
            If itemList.Length >= 4 Then
                Me.Name = itemList(0)
                Me.LabelTemplate = itemList(1)
                Me.ToolTip = itemList(2)
                Me.Filter = itemList(3)
                If itemList.Length >= 5 Then
                    Try
                        Dim c As Color = Drawing.Color.FromName(itemList(4))
                        Me.Color = c
                    Catch ex As Exception
                        Me.Color = SystemColors.WindowText
                    End Try
                End If
            Else
                Throw New ArgumentException(My.Resources.MainFormApprovalNodeTagItemListMessage, "itemList")
            End If
        End Sub
    End Structure
#End Region
    Private _progressBarEnabled As Boolean
    Private Delegate Sub SetRefreshProgressBarEnabledDelegate(ByVal state As Boolean)
    Public Sub SetRefreshProgressBarEnabled(ByVal state As Boolean)
        If My.Application.ClosedByMainForm Then Return
        If Me.InvokeRequired Then
            Dim d As New SetRefreshProgressBarEnabledDelegate(AddressOf SetRefreshProgressBarEnabled)
            Me.BeginInvoke(d, state)
        Else
            _progressBarEnabled = state
            If state Then
                If OsLevel > 500 AndAlso (Application.VisualStyleState And VisualStyles.VisualStyleState.ClientAreaEnabled) = VisualStyles.VisualStyleState.ClientAreaEnabled Then
                    Me.RefreshProgressBar.Style = ProgressBarStyle.Marquee
                    Me.RefreshProgressBar.Value = Me.RefreshProgressBar.Maximum
                Else
                    RefreshFlasher.Start()
                    Me.RefreshProgressBar.Style = ProgressBarStyle.Continuous
                    Me.RefreshProgressBar.Value = Me.RefreshProgressBar.Maximum
                End If
            Else
                If OsLevel = 500 OrElse (Application.VisualStyleState And VisualStyles.VisualStyleState.ClientAreaEnabled) = VisualStyles.VisualStyleState.NoneEnabled Then
                    RefreshFlasher.Stop()
                Else
                    Me.RefreshProgressBar.Style = ProgressBarStyle.Continuous
                End If
                Me.RefreshProgressBar.Value = Me.RefreshProgressBar.Minimum
            End If
        End If
    End Sub

    Private Delegate Sub SetAutoRefreshTextDelegate(ByVal state As Boolean)
    Friend Sub SetAutoRefreshText(ByVal state As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetAutoRefreshTextDelegate(AddressOf SetAutoRefreshText)
            Me.BeginInvoke(d, state)
        Else
            If state Then
                Me.StatusBarRefreshEnabled.Text = Terminology.GetString(MODULENAME, "StatusBarRefreshEnabled")
            Else
                Me.StatusBarRefreshEnabled.Text = Terminology.GetString(MODULENAME, "StatusBarRefreshDisabled")
            End If
        End If
    End Sub

    Friend Sub SetEditInProgress(ByVal state As Boolean)
        SetControlVisible(Me.StatusBarRefreshBlockedByEdit, state)
    End Sub

    Private Delegate Sub SetControlVisibleDelegate(ByVal target As ToolStripItem, ByVal state As Boolean)
    Friend Sub SetControlVisible(ByVal target As ToolStripItem, ByVal state As Boolean)
        If target Is Nothing Then Throw New ArgumentNullException("target")
        If Me.InvokeRequired Then
            Dim d As New SetControlVisibleDelegate(AddressOf SetControlVisible)
            Me.BeginInvoke(d, target, state)
        Else
            If target.Visible <> state Then target.Visible = state
        End If
    End Sub

    'Private Sub MainForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = Keys.F5 AndAlso e.Modifiers = Keys.None Then
    '        '_refreshRequired = True
    '        Console.ConsoleData.StartRefresh()
    '    End If
    'End Sub

    Private Sub StatusBar_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshProgressBar.DoubleClick, StatusBarRefreshNeededLabel.DoubleClick
        Console.ConsoleData.StartRefresh()
    End Sub

    Private Sub StatusBarRefreshEnabled_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBarRefreshEnabled.DoubleClick
        ConsoleData.Polling = Not ConsoleData.Polling
    End Sub

    Private Sub ClockTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PollTimer.Tick
        Me.ToolStripClock.Text = DateTime.Now.ToLongTimeString()
    End Sub

    Private Sub RefreshFlasher_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshFlasher.Tick
        If _progressBarEnabled AndAlso OsLevel = 500 Then
            If Me.RefreshProgressBar.Value = Me.RefreshProgressBar.Maximum Then
                Me.RefreshProgressBar.Value = Me.RefreshProgressBar.Minimum
            Else
                Me.RefreshProgressBar.Value = Me.RefreshProgressBar.Maximum
            End If
        End If
    End Sub

    'Private Sub NavigationPanel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles NavigationPanel.Resize
    '    Dim navTreeHeight As Integer = Me.NavigationPanel.Height - Me.JobAdminTreeButton.Height - Me.RequestAdminButton.Height - Me.ConsultantProfileButton.Height
    '    Me.NavTreeJob.Height = navTreeHeight
    '    Me.NavTreeRequest.Height = navTreeHeight
    '    Me.NavTreeConsultant.Height = navTreeHeight
    'End Sub

    Private Sub OpenEventLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventLogMenutItem.Click
        SyncLogViewer.Show()
    End Sub

    Private Sub TreeButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NavTreeRequestAdminButton.MouseDown, NavTreeJobAdminButton.MouseDown, NavTreeUsersButton.MouseDown
        Dim rb As Button = TryCast(sender, Button)
        If rb Is Nothing Then Return
        rb.BackgroundImage = My.Resources.ButtonMouseDown
    End Sub

    Private Sub TreeButton_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavTreeRequestAdminButton.MouseEnter, NavTreeJobAdminButton.MouseEnter, NavTreeUsersButton.MouseEnter
        Dim rb As Button = TryCast(sender, Button)
        If rb Is Nothing Then Return
        If CBool(rb.Tag) Then
            rb.BackgroundImage = My.Resources.ButtonMouseDown
        Else
            rb.BackgroundImage = My.Resources.ButtonOver
        End If
    End Sub

    Private Sub TreeButton_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavTreeRequestAdminButton.MouseLeave, NavTreeJobAdminButton.MouseLeave, NavTreeUsersButton.MouseLeave
        Dim rb As Button = TryCast(sender, Button)
        If rb Is Nothing Then Return
        If CBool(rb.Tag) Then
            rb.BackgroundImage = My.Resources.ButtonSelected
        Else
            rb.BackgroundImage = My.Resources.ButtonBG
        End If
    End Sub

    Private Sub TreeButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NavTreeRequestAdminButton.MouseUp, NavTreeJobAdminButton.MouseUp, NavTreeUsersButton.MouseUp
        Dim rb As Button = TryCast(sender, Button)
        If rb Is Nothing Then Return
        If CBool(rb.Tag) Then
            rb.BackgroundImage = My.Resources.ButtonSelected
        Else
            rb.BackgroundImage = My.Resources.ButtonOver
        End If
    End Sub

    Private Sub DeviceTrackingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceTrackingToolStripMenuItem.Click
        DeviceTrackingForm.Show()
    End Sub
End Class
