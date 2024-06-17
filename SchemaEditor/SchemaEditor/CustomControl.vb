Imports System.ComponentModel
Imports activiser.SchemaEditor.activiserSchema

Public Class CustomControl
    Private Const baseHeight As Integer = 22

    Private _label As String
    Private _controlType As ControlType
    Private _dataType As String
    Private _fieldName As String
    Private _LockCode As LockCode = 0
    Private _labelPosition As LabelPosition = LabelPosition.Left
    Private _lines As Byte = 1
    Private _maximumValue As Decimal
    Private _minimumValue As Decimal
    Private _pdaControlCollection As PdaControlCollection
    Private _decimalPlaces As Byte
    Private _sequence As Byte
    Private _tableName As String
    Private _limitToList As Boolean
    'Private _showLabel As Boolean = True
    Private _FormFieldRow As FormFieldRow
    Private _controlPosition As ControlPosition = ControlPosition.FullWidth
    Private _Width As Byte = 100

    Private _visibleControl As Control

    Private _initialised As Boolean

    Sub New(ByRef c As FormFieldRow)
        Me.FormFieldRow = c
        InitializeComponent()
        'Me.SuspendLayout()
        Me._customForm = c.FormRow
        Me.Label = c.Label
        Me.FieldName = c.AttributeName
        Me.TableName = c.FormRow.EntityName
        Try
            Me.ControlType = CType(c.FieldType, ControlType)
        Catch ex As Exception
            Me.ControlType = ControlType.Undefined
        End Try

        Me.DisplayOrder = c.DisplayOrder
        If Me.DisplayOrder = 0 Then Me.BackColor = Color.Brown
        Me.ControlPosition = If(c.Position.Length > 0, CType(Asc(c.Position(0)), ControlPosition), ControlPosition.FullWidth)
        Me.LabelPosition = If(c.Position.Length > 1, CType(Asc(c.Position(1)), LabelPosition), LabelPosition.Left)
        Me.DecimalPlaces = c.DecimalPlaces
        Me.LockCode = CType(c.LockCode, SchemaEditor.LockCode)
        Me.Lines = c.Lines
        If Not c.IsListDataSourceNull AndAlso Not String.IsNullOrEmpty(c.ListDataSource) Then
            Me.ListDataSource = c.ListDataSource
            Me.ListDisplayColumn = c.ListDisplayColumn
            Me.ListValueColumn = c.ListValueColumn
        ElseIf Not c.IsListDataNull AndAlso Not String.IsNullOrEmpty(c.ListData) Then
            ' TODO:
        End If
        Me.MaximumValue = c.MaximumValue
        Me.MinimumValue = c.MinimumValue
        Me.ControlWidth = c.Width
        Me.SortPriority = c.SortPriority
        Me.Visible = True
        Me._initialised = True
        Me.Height = Me.GetHeight()
        Me.ResumeLayout()
    End Sub

    Private _node As LinkedListNode(Of CustomControl)
    Public ReadOnly Property Node() As LinkedListNode(Of CustomControl)
        Get
            Return _pdaControlCollection.Find(Me) ' _node
        End Get
    End Property

    Public Property PdaControlCollection() As PdaControlCollection
        Get
            Return _pdaControlCollection
        End Get
        Set(ByVal value As PdaControlCollection)
            If _pdaControlCollection IsNot value Then
                If _pdaControlCollection IsNot Nothing Then
                    If _pdaControlCollection.Contains(Me) Then
                        _pdaControlCollection.Remove(Me)
                    End If
                End If
                _pdaControlCollection = value
                _node = _pdaControlCollection.AddLast(Me)
            End If
        End Set
    End Property

    Public Property Label() As String
        Get
            Return _label
        End Get
        Set(ByVal value As String)
            _label = value
            Me.LabelEditor.Text = value
            If Me._FormFieldRow IsNot Nothing AndAlso Me._FormFieldRow.Label <> value Then
                Me._FormFieldRow.Label = value
            End If
        End Set
    End Property

    Private _listDataSource As String
    Public Property ListDataSource() As String
        Get
            Return _listDataSource
        End Get
        Set(ByVal value As String)
            _listDataSource = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.ListDataSource <> value Then
                Me.FormFieldRow.ListDataSource = value
            End If
        End Set
    End Property

    Private _listValueColumn As String
    Public Property ListValueColumn() As String
        Get
            Return _listValueColumn
        End Get
        Set(ByVal value As String)
            _listValueColumn = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.ListValueColumn <> value Then
                Me.FormFieldRow.ListValueColumn = value
            End If
        End Set
    End Property

    Private _listDisplayColumn As String
    Public Property ListDisplayColumn() As String
        Get
            Return _listDisplayColumn
        End Get
        Set(ByVal value As String)
            _listDisplayColumn = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.ListDisplayColumn <> value Then
                Me.FormFieldRow.ListDisplayColumn = value
            End If
        End Set
    End Property


    Private _DbConnection As SqlClient.SqlConnection
    Public Property DatabaseConnection() As SqlClient.SqlConnection
        Get
            Return _DbConnection
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            _DbConnection = value
        End Set
    End Property


    Public Sub FillList()
        If String.IsNullOrEmpty(_listDataSource) AndAlso String.IsNullOrEmpty(_listValueColumn) AndAlso String.IsNullOrEmpty(_listDisplayColumn) Then
            ListFiller.FillListBoxData(Me.ComboBox1, Me._FormFieldRow.ListData)
        Else
            If _DbConnection Is Nothing Then
                Me._DbConnection = sqlConnection ' New SqlClient.SqlConnection(My.Settings.activiserConnectionString)
            End If
            If _DbConnection IsNot Nothing Then
                If Not String.IsNullOrEmpty(_listDataSource) Then
                    If Not String.IsNullOrEmpty(_listValueColumn) Then
                        If Not String.IsNullOrEmpty(_listDisplayColumn) Then
                            Dim SQL As String
                            If _listValueColumn <> _listDisplayColumn Then
                                SQL = String.Format("SELECT [{0}], [{1}] FROM {2} ORDER BY [{1}]", _listValueColumn, _listDisplayColumn, _listDataSource)
                            Else
                                SQL = String.Format("SELECT [{0}] FROM {1} ORDER BY [{0}]", _listValueColumn, _listDataSource)
                            End If

                            Dim cmd As New SqlClient.SqlDataAdapter(SQL, _DbConnection)
                            Dim dt As New DataTable(_listDataSource)
                            cmd.Fill(dt)
                            Me.ComboBox1.SuspendLayout()
                            Me.ComboBox1.DataSource = dt
                            Me.ComboBox1.ValueMember = _listValueColumn
                            Me.ComboBox1.DisplayMember = _listDisplayColumn
                            Me.ComboBox1.ResumeLayout()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Property LimitToList() As Boolean
        Get
            Return _limitToList
        End Get
        Set(ByVal value As Boolean)
            _limitToList = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.Lines <> CByte(value) Then
                Me.FormFieldRow.Lines = CByte(value)
            End If
        End Set
    End Property

    Private _customForm As FormRow
    Public Property ParentCustomForm() As FormRow
        Get
            Return _customForm
        End Get
        Set(ByVal value As FormRow)
            _customForm = value
        End Set
    End Property


    Public Property ControlPosition() As ControlPosition
        Get
            Return _controlPosition
        End Get
        Set(ByVal value As ControlPosition)
            _controlPosition = value
            Dim pos As String = Me.FormFieldRow.Position
            Dim cpos As Char = If(pos.Length > 0, pos(0), "F"c)
            Dim lpos As Char = If(pos.Length = 2, pos(1), "L"c)

            If Me.FormFieldRow IsNot Nothing AndAlso cpos <> Chr(value) Then
                Me.FormFieldRow.Position = Chr(value) & lpos
            End If
            If value = ControlPosition.Right Then
                Me.PictureBox1.Dock = DockStyle.Right
            Else
                Me.PictureBox1.Dock = DockStyle.Left
            End If
        End Set
    End Property

    Public Property ControlType() As ControlType
        Get
            Return _controlType
        End Get
        Set(ByVal value As ControlType)
            If value <> Me._controlType Then
                setControlType(value)
                If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.FieldType <> value Then
                    Me.FormFieldRow.FieldType = CByte(value)
                End If
            End If
        End Set
    End Property

    Private _dataColumn As System.Data.DataColumn
    Public Property DataColumn() As System.Data.DataColumn
        Get
            Return _dataColumn
        End Get
        Set(ByVal value As System.Data.DataColumn)
            _dataColumn = value
        End Set
    End Property

    Public Property DataType() As String
        Get
            Return _dataType
        End Get
        Set(ByVal value As String)
            _dataType = value
        End Set
    End Property

    Public Property FieldName() As String
        Get
            Return _fieldName
        End Get
        Set(ByVal value As String)
            _fieldName = value
            If String.IsNullOrEmpty(Me.Label) Then Me.Label = ExpandName(value)
        End Set
    End Property

    Public Property LockCode() As LockCode
        Get
            Return _LockCode
        End Get
        Set(ByVal value As LockCode)
            _LockCode = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.LockCode <> value Then
                Me.FormFieldRow.LockCode = CByte(value)
            End If
            If Me.ControlType = ControlType.DropDownList Then
                If _LockCode <> 0 Then
                    Me.ComboBox1.DropDownStyle = ComboBoxStyle.Simple
                    Me.ComboBox1.Enabled = False
                Else
                    Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
                    Me.ComboBox1.Enabled = Me.DisplayOrder <> 0
                End If
            ElseIf Me.ControlType = ControlType.TextBox Then
                Me.TextBox1.ReadOnly = value <> 0 OrElse Me.DisplayOrder = 0
            ElseIf Me.ControlType = ControlType.Number Then
                Me.NumericUpDown1.Enabled = value = 0 AndAlso Me.DisplayOrder <> 0
            ElseIf Me.ControlType = ControlType.Date OrElse Me.ControlType = ControlType.DateTime OrElse Me.ControlType = ControlType.Time Then
                Me.DateTimePicker1.Enabled = value = 0 AndAlso Me.DisplayOrder <> 0
            End If
        End Set
    End Property

    Public Function GetWidth() As Integer
        Return ((240 * Me._Width) \ 100) + 10
    End Function

    Private Sub LocateCheckbox()
        ' none and top not valid for checkbox...

        Me.LabelEditor.Visible = True
        Me.LabelEditor.Dock = DockStyle.Fill
        Me.LabelEditor.BringToFront()

        Me.Splitter1.Visible = False

        If Me._labelPosition = SchemaEditor.LabelPosition.Left Then
            Me.CheckBox1.Dock = DockStyle.Right
        Else
            Me.CheckBox1.Dock = DockStyle.Left
        End If
    End Sub

    Private Sub LocateNonCheckbox()
        If _labelPosition = SchemaEditor.LabelPosition.None Then
            Me.Splitter1.Enabled = False
            Me.Splitter1.Visible = False
            Me.LabelEditor.Visible = False
        Else
            Me.LabelEditor.Visible = True
            Me.Splitter1.Visible = True
            Me._visibleControl.BringToFront()
            If _labelPosition = SchemaEditor.LabelPosition.Top Then
                Me.LabelEditor.Dock = DockStyle.Top
                Me.Splitter1.Dock = DockStyle.Top
                Me.Splitter1.Height = 3
                Me.Splitter1.Enabled = False
            Else
                If _labelPosition = SchemaEditor.LabelPosition.Right Then
                    Me.LabelEditor.Dock = DockStyle.Right
                    Me.Splitter1.Dock = DockStyle.Right
                Else
                    Me.LabelEditor.Dock = DockStyle.Left
                    Me.Splitter1.Dock = DockStyle.Left
                End If
                Me.Splitter1.Width = 3
                Me.Splitter1.Enabled = True
            End If
        End If
    End Sub

    Public Function GetHeight() As Integer
        ' If Not Me._initialized Then Exit Sub

        Dim h As Integer = Me.Padding.Vertical
        If Me.ControlType = ControlType.TextBox Then
            h += baseHeight * Me._lines
        Else
            h += baseHeight
        End If

        If Me._controlType = SchemaEditor.ControlType.CheckBox Then
            LocateCheckbox()
        Else
            LocateNonCheckbox()
            If Me._labelPosition = SchemaEditor.LabelPosition.Top Then
                h += Me.LabelEditor.Height + Me.Splitter1.Height
            End If
        End If
        Return h
    End Function

    Public Property LabelPosition() As LabelPosition
        Get
            Return _labelPosition
        End Get
        Set(ByVal value As LabelPosition)
            If _labelPosition <> value Then
                _labelPosition = value
                Dim pos As String = Me.FormFieldRow.Position
                Dim cpos As Char = If(pos.Length > 0, pos(0), "F"c)
                Dim lpos As Char = If(pos.Length = 2, pos(1), "L"c)

                If Me.FormFieldRow IsNot Nothing AndAlso lpos <> Chr(value) Then
                    Me.FormFieldRow.Position = cpos & Chr(value)
                End If
                If Me._initialised Then Me.Height = Me.GetHeight()
            End If
        End Set
    End Property

    Public Property Lines() As Byte
        Get
            Return _lines
        End Get
        Set(ByVal value As Byte)
            Dim doShuffle As Boolean = _lines <> value
            _lines = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.Lines <> _lines Then
                Me.FormFieldRow.Lines = _lines
            End If
            If Me.ControlType = ControlType.TextBox Then
                Me.TextBox1.Multiline = Lines > 1
                Me.Height = Me.GetHeight()
            End If
        End Set
    End Property

    Public Property MaximumValue() As Decimal
        Get
            Return _maximumValue
        End Get
        Set(ByVal value As Decimal)
            _maximumValue = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.MaximumValue <> value Then
                Me.FormFieldRow.MaximumValue = value
            End If
        End Set
    End Property

    Public Property MinimumValue() As Decimal
        Get
            Return _minimumValue
        End Get
        Set(ByVal value As Decimal)
            _minimumValue = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.MinimumValue <> value Then
                Me.FormFieldRow.MinimumValue = value
            End If
        End Set
    End Property

    Public Property FormFieldRow() As FormFieldRow
        Get
            Return _FormFieldRow
        End Get
        Set(ByVal value As FormFieldRow)
            _FormFieldRow = value
        End Set
    End Property

    Public Property DecimalPlaces() As Byte
        Get
            Return _decimalPlaces
        End Get
        Set(ByVal value As Byte)
            _decimalPlaces = value
            Me.NumericUpDown1.DecimalPlaces = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.DecimalPlaces <> value Then
                Me.FormFieldRow.DecimalPlaces = value
            End If
        End Set
    End Property

    Public Property DisplayOrder() As Byte
        Get
            Return _sequence
        End Get
        Set(ByVal value As Byte)
            _sequence = value
            If Me._FormFieldRow IsNot Nothing AndAlso Me._FormFieldRow.DisplayOrder <> value Then
                Me._FormFieldRow.DisplayOrder = value
            End If
            If Me._sequence = 0 Then
                Me.BackColor = Color.Brown
                'Me.PictureBox1.BackColor = Me.BackColor
                Me.ComboBox1.Enabled = False
                Me.CheckBox1.Enabled = False
                Me.TextBox1.Enabled = False
                Me.NumericUpDown1.Enabled = False
                Me.DateTimePicker1.Enabled = False
                Application.DoEvents()
            Else
                If Me._pdaControlCollection IsNot Nothing Then
                    If Me._pdaControlCollection.SelectedItem Is Me Then
                        Me.BackColor = SystemColors.Info
                    Else
                        Me.BackColor = SystemColors.Control
                    End If
                    'Me.PictureBox1.BackColor = Color.Transparent
                End If
                Me.ComboBox1.Enabled = Me.ComboBox1.Visible AndAlso Me.LockCode = 0
                Me.CheckBox1.Enabled = Me.CheckBox1.Visible AndAlso Me.LockCode = 0
                Me.TextBox1.Enabled = Me.TextBox1.Visible AndAlso Me.LockCode = 0
                Me.NumericUpDown1.Enabled = Me.NumericUpDown1.Visible AndAlso Me.LockCode = 0
                Me.DateTimePicker1.Enabled = Me.DateTimePicker1.Visible AndAlso Me.LockCode = 0
            End If
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return _tableName
        End Get
        Set(ByVal value As String)
            _tableName = value
        End Set
    End Property

    Public Property ControlWidth() As Byte
        Get
            Return _Width
        End Get
        Set(ByVal value As Byte)
            _Width = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.Width <> value Then
                Me.FormFieldRow.Width = value
            End If
        End Set
    End Property

    Private Sub setControlType(ByVal value As ControlType)
        _controlType = value

        '_pdaCustomControlItem.FieldType = value
        Me.CheckBox1.Visible = False
        Me.ComboBox1.Visible = False
        Me.TextBox1.Visible = False
        Me.DateTimePicker1.Visible = False
        Me.NumericUpDown1.Visible = False
        Select Case value
            Case ControlType.CheckBox
                Me._visibleControl = Me.CheckBox1
            Case ControlType.DropDownList
                Me._visibleControl = Me.ComboBox1
            Case ControlType.Date
                Me._visibleControl = Me.DateTimePicker1
                Me.DateTimePicker1.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
                Me.DateTimePicker1.Value = Today
            Case ControlType.DateTime
                Me._visibleControl = Me.DateTimePicker1
                Me.DateTimePicker1.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " & System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern
                Me.DateTimePicker1.Value = Now
            Case ControlType.Number
                Me._visibleControl = Me.NumericUpDown1
            Case ControlType.TextBox
                Me._visibleControl = Me.TextBox1
            Case ControlType.Time
                Me._visibleControl = Me.DateTimePicker1
                Me.DateTimePicker1.CustomFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern
                Me.DateTimePicker1.Value = New Date(1900, 1, 1) + Now.TimeOfDay
        End Select
        Me._visibleControl.Visible = True
    End Sub

    Private Sub PdaControl_BackColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.BackColorChanged
        Me.LabelEditor.BackColor = Me.BackColor
        Me.CheckBox1.BackColor = Me.BackColor
        Me.PictureBox1.BackColor = Me.BackColor
        Application.DoEvents()
    End Sub

    Private Sub PdaControl_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        If _pdaControlCollection IsNot Nothing Then
            _pdaControlCollection.SelectedItem = Me
        End If
    End Sub

    Private Sub LabelEditor_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelEditor.TextChanged
        Label = Me.LabelEditor.Text
    End Sub

    Private Sub PdaControl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me._initialised Then
            Dim w As Integer = Me.GetWidth  ' ok, so 240 is a hack
            Dim h As Integer = Me.GetHeight
            Dim s As New Size(w, h)
            If Not s.Equals(Me.Size) Then
                Debug.Print(String.Format("PdaControl '{0}' Resized: {1}", Me.FieldName, Me.Size.ToString))
                Me.Size = s
                Debug.Print(String.Format("Reset to: {1}", Me.FieldName, Me.Size.ToString))
                Debug.Print(Environment.StackTrace())
            End If
            If Me.LabelPosition <> LabelPosition.Top Then
                Me.LabelEditor.Width = Me.ControlWidth \ 2
            End If
        End If
    End Sub

    Private _MouseDown As Boolean = False
    Private _Moving As Boolean = False
    Private Sub PdaControl_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, PictureBox1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left AndAlso Me._MouseDown Then
            Me._Moving = True
            Dim r As Rectangle = Me.Bounds
            If Not r.Contains(e.Location) Then
                DoDragDrop(Me, DragDropEffects.Move)
            End If
        End If
    End Sub

    Private Sub PdaControl_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Me._MouseDown = True
    End Sub

    Private Sub PdaControl_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me._Moving = False
            Me._MouseDown = False
        End If
    End Sub

    Private Sub PdaControl_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        Debug.Print(String.Format("PdaControl '{0}' Moved. New Location: {1}", Me.FieldName, Me.Location))
        Debug.Print(Environment.StackTrace())
    End Sub

    Private _sortPriority As Short
    Public Property SortPriority() As Short
        Get
            Return _sortPriority
        End Get
        Set(ByVal value As Short)
            _sortPriority = value
            If Me.FormFieldRow IsNot Nothing AndAlso Me.FormFieldRow.SortPriority <> value Then
                Me.FormFieldRow.SortPriority = value
            End If
        End Set
    End Property

    Private Sub Splitter1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles Splitter1.SplitterMoved
        If Me._FormFieldRow IsNot Nothing Then
            Me._FormFieldRow.LabelWidth = CByte(((e.X - Me.PictureBox1.Width) * 100) \ 240)
        End If
    End Sub
End Class
