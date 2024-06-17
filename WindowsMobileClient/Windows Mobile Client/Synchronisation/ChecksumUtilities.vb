'Imports activiser.WebService
Imports activiser.Library.WebService
Imports activiser.Library.WebService.activiserDataSet

Module ChecksumUtilities
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
            checkStringBuilder.Append(DateTime.SpecifyKind(drJob.StartTime, DateTimeKind.Utc).ToString("yyyyMMddHHmmss", WithoutCulture))
        End If

        If Not drJob.IsFinishTimeNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(DateTime.SpecifyKind(drJob.FinishTime, DateTimeKind.Utc).ToString("yyyyMMddHHmmss", WithoutCulture))
        End If

        If Not drJob.IsJobDetailsNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(drJob.JobDetails)
        End If

        If Not drJob.IsReturnDateNull Then
            checkStringBuilder.Append("|"c)
            checkStringBuilder.Append(DateTime.SpecifyKind(drJob.ReturnDate, DateTimeKind.Utc).ToString("yyyyMMdd", WithoutCulture))
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
        Dim result As String = Utilities.GetHash(checkStringBuilder.ToString())
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
            checkStringBuilder.Append(drRequest.NextActionDate.Date.ToString("yyyyMMdd", WithoutCulture))
        End If

        checkStringBuilder.Replace(" ", "")
        checkStringBuilder.Replace(vbTab, "")
        checkStringBuilder.Replace(vbCr, "")
        checkStringBuilder.Replace(vbLf, "")
        Dim checkString As String = checkStringBuilder.ToString()
        Debug.WriteLine(String.Format("Check string for Request {0}: {1}", drRequest.RequestUID, checkString))
        Dim result As String = Utilities.GetHash(checkString)
        Debug.WriteLine(String.Format("Checksum for Request {0}: {1}", drRequest.RequestUID, result))
        Return result
    End Function
End Module
