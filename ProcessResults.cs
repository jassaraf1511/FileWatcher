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
    public class ProcessResults
    {
        private string outputResultDataFile;

        private LogWriter log;
        private InterfaceDetail appSettings;
        private string csvHeader;
        private StreamWriter outPutFileResult;
        private FedWireMessage fedMessage;
        private string wireDataFile;
        private string inOutFile;
        private DateTime fileReceiveDate;

        OutputResultMessage resultMessage;
        public ProcessResults(LogWriter log, InterfaceDetail appSettings, string wireDataFile, string inOutFile, DateTime fileReceiveDate)
        {
            this.log = log;
            this.appSettings = appSettings;

            this.wireDataFile = wireDataFile;
            this.inOutFile = inOutFile;
            this.fileReceiveDate= fileReceiveDate;
            initialize();

        }

        public string OutputResultDataFile { get => outputResultDataFile; set => outputResultDataFile = value; }
        public LogWriter Log { get => log; set => log = value; }
        public InterfaceDetail AppSettings { get => appSettings; set => appSettings = value; }
        public string WireDataFile { get => wireDataFile; set => wireDataFile = value; }
        public string InOutFile { get => inOutFile; set => inOutFile = value; }
        public DateTime FileReceiveDate { get => fileReceiveDate; set => fileReceiveDate = value; }

        public int storeMessage(FedWireMessage fedMessage)
        {
            this.fedMessage = fedMessage;
            resultMessage = new OutputResultMessage();
            try
            {


                resultMessage.OMAD = !string.IsNullOrEmpty(fedMessage.Omad) ? fedMessage.Omad : "";
                resultMessage.IMAD = !string.IsNullOrEmpty(fedMessage.Imad) ? fedMessage.Imad : "";
                resultMessage.MessageDuplication = !string.IsNullOrEmpty(fedMessage.MessageDisposition.MessageDuplicationCode) ? fedMessage.MessageDisposition.MessageDuplicationCode : "";
                resultMessage.MessageStatus = !string.IsNullOrEmpty(fedMessage.MessageDisposition.MessageStatusStr) ? fedMessage.MessageDisposition.MessageDuplicationCode : "";
                resultMessage.InputCycleDate = !string.IsNullOrEmpty(fedMessage.InputMessageAccountabilityData.InputCycleDate_CCYYMMDD) ? fedMessage.InputMessageAccountabilityData.InputCycleDate_CCYYMMDD : "";

                resultMessage.OutputCycleDateDateReceived = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.InputCycleDate_CCYYMMDD) ? fedMessage.OutputMessageAccountabilityData.InputCycleDate_CCYYMMDD : "";

                resultMessage.WIREINOUT = fedMessage.IncomingOutGoing;
                resultMessage.TypeSubtype = !string.IsNullOrEmpty(fedMessage.TypeSubType.MessageTypeSubType) ? fedMessage.TypeSubType.MessageTypeSubType : "";

                resultMessage.TypeCode = !string.IsNullOrEmpty(fedMessage.TypeSubType.TransactionTypeName) ?  fedMessage.TypeSubType.TransactionTypeName : "";
                resultMessage.TypeSubCode = !string.IsNullOrEmpty(fedMessage.TypeSubType.TransactionSubTypeName) ? fedMessage.TypeSubType.TransactionSubTypeName : "";

                resultMessage.SenderABA = !string.IsNullOrEmpty(fedMessage.SenderDI.SenderReceiverABA) ? fedMessage.SenderDI.SenderReceiverABA : "";
                resultMessage.SenderName = !string.IsNullOrEmpty(fedMessage.SenderDI.SenderReceiverShortName) ? fedMessage.SenderDI.SenderReceiverShortName : "";
                resultMessage.SenderReference = !string.IsNullOrEmpty(fedMessage.SenderReference) ? fedMessage.SenderReference : "";
                resultMessage.ReceiverABA = !string.IsNullOrEmpty(fedMessage.ReceiverDI.SenderReceiverABA) ? fedMessage.ReceiverDI.SenderReceiverABA : "";
                resultMessage.ReceiverName = !string.IsNullOrEmpty(fedMessage.ReceiverDI.SenderReceiverShortName) ? fedMessage.ReceiverDI.SenderReceiverShortName : "";
                resultMessage.BusinessFunction = !string.IsNullOrEmpty(fedMessage.BusinessFunctionCode) ? fedMessage.BusinessFunctionCode : "";
                resultMessage.TransactionCode = "";
                resultMessage.PaymentNotificationContactName = fedMessage.Paymentnotification != null && !string.IsNullOrEmpty(fedMessage.Paymentnotification.ContactName) ? fedMessage.Paymentnotification.ContactName : "";
                resultMessage.TransactionAmount = String.Format("{0:###,###,###,###.00}", fedMessage.Amount.TransactionAmount);
                resultMessage.InstructedAmountCurrency = fedMessage.InstructedAmount != null && !string.IsNullOrEmpty(fedMessage.InstructedAmount.Currency) ? fedMessage.InstructedAmount.Currency : ""; ;
                resultMessage.InstructedAmount = fedMessage.InstructedAmount != null && fedMessage.InstructedAmount != null ? String.Format("{0:###,###,###,###.00}", fedMessage.InstructedAmount.Amount) : "";
                resultMessage.IntemediaryFIIdentifier = !string.IsNullOrEmpty(fedMessage.IntermediaryFI.AccountIdentifier) ? fedMessage.IntermediaryFI.AccountIdentifier : "";
                resultMessage.IntermediaryFIName = !string.IsNullOrEmpty(fedMessage.IntermediaryFI.AccountName) ? fedMessage.IntermediaryFI.AccountName : "";
                resultMessage.BeneficiaryFIIdentifierAccount = !string.IsNullOrEmpty(fedMessage.BeneficiaryFI.AccountIdentifier) ? fedMessage.BeneficiaryFI.AccountIdentifier : "";
                resultMessage.BeneficiaryFIName = !string.IsNullOrEmpty(fedMessage.BeneficiaryFI.AccountName) ? fedMessage.BeneficiaryFI.AccountName : "";
                resultMessage.BeneficiaryIdentifierAccount = !string.IsNullOrEmpty(fedMessage.Beneficiary.AccountIdentifier) ? fedMessage.Beneficiary.AccountIdentifier : "";
                resultMessage.BeneficiaryName = !string.IsNullOrEmpty(fedMessage.Beneficiary.AccountName) ? fedMessage.Beneficiary.AccountName : "";
                resultMessage.AccountDebitInDrawdownIdentifier = !string.IsNullOrEmpty(fedMessage.AccountDebitedinDrawdown.AccountIdentifier) ? fedMessage.AccountDebitedinDrawdown.AccountIdentifier : "";
                resultMessage.AccountDebitInDrawdownName = !string.IsNullOrEmpty(fedMessage.AccountDebitedinDrawdown.AccountName) ? fedMessage.AccountDebitedinDrawdown.AccountName : "";
                resultMessage.OriginatorIdentifier = !string.IsNullOrEmpty(fedMessage.Originator.AccountIdentifier) ? fedMessage.Originator.AccountIdentifier : "";
                resultMessage.OriginatorName = !string.IsNullOrEmpty(fedMessage.Originator.AccountName) ? fedMessage.Originator.AccountName : "";
                resultMessage.OriginatorOptionFPartyIdentifier = !string.IsNullOrEmpty(fedMessage.OriginatorOptionF.PartyUniqueIdentifier) ? fedMessage.OriginatorOptionF.PartyUniqueIdentifier : "";
                resultMessage.OriginatorOptionFPartyName = !string.IsNullOrEmpty(fedMessage.OriginatorOptionF.PartyDetails.Name) ? fedMessage.OriginatorOptionF.PartyDetails.Name : "";
                resultMessage.OriginatorFIIdentifier = !string.IsNullOrEmpty(fedMessage.OriginatorFI.AccountIdentifier) ? fedMessage.OriginatorFI.AccountIdentifier : "";
                resultMessage.OriginatorFIName = !string.IsNullOrEmpty(fedMessage.OriginatorFI.AccountName) ? fedMessage.OriginatorFI.AccountName : "";
                resultMessage.InstructingFIIdentifier = !string.IsNullOrEmpty(fedMessage.InstructingFI.AccountIdentifier) ? fedMessage.InstructingFI.AccountIdentifier : ""; 
                resultMessage.InstructingFIName = !string.IsNullOrEmpty(fedMessage.InstructingFI.AccountName) ? fedMessage.InstructingFI.AccountName : ""; 
                resultMessage.DrawDownCreditAccountNumber = !string.IsNullOrEmpty(fedMessage.AccountCreditedinDrawdown) ? fedMessage.AccountCreditedinDrawdown : ""; 
                resultMessage.OriginatortoBeneficiaryInformation = fedMessage.OriginatortoBeneficiaryInformation !=null && !string.IsNullOrEmpty(fedMessage.OriginatortoBeneficiaryInformation[0]) ? fedMessage.OriginatortoBeneficiaryInformation[0] : ""; 
                try
                {
                    resultMessage.SwiftInstructedCurrency = !string.IsNullOrEmpty(fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedCurrency) ? fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedCurrency : "";
                    resultMessage.SwiftInstructedAmount = !string.IsNullOrEmpty(fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedAmountFmt) ? fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedAmountFmt : "";
                   
                    if (fedMessage.SwiftB52AOrderingInstitution !=null && fedMessage.SwiftB52AOrderingInstitution.SwiftDetails != null)
                    {
                        resultMessage.SwiftOrderingInstitution = !string.IsNullOrEmpty(fedMessage.SwiftB52AOrderingInstitution.SwiftDetails[0]) ? fedMessage.SwiftB52AOrderingInstitution.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB50AOrderingCustomer != null && fedMessage.SwiftB50AOrderingCustomer.SwiftDetails != null)
                    {
                        resultMessage.SwiftOrderingCustomer = !string.IsNullOrEmpty(fedMessage.SwiftB50AOrderingCustomer.SwiftDetails[0]) ? fedMessage.SwiftB50AOrderingCustomer.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB56AIntermediaryInstitution != null && fedMessage.SwiftB56AIntermediaryInstitution.SwiftDetails != null)
                    {
                        resultMessage.SwiftIntermiediaryInstitution = !string.IsNullOrEmpty(fedMessage.SwiftB56AIntermediaryInstitution.SwiftDetails[0]) ? fedMessage.SwiftB56AIntermediaryInstitution.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB57AAccountwithInstitution != null && fedMessage.SwiftB57AAccountwithInstitution.SwiftDetails != null)
                    {
                        resultMessage.SwiftAccountWithInstitution = !string.IsNullOrEmpty(fedMessage.SwiftB57AAccountwithInstitution.SwiftDetails[0]) ? fedMessage.SwiftB57AAccountwithInstitution.SwiftDetails[0] : "";
                    }


                    if (fedMessage.SwiftB59ABeneficiaryCustomer != null && fedMessage.SwiftB59ABeneficiaryCustomer.SwiftDetails != null)
                    {
                        resultMessage.SwiftBeneficiaryCustomer = !string.IsNullOrEmpty(fedMessage.SwiftB59ABeneficiaryCustomer.SwiftDetails[0]) ? fedMessage.SwiftB59ABeneficiaryCustomer.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB70RemittanceInformation != null && fedMessage.SwiftB70RemittanceInformation.SwiftDetails != null)
                    {
                        resultMessage.SwiftRemittanceInformation = !string.IsNullOrEmpty(fedMessage.SwiftB70RemittanceInformation.SwiftDetails[0]) ? fedMessage.SwiftB70RemittanceInformation.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB72SendertoReceiverInformation != null && fedMessage.SwiftB72SendertoReceiverInformation.SwiftDetails != null)
                    {
                        resultMessage.SwiftSenderToReceiverInformation = !string.IsNullOrEmpty(fedMessage.SwiftB72SendertoReceiverInformation.SwiftDetails[0]) ? fedMessage.SwiftB72SendertoReceiverInformation.SwiftDetails[0] : "";
                    }
                    if (fedMessage.UnstructuredAddendaInformation != null && fedMessage.UnstructuredAddendaInformation.AddendaDetail != null)
                    {
                        resultMessage.AddendaInformation = !string.IsNullOrEmpty(fedMessage.UnstructuredAddendaInformation.AddendaDetail) ? fedMessage.UnstructuredAddendaInformation.AddendaDetail : "";
                    }


                    //  

                }
                catch (Exception ex )
                {
                    log.WriteToLog(ex.Message);
                    log.WriteToLog(ex.StackTrace);
                }
              
                resultMessage.previousMessageIdentifier = !string.IsNullOrEmpty(fedMessage.PreviousMessageId) ? fedMessage.PreviousMessageId : ""; ;
                resultMessage.NonProcessedInformation = !string.IsNullOrEmpty(fedMessage.Miscellaneous) ? fedMessage.Miscellaneous : ""; ;
                resultMessage.FileName = Path.Combine(fedMessage.MessageFilePath, fedMessage.OriginName);
                resultMessage.FileName = fedMessage.MessageFilePath;
                resultMessage.RecordNumber = fedMessage.MessageId;
              //  string record = PropertyExtensions.SetCsvRecord(resultMessage);

              //  outPutFileResult.WriteLine(record);
              //  outPutFileResult.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
           
            return 1;
        }
       
        public void WriteResults()
        {
            try
            {
                string record = PropertyExtensions.SetCsvRecord(resultMessage);

                outPutFileResult.WriteLine(record);
                outPutFileResult.Flush();
                outPutFileResult.WriteLine(record);
                outPutFileResult.Flush();
            }
            catch { }
           
        }

        private void initialize()
        {

          //  string dateyyyyMM = String.Format("yyyyMM", fileReceiveDate);
          string dateyyyyMM= fileReceiveDate.ToString("yyyyMM");
            string resultFileName = $"FedWireDataResults_{inOutFile}_{dateyyyyMM}.txt";
            csvHeader = loadCsvFileHeader();

           
            outputResultDataFile = Path.Combine(appSettings.CommonDetail.OutputReportDirectory, resultFileName);
            bool fileExist = !File.Exists(outputResultDataFile) ? false : true;

            if (fileExist)
            {
              while (isFileLocked(outputResultDataFile))
                {
                    log.WriteToLog("Fileis Being Locked by ANother Process Waiting");
                }
            }
            outPutFileResult = new StreamWriter(outputResultDataFile, true);

            try
            {
                if (!fileExist)
                {
                    outPutFileResult.WriteLine(csvHeader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                outPutFileResult.Flush();
            }
        }

        public void closeFile()
        {


            try
            {
                outPutFileResult.Flush();
                outPutFileResult.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private string loadCsvFileHeader()
        {
            int count = 0;
            var fileHeaderName = appSettings.CommonDetail.OutputResultDataFileHeader;
            List<string> headerList = new List<string>();
            StringBuilder sbHeader = new StringBuilder();
            try
            {
                //StreamReader fedIncoming = new StreamReader(inputFile);
                using (var reader = new StreamReader(fileHeaderName))
                {
                    while (!reader.EndOfStream)
                    {

                        string header = reader.ReadLine();
                        if (!string.IsNullOrEmpty(header))
                        {
                            if (count > 0) sbHeader.Append("\t");
                            sbHeader.Append('"' + header + '"');
                            count++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.WriteToLog(ex.StackTrace);
            }

            // log.WriteToLog(sbHeader.ToString());
            // Console.WriteLine(log.StrLogPath);
            return sbHeader.ToString();

        }

        // Return true if the file is locked for the indicated access.
        private bool isFileLocked(string filename, FileAccess file_access = FileAccess.ReadWrite)
        {
            // Try to open the file with the indicated access.
            
            try
            {
                FileStream fs =
                    new FileStream(filename, FileMode.Open, file_access);
                fs.Close();
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
