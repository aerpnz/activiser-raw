Imports System.Text
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Module CryptoNativeMethods
    Sub New()
    End Sub

    Public Class WinApi
        Shared Sub New()
        End Sub

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
        Private Sub New()
        End Sub

        <DllImport(CryptDll)> _
        Public Shared Function CryptAcquireContext( _
         ByRef phProv As IntPtr, ByVal pszContainer As String, _
         ByVal pszProvider As String, ByVal dwProvType As Integer, _
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
