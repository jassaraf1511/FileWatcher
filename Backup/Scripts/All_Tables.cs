/*
Line 1: public class PartyInformation  
	Line 18: public class PaymentNotification  
	Line 36: public class RelatedRemittanceInformation  
	Line 66: public class RemittanceDocumentData  
	Line 87: public class RemittanceDocumentInvoice  
	Line 162: public class ServiceMessage  
	Line 173: public class SwiftData  
	Line 215: public class InterfaceLoadHistory  
	Line 247: public class RawMessage  
	Line 268: public class FedMessageByTag  
	Line 352: public class FedMessageWire  
	Line 437: public class AccountInformation  
	Line 454: public class Addenda  
	Line 466: public class OriginatorOptionF  
	Line 483: public class PartyAdviceInformation  

*/


public class PartyInformation  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string TagId { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string PartyInfo { get; set; }
    public string PartyInfoL1 { get; set; }
    public string PartyInfoL2 { get; set; }
    public string PartyInfoL3 { get; set; }
    public string PartyInfoL4 { get; set; }
    public string PartyInfoL5 { get; set; }
 }
public class PaymentNotification  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string TagId { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string NotificationIndicator { get; set; }
    public string ContactEmail { get; set; }
    public string ContactName { get; set; }
    public string ContactPhoneNumber { get; set; }
    public string ContactFaxNumber { get; set; }
    public string ContactMobileNumber { get; set; }
    public string EndToEndIdentification { get; set; }
 }
public class RelatedRemittanceInformation  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string RemittanceIdentification { get; set; }
    public string RemittanceLocationMethod { get; set; }
    public string RemittanceElectronicAddress { get; set; }
    public string RemittanceName { get; set; }
    public string AddressType { get; set; }
    public string Department { get; set; }
    public string SubDepartment { get; set; }
    public string StreetName { get; set; }
    public string BuildingNumber { get; set; }
    public string PostCode { get; set; }
    public string TownName { get; set; }
    public string CountryStateSubDivision { get; set; }
    public string Country { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string AddressLine4 { get; set; }
    public string AddressLine5 { get; set; }
    public string AddressLine6 { get; set; }
    public string AddressLine7 { get; set; }
 }
public class RemittanceDocumentData  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string PrimaryDocumentTypeCode { get; set; }
    public string PrimaryProprietaryDocumentTypeCode { get; set; }
    public string PrimaryDocumentIdentificationNumber { get; set; }
    public string PrimaryIssuer { get; set; }
    public string NegotiatedDiscountCurrency { get; set; }
    public decimal? NegotiatedDiscountAmount { get; set; }
    public string AdjustmentReasonCode { get; set; }
    public string AdjustmentCreditDebitIndicator { get; set; }
    public string AdjustmentCurrencyCode { get; set; }
    public decimal? AdjustmentAmount { get; set; }
    public string AdjustmentAdditionalInformation { get; set; }
 }
public class RemittanceDocumentInvoice  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string OriginatorIdentificationType { get; set; }
    public string OriginatorIdentificationCode { get; set; }
    public string OriginatorName { get; set; }
    public string OriginatorIdentificationNumber { get; set; }
    public string OriginatorIdentificationNumberIssuer { get; set; }
    public string OriginatorDateOfBirth { get; set; }
    public string OriginatorPlaceOfBirth { get; set; }
    public string OriginatorAddressType { get; set; }
    public string OriginatorDepartment { get; set; }
    public string OriginatorSubDepartment { get; set; }
    public string OriginatorStreetName { get; set; }
    public string OriginatorBuildingNumber { get; set; }
    public string OriginatorPostCode { get; set; }
    public string OriginatorTownName { get; set; }
    public string OriginatorCountryStateSubDivision { get; set; }
    public string OriginatorCountry { get; set; }
    public string OriginatorAddressLine1 { get; set; }
    public string OriginatorAddressLine2 { get; set; }
    public string OriginatorAddressLine3 { get; set; }
    public string OriginatorAddressLine4 { get; set; }
    public string OriginatorAddressLine5 { get; set; }
    public string OriginatorAddressLine6 { get; set; }
    public string OriginatorAddressLine7 { get; set; }
    public string OriginatorCountryOfResidence { get; set; }
    public string OriginatorContactName { get; set; }
    public string OriginatorContactPhoneNumber { get; set; }
    public string OriginatorContactMobileNumber { get; set; }
    public string OriginatorContactFaxNumber { get; set; }
    public string OriginatorContactElectronicAddress { get; set; }
    public string OriginatorContactOtherInfo { get; set; }
    public string BeneficiaryIdentificationType { get; set; }
    public string BeneficiaryIdentificationCode { get; set; }
    public string BeneficiaryName { get; set; }
    public string BeneficiaryIdentificationNumber { get; set; }
    public string BeneficiaryIdentificationNumberIssuer { get; set; }
    public string BeneficiaryDateOfbirth { get; set; }
    public string BeneficiaryPlaceOfBirth { get; set; }
    public string BeneficiaryAddressType { get; set; }
    public string BeneficiaryDepartment { get; set; }
    public string BeneficiarySubDepartment { get; set; }
    public string BeneficiaryStreetName { get; set; }
    public string BeneficiaryBuildingNumber { get; set; }
    public string BeneficiaryPostCode { get; set; }
    public string BeneficiaryTownName { get; set; }
    public string BeneficiaryCountryStateSubDivision { get; set; }
    public string BeneficiaryCountry { get; set; }
    public string BeneficiaryAddressLine1 { get; set; }
    public string BeneficiaryAddressLine2 { get; set; }
    public string BeneficiaryAddressLine3 { get; set; }
    public string BeneficiaryAddressLine4 { get; set; }
    public string BeneficiaryAddressLine5 { get; set; }
    public string BeneficiaryAddressLine6 { get; set; }
    public string BeneficiaryAddressLine7 { get; set; }
    public string BeneficiaryCountryOfResidence { get; set; }
    public string ActualAmountPaidCurrency { get; set; }
    public decimal? ActualAmountPaid { get; set; }
    public string GrossAmountCurrency { get; set; }
    public decimal? GrossAmount { get; set; }
    public string SecondaryDocumentTypeCode { get; set; }
    public string SecondaryProprietaryDocumentTypeCode { get; set; }
    public string SecondaryDocumentIdentificationNumber { get; set; }
    public string SecondaryIssuer { get; set; }
    public string SecondaryFreeTextL1 { get; set; }
    public string SecondaryFreeTextL2 { get; set; }
    public string SecondaryFreeTextL3 { get; set; }
 }
