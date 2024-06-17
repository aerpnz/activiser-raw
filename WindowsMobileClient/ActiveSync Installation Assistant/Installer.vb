Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.IO
Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Public Class Installer
    Private Const CEAppMgrPathRegKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\CEAppMgr.exe"
    Private Const ActiveSyncRegPath As String = "SOFTWARE\Microsoft\Windows CE Services"
    Private Const ActiveSyncFolderKey As String = "InstalledDir"
    Private Const SetupIniFileName As String = "Setup.ini"
    Private Const ApplicationFolder As String = "\activiser™ Windows Mobile Client"

    Dim appManager As String = GetAppManager()
    Dim installPath As String = GetAppInstallDirectory()

    Private Function GetAppManager() As String
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey(CEAppMgrPathRegKey)
        Dim appManager As String = CStr(key.GetValue(""))
        If String.IsNullOrEmpty(appManager) Then
            Throw New Exception("CEAppMgr is not correctly installed (setup can't locate it in the registry).")
        End If
        key.Close()
        Return appManager
    End Function

    Private Function GetAppInstallDirectory() As String
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey(ActiveSyncRegPath)
        If key Is Nothing Then
            Throw New Exception("ActiveSync is not installed.")
        End If
        Dim activeSyncPath As String = CType(key.GetValue(ActiveSyncFolderKey), String)
        If String.IsNullOrEmpty(activeSyncPath) Then
            Throw New Exception("ActiveSync is not correctly installed (setup can't locate ActiveSync folder in the registry).")
        End If
        Dim installPath As String = Path.Combine(activeSyncPath, ApplicationFolder)
        key.Close()
        Return installPath
    End Function

    Private Sub RunAppManager(ByVal arg As String)
        ' get path to the app manager

        If Not (appManager Is Nothing) Then
            If arg Is Nothing Then
                Process.Start(String.Format("""{0}""", appManager))
            Else
                ' launch the app
                Process.Start( _
                   String.Format("""{0}""", appManager), _
                   String.Format("""{0}""", arg))
            End If
        Else
            ' could not locate app manager
            MessageBox.Show("Could not find app manager")
        End If
    End Sub


    Public Sub New()
        MyBase.New()
        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent
    End Sub

    Private Sub Installer_AfterInstall(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles Me.AfterInstall
        RunAppManager(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), SetupIniFileName))
    End Sub

    Private Sub Installer_AfterUninstall(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles Me.AfterUninstall
        RunAppManager(Nothing)
    End Sub
End Class
