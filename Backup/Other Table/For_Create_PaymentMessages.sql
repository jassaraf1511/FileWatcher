USE [PaymentInterfaceDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF EXISTS (SELECT * 
    FROM INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'PaymentMessages')
BEGIN
  drop table [dbo].[PaymentMessages]
END
CREATE TABLE [dbo].[PaymentMessages](
	[InternalIdentifier] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[InterfaceID] [varchar](64), 
	[MessageType] [varchar](32) NULL,
	[TransactionIdentifier] [varchar](64) NULL,
	[RejectReasonCode] [varchar](32) NULL,
	[RecordType] [varchar](2) NULL,
	[RecordKey] [varchar](1) NULL,
	[ControlType] [varchar](1) NULL,
	[UnpostItemReason] [smallint] NULL,
	[OriginalTransactionCode] [smallint] NULL,
	[TransactionCode] [smallint] NULL,
	[TransactionDate] [date] NULL,
	[TransactionPostingDate] [date] NULL,
	[EntryRefNumber] [varchar](64) NULL,
	[EntryReferenceNumber] [varchar](64) NULL,
	[EntryDate] [varchar](2) NULL,
	[AdjustmentTransactionReference] [varchar](8) NULL,
	[AlphaBatchCode] [varchar](2) NULL,
	[AdjustmentTransactionReferenceNumber] [varchar](6) NULL,
	[RejectReasonDescription] [varchar](512) NULL,
	[LoadStatus] [varchar](16) NULL,
	[UserId] [varchar](32) NULL,
	[XMLDataRecord] [varchar](max) NULL,
	[DataRecord] [varchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatorId] [varchar](32) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatorId] [varchar](32) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO


