using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using activiser.WebService.Gateway.Crm.DataAccessLayer;
using Microsoft.Crm.Sdk;
using Microsoft.Crm.SdkTypeProxy;

/// <summary>
/// Utilities used by various parts of the gateway
/// </summary>
namespace activiser.WebService
{

    public enum GatewayTransactionType
    {
        Create,
        Update
    }

    public sealed partial class Utilities
    {
        private const string STR_EntityMoniker = "EntityMoniker";
        private const string STR_IncidentResolution = "IncidentResolution";
        private const string STR_Incidentid = "incidentid";
        private const string STR_Property = "Property";
        private const string STR_Name = "Name";
        private const string STR_Value = "Value";
        private const string STR_Statuscode = "statuscode";
        private const string STR_Status = "Status";
        private const string STR_Datatype = "datatype";
        private const string STR_Createdon = "createdon";
        private const string STR_CompletedDate = "CompletedDate";
        private const string STR_Datetime = "datetime";
        private const string STR_Request = "REQUEST";

        /// <summary>
        /// Generates an XML document for an activiser entity for the supplied CRM entity.
        /// 
        /// This is done using direct parsing of the XML and massaging into an activiser format, rather 
        /// than by first deserializing the CRM entities, since the data needs to be in XML anyway, for
        /// passing to the generic activiser gateway.
        /// 
        /// This could probably also be done using and XML transform; but the author doesn't know XML that well...
        /// 
        /// </summary>
        /// 
        /// <param name="entityXml">One of InputParameters, PreEntityImages or PostEntityImages from the CRM plugin. Which
        /// one is used is determined by the caller, but typically InputParametes for Updates and PostEntityImage for Create</param>
        /// <param name="entityMap"></param>
        /// <param name="attributeMaps"></param>
        /// <returns></returns>
        public static XmlDocument CreateActiviserDoc(string entityXml, GatewayTransactionType txType, CrmGatewayConfig.EntityMapRow entityMap, CrmGatewayConfig.AttributeMapDataTable attributeMaps)
        {
            XmlDocument entityDoc = new XmlDocument();
            entityDoc.LoadXml(entityXml);

            XmlDocument aeDoc = new XmlDocument();
            XmlNode ae = aeDoc.CreateElement(entityMap.activiserEntityName); // create root element
            aeDoc.AppendChild(ae);

            XmlNodeList propertyList = entityDoc.GetElementsByTagName(STR_Property);

            /// process:
            ///     create dictionary<string,string> of <CrmAttributeName>,<Value>
            ///     process dictionary in activiserAttribute, Sequence order
            ///     create new attribute list and new XML document.
            ///     

            Dictionary<string, string> crmAttributeList = new Dictionary<string, string>();

            foreach (XmlElement element in propertyList)
            {
                string name = element.GetAttribute(STR_Name);

                CrmGatewayConfig.AttributeMapRow[] amList = attributeMaps.FindByCrmAttributeName(name);
                if (amList != null && amList.Length != 0)
                {
                    XmlNodeList valueNodes = element.GetElementsByTagName(STR_Value);

                    if (valueNodes != null && valueNodes.Count == 1) // can't handle multiple value nodes, which shouldn't exist anyway !
                    {
                        string value = valueNodes[0].InnerText;
                        crmAttributeList.Add(name, value);
                    }
                }
                //else
                //{
                //    // ignoring unmapped or 'toomany mapped' fields.
                //}
            }


            CrmGatewayConfig.AttributeMapRow[] entityAmList = attributeMaps.FindByEntityMapId(entityMap.EntityMapId); //note, this is an ordered list
            Dictionary<string, XmlElement> attributeList = new Dictionary<string, XmlElement>();

            foreach (var amr in entityAmList)
            {
                if (txType == GatewayTransactionType.Update && amr.UpdateFromCrm == 0) continue;
                if (txType == GatewayTransactionType.Create && amr.CreateFromCrm == 0) continue;

                if (crmAttributeList.ContainsKey(amr.CrmAttributeName))
                {
                    string value = crmAttributeList[amr.CrmAttributeName];
                    XmlElement aa;
                    if (attributeList.ContainsKey(amr.activiserAttributeName))
                    {
                        aa = attributeList[amr.activiserAttributeName];
                    }
                    else
                    {
                        aa = aeDoc.CreateElement(amr.activiserAttributeName);

                        // add datatype attribute, not particularly useful, except for debugging.
                        XmlAttribute aadt = aeDoc.CreateAttribute(STR_Datatype);
                        aadt.Value = amr.DataType;
                        aa.Attributes.Append(aadt);

                        attributeList.Add(amr.activiserAttributeName, aa);
                    }

                    if (amr.IsStatusAttribute)
                    {
                        CrmGatewayConfig.StatusMapRow[] statusMapRows = entityMap.GetStatusMapRows();
                        aa.InnerText = statusMapRows[0].activiserStatusCode.ToString(); //default to first status/
                        foreach (CrmGatewayConfig.StatusMapRow smr in statusMapRows)
                        {
                            if (smr.CrmStatusCode == int.Parse(value))
                            {
                                aa.InnerText = smr.activiserStatusCode.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.String)
                        {
                            if (string.IsNullOrEmpty(aa.InnerText))
                                aa.InnerText = value.Replace("\n","\r\n");
                            else // concatenate multiple matching lines
                                aa.InnerText += '\n' + value.Replace("\n", "\r\n");
                        }
                        else if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.DateOnly)
                        {
                            aa.InnerText = value;
                        }
                        else if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.TimeOnly)
                        {
                            aa.InnerText = value;
                        }
                        else if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.DateTime)
                        {
                            // CRM passes times as - <Value>2008-05-29T04:46:54-00:00</Value>
                            aa.InnerText = value;
                        }
                        else
                        {
                            aa.InnerText = value;
                        }
                    }
                }
            }

