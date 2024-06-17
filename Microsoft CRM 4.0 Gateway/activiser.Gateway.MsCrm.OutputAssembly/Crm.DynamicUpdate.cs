using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;
using System.Diagnostics;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private int DynamicUpdate(CrmService crm, DynamicEntity oldData, CrmGatewayConfig.EntityMapRow emr, DataRow newData)
        {
            List<Property> targetPropList = new List<Property>();

            bool doStatusChange = false;
            bool doOwnerChange = false;

            foreach (CrmGatewayConfig.AttributeMapRow amr in emr.GetAttributeMapRows())
            {
                if (amr.UpdateFromActiviser == 0) continue;
                if (amr.CrmAttributeName == STR_CrmCreatedOn || amr.CrmAttributeName == STR_CrmModifiedOn) continue;


                Property newProp = CreateProperty(amr.CrmAttributeName, amr.DataType, newData[amr.activiserAttributeName], amr.IsLookupTargetNull() ? null : amr.LookupTarget, amr.IsMaximumLengthNull() ? (int?)null : (int?)amr.MaximumLength);

                if (oldData.Contains(amr.CrmAttributeName))
                {
                    Property oldProp = oldData[amr.CrmAttributeName];
                    if (!(newProp.GetType() == oldProp.GetType()))
                    {
                        LogErrorMessage(0, Properties.Resources.DynamicUpdateDataTypeMismatchWarning,
                            amr.CrmAttributeName,
                            oldProp.GetType().ToString(),
                            newProp.GetType().ToString()
                            );
                        continue;
                    }
                    object oldValue = DynamicEntity.PropertyFactory.GetPropertyValue(oldProp);
                    object newValue = DynamicEntity.PropertyFactory.GetPropertyValue(newProp);
                    if (CompareDynamicProperty(oldValue, newValue))
                    {
                        if (!amr.IsIdAttribute)
                            continue; // no need to update.
                    }
                    // TODO:
                    if (amr.IsStatusAttribute)
                    {
                        doStatusChange = true;
                        continue; // will do status change in separate pass, after other updates.
                    }
                    else if (amr.IsOwnerAttribute)
                    {
                        doOwnerChange = true;
                        continue;
                    }
                }
                if (newProp is CrmDateTimeProperty)
                {
                    CrmDateTimeProperty dateProp = (CrmDateTimeProperty)newProp;
                    if (!dateProp.Value.IsNull) targetPropList.Add(dateProp);
                }
                else
                {
                    targetPropList.Add(newProp);
                }
            }

            if (targetPropList.Count != 1) // will always have 'key' field. If there are no others, then there's nothing to change.
            {
                DynamicEntity targetDe = new DynamicEntity();
                targetDe.Name = emr.CrmEntityName;
                targetDe.Properties = targetPropList.ToArray();

                Debug.WriteLine(targetDe.ToString());

                TargetUpdateDynamic target = new TargetUpdateDynamic();
                target.Entity = targetDe;

                UpdateRequest update = new UpdateRequest();
                update.Target = target;

                // Execute the request.
                try
                {
                    UpdateResponse updated = (UpdateResponse)crm.Execute(update);
                }
                catch (System.Web.Services.Protocols.SoapException ex)
                {
                    string message = string.Format(Properties.Resources.DynamicUpdateErrorMessageFormat,
                        emr.activiserEntityName, emr.CrmEntityName, ex.Message, ex.Detail.InnerXml, targetDe == null ? STR_Null : targetDe.ToString(), ex.StackTrace);
                    LogErrorMessage(EventId.UpdateFailure, message);
                    return 0;
                }
            }

            if (doStatusChange)
            {
                return DynamicSetState(crm, emr, newData);
            }
            else if (doOwnerChange)
            {
                return DynamicAssign(crm, emr, newData);
            }
            return 1;
        }
    }
}