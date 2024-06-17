Imports System
Imports System.Threading

Public Class SyncNotificationDialog
    Const DBL_OpacityIncrement As Double = 1.0 / 100.001
    Const DBL_OpacityBottom As Double = 1.0 / 100.0
    Const DBL_OpacityTop As Double = 1.0 - DBL_OpacityBottom

    Private fadeInTime As Integer
    Private waitTime As Integer
    Private fadeOutTime As Integer

    Private fadeInInterval As Integer
    Private fadeOutInterval As Integer

    Private _position As Integer = 1
    Private _consultantUid As Guid
    Private _opacity As Double

    Private _waiting As Boolean
    Private _fadingOut As Boolean

    Public Event LinkClicked As EventHandler(Of LinkLabelLinkClickedEventArgs)

    Public Sub New(ByVal message As String, ByVal position As Integer, ByVal consultantUid As Guid)
        Me.InitializeComponent()

        fadeInTime = Math.Max(My.Settings.NotificationFadeInTime, 2000)
        fadeInInterval = fadeInTime \ 100
        Me.FadeTimer.Interval = fadeInInterval

        waitTime = Math.Max(My.Settings.NotificationDisplayTime, 2000)
        Me.WaitTimer.Interval = waitTime

        fadeOutTime = Math.Max(My.Settings.NotificationFadeOutTime, 2000)
        fadeOutInterval = fadeOutTime \ 100

        Me.Message = message
        Me.Position = position
        Me.ConsultantGuid = consultantUid
    End Sub

    Private Sub SyncNotificationDialog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.StartFader()
    End Sub

#Region "Properties"
    Public Property Position() As Integer
        Get
            Return _position
        End Get
        Set(ByVal Value As Integer)
            _position = Value
        End Set
    End Property

    Public Property ConsultantGuid() As Guid
        Get
            Return _consultantUid
        End Get
        Set(ByVal value As Guid)
            _consultantUid = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return Me.SyncMessage.Text
        End Get
        Set(ByVal Value As String)
            Me.SyncMessage.Text = Value
        End Set
    End Property
#End Region

    Public Sub StopFader()
        StartWaitTimer()
    End Sub

    Public Sub StartFader()
        If _waiting Then Return
        FadeTimer.Start()
    End Sub

    Private Sub StartWaitTimer()
        FadeTimer.Stop()
        Me._opacity = 1.0
        setOpacity(_opacity)
        _waiting = True
        WaitTimer.Start()
    End Sub

    Private Delegate Sub setOpacityDelegate(ByVal opacity As Double)

    Private Sub setOpacity(ByVal opacity As Double)
        Try
            If Me.InvokeRequired Then
                Dim d As New setOpacityDelegate(AddressOf setOpacity)
                Me.Invoke(d, opacity)
            Else
                Me.Opacity = opacity
                Me.Refresh()
            End If
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
    End Sub

    Private Delegate Sub closeMeDelegate()

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub Message_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles SyncMessage.LinkClicked
        StopFader()
        e.Link.LinkData = Me.ConsultantGuid
        e.Link.Name = Message
        RaiseEvent LinkClicked(Me, e)
    End Sub

    Private Sub NotificationDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rectTemp As System.Drawing.Rectangle
        rectTemp = Screen.GetWorkingArea(rectTemp)
        Me.Location = New System.Drawing.Point(0, rectTemp.Height - (Me.Height * (Position + 1)))
        Me.Opacity = 0
    End Sub

#Region "Mouse Events"
    Private _mouseDown As Boolean
    Private _mouseOverMe, _mouseOverLink, _mouseOverLogo As Boolean
    Private _mouseDownLocation As Point


    Private Sub Me_MouseOver(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter, Me.MouseHover
        _mouseOverMe = True
        MouseOverFaderControl()
    End Sub

    Private Sub Link_MouseOver(ByVal sender As Object, ByVal e As System.EventArgs) Handles SyncMessage.MouseHover, SyncMessage.MouseEnter
        _mouseOverLink = True
        MouseOverFaderControl()
    End Sub

    Private Sub Logo_MouseOver(ByVal sender As Object, ByVal e As System.EventArgs) Handles Logo.MouseHover, Logo.MouseEnter
        _mouseOverLogo = True
        MouseOverFaderControl()
    End Sub

    Private Sub Me_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave, Me.MouseHover
        _mouseOverMe = False
        MouseOverFaderControl()
    End Sub

    Private Sub Link_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles SyncMessage.MouseEnter, SyncMessage.MouseLeave
        _mouseOverLink = False
        MouseOverFaderControl()
    End Sub

    Private Sub Logo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Logo.MouseLeave, Logo.MouseEnter
        _mouseOverLogo = False
        MouseOverFaderControl()
    End Sub

    Private Sub MouseOverFaderControl()
        If _mouseOverMe OrElse _mouseOverLink OrElse _mouseOverLogo Then
            StopFader()
        Else
            StartFader()
        End If
    End Sub

    Private Sub NotificationDialog_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, Logo.MouseDown, SyncMessage.MouseDown
        _mouseDown = True
        _mouseDownLocation = e.Location
    End Sub

    Private Sub NotificationDialog_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, Logo.MouseUp, CloseButton.MouseUp, SyncMessage.MouseUp
        _mouseDown = False
    End Sub

    Private Sub NotificationDialog_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, Logo.MouseMove, SyncMessage.MouseMove
        StopFader()
        If _mouseDown Then
            Dim lNewVariable As System.Drawing.Point = New Point(Me.Location.X + (-_mouseDownLocation.X + e.X), Me.Location.Y + (-_mouseDownLocation.Y + e.Y))
            Me.Location = lNewVariable
            Me.Refresh()
        End If
    End Sub

    Private Sub CloseButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CloseButton.MouseDown
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.MidnightBlue
            l.ForeColor = Color.Transparent
        End If
    End Sub

    Private Sub CloseButton_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CloseButton.MouseLeave
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.Transparent
            l.ForeColor = Color.MidnightBlue
        End If
    End Sub

    Private Sub CloseButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CloseButton.MouseUp
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.Transparent
            l.ForeColor = Color.MidnightBlue
        End If
    End Sub

    Private Sub CloseButton_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.MouseHover
        Dim l As Control = TryCast(sender, Control)
        If l IsNot Nothing Then
            l.BackColor = Color.DodgerBlue
            l.ForeColor = Color.Transparent
        End If
    End Sub

#End Region

    Private Sub FadeTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FadeTimer.Tick
        If Not _fadingOut Then
            If _opacity <= DBL_OpacityTop Then
                _opacity += DBL_OpacityIncrement
                setOpacity(_opacity)
                Return
            End If
            ' fade-in finished, got to wait now
            setOpacity(1.0)
            StartWaitTimer()
            Return
        End If

        If _opacity >= DBL_OpacityBottom Then
            _opacity -= DBL_OpacityIncrement
            setOpacity(_opacity)
            Return
        End If

        FadeTimer.Stop()
        Me.Close()
    End Sub

    Private Sub WaitTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WaitTimer.Tick
        _fadingOut = True
        _waiting = False
        FadeTimer.Interval = fadeOutInterval
        StartFader()
    End Sub
End Class