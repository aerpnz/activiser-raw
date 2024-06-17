Imports System.Collections.Generic
Imports activiser.Library
Imports activiser.Library.WebService.FormDefinition

'Imports activiser.NativeMethods.SystemInformation

Public Class CustomFormPanel
    Private Const MODULENAME As String = "CustomForm"
    Private Const CREATEDDATETIME As String = "CreatedDateTime"
    Private Const MODIFIEDDATETIME As String = "ModifiedDateTime"
    Private Const RES_CustomFormRow As String = "customFormRow"
    Private Const RES_NullDataError As String = "NullDataError"
    Private Const RES_SaveError As String = "SaveError"
    Private Const RES_RequiredDataMissing As String = "RequiredDataMissing"
    Private Const RES_RequiredDataMissingDetails As String = "RequiredDataMissingDetails"
    Private Const RES_ConfirmDelete As String = "ConfirmDelete"
    Private Const RES_ConfirmCancelInsert As String = "ConfirmCancelInsert"
    Private Const STR_TableNotFoundTemplate As String = "TableNotFoundTemplate"
    Private Const STR_PrimaryKeyNotGuid As String = "PrimaryKeyNotGuid"
    Private Const STR_ForeignKeyNotGuid As String = "ForeignKeyNotGuid"
    Private Const STR_SortPriority As String = "SortPriority"
    Private Const STR_Sequence As String = "Sequence"
    Private Const STR_DefinitionError As String = "DefinitionError"

    Private fontSize As Integer = AppConfig.GetSetting(My.Resources.AppConfigTextSizeKey, 8)
    Private fieldFont As Font

    Private WithEvents BindingSource1 As System.Windows.Forms.BindingSource

    Private _datatable As DataTable
    Private _currentRows As New Generic.List(Of DataRow)
    Private _currentRow As DataRow
    Private _customForm As WebService.FormDefinition.FormRow
    Private _parentGuid As Guid
    Private _pkColumn As DataColumn
    Private _fkColumn As DataColumn
    Private sortColumns As New List(Of String)
    Private sortOrder As String

    Private controlList As New List(Of CustomControl)

    'Private origValues As New List(Of DataRow)
    'Private newValues As New List(Of DataRow)
    'Private modifiedValues As New List(Of DataRow)

    Private _parentForm As Form

    Public Property ParentForm() As Form
        Get
            Return _parentForm
        End Get
        Set(ByVal value As Form)
            _parentForm = value
        End Set
    End Property

    Public Sub ArrangeItems()
        If controlList Is Nothing OrElse controlList.Count = 0 Then Return
        If Not Me._initialised Then Return

        Dim pc As CustomControl
        Dim nextPc As CustomControl
        Dim lastPc As CustomControl
        Dim t As Integer
        Dim l As Integer
        Dim w As Integer
        Dim h As Integer
        Dim g As Integer = (CInt(RunTimeScale) + 1) * 2 ' gap

        Dim topIndex As Integer = controlList.Count - 1
        Me.SuspendLayout()
        Me.CustomControlPanel.AutoScroll = False ' turn off autoscroll, to reset clientsize.


        For i As Integer = 0 To topIndex
            pc = controlList(i)
            If pc Is Nothing Then Continue For ' deal with hidden controls
            If i < topIndex Then
                nextPc = controlList(i + 1)
            Else
                nextPc = Nothing
            End If
            If i > 0 Then
                lastPc = controlList(i - 1)
            Else
                lastPc = Nothing
            End If
            h = pc.GetHeight
            w = pc.GetWidth

            If pc.ControlPosition = ControlPosition.Right Then
                l = Me.ClientSize.Width - w - GetSystemMetrics(SM_CXVSCROLL)
            Else
                l = 0
            End If
            pc.SuspendLayout()
            pc.Location = New Point(l, t)
            pc.Size = New Size(w, h)
            pc.ResumeLayout()

            If nextPc Is Nothing Then Exit For
            If Not ((pc.ControlPosition = ControlPosition.Left) AndAlso (nextPc.ControlPosition = ControlPosition.Right) AndAlso (nextPc.WidthPercent + pc.WidthPercent <= 100)) Then
                If lastPc IsNot Nothing Then
                    If (pc.ControlPosition = ControlPosition.Right) AndAlso (lastPc.ControlPosition = ControlPosition.Left) AndAlso (lastPc.WidthPercent + pc.WidthPercent <= 100) Then
                        If pc.Height < lastPc.Height Then
                            h = lastPc.Height
                        End If
                    End If
                End If
                t += h + g
            End If
        Next
        Me.CustomControlPanel.AutoScroll = True
        Me.ResumeLayout()
    End Sub

    Private _initialised As Boolean

    Private Sub LoadFields(ByVal customFormRow As WebService.FormDefinition.FormRow)
        Dim ffrs As New Generic.SortedList(Of Integer, FormFieldRow)
        For Each ffr As FormFieldRow In customFormRow.GetFormFieldRows
            If ffr.DisplayOrder <> 0 Then
                ffrs.Add(ffr.DisplayOrder, ffr)
            End If
        Next

        For Each ffr As FormFieldRow In ffrs.Values
            Dim uc As New CustomControl(ffr, Me._datatable, Me, fieldFont)
            uc.Font = fieldFont
            controlList.Add(uc) ' = uc
            uc.DataBindings.Add("Value", Me.BindingSource1, ffr.AttributeName)
            uc.Enabled = False
            AddHandler uc.ValueChanged, AddressOf CustomControl_ValueChanged
        Next
    End Sub

    Private Sub SetupBindingSource(ByVal customFormRow As WebService.FormDefinition.FormRow)

        Dim sffrs As New Generic.SortedList(Of Integer, FormFieldRow)

        For Each ffr As FormFieldRow In customFormRow.GetFormFieldRows()
            If ffr.SortPriority <> 0 And sortColumns.Contains(ffr.AttributeName) Then
                sffrs.Add(ffr.SortPriority, ffr)
            End If
        Next
        For Each ffr As FormFieldRow In sffrs.Values
            sortColumns.Add(ffr.AttributeName)
        Next
        sortOrder = String.Join(",", sortColumns.ToArray)

        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingSource1.DataSource = gClientDataSet
        Me.BindingSource1.DataMember = Me._datatable.TableName
        Me.BindingSource1.Filter = "1=2"
        Me.BindingSource1.Sort = sortOrder
    End Sub


    Private Sub SetupBindingNavigator()
        If Me.CustomForm.EntityPK = Me.CustomForm.EntityParentFK Then ' 1-1 relationship
            Me._maxChildren = 1
            Me.BindingNavigator1.Visible = False
        Else
            Me._maxChildren = Me.CustomForm.MaxItems
            Me.BindingNavigator1.Visible = True
        End If
    End Sub

    Public Sub New(ByVal customFormRow As WebService.FormDefinition.FormRow, ByVal parent As Panel, ByVal ownerForm As Form)
        Const METHODNAME As String = "New"
        If customFormRow Is Nothing Then
            Throw New ArgumentNullException("customFormRow")
        End If
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.SuspendLayout()
        Try
            parent.Controls.Add(Me)
            Me.ParentForm = ownerForm
            Me.LoadNavBarButtonImages(False, True)
            Me._customForm = customFormRow
            If Not gClientDataSet.Tables.Contains(customFormRow.EntityName) Then
                Terminology.DisplayFormattedMessage(ownerForm, MODULENAME, STR_TableNotFoundTemplate, MessageBoxIcon.Exclamation, customFormRow.EntityName)
                Me._customForm = Nothing
                Return
            End If

            Me._datatable = gClientDataSet.Tables(customFormRow.EntityName)
            Me._pkColumn = Me._datatable.Columns(customFormRow.EntityPK)
            Me._fkColumn = Me._datatable.Columns(customFormRow.EntityParentFK)
            If Me._pkColumn.DataType IsNot GetType(Guid) Then
                Throw New ArgumentOutOfRangeException(RES_CustomFormRow, Terminology.GetString(MODULENAME, STR_PrimaryKeyNotGuid))
            End If

            If Me._fkColumn.DataType IsNot GetType(Guid) Then
                Throw New ArgumentOutOfRangeException(RES_CustomFormRow, Terminology.GetString(MODULENAME, STR_ForeignKeyNotGuid))
            End If

            SetupBindingNavigator()

            SetupBindingSource(customFormRow)

            fieldFont = New Font(Me.Font.Name, fontSize, FontStyle.Regular)
            LoadFields(customFormRow)

            _initialised = True
            Me.ArrangeItems()
            Me.ResumeLayout()

        Catch ex As Exception
            Debug.WriteLine(New ExceptionParser(ex).ToString)
            LogError(MODULENAME, METHODNAME, ex, True, Terminology.GetFormattedString(MODULENAME, STR_DefinitionError, ex.Message, ex.ToString()))
        Finally
            Me.ResumeLayout()
            RefreshControl(Me.CustomControlPanel)

        End Try
        Me.Refresh()
    End Sub

    Private Sub LoadNavBarButtonImages(Optional ByVal editButtonsOnly As Boolean = False, Optional ByVal setReadOnly As Boolean = False)
        If Me.BindingNavigator1.Height <> 24 Then ' bigger buttons required - designtime is 24, bigger means it's been zoomed by .NetCF
            If Not editButtonsOnly Then
                Me.MoveFirstButton.Image = My.Resources.MoveFirst32
                Me.MovePreviousButton.Image = My.Resources.MovePrevious32
                Me.MoveNextButton.Image = My.Resources.MoveNext32
                Me.MoveLastButton.Image = My.Resources.MoveLast32
            End If
            If setReadOnly Then
                Me.DeleteRecordButton.Image = My.Resources.Delete32disabled
                Me.AddRecordButton.Image = My.Resources.Add32disabled
            Else
                Me.DeleteRecordButton.Image = My.Resources.Delete32
                Me.AddRecordButton.Image = My.Resources.Add32
            End If
        Else
            If Not editButtonsOnly Then
                Me.MoveFirstButton.Image = My.Resources.MoveFirst16
                Me.MovePreviousButton.Image = My.Resources.MovePrevious16
                Me.MoveNextButton.Image = My.Resources.MoveNext16
                Me.MoveLastButton.Image = My.Resources.MoveLast16
            End If
            If setReadOnly Then
                Me.DeleteRecordButton.Image = My.Resources.Delete16disabled
                Me.AddRecordButton.Image = My.Resources.Add16disabled
            Else
                Me.DeleteRecordButton.Image = My.Resources.Delete16
                Me.AddRecordButton.Image = My.Resources.Add16
            End If
        End If
    End Sub

    Public ReadOnly Property CustomForm() As WebService.FormDefinition.FormRow
        Get
            Return _customForm
        End Get
    End Property

    Private Sub CustomFormPanel_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.LostFocus
        'Debug.WriteLine(String.Format("CustomFormPanel {0} lost focus", Me.CustomForm.FormName))
        Debug.Write("CustomFormPanel ")
        If Me._customForm IsNot Nothing Then
            Debug.Write(" '" & Me._customForm.FormName & "' ")
            Me.EndEdit()
        End If
        Debug.WriteLine("Lost Focus")
    End Sub

    Private Sub CustomFormPanel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ArrangeItems()
    End Sub

    Public Sub SetParentFilter(ByVal parentId As Guid)
        If _parentGuid.Equals(parentId) Then Return
        _parentGuid = parentId
        Me.BindingSource1.Filter = String.Format(WithoutCulture, "{0}='{1}'", Me._fkColumn.ColumnName, parentId.ToString)
        Me._currentRows.Clear()
        Me._currentRows.AddRange(Me._datatable.Select(String.Format(WithoutCulture, "{0}='{1}'", Me._fkColumn.ColumnName, parentId.ToString)))
        Debug.WriteLine(Me.BindingSource1.Filter)
        Debug.WriteLine(Me.BindingSource1.Count)
        If Me.MaximumChildren = 1 AndAlso Not Me.ReadOnly Then
            If Me.BindingSource1.Count = 0 Then
                If Not Me.BindingSource1.AllowNew Then
                    Me.BindingSource1.AllowNew = True ' Hack: may have been disabled because of 'ReadOnly' option.
                End If
                Me.BindingSource1.AddNew()
                Me.BindingSource1.MoveLast()
            End If
        ElseIf Me.MaximumChildren <> 1 Then
            Me.RefreshNavBar()
        End If
        _haveChanges = False
    End Sub

    Private Sub MoveFirstButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveFirstButton.Click
        If Me.EndEdit() Then Me.BindingSource1.MoveFirst()
    End Sub

    Private Sub MovePreviousButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovePreviousButton.Click
        If Me.EndEdit() Then Me.BindingSource1.MovePrevious()
    End Sub

    Private Sub MoveNextButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MoveNextButton.Click
        If Me.EndEdit() Then Me.BindingSource1.MoveNext()
    End Sub

    Private Sub MoveLastButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MoveLastButton.Click
        If Me.EndEdit() Then Me.BindingSource1.MoveLast()
    End Sub

    Private Function EndEdit(Optional ByVal suppressErrors As Boolean = False) As Boolean
        Dim result As Boolean

        Try
            If Me.ReadOnly Then
                Try
                    Me.BindingSource1.CancelEdit()
                Catch ex As Exception

                End Try
                Return True
            End If
            If _currentRow Is Nothing Then
                result = True
            Else
                'If Me.MoveLastButton.Visible Then Me.MoveLastButton.Focus()
                If _currentRow.IsNull(CREATEDDATETIME) Then
                    _currentRow(CREATEDDATETIME) = DateTime.UtcNow
                End If
                Dim badFields As New Generic.List(Of String)
                For Each uc As CustomControl In Me.controlList
                    If uc.ReadOnly Then Continue For
                    If Me._datatable.Columns(uc.FieldName).AllowDBNull Then
                        If Convert.IsDBNull(uc.Value) Then
                            If Not _currentRow.IsNull(uc.FieldName) Then
                                _currentRow(uc.FieldName) = Convert.DBNull
                            End If
                        Else
                            If Not _currentRow.Item(uc.FieldName).Equals(uc.Value) Then
                                _currentRow(uc.FieldName) = uc.Value
                            End If
                        End If
                    Else
                        If (_currentRow.IsNull(uc.FieldName)) _
                            OrElse ((Me._datatable.Columns(uc.FieldName).DataType Is GetType(String)) _
                            AndAlso String.IsNullOrEmpty(CStr(_currentRow(uc.FieldName)).Trim)) Then
                            badFields.Add(uc.Caption.Text)
                        End If
                    End If
                Next
                If badFields.Count <> 0 Then
                    If Not suppressErrors Then
                        Dim badFieldList As String = String.Join(", ", badFields.ToArray())
                        Dim messageDetail As String = Terminology.GetFormattedString(MODULENAME, RES_RequiredDataMissingDetails, badFieldList)
                        Terminology.DisplayFormattedMessage(Me.ParentForm, MODULENAME, RES_RequiredDataMissing, MessageBoxIcon.Hand, Me.CustomForm.FormName, messageDetail)
                    End If
                    result = False
                    Exit Try
                End If
                Me._currentRow.EndEdit()
                Me.BindingSource1.EndEdit() ' add/update record to binding source

                result = True
            End If
        Catch ex As NoNullAllowedException
            If Not suppressErrors Then
                Terminology.DisplayMessage(Me.ParentForm, MODULENAME, RES_NullDataError, MessageBoxIcon.Exclamation)
            End If
            result = False
        Catch ex As Exception
            If Not suppressErrors Then
                Terminology.DisplayMessage(Me.ParentForm, MODULENAME, RES_SaveError, MessageBoxIcon.Exclamation)
            End If
            result = False
        Finally
            If result Then
                Me._haveChanges = False
            End If
        End Try
        Return result
    End Function

    Private Sub AddRecordButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddRecordButton.Click
        Try
            If Me.EndEdit() Then
                If Me._maxChildren = 0 OrElse Me.BindingSource1.Count < Me._maxChildren Then
                    Dim newObject As Object = Me.BindingSource1.AddNew()
                    Dim dr As DataRowView = TryCast(newObject, DataRowView)
                    If dr IsNot Nothing Then
                        If _pkColumn.ColumnName <> _fkColumn.ColumnName Then dr.Row(_pkColumn) = Guid.NewGuid
                        dr.Row(_fkColumn) = _parentGuid
                        dr.Row(CREATEDDATETIME) = DateTime.UtcNow
                        dr.Row(MODIFIEDDATETIME) = DateTime.UtcNow
                        ' set default values
                        For Each cc As CustomControl In Me.controlList
                            Select Case cc.ControlType
                                Case ControlType.CheckBox
                                    dr.Row.Item(cc.FieldName) = False
                                    cc.Value = False
                                Case ControlType.Date
                                    dr.Row.Item(cc.FieldName) = DateTime.Today
                                    cc.Value = DateTime.Today
                                Case ControlType.DateTime
                                    dr.Row.Item(cc.FieldName) = DateTime.Now
                                    cc.Value = DateTime.Now
                                Case ControlType.List
                                    'If Not cc.ReadOnly Then
                                    '    dr.Row.Item(cc.FieldName) = cc.DefaultListValue
                                    '    'cc.Value = cc.DefaultListValue
                                    'End If
                                Case ControlType.Number
                                    dr.Row.Item(cc.FieldName) = 0
                                    cc.Value = 0
                                Case ControlType.TextBox
                                    dr.Row.Item(cc.FieldName) = String.Empty
                                    cc.Value = String.Empty
                                Case ControlType.Time
                                    dr.Row.Item(cc.FieldName) = DateTime.Now.TimeOfDay
                                    cc.Value = DateTime.Now.TimeOfDay
                                Case ControlType.Undefined
                                    dr.Row.Item(cc.FieldName) = Nothing
                                    cc.Value = DBNull.Value
                            End Select
                            'cc.SetDefaultValue()
                        Next
                    End If
                    Me.BindingSource1.Position = Me.BindingSource1.IndexOf(newObject) '.MoveLast()
                    Me.RefreshNavBar()
                End If
            End If
            ' Me.BindingSource1.MoveLast()
        Catch ex As Exception
            LogError(MODULENAME, "AddRecord", ex, True, RES_UnableToAddRecord)
        End Try
    End Sub

    Private _deletingRow As Boolean

    Private Sub DeleteRecordButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteRecordButton.Click
        ' somehow hopper managed to click 'delete' before it got disabled!
        If Me.BindingSource1.Current IsNot Nothing Then
            _deletingRow = True
            Try
                Dim previousPosition As Integer = Me.BindingSource1.Position
                If _currentRow Is Nothing Then
                    If Me.BindingSource1.Current IsNot Nothing Then
                        Dim drv As DataRowView = TryCast(Me.BindingSource1.Current, DataRowView)
                        _currentRow = drv.Row
                    End If
                End If
                Dim victimRow As DataRow = _currentRow
                If victimRow.RowState = DataRowState.Detached Then ' a new row
                    If Terminology.AskFormattedQuestion(Me.ParentForm, MODULENAME, RES_ConfirmCancelInsert, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, Me._customForm.FormName) = DialogResult.Yes Then
                        Try
                            victimRow.CancelEdit()
                            Me.BindingSource1.CancelEdit()
                            _currentRow = Nothing
                            'victimRow.RejectChanges()
                            ' victimRow.Delete()
                        Catch ex As Exception
                            LogError(MODULENAME, "DeleteRecord", ex, False, Nothing)
                        End Try
                    End If
                Else
                    If Terminology.AskFormattedQuestion(Me.ParentForm, MODULENAME, RES_ConfirmDelete, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, Me._customForm.FormName) = DialogResult.Yes Then
                        Try
                            victimRow.CancelEdit()
                            If victimRow.RowState = DataRowState.Added Then
                                victimRow.RejectChanges()
                            ElseIf victimRow.RowState = DataRowState.Modified Then
                                victimRow.RejectChanges()
                                victimRow.Delete()
                            End If
                            _currentRow = Nothing
                            'victimRow.CancelEdit()
                            'victimRow.RejectChanges()
                            'victimRow.Delete()
                            'If Me.BindingSource1.Current IsNot Nothing Then Me.BindingSource1.RemoveCurrent()
                        Catch ex As Exception
                            LogError(MODULENAME, "DeleteRecord", ex, False, Nothing)
                        End Try
                    End If
                End If
                If Me.BindingSource1.Count = 0 Then
                    Me.BindingSource1.Position = -1
                ElseIf (previousPosition >= Me.BindingSource1.Count) Then
                    Me.BindingSource1.Position = Me.BindingSource1.Count - 1
                Else
                    Me.BindingSource1.Position = previousPosition
                End If
            Catch ex As Exception
                LogError(Me.CustomForm.FormName, "DeleteRecord", ex, True, RES_DeleteError)
            Finally
                _deletingRow = False
                RefreshNavBar()
            End Try
        End If
    End Sub

    Public Function Save() As Boolean
        Return Me.EndEdit()
    End Function

    'Public Sub Close()
    '    Me.EndEdit()
    '    For Each dr As DataRow In Me._currentRows
    '        Me._datatable.ImportRow(dr)
    '    Next
    'End Sub
    Public Sub Cancel()
        Me.BindingSource1.CancelEdit()
        For Each drv As DataRowView In Me.BindingSource1
            If Me._currentRows.IndexOf(drv.Row) = -1 Then
                drv.Row.Delete()
            End If
        Next
        'RCP 2007-11-08: Que? why are we importing rows on a cancel ?
        ' because we're restoring the previous state.
        Try
            For Each dr As DataRow In Me._currentRows
                Me._datatable.ImportRow(dr)
            Next
        Catch ex As Exception
            LogError(MODULENAME, "Cancel", ex, False, Nothing)
        End Try
    End Sub

    Private _readOnly As Boolean
    Public Property [ReadOnly]() As Boolean
        Get
            Return _readOnly
        End Get
        Set(ByVal value As Boolean)
            _readOnly = value
            If _readOnly Then
                Me.AddRecordButton.Enabled = False
                Me.DeleteRecordButton.Enabled = False
                For Each uc As CustomControl In Me.controlList
                    uc.Enabled = False
                Next
                If Me.MaximumChildren <> 1 Then
                    Me.BindingSource1.AllowNew = False
                End If
                'Me.BindingSource1.RaiseListChangedEvents = False
            Else
                Me.AddRecordButton.Enabled = True
                Me.DeleteRecordButton.Enabled = True
                For Each uc As CustomControl In Me.controlList
                    uc.Enabled = _currentRow IsNot Nothing ' True
                Next
                Me.BindingSource1.AllowNew = True
                'Me.BindingSource1.RaiseListChangedEvents = True
            End If
            Me.LoadNavBarButtonImages(True, _readOnly)
        End Set
    End Property

    Private Delegate Sub SetChildLockDelegate(ByVal lockCode As LockCodes)

    Public Sub SetChildLock(ByVal lockCode As LockCodes)
        If Me.InvokeRequired Then
            Dim d As New SetChildLockDelegate(AddressOf SetChildLock)
            Me.Invoke(d, lockCode)
            Return
        End If

        For Each uc As CustomControl In Me.controlList
            If (uc.CustomControlRow.LockCode And lockCode) <> 0 Then
                uc.Enabled = False
            End If
        Next
    End Sub

    Public Sub UnSetChildLock(ByVal lockCode As LockCodes)
        If Me.InvokeRequired Then
            Dim d As New SetChildLockDelegate(AddressOf UnSetChildLock)
            Me.Invoke(d, lockCode)
            Return
        End If

        For Each uc As CustomControl In Me.controlList
            If (uc.CustomControlRow.LockCode And lockCode) <> 0 Then
                uc.Enabled = True
            End If
        Next
    End Sub

    Private _haveChanges As Boolean
    Public ReadOnly Property HasChanges() As Boolean
        Get
            Return _haveChanges
        End Get
    End Property

    Private _maxChildren As Integer = AppConfig.GetSetting("MaximumCustomRecordsPerParent", 20)
    Public Property MaximumChildren() As Integer
        Get
            Return _maxChildren
        End Get
        Set(ByVal value As Integer)
            _maxChildren = value
        End Set
    End Property

    Private Sub RefreshNavBar()
        If Me._maxChildren = 1 Then Exit Sub
        Dim p As Integer = Me.BindingSource1.Position
        Dim c As Integer = Me.BindingSource1.Count
        If c > 0 Then
            Me.MoveLastButton.Enabled = p < c - 1
            Me.MoveFirstButton.Enabled = p > 0
            Me.MoveNextButton.Enabled = p < c - 1
            Me.MovePreviousButton.Enabled = p > 0
            Me.DeleteRecordButton.Enabled = Not Me.ReadOnly AndAlso (p < c)
            Me.AddRecordButton.Enabled = Not Me.ReadOnly AndAlso ((Me.MaximumChildren <= 0) OrElse (c < Me.MaximumChildren))
        Else
            Me.MoveLastButton.Enabled = False
            Me.MoveFirstButton.Enabled = False
            Me.MoveNextButton.Enabled = False
            Me.MovePreviousButton.Enabled = False
            Me.DeleteRecordButton.Enabled = False
            Me.AddRecordButton.Enabled = Not Me.ReadOnly AndAlso Not Me._parentGuid.Equals(Guid.Empty)
        End If
        Me.RecordLocationLabel.Text = String.Format(WithCulture, My.Resources.CustomFormRecordLocationFormat, Me.BindingSource1.Position + 1, Me.BindingSource1.Count)
    End Sub

    Private Sub BindingSource1_BindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles BindingSource1.BindingComplete
        If Not e.BindingCompleteState = BindingCompleteState.Success Then Exit Sub

        If e.BindingCompleteContext = BindingCompleteContext.DataSourceUpdate Then
            RefreshNavBar()
        Else
            'If e.Binding.Control.Focused And Not Me.ReadOnly Then
            '    Me._haveChanges = True
            'End If
        End If
    End Sub

