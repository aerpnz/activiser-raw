using System;
using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private DynamicEntity DynamicRetrieve(CrmService crm, CrmGatewayConfig.EntityMapRow emr, DataRow dr)
        {
            TargetRetrieveDynamic target = new TargetRetrieveDynamic();
            target.EntityName = emr.CrmEntityName;
            target.EntityId = (Guid)dr[emr.activiserEntityIdAttribute];

            ColumnSet columnSet = new ColumnSet();
            List<string> columnList = new List<string>();
            foreach (CrmGatewayConfig.AttributeMapRow amr in emr.GetAttributeMapRows())
            {
                if (amr.CrmAttributeName == STR_CrmCreatedOn || amr.CrmAttributeName == STR_CrmModifiedOn) continue;
                columnList.Add(amr.CrmAttributeName);
            }
            columnSet.Attributes = columnList.ToArray();

            // Create a retrieve request object.
            RetrieveRequest retrieve = new RetrieveRequest();
            retrieve.Target = target;
            retrieve.ColumnSet = columnSet;
            retrieve.ReturnDynamicEntities = true;

            // Create a response reference and execute the retrieve request.
            try
            {
                RetrieveResponse response = (RetrieveResponse)crm.Execute(retrieve);
                DynamicEntity retrievedEntity = (DynamicEntity)response.BusinessEntity;
                return retrievedEntity;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                LogWarningMessage(EventId.RetrieveFailure, Properties.Resources.DynamicRetrieveFailureMessage, ex);
                return null;
            }
            catch (Exception ex)
            {
                LogWarningMessage(EventId.RetrieveFailure, Properties.Resources.DynamicRetrieveFailureMessage, ex);
                return null;
            }
        }
    }
}