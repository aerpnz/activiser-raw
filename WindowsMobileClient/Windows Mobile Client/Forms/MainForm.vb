Imports Microsoft.WindowsCE.Forms

Imports activiser.Library.Gps
Imports System.Windows.Forms
Imports activiser.Library
Imports activiser.Library.WebService
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.Forms

Public Class MainForm
    Private Const MODULENAME As String = "MainForm"

    Private _statusColors As New Collections.Generic.Dictionary(Of Integer, Color)
    Private _statusBackColors As New Collections.Generic.Dictionary(Of Integer, Color)
    Private _selectedRequest As RequestRow

    Private _RequestListPopulating As Boolean
    Private _showRequestListCheckboxes As Boolean
    Private _showToolBar As Boolean
    Private _showToolBarCaptions As Boolean
    Private _toolBarSize As Integer
    'Private _requestListDateColumnIndex As Integer = 2 ' TODO: parameterise this.
    Private _sortColumnIndex As Integer
    Private Shared _showRequestedStatus As Boolean

    'Private _largeScreen As Boolean

    Private Shared _useLastModifiedTimeForNullActionDates As Boolean
    Private Shared _showTimeInDateColumn As Boolean

    Private statusIdFilter As Integer = -1 'Set to all by default

    Private WithEvents _backgroundLoaderThread As Threading.Thread

    Private Const STR_RequestStatusSortOrder As String = "DisplayOrder, IsNewStatus, IsInProgressStatus, IsCompleteStatus, IsCancelledStatus, RequestStatusID"

    <Flags()> Private Enum statusTypes
        None = 0
        [New] = 1
        WIP = 2
        Complete = 4
        Cancelled = 8
        Open = [New] Or [WIP]
        Closed = Complete Or Cancelled
        All = 15
    End Enum

    Private statusTypeFilter As statusTypes = statusTypes.All

    Private Delegate Sub SetSetSyncColoursDelegate(ByVal foreColour As Color, ByVal backColour As Color)

    Public Sub SetSyncColors(ByVal foreColor As Color, ByVal backColor As Color)
        If Me.InvokeRequired Then
            Dim sbc As New SetSetSyncColoursDelegate(AddressOf SetSyncColors)
            Me.Invoke(sbc, foreColor, backColor)
        Else
            Me.SyncButton.ForeColor = foreColor
            Me.SyncButton.BackColor = backColor
        End If
    End Sub

    Public Property SelectedRequest() As RequestRow
        Get
            Return _selectedRequest
        End Get
        Set(ByVal value As RequestRow)
            _selectedRequest = value
            For Each lvi As ListViewItem In Me.RequestList.Items
                If lvi.Tag Is value Then
                    lvi.Focused = True
                    lvi.Selected = True
                    Me.RequestList.EnsureVisible(lvi.Index)
                End If
            Next
        End Set
    End Property

    Private Shared Function GetColumnIndex(ByVal table As DataTable, ByVal columnName As String) As Integer
        Return table.Columns.IndexOf(columnName)
    End Function

    Public Sub InitializeRequestList()
        If Me.InvokeRequired() Then
            Dim d As New simpleSubDelegate(AddressOf InitializeRequestList)
            Me.Invoke(d)
            Return
        End If
        'Setting up styles
        Try
            _statusColors.Clear()
            _statusBackColors.Clear()
            Me.RequestList.Columns.Clear()

            For Each dr As RequestStatusRow In gClientDataSet.RequestStatus
                Try
                    Dim foreColor As Color = Color.FromArgb(dr.Colour)
                    _statusColors.Add(dr.RequestStatusID, foreColor)
                Catch ex As Exception
                    _statusColors.Add(dr.RequestStatusID, SystemColors.WindowText)
                End Try
                Try
                    Dim backColor As Color = Color.FromArgb(dr.BackColour)
                    _statusBackColors.Add(dr.RequestStatusID, backColor)
                Catch ex As Exception
                    _statusBackColors.Add(dr.RequestStatusID, SystemColors.Window)
                End Try
            Next

            ' why is this here?
            'If Not _cellStyles.ContainsKey(-1) Then _cellStyles.Add(-1, Color.WhiteSmoke)

            Me.RequestList.Columns.Add(New SortableColumnHeader(Terminology.GetString(MODULENAME, RES_RequestListRequestNumberColumn), 55, HorizontalAlignment.Center, gClientDataSet.Request.RequestNumberColumn.ColumnName, ColumnType.String))
            Me.RequestList.Columns.Add(New SortableColumnHeader(Terminology.GetString(MODULENAME, RES_RequestListRequestIDColumn), 0, HorizontalAlignment.Center, gClientDataSet.Request.RequestIDColumn.ColumnName, ColumnType.Number))
            Me.RequestList.Columns.Add(New SortableColumnHeader(Terminology.GetString(MODULENAME, RES_RequestListDateColumn), 66, HorizontalAlignment.Center, gClientDataSet.Request.NextActionDateColumn.ColumnName, ColumnType.Data))
            Me.RequestList.Columns.Add(New SortableColumnHeader(Terminology.GetString(MODULENAME, RES_RequestListClientColumn), 94, HorizontalAlignment.Left, gClientDataSet.Request.ClientSiteNameColumn.ColumnName, ColumnType.String))
            Me.RequestList.Columns.Add(New SortableColumnHeader(Terminology.GetString(MODULENAME, RES_RequestListStatusColumn), 40, HorizontalAlignment.Center, If(_showRequestedStatus, gClientDataSet.Request.ConsultantStatusDescriptionColumn.ColumnName, gClientDataSet.Request.RequestStatusDescriptionColumn.ColumnName), ColumnType.String))
            Me.RequestList.Columns.Add(New SortableColumnHeader(Terminology.GetString(MODULENAME, RES_RequestListJobsColumn), 20, HorizontalAlignment.Center, "Jobs", ColumnType.Number))

            Try
                Dim columnWidths() As String = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormColumnWidthsKey, "").Split(","c)
                If columnWidths.Length > 1 Then
                    For i As Integer = 0 To columnWidths.Length - 2 ' don't need the empty trailing item.
                        If i < Me.RequestList.Columns.Count Then ' ignore extra entries
                            Me.RequestList.Columns(i).Width = CInt(columnWidths(i))
                        End If
                    Next
                ElseIf activiser.MyDpiX > 100 Then
                    For Each c As ColumnHeader In Me.RequestList.Columns
                        c.Width = c.Width * 2
                    Next
                End If
            Catch ex As Exception
                'ignore errors restoring column widths
            End Try

            Me.RequestList.Activation = ItemActivation.OneClick

            Me._sortColumnIndex = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormSortColumnKey, 0)
            Dim header As SortableColumnHeader = CType(RequestList.Columns(Me._sortColumnIndex), SortableColumnHeader)
            header.Ascending = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormSortAscendingKey, True)
        Catch ex As Exception
            LogError(MODULENAME, "InitializeRequestList", ex, True, RES_RequestListInitialisationError)
        End Try
    End Sub

    Private Sub SetCurrentRequest()
        Try
            'Dim strMessage As String = String.Empty
            If RequestList.SelectedIndices.Count = 1 Then ' Note: for compact framework, this will only be 1 or 0.
                Dim listViewItem As ListViewItem = Me.RequestList.Items(Me.RequestList.SelectedIndices.Item(0))
                SelectedRequest = DirectCast(listViewItem.Tag, RequestRow) 'gClientDataSet.Request.FindByRequestUID(CType(listViewItem.Tag, Guid)) 

                '======================================
                'fill information box
                '======================================
                Dim description As String = String.Empty
                Dim request As String = String.Empty
                Dim consultant As String = String.Empty
                Dim status As String = String.Empty
                Dim requestedStatus As String = String.Empty
                Dim datestring As String = String.Empty

                If Not SelectedRequest.IsShortDescriptionNull Then description = SelectedRequest.ShortDescription
                If Not SelectedRequest.IsAssignedToUIDNull Then consultant = SelectedRequest.ConsultantRow.Name
                If Not SelectedRequest.IsNextActionDateNull Then datestring = FormatDate(SelectedRequest.NextActionDate)

                If Not SelectedRequest.IsRequestStatusDescriptionNull Then
                    status = SelectedRequest.RequestStatusDescription
                Else
                    status = Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
                End If

                If Not SelectedRequest.IsConsultantStatusDescriptionNull Then
                    requestedStatus = SelectedRequest.ConsultantStatusDescription
                Else
                    requestedStatus = Terminology.GetString(My.Resources.SharedMessagesKey, RES_None)
                End If

                If String.IsNullOrEmpty(SelectedRequest.RequestNumber) AndAlso SelectedRequest.RequestID <> 0 Then
                    request = SelectedRequest.RequestID.ToString(WithoutCulture)
                ElseIf Not String.IsNullOrEmpty(SelectedRequest.RequestNumber) Then
                    request = SelectedRequest.RequestNumber
                Else
                    request = Terminology.GetString(MODULENAME, "RequestNumberUnknown")
                End If

                RequestDetails.Text = String.Format(WithCulture, Terminology.GetString(MODULENAME, "RequestDetailTemplate"), description, consultant, status, request, datestring, requestedStatus)
            Else
                SelectedRequest = Nothing
            End If

        Catch ex As Exception
            SelectedRequest = Nothing
            RequestDetails.Text = String.Empty
        End Try
    End Sub
