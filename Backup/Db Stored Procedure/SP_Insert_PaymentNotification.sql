IF OBJECT_ID('SP_Insert_PaymentNotification', 'P') IS NOT NULL 
BEGIN 
DROP PROC SP_Insert_PaymentNotification
END
GO
  CREATE PROCEDURE SP_Insert_PaymentNotification @MessageReference varchar(32) = NULL,
  @MessageOrigin varchar(10) = NULL,
  @OrigineName varchar(64) = NULL,
  @IncomingOutGoing varchar(16) = NULL,
  @TagId varchar(64) = NULL,
  @MessageId varchar(32) = NULL,
  @SequenceNumber int = NULL,
  @NotificationIndicator varchar(1) = NULL,
  @ContactEmail varchar(2048) = NULL,
  @ContactName varchar(140) = NULL,
  @ContactPhoneNumber varchar(35) = NULL,
  @ContactFaxNumber varchar(35) = NULL,
  @ContactMobileNumber varchar(35) = NULL,
  @EndToEndIdentification varchar(35) = NULL,
  @ID numeric(18, 0) out AS BEGIN
INSERT INTO
  dbo.PaymentNotification (
    MessageReference,
    MessageOrigin,
    OrigineName,
    IncomingOutGoing,
    TagId,
    MessageId,
    SequenceNumber,
    NotificationIndicator,
    ContactEmail,
    ContactName,
    ContactPhoneNumber,
    ContactFaxNumber,
    ContactMobileNumber,
    EndToEndIdentification
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
  @NotificationIndicator,
  @ContactEmail,
  @ContactName,
  @ContactPhoneNumber,
  @ContactFaxNumber,
  @ContactMobileNumber,
  @EndToEndIdentification;
set
  @ID = @@IDENTITY
END
GO