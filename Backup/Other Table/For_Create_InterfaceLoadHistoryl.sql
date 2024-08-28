USE [FundWireInterfaceDB]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PaymentDataFiles')
BEGIN
  drop table [dbo].[PaymentDataFiles]
END

Go

CREATE TABLE [dbo].[PaymentDataFiles](
	[FileId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[InterfaceIdentifier] [varchar](64) NOT NULL,  /*The ID of The Interface File */
	[InterfaceName] [varchar](64) NOT NULL,
	[InterfaceBusinessDate] [datetime]  NULL,
	[InterfaceType] [varchar](32)  NULL,
	[IncomingOutGoing] [varchar](16) NOT NULL, --- Message  Incoming - Outgoing
	[LoadProcessId] [varchar](32)  NULL,
	[LoadProcessName] [varchar](32)  NULL,
	[FileOrigin] [varchar](128)  NULL,
	[FileDestination] [varchar](128)  NULL,
	[FileArchived] [varchar](128)  NULL,
	[FileReceivedDate] [datetime]  NULL,
	[FileDownloadStartTime] [datetime]  NULL,
	[FileDownloadEndTime] [datetime]  NULL,
	[FileProcessingStartTime] [datetime]  NULL,
	[FileProcessingEndTime] [datetime]  NULL,
	[RecordsNumber] [int]  NULL,
	[RecordProcessed] [int]  NULL,
	[RecordRejected] [int]  NULL,
	[FileSize] [int]  NULL,
	[RejectReasonCode] [varchar](32)  NULL,
	[RejectReasonDescription] [varchar](512)  NULL,
	[LoadStatus] [varchar](16)  NULL,
	[UserId] [varchar](32)  NULL,
	[DataFileContent] [varbinary](max)  NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatorId] [varchar](32) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatorId] [varchar](32) NULL
)

GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_PaymentDataFiles_InterfaceIdentifier')   
    DROP INDEX IX_PaymentDataFiles_InterfaceIdentifier ON PaymentDataFiles;   
GO  

CREATE NONCLUSTERED INDEX IX_PaymentDataFiles_InterfaceIdentifier 
    ON PaymentDataFiles (InterfaceIdentifier);   
GO  
SET ANSI_PADDING OFF
GO




