USE [FundWireInterfaceDB] 
GO 

IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'SP_Insert_RawMessage')
BEGIN
    DROP 	PROC SP_Insert_RawMessage 
END
GO 
  CREATE PROCEDURE SP_Insert_RawMessage
	@MessageId                       varchar(64),
	@MessageOrigin                   varchar(10) = NULL,
	@OrigineName                     varchar(64) = NULL,
	@IncomingOutGoing                varchar(16) = NULL,
	@IMAD                            varchar(34) = NULL,
	@OMAD                            varchar(34) = NULL,
	@InputCycleDate                  varchar(8) = NULL,
	@InputSource                     varchar(8) = NULL,
	@InputSequenceNumber             varchar(6) = NULL,
	@OutputCycleDateDateReceived     varchar(8) = NULL,
	@OutputDestination               varchar(8) = NULL,
	@OutputSequenceNumber            varchar(6) = NULL,
	@OutputDate                      varchar(4) = NULL,
	@OutputTime                      varchar(4) = NULL,
	@FileId 						 varchar(32)=NULL,
	@ClientId                        varchar(32) = NULL,
	@DateReceived                    datetime = NULL,
	@DateLoaded                      datetime = NULL,
	@LoadStatus                      varchar(16) = NULL,
	@LoadRejectReason                varchar(512) = NULL,
	@DataRecord                      varchar(Max) = NULL,
	@CreationDate                    datetime = NULL,
	@CreatorId                       varchar(32) = NULL,
	@UpdateDate                      datetime = NULL,
	@UpdatorId                       varchar(32) = NULL,
	@DeletedDate                     datetime = NULL,
	@DeleteddBy                      varchar(32) = NULL,
	@InternalId numeric(18, 0) out
  AS 
  BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.RawMessage WHERE IMAD = @IMAD and MessageOrigin = @MessageOrigin and OrigineName = @OrigineName and IncomingOutGoing = @IncomingOutGoing)	
		BEGIN

			INSERT INTO dbo.RawMessage
			(
				MessageId,
				MessageOrigin,
				OrigineName,
				IncomingOutGoing,
				FileId,
				ClientId,
				IMAD,
				OMAD,
				InputCycleDate,
				InputSource,
				InputSequenceNumber,
				OutputCycleDateDateReceived,
				OutputDestination,
				OutputSequenceNumber,
				OutputDate,
				OutputTime,	
				DateReceived,
				DateLoaded,
				LoadStatus,
				LoadRejectReason,
				DataRecord,
				CreationDate,
				CreatorId,
				UpdateDate,
				UpdatorId,
				DeletedDate,
				DeleteddBy
			  )
			output inserted.*
			SELECT
			  @MessageId,
			  @MessageOrigin,
			  @OrigineName,
			  @IncomingOutGoing,
			  @FileId,
			  @ClientId,
			  @IMAD,
			  @OMAD,
			  @InputCycleDate,
			  @InputSource,
			  @InputSequenceNumber,
			  @OutputCycleDateDateReceived,
			  @OutputDestination,
			  @OutputSequenceNumber,
			  @OutputDate,
			  @OutputTime,	  
			  @DateReceived,
			  @DateLoaded,
			  @LoadStatus,
			  @LoadRejectReason,
			  @DataRecord,
			  @CreationDate,
			  @CreatorId,
			  @UpdateDate,
			  @UpdatorId,
			  @DeletedDate,
			  @DeleteddBy;			  
			set   @InternalId = @@IDENTITY
		END
	ELSE
		BEGIN 
			set  @InternalId =0
		END 
END
GO