#Region "Thread safety delegates"
    Private Delegate Sub RefreshControlCallBack(ByVal control As Control)
    Private Sub RefreshControl(ByVal control As Control)
        If control.InvokeRequired Then
            Dim d As New RefreshControlCallBack(AddressOf RefreshControl)
            Me.Invoke(d, New Object() {control})
        Else
            control.Refresh()
        End If
    End Sub

    Private Delegate Sub ChangeControlEnabledStateCallBack(ByVal control As Control, ByVal enabled As Boolean)
    Private Sub ChangeControlEnabledState(ByVal control As Control, ByVal enabled As Boolean)
        If control.InvokeRequired Then
            Dim d As New ChangeControlEnabledStateCallBack(AddressOf ChangeControlEnabledState)
            Me.Invoke(d, New Object() {control, enabled})
        Else
            control.Enabled = enabled
        End If
    End Sub
#End Region

    Private _inControlValueChanged As Boolean
    Private Sub CustomControl_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        Const METHODNAME As String = "CustomControl_ValueChanged"
        If _inControlValueChanged Then Exit Sub
        Dim cc As CustomControl = TryCast(sender, CustomControl)
        If cc IsNot Nothing Then
            If _currentRow IsNot Nothing Then
                Try
                    _inControlValueChanged = True
                    Select Case cc.ControlType
                        Case ControlType.CheckBox
                            If cc.CustomControlRow.Lines = 2 Then ' allow tri-state, yes, no, don't know.
                                If cc.CheckBox1.CheckState = CheckState.Indeterminate Then
                                    _currentRow(cc.CustomControlRow.AttributeName) = Convert.DBNull
                                Else
                                    _currentRow(cc.CustomControlRow.AttributeName) = cc.CheckBox1.Checked
                                End If
                            Else
                                _currentRow(cc.CustomControlRow.AttributeName) = cc.CheckBox1.Checked
                            End If
                        Case ControlType.Date, ControlType.DateTime, ControlType.Time
                            _currentRow(cc.CustomControlRow.AttributeName) = cc.Value
                        Case ControlType.List
                            If cc.ComboBox1.SelectedIndex = -1 Then
                                _currentRow(cc.CustomControlRow.AttributeName) = Convert.DBNull
                            Else
                                _currentRow(cc.CustomControlRow.AttributeName) = cc.Value
                            End If
                            ' sync list boxes based on same data
                            For Each c As CustomControl In Me.controlList
                                If c.ControlType = ControlType.List Then
                                    If c Is cc Then Continue For
                                    If c.FieldName = cc.FieldName AndAlso c.ListDataSource = cc.ListDataSource AndAlso c.ListValueColumn = cc.ListValueColumn Then
                                        c.Value = cc.Value
                                    End If
                                End If
                            Next
                        Case ControlType.Number
                            _currentRow(cc.CustomControlRow.AttributeName) = cc.Value
                        Case ControlType.TextBox
                            If CStr(cc.Value).Trim = String.Empty Then
                                _currentRow(cc.CustomControlRow.AttributeName) = Convert.DBNull
                            Else
                                _currentRow(cc.CustomControlRow.AttributeName) = cc.Value
                            End If
                    End Select
                    _currentRow(MODIFIEDDATETIME) = DateTime.UtcNow
                    If _currentRow.IsNull(CREATEDDATETIME) Then
                        _currentRow(CREATEDDATETIME) = DateTime.UtcNow
                    End If

                    If Not Me._datatable.Columns(cc.FieldName).AllowDBNull AndAlso _currentRow.IsNull(cc.FieldName) Then
                        Debug.WriteLine("Current non-nullable field is null")
                    Else
                        If Not cc.ControlType = ControlType.TextBox Then
                            _currentRow.EndEdit()
                            '    Me.EndEdit(True) ' HACK: 2007-08-02, stuff not saving ?!?!?!?GRRRRRR
                        End If
                    End If
                Catch ex As ConstraintException
                    Terminology.DisplayMessage(Me.ParentForm, MODULENAME, "DuplicateRecord", MessageBoxIcon.Exclamation)
                    _currentRow.CancelEdit()
                    _currentRow.RejectChanges()
                Catch ex As Exception
                    LogError(Me.CustomForm.FormName, METHODNAME, ex, True, Nothing)
                Finally
                    Me._haveChanges = True
                    _inControlValueChanged = False
                End Try
            End If

        End If
    End Sub

    Private Sub BindingSource1_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingSource1.CurrentChanged
        If _deletingRow Then Return
        'If Me._haveChanges Then
        '    Me.EndEdit()
        'End If
    End Sub

    Private Sub BindingSource1_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingSource1.PositionChanged
        Const METHODNAME As String = "BindingSource1_PositionChanged"
        If Me.BindingSource1.Position <> -1 Then
            Dim drv As DataRowView = TryCast(Me.BindingSource1.Current, DataRowView)
            If drv IsNot Nothing Then
                Try
                    _currentRow = drv.Row
                    If _currentRow.IsNull(Me._pkColumn) Then ' new row
                        If Me._fkColumn.ColumnName = Me._pkColumn.ColumnName Then
                            _currentRow(Me._pkColumn) = _parentGuid
                        Else
                            _currentRow(Me._pkColumn) = Guid.NewGuid
                        End If
                        _currentRow(CREATEDDATETIME) = DateTime.UtcNow
                    End If
                    If _currentRow.IsNull(Me._fkColumn) Then
                        _currentRow(Me._fkColumn) = _parentGuid
                    End If
                    _currentRow(MODIFIEDDATETIME) = DateTime.UtcNow

                    For Each c As Control In Me.controlList
                        ChangeControlEnabledState(c, Not Me.ReadOnly)
                    Next
                Catch ex As Exception
                    LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingRecord)
                End Try
            Else
                _currentRow = Nothing

                For Each c As Control In Me.controlList
                    ChangeControlEnabledState(c, False)
                Next
            End If
        Else
            _currentRow = Nothing
            For Each c As Control In Me.controlList
                ChangeControlEnabledState(c, False)
            Next
        End If
        RefreshNavBar()
    End Sub

    'Private Sub CustomFormPanel_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.LostFocus
    '    Debug.Write("CustomFormPanel ")
    '    If Me._customForm IsNot Nothing Then
    '        Debug.Write(" '" & Me._customForm.FormName & "' ")
    '    End If
    '    Debug.WriteLine("Lost Focus")
    'End Sub

    Private Sub CustomControlPanel_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CustomControlPanel.LostFocus
        Debug.Write("CustomControlPanel ")
        If Me._customForm IsNot Nothing Then
            Debug.Write(" '" & Me._customForm.FormName & "' ")
        End If
        Debug.WriteLine("Lost Focus")
    End Sub
End Class
