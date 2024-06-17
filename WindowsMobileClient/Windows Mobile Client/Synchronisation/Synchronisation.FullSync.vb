Imports activiser.Library
Imports activiser.Library.WebService
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.WebService.FormDefinition
Imports System.Threading
Imports System.Xml
#If WINDOWSMOBILE Then
Imports System.Media
#End If

Partial Friend Class Synchronisation
    Private Const STR_ModifiedDateTime As String = "ModifiedDateTime"
    Private Const STR_CreatedDateTime As String = "CreatedDateTime"

    Public Shared Event Started As EventHandler(Of EventArgs)
    Public Shared Event StateChanged As EventHandler(Of EventArgs)
    Public Shared Event Finished As EventHandler(Of EventArgs)
    Public Shared Event ExceptionRaised As EventHandler(Of ExceptionEventArgs)

    Private Shared _state As String
    Private Shared _stateArgs() As String

    Private Shared _saveThread As Thread

#If FRAMEWORK_VERSION_GE35 Then
    Private Shared Function UseCompression() As Boolean
        Return AppConfig.GetSetting(My.Resources.AppConfigUseCompressionKey, True)
    End Function
#Else
    Private Shared Function UseCompression() As Boolean
        Return False
    End Function
#End If

    Friend Shared Sub SetState(ByVal value As String, ByVal ParamArray args() As String)
        LogSyncMessage(value, args)
        _state = Terminology.GetFormattedString(MODULENAME, value, args).Replace(_retryToken, String.Empty)
        _stateArgs = args
        Notifier.Text = _state
        RaiseEvent StateChanged(Threading.Thread.CurrentThread, New EventArgs())
    End Sub

    Public Shared ReadOnly Property State() As String
        Get
            Return _state
        End Get
    End Property

    Friend Shared Function SyncFull() As Boolean
        Const METHODNAME As String = "SyncFull"

        If gSyncInProgress Then Return False
        gSyncInProgress = True
        gHoldAutoSync = True
        gCancelSync = False
        Dim timerOn As Boolean = SyncTimer.Enabled

        SyncTimer.Enabled = False

        Dim hasChecksumError As Boolean

        initSounds()

        Try
            _startedAt = DateTime.Now
            gSyncLog.Clear()
            SetState(RES_SyncStarting)

            RaiseEvent Started(Threading.Thread.CurrentThread, New EventArgs())
            Startup.InitialiseWebServiceProxy(Nothing, True)

            If Authenticate() <> AuthenticationStatus.Ok Then
                Throw New SyncAuthorisationException()
            End If

            If gCancelSync Then Throw New SyncCanceledException()
            SetState(RES_Authenticated)

            Dim syncTime As DateTime = StartSync()

            Dim dsChanges As activiserDataSet = GetDataToUpload(syncTime)

            Dim somethingToUpload As Boolean

            If dsChanges IsNot Nothing Then
                somethingToUpload = True
                UploadChanges(dsChanges, hasChecksumError)
            End If

            Dim schemaUpdated As Boolean
            schemaUpdated = GetSchemaAndTerminologyUpdates()

            Dim dsReturnDataset As DataSet
            Dim dsReturnClientDataset As activiserDataSet = Nothing

            dsReturnDataset = DownloadChanges()

            If dsReturnDataset Is Nothing Then
                SetState(RES_NoDataNote)
            Else
                dsReturnClientDataset = MergeReturnedClientDataset(dsReturnDataset)
                StartProfileUpdateThread()
            End If

            UploadGpsLog()
            UpLoadErrorLog()

            If somethingToUpload OrElse schemaUpdated OrElse dsReturnDataset IsNot Nothing Then
                SetState(RES_Saving)
                gClientDataSet.AcceptChanges()
                _saveThread = New Threading.Thread(AddressOf SaveMainDatabase)
                _saveThread.Name = "Syncfull main database save @ " & DateTime.Now.ToString("o")
                _saveThread.Start()

            End If

            ConsultantConfig.LastSync = gWebServer.SyncComplete(gDeviceIDString, gConsultantUID)

            UploadProfile()

            gMainForm.SetSyncColors(System.Drawing.SystemColors.ActiveCaptionText, System.Drawing.SystemColors.ActiveCaption)
            gSyncsMissed = 0

            If dsReturnClientDataset IsNot Nothing AndAlso dsReturnClientDataset.RequestStatus.Count <> 0 Then
                gMainForm.InitializeRequestList()
                gMainForm.PopulateRequestMenu()
                gMainForm.Invoke(New SimpleSubDelegate(AddressOf gMainForm.SendToBack))
            End If

            If _saveThread IsNot Nothing Then
                SetState(RES_SaveCompleting)
                If Not _saveThread.Join(30000) Then
                    ' TODO: Log this.
                End If
            End If
            'WaitForSaveCommittedCompletion(gClientDataSet)
            'WaitForSavePendingCompletion(gClientDataSet)

            If hasChecksumError Then Return False

            ConsultantConfig.LastSyncOK = True

            SetState(RES_SyncComplete)
            Return True

        Catch ex As SyncCanceledException
            SetState(RES_Cancelled)
            RaiseEvent ExceptionRaised(Threading.Thread.CurrentThread, New ExceptionEventArgs(ex))
            Return False

        Catch ex As NoChecksumDataException
            SetState(RES_CompleteWithErrors)
            ConsultantConfig.LastSyncOK = False
            LogSyncMessage(RES_NoChecksumDataException, ex.Message)
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            RaiseEvent ExceptionRaised(Threading.Thread.CurrentThread, New ExceptionEventArgs(ex))
            Return False

        Catch ex As System.Net.WebException
            SetState(RES_CompleteWithErrors)
            ConsultantConfig.LastSyncOK = False
            LogSyncMessage(RES_CommsFailureMessage, ex.Message)
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            RaiseEvent ExceptionRaised(Threading.Thread.CurrentThread, New ExceptionEventArgs(ex))
            Return False

        Catch ex As SyncAuthorisationException ' note: authentication process logs its own errors, so we don't re-log them here.
            ConsultantConfig.LastSyncOK = False
            SetState(RES_CompleteWithErrors)
            RaiseEvent ExceptionRaised(Threading.Thread.CurrentThread, New ExceptionEventArgs(ex))
            Return False

        Catch ex As Exception
            ConsultantConfig.LastSyncOK = False
            SetState(RES_CompleteWithErrors)
            LogSyncMessage(RES_UnhandledErrorMessage, ex.Message)
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
            RaiseEvent ExceptionRaised(Threading.Thread.CurrentThread, New ExceptionEventArgs(ex))
            Return False

        Finally
            RaiseEvent Finished(Threading.Thread.CurrentThread, New EventArgs())

            gSyncLog.AddBreak()
            Try
                ' reset forced sync time
                If Not String.IsNullOrEmpty(AppConfig.GetSetting(My.Resources.AppConfigForceLastSyncDateTimeKey, String.Empty)) Then
                    AppConfig.SaveSetting(My.Resources.AppConfigForceLastSyncDateTimeKey, String.Empty)
                End If
            Catch ' ignore errors here.
            End Try

            ' arguably, this should be set at the _start_ of the sync process, but it's used for the auto-sync interval
            ' doing it this way means we wait the defined interval after the end of an attempt. This should avoid a never-ending sync
            ' situation if someone makes the sync interval shorter than a typical sync takes to complete.
            gLastSyncAttempt = DateTime.UtcNow

            gSyncInProgress = False
            gHoldAutoSync = False

            If AppConfig.GetSetting("PlaySyncCompleteSound", True) Then
                If _syncCompleteSound IsNot Nothing Then
                    _syncCompleteSound.Play()
                End If
            End If

            SyncTimer.Enabled = timerOn
            If AutoSyncTimer IsNot Nothing Then
                AutoSyncTimer.Enabled = gAutoSync
            End If
        End Try
    End Function

    Private Shared Function SaveMainDatabase() As Boolean
        SaveCommitted(gClientDataSet, gMainDbFileName)
        SavePending(gClientDataSet, gMainDbFileName)
    End Function

    Friend Shared Function UploadClientDataSetUpdates(ByRef dsChanges As activiserDataSet) As UploadResults

