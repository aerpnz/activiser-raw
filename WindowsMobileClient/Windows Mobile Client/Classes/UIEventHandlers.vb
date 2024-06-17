Imports Microsoft.WindowsCE.Forms

Module UIEventHandlers
#Region "Form navigation"
    Public Function GetActiveControl(ByVal parent As Control) As Control
        For Each c As Control In parent.Controls
            If c.Focused Then
                Dim cc As ContainerControl = TryCast(c, ContainerControl)
                If cc IsNot Nothing Then
                    Return GetActiveControl(cc)
                Else
                    Return c
                End If
            End If
        Next
        Return Nothing
    End Function
#End Region
End Module
