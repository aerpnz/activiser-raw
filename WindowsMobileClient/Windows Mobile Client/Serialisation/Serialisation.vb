Option Compare Text

Imports Microsoft.VisualBasic
Imports System

'Imports WS = activiser.WebService
Imports System.ComponentModel

Module Serialisation
    Private Const MODULENAME As String = "Serialisation"

    'Private _xws As Xml.XmlWriterSettings

    'Sub New()
    '    _xws = New Xml.XmlWriterSettings()
    '    _xws.Indent = True
    '    _xws.NewLineHandling = Xml.NewLineHandling.Replace
    '    _xws.NewLineChars = vbCrLf
    '    _xws.Encoding = System.Text.UTF8Encoding.Unicode
    'End Sub

    'Private backGroundSaveCommitted As New Generic.Dictionary(Of String, AsyncSaveCommitted)

    'Public ReadOnly Property SaveInProgress(ByVal ds As DataSet) As Boolean
    '    Get
    '        Return backGroundSavePending.ContainsKey(ds.DataSetName)
    '    End Get
    'End Property

    'Public Sub StartSaveCommitted(ByVal ds As DataSet, ByVal location As String)
    '    Try
    '        Threading.Monitor.Enter(backGroundSaveCommitted)
    '        If backGroundSaveCommitted.ContainsKey(ds.DataSetName) Then
    '            ' hack to get call stack for debugging purposes.
    '            Try
    '                Throw New ApplicationException("Nested save attempted for dataset '" & ds.DataSetName & "'.")
    '            Catch ex As Exception
    '                LogError(MODULENAME, "StartSaveCommitted", ex, False, Nothing, Nothing)
    '            End Try
    '        Else
    '            Dim saveThread As AsyncSaveCommitted = New AsyncSaveCommitted(ds, location)
    '            backGroundSaveCommitted.Add(ds.DataSetName, saveThread)
    '            saveThread.Start()
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        Threading.Monitor.Exit(backGroundSaveCommitted)
    '    End Try
    'End Sub

    'Public Sub CompleteSaveCommitted(ByVal ds As DataSet)
    '    Try
    '        Threading.Monitor.Enter(backGroundSaveCommitted)
    '        If backGroundSaveCommitted.ContainsKey(ds.DataSetName) Then
    '            backGroundSaveCommitted.Remove(ds.DataSetName)
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        Threading.Monitor.Exit(backGroundSaveCommitted)
    '    End Try
    'End Sub

    'Public Sub WaitForSaveCommittedCompletion(ByVal ds As DataSet)
    '    If backGroundSaveCommitted.ContainsKey(ds.DataSetName) Then
    '        Dim saveThread As AsyncSaveCommitted = backGroundSaveCommitted(ds.DataSetName)
    '        If Not saveThread.Join(30000) Then
    '            LogError(MODULENAME, "WaitForSaveCommittedCompletion", _
    '                     New ApplicationException(String.Format("Error waiting for save thread for dataset '{0}'.", ds.DataSetName)), False, Nothing, Nothing)
    '        End If
    '    End If
    'End Sub

    'Private Class AsyncSaveCommitted
    '    Private _ds As DataSet
    '    Private _location As String
    '    Private _result As Exception
    '    Private t As Threading.Thread

    '    Public Event SaveCompleted As EventHandler

    '    Public Sub New(ByVal ds As DataSet, ByVal location As String)
    '        Me._ds = ds
    '        Me._location = location
    '    End Sub

    '    Public Sub Start()
    '        t = New Threading.Thread(AddressOf Save)
    '        t.Name = String.Format("SaveCommitted of {0} started @ {1:o}", _ds.DataSetName, DateTime.Now)
    '        Debug.WriteLine(String.Format("Starting thread: {0}", t.Name))
    '        t.Start()
    '    End Sub

    '    Private Sub Save()
    '        Try
    '            SaveCommitted(_ds, _location)
    '            _result = Nothing
    '        Catch ex As Exception
    '            _result = ex
    '            LogError(MODULENAME, "BackgroundSaveCompleted", ex, True, RES_ErrorSavingDataSet)
    '        Finally
    '            CompleteSaveCommitted(_ds)
    '            Debug.WriteLine(String.Format("Thread complete: {0}", t.Name))
    '            RaiseEvent SaveCompleted(Me, New System.EventArgs())
    '        End Try
    '    End Sub

    '    Public ReadOnly Property Result() As Exception
    '        Get
    '            Return _result
    '        End Get
    '    End Property

    '    Public ReadOnly Property Thread() As Threading.Thread
    '        Get
    '            Return t
    '        End Get
    '    End Property

    '    Public Function Join(Optional ByVal timeout As Integer = 0) As Boolean
    '        If t IsNot Nothing Then
    '            Return t.Join(If(timeout = 0, Threading.Timeout.Infinite, timeout))
    '        Else
    '            Return True
    '        End If
    '    End Function
    'End Class

    Private saveNeeded As New Generic.Dictionary(Of String, Boolean)
    Private saveInProgress As New Generic.Dictionary(Of String, Boolean)
    Private backGroundSavePending As New Generic.Dictionary(Of String, AsyncSavePending)
    Public Sub StartSavePending(ByVal ds As DataSet, ByVal location As String)
        Threading.Monitor.Enter(backGroundSavePending)
        Threading.Monitor.Enter(saveInProgress)
        Try
            saveNeeded(ds.DataSetName) = True
            If Not saveInProgress.ContainsKey(ds.DataSetName) OrElse (Not saveInProgress(ds.DataSetName) AndAlso Not backGroundSavePending.ContainsKey(ds.DataSetName)) Then
                Dim saveThread As AsyncSavePending = New AsyncSavePending(ds, location)
                backGroundSavePending.Add(ds.DataSetName, saveThread)
                saveThread.Start()
            End If
        Catch ex As Exception
            Throw
        Finally
            Threading.Monitor.Exit(saveInProgress)
            Threading.Monitor.Exit(backGroundSavePending)
        End Try
    End Sub

    'Public Sub CompleteSavePending(ByVal ds As DataSet)
    '    Try
    '        Threading.Monitor.Enter(backGroundSavePending)
    '        backGroundSavePending.Remove(ds.DataSetName) ' silently fails if key not found
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        Threading.Monitor.Exit(backGroundSavePending)
    '    End Try
    'End Sub

    'Public Sub WaitForSavePendingCompletion(ByVal ds As DataSet)
    '    If backGroundSavePending.ContainsKey(ds.DataSetName) Then
    '        Dim saveThread As AsyncSavePending = backGroundSavePending(ds.DataSetName)
    '        If Not saveThread.Join(30000) Then
    '            LogError(MODULENAME, "StartSavePending", _
    '                     New ApplicationException(String.Format("Error waiting for save thread for dataset '{0}'.", ds.DataSetName)), False, Nothing, Nothing)
    '        End If
    '    End If
    'End Sub

    Private Class AsyncSavePending
        Private _ds As DataSet
        Private _location As String
        Private _result As Exception
        Private t As Threading.Thread

        Public Event SaveCompleted As EventHandler

        Public Sub New(ByVal ds As DataSet, ByVal location As String)
            Me._ds = ds
            Me._location = location
        End Sub

        Public Sub Start()
            t = New Threading.Thread(AddressOf Save)
            t.Name = String.Format("SavePending of {0} started @ {1:o}", _ds.DataSetName, DateTime.Now)
            Debug.WriteLine(String.Format("Starting thread: {0}", t.Name))
            t.Start()
        End Sub

        Private Sub Save()
            Try
                SavePending(_ds, _location)
                _result = Nothing
            Catch ex As Exception
                _result = ex
                LogError(MODULENAME, "BackgroundSavePendingCompleted", ex, True, RES_ErrorSavingDataSet)
            Finally
                backGroundSavePending.Remove(_ds.DataSetName)
                Debug.WriteLine(String.Format("Thread complete: {0}", t.Name))
                RaiseEvent SaveCompleted(Me, New System.EventArgs())
            End Try
        End Sub

        Public ReadOnly Property Result() As Exception
            Get
                Return _result
            End Get
        End Property

        Public ReadOnly Property Thread() As Threading.Thread
            Get
                Return t
            End Get
        End Property

        Public Function Join(Optional ByVal timeout As Integer = 0) As Boolean
            If t IsNot Nothing Then
                Return t.Join(If(timeout = 0, Threading.Timeout.Infinite, timeout))
            Else
                Return True
            End If
        End Function
    End Class

    Private Function MakeBackup(ByVal folder As String, ByVal source As String) As String
        Return MakeBackup(folder, source, My.Resources.BackupFileSuffix, My.Resources.XmlFileType, Nothing)
    End Function

    Private Function MakeBackup(ByVal folder As String, ByVal source As String, ByVal suffix As String) As String
        Return MakeBackup(folder, source, My.Resources.BackupFileSuffix, suffix, Nothing)
    End Function

    Private Function MakeBackupToErrorFolder(ByVal folder As String, ByVal source As String, ByVal isSchema As Boolean) As String
        Return MakeBackup(folder, source, String.Format(".pending.{0:yyyyMMddHHmmss}", DateTime.Now), If(isSchema, My.Resources.XmlSchemaFileType, My.Resources.XmlFileType), gErrorFolder)
    End Function

    Private Function MakeBackup(ByVal folder As String, ByVal source As String, ByVal backupSuffix As String, ByVal suffix As String, ByVal targetFolder As String) As String
        Dim sourcePath As String = IO.Path.Combine(folder, source & suffix)
        Dim sourceInfo As New FileInfo(sourcePath)

        Dim targetPath As String = _
            IO.Path.Combine(If(String.IsNullOrEmpty(targetFolder), folder, targetFolder), _
                            source & backupSuffix & suffix)

        If sourceInfo.Exists AndAlso sourceInfo.Length > 38 Then ' note: 38 is the length of a minimal XML Header.
            sourceInfo = Nothing
            Try
                File.Delete(targetPath) ' NOTE: Delete succeeds even if the file doesn't exist already.
            Catch ex As UnauthorizedAccessException
                LogError(MODULENAME, "MakeBackup", ex, False, "FileDeleteFailed", targetPath)
            Catch ex As IOException
                LogError(MODULENAME, "MakeBackup", ex, False, "FileDeleteFailed", targetPath)
            End Try

            Try
                File.Move(sourcePath, targetPath)
            Catch ex As UnauthorizedAccessException
                LogError(MODULENAME, "MakeBackup", ex, False, "FileMoveFailed", sourcePath, targetPath)
            Catch ex As IOException
                LogError(MODULENAME, "MakeBackup", ex, False, "FileMoveFailed", sourcePath, targetPath)
            End Try

        Else ' current file is empty anyway, so just delete it, leave existing backup.
            Try
                File.Delete(sourcePath)
            Catch ex As UnauthorizedAccessException
                LogError(MODULENAME, "MakeBackup", ex, False, "FileDeleteFailed", sourcePath)
            Catch ex As IOException
                LogError(MODULENAME, "MakeBackup", ex, False, "FileDeleteFailed", sourcePath)
            End Try

            sourceInfo = Nothing
        End If

        Return sourcePath
    End Function


    ''' <summary>
    ''' Save commited data to XML file.
    ''' </summary>
    ''' <param name="ds">DataSet to save</param>
    ''' <param name="location">Target filename, excluding the folder path</param>
    ''' <remarks>
    ''' In order to maintain a 'transaction log', all uncommited rows in a dataset are
    ''' saved into a 'Pending' folder, and 'accept changes' only ever called when
    ''' loading the datasets from the 'LastSync' folder. This is simple to code and debug
    ''' and avoids needing 'lastmodifiedtime' queries on the datasets.
    ''' </remarks>
    Public Sub SaveCommitted(ByVal ds As DataSet, ByVal location As String)
        Const METHODNAME As String = "SerialiseCommittedData"

        If ds Is Nothing Then Throw New ArgumentNullException("ds")
        If String.IsNullOrEmpty(location) Then Throw New ArgumentNullException("location")
        'WaitForSaveCommittedCompletion(ds)

        Try
            location = MakeBackup(gDatabaseFolder, location)

            Dim fs As New FileStream(location, FileMode.Create)
            'Dim cs As New Compression.GZipStream(fs, Compression.CompressionMode.Compress)

            Dim dst As DataSet = ds.Copy
            If dst IsNot Nothing Then
                dst.RejectChanges() ' discard changes (they'll be saved in the 'Pending' folder.
                'Dim xw As Xml.XmlWriter = Xml.XmlWriter.Create(fs, _xws)
                dst.WriteXml(fs, XmlWriteMode.IgnoreSchema)
            Else ' no data, just leave empty file.
                fs.Close()
            End If

        Catch ex As Threading.ThreadAbortException
            ' application closing...
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, RES_ErrorSavingDataSet)
            'Throw
        Finally
            'Application.DoEvents()
        End Try
        Debug.WriteLine(METHODNAME & " Complete")
    End Sub


    Private Sub SetSaveInProgress(ByVal ds As DataSet, ByVal value As Boolean)
        Threading.Monitor.Enter(saveInProgress)
        saveInProgress(ds.DataSetName) = value
        Threading.Monitor.Exit(saveInProgress)
    End Sub


    Private Sub SetSaveNeeded(ByVal ds As DataSet, ByVal saveNeededValue As Boolean)
        Threading.Monitor.Enter(saveNeeded)
        saveNeeded(ds.DataSetName) = saveNeededValue
        Threading.Monitor.Exit(saveNeeded)
    End Sub

    Private Sub InitSaveFlags(ByVal ds As DataSet)
        Threading.Monitor.Enter(saveNeeded)
        Threading.Monitor.Enter(saveInProgress)
        If saveInProgress.ContainsKey(ds.DataSetName) Then
            saveInProgress(ds.DataSetName) = True
        Else
            saveInProgress.Add(ds.DataSetName, True)
        End If

        If saveNeeded.ContainsKey(ds.DataSetName) Then
            saveNeeded(ds.DataSetName) = True
        Else
            saveNeeded.Add(ds.DataSetName, True)
        End If
        Threading.Monitor.Exit(saveInProgress)
        Threading.Monitor.Exit(saveNeeded)
    End Sub

    ''' <summary>
    ''' Save Uncommitted (Pending) Data to XML file.
    ''' </summary>
    ''' <param name="ds">DataSet to save</param>
    ''' <param name="location">Target filename, excluding the folder path</param>
    ''' <remarks>
    ''' In order to maintain a 'transaction log', all uncommited rows in a dataset are
    ''' saved into a 'Pending' folder, and 'accept changes' only ever called when
    ''' loading the datasets from the 'LastSync' folder. This is simple to code and debug
    ''' and avoids needing 'lastmodifiedtime' queries on the datasets.
    ''' </remarks>
    Public Sub SavePending(ByVal ds As DataSet, ByVal location As String)
        Const METHODNAME As String = "SerialisePendingData"

        If ds Is Nothing Then Throw New ArgumentNullException("ds")
        If String.IsNullOrEmpty(location) Then Throw New ArgumentNullException("location")

        If saveInProgress.ContainsKey(ds.DataSetName) AndAlso saveInProgress(ds.DataSetName) Then
            saveNeeded(ds.DataSetName) = True
            Return
        End If
        InitSaveFlags(ds)
        Dim saveToLocation As String

        Do While saveNeeded(ds.DataSetName)
            Try
                SetSaveNeeded(ds, False)
                SetSaveInProgress(ds, True)
                saveToLocation = MakeBackup(gTransactionFolder, location)

                Using fs As New FileStream(saveToLocation, FileMode.Create)
                    Using dst As DataSet = ds.GetChanges(DataRowState.Modified Or DataRowState.Added Or DataRowState.Deleted)
                        If dst IsNot Nothing Then
                            dst.WriteXml(fs, XmlWriteMode.DiffGram)
                        Else
                            fs.Close()
                        End If
                    End Using
                End Using
                'Dim cs As New Compression.GZipStream(fs, Compression.CompressionMode.Compress)

                'Catch ex As Threading.ThreadAbortException
                ' application closing...
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorSavingDataSet)
                SetSaveNeeded(ds, True)
                Throw
            Finally
                SetSaveInProgress(ds, False)
            End Try
            Debug.WriteLine(METHODNAME & " Complete")
        Loop

    End Sub

    Public Sub SaveSchema(Of T As DataSet)(ByVal ds As T, ByVal filename As String)
        If ds Is Nothing Then Throw New ArgumentNullException("ds")

        ds.WriteXmlSchema(MakeBackup(gSchemaFolder, filename, My.Resources.XmlSchemaFileType))
    End Sub


    'Public Sub LoadSchema(Of T As DataSet)(ds As t, ByVal filename As String)
    '    If ds Is Nothing Then
    '        Throw New ArgumentNullException("ds")
    '        Exit Sub
    '    End If

    '    filename = System.IO.Path.Combine(gSchemaFolder, filename.Replace(".XML", ".XSD"))
    '    ds.ReadXmlSchema(filename)
    'End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")> _
    Public Sub DealWithDataSetErrors(Of T As DataSet)(ByVal targetDataSet As T, Optional ByVal discardRowsInError As Boolean = False)
        Const METHODNAME As String = "DealWithDataSetErrors"
        Dim swr As IO.StringWriter = Nothing
        Dim rowsInError() As DataRow
        Dim myTable As DataTable
        Dim myCol As DataColumn
        Dim errorMessage As String

        Try
            For Each myTable In targetDataSet.Tables
                If myTable.HasErrors Then ' Test if the table has errors. If not, skip it.
                    rowsInError = myTable.GetErrors() ' Get an array of all rows with errors.
                    swr = New IO.StringWriter(WithoutCulture)
                    swr.WriteLine(My.Resources.SerialisationTableErrorsTemplate, myTable.TableName)

                    For Each dr As DataRow In rowsInError
                        swr.WriteLine("Row Error: {0}", dr.RowError)
                        For Each myCol In myTable.Columns
                            Dim err As String = dr.GetColumnError(myCol)
                            If Not String.IsNullOrEmpty(err) Then
                                swr.WriteLine("Column '{0}' : '{1}' : Error '{2}'", myCol.ColumnName, dr(myCol).ToString, err)
                            Else
                                swr.WriteLine("Column '{0}' : '{1}'", myCol.ColumnName, dr(myCol).ToString)
                            End If
                        Next
                        dr.ClearErrors() ' Clear the row errors
                        If discardRowsInError Then
                            myTable.Rows.Remove(dr)
                            ' dr.Delete()
                            ' dr.AcceptChanges() ' really cruel hack
                        End If
                        swr.WriteLine()
                    Next dr
                    errorMessage = swr.ToString
                    swr.Close()
                    swr = Nothing
                    gEventLog.EventLog.AddEventLogRow( _
                        Guid.NewGuid, DateTime.UtcNow, DateTime.UtcNow, 0, gDeviceIDString, MODULENAME, _
                        gDeviceID, ConsultantConfig.Name, My.Resources.SerialisationTableMergeError, Nothing, _
                        errorMessage, Nothing, DateTime.UtcNow, ConsultantConfig.Name, DateTime.UtcNow, _
                        ConsultantConfig.Name, Nothing)
                End If
            Next
        Catch ex As Threading.ThreadAbortException
            ' application closing...
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, RES_UnableToProcessErrors)
        Finally
            If swr IsNot Nothing Then
                swr.Flush()
                swr.Close()
            End If

            'swr = Nothing
            'Try
            '    SavePending(gEventLog, gErrorLogDbFileName)
            'Catch ' ex As Exception

            'End Try
        End Try

    End Sub

    ''' <summary>
    ''' Merge data from one DataSet (SourceDataSet) into another (TargetDataSet), optionally accepting changes
    ''' </summary>
    ''' <param name="TargetDataSet">Target DataSet</param>
    ''' <param name="SourceDataSet">Source DataSet</param>
    ''' <param name="AcceptChanges"></param>
    ''' <remarks>
    ''' DataSetMerge simulates a DataSet.Merge, but allows us to optionally 'acceptchanges' on
    ''' merged-in rows. This allows us to maintain a convenient transaction table, by 
    ''' allowing us to have download rows from the server, and not have them appear as 
    ''' transactions in the local database.
    ''' The Source DataSet may omit tables, but tables supplied must have schemas 
    ''' that match the target database, or an exception will be thrown.
    ''' </remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Public Sub Merge(Of t As DataSet, s As DataSet)(ByVal targetDataSet As t, ByVal sourceDataSet As s, Optional ByVal acceptChanges As Boolean = False)
        Const METHODNAME As String = "DataSetMerge"

        If targetDataSet Is Nothing Then
            Throw New ArgumentNullException("targetDataSet")
        End If

        If sourceDataSet Is Nothing Then
            Throw New ArgumentNullException("sourceDataSet")
        End If

        Try
            targetDataSet.EnforceConstraints = True
        Catch ex As Exception
            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try

        'dsTarget.Merge(dsSource, AcceptChanges)
        For Each dtin As DataTable In sourceDataSet.Tables
            If dtin.Rows.Count > 0 Then
                Try
                    Dim dt As DataTable = targetDataSet.Tables(dtin.TableName)
                    dt.BeginLoadData()
                    For Each dr As DataRow In dtin.Rows
                        Try
                            dt.LoadDataRow(dr.ItemArray, acceptChanges)
                        Catch ex As Exception
                            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
                            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                        End Try
                    Next
                    dt.EndLoadData()
                Catch ex As Exception
                    Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                End Try
            End If
        Next
        Debug.WriteLine("DataSetMerge Complete") ' dummy line, because I hate it when a debug step goes to the last statement _inside_ the catch block, instead of leaving the try block.
    End Sub

    Public Sub ReloadPendingTransactions(Of T As DataSet)(ByVal ds As T, ByVal fileName As String)
        Const METHODNAME As String = "FetchPending"
        Dim readFrom As String = Path.Combine(gTransactionFolder, fileName & My.Resources.XmlFileType)
        Dim sourceDataInfo As FileInfo = New FileInfo(readFrom)
        If sourceDataInfo.Exists AndAlso sourceDataInfo.Length > 0 Then
            Try
                'FetchDataSet(ds, location, SaveModes.Pending, discardRowsInError)
                ds.EnforceConstraints = False
                ds.ReadXml(readFrom, XmlReadMode.DiffGram)
                ds.EnforceConstraints = True
            Catch ex As Xml.XmlException
                'something wrong with the data
                Terminology.DisplayFormattedMessage(Nothing, MODULENAME, RES_ErrorLoadingData, MessageBoxIcon.Exclamation, ex.Message, ex.LineNumber, ex.LinePosition)
                MakeBackupToErrorFolder(gTransactionFolder, fileName, False)
                LogError(MODULENAME, METHODNAME, ex, True, Nothing)
            Catch ex As Threading.ThreadAbortException
                ' application closing...
            Catch ex As ConstraintException
                Terminology.DisplayMessage(Nothing, MODULENAME, RES_DataInconsistencyWarning, MessageBoxIcon.Exclamation)
                MakeBackupToErrorFolder(gTransactionFolder, fileName, False)
                LogError(MODULENAME, METHODNAME, ex, True, Nothing)
                DealWithDataSetErrors(ds, True)
                SavePending(ds, fileName)
                ds.EnforceConstraints = True
            Catch ex As Exception
                Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
                LogError(MODULENAME, METHODNAME, ex, True, Nothing)
            End Try
        End If
        Debug.WriteLine(METHODNAME & " Complete") ' dummy line, because I hate it when a debug step goes to the last statement _inside_ the catch block, instead of leaving the try block.
    End Sub

    Public Function LoadDataSet(Of T As DataSet)(ByVal ds As T, ByVal fileName As String) As Boolean
        Const METHODNAME As String = "FetchDataSet"

        Dim schemaPath As String = Path.Combine(gSchemaFolder, fileName & My.Resources.XmlSchemaFileType)
        Dim committedPath As String = Path.Combine(gDatabaseFolder, fileName & My.Resources.XmlFileType)
        Dim pendingPath As String = Path.Combine(gTransactionFolder, fileName & My.Resources.XmlFileType)

        Try
            ds.Clear()
            ds.EnforceConstraints = False

            Dim schemaFileInfo As FileInfo = New FileInfo(schemaPath)
            If schemaFileInfo.Exists AndAlso schemaFileInfo.Length > 168 Then
                ds.ReadXmlSchema(schemaPath)
            End If

            Dim committedFileInfo As FileInfo = New FileInfo(committedPath)
            If committedFileInfo.Exists AndAlso committedFileInfo.Length > 168 Then ' XML 'empty' file is 168 bytes.
                ds.ReadXml(committedPath, XmlReadMode.IgnoreSchema)
                ds.AcceptChanges()
            End If

            Dim pendingFileInfo As FileInfo = New FileInfo(pendingPath)
            If pendingFileInfo.Exists AndAlso pendingFileInfo.Length > 168 Then ' XML 'empty' file is 168 bytes.
                ds.ReadXml(pendingPath, XmlReadMode.DiffGram)
            End If

            ds.EnforceConstraints = True
            Return True
        Catch ex As Threading.ThreadAbortException
            ' application closing...
            Return False
        Catch ex As ConstraintException
            Terminology.DisplayMessage(Nothing, MODULENAME, RES_DataInconsistencyWarning, MessageBoxIcon.Exclamation)
            MakeBackup(gSchemaFolder, fileName, String.Format(".{0:yyyyMMddHHmmss}", DateTime.Now), My.Resources.XmlSchemaFileType, gErrorFolder)
            MakeBackupToErrorFolder(gSchemaFolder, fileName, True)
            MakeBackupToErrorFolder(gDatabaseFolder, fileName, False)
            MakeBackupToErrorFolder(gTransactionFolder, fileName, False)
            DealWithDataSetErrors(ds, True)
            ds.EnforceConstraints = True
            SaveCommitted(ds, fileName)
            SavePending(ds, fileName)
            Return True
        Catch ex As Exception
            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
            LogError(MODULENAME, METHODNAME, ex, True, Nothing)
            Return False
        Finally
            Debug.WriteLine(METHODNAME & " Complete") ' dummy line, because I hate it when a debug step goes to the last statement _inside_ the catch block, instead of leaving the try block.
        End Try
    End Function

    Public Function ReadSchema(Of T As DataSet)(ByVal ds As T, ByVal fileName As String) As Boolean
        Const METHODNAME As String = "FetchDataSetSchema"

        Dim schemaPath As String = Path.Combine(gSchemaFolder, fileName & ".XSD")

        Try
            If (New FileInfo(schemaPath)).Exists Then
                ds.ReadXmlSchema(schemaPath)
            End If

            Return True
        Catch ex As Threading.ThreadAbortException
            ' application closing...
            Return False
        Catch ex As Exception
            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
            LogError(MODULENAME, METHODNAME, ex, True, Nothing)
            Return False
        Finally
            Debug.WriteLine(METHODNAME & " Complete") ' dummy line, because I hate it when a debug step goes to the last statement _inside_ the catch block, instead of leaving the try block.
        End Try
    End Function
End Module