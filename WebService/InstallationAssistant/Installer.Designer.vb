<System.ComponentModel.RunInstaller(True)> Partial Class Installer
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.EventLogInstaller1 = New System.Diagnostics.EventLogInstaller
        '
        'EventLogInstaller1
        '
        Me.EventLogInstaller1.CategoryCount = 0
        Me.EventLogInstaller1.CategoryResourceFile = Nothing
        Me.EventLogInstaller1.Log = "Application"
        Me.EventLogInstaller1.MessageResourceFile = Nothing
        Me.EventLogInstaller1.ParameterResourceFile = Nothing
        Me.EventLogInstaller1.Source = "Activiser Web Service"
        '
        'Installer
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.EventLogInstaller1})

    End Sub
    Friend WithEvents EventLogInstaller1 As System.Diagnostics.EventLogInstaller

End Class
