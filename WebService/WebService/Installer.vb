Imports System

Imports System.ComponentModel
Imports System.Configuration

Imports System.Configuration.Install
Imports System.Text.RegularExpressions
Imports Microsoft.Win32

Public Class Installer

    Private logFilePath As String
    'Private log As String

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent
        Try
            Dim logFileName As String = String.Format("activiserWebServiceConfigLog-{0:yyyyMMdd-HHmm}.txt", DateTime.Now)
            Dim tempFolderName As String = Environment.GetEnvironmentVariable("TEMP")
            logFilePath = IO.Path.Combine(tempFolderName, logFileName)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private displayWarning As Boolean = False

    Private Function FixFxVersion(ByRef scriptMapVals As DirectoryServices.PropertyValueCollection, ByVal frameworkVersion As String) As Object
        Dim objScriptMaps As New List(Of String)
        LogMessage("Fixing framework version for property '{0}'", scriptMapVals.PropertyName)
        Dim versionRegex As Regex = New Regex("(?<=\\v)\d{1}\.\d{1}\.\d{1,5}(?=\\)")
        For Each scriptMapVal As String In scriptMapVals
            If scriptMapVal.Contains("Framework") Then
                objScriptMaps.Add(versionRegex.Replace(scriptMapVal, frameworkVersion))
            Else
                objScriptMaps.Add(scriptMapVal)
            End If
        Next
        scriptMapVals.Value = objScriptMaps.ToArray
        Return scriptMapVals.Value
    End Function

    <Flags()> Enum AuthModes
        Anonymous = 1
        Basic = 2
        Ntlm = 4
        MD5 = 16
        Passport = 64
    End Enum

    Private Function GetAppPool() As DirectoryServices.DirectoryEntry
        Try
            LogMessage("Getting application pool from IIS")
            Dim appPools As New DirectoryServices.DirectoryEntry("IIS://localhost/W3SVC/AppPools")
            Dim result As DirectoryServices.DirectoryEntry = Nothing
            Dim template As DirectoryServices.DirectoryEntry = Nothing
            If appPools IsNot Nothing Then
                For Each appPool As DirectoryServices.DirectoryEntry In appPools.Children
                    'r.TextBox1.Text &= appPool.Name & ":" & appPool.SchemaClassName & vbNewLine
                    If appPool.Name = "activiserAppPool" Then
                        result = appPool
                        Exit For
                    ElseIf template Is Nothing AndAlso appPool.Name = "DefaultAppPool" Then
                        template = appPool
                    ElseIf appPool.Name = "ASP.NET V2.0" Then
                        template = appPool
                    End If
                Next

                If result Is Nothing Then
                    If template IsNot Nothing Then
                        LogMessage("Creating new application pool based on '{0}\{1}'", template.Path, template.Name)
                        result = template.CopyTo(template.Parent, "activiserAppPool")
                    Else
                        LogMessage("Creating new application pool in {0}", appPools.Path, appPools.Name)
                        result = appPools.Children.Add("activiserAppPool", "IIsApplicationPool")
                    End If
                    result.CommitChanges()
                End If

                LogMessage("Using application pool '{0}\{1}' for web service.", result.Path, result.Name)
                Return result
            Else
                LogMessage("Don't know where to find/create an application pool (Query of IIS://localhost/W3SVC/AppPools failed).")
                Return Nothing
            End If
        Catch ex As Exception
            LogMessage("Error getting application pool from IIS", ex)
            Return Nothing
        End Try
    End Function

    Private serverUrlBase As String
    Private gatewayUrl As String

    Private Sub SetVirtualDirectoryOptions(ByVal sitePath As String)
        LogMessage("Setting virtual directory options for site '{0}'", sitePath)
        Try
            Dim ds As New DirectoryServices.DirectoryEntry(sitePath)
            If ds IsNot Nothing Then
                Dim pvc As DirectoryServices.PropertyValueCollection
                pvc = ds.Properties("ScriptMaps")

                ds.InvokeSet("ScriptMaps", FixFxVersion(pvc, Me.Context.Parameters("fxversion")))
                ds.InvokeSet("AuthFlags", AuthModes.Basic Or AuthModes.NTLM Or AuthModes.MD5)
                Dim appPool As DirectoryServices.DirectoryEntry = GetAppPool()
                If appPool IsNot Nothing Then
                    ds.InvokeSet("AppPoolId", appPool.Name)
                End If
                ds.CommitChanges()
            End If
        Catch ex As Exception
            LogMessage("Error setting virtual directory options.", ex)
        End Try
    End Sub

    Private Function GetVirtualDirectoryUrl(ByVal sitePath As String, ByVal virtualDirectory As String) As String
        Try
            LogMessage("Getting virtual directory URL for site '{0}', virtual directory '{1}'", sitePath, virtualDirectory)
            Dim ds As New DirectoryServices.DirectoryEntry(sitePath)
            If ds IsNot Nothing Then
                Dim serverBindingString As String = ds.Properties("ServerBindings")(0).ToString
                LogMessage("Virtual directory server bindings: '{0}'", serverBindingString)

                Dim serverBindings() As String = serverBindingString.Split(":"c)
                Dim ipAddress As String = serverBindings(0)
                Dim serverPort As String = serverBindings(1)
                Dim serverHostHeader As String = serverBindings(2)

                If String.IsNullOrEmpty(serverPort) Then serverPort = "80"
                If String.IsNullOrEmpty(serverHostHeader) Then serverHostHeader = ipAddress
                If String.IsNullOrEmpty(serverHostHeader) Then serverHostHeader = "localhost"
                Dim result As String = String.Format("http://{0}:{1}/{2}", serverHostHeader, serverPort, virtualDirectory)
                LogMessage("Returning virtual directory URL: '{0}'", result)
                Return result
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            LogMessage("Error getting virtual directory URL", ex)
            Return String.Empty
        End Try
    End Function

    Private Sub ConfigureConnectionStrings()
        LogMessage("Configuring connection strings.")
        Dim targetDir As String = Me.Context.Parameters("TARGETDIR")
        Dim dbServer As String = Me.Context.Parameters("DBSERVER")
        Dim dbName As String = Me.Context.Parameters("DBNAME")
        Dim dbUser As String = Me.Context.Parameters("DBUSER")
        Dim dbPassword As String = Me.Context.Parameters("DBPASSWORD")
        Dim crmServer As String = Me.Context.Parameters("CRMURL")
        Dim crmUser As String = Me.Context.Parameters("CRMUSER")
        Dim crmPassword As String = Me.Context.Parameters("CRMPASSWORD")
        Dim crmDomain As String = Me.Context.Parameters("CRMDOMAIN")
        Dim gatewayOption As Integer = CInt(Me.Context.Parameters("GATEWAYOPTION"))


        Dim sc As New SqlClient.SqlConnectionStringBuilder() '(connString.ConnectionString)
        sc.DataSource = dbServer
        sc.InitialCatalog = dbName
        If String.IsNullOrEmpty(dbUser) Then
            sc.IntegratedSecurity = True
        Else
            sc.UserID = dbUser
            sc.Password = dbPassword
            sc.IntegratedSecurity = False
        End If

        sc.ApplicationName = String.Format("activiser Web Service V{0}", My.Application.Info.Version())
        LogMessage("Connection string = '{0}'", sc.ConnectionString)

        Dim webConfigPath As String = IO.Path.Combine(targetDir, "web.config")
        LogMessage("Looking for web.config @ '{0}'", webConfigPath)

        Dim wcfi As New IO.FileInfo(webConfigPath)
        If Not wcfi.Exists Then
            Throw New IO.FileNotFoundException("Web Service Installer unable to find web.config", wcfi.FullName)
        End If
        LogMessage("Web.config = '{0}'", wcfi.FullName)

        Try
            If wcfi.IsReadOnly Then
                wcfi.IsReadOnly = False ' can't modify read-only web.config
            End If
        Catch ex As Exception
            LogMessage("Error getting file information for web.config", ex)
        End Try

        Dim fileMap As New System.Configuration.ExeConfigurationFileMap
        fileMap.ExeConfigFilename = webConfigPath
        LogMessage("Loading web.config")
        Dim webConfig As System.Configuration.Configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None)
        LogMessage("Searching connection strings")

        Dim gotActiviserConnString, gotGatewayConnString As Boolean

        For Each connString As System.Configuration.ConnectionStringSettings In webConfig.ConnectionStrings.ConnectionStrings
            If connString.Name = "activiser.My.MySettings.activiserConnectionString" OrElse connString.Name = "activiserConnectionString" Then
                LogMessage("Setting connection string {0}", connString.Name)
                connString.ConnectionString = sc.ConnectionString
                gotActiviserConnString = True
                LogMessage("Connection string {0} = {1}", connString.Name, connString.ConnectionString)
            ElseIf connString.Name = "activiser.My.MySettings.OutputGateway" OrElse connString.Name = "OutputGateway" Then
                LogMessage("Setting connection string {0}", connString.Name)
                connString.ConnectionString = String.Format("GatewayUrl={0}/activisergateway.asmx;CrmUrl={1};User={2};Password={3};Domain={4};", serverUrlBase, crmServer, crmUser, crmPassword, crmDomain)
                gotGatewayConnString = True
                LogMessage("Connection string {0} = {1}", connString.Name, connString.ConnectionString)
            End If
        Next

        ' these two shouldn't be necessary, but are here in case a dodgy web.config gets through...
        If Not gotActiviserConnString Then
            webConfig.ConnectionStrings.ConnectionStrings.Add(New System.Configuration.ConnectionStringSettings("activiser.My.MySettings.activiserConnectionString", sc.ConnectionString))
        End If

        If Not gotGatewayConnString Then
            webConfig.ConnectionStrings.ConnectionStrings.Add(New System.Configuration.ConnectionStringSettings("activiser.My.MySettings.OutputGateway", _
                String.Format("GatewayUrl={0}/activisergateway.asmx;CrmUrl={1};User={2};Password={3};Domain={4};", serverUrlBase, crmServer, crmUser, crmPassword, crmDomain)))
        End If

        Dim webSettings As Web.Configuration.SystemWebSectionGroup = CType(webConfig.GetSectionGroup("system.web"), Web.Configuration.SystemWebSectionGroup)
        webSettings.Identity.Impersonate = True
        webSettings.Compilation.Debug = False

        Dim applicationSettings As System.Configuration.ApplicationSettingsGroup = CType(webConfig.SectionGroups("applicationSettings"), ApplicationSettingsGroup)
        Dim clientSection As System.Configuration.ClientSettingsSection = CType(applicationSettings.Sections("activiser.My.MySettings"), ClientSettingsSection)
        For Each settingElement As System.Configuration.SettingElement In clientSection.Settings()
            If settingElement.Name = "UseICTGateway" Then
                Select Case gatewayOption
                    Case 1
                        settingElement.Value.ValueXml.InnerText = "False"
                    Case 2
                        settingElement.Value.ValueXml.InnerText = "True"
                End Select
                Exit For
            End If
        Next

        LogMessage("Saving web.config")
        webConfig.Save(ConfigurationSaveMode.Minimal, True)

    End Sub

    Private Sub Installer_AfterInstall(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles MyBase.AfterInstall
        LogMessage("Post-Install starting.")

        Try
            For Each de As DictionaryEntry In Me.Context.Parameters
                LogMessage(String.Format("Parameter {0} = {1}", de.Key, de.Value))
            Next
        Catch ex As Exception

        End Try

        Try
            LogMessage("Installing activiser Event Log class...")
            Me.EventLogInstaller1.Install(e.SavedState)
        Catch ex As System.ArgumentException
            LogMessage(String.Format("activiser Event Log class already exists."))
        Catch ex As Exception
            LogMessage(String.Format("Event Log Installer failed: {0}", ex.ToString))
        End Try

        Try
            LogMessage("Getting site/vdir parameters from MSI")
            If Not Me.Context.Parameters.ContainsKey("site") OrElse Not Me.Context.Parameters.ContainsKey("vdir") Then
                Throw New InstallException("Error in custom installer arguments, site or vdir missing.")
            End If
            Dim site As String = Me.Context.Parameters.Item("site")
            Dim virtualDir As String = Me.Context.Parameters("vdir")
            Dim virtualDirectoryAdsiPath As String = String.Format("IIS:/{0}/ROOT/{1}", site.Replace("/LM/", "/localhost/"), virtualDir)
            Dim virtualServerAdsiPath As String = String.Format("IIS:/{0}", site.Replace("/LM/", "/localhost/"))

            serverUrlBase = GetVirtualDirectoryUrl(virtualServerAdsiPath, virtualDir)
            LogMessage("ServerUrlBase = {0}", serverUrlBase)

            SetVirtualDirectoryOptions(virtualDirectoryAdsiPath)
            ConfigureConnectionStrings()

            LogMessage("Saving my location to Registry")
            Dim rk As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\activiser\activiserWebService", True)
            rk.SetValue("WebServiceURL", serverUrlBase)

            Dim serverTestThread As System.Threading.Thread = Nothing
            If Me.Context.Parameters("testserver") = "1" Then
                Dim cmd As String = String.Format("{0}/activiser.asmx", serverUrlBase)
                serverTestThread = New System.Threading.Thread(AddressOf StartBrowser)
                serverTestThread.Start(cmd)
                'LogMessage("Server test requested, firing up {0}", cmd)
                'System.Diagnostics.Process.Start(cmd)
                ' Threading.Thread.Sleep(2000)
            End If

            Dim gatewayTestThread As System.Threading.Thread = Nothing
            If Me.Context.Parameters("testgateway") = "1" Then
                Dim cmd As String = String.Format("{0}/activisergateway.asmx", serverUrlBase)
                gatewayTestThread = New System.Threading.Thread(AddressOf StartBrowser)
                gatewayTestThread.Start(cmd)
                'LogMessage("Server test requested, firing up {0}", cmd)
                'System.Diagnostics.Process.Start(cmd)
                ' Threading.Thread.Sleep(2000)
            End If

            If serverTestThread IsNot Nothing Then
                serverTestThread.Join()
            End If

            If gatewayTestThread IsNot Nothing Then
                gatewayTestThread.Join()
            End If

        Catch ex As Exception
            LogMessage("Installer_AfterInstall failed", ex)
            'Me.displayWarning = True
            'Throw
        End Try



        If Me.displayWarning Then
            Dim message As String
            message = String.Format("{1}{0}{2}{0}{3}", vbNewLine, _
                "An error occurred while trying configure your activiser™ web service.", _
                "This is not fatal, but means manual configuration of the web service will be required", _
                "Details of the error(s) can be found in the installation log file" _
                )
            Windows.Forms.MessageBox.Show(message, "activiser™ Web Service Setup", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Warning, Windows.Forms.MessageBoxDefaultButton.Button1)
        End If
    End Sub

    Private Sub StartBrowser(ByVal url As Object)
        Dim urlString As String = CStr(url)
        Threading.Thread.CurrentThread.Name = urlString
        Threading.Thread.CurrentThread.IsBackground = False
        LogMessage("Browser requested for url: {0}", urlString)
        Process.Start("IExplore.exe", urlString)

        Threading.Thread.Sleep(0)
    End Sub

#Region "Message logging"
    Private messageLogNumber As Integer = 1

    Private Sub LogMessage(ByVal message As String)
        Dim formattedMessage As String = String.Format("({0}) {1:yyyy-MM-dd HH:mm:ss.ff} : {2}{3}", messageLogNumber, DateTime.Now, message, vbNewLine)

        My.Computer.FileSystem.WriteAllText(logFilePath, formattedMessage, True)
        'Dim messageLog As IO.StreamWriter
        'messageLog = New System.IO.StreamWriter(logFilePath, True, System.Text.Encoding.UTF8)
        'messageLog.AutoFlush = True
        'messageLog.NewLine = vbCrLf
        'log &= formattedMessage
        'messageLog.WriteLine(formattedMessage)
        Me.Context.LogMessage(formattedMessage)
        messageLogNumber += 1
    End Sub

    Private Sub LogMessage(ByVal message As String, ByVal ParamArray messageArgs() As String)
        If messageArgs Is Nothing OrElse messageArgs.Length = 0 Then
            LogMessage(message)
        Else
            LogMessage(String.Format(message, messageArgs))
        End If
    End Sub

    Private Sub LogMessage(ByVal message As String, ByVal ex As Exception)
        displayWarning = True
        If ex Is Nothing Then
            LogMessage(message)
        Else
            LogMessage(String.Format("{1}{0}Exception details: {2}", vbNewLine, message, ex.ToString))
        End If
    End Sub

#End Region

End Class
