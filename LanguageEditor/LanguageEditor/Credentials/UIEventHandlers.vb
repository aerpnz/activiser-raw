Imports system.Globalization

Module UIEventHandlers
#Region "Cut/Copy/Paste/Delete ContextMenu Methods"
    Public Sub ContextMenuPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As System.Windows.Forms.MenuItem = TryCast(sender, System.Windows.Forms.MenuItem)
        If mi IsNot Nothing Then
            Dim cm As System.Windows.Forms.ContextMenu = TryCast(mi.Parent, System.Windows.Forms.ContextMenu)
            If cm IsNot Nothing Then
                Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
                If tb IsNot Nothing Then
                    Dim clipText As String = Clipboard.GetText()
                    If tb.Text.Length - tb.SelectedText.Length + clipText.Length <= tb.MaxLength Then
                        If tb.Enabled AndAlso Not tb.ReadOnly Then tb.SelectedText = clipText
                    End If
                    Exit Sub
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
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Public Sub ContextMenuCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As System.Windows.Forms.MenuItem = TryCast(sender, System.Windows.Forms.MenuItem)
        If mi IsNot Nothing Then
            Dim cm As System.Windows.Forms.ContextMenu = TryCast(mi.Parent, System.Windows.Forms.ContextMenu)
            If cm IsNot Nothing Then
                Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
                If tb IsNot Nothing Then
                    Clipboard.SetText(tb.SelectedText)
                    Exit Sub
                End If
                Dim dt As DateTimePicker = TryCast(cm.SourceControl, DateTimePicker)
                If dt IsNot Nothing Then
                    Clipboard.SetText(dt.Text)
                End If
            End If
        End If
    End Sub

    Public Sub ContextMenuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As System.Windows.Forms.MenuItem = TryCast(sender, System.Windows.Forms.MenuItem)
        If mi IsNot Nothing Then
            Dim cm As System.Windows.Forms.ContextMenu = TryCast(mi.Parent, System.Windows.Forms.ContextMenu)
            If cm IsNot Nothing Then
                Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
                If tb IsNot Nothing Then
                    If tb.Enabled AndAlso Not tb.ReadOnly Then tb.SelectedText = String.Empty
                End If
            End If
        End If
    End Sub

    Public Sub ContextMenuCut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As System.Windows.Forms.MenuItem = TryCast(sender, System.Windows.Forms.MenuItem)
        If mi IsNot Nothing Then
            Dim cm As System.Windows.Forms.ContextMenu = TryCast(mi.Parent, System.Windows.Forms.ContextMenu)
            If cm IsNot Nothing Then
                Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
                If tb IsNot Nothing Then
                    Dim clipText As String = tb.SelectedText
                    Clipboard.SetText(clipText)
                    If tb.Enabled AndAlso Not tb.ReadOnly Then tb.SelectedText = String.Empty
                End If
            End If
        End If
    End Sub

    Public Sub ContextMenuSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mi As System.Windows.Forms.MenuItem = TryCast(sender, System.Windows.Forms.MenuItem)
        If mi IsNot Nothing Then
            Dim cm As System.Windows.Forms.ContextMenu = TryCast(mi.Parent, System.Windows.Forms.ContextMenu)
            If cm IsNot Nothing Then
                Dim tb As TextBox = TryCast(cm.SourceControl, TextBox)
                If tb IsNot Nothing Then
                    tb.Focus()
                    tb.SelectAll()
                End If
            End If
        End If
    End Sub

    'Private Sub ContextMenu_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim mi As System.Windows.Forms.MenuItem = TryCast(sender, System.Windows.Forms.MenuItem)
    '    If mi IsNot Nothing Then
    '        Dim cm As System.Windows.Forms.ContextMenu = TryCast(mi.Parent, System.Windows.Forms.ContextMenu)
    '        If cm IsNot Nothing Then
    '        End If
    '    End If
    'End Sub
#End Region
End Module
