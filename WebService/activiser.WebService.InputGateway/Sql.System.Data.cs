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

namespace activiser.WebService.InputGateway
{
    public class Sql
    {
        private static Settings mySettings = Settings.Default;

        private static R FetchRow<T, R>(T table, R keyRow)
            where T : DataTable
            where R : DataRow
        {
            try
            {
                //Dim table As DataTable = keyRow.Table 
                List<string> columnList = new List<string>();
                List<string> filterList = new List<string>();
                Dictionary<string, object> filterValues = new Dictionary<string, object>();

                for (int i = 0; i <= table.PrimaryKey.Length - 1; i++)
                {
                    DataColumn column = table.PrimaryKey[i];
                    if (keyRow.IsNull(column))
                    {
                        throw new ArgumentNullException(column.ColumnName, "Primary key filter value null");
                    }
                    else
                    {
                        string pName = string.Format("PK{0}", i);
                        filterList.Add(string.Format("([{0}]=@{1})", column.ColumnName, pName));
                        filterValues.Add(pName, keyRow[column]);
                    }
                }

                if (filterList.Count == 0)
                {
                    throw new ArgumentException("keyRow", "Primary key filter missing");
                }

                DataTable dt = new DataTable(table.TableName);
                foreach (DataColumn column in table.Columns)
                {
                    if (string.IsNullOrEmpty(column.Expression))
                    {
                        columnList.Add(string.Format("[{0}]", column.ColumnName));
                        dt.Columns.Add(column.ColumnName, column.DataType);
                    }
                }

                string sql = string.Format("SELECT {0} FROM {1} WHERE {2}", string.Join(",", columnList.ToArray()), table.TableName, string.Join(" AND ", filterList.ToArray()));


                SqlDataAdapter cmd = new SqlDataAdapter(sql, Settings.Default.activiserConnectionString);
                foreach (string filterName in filterValues.Keys)
                {
                    cmd.SelectCommand.Parameters.AddWithValue(filterName, filterValues[filterName]);
                }
                columnList.Clear();
                filterList.Clear();
                filterValues.Clear();

                columnList = null;
                filterList = null;
                filterValues = null;

                switch (cmd.Fill(dt))
                {
                    case 1:
                        R newRow = (R)table.NewRow();
                        DataRow returnedRow = dt.Rows[0];
                        foreach (DataColumn column in table.Columns)
                        {
                            if (string.IsNullOrEmpty(column.Expression))
                            {
                                newRow[column.ColumnName] = returnedRow[column.ColumnName];
                            }
                        }


                        return newRow;
                    case 0:
                        dt = null;
                        return null;
                    default:
                        throw new InvalidOperationException("FetchRow returned multiple rows for supplied primary key");
                        //break;
                }
            }

            catch (ArgumentException ex)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                throw;
            }
            //return null;
        }

