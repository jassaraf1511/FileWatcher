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

	
USE [FundWireInterfaceDB]
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PartyAdviceInformation')
BEGIN
  drop table [dbo].[PartyAdviceInformation]
END

GO

CREATE TABLE [dbo].[PartyAdviceInformation](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[TagId] [varchar](64) NOT NULL,
	[SequenceNumber] [int],
	[AdviceCode] [varchar](3) NULL,
	[AdviceInfo] [varchar](26) NULL,
	[AdviceInfoL1] [varchar](33) NULL,
	[AdviceInfoL2] [varchar](33) NULL,
	[AdviceInfoL3] [varchar](33) NULL,
	[AdviceInfoL4] [varchar](33) NULL,
	[AdviceInfoL5] [varchar](33) NULL,
	CONSTRAINT PK_PartyAdviceInformation PRIMARY KEY (InternalId),
	CONSTRAINT UC_PartyAdviceInformation_Source UNIQUE (MessageId,TagId,SequenceNumber)	,
	CONSTRAINT FK_PartyAdviceInformation_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PartyAdviceInformation_MessageId')   
    DROP INDEX IX_PartyAdviceInformation_MessageId ON PartyAdviceInformation;   
GO  

CREATE NONCLUSTERED INDEX X_PartyAdviceInformation_MessageId 
    ON PartyAdviceInformation (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
