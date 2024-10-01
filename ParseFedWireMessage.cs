using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using Cfsb.Incoming.FedWires.Utils;
using Cfsb.LoggerWriter.Services;
using Cfsb.Incoming.FedWires.DataEntities;
using Cfsb.Incoming.FedWires.MessageStructure;

namespace Cfsb.Incoming.FedWires.Utils
{
    public class ParseFedWireMessage
    {
        private WireOutputMessage outputMessage;
        private FedWireMessage fedMessage;
        private string outputCsvRecord;
        private string interfaceID;
        private LogWriter log;
        private string messageInOut;

        public string OutputCsvRecord { get => outputCsvRecord; set => outputCsvRecord = value; }
        public FedWireMessage FedMessage { get => fedMessage; set => fedMessage = value; }
        public LogWriter Log { get => log; set => log = value; }
        public string MessageInOut { get => messageInOut; set => messageInOut = value; }

        public ParseFedWireMessage()
        {
            messageInOut = string.Empty;
        }

        public ParseFedWireMessage(LogWriter log)
        {
            this.Log = log;
        }

        private Dictionary<string, WireTagData> processLine(string message)
        {
            string[] array = message.Split('{');

            Dictionary<string, WireTagData> dataList = new Dictionary<string, WireTagData>();
            try
            {
                foreach (string tagLine in array)
                {
                    try
                    {
                        WireTagData wireTagData = getWeireData(tagLine);

                        if (wireTagData != null && !dataList.ContainsKey(wireTagData.TagId))
                        {
                            dataList.Add(wireTagData.TagId, wireTagData);
                        }
                    }
                    catch (Exception exFor)
                    {
                        log.WriteToLog("Wire Data Exception Data : " + tagLine);
                        log.WriteToLog(exFor.Message);
                        log.WriteToLog(exFor.StackTrace);

                        continue;
                    }


                }

            }
            catch (Exception ex)
            {
                log.WriteToLog(ex.Message);
                log.WriteToLog(ex.StackTrace);
                log.WriteToLog(message);
            }

            return dataList;

        }

        public Dictionary<string, WireTagData> ParseMessage(string wireMessage)
        {
            
            initializeMessage();

            if (string.IsNullOrEmpty(wireMessage)) return null;
            string messageLine = wireMessage;
            interfaceID = " ";
            messageInOut = "WIREIN";
            try
            {

                messageLine = wireMessage.StartsWith("{") ? wireMessage : wireMessage.Substring(8);
                if (!wireMessage.StartsWith("{"))
                {
                       messageInOut = "WIREOUT";
                       interfaceID = wireMessage.Substring(0, 7);
                }
            }
            catch (Exception ex)
            {
                log.WriteToLog(ex.Message);
                log.WriteToLog(ex.StackTrace);
                log.WriteToLog(wireMessage);
            }
            Dictionary<string, WireTagData> dictTagList = processLine(messageLine);

            outputMessage = new WireOutputMessage();
            //PropertyExtensions.InitializePropery(outputMessage);

                    
          
            fedMessage.MessageData = wireMessage;
            if (dictTagList != null && dictTagList.Count > 0)
            {
                analyszeMessageTagList(dictTagList);
            }

            return dictTagList;
        }
        private WireTagData getWeireData(string tagData)
        {
            WireTagData wireTagData = new WireTagData();

            if (string.IsNullOrEmpty(tagData)) return null;
            try
            {


                string[] splitArray = tagData.Split("}");
                if (splitArray.Length < 2) return null;
                wireTagData.TagId = splitArray[0];
                wireTagData.TagData = splitArray[1];
            }
            catch (Exception ex)
            {
                Log.WriteToLog(ex.Message);
                Log.WriteToLog(ex.StackTrace);
                Log.WriteToLog(tagData);
            }
            return wireTagData;


        }


