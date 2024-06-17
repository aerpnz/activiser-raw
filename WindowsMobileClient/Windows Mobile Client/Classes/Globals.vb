Imports activiser.Library
Imports activiser.Library.PlatformInfo

Module Globals
#Region "Global Object/Variable Declaration"
    Public gApplicationGuid As Guid = New Guid("F97EB4E7-F684-4402-895D-CFD15CE86498")

    Public gDeviceID As String
    Public gDeviceIDString As String

    Public gConsultantUID As Guid = Guid.Empty
    Public gUsername As String
    Public gPassword As String

    Public gSavePassword As Boolean = True
    Public gDomain As String = String.Empty
    Public gDomainUsername As String = String.Empty
    Public gDomainPassword As String = String.Empty

    Public gintJobHistoryCount As Integer = 5
    Public gintJobHistoryAge As Integer = 183

    Public gDateFormat As String = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
    Public gTimeFormat As String = GetShortTimeFormat(System.Globalization.DateTimeFormatInfo.CurrentInfo.LongTimePattern)

    Public gAllowNewRequests As Boolean = True
    Public gAllowRequestsForClientsOnHold As Boolean = True

    Public WithCulture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentUICulture
    Public WithoutCulture As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

    Public gMainForm As MainForm

    Public gWebServer As Library.WebService.activiser
    Public gServerUrl As String
    Public gWebServiceTimeout As Integer

#Region "Datasets"
    ' Datasets to hold the information stored as xml.
    Public gClientDataSet As New WebService.activiserDataSet
    Public gFormDefinitions As New WebService.FormDefinition
    Public gEventLog As New WebService.EventLogDataSet

#End Region



#Region "File Locations"
    Public gDatabaseRoot As String
    Public gErrorFolder As String
    Public gDatabaseFolder As String
    Public gTransactionFolder As String
    Public gSchemaFolder As String

    Public gMainDbFileName As String
    Public gConfigDbFileName As String
    Public gLocalItemsFileName As String
    Public gErrorLogDbFileName As String
    Public gGpsFileName As String
    Public gFormDefinitionFileName As String

    Public gTerminologyFileName As String

    Public gLoadSuccessful As Boolean

#End Region

#End Region

#Region "Collections"
    Public JobForms As New Generic.List(Of JobForm)
    'Public RequestForms As New Generic.List(Of RequestForm)
#End Region


#Region "Synchronisation Global Variables"
    Public gSyncLog As New SyncLog

    Public gSyncInterval As Integer
    Public gSyncsMissed As Integer
    Public gAutoSyncInterval As Integer
    Public gAutoSync As Boolean
    Public gHoldAutoSync As Boolean
    Public gCancelSync As Boolean

    Public gLastSyncAttempt As DateTime = DateTime.UtcNow
    Public gSyncDays As String
    Public gSyncStartTime As DateTime
    Public gSyncFinishTime As DateTime
    Public gSyncInProgress As Boolean
    Public gSyncThread As Threading.Thread

    'Public gSyncHandler As Synchronisation
    'Private _waitForThread As New AutoResetEvent(False)
#End Region
End Module
