---- ADDENDA TABLE
/*
 	 Addenda table 

	Tag: 8200	Unstructured Addenda Information

*/
	
USE [FundWireInterfaceDB]
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'Addenda')
BEGIN
  drop table [dbo].[Addenda]
END

GO

CREATE TABLE [dbo].[Addenda](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[SequenceNumber] [int],
	[AddendaLength] [int] NULL,
	[AddendaInformation] [varchar](max) NULL,
	CONSTRAINT PK_Addenda PRIMARY KEY (InternalId),
	CONSTRAINT UC_Addenda_Source UNIQUE (MessageId,SequenceNumber),
	CONSTRAINT FK_Addenda_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	

GO


IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_Addenda_MessageId')   
    DROP INDEX IX_Addenda_MessageId ON Addenda;   
GO  

CREATE NONCLUSTERED INDEX IX_Addenda_MessageId 
    ON Addenda (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  