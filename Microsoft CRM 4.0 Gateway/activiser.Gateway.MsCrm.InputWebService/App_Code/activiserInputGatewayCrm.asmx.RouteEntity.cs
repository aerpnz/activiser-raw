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
        public string Route(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData)
        {
            return string.Empty;
        }
    }
}