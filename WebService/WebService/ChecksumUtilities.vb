Imports activiser.activiserDataSet

Module ChecksumUtilities
    'Friend ReadOnly WithCulture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentUICulture
    Friend ReadOnly WithoutCulture As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

    'Friend Function CheckSum(ByVal s As String, ByVal guid() As Byte) As Byte()
    '    If s.Length < 1 Then
    '        Return guid
    '    End If
    '    Dim c() As Char = s.ToCharArray
    '    Dim u As Integer = c.Length
    '    If u Mod 16 <> 0 Then
    '        u = (u \ 16) * 16
    '    End If
    '    u -= 1
    '    Dim b(u) As Byte
    '    Dim r() As Byte = guid
    '    For i As Integer = 0 To c.GetUpperBound(0)
    '        b(i) = CByte(Asc(c(i)) And &HFF)
    '    Next

    '    For i As Integer = 0 To c.GetUpperBound(0) Step 16
    '        For j As Integer = 0 To 15
    '            Dim g(15) As Byte
    '            Array.Copy(b, i, g, 0, 16)
    '            r(j) = r(j) Xor g(j)
    '        Next
    '    Next
    '    Return r
    'End Function

    'Friend Function CheckSum(ByVal c() As Byte, ByVal guid() As Byte) As Byte()
    '    Dim u As Integer = c.Length
    '    If u Mod 16 <> 0 Then
    '        u = (u \ 16) * 16
    '    End If
    '    u -= 1
    '    Dim b(u) As Byte
    '    Dim r() As Byte = guid
    '    For i As Integer = 0 To c.GetUpperBound(0)
    '        b(i) = c(i)
    '    Next

    '    For i As Integer = 0 To c.GetUpperBound(0) Step 16
    '        For j As Integer = 0 To 15
    '            Dim g(15) As Byte
    '            Array.Copy(b, i, g, 0, 16)
    '            r(j) = r(j) Xor g(j)
    '        Next
    '    Next
    '    Return r
    'End Function

    Function JobChecksum(ByVal drJob As JobRow) As String
        If drJob Is Nothing Then Return Nothing
        Dim checkStringBuilder As New System.Text.StringBuilder(200)

        checkStringBuilder.Append(drJob.JobUID.ToString("n"))
        checkStringBuilder.Append("|"c)
        checkStringBuilder.Append(drJob.ConsultantUID.ToString("n"))
        checkStringBuilder.Append("|"c)
        'checkStringBuilder.Append(drJob.ClientSiteUID.ToString("n"))
        'checkStringBuilder.Append("|"c)
        checkStringBuilder.Append(drJob.RequestUID.ToString("n"))

        If Not drJob.IsStartTimeNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(DateTime.SpecifyKind(drJob.StartTime, DateTimeKind.Utc).ToString("yyyyMMddHHmmss"))
        End If

        If Not drJob.IsFinishTimeNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(DateTime.SpecifyKind(drJob.FinishTime, DateTimeKind.Utc).ToString("yyyyMMddHHmmss"))
        End If

        If Not drJob.IsJobDetailsNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drJob.JobDetails)
        End If

        If Not drJob.IsReturnDateNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(DateTime.SpecifyKind(drJob.ReturnDate, DateTimeKind.Utc).ToString("yyyyMMdd"))
        End If

        If Not drJob.IsSignatoryNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drJob.Signatory)
        End If

        If Not drJob.IsMinutesTravelledNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drJob.MinutesTravelled)
        End If

        checkStringBuilder.Replace(" ", "")
        checkStringBuilder.Replace(vbTab, "")
        checkStringBuilder.Replace(vbCr, "")
        checkStringBuilder.Replace(vbLf, "")
        Dim checkString As String = checkStringBuilder.ToString()
        Debug.WriteLine(String.Format("Check string for Job {0}: {1}", drJob.JobUID, checkString))
        Dim result As String = Utilities.GetHash(checkString)
        Debug.WriteLine(String.Format("Checksum for Job {0}: {1}", drJob.JobUID, result)) 'New Guid(byteGuid).ToString))
        Return result
    End Function

    Friend Function RequestChecksum(ByVal drRequest As RequestRow) As String
        If drRequest Is Nothing Then Return Nothing
        Dim checkStringBuilder As New System.Text.StringBuilder(200)

        checkStringBuilder.Append(drRequest.RequestUID.ToString("n"))
        checkStringBuilder.Append("|"c)
        checkStringBuilder.Append(drRequest.ClientSiteUID.ToString("n"))

        If Not drRequest.IsAssignedToUIDNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drRequest.AssignedToUID.ToString("n"))
        End If

        If Not drRequest.IsContactNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drRequest.Contact)
        End If

        If Not drRequest.IsShortDescriptionNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drRequest.ShortDescription)
        End If

        If Not drRequest.IsLongDescriptionNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drRequest.LongDescription)
        End If

        If Not drRequest.IsConsultantStatusIDNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drRequest.ConsultantStatusID)
        End If

        If Not drRequest.IsNextActionDateNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drRequest.NextActionDate.Date.ToString("yyyyMMdd"))
        End If

        checkStringBuilder.Replace(" ", "")
        checkStringBuilder.Replace(vbTab, "")
        checkStringBuilder.Replace(vbCr, "")
        checkStringBuilder.Replace(vbLf, "")
        Dim checkString As String = checkStringBuilder.ToString()
        Debug.WriteLine(String.Format("Check string for Request {0}: {1}", drRequest.RequestUID, checkString))
        Dim result As String = Utilities.GetHash(checkString)
        Debug.WriteLine(String.Format("Checksum for Request {0}: {1}", drRequest.RequestUID, result)) 'New Guid(byteGuid).ToString))
        Return result
    End Function

End Module
