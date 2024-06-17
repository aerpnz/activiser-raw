Imports activiser
Imports activiser.WebService.Utilities
Imports System
Imports System.Diagnostics
Imports System.Web.Services
Imports system.Reflection
Imports activiser.WebService.Gateway
Imports activiser.WebService.Gateway.GatewayDefinitionTableAdapters
Imports System.IO
Imports activiser.WebService.Gateway.GatewayTransactionDataSetTableAdapters
Imports System.Collections.Generic
Imports System.Data
Imports System.Text
Imports System.Xml

Partial Public Class activiserClientWebService

    Private Structure OutputGatewayItem
        Public Name As String
        Public Assembly As Assembly
        Public OutputClass As System.Type
        Public Id As Guid

        Public Sub New(ByVal name As String, ByVal assembly As Assembly, ByVal outputClass As System.Type, ByVal id As Guid)
            Me.Name = name
            Me.Assembly = assembly
            Me.OutputClass = outputClass
            Me.Id = id
        End Sub
    End Structure

    Private Shared outputGateways As New List(Of OutputGatewayItem)

    Private Shared Sub LoadGatewayList()
        Using gatewayListTA As New GatewayTableAdapter()
            gatewayListTA.Connection = New SqlClient.SqlConnection(My.Settings.activiserConnectionString)

            Using gatewayList As GatewayDefinition.GatewayDataTable = gatewayListTA.GetData()
                Dim name, assemblyName, className As String
                For Each gateway As GatewayDefinition.GatewayRow In CType(gatewayList.Select(Nothing, gatewayList.OutputPriorityColumn.ColumnName), GatewayDefinition.GatewayRow())
                    If gateway.Status = 0 Then Continue For ' 0 == disabled.
                    name = gateway.Name
                    If name = "NULL" Then Continue For ' dummy, test entry.

                    assemblyName = gateway.OutputAssemblyPath
                    className = gateway.OutputClassName
                    If Not String.IsNullOrEmpty(name) AndAlso Not String.IsNullOrEmpty(assemblyName) AndAlso Not String.IsNullOrEmpty(className) Then
                        Try
                            Dim assemblyFullPath As String
                            If Not assemblyName.Contains(System.IO.Path.DirectorySeparatorChar) AndAlso Not assemblyName.Contains(System.IO.Path.AltDirectorySeparatorChar) Then ' no path
                                Dim executing As String = New Uri(Assembly.GetExecutingAssembly().GetName.CodeBase).AbsolutePath
                                Dim fi As New IO.FileInfo(executing)
                                assemblyFullPath = System.IO.Path.Combine(fi.DirectoryName, assemblyName)
                            Else
                                assemblyFullPath = assemblyName
                            End If

                            Dim ogAssembly As Assembly
                            ogAssembly = Reflection.Assembly.Load(Reflection.AssemblyName.GetAssemblyName(assemblyFullPath))
                            Dim ogType As System.Type = ogAssembly.GetType(className, True)
                            Dim ogIf As System.Type = ogType.GetInterface("IOutputGateway")

                            If ogIf IsNot Nothing Then
                                outputGateways.Add(New OutputGatewayItem(name, ogAssembly, ogType, ogType.GUID))
                            End If
                        Catch ex As Exception
                            LogError(WebServiceGuid, DateTime.Now, WebServiceGuid.ToString(), "LoadGatewayList", "Error loading gateway list", "", Guid.Empty, "", ex)
                        End Try
                    End If
                Next
            End Using
        End Using

    End Sub

    Private Shared Function GetGatewayInstance(ByVal ogi As OutputGatewayItem) As WebService.OutputGateway.IOutputGateway
        Try
            Dim result As activiser.WebService.OutputGateway.IOutputGateway
            result = CType(Activator.CreateInstance(ogi.OutputClass), WebService.OutputGateway.IOutputGateway)
            result.ConnectionString = My.Settings.activiserConnectionString
            Return result
        Catch ex As TargetInvocationException
            LogError(ogi.Id, DateTime.UtcNow(), WebServiceGuid.ToString, "GetGatewayInstance", "Error getting gateway instance", Nothing, ogi.Id, String.Format("{0};{1}", ogi.Name, ogi.OutputClass.FullName), ex)
        Catch ex As Exception
            LogError(ogi.Id, DateTime.UtcNow(), WebServiceGuid.ToString, "GetGatewayInstance", "Error getting gateway instance", Nothing, ogi.Id, String.Format("{0};{1}", ogi.Name, ogi.OutputClass.FullName), ex)
            Debug.WriteLine(ex.ToString())
        End Try
        Return Nothing
    End Function

    Private Sub SaveToGateway(ByVal transactionId As Guid, ByVal ds As activiserDataSet, ByVal consultantUid As Guid)
        If outputGateways.Count = 0 Then Return

        Using qta As New GatewayTransactionQueueTableAdapter(), _
            dta As New GatewayTransactionDataTableAdapter

            qta.Connection = New SqlClient.SqlConnection(My.Settings.activiserConnectionString)
            dta.Connection = qta.Connection
            Dim txStartTime As Date = DateTime.UtcNow
            qta.Insert(transactionId, WebServiceGuid, MyBase.Context.Request.LogonUserIdentity.Name, -1, consultantUid, txStartTime, Nothing, 0, Nothing)

            Dim dx As New StringBuilder
            Dim sbw As New StringWriter(dx)
            ds.WriteXml(sbw, XmlWriteMode.IgnoreSchema)
            dta.Insert(Guid.NewGuid(), transactionId, 0, dx.ToString())

            Dim og As WebService.OutputGateway.IOutputGateway
            Dim ogSaveResult As Integer

            For Each ogi As OutputGatewayItem In outputGateways
                Dim saveEx As Exception = Nothing
                og = GetGatewayInstance(ogi)
                If og IsNot Nothing Then
                    ogSaveResult = og.Save(transactionId, ds, saveEx)
                    If saveEx IsNot Nothing OrElse ogSaveResult <> 0 Then
                        LogError(og.GatewayId, DateTime.UtcNow, transactionId.ToString(), og.ToString(), "Error saving data to gateway", String.Empty, Guid.Empty, ogSaveResult.ToString(), saveEx)
                    End If
                End If
            Next

            qta.Update(transactionId, WebServiceGuid, MyBase.Context.Request.LogonUserIdentity.Name, -1, consultantUid, txStartTime, DateTime.UtcNow, -1, Nothing)
        End Using

    End Sub

    <WebMethod()> _
    Public Function TestGateway() As XmlNode
        Dim result As New XmlDocument()
        Dim rootNode As XmlElement = result.CreateElement("GatewayTestResults")
        result.AppendChild(rootNode)

        If outputGateways.Count = 0 Then
            Dim messageNode As XmlElement = result.CreateElement("NoGatewayMessage")
            messageNode.InnerText = "No gateways defined"
            rootNode.AppendChild(messageNode)
            Return result
        End If

        ' Dim result As String = String.Empty

        Dim ogNode As XmlElement
        Dim ogNameNode As XmlAttribute
        Dim ogIdNode As XmlAttribute
        Dim ogErrorNode As XmlElement
        Dim ogResultNode As XmlNode
        For Each ogi As OutputGatewayItem In outputGateways
            ogNode = result.CreateElement("GatewayTestResult")

            ogNameNode = result.CreateAttribute("GatewayName")
            ogNameNode.Value = ogi.Name

            ogIdNode = result.CreateAttribute("GatewayId")
            ogIdNode.Value = ogi.Id.ToString()

            ogNode.Attributes.Append(ogNameNode)
            ogNode.Attributes.Append(ogIdNode)

            Try
                Dim og As WebService.OutputGateway.IOutputGateway = GetGatewayInstance(ogi)

                If og IsNot Nothing Then

                    Dim testResult As String = og.Test
                    Dim testXml As New Xml.XmlDocument()
                    Try
                        testXml.LoadXml(testResult)
                        ogResultNode = result.ImportNode(testXml.FirstChild, True)
                    Catch ex As XmlException
                        testXml = Nothing
                        ogResultNode = result.CreateElement("Result")
                        ogResultNode.InnerText = testResult
                    End Try

                    ogNode.AppendChild(ogResultNode)
                Else
                    ogErrorNode = result.CreateElement("Error")
                    ogErrorNode.InnerText = "Constructor not found or invalid"
                    ogNode.AppendChild(ogErrorNode)
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                ogErrorNode = result.CreateElement("Error")
                ogErrorNode.InnerText = ex.ToString()
                ogNode.AppendChild(ogErrorNode)
                'result &= "Failure when testing gateway: " & ex.ToString() & vbNewLine
            End Try

            rootNode.AppendChild(ogNode)
        Next
        Return result
    End Function
End Class