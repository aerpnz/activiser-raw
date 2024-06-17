Imports System.ComponentModel

<DefaultEvent("PropertyChanged")> _
<DefaultProperty("NullableValue")> _
Public Class DateTimePicker
    Implements INotifyPropertyChanged
    'Public Event ValueChanged As EventHandler

    Private WithEvents timer As System.Timers.Timer
    Private WithEvents DatePopup As DatePickerPopup
    Private WithEvents TimePopup As TimePickerPopup

    Private Const testDate As DateTime = #12/29/9998 11:30:00 PM#
    Private Const noDate As DateTime = #12/31/9999 11:59:59 PM#
    Private Const bottomDate As DateTime = #1/1/1753#
    Private Const topDate As DateTime = #12/31/9998 11:59:59 PM#

    Private _maxValue As DateTime = topDate
    Private _minValue As DateTime = bottomDate

    Private _dataValue As Nullable(Of DateTime)
    Private _displayValue As DateTime

    Private _preNullValue As DateTime = noDate
    Private _useCurrentTime As Boolean = True
    Private _haveValue As Boolean
    Private _settingNullState As Boolean


    Private _settingValue As Boolean
    Private _inDrawValue As Boolean

    Private _dateWidth As Integer
    Private _timeWidth As Integer

    Private _alignment As Windows.Forms.LeftRightAlignment = LeftRightAlignment.Left

    Private _initialised As Boolean

    Public Event ValueChanged As EventHandler
    Public Shadows Event Validated As EventHandler

#Region "ctor"
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.timer = New System.Timers.Timer(10)
        Me.timer.AutoReset = False
        DatePopup = New DatePickerPopup()
        Me.DatePopup.Value = Date.Today
        Me.DatePopup.AnchorControl = Me.DateCombo

        TimePopup = New TimePickerPopup()
        Me.TimePopup.StartValue = Date.Today
        Me.TimePopup.AnchorControl = Me.TimeCombo
        Me.TimePopup.Refresh()

        _initialised = True
    End Sub

    Private Sub DateTimePicker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CalcMinimumSize()
    End Sub
#End Region

