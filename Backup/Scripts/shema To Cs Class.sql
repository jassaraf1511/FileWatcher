CREATE PROCEDURE usp_TableToClass
/*
Created by Cade Bryant.
Generates C# class code for a table
and fields/properties for each column.

Run as "Results to Text" or "Results to File" (not Grid)

Example: EXEC usp_TableToClass 'MyTable'
*/

@table_name SYSNAME

AS

SET NOCOUNT ON

DECLARE @temp TABLE
(
sort INT,
code TEXT
)

INSERT INTO @temp
SELECT 1, 'public class ' + @table_name + CHAR(13) + CHAR(10) + '{'

INSERT INTO @temp
SELECT 2, CHAR(13) + CHAR(10) + '#region Constructors' + CHAR(13) + CHAR(10)
INSERT INTO @temp
SELECT 3, CHAR(9) + 'public ' + @table_name + '()'
+ CHAR(13) + CHAR(10) + CHAR(9) + '{'
+ CHAR(13) + CHAR(10) + CHAR(9) + '}'

INSERT INTO @temp
SELECT 4, '#endregion' + CHAR(13) + CHAR(10)

INSERT INTO @temp
SELECT 5, '#region Private Fields' + CHAR(13) + CHAR(10)
INSERT INTO @temp
SELECT 6, CHAR(9) + 'private ' +
CASE
WHEN DATA_TYPE LIKE '%CHAR%' THEN 'string '
WHEN DATA_TYPE LIKE '%INT%' THEN 'int '
WHEN DATA_TYPE LIKE '%DATETIME%' THEN 'DateTime '
WHEN DATA_TYPE LIKE '%BINARY%' THEN 'byte[] '
WHEN DATA_TYPE = 'BIT' THEN 'bool '
WHEN DATA_TYPE LIKE '%TEXT%' THEN 'string '
ELSE 'object '
END + '_' + COLUMN_NAME + ';' + CHAR(9)
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @table_name
ORDER BY ORDINAL_POSITION

INSERT INTO @temp
SELECT 7, '#endregion' +
CHAR(13) + CHAR(10)

INSERT INTO @temp
SELECT 8, '#region Public Properties' + CHAR(13) + CHAR(10)
INSERT INTO @temp
SELECT 9, CHAR(9) + 'public ' +
CASE
WHEN DATA_TYPE LIKE '%CHAR%' THEN 'string '
WHEN DATA_TYPE LIKE '%INT%' THEN 'int '
WHEN DATA_TYPE LIKE '%DATETIME%' THEN 'DateTime '
WHEN DATA_TYPE LIKE '%BINARY%' THEN 'byte[] '
WHEN DATA_TYPE = 'BIT' THEN 'bool '
WHEN DATA_TYPE LIKE '%TEXT%' THEN 'string '
ELSE 'object '
END + COLUMN_NAME +
CHAR(13) + CHAR(10) + CHAR(9) + '{' +
CHAR(13) + CHAR(10) + CHAR(9) + CHAR(9) +
'get { return _' + COLUMN_NAME + '; }' +
CHAR(13) + CHAR(10) + CHAR(9) + CHAR(9) +
'set { _' + COLUMN_NAME + ' = value; }' +
CHAR(13) + CHAR(10) + CHAR(9) + '}'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @table_name
ORDER BY ORDINAL_POSITION

INSERT INTO @temp
SELECT 10, '#endregion' +
CHAR(13) + CHAR(10) + '}'

SELECT code FROM @temp
ORDER BY sort




After running this procedure, execute SP with parameters. 
Now, the result will be loaded as model class.

exec CREATEMODEL 'EmployeeMaster', 'public class '  
Output

public class EmployeeMaster  
{  
public long? RowId { get; set; }  
public string EmpFirstName { get; set; }  
public string EmpLastName { get; set; }  
public long? PhoneNo { get; set; }  
public long? City { get; set; }  
public string Address { get; set; }  
public DateTime? DateOfBirth { get; set; }  
public int? Gender { get; set; }  
public bool? MaritalStatus { get; set; }  
public bool? EmpStatus { get; set; }  
}  


SELECT * 
    FROM 
INFORMATION_SCHEMA.TABLES   
    WHERE TABLE_SCHEMA = N'dbo'
	
	
	
