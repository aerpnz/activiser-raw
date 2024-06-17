Imports System.Media
Imports System.Windows.Forms
Imports activiser.Library.Forms.GraphicsUtilities

Partial Friend Class Synchronisation
    Private Const MODULENAME As String = "Sync"

    Private Shared _syncOk As Boolean
    Private Shared _startedAt As DateTime

    Shared Sub New()
        SetSyncInterval(ConfigurationSettings.GetValue(My.Resources.AppConfigSyncIntervalKey, 60))
        SetAutoSyncInterval(ConfigurationSettings.GetValue(My.Resources.AppConfigAutoSyncIntervalKey, 60), ConfigurationSettings.GetValue(My.Resources.AppConfigAutoSyncKey, False))
    End Sub

    Private Sub New()

    End Sub

#Region "Timers"
    Public Shared WithEvents SyncTimer As New System.Windows.Forms.Timer
    Public Shared WithEvents AutoSyncTimer As New System.Windows.Forms.Timer

    'Set the sync prompt to a given interval value
    Public Shared Sub SetSyncInterval(ByVal interval As Integer)
        If interval < 1 Then interval = 60
        gSyncInterval = interval * 60 * 1000

        If gSyncInterval = 0 Then
            SyncTimer.Enabled = False
        Else
            SyncTimer.Interval = gSyncInterval
            SyncTimer.Enabled = True
        End If
    End Sub

    Public Shared Sub SetAutoSyncInterval(ByVal interval As Integer, ByVal enabled As Boolean)
        gAutoSyncInterval = interval
        gAutoSync = enabled

        AutoSyncTimer.Enabled = False
        AutoSyncTimer.Interval = 60000 ' interval * 1000
        AutoSyncTimer.Enabled = gAutoSync
        ConfigurationSettings.Save()
    End Sub

    Private Shared Sub SyncTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SyncTimer.Tick
        gSyncsMissed += 1
        If gSyncsMissed > 5 Then
            Dim c As Color = SystemColors.ActiveCaption
            If c.R > 200 AndAlso c.G < 100 AndAlso c.B < 100 Then
                c = NegateColor(c)
            Else
                c = Color.Crimson
            End If
            gMainForm.SetSyncColors(Color.White, c)
        ElseIf gSyncsMissed > 4 Then
            gMainForm.SetSyncColors(Color.White, Color.Orange)
        ElseIf gSyncsMissed > 3 Then
            gMainForm.SetSyncColors(Color.White, Color.Goldenrod)
        ElseIf gSyncsMissed > 2 Then
            gMainForm.SetSyncColors(Color.White, Color.LimeGreen)
        ElseIf gSyncsMissed > 1 Then
            gMainForm.SetSyncColors(Color.White, Color.Green)
        Else
            gMainForm.SetSyncColors(System.Drawing.SystemColors.ActiveCaptionText, System.Drawing.SystemColors.ActiveCaption)
        End If
        'Application.DoEvents()
    End Sub

    Private Shared Function DoNotAutoSync() As Boolean
        If Not gAutoSync Then Return True
        If gHoldAutoSync Then Return True
        If gSyncInProgress Then Return True
        If JobForms.Count > 0 Then Return True

        If DateTime.Now.TimeOfDay < gSyncStartTime.TimeOfDay Then Return True
        If DateTime.Now.TimeOfDay > gSyncFinishTime.TimeOfDay Then Return True

        Select Case DateTime.Today.DayOfWeek
            Case DayOfWeek.Sunday
                If gSyncDays.IndexOf("U", StringComparison.Ordinal) = -1 Then Return True
            Case DayOfWeek.Monday
                If gSyncDays.IndexOf("M", StringComparison.Ordinal) = -1 Then Return True
            Case DayOfWeek.Tuesday
                If gSyncDays.IndexOf("T", StringComparison.Ordinal) = -1 Then Return True
            Case DayOfWeek.Wednesday
                If gSyncDays.IndexOf("W", StringComparison.Ordinal) = -1 Then Return True
            Case DayOfWeek.Thursday
                If gSyncDays.IndexOf("H", StringComparison.Ordinal) = -1 Then Return True
            Case DayOfWeek.Friday
                If gSyncDays.IndexOf("F", StringComparison.Ordinal) = -1 Then Return True
            Case DayOfWeek.Saturday
                If gSyncDays.IndexOf("S", StringComparison.Ordinal) = -1 Then Return True

        End Select

        Return False
    End Function
#End Region