        private void analyszeMessageTagList(Dictionary<string, WireTagData> wireTagList)
        {
           
            PropertyExtensions.InitializePropery(outputMessage);
            if (wireTagList == null) return;

            //  fedMessage.MessageKey = Guid.NewGuid().ToString();
            fedMessage.InterfaceID = " ";// interfaceID;
            FedMessage.IncomingOutGoing = messageInOut;
            try
            {
                foreach (KeyValuePair<string, WireTagData> item in wireTagList)
                {
                    try
                    {
                        if (item.Key != null && item.Value != null) checkTagData(item.Value);
                    }
                    catch (Exception exDict)
                    {
                        log.WriteToLog(exDict.Message);
                        log.WriteToLog("message exception on key value "); // + item.Key + item.Value);
                        log.WriteToLog(exDict.StackTrace);
                        log.WriteToLog(exDict.Data.ToString());
                        //  Console.WriteLine(exDict.Message);
                        //   Console.WriteLine("message exception on key value "); // + item.Key + item.Value);
                        //  Console.WriteLine(exDict.StackTrace);
                        //   Console.WriteLine(exDict.Data.ToString());
                        continue;
                    }

                }
            }
            catch (Exception ex)
            {
                log.WriteToLog(ex.Message);
                log.WriteToLog(ex.StackTrace);
                log.WriteToLog(ex.Data.ToString());

            }


            outputCsvRecord = PropertyExtensions.SetCsvRecord(outputMessage);
        }

