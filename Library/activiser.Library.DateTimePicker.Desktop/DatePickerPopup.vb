Imports System.ComponentModel

Friend Class DatePickerPopup
    Friend Event SelectedValueChanged As EventHandler ' Of DatePickerValueChangedEventArgs)

    Private Shared ReadOnly borderSize As New Size(2, 2)
    Private _value As DateTime
    Private _anchorControl As Control

#Region "Friend Properties"
    Friend Property Value() As DateTime
        Get
            Return _value
        End Get
        Set(ByVal value As DateTime)
            _value = value
            Me.Calendar.SelectionStart = value
        End Set
    End Property

    Friend Property AnchorControl() As Control
        Get
            Return _anchorControl
        End Get
        Set(ByVal value As Control)
            _anchorControl = value
            If value IsNot Nothing Then
                locateDropDown(Me, Me._anchorControl, True)
            End If
        End Set
    End Property
#End Region

#Region "Friend Methods"
    Friend Sub Popup()
        If Me._anchorControl IsNot Nothing Then
            locateDropDown(Me, Me._anchorControl, True)
        End If
        Me.Show()
        Me.TopMost = True
    End Sub
#End Region

#Region "Component Event Handlers"
    Private Sub Calendar_DateSelected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles Calendar.DateSelected
        Me._value = Calendar.SelectionStart
        RaiseEvent SelectedValueChanged(Me, New System.EventArgs)
        'RaiseEvent ValueChanged(Me, New DatePickerValueChangedEventArgs(Me.Value))
    End Sub

    Private Sub CalendarPopup_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        Me.Hide()
    End Sub

    ' for changes to calendar size we don't control, like when week numbers are added
    Private Sub Calendar_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Calendar.SizeChanged
        SizeMe()
    End Sub

#End Region

#Region "Private Methods"
    Private Sub SizeMe()
        Me.Border.Size = Drawing.Size.Add(Me.Calendar.Size, borderSize)
        Me.Size = Drawing.Size.Add(Me.Calendar.Size, borderSize)
    End Sub
#End Region

#Region "Overridden Event Handlers"
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        SizeMe()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
        MyBase.OnFontChanged(e)
        SizeMe()
    End Sub

    Protected Overrides Sub OnShown(ByVal e As System.EventArgs)
        MyBase.OnShown(e)
        If Me._anchorControl IsNot Nothing Then
            locateDropDown(Me, Me._anchorControl, True)
        End If
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.Calendar.ForeColor = MyBase.ForeColor
    End Sub
#End Region

    '<DefaultValue(GetType(SystemColors), "WindowText")> _
    'Public Overrides Property ForeColor() As System.Drawing.Color
    '    Get
    '        Return MyBase.ForeColor
    '    End Get
    '    Set(ByVal value As System.Drawing.Color)
    '        MyBase.ForeColor = value
    '    End Set
    'End Property
End Class