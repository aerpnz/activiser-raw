namespace activiser.WebService.Gateway {
    
    public partial class GatewayTransactionDataSet {
    }
}

namespace activiser.WebService.Gateway.GatewayTransactionDataSetTableAdapters {
    public partial class GatewayTransactionDataTableAdapter
    {
        private int _commandTimeout;

        public int CommandTimout
        {
            get
            {
                return _commandTimeout;
            }
            set
            {
                _commandTimeout = value;
                foreach (System.Data.SqlClient.SqlCommand cmd in CommandCollection)
                {
                    cmd.CommandTimeout = value;
                }
            }
        }
    }    
}
