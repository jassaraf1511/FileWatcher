IF OBJECT_ID('SP_Insert_RawMessage', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_RawMessage
END
GO
  CREATE PROCEDURE SP_Insert_RawMessage @MessageId varchar(32),
  @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @FileId numeric = NULL,
  @ClientId varchar(32) = NULL,
  @DateReceived datetime = NULL,
  @DateLoaded datetime = NULL,
  @LoadStatus varchar(16) = NULL,
  @LoadRejectReason varchar(512) = NULL,
  @UserId varchar(32) = NULL,
  @DataRecord varchar(Max) = NULL,
  @CreationDate datetime = NULL,
  @CreatorId varchar(32) = NULL,
  @UpdateDate datetime = NULL,
  @UpdatorId varchar(32) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.RawMessage (
    MessageId,
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    FileId,
    ClientId,
    DateReceived,
    DateLoaded,
    LoadStatus,
    LoadRejectReason,
    UserId,
    DataRecord,
    CreationDate,
    CreatorId,
    UpdateDate,
    UpdatorId
  )
output inserted.*
SELECT
  @MessageId,
  @MessageReference,
  @MessageOrigin,
  @OrigineName,
  @IncomingOutGoing,
  @FileId,
  @ClientId,
  @DateReceived,
  @DateLoaded,
  @LoadStatus,
  @LoadRejectReason,
  @UserId,
  @DataRecord,
  @CreationDate,
  @CreatorId,
  @UpdateDate,
  @UpdatorId;
set
  @ID = @@IDENTITY
END
GO