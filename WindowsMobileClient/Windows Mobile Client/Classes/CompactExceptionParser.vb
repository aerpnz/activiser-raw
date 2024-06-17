Imports System.Reflection

Public Class ExceptionParser
    Private targetEx As Exception
    Private timeStamp As String = Now.ToString("s", WithCulture)
    Private memoryStatus As New MemoryStatus()
    Private machineName As String = gDeviceID
    Private processorType As String

    Private _ToString As String = String.Empty

    Public Sub New(ByVal targetException As Exception)
        targetEx = targetException

        If targetException Is Nothing Then Return
        _ToString = Me.GetExceptionString
    End Sub

    Private Shared Function GetCustomExceptionInfo(ByVal Ex As Exception) As Generic.Dictionary(Of String, Object)
        Dim customInfo As New Generic.Dictionary(Of String, Object)

        Dim pi As PropertyInfo
        For Each pi In Ex.GetType().GetProperties()
            Dim baseEx As Type = GetType(System.Exception)
            If baseEx.GetProperty(pi.Name) Is Nothing Then
                customInfo.Add(pi.Name, pi.GetValue(Ex, Nothing))
            End If
        Next

        Return customInfo
    End Function

    Private Function OtherInfomationToString() As String
        Try
            Dim ht As Generic.Dictionary(Of String, Object) = GetCustomExceptionInfo(targetEx)
            Dim sb As New System.Text.StringBuilder

            For Each kvp As Generic.KeyValuePair(Of String, Object) In ht
                sb.Append(String.Format(WithoutCulture, "{0}: ", kvp.Key))
                If kvp.Value IsNot Nothing Then
                    sb.Append(String.Format(WithoutCulture, "{0} ", kvp.Value.ToString()))
                Else
                    sb.Append("Null")
                End If
                sb.Append(vbNewLine)
            Next

            Return sb.ToString

        Catch ex As Exception
            'Catch all exceptions
            Return ex.Message & vbNewLine & vbNewLine & _
            ex.GetType.FullName & vbNewLine & vbNewLine & _
            "Location:" & ex.StackTrace & vbNewLine
        Finally
            'Clean up Code

        End Try
    End Function

    Private Shared Function formatBytes(ByVal value As Integer) As String
        Const KiB As Integer = 1024
        Const MiB As Integer = KiB * KiB
        Const GiB As Integer = MiB * KiB
        If value < KiB Then
            Return value.ToString("0 Bytes", WithoutCulture)
        ElseIf value < MiB Then
            Return (value / KiB).ToString("0.## KiB", WithoutCulture)
        ElseIf value < GiB Then
            Return (value / MiB).ToString("0.## MiB", WithoutCulture)
        Else
            Return (value / GiB).ToString("0.## GiB", WithoutCulture)
        End If
    End Function

    Public Shadows ReadOnly Property ToString() As String
        Get
            Return Me._ToString
        End Get
    End Property

    Private Function GetExceptionString() As String
        Dim sbCopyInformation As New System.Text.StringBuilder
        Try
            Dim stackTrace As String() = targetEx.StackTrace.Split(New Char() {Chr(10), Chr(13)})
            Dim innerEx As Exception = targetEx
            Dim OtherInformationString As String = OtherInfomationToString()

            sbCopyInformation.AppendFormat(WithoutCulture, "{0}{1}", "Exception Information:", vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}{1}", timeStamp, vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}{1}{2}", targetEx.GetType().FullName, vbNewLine, vbNewLine)
            Dim myThread As System.Threading.Thread = System.Threading.Thread.CurrentThread
            Dim threadName As String = If(Not String.IsNullOrEmpty(myThread.Name), myThread.Name, "Unnamed thread")
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}Thread: {1}", threadName, vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}ThreadId: {1}", myThread.ManagedThreadId, vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "Message", targetEx.Message, vbNewLine)
            'sbCopyInformation.Append(String.Format("{0}: {1}{2}", "Target Method", GetTargetMethodFormat(m_TargetEx), vbNewLine))

            'Dim we As System.Net.WebException

            For Each st As String In stackTrace
                If st.Trim <> String.Empty Then
                    sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "Stack Trace", st, vbNewLine)
                End If
            Next st

            If Not innerEx Is Nothing Then
                sbCopyInformation.Append(vbNewLine)
                sbCopyInformation.AppendFormat(WithoutCulture, "{0}:{1}", "Inner Exception Trace", vbNewLine)
                Dim level As Integer = 0
                While Not innerEx Is Nothing
                    Dim indent As String = New String(" "c, level * 4)

                    sbCopyInformation.AppendFormat(WithoutCulture, "{0}{1}{2}", indent, innerEx.GetType().FullName, vbNewLine)
                    sbCopyInformation.AppendFormat(WithoutCulture, "{0}{1}{2}", indent, innerEx.Message, vbNewLine)

                    innerEx = innerEx.InnerException
                    level += 1
                End While
            End If

            If OtherInformationString <> String.Empty Then
                sbCopyInformation.Append(vbNewLine)
                sbCopyInformation.AppendFormat(WithoutCulture, "{0}:{1}", "Other Information", vbNewLine)
                sbCopyInformation.Append(OtherInformationString)
                sbCopyInformation.Append(vbNewLine)
            End If

            'System Info
            sbCopyInformation.Append(vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}:{1}", "System Information", vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "Activiser version", GetVersion.ToString(4), vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "Activiser server URL", gServerUrl, vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "Machine Name", machineName, vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "Total Physical Memory", formatBytes(memoryStatus.TotalPhysicalMemory), vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "Available Physical Memory", formatBytes(memoryStatus.AvailablePhysicalMemory), vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}{2}", "OS Version", Environment.OSVersion.ToString, vbNewLine)
            sbCopyInformation.AppendFormat(WithoutCulture, "{0}: {1}", "Processor Type", processorType)

            Return sbCopyInformation.ToString

        Catch ex As Exception
            'Catch all exceptions

            Return sbCopyInformation.ToString & vbNewLine & _
                "And got this exception whilst parsing the exception:" & vbNewLine & _
            ex.Message & vbNewLine & vbNewLine & _
            ex.GetType.FullName & vbNewLine & vbNewLine & _
            "Location:" & ex.StackTrace & vbNewLine
        Finally
            'Clean up Code

        End Try
    End Function

End Class

