using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Cfsb.Incoming.FedWires.Utils;
using Cfsb.LoggerWriter.Services;
using Cfsb.Incoming.FedWires.DataEntities;
using Cfsb.Incoming.FedWires.MessageStructure;
using System.Linq;

namespace Cfsb.Incoming.FedWires.Services
{
    public class FedWireFileProcessor
    {
        private string fileToParse;
        private int recordProcess;
        private int recordRead;
        private int recordReject;
        private LogWriter log;
        private InterfaceDetail appSettings;
        private string dbFedWireConStr;
        private string incomingOutgoing;
        private string filePathOrigin;

        public FedWireFileProcessor()
        {

        }

        public string FileToParse { get => fileToParse; set => fileToParse = value; }
         public int RecordProcess { get => recordProcess; set => recordProcess = value; }
        public int RecordRead { get => recordRead; set => recordRead = value; }
        public int RecordReject { get => recordReject; set => recordReject = value; }
        public LogWriter Log { get => log; set => log = value; }
        public InterfaceDetail AppSettings { get => appSettings; set => appSettings = value; }
        public string DbFedWireConStr { get => dbFedWireConStr; set => dbFedWireConStr = value; }
        public string IncomingOutgoing { get => incomingOutgoing; set => incomingOutgoing = value; }
        public string FilePathOrigin { get => filePathOrigin; set => filePathOrigin = value; }

        private DateTime fileReceiveDate;

        public int processFile()
        {
            string inputFile = null;
           
            inputFile = fileToParse;
            recordRead = 0;
            RecordProcess = 0;
            recordReject = 0;

            string fileId = DateTime.Now.Ticks.ToString("x");
            string wireDataFile = (!string.IsNullOrEmpty(inputFile)) ? inputFile : this.fileToParse;

            bool continueLoop = true;
            string filePath = Path.GetFullPath(inputFile);
            string fileName = Path.GetFileName(inputFile);
            incomingOutgoing = fileName.ToLower().Contains("incoming") ? "WIREIN" : "WIREOUT";

            fileReceiveDate = File.GetCreationTime(inputFile);

            ORM.DBConnect dbConnect = new ORM.DBConnect(log, appSettings, dbFedWireConStr);
            ProcessResults results = new ProcessResults(Log, appSettings, wireDataFile,incomingOutgoing,fileReceiveDate);
            ORM.DbInsertMessageRepo  storDbMessage= new ORM.DbInsertMessageRepo(Log, ORM.DBConnect.Connection);
           
            int count=0;


            try
            {
                
                using (var reader = new StreamReader(wireDataFile))
                {
                    while (!reader.EndOfStream && continueLoop)
                    {
                        recordRead++;
                        try
                        {
                            string record = reader.ReadLine();

                            Dictionary<string, WireTagData> dataList = new Dictionary<string, WireTagData>();

                            if (!string.IsNullOrEmpty(record) && record.Contains("{1520}"))
                            {
                                ParseFedWireMessage parseMessage = new ParseFedWireMessage(Log);

                                
                                dataList = parseMessage.ParseMessage(record);
                                if (!string.IsNullOrEmpty(parseMessage.FedMessage.Imad) )                                 
                                {
                                    incomingOutgoing = parseMessage.MessageInOut;
                                    parseMessage.FedMessage.IncomingOutGoing = incomingOutgoing;
                                    parseMessage.FedMessage.MessageOrigin = "FILE";
                                    parseMessage.FedMessage.OriginName = fileName;
                                    parseMessage.FedMessage.MessageFilePath = filePath;
                                    parseMessage.FedMessage.MessageId = fileId + string.Format("{0:00000000}", recordRead);
                                    parseMessage.FedMessage.FileId = fileId;
                                    parseMessage.FedMessage.MessageFilePath = filePathOrigin;

                                    results.storeMessage(parseMessage.FedMessage);

                                    try {
                                        
                                        count = storDbMessage.StoreFedMessageRepo(parseMessage.FedMessage, recordRead);
                                        if (storDbMessage.MessageRepoInserted)
                                        {
                                            results.WriteResults();
                                            recordProcess++;

                                        }
                                        else
                                        {
                                            recordReject++;
                                        }
                                    }
                                    catch(Exception stex)
                                    {
                                        log.WriteToLog(stex.Message);
                                        log.WriteToLog(stex.StackTrace);
                                        recordReject++;
                                    }
                                    
                                    
                                }
                            }
                           
                            
                            if (!string.IsNullOrEmpty(record) && !record.Contains("{1520}"))
                            {
                                continueLoop = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            recordReject++;
                            log.WriteToLog("Error On File " + wireDataFile + " At REcord Number " + recordRead++.ToString());
                            log.WriteToLog(ex.Message);
                            log.WriteToLog(ex.StackTrace);
                            log.WriteToLog(ex.Data.ToString());
                        }

                    }
                    reader.Close();
                }

            }
            catch (Exception exFile)
            {
                log.WriteToLog(exFile.Message);
                log.WriteToLog(exFile.StackTrace);
            }
            finally
            {
                results.closeFile();
            }

            try
            {
                insertLoadHistory(wireDataFile, storDbMessage);
                dbConnect.CloseConnection();
            }
            catch { }


           
            return recordProcess;
        }


        private int insertLoadHistory(string wireDataFile, ORM.DbInsertMessageRepo storDbMessage)
        {
            int count;
            count = storDbMessage.InsertLoadMessageFile(wireDataFile, RecordRead, RecordProcess, RecordReject);
        
            return count;
        }
    }
}
