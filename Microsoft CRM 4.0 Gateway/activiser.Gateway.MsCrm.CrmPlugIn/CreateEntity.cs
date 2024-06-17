using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Crm.Sdk;
using crmsdk = Microsoft.Crm.Sdk;

namespace activiser.InputGateway.CrmPlugIn
{
    public class CreateEntity : IPlugin, IDisposable
    {
        private activiserInputGatewayCrm.activiserInputGatewayCrm crmIg;

        public CreateEntity(string config, string secureConfig)
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
                    Guid objectId;
                    DynamicEntity target = (DynamicEntity)context.InputParameters[ParameterName.Target];
                    // add a 'fully named' version of the entity ID to the property bag.
                    if (context.OutputParameters.Properties.Contains("id"))
                    {
                        objectId = (Guid)context.OutputParameters.Properties["id"];
                        string fullIdName = String.Format(CultureInfo.InvariantCulture, "{0}id", context.PrimaryEntityName);
                        if (!target.Properties.Contains(fullIdName))
                        {
                            target.Properties.Add(new crmsdk.KeyProperty(fullIdName, new crmsdk.Key(objectId)));
                        }
                    }
                    else
                    {
                        Logging.LogWarning(ErrorCode.CreateEntityException, Properties.Resources.CreateEntityIdMissingExceptionMessage);
                    }
                    string entityData = Serialization.Serialize(target);
                    string postData = null;

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
                    crmIg.Create(txId, context.OrganizationId.ToString(Properties.Resources.OrganizationIdFormat), context.UserId.ToString(), context.PrimaryEntityName, entityData, postData, true);
                }
                catch (Exception ex)
                {
                    Logging.LogError(ErrorCode.CreateEntityException, Properties.Resources.CreateEntityExceptionMessage,
                            context.PrimaryEntityName, ex.ToString());
                }
            }
            else
            {
                {
                    Logging.LogWarning(ErrorCode.NoGateway,
                        Properties.Resources.NoGatewayExceptionMessageFormat,
                        MethodBase.GetCurrentMethod().Module.Name, context.PrimaryEntityName);
                }
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
