using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Crm.Sdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService
{
    public partial class activiserInputGatewayCrm : System.Web.Services.WebService
    {
        [WebMethod]
        public string Delete(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData)
        {
            Logging.LogMessage(ErrorCode.Information,
                Resources.GlobalResources.DeleteMessageTemplate,
                entity,
                organizationId, userId, transactionId,
                entityPreData);
            return string.Empty;
        }
    }
}