Imports activiser.Library
Imports activiser.Library.activiserWebService

Friend Module LogInOut
    Friend LoggedOn As Boolean

    Friend Function Logon() As Boolean

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
            MessageBox.Show(ex.Message)
        End Try
    End Function


    Friend Function TryLogon(ByVal serverUrl As String, ByVal useIntegratedAuthentication As Boolean, ByVal username As String, ByVal password As String, ByVal linkUser As Boolean, ByVal owner As Form) As Boolean
        If LoggedOn Then Logout()

        Try
            Dim credentials As Net.ICredentials = Nothing

            If Not TestUrl(serverUrl, credentials, owner) Then
                MessageBox.Show(My.Resources.WebServiceCommunicationFailure, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim user As Utility.UserRow
            If useIntegratedAuthentication Then
                user = TestUserLogin(serverUrl, Nothing, Nothing, owner)
            Else
                user = TestUserLogin(serverUrl, username, password, owner)
            End If

            If user IsNot Nothing Then
                WebService.Url = serverUrl
                My.Settings.activiserServerUrl = serverUrl

                WebService.Credentials = credentials
                WebService.PreAuthenticate = credentials IsNot Nothing
                WebService.Proxy = GetProxy()
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
                        Select Case MessageBox.Show(String.Format(My.Resources.UserLinkConfirmationTemplate, ConsoleUser.Name, My.User.Name), My.Resources.activiserFormTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
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
                MessageBox.Show(My.Resources.LoginFailure, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            LoggedOn = True
            Return True
        Catch wex As System.Net.WebException
            MessageBox.Show(My.Resources.WebServiceCommunicationFailure, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show(String.Format("{0} : {1}", My.Resources.UnhandledError, ex.Message), My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try

    End Function

    Friend Function Logout() As Boolean
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
        End Try
    End Function

    Friend Sub LinkNetworkUser(ByVal cudt As Utility.UserDataTable, ByVal pw As String)
        Try
            If WebService.ConsoleLinkNetworkUser(deviceId, cudt, pw) Then
                ConsoleUser.DomainLogon = My.User.Name
                My.Settings.AutoDomainLogon = True
                My.Settings.Save()
            Else
                MessageBox.Show(My.Resources.UserLinkFailure, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Friend Function TestUserLogin(ByVal serverUrl As String, ByVal activiserUser As String, ByVal activiserPassword As String, ByVal owner As Form) As Utility.UserRow
        Dim credentials As Net.ICredentials = Nothing

        If Not TestUrl(serverUrl, credentials, owner) Then
            Return Nothing
        End If

        Try
            Dim ws As New activiserWebService.activiser
            ws.Url = serverUrl
            ws.Proxy = GetProxy()
            ws.UnsafeAuthenticatedConnectionSharing = True

            ws.PreAuthenticate = credentials IsNot Nothing
            ws.Credentials = credentials

            Dim cudt As Utility.UserDataTable

            If String.IsNullOrEmpty(activiserUser) Then
                cudt = ws.ConsoleLogonNetworkUser(deviceId)
            Else
                Dim pw As String = GetHash(activiserPassword)
                cudt = ws.ConsoleLogonConsultant(deviceId, activiserUser, pw)
            End If

            If cudt IsNot Nothing AndAlso cudt.Rows.Count = 1 Then
                Return cudt(0)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function

    Friend Sub InteractiveUrlTest(ByVal serverUrl As String, ByVal owner As Form)
        Dim credentials As Net.ICredentials = Nothing
        If TestUrl(serverUrl, credentials, owner) Then
            MessageBox.Show(My.Resources.WebServiceCommunicationSuccessful, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(My.Resources.WebServiceCommunicationFailure, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Friend Function TestUrl(ByVal serverUrl As String, ByRef credentials As Net.ICredentials, ByVal owner As Form, Optional ByVal testActiviserWebService As Boolean = True) As Boolean
        Dim result As Boolean = False

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try
            Dim testUri As New Uri(serverUrl)
            Dim newWebRequest As System.Net.HttpWebRequest
            newWebRequest = CType(Net.WebRequest.Create(testUri), Net.HttpWebRequest)

            If newWebRequest IsNot Nothing Then
                newWebRequest.UnsafeAuthenticatedConnectionSharing = True

                Debug.Print(testUri.HostNameType.ToString())
                If testUri.IsLoopback Then ' local host
                    Debug.Print("Locally hosted")
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
                            Throw aex
                        Else
                            Dim soapEx As Net.Sockets.SocketException = TryCast(ex.InnerException, Net.Sockets.SocketException)
                            If soapEx IsNot Nothing Then
                                Throw soapEx
                            End If
                            Throw
                        End If
                    End Try
                    hr = TryCast(r, Net.HttpWebResponse)

                    If hr IsNot Nothing AndAlso hr.StatusCode = Net.HttpStatusCode.OK Then
                        If testActiviserWebService Then
                            If r.ContentLength <> 0 Then
                                Dim s As IO.Stream = r.GetResponseStream()
                                Dim sr As New IO.StreamReader(s, True)
                                Dim html As String = sr.ReadToEnd
                                'Debug.WriteLine(html)
                                If html.Contains(My.Resources.ActiviserWebServiceMagic) Then
                                    Dim confirmResult As CredUIConfirmReturnCodes = ConfirmUrlCredentials(testUri)
                                    If confirmResult <> CredentialsXP.CredUIConfirmReturnCodes.NO_ERROR Then
                                        handleConfirmError(confirmResult)
                                    End If
                                    result = True
                                End If
                            End If
                        Else
                            Dim confirmResult As CredUIConfirmReturnCodes = ConfirmUrlCredentials(testUri)
                            If confirmResult <> CredentialsXP.CredUIConfirmReturnCodes.NO_ERROR Then
                                handleConfirmError(confirmResult)
                            End If
                            result = True
                        End If
                        r.Close()
                    End If
                Else
                    handlePromptError(promptResult)
                End If
            End If
        Catch ex As Exception
            result = False
        Finally
            If owner IsNot Nothing Then owner.TopMost = False
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
        Return result

    End Function

    Friend Function GetProxy() As Net.WebProxy
        Dim result As Net.WebProxy = Nothing
        Try
            If Not String.IsNullOrEmpty(My.Settings.proxyServerUrl) Then
                Dim proxyUrl As String = My.Settings.proxyServerUrl
                If proxyUrl = "default" Then
                    'TraceVerbose("Default ProxyServer requested")
                    Return Nothing
                End If

                Dim proxyUri As New Uri(proxyUrl)

                result = New Net.WebProxy(proxyUri, False)
                If My.Settings.proxyServerAuthenticate Then

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
        End Try
        Return result
    End Function

    Friend Sub handlePromptError(ByVal promptResult As CredentialsXP.CredUIReturnCodes)
        '        TraceVerbose(promptResult.ToString())
    End Sub

    Friend Sub handleConfirmError(ByVal confirmResult As CredentialsXP.CredUIConfirmReturnCodes)
        '       TraceVerbose(confirmResult.ToString())
    End Sub

End Module

