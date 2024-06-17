Imports activiser.Library.WebService.FormDefinition

Module CustomDataUtilities
    Friend Sub RemoveCustomData(ByVal entityName As String, ByVal parentId As Guid)
        Dim cq As New Generic.List(Of FormRow)
        For Each ce As FormRow In gFormDefinitions.Form
            If ce.EntityName = entityName Then cq.Add(ce)
        Next

        For Each ce As FormRow In cq
            Dim parentFk As String = ce.EntityParentFK
            If gClientDataSet.Tables(ce.EntityName).Columns(parentFk).DataType IsNot GetType(Guid) Then Continue For
            Dim pfkIndex As Integer = gClientDataSet.Tables(ce.EntityName).Columns.IndexOf(parentFk)
            Dim dq As New Generic.List(Of DataRow)

            For Each dr As DataRow In gClientDataSet.Tables(ce.EntityName).Rows
                If CType(dr(pfkIndex), Guid) = parentId Then dq.Add(dr)
            Next

            For Each victim As DataRow In dq
                gClientDataSet.Tables(ce.EntityName).Rows.Remove(victim)
            Next
        Next
    End Sub

    Friend Sub DeleteCustomData(ByVal entityName As String, ByVal parentId As Guid)
        Dim cq As New Generic.List(Of FormRow)
        For Each ce As FormRow In gFormDefinitions.Form
            If ce.EntityName = entityName Then cq.Add(ce)
        Next

        For Each ce As FormRow In cq
            Dim parentFk As String = ce.EntityParentFK
            If gClientDataSet.Tables(ce.EntityName).Columns(parentFk).DataType IsNot GetType(Guid) Then Continue For
            Dim pfkIndex As Integer = gClientDataSet.Tables(ce.EntityName).Columns.IndexOf(parentFk)
            Dim dq As New Generic.List(Of DataRow)

            For Each dr As DataRow In gClientDataSet.Tables(ce.EntityName).Rows
                If CType(dr(pfkIndex), Guid) = parentId Then dq.Add(dr)
            Next

            For Each victim As DataRow In dq
                victim.Delete()
            Next
        Next
    End Sub
End Module
