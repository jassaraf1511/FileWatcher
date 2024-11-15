using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using CFSB.LoggerWriter.Service;
using CFSB.FileProcessing.DataEntities;
using CFSB.DataAccess.DataEntities;
using CFSB.DataAccess.Models;
using CRB.DataFileManagement.Services;
using System.Configuration;
using System.Diagnostics;



namespace CFSB.FileProcessing.Services
{
    public class FirstDataFileParsing
    {
        private LogWriter loggerInstance;
        private string xmlDataDictionary;
        private string dataFileName;
        private int processedRecord;
        private int successRecord;

        private DataSet FirstDataPayments;
        private DataTable FirstDataPaymentMessages;
        private IEnumerable<XsdDataDictionaryProperty> dataDictionaryList;
        private static GenerateXmlMessages messageInstance;
        private DataDictionaryProperty dataDictionary;
        private string processingDirectory;
        private string dataDictionaryName;
        DataDictionaryRepository dbEvents;



        private static string dbConnString;

        public FirstDataFileParsing(string dataFileName)
        {

            this.loggerInstance = LogWriter.Instance;
            messageInstance = GenerateXmlMessages.Instance;

            this.dataDictionaryName = AppConfig.DataDictionaryName;
            dbConnString = AppConfig.SQLInterfaceConnectionString;
            this.dataFileName = dataFileName;

            //  dataDictionary=getXmlDataDictionary();
            startProcessing();
        }
        public FirstDataFileParsing(string xmlDataDictionary, string dataFileName)
        {

            this.loggerInstance = LogWriter.Instance;
            messageInstance = GenerateXmlMessages.Instance;

            this.dataDictionaryName = AppConfig.DataDictionaryName;
            dbConnString = AppConfig.SQLInterfaceConnectionString;
            this.dataFileName = dataFileName;
            this.xmlDataDictionary = xmlDataDictionary;
            this.dataFileName = dataFileName;
            startProcessing();
        }

        private void startProcessing()
        {   /// Move File To Processing Directory

            processingDirectory = ConfigurationManager.AppSettings["AppFileProcessingDirectory"];
            if (!DataFileManager.MoveFile(dataFileName, processingDirectory))
            {
                loggerInstance.WriteToLog(String.Format("Error Moving File {0} to Processing Directory {1} ", dataFileName, processingDirectory));
                return;
            }


            dataFileName = processingDirectory + @"\" + Path.GetFileName(dataFileName);
            dbEvents = new DataDictionaryRepository(dbConnString);
            loadDataDictionary();
            createDataSetTable();
            processFile();      

        }
        private void loadDataDictionary()
        {
            bool loadFromFile = false;

            string xmlData;
            try
            {
                if (this.xmlDataDictionary == null)
                {
                    dataDictionary = getXmlDataDictionary();
                    xmlData = dataDictionary.XMLDataDictionary;
                }
                else
                {
                    loadFromFile = true;
                    xmlData = this.xmlDataDictionary;
                }
                LoadDataDictionary ldatDict = new LoadDataDictionary(xmlData, loadFromFile);

            }
            catch(Exception ex)
            {
                loggerInstance.WriteToLog(this.xmlDataDictionary);
                loggerInstance.WriteToLog(ex);

            }
        }

