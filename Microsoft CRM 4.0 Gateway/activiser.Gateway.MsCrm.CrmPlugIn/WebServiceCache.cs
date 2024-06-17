using System;
using System.Collections.Generic;
using System.Text;

namespace activiser.InputGateway
{
    sealed class WebServiceCache
    {
        public static activiserCrmInputGateway.activiserInputGatewayCrm activiser 
            = new activiserCrmInputGateway.activiserInputGatewayCrm();

        public static void SetUrl(string url)
        {
            if (url != activiser.Url)
            {
                activiser.Url = url;
            }
        }
    }
}
