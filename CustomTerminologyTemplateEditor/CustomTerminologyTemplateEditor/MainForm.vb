Public Class MainForm

    Private _currentFileName As String

    Private Sub loadXml(ByVal fileName As String)
        Me.BindingSource1.SuspendBinding()
        Me.FieldList1.BindingSource1.SuspendBinding()

        _currentFileName = fileName
        Dim ds As New CustomStringsDataSet
        ' ds.EnforceConstraints = False
        ds.ReadXml(fileName)

        If Main.CustomStringsDataSet Is Nothing Then
            Main.CustomStringsDataSet = ds
        Else
            Main.CustomStringsDataSet.Clear()

            Main.CustomStringsDataSet.Merge(ds)
            Main.CustomStringsDataSet.AcceptChanges()
        End If


        Me.CustomStringsDataSet = Main.CustomStringsDataSet

        Me.BindingSource1.DataSource = Main.CustomStringsDataSet
        Me.BindingSource1.DataMember = Main.CustomStringsDataSet.StringModule.TableName
        Me.BindingSource1.ResumeBinding()

        Me.FieldList1.CustomStringsDataSet = Main.CustomStringsDataSet
        Me.FieldList1.BindingSource1.DataSource = Main.CustomStringsDataSet
        Me.FieldList1.BindingSource1.DataMember = Main.CustomStringsDataSet.StringValue.TableName
        Me.FieldList1.BindingSource1.ResumeBinding()
        Me.FieldList1.BindingSource1.CurrencyManager.Refresh()
    End Sub

    Private Sub saveXml(ByVal fileName As String)
        Dim xw As New Xml.XmlTextWriter(fileName, System.Text.Encoding.UTF8)
        xw.Formatting = Xml.Formatting.Indented

        Me.CustomStringsDataSet.WriteXml(xw, XmlWriteMode.IgnoreSchema)
        Me.CustomStringsDataSet.AcceptChanges()
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.CustomStringsDataSet.HasChanges() Then
            Select Case MsgBox("Save changes?", MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Yes
                    saveXml(_currentFileName)
                Case MsgBoxResult.No
                    Exit Select
                Case MsgBoxResult.Cancel
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadXml(My.Settings.FormLabelsXml)
    End Sub

    Private Sub BindingSource1_CurrentItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingSource1.CurrentItemChanged
        If CurrentForm IsNot Nothing Then
            Try
                If Not CurrentForm.IsNull(Me.CustomStringsDataSet.StringModule.ModuleNameColumn.ColumnName) Then
                    Me.FieldList1.ModuleName = CurrentForm.ModuleName
                    Me.FieldList1.ClientKey = CurrentForm.ClientKey
                    Me.FieldList1.LanguageId = CurrentForm.LanguageId
                    Me.FieldList1.BindingSource1.Filter = String.Format("ModuleName='{0}' AND ClientKey={1} AND LanguageId={2}", CurrentForm.ModuleName, CurrentForm.ClientKey, CurrentForm.LanguageId)
                    Me.CustomStringsDataSet.StringValue.ModuleNameColumn.DefaultValue = CurrentForm.ModuleName
                    Me.CustomStringsDataSet.StringValue.ClientKeyColumn.DefaultValue = CurrentForm.ClientKey
                    Me.CustomStringsDataSet.StringValue.LanguageIdColumn.DefaultValue = CurrentForm.LanguageId
                End If
                If Not Me.FieldList1.Enabled AndAlso Not CurrentForm.IsNull(Me.CustomStringsDataSet.StringModule.ModuleNameColumn.ColumnName) Then
                    Me.FieldList1.Enabled = True
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End If
    End Sub

    Private Sub BindingSource1_PositionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingSource1.PositionChanged
        If _inPasteRows Then Exit Sub

        If Me.BindingSource1.Position = -1 Then Return

        Try
            Dim drv As DataRowView = TryCast(Me.BindingSource1.Current, DataRowView)
            If drv IsNot Nothing Then
                CurrentForm = TryCast(drv.Row, CustomStringsDataSet.StringModuleRow)
                If CurrentForm IsNot Nothing Then
                    If CurrentForm.IsNull(Me.CustomStringsDataSet.StringModule.ModuleNameColumn.ColumnName) Then
                        Me.FieldList1.BindingSource1.Filter = "ModuleName is null AND ClientKey is null AND LanguageId is null"
                        Me.FieldList1.Enabled = False
                        Me.CustomStringsDataSet.StringValue.ModuleNameColumn.DefaultValue = DBNull.Value
                        Me.CustomStringsDataSet.StringValue.ClientKeyColumn.DefaultValue = DBNull.Value
                        Me.CustomStringsDataSet.StringValue.LanguageIdColumn.DefaultValue = DBNull.Value
                        Me.FieldList1.ModuleName = ""
                        Me.FieldList1.ClientKey = 1
                        Me.FieldList1.LanguageId = 1
                    Else
                        Me.FieldList1.BindingSource1.Filter = String.Format("ModuleName='{0}' AND ClientKey={1} AND LanguageId={2}", CurrentForm.ModuleName, CurrentForm.ClientKey, CurrentForm.LanguageId)
                        Me.CustomStringsDataSet.StringValue.ModuleNameColumn.DefaultValue = CurrentForm.ModuleName
                        Me.CustomStringsDataSet.StringValue.ClientKeyColumn.DefaultValue = CurrentForm.ClientKey
                        Me.CustomStringsDataSet.StringValue.LanguageIdColumn.DefaultValue = CurrentForm.LanguageId
                        Me.FieldList1.ModuleName = CurrentForm.ModuleName
                        Me.FieldList1.ClientKey = CurrentForm.ClientKey
                        Me.FieldList1.LanguageId = CurrentForm.LanguageId
                    End If
                End If
            End If
        Catch ex As Exception
            CurrentForm = Nothing
        End Try
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSaveButton.Click
        saveXml(_currentFileName)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        If Me.CustomStringsDataSet.HasChanges() Then
            Select Case MsgBox("Save changes?", MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Yes
                    saveXml(_currentFileName)
                Case MsgBoxResult.No
                    Exit Select
                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select
        End If
        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not String.IsNullOrEmpty(Me.OpenFileDialog1.FileName) Then
                Me._currentFileName = Me.OpenFileDialog1.FileName
                loadXml(Me._currentFileName)
            End If
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripItem.Click
        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not String.IsNullOrEmpty(Me.SaveFileDialog1.FileName) Then
                Me._currentFileName = Me.SaveFileDialog1.FileName
                saveXml(Me._currentFileName)
            End If
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If Me.CustomStringsDataSet.HasChanges() Then
            Select Case MsgBox("Save changes?", MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Yes
                    saveXml(_currentFileName)
                Case MsgBoxResult.No
                    Exit Select
                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select
        End If
        Me.Close()
    End Sub

    Private _inPasteRows As Boolean
    Private Sub PasteFieldsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click
        If _inPasteRows Then Return
        Try
            _inPasteRows = True
            Me.FieldList1.BindingSource1.SuspendBinding()
            Me.CustomStringsDataSet.EnforceConstraints = False
            Me.DataGridView1.Enabled = False
            Main.PasteRows()
        Catch ex As Exception

        Finally
            Me.BindingSource1.CurrencyManager.Refresh()
            Me.FieldList1.BindingSource1.CurrencyManager.Refresh()
            Me.FieldList1.BindingSource1.ResumeBinding()
            Me.DataGridView1.Enabled = True
            _inPasteRows = False
        End Try
    End Sub

    Private Sub FieldList1_FieldRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FieldList1.FieldRowChanged
        If Not Me.CustomStringsDataSet.EnforceConstraints Then
            Try
                Me.CustomStringsDataSet.EnforceConstraints = True
                Me.DataGridView1.Enabled = True
                Me.BindingSource1.CurrencyManager.Refresh()
            Catch ex As Data.ConstraintException
                Me.DataGridView1.Enabled = False
            End Try
        End If
        If Me.CustomStringsDataSet.HasErrors Then
            If Me.CustomStringsDataSet.StringModule.HasErrors Then
                '
            ElseIf Me.CustomStringsDataSet.StringValue.HasErrors Then
                For Each dr As CustomStringsDataSet.StringValueRow In Me.CustomStringsDataSet.StringValue.GetErrors
                    Try
                        dr.ClearErrors()
                        Me.CustomStringsDataSet.EnforceConstraints = True
                    Catch ex As Exception

                    End Try
                Next
            End If
        End If
    End Sub

    Private Sub AddFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        Dim gnf As New GetNewForm
        If gnf.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim newModuleName As String = gnf.ModuleName
            Dim newDisplayName As String = gnf.DisplayName

            If Not String.IsNullOrEmpty(newModuleName) Then
                If String.IsNullOrEmpty(newDisplayName) Then newDisplayName = newModuleName
                Me.CustomStringsDataSet.StringModule.AddStringModuleRow(newModuleName, newDisplayName, gnf.ClientKey, gnf.LanguageId)
                Me.BindingSource1.CurrencyManager.Refresh()
                Me.FieldList1.BindingSource1.CurrencyManager.Refresh()
            End If
        End If
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click
        Clipboard.SetDataObject(Me.FieldList1.DataGridView1.GetClipboardContent())
    End Sub

    Private Sub DoToolTipsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoToolTipsButton.Click
        Me.FieldList1.PasteToolTips = Me.DoToolTipsButton.Checked
    End Sub

    Private oldModuleName As String
    Private oldClientKey As Integer
    Private oldLanguageId As Integer

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        If e.ColumnIndex = Me.ModuleNameDataGridViewTextBoxColumn.Index Then
            ' form name changed
            Dim cell As DataGridViewCell = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex)
            oldModuleName = CStr(cell.Value)
        ElseIf e.ColumnIndex = Me.ClientKey.Index Then
            Dim cell As DataGridViewCell = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex)
            oldClientKey = CInt(cell.Value)
        ElseIf e.ColumnIndex = Me.LanguageId.Index Then
            Dim cell As DataGridViewCell = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex)
            oldLanguageId = CInt(cell.Value)
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        If Me.DataGridView1.DataSource Is Nothing Then Return
        If Me.BindingSource1.DataSource Is Nothing Then Return

        If e.RowIndex = -1 OrElse e.ColumnIndex = -1 Then Return

        If e.ColumnIndex = Me.ModuleNameDataGridViewTextBoxColumn.Index Then
            ' form name changed
            Dim cell As DataGridViewCell = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex)
            Dim newCellValue As String = CStr(cell.Value)
            If newCellValue <> oldModuleName Then 'form name changed
                Dim drv As DataRowView = CType(Me.DataGridView1.CurrentRow.DataBoundItem, DataRowView)
                Dim fr As CustomStringsDataSet.StringModuleRow = CType(drv.Row, CustomStringsDataSet.StringModuleRow)
                For Each row As CustomStringsDataSet.StringValueRow In Me.CustomStringsDataSet.StringValue
                    If row.ModuleName = oldModuleName AndAlso (row.ClientKey = fr.ClientKey) AndAlso (row.LanguageId = fr.LanguageId) Then
                        row.ModuleName = newCellValue
                    End If
                Next
                Me.FieldList1.Refresh()
            End If
        ElseIf e.ColumnIndex = Me.ClientKey.Index Then
            Dim cell As DataGridViewCell = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex)
            Dim newClientKey As Integer = CInt(cell.Value)
            If newClientKey <> oldClientKey Then 'form name changed
                Dim drv As DataRowView = CType(Me.DataGridView1.CurrentRow.DataBoundItem, DataRowView)
                'Me.BindingSource1.SuspendBinding()
                Dim fr As CustomStringsDataSet.StringModuleRow = CType(drv.Row, CustomStringsDataSet.StringModuleRow)
                For Each row As CustomStringsDataSet.StringValueRow In Me.CustomStringsDataSet.StringValue
                    If row.ModuleName = fr.ModuleName AndAlso (row.ClientKey = oldClientKey) AndAlso (row.LanguageId = fr.LanguageId) Then
                        row.ClientKey = newClientKey
                    End If
                Next
                'Me.BindingSource1.ResumeBinding()
                Me.FieldList1.Refresh()
            End If
        ElseIf e.ColumnIndex = Me.LanguageId.Index Then
            Dim cell As DataGridViewCell = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex)
            Dim newLanguageId As Integer = CInt(cell.Value)
            If newLanguageId <> oldLanguageId Then 'form name changed
                Dim drv As DataRowView = CType(Me.DataGridView1.CurrentRow.DataBoundItem, DataRowView)
                ' Me.BindingSource1.SuspendBinding()
                Dim fr As CustomStringsDataSet.StringModuleRow = CType(drv.Row, CustomStringsDataSet.StringModuleRow)
                For Each row As CustomStringsDataSet.StringValueRow In Me.CustomStringsDataSet.StringValue
                    If row.ModuleName = fr.ModuleName AndAlso (row.ClientKey = fr.ClientKey) AndAlso (row.LanguageId = oldLanguageId) Then
                        row.LanguageId = newLanguageId
                    End If
                Next
                ' Me.BindingSource1.ResumeBinding()
                Me.FieldList1.Refresh()
            End If
        End If
    End Sub

    Private Sub SettingsButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReplaceOnPasteButton.CheckedChanged, DoToolTipsButton.CheckedChanged
        ReplaceOnPaste = Me.ReplaceOnPasteButton.Checked
        DoToolTips = Me.DoToolTipsButton.Checked
    End Sub
End Class
