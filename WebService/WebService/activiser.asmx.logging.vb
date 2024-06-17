Option Explicit On
Option Strict On

Imports activiser
Imports activiser.DataSchema
Imports activiser.WebService.Utilities

Imports System
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Reflection
Imports System.Data
Imports activiser.activiserDataSet
Imports activiser.activiserDataSetTableAdapters

Partial Public Class activiserClientWebService

#Region "Error and Event Log Methods"
    Private Shared Sub LogSyncEvent(ByVal syncDateTime As DateTime, ByVal consultantUid As Nullable(Of Guid), ByVal message As String, ByVal syncData As Xml.XmlNode)
        Try
            Dim slta As New SyncLogDataSetTableAdapters.SyncLogTableAdapter
            slta.Insert(Guid.NewGuid(), syncDateTime, consultantUid, message, syncData, DateTime.UtcNow, My.User.Name, DateTime.UtcNow, My.User.Name)
        Catch ex As Exception

        End Try
        'Try
        '    Dim elta As New EventLogDataSetTableAdapters.EventLogTableAdapter
        '    Dim now As DateTime = DateTime.UtcNow
        '    'elta.Connection = Me.conActiviserDB
        '    elta.Insert(EventTypeId, now, consultantUid, RequestUid, JobUid, ClientSiteUid, now, now)
        'Catch ex As Exception
        '    Dim cuid As Guid = If(consultantUid.HasValue, consultantUid.Value, Guid.Empty)
        '    Dim ruid As Guid = If(RequestUid.HasValue, RequestUid.Value, Guid.Empty)
        '    Dim juid As Guid = If(JobUid.HasValue, JobUid.Value, Guid.Empty)

        '    LogError(cuid, DateTime.UtcNow, WebServiceGuid.ToString, MethodBase.GetCurrentMethod.Name, "Error logging event", "EventLog", ruid, juid.ToString, ex)
        'End Try
    End Sub


    ' Web method that allows the consultant to send errors that occur on their Pocket PC application
    ' These errors will be sent automatically on synchronisation after they have occured.
    ' There errors will be added to the Error table in 
    <WebMethod()> _
      Public Function UploadEventLog(ByVal deviceId As String, ByVal dsEvent As EventLogDataSet) As Boolean
        If CheckDeviceId(deviceId) Then
            Try
                Dim elta As New EventLogDataSetTableAdapters.EventLogTableAdapter
                For Each dr As EventLogDataSet.EventLogRow In dsEvent.EventLog
                    elta.Insert(Guid.NewGuid, DateTime.UtcNow, dr.EventDateTime, dr.EventClass, dr.SystemId, dr.Source, dr.HostName, dr.LoggedBy, dr.Message, dr.Status, dr.Notes, dr.EventData, _
                        If(dr.IsCreatedNull, DateTime.UtcNow, dr.Created), _
                        If(dr.IsCreatedByNull, My.User.Name, dr.CreatedBy), _
                        If(dr.IsModifiedNull, DateTime.UtcNow, dr.Modified), _
                        If(dr.IsModifiedByNull, My.User.Name, dr.ModifiedBy))
                Next
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    <WebMethod()> _
  Public Function UploadDeviceTrackingInfo(ByVal deviceId As String, ByVal trackingData As DeviceTrackingDataSet) As Boolean
        If CheckDeviceId(deviceId) Then
            If trackingData Is Nothing Then Return False
            Try
                Using dtta As New DeviceTrackingDataSetTableAdapters.DeviceTrackingTableAdapter
                    For Each dr As DeviceTrackingDataSet.DeviceTrackingRow In trackingData.DeviceTracking
                        dtta.Insert(dr.DeviceTrackingUid, dr.TimeStamp, dr.Username, dr.ConsultantUid, dr.SystemId, dr.TrackingInfo)
                    Next
                End Using
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function

#End Region

End Class