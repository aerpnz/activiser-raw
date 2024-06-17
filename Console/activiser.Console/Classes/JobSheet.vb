Option Strict Off

Imports System.IO
Imports activiser.Library
Imports System.Text.RegularExpressions
Imports activiser.Library.activiserWebService
Imports activiser.Library.activiserWebService.Utility

' Authors - Michael Coulam and Tim Khoo
' Date Modified - 12-October-2004
' This class creates a Job Sheet for a particular Job ID and renders it in HTML.

Public Class JobSheet
    Private jobRow As activiserDataSet.JobRow
    'Private Shared serverProfile As New Utility.ServerProfileDataTable
    Private Shared mailAddressRow As Utility.ServerSettingRow
    Private Shared licenseRow As Utility.ServerSettingRow
    Private Shared adminEmail As String = String.Empty
    Private Shared activiserEmail As String = String.Empty
    Private Shared licenseeName As String = String.Empty

    Private _signature As Image

    Shared Sub New()
        GetServerProfile()
    End Sub

    Sub New(ByVal drJob As activiserDataSet.JobRow, ByVal sign As Image)
        If drJob Is Nothing Then
            Return
        End If
        jobRow = drJob
        _signature = sign
    End Sub

    'Print out to HTML.
    Private Sub SaveSignatureToFile(ByVal SignatureFile As String)
        Try
            _signature.Save(SignatureFile, System.Drawing.Imaging.ImageFormat.Bmp) 'Save the signature image to bmp.
        Catch ex As Exception
            Try
                Dim bmp As New Bitmap(224, 72, Imaging.PixelFormat.Format24bppRgb)
                For x As Integer = 0 To 223
                    For y As Integer = 0 To 71
                        bmp.SetPixel(x, y, Color.White)
                    Next
                Next
                bmp.Save(SignatureFile, System.Drawing.Imaging.ImageFormat.Bmp) 'Save the signature image to bmp.
            Catch ex2 As Exception
            End Try
        End Try
    End Sub

    Public Function GenerateJobHtml() As String
        Dim TempFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp()
        Dim SignatureFile As String = My.Computer.FileSystem.CombinePath(TempFolder, "activiserJob" & jobRow.JobUID.ToString & "Signature.bmp")
        Dim tempfilename As String = My.Computer.FileSystem.CombinePath(TempFolder, "activiserJob" & jobRow.JobUID.ToString & ".HTML")

        Dim template As String = Nothing
        Dim templateFilename As String = My.Settings.JobSheetTemplateFile
        If My.Computer.FileSystem.FileExists(templateFilename) Then
            template = My.Computer.FileSystem.ReadAllText(templateFilename)
        ElseIf My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CombinePath("HtmlTemplates", templateFilename)) Then
            template = My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.CombinePath("HtmlTemplates", templateFilename))
        End If

        If String.IsNullOrEmpty(template) Then Return Nothing

        Dim HTMLWriter As New StreamWriter(tempfilename) 'Create the jobsheet in html
        HTMLWriter.Write(formatHtml(template, SignatureFile))
        HTMLWriter.Close()
        SaveSignatureToFile(SignatureFile)
        Return tempfilename
    End Function

    'Private Function GetAncestorRow(ByVal parentList As Stack(Of String)) As DataRow
    '    Dim result As DataRow
    '    If parentList.Count = 1 Then
    '        ' tis me
    '    Else
    '        parentList.Pop()
    '        GetAncestorRow(parentList)
    '    End If
    '    For Each rellie As DataRelation In jobRow.Table.ParentRelations
    '        If rellie.ParentTable.TableName = tableName Then

    '        End If
    '    Next
    '    Return result
    'End Function

    Private Shared Function FindRelation(ByVal dt As DataTable, ByVal candidateName As String) As Data.DataRelation
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

    Private Shared Function FindRelatedRow(ByVal dr As DataRow, ByVal candidateName As String) As DataRow
        Dim parent As DataRelation = FindRelation(dr.Table, candidateName)
        If parent Is Nothing Then
            Return Nothing
        Else
            Return dr.GetParentRow(parent)
        End If
    End Function

    Private Function formatHtml(ByRef template As String, ByVal sigFileName As String) As String
        Dim message As New System.Text.StringBuilder(template, template.Length * 2)

        Dim jobDuration As TimeSpan = jobRow.FinishTime - jobRow.StartTime
        Dim jobLocalStartTime As DateTime = jobRow.JobDate
        Dim jobLocalFinishTime As DateTime = jobLocalStartTime + jobDuration
        message.Replace("{Signature.bmp}", sigFileName)

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
                                'If Not (tableName = "Job" AndAlso columnName = "JobDate") Then dv = dv.ToLocalTime()
                                If String.IsNullOrEmpty(formatSpecifier) Then
                                    replacementText = dv.ToString()
                                Else
                                    replacementText = dv.ToString(formatSpecifier)
                                End If
                            Else
                                If String.IsNullOrEmpty(formatSpecifier) Then
                                    replacementText = dr(dc).ToString()
                                    replacementText = replacementText.Replace(vbCrLf, "<br>").Replace(vbCr, "<br>").Replace(vbLf, "<br>")
                                Else
                                    Dim format As String = "{0:" & formatSpecifier & "}"
                                    replacementText = String.Format(format, dr(dc))
                                    replacementText = replacementText.Replace(vbCrLf, "<br>").Replace(vbCr, "<br>").Replace(vbLf, "<br>")
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

        If Not template.Contains("{LicenseeName}") Then
            If template.Contains("</BODY>") Then
                message.Insert(message.ToString.LastIndexOf("</BODY>", StringComparison.OrdinalIgnoreCase), My.Resources.JobSheetLicenseePostScript)
            ElseIf template.Contains("</HTML>") Then
                message.Insert(message.ToString.LastIndexOf("</HTML>", StringComparison.OrdinalIgnoreCase), My.Resources.JobSheetLicenseePostScript)
            Else
                message.Append(My.Resources.JobSheetLicenseePostScript)
            End If
        End If
        message.Replace("{LicenseeName}", licenseeName) ' - activiser license holder name
        message.Replace("{AdminEmail}", adminEmail) ' - activiser administrator email address 
        message.Replace("{ActiviserEmail}", activiserEmail) ' - activiser service email address 
        message.Replace("™", "&trade;")

        Return message.ToString
    End Function

    Private Shared Sub GetServerProfile()
        Try
            Dim spt = ConsoleData.WebService.ConsoleGetServerProfile(deviceId, ConsoleUser.ConsultantUID, True)
            Dim maq = From ss As ServerSettingRow In spt 

            For Each spr As Utility.ServerSettingRow In maq
                Select Case spr.Name
                    Case "Licensee" : licenseeName = spr.Value
                    Case "ActiviserEmailAddress" : activiserEmail = spr.Value
                    Case "AdministratorEmailAddress" : adminEmail = spr.Value
                End Select
            Next

        Catch ex As Exception
            DisplayException.Show(ex, My.Resources.JobSheetServerProfileError, Icons.Critical)
        End Try
    End Sub

End Class
