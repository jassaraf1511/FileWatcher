USE [FundWireInterfaceDB]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'FedMessageWire')
BEGIN
  drop table [dbo].[FedMessageWire]
END

Go
CREATE TABLE [dbo].[FedMessageWire](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[FileId] 								[numeric](18, 0) NULL,		/* File Id if Message came from FILE */
	[ClientId] 								[varchar](32) NOT NULL,		/* Client ID */	
	[DateReceived] 						 	[datetime]  NULL,		/* Date message received */
	[DateLoaded] 							[datetime]  NULL,		/* Date message Loaded */
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
	[ChargesDetail] [varchar](1) NULL,
	[ChargeCurrency1] [varchar](3) NULL,
	[ChargeAmount1] [decimal](18, 2) NULL,
	[ChargeCurrency2] [varchar](3) NULL,
	[ChargeAmount2] [decimal](18, 2) NULL,
	[ChargeCurrency3] [varchar](3) NULL,
	[ChargeAmount3] [decimal](18, 2) NULL,	
	[ChargeCurrency4] [varchar](3) NULL,
	[ChargeAmount4] [decimal](18, 2) NULL,		
	[InstructedCurrency] [varchar](3) NULL,
	[InstructedAmount] [decimal](18, 2) NULL,
	[ExchangeRate] [decimal](18, 15) NULL,
	[ReferenceforBeneficiary] [varchar](16) NULL,	
	[OriginatorOptionFPartyIdentifier] [varchar](4) NULL,
	[OriginatorOptionFPartyUniqueIdentifier] [varchar](31) NULL,
	[OriginatorOptionFPartyNameCode] [varchar](1) NULL,
	[OriginatorOptionFPartyName] [varchar](34) NULL,
	[AccountCreditedinDrawDownABA] [varchar](9) NULL,
	[OriginatortoBeneficiaryInfL1] [varchar](35) NULL,
	[OriginatortoBeneficiaryInfL2] [varchar](35) NULL,
	[OriginatortoBeneficiaryInfL3] [varchar](35) NULL,
	[OriginatortoBeneficiaryInfL4] [varchar](35) NULL,			
	[MethodOfPaymentCode] [varchar](5) NULL,
	[MethodOfPaymentAdditionalInformation] [varchar](30) NULL,
	[FItoFIInformationL1] [varchar](35) NULL,
	[FItoFIInformationL2] [varchar](35) NULL,	
	[FItoFIInformationL3] [varchar](35) NULL,
	[FItoFIInformationL4] [varchar](35) NULL,	
	[FItoFIInformationL5] [varchar](35) NULL,
	[FItoFIInformationL6] [varchar](35) NULL,		
	[DateofRemittanceDocument] [datetime] NULL,
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
	[ErrorDescription] [varchar](35) NULL,
	[LoadStatus] [varchar](16) NULL,
	[LoadRejectReason] [varchar](512) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatorId] [varchar](32) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatorId] [varchar](32) NULL,
	CONSTRAINT PK_FedMessageWire PRIMARY KEY (MessageId),
	CONSTRAINT UC_FedMessageWire_InternalId UNIQUE (InternalId),
	CONSTRAINT UC_FedMessageWire_Source UNIQUE (ClientId,FileId,IncomingOutGoing,OrigineName,MessageOrigin,MessageReference)	
)

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_FedMessageWire_MessageId')   
    DROP INDEX IX_FedMessageWire_MessageId ON FedMessageWire;   
GO  

CREATE NONCLUSTERED INDEX IX_FedMessageWire_MessageId 
    ON FedMessageWire (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
SET ANSI_PADDING OFF
GO


    ---External Table
	--- Tag:	5010	Originator Option F					Table : OriginatorOptionF	
	--- Tag:	8200	Unstructured Addenda Information 	Table : Addenda
	--- Tag:	3620 	Payment  Notification 				Table : PaymentNotification	
	--- Tag:	8250 	Related Remittance Information		Table : RelatedRemittanceInformation	

	/*

		Swift  Data Table
		Tag : 7033	Swift Sequence B 33B Currency/Instructed Amount			Table : SwiftData
		Tag : 7050	Swift Sequence B 50a Ordering Customer
		Tag : 7052	Swift Sequence B 52a Ordering Institution				Table : SwiftData
		Tag : 7056	Swift Sequence B 56a Intermediary Institution			Table : SwiftData
		Tag : 7057	Swift Sequence B 57a Account with Institution			Table : SwiftData
		Tag : 7059	Swift Sequence B 59a Beneficiary Customer				Table : SwiftData
		Tag : 7070	Swift Sequence B 70 Remittance Information				Table : SwiftData
		Tag : 7072	Swift Sequence B 72 Sender to Receiver Information		Table : SwiftData

	*/
	/*
		Below Tag to Table PartyInformation
		Tag: 6100	Receiver FI Information
		Tag: 6200	Intermediary FI Information
		Tag: 6300	Beneficiary s FI Information
		Tag: 6400	Beneficiary Information	
	*/
	
	/* 

		Tag : 6110	Drawdown Debit Account Advice Information	Table : PartyAdviceInformation	
		Tag : 6210	Intermediary FI Advice Information			Table : PartyAdviceInformation		
		Tag : 6310	Beneficiary s FI Advice Information			Table : PartyAdviceInformation
		Tag : 6410	Beneficiary Advice Information				Table : PartyAdviceInformation
*/
	/*
		Below Tag to Table AccountInformation
		Tag: 4000	Intermediary FI
		Tag: 4100	Beneficiary FI
		Tag: 4200	Beneficiary
		Tag: 4400	Account Debited in Drawdown
		Tag: 5000	Originator
		Tag: 5100	Originator FI
		Tag: 5200	Instructing FI
	*/
	
	/*
		 Remittance Document Invoice See Remittance RemittanceDocumentInvoice

		RemittanceDocumentInvoice It s  A group of Tags and can have multiple occurrances

		Tag: 8300	Remittance Originator									Table : RemittanceDocumentInvoice
		Tag: 8350	Remittance Beneficiary									Table : RemittanceDocumentInvoice
		Tag: 8450	Actual Amount Paid										Table : RemittanceDocumentInvoice
		Tag: 8500	Gross Amount of Remittance Document						Table : RemittanceDocumentData
		Tag: 8700	Secondary Remittance Document Information				Table : RemittanceDocumentInvoice
		Tag: 8750	Remittance Free Text Line 1 to 3 (140 characters each)	Table : RemittanceDocumentInvoice

	*/
	
	/*
		 Remittance Document Invoice See Remittance RemittanceDocumentInvoice

		RemittanceDocumentInvoice It s  A group of Tags and can have multiple occurrances

		Tag: 8400	Primary Remittance Document Information		Table : RemittanceDocumentData
		Tag: 8550	Amount of Negotiated Discount				Table : RemittanceDocumentData
		Tag: 8600	Adjustment Information						Table : RemittanceDocumentData


	*/

