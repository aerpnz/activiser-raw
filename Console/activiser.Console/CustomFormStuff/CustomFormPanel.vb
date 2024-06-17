Imports System.Collections.Generic
Imports activiser.Library.activiserWebService
Imports activiser.Library.activiserWebService.FormDefinition

Public Class CustomFormPanel
    Private Const MODULENAME As String = "CustomForm"
    Private Const CREATEDDATETIME As String = "CreatedDateTime"
    Private Const MODIFIEDDATETIME As String = "ModifiedDateTime"
    Private Const RES_CustomFormRow As String = "customFormRow"
    Private Const RES_NullDataError As String = "NullDataError"
    Private Const RES_SaveError As String = "SaveError"
    Private Const RES_RequiredDataMissing As String = "RequiredDataMissing"
    Private Const RES_RequiredDataMissingDetails As String = "RequiredDataMissingDetails"
    Private Const RES_UndoChangesToCustomData As String = "UndoChangesToCustomData"
    Private Const RES_UndoChangesToConfirmDelete As String = "DeleteConfirmation"
    Private Const RES_UnableToSaveCustomForm As String = "UnableToSaveCustomForm"

    Private Shared myScale As Double = 1 'MyDpiX / DesignDpi

    Private _datatable As DataTable
    Private _parentRow As DataRow
    Private _currentRows As New Generic.List(Of DataRow)
    Private _currentRow As DataRow
    Private _customForm As FormDefinition.FormRow
    Private _parentGuid As Guid
    Private _pkColumn As DataColumn
    Private _fkColumn As DataColumn
    Private sortColumns As New List(Of String)
    Private sortOrder As String

    Private _allowDeleteCurrent As Boolean

    Private controlList As New List(Of CustomControl)

    Private WithEvents _consoleData As New ConsoleData(MODULENAME)
    Private _refreshing As Boolean

    'HACK: need to save a new record before we can delete it?
    'Private _deletingRecord As Boolean

    Private _validState As Boolean
    Public Property FormDefinitionValid() As Boolean
        Get
            Return _validState
        End Get
        Private Set(ByVal validState As Boolean)
            _validState = validState
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
        Dim g As Integer = (CInt(myScale) + 1) * 2 ' gap

        Dim topIndex As Integer = controlList.Count - 1

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
                l = Me.CustomControlPanel.ClientSize.Width - (w + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth)
            Else
                l = 0
            End If
            pc.Location = New Point(l, t)
            pc.Size = New Size(w, h)

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
            'Application.DoEvents()
            Me.Refresh()
        Next
    End Sub

    Private _initialised As Boolean
    Public Sub New(ByVal customFormRow As FormDefinition.FormRow)
        If customFormRow Is Nothing Then
            Throw New ArgumentNullException("customFormRow")
        End If
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _consoleData = New ConsoleData(String.Format("Custom Form for {0}/{1}", customFormRow.ParentEntityName, customFormRow.EntityName))

        Me.SuspendLayout()
        Try
            'parent.Controls.Add(Me)
            Me._customForm = customFormRow
            Me._datatable = ConsoleData.CoreDataSet.Tables(customFormRow.EntityName)
            If Me._datatable Is Nothing Then
                Terminology.DisplayFormattedMessage(MODULENAME, "TableNotFoundTemplate", MessageBoxIcon.Warning, customFormRow.EntityName)
                Me._customForm = Nothing
                Return
            End If
            Me._pkColumn = Me._datatable.Columns(customFormRow.EntityPK)
            Me._fkColumn = Me._datatable.Columns(customFormRow.EntityParentFK)
            If Me._pkColumn.DataType IsNot GetType(Guid) Then
                Throw New ArgumentOutOfRangeException(RES_CustomFormRow, My.Resources.CustomFormPrimaryKeyNotGuid)
            End If

            If Me._fkColumn.DataType IsNot GetType(Guid) Then
                Throw New ArgumentOutOfRangeException(RES_CustomFormRow, My.Resources.CustomFormForeignKeyNotGuid)
            End If

            If Me.CustomForm.EntityPK = customFormRow.EntityParentFK Then ' 1-1 relationship
                Me._maxChildren = 1
                'Me.BindingNavigator1.Visible = False
            Else
                Me._maxChildren = customFormRow.MaxItems
                'Me.BindingNavigator1.Visible = True
            End If
            Me.BindingSource1.DataSource = Console.ConsoleData.CoreDataSet
            Me.BindingSource1.DataMember = customFormRow.EntityName
            Me.BindingSource1.Filter = "1=2"

            For Each ccr As FormDefinition.FormFieldRow In CType(SortDataRowArray(customFormRow.GetFormFieldRows, "SortPriority"), FormDefinition.FormFieldRow())
                If ccr.SortPriority > 0 AndAlso Not sortColumns.Contains(ccr.AttributeName) Then
                    sortColumns.Add(ccr.AttributeName)
                End If
            Next
            sortOrder = String.Join(",", sortColumns.ToArray)
            Me.BindingSource1.Sort = sortOrder

            Dim ccrs = _
                From ffr As FormFieldRow In customFormRow.GetFormFieldRows _
                Where ffr.LockCode <> LockCodes.Hidden _
                Order By ffr.DisplayOrder, ffr.Label, ffr.AttributeName

            controlList.Clear()

            For Each ccr In ccrs
                Dim uc As New CustomControl(ccr, Me._datatable, Me, Me.Font)
                controlList.Add(uc)
                uc.DataBindings.Add("Value", Me.BindingSource1, ccr.AttributeName)
                uc.Enabled = False
                AddHandler uc.ValueChanged, AddressOf CustomControl_ValueChanged
            Next

            _initialised = True
            Me.ArrangeItems()
            Me.FormDefinitionValid = True

        Catch ex As Exception
            TraceError(ex)
            'Debug.WriteLine(New ExceptionParser(ex).ToString)
            'LogError(MODULENAME, METHODNAME, ex, True, String.Format(My.Resources.CustomFormDefinitionError, ex.Message))
        Finally
            RefreshNavBar()
            RefreshControl(Me.CustomControlPanel)
            RefreshControl(Me.CustomFormBorder)
            Me.ResumeLayout()
        End Try
        Me.Refresh()
    End Sub

    Public ReadOnly Property CustomForm() As FormDefinition.FormRow
        Get
            Return _customForm
        End Get
    End Property

    Private Sub CustomFormPanel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ArrangeItems()
    End Sub

    Public Sub SetParentFilter(ByVal parentGuid As Guid)
        _parentGuid = parentGuid
        Me.BindingSource1.Filter = String.Format(WithoutCulture, "{0}='{1}'", Me._fkColumn.ColumnName, parentGuid.ToString)
        Me._currentRows.Clear()
        Me._currentRows.AddRange(Me._datatable.Select(String.Format(WithoutCulture, "{0}='{1}'", Me._fkColumn.ColumnName, parentGuid.ToString)))
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
        Else
            Me.BindingSource1.MoveFirst()
        End If
        Me.RefreshNavBar()
        _haveChanges = False
    End Sub

    Public Property Current() As DataRow
        Get
            Return _parentRow
        End Get
        Set(ByVal parentRow As DataRow)
            If parentRow Is Nothing Then
                _parentGuid = Guid.Empty
            Else
                _parentGuid = CType(parentRow(Me.CustomForm.ParentPK), Guid)
            End If
            _parentRow = parentRow

            Try
                Me.BindingSource1.EndEdit()
            Catch ex As NoNullAllowedException
                Me.BindingSource1.CancelEdit()
            End Try

            Me.BindingSource1.SuspendBinding()
            Me.BindingSource1.Filter = String.Format("{0}='{1}'", Me.CustomForm.EntityParentFK, _parentGuid)
            Me.BindingSource1.ResumeBinding()
            Me._datatable.Columns(Me.CustomForm.EntityParentFK).DefaultValue = _parentGuid
            Me.BindingSource1.MoveFirst()
            Me.DeleteRecordButton.Enabled = _allowDeleteCurrent
            Me.RefreshNavBar()
            _haveChanges = False
        End Set
    End Property

    'Private _addingNewRecord As Boolean
    'Private Sub BindingSource1_AddingNew(ByVal sender As Object, ByVal e As System.ComponentModel.AddingNewEventArgs) Handles BindingSource1.AddingNew
    '    _addingNewRecord = True
    '    'Try
    '    '    'Dim dr As DataRowView = TryCast(e.NewObject, DataRowView)
    '    '    'If dr IsNot Nothing Then
    '    '    '    If _pkColumn.ColumnName <> _fkColumn.ColumnName Then dr.Row(_pkColumn) = Guid.NewGuid
    '    '    '    dr.Row(_fkColumn) = _parentGuid
    '    '    '    dr.Row(CREATEDDATETIME) = DateTime.UtcNow
    '    '    '    dr.Row(MODIFIEDDATETIME) = DateTime.UtcNow
    '    '    'End If
    '    '    ' Me.BindingSource1.Position = Me.BindingSource1.IndexOf(e.NewObject)
    '    '    'Me.RefreshNavBar()
    '    'Catch ex As Exception
    '    '    TraceError(ex)
    '    'End Try

    'End Sub

    Private Delegate Function EndEditDelegate() As Boolean

    Private Function EndEdit() As Boolean
        If Me.InvokeRequired Then
            Dim d As New EndEditDelegate(AddressOf EndEdit)
            Return CBool(Me.Invoke(d))
        End If

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
                If Me.MoveLastButton.Visible Then Me.MoveLastButton.Select()
                If _currentRow.IsNull(CREATEDDATETIME) Then
                    _currentRow(CREATEDDATETIME) = DateTime.UtcNow
                End If
                _currentRow(MODIFIEDDATETIME) = DateTime.UtcNow
                Dim badFields As New Generic.List(Of String)
                For Each uc As CustomControl In Me.controlList
                    If Not Me._datatable.Columns(uc.FieldName).AllowDBNull AndAlso (_currentRow.IsNull(uc.FieldName) OrElse (Me._datatable.Columns(uc.FieldName).DataType Is GetType(String) AndAlso String.IsNullOrEmpty(CStr(_currentRow(uc.FieldName)).Trim))) Then
                        Dim newValue As Object = uc.Value
                        If newValue Is Nothing OrElse (Me._datatable.Columns(uc.FieldName).DataType Is GetType(String) AndAlso String.IsNullOrEmpty(CStr(newValue).Trim)) Then
                            badFields.Add(uc.Caption.Text)
                        Else
                            _currentRow(uc.FieldName) = uc.Value
                        End If
                    End If
                Next
                If badFields.Count <> 0 Then
                    Dim badFieldList As String = String.Join(", ", badFields.ToArray())
                    Dim messageDetail As String = Terminology.GetFormattedString(MODULENAME, RES_RequiredDataMissingDetails, badFieldList)
                    Terminology.DisplayFormattedMessage(MODULENAME, RES_RequiredDataMissing, MessageBoxIcon.Hand, Me.CustomForm.FormName, messageDetail)
                    result = False
                    Exit Try
                End If
                Me.BindingSource1.EndEdit()
                result = True
            End If
        Catch ex As NoNullAllowedException
            Terminology.DisplayMessage(MODULENAME, RES_NullDataError, MessageBoxIcon.Exclamation)
            result = False
        Catch ex As Exception
            Terminology.DisplayMessage(MODULENAME, RES_SaveError, MessageBoxIcon.Exclamation)
            result = False
        Finally
            If result Then
                Me._haveChanges = False
            End If
        End Try
        Return result
    End Function

    Public Function Save() As Boolean
        Return Me.EndEdit()
    End Function

    Public Sub Cancel()
        Me.BindingSource1.CancelEdit()
        For Each drv As DataRowView In Me.BindingSource1
            If Me._currentRows.IndexOf(drv.Row) = -1 Then
                drv.Row.Delete()
            End If
        Next
        For Each dr As DataRow In Me._currentRows
            Me._datatable.ImportRow(dr)
        Next
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
                _allowDeleteCurrent = False
                Me.DeleteRecordButton.Enabled = False
                For Each uc As CustomControl In Me.controlList
                    uc.Enabled = False
                Next
                Me.BindingSource1.AllowNew = False
                Me.BindingSource1.RaiseListChangedEvents = False
            Else
                Me.AddRecordButton.Enabled = True
                _allowDeleteCurrent = False
                Me.DeleteRecordButton.Enabled = False ' will reset as relevant in RefreshNavBar
                For Each uc As CustomControl In Me.controlList
                    uc.Enabled = _currentRow IsNot Nothing ' True
                Next
                Me.BindingSource1.AllowNew = True
                Me.BindingSource1.RaiseListChangedEvents = True
            End If
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

    Private _maxChildren As Integer ' = ConfigurationSettings.GetIntegerValue("MaximumCustomRecordsPerParent", 20)
    Public Property MaximumChildren() As Integer
        Get
            Return _maxChildren
        End Get
        Set(ByVal value As Integer)
            _maxChildren = value
        End Set
    End Property

    Private Delegate Sub RefreshNavBarCallBack()

    Private Sub RefreshNavBar()
        If Not Me.FormDefinitionValid Then
            Return
        End If
        If Me.InvokeRequired Then
            Dim d As New RefreshNavBarCallBack(AddressOf RefreshNavBar)
            Me.Invoke(d)
        Else
            If Me._maxChildren = 1 OrElse Me._customForm.MaxItems = 1 Then
                Me.MoveLastButton.Enabled = False
                Me.MoveFirstButton.Enabled = False
                Me.MoveNextButton.Enabled = False
                Me.MovePreviousButton.Enabled = False
                _allowDeleteCurrent = False
                Me.DeleteRecordButton.Enabled = False
                Me.BindingNavigatorPositionItem.Enabled = False
                Me.BindingSource1.AllowNew = Me.BindingSource1.Count = 0
                Me.AddRecordButton.Enabled = Me.BindingSource1.AllowNew  'Me.BindingSource1.Count = 0
            Else
                Dim p As Integer = Me.BindingSource1.Position
                Dim c As Integer = Me.BindingSource1.Count
                If c > 0 Then
                    Me.MoveLastButton.Enabled = p < c - 1
                    Me.MoveFirstButton.Enabled = p > 0
                    Me.MoveNextButton.Enabled = p < c - 1
                    Me.MovePreviousButton.Enabled = p > 0
                    _allowDeleteCurrent = (p < c) AndAlso Not Me.ReadOnly AndAlso Me._currentRow IsNot Nothing AndAlso Me._currentRow.RowState = DataRowState.Added
                    Me.DeleteRecordButton.Enabled = _allowDeleteCurrent
                    Me.AddRecordButton.Enabled = (Me.MaximumChildren <= 0) OrElse (c < Me.MaximumChildren)
                Else
                    Me.MoveLastButton.Enabled = False
                    Me.MoveFirstButton.Enabled = False
                    Me.MoveNextButton.Enabled = False
                    Me.MovePreviousButton.Enabled = False
                    _allowDeleteCurrent = False
                    Me.DeleteRecordButton.Enabled = False
                    Me.AddRecordButton.Enabled = Not Me.ReadOnly AndAlso Not Me._parentGuid.Equals(Guid.Empty)
                End If
                ResetDeleteEnabled()
            End If
            Me.UndoChangesButton.Enabled = Me._currentRow IsNot Nothing AndAlso ((Me._currentRow.RowState = DataRowState.Modified AndAlso DataRowHasChanges(Me._currentRow)) OrElse Me._currentRow.RowState = DataRowState.Detached)
        End If
        'Me.RecordLocationLabel.Text = String.Format(WithCulture, My.Resources.CustomFormRecordLocationFormat, Me.BindingSource1.Position + 1, Me.BindingSource1.Count)
    End Sub

    '    Private Sub BindingSource1_BindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles BindingSource1.BindingComplete
    '        If Not e.BindingCompleteState = BindingCompleteState.Success Then Exit Sub
    '
    '        If e.BindingCompleteContext = BindingCompleteContext.DataSourceUpdate Then
    '            RefreshNavBar()
    '        Else
    '            If e.Binding.Control.Focused And Not Me.ReadOnly Then
    '                Me._haveChanges = True
    '            End If
    '        End If
    '    End Sub

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

    Private Delegate Sub ChangeToolstripEnabledStateCallBack(ByVal control As ToolStripItem, ByVal enabled As Boolean)
    Private Sub ChangeControlEnabledState(ByVal control As ToolStripItem, ByVal enabled As Boolean)
        If control.Owner.InvokeRequired Then
            Dim d As New ChangeToolstripEnabledStateCallBack(AddressOf ChangeControlEnabledState)
            Me.Invoke(d, New Object() {control, enabled})
        Else
            control.Enabled = enabled
        End If
    End Sub

