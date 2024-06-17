Imports activiser.Library
Imports System.ComponentModel

Public Class EventLogSubForm
    'Const MODULENAME As String = "EventLogSubForm"
    'Private _currentEntry As WebService.ClientDataSet.EventLogRow

    'Private WithEvents _consoleData As New ConsoleData(MODULENAME)
    '<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    'Public Property ConsoleData() As ConsoleData
    '    Get
    '        Return _consoleData
    '    End Get
    '    Set(ByVal value As ConsoleData)
    '        _consoleData = value
    '    End Set
    'End Property

    'Private haveInitialised As Boolean = False

    'Public Sub New()
    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    Me.EventLogBindingSource.DataSource = Console.ConsoleData.CoreDataSet
    '    Me.EventLogBindingSource.DataMember = Console.ConsoleData.CoreDataSet.EventLog.TableName
    '    If ConsoleUser IsNot Nothing Then
    '        haveInitialised = False
    '        Utilities.RestoreColumnWidths(My.Settings.EventLogColumnWidths, Me.EventLogDataGridView)
    '        Utilities.RestoreColumnOrder(My.Settings.EventLogColumnOrder, Me.EventLogDataGridView)
    '        haveInitialised = True
    '    End If
    'End Sub

    'Private Sub _consoleData_Refreshed(ByVal sender As Object, ByVal e As ConsoleDataRefreshEventArgs) Handles _consoleData.Refreshed
    '    RefreshMe()
    'End Sub

    'Private Delegate Sub RefreshMeDelegate()
    'Private Sub RefreshMe()
    '    If Me.InvokeRequired Then
    '        Dim d As New RefreshMeDelegate(AddressOf RefreshMe)
    '        Me.Invoke(d)
    '    Else
    '        If Me.Visible Then
    '            Me.EventLogBindingSource.ResetBindings(False)
    '        End If
    '    End If
    'End Sub

    'Private Sub EventLogDataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles EventLogDataGridView.CellFormatting
    '    If sender Is Me.EventLogDataGridView Then
    '        Dim jr As WebService.ClientDataSet.EventLogRow
    '        Try
    '            If Me.EventLogBindingSource.Count > 0 AndAlso e.RowIndex < Me.EventLogBindingSource.Count Then
    '                Dim drv As DataRowView = TryCast(Me.EventLogBindingSource.Item(e.RowIndex), Data.DataRowView)
    '                If drv IsNot Nothing Then
    '                    jr = TryCast(drv.Row, WebService.ClientDataSet.EventLogRow)
    '                    If jr IsNot Nothing Then
    '                        Dim gvc As DataGridViewColumn = Me.EventLogDataGridView.Columns(e.ColumnIndex)
    '                        If gvc Is Me.EventDescriptionColumn Then
    '                            Dim requestNumber, consultantName, siteName As String

    '                            If Not jr.IsRequestNumberNull Then : requestNumber = jr.RequestNumber : Else : requestNumber = My.Resources.Unassigned : End If
    '                            If Not jr.IsConsultantNameNull Then : consultantName = jr.ConsultantName : Else : consultantName = My.Resources.Unassigned : End If
    '                            If Not jr.IsSiteNameNull Then : siteName = jr.SiteName : Else : siteName = My.Resources.Unassigned : End If

    '                            e.Value = String.Format(jr.EventDescription, requestNumber, consultantName, jr.UTCEventTime.ToLocalTime.ToString("G"), siteName)
    '                            e.FormattingApplied = True
    '                        ElseIf gvc Is Me.JobDateColumn Then
    '                            If jr.JobRow IsNot Nothing Then
    '                                If jr.JobRow.IsStartTimeNull Then
    '                                    e.Value = My.Resources.NotAvailable
    '                                Else
    '                                    'Dim jdst As DateTime = New Date(jr.JobDate.Year, jr.JobDate.Month, jr.JobDate.Day, jr.StartTime.Hour, jr.StartTime.Minute, 0, DateTimeKind.Utc).ToLocalTime
    '                                    e.Value = DateTime.SpecifyKind(jr.JobRow.StartTime, DateTimeKind.Utc).ToLocalTime.ToString("G")
    '                                End If
    '                            Else
    '                                e.Value = String.Empty
    '                            End If

    '                        ElseIf gvc Is Me.EventTimeColumn Then
    '                            e.Value = DateTime.SpecifyKind(jr.UTCEventTime, DateTimeKind.Utc).ToLocalTime.ToString("G")
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Catch ex As Exception
    '            Debug.WriteLine(ex.ToString)
    '        End Try
    '    End If
    'End Sub

    'Private Sub EventLogDataGridView_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles EventLogDataGridView.ColumnHeaderMouseClick
    '    If sender Is Me.EventLogDataGridView Then
    '        Dim gvc As DataGridViewColumn = Me.EventLogDataGridView.Columns(e.ColumnIndex)
    '        If Me.EventLogBindingSource.Count > 0 Then
    '            If gvc Is Me.JobDateColumn Then
    '                If Me.EventLogBindingSource.Sort Is Nothing OrElse Me.EventLogBindingSource.Sort.IndexOf("StartTime DESC") <> -1 Then
    '                    Me.EventLogBindingSource.Sort = "StartTime, FinishTime"
    '                    gvc.HeaderCell.SortGlyphDirection = SortOrder.Ascending
    '                Else
    '                    Me.EventLogBindingSource.Sort = "StartTime DESC, FinishTime DESC"
    '                    gvc.HeaderCell.SortGlyphDirection = SortOrder.Descending
    '                End If
    '            ElseIf gvc Is Me.EventDescriptionColumn Then
    '                Me.JobDateColumn.HeaderCell.SortGlyphDirection = SortOrder.None
    '                If Me.EventLogBindingSource.Sort Is Nothing OrElse Me.EventLogBindingSource.Sort.IndexOf("RequestNumber DESC") <> -1 Then
    '                    Me.EventLogBindingSource.Sort = "EventDescription,RequestNumber,ConsultantName,UTCEventTime,SiteName"
    '                    gvc.HeaderCell.SortGlyphDirection = SortOrder.Ascending
    '                Else
    '                    Me.EventLogBindingSource.Sort = "EventDescription DESC,RequestNumber DESC,ConsultantName DESC,UTCEventTime DESC,SiteName DESC"
    '                    gvc.HeaderCell.SortGlyphDirection = SortOrder.Descending
    '                End If
    '            Else
    '                Me.JobDateColumn.HeaderCell.SortGlyphDirection = SortOrder.None
    '                Me.EventDescriptionColumn.HeaderCell.SortGlyphDirection = SortOrder.None
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub EventLogDataGridView_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles EventLogDataGridView.ColumnDisplayIndexChanged
    '    If haveInitialised Then
    '        If My.Settings.EventLogColumnOrder Is Nothing Then
    '            My.Settings.EventLogColumnOrder = New Collections.Specialized.StringCollection
    '        End If
    '        SaveColumnOrderChange(My.Settings.EventLogColumnOrder, e.Column.Name, e.Column.DisplayIndex)
    '    End If
    'End Sub

    'Private Sub EventLogDataGridView_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles EventLogDataGridView.ColumnWidthChanged
    '    If haveInitialised Then
    '        If My.Settings.EventLogColumnWidths Is Nothing Then
    '            My.Settings.EventLogColumnWidths = New Collections.Specialized.StringCollection
    '        End If
    '        SaveColumnWidthChange(My.Settings.EventLogColumnWidths, e.Column.Name, e.Column.Width)
    '    End If
    'End Sub

    'Private baseFilter As String = ""
    'Private StartTimeFilter As String = ""
    'Private FinishTimeFilter As String = ""
    'Private ConsultantFilter As String = ""
    'Private ClientFilter As String = ""
    'Private RequestFilter As String = ""

    'Private Sub BuildFilter()
    '    Dim filters As New Collections.Specialized.StringCollection

    '    If Not String.IsNullOrEmpty(baseFilter) Then filters.Add(baseFilter)
    '    If Not String.IsNullOrEmpty(StartTimeFilter) Then filters.Add(StartTimeFilter)
    '    If Not String.IsNullOrEmpty(FinishTimeFilter) Then filters.Add(FinishTimeFilter)
    '    If Not String.IsNullOrEmpty(ConsultantFilter) Then filters.Add(ConsultantFilter)
    '    If Not String.IsNullOrEmpty(ClientFilter) Then filters.Add(ClientFilter)
    '    If Not String.IsNullOrEmpty(RequestFilter) Then filters.Add(RequestFilter)

    '    Dim aFilters(filters.Count - 1) As String
    '    filters.CopyTo(aFilters, 0)
    '    Dim newFilter As String = String.Join(" AND ", aFilters)
    '    Me.EventLogBindingSource.Filter = newFilter
    'End Sub

    'Public Property Filter() As String
    '    Get
    '        Return Me.EventLogBindingSource.Filter
    '    End Get
    '    Set(ByVal value As String)
    '        Me.baseFilter = value
    '        BuildFilter()
    '    End Set
    'End Property

    'Private Sub FilterMenuDateToday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuDateToday.Click
    '    Dim utcToday As Date = Today.ToUniversalTime
    '    If FilterMenuDateToday.Checked Then
    '        Me.FilterMenuAllDates.Checked = False
    '        Me.FilterMenuYesterday.Checked = False
    '        Me.FilterMenuPastWeek.Checked = False
    '        Me.FilterMenuPastMonth.Checked = False
    '        StartTimeFilter = String.Format("{0} >= '{1:s}'", EventLog.UTCEventTimeColumn.ColumnName, utcToday)
    '    Else
    '        StartTimeFilter = ""
    '        FinishTimeFilter = ""
    '    End If
    '    BuildFilter()
    'End Sub

    'Private Sub RemoveDateFilter()
    '    Me.FilterMenuAllDates.Checked = True
    '    Me.FilterMenuYesterday.Checked = False
    '    Me.FilterMenuDateToday.Checked = False
    '    Me.FilterMenuPastWeek.Checked = False
    '    Me.FilterMenuPastMonth.Checked = False
    '    StartTimeFilter = ""
    '    FinishTimeFilter = ""
    '    BuildFilter()
    'End Sub
    'Private Sub FilterMenuAllDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuAllDates.Click
    '    RemoveDateFilter()
    'End Sub

    'Private Sub FilterMenuYesterday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilterMenuYesterday.Click
    '    Dim utcToday As Date = Today.ToUniversalTime
    '    If FilterMenuYesterday.Checked Then
    '        Me.FilterMenuAllDates.Checked = False
    '        Me.FilterMenuDateToday.Checked = False
    '        Me.FilterMenuPastWeek.Checked = False
    '        Me.FilterMenuPastMonth.Checked = False
    '        StartTimeFilter = String.Format("{0} >= '{1:u}'", Console.ConsoleData.CoreDataSet.EventLog.UTCEventTimeColumn.ColumnName, utcToday.Subtract(TimeSpan.FromDays(1)).ToUniversalTime)
    '        FinishTimeFilter = String.Format("{0} < '{1:u}'", Console.ConsoleData.CoreDataSet.EventLog.UTCEventTimeColumn.ColumnName, utcToday)
    '        BuildFilter()
    '    Else
    '        RemoveDateFilter()
    '    End If
    'End Sub

    'Private Sub FilterMenuPastWeek_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilterMenuPastWeek.Click
    '    Dim utcToday As Date = Today.ToUniversalTime
    '    If FilterMenuPastWeek.Checked Then
    '        Me.FilterMenuAllDates.Checked = False
    '        Me.FilterMenuDateToday.Checked = False
    '        Me.FilterMenuYesterday.Checked = False
    '        Me.FilterMenuPastMonth.Checked = False
    '        StartTimeFilter = String.Format("{0} >= '{1:u}'", Console.ConsoleData.CoreDataSet.EventLog.UTCEventTimeColumn.ColumnName, utcToday.Subtract(TimeSpan.FromDays(7)))
    '        FinishTimeFilter = ""
    '        BuildFilter()
    '    Else
    '        RemoveDateFilter()
    '    End If
    'End Sub

    'Private Sub FilterMenuPastMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilterMenuPastMonth.Click
    '    Dim utcToday As Date = Today.ToUniversalTime
    '    If FilterMenuPastMonth.Checked Then
    '        Me.FilterMenuAllDates.Checked = False
    '        Me.FilterMenuDateToday.Checked = False
    '        Me.FilterMenuYesterday.Checked = False
    '        Me.FilterMenuPastWeek.Checked = False
    '        Dim days As Integer
    '        If Date.Today.Month > 1 Then
    '            days = DateTime.DaysInMonth(Date.Today.Year, Date.Today.Month - 1)
    '        Else
    '            days = DateTime.DaysInMonth(Date.Today.Year - 1, 12)
    '        End If
    '        StartTimeFilter = String.Format("{0} >= '{1:u}'", Console.ConsoleData.CoreDataSet.EventLog.UTCEventTimeColumn.ColumnName, utcToday.Subtract(TimeSpan.FromDays(days)))
    '        FinishTimeFilter = ""
    '        BuildFilter()
    '    Else
    '        RemoveDateFilter()
    '    End If
    'End Sub

    'Private Sub FilterMenuConsultantInclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuConsultantInclude.Click
    '    Const FilterTemplate As String = "ConsultantUID = '{0}'"
    '    ConsultantFilter = String.Empty
    '    If _currentEntry IsNot Nothing Then
    '        If FilterMenuConsultantInclude.Checked Then
    '            ConsultantFilter = String.Format(FilterTemplate, _currentEntry.ConsultantUID)
    '            Me.FilterMenuConsultantExclude.Checked = False
    '            Me.FilterMenuConsultantNone.Checked = False
    '        Else
    '            Me.FilterMenuConsultantNone.Checked = True
    '        End If
    '    End If
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuConsultantExclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuConsultantExclude.Click
    '    Const FilterTemplate As String = "ConsultantUID <> '{0}'"
    '    ConsultantFilter = String.Empty
    '    If _currentEntry IsNot Nothing Then
    '        If FilterMenuConsultantInclude.Checked Then
    '            ConsultantFilter = String.Format(FilterTemplate, _currentEntry.ConsultantUID)
    '            Me.FilterMenuConsultantInclude.Checked = False
    '            Me.FilterMenuConsultantNone.Checked = False
    '        Else
    '            Me.FilterMenuConsultantNone.Checked = True
    '        End If
    '    End If
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuConsultantNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuConsultantNone.Click
    '    ConsultantFilter = String.Empty
    '    Me.FilterMenuConsultantInclude.Checked = True
    '    Me.FilterMenuConsultantExclude.Checked = False
    '    Me.FilterMenuConsultantInclude.Checked = False
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuClientInclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuClientInclude.Click
    '    Const FilterTemplate As String = "ClientSiteUID = '{0}'"
    '    ClientFilter = String.Empty
    '    Me.FilterMenuClientExclude.Checked = False
    '    If _currentEntry IsNot Nothing Then
    '        If Me.FilterMenuClientInclude.Checked Then
    '            ClientFilter = String.Format(FilterTemplate, _currentEntry.ClientSiteUID)
    '            Me.FilterMenuClientNone.Checked = False
    '        Else
    '            Me.FilterMenuClientNone.Checked = True
    '        End If
    '    End If
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuClientExclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuClientExclude.Click
    '    Const FilterTemplate As String = "ClientSiteUID <> '{0}'"
    '    ClientFilter = String.Empty
    '    Me.FilterMenuClientInclude.Checked = False
    '    If _currentEntry IsNot Nothing Then
    '        If Me.FilterMenuClientExclude.Checked Then
    '            ClientFilter = String.Format(FilterTemplate, _currentEntry.ClientSiteUID)
    '            Me.FilterMenuClientNone.Checked = False
    '        Else
    '            Me.FilterMenuClientNone.Checked = True
    '        End If
    '    End If
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuClientNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuClientNone.Click
    '    ClientFilter = ""
    '    Me.FilterMenuClientExclude.Checked = False
    '    Me.FilterMenuClientInclude.Checked = False
    '    Me.FilterMenuClientNone.Checked = True
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuRequestInclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuRequestInclude.Click
    '    Const FilterTemplate As String = "RequestUID = '{0}'"
    '    RequestFilter = String.Empty
    '    Me.FilterMenuRequestExclude.Checked = False
    '    If _currentEntry IsNot Nothing Then
    '        If Me.FilterMenuRequestInclude.Checked Then
    '            Me.FilterMenuRequestNone.Checked = False
    '            RequestFilter = String.Format(FilterTemplate, _currentEntry.RequestUID)
    '        Else
    '            Me.FilterMenuRequestNone.Checked = True
    '        End If
    '    End If
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuRequestExclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuRequestExclude.Click
    '    Const FilterTemplate As String = "RequestUID <> '{0}'"
    '    RequestFilter = String.Empty
    '    Me.FilterMenuRequestInclude.Checked = False
    '    If _currentEntry IsNot Nothing Then
    '        If Me.FilterMenuRequestInclude.Checked Then
    '            Me.FilterMenuRequestExclude.Checked = False
    '            RequestFilter = String.Format(FilterTemplate, _currentEntry.RequestUID)
    '        Else
    '            Me.FilterMenuRequestNone.Checked = True
    '        End If
    '    End If
    '    BuildFilter()
    'End Sub

    'Private Sub FilterMenuRequestNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterMenuRequestNone.Click
    '    Me.FilterMenuRequestExclude.Checked = False
    '    Me.FilterMenuRequestInclude.Checked = False
    '    Me.FilterMenuRequestNone.Checked = True
    '    RequestFilter = ""
    '    BuildFilter()
    'End Sub

    'Private Sub EventLogBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EventLogBindingSource.PositionChanged
    '    If sender Is Me.EventLogBindingSource Then
    '        If Me.EventLogBindingSource.Position = -1 Then
    '            _currentEntry = Nothing
    '            Return
    '        End If
    '        If TypeOf Me.EventLogBindingSource.Current Is DataRowView Then
    '            Dim drv As DataRowView = CType(Me.EventLogBindingSource.Current, DataRowView)
    '            _currentEntry = TryCast(drv.Row, WebService.ClientDataSet.EventLogRow)
    '        End If
    '    End If
    'End Sub

    'Private Sub EventLogSubForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    TraceInfo("Loading")
    '    Terminology.LoadLabels(Me)
    '    Terminology.LoadToolTips(Me, ToolTipProvider)
    'End Sub

    'Private Sub EventLogSubForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
    '    RefreshMe()
    'End Sub
End Class
