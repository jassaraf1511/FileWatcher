DECLARE @TABLE_NAME varchar(100)
declare @return_value int,@DETAIL varchar(max)
DECLARE CurName CURSOR FAST_FORWARD READ_ONLY
 FOR
    SELECT TABLE_NAME 
	FROM INFORMATION_SCHEMA.TABLES 
	WHERE TABLE_SCHEMA = N'dbo'

 OPEN CurName

 FETCH NEXT FROM CurName INTO @TABLE_NAME

 WHILE @@FETCH_STATUS = 0
    BEGIN

        EXEC	@return_value = [dbo].[Sp_Create_Class_Model]
		@TableName = @TABLE_NAME,
		@CLASSNAME = N'public class ',
		@DETAIL = @DETAIL OUTPUT

print	@DETAIL 

SELECT	'Return Value' = @DETAIL
        FETCH NEXT FROM CurName INTO @TABLE_NAME

    END

 CLOSE CurName
 DEALLOCATE CurName
 
---for sp insert generation
DECLARE @TABLE_NAME varchar(100)
DECLARE	@return_value int,@DETAIL varchar(max)
DECLARE CurName CURSOR FAST_FORWARD READ_ONLY
 FOR
    SELECT TABLE_NAME 
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_SCHEMA = N'dbo'

 OPEN CurName

 FETCH NEXT FROM CurName INTO @TABLE_NAME

 WHILE @@FETCH_STATUS = 0
    BEGIN

        EXEC	@return_value = [dbo].[GenerateSPforInsert]
		@Schemaname = dbo,
		@Tablename = @TABLE_NAME
		---@DETAIL = @DETAIL OUTPUT

---print	@DETAIL 

SELECT	'Return Value' = @DETAIL
        FETCH NEXT FROM CurName INTO @TABLE_NAME

    END

 CLOSE CurName
 DEALLOCATE CurName


GO

