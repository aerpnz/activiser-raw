using System;
using System.Reflection;
using Microsoft.Crm.Sdk;

namespace activiser.InputGateway.CrmPlugIn
{
    public class SetEntityState : IPlugin, IDisposable
    {
        private activiserInputGatewayCrm.activiserInputGatewayCrm crmIg;

        public SetEntityState(string config, string secureConfig)
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
                    string entityData = Serialization.Serialize(context.InputParameters.Properties);

                    string preData = null, postData = null;

                    if (context.PreEntityImages != null && context.PreEntityImages.Contains(ParameterName.PreBusinessEntity))
                    {
                        preData = Serialization.Serialize((DynamicEntity)context.PreEntityImages[ParameterName.PreBusinessEntity]);
                    }

                    if (context.PostEntityImages != null && context.PostEntityImages.Contains(ParameterName.PostBusinessEntity))
                    {
                        postData = Serialization.Serialize((DynamicEntity)context.PostEntityImages[ParameterName.PostBusinessEntity]);
                    }

                    string newState = string.Empty;
                    int newStatus = 0;

                    if (context.InputParameters.Contains(ParameterName.State))
                    {
                        newState = (string)context.InputParameters[ParameterName.State];
                    }

                    if (context.InputParameters.Contains(ParameterName.Status))
                    {
                        newStatus = (int)context.InputParameters[ParameterName.Status];
                    }

                    string txId = string.Empty;
                    if (context.SharedVariables.Contains(Properties.Resources.ActiviserTransactionIDKey))
                    {
                        PropertyBagEntry txProp = (PropertyBagEntry)context.SharedVariables[Properties.Resources.ActiviserTransactionIDKey];
                        txId = (string)txProp.Value;
                    } 
                    
                    crmIg.SetState(txId, context.OrganizationId.ToString(Properties.Resources.OrganizationIdFormat), context.UserId.ToString(), context.PrimaryEntityName, entityData, newState, newStatus, preData, postData);
                }
                catch (Exception ex)
                {
                    Logging.LogError(ErrorCode.SetEntityStateException, Properties.Resources.SetEntityStateExceptionMessage,
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
