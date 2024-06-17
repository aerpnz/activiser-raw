Imports System.ComponentModel
Imports System.Text
Imports System.Security.Cryptography
Imports System.Net
Imports System.Runtime.CompilerServices

Module Utilities
    Const MODULENAME As String = "Utilities"

    Private _assemblyName As System.Reflection.AssemblyName
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

    Public Function GetHash(ByVal value As Byte()) As String
        If value Is Nothing OrElse value.Length = 0 Then Return String.Empty
        Try
            Return New Guid(md5.ComputeHash(value)).ToString("N")
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

            Dim ms As New MemoryStream
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

            Dim ms As New MemoryStream(encryptedBytes)
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
    Private Function ReadAll(ByVal source As Stream, ByVal expectedLength As Integer) As Byte()
        Try
            Dim resultBytes() As Byte = {}

            Dim bufferLength As Integer = expectedLength + 1
            If expectedLength < 1 Then bufferLength = 65536

            Dim buffer(bufferLength - 1) As Byte

            Dim totalRead As Integer = 0
            Dim read As Integer

            read = source.Read(buffer, 0, bufferLength)

            Do While read <> 0
                ReDim Preserve resultBytes(totalRead + read - 1)
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

    'Checks that a connection exists
    Public Function Connected() As Boolean
        Try
            Dim thisHost As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip As Net.IPAddress In thisHost.AddressList
                If Not IPAddress.IsLoopback(ip) Then Return True
            Next
        Catch
        End Try

        Return False
    End Function

    Public Function CanResolveName(ByVal url As String) As Boolean
        Try
            Dim host As IPHostEntry = Dns.GetHostEntry(New Uri(url).Host)
            Return host.AddressList.Length <> 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function XorGuid(ByVal x As Guid, ByVal y As Guid) As Guid
        Dim xB() As Byte = x.ToByteArray
        Dim yB() As Byte = y.ToByteArray
        Dim rB(15) As Byte
        For i As Integer = 0 To 15
            rB(i) = xB(i) Xor yB(i)
        Next
        Return New Guid(rB)
    End Function

    Public Function XorBytes(ByVal x As Byte(), ByVal y As Byte()) As Byte()
        If x Is Nothing Then Throw New ArgumentNullException("x")
        If y Is Nothing Then Throw New ArgumentNullException("y")

        If x.Length <> y.Length Then
            Throw New ArgumentException("array lengths must be equal")
        End If
        Dim rB(x.GetUpperBound(0)) As Byte
        For i As Integer = 0 To x.GetUpperBound(0)
            rB(i) = x(i) Xor y(i)
        Next
        Return rB
    End Function
#If MINORPLANETCLIENT Then
    'for Minor planet client, registry is a little unstable.... so we'll use the app.config file instead..
    Public Function GetRegistryValue(ByVal key As String, ByVal defaultValue As String) As String
        Return AppConfig.GetSetting(key, defaultValue)
    End Function

#Else
    Public Function GetRegistryValue(ByVal key As String, ByVal defaultValue As String) As String
        Try
            Dim uh As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("activiser")
            If uh IsNot Nothing Then
                Dim result As String = CStr(uh.GetValue(key, defaultValue))
                Return result
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
        Return defaultValue
    End Function

#End If

    'Public Function GetRegistryValue(ByVal key As String, ByVal defaultValue As Boolean) As Boolean
    '    Try
    '        Dim uh As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("activiser")
    '        If uh IsNot Nothing Then
    '            Dim result As Boolean = CBool(uh.GetValue(key, defaultValue))
    '            Return result
    '        End If
    '    Catch ex As Exception
    '        Debug.WriteLine(ex.ToString)
    '    End Try
    '    Return defaultValue
    'End Function

    'Public Function GetRegistryValue(ByVal key As String, ByVal defaultValue As Integer) As Integer
    '    Try
    '        Dim uh As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("activiser")
    '        If uh IsNot Nothing Then
    '            Dim result As Integer = CInt(uh.GetValue(key, defaultValue))
    '            Return result
    '        End If
    '    Catch ex As Exception
    '        Debug.WriteLine(ex.ToString)
    '    End Try
    '    Return defaultValue
    'End Function

