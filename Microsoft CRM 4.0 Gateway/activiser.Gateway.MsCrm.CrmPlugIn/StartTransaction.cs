using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Crm.Sdk;
using Microsoft.Crm.SdkTypeProxy;
using System.Reflection;

namespace activiser.InputGateway.CrmPlugIn
{
    public class StartTransaction : IPlugin, IDisposable
    {
        private activiserInputGatewayCrm.activiserInputGatewayCrm crmIg;

        public StartTransaction(string config, string secureConfig)
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
                    string txId = crmIg.StartTransaction(context.OrganizationId.ToString(Properties.Resources.OrganizationIdFormat), context.UserId.ToString());

                    PropertyBagEntry txProp = new PropertyBagEntry(Properties.Resources.ActiviserTransactionIDKey, txId);
                    context.SharedVariables.Properties.Add(txProp);
                }
                catch (Exception ex)
                {
                    Logging.LogError(ErrorCode.StartTransactionException, Properties.Resources.StartTransactionExceptionMessage,
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
