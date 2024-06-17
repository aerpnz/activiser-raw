Imports System.ComponentModel

<ComponentModel.DesignTimeVisible(True)> _
Public Class PasswordContextMenu : Inherits ContextMenuBase

    Public Sub New()
        MyBase.New(ContextMenuType.Password)
    End Sub

    Protected Overrides Sub OnPopup(ByVal e As System.EventArgs)
        MyBase.OnPopup(e)
        Dim tb As TextBox = TryCast(SourceControl, TextBox)
        If tb IsNot Nothing Then
            PasteMI.Enabled = tb.Enabled AndAlso Not tb.ReadOnly
            ClearMI.Enabled = PasteMI.Enabled
            SelectAllMI.Enabled = PasteMI.Enabled
            Return
        End If
        PasteMI.Enabled = False
        ClearMI.Enabled = False
        SelectAllMI.Enabled = False
    End Sub
End Class