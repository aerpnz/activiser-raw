Imports activiser.Library.activiserWebService

Public Class StatusCustomisation

    Private _currentStatus As activiserDataSet.RequestStatusRow

    Private Sub LoadData()
        Me.CustomisationTables.ClientSiteStatus.Clear()
        Me.CustomisationTables.JobStatus.Clear()
        Me.CustomisationTables.RequestStatus.Clear()

        Me.CustomisationTables.ClientSiteStatus.Merge(ConsoleData.CoreDataSet.ClientSiteStatus)
        Me.CustomisationTables.JobStatus.Merge(ConsoleData.CoreDataSet.JobStatus)
        Me.CustomisationTables.RequestStatus.Merge(ConsoleData.CoreDataSet.RequestStatus)

        Me.CustomisationTables.AcceptChanges()
    End Sub

    Private Function SaveData() As Boolean
        Me.Validate()

        Me.ClientSiteStatusBindingSource.EndEdit()
        Me.JobStatusBindingSource.EndEdit()
        Me.RequestStatusBindingSource.EndEdit()

        Try
            Using dsUpdates As activiserDataSet = CType(Me.CustomisationTables.GetChanges, activiserDataSet)
                If ConsoleData.DataSetHasData(dsUpdates) <> 0 Then
                    Dim now As DateTime = DateTime.UtcNow
                    For Each dr As activiserDataSet.RequestStatusRow In dsUpdates.RequestStatus
                        dr.ModifiedDateTime = now
                    Next
                    For Each dr As activiserDataSet.JobStatusRow In dsUpdates.JobStatus
                        dr.ModifiedDateTime = now
                    Next
                    For Each dr As activiserDataSet.ClientSiteStatusRow In dsUpdates.ClientSiteStatus
                        dr.ModifiedDateTime = now
                    Next
                    ConsoleData.CoreDataSet.RequestStatus.Merge(dsUpdates.RequestStatus)
                    ConsoleData.CoreDataSet.JobStatus.Merge(dsUpdates.JobStatus)
                    ConsoleData.CoreDataSet.ClientSiteStatus.Merge(dsUpdates.ClientSiteStatus)
                End If
                Console.ConsoleData.StartRefresh()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientSiteStatusToolbarSaveButton.Click, JobStatusToolbarSaveButton.Click, RequestStatusToolbarSaveButton.Click
        SaveData()
    End Sub

    Private Sub Customisation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Cancel Then
            SaveData()
        End If
    End Sub

    Private Sub Customisation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Terminology.LoadLabels(Me)
        Terminology.LoadToolTips(Me, ToolTipProvider)
        LoadData()
    End Sub

    Private Sub RequestStatusBindingSource_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestStatusBindingSource.CurrentChanged
        Dim drv As System.Data.DataRowView = TryCast(RequestStatusBindingSource.Current, System.Data.DataRowView)
        If drv IsNot Nothing AndAlso drv.Row IsNot Nothing Then
            Dim currentStatus As activiserDataSet.RequestStatusRow = TryCast(drv.Row, activiserDataSet.RequestStatusRow)
            If currentStatus IsNot Nothing Then
                _currentStatus = currentStatus
                Debug.WriteLine(_currentStatus.RowState.ToString())
                If _currentStatus.RowState = DataRowState.Detached Then ' new row
                    _currentStatus.Colour = 0
                    _currentStatus.DisplayOrder = 0
                    _currentStatus.IsCancelledStatus = False
                    _currentStatus.IsClientMenuItem = True
                    _currentStatus.IsCompleteStatus = False
                    _currentStatus.IsInProgressStatus = True
                    _currentStatus.IsNewStatus = False
                    _currentStatus.IsReasonRequired = False
                    _currentStatus.ModifiedDateTime = DateTime.UtcNow
                    _currentStatus.CreatedDateTime = DateTime.UtcNow
                End If
                Me.RequestStatusColourPanel.BackColor = If(_currentStatus.Colour = -1, Color.White, Color.FromArgb(_currentStatus.Colour))
                Me.RequestStatusColourPanel.ForeColor = Me.RequestStatusColourPanel.BackColor ' If(_currentStatus.Colour = -1, Color.White, Color.FromArgb(_currentStatus.Colour))
                Me.RequestStatusBackColourPanel.BackColor = If(_currentStatus.BackColour = -1, Color.White, Color.FromArgb(_currentStatus.BackColour))
                Me.RequestStatusBackColourPanel.ForeColor = Me.RequestStatusBackColourPanel.BackColor
                Me.RequestStatusColourPanel.Refresh()
                Me.RequestStatusBackColourPanel.Refresh()
            Else
                _currentStatus = Nothing
            End If
        End If
    End Sub

    Private Sub ColourPanel_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RequestStatusColourPanel.MouseDoubleClick, RequestStatusBackColourPanel.MouseDoubleClick
        Dim source As Panel = TryCast(sender, Panel)
        If source IsNot Nothing Then
            Me.ColorDialog.Color = source.BackColor
            Me.ColorDialog.ShowDialog()
            source.BackColor = Me.ColorDialog.Color
            If sender Is Me.RequestStatusBackColourPanel Then
                _currentStatus.BackColour = source.BackColor.ToArgb
            Else
                _currentStatus.Colour = source.BackColor.ToArgb
            End If
            Me.Refresh()
        End If
    End Sub

    Private Sub RequestStatusDataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles RequestStatusDataGridView.CellFormatting
        If TypeOf RequestStatusBindingSource.Current Is System.Data.DataRowView Then
            If RequestStatusBindingSource.List.Count > 0 AndAlso e.RowIndex < RequestStatusBindingSource.List.Count Then
                Dim drv As System.Data.DataRowView = CType(RequestStatusBindingSource.Item(e.RowIndex), System.Data.DataRowView)

                If TypeOf drv.Row Is activiserDataSet.RequestStatusRow Then
                    Dim rs As activiserDataSet.RequestStatusRow = CType(drv.Row, activiserDataSet.RequestStatusRow)
                    e.CellStyle.ForeColor = Color.FromArgb(rs.Colour)
                    e.CellStyle.BackColor = If(rs.BackColour = -1, Color.White, Color.FromArgb(rs.BackColour))
                End If
            End If
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpMenuAboutButton.Click
        SplashScreen.ShowDialog()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileSaveMenuItem.Click
        SaveData()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileExitMenuItem.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ClientSiteStatusBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientSiteStatusBindingSource.PositionChanged
        Dim drv As DataRowView = TryCast(ClientSiteStatusBindingSource.Current, DataRowView)
        If drv IsNot Nothing Then
            Dim cssr As activiserDataSet.ClientSiteStatusRow = TryCast(drv.Row, activiserDataSet.ClientSiteStatusRow)
            If cssr IsNot Nothing Then
                If cssr.IsNull("CreatedDateTime") Then cssr.CreatedDateTime = DateTime.UtcNow
                If cssr.IsNull("ModifiedDateTime") Then cssr.ModifiedDateTime = DateTime.UtcNow
                cssr.IsActive = True
            End If
        End If
    End Sub

    Private Sub cancelDialogButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbortButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoneButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SyncOnTimeColour_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SyncOnTimeColour.DoubleClick, SyncMissedNoneLabel.DoubleClick
        Me.ColorDialog.Color = Me.SyncOnTimeColour.BackColor
        Me.ColorDialog.ShowDialog()
        Me.SyncOnTimeColour.BackColor = Me.ColorDialog.Color
        My.Settings.SyncOnTimeColour = Me.SyncOnTimeColour.BackColor
    End Sub

    Private Sub SyncMissedOneColour_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SyncMissedOneLabel.DoubleClick, SyncMissedOneColour.DoubleClick
        Me.ColorDialog.Color = Me.SyncMissedOneColour.BackColor
        Me.ColorDialog.ShowDialog()
        Me.SyncMissedOneColour.BackColor = Me.ColorDialog.Color
        My.Settings.SyncMissedOneColour = Me.SyncMissedOneColour.BackColor
    End Sub

    Private Sub SyncMissedTwoColour_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SyncMissedTwoColour.DoubleClick, SyncMissedMoreThanOneLabel.DoubleClick
        Me.ColorDialog.Color = Me.SyncMissedTwoColour.BackColor
        Me.ColorDialog.ShowDialog()
        Me.SyncMissedTwoColour.BackColor = Me.ColorDialog.Color
        My.Settings.SyncMissedMoreColour = Me.SyncMissedTwoColour.BackColor
    End Sub

End Class