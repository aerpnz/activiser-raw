Public NotInheritable Class SplashScreen
    Const MODULENAME As String = "SplashScreen"

    Private _amAboutBox As Boolean

    Public Property IsAboutBox() As Boolean
        Get
            Return _amAboutBox
        End Get
        Set(ByVal value As Boolean)
            _amAboutBox = value
        End Set
    End Property

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If MessageBox.Show("Stop loading activiser console?", My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                ConsoleData.WebService.Abort()
                ConsoleData.InitialLoadCancelled = True
                Dim cea As New System.ComponentModel.CancelEventArgs(False)
                Application.Exit(cea)
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Terminology.LoadLabels(Me)
        'Application title
        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'If the application title is missing, use the application name, without the extension
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.ApplicationTitle.Text = Terminology.GetString(MODULENAME, "ApplicationTitle")
        'Dim filVer As FileVersionInfo = Diagnostics.FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location)

        'Dim asmver As New Version(filVer.FileMajorPart, filVer.FileMinorPart, filVer.FileBuildPart, filVer.FilePrivatePart)

        Version.Text = Terminology.GetFormattedString(MODULENAME, "VersionTemplate", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
        Copyright.Text = My.Application.Info.Copyright

        Dim userRegistryServerUrl As String
        Dim commandLineUrl As String
        Dim serverUrl As String

        Try
            userRegistryServerUrl = TryCast(userRegistryBase.GetValue(My.Resources.RegistryServerUrlValueName), String)
        Catch ex As Exception
            userRegistryServerUrl = String.Empty
        End Try

        Try
            Dim clUri As Uri = Nothing
            If My.Application.CommandLineArgs.Count > 0 AndAlso Uri.TryCreate(My.Application.CommandLineArgs(0), UriKind.Absolute, clUri) Then
                ' got a command-line URL
                commandLineUrl = clUri.ToString()
            Else
                commandLineUrl = String.Empty
            End If
        Catch ex As Exception
            commandLineUrl = String.Empty
        End Try

        If Not String.IsNullOrEmpty(commandLineUrl) Then
            serverUrl = commandLineUrl
        ElseIf Not String.IsNullOrEmpty(userRegistryServerUrl) Then
            serverUrl = userRegistryServerUrl
        ElseIf Not String.IsNullOrEmpty(My.Settings.activiserServerUrl) Then
            serverUrl = My.Settings.activiserServerUrl
        Else
            serverUrl = "<Server Unknown>"
        End If

        Me.ServerUrlLabel.Text = serverUrl
    End Sub

    ''
    '' to enable use as an About box and a Splash Screen !
    Private Sub MainLayoutPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MainLayoutPanel.MouseClick
        If IsAboutBox Then
            Me.Close()
        End If
    End Sub

    Private Sub Copyright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Copyright.Click, Version.Click, ApplicationTitle.Click
        If IsAboutBox Then
            Me.Close()
        End If
    End Sub

    Private Sub SplashScreen_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        IsAboutBox = ConsoleUser IsNot Nothing
        If Not Me.IsAboutBox Then
            Me.ProgressBar1.Visible = True
            Me.ProgressBar1.Enabled = True
            If OsLevel > 500 Then
                Me.ProgressBar1.Style = ProgressBarStyle.Marquee
            Else
                Me.Timer1.Start()
                Me.ProgressBar1.Style = ProgressBarStyle.Continuous
            End If
        Else
            Me.ProgressBar1.Visible = False
        End If
    End Sub

    Private _mouseDown As Boolean
    Private _mouseDownLocation As Point
    Private Sub Caption_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, Version.MouseDown, MainLayoutPanel.MouseDown, Copyright.MouseDown, ApplicationTitle.MouseDown
        _mouseDown = True
        _mouseDownLocation = e.Location
        'Application.DoEvents()
        Me.Refresh()
    End Sub

    Private Sub Caption_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, Version.MouseMove, MainLayoutPanel.MouseMove, Copyright.MouseMove, ApplicationTitle.MouseMove
        If _mouseDown Then
            Me.Location = New Point(Me.Location.X + (-_mouseDownLocation.X + e.X), Me.Location.Y + (-_mouseDownLocation.Y + e.Y))
            'Application.DoEvents()
            Me.Refresh()
        End If
    End Sub

    Private Sub Caption_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, Version.MouseUp, MainLayoutPanel.MouseUp, Copyright.MouseUp, ApplicationTitle.MouseUp
        _mouseDown = False
        'Application.DoEvents()
        Me.Refresh()
    End Sub

    Private Delegate Sub SetUrlTextDelegate(ByVal serverUrl As String)
    Public Sub SetUrlText(ByVal serverUrl As String)
        If Me.InvokeRequired Then
            Dim d As New SetUrlTextDelegate(AddressOf SetUrlText)
            Me.Invoke(d, serverUrl)
        Else
            Me.ServerUrlLabel.Text = serverUrl
        End If
    End Sub

    Private Delegate Sub HideSplashScreenDelegate()
    Public Sub HideSplashScreen()
        If Me.InvokeRequired Then
            Dim d As New HideSplashScreenDelegate(AddressOf HideSplashScreen)
            Me.Invoke(d)
        Else
            Me.Visible = False
        End If
    End Sub

    Private Delegate Sub ShowSplashScreenDelegate()
    Public Sub ShowSplashScreen()
        If Me.InvokeRequired Then
            Dim d As New ShowSplashScreenDelegate(AddressOf ShowSplashScreen)
            Me.Invoke(d)
        Else
            Me.ServerUrlLabel.Text = My.Settings.activiserServerUrl
            Me.Visible = True
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.ProgressBar1.Value < Me.ProgressBar1.Maximum Then
            Me.ProgressBar1.Value += 1
        Else
            Me.ProgressBar1.Value = Me.ProgressBar1.Minimum
        End If
    End Sub

    Private Sub SplashScreen_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDoubleClick
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub MainPanel_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainLayoutPanel.DoubleClick, Version.DoubleClick, Copyright.DoubleClick, ApplicationTitle.DoubleClick
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
