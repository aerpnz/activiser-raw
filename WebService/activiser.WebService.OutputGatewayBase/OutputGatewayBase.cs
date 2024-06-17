using System;
using System.Data;

namespace activiser.WebService.OutputGateway
{
    public abstract class OutputGatewayBase : IOutputGateway
    {

        private string _connectionString;

        public OutputGatewayBase()
        {

        }

        public OutputGatewayBase(string connectionString)
        {
            this._connectionString = (connectionString);
        }

        #region IOutputGateway Members

        string IOutputGateway.ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        string IOutputGateway.Test()
        {
            return "Pass";
        }

        int IOutputGateway.Save(Guid transactionId, DataSet transactionData, out Exception saveException)
        {
            saveException = null;
            return 0;
        }

        private static Guid _gatewayId = System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID;
        Guid IOutputGateway.GatewayId
        {
            get { return _gatewayId; }
        }

        #endregion
    }
}