Partial Friend Class Synchronisation
    Private Shared _retryToken As String = "#RETRY#"
    Private Shared _retryMessage As String = Terminology.GetString(MODULENAME, RES_TryAgain)

    Friend Shared Sub LogSyncMessage(ByVal messageId As String, ByVal ParamArray args() As String)
        Const METHODNAME As String = "LogSyncMessage"

        Try
            Dim message As String
            If args Is Nothing OrElse args.Length = 0 Then
                message = Terminology.GetString(MODULENAME, messageId).Replace(_retryToken, _retryMessage)
            Else
                message = Terminology.GetFormattedString(MODULENAME, messageId, args).Replace(_retryToken, _retryMessage)
            End If

            gSyncLog.AddEntry(message)

        Catch ex As ObjectDisposedException ' hopper trapped
            LogError(MODULENAME, METHODNAME, ex, True, RES_ObjectDisposedExceptionMessage, DateTime.Now, ex.StackTrace)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, RES_ErrorSettingStatusText)
        End Try
    End Sub
End Class

