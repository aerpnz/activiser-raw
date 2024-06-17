Imports System.Reflection
Imports activiser.Library

Module Startup
    Const MODULENAME As String = "Startup"

    Public Sub LocateDatabase()
        gDatabaseRoot = AppConfig.GetSetting(My.Resources.AppConfigDatabaseRootFolderKey, My.Resources.AppConfigDatabaseRootFolderDefault)
        If gDatabaseRoot = "." Then ' default folder
            gDatabaseRoot = New FileInfo([Assembly].GetExecutingAssembly.GetName().CodeBase).DirectoryName
        End If

        Dim dbRoot As New DirectoryInfo(gDatabaseRoot)

        gErrorFolder = AppConfig.GetSetting(My.Resources.AppConfigErrorFolderKey, My.Resources.AppConfigErrorFolderDefault)
        If Not gErrorFolder.StartsWith(System.IO.Path.DirectorySeparatorChar, StringComparison.Ordinal) Then
            gErrorFolder = Path.Combine(dbRoot.FullName, gErrorFolder)
        End If
        If Not (New System.IO.DirectoryInfo(gErrorFolder)).Exists Then
            System.IO.Directory.CreateDirectory(gErrorFolder)
        End If

        gDatabaseFolder = AppConfig.GetSetting(My.Resources.AppConfigDataFolderKey, My.Resources.AppConfigDataFolderDefault)
        If Not gDatabaseFolder.StartsWith(System.IO.Path.DirectorySeparatorChar, StringComparison.Ordinal) Then
            gDatabaseFolder = Path.Combine(dbRoot.FullName, gDatabaseFolder)
        End If
        If Not (New System.IO.DirectoryInfo(gDatabaseFolder)).Exists Then
            System.IO.Directory.CreateDirectory(gDatabaseFolder)
        End If

        gTransactionFolder = AppConfig.GetSetting(My.Resources.AppConfigPendingFolderKey, My.Resources.AppConfigPendingFolderDefault)
        If Not gTransactionFolder.StartsWith(System.IO.Path.DirectorySeparatorChar, StringComparison.Ordinal) Then
            gTransactionFolder = Path.Combine(dbRoot.FullName, gTransactionFolder)
        End If
        If Not (New System.IO.DirectoryInfo(gTransactionFolder)).Exists Then
            System.IO.Directory.CreateDirectory(gTransactionFolder)
        End If

        gSchemaFolder = AppConfig.GetSetting(My.Resources.AppConfigSchemaFolderKey, My.Resources.AppConfigSchemaFolderDefault)
        If Not gSchemaFolder.StartsWith(System.IO.Path.DirectorySeparatorChar, StringComparison.Ordinal) Then
            gSchemaFolder = Path.Combine(dbRoot.FullName, gSchemaFolder)
        End If
        If Not (New System.IO.DirectoryInfo(gSchemaFolder)).Exists Then
            System.IO.Directory.CreateDirectory(gSchemaFolder)
        End If

        gMainDbFileName = AppConfig.GetSetting(My.Resources.AppConfigMainDbKey, My.Resources.AppConfigMainDbDefault)
        gConfigDbFileName = AppConfig.GetSetting(My.Resources.AppConfigSettingsKey, My.Resources.AppConfigSettingsDefault)
        gLocalItemsFileName = AppConfig.GetSetting(My.Resources.AppConfigLocalItemsKey, My.Resources.AppConfigLocalItemsDefault)
        gErrorLogDbFileName = AppConfig.GetSetting(My.Resources.AppConfigEventLogKey, My.Resources.AppConfigEventLogDefault)
        gGpsFileName = AppConfig.GetSetting(My.Resources.AppConfigGpsLogKey, My.Resources.AppConfigGpsLogDefault)
        gFormDefinitionFileName = AppConfig.GetSetting(My.Resources.AppConfigFormDefinitionsKey, My.Resources.AppConfigFormDefinitionsDefault)
    End Sub

    Public Sub LoadCustomTerminology()
        gTerminologyFileName = AppConfig.GetSetting(My.Resources.AppConfigLanguageFileKey, My.Resources.AppConfigLanguageFileDefault)
        Terminology.FileName = gTerminologyFileName
        Terminology.Load()
    End Sub

    ''' <summary>
    ''' Initialises the global web service proxy instance
    ''' </summary>
    ''' <param name="suppressDialog">Prevents display of username/password dialog in the event that there is missing information</param>
    ''' <returns>True on success, False if there is any kind of problem.</returns>
    ''' <remarks></remarks>
    Public Function InitialiseWebServiceProxy(ByVal owner As Form, ByVal suppressDialog As Boolean) As Boolean
        Const METHODNAME As String = "InitialiseWebServiceProxy"
        Try
            'gWebServer = New Library.WebService.activiser
            'gWebServer.UserAgent = My.Resources.UserAgentString & GetVersion().ToString()

            Try
                gWebServer = New Library.WebService.activiser(gServerUrl)
                'gWebServer.Url = gServerUrl
            Catch ex As System.UriFormatException
                Terminology.DisplayFormattedMessage(owner, MODULENAME, "UrlError", MessageBoxIcon.Hand, gServerUrl)
                Return False
            End Try
            gWebServer.Timeout = gWebServiceTimeout
            'gWebServer.EnableDecompression = True

            Dim proxyString As String = AppConfig.GetSetting(My.Resources.AppConfigProxyServerKey, String.Empty)
            If proxyString <> String.Empty Then
                gWebServer.Proxy = New Net.WebProxy(proxyString)
            End If

            gDomainUsername = GetRegistryValue(My.Resources.RegistryDomainUserNameKey, gDomainUsername)
            If gDomainUsername <> String.Empty Then
                gDomain = GetRegistryValue(My.Resources.RegistryDomainKey, gDomain)
                'Dim passwordB64 As String = GetRegistryValue(My.Resources.RegistryDomainPasswordKey, String.Empty)
                'gDomainPassword = DecryptPassword(passwordB64)
                gDomainPassword = GetRegistryValue(My.Resources.RegistryDomainPasswordKey, String.Empty)

                If Not suppressDialog AndAlso String.IsNullOrEmpty(gDomainPassword) Then ' no password saved, pop-up credential dialog
                    Using cd As New CredentialDialog(Nothing, gDomainUsername, Decrypt(gDeviceID, gDomainPassword), gDomain)
                        If cd.ShowDialog = DialogResult.OK Then
                            gSavePassword = cd.SavePassword
                            gDomainPassword = Encrypt(gDeviceID, cd.Password)
                            gDomainUsername = cd.UserName
                            gDomain = cd.DomainName
                            cd.CloseDialog()
                            SetRegistryValue(My.Resources.RegistryDomainUserNameKey, gDomainUsername)
                            SetRegistryValue(My.Resources.RegistryDomainKey, gDomain)
                            If Not String.IsNullOrEmpty(gDomainPassword) AndAlso gSavePassword Then
                                SetRegistryValue(My.Resources.RegistryDomainPasswordKey, gDomainPassword)
                            Else
                                SetRegistryValue(My.Resources.RegistryDomainPasswordKey, String.Empty)
                            End If
                        End If
                    End Using
                End If

                If Not String.IsNullOrEmpty(gDomain) Then
                    gWebServer.Credentials = New Net.NetworkCredential(gDomainUsername, Decrypt(gDeviceID, gDomainPassword), gDomain)
                Else
                    gWebServer.Credentials = New Net.NetworkCredential(gDomainUsername, Decrypt(gDeviceID, gDomainPassword))
                End If
            Else
                gWebServer.Credentials = Nothing
            End If

            'Future:
            gUsername = GetRegistryValue(My.Resources.RegistryUserNameKey, String.Empty)
            gPassword = GetRegistryValue(My.Resources.RegistryPasswordKey, String.Empty)

            System.Net.ServicePointManager.CertificatePolicy = Nothing ' destroy initial setup version, if there.
            System.Net.ServicePointManager.CertificatePolicy = New CertificatePolicy()
            Return True

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, Nothing)
            Return False
        End Try
    End Function

End Module
