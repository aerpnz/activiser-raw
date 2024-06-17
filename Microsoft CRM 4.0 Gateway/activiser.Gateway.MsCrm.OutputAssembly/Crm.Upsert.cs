using System;
using System.Data;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private int UpSertEntities(CrmGatewayConfig.EntityMapRow emr, DataSet transactionData, CrmService crm)
        {
            DataTable dt = transactionData.Tables[emr.activiserEntityName];
            bool _processingJobTable = String.Compare(emr.activiserEntityName, "JOB", true) == 0;
            foreach (DataRow dr in dt.Rows)
            {
                // HACK: prevent Status Change entries from propagating to CRM
                if (_processingJobTable)
                {
                    try
                    {
                        if ((int)dr["JobStatusID"] == 6)
                        {
                            continue;
                        }
                    }
                    catch { }
                }

                Guid entityId = (Guid)dr[emr.activiserEntityIdAttribute];
                if (entityId == Guid.Empty)
                {
                    throw new InvalidOperationException(Properties.Resources.EntityIDMissingOrEmpty);
                }

                // attempt to retrieve existing entity
                // if this succeeds, then we need to do an update, otherwise we're creating.
                DynamicEntity de = DynamicRetrieve(crm, emr, dr);

                if (de == null)
                {
                    LogInfoMessage(EventId.CreatingEntity, Properties.Resources.CreatingNewEntityFromActiviser, new object[] { emr.CrmEntityName, emr.activiserEntityName });
                    // insert
                    if (DynamicInsert(crm, emr, dr) != 0)
                    {
                        LogSuccess(EventId.CreateSuccessAudit, Properties.Resources.CreatedNewEntityFromActiviser, new object[] { emr.CrmEntityName, emr.activiserEntityName });
                    }
                    else
                    {
                        LogFailure(EventId.CreateFailureAudit, Properties.Resources.FailedToCreateNewEntityFromActiviser, new object[] { emr.CrmEntityName, emr.activiserEntityName });
                    }
                }
                else
                {
                    LogInfoMessage(EventId.UpdatingEntity, Properties.Resources.UpdatingEntityFromActiviser, new object[] { emr.CrmEntityName, emr.activiserEntityName });
                    // update
                    if (DynamicUpdate(crm, de, emr, dr) != 0)
                    {
                        LogSuccess(EventId.UpdateSuccessAudit, Properties.Resources.UpdatedEntityFromActiviser, new object[] { emr.CrmEntityName, emr.activiserEntityName });
                    }
                    else
                    {
                        LogFailure(EventId.UpdateFailureAudit, Properties.Resources.FailedToUpdateEntityFromActiviser, new object[] { emr.CrmEntityName, emr.activiserEntityName });
                    }
                }
            }
            return 0;
        }
    }
}