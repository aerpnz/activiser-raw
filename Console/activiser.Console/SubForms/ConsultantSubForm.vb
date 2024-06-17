Imports System.ComponentModel
Imports activiser.Library.activiserWebService

Public Class ConsultantSubForm
    Const MODULENAME As String = "ConsultantSubForm"
    Private _currentConsultant As activiserDataSet.ConsultantRow
    Private WithEvents _consoleData As New ConsoleData(MODULENAME)

    Private _giNotes As String
    Private _giConsultantName As String
    Private _gi As activiser.Library.Gps.GpsPosition


    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property ConsoleData() As ConsoleData
        Get
            Return _consoleData
        End Get
        Set(ByVal value As ConsoleData)
            _consoleData = value
        End Set
    End Property

    Public ReadOnly Property CurrentConsultant() As activiserDataSet.ConsultantRow
        Get
            Return _currentConsultant
        End Get
    End Property
    Private _accessLevel As Console.AccessLevels
    Public Property AccessLevel() As AccessLevels
        Get
            Return _accessLevel
        End Get
        Set(ByVal value As AccessLevels)
            _accessLevel = value
            If Not ((_accessLevel And Console.AccessLevels.Management) = Console.AccessLevels.Management) Then
                Me.ManagementCheckBox.Enabled = False
                Me.AdministrationCheckBox.Enabled = False
                Me.IsActiviserUserCheckBox.Enabled = False
                Me.DomainLogonTextBox.ReadOnly = True
            Else
                Me.ManagementCheckBox.Enabled = True
                Me.AdministrationCheckBox.Enabled = True
                Me.IsActiviserUserCheckBox.Enabled = True
                Me.DomainLogonTextBox.ReadOnly = False
            End If
        End Set
    End Property

    'Friend CellStyleTwoSyncsMissed As DataGridViewCellStyle
    'Friend CellStyleOneSyncMissed As DataGridViewCellStyle
    'Friend CellStyleSyncOK As DataGridViewCellStyle

    Private Sub Save()
        If Me.CurrentConsultant Is Nothing Then Return
        Me.Validate()
        Me.CurrentConsultant.ModifiedDateTime = DateTime.UtcNow
        Me.CurrentConsultant.EndEdit()

        Console.ConsoleData.StartRefresh()
    End Sub

    Private Sub Undo()
        If Me.CurrentConsultant Is Nothing Then Return
        Me.CurrentConsultant.RejectChanges()

    End Sub

    'this is so that the initialisation can be driven manually from the main form load event, after the initial data set has loaded.
    Friend Sub InitialiseForm()
        Try
            Terminology.LoadLabels(Me)
            Terminology.LoadToolTips(Me, ToolTipProvider)

            GpsSupport.LoadTemplate(GpsBrowser)

            Me.CoreDataSet = Console.ConsoleData.CoreDataSet
            Me.ConsultantBindingSource.DataSource = Console.ConsoleData.CoreDataSet

            'HACK: Don't know why this is necessary
            'Me.ConsultantUIDColumn.Visible = False

            'ActiviserUsersOnly = True
        Catch ex As Exception
            'MsgBox(ex.StackTrace)
            activiser.Library.DisplayException.Show(ex, My.Resources.ConsultantSubFormFailedToLoad, Library.Icons.People)
        End Try
    End Sub

    Private Delegate Function ControlFocusedDelegate(ByVal target As Control) As Boolean

    Private Function ControlFocused(ByVal target As Control) As Boolean
        If target.InvokeRequired Then
            Dim d As New ControlFocusedDelegate(AddressOf ControlFocused)
            Return CBool(Me.Invoke(d, target))
        Else
            Return target.Focused
        End If
    End Function

    'Private _dirty As Boolean
    Private Sub ConsultantBindingSource_BindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles ConsultantBindingSource.BindingComplete
        If Not e.BindingCompleteState = BindingCompleteState.Success Then Exit Sub

        If e.BindingCompleteContext = BindingCompleteContext.DataSourceUpdate Then
        Else
            If ControlFocused(e.Binding.Control) Then
                '_dirty = True
            End If
        End If
    End Sub

    Private Sub ConsultantBindingSource_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsultantBindingSource.CurrentChanged
        SetCurrentConsultant()
    End Sub

    Private Sub ConsultantBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles ConsultantBindingSource.ListChanged
        SetCurrentConsultant()
    End Sub

    'Private _refreshRequired As Boolean
    Private Sub _consoleData_Refreshed(ByVal sender As Object, ByVal e As ConsoleDataRefreshEventArgs) Handles _consoleData.Refreshed
        'TraceInfo("ConsultantSubForm received data refresh")
        Me.ResumeBindingSources()
    End Sub

    Private Sub _consoleData_Refreshing(ByVal sender As Object, ByVal e As System.EventArgs) Handles _consoleData.Refreshing
        Me.SuspendBindingSources()
        'Me.PollTimer.Enabled = False
    End Sub

    Private Delegate Sub SuspendBindingSourcesCallback()
    Private Sub SuspendBindingSources()
        ' do nothing (template method)
        'If Me.InvokeRequired Then
        '    Dim d As New SuspendBindingSourcesCallback(AddressOf SuspendBindingSources)
        '    Me.BeginInvoke(d)
        'Else
        '    'Me._suspended = True
        '    'Me.PollTimer.Enabled = False
        'End If
    End Sub

    Private Delegate Sub ResumeBindingSourcesCallback()
    Private Sub ResumeBindingSources()
        If Me.InvokeRequired Then ' OrElse Me.ConsultantDataGridView.InvokeRequired Then
            Dim d As New ResumeBindingSourcesCallback(AddressOf ResumeBindingSources)
            Me.BeginInvoke(d)
        Else
            Dim cr As activiserDataSet.ConsultantRow = _currentConsultant
            Me.ConsultantBindingSource.ResetBindings(False)
            'Me.ConsultantDataGridView.Refresh()
            If cr IsNot Nothing Then
                Dim index As Integer = Me.ConsultantBindingSource.IndexOf(cr)
                If index <> -1 Then
                    Me.ConsultantBindingSource.Position = index
                Else
                    Me.ConsultantBindingSource.MoveFirst()
                End If
            End If
            'HACK: Don't know why this is necessary
            'Me.ConsultantUIDColumn.Visible = False
        End If
    End Sub


    Private Sub SetCurrentConsultant()
        Dim drv As System.Data.DataRowView = TryCast(ConsultantBindingSource.Current, System.Data.DataRowView)
        If drv IsNot Nothing AndAlso drv.Row IsNot Nothing Then
            Dim cr As activiserDataSet.ConsultantRow = TryCast(drv.Row, activiserDataSet.ConsultantRow)
            If cr Is _currentConsultant Then Return

            _currentConsultant = cr

            If cr IsNot Nothing Then
                SetGpsBrowserContent()

                If (ConsoleUser.Management OrElse cr.ConsultantUID = ConsoleUser.ConsultantUID) Then
                    SetControlEnabled(Me.ChangePasswordButton, True)
                Else
                    SetControlEnabled(Me.ChangePasswordButton, False)
                End If
            Else
                Me.GpsBrowser.Url = New Uri("about:blank")
                SetControlEnabled(Me.ConsultantInfoPanel, False)
            End If
        End If
    End Sub

    Private Sub ConsultantBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsultantBindingSource.PositionChanged
        SetCurrentConsultant()
    End Sub


