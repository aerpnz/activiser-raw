Friend Module Credentials
    Const MODULENAME As String = "Credentials"

    Const STR_WebServiceCredentialPrompt As String = "WebServiceCredentialPrompt"
    Const STR_ProxyCredentialPrompt As String = "ProxyCredentialPrompt"

    Friend Function GetUrlCredentials(ByVal testUri As Uri, ByRef credentials As Net.ICredentials, ByVal owner As Form, ByVal forProxy As Boolean) As CredUIReturnCodes
        Dim promptResult As CredUIReturnCodes

        Dim message As String
        If Not forProxy Then
            If OsLevel > 500 Then
                message = Terminology.GetFormattedString(MODULENAME, STR_WebServiceCredentialPrompt, testUri.ToString(), My.Resources.CredentialXPHelpInfo)
            Else
                message = Terminology.GetFormattedString(MODULENAME, STR_WebServiceCredentialPrompt, testUri.ToString(), My.Resources.Credential2KHelpInfo)
            End If
        Else
            If OsLevel > 500 Then
                message = Terminology.GetFormattedString(MODULENAME, STR_ProxyCredentialPrompt, testUri.ToString(), My.Resources.CredentialXPHelpInfo)
            Else
                message = Terminology.GetFormattedString(MODULENAME, STR_ProxyCredentialPrompt, testUri.ToString(), My.Resources.Credential2KHelpInfo)
            End If
        End If

        Dim username As String = String.Empty
        Dim password As String = String.Empty
        Dim domain As String = String.Empty
        If OsLevel > 500 Then
            promptResult = CredentialsXP.PromptForCredentials(testUri.AbsoluteUri, username, password, domain, My.Resources.activiserFormTitle, message, Nothing, owner)
        Else
            promptResult = Win2KCredentials.PromptForCredentials(testUri.AbsoluteUri, username, password, domain, My.Resources.activiserFormTitle, message, Nothing, owner)
        End If
        credentials = GetCredentials(username, password, domain)
        Return promptResult
    End Function

    Friend Function ConfirmUrlCredentials(ByVal testUri As Uri) As CredUIConfirmReturnCodes
        Dim confirmResult As CredUIConfirmReturnCodes

        If OsLevel > 500 Then
            confirmResult = CredentialsXP.ConfirmCredentials(testUri.AbsoluteUri, True)
        Else
            confirmResult = Win2KCredentials.ConfirmCredentials(testUri.AbsoluteUri, True)
        End If
        Return confirmResult
    End Function

    Friend Function RejectUrlCredentials(ByVal testUri As Uri) As CredUIConfirmReturnCodes
        Dim confirmResult As CredUIConfirmReturnCodes

        If OsLevel > 500 AndAlso Not My.Settings.ForceWin2KLogon Then
            confirmResult = CredentialsXP.ConfirmCredentials(testUri.AbsoluteUri, False)
        Else
            confirmResult = Win2KCredentials.ConfirmCredentials(testUri.AbsoluteUri, False)
        End If
        Return confirmResult
    End Function

    Private Function GetCredentials(ByVal username As String, ByVal password As String, ByVal domain As String) As System.Net.ICredentials
        Dim creds As Net.NetworkCredential
        If Not String.IsNullOrEmpty(username) AndAlso Not String.IsNullOrEmpty(password) AndAlso Not String.IsNullOrEmpty(domain) Then
            creds = New Net.NetworkCredential(username, password, domain)
        ElseIf Not String.IsNullOrEmpty(username) Then
            creds = New Net.NetworkCredential(username, password)
        Else
            Return Nothing
        End If

        Return creds
    End Function
End Module
