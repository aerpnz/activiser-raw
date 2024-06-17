using System;
using System.Reflection;
using Microsoft.Crm.Sdk;

namespace activiser.InputGateway.CrmPlugIn
{
    public class AssignEntity : IPlugin, IDisposable
    {
        private activiserInputGatewayCrm.activiserInputGatewayCrm crmIg;

        public AssignEntity(string config, string secureConfig)
        {
            if (!string.IsNullOrEmpty(config))
            {
                crmIg = new activiserInputGatewayCrm.activiserInputGatewayCrm();
                crmIg.Url = config;
            }
        }

        #region IPlugin Members

        public void Execute(IPluginExecutionContext context)
        {
            if (crmIg != null)
            {
                try
                {
                    string entityIdentity = Serialization.Serialize((Moniker)context.InputParameters[ParameterName.Target]);
                    string newAssignee = Serialization.Serialize((SecurityPrincipal)context.InputParameters[ParameterName.Assignee]);

                    string preData = null, postData = null;

                    if (context.PreEntityImages != null && context.PreEntityImages.Contains(ParameterName.PreBusinessEntity))
                    {
                        preData = Serialization.Serialize((DynamicEntity)context.PreEntityImages[ParameterName.PreBusinessEntity]);
                    }

                    if (context.PostEntityImages != null && context.PostEntityImages.Contains(ParameterName.PostBusinessEntity))
                    {
                        postData = Serialization.Serialize((DynamicEntity)context.PostEntityImages[ParameterName.PostBusinessEntity]);
                    }

                    string txId = string.Empty;
                    if (context.SharedVariables.Contains(Properties.Resources.ActiviserTransactionIDKey))
                    {
                        PropertyBagEntry txProp = (PropertyBagEntry)context.SharedVariables[Properties.Resources.ActiviserTransactionIDKey];
                        txId = (string)txProp.Value;
                    }
                    crmIg.Assign(txId, context.OrganizationId.ToString(Properties.Resources.OrganizationIdFormat), context.UserId.ToString(), context.PrimaryEntityName, entityIdentity, newAssignee, preData, postData);
                }
                catch (Exception ex)
                {
                    Logging.LogError(ErrorCode.AssignEntityException, Properties.Resources.AssignEntityExceptionMessage,
                            context.PrimaryEntityName, ex.ToString());
                }
            }
            else
            {
                Logging.LogWarning(ErrorCode.NoGateway,
                    Properties.Resources.NoGatewayExceptionMessageFormat,
                    MethodBase.GetCurrentMethod().Module.Name, context.PrimaryEntityName);
            }
        }

        #endregion

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (crmIg != null)
                {
                    crmIg.Dispose();
                    crmIg = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
