Imports activiser
Imports activiser.WebService.Utilities
Imports System
Imports System.Collections
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Web.Services
Imports system.Reflection
Imports System.Data
Imports System.Collections.Generic

Partial Public Class activiserClientWebService
    <WebMethod()> _
    Public Function SchemaUpdateRequired(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal lastCheck As DateTime) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return lastCheck < DataSchema.LastSchemaChange
    End Function

    <WebMethod()> _
    Public Function GetClientDataSetSchema(ByVal deviceId As String, ByVal consultantUid As Guid) As activiserDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return DataSchema.GetClientSchema(DataSchema.SchemaClientMask.Mobile)
    End Function

    <WebMethod()> _
    Public Function GetClientDataSetSchemaCompressed(ByVal deviceId As String, ByVal consultantUid As Guid) As Byte()
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return CompressString(DataSchema.GetClientSchema(DataSchema.SchemaClientMask.Mobile).GetXml())
    End Function

    <WebMethod()> _
    Public Function GetClientDataSetSchemaMasked(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal clientMask As Integer) As activiserDataSet
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return DataSchema.GetClientSchema(clientMask)
    End Function

    <WebMethod()> _
    Public Function GetClientDataSetSchemaMaskedCompressed(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal clientMask As Integer) As Byte()
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return CompressString(DataSchema.GetClientSchema(clientMask).GetXml())
    End Function

    <WebMethod()> _
        Public Function FormUpdateRequired(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal lastCheck As DateTime) As Boolean
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return lastCheck < FormDefinition.LastFormChange
    End Function

    <WebMethod()> _
    Public Function GetFormDefinitions(ByVal deviceId As String, ByVal consultantUid As Guid) As FormDefinition
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return FormDefinition.GetFormDefinition(DataSchema.SchemaClientMask.All, Nothing)
    End Function

    <WebMethod()> _
    Public Function GetFormDefinitionsMasked(ByVal deviceId As String, ByVal consultantUid As Guid, ByVal clientMask As Integer, ByVal since As DateTime?) As FormDefinition
        If Not CheckDeviceId(deviceId) Then Return Nothing

        Return FormDefinition.GetFormDefinition(clientMask, since) 'If(since > DateTime.MinValue, since, Nothing))
    End Function
End Class