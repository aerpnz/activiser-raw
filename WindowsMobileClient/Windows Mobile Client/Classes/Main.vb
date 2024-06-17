Option Explicit On 
Option Strict On

Imports Microsoft.WindowsCE.Forms
Imports Microsoft.VisualBasic
Imports System

'Imports WS = Activiser.WebService

Module Main
    Friend SetupNeeded As Boolean

    Sub main()
        If Environment.Version.CompareTo(New System.Version(My.Resources.NetFxMinimumVersion)) < 0 Then
            Terminology.DisplayMessage(Nothing, "General", "NetFxMinimumVersionMessage", MessageBoxIcon.Hand)
            Return
        End If

        gDeviceID = GetDeviceID()
        gDeviceIDString = String.Format(WithoutCulture, "{0};{1}", GetVersion().ToString(4), gDeviceID)

        System.Net.ServicePointManager.CertificatePolicy = New CertificatePolicy()

        LocateDatabase() ' get database locations; required for initial setup and normal use.

        SetupNeeded = String.IsNullOrEmpty(GetRegistryValue(My.Resources.RegistryServerUrlKey, String.Empty)) _
            OrElse Not GetRegistryValue(My.Resources.RegistryServerUrlKey, String.Empty).IsUrl(UriKind.Absolute) _
            OrElse Not (New FileInfo(Path.Combine(gDatabaseFolder, gConfigDbFileName & My.Resources.XmlFileType))).Exists

        'check for new installation; ServerUrl will be empty for a new installation.
        If SetupNeeded Then
            gUsername = GetRegistryValue(My.Resources.RegistryUserNameKey, String.Empty)
            gPassword = GetRegistryValue(My.Resources.RegistryPasswordKey, String.Empty)

            gDomain = GetRegistryValue(My.Resources.RegistryDomainKey, String.Empty)
            gDomainUsername = GetRegistryValue(My.Resources.RegistryDomainUserNameKey, String.Empty)

            gDomainPassword = GetRegistryValue(My.Resources.RegistryDomainPasswordKey, String.Empty)

            gServerUrl = ConfigurationSettings.GetValue(My.Resources.AppConfigServerUrlKey, My.Resources.DefaultUrl)

            gWebServiceTimeout = ConfigurationSettings.GetValue(My.Resources.AppConfigServerTimeoutKey, 30) * 1000

            Using sf As New SetupForm
                sf.ControlBox = False ' disable control box on initial setup screen.  
                sf.MinimizeBox = False
                If Not sf.ShowDialog() = DialogResult.OK Then
                    Exit Sub
                End If
            End Using
        Else
            gServerUrl = ConfigurationSettings.GetValue(My.Resources.AppConfigServerUrlKey, GetRegistryValue(My.Resources.RegistryServerUrlKey, My.Resources.DefaultUrl))
        End If

        Try
            If StartupForm.ShowDialog() <> DialogResult.Abort AndAlso gLoadSuccessful Then
                gMainForm = New MainForm()
                Application.Run(gMainForm)
            End If
        Catch ex As Exception
            LogErrorToFile(ex)
#If Not Debug Then
            ErrorDialog.DisplayError(Nothing, ex)
#End If
        Finally
            ConfigurationSettings.Save()
            gMainForm = Nothing

            If Gps.GpsEnabled Then
                Gps.StopGps()
            End If
            'For Each t As Threading.Thread In Threading.Thread
        End Try

        Debug.WriteLine("application exiting...")

    End Sub
End Module

