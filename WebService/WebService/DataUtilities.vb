Option Explicit Off
Option Strict Off

Imports System.Xml

Public Module DataUtilities
    Private Const STR_ModifiedDateTime As String = "ModifiedDateTime"
    Private Const STR_CreatedDateTime As String = "CreatedDateTime"

    Public Function DataRowHasChanges(ByVal dr As DataRow) As Boolean
        For Each dc As DataColumn In dr.Table.Columns
            If dc.ColumnName = STR_ModifiedDateTime Then Continue For
            If dc.ColumnName = STR_CreatedDateTime Then Continue For
            If (dr.HasVersion(DataRowVersion.Proposed) AndAlso dr.HasVersion(DataRowVersion.Current)) Then
                If dc.DataType.IsValueType Then
                    If Not dr.Item(dc, DataRowVersion.Current).Equals(dr.Item(dc, DataRowVersion.Proposed)) Then
                        Return True
                    End If
                Else
                    If dr.Item(dc, DataRowVersion.Current) IsNot (dr.Item(dc, DataRowVersion.Proposed)) Then
                        Return True
                    End If
                End If
            ElseIf (dr.HasVersion(DataRowVersion.Current) AndAlso dr.HasVersion(DataRowVersion.Original)) Then
                If dc.DataType.IsValueType Then
                    If Not dr.Item(dc, DataRowVersion.Current).Equals(dr.Item(dc, DataRowVersion.Original)) Then
                        Return True
                    End If
                Else
                    If dr.Item(dc, DataRowVersion.Current) IsNot (dr.Item(dc, DataRowVersion.Original)) Then
                        Return True
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Public Function DataRowsAreDifferent(Of T As DataRow)(ByVal dr1 As T, ByVal dr2 As T) As Boolean
        For Each dc As DataColumn In dr1.Table.Columns
            If dc.ColumnName = STR_ModifiedDateTime Then Continue For
            If dc.ColumnName = STR_CreatedDateTime Then Continue For
            Try
                If Not dr1.Item(dc).Equals(dr2.Item(dc)) Then
                    Return True
                End If
            Catch ' ex As Exception ' assume failure to compare = difference.
                Return True
            End Try
        Next
        Return False
    End Function

    Public Function SerialiseDataRow(ByVal row As DataRow) As XmlDocument
        If row Is Nothing Then
            Throw (New ArgumentNullException("row"))
        End If

        Dim xmlResult As New XmlDocument()
        Dim rootNode As XmlNode = xmlResult.CreateNode(XmlNodeType.Element, row.Table.TableName, Nothing)
        xmlResult.AppendChild(rootNode)
        For Each dc As DataColumn In row.Table.Columns
            Dim columnNode As XmlNode = xmlResult.CreateNode(XmlNodeType.Element, dc.ColumnName, Nothing)
            columnNode.InnerText = row(dc).ToString()
            rootNode.AppendChild(columnNode)
        Next
        Return xmlResult
    End Function

    Public Function DeserialiseDataRow(Of R As DataRow, T As DataTable)(ByVal xmlRow As String, ByVal table As T) As R
        If table Is Nothing Then Throw (New ArgumentNullException("table"))
        If String.IsNullOrEmpty(xmlRow) Then Throw New ArgumentNullException("xmlRow")

        Dim xmlResult As New XmlDocument()
        xmlResult.LoadXml(xmlRow)
        Dim rootNode As XmlNode = xmlResult.FirstChild
        If rootNode Is Nothing Then Throw New ArgumentException("XML Invalid (no root node found)", "xmlRow")
        Dim row As R = table.NewRow()
        If rootNode.Name <> table.TableName Then
            Throw New ArgumentOutOfRangeException("table in xmlRow does not match supplied datarow table")
        End If

        For Each columnNode As XmlNode In rootNode.ChildNodes
            If table.Columns.Contains(columnNode.Name) Then
                Dim column As DataColumn = table.Columns(columnNode.Name)
                row.Item(columnNode.Name) = Convert.ChangeType(columnNode.InnerText, column.DataType)
            Else
                Throw New ArgumentOutOfRangeException(String.Format("column '{0}' does not exist in supplied datarow table", columnNode.Name))
            End If
        Next
        Return row
    End Function

    Public Function IIf(Of T)(ByVal condition As Boolean, ByVal trueValue As T, ByVal falseValue As T) As T
        If condition Then
            Return trueValue
        Else
            Return falseValue
        End If
    End Function
End Module
