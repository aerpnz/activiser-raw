Partial Class FormDefinition

    Public Shared Function LastFormChange() As DateTime
        Return CDate((New FormDefinitionTableAdapters.QueriesTableAdapter)._lastFormChange().Value)
    End Function

    Public Shared Function GetFormDefinition() As FormDefinition
        Dim result As New FormDefinition()

        Using _
            fta As New FormDefinitionTableAdapters.FormTableAdapter, _
            ffta As New FormDefinitionTableAdapters.FormFieldTableAdapter

            fta.Fill(result.Form)
            ffta.Fill(result.FormField)
        End Using

        Return result
    End Function

    Public Shared Function GetFormDefinition(ByVal clientMask As Int32, ByVal since As DateTime?) As FormDefinition
        Dim result As New FormDefinition()

        Using _
            fta As New FormDefinitionTableAdapters.FormTableAdapter, _
            ffta As New FormDefinitionTableAdapters.FormFieldTableAdapter

            fta.FillByClientMask(result.Form, clientMask, since)
            ffta.FillByClientMask(result.FormField, clientMask, since)
        End Using

        Return result
    End Function
End Class
