using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Cfsb.Incoming.FedWires.Utils;
using Cfsb.LoggerWriter.Services;
using Cfsb.Incoming.FedWires.DataEntities;
using Cfsb.Incoming.FedWires.MessageStructure;

namespace Cfsb.Incoming.FedWires.ORM
{
    public class DbInsertMessageRepo
    {

        private DateTime? msgReceivedDate;
        private LogWriter log;
        private SqlConnection connection = null;
        private FedWireMessage fedMessage;
        private bool messageRepoInserted;
        private bool rawMessageInserted;

        public LogWriter Log { get => log; set => log = value; }
        public SqlConnection Connection { get => connection; set => connection = value; }
        public FedWireMessage FedMessage { get => fedMessage; set => fedMessage = value; }
        public bool MessageRepoInserted { get => messageRepoInserted; set => messageRepoInserted = value; }
        public bool RawMessageInserted { get => rawMessageInserted; set => rawMessageInserted = value; }

        public DbInsertMessageRepo()
        {

        }
        public DbInsertMessageRepo(LogWriter log, SqlConnection connection)
        {

            this.log = log;
            this.connection = connection;

        }

        public DbInsertMessageRepo(LogWriter log)
        {

            this.log = log;
            this.connection = ORM.DBConnect.Connection;

        }
        public int StoreFedMessageRepo(FedWireMessage fedMessage, int recordNumber)
        {
      
            messageRepoInserted = false;
            rawMessageInserted = false;
            DBConnect.CheckConnection();

            try
            {
                this.fedMessage = fedMessage;
                FedWireMsgRepo fedWireMsgRepo = new FedWireMsgRepo();
                PropertyExtensions.InitializePropery(fedWireMsgRepo);
                try
                {
                    if (fedMessage.SenderSuppliedInformation!=null)
                    { 
                        fedWireMsgRepo.SndFormatVersion = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.FormatVersion) ? fedMessage.SenderSuppliedInformation.FormatVersion : String.Empty;
                        fedWireMsgRepo.SndUserRequestCorrelation = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.UserRequestCorrelation) ? fedMessage.SenderSuppliedInformation.UserRequestCorrelation : String.Empty;
                        fedWireMsgRepo.SndTestProductionCode = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.ProductionCode) ? fedMessage.SenderSuppliedInformation.ProductionCode : String.Empty;
                        fedWireMsgRepo.SndMessageDuplication = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.MessageDuplicationCode) ? fedMessage.SenderSuppliedInformation.MessageDuplicationCode : String.Empty;
                    }

                    fedWireMsgRepo.OMAD = !string.IsNullOrEmpty(fedMessage.Omad) ? fedMessage.Omad : String.Empty;
                    fedWireMsgRepo.IMAD = !string.IsNullOrEmpty(fedMessage.Imad) ? fedMessage.Imad : String.Empty;

                    fedWireMsgRepo.FormatVersion = !string.IsNullOrEmpty(fedMessage.MessageDisposition.FormatVersion) ? fedMessage.MessageDisposition.FormatVersion : String.Empty;
                    fedWireMsgRepo.TestProductionCode = !string.IsNullOrEmpty(fedMessage.MessageDisposition.ProductionCode) ? fedMessage.MessageDisposition.ProductionCode : String.Empty;
                    fedWireMsgRepo.MessageDuplication = !string.IsNullOrEmpty(fedMessage.MessageDisposition.MessageDuplicationCode) ? fedMessage.MessageDisposition.MessageDuplicationCode : String.Empty;
                    fedWireMsgRepo.MessageStatus = !string.IsNullOrEmpty(fedMessage.MessageDisposition.MessageStatusStr) ? fedMessage.MessageDisposition.MessageStatusStr : String.Empty;

                    fedWireMsgRepo.InputCycleDate = !string.IsNullOrEmpty(fedMessage.InputMessageAccountabilityData.InputCycleDate_CCYYMMDD) ? fedMessage.InputMessageAccountabilityData.InputCycleDate_CCYYMMDD : String.Empty;
                    fedWireMsgRepo.InputSource = !string.IsNullOrEmpty(fedMessage.InputMessageAccountabilityData.InputSource) ? fedMessage.InputMessageAccountabilityData.InputSource : String.Empty;
                    fedWireMsgRepo.InputSequenceNumber = !string.IsNullOrEmpty(fedMessage.InputMessageAccountabilityData.InputSequenceNumber) ? fedMessage.InputMessageAccountabilityData.InputSequenceNumber : String.Empty;

