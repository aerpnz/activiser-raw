Imports System.Text
Imports System.Runtime.InteropServices

Module Win2KCredentials
    Const CRED_Separator As Char = "!"c

    Private credList As New Generic.Dictionary(Of String, String)

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

        If String.IsNullOrEmpty(targetName) Then Return CredUIReturnCodes.ERROR_INVALID_PARAMETER
        Try
            Dim rk As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.CreateSubKey(My.Resources.CredentialsRegistryBase)
            Dim encryptionKey As String = GetKey()
            Dim targetNameHash As String = Encrypt(encryptionKey, targetName)
            Dim targetValues As String = Decrypt(CStr(rk.GetValue(targetNameHash, String.Empty)), encryptionKey)
            Dim returnUserName As String = Environment.UserName
            Dim returnDomain As String = Environment.UserDomainName
            Dim returnPassword As String = String.Empty

            Dim gotvalues As Boolean
            If targetValues.Length > 0 Then
                Dim targetElements() As String = targetValues.Split(CRED_Separator)
                If targetElements.Length = 3 Then
                    returnUserName = Decrypt(targetElements(0), encryptionKey)
                    returnPassword = Decrypt(targetElements(1), encryptionKey)
                    returnDomain = Decrypt(targetElements(2), encryptionKey)
                    gotvalues = True
                End If
            End If

            If gotvalues And Not My.Computer.Keyboard.ShiftKeyDown Then
                userName = returnUserName
                password = returnPassword
                domain = returnDomain
                Return CredUIReturnCodes.NO_ERROR
            End If

            Using cd As New CredentialDialog(returnUserName, returnPassword, returnDomain)
                cd.Message.Text = message
                cd.Text = caption
                cd.Owner = owner
                If Not String.IsNullOrEmpty(returnPassword) Then cd.SavePassword = True
                Select Case cd.ShowDialog()
                    Case DialogResult.OK
                        userName = cd.Username
                        password = cd.Password
                        domain = cd.DomainName
                        Dim saveKey As String
                        Dim encryptedUserName As String = Encrypt(encryptionKey, cd.Username)
                        Dim encryptedDomain As String = Encrypt(encryptionKey, cd.DomainName)
                        Dim encryptedPassword As String = String.Empty
                        If cd.SavePassword Then
                            encryptedPassword = Encrypt(encryptionKey, password)
                        End If
                        saveKey = encryptedUserName & CRED_Separator & encryptedPassword & CRED_Separator & encryptedDomain
                        If credList.ContainsKey(targetNameHash) Then credList.Remove(targetNameHash)
                        credList.Add(targetNameHash, Encrypt(encryptionKey, saveKey))

                        Return CredUIReturnCodes.NO_ERROR
                    Case DialogResult.Cancel
                        Return CredUIReturnCodes.ERROR_CANCELLED
                End Select
            End Using

        Catch ex As Exception
            Return CredUIReturnCodes.ERROR_CANCELLED
        End Try
    End Function

    Public Function ConfirmCredentials(ByVal target As String, ByVal confirm As Boolean) As CredUIConfirmReturnCodes
        If String.IsNullOrEmpty(target) Then Return CredUIConfirmReturnCodes.ERROR_INVALID_PARAMETER
        Dim targetNameHash As String = Encrypt(GetKey(), target)

        If credList.ContainsKey(targetNameHash) Then
            If confirm Then
                Dim rk As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.CreateSubKey(My.Resources.CredentialsRegistryBase)
                rk.SetValue(targetNameHash, credList.Item(targetNameHash))
            Else
                credList.Remove(targetNameHash)
            End If
            Return CredUIConfirmReturnCodes.NO_ERROR
        Else
            Return CredUIConfirmReturnCodes.ERROR_NOT_FOUND
        End If
    End Function

#Region "LookupAccountName"

#End Region

    Private Function GetKey() As String
        Dim userSid(), computerSid() As Byte
        Dim cbSid As Int32
        Dim refDomainName As New StringBuilder
        Dim cbRefDomainName As Int32
        Dim peUse As SidNameUse
        Dim result As String = String.Empty

        Try

            'First get the buffer sizes
            If Not LookupAccountName(Nothing, My.User.Name, Nothing, cbSid, Nothing, cbRefDomainName, peUse) Then
                Dim win32Error As Integer = Marshal.GetLastWin32Error()
                If win32Error <> 122 Then 'Data area too small; expected result
                    Throw New System.ComponentModel.Win32Exception(win32Error)
                End If
            End If

            'Adjust the buffers to the needed size
            ReDim userSid(cbSid)
            refDomainName.EnsureCapacity(cbRefDomainName)

            'Get the data again, now with the changed buffers
            If Not LookupAccountName(Nothing, My.User.Name, userSid, cbSid, refDomainName, cbRefDomainName, peUse) Then
                Dim win32Error As Integer = Marshal.GetLastWin32Error()
                Throw New System.ComponentModel.Win32Exception(win32Error)
            End If

            If Not LookupAccountName(Nothing, My.Computer.Name, Nothing, cbSid, Nothing, cbRefDomainName, peUse) Then
                Dim win32Error As Integer = Marshal.GetLastWin32Error()
                If win32Error <> 122 Then 'Data area too small; expected result
                    Throw New System.ComponentModel.Win32Exception(win32Error)
                End If
            End If

            'Adjust the buffers to the needed size
            ReDim computerSid(cbSid)
            refDomainName.EnsureCapacity(cbRefDomainName)

            'Get the data again, now with the changed buffers
            If Not LookupAccountName(Nothing, My.Computer.Name, computerSid, cbSid, refDomainName, cbRefDomainName, peUse) Then
                Dim win32Error As Integer = Marshal.GetLastWin32Error()
                Throw New System.ComponentModel.Win32Exception(win32Error)
            End If
            Dim key(userSid.Length + computerSid.Length) As Byte
            Array.Copy(computerSid, key, computerSid.Length)
            Array.ConstrainedCopy(userSid, 0, key, computerSid.Length, userSid.Length)
            Dim i, j, k As Integer
            For i = 0 To key.Length - 1
                If key(i) <> 0 Then
                    j += 1
                End If
            Next
            Dim compressedKey(j) As Byte
            For i = 0 To key.Length - 1
                If key(i) = 0 Then
                    Continue For
                Else
                    compressedKey(k) = key(i)
                    k += 1
                End If
            Next


            result = Encrypt(My.User.Name, Convert.ToBase64String(compressedKey))
        Catch ex As System.ComponentModel.Win32Exception
            Debug.WriteLine(ex.ToString)
        Catch ex As Exception

        Finally

        End Try
        Return result

    End Function


End Module
