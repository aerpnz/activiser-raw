using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using ExtensionMethods;
using activiser;
using activiser.WebService.Gateway;

namespace activiser.WebService.InputGateway
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://activiser.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public partial class InputGatewayService : System.Web.Services.WebService
    {
        [WebMethod]
        public string Test()
        {
            return Sql.Test(); // "Hello World";
        }

        [WebMethod]
        public string StartTransaction(string gatewayId, string systemId, string userId)
        {
            Guid userGuid;

            if(!userId.IsGuid(out userGuid)) {
                throw new FormatException();
            }

            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        //TODO: do something with this
        [WebMethod]
        public string FinishTransaction(string transactionId)
        {
            return DateTime.UtcNow.ToString("o");
        }

        //[WebMethod]
        //public string Retrieve(string transactionId, string entityName, string entityId)
        //{
        //    Entity entityToFetch = new Entity(entityName, entityId);
        //    Entity fetchedEntity = Sql.FetchEntity(entityToFetch);
        //    return fetchedEntity.EntityXml;
        //}

        [WebMethod]
        public System.Xml.XmlDocument Retrieve(string transactionId, string entityName, string entityId)
        {
            Entity entityToFetch = new Entity(entityName, entityId);
            try
            {
                Entity fetchedEntity = Sql.FetchEntity(entityToFetch);
                return fetchedEntity.EntityDoc;
            }
            catch
            {
                return null;
            }
        }

        //TODO:
        //[WebMethod]
        //public string RetrieveMultiple(string transactionId, string entityList)
        //{
        //    return string.Empty;
        //}

        /// <summary>
        /// Creates a new activiser entity based on the supplied XML data.
        /// 
        /// for example:
        /// &lt;Request&gt;
        ///    &lt;RequestNumber&gt;R-1010-WM7N&lt;/RequestNumber&gt;
        ///    &lt;RequestStatusID&gt;1&lt;/RequestStatusID&gt;
        ///    &lt;AssignedToUid&gt;56e6702a-40ca-dc11-8906-000c294662c8&lt;/AssignedToUid&gt;
        ///    &lt;ShortDescription&gt;Test 2008-03-26.13.43&lt;/ShortDescription&gt;
        ///    &lt;ClientSiteUID&gt;ea5aabf1-a3e9-dc11-9403-000c294662be&lt;/ClientSiteUID&gt;
        ///    &lt;RequestUID&gt;ddf4a9bd-cdfa-dc11-abe0-000c294662c8&lt;/RequestUID&gt;
        /// &lt;/Request&gt;
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="entityData"></param>
        /// <param name="updateIfExtant"></param>
        /// <returns></returns>
        [WebMethod]
        public string Create(string transactionId, string entityData, bool updateIfExists)
        {
            Entity newEntity = new Entity(entityData);

            // for create, missing required attributes aren a problem.
            if (newEntity.Status != ResultCode.Success)
                return newEntity.Status.Description();

            Entity extantEntity = Sql.FetchEntity(newEntity);
            if (extantEntity == null)
            {
                if (Sql.CreateEntity(newEntity) != 1)
                    return ResultCode.UnableToAddEntity.Description();
            }
            else
            {
                if (updateIfExists)
                {
                    if (Sql.UpdateEntity(extantEntity, newEntity) == 1)
                        return ResultCode.ExistingEntityUpdated.Description();
                    else
                        return ResultCode.UnableToUpdateEntity.Description();
                }
                else
                {
                    return ResultCode.EntityAlreadyExists.Description();
                }
            }

            return ResultCode.Success.Description();
        }

        /// <summary>
        /// Updates an existing activiser entity based on the supplied XML data.
        /// 
        /// for example:
        /// &lt;Request&gt;
        ///    &lt;RequestNumber&gt;R-1010-WM7N&lt;/RequestNumber&gt;
        ///    &lt;RequestStatusID&gt;1&lt;/RequestStatusID&gt;
        ///    &lt;AssignedToUid&gt;56e6702a-40ca-dc11-8906-000c294662c8&lt;/AssignedToUid&gt;
        ///    &lt;ShortDescription&gt;Test 2008-03-26.13.43&lt;/ShortDescription&gt;
        ///    &lt;ClientSiteUID&gt;ea5aabf1-a3e9-dc11-9403-000c294662be&lt;/ClientSiteUID&gt;
        ///    &lt;RequestUID&gt;ddf4a9bd-cdfa-dc11-abe0-000c294662c8&lt;/RequestUID&gt;
        /// &lt;/Request&gt;
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="entityData"></param>
        /// <param name="updateIfExtant"></param>
        /// <returns></returns>
        [WebMethod]
        public string Update(string transactionId, string entityData, bool createIfMissing)
        {
            Entity newEntity = new Entity(entityData);

            // for update, missing required attributes aren't a problem.
            if ((newEntity.Status != ResultCode.Success) && (newEntity.Status != ResultCode.EntityMissingRequiredAttribute))
                return newEntity.Status.Description();

            Entity extantEntity = Sql.FetchEntity(newEntity);
            if (extantEntity == null)
            {
                if (createIfMissing)
                {
                    if (Sql.CreateEntity(newEntity) == 1)
                        return ResultCode.MissingEntityCreated.Description();
                    else
                        return ResultCode.UnableToAddEntity.Description();
                }
                else
                {
                    return ResultCode.EntityMissingFromDatabase.Description();
                }
            }
            else
            {
                Sql.UpdateEntity(extantEntity, newEntity);
            }
            return ResultCode.Success.Description();
        }

        private bool EntityExists(string entityName)
        {
            return DataSchema.EntityExists(entityName);
        }
    }
}
