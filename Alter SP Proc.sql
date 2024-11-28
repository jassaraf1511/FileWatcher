USE [CorporateActions]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_Action_Option_Cash_Payout]    Script Date: 11/28/2024 1:07:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROC [dbo].[sp_insert_Action_Option_Cash_Payout]
(
	---@Action_Option_Cash_Payout_Id as int,
	@TransactionId as varchar(32),
	@CorporateActionReference as varchar(16) = Null,
	@CAOptionNumber as numeric = Null,
	@SuboptionSequence as numeric = Null,
	@CreditDebitIndicator as varchar(8) = Null,
	@TypeofIncome as varchar(8) = Null,
	@OtherTypeofIncome as varchar(8) = Null,
	@EntitledAmountCurrency as varchar(3) = Null,
	@EntitledAmount as decimal (30,15) = Null,
	@FXRateFixingDate as datetime = Null,
	@EarlyDeadlineDate as datetime = Null,
	@PaymentDate as datetime = Null,
	@ValueDate as datetime = Null,
	@ChargeAndFeesCurrency as varchar(3) = Null,
	@ChargeAndFeesAmount as decimal (30,15) = Null,
	@ExchangeRateCurrencyA as varchar(3) = Null,
	@ExchangeRateCurrencyB as varchar(3) = Null,
	@ExchangeRate as decimal (30,15) = Null,
	@GrossDividendType  as varchar(8) = Null,
	@GrossDividendCode  as varchar(8) = Null,
	@GrossDividendCurrency as varchar(3) = Null,
	@GrossDividendAmount as decimal (30,15) = Null,
	@CalculatedInterestRateType as varchar(8) = Null,
	@CalculatedInterestRateCode as varchar(8) = Null,
	@CalculatedInterest as decimal (30,15) = Null,
	@NetDividendType as varchar(8) = Null,
	@NetDividendCode as varchar(8) = Null,	
	@NetDividendCurrency as varchar(3) = Null,
	@NetDividendRate as decimal (30,15) = Null,
	@ApplicableRateType as varchar(8) = Null,
	@ApplicableRateCode as varchar(8) = Null,
	@ApplicableRate as decimal (30,15) = Null,
	@SollicitationRateType as varchar(8) = Null,
	@SollicitationRateCode as varchar(8) = Null,
	@SollicitationRate as decimal (30,15) = Null,
	@WithHoldingTaxRateType as varchar(8) = Null,
	@WithHoldingTaxRateCode as varchar(8) = Null,
	@WithHoldingTaxRate as decimal (30,15) = Null,
	@CashPriceCode as varchar(8) = Null,
	@CashPriceType as varchar(8) = Null,
	@CashPriceCurrency as varchar(3) = Null,
	@CashPriceAmount as decimal (30,15) = Null,
	@OfferPriceCode as varchar(8) = Null,
	@OfferPriceType as varchar(8) = Null,
	@OfferPriceCurrency as varchar(3) = Null,
	@OfferPriceAmount as decimal (30,15) = Null,
	@Status as varchar(10) = Null,
	@CreationDate as datetime = Null,
	@ReceiptDate as datetime = Null,
	@ModificatioinDate as datetime = Null,
	@CancelationDate as datetime = Null,
	@CreatedBy as varchar(35) = Null,
	@ModifiedBy as varchar(35) = Null,
	@CancelledBy as varchar(35) = Null

)
AS

