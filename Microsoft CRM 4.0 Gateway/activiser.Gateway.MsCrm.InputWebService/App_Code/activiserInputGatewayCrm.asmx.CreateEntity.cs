using System;
using System.Web.Services;
using System.Xml;
using System.Xml.XPath;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService
{
    public partial class activiserInputGatewayCrm : System.Web.Services.WebService
    {
        [WebMethod]
        public string Create(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPostData, bool updateIfExists)
        {
            Logging.LogMessage(ErrorCode.Information, 
                Resources.GlobalResources.CreateMessageTemplate,
                entity,
                organizationId, userId, transactionId,
                entityPostData,
                activiserConnectionString);
            Guid? orgId = organizationId.ParseGuid();

            CrmGatewayConfig entityConfig = CrmGatewayConfig.GetGatewayCrmEntityConfig(activiserConnectionString, orgId, entity);
            if (entityConfig == null || entityConfig.EntityMap.Count != 1) return null;

            CrmGatewayConfig.EntityMapRow emr = entityConfig.EntityMap[0];
            CrmGatewayConfig.AttributeMapDataTable amdt = entityConfig.AttributeMap;

            XmlDocument aeDoc = Utilities.CreateActiviserDoc(entityPostData, GatewayTransactionType.Create, emr, amdt);
            
            string activiserInput = aeDoc.OuterXml;
            System.Diagnostics.Debug.WriteLine(activiserInput);

            using (var aig = activiserInputGateway(organizationId))
            {
                string txId;
                if (!string.IsNullOrEmpty(transactionId))
                    txId = transactionId;
                else
                    txId = aig.StartTransaction(Resources.GlobalResources.activiserGatewayID, organizationId, userId);
                
                aig.Create(txId, activiserInput, true);

                if (string.IsNullOrEmpty(transactionId))
                    aig.FinishTransaction(txId);
            }
            return string.Empty;
        }
    }
}