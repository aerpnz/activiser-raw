//(C)++
//(C) Copyright 2008, Activiser(tm) Limited
//(C)--

using System;
using System.Collections.Generic;
using System.Xml;
using System.Collections.Specialized;
using ExtensionMethods;
using System.Diagnostics;
using activiser.WebService;
using activiser.WebService.Gateway;

namespace activiser.WebService.InputGateway
{
    /// <summary>
    /// Deserializes an XML fragment into a string dictionary.
    /// Optionally, first serializes a DataRow to XML and then deserializes the XML into the string dictionary.
    /// This is used by the gateway functions as a simplified mechanism for validation and conversion to activiser data.
    /// </summary>

    public class Entity
    {
        public Entity(string entityXml)
        {
            EntityDoc = GetXmlDocument(entityXml);
        }

        // todo: change this so we don't serialize and deserialise the datarow.
        public Entity(System.Data.DataRow entityRow)
        {


            if (entityRow == null) throw new ArgumentNullException("entityRow");
            if (entityRow.Table == null) throw new ArgumentException("entityRow must be attached to a table");

            Name = entityRow.Table.TableName;
            GetDefinedSchema();
            if (Status == ResultCode.EntityNotDefined) throw new ArgumentException(ResultCode.EntityNotDefined.Description());

            foreach (System.Data.DataColumn dc in entityRow.Table.Columns)
            {
                AttributeKeys.Add(dc.ColumnName);
                Attributes.Add(dc.ColumnName, Sql.SerializeValue(entityRow[dc.ColumnName]));
            }

             // EntityDoc = Sql.SerialiseDataRow(entityRow);
            EntityXml = Sql.SerialiseDataRow(entityRow);
            _EntityDoc = GetXmlDocument(EntityXml);
            
            this.Status = TestStructure();
        }

        // constructor for 'fetch'
        public Entity(string entityName, string entityId)
        {
            Name = entityName;
            GetDefinedSchema();
            PrimaryKeyValue = entityId;
            Attributes.Add(PrimaryKeyAttribute, entityId);
        }

        public string EntityXml { get; private set; }
        public string Name { get; private set; }

        private XmlDocument _EntityDoc;
        public XmlDocument EntityDoc
        {
            get
            {
                return _EntityDoc;
            }
            private set
            {
                _EntityDoc = value;
                if (value == null) return;
                Name = _EntityDoc.DocumentElement.Name;
                EntityXml = _EntityDoc.InnerXml;

                GetDefinedSchema();
                if (Status == ResultCode.EntityNotDefined) throw new ArgumentException(ResultCode.EntityNotDefined.Description());

                foreach (XmlNode child in _EntityDoc.DocumentElement.ChildNodes)
                {
                    if (child is XmlElement)
                    {
                        AttributeKeys.Add(child.Name);
                        Attributes.Add(child.Name, child.InnerText);
                    }
                }

                this.Status = TestStructure();
            }
        }

        private List<string> _AttributeKeys = new List<string>();
        public List<string> AttributeKeys { get { return _AttributeKeys; } }
        
        private Dictionary<string, string> _Attributes = new Dictionary<string, string>();
        public Dictionary<string, string> Attributes { get { return _Attributes; } }

        public object this[string attributeName] { get { return Attributes[attributeName]; } }
        public bool Contains(string attributeName) { return Attributes.ContainsKey(attributeName); }

        public string PrimaryKeyAttribute { get; set; }
        public string PrimaryKeyValue { get; set; }

        public ResultCode Status { get; set; }

        private List<string> _RequiredAttributes = new List<string>();
        public List<string> RequiredAttributes { get { return _RequiredAttributes; } }
        private Dictionary<string, int> _DefinedAttributes = new Dictionary<string, int>();
        public Dictionary<string,int> DefinedAttributes { get { return _DefinedAttributes; } }

        private void GetDefinedSchema()
        {
            Status = ResultCode.Unknown;

            DataSchema ads = DataSchema.GetDataSchema(Name);
            if (ads == null || ads.Entity.Count != 1)
            {
                Status = ResultCode.EntityNotDefined;
                return;
            }

            DataSchema.EntityRow er = ads.Entity[0];
            PrimaryKeyAttribute = er.PrimaryKeyAttributeName;

            foreach (DataSchema.AttributeRow ar in er.GetAttributeRows())
            {
                DefinedAttributes.Add(ar.AttributeName, ar.AttributeTypeCode);
                if (ar.Required) RequiredAttributes.Add(ar.AttributeName);
            }
        }

        private ResultCode TestStructure()
        {
        //TODO: add data type checking.
            try
            {
                bool gotPK = false;
                bool gotProblem = false;
                List<string> problemList = new List<string>();
                foreach (string an in AttributeKeys)
                {
                    if (!DefinedAttributes.ContainsKey(an))
                    {
                        problemList.Add(an);
                        gotProblem = true;
                    }
                    if (an == PrimaryKeyAttribute)
                    {
                        PrimaryKeyValue = Attributes[an];
                        gotPK = true;
                    }
                }
                if (gotProblem)
                {
                    string[] attributeList = new string[DefinedAttributes.Count];
                    DefinedAttributes.Keys.CopyTo(attributeList, 0);
                    LogMessage("Undefined attributes found in input entity '{0}':\n\t{1}\ndefined attributes:\n\t{2}",
                        EventLogEntryType.Warning, 
                        Name,
                        string.Join(",\n\t", problemList.ToArray()),
                        string.Join(",\n\t", attributeList)
                        );
                    return ResultCode.EntityAttributeNotDefined;
                }

                if (!gotPK)
                {
                    LogMessage("input entity '{0}' missing primary key value", EventLogEntryType.Error, Name);
                    return ResultCode.EntityMissingPrimaryKey;
                }

                problemList.Clear();
                gotProblem = false;
                foreach (string ra in RequiredAttributes)
                    if (!Attributes.ContainsKey(ra))
                    {
                        gotProblem = true;
                        problemList.Add(ra);
                    }

                if (gotProblem)
                {
                    LogMessage("Missing required attributes found in input entity '{0}':\n\t{1}\nrequired attributes:\n\t{2}",
                        EventLogEntryType.Warning,
                        Name,
                        string.Join(",\n\t", problemList.ToArray()),
                        string.Join(",\n\t", RequiredAttributes.ToArray())
                        );
                    return ResultCode.EntityMissingRequiredAttribute; // Note: not necessarily fatal
                }
                return ResultCode.Success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static XmlDocument GetXmlDocument(string xmlText)
        {
            XmlDocument result = new XmlDocument();
            result.LoadXml(xmlText);
            return result;
        }

        static private void LogMessage(string message, EventLogEntryType entryType, params object[] args)
        {
            if (args != null) message = string.Format(message, args);
            EventLog errorLogger = new System.Diagnostics.EventLog(String.Empty, Environment.MachineName, Properties.Resources.EventLogSource);
            if (errorLogger != null)
            {
                errorLogger.WriteEntry(message, entryType, 0, 0);
            }
        }

    }
}
