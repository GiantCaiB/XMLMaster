using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

namespace XMLMaster
{
    class DBHandler
    {
        DI_Core.DatabaseManager.clsDatabaseManager DBManager { get; set; }
        public DBHandler()
        {
            DBManager = new DI_Core.DatabaseManager.clsDatabaseManager(new DI_Core.Logger.clsLogger(false, DI_Core.Logger.clsLogger.GetNewLogFilename()), false);
            DBManager.DatabaseBrand = "SQLServer";
            DBManager.ConnectionString = $@"Server={Config.Config.SERVER_NAME};Database={Config.Config.DB_NAME};User Id={Config.Config.USR_NAME};Password={Config.Config.PASSWORD};";
            DBManager.TimeOut = 15;
        }
        public string WithdrawXMLList(Application application)
        {
            Dictionary<string, object> applicationInfo = new Dictionary<string, object>()
            {
                { "id_company", Config.Config.COMPANY_ID },
                { "id_request", application.ApplicationID  },
                { "no_sequence", application.SequenceNumber}
            };
            DataSet dtSubmission = DBManager.SelectData(new DI_Core.DatabaseManager.SqlQueryWithParameters($"SELECT tx_xml_request FROM {Config.Config.SCHEMA}.t_req_xml (nolock) xml where xml.id_company = @id_company and xml.id_request = @id_request and xml.no_seq_request = @no_sequence", true, applicationInfo));
            return DI_Core.clsCompression.sfnDecompress(dtSubmission.Tables[0].Rows[0][0].ToString());
        }
        public void UpdateDB(XmlDocument xml)
        {
            string applicationID = xml.SelectSingleNode(Config.Xpath.APP_ID).InnerXml;
            string sequenceNumber = xml.SelectSingleNode(Config.Xpath.SEQ_NO).InnerXml;
            var saveString = DI_Core.clsCompression.sfnCompress(xml.OuterXml);
            if (applicationID != null && sequenceNumber!=null)
            {
                DBManager.UpdateData(new DI_Core.DatabaseManager.SqlQueryWithParameters($"UPDATE {Config.Config.SCHEMA}.t_req_xml SET tx_xml_request = @tx_xml WHERE id_company = @id_company AND id_request = @id_request and no_seq_request = @no_sequence ", true, new Dictionary<string, object>()
                {
                        { "id_company", Config.Config.COMPANY_ID },
                        { "id_request",  xml.SelectSingleNode(Config.Xpath.APP_ID).InnerXml},
                        { "no_sequence", xml.SelectSingleNode(Config.Xpath.SEQ_NO).InnerXml},
                        { "tx_xml", saveString }
                }));
            }
        }
    }
}
