Imports activiser.Library.WebService.activiserDataSet

Partial Class Synchronisation

    Friend Shared Sub SyncGetClientDetails(ByVal owner As Form, ByVal clientSite As ClientSiteRow, ByVal syncHistory As Boolean)
        Const METHODNAME As String = "GetClientDetails"

        If gSyncInProgress Then Return
        gSyncInProgress = True

        Try
            Dim boolRetry As Boolean
            Do
                boolRetry = False
                Try
                    _syncOk = False
                    Startup.InitialiseWebServiceProxy(Nothing, True)

                    LogSyncMessage(RES_GettingClientDetails)
                    If Authenticate() <> AuthenticationStatus.Ok Then
                        Return
                    End If
                    LogSyncMessage(RES_Authenticated)

                    Dim csd As DataSet = gWebServer.GetClientSiteDetails(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID)
                    Merge(gClientDataSet, csd, True)

                    If syncHistory Then
                        LogSyncMessage(RES_GettingClientHistory)

                        Dim dsJob As DataSet = gWebServer.GetJobHistory(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID, gintJobHistoryCount, gintJobHistoryAge)
                        If Not DataSetIsNullOrEmpty(dsJob) Then
                            LogSyncMessage(RES_Merging)
                            Merge(gClientDataSet, dsJob, True)
                            ConsultantConfig.UpdateProfile() ' add downloaded requests and jobs to profile.
                            UploadProfile()
                        End If
                    End If

                    LogSyncMessage(RES_Saving)
                    SavePending(gClientDataSet, gMainDbFileName)

                    _syncOk = True

                Catch ex As System.Net.WebException
                    Select Case Terminology.AskQuestion(owner, MODULENAME, RES_CommsFailureQuestion, MessageBoxButtons.RetryCancel, MessageBoxDefaultButton.Button1)
                        Case Windows.Forms.DialogResult.Cancel
                            LogSyncMessage(RES_Cancelled)
                            Exit Sub
                        Case Windows.Forms.DialogResult.Retry
                            boolRetry = True
                            'Application.DoEvents()
                    End Select
                Catch ex As Exception
                    LogSyncMessage(RES_UnhandledErrorMessage, ex.ToString)
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                Finally

                End Try
            Loop While boolRetry
        Finally
            gSyncInProgress = False
        End Try
    End Sub

    Friend Shared Sub SyncGetClientHistory(ByVal owner As Form, ByVal clientSite As ClientSiteRow)
        Const METHODNAME As String = "GetClientHistory"
        If gSyncInProgress Then Return
        gSyncInProgress = True

        Try
            Dim boolRetry As Boolean
            Do
                boolRetry = False
                Try
                    _syncOk = False
                    Startup.InitialiseWebServiceProxy(Nothing, True)

                    LogSyncMessage(RES_GettingClientHistory)
                    If Authenticate() <> AuthenticationStatus.Ok Then
                        Return
                    End If
                    LogSyncMessage(RES_Authenticated)


                    Dim dsJob As DataSet = gWebServer.GetJobHistory(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID, gintJobHistoryCount, gintJobHistoryAge)
                    If Not DataSetIsNullOrEmpty(dsJob) Then
                        LogSyncMessage(RES_Merging)
                        Merge(gClientDataSet, dsJob, True)
                        ConsultantConfig.UpdateProfile() ' add downloaded requests and jobs to profile.

                        LogSyncMessage(RES_Saving)
                        SavePending(gClientDataSet, gMainDbFileName)
                        UploadProfile()
                    End If

                    _syncOk = True

                Catch ex As System.Net.WebException
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                    LogSyncMessage(RES_CommsFailureMessage, ex.Message)
                    Select Case Terminology.AskQuestion(owner, MODULENAME, RES_CommsFailureQuestion, MessageBoxButtons.RetryCancel, MessageBoxDefaultButton.Button1)
                        Case Windows.Forms.DialogResult.Cancel
                            LogSyncMessage(RES_Cancelled)
                            Exit Sub
                        Case Windows.Forms.DialogResult.Retry
                            boolRetry = True
                            'Application.DoEvents()
                    End Select
                Catch ex As Exception
                    LogSyncMessage(RES_UnhandledErrorMessage, ex.ToString)
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                Finally

                End Try
            Loop While boolRetry
        Finally
            gSyncInProgress = False
        End Try
    End Sub

    Friend Shared Sub SyncGetClientRequests(ByVal owner As Form, ByVal clientSite As ClientSiteRow)
        Const METHODNAME As String = "GetClientRequests"
        If gSyncInProgress Then Return
        gSyncInProgress = True

        Try
            Dim boolRetry As Boolean
            Do
                boolRetry = False
                Try
                    _syncOk = False
                    Startup.InitialiseWebServiceProxy(Nothing, True)

                    LogSyncMessage(RES_GettingClientRequests)
                    If Authenticate() <> AuthenticationStatus.Ok Then
                        Return
                    End If
                    LogSyncMessage(RES_Authenticated)


                    Dim dsJob As DataSet = gWebServer.GetClientSiteOpenRequests(gDeviceIDString, gConsultantUID, clientSite.ClientSiteUID)
                    If Not DataSetIsNullOrEmpty(dsJob) Then
                        LogSyncMessage(RES_Merging)
                        Merge(gClientDataSet, dsJob, True)
                        ConsultantConfig.UpdateProfile() ' add downloaded requests and jobs to profile.

                        LogSyncMessage(RES_Saving)
                        SavePending(gClientDataSet, gMainDbFileName)
                        'UploadProfile()
                    End If
                    _syncOk = True

                Catch ex As System.Net.WebException
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                    LogSyncMessage(RES_CommsFailureMessage, ex.Message)
                    Select Case Terminology.AskQuestion(owner, MODULENAME, RES_CommsFailureQuestion, MessageBoxButtons.RetryCancel, MessageBoxDefaultButton.Button1)
                        Case Windows.Forms.DialogResult.Cancel
                            LogSyncMessage(RES_Cancelled)
                            Exit Sub
                        Case Windows.Forms.DialogResult.Retry
                            boolRetry = True
                            'Application.DoEvents()
                    End Select
                Catch ex As Exception
                    LogSyncMessage(RES_UnhandledErrorMessage, ex.ToString)
                    LogError(MODULENAME, METHODNAME, ex, False, Nothing)
                Finally

                End Try
            Loop While boolRetry
        Finally
            gSyncInProgress = False
        End Try
    End Sub

End Class
