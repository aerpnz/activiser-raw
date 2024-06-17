//(C)++
//(C) Copyright 2008, Activiser(tm) Limited
//(C)--

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using activiser.WebService.Properties;
using System.Xml;

namespace activiser.WebService.InputGateway
{
    class Sql
    {
        private static Settings mySettings = Settings.Default;

        public static string Test()
        {
            using (SqlConnection activiserConnection = new SqlConnection(mySettings.activiserConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT @User=RTRIM(SYSTEM_USER),@LocalTime=GETDATE(),@HostName=SERVERPROPERTY('MachineName'),@InstanceName=COALESCE(SERVERPROPERTY('InstanceName'),'<default>'),@DbName=DB_NAME()", activiserConnection))
                {
                    SqlParameter user = new SqlParameter("User", SqlDbType.NVarChar, 128, ParameterDirection.Output, true, 0,0,null, DataRowVersion.Default, null);
                    SqlParameter localtime = new SqlParameter("LocalTime", SqlDbType.DateTime, 128, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Default, null);
                    SqlParameter hostName = new SqlParameter("HostName", SqlDbType.Variant, 128, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Default, null);
                    SqlParameter instanceName = new SqlParameter("InstanceName", SqlDbType.Variant, 128, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Default, null);
                    SqlParameter dbName = new SqlParameter("DbName", SqlDbType.NVarChar, 128, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Default, null);
                    cmd.Parameters.Add(user);
                    cmd.Parameters.Add(localtime);
                    cmd.Parameters.Add(hostName);
                    cmd.Parameters.Add(instanceName);
                    cmd.Parameters.Add(dbName);
                    activiserConnection.Open();
                    cmd.ExecuteNonQuery();
                    activiserConnection.Close();

                    return string.Format(Resources.TestMessageFormat, user, ((DateTime)localtime.Value).ToString("u"), DateTime.Now.ToString("u"), (string)hostName.Value + '/' + (string)instanceName.Value, (string)dbName.Value);
                }
            }
        }

        public static Entity FetchEntity(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException(Resources.EntityArgumentName);
            if (entity.Status != ResultCode.Success && entity.Status != ResultCode.Unknown && entity.Status != ResultCode.EntityMissingRequiredAttribute)
            {
                throw new ArgumentException(Resources.EntityDataNotValid, Resources.EntityArgumentName);
            }

            using (SqlConnection activiserConnection = new SqlConnection(mySettings.activiserConnectionString))
            {
                string[] attributeList = new string[entity.DefinedAttributes.Count];
                entity.DefinedAttributes.Keys.CopyTo(attributeList, 0);
                string schemaSql = string.Format("SELECT {0} FROM {1}", string.Join(", ", attributeList), entity.Name);

                SqlDataAdapter cmd = new SqlDataAdapter(schemaSql, activiserConnection);
                DataTable dt = new DataTable(entity.Name);
                cmd.FillSchema(dt, SchemaType.Mapped);

                string pName = string.Format("PK{0}", entity.Name);
                string pValue = entity.Attributes[entity.PrimaryKeyAttribute];
                string sql = string.Format("{0} WHERE ({1}=@{2})", schemaSql, entity.PrimaryKeyAttribute, pName);

                cmd = new SqlDataAdapter(sql, activiserConnection);
                cmd.SelectCommand.Parameters.AddWithValue(pName, pValue);

                switch (cmd.Fill(dt))
                {
                    case 1:
                        return new Entity(dt.Rows[0]);
                    case 0:
                        return null;
                    default:
                        throw new InvalidOperationException(Resources.FetchEntityFoundMultipleRows);
                    //break;
                }
            }
        }

        public static int CreateEntity(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException(Resources.EntityArgumentName);
            if (entity.Status != ResultCode.Success) 
                throw new ArgumentException(Resources.EntityDataNotValid, Resources.EntityArgumentName);

            using (SqlConnection activiserConnection = new SqlConnection(mySettings.activiserConnectionString))
            {
                queryColumnList columnList = new queryColumnList(entity.Name);
                foreach (string columnName in entity.Attributes.Keys)
                    columnList.Add(columnName, false, entity.Attributes[columnName]);

                if (columnList.Count == 0) 
                    throw new ArgumentException(Resources.EntityDataNotValid, Resources.EntityArgumentName);

                columnList.Add(entity.PrimaryKeyAttribute, true, entity.PrimaryKeyValue);
                using (SqlDataAdapter da = new SqlDataAdapter(columnList.SchemaQuery(), activiserConnection))
                using (DataTable table = new DataTable(entity.Name))
                {
                    da.FillSchema(table, SchemaType.Mapped);

                    foreach (string columnName in columnList.ColumnNames)
                    {
                        DataColumn tableColumn = table.Columns[columnName];
                        // remove auto-increment columns from insert.
                        if (tableColumn.AutoIncrement || !string.IsNullOrEmpty(tableColumn.Expression))
                        {
                            columnList.Remove(columnName);
                            continue;
                        }
                        queryColumn c = columnList[columnName];
                        if (c == null)
                        {
                            continue;
                        }
                        if (tableColumn.DataType == typeof(Guid))
                        {
                            c.Value = new Guid((string)c.Value);
                        }
                        else if(tableColumn.DataType == typeof(DateTime))
                        {
                            DateTime specifiedDate;
                            if (DateTime.TryParse((string)c.Value, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out specifiedDate))
                            {
                                c.Value = specifiedDate.ToUniversalTime();
                            }
                            else
                            {
                                c.Value = DBNull.Value;
                            }
                        }
                        else
                        {
                            c.Value = Convert.ChangeType((string)c.Value, tableColumn.DataType);
                        }
                    }
                }
                
                using (SqlCommand cmd = new SqlCommand(columnList.InsertQuery(), activiserConnection))
                {
                    columnList.AddSqlParameters(cmd);
                    activiserConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    activiserConnection.Close();
                    return result;
                }
            }
        }

        public static int UpdateEntity(Entity oldEntity, Entity newEntity)
        {
            if (newEntity == null) throw new ArgumentNullException("newEntity");
            if (newEntity.Status != ResultCode.Success) throw new ArgumentException(Resources.NewEntityDataNotValid, "newEntity");
            if (oldEntity == null) throw new ArgumentNullException("oldEntity");
            if (oldEntity.Status != ResultCode.Success) throw new ArgumentException(Resources.OldEntityDataNotValid, "oldEntity");

            if (string.Compare(oldEntity.PrimaryKeyValue, newEntity.PrimaryKeyValue, true) != 0)
                throw new InvalidOperationException("Not allowed to change primary key value");

            using (SqlConnection activiserConnection = new SqlConnection(mySettings.activiserConnectionString))
            {
                // build private pretty column list
                queryColumnList columnList = new queryColumnList(newEntity.Name);
                foreach (string columnName in newEntity.Attributes.Keys)
                {
                    if (oldEntity.Attributes.ContainsKey(columnName) && oldEntity.Attributes[columnName] == newEntity.Attributes[columnName])
                        continue; // no need to update unchanged data.
                    else
                        columnList.Add(columnName, false, newEntity.Attributes[columnName]);
                }

                // nothing to update, abort
                if (columnList.Count == 0) return 0;

                // add PK
                queryColumn pkc = columnList.Add(newEntity.PrimaryKeyAttribute, true, newEntity.PrimaryKeyValue);

                using (SqlDataAdapter da = new SqlDataAdapter(columnList.SchemaQuery(), activiserConnection))
                using (DataTable table = new DataTable(newEntity.Name))
                {
                    da.FillSchema(table, SchemaType.Mapped);

                    try
                    {
                        foreach (string columnName in columnList.ColumnNames)
                        {
                            DataColumn tableColumn = table.Columns[columnName];
                            // remove auto-increment columns from update.
                            if (tableColumn.AutoIncrement || !string.IsNullOrEmpty(tableColumn.Expression))
                            {
                                columnList.Remove(columnName);
                                continue;
                            }
                            queryColumn c = columnList[columnName];

                            if (c == null) //ignore missing data.
                            {
                                columnList.Remove(columnName);
                                continue;
                            }

                            if (tableColumn.DataType == typeof(Guid))
                            {
                                c.Value = new Guid((string)c.Value);
                                continue;
                            }
                            
                            if (tableColumn.DataType == typeof(DateTime))
                            {
                                DateTime specifiedDate;
                                if (DateTime.TryParse((string)c.Value, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out specifiedDate))
                                {
                                    c.Value = specifiedDate.ToUniversalTime();
                                }
                                else
                                {
                                    c.Value = DBNull.Value;
                                }
                                continue;
                            }

                            c.Value = Convert.ChangeType((string)c.Value, tableColumn.DataType);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                    if (columnList.Count == 0) return 0; // still nothing to update.

                    // fix PK data
                    if (table.Columns[newEntity.PrimaryKeyAttribute].DataType == typeof(Guid))
                    {
                        pkc.Value = new Guid((string)pkc.Value);
                    }
                    else
                    {
                        pkc.Value = Convert.ChangeType(pkc.Value, table.Columns[newEntity.PrimaryKeyAttribute].DataType);
                    }
                }

                using (SqlCommand cmd = new SqlCommand(columnList.UpdateQuery(), activiserConnection))
                {
                    columnList.AddSqlParameters(cmd);
                    activiserConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    activiserConnection.Close();
                    return result;
                }
            }
        }

        public static string SerialiseDataRow(DataRow row)
        {
            if (row == null) throw new ArgumentNullException("row");
            XmlDocument xmlResult = new XmlDocument();
            XmlNode rootNode = xmlResult.CreateNode(XmlNodeType.Element, row.Table.TableName, null);
            xmlResult.AppendChild(rootNode);
            foreach (DataColumn dc in row.Table.Columns)
            {
                XmlNode columnNode = xmlResult.CreateNode(XmlNodeType.Element, dc.ColumnName, null);
                columnNode.InnerText = SerializeValue(row[dc]);
                rootNode.AppendChild(columnNode);
            }
            return xmlResult.OuterXml;
        }

        public static string SerializeValue(object value)
        {
            if (object.ReferenceEquals(value.GetType(), typeof(DateTime)))
            {
                return ((DateTime)value).ToString("o");
            }
            else if (object.ReferenceEquals(value.GetType(), typeof(DateTimeOffset)))
            {
                return ((DateTimeOffset)value).ToString("o");
            }
            else
            {
                return value.ToString();
            }
        }

        private class queryColumn
        {
            public string ColumnName { get; set; }
            public bool PrimaryKey { get; set; }
            public object Value { get; set; }
            public string DataType { get; set; }
            public string ParameterName { get { return string.Format(PrimaryKey ? "@PK{0}" : "@ARG{0}", ColumnName); } }
            public string ArgumentName { get { return string.Format(PrimaryKey ? "PK{0}" : "ARG{0}", ColumnName); } }
            public string UpdateSnippet { get { return string.Format("{0} = {1}", ColumnName, ParameterName); } }
        }


        private class queryColumnList : IEnumerable<queryColumn>
        {
            //private Dictionary<string, queryColumn> _columnDictionary = new Dictionary<string, queryColumn>();
            private List<queryColumn> _columnList = new List<queryColumn>();
            private queryColumn _primaryKey;

            public string EntityName { get; set; }

            public queryColumnList(string entityName)
            {
                EntityName = entityName;
            }

            public queryColumn this[string columnName] {
                get
                {
                    foreach (queryColumn c in _columnList)
                    {
                        if (c.ColumnName == columnName) return c;
                    }
                    return null;
                }
            }

            public bool Contains(string columnName)
            {
                foreach (queryColumn c in _columnList)
                {
                    if (c.ColumnName == columnName) return true;
                }
                return false;
            }

            public int Count { get { return _columnList.Count; } }

            public queryColumn Add(string columnName, object value)
            {
                queryColumn result = new queryColumn { ColumnName = columnName, Value = value };
                _columnList.Add(result);
                return result;
            }


            public queryColumn Add(string columnName, bool primaryKey, object value)
            {
                queryColumn result = new queryColumn { ColumnName = columnName, Value = value, PrimaryKey = primaryKey };
                if (primaryKey) 
                    _primaryKey = result;
                else 
                    _columnList.Add(result);

                return result;
            }

            public void Remove(string columnName)
            {
                foreach (queryColumn c in _columnList)
                {
                    if (c.ColumnName == columnName)
                    {
                        _columnList.Remove(c);
                        return;
                    }
                }
            }

            public string SchemaQuery()
            {
                List<string> schemaColumns = new List<string>(ColumnNames);
                schemaColumns.Add(_primaryKey.ColumnName);
                return string.Format("SELECT {0} FROM {1}", string.Join(", ", schemaColumns.ToArray()), EntityName);
            }

            public string InsertQuery()
            {
                string result = string.Format("INSERT [{0}]({1}) VALUES({2})",
                    EntityName,
                    string.Join(",", ColumnNames),
                    string.Join(",", ParameterNames)
                    );
                return result;
            }

            public string UpdateQuery()
            {
                if (_primaryKey == null)
                    throw new ArgumentException("Primary key information missing.");

                return string.Format("UPDATE {0} SET {1} WHERE {2}={3}",
                        EntityName, 
                        string.Join(",", UpdateSnippets),
                        _primaryKey.ColumnName,
                        _primaryKey.ParameterName);
            }

            public void AddSqlParameters(SqlCommand cmd)
            {
                foreach (queryColumn c in _columnList)
                {
                    if (c.Value is string)
                    {
                        string sValue = (string)c.Value;
                        if (sValue.Contains("\n") && !sValue.Contains("\r\n")) // restore CRLF for data inbound from LF-only systems
                            sValue = sValue.Replace("\n","\r\n");

                        cmd.Parameters.AddWithValue(c.ArgumentName, sValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(c.ArgumentName, c.Value);
                    }
                }
                if (_primaryKey != null)
                {
                    cmd.Parameters.AddWithValue(_primaryKey.ArgumentName, _primaryKey.Value);
                }
            }

            public string[] ColumnNames
            {
                get
                {
                    List<string> result = new List<string>();
                    bool gotCreated = false, gotModded = false;
                    foreach (queryColumn c in _columnList)
                    {
                        result.Add(c.ColumnName);
                        if (c.ColumnName == Properties.Resources.TimestampColumnCreated) gotCreated = true;
                        if (c.ColumnName == Properties.Resources.TimestampColumnModified) gotModded = true;
                    }
                    if (!gotCreated)
                    {
                        result.Add(Properties.Resources.TimestampColumnCreated);
                    }
                    if (!gotModded)
                    {
                        result.Add(Properties.Resources.TimestampColumnModified);
                    }

                    return result.ToArray();
                }
            }

            public string[] ParameterNames
            {
                get
                {
                    List<string> result = new List<string>();
                    bool gotCreated = false, gotModded = false;
                    foreach (queryColumn c in _columnList)
                    {
                        result.Add(c.ParameterName);
                        if (c.ColumnName == Properties.Resources.TimestampColumnCreated) gotCreated = true;
                        if (c.ColumnName == Properties.Resources.TimestampColumnModified) gotModded = true;
                    }
                    if (!gotCreated)
                    {
                        result.Add("GETUTCDATE()");
                    }
                    if (!gotModded)
                    {
                        result.Add("GETUTCDATE()");
                    }
                    return result.ToArray();
                }
            }

            public string[] ArgumentNames
            {
                get
                {
                    List<string> result = new List<string>();
                    foreach (queryColumn c in _columnList)
                    {
                        result.Add(c.ArgumentName);
                    }
                    return result.ToArray();
                }
            }

            public string[] UpdateSnippets
            {
                get
                {
                    List<string> result = new List<string>();
                    bool gotModded = false;
                    foreach (queryColumn c in _columnList)
                    {
                        result.Add(c.UpdateSnippet);
                        if (c.ColumnName == Properties.Resources.TimestampColumnModified) gotModded = true;
                    }
                    if (!gotModded)
                    {
                        result.Add(string.Format("{0} = GETUTCDATE()", Properties.Resources.TimestampColumnModified));
                    }

                    return result.ToArray();
                }
            }

            public object[] Values
            {
                get
                {
                    List<object> result = new List<object>();
                    foreach (queryColumn c in _columnList)
                    {
                        result.Add(c.Value);
                    }
                    return result.ToArray();
                }
            }

            #region IEnumerable<queryColumn> Members

            public IEnumerator<queryColumn> GetEnumerator()
            {
                return _columnList.GetEnumerator();
//                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _columnList.GetEnumerator();
//                throw new NotImplementedException();
            }

            #endregion
        }

        [Serializable()]
        public class FetchException : System.Exception
        {

            public FetchException()
                : base()
            {
            }

            public FetchException(string message)
                : base(message)
            {
            }

            public FetchException(string message, Exception inner)
                : base(message, inner)
            {
            }

            protected FetchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
                : base(info, context)
            {
            }
        }

        [Serializable()]
        public class InsertException : System.Exception
        {

            public InsertException()
                : base()
            {
            }

            public InsertException(string message)
                : base(message)
            {
            }

            public InsertException(string message, Exception inner)
                : base(message, inner)
            {
            }

            protected InsertException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
                : base(info, context)
            {
            }
        }

        [Serializable()]
        public class UpdateException : System.Exception
        {

            public UpdateException()
                : base()
            {
            }

            public UpdateException(string message)
                : base(message)
            {
            }

            public UpdateException(string message, Exception inner)
                : base(message, inner)
            {
            }

            protected UpdateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
                : base(info, context)
            {
            }
        } 
    }
}
