Imports System.Runtime.InteropServices

Module InteropSupport
    Friend Sub ReleaseComObject(Of T)(ByRef victim As T)
        If victim Is Nothing Then Return

        Try
            Marshal.FinalReleaseComObject(victim)
            victim = Nothing
        Catch ex As Exception
            ' don't really care
            TraceError(ex)
        End Try
    End Sub
End Module
