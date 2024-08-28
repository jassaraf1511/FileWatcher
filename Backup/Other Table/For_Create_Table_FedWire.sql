USE [FundWireInterfaceDB]
GO

/****** Object:  Table [dbo].[FedMessageWire]    Script Date: 10/27/2020 3:53:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FedMessageWire](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,  --- File Or MQ
	[OrigineName] [varchar](64) NOT NULL,    --- Fie ID or Queue Name
	[IncomingOutGoing] [varchar](16) NOT NULL, --- Message  Incoming - Outgoing
	[MessageId] [varchar](32) NOT NULL,
	[SenderFormatVersion] [varchar](2) NULL,
	[SenderUserRequestCorrelation] [varchar](8) NULL,
	[SenderProductionCode] [varchar](1) NULL,
	[SenderMessageDuplicationCode] [varchar](1) NULL,
	[MessageType] [varchar](2) NULL,
	[MessageTypeSubType] [varchar](2) NULL,
	[ImadCycleDate] [varchar](8) NULL,
	[ImadSource] [varchar](8) NULL,
	[ImadSequenceNumber] [varchar](6) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SenderABANumber] [varchar](9) NULL,
	[SenderShortName] [varchar](18) NULL,
	[ReceiverABANumber] [varchar](9) NULL,
	[ReceiverShortName] [varchar](18) NULL,
	[BusinessFunctionCode] [varchar](3) NULL,
	[SenderReference] [varchar](16) NULL,
	[PreviousMessageId] [varchar](22) NULL,
	[LocalInstrumentCode] [varchar](4) NULL,
	[ProprietaryCode] [varchar](35) NULL,
	[PaymentNotificationId] [numeric](18, 0) NULL,
	
	[ChargesDetail] [varchar](1) NULL,
	[ChargeCurrency1] [varchar](3) NULL,
	[ChargeAmount1] [decimal](18, 2) NULL,
	[ChargeCurrency2] [varchar](3) NUL,
	[ChargeAmount2] [decimal](18, 2) NULL,
	[ChargeCurrency3] [varchar](3) NULL,
	[ChargeAmount3] [decimal](18, 2) NULL,	
	[ChargeCurrency4] [varchar](3) NULL,
	[ChargeAmount4] [decimal](18, 2) NULL,	
	
	[InstructedCurrency] [varchar](3) NULL,
	[InstructedAmount] [decimal](18, 2) NULL,
	[ExchangeRate] [decimal](18, 15) NULL,
	
	[IntermediaryFIId] [numeric](18, 0) NULL,	// FedAccountInformation
	[BeneficiaryFIId] [numeric](18, 0) NULL,	// FedAccountInformation
	[BeneficiaryId]   [numeric](18, 0) NULL,	// FedAccountInformation
	[ReferenceforBeneficiary] [varchar](16) NULL,
	
	[DebitedinDrawdownAccountId] [numeric](18, 0) NULL,	// FedAccountInformation
	[OriginatorAccountId] [numeric](18, 0) NULL,	// FedAccountInformation
		
	// See Tag 5010
	[OriginatorOptionFPartyIdentifier] [varchar](4) NULL,
	[OriginatorOptionFPartyUniqueIdentifier] [varchar](31) NULL,
	[OriginatorOptionFPartyNameCode] [varchar](1) NULL,
	[OriginatorOptionFPartyName] [varchar](34) NULL,
	[OrginatorOptionFCode1] [varchar](1) NULL,
	[OrginatorOptionFLine1] [varchar](34) NULL,	
	[OrginatorOptionFCode2] [varchar](1) NULL,
	[OrginatorOptionFLine2] [varchar](34) NULL,	
	[OrginatorOptionFCode3] [varchar](1) NULL,
	[OrginatorOptionFLine3] [varchar](34) NULL,	

	[OriginatorFIId] [numeric](18, 0) NULL,	// FedAccountInformation
	[InstructingFIId] [numeric](18, 0) NULL,	// FedAccountInformation
		
	[AccountCreditedinDrawDownABA] [varchar](9) NULL,
	[OriginatortoBeneficiaryInfL1] [varchar](35) NULL,
	[OriginatortoBeneficiaryInfL2] [varchar](35) NULL,
	[OriginatortoBeneficiaryInfL3] [varchar](35) NULL,
	[OriginatortoBeneficiaryInfL4] [varchar](35) NULL,
	
	[ReceiverFIInformationId] [numeric](18, 0) NULL,	// FedPartyInformation	
	[DrawDownDBAcctAdviceId] [numeric](18, 0) NULL,	// FedPartyAdviceInformation

	[IntermediaryFIInformationId] [numeric](18, 0) NULL,	// FedPartyInformation
	[IntermediaryFIAdviceId] [numeric](18, 0) NULL,	// FedPartyAdviceInformation
	
	[BeneficiaryFIInformationId] [numeric](18, 0) NULL,	// FedPartyInformation
	[BeneficiarysFIAdviceId] [numeric](18, 0) NULL,	// FedPartyAdviceInformation

	[BeneficiaryIInformationId] [numeric](18, 0) NULL,	// FedPartyInformation
	[BeneficiaryFIAdviceId] [numeric](18, 0) NULL,	// FedPartyAdviceInformation
		
	[MethodOfPaymentCode] [varchar](5) NULL,
	[MethodOfPaymentAdditionalInformation] [varchar](30) NULL,
	
	[FItoFIInformationL1] [varchar](35) NULL,
	[FItoFIInformationL2] [varchar](35) NULL,	
	[FItoFIInformationL3] [varchar](35) NULL,
	[FItoFIInformationL4] [varchar](35) NULL,	
	[FItoFIInformationL5] [varchar](35) NULL,
	[FItoFIInformationL6] [varchar](35) NULL,	
	
	
--	[SwiftInstructedAmountTag33B] [varchar](5) NULL,
	[SwiftInstructedCurrency] [varchar](3) NULL,
	[SwiftInstructedAmount] [decimal](18, 2) NULL,
--	[SwiftB50ATag] [varchar](5) NULL,
	[SwiftOrderingCustomer] [varchar](175) NULL,
--	[SwiftTagB52] [varchar](5) NULL,
	[SwiftOrderingInstitution] [varchar](175) NULL,
--	[SwiftTagB56] [varchar](5) NULL,
	[SwiftIntermediaryInstitution] [varchar](175) NULL,
	[SwiftTagB57] [varchar](5) NULL,
	[SwiftAccountwithInstitution] [varchar](175) NULL,
	[SwiftTagB59] [varchar](5) NULL,
	[SwiftBeneficiaryCustomer] [varchar](175) NULL,
	[SwiftTagB70] [varchar](5) NULL,
	[SwiftRemittanceInformation] [varchar](175) NULL,
	[SwiftTagB72] [varchar](5) NULL,
	[SwiftSendertoReceiverInformation] [varchar](175) NULL,
	[AddendaLength] [int] NULL,
	[AddendaInformation] [varchar](max) NULL,
	[RemitanceIdentification] [varchar](35) NULL,
	[RemittanceLocationMethod] [varchar](4) NULL,
	[RemittanceElectronicAddress] [varchar](2048) NULL,
	[RemittanceName] [varchar](140) NULL,
	[AddressType] [varchar](4) NULL,
	[Department] [varchar](70) NULL,
	[SubDepartment] [varchar](70) NULL,
	[StreetName] [varchar](70) NULL,
	[BuildingNumber] [varchar](16) NULL,
	[PostCode] [varchar](16) NULL,
	[TownName] [varchar](35) NULL,
	[CountryStateSubDivision] [varchar](35) NULL,
	[Country] [varchar](2) NULL,
	[AddressLine] [varchar](490) NULL,
	[RemittanceOriginatorIdentificationType] [varchar](2) NULL,
	[RemittanceOriginatorIdentificationCode] [varchar](4) NULL,
	[RemittanceOriginatorName] [varchar](140) NULL,
	[RemittanceOriginatorIdentificationNumber] [varchar](35) NULL,
	[RemittanceOriginatorIdentificationNumberIssuer] [varchar](35) NULL,
	[RemittanceOriginatorDateOfBirth] [varchar](8) NULL,
	[RemittanceOriginatorPlaceOfBirth] [varchar](74) NULL,
	[RemittanceOriginatorAddressType] [varchar](4) NULL,
	[RemittanceOriginatorDepartment] [varchar](70) NULL,
	[RemittanceOriginatorSubDepartment] [varchar](70) NULL,
	[RemittanceOriginatorStreetName] [varchar](70) NULL,
	[RemittanceOriginatorBuildingNumber] [varchar](16) NULL,
	[RemittanceOriginatorPostCode] [varchar](16) NULL,
	[RemittanceOriginatorTownName] [varchar](35) NULL,
	[RemittanceOriginatorCountryStateSubDivision] [varchar](35) NULL,
	[RemittanceOriginatorCountry] [varchar](2) NULL,
	[RemittanceOriginatorAddressLine] [varchar](490) NULL,
	[RemittanceOriginatorCountryOfResidence] [varchar](2) NULL,
	[RemittanceOriginatorContactName] [varchar](140) NULL,
	[RemittanceOriginatorContactPhoneNumber] [varchar](35) NULL,
	[RemittanceOriginatorContactMobileNumber] [varchar](35) NULL,
	[RemittanceOriginatorContactFaxNumber] [varchar](35) NULL,
	[RemittanceOriginatorContactElectronicAddress] [varchar](2048) NULL,
	[RemittanceOriginatorContactOtherInfo] [varchar](35) NULL,
	[RemittanceBeneficiaryIdentificationType] [varchar](4) NULL,
	[RemittanceBeneficiaryIdentificationCode] [varchar](4) NULL,
	[RemittanceBeneficiaryName] [varchar](140) NULL,
	[RemittanceBeneficiaryIdentificationNumber] [varchar](35) NULL,
	[RemittanceBeneficiaryIdentificationNumberIssuer] [varchar](35) NULL,
	[RemittanceBeneficiaryDateOfbirth] [varchar](8) NULL,
	[RemittanceBeneficiaryPlaceOfBirth] [varchar](74) NULL,
	[RemittanceBeneficiaryAddressType] [varchar](4) NULL,
	[RemittanceBeneficiaryDepartment] [varchar](70) NULL,
	[RemittanceBeneficiarySubDepartment] [varchar](70) NULL,
	[RemittanceBeneficiaryStreetName] [varchar](70) NULL,
	[RemittanceBeneficiaryBuildingNumber] [varchar](16) NULL,
	[RemittanceBeneficiaryPostCode] [varchar](16) NULL,
	[RemittanceBeneficiaryTownName] [varchar](35) NULL,
	[RemittanceBeneficiaryCountryStateSubDivision] [varchar](35) NULL,
	[RemittanceBeneficiaryCountry] [varchar](2) NULL,
	[RemittanceBeneficiaryAddressLine] [varchar](70) NULL,
	[RemittanceBeneficiaryCountryOfResidence] [varchar](2) NULL,
	[RemittanceActualAmountPaidCurrency] [varchar](3) NULL,
	[RemittanceActualAmountPaid] [decimal](18, 2) NULL,
	[RemittanceGrossAmountCurrency] [varchar](3) NULL,
	[RemittanceGrossAmount] [decimal](18, 2) NULL,
	[RemittanceSecondaryDocumentTypeCode] [varchar](4) NULL,
	[RemittanceSecondaryProprietaryDocumentTypeCode] [varchar](35) NULL,
	[RemittanceSecondaryDocumentIdentificationNumber] [varchar](35) NULL,
	[RemittanceSecondaryIssuer] [varchar](35) NULL,
	[RemittanceSecondaryRemittanceFreeText] [varchar](420) NULL,
	[RemittancePrimaryDocumentTypeCode] [varchar](4) NULL,
	[RemittancePrimaryProprietaryDocumentTypeCode] [varchar](35) NULL,
	[RemittancePrimaryDocumentIdentificationNumber] [varchar](35) NULL,
	[RemittancePrimaryIssuer] [varchar](35) NULL,
	[RemittanceNegotiatedDiscountCurrency] [varchar](3) NULL,
	[RemittanceNegotiatedDiscountAmount] [decimal](18, 2) NULL,
	[AdjustmentReasonCode] [varchar](2) NULL,
	[AdjustmentCreditDebitIndicator] [varchar](4) NULL,
	[AdjustmentCurrencyCode] [varchar](3) NULL,
	[AdjustmentAmount] [decimal](18, 2) NULL,
	[AdjustmentAdditionalInformation] [varchar](140) NULL,
	[DateofRemittanceDocument] [datetime] NULL,
	[ServiceMessageInformation] [varchar](420) NULL,
	[MessageDispositionFormatVersion] [varchar](2) NULL,
	[MessageDispositionProductionCode] [varchar](1) NULL,
	[MessageDispositionMessageDuplicationCode] [varchar](1) NULL,
	[MessageDispositionStatusIndicator] [varchar](1) NULL,
	[ReceiptDateMMDD] [varchar](4) NULL,
	[ReceiptTimeHHMM] [varchar](4) NULL,
	[ReceiptApplicationID] [varchar](4) NULL,
	[OmadCycleDate] [varchar](8) NULL,
	[OmadDestinationId] [varchar](8) NULL,
	[OmadOutputSequenceNumber] [varchar](6) NULL,
	[OmadOutputDateDateMMDD] [varchar](4) NULL,
	[OmadOutputTimeHHMM] [varchar](4) NULL,
	[OmadOutputApplicationID] [varchar](4) NULL,
	[ErrorCatecoryCode] [varchar](1) NULL,
	[ErrorCode] [varchar](3) NULL,
	[ErrorDescription] [varchar](35) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

