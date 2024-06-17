Imports system.ComponentModel

<DefaultProperty("Value"), DefaultEvent("PropertyChanged")> _
Public Class DurationPicker
    Implements INotifyPropertyChanged
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Private _settingValue As Boolean

    Private Delegate Sub SetValueDelegate(ByVal value As Integer)
    Private Sub SetValue(ByVal value As Integer)
        If Me.InvokeRequired Then
            Dim svd As New SetValueDelegate(AddressOf SetValue)
            Me.Invoke(svd, value)
        Else
            _settingValue = True
            Dim mins, hours As Integer
            mins = value Mod 60
            hours = (value - mins) \ 60
            Me.Hours.Value = hours
            Me.Minutes.Value = mins
            _settingValue = False
        End If
    End Sub

    <Category("Data"), Browsable(True), DefaultValue(60), Bindable(True)> _
    Public Property Value() As Integer
        Get
            Return CInt(Me.Hours.Value) * 60 + CInt(Me.Minutes.Value)
        End Get
        Set(ByVal value As Integer)
            SetValue(value)
        End Set
    End Property

    Private Sub Time_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Hours.ValueChanged, Minutes.ValueChanged
        If _settingValue Then Return
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Value"))
    End Sub

End Class