SELECT (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo') as x

loop through x(
    print TABLE_NAME
    )
end

begin
    while(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo') as x

    begin
      print TABLE_NAME
    end loop
end



DECLARE @TABLE_NAME varchar(100)
declare @ret varchAR(max)
DECLARE CurName CURSOR FAST_FORWARD READ_ONLY
 FOR
    SELECT TABLE_NAME 
	FROM INFORMATION_SCHEMA.TABLES 
	WHERE TABLE_SCHEMA = N'dbo'

 OPEN CurName

 FETCH NEXT FROM CurName INTO @TABLE_NAME

 WHILE @@FETCH_STATUS = 0
    BEGIN

        print @TABLE_NAME
        @ret=exec CREATEMODEL @TABLE_NAME, 'public class ' 
		print @ret
        FETCH NEXT FROM CurName INTO @TABLE_NAME

    END

 CLOSE CurName
 DEALLOCATE CurName
 
 
 declare @ret varchAR(max)
 exec sp_executesql N'exec @ret =procc', 
N'@ret varchar(MAX) OUTPUT',@ret = @ret OUTPUT select @ret  as result

USE [FundWireInterfaceDB]
GO

DECLARE	@return_value int,
		@Result varchar(max)

EXEC	@return_value = [dbo].[Create_Class_Model]
		@TableName = Addenda,
		@CLASSNAME = N'public class',
		@Result = @Result OUTPUT

SELECT	@Result as N'@Result'

SELECT	'Return Value' = @return_value

GO

USE [FundWireInterfaceDB]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[Create_Class_Model]
		@TableName = AccountInformation,
		@CLASSNAME = N'public class '

SELECT	'Return Value' = @return_value

GO


USE [FundWireInterfaceDB]
GO

DECLARE	@return_value int,
		@DETAIL varchar(max)

EXEC	@return_value = [dbo].[Create_Class_Model]
		@TableName = N'Addenda',
		@CLASSNAME = N'public class ',
		@DETAIL = @DETAIL OUTPUT

SELECT	@DETAIL as N'@DETAIL'

SELECT	'Return Value' = @return_value

GO


--------------------------------
--------------------------------
USE [FundWireInterfaceDB]
GO

/****** Object:  StoredProcedure [dbo].[Create_Class_Model]    Script Date: 10/29/2020 12:34:02 PM ******/
DROP PROCEDURE [dbo].[Create_Class_Model]
GO

/****** Object:  StoredProcedure [dbo].[Create_Class_Model]    Script Date: 10/29/2020 12:34:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Create_Class_Model]  
  
      @TableName VARCHAR(500) ,  
      @CLASSNAME VARCHAR(500),
      @DETAIL VARCHAR(MAX) out
	 	  
  
AS  
BEGIN  
    DECLARE @Result VARCHAR(MAX)  
  
    SET @Result = @CLASSNAME + @TableName + '  
{'  
  
SELECT @Result = @Result + '  
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }'  
FROM  
(  
    SELECT   
        REPLACE(col.NAME, ' ', '_') ColumnName,  
        column_id ColumnId,  
        CASE typ.NAME   
            WHEN 'bigint' THEN 'long'  
            WHEN 'binary' THEN 'byte[]'  
            WHEN 'bit' THEN 'bool'  
            WHEN 'char' THEN 'string'  
            WHEN 'date' THEN 'DateTime'  
            WHEN 'datetime' THEN 'DateTime'  
            WHEN 'datetime2' then 'DateTime'  
            WHEN 'datetimeoffset' THEN 'DateTimeOffset'  
            WHEN 'decimal' THEN 'decimal'  
            WHEN 'float' THEN 'float'  
            WHEN 'image' THEN 'byte[]'  
            WHEN 'int' THEN 'int'  
            WHEN 'money' THEN 'decimal'  
            WHEN 'nchar' THEN 'char'  
            WHEN 'ntext' THEN 'string'  
            WHEN 'numeric' THEN 'decimal'  
            WHEN 'nvarchar' THEN 'string'  
            WHEN 'real' THEN 'double'  
            WHEN 'smalldatetime' THEN 'DateTime'  
            WHEN 'smallint' THEN 'short'  
            WHEN 'smallmoney' THEN 'decimal'  
            WHEN 'text' THEN 'string'  
            WHEN 'time' THEN 'TimeSpan'  
            WHEN 'timestamp' THEN 'DateTime'  
            WHEN 'tinyint' THEN 'byte'  
            WHEN 'uniqueidentifier' THEN 'Guid'  
            WHEN 'varbinary' THEN 'byte[]'  
            WHEN 'varchar' THEN 'string'  
            ELSE 'UNKNOWN_' + typ.NAME  
        END ColumnType,  
        CASE   
            WHEN col.is_nullable = 1 and typ.NAME in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')   
            THEN '?'   
            ELSE ''   
        END NullableSign  
    FROM SYS.COLUMNS col join sys.types typ on col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id  
    where object_id = object_id(@TableName)  
) t   
ORDER BY ColumnId  
SET @Result = @Result  + '  
}'  
set @DETAIL=  @Result
select @DETAIL
--print @Result  

END  
GO



public class Addenda    {        public decimal InternalId { get; set; }        public string MessageOrigin { get; set; }        public string OrigineName { get; set; }        public string IncomingOutGoing { get; set; }        public string MessageId { get; set; }        public int? AddendaLength { get; set; }        public string AddendaInformation { get; set; }    }