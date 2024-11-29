USE [CorporateActions]
GO
/****** Object:  StoredProcedure [dbo].[TEST_sp_insert_Swift_Message]    Script Date: 11/28/2024 8:46:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[TEST_sp_insert_Swift_Message]
(
	@Message_ID as varchar(32),
	@MessageType as varchar(3) = Null,
	@SenderBIC as varchar(9) = Null,
	@Depositary_ID as [varchar](16) = Null,
	@CorporateActionReference as varchar(16) = Null,
	@Messagefunction as varchar(4) = Null,
	@ProcStatusCode as varchar(4) = Null,
	@Mandatoryvoluntaryindicator as varchar(4) = Null,
	@Eventtype as varchar(4) = Null,
	@SwiftMessage as varchar(max) = Null,
	@SwiftMessageFileName as varchar(128) = Null,
	@Status as varchar(10) = Null,
	@ErrorType as varchar(16) = Null,
	@ErrorReason as varchar(256) = Null,
	@ReceiptDate as datetime = Null,
	@CreationDate as datetime = Null,
	@UpdatedDate datetime = Null,
	@CancelationDate datetime = Null,
	@CreatedBy as varchar(35) = Null,
	@UpdatedBy as varchar(35) = Null,
	@CanceledBy as varchar(35) = Null
	)
AS
    declare @ExitStatus  int; 
	declare @ExitCode varchar(10);
    declare	@ExitMessage varchar(500);
	declare @ErrorMessage NVARCHAR(4000);
	declare @ErrorSeverity INT;
	declare @ErrorState INT;
	
begin
		


		EXEC [dbo].[sp_Check_CorporateAction_Existence]
		@Message_ID = @Message_ID,
		@MessageType = @MessageType,
		@SenderBIC = @SenderBIC,
		@ExitStatus = @ExitStatus OUTPUT,
		@ExitCode = @ExitCode OUTPUT,
		@ExitMessage = @ExitMessage OUTPUT

		SELECT	@ExitStatus as N'@ExitStatus',
				@ExitCode as N'@ExitCode',
				@ExitMessage as N'@ExitMessage';


		print @SenderBIC;
		if(@ExitCode <>'OK')
        begin
		  
           set @ExitStatus = 10;
		   goto Sp_Exit;

        End
    
		
		begin transaction

			begin try

				-- insert
					insert [dbo].[Swift_Message] (
						Message_ID,
						MessageType,
						SenderBIC,
						Depositary_ID,
						CorporateActionReference,
						Messagefunction,
						ProcStatusCode,
						Mandatoryvoluntaryindicator,
						Eventtype,
						SwiftMessage,
						SwiftMessageFileName,
						Status,
						ErrorType,
						ErrorReason,
						ReceiptDate,
						CreationDate,
						UpdatedDate,
						CancelationDate,
						CreatedBy,
						UpdatedBy,
						CanceledBy)
					values (
						@Message_ID,
						@MessageType,
						@SenderBIC,
						@Depositary_ID,
						@CorporateActionReference,
						@Messagefunction,
						@ProcStatusCode,
						@Mandatoryvoluntaryindicator,
						@Eventtype,
						@SwiftMessage,
						@SwiftMessageFileName,
						@Status,
						@ErrorType,
						@ErrorReason,
						@ReceiptDate ,
						@CreationDate,
						@UpdatedDate ,
						@CancelationDate ,
						@CreatedBy,
						@UpdatedBy,
						@CanceledBy)


				-- Return the new ID
				Set @ExitStatus=0;
				set @ExitCode='OK';
				Set @ExitMessage='Swift Message Inserted';
			     -----SCOPE_IDENTITY() ;

				commit transaction

			end try 

			begin catch

				Set @ExitStatus=20;
				set @ExitCode='NOK';			
				Set @ExitMessage='Swift Message Not Inserted';

				select  @ExitStatus , @ExitCode, @ExitMessage,  @ErrorMessage ; -----= 

				select @ErrorMessage=ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
				raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);

				rollback transaction

			end catch;
Sp_Exit:
	
	---select @ExitStatus, @ExitCode, @ExitMessage;
End  
