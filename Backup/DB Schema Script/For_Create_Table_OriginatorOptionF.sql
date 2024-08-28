---- Orgiginator Option F

/*

	Tag:	5010	Originator Option F

*/

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'OriginatorOptionF')
BEGIN
  drop table [dbo].[OriginatorOptionF]
END

GO

CREATE TABLE [dbo].[OriginatorOptionF](
	[InternalId]  			[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 		[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]			[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  			[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 		[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[TagId] 				[varchar](64) NOT NULL,
	[MessageId]  			[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[SequenceNumber] 		[int],
	[IdentifierTypeCode] 	[varchar](4) NULL,
	[UniqueIdentifier] 		[varchar](35) NULL,
	[OrginatorOptionFCodeName] [varchar](1) NULL,
	[OrginatorOptionFName] 	[varchar](1) NULL,	
	[OrginatorOptionFCode1] [varchar](1) NULL,
	[OrginatorOptionFLine1] [varchar](34) NULL,	
	[OrginatorOptionFCode2] [varchar](1) NULL,
	[OrginatorOptionFLine2] [varchar](34) NULL,	
	[OrginatorOptionFCode3] [varchar](1) NULL,
	[OrginatorOptionFLine3] [varchar](34) NULL,
	CONSTRAINT PK_OriginatorOptionF PRIMARY KEY (InternalId),
	CONSTRAINT UC_OriginatorOptionF_Source UNIQUE (MessageId,TagId,SequenceNumber),
	CONSTRAINT FK_OriginatorOptionF_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)

GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_OriginatorOptionF_MessageId')   
    DROP INDEX IX_OriginatorOptionF_MessageId ON OriginatorOptionF;   
GO  

CREATE NONCLUSTERED INDEX IX_OriginatorOptionF_MessageId 
    ON OriginatorOptionF (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