#Region "General Appearance/Behaviour Properties"
    <Category("Appearance"), DefaultValue(GetType(Windows.Forms.LeftRightAlignment), "Left"), Localizable(True)> _
    Public Property Alignment() As Windows.Forms.LeftRightAlignment
        Get
            Return _alignment
        End Get
        Set(ByVal value As Windows.Forms.LeftRightAlignment)
            If value <> _alignment Then
                _alignment = value
                If value = LeftRightAlignment.Right Then
                    Me.TimeCombo.Dock = DockStyle.Right
                    Me.DateTimeSpacer.Dock = DockStyle.Right
                    Me.DateCombo.Dock = DockStyle.Right
                    Me.CheckBoxSpacer.Dock = DockStyle.Right
                    Me.HaveValueCheckBox.Dock = DockStyle.Right
                    Me.TimeCombo.BringToFront()
                    Me.DateTimeSpacer.BringToFront()
                    Me.DateCombo.BringToFront()
                    Me.CheckBoxSpacer.BringToFront()
                    Me.HaveValueCheckBox.BringToFront()
                Else
                    Me.TimeCombo.Dock = DockStyle.Left
                    Me.DateTimeSpacer.Dock = DockStyle.Left
                    Me.DateCombo.Dock = DockStyle.Left
                    Me.CheckBoxSpacer.Dock = DockStyle.Left
                    Me.HaveValueCheckBox.Dock = DockStyle.Left
                    Me.TimeCombo.SendToBack()
                    Me.DateTimeSpacer.SendToBack()
                    Me.DateCombo.SendToBack()
                    Me.CheckBoxSpacer.SendToBack()
                    Me.HaveValueCheckBox.SendToBack()
                End If
            End If
        End Set
    End Property

    <Category("Behavior"), DefaultValue(GetType(DateTime), "1753-01-01 00:00"), Localizable(True)> _
    Public Property MinValue() As DateTime
        Get
            Return _minValue
        End Get
        Set(ByVal value As DateTime)
            If value > topDate OrElse value < bottomDate Then Return
            _minValue = value
            DatePopup.Calendar.MinDate = value.Date
            If TimePopup.StartValue <> value Then TimePopup.StartValue = value
            If Me.HasValue AndAlso Me.DisplayValue < value Then
                Me.DisplayValue = value
            End If
        End Set
    End Property

    <Category("Behavior"), DefaultValue(GetType(DateTime), "9998-12-31 23:59:59"), Localizable(True)> _
    Public Property MaxValue() As DateTime
        Get
            Return _maxValue
        End Get
        Set(ByVal value As DateTime)
            If value > topDate OrElse value < bottomDate Then Return
            _maxValue = value
            Me.DatePopup.Calendar.MaxDate = value.Date
            If TimePopup.StartValue > value.AddDays(-1) Then TimePopup.StartValue = value.AddDays(-1)
            If Me.HasValue AndAlso Me.NullableValue.Value > value Then
                Me.Value = value
            End If
        End Set
    End Property

    <Category("Appearance"), DefaultValue(15), Localizable(True)> _
    Public Property TimeInterval() As Integer
        Get
            Return TimePopup.TimeInterval
        End Get
        Set(ByVal value As Integer)
            TimePopup.TimeInterval = value
        End Set
    End Property

    Private _showTimeDifference As Boolean = False
    <Category("Appearance"), DefaultValue(False), Localizable(True)> _
    Public Property ShowTimeDifference() As Boolean
        Get
            Return _showTimeDifference
        End Get
        Set(ByVal value As Boolean)
            _showTimeDifference = value
        End Set
    End Property

    <Category("Appearance"), DefaultValue(True)> _
    Public Property IncludeZeroTime() As Boolean
        Get
            Return TimePopup.IncludeZeroTime
        End Get
        Set(ByVal value As Boolean)
            TimePopup.IncludeZeroTime = value
        End Set
    End Property

    Private _nullable As Boolean = True
    <Category("Appearance"), DefaultValue(True)> _
    Public Property ShowCheckBox() As Boolean
        Get
            Return _nullable 'Me.HaveValueCheckBox.Visible
        End Get
        Set(ByVal value As Boolean)
            _nullable = value
            Me.HaveValueCheckBox.Visible = value
            Me.CheckBoxSpacer.Visible = value
            CalcMinimumSize()
            If value OrElse Me.HasValue Then Return
            Me._useCurrentTime = True ' if we're hiding the null-indicating checkbox, we need to force a time into the picker, since no time is no longer an option.
            Me.DrawValue()
        End Set
    End Property

    <DefaultValue(True), Category("Appearance")> _
    Public Property ShowDate() As Boolean
        Get
            Return Me.DateCombo.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.DateCombo.Visible = value
            If Not Me.TimeCombo.Visible Then Me.TimeCombo.Visible = True
            Me.DateTimeSpacer.Visible = Me.DateCombo.Visible AndAlso Me.TimeCombo.Visible
            CalcMinimumSize()
        End Set
    End Property

    <DefaultValue(True), Category("Appearance")> _
    Public Property ShowTime() As Boolean
        Get
            Return Me.TimeCombo.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.TimeCombo.Visible = value
            If Not Me.DateCombo.Visible Then Me.DateCombo.Visible = True
            Me.DateTimeSpacer.Visible = Me.DateCombo.Visible AndAlso Me.TimeCombo.Visible
            CalcMinimumSize()
        End Set
    End Property

    <Category("Appearance"), DefaultValue(6), Localizable(True)> _
    Public Property InterPickerGap() As Integer
        Get
            Return Me.DateTimeSpacer.Width
        End Get
        Set(ByVal value As Integer)
            Me.DateTimeSpacer.Width = value
        End Set
    End Property

    'Private _ForceRoundedTime As Boolean = False
    '<Category("Behavior"), DefaultValue(False), Localizable(True)> _
    'Public Property ForceRoundedTime() As Boolean
    '    Get
    '        Return _ForceRoundedTime
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _ForceRoundedTime = value
    '        Me.DrawValue()
    '    End Set
    'End Property

    Private _displayInLocalTime As Boolean = True
    <Category("Behavior"), DefaultValue(True), Localizable(True)> _
    Public Property DisplayInLocalTime() As Boolean
        Get
            Return _displayInLocalTime
        End Get
        Set(ByVal value As Boolean)
            _displayInLocalTime = value
            If value AndAlso Me.HasValue Then
                Me.DisplayValue = Me._dataValue.Value.ToLocalTime
            ElseIf Me.HasValue Then
                Me.DisplayValue = Me._dataValue.Value
            ElseIf value Then
                Me.DisplayValue = Me.DefaultValue.ToLocalTime
            Else
                Me.DisplayValue = Me.DefaultValue
            End If
        End Set
    End Property

    Private _useUniversalTime As Boolean = True
    <Category("Behavior"), DefaultValue(True), Localizable(True)> _
    Public Property ForceUniversalTimeValues() As Boolean
        Get
            Return _useUniversalTime
        End Get
        Set(ByVal value As Boolean)
            _useUniversalTime = value
            Me.DrawValue()
        End Set
    End Property

    Private _useOutlookStyleTimeDiff As Boolean = True
    <Category("Behavior"), DefaultValue(True), Localizable(True)> _
    Public Property UseOutlookStyleTimeDifferences() As Boolean
        Get
            Return _useOutlookStyleTimeDiff
        End Get
        Set(ByVal value As Boolean)
            _useOutlookStyleTimeDiff = value
        End Set
    End Property

