Imports System.ComponentModel
Imports activiser.Library.Forms

<ComponentModel.DesignTimeVisible(True)> _
Public Class ReadOnlyContextMenu : Inherits ContextMenuBase

    Public Sub New()
        MyBase.New(ContextMenuType.Readonly)
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

        If Not ShowCall Then Return ' not relevant if we don't have the make phone call menu item.

#If WINDOWSMOBILE Then
        If _showCall AndAlso HavePhone() Then
            If Not Me.MenuItems.Contains(CallMI) Then
                Me.MenuItems.Add(CallMI)
            End If
            CallMI.Enabled = False
        Else
            If Me.MenuItems.Contains(CallMI) Then
                Me.MenuItems.Remove(CallMI)
            End If
        End If
#End If

        Try
            Dim tb As TextBox = TryCast(Me.SourceControl, TextBox)
            If tb IsNot Nothing Then
                SelectAllMI.Enabled = True
#If WINDOWSMOBILE Then
                CallMI.Enabled = CanPhone(tb)
#End If
                Return
            End If

            Dim nb As NumberPicker = TryCast(Me.SourceControl, NumberPicker)
            If nb IsNot Nothing Then
                SelectAllMI.Enabled = True
#If WINDOWSMOBILE Then
                CallMI.Enabled = CanPhone(nb.Text)
#End If            
                Return
            End If

            Dim lb As Label = TryCast(Me.SourceControl, Label)
            If lb IsNot Nothing Then
                SelectAllMI.Enabled = False
#If WINDOWSMOBILE Then
                CallMI.Enabled = CanPhone(lb.Text)
#End If            
                Return
            End If

            Dim cb As ComboBox = TryCast(Me.SourceControl, ComboBox)
            If cb IsNot Nothing Then
                SelectAllMI.Enabled = False
#If WINDOWSMOBILE Then
                If cb.SelectedText.Length <> 0 Then
                    CallMI.Enabled = CanPhone(cb.SelectedText)
                Else
                    CallMI.Enabled = CanPhone(cb.Text)
                End If
#End If            
                Return
            End If

            SelectAllMI.Enabled = False
#If WINDOWSMOBILE Then
            CallMI.Enabled = False
#End If            

        Catch 'ex As Exception
#If WINDOWSMOBILE Then
            CallMI.Enabled = False
#End If            
        End Try
    End Sub
End Class
