Imports System.Text
Imports activiser.SchemaEditor.activiserSchema
Imports activiser.SchemaEditor.CandidateEntityDataSet

Public Class CustomFormEditor
    Private _pdaDataSet As activiserSchema
    Private _datatable As EntityRow
    Private _customForm As FormRow
    Private _selectedPdaControl As CustomControl
    Private _selectedFormFieldRow As FormFieldRow
    Private _dbCatalog As CandidateEntityDataSet
    Private _items As Integer = 0
    Private _loading As Boolean = True

    Private WithEvents _pdaControlCollection As PdaControlCollection

    'Public Event LabelChanged As EventHandler

    Public Sub New(ByVal cf As FormRow, ByVal t As EntityRow, ByVal er As CandidateEntityRow)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _pdaControlCollection = New PdaControlCollection

        'Me.SuspendLayout()
        Me._customForm = cf
        Me.FormNameTextBox.Text = cf.FormName
        Me.FormLabelTextBox.Text = cf.FormLabel
        Me.TableName.Text = t.EntityName '.TableName
        Me.CustomTablePK.Text = cf.EntityPK
        Me.CustomTableFK.Text = cf.EntityParentFK
        Me.ParentTable.Text = cf.ParentEntityName
        Me.ParentPK.Text = cf.ParentPK
        Me.FormCopyLimit.Value = cf.MaxItems
        Me.FormPrioritySelector.Value = cf.Priority
        Me.FormLockCodePicker.SelectedValue = cf.LockCode
        'Me.ReadOnlyFormCheckBox.Checked = cf.LockCode <> 0

        Me._datatable = t
        Me._pdaDataSet = CType(cf.Table.DataSet, activiserSchema)
        Me.StyleSelector.Items.AddRange(GetEnumDescriptions(GetType(ControlType)))
        Me.LockCodeSelector.Items.AddRange(GetEnumDescriptions(GetType(LockCode)))
        Me.FormLockCodePicker.Items.AddRange(GetEnumDescriptions(GetType(LockCode)))

        addColumnsToListView(t)

        Dim ccrs() As FormFieldRow = cf.GetFormFieldRows
        Dim s(ccrs.Length - 1) As Integer
        Dim cc As FormFieldRow
        For i As Integer = 0 To ccrs.Length - 1
            cc = ccrs(i)
            s(i) = If(cc.DisplayOrder = 0, 255, cc.DisplayOrder)
        Next
        Array.Sort(s, ccrs)
        For Each cc In ccrs
            If _items < 250 Then
                Dim pc As CustomControl = addItem(cc)
                Debug.Print(pc.Size.ToString)
            Else
                Exit For
            End If
        Next
        Me.ResumeLayout()
        Me._pdaControlCollection.ArrangeItems()
        _loading = False
        Me.PdaForm.Refresh()
        Me.PdaFormGroup.Refresh()
        Me.Refresh()
    End Sub

    Public Property DbCatalog() As CandidateEntityDataSet
        Get
            Return _dbCatalog
        End Get
        Set(ByVal value As CandidateEntityDataSet)
            _dbCatalog = value
        End Set
    End Property

    Public ReadOnly Property CustomForm() As FormRow
        Get
            Return _customForm
        End Get
    End Property

#Region "Add Control"
    Private Function addItem(ByVal c As FormFieldRow) As CustomControl
        If _items > 200 Then
            MessageBox.Show("Can't add more than 200 controls to a form.", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Return Nothing
        End If

        If Me.PdaForm.Controls.ContainsKey(c.AttributeName) Then
            MessageBox.Show(My.Resources.CustomFieldAlreadyPresent, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Return Nothing
        End If

        Dim uc As New CustomControl(c)
        uc.PdaControlCollection = _pdaControlCollection
        Me.PdaForm.Controls.Add(uc)

        If c.FieldType = ControlType.DropDownList Then
            uc.ListDataSource = c.ListDataSource
            uc.ListDisplayColumn = c.ListDisplayColumn
            uc.ListValueColumn = c.ListValueColumn
            uc.FillList()
        End If

        _items += 1

        Return uc

    End Function

    Private Function addItem(ByVal c As AttributeRow) As CustomControl
        'If Me.PdaForm.Controls.ContainsKey(c.ColumnName) Then
        '    If MessageBox.Show(My.Resources.CustomFieldAlreadyPresent, My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
        '        Return Nothing
        '    End If
        '    'MessageBox.Show(My.Resources.CustomFieldAlreadyPresent, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        'End If

        Dim ccr As FormFieldRow = Me._pdaDataSet.FormField.NewFormFieldRow()
        Dim ct As ControlType
        ccr.AttributeName = c.AttributeName
        ccr.AttributeId = c.AttributeId
        ccr.Label = ExpandName(c.AttributeName)
        ccr.LabelWidth = 50
        'ccr.CreatedDateTime = Now
        'ccr.ModifiedDateTime = Now

        ccr.FormFieldId = Guid.NewGuid
        ccr.FormRow = Me._customForm
        ccr.DisplayOrder = CByte(_pdaControlCollection.TopSequenceNumber + 1)
        ccr.DecimalPlaces = 2
        ccr.Created = DateTime.UtcNow
        ccr.CreatedBy = My.User.Name
        ccr.Modified = DateTime.UtcNow
        ccr.ModifiedBy = My.User.Name
        ccr.LockCode = 0
        ccr.MinimumValue = 0
        ccr.MaximumValue = 1000
        ccr.SortPriority = 0
        ccr.Width = 100

        ccr.Position = "FL" ' default is full-width, label on left.

        Select Case CType(c.AttributeTypeCode, AttributeType) ' c.DataType.Name
            Case AttributeType.String, AttributeType.StringPK, AttributeType.StringFK _
                , AttributeType.WebAddress, AttributeType.PhoneNumber, AttributeType.Password, AttributeType.EmailAddress _
                , AttributeType.Address
                ct = ControlType.TextBox
                ccr.MaximumValue = c.MaxLength
                ' 100 may seem like a lot for one line, but in practice, this is the length used for 'subject' or 'short description' fields.
                If c.MaxLength < 0 OrElse c.MaxLength > 500 Then ' effectively unlimited length for a PDA
                    ccr.Lines = 6
                    ccr.Position = "FT"
                Else
                    ccr.Lines = Math.Max(CByte(1), CByte(c.MaxLength \ 100))
                End If
                If c.MaxLength > 50 Then ' for long textboxes, put the label on top.
                    ccr.Position = "FT" ' move label to top for multi-line text boxes.
                End If
            Case AttributeType.Boolean ' TypeCode.Boolean.ToString
                ct = ControlType.CheckBox
                ccr.Position = "FR" ' checkboxes don't have a 'checkalign' property on a PDA, so the label on the left is a hack - label on the right much better.
                ccr.LabelWidth = 80
            Case AttributeType.Integer, AttributeType.BigInt, AttributeType.Short, AttributeType.Byte
                If c.AttributeName.EndsWith("ID") Then ' assume lookup
                    ct = ControlType.DropDownList
                    ccr.DecimalPlaces = 0
                Else
                    ct = ControlType.Number
                    ccr.DecimalPlaces = 0
                End If
            Case AttributeType.IntegerFK, AttributeType.BigIntFK
                ct = ControlType.DropDownList
                ccr.DecimalPlaces = 0
            Case AttributeType.IntegerPK, AttributeType.BigIntPK, AttributeType.IntIdentity, AttributeType.BigIntIdentity
                ct = ControlType.Number
                ccr.DecimalPlaces = 0
            Case AttributeType.Float, AttributeType.Decimal, AttributeType.Currency '  TypeCode.Double.ToString, TypeCode.Single.ToString, TypeCode.Decimal.ToString
                ct = ControlType.Number
                ccr.DecimalPlaces = 2

            Case AttributeType.DateTime
                ct = ControlType.DateTime
            Case AttributeType.DateOnly
                ct = ControlType.Date
            Case AttributeType.TimeOnly
                ct = ControlType.Time
            Case AttributeType.TimeStamp
                ct = ControlType.DateTime

            Case AttributeType.Guid, AttributeType.GuidFK
                ct = ControlType.DropDownList
            Case AttributeType.GuidPK
                ct = ControlType.TextBox
        End Select
        ccr.FieldType = CByte(ct)

        ' ccr.CustomControlTypeRow = Me._pdaDataSet.CustomControlType.FindByCustomControlTypeID(ct)
        Me._pdaDataSet.FormField.AddFormFieldRow(ccr)
        Return addItem(ccr)
    End Function
#End Region

#Region "Fill Column List"
    Private Sub addColumnsToListView(ByVal t As EntityRow)
        Dim lvg As New ListViewGroup(t.EntityName, t.EntityName)

        For Each c As AttributeRow In t.GetAttributeRows
            Dim style As String = ""
            Select Case CType(c.AttributeTypeCode, AttributeType)
                Case AttributeType.Guid, AttributeType.GuidFK, AttributeType.GuidPK ' "Guid"
                    If c.AttributeName = _customForm.EntityParentFK OrElse c.AttributeName = _customForm.EntityPK Then
                        style = "ID"
                    Else
                        style = "ComboBox"
                    End If
                Case AttributeType.String, AttributeType.StringFK, AttributeType.StringPK _
                    , AttributeType.EmailAddress, AttributeType.PhoneNumber, AttributeType.WebAddress _
                    , AttributeType.Expression
                    style = "TextBox"
                Case AttributeType.Password
                    style = "TextBox"
                Case AttributeType.Boolean ' TypeCode.Boolean.ToString : 
                    style = "CheckBox"
                Case AttributeType.Integer, AttributeType.IntegerFK, AttributeType.IntegerPK, AttributeType.IntIdentity _
                    , AttributeType.BigInt, AttributeType.BigIntFK, AttributeType.BigIntPK, AttributeType.BigIntIdentity _
                    , AttributeType.Short, AttributeType.Byte
                    ' TypeCode.Int32.ToString, TypeCode.Byte.ToString, TypeCode.Int16.ToString, TypeCode.SByte.ToString, TypeCode.Int64.ToString, TypeCode.UInt16.ToString, TypeCode.UInt32.ToString, TypeCode.UInt64.ToString
                    If c.AttributeName.EndsWith("ID") Then ' assume lookup
                        style = "ComboBox"
                    Else
                        style = "Number"
                    End If
                Case AttributeType.Float, AttributeType.Decimal, AttributeType.Currency ' TypeCode.Double.ToString, TypeCode.Single.ToString, TypeCode.Decimal.ToString
                    style = "Number"
                Case AttributeType.DateOnly, AttributeType.TimeStamp, AttributeType.DateTime, AttributeType.TimeOnly ' "Date", "DateTime", "Time"
                    If (c.AttributeName.StartsWith("Created") OrElse c.AttributeName.StartsWith("Modified")) Then
                        style = "AuditDate"
                    Else
                        style = "Date"
                    End If
                Case Else
                    Debug.WriteLine(c.AttributeName & "has an unknown type") '.DataType.Name)
                    style = ""
            End Select
            If style <> "" Then
                Dim lvi As New ListViewItem(ExpandName(c.AttributeName))
                lvi.SubItems.Add(style)
                lvi.SubItems.Add(c.AttributeName)
                lvi.Tag = c
                If style = "ID" Then ' c.ColumnName = "ID" OrElse c.ColumnName = "UID" OrElse c.ColumnName = t.TableName & "ID" OrElse c.ColumnName = t.TableName & "UID" Then
                    lvi.ForeColor = SystemColors.InfoText
                    lvi.BackColor = SystemColors.Info
                    lvi.ToolTipText = "Identifier column"
                ElseIf style = "AuditDate" Then
                    lvi.ForeColor = SystemColors.InfoText
                    lvi.BackColor = SystemColors.Info
                    lvi.ToolTipText = "Data change tracking column"
                Else
                    lvi.ForeColor = SystemColors.WindowText
                End If
                'If _pdaControlCollection.Contains(c.ColumnName) Then
                '    lvi.ForeColor = SystemColors.GrayText
                'Else

                'End If
                Me.FieldChooser.Items.Add(lvi)
            End If
        Next
    End Sub
#End Region

#Region "Move Control"
    Public Sub Swap(ByVal control1 As CustomControl, ByVal control2 As CustomControl)
        Dim l1 As Point = control1.Location
        Dim l2 As Point = control2.Location
        Dim h1 As Integer = control1.Height
        Dim h2 As Integer = control2.Height

        ' adjust location for height
        If h1 <> h2 Then
            Dim hd As Integer = Math.Abs(h1 - h2)
            If l1.Y > l2.Y Then
                If h1 > h2 Then
                    l1.Y -= hd
                Else
                    l1.Y += hd
                End If
            Else
                If h1 > h2 Then
                    l2.Y -= hd
                Else
                    l2.Y += hd
                End If
            End If
        End If

        control1.Location = l2
        control2.Location = l1

        Dim s As Byte = control1.DisplayOrder
        control1.DisplayOrder = control2.DisplayOrder
        control2.DisplayOrder = s
    End Sub

    Private Sub MoveControlUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveControlUp.Click
        If Me._selectedPdaControl IsNot Nothing Then
            If _pdaControlCollection.Find(Me._selectedPdaControl).Previous IsNot Nothing Then
                Me._pdaControlCollection.MoveUp(Me._selectedPdaControl)
                Me._pdaControlCollection.ArrangeItems()
            End If
        End If
    End Sub

    Private Sub MoveControlDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveControlDown.Click
        If Me._selectedPdaControl IsNot Nothing Then
            If _pdaControlCollection.Find(Me._selectedPdaControl).Next IsNot Nothing Then
                Me._pdaControlCollection.MoveDown(Me._selectedPdaControl)
                Me._pdaControlCollection.ArrangeItems()
            End If
        End If
    End Sub
#End Region

#Region "Drag & Drop"
    Private _DragInProgress As Boolean = False
    Private Sub FieldChooser_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles FieldChooser.ItemDrag
        Dim lvi As ListViewItem = TryCast(e.Item, ListViewItem)
        If lvi IsNot Nothing AndAlso lvi.ForeColor <> SystemColors.GrayText AndAlso lvi.ForeColor <> SystemColors.InfoText Then
            DoDragDrop(e.Item, DragDropEffects.Copy)
        End If
        'End If
    End Sub

    Private Sub PdaForm_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PdaForm.DragDrop
        Dim lvi As ListViewItem = TryCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem)
        If lvi Is Nothing OrElse lvi.ListView IsNot Me.FieldChooser Then
            Return
        End If

        ' ok, so we have items to drop. lets get them from the source, since the DragEventArgs only have one of them!
        For Each lvi In Me.FieldChooser.SelectedItems
            Dim c As AttributeRow = TryCast(lvi.Tag, AttributeRow)
            If c IsNot Nothing Then
                If c.EntityId = Me._customForm.EntityId Then 'If c.Table.TableName = Me._customForm.EntityName Then
                    Dim newControl As CustomControl = addItem(c)
                    Me._pdaControlCollection.ArrangeItems()
                    newControl.Select()
                    'Me._selectedPdaControl = newControl
                End If
            End If
        Next

        Me.FieldChooser.SelectedItems.Clear()
    End Sub

    Private Sub PdaForm_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PdaForm.DragEnter
        Dim lvi As ListViewItem = TryCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem)
        If lvi IsNot Nothing Then
            Dim c As AttributeRow = TryCast(lvi.Tag, AttributeRow)
            If c IsNot Nothing Then
                If c.EntityId = Me._customForm.EntityId Then ' .Table.TableName = Me._customForm.EntityName Then
                    e.Effect = DragDropEffects.Copy
                End If
            End If
        End If
    End Sub

    Private Sub FieldChooser_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FieldChooser.DragEnter
        Dim pc As CustomControl = TryCast(e.Data.GetData(GetType(CustomControl)), CustomControl)
        If pc IsNot Nothing Then
            If Me._pdaControlCollection.Contains(pc) Then
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    Private Sub FieldChooser_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FieldChooser.DragDrop
        Dim pc As CustomControl = TryCast(e.Data.GetData(GetType(CustomControl)), CustomControl)
        If pc IsNot Nothing Then
            If Me._pdaControlCollection.Contains(pc) Then
                If pc.FormFieldRow.RowState = DataRowState.Added Then '.IsCreatedNull Then ' never saved
                    RemoveItem(pc)
                Else
                    MessageBox.Show("Unable to remove custom control that has already been saved to the database", My.Resources.activiserFormTitle)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Display"
    Private _setViewInProgress As Boolean
    Private Sub SetView()
        _setViewInProgress = True
        Dim pc As CustomControl = _selectedPdaControl
        Dim cc As FormFieldRow = pc.FormFieldRow
        Dim cf As FormRow = Me._customForm

        Me.CurrentFieldLabel.Text = cf.EntityName & "." & cc.AttributeName
        Me.LockCodeSelector.SelectedIndex = Me.LockCodeSelector.Items.IndexOf(pc.LockCode.ToString())
        'Me.ReadOnlyControlCheckBox.Checked = cc.LockCode <> 0
        Me.StyleSelector.SelectedItem = pc.ControlType.ToString()

        If pc.ControlType = ControlType.CheckBox Then
            Me.LabelTopRadio.Enabled = False
            Me.LabelNoneRadio.Enabled = False
        Else
            Me.LabelTopRadio.Enabled = True
            Me.LabelNoneRadio.Enabled = True
        End If

        Select Case pc.LabelPosition
            Case LabelPosition.Left
                Me.LabelLeftRadio.Checked = True
            Case LabelPosition.None
                Me.LabelNoneRadio.Checked = True
            Case LabelPosition.Right
                Me.LabelRightRadio.Checked = True
            Case LabelPosition.Top
                Me.LabelTopRadio.Checked = True
        End Select

        ' set control position before width setting, to avoid automatic, unintentional changing of values
        Select Case pc.ControlPosition
            Case ControlPosition.FullWidth
                Me.ControlFullWidthRadio.Checked = True
            Case ControlPosition.Left
                Me.ControlLeftRadio.Checked = True
            Case ControlPosition.Right
                Me.ControlRightRadio.Checked = True
        End Select

        Me.ControlWidthNumberBox.Value = cc.Width

        If pc.ControlType = ControlType.TextBox Then
            Me.LinesNumberBox.Value = pc.Lines
            Me.LinesNumberBox.Minimum = 1
            Me.LinesNumberBox.Maximum = 10
        ElseIf pc.ControlType = ControlType.DropDownList Then
            Me.ListLimitToList.Checked = pc.FormFieldRow.Lines <> 0
            Me.LinesNumberBox.Minimum = 0
            Me.LinesNumberBox.Maximum = 255
            Me.LinesNumberBox.Value = pc.FormFieldRow.Lines
        Else
            Me.LinesNumberBox.Minimum = 0
            Me.LinesNumberBox.Maximum = 255
            Me.LinesNumberBox.Value = pc.FormFieldRow.Lines
        End If

        Me.DecimalPlacesNumberBox.Value = pc.DecimalPlaces

        Me.MaxValueNumberBox.DecimalPlaces = pc.DecimalPlaces
        Me.MaxValueNumberBox.Value = pc.MaximumValue
        Me.TextMaxLength.Maximum = pc.MaximumValue
        Me.TextMaxLength.Value = CInt(pc.MaximumValue)

        Me.MinValueNumberBox.DecimalPlaces = pc.DecimalPlaces
        Me.MinValueNumberBox.Value = pc.MinimumValue

        If Not cc.IsListDataSourceNull AndAlso Not String.IsNullOrEmpty(cc.ListDataSource) Then
            Me.ListSourceTableTextBox.Text = cc.ListDataSource
            Me.ListSourceDataColumnTextBox.Text = cc.ListValueColumn
            Me.ListSourceDisplayColumnTextBox.Text = cc.ListDisplayColumn
        End If

        Dim manualList As Boolean = (Not cc.IsListDataNull AndAlso Not String.IsNullOrEmpty(cc.ListData))

        Me.ManualListCheckBox.Checked = manualList
        Me.ManualListEditButton.Enabled = manualList

        Me.ListSourceTableTextBox.Enabled = Not manualList
        Me.ListSourceDataColumnTextBox.Enabled = Not manualList
        Me.ListSourceDisplayColumnTextBox.Enabled = Not manualList

        Me.ListSourceTableButton.Enabled = Not manualList
        Me.ListSourceDataColumnButton.Enabled = Not manualList
        Me.ListSourceDisplayColumnButton.Enabled = Not manualList
        _setViewInProgress = False
    End Sub

#End Region


#Region "Custom Control Selection"

    Private Sub pcc_SelectedItemChanged(ByVal sender As Object, ByVal e As SelectedItemChangedEventArgs) Handles _pdaControlCollection.SelectedItemChanged
        Dim pc As CustomControl
        _selectedPdaControl = e.PdaControl
        For Each pc In Me._pdaControlCollection
            If pc IsNot e.PdaControl Then
                If pc.DisplayOrder = 0 Then
                    pc.BackColor = Color.Brown
                Else
                    pc.BackColor = SystemColors.Control
                End If
            Else
                pc.BackColor = SystemColors.Info
            End If
        Next
        If _selectedPdaControl IsNot Nothing Then
            Me._selectedFormFieldRow = Me._selectedPdaControl.FormFieldRow
            Dim ct As ControlType = CType(Me._selectedFormFieldRow.FieldType, ControlType) 'CType(System.Enum.Parse(GetType(ControlTypes), CStr(Me.StyleSelector.SelectedItem)), ControlTypes)

            Me.TextBoxGroup.Visible = ct = ControlType.TextBox
            Me.NumberGroup.Visible = ct = ControlType.Number
            Me.ListGroup.Visible = ct = ControlType.DropDownList
            Me.CheckBoxGroup.Visible = ct = ControlType.CheckBox
        End If
        SetView()
    End Sub
#End Region


#Region "Custom Control Basic Properties"
    Private Sub StyleSelector_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StyleSelector.SelectedValueChanged, StyleSelector.SelectionChangeCommitted
        If _setViewInProgress Then Return
        Try
            Dim ct As ControlType = CType(System.Enum.Parse(GetType(ControlType), CStr(Me.StyleSelector.SelectedItem)), ControlType)
            Dim pc As CustomControl = _pdaControlCollection.SelectedItem
            If pc IsNot Nothing Then
                pc.ControlType = ct
            End If
            Me.TextBoxGroup.Visible = ct = ControlType.TextBox
            Me.NumberGroup.Visible = ct = ControlType.Number
            Me.ListGroup.Visible = ct = ControlType.DropDownList
            Me.CheckBoxGroup.Visible = ct = ControlType.CheckBox

            'Select Case ct
            '    Case ControlTypes.CheckBox
            '        Me.CheckBoxGroup.Visible = True
            '    Case ControlTypes.Date, ControlTypes.DateTime, ControlTypes.Time
            '    Case ControlTypes.List
            '        Me.ListGroup.Visible = True
            '    Case ControlTypes.Number
            '        Me.NumberGroup.Visible = True
            '    Case ControlTypes.TextBox
            '        Me.TextBoxGroup.Visible = True
            '    Case ControlTypes.Undefined
            'End Select
        Catch ex As System.ArgumentException
            ' do nothing
        End Try
    End Sub

    Private Sub LabelPositionRadio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelTopRadio.CheckedChanged, LabelRightRadio.CheckedChanged, LabelNoneRadio.CheckedChanged, LabelLeftRadio.CheckedChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        If Me.LabelLeftRadio.Checked Then
            If Me._selectedPdaControl.LabelPosition <> LabelPosition.Left Then
                Me._selectedPdaControl.LabelPosition = LabelPosition.Left
            End If
        ElseIf Me.LabelRightRadio.Checked Then
            If Me._selectedPdaControl.LabelPosition <> LabelPosition.Right Then
                Me._selectedPdaControl.LabelPosition = LabelPosition.Right
            End If
        ElseIf Me.LabelTopRadio.Checked Then
            If Me._selectedPdaControl.LabelPosition <> LabelPosition.Top Then
                Me._selectedPdaControl.LabelPosition = LabelPosition.Top
            End If
        Else
            If Me._selectedPdaControl.LabelPosition <> LabelPosition.None Then
                Me._selectedPdaControl.LabelPosition = LabelPosition.None
            End If
        End If
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub ControlPositionRadio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlRightRadio.CheckedChanged, ControlLeftRadio.CheckedChanged, ControlFullWidthRadio.CheckedChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        If Me.ControlLeftRadio.Checked Then
            Me._selectedPdaControl.ControlPosition = ControlPosition.Left
            Me._selectedPdaControl.Left = 0
        ElseIf Me.ControlRightRadio.Checked Then
            Me._selectedPdaControl.ControlPosition = ControlPosition.Right
            Me._selectedPdaControl.Left = Me.PdaForm.ClientSize.Width - Me._selectedPdaControl.ControlWidth
        ElseIf Me.ControlFullWidthRadio.Checked Then
            Me._selectedPdaControl.ControlPosition = ControlPosition.FullWidth
            Me.ControlWidthNumberBox.Value = 100
            Me._selectedPdaControl.ControlWidth = 100
            Me._selectedPdaControl.Left = 0
        End If
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub ControlWidthNumberBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles ControlWidthNumberBox.Validated
    End Sub

    'Private Sub setPdaControlWidth(ByVal pc As PdaControl, ByVal Width As Byte)
    '    'pc.Width = Width
    '    'If Width <> 100 Then
    '    '    pc.Width = ((240 * Width) \ 100) + 10 ' 10 = 'grabber' size
    '    '    If pc.ControlPosition = ControlPosition.FullWidth Then
    '    '        pc.ControlPosition = ControlPosition.Left
    '    '    ElseIf pc.ControlPosition = ControlPosition.Right Then
    '    '        pc.Left = 260 - pc.Width ' Me.PdaForm.ClientSize.Width - pc.Width
    '    '    End If
    '    'Else
    '    '    pc.Width = 250 ' 240 + 10 for the 'grabber'
    '    '    pc.Left = 0
    '    'End If
    '    Me._pdaControlCollection.ArrangeItems()
    'End Sub

    Private Sub ControlWidthNumberBox_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlWidthNumberBox.ValueChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        _selectedPdaControl.ControlWidth = Decimal.ToByte(Me.ControlWidthNumberBox.Value)
        If Me._selectedPdaControl.ControlWidth <> 100 AndAlso Me._selectedPdaControl.ControlPosition = ControlPosition.FullWidth Then
            Me.ControlLeftRadio.Checked = True
        End If
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub ListLimitToList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListLimitToList.CheckedChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        Me._selectedPdaControl.LimitToList = Me.ListLimitToList.Checked
    End Sub

    Private Sub MinValueNumberBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinValueNumberBox.Validated, MinValueNumberBox.ValueChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        Me._selectedPdaControl.MinimumValue = DirectCast(sender, NumericUpDown).Value
    End Sub

    Private Sub MaxPicker_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MaxValueNumberBox.ValueChanged, TextMaxLength.ValueChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        Me._selectedPdaControl.MaximumValue = DirectCast(sender, NumericUpDown).Value
    End Sub

    'Private Sub MaxValueNumberBox_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaxValueNumberBox.ValueChanged
    'If _setViewInProgress Then Return
    'If Me._selectedPdaControl Is Nothing Then Return
    'Me._selectedPdaControl.MaximumValue = Decimal.ToInt32(Me.MaxValueNumberBox.Value)
    'End Sub

    Private Sub DecimalPlacesNumberBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles DecimalPlacesNumberBox.ValueChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        Me._selectedPdaControl.DecimalPlaces = Decimal.ToByte(DirectCast(sender, NumericUpDown).Value)
    End Sub

    Private Sub LinesNumberBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinesNumberBox.Validated, LinesNumberBox.ValueChanged
        If _setViewInProgress Then Return
        If _loading Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        Dim newLines As Byte = Decimal.ToByte(Me.LinesNumberBox.Value)
        If Me._selectedPdaControl.ControlType = ControlType.TextBox Then ' lines is highjacked for other things too
            If newLines < 1 Then newLines = 1
        End If
        Me._selectedPdaControl.Lines = Decimal.ToByte(Me.LinesNumberBox.Value)
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub CheckBoxEnableTristate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEnableTristate.CheckedChanged
        If _setViewInProgress Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        Me._selectedPdaControl.Lines = CByte(Me.CheckBoxEnableTristate.Checked)
    End Sub

#End Region

    Private Sub EditListButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualListEditButton.Click
        If Me._selectedFormFieldRow Is Nothing Then Return
        Dim le As New ListEditor()

        For Each lf As ListFiller In ListFiller.GetList(Me._selectedFormFieldRow.ListData)
            le.ListEditorDataSet1.ListItems.AddListItemsRow(CStr(lf.Key), lf.DisplayValue)
        Next

        If le.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim newData As New StringBuilder(1000)
            For Each ledr As ListEditorDataSet.ListItemsRow In le.ListEditorDataSet1.ListItems
                newData.AppendFormat("{0};{1}", ledr.DataValue, ledr.DisplayValue)
                newData.AppendLine()
            Next
            Me._selectedFormFieldRow.ListData = newData.ToString()
            If _selectedPdaControl IsNot Nothing Then Me._selectedPdaControl.FillList()
        End If
    End Sub

    Public Sub RemoveItem(ByVal pc As CustomControl)
        If pc IsNot Nothing Then
            If Me._pdaControlCollection.Contains(pc) Then
                Me._pdaControlCollection.Remove(pc)
                If Me.PdaForm.Controls.Contains(pc) Then
                    Me.PdaForm.Controls.Remove(pc)
                End If

                For Each lvi As ListViewItem In Me.FieldChooser.Items
                    If lvi.SubItems(2).Text = pc.FieldName Then
                        'lvi.Tag = pc.DataColumn
                        lvi.ForeColor = SystemColors.WindowText
                        Exit For
                    End If
                Next
                pc.FormFieldRow.Delete()
                'Me._selectedPC = Nothing
                pc = Nothing
            End If
            Me._pdaControlCollection.ArrangeItems()
        End If
    End Sub

    Public Sub RemoveCurrentItem()
        RemoveItem(Me._selectedPdaControl)
    End Sub

    Private Sub ListSourceTableButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListSourceTableButton.Click
        'Dim el As Collections.Specialized.StringCollection = My.Settings.tablePickerExclusionList

        Dim tablePicker As New TablePicker(Me.DbCatalog.CandidateEntity)
        tablePicker.MultiSelect = False
        tablePicker.ShowDialog()
        Dim dr As CandidateEntityRow = tablePicker.SelectedTable
        If dr IsNot Nothing Then
            Me.ListSourceTableTextBox.Text = dr.EntityName
            If Me._selectedPdaControl IsNot Nothing Then Me._selectedPdaControl.ListDataSource = dr.EntityName
        End If
    End Sub

    Private Sub ListSourceDataColumnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListSourceDataColumnButton.Click
        If Me._selectedPdaControl Is Nothing Then Return
        If String.IsNullOrEmpty(Me._selectedPdaControl.ListDataSource) Then
            MessageBox.Show("Please specify a table before selecting a data field", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            Dim listSourceTable As CandidateEntityRow = _
                Me._dbCatalog.CandidateEntity.FindBySchemaNameEntityName("dbo", Me.ListSourceTableTextBox.Text)
            Dim columnList() As CandidateEntityAttributeRow = _
                listSourceTable.GetCandidateEntityAttributeRows()

            Dim columnPicker As New ColumnPicker(columnList)
            If columnPicker.ShowDialog = DialogResult.OK Then
                Dim selectedColumn As CandidateEntityAttributeRow = columnPicker.SelectedColumn
                If selectedColumn IsNot Nothing Then
                    Me.ListSourceDataColumnTextBox.Text = selectedColumn.AttributeName
                    Me._selectedPdaControl.ListValueColumn = selectedColumn.AttributeName
                    Me._selectedPdaControl.FillList()
                End If
            End If
        End If
    End Sub

    Private Sub ListSourceDisplayColumnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListSourceDisplayColumnButton.Click
        If String.IsNullOrEmpty(Me._selectedPdaControl.ListDataSource) Then
            MessageBox.Show("Please specify a table before selecting a display field", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            Dim listSourceTable As CandidateEntityRow = _
                Me._dbCatalog.CandidateEntity.FindBySchemaNameEntityName("dbo", Me.ListSourceTableTextBox.Text)
            Dim columnList() As CandidateEntityAttributeRow = _
                listSourceTable.GetCandidateEntityAttributeRows()

            Dim columnPicker As New ColumnPicker(columnList)
            If columnPicker.ShowDialog = DialogResult.OK Then
                Dim selectedColumn As CandidateEntityAttributeRow = columnPicker.SelectedColumn
                If selectedColumn IsNot Nothing Then
                    Me.ListSourceDisplayColumnTextBox.Text = selectedColumn.AttributeName
                    Me._selectedPdaControl.ListDisplayColumn = selectedColumn.AttributeName
                    Me._selectedPdaControl.FillList()
                End If
            End If
        End If

    End Sub

    Private Sub ManualListCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualListCheckBox.CheckedChanged
        If _setViewInProgress Then Return
        Dim manualList As Boolean = ManualListCheckBox.Checked
        Me.ManualListEditButton.Enabled = manualList

        Me.ListSourceTableTextBox.Enabled = Not manualList
        Me.ListSourceDataColumnTextBox.Enabled = Not manualList
        Me.ListSourceDisplayColumnTextBox.Enabled = Not manualList
        Me.ListSourceTableButton.Enabled = Not manualList
        Me.ListSourceDataColumnButton.Enabled = Not manualList
        Me.ListSourceDisplayColumnButton.Enabled = Not manualList
    End Sub

    Private Sub PdaFormGroup_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles PdaFormGroup.Resize
        Debug.Print(CStr(PdaFormGroup.Width))
        If PdaFormGroup.Width > 270 Then
            PdaFormGroup.Width = 270
            ' Stop
        End If
    End Sub

    Private Sub SortPriorityNumberBox_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortPriorityNumberBox.ValueChanged
        If _setViewInProgress Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        Me._selectedPdaControl.SortPriority = Decimal.ToByte(Me.SortPriorityNumberBox.Value)
    End Sub

    Private Sub FormPropertiesGroup_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormPropertiesGroup.Enter

    End Sub

    Private Sub FormPrioritySelector_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormPrioritySelector.Validated
        Me.CustomForm.Priority = CByte(Me.FormPrioritySelector.Value)
    End Sub

    Private Sub FormCopyLimit_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormCopyLimit.Validated
        Me.CustomForm.MaxItems = CByte(Me.FormCopyLimit.Value)
    End Sub

    Private Sub FormNameTextBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormNameTextBox.Validated
        Me.CustomForm.FormName = Me.FormNameTextBox.Text

    End Sub

    Private Sub ReadOnlyCheckBox_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If _setViewInProgress Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        'TODO: _selectedPdaControl.LockCode = If(Me.ReadOnlyControlCheckBox.Checked, CByte(1), CByte(0))
    End Sub

    Private Sub ReadOnlyFormCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReadOnlyFormCheckBox.CheckedChanged
        'TODO: Me._customForm.LockCode = If(Me.ReadOnlyFormCheckBox.Checked, CByte(1), CByte(0))
    End Sub

    Private Sub LockWithParentCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me._customForm IsNot Nothing Then
            'TODO: Me._customForm.LockCode = If(Me.ReadOnlyFormCheckBox.Checked, CByte(1), CByte(0))
        End If
    End Sub

    Private Sub LockControlWithFormCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If _setViewInProgress Then Return
        If Me._selectedPdaControl Is Nothing Then Return
        'TODO: _selectedPdaControl.FormFieldRow.LockWithParent = Me.LockControlWithFormCheckBox.Checked
    End Sub

    'Private Sub TextMaxLength_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextMaxLength.Validated
    '    If _setViewInProgress Then Return
    '    If Me._selectedPdaControl Is Nothing Then Return
    '    _selectedPdaControl.MaximumValue = CInt(Me.TextMaxLength.Value)
    'End Sub

    Private Sub FormPrioritySelector_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormPrioritySelector.ValueChanged
        Me._customForm.Priority = CByte(Me.FormPrioritySelector.Value)
    End Sub

    Private Sub FormCopyLimit_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormCopyLimit.ValueChanged
        Me._customForm.MaxItems = CByte(Me.FormCopyLimit.Value)
    End Sub

    Private Sub TableName_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TableName.Validated
        Me._customForm.EntityName = Me.TableName.Text
    End Sub

    Private Sub CustomTablePK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomTablePK.Validated
        Me._customForm.EntityPK = Me.CustomTablePK.Text
    End Sub

    Private Sub CustomTableFK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomTableFK.Validated
        Me._customForm.EntityParentFK = Me.CustomTableFK.Text
    End Sub

    Private Sub ParentTable_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ParentTable.Validated
        Me._customForm.ParentEntityName = Me.ParentTable.Text
    End Sub

    Private Sub ParentPK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ParentPK.Validated
        Me._customForm.ParentPK = Me.ParentPK.Text
    End Sub

    'Private Sub HideControlCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.HideControlCheckBox.Checked Then
    '        Do While Me._selectedPdaControl.Node.Next IsNot Nothing AndAlso Me._selectedPdaControl.Node.Next.Value.DisplayOrder <> 0
    '            Me._pdaControlCollection.MoveDown(Me._selectedPdaControl)
    '        Loop
    '        Me._selectedPdaControl.DisplayOrder = 0
    '        Me._pdaControlCollection.ArrangeItems()
    '    Else
    '        Do While Me._selectedPdaControl.Node.Previous IsNot Nothing AndAlso Me._selectedPdaControl.Node.Previous.Value.DisplayOrder = 0
    '            Me._pdaControlCollection.MoveUp(Me._selectedPdaControl)
    '        Loop
    '        Me._selectedPdaControl.DisplayOrder = Me._pdaControlCollection.TopSequenceNumber + CByte(1)
    '        Me._pdaControlCollection.ArrangeItems()
    '    End If
    'End Sub

    Private Sub LockCodeSelector_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles LockCodeSelector.SelectionChangeCommitted
        Dim newCode As LockCode = CType([Enum].Parse(GetType(LockCode), CStr(LockCodeSelector.SelectedItem)), LockCode)
        Select Case newCode
            Case LockCode.LockWithParent
                Me._selectedPdaControl.LockCode = newCode
                Me._selectedFormFieldRow.LockCode = CByte(newCode)
            Case LockCode.Locked
                Me._selectedPdaControl.LockCode = newCode
                Me._selectedFormFieldRow.LockCode = CByte(newCode)
            Case LockCode.None
                Me._selectedPdaControl.LockCode = newCode
                Me._selectedFormFieldRow.LockCode = CByte(newCode)
            Case LockCode.Hidden
                Me._selectedPdaControl.DisplayOrder = 0
                Me._selectedFormFieldRow.DisplayOrder = 0
                Me._selectedPdaControl.LockCode = newCode
                Me._selectedFormFieldRow.LockCode = CByte(newCode)
            Case LockCode.LockedWhenSynchronised
                Me._selectedPdaControl.LockCode = newCode
                Me._selectedFormFieldRow.LockCode = CByte(newCode)
            Case LockCode.LockedWhenFlagged
                Me._selectedPdaControl.LockCode = newCode
                Me._selectedFormFieldRow.LockCode = CByte(newCode)

        End Select
    End Sub

    Private Sub TableName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TableName.TextChanged

    End Sub

    Private Sub FormLabelTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormLabelTextBox.TextChanged
        Me._customForm.FormLabel = Me.FormLabelTextBox.Text
        Dim tp As TabPage = TryCast(Me.Parent, TabPage)
        If tp IsNot Nothing Then
            tp.Text = String.Format("{0}.{1}.{2}", _customForm.ParentEntityName, _customForm.EntityName, _customForm.FormLabel)
        End If
        'RaiseEvent LabelChanged(Me, e)
    End Sub
End Class
