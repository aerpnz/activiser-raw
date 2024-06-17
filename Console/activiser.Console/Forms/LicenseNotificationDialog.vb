Public Class LicenseNotificationDialog
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

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
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents DisplayTimer As System.Windows.Forms.Timer
    Friend WithEvents llbMessage As System.Windows.Forms.LinkLabel
    Friend WithEvents StartTimer As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LicenseNotificationDialog))
        Me.lblClose = New System.Windows.Forms.Label
        Me.DisplayTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StartTimer = New System.Windows.Forms.Timer(Me.components)
        Me.llbMessage = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'lblClose
        '
        Me.lblClose.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblClose, "lblClose")
        Me.lblClose.Name = "lblClose"
        '
        'DisplayTimer
        '
        Me.DisplayTimer.Interval = 5000
        '
        'StartTimer
        '
        '
        'llbMessage
        '
        resources.ApplyResources(Me.llbMessage, "llbMessage")
        Me.llbMessage.Name = "llbMessage"
        Me.llbMessage.TabStop = True
        '
        'LicenseNotificationDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ControlBox = False
        Me.Controls.Add(Me.llbMessage)
        Me.Controls.Add(Me.lblClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LicenseNotificationDialog"
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Sub New(ByVal message As String)
        Me.New()
        Me.DisplayText = Message
    End Sub

    Private CloseOnFaded As Boolean
    Private Clear As Double
    Private intPosition As Integer = 1

    Public Property Position() As Integer
        Get
            Return intPosition
        End Get
        Set(ByVal Value As Integer)
            intPosition = Value
        End Set
    End Property

    Public Property DisplayText() As String
        Get
            Return llbMessage.Text
        End Get
        Set(ByVal Value As String)
            llbMessage.Text = Value
        End Set
    End Property

    Public Property DisplayTime() As Integer
        Get
            Return DisplayTimer.Interval
        End Get
        Set(ByVal Value As Integer)
            DisplayTimer.Interval = Value
        End Set
    End Property

    Private Sub frmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If CloseOnFaded Then Return 'don't call twice if is closing already
        e.Cancel = True 'handles this event in another routine
        CloseForm() 'this sets the closeonfaded flag
    End Sub

    Private Sub CloseForm()
        If Not CloseOnFaded Then 'if flag not set
            CloseOnFaded = True 'set flag
            For Clear As Double = 1 To 0 Step -0.01
                System.Threading.Thread.Sleep(10)
                Me.Opacity = Clear
                ' Application.DoEvents()
                Me.Refresh()
            Next
            Me.Close()
        Else 'if flag set, end the application
            Me.Close()
        End If
    End Sub

    Private Sub frmInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rectTemp As System.Drawing.Rectangle = Screen.GetWorkingArea(rectTemp)
        Me.Location = New System.Drawing.Point(0, rectTemp.Height - (74 * Position))
        Me.Opacity = 0
        Me.StartTimer.Enabled = True
        Me.StartTimer.Start()
    End Sub

    Private Sub lblClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblClose.Click
        Me.Close()
    End Sub

    Private Sub DisplayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayTimer.Tick
        DisplayTimer.Stop()
        Me.Close()
    End Sub

    Private Sub frmInfo_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter
        Me.DisplayTimer.Stop()
    End Sub

    Private Sub frmInfo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave
        Me.DisplayTimer.Start()
    End Sub

    Private Sub StartTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartTimer.Tick
        Me.StartTimer.Stop()

        For Clear As Double = 0 To 1.01 Step 0.01
            System.Threading.Thread.Sleep(10)
            Me.Opacity = Clear
            'Application.DoEvents()
            Me.Refresh()
        Next

        DisplayTimer.Interval = DisplayTime
        DisplayTimer.Start()
    End Sub

    Private Sub llbMessage_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbMessage.LinkClicked
        Dim f As New ServerProfileForm()
        f.RegistrationTab.Focus()
        Me.Hide()
        f.ShowDialog()
        Me.Close()
    End Sub
End Class