#Region "Sounds"
    Friend Shared _soundsInitialised As Boolean
    Friend Shared _syncStartSound As SoundPlayer
    Friend Shared _syncCompleteSound As SoundPlayer
    Friend Shared _syncNewRequestSound As SoundPlayer

    Friend Shared Sub initSounds()
        If _soundsInitialised Then Return

        If _syncStartSound Is Nothing AndAlso ConfigurationSettings.GetValue("PlaySyncStartSound", True) Then
            Try : _syncStartSound = New SoundPlayer(IO.Path.Combine(gDatabaseRoot, My.Resources.SoundsSyncStarted)) : Catch : End Try
        End If

        If _syncCompleteSound Is Nothing AndAlso ConfigurationSettings.GetValue("PlaySyncCompleteSound", True) Then
            Try : _syncCompleteSound = New SoundPlayer(IO.Path.Combine(gDatabaseRoot, My.Resources.SoundsSyncComplete)) : Catch : End Try
        End If

        If _syncNewRequestSound Is Nothing AndAlso ConfigurationSettings.GetValue("PlayNewRequestSound", True) Then
            Try : _syncNewRequestSound = New SoundPlayer(IO.Path.Combine(gDatabaseRoot, My.Resources.SoundsNewRequest)) : Catch : End Try
        End If
        _soundsInitialised = True
    End Sub
