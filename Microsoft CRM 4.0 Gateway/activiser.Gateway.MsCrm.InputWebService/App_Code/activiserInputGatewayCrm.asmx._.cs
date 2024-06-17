using System;
using System.ComponentModel;
using System.Web.Services;
using System.IO;
using System.Configuration;

namespace activiser.WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://activiser.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public partial class activiserInputGatewayCrm : System.Web.Services.WebService
    {
        private static string WebConfigValue(string key, string defaultValue)
        {
            string result = null;
            try
            {
                result = ConfigurationSettings.AppSettings[key];
            }
            catch
            {
            }
            if (string.IsNullOrEmpty(result))
            {
                result = defaultValue;
            }
            return result;
        }

        private static string activiserConnectionString
        {
            get
            {
                return WebConfigValue(
                    Resources.GlobalResources.activiserConnectionStringKey, 
                    Resources.GlobalResources.DefaultConnectionString);
            }
        }

        private string GetInstanceMapPath()
        {
            string instanceMapPath = WebConfigValue(Resources.GlobalResources.InstanceMapPathKey, Resources.GlobalResources.InstanceMapDefaultPath);
            if(instanceMapPath.StartsWith("~/")) {
                instanceMapPath = this.Server.MapPath(instanceMapPath);
            }
            if (!(new DirectoryInfo(instanceMapPath).Exists)) throw new DirectoryNotFoundException(instanceMapPath);

            string instanceMapFileName =
            Path.Combine(
                instanceMapPath,
                WebConfigValue(Resources.GlobalResources.InstanceMapNameKey, Resources.GlobalResources.InstanceMapDefaultName)
            );
            if (!(new FileInfo(instanceMapFileName).Exists)) throw new FileNotFoundException(instanceMapFileName);
            return instanceMapFileName;
        }

        //private string InputGatewayUrl
        //{
        //    get
        //    {
        //        return WebConfigValue(
        //                Resources.GlobalResources.activiserInputGatewayKey,
        //                Resources.GlobalResources.activiserInputGatewayDefaultValue);
        //    }
        //}

        private activiserInputGateway.InputGatewayService activiserInputGateway(string organizationId)
        {
            Guid orgGuid;
            if (!organizationId.IsGuid(out orgGuid))
            {
                throw new ArgumentException(string.Format(Resources.GlobalResources.OrganizationIdNotValidMessageTemplate, organizationId));
            }
            return activiserInputGateway(orgGuid);
        }

        private activiserInputGateway.InputGatewayService activiserInputGateway(Guid organizationId)
        {
            string activiserUrl = string.Empty;
            int timeout = 60000;
            string username = null, password = null, domain = null;
            var instanceMap = new InstanceMap();
            try
            {
                string instanceMapPath =  GetInstanceMapPath();

                instanceMap.ReadXml(instanceMapPath);
                foreach (var m in instanceMap.Map)
                {
                    if (m.OrganizationId == organizationId)
                    {
                        activiserUrl = m.ActiviserUrl;
                        timeout = m.Timeout;
                        username = m.IsUserNameNull() ? null : m.UserName;
                        password = m.IsPasswordNull() ? null : m.Password;
                        domain = m.IsDomainNull() ? null : m.Domain;
                        break;
                    }
                    else if (m.OrganizationId == Guid.Empty) // default.
                    {
                        activiserUrl = m.ActiviserUrl;
                        timeout = m.Timeout;
                        username = m.IsUserNameNull() ? null : m.UserName;
                        password = m.IsPasswordNull() ? null : m.Password;
                        domain = m.IsDomainNull() ? null : m.Domain;
                    }
                }
            }
            catch
            {
                throw new ArgumentException(string.Format(Resources.GlobalResources.InstanceLookupFailureTemplate, organizationId));
            }

            if (string.IsNullOrEmpty(activiserUrl))
            {
                throw new ArgumentException(string.Format(Resources.GlobalResources.InstanceLookupFailureTemplate, organizationId));
            }
            var result = new activiserInputGateway.InputGatewayService();
            result.Url = activiserUrl;
            result.Timeout = timeout;
            if (!string.IsNullOrEmpty(username))
            {
                result.Credentials = new System.Net.NetworkCredential(username, password, domain);
            }
            else
            {
                result.UseDefaultCredentials = true;
            }
            return result;
        }

        [WebMethod]
        public string Test(string organizationId)
        {
            try
            {
                var inputGateway = activiserInputGateway(organizationId);
                return inputGateway.Test();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [WebMethod]
        public string StartTransaction(string organizationId, string userId)
        {
            var inputGateway = activiserInputGateway(organizationId);
            return inputGateway.StartTransaction(Resources.GlobalResources.activiserGatewayID, organizationId, userId);
        }

        [WebMethod]
        public string FinishTransaction(string organizationId, string transactionId)
        {
             return activiserInputGateway(organizationId).FinishTransaction(transactionId);
        }

    }
}
