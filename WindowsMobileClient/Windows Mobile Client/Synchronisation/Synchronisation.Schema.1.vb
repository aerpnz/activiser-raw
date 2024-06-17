Imports activiser.Library.WebService

Partial Class Synchronisation
    Private Shared Function UpdateTerminology(ByVal lastSchemaCheck As DateTime) As Boolean
        Try
            LogSyncMessage(RES_CheckingCustomTerms)
            Dim terminologyUpdateDs As LanguageDataSet = gWebServer.GetTerminology(gDeviceIDString, gConsultantUID, Terminology.ClientKey, AppConfig.GetSetting(My.Resources.AppConfigLanguageIdKey, CInt(My.Resources.AppConfigLanguageIdDefault)), lastSchemaCheck.AddDays(-7))
            If (terminologyUpdateDs.StringValue.Count <> 0) Then
                LogSyncMessage(RES_UpdatingCustomTerms)
                Terminology.Merge(terminologyUpdateDs)
                Return True
            End If
        Catch ex As Exception
            'Catch the exception. This will catch the exception if the webservice does not exist.
            LogError(MODULENAME, "UpdateTerminology", ex, False, RES_ErrorGettingTerminologyUpdates)
        End Try
        Return False
    End Function


    Private Shared Function SchemaCheckRequired(ByVal schemaCheckTime As Date, ByVal lastSchemaCheck As Date) As Boolean
        Try
            Dim schemaCheckInterval As Integer = AppConfig.GetSetting(My.Resources.AppConfigSchemaCheckIntervalKey, 1)
            If schemaCheckInterval Mod 1440 = 0 Then ' multiple of a day; work on the date only
                If lastSchemaCheck.Date.AddMinutes(schemaCheckInterval) >= schemaCheckTime Then
                    Return False
                End If
            Else
                If lastSchemaCheck.AddMinutes(schemaCheckInterval) >= schemaCheckTime Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return True
        End Try
    End Function


    Private Shared Function UpdateSchema(ByVal since As Date) As DataSet
        Dim dsReturnDataset As DataSet
        gSyncLog.AddEntry(Terminology.GetString(MODULENAME, RES_UpdatingSchema))
        dsReturnDataset = gWebServer.GetClientDataSetSchema(gDeviceIDString, gConsultantUID)
        If gCancelSync Then Throw New SyncCanceledException()

        gClientDataSet.Merge(dsReturnDataset, False, MissingSchemaAction.AddWithKey)
        SaveSchema(gClientDataSet, gMainDbFileName)

        Dim fds As FormDefinition = gWebServer.GetFormDefinitionsMasked(gDeviceIDString, gConsultantUID, SchemaClients.Mobile, since)
        gFormDefinitions.Merge(fds)
        gFormDefinitions.AcceptChanges()
        SaveSchema(gFormDefinitions, gFormDefinitionFileName)
        SaveCommitted(gFormDefinitions, gFormDefinitionFileName)

        SaveSchema(ConsultantConfig.ConsultantItemDataSet, gLocalItemsFileName)
        SaveSchema(ConsultantConfig.ConsultantSettingsDataSet, gConfigDbFileName)
        Return dsReturnDataset
    End Function

    Friend Shared Function GetSchemaAndTerminologyUpdates(Optional ByVal force As Boolean = False) As Boolean
        Dim schemaUpdated As Boolean = False
        Dim schemaCheckTime As Date = DateTime.UtcNow
        Dim lastSchemaCheck As Date = DateTime.SpecifyKind(AppConfig.GetSetting(My.Resources.AppConfigLastSchemaCheckKey, DateTime.MinValue), DateTimeKind.Utc)

        If (Not force) AndAlso (Not SchemaCheckRequired(schemaCheckTime, lastSchemaCheck)) Then Return False

        Try
            gSyncLog.AddEntry(Terminology.GetString(MODULENAME, RES_CheckingSchema))

            If gCancelSync Then Exit Try

            If force _
                OrElse gWebServer.SchemaUpdateRequired(gDeviceIDString, gConsultantUID, lastSchemaCheck) _
                OrElse gWebServer.FormUpdateRequired(gDeviceIDString, gConsultantUID, lastSchemaCheck) _
                Then
                If gCancelSync Then Exit Try
                UpdateSchema(lastSchemaCheck)
                schemaUpdated = True
                If gCancelSync Then Exit Try
            Else
                LogSyncMessage(RES_CheckingSchemaNone)
            End If

            If UpdateTerminology(lastSchemaCheck) Then schemaUpdated = True
            If gCancelSync Then Exit Try

            AppConfig.SaveSetting(My.Resources.AppConfigLastSchemaCheckKey, schemaCheckTime)

            AppConfig.Save()

        Catch ex As Exception
            Throw
        Finally

        End Try

        If gCancelSync Then Throw New SyncCanceledException()
        Return schemaUpdated
    End Function

End Class