        /// <summary> 
        /// Update a single row in the database 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="originalRow"></param> 
        /// <param name="newRow"></param> 
        /// <returns>1 = row updated 
        /// 0 = now unchanged 
        /// </returns> 
        /// <remarks></remarks> 
        /// TODO: add concurrency conflict check/log facility 
        private static bool UpdateRow<T>(T originalRow, T newRow) where T : DataRow
        {
            try
            {
                DataTable table = newRow.Table;

                //Dim columnList As New List(Of String) 
                List<string> filterList = new List<string>();
                Dictionary<string, object> filterValues = new Dictionary<string, object>();

                List<string> parameterList = new List<string>();
                Dictionary<string, object> parameterValues = new Dictionary<string, object>();


                for (int i = 0; i <= table.PrimaryKey.Length - 1; i++)
                {
                    DataColumn column = table.PrimaryKey[i];
                    if (newRow.IsNull(column))
                    {
                        throw new ArgumentNullException(column.ColumnName, "Primary key filter value null");
                    }
                    else
                    {
                        string pName = string.Format("PK{0}", i);
                        filterList.Add(string.Format("([{0}]=@{1})", column.ColumnName, pName));
                        filterValues.Add(pName, newRow[column]);
                    }
                }

                if (filterList.Count == 0)
                {
                    throw new ArgumentException("newRow", "Primary key filter missing");
                }

                for (int i = 0; i <= table.Columns.Count - 1; i++)
                {
                    DataColumn column = table.Columns[i];
                    if (!string.IsNullOrEmpty(column.Expression))
                        continue;
                    // can't update expression columns 
                    if (column.AutoIncrement)
                        continue;
                    // not gonna be updating auto-numbers. 

                    if (column.ColumnName == "CreatedDateTime")
                        continue;
                    // ignore createdTime column 
                    if (column.ColumnName == "ModifiedDateTime")
                        continue;
                    // ignore modifiedTime column 
                    // if both are null, or if their values are equal, don't update 'em. 
                    if (originalRow.IsNull(i) && newRow.IsNull(i))
                        continue;
                    if (!originalRow.IsNull(i) && !newRow.IsNull(i) && originalRow[i].Equals(newRow[i]))
                        continue;

                    if (newRow.IsNull(i))
                    {
                        parameterList.Add(string.Format("[{0}] = null", column.ColumnName));
                    }
                    else
                    {
                        string argName = string.Format("ARG{0}", column.ColumnName);
                        parameterList.Add(string.Format("[{0}] = @{1}", column.ColumnName, argName));
                        parameterValues.Add(argName, newRow[i]);
                    }
                }
                if (parameterList.Count == 0)
                {
                    return false;
                    // nothing to update. 
                }

                string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", table.TableName, string.Join(",", parameterList.ToArray()), string.Join(" AND ", filterList.ToArray()));

                SqlCommand cmd = new SqlCommand(sql, new SqlConnection(mySettings.activiserConnectionString));
                foreach (string filterName in filterValues.Keys)
                {
                    cmd.Parameters.AddWithValue(filterName, filterValues[filterName]);
                }

                foreach (string argName in parameterValues.Keys)
                {
                    cmd.Parameters.AddWithValue(argName, parameterValues[argName]);
                }

                cmd.Connection.Open();
                int result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return result != 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static bool InsertRow<T>(T newRow) where T : DataRow
        {
            if (newRow == null)
                throw new ArgumentNullException("newRow");
            if (newRow.ItemArray.Length == 0)
                throw new ArgumentException("Row has no data", "newRow");

            try
            {
                DataTable table = newRow.Table;
                List<string> columnList = new List<string>();
                List<string> parameterList = new List<string>();
                Dictionary<string, object> parameterValues = new Dictionary<string, object>();

                // sanity check 
                for (int i = 0; i <= table.PrimaryKey.Length - 1; i++)
                {
                    DataColumn column = table.PrimaryKey[i];
                    if (newRow.IsNull(column))
                    {
                        throw new ArgumentNullException(column.ColumnName, "Primary key value is null");
                    }
                }

                for (int i = 0; i <= table.Columns.Count - 1; i++)
                {
                    DataColumn column = table.Columns[i];
                    if (!string.IsNullOrEmpty(column.Expression))
                        continue;
                    // can't update expression columns 
                    if (column.AutoIncrement)
                        continue;
                    // not gonna be updating auto-numbers. 

                    columnList.Add(string.Format("[{0}]", table.Columns[i].ColumnName));

                    if (newRow.IsNull(i) && ((column.ColumnName == "CreatedDateTime") || (column.ColumnName == "ModifiedDateTime")))
                    {
                        parameterList.Add("GETUTCDATE()");
                    }
                    else
                    {
                        string argName = string.Format("ARG{0}", i);
                        parameterList.Add(string.Format("@{0}", argName));
                        if (newRow.IsNull(i))
                        {
                            parameterValues.Add(argName, DBNull.Value);
                        }
                        else
                        {
                            parameterValues.Add(argName, newRow[i]);
                        }
                    }
                }

                string sql = string.Format("INSERT [{0}]({1}) VALUES({2})", table.TableName, string.Join(",", columnList.ToArray()), string.Join(",", parameterList.ToArray()));

                SqlCommand cmd = new SqlCommand(sql, new SqlConnection(mySettings.activiserConnectionString));
                foreach (string argName in parameterValues.Keys)
                {
                    cmd.Parameters.AddWithValue(argName, parameterValues[argName]);
                }

                cmd.Connection.Open();
                int result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return result != 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Serializable()]
        internal class InsertException : System.Exception
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
        internal class UpdateException : System.Exception
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
