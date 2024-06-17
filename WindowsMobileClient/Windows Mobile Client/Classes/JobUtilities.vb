Imports activiser.Library
Imports activiser.Library.WebService.activiserDataSet

Module JobUtilities
    Private Const MODULENAME As String = "JobUtilities"

    'Private Sub RemoveCustomData(ByVal Job As JobRow)
    '    For Each cfr As WebService.FormDefinition.FormRow In gFormDefinitions.Form.Select("ParentEntity='Job'", gFormDefinitions.Form.PriorityColumn.ColumnName)
    '        If gClientDataSet.Tables.Contains(cfr.EntityName) Then
    '            Dim dt As DataTable = gClientDataSet.Tables(cfr.EntityName)
    '            If dt IsNot Nothing Then
    '                Const STR_FilterTemplate As String = "[{0}] = '{1}'"
    '                For Each customRow As DataRow In dt.Select(String.Format(WithoutCulture, STR_FilterTemplate, cfr.EntityParentFK, Job.JobUID))
    '                    If Not customRow.RowState = DataRowState.Deleted Then customRow.Delete()
    '                Next
    '            End If
    '        End If
    '    Next
    'End Sub

    Public Sub RemoveJob(ByVal Job As JobRow)
        If Job.RowState = DataRowState.Deleted Then Return

        Dim currentCursor As Cursor = Cursor.Current
        If Not System.Windows.Forms.Cursor.Current Is Cursors.WaitCursor Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        End If

        Try
            DeleteCustomData("Job", Job.JobUID)
            ConsultantConfig.RemoveItem("Job", Job.JobUID)
            Job.Delete()
        Catch ex As Exception
            LogError(MODULENAME, "RemoveJob", ex, False, Nothing)
        Finally
            System.Windows.Forms.Cursor.Current = currentCursor
        End Try
    End Sub

    Public Function JobOkForDeletion(ByVal job As JobRow) As Boolean
        Return job.JobStatusID <> JobStatusCodes.Draft AndAlso job.JobStatusID <> JobStatusCodes.Complete AndAlso job.JobStatusID <> JobStatusCodes.Signed
    End Function

    'Private _JobStatusIcons As System.Windows.Forms.ImageList

    'Public Function JobStatusIcons() As System.Windows.Forms.ImageList
    '    If _JobStatusIcons Is Nothing Then
    '        LoadImage()
    '    End If

    '    Return _JobStatusIcons
    'End Function

    'Public Sub LoadImage()
    '    Dim bm As Image
    '    Dim g As Graphics

    '    _JobStatusIcons = New System.Windows.Forms.ImageList
    '    For s As Integer = 0 To 6
    '        bm = New Bitmap(20, 20)
    '        If bm IsNot Nothing Then
    '            g = Graphics.FromImage(bm)
    '            Dim c As Color = GetStatusColor(s)
    '            g.Clear(c)
    '            g.FillRectangle(New SolidBrush(c), 0, 0, 19, 19)
    '            _JobStatusIcons.Images.Add(bm)
    '        End If
    '    Next
    'End Sub
End Module
