using System;
using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private int DynamicInsert(CrmService crm, CrmGatewayConfig.EntityMapRow emr, DataRow dr)
        {
            Dictionary<string, Property> propertyDictionary = new Dictionary<string, Property>();
            foreach (CrmGatewayConfig.EntityDefaultValueRow dvr in emr.GetEntityDefaultValueRows())
            {
                propertyDictionary.Add(dvr.AttributeName, CreateProperty(dvr.AttributeName, dvr.AttributeType, dvr.DefaultValue, null, null));
            }
            foreach (CrmGatewayConfig.AttributeMapRow amr in emr.GetAttributeMapRows())
            {
                // add properties to entity
                if (amr.CreateFromActiviser == 0) continue;
                if (dr.IsNull(amr.activiserAttributeName)) continue;
                if (amr.CrmAttributeName == STR_CrmCreatedOn || amr.CrmAttributeName == STR_CrmModifiedOn) continue;
                if (amr.IsStatusAttribute)
                {
                    int newState = (int)dr[amr.activiserAttributeName];
                    foreach (CrmGatewayConfig.StatusMapRow smr in emr.GetStatusMapRows())
                    {
                        if (smr.activiserStatusCode == newState)
                        {
                            propertyDictionary.Add(STR_Statecode, CreateProperty(STR_Statecode, STR_String, smr.CrmStateCode, null, null));
                            propertyDictionary.Add(STR_Status, CreateProperty(STR_Statuscode, STR_Status, smr.CrmStatusCode, null, null));
                            break;
                        }
                    }
                    continue;
                }
                try
                {
                    // override default value.
                    if (propertyDictionary.ContainsKey(amr.CrmAttributeName)) propertyDictionary.Remove(amr.CrmAttributeName);
                    Property prop = CreateProperty(amr.CrmAttributeName, amr.DataType, dr[amr.activiserAttributeName], amr.IsLookupTargetNull() ? null : amr.LookupTarget, amr.IsMaximumLengthNull() ? (int?)null : (int?)amr.MaximumLength);
                    propertyDictionary.Add(amr.CrmAttributeName, prop);
                }
                catch (PropertyTypeNotSupportedException)
                {
                    LogWarningMessage(EventId.UnsupportedProperty, Properties.Resources.DynamicInsertUnsupportedPropertyErrorMessageFormat,
                            amr.CrmAttributeName, amr.activiserAttributeName, amr.DataType);
                }
            }

            DynamicEntity targetDe;
            targetDe = new DynamicEntity();
            targetDe.Name = emr.CrmEntityName;
            List<Property> propList = new List<Property>();
            propList.AddRange(propertyDictionary.Values);
            targetDe.Properties = propList.ToArray();

            TargetCreateDynamic targetCreate = new TargetCreateDynamic();
            targetCreate.Entity = targetDe;

            CreateRequest createRequest = new CreateRequest();
            createRequest.Target = targetCreate;
            try
            {
                CreateResponse createResponse = (CreateResponse)crm.Execute(createRequest);
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                string message = string.Format(Properties.Resources.DynamicInsertErrorMessageFormat,
                    emr.activiserEntityName, emr.CrmEntityName, ex.Message, ex.Detail.InnerXml, targetDe == null ? STR_Null : targetDe.ToString(), ex.StackTrace);
                LogErrorMessage(EventId.CreateFailure, message);
                return 0;
            }
            return 1;
        }
    }
}