#End Region

#Region "Date Format"
    Public ReadOnly Property DefaultDateFormat() As String
        Get
            Return Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
        End Get
    End Property

    Private _dateFormat As DateTimePickerFormat = DateTimePickerFormat.Short
    Private _customDateFormat As String = DefaultDateFormat

    <Category("Appearance"), Localizable(True), DefaultValue(GetType(DateTimePickerFormat), "Short")> _
    Public Property DateFormat() As DateTimePickerFormat
        Get
            Return _dateFormat
        End Get
        Set(ByVal value As DateTimePickerFormat)
            _dateFormat = value
            CalcMinimumSize()
        End Set
    End Property

    <Category("Appearance"), Localizable(True)> _
    Public Property CustomDateFormat() As String
        Get
            Return _customDateFormat
        End Get
        Set(ByVal value As String)
            If value <> _customDateFormat Then
                Try
                    Dim testDateString As String = testDate.ToString(value)
                    _customDateFormat = value
                    CalcMinimumSize()
                Catch ' ex As Exception
                End Try
            End If
        End Set
    End Property

    Private Function ShouldSerializeCustomDateFormat() As Boolean
        Return CustomDateFormat <> DefaultDateFormat
    End Function

    Private Sub ResetCustomDateFormat()
        CustomDateFormat = DefaultDateFormat
    End Sub

    <Category("Appearance"), Localizable(True), DefaultValue(False)> _
    Public Property ShowWeekNumbers() As Boolean
        Get
            Return Me.DatePopup.Calendar.ShowWeekNumbers
        End Get
        Set(ByVal value As Boolean)
            Me.DatePopup.Calendar.ShowWeekNumbers = value
        End Set
    End Property

#End Region

