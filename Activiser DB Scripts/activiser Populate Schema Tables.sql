SELECT     TABLE_CATALOG + '.' + TABLE_SCHEMA + '.' + TABLE_NAME AS FullTableName, TABLE_NAME AS ShortTableName, 
                      CASE TABLE_TYPE WHEN 'BASE TABLE' THEN 'Table' WHEN 'VIEW' THEN 'View' ELSE 'Unknown' END AS TableType
FROM         INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = 'dbo' and TABLE_TYPE = 'BASE TABLE'
ORDER BY FullTableName

SELECT     INFORMATION_SCHEMA.COLUMNS.TABLE_CATALOG + '.' + INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA + '.' + INFORMATION_SCHEMA.COLUMNS.TABLE_NAME
                       AS FullTableName, INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME AS ColumnName, 
                      INFORMATION_SCHEMA.COLUMNS.DATA_TYPE AS DataType, 
                      CASE INFORMATION_SCHEMA.COLUMNS.IS_NULLABLE WHEN 'Yes' THEN 1 ELSE 0 END AS IsNullable, 
                      INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AS ShortTableName
FROM         INFORMATION_SCHEMA.COLUMNS INNER JOIN
                      INFORMATION_SCHEMA.TABLES ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = INFORMATION_SCHEMA.TABLES.TABLE_NAME
WHERE INFORMATION_SCHEMA.TABLES.TABLE_SCHEMA = 'dbo' and TABLE_TYPE = 'BASE TABLE'

INSERT INTO [activiser].[metadata].[SchemaAttribute]
           ([AttributeId]
           ,[EntityId]
           ,[AttributeName]
           ,[AttributeType]
           ,[MaxLength]
           ,[Required])

SELECT     NEWID()
, metadata.SchemaEntity.EntityId
, INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME AS ColumnName
, CASE INFORMATION_SCHEMA.COLUMNS.DATA_TYPE 
	WHEN 'int' then 
		CASE
			WHEN PrimaryKeyAttributeName = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME THEN
				101
			ELSE
				1
		END
	WHEN 'float' then 4
	WHEN 'money' then 7
	WHEN 'datetime' then 3
	WHEN 'nchar' then 2
	WHEN 'char' then 2
	WHEN 'varchar' then 2
	WHEN 'nvarchar' then 2
	WHEN 'bit' then 5
	WHEN 'tinyint' then 6
	WHEN 'uniqueidentifier' then 
		CASE 
			WHEN PrimaryKeyAttributeName = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME THEN
				100
			ELSE
				0
		END
			
  END
AS [AttributeType]
, ISNULL(INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, 0)
, CASE INFORMATION_SCHEMA.COLUMNS.IS_NULLABLE WHEN 'Yes' THEN 0 ELSE 1 END AS [Required]
FROM         INFORMATION_SCHEMA.COLUMNS INNER JOIN
                      metadata.SchemaEntity ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = metadata.SchemaEntity.EntityName