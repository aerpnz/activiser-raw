Namespace My

    ' The following events are availble for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_NetworkAvailabilityChanged(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            If e.IsNetworkAvailable Then
                If My.Settings.EnableDatabasePolling Then
                    Console.ConsoleData.StartPolling()
                End If
            Else
                Console.ConsoleData.StopPolling()
            End If
        End Sub

        Friend ClosedByMainForm As Boolean

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            TraceEvent("Activiser Console Version {0} Shutdown ", My.Application.Info.Version)
        End Sub

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            TraceInit()
            TraceEvent("Activiser Console Version {0} Started ", My.Application.Info.Version)

            If OsLevel < 500 Then ' win2k minimum supported version
                MessageBox.Show(My.Resources.UnsupportedOperatingSystem, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Cancel = True
                'ElseIf OsLevel > 500 Then
                'Me.EnableVisualStyles = True
                'Else ' OS is Windows 2000

            End If
            'setup custom handling of potential certificate problems.
            Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf RemoteCertificateValidationCallback
        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            TraceError(e.Exception)
            If ClosedByMainForm Then Return
            If ConsoleData.InitialLoadCancelled Then Return
            If OsLevel = 500 Then
                If e.Exception.StackTrace.Contains("HideSplashScreen") Then
                    e.ExitApplication = False
                    Return
                End If
            End If
            Library.DisplayException.Show(e.Exception, "Please restart the activiser™ console. If this problem persists, please contact your support team", Library.Icons.FirstAid)
            e.ExitApplication = False

            '#If DEBUG Then
            '#Else
            '            Dim sav As System.AccessViolationException = TryCast(e.Exception, System.AccessViolationException)
            '            If sav IsNot Nothing Then ' AndAlso ConsoleData.MainForm IsNot Nothing AndAlso Not ConsoleData.MainForm.Initialised Then
            '                ' HACK: release version gets some access violation
            '                e.ExitApplication = False
            '            Else
            '                Library.DisplayException.Show(e.Exception, Library.Icons.FirstAid)
            '            End If
            '#End If
        End Sub
    End Class

End Namespace

