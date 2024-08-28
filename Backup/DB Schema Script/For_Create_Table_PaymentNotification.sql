USE [FundWireInterfaceDB]
GO


/*
	Tag 3620	Payment Notification
*/

/****** Object:  Table [dbo].[PaymentNotification]     ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PaymentNotification')
BEGIN
  drop table [dbo].[PaymentNotification]
END

Go
CREATE TABLE [dbo].[PaymentNotification](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[TagId] [varchar](64) NOT NULL,
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[SequenceNumber] [int],
	[NotificationIndicator] [varchar](1) NULL,
	[ContactEmail] [varchar](2048) NULL,
	[ContactName] [varchar](140) NULL,
	[ContactPhoneNumber] [varchar](35) NULL,
	[ContactFaxNumber] [varchar](35) NULL,
	[ContactMobileNumber] [varchar](35) NULL,
	[EndToEndIdentification] [varchar](35) NULL,
	CONSTRAINT PK_PaymentNotification PRIMARY KEY (InternalId),
	CONSTRAINT UC_PaymentNotification_Source UNIQUE (MessageId,TagId,SequenceNumber),
	CONSTRAINT FK_PaymentNotification_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	

GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PaymentNotification_MessageId')   
    DROP INDEX IX_PaymentNotification_MessageId ON PaymentNotification;   
GO  

CREATE NONCLUSTERED INDEX X_PaymentNotification_MessageId 
    ON PaymentNotification (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  