                    if (fedMessage.OutputMessageAccountabilityData != null && fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp != null)
                    {
                        fedWireMsgRepo.OutputCycleDateDateReceived = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.InputCycleDate_CCYYMMDD) ? fedMessage.OutputMessageAccountabilityData.InputCycleDate_CCYYMMDD : String.Empty;
                        fedWireMsgRepo.OutputDestination = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.InputSource) ? fedMessage.OutputMessageAccountabilityData.InputSource : String.Empty;
                        fedWireMsgRepo.OutputSequenceNumber = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.InputSequenceNumber) ? fedMessage.OutputMessageAccountabilityData.InputSequenceNumber : String.Empty;
                        fedWireMsgRepo.OutputDate = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp.ReceiptDate) ? fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp.ReceiptDate : String.Empty;
                        fedWireMsgRepo.OutputTime = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp.ReceiptTime) ? fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp.ReceiptTime : String.Empty;
                        fedWireMsgRepo.OutputApplication = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp.ApplicationId) ? fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp.ApplicationId : String.Empty;

                    }

                    if (fedMessage.ReceiptTimeStamp !=null)
                    {
                        fedWireMsgRepo.ReceiptDate = !string.IsNullOrEmpty(fedMessage.ReceiptTimeStamp.ReceiptDate) ? fedMessage.ReceiptTimeStamp.ReceiptDate : String.Empty;
                        fedWireMsgRepo.ReceiptTime = !string.IsNullOrEmpty(fedMessage.ReceiptTimeStamp.ReceiptTime) ? fedMessage.ReceiptTimeStamp.ReceiptTime : String.Empty;
                        fedWireMsgRepo.ReceiptApplication = !string.IsNullOrEmpty(fedMessage.ReceiptTimeStamp.ApplicationId) ? fedMessage.ReceiptTimeStamp.ApplicationId : String.Empty;

                    }

                    fedWireMsgRepo.SndFormatVersion = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.FormatVersion) ? fedMessage.SenderSuppliedInformation.FormatVersion : String.Empty;
                    fedWireMsgRepo.SndUserRequestCorrelation = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.UserRequestCorrelation) ? fedMessage.SenderSuppliedInformation.UserRequestCorrelation : String.Empty;
                    fedWireMsgRepo.SndTestProductionCode = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.ProductionCode) ? fedMessage.SenderSuppliedInformation.ProductionCode : String.Empty;
                    fedWireMsgRepo.SndMessageDuplication = !string.IsNullOrEmpty(fedMessage.SenderSuppliedInformation.MessageDuplicationCode) ? fedMessage.SenderSuppliedInformation.MessageDuplicationCode : String.Empty;

                    //
                    fedWireMsgRepo.WIREINOUT = fedMessage.IncomingOutGoing;
                    fedWireMsgRepo.TypeSubtype = !string.IsNullOrEmpty(fedMessage.TypeSubType.MessageTypeSubType) ? fedMessage.TypeSubType.MessageTypeSubType : String.Empty;

                    fedWireMsgRepo.TypeCode = !string.IsNullOrEmpty(fedMessage.TypeSubType.MessageType) ? fedMessage.TypeSubType.MessageType : String.Empty;
                    fedWireMsgRepo.TypeSubCode = !string.IsNullOrEmpty(fedMessage.TypeSubType.MessageSubType) ? fedMessage.TypeSubType.MessageSubType : String.Empty;

                    fedWireMsgRepo.TypeCodeDesc = !string.IsNullOrEmpty(fedMessage.TypeSubType.TransactionTypeName) ? fedMessage.TypeSubType.TransactionTypeName : String.Empty;
                    fedWireMsgRepo.TypeSubCodeDesc = !string.IsNullOrEmpty(fedMessage.TypeSubType.TransactionSubTypeName) ? fedMessage.TypeSubType.TransactionSubTypeName : String.Empty;


                    fedWireMsgRepo.SenderABA = !string.IsNullOrEmpty(fedMessage.SenderDI.SenderReceiverABA) ? fedMessage.SenderDI.SenderReceiverABA : String.Empty;
                    fedWireMsgRepo.SenderName = !string.IsNullOrEmpty(fedMessage.SenderDI.SenderReceiverShortName) ? fedMessage.SenderDI.SenderReceiverShortName : String.Empty;
                    fedWireMsgRepo.SenderReference = !string.IsNullOrEmpty(fedMessage.SenderReference) ? fedMessage.SenderReference : String.Empty;

                    fedWireMsgRepo.ReceiverABA = !string.IsNullOrEmpty(fedMessage.ReceiverDI.SenderReceiverABA) ? fedMessage.ReceiverDI.SenderReceiverABA : String.Empty;
                    fedWireMsgRepo.ReceiverName = !string.IsNullOrEmpty(fedMessage.ReceiverDI.SenderReceiverShortName) ? fedMessage.ReceiverDI.SenderReceiverShortName : String.Empty;

                    fedWireMsgRepo.BusinessFunction = !string.IsNullOrEmpty(fedMessage.BusinessFunctionCode) ? fedMessage.BusinessFunctionCode : String.Empty;
                    fedWireMsgRepo.TransactionCode = String.Empty; // to complete

                    
                    fedWireMsgRepo.PaymentNotificationContactName = fedMessage.Paymentnotification != null && !string.IsNullOrEmpty(fedMessage.Paymentnotification.ContactName) ? fedMessage.Paymentnotification.ContactName : String.Empty;

                    fedWireMsgRepo.TransactionAmount = fedMessage.Amount.TransactionAmount;
                    fedWireMsgRepo.InstructedAmountCurrency = fedMessage.InstructedAmount != null && !string.IsNullOrEmpty(fedMessage.InstructedAmount.Currency) ? fedMessage.InstructedAmount.Currency : String.Empty; ;
                    fedWireMsgRepo.InstructedAmount = fedMessage.InstructedAmount.Amount;


                    if (fedMessage.IntermediaryFI != null)
                    {
                        fedWireMsgRepo.IntemediaryFIIdentifier = !string.IsNullOrEmpty(fedMessage.IntermediaryFI.AccountIdentifier) ? fedMessage.IntermediaryFI.AccountIdentifier : String.Empty;
                        fedWireMsgRepo.IntermediaryFIName = !string.IsNullOrEmpty(fedMessage.IntermediaryFI.AccountName) ? fedMessage.IntermediaryFI.AccountName : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "IntermediaryFIAddressL", fedMessage.IntermediaryFI.AccountAddress);
                    }

                    if (fedMessage.BeneficiaryFI != null)
                    {
                        fedWireMsgRepo.BeneficiaryFIIdentifierAccount = !string.IsNullOrEmpty(fedMessage.BeneficiaryFI.AccountIdentifier) ? fedMessage.BeneficiaryFI.AccountIdentifier : String.Empty;
                        fedWireMsgRepo.BeneficiaryFIName = !string.IsNullOrEmpty(fedMessage.BeneficiaryFI.AccountName) ? fedMessage.BeneficiaryFI.AccountName : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "BeneficiaryFIAddressL", fedMessage.BeneficiaryFI.AccountAddress);
                    }
                    if (fedMessage.Beneficiary != null)
                    {
                        fedWireMsgRepo.BeneficiaryIdentifierAccount = !string.IsNullOrEmpty(fedMessage.Beneficiary.AccountIdentifier) ? fedMessage.Beneficiary.AccountIdentifier : String.Empty;
                        fedWireMsgRepo.BeneficiaryName = !string.IsNullOrEmpty(fedMessage.Beneficiary.AccountName) ? fedMessage.Beneficiary.AccountName : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "BeneficiaryAddressL", fedMessage.Beneficiary.AccountAddress);

                    }
                    if (fedMessage.AccountDebitedinDrawdown != null)
                    {
                        fedWireMsgRepo.AccountDebitInDrawDownIdentifier = !string.IsNullOrEmpty(fedMessage.AccountDebitedinDrawdown.AccountIdentifier) ? fedMessage.AccountDebitedinDrawdown.AccountIdentifier : String.Empty;
                        fedWireMsgRepo.AccountDebitInDrawDownName = !string.IsNullOrEmpty(fedMessage.AccountDebitedinDrawdown.AccountName) ? fedMessage.AccountDebitedinDrawdown.AccountName : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "AccountDebitInDrawDownAddressL", fedMessage.AccountDebitedinDrawdown.AccountAddress);

                    }

                    if (fedMessage.Originator != null)
                    {
                        fedWireMsgRepo.OriginatorIdentifier = !string.IsNullOrEmpty(fedMessage.Originator.AccountIdentifier) ? fedMessage.Originator.AccountIdentifier : String.Empty;
                        fedWireMsgRepo.OriginatorName = !string.IsNullOrEmpty(fedMessage.Originator.AccountName) ? fedMessage.Originator.AccountName : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "OriginatorAddressL", fedMessage.Originator.AccountAddress);
                    }

                    if (fedMessage.OriginatorOptionF != null)
                    {
                        fedWireMsgRepo.OriginatorOptionFPartyIdentifier = !string.IsNullOrEmpty(fedMessage.OriginatorOptionF.PartyUniqueIdentifier) ? fedMessage.OriginatorOptionF.PartyUniqueIdentifier : String.Empty;
                        fedWireMsgRepo.OriginatorOptionFPartyName = !string.IsNullOrEmpty(fedMessage.OriginatorOptionF.PartyDetails.Name) ? fedMessage.OriginatorOptionF.PartyDetails.Name : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "OriginatorOptionFIDetL", fedMessage.OriginatorOptionF.PartyLineDetail);
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "OriginatorOptionFICodeL", fedMessage.OriginatorOptionF.PartyLineCode);
                    }

                    if (fedMessage.OriginatorFI != null)
                    {
                        fedWireMsgRepo.OriginatorFIIdentifier = !string.IsNullOrEmpty(fedMessage.OriginatorFI.AccountIdentifier) ? fedMessage.OriginatorFI.AccountIdentifier : String.Empty;
                        fedWireMsgRepo.OriginatorFIName = !string.IsNullOrEmpty(fedMessage.OriginatorFI.AccountName) ? fedMessage.OriginatorFI.AccountName : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "OriginatorFIAddressL", fedMessage.OriginatorFI.AccountAddress);
                        
                    }

                    if (fedMessage.InstructingFI != null)
                    {
                        fedWireMsgRepo.InstructingFIIdentifier = !string.IsNullOrEmpty(fedMessage.InstructingFI.AccountIdentifier) ? fedMessage.InstructingFI.AccountIdentifier : String.Empty;
                        fedWireMsgRepo.InstructingFIName = !string.IsNullOrEmpty(fedMessage.InstructingFI.AccountName) ? fedMessage.InstructingFI.AccountName : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "InstructingFIAddressL", fedMessage.InstructingFI.AccountAddress);
                    }

                    if (fedMessage.ReceiverFIInformation != null)
                    {
                        fedWireMsgRepo.ReceiverFIInformation = !string.IsNullOrEmpty(fedMessage.ReceiverFIInformation.TransactionFIInformation) ? fedMessage.ReceiverFIInformation.TransactionFIInformation : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "ReceiverFIIAddInformationL", fedMessage.ReceiverFIInformation.InformationDetails);
                    }

                    if (fedMessage.IntermediaryFIInformation != null)
                    {
                        fedWireMsgRepo.IntermediaryFIInformation = !string.IsNullOrEmpty(fedMessage.IntermediaryFIInformation.TransactionFIInformation) ? fedMessage.IntermediaryFIInformation.TransactionFIInformation : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "IntermediaryFIIAddInformationL", fedMessage.IntermediaryFIInformation.InformationDetails);
                    }

                    if (fedMessage.BeneficiarysFIInformation != null)
                    {
                        fedWireMsgRepo.BeneficiarysFIInformation = !string.IsNullOrEmpty(fedMessage.BeneficiarysFIInformation.TransactionFIInformation) ? fedMessage.BeneficiarysFIInformation.TransactionFIInformation : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "BeneficiarysFIInformationL", fedMessage.BeneficiarysFIInformation.InformationDetails);
                    }

                    if (fedMessage.BeneficiaryInformation != null)
                    {
                        fedWireMsgRepo.BeneficiaryInformation = !string.IsNullOrEmpty(fedMessage.BeneficiaryInformation.TransactionFIInformation) ? fedMessage.BeneficiaryInformation.TransactionFIInformation : String.Empty;
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "BeneficiaryInformationL", fedMessage.BeneficiaryInformation.InformationDetails);
                    }

                    fedWireMsgRepo.DrawDownCreditAccountNumber = !string.IsNullOrEmpty(fedMessage.AccountCreditedinDrawdown) ? fedMessage.AccountCreditedinDrawdown : String.Empty;

                    if (fedMessage.SwiftB33BCurrencyInstructedAmount != null)
                    {
                        fedWireMsgRepo.SwiftInstructedCurrency = !string.IsNullOrEmpty(fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedCurrency) ? fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedCurrency : String.Empty;
                        fedWireMsgRepo.SwiftInstructedAmount = fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedAmount;
                       
                    }

                    if (fedMessage.OriginatortoBeneficiaryInformation!=null)
                    { 
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo, "OriginatortoBeneficiaryInformation", fedMessage.OriginatortoBeneficiaryInformation);
                    }

                    try
                   {
                       
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo,
                                                                  "SwiftOrderingCustomerL",
                                                                  fedMessage.SwiftB50AOrderingCustomer.SwiftDetails);
                       
                    }
                    catch (Exception exSet)
                    {
                        log.WriteToLog(exSet.Message);
                      log.WriteToLog(exSet.StackTrace);
                        log.WriteToLog(fedMessage.Imad);
                    }
                    try
                    {
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo,
                                                                  "SwiftOrderingInstitutionL",
                                                                  fedMessage.SwiftB52AOrderingInstitution.SwiftDetails);
                                                                  

                    }
                    catch (Exception exSet)
                    {
                        log.WriteToLog(exSet.Message);
                        log.WriteToLog(exSet.StackTrace);
                        log.WriteToLog(fedMessage.Imad);
                    }

                    try
                    {
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo,
                                                                   "SwiftIntermiediaryInstitutionL",
                                                                   fedMessage.SwiftB56AIntermediaryInstitution.SwiftDetails);
                    }
                    catch (Exception exSet)
                    {
                        log.WriteToLog(exSet.Message);
                        log.WriteToLog(exSet.StackTrace);
                        log.WriteToLog(fedMessage.Imad);
                    }

                    try
                    {

                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo,
                                                    "SwiftAccountWithInstitution",
                                                    fedMessage.SwiftB57AAccountwithInstitution.SwiftDetails);
                    }
                    catch (Exception exSet)
                    {
                        log.WriteToLog(exSet.Message);
                        log.WriteToLog(exSet.StackTrace);
                        log.WriteToLog(fedMessage.Imad);
                    }

                    try
                    {
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo,
                                                     "SwiftBeneficiaryCustomerL",
                                                     fedMessage.SwiftB59ABeneficiaryCustomer.SwiftDetails);
                    }
                    catch (Exception exSet)
                    {
                        log.WriteToLog(exSet.Message);
                        log.WriteToLog(exSet.StackTrace);
                        log.WriteToLog(fedMessage.Imad);
                    }

                    try
                    {
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo,
                                                     "SwiftRemittanceInformationL",
                                                     fedMessage.SwiftB70RemittanceInformation.SwiftDetails);
                    }
                    catch (Exception exSet)
                    {
                        log.WriteToLog(exSet.Message);
                        log.WriteToLog(exSet.StackTrace);
                        log.WriteToLog(fedMessage.Imad);
                    }

                    try
                    {
                        PropertyExtensions.SetTableFieldWithArray(fedWireMsgRepo,
                                                     "SwiftSenderToReceiverInformationL",
                                                     fedMessage.SwiftB72SendertoReceiverInformation.SwiftDetails);
                    }
                    catch (Exception exSet)
                    {
                        log.WriteToLog(exSet.Message);
                        log.WriteToLog(exSet.StackTrace);
                        log.WriteToLog(fedMessage.Imad);
                    }


                    if (fedMessage.UnstructuredAddendaInformation != null && fedMessage.UnstructuredAddendaInformation.AddendaDetail != null)
                    {
                        fedWireMsgRepo.AddendaInformation = !string.IsNullOrEmpty(fedMessage.UnstructuredAddendaInformation.AddendaDetail) ? fedMessage.UnstructuredAddendaInformation.AddendaDetail : String.Empty;
                    }

                    fedWireMsgRepo.previousMessageIdentifier = !string.IsNullOrEmpty(fedMessage.PreviousMessageId) ? fedMessage.PreviousMessageId : String.Empty; ;
                    fedWireMsgRepo.NonProcessedInformation = !string.IsNullOrEmpty(fedMessage.Miscellaneous) ? fedMessage.Miscellaneous : String.Empty; ;


                    fedWireMsgRepo.FileName = Path.Combine(fedMessage.MessageFilePath, fedMessage.OriginName);
                    fedWireMsgRepo.FileName = fedMessage.MessageFilePath;


                    string format = "yyyyMMdd";
                    DateTime receivedDate;
                    msgReceivedDate = null;
                    if (DateTime.TryParseExact(fedWireMsgRepo.InputCycleDate,
                                               format,
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out receivedDate))
                    {
                        msgReceivedDate = receivedDate;
                    }

                    int count = 0;

                    fedWireMsgRepo.RecordNumber = recordNumber;
                    //  if (!checkFedRepoMessage(fedWireMsgRepo))
                    count = InsertFedWireMessage(fedWireMsgRepo);

                }
                catch (Exception ex)
                {
                    log.WriteToLog(ex.Message);
                    log.WriteToLog(ex.StackTrace);

                    fedWireMsgRepo.LoadStatus = "Error";
                    fedWireMsgRepo.LoadRejectReason = "ex.Message";
              
                 
                }
                try
                {
                    InsertRawMessage(fedWireMsgRepo);
                }
                catch (Exception ex)
                {
                  
                    fedWireMsgRepo.LoadStatus = "Error";
                    fedWireMsgRepo.LoadRejectReason = "ex.Message";
                    log.WriteToLog(ex.Message);
                    log.WriteToLog(ex.StackTrace);
                }
            }
            catch (Exception stEx)
            {
               
                log.WriteToLog(stEx.Message);
                log.WriteToLog(stEx.StackTrace);
            }
            return 1;
        }


        private int InsertFedWireMessage(FedWireMsgRepo fedWireMsgRepo)
        {

            using (SqlCommand command = connection.CreateCommand())
            {

                List<SqlParameter> dictionaryObjectParameters = getFundMessagesParameters(fedWireMsgRepo);
                try
                {
                    command.Parameters.Clear();

                    command.CommandText = "SP_Insert_FedWireMsgRepo";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(dictionaryObjectParameters.ToArray());
                    command.Parameters["@Status"].Direction = ParameterDirection.Output;
                    command.Parameters["@InternalId"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    string  status = (string)(command.Parameters["@Status"].Value).ToString();
                    int generatedKey = Convert.ToInt32(command.Parameters["@InternalId"].Value);
                                      ;
                    messageRepoInserted = true;
                    if (generatedKey <1 )
                    {
                        messageRepoInserted = false;
                        fedWireMsgRepo.LoadStatus = "FAILED";
                        fedWireMsgRepo.LoadRejectReason = "Failed to insert into Message Repo Table with Status : " + status;
                    }
                    return generatedKey;
                }
                catch (SqlException sqlEx)
                {
                    fedWireMsgRepo.LoadStatus = "Error";
                    fedWireMsgRepo.LoadRejectReason = sqlEx.Message;

                    log.WriteToLog(sqlEx.Message);
                    log.WriteToLog(sqlEx.StackTrace);
                    return -1;

                }
            }

        }

        private int InsertRawMessage(FedWireMsgRepo fedWireMsgRepo)
        {
            DBConnect.CheckConnection();


            using (SqlCommand command = connection.CreateCommand())
            {

                List<SqlParameter> dictionaryObjectParameters = getRawMessageParameters(fedWireMsgRepo);
                try
                {

                    command.Parameters.Clear();

                    command.CommandText = "SP_Insert_Rawmessage";
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.AddRange(dictionaryObjectParameters.ToArray());
                    command.Parameters["@InternalId"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    int generatedKey = Convert.ToInt32(command.Parameters["@InternalId"].Value);
                    ;
                   // messageRepoInserted = true;
                    if (generatedKey < 1)
                    {
                      //  messageRepoInserted = false;
                        string messageError = $"Failed to insert into Meassage Raw Table, Mesage  Row Already Exist with  IMAD ={fedWireMsgRepo.IMAD} = File = {fedWireMsgRepo.FileName}";
                           
                        fedWireMsgRepo.LoadStatus = "FAILED";
                      // log.WriteToLog(messageError);
                    }
                    return generatedKey;

                }
                catch (SqlException sqlEx)
                {
                    fedWireMsgRepo.LoadStatus = "Error";
                    fedWireMsgRepo.LoadRejectReason = sqlEx.Message;

                    log.WriteToLog(sqlEx.Message + " IMAD " + fedMessage.Imad);
                    log.WriteToLog(sqlEx.StackTrace);
                    return -1;

                }
            }

        }


        public int InsertLoadMessageFile(string messageFile, int recordRead, int recordProcessed, int recordRejected )
        {
            // getFileHistoryParameters(DateTime fileDate, int fileZize, int recordRead, int recordProcessed, int recordRejected, string fileContent)
            FileInfo fi = new FileInfo(messageFile);
            string fielContent= System.IO.File.ReadAllText(messageFile);
            using (SqlCommand command = connection.CreateCommand())
            {

                List<SqlParameter> dictionaryObjectParameters = getFileHistoryParameters(fi.CreationTime, (int)fi.Length, recordRead, recordProcessed, recordRejected, fielContent);
                try
                {

                    command.Parameters.Clear();

                    command.CommandText = "SP_InterfaceLoadHistory";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(dictionaryObjectParameters.ToArray());
                    command.Parameters["@InternalId"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    int generatedKey = Convert.ToInt32(command.Parameters["@InternalId"].Value);
                    ;
                    messageRepoInserted = true;
                    if (generatedKey < 1)
                    {
                        messageRepoInserted = false;
                        Console.WriteLine("Message Key = " + generatedKey);
                      
                    }
                    return generatedKey;

                }
                catch (SqlException sqlEx)
                {
                  

                    log.WriteToLog(sqlEx.Message + " IMAD " + fedMessage.Imad);
                    log.WriteToLog(sqlEx.StackTrace);
                    return -1;

                }
            }

        }
        private void initialize()
        {

        }

        private List<SqlParameter> getFundMessagesParameters(FedWireMsgRepo fedWireMsgRepo)
        {

            List<SqlParameter> sqlParameterlist = new List<SqlParameter>();
            fedWireMsgRepo.CreationDate = DateTime.Now;
            fedWireMsgRepo.UpdateDate = DateTime.Now;
            fedWireMsgRepo.UpdatorId = Environment.UserName;
            fedWireMsgRepo.CreatorId = Environment.UserName;
            fedWireMsgRepo.LoadStatus = "Active";

           
            sqlParameterlist.Add(new SqlParameter("IMAD", ((object)fedWireMsgRepo.IMAD) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OMAD", ((object)fedWireMsgRepo.OMAD) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("WIREINOUT", ((object)fedWireMsgRepo.WIREINOUT) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("FormatVersion", ((object)fedWireMsgRepo.FormatVersion) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TestProductionCode", ((object)fedWireMsgRepo.TestProductionCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("MessageDuplication", ((object)fedWireMsgRepo.MessageDuplication) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("MessageStatus", ((object)fedWireMsgRepo.MessageStatus) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiptDate", ((object)fedWireMsgRepo.ReceiptDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiptTime", ((object)fedWireMsgRepo.ReceiptTime) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiptApplication", ((object)fedWireMsgRepo.ReceiptApplication) ?? DBNull.Value));

            sqlParameterlist.Add(new SqlParameter("InputCycleDate", ((object)fedWireMsgRepo.InputCycleDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InputSource", ((object)fedWireMsgRepo.InputSource) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InputSequenceNumber", ((object)fedWireMsgRepo.InputSequenceNumber) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputCycleDateDateReceived", ((object)fedWireMsgRepo.OutputCycleDateDateReceived) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputDestination", ((object)fedWireMsgRepo.OutputDestination) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputSequenceNumber", ((object)fedWireMsgRepo.OutputSequenceNumber) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputDate", ((object)fedWireMsgRepo.OutputDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputTime", ((object)fedWireMsgRepo.OutputTime) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputApplication", ((object)fedWireMsgRepo.OutputApplication) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SndFormatVersion", ((object)fedWireMsgRepo.SndFormatVersion) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SndUserRequestCorrelation", ((object)fedWireMsgRepo.SndUserRequestCorrelation) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SndTestProductionCode", ((object)fedWireMsgRepo.SndTestProductionCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SndMessageDuplication", ((object)fedWireMsgRepo.SndMessageDuplication) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TypeSubtype", ((object)fedWireMsgRepo.TypeSubtype) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TypeCode", ((object)fedWireMsgRepo.TypeCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TypeSubCode", ((object)fedWireMsgRepo.TypeSubCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TypeSubCodeDesc", ((object)fedWireMsgRepo.TypeSubCodeDesc) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TypeCodeDesc", ((object)fedWireMsgRepo.TypeCodeDesc) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SenderABA", ((object)fedWireMsgRepo.SenderABA) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SenderName", ((object)fedWireMsgRepo.SenderName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SenderReference", ((object)fedWireMsgRepo.SenderReference) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverABA", ((object)fedWireMsgRepo.ReceiverABA) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverName", ((object)fedWireMsgRepo.ReceiverName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BusinessFunction", ((object)fedWireMsgRepo.BusinessFunction) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TransactionCode", ((object)fedWireMsgRepo.TransactionCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("PaymentNotificationContactName", ((object)fedWireMsgRepo.PaymentNotificationContactName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("TransactionAmount", ((object)fedWireMsgRepo.TransactionAmount) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructedAmountCurrency", ((object)fedWireMsgRepo.InstructedAmountCurrency) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructedAmount", ((object)fedWireMsgRepo.InstructedAmount) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntemediaryFIIdCode", ((object)fedWireMsgRepo.IntemediaryFIIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntemediaryFIIdentifier", ((object)fedWireMsgRepo.IntemediaryFIIdentifier) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIName", ((object)fedWireMsgRepo.IntermediaryFIName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIAddressL1", ((object)fedWireMsgRepo.IntermediaryFIAddressL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIAddressL2", ((object)fedWireMsgRepo.IntermediaryFIAddressL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIAddressL3", ((object)fedWireMsgRepo.IntermediaryFIAddressL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryFIIdCode", ((object)fedWireMsgRepo.BeneficiaryFIIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryFIIdentifierAccount", ((object)fedWireMsgRepo.BeneficiaryFIIdentifierAccount) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryFIName", ((object)fedWireMsgRepo.BeneficiaryFIName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryFIAddressL1", ((object)fedWireMsgRepo.BeneficiaryFIAddressL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryFIAddressL2", ((object)fedWireMsgRepo.BeneficiaryFIAddressL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryFIAddressL3", ((object)fedWireMsgRepo.BeneficiaryFIAddressL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryIdCode", ((object)fedWireMsgRepo.BeneficiaryIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryIdentifierAccount", ((object)fedWireMsgRepo.BeneficiaryIdentifierAccount) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryName", ((object)fedWireMsgRepo.BeneficiaryName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryAddressL1", ((object)fedWireMsgRepo.BeneficiaryAddressL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryAddressL2", ((object)fedWireMsgRepo.BeneficiaryAddressL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryAddressL3", ((object)fedWireMsgRepo.BeneficiaryAddressL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReferenceForBeneficiary", ((object)fedWireMsgRepo.ReferenceForBeneficiary) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("AccountDebitInDrawDownIdCode", ((object)fedWireMsgRepo.AccountDebitInDrawDownIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("AccountDebitInDrawDownIdentifier", ((object)fedWireMsgRepo.AccountDebitInDrawDownIdentifier) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("AccountDebitInDrawDownName", ((object)fedWireMsgRepo.AccountDebitInDrawDownName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("AccountDebitInDrawDownAddressL1", ((object)fedWireMsgRepo.AccountDebitInDrawDownAddressL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("AccountDebitInDrawDownAddressL2", ((object)fedWireMsgRepo.AccountDebitInDrawDownAddressL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("AccountDebitInDrawDownAddressL3", ((object)fedWireMsgRepo.AccountDebitInDrawDownAddressL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorIdCode", ((object)fedWireMsgRepo.OriginatorIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorIdentifier", ((object)fedWireMsgRepo.OriginatorIdentifier) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorName", ((object)fedWireMsgRepo.OriginatorName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorAddressL1", ((object)fedWireMsgRepo.OriginatorAddressL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorAddressL2", ((object)fedWireMsgRepo.OriginatorAddressL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorAddressL3", ((object)fedWireMsgRepo.OriginatorAddressL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFIdCode", ((object)fedWireMsgRepo.OriginatorOptionFIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFPartyIdentifier", ((object)fedWireMsgRepo.OriginatorOptionFPartyIdentifier) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFPartyName", ((object)fedWireMsgRepo.OriginatorOptionFPartyName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFICodeL1", ((object)fedWireMsgRepo.OriginatorOptionFICodeL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFIDetL1", ((object)fedWireMsgRepo.OriginatorOptionFIDetL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFICodeL2", ((object)fedWireMsgRepo.OriginatorOptionFICodeL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFIDetL2", ((object)fedWireMsgRepo.OriginatorOptionFIDetL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFICodeL3", ((object)fedWireMsgRepo.OriginatorOptionFICodeL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorOptionFIDetL3", ((object)fedWireMsgRepo.OriginatorOptionFIDetL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorFIIdCode", ((object)fedWireMsgRepo.OriginatorFIIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorFIIdentifier", ((object)fedWireMsgRepo.OriginatorFIIdentifier) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorFIName", ((object)fedWireMsgRepo.OriginatorFIName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorFIAddressL1", ((object)fedWireMsgRepo.OriginatorFIAddressL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorFIAddressL2", ((object)fedWireMsgRepo.OriginatorFIAddressL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatorFIAddressL3", ((object)fedWireMsgRepo.OriginatorFIAddressL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructingFIIdCode", ((object)fedWireMsgRepo.InstructingFIIdCode) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructingFIIdentifier", ((object)fedWireMsgRepo.InstructingFIIdentifier) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructingFIName", ((object)fedWireMsgRepo.InstructingFIName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructingFIAddressL1", ((object)fedWireMsgRepo.InstructingFIAddressL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructingFIAddressL2", ((object)fedWireMsgRepo.InstructingFIAddressL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InstructingFIAddressL3", ((object)fedWireMsgRepo.InstructingFIAddressL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DrawDownCreditAccountNumber", ((object)fedWireMsgRepo.DrawDownCreditAccountNumber) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatortoBeneficiaryInformationL1", ((object)fedWireMsgRepo.OriginatortoBeneficiaryInformationL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatortoBeneficiaryInformationL2", ((object)fedWireMsgRepo.OriginatortoBeneficiaryInformationL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatortoBeneficiaryInformationL3", ((object)fedWireMsgRepo.OriginatortoBeneficiaryInformationL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OriginatortoBeneficiaryInformationL4", ((object)fedWireMsgRepo.OriginatortoBeneficiaryInformationL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverFIInformation", ((object)fedWireMsgRepo.ReceiverFIInformation) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverFIIAddInformationL1", ((object)fedWireMsgRepo.ReceiverFIIAddInformationL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverFIIAddInformationL2", ((object)fedWireMsgRepo.ReceiverFIIAddInformationL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverFIIAddInformationL3", ((object)fedWireMsgRepo.ReceiverFIIAddInformationL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverFIIAddInformationL4", ((object)fedWireMsgRepo.ReceiverFIIAddInformationL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ReceiverFIIAddInformationL5", ((object)fedWireMsgRepo.ReceiverFIIAddInformationL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIInformation", ((object)fedWireMsgRepo.IntermediaryFIInformation) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIIAddInformationL1", ((object)fedWireMsgRepo.IntermediaryFIIAddInformationL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIIAddInformationL2", ((object)fedWireMsgRepo.IntermediaryFIIAddInformationL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIIAddInformationL3", ((object)fedWireMsgRepo.IntermediaryFIIAddInformationL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIIAddInformationL4", ((object)fedWireMsgRepo.IntermediaryFIIAddInformationL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IntermediaryFIIAddInformationL5", ((object)fedWireMsgRepo.IntermediaryFIIAddInformationL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftInstructedCurrency", ((object)fedWireMsgRepo.SwiftInstructedCurrency) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftInstructedAmount", ((object)fedWireMsgRepo.SwiftInstructedAmount) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingCustomerL1", ((object)fedWireMsgRepo.SwiftOrderingCustomerL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingCustomerL2", ((object)fedWireMsgRepo.SwiftOrderingCustomerL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingCustomerL3", ((object)fedWireMsgRepo.SwiftOrderingCustomerL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingCustomerL4", ((object)fedWireMsgRepo.SwiftOrderingCustomerL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingCustomerL5", ((object)fedWireMsgRepo.SwiftOrderingCustomerL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingInstitutionL1", ((object)fedWireMsgRepo.SwiftOrderingInstitutionL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingInstitutionL2", ((object)fedWireMsgRepo.SwiftOrderingInstitutionL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingInstitutionL3", ((object)fedWireMsgRepo.SwiftOrderingInstitutionL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingInstitutionL4", ((object)fedWireMsgRepo.SwiftOrderingInstitutionL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftOrderingInstitutionL5", ((object)fedWireMsgRepo.SwiftOrderingInstitutionL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftIntermiediaryInstitutionL1", ((object)fedWireMsgRepo.SwiftIntermiediaryInstitutionL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftIntermiediaryInstitutionL2", ((object)fedWireMsgRepo.SwiftIntermiediaryInstitutionL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftIntermiediaryInstitutionL3", ((object)fedWireMsgRepo.SwiftIntermiediaryInstitutionL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftIntermiediaryInstitutionL4", ((object)fedWireMsgRepo.SwiftIntermiediaryInstitutionL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftIntermiediaryInstitutionL5", ((object)fedWireMsgRepo.SwiftIntermiediaryInstitutionL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftAccountWithInstitutionL1", ((object)fedWireMsgRepo.SwiftAccountWithInstitutionL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftAccountWithInstitutionL2", ((object)fedWireMsgRepo.SwiftAccountWithInstitutionL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftAccountWithInstitutionL3", ((object)fedWireMsgRepo.SwiftAccountWithInstitutionL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftAccountWithInstitutionL4", ((object)fedWireMsgRepo.SwiftAccountWithInstitutionL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftAccountWithInstitutionL5", ((object)fedWireMsgRepo.SwiftAccountWithInstitutionL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftBeneficiaryCustomerL1", ((object)fedWireMsgRepo.SwiftBeneficiaryCustomerL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftBeneficiaryCustomerL2", ((object)fedWireMsgRepo.SwiftBeneficiaryCustomerL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftBeneficiaryCustomerL3", ((object)fedWireMsgRepo.SwiftBeneficiaryCustomerL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftBeneficiaryCustomerL4", ((object)fedWireMsgRepo.SwiftBeneficiaryCustomerL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftBeneficiaryCustomerL5", ((object)fedWireMsgRepo.SwiftBeneficiaryCustomerL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftRemittanceInformationL1", ((object)fedWireMsgRepo.SwiftRemittanceInformationL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftRemittanceInformationL2", ((object)fedWireMsgRepo.SwiftRemittanceInformationL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftRemittanceInformationL3", ((object)fedWireMsgRepo.SwiftRemittanceInformationL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftRemittanceInformationL4", ((object)fedWireMsgRepo.SwiftRemittanceInformationL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftRemittanceInformationL5", ((object)fedWireMsgRepo.SwiftRemittanceInformationL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftSenderToReceiverInformationL1", ((object)fedWireMsgRepo.SwiftSenderToReceiverInformationL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftSenderToReceiverInformationL2", ((object)fedWireMsgRepo.SwiftSenderToReceiverInformationL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftSenderToReceiverInformationL3", ((object)fedWireMsgRepo.SwiftSenderToReceiverInformationL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftSenderToReceiverInformationL4", ((object)fedWireMsgRepo.SwiftSenderToReceiverInformationL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("SwiftSenderToReceiverInformationL5", ((object)fedWireMsgRepo.SwiftSenderToReceiverInformationL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("AddendaInformation", ((object)fedWireMsgRepo.AddendaInformation) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("previousMessageIdentifier", ((object)fedWireMsgRepo.previousMessageIdentifier) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("NonProcessedInformation", ((object)fedWireMsgRepo.NonProcessedInformation) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("FileName", ((object)fedWireMsgRepo.FileName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("RecordNumber", ((object)fedWireMsgRepo.RecordNumber) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("LoadStatus", ((object)fedWireMsgRepo.LoadStatus) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("LoadRejectReason", ((object)fedWireMsgRepo.LoadRejectReason) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("CreationDate", ((object)fedWireMsgRepo.CreationDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("CreatorId", ((object)fedWireMsgRepo.CreatorId) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("UpdateDate", ((object)fedWireMsgRepo.UpdateDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("UpdatorId", ((object)fedWireMsgRepo.UpdatorId) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DeletedDate", ((object)fedWireMsgRepo.DeletedDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DeleteddBy", ((object)fedWireMsgRepo.DeleteddBy) ?? DBNull.Value));


            sqlParameterlist.Add(new SqlParameter("BeneficiarysFIInformation", ((object)fedWireMsgRepo.BeneficiarysFIInformation) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiarysFIInformationL1", ((object)fedWireMsgRepo.BeneficiarysFIInformationL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiarysFIInformationL2", ((object)fedWireMsgRepo.BeneficiarysFIInformationL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiarysFIInformationL3", ((object)fedWireMsgRepo.BeneficiarysFIInformationL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiarysFIInformationL4", ((object)fedWireMsgRepo.BeneficiarysFIInformationL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiarysFIInformationL5", ((object)fedWireMsgRepo.BeneficiarysFIInformationL5) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryInformation", ((object)fedWireMsgRepo.BeneficiaryInformation) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryInformationL1", ((object)fedWireMsgRepo.BeneficiaryInformationL1) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryInformationL2", ((object)fedWireMsgRepo.BeneficiaryInformationL2) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryInformationL3", ((object)fedWireMsgRepo.BeneficiaryInformationL3) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryInformationL4", ((object)fedWireMsgRepo.BeneficiaryInformationL4) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BeneficiaryInformationL5", ((object)fedWireMsgRepo.BeneficiaryInformationL5) ?? DBNull.Value));
           
            sqlParameterlist.Add(new SqlParameter("@Status", SqlDbType.VarChar, 32));            
            sqlParameterlist.Add(new SqlParameter("@InternalId", SqlDbType.Int, (int)ParameterDirection.Output));


            return sqlParameterlist;


        }


        // Processing Raw Message
        private List<SqlParameter> getRawMessageParameters(FedWireMsgRepo fedWireMsgRepo)
        {
            List<SqlParameter> sqlParameterlist = new List<SqlParameter>();

            fedWireMsgRepo.CreationDate = DateTime.Now;
            fedWireMsgRepo.UpdateDate = DateTime.Now;
            fedWireMsgRepo.UpdatorId = Environment.UserName;
            fedWireMsgRepo.CreatorId = Environment.UserName;
            fedWireMsgRepo.LoadStatus = "Active";
            
            sqlParameterlist.Add(new SqlParameter("MessageId", ((object)fedMessage.MessageId) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("MessageOrigin", ((object)fedMessage.MessageOrigin) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OrigineName", ((object)fedMessage.OriginName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IncomingOutGoing", ((object)fedMessage.IncomingOutGoing) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IMAD", ((object)fedWireMsgRepo.IMAD) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OMAD", ((object)fedWireMsgRepo.OMAD) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InputCycleDate", ((object)fedWireMsgRepo.InputCycleDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InputSource", ((object)fedWireMsgRepo.InputSource) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InputSequenceNumber", ((object)fedWireMsgRepo.InputSequenceNumber) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputCycleDateDateReceived", ((object)fedWireMsgRepo.OutputCycleDateDateReceived) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputDestination", ((object)fedWireMsgRepo.OutputDestination) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputSequenceNumber", ((object)fedWireMsgRepo.OutputSequenceNumber) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputDate", ((object)fedWireMsgRepo.OutputDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("OutputTime", ((object)fedWireMsgRepo.OutputTime) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("FileId", ((object)fedMessage.FileId) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ClientId", ((object)fedWireMsgRepo.SenderABA) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DateReceived", ((object)msgReceivedDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DateLoaded", DateTime.Now));
            sqlParameterlist.Add(new SqlParameter("LoadStatus", ((object)fedWireMsgRepo.LoadStatus) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("LoadRejectReason", ((object)fedWireMsgRepo.LoadRejectReason) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DataRecord", ((object)fedMessage.MessageData) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("CreationDate", ((object)fedWireMsgRepo.CreationDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("CreatorId", ((object)fedWireMsgRepo.CreatorId) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("UpdateDate", ((object)fedWireMsgRepo.UpdateDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("UpdatorId", ((object)fedWireMsgRepo.UpdatorId) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DeletedDate", DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DeleteddBy", DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("@InternalId", SqlDbType.Int, (int)ParameterDirection.Output));

            return sqlParameterlist;
        }


        // Prtocessing Load File 
        private List<SqlParameter> getFileHistoryParameters(DateTime fileDate, int fileZize, int recordRead, int recordProcessed, int recordRejected, string fileContent)
        {
            List<SqlParameter> sqlParameterlist = new List<SqlParameter>();

            byte[] fileByteContents = Encoding.UTF8.GetBytes(fileContent);
            sqlParameterlist.Add(new SqlParameter("FileId", ((object)fedMessage.FileId) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("IncomingOutGoing", ((object)fedMessage.IncomingOutGoing) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("FilePath", ((object)fedMessage.MessageFilePath) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("Filename", ((object)fedMessage.OriginName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InterfaceIdentifier", ((object)fedMessage.InterfaceID) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ClientId",  DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("InterfaceName", "FEDWIRE" ));
            sqlParameterlist.Add(new SqlParameter("InterfaceType",  DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("LoadProcessId",  DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("LoadProcessName",  DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("LoadDate", ((object)msgReceivedDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("ProcessedDate", ((object)msgReceivedDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("BusinessDate", ((object)msgReceivedDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("FileReceivedDate", ((object)fileDate) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("RecordsNumber", ((object)recordRead) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("RecordProcessed", ((object)recordProcessed) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("RecordRejected", ((object)recordRejected) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("FileSize", ((object)fileZize) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("RejectReasonCode", DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("RejectReasonDescription",  DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("LoadStatus", "Active"));
            sqlParameterlist.Add(new SqlParameter("DataFileContent", ((object)fileContent) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("CreationDate", ((object)DateTime.Now) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("CreatedBy", ((object)Environment.UserName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("UpdateDate", ((object)DateTime.Now) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("UpdatedBy", ((object)Environment.UserName) ?? DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DeletedDate",  DBNull.Value));
            sqlParameterlist.Add(new SqlParameter("DeleteddBy",  DBNull.Value));


            sqlParameterlist.Add(new SqlParameter("@InternalId", SqlDbType.Int, (int)ParameterDirection.Output));

            return sqlParameterlist;
        }


    }

}
