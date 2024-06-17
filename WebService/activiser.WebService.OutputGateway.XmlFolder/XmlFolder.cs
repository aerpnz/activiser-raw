using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

// 0a8270a0-a19a-4ee1-9c54-e27b90c4331f

namespace activiser.WebService.XmlFolderOutputGateway
{
    public partial class Gateway : activiser.WebService.OutputGateway.IOutputGateway
    {
        private const string STR_Null = "<Null>";

        private string _connectionString;

        public Gateway()
        {

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
                //this.Connection = new SqlConnection(value);
            }
        }

        private string OutputFolder()
        {
            return getSetting("OutputFolder");
        }

        public string Test()
        {
            StringBuilder result = new StringBuilder(1000); // Properties.Resources.DefaultTestMessage;
            result.Append("<XmlTestResult>");
            try
            {
                string outputFolder = OutputFolder();
                result.AppendFormat("<OutputFolder>{0}</OutputFolder>", outputFolder);
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(outputFolder);
                if (! di.Exists)
                {
                    result.Append("<Notes>Folder does not exist or is not accessible.</Notes>");
                }
                else {
                    string testFileName =
                        System.IO.Path.Combine(outputFolder, string.Format("{0}-{1}-Test.txt", GatewayId, DateTime.UtcNow.ToString("yyyyMMsshhmmssff")));
                    System.IO.StreamWriter tester = new System.IO.StreamWriter(testFileName,false, Encoding.UTF8);
                    try
                    {
                        tester.Write(string.Format(Properties.Resources.TestTemplate, DateTime.UtcNow));
                        result.AppendFormat("<Notes>Test file created: {0}</Notes>", testFileName);
                    }
                    catch (Exception ex)
                    {
                        result.AppendFormat("<Notes>Unable to create test file: {0}</Notes>", testFileName);
                        result.AppendFormat("<ExceptionString>{0}</ExceptionString>", ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result.AppendFormat("<GeneralException>{0}</GeneralException>", ex.ToString());
            }
            
            result.Append("</XmlTestResult>");
            return result.ToString();
        }

        private string getSetting(string name)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Properties.Settings.Default.SettingQuery, ConnectionString);
                da.SelectCommand.Parameters.AddWithValue(Properties.Settings.Default.SettingNameParameter, name);

                DataSet resultSet = new DataSet("Gateway");
                resultSet.Tables.Add("Settings");

                if (da.Fill(resultSet, "Settings") != 0)
                {
                    return resultSet.Tables[0].Rows[0].ItemArray[1].ToString();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
//                return ex.ToString();
            }
        }

        public int Save(Guid transactionId, DataSet transactionData, out Exception saveException)
        {
            saveException = null;

            string outputFolder = OutputFolder();

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(outputFolder);
            if (!di.Exists)
            {
                saveException = new System.IO.DirectoryNotFoundException(string.Format(Properties.Resources.DirectoryNotFoundMessage, outputFolder));
                return -1;
            }
            else
            {
                string txFileName = string.Format(Properties.Settings.Default.FileNameFormat, GatewayId, transactionId, DateTime.UtcNow.ToString("yyyyMMsshhmmssff"));
                string txFilePath = System.IO.Path.Combine(outputFolder, txFileName);
                try
                {
                    transactionData.WriteXml(txFilePath, XmlWriteMode.IgnoreSchema);
                }
                catch (Exception ex)
                {
                    saveException = new TransactionSaveException(string.Format(Properties.Resources.UnableToCreateTransactionFile, txFilePath), ex);
                    return -2;
                }
            }

            return 0;
        }
    }
}
