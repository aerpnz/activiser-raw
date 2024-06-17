Partial Friend Class Synchronisation
    Friend Shared Sub UploadGpsLog()
        If gCancelSync Then Return
        If TrackingData.DeviceTracking.Count = 0 Then Return ' nothing to upload.

        Dim _gpsActive As Boolean = Not Gps.Paused
        Try
            ' pause logging tracking info, we don't want to be uploading data while it's updating.
            If _gpsActive Then Gps.Pause()
            LogSyncMessage(RES_UploadingTrackingInformation)

            If gWebServer.UploadDeviceTrackingInfo(gDeviceIDString, TrackingData) Then
                TrackingData.Clear()
                'Serialisation.StartSavePending(TrackingData, gGpsFileName)
            End If
        Catch ex As Exception
            LogSyncMessage("ErrorUploadingTrackingData")
        Finally
            If _gpsActive Then Gps.Resume()
        End Try
    End Sub
End Class

