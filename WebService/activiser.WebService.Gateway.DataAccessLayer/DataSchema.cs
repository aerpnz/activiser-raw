using activiser.WebService.Gateway.DataSchemaTableAdapters;

namespace activiser.WebService.Gateway
{
    public partial class DataSchema
    {
        /// <summary>
        /// Returns an DataSchema filled with the current schema configuration for a specify entity.
        /// </summary>
        /// <param name="entityName">Name of the entity to retrieve the schema for</param>
        /// <returns>DataSchema</returns>
        /// <remarks></remarks>
        public static DataSchema GetDataSchema(string entityName)
        {
            DataSchema result = new DataSchema();

            using (EntityTableAdapter eta = new EntityTableAdapter())
            using (AttributeTableAdapter ata = new AttributeTableAdapter())
            {

                eta.FillByEntityName(result.Entity, entityName);
                if (result.Entity.Rows.Count != 1) //' should only be one!
                {  
                    result.Dispose();
                    return null;
                }
                ata.FillByEntityId(result.Attribute, result.Entity[0].EntityId);
            }
            return result;
        }

        public static bool EntityExists(string entityName)
        {
            using (EntityTableAdapter eta = new EntityTableAdapter())
            {
                return (bool)eta._EntityExists(entityName);
            }
        }
    }
}
