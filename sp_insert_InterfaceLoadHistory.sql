USE [FundWireInterfaceDB] 
GO 

IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'SP_InterfaceLoadHistory')
BEGIN
    DROP 	PROC SP_InterfaceLoadHistory 
END
GO 
CREATE PROCEDURE SP_InterfaceLoadHistory
	  @FileId varchar(32), 
	  @IncomingOutGoing varchar(16) = NULL, 
	  @FilePath varchar(256) = NULL, 
	  @Filename varchar(256) = NULL, 
	  @InterfaceIdentifier varchar(64) = NULL, 
	  @ClientId varchar(63) = NULL, 
	  @InterfaceName varchar(64) = NULL, 
	  @InterfaceType varchar(32) = NULL, 
	  @LoadProcessId varchar(32) = NULL, 
	  @LoadProcessName varchar(32) = NULL, 
	  @LoadDate datetime = NULL, 
	  @ProcessedDate datetime = NULL, 
	  @BusinessDate datetime = NULL, 
	  @FileReceivedDate datetime = NULL, 
	  @RecordsNumber int = NULL, 
	  @RecordProcessed int = NULL, 
	  @RecordRejected int = NULL, 
	  @FileSize int = NULL, 
	  @RejectReasonCode varchar(32) = NULL, 
	  @RejectReasonDescription varchar(512) = NULL, 
	  @LoadStatus varchar(16) = NULL, 
	  @DataFileContent varchar(MAX) = NULL, 
	  @CreationDate datetime = NULL, 
	  @CreatedBy varchar(32) = NULL, 
	  @UpdateDate datetime = NULL, 
	  @UpdatedBy varchar(32) = NULL, 
	  @DeletedDate datetime = NULL, 
	  @DeleteddBy varchar(32) = NULL, 
	  @InternalId numeric (18, 0) out 
  AS 
  BEGIN
	IF NOt EXISTS 
	   ( SELECT * FROM dbo.interfaceloadhistory
    	 WHERE IncomingOutGoing  = @IncomingOutGoing and 
		       FilePath          = @FilePath and 
			   Filename          = @Filename
	    )	
		BEGIN

			INSERT INTO dbo.interfaceloadhistory
			(
				FileId, 
				IncomingOutGoing, 
				FilePath, 
				Filename, 
				InterfaceIdentifier, 
				ClientId, 
				InterfaceName, 
				InterfaceType, 
				LoadProcessId, 
				LoadProcessName, 
				LoadDate, 
				ProcessedDate, 
				BusinessDate, 
				FileReceivedDate, 
				RecordsNumber, 
				RecordProcessed, 
				RecordRejected, 
				FileSize, 
				RejectReasonCode, 
				RejectReasonDescription, 
				LoadStatus, 
				DataFileContent, 
				CreationDate, 
				CreatedBy, 
				UpdateDate, 
				UpdatedBy, 
				DeletedDate, 
				DeleteddBy
			  )
			output inserted.*
			SELECT
				  @FileId, 
				  @IncomingOutGoing, 
				  @FilePath, 
				  @Filename, 
				  @InterfaceIdentifier, 
				  @ClientId, 
				  @InterfaceName, 
				  @InterfaceType, 
				  @LoadProcessId, 
				  @LoadProcessName, 
				  @LoadDate, 
				  @ProcessedDate, 
				  @BusinessDate, 
				  @FileReceivedDate, 
				  @RecordsNumber, 
				  @RecordProcessed, 
				  @RecordRejected, 
				  @FileSize, 
				  @RejectReasonCode, 
				  @RejectReasonDescription, 
				  @LoadStatus, 
				  @DataFileContent, 
				  @CreationDate, 
				  @CreatedBy, 
				  @UpdateDate, 
				  @UpdatedBy, 
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

