Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices

Module Utilities

    Const STR_ModifiedDateTime As String = "ModifiedDateTime"
    Const STR_ObjectId As String = "ObjectId"
    Const STR_Description As String = "Description"

    Public ReadOnly OsLevel As Integer = Environment.OSVersion.Version.Major * 100 + Environment.OSVersion.Version.Minor

    'Public WithCulture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentUICulture
    Public WithoutCulture As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

    Private guidRegex As New Regex("^[A-Fa-f0-9]{32}$|^({|\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\))?$")

    <Extension()> _
    Friend Function IsGuid(ByVal value As String) As Boolean
        If String.IsNullOrEmpty(value) Then Return False
        Return guidRegex.IsMatch(value)
    End Function

    Public Function DataRowHasChanges(ByVal dr As DataRow) As Boolean
        If dr.RowState = DataRowState.Added Then
            Return True
        End If
        'If dr.RowState = DataRowState.Unchanged Then
        '    Return False
        'End If
        For Each dc As DataColumn In dr.Table.Columns
            If dc.ColumnName = STR_ModifiedDateTime Then Continue For
            If Not String.IsNullOrEmpty(dc.Expression) Then Continue For
            If (dr.HasVersion(DataRowVersion.Proposed) AndAlso dr.HasVersion(DataRowVersion.Current)) Then
                If dc.DataType.IsValueType Then
                    If Not dr.Item(dc, DataRowVersion.Current).Equals(dr.Item(dc, DataRowVersion.Proposed)) Then
                        Return True
                    End If
                Else
                    If dr.Item(dc, DataRowVersion.Current) IsNot (dr.Item(dc, DataRowVersion.Proposed)) Then
                        Return True
                    End If
                End If
            ElseIf (dr.HasVersion(DataRowVersion.Current) AndAlso dr.HasVersion(DataRowVersion.Original)) Then
                If dc.DataType.IsValueType Then
                    If Not dr.Item(dc, DataRowVersion.Current).Equals(dr.Item(dc, DataRowVersion.Original)) Then
                        Return True
                    End If
                Else
                    If dr.Item(dc, DataRowVersion.Current) IsNot (dr.Item(dc, DataRowVersion.Original)) Then
                        Return True
                    End If
                End If
            End If
        Next
        Return False
    End Function



    Public Function SortDataRowArray(ByVal target() As DataRow, ByVal columnName As String) As DataRow()
        If target IsNot Nothing AndAlso target.Length > 0 Then
            Dim dr As DataRow = target(0)
            If dr.Table IsNot Nothing Then
                Dim i As Integer = dr.Table.Columns.IndexOf(columnName)
                target = SortDataRowArray(target, i)
            Else
                Dim keys(target.Length - 1) As Object
                For i As Integer = 0 To target.Length - 1
                    keys(i) = target(i).Item(columnName)
                Next
                Array.Sort(keys, target)
            End If
        End If

        Return target
    End Function

    Public Function SortDataRowArray(ByVal target() As DataRow, ByVal columnIndex As Integer) As DataRow()
        If target IsNot Nothing AndAlso target.Length > 0 Then
            Dim keys(target.Length - 1) As Object
            For i As Integer = 0 To target.Length - 1
                keys(i) = target(i).Item(columnIndex)
            Next
            Array.Sort(keys, target)
        End If
        Return target
    End Function

    Public Sub SaveColumnWidthChange(ByVal SettingCollection As Collections.Specialized.StringCollection, ByVal ColumnName As String, ByVal Width As Integer)
        Dim newValue As String = ColumnName & ":"c & Width
        For Each item As String In SettingCollection
            If item.IndexOf(":"c) > 0 Then
                Dim keyvalue() As String = item.Split(New Char() {":"c}, 2)
                Dim key As String = keyvalue(0)
                If key = ColumnName Then
                    SettingCollection.Remove(item)
                    Exit For
                End If
            End If
        Next
        SettingCollection.Add(newValue)
        My.Settings.Save()
    End Sub

    Public Sub RestoreColumnWidths(ByVal SettingCollection As Specialized.StringCollection, ByVal TargetDataGridView As System.Windows.Forms.DataGridView)
        If SettingCollection IsNot Nothing Then
            For Each item As String In SettingCollection
                If item.IndexOf(":"c) > 0 Then
                    Dim keyvalue() As String = item.Split(New Char() {":"c}, 2)
                    Dim key As String = keyvalue(0)
                    Dim colwidth As String = keyvalue(1)
                    If TargetDataGridView.Columns.Contains(key) Then
                        Dim c As DataGridViewColumn = TargetDataGridView.Columns(key)
                        If IsNumeric(colwidth) Then
                            c.Width = CInt(colwidth)
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub SaveColumnOrderChange(ByVal SettingCollection As Collections.Specialized.StringCollection, ByVal ColumnName As String, ByVal index As Integer)
        Dim newValue As String = ColumnName & ":"c & index
        For Each item As String In SettingCollection
            If item.IndexOf(":"c) > 0 Then
                Dim keyvalue() As String = item.Split(New Char() {":"c}, 2)
                Dim key As String = keyvalue(0)
                If key = ColumnName Then
                    SettingCollection.Remove(item)
                    Exit For
                End If
            End If
        Next
        SettingCollection.Add(newValue)
        My.Settings.Save()
    End Sub

    Public Sub RestoreColumnOrder(ByVal SettingCollection As Specialized.StringCollection, ByVal TargetDataGridView As System.Windows.Forms.DataGridView)
        If SettingCollection IsNot Nothing Then
            For Each item As String In SettingCollection
                If item.IndexOf(":"c) > 0 Then
                    Dim keyvalue() As String = item.Split(New Char() {":"c}, 2)
                    Dim key As String = keyvalue(0)
                    Dim index As String = keyvalue(1)
                    If TargetDataGridView.Columns.Contains(key) Then
                        Dim c As DataGridViewColumn = TargetDataGridView.Columns(key)
                        If IsNumeric(index) Then
                            c.DisplayIndex = CInt(index)
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Public Const GetXRatio As Integer = 1
    Public Const GetYRatio As Integer = 1

    'Public MyDpiX As Double = 96.0!
    'Public MyDpiY As Double = 96.0!

    'Public isLandScape As Boolean = False

    'Public Function ExpandName(ByVal value As String) As String
    '    Dim source() As Char = value.ToCharArray
    '    Dim result As New System.Text.StringBuilder(source.Length)

    '    'Dim lastC As Char = " "c
    '    Dim top As Integer = source.Length - 1
    '    For i As Integer = 0 To top
    '        Dim c As Char = source(i)
    '        Dim nextC As Char = Char.MinValue
    '        If i < top Then
    '            nextC = source(i + 1)
    '        End If
    '        If (Char.IsWhiteSpace(c) OrElse Char.IsUpper(c) OrElse Char.IsNumber(c)) Then
    '            If Not nextC = Char.MinValue Then
    '                If Not (Char.IsUpper(nextC) OrElse Char.IsNumber(nextC)) AndAlso Not Char.IsWhiteSpace(nextC) Then
    '                    result.Append(" "c)
    '                End If
    '            End If
    '            If Not Char.IsWhiteSpace(c) Then
    '                result.Append(c)
    '            End If
    '        Else
    '            result.Append(c)
    '        End If
    '        'lastC = c
    '    Next
    '    Return result.ToString
    'End Function

    'Public Function StripPrefixFromName(ByVal value As String) As String
    '    Dim source() As Char = value.ToCharArray

    '    For i As Integer = 0 To source.GetUpperBound(0)
    '        If Char.IsUpper(source(i)) Then
    '            Return New String(source, i, source.Length - i)
    '        End If
    '    Next

    '    ' if we get here, there would appear to be no prefix, just return the original string
    '    Return value
    'End Function

    'Public Function GetPrefixFromName(ByVal value As String) As String
    '    Dim source() As Char = value.ToCharArray

    '    For i As Integer = 0 To source.GetUpperBound(0)
    '        If Char.IsUpper(source(i)) Then
    '            Return New String(source, 0, i)
    '        End If
    '    Next

    '    ' if we get here, there would appear to be no prefix, just return an empty string
    '    Return String.Empty
    'End Function

    'Public Function IIf(ByVal b As Boolean, ByVal x As Integer, ByVal y As Integer) As Integer
    '    If b Then
    '        Return x
    '    Else
    '        Return y
    '    End If
    'End Function

    'Public Function IIf(Of T)(ByVal expression As Boolean, ByVal trueValue As T, ByVal falseValue As T) As T
    '    If expression Then
    '        Return trueValue
    '    Else
    '        Return falseValue
    '    End If
    'End Function


    Public Function XorGuid(ByVal x As Guid, ByVal y As Guid) As Guid
        Dim xB() As Byte = x.ToByteArray
        Dim yB() As Byte = y.ToByteArray
        Dim rB(15) As Byte
        For i As Integer = 0 To 15
            rB(i) = xB(i) Xor yB(i)
        Next
        Return New Guid(rB)
    End Function


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


    'Public Function EncryptString(ByVal value As String, ByVal key As Byte()) As String
    '    If value <> String.Empty Then
    '        Try
    '            Return System.Convert.ToBase64String(Crypto.Encrypt(key, System.Text.UnicodeEncoding.Unicode.GetBytes(value)))
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    Else
    '        Return String.Empty
    '    End If
    'End Function

    'Public Function EncryptString(ByVal value As String, ByVal key As Guid) As String
    '    Return EncryptString(value, key.ToByteArray())
    'End Function

    'Public Function DecryptString(ByVal value As String, ByVal key As Byte()) As String
    '    If value <> String.Empty Then
    '        Try
    '            Dim encryptedBytes() As Byte = System.Convert.FromBase64String(value)
    '            Dim decryptedBytes() As Byte = Crypto.Decrypt(key, encryptedBytes)
    '            Return System.Text.UnicodeEncoding.Unicode.GetString(decryptedBytes, 0, decryptedBytes.Length)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    Else
    '        Return String.Empty
    '    End If
    'End Function

    'Public Function DecryptString(ByVal value As String, ByVal key As Guid) As String
    '    Return DecryptString(value, key.ToByteArray())
    'End Function
End Module

