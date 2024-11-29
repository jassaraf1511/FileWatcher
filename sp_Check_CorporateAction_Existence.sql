USE [CorporateActions]
GO
/****** Object:  StoredProcedure [dbo].[sp_Check_CorporateAction_Existence]    Script Date: 11/28/2024 8:45:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Check_CorporateAction_Existence] 
(
	@Message_ID as varchar(32) null,
	@MessageType as varchar(3) = Null,
	@SenderBIC as varchar(9) = Null,
	@CorporateActionReference as varchar(16) = Null,
	@Mandatoryvoluntaryindicator as varchar(4) = Null,
	@Eventtype as varchar(4) = Null,
	@ExitStatus as int output,
	@ExitCode  as varchar(10) output,
    @ExitMessage  as varchar(500) output

)  
 As  
   
Begin  
	
	Set @ExitStatus=0;
	Set @ExitMessage='';
	set @ExitCode='';
	
	IF (EXISTS (
                SELECT 1
                FROM [dbo].[Swift_Message]
				Where Message_ID =@Message_ID				
                )
        )    
		BEGIN
			Set @ExitStatus=10;
			set @ExitCode='NOK';
			Set @ExitMessage='Swift Message Already Exists';
			GOTO Sp_check_end
		END
	
    Set @ExitStatus=0;
	set @ExitCode='OK';
	Set @ExitMessage='Swift Message Inserted';
	
Sp_check_end:
	print @ExitStatus;
	----select @ExitStatus, @ExitCode, @ExitMessage;
End  

