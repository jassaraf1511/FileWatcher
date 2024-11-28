USE [CorporateActions]
GO

ALTER TABLE [dbo].[Action_Option_Cash_Payout] DROP CONSTRAINT [FK_Action_Option_Cash_Payout_Announcement_Event]
GO

/****** Object:  Table [dbo].[Action_Option_Cash_Payout]    Script Date: 11/28/2024 1:01:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Action_Option_Cash_Payout]') AND type in (N'U'))
DROP TABLE [dbo].[Action_Option_Cash_Payout]
GO

/****** Object:  Table [dbo].[Action_Option_Cash_Payout]    Script Date: 11/28/2024 1:01:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Action_Option_Cash_Payout](
	[Action_Option_Cash_Payout_Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [varchar](32) NULL,
	[CorporateActionReference] [varchar](16) NULL,
	[CAOptionNumber] [numeric](3, 0) NULL,
	[SuboptionSequence] [numeric](3, 0) NULL,
	[CreditDebitIndicator] [varchar](8) NULL,
	[TypeofIncome] [varchar](8) NULL,
	[OtherTypeofIncome] [varchar](8) NULL,
	[EntitledAmountCurrency] [varchar](3) NULL,
	[EntitledAmount] [decimal](30, 15) NULL,
	[FXRateFixingDate] [datetime] NULL,
	[EarlyDeadlineDate] [datetime] NULL,
	[PaymentDate] [datetime] NULL,
	[ValueDate] [datetime] NULL,
	[ChargeAndFeesCurrency] [varchar](3) NULL,
	[ChargeAndFeesAmount] [decimal](30, 15) NULL,
	[ExchangeRateCurrencyA] [varchar](3) NULL,
	[ExchangeRateCurrencyB] [varchar](3) NULL,
	[ExchangeRate] [decimal](30, 15) NULL,
	[GrossDividendType] [varchar](8) NULL,
	[GrossDividendCode] [varchar](8) NULL,
	[GrossDividendCurrency] [varchar](3) NULL,
	[GrossDividendAmount] [decimal](30, 15) NULL,
	[CalculatedInterestRateType] [varchar](8) NULL,
	[CalculatedInterestRateCode] [varchar](8) NULL,
	[CalculatedInterest] [decimal](30, 15) NULL,
	[NetDividendType] [varchar](8) NULL,
	[NetDividendCode] [varchar](8) NULL,
	[NetDividendCurrency] [varchar](3) NULL,
	[NetDividendRate] [decimal](30, 15) NULL,
	[ApplicableRateType] [varchar](8) NULL,
	[ApplicableRateCode] [varchar](8) NULL,
	[ApplicableRate] [decimal](30, 15) NULL,
	[SollicitationRateType] [varchar](8) NULL,
	[SollicitationRateCode] [varchar](8) NULL,
	[SollicitationRate] [decimal](30, 15) NULL,
	[WithHoldingTaxRateType] [varchar](8) NULL,
	[WithHoldingTaxRateCode] [varchar](8) NULL,
	[WithHoldingTaxRate] [decimal](30, 15) NULL,
	[CashPriceCode] [varchar](8) NULL,
	[CashPriceType] [varchar](8) NULL,
	[CashPriceCurrency] [varchar](3) NULL,
	[CashPriceAmount] [decimal](30, 15) NULL,
	[OfferPriceCode] [varchar](8) NULL,
	[OfferPriceType] [varchar](8) NULL,
	[OfferPriceCurrency] [varchar](3) NULL,
	[OfferPriceAmount] [decimal](30, 15) NULL,
	[Status] [varchar](10) NULL,
	[CreationDate] [datetime] NULL,
	[ReceiptDate] [datetime] NULL,
	[ModificatioinDate] [datetime] NULL,
	[CancelationDate] [datetime] NULL,
	[CreatedBy] [varchar](35) NULL,
	[ModifiedBy] [varchar](35) NULL,
	[CancelledBy] [varchar](35) NULL,
 CONSTRAINT [PK_Action_Option_Cash_Payout] PRIMARY KEY CLUSTERED 
(
	[Action_Option_Cash_Payout_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Action_Option_Cash_Payout]  WITH CHECK ADD  CONSTRAINT [FK_Action_Option_Cash_Payout_Announcement_Event] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Announcement_Event] ([TransactionId])
GO

ALTER TABLE [dbo].[Action_Option_Cash_Payout] CHECK CONSTRAINT [FK_Action_Option_Cash_Payout_Announcement_Event]
GO


------
USE [CorporateActions]
GO

ALTER TABLE [dbo].[Action_Option_Security_Movement] DROP CONSTRAINT [FK_Action_Option_Security_Movement_Announcement_Event]
GO

/****** Object:  Table [dbo].[Action_Option_Security_Movement]    Script Date: 11/28/2024 1:55:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Action_Option_Security_Movement]') AND type in (N'U'))
DROP TABLE [dbo].[Action_Option_Security_Movement]
GO

/****** Object:  Table [dbo].[Action_Option_Security_Movement]    Script Date: 11/28/2024 1:55:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Action_Option_Security_Movement](
	[Action_Option_Security_Movement_Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [varchar](32) NULL,
	[CorporateActionReference] [varchar](16) NULL,
	[CAOptionNumber] [numeric](3, 0) NULL,
	[SuboptionSequence] [numeric](3, 0) NULL,
	[CreditDebitIndicator] [varchar](8) NULL,
	[TemporaryIndicator] [varchar](8) NULL,
	[NewSecuritiesIssuanceIndicator] [varchar](8) NULL,
	[NonEligibleProceedsIndicator] [varchar](8) NULL,
	[IssuerOfferorTaxabilityIndicator] [varchar](8) NULL,
	[TypeofIncome] [varchar](8) NULL,
	[OtherTypeofIncome] [varchar](8) NULL,
	[SecurityIdType] [varchar](12) NULL,
	[SecurityIdentifier] [varchar](12) NULL,
	[SecurityDescription] [varchar](140) NULL,
	[InternalsecurityID] [varchar](12) NULL,
	[MethodofInterestComputation] [varchar](10) NULL,
	[TypeofFinancialInstrument] [varchar](35) NULL,
	[OptionStyle] [varchar](8) NULL,
	[CurrencyDenomination] [varchar](3) NULL,
	[IssuePriceCode] [varchar](8) NULL,
	[IssuePriceType] [varchar](8) NULL,
	[IssuePriceCurrency] [varchar](3) NULL,
	[IssuePriceAmount] [decimal](30, 15) NULL,
	[CashPricePaidPerProductCode] [varchar](8) NULL,
	[CashPricePaidPerProductType] [varchar](8) NULL,
	[CashPricePaidPerProductCurrency] [varchar](3) NULL,
	[CashPricePaidPerProductAmount] [decimal](30, 15) NULL,
	[MinimumExQtyCode] [varchar](8) NULL,
	[MinimumExercisableQuantity] [decimal](15, 0) NULL,
	[MinimumLotQtyCode] [varchar](8) NULL,
	[MinimumExercisableMultipleLotQuantity] [decimal](15, 0) NULL,
	[MinimumNomtQtyCode] [varchar](8) NULL,
	[MinimumNominalQuantity] [decimal](15, 0) NULL,
	[PostingQuantityiCode] [varchar](8) NULL,
	[PostingQuantity] [decimal](15, 0) NULL,
	[PlaceOfSafekeepingCode] [varchar](8) NULL,
	[PlaceOfSafekeeping] [varchar](30) NULL,
	[DispositionOfFactionType] [varchar](8) NULL,
	[DispositionOfFactionCode] [varchar](8) NULL,
	[CurrencyOption] [varchar](3) NULL,
	[CashInLieuCode] [varchar](8) NULL,
	[CashInLieuType] [varchar](8) NULL,
	[CashInLieuCurrency] [varchar](3) NULL,
	[CashInLieuAmount] [decimal](30, 15) NULL,
	[IndicativeMarketPriceCode] [varchar](8) NULL,
	[IndicativeMarketPriceType] [varchar](8) NULL,
	[IndicativeMarketPriceCurrency] [varchar](3) NULL,
	[IndicativeMarketPriceAmount] [decimal](30, 15) NULL,
	[RateQuantityToOldSecurities ] [decimal](30, 15) NULL,
	[RateQuantityOfNewSecurities] [decimal](30, 15) NULL,
	[WithholdingTaxRateType] [varchar](8) NULL,
	[WithholdingTaxRateCode] [varchar](8) NULL,
	[WithholdingTaxRate] [decimal](30, 15) NULL,
	[SecondLevelTaxRateType] [varchar](8) NULL,
	[SecondLevelTaxRateCode] [varchar](8) NULL,
	[SecondLevelTaxRate] [decimal](30, 15) NULL,
	[TransactionTaxRateType] [varchar](8) NULL,
	[TransactionTaxRateCode] [varchar](8) NULL,
	[TransactionTaxRate] [decimal](30, 15) NULL,
	[DividendRankingDate] [datetime] NULL,
	[PaymentDate] [datetime] NULL,
	[PariPassuDate] [datetime] NULL,
	[PaymentCurrency] [varchar](3) NULL,
	[PaymentAmount] [decimal](30, 15) NULL,
	[Status] [varchar](10) NULL,
	[CreationDate] [datetime] NULL,
	[ReceiptDate] [datetime] NULL,
	[ModificatioinDate] [datetime] NULL,
	[CancelationDate] [datetime] NULL,
	[CreatedBy] [varchar](35) NULL,
	[ModifiedBy] [varchar](35) NULL,
	[CancelledBy] [varchar](35) NULL,
 CONSTRAINT [PK_Action_Option_Security_Movement] PRIMARY KEY CLUSTERED 
(
	[Action_Option_Security_Movement_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Action_Option_Security_Movement]  WITH CHECK ADD  CONSTRAINT [FK_Action_Option_Security_Movement_Announcement_Event] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Announcement_Event] ([TransactionId])
GO

ALTER TABLE [dbo].[Action_Option_Security_Movement] CHECK CONSTRAINT [FK_Action_Option_Security_Movement_Announcement_Event]
GO


