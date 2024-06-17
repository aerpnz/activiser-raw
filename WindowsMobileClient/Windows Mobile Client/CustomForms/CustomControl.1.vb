Imports System.ComponentModel
Imports activiser.Library

'Imports activiser.NativeMethods.SystemInformation

Partial Public Class CustomControl
    Private _controlType As ControlType
    Private _fieldName As String
    Private _labelPosition As LabelPosition = LabelPosition.Left
    Private _lines As Byte = 1
    Private _customControlRow As WebService.FormDefinition.FormFieldRow
    Private _controlPosition As ControlPosition = ControlPosition.FullWidth
    Private _widthPercent As Byte = 100
    Private _labelWidthPercent As Byte = 50
    Private _customForm As WebService.FormDefinition.FormRow
    Private _dataType As Type
    Private _dirty As Boolean
    Private _nullable As Boolean
    Private _font As Font
    Private _textFont As Font

    'Private _initialised As Boolean
    'Private _table As DataTable

    ' HACK, always have a ComboBox, to provide default sizing.
    Private dummyCombo As New ComboBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListControl1 As System.Windows.Forms.ListControl
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents NumericUpDown1 As activiser.Library.Forms.NumberPicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker

    Private WithEvents EditContextMenu As New EditContextMenu

    Public Event ValueChanged As EventHandler

#Region "Control additions"

    Private Sub addCheckBox(ByVal c As WebService.FormDefinition.FormFieldRow)
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox1.Font = _font
        Me.CheckBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBox1.Height = Me.dummyCombo.Height
        Me.CheckBox1.Name = c.AttributeName & "CheckBox"
        Me.CheckBox1.Enabled = c.LockCode <> LockCodes.Locked
        Me.CheckBox1.ThreeState = c.Lines > 1
        If Me.CheckBox1.ThreeState Then ' default to null for nullable fields.
            Me.CheckBox1.CheckState = CheckState.Indeterminate
        Else
            Me.CheckBox1.Checked = False
        End If
        If c.Position(1) = "R" Then 'right hand label; default CheckBox behavior, so hide caption and splitter
            Me.Caption.Visible = False
            Me.Splitter1.Visible = False
            Me.CheckBox1.Text = c.Label.Trim
        End If
        Me.Controls.Add(Me.CheckBox1)
        AddHandler CheckBox1.KeyDown, AddressOf Control_KeyDown
        AddHandler CheckBox1.CheckStateChanged, AddressOf MakeDirty
        Me.CheckBox1.BringToFront()
    End Sub

    Private Sub addListBox(ByVal c As WebService.FormDefinition.FormFieldRow)

        If (c.LockCode = LockCodes.Locked) Then ' load the 'readonly' list box; we'll only be showing one line.
            Me.ListBox1 = New ListBox()
            Me.ListControl1 = Me.ListBox1
            ' hack to make listbox same height as combobox.
            Me.ListControl1.Font = New Font(_font.Name, _font.Size + 2, _font.Style)
            Me.ListControl1.Height = Me.dummyCombo.Height
            Me.ListControl1.Dock = System.Windows.Forms.DockStyle.Top
            Me.ListControl1.Enabled = False
        Else
            Me.ComboBox1 = New System.Windows.Forms.ComboBox
            Me.ListControl1 = Me.ComboBox1
            Me.ListControl1.Font = _font
            If c.Lines = 0 Then
                Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
            Else
                Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDown
            End If
            Me.ListControl1.Dock = System.Windows.Forms.DockStyle.Top
            AddHandler ComboBox1.KeyDown, AddressOf Control_KeyDown
            AddHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged
            Me.ListControl1.Enabled = True
        End If
        Me.ListControl1.Name = c.AttributeName & "ListBox"

        Me.ListControl1.ContextMenu = Me.EditContextMenu
        Me.Controls.Add(Me.ListControl1)

        Me.ListControl1.BringToFront()

        Me._listDataSource = c.ListDataSource
        Me._listDisplayColumn = c.ListDisplayColumn
        Me._listValueColumn = c.ListValueColumn
        Me.FillList()

    End Sub

    Private Sub addDatePicker(ByVal c As WebService.FormDefinition.FormFieldRow)
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker1.Font = _font
        Me.DateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DateTimePicker1.Height = Me.dummyCombo.Height
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Name = c.AttributeName & "DatePicker"
        Me.DateTimePicker1.CustomFormat = gDateFormat
        Me.DateTimePicker1.Value = Today
        Me.DateTimePicker1.Enabled = c.LockCode <> LockCodes.Locked
        Me.DateTimePicker1.ContextMenu = Me.EditContextMenu
#If WINDOWSCE Then
        EnableContextMenu(Me.DateTimePicker1)
