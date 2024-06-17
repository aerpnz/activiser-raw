using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using activiser.WebService.Gateway.Crm.DataAccessLayer.CrmGatewayConfigTableAdapters;

namespace activiser.WebService.Gateway.Crm.DataAccessLayer {
    
    public partial class CrmGatewayConfig {
        partial class AttributeMapDataTable
        {
            // possibly less than optimal to _not_ use a select, 
            // but the list will normally be short, and doing it this way doubles as a way to
            // validate the attribute name.
            public AttributeMapRow[] FindByCrmAttributeName(string crmAttributeName)
            {
                List<AttributeMapRow> amlist = new List<AttributeMapRow>();
                foreach (AttributeMapRow amr in this.Select(null, "Sequence"))
                {
                    if (amr.CrmAttributeName == crmAttributeName) { amlist.Add(amr); }
                }
                return amlist.ToArray() ;
            }

            // possibly less than optimal to _not_ use a select, 
            // but the list will normally be short, and doing it this way doubles as a way to
            // validate the attribute name.
            public AttributeMapRow[] FindByActiviserAttributeName(string activiserAttributeName)
            {
                List<AttributeMapRow> amlist = new List<AttributeMapRow>();
                foreach (AttributeMapRow amr in this.Select(null, "activiserAttributeName, Sequence, CrmAttributeName"))
                {
                    if (amr.activiserAttributeName == activiserAttributeName) { amlist.Add(amr); }
                }
                return amlist.ToArray();
            }

            // possibly less than optimal to _not_ use a select, 
            // but the list will normally be short, and doing it this way doubles as a way to
            // validate the attribute name.
            public AttributeMapRow[] FindByEntityMapId(int entityMapId)
            {
                List<AttributeMapRow> amlist = new List<AttributeMapRow>();

                foreach (AttributeMapRow amr in this.Select(null, "Sequence"))
                {
                    if (amr.EntityMapId == entityMapId) { amlist.Add(amr); }
                }
                return amlist.ToArray();
            }
        }


        //public static CrmGatewayConfig GetGatewayConfig()
        //{
        //    return GetGatewayConfig(null);
        //}
        /// <summary>
        /// Creates and fills a new instance of CrmGatewayConfig.
        /// 
        /// All data is loaded into the dataset, so it really needs to be kept small (which it should be)
        /// 
        /// </summary>
        /// <returns>a new CrmGatewayConfig object</returns>
        public static CrmGatewayConfig GetGatewayConfig(string connectionString)
        {
            CrmGatewayConfig result = new CrmGatewayConfig();
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Properties.Settings.Default.activiserConnectionString;
            }
            
            EntityMapTableAdapter emta = new EntityMapTableAdapter();
            AttributeMapTableAdapter amta = new AttributeMapTableAdapter();
            StatusMapTableAdapter smta = new StatusMapTableAdapter();
            EntityDefaultValueTableAdapter davta = new EntityDefaultValueTableAdapter();
            SettingTableAdapter sta = new SettingTableAdapter();

            emta.Connection.ConnectionString = connectionString;
            amta.Connection.ConnectionString = connectionString;
            smta.Connection.ConnectionString = connectionString;
            davta.Connection.ConnectionString = connectionString;
            sta.Connection.ConnectionString = connectionString;

            try
            {
                emta.Fill(result.EntityMap);
                amta.Fill(result.AttributeMap);
                smta.Fill(result.StatusMap);
                davta.Fill(result.EntityDefaultValue);
                sta.Fill(result.Setting);
                return result;
            }
            finally
            {
                if (emta != null) emta.Dispose();
                if (amta != null) amta.Dispose();
                if (smta != null) smta.Dispose();
                if (davta != null) davta.Dispose();
                if (sta != null) sta.Dispose();
            }
        }

        //public static CrmGatewayConfig GetGatewayCrmEntityConfig(string crmEntityName)
        //{
        //    return GetGatewayCrmEntityConfig(null, crmEntityName);
        //}
        /// <summary>
        /// Creates a new instance of CrmGatewayConfig and fills it with data 
        /// relevant to the specified CRM Entity
        /// </summary>
        /// <param name="crmEntityName">A CRM Entity specified in the CRM gateway configuration</param>
        /// <returns>a new CrmGatewayConfig object</returns>
        public static CrmGatewayConfig GetGatewayCrmEntityConfig(string connectionString, Guid? organizationId, string crmEntityName)
        {
            EntityMapRow emr = GetCrmEntityMap(connectionString, organizationId, crmEntityName);
            if (emr == null) return null;

            CrmGatewayConfig result = new CrmGatewayConfig();
            
            result.EntityMap.ImportRow(emr);

            GetAttributeMapRows(connectionString, emr.EntityMapId, result.AttributeMap);
            GetStatusMapRows(connectionString, emr.EntityMapId, result.StatusMap);
            GetEntityDefaultValueRows(connectionString, emr.EntityMapId, result.EntityDefaultValue);
            GetSettings(connectionString, result.Setting);
            return result;
        }

