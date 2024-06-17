Imports System.ComponentModel

Friend Class TimePickerPopup
    Friend Event SelectedValueChanged As EventHandler

    Friend ReadOnly DefaultTimeFormat As String = GetShortTimeFormat(Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern)
    Private _anchorControl As Control
    Private _timeFormat As String = DefaultTimeFormat
    Private _startValue As DateTime = DateTime.MinValue
    Private _maxValue As DateTime = DateTime.MaxValue
    Private _topValue As TimeSpan = TimeSpan.FromTicks(Math.Min(_startValue.AddDays(1).Ticks, _maxValue.Ticks) - _startValue.Ticks)
    Private _timeInterval As Integer
    Private _intervalTimeSpan As TimeSpan
    Private _showTimeDifference As Boolean
    Private _includeZeroTime As Boolean = True
    Private _minWidth As Integer = 112
    Private _maxItems As Integer = 8

    Private _inSelectingValue As Boolean
    Private _inSelectingIndex As Boolean


#Region "ctor"
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.TimeList.FormatString = DefaultTimeFormat
        Me.TimeInterval = 15
    End Sub
#End Region

#Region "Friend Properties"
    Friend Property TimeFormat() As String
        Get
            Return _timeFormat
        End Get
        Set(ByVal value As String)
            _timeFormat = value
        End Set
    End Property

    Friend Property TimeInterval() As Integer
        Get
            Return _timeInterval
        End Get
        Set(ByVal value As Integer)
            If value <= 0 OrElse value > 720 Then ' even 720 is silly - 12 hours.
                Throw New ArgumentOutOfRangeException("value")
            Else
                _timeInterval = value
                _intervalTimeSpan = TimeSpan.FromMinutes(value)
                Populate()
            End If
        End Set
    End Property

    Friend Property StartValue() As DateTime
        Get
            Return _startValue
        End Get
        Set(ByVal value As DateTime)
            _startValue = value
        End Set
    End Property

    Friend Property MaxValue() As DateTime
        Get
            Return _maxValue
        End Get
        Set(ByVal value As DateTime)
            _maxValue = value
        End Set
    End Property

    Friend Property ShowTimeDifference() As Boolean
        Get
            Return _showTimeDifference
        End Get
        Set(ByVal value As Boolean)
            If value <> _showTimeDifference Then
                _showTimeDifference = value
            End If
        End Set
    End Property

    Friend Property IncludeZeroTime() As Boolean
        Get
            Return _includeZeroTime
        End Get
        Set(ByVal value As Boolean)
            If value <> _includeZeroTime Then
                _includeZeroTime = value
            End If
        End Set
    End Property

    Public Property SelectedIndex() As Integer
        Get
            Return Me.TimeList.SelectedIndex
        End Get
        Set(ByVal value As Integer)
            Me.TimeList.SelectedIndex = value
        End Set
    End Property

    Public Property SelectedValue() As TimeSpan
        Get
            Dim result As TimeSpan = Me.StartValue.TimeOfDay + CType(Me.TimeList.Items(Me.TimeList.SelectedIndex), TimeSpan)
            Return result
        End Get
        Set(ByVal value As TimeSpan)
            _inSelectingValue = True
            Try
                If value > Me._topValue + TimeSpan.FromDays(1) Then
                    Throw New ArgumentOutOfRangeException("value")
                End If
                If Me.TimeList.Items.Contains(Me.StartValue.TimeOfDay + value) Then
                    Me.TimeList.SelectedIndex = Me.TimeList.Items.IndexOf(value)
                Else
                    Me.TimeList.SelectedIndex = -1
                    For i As Integer = 0 To Me.TimeList.Items.Count - 1
                        Dim itemTime As System.TimeSpan = Me.StartValue.TimeOfDay + CType(Me.TimeList.Items(i), TimeSpan)
                        'Debug.WriteLine(itemTime)
                        If itemTime >= value Then
                            If i > 0 Then
                                Me.TimeList.SelectedIndex = i
                                Exit For
                            End If
                        End If
                    Next
                End If
            Catch
            Finally
                _inSelectingValue = False
            End Try
        End Set
    End Property

    Public Property MinimumWidth() As Integer
        Get
            Return _minWidth
        End Get
        Set(ByVal value As Integer)
            _minWidth = value
        End Set
    End Property

    Public Property MaximumItems() As Integer
        Get
            Return _maxItems
        End Get
        Set(ByVal value As Integer)
            _maxItems = value
        End Set
    End Property

    Public Property AnchorControl() As Control
        Get
            Return _anchorControl
        End Get
        Set(ByVal value As Control)
            _anchorControl = value
            If value IsNot Nothing Then
                locateDropDown(Me, Me._anchorControl, False)
            End If
        End Set
    End Property
#End Region