#End If
        Me.Controls.Add(Me.DateTimePicker1)
        AddHandler DateTimePicker1.KeyDown, AddressOf Control_KeyDown
        AddHandler DateTimePicker1.ValueChanged, AddressOf MakeDirty
        Me.DateTimePicker1.BringToFront()
    End Sub

    Private Sub addDateTimePicker(ByVal c As WebService.FormDefinition.FormFieldRow)
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker1.Font = _font
        Me.DateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DateTimePicker1.Height = Me.dummyCombo.Height
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Name = c.AttributeName & "DateTimePicker"
        Me.DateTimePicker1.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " & System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern
        Me.DateTimePicker1.ShowUpDown = True
        Me.DateTimePicker1.Value = Now
        Me.DateTimePicker1.ContextMenu = Me.EditContextMenu
        Me.DateTimePicker1.Enabled = c.LockCode <> LockCodes.Locked
#If WINDOWSCE Then
        EnableContextMenu(Me.DateTimePicker1)
#End If
        Me.Controls.Add(Me.DateTimePicker1)
        AddHandler DateTimePicker1.KeyDown, AddressOf Control_KeyDown
        AddHandler DateTimePicker1.ValueChanged, AddressOf MakeDirty
        Me.DateTimePicker1.BringToFront()
    End Sub

    Private Sub addNumberBox(ByVal c As WebService.FormDefinition.FormFieldRow)
        Me.NumericUpDown1 = New activiser.Library.Forms.NumberPicker
        Me.NumericUpDown1.Font = _font
        Me.NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Top
        If c.Lines > 1 Then
            Me.NumericUpDown1.Increment = c.Lines
        Else
            Me.NumericUpDown1.Increment = 1
        End If
        Me.NumericUpDown1.Name = c.AttributeName & "NumberPicker"
        Me.NumericUpDown1.DecimalPlaces = c.DecimalPlaces
        Me.NumericUpDown1.Maximum = CDec(c.MaximumValue)
        Me.NumericUpDown1.Minimum = CDec(c.MinimumValue)
        Me.NumericUpDown1.ReadOnly = c.LockCode = LockCodes.Locked
        Me.NumericUpDown1.ContextMenu = Me.EditContextMenu
#If WINDOWSCE Then
        EnableContextMenu(Me.NumericUpDown1)
#End If
        Me.Controls.Add(Me.NumericUpDown1)
        'Me.NumericUpDown1.Size = New Size(100, 22) '' HACK: this isn't working properly in the (home-grown) control.
        Me.Height = Me.NumericUpDown1.Height
        AddHandler NumericUpDown1.KeyDown, AddressOf Control_KeyDown
        AddHandler NumericUpDown1.ValueChanged, AddressOf MakeDirty
        Me.NumericUpDown1.BringToFront()
    End Sub

    Private Sub addTextBox(ByVal c As WebService.FormDefinition.FormFieldRow)
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox1.Name = c.AttributeName & "TextBox"
        Me.TextBox1.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Multiline = c.Lines > 1
        Me.TextBox1.MaxLength = If(c.MaximumValue > 0, CInt(c.MaximumValue), 0)
        Me.TextBox1.Font = Me._font
        Me.TextBox1.ReadOnly = c.LockCode = LockCodes.Locked
        If Me.TextBox1.Multiline Then
            Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Else
            Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Top
        End If
        If c.MaximumValue > 20 Then
            If c.Lines > 1 Then
                Me.TextBox1.ScrollBars = ScrollBars.Both
                Me.TextBox1.AcceptsReturn = True
            End If
        End If
        Me.TextBox1.ContextMenu = Me.EditContextMenu
#If WINDOWSCE Then
        EnableContextMenu(Me.TextBox1)
#End If
        Me.Controls.Add(Me.TextBox1)
        AddHandler TextBox1.KeyDown, AddressOf Control_KeyDown
        AddHandler TextBox1.TextChanged, AddressOf MakeDirty
        Me.TextBox1.BringToFront()
    End Sub

    Private Sub addTimePicker(ByVal c As WebService.FormDefinition.FormFieldRow)
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker1.Font = _font
        Me.DateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DateTimePicker1.Height = Me.dummyCombo.Height
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Name = c.AttributeName & "TimePicker"
        Me.DateTimePicker1.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern
        Me.DateTimePicker1.Value = New Date(1900, 1, 1) + Now.TimeOfDay
        Me.DateTimePicker1.ShowUpDown = True
        Me.DateTimePicker1.Enabled = c.LockCode <> LockCodes.Locked
        Me.DateTimePicker1.ContextMenu = Me.EditContextMenu
#If WINDOWSCE Then
        EnableContextMenu(Me.DateTimePicker1)
#End If
        Me.Controls.Add(Me.DateTimePicker1)
        AddHandler DateTimePicker1.KeyDown, AddressOf Control_KeyDown
        AddHandler DateTimePicker1.ValueChanged, AddressOf MakeDirty
        Me.DateTimePicker1.BringToFront()
    End Sub

