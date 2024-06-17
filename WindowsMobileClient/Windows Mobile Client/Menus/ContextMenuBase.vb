Imports activiser.Library.Forms

Public Enum ContextMenuType
    [Normal]
    [ReadOnly]
    [Password]
End Enum

<ComponentModel.DesignTimeVisible(True)> _
Public Class ContextMenuBase : Inherits ContextMenu

    Protected WithEvents CutMI As System.Windows.Forms.MenuItem
    Protected WithEvents CopyMI As System.Windows.Forms.MenuItem
    Protected WithEvents PasteMI As System.Windows.Forms.MenuItem
    Protected WithEvents ClearMI As System.Windows.Forms.MenuItem
    Protected WithEvents SelectAllMI As System.Windows.Forms.MenuItem
    Protected WithEvents CallMI As System.Windows.Forms.MenuItem

    Private _menuType As ContextMenuType = ContextMenuType.Normal
    Private _showCall As Boolean

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal menuType As ContextMenuType)
        MyBase.New()
        _menuType = menuType
        Select Case menuType
            Case ContextMenuType.Normal
                CutMI = New System.Windows.Forms.MenuItem
                CopyMI = New System.Windows.Forms.MenuItem
                PasteMI = New System.Windows.Forms.MenuItem
                ClearMI = New System.Windows.Forms.MenuItem
                SelectAllMI = New System.Windows.Forms.MenuItem
            Case ContextMenuType.Password
                PasteMI = New System.Windows.Forms.MenuItem
                ClearMI = New System.Windows.Forms.MenuItem
                SelectAllMI = New System.Windows.Forms.MenuItem
            Case ContextMenuType.ReadOnly
                CopyMI = New System.Windows.Forms.MenuItem
                SelectAllMI = New System.Windows.Forms.MenuItem
        End Select

        If CutMI IsNot Nothing Then
            CutMI.Text = Terminology.GetString("ContextMenu", RES_Cut)
            Me.AddMenuItem(CutMI)
            AddHandler CutMI.Click, AddressOf ContextMenuCut_Click
        End If

        If CopyMI IsNot Nothing Then
            CopyMI.Text = Terminology.GetString("ContextMenu", RES_Copy)
            Me.AddMenuItem(CopyMI)
            AddHandler CopyMI.Click, AddressOf ContextMenuCopy_Click
        End If

        If PasteMI IsNot Nothing Then
            PasteMI.Text = Terminology.GetString("ContextMenu", RES_Paste)
            Me.AddMenuItem(PasteMI)
            AddHandler PasteMI.Click, AddressOf ContextMenuPaste_Click
        End If

        If ClearMI IsNot Nothing Then
            ClearMI.Text = Terminology.GetString("ContextMenu", RES_Clear)
            Me.AddMenuItem(ClearMI)
            AddHandler ClearMI.Click, AddressOf ContextMenuClear_Click
        End If

        If SelectAllMI IsNot Nothing Then
            SelectAllMI.Text = Terminology.GetString("ContextMenu", RES_SelectAll)
            Me.AddMenuItem(SelectAllMI)
            AddHandler SelectAllMI.Click, AddressOf ContextMenuSelectAll_Click
        End If
#If WINDOWSMOBILE Then
        If menuType <> ContextMenuType.Password AndAlso HavePhone() Then
            _showCall = True
            CallMI = New System.Windows.Forms.MenuItem
            CallMI.Text = Terminology.GetString("ContextMenu", RES_Call)
            Me.AddMenuItem(CallMI)
            AddHandler CallMI.Click, AddressOf ContextMenuCall_Click

            'TODO: Add SMS Support
            'SmsMI = New System.Windows.Forms.MenuItem
            'Me.SmsMI.Text = "Send Text"
        End If
