	
USE [FundWireInterfaceDB]
GO


/*
	Tag 3620	Payment Notification
*/

/****** Object:  Table [dbo].[PaymentNotification]     ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PaymentNotification')
BEGIN
  drop table [dbo].PaymentNotification]
END

Go
CREATE TABLE [dbo].[PaymentNotification](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[TagId] [varchar](64) NOT NULL,
	[Indicator] [varchar](1) NULL,
	[ContactEmail] [varchar](2048) NULL,
	[ContactName] [varchar](140) NULL,
	[ContactPhoneNumber] [varchar](35) NULL,
	[ContactFaxNumber] [varchar](35) NULL,
	[ContactMobileNumber] [varchar](35) NULL,
	[EndToEndIdentification] [varchar](35) NULL
)
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PaymentNotification_MessageId')   
    DROP INDEX IX_PaymentNotification_MessageId ON PaymentNotification;   
GO  

CREATE NONCLUSTERED INDEX X_PaymentNotification_MessageId 
    ON PaymentNotification (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  

---- Table common 
/*

	Tag : 4000	Intermediary FI
	Tag : 4100	Beneficiary FI
	Tag : 4200	Beneficiary
	Tag : 4400	Account Debited in Drawdown
	Tag : 5000	Originator
	Tag : 5100	Originator FI
	Tag : 5200	Instructing FI
*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'AccountInformation')
BEGIN
  drop table [dbo].AccountInformation]
END

GO

CREATE TABLE [dbo].[AccountInformation](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SequenceNumber] [int],
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[TagId] [varchar](64) NOT NULL,
	[AccountIdCode] [varchar](1) NULL,
	[AccountIdentifier] [varchar](34) NULL,
	[AccountName] [varchar](35) NULL,
	[AddressLine1] [varchar](35) NULL,
	[AddressLine2] [varchar](35) NULL,
	[AddressLine3] [varchar](35) NULL
)

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_AccountInformation_MessageId')   
    DROP INDEX IX_AccountInformation_MessageId ON AccountInformation;   
GO  

CREATE NONCLUSTERED INDEX X_AccountInformation_MessageId 
    ON AccountInformation (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  

/* 
 - Drawdown Debit Account Advice Information
 - Intermediary FI Advice Information
 - Beneficiary’s FI Advice Information
 - Beneficiary Advice Information

	Tag : 6110	Drawdown Debit Account Advice Information
	Tag : 6210	Intermediary FI Advice Information
	Tag : 6310	Beneficiary s FI Advice Information
	Tag : 6410	Beneficiary Advice Information
*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PartyAdviceInformation')
BEGIN
  drop table [dbo].PartyAdviceInformation]
END

GO

CREATE TABLE [dbo].[PartyAdviceInformation](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SequenceNumber] [int],
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[TagId] [varchar](64) NOT NULL,
	[AdviceCode] [varchar](3) NULL,
	[AdviceInfo] [varchar](26) NULL,
	[AdviceInfoL1] [varchar](33) NULL,
	[AdviceInfoL2] [varchar](33) NULL,
	[AdviceInfoL3] [varchar](33) NULL,
	[AdviceInfoL4] [varchar](33) NULL
)
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PartyAdviceInformation_MessageId')   
    DROP INDEX IX_PartyAdviceInformation_MessageId ON PartyAdviceInformation;   
GO  

CREATE NONCLUSTERED INDEX X_PartyAdviceInformation_MessageId 
    ON PartyAdviceInformation (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  

/* 
 - Receiver FI Information
 - Intermediary FI Information
 - Beneficiary’s FI Information
 - Beneficiary Information

	Tag : 6200	Intermediary FI Information
	Tag : 6300	Beneficiary s FI Information
	Tag : 6400	Beneficiary Information
	Tag : 3620	Payment Notification

*/

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PartyInformation')
BEGIN
  drop table [dbo].PartyInformation]
END

GO
CREATE TABLE [dbo].[PartyInformation](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SequenceNumber] [int],
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[TagId] [varchar](64) NOT NULL,
	[PartyInfo] [varchar](30) NULL,
	[PartyInfoL1] [varchar](33) NULL,
	[PartyInfoL2] [varchar](33) NULL,
	[PartyInfoL3] [varchar](33) NULL,
	[PartyInfoL4] [varchar](33) NULL,
	[PartyInfoL5] [varchar](33) NULL
)
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PartyInformation_MessageId')   
    DROP INDEX IX_PartyInformation_MessageId ON PartyInformation;   
