Imports System.ComponentModel
Imports System.Configuration
Imports System.Configuration.Install
Imports System.Security.Policy
Imports System.Security
<System.Runtime.InteropServices.ComVisible(False)> _
Public Class Installer
    Const STR_SERVERURL As String = "SERVERURL"
    Private ReadOnly installPolicyLevel As String = "Machine"
    Private ReadOnly namedPermissionSet As String = "FullTrust"
    Private ReadOnly codeGroupDescription As String = "VSTO Permissions for "
    Private ReadOnly productName As String = "activiser™ Add In for Outlook 2003"
    Private ReadOnly debugBreakOnInstall As Boolean = False
    Private _codeGroupName As String = ""

    Public Sub New()
        MyBase.New()
        'This call is required by the Component Designer.
        InitializeComponent()
        'Add initialization code after the call to InitializeComponent
    End Sub

    ''' <summary>
    ''' Gets a CodeGroup name based on the productname and URL evidence
    ''' </summary>
    Private ReadOnly Property CodeGroupName() As String
        Get
            If (debugBreakOnInstall) Then System.Diagnostics.Debugger.Break()
            'If (CodeGroupName.Length = 0) Then CodeGroupName = "[" + Me.Context.Parameters("productName") + "] " + InstallDirectory
            _codeGroupName = "[" + productName + "] " + InstallDirectory
            Return _codeGroupName
        End Get
    End Property

    ''' <summary>
    ''' Gets the installdirectory with a wildcard suffix for use with URL evidence
    ''' </summary>
    Private ReadOnly Property InstallDirectory() As String
        Get
            ' Get the install directory of the current installer
            'If (debugBreakOnInstall) Then System.Diagnostics.Debugger.Break()
            'If assemblyPath Is Nothing Then assemblyPath = ""
            Dim assemblyPath As String = Me.Context.Parameters("assemblypath")
            Dim _installDirectory As String = assemblyPath.Substring(0, assemblyPath.LastIndexOf("\"))
            If (Not _installDirectory.EndsWith("\")) Then
                _installDirectory &= "\"
                _installDirectory &= "*"
            End If
            Return _installDirectory
        End Get
    End Property

    Public Overrides Sub Install(ByVal stateSaver As System.Collections.IDictionary)
        MyBase.Install(stateSaver)
        Try
            ConfigureCodeAccessSecurity()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString())
            Me.Rollback(stateSaver)
        End Try
    End Sub

    ''' <summary>
    ''' Configures FullTrust for the entire installdirectory
    ''' </summary>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2103:ReviewImperativeSecurity")> Private Sub ConfigureCodeAccessSecurity()
        Dim machinePolicyLevel As PolicyLevel = GetPolicyLevel()
        If (GetCodeGroup(machinePolicyLevel) Is Nothing) Then
            ' Create a new FullTrust permission set
            Dim permissionSet As PermissionSet = New NamedPermissionSet(Me.namedPermissionSet)
            Dim membershipCondition As IMembershipCondition = New UrlMembershipCondition(InstallDirectory)
            ' Create the code group
            Dim policyStatement As PolicyStatement = New PolicyStatement(permissionSet)
            Dim codeGroup As CodeGroup = New UnionCodeGroup(membershipCondition, policyStatement)
            codeGroup.Description = Me.codeGroupDescription
            codeGroup.Name = Me.CodeGroupName
            ' Add the code group
            machinePolicyLevel.RootCodeGroup.AddChild(codeGroup)
            ' Save changes
            SecurityManager.SavePolicy()
        End If
    End Sub

    ''' <summary>
    ''' Gets the currently defined policylevel
    ''' </summary>
    ''' <returns></returns>
    Private Function GetPolicyLevel() As System.Security.Policy.PolicyLevel
        ' Find the machine policy level
        Dim machinePolicyLevel As PolicyLevel = Nothing
        Dim policyHierarchy As System.Collections.IEnumerator = SecurityManager.PolicyHierarchy()
        While policyHierarchy.MoveNext()
            Dim level As PolicyLevel = CType(policyHierarchy.Current, PolicyLevel)
            If (level.Label.CompareTo(installPolicyLevel) = 0) Then
                machinePolicyLevel = level
                Exit While
            End If
        End While
        If (machinePolicyLevel Is Nothing) Then
            Throw New ArgumentException(My.Resources.InstallerErrorCouldNotFindMachinePolicyLevel, "machinePolicyLevel")
        End If
        Return machinePolicyLevel
    End Function

    ''' <summary>
    ''' Gets current codegroup based on CodeGroupName at the given policylevel
    ''' </summary>
    ''' <param name="policyLevel"></param>
    ''' <returns>null if not found</returns>
    Private Function GetCodeGroup(ByVal policyLevel As System.Security.Policy.PolicyLevel) As System.Security.Policy.CodeGroup
        Dim codeGroup As System.Security.Policy.CodeGroup
        For Each codeGroup In policyLevel.RootCodeGroup.Children
            If (codeGroup.Name.CompareTo(CodeGroupName) = 0) Then
                Return codeGroup
            End If
        Next
        Return Nothing
    End Function

    Public Overrides Sub Uninstall(ByVal savedState As System.Collections.IDictionary)
        If (debugBreakOnInstall) Then System.Diagnostics.Debugger.Break()
        MyBase.Uninstall(savedState)
        Try
            Me.UninstallCodeAccessSecurity()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(My.Resources.InstallerErrorUnableToUninstallCodeAccessSecurity + ex.ToString())
        End Try
    End Sub

    Private Sub UninstallCodeAccessSecurity()
        Dim machinePolicyLevel As PolicyLevel = GetPolicyLevel()
        Dim codeGroup As CodeGroup = GetCodeGroup(machinePolicyLevel)
        If Not (codeGroup Is Nothing) Then
            machinePolicyLevel.RootCodeGroup.RemoveChild(codeGroup)
            ' Save changes
            SecurityManager.SavePolicy()
        End If
    End Sub

    Private Sub Installer_AfterInstall(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles Me.AfterInstall
        If Me.Context.Parameters.ContainsKey(STR_SERVERURL) Then
            Try
                Dim appConfigFileName As String = New Uri(String.Format("{0}.config", Reflection.Assembly.GetExecutingAssembly.CodeBase)).LocalPath()
                'MsgBox(appConfigFileName)
                Dim appConfig As New Xml.XmlDocument
                appConfig.Load(appConfigFileName)

                Dim serverUrlNode As Xml.XmlNode
                Dim valueNode As Xml.XmlNode
                serverUrlNode = appConfig.SelectSingleNode(My.Resources.InstallerServerUrlXPathQuery)
                If serverUrlNode IsNot Nothing Then
                    valueNode = serverUrlNode.SelectSingleNode("value")
                    valueNode.InnerText = Me.Context.Parameters(STR_SERVERURL)
                End If
                appConfig.Save(appConfigFileName)

                My.Computer.Registry.LocalMachine.CreateSubKey(My.Resources.OutlookAddInRegistryBase).SetValue(My.Resources.RegistrySetupDateValueName, DateTime.UtcNow.ToString("u"))

            Catch ex As Exception
                MessageBox.Show(String.Format("Error saving server URL: {0}{1}{2}", ex.Message, vbNewLine, ex.ToString), My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Throw
            End Try
        End If

    End Sub
End Class