#If FRAMEWORK_VERSION_GE35 Then
        If UseCompression() Then
            Dim compressedClientData() As Byte = CompressString(dsChanges.GetXml)
            Dim compressedResults() As Byte = gWebServer.UploadClientDataSetUpdatesCompressed(gDeviceIDString, gConsultantUID.ToString(), compressedClientData)
            Dim uncompressedResults As String = DecompressString(compressedResults)

            Dim resultsXml As New System.Xml.XmlDocument
            resultsXml.LoadXml(uncompressedResults)
            Dim results As UploadResults = DeSerializeDataSetFromXmlDoc(Of UploadResults)(resultsXml)
            Return results
        Else
            Dim clientData As New Xml.XmlDocument
            clientData.PreserveWhitespace = True
            clientData.LoadXml(dsChanges.GetXml())
            Dim resultsXml As XmlNode = gWebServer.UploadClientDataSetUpdatesAsXml(gDeviceIDString, gConsultantUID.ToString(), clientData)
            Dim results As UploadResults = DeSerializeDataSetFromXmlDoc(Of UploadResults)(resultsXml)
            Return results
        End If
#Else
        Dim clientData As New Xml.XmlDocument
        clientData.PreserveWhitespace = True
        clientData.LoadXml(dsChanges.GetXml())
        Dim resultsXml As XmlNode = gWebServer.UploadClientDataSetUpdatesAsXml(gDeviceIDString, gConsultantUID.ToString(), clientData)
        Dim results As UploadResults = DeSerializeDataSetFromXmlDoc(Of UploadResults)(resultsXml)
        Return results
