Module Main
    Private Const MODULENAME As String = "Main"

    Private Const deviceGuid As String = "c760e9fb-abb7-49c2-b2f7-975eef25d374"
    Friend ReadOnly deviceId As String = String.Format("{0};{1}", My.Application.Info.Version.ToString(4), deviceGuid)
    Friend ReadOnly OutlookClientGuid As New Guid(deviceGuid)

    '    Friend ReadOnly OsLevel As Integer = Environment.OSVersion.Version.Major * 100 + Environment.OSVersion.Version.Minor

    Friend explorerList As New Generic.List(Of ExplorerWrapper)

    Friend machineRegistryBase As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.CreateSubKey(My.Resources.OutlookAddInRegistryBase)
    Friend userRegistryBase As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.CreateSubKey(My.Resources.OutlookAddInRegistryBase)
End Module
