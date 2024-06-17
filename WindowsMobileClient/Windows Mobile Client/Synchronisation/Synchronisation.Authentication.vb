Partial Friend Class Synchronisation
    Friend Enum AuthenticationStatus
        Ok
        UserAuthenticationFailed
        DeviceAuthenticationFailed
        NoNetwork
        ServerTooOld
        NameResolutionFailure
    End Enum

    Friend Shared Function Authenticate() As AuthenticationStatus
        Const METHODNAME As String = "Authenticate"

        Dim w As System.Net.HttpWebRequest
        Dim r As System.Net.WebResponse
        Dim hr As System.Net.HttpWebResponse
        Try

            ' RCP: 2008-12-09, removed this because it prevents the PDA from even trying to connect!
            'If Not Connected() Then
            'LogSyncMessage(RES_CommsFailureMessage, Terminology.GetString(MODULENAME, RES_NotConnectedToANetwork))
            'Return AuthenticationStatus.NoNetwork
            'End If

            If Not CanResolveName(gWebServer.Url) Then
                LogSyncMessage(RES_CommsFailureMessage, Terminology.GetString(MODULENAME, RES_UnableToResolveHostName))
                Return AuthenticationStatus.NameResolutionFailure
            End If

            If ConsultantConfig.LastSyncOK AndAlso ConsultantConfig.LastSync.ToLocalTime.Date = DateTime.Today Then
                Return AuthenticationStatus.Ok ' if we last successfully sync'd today, then assume authentication ok.
            End If

            LogSyncMessage(RES_Authenticating)

            Try ' try checking the server version first, if this fails, then be a little more creative in order to get a more meaningful message
                Dim v As Version = GetServerVersion()
                Dim minVersion As New Version(My.Resources.MinimumServerVersion)
                If v >= minVersion Then
                    LogSyncMessage(RES_ServerVersionOk)
                    Return AuthenticationStatus.Ok
                Else
                    LogSyncMessage(RES_ServerVersionTooOld, CStr(v.Major), CStr(v.Minor), CStr(v.Build), CStr(v.Revision))
                    Return AuthenticationStatus.ServerTooOld
                End If
            Catch ex As UnauthorizedAccessException ' -1 returned by GetServerVersion = deviceIdCheck failed
                LogSyncMessage(RES_DeviceAuthenticationFailed)
                Return AuthenticationStatus.DeviceAuthenticationFailed
            Catch ex As FormatException
                LogSyncMessage(RES_ServerVersionTooOld)
                Return AuthenticationStatus.ServerTooOld
            Catch ex As Net.Sockets.SocketException
                LogSyncMessage(RES_CommsFailureMessage, ex.Message)
                Return AuthenticationStatus.NoNetwork
                'Throw
            Catch ex As Net.WebException
                Dim message As String = ex.Message
                hr = TryCast(ex.Response, Net.HttpWebResponse)
                If hr IsNot Nothing Then
                    message = Terminology.GetFormattedString(MODULENAME, RES_HttpResponseTemplate, CInt(hr.StatusCode), hr.StatusDescription)
                End If
                LogSyncMessage(RES_CommsFailureMessage, message)
                Return AuthenticationStatus.NoNetwork
                'Throw
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorGettingServerVersion)
                ' if we get here, something's gone wrong, we'll take the long-winded approach, and hopefully get more detail on the error
            End Try

            Dim creds As Net.NetworkCredential
            If Not String.IsNullOrEmpty(gDomainUsername) AndAlso Not String.IsNullOrEmpty(gDomainPassword) AndAlso Not String.IsNullOrEmpty(gDomain) Then
                creds = New Net.NetworkCredential(gDomainUsername, Decrypt(gDeviceID, gDomainPassword), gDomain)
            ElseIf Not String.IsNullOrEmpty(gDomainUsername) AndAlso Not String.IsNullOrEmpty(gDomainPassword) Then
                creds = New Net.NetworkCredential(gDomainUsername, Decrypt(gDeviceID, gDomainPassword))
            ElseIf Not String.IsNullOrEmpty(gDomainUsername) Then
                creds = New Net.NetworkCredential(gDomainUsername, String.Empty)
            Else
                creds = Nothing
            End If

            Dim Uri As New Uri(Globals.gServerUrl)
            w = CType(Net.WebRequest.Create(Uri), Net.HttpWebRequest)
            If w IsNot Nothing Then
                w.PreAuthenticate = creds IsNot Nothing
                w.Credentials = creds

                Try
                    r = w.GetResponse()
                    hr = TryCast(r, Net.HttpWebResponse)
                Catch ex As Net.WebException
                    If ex.Status = Net.WebExceptionStatus.ProtocolError Then
                        LogSyncMessage(RES_AuthenticationFailed)
                        Return AuthenticationStatus.UserAuthenticationFailed
                    Else
                        LogSyncMessage(RES_CommsFailureMessage, ex.Message)
                        Return AuthenticationStatus.NoNetwork
                    End If
                End Try

                If hr Is Nothing OrElse hr.StatusCode <> Net.HttpStatusCode.OK Then
                    LogSyncMessage(RES_AuthenticationFailed)
                    Return AuthenticationStatus.UserAuthenticationFailed
                Else
                    Dim amAuthenticated As Boolean = CheckDeviceId()

                    ' Me.SetSyncStatus(If(amAuthenticated, RES_DeviceAuthenticated, RES_DeviceAuthenticationFailed), True)

                    If Not amAuthenticated Then
                        LogSyncMessage(RES_DeviceAuthenticationFailed)
                        Return AuthenticationStatus.DeviceAuthenticationFailed
                    Else
                        Try
                            Dim v As Version = GetServerVersion()
                            Dim minVersion As New Version(My.Resources.MinimumServerVersion)
                            If v >= minVersion Then
                                LogSyncMessage(RES_ServerVersionOk)
                                Return AuthenticationStatus.Ok
                            Else
                                LogSyncMessage(RES_ServerVersionTooOld, CStr(v.Major), CStr(v.Minor), CStr(v.Build), CStr(v.Revision))
                                Return AuthenticationStatus.ServerTooOld
                            End If
                        Catch ex As UnauthorizedAccessException
                            Return AuthenticationStatus.DeviceAuthenticationFailed
                        Catch ex As ArgumentNullException
                            Return AuthenticationStatus.ServerTooOld
                        Catch ex As Net.Sockets.SocketException
                            Return AuthenticationStatus.NoNetwork
                        Catch ex As Net.WebException
                            Return AuthenticationStatus.NoNetwork
                        Catch ex As Exception
                            LogSyncMessage(RES_ServerVersionTooOld)
                            Return AuthenticationStatus.ServerTooOld
                        End Try
                        'Return AuthenticationStatus.Ok
                    End If
                End If
            End If
        Catch ex As Net.WebException
            gSyncLog.AddEntry(ex.Message)
            Return AuthenticationStatus.NoNetwork
        Catch ex As Exception
            gSyncLog.AddEntry(Terminology.GetString(MODULENAME, RES_NavigationError))
            Return AuthenticationStatus.NoNetwork
        End Try
    End Function


    Private Shared Function GetServerVersion() As Version
        Const METHODNAME As String = "GetServerVersion"
        Try
            LogSyncMessage(RES_GetServerVersion)

            Dim versionString As String = gWebServer.GetServerVersion(gDeviceIDString)

            If String.IsNullOrEmpty(versionString) Then
                Throw New FormatException()
            ElseIf versionString = "-1" Then
                Throw New UnauthorizedAccessException()
            End If
            Return New Version(versionString)
        Catch ex As Net.Sockets.SocketException
            Throw
        Catch ex As System.Net.WebException
            Throw
        Catch ex As Exception
            gSyncLog.AddEntry(ex.Message)
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Throw
        Finally
            Debug.WriteLine("GetServerVersion Done.")
        End Try
    End Function

End Class