begin transaction

	begin try

		-- insert
			insert [dbo].[Action_Option_Cash_Payout] (
			--	Action_Option_Cash_Payout_Id,
				TransactionId,
				CorporateActionReference,
				CAOptionNumber,
				SuboptionSequence,
				CreditDebitIndicator,
				TypeofIncome,
				OtherTypeofIncome,
				EntitledAmountCurrency,
				EntitledAmount,
				FXRateFixingDate,
				EarlyDeadlineDate,
				PaymentDate,
				ValueDate,
				ChargeAndFeesCurrency,
				ChargeAndFeesAmount,
				ExchangeRateCurrencyA,
				ExchangeRateCurrencyB,
				ExchangeRate,
				GrossDividendType,
				GrossDividendCode,
				GrossDividendCurrency,
				GrossDividendAmount,
				CalculatedInterestRateType,
				CalculatedInterestRateCode,
				CalculatedInterest,
				NetDividendType,
				NetDividendCode,
				NetDividendCurrency,
				NetDividendRate,
				ApplicableRateType,
				ApplicableRateCode,
				ApplicableRate,
				SollicitationRateCode,
				SollicitationRate,
				WithHoldingTaxRateType,
				WithHoldingTaxRateCode,
				WithHoldingTaxRate,
				CashPriceCode,
				CashPriceType,
				CashPriceCurrency,
				CashPriceAmount,
				OfferPriceCode,
				OfferPriceType,
				OfferPriceCurrency,
				OfferPriceAmount,
				Status,
				CreationDate,
				ReceiptDate,
				ModificatioinDate,
				CancelationDate,
				CreatedBy,
				ModifiedBy,
				CancelledBy)
			values (
			--	@Action_Option_Cash_Payout_Id,
@TransactionId,
				@CorporateActionReference,
				@CAOptionNumber,
				@SuboptionSequence,
				@CreditDebitIndicator,
				@TypeofIncome,
				@OtherTypeofIncome,
				@EntitledAmountCurrency,
				@EntitledAmount,
				@FXRateFixingDate,
				@EarlyDeadlineDate,
				@PaymentDate,
				@ValueDate,
				@ChargeAndFeesCurrency,
				@ChargeAndFeesAmount,
				@ExchangeRateCurrencyA,
				@ExchangeRateCurrencyB,
				@ExchangeRate,
				@GrossDividendType,
				@GrossDividendCode,
				@GrossDividendCurrency,
				@GrossDividendAmount,
				@CalculatedInterestRateType,
				@CalculatedInterestRateCode,
				@CalculatedInterest,
				@NetDividendType,
				@NetDividendCode,
				@NetDividendCurrency,
				@NetDividendRate,
				@ApplicableRateType,
				@ApplicableRateCode,
				@ApplicableRate,
				@SollicitationRateCode,
				@SollicitationRate,
				@WithHoldingTaxRateType,
				@WithHoldingTaxRateCode,
				@WithHoldingTaxRate,
				@CashPriceCode,
				@CashPriceType,
				@CashPriceCurrency,
				@CashPriceAmount,
				@OfferPriceCode,
				@OfferPriceType,
				@OfferPriceCurrency,
				@OfferPriceAmount,
				@Status,
				@CreationDate,
				@ReceiptDate,
				@ModificatioinDate,
				@CancelationDate,
				@CreatedBy,
				@ModifiedBy,
				@CancelledBy)
		-- Return the new ID
		select SCOPE_IDENTITY();

		commit transaction

	end try

	begin catch

		declare @ErrorMessage NVARCHAR(4000);
		declare @ErrorSeverity INT;
		declare @ErrorState INT;

		select @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

		raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);

		rollback transaction

	end catch;
				
--

