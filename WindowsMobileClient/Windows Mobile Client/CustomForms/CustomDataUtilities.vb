Imports activiser.Library.WebService.FormDefinition

Module CustomDataUtilities
    Friend Sub RemoveCustomData(ByVal entityName As String, ByVal parentId As Guid)
        Dim cq = From ce As FormRow In gFormDefinitions.Form.Cast(Of FormRow)() Where ce.EntityName = entityName

        For Each ce As FormRow In cq
            Dim parentFk As String = ce.EntityParentFK
            If gClientDataSet.Tables(ce.EntityName).Columns(parentFk).DataType IsNot GetType(Guid) Then Continue For
            Dim pfkIndex As Integer = gClientDataSet.Tables(ce.EntityName).Columns.IndexOf(parentFk)
            Dim dq = From dr As DataRow In gClientDataSet.Tables(ce.EntityName).Rows.Cast(Of DataRow)() _
                     Where CType(dr(pfkIndex), Guid) = parentId
            For Each victim As DataRow In dq
                gClientDataSet.Tables(ce.EntityName).Rows.Remove(victim)
            Next
        Next
    End Sub

    Friend Sub DeleteCustomData(ByVal entityName As String, ByVal parentId As Guid)
        Dim cq = From ce As FormRow In gFormDefinitions.Form.Cast(Of FormRow)() Where ce.EntityName = entityName

        For Each ce As FormRow In cq
            Dim parentFk As String = ce.EntityParentFK
            If gClientDataSet.Tables(ce.EntityName).Columns(parentFk).DataType IsNot GetType(Guid) Then Continue For
            Dim pfkIndex As Integer = gClientDataSet.Tables(ce.EntityName).Columns.IndexOf(parentFk)
            Dim dq = From dr As DataRow In gClientDataSet.Tables(ce.EntityName).Rows.Cast(Of DataRow)() _
                     Where CType(dr(pfkIndex), Guid) = parentId
            For Each victim As DataRow In dq
                victim.Delete()
            Next
        Next
    End Sub

    'Friend Function GetCustomTablesForEntity(ByVal entityName As String) As Generic.List(Of DataTable)
    '    Dim result As New Generic.List(Of DataTable)
    '    For Each fr As FormRow In gFormDefinitions.Form
    '        If fr.ParentEntityName = entityName Then
    '            result.Add(gClientDataSet.Tables(fr.EntityName))
    '        End If
    '    Next
    '    Return result
    'End Function

    'Friend Function GetModifiedRows(ByVal entityName As String, ByVal since As DateTime) As Generic.List(Of DataRow)
    '    Dim result As New Generic.List(Of DataRow)

    'End Function
End Module
