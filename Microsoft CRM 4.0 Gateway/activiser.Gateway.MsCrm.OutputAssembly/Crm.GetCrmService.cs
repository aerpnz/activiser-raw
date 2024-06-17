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
        // copied from MS CRM SDK: crmserviceutility.cs
        private static CrmService GetCrmService(string crmServerUrl, string organizationName)
        {
            if (String.IsNullOrEmpty(crmServerUrl)) throw new ArgumentNullException("crmServerUrl");

            try
            {
                // Setup the Authentication Token
                CrmAuthenticationToken token = new CrmAuthenticationToken();
                token.OrganizationName = organizationName;

                CrmService service = new CrmService();

                UriBuilder builder = new UriBuilder(crmServerUrl);
                builder.Path = Properties.Resources.CrmServiceBuilderPath;
                service.Url = builder.Uri.ToString();

                System.Net.ICredentials creds = GetCredentials();
                if (creds != null)
                {
                    service.Credentials = creds;
                    service.PreAuthenticate = true;
                }
                else
                {
                    service.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }
                service.CrmAuthenticationTokenValue = token;

                return service;
            }
            catch (Exception ex)
            {
                throw new GetCrmServiceException(ex.Message, ex);
            }
        }    
    }
}