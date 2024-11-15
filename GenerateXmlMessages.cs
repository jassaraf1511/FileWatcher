using System;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using CFSB.DataAccess.DataEntities;
using CFSB.LoggerWriter.Service;


namespace CFSB.FileProcessing.Services
{
    public class GenerateXmlMessages
    {
        public static DataTable XmlMessageTable;
        private static GenerateXmlMessages instance;
        static object lockObject = new object();
        static LogWriter loggerInstance;
        static string dbConnString;
        static int messageId = 0;
        private static StringBuilder dataFileXmlMessage;

        public static GenerateXmlMessages Instance
        {
            get
            {
                // If the instance is null then create one and init the Queue
                if (instance == null)
                {
                    instance = new GenerateXmlMessages();
                    loggerInstance = LogWriter.Instance;
                    loggerInstance.WriteToLog(AppConfig.InterfaceName);
                    dbConnString = AppConfig.SQLInterfaceConnectionString;
                    dataFileXmlMessage = new StringBuilder();

                }
                return instance;
            }
        }

        public void CreateXmlTable(DataTable sourceDataTable, string tableName)
        {
            createXmlTargetTable(sourceDataTable, tableName);
            
        }
        public PaymentMessages GenerateMessages(DataRow sourceDataRow)
        {
           // loggerInstance.WriteToLog("Generate XML");
            try
            {
                lock (lockObject)
                {
                    try
                    {


                        DataRow xmlDataRow = importDataRowToTarget(sourceDataRow);


                        string transactionName = xmlDataRow["TransactionIdentifier"].ToString();
                        messageId++;

                        string xmlMessageFile = getXmlMessageFileName(transactionName);
                        string xmlMessage = generateXml();
                        saveXmlMessage(xmlMessageFile, xmlMessage);
                        PaymentMessages paymenteassge = deserializeXml(xmlMessageFile);
                        paymenteassge.XMLDataRecord = xmlMessage;
                        //   paymenteassge.InterfaceID = "037";
                        return paymenteassge;
                    }
                    catch (Exception exMessage)
                    {
                        loggerInstance.WriteToLog(exMessage);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog(ex);
                return null;
            }

        }

        private static DataRow importDataRowToTarget(DataRow sourceDataRow)
        {
            DataRow importDataRow;
            importDataRow = sourceDataRow;
            XmlMessageTable.Clear();
            XmlMessageTable.NewRow();
            XmlMessageTable.ImportRow(importDataRow);
            return importDataRow;
        }

        private static string getXmlMessageFileName(string transactionName)
        {
            //InterfaceID
            //return AppConfig.MessageDirectory + "MCHE037_" + transactionName +"_" +  DateTime.Now.ToString("yyyyMMdd") + messageId.ToString("00000000") + ".xml";
            return AppConfig.MessageDirectory + AppConfig.InterfaceId + "_" + transactionName + "_" + messageId.ToString("00000") + ".xml";
        }
        private static void createXmlTargetTable(DataTable sourceDataTable, string tableName)
        {
            lock (lockObject)
            {
                if (XmlMessageTable == null)
                {
                    XmlMessageTable = new DataTable();
                    XmlMessageTable = sourceDataTable.Clone();
                    XmlMessageTable.TableName = tableName;
                }
            }
        }
        private static string generateXml()
        {

            System.IO.StringWriter writer = new System.IO.StringWriter();
            XmlMessageTable.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);

            StringBuilder xmlMessage = new StringBuilder(writer.ToString());

            xmlMessage.Replace("<DocumentElement>", "");
            xmlMessage.Replace("</DocumentElement>", "");
            return xmlMessage.ToString().Trim(); ;
        }
        private static void saveXmlMessage(string xmlMesageFile, string xmlMessage)
        {
            try
            {
                dataFileXmlMessage.AppendLine(xmlMessage);
                using (TextWriter textWriter = File.CreateText(xmlMesageFile))
                {

                    //
                    textWriter.Write(xmlMessage);
                }
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog("error on file " + xmlMesageFile);
                loggerInstance.WriteToLog(ex);
            }
        }

        private static PaymentMessages deserializeXml(string xmlMessageFile)
        {
            try
            {
                FileStream file = new FileStream(xmlMessageFile, FileMode.Open);
                PaymentMessages paymentObject = new PaymentMessages();
                XmlSerializer serializer = new XmlSerializer(typeof(PaymentMessages));
                paymentObject = (PaymentMessages)serializer.Deserialize(file);

                file.Close();
                return paymentObject;
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog("error on file " + xmlMessageFile);
                loggerInstance.WriteToLog(ex);
                return null;
            }

        }
        private int addPayment(PaymentMessages paymentObject)
        {
            int recCount = 0;


            return recCount;
        }
        public string DataFileXmlMessage
        {
            get
            {
                return dataFileXmlMessage.ToString();
            }
        }
    }
}
