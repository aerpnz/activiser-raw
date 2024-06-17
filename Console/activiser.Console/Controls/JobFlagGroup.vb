Imports System.ComponentModel

<DefaultProperty("Value"), DefaultEvent("PropertyChanged")> _
Public Class JobFlagGroup
    Implements INotifyPropertyChanged
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Private _settingValue As Boolean
    Private _value As Integer

    Private Delegate Sub SetValueDelegate(ByVal value As Integer)
    Private Sub SetValue(ByVal value As Integer)
        If Me.InvokeRequired Then
            Dim svd As New SetValueDelegate(AddressOf SetValue)
            Me.Invoke(svd, value)
        Else
            _settingValue = True
            If _value <> value Then
                _value = value
                AdministrationCheckBox.Checked = (value And 2) = 2
                ManagementCheckBox.Checked = (value And 1) = 1
            End If
            _settingValue = False
        End If
    End Sub

    <Category("Data"), Browsable(True), DefaultValue(0), Bindable(True)> _
        Public Property Value() As Integer
        Get
            Return _value
        End Get
        Set(ByVal value As Integer)
            SetValue(value)
        End Set
    End Property

    Private _adminEnabled As Boolean
    <DefaultValue(False), Category("Behavior")> _
    Public Property AdminEnabled() As Boolean
        Get
            Return _adminEnabled
        End Get
        Set(ByVal value As Boolean)
            _adminEnabled = value
            Me.AdministrationCheckBox.Enabled = value
        End Set
    End Property

    Private _managmentEnabled As Boolean
    <DefaultValue(False), Category("Behavior")> _
    Public Property ManagementEnabled() As Boolean
        Get
            Return _managmentEnabled
        End Get
        Set(ByVal value As Boolean)
            _managmentEnabled = value
            Me.ManagementCheckBox.Enabled = value
        End Set
    End Property

    Private Sub AdministrationCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdministrationCheckBox.CheckedChanged
        If _settingValue Then Return
        If AdministrationCheckBox.Checked Then
            Value = Value Or 2
        Else
            Value = Value And Not 2
        End If
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Value"))
    End Sub

    Private Sub ManagementCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ManagementCheckBox.CheckedChanged
        If _settingValue Then Return
        If ManagementCheckBox.Checked Then
            Value = Value Or 1
        Else
            Value = Value And Not 1
        End If
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Value"))
    End Sub

End Class