#End Region

    Sub New(ByVal controlRow As WebService.FormDefinition.FormFieldRow, ByVal table As DataTable, ByVal parent As CustomFormPanel, ByVal font As Font)
        If controlRow Is Nothing Then
            Throw New ArgumentNullException("controlRow")
        End If

        If table Is Nothing Then
            Throw New ArgumentNullException("table")
        End If

        If parent Is Nothing Then
            Throw New ArgumentNullException("parent")
        End If

        InitializeComponent()
        Me.SuspendLayout()
        Me.Font = font
        Me.dummyCombo.Font = font ' used for sizing everything else

        parent.CustomControlPanel.Controls.Add(Me)

        Me._customControlRow = controlRow
        Me._customForm = controlRow.FormRow
        Me._fieldName = controlRow.AttributeName
        Me._dataType = table.Columns(Me._fieldName).DataType
        Me._nullable = table.Columns(Me._fieldName).AllowDBNull

        Me._widthPercent = controlRow.Width
        Me._labelPosition = CType(Asc(controlRow.Position(1)), LabelPosition)
        If controlRow.LabelWidth = 0 Then
            Me._labelWidthPercent = 40
        Else
            Me._labelWidthPercent = controlRow.LabelWidth
        End If
        Me._controlPosition = CType(Asc(controlRow.Position(0)), ControlPosition)
        Me._lines = controlRow.Lines

        Me.Caption.Text = controlRow.Label
        Try
            Me._controlType = CType(controlRow.FieldType, ControlType)
        Catch ex As Exception
            Me._controlType = ControlType.Undefined
        End Try
        Select Case Me._controlType
            Case ControlType.CheckBox
                addCheckBox(controlRow)
            Case ControlType.List
                addListBox(controlRow)
            Case ControlType.Date
                addDatePicker(controlRow)
            Case ControlType.DateTime
                addDateTimePicker(controlRow)
            Case ControlType.Number
                addNumberBox(controlRow)
            Case ControlType.TextBox
                addTextBox(controlRow)
            Case ControlType.Time
                addTimePicker(controlRow)
            Case Else
                Throw New ArgumentException(String.Format(My.Resources.CustomFormUnsupportedControlType, Me._controlType), "controlRow")
        End Select
        '_initialised = True
        SetFonts()
        Me.SetLayout()
        Me.ResumeLayout(True)
        Me.Refresh()
    End Sub

    Private Sub SetFonts()
        Me.dummyCombo.Font = _font
        Select Case Me._controlType
            Case ControlType.CheckBox
                Me.CheckBox1.Font = _font
            Case ControlType.List
                If Me.ComboBox1 IsNot Nothing Then
                    Me.ComboBox1.Font = _font
                End If
                If Me.ListBox1 IsNot Nothing Then
                    ' hack to make listbox same height as combobox.
                    Me.ListControl1.Font = New Font(_font.Name, _font.Size + 2, _font.Style)
                End If
            Case ControlType.Date, ControlType.DateTime, ControlType.Time
                Me.DateTimePicker1.Font = _font
            Case ControlType.Number
                Me.NumericUpDown1.Font = _font
            Case ControlType.TextBox
                Me.TextBox1.Font = _textFont
        End Select
        If Not Me._nullable Then
            Me.Caption.Font = New Font(_font.Name, _font.Size, FontStyle.Bold)
        Else
            Me.Caption.Font = New Font(_font.Name, _font.Size, FontStyle.Italic Or FontStyle.Bold)
        End If
    End Sub

    Public Overrides Property Font() As Font
        Get
            Return _font
        End Get
        Set(ByVal value As Font)
            Me._textFont = value
            Me._font = If(value.Size > 8, value, New Font(value.Name, 9, value.Style))

            If Not MyBase.Font.Equals(value) Then
                MyBase.Font = value
            End If
            SetFonts()
        End Set
    End Property

    Private _listDataSource As String
    Public ReadOnly Property ListDataSource() As String
        Get
            Return _listDataSource
        End Get
    End Property

    Private _listValueColumn As String
    Public ReadOnly Property ListValueColumn() As String
        Get
            Return _listValueColumn
        End Get
    End Property

    Private _listDisplayColumn As String
    Public ReadOnly Property ListDisplayColumn() As String
        Get
            Return _listDisplayColumn
        End Get
    End Property

    Public ReadOnly Property Lines() As Integer
        Get
            Return _lines
        End Get
    End Property

    'Private _ListBindingSource As BindingSource
    Private Class ListFiller
        Public Key As Object
        Public DisplayValue As String
        Public Sub New(ByVal key As Object, ByVal displayValue As String)
            Me.Key = key
            Me.DisplayValue = displayValue
        End Sub

        Public Overrides Function ToString() As String
            Return DisplayValue
        End Function
    End Class

    Public Sub FillList()
        If String.IsNullOrEmpty(_listDataSource) AndAlso String.IsNullOrEmpty(_listValueColumn) AndAlso String.IsNullOrEmpty(_listDisplayColumn) Then
            ' assume 'integrated' list
            Dim listReader As New IO.StringReader(Me._customControlRow.ListData)
            Dim listData As New Generic.List(Of String)
            Dim listItem As String

            Do
                listItem = listReader.ReadLine
                If listItem Is Nothing Then Exit Do
                If Not String.IsNullOrEmpty(listItem.Trim) Then
                    listData.Add(listItem)
                End If
            Loop
            For Each listItem In listData
                Dim listKey, listValue As String
                Dim separatorLocation As Integer = listItem.IndexOf(";"c)
                listKey = listItem.Substring(0, separatorLocation)
                listValue = listItem.Substring(separatorLocation + 1, listItem.Length - separatorLocation)
                If Not String.IsNullOrEmpty(listKey) AndAlso Not String.IsNullOrEmpty(listValue) Then
                    If Me.ListControl1 Is Me.ComboBox1 Then
                        Me.ComboBox1.Items.Add(New CustomControl.ListFiller(listKey, listValue))
                    ElseIf Me.ListControl1 Is Me.ListBox1 Then
                        Me.ListBox1.Items.Add(New CustomControl.ListFiller(listKey, listValue))
                    Else
                        'panic
                    End If
                End If
            Next
            'Dim ccdrs() As WebService.ClientDataSet.CustomControlDataRow = Me._customControlRow.GetCustomControlDataRows
            'Me.ListControl1.ValueMember = "DataValue"
            'Me.ListControl1.DisplayMember = "DisplayValue"
            'Me.ListControl1.DataSource = ccdrs
        Else
            If Not String.IsNullOrEmpty(_listDataSource) Then
                If Not String.IsNullOrEmpty(_listValueColumn) Then
                    If Not String.IsNullOrEmpty(_listDisplayColumn) Then
                        Dim listSourceTable As DataTable = gClientDataSet.Tables(_listDataSource)

                        Me.ListControl1.SuspendLayout()

                        Dim valueIndex As Integer = listSourceTable.Columns.IndexOf(_listValueColumn)
                        Dim displayIndex As Integer = listSourceTable.Columns.IndexOf(_listDisplayColumn)
                        Dim sourceList As System.Data.DataRow()
                        Dim listFilter As String = String.Format(WithoutCulture, "NOT {0} IS NULL", _listValueColumn)
                        If Not Me._customControlRow.IsListDataNull AndAlso Not String.IsNullOrEmpty(Me._customControlRow.ListData) Then
                            listFilter = String.Format(WithoutCulture, Me._customControlRow.ListData, _listDataSource, _listValueColumn, _listDisplayColumn)
                        End If
                        If Me._customControlRow.LockCode = 1 Then ' no need to sort a read-only list.
                            sourceList = listSourceTable.Select(listFilter) ' , _listDisplayColumn)
                        Else
                            sourceList = listSourceTable.Select(listFilter, _listDisplayColumn)
                        End If
                        If Me.ListControl1 Is Me.ComboBox1 Then
                            For Each dr As DataRow In sourceList
                                If dr.IsNull(displayIndex) Then
                                    Me.ComboBox1.Items.Add(New CustomControl.ListFiller(dr(valueIndex), String.Empty))
                                Else
                                    Me.ComboBox1.Items.Add(New CustomControl.ListFiller(dr(valueIndex), CStr(dr(displayIndex))))
                                End If
                            Next
                        ElseIf Me.ListControl1 Is Me.ListBox1 Then
                            For Each dr As DataRow In sourceList
                                If dr.IsNull(displayIndex) Then
                                    Me.ListBox1.Items.Add(New CustomControl.ListFiller(dr(valueIndex), String.Empty))
                                Else
                                    Me.ListBox1.Items.Add(New CustomControl.ListFiller(dr(valueIndex), CStr(dr(displayIndex))))
                                End If
                            Next
                        Else
                            'panic
                        End If

                        Me.ListControl1.ValueMember = "Key" ' _listValueColumn
                        Me.ListControl1.DisplayMember = "DisplayValue" ' _listDisplayColumn
                        Me.ListControl1.ResumeLayout()
                    End If
                End If
            End If
        End If
    End Sub


    Public ReadOnly Property CustomFormRow() As WebService.FormDefinition.FormRow
        Get
            Return _customForm
        End Get
    End Property

    Public ReadOnly Property ControlPosition() As ControlPosition
        Get
            Return _controlPosition
        End Get
    End Property

    Public ReadOnly Property ControlType() As ControlType
        Get
            Return _controlType
        End Get
    End Property

    Public ReadOnly Property CustomControlRow() As WebService.FormDefinition.FormFieldRow
        Get
            Return _customControlRow
        End Get
    End Property

    Public ReadOnly Property FieldName() As String
        Get
            Return Me._fieldName
        End Get
    End Property

    Public ReadOnly Property Nullable() As Boolean
        Get
            Return _nullable
        End Get
    End Property

    Public ReadOnly Property WidthPercent() As Byte
        Get
            Return _widthPercent
        End Get
    End Property

    Public ReadOnly Property LabelPosition() As LabelPosition
        Get
            Return Me._labelPosition
        End Get
    End Property

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")> _
    Public Function GetWidth() As Integer
        Dim result As Integer
        Dim p As Panel = TryCast(Me.Parent, Panel)
        If p IsNot Nothing Then
            result = ((p.ClientSize.Width * Me._widthPercent) \ 100)
            If Me.CustomControlRow IsNot Nothing Then
                If Me.CustomControlRow.Position = "F" Then
                    result -= GetSystemMetrics(SM_CXVSCROLL)
                Else
                    result -= GetSystemMetrics(SM_CXVSCROLL) \ 2
                    If Me.CustomControlRow.Position = "L" Then
                        result -= System.Windows.Forms.SystemInformation.BorderSize.Width
                    End If
                End If
            Else
                result -= GetSystemMetrics(SM_CXVSCROLL)
            End If
            'If p.ClientSize.Width < p.Width Then
            '    result -= p.Width - p.ClientSize.Width
            '    result -= 8
            'End If
        Else
            result = ((Windows.Forms.Screen.PrimaryScreen.Bounds.Width * Me._widthPercent) \ 100) - GetSystemMetrics(SM_CXVSCROLL)
            'result = ((240 * Me._widthPercent) \ 100) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth
        End If
        Return result
    End Function

    Private _labelHeight As Integer
    Public Sub SetLayout()
        Select Case _labelPosition
            Case LabelPosition.Left
                Me.Caption.Visible = True
                Me.Caption.Dock = DockStyle.Left
                Me.Splitter1.Dock = DockStyle.Left
                Me.Splitter1.Width = CInt(3 * RunTimeScale)
                Me.Splitter1.Enabled = True
                Me.Splitter1.Visible = True
            Case LabelPosition.None
                Me.Splitter1.Enabled = False
                Me.Splitter1.Visible = False
                Me.Caption.Visible = False
            Case LabelPosition.Right
                If Me._customControlRow.FieldType = ControlType.CheckBox Then
                    Me.Splitter1.Visible = False
                    Me.Caption.Visible = False
                Else
                    Me.Caption.Visible = True
                    Me.Caption.Dock = DockStyle.Right
                    Me.Splitter1.Dock = DockStyle.Right
                    Me.Splitter1.Width = CInt(3 * RunTimeScale)
                    Me.Splitter1.Enabled = True
                    Me.Splitter1.Visible = True
                End If
            Case LabelPosition.Top
                Me.Caption.Visible = True
                Me.Caption.Dock = DockStyle.Top
                Using g As Graphics = Me.CreateGraphics
                    Dim s As SizeF = g.MeasureString(Me.Caption.Text, Me.Font)
                    _labelHeight = CInt((RunTimeScale * 2) + s.Height)
                    Me.Caption.Height = _labelHeight
                End Using
                Me.Splitter1.Dock = DockStyle.Top
                Me.Splitter1.Visible = False
                Me.Splitter1.Enabled = False
        End Select
        Me.ResizeMe()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")> _
    Public Function GetHeight() As Integer
        Dim h As Integer = 0

        If Me.LabelPosition = LabelPosition.Top Then
            h += _labelHeight
        End If
        Select Case Me._controlType
            Case ControlType.CheckBox
                h += Me.dummyCombo.Height
            Case ControlType.Date, ControlType.DateTime, ControlType.Time
                h += Me.DateTimePicker1.Height
            Case ControlType.List
                h += Me.ListControl1.Height
            Case ControlType.Number
                h += Me.NumericUpDown1.Height
            Case ControlType.TextBox, ControlType.Undefined
                If Me.Lines > 1 Then
                    Using g As Graphics = Me.CreateGraphics
                        Dim s As Size = g.MeasureString(My.Resources.activiserFormTitle, MyBase.Font).ToSize
                        h += s.Height * Me._lines
                    End Using
                Else
                    h += Me.dummyCombo.Height
                End If
        End Select
        h += CInt(RunTimeScale * 2)
        Return h
    End Function

    Private _inResize As Boolean

    Private Sub CustomControl_ParentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ParentChanged
        ResizeMe()
    End Sub

    Private Sub ResizeMe()
        ' If Not Me._initialised Then Return
        If Me._inResize Then Return

        Me._inResize = True
        Dim w As Integer = Me.GetWidth()
        Dim h As Integer = Me.GetHeight()
        Dim s As New Size(w, h)
        If Not s.Equals(Me.Size) Then
            'Debug.WriteLine(String.Format("Control '{0}' Resized: {1}, {2}", Me.FieldName, Me.Size.Width, Me.Size.Height))
            Me.Size = s
            'Debug.WriteLine(String.Format("Reset to: {0}, {1}", Me.Size.Width, Me.Size.Height))
        End If

        If Me._labelPosition = LabelPosition.Left OrElse Me._labelPosition = LabelPosition.Right Then
            Dim newCaptionWidth As Integer = (Me.Width * Me._labelWidthPercent) \ 100
            If Not Me.Caption.Width = newCaptionWidth Then Me.Caption.Width = newCaptionWidth
        End If

        Me._inResize = False
    End Sub

    Private Sub Control_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ResizeMe()
    End Sub

