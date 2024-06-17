Friend Module Credentials
    Const MODULENAME As String = "Credentials"

    Const STR_WebServiceCredentialPrompt As String = "WebServiceCredentialPrompt"
    Const STR_ProxyCredentialPrompt As String = "ProxyCredentialPrompt"

    Friend Function GetUrlCredentials(ByVal testUri As Uri, ByRef credentials As Net.ICredentials, ByVal owner As Form, ByVal forProxy As Boolean) As CredUIReturnCodes
        Dim promptResult As CredUIReturnCodes

        Dim message As String
        If Not forProxy Then
            message = Terminology.GetFormattedString(MODULENAME, STR_WebServiceCredentialPrompt, testUri.ToString(), My.Resources.CredentialXPHelpInfo)
            'If OsLevel > 500 AndAlso Not My.Settings.ForceWin2KLogon Then
            'Else
            '    message = Terminology.GetFormattedString(MODULENAME, STR_WebServiceCredentialPrompt, testUri.ToString(), My.Resources.Credential2KHelpInfo)
            'End If
        Else
            message = Terminology.GetFormattedString(MODULENAME, STR_ProxyCredentialPrompt, testUri.ToString(), My.Resources.CredentialXPHelpInfo)
            'If OsLevel > 500 AndAlso Not My.Settings.ForceWin2KLogon Then
            'Else
            '    message = Terminology.GetFormattedString(MODULENAME, STR_ProxyCredentialPrompt, testUri.ToString(), My.Resources.Credential2KHelpInfo)
            'End If
        End If

        Dim username As String = String.Empty
        Dim password As String = String.Empty
        Dim domain As String = String.Empty

        promptResult = CredentialsXP.PromptForCredentials(testUri.AbsoluteUri, username, password, domain, My.Resources.activiserFormTitle, message, Nothing, owner)
        credentials = GetCredentials(username, password, domain)

        'If My.Computer.Keyboard.ShiftKeyDown Then
        '    promptResult = CredentialsXP.PromptForCredentials(testUri.AbsoluteUri, username, password, domain, My.Resources.activiserFormTitle, message, Nothing, owner)
        '    credentials = GetCredentials(username, password, domain)
        'Else
        '    promptResult = CredUIReturnCodes.NO_ERROR
        '    credentials = GetCredentials(My.User.Name, password, domain) ' username, password, domain)
        'End If

        Return promptResult
    End Function

    Friend Function ConfirmUrlCredentials(ByVal testUri As Uri) As CredUIConfirmReturnCodes
        Return CredentialsXP.ConfirmCredentials(testUri.AbsoluteUri, True)
    End Function

    Friend Function RejectUrlCredentials(ByVal testUri As Uri) As CredUIConfirmReturnCodes
        Return CredentialsXP.ConfirmCredentials(testUri.AbsoluteUri, False)
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
