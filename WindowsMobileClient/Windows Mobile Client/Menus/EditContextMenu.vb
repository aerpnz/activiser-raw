Imports System.ComponentModel
Imports activiser.Library.Forms

<ComponentModel.DesignTimeVisible(True)> _
Public Class EditContextMenu : Inherits ContextMenuBase

    Public Sub New()
        MyBase.New(ContextMenuType.Normal)
    End Sub

    Private _showCall As Boolean = True
    Public Property ShowCall() As Boolean
        Get
            Return _showCall
        End Get
        Set(ByVal value As Boolean)
            _showCall = value
        End Set
    End Property

    Protected Overrides Sub OnPopup(ByVal e As System.EventArgs)
        MyBase.OnPopup(e)
        Dim enableCuts As Boolean
        Dim enablePastes As Boolean
        Dim enablePhone As Boolean
        Dim enableSelectAll As Boolean
        'Dim enableMail As Boolean

        ' probly not the prettiest, but looks slightly better than a nested if...

        If TestTextBox(TryCast(Me.SourceControl, TextBox), enableCuts, enablePastes, enablePhone, enableSelectAll) Then
            'do nothing
        ElseIf TestNumberPicker(TryCast(Me.SourceControl, NumberPicker), enableCuts, enablePastes, enablePhone, enableSelectAll) Then
            'do nothing
        ElseIf TestComboBox(TryCast(Me.SourceControl, ComboBox), enableCuts, enablePastes, enablePhone, enableSelectAll) Then
            'do nothing
        ElseIf TestDateTimePicker(TryCast(Me.SourceControl, DateTimePicker), enableCuts, enablePastes, enablePhone, enableSelectAll) Then
            'do nothing
        Else
            TestLabel(TryCast(Me.SourceControl, Label), enableCuts, enablePastes, enablePhone, enableSelectAll)
        End If

#If WINDOWSMOBILE Then
        If _showCall AndAlso HavePhone() Then
            If Not Me.MenuItems.Contains(CallMI) Then
                Me.MenuItems.Add(CallMI)
            End If
            CallMI.Enabled = enablePhone
        Else
            If Me.MenuItems.Contains(CallMI) Then
                Me.MenuItems.Remove(CallMI)
            End If
        End If
#End If

        CutMI.Enabled = enableCuts
        ClearMI.Enabled = enableCuts
        PasteMI.Enabled = enablePastes
        SelectAllMI.Enabled = enableSelectAll
    End Sub

    Private Shared Function TestTextBox(ByVal tb As TextBox, ByRef enableCuts As Boolean, ByRef enablePastes As Boolean, ByRef enablePhone As Boolean, ByRef enableSelectAll As Boolean) As Boolean
        If tb Is Nothing Then Return False
        enableCuts = tb.Enabled AndAlso Not tb.ReadOnly
        enablePastes = enableCuts
        enableSelectAll = True
#If WINDOWSMOBILE Then
        enablePhone = CanPhone(tb)
#End If
        Return True
    End Function

    Private Shared Function TestNumberPicker(ByVal nb As NumberPicker, ByRef enableCuts As Boolean, ByRef enablePastes As Boolean, ByRef enablePhone As Boolean, ByRef enableSelectAll As Boolean) As Boolean
        If nb Is Nothing Then Return False
        enableCuts = nb.Enabled AndAlso Not nb.ReadOnly
        enablePastes = enableCuts
        enableSelectAll = False
#If WINDOWSMOBILE Then
        enablePhone = CanPhone(nb.Text)
#End If       
        Return True
    End Function

    Private Shared Function TestComboBox(ByVal cb As ComboBox, ByRef enableCuts As Boolean, ByRef enablePastes As Boolean, ByRef enablePhone As Boolean, ByRef enableSelectAll As Boolean) As Boolean
        If cb Is Nothing Then Return False
        enableCuts = cb.Enabled AndAlso cb.DropDownStyle = ComboBoxStyle.DropDown
        enablePastes = cb.Enabled
        enableSelectAll = False
#If WINDOWSMOBILE Then
        enablePhone = CanPhone(cb.Text)
#End If       
        Return True
    End Function

    Private Shared Function TestDateTimePicker(ByVal dt As DateTimePicker, ByRef enableCuts As Boolean, ByRef enablePastes As Boolean, ByRef enablePhone As Boolean, ByRef enableSelectAll As Boolean) As Boolean
        If dt Is Nothing Then Return False
        enableCuts = False
        enablePastes = dt.Enabled
        enablePhone = False
        enableSelectAll = False
        Return True
    End Function

    Private Shared Function TestLabel(ByVal lb As Label, ByRef enableCuts As Boolean, ByRef enablePastes As Boolean, ByRef enablePhone As Boolean, ByRef enableSelectAll As Boolean) As Boolean
        If lb Is Nothing Then Return False
        enableCuts = False
        enablePastes = False
        enableSelectAll = False
#If WINDOWSMOBILE Then
        enablePhone = CanPhone(lb.Text)
#End If
        Return True
    End Function
End Class