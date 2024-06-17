'Imports activiser.WebService
Imports System.Text
Imports activiser.Library
Imports activiser.Library.WebService
Imports System.ComponentModel
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.WebService.ConsultantItemDataSet

<System.Runtime.InteropServices.ComVisible(False)> _
Public Class SyncForm
    Const MODULENAME As String = "SyncForm"

    Private WithEvents _syncLog As SyncLog ' = gSyncLog

    Private _status As String
    Private _syncOk As Boolean
    Private _startedAt As DateTime = DateTime.Now
    Private _autoClose As Boolean = AppConfig.GetSetting(My.Resources.AppConfigAutoCloseSyncFormKey, False)
    Private _logMutex As New Threading.ManualResetEvent(True)

    Private _askingQuestion As Boolean '= False

    Private Function ConfirmCancel() As Boolean
        Try
            _askingQuestion = True
            _logMutex.Reset()
            Return Terminology.AskQuestion(Me, MODULENAME, RES_SyncCancelConfirm, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes
        Catch ex As Exception

        Finally
            _askingQuestion = False
            _logMutex.Set()
            RefreshLog()
        End Try
    End Function

#Region "Constructors"
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal owner As Form)
        MyBase.New()
        InitializeComponent()

        Me.Owner = owner

        Url.Text = gWebServer.Url
        AutoCloseCheckBox.Checked = _autoClose

        AddHandler Synchronisation.Started, AddressOf SyncStartHandler
        AddHandler Synchronisation.StateChanged, AddressOf SyncStateChangeHandler
        AddHandler Synchronisation.Finished, AddressOf SyncFinishHandler
        AddHandler Synchronisation.ExceptionRaised, AddressOf SyncExceptionHandler
        _syncLog = gSyncLog
    End Sub

#End Region

    Private Sub SyncForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        _logMutex.Close()
        RemoveHandler Synchronisation.Started, AddressOf SyncStartHandler
        RemoveHandler Synchronisation.StateChanged, AddressOf SyncStateChangeHandler
        RemoveHandler Synchronisation.Finished, AddressOf SyncFinishHandler
        RemoveHandler Synchronisation.ExceptionRaised, AddressOf SyncExceptionHandler
        _syncLog = Nothing ' remove connection to gSyncLog!
        _logMutex = Nothing
    End Sub

    Private Sub Synchronise_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
#If WINDOWSCE Then
        EnableContextMenus(Me.Controls)
#End If
    End Sub

#Region "Sync Events"
    Private Sub SyncStartHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.InvokeRequired Then
            Dim d As New simpleSubDelegate(AddressOf SyncStarted)
            Me.Invoke(d)
        Else
            SyncStarted()
        End If
    End Sub

    Private Sub SyncFinishHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        _logMutex.WaitOne()

        If Me.InvokeRequired Then
            Dim d As New SimpleSubDelegate(AddressOf SyncFinished)
            Me.Invoke(d)
        Else
            SyncFinished()
        End If
    End Sub

    Private Sub SyncStateChangeHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        _status = Synchronisation.State
    End Sub

    Private Sub SyncExceptionHandler(ByVal sender As Object, ByVal e As ExceptionEventArgs)
        _syncOk = False
    End Sub

    Friend Sub SyncStarted()
        Me._startedAt = DateTime.Now
        _syncOk = True ' optimistic, I know.

        If Owner IsNot Nothing Then
            Owner.ControlBox = False
        Else
            Me.ControlBox = False
        End If

        Me.MenuClose.Enabled = False
        Me.OkCancelButton.Text = Terminology.GetString(MODULENAME, RES_Cancel)

        Me.SyncButton.Text = Terminology.GetString(MODULENAME, RES_HideButton)
        Me.SyncButton.Enabled = True

        Me.ResetServerButton.Enabled = False
        Me.MenuClose.Enabled = False

        Me.RefreshLog()
        Me.LogRefreshTimer.Enabled = True
    End Sub

    Friend Sub SyncFinished()
        _logMutex.WaitOne()
        If Not Me.Visible Then Me.Show()

        Me.LogRefreshTimer.Enabled = False

        Me.OkCancelButton.Text = Terminology.GetString(MODULENAME, RES_OK)
        Me.OkCancelButton.Enabled = True

        Me.SyncButton.Text = Terminology.GetString(MODULENAME, RES_SyncButton)
        Me.SyncButton.Enabled = True

        Me.ResetServerButton.Enabled = True
        Me.MenuClose.Enabled = True

        Try
            If Owner IsNot Nothing Then
                Owner.ControlBox = True ' note, this fiddles with the Z-Order, so we need to pop ourselves back up afterwards !
            Else
                Me.ControlBox = True
            End If
        Catch ex As ObjectDisposedException ' triggered by hopper ?
            Debug.WriteLine(String.Format("{0} ObjectDisposedException in SyncForm.SyncComplete: {1}", DateTime.Now, ex.StackTrace))
        End Try

        If _autoClose AndAlso _syncOk Then
            Me.LogRefreshTimer.Enabled = False
            Me.Close()
            Return
        Else
            Me.BringToFront()
            Me.Activate()
            Me.Focus()
        End If
    End Sub
#End Region

#Region "Button Events"
    Private Sub OkCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkCancelButton.Click
        If sender IsNot OkCancelButton Then Return

        If OkCancelButton.Text = Terminology.GetString(MODULENAME, RES_Cancel) Then
            If Not gCancelSync Then
                If ConfirmCancel() Then
                    Synchronisation.LogSyncMessage(RES_SyncCancelling)
                    gCancelSync = True
                Else
                    gCancelSync = False
                End If
            End If
            Return
        End If

        Me.Close()
    End Sub

    Private Sub SyncButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncButton.Click
        If Me.SyncButton.Text = Terminology.GetString(MODULENAME, RES_HideButton) Then
            Dim f As Form = Me.Owner
            Me.Owner = Nothing
            Me.Visible = False
            f.Activate()
            'Me.Notification.Visible = True
            Return
        Else
            Synchronisation.StartManualSync()
        End If
    End Sub

    Private Sub AutoCloseCheckBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoCloseCheckBox.Click
        _autoClose = Not _autoClose
        AutoCloseCheckBox.Checked = _autoClose
        AppConfig.SaveSetting(My.Resources.AppConfigAutoCloseSyncFormKey, _autoClose)
    End Sub

    Private Sub ResetServerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetServerButton.Click
        ' re-initialise web service proxy, in case of network failure.
        Startup.InitialiseWebServiceProxy(Me, True)
    End Sub
#End Region

#Region "Logging"
    Private Delegate Sub RefreshLogDelegate()
    Private Sub RefreshLog()
        If _askingQuestion Then Return
        If Me.InvokeRequired Then
            Dim d As New RefreshLogDelegate(AddressOf RefreshLog)
            Me.BeginInvoke(d)
            Return
        End If

        Try
            Me.SuspendLayout()
            Try
                If Not Me.StatusLabel.Text = _status Then Me.StatusLabel.Text = _status ' _syncLog.LastEntry Then Me.StatusLabel.Text = _syncLog.LastEntry
                If Not Me.Log.Text = gSyncLog.Text Then Me.Log.Text = gSyncLog.Text
            Catch ex As Exception

            End Try

            Me.ResumeLayout()
            Me.Refresh()
        Catch ex As ObjectDisposedException

        End Try

    End Sub

    Private Sub LogRefreshTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LogRefreshTimer.Tick
        Me.RefreshLog()
    End Sub

    Private Sub _syncLog_EntryAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles _syncLog.EntryAdded
        Me.RefreshLog()
    End Sub
#End Region

End Class