#Region "Time Format"

    Public ReadOnly Property DefaultCustomTimeFormat() As String
        Get
            Return GetShortTimeFormat(Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern)
        End Get
    End Property

    Private _timeFormat As DateTimePickerFormat = DateTimePickerFormat.Short
    <Category("Appearance"), Localizable(True), DefaultValue(GetType(DateTimePickerFormat), "Short")> _
    Public Property TimeFormat() As DateTimePickerFormat
        Get
            Return _timeFormat
        End Get
        Set(ByVal value As DateTimePickerFormat)
            _timeFormat = value
            Select Case Me.TimeFormat
                Case DateTimePickerFormat.Custom
                    Me.TimePopup.TimeFormat = _customTimeFormat
                Case DateTimePickerFormat.Long
                    Me.TimePopup.TimeFormat = Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
                Case DateTimePickerFormat.Short
                    Me.TimePopup.TimeFormat = DefaultCustomTimeFormat
            End Select

            'Me.TimePopup.TimeFormat = value
            CalcMinimumSize()
        End Set
    End Property


    Private _customTimeFormat As String = DefaultCustomTimeFormat
    <Category("Appearance"), Localizable(True)> _
    Public Property CustomTimeFormat() As String
        Get
            Return _customTimeFormat
        End Get
        Set(ByVal value As String)
            If value <> _customTimeFormat Then
                Try
                    ' test format, if OK, then use it, otherwise, ignore it.
                    Dim testTimeString As String = testDate.ToString(value)
                    _customTimeFormat = value
                    Me.TimePopup.TimeFormat = value
                    CalcMinimumSize()
                Catch ' ex As Exception
                End Try
            End If
        End Set
    End Property

    Private Function ShouldSerializeCustomTimeFormat() As Boolean
        Return CustomTimeFormat <> DefaultCustomTimeFormat
    End Function

    Private Sub ResetCustomTimeFormat()
        CustomTimeFormat = DefaultCustomTimeFormat
    End Sub
#End Region