GO  

CREATE NONCLUSTERED INDEX X_PartyInformation_MessageId 
    ON PartyInformation (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  

/*

	Swift  Data Table
	Tag : 7033	Swift Sequence B 33B Currency/Instructed Amount
	Tag : 7050	Swift Sequence B 50a Ordering Customer
	Tag : 7052	Swift Sequence B 52a Ordering Institution
	Tag : 7056	Swift Sequence B 56a Intermediary Institution
	Tag : 7057	Swift Sequence B 57a Account with Institution
	Tag : 7059	Swift Sequence B 59a Beneficiary Customer
	Tag : 7070	Swift Sequence B 70 Remittance Information
	Tag : 7072	Swift Sequence B 72 Sender to Receiver Information

*/

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'SwiftData')
BEGIN
  drop table [dbo].PartyInformation]
END
GO

CREATE TABLE [dbo].[SwiftData](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[InstructedCurrency] [varchar](3) NULL,
	[InstructedAmount] [decimal](18, 2),
	[OrderingCustomerL1] [varchar](35) NULL,
	[OrderingCustomerL2] [varchar](35) NULL,
	[OrderingCustomerL3] [varchar](35) NULL,
	[OrderingCustomerL4] [varchar](35) NULL,
	[OrderingCustomerL5] [varchar](35) NULL,
	[IntermediaryInstitutionL1] [varchar](35) NULL,
	[IntermediaryInstitutionL2] [varchar](35) NULL,
	[IntermediaryInstitutionL3] [varchar](35) NULL,
	[IntermediaryInstitutionL4] [varchar](35) NULL,
	[IntermediaryInstitutionL5] [varchar](35) NULL,
	[AccountwithInstitutionL1] [varchar](35) NULL,
	[AccountwithInstitutionL2] [varchar](35) NULL,
	[AccountwithInstitutionL3] [varchar](35) NULL,
	[AccountwithInstitutionL4] [varchar](35) NULL,
	[AccountwithInstitutionL5] [varchar](35) NULL,
	[BeneficiaryCustomerL1] [varchar](35) NULL,
	[BeneficiaryCustomerL2] [varchar](35) NULL,
	[BeneficiaryCustomerL3] [varchar](35) NULL,
	[BeneficiaryCustomerL4] [varchar](35) NULL,
	[BeneficiaryCustomerL5] [varchar](35) NULL,
	[RemittanceInformationL1] [varchar](35) NULL,
	[RemittanceInformationL2] [varchar](35) NULL,
	[RemittanceInformationL3] [varchar](35) NULL,
	[RemittanceInformationL4] [varchar](35) NULL,
	[RemittanceInformationL5] [varchar](35) NULL,
	[SendertoReceiverInformationL1] [varchar](35) NULL,
	[SendertoReceiverInformationL2] [varchar](35) NULL,
	[SendertoReceiverInformationL3] [varchar](35) NULL,
	[SendertoReceiverInformationL4] [varchar](35) NULL,
	[SendertoReceiverInformationL5] [varchar](35) NULL,
)

GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_SwiftData_MessageId')   
    DROP INDEX IX_SwiftData_MessageId ON SwiftData;   
GO  