#End If
    End Function

    Friend Shared Function GetDataToUpload(ByVal syncTime As DateTime) As activiserDataSet
        Dim result As activiserDataSet
        Try
            SetState(RES_FindingDataToUpload)

            result = TryCast(gClientDataSet.GetChanges(DataRowState.Added Or DataRowState.Modified), activiserDataSet)
            If result IsNot Nothing Then
                result.EnforceConstraints = False
            End If

            AddUpdatedRequests(result)
            AddUpdatedJobs(result)
            AddUpdatedCustomData(result)

            'HACK: (RCP) for some reason, jobs for other consultants are getting 'modified' and uploaded.
            'This forces the removal of other people's jobs.
            'The real fix is to not modify them in the first place, but I don't have time to figure that out right now.
            RemoveForeignJobs(result)
            RemoveExtraData(result)
            SetModifiedTimes(result, syncTime)

            If result IsNot Nothing Then CleanDataSet(result)

            If DataSetIsNullOrEmpty(result) Then
                result = Nothing
            End If
            If gCancelSync Then Throw New SyncCanceledException()

        Catch ex As Exception
            Throw
        Finally

        End Try

        Return result
    End Function

    Private Shared Sub UploadChanges(ByVal dsChanges As activiserDataSet, ByRef hasChecksumError As Boolean)
        SavePending(gClientDataSet, gMainDbFileName)

        Try
            SetState(RES_Uploading)
            dsChanges.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            ' Dim returnDS As WebService.UploadResults = gWebServer.UploadClientDataSetUpdates(gDeviceIDString, gConsultantUID, dsChanges)
            Dim returnDS As WebService.UploadResults = UploadClientDataSetUpdates(dsChanges)
            If returnDS Is Nothing Then
                If gCancelSync Then
                    Return
                Else
                    Throw New NoChecksumDataException
                End If
            Else
                ValidateChecksums(returnDS, hasChecksumError)
            End If

            LogSyncMessage(RES_UploadComplete)
        Catch ex As Exception
            Throw
        Finally
            Debug.WriteLine("UploadChanges Complete")
            If hasChecksumError Then
                SetState(RES_ChecksumErrorMessage)
            End If
        End Try
        If gCancelSync Then Throw New SyncCanceledException()
    End Sub

    Friend Shared Sub CheckForNewRequests(ByVal dsReturnDataset As activiserDataSet)
        Dim newRequestSoundFile As String = IO.Path.Combine(gDatabaseRoot, My.Resources.SoundsNewRequest)
        If Not IO.File.Exists(newRequestSoundFile) Then Return

        Dim haveNewRequests As Boolean = False
        For Each nr As RequestRow In dsReturnDataset.Request
            Dim rs As activiserDataSet.RequestStatusRow = gClientDataSet.RequestStatus.FindByRequestStatusID(nr.RequestStatusID)
            If rs IsNot Nothing Then
                If rs.IsNewStatus Then
                    haveNewRequests = True
                    Exit For
                ElseIf Not gClientDataSet.Request.Rows.Contains(nr.RequestUID) Then
                    haveNewRequests = True
                    Exit For
                End If
            Else
                haveNewRequests = True
                Exit For
            End If
        Next
        If haveNewRequests AndAlso _syncNewRequestSound IsNot Nothing Then
            _syncNewRequestSound.Play()
        End If
    End Sub


    Friend Shared Sub AddUpdatedRequests(ByRef dsChanges As activiserDataSet)
        Dim rrs As New Generic.List(Of RequestRow)

        For Each rr As RequestRow In gClientDataSet.Request
            If rr.RowState <> DataRowState.Deleted Then