USE [CorporateActions]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_Action_Option_Security_Movement]    Script Date: 11/28/2024 1:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[sp_insert_Action_Option_Security_Movement]
(
	---@Action_Option_Security_Movement_Id as int,
	@TransactionId as varchar(32) = Null,
	@CorporateActionReference as varchar(16) = Null,
	@CAOptionNumber as numeric = Null,
	@SuboptionSequence as numeric = Null,
	@CreditDebitIndicator as [varchar](8) = Null,
	@TemporaryIndicator as [varchar](8) = Null,
	@NewSecuritiesIssuanceIndicator as [varchar](8) = Null,
	@NonEligibleProceedsIndicator as [varchar](8) = Null,
	@IssuerOfferorTaxabilityIndicator as [varchar](8) = Null,
	@TypeofIncome as [varchar](8) = Null,
	@OtherTypeofIncome as [varchar](8) = Null,
	@SecurityIdType as varchar(12) = Null,
	@SecurityIdentifier as varchar(12) = Null,
	@SecurityDescription as varchar(140) = Null,
	@InternalsecurityID as varchar(12) = Null,
	@MethodofInterestComputation as varchar(10) = Null,
	@TypeofFinancialInstrument as varchar(35) = Null,
	@OptionStyle as [varchar](8) = Null,
	@CurrencyDenomination as [varchar](8) = Null,
	@IssuePriceCode as [varchar](8) = Null,
	@IssuePriceType as [varchar](8) = Null,
	@IssuePriceCurrency as varchar(3) = Null,
	@IssuePriceAmount as decimal (30,15) = Null,
	@CashPricePaidPerProductCode as [varchar](8) NULL,
    @CashPricePaidPerProductType as [varchar](8) NULL,
    @CashPricePaidPerProductCurrency as varchar(3) NULL,
    @CashPricePaidPerProductAmount as decimal(30, 15) NULL,
	@MinimumExQtyCode as [varchar](8) = Null,
	@MinimumExercisableQuantity as decimal (30,15) = Null,
	@MinimumLotQtyCode as [varchar](8) = Null,
	@MinimumExercisableMultipleLotQuantity as decimal (30,15) = Null,
	@MinimumNomtQtyCode as [varchar](8) = Null,
	@MinimumNominalQuantity as decimal (30,15) = Null,
	@PostingQuantityiCode as [varchar](8) = Null,
	@PostingQuantity as decimal (30,15) = Null,
	@PlaceOfSafekeepingCode as [varchar](8) = Null,
	@PlaceOfSafekeeping as varchar(30) = Null,
	@DispositionOfFactionType as varchar(8) = Null,
	@DispositionOfFactionCode as [varchar](8) = Null,
	@CurrencyOption as varchar(3) = Null,
	@CashInLieuCode as [varchar](8) = Null,
	@CashInLieuType as [varchar](8) = Null,
	@CashInLieuCurrency as varchar(3) = Null,
	@CashInLieuAmount as decimal (30,15) = Null,
	@IndicativeMarketPriceCode as [varchar](8) = Null,
	@IndicativeMarketPriceType as [varchar](8) = Null,
	@IndicativeMarketPriceCurrency as varchar(3) = Null,
	@IndicativeMarketPriceAmount as decimal (30,15) = Null,
	@RateQuantityToOldSecurities as decimal (30,15) = Null,
	@RateQuantityOfNewSecurities  as decimal (30,15) = Null,
	@WithholdingTaxRateType as [varchar](8) = Null,
	@WithholdingTaxRateCode as [varchar](8) = Null,
	@WithholdingTaxRate as decimal (30,15) = Null,
	@SecondLevelTaxRateType as [varchar](8) = Null,
	@SecondLevelTaxRateCode as [varchar](8) = Null,
	@SecondLevelTaxRate as decimal (30,15) = Null,
	@TransactionTaxRateType as [varchar](8) = Null,
	@TransactionTaxRateCode as [varchar](8) = Null,
	@TransactionTaxRate as decimal (30,15) = Null,
	@DividendRankingDate as datetime = Null,
	@PaymentDate as datetime = Null,
	@PariPassuDate as datetime = Null,
	@PaymentCurrency as varchar(3) = Null,
	@PaymentAmount as decimal (30,15) = Null,
	@Status as varchar(10) = Null,
	@CreationDate as datetime = Null,
	@ReceiptDate as datetime = Null,
	@ModificatioinDate as datetime = Null,
	@CancelationDate as datetime = Null,
	@CreatedBy as varchar(35) = Null,
	@ModifiedBy as varchar(35) = Null,
	@CancelledBy as varchar(35) = Null

)
AS

