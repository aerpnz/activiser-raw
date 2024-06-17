Module ExceptionManagement

    Public Class ExceptionEventArgs
        Inherits EventArgs

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal ex As Exception)
            Me.Ex = ex
        End Sub

        Private _ex As Exception
        Public Property Ex() As Exception
            Get
                Return _ex
            End Get
            Set(ByVal value As Exception)
                _ex = value
            End Set
        End Property

    End Class

    Private Const RES_None As String = "<None>"
    'Error is written to XML without prompting the user
    'Overloaded method which prompts the user if an error occurs

    Public Sub LogErrorToFile(ByVal ex As Exception, Optional ByVal ExtraMessage As String = RES_None)
        Try
            Dim goryDetails As New ExceptionParser(ex)

            Const STR_ErrorFileNameFormat As String = "error-{0:yyyyMMdd-HHmmss}.txt"
            Dim f As New IO.StreamWriter(IO.Path.Combine(gErrorFolder, String.Format(WithoutCulture, STR_ErrorFileNameFormat, DateTime.Now)))
            f.AutoFlush = False
            f.WriteLine(String.Format(WithoutCulture, "Unhandled error in activiser @ {0}:", DateTime.Now))
            f.WriteLine()
            f.WriteLine(String.Format(WithoutCulture, "Additional information: {0}", ExtraMessage))
            f.Write("ToString():")
            f.WriteLine(ex.ToString)
            f.Write("Details:")
            f.WriteLine(goryDetails.ToString)
            f.Close()
        Catch cex As Exception
            Debug.WriteLine(cex.ToString)
        End Try

    End Sub

    Private _inLogError As Boolean
    Public Sub LogError(ByVal moduleName As String, ByVal functionName As String, ByVal ex As Exception, ByVal displayMessage As Boolean, ByVal messageId As String, ByVal ParamArray messageArgs() As Object)

        'If there is an error in the errorlogger, we don't want to trigger a loop!
        If _inLogError Then Return

        _inLogError = True

        Try
            Dim message As String
            Dim myThread As System.Threading.Thread = System.Threading.Thread.CurrentThread
            Dim threadMessage As String = If(Not String.IsNullOrEmpty(myThread.Name), myThread.Name, "Unnamed thread, Id=" & myThread.ManagedThreadId)

            If String.IsNullOrEmpty(messageId) Then
                message = Terminology.GetFormattedString(My.Resources.SharedMessagesKey, "ExceptionMessageFormat", threadMessage, moduleName, functionName, ex.Message)
            Else
                If messageArgs Is Nothing OrElse messageArgs.Length = 0 Then
                    message = Terminology.GetFormattedString(moduleName, messageId)
                Else
                    message = Terminology.GetFormattedString(moduleName, messageId, messageArgs)
                End If
            End If

            If displayMessage Then
                Try
                    ErrorDialog.DisplayError(Nothing, ex, message)
                Catch EDex As Exception
                    Debug.WriteLine("Error saving pending errors: " & EDex.ToString)
                End Try
            Else
                message = threadMessage & vbCrLf & message
            End If

            If Not gEventLog Is Nothing Then
                Dim goryDetails As New ExceptionParser(ex)
                If message.Length > gEventLog.EventLog.MessageColumn.MaxLength Then
                    message = message.Substring(0, gEventLog.EventLog.MessageColumn.MaxLength)
                End If
                gEventLog.EventLog.AddEventLogRow(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, 0, gDeviceIDString, moduleName & ":" & functionName, gDeviceID, ConsultantConfig.Name, message, String.Empty, goryDetails.ToString, Nothing, DateTime.UtcNow, ConsultantConfig.Name, DateTime.UtcNow, ConsultantConfig.Name, Nothing)
                Try
                    SavePending(gEventLog, gErrorLogDbFileName)
                Catch saveEx As Exception
                    Debug.WriteLine("Error saving pending errors: " & saveEx.ToString)
                    LogErrorToFile(ex)
                End Try
            Else
                LogErrorToFile(ex)
            End If

        Finally
            _inLogError = False
        End Try
    End Sub
End Module
