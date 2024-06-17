Imports System.Text
Imports activiser.DataSchemaTableAdapters

Partial Class DataSchema

    Partial Class AttributeDataTable
        Public Function FindByAttributeName(ByVal attributeName As String) As AttributeRow()
            Dim result As New List(Of AttributeRow)
            For Each ar As AttributeRow In Me.Rows
                If ar.AttributeName = attributeName Then
                    result.Add(ar)
                End If
            Next
            If result.Count = 0 Then
                result = Nothing
                Return Nothing
            Else
                Return result.ToArray()
            End If
        End Function
    End Class

    Public Enum AttributeType
        [Unknown] = -1
        [Guid] = 0
        [Integer] = 1
        [String] = 2
        [DateTime] = 3
        [Float] = 4
        [Boolean] = 5
        [Byte] = 6
        [Currency] = 7
        [BigInt] = 8
        [Decimal] = 9
        [Short] = 10
        [Timestamp] = 11
        [DateOnly] = 12
        [TimeOnly] = 13
        [GuidPK] = 100
        [IntegerPK] = 101
        [BigIntPK] = 102
        [GuidFK] = 200
        [IntegerFK] = 201
        [BigIntFK] = 202
        [EmailAddress] = 1010
        [WebAddress] = 1011
        [Address] = 1012
        [PhoneNumber] = 1013
        [Password] = 1014
    End Enum

    'TODO: Add Schema 'Cache'


    ''' <summary>
    ''' Returns an DataSchema filled with the current schema configuration
    ''' </summary>
    ''' <returns>DataSchema</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSchema() As DataSchema

        Dim result As New DataSchema()

        Using _
            eta As New EntityTableAdapter, _
            ata As New AttributeTableAdapter

            eta.Fill(result.Entity)
            ata.Fill(result.Attribute)
        End Using

        Return result
    End Function


    ''' <summary>
    ''' Returns an DataSchema filled with the current schema configuration
    ''' </summary>
    ''' <returns>DataSchema</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSchema(ByVal clientMask As Int32) As DataSchema

        Dim result As New DataSchema()

        Using _
            eta As New EntityTableAdapter, _
            ata As New AttributeTableAdapter

            eta.FillByClientMask(result.Entity, clientMask)
            If result.Entity.Rows.Count = 0 Then
                result.Dispose()
                Return Nothing
            End If

            ata.ClearBeforeFill = False
            For Each er As EntityRow In result.Entity
                ata.FillByClientMaskAndEntity(result.Attribute, clientMask, er.EntityId)
            Next
        End Using

        Return result
    End Function

    ''' <summary>
    ''' Returns an DataSchema filled with the current schema configuration
    ''' </summary>
    ''' <returns>DataSchema</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSchema(ByVal entityName As String) As DataSchema

        Dim result As New DataSchema()

        Using _
            eta As New EntityTableAdapter, _
            ata As New AttributeTableAdapter

            eta.FillByEntityName(result.Entity, entityName)
            If result.Entity.Rows.Count <> 1 Then  ' should only be one!
                result.Dispose()
                Return Nothing
            End If

            ata.FillByEntityId(result.Attribute, result.Entity(0).EntityId)

        End Using

        Return result
    End Function

    Private Shared Function GetAttributeDataType(ByVal attribute As AttributeRow) As Type
        Dim dataType As Type
        Select Case attribute.AttributeTypeCode
            Case AttributeType.Boolean
                dataType = GetType(Boolean)
            Case AttributeType.Byte
                dataType = GetType(Byte)
            Case AttributeType.Currency
                dataType = GetType(Decimal)
            Case AttributeType.DateTime, AttributeType.DateOnly
                dataType = GetType(DateTime)
            Case _
                AttributeType.Address, _
                AttributeType.EmailAddress, _
                AttributeType.String, _
                AttributeType.PhoneNumber, _
                AttributeType.Password, _
                AttributeType.WebAddress
                dataType = GetType(String)
            Case AttributeType.Float
                dataType = GetType(Double)
            Case AttributeType.Guid, AttributeType.GuidPK, AttributeType.GuidFK
                dataType = GetType(Guid)
            Case AttributeType.Integer, AttributeType.IntegerPK, AttributeType.IntegerFK
                dataType = GetType(Int32)
            Case AttributeType.BigInt, AttributeType.BigIntPK, AttributeType.BigIntFK
                dataType = GetType(Int64)
            Case Else
                Throw New AttributeTypeInvalidException(attribute.AttributeName, attribute.AttributeTypeCode)
        End Select
        Return dataType
    End Function

    ''' <summary>
    ''' Returns a ClientDataSet object that is empty, but with the schema expanded to
    ''' include all the custom entities and attributes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetExpandedClientSchema() As ClientDataSet
        Dim result As New ClientDataSet()

        Using _
            ds As DataSchema = GetDataSchema()

            For Each entity As EntityRow In ds.Entity
                If Not result.Tables.Contains(entity.EntityName) Then
                    result.Tables.Add(entity.EntityName)
                End If

                Using dt As DataTable = result.Tables(entity.EntityName)
                    For Each attribute As AttributeRow In entity.GetAttributeRows
                        If Not dt.Columns.Contains(attribute.AttributeName) Then
                            dt.Columns.Add(attribute.AttributeName, GetAttributeDataType(attribute))
                        End If
                    Next
                End Using
            Next
        End Using

        Return result
    End Function

    Public Shared Function GetExpandedClientSchema(ByVal clientMask As Integer) As ClientDataSet
        Dim result As New ClientDataSet()

        Using _
            ds As DataSchema = GetDataSchema(clientMask)

            For Each entity As EntityRow In ds.Entity

                If Not result.Tables.Contains(entity.EntityName) Then
                    result.Tables.Add(entity.EntityName)
                End If

                Using dt As DataTable = result.Tables(entity.EntityName)
                    For Each attribute As AttributeRow In entity.GetAttributeRows
                        If Not dt.Columns.Contains(attribute.AttributeName) Then
                            dt.Columns.Add(attribute.AttributeName, GetAttributeDataType(attribute))
                        End If
                    Next
                End Using
            Next
        End Using

        Return result
    End Function

    Public Shared Function GetExpandedClientDataSet() As ClientDataSet
        Dim result As ClientDataSet = GetExpandedClientSchema()

        FillExpandedClientDataSet(result, -1)

        Return result
    End Function

    Public Shared Function GetExpandedClientDataSet(ByVal since? As Date, ByVal clientMask As Int32) As ClientDataSet
        Dim result As ClientDataSet = GetExpandedClientSchema()

        FillExpandedClientDataSet(result, since, clientMask)

        Return result
    End Function

    'TODO:
    Public Shared Function FillExpandedClientDataSetForConsultant(ByVal target As ClientDataSet, ByVal clientMask As Int32, ByVal consultantUid As Guid, ByVal since As DateTime?) As Integer
        Dim result As Integer = 0
        Using _
            ds As DataSchema = GetDataSchema(clientMask)

            ' note: the fillsequence is important, get it wrong and there will be constraint violation issues.
            For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
                If Not target.Tables.Contains(entity.EntityName) Then Continue For ' skip tables where clientmask doesn't fit.
                Using dt As DataTable = target.Tables(entity.EntityName)
                    Dim columnList As New List(Of String)
                    For Each attribute As AttributeRow In entity.GetAttributeRows
                        If dt.Columns.Contains(attribute.AttributeName) Then
                            columnList.Add(attribute.AttributeName)
                        End If
                    Next

                    Dim sql As New StringBuilder(1000)
                    Dim filters As New Dictionary(Of String, String)
                    Dim filterValues As New Dictionary(Of String, Object)
                    sql.AppendFormat("SELECT {1} FROM {0}", entity.EntityName, String.Join(",", columnList.ToArray()))
                    If since.HasValue Then
                        filters.Add("@Since", "ModifiedDateTime >= @Since")
                        filterValues.Add("@Since", since)
                    End If

                    Using cmd As New SqlClient.SqlDataAdapter(sql.ToString(), My.Settings.activiserConnectionString)
                        result += cmd.Fill(dt)
                    End Using
                End Using
            Next
        End Using
    End Function

    Public Shared Function FillExpandedClientDataSet(ByVal target As ClientDataSet, ByVal clientMask As Int32) As Integer
        Dim result As Integer = 0
        Using _
            ds As DataSchema = GetDataSchema(clientMask)

            For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
                If Not target.Tables.Contains(entity.EntityName) Then Continue For ' skip tables where clientmask doesn't fit.
                Using dt As DataTable = target.Tables(entity.EntityName)
                    Dim columnList As New List(Of String)
                    For Each attribute As AttributeRow In entity.GetAttributeRows
                        If dt.Columns.Contains(attribute.AttributeName) Then
                            columnList.Add(attribute.AttributeName)
                        End If
                    Next

                    Dim sql As String = String.Format("SELECT {1} FROM {0}", entity.EntityName, String.Join(",", columnList.ToArray()))
                    Using cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
                        result += cmd.Fill(dt)
                    End Using
                End Using
            Next
        End Using
    End Function

    'TODO:
    Public Shared Function FillExpandedClientDataSetByEntity(ByVal target As ClientDataSet, ByVal EntityName As String, ByVal clientMask As Int32) As Integer
        Dim result As Integer = 0
        Using _
            ds As DataSchema = GetDataSchema(clientMask)

            For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
                If Not target.Tables.Contains(entity.EntityName) Then Continue For ' skip tables where clientmask doesn't fit.
                Using dt As DataTable = target.Tables(entity.EntityName)
                    Dim columnList As New List(Of String)
                    For Each attribute As AttributeRow In entity.GetAttributeRows
                        If dt.Columns.Contains(attribute.AttributeName) Then
                            columnList.Add(attribute.AttributeName)
                        End If
                    Next

                    Dim sql As String = String.Format("SELECT {1} FROM {0}", entity.EntityName, String.Join(",", columnList.ToArray()))
                    Using cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
                        result += cmd.Fill(dt)
                    End Using
                End Using
            Next
        End Using
    End Function

    'TODO:
    Public Shared Function FillExpandedClientDataSetByParent(ByVal target As ClientDataSet, ByVal ParentEntityName As String, ByVal clientMask As Int32) As Integer
        Dim result As Integer = 0
        Using _
            ds As DataSchema = GetDataSchema(clientMask)

            For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
                If Not target.Tables.Contains(entity.EntityName) Then Continue For ' skip tables where clientmask doesn't fit.
                Using dt As DataTable = target.Tables(entity.EntityName)
                    Dim columnList As New List(Of String)
                    For Each attribute As AttributeRow In entity.GetAttributeRows
                        If dt.Columns.Contains(attribute.AttributeName) Then
                            columnList.Add(attribute.AttributeName)
                        End If
                    Next

                    Dim sql As String = String.Format("SELECT {1} FROM {0}", entity.EntityName, String.Join(",", columnList.ToArray()))
                    Using cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
                        result += cmd.Fill(dt)
                    End Using
                End Using
            Next
        End Using
    End Function

    'TODO: Entity/Parent/Consultant versions of this.
    Public Shared Function FillExpandedClientDataSet(ByVal target As ClientDataSet, ByVal since? As Date, ByVal clientMask As Int32) As Integer
        If Not since.HasValue OrElse since.Value <= SqlTypes.SqlDateTime.MinValue.Value Then Return FillExpandedClientDataSet(target, clientMask)

        Dim result As Integer = 0
        Using _
            ds As DataSchema = GetDataSchema()

            For Each entity As EntityRow In ds.Entity.Select(Nothing, ds.Entity.FillSequenceColumn.ColumnName)
                Using dt As DataTable = target.Tables(entity.EntityName)
                    Dim columnList As New List(Of String)
                    For Each attribute As AttributeRow In entity.GetAttributeRows
                        columnList.Add(attribute.AttributeName)
                    Next

                    Dim sql As String = String.Format("SELECT {1} FROM {0} WHERE ModifiedDateTime >= @Since", entity.EntityName, String.Join(",", columnList.ToArray()))
                    Using cmd As New SqlClient.SqlDataAdapter(sql, My.Settings.activiserConnectionString)
                        cmd.SelectCommand.Parameters.AddWithValue("@Since", since.Value)
                        result += cmd.Fill(dt)
                    End Using
                End Using
            Next
        End Using
    End Function

    Public Shared Function EntityExists(ByVal entityName As String) As Boolean
        Return CBool((New EntityTableAdapter)._EntityExists(entityName))
    End Function

#Region "Exceptions"
    <Serializable()> _
    Public Class AttributeTypeInvalidException
        Inherits System.Exception

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub

        Public Sub New( _
            ByVal info As System.Runtime.Serialization.SerializationInfo, _
            ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub

        Public Sub New(ByVal attributeName As String, ByVal attributeTypeCode As Integer)
            MyBase.New(String.Format("Attribute '{0}' has an invalid type code ({1})", attributeName, attributeTypeCode))
            Me.AttributeName = attributeName
            Me.AttributeTypeCode = attributeTypeCode
        End Sub

        Private _attributeTypeCode As Integer
        Public Property AttributeTypeCode() As Integer
            Get
                Return _attributeTypeCode
            End Get
            Set(ByVal value As Integer)
                _attributeTypeCode = value
            End Set
        End Property

        Private _propertyName As String
        Public Property AttributeName() As String
            Get
                Return _propertyName
            End Get
            Set(ByVal value As String)
                _propertyName = value
            End Set
        End Property
    End Class
#End Region

End Class

Namespace DataSchemaTableAdapters
    
    Partial Public Class EntityTableAdapter

    End Class
End Namespace