        private void createDataSetTable()
        {

            FirstDataPayments = new DataSet("Payments");
            FirstDataPaymentMessages = new DataTable("PaymentMessages");


            dataDictionaryList = LoadDataDictionary.DataDictionaryList.Values;

            foreach (KeyValuePair<string, XsdDataDictionaryProperty> entry in LoadDataDictionary.DataDictionaryList)
            {
                string entryKey = entry.Key;


                XsdDataDictionaryProperty dataProperty = entry.Value;
                if (dataProperty.DataType != null)
                {

                    Type typeObj = TypeExtensions.GetTypeFromName(dataProperty.DataType);
                    try
                    {
                        FirstDataPaymentMessages.Columns.Add(new DataColumn(dataProperty.SQLTargetName, typeObj));
                    }
                    catch(Exception ex)
                    {
                        loggerInstance.WriteToLog(ex);
                    }
                }

            }

            FirstDataPaymentMessages.Columns.Add(new DataColumn("DataRecord", typeof(String)));
            FirstDataPaymentMessages.Columns.Add(new DataColumn("TransactionIdentifier", typeof(String)));
            FirstDataPaymentMessages.RowChanged += new DataRowChangeEventHandler(Row_Changed);
            messageInstance.CreateXmlTable(FirstDataPaymentMessages, "PaymentMessages");


        }
        private int processFile()
        {
            int recordCount = 0;
            try
            {

                using (StreamReader reader = new StreamReader(dataFileName))
                {

                    while (reader.Peek() >= 0)
                    {
                        string line = reader.ReadLine();
                        if (line.StartsWith("D"))
                        {
                            parseRecord(line);
                            processedRecord++;
                            recordCount++;

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog(dataFileName);
                loggerInstance.WriteToLog(ex);

            }

            if (recordCount > 0) archiveDataFile();
            return recordCount;
        }


        private int parseRecord(string record)
        {
            int columnCount = 0;
            int recordLength = record.Length;

            bool toInclude = false;
            int transactionCode = int.Parse(record.Substring(43, 3));
            string recordType = record.Substring(0, 2);

            dataDictionaryList = LoadDataDictionary.DataDictionaryList.Values;
  
            DataRow dataRow = FirstDataPaymentMessages.NewRow();

            foreach (XsdDataDictionaryProperty dataProperty in dataDictionaryList)
            {
                int startPOsition = dataProperty.TrgStart - 1;
                int length = dataProperty.TrgLen;
                int endPosition = dataProperty.TrgCalcEnd;
                string parsedValue = "   ";
                

                DataRuleProperties dataRule = dataProperty.RuleProperties;
                toInclude = isMessageAttributeVaild(dataProperty.TargetName, dataRule, transactionCode);
                             
                if (toInclude && endPosition <= recordLength)
                {
                    columnCount++;

                    Type targetType = TypeExtensions.GetTypeFromName(dataProperty.DataType);

                    parsedValue = record.Substring(startPOsition, length);

                    string errorMessage;
                    object result = DataTransformationRule.DatatypeConversion(parsedValue, dataProperty.SrcType, targetType, dataProperty.TransalationRule, out errorMessage);

                
                    if (result != null)
                        dataRow[dataProperty.TargetName] = result;

                }


            }
            ///
            /// Temporary excluding DP record
            ///
            if (columnCount > 0 && recordType != "DP")
            {
                string transactionName = dataRow["RecordType"].ToString().Trim() + "_"
                                          + dataRow["TransactionCode"].ToString().Trim() + "_"
                                          + dataRow["EntryRefNumberLocator"].ToString().Trim();


                dataRow["TransactionIdentifier"] = transactionName;
                dataRow["DataRecord"] = record;
                successRecord++;
                FirstDataPaymentMessages.Rows.Add(dataRow);
            }
            return columnCount;
        }

        /// <summary>
        /// Fire Event to generate the meassage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            PaymentMessages paymentMessage = messageInstance.GenerateMessages(e.Row);

            PaymentMessagesRepository dbPaymentEvents = new PaymentMessagesRepository(dbConnString);
       
            int rec = dbPaymentEvents.AddPaymentMessages(paymentMessage);
        }

        private DataDictionaryProperty getXmlDataDictionary()
        {
            DataDictionaryProperty dataDictionary = new DataDictionaryProperty();

            try
            {
                dataDictionary = dbEvents.GetDataDictionaryByName(dataDictionaryName);
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog(ex);
            }
            return dataDictionary;
        }

        /// <summary>
        /// Check Attribute inclusion and Exclusion rule
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        private bool isMessageAttributeVaild(string attributeName, DataRuleProperties rules, int pTransactionCode)
        {
            bool inclusionExclusion = false;
            if (rules.isApplyForAll) return true;
            if (!rules.IsToExclude || !rules.IsToInclude) return false;

            if (rules.IncludeListValue == null) return false;

            foreach (string transactionCode in rules.IncludeListValue)
            {
                  
                    if (transactionCode == pTransactionCode.ToString())
                    {
                     inclusionExclusion = true;
                    }
                }
           

        
            return inclusionExclusion;
        }

        private void archiveDataFile()
        {
            DataFileCompression compressFile = new DataFileCompression();
            string archiveDirectory =  ConfigurationManager.AppSettings["AppArchivingDirectory"]; ;
            string xmlFileData = messageInstance.DataFileXmlMessage;
            string xmlFile=saveXmlData(xmlFileData);
            string xmlZipFileName = compressFile.compressFile(xmlFile, archiveDirectory);            
            string dataZipFileName = compressFile.compressFile(dataFileName, archiveDirectory);
            insertDataFileRecord(xmlZipFileName, dataZipFileName);


        }
        private  string saveXmlData(string xmlMessage)
        {
            string xmlMesageFile= processingDirectory + @"\"+Path.GetFileNameWithoutExtension(dataFileName) + "XML.xml";
            try
            {
                using (TextWriter textWriter = File.CreateText(xmlMesageFile))
                {

                    //
                    textWriter.Write(xmlMessage);
                }
                return xmlMesageFile;
            }
            catch (Exception ex)
            {
                
                loggerInstance.WriteToLog("error on file " + xmlMesageFile);
                loggerInstance.WriteToLog(ex);
                return null;
            }
        }
        private void insertDataFileRecord(string xmlZipFile, string dataZipFile)
        {
            PaymentDataFilesProperty paymentDataFiles = new PaymentDataFilesProperty();
            paymentDataFiles.InterfaceIdentifier = "037";
            paymentDataFiles.InterfaceName = "FFirs Data File 037 Merchant Payment";
            paymentDataFiles.InterfaceBusinessDate = DateTime.Now;
            paymentDataFiles.InterfaceDescription = "";
            paymentDataFiles.InterfaceType = "Payment";
            paymentDataFiles.LoadProcessId = Process.GetCurrentProcess().Id.ToString(); ;
            paymentDataFiles.LoadProcessName = Process.GetCurrentProcess().ProcessName;
            paymentDataFiles.FileId = Path.GetFileName(dataFileName);
            paymentDataFiles.FileDownloadProcessId = "";
            paymentDataFiles.FileDownloadProcessName ="";
            paymentDataFiles.FileOrigin = "";
            paymentDataFiles.FileDestination = ConfigurationManager.AppSettings["AppFileProcessingDirectory"]; 
            paymentDataFiles.FileArchived = ConfigurationManager.AppSettings["AppArchivingDirectory"]; 
            paymentDataFiles.LogFileName = "";
            paymentDataFiles.FileReceivedTime = null;
            paymentDataFiles.FileDownloadStartTime = null;
            paymentDataFiles.FileDownloadEndTime = null;
            paymentDataFiles.FileProcessingStartTime = null;
            paymentDataFiles.FileProcessingEndTime =null;
            paymentDataFiles.RecordsNumber = processedRecord;
            paymentDataFiles.RecordProcessed = successRecord;
            paymentDataFiles.RecordRejected = 0;
            paymentDataFiles.FileSize = 037;
            paymentDataFiles.RejectReasonCode = null;
            paymentDataFiles.RejectReasonDescription = null;
            paymentDataFiles.LoadStatus = "SUCCESS";
            paymentDataFiles.UserId = Environment.UserName;
         //   paymentDataFiles.XMLDataDictionary = null;
         //   paymentDataFiles.XLSDataDictionary = null;
            paymentDataFiles.XmlFileContent =  loadZipFiles(xmlZipFile);
            paymentDataFiles.DataFileContent = loadZipFiles(dataZipFile);
            paymentDataFiles.CreationDate = DateTime.Now;
            paymentDataFiles.CreatorId = Environment.UserName;

            PaymentDataFileRepository dbPaymentDataFileEvents = new PaymentDataFileRepository(dbConnString);

            int rec = dbPaymentDataFileEvents.AddPaymentDataFile(paymentDataFiles);
        }

        private  Byte [] loadZipFiles(string zipFileName)
        {
            
            FileStream fsSTream = new FileStream(zipFileName, FileMode.Open, FileAccess.Read);
            Byte[] fileContent = new Byte[1];
            BinaryReader binReader = new BinaryReader(fsSTream);
            try
            {

                fileContent = binReader.ReadBytes((int)fsSTream.Length);
               

            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog(ex);
                loggerInstance.WriteToLog("failed to laod file " + zipFileName);

            }
            finally
            {
                try
                {
                    fsSTream.Close();
                }
                catch { }
            }

            
            if (fileContent.Length < 10) fileContent = new byte[1];

            return fileContent;

        }
    }
}