        private void checkTagData(WireTagData messageLine)
        {
            int tagId = 0;

            if (String.IsNullOrEmpty(messageLine.TagId) || String.IsNullOrEmpty(messageLine.TagData)) return;

            if (!int.TryParse(messageLine.TagId, out tagId)) return;
            switch (tagId)
            {
                case 1100:
                    fedMessage.MessageDisposition = new MessageDispositionStatus();
                    break;

                case 1110:
                    fedMessage.ReceiptTimeStamp = new MessageReceiptTimeStamp(messageLine);
                    break;
                case 1120:
                    AccountabilityData oMadHeader = new AccountabilityData(messageLine);
                    string omad = $"{oMadHeader.InputCycleDate_CCYYMMDD} {oMadHeader.InputSource}  { oMadHeader.InputSequenceNumber} {oMadHeader.MsgReceiptTimeStamp.ReceiptDate}{ oMadHeader.MsgReceiptTimeStamp.ReceiptTime}";
                    fedMessage.OutputMessageAccountabilityData = oMadHeader;
                    fedMessage.Omad = messageLine.TagData;
                    outputMessage.MsgHeader_IMAD = omad;
                    break;
                case 1500:
                    fedMessage.SenderSuppliedInformation = new SenderReceiverInfo(messageLine);
                    break;
                case 1510:
                    fedMessage.TypeSubType = new TypeSubTypeInfo(messageLine);
                    break;
                case 1520:
                    AccountabilityData iMadHeader = new AccountabilityData(messageLine);
                    string imad = $"{iMadHeader.InputCycleDate_CCYYMMDD} {iMadHeader.InputSource}  { iMadHeader.InputSequenceNumber} { iMadHeader.MsgReceiptTimeStamp.ReceiptDate}{ iMadHeader.MsgReceiptTimeStamp.ReceiptTime}";

                    fedMessage.InputMessageAccountabilityData = iMadHeader;
                    fedMessage.Imad = messageLine.TagData;
                    outputMessage.MsgHeader_IMAD = imad;
                    break;

                case 2000:
                    AmountData parsedAmount = new AmountData(messageLine);
                    fedMessage.Amount = parsedAmount;
                    outputMessage.MsgBSC_2000_Amount = String.Format("{0:###,###,###,###.00}", parsedAmount.TransactionAmount);
                    break;
                case 3100:
                    SenderReceiverDI senderDi = new SenderReceiverDI(messageLine);
                    fedMessage.SenderDI = senderDi;
                    outputMessage.MsgBSC_3100_SenderDI = senderDi.SenderReceiverABA + " " + senderDi.SenderReceiverShortName;
                    break;
                case 3320:
                    fedMessage.SenderReference = !String.IsNullOrEmpty(messageLine.TagData) ? messageLine.TagData.Replace("*", "") : null;
                    break;
                case 3400:
                    SenderReceiverDI receiverDi = new SenderReceiverDI(messageLine);
                    fedMessage.ReceiverDI = receiverDi;
                    outputMessage.MsgBSC_3400_RecieverDI = receiverDi.SenderReceiverABA + " " + receiverDi.SenderReceiverShortName;
                    break;
                case 3500:
                    fedMessage.PreviousMessageId = !String.IsNullOrEmpty(messageLine.TagData) ? messageLine.TagData : null;
                    break;
                case 3600:  // Need To split This Function Business Code and Type Code
                    fedMessage.BusinessFunctionCode = !String.IsNullOrEmpty(messageLine.TagData) ? messageLine.TagData.Replace("*", "") : null;
                    break;
                case 3610:
                    fedMessage.LocalInstrument = new LocalInstrument(messageLine);
                    break;
                case 3620:
                    Paymentnotification paymentNotice = new Paymentnotification(messageLine);
                    fedMessage.Paymentnotification = paymentNotice;
                    outputMessage.MsgCHRG_3620_PaymentNotice_Contact = paymentNotice.ContactNotification;
                    outputMessage.MsgCHRG_3620_PaymentNotice_Name = paymentNotice.ContactName;
                    break;
                case 3700:
                    fedMessage.Charges = new Charges(messageLine);
                    break;
                case 3710:
                    fedMessage.InstructedAmount = new AmountWithCurrency(messageLine);
                    break;
                case 3720:
                    string exchangeRateStr = !String.IsNullOrEmpty(messageLine.TagData) ? messageLine.TagData.Replace("*", "").Replace(",", ".") : null;
                    decimal exchangeRate = 0;
                    if (!String.IsNullOrEmpty(exchangeRateStr))
                    {
                        Decimal.TryParse(exchangeRateStr, out exchangeRate);
                    }
                    break;
                case 4000:
                    AccountInformation intrFI = new AccountInformation(messageLine);
                    fedMessage.IntermediaryFI = intrFI;
                    outputMessage.MsgBENE_4000_IntrFI_ID = intrFI.AccountIdentifier;
                    outputMessage.MsgBENE_4000_IntrFI_Name = intrFI.AccountName;
                    outputMessage.MsgBENE_4000_IntrFI_Address1 = getAddressLine(intrFI.AccountAddress, 1);
                    outputMessage.MsgBENE_4000_IntrFI_Address2 = getAddressLine(intrFI.AccountAddress, 2);
                    outputMessage.MsgBENE_4000_IntrFI_Address3 = getAddressLine(intrFI.AccountAddress, 3);
                    break;
                case 4100:
                    AccountInformation beneFi = new AccountInformation(messageLine);
                    fedMessage.BeneficiaryFI = beneFi;
                    outputMessage.MsgBENE_4100_BeneFI_ID = beneFi.AccountIdentifier;
                    outputMessage.MsgBENE_4100_BeneFI_Name = beneFi.AccountName;
                    outputMessage.MsgBENE_4100_BeneFI_Address1 = getAddressLine(beneFi.AccountAddress, 1);
                    outputMessage.MsgBENE_4100_BeneFI_Address2 = getAddressLine(beneFi.AccountAddress, 2);
                    outputMessage.MsgBENE_4100_BeneFI_Address3 = getAddressLine(beneFi.AccountAddress, 3);

                    break;
                case 4200:
                    AccountInformation beneficiary = new AccountInformation(messageLine);
                    fedMessage.Beneficiary = beneficiary;
                    outputMessage.MsgBENE_4200_Bene_ID = beneficiary.AccountIdentifier;
                    outputMessage.MsgBENE_4200_Bene_Name = beneficiary.AccountName;
                    outputMessage.MsgBENE_4200_Bene_Address1 = getAddressLine(beneficiary.AccountAddress, 1);
                    outputMessage.MsgBENE_4200_Bene_Address2 = getAddressLine(beneficiary.AccountAddress, 2);
                    outputMessage.MsgBENE_4200_Bene_Address3 = getAddressLine(beneficiary.AccountAddress, 3);
                    break;
                case 4320:
                    outputMessage.MsgBENE_4320_BeneRef = !String.IsNullOrEmpty(messageLine.TagData) ? messageLine.TagData.Replace("*", "") : "";
                    fedMessage.ReferenceforBeneficiary = !String.IsNullOrEmpty(messageLine.TagData) ? messageLine.TagData.Replace("*", "") : null;
                    break;
                case 4400:
                    AccountInformation beneAcctDrwDwn = new AccountInformation(messageLine);
                    fedMessage.AccountDebitedinDrawdown = beneAcctDrwDwn;
                    outputMessage.MsgBENE_4400_AcctDrwDwn_ID = beneAcctDrwDwn.AccountIdentifier;
                    outputMessage.MsgBENE_4400_AcctDrwDwn_Name = beneAcctDrwDwn.AccountName;
                    outputMessage.MsgBENE_4400_AcctDrwDwn_Address1 = getAddressLine(beneAcctDrwDwn.AccountAddress, 1);
                    outputMessage.MsgBENE_4400_AcctDrwDwn_Address2 = getAddressLine(beneAcctDrwDwn.AccountAddress, 2);
                    outputMessage.MsgBENE_4400_AcctDrwDwn_Address3 = getAddressLine(beneAcctDrwDwn.AccountAddress, 3);
                    break;

                case 5000:
                    AccountInformation originator = new AccountInformation(messageLine);
                    fedMessage.Originator = originator;
                    outputMessage.MsgORIG_5000_Originator_ID = originator.AccountIdentifier;
                    outputMessage.MsgORIG_5000_Originator_Name = originator.AccountName;
                    outputMessage.MsgORIG_5000_Originator_Address1 = getAddressLine(originator.AccountAddress, 1);
                    outputMessage.MsgORIG_5000_Originator_Address2 = getAddressLine(originator.AccountAddress, 2);
                    outputMessage.MsgORIG_5000_Originator_Address3 = getAddressLine(originator.AccountAddress, 3);
                    break;
                case 5010:
                    PartyInformation originatorOpF = new PartyInformation(messageLine);
                    fedMessage.OriginatorOptionF = originatorOpF;
                    if (originatorOpF.SlpitNameAndAdressLines.Length > 0)
                    {
                        outputMessage.MsgORIG_5010_OriginatorOpF_PartyIdentifier =
                            originatorOpF.IdentificationCodeDescription +
                           originatorOpF.SlpitNameAndAdressLines[0];
                    }
                    if (originatorOpF.SlpitNameAndAdressLines.Length > 1)
                        outputMessage.MsgORIG_5010_OriginatorOpF_Name = originatorOpF.SlpitNameAndAdressLines[1];
                    if (originatorOpF.SlpitNameAndAdressLines.Length > 2)
                        outputMessage.MsgORIG_5010_OriginatorOpF_L1 = originatorOpF.SlpitNameAndAdressLines[2];
                    if (originatorOpF.SlpitNameAndAdressLines.Length > 3)
                        outputMessage.MsgORIG_5010_OriginatorOpF_L2 = originatorOpF.SlpitNameAndAdressLines[3];
                    if (originatorOpF.SlpitNameAndAdressLines.Length > 4)
                        outputMessage.MsgORIG_5010_OriginatorOpF_L3 = originatorOpF.SlpitNameAndAdressLines[4];

                    break;
                case 5100:
                    AccountInformation originatorFI = new AccountInformation(messageLine);
                    fedMessage.OriginatorFI = originatorFI;
                    outputMessage.MsgORIG_5100_OriginatorFI_ID = originatorFI.AccountIdentifier;
                    outputMessage.MsgORIG_5100_OriginatorFI_Name = originatorFI.AccountName;
                    outputMessage.MsgORIG_5100_OriginatorFI_Address1 = getAddressLine(originatorFI.AccountAddress, 1);
                    outputMessage.MsgORIG_5100_OriginatorFI_Address2 = getAddressLine(originatorFI.AccountAddress, 2);
                    outputMessage.MsgORIG_5100_OriginatorFI_Address3 = getAddressLine(originatorFI.AccountAddress, 3);
                    break;
                case 5200:
                    AccountInformation instructingFI = new AccountInformation(messageLine);
                    fedMessage.InstructingFI = instructingFI;
                    outputMessage.MsgORIG_5200_InstructingFI_ID = instructingFI.AccountIdentifier;
                    outputMessage.MsgORIG_5200_InstructingFI_Name = instructingFI.AccountName;
                    outputMessage.MsgORIG_5200_InstructingFI_Address1 = getAddressLine(instructingFI.AccountAddress, 1);
                    outputMessage.MsgORIG_5200_InstructingFI_Address2 = getAddressLine(instructingFI.AccountAddress, 2);
                    outputMessage.MsgORIG_5200_InstructingFI_Address3 = getAddressLine(instructingFI.AccountAddress, 3);
                    break;
                case 5400:
                    fedMessage.AccountCreditedinDrawdown = !String.IsNullOrEmpty(messageLine.TagData) ? messageLine.TagData : null;
                    break;
                case 6000:
                    string[] beneficiaryInfo = splitLines(messageLine.TagData);
                    if (beneficiaryInfo == null || beneficiaryInfo.Length < 1) return;
                    fedMessage.OriginatortoBeneficiaryInformation = beneficiaryInfo;
                    outputMessage.MsgORIG_6000_Orig2BeneInfo_L1 = getAddressLine(beneficiaryInfo, 1);
                    outputMessage.MsgORIG_6000_Orig2BeneInfo_L2 = getAddressLine(beneficiaryInfo, 2);
                    outputMessage.MsgORIG_6000_Orig2BeneInfo_L3 = getAddressLine(beneficiaryInfo, 3);
                    outputMessage.MsgORIG_6000_Orig2BeneInfo_L4 = getAddressLine(beneficiaryInfo, 4);

                    break;
                case 6100:
                    // outputMessage.MsgFI2FI_6100_RcvrFIInfo = messageLine.TagData.Replace("*", "");
                    fedMessage.ReceiverFIInformation = new PartyTransactionFIInformation(messageLine);
                    outputMessage.MsgFI2FI_6100_RcvrFIInfo = formatTagArray(messageLine.TagData);
                    break;
                case 6110:
                    fedMessage.DrawdownDebitAccountAdviceInformation = new AccountAdviceInformation(messageLine);
                    break;
                case 6200:
                    fedMessage.IntermediaryFIInformation = new PartyTransactionFIInformation(messageLine);
                    break;
                case 6210:
                    fedMessage.IntermediaryFIAdviceInformation = new AccountAdviceInformation(messageLine);
                    break;
                case 6300:
                    fedMessage.BeneficiarysFIInformation = new PartyTransactionFIInformation(messageLine);
                    outputMessage.MsgFI2FI_6300_BeneFIInfo = formatTagArray(messageLine.TagData);
                    break;
                case 6310:
                    fedMessage.BeneficiarysFIAdviceInformation = new AccountAdviceInformation(messageLine);
                    break;
                case 6400:
                    fedMessage.BeneficiaryInformation = new PartyTransactionFIInformation(messageLine);
                    outputMessage.MsgFI2FI_6400_BeneInfo = formatTagArray(messageLine.TagData);
                    break;
                case 6410:
                    fedMessage.BeneficiaryAdviceInformation = new AccountAdviceInformation(messageLine);
                    outputMessage.MsgFI2FI_6410_BeneAdvc_Addl = formatTagArray(messageLine.TagData);
                    break;
                case 6420:
                    fedMessage.MethodofPaymenttoBeneficiary = new MethodPayment();
                    outputMessage.MsgFI2FI_6410_BeneAdvc_Addl = formatTagArray(messageLine.TagData);
                    break;
                case 6500:
                    fedMessage.FItoFIInformation = splitLines(messageLine.TagData);
                    outputMessage.MsgFI2FI_6500_FItoFI = formatTagArray(messageLine.TagData);
                    break;
                case 7033:
                    //           log.WriteToLog("SWIFT B33 " + messageLine.TagData);
                    fedMessage.SwiftB33BCurrencyInstructedAmount = new SwiftCurrencyInstructedAmount(messageLine);
                    break;
                case 7050:
                    //      log.WriteToLog("SWIFT B7050 " + messageLine.TagData);
                    fedMessage.SwiftB50AOrderingCustomer = new SwiftTagPaymentInformation(messageLine);
                    break;
                case 7052:
                    //    log.WriteToLog("SWIFT B7052 " + messageLine.TagData);
                    fedMessage.SwiftB52AOrderingInstitution = new SwiftTagPaymentInformation(messageLine);
                    break;
                case 7056:
                    //  log.WriteToLog("SWIFT B7056 " + messageLine.TagData);
                    fedMessage.SwiftB56AIntermediaryInstitution = new SwiftTagPaymentInformation(messageLine);
                    break;
                case 7057:
                    //log.WriteToLog("SWIFT B7057 " + messageLine.TagData);
                    fedMessage.SwiftB57AAccountwithInstitution = new SwiftTagPaymentInformation(messageLine);
                    break;
                case 7059:
                    // log.WriteToLog("SWIFT 7059 " + messageLine.TagData);
                    fedMessage.SwiftB59ABeneficiaryCustomer = new SwiftTagPaymentInformation(messageLine);
                    break;
                case 7070:
                    //  log.WriteToLog("SWIFT B7070 " + messageLine.TagData);
                    fedMessage.SwiftB70RemittanceInformation = new SwiftTagPaymentInformation(messageLine);
                    break;
                case 7072:
                    //log.WriteToLog("SWIFT B7072 " + messageLine.TagData);
                    fedMessage.SwiftB72SendertoReceiverInformation = new SwiftTagPaymentInformation(messageLine);
                    break;
                case 8200:
                    fedMessage.UnstructuredAddendaInformation = new AddendaInformation(messageLine);
                    break;
                case 9000:
                    break;

                default:
                    fedMessage.Miscellaneous+= messageLine.TagId + "|" + messageLine.TagData;
                    break;
            }
        }
        private string getAddressLine(string[] addresseDetail, int lineNumber)
        {
            string addressLine = string.Empty;
            int index = lineNumber - 1;
            if (addresseDetail == null) return addressLine;
            return addresseDetail.Length >= lineNumber ? addresseDetail[index] : addressLine;
        }

