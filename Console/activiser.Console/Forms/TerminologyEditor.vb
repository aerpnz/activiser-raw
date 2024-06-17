Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.ComponentModel
Imports activiser.Library
Imports activiser.Library.activiserWebService
Imports activiser.Library.activiserWebService.LanguageDataSet

Public Class TerminologyEditor
    Private Const STR_DefaultValue As String = "DefaultValue"

    Dim cfr As CustomStringsDataSet.FormRow
    Dim clr As LabelRow

    Dim _haveLoaded As Boolean

    Friend Sub SaveTerminology()
        If Me.CustomisationTables.HasChanges Then
            Dim ds As New LanguageDataSet
            ds.Merge(Me.CustomisationTables.GetChanges())
            Dim now As DateTime = DateTime.UtcNow.AddMinutes(5) ' add a few minutes as a buffer for simultaneous sync's
            For Each dr As LabelRow In ds.Label
                If dr.RowState = DataRowState.Modified Then
                    dr.ModifiedDateTime = now
                End If
            Next
            Dim result As LanguageDataSet = ConsoleData.WebService.ConsoleUpdateTerminology(deviceId, ConsoleUser.ConsultantUID, ds)
            If result IsNot Nothing Then

                Debug.WriteLine(result.Label.Rows.Count)
                If result.Label.Rows.Count <> 0 Then
                    Terminology.Load(result)
                End If
            End If
        End If
    End Sub

    Private Sub TerminologyEditor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        saveTerminology()
    End Sub

    Private Sub TerminologyEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Terminology.LoadLabels(Me)
        Terminology.LoadToolTips(Me, ToolTipProvider)
        If New IO.FileInfo(My.Settings.CustomLabelTemplates).Exists Then
            Try
                LoadData()
                LoadTree()
                _haveLoaded = True
            Catch ex As Xml.XmlException

            Catch ex As IO.FileNotFoundException

            End Try
        End If
    End Sub

    Private Sub LoadTree()
        'Dim tvRoot As TreeNode
        Dim tvNode As TreeNode

        ' tvRoot = Me.TreeView.Nodes.Add("Root")

        For Each cltfr As CustomStringsDataSet.FormRow In CustomLabelTemplates.Form.Select("", String.Format("{0},{1},{2}", CustomLabelTemplates.Form.ClientKeyColumn.ColumnName, CustomLabelTemplates.Form.LanguageIdColumn.ColumnName, CustomLabelTemplates.Form.FormNameColumn.ColumnName))
            Dim clientOrConsoleText As String
            Select Case cltfr.ClientKey
                Case 1 ' PDA Client
                    clientOrConsoleText = "Client"
                Case 2 ' Console
                    clientOrConsoleText = "Console"
                Case 4 ' Outlook
                    clientOrConsoleText = "Outlook"
                Case Else
                    clientOrConsoleText = "Unknown"
            End Select

            tvNode = New TreeNode(String.Format("({0}) {1}", clientOrConsoleText, cltfr.FormName))
            If String.IsNullOrEmpty(cltfr.DisplayName) Then
                tvNode.Text = String.Format("({0}) {1}", clientOrConsoleText, cltfr.FormName)
            Else
                tvNode.Text = String.Format("({0}) {1}", clientOrConsoleText, cltfr.DisplayName)
            End If
            tvNode.Tag = cltfr
            Me.FormList.Nodes.Add(tvNode)
        Next
    End Sub

    Private Sub LoadData()
        'If cfr Is Nothing Then Exit Sub
        Try
            CustomLabelTemplates.ReadXml(My.Settings.CustomLabelTemplates)

            CustomisationTables.Clear()
            CustomisationTables.Label.BeginLoadData()
            
            Dim currentData As LanguageDataSet = ConsoleData.WebService.GetTerminology(deviceId, ConsoleUser.ConsultantUID, ClientKey.All, My.Settings.LanguageId, DateTime.MinValue)
            CustomisationTables.Merge(currentData)

            If Not CustomisationTables.Label.Columns.Contains(STR_DefaultValue) Then
                CustomisationTables.Label.Columns.Add(STR_DefaultValue, GetType(String))
                Me.DefaultValueColumn.DataPropertyName = STR_DefaultValue
            End If

            For Each field As CustomStringsDataSet.FieldRow In CustomLabelTemplates.Field
                Dim extantRows() As DataRow = _
                    Me.CustomisationTables.Label.Select(String.Format( _
                        "ClientKey={0} and LanguageId={1} and FormName='{2}' and FieldName='{3}'", _
                        field.ClientKey, field.LanguageId, field.FormName, field.FieldName))
                'If field.FormName = "AboutForm" Then Stop
                If extantRows IsNot Nothing AndAlso extantRows.Length = 1 Then ' should only be one !
                    Dim extantLabel As DataRow = extantRows(0)
                    extantLabel(STR_DefaultValue) = field.DefaultValue
                Else
                    Dim defaultLr As LabelRow = CustomisationTables.Label.AddLabelRow(field.ClientKey, field.FormName, field.FieldName, field.LanguageId, field.DefaultValue, DateTime.UtcNow, DateTime.UtcNow)
                    defaultLr(STR_DefaultValue) = field.DefaultValue
                End If
            Next

            CustomisationTables.AcceptChanges()
            Me.DefaultValueColumn.DataPropertyName = STR_DefaultValue
            CustomisationTables.Label.EndLoadData()

        Catch ex As Exception
            Dim ed As New activiser.Library.DisplayException(ex)
            Debug.WriteLine(ed.ToString)
            ed.Show()
        End Try

    End Sub


    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileExitMenuItem.Click
        'Exit the application
        ' Global.System.Windows.Forms.Application.Exit()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FileSaveAsMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Xml Files (*.xml)|*.xml"
        SaveFileDialog.DefaultExt = "XML"
        If SaveFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim FileName As String = SaveFileDialog.FileName
        If String.IsNullOrEmpty(FileName) Then Exit Sub
        Me.CustomisationTables.WriteXml(FileName, XmlWriteMode.WriteSchema)
    End Sub

    Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles FormList.AfterSelect
        cfr = TryCast(Me.FormList.SelectedNode.Tag, CustomStringsDataSet.FormRow)
        Me.CustomLabelBindingSource.Filter = String.Format("FormName='{0}' and ClientKey={1} and LanguageId={2}", cfr.FormName, cfr.ClientKey, cfr.LanguageId)
    End Sub

    Private Sub FileSaveAllMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileSaveAllMenuItem.Click
        'saveTerminology()
        ConsoleData.WebService.ConsoleUpdateTerminology(DeviceId, ConsoleUser.ConsultantUID, Me.CustomisationTables)
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileSaveMenuItem.Click
        saveTerminology()
    End Sub

    Private Sub SaveClientItemsAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileSaveClientItemsAsMenuItem.Click
        SaveTerminology()
        Using clientDS As LanguageDataSet = ConsoleData.WebService.GetTerminology(deviceId, ConsoleUser.ConsultantUID, 1, -1, DateTime.MinValue)
            Using SaveFileDialog As New SaveFileDialog
                SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                SaveFileDialog.Filter = "Xml Files (*.xml)|*.xml"
                If SaveFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Dim FileName As String = SaveFileDialog.FileName
                    If Not String.IsNullOrEmpty(FileName) Then
                        clientDS.WriteXml(FileName, XmlWriteMode.WriteSchema)
                    End If
                End If
            End Using
        End Using
    End Sub

    Private Sub ImportToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Xml Files (*.xml)|*.xml"
        OpenFileDialog.CheckFileExists = True
        OpenFileDialog.DefaultExt = "XML"
        If OpenFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim FileName As String = OpenFileDialog.FileName
        If String.IsNullOrEmpty(FileName) Then Exit Sub
        Dim ds As New LanguageDataSet
        Try
            ds.ReadXml(FileName)
            Me.CustomisationTables.Merge(ds, False, MissingSchemaAction.Ignore) '.WriteXml(FileName, XmlWriteMode.WriteSchema)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LabelList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles LabelList.CellContentClick
        If Not _haveLoaded Then Return
        If Me.LabelList.Columns(e.ColumnIndex) Is Me.ResetToDefaultButton Then ' reset to default
            If clr IsNot Nothing Then
                clr.Label = CStr(Me.LabelList.Item(Me.DefaultValueColumn.Name, e.RowIndex).Value)
                clr.ModifiedDateTime = DateTime.UtcNow
            End If
        End If
    End Sub

    Private Sub LabelList_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles LabelList.CellValueChanged
        If Not _haveLoaded Then Return
        If e.ColumnIndex = -1 OrElse e.RowIndex = -1 Then Return
        If Not Me.LabelList.IsCurrentRowDirty Then Return
        Me.LabelList.Rows(e.RowIndex).Cells(Me.ModifiedDateTimeColumn.Index).Value = DateTime.UtcNow
    End Sub

    Private Sub LabelList_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles LabelList.RowEnter
        If Not _haveLoaded Then Return
        Dim drv As DataRowView = TryCast(LabelList.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv IsNot Nothing Then
            clr = TryCast(drv.Row, LabelRow)
        End If
    End Sub

    Private Sub LabelList_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles LabelList.DataError
        'WTF!
        TraceError("DataError in LabelList: Context:{0}, Row:{1}, Column:{2}", CInt(e.Context), e.RowIndex, e.ColumnIndex)
        TraceError(e.Exception)
        e.ThrowException = False
    End Sub

#Region "Find and Replace"

    Dim WithEvents fr As FindAndReplace
    Private Sub FindReplaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindReplaceToolStripMenuItem.Click
        fr = New FindAndReplace()
        fr.Owner = Me
        fr.TopLevel = True

        fr.Show()
    End Sub

    Private Sub fr_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles fr.Disposed
        fr = Nothing
    End Sub

    Private Sub fr_FindClicked(ByVal sender As Object, ByVal e As FindEventArgs) Handles fr.FindClicked
        'If Me.Tabs.SelectedTab IsNot Me.LabelsTabPage Then
        '    Return
        'End If

        Dim lFrTextToFind As String = fr.TextToFind
        Me.LabelList.SelectionMode = DataGridViewSelectionMode.CellSelect

        Dim foundIt As Boolean

        Dim inCellSelectionStart As Integer = -1
        'Dim inCellSelectionLength As Integer = -1

        If LabelList.IsCurrentCellInEditMode Then
            Dim labelCell As DataGridViewCell = LabelList.CurrentRow.Cells(Me.LabelColumn.Index)
            Dim label As String = CStr(labelCell.Value)
            Dim tb As TextBox = TryCast(Me.LabelList.EditingControl, TextBox)
            If tb IsNot Nothing Then
                inCellSelectionStart = tb.SelectionStart
                'inCellSelectionLength = tb.SelectionLength
                Dim nextIndex As Integer = label.IndexOf(lFrTextToFind, inCellSelectionStart + 1, StringComparison.CurrentCultureIgnoreCase)
                If nextIndex <> -1 Then
                    tb.SelectionStart = nextIndex
                    tb.SelectionLength = lFrTextToFind.Length
                    Return
                End If
            End If
        End If

        Dim currentRowIndex As Integer = LabelList.CurrentRow.Index + 1

        If currentRowIndex > LabelList.RowCount Then
            If MessageBox.Show("At end of data, try again from start?", My.Resources.activiserFormTitle, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                currentRowIndex = 0
            Else
                Return
            End If
        End If

        For rowIndex As Integer = currentRowIndex To Me.LabelList.Rows.Count - 1
            Dim dgvr As DataGridViewRow = Me.LabelList.Rows(rowIndex)
            Dim labelCell As DataGridViewCell = dgvr.Cells(Me.LabelColumn.Index)
            Dim label As String = CStr(labelCell.Value)

            If label IsNot Nothing AndAlso label.Contains(lFrTextToFind) Then
                dgvr.Selected = True
                ' labelCell.Selected = True
                Me.LabelList.CurrentCell = labelCell
                Me.LabelList.BeginEdit(False)

                Dim tb As TextBox = TryCast(Me.LabelList.EditingControl, TextBox)
                If tb IsNot Nothing Then
                    tb.Focus()
                    tb.SelectionStart = label.IndexOf(lFrTextToFind, StringComparison.Ordinal)
                    tb.SelectionLength = lFrTextToFind.Length
                    foundIt = True
                    Exit For
                End If
            End If
        Next

        If Not foundIt Then
            MessageBox.Show("Text not found")
        End If
    End Sub

    Private Sub fr_ReplaceAllClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles fr.ReplaceAllClicked
        'If Not Me.Tabs.SelectedTab Is Me.LabelsTabPage Then Return

        Dim lFrTextToFind As String = fr.TextToFind
        Dim lFrReplacementText As String = fr.ReplacementText

        Me.LabelList.SelectionMode = DataGridViewSelectionMode.CellSelect

        Dim found As Integer = 0
        'Dim foundIt As Boolean = False

        Dim inCellSelectionStart As Integer = -1
        'Dim inCellSelectionLength As Integer = -1

        If LabelList.IsCurrentCellInEditMode Then
            Dim labelCell As DataGridViewCell = LabelList.CurrentRow.Cells(Me.LabelColumn.Index)
            Dim label As String = CStr(labelCell.Value)
            Dim tb As TextBox = TryCast(Me.LabelList.EditingControl, TextBox)
            If tb IsNot Nothing Then
                inCellSelectionStart = tb.SelectionStart
                'inCellSelectionLength = tb.SelectionLength
                Dim nextIndex As Integer = label.IndexOf(lFrTextToFind, inCellSelectionStart + 1, StringComparison.CurrentCultureIgnoreCase)
                If nextIndex <> -1 Then
                    tb.SelectionStart = nextIndex
                    tb.SelectionLength = lFrTextToFind.Length
                    tb.SelectedText = lFrReplacementText
                    found += 1
                End If
            End If
        End If


        Dim currentRowIndex As Integer = LabelList.CurrentRow.Index
        For rowIndex As Integer = currentRowIndex To Me.LabelList.Rows.Count - 1
            Dim dgvr As DataGridViewRow = Me.LabelList.Rows(rowIndex)
            Dim labelCell As DataGridViewCell = dgvr.Cells(Me.LabelColumn.Index)
            Dim label As String = CStr(labelCell.Value)

            If label IsNot Nothing Then
                If label.Contains(lFrTextToFind) Then
                    Dim newLabel As String = label.Replace(lFrTextToFind, lFrReplacementText)
                    labelCell.Value = newLabel
                    found += 1
                End If
            End If
        Next

        MessageBox.Show(If(found = 0, "Text not found", String.Format("{0} records modified", found)))

        Me.LabelList.CurrentCell = Me.LabelList.Rows(currentRowIndex).Cells(Me.LabelColumn.Index)
    End Sub

    Private Sub fr_ReplaceClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles fr.ReplaceClicked
        'If Not Me.Tabs.SelectedTab Is Me.LabelsTabPage Then Return

        If LabelList.IsCurrentCellInEditMode Then
            'Dim labelCell As DataGridViewCell = LabelList.CurrentRow.Cells(Me.LabelColumn.Index)
            'Dim label As String = CStr(labelCell.Value)
            Dim tb As TextBox = TryCast(Me.LabelList.EditingControl, TextBox)
            If tb IsNot Nothing Then
                tb.SelectedText = fr.ReplacementText
                tb.SelectionLength = fr.ReplacementText.Length
            End If
        End If
    End Sub
#End Region

End Class
