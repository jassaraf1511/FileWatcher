USE [FundWireInterfaceDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'FedMessageByTag')
BEGIN
  drop table [dbo].[FedMessageByTag]
END

Go
 
CREATE TABLE [dbo].[FedMessageByTag] (
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL  UNIQUE,		/* The ID of The Interface File */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[FileId] 								[numeric](18, 0) NULL,		/* File Id if Message came from FILE */
	[ClientId] 								[varchar](32) NOT NULL,		/* Client ID */	[DateReceived] 						 	[datetime]  NULL,		/* Date message received */
	[DateLoaded] 							[datetime]  NULL,		/* Date message Loaded */
	OutputCycleDate 				 		[varchar](10)   NULL,		/* The Message Output Cycle Date */
	OutputDestinationId 			 		[varchar](8) NULL,		/* The Message Output DestinationId */
	OutputSequenceNumber 			 		[varchar](6) NULL,		/* The Message Output Sequence Number */
	OutputDateDateMMDD 				 		[varchar](4) NULL,		/* The Message Output Date (MMDD, based on the calendar date) */
	OutputTimeHHMM  				 		[varchar](4),		/* The Message Output Time (HHMM, based on a 24-hour clock, Eastern Time) */
	OutputApplicationID 			 		[varchar](4) NULL,		/* The Message Output FRB Application ID (4 characters) */
	SenderSuppliedInformation		 		[varchar] (12) NULL,		/*Sender Supplied Information*/
	TypeSubType						 		[varchar] (4) NULL,		/*Type/Subtype*/
	InputMessageAccountabilityData	 		[varchar] (22) NULL,		/*Input Message Accountability Data (IMAD)*/
	SenderDI						 		[varchar] (27) NULL,		/*Sender DI*/
	ReceiverDI						 		[varchar] (27) NULL,		/*Receiver DI*/
	BusinessFunctionCode			 		[varchar] (3) NULL,		/*Business Function Code*/
	SenderReference					 		[varchar] (16) NULL,		/*Sender Reference*/
	PreviousMessageId				 		[varchar] (22) NULL,		/*Previous Message Identifier*/
	LocalInstrument					 		[varchar] (39) NULL,		/*Local Instrument*/
	PaymentNotification				 		[varchar] (2328) NULL,		/*Payment Notification*/
	Charges						 	 		[varchar] (73) NULL,		/*Charges The Detail of Chargecurrency and Amount up to 4 Occurrence)*/
	InstructedAmount				 		[varchar] (18) NULL,		/*Instructed Amount*/
	ExchangeRate					 		[varchar] (12) NULL,		/*Exchange Rate*/
	IntermediaryFI					 		[varchar] (175) NULL,		/*Intermediary FI*/
	BeneficiaryFI					 		[varchar] (175) NULL,		/*Beneficiary FI*/
	Beneficiary						 		[varchar] (175) NULL,		/*Beneficiary*/
	ReferenceforBeneficiary				 	[varchar] (16) NULL,		/*Reference for Beneficiary*/
	AccountDebitedinDrawdown		   	 	[varchar] (175) NULL,		/*Account Debited in Drawdown*/
	Originator							 	[varchar] (175) NULL,		/*Originator*/
	OriginatorOptionF					 	[varchar] (175) NULL,		/*Originator Option F*/
	OriginatorFI						 	[varchar] (175) NULL,		/*Originator FI*/
	InstructingFI						 	[varchar] (175) NULL,		/*Instructing FI*/
	AccountCreditedinDrawdown		 		[varchar] (9) NULL,		/*Account Credited in Drawdown*/
	OriginatortoBeneficiaryInformation		[varchar] (140) NULL,		/*Originator to Beneficiary Information*/
	ReceiverFIInformation					[varchar] (195) NULL,		/*Receiver FI Information*/
	DrawdownDebitAccountAdviceInformation 	[varchar] (194) NULL,		/*Drawdown Debit Account Advice Information*/
	IntermediaryFIInformation				[varchar] (195) NULL,		/*Intermediary FI Information*/
	IntermediaryFIAdviceInformation			[varchar] (194) NULL,		/*Intermediary FI Advice Information*/
	BeneficiarysFIInformation				[varchar] (195) NULL,		/*Beneficiary s FI Information*/
	BeneficiarysFIAdviceInformation			[varchar] (194) NULL,		/*Beneficiary s FI Advice Information*/
	BeneficiaryInformation					[varchar] (195) NULL,		/*Beneficiary Information*/
	BeneficiaryAdviceInformation			[varchar] (194) NULL,		/*Beneficiary Advice Information*/
	MethodofPaymenttoBeneficiary			[varchar] (35) NULL,		/*Method of Payment to Beneficiary*/
	FItoFIInformation						[varchar] (210) NULL,		/*FI to FI Information */
	SwiftB33BCurrencyInstructedAmount	    [varchar] (23) NULL,		/*Swift Sequence B 33B Currency/Instructed Amount*/
	SwiftB50AOrderingCustomer				[varchar] (180) NULL,		/*Swift Sequence B 50a Ordering Customer*/
	SwiftB52AOrderingInstitution			[varchar] (180) NULL,		/*Swift Sequence B 52a Ordering Institution*/
	SwiftB56AIntermediaryInstitution		[varchar] (180) NULL,		/*Swift Sequence B 56a Intermediary Institution*/
	SwiftB57AAccountwithInstitution			[varchar] (180) NULL,		/*Swift Sequence B 57a Account with Institution*/
	SwiftB59ABeneficiaryCustomer			[varchar] (180) NULL,		/*Swift Sequence B 59a Beneficiary Customer*/
	SwiftB70RemittanceInformation			[varchar] (180) NULL,		/*Swift Sequence B 70 Remittance Information*/
	SwiftB72SendertoReceiverInformation		[varchar] (180) NULL,		/*Swift Sequence B 72 Sender to Receiver Information*/
	UnstructuredAddendaInformation			[varchar] (max) NULL,		/*Unstructured Addenda Information*/
	RelatedRemittanceInformation			[varchar] (2616) NULL,		/*Related Remittance Information*/
	RemittanceOriginatorInfo				[varchar] (3436) NULL,		/*Remittance Originator*/
	RemittanceBeneficiaryInfo				[varchar] (690) NULL,		/*Remittance Beneficiary*/
	ActualAmountPaid						[varchar] (22) NULL,		/*Actual Amount Paid*/
	GrossAmountofRemittanceDocument			[varchar] (22) NULL,		/*Gross Amount of Remittance Document*/
	SecondaryRemittanceDocumentInformation	[varchar] (109) NULL,		/*Secondary Remittance Document Information*/
	RemittanceFreeText						[varchar] (420) NULL,		/*Remittance Free Text Line 1 to 3 (140 characters each)*/
	PrimaryRemittanceDocumentInformation	[varchar] (109) NULL,		/*Primary Remittance Document Information*/
	AmountofNegotiatedDiscount				[varchar] (22) NULL,		/*Amount of Negotiated Discount*/
	AdjustmentInformationDetails			[varchar] (168) NULL,		/*Adjustment Information*/
	DateofRemittanceDocument				[varchar] (8) NULL,		/*Date of Remittance Document*/
	ServiceMessageInformation				[varchar] (420) NULL,		/*Service Message Information Line 1 to 12 (35 characters each)*/
	MessageDisposition						[varchar] (5) NULL,		/*Message Disposition*/
	ReceiptTimeStamp						[varchar] (12) NULL,		/*Receipt Time Stamp*/
	OutputMessageAccountabilityData			[varchar] (34) NULL,		/*Output Message Accountability Data (OMAD)*/
	Error								    [varchar] (41) NULL,		/*Error*/
	[LoadStatus] [varchar](16) NULL,
	[LoadRejectReason] [varchar](512) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatorId] [varchar](32) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatorId] [varchar](32) NULL,
	CONSTRAINT PK_FedMessageByTag PRIMARY KEY (MessageId)
)
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_FedMessageByTag_MessageId')   
    DROP INDEX IX_FedMessageByTag_MessageId ON FedMessageByTag;   
GO  

CREATE NONCLUSTERED INDEX IX_FedMessageByTag_MessageId 
    ON FedMessageByTag (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
SET ANSI_PADDING OFF
GO