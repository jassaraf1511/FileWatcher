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

	
USE [FundWireInterfaceDB]
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PartyInformation')
BEGIN
  drop table [dbo].[PartyInformation]
END

GO

CREATE TABLE [dbo].[PartyInformation](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[TagId] [varchar](64) NOT NULL,
	[SequenceNumber] [int],
	[AccountIdCode] [varchar](1) NULL,
	[AccountIdentifier] [varchar](34) NULL,
	[AccountName] [varchar](35) NULL,
	[AddressLine1] [varchar](35) NULL,
	[AddressLine2] [varchar](35) NULL,
	[AddressLine3] [varchar](35) NULL,
	CONSTRAINT PK_PartyInformation PRIMARY KEY (InternalId),
	CONSTRAINT UC_PartyInformation_Source UNIQUE (MessageId,TagId,SequenceNumber)	,
	CONSTRAINT FK_PartyInformation_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	

GO


IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PartyInformation_MessageId')   
    DROP INDEX IX_PartyInformation_MessageId ON PartyInformation;   
GO  

CREATE NONCLUSTERED INDEX IX_PartyInformation_MessageId 
    ON PartyInformation (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
 