<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.EventLogInstaller = New System.Diagnostics.EventLogInstaller
        '
        'EventLogInstaller
        '
        Me.EventLogInstaller.CategoryCount = 0
        Me.EventLogInstaller.CategoryResourceFile = Nothing
        Me.EventLogInstaller.Log = "activiser"
        Me.EventLogInstaller.MessageResourceFile = Nothing
        Me.EventLogInstaller.ParameterResourceFile = Nothing
        Me.EventLogInstaller.Source = "activiser Web Service"
        Me.EventLogInstaller.UninstallAction = System.Configuration.Install.UninstallAction.NoAction
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.EventLogInstaller})

    End Sub
    Friend WithEvents EventLogInstaller As System.Diagnostics.EventLogInstaller

End Class
