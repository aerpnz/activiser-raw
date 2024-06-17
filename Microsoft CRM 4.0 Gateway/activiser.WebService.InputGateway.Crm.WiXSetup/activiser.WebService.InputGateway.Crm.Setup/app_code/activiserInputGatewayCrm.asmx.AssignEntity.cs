using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Crm.Sdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService
{
    public partial class activiserInputGatewayCrm : System.Web.Services.WebService
    {
        [WebMethod]
        public string Assign(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData)
        {
            CrmGatewayConfig entityConfig = CrmGatewayConfig.GetGatewayCrmEntityConfig(activiserConnectionString, entity);
            if (entityConfig == null || entityConfig.EntityMap.Count != 1) return null;

            CrmGatewayConfig.EntityMapRow emr = entityConfig.EntityMap[0];
            CrmGatewayConfig.AttributeMapDataTable amdt = entityConfig.AttributeMap;

            XmlDocument aeDoc = Utilities.CreateActiviserDoc(entityPostData, GatewayTransactionType.Update, emr, amdt);

            string activiserInput = aeDoc.OuterXml;
            System.Diagnostics.Debug.WriteLine(activiserInput);

            return string.Empty;
        }
    }
}