using System;
using System.Data;
using System.Data.SqlClient;
using activiser.WebService.CrmOutputGateway.CrmSdk;
using activiser.WebService.Gateway.Crm.DataAccessLayer;
using activiser.WebService.Gateway.Crm.DataAccessLayer.CrmGatewayConfigTableAdapters;
using System.Diagnostics;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway : activiser.WebService.OutputGateway.IOutputGateway
    {
        private const string STR_Null = "<Null>";
        private const string STR_CrmCreatedOn = "createdon";
        private const string STR_CrmModifiedOn = "modifiedon";
        private const string STR_Statecode = "statecode";
        private const string STR_Statuscode = "statuscode";
        private const string STR_Status = "status";
        private const string STR_String = "string";

        private string _connectionString;
        private SqlConnection _connection;
        private CrmGatewayConfig _gatewayConfig = new CrmGatewayConfig();
        private CrmGatewayConfig.EntityMapDataTable _em;
        private CrmGatewayConfig.StatusMapDataTable _sm;
        private SettingTableAdapter _settingTA = new SettingTableAdapter();
        private EntityMapTableAdapter _entityTA = new EntityMapTableAdapter();
        private AttributeMapTableAdapter _attributeTA = new AttributeMapTableAdapter();
        private StatusMapTableAdapter _statusTA = new StatusMapTableAdapter();

        public Gateway()
        {
            _em = _gatewayConfig.EntityMap;
            _sm = _gatewayConfig.StatusMap;
        }

        private static Guid _gatewayId = System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID;
        public Guid GatewayId
        {
            get { return _gatewayId; }
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
                this.Connection = new SqlConnection(value);
            }
        }

        public string Test()
        {
            string result = Properties.Resources.DefaultTestMessage;
            try
            {
                result = string.Format(Properties.Resources.ParametersMessageFormat, OrganizationName, CrmServiceLocation);
                CrmService crm = GetCrmService(CrmServiceLocation, OrganizationName);

                WhoAmIRequest whoRequest = new WhoAmIRequest();
                WhoAmIResponse whoResponse = (WhoAmIResponse)crm.Execute(whoRequest);
                if (whoResponse != null && whoResponse.UserId != Guid.Empty)
                {
                    result += string.Format(Properties.Resources.WhoAmIResponseFormat, whoResponse.OrganizationId, whoResponse.UserId);
                }
                LogSuccess(EventId.GatewayTestSuccessAudit, Properties.Resources.TestSucceeded, whoResponse.OrganizationId, whoResponse.UserId);
            }
            catch (GetCrmServiceException ex)
            {
                result += Properties.Resources.UnableToConnectToService;
                LogErrorMessage(EventId.FailedToGetCrmServiceInstance, Properties.Resources.ErrorGettingCrmServiceInstance, ex, OrganizationName);
            }
            catch (Exception ex)
            {
                result += string.Format(Properties.Resources.ErrorTestingCRMConnection, ex.ToString());
            }
            return string.Format("<CrmGatewayTestResult>{0}</CrmGatewayTestResult>", result);
        }

        private SqlConnection Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
                this._connectionString = (value == null) ? null : value.ConnectionString;
                _gatewayConfig.Clear();

                _settingTA.Connection = this._connection;
                _entityTA.Connection = this._connection;
                _attributeTA.Connection = this._connection;
                _statusTA.Connection = this._connection;

                if (value == null) return;

                _settingTA.Fill(_gatewayConfig.Setting);
                _entityTA.Fill(_gatewayConfig.EntityMap);
                _attributeTA.Fill(_gatewayConfig.AttributeMap);
                _statusTA.Fill(_gatewayConfig.StatusMap);
            }
        }

        private string getSetting(string name)
        {
            try
            {
                return _gatewayConfig.Setting.FindBySettingName(name).Value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        public string CrmServiceLocation
        {
            get { return _gatewayConfig.Setting.FindBySettingName(Properties.Resources.ConfigCrmServiceLocationKey).Value; }
        }

        public string OrganizationName
        {
            get { return _gatewayConfig.Setting.FindBySettingName(Properties.Resources.ConfigOrganizationNameKey).Value; }
        }

    }
}
