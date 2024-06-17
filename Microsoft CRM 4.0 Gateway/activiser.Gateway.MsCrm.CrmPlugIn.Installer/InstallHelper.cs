using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using activiser.InputGateway.CrmPlugIn.Properties;
using System.Diagnostics;


namespace activiser.InputGateway.CrmPlugIn
{
    [RunInstaller(true)]
    public partial class InstallHelper : Installer
    {
        public InstallHelper()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            EventLog eventLog = new EventLog(Resources.EventLogName, ".", Resources.EventLogSource);
            eventLog.WriteEntry(Properties.Resources.CrmPlugInInstalledMessage);
            eventLog.Close();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }
    }
}
