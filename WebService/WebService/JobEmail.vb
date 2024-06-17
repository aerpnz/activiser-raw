Imports System.Net.Mail
Imports activiser.WebService.Utilities
Imports System.Data
Imports System.Text.RegularExpressions
Imports activiser.activiserDataSetTableAdapters

Public Module JobEmail
    Private subjectTemplate As String = String.Empty
    Private messageTemplate As String = String.Empty
    Private mailServer As String = My.Settings.DefaultMailServer
    Private mailServerPort As Short = My.Settings.DefaultMailServerPort
    Private adminEmail As String = String.Empty
    Private activiserEmail As String = String.Empty
    Private licenseeName As String = String.Empty
    Private emailAddressRegexPattern As String = String.Empty
    Private emailAddressRegex As Regex

    Private jta As New JobTableAdapter
    Private rta As New RequestTableAdapter
    Private sta As New ClientSiteTableAdapter
    Private cta As New ConsultantTableAdapter

    Private _lastProfileRefresh As DateTime = DateTime.MinValue

    Public Sub RefreshProfile()
        Try
            If DateTime.UtcNow < _lastProfileRefresh.AddMinutes(1) Then Return
            Dim thisProfileRefresh As DateTime = DateTime.UtcNow

            If Utility.LastServerSettingChange() > _lastProfileRefresh Then
                subjectTemplate = Utility.GetServerSetting("JobEmailSubjectTemplate", String.Empty)
                messageTemplate = Utility.GetServerSetting("JobEmailBodyTemplate", String.Empty)
                mailServer = Utility.GetServerSetting("MailServerAddress", mailServer)
                mailServerPort = Utility.GetServerSetting("MailServerPort", mailServerPort)
                adminEmail = Utility.GetServerSetting("AdministratorEmailAddress", String.Empty)
                activiserEmail = Utility.GetServerSetting("ActiviserEmailAddress", String.Empty)
                licenseeName = Utility.GetServerSetting("Licensee", String.Empty)
                emailAddressRegexPattern = Utility.GetServerSetting("EmailRegex", ".*")
                emailAddressRegex = New Regex(emailAddressRegexPattern)
            End If

            _lastProfileRefresh = thisProfileRefresh
        Catch ex As Exception
            LogError(activiserClientWebService.WebServiceGuid, DateTime.UtcNow, "Job Sheet Mailer", Reflection.MethodBase.GetCurrentMethod.Name, "Error retrieving mail server details", "ServerProfile", Guid.Empty, String.Empty, ex)

        Finally

        End Try
    End Sub

    Public Sub SendEmailForJob(ByVal jobUid As Guid)
        RefreshProfile()

        If String.IsNullOrEmpty(mailServer) OrElse String.IsNullOrEmpty(messageTemplate) OrElse String.IsNullOrEmpty(subjectTemplate) OrElse String.IsNullOrEmpty(adminEmail) OrElse String.IsNullOrEmpty(activiserEmail) Then
            LogError(activiserClientWebService.WebServiceGuid, DateTime.UtcNow, activiserClientWebService.WebServiceGuid.ToString(), "SendEmailForJob", "Missing configuration", "profile.Server", Guid.Empty, Nothing, Nothing)
            Return
        End If

        Try
            Dim ds As New DataSchema
            Dim coreDataSet As activiserDataSet = DataSchema.GetClientSchema(-1)

            If jta.FillByJobUid(coreDataSet.Job, jobUid) = 1 Then
                Dim jobRow As activiserDataSet.JobRow = coreDataSet.Job(0)
                If jobRow.IsEmailNull Then Return
                If String.IsNullOrEmpty(jobRow.Email.Trim) Then Return
                If jobRow.EmailStatus <> 0 Then Return ' already sent.

                If Not ValidateEmailAddress(jobRow.Email) Then
                    Throw New ArgumentOutOfRangeException(String.Format("Invalid email address ({1}) in job: {0}", jobRow.JobUID.ToString, jobRow.Email))
                End If

                ' populate supporting tables
                cta.FillByConsultantUid(coreDataSet.Consultant, jobRow.ConsultantUID)

                If rta.FillByRequestUid(coreDataSet.Request, jobRow.RequestUID) <> 1 Then
                    Throw New ApplicationException(String.Format("Unable to find request for job {0}", jobUid))
                End If

                Dim requestRow As activiserDataSet.RequestRow = coreDataSet.Request(0)
                sta.FillByClientSiteUid(coreDataSet.ClientSite, requestRow.ClientSiteUID)

                'TODO: use this data in the email !
                ' DataSchema.FillEntityDetail(coreDataSet, "Job", jobUid, ds, -1, "JobMailer")

                Dim message As String = formatHtml(messageTemplate, jobRow, True)
                Dim subject As String = formatHtml(subjectTemplate, jobRow, False)
                Try
                    Dim mc As New SmtpClient(mailServer, mailServerPort)
                    mc.Send(activiserEmail, jobRow.Email, subject.ToString, message.ToString)
                    jobRow.EmailStatus = 1
                    jta.Update(jobRow)
                Catch ex As Exception
                    Try
                        LogError(jobRow.ConsultantUID, DateTime.UtcNow, "Job Sheet Mailer", Reflection.MethodBase.GetCurrentMethod.Name, "Error sending job email", "Job", jobRow.JobUID, String.Empty, ex)
                        jobRow.EmailStatus = 129 ' MSB set = error. this needs work.
                        jta.Update(jobRow)
                    Catch ex2 As Exception

                    End Try
                End Try
            End If
        Catch ex As Exception
            LogError(activiserClientWebService.WebServiceGuid, DateTime.UtcNow, "Job Sheet Mailer", Reflection.MethodBase.GetCurrentMethod.Name, "Error setting up job email", "Job", jobUid, "Error populating email dataset", ex)
        End Try

    End Sub

    'Check email format
    Private Function ValidateEmailAddress(ByVal strEmailAddress As String) As Boolean
        'Dim regEx As New System.Text.RegularExpressions.Regex(My.Settings.EmailRegex)
        ' "^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")
        If emailAddressRegex Is Nothing Then Return True ' if the regex is missing assume correct, and hope mailer doesn't crash with bogus addresses !
        Return emailAddressRegex.IsMatch(strEmailAddress)
    End Function

    Private Function FindRelation(ByVal dt As DataTable, ByVal candidateName As String) As Data.DataRelation
        For Each rellie As DataRelation In dt.ParentRelations
            If rellie.ParentTable.TableName = candidateName Then
                Return rellie
            End If
        Next
        For Each rellie As DataRelation In dt.ChildRelations
            If rellie.ChildTable.TableName = candidateName Then
                Return rellie
            End If
        Next
        Return Nothing
    End Function

    Private Function FindRelatedRow(ByVal dr As DataRow, ByVal candidateName As String) As DataRow
        Dim parent As DataRelation = FindRelation(dr.Table, candidateName)
        If parent Is Nothing Then
            Return Nothing
        Else
            Return dr.GetParentRow(parent)
        End If
    End Function

    Private Function formatHtml(ByRef template As String, ByVal jobRow As activiserDataSet.JobRow, ByVal forceLicensee As Boolean) As String
        Dim message As New System.Text.StringBuilder(template, template.Length * 2)

        Dim jobDuration As TimeSpan = jobRow.FinishTime - jobRow.StartTime
        Dim jobLocalStartTime As DateTime = jobRow.JobDate
        Dim jobLocalFinishTime As DateTime = jobLocalStartTime + jobDuration

        Dim regex As New Regex("\{((?<table>[\w]+)\.)+(?<column>[\w]+)(\:(?<format>[\w]?.*))?\}")
        Dim matches As MatchCollection = regex.Matches(message.ToString)

        For Each match As Match In matches
            Dim tableName As String = match.Groups("table").Value
            Dim columnName As String = match.Groups("column").Value
            Dim formatSpecifier As String = match.Groups("format").Value
            Dim replacementText As String = match.Value
            If Not String.IsNullOrEmpty(tableName) Then
                If Not String.IsNullOrEmpty(columnName) Then
                    Dim dr As DataRow = Nothing
                    Dim dc As DataColumn = Nothing
                    If tableName = jobRow.Table.TableName Then
                        dr = jobRow
                        dc = jobRow.Table.Columns(columnName)
                    Else
                        If jobRow.Table.DataSet.Tables.Contains(tableName) Then
                            Dim dt As DataTable = jobRow.Table.DataSet.Tables(tableName)
                            If dt.Columns.Contains(columnName) Then
                                Dim parentRow As DataRow = FindRelatedRow(jobRow, tableName)
                                If parentRow IsNot Nothing Then
                                    dr = parentRow
                                    dc = dt.Columns(columnName)
                                End If
                            End If
                        End If
                    End If
                    If dr IsNot Nothing AndAlso dc IsNot Nothing Then
                        If dr.IsNull(dc) Then
                            replacementText = String.Empty
                        Else
                            If dc.DataType Is GetType(DateTime) Then
                                Dim dv As DateTime = CDate(dr(dc))
                                If tableName = "Job" Then
                                    If columnName = "StartTime" Then
                                        dv = jobLocalStartTime
                                    ElseIf columnName = "FinishTime" Then
                                        dv = jobLocalFinishTime
                                    End If
                                End If

                                If String.IsNullOrEmpty(formatSpecifier) Then
                                    replacementText = dv.ToString()
                                Else
                                    replacementText = dv.ToString(formatSpecifier)
                                End If
                            Else
                                If String.IsNullOrEmpty(formatSpecifier) Then
                                    replacementText = dr(dc).ToString()
                                Else
                                    Dim format As String = "{0:" & formatSpecifier & "}"
                                    replacementText = String.Format(format, dr(dc))
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            If match.Value = replacementText Then
                replacementText = String.Format("#ERROR:{0}", replacementText)
            End If
            message.Replace(match.Value, replacementText)
        Next

        If forceLicensee Then
            If Not template.Contains("{LicenseeName}") Then
                If template.Contains("</BODY>") Then
                    message.Insert(message.ToString.LastIndexOf("</BODY>"), My.Resources.JobSheetLicenseePostScript)
                    message.Replace("™", "&trade;")
                ElseIf template.Contains("</HTML>") Then
                    message.Insert(message.ToString.LastIndexOf("</HTML>"), My.Resources.JobSheetLicenseePostScript)
                    message.Replace("™", "&trade;")
                Else
                    message.Append(My.Resources.JobSheetLicenseePostScript)
                End If
            End If
        End If

        message.Replace("{LicenseeName}", licenseeName) ' - activiser license holder name
        message.Replace("{AdminEmail}", adminEmail) ' - activiser administrator email address 
        message.Replace("{ActiviserEmail}", activiserEmail) ' - activiser service email address 

        Return message.ToString()
    End Function

    Friend Function SendMessage(ByVal host As String, ByVal port As Integer, ByVal sender As String, ByVal recipients As String, ByVal subject As String, ByVal message As String, ByRef exceptionResult As Exception) As Integer
        If String.IsNullOrEmpty(host) Then Return -1
        If port = 0 Then Return -2
        If String.IsNullOrEmpty(sender) Then Return -3
        If String.IsNullOrEmpty(recipients) Then Return -4
        If String.IsNullOrEmpty(subject) Then Return -5

        Try
            Dim mc As New SmtpClient(host, port)
            mc.Send(sender, recipients, subject, message)
            Return 0
        Catch ex As SmtpFailedRecipientsException
            exceptionResult = ex
            Return 1
        Catch ex As SmtpException
            exceptionResult = ex
            Return 2
        Catch ex As ObjectDisposedException
            exceptionResult = ex
            Return 3
        Catch ex As InvalidOperationException
            exceptionResult = ex
            Return 4
        End Try
    End Function
End Module
