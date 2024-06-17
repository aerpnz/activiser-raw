Public NotInheritable Class SplashScreen
    Const MODULENAME As String = "SplashScreen"

    Private _amAboutBox As Boolean

    Private Sub SplashScreen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If MessageBox.Show("Stop loading activiser custom form editor?", My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Dim cea As New System.ComponentModel.CancelEventArgs(False)
                Application.Exit(cea)
                Me.Close()
            End If
        End If
    End Sub

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Application title
        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'If the application title is missing, use the application name, without the extension
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.ApplicationTitle.Text = My.Application.Info.Title
        Me.Version.Text = My.Application.Info.Version.ToString(4)
        Me.Copyright.Text = My.Application.Info.Copyright

        If Not String.IsNullOrEmpty(My.Settings.designerConnectionString) Then
            Try
                Using sc As New SqlClient.SqlConnection(My.Settings.designerConnectionString)
                    Me.SetUrl(sc.DataSource & "." & sc.Database)
                End Using
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Delegate Sub SetUrlDelegate(ByVal url As String)
    Public Sub SetUrl(ByVal url As String)
        If Me.InvokeRequired Then
            Dim d As New SetUrlDelegate(AddressOf SetUrl)
            Me.Invoke(d, url)
            Return
        End If
        Me.ServerUrlLabel.Text = url
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
        _amAboutBox = sqlConnection IsNot Nothing
        If Not Me._amAboutBox Then
            Me.ProgressBar1.Visible = True
            Me.ProgressBar1.Enabled = True
            If My.Computer.Info.OSVersion > "5.0" Then ' OsLevel > 500 Then
                Me.ProgressBar1.Style = ProgressBarStyle.Marquee
            Else
                Me.Timer1.Start()
                Me.ProgressBar1.Style = ProgressBarStyle.Continuous
            End If
        End If
    End Sub

    Private _mouseDown As Boolean
    Private _mouseDownLocation As Point
    Private Sub Caption_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, Version.MouseDown, MainLayoutPanel.MouseDown, Copyright.MouseDown, ApplicationTitle.MouseDown
        _mouseDown = True
        _mouseDownLocation = e.Location
        Application.DoEvents()
    End Sub

    Private Sub Caption_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, Version.MouseMove, MainLayoutPanel.MouseMove, Copyright.MouseMove, ApplicationTitle.MouseMove
        If _mouseDown Then
            Me.Location = New Point(Me.Location.X + (-_mouseDownLocation.X + e.X), Me.Location.Y + (-_mouseDownLocation.Y + e.Y))
            Application.DoEvents()
        End If
    End Sub

    Private Sub Caption_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, Version.MouseUp, MainLayoutPanel.MouseUp, Copyright.MouseUp, ApplicationTitle.MouseUp
        _mouseDown = False
        Application.DoEvents()
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
            Me.ServerUrlLabel.Text = My.Settings.designerConnectionString
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

End Class
