using System;
using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;
using System.Diagnostics;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private static System.Net.ICredentials GetCredentials()
        {
            Microsoft.Win32.RegistryKey regBase = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(Properties.Resources.RegistryBaseKey, false);
            if (regBase == null) return null;

            if (regBase.ValueCount == 0) return null;

            string userName = (string)regBase.GetValue(Properties.Resources.RegistryUsernameKey, null);
            if (string.IsNullOrEmpty(userName)) return null;
            System.Net.NetworkCredential result = new System.Net.NetworkCredential();
            result.UserName = userName;

            string password = (string)regBase.GetValue(Properties.Resources.RegistryPasswordKey, null);
            if (!string.IsNullOrEmpty(password)) result.Password = password;

            string domain = (string)regBase.GetValue(Properties.Resources.RegistryDomainKey, null);
            if (!string.IsNullOrEmpty(null)) result.Domain = domain;

            return result;
        }
    }
}