        /// <summary>
        /// Creates a new instance of CrmGatewayConfig and fills it with data 
        /// relevant to the specified activiser™ Entity
        /// </summary>
        /// <param name="crmEntityName">An activiser™ Entity specified in the CRM gateway configuration</param>
        /// <returns>a new CrmGatewayConfig object</returns>
        public static CrmGatewayConfig GetGatewayActiviserEntityConfig(string connectionString, Guid? organizationId, string activiserEntityName)
        {
            EntityMapRow emr = GetActiviserEntityMap(connectionString, organizationId, activiserEntityName);
            if (emr == null) return null;

            CrmGatewayConfig result = new CrmGatewayConfig();
            result.EntityMap.ImportRow(emr);

            GetAttributeMapRows(connectionString, emr.EntityMapId, result.AttributeMap);
            GetStatusMapRows(connectionString, emr.EntityMapId, result.StatusMap);
            GetEntityDefaultValueRows(connectionString, emr.EntityMapId, result.EntityDefaultValue);
            GetSettings(connectionString, result.Setting);
            return result;
        }

        /// <summary>
        /// Gets a single Entity Map item for the specified CRM Entity
        /// </summary>
        /// <param name="entityName">CRM Entity name</param>
        /// <returns>new EntityMapRow</returns>
        public static EntityMapRow GetCrmEntityMap(string connectionString, Guid? organizationId, string entityName)
        {
            using (EntityMapTableAdapter ta = new EntityMapTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                EntityMapDataTable resultTable = ta.GetDataByCrmEntityName(organizationId, entityName);
                if (resultTable != null && resultTable.Count == 1)
                {
                    return resultTable[0];
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a single Entity Map item for the specified activiser™ Entity
        /// </summary>
        /// <param name="entityName">activiser™ Entity name</param>
        /// <returns>new EntityMapRow</returns>
        public static EntityMapRow GetActiviserEntityMap(string connectionString, Guid? organizationId, string entityName)
        {
            using (EntityMapTableAdapter ta = new EntityMapTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                EntityMapDataTable resultTable = ta.GetDataByActiviserEntityName(organizationId, entityName);
                if (resultTable != null && resultTable.Count == 1)
                {
                    return resultTable[0];
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a single Attribute Map items for the specified Entity
        /// </summary>
        /// <param name="entityMapId">Integer identifier for the entity map entry</param>
        /// <returns>Array of AttributeMapRows on success, null if none found.
        /// </returns>
        public static AttributeMapRow[] GetAttributeMapRows(string connectionString, int entityMapId)
        {
            using (AttributeMapTableAdapter ta = new AttributeMapTableAdapter()) 
            {
                ta.Connection.ConnectionString = connectionString;
                AttributeMapDataTable resultTable = ta.GetDataByEntityMapId(entityMapId);
                if (resultTable != null && resultTable.Count == 1)
                {
                    AttributeMapRow[] result = new AttributeMapRow[resultTable.Count];
                    resultTable.Rows.CopyTo(result, 0);
                    return result;
                }
            }
            return null;   
        }

        public static int GetAttributeMapRows(string connectionString, int entityMapId, AttributeMapDataTable dt)
        {
            using (AttributeMapTableAdapter ta = new AttributeMapTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                try
                {
                    ta.FillByEntityMapId(dt, entityMapId);
                    return dt.Count;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return 0;
                }
            }
        }

        public static StatusMapRow[] GetStatusMapRows(string connectionString, int entityMapId)
        {
            using (StatusMapTableAdapter ta = new StatusMapTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                StatusMapDataTable resultTable = ta.GetDataByEntityMapId(entityMapId);
                if (resultTable != null && resultTable.Count == 1)
                {
                    StatusMapRow[] result = new StatusMapRow[resultTable.Count];
                    resultTable.Rows.CopyTo(result, 0);
                    return result; 
                }
            }
            return null;
        }


        public static int GetStatusMapRows(string connectionString, int entityMapId, StatusMapDataTable dt)
        {
            using (StatusMapTableAdapter ta = new StatusMapTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                ta.FillByEntityMapId(dt, entityMapId);
                return dt.Count;
            }
        }

        public static EntityDefaultValueRow[] GetEntityDefaultValueRows(string connectionString, int entityMapId)
        {
            using (EntityDefaultValueTableAdapter ta = new EntityDefaultValueTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString; 
                EntityDefaultValueDataTable resultTable = ta.GetDataByEntityMapId(entityMapId);
                if (resultTable != null && resultTable.Count == 1)
                {
                    EntityDefaultValueRow[] result = new EntityDefaultValueRow[resultTable.Count];
                    resultTable.Rows.CopyTo(result, 0);
                    return result;
                }
            }
            return null;
        }

        public static int GetEntityDefaultValueRows(string connectionString, int entityMapId, EntityDefaultValueDataTable dt)
        {
            using (EntityDefaultValueTableAdapter ta = new EntityDefaultValueTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                ta.FillByEntityMapId(dt, entityMapId);
                return dt.Count;
            }
        }

        public static SettingRow[] GetSettings(string connectionString)
        {
            using (SettingTableAdapter ta = new SettingTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                SettingDataTable resultTable = ta.GetData();
                if (resultTable != null && resultTable.Count != 0)
                {
                    SettingRow[] result = new SettingRow[resultTable.Count];
                    resultTable.Rows.CopyTo(result, 0);
                    return result;
                }
            }
            return null;
        }

        public static int GetSettings(string connectionString, SettingDataTable dt)
        {
            using (SettingTableAdapter ta = new SettingTableAdapter())
            {
                ta.Connection.ConnectionString = connectionString;
                ta.Fill(dt);
                return dt.Count;
            }
        }
    }
}
