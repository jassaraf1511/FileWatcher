IF OBJECT_ID('SP_Insert_OriginatorOptionF', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_OriginatorOptionF
END
GO
  CREATE PROCEDURE SP_Insert_OriginatorOptionF @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @TagId varchar(64) = NULL,
  @MessageId varchar(32) = NULL,
  @SequenceNumber int = NULL,
  @OrginatorOptionFCode1 varchar(1) = NULL,
  @OrginatorOptionFLine1 varchar(34) = NULL,
  @OrginatorOptionFCode2 varchar(1) = NULL,
  @OrginatorOptionFLine2 varchar(34) = NULL,
  @OrginatorOptionFCode3 varchar(1) = NULL,
  @OrginatorOptionFLine3 varchar(34) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.OriginatorOptionF (
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    TagId,
    MessageId,
    SequenceNumber,
    OrginatorOptionFCode1,
    OrginatorOptionFLine1,
    OrginatorOptionFCode2,
    OrginatorOptionFLine2,
    OrginatorOptionFCode3,
    OrginatorOptionFLine3
  )
     output inserted.*
SELECT
  @MessageReference,
  @MessageOrigin,
  @OrigineName,
  @IncomingOutGoing,
  @TagId,
  @MessageId,
  @SequenceNumber,
  @OrginatorOptionFCode1,
  @OrginatorOptionFLine1,
  @OrginatorOptionFCode2,
  @OrginatorOptionFLine2,
  @OrginatorOptionFCode3,
  @OrginatorOptionFLine3;
set
  @ID = @@IDENTITY
END
GO