Imports activiser.Library.activiserWebService
Imports System.Collections.Specialized

Module ServerSideSettings
    Friend Function DeserializeStringArray(ByVal value As String) As String()
        Dim serializer As New Xml.Serialization.XmlSerializer(GetType(String()))
        Dim sr As New IO.StringReader(value)
        Dim xr As New Xml.XmlTextReader(sr)
        If serializer.CanDeserialize(xr) Then
            Dim result As String() = CType(serializer.Deserialize(xr), String())
            Return result
        Else
            Return Nothing
        End If
    End Function


    Friend Sub LoadServerSettings()
        Try
            Dim spt = ConsoleData.WebService.ConsoleGetSettings(deviceId, ConsoleUser.ConsultantUID)
            If spt IsNot Nothing Then
                For Each sp As ClientSettingDataSet.ClientSettingRow In spt
                    Select Case sp.Name
                        Case "JobSheetTemplateFile"
                            My.Settings.JobSheetTemplateFile = sp.Value
                        Case "JobsReadOnly"
                            My.Settings.JobsReadOnly = CBool(sp.Value)
                            My.Settings.JobsAllowEdits = Not My.Settings.JobsReadOnly
                        Case "RequestsReadOnly"
                            My.Settings.RequestsReadOnly = CBool(sp.Value)
                            My.Settings.RequestsAllowEdits = Not My.Settings.RequestsReadOnly
                        Case "JobTreeUserNodes"
                            My.Settings.JobTreeUserNodes.Clear()
                            My.Settings.JobTreeUserNodes.AddRange(DeserializeStringArray(sp.Value))
                        Case "JobTreeAdminNodes"
                            My.Settings.JobTreeAdminNodes.Clear()
                            My.Settings.JobTreeAdminNodes.AddRange(DeserializeStringArray(sp.Value))
                        Case "JobTreeManagementNodes"
                            My.Settings.JobTreeManagementNodes.Clear()
                            My.Settings.JobTreeManagementNodes.AddRange(DeserializeStringArray(sp.Value))
                        Case "LockRequestNumber"
                            My.Settings.LockRequestNumber = CBool(sp.Value)
                        Case "LockJobOnManagementApproval"
                            My.Settings.LockJobOnManagementApproval = CBool(sp.Value)
                        Case "LockJobOnAdminApproval"
                            My.Settings.LockJobOnAdminApproval = CBool(sp.Value)
                        Case "DeviceTrackingTemplateFile"
                            My.Settings.DeviceTrackingTemplateFile = sp.Value
                        Case "DefaultLatitude"
                            My.Settings.DefaultLatitude = CDbl(sp.Value)
                        Case "DefaultLongitude"
                            My.Settings.DefaultLongitude = CDbl(sp.Value)
                        Case "JobsDisableJobNumberEditing"
                            My.Settings.JobsDisableJobNumberEditing = CBool(sp.Value)
                    End Select
                Next
                My.Settings.Save()
            End If
        Catch ex As Exception
            TraceError(ex)
            'otherwise, we won't bother the user with it...
        End Try
    End Sub
End Module
