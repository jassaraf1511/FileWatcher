USE [FundWireInterfaceDB]
GO

/****** Object:  StoredProcedure [dbo].[Sp_Create_Class_Model]    Script Date: 10/29/2020 12:34:02 PM ******/
DROP PROCEDURE [dbo].[Sp_Create_Class_Model]
GO

/****** Object:  StoredProcedure [dbo].[Sp_Create_Class_Model]    Script Date: 10/29/2020 12:34:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_Create_Class_Model]  
  
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


