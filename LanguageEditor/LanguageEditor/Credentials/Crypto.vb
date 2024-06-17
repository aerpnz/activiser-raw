Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Module Crypto

    Private md5 As MD5 = md5.Create

    Public Function GetHash(ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty
        Try
            Return New Guid(md5.ComputeHash(Encoding.Default.GetBytes(value))).ToString("N")
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return String.Empty
        End Try
    End Function

    Public Function Encrypt(ByVal key As String, ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty
        If String.IsNullOrEmpty(key) Then Return String.Empty
        Try
            Return Encrypt(New Guid(md5.ComputeHash(Encoding.Default.GetBytes(key))), value)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return String.Empty
        End Try
    End Function

    Public Function Decrypt(ByVal key As String, ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty
        If String.IsNullOrEmpty(key) Then Return String.Empty
        Try
            Return Decrypt(New Guid(md5.ComputeHash(Encoding.Default.GetBytes(key))), value)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return String.Empty
        End Try
    End Function

    Public Function Encrypt(ByVal key As Guid, ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty

        Dim encryptedBytes As Byte() = Encrypt(key, Encoding.Unicode.GetBytes(value))
        If encryptedBytes Is Nothing OrElse encryptedBytes.Length = 0 Then Return String.Empty

        Return Convert.ToBase64String(encryptedBytes)
    End Function

    Public Function Encrypt(ByVal key As Guid, ByVal valueBytes As Byte()) As Byte()
        If valueBytes Is Nothing Then Return Nothing
        Try
            Dim symmAlg As New RijndaelManaged()
            symmAlg.Key = md5.ComputeHash(key.ToByteArray)
            symmAlg.GenerateIV()

            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, symmAlg.CreateEncryptor(), CryptoStreamMode.Write)

            cs.Write(valueBytes, 0, valueBytes.Length)
            cs.FlushFinalBlock()

            Dim encryptedBytes As Byte() = ms.ToArray()

            Dim result(encryptedBytes.Length + 15) As Byte

            Array.Copy(symmAlg.IV, 0, result, 0, 16)
            Array.Copy(encryptedBytes, 0, result, 16, encryptedBytes.Length)

            Return result
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return Nothing
        End Try

    End Function

    Public Function Decrypt(ByVal key As Guid, ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty
        Try
            Dim symmAlg As New RijndaelManaged()

            Dim cypherBytes As Byte() = Convert.FromBase64String(value)
            Dim iv(15) As Byte

            Array.Copy(cypherBytes, 0, iv, 0, 16)

            Dim encryptedLength As Integer = cypherBytes.Length - 16
            Dim encryptedBytes(encryptedLength - 1) As Byte

            Array.Copy(cypherBytes, 16, encryptedBytes, 0, encryptedLength)

            symmAlg.IV = iv
            symmAlg.Key = md5.ComputeHash(key.ToByteArray)

            Dim ms As New IO.MemoryStream(encryptedBytes)
            Dim cs As New CryptoStream(ms, symmAlg.CreateDecryptor(), CryptoStreamMode.Read)

            Dim resultBytes() As Byte = ReadAll(cs, encryptedBytes.Length)

            Return Encoding.Unicode.GetString(resultBytes, 0, resultBytes.Length)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return String.Empty
        End Try
    End Function


    ' not liking the redim stuff, but it's so much easier to read and follow than a list-based implementation.
    '
    Private Function ReadAll(ByVal source As IO.Stream, ByVal expectedLength As Integer) As Byte()
        Try
            Dim resultBytes() As Byte = {}

            Dim bufferLength As Integer = expectedLength + 1
            If expectedLength < 1 Then bufferLength = 65536

            Dim buffer(bufferLength - 1) As Byte

            Dim totalRead As Integer = 0
            Dim read As Integer

            read = source.Read(buffer, 0, bufferLength)

            Do While read <> 0
                ReDim Preserve resultBytes(totalRead + read)
                Array.Copy(buffer, 0, resultBytes, totalRead, read)
                ' since buffer size is one bigger than we need it, this is a quick and dirty way
                ' of checking that we've read the entire expected amount of data.
                If read <= expectedLength Then Exit Do
                totalRead += read
                read = source.Read(buffer, 0, bufferLength)
            Loop

            Return resultBytes
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return Nothing
        End Try

    End Function
End Module