#End If
    End Sub

    Public Sub AddMenuItem(ByVal item As MenuItem)
        MyBase.MenuItems.Add(item)
    End Sub




    Friend Sub ContextMenuSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As MenuItem = TryCast(sender, MenuItem)
        If mi Is Nothing Then Return ' Que ?!

        Dim cm As ContextMenu = TryCast(mi.Parent, ContextMenu)
        If cm Is Nothing Then Return ' Que ?!

        Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
        If tb IsNot Nothing Then
            tb.Focus()
            tb.SelectAll()
        End If

        Dim nb As NumberPicker = TryCast(cm.SourceControl, NumberPicker)
        If nb IsNot Nothing Then
            nb.Focus()
            'nb.SelectAll()
            Return
        End If

    End Sub

    Friend Sub ContextMenuClear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As MenuItem = TryCast(sender, MenuItem)
        If mi Is Nothing Then Return ' Que ?!

        Dim cm As ContextMenu = TryCast(mi.Parent, ContextMenu)
        If cm Is Nothing Then Return ' Que ?!

        Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
        If tb IsNot Nothing Then
            If tb.Enabled AndAlso Not tb.ReadOnly Then tb.SelectedText = String.Empty
            Return
        End If

        Dim nb As NumberPicker = TryCast(cm.SourceControl, NumberPicker)
        If nb IsNot Nothing Then
            If nb.Enabled AndAlso Not nb.ReadOnly Then nb.Value = nb.DefaultValue
            Return
        End If
    End Sub

    Friend Sub ContextMenuCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As MenuItem = TryCast(sender, MenuItem)
        If mi Is Nothing Then Return ' Que ?!

        Dim cm As ContextMenu = TryCast(mi.Parent, ContextMenu)
        If cm Is Nothing Then Return ' Que ?!

        Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
        If tb IsNot Nothing Then
            Clipboard.SetText(tb.SelectedText)
            Return
        End If

        Dim dt As DateTimePicker = TryCast(cm.SourceControl, DateTimePicker)
        If dt IsNot Nothing Then
            Clipboard.SetText(dt.Text)
            Return
        End If

        Dim nb As NumberPicker = TryCast(cm.SourceControl, NumberPicker)
        If nb IsNot Nothing Then
            Clipboard.SetText(nb.Text)
            Return
        End If

        Dim cb As ComboBox = TryCast(cm.SourceControl, ComboBox)
        If cb IsNot Nothing Then
            Clipboard.SetText(cb.Text)
            Return
        End If

        Dim lbx As ListBox = TryCast(cm.SourceControl, ListBox)
        If lbx IsNot Nothing Then
            Clipboard.SetText(lbx.Text)
            Return
        End If

        Dim lb As Label = TryCast(cm.SourceControl, Label)
        If lb IsNot Nothing Then
            Clipboard.SetText(lb.Text)
            Return
        End If
    End Sub

    Friend Sub ContextMenuCut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As MenuItem = TryCast(sender, MenuItem)
        If mi Is Nothing Then Return ' Que ?!

        Dim cm As ContextMenu = TryCast(mi.Parent, ContextMenu)
        If cm Is Nothing Then Return ' Que ?!

        Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
        If tb IsNot Nothing Then
            Dim clipText As String = tb.SelectedText
            Clipboard.SetText(clipText)
            If tb.Enabled AndAlso Not tb.ReadOnly Then tb.SelectedText = String.Empty
            Return
        End If

        Dim nb As NumberPicker = TryCast(cm.SourceControl, NumberPicker)
        If nb IsNot Nothing Then
            Dim clipText As String = nb.Text
            Clipboard.SetText(clipText)
            If nb.Enabled AndAlso Not nb.ReadOnly Then nb.Value = nb.DefaultValue
            Return
        End If
    End Sub

    Friend Sub ContextMenuPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As MenuItem = TryCast(sender, MenuItem)
        If mi Is Nothing Then Return ' Que ?!

        Dim cm As ContextMenu = TryCast(mi.Parent, ContextMenu)
        If cm Is Nothing Then Return ' Que ?!

        Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
        If tb IsNot Nothing Then
            Dim clipText As String = Clipboard.GetText()
            If tb.Text.Length - tb.SelectedText.Length + clipText.Length <= tb.MaxLength Then
                If tb.Enabled AndAlso Not tb.ReadOnly Then tb.SelectedText = clipText
            End If
            Return
        End If

        Dim dt As DateTimePicker = TryCast(cm.SourceControl, DateTimePicker)
        If dt IsNot Nothing Then
            Dim clipText As String = Clipboard.GetText()
            If Not String.IsNullOrEmpty(clipText) Then
                Dim newValue As DateTime
                Try
                    newValue = DateTime.Parse(clipText, CultureInfo.CurrentCulture)
                    dt.Value = newValue
                Catch ex As FormatException
                    Beep()
                End Try
            End If
            Return
        End If

        Dim nb As NumberPicker = TryCast(cm.SourceControl, NumberPicker)
        If nb IsNot Nothing Then
            Dim clipText As String = Clipboard.GetText()
            If Not String.IsNullOrEmpty(clipText) Then
                Dim newValue As Decimal
                Try
                    newValue = Decimal.Parse(clipText, CultureInfo.CurrentCulture)
                    nb.Value = newValue
                Catch ex As FormatException
                    Beep()
                End Try
            End If
            Return
        End If

        'TODO: test that this works.
        Dim cb As ComboBox = TryCast(cm.SourceControl, ComboBox)
        If cb IsNot Nothing Then
            cb.Focus()
            cb.Text = Clipboard.GetText()
        End If

        Dim lbx As ListBox = TryCast(cm.SourceControl, ListBox)
        If lbx IsNot Nothing Then
            lbx.Focus()
            lbx.Text = Clipboard.GetText()
        End If

    End Sub

