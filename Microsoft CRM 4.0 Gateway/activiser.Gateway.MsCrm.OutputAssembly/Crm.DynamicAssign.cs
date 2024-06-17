using System;
using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private int DynamicAssign(CrmService crm, CrmGatewayConfig.EntityMapRow emr, DataRow dr)
        {
            SecurityPrincipal newOwner = null;
            TargetOwnedDynamic target = null;

            bool gotOwner = false, gotId = false;

            List<Property> propList = new List<Property>();
            foreach (CrmGatewayConfig.AttributeMapRow amr in emr.GetAttributeMapRows())
            {
                if (dr.IsNull(amr.activiserAttributeName)) continue;
                if (amr.IsIdAttribute)
                {
                    target = new TargetOwnedDynamic();
                    target.EntityName = emr.CrmEntityName;
                    target.EntityId = (Guid)dr[amr.activiserAttributeName];
                    gotId = true;
                    continue;
                }
                if (amr.IsOwnerAttribute)
                {
                    newOwner = new SecurityPrincipal();
                    newOwner.Type = SecurityPrincipalType.User;
                    newOwner.PrincipalId = (Guid)dr[amr.activiserAttributeName];
                    gotOwner = true;
                    continue;
                }
                if (gotOwner && gotId) break;
            }

            if (!(gotOwner && gotId)) throw new InvalidOperationException(Properties.Resources.DynamicAssignMissingDataMessage);

            AssignRequest assignRequest = new AssignRequest();
            assignRequest.Assignee = newOwner;
            assignRequest.Target = target;

            // Execute the request.
            try
            {
                AssignResponse response = (AssignResponse)crm.Execute(assignRequest);
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                string message = string.Format(Properties.Resources.DynamicAssignErrorMessageFormat,
                    emr.activiserEntityName, emr.CrmEntityName, ex.Message, ex.Detail.InnerXml,
                    (target == null) ? STR_Null : string.Format("{0}/{1}", target.EntityName, target.EntityId),
                    (assignRequest == null) ? STR_Null : string.Format("{0}/{1}", assignRequest.Assignee.Type, assignRequest.Assignee.PrincipalId),
                    ex.StackTrace);
                LogErrorMessage(EventId.AssignFailure, message);
                return 0;
            }

            return 1;
        }
    }
}