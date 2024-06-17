Option Strict Off

Imports System
Imports System.Threading
Imports System.ComponentModel
Imports activiser.Library
Imports activiser.Library.activiserWebService

Public Class ConsoleData
    '    Inherits Component
    Implements IDisposable
    Implements ISupportInitialize
    Const STR_ModifiedDateTime As String = "ModifiedDateTime"

#Region "Instance"
    Public Sub New()
        Listeners.Add(Me)
    End Sub

    Public Sub New(ByVal ownerName As String)
        Me.OwnerName = ownerName
        Listeners.Add(Me)
    End Sub

    Private _ownerName As String = String.Empty
    Public Property OwnerName() As String
        Get
            Return _ownerName
        End Get
        Set(ByVal value As String)
            _ownerName = value
        End Set
    End Property

    Private Sub RaiseRefreshingEvent(ByVal e As System.EventArgs)
        RaiseEvent Refreshing(Me, e)
    End Sub

    Private Sub RaiseRefreshedEvent(ByVal e As ConsoleDataRefreshEventArgs)
        RaiseEvent Refreshed(Me, e)
    End Sub

#Region "IDisposable Support "
    Private disposedValue As Boolean         ' To detect redundant calls

    ' IDisposable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")> _
    Protected Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Listeners.Remove(Me)
                ' TODO: free unmanaged resources when explicitly called
            Else
                MyBase.Finalize()
            End If

            ' TODO: free shared unmanaged resources
            Me.Dispose(disposing)
        End If
        Me.disposedValue = True
    End Sub


    ' This code added by Visual Basic to correctly implement the disposable pattern.
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")> _
    Public Sub Dispose() Implements System.IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
        Me.Dispose()
    End Sub
#End Region

    Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit

    End Sub

    Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        ConsoleData.Notify = My.Settings.NotificationEnabled
    End Sub

#End Region

