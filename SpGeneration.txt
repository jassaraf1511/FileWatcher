USE [CorporateActionsISOFiles]
GO

/****** Object:  StoredProcedure [dbo].[createInsertSP]    Script Date: 10/8/2024 3:38:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[createInsertSP]
@spSchema varchar(200),
@spTable varchar(200)

AS
BEGIN
	declare @SQL_DROP varchar(max)
	declare @SQL varchar(max)
	declare @COLUMNS varchar(max)
	declare @PK_COLUMN varchar(200)
 	declare @COLUMNS_N varchar(max)
	set @SQL = char(13) + char(13)
	set @SQL_DROP = ''
	set @COLUMNS = ''

	-- step 1: add a modifications section
	set @SQL = @SQL + '---------------------------------------------------------------------' + char(13)
	set @SQL = @SQL + '-- Author: Jacob Assaraf' + char(13)
	set @SQL = @SQL + '-- Created: ' + convert(varchar(11), getdate(), 106) + char(13)
	set @SQL = @SQL + '-- Function: Inserts a ' + @spSchema + '.' + @spTable + ' table record' + char(13)
	set @SQL = @SQL + '--' + char(13)
	set @SQL = @SQL + '-- Modifications:' + char(13)
	set @SQL = @SQL + '---------------------------------------------------------------------' + char(13)	
	
	-- step 2: generate the drop statement and then the create statement
	set @SQL_DROP = @SQL_DROP + char(13) + 'IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[' + @spSchema + '].[insert' + @spTable + ']'') AND type in (N''P'', N''PC''))' + char(13)
	set @SQL_DROP = @SQL_DROP + char(9)+'DROP PROCEDURE [' + @spSchema + '].[sp_insert_' + @spTable + ']' + char(13)
 	set @SQL = @SQL + char(13)+ @SQL_DROP
	set @SQL = @SQL + char(13)+ 'CREATE PROC [' + @spSchema + '].[sp_insert_' + @spTable + ']' + char(13) 
	set @SQL = @SQL + '(' + char(13)
 	 	
 	-- step 3: now put all the table columns in bar the primary key (as this is an insert and it is an identity column)
	select @COLUMNS = @COLUMNS + '@' + COLUMN_NAME 
			+ ' as ' 
			+ DATA_TYPE --- (case DATA_TYPE when 'numeric' then DATA_TYPE + '(' + convert(varchar(10), NUMERIC_PRECISION) + ',' + convert(varchar(10), NUMERIC_SCALE) + ')' else DATA_TYPE end)
			+ (case when CHARACTER_MAXIMUM_LENGTH is not null then '(' + case when CONVERT(varchar(10), CHARACTER_MAXIMUM_LENGTH) = '-1' then 'max' else CONVERT(varchar(10), CHARACTER_MAXIMUM_LENGTH) end + ')' else '' end)
			+ (case 
				when IS_NULLABLE = 'YES'
					then
						case when COLUMN_DEFAULT is null
							then ' = Null'
							else ''
						end
					else
						case when COLUMN_DEFAULT is null
							then ''
							else
								case when COLUMN_NAME = @PK_COLUMN
									then ''
									else ' = ' + replace(replace(COLUMN_DEFAULT, '(', ''), ')', '')
								end
						end
				end)
			+ ',' + char(13) 
	from INFORMATION_SCHEMA.COLUMNS
	where TABLE_SCHEMA = @spSchema 
		and TABLE_NAME = @spTable
	order by ORDINAL_POSITION
	
	---set @SQL = @SQL + left(@COLUMNS, len(@COLUMNS) - 2) + char(13)
	set @COLUMNS=replace(@COLUMNS, '@', +char(9)+'@')
	set @SQL = @SQL + SUBSTRING(@COLUMNS,1,len(@COLUMNS) - 2) + char(13)
 	
	--print len(@COLUMNS)
	--print @COLUMNS

 	set @SQL = @SQL +  char(13)
	set @SQL = @SQL + ')' + char(13)
	set @SQL = @SQL + 'AS' + char(13)
	set @SQL = @SQL + '' + char(13)
 	
	-- body here
 	
	-- step 5: begins a transaction
	set @SQL = @SQL + 'begin transaction' + char(13) + char(13)
 	
 	-- step 6: begin a try
	set @SQL = @SQL + char(9) + 'begin try' + char(13) + char(13) 
 	
	set @SQL = @SQL + char(9)+ char(9)+  '-- insert' + char(13)
 		
	-- step 7: code the insert
	set @COLUMNS = ''
 		
	select @COLUMNS = @COLUMNS + '@' + COLUMN_NAME + ','
	from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @spTable
	order by ORDINAL_POSITION

	--set @COLUMNS = left(@COLUMNS, len(@COLUMNS) -1) -- trim off the last comma
	 set @COLUMNS = SUBSTRING(@COLUMNS, 1,len(@COLUMNS) -1) -- trim off the last comma 
	 
 	 set @COLUMNS_N=replace(@COLUMNS, ',', ','+char(13))
	 set @COLUMNS_N=replace(@COLUMNS_N, '@', +char(9)+char(9)+char(9)+char(9)+'@')
	 set @COLUMNS=@COLUMNS_N

	set @SQL = @SQL + char(9)+ char(9)+ char(9)+ 'insert [' + @spSchema + '].[' + @spTable + '] (' + char(13) + replace(@COLUMNS, '@', '') + ')' + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ char(9)+ 'values (' +char(13) + @COLUMNS + ')' + char(13)
	
	set @SQL = @SQL + char(13) + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ '-- Return the new ID'  + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ 'select SCOPE_IDENTITY();' + char(13) + char(13)
 	
 	-- step 8: commit the transaction
	set @SQL = @SQL + char(9)+ char(9)+ 'commit transaction' + char(13) + char(13)
 	
 	-- step 9: end the try
	set @SQL = @SQL + char(9)+ 'end try' + char(13) + char(13)
 	
 	-- step 10: begin a catch
	set @SQL = @SQL + char(9)+ 'begin catch' + char(13) + char(13)  
 	
 	-- step 11: raise the error
	set @SQL = @SQL + char(9)+ char(9)+ 'declare @ErrorMessage NVARCHAR(4000);' + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ 'declare @ErrorSeverity INT;' + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ 'declare @ErrorState INT;' + char(13) + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ 'select @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();' + char(13) + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ 'raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);' + char(13) + char(13)
	set @SQL = @SQL + char(9)+ char(9)+ 'rollback transaction' + char(13) + char(13)
 	
 	-- step 11: end the catch
	set @SQL = @SQL + char(9)+ 'end catch;' + char(13) + char(13) 
 	print LEN(@sql)
	
 	-- step 12: return both the drop and create statements
	RETURN  @SQL_DROP + char(13) + char(13) + @SQL
 
END
GO


---- For EXceution Testing 
USE [CorporateActionsISOFiles]
GO

USE [CorporateActionsISOFiles]
GO
set nocount on;

DECLARE	@return_value varchar(max)

EXEC	@return_value = [dbo].[createInsertSP]
		@spSchema = N'dbo',
		@spTable = N'CA_Options_Rates'

SELECT	'ReturnValue' = len(@return_value)


GO
---

select * from INFORMATION_SCHEMA.COLUMNS
	where TABLE_SCHEMA = 'dbo'
		and TABLE_NAME = 'Swift_Qualifier_LDM'
	order by ORDINAL_POSITION
GO	
