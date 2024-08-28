SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
/*
	Raw Message Table

*/

USE [FundWireInterfaceDB]
GO


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'RawMessage')
BEGIN
  drop table [dbo].[RawMessage]
END
CREATE TABLE [dbo].[RawMessage](
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
	[LoadStatus] [varchar](16) NULL,
	[LoadRejectReason] [varchar](512) NULL,
	[UserId] [varchar](32) NULL,
	[DataRecord] [varchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatorId] [varchar](32) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatorId] [varchar](32) NULL,
	CONSTRAINT PK_RawMessage PRIMARY KEY (MessageId),
	CONSTRAINT UC_RawMessage_InternalId UNIQUE (InternalId),
	CONSTRAINT UC_RawMessage_Source UNIQUE (ClientId,FileId,IncomingOutGoing,OrigineName,MessageOrigin,MessageReference)	
) 
GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_RawMessage_MessageId')   
    DROP INDEX IX_RawMessage_MessageId ON RawMessage;   
GO  

CREATE NONCLUSTERED INDEX IX_RawMessage_MessageId 
    ON RawMessage (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  
SET ANSI_PADDING OFF
GO