#Region "Shared"


    Shared Sub New()
        'NotificationTimer.Interval = 1000
        'NotificationTimer.AutoReset = False
        _WebService.UserAgent = "activiser Console " & My.Application.Info.Version.ToString(4)
        _WebService.PreAuthenticate = True
        _WebService.Credentials = New Net.NetworkCredential()
        If Not String.IsNullOrEmpty(My.Settings.proxyServerUrl) Then
            Dim proxyUrl As New Uri(My.Settings.proxyServerUrl)
            _WebService.Proxy = New Net.WebProxy(proxyUrl, False)
            If My.Settings.proxyServerAuthenticate Then
                _WebService.Proxy.Credentials = _WebService.Credentials
            End If
        Else
            _WebService.Proxy = New Net.WebProxy()
        End If

    End Sub

    Public Event Refreshing As EventHandler
    Public Event Refreshed As EventHandler(Of ConsoleDataRefreshEventArgs)

    Private Shared WithEvents _WebService As New Library.activiserWebService.activiser
    Private Shared WithEvents _CoreDataSet As New activiserDataSet
    Private Shared WithEvents _FormDefs As New FormDefinition
    Private Shared WithEvents _SyncLogDataSet As New SyncLogDataSet
    Private Shared WithEvents _EventDataSet As New EventLogDataSet

    Private Shared WithEvents PollTimer As New Timers.Timer()

    Private Shared lastRefresh As DateTime = DateTime.UtcNow
    Private Shared Listeners As New List(Of ConsoleData)

    Friend Shared MainForm As MainForm

    Public Shared ReadOnly Property WebService() As Library.activiserWebService.activiser
        Get
            Return _WebService
        End Get
    End Property

    Public Shared ReadOnly Property CoreDataSet() As activiserDataSet
        Get
            Return _CoreDataSet
        End Get
    End Property

    Public Shared ReadOnly Property FormDefs() As FormDefinition
        Get
            Return _FormDefs
        End Get
    End Property

    Public Shared ReadOnly Property EventDataSet() As EventLogDataSet
        Get
            Return _EventDataSet
        End Get
    End Property

    Public Shared ReadOnly Property SyncLogDataSet() As SyncLogDataSet
        Get
            Return _SyncLogDataSet
        End Get
    End Property

    Private Shared _pollInterval As Integer = 30
    Public Shared Property PollInterval() As Integer
        Get
            Return _pollInterval
        End Get
        Set(ByVal value As Integer)
            _pollInterval = value

            PollTimer.Interval = _pollInterval * 1000
            PollTimer.AutoReset = Polling
            PollTimer.Enabled = Polling

            My.Settings.PollTimerInterval = _pollInterval
            My.Settings.Save()
        End Set
    End Property

    Private Shared initialLoadDataSet As activiserDataSet
    Private Shared _initialLoadInProgress As Boolean
    Private Shared _initialLoadCancelled As Boolean
    Friend Shared Property InitialLoadCancelled() As Boolean
        Get
            Return _initialLoadCancelled
        End Get
        Set(ByVal value As Boolean)
            _initialLoadCancelled = value
        End Set
    End Property

    Private Shared Sub _WebService_ConsoleGetDataSetCompleted(ByVal sender As Object, ByVal e As ConsoleGetDataSetCompletedEventArgs) Handles _WebService.ConsoleGetDataSetCompleted
        _initialLoadInProgress = False
        If Not e.Cancelled AndAlso (e.Error Is Nothing) Then
            initialLoadDataSet = e.Result
        Else
            If e.Error IsNot Nothing Then
                Throw e.Error
            End If

            InitialLoadCancelled = True
        End If
    End Sub

    Private Shared Sub SetDefaultValuesForCustomDataTables()
        For Each cfr As FormDefinition.FormRow In FormDefs.Form ' CoreDataSet.CustomForm
            If CoreDataSet.Tables.Contains(cfr.EntityName) Then '.TableName) Then
                For Each ccr As FormDefinition.FormFieldRow In cfr.GetFormFieldRows
                    Dim dt As DataTable = CoreDataSet.Tables(cfr.EntityName)
                    For Each dc As DataColumn In dt.Columns
                        If ccr.AttributeName = dc.ColumnName Then
                            ' found it
                            If Not dc.AllowDBNull Then
                                If dc.DataType Is GetType(Guid) Then
                                    Exit For
                                End If
                                Select Case ccr.FieldType
                                    Case 1  ' string
                                        dc.DefaultValue = ""
                                    Case 2  ' check box
                                        dc.DefaultValue = 0
                                    Case 3, 4, 5 ' date/time
                                        dc.DefaultValue = DateTime.MinValue
                                    Case 6 'number
                                        dc.DefaultValue = ccr.MinimumValue
                                    Case 7 'list
                                        dc.DefaultValue = ccr.MinimumValue
                                End Select
                            End If
                            Exit For
                        End If
                    Next
                Next
            End If
        Next
    End Sub

    'Private Shared Sub LoadDataAddExtensionColumns()
    '    Dim dc As DataColumn
    '    dc = CoreDataSet.Request.Columns.Add("AssignedToName", GetType(String), "Parent(FK_Request_Consultant).Name")
    '    dc.AllowDBNull = True

    '    dc = CoreDataSet.Request.Columns.Add("SiteName", GetType(String), "Parent(FK_Request_ClientSite).SiteName")
    '    dc.AllowDBNull = True
    'End Sub

    Public Shared Sub LoadData()
        Try
            If InitialLoadCancelled Then Return
            lastRefresh = DateTime.UtcNow
            Terminology.Load(WebService.GetTerminology(deviceId, ConsoleUser.ConsultantUID, SchemaClientMask.Console, My.Settings.LanguageId, DateTime.MinValue))

            '_WebService.ConsoleGetDataSetAsync(deviceId, ApplicationGuid)

            If My.Settings.UseSynchronousDataLoad Then
                initialLoadDataSet = _WebService.ConsoleGetDataSet(deviceId, ConsoleUser.ConsultantUID)
            Else
                _WebService.ConsoleGetDataSetAsync(deviceId, ConsoleUser.ConsultantUID)
                _initialLoadInProgress = True
                'initialLoadDataSet = _WebService.ConsoleGetDataSet(DeviceId, ConsoleUser.ConsultantUID)
                Do While _initialLoadInProgress 'initialLoadDataSet Is Nothing
                    Threading.Thread.Sleep(20)
                    Application.DoEvents()
                    If InitialLoadCancelled Then
                        Return
                    End If
                Loop
            End If


            CoreDataSet.Merge(initialLoadDataSet, True, MissingSchemaAction.AddWithKey)
            CoreDataSet.AcceptChanges()
            CoreDataSet.EnforceConstraints = False
            'LoadDataAddExtensionColumns()

            ' set default values for custom data tables.
            SetDefaultValuesForCustomDataTables()

            CoreDataSet.EnforceConstraints = True

            _FormDefs.Merge(WebService.GetFormDefinitionsMasked(deviceId, ConsoleUser.ConsultantUID, SchemaClientMask.Console, Nothing))

            Try
                EventDataSet.Merge(WebService.ConsoleGetEventLog(deviceId, ConsoleUser.ConsultantUID, DateTime.Today.AddDays(-7).Date.ToUniversalTime))
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Dim ed As New activiser.Library.DisplayException(ex, My.Resources.ConsoleDataInitialDataLoadFailureTemplate, My.Resources.activiserFormTitle, Library.Icons.Exclamation)
            ed.Show()
            'MsgBox(String.Format(My.Resources.ConsoleDataInitialDataLoadFailureTemplate, ex.Message))
        Finally
            If Not initialLoadDataSet Is Nothing Then
                initialLoadDataSet.Dispose()
                initialLoadDataSet = Nothing
            End If
        End Try
    End Sub

    Private Shared _polling As Boolean
    Public Shared Property Polling() As Boolean
        Get
            Return _polling
        End Get
        Set(ByVal value As Boolean)
            _polling = value
            Try
                MainForm.SetAutoRefreshText(value)
                PollTimer.AutoReset = value
                If value Then
                    PollTimer.Start()
                Else
                    PollTimer.Stop()
                End If
            Catch ex As Exception
                TraceError("Error setting polling status: {0}", ex.Message)
            End Try
        End Set
    End Property

    Public Shared Sub StartPolling()
        Polling = True
    End Sub

    Public Shared Sub StopPolling()
        Polling = False
    End Sub

    Public Shared Sub AbortPolling()
        Polling = False
        WebService.Abort()
        _refreshInProgress = False
    End Sub

    Private Shared _refreshErrorCount As Integer ' = 0
    Private Shared _lastRefreshError As Exception ' = Nothing

    Friend Shared Sub SetLastRefreshError(ByVal lastRefreshError As Exception)
        _lastRefreshError = lastRefreshError
    End Sub

    Friend Shared Sub ClearLastRefreshError()
        _lastRefreshError = Nothing
    End Sub

    Public Shared ReadOnly Property LastRefreshError() As Exception
        Get
            Return _lastRefreshError
        End Get
    End Property

    'Private Sub SetLastRefreshError(ByVal ex As Exception)
    '    _lastRefreshError = ex
    'End Sub


    Public Shared Function DataSetHasData(Of T As DataSet)(ByVal ds As T) As Integer
        If ds Is Nothing Then Return 0
        If TraceLevel.TraceVerbose Then
            TraceVerbose("Checking dataset {0} for data", ds.DataSetName)
            Trace.Indent()
        End If


        Dim result As Integer
        For Each dt As DataTable In ds.Tables
            If dt.Rows.Count <> 0 Then
                TraceVerbose("Datatable {0} has {1} rows", dt.TableName, dt.Rows.Count)
                result += dt.Rows.Count
            End If
        Next
        If TraceLevel.TraceVerbose Then
            TraceVerbose("Dataset {0} has a total of {1} rows in {2} tables", ds.DataSetName, result, ds.Tables.Count)
            Trace.Unindent()
        End If
        Return result
    End Function

    Private Shared _refreshInProgress As Boolean
    'Private Shared _needToTerminate As Boolean

    Public Shared ReadOnly Property RefreshInProgress() As Boolean
        Get
            Return _refreshInProgress
        End Get
    End Property

    Private Shared Function GetDataForUpload() As activiserDataSet
        TraceInfo("Entered")
        Trace.Indent()
        Dim dsChanges As activiserDataSet = TryCast(CoreDataSet.GetChanges(DataRowState.Added Or DataRowState.Deleted Or DataRowState.Modified), activiserDataSet)
        Dim updatedRowCount As Integer
        If dsChanges IsNot Nothing Then
            If TraceLevel.TraceVerbose Then
                TraceVerbose("Checking changes are valid")
                Trace.Indent()
            End If

            dsChanges.EnforceConstraints = False ' allow removal of FK parent rows.
            Dim utcNow As DateTime = DateTime.UtcNow '.AddMinutes(1) ' set time forward a little as a buffer for simultaneous sync's
            'If dsChanges.EventLog IsNot Nothing AndAlso dsChanges.EventLog.Count > 0 Then
            '    TraceVerbose("Flushing event log data")
            '    dsChanges.EventLog.AcceptChanges()
            '    dsChanges.EventLog.Clear()
            'End If
            For Each dt As DataTable In dsChanges.Tables
                For Each dr As DataRow In dt.Select()
                    Dim drReallyHasChanges As Boolean = DataRowHasChanges(dr)
                    If Not drReallyHasChanges Then
                        'If dt.Rows.Contains(dr(dr.Table.PrimaryKey(0))) Then
                        'End If
                        Try
                            TraceVerbose("Data row in table '{0}' rejected - Primary Key value = [ {1} ]", dt.TableName, dr(dt.PrimaryKey(0)).ToString())
                            dr.RejectChanges()
                            dt.Rows.Remove(dr)
                        Catch ex As Exception
                            TraceWarning("Error removing rejected row in table '{0}' - Primary Key value = [ {1} ], exception message = {2}", dt.TableName, dr(dt.PrimaryKey(0)).ToString(), ex.ToString())
                        End Try
                    Else
                        TraceVerbose("Data row in table '{0}' modified - Primary Key value = [ {1} ], row ModifiedDateTime set to {2}", dt.TableName, dr(dt.PrimaryKey(0)).ToString(), utcNow.ToString("s"))
                        'If TraceLevel.TraceVerbose Then Trace.WriteLine(String.Format("{0} : {1} : {2} : Data row in table '{3}' modified - Primary Key value = [ {4} ], row ModifiedDateTime set to {5}", DateTime.Now.ToString(traceTimeStampFormat), GetType(ConsoleData).FullName, MethodBase.GetCurrentMethod.Name, dt.TableName, dr(dt.PrimaryKey(0)).ToString(), utcNow.ToString("s")))
                        dr(STR_ModifiedDateTime) = utcNow
                    End If
                Next
                updatedRowCount += dt.Rows.Count
            Next
            Trace.Unindent()
        End If
        Trace.Unindent()
        If updatedRowCount <> 0 Then
            TraceInfo("Returning changes")
            Return dsChanges
        Else
            TraceInfo("Returning nothing")
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' When automatic polling is disabled, we still need the occasional poll because we've changed
    ''' something. Need refresh will start the timer with the automatic restart disabled if the
    ''' timer wasn't already running.
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Shared Sub NeedRefresh()
        TraceVerbose("")
        'If Not MainForm.ShowRefreshNeeded Then
        MainForm.SetControlVisible(MainForm.StatusBarRefreshNeededLabel, True)
        'MainForm.ShowRefreshNeeded = True
        'End If
    End Sub

    Private Shared Sub PollTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles PollTimer.Elapsed
        '_needRefresh = True
        StartRefresh()
    End Sub

    Public Shared Sub StartRefresh()
        If _refreshInProgress OrElse EditInProgress OrElse _uploadInProgress Then Return

        Dim t As New Threading.Thread(AddressOf Refresh)
        t.Name = String.Format("Refresh started at {0}", DateTime.Now)

        TraceInfo("Refresh thread starting; ManagedThreadId = {0}, Name = {1}", t.ManagedThreadId, t.Name)

        Try
            t.Start()
        Catch ex As OutOfMemoryException
            TraceError("Out of memory exception starting refresh thread, exception message : {0}", ex.ToString)
            Throw
        Catch ex As ThreadStateException
            Throw
        Catch ex As InvalidOperationException
            Throw
        End Try
    End Sub

    Private Shared _editors As New Generic.Dictionary(Of String, Boolean)
    Private Shared _editInProgress As Boolean
    Public Shared Property EditInProgress() As Boolean
        Get
            Return _editInProgress
        End Get
        Set(ByVal value As Boolean)
            If _editInProgress <> value Then
                _editInProgress = value
                MainForm.SetEditInProgress(value)
            End If
        End Set
    End Property

    Public Shared Sub SetEditorState(ByVal form As String, ByVal state As Boolean)
        If _editors.ContainsKey(form) Then
            If _editors.Item(form) = state Then Return
            _editors(form) = state
        Else
            _editors.Add(form, state)
        End If
        TraceVerbose("form = {0}, state = {1}", form, state.ToString())

        Dim editState As Boolean = False
        For Each de As Generic.KeyValuePair(Of String, Boolean) In _editors
            If de.Value = True Then
                editState = True
                Exit For
            End If
        Next
        If EditInProgress <> editState Then
            EditInProgress = editState
        End If
    End Sub
    'Private Shared Sub Refresher()
    '    Do
    '        If _needToTerminate Then Return
    '        'Threading.Thread.Sleep(2000)
    '        If _refreshInProgress OrElse EditInProgress Then Continue Do
    '        If _needRefresh Then
    '            Refresh()
    '            _needRefresh = False
    '        End If
    '    Loop
    'End Sub

    Private Shared _uploadInProgress As Boolean
    Public Shared Function UploadChanges() As Boolean
        _uploadInProgress = True
        Try
            Using dsChanges As activiserDataSet = GetDataForUpload()
                If dsChanges IsNot Nothing Then
                    Dim updatedRowCount As Integer
                    For Each dt As DataTable In dsChanges.Tables
                        updatedRowCount += dt.Rows.Count
                    Next
                    If updatedRowCount <> 0 Then
                        TraceInfo("Console Uploading {0} rows to web service....", updatedRowCount)
                        '#If DEBUGUI1 Then
                        '                        'Threading.Thread.Sleep(2000)
                        '#End If
                        _WebService.ConsoleUploadDataSetUpdates(deviceId, ConsoleUser.ConsultantUID, dsChanges)
                        TraceInfo("Console Uploaded {0} rows to web service.", updatedRowCount)
                        'Application.DoEvents()
                        '#If DEBUGUI1 Then
                        '                        'Threading.Thread.Sleep(2000)
                        '#End If
                        '_CoreDataSet.AcceptChanges() ' assume success.
                        TraceInfo("Console _CoreDataSet changes accepted.")
                        '#If DEBUGUI1 Then
                        '                        'Threading.Thread.Sleep(2000)
                        '#End If
                    End If
                End If
                ClearLastRefreshError()
            End Using
            Return True
        Catch ex As Exception
            TraceInfo("Console Data UploadChanges raised an exception at {0}{1}{2}", Now.ToShortTimeString, vbNewLine, ex.ToString)
            If _lastRefreshError IsNot Nothing Then
                If _lastRefreshError.GetType Is ex.GetType Then
                    _refreshErrorCount += 1
                    SetLastRefreshError(ex)
                    If _refreshErrorCount Mod 10 = 0 Then
                        activiser.Library.DisplayException.Show(ex, String.Format("Unable to refresh data ({0} time(s)).", _refreshErrorCount), Library.Icons.Exclamation)
                    End If
                Else
                    If _refreshErrorCount <> 0 Then
                        activiser.Library.DisplayException.Show(ex, String.Format("Unable to refresh data ({0} time(s)).", _refreshErrorCount), Library.Icons.Exclamation)
                    End If
                    _refreshErrorCount = 1
                    SetLastRefreshError(ex)
                End If
            Else
                _refreshErrorCount = 1
                SetLastRefreshError(ex)
            End If
            Return False
        Finally
            _uploadInProgress = False
        End Try
    End Function

    Public Shared Sub Refresh()
        If _refreshInProgress OrElse EditInProgress OrElse _uploadInProgress Then Return
        Try
            _refreshInProgress = True
            TraceInfo("refresh starting")
            'Threading.Thread.Sleep(10000)
            MainForm.SetRefreshProgressBarEnabled(True)

            ' RCP 2007-05-07, moved this to before upload; the upload and corresponding activity on implicit listeners (forms bound to
            ' console data set), was causing refreshes when we're not ready for them.

            TraceInfo("Notifying Listeners that refresh is beginning.")
            Trace.Indent()
            Dim cdRing As New System.EventArgs()
            For Each cd As ConsoleData In Listeners
                TraceVerbose("Notifying listener '{0}'", cd.OwnerName)
