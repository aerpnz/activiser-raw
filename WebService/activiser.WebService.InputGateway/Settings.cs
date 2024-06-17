namespace activiser.WebService.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }

        [System.Configuration.ApplicationScopedSettingAttribute()]
        [System.Configuration.SpecialSettingAttribute(System.Configuration.SpecialSetting.ConnectionString)]
        [System.Configuration.DefaultSettingValueAttribute("Data Source=.;Initial Catalog=activiser;Integrated Security=True;Application Name=\"activiser Web Service\"")]
        public string activiserConnectionString
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["activiserConnectionString"].ConnectionString;
            }
        }
    }
}
