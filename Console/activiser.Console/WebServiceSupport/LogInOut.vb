Imports activiser.Library.activiserWebService

Friend Module LogInOut
    Private Const MODULENAME As String = "LogInOut"
    Const STR_UserLinkConfirmationTemplate As String = "UserLinkConfirmationTemplate"
    Const STR_LoginFailure As String = "LoginFailure"
    Const STR_UnhandledError As String = "UnhandledError"
    Const STR_LoginFormUserLinkFailure As String = "UserLinkFailure"
    Const STR_WebServiceAuthenticationFailure As String = "WebServiceAuthenticationFailure"
    Const STR_WebServiceCommunicationSuccessful As String = "WebServiceCommunicationSuccessful"
    Const STR_WebServiceCommunicationFailure As String = "WebServiceCommunicationFailure"
    Friend LoggedOn As Boolean


    Friend Function Logon() As Boolean
        TraceVerbose("Entered")
        Dim serverUrl As String = Nothing
        Try
            LoggedOn = False

            Dim userRegistryServerUrl As String
            Dim commandLineUrl As String
            Dim setupRequired As Boolean
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
            End If

            setupRequired = String.IsNullOrEmpty(serverUrl)

            If My.Settings.AutoDomainLogon AndAlso Not My.Computer.Keyboard.ShiftKeyDown AndAlso Not setupRequired Then
                If TryLogon(serverUrl, True, Nothing, Nothing, False, Nothing) Then
                    LoggedOn = True
                    Return True
                End If
            End If

            Using loginForm As New LoginForm
                Dim logonDialogResult As System.Windows.Forms.DialogResult = loginForm.ShowDialog()
                If logonDialogResult = Windows.Forms.DialogResult.OK AndAlso ConsoleUser IsNot Nothing Then
                    LoggedOn = True
                    serverUrl = loginForm.ServerUrlTextBox.Text
                    Return True
                Else
                    Return False
                End If
            End Using

        Catch ex As Exception
            TraceError(ex)
        Finally
#If ACTIVISERCONSOLE Then
            If Not String.IsNullOrEmpty(serverUrl) andalso SplashScreen.Visible Then
                SplashScreen.SetUrlText(serverUrl)
            End If
