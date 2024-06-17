Option Strict Off

Imports System
Imports Microsoft.Data.ConnectionUI
Imports activiser.SchemaEditor.activiserSchema
Imports activiser.SchemaEditor.activiserSchemaTableAdapters
Imports activiser.SchemaEditor.CandidateEntityDataSet

'Imports activiser.SchemaEditor.DataAccessLayer

Public Class MainForm
    Dim ds As DataSet
    Private _customDataSet As DataSet = New DataSet("CustomData")

    Public Function GetTableSchema(ByVal entityName As String) As DataTable
        Dim dsReturn As New DataSet(entityName & "DataSet")
        Dim dtReturn As New DataTable(entityName)
        'If conActiviserDB.State <> ConnectionState.Open Then Me.conActiviserDB.Open()
        Try
            Dim cmd As New SqlClient.SqlCommand
            cmd.Connection = sqlConnection ' New SqlClient.SqlConnection(My.Settings.activiserConnectionString) ' Me.conActiviserDB
            cmd.CommandText = "SELECT * FROM dbo." & entityName
            Dim daCustomTable As New SqlClient.SqlDataAdapter(cmd)
            daCustomTable.FillSchema(dtReturn, SchemaType.Source)

        Catch ex As Exception
            MessageBox.Show(String.Format("Error getting schema for table: '{0}': ", ex.Message))
            'Continue
        End Try
        Return dtReturn
    End Function

    'Function GetTableSchema(ByVal er As CandidateEntityRow) As DataTable
    '    Dim dsReturn As New DataSet(er.EntityName & "DataSet")
    '    Dim dtReturn As New DataTable(er.EntityName)
    '    For Each ar As CandidateEntityAttributeRow In er.GetCandidateEntityAttributeRows

    '    Next
    'End Function

    Private Sub LoadExistingSchema()
        _schema.EnforceConstraints = False

        Me.schemaTAM.EntityTableAdapter.Fill(Me._schema.Entity)
        Me.schemaTAM.AttributeTypeTableAdapter.Fill(Me._schema.AttributeType)
        Me.schemaTAM.AttributeTableAdapter.Fill(Me._schema.Attribute)
        Me.schemaTAM.FormTableAdapter.Fill(Me._schema.Form)
        Me.schemaTAM.FormFieldTableAdapter.Fill(Me._schema.FormField)
        Me.schemaTAM.ClientTableAdapter.Fill(Me._schema.Client)

        Me.candidateEntityData = GetCandateEntityDataSet()

        For Each er As EntityRow In _schema.Entity
            Dim t As DataTable = GetTableSchema(er.EntityName)
            _customDataSet.Tables.Add(t)

            Dim thisEntityName As String = er.EntityName
            Dim eq = From qfr As FormRow In _schema.Form _
                     Where qfr.EntityName = thisEntityName

            For Each cf As FormRow In eq
                Dim ser As EntityRow = _schema.Entity.FindByEntityId(cf.EntityId)
                Dim cfe As New CustomFormEditor(cf, ser, Nothing)
                'AddHandler cfe.LabelChanged, AddressOf CustomForm_LabelChanged
                cfe.Dock = DockStyle.Fill
                cfe.DbCatalog = candidateEntityData

                Dim tp As TabPage = New TabPage()
                If Not cf.IsFormLabelNull Then
                    tp.Text = String.Format("{0}.{1}.{2}", cf.ParentEntityName, thisEntityName, cf.FormLabel)
                Else
                    tp.Text = String.Format("{0}.{1}.{2}", cf.ParentEntityName, thisEntityName, cf.FormName)
                End If

                tp.Name = String.Format("TabPage_{0}_{1}_{2}", cf.ParentEntityName, thisEntityName, cf.FormName)
                Me.TabControl1.TabPages.Add(tp)

                tp.Controls.Add(cfe)
            Next
        Next


    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me._schema.HasChanges Then
            Select Case MessageBox.Show("Save changes?", My.Resources.activiserFormTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)
                Case Windows.Forms.DialogResult.Yes

                Case Windows.Forms.DialogResult.No
                Case Windows.Forms.DialogResult.Cancel
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Test Connection
        Do While sqlConnection Is Nothing
            Try
                If Not My.Computer.Keyboard.ShiftKeyDown AndAlso sqlConnection Is Nothing Then
                    sqlConnection = New SqlClient.SqlConnection(My.Settings.activiserConnectionString)
                    sqlConnection.Open()
                    sqlConnection.Close()
                End If
            Catch ex As Exception
                sqlConnection = Nothing
            End Try

            If sqlConnection Is Nothing OrElse My.Computer.Keyboard.ShiftKeyDown Then
                Try
                    ' Use Visual Studio Logon Dialog.
                    Dim dlg As New DataConnectionDialog()
                    dlg.DataSources.Add(DataSource.SqlDataSource)
                    dlg.SetSelectedDataProvider(DataSource.SqlDataSource, DataProvider.SqlDataProvider)
                    dlg.ConnectionString = My.Settings.activiserConnectionString

                    If DataConnectionDialog.Show(dlg) = Windows.Forms.DialogResult.Cancel Then
                        'Exit Try
                        Me.Close()
                    End If
                    sqlConnection = New SqlClient.SqlConnection(dlg.ConnectionString)

                Catch ex2 As Exception
                    MessageBox.Show("Error connecting to activiser database, please try again.")
                End Try
            End If

            If sqlConnection IsNot Nothing Then
                SplashScreen.SetUrl(sqlConnection.DataSource & "." & sqlConnection.Database)

                sqlConnection.Open()
                Dim serverVersion As String = sqlConnection.ServerVersion
                If String.IsNullOrEmpty(serverVersion) Then
                    sqlConnection = Nothing
                Else
                    Try
                        Dim sq As New FormTableAdapter()
                        sq.Connection = sqlConnection
                        sq.GetData()
                        sqlConnection.Close()
                    Catch ex As Exception
                        sqlConnection = Nothing
                    End Try
                End If
            End If
        Loop

        My.Settings.activiserConnectionString = sqlConnection.ConnectionString
        My.Settings.Save()

        Me.schemaTAM.Connection = sqlConnection
        Me.schemaTAM.EntityTableAdapter.Connection = sqlConnection
        Me.schemaTAM.AttributeTableAdapter.Connection = sqlConnection
        Me.schemaTAM.AttributeTypeTableAdapter.Connection = sqlConnection
        Me.schemaTAM.ClientTableAdapter.Connection = sqlConnection
        Me.schemaTAM.FormFieldTableAdapter.Connection = sqlConnection
        Me.schemaTAM.FormTableAdapter.Connection = sqlConnection

        Me.LoadExistingSchema()
        Me._schema.AcceptChanges()
        Me._customDataSet.AcceptChanges()
    End Sub

    Private Sub AddTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click, NewCustomFormButton.Click
        AddForm()
    End Sub

    Private Sub SaveData()

        For Each ccr As FormFieldRow In CType(Me._schema.FormField.Select("", "", DataViewRowState.Deleted), FormFieldRow())
            Try
                ccr.RejectChanges()
                Me.schemaTAM.FormFieldTableAdapter.Delete(ccr.FormFieldId)
                ccr.Delete()
                ccr.AcceptChanges()
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        Next

        Dim fq = From qf In _schema.Form
        For Each f As FormRow In fq
            'Dim entity As EntityRow = _schema.Entity.FindByEntityName(f.EntityName)

            'If entity Is Nothing Then
            '    ' Add entity.
            '    entity = _schema.Entity.AddEntityRow(Guid.NewGuid(), f.EntityName, f.EntityPK, 0, 0, -1, 99, f.ParentEntityName, f.EntityParentFK, DateTime.UtcNow, My.User.Name, DateTime.UtcNow, My.User.Name)

            'End If
        Next

        Dim ds As activiserSchema = CType(Me._schema.GetChanges(DataRowState.Added Or DataRowState.Modified), activiserSchema)
        If ds IsNot Nothing Then
            Dim utcnow As DateTime = DateTime.UtcNow
            For Each dr As FormFieldRow In ds.FormField
                If dr.IsCreatedNull Then dr.Created = utcnow
                dr.Modified = utcnow
            Next
            For Each dr As FormRow In ds.Form
                If dr.IsCreatedNull Then dr.Created = utcnow
                dr.Modified = utcnow
            Next
            For Each dr As EntityRow In ds.Entity
                If Not dr.IsParentEntityIdNull AndAlso dr.ParentEntityId = Guid.Empty Then dr.SetParentEntityIdNull()
                If dr.IsCreatedNull Then dr.Created = utcnow
                dr.Modified = utcnow
            Next
            For Each dr As AttributeRow In ds.Attribute
                If dr.IsCreatedNull Then dr.Created = utcnow
                dr.Modified = utcnow
            Next

            Me.schemaTAM.UpdateAll(ds)
            'Me.schemaTAM.FormTableAdapter.Update(Me._schema)
            'Me.schemaTAM.FormFieldTableAdapter.Update(Me._schema.FormField)
            Me._schema.AcceptChanges()
        End If

    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        Me.Validate()
        SaveData()
    End Sub

    Private Sub Cut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click, CutToolStripButton.Click
        Dim cfe As CustomFormEditor = CType(Me.TabControl1.SelectedTab.Controls(0), CustomFormEditor)
        cfe.RemoveCurrentItem()
    End Sub

    Private Sub PdaForm_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TabControl1.DragEnter
        Dim lvi As ListViewItem = TryCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem)
        If lvi IsNot Nothing Then
            Dim tp As TabPage = Me.TabControl1.SelectedTab
            Dim cfe As CustomFormEditor = TryCast(tp.Controls(0), CustomFormEditor)
            If cfe IsNot Nothing Then
                Dim c As DataColumn = TryCast(lvi.Tag, DataColumn)
                If c IsNot Nothing Then
                    If c.Table.TableName = cfe.CustomForm.EntityName Then
                        e.Effect = DragDropEffects.Copy
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub RemoveTableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveTableToolStripMenuItem.Click
        If Me.TabControl1.SelectedTab IsNot Nothing Then
            Dim cfe As CustomFormEditor = TryCast(Me.TabControl1.SelectedTab.Controls(0), CustomFormEditor)
            If cfe IsNot Nothing Then
                Dim cf As FormRow = cfe.CustomForm
                If cf.GetFormFieldRows.Length = 0 Then
                    If MessageBox.Show(String.Format("Remove empty custom form for table '{0}' ?", cf.EntityName), My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        Dim tp As TabPage
                        'Me._customDataSet.Tables.Remove(cf.TableName)
                        'My.Settings.tablePickerExclusionList.Remove(cf.TableName)
                        cf.Delete()
                        tp = Me.TabControl1.SelectedTab
                        Me.TabControl1.TabPages.Remove(tp)
                        tp.Controls(0).Dispose()
                        tp.Tag = Nothing
                        tp.Dispose()
                        tp = Nothing
                    End If
                Else
                    If MessageBox.Show(String.Format("Delete non-empty custom form for table '{0}' ?", cf.EntityName), My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        'Me._customDataSet.Tables.Remove(cf.TableName)
                        'My.Settings.tablePickerExclusionList.Remove(cf.TableName)
                        cf.Delete()
                        Dim tp As TabPage = Me.TabControl1.SelectedTab
                        Me.TabControl1.TabPages.Remove(tp)
                        tp.Controls(0).Dispose()
                        tp.Tag = Nothing
                        tp.Dispose()
                        tp = Nothing
                    End If
                End If
            End If
        End If
    End Sub

    Private Shared Function GetPkType(ByVal pkAttribute As CandidateEntityAttributeRow) As AttributeType
        If pkAttribute Is Nothing Then
            Return AttributeType.Unknown
        ElseIf pkAttribute.AttributeType = "uniqueidentifier" Then
            Return AttributeType.GuidPK
        ElseIf pkAttribute.AttributeType = "int" Then
            Return AttributeType.IntegerPK
        ElseIf pkAttribute.AttributeType = "bigint" Then
            Return AttributeType.BigIntPK
        ElseIf pkAttribute.AttributeType.StartsWith("nvarchar") Then
            Return AttributeType.StringPK
        Else
            Return AttributeType.Unknown
        End If
    End Function

    Friend Function getAttributeTypeCode(ByVal car As CandidateEntityAttributeRow) As AttributeType
        Dim isPk As Boolean = car.AttributeIsPK
        Dim attName As String = car.AttributeName
        Select Case car.AttributeType
            Case "int"
                If isPk Then Return AttributeType.IntegerPK
                If attName.EndsWith("ID", StringComparison.InvariantCultureIgnoreCase) Then Return AttributeType.IntegerFK
                Return AttributeType.Integer

            Case "nvarchar", "nchar", "varchar", "char", "ntext", "text"
                If isPk Then Return AttributeType.StringPK
                If attName.Contains("email") Then Return AttributeType.EmailAddress
                If attName.Contains("phone") Then Return AttributeType.PhoneNumber
                If attName.Contains("web") OrElse attName.Contains("url") Then Return AttributeType.WebAddress
                If attName.Contains("address") Then Return AttributeType.Address
                Return AttributeType.String

            Case "datetime", "smalldatetime", "date", "time", "datetimeoffset", "datetime2"
                If attName.EndsWith("Date") Then Return AttributeType.DateOnly
                If attName.EndsWith("DateTime") Then Return AttributeType.DateTime
                If attName.EndsWith("Time") Then Return AttributeType.TimeOnly
                Return AttributeType.DateTime

            Case "float", "real"
                Return AttributeType.Float

            Case "decimal", "numeric"
                Return AttributeType.Decimal

            Case "money", "smallmoney"
                Return AttributeType.Currency

            Case "uniqueidentifier"
                If isPk Then Return AttributeType.GuidPK
                Return AttributeType.Guid

            Case "bit"
                Return AttributeType.Boolean

            Case "smallint"
                Return AttributeType.Short

            Case "tinyint"
                Return AttributeType.Byte

            Case "bigint"
                If isPk Then Return AttributeType.BigIntPK
                If attName.EndsWith("ID", StringComparison.InvariantCultureIgnoreCase) Then Return AttributeType.BigIntFK
                Return AttributeType.BigInt
        End Select

        Return AttributeType.Unknown
    End Function

    Private Sub AddForm()
        candidateEntityData = CandidateEntityDataSet.GetCandateEntityDataSet() ' force refresh
        Dim tablePicker As New TablePicker(candidateEntityData.CandidateEntity)

        tablePicker.ShowDialog()
        For Each cer As CandidateEntityRow In tablePicker.SelectedTables
            Dim candidateEntityAttributes As CandidateEntityAttributeRow() = cer.GetCandidateEntityAttributeRows()
            Dim entityName As String = cer.EntityName

            Dim ser As EntityRow
            Dim t As DataTable
            Dim formName As String
            Dim pkName As String = String.Empty
            Dim pkType As AttributeType = AttributeType.Unknown

            If Me._customDataSet.Tables.Contains(entityName) Then
                t = Me._customDataSet.Tables(entityName)
                ser = Me._schema.Entity.FindByEntityName(entityName)
                Dim q As Integer = Aggregate cfr As FormRow In _schema.Form Where cfr.EntityName = entityName Into Count()
                Dim defaultFormName As String = String.Format("{0}_{1}", entityName, q + 1)

                Dim ef As Integer = Aggregate qf As FormRow In _schema.Form Where qf.FormName = entityName Into Count()

                If ef = 0 Then
                    formName = entityName
                Else
                    formName = InputBox("Please enter name for custom form:", My.Resources.activiserFormTitle, defaultFormName)
                    If String.IsNullOrEmpty(formName) Then 'cancelled
                        Continue For
                    End If
                End If
            Else
                t = GetTableSchema(entityName)
                Dim eaq = From qea In candidateEntityAttributes Where qea.AttributeIsPK Take 1
                Dim pkAttribute As CandidateEntityAttributeRow
                For Each qea As CandidateEntityAttributeRow In eaq
                    pkAttribute = qea
                    pkName = pkAttribute.AttributeName
                    pkType = GetPkType(pkAttribute)
                    Exit For
                Next
                If String.IsNullOrEmpty(pkName) OrElse (pkType = AttributeType.Unknown) Then
                    MessageBox.Show(String.Format("Unable to find valid primary key for entity '{0}'", entityName))
                    Continue For
                End If

                ser = Me._schema.Entity.NewEntityRow()
                ser.EntityId = Guid.NewGuid
                ser.EntityName = entityName
                ser.PrimaryKeyAttributeName = pkName
                ser.PrimaryKeyAttributeType = pkType
                ser.ClientMask = -1
                ser.ClientMaskInsert = -1
                ser.ClientMaskUpdate = -1
                ser.Created = DateTime.UtcNow
                ser.CreatedBy = My.User.Name
                ser.SetEffectiveFromNull()
                ser.SetEffectiveUntilNull()
                ser.FillSequence = Me._schema.Entity.Count * 10
                ser.IsCoreEntity = False
                ser.Modified = DateTime.UtcNow
                ser.ModifiedBy = My.User.Name
                ser.Version = 1

                Me._schema.Entity.AddEntityRow(ser)

                For Each car As CandidateEntityAttributeRow In candidateEntityAttributes
                    Dim ar As AttributeRow = Me._schema.Attribute.NewAttributeRow()
                    ar.AttributeId = Guid.NewGuid()
                    ar.AttributeName = car.AttributeName
                    ar.ClientMask = -1
                    ar.ClientMaskInsert = -1
                    ar.ClientMaskUpdate = -1
                    ar.EntityId = ser.EntityId
                    ar.Expression = String.Empty
                    ar.IsCoreAttribute = False
                    ar.IsPrimaryKeyAttribute = car.AttributeIsPK
                    ar.MaxLength = car.MaxLength
                    ar.Required = Not car.AttributeIsNullable
                    ar.Created = DateTime.UtcNow
                    ar.Modified = DateTime.UtcNow
                    ar.CreatedBy = My.User.Name
                    ar.ModifiedBy = My.User.Name
                    ar.AttributeTypeCode = getAttributeTypeCode(car)
                    Me._schema.Attribute.AddAttributeRow(ar)
                Next
                formName = entityName
            End If

            Dim cf As FormRow = Me._schema.Form.NewFormRow()
            cf.FormName = formName
            cf.FormLabel = formName
            cf.FormId = Guid.NewGuid
            cf.EntityId = ser.EntityId
            cf.EntityName = ser.EntityName
            cf.EntityPK = ser.PrimaryKeyAttributeName
            cf.Created = DateTime.UtcNow
            cf.Modified = DateTime.UtcNow
            cf.CreatedBy = My.User.Name
            cf.ModifiedBy = My.User.Name
            cf.LockCode = 0
            cf.Priority = 0
            cf.ClientMask = -1

            cf.EntityParentFK = ""

            ' try and get some useful defaults...
            Dim possibleParentColumns As New Collections.Specialized.StringCollection

            For Each ar As CandidateEntityAttributeRow In candidateEntityAttributes
                If cf.EntityParentFK = "" Then
                    Dim tcn As String = ar.AttributeName.ToLower.Trim
                    If tcn = "jobuid" Then
                        cf.ParentEntityId = _schema.Entity.FindByEntityName("Job").EntityId
                        cf.ParentEntityName = "Job"
                        cf.ParentPK = "JobUID"
                        cf.EntityParentFK = ar.AttributeName
                    ElseIf tcn = "requestuid" Then
                        cf.ParentEntityId = _schema.Entity.FindByEntityName("Request").EntityId
                        cf.ParentEntityName = "Request"
                        cf.ParentPK = "RequestUID"
                        cf.EntityParentFK = ar.AttributeName
                    ElseIf tcn = "clientsiteuid" OrElse tcn = "clientuid" OrElse tcn = "siteuid" Then
                        cf.ParentEntityId = _schema.Entity.FindByEntityName("ClientSite").EntityId
                        cf.ParentEntityName = "ClientSite"
                        cf.ParentPK = "ClientSiteUID"
                        cf.EntityParentFK = ar.AttributeName
                    End If
                End If

                If ar.AttributeType = "uniqueidentifier" Then
                    possibleParentColumns.Add(ar.AttributeName)
                End If
                If Not String.IsNullOrEmpty(cf.EntityParentFK) And Not String.IsNullOrEmpty(cf.EntityPK) Then
                    Exit For
                End If
            Next

            If String.IsNullOrEmpty(cf.EntityParentFK) Then
                Dim ftPicker As New FormTypePicker(t.TableName, possibleParentColumns)
                If ftPicker.ShowDialog = Windows.Forms.DialogResult.OK AndAlso Not String.IsNullOrEmpty(ftPicker.SelectedColumn) Then
                    cf.ParentEntityId = _schema.Entity.FindByEntityName(ftPicker.ParentEntity).EntityId
                    cf.ParentEntityName = ftPicker.ParentEntity
                    cf.EntityParentFK = ftPicker.SelectedColumn
                Else
                    Exit Sub
                End If
            End If


            If Not _customDataSet.Tables.Contains(entityName) Then
                _customDataSet.Tables.Add(t)
            End If

            cf.MaxItems = If(cf.EntityParentFK = cf.EntityPK, CByte(1), CByte(0))
            Me._schema.Form.AddFormRow(cf)
            Dim tp As TabPage = New TabPage(String.Format("{0}.{1}.{2}", cf.ParentEntityName, cf.EntityName, cf.FormLabel))
            tp.Name = cf.FormName & "TabPage"
            Dim cfe As New CustomFormEditor(cf, ser, cer)
            cfe.DbCatalog = candidateEntityData
            cfe.Dock = DockStyle.Fill
            tp.Controls.Add(cfe)
            Me.TabControl1.TabPages.Add(tp)
            Me.TabControl1.SelectedTab = tp
            tp.Focus()
        Next
    End Sub

    'Private Sub CustomForm_LabelChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim cfe As CustomFormEditor = TryCast(sender, CustomFormEditor)
    '    If cfe IsNot Nothing Then
    '        Dim tp As TabPage = TryCast(cfe.Parent, TabPage)
    '        If tp IsNot Nothing Then
    '            tp.Text = String.Format("{0}.{1}.{2}", cfe.CustomForm.ParentEntityName, cfe.CustomForm.EntityName, cfe.FormLabelTextBox.Text)
    '        End If
    '    End If
    'End Sub
End Class
