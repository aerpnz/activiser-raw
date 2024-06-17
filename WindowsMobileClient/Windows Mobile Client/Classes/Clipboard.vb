
Public NotInheritable Class Clipboard
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")> _
    Public Shared Function GetText() As String
        Dim o As IDataObject = Windows.Forms.Clipboard.GetDataObject()
        If o IsNot Nothing AndAlso o.GetDataPresent(DataFormats.StringFormat) Then
            Return CStr(o.GetData(DataFormats.StringFormat, True))
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Sub SetText(ByVal value As String)
        Windows.Forms.Clipboard.SetDataObject(value)
    End Sub

    Private Sub New()

    End Sub
End Class
