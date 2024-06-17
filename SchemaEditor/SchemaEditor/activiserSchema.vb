Partial Class activiserSchema
    Partial Class EntityDataTable
        Public Function FindByEntityName(ByVal entityName As String) As EntityRow
            For Each er As EntityRow In Me.Rows
                If er.EntityName = entityName Then
                    Return er
                End If
            Next
            Return Nothing
        End Function
    End Class
End Class

Namespace activiserSchemaTableAdapters
    Partial Public Class TableAdapterManager
        Public Sub New()
            AttributeTableAdapter = New AttributeTableAdapter
            FormTableAdapter = New FormTableAdapter
            FormFieldTableAdapter = New FormFieldTableAdapter
            EntityTableAdapter = New EntityTableAdapter
            AttributeTypeTableAdapter = New AttributeTypeTableAdapter
            AttributeTableAdapter = New AttributeTableAdapter
            ClientTableAdapter = New ClientTableAdapter
        End Sub
    End Class
End Namespace
