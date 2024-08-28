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
    WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'DataDictionary')
BEGIN
  drop table [dbo].[DataDictionary]
END
CREATE TABLE [dbo].[DataDictionary](
	[InternalIdentifier] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[DictionaryId] [numeric](18, 0) NOT NULL,
	[DictionaryName] [varchar](64) NULL,
	[ProviderId] [varchar](64) NULL,
	[ProviderName] [varchar](64) NULL,
	[InterfaceId] [varchar](64) NULL,
	[InterfaceName] [varchar](128) NULL,
	[XMLDataDictionary] [varchar](max) NULL,
	[XSDDataDictionary] [varchar](max) NULL,
	[ExcelDataDictionary] [varbinary](max) NULL,
	[VersionNumber] [varchar](64) NULL,
	[VersionChanges] [varchar](512) NULL,
	[VersionChangesType] [varchar](32) NULL,
	[Description] [varchar](64) NULL,
	[EffectiveDate] [datetime] NULL,
	[PStatus] [varchar](32) NULL,
	[UserId] [varchar](64) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatorId] [varchar](64) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatorId] [varchar](64) NULL,
	[InactiveDate] [datetime] NULL,
	[InactiveatorId] [varchar](64) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_DataDictionary_DictionaryName')   
    DROP INDEX IX_DataDictionary_DictionaryName ON DataDictionary;   
GO  

CREATE UNIQUE NONCLUSTERED INDEX IX_DataDictionary_DictionaryName 
    ON DataDictionary (DictionaryName);   
GO  
SET ANSI_PADDING OFF
GO

SET ANSI_PADDING OFF
GO