#If MINORPLANETCLIENT Then
    Public Sub SetRegistryValue(ByVal key As String, ByVal value As String)
        AppConfig.SaveSetting(key, value)
        AppConfig.Save()
    End Sub
#Else
    Public Sub SetRegistryValue(ByVal key As String, ByVal value As String)
        Try
            Dim uh As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("activiser")
            If uh IsNot Nothing Then
                uh.SetValue(key, value, Microsoft.Win32.RegistryValueKind.String)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub
#End If
    'Public Sub SetRegistryValue(ByVal key As String, ByVal value As Boolean)
    '    Try
    '        Dim uh As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("activiser")
    '        If uh IsNot Nothing Then
    '            uh.SetValue(key, value.ToString, Microsoft.Win32.RegistryValueKind.String)
    '        End If
    '    Catch ex As Exception
    '        Debug.WriteLine(ex.ToString)
    '    End Try
    'End Sub

    'Public Sub SetRegistryValue(ByVal key As String, ByVal value As Integer)
    '    Try
    '        Dim uh As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("activiser")
    '        If uh IsNot Nothing Then
    '            uh.SetValue(key, value, Microsoft.Win32.RegistryValueKind.DWord)
    '        End If
    '    Catch ex As Exception
    '        Debug.WriteLine(ex.ToString)
    '    End Try
    'End Sub

    Public Function GetShortTimeFormat(ByVal LongFormat As String) As String
        Dim i As Integer = LongFormat.LastIndexOf(":", StringComparison.Ordinal)
        If i = -1 Then i = LongFormat.IndexOf("s", StringComparison.Ordinal)
        If i = -1 Then Return LongFormat
        Dim result As String = LongFormat.Substring(0, i)
        If LongFormat.IndexOfAny("t".ToCharArray) <> -1 Then
            Dim remainder As String = LongFormat.Substring(i)
            Dim j As Integer = remainder.LastIndexOfAny("sS.fF".ToCharArray)
            If j <> -1 AndAlso j < remainder.Length Then
                Dim tail As String = remainder.Substring(j + 1)
                result &= tail
            End If
        End If
        Return result
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
 Public Function FormatDate(ByVal d As DateTime, ByVal IncludeTime As Boolean) As String
        Const METHODNAME As String = "FormatDate"
        Try
            'If String.IsNullOrEmpty(gDateFormat) Then
            '    gDateFormat = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern
            'End If
            If d.Kind <> DateTimeKind.Local Then
                d = d.ToLocalTime
            End If
            If Not IncludeTime Then
                Return d.ToString(gDateFormat, System.Globalization.DateTimeFormatInfo.CurrentInfo)
            Else
                Return d.ToString(gDateFormat & " " & gTimeFormat, System.Globalization.DateTimeFormatInfo.CurrentInfo)
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            Return String.Empty
        End Try
    End Function

    Public Function FormatDate(ByVal d As DateTime) As String
        Return FormatDate(d, False)
    End Function

    'Public Function SortDataRowArray(ByVal target() As DataRow, ByVal columnName As String) As DataRow()
    '    If target IsNot Nothing AndAlso target.Length > 0 Then
    '        Dim dr As DataRow = target(0)
    '        If dr.Table IsNot Nothing Then
    '            Dim i As Integer = dr.Table.Columns.IndexOf(columnName)
    '            target = SortDataRowArray(target, i)
    '        Else
    '            Dim keys(target.Length - 1) As Object
    '            For i As Integer = 0 To target.Length - 1
    '                keys(i) = target(i).Item(columnName)
    '            Next
    '            Array.Sort(keys, target)
    '        End If
    '    End If

    '    Return target
    'End Function

    'Public Function SortDataRowArray(ByVal target() As DataRow, ByVal columnIndex As Integer) As DataRow()
    '    If target IsNot Nothing AndAlso target.Length > 0 Then
    '        Dim keys(target.Length - 1) As Object
    '        For i As Integer = 0 To target.Length - 1
    '            keys(i) = target(i).Item(columnIndex)
    '        Next
    '        Array.Sort(keys, target)
    '    End If
    '    Return target
    'End Function

    'Public Function DataRowHasChanges(Of T As DataRow)(ByVal dr As T) As Boolean
    '    ' mystery as to how a row with different current and proposed values can be classified as 'unchanged', but nonetheless 
    '    ' it happens!
    '    Debug.WriteLine(dr.RowState.ToString())
    '    ' If dr.RowState = DataRowState.Unchanged Then Return False

    '    ' new or removed row, assume truth to rumour of changes
    '    If dr.RowState = DataRowState.Detached Then Return True
    '    If dr.RowState = DataRowState.Deleted Then Return True
    '    If dr.RowState = DataRowState.Added Then Return True

    '    If dr.HasVersion(DataRowVersion.Current) AndAlso dr.HasVersion(DataRowVersion.Proposed) Then
    '        For i As Integer = 0 To dr.ItemArray.Length - 1
    '            If dr(i).GetType.IsValueType Then
    '                If Not dr(i, DataRowVersion.Current).Equals(dr(i, DataRowVersion.Proposed)) Then
    '                    Return True
    '                End If
    '            Else
    '                If dr(i, DataRowVersion.Current) IsNot dr(i, DataRowVersion.Proposed) Then
    '                    Return True
    '                End If
    '            End If
    '        Next
    '    ElseIf dr.HasVersion(DataRowVersion.Proposed) AndAlso Not dr.HasVersion(DataRowVersion.Current) Then
    '        Return True ' new record, must be something different !
    '    End If
    '    Return False
    'End Function

    Public Sub SetFormState(ByVal target As Form, ByVal menuItem As MenuItem, ByVal maximized As Boolean)
        target.WindowState = FormWindowState.Normal
        If maximized Then
            target.WindowState = FormWindowState.Maximized
            target.BringToFront()
            'Else
            '    target.WindowState = FormWindowState.Normal
        End If
        If target.WindowState = FormWindowState.Maximized Then
            menuItem.Checked = True
        Else
            menuItem.Checked = False
        End If
    End Sub

    Public Function GetVersion() As System.Version
        If _assemblyName Is Nothing Then
            Dim a As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
            _assemblyName = a.GetName
        End If

        Return _assemblyName.Version
    End Function

    Public Sub SetControlFont(ByVal c As Control, ByVal font As Font)
        Try
            If c.GetType().GetProperty("Font") IsNot Nothing Then
                If TypeOf c Is TabPage Then Exit Try
                If TypeOf c Is Panel Then Exit Try
                If TypeOf c Is Splitter Then Exit Try

                c.Font = font
            End If
        Catch ex As NotSupportedException
            ' don't care
            Debug.WriteLine(c.GetType().Name & " doesn't support setting the font")
        End Try
        For Each child As Control In c.Controls
            SetControlFont(child, font)
        Next
    End Sub

