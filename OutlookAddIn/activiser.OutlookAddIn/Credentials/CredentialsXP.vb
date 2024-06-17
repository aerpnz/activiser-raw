Imports System.Text
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Friend Module CredentialsXP

    <Flags()> _
    Public Enum CREDUI_FLAGS
        INCORRECT_PASSWORD = &H1
        DO_NOT_PERSIST = &H2
        REQUEST_ADMINISTRATOR = &H4
        EXCLUDE_CERTIFICATES = &H8
        REQUIRE_CERTIFICATE = &H10
        SHOW_SAVE_CHECK_BOX = &H40
        ALWAYS_SHOW_UI = &H80
        REQUIRE_SMARTCARD = &H100
        PASSWORD_ONLY_OK = &H200
        VALIDATE_USERNAME = &H400
        COMPLETE_USERNAME = &H800
        PERSIST = &H1000
        SERVER_CREDENTIAL = &H4000
        EXPECT_CONFIRMATION = &H20000
        GENERIC_CREDENTIALS = &H40000
        USERNAME_TARGET_CREDENTIALS = &H80000
        KEEP_USERNAME = &H100000
    End Enum

    Public Enum CredUIReturnCodes As Integer
        NO_ERROR = 0
        ERROR_CANCELLED = 1223
        ERROR_NO_SUCH_LOGON_SESSION = 1312
        ERROR_NOT_FOUND = 1168
        ERROR_INVALID_ACCOUNT_NAME = 1315
        ERROR_INSUFFICIENT_BUFFER = 122
        ERROR_INVALID_PARAMETER = 87
        ERROR_INVALID_FLAGS = 1004
    End Enum

    Public Enum CredUIConfirmReturnCodes As Integer
        NO_ERROR = 0
        ERROR_NOT_FOUND = 1168
        ERROR_INVALID_PARAMETER = 87
    End Enum

    Public Enum CredUIParseReturnCodes As Integer
        NO_ERROR = 0
        ERROR_INVALID_ACCOUNT_NAME = 1315
        ERROR_INSUFFICIENT_BUFFER = 122
        ERROR_INVALID_PARAMETER = 87
    End Enum

    Private Const MAX_USER_NAME As Integer = 513
    Private Const MAX_PASSWORD As Integer = 513
    Private Const MAX_DOMAIN As Integer = 337

    Public Function PromptForCredentials( _
             ByRef creditUI As CREDUI_INFO, _
             ByVal targetName As String, _
             ByVal netError As Integer, _
             ByRef userName As String, _
             ByRef password As String, _
             ByRef save As Boolean, _
             ByVal flags As CREDUI_FLAGS) _
        As CredUIReturnCodes

        Dim saveCredentials As Integer
        Dim user As New StringBuilder(MAX_USER_NAME)
        Dim pwd As New StringBuilder(MAX_PASSWORD)
        saveCredentials = Convert.ToInt32(save)
        creditUI.cbSize = Marshal.SizeOf(creditUI)
        Dim result As CredUIReturnCodes
        result = CredUIPromptForCredentials( _
                        creditUI, targetName, _
                        IntPtr.Zero, netError, _
                        user, MAX_USER_NAME, _
                        pwd, MAX_PASSWORD, _
                        saveCredentials, flags)
        save = Convert.ToBoolean(saveCredentials)
        userName = user.ToString
        password = pwd.ToString
        Return result
    End Function

    Public Function PromptForCredentials( _
             ByVal targetName As String, _
             ByRef userName As String, _
             ByRef password As String, _
             ByRef domain As String, _
             ByVal caption As String, _
             ByVal message As String, _
             ByVal logo As Bitmap, _
             ByVal owner As Form) _
        As CredUIReturnCodes
        Dim creduiFlags As CREDUI_FLAGS = CREDUI_FLAGS.EXPECT_CONFIRMATION Or CREDUI_FLAGS.GENERIC_CREDENTIALS
        Return PromptForCredentials(targetName, userName, password, domain, caption, message, logo, owner, creduiFlags)
    End Function

    Public Function PromptForCredentials( _
             ByVal targetName As String, _
             ByRef userName As String, _
             ByRef password As String, _
             ByRef domain As String, _
             ByVal caption As String, _
             ByVal message As String, _
             ByVal logo As Bitmap, _
             ByVal owner As Form, _
             ByVal flags As CREDUI_FLAGS) _
        As CredUIReturnCodes

        Dim saveCredentials As Integer
        Dim user As New StringBuilder(MAX_USER_NAME)
        Dim pwd As New StringBuilder(MAX_PASSWORD)
        saveCredentials = Convert.ToInt32(True)

        Dim creditUI As CREDUI_INFO
        If owner IsNot Nothing Then
            creditUI.hwndParent = owner.Handle
        End If

        creditUI.pszCaptionText = caption
        creditUI.pszMessageText = message
        If logo IsNot Nothing Then
            creditUI.hbmBanner = logo.GetHbitmap
        End If

        creditUI.cbSize = Marshal.SizeOf(creditUI)
        Dim result As CredUIReturnCodes
        If My.Computer.Keyboard.ShiftKeyDown Then
            flags = flags Or CREDUI_FLAGS.ALWAYS_SHOW_UI
        End If
        result = CredUIPromptForCredentials( _
                        creditUI, targetName, _
                        IntPtr.Zero, 0, _
                        user, MAX_USER_NAME, _
                        pwd, MAX_PASSWORD, _
                        saveCredentials, flags)

        If logo IsNot Nothing Then
            DeleteObject(logo.GetHbitmap)
        End If

        password = pwd.ToString
        Dim parseResult As CredUIParseReturnCodes = ParseUserName(user.ToString, userName, domain)
        If parseResult <> CredUIParseReturnCodes.NO_ERROR Then
            result = CType(parseResult, CredUIReturnCodes)
        End If
        Return result
    End Function


    Public Function ParseUserName(ByVal userName As String, ByRef userPart As String, ByRef domainPart As String) As CredUIParseReturnCodes
        ' although CredUIParseUserName is supposed to work with no domain specified, it seems to not be so.
        ' therefore, we will check for the presence of one of the domain separators and bypass the check if neither is found.
        If userName.IndexOfAny("\@".ToCharArray) <> -1 Then
        Dim user As New StringBuilder(MAX_USER_NAME)
        Dim domain As New StringBuilder(MAX_DOMAIN)
        Dim result As CredUIParseReturnCodes
        result = CredUIParseUserName(userName, user, MAX_USER_NAME, domain, MAX_DOMAIN)
        userPart = user.ToString()
        domainPart = domain.ToString()
        Return result
        Else
            userPart = userName
            domainPart = String.Empty
            Return CredUIParseReturnCodes.NO_ERROR
        End If
    End Function


    Public Function ConfirmCredentials(ByVal target As String, ByVal confirm As Boolean) As CredUIConfirmReturnCodes
        Return CredUIConfirmCredentials(target, confirm)
    End Function

End Module