        private string formatTagArray(string tagLine)
        {
            if (string.IsNullOrEmpty(tagLine)) return "";

            string[] strArray = tagLine.Split("*");
            if (strArray.Length == 0) return tagLine;

            StringBuilder sb = new StringBuilder();
            int i = 0;

            foreach (string str in strArray)
            {

                switch (i)
                {
                    case 0:
                        sb.AppendLine("Line 1:\t" + str);
                        break;
                    case 1:
                        sb.AppendLine("Additional Information:\t" + str);
                        break;

                    default:
                        sb.Append(str);
                        break;
                }
                i++;

            }

            return sb.ToString();
        }

        private string[] splitLines(string tagLine)
        {
            if (string.IsNullOrEmpty(tagLine)) return null;

            string[] strArray = tagLine.Split("*");
            if (strArray.Length == 0) return null;

            return strArray;
        }
        private void initializeMessage()
        {

            fedMessage = new FedWireMessage();
            PropertyExtensions.InitializePropery(fedMessage);

            fedMessage.SenderSuppliedInformation = new SenderReceiverInfo();
            fedMessage.TypeSubType = new TypeSubTypeInfo();
            fedMessage.InputMessageAccountabilityData = new AccountabilityData();
            fedMessage.Amount = new AmountData();
            fedMessage.SenderDI = new SenderReceiverDI(); fedMessage.ReceiverDI = new SenderReceiverDI();
            fedMessage.LocalInstrument = new LocalInstrument();
            fedMessage.Paymentnotification = new Paymentnotification();
            fedMessage.Charges = new Charges();
            fedMessage.InstructedAmount = new AmountWithCurrency();            
            fedMessage.IntermediaryFI = new AccountInformation();
            fedMessage.BeneficiaryFI = new AccountInformation();
            fedMessage.Beneficiary = new AccountInformation();
            fedMessage.AccountDebitedinDrawdown = new AccountInformation();
            fedMessage.Originator = new AccountInformation();
            fedMessage.OriginatorOptionF = new PartyInformation();
            fedMessage.OriginatorFI = new AccountInformation();
            fedMessage.InstructingFI = new AccountInformation();
            fedMessage.ReceiverFIInformation = new PartyTransactionFIInformation();
            fedMessage.DrawdownDebitAccountAdviceInformation = new AccountAdviceInformation();
            fedMessage.IntermediaryFIInformation = new PartyTransactionFIInformation();
            fedMessage.IntermediaryFIAdviceInformation = new AccountAdviceInformation();
            fedMessage.BeneficiarysFIInformation = new PartyTransactionFIInformation();
            fedMessage.BeneficiarysFIAdviceInformation = new AccountAdviceInformation();
            fedMessage.BeneficiaryInformation = new PartyTransactionFIInformation();
            fedMessage.BeneficiaryAdviceInformation = new AccountAdviceInformation();
            fedMessage.MethodofPaymenttoBeneficiary = new MethodPayment();
            fedMessage.SwiftB33BCurrencyInstructedAmount = new SwiftCurrencyInstructedAmount();
            fedMessage.SwiftB50AOrderingCustomer = new SwiftTagPaymentInformation();
            fedMessage.SwiftB52AOrderingInstitution = new SwiftTagPaymentInformation();
            fedMessage.SwiftB56AIntermediaryInstitution = new SwiftTagPaymentInformation();
            fedMessage.SwiftB57AAccountwithInstitution = new SwiftTagPaymentInformation();
            fedMessage.SwiftB59ABeneficiaryCustomer = new SwiftTagPaymentInformation();
            fedMessage.SwiftB70RemittanceInformation = new SwiftTagPaymentInformation();
            fedMessage.SwiftB72SendertoReceiverInformation = new SwiftTagPaymentInformation();
            fedMessage.UnstructuredAddendaInformation = new AddendaInformation();
            fedMessage.RelatedRemittanceInformation = new RemittanceInformation();
            fedMessage.RemittanceOriginatorInfo = new RemittamceOriginator();
            fedMessage.RemittanceBeneficiaryInfo = new RemittanceBeneficiary();
            fedMessage.ActualAmountPaid = new AmountWithCurrency();
            fedMessage.GrossAmountofRemittanceDocument = new AmountWithCurrency();
            fedMessage.SecondaryRemittanceDocumentInformation = new RemitanceDocument();
            fedMessage.PrimaryRemittanceDocumentInformation = new RemitanceDocument();
            fedMessage.AmountofNegotiatedDiscount = new AmountWithCurrency();
            fedMessage.AdjustmentInformationDetails = new AdjustementInformation();
            fedMessage.MessageDisposition = new MessageDispositionStatus();
            fedMessage.ReceiptTimeStamp = new MessageReceiptTimeStamp();
            fedMessage.OutputMessageAccountabilityData = new AccountabilityData();
            fedMessage.Error = new MessageError();
            fedMessage.OriginatorOptionF.PartyDetails = new AccountAndPartyInformation();
            fedMessage.OutputMessageAccountabilityData.MsgReceiptTimeStamp = new MessageReceiptTimeStamp();
        }
    }
}