#Region "Component Event Handlers"
    Private _timerTarget As ComboBox

    Private Delegate Sub CloseDropDownCallback(ByVal control As ComboBox)
    Private Sub CloseDropDown(ByVal control As ComboBox)
        If control.InvokeRequired Then
            Dim cb As New CloseDropDownCallback(AddressOf CloseDropDown)
            Me.Invoke(cb, control)
        Else
            control.DroppedDown = False
            control.SelectAll()
            control.Select(0, 0)
        End If
    End Sub

    Private Delegate Sub SetComboTextCallback(ByVal control As ComboBox, ByVal text As String)
    Private Sub SetComboText(ByVal control As ComboBox, ByVal text As String)
        If control.InvokeRequired Then
            Dim cb As New SetComboTextCallback(AddressOf SetComboText)
            Me.Invoke(cb, control, text)
        Else
            control.Text = text
            control.Select(0, 0)
            Application.DoEvents()
        End If
    End Sub

    Private Sub Combo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimeCombo.Leave, DateCombo.Leave
        Dim c As ComboBox = TryCast(sender, ComboBox)
        If c IsNot Nothing Then
            c.SelectionStart = 0
            c.SelectionLength = 0
        End If
    End Sub

    'Private Sub DateCombo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateCombo.TextChanged
    '    If _inDrawValue Then Exit Sub
    '    Dim dt As DateTime
    '    If Date.TryParseExact(Me.DateCombo.Text, Me._customDateFormat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces, dt) Then
    '        If Me.HasValue Then
    '            Me.DisplayValue = dt.Date + Me.DisplayValue.TimeOfDay
    '        Else
    '            Me.DisplayValue = dt
    '        End If
    '    End If
    'End Sub

    Private Sub DateCombo_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateCombo.DropDown
        If DatePopup.Visible Then
            DatePopup.Hide()
        Else
            DatePopup.Calendar.SelectionStart = Me.DisplayValue.Date
            DatePopup.Calendar.SelectionEnd = Me.DisplayValue.Date
            DatePopup.Popup()
        End If
        _timerTarget = Me.DateCombo
        timer.Start()
    End Sub

    Private Sub calendarPopup_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DatePopup.SelectedValueChanged
        Me.DatePopup.Hide()
        Me.CloseDropDown(Me.DateCombo)
        Dim newDateTime As DateTime = Me.DatePopup.Value + Me.DisplayValue.TimeOfDay

        Me.DisplayValue = newDateTime 'Me.DatePopup.Value + Me.DisplayValue.Value.TimeOfDay
    End Sub

    Private Sub DateCombo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DateCombo.Validating
        If _inDrawValue Then Exit Sub
        Dim dt As DateTime
        If String.IsNullOrEmpty(Me.DateCombo.Text) Then
            If Me._nullable Then
                Me.NullableValue = Nothing
            Else
                Me.DisplayValue = Me.DisplayValue.Date + Me.DisplayValue.TimeOfDay
            End If
        Else
            If Not Date.TryParse(Me.DateCombo.Text, dt) Then
                MessageBox.Show(My.Resources.InvalidDateWarning, My.Resources.InvalidDateWarningHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
            Else
                If dt.Date <> Me.DisplayValue.Date Then
                    Me.DisplayValue = dt.Date + Me.DisplayValue.TimeOfDay
                End If
            End If
        End If
    End Sub

    Private Sub TimeCombo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TimeCombo.KeyDown
        Select Case e.KeyData
            Case Keys.Down
                If Me.HasValue Then
                    Dim newValue As DateTime = Me.DisplayValue.AddMinutes(Me.TimeInterval)
                    If newValue <= Me.MaxValue Then
                        Me.DisplayValue = newValue
                    End If
                End If
            Case Keys.Up
                If Me.HasValue Then
                    Dim newValue As DateTime = Me.DisplayValue.AddMinutes(-Me.TimeInterval)
                    If newValue >= Me.MinValue Then
                        Me.DisplayValue = newValue
                    End If
                End If
        End Select
    End Sub

    Private Sub TimeCombo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TimeCombo.Validating
        If _inDrawValue Then Exit Sub
        Dim dt As DateTime
        If Not Date.TryParse(TimeCombo.Text, dt) Then
            e.Cancel = True
        Else
            dt = Me.DisplayValue.Date + dt.TimeOfDay
            If dt >= Me.MinValue AndAlso dt <= Me.MaxValue Then
                If dt <> Me.DisplayValue Then
                    Me.DisplayValue = dt
                End If
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private prePopupDate As DateTime

    Private Sub TimeCombo_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimeCombo.DropDown
        If Me.TimePopup.Visible Then
            Me.TimePopup.Hide()
        Else
            Dim currentPickerTime As DateTime
            If DateTime.TryParse(Me.TimeCombo.Text, currentPickerTime) Then
                currentPickerTime = Me.DisplayValue.Date + currentPickerTime.TimeOfDay
                If currentPickerTime <> Me.DisplayValue Then
                    Me.DisplayValue = currentPickerTime
                End If
            Else
                currentPickerTime = Me.DisplayValue
            End If
            prePopupDate = currentPickerTime.Date
            Dim doWholeDay As Boolean = (Not _useOutlookStyleTimeDiff AndAlso currentPickerTime.Date > MinValue.Date) OrElse (_useOutlookStyleTimeDiff AndAlso currentPickerTime > Me.MinValue.AddDays(1))
            If doWholeDay Then ' currentPickerTime > Me.MinValue.AddDays(1) Then
                Me.TimePopup.StartValue = currentPickerTime.Date
                Me.TimePopup.ShowTimeDifference = False
                Me.TimePopup.Popup(currentPickerTime.TimeOfDay)
            Else
                Me.TimePopup.StartValue = Me.MinValue
                Me.TimePopup.ShowTimeDifference = Me.ShowTimeDifference
                If Me.MinValue.Date < currentPickerTime.Date Then
                    Me.TimePopup.Popup(currentPickerTime.TimeOfDay + TimeSpan.FromDays(1))
                Else
                    Me.TimePopup.Popup(currentPickerTime.TimeOfDay)
                End If
            End If

        End If
        _timerTarget = Me.TimeCombo
        timer.Start()
    End Sub

    Private Sub timePopup_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TimePopup.SelectedValueChanged
        Me.TimePopup.Hide()
        Dim currentPickerTime As DateTime
        If DateTime.TryParse(Me.TimeCombo.Text, currentPickerTime) Then
            currentPickerTime = prePopupDate + currentPickerTime.TimeOfDay
            If currentPickerTime <> Me.DisplayValue Then
                Me.DisplayValue = currentPickerTime
            End If
        Else
            currentPickerTime = Me.DisplayValue
        End If
        Me.DisplayValue = Me.TimePopup.StartValue.Date + Me.TimePopup.SelectedValue
        'Debug.WriteLine(Me.DisplayValue)
    End Sub

    Private Sub HaveValueCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HaveValueCheckBox.CheckedChanged
        If Not _initialised Then Return
        If _inDrawValue Then Return
        If Me.HasValue <> Me.HaveValueCheckBox.Checked Then
            Me.HasValue = Me.HaveValueCheckBox.Checked
        End If
    End Sub

#End Region

#Region "Data Properties"

    Private Function ShouldSerializeValue() As Boolean
        Return False
    End Function

    Private Function ShouldSerializeDbValue() As Boolean
        Return False
    End Function

    Private Function ShouldSerializeNullableValue() As Boolean
        Return Not _useCurrentTime
    End Function

    Private Sub ResetValue()
        Me._useCurrentTime = True
    End Sub

    Private Sub ResetNullableValue()
        Me._useCurrentTime = True
    End Sub

    <Category("Data"), Bindable(True)> _
    Public Property NullableValue() As Nullable(Of DateTime)
        Get
            If Not Me.HasValue Then
                If Me._useCurrentTime Then
                    Return Me.DefaultValue
                Else
                    Return Nothing
                End If
            ElseIf Me.DisplayInLocalTime Then
                Return DisplayValue.ToUniversalTime
            Else
                Return DisplayValue
            End If
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _settingValue = True
            _useCurrentTime = False
            If value.HasValue Then
                If value.Value < Me.MinValue Then
                    Return
                End If

                If value.Value > Me.MaxValue Then
                    Return
                End If

                If value.Value = noDate Then
                    Me._useCurrentTime = True
                End If

                Me.HasValue = True
                _dataValue = FixTime(value.Value)

                If Me.DisplayInLocalTime Then
                    If Me.ForceUniversalTimeValues Then
                        DisplayValue = DateTime.SpecifyKind(_dataValue.Value, DateTimeKind.Utc).ToLocalTime
                    Else
                        DisplayValue = _dataValue.Value.ToLocalTime
                    End If
                Else
                    DisplayValue = _dataValue.Value
                End If
                _preNullValue = DisplayValue
            Else
                Me.HasValue = False
                _dataValue = Nothing
                _preNullValue = noDate
                DisplayValue = noDate
            End If

            _settingValue = False
        End Set
    End Property

    <Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(True)> _
    Public Property DBValue() As Object
        Get
            If Me.HasValue Then
                Return NullableValue.Value
            ElseIf Me._useCurrentTime Then
                Return DefaultValue
            Else
                Return DBNull.Value
            End If
        End Get
        Set(ByVal value As Object)
            If IsDBNull(value) OrElse Not IsDate(value) Then
                Me.NullableValue = Nothing
            Else
                Me.NullableValue = CDate(value)
            End If
        End Set
    End Property

    <Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(True)> _
    Public Property Value() As DateTime
        Get
            If NullableValue.HasValue Then
                Return NullableValue.Value
            Else
                Return DefaultValue
            End If
        End Get
        Set(ByVal value As DateTime)
            Me.NullableValue = value
        End Set
    End Property

    Private Sub RaiseValueChangedEvents()
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Value"))
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DBValue"))
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("NullableValue"))
        RaiseEvent ValueChanged(Me, New EventArgs())
    End Sub

    Public Property DisplayValue() As DateTime
        Get
            If Me.HasValue Then
                Return _displayValue
            ElseIf Me._useCurrentTime Then
                Return Me.DefaultValue
            Else
                Return _preNullValue
            End If
        End Get
        Private Set(ByVal value As DateTime)
            If _displayValue <> value Then
                _displayValue = value
                Me._haveValue = True
                If Not Me._settingValue Then
                    RaiseValueChangedEvents()
                End If
            End If

            If Not _settingNullState Then
                DrawValue()
            End If

        End Set
    End Property

    <Category("Data")> _
    Public Property HasValue() As Boolean
        Get
            Return Me._haveValue
        End Get
        Private Set(ByVal value As Boolean)
            _settingNullState = True
            If _settingValue Then
                Me._haveValue = value
            Else
                If value And Not Me._haveValue Then
                    If Me._preNullValue = noDate Then
                        Me._useCurrentTime = True
                        Me.DisplayValue = Me.DefaultValue
                    Else
                        Me.DisplayValue = _preNullValue
                    End If
                    Me._haveValue = value
                    DrawValue()
                Else
                    If Me._haveValue Then
                        Me._preNullValue = Me.DisplayValue
                    ElseIf Me._useCurrentTime Then
                        Me._preNullValue = noDate
                    Else
                        Me._preNullValue = Me.DisplayValue
                    End If
                    Me._haveValue = value
                    DrawValue()
                End If
                RaiseValueChangedEvents()
                '                RaiseEvent ValueChanged(Me, New System.EventArgs())
            End If
            _settingNullState = False
        End Set
    End Property

    Public ReadOnly Property DefaultValue() As Date
        Get
            If Me.ForceUniversalTimeValues OrElse Me.DisplayInLocalTime Then
                Return FixTime(DateTime.UtcNow)
            Else
                Return FixTime(DateTime.Now)
            End If
        End Get
    End Property
