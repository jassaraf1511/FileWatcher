IF OBJECT_ID('SP_Insert_Addenda', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_Addenda
END
GO
  CREATE PROCEDURE SP_Insert_Addenda @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @MessageId varchar(32) = NULL,
  @SequenceNumber int = NULL,
  @AddendaLength int = NULL,
  @AddendaInformation varchar(Max) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.Addenda (
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    MessageId,
    SequenceNumber,
    AddendaLength,
    AddendaInformation
  )
  output inserted.*
SELECT
  @MessageReference,
  @MessageOrigin,
  @OrigineName,
  @IncomingOutGoing,
  @MessageId,
  @SequenceNumber,
  @AddendaLength,
  @AddendaInformation;
set
  @ID = @@IDENTITY
END
GO
