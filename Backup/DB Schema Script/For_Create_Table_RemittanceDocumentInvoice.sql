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
	
USE [FundWireInterfaceDB]
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'RemittanceDocumentInvoice')
BEGIN
  drop table [dbo].[RemittanceDocumentInvoice]
END

GO

CREATE TABLE [dbo].[RemittanceDocumentInvoice](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
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
	[OriginatorAddressLine3] [varchar](70) NULL,
	[OriginatorAddressLine4] [varchar](70) NULL,
	[OriginatorAddressLine5] [varchar](70) NULL,
	[OriginatorAddressLine6] [varchar](70) NULL,
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
	[BeneficiaryAddressLine3] [varchar](70) NULL,
	[BeneficiaryAddressLine4] [varchar](70) NULL,
	[BeneficiaryAddressLine5] [varchar](70) NULL,
	[BeneficiaryAddressLine6] [varchar](70) NULL,
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
	[SecondaryFreeTextL3] [varchar](140) NULL,
	CONSTRAINT PK_RemittanceDocumentInvoice PRIMARY KEY (InternalId),
	CONSTRAINT UC_RemittanceDocumentInvoice_Source UNIQUE (MessageId,SequenceNumber),
	CONSTRAINT FK_RemittanceDocumentInvoice_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	


GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_RemittanceDocumentInvoice_MessageId')   
    DROP INDEX IX_RemittanceDocumentInvoice_MessageId ON RemittanceDocumentInvoice;   
GO  

CREATE NONCLUSTERED INDEX IX_RemittanceDocumentInvoice_MessageId 
    ON RemittanceDocumentInvoice (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  


