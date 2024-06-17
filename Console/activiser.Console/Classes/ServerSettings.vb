Option Strict Off
Option Infer On

Imports activiser.Library.activiserWebService.Utility

Friend Module ServerSettings
    Private lastSettingCheck As DateTime = DateTime.MinValue
    Private currentSettings As ServerSettingDataTable ' = ConsoleData.WebService.ConsoleGetServerProfile(deviceId, ConsoleUser.ConsultantUID, True)

    Function GetCurrentSetting(ByVal name As String) As String
        Dim thisSettingCheck = DateTime.UtcNow
#If DEBUG Then
        If currentSettings Is Nothing OrElse lastSettingCheck.AddMinutes(1) > DateTime.UtcNow Then
#Else
        If currentSettings Is Nothing OrElse lastSettingCheck.AddMinutes(10) > DateTime.UtcNow Then
#End If
            If ConsoleData.WebService.ConsoleGetLastServerSettingChange(deviceId) > lastSettingCheck Then
                currentSettings = ConsoleData.WebService.ConsoleGetServerProfile(deviceId, ConsoleUser.ConsultantUID, True)
            End If
        End If
        lastSettingCheck = thisSettingCheck

        Try
            For Each ss As ServerSettingRow In currentSettings
                If ss.Name = name AndAlso ss.Status = 0 Then
                    Return ss.Value
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Module
