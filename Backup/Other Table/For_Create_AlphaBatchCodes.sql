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
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'AlphaBatchCodes')
BEGIN
  drop table [dbo].[AlphaBatchCodes]
END
CREATE TABLE [dbo].[AlphaBatchCodes](
	[InternalIdentifier] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AssignedCode] [smallint] NULL,
	[AlphaBatchCode] [varchar](32) NULL,
	[Description] [varchar](64) NULL,
	[ShortDescription] [varchar](32) NULL,
	[TransactionType] [varchar](64) NULL,
	[TransactionSide] [varchar](32) NULL,
	[InterestType]   [varchar](64) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatorId] [varchar](32) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatorId] [varchar](32) NULL
) 
GO

SET ANSI_PADDING OFF
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_AlphaBatchCodes_AlphaBatchCode')   
    DROP INDEX IX_AlphaBatchCodes_AlphaBatchCode ON AlphaBatchCodes;   
GO  

CREATE NONCLUSTERED INDEX X_AlphaBatchCodes_AlphaBatchCode 
    ON AlphaBatchCodes (AlphaBatchCode);   
GO  