#If WINDOWSMOBILE Then
    Friend Sub ContextMenuCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mi As MenuItem = TryCast(sender, MenuItem)
        If mi Is Nothing Then Return ' Que ?!

        Dim cm As ContextMenu = TryCast(mi.Parent, ContextMenu)
        If cm Is Nothing Then Return ' Que ?!

        Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
        If tb IsNot Nothing Then
            MakePhoneCall(tb)
            Return
        End If

        Dim nb As NumberPicker = TryCast(cm.SourceControl, NumberPicker)
        If nb IsNot Nothing Then
            MakePhoneCall(nb.Text)
            Return
        End If

        Dim cb As ComboBox = TryCast(cm.SourceControl, ComboBox)
        If cb IsNot Nothing Then
            MakePhoneCall(cb.Text)
        End If

        Dim lbx As ListBox = TryCast(cm.SourceControl, ListBox)
        If lbx IsNot Nothing Then
            MakePhoneCall(lbx.Text)
        End If
    End Sub
#End If

    'Private Function ValidateEmailAddress(ByVal strEmailAddress As String) As Boolean
    '    Dim regEx As New System.Text.RegularExpressions.Regex("^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")
    '    Return regEx.IsMatch(strEmailAddress)
    'End Function

    'Private Function CanMail(ByVal text As String) As Boolean
    '    Return ValidateEmailAddress(text)
    'End Function

    'Private Sub MailMI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MailMI.Click
    '    Dim tb As TextBox = TryCast(Me.SourceControl, TextBox)
    '    If tb IsNot Nothing Then
    '        SendEmail(tb)
    '        Return
    '    End If

    '    Dim nb As NumberPicker = TryCast(Me.SourceControl, NumberPicker)
    '    If nb IsNot Nothing Then
    '        SendEmail(nb.SelectedText)
    '        Return
    '    End If

    '    Dim cb As ComboBox = TryCast(Me.SourceControl, ComboBox)
    '    If cb IsNot Nothing Then
    '        SendEmail(cb.Text)
    '    End If
    'End Sub
End Class