#End Region

    Private _inControlValueChanged As Boolean
    Private Sub CustomControl_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        If _inControlValueChanged Then Return
        If _refreshing Then Return
        Dim cc As CustomControl = TryCast(sender, CustomControl)
        If cc IsNot Nothing Then
            If _currentRow IsNot Nothing Then
                Try
                    _inControlValueChanged = True
                    Select Case cc.ControlType
                        Case ControlType.CheckBox
                            If cc.CustomControlRow.Lines = 2 Then
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
                            If Not cc.Enabled Then Return ' HACK: error, really.
                            If cc.ComboBox1.SelectedIndex = -1 Then
                                _currentRow(cc.CustomControlRow.AttributeName) = Convert.DBNull
                            Else
                                _currentRow(cc.CustomControlRow.AttributeName) = cc.Value
                            End If
                            For Each c As CustomControl In Me.controlList
                                If c.ControlType = ControlType.List Then
                                    If c Is cc Then Continue For
                                    If c.FieldName = cc.FieldName AndAlso c.ListDataSource = cc.ListDataSource AndAlso c.ListValueColumn = cc.ListValueColumn Then
                                        c.Value = cc.Value ' = cc.ComboBox1.SelectedValue ' sync list boxes based on same data
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
                Catch ex As Exception
                    TraceError(ex)
                    'LogError(Me.CustomForm.FormName, METHODNAME, ex, True, Nothing)
                Finally
                    _inControlValueChanged = False
                End Try
            End If

        End If
    End Sub

    Private Sub BindingSource1_AddingNew(ByVal sender As Object, ByVal e As System.ComponentModel.AddingNewEventArgs) Handles BindingSource1.AddingNew
        'If e.NewObject IsNot Nothing Then
        '    Me.ResetDeleteEnabled()
        'End If
    End Sub

    Private Sub BindingSource1_BindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles BindingSource1.BindingComplete
        Debug.Print(e.BindingCompleteContext.ToString())
        Debug.Print(e.BindingCompleteState.ToString())
    End Sub

    Private Sub BindingSource1_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingSource1.CurrentChanged
        If Me._haveChanges Then
            Me.EndEdit()
        End If
    End Sub

    Private Sub BindingSource1_CurrentItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingSource1.CurrentItemChanged
        TraceVerbose(STR_Fired)
    End Sub

    Private Sub BindingSource1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingManagerDataErrorEventArgs) Handles BindingSource1.DataError
        TraceError(e.Exception)
        'Stop
    End Sub

    Private Sub BindingSource1_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles BindingSource1.ListChanged
        TraceVerbose(STR_Fired)
        RefreshNavBar()
    End Sub

    Private Sub BindingSource1_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingSource1.PositionChanged
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
                    End If

                    If _currentRow.IsNull(Me._fkColumn) Then
                        _currentRow(Me._fkColumn) = _parentGuid
                    End If
                    If _currentRow.IsNull(CREATEDDATETIME) Then
                        _currentRow(CREATEDDATETIME) = DateTime.UtcNow
                    End If
                    _currentRow(MODIFIEDDATETIME) = DateTime.UtcNow
                    ChangeControlEnabledState(Me.CustomControlPanel, True)
                    For Each c As Control In Me.controlList
                        ChangeControlEnabledState(c, Not Me.ReadOnly)
                    Next
                Catch ex As Exception
                    TraceError(ex)
                    'exceptionMsg("BindingSource1_PositionChanged", ex, MODULENAME, False, "Error setting current row")
                End Try
                'Me.DeleteRecordButton.Enabled = drv.Row.RowState = DataRowState.Added ' handled in refreshnavbar
            Else
                _currentRow = Nothing
                ChangeControlEnabledState(Me.CustomControlPanel, False)
                For Each c As Control In Me.controlList
                    ChangeControlEnabledState(c, False)
                Next
            End If
        Else
            _currentRow = Nothing
            ChangeControlEnabledState(Me.CustomControlPanel, False)
            For Each c As Control In Me.controlList
                ChangeControlEnabledState(c, False)
            Next
        End If
        RefreshNavBar()
    End Sub


    Private Sub AddRecordButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddRecordButton.Click
        TraceVerbose(STR_Fired)

        Try
            Me.BindingSource1.EndEdit()
        Catch ex As NoNullAllowedException
            ' hard-core, intolerant of users being insensitive
            Me.BindingSource1.CancelEdit()
        End Try
        'If Me._haveChanges Then

        'End If
        'ResetDeleteEnabled()
    End Sub

    Private Sub _consoleData_Refreshed(ByVal sender As Object, ByVal e As ConsoleDataRefreshEventArgs) Handles _consoleData.Refreshed
        ' TODO: reload drop-down lists.
        _refreshing = False
    End Sub

    Private Sub _consoleData_Refreshing(ByVal sender As Object, ByVal e As System.EventArgs) Handles _consoleData.Refreshing
        _refreshing = True
        Try
            Me.BindingSource1.EndEdit()
        Catch ex As NoNullAllowedException
            ' Terminology.DisplayMessage(MODULENAME, RES_UnableToSaveCustomForm, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub CustomFormPanel_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Validated
        Me.BindingSource1.EndEdit()
    End Sub

    Private _inResettingDeleteEnabledState As Boolean

    Private Delegate Sub ResetDeleteEnabledCallback()

    Private Sub ResetDeleteEnabled()
        If _inResettingDeleteEnabledState Then Return
        If Me.InvokeRequired Then
            Dim d As New ResetDeleteEnabledCallback(AddressOf ResetDeleteEnabled)
            Me.Invoke(d)
        Else
            _inResettingDeleteEnabledState = True
            Me.DeleteRecordButton.Enabled = _allowDeleteCurrent ' Me.Current IsNot Nothing AndAlso Me.Current.RowState = DataRowState.Added
            _inResettingDeleteEnabledState = False
        End If
    End Sub

    Private Sub DeleteRecordButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteRecordButton.Click
        If Me._currentRow IsNot Nothing AndAlso Me._currentRow.RowState = DataRowState.Added Then
            If Terminology.AskQuestion(MODULENAME, RES_UndoChangesToConfirmDelete, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me._currentRow.RejectChanges()
            End If
        End If
    End Sub

    Private Sub DeleteRecordButton_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteRecordButton.EnabledChanged
        ResetDeleteEnabled()
    End Sub

    Private Sub UndoChangesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoChangesButton.Click
        If Me._currentRow IsNot Nothing AndAlso Me._currentRow.RowState = DataRowState.Modified AndAlso DataRowHasChanges(Me._currentRow) Then
            If Terminology.AskQuestion(MODULENAME, RES_UndoChangesToCustomData, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me._currentRow.CancelEdit()
                Me._currentRow.RejectChanges()
            End If
        ElseIf Me._currentRow IsNot Nothing AndAlso Me._currentRow.RowState = DataRowState.Detached Then
            If Terminology.AskQuestion(MODULENAME, RES_UndoChangesToConfirmDelete, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me._currentRow.CancelEdit()
                Me.BindingSource1.RemoveCurrent()
            End If
        End If
    End Sub
End Class
