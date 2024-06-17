using System;
using System.Data;
using System.Collections.Generic;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;
using System.Diagnostics;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private bool CompareDynamicProperty(object a, object b)
        {
            if ((a is DBNull) && (b is DBNull))
                return true;

            if ((a is string) && (b is string))
                return (string)a == (string)b;

            if ((a is Key) && (b is Key))
                return ((Key)a).Equals((Key)b);

            if ((a is CrmReference) && (b is CrmReference))
                return ((CrmReference)a).Value == ((CrmReference)b).Value;

            if ((a is Guid) && (b is Guid))
                return (Guid)a == (Guid)b;

            if ((a is DateTime) && (b is DateTime))
                return (DateTime)a == (DateTime)b;

            if ((a is CrmDateTime) && (b is CrmDateTime))
                return ((CrmDateTime)a).UniversalTime == ((CrmDateTime)b).UniversalTime;

            if (a.GetType() == b.GetType())
                return a.Equals(b);

            return false;
        }
    }
}