using System;
using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private int DynamicSetState(CrmService crm, CrmGatewayConfig.EntityMapRow emr, DataRow dr)
        {
            int newState;
            string newCrmState = null;
            int newCrmStatus = -99999;

            Moniker entityId = null;

            bool gotState = false, gotId = false;

            List<Property> propList = new List<Property>();
            foreach (CrmGatewayConfig.AttributeMapRow amr in emr.GetAttributeMapRows())
            {
                if (dr.IsNull(amr.activiserAttributeName)) continue;
                if (amr.IsIdAttribute)
                {
                    entityId = new Moniker();
                    entityId.Name = emr.CrmEntityName;
                    entityId.Id = (Guid)dr[amr.activiserAttributeName];
                    gotId = true;
                    continue;
                }
                if (amr.IsStatusAttribute)
                {
                    newState = (int)dr[amr.activiserAttributeName];
                    foreach (CrmGatewayConfig.StatusMapRow smr in emr.GetStatusMapRows())
                    {
                        if (smr.activiserStatusCode == newState)
                        {
                            newCrmState = smr.CrmStateCode;
                            newCrmStatus = smr.CrmStatusCode;
                            gotState = true;
                            break;
                        }
                    }
                    continue;
                }
                if (gotState && gotId) break;
            }

            if (!(gotState && gotId)) throw new InvalidOperationException(Properties.Resources.DynamicSetStateMissingDataMessage);

            SetStateDynamicEntityRequest stateRequest = new SetStateDynamicEntityRequest();
            stateRequest.Entity = entityId;
            stateRequest.Status = newCrmStatus;
            stateRequest.State = newCrmState;

            try
            {
                // Execute the request.
                SetStateDynamicEntityResponse response = (SetStateDynamicEntityResponse)crm.Execute(stateRequest);
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                string message = string.Format(Properties.Resources.DynamicSetStateErrorMessageFormat,
                    emr.activiserEntityName, emr.CrmEntityName, ex.Message, ex.Detail.InnerXml,
                    (stateRequest == null) ? STR_Null :
                        string.Format("{0}/{1}/{2}/{3}",
                            stateRequest.Entity.Name, stateRequest.Entity.Id, stateRequest.State, stateRequest.Status),
                    ex.StackTrace);
                LogErrorMessage(EventId.SetStateFailure, message);
                return 0;
            }

            return 1;
        }
    }
}