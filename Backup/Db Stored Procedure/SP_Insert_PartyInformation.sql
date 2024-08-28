IF OBJECT_ID('SP_Insert_PartyInformation', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_PartyInformation
END
GO
  CREATE PROCEDURE SP_Insert_PartyInformation @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @TagId varchar(64) = NULL,
  @MessageId varchar(32) = NULL,
  @SequenceNumber int = NULL,
  @PartyInfo varchar(30) = NULL,
  @PartyInfoL1 varchar(33) = NULL,
  @PartyInfoL2 varchar(33) = NULL,
  @PartyInfoL3 varchar(33) = NULL,
  @PartyInfoL4 varchar(33) = NULL,
  @PartyInfoL5 varchar(33) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.PartyInformation (
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    TagId,
    MessageId,
    SequenceNumber,
    PartyInfo,
    PartyInfoL1,
    PartyInfoL2,
    PartyInfoL3,
    PartyInfoL4,
    PartyInfoL5
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
  @PartyInfo,
  @PartyInfoL1,
  @PartyInfoL2,
  @PartyInfoL3,
  @PartyInfoL4,
  @PartyInfoL5;
set
  @ID = @@IDENTITY
END
GO