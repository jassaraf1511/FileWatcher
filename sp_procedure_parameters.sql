CREATE PROCEDURE sp_get_storeprocedure_parameters
  @sp_name VARCHAR(100) = NULL,
  @InternalId numeric(3, 0) out
AS
  IF @sp_name IS NULL
  BEGIN
    RETURN (1)
  END
  IF NOT EXISTS
  (
         SELECT *
         FROM   sys.procedures
         WHERE  NAME = @sp_name )
  BEGIN
    RETURN(2)
  END
  SELECT     'ParameterName' = a.NAME,
             'ParameterType' = type_name(a.user_type_id),
             'ParameterLength' = a.max_length,
             'ParameterPrec' =
             CASE
                        WHEN type_name(a.system_type_id) = 'uniqueidentifier' THEN PRECISION
                        ELSE odbcprec( a.system_type_id, a.max_length, a.PRECISION )
             END,
             'ParameterScale' = odbcscale(a.system_type_id, a.scale),
             'ParameterMode' =     b.parameter_mode,
             'ParameterPosition' = b.ordinal_position,
             'ParameterIsResult' = b.is_result,
             'ParameterCollation' = CONVERT( sysname,
             CASE
                        WHEN a.system_type_id IN (35,
                                                  99,
                                                  167,
                                                  175,
                                                  231,
                                                  239) THEN serverproperty('collation')
             END )
  FROM       sys.parameters a
  INNER JOIN information_schema.parameters b
  ON         specific_name = object_name(object_id)
  AND        b.parameter_name = a.NAME
  WHERE      object_id = object_id(@sp_name)
  IF @@ERROR <> 0
  BEGIN
    RETURN (3)
  END
  ELSE
  BEGIN
    RETURN (0)
  END
  go
  USE [CorporateActions]
GO

DECLARE	@return_value int,
		@InternalId numeric(3, 0)

EXEC	@return_value = [dbo].[sp_get_storeprocedure_parameters]
		@sp_name = N'sp_insert_Swift_Message',
		@InternalId = @InternalId OUTPUT

SELECT	@InternalId as N'@InternalId'

SELECT	'Return Value' = @return_value

GO