#Region "Private Methods"
    Private Sub SetSize()
        Dim testString As String
        If Me.ShowTimeDifference Then
            testString = String.Format(My.Resources.TimePickerHoursTemplate, New DateTime(2007, 1, 13, 23, 59, 59).ToString(Me.TimeFormat), 1.25)
        Else
            testString = New DateTime(2007, 1, 13, 23, 59, 59).ToString(Me.TimeFormat)
        End If
        testString &= ".."
        Dim g As Graphics = Me.CreateGraphics
        Dim s As Size = g.MeasureString(testString, Me.Font).ToSize
        Me.TimeList.Size = New Size(Math.Max(Me.MinimumWidth, s.Width + SystemInformation.VerticalScrollBarWidth), s.Height * MaximumItems)
        Me.Size = Me.TimeList.Size
    End Sub

    Private Sub Populate()
        Me.SuspendLayout()
        Me.TimeList.Items.Clear()

        Dim startTime As TimeSpan = TimeSpan.Zero
        Dim stopTime As New TimeSpan(23, 59, 59)

        For itemTime As TimeSpan = startTime To stopTime Step _intervalTimeSpan
            Me.TimeList.Items.Add(itemTime)
        Next

        Me.ResumeLayout(True)
    End Sub

    'Private Sub Populate()
    '    Me.SuspendLayout()
    '    Me.TimeList.Items.Clear()

    '    _topValue = _startValue.TimeOfDay + TimeSpan.FromTicks(Math.Min(_startValue.AddDays(1).Ticks, _maxValue.Ticks) - _startValue.Ticks)
    '    Debug.WriteLine(String.Format("_startvalue = {0}, _topValue = {1}", _startValue, _topValue))
    '    Dim startTime As TimeSpan = Me._startValue.TimeOfDay

    '    If Me.ShowTimeDifference AndAlso Not _includeZeroTime Then
    '        startTime += _intervalTimeSpan
    '    End If

    '    For itemTime As TimeSpan = startTime To _topValue Step _intervalTimeSpan
    '        Me.TimeList.Items.Add(itemTime)
    '    Next

    '    Me.ResumeLayout(True)
    'End Sub
#End Region

#Region "Component Event Handlers"
    Private Sub TimeList_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TimeList.DrawItem
        If e.Index = -1 Then Return
        Dim itemTime As System.TimeSpan = CType(Me.TimeList.Items.Item(e.Index), TimeSpan)
        Dim diffTime As System.TimeSpan = itemTime ' - StartValue.TimeOfDay
        Dim dt As DateTime = Me.StartValue + diffTime

        Dim itemText As String
        Dim dtText As String = dt.ToString(_timeFormat)
        If Me.ShowTimeDifference Then
            Dim format As String
            If diffTime.TotalHours < 1 Then
                If diffTime.Minutes = 1 Then
                    format = My.Resources.TimePickerMinuteTemplate
                Else
                    format = My.Resources.TimePickerMinutesTemplate
                End If
            ElseIf diffTime.TotalHours = 1 Then
                format = My.Resources.TimePickerHourTemplate
            Else
                format = My.Resources.TimePickerHoursTemplate
            End If
            itemText = String.Format(format, dtText, diffTime.TotalHours, diffTime.Minutes)
        Else
            itemText = dtText
        End If
        e.DrawBackground()
        e.Graphics.DrawString(itemText, e.Font, New SolidBrush(e.ForeColor), New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
    End Sub

    Private Sub TimeList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimeList.SelectedIndexChanged
        If _inSelectingValue Then Return
        RaiseEvent SelectedValueChanged(Me, New System.EventArgs())
    End Sub
#End Region

#Region "Overridden Event Handlers"
    Protected Overrides Sub OnDeactivate(ByVal e As System.EventArgs)
        MyBase.OnDeactivate(e)
        Me.Hide()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        MyBase.OnSizeChanged(e)
        If Me._anchorControl IsNot Nothing Then
            locateDropDown(Me, Me._anchorControl, False)
        End If
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.TimeList.ForeColor = MyBase.ForeColor
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
        MyBase.OnFontChanged(e)
    End Sub
#End Region

#Region "Friend Methods"
    Friend Sub Popup()
        Me.Popup(Me.StartValue.TimeOfDay)
    End Sub

    Friend Sub Popup(ByVal selectedValue As TimeSpan)
        Me.SetSize()
        If Me._anchorControl IsNot Nothing Then
            locateDropDown(Me, Me._anchorControl, False)
        End If
        'Me.Populate()
        Me.SuspendLayout()
        Me.Show()
        Me.TopMost = True
        Me.SelectedValue = selectedValue
        Me.ResumeLayout()
        Application.DoEvents()
    End Sub
#End Region

    Private Sub TimePickerPopup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Populate()
    End Sub
End Class