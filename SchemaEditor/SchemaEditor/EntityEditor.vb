Imports System.Text

Public Class EntityEditor
    Private _pdaDataSet As CustomFormDefinition
    Private _datatable As DataTable
    Private _customForm As CustomFormDefinition.CustomFormRow
    Private _selectedPdaControl As CustomControl
    Private _selectedCustomControlRow As CustomFormDefinition.CustomControlRow
    Private _dbCatalog As DbCatalog
    Private _items As Integer = 0

    Private WithEvents _pdaControlCollection As PdaControlCollection

    Public Sub New(ByVal cf As CustomFormDefinition.CustomFormRow, ByVal t As DataTable)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _pdaControlCollection = New PdaControlCollection

        'Me.SuspendLayout()
        Me._customForm = cf
        Me.FormNameTextBox.Text = cf.CustomFormName
        Me.TableName.Text = t.TableName
        Me.CustomTablePK.Text = cf.PrimaryKeyColumnName
        Me.CustomTableFK.Text = cf.ForeignKeyColumnName
        Me.ParentTable.Text = cf.ParentTableName
        Me.ParentPK.Text = cf.ParentPrimaryKeyColumnName
        Me.FormCopyLimit.Value = cf.OneToMany
        Me.FormPrioritySelector.Value = cf.Priority
        Me.LockFormWithParentCheckBox.Checked = cf.LockWithParent
        Me.ReadOnlyFormCheckBox.Checked = cf.IsReadOnly

        Me._datatable = t
        Me._pdaDataSet = CType(cf.Table.DataSet, CustomFormDefinition)
        Me.StyleSelector.Items.AddRange(GetEnumDescriptions(GetType(ControlType)))

        addColumnsToListView(t)

        Dim ccrs() As CustomFormDefinition.CustomControlRow = cf.GetCustomControlRows
        Dim s(ccrs.Length - 1) As Integer
        Dim cc As CustomFormDefinition.CustomControlRow
        For i As Integer = 0 To ccrs.Length - 1
            cc = ccrs(i)
            s(i) = IIf(cc.Sequence = 0, 255, cc.Sequence)
        Next
        Array.Sort(s, ccrs)
        For Each cc In ccrs
            If _items < 200 Then
                Dim pc As CustomControl = addItem(cc)
                Debug.Print(pc.Size.ToString)
            Else
                Exit For
            End If
        Next
        Me.ResumeLayout()
        Me._pdaControlCollection.ArrangeItems()
        Me.PdaForm.Refresh()
        Me.PdaFormGroup.Refresh()
        Me.Refresh()
    End Sub

    Public Property DbCatalog() As DbCatalog
        Get
            Return _dbCatalog
        End Get
        Set(ByVal value As DbCatalog)
            _dbCatalog = value
        End Set
    End Property

    Public ReadOnly Property CustomForm() As CustomFormDefinition.CustomFormRow
        Get
            Return _customForm
        End Get
    End Property

