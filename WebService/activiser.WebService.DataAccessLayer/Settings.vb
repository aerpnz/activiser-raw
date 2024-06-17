
Namespace My

    'This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.
    Partial Friend NotInheritable Class MySettings
        <Global.System.Configuration.ApplicationScopedSettingAttribute(), _
        Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString), _
        Global.System.Configuration.DefaultSettingValueAttribute("Data Source=.;Initial Catalog=activiser;Integrated Security=True;Application Name=""activiser Web Service""")> _
        Public ReadOnly Property activiserConnectionString() As String
            Get
                Return System.Web.Configuration.WebConfigurationManager.ConnectionStrings("activiserConnectionString").ConnectionString
                ' Return CType(Me("activiserConnectionString"), String)
            End Get
        End Property
    End Class
End Namespace