#Region "Value"
    Public ReadOnly Property Dirty() As Boolean
        Get
            Return _dirty
        End Get
    End Property

    'Private _value As Object
    Private _settingValue As Boolean
    Private Sub setCheckBoxValue(ByVal value As Object)
        If value Is Nothing OrElse IsDBNull(value) Then
            If Me.CheckBox1.ThreeState Then
                Me.setCheckBoxValue(Me.CheckBox1, CheckState.Indeterminate)
            Else
                Me.setCheckBoxValue(Me.CheckBox1, CheckState.Unchecked)
            End If
        Else
            Try
                If CBool(value) Then
                    Me.setCheckBoxValue(Me.CheckBox1, CheckState.Checked)
                Else
                    Me.setCheckBoxValue(Me.CheckBox1, CheckState.Unchecked)
                End If
            Catch ex As Exception
                If Me.CheckBox1.ThreeState Then
                    Me.setCheckBoxValue(Me.CheckBox1, CheckState.Indeterminate)
                Else
                    Me.setCheckBoxValue(Me.CheckBox1, CheckState.Unchecked)
                End If
            End Try
        End If
        ' this is just here to improve the PDA debugging experience - stepping from any previous line will take right out of the method.
        Debug.WriteLine("setCheckBoxValue called.")
    End Sub

    Private Sub setDateTimeValue(ByVal value As Object)
        If value Is Nothing OrElse IsDBNull(value) Then
            If Me._controlType = ControlType.Time Then
                SetDateTimePickerValue(Me.DateTimePicker1, New DateTime(1900, 1, 1, 0, 0, 0)) ' set to zero-hour
            Else
                SetDateTimePickerValue(Me.DateTimePicker1, DateTime.Today)
            End If
        Else
            If TypeOf value Is DateTime Then
                SetDateTimePickerValue(Me.DateTimePicker1, DirectCast(value, DateTime))
            End If
        End If
        ' this is just here to improve the PDA debugging experience - stepping from any previous line will take right out of the method.
        Debug.WriteLine("setDateTimeValue called.")
    End Sub

    Private Sub setNumericValue(ByVal value As Object)
        Dim newValue As Decimal
        If Value Is Nothing OrElse IsDBNull(Value) Then
            newValue = Me.NumericUpDown1.Minimum
        Else
            If TypeOf value Is Decimal Then
                newValue = DirectCast(value, Decimal)
            ElseIf TypeOf value Is Int32 OrElse TypeOf value Is Int16 OrElse TypeOf value Is Single OrElse TypeOf value Is Double Then
                newValue = CDec(value)
            Else
                If Not IsDBNull(value) Then
                    Try
                        newValue = CDec(value)
                    Catch
                        newValue = Me.NumericUpDown1.Minimum
                    End Try
                Else
                    newValue = Me.NumericUpDown1.Minimum
                End If
            End If
            If newValue < Me.NumericUpDown1.Minimum Then
                newValue = Me.NumericUpDown1.Minimum
            ElseIf newValue > Me.NumericUpDown1.Maximum Then
                newValue = Me.NumericUpDown1.Maximum
            End If

        End If
        Me.SetNumericUpDownValue(Me.NumericUpDown1, newValue)
        ' this is just here to improve the PDA debugging experience - stepping from any previous line will take right out of the method.
        Debug.WriteLine("setNumericValue called.")
    End Sub



    Private Function GetValue() As Object
        If Me.InvokeRequired Then
            Return Me.Invoke(New GetValueDelegate(Of Object)(AddressOf GetValue))
        Else
            Select Case Me._controlType
                Case ControlType.CheckBox
                    If Me._nullable AndAlso Me.CheckBox1.CheckState = CheckState.Indeterminate Then
                        Return Convert.DBNull
                    Else
                        Return Me.CheckBox1.Checked
                    End If
                Case ControlType.Date, ControlType.DateTime, ControlType.Time
                    If Me._nullable AndAlso Me.DateTimePicker1.Value = DateTime.MinValue Then
                        Return Convert.DBNull
                    Else
                        Return Me.DateTimePicker1.Value
                    End If
                Case ControlType.List
                    If Me._nullable AndAlso Me.ComboBox1.SelectedIndex = -1 Then
                        Return DBNull.Value
                    Else
                        Try
                            Return CType(Me.ComboBox1.SelectedItem, ListFiller).Key
                        Catch ex As Exception
                            Return DBNull.Value
                        End Try
                    End If
                Case ControlType.Number
                    If Me._dataType Is GetType(Double) Then
                        Return Me.NumericUpDown1.ToDouble
                    ElseIf Me._dataType Is GetType(Int32) Then
                        Return Me.NumericUpDown1.ToInt32
                    ElseIf Me._dataType Is GetType(Byte) Then
                        Return Me.NumericUpDown1.ToByte
                    ElseIf Me._dataType Is GetType(Int16) Then
                        Return Me.NumericUpDown1.ToInt16
                    ElseIf Me._dataType Is GetType(Int64) Then
                        Return Me.NumericUpDown1.ToInt64
                    ElseIf Me._dataType Is GetType(Single) Then
                        Return Me.NumericUpDown1.ToSingle
                    ElseIf Me._dataType Is GetType(Decimal) Then
                        Return Me.NumericUpDown1.ToDecimal
                    Else
                        Return Me.NumericUpDown1.Value
                    End If
                Case ControlType.TextBox
                    Return Me.TextBox1.Text
                Case Else
                    'que? - ignore this
                    Return Nothing
            End Select
        End If
    End Function

    Private Delegate Sub SetValueDelegate(ByVal value As Object)
    Private Sub SetValue(ByVal value As Object)
        If Me._settingValue Then Return

        If Me.InvokeRequired Then
            Dim d As New SetValueDelegate(AddressOf SetValue)
            Me.Invoke(d, New Object() {value})
        Else
            Me._settingValue = True
            'Me._value = value
            Select Case Me._controlType
                Case ControlType.CheckBox
                    setCheckBoxValue(value)
                Case ControlType.Date, ControlType.DateTime, ControlType.Time
                    setDateTimeValue(value)
                Case ControlType.List
                    If value Is Nothing OrElse IsDBNull(value) Then
                        Me.SetListIndex(Me.ListControl1, -1)
                    Else
                        Me.SetListValue(Me.ListControl1, value)
                    End If
                Case ControlType.Number
                    setNumericValue(value)
                Case ControlType.TextBox
                    Try
                        If value Is Nothing OrElse IsDBNull(value) Then
                            SetTextBoxValue(Me.TextBox1, String.Empty)
                        Else
                            SetTextBoxValue(Me.TextBox1, value.ToString)
                        End If
                    Catch ex As Exception
                        SetTextBoxValue(Me.TextBox1, String.Empty)
                    End Try
                Case ControlType.Undefined
                    'que? - ignore this
            End Select
            Me._dirty = False
            Me._settingValue = False
        End If
    End Sub

    '    <Category("Data"), Bindable(True)> _
    Public Property Value() As Object
        Get
            Return GetValue()
        End Get
        Set(ByVal value As Object)
            Me.SetValue(value)
        End Set
    End Property

    Public ReadOnly Property DefaultListValue() As Object
        Get
            If Me._customControlRow.LockCode = 1 Then
                Return DBNull.Value
            End If

            If Me.Nullable Then
                Return DBNull.Value
            Else
                If Me.ComboBox1.Items.Count = 0 Then
                    Return DBNull.Value
                Else
                    Return CType(Me.ComboBox1.Items(0), ListFiller).Key
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property [ReadOnly]() As Boolean
        Get
            Return Me._customControlRow.LockCode = 1
        End Get
    End Property

    'Private Delegate Sub SetDefaultValueDelegate()
    'Public Sub SetDefaultValue()
    '    If Me.InvokeRequired Then
    '        Dim d As New SetDefaultValueDelegate(AddressOf SetDefaultValue)
    '        Me.Invoke(d)
    '    Else
    '        Select Case Me._controlType
    '            Case ControlTypes.CheckBox
    '                Me.SetValue(False)
    '            Case ControlTypes.Date
    '                Me.SetValue(DateTime.Today)
    '            Case ControlTypes.DateTime
    '                Me.SetValue(DateTime.Now)
    '            Case ControlTypes.Time
    '                Me.SetValue(DateTime.Now.TimeOfDay)
    '            Case ControlTypes.List
    '                If Me._nullable Then
    '                    Me.SetValue(-1)
    '                Else
    '                    Dim drv As DataRowView = TryCast(Me._ListBindingSource.Item(0), DataRowView)
    '                    If drv IsNot Nothing Then
    '                        Me.SetValue(drv.Row(Me._listValueColumn))
    '                    End If
    '                End If
    '            Case ControlTypes.Number
    '                Me.SetValue(0)
    '            Case ControlTypes.TextBox
    '                Me.SetValue(String.Empty)
    '            Case ControlTypes.Undefined
    '                'que? - ignore this
    '        End Select
    '    End If
    '    Debug.WriteLine("Default Value Set")
    '    Application.DoEvents()
    'End Sub