            foreach (XmlElement aa in attributeList.Values)
            {
                ae.AppendChild(aa);
            }
            return aeDoc;
        }


        public static XmlDocument CreateActiviserDoc(string entityXml, string entityPostXml, GatewayTransactionType txType, CrmGatewayConfig.EntityMapRow entityMap, CrmGatewayConfig.AttributeMapDataTable attributeMaps)
        {
            XmlDocument entityDoc = null;
            if (!string.IsNullOrEmpty(entityXml))
            {
                entityDoc = new XmlDocument();
                entityDoc.LoadXml(entityXml);
            }

            bool havePostDoc = false;
            XmlDocument entityPostDoc = null;
            if (!string.IsNullOrEmpty(entityPostXml))
            {
                entityPostDoc = new XmlDocument();
                entityDoc.LoadXml(entityPostXml);
                havePostDoc = true;
            }

            XmlDocument aeDoc = new XmlDocument();
            XmlNode ae = aeDoc.CreateElement(entityMap.activiserEntityName); // create root element
            aeDoc.AppendChild(ae);



            CrmGatewayConfig.AttributeMapRow[] entityAmList = attributeMaps.FindByEntityMapId(entityMap.EntityMapId); //note, this is an ordered list
            Dictionary<string, XmlElement> attributeList = new Dictionary<string, XmlElement>();
            Dictionary<string, string> crmAttributeList = new Dictionary<string, string>();

            // is this a 'SetState' event with 'entityPostData', or an 'incidentResolution', with only entityXml ?


            if (havePostDoc)
            {
                XmlNodeList propertyList = entityDoc.GetElementsByTagName(STR_Property);

                /// process:
                ///     create dictionary<string,string> of <CrmAttributeName>,<Value>
                ///     process dictionary in activiserAttribute, Sequence order
                ///     create new attribute list and new XML document.
                ///     

                foreach (XmlElement element in propertyList)
                {
                    string name = element.GetAttribute(STR_Name);

                    CrmGatewayConfig.AttributeMapRow[] amList = attributeMaps.FindByCrmAttributeName(name);
                    if (amList != null && amList.Length != 0)
                    {
                        XmlNodeList valueNodes = element.GetElementsByTagName(STR_Value);

                        if (valueNodes != null && valueNodes.Count == 1) // can't handle multiple value nodes, which shouldn't exist anyway !
                        {
                            string value = valueNodes[0].InnerText;
                            crmAttributeList.Add(name, value);
                        }
                    }
                }
            }
            else
            {
                PropertyBagCollection entityData = Serialization.Deserialize<PropertyBagCollection>(entityXml);
                if (entityData.Contains(STR_IncidentResolution))
                {
                    DynamicEntity resolution = (DynamicEntity) entityData[STR_IncidentResolution];
                    crmAttributeList.Add(STR_Incidentid, ((Lookup)resolution.Properties[STR_Incidentid]).Value.ToString());
                    crmAttributeList.Add(STR_Statuscode, ((int)entityData[STR_Status]).ToString());
                    // HACK: special case for cases - haha!
                    // are we closing a request ?

                    if (string.Compare(entityMap.activiserEntityName, STR_Request, StringComparison.OrdinalIgnoreCase) == 0
                        && resolution.Properties.Contains(STR_Createdon)
                        )
                    {
                        XmlElement aa = aeDoc.CreateElement(STR_CompletedDate, null);
                        XmlAttribute aadt = aeDoc.CreateAttribute(STR_Datatype);
                        aadt.Value = STR_Datetime;
                        aa.Attributes.Append(aadt);
                        aa.InnerText = (string) resolution.Properties[STR_Createdon];
                        attributeList.Add(STR_CompletedDate, aa);
                    }
                }
                else if (entityData.Contains(STR_EntityMoniker))
                {
                    Moniker moniker = (Moniker)entityData[STR_EntityMoniker];
                    if (string.Compare(moniker.Name, entityMap.CrmEntityName, true, CultureInfo.InvariantCulture) != 0)
                    {
                        throw new ArgumentException("Moniker type doesn't match entity map name");
                    }
                    crmAttributeList.Add(STR_Incidentid, moniker.Id.ToString());
                    crmAttributeList.Add(STR_Statuscode, ((int)entityData[STR_Status]).ToString());
                }
                else
                {
                    // dunno
                }
            }


            foreach (var amr in entityAmList)
            {
                if (txType == GatewayTransactionType.Update && amr.UpdateFromCrm == 0) continue;
                if (txType == GatewayTransactionType.Create && amr.CreateFromCrm == 0) continue;

                if (crmAttributeList.ContainsKey(amr.CrmAttributeName))
                {
                    string value = crmAttributeList[amr.CrmAttributeName];
                    XmlElement aa;
                    if (attributeList.ContainsKey(amr.activiserAttributeName))
                    {
                        aa = attributeList[amr.activiserAttributeName];
                    }
                    else
                    {
                        aa = aeDoc.CreateElement(amr.activiserAttributeName);

                        // add datatype attribute, not particularly useful, except for debugging.
                        XmlAttribute aadt = aeDoc.CreateAttribute(STR_Datatype);
                        aadt.Value = amr.DataType;
                        aa.Attributes.Append(aadt);

                        attributeList.Add(amr.activiserAttributeName, aa);
                    }

                    if (amr.IsStatusAttribute)
                    {
                        CrmGatewayConfig.StatusMapRow[] statusMapRows = entityMap.GetStatusMapRows();
                        aa.InnerText = statusMapRows[0].activiserStatusCode.ToString(); //default to first status/
                        foreach (CrmGatewayConfig.StatusMapRow smr in statusMapRows)
                        {
                            if (smr.CrmStatusCode == int.Parse(value))
                            {
                                aa.InnerText = smr.activiserStatusCode.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.String)
                        {
                            if (string.IsNullOrEmpty(aa.InnerText))
                                aa.InnerText = value;
                            else // concatenate multiple matching lines
                                aa.InnerText += '\n' + value;
                        }
                        else if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.DateOnly)
                        {
                            aa.InnerText = value;
                        }
                        else if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.TimeOnly)
                        {
                            aa.InnerText = value;
                        }
                        else if (Utilities.GetActiviserTypeCode(amr.DataType) == ActiviserAttributeType.DateTime)
                        {
                            // CRM passes times as - <Value>2008-05-29T04:46:54-00:00</Value>
                            aa.InnerText = value;
                        }
                        else
                        {
                            aa.InnerText = value;
                        }
                    }
                }
            }

            foreach (XmlElement aa in attributeList.Values)
            {
                ae.AppendChild(aa);
            }
            return aeDoc;
        }


    }
}