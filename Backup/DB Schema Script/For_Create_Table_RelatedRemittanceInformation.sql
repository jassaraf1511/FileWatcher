---- RelatedRemittanceInformation
/*
 	 Related Document table 

	Tag: 8250	Related Remittance Information


*/
	
USE [FundWireInterfaceDB]
GO


IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'RelatedRemittanceInformation')
BEGIN
  drop table [dbo].[RelatedRemittanceInformation]
END

GO

CREATE TABLE [dbo].[RelatedRemittanceInformation](
	[InternalId]  						 	[numeric](18, 0) IDENTITY(1,1) NOT NULL,		/* The ID of The Interface File */
	[MessageReference] 						[varchar](32) NOT NULL,		/* The Message Reference can be MQ Message Id or File Record NUMBER*/
	[MessageOrigin]						 	[varchar](10) NOT NULL,		/* Origin Message File or MQ */
	[OrigineName]  						 	[varchar](64) NOT NULL,		/* ID of the File or MQ Queue */
	[IncomingOutGoing] 						[varchar](16) NOT NULL,		/* Message Type : Incoming / Outgoing */
	[MessageId]  						 	[varchar](32) NOT NULL,		/* Message ID Generated for Message and Common To all tables */
	[SequenceNumber] [int],
	[RemittanceIdentification] [varchar](35) NULL,
	[RemittanceLocationMethod] [varchar](4) NULL,
	[RemittanceElectronicAddress] [varchar](2048) NULL,
	[RemittanceName] [varchar](140) NULL,
	[AddressType] [varchar](4) NULL,
	[Department] [varchar](70) NULL,
	[SubDepartment] [varchar](70) NULL,
	[StreetName] [varchar](70) NULL,
	[BuildingNumber] [varchar](16) NULL,
	[PostCode] [varchar](16) NULL,
	[TownName] [varchar](35) NULL,
	[CountryStateSubDivision] [varchar](35) NULL,
	[Country] [varchar](2) NULL,
	[AddressLine1] [varchar](70) NULL,
	[AddressLine2] [varchar](70) NULL,
	[AddressLine3] [varchar](70) NULL,
	[AddressLine4] [varchar](70) NULL,
	[AddressLine5] [varchar](70) NULL,
	[AddressLine6] [varchar](70) NULL,
	[AddressLine7] [varchar](70) NULL,
	CONSTRAINT PK_RelatedRemittance PRIMARY KEY (InternalId),
	CONSTRAINT UC_RelatedRemittance_Source UNIQUE (MessageId,SequenceNumber),
	CONSTRAINT FK_RelatedRemittance_MessageId FOREIGN KEY (MessageId) REFERENCES FedMessageWire(MessageId)	
)	
GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_RelatedRemittanceInformation_MessageId')   
    DROP INDEX IX_Addenda_MessageId ON RelatedRemittanceInformation;   
GO  

CREATE NONCLUSTERED INDEX X_RelatedRemittanceInformation_MessageId 
    ON RelatedRemittanceInformation (MessageOrigin,OrigineName,IncomingOutGoing,MessageId);   
GO  