#If FRAMEWORK_VERSION_GE35 Then
                If ((rr.RowState And (DataRowState.Added Or DataRowState.Modified)) <> 0) _
                    OrElse (rr.RowState = DataRowState.Unchanged AndAlso rr.ModifiedDateTime > ConsultantConfig.LastSync) _
                    AndAlso rr.HasChanges() Then
#Else
                If ((rr.RowState And (DataRowState.Added Or DataRowState.Modified)) <> 0) _
                    OrElse (rr.RowState = DataRowState.Unchanged AndAlso rr.ModifiedDateTime > ConsultantConfig.LastSync) _
                    AndAlso HasChanges(rr) Then
#End If
                    rrs.Add(rr)
                End If
            End If

        Next

        If rrs.Count = 0 Then Return

        If dsChanges Is Nothing Then dsChanges = CType(gClientDataSet.Clone, activiserDataSet)

        dsChanges.EnforceConstraints = False

        For Each rr As RequestRow In rrs
            If Not dsChanges.Request.Rows.Contains(rr.RequestUID) Then
                dsChanges.Request.ImportRow(rr)
            End If
        Next
    End Sub

    Friend Shared Sub AddUpdatedJobs(ByRef dsChanges As activiserDataSet)
        Dim jrs As New Generic.List(Of JobRow)

        For Each jr As JobRow In gClientDataSet.Job
            If ((jr.RowState And (DataRowState.Added Or DataRowState.Modified)) <> 0) Then
#If FRAMEWORK_VERSION_GE35 Then
                If jr.ConsultantUID = gConsultantUID AndAlso jr.HasChanges() Then
#Else
                If jr.ConsultantUID = gConsultantUID AndAlso HasChanges(jr) Then
#End If
                    jrs.Add(jr)
                End If
            End If
        Next

        If jrs.Count = 0 Then Return

        If dsChanges Is Nothing Then dsChanges = CType(gClientDataSet.Clone, activiserDataSet)

        dsChanges.EnforceConstraints = False

        For Each jr As JobRow In jrs
            If Not dsChanges.Job.Rows.Contains(jr.JobUID) Then
                dsChanges.Job.ImportRow(jr)
            End If
        Next
    End Sub


    'TODO: change to LINQ
    Friend Shared Sub AddUpdatedCustomData(Of T As DataSet)(ByRef dsChanges As T)
        Dim fq As New Generic.List(Of String) '= From fr As FormRow In gFormDefinitions.Form Select fr.EntityName Distinct
        For Each fd As FormRow In gFormDefinitions.Form
            If Not fq.Contains(fd.EntityName) Then fq.Add(fd.EntityName)
        Next

        For Each entityName As String In fq
            Dim srcTable As DataTable = gClientDataSet.Tables(entityName)
            If Not srcTable.Columns.Contains(STR_ModifiedDateTime) Then Continue For
            Dim crq As New Generic.List(Of DataRow) ''Where(CDate(dr("ModifiedDateTime")) >= ConsultantConfig.LastSync)
            For Each dr As DataRow In srcTable.Rows
                If dr.RowState = DataRowState.Added Then
                    If dr.IsNull(STR_ModifiedDateTime) Then dr(STR_ModifiedDateTime) = DateTime.UtcNow
                    If dr.IsNull(STR_CreatedDateTime) Then dr(STR_CreatedDateTime) = DateTime.UtcNow
                    crq.Add(dr)
                Else
