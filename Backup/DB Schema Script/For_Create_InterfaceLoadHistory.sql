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
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'InterfaceLoadHistory')
BEGIN
  drop table [dbo].[InterfaceLoadHistory]
END

Go

CREATE TABLE [dbo].[InterfaceLoadHistory](
	[FileId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[IncomingOutGoing] 	[varchar](16) NOT NULL,		/* FileType : Incoming / Outgoing */
	[Filename] 		[varchar](256) NOT NULL,		/* File Name*/
	[InterfaceIdentifier] [varchar](64) NOT NULL,  /*The ID of The Interface File */
	[ClientId] [varchar](63) Null,  /*The ID of The Interface File */
	[InterfaceName] [varchar](64) NOT NULL,
	[InterfaceType] [varchar](32)  NULL,
	[LoadProcessId] [varchar](32)  NULL,
	[LoadProcessName] [varchar](32)  NULL,
	[LoadDate] [datetime]  NULL,
	[ProcessedDate] [datetime]  NULL,
	[BusinessDate] [datetime]  NULL,
	[FileOrigin] [varchar](128)  NULL,
	[FileDestination] [varchar](128)  NULL,
	[FileArchived] [varchar](128)  NULL,
	[FileReceivedDate] [datetime]  NULL,
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
	[UpdatorId] [varchar](32) NULL,
	CONSTRAINT PK_InterfaceLoadHistory PRIMARY KEY (FileId)
)

GO





