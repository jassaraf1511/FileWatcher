SELECT
    sys.tables.name,
    sys.indexes.name,
    sys.columns.name
FROM sys.indexes
    INNER JOIN sys.tables ON sys.tables.object_id = sys.indexes.object_id
    INNER JOIN sys.index_columns ON sys.index_columns.index_id = sys.indexes.index_id
        AND sys.index_columns.object_id = sys.tables.object_id
    INNER JOIN sys.columns ON sys.columns.column_id = sys.index_columns.column_id
        AND sys.columns.object_id = sys.tables.object_id
---WHERE sys.tables.name = 'TABLE NAME HERE'
ORDER BY
    sys.tables.name,
    sys.indexes.name,
    sys.columns.name