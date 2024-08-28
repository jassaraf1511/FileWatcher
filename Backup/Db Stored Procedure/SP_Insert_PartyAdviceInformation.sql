IF OBJECT_ID('SP_Insert_PartyAdviceInformation', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_PartyAdviceInformation
END
GO
  CREATE PROCEDURE SP_Insert_PartyAdviceInformation @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @MessageId varchar(32) = NULL,
  @TagId varchar(64) = NULL,
  @SequenceNumber int = NULL,
  @AdviceCode varchar(3) = NULL,
  @AdviceInfo varchar(26) = NULL,
  @AdviceInfoL1 varchar(33) = NULL,
  @AdviceInfoL2 varchar(33) = NULL,
  @AdviceInfoL3 varchar(33) = NULL,
  @AdviceInfoL4 varchar(33) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.PartyAdviceInformation (
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    MessageId,
    TagId,
    SequenceNumber,
    AdviceCode,
    AdviceInfo,
    AdviceInfoL1,
    AdviceInfoL2,
    AdviceInfoL3,
    AdviceInfoL4
  )
output inserted.*
SELECT
  @MessageReference,
  @MessageOrigin,
  @OrigineName,
  @IncomingOutGoing,
  @MessageId,
  @TagId,
  @SequenceNumber,
  @AdviceCode,
  @AdviceInfo,
  @AdviceInfoL1,
  @AdviceInfoL2,
  @AdviceInfoL3,
  @AdviceInfoL4;
set
  @ID = @@IDENTITY
END
GO