#End Region

    Public Shared Sub StartManualSync()
        If Not gSyncInProgress Then
            If gSyncThread IsNot Nothing Then ' 'no' sync in progress, but thread defined ?
                gSyncThread.Priority = Threading.ThreadPriority.Normal
                Threading.Thread.Sleep(300) ' Give it a while to shutdown and try again.
                If gSyncThread IsNot Nothing Then ' still there, panic.
                    If Terminology.AskQuestion(Nothing, MODULENAME, RES_SyncThreadConnectFailure, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        gSyncThread.Abort()
                        gSyncThread = Nothing
                    Else
                        Return
                    End If
                End If
            End If

            If gSyncThread Is Nothing Then
                gSyncThread = New Threading.Thread(AddressOf SyncThread)
                gSyncThread.Start()
            End If
        End If
    End Sub

    'Public Shared Sub WaitForSync()
    '    Do While gSyncThread IsNot Nothing
    '        If gSyncThread.Join(10) Then
    '            Return
    '        End If
    '        Application.DoEvents() ' hack - really need to set up message loop.
    '    Loop
    'End Sub

    Private Shared Sub SyncThread()
        Const METHODNAME As String = "SyncThread"
        Const STR_ThreadNameTemplate As String = "Sync started at {0:s}"

        Try
            initSounds()
            gSyncThread.Name = String.Format(WithoutCulture, STR_ThreadNameTemplate, DateTime.Now)
            SyncFull()

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            '_waitForThread.Set()
            gSyncThread = Nothing
        End Try
    End Sub

    'Private Shared Sub AutoSyncThread()
    '    Const METHODNAME As String = "AutoSyncThread"
    '    Const STR_ThreadNameTemplate As String = "Auto Sync started at {0:s}"

    '    Try
    '        initSounds()
    '        gSyncThread.Name = String.Format(WithoutCulture, STR_ThreadNameTemplate, DateTime.Now)
    '        If _syncStartSound IsNot Nothing Then
    '            _syncStartSound.Play()
    '        End If

    '        SyncFull()
    '    Catch ex As Exception
    '        LogError(MODULENAME, METHODNAME, ex, False, Nothing)
    '    Finally
    '        gSyncThread = Nothing
    '    End Try
    'End Sub

    Public Shared Function CheckForAutoSync(ByVal owner As Form, ByVal moduleName As String, ByVal ResumeQuestion As String, ByVal CancelledMessage As String) As Boolean
        If Not gSyncInProgress Then Return True

        If Terminology.AskQuestion(owner, moduleName, RES_SyncInProgressQuestion, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
            Using sf As New SyncForm(owner)
                sf.SyncStarted() ' initialise sync view.
                sf.ShowDialog()
            End Using

            If Not gSyncInProgress Then
                If Terminology.AskQuestion(owner, moduleName, ResumeQuestion, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Return True
                End If
            Else
                Terminology.DisplayMessage(owner, moduleName, CancelledMessage, MessageBoxIcon.Asterisk)
            End If
        End If
        Return False
    End Function

    Private Shared Sub AutoSyncTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AutoSyncTimer.Tick
        Debug.WriteLine(String.Format("AutoSyncTimer_Tick: {0:s}", DateTime.UtcNow))
        Debug.WriteLine(String.Format("LastSyncAttempt: {0:s}", gLastSyncAttempt))
        If DoNotAutoSync() Then Return

        If DateTime.UtcNow > gLastSyncAttempt.AddMinutes(gAutoSyncInterval) Then
            If gSyncThread Is Nothing Then
                gSyncThread = New Threading.Thread(AddressOf SyncThread)
                gSyncThread.Start()
            End If
        End If
    End Sub

    'Private Sub CurrentSyncForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles gSyncForm.Disposed
    '    EndSync()
    'End Sub

    Public Shared Function SerializeDataSetToXmlDoc(ByVal ds As DataSet) As Xml.XmlDocument
        If ds Is Nothing Then Return Nothing
        Dim xd As New Xml.XmlDocument()
        xd.LoadXml(ds.GetXml())
        Return xd
    End Function

    Public Shared Function DeSerializeDataSetFromXmlDoc(Of T As {DataSet, New})(ByVal dsXml As Xml.XmlNode) As T
        If dsXml Is Nothing Then Return Nothing
        Dim result As New T
        Dim srpda As New IO.StringReader(dsXml.OuterXml)
        Dim xrs As New Xml.XmlReaderSettings()
        xrs.IgnoreWhitespace = False
        Dim xrpda As Xml.XmlReader = Xml.XmlReader.Create(srpda, xrs)

        result.EnforceConstraints = False
        result.ReadXml(New Xml.XmlNodeReader(dsXml))

        Return result
    End Function

    Public Shared Function DeSerializeDataSetFromXmlDoc(ByVal schemaFileName As String, ByVal dsXml As Xml.XmlNode) As DataSet
        If dsXml Is Nothing Then Return Nothing
        Dim result As New DataSet
        If Not ReadSchema(result, schemaFileName) Then
            ' don't really care; just carry on regardless, and hope for the best :)
            Debug.WriteLine(String.Format("Schema file not found: {0}", schemaFileName))
        End If

        Dim s As String = dsXml.OuterXml
        Debug.WriteLine(s.Contains(vbCrLf))

        Dim srpda As New IO.StringReader(dsXml.OuterXml)
        Dim xrs As New Xml.XmlReaderSettings()
        xrs.IgnoreWhitespace = False
        Dim xrpda As Xml.XmlReader = Xml.XmlReader.Create(srpda, xrs)

        result.EnforceConstraints = False
        result.ReadXml(New Xml.XmlNodeReader(dsXml))

        Return result
    End Function

    Public Shared Function DeSerializeDataSetFromString(ByVal schemaFileName As String, ByVal dsXml As String) As DataSet
        If dsXml Is Nothing Then Return Nothing
        Dim result As New DataSet
        If Not ReadSchema(result, schemaFileName) Then
            ' don't really care; just carry on regardless, and hope for the best :)
            Debug.WriteLine(String.Format("Schema file not found: {0}", schemaFileName))
        End If

        Debug.WriteLine(dsXml.Contains(vbCrLf))

        Dim srpda As New IO.StringReader(dsXml)
        Dim xrpda As Xml.XmlReader = Xml.XmlReader.Create(srpda)

        result.EnforceConstraints = False
        result.ReadXml(xrpda)

        Return result
    End Function

    Public Shared Function DeSerializeDataSetFromCompressedString(ByVal schemaFileName As String, ByVal data As Byte()) As DataSet
        If data Is Nothing OrElse data.Length = 0 Then Return Nothing
        Dim result As New DataSet
        If Not ReadSchema(result, schemaFileName) Then
            ' don't really care; just carry on regardless, and hope for the best :)
            Debug.WriteLine(String.Format("Schema file not found: {0}", schemaFileName))
        End If

        Dim uncompressedData As String = DecompressString(data)
        'Debug.WriteLine(If(uncompressedData.Contains(vbCrLf), "Found CR/LF", "No CR/LF Found") & " in uncompressed data")

        Dim xd As New Xml.XmlDocument
        xd.PreserveWhitespace = True
        xd.LoadXml(uncompressedData)
        'Debug.WriteLine(If(xd.OuterXml.Contains(vbCrLf), "Found CR/LF", "No CR/LF Found") & " in loaded XML data")

        Dim xr As Xml.XmlReader = New Xml.XmlNodeReader(xd)

        result.EnforceConstraints = False
        result.ReadXml(xr)

        Return result
    End Function

    Public Shared Sub DeSerializeDataSetFromXmlDoc(Of T As {DataSet, New})(ByVal ds As T, ByVal dsXml As Xml.XmlNode)
        If ds Is Nothing Then Throw New ArgumentNullException("ds")
        'Dim result As New T
        Dim srpda As New IO.StringReader(dsXml.OuterXml)
        Dim xrpda As New Xml.XmlTextReader(srpda)

        ds.EnforceConstraints = False
        ds.ReadXml(xrpda, XmlReadMode.InferSchema)
        Return
    End Sub

End Class
