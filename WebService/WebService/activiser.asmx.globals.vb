Imports activiser
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Web.Mail
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports system.Reflection

Partial Public Class activiserClientWebService
    Const csEventSource As String = "activiser"

    Const STR_CreatedDateTime As String = "CreatedDateTime"
    Const STR_ModifiedDateTime As String = "ModifiedDateTime"

    Private Const EventTypeId_RequestInserted As Integer = 1
    Private Const EventTypeId_JobInserted As Integer = 2
    Private Const EventTypeId_RequestUpdated As Integer = 3
    Private Const EventTypeId_JobUpdated As Integer = 4

    Private Shared ReadOnly NullDate As DateTime = DateTime.MinValue

    Friend Shared WebServiceGuid As New Guid("0ABCE90F-330F-4520-963B-86C9F1851DCE")
    Friend Shared OutlookClientGuid As New Guid("c760e9fb-abb7-49c2-b2f7-975eef25d374")
    Friend Shared ConsoleGuid As New Guid("35060af8-9b60-45a9-ac93-5dc6711af0cd")
    'Private configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader

    'Friend appSettings As NameValueCollection
    'Friend connectionStrings As System.Configuration.ConnectionStringSettingsCollection

    'Dont appear to be used anywhere else in the web service.


    'Private Shared ReadOnly Version As String = My.Application.Info.Version.ToString(2)
    'Private Shared ReadOnly Build As String = My.Application.Info.Version.ToString(4) ' "V3.2.7.21"



End Class