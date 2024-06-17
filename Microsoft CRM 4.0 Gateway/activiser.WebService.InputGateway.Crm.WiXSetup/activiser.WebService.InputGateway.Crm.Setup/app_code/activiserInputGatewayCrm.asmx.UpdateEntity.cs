using System.Web.Services;
using System.Xml;
using System.Xml.XPath;
using activiser.WebService.Gateway.Crm.DataAccessLayer;
using System.Diagnostics;

namespace activiser.WebService
{
    public partial class activiserInputGatewayCrm : System.Web.Services.WebService
    {
        [WebMethod]
        public string Update(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData, string entityPostData, bool createIfMissing)
        {
            Logging.LogMessage(ErrorCode.Information, 
                Resources.GlobalResources.UpdateMessageTemplate, 
                entity, 
                organizationId, userId, transactionId,
                entityPreData, entityPostData);
            CrmGatewayConfig entityConfig = CrmGatewayConfig.GetGatewayCrmEntityConfig(activiserConnectionString, entity);
            if (entityConfig == null || entityConfig.EntityMap.Count != 1) return null;

            CrmGatewayConfig.EntityMapRow emr = entityConfig.EntityMap[0];
            CrmGatewayConfig.AttributeMapDataTable amdt = entityConfig.AttributeMap;

            XmlDocument aeDoc = Utilities.CreateActiviserDoc(entityPostData, GatewayTransactionType.Update, emr, amdt);

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