#End Region

#Region "Overridden Event Handlers and Appearance Properties"
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Me.DrawValue()
    End Sub

    Private Sub GetDateTimeWidths()
        Dim g As Graphics = Me.CreateGraphics()
        Dim testDateString As String = testDate.ToString(_customDateFormat)
        Dim testTimeString As String = testDate.ToString(_customTimeFormat)
        Select Case Me.DateFormat
            Case DateTimePickerFormat.Custom
                testDateString = testDate.ToString(_customDateFormat)
            Case DateTimePickerFormat.Long
                testDateString = testDate.ToLongDateString
            Case DateTimePickerFormat.Short
                testDateString = testDate.ToShortDateString
        End Select
        Select Case Me.TimeFormat
            Case DateTimePickerFormat.Custom
                testTimeString = testDate.ToString(_customTimeFormat)
            Case DateTimePickerFormat.Long
                testTimeString = testDate.ToLongTimeString
            Case DateTimePickerFormat.Short
                testTimeString = testDate.ToString(DefaultCustomTimeFormat)
        End Select
        Dim ds As Size = g.MeasureString(testDateString, MyBase.Font).ToSize
        Dim ts As Size = g.MeasureString(testTimeString, MyBase.Font).ToSize
        _dateWidth = ds.Width + MyBase.Font.Height + Me.DateCombo.Height
        _timeWidth = ts.Width + MyBase.Font.Height + Me.DateCombo.Height
    End Sub

    Private Sub SetSizes()
        GetDateTimeWidths()
        If Me.ShowDate AndAlso Me.ShowTime Then
            Dim extraWidth As Integer = (Me.Width - Me.DefaultSize.Width) \ 2
            Me.DateCombo.Width = Math.Max(_dateWidth + extraWidth, DateCombo.Height)
            Me.TimeCombo.Width = Math.Max(_timeWidth + extraWidth, DateCombo.Height)
            Me.TimePopup.MinimumWidth = Me.TimeCombo.Width
        ElseIf Me.ShowDate Then
            If Me.ShowCheckBox Then
                Me.DateCombo.Width = Me.Width - (Me.HaveValueCheckBox.Width + Me.CheckBoxSpacer.Width)
            Else
                Me.DateCombo.Width = Me.Width
            End If
        Else ' Me.TimeCombo.Visible 
            If Me.ShowCheckBox Then
                Me.TimeCombo.Width = Me.Width - (Me.HaveValueCheckBox.Width + Me.CheckBoxSpacer.Width)
            Else
                Me.TimeCombo.Width = Me.Width
            End If
        End If
    End Sub

    Protected Overrides ReadOnly Property DefaultSize() As System.Drawing.Size
        Get
            Return CalcMinimumSize()
        End Get
    End Property

    Private Function CalcMinimumSize() As Size
        If _initialised Then
            GetDateTimeWidths()
            Dim s As New Size(0, Me.DateCombo.Height)
            If Me.ShowDate AndAlso Me.ShowTime Then
                s.Width = _dateWidth + _timeWidth + Me.DateTimeSpacer.Width
            ElseIf Me.ShowDate Then
                s.Width = _dateWidth
            Else ' Me.ShowTime
                s.Width = _timeWidth
            End If
            If Me.ShowCheckBox Then
                s.Width += Me.CheckBoxSpacer.Width + Me.HaveValueCheckBox.Width
            End If
            Return s
        Else
            Return MyBase.MinimumSize
        End If
    End Function

    <Localizable(True)> _
    Public Overrides Property MinimumSize() As System.Drawing.Size
        Get
            Return MyBase.MinimumSize
        End Get
        Set(ByVal value As System.Drawing.Size)
            MyBase.MinimumSize = value
            DrawValue()
        End Set
    End Property

    'Private Sub RecalcMinimumSize()
    '    If Not _initialised Then Return
    '    Me.MinimumSize = CalcMinimumSize()
    'End Sub

    'Private Function ShouldSerializeMinimumSize() As Boolean
    '    Return Not Size.Equals(Me.MinimumSize, Me.CalcMinimumSize)
    'End Function

    Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
        MyBase.OnFontChanged(e)
        If Not _initialised Then Return
        Me.DateCombo.Font = MyBase.Font
        Me.TimeCombo.Font = MyBase.Font
        Me.DatePopup.Font = MyBase.Font
        Me.TimePopup.Font = MyBase.Font
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        MyBase.OnSizeChanged(e)
        If Not _initialised Then Return
        DrawValue()
    End Sub
