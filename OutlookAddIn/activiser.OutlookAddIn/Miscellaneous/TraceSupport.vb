Imports System.Reflection

Module TraceSupport
    Friend Const traceTimeStampFormat As String = "yyyy-MM-dd HH:mm:ss.fff"
    Friend TraceLevel As New TraceSwitch("TraceLevel", "Level of information to trace for debugging.", "Warning")

    Friend Const STR_Fired As String = "Fired"
    Friend Const STR_Entered As String = "Entered"
    Friend Const STR_Done As String = "Done"
    
    Private messageNumber As Integer

    Friend Sub TraceInit()
        Dim message As String
        message = String.Format("{0}|{7}|{9}|{1}|{2}|{3}|{4}|{6}|{5}|{8}", _
            "Time", "Event Class", "Event Method", _
            "Calling Class", "Calling Method", _
            "Thread Name", "Thread Id", "Message #", "Message", "Indent")
        Trace.WriteLine(Message)
    End Sub

    Friend Sub TraceEvent(ByVal format As String, ByVal ParamArray args() As Object)
        Dim caller, callersCaller As StackFrame
        Dim stack As System.Diagnostics.StackTrace = New StackTrace(2) ' skip me.
        messageNumber += 1

        caller = stack.GetFrame(0)
        callersCaller = stack.GetFrame(1)

        Dim message As String
        Dim timeStamp As String = DateTime.Now.ToString(traceTimeStampFormat)
        Dim indent As String = CStr(Trace.IndentLevel)
        Dim userMessage As String = String.Empty
        If args IsNot Nothing AndAlso args.Length > 0 Then
            userMessage = String.Format(format, args)
        ElseIf Not String.IsNullOrEmpty(format) Then
            userMessage = format
        End If
        message = String.Format("{0}|{7}|{9}|{1}|{2}|{3}|{4}|{6}|{5}|{8}", _
            timeStamp, caller.GetMethod.ReflectedType.Name, caller.GetMethod.Name, callersCaller.GetMethod.ReflectedType.Name, callersCaller.GetMethod.Name, Threading.Thread.CurrentThread.Name, Threading.Thread.CurrentThread.ManagedThreadId, messageNumber, userMessage, indent)
        Trace.WriteLine(message)
    End Sub

    Friend Sub TraceVerbose(ByVal format As String)
        If TraceLevel.TraceVerbose Then
            TraceEvent(format)
            'If ConsoleData.RefreshInProgress Then Threading.Thread.Sleep(250)
        End If
    End Sub

    Friend Sub TraceVerbose(ByVal format As String, ByVal ParamArray args() As Object)
        If TraceLevel.TraceVerbose Then
            TraceEvent(format, args)
            'If ConsoleData.RefreshInProgress Then Threading.Thread.Sleep(250)
        End If
    End Sub

    Friend Sub TraceInfo(ByVal format As String)
        If TraceLevel.TraceInfo Then
            TraceEvent(format)
            'If ConsoleData.RefreshInProgress Then Threading.Thread.Sleep(250)
        End If
    End Sub

    Friend Sub TraceInfo(ByVal format As String, ByVal ParamArray args() As Object)
        If TraceLevel.TraceInfo Then
            TraceEvent(format, args)
            'If ConsoleData.RefreshInProgress Then Threading.Thread.Sleep(250)
        End If
    End Sub

    Friend Sub TraceWarning(ByVal format As String)
        If TraceLevel.TraceWarning Then
            TraceEvent(format)
        End If
    End Sub

    Friend Sub TraceWarning(ByVal format As String, ByVal ParamArray args() As Object)
        If TraceLevel.TraceWarning Then
            TraceEvent(format, args)
        End If
    End Sub

    Friend Sub TraceError(ByVal format As String)
        If TraceLevel.TraceError Then
            TraceEvent(format)
        End If
    End Sub

    Friend Sub TraceError(ByVal format As String, ByVal ParamArray args() As Object)
        If TraceLevel.TraceError Then
            TraceEvent(format, args)
        End If
    End Sub

    Friend Sub TraceError(ByVal ex As Exception)
        If TraceLevel.TraceError Then
            TraceEvent(ex.ToString())
        End If
    End Sub

End Module