#If FRAMEWORK_VERSION_GE35 Then
                    If dr.RowState <> DataRowState.Deleted AndAlso dr.HasChanges() Then
#Else
                    If dr.RowState <> DataRowState.Deleted AndAlso HasChanges(dr) Then
#End If
                        If dr.IsNull(STR_ModifiedDateTime) Then dr(STR_ModifiedDateTime) = DateTime.UtcNow
                        crq.Add(dr)
                    End If
                End If
            Next

            If crq.Count = 0 Then Return

            If dsChanges Is Nothing Then dsChanges = CType(gClientDataSet.Clone, T)

            If Not dsChanges.Tables.Contains(entityName) Then
                dsChanges.Tables.Add(srcTable.Clone)
                dsChanges.EnforceConstraints = False
            End If

            Dim tgtTable As DataTable = dsChanges.Tables(entityName)
            For Each dr As DataRow In crq
                tgtTable.ImportRow(dr)
            Next
        Next
    End Sub

    Friend Shared Sub RemoveForeignJobs(ByVal dsChanges As activiserDataSet)
        Const METHODNAME As String = "RemoveForeignJobs"
        If dsChanges Is Nothing Then Return
        Dim jrs() As JobRow
        Const STR_OtherUserFilter As String = "ConsultantUID <> '{0}'"
        jrs = CType(dsChanges.Job.Select(String.Format(WithoutCulture, STR_OtherUserFilter, gConsultantUID)), JobRow())
        If jrs Is Nothing OrElse jrs.Length = 0 Then
            Return
        End If
        'Application.DoEvents()

        Try
            For Each jr As JobRow In jrs
                If dsChanges.Request.Rows.Contains(jr.RequestUID) Then
                    Dim r As RequestRow = dsChanges.Request.FindByRequestUID(jr.RequestUID)
                    If r IsNot Nothing Then
                        If r.GetJobRows.Length = 0 Then
                            dsChanges.Request.RemoveRequestRow(r)
                        End If
                    End If
                End If
            Next
            For Each jr As JobRow In jrs
                dsChanges.Job.RemoveJobRow(jr)
            Next
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
        'Application.DoEvents()
    End Sub

    Friend Shared Sub RemoveExtraData(ByVal dsChanges As activiserDataSet)
        Const METHODNAME As String = "RemoveExtraData"
        If dsChanges Is Nothing Then Return

        Try
            dsChanges.JobStatus.Clear()
            dsChanges.RequestStatus.Clear()
            dsChanges.ClientSite.Clear()    ' this could be really dangerous.
            dsChanges.ClientSiteStatus.Clear()

            ' because we may be updating the consultant tracking information, we need to include the current user in the upload.
            For Each dr As activiserDataSet.ConsultantRow In dsChanges.Consultant.Select()
                If dr.ConsultantUID <> gConsultantUID Then
                    dsChanges.Consultant.RemoveConsultantRow(dr)
                End If
            Next

            ' clear any lookup tables
            For Each t As DataTable In dsChanges.Tables
                If t.Rows.Count <> 0 AndAlso (t.PrimaryKey.Length <> 0) AndAlso (t.PrimaryKey(0).DataType IsNot GetType(Guid)) Then ' don't allow upload of non-transaction tables; this should get rid of any lookup tables.
                    t.Clear()
                End If
            Next
            'For Each cfr As WebService.FormDefinition.FormRow In gClientDataSet.CustomForm
            '    if 
            'Next

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
        'Application.DoEvents()
    End Sub



    Friend Shared Function LoadExistingErrors() As activiserDataSet
        Dim errorFileName As String = IO.Path.Combine(gErrorFolder, My.Resources.ChecksumErrorXml)
        Dim dsError As New activiserDataSet
        dsError.EnforceConstraints = False
        If New FileInfo(errorFileName).Exists Then
            Try
                dsError.ReadXml(errorFileName)
            Catch ex As Exception
            End Try
        End If
        Return dsError
    End Function

    Private Shared Sub UpLoadErrorLog()
        If gCancelSync Then Return
        Try
            If DataSetIsNullOrEmpty(gEventLog) Then Return
            SetState(RES_UploadingExceptionReports)
            gWebServer.UploadEventLog(gDeviceIDString, gEventLog)
        Catch ex As Exception
            '11.4.2006 Tim: If error log synchronisation fails, nuke the error log
            ' do nothing if error report generates an error !
        Finally
            ' clear the error log
            gEventLog.Clear()
            gEventLog.AcceptChanges()
            'Serialisation.SaveCommitted(gEventLog, gErrorLogDbFileName)
            SavePending(gEventLog, gErrorLogDbFileName)
            ' Dim fileName As String = strErrorLog
            'Dim fn = Path.Combine(gTransactionFolder, gErrorLogDbFileName)
            'If System.IO.File.Exists(fn) Then
            '    Try
            '        System.IO.File.Delete(fn)
            '    Catch ' ex As Exception
            '        ' don't really care if it fails
            '    End Try
            'End If
        End Try
    End Sub


    Friend Shared Function DownloadChanges() As DataSet
        Dim result As DataSet = Nothing
        Try
            SetState(RES_Downloading)
