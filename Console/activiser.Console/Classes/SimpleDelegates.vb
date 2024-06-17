Module SimpleDelegates
    Delegate Sub SimpleSubDelegate()
    Delegate Function GetValueDelegate(Of T)() As T

    Delegate Sub SetTextDelegate(ByVal control As Control, ByVal message As String)
    Delegate Function GetTextDelegate(ByVal control As Control) As String

    Delegate Sub SetControlEnabledDelegate(Of T)(ByVal c As T, ByVal enabled As Boolean)
    Delegate Function GetControlEnabledDelegate(Of T)(ByVal c As T) As Boolean

    Delegate Function GetWindowStateDelegate() As FormWindowState
End Module