public class ServiceMessage  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string MessageInformation { get; set; }
 }
public class SwiftData  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string InstructedCurrency { get; set; }
    public decimal? InstructedAmount { get; set; }
    public string OrderingCustomerL1 { get; set; }
    public string OrderingCustomerL2 { get; set; }
    public string OrderingCustomerL3 { get; set; }
    public string OrderingCustomerL4 { get; set; }
    public string OrderingCustomerL5 { get; set; }
    public string IntermediaryInstitutionL1 { get; set; }
    public string IntermediaryInstitutionL2 { get; set; }
    public string IntermediaryInstitutionL3 { get; set; }
    public string IntermediaryInstitutionL4 { get; set; }
    public string IntermediaryInstitutionL5 { get; set; }
    public string AccountwithInstitutionL1 { get; set; }
    public string AccountwithInstitutionL2 { get; set; }
    public string AccountwithInstitutionL3 { get; set; }
    public string AccountwithInstitutionL4 { get; set; }
    public string AccountwithInstitutionL5 { get; set; }
    public string BeneficiaryCustomerL1 { get; set; }
    public string BeneficiaryCustomerL2 { get; set; }
    public string BeneficiaryCustomerL3 { get; set; }
    public string BeneficiaryCustomerL4 { get; set; }
    public string BeneficiaryCustomerL5 { get; set; }
    public string RemittanceInformationL1 { get; set; }
    public string RemittanceInformationL2 { get; set; }
    public string RemittanceInformationL3 { get; set; }
    public string RemittanceInformationL4 { get; set; }
    public string RemittanceInformationL5 { get; set; }
    public string SendertoReceiverInformationL1 { get; set; }
    public string SendertoReceiverInformationL2 { get; set; }
    public string SendertoReceiverInformationL3 { get; set; }
    public string SendertoReceiverInformationL4 { get; set; }
    public string SendertoReceiverInformationL5 { get; set; }
 }
