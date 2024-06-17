using System;
using System.Data;

namespace activiser.WebService.OutputGateway
{
    public interface IOutputGateway
    {
        Guid GatewayId { get; }

        string ConnectionString { get; set; }

        int Save(Guid transactionId, DataSet transactionData, out Exception saveException);

        string Test();

    }
}