#If FRAMEWORK_VERSION_GE35 Then
            If UseCompression() Then
                Dim updateBytes() As Byte = gWebServer.GetClientDataSetUpdatesCompressed(gDeviceIDString, gConsultantUID.ToString(), ConsultantConfig.LastSync)
                result = DeSerializeDataSetFromCompressedString(gMainDbFileName, updateBytes)
            Else
                Dim updatesXml As XmlNode = gWebServer.GetClientDataSetUpdatesAsXml(gDeviceIDString, gConsultantUID.ToString(), ConsultantConfig.LastSync)
                result = DeSerializeDataSetFromXmlDoc(gMainDbFileName, updatesXml) ' note, if you try and use activiserDataSet here, it will silently ignore all custom data.
            End If
#Else
            Dim updatesXml As XmlNode = gWebServer.GetClientDataSetUpdatesAsXml(gDeviceIDString, gConsultantUID.ToString(), ConsultantConfig.LastSync)
            result = DeSerializeDataSetFromXmlDoc(gMainDbFileName, updatesXml) ' note, if you try and use activiserDataSet here, it will silently ignore all custom data.
#End If


            If DataSetIsNullOrEmpty(result) Then
                result.Dispose()
                result = Nothing
            End If
        Catch ex As Exception
            LogError(MODULENAME, "DownloadChanges", ex, False, Nothing)
        Finally
            Debug.WriteLine("DownloadChanges complete.")
        End Try

        If gCancelSync Then Throw New SyncCanceledException()
        Return result
    End Function

    Private Shared Sub ReloadLookups(ByVal mergedDataSet As activiserDataSet)
        If AppConfig.GetSetting(My.Resources.AppConfigFlushCustomLookupsKey, False) Then
            ' TODO:
        End If
    End Sub

    Private Shared Function MergeReturnedClientDataset(ByVal returnedDataSet As DataSet) As activiserDataSet
        Dim mergedDataSet As activiserDataSet
        SetState(RES_Merging)
        For Each dt As DataTable In gClientDataSet.Tables
            For Each dr As DataRow In dt.Select(Nothing, Nothing, DataViewRowState.Deleted)
                dr.AcceptChanges() ' flush deleted rows
            Next
        Next
        mergedDataSet = New activiserDataSet
        mergedDataSet.Merge(returnedDataSet, False, MissingSchemaAction.AddWithKey)

        ReloadLookups(mergedDataSet)
        gClientDataSet.Merge(mergedDataSet, False, MissingSchemaAction.AddWithKey)
        gClientDataSet.AcceptChanges()
        CheckForNewRequests(mergedDataSet)
        Return mergedDataSet
    End Function

    Private Shared Sub CheckSyncFrequency()
        Dim requiredSyncInterval As Integer = gWebServer.GetConsultantSyncInterval(gDeviceIDString, gConsultantUID)
        AppConfig.SaveSetting(My.Resources.AppConfigSyncIntervalKey, requiredSyncInterval)
        SetSyncInterval(requiredSyncInterval)

        AppConfig.Save()
    End Sub

End Class
