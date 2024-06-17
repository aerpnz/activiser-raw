using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Crm.Sdk;
using Microsoft.Crm.SdkTypeProxy;
using System.Reflection;

namespace activiser.InputGateway.CrmPlugIn
{
    public class FinishTransaction : IPlugin, IDisposable
    {
        private activiserInputGatewayCrm.activiserInputGatewayCrm crmIg;

        public FinishTransaction(string config, string secureConfig)
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
            // if no 'ActiviserTransactionID' property, then there was no corresponding 'StartTransaction', just ignore it.
            if (context.SharedVariables.Contains(Properties.Resources.ActiviserTransactionIDKey))
            {
                if (crmIg != null)
                {
                    try
                    {
                        PropertyBagEntry txProp = (PropertyBagEntry) context.SharedVariables[Properties.Resources.ActiviserTransactionIDKey];
                        crmIg.FinishTransaction(context.OrganizationId.ToString(Properties.Resources.OrganizationIdFormat), (string)txProp.Value);
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ErrorCode.FinishTransactionException, Properties.Resources.FinishTransactionExceptionMessage,
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
