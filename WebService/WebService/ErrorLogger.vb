Imports activiser.WebService.Utilities

Public Module ErrorLogger
    Private Const _divider As String = "-----------------------------------------------" & vbNewLine

    Public Function LogError(ByVal ConsultantUID As Guid, ByVal ErrorDateTime As Date, ByVal DeviceID As String, ByVal Source As String, ByVal Message As String, ByVal TableName As String, ByVal TableUID As Guid, ByVal TableData As String, ByVal exception As Exception) As Boolean
        Dim exceptionMessage As String = String.Empty
        Dim exceptionData As Xml.XmlDocument = Nothing ' String = String.Empty
        If exception IsNot Nothing Then
            Dim ep As New ExceptionParser(exception)
            exceptionData = ep.GetXml() '.ToString()
        End If
        Try
            Try
                Dim ApplicationEventLog As New System.Diagnostics.EventLog(My.Resources.EventLogName, My.Computer.Name, My.Resources.EventLogSource)
                Dim emBuilder As New System.Text.StringBuilder("Activiser web service V", 1000)
                emBuilder.AppendFormat("{0}.", My.Application.Info.Version.ToString(4))
                emBuilder.AppendLine()
                emBuilder.Append(Message)
                emBuilder.AppendLine()
                emBuilder.Append(_divider)
                emBuilder.AppendLine()
                emBuilder.AppendFormat("DeviceID: {0}", DeviceID)
                If Not ConsultantUID.Equals(Guid.Empty) Then
                    emBuilder.AppendLine()
                    emBuilder.AppendFormat("ConsultantUID: {0}", ConsultantUID)
                End If
                emBuilder.AppendLine()
                emBuilder.AppendFormat("Source: {0}", Source)
                emBuilder.AppendLine()
                emBuilder.AppendFormat("ErrorDateTime: {0}", ErrorDateTime.ToString("G"))
                If Not String.IsNullOrEmpty(TableName) Then
                    emBuilder.AppendLine()
                    emBuilder.AppendFormat("TableName: {0}", TableName)
                End If
                If Not TableUID.Equals(Guid.Empty) Then
                    emBuilder.AppendLine()
                    emBuilder.AppendFormat("TableUID: {0}", TableUID)
                End If
                emBuilder.AppendLine()
                emBuilder.AppendFormat("Table Data: {0}", TableData)

                If exceptionData IsNot Nothing Then
                    emBuilder.AppendLine()
                    emBuilder.Append(_divider)
                    emBuilder.AppendFormat("Exception Data: {0}", exceptionData.OuterXml)
                End If

                emBuilder.AppendLine()
                exceptionMessage = emBuilder.ToString

                Try
                    My.Application.Log.WriteEntry(exceptionMessage)
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)
                End Try

                Try
                    If exception Is Nothing Then
                        ApplicationEventLog.WriteEntry(exceptionMessage, EventLogEntryType.Information)
                    Else
                        ApplicationEventLog.WriteEntry(exceptionMessage, EventLogEntryType.Error)
                    End If
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)
                End Try

            Catch ex As Exception
                Debug.WriteLine(ex.ToString)
            End Try

            Dim elta As New EventLogDataSetTableAdapters.EventLogTableAdapter
            elta.Insert(Guid.NewGuid(), DateTime.UtcNow, ErrorDateTime, 4, DeviceID, Source, My.Computer.Name, My.User.Name, Message, Nothing, Nothing, If(exceptionData Is Nothing, Nothing, exceptionData.OuterXml), DateTime.UtcNow, My.User.Name, DateTime.UtcNow, My.User.Name)

            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function
End Module
