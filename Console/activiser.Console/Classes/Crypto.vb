'	Crypto class
'	--
'	Uses crypto API functions to encrypt and decrypt data. A passphrase 
'	string is used to create a 128-bit hash that is used to create a 
'	40-bit crypto key. The same key is required to encrypt and decrypt 
'	the data.

Imports System.Runtime.InteropServices
Imports System.Text


' Encrypts and decrypts data using the crypto APIs.

Public NotInheritable Class Crypto
    ' all static methods
    Private Sub New()
    End Sub


    ' Encrypt data. Use passphrase to generate the encryption key. 
    ' Returns a byte array that contains the encrypted data.
    Public Shared Function Encrypt(ByVal passphrase As String, ByVal data() As Byte) As Byte()
        Return Encrypt(ASCIIEncoding.ASCII.GetBytes(passphrase), data)
    End Function

    Public Shared Function Encrypt(ByVal passphrase As Guid, ByVal data() As Byte) As Byte()
        Return Encrypt(passphrase.ToByteArray, data)
    End Function

    Public Shared Function Encrypt(ByVal passphrase As Byte(), ByVal data() As Byte) As Byte()
        ' holds encrypted data
        Dim buffer As Byte() = Nothing

        ' crypto handles
        Dim hProv As IntPtr = IntPtr.Zero
        Dim hKey As IntPtr = IntPtr.Zero

        Try
            ' get crypto provider, specify the provider (3rd argument)
            ' instead of using default to ensure the same provider is 
            ' used on client and server
            If Not WinApi.CryptAcquireContext(hProv, Nothing, WinApi.MS_DEF_PROV, WinApi.PROV_RSA_FULL, WinApi.CRYPT_VERIFYCONTEXT) Then
                Failed()
            End If

            ' generate encryption key from passphrase
            hKey = GetCryptoKey(hProv, passphrase)

            ' determine how large of a buffer is required
            ' to hold the encrypted data
            Dim dataLength As Integer = data.Length
            Dim bufLength As Integer = data.Length

            If Not WinApi.CryptEncrypt(hKey, IntPtr.Zero, True, 0, Nothing, dataLength, bufLength) Then
                Failed()
            End If

            ' allocate and fill buffer with encrypted data
            buffer = New Byte(dataLength - 1) {}
            System.Buffer.BlockCopy(data, 0, buffer, 0, data.Length)

            dataLength = data.Length
            bufLength = buffer.Length
            If Not WinApi.CryptEncrypt(hKey, IntPtr.Zero, True, 0, buffer, dataLength, bufLength) Then
                Failed()
            End If
        Finally
            ' release crypto handles
            If Not hKey.Equals(IntPtr.Zero) Then
                WinApi.CryptDestroyKey(hKey)
            End If

            If Not hProv.Equals(IntPtr.Zero) Then
                WinApi.CryptReleaseContext(hProv, 0)
            End If
        End Try

        Return buffer
    End Function

    ' Decrypt data. Use passphrase to generate the encryption key. 
    ' Returns a byte array that contains the decrypted data.
    Public Shared Function Decrypt(ByVal passphrase As String, ByVal data() As Byte) As Byte()
        Return (Decrypt(ASCIIEncoding.ASCII.GetBytes(passphrase), data))
    End Function

    Public Shared Function Decrypt(ByVal passphrase As Guid, ByVal data() As Byte) As Byte()
        Return Decrypt(passphrase.ToByteArray, data)
    End Function

    Public Shared Function Decrypt(ByVal passphrase As Byte(), ByVal data() As Byte) As Byte()
        If data Is Nothing Then
            Return Nothing
        End If

        ' make a copy of the encrypted data
        Dim dataCopy As Byte() = CType(data.Clone(), Byte())

        ' holds the decrypted data
        Dim buffer As Byte() = Nothing

        ' crypto handles
        Dim hProv As IntPtr = IntPtr.Zero
        Dim hKey As IntPtr = IntPtr.Zero

        Try
            ' get crypto provider, specify the provider (3rd argument)
            ' instead of using default to ensure the same provider is 
            ' used on client and server
            If Not WinApi.CryptAcquireContext(hProv, Nothing, WinApi.MS_DEF_PROV, WinApi.PROV_RSA_FULL, WinApi.CRYPT_VERIFYCONTEXT) Then
                Failed()
            End If

            ' generate encryption key from the passphrase
            hKey = GetCryptoKey(hProv, passphrase)

            ' decrypt the data
            Dim dataLength As Integer = dataCopy.Length
            If Not WinApi.CryptDecrypt(hKey, IntPtr.Zero, True, 0, dataCopy, dataLength) Then
                Failed()
            End If

            ' copy to a buffer that is returned to the caller
            ' the decrypted data size might be less then
            ' the encrypted size
            buffer = New Byte(dataLength - 1) {}
            System.Buffer.BlockCopy(dataCopy, 0, buffer, 0, dataLength)
        Finally
            ' release crypto handles
            If Not hKey.Equals(IntPtr.Zero) Then
                WinApi.CryptDestroyKey(hKey)
            End If

            If Not hProv.Equals(IntPtr.Zero) Then
                WinApi.CryptReleaseContext(hProv, 0)
            End If
        End Try

        Return buffer
    End Function

    ' Create a crypto key form a passphrase. This key is 
    ' used to encrypt and decrypt data.
    Private Shared Function GetCryptoKey(ByVal hProv As IntPtr, ByVal passphrase As String) As IntPtr
        Return GetCryptoKey(hProv, ASCIIEncoding.ASCII.GetBytes(passphrase))
    End Function

    ' Create a crypto key form a passphrase. This key is 
    ' used to encrypt and decrypt data.
    Private Shared Function GetCryptoKey(ByVal hProv As IntPtr, ByVal keyData As Byte()) As IntPtr
        ' crypto handles
        Dim hHash As IntPtr = IntPtr.Zero
        Dim hKey As IntPtr = IntPtr.Zero

        Try
            ' create 128 bit hash object
            If Not WinApi.CryptCreateHash(hProv, WinApi.CALG_MD5, IntPtr.Zero, 0, hHash) Then
                Failed()
            End If

            ' add passphrase to hash
            'Dim keyData As Byte() = passphrase '.ToByteArray
            If Not WinApi.CryptHashData(hHash, keyData, CType(keyData.Length, Integer), 0) Then
                Failed()
            End If

            ' create 40 bit crypto key from passphrase hash
            If Not WinApi.CryptDeriveKey(hProv, WinApi.CALG_RC2, hHash, WinApi.CRYPT_EXPORTABLE, hKey) Then
                Failed()
            End If

        Finally
            ' release hash object
            If Not hHash.Equals(IntPtr.Zero) Then
                WinApi.CryptDestroyHash(hHash)
            End If
        End Try

        Return hKey
    End Function

    ' Throws SystemException with GetLastError information.
    Private Shared Sub Failed()
        Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error)
        'Throw New SystemException( _
        ' String.Format("{0} failed.{3}Last error - 0x{1:x}.{3}Error message - {2}", command, wex.NativeErrorCode, wex.Message, vbNewLine))
    End Sub

    Public Shared Function EncryptString(ByVal value As String, ByVal key As Byte()) As String
        If value <> String.Empty Then
            Try
                Return System.Convert.ToBase64String(Crypto.Encrypt(key, System.Text.UnicodeEncoding.Unicode.GetBytes(value)))
            Catch ex As Exception
                Return String.Empty
            End Try
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function EncryptString(ByVal value As String, ByVal key As Guid) As String
        Return EncryptString(value, key.ToByteArray())
    End Function

    Public Shared Function DecryptString(ByVal value As String, ByVal key As Byte()) As String
        If value <> String.Empty Then
            Try
                Dim encryptedBytes() As Byte = System.Convert.FromBase64String(value)
                Dim decryptedBytes() As Byte = Crypto.Decrypt(key, encryptedBytes)
                Return System.Text.UnicodeEncoding.Unicode.GetString(decryptedBytes, 0, decryptedBytes.Length)
            Catch ex As Exception
                Return String.Empty
            End Try
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function DecryptString(ByVal value As String, ByVal key As Guid) As String
        Return DecryptString(value, key.ToByteArray())
    End Function
End Class
