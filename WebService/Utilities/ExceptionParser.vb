Imports System.Reflection
Imports System.Xml.Linq

Public Class ExceptionParser
    Private m_TargetEx As Exception
    '    Private m_UserFriendlyMessage As String
    Private m_TimeStamp As String = Now.ToString("s")
    Private m_TotalPhysicalMemory As ULong = My.Computer.Info.TotalPhysicalMemory
    Private m_AvailablePhysicalMemory As ULong = My.Computer.Info.AvailablePhysicalMemory
    Private m_WindowsUserName As String = Environment.UserName
    Private m_OperatingSystemVersion As String = Environment.OSVersion.ToString
    Private m_MachineName As String = Environment.MachineName()
    Private m_DomainName As String = Environment.UserDomainName
    Private m_ProcessorType As String

    Private _ToString As String

    Public Sub New(ByVal targetException As Exception)
        m_TargetEx = targetException
        _ToString = Me.GetExceptionString
    End Sub

    Private Shared Function GetCustomExceptionInfo(ByVal Ex As Exception) As Generic.SortedList(Of String, Object)


        Dim customInfo As New Generic.SortedList(Of String, Object)

        Dim pi As PropertyInfo
        For Each pi In Ex.GetType().GetProperties()
            Dim baseEx As Type = GetType(System.Exception)
            If baseEx.GetProperty(pi.Name) Is Nothing Then
                customInfo.Add(pi.Name, pi.GetValue(Ex, Nothing))
            End If
        Next

        Return customInfo

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Function OtherInfomationToString() As String
        Try
            Dim ht As Generic.SortedList(Of String, Object) = GetCustomExceptionInfo(m_TargetEx)
            'Dim ide As IDictionaryEnumerator = ht.GetEnumerator()
            Dim sb As New System.Text.StringBuilder

            For Each kvp As KeyValuePair(Of String, Object) In ht
                Dim key As String = kvp.Key
                Dim value As String = String.Empty
                If kvp.Value IsNot Nothing Then
                    If TypeOf kvp.Value Is System.Xml.XmlNode Then
                        value = CType(kvp.Value, System.Xml.XmlNode).InnerXml
                    Else
                        value = kvp.Value.ToString()
                    End If
                End If
                sb.AppendFormat("{0}: {1}{2}", key, value, vbNewLine)
            Next

            Return sb.ToString
        Catch ex As Exception
            'Catch all exceptions
            Return ex.Message & vbCrLf & vbCrLf & _
            ex.GetType.FullName & vbCrLf & vbCrLf & _
            "Location:" & ex.StackTrace & vbCrLf & _
            "Source:" & ex.Source
        Finally
            'Clean up Code

        End Try
    End Function

    Public Shadows ReadOnly Property ToString() As String
        Get
            Return Me._ToString
        End Get
    End Property

    Public Shadows ReadOnly Property ToString(ByVal includeSystemInfo As Boolean) As String
        Get
            Dim result As New System.Text.StringBuilder(Me._ToString)
            If IncludeSystemInfo Then
                'System Info
                result.Append(vbNewLine)
                result.AppendFormat("{0}:{1}", "System Information", vbNewLine)
                result.AppendFormat("{0}: {1}{2}", "Windows User Name", m_WindowsUserName, vbNewLine)
                result.AppendFormat("{0}: {1}{2}", "Machine Name", m_MachineName, vbNewLine)
                result.AppendFormat("{0}: {1}{2}", "Domain Name", m_DomainName, vbNewLine)
                result.AppendFormat("{0}: {1}{2}", "Total Physical Memory", m_TotalPhysicalMemory.ToString & " bytes (" & CStr(m_TotalPhysicalMemory / 1000000000) & " Gb)", vbNewLine)
                result.AppendFormat("{0}: {1}{2}", "Available Physical Memory", m_AvailablePhysicalMemory.ToString & " bytes (" & CStr(m_AvailablePhysicalMemory / 1000000000) & " Gb)", vbNewLine)
                result.AppendFormat("{0}: {1}{2}", "Operating System Version", m_OperatingSystemVersion, vbNewLine)
                result.AppendFormat("{0}: {1}", "Processor Type", m_ProcessorType)
            End If
            Return result.ToString
        End Get
    End Property

    Private Shared Function GetTargetMethodFormat(ByVal Ex As Exception) As String
        Return "[" & Ex.TargetSite.DeclaringType.Assembly.GetName().Name & "]" & Ex.TargetSite.DeclaringType.ToString & "::" & Ex.TargetSite.Name & "()"
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Function GetExceptionString() As String
        Dim sbCopyInformation As New System.Text.StringBuilder
        Try
            Dim stackTrace As String() = m_TargetEx.StackTrace.Split(New Char() {Chr(10), Chr(13)})
            Dim innerEx As Exception = m_TargetEx.InnerException
            Dim OtherInformationString As String = OtherInfomationToString()

            sbCopyInformation.AppendFormat("{0}{1}", "Exception Information:", vbNewLine)
            sbCopyInformation.AppendFormat("{0}{1}", m_TimeStamp, vbNewLine)
            sbCopyInformation.AppendFormat("{0}{1}{2}", m_TargetEx.GetType().FullName, vbNewLine, vbNewLine)

            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Message", m_TargetEx.Message, vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Source", m_TargetEx.Source, vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Target Method", GetTargetMethodFormat(m_TargetEx), vbNewLine)
            sbCopyInformation.AppendFormat("{0}: {1}{2}{3}", "Help Link", m_TargetEx.HelpLink, vbNewLine, vbNewLine)

            'Dim we As System.Net.WebException

            For Each st As String In stackTrace
                If st.Trim <> String.Empty Then
                    sbCopyInformation.AppendFormat("{0}: {1}{2}", "Stack Trace", st, vbNewLine)
                End If
            Next st

            If Not innerEx Is Nothing Then
                sbCopyInformation.Append(vbNewLine)
                sbCopyInformation.Append("="c, 40)
                sbCopyInformation.AppendFormat("{0}:{1}", "Inner Exception Trace", vbNewLine)
                Dim level As Integer = 0
                While Not innerEx Is Nothing
                    Dim indent As String = New String(" "c, level * 4)
                    sbCopyInformation.Append(indent)
                    sbCopyInformation.Append("-"c, 40)
                    sbCopyInformation.AppendFormat("{0}{1}{2}", indent, innerEx.GetType().FullName, vbNewLine)
                    sbCopyInformation.AppendFormat("{0}{1}{2}", indent, GetTargetMethodFormat(innerEx), vbNewLine)
                    sbCopyInformation.AppendFormat("{0}{1}{2}", indent, New ExceptionParser(innerEx).ToString(False), vbNewLine)

                    innerEx = innerEx.InnerException
                    level += 1
                End While
                sbCopyInformation.Append("="c, 40)
            End If

            If OtherInformationString <> String.Empty Then
                sbCopyInformation.Append(vbNewLine)
                sbCopyInformation.AppendFormat("{0}:{1}", "Other Information", vbNewLine)
                sbCopyInformation.Append(OtherInformationString)
                sbCopyInformation.Append(vbNewLine)
            End If

            Return sbCopyInformation.ToString

        Catch ex As Exception
            'Catch all exceptions

            Return sbCopyInformation.ToString & vbNewLine & _
                "And got this exception whilst parsing the exception:" & vbNewLine & _
            ex.Message & vbCrLf & vbCrLf & _
            ex.GetType.FullName & vbCrLf & vbCrLf & _
            "Location:" & ex.StackTrace & vbCrLf & _
            "Source:" & ex.Source
        Finally
            'Clean up Code

        End Try
    End Function

    Public Function GetXml() As Xml.XmlDocument
        Dim result As New Xml.XmlDocument

        Try
            Dim xmlDoc As Xml.XmlNode = result.AppendChild(result.CreateElement("Exception", "http://www.activiser.com/Schemas/ExceptionParser.XSD"))
            Dim data As Xml.XmlNode = result.CreateElement("ExceptionData")
            ' TODO: make this pretty !
            data.InnerText = Me._ToString

            xmlDoc.AppendChild(data)

            '    Dim stackTrace As String() = m_TargetEx.StackTrace.Split(New Char() {Chr(10), Chr(13)})
            '    Dim innerEx As Exception = m_TargetEx.InnerException
            '    Dim OtherInformationString As String = OtherInfomationToString()

            '    sbCopyInformation.AppendFormat("{0}{1}", "Exception Information:", vbNewLine)
            '    sbCopyInformation.AppendFormat("{0}{1}", m_TimeStamp, vbNewLine)
            '    sbCopyInformation.AppendFormat("{0}{1}{2}", m_TargetEx.GetType().FullName, vbNewLine, vbNewLine)

            '    sbCopyInformation.AppendFormat("{0}: {1}{2}", "Message", m_TargetEx.Message, vbNewLine)
            '    sbCopyInformation.AppendFormat("{0}: {1}{2}", "Source", m_TargetEx.Source, vbNewLine)
            '    sbCopyInformation.AppendFormat("{0}: {1}{2}", "Target Method", GetTargetMethodFormat(m_TargetEx), vbNewLine)
            '    sbCopyInformation.AppendFormat("{0}: {1}{2}{3}", "Help Link", m_TargetEx.HelpLink, vbNewLine, vbNewLine)

            '    'Dim we As System.Net.WebException

            '    For Each st As String In stackTrace
            '        If st.Trim <> String.Empty Then
            '            sbCopyInformation.AppendFormat("{0}: {1}{2}", "Stack Trace", st, vbNewLine)
            '        End If
            '    Next st

            '    If Not innerEx Is Nothing Then
            '        sbCopyInformation.Append(vbNewLine)
            '        sbCopyInformation.Append("="c, 40)
            '        sbCopyInformation.AppendFormat("{0}:{1}", "Inner Exception Trace", vbNewLine)
            '        Dim level As Integer = 0
            '        While Not innerEx Is Nothing
            '            Dim indent As String = New String(" "c, level * 4)
            '            sbCopyInformation.Append(indent)
            '            sbCopyInformation.Append("-"c, 40)
            '            sbCopyInformation.AppendFormat("{0}{1}{2}", indent, innerEx.GetType().FullName, vbNewLine)
            '            sbCopyInformation.AppendFormat("{0}{1}{2}", indent, GetTargetMethodFormat(innerEx), vbNewLine)
            '            sbCopyInformation.AppendFormat("{0}{1}{2}", indent, New ExceptionParser(innerEx).ToString(False), vbNewLine)

            '            innerEx = innerEx.InnerException
            '            level += 1
            '        End While
            '        sbCopyInformation.Append("="c, 40)
            '    End If

            '    If OtherInformationString <> String.Empty Then
            '        sbCopyInformation.Append(vbNewLine)
            '        sbCopyInformation.AppendFormat("{0}:{1}", "Other Information", vbNewLine)
            '        sbCopyInformation.Append(OtherInformationString)
            '        sbCopyInformation.Append(vbNewLine)
            '    End If

            '    Return sbCopyInformation.ToString

        Catch ex As Exception
            'Catch all exceptions

            'Return sbCopyInformation.ToString & vbNewLine & _
            '    "And got this exception whilst parsing the exception:" & vbNewLine & _
            'ex.Message & vbCrLf & vbCrLf & _
            'ex.GetType.FullName & vbCrLf & vbCrLf & _
            '"Location:" & ex.StackTrace & vbCrLf & _
            '"Source:" & ex.Source
        Finally
            'Clean up Code

        End Try

        Return result
    End Function
