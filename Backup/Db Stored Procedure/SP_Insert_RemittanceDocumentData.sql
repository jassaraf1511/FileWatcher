IF OBJECT_ID('SP_Insert_RemittanceDocumentData', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_RemittanceDocumentData
END
GO
  CREATE PROCEDURE SP_Insert_RemittanceDocumentData @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @MessageId varchar(32) = NULL,
  @SequenceNumber int = NULL,
  @PrimaryDocumentTypeCode varchar(4) = NULL,
  @PrimaryProprietaryDocumentTypeCode varchar(35) = NULL,
  @PrimaryDocumentIdentificationNumber varchar(35) = NULL,
  @PrimaryIssuer varchar(35) = NULL,
  @NegotiatedDiscountCurrency varchar(3) = NULL,
  @NegotiatedDiscountAmount decimal = NULL,
  @AdjustmentReasonCode varchar(2) = NULL,
  @AdjustmentCreditDebitIndicator varchar(4) = NULL,
  @AdjustmentCurrencyCode varchar(3) = NULL,
  @AdjustmentAmount decimal = NULL,
  @AdjustmentAdditionalInformation varchar(140) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.RemittanceDocumentData (
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    MessageId,
    SequenceNumber,
    PrimaryDocumentTypeCode,
    PrimaryProprietaryDocumentTypeCode,
    PrimaryDocumentIdentificationNumber,
    PrimaryIssuer,
    NegotiatedDiscountCurrency,
    NegotiatedDiscountAmount,
    AdjustmentReasonCode,
    AdjustmentCreditDebitIndicator,
    AdjustmentCurrencyCode,
    AdjustmentAmount,
    AdjustmentAdditionalInformation
  )
output inserted.*
SELECT
  @MessageReference,
  @MessageOrigin,
  @OrigineName,
  @IncomingOutGoing,
  @MessageId,
  @SequenceNumber,
  @PrimaryDocumentTypeCode,
  @PrimaryProprietaryDocumentTypeCode,
  @PrimaryDocumentIdentificationNumber,
  @PrimaryIssuer,
  @NegotiatedDiscountCurrency,
  @NegotiatedDiscountAmount,
  @AdjustmentReasonCode,
  @AdjustmentCreditDebitIndicator,
  @AdjustmentCurrencyCode,
  @AdjustmentAmount,
  @AdjustmentAdditionalInformation;
set
  @ID = @@IDENTITY
END
GO