#Region "Dialogs"

    'Private _currentDialog As Form

    Private Sub DoDialog(ByVal f As Form)
        Try
            Cursor.Current = Cursors.Default
            f.ShowDialog()
        Catch ex As ObjectDisposedException
            ' not really an error !
        Catch ex As Exception
            LogError(MODULENAME, "DoDialog", ex, False, Nothing)
        Finally
            'Me.Show()
            Me.Activate()
            Me.PopulateRequestList()
        End Try
    End Sub

    'Private Sub ReturnFromDialog()
    '    Me.InputPanel.Enabled = False
    '    Me.PopulateRequestList()
    '    Me.Show()
    '    Me.BringToFront()
    '    Me.Activate()
    'End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Sub ClientList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientListButton.Click, MainMenuClientList.Click
        Const METHODNAME As String = "ClientList_Click"
        Try
            Cursor.Current = Cursors.WaitCursor
            Using f As New ClientForm(Me)
                DoDialog(f)
            End Using
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub ClientInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestListMenuClientInfo.Click, OpenMenuClientInfo.Click
        Const METHODNAME As String = "ClientInfo_Click"
        Try
            If SelectedRequest Is Nothing Then
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor
            Using f As New ClientForm(Me, SelectedRequest.ClientSiteRow, False)
                DoDialog(f)
            End Using
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub CreateNewRequest()
        If Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeCreateRequest, RES_CreateRequestCancelled) Then
            Cursor.Current = Cursors.WaitCursor
            Using f As activiser.RequestForm = New RequestForm(Me)
                DoDialog(f)
                If f IsNot Nothing AndAlso f.Request IsNot Nothing Then
                    SelectedRequest = f.Request
                End If
            End Using
        End If
    End Sub

    Private Sub CreateNewJob()
        If SelectedRequest Is Nothing Then Return
        If Not AllowNewJob(Me, SelectedRequest) Then Return

        If SelectedRequest IsNot Nothing AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeCreateJob, RES_CreateJobCancelled) Then
            Cursor.Current = Cursors.WaitCursor
            Using f As New JobForm(Me, Me.SelectedRequest)
                DoDialog(f)
            End Using
        End If
    End Sub

#End Region

    Private Function GetStatusColor(ByVal statusId As Integer) As Color
        Return If(Me._statusColors.ContainsKey(statusId), Me._statusColors(statusId), SystemColors.WindowText)
    End Function

    Private Function GetStatusBackColor(ByVal statusId As Integer) As Color
        Return If(Me._statusBackColors.ContainsKey(statusId), Me._statusBackColors(statusId), SystemColors.Window)
    End Function

    Private Shared Function GetStatusString(ByVal drow As RequestRow) As String
        If _showRequestedStatus Then
            If Not drow.IsConsultantStatusDescriptionNull Then
                Return drow.ConsultantStatusDescription
            End If

            If Not drow.IsConsultantStatusIDNull Then
                Return mapStatus(drow.ConsultantStatusID)
            End If
        End If

        If Not drow.IsRequestStatusDescriptionNull Then
            Return drow.RequestStatusDescription
        End If

        If Not drow.IsRequestStatusIDNull Then
            Return mapStatus(drow.RequestStatusID)
        End If
        Return Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
    End Function

    Private Shared Function GetDateString(ByVal drow As RequestRow) As String
        If drow.IsNextActionDateNull Then
            If Not _useLastModifiedTimeForNullActionDates Then Return String.Empty
            If drow.IsModifiedDateTimeNull Then Return String.Empty
            Return FormatDate(drow.ModifiedDateTime, _showTimeInDateColumn)
        Else
            Return FormatDate(drow.NextActionDate, _showTimeInDateColumn)
        End If
    End Function

    'Private Shared Function UseColouredText() As Boolean
    '    Return ConfigurationSettings.GetStringValue(My.Resources.AppConfigMainFormGridColouringKey, My.Resources.AppConfigMainFormGridColouringText) = My.Resources.AppConfigMainFormGridColouringText
    'End Function

    Private Function GetSortColumn() As String
        Dim sortColumn As String
        'get sort order
        Dim sortColumnHeader As SortableColumnHeader
        sortColumnHeader = CType(Me.RequestList.Columns(Me._sortColumnIndex), SortableColumnHeader)
        If gClientDataSet.Request.Columns.IndexOf(sortColumnHeader.ColumnName) <> -1 Then
            sortColumn = sortColumnHeader.ColumnName
            If sortColumnHeader.Ascending Then
                sortColumn &= " ASC"
            Else
                sortColumn &= " DESC"
            End If
        Else
            sortColumn = String.Empty
        End If
        Return sortColumn
    End Function

    Private Shared Function UseRequestStatus(ByVal drow As RequestRow) As Boolean
        If _showRequestedStatus Then
            Return drow.IsConsultantStatusIDNull OrElse (drow.ConsultantStatusID = 0 AndAlso drow.RequestStatusID <> 0)
        Else
            Return True
        End If
    End Function

    Private Shared Function GetRequestIdString(ByVal drow As RequestRow) As String
        Dim strRequestID As String
        If Not drow.IsRequestIDNull Then
            strRequestID = drow.RequestID.ToString(WithoutCulture)
        Else
            strRequestID = Terminology.GetString(MODULENAME, RES_NoRequestID)
        End If
        Return strRequestID
    End Function

    Private Shared Function GetRequestNumber(ByVal drow As RequestRow) As String
        Dim requestNumber As String
        If drow.IsRequestNumberNull Then
            If drow.IsRequestIDNull Then
                requestNumber = Terminology.GetString(MODULENAME, RES_NoRequestNumber)
            Else
                requestNumber = CStr(drow.RequestID)
            End If
        Else
            requestNumber = drow.RequestNumber
        End If
        Return requestNumber
    End Function

    Private Shared Function GetSiteName(ByVal drow As RequestRow) As String
        Dim siteName As String
        If drow.IsClientSiteNameNull Then
            If drow.ClientSiteRow IsNot Nothing AndAlso Not String.IsNullOrEmpty(drow.ClientSiteRow.SiteName.Trim) Then
                siteName = drow.ClientSiteRow.SiteName
            Else
                siteName = Terminology.GetString(MODULENAME, RES_ClientSiteUnknown)
            End If
        Else
            siteName = drow.ClientSiteName
        End If

        Return siteName
    End Function

    Private Sub ResetRequestSelection()
        If Me.RequestList.Items.Count <> 0 Then
            Dim gotSelectedRequest As Boolean
            If Me.SelectedRequest IsNot Nothing Then
                For i As Integer = 0 To Me.RequestList.Items.Count - 1
                    If Me.RequestList.Items(i).Tag Is Me.SelectedRequest Then
                        Me.RequestList.Items(i).Selected = True
                        Me.RequestList.EnsureVisible(i)
                        gotSelectedRequest = True
                        Exit For
                    End If
                Next
            End If
            If Not gotSelectedRequest Then Me.RequestList.Items(0).Selected = True
            SetCurrentRequest()
        Else
            SelectedRequest = Nothing
            RequestDetails.Text = String.Empty
        End If
    End Sub

    Private Shared Function statusTypeFilterMatches(ByVal statusTypeMask As statusTypes, ByVal statusType As statusTypes) As Boolean
        Return (statusTypeMask And statusType) = statusType
    End Function

    Private Function GetListViewItem(ByVal drow As RequestRow, ByVal requestStatusRow As RequestStatusRow) As ListViewItem
        Dim result As ListViewItem
        result = New ListViewItem(New String() { _
            GetRequestNumber(drow), _
            GetRequestIdString(drow), _
            GetDateString(drow), _
            GetSiteName(drow), _
            GetStatusString(drow), _
            drow.GetJobRows.Length.ToString(WithoutCulture)})

        With result
            .Tag = drow ' .RequestUID
            .ForeColor = GetStatusColor(requestStatusRow.RequestStatusID)
            .BackColor = GetStatusBackColor(requestStatusRow.RequestStatusID)
        End With
        Return result
    End Function

    Public Sub PopulateRequestList()
        Const METHODNAME As String = "PopulateRequestList"

        If gClientDataSet.Request.Rows.Count = 0 Then Return

        If Me.InvokeRequired Then
            Me.BeginInvoke(New simpleSubDelegate(AddressOf PopulateRequestList))
            Return
        End If

        _RequestListPopulating = True
        Dim lastSelectedRequest As RequestRow = Me.SelectedRequest
        Try
            RequestList.Items.Clear()

            Dim statusFiltered As Boolean = statusIdFilter <> -1
            Dim statusTypeFiltered As Boolean = Me.statusTypeFilter <> statusTypes.All AndAlso Me.statusTypeFilter <> statusTypes.None
            Dim filterNew As Boolean = Not statusTypeFilterMatches(Me.statusTypeFilter, statusTypes.[New])
            Dim filterWip As Boolean = Not statusTypeFilterMatches(Me.statusTypeFilter, statusTypes.WIP)
            Dim filterComplete As Boolean = Not statusTypeFilterMatches(Me.statusTypeFilter, statusTypes.Complete)
            Dim filterCancelled As Boolean = Not statusTypeFilterMatches(Me.statusTypeFilter, statusTypes.Cancelled)
            Dim sortColumn As String = GetSortColumn()

            Dim drow As RequestRow
            For Each drow In gClientDataSet.Request.Select(String.Empty, sortColumn, DataViewRowState.CurrentRows)
                Try
                    If ViewStatusIncomplete.Checked AndAlso Not hasIncompleteJob(drow) Then Continue For
                    If ViewPersonMe.Checked AndAlso Not IsMyRequest(drow) Then Continue For
                    If ViewPersonOthers.Checked AndAlso IsMyRequest(drow) Then Continue For

                    Dim statusId As Integer = If(UseRequestStatus(drow), drow.RequestStatusID, drow.ConsultantStatusID)
                    If statusFiltered AndAlso (statusId <> statusIdFilter) Then Continue For

                    Dim requestStatusRow As RequestStatusRow = gClientDataSet.RequestStatus.FindByRequestStatusID(statusId)

                    If statusTypeFiltered AndAlso ((filterNew AndAlso requestStatusRow.IsNewStatus) OrElse (filterWip AndAlso requestStatusRow.IsInProgressStatus) OrElse (filterComplete AndAlso requestStatusRow.IsCompleteStatus) OrElse (filterCancelled AndAlso requestStatusRow.IsCancelledStatus)) Then
                        Continue For
                    End If

                    RequestList.Items.Add(GetListViewItem(drow, requestStatusRow))

                Catch ex As Exception
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                End Try
            Next drow

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Me.SelectedRequest = lastSelectedRequest
            Me.UpdateInfo()
            Me.RequestList.Focus()
            Me._RequestListPopulating = False
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Public Sub UpdateInfo()
        Try
            Dim lastSyncText As String = Terminology.GetFormattedString(MODULENAME, RES_LastSyncMessageFormat, If(ConsultantConfig.LastSync = DateTime.MinValue, Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown), FormatDate(ConsultantConfig.LastSync, True)))
            'Me.LastSyncLabel.Text = lastSyncText
            Me.SyncButton.Text = lastSyncText
            If Not Me.GpsLabel.Visible Then Return

            If Gps.HaveValidPosition Then
                Me.SetGpsLabelText(Gps.LastKnownValidPosition.ToString("l", WithoutCulture))
            Else
                Me.SetGpsLabelText(String.Empty)
            End If
        Catch ex As Exception
            Me.SyncButton.Text = String.Empty
        End Try
    End Sub

