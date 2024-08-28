----ServiceMessageInformation

/*

	Service Message Information Line 1 to 12 (35 characters each) 1 Line By Raw 
	Tag: 9000	


*/


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'ServiceMessage')
BEGIN
  drop table [dbo].[ServiceMessage]
END

GO

CREATE TABLE [dbo].[ServiceMessage](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[SequenceNumber] [int],
	[MessageInformation] [varchar](35) NULL,
	CONSTRAINT PK_ServiceMessage PRIMARY KEY (InternalId),
	CONSTRAINT UC_ServiceMessage_Source UNIQUE (MessageId,SequenceNumber),
	CONSTRAINT FK_ServiceMessage_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_ServiceMessage_MessageId')   
    DROP INDEX IX_ServiceMessage_MessageId ON ServiceMessage;   
GO  

CREATE NONCLUSTERED INDEX IX_ServiceMessage_MessageId 
    ON ServiceMessage (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