public class InterfaceLoadHistory  
{  
    public decimal FileId { get; set; }
    public string IncomingOutGoing { get; set; }
    public string Filename { get; set; }
    public string InterfaceIdentifier { get; set; }
    public string ClientId { get; set; }
    public string InterfaceName { get; set; }
    public string InterfaceType { get; set; }
    public string LoadProcessId { get; set; }
    public string LoadProcessName { get; set; }
    public DateTime? LoadDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public DateTime? BusinessDate { get; set; }
    public string FileOrigin { get; set; }
    public string FileDestination { get; set; }
    public string FileArchived { get; set; }
    public DateTime? FileReceivedDate { get; set; }
    public int? RecordsNumber { get; set; }
    public int? RecordProcessed { get; set; }
    public int? RecordRejected { get; set; }
    public int? FileSize { get; set; }
    public string RejectReasonCode { get; set; }
    public string RejectReasonDescription { get; set; }
    public string LoadStatus { get; set; }
    public string UserId { get; set; }
    public byte[] DataFileContent { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreatorId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UpdatorId { get; set; }
 }
public class RawMessage  
{  
    public decimal InternalId { get; set; }
    public string MessageId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public decimal? FileId { get; set; }
    public string ClientId { get; set; }
    public DateTime? DateReceived { get; set; }
    public DateTime? DateLoaded { get; set; }
    public string LoadStatus { get; set; }
    public string LoadRejectReason { get; set; }
    public string UserId { get; set; }
    public string DataRecord { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreatorId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UpdatorId { get; set; }
 }
public class FedMessageByTag  
{  
    public decimal InternalId { get; set; }
    public string MessageId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public decimal? FileId { get; set; }
    public string ClientId { get; set; }
    public DateTime? DateReceived { get; set; }
    public DateTime? DateLoaded { get; set; }
    public string OutputCycleDate { get; set; }
    public string OutputDestinationId { get; set; }
    public string OutputSequenceNumber { get; set; }
    public string OutputDateDateMMDD { get; set; }
    public string OutputTimeHHMM { get; set; }
    public string OutputApplicationID { get; set; }
    public string SenderSuppliedInformation { get; set; }
    public string TypeSubType { get; set; }
    public string InputMessageAccountabilityData { get; set; }
    public string SenderDI { get; set; }
    public string ReceiverDI { get; set; }
    public string BusinessFunctionCode { get; set; }
    public string SenderReference { get; set; }
    public string PreviousMessageId { get; set; }
    public string LocalInstrument { get; set; }
    public string PaymentNotification { get; set; }
    public string Charges { get; set; }
    public string InstructedAmount { get; set; }
    public string ExchangeRate { get; set; }
    public string IntermediaryFI { get; set; }
    public string BeneficiaryFI { get; set; }
    public string Beneficiary { get; set; }
    public string ReferenceforBeneficiary { get; set; }
    public string AccountDebitedinDrawdown { get; set; }
    public string Originator { get; set; }
    public string OriginatorOptionF { get; set; }
    public string OriginatorFI { get; set; }
    public string InstructingFI { get; set; }
    public string AccountCreditedinDrawdown { get; set; }
    public string OriginatortoBeneficiaryInformation { get; set; }
    public string ReceiverFIInformation { get; set; }
    public string DrawdownDebitAccountAdviceInformation { get; set; }
    public string IntermediaryFIInformation { get; set; }
    public string IntermediaryFIAdviceInformation { get; set; }
    public string BeneficiarysFIInformation { get; set; }
    public string BeneficiarysFIAdviceInformation { get; set; }
    public string BeneficiaryInformation { get; set; }
    public string BeneficiaryAdviceInformation { get; set; }
    public string MethodofPaymenttoBeneficiary { get; set; }
    public string FItoFIInformation { get; set; }
    public string SwiftB33BCurrencyInstructedAmount { get; set; }
    public string SwiftB50AOrderingCustomer { get; set; }
    public string SwiftB52AOrderingInstitution { get; set; }
    public string SwiftB56AIntermediaryInstitution { get; set; }
    public string SwiftB57AAccountwithInstitution { get; set; }
    public string SwiftB59ABeneficiaryCustomer { get; set; }
    public string SwiftB70RemittanceInformation { get; set; }
    public string SwiftB72SendertoReceiverInformation { get; set; }
    public string UnstructuredAddendaInformation { get; set; }
    public string RelatedRemittanceInformation { get; set; }
    public string RemittanceOriginatorInfo { get; set; }
    public string RemittanceBeneficiaryInfo { get; set; }
    public string ActualAmountPaid { get; set; }
    public string GrossAmountofRemittanceDocument { get; set; }
    public string SecondaryRemittanceDocumentInformation { get; set; }
    public string RemittanceFreeText { get; set; }
    public string PrimaryRemittanceDocumentInformation { get; set; }
    public string AmountofNegotiatedDiscount { get; set; }
    public string AdjustmentInformationDetails { get; set; }
    public string DateofRemittanceDocument { get; set; }
    public string ServiceMessageInformation { get; set; }
    public string MessageDisposition { get; set; }
    public string ReceiptTimeStamp { get; set; }
    public string OutputMessageAccountabilityData { get; set; }
    public string Error { get; set; }
    public string LoadStatus { get; set; }
    public string LoadRejectReason { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreatorId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UpdatorId { get; set; }
 }
public class FedMessageWire  
{  
    public decimal InternalId { get; set; }
    public string MessageId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public decimal? FileId { get; set; }
    public string ClientId { get; set; }
    public DateTime? DateReceived { get; set; }
    public DateTime? DateLoaded { get; set; }
    public string SenderFormatVersion { get; set; }
    public string SenderUserRequestCorrelation { get; set; }
    public string SenderProductionCode { get; set; }
    public string SenderMessageDuplicationCode { get; set; }
    public string MessageType { get; set; }
    public string MessageTypeSubType { get; set; }
    public string ImadCycleDate { get; set; }
    public string ImadSource { get; set; }
    public string ImadSequenceNumber { get; set; }
    public decimal? Amount { get; set; }
    public string SenderABANumber { get; set; }
    public string SenderShortName { get; set; }
    public string ReceiverABANumber { get; set; }
    public string ReceiverShortName { get; set; }
    public string BusinessFunctionCode { get; set; }
    public string SenderReference { get; set; }
    public string PreviousMessageId { get; set; }
    public string LocalInstrumentCode { get; set; }
    public string ProprietaryCode { get; set; }
    public string ChargesDetail { get; set; }
    public string ChargeCurrency1 { get; set; }
    public decimal? ChargeAmount1 { get; set; }
    public string ChargeCurrency2 { get; set; }
    public decimal? ChargeAmount2 { get; set; }
    public string ChargeCurrency3 { get; set; }
    public decimal? ChargeAmount3 { get; set; }
    public string ChargeCurrency4 { get; set; }
    public decimal? ChargeAmount4 { get; set; }
    public string InstructedCurrency { get; set; }
    public decimal? InstructedAmount { get; set; }
    public decimal? ExchangeRate { get; set; }
    public string ReferenceforBeneficiary { get; set; }
    public string OriginatorOptionFPartyIdentifier { get; set; }
    public string OriginatorOptionFPartyUniqueIdentifier { get; set; }
    public string OriginatorOptionFPartyNameCode { get; set; }
    public string OriginatorOptionFPartyName { get; set; }
    public string AccountCreditedinDrawDownABA { get; set; }
    public string OriginatortoBeneficiaryInfL1 { get; set; }
    public string OriginatortoBeneficiaryInfL2 { get; set; }
    public string OriginatortoBeneficiaryInfL3 { get; set; }
    public string OriginatortoBeneficiaryInfL4 { get; set; }
    public string MethodOfPaymentCode { get; set; }
    public string MethodOfPaymentAdditionalInformation { get; set; }
    public string FItoFIInformationL1 { get; set; }
    public string FItoFIInformationL2 { get; set; }
    public string FItoFIInformationL3 { get; set; }
    public string FItoFIInformationL4 { get; set; }
    public string FItoFIInformationL5 { get; set; }
    public string FItoFIInformationL6 { get; set; }
    public DateTime? DateofRemittanceDocument { get; set; }
    public string MessageDispositionFormatVersion { get; set; }
    public string MessageDispositionProductionCode { get; set; }
    public string MessageDispositionMessageDuplicationCode { get; set; }
    public string MessageDispositionStatusIndicator { get; set; }
    public string ReceiptDateMMDD { get; set; }
    public string ReceiptTimeHHMM { get; set; }
    public string ReceiptApplicationID { get; set; }
    public string OmadCycleDate { get; set; }
    public string OmadDestinationId { get; set; }
    public string OmadOutputSequenceNumber { get; set; }
    public string OmadOutputDateDateMMDD { get; set; }
    public string OmadOutputTimeHHMM { get; set; }
    public string OmadOutputApplicationID { get; set; }
    public string ErrorCatecoryCode { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorDescription { get; set; }
    public string LoadStatus { get; set; }
    public string LoadRejectReason { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreatorId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UpdatorId { get; set; }
 }
public class AccountInformation  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public string TagId { get; set; }
    public int? SequenceNumber { get; set; }
    public string AccountIdCode { get; set; }
    public string AccountIdentifier { get; set; }
    public string AccountName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
 }
public class Addenda  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public int? AddendaLength { get; set; }
    public string AddendaInformation { get; set; }
 }
public class OriginatorOptionF  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string TagId { get; set; }
    public string MessageId { get; set; }
    public int? SequenceNumber { get; set; }
    public string OrginatorOptionFCode1 { get; set; }
    public string OrginatorOptionFLine1 { get; set; }
    public string OrginatorOptionFCode2 { get; set; }
    public string OrginatorOptionFLine2 { get; set; }
    public string OrginatorOptionFCode3 { get; set; }
    public string OrginatorOptionFLine3 { get; set; }
 }
public class PartyAdviceInformation  
{  
    public decimal InternalId { get; set; }
    public string MessageReference { get; set; }
    public string MessageOrigin { get; set; }
    public string OrigineName { get; set; }
    public string IncomingOutGoing { get; set; }
    public string MessageId { get; set; }
    public string TagId { get; set; }
    public int? SequenceNumber { get; set; }
    public string AdviceCode { get; set; }
    public string AdviceInfo { get; set; }
    public string AdviceInfoL1 { get; set; }
    public string AdviceInfoL2 { get; set; }
    public string AdviceInfoL3 { get; set; }
    public string AdviceInfoL4 { get; set; }
 }
Completion time: 2020-10-30T13:35:41.5800459-04:00