begin transaction

	begin try

		-- insert
			insert [dbo].[Action_Option_Security_Movement] (
			---	Action_Option_Security_Movement_Id,
				TransactionId,
				CorporateActionReference,
				CAOptionNumber,
				SuboptionSequence,
				CreditDebitIndicator,
				TemporaryIndicator,
				NewSecuritiesIssuanceIndicator,
				NonEligibleProceedsIndicator,
				IssuerOfferorTaxabilityIndicator,
				TypeofIncome,
				OtherTypeofIncome,
				SecurityIdType,
				SecurityIdentifier,
				SecurityDescription,
				InternalsecurityID,
				MethodofInterestComputation,
				TypeofFinancialInstrument,
				OptionStyle,
				CurrencyDenomination,
				IssuePriceCode,
				IssuePriceType,
				IssuePriceCurrency,
				IssuePriceAmount,
				CashPricePaidPerProductCode, 
				CashPricePaidPerProductType, 
				CashPricePaidPerProductCurrency, 
				CashPricePaidPerProductAmount, 
				MinimumExQtyCode,
				MinimumExercisableQuantity,
				MinimumLotQtyCode,
				MinimumExercisableMultipleLotQuantity,
				MinimumNomtQtyCode,
				MinimumNominalQuantity,
				PostingQuantityiCode,
				PostingQuantity,
				PlaceOfSafekeepingCode,
				PlaceOfSafekeeping,
				DispositionOfFactionType,
				DispositionOfFactionCode,
				CurrencyOption,
				CashInLieuCode,
				CashInLieuType,
				CashInLieuCurrency,
				CashInLieuAmount,
				IndicativeMarketPriceCode,
				IndicativeMarketPriceType,
				IndicativeMarketPriceCurrency,
				IndicativeMarketPriceAmount,
				RateQuantityToOldSecurities,
				RateQuantityOfNewSecurities ,
				WithholdingTaxRateType,
				WithholdingTaxRateCode,
				WithholdingTaxRate,
				SecondLevelTaxRateType,
				SecondLevelTaxRateCode,
				SecondLevelTaxRate,
				TransactionTaxRateType,
				TransactionTaxRateCode,
				TransactionTaxRate,
				DividendRankingDate,
				PaymentDate,
				PariPassuDate,
				PaymentCurrency,
				PaymentAmount,
				Status,
				CreationDate,
				ReceiptDate,
				ModificatioinDate,
				CancelationDate,
				CreatedBy,
				ModifiedBy,
				CancelledBy)
			values (
			---	@Action_Option_Security_Movement_Id,
				@TransactionId,
				@CorporateActionReference,
				@CAOptionNumber,
				@SuboptionSequence,
				@CreditDebitIndicator,
				@TemporaryIndicator,
				@NewSecuritiesIssuanceIndicator,
				@NonEligibleProceedsIndicator,
				@IssuerOfferorTaxabilityIndicator,
				@TypeofIncome,
				@OtherTypeofIncome,
				@SecurityIdType,
				@SecurityIdentifier,
				@SecurityDescription,
				@InternalsecurityID,
				@MethodofInterestComputation,
				@TypeofFinancialInstrument,
				@OptionStyle,
				@CurrencyDenomination,
				@IssuePriceCode,
				@IssuePriceType,
				@IssuePriceCurrency,
				@IssuePriceAmount,
				@CashPricePaidPerProductCode, 
				@CashPricePaidPerProductType, 
				@CashPricePaidPerProductCurrency, 
				@CashPricePaidPerProductAmount, 
				@MinimumExQtyCode,
				@MinimumExercisableQuantity,
				@MinimumLotQtyCode,
				@MinimumExercisableMultipleLotQuantity,
				@MinimumNomtQtyCode,
				@MinimumNominalQuantity,
				@PostingQuantityiCode,
				@PostingQuantity,
				@PlaceOfSafekeepingCode,
				@PlaceOfSafekeeping,
				@DispositionOfFactionType,
				@DispositionOfFactionCode,
				@CurrencyOption,
				@CashInLieuCode,
				@CashInLieuType,
				@CashInLieuCurrency,
				@CashInLieuAmount,
				@IndicativeMarketPriceCode,
				@IndicativeMarketPriceType,
				@IndicativeMarketPriceCurrency,
				@IndicativeMarketPriceAmount,
				@RateQuantityToOldSecurities,
				@RateQuantityOfNewSecurities ,
				@WithholdingTaxRateType,
				@WithholdingTaxRateCode,
				@WithholdingTaxRate,
				@SecondLevelTaxRateType,
				@SecondLevelTaxRateCode,
				@SecondLevelTaxRate,
				@TransactionTaxRateType,
				@TransactionTaxRateCode,
				@TransactionTaxRate,
				@DividendRankingDate,
				@PaymentDate,
				@PariPassuDate,
				@PaymentCurrency,
				@PaymentAmount,
				@Status,
				@CreationDate,
				@ReceiptDate,
				@ModificatioinDate,
				@CancelationDate,
				@CreatedBy,
				@ModifiedBy,
				@CancelledBy)


		-- Return the new ID
		select SCOPE_IDENTITY();

		commit transaction

	end try

	begin catch

		declare @ErrorMessage NVARCHAR(4000);
		declare @ErrorSeverity INT;
		declare @ErrorState INT;

		select @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

		raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);

		rollback transaction

	end catch;