#End Region

#Region "Draw Value"
    Private Delegate Sub DrawValueDelegate()

    Private Sub DrawDate(ByVal v As DateTime)
        If v = noDate Then
            Me.DateCombo.Text = String.Empty
            Return
        End If
        Select Case Me.DateFormat
            Case DateTimePickerFormat.Custom
                Me.DateCombo.Text = v.ToString(_customDateFormat)
            Case DateTimePickerFormat.Long
                Me.DateCombo.Text = v.ToLongDateString
            Case DateTimePickerFormat.Short
                Me.DateCombo.Text = v.ToShortDateString
        End Select
        Me.DateCombo.Select(0, 0)
    End Sub

    Private Sub DrawTime(ByVal v As DateTime)
        Select Case Me.TimeFormat
            Case DateTimePickerFormat.Custom
                Me.TimeCombo.Text = v.ToString(_customTimeFormat)
            Case DateTimePickerFormat.Long
                Me.TimeCombo.Text = v.ToLongTimeString
            Case DateTimePickerFormat.Short
                Me.TimeCombo.Text = v.ToString(DefaultCustomTimeFormat)
        End Select
        Me.TimeCombo.Select(0, 0)
    End Sub

    Private Sub DrawValue()
        If Not _initialised Then Return
        If Me.InvokeRequired Then
            Dim dvd As New DrawValueDelegate(AddressOf DrawValue)
            Me.Invoke(dvd)
        Else
            _inDrawValue = True
            Me.SetSizes()
            Dim v As DateTime
            If Me.HasValue OrElse (Not Me.ShowCheckBox) Then
                v = Me.DisplayValue
                DrawDate(v)
                DrawTime(v)
                Me.DateCombo.Enabled = True
                Me.TimeCombo.Enabled = True
                If Not Me._settingNullState Then Me.HaveValueCheckBox.Checked = True
            Else
                Me.DateCombo.Text = String.Empty
                Me.TimeCombo.Text = String.Empty
                Me.DateCombo.Enabled = False
                Me.TimeCombo.Enabled = False
                If Not Me._settingNullState Then Me.HaveValueCheckBox.Checked = False
                'ElseIf Me._preNullValue <> noDate Then
                '    v = Me._preNullValue
                'Else
                '    v = Me.DefaultValue
                '    If Me.DisplayInLocalTime Then v = v.ToLocalTime
            End If


            'Me.HaveValueCheckBox.Checked = Me.HasValue
            'Dim enableDropDowns As Boolean = Me.HaveValueCheckBox.Checked OrElse Not Me.ShowCheckBox

            _inDrawValue = False
        End If
    End Sub
#End Region

    Private Sub timer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer.Elapsed
        Me.CloseDropDown(Me._timerTarget)
    End Sub

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Overrides Sub OnValidated(ByVal e As System.EventArgs)
        MyBase.OnValidated(e)
        RaiseEvent Validated(Me, e)
    End Sub

End Class
