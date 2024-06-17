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
Imports System.ComponentModel

Partial Public Class activiserClientWebService

#Region "Profile Web Service Methods"

    ' Gets the user's profile settings from the Pocket PC and stores them in the database.
    <WebMethod()> _
    Public Function UploadUserItems(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal items As ConsultantItemDataSet) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing
        If items Is Nothing Then Return Nothing

        Try
#If DEBUG Then
            For Each i As ConsultantItemDataSet.ConsultantItemRow In items.ConsultantItem
                If i.RowState = DataRowState.Deleted Then Continue For
                If i.IsNull(items.ConsultantItem.ConsultantItemIdColumn) Then
                    i.ConsultantItemId = Guid.NewGuid()
                End If
            Next
#End If
            Using ta As New ConsultantItemDataSetTableAdapters.ConsultantItemTableAdapter
                ta.Update(items)
            End Using

            Return True
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading consultant profile", "ConsultantProfile", ConsultantUid, "", ex)
            Return False
        End Try
    End Function

    ' Gets the user's profile settings from the Pocket PC and stores them in the database.
    <WebMethod()> _
    Public Function UploadUserSettings(ByVal deviceId As String, ByVal ConsultantUid As Guid, ByVal settings As ConsultantSettingDataSet) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing
        If settings Is Nothing Then Return Nothing

        Try
            Using ta As New ConsultantSettingDataSetTableAdapters.ConsultantSettingTableAdapter
                ta.Update(settings)
            End Using

            Return True
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error uploading consultant profile", "ConsultantProfile", ConsultantUid, "", ex)
            Return False
        End Try
    End Function

    ' Sends the user's profile settings from the database to the user.
    <WebMethod()> _
    Public Function GetUserProfile(ByVal deviceId As String, ByVal ConsultantUid As Guid) As ConsultantSettingDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim ds As New ConsultantSettingDataSet

            Dim csta As New ConsultantSettingDataSetTableAdapters.ConsultantSettingTableAdapter

            csta.FillForConsultant(ds.ConsultantSetting, ConsultantUid, deviceId)

            Return ds

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting consultant profile", "ConsultantProfile", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function

    ' Sends the user's profile settings from the database to the user.
    <WebMethod()> _
    Public Function GetUserItems(ByVal deviceId As String, ByVal ConsultantUid As Guid) As ConsultantItemDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Try
            Dim ds As New ConsultantItemDataSet

            Dim cita As New ConsultantItemDataSetTableAdapters.ConsultantItemTableAdapter

            cita.FillForConsultant(ds.ConsultantItem, ConsultantUid, deviceId)

            Return ds

        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting consultant profile", "ConsultantProfile", ConsultantUid, "", ex)
            Return Nothing
        End Try
    End Function

    <Obsolete("Use GetConsultantSyncInterval instead."), _
    EditorBrowsable(EditorBrowsableState.Never), _
    WebMethod()> _
    Public Function GetAutoSyncInterval(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Integer
        Return GetConsultantSyncInterval(deviceId, ConsultantUid)
    End Function

    <WebMethod()> _
        Public Function GetConsultantSyncInterval(ByVal deviceId As String, ByVal ConsultantUid As Guid) As Integer
        If Not CheckDeviceId(deviceId) Then Return Nothing
        Try
            Return Utility.GetConsultantSyncInterval(ConsultantUid)
        Catch ex As Exception
            LogError(ConsultantUid, DateTime.UtcNow, deviceId, MethodBase.GetCurrentMethod.Name, "Error getting consultant sync interval", "ConsultantProfile", ConsultantUid, "", ex)
            Return 0
        End Try
    End Function

#End Region

End Class
