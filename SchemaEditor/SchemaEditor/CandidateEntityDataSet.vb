Imports activiser.SchemaEditor.CandidateEntityDataSetTableAdapters

Partial Public Class CandidateEntityDataSet
    Public Shared Function GetCandateEntityDataSet() As CandidateEntityDataSet
        Dim result As New CandidateEntityDataSet
        Dim ceta As New CandidateEntityTableAdapter
        Dim ceata As New CandidateEntityAttributeTableAdapter

        ceta.Connection.ConnectionString = My.Settings.activiserConnectionString
        ceata.Connection.ConnectionString = My.Settings.activiserConnectionString

        ceta.Fill(result.CandidateEntity)
        result.EnforceConstraints = False
        ceata.Fill(result.CandidateEntityAttribute)

        For Each cear As CandidateEntityAttributeRow In result.CandidateEntityAttribute.GetErrors
            Debug.Print(cear.AttributeName)
            cear.Delete()
            cear.AcceptChanges()
        Next
        result.EnforceConstraints = True

        Dim badEggs As New List(Of CandidateEntityRow)

        For Each cer As CandidateEntityRow In result.CandidateEntity
            If (Aggregate qar In cer.GetCandidateEntityAttributeRows Where _
                    (qar.AttributeIsPK <> 0) OrElse _
                    (qar.AttributeName = "ModifiedDateTime") OrElse _
                    (qar.AttributeName = "CreatedDateTime") _
                Into Count()) <> 3 Then
                badEggs.Add(cer)
                Continue For
            End If
        Next

        For Each cer As CandidateEntityRow In badEggs
            cer.Delete()
            cer.AcceptChanges()
        Next


        Return result
    End Function
End Class
