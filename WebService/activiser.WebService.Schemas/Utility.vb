Partial Class Utility

    Partial Class ActiveConsultantsDataTable

        Private Sub ActiveConsultantsDataTable_ActiveConsultantsRowChanging(ByVal sender As System.Object, ByVal e As ActiveConsultantsRowChangeEvent) Handles Me.ActiveConsultantsRowChanging

        End Sub

    End Class

    Private Shared uta As New UtilityTableAdapters.Utilities

    Public Shared Function PingServer() As Boolean
        Return Not String.IsNullOrEmpty(CStr(uta.PingServer()))
    End Function

    Public Shared Function GetServerTime() As Date
        Dim result As Date? = CType(uta.GetServerTime(), Date?)
        If result.HasValue Then
            Return Date.SpecifyKind(result.Value, DateTimeKind.Utc)
        Else
            Return Date.UtcNow
        End If
    End Function

    Public Shared Function GetSqlLoginName() As String
        Return CStr(uta.GetSqlLoginName)
    End Function

    Public Shared Function GetConsultantSyncTime(ByVal consultantUid As Guid) As Date
        Dim result As Date? = CType(uta.ConsultantGetSyncTime(consultantUid), Date?)
        If result.HasValue Then
            Return Date.SpecifyKind(result.Value, DateTimeKind.Utc)
        Else
            Return Date.MinValue
        End If
    End Function

    Public Shared Function GetConsultantLastSyncTime(ByVal consultantUid As Guid) As Date
        Dim result As Date? = CType(uta.ConsultantGetLastSyncTime(consultantUid), Date?)
        If result.HasValue Then
            Return Date.SpecifyKind(result.Value, DateTimeKind.Utc)
        Else
            Return Date.MinValue
        End If
    End Function

    Public Shared Function ConsultantStartSync(ByVal ConsultantUid As Guid) As Date
        Try
            Dim result As Date?
            If uta.ConsultantStartSync(ConsultantUid, result) <> 0 AndAlso result.HasValue Then
                Return Date.SpecifyKind(result.Value, DateTimeKind.Utc)
            Else
                Return Date.MinValue
            End If
        Catch ex As Exception
            Return Date.MinValue
        End Try
    End Function

    Public Shared Function ConsultantCompleteSync(ByVal ConsultantUid As Guid) As Date
        Try
            If uta.ConsultantCompleteSync(ConsultantUid) > 0 Then
                Dim result As Date? = CType(uta.ConsultantGetLastSyncTime(ConsultantUid), Date?)
                If result.HasValue Then
                    Return Date.SpecifyKind(result.Value, DateTimeKind.Utc)
                Else
                    Return Date.UtcNow
                End If
            Else
                Return Date.MinValue
            End If
        Catch ex As Exception
            Return Date.MinValue
        End Try
    End Function

    Public Shared Function GetConsultantSyncInterval(ByVal consultantUid As Guid) As Integer
        Dim result As Integer? = CType(uta.ConsultantGetSyncInterval(consultantUid), Integer?)
        If result.HasValue Then
            Return result.Value
        Else
            Return 0
        End If
    End Function

    Public Shared Function RequestExists(ByVal uid As Guid) As Boolean
        Dim result As Integer? = CType(uta.RequestExists(uid), Integer?)
        Return result.HasValue AndAlso result.Value <> 0
    End Function

    Public Shared Function JobExists(ByVal uid As Guid) As Boolean
        Dim result As Integer? = CType(uta.JobExists(uid), Integer?)
        Return result.HasValue AndAlso result.Value <> 0
    End Function

End Class