#If DEBUGUI1 Then
                'Threading.Thread.Sleep(2000)
#End If
                cd.RaiseRefreshingEvent(cdRing)
            Next
            Trace.Unindent()

            If UploadChanges() Then
                TraceInfo("uploadChanges OK, getting updates from server")
                Dim refreshTime As Date = _WebService.GetServerTime(deviceId)
                Using updateDataSet As SyncLogDataSet = _WebService.ConsoleGetSyncLog(deviceId, ConsoleUser.ConsultantUID, lastRefresh)
                    If updateDataSet.SyncLog.Count <> 0 Then
                        If ConsoleData.Notify Then
                            StartNotifiers(updateDataSet)
                        End If
                        EventDataSet.Merge(updateDataSet)
                        Dim cded As New ConsoleDataRefreshEventArgs(Nothing)
                        For Each cd As ConsoleData In Listeners
                            If cd.OwnerName = SyncLogViewer.MODULENAME Then
                                TraceVerbose("Notifying listener '{0}'", cd.OwnerName)
                                cd.RaiseRefreshedEvent(cded)
                            End If
                        Next
                    End If
                End Using

                If lastRefresh = #12:00:00 AM# Then ' trap a funny where lastRefresh gets reset
                    lastRefresh = DateTime.Today
                End If
                Using updateDataSet As activiserDataSet = _WebService.ConsoleGetDataSetUpdates(deviceId, ConsoleUser.ConsultantUID, lastRefresh.AddMinutes(-My.Settings.SyncOverlapBuffer))
                    If updateDataSet IsNot Nothing Then 'AndAlso DataSetHasData(updateDataSet) <> 0 Then '
                        Dim cded As New ConsoleDataRefreshEventArgs(updateDataSet) ' System.EventArgs ' ConsoleDataRefreshEventArgs(_UpdateDataSet.EventLog.Count, _UpdateDataSet.Consultant.Count, _UpdateDataSet.ClientSiteStatus.Count, _UpdateDataSet.ClientSite.Count, _UpdateDataSet.RequestStatus.Count, _UpdateDataSet.Request.Count, _UpdateDataSet.JobStatus.Count, _UpdateDataSet.Job.Count, 0)
                        TraceInfo("got updates")
                        Try
                            CoreDataSet.Merge(updateDataSet, False, MissingSchemaAction.AddWithKey)
                            CoreDataSet.AcceptChanges()

                            TraceInfo("Notifying Listeners that refresh is complete.")
                            Trace.Indent()
                            For Each cd As ConsoleData In Listeners
                                TraceVerbose("Notifying listener '{0}'", cd.OwnerName)
                                cd.RaiseRefreshedEvent(cded)
                            Next
                            Trace.Unindent()

                        Catch ex As Exception
                            TraceError(ex)
                            Dim ed As New activiser.Library.DisplayException(ex)
                            ed.Show()
                        Finally
                        End Try

                        TraceInfo("Console Data Refreshed at {0:s}", DateTime.UtcNow)
                    Else
                        TraceInfo("got no updates")