#End If
            TraceVerbose("Done")
        End Try
    End Function


    Friend Function TryLogon(ByVal serverUrl As String, ByVal useIntegratedAuthentication As Boolean, ByVal username As String, ByVal password As String, ByVal linkUser As Boolean, ByVal owner As Form) As Boolean
        TraceVerbose("Entered")
        If LoggedOn Then Logout()

        Try
            Dim credentials As Net.ICredentials = Nothing

            If Not TestUrl(serverUrl, credentials, owner) Then
                SplashScreen.HideSplashScreen()
                Terminology.DisplayMessage(MODULENAME, STR_WebServiceCommunicationFailure, MessageBoxIcon.Warning)
                Return False
            End If

            Dim user As Utility.UserRow
            If useIntegratedAuthentication Then
                user = TestUserLogin(serverUrl, Nothing, Nothing, owner)
            Else
                user = TestUserLogin(serverUrl, username, password, owner)
            End If

            If user IsNot Nothing Then
                ConsoleData.WebService.Url = serverUrl
                My.Settings.activiserServerUrl = serverUrl

                ConsoleData.WebService.Credentials = credentials
                ConsoleData.WebService.PreAuthenticate = credentials IsNot Nothing
                ConsoleData.WebService.Proxy = GetProxy()
                ConsoleUser = user
                If linkUser Then
                    If String.IsNullOrEmpty(password) Then
                        password = "*"
                    Else
                        password = GetHash(password)
                    End If

                    If ConsoleUser.IsDomainLogonNull Then
                        LinkNetworkUser(CType(user.Table, Utility.UserDataTable), password)
                    ElseIf ConsoleUser.DomainLogon <> My.User.Name Then
                        Select Case Terminology.AskFormattedQuestion(MODULENAME, STR_UserLinkConfirmationTemplate, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button2, ConsoleUser.Name, My.User.Name)
                            Case Windows.Forms.DialogResult.Yes
                                LinkNetworkUser(CType(user.Table, Utility.UserDataTable), password)
                            Case Windows.Forms.DialogResult.No
                                Exit Select
                            Case Windows.Forms.DialogResult.Cancel
                                Return False
                        End Select
                    End If
                End If
            Else
                Terminology.DisplayMessage(MODULENAME, STR_LoginFailure, MessageBoxIcon.Error)
                Return False
            End If
            LoggedOn = True
            Return True
        Catch wex As System.Net.WebException
            TraceError(wex)
            Terminology.DisplayFormattedMessage(MODULENAME, "WebServiceCommunicationFailure", MessageBoxIcon.Warning, wex.Message)
        Catch ex As Exception
            TraceError(ex)
            activiser.Library.DisplayException.Show(ex, Terminology.GetString(MODULENAME, STR_UnhandledError), activiser.Library.Icons.Critical)
        Finally
            TraceVerbose("Done")
        End Try

    End Function

    Friend Function Logout() As Boolean
        TraceVerbose("Entered")
        Try
            If ConsoleUser IsNot Nothing OrElse LoggedOn Then
                ConsoleUser = Nothing
                LoggedOn = False
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        Finally
            TraceVerbose("Done")
        End Try
    End Function

    Friend Sub LinkNetworkUser(ByVal cudt As Utility.UserDataTable, ByVal pw As String)
        TraceVerbose("Entered")
        Try
            If ConsoleData.WebService.ConsoleLinkNetworkUser(DeviceId, cudt, pw) Then
                ConsoleUser.DomainLogon = My.User.Name
                My.Settings.AutoDomainLogon = True
                My.Settings.Save()
            Else
                Terminology.DisplayMessage(MODULENAME, STR_LoginFormUserLinkFailure, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            TraceError(ex)
        Finally
            TraceVerbose("Done")
        End Try
    End Sub

    Friend Function TestUserLogin(ByVal serverUrl As String, ByVal activiserUser As String, ByVal activiserPassword As String, ByVal owner As Form) As Utility.UserRow
        TraceVerbose("Entered")
        Dim credentials As Net.ICredentials = Nothing

        If Not TestUrl(serverUrl, credentials, owner) Then
            Return Nothing
        End If

        Try
            Dim ws As New Library.activiserWebService.activiser
            ws.Url = serverUrl
            ws.Proxy = GetProxy()
            ws.UnsafeAuthenticatedConnectionSharing = True

            ws.PreAuthenticate = credentials IsNot Nothing
            ws.Credentials = credentials

            Dim cudt As Utility.UserDataTable

            If String.IsNullOrEmpty(activiserUser) Then
                cudt = ws.ConsoleLogonNetworkUser(DeviceId)
            Else
                Dim pw As String = GetHash(activiserPassword)
                cudt = ws.ConsoleLogonConsultant(DeviceId, activiserUser, pw)
            End If

            If cudt IsNot Nothing AndAlso cudt.Rows.Count = 1 Then
                Return cudt(0)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            TraceError(ex)
            Throw
        Finally
            TraceVerbose("Done")
        End Try
    End Function

    Friend Sub InteractiveUrlTest(ByVal serverUrl As String, ByVal owner As Form)
        TraceVerbose("Entered")
        Dim credentials As Net.ICredentials = Nothing
        If TestUrl(serverUrl, credentials, owner) Then
            Terminology.DisplayMessage(MODULENAME, STR_WebServiceCommunicationSuccessful, MessageBoxIcon.Information)
        Else
            Terminology.DisplayMessage(MODULENAME, STR_WebServiceCommunicationFailure, MessageBoxIcon.Warning)
        End If
        TraceVerbose("Done")
    End Sub

    Friend Function TestUrl(ByVal serverUrl As String, ByRef credentials As Net.ICredentials, ByVal owner As Form) As Boolean
        TraceVerbose("Entered")
        Dim result As Boolean = False

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        ' first try 'Windows Integrated Authentication.
        Try
            Dim ws As New Library.activiserWebService.activiser()
            ws.Url = serverUrl
            If ws.CheckDeviceId(deviceId) Then
                Return True
            End If
        Catch ex As Exception

        End Try

        Try
            Dim testUri As New Uri(serverUrl)
            Dim newWebRequest As System.Net.HttpWebRequest
            newWebRequest = CType(Net.WebRequest.Create(testUri), Net.HttpWebRequest)

            If newWebRequest IsNot Nothing Then
                newWebRequest.UnsafeAuthenticatedConnectionSharing = True


                If testUri.IsLoopback Then ' local host
                    TraceVerbose("Locally hosted")
                End If

                Dim r As System.Net.WebResponse
                Dim hr As System.Net.HttpWebResponse

                Dim promptResult As CredUIReturnCodes = GetUrlCredentials(testUri, credentials, owner, False)

                If promptResult = CredUIReturnCodes.NO_ERROR Then
                    Dim credentialCache As System.Net.ICredentials = credentials
                    newWebRequest.PreAuthenticate = credentialCache IsNot Nothing
                    newWebRequest.Credentials = credentialCache
                    newWebRequest.Proxy = GetProxy()

                    Try
                        r = newWebRequest.GetResponse()
                    Catch ex As Net.WebException
                        Dim aex As Security.Authentication.AuthenticationException = TryCast(ex.InnerException, Security.Authentication.AuthenticationException)
                        If aex IsNot Nothing Then
                            TraceError(aex)
                            Throw aex
                        Else
                            Dim soapEx As Net.Sockets.SocketException = TryCast(ex.InnerException, Net.Sockets.SocketException)
                            If soapEx IsNot Nothing Then
                                TraceError(soapEx)
                                Throw soapEx
                            End If
                            TraceError(ex)
                            Throw
                        End If
                    End Try
                    hr = TryCast(r, Net.HttpWebResponse)

                    If hr IsNot Nothing AndAlso hr.StatusCode = Net.HttpStatusCode.OK Then
                        If r.ContentLength <> 0 Then
                            Dim s As IO.Stream = r.GetResponseStream()
                            Dim sr As New IO.StreamReader(s, True)
                            Dim html As String = sr.ReadToEnd
                            If html.Contains(My.Resources.ActiviserWebServiceMagic) Then
                                Dim confirmResult As CredUIConfirmReturnCodes = ConfirmUrlCredentials(testUri)
                                If confirmResult <> CredentialsXP.CredUIConfirmReturnCodes.NO_ERROR Then
                                    handleConfirmError(confirmResult)
                                End If
                                result = True
                            End If
                        End If
                        r.Close()
                    End If
                Else
                    handlePromptError(promptResult)
                End If
            End If
        Catch ex As Exception
            TraceError(ex)
            result = False
        Finally
            If owner IsNot Nothing Then owner.TopMost = False
            System.Windows.Forms.Cursor.Current = Cursors.Default
            TraceVerbose("Done")
        End Try
        Return result

    End Function

    Friend Function GetProxy() As Net.WebProxy
        TraceVerbose("Entered")
        Dim result As Net.WebProxy = Nothing
        Try
            If Not String.IsNullOrEmpty(My.Settings.proxyServerUrl) Then
                Dim proxyUrl As String = My.Settings.proxyServerUrl
                If proxyUrl = "default" Then
                    TraceVerbose("Default ProxyServer requested")
                    Return Nothing
                End If

                TraceVerbose("ProxyServer = {0}", proxyUrl)
                Dim proxyUri As New Uri(proxyUrl)

                result = New Net.WebProxy(proxyUri, False)
                If My.Settings.proxyServerAuthenticate Then
                    TraceVerbose("ProxyServerAuthentication Requested (My.Settings.proxyServerAuthenticate=true)")

                    Dim credentials As Net.ICredentials = Nothing
                    Dim promptResult As CredUIReturnCodes = GetUrlCredentials(proxyUri, credentials, Nothing, True)
                    If promptResult = CredUIReturnCodes.NO_ERROR Then
                        result.Credentials = credentials
                        ConfirmUrlCredentials(proxyUri)
                    End If
                End If
            Else
                result = New Net.WebProxy() ' Nothing 'force no proxy, otherwise gathered from Internet Explorer, which may or may not work!
            End If
        Catch ex As Exception
            TraceError(ex)
        End Try
        TraceVerbose("Done")
        Return result
    End Function

    Friend Sub handlePromptError(ByVal promptResult As CredentialsXP.CredUIReturnCodes)
        TraceVerbose(promptResult.ToString())
    End Sub

    Friend Sub handleConfirmError(ByVal confirmResult As CredentialsXP.CredUIConfirmReturnCodes)
        TraceVerbose(confirmResult.ToString())
    End Sub

End Module

