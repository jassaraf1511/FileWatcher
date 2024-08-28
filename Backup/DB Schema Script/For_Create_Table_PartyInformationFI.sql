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
	
USE [FundWireInterfaceDB]
GO
IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PartyInformationFI')
BEGIN
  drop table [dbo].[PartyInformationFI]
END

GO
CREATE TABLE [dbo].[PartyInformationFI](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[TagId] [varchar](64) NOT NULL,
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[SequenceNumber] [int],
	[PartyInfo] [varchar](30) NULL,
	[PartyInfoL1] [varchar](33) NULL,
	[PartyInfoL2] [varchar](33) NULL,
	[PartyInfoL3] [varchar](33) NULL,
	[PartyInfoL4] [varchar](33) NULL,
	[PartyInfoL5] [varchar](33) NULL,
	CONSTRAINT PK_PartyInformationFI PRIMARY KEY (InternalId),
	CONSTRAINT UC_PartyInformationFI_Source UNIQUE (MessageId,TagId,SequenceNumber),
	CONSTRAINT FK_PartyInformationFI_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PartyInformationFI_MessageId')   
    DROP INDEX IX_PartyInformationFI_MessageId ON PartyInformationFI;   
GO  

CREATE NONCLUSTERED INDEX IX_PartyInformationFI_MessageId 
    ON PartyInformationFI (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  