#End Region

#Region "Thread safety delegates"
    Private Delegate Sub SetListValueCallBack(ByVal Control As ListControl, ByVal Value As Object)
    Private Sub SetListValue(ByVal Control As ListControl, ByVal Value As Object)
        If Control.InvokeRequired Then
            Dim d As New SetListValueCallBack(AddressOf SetListValue)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            If IsDBNull(Value) Then
                Control.SelectedIndex = -1 ' .SelectedValue = Nothing
                Return
            Else
                Control.SuspendLayout()
                Control.SelectedIndex = -1
                If Control Is Me.ComboBox1 Then
                    For Each li As ListFiller In Me.ComboBox1.Items
                        If li.Key.Equals(Value) Then
                            Me.ComboBox1.SelectedItem = li
                            Exit For
                        End If
                    Next
                ElseIf Control Is Me.ListBox1 Then
                    For Each li As ListFiller In Me.ListBox1.Items
                        If li.Key.Equals(Value) Then
                            Me.ListBox1.SelectedItem = li
                            Me.ListBox1.SelectedIndex = -1
                            Exit For
                        End If
                    Next
                Else
                    'que ?
                End If
                Control.ResumeLayout()
            End If
        End If
    End Sub

    Private Delegate Sub SetTextBoxValueCallBack(ByVal Control As TextBox, ByVal Value As String)
    Private Sub SetTextBoxValue(ByVal Control As TextBox, ByVal Value As String)
        If Control.InvokeRequired Then
            Dim d As New SetTextBoxValueCallBack(AddressOf SetTextBoxValue)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            If Control.Text = Value Then Return
            Control.Text = Value
        End If
    End Sub

    Private Delegate Sub SetListIndexCallBack(ByVal Control As ListControl, ByVal Value As Integer)
    Private Sub SetListIndex(ByVal Control As ListControl, ByVal Value As Integer)
        If Control.InvokeRequired Then
            Dim d As New SetListIndexCallBack(AddressOf SetListIndex)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            If Control.SelectedIndex = Value Then Return
            Debug.WriteLine(String.Format("Setting List '{0}' Index: Old: {1}, New: {2}", Me.FieldName, Control.SelectedIndex, Value))
            Control.SelectedIndex = Value
        End If
    End Sub

    Private Delegate Sub SetDateTimePickerValueCallBack(ByVal Control As DateTimePicker, ByVal Value As DateTime)
    Private Sub SetDateTimePickerValue(ByVal Control As DateTimePicker, ByVal Value As DateTime)
        If Control.InvokeRequired Then
            Dim d As New SetDateTimePickerValueCallBack(AddressOf SetDateTimePickerValue)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            If Control.Value = Value Then Return
            Debug.WriteLine(String.Format("Setting DateTimePicker '{0}' Value: Old: {1:s}, New: {2:s}", Me.FieldName, Control.Value, Value))
            Control.Value = Value
        End If
    End Sub

    Private Delegate Sub SetNumericUpDownValueCallBack(ByVal Control As activiser.Library.Forms.NumberPicker, ByVal Value As Decimal)
    Private Sub SetNumericUpDownValue(ByVal Control As activiser.Library.Forms.NumberPicker, ByVal Value As Decimal)
        If Control.InvokeRequired Then
            Dim d As New SetNumericUpDownValueCallBack(AddressOf SetNumericUpDownValue)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            If Control.Value = Value Then Return
            Debug.WriteLine(String.Format("Setting Number '{0}' Value: Old: {1}, New: {2}", Me.FieldName, Control.Value, Value))
            Control.Value = Value
        End If
    End Sub

    Private Delegate Sub SetCheckBoxValueCallBack(ByVal Control As CheckBox, ByVal Value As CheckState)
    Private Sub SetCheckBoxValue(ByVal Control As CheckBox, ByVal Value As CheckState)
        If Control.InvokeRequired Then
            Dim d As New SetCheckBoxValueCallBack(AddressOf SetCheckBoxValue)
            Me.Invoke(d, New Object() {Control, Value})
        Else
            If Control.CheckState = Value Then Return
            Debug.WriteLine(String.Format("Setting Checkbox '{0}' Value: Old: {1}, New: {2}", Me.FieldName, Control.CheckState.ToString, Value.ToString))
            Control.CheckState = Value
        End If
    End Sub
