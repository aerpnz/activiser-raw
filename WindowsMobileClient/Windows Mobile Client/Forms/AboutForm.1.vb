Imports activiser.Library.PlatformInfo

Public Class AboutForm
    Const MODULENAME As String = "AboutForm"

    Public Sub New(ByVal owner As Form)
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Owner = owner
    End Sub

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.ActiviserIcon

        Dim v As System.Version = GetVersion()
        Dim versionText As String = Terminology.GetFormattedString(MODULENAME, "VersionFormat", v.Major, v.Minor, v.Build, v.Revision)
        Dim userText As String = Terminology.GetFormattedString(MODULENAME, "UserFormat", ConsultantConfig.Name, If(ConsultantConfig.LastSyncOK, FormatDate(ConsultantConfig.LastSync, False), My.Resources.Unknown))
        Dim deviceIdText As String = Terminology.GetFormattedString(MODULENAME, "DeviceIdFormat", GetFormattedDeviceID())
        Dim serverUri As New Uri(gServerUrl)
        Dim serverText As String = Terminology.GetFormattedString(MODULENAME, "ServerFormat", serverUri.Host)
        Dim serverUrl As String = Terminology.GetFormattedString(MODULENAME, "ServerUrlFormat", serverUri.AbsolutePath)
        Dim platformText As String = Terminology.GetFormattedString(MODULENAME, "PlatformFormat", Info.PlatformName, System.Environment.OSVersion.Version.ToString(2))
        Dim frameworkText As String = Terminology.GetFormattedString(MODULENAME, "FrameworkVersionFormat", Environment.Version.Major, Environment.Version.Minor, Environment.Version.Build, Environment.Version.Revision)

        Const STR_AboutMessageFormat As String = "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}"
        AboutMessage.Text = String.Format(WithCulture, STR_AboutMessageFormat, vbNewLine, versionText, platformText, userText, deviceIdText, serverText, serverUrl, frameworkText)
#If WINDOWSMOBILE Then
        If Me.Width > 400 AndAlso Me.Height > 400 Then ' VGA device
            Me.Logo.Image = My.Resources.Logo200x100
            'Me.LogoPanel.Height = Me.Logo.Height '+ (Me.Logo.Top * 2)
        Else
            Me.Logo.Image = My.Resources.Logo100x50
        End If
#Else
        EnableContextMenus(Me.Controls)
#End If
    End Sub

    Private Sub Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

#Region "simulated lock - ignore keypresses."
    Private Sub txtText_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AboutMessage.KeyDown
        e.Handled = True
    End Sub

    Private Sub AboutMessage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles AboutMessage.KeyPress
        e.Handled = True
    End Sub

    Private Sub AboutMessage_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AboutMessage.KeyUp
        e.Handled = True
    End Sub
#End Region

    Private Sub AboutForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = System.Windows.Forms.Keys.Up) Then
            'Up
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Down) Then
            'Down
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Left) Then
            'Left
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Right) Then
            'Right
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Enter) Then
            'Enter
        End If

    End Sub

    Private Sub LogoPanel_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