#If FRAMEWORK_VERSION_GE35 Then
    <Extension()> _
    Friend Function IsUrl(ByVal value As String, ByVal uriKind As UriKind) As Boolean
#Else
    Friend Function IsUrl(ByVal value As String, ByVal uriKind As UriKind) As Boolean
#End If
        Try
            Dim uri As New Uri(value, uriKind)
            Return True
        Catch ex As UriFormatException
            Return False
        End Try
    End Function

#If FRAMEWORK_VERSION_GE35 Then
    <Extension()> _
    Friend Function IsNullOrEmpty(ByVal ds As DataSet) As Boolean
#Else
    Friend Function IsNullOrEmpty(ByVal ds As DataSet) As Boolean
#End If
        If ds Is Nothing Then Return True

        For Each dt As DataTable In ds.Tables
            If dt.Rows.Count <> 0 Then Return False
        Next
        Return True
    End Function

#If FRAMEWORK_VERSION_GE35 Then
    <Extension()> _
    Public Function InSet(ByVal value As Char, ByVal valueSet As String) As Boolean
#Else
    Public Function InSet(ByVal value As Char, ByVal valueSet As String) As Boolean
#End If
        Return valueSet.IndexOf(value) <> -1
    End Function

    '<Extension()> _
    'Public Function InSet(Of T)(ByVal value As T, ByVal valueSet() As T) As Boolean
    '    Return Array.IndexOf(valueSet, value) <> -1
    'End Function

