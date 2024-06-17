Imports System.Text
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Module NativeMethods
    'Sub New()
    'End Sub

    '#Region "General"
    '    Public Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32 ) As Int32
    '#End Region

    Friend Sub ReleaseComObject(Of T)(ByRef victim As T)
        If victim Is Nothing Then Return

        Try
            Marshal.FinalReleaseComObject(victim)
            victim = Nothing
        Catch ex As Exception
            ' don't really care
            TraceError(ex)
        End Try
    End Sub

    ' API functions

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
        <MarshalAs(UnmanagedType.LPWStr)> ByVal lpSystemName As String, _
        <MarshalAs(UnmanagedType.LPWStr)> ByVal lpAccountName As String, _
        <MarshalAs(UnmanagedType.LPArray)> ByVal Sid() As Byte, _
        ByRef cbSid As System.Int32, _
        <MarshalAs(UnmanagedType.LPWStr)> ByVal ReferencedDomainName As System.Text.StringBuilder, _
        ByRef cchReferencedDomainName As System.Int32, _
        ByRef peUse As SidNameUse) _
    As <MarshalAs(UnmanagedType.Bool)> Boolean
#End Region

    Public Class WinApi
        'Shared Sub New()
        'End Sub

#Region " Crypto API imports "

        Private Const ALG_CLASS_HASH As Integer = (4 << 13)
        Private Const ALG_TYPE_ANY As Integer = 0
        Private Const ALG_CLASS_DATA_ENCRYPT As Integer = (3 << 13)
        Private Const ALG_TYPE_STREAM As Integer = (4 << 9)
        Private Const ALG_TYPE_BLOCK As Integer = (3 << 9)

        Private Const ALG_SID_DES As Integer = 1
        Private Const ALG_SID_RC4 As Integer = 1
        Private Const ALG_SID_RC2 As Integer = 2
        Private Const ALG_SID_MD5 As Integer = 3

        Public Const MS_DEF_PROV As String = "Microsoft Base Cryptographic Provider v1.0"

        Public Const PROV_RSA_FULL As Integer = 1
        Public Const CRYPT_VERIFYCONTEXT As Integer = &HF0000000
        Public Const CRYPT_EXPORTABLE As Integer = &H1

        Public Const CALG_MD5 As Integer = ALG_CLASS_HASH Or ALG_TYPE_ANY Or ALG_SID_MD5
        'Public Shared ReadOnly CALG_DES As Integer = ALG_CLASS_DATA_ENCRYPT Or ALG_TYPE_BLOCK Or ALG_SID_DES
        Public Const CALG_RC2 As Integer = ALG_CLASS_DATA_ENCRYPT Or ALG_TYPE_BLOCK Or ALG_SID_RC2
        'Public Shared ReadOnly CALG_RC4 As Integer = ALG_CLASS_DATA_ENCRYPT Or ALG_TYPE_STREAM Or ALG_SID_RC4

#If COMPACT_FRAMEWORK Then
        Private Const CryptDll As String = "coredll.dll"
        Private Const KernelDll As String = "coredll.dll" '
#Else
        Private Const CryptDll As String = "advapi32.dll"
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")> Private Const KernelDll As String = "kernel32.dll" '
#End If

        ' all static 
        'Private Sub New()
        'End Sub

        <DllImport(CryptDll)> _
        Public Shared Function CryptAcquireContext( _
         ByRef phProv As IntPtr, _
         <MarshalAs(UnmanagedType.LPWStr)> ByVal pszContainer As String, _
         <MarshalAs(UnmanagedType.LPWStr)> ByVal pszProvider As String, _
         ByVal dwProvType As Integer, _
         ByVal dwFlags As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptReleaseContext( _
         ByVal hProv As IntPtr, ByVal dwFlags As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptDeriveKey( _
         ByVal hProv As IntPtr, ByVal Algid As Integer, _
         ByVal hBaseData As IntPtr, ByVal dwFlags As Integer, _
         ByRef phKey As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptCreateHash( _
         ByVal hProv As IntPtr, ByVal Algid As Integer, _
         ByVal hKey As IntPtr, ByVal dwFlags As Integer, _
         ByRef phHash As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptHashData( _
         ByVal hHash As IntPtr, ByVal pbData() As Byte, ByVal dwDataLen As Integer, _
         ByVal dwFlags As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptEncrypt( _
         ByVal hKey As IntPtr, ByVal hHash As IntPtr, _
         <MarshalAs(UnmanagedType.Bool)> ByVal Final As Boolean, ByVal dwFlags As Integer, _
         ByVal pbData() As Byte, ByRef pdwDataLen As Integer, _
         ByVal dwBufLen As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptDecrypt( _
         ByVal hKey As IntPtr, ByVal hHash As IntPtr, _
         <MarshalAs(UnmanagedType.Bool)> ByVal Final As Boolean, ByVal dwFlags As Integer, _
         ByVal pbData() As Byte, ByRef pdwDataLen As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptDestroyHash(ByVal hHash As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport(CryptDll)> _
        Public Shared Function CryptDestroyKey(ByVal hKey As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region
    End Class
End Module
