Imports System.Text
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Module NativeMethods
    Sub New()
    End Sub

#Region "Credential Management"

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure CREDUI_INFO
        Public cbSize As Integer
        Public hwndParent As IntPtr
        <MarshalAs(UnmanagedType.LPWStr)> Public pszMessageText As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pszCaptionText As String
        Public hbmBanner As IntPtr
    End Structure

    Public Declare Unicode Function CredUIPromptForCredentials Lib "credui" Alias "CredUIPromptForCredentialsW" _
        (ByRef creditUR As CREDUI_INFO, _
         ByVal targetName As String, ByVal reserved1 As IntPtr, _
         ByVal iError As Integer, _
         ByVal userName As StringBuilder, ByVal maxUserName As Integer, _
         ByVal password As StringBuilder, ByVal maxPassword As Integer, _
         ByRef iSave As Integer, _
         ByVal flags As CREDUI_FLAGS) _
    As CredUIReturnCodes

    Public Declare Auto Function DeleteObject Lib "Gdi32" (ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean

    Public Declare Unicode Function CredUIParseUserName Lib "credui" Alias "CredUIParseUserNameW" _
        (ByVal userName As String, _
         ByVal user As StringBuilder, ByVal userMaxChars As Integer, _
         ByVal domain As StringBuilder, ByVal domainMaxChars As Integer) _
    As CredUIParseReturnCodes

    Public Declare Unicode Function CredUIConfirmCredentials Lib "credui" Alias "CredUIConfirmCredentialsW" (ByVal targetName As String, <MarshalAs(UnmanagedType.Bool)> ByVal confirm As Boolean) As CredUIConfirmReturnCodes

    Public Enum SidNameUse
        SidTypeUser = 1
        SidTypeGroup
        SidTypeDomain
        SidTypeAlias
        SidTypeWellKnownGroup
        SidTypeDeletedAccount
        SidTypeInvalid
        SidTypeUnknown
        SidTypeComputer
    End Enum

    Public Declare Function LookupAccountName Lib "advapi32.dll" Alias "LookupAccountNameA" ( _
        ByVal lpSystemName As String, _
        ByVal lpAccountName As String, _
        <MarshalAs(UnmanagedType.LPArray)> ByVal Sid() As Byte, _
        ByRef cbSid As System.Int32, _
        <MarshalAs(UnmanagedType.LPWStr)> ByVal ReferencedDomainName As System.Text.StringBuilder, _
        ByRef cchReferencedDomainName As System.Int32, _
        ByRef peUse As SidNameUse) _
    As <MarshalAs(UnmanagedType.Bool)> Boolean
#End Region

End Module
