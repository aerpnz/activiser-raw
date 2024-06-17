Public NotInheritable Class SplashScreen
    Const MODULENAME As String = "SplashScreen"

    Private _amAboutBox As Boolean = True

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Terminology.LoadLabels(Me)
        'Application title
        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'If the application title is missing, use the application name, without the extension
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.ApplicationTitle.Text = Terminology.GetString(MODULENAME, "ApplicationTitle")
        Version.Text = Terminology.GetFormattedString(MODULENAME, "VersionTemplate", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
        Copyright.Text = My.Application.Info.Copyright
    End Sub

    ''
    '' to enable use as an About box and a Splash Screen !
    Private Sub MainLayoutPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MainLayoutPanel.MouseClick
        If _amAboutBox Then
            Me.Close()
        End If
    End Sub

    Private Sub Copyright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Copyright.Click, Version.Click, ApplicationTitle.Click
        If _amAboutBox Then
            Me.Close()
        End If
    End Sub

    Private Sub SplashScreen_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ConsoleUser IsNot Nothing Then
            _amAboutBox = True
        End If
    End Sub

    Private _mouseDown As Boolean
    Private _mouseDownLocation As Point
    Private Sub Caption_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        _mouseDown = True
        _mouseDownLocation = e.Location
    End Sub

    Private Sub Caption_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If _mouseDown Then
            Me.Location = New Point(Me.Location.X + (-_mouseDownLocation.X + e.X), Me.Location.Y + (-_mouseDownLocation.Y + e.Y))
        End If
    End Sub

    Private Sub Caption_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        _mouseDown = False
    End Sub
End Class