CREATE NONCLUSTERED INDEX X_SwiftData_MessageId 
    ON SwiftData (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  


---- ADDENDA TABLE
/*
 	 Addenda table 

	Tag: 8200	Unstructured Addenda Information

*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'Addenda')
BEGIN
  drop table [dbo].Addenda]
END

GO

CREATE TABLE [dbo].[Addenda](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[AddendaLength] [int] NULL,
	[AddendaInformation] [varchar](max) NULL
)

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_Addenda_MessageId')   
    DROP INDEX IX_Addenda_MessageId ON Addenda;   
GO  

CREATE NONCLUSTERED INDEX X_AccountInformation_MessageId 
    ON Addenda (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  


---- ADDENDA TABLE
/*
 	 Related Document table 

	Tag: 8250	Related Remittance Information


*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'RelatedRemittanceInformation')
BEGIN
  drop table [dbo].RelatedRemittanceInformation]
END

GO

CREATE TABLE [dbo].[RelatedRemittanceInformation](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
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
	[AddressLine1] [varchar](70) NULL,
	[AddressLine2] [varchar](70) NULL,
	[AddressLine3] [varchar](70) NULL
	[AddressLine4] [varchar](70) NULL,
	[AddressLine5] [varchar](70) NULL,
	[AddressLine6] [varchar](70) NULL
	[AddressLine7] [varchar](70) NULL,

)

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_RelatedRemittanceInformation_MessageId')   
    DROP INDEX IX_Addenda_MessageId ON Addenda;   
GO  

CREATE NONCLUSTERED INDEX X_RelatedRemittanceInformation_MessageId 
    ON RelatedRemittanceInformation (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  


RemittanceDocumentInvoice


---- RemittanceDocumentInvoice
/*
 	 Remittance Document Invoice

	RemittanceDocumentInvoice It s  A group of Tags and can have multiple occurrances

	Tag: 8300	Remittance Originator
	Tag: 8350	Remittance Beneficiary
	Tag: 8450	Actual Amount Paid
	Tag: 8500	Gross Amount of Remittance Document
	Tag: 8700	Secondary Remittance Document Information
	Tag: 8750	Remittance Free Text Line 1 to 3 (140 characters each) 

*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'RemittanceDocumentInvoice')
BEGIN
  drop table [dbo].RemittanceDocumentInvoice]
END

GO

CREATE TABLE [dbo].[RelatedRemittanceInformation](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[SequenceNumber] [int],

	---  Tag 8300

	[OriginatorIdentificationType] [varchar](2) NULL,
	[OriginatorIdentificationCode] [varchar](4) NULL,
	[OriginatorName] [varchar](140) NULL,
	[OriginatorIdentificationNumber] [varchar](35) NULL,
	[OriginatorIdentificationNumberIssuer] [varchar](35) NULL,
	[OriginatorDateOfBirth] [varchar](8) NULL,
	[OriginatorPlaceOfBirth] [varchar](74) NULL,
	[OriginatorAddressType] [varchar](4) NULL,
	[OriginatorDepartment] [varchar](70) NULL,
	[OriginatorSubDepartment] [varchar](70) NULL,
	[OriginatorStreetName] [varchar](70) NULL,
	[OriginatorBuildingNumber] [varchar](16) NULL,
	[OriginatorPostCode] [varchar](16) NULL,
	[OriginatorTownName] [varchar](35) NULL,
	[OriginatorCountryStateSubDivision] [varchar](35) NULL,
	[OriginatorCountry] [varchar](2) NULL,
	[OriginatorAddressLine1] [varchar](70) NULL,
	[OriginatorAddressLine2] [varchar](70) NULL,
	[OriginatorAddressLine3] [varchar](70) NULL
	[OriginatorAddressLine4] [varchar](70) NULL,
	[OriginatorAddressLine5] [varchar](70) NULL,
	[OriginatorAddressLine6] [varchar](70) NULL
	[OriginatorAddressLine7] [varchar](70) NULL,
	[OriginatorCountryOfResidence] [varchar](2) NULL,
	[OriginatorContactName] [varchar](140) NULL,
	[OriginatorContactPhoneNumber] [varchar](35) NULL,
	[OriginatorContactMobileNumber] [varchar](35) NULL,
	[OriginatorContactFaxNumber] [varchar](35) NULL,
	[OriginatorContactElectronicAddress] [varchar](2048) NULL,
	[OriginatorContactOtherInfo] [varchar](35) NULL,

	---  Tag 8350
	[BeneficiaryIdentificationType] [varchar](4) NULL,
	[BeneficiaryIdentificationCode] [varchar](4) NULL,
	[BeneficiaryName] [varchar](140) NULL,
	[BeneficiaryIdentificationNumber] [varchar](35) NULL,
	[BeneficiaryIdentificationNumberIssuer] [varchar](35) NULL,
	[BeneficiaryDateOfbirth] [varchar](8) NULL,
	[BeneficiaryPlaceOfBirth] [varchar](74) NULL,
	[BeneficiaryAddressType] [varchar](4) NULL,
	[BeneficiaryDepartment] [varchar](70) NULL,
	[BeneficiarySubDepartment] [varchar](70) NULL,
	[BeneficiaryStreetName] [varchar](70) NULL,
	[BeneficiaryBuildingNumber] [varchar](16) NULL,
	[BeneficiaryPostCode] [varchar](16) NULL,
	[BeneficiaryTownName] [varchar](35) NULL,
	[BeneficiaryCountryStateSubDivision] [varchar](35) NULL,
	[BeneficiaryCountry] [varchar](2) NULL,
	[BeneficiaryAddressLine1] [varchar](70) NULL,
	[BeneficiaryAddressLine2] [varchar](70) NULL,
	[BeneficiaryAddressLine3] [varchar](70) NULL
	[BeneficiaryAddressLine4] [varchar](70) NULL,
	[BeneficiaryAddressLine5] [varchar](70) NULL,
	[BeneficiaryAddressLine6] [varchar](70) NULL
	[BeneficiaryAddressLine7] [varchar](70) NULL,
	[BeneficiaryCountryOfResidence] [varchar](2) NULL,

	---  Tag 8450
	[ActualAmountPaidCurrency] [varchar](3) NULL,
	[ActualAmountPaid] [decimal](18, 2) NULL,

	---  Tag 8500
	[GrossAmountCurrency] [varchar](3) NULL,
	[GrossAmount] [decimal](18, 2) NULL,

	---  Tag 8700
	[SecondaryDocumentTypeCode] [varchar](4) NULL,
	[SecondaryProprietaryDocumentTypeCode] [varchar](35) NULL,
	[SecondaryDocumentIdentificationNumber] [varchar](35) NULL,
	[SecondaryIssuer] [varchar](35) NULL,
	[SecondaryFreeTextL1] [varchar](140) NULL,
	[SecondaryFreeTextL2] [varchar](140) NULL,
	[SecondaryFreeTextL3] [varchar](140) NULL

)

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_RemittanceDocumentInvoice_MessageId')   
    DROP INDEX IX_RemittanceDocumentInvoice_MessageId ON Addenda;   
GO  

CREATE NONCLUSTERED INDEX X_RemittanceDocumentInvoice_MessageId 
    ON RemittanceDocumentInvoice (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  


----RemittanceDocumentData

/*

		RemittanceDocumentInvoice It s  A group of Tags and can have multiple occurrances

		Tag: 8400	Primary Remittance Document Information
		Tag: 8550	Amount of Negotiated Discount
		Tag: 8600	Adjustment Information


*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'RemittanceDocumentData')
BEGIN
  drop table [dbo].RemittanceDocumentData]
END

GO

CREATE TABLE [dbo].[RemittanceDocumentData](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[SequenceNumber] [int],

	---  Tag 8400
	[PrimaryDocumentTypeCode] [varchar](4) NULL,
	[PrimaryProprietaryDocumentTypeCode] [varchar](35) NULL,
	[PrimaryDocumentIdentificationNumber] [varchar](35) NULL,
	[PrimaryIssuer] [varchar](35) NULL,

	---  Tag 8550
	[NegotiatedDiscountCurrency] [varchar](3) NULL,
	[NegotiatedDiscountAmount] [decimal](18, 2) NULL,

	---  Tag 8600
	[AdjustmentReasonCode] [varchar](2) NULL,
	[AdjustmentCreditDebitIndicator] [varchar](4) NULL,
	[AdjustmentCurrencyCode] [varchar](3) NULL,
	[AdjustmentAmount] [decimal](18, 2) NULL,
	[AdjustmentAdditionalInformation] [varchar](140) NULL
)

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_RemittanceDocumentData_MessageId')   
    DROP INDEX IX_RemittanceDocumentData_MessageId ON Addenda;   
GO  

CREATE NONCLUSTERED INDEX X_RemittanceDocumentData_MessageId 
    ON RemittanceDocumentData (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  


----ServiceMessageInformation

/*

	Service Message Information Line 1 to 12 (35 characters each) 1 Line By Raw 
	Tag: 9000	


*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'ServiceMessage')
BEGIN
  drop table [dbo].ServiceMessage]
END

GO

CREATE TABLE [dbo].[ServiceMessage](
	[InternalId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageOrigin] [varchar](10) NOT NULL,
	[OrigineName] [varchar](64) NOT NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL,
	[MessageId] [varchar](32) NOT NULL,
	[SequenceNumber] [int],
	[MessageInformation] [varchar](35) NULL,

)

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_ServiceMessage_MessageId')   
    DROP INDEX IX_ServiceMessage_MessageId ON Addenda;   
GO  

CREATE NONCLUSTERED INDEX X_ServiceMessageData_MessageId 
    ON ServiceMessage (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
