Imports activiser.Library
#If MINORPLANETCLIENT Then
Imports activiser.Library.MinorPlanet
Imports System.Text
#Else
Imports activiser.Library.Gps
#End If

Module Gps
    Private Const MODULENAME As String = "GPS"
    Private Const RES_InitTimeout As String = "InitTimeout"
    Private Const RES_InfoInvalid As String = "InfoInvalid"
    Private Const RES_PositionInfo As String = "PositionInfo"
    Private Const RES_NoInfo As String = "NoInfo"
    Private Const RES_Off As String = "OffText"
    Private Const RES_TrackingInterval As String = "TrackingInterval"
    Private Const RES_LocationUnknown As String = "LocationUnknown"

#If MINORPLANETCLIENT Then
    Public gGPS As activiser.Library.MinorPlanet.DcuClient '.Gps.GpsClient
#Else
    Public gGPS As activiser.Library.Gps.GpsClient
#End If


    Friend HaveValidPosition As Boolean '= False
    Friend GpsEnabled As Boolean '= False

    'Friend LastKnownPosition As GpsPosition
    Friend LastKnownValidPosition As GpsPosition
#If MINORPLANETCLIENT Then

#Else
    Friend DeviceState As GpsDeviceState
#End If

    Friend ErrorState As String
    Friend TrackingInfoFormat As String

    Friend TrackingData As WebService.DeviceTrackingDataSet

    Private lastRecordedPosition As GpsPosition
    Private WithEvents TrackingPoller As System.Threading.Timer ' Timer
    Private _trackingInterval As Integer '= 0
    Private startupThread As Threading.Thread

    Private _paused As Boolean
    Private _abort As Boolean

    Private _hideGps As Boolean = AppConfig.GetSetting(My.Resources.AppConfigHideGpsKey, False)

    Public Property HideGps() As Boolean
        Get
            Return _hideGps
        End Get
        Set(ByVal value As Boolean)
            _hideGps = value
            AppConfig.SaveSetting(My.Resources.AppConfigHideGpsKey, value)
        End Set
    End Property

    Public Property TrackingInterval() As Integer
        Get
            Return _trackingInterval
        End Get
        Set(ByVal value As Integer)
            _trackingInterval = value
        End Set
    End Property

    Private Sub GpsOpener()
        Try
            If gGPS Is Nothing Then gGPS = New activiser.Library.Gps.GpsClient()
            If Not gGPS.Opened Then gGPS.Open()

            DeviceState = GpsClient.GetDeviceState

            AddHandler gGPS.DeviceStateChanged, AddressOf GpsDeviceStateChangedHandler
            AddHandler gGPS.PositionChanged, AddressOf GpsPositionChangedHandler

            ' wait for device to initialize - give it 30 seconds
            For i As Integer = 1 To 30
                Threading.Thread.Sleep(1000)
                If _abort Then Return
                If DeviceState.DeviceState = GpsServiceState.On AndAlso DeviceState.ServiceState = GpsServiceState.On Then
                    Return
                End If
            Next

            If DeviceState.DeviceState <> GpsServiceState.On OrElse DeviceState.ServiceState <> GpsServiceState.On Then
                Throw New GpsException(Terminology.GetString(MODULENAME, RES_InitTimeout))
            End If

        Catch ex As Exception
            If gGPS IsNot Nothing Then
                gGPS.Close()
                gGPS = Nothing
            End If
        Finally
            SetMenuStates()
        End Try
    End Sub

    Private Sub GpsStarter()
        ' first load existing tracking data, if any
        TrackingData = New WebService.DeviceTrackingDataSet
        Serialisation.ReloadPendingTransactions(TrackingData, gGpsFileName)

        Dim t As New Threading.Thread(AddressOf GpsOpener)
        t.Name = "GPS Opener"
        t.IsBackground = True
        t.Priority = Threading.ThreadPriority.Lowest
        t.Start()

        For i As Integer = 1 To 60 ' wait a bit
            Threading.Thread.Sleep(1000)
            Application.DoEvents()
            If gGPS IsNot Nothing AndAlso gGPS.Opened Then
                Return
            End If
        Next
        'TODO: display warning
        ErrorState = RES_InitTimeout
        t.Abort()
        t = Nothing
        Return
    End Sub

    Friend Sub StartGPS()
        TrackingInterval = AppConfig.GetSetting(RES_TrackingInterval, 5)

