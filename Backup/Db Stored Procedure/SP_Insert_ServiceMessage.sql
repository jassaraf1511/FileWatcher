IF OBJECT_ID('SP_Insert_ServiceMessage', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_ServiceMessage
END
GO
  CREATE PROCEDURE SP_Insert_ServiceMessage @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @MessageId varchar(32) = NULL,
  @SequenceNumber int = NULL,
  @MessageInformation varchar(35) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.ServiceMessage (
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    MessageId,
    SequenceNumber,
    MessageInformation
  )
output inserted.*
SELECT
  @MessageReference,
  @MessageOrigin,
  @OrigineName,
  @IncomingOutGoing,
  @MessageId,
  @SequenceNumber,
  @MessageInformation;
set
  @ID = @@IDENTITY
END
GO