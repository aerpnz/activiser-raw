using System;
using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;
using System.Diagnostics;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        public int Save(Guid transactionId, DataSet transactionData, out Exception saveException)
        {
            try
            {
                saveException = null;
                CrmService crm = GetCrmService(CrmServiceLocation, OrganizationName);

                // loop through each table that needs to be inserted and/or updated into CRM
                var eml = from CrmGatewayConfig.EntityMapRow emr in _em
                          where (emr.InsertActiviserToCrm | emr.UpdateActiviserToCrm) != 0
                          orderby emr.ProcessOrder
                          select emr;

                foreach (CrmGatewayConfig.EntityMapRow emr in eml)
                    //_em.Select(string.Format("({0} <> 0) OR ({1} <> 0)",
                    //    _em.InsertActiviserToCrmColumn.ColumnName,
                    //    _em.UpdateActiviserToCrmColumn.ColumnName)
                    //    , _em.ProcessOrderColumn.ColumnName))
                {
                    // Update or Insert all activiser entities for this map in the transaction data set
                    if (transactionData.Tables.Contains(emr.activiserEntityName))
                    {
                        UpSertEntities(emr, transactionData, crm);
                    }
                }
            }
            catch (GetCrmServiceException ex)
            {
                LogErrorMessage(0, Properties.Resources.ErrorGettingCrmServiceInstance, ex, OrganizationName);
                saveException = ex;
                return -1;
            }
            catch (Exception ex)
            {
                LogErrorMessage(EventId.GeneralSaveError, Properties.Resources.ErrorSavingDataToCRM, ex);
                saveException = ex;
                return -1;
            }

            return 0;
        }
    }
}