#If MINORPLANETCLIENT Then
        'SetMenuStates() ' start in 'off' state
#Else
        SetMenuStates() ' start in 'off' state
#End If
        startupThread = New Threading.Thread(AddressOf GpsStarter)
        startupThread.IsBackground = True
        startupThread.Priority = Threading.ThreadPriority.BelowNormal
        startupThread.Name = "GPS Starter"
        startupThread.Start()
    End Sub

    Friend Sub StopGps()
        _abort = True
        If gGPS IsNot Nothing Then
#If MINORPLANETCLIENT Then
            RemoveHandler gGPS.DeviceIdChanged, AddressOf GpsDeviceStateChangedHandler
            RemoveHandler gGPS.DcuVersionChanged, AddressOf GpsDeviceStateChangedHandler
#Else
            RemoveHandler gGPS.DeviceStateChanged, AddressOf GpsDeviceStateChangedHandler
#End If

            RemoveHandler gGPS.PositionChanged, AddressOf GpsPositionChangedHandler

            gGPS.Dispose()
            gGPS = Nothing
            TrackingPoller = Nothing '.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
            SavePending(TrackingData, gGpsFileName)
            TrackingData.Dispose()
            TrackingData = Nothing
        End If
#If MINORPLANETCLIENT Then
        'SetMenuStates()
#Else
        SetMenuStates()
#End If
    End Sub

#Region "Pause"
    Friend Sub Pause()
        Paused = True
    End Sub

    Friend Sub [Resume]()
        Paused = False
    End Sub

    Friend Property Paused() As Boolean
        Get
            Return _paused
        End Get
        Set(ByVal value As Boolean)
            _paused = value
        End Set
    End Property

#End Region

#If MINORPLANETCLIENT Then
#Else
    Public Sub SetMenuStates()
        If gMainForm Is Nothing Then Return

        If _hideGps Then Return

        Dim menuState As GpsServiceState = GpsServiceState.Off

        If gGPS IsNot Nothing AndAlso gGPS.Opened Then
            If DeviceState IsNot Nothing Then
                If DeviceState.DeviceState = GpsServiceState.On AndAlso DeviceState.ServiceState = GpsServiceState.On Then
                    menuState = GpsServiceState.On
                Else
                    menuState = GpsServiceState.Off
                End If
            End If
        End If

        gMainForm.SetGpsMenuState(menuState)
        For Each j As JobForm In JobForms
            If j IsNot Nothing Then
                Try
                    j.SetGpsMenuState(menuState)
                Catch ex As ObjectDisposedException
                End Try
            End If
        Next
        Application.DoEvents()
    End Sub
