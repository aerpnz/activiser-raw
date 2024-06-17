Imports activiser.Library
Imports activiser.Library.activiserWebService


Module Main
    Friend ReadOnly OsLevel As Integer = Environment.OSVersion.Version.Major * 100 + Environment.OSVersion.Version.Minor
    Friend ConsoleUser As Utility.UserRow
    Friend WebService As New activiserWebService.activiser

    Friend machineRegistryBase As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey(My.Resources.ConsoleRegistryBase, False)
    Friend userRegistryBase As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.CreateSubKey(My.Resources.ConsoleRegistryBase)

    Public Const deviceIdbase As String = "35060af8-9b60-45a9-ac93-5dc6711af0cd"
    Public ReadOnly ApplicationGuid As New Guid(deviceIdbase)
    Public ReadOnly deviceId As String = String.Format("{0};{1}", My.Application.Info.Version.ToString(4), ApplicationGuid.ToString("N"))

    Friend ReadOnly WebServiceGuid As Guid = New Guid("0ABCE90F-330F-4520-963B-86C9F1851DCE")

    Sub Main()
        If LoginForm.ShowDialog = DialogResult.OK Then
            LanguageEditor.ShowDialog()
        End If
    End Sub
End Module
