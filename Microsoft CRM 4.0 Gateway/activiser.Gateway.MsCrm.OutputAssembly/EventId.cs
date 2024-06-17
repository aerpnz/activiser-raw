using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace activiser.WebService.CrmOutputGateway
{
    public enum EventId
    {
        Unspecifified = 0,

        GeneralSaveError,

        FailedToGetCrmServiceInstance = 1001,
        UnsupportedProperty = 1002,

        RetrieveFailure = 2014,

        AssignFailure = 3014,

        SetStateFailure = 4014,

        CreatingEntity = 5001,
        CreateFailure = 5014,
        CreateSuccessAudit = 5101,
        CreateFailureAudit = 5114,

        UpdatingEntity = 6001,
        UpdateFailure = 6013,
        UpdateSuccessAudit = 6101,
        UpdateFailureAudit = 6114,

        GatewayTestSuccessAudit = 9101,
        GatewayTestFailureAudit = 9114,
        

    }
}