#Region "Custom Request Status Menu Items"

    'Adds custom context menu options
    Dim RequestMenuItemList As New Collections.Generic.List(Of RequestStatusMenuItem)

    Private Sub ChangeRequestStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Const METHODNAME As String = "RequestListMenuChangeStatus_ItemClick"
        Try
            Dim newStatus As Integer
            Dim isReasonRequired As Boolean
            For Each miRequest As RequestStatusMenuItem In RequestMenuItemList
                If sender Is miRequest.RequestMenuItem OrElse sender Is miRequest.MainMenuItem Then
                    newStatus = miRequest.RequestStatusID
                    isReasonRequired = miRequest.IsReasonRequired
                    Exit For
                End If
            Next

            Dim listViewItem As ListViewItem
            Dim victims As New Collections.Generic.Queue(Of RequestRow)
            Dim victim As RequestRow

            'first pass - look for 'checked' items
            For Each listViewItem In RequestList.Items    ' = intFirstRow To intLastRow
                If listViewItem.Checked = True Then
                    victims.Enqueue(CType(listViewItem.Tag, RequestRow))
                End If
            Next

            'second pass - if no 'checked' items, used 'selected items' list.
            ' Note, for Compact Framework, there can be only one selected item.
            If victims.Count = 0 AndAlso Me.RequestList.SelectedIndices.Count > 0 Then
                For Each i As Integer In Me.RequestList.SelectedIndices
                    listViewItem = Me.RequestList.Items(i)
                    victims.Enqueue(CType(listViewItem.Tag, RequestRow))
                Next
            End If

            'do the work.
            If victims.Count = 0 Then
                Terminology.DisplayMessage(Me, MODULENAME, RES_NoRequestToChange, MessageBoxIcon.Asterisk)
            Else
                _YesToAll = False
                Do While victims.Count > 0
                    victim = victims.Dequeue
                    ChangeRequestStatus(Me, victim, newStatus, isReasonRequired)
                Loop
            End If

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Me.Activate()
            Me.PopulateRequestList()
        End Try
    End Sub

    'Dynamic delegate, handles MenuItemRequestFilter.Click
    Private Sub ViewStatusSpecific_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'For Each filterMI As MenuItem In MainMenuFilterStatus.MenuItems ' clear all generic status filters
        '    If filterMI.MenuItems.Count = 0 Then filterMI.Checked = False
        'Next
        Me.ViewStatusAll.Checked = False
        Me.ViewStatusCancelled.Checked = False
        Me.ViewStatusComplete.Checked = False
        Me.ViewStatusIP.Checked = False
        Me.ViewStatusNew.Checked = False
        Me.ViewStatusIncomplete.Checked = False

        For Each miRequest As RequestStatusMenuItem In RequestMenuItemList
            If sender Is miRequest.FilterMenuItem Then
                statusIdFilter = miRequest.RequestStatusID
                Exit For
            End If
        Next

        'Uncheck all menuitems
        For Each miRequest As RequestStatusMenuItem In RequestMenuItemList
            If statusIdFilter = miRequest.RequestStatusID Then
                miRequest.FilterMenuItem.Checked = True
            Else
                miRequest.FilterMenuItem.Checked = False
            End If
        Next

        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormStatusFilterKey, statusIdFilter)
        Me.PopulateRequestList()
    End Sub

    Private Function AddStatusToContextMenu(ByVal dr As RequestStatusRow) As MenuItem
        'Add item to context menu
        Dim newMenuItem As New MenuItem()
        newMenuItem.Text = dr.Description
        RequestListMenuChangeStatus.MenuItems.Add(newMenuItem)
        AddHandler newMenuItem.Click, AddressOf ChangeRequestStatus_Click
        Return newMenuItem
    End Function

    Private Function AddStatusToMainMenu(ByVal dr As RequestStatusRow) As MenuItem
        'Add item to main menu change status
        Dim newMenuItem As New MenuItem()
        newMenuItem.Text = dr.Description
        MainMenuChangeStatus.MenuItems.Add(newMenuItem)
        AddHandler newMenuItem.Click, AddressOf ChangeRequestStatus_Click
        Return newMenuItem
    End Function

    Private Function AddStatusToFilterMenu(ByVal dr As RequestStatusRow) As MenuItem
        'add item to main menu filter
        Dim newMenuItem As New MenuItem()
        newMenuItem.Text = dr.Description
        ViewStatusSpecific.MenuItems.Add(newMenuItem)
        AddHandler newMenuItem.Click, AddressOf ViewStatusSpecific_Click
        Return newMenuItem
    End Function

    Private Delegate Sub buildRequestMenuDelegate()

    Friend Sub PopulateRequestMenu()
        Const METHODNAME As String = "PopulateRequestMenu"

        If Me.InvokeRequired Then
            Dim d As New buildRequestMenuDelegate(AddressOf PopulateRequestMenu)
            Me.Invoke(d)
            Return
        End If

        ' first remove any eventhandlers
        If Me.RequestMenuItemList.Count > 0 Then
            For Each item As RequestStatusMenuItem In Me.RequestMenuItemList
                Try
                    If item.MainMenuItem IsNot Nothing Then
                        RemoveHandler item.MainMenuItem.Click, AddressOf ChangeRequestStatus_Click
                        'Me.MainMenuChangeStatus.MenuItems.Remove(item.MainMenuItem)
                    End If

                    If item.RequestMenuItem IsNot Nothing Then
                        RemoveHandler item.RequestMenuItem.Click, AddressOf ChangeRequestStatus_Click
                        'Me.RequestListMenuChangeStatus.MenuItems.Remove(item.RequestMenuItem)
                    End If

                    If item.FilterMenuItem IsNot Nothing Then
                        RemoveHandler item.FilterMenuItem.Click, AddressOf ViewStatusSpecific_Click
                        ' Me.MainMenuFilterStatus.MenuItems.Remove(item.FilterMenuItem)
                    End If

                Catch ex As Exception
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                End Try
            Next
        End If

        'then clear the menus.
        Me.MainMenuChangeStatus.MenuItems.Clear()
        Me.RequestListMenuChangeStatus.MenuItems.Clear()
        Me.ViewStatusSpecific.MenuItems.Clear()
        ' Me.MainMenuFilterStatus.MenuItems.Clear()

        For Each dr As RequestStatusRow In gClientDataSet.RequestStatus.Select(Nothing, STR_RequestStatusSortOrder)
            If dr.IsClientMenuItem Then
                Try
                    RequestMenuItemList.Add(New RequestStatusMenuItem(dr, AddStatusToContextMenu(dr), AddStatusToFilterMenu(dr), AddStatusToMainMenu(dr)))
                Catch ex As Exception
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                End Try
            Else ' If dr.IsNewStatus Then '-- add all items to filter list.
                Try
                    RequestMenuItemList.Add(New RequestStatusMenuItem(dr, Nothing, AddStatusToFilterMenu(dr), Nothing))
                Catch ex As Exception
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                End Try
            End If
        Next
    End Sub

