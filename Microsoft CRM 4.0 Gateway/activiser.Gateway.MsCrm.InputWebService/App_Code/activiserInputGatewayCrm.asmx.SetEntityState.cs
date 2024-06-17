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
using System.Diagnostics;
using activiser.WebService;

namespace activiser.WebService
{
    public partial class activiserInputGatewayCrm : System.Web.Services.WebService
    {
        [WebMethod]
        public string SetState(string transactionId, string organizationId, string userId, string entity, string entityData, string newState, int newStatusCode, string entityPreData, string entityPostData)
        {
            Logging.LogMessage(ErrorCode.Information,
                Resources.GlobalResources.SetStateMessageTemplate,
                entity,
                organizationId, userId, transactionId,
                newState, newStatusCode, 
                activiserConnectionString);
            Guid? orgId = organizationId.ParseGuid();
            CrmGatewayConfig entityConfig = CrmGatewayConfig.GetGatewayCrmEntityConfig(activiserConnectionString, orgId, entity);
            if (entityConfig == null || entityConfig.EntityMap.Count != 1) return null;

            CrmGatewayConfig.EntityMapRow emr = entityConfig.EntityMap[0];
            CrmGatewayConfig.AttributeMapDataTable amdt = entityConfig.AttributeMap;

            XmlDocument aeDoc = Utilities.CreateActiviserDoc(entityData, entityPostData, GatewayTransactionType.Update, emr, amdt);

            string activiserInput = aeDoc.OuterXml;
            Debug.WriteLine(activiserInput);

            using (var aig = activiserInputGateway(organizationId))
            {
                string txId;
                if (!string.IsNullOrEmpty(transactionId))
                    txId = transactionId;
                else
                    txId = aig.StartTransaction(Resources.GlobalResources.activiserGatewayID, organizationId, userId);

                aig.Update(txId, activiserInput, true);

                if (string.IsNullOrEmpty(transactionId))
                    aig.FinishTransaction(txId);
            }
            return string.Empty;
        }
    }
}