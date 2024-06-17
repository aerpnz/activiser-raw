Imports Microsoft.WindowsCE.Forms

Module Notifier
    Private Const MODULENAME As String = "Sync"
#If WINDOWSMOBILE Then
    Private _notificationTemplate As String = Terminology.GetString(MODULENAME, RES_NotificationTemplate)

    Private WithEvents _notification As Notification
    Private WithEvents _notificationTimer As System.Threading.Thread = New Threading.Thread(AddressOf notificationThread)

    Private _caption As String = String.Empty
    Private _message As String
    Private _visible As Boolean

    Private Sub notificationThread()
        _notification = New Notification
        Dim message As String = _message
        Dim caption As String = _caption

        Do

            If message <> _message Then
                message = _message
                _notification.Text = String.Format(_notificationTemplate, _message)
            End If

            If caption <> _caption Then
                caption = _caption
                _notification.Caption = caption
            End If

            _notification.Visible = _visible AndAlso Not String.IsNullOrEmpty(_message)
            Threading.Thread.Sleep(500)
        Loop Until _notification Is Nothing
    End Sub

    Private Sub Notification_BalloonChanged(ByVal sender As Object, ByVal e As BalloonChangedEventArgs) Handles _notification.BalloonChanged

    End Sub

    Private Sub Notification_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _notification.Disposed
        _notification = Nothing
    End Sub

    Private Sub Notification_ResponseSubmitted(ByVal sender As Object, ByVal e As ResponseSubmittedEventArgs) Handles _notification.ResponseSubmitted
        SyncForm.Show()
    End Sub

    Friend Delegate Sub SetNotificationTextDelegate(ByVal message As String)

    Friend Property Text() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return _caption
        End Get
        Set(ByVal value As String)
            _caption = value
        End Set
    End Property

    Friend Property Visible() As Boolean
        Get
            Return _visible
        End Get
        Set(ByVal value As Boolean)
            _visible = value
        End Set
    End Property
#Else
    'TODO: Add a WinCE alternative to notification (maybe?).
    Private _message As String = String.Empty
    Friend Property Text() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property
#End If

End Module