#End If

    Friend Sub GpsDeviceStateChangedHandler(ByVal sender As Object, ByVal e As DeviceStateChangedEventArgs)
        If e IsNot Nothing Then
            DeviceState = e.DeviceState
        Else
            DeviceState = Nothing
        End If
        SetMenuStates()
        If DeviceState IsNot Nothing Then
            If DeviceState.DeviceState = GpsServiceState.On AndAlso DeviceState.ServiceState = GpsServiceState.On Then
                If TrackingInterval <> 0 Then
                    logLastKnownValidPosition()

                    If TrackingPoller Is Nothing Then TrackingPoller = New System.Threading.Timer(AddressOf TrackingPoller_Tick, Nothing, 2000, TrackingInterval * 60000)
                    Threading.Thread.Sleep(1000)
                End If
            Else
                If TrackingPoller IsNot Nothing Then
                    TrackingPoller.Dispose()
                End If
                TrackingPoller = Nothing
                'RemoveHandler TrackingPoller.Tick, AddressOf TrackingPoller_Tick
            End If
        End If
    End Sub

    Friend Sub GpsPositionChangedHandler(ByVal sender As Object, ByVal e As PositionChangedEventArgs)
        'LastKnownPosition = e.Position
        HaveValidPosition = e.Position.PositionValid
        If _hideGps Then Return

        If gMainForm Is Nothing Then Return

        Try
            If HaveValidPosition Then
                ErrorState = Nothing
                LastKnownValidPosition = e.Position

                If gMainForm.IsVisible Then
                    gMainForm.SetGpsLabelText(LastKnownValidPosition.ToString("l", WithoutCulture))
                End If
            Else
                If gMainForm.IsVisible Then
                    gMainForm.SetGpsLabelText(Terminology.GetString(MODULENAME, RES_LocationUnknown))
                End If
                ErrorState = RES_InfoInvalid
            End If
        Catch ex As ObjectDisposedException
            gGPS.Dispose()
        End Try
    End Sub

    Friend Sub DisplayGpsInfo(ByVal owner As Form)
        If LastKnownValidPosition Is Nothing Then
            If ErrorState IsNot Nothing Then
                Terminology.DisplayMessage(owner, MODULENAME, ErrorState, MessageBoxIcon.Asterisk)
            Else
                Terminology.DisplayMessage(owner, MODULENAME, RES_NoInfo, MessageBoxIcon.Asterisk)
            End If
        Else
            Dim state As String
            If gGPS IsNot Nothing AndAlso gGPS.Running Then
#If MINORPLANETCLIENT Then
                state = Terminology.GetString(MODULENAME, RES_OK)  ' DeviceState.DeviceState.ToString() & "/" & DeviceState.ServiceState.ToString()
#Else
                state = DeviceState.DeviceState.ToString() & "/" & DeviceState.ServiceState.ToString()
#End If
            Else
                state = Terminology.GetString(MODULENAME, RES_Off)
            End If

            Terminology.DisplayFormattedMessage(owner, MODULENAME, RES_PositionInfo, MessageBoxIcon.Asterisk, _
                LastKnownValidPosition.LatitudeInDegreesMinutesSeconds.ToString(), _
                LastKnownValidPosition.LongitudeInDegreesMinutesSeconds.ToString(), _
                LastKnownValidPosition.SeaLevelAltitude.ToString("0", WithoutCulture), _
                LastKnownValidPosition.Time.ToString("g", WithoutCulture), _
                state)
        End If
    End Sub

    Private Sub TrackingPoller_Tick(ByVal state As Object)
        If Paused Then Return
        If LastKnownValidPosition IsNot Nothing Then
            logLastKnownValidPosition()
        End If
    End Sub

    Private Sub logLastKnownValidPosition()
        If Paused Then Return
        If LastKnownValidPosition IsNot Nothing Then
            If lastRecordedPosition IsNot Nothing AndAlso Not lastRecordedPosition.HasMovedFrom(LastKnownValidPosition) Then
                Return ' no change
            Else
                lastRecordedPosition = LastKnownValidPosition
                Dim tr As WebService.DeviceTrackingDataSet.DeviceTrackingRow
                Dim trackingInfo As String = lastRecordedPosition.ToString(TrackingInfoFormat, WithoutCulture)
                tr = TrackingData.DeviceTracking.AddDeviceTrackingRow(Guid.NewGuid, lastRecordedPosition.Time, ConsultantConfig.Name, gDeviceIDString, trackingInfo, gConsultantUID)

                ConsultantConfig.ConsultantRow.TrackingInfo = trackingInfo
                ConsultantConfig.ConsultantRow.TrackingTimestamp = lastRecordedPosition.Time
                ConsultantConfig.SavePending()

                If AppConfig.GetSetting("AutoSyncGPS", False) Then Synchronisation.UploadGpsLog()
                SavePending(TrackingData, gGpsFileName)
            End If
        End If
    End Sub
End Module