#If DEBUG Then
                        ' when debugging, force form refresh even when the data hasn't changed.
                        Dim cded As New ConsoleDataRefreshEventArgs(Nothing)
                        For Each cd As ConsoleData In Listeners
                            If cd.OwnerName <> SyncLogViewer.MODULENAME Then
                                TraceVerbose("Notifying listener '{0}'", cd.OwnerName)
                                cd.RaiseRefreshedEvent(cded)
                            End If
                        Next
#End If
                    End If
                    TraceInfo("turning off refresh required label")
                    MainForm.SetControlVisible(MainForm.StatusBarRefreshNeededLabel, False)
                End Using


                lastRefresh = CType(refreshTime, DateTime)
                TraceInfo("lastRefresh now = {0}", lastRefresh)
            End If

            '_WebService.ConsoleGetDataSetUpdatesAsync(DeviceId, ConsoleUser.ConsultantUID, lastRefresh, refreshTime)
        Catch ex As Exception
            Dim ed As New activiser.Library.DisplayException(ex)
            ed.Show()
            TraceError("Console Data Refresh raised an exception at {0}{1}{2}", Now.ToShortTimeString, vbNewLine, ex.ToString)
            If _lastRefreshError IsNot Nothing Then
                If _lastRefreshError.GetType Is ex.GetType Then
                    _refreshErrorCount += 1
                    SetLastRefreshError(ex)
                    If _refreshErrorCount Mod 10 = 0 Then
                        Try
                            activiser.Library.DisplayException.Show(ex, String.Format("Unable to refresh data ({0} time(s)).", _refreshErrorCount), Library.Icons.Exclamation)
                        Catch ex2 As Exception
                            TraceError("{0}: Error displaying exception: {1}{2}", Now.ToShortTimeString, vbNewLine, ex.ToString)
                        End Try
                    End If
                Else
                    _refreshErrorCount = 0
                    SetLastRefreshError(ex)
                End If
            Else
                SetLastRefreshError(ex)
            End If
        Finally
            ' reset refresh in progress, otherwise one wee error will stop refreshes.
            TraceInfo("refresh done")
            _refreshInProgress = False
            MainForm.SetRefreshProgressBarEnabled(False)
        End Try
    End Sub