#End Region

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Const METHODNAME As String = "Form_Load"
        Try
            Me.Icon = My.Resources.ActiviserIcon

            Terminology.LoadLabels(Me)

            If ConfigurationSettings.GetValue("Hopper", False) Then
                Me.MainMenuExit.Enabled = False
                Me.MainMenuExit.Text = "Hopper"
            End If

            If Gps.HideGps Then
                If Me.MainMenu.MenuItems.Contains(Me.MainMenuGPS) Then
                    Me.MainMenu.MenuItems.Remove(Me.MainMenuGPS)
                End If
                Me.GpsLabel.Visible = False
            Else
                Me.GpsLabel.Visible = True
            End If


            _backgroundLoaderThread = New Threading.Thread(AddressOf backgroundLoader)
            _backgroundLoaderThread.Start()

            Me.Show()

#If Not WINDOWSMOBILE Then
            EnableContextMenus(Me.Controls)
#End If
            Me.BringToFront()

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try

    End Sub

#Region "Delegates"
    Private Delegate Sub simpleSubDelegate()

    Private Delegate Sub SetTextDelegate(ByVal control As Control, ByVal message As String)
    Private Sub SetControlText(ByVal control As Control, ByVal Value As String)
        If Me.InvokeRequired Then
            Dim d As New SetTextDelegate(AddressOf SetControlText)
            Me.Invoke(d, control, Value)
        Else
            Try
                control.Text = Value
                Me.Refresh()
            Catch ex As ObjectDisposedException 'hopper discovered this
                'don't care.
            End Try
        End If
    End Sub

    Private Delegate Function GetTextDelegate(ByVal control As Control) As String
    Private Function GetControlText(ByVal control As Control) As String
        If Me.InvokeRequired Then
            Dim d As New GetTextDelegate(AddressOf GetControlText)
            Return CStr(Me.Invoke(d, control))
        Else
            Try
                Return control.Text
            Catch ex As ObjectDisposedException 'hopper discovered this
                Return String.Empty
            End Try
        End If
    End Function

    Private Delegate Sub SetCheckBoxCheckedDelegate(ByVal control As CheckBox, ByVal state As Boolean)
    Private Sub SetCheckState(ByVal control As CheckBox, ByVal state As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetCheckBoxCheckedDelegate(AddressOf SetCheckState)
            Me.Invoke(d, control, state)
        Else
            Try
                control.Checked = state
                Me.Refresh()
            Catch ex As ObjectDisposedException 'hopper discovered this
                'don't care.
            End Try
        End If
    End Sub

    Private Delegate Function GetCheckboxCheckedDelegate(ByVal control As CheckBox) As Boolean
    Private Function GetCheckState(ByVal control As CheckBox) As Boolean
        If Me.InvokeRequired Then
            Dim d As New GetCheckboxCheckedDelegate(AddressOf GetCheckState)
            Return CType(Me.Invoke(d, control), Boolean)
        End If
        Try
            Return control.Checked
        Catch ex As ObjectDisposedException 'hopper discovered this
            Return False
        End Try
    End Function

    Private Delegate Sub SetMenuItemCheckedDelegate(ByVal control As MenuItem, ByVal state As Boolean)
    Private Sub SetCheckState(ByVal control As MenuItem, ByVal state As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetMenuItemCheckedDelegate(AddressOf SetCheckState)
            Me.Invoke(d, control, state)
        Else
            Try
                control.Checked = state
                Me.Refresh()
            Catch ex As ObjectDisposedException 'hopper discovered this
                'don't care.
            End Try
        End If
    End Sub

    Private Delegate Function GetMenuItemCheckedDelegate(ByVal control As MenuItem) As Boolean
    Private Function GetCheckState(ByVal control As MenuItem) As Boolean
        If Me.InvokeRequired Then
            Dim d As New GetMenuItemCheckedDelegate(AddressOf GetCheckState)
            Return CType(Me.Invoke(d, control), Boolean)
        Else
            Try
                Return control.Checked
            Catch ex As ObjectDisposedException 'hopper discovered this
                Return False
            End Try
        End If
    End Function

    Private Delegate Sub SetControlVisibilityDelegate(ByVal control As Control, ByVal visible As Boolean)
    Private Sub SetControlVisibility(ByVal control As Control, ByVal visible As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetControlVisibilityDelegate(AddressOf SetControlVisibility)
            Me.Invoke(d, control, visible)
        Else
            control.Visible = visible
        End If
    End Sub

    Private Delegate Function GetControlVisibilityDelegate(ByVal control As Control) As Boolean
    Private Function GetControlVisibility(ByVal control As Control) As Boolean
        If Me.InvokeRequired Then
            Dim d As New GetControlVisibilityDelegate(AddressOf GetControlVisibility)
            Return CBool(Me.Invoke(d, control))
        Else
            Return control.Visible
        End If
    End Function


    Private Delegate Sub SetControlEnabledDelegate(ByVal control As Control, ByVal enabled As Boolean)
    Private Sub SetControlEnabled(ByVal control As Control, ByVal enabled As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetControlEnabledDelegate(AddressOf SetControlEnabled)
            Me.Invoke(d, control, enabled)
        Else
            control.Enabled = enabled
        End If
    End Sub

    Private Delegate Function GetControlEnabledDelegate(ByVal control As Control) As Boolean
    Private Function GetControlEnabled(ByVal control As Control) As Boolean
        If Me.InvokeRequired Then
            Dim d As New GetControlEnabledDelegate(AddressOf GetControlEnabled)
            Return CBool(Me.Invoke(d, control))
        Else
            Return control.Enabled
        End If
    End Function

    Private Delegate Sub SetMenuItemEnabledDelegate(ByVal menuItem As MenuItem, ByVal enabled As Boolean)
    Private Sub SetControlEnabled(ByVal menuItem As MenuItem, ByVal enabled As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetMenuItemEnabledDelegate(AddressOf SetControlEnabled)
            Me.Invoke(d, menuItem, enabled)
        Else
            menuItem.Enabled = enabled
        End If
    End Sub

    Private Delegate Function GetMenuItemEnabledDelegate(ByVal menuItem As MenuItem) As Boolean
    Private Function GetControlEnabled(ByVal menuItem As MenuItem) As Boolean
        If Me.InvokeRequired Then
            Dim d As New GetMenuItemEnabledDelegate(AddressOf GetControlEnabled)
            Return CBool(Me.Invoke(d, menuItem))
        Else
            Return menuItem.Enabled
        End If
    End Function
#End Region

    Private Sub SetRequestListStyle()
        If Me.InvokeRequired Then
            Dim d As New simpleSubDelegate(AddressOf SetRequestListStyle)
            Me.Invoke(d)
        Else
            Me.RequestList.HeaderStyle = If(Me.ViewShowColumnHeaders.Checked, ColumnHeaderStyle.Clickable, ColumnHeaderStyle.None)
            Me.RequestList.CheckBoxes = _showRequestListCheckboxes
        End If
    End Sub

    ''' Note: loading all this in a background thread so that the main application form comes up quickly.
    Private Sub backgroundLoader()
        Const METHODNAME As String = "backgroundLoader"
        Try
            Debug.WriteLine("Main form thread:" & CStr(Threading.Thread.CurrentThread.ManagedThreadId))

            _useLastModifiedTimeForNullActionDates = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormUseLastModifiedForNullActionDatesKey, False)
            _showTimeInDateColumn = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowTimeInDateColumnKey, False)
            _showRequestedStatus = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowRequestedStatusKey, True)
            _showToolBar = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowToolBarKey, True)
            _toolBarSize = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormToolBarSizeKey, CInt(RunTimeScale * 2))
            _showToolBarCaptions = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowToolBarCaptionsKey, False)
            _showRequestListCheckboxes = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowCheckBoxesKey, False)

            SetCheckState(Me.ViewShowTimeInDateColumn, _showTimeInDateColumn)
            SetCheckState(Me.ViewShowCheckBoxes, _showRequestListCheckboxes)
            SetCheckState(Me.ViewShowColumnHeaders, ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowColumnHeadersKey, True))
            SetCheckState(Me.ViewShowSyncInfo, ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowSyncInfoKey, True))
            SetCheckState(Me.ViewShowRequestedStatus, _showRequestedStatus)
            SetCheckState(Me.ViewMenuToolbarShowCaptions, _showToolBarCaptions)

            Me.LoadImages()
            Me.SetToolBarView()
            Me.PopulateRequestMenu()
            Me.InitializeRequestList()
            Me.SetRequestListStyle()

            SetControlVisibility(Me.LastSyncPanel, GetCheckState(Me.ViewShowSyncInfo))

            Me.statusIdFilter = ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormStatusFilterKey, -1)
            For Each miRequest As RequestStatusMenuItem In RequestMenuItemList
                SetCheckState(miRequest.FilterMenuItem, miRequest.RequestStatusID = statusIdFilter)
            Next

            SetCheckState(Me.ViewPersonMe, ConfigurationSettings.GetValue(My.Resources.AppConfigMainFormShowMyRequestsOnlyKey, True))
            If Me.ViewPersonMe.Checked Then
                Me.SetConsultantFilter(Me.ViewPersonMe)
            Else
                Me.SetConsultantFilter(Me.ViewPersonAll)
            End If

            SetControlEnabled(Me.MainMenuNewRequest, gAllowNewRequests)

            Gps.SetMenuStates()

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

#Region "Menu Items"

    Private Sub Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuExit.Click
        If Terminology.AskQuestion(Me, MODULENAME, RES_ConfirmExit, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Me.Close()
        Else
            Return
        End If
    End Sub

    Private Sub Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click
        Me.PopulateRequestList()
        Me.RequestDetails.Text = String.Empty
        Me.SelectedRequest = Nothing
    End Sub


    Private Sub NewJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenMenuNewJob.Click, RequestListMenuNewJob.Click, NewJobButton.Click
        Const METHODNAME As String = "NewJob_Click"
        Try
            If SelectedRequest Is Nothing Then
                If gAllowNewRequests Then
                    If Terminology.AskQuestion(Me, MODULENAME, RES_ConfirmNewRequest, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                        CreateNewRequest()
                    End If
                Else
                    Terminology.DisplayMessage(Me, MODULENAME, RES_NewRequestsNotAllowed, MessageBoxIcon.Asterisk)
                End If
            Else
                CreateNewJob()
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    'Tools
    Private Sub OpenRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestListMenuOpen.Click, OpenMenuRequest.Click, OpenRequestButton.Click
        Const METHODNAME As String = "OpenRequest_Click"
        Try
            If SelectedRequest IsNot Nothing AndAlso Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeOpenRequest, RES_OpenRequestCancelled) Then
                Cursor.Current = Cursors.WaitCursor
                Using f As activiser.RequestForm = New RequestForm(Me, SelectedRequest, RequestFormTab.Main)
                    DoDialog(f)
                End Using
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub About_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuAbout.Click
        Const METHODNAME As String = "About_Click"
        Try
            'activiser.PlaySound()
            Cursor.Current = Cursors.WaitCursor
            Using f As New AboutForm(Me)
                Cursor.Current = Cursors.Default
                f.ShowDialog()
            End Using
            Me.BringToFront()
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    'Context Menu Items
    Private Sub JobList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestListMenuListJobs.Click, OpenMenuJobList.Click
        Const METHODNAME As String = "JobList_Click"
        Try
            If SelectedRequest Is Nothing Then
                Exit Sub
            End If
            If Synchronisation.CheckForAutoSync(Me, MODULENAME, RES_ResumeOpenRequest, RES_OpenRequestCancelled) Then
                'activiser.PlaySound()
                Cursor.Current = Cursors.WaitCursor
                Using f As activiser.RequestForm = New RequestForm(Me, SelectedRequest, RequestFormTab.Jobs)
                    Cursor.Current = Cursors.Default
                    DoDialog(f)
                End Using
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Dim _YesToAll As Boolean

    Private Shared Function GetMessageKey(ByVal Victim As RequestRow) As String
        Select Case Victim.RowState
            Case DataRowState.Added
                Return "ConfirmNewRequestRemoval"
            Case DataRowState.Deleted, DataRowState.Detached
                Return "Yes"
            Case DataRowState.Modified
                Return "ConfirmModifiedRequestRemoval"
            Case DataRowState.Unchanged
                Return "ConfirmRequestRemoval"
            Case Else
                Return "No"
        End Select
        Return String.Empty
    End Function

    Private Function ConfirmRequestRemoval(ByVal Victim As RequestRow, ByVal NoOfVictims As Integer) As DialogResult
        Dim messageKey As String = GetMessageKey(Victim)
        If messageKey = "Yes" Then Return DialogResult.Yes
        If messageKey = "No" Then Return DialogResult.No

        Dim buttons As MessageBoxButtons = If(NoOfVictims = 1, MessageBoxButtons.YesNo, MessageBoxButtons.YesNoCancel)

        Dim initialConfirmation As DialogResult
        Dim multipleConfirmation As DialogResult

        initialConfirmation = Terminology.AskFormattedQuestion(Me, MODULENAME, messageKey, buttons, MessageBoxDefaultButton.Button2, Victim.RequestNumber, Victim.ClientSiteRow.SiteName, Victim.ShortDescription)
        If NoOfVictims = 1 Then Return initialConfirmation

        If initialConfirmation = DialogResult.Yes Then
            multipleConfirmation = Terminology.AskQuestion(Me, MODULENAME, RES_DeleteYesToAll, buttons, MessageBoxDefaultButton.Button2)
            _YesToAll = (multipleConfirmation = DialogResult.Yes)
            If _YesToAll Then Cursor.Current = Cursors.WaitCursor

            If multipleConfirmation = DialogResult.Yes OrElse multipleConfirmation = DialogResult.No Then
                Return DialogResult.Yes
            End If
        End If

        Return initialConfirmation
    End Function

    Private Sub EnqueueCheckedItems(ByVal victims As Collections.Generic.Queue(Of RequestRow))
        ' enqueue checked items
        For Each listViewItem As ListViewItem In RequestList.Items
            If listViewItem.Checked = True Then
                victims.Enqueue(CType(listViewItem.Tag, RequestRow))
            End If
        Next
    End Sub

    Private Sub EnqueueSelectedItems(ByVal victims As Collections.Generic.Queue(Of RequestRow))
        'enqueue selected items
        If victims.Count = 0 AndAlso Me.RequestList.SelectedIndices.Count > 0 Then
            For Each i As Integer In Me.RequestList.SelectedIndices
                victims.Enqueue(CType(Me.RequestList.Items(i).Tag, RequestRow))
            Next
        End If
    End Sub

    Private Sub RemoveSingleRequest(ByVal victims As Collections.Generic.Queue(Of RequestRow))
        Dim victim As RequestRow
        victim = victims.Dequeue()
        If ConfirmRequestRemoval(victim, 1) = DialogResult.Yes Then
            RemoveRequest(Me, victim)
        End If
    End Sub

    Private Sub RemoveMultipleRequests(ByVal victims As Collections.Generic.Queue(Of RequestRow))
        _YesToAll = False
        Do While victims.Count > 0
            Dim victim As RequestRow
            victim = victims.Dequeue
            If Me._YesToAll Then
                RemoveRequest(Me, victim)
            Else
                Dim removalConfirmation As DialogResult = ConfirmRequestRemoval(victim, victims.Count)
                If removalConfirmation = DialogResult.Cancel Then Exit Do ' stop deleting

                If removalConfirmation = DialogResult.Yes Then
                    RemoveRequest(Me, victim)
                End If
            End If
        Loop
    End Sub

    Private Sub RemoveRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestListMenuRemoveRequest.Click, MainMenuRemoveRequest.Click
        Const METHODNAME As String = "RemoveRequest_Click"
        Try
            'Dim currentRequest As activiser.RequestRow '= moCurrentRequest
            'Dim listViewItem As ListViewItem
            Dim victims As New Collections.Generic.Queue(Of RequestRow)

            EnqueueCheckedItems(victims)
            EnqueueSelectedItems(victims)

            If victims.Count = 0 Then
                Terminology.DisplayMessage(Me, MODULENAME, RES_NoRequestToRemove, MessageBoxIcon.Asterisk)
                Return
            End If

            If victims.Count = 1 Then
                RemoveSingleRequest(victims)
            Else
                RemoveMultipleRequests(victims)
            End If

            ConsultantConfig.SavePending()

            RequestDetails.Text = String.Empty
            SelectedRequest = Nothing
            _YesToAll = False
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try

        Me.PopulateRequestList()
    End Sub
#End Region

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim columnWidths As New System.Text.StringBuilder(RequestList.Columns.Count * 3)
        For Each c As ColumnHeader In Me.RequestList.Columns
            columnWidths.AppendFormat(WithoutCulture, "{0},", c.Width)
        Next
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormToolBarSizeKey, _toolBarSize)
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormLastSortColumnKey, _sortColumnIndex)
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormColumnWidthsKey, columnWidths.ToString)
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormStatusFilterKey, statusIdFilter)
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowMyRequestsOnlyKey, ViewPersonMe.Checked)
        ConfigurationSettings.Save()
        SavePending(gClientDataSet, gMainDbFileName)
    End Sub

    Private Sub DoSync()
        Const METHODNAME As String = "DoSync"
        Try
            Using sf As New SyncForm(Me)
                Synchronisation.StartManualSync()
                sf.ShowDialog()
            End Using
            Me.PopulateRequestList()
            Me.UpdateInfo()
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    'Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    SavePending(gClientDataSet, gMainDbFileName)
    'End Sub

    Friend Sub SetConsultantFilter(ByVal menuItem As MenuItem)
        For Each filterMI As MenuItem In Me.MainMenuFilterOwner.MenuItems
            SetCheckState(filterMI, filterMI Is menuItem)
        Next
        Me.PopulateRequestList()
    End Sub

    Private Sub ConsultantFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewPersonAll.Click, ViewPersonOthers.Click, ViewPersonMe.Click
        SetConsultantFilter(CType(sender, MenuItem))
    End Sub

    Private Sub ViewStatus_Clicked(ByVal mi As MenuItem, ByVal tf As statusTypes)
        For Each miRequest As RequestStatusMenuItem In RequestMenuItemList ' remove all specific status filters.
            miRequest.FilterMenuItem.Checked = False
        Next

        Me.ViewStatusAll.Checked = mi Is Me.ViewStatusAll
        Me.ViewStatusCancelled.Checked = mi Is Me.ViewStatusCancelled
        Me.ViewStatusComplete.Checked = mi Is Me.ViewStatusComplete
        Me.ViewStatusIP.Checked = mi Is Me.ViewStatusIP
        Me.ViewStatusNew.Checked = mi Is Me.ViewStatusNew
        Me.ViewStatusIncomplete.Checked = mi Is Me.ViewStatusIncomplete
        'For Each filterMI As MenuItem In MainMenuFilterStatus.MenuItems
        '    If filterMI Is mi Then
        '        filterMI.Checked = True
        '    Else
        '        If filterMI.MenuItems.Count = 0 Then filterMI.Checked = False
        '    End If
        'Next

        Me.statusTypeFilter = tf
        Me.statusIdFilter = -1
        Me.PopulateRequestList()
    End Sub

    Private Sub ViewStatusAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStatusAll.Click
        ViewStatus_Clicked(ViewStatusAll, statusTypes.All)
    End Sub

    Private Sub ViewStatusNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStatusNew.Click
        ViewStatus_Clicked(ViewStatusNew, statusTypes.[New])
    End Sub

    Private Sub ViewStatusIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStatusIP.Click
        ViewStatus_Clicked(ViewStatusIP, statusTypes.WIP)
    End Sub

    Private Sub ViewStatusComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStatusComplete.Click
        ViewStatus_Clicked(ViewStatusComplete, statusTypes.Complete)
    End Sub

    Private Sub ViewStatusCancelled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStatusCancelled.Click
        ViewStatus_Clicked(ViewStatusCancelled, statusTypes.Cancelled)
    End Sub

    Private Sub ViewIncomplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStatusIncomplete.Click
        ViewStatus_Clicked(ViewStatusIncomplete, statusTypes.All)
    End Sub

    Private Sub requestList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestList.SelectedIndexChanged
        If Not _RequestListPopulating Then
            SetCurrentRequest()
        End If
        RequestList.Invalidate()
    End Sub

    Private Sub requestList_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles RequestList.ColumnClick
        Dim reverse As Boolean = Me._sortColumnIndex = e.Column
        Me._sortColumnIndex = e.Column

        Dim sortColumnHeader As SortableColumnHeader = CType(Me.RequestList.Columns(Me._sortColumnIndex), SortableColumnHeader)
        If reverse Then sortColumnHeader.Ascending = Not sortColumnHeader.Ascending

        Me.PopulateRequestList()
        'Me.RequestList.BeginUpdate()
        'SortListView(Me.RequestList, e.Column, True)
        'Me.RequestList.EndUpdate()
    End Sub

    ''helper method to sort a particular listview column
    'Private Shared Sub SortListView(ByRef lv As ListView, ByVal col As Integer, ByVal isOppDirect As Boolean)
    '    Const METHODNAME As String = "SortListView"
    '    Try
    '        If lv.Items.Count = 0 Then Return

    '        Dim header As SortableColumnHeader = CType(lv.Columns(col), SortableColumnHeader)

    '        If isOppDirect Then
    '            header.Ascending = Not header.Ascending
    '        Else
    '            header.Ascending = True
    '        End If

    '        ConfigurationSettings.SetIntegerValue(My.Resources.AppConfigMainFormSortColumnKey, col)
    '        ConfigurationSettings.SetBooleanValue(My.Resources.AppConfigMainFormSortAscendingKey, header.Ascending)

    '        Dim cItems As Integer = lv.Items.Count - 1
    '        Dim rgTemp As New Generic.List(Of listViewItemWrapper)

    '        For i As Integer = 0 To cItems
    '            rgTemp.Add(New listViewItemWrapper(lv.Items(i), col))
    '        Next

    '        If gClientDataSet.Request.Columns.Contains(header.ColumnName) Then
    '            rgTemp.Sort(0, rgTemp.Count, New activiser.ListViewItemDataRowComparer(header.Ascending, header.ColumnName))
    '        Else
    '            Select Case header.ColumnType
    '                Case ColumnType.Number
    '                    rgTemp.Sort(0, rgTemp.Count, New activiser.ListViewItemNumberComparer(header.Ascending))
    '                Case ColumnType.DateTime
    '                    rgTemp.Sort(0, rgTemp.Count, New activiser.ListViewItemDateComparer(header.Ascending))
    '                Case Else ' sort by string
    '                    rgTemp.Sort(0, rgTemp.Count, New activiser.listViewItemComparer(header.Ascending))
    '            End Select
    '        End If

    '        'If rgTemp.Count <> 0 Then

    '        'End If

    '        lv.Items.Clear()
    '        For i As Integer = 0 To cItems
    '            lv.Items.Add(CType(rgTemp(i), ListViewItem))
    '        Next


    '    Catch ex As Exception
    '        LogError(MODULENAME, METHODNAME, ex, False, Nothing)
    '    End Try
    'End Sub

    Private Sub Sync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncButton.Click, MainMenuSync.Click
        DoSync()
        'Me.BringToFront()
    End Sub

    Private Sub NewRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuNewRequest.Click
        CreateNewRequest()
    End Sub

    Private Sub SettingsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuSettings.Click
        Const METHODNAME As String = "Settings_Click"
        Try
            Cursor.Current = Cursors.WaitCursor
            Using f As New SettingsForm(Me)
                DoDialog(f)
            End Using

            Dim fontSize As Integer = ConfigurationSettings.GetValue(My.Resources.AppConfigTextSizeKey, 8)
            Dim mainFont As Font = New Font(Me.Font.Name, fontSize, FontStyle.Regular)
            Dim detailFont As New Font(Me.Font.Name, If(fontSize < 5, 4, fontSize - 1), FontStyle.Regular)
            Me.RequestList.Font = mainFont
            Me.RequestDetails.Font = detailFont
            Me.GpsLabel.Font = detailFont

            Me.InitializeRequestList()
            Me.PopulateRequestList()
            Me.SetToolBarView()
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub MenuViewToolbar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewMenuToolbarOff.Click, ViewMenuToolbarSmall.Click, ViewMenuToolbarMedium.Click, ViewMenuToolbarLarge.Click, ViewMenuToolbarSmaller.Click, ViewMenuToolbarLarger.Click
        _showToolBar = Not sender Is ViewMenuToolbarOff
        If sender Is ViewMenuToolbarSmaller Then
            _toolBarSize = 1
        ElseIf sender Is ViewMenuToolbarSmall Then
            _toolBarSize = 2
        ElseIf sender Is ViewMenuToolbarMedium Then
            _toolBarSize = 3
        ElseIf sender Is ViewMenuToolbarLarge Then
            _toolBarSize = 4
        ElseIf sender Is ViewMenuToolbarLarger Then
            _toolBarSize = 5
        Else
            _toolBarSize = 0
        End If

        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowToolBarKey, _showToolBar)
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormToolBarSizeKey, _toolBarSize)
        SetToolBarView()
    End Sub
    ' toolbar/button sizes - first value is strictly unnecessary (since it's just the array index), 
    ' but in there for clarity of the code.
    ' values: (0) = index, (1) = icon size/toolbar height, (2) = toolbar height with captions, (3) button width.
    Private Shared _toobarSizes(,) As Integer = _
        { _
            {0, 0, 0, 0}, _
            {1, 16, 24, 32}, _
            {2, 24, 36, 48}, _
            {3, 32, 48, 56}, _
            {4, 48, 72, 80}, _
            {5, 64, 96, 104} _
        }


    Private Sub SetButtonSizeAndIcon(ByVal target As Library.Forms.ImageButton, ByVal iconSize As Integer, ByVal buttonHeight As Integer, ByVal buttonWidth As Integer, ByVal buttonName As String, ByVal showText As Boolean, ByVal font As Font)
        target.TextVisible = showText
        target.Size = New Size(buttonWidth, buttonHeight)
        target.Font = font
        target.Image = CType(My.Resources.ResourceManager.GetObject(String.Format("{0}Icon{1}", buttonName, iconSize)), Bitmap)
    End Sub

    Private Sub SetToolBarLeft(ByVal buttonFont As Font)
        Dim iconSize As Integer = _toobarSizes(_toolBarSize, 1)
        Dim buttonWidth As Integer = _toobarSizes(_toolBarSize, 3)
        Dim borderSize As Integer = CInt(4 * RunTimeScale) '   Me.Toolbar.Width - Me.ToolbarMargin.Width
        Dim toolbarWidth As Integer

        Dim buttonHeight As Integer = _toobarSizes(_toolBarSize, 2) + If(_showToolBarCaptions, CInt(buttonFont.Size * RunTimeScale), 0)

        NewJobButton.Dock = DockStyle.None
        ClientListButton.Dock = DockStyle.None
        OpenRequestButton.Dock = DockStyle.None

        Me.Toolbar.Dock = DockStyle.Left

        SetButtonSizeAndIcon(OpenRequestButton, iconSize, buttonHeight, buttonWidth, "Request", _showToolBarCaptions, buttonFont)
        SetButtonSizeAndIcon(NewJobButton, iconSize, buttonHeight, buttonWidth, "Job", _showToolBarCaptions, buttonFont)
        SetButtonSizeAndIcon(ClientListButton, iconSize, buttonHeight, buttonWidth, "Client", _showToolBarCaptions, buttonFont)
        toolbarWidth = OpenRequestButton.Width ' buttonWidth + borderSize
        Me.Toolbar.Width = toolbarWidth
        OpenRequestButton.Dock = DockStyle.Top
        NewJobButton.Dock = DockStyle.Top
        ClientListButton.Dock = DockStyle.Top
    End Sub


    Private Sub SetToolBarBottom(ByVal buttonFont As Font)
        Dim iconSize As Integer = _toobarSizes(_toolBarSize, 1)
        Dim buttonWidth As Integer = _toobarSizes(_toolBarSize, 3)
        Dim borderSize As Integer = CInt(4 * RunTimeScale) 'Me.Toolbar.Height - Me.ToolbarMargin.Height
        Dim buttonHeight As Integer = _toobarSizes(_toolBarSize, 2) + If(_showToolBarCaptions, CInt(buttonFont.Size), 0) 'If(_showToolBarCaptions, 2, 1))
        Dim toolbarHeight As Integer

        NewJobButton.Dock = DockStyle.None
        ClientListButton.Dock = DockStyle.None
        OpenRequestButton.Dock = DockStyle.None

        Me.Toolbar.Dock = DockStyle.Bottom

        SetButtonSizeAndIcon(OpenRequestButton, iconSize, buttonHeight, buttonWidth, "Request", _showToolBarCaptions, buttonFont)
        SetButtonSizeAndIcon(NewJobButton, iconSize, buttonHeight, buttonWidth, "Job", _showToolBarCaptions, buttonFont)
        SetButtonSizeAndIcon(ClientListButton, iconSize, buttonHeight, buttonWidth, "Client", _showToolBarCaptions, buttonFont)
        toolbarHeight = OpenRequestButton.Height ' buttonHeight + borderSize ' _toobarSizes(_toolBarSize, 1) + borderSize + If(_showToolBarCaptions, CInt(buttonFont.Size), 0)
        Me.Toolbar.Height = toolbarHeight
        OpenRequestButton.Dock = DockStyle.Left
        NewJobButton.Dock = DockStyle.Left
        ClientListButton.Dock = DockStyle.Left
    End Sub

    Private Sub SetToolBarView()
        If Me.InvokeRequired Then
            Dim d As New simpleSubDelegate(AddressOf SetToolBarView)
            Me.Invoke(d)
        Else
            Try
                Me.SuspendLayout()
                Me.Toolbar.SuspendLayout()
                Me.OpenRequestButton.BeginUpdate()
                Me.NewJobButton.BeginUpdate()
                Me.ClientListButton.BeginUpdate()
                Me.OpenRequestButton.Padding = New Padding(4)
                Me.NewJobButton.Padding = New Padding(4)
                Me.ClientListButton.Padding = New Padding(4)

                Me.Toolbar.Visible = _showToolBar

                If Not _showToolBar Then
                    Return ' not worth doing the rest !
                End If

                For Each mi As MenuItem In Me.ViewToolBar.MenuItems
                    If mi Is ViewMenuToolbarShowCaptions Then Continue For
                    mi.Checked = False
                Next

                Select Case _toolBarSize
                    Case 5
                        ViewMenuToolbarLarger.Checked = True
                    Case 4
                        ViewMenuToolbarLarge.Checked = True
                    Case 3
                        ViewMenuToolbarMedium.Checked = True
                    Case 2
                        ViewMenuToolbarSmall.Checked = True
                    Case 1
                        ViewMenuToolbarSmaller.Checked = True
                    Case Else
                        ViewMenuToolbarOff.Checked = True
                End Select

                Dim buttonFontSize As Single = ConfigurationSettings.GetValue(My.Resources.AppConfigTextSizeKey, 8) - 1
                If buttonFontSize < 4 Then buttonFontSize = 4
                Dim buttonFont As New Font(Me.RequestList.Font.Name, buttonFontSize, FontStyle.Regular)

                If Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height Then
                    SetToolBarLeft(buttonFont)
                Else
                    SetToolBarBottom(buttonFont)
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
            Finally
                Me.OpenRequestButton.EndUpdate()
                Me.NewJobButton.EndUpdate()
                Me.ClientListButton.EndUpdate()
                Me.Toolbar.ResumeLayout()
                Me.ResumeLayout()
            End Try

        End If
    End Sub



    Public Sub LoadImages()
        If Me.InvokeRequired Then
            Dim d As New simpleSubDelegate(AddressOf LoadImages)
            Me.Invoke(d)
            Return
        End If

        If MyDpiX > 100 Then
            SyncButton.Image = My.Resources.SyncIcon48
        Else
            SyncButton.Image = My.Resources.SyncIcon24
        End If
        Me.LastSyncPanel.Height = SyncButton.Image.Height
    End Sub


    Private Sub menuShowCheckBoxes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewShowCheckBoxes.Click
        Dim isChecked As Boolean = Not ViewShowCheckBoxes.Checked
        Me.RequestList.CheckBoxes = isChecked
        Me.ViewShowCheckBoxes.Checked = isChecked
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowCheckBoxesKey, isChecked)
    End Sub

    Private Sub ViewShowColumnHeaders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewShowColumnHeaders.Click
        Dim isChecked As Boolean = Not ViewShowColumnHeaders.Checked
        Me.RequestList.HeaderStyle = If(isChecked, ColumnHeaderStyle.Clickable, ColumnHeaderStyle.None)
        Me.ViewShowColumnHeaders.Checked = isChecked
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowColumnHeadersKey, isChecked)
    End Sub

    Private Sub ViewShowSyncInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewShowSyncInfo.Click
        Dim isChecked As Boolean = Not ViewShowSyncInfo.Checked
        Me.LastSyncPanel.Visible = isChecked
        Me.ViewShowSyncInfo.Checked = isChecked
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowSyncInfoKey, isChecked)
    End Sub

    Private Shared Function ListViewCheckedItems(ByVal lv As ListView) As ListViewItem()
        Dim result As New Collections.Generic.List(Of ListViewItem)

        For Each listViewItem As ListViewItem In lv.Items
            If listViewItem.Checked Then
                result.Add(listViewItem)
            End If
        Next
        Return result.ToArray
    End Function

    Private Sub RequestListMenu_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestListMenu.Popup
        If ListViewCheckedItems(Me.RequestList).Length <= 1 Then
            Me.RequestListMenuRemoveRequest.Text = Terminology.GetString(MODULENAME, RES_RequestMenuRemove)
        Else
            Me.RequestListMenuRemoveRequest.Text = Terminology.GetString(MODULENAME, RES_RequestMenuRemovePlural)
        End If
    End Sub



    Private Sub MainMenu_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenu.Popup
        If ListViewCheckedItems(Me.RequestList).Length <= 1 Then
            Me.MainMenuRemoveRequest.Text = Terminology.GetString(MODULENAME, RES_RequestMenuRemove)
        Else
            Me.MainMenuRemoveRequest.Text = Terminology.GetString(MODULENAME, RES_RequestMenuRemovePlural)
        End If
    End Sub

    Public Function IsVisible() As Boolean
        If Me.InvokeRequired Then
            Return CBool(Me.Invoke(New GetValueDelegate(Of Boolean)(AddressOf IsVisible)))
        Else
            Return Me.Visible
        End If
    End Function

    Private Function GetWindowState() As FormWindowState
        'TODO: make this into a try/catch
        'If Me.IsDisposed Then Return FormWindowState.Normal
        If Me.InvokeRequired Then
            Dim d As New GetWindowStateDelegate(AddressOf GetWindowState)
            Return CType(Me.Invoke(d), FormWindowState)
        Else
            Return Me.WindowState '= FormWindowState.Maximized
        End If
    End Function

    Public ReadOnly Property WindowsState() As FormWindowState
        Get
            Return Me.GetWindowState()
        End Get
    End Property


    Private Sub MenuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestListMenuSelectAll.Click, MenuEditSelectAll.Click
        For Each listViewItem As ListViewItem In Me.RequestList.Items
            listViewItem.Checked = True
        Next
    End Sub

    Private Sub MenuSelectNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestListMenuSelectNone.Click, MenuEditSelectNone.Click
        For Each listViewItem As ListViewItem In Me.RequestList.Items
            listViewItem.Checked = False
        Next
    End Sub

    Private Sub ViewShowTimeInDateColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewShowTimeInDateColumn.Click
        ViewShowTimeInDateColumn.Checked = Not ViewShowTimeInDateColumn.Checked
        _showTimeInDateColumn = ViewShowTimeInDateColumn.Checked
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowTimeInDateColumnKey, _showTimeInDateColumn)
        Me.PopulateRequestList()
    End Sub

    Private Delegate Sub SetGpsMenuStateDelegate(ByVal GpsState As GpsServiceState)

    Friend Sub SetGpsMenuState(ByVal gpsState As GpsServiceState)
        If Me.InvokeRequired Then
            Me.Invoke(New SetGpsMenuStateDelegate(AddressOf SetGpsMenuState), gpsState)
        Else
            If gpsState = GpsServiceState.On Then
                Me.MainMenuGpsOff.Checked = False
                Me.MainMenuGpsOff.Enabled = True
                Me.MainMenuGpsOn.Checked = True
                Me.MainMenuGpsOn.Enabled = False
                Me.GpsLabel.Visible = True
            Else
                Me.MainMenuGpsOn.Checked = False
                Me.MainMenuGpsOn.Enabled = True
                Me.MainMenuGpsOff.Checked = True
                Me.MainMenuGpsOff.Enabled = False
                Me.GpsLabel.Visible = False
            End If
        End If
    End Sub

    Private Delegate Sub SetGpsLabelTextDelegate(ByVal message As String)

    Friend Sub SetGpsLabelText(ByVal message As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetGpsLabelTextDelegate(AddressOf SetGpsLabelText), message)
        Else
            GpsLabel.Text = message
        End If
    End Sub

    Private Sub MainMenuGpsInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuGpsInfo.Click
        Gps.DisplayGpsInfo(Me)
    End Sub

    Private Sub MainMenuGpsOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuGpsOn.Click
        Gps.StartGPS()
    End Sub

    Private Sub MainMenuGpsOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuGpsOff.Click
        Gps.StopGps()
    End Sub

    Private Sub MainForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        gMainForm = Nothing
    End Sub

    Private Sub ViewShowRequestedStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewShowRequestedStatus.Click
        ViewShowRequestedStatus.Checked = Not ViewShowRequestedStatus.Checked
        _showRequestedStatus = ViewShowRequestedStatus.Checked
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowRequestedStatusKey, _showRequestedStatus)
        Me.InitializeRequestList()
    End Sub

    Private Sub backgroundLoaderTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles backgroundLoaderTimer.Tick
        backgroundLoader()
    End Sub

#If WINDOWSMOBILE Then
#Region "Input Panel Support"
    'Private Sub EnableInputPanelOnFormActivated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    '    InputPanel.Enabled = False
    'End Sub

    'Private Sub EnableInputPanelOnFormLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    InputPanel.Enabled = False
    'End Sub

    'Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
    '    InputPanel.Enabled = False
    'End Sub
#End Region

#Region "WindowsState Support"

    Private Sub ViewFullScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFullScreen.Click
        SetFormState(Me, Me.ViewFullScreen, Not Me.ViewFullScreen.Checked)
    End Sub

    Private Sub ViewOrientation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewRotate0.Click, ViewRotate90.Click, ViewRotate180.Click, ViewRotate270.Click
        If sender Is Me.ViewRotate0 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle0
        ElseIf sender Is Me.ViewRotate90 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90
        ElseIf sender Is Me.ViewRotate180 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle180
        ElseIf sender Is Me.ViewRotate270 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle270
        End If
        FixOrientationMenu()
    End Sub

    Private Sub FixOrientationMenu()
        Me.ViewRotate0.Checked = False
        Me.ViewRotate90.Checked = False
        Me.ViewRotate180.Checked = False
        Me.ViewRotate270.Checked = False
        Select Case SystemSettings.ScreenOrientation
            Case ScreenOrientation.Angle0
                Me.ViewRotate0.Checked = True
            Case ScreenOrientation.Angle90
                Me.ViewRotate90.Checked = True
            Case ScreenOrientation.Angle180
                Me.ViewRotate180.Checked = True
            Case ScreenOrientation.Angle270
                Me.ViewRotate270.Checked = True
        End Select
    End Sub

#End Region


#End If

    Private Sub ViewStatusOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStatusOpen.Click
        ViewStatus_Clicked(ViewStatusOpen, statusTypes.Open)
    End Sub

    Private Sub ViewMenuToolbarShowCaptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewMenuToolbarShowCaptions.Click
        _showToolBarCaptions = Not _showToolBarCaptions
        ViewMenuToolbarShowCaptions.Checked = _showToolBarCaptions
        ConfigurationSettings.SetValue(My.Resources.AppConfigMainFormShowToolBarCaptionsKey, _showToolBarCaptions)
        SetToolBarView()
    End Sub

    Private Sub MainForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        ' If _backgroundLoaderThread IsNot Nothing Then Return
        If _showToolBar Then SetToolBarView()
    End Sub

    Private Sub RequestListMenuOpenCurrentJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestListMenuOpenCurrentJob.Click
        Const METHODNAME As String = "RequestListMenuOpenCurrentJob_Click"
        Try
            If SelectedRequest IsNot Nothing Then
                Dim wipRow As JobRow = Nothing
                Dim wipRows As Integer = GetWIPJob(Me.SelectedRequest, wipRow)
                If wipRows = 1 AndAlso wipRow IsNot Nothing Then
                    Using jf As New JobForm(Me, wipRow)
                        jf.ShowDialog()
                    End Using
                ElseIf wipRows > 1 OrElse (wipRows = 0 AndAlso wipRow IsNot Nothing) Then 'no WipRows, but we have unsynced rows.
                    Using f As activiser.RequestForm = New RequestForm(Me, SelectedRequest, RequestFormTab.Jobs)
                        DoDialog(f)
                    End Using
                Else
                    Terminology.DisplayMessage(Me, MODULENAME, RES_NoWIPJobs, MessageBoxIcon.Asterisk)
                End If
                Cursor.Current = Cursors.WaitCursor

            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally

        End Try

    End Sub
End Class
