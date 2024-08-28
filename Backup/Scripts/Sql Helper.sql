https://codebeautify.org/sqlformatter

--- https://gallery.technet.microsoft.com/scriptcenter/Generate-Stored-Procedure-17a9007d

USE FundWireInterfaceDB;
SELECT 
  ROUTINE_SCHEMA,
  ROUTINE_NAME
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE';


IF OBJECT_ID('sp_AccountInformation','P') IS NOT NULL
DROP PROC sp_AccountInformation
GO
CREATE PROCEDURE sp_AccountInformation
@MessageReference      varchar(32) = NULL,
@MessageOrigin         varchar(10) = NULL,
@OrigineName           varchar(64) = NULL,
@IncomingOutGoing      varchar(16) = NULL,
@MessageId             varchar(32) = NULL,
@TagId                 varchar(64) = NULL,
@SequenceNumber        int = NULL,
@AccountIdCode         varchar(1) = NULL,
@AccountIdentifier     varchar(34) = NULL,
@AccountName           varchar(35) = NULL,
@AddressLine1          varchar(35) = NULL,
@AddressLine2          varchar(35) = NULL,
@AddressLine3          varchar(35) = NULL,
@DMLFlag	 VARCHAR(1)
AS

IF @DMLFlag = 'I'
BEGIN
 
INSERT INTO dbo.AccountInformation
(MessageReference,MessageOrigin,OrigineName,IncomingOutGoing,MessageId,TagId,SequenceNumber,AccountIdCode,AccountIdentifier,AccountName,AddressLine1,AddressLine2,AddressLine3)
SELECT @MessageReference,@MessageOrigin,@OrigineName,@IncomingOutGoing,@MessageId,@TagId,@SequenceNumber,@AccountIdCode,@AccountIdentifier,@AccountName,@AddressLine1,@AddressLine2,@AddressLine3
 
END
 
IF @DMLFlag = 'U'
BEGIN
 
UPDATE dbo.AccountInformation
   SET MessageReference      = @MessageReference,
       MessageOrigin         = @MessageOrigin,
       OrigineName           = @OrigineName,
       IncomingOutGoing      = @IncomingOutGoing,
       MessageId             = @MessageId,
       TagId                 = @TagId,
       SequenceNumber        = @SequenceNumber,
       AccountIdCode         = @AccountIdCode,
       AccountIdentifier     = @AccountIdentifier,
       AccountName           = @AccountName,
       AddressLine1          = @AddressLine1,
       AddressLine2          = @AddressLine2,
       AddressLine3          = @AddressLine3
 WHERE InternalId = @InternalId
 
 
END
 
IF @DMLFlag = 'D'
BEGIN
 
DELETE FROM dbo.AccountInformation
 WHERE InternalId = @InternalId
 
 
END
GO




USE [FundWireInterfaceDB]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[GenerateSPforInsert]
		@Schemaname = dbo,
		@Tablename = FedMessageByTag

---SELECT	'Return Value' = @return_value

GO

USE [FundWireInterfaceDB]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[GenerateSPforInsert]
		@Schemaname = dbo,
		@Tablename = FedMessageByTag

SELECT	'Return Value' = @return_value

GO