Imports activiser.Library.WebService

Partial Friend Class Synchronisation

    Private Shared _profileUpdateThread As Threading.Thread
    Friend Shared ReadOnly Property ProfileUpdateThread() As Threading.Thread
        Get
            Return _profileUpdateThread
        End Get
    End Property

    Friend Shared Function StartProfileUpdateThread() As Threading.Thread
        _profileUpdateThread = New Threading.Thread(AddressOf ConsultantConfig.UpdateProfile)
        _profileUpdateThread.Start()
        Return _profileUpdateThread
    End Function


    Friend Shared Sub UploadProfile()
        If gCancelSync Then Return
        SetState(RES_ProfileSyncing)
        If _profileUpdateThread IsNot Nothing Then
            _profileUpdateThread.Join()
        End If

        If ConsultantConfig.ConsultantItemDataSet.HasChanges Then
            gWebServer.UploadUserItems(gDeviceIDString, gConsultantUID, CType(ConsultantConfig.ConsultantItemDataSet.GetChanges(), ConsultantItemDataSet))
        End If

        ' this dataset should only have one row anyway, so we'll just upload the whole thing.
        If ConsultantConfig.ConsultantSettingsDataSet.HasChanges Then
            gWebServer.UploadUserSettings(gDeviceIDString, gConsultantUID, ConsultantConfig.ConsultantSettingsDataSet)
        End If

        ConsultantConfig.SaveFull()
    End Sub
End Class
