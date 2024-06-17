Imports System.Reflection
Imports activiser.Library
Imports System.Xml
Imports activiser.Library.WebService

Public Class StartupForm
    Inherits System.Windows.Forms.Form
    Private Const MODULENAME As String = "StartupForm"

    Private WithEvents _backgroundLoaderThread As Threading.Thread

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        'note: this doesn't use the custom Terminology library, because it hasn't been loaded at this stage...
        Dim v As Version = Reflection.Assembly.GetExecutingAssembly.GetName.Version
        Me.VersionNumber.Text = String.Format(WithCulture, My.Resources.AboutFormVersionFormat, v.Major, v.Minor, v.Build, v.Revision)
    End Sub

    Private Sub loadLogo()
        If Me.InvokeRequired Then
            Dim d As New simpleSubDelegate(AddressOf loadLogo)
            Me.Invoke(d)
            Return
        End If

        If Me.Width > 400 AndAlso Me.Height > 400 Then
            Me.Logo.Image = My.Resources.Logo400x200
        End If
    End Sub

    Private Sub RefreshVersionNumber()
        Dim v As Version = Reflection.Assembly.GetExecutingAssembly.GetName.Version
        SetControlText(VersionNumber, Terminology.GetFormattedString(RES_AboutForm, RES_VersionFormat, v.Major, v.Minor, v.Build, v.Revision))
    End Sub

    Private Delegate Function askDelegate(ByVal template As String, ByVal buttons As MessageBoxButtons, ByVal defaultButton As MessageBoxDefaultButton, ByVal message As String) As DialogResult
    Private Function ask(ByVal template As String, ByVal buttons As MessageBoxButtons, ByVal defaultButton As MessageBoxDefaultButton, ByVal message As String) As DialogResult
        If Me.InvokeRequired Then
            Return DirectCast(Me.Invoke(New askDelegate(AddressOf ask), template, buttons, defaultButton, message), DialogResult)
        End If

        Return Terminology.AskFormattedQuestion(Me, MODULENAME, template, buttons, defaultButton, message)
    End Function

    Private Delegate Function getMeDelegate() As Form
    Private Function getMe() As Form
        If Me.InvokeRequired Then
            Return DirectCast(Me.Invoke(New getMeDelegate(AddressOf getMe)), Form)
        End If
        Return Me
    End Function

    Private Function DownLoadMainDb(ByVal message As String) As Boolean
        Dim result As Boolean
        Do While Not result
            If ask(RES_DownloadQuestionTemplate, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1, message) <> Windows.Forms.DialogResult.Yes Then
                Abort()
            End If
            result = TryDownload()
        Loop
        Return result
    End Function

    Private Sub Abort()
        If Me.InvokeRequired Then
            Dim d As New simpleSubDelegate(AddressOf Abort)
            Me.Invoke(d)
        Else '    Return
            If _backgroundLoaderThread IsNot Nothing Then
                _backgroundLoaderThread.Abort()
            End If
            gWebServer.Abort()
            gLoadSuccessful = False
            Application.Exit()
            'Me.DialogResult = Windows.Forms.DialogResult.Abort
            'Me.Close()
        End If
    End Sub

    Private Sub LoadComplete()
        If Me.InvokeRequired Then
            Dim d As New simpleSubDelegate(AddressOf LoadComplete)
            Me.Invoke(d)
            Return
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BackgroundLoader()
        Try
            loadLogo()

            LoadCustomTerminology()
            Terminology.LoadLabels(Me)

            RefreshVersionNumber()

            gWebServiceTimeout = AppConfig.GetSetting(My.Resources.AppConfigServerTimeoutKey, 30) * 1000
            gAllowNewRequests = AppConfig.GetSetting(My.Resources.AppConfigAllowNewRequestsKey, True)
            gAllowRequestsForClientsOnHold = AppConfig.GetSetting(My.Resources.AppConfigAllowRequestsForClientsOnHoldKey, True)

            Gps.TrackingInfoFormat = AppConfig.GetSetting(My.Resources.AppConfigTrackingInfoFormatKey, My.Resources.AppConfigTrackingInfoDefaultFormat)

            Startup.InitialiseWebServiceProxy(Me, False)

            'Dim lfn, pfn As String

            'find error log, if it exists, and load it
            ReloadPendingTransactions(gEventLog, gErrorLogDbFileName)


            ' find consultant profile database and load it.
            'lfn = Path.Combine(gDatabaseFolder, gConfigDbFileName) & My.Resources.XmlFileType
            If Not Main.SetupNeeded Then
                Try
                    ' Reading Application configuration
                    StatusMessage = Terminology.GetString(MODULENAME, RES_LoadingConfiguration)
                    ConsultantConfig.Load()
                    gConsultantUID = ConsultantConfig.UID
                Catch ex As Threading.ThreadAbortException
                    ' application closing...
                    Return
                Catch ex As Exception
                    Abort()
                End Try
            End If

            gintJobHistoryAge = ConsultantConfig.JobHistoryAgeLimit
            gintJobHistoryCount = ConsultantConfig.JobHistoryLimit
            UserName = Terminology.GetFormattedString(RES_AboutForm, RES_UserFormat, ConsultantConfig.Name)

            StatusMessage = Terminology.GetString(MODULENAME, RES_LoadingMainDB)

            Dim mainDbLoaded As Boolean = False
            Do While Not mainDbLoaded
                If Not (New FileInfo(Path.Combine(gDatabaseFolder, gMainDbFileName) & My.Resources.XmlFileType)).Exists Then
                    mainDbLoaded = DownLoadMainDb(Terminology.GetString(MODULENAME, RES_MainDatabaseMissing))
                Else
                    mainDbLoaded = LoadDataSet(gClientDataSet, gMainDbFileName)
                    If Not mainDbLoaded Then
                        mainDbLoaded = DownLoadMainDb(Terminology.GetString(MODULENAME, RES_ErrorLoadingMainDatabase))
                    End If
                End If
            Loop


            Try
                StatusMessage = Terminology.GetString(MODULENAME, RES_LoadingForms)
                LoadDataSet(gFormDefinitions, gFormDefinitionFileName)
                'Dim formDefsDbName As String = Path.Combine(gDatabaseFolder, gFormDefinitionFileName)
                'If (New FileInfo(formDefsDbName).Exists) Then
                '    gFormDefinitions.ReadXml(formDefsDbName, XmlReadMode.IgnoreSchema)
                'End If

            Catch ex As Exception
                Terminology.DisplayMessage(Nothing, MODULENAME, RES_ErrorLoadingForms, MessageBoxIcon.Exclamation)
                gFormDefinitions.Clear()
            End Try


            StatusMessage = Terminology.GetString(MODULENAME, RES_MainDatabaseLoaded)

            LoadRequestStatusCodes()


            gLoadSuccessful = True

            ' load consultant row into configuration
            ConsultantConfig.ConsultantRow = gClientDataSet.Consultant.FindByConsultantUID(ConsultantConfig.UID)
            ' needs to happen after previous bit of code - need the consultant row.
            ConsultantConfig.LastSync = AppConfig.GetSetting(My.Resources.AppConfigForceLastSyncDateTimeKey, ConsultantConfig.LastSync)

            gSyncDays = AppConfig.GetSetting(My.Resources.AppConfigSyncDaysKey, My.Resources.AppConfigSyncDaysDefault)

            Try
                gSyncStartTime = DateTime.Parse(AppConfig.GetSetting(My.Resources.AppConfigSyncStartTimeKey, My.Resources.AppConfigSyncStartTimeDefault), WithoutCulture)
            Catch ex As Threading.ThreadAbortException
                ' application closing...
                Return
            Catch ex As Exception
                gSyncStartTime = New DateTime(1900, 1, 1, 8, 0, 0)
            End Try

            Try
                gSyncFinishTime = DateTime.Parse(AppConfig.GetSetting(My.Resources.AppConfigSyncFinishTimeKey, My.Resources.AppConfigSyncFinishTimeDefault), WithoutCulture)
            Catch ex As Threading.ThreadAbortException
                ' application closing...
                Return
            Catch ex As Exception
                gSyncFinishTime = New DateTime(1900, 1, 1, 18, 0, 0)
            End Try

            If AppConfig.GetSetting("EnableTracking", False) Then Gps.StartGPS()

            LoadComplete()

        Catch ex As Threading.ThreadAbortException
            ' application closing...
            Return
        Catch ex As Exception
            Terminology.DisplayFormattedMessage(Nothing, MODULENAME, RES_FatalErrorLoadingMainDB, MessageBoxIcon.Exclamation, ex.Message)
            Abort()
        End Try

    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Const METHODNAME As String = "Form_Load"
        Try
            gLoadSuccessful = False

            _backgroundLoaderThread = New Threading.Thread(AddressOf BackgroundLoader)
            _backgroundLoaderThread.Start()

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, Nothing)
            gLoadSuccessful = False
        End Try
    End Sub

    Private Sub SetControlText(ByVal control As Control, ByVal Value As String)
        If Me.InvokeRequired Then
            Dim d As New SetTextDelegate(AddressOf SetControlText)
            Me.Invoke(d, control, Value)
            Return
        End If
        Try
            control.Text = Value
            Me.Refresh()
        Catch ex As ObjectDisposedException 'hopper discovered this
            'don't care.
        End Try
    End Sub


    Private Function GetControlText(ByVal control As Control) As String
        If Me.InvokeRequired Then
            Dim d As New GetTextDelegate(AddressOf GetControlText)
            Return CStr(Me.Invoke(d, control))
        End If
        Try
            Return control.Text
        Catch ex As ObjectDisposedException 'hopper discovered this
            Return String.Empty
        End Try
    End Function

    Friend Property StatusMessage() As String
        Get
            Return GetControlText(Me.StatusMessageBox)
        End Get
        Set(ByVal Value As String)
            SetControlText(Me.StatusMessageBox, Value)
        End Set
    End Property

    Private Property UserName() As String
        Get
            Return GetControlText(UserNameBox)
        End Get
        Set(ByVal value As String)
            SetControlText(UserNameBox, value)
        End Set
    End Property


    Private Function GetValidConnection() As Boolean
        StatusMessage = Terminology.GetString(MODULENAME, RES_TestingConnection)
        Do While Not Synchronisation.CheckDeviceId()
            If Terminology.AskQuestion(Me, MODULENAME, RES_EnterUrlDialogQuestion, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Using sf As New SetupForm()
                    sf.Owner = Me
                    If sf.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                        Me.DialogResult = Windows.Forms.DialogResult.Abort
                        Return False
                    End If
                End Using
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Return False
            End If
        Loop
        Return True
    End Function

    Private Function GetTerminology() As Boolean
        Try
            StatusMessage = Terminology.GetString(MODULENAME, RES_TerminologyLoading)
            Terminology.Load(gWebServer.GetTerminology(gDeviceIDString, gConsultantUID, Terminology.ClientKey, AppConfig.GetSetting(My.Resources.AppConfigLanguageIdKey, CInt(My.Resources.AppConfigLanguageIdDefault)), DateTime.MinValue))
            Return True
        Catch ex As Exception
            StatusMessage = Terminology.GetString(MODULENAME, RES_TerminologyLoadError)
            LogError(MODULENAME, "GetTerminology", ex, False, RES_TerminologyLoadError)
            Return False
        End Try
    End Function

    Private Sub SetControlEnabled(ByVal menuItem As MenuItem, ByVal enabled As Boolean)
        If Me.InvokeRequired Then
            Dim d As New SetControlEnabledDelegate(Of MenuItem)(AddressOf SetControlEnabled)
            Me.Invoke(d, menuItem, enabled)
        Else
            menuItem.Enabled = enabled
        End If
    End Sub

    Private Function GetControlEnabled(ByVal menuItem As MenuItem) As Boolean
        If Me.InvokeRequired Then
            Dim d As New GetControlEnabledDelegate(Of MenuItem)(AddressOf GetControlEnabled)
            Return CBool(Me.Invoke(d, menuItem))
        Else
            Return menuItem.Enabled
        End If
    End Function


    Private Sub DownloadForms()
        Dim fds As FormDefinition = gWebServer.GetFormDefinitionsMasked(gDeviceIDString, gConsultantUID, SchemaClients.Mobile, Nothing)
        gFormDefinitions.Merge(fds)
        gFormDefinitions.AcceptChanges()
        SetControlEnabled(Me.CancelButton, False)
        SaveCommitted(gFormDefinitions, gFormDefinitionFileName)
        gFormDefinitions.Clear() ' need to clear these here so the startup process can reload them !
        SetControlEnabled(Me.CancelButton, True)
    End Sub


    Private Sub SaveConsultantProfile()
        ConsultantConfig.UpdateProfile()
        SetControlEnabled(Me.CancelButton, False)
        Synchronisation.UploadProfile()
        SetControlEnabled(Me.CancelButton, True)
    End Sub

    Private Sub SaveMainDb(ByVal clientData As String)
        SetControlEnabled(Me.CancelButton, False)
        StatusMessage = Terminology.GetString(MODULENAME, RES_SavingMainDB)
        Dim dbFile As New System.IO.StreamWriter(Path.Combine(gDatabaseFolder, gMainDbFileName) & My.Resources.XmlFileType, False)
        dbFile.Write(clientData)
        dbFile.Close()
        SetControlEnabled(Me.CancelButton, True)
    End Sub

    'Private Sub SaveMainDb(ByVal clientXmlData As XmlNode)
    '    SetControlEnabled(Me.CancelButton, False)
    '    StatusMessage = Terminology.GetString(MODULENAME, RES_SavingMainDB)
    '    Dim dbFile As New System.IO.StreamWriter(Path.Combine(gDatabaseFolder, gMainDbFileName) & My.Resources.XmlFileType, False)
    '    dbFile.WriteLine(My.Resources.XmlHeader)
    '    dbFile.Write(clientXmlData.OuterXml)
    '    dbFile.Close()
    '    SetControlEnabled(Me.CancelButton, True)
    'End Sub

    Private Function TryDownload() As Boolean
        Const METHODNAME As String = "TryDownload"
        'Dim ds As WebService.activiserDataSet = Nothing

        If Not GetValidConnection() Then Return False

        GetTerminology()

        Try
            StatusMessage = Terminology.GetString(MODULENAME, RES_DownloadingMainDatabase)
            Dim syncTime As DateTime = gWebServer.SyncStart(gDeviceIDString, gConsultantUID)

            Dim ds As DataSet = gWebServer.GetClientDataSetSchema(gDeviceIDString, gConsultantUID)
            If ds IsNot Nothing Then
                'gClientDataSet.Merge(ds, True, MissingSchemaAction.Add)
                SaveSchema(ds, gMainDbFileName)
                ReadSchema(gClientDataSet, gMainDbFileName)
            End If

#If MINORPLANETCLIENT Then
            Dim clientXmlData As XmlNode = CType(gWebServer.GetClientDataSetAsXml(gDeviceIDString, gConsultantUID), XmlNode)
            Dim clientDecompressedData As String = clientXmlData.OuterXml
#Else
            Dim clientCompressedData() As Byte = gWebServer.GetClientDataSetCompressed(gDeviceIDString, gConsultantUID)
            Dim clientDecompressedData As String = DecompressString(clientCompressedData)
#End If

            If Not String.IsNullOrEmpty(clientDecompressedData) Then
                SaveMainDb(clientDecompressedData)

                LoadDataSet(gClientDataSet, gMainDbFileName)
                DownloadForms()
            Else
                StatusMessage = Terminology.GetFormattedString(MODULENAME, RES_ErrorGettingMainDataBase, Terminology.GetString(MODULENAME, RES_NoDataGettingMainDataBase))
                Return False
            End If

            ConsultantConfig.LastSync = gWebServer.SyncComplete(gDeviceIDString, gConsultantUID)
            SaveConsultantProfile()
            Return True
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            LogError(MODULENAME, METHODNAME, ex, True, RES_DatabaseDownloadFailure)
            Return False
        End Try
    End Function
#If WINDOWSMOBILE Then
#Region "InputPanel"
    Private Sub StartupForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Me.InputPanel.Enabled = False
    End Sub

    Private _inPanelSwitch As Boolean
    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        Const METHODNAME As String = "InputPanelSwitch"
        If Me._inPanelSwitch Then Return
        Me._inPanelSwitch = True
        Try
            If loadInitial Then
                InputPanel.Enabled = False ' ConfigurationSettings.GetBooleanValue(MODULENAME & My.Resources.InputPanelEnabledRegValueName, False)
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
            Else
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                AppConfig.SaveSetting(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.InputPanel.Enabled)
            End If
            'Application.DoEvents()
        Catch ex As ObjectDisposedException
            ' who cares
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, String.Empty)
        Finally
            Me._inPanelSwitch = False
        End Try
    End Sub

    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        If Me._inPanelSwitch Then Return
        InputPanelSwitch(False)
    End Sub

#End Region
#End If

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Abort()
    End Sub
End Class