End Class


'Public Class MemoryStatus
'    Public Structure _MEMORYSTATUSEX
'        Friend Length As Integer
'        Friend Load As Integer
'        Friend _TotalPhysicalMemory As Long
'        Friend _AvailablePhysicalMemory As Long
'        Friend _TotalPageFileMemory As Long
'        Friend _AvailablePageFileMemory As Long
'        Friend _TotalVirtualMemory As Long
'        Friend _AvailableVirtualMemory As Long
'        Friend _AvailableExtendedMemory As Long
'    End Structure

'    Private Shared _MemoryStatusExtended As _MEMORYSTATUSEX
'    Private Declare Unicode Sub GlobalMemoryStatusEx Lib "Kernel32" (ByRef _MemoryStatus As _MEMORYSTATUSEX)

'    Sub New()
'        _MemoryStatusExtended.Length = System.Runtime.InteropServices.Marshal.SizeOf(_MemoryStatusExtended)
'        GlobalMemoryStatusEx(_MemoryStatusExtended)
'    End Sub
'    ' 
'    Public Shared ReadOnly Property MemoryLoad() As Long
'        Get
'            Return _MemoryStatusExtended.Load
'        End Get
'    End Property


'    Public Shared ReadOnly Property TotalPhysicalMemory() As ULong
'        Get
'            Return My.Computer.Info.TotalPhysicalMemory
'        End Get
'    End Property

'    Public Shared ReadOnly Property AvailablePhysicalMemory() As ULong
'        Get
'            Return My.Computer.Info.AvailablePhysicalMemory
'        End Get
'    End Property

'    Public Shared ReadOnly Property TotalVirtualMemory() As ULong
'        Get
'            Return My.Computer.Info.TotalVirtualMemory
'        End Get
'    End Property

'    Public Shared ReadOnly Property AvailableVirtualMemory() As ULong
'        Get
'            Return My.Computer.Info.AvailableVirtualMemory
'        End Get
'    End Property
'End Class
