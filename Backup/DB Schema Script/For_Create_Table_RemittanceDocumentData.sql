----RemittanceDocumentData

/*

		RemittanceDocumentData It s  A group of Tags and can have multiple occurrances

		Tag: 8400	Primary Remittance Document Information
		Tag: 8550	Amount of Negotiated Discount
		Tag: 8600	Adjustment Information


*/
	
USE [FundWireInterfaceDB]
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'RemittanceDocumentData')
BEGIN
  drop table [dbo].[RemittanceDocumentData]
END

GO

CREATE TABLE [dbo].[RemittanceDocumentData](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
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
	[AdjustmentAdditionalInformation] [varchar](140) NULL,
	CONSTRAINT PK_RemittanceDocumentData PRIMARY KEY (InternalId),
	CONSTRAINT UC_RemittanceDocumentData_Source UNIQUE (MessageId,SequenceNumber),
	CONSTRAINT FK_RemittanceDocumentData_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_RemittanceDocumentData_MessageId')   
    DROP INDEX IX_RemittanceDocumentData_MessageId ON RemittanceDocumentData;   
GO  

CREATE NONCLUSTERED INDEX IX_RemittanceDocumentData_MessageId 
    ON RemittanceDocumentData (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
