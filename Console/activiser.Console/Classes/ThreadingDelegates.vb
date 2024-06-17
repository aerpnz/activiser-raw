Friend Class ThreadingDelegates

    Private Sub New()
    End Sub

    Private Delegate Sub ControlEnabledDelegate(ByVal targetContainer As Control, ByVal targetItem As Control, ByVal enabled As Boolean)
    Friend Shared Sub ControlEnabled(ByVal targetContainer As Control, ByVal targetItem As Control, ByVal enabled As Boolean)
        If targetContainer.InvokeRequired Then
            Dim ecd As New ControlEnabledDelegate(AddressOf ControlEnabled)
            targetContainer.Invoke(ecd, targetContainer, targetItem, enabled)
        Else
            If targetItem.Enabled = enabled Then Return
            targetItem.Enabled = enabled
        End If
    End Sub

    Private Delegate Sub ToolStripItemEnabledDelegate(ByVal targetContainer As Control, ByVal targetItem As ToolStripItem, ByVal enabled As Boolean)
    Friend Shared Sub ToolStripItemEnabled(ByVal targetContainer As Control, ByVal targetItem As ToolStripItem, ByVal enabled As Boolean)
        If targetContainer.InvokeRequired Then
            Dim ecd As New ToolStripItemEnabledDelegate(AddressOf ToolStripItemEnabled)
            targetContainer.Invoke(ecd, targetContainer, targetItem, enabled)
        Else
            If targetItem.Enabled = enabled Then Return
            targetItem.Enabled = enabled
        End If
    End Sub

    Private Delegate Sub BindingSourceInvokeDelegate(ByVal targetContainer As Control, ByVal bs As BindingSource)
    Friend Shared Sub BindingSourceCancelEdit(ByVal targetContainer As Control, ByVal bs As BindingSource)
        'Return
        'TODO: make this work
        If targetContainer.InvokeRequired Then
            targetContainer.Invoke(New BindingSourceInvokeDelegate(AddressOf BindingSourceCancelEdit), targetContainer, bs)
        Else
            Try
                bs.EndEdit()
                CType(bs.Current, DataRowView).Row.RejectChanges()
                'bs.CancelEdit()
            Catch ex As System.ArgumentOutOfRangeException
                TraceWarning("CancelEdit failed: {0}", ex.ToString)
            End Try
        End If
    End Sub

    Friend Shared Sub BindingSourceEndEdit(ByVal targetContainer As Control, ByVal bs As BindingSource)
        If targetContainer.InvokeRequired Then
            targetContainer.Invoke(New BindingSourceInvokeDelegate(AddressOf BindingSourceEndEdit), targetContainer, bs)
        Else
            bs.EndEdit()
            'bs.CurrencyManager.Refresh()
        End If
    End Sub


    Private Delegate Function ControlFocusedDelegate(ByVal targetContainer As Control, ByVal target As Control) As Boolean
    Friend Shared Function ControlFocused(ByVal targetContainer As Control, ByVal target As Control) As Boolean
        If targetContainer.InvokeRequired Then
            Dim d As New ControlFocusedDelegate(AddressOf ControlFocused)
            Return CBool(targetContainer.Invoke(d, targetContainer, target))
        Else
            Return target.Focused
        End If
    End Function

    Private Delegate Sub FormActivateDelegate(ByVal target As Form)
    Friend Shared Sub FormActivate(ByVal target As Form)
        If target.InvokeRequired Then
            Dim d As New FormActivateDelegate(AddressOf FormActivate)
            target.Invoke(d, target)
        Else
            target.Activate()
        End If
    End Sub

End Class
