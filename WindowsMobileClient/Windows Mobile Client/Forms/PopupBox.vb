Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.ComponentModel
Imports activiser.Library.Forms

Public Class PopupBox
    Friend Const MODULENAME As String = "DialogBox"

    Friend Event PopupCanceled As EventHandler(Of EventArgs)
    Friend Event PopupAcknowledged As EventHandler(Of EventArgs)
    Friend Event PopupTimedOut As EventHandler(Of EventArgs)

    Public Sub New(ByVal owner As Form, ByVal message As String, ByVal timeout As Integer)
        Me.InitializeComponent()
        Me.Message = message
        Me.Owner = owner
        If timeout <> 0 Then
            Me.PopupTimer.Interval = timeout
        End If
    End Sub

    Public Property Message() As String
        Get
            Return Caption.Text
        End Get
        Set(ByVal value As String)
            Caption.Text = value
            Me.Refresh()
        End Set
    End Property


    Public Property Message2() As String
        Get
            Return Caption2.Text
        End Get
        Set(ByVal value As String)
            Caption2.Text = value
            Me.Refresh()
        End Set
    End Property

    Public Property CaptionAlign() As Drawing.ContentAlignment
        Get
            Return Me.Caption.TextAlign
        End Get
        Set(ByVal value As Drawing.ContentAlignment)
            Me.Caption.TextAlign = value
            Me.Refresh()
        End Set
    End Property

    Private _timeout As Integer
    Public Property Timeout() As Integer
        Get
            Return _timeout
        End Get
        Set(ByVal value As Integer)
            _timeout = value
        End Set
    End Property

    Private _acknowledged As Boolean
    Public Property Acknowledged() As Boolean
        Get
            Return _acknowledged
        End Get
        Private Set(ByVal value As Boolean)
            _acknowledged = value
        End Set
    End Property

    Private _canceled As Boolean
    Public Property Canceled() As Boolean
        Get
            Return _canceled
        End Get
        Private Set(ByVal value As Boolean)
            _canceled = value
        End Set
    End Property

    Private _timedOut As Boolean
    Public Property TimedOut() As Boolean
        Get
            Return _timedOut
        End Get
        Set(ByVal value As Boolean)
            _timedOut = value
        End Set
    End Property

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BringToFront()
        If Timeout <> 0 Then
            Me.PopupTimer.Enabled = True
        End If
#If Not WINDOWSMOBILE Then
        EnableContextMenus(Me.Controls)
#End If
    End Sub
#If WINDOWSMOBILE Then
#Region "Input Panel Support"


    Private Sub EnableInputPanelOnFormLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InputPanelSwitch(True)
    End Sub

    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        InputPanelSwitch(False)
    End Sub

    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        Try
            If loadInitial Then
                InputPanel.Enabled = ConfigurationSettings.GetValue(MODULENAME & My.Resources.InputPanelEnabledRegValueName, False)
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
            Else
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                ConfigurationSettings.SetValue(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.InputPanel.Enabled)
            End If
            'Application.DoEvents()
            Me.Refresh()
        Catch ex As ObjectDisposedException
            ' who cares
        Catch ex As Exception
            Const METHODNAME As String = "InputPanelSwitch"
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub
#End Region
#End If

    Private Sub OkYesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkYesButton.Click
        Me.Acknowledged = True
        RaiseEvent PopupAcknowledged(Me, New EventArgs())
        'Me.Hide()
    End Sub

    Private Sub CancelAbortButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelAbortButton.Click
        Me.Canceled = True
        RaiseEvent PopupCanceled(Me, New EventArgs())
        'Me.Hide()
    End Sub

    Private Sub PopupTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PopupTimer.Tick
        Me.TimedOut = True
        RaiseEvent PopupTimedOut(Me, New EventArgs())
    End Sub
End Class