#Region "Thread Delegates"
    Private Delegate Sub SetControlTextCallBack(ByVal Control As Control, ByVal Value As String)
    Private Sub SetControlText(ByVal Control As Control, ByVal Value As String)
        If Control.InvokeRequired Then
            Dim d As New SetControlTextCallBack(AddressOf SetControlText)
            Me.BeginInvoke(d, New Object() {Control, Value})
        Else
            Control.Text = Value
        End If
    End Sub

    Private Delegate Sub ChangeCheckBoxCheckStateDelegate(ByVal Control As CheckBox, ByVal Value As CheckState)
    Private Sub ChangeCheckBoxCheckState(ByVal Control As CheckBox, ByVal Value As CheckState)
        If Control.InvokeRequired Then
            Dim d As New ChangeCheckBoxCheckStateDelegate(AddressOf ChangeCheckBoxCheckState)
            Me.BeginInvoke(d, New Object() {Control, Value})
        Else
            Control.CheckState = Value
        End If
    End Sub

    Private Delegate Sub SetControlEnabledDelegate(ByVal victim As Control, ByVal enabled As Boolean)
    Private Sub SetControlEnabled(ByVal victim As Control, ByVal enabled As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetControlEnabledDelegate(AddressOf SetControlEnabled)
            Me.BeginInvoke(d, victim, enabled)
        Else
            'victim.Visible = enabled
            victim.Enabled = enabled
        End If
    End Sub
#End Region

    Private Sub ConsultantSubForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim template As String = GpsSupport.GetDeviceTrackingHtml()

        If Not String.IsNullOrEmpty(template) Then
            Me.GpsBrowser.DocumentText = template
        End If
    End Sub

    Private Sub SubForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            ResumeBindingSources()
        Else
            SuspendBindingSources()
        End If
    End Sub

    'Private Sub RefreshGridSelection()
    '    'If Me._settingFilter Then Return
    '    If Me.ConsultantBindingSource.Position = -1 Then Return

    '    Dim drv As DataRowView = TryCast(Me.ConsultantBindingSource.Current, DataRowView)
    '    Try
    '        If drv IsNot Nothing Then
    '            Me.ConsultantDataGridView.ClearSelection()
    '            'If Me.JobDataGridView.SelectedRows.Count <> 1 OrElse (Me.JobDataGridView.SelectedRows(0).DataBoundItem IsNot drv) Then
    '            Dim dgrv As DataGridViewRow
    '            For i As Integer = 0 To Me.ConsultantDataGridView.Rows.Count - 1
    '                dgrv = Me.ConsultantDataGridView.Rows(i)
    '                If dgrv.DataBoundItem Is drv Then
    '                    Dim newCell As DataGridViewCell = Me.ConsultantDataGridView.Item(1, i)
    '                    If Not newCell.Visible Then
    '                        If i > 3 Then
    '                            Me.ConsultantDataGridView.FirstDisplayedScrollingRowIndex = i - 3
    '                        Else
    '                            Me.ConsultantDataGridView.FirstDisplayedScrollingRowIndex = 0
    '                        End If

    '                        If Not newCell.Visible Then
    '                            Me.ConsultantDataGridView.FirstDisplayedCell = newCell
    '                        End If
    '                    End If

    '                    If newCell.RowIndex <> i Then ' IsNot Me.JobDataGridView.CurrentCell Then
    '                        Me.ConsultantDataGridView.CurrentCell = newCell
    '                        Me.ConsultantDataGridView.CurrentCell = Nothing ' reset to force selection of whole row
    '                    End If

    '                    dgrv.Selected = True
    '                    Exit For
    '                End If
    '            Next
    '            'End If
    '        End If
    '    Catch ex As Exception
    '        Me.ConsultantDataGridView.CurrentCell = Nothing
    '    End Try
    '    'Me.JobDataGridView.CurrentCell = Me.JobDataGridView.Item(-1, -1)

    'End Sub

    Private Sub ChangePasswordButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me._currentConsultant IsNot Nothing Then
            Using pf As New ChangePasswordForm
                If pf.ShowDialog() = DialogResult.OK Then
                    Dim newPassword As String = Encrypt(Me._currentConsultant.ConsultantUID, GetHash(pf.NewPassword.Text))
                    Try
                        Dim target As New Utility.UserDataTable
                        If ConsoleData.WebService.ConsoleChangeUserPassPhrase(deviceId, target, pf.OldPassword.Text, newPassword) Then
                        Else

                        End If
                    Catch ex As Exception
                        TraceError(ex)
                    End Try
                End If
            End Using
        End If
    End Sub



#Region "Filters"
    Public Property Filter() As String
        Get
            Return Me.ConsultantBindingSource.Filter
        End Get
        Set(ByVal value As String)
            If Me.ConsultantBindingSource.Filter = value Then Return
            Me.SuspendLayout()
            Try
                Me.ConsultantBindingSource.EndEdit()
                Me.ConsultantBindingSource.Filter = value
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
            Finally
                Me.ResumeLayout()
            End Try
        End Set
    End Property

    Public Function SetFilter(ByVal filter As String) As Boolean
        Me.Filter = filter
        Return Me.Filter = filter
    End Function

#End Region


    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        If Not Me._currentConsultant Is Nothing Then
            Save()
        End If
    End Sub

    Private Sub UndoButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UndoButton.Click
        Me.Undo()
    End Sub

    Private Sub UndoButton_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles UndoButton.MouseEnter
        Me.UndoButton.Enabled = Me.CurrentConsultant IsNot Nothing AndAlso DataRowHasChanges(Me.CurrentConsultant)
    End Sub

    Private Sub SaveButton_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.MouseEnter
        Me.SaveButton.Enabled = Me.CurrentConsultant IsNot Nothing AndAlso DataRowHasChanges(Me.CurrentConsultant)
    End Sub

    Private Sub UndoButton_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles UndoButton.MouseLeave
        Me.UndoButton.Enabled = True
    End Sub

    Private Sub SaveButton_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.MouseLeave
        Me.SaveButton.Enabled = True
    End Sub

#Region "GPS"
    Private Sub SetGpsBrowserContent()

        GpsSupport.ClearMap(Me.GpsBrowser)

        If _currentConsultant Is Nothing OrElse _currentConsultant.IsTrackingInfoNull OrElse String.IsNullOrEmpty(_currentConsultant.TrackingInfo) Then
            ' Me.GpsBrowser.DocumentText = String.Empty
            Return
        End If

        _gi = Nothing
        If Not (Library.Gps.GpsPosition.TryParse(_currentConsultant.TrackingInfo, _gi) AndAlso _gi.LatitudeValid AndAlso _gi.LongitudeValid) Then
            Debug.WriteLine("Invalid")
            GpsSupport.ClearMap(Me.GpsBrowser)
            Return
        End If

        _giNotes = String.Format("{0}<br>{1}", _currentConsultant.Username.Trim, _gi.ToString("L"))

        _giConsultantName = _currentConsultant.Name

        If Not _browserLoaded Then
            _loadMapWhenBrowserLoaded = True
            Return
        End If
        GpsSupport.DisplayMap(Me.GpsBrowser, _gi.Latitude, _gi.Longitude, _giConsultantName, _giNotes)
    End Sub

    Private _loadMapWhenBrowserLoaded As Boolean
    Private _browserLoaded As Boolean
    Private Sub GpsBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles GpsBrowser.DocumentCompleted
        Debug.Print("GpsBrowser_DocumentCompleted: " & GpsBrowser.DocumentText)
        _browserLoaded = True
        If _loadMapWhenBrowserLoaded Then
            GpsSupport.DisplayMap(Me.GpsBrowser, _gi.Latitude, _gi.Longitude, _giConsultantName, _giNotes)
            _loadMapWhenBrowserLoaded = False
        End If
    End Sub

    Private Sub GpsBrowser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles GpsBrowser.Navigated
        Debug.Print("GpsBrowser_Navigated: " & GpsBrowser.DocumentText)
    End Sub
#End Region


End Class