#End Region

#Region "notification"
    Private Shared _notify As Boolean = True
    <DefaultValue(True)> _
    Public Shared Property Notify() As Boolean
        Get
            Return _notify
        End Get
        Set(ByVal value As Boolean)
            _notify = value
            My.Settings.NotificationEnabled = value
        End Set
    End Property

    Private Shared notificationDisplayList As New List(Of SyncNotificationDialog)

    Private Delegate Sub ShowNotifierDelegate(ByVal ni As NotificationInfo, ByVal position As Integer)

    Private Shared Sub ShowNotifier(ByVal ni As NotificationInfo, ByVal position As Integer)
        'If InvokeRequired Then
        '    Dim d As New ShowNotifierDelegate(AddressOf ShowNotifier)
        '    Me.BeginInvoke(d, ni, position)
        '    Return
        'End If

        Dim nd As SyncNotificationDialog = New SyncNotificationDialog(String.Format(My.Settings.NotificationFormat, ni.ConsultantName, ni.SyncTime.ToLocalTime.ToString("t")), position, ni.ConsultantUid)
        notificationDisplayList.Add(nd)
        nd.Show()
    End Sub

    Private Shared Sub Notifier(ByVal newEvents As Object)
        Try
            Dim notificationList As Generic.Dictionary(Of Guid, NotificationInfo) = TryCast(newEvents, Generic.Dictionary(Of Guid, NotificationInfo))
            If notificationList Is Nothing Then Return
            If notificationList.Count = 0 Then Return

            Dim position As Integer = 1

            For Each ni As NotificationInfo In notificationList.Values
                ShowNotifier(ni, position)
                position += 1
            Next

            For Each f As SyncNotificationDialog In notificationDisplayList
                AddHandler f.Disposed, AddressOf Notifier_Disposed
                AddHandler f.LinkClicked, AddressOf Notifier_LinkClicked
            Next

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Shared Sub StartNotifiers(ByVal updateDataSet As SyncLogDataSet)
        Dim nil As New Generic.Dictionary(Of Guid, NotificationInfo)

        Dim rq = From qsl As SyncLogDataSet.SyncLogRow In updateDataSet.SyncLog.Rows Where qsl.SyncDateTime > lastRefresh _
                 Order By qsl.ConsultantUid, qsl.SyncDateTime _
                 Select qsl.ConsultantUid, qsl.SyncDateTime, qsl.Message


        For Each sr In rq ' updateDataSet.SyncLog
            ' note: the 'sync' process refreshes using an 'overlap' on the refresh, as a hack
            ' to reduce the risk of missed records.
            ' so that we don't get duplicate data, we ignore the data we've already seen.
            If sr.SyncDateTime > lastRefresh Then
                If Not nil.ContainsKey(sr.ConsultantUid) Then
                    Dim cr As activiserDataSet.ConsultantRow = ConsoleData.CoreDataSet.Consultant.FindByConsultantUID(sr.ConsultantUid)
                    If cr IsNot Nothing Then
                        Dim ni As New NotificationInfo(sr.ConsultantUid, cr.Name, If(cr.IsSyncTimeNull, DateTime.UtcNow, cr.SyncTime))
                        nil.Add(ni.ConsultantUid, ni)
                    End If
                End If
            End If
        Next
        Dim t As New Thread(New ParameterizedThreadStart(AddressOf Notifier))
        t.Name = String.Format("Notification display thread started @ {0}", DateTime.Now.ToString("s"))
        t.Priority = ThreadPriority.BelowNormal
        t.Start(nil)
    End Sub


    Private Shared Sub Notifier_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim nd As SyncNotificationDialog = TryCast(sender, SyncNotificationDialog)
        If nd Is Nothing Then Return
        notificationDisplayList.Remove(nd)
        nd = Nothing
    End Sub

    Private Shared Sub Notifier_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim consultantUid As Guid = New Guid(e.Link.LinkData.ToString())
            MainForm.FindFirstJobByEngineer(consultantUid)
        Catch ex As FormatException
            ' not a guid ?!?!
        End Try
    End Sub

#End Region

End Class