#If FRAMEWORK_VERSION_GE35 Then
    <Extension()> _
    Public Function InSet(Of T)(ByVal value As T, ByVal ParamArray valueSet() As T) As Boolean
#Else
    Public Function InSet(Of T)(ByVal value As T, ByVal ParamArray valueSet() As T) As Boolean
#End If
        Return Array.IndexOf(valueSet, value) <> -1
    End Function

#If FRAMEWORK_VERSION_GE35 Then
    <Extension()> _
    Function HasChanges(Of T As DataRow)(ByVal dr As T) As Boolean
#Else
    Function HasChanges(Of T As DataRow)(ByVal dr As T) As Boolean
#End If
        'Debug.WriteLine(dr.RowState.ToString())

        ' new or removed row, assume truth to rumour of changes
        If dr.RowState = DataRowState.Detached Then Return True
        If dr.RowState = DataRowState.Deleted Then Return True
        If dr.RowState = DataRowState.Added Then Return True

        If dr.HasVersion(DataRowVersion.Current) AndAlso dr.HasVersion(DataRowVersion.Proposed) Then
            For i As Integer = 0 To dr.ItemArray.Length - 1
                If dr(i).GetType.IsValueType Then
                    If Not dr(i, DataRowVersion.Current).Equals(dr(i, DataRowVersion.Proposed)) Then
                        Return True
                    End If
                Else
                    If dr(i, DataRowVersion.Current) IsNot dr(i, DataRowVersion.Proposed) Then
                        Return True
                    End If
                End If
            Next
        ElseIf dr.HasVersion(DataRowVersion.Proposed) AndAlso Not dr.HasVersion(DataRowVersion.Current) Then
            Return True ' new record, must be something different !
        End If
        Return False
    End Function

#If FRAMEWORK_VERSION_GE35 Then

    Public Function CompressString(ByVal value As String) As Byte()
        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(value)
        Dim compressedBuffer As Byte()
        Using _
            ms As New System.IO.MemoryStream(value.Length), _
            cs As New System.IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress)

            cs.Write(buffer, 0, buffer.Length)
            cs.Close()
            compressedBuffer = ms.ToArray()
        End Using

        Dim result(CInt(compressedBuffer.Length) + 3) As Byte
        Array.Copy(BitConverter.GetBytes(buffer.Length), 0, result, 0, 4)
        Array.Copy(compressedBuffer, 0, result, 4, compressedBuffer.Length)

        Return result
    End Function

    Public Function DecompressString(ByVal value As Byte()) As String
        Using _
            ms As New System.IO.MemoryStream(value, 4, value.Length - 4), _
            cs As New System.IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Decompress)

            Dim resultBuffer(BitConverter.ToInt32(value, 0) - 1) As Byte
            If cs.Read(resultBuffer, 0, resultBuffer.Length) = resultBuffer.Length Then
                Return System.Text.Encoding.UTF8.GetString(resultBuffer, 0, resultBuffer.Length)
            Else
                Throw New ArgumentException("value is not a valid compressed string", "value")
            End If
        End Using
    End Function
#Else
	'TODO: Pre-CF35 version.
	
#End If
End Module
