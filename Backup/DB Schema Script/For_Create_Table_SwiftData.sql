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
	
USE [FundWireInterfaceDB]
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'SwiftData')
BEGIN
  drop table [dbo].[SwiftData]
END
GO

CREATE TABLE [dbo].[SwiftData](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[SequenceNumber] [int],
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
	CONSTRAINT PK_SwiftData PRIMARY KEY (InternalId),
	CONSTRAINT UC_SwiftData_Source UNIQUE (MessageId,SequenceNumber),
	CONSTRAINT FK_SwiftData_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	

GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_SwiftData_MessageId')   
    DROP INDEX IX_SwiftData_MessageId ON SwiftData;   
GO  

CREATE NONCLUSTERED INDEX IX_SwiftData_MessageId 
    ON SwiftData (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  

