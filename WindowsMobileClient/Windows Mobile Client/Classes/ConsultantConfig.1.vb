Imports activiser.Library.WebService
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.WebService.ConsultantItemDataSet
Imports activiser.Library.WebService.ConsultantSettingDataSet

Public NotInheritable Class ConsultantConfig
    Private Const MODULENAME As String = "ConsultantConfig"
    Private Const RES_ConsultantSettingsMissing As String = "ConsultantSettingsMissing"
    Private Shared _dsConsultantSettings As New ConsultantSettingDataSet
    Private Shared _dsConsultantItems As New ConsultantItemDataSet
    Private Shared _consultantRow As activiserDataSet.ConsultantRow
    Private Shared _settingsRow As ConsultantSettingRow
    Private Shared _Shortcuts As New Generic.List(Of String)()

    Private Sub New()
    End Sub

    Public Shared Property ConsultantRow() As activiserDataSet.ConsultantRow
        Get
            Return _consultantRow
        End Get
        Set(ByVal value As activiserDataSet.ConsultantRow)
            _consultantRow = value
        End Set
    End Property

    Public Shared Property SettingsRow() As ConsultantSettingRow
        Get
            Return _settingsRow
        End Get
        Set(ByVal value As ConsultantSettingRow)
            _settingsRow = value
        End Set
    End Property

    Public Shared Property ConsultantSettingsDataSet() As ConsultantSettingDataSet
        Get
            Return _dsConsultantSettings
        End Get
        Set(ByVal value As ConsultantSettingDataSet)
            _dsConsultantSettings = value
        End Set
    End Property

    Public Shared Property ConsultantItemDataSet() As ConsultantItemDataSet
        Get
            Return _dsConsultantItems
        End Get
        Set(ByVal value As ConsultantItemDataSet)
            _dsConsultantItems = value
        End Set
    End Property

    'Private Shared drHistoryAge As WebService.ConsultantProfile.ConsultantSettingRow
    'Private Shared drHistoryNumber As WebService.ConsultantProfile.ConsultantSettingRow
    'Private Shared drShortCuts As WebService.ConsultantProfile.ConsultantSettingRow
    'Private Shared ShortcutRows As New Generic.List(Of WebService.ConsultantProfile.ConsultantProfileEntryRow)

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    Public Shared ReadOnly Property Shortcuts() As Generic.List(Of String)
        Get
            Return _Shortcuts
        End Get
    End Property

    ''' I am my device, until I have my own name !
    Public Shared ReadOnly Property Name() As String
        Get
            If _settingsRow IsNot Nothing AndAlso Not String.IsNullOrEmpty(_settingsRow.Name) Then Return _settingsRow.Name
            If _consultantRow IsNot Nothing AndAlso Not String.IsNullOrEmpty(_consultantRow.Name) Then Return _consultantRow.Name
            Return gDeviceID
        End Get
    End Property

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> _
    Public Shared ReadOnly Property UID() As Guid
        Get
            If _settingsRow IsNot Nothing AndAlso Not _settingsRow.ConsultantUid = Guid.Empty Then Return _settingsRow.ConsultantUid
            If _consultantRow IsNot Nothing Then Return _consultantRow.ConsultantUID
            Return Guid.Empty
        End Get
    End Property

    Public Shared Property LastSync() As Date
        Get
            If _consultantRow Is Nothing Then Return #1/1/1900# ' this really indicates a bug, but ignore for now :)
            Return If(_consultantRow.IsLastSyncTimeNull, #1/1/1900#, _consultantRow.LastSyncTime)
        End Get
        Set(ByVal Value As Date)
            If _consultantRow Is Nothing Then Return 'ignore if _consultantRow not yet valid
            _consultantRow.LastSyncTime = Value
            _consultantRow.ModifiedDateTime = DateTime.UtcNow
            _settingsRow.LastSync = Value
        End Set
    End Property

    Private Shared _lastSyncOK As Boolean
    Public Shared Property LastSyncOK() As Boolean
        Get
            Return _lastSyncOK
        End Get
        Set(ByVal value As Boolean)
            _lastSyncOK = value
        End Set
    End Property

    Public Shared Property JobHistoryLimit() As Integer
        Get
            If _settingsRow Is Nothing Then Return 5
            Return _settingsRow.JobHistoryNumber
        End Get
        Set(ByVal Value As Integer)
            If _settingsRow IsNot Nothing Then _settingsRow.JobHistoryNumber = Value
        End Set
    End Property

    Public Shared Property JobHistoryAgeLimit() As Integer
        Get
            If _settingsRow Is Nothing Then Return 183
            Return _settingsRow.JobHistoryAgeLimit
        End Get
        Set(ByVal Value As Integer)
            If _settingsRow IsNot Nothing Then _settingsRow.JobHistoryAgeLimit = Value
        End Set
    End Property

    Private Shared Function GetItem(ByVal Type As String, ByVal UID As Guid) As ConsultantItemRow
        Try
            For Each ci As ConsultantItemRow In _dsConsultantItems.ConsultantItem
                If ci.RowState = DataRowState.Deleted Then Continue For
                If ci.EntityId = UID AndAlso ci.Entity = Type Then Return ci
            Next
        Catch ex As Exception
            LogError(MODULENAME, "GetItem", ex, False, "GetItemFailed")
        End Try

        Return Nothing
    End Function

    Public Shared Sub AddItem(ByVal entityName As String, ByVal entityId As Guid, ByVal modified As DateTime)
        Dim cir As ConsultantItemRow = GetItem(entityName, entityId)
        If cir Is Nothing Then
            cir = _dsConsultantItems.ConsultantItem.AddConsultantItemRow(Guid.NewGuid, ConsultantConfig.UID, gDeviceID, entityName, entityId, 0, DateTime.UtcNow, Name, modified, Name)
        End If
        If cir.Status <> 0 Then cir.Status = 0
    End Sub

    Public Shared Sub RemoveItem(ByVal entityName As String, ByVal entityId As Guid)
        Dim dr As ConsultantItemRow
        dr = GetItem(entityName, entityId)
        If Not dr Is Nothing Then
            dr.Delete()
        Else
            'If Request does not exist, add it and then remove it (to force the status change)
            AddItem(entityName, entityId, DateTime.UtcNow)
            RemoveItem(entityName, entityId)
        End If
    End Sub

    'Public Shared Sub AddRequest(ByVal request As RequestRow)
    '    If GetItem("Request", request.RequestUID) Is Nothing Then
    '        _dsConsultantItems.ConsultantItem.AddConsultantItemRow(Guid.NewGuid, ConsultantConfig.UID, gDeviceID, "Request", request.RequestUID, 0, DateTime.UtcNow, Name, request.ModifiedDateTime, Name)
    '    End If
    'End Sub

    'Public Shared Sub RemoveRequest(ByVal request As RequestRow)
    '    Dim dr As ConsultantItemRow
    '    dr = GetItem("Request", request.RequestUID)
    '    If Not dr Is Nothing Then
    '        dr.Delete()
    '    Else
    '        'If Request does not exist, add it and then remove it (to force the status change)
    '        AddRequest(request)
    '        RemoveRequest(request)
    '    End If
    'End Sub

    '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="0#")> _
    '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId:="0#")> _
    'Public Shared Sub RemoveJob(ByVal job As JobRow)
    '    Dim dr As ConsultantItemRow
    '    dr = GetItem("Job", job.JobUID)
    '    If Not dr Is Nothing Then
    '        dr.Delete()
    '    Else
    '        'If Job does not exist, add it and then remove it
    '        AddJob(job)
    '        RemoveJob(job)
    '    End If
    'End Sub

    '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="0#")> _
    '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId:="0#")> _
    'Public Shared Sub AddJob(ByVal job As JobRow)
    '    If GetItem("Job", job.JobUID) Is Nothing Then
    '        _dsConsultantItems.ConsultantItem.AddConsultantItemRow(Guid.NewGuid, ConsultantConfig.UID, gDeviceID, "Job", job.JobUID, 0, DateTime.UtcNow, Name, job.ModifiedDateTime, Name)
    '    End If
    'End Sub

    Public Shared Sub UpdateShortcuts(ByVal value As String)
        Dim newShortCuts As New Generic.List(Of String)
        newShortCuts.AddRange(value.Split(vbNewLine.ToCharArray()))

        Dim removedShortCuts As New Generic.List(Of String) ' = From s As String In _Shortcuts Where Not newShortCuts.Contains(s) Select s
        For Each s As String In _Shortcuts
            If s.Trim.Length = 0 Then Continue For
            If Not newShortCuts.Contains(s) Then
                removedShortCuts.Add(s)
            End If
        Next

        For Each sc As String In removedShortCuts
            _Shortcuts.Remove(sc)
        Next

        Dim addedShortCuts As New Generic.List(Of String)
        '' = From s As String In newShortCuts Where Not _Shortcuts.Contains(s) Select s

        For Each s As String In newShortCuts
            If s.Trim.Length = 0 Then Continue For
            If Not _Shortcuts.Contains(s) Then
                addedShortCuts.Add(s)
            End If
        Next

        For Each sc As String In addedShortCuts
            _Shortcuts.Add(sc)
        Next

        ' sort
        _Shortcuts.Sort()
        _settingsRow.Shortcuts = String.Join(vbCrLf, _Shortcuts.ToArray())

    End Sub

    'Private Shared Sub LoadDataSets()
    '    Const METHODNAME As String = "LoadDataSet"
    '    Try


    '    Catch ex As Exception
    '        Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
    '        LogError(MODULENAME, METHODNAME, ex, True, Nothing)
    '    End Try
    'End Sub

    Private Shared Sub LoadShortcuts()
        _Shortcuts.Clear()
        If _settingsRow Is Nothing OrElse _settingsRow.IsShortcutsNull Then
            Return
        End If

        Dim newShortCuts As New Generic.List(Of String)
        newShortCuts.AddRange(_settingsRow.Shortcuts.Split(vbNewLine.ToCharArray()))
        For Each s As String In newShortCuts
            If Not String.IsNullOrEmpty(s) Then
                _Shortcuts.Add(s)
            End If
        Next
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
Public Shared Sub Load()
        Const METHODNAME As String = "Load"
        Try
            _settingsRow = Nothing
            _dsConsultantItems.Clear()
            _dsConsultantSettings.Clear()

            LoadDataSet(_dsConsultantSettings, gConfigDbFileName)
            LoadDataSet(_dsConsultantItems, gLocalItemsFileName)

            If _dsConsultantSettings.ConsultantSetting.Count <> 0 Then
                _settingsRow = CType(_dsConsultantSettings.ConsultantSetting.Rows(0), ConsultantSettingRow) ' pick first row in dataset !
                LoadShortcuts()
            Else
                If _consultantRow IsNot Nothing Then ' ?
                    _settingsRow = _dsConsultantSettings.ConsultantSetting.AddConsultantSettingRow(Guid.NewGuid, _
                            _consultantRow.ConsultantUID, gDeviceID, Name, 5, 183, Nothing, 0, DateTime.MinValue, DateTime.UtcNow, Name, DateTime.UtcNow, Name)
                Else
                    LogError(MODULENAME, METHODNAME, Nothing, False, RES_ConsultantSettingsMissing)
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Public Shared Sub SavePending()
        Const METHODNAME As String = "SavePending"
        Try
            Serialisation.SavePending(_dsConsultantSettings, gConfigDbFileName)
            Serialisation.SavePending(_dsConsultantItems, gLocalItemsFileName)
            
        Catch ex As Exception
            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Public Shared Sub SaveFull()
        Const METHODNAME As String = "SaveFull"
        Try
            _dsConsultantSettings.AcceptChanges()
            _dsConsultantItems.AcceptChanges()
            SaveCommitted(_dsConsultantSettings, gConfigDbFileName)
            SaveCommitted(_dsConsultantItems, gLocalItemsFileName)
            Serialisation.SavePending(_dsConsultantSettings, gConfigDbFileName)
            Serialisation.SavePending(_dsConsultantItems, gLocalItemsFileName)
            
        Catch ex As Exception
            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub
    'TODO: Add client and custom data.
    Public Shared Sub UpdateProfile()
        For Each dr As RequestRow In gClientDataSet.Request.Rows
            If Not dr.RowState = DataRowState.Deleted Then
                AddItem("Request", dr.RequestUID, dr.ModifiedDateTime)
            End If
        Next
        For Each dr As JobRow In gClientDataSet.Job.Rows
            If Not dr.RowState = DataRowState.Deleted Then
                AddItem("Job", dr.JobUID, dr.ModifiedDateTime)
            End If
        Next
    End Sub
End Class
