using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using activiser.WebService.Gateway.Crm.DataAccessLayer.CrmGatewayConfigTableAdapters;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using System.Reflection;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        //public static Property CreateProperty(string propertyName, string crmDataType, object value)
        //{
        //    return CreateProperty(propertyName, crmDataType, value, "");
        //}

        public static Property CreateProperty(string propertyName, string crmDataType, object value, string typeCode, int? maxLength)
        {
            object crmValue;
            switch (crmDataType.ToLower())
            {
                case "bit":
                case "boolean":
                case "crmboolean":
                    {
                        crmValue = new CrmBoolean();
                        ((CrmBoolean)crmValue).Value = (bool)value;
                        break;
                    }
                case "datetime":
                case "crmdatetime":
                    {
                        if (value is DBNull)
                        {
                            crmValue = new CrmDateTime();
                            ((CrmDateTime)crmValue).IsNull = true;
                        }
                        else
                        {
                            if (maxLength.HasValue && (maxLength.Value != 0))
                            {
                                crmValue = CrmDateTime.FromUser(DateTime.SpecifyKind((DateTime)value, DateTimeKind.Local));
                            }
                            else
                            {
                                crmValue = CrmDateTime.FromUniversal(DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc));
                            }
                        }
                        break;
                    }
                case "decimal":
                case "crmdecimal":
                    {
                        crmValue = new CrmDecimal();
                        ((CrmDecimal)crmValue).Value = (decimal)value;

                        break;
                    }
                case "float":
                case "crmfloat":
                    {
                        crmValue = new CrmFloat();
                        ((CrmFloat)crmValue).Value = (double)value;

                        break;
                    }
                case "money":
                case "crmmoney":
                    {
                        crmValue = new CrmMoney();
                        ((CrmMoney)crmValue).Value = (decimal)value;

                        break;
                    }
                case "int":
                case "number":
                case "crmnumber":
                    {
                        crmValue = new CrmNumber();
                        ((CrmNumber)crmValue).Value = (int)value;

                        break;
                    }
                case "customer":
                    {
                        if (string.IsNullOrEmpty(typeCode)) throw new ArgumentNullException("typeCode");
                        crmValue = new Customer();
                        // ((Customer)crmValue).name = typeCode;
                        ((Customer)crmValue).type = typeCode;
                        ((Customer)crmValue).Value = (Guid)value;

                        break;
                    }
                case "entitynamereference":
                    {
                        crmValue = new EntityNameReference();
                        ((EntityNameReference)crmValue).Value = (string)value;

                        break;
                    }
                case "primarykey":
                case "key":
                    {
                        crmValue = new Key();
                        ((Key)crmValue).Value = (Guid)value;

                        break;
                    }
                case "lookup":
                    {
                        if (string.IsNullOrEmpty(typeCode)) throw new ArgumentNullException("typeCode");
                        crmValue = new Lookup();
                        // ((Lookup)crmValue).name = typeCode;
                        ((Lookup)crmValue).type = typeCode;
                        ((Lookup)crmValue).Value = (Guid)value;

                        break;
                    }
                case "owner":
                    {
                        if (string.IsNullOrEmpty(typeCode)) throw new ArgumentNullException("typeCode");
                        crmValue = new Owner();
                        ((Owner)crmValue).type = typeCode;
                        ((Owner)crmValue).Value = (Guid)value;

                        break;
                    }
                case "picklist":
                    {
                        crmValue = new Picklist();
                        ((Picklist)crmValue).Value = (int)value;

                        break;
                    }
                //case "partylist":
                //    {
                //        p = new activityparty();
                //        break;
                //    }
                case "status":
                    {
                        crmValue = new Status();
                        ((Status)crmValue).Value = (int)value;

                        break;
                    }
                case "state":
                    {
                        crmValue = (string) value ;
                        break;
                    }
                case "string":
                case "nvarchar":
                case "ntext":
                case "text":
                    {
                        if (value is DBNull)
                        {
                            crmValue = string.Empty;
                        }
                        else
                        {
                            string stringValue = (string)value;
                            if (maxLength.HasValue && stringValue.Length > maxLength.Value)
                                crmValue = stringValue.Substring(0, maxLength.Value);
                            else
                                crmValue = stringValue;
                        }
                        break;
                    }
                case "guid":
                case "uniqueidentifier":
                    {
                        crmValue = new UniqueIdentifier();
                        ((UniqueIdentifier)crmValue).Value = (Guid)value;
                        break;
                    }
                default:
                    {
                        throw new PropertyTypeNotSupportedException(value);
                    }
            }

            try
            {
                Property p = DynamicEntity.PropertyFactory.CreateInstance(propertyName, crmValue);
                return p;
            }
            catch (DynamicEntity.PropertyTypeNotSupportedException)
            {
                throw; // new PropertyTypeNotSupportedException(value);
            }

        }

        //public static void SetPropertyValue(Property property, object value)
        //{
        //    DynamicEntity.PropertyFactory.SetPropertyValue(property, value);
        //}

        //public static object GetPropertyValue(Property property)
        //{
        //    return DynamicEntity.PropertyFactory.GetPropertyValue(property);
        //}

        [global::System.Serializable]
        public class PropertyTypeNotSupportedException : Exception
        {
            public PropertyTypeNotSupportedException() { }
            public PropertyTypeNotSupportedException(string message) : base(message) { }
            public PropertyTypeNotSupportedException(string message, Exception inner) : base(message, inner) { }
            protected PropertyTypeNotSupportedException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }

            public PropertyTypeNotSupportedException(object value) : base("Property type is not supported.") {
                _type = value.GetType();
            }

            private System.Type _type;
            public System.Type ObjectType { get { return _type; } }
        }
    }
}