#Region "Add Control"
    Private Function addItem(ByVal c As CustomFormDefinition.CustomControlRow) As CustomControl
        If _items > 200 Then
            MessageBox.Show("Can't add more than 200 controls to a form.", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Return Nothing
        End If

        If Me.PdaForm.Controls.ContainsKey(c.FieldName) Then
            MessageBox.Show(My.Resources.CustomFieldAlreadyPresent, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Return Nothing
        End If

        Dim uc As New CustomControl(c)
        uc.PdaControlCollection = _pdaControlCollection
        Me.PdaForm.Controls.Add(uc)

        'For Each lvi As ListViewItem In Me.FieldChooser.Items
        '    If lvi.SubItems(2).Text = uc.FieldName Then
        '        lvi.ForeColor = SystemColors.GrayText
        '        lvi.Text = uc.Label
        '    End If
        'Next

        If c.CustomControlTypeID = ControlType.DropDownList Then
            uc.ListDataSource = c.ListDataSource
            uc.ListDisplayColumn = c.ListDisplayColumn
            uc.ListValueColumn = c.ListValueColumn
            uc.FillList()
            'Dim ccds() As CustomFormDefinition.CustomControlDataRow = c.GetCustomControlDataRows
            'If ccds IsNot Nothing AndAlso ccds.Length <> 0 Then
            '    uc.ComboBox1.DataSource = ccds
            '    uc.ComboBox1.ValueMember = "DataValue"
            '    uc.ComboBox1.DisplayMember = "DisplayValue"
            'End If
        End If

        _items += 1

        Return uc

    End Function

    Private Function addItem(ByVal c As DataColumn) As CustomControl
        If Me.PdaForm.Controls.ContainsKey(c.ColumnName) Then
            If MessageBox.Show(My.Resources.CustomFieldAlreadyPresent, My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Return Nothing
            End If
            'MessageBox.Show(My.Resources.CustomFieldAlreadyPresent, My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        End If

        Dim ccr As CustomFormDefinition.CustomControlRow = Me._pdaDataSet.CustomControl.NewCustomControlRow()
        Dim ct As ControlType
        ccr.FieldName = c.ColumnName
        ccr.Label = ExpandName(c.ColumnName)
        ccr.LabelWidthPercent = 50
        'ccr.CreatedDateTime = Now
        'ccr.ModifiedDateTime = Now
        ccr.CustomControlUID = Guid.NewGuid
        ccr.CustomFormRow = Me._customForm
        ccr.Sequence = CByte(_pdaControlCollection.TopSequenceNumber + 1)

        Select Case c.DataType.Name
            Case TypeCode.String.ToString
                ct = ControlType.TextBox
                ccr.MaximumValue = c.MaxLength
                ccr.Lines = IIf(c.MaxLength > 40, CByte(3), CByte(1))
            Case TypeCode.Boolean.ToString
                ct = ControlType.CheckBox
                ccr.LabelWidthPercent = 80
            Case TypeCode.Int32.ToString, TypeCode.Byte.ToString, TypeCode.Int16.ToString, TypeCode.SByte.ToString, TypeCode.Int64.ToString, TypeCode.UInt16.ToString, TypeCode.UInt32.ToString, TypeCode.UInt64.ToString
                If c.ColumnName.EndsWith("ID") Then ' assume lookup
                    ct = ControlType.DropDownList
                    ccr.DecimalPlaces = 0
                Else
                    ct = ControlType.Number
                    ccr.DecimalPlaces = 0
                End If
            Case TypeCode.Double.ToString, TypeCode.Single.ToString, TypeCode.Decimal.ToString
                ct = ControlType.Number
                ccr.DecimalPlaces = 2

            Case TypeCode.DateTime.ToString
                If (c.ColumnName.IndexOf("Date") <> -1 AndAlso c.ColumnName.IndexOf("Time") <> -1) OrElse (c.ColumnName.IndexOf("Timestamp") <> -1) Then
                    ct = ControlType.DateTime
                ElseIf c.ColumnName.IndexOf("Date") <> -1 Then
                    ct = ControlType.Date
                ElseIf c.ColumnName.IndexOf("Time") <> -1 Then
                    ct = ControlType.Time
                Else : ct = ControlType.DateTime
                End If
            Case "Guid"
                ct = ControlType.DropDownList
        End Select

        ' ccr.CustomControlTypeRow = Me._pdaDataSet.CustomControlType.FindByCustomControlTypeID(ct)
        Me._pdaDataSet.CustomControl.AddCustomControlRow(ccr)
        Return addItem(ccr)
    End Function
#End Region

#Region "Fill Column List"
    Private Sub addColumnsToListView(ByVal t As DataTable)
        Dim lvg As New ListViewGroup(t.TableName, t.TableName)

        For Each c As DataColumn In t.Columns
            Dim style As String = ""
            Select Case c.DataType.Name
                Case "Guid"
                    If c.ColumnName = _customForm.ForeignKeyColumnName OrElse c.ColumnName = _customForm.PrimaryKeyColumnName Then
                        style = "ID"
                    Else
                        style = "ComboBox"
                    End If
                Case TypeCode.String.ToString : style = "TextBox"
                Case TypeCode.Boolean.ToString : style = "CheckBox"
                Case TypeCode.Int32.ToString, TypeCode.Byte.ToString, TypeCode.Int16.ToString, TypeCode.SByte.ToString, TypeCode.Int64.ToString, TypeCode.UInt16.ToString, TypeCode.UInt32.ToString, TypeCode.UInt64.ToString
                    If c.ColumnName.EndsWith("ID") Then ' assume lookup
                        style = "ComboBox"
                    Else
                        style = "Number"
                    End If
                Case TypeCode.Double.ToString, TypeCode.Single.ToString, TypeCode.Decimal.ToString
                    style = "Number"
                Case "Date", "DateTime", "Time"
                    If (c.ColumnName.StartsWith("Created") OrElse c.ColumnName.StartsWith("Modified")) Then
                        style = "AuditDate"
                    Else
                        style = "Date"
                    End If
                Case Else
                    Debug.WriteLine(c.DataType.Name)
                    style = ""
            End Select
            If style <> "" Then
                Dim lvi As New ListViewItem(ExpandName(c.ColumnName))
                lvi.SubItems.Add(style)
                lvi.SubItems.Add(c.ColumnName)
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

        Dim s As Byte = control1.Sequence
        control1.Sequence = control2.Sequence
        control2.Sequence = s
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
        If lvi IsNot Nothing Then
            Dim c As DataColumn = TryCast(lvi.Tag, DataColumn)
            If c IsNot Nothing Then
                If c.Table.TableName = Me._customForm.TableName Then
                    Dim newControl As CustomControl = addItem(c)
                    Me._pdaControlCollection.ArrangeItems()
                    newControl.Select()
                    'Me._selectedPdaControl = newControl
                End If
            End If
        End If
    End Sub

    Private Sub PdaForm_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PdaForm.DragEnter
        Dim lvi As ListViewItem = TryCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem)
        If lvi IsNot Nothing Then
            Dim c As DataColumn = TryCast(lvi.Tag, DataColumn)
            If c IsNot Nothing Then
                If c.Table.TableName = Me._customForm.TableName Then
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
                If pc.CustomControlRow.IsCreatedDateTimeNull Then ' never saved
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
        Dim cc As CustomFormDefinition.CustomControlRow = pc.CustomControlRow
        Dim cf As CustomFormDefinition.CustomFormRow = Me._customForm

        Me.CurrentFieldLabel.Text = cf.TableName & "." & cc.FieldName
        Me.ReadOnlyControlCheckBox.Checked = cc.IsReadOnly
        Me.StyleSelector.SelectedItem = pc.ControlType.ToString()

        Select Case pc.LabelPosition
            Case LabelPositions.Left
                Me.LabelLeftRadio.Checked = True
            Case LabelPositions.None
                Me.LabelNoneRadio.Checked = True
            Case LabelPositions.Right
                Me.LabelRightRadio.Checked = True
            Case LabelPositions.Top
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

        Me.ControlWidthNumberBox.Value = cc.WidthPercent

        If pc.ControlType = ControlType.TextBox Then
            Me.LinesNumberBox.Value = pc.Lines
            Me.LinesNumberBox.Minimum = 1
            Me.LinesNumberBox.Maximum = 10
        ElseIf pc.ControlType = ControlType.DropDownList Then
            Me.ListLimitToList.Checked = pc.CustomControlRow.Lines <> 0
            Me.LinesNumberBox.Minimum = 0
            Me.LinesNumberBox.Maximum = 255
            Me.LinesNumberBox.Value = pc.CustomControlRow.Lines
        Else
            Me.LinesNumberBox.Minimum = 0
            Me.LinesNumberBox.Maximum = 255
            Me.LinesNumberBox.Value = pc.CustomControlRow.Lines
        End If

        Me.MaxValueNumberBox.Value = pc.MaximumValue
        Me.TextMaxLength.Value = pc.MaximumValue
        Me.MinValueNumberBox.Value = pc.MinimumValue
        Me.DecimalPlacesNumberBox.Value = pc.DecimalPlaces

        Me.ListSourceTableTextBox.Text = cc.ListDataSource
        Me.ListSourceDataColumnTextBox.Text = cc.ListValueColumn
        Me.ListSourceDisplayColumnTextBox.Text = cc.ListDisplayColumn

        Dim manualList As Boolean = String.IsNullOrEmpty(cc.ListDataSource)

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
                If pc.Sequence = 0 Then
                    pc.BackColor = Color.Brown
                Else
                    pc.BackColor = SystemColors.Control
                End If
            Else
                pc.BackColor = SystemColors.Info
            End If
        Next
        If _selectedPdaControl IsNot Nothing Then
            Me._selectedCustomControlRow = Me._selectedPdaControl.CustomControlRow
            Dim ct As ControlType = CType(Me._selectedCustomControlRow.CustomControlTypeID, ControlType) 'CType(System.Enum.Parse(GetType(ControlTypes), CStr(Me.StyleSelector.SelectedItem)), ControlTypes)

            Me.TextBoxGroup.Visible = ct = ControlType.TextBox
            Me.NumberGroup.Visible = ct = ControlType.Number
            Me.ListGroup.Visible = ct = ControlType.DropDownList
            Me.CheckBoxGroup.Visible = ct = ControlType.CheckBox

            'Me.TextBoxGroup.Visible = False
            'Me.NumberGroup.Visible = False
            'Me.ListGroup.Visible = False
            'Me.CheckBoxGroup.Visible = False

            'Select Case Me._selectedCustomControlRow.CustomControlTypeID
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
        End If
        SetView()
    End Sub
#End Region


#Region "Custom Control Basic Properties"
    Private Sub StyleSelector_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StyleSelector.SelectedValueChanged
        If _setViewInProgress Then Exit Sub
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
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        If Me.LabelLeftRadio.Checked Then
            If Me._selectedPdaControl.LabelPosition <> LabelPositions.Left Then
                Me._selectedPdaControl.LabelPosition = LabelPositions.Left
            End If
        ElseIf Me.LabelRightRadio.Checked Then
            If Me._selectedPdaControl.LabelPosition <> LabelPositions.Right Then
                Me._selectedPdaControl.LabelPosition = LabelPositions.Right
            End If
        ElseIf Me.LabelTopRadio.Checked Then
            If Me._selectedPdaControl.LabelPosition <> LabelPositions.Top Then
                Me._selectedPdaControl.LabelPosition = LabelPositions.Top
            End If
        Else
            If Me._selectedPdaControl.LabelPosition <> LabelPositions.None Then
                Me._selectedPdaControl.LabelPosition = LabelPositions.None
            End If
        End If
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub ControlPositionRadio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlRightRadio.CheckedChanged, ControlLeftRadio.CheckedChanged, ControlFullWidthRadio.CheckedChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        If Me.ControlLeftRadio.Checked Then
            Me._selectedPdaControl.ControlPosition = ControlPosition.Left
            Me._selectedPdaControl.Left = 0
        ElseIf Me.ControlRightRadio.Checked Then
            Me._selectedPdaControl.ControlPosition = ControlPosition.Right
            Me._selectedPdaControl.Left = Me.PdaForm.ClientSize.Width - Me._selectedPdaControl.Width
        ElseIf Me.ControlFullWidthRadio.Checked Then
            Me._selectedPdaControl.ControlPosition = ControlPosition.FullWidth
            Me.ControlWidthNumberBox.Value = 100
            Me._selectedPdaControl.WidthPercent = 100
            Me._selectedPdaControl.Left = 0
        End If
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub ControlWidthNumberBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles ControlWidthNumberBox.Validated
    End Sub

    'Private Sub setPdaControlWidth(ByVal pc As PdaControl, ByVal widthPercent As Byte)
    '    'pc.WidthPercent = widthPercent
    '    'If widthPercent <> 100 Then
    '    '    pc.Width = ((240 * widthPercent) \ 100) + 10 ' 10 = 'grabber' size
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
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        _selectedPdaControl.WidthPercent = Decimal.ToByte(Me.ControlWidthNumberBox.Value)
        If Me._selectedPdaControl.WidthPercent <> 100 AndAlso Me._selectedPdaControl.ControlPosition = ControlPosition.FullWidth Then
            Me.ControlLeftRadio.Checked = True
        End If
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub ListLimitToList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListLimitToList.CheckedChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        Me._selectedPdaControl.LimitToList = Me.ListLimitToList.Checked
    End Sub

    Private Sub MinValueNumberBox_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinValueNumberBox.ValueChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        Me._selectedPdaControl.MinimumValue = Decimal.ToInt32(Me.MinValueNumberBox.Value)
    End Sub

    Private Sub MaxValueNumberBox_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaxValueNumberBox.ValueChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        Me._selectedPdaControl.MaximumValue = Decimal.ToInt32(Me.MaxValueNumberBox.Value)
    End Sub

    Private Sub DecimalPlacesNumberBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DecimalPlacesNumberBox.ValueChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        Me._selectedPdaControl.DecimalPlaces = Decimal.ToByte(Me.DecimalPlacesNumberBox.Value)
    End Sub

    Private Sub LinesNumberBox_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinesNumberBox.ValueChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        Dim newLines As Byte = Decimal.ToByte(Me.LinesNumberBox.Value)
        If Me._selectedPdaControl.ControlType = ControlType.TextBox Then ' lines is highjacked for other things too
            If newLines < 1 Then newLines = 1
        End If
        Me._selectedPdaControl.Lines = Decimal.ToByte(Me.LinesNumberBox.Value)
        Me._pdaControlCollection.ArrangeItems()
    End Sub

    Private Sub CheckBoxEnableTristate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEnableTristate.CheckedChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        Me._selectedPdaControl.Lines = CByte(Me.CheckBoxEnableTristate.Checked)
    End Sub

#End Region

    Private Sub EditListButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualListEditButton.Click
        If Me._selectedCustomControlRow Is Nothing Then Return
        Dim le As New ListEditor()

        For Each lf As ListFiller In ListFiller.GetList(Me._selectedCustomControlRow.ListData)
            le.ListEditorDataSet1.ListItems.AddListItemsRow(CStr(lf.Key), lf.DisplayValue)
        Next

        If le.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim newData As New Stringbuilder(1000)
            For Each ledr As ListEditorDataSet.ListItemsRow In le.ListEditorDataSet1.ListItems
                newData.AppendFormat("{0};{1}", ledr.DataValue, ledr.DisplayValue)
                newData.AppendLine()
            Next
            Me._selectedCustomControlRow.ListData = newData.ToString()
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
                pc.CustomControlRow.Delete()
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
        Dim el As Collections.Specialized.StringCollection = My.Settings.tablePickerExclusionList

        Dim tablePicker As New TablePicker(Me.DbCatalog.Table, My.Settings.tablePickerLookupExclusionList, "")
        tablePicker.MultiSelect = False
        tablePicker.ShowDialog()
        Dim dr As DbCatalog.TableRow = tablePicker.SelectedTable
        If dr IsNot Nothing Then
            Me.ListSourceTableTextBox.Text = dr.ShortTableName
            If Me._selectedPdaControl IsNot Nothing Then Me._selectedPdaControl.ListDataSource = dr.ShortTableName
        End If
    End Sub

    Private Sub ListSourceDataColumnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListSourceDataColumnButton.Click
        If Me._selectedPdaControl Is Nothing Then Return
        If String.IsNullOrEmpty(Me._selectedPdaControl.ListDataSource) Then
            MessageBox.Show("Please specify a table before selecting a data field", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            Dim columnList() As DbCatalog.ColumnRow = CType(Me._dbCatalog.Column.Select(String.Format("ShortTableName='{0}'", Me._selectedPdaControl.ListDataSource)), CustomFormDesigner.DbCatalog.ColumnRow())

            Dim columnPicker As New ColumnPicker(columnList)
            If columnPicker.ShowDialog = DialogResult.OK Then
                Dim selectedColumn As DbCatalog.ColumnRow = columnPicker.SelectedColumn
                If selectedColumn IsNot Nothing Then
                    Me.ListSourceDataColumnTextBox.Text = selectedColumn.ColumnName
                    Me._selectedPdaControl.ListValueColumn = selectedColumn.ColumnName
                    Me._selectedPdaControl.FillList()
                End If
            End If
        End If
    End Sub

    Private Sub ListSourceDisplayColumnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListSourceDisplayColumnButton.Click
        If String.IsNullOrEmpty(Me._selectedPdaControl.ListDataSource) Then
            MessageBox.Show("Please specify a table before selecting a display field", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            Dim columnList() As DbCatalog.ColumnRow = CType(Me._dbCatalog.Column.Select(String.Format("ShortTableName='{0}'", Me._selectedPdaControl.ListDataSource)), CustomFormDesigner.DbCatalog.ColumnRow())

            Dim columnPicker As New ColumnPicker(columnList)
            If columnPicker.ShowDialog = DialogResult.OK Then
                Dim selectedColumn As DbCatalog.ColumnRow = columnPicker.SelectedColumn
                If selectedColumn IsNot Nothing Then
                    Me.ListSourceDisplayColumnTextBox.Text = selectedColumn.ColumnName
                    Me._selectedPdaControl.ListDisplayColumn = selectedColumn.ColumnName
                    Me._selectedPdaControl.FillList()
                End If
            End If
            'End If
        End If
    End Sub

    Private Sub ManualListCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualListCheckBox.CheckedChanged
        If _setViewInProgress Then Exit Sub
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
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        Me._selectedPdaControl.SortPriority = Decimal.ToByte(Me.SortPriorityNumberBox.Value)
    End Sub

    Private Sub FormPropertiesGroup_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormPropertiesGroup.Enter

    End Sub

    Private Sub FormPrioritySelector_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormPrioritySelector.Validated
        Me.CustomForm.Priority = CByte(Me.FormPrioritySelector.Value)
    End Sub

    Private Sub FormCopyLimit_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormCopyLimit.Validated
        Me.CustomForm.OneToMany = CByte(Me.FormCopyLimit.Value)
    End Sub

    Private Sub FormNameTextBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormNameTextBox.Validated
        Me.CustomForm.CustomFormName = Me.FormNameTextBox.Text
        Dim tp As TabPage = TryCast(Me.Parent, TabPage)
        If tp IsNot Nothing Then
            tp.Text = String.Format("{0}.{1}.{2}", _customForm.ParentTableName, _customForm.TableName, _customForm.CustomFormName)
        End If
    End Sub

    Private Sub ReadOnlyCheckBox_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReadOnlyControlCheckBox.CheckStateChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        _selectedPdaControl.IsReadOnly = Me.ReadOnlyControlCheckBox.Checked
    End Sub

    Private Sub ReadOnlyFormCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReadOnlyFormCheckBox.CheckedChanged
        Me._customForm.IsReadOnly = Me.ReadOnlyFormCheckBox.Checked
    End Sub

    Private Sub LockWithParentCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LockFormWithParentCheckBox.CheckedChanged
        If Me._customForm IsNot Nothing Then
            Me._customForm.LockWithParent = Me.LockFormWithParentCheckBox.Checked
        End If
    End Sub

    Private Sub LockControlWithFormCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LockControlWithFormCheckBox.CheckedChanged
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        _selectedPdaControl.CustomControlRow.LockWithParent = Me.LockControlWithFormCheckBox.Checked
    End Sub

    Private Sub TextMaxLength_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextMaxLength.Validated
        If _setViewInProgress Then Exit Sub
        If Me._selectedPdaControl Is Nothing Then Exit Sub
        _selectedPdaControl.MaximumValue = CInt(Me.TextMaxLength.Value)
    End Sub

    Private Sub FormPrioritySelector_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormPrioritySelector.ValueChanged
        Me._customForm.Priority = CByte(Me.FormPrioritySelector.Value)
    End Sub

    Private Sub FormCopyLimit_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormCopyLimit.ValueChanged
        Me._customForm.OneToMany = CByte(Me.FormCopyLimit.Value)
    End Sub

    Private Sub TableName_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TableName.Validated
        Me._customForm.TableName = Me.TableName.Text
    End Sub

    Private Sub CustomTablePK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomTablePK.Validated
        Me._customForm.PrimaryKeyColumnName = Me.CustomTablePK.Text
    End Sub

    Private Sub CustomTableFK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomTableFK.Validated
        Me._customForm.ForeignKeyColumnName = Me.CustomTableFK.Text
    End Sub

    Private Sub ParentTable_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ParentTable.Validated
        Me._customForm.ParentTableName = Me.ParentTable.Text
    End Sub

    Private Sub ParentPK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ParentPK.Validated
        Me._customForm.ParentPrimaryKeyColumnName = Me.ParentPK.Text
    End Sub

    Private Sub HideControlCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideControlCheckBox.CheckedChanged
        If Me.HideControlCheckBox.Checked Then
            Do While Me._selectedPdaControl.Node.Next IsNot Nothing AndAlso Me._selectedPdaControl.Node.Next.Value.Sequence <> 0
                Me._pdaControlCollection.MoveDown(Me._selectedPdaControl)
            Loop
            Me._selectedPdaControl.Sequence = 0
            Me._pdaControlCollection.ArrangeItems()
        Else
            Do While Me._selectedPdaControl.Node.Previous IsNot Nothing AndAlso Me._selectedPdaControl.Node.Previous.Value.Sequence = 0
                Me._pdaControlCollection.MoveUp(Me._selectedPdaControl)
            Loop
            Me._selectedPdaControl.Sequence = Me._pdaControlCollection.TopSequenceNumber + CByte(1)
            Me._pdaControlCollection.ArrangeItems()
        End If
    End Sub
End Class
