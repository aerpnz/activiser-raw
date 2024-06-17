Imports System.Runtime.InteropServices

Friend Class ExplorerWrapper
    Implements IDisposable

    Const MODULENAME As String = "ExplorerWrapper"


    Private WithEvents _explorer As Outlook.Explorer
    Private WithEvents _commandBars As Office.CommandBars
    Private WithEvents _commandBar As Office.CommandBar
    Private WithEvents _cbAboutButton As Office.CommandBarButton
    Private WithEvents _cbNewRequestButton As Office.CommandBarButton
    Private WithEvents _cbScheduleRequestButton As Office.CommandBarButton
    Private WithEvents _cbLoginButton As Office.CommandBarButton
    'Private WithEvents _cmOptionsMenu As Office.CommandBarPopup
    Private WithEvents _cbConfigButton As Office.CommandBarButton
    Private WithEvents _miNewRequestMenuItem As Office.CommandBarButton
    Private WithEvents _miScheduleRequestMenuItem As Office.CommandBarButton

    Private _newRequestToolbarButtonTag As String = Guid.NewGuid.ToString()
    Private _newRequestContextMenuTag As String = Guid.NewGuid.ToString()
    Private _scheduleRequestToolbarButtonTag As String = Guid.NewGuid.ToString()
    Private _scheduleRequestContextMenuTag As String = Guid.NewGuid.ToString()
    Private _cbLoginButtonTag As String = Guid.NewGuid.ToString()
    Private _aboutButtonTag As String = Guid.NewGuid.ToString()
    Private _optionsButtonTag As String = Guid.NewGuid.ToString()

    Public ReadOnly Property LoginButton() As Office.CommandBarButton
        Get
            Return _cbLoginButton
        End Get
    End Property

    Public ReadOnly Property NewRequestButton() As Office.CommandBarButton
        Get
            Return _cbNewRequestButton
        End Get
    End Property

    Public ReadOnly Property ScheduleRequestButton() As Office.CommandBarButton
        Get
            Return _cbScheduleRequestButton
        End Get
    End Property

    Private Sub InitializeUI()
        TraceInfo(STR_Entered)

        _explorer.Activate()
        _commandBars = _explorer.CommandBars
        AddHandler _commandBars.OnUpdate, AddressOf _commandBars_OnUpdate

        'Return

        For Each cb As Office.CommandBar In _commandBars
            If cb.Name = My.Resources.ActiviserCommandBarName Then
                cb.Delete()
            End If
        Next

        _commandBar = _commandBars.Add(My.Resources.ActiviserCommandBarName, Office.MsoBarPosition.msoBarTop, False, True)
        If Not String.IsNullOrEmpty(My.Settings.CommandBarLocation) Then
            'Note: Height and Width are stored, but ignored - setting them doesn't work!
            'My.Settings.CommandBarLocation = String.Format("{0},{1},{2},{3},{4},{5}", _commandBar.Position, _commandBar.RowIndex, _commandBar.Left, _commandBar.Top, _commandBar.Width, _commandBar.Height)
            '"msoBarLeft,1,0,0,27,378"
            Try
                Dim items() As String = My.Settings.CommandBarLocation.Split(","c)
                If items IsNot Nothing AndAlso items.Length > 3 Then
                    Dim rowindex, left, top As Integer

                    If Not Integer.TryParse(items(1), rowindex) Then Exit Try
                    If Not Integer.TryParse(items(2), left) Then Exit Try
                    If Not Integer.TryParse(items(3), top) Then Exit Try

                    _commandBar.Position = CType([Enum].Parse(GetType(Office.MsoBarPosition), items(0), True), Microsoft.Office.Core.MsoBarPosition)

                    _commandBar.RowIndex = rowindex
                    _commandBar.Left = left
                    _commandBar.Top = top
                End If
            Catch ex As System.Runtime.InteropServices.COMException
                Debug.WriteLine(ex.Message)

            Catch ex As Exception
                TraceError(ex)
            End Try
        End If

        _cbAboutButton = CType(_commandBar.Controls.Add(Office.MsoControlType.msoControlButton), Office.CommandBarButton)
        _cbAboutButton.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_AboutCaption)
        _cbAboutButton.TooltipText = Terminology.GetString(My.Resources.SharedMessagesKey, RES_AboutToolTip)
        _cbAboutButton.Picture = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.Logo16)
        _cbAboutButton.Mask = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.Logo16Mask)
        _cbAboutButton.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIcon
        _cbAboutButton.Tag = _aboutButtonTag

        _cbNewRequestButton = CType(_commandBar.Controls.Add(Office.MsoControlType.msoControlButton), Office.CommandBarButton)
        _cbNewRequestButton.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ToolBarNewRequestCaption)
        _cbNewRequestButton.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
        _cbNewRequestButton.Enabled = False
        _cbNewRequestButton.Tag = _newRequestToolbarButtonTag

        _cbScheduleRequestButton = CType(_commandBar.Controls.Add(Office.MsoControlType.msoControlButton), Office.CommandBarButton)
        _cbScheduleRequestButton.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ToolBarScheduleRequestCaption)
        _cbScheduleRequestButton.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
        _cbScheduleRequestButton.Enabled = False
        _cbScheduleRequestButton.Tag = _scheduleRequestToolbarButtonTag

        _cbLoginButton = CType(_commandBar.Controls.Add(Office.MsoControlType.msoControlButton), Office.CommandBarButton)
        _cbLoginButton.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_LoginCaption)
        _cbLoginButton.Picture = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.Login)
        _cbLoginButton.Mask = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.LoginMask)
        _cbLoginButton.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
        _cbLoginButton.Tag = _cbLoginButtonTag

        _cbConfigButton = CType(_commandBar.Controls.Add(Office.MsoControlType.msoControlButton, ), Office.CommandBarButton)
        _cbConfigButton.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ConfigureCaption)
        _cbConfigButton.Picture = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.Configure)
        _cbConfigButton.Mask = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.ConfigureMask)
        _cbConfigButton.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
        _cbConfigButton.Tag = Me._optionsButtonTag

        _commandBar.Visible = True

        TraceInfo(STR_Done)
    End Sub

    Private Sub NewRequestButton_Click(ByVal Ctrl As Microsoft.Office.Core.CommandBarButton, ByRef CancelDefault As Boolean) Handles _miNewRequestMenuItem.Click, _cbNewRequestButton.Click
        TraceInfo(STR_Entered)

        If Not (Ctrl.Tag = Me._newRequestToolbarButtonTag OrElse Ctrl.Tag = Me._newRequestContextMenuTag) Then
            Return
        End If
        Dim isRequest As Boolean
        Dim appointment As Outlook.AppointmentItem
        Dim requestForm As OutlookRequestForm

        If _explorer.Selection.Count = 1 Then
            Try
                appointment = CType(Application.ActiveExplorer.Selection(1), Outlook.AppointmentItem)
            Catch ex As Exception
                TraceError(ex)
                Terminology.DisplayFormattedMessage(MODULENAME, RES_ErrorGettingAppointmment, MessageBoxIcon.Error, ex.Message)
                Return
            End Try

            isRequest = IsGuid(appointment.BillingInformation)

            If appointment IsNot Nothing AndAlso isRequest Then
                Try
                    requestForm = New OutlookRequestForm(Me.Application, appointment, False)
                    requestForm.Show()
                Catch ex As Exception
                    TraceError(ex)
                    Terminology.DisplayFormattedMessage(MODULENAME, RES_ErrorCreatingRequestForm, MessageBoxIcon.Error, ex.Message)
                End Try
            Else
                Try
                    If appointment IsNot Nothing Then
                        requestForm = New OutlookRequestForm(Me.Application, appointment, True)
                    Else
                        requestForm = New OutlookRequestForm(Me.Application, True, appointment.Subject, appointment.Body)
                    End If
                    requestForm.Show()
                Catch ex As Exception
                    TraceError(ex)
                    Terminology.DisplayFormattedMessage(MODULENAME, RES_ErrorCreatingRequestForm, MessageBoxIcon.Error, ex.Message)
                End Try
            End If
        Else
            'HACK - VSTO Outlook API doesn't expose selected time range, even just creating a new appointment ignores the current selected time - you must use the
            ' the 'New Appointment' button.
            'AddHandler Application.Inspectors.NewInspector, AddressOf inspectors_NewInspector
            Dim button As Office.CommandBarControl = Application.ActiveExplorer.CommandBars.FindControl(, 1106)
            ThisAddIn.NewRequestButtonClicked = True
            'Me._NewRequestButtonClicked = True
            button.Execute()

        End If
        TraceVerbose(STR_Done)
    End Sub

    Private Sub ScheduleRequestButton_Click(ByVal Ctrl As Microsoft.Office.Core.CommandBarButton, ByRef CancelDefault As Boolean) Handles _cbScheduleRequestButton.Click, _miScheduleRequestMenuItem.Click
        TraceInfo(STR_Entered)
        If Not (Ctrl.Tag = Me._scheduleRequestContextMenuTag OrElse Ctrl.Tag = Me._scheduleRequestToolbarButtonTag) Then
            Return
        End If

        Dim appointment As Outlook.AppointmentItem
        Dim requestForm As OutlookRequestForm

        If _explorer.Selection.Count = 1 Then
            Try
                appointment = CType(Application.ActiveExplorer.Selection(1), Outlook.AppointmentItem)
            Catch ex As Exception
                TraceError(ex)
                Terminology.DisplayFormattedMessage(MODULENAME, RES_ErrorGettingAppointmment, MessageBoxIcon.Error, ex.Message)
                Return
            End Try
        ElseIf _explorer.Selection.Count = 0 Then
            Try
                Dim button As Office.CommandBarControl = Application.ActiveExplorer.CommandBars.FindControl(, 1106)
                button.Execute()
                appointment = CType(Application.ActiveInspector.CurrentItem, Outlook.AppointmentItem)
                Application.ActiveInspector.Close(Outlook.OlInspectorClose.olDiscard)
            Catch ex As Exception
                TraceError(ex)
                Terminology.DisplayFormattedMessage(MODULENAME, RES_ErrorGettingAppointmment, MessageBoxIcon.Error, ex.Message)
            End Try
        Else
            Return
        End If

        Try
            requestForm = New OutlookRequestForm(Me.Application, appointment, False)
            requestForm.Show()
        Catch ex As Exception
            TraceError(ex)
            Terminology.DisplayFormattedMessage(MODULENAME, RES_ErrorCreatingRequestForm, MessageBoxIcon.Error, ex.Message)
        End Try

        TraceVerbose(STR_Done)
    End Sub

    Private Sub AboutButton_Click(ByVal Ctrl As Microsoft.Office.Core.CommandBarButton, ByRef CancelDefault As Boolean) Handles _cbAboutButton.Click
        TraceInfo(STR_Entered)
        Using f As New SplashScreen
            f.ShowDialog()
            f.Close()
        End Using
        TraceVerbose(STR_Done)
    End Sub

    Private Sub Config_Click(ByVal Ctrl As Microsoft.Office.Core.CommandBarButton, ByRef CancelDefault As Boolean) Handles _cbConfigButton.Click
        TraceInfo(STR_Entered)
        Using f As New OptionsForm()
            f.ShowDialog()
            f.Close()
        End Using
        TraceVerbose(STR_Done)
    End Sub

    Private Sub _cbLoginButton_Click(ByVal Ctrl As Microsoft.Office.Core.CommandBarButton, ByRef CancelDefault As Boolean) Handles _cbLoginButton.Click
        TraceInfo(STR_Entered)
        If LoggedOn Then
            LogInOut.Logout()
        Else
            LogInOut.Logon()
        End If
        ThisAddIn.SetIconState()
        TraceVerbose(STR_Done)
    End Sub

    Private _Application As Outlook.Application
    Friend Property Application() As Outlook.Application
        Get
            Return _Application
        End Get
        Set(ByVal value As Outlook.Application)
            _Application = value
        End Set
    End Property


    Private _explorerList As Generic.List(Of ExplorerWrapper)
    Friend Property ExplorerList() As Generic.List(Of ExplorerWrapper)
        Get
            Return _explorerList
        End Get
        Set(ByVal value As Generic.List(Of ExplorerWrapper))
            _explorerList = value
        End Set
    End Property

    Friend Property Explorer() As Outlook.Explorer
        Get
            Return _explorer
        End Get
        Set(ByVal value As Outlook.Explorer)
            _explorer = value
        End Set
    End Property

    Friend Sub New(ByVal explorer As Outlook.Explorer, ByVal list As Generic.List(Of ExplorerWrapper))
        TraceInfo(STR_Entered)
        If explorer Is Nothing Then
            Throw New ArgumentNullException("explorer")
        End If
        Me.Application = explorer.Application
        Me._explorer = explorer
        InitializeUI()
        'list.Add(Me)
        Me.ExplorerList = list
        TraceVerbose(STR_Done)
    End Sub

    Private _finalized As Boolean
    Friend Sub Close()
        If _finalized Then Return
        TraceVerbose(My.Resources.TRACEMESSAGEExplorerClosed, _explorer.Caption)

        If _commandBar IsNot Nothing Then
            My.Settings.CommandBarLocation = String.Format("{0},{1},{2},{3},{4},{5}", _commandBar.Position, _commandBar.RowIndex, _commandBar.Left, _commandBar.Top, _commandBar.Width, _commandBar.Height)
            My.Settings.Save()
        End If

        If Not _finalizing AndAlso _commandBars IsNot Nothing Then
            Try
                RemoveHandler _commandBars.OnUpdate, AddressOf _commandBars_OnUpdate
            Catch ex As Runtime.InteropServices.InvalidComObjectException
                ' don't care.
            Catch ex As Exception
                TraceError(ex)
            End Try
        End If

        If Me.ExplorerList IsNot Nothing Then
            If Me.ExplorerList.Contains(Me) Then
                Me._explorerList.Remove(Me)
            End If
        End If

        Marshal.FinalReleaseComObject(_commandBar)
        Marshal.FinalReleaseComObject(_commandBars)
        Marshal.FinalReleaseComObject(_explorer)
    End Sub

    Private _finalizing As Boolean
    Protected Overrides Sub Finalize()
        _finalizing = True
        Me.Close()
        MyBase.Finalize()
    End Sub

    Private Sub _commandBars_OnUpdate() 'Handles _commandBars.OnUpdate
        TraceInfo(STR_Entered)
        Try
            Dim currentExplorer As Outlook.Explorer = TryCast(_commandBars.Parent, Outlook.Explorer)
            If currentExplorer IsNot Nothing Then
                For Each cb As Office.CommandBar In currentExplorer.CommandBars
                    If cb.Name = My.Resources.ContextMenuCommandBarName Then
                        If currentExplorer.CurrentFolder.DefaultItemType = Outlook.OlItemType.olAppointmentItem Then
                            If currentExplorer.Selection.Count > 1 Then Exit Sub
                            Dim isRequest As Boolean = False
                            If Application.ActiveExplorer.Selection.Count = 1 Then
                                Dim s As Outlook.AppointmentItem = TryCast(Application.ActiveExplorer.Selection(1), Outlook.AppointmentItem)
                                If s IsNot Nothing Then
                                    isRequest = IsGuid(s.BillingInformation)
                                End If
                            End If

                            Dim oldProtection As Office.MsoBarProtection = cb.Protection
                            If CBool(cb.Protection And Microsoft.Office.Core.MsoBarProtection.msoBarNoCustomize) Then
                                cb.Protection = cb.Protection And Not (Microsoft.Office.Core.MsoBarProtection.msoBarNoCustomize)
                            End If

                            If isRequest Then
                                _miNewRequestMenuItem = CType(cb.Controls.Add(Office.MsoControlType.msoControlButton), Office.CommandBarButton)
                                _miNewRequestMenuItem.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ContextMenuOpenRequestCaption)
                                _miNewRequestMenuItem.Tag = Me._newRequestContextMenuTag
                                _miNewRequestMenuItem.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
                                _miNewRequestMenuItem.BeginGroup = True
                                _miNewRequestMenuItem.Visible = True
                                _miNewRequestMenuItem.Enabled = True
                                _miNewRequestMenuItem.Priority = 1
                            Else
                                _miNewRequestMenuItem = CType(cb.Controls.Add(Office.MsoControlType.msoControlButton), Office.CommandBarButton)
                                _miNewRequestMenuItem.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ContextMenuNewRequestCaption)
                                _miNewRequestMenuItem.Tag = Me._newRequestContextMenuTag
                                _miNewRequestMenuItem.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
                                _miNewRequestMenuItem.BeginGroup = True
                                _miNewRequestMenuItem.Visible = True
                                _miNewRequestMenuItem.Enabled = True
                                _miNewRequestMenuItem.Priority = 1
                                'End If

                                'If currentExplorer.Selection.Count = 0 Then
                                _miScheduleRequestMenuItem = CType(cb.Controls.Add(Office.MsoControlType.msoControlButton), Office.CommandBarButton)
                                _miScheduleRequestMenuItem.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ContextMenuScheduleRequestCaption)
                                _miScheduleRequestMenuItem.Tag = Me._scheduleRequestContextMenuTag
                                _miScheduleRequestMenuItem.Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
                                _miScheduleRequestMenuItem.BeginGroup = False
                                _miScheduleRequestMenuItem.Visible = True
                                _miScheduleRequestMenuItem.Enabled = True
                                _miScheduleRequestMenuItem.Priority = 1
                            End If

                            cb.Protection = oldProtection

                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            TraceError(ex)
        Finally
            TraceVerbose(STR_Done)
        End Try
    End Sub

    Private Sub _explorer_BeforeItemPaste(ByRef ClipboardContent As Object, ByVal Target As Microsoft.Office.Interop.Outlook.MAPIFolder, ByRef Cancel As Boolean) Handles _explorer.BeforeItemPaste
        ' is this one or more activiser appointment(s) being moved ?

        Dim selection As Outlook.Selection = TryCast(ClipboardContent, Outlook.Selection)
        If selection Is Nothing Then Return ' don't know, don't care.

        For i As Integer = 1 To selection.Count
            Dim appTest As Outlook.AppointmentItem = TryCast(selection.Item(i), Outlook.AppointmentItem)
            If appTest IsNot Nothing Then
                Debug.WriteLine(appTest.Start)
                If appTest.MessageClass = My.Resources.OutlookAppointmentClass Then ' this is a request !
                    Debug.WriteLine("Moving request: " & appTest.BillingInformation)
                End If
            End If
        Next
    End Sub

    'Private Sub _explorer_BeforeItemCut(ByRef Cancel As Boolean) Handles _explorer.BeforeItemCut
    '    Debug.WriteLine(_explorer.CurrentFolder.Name)
    'End Sub

    Private Sub explorer_FolderSwitch() Handles _explorer.FolderSwitch
        TraceVerbose(My.Resources.TRACEMESSAGEExplorerFolderSwitch, _explorer.CurrentFolder.Name, _explorer.Caption)
        Dim isCalendar As Boolean = _explorer.CurrentFolder.DefaultItemType = Outlook.OlItemType.olAppointmentItem
        Me._cbNewRequestButton.Enabled = isCalendar
        Me._cbScheduleRequestButton.Enabled = isCalendar
        TraceVerbose(STR_Done)
    End Sub

    Private Sub explorer_Close() Handles _explorer.Close
        Me.Dispose()
    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Me.Close()
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