#End Region

    Private Sub MakeDirty(ByVal sender As Object, ByVal e As System.EventArgs)
        If _settingValue Then Return
        _dirty = True
        RaiseEvent ValueChanged(Me, New System.EventArgs)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If _settingValue Then Return
        _dirty = True
        RaiseEvent ValueChanged(Me, New System.EventArgs)
    End Sub


    Private Sub Control_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If Me.Parent Is Nothing Then Return
        'If e.KeyCode = Keys.F23 Then ' DPad centre key
        '    If sender Is Me.ComboBox1 Then
        '        _inDropDown = Not _inDropDown
        '    End If
        'End If
        If sender Is Me.ComboBox1 Then
            If e.KeyCode = Keys.Left Then
                Me.Parent.SelectNextControl(Me, False, True, True, False)
                e.Handled = True
            ElseIf e.KeyCode = Keys.Right Then
                Me.Parent.SelectNextControl(Me, True, True, True, False)
                e.Handled = True
            End If
        Else
            If e.KeyCode = Keys.Up Then
                Me.Parent.SelectNextControl(Me, False, True, True, False)
                e.Handled = True
            ElseIf e.KeyCode = Keys.Down Then
                Me.Parent.SelectNextControl(Me, True, True, True, False)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub CustomControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click

    End Sub
End Class
