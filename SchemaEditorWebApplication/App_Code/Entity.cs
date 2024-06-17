using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;

namespace SchemaEditorWebApplication
{
    [MetadataType(typeof(EntityMetaData))]
    public partial class Entity
    {
        
    }

    public partial class EntityMetaData
    {
        [ScaffoldColumn(false)]
        
        public DateTime Created;

        [ScaffoldColumn(false)]
        public String CreatedBy;

        [ScaffoldColumn(false)]
        public DateTime Modified;

        [ScaffoldColumn(false)]
        public String ModifiedBy;     
    }
}
