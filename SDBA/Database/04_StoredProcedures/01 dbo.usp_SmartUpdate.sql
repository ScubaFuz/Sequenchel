ALTER PROCEDURE [dbo].[usp_SmartUpdate] 
--declare
	 @SourceSchemaName [sysname]
	,@SourceTableName [sysname]
	,@TargetSchemaName [sysname]
	,@TargetTableName [sysname]
	,@CreateTargetTable [bit] = 0
	,@UseAuditTable [bit] = 0
	,@CreateAuditTable [bit] = 0
	,@RemoveNonSourceData [bit] = 0
	,@UseTargetCollation [bit] = 0
	,@ClearTargetTable [bit] = 0
	,@UseAllColumns [bit] = 0
	,@EqualizeTextData bit = 0
	,@SourceFilter nvarchar(255) = NULL
	,@UseDistinct [bit] = 0
	,@DebugMode [bit] = 0
WITH ENCRYPTION
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Update a target table with all new / changed / deleted data from a Source table
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2016-02-19	BT		Initial version
-- 1.1		2016-10-29	BT		Added the option to remove all target data before inserting new data.
-- 1.2		2016-11-22	BT		If ClearTargetTable = 1 no primary key is required.
-- 2.0		2017-03-05	BT		Rewrite to handle changing or slightly different data types
-- 3.0		2020-08-25	BT		Added a source filter option to limit the scope
-- 3.1		2020-09-19	BT		Added a check if (all) primary key column exist in both Source and Target table
-- 3.2		2020-09-28	BT		Added DISTINCT option for main copy operation
-- 3.3		2020-10-06	BT		When creating the target tables, no auditing is required or possible => ClearTargetTable is activated
-- ****************************************************************************

/**********DEBUG******************************
	SET @SourceSchemaName = 'dbo'
	SET @SourceTableName = 'tbl_Servers'
	SET @TargetSchemaName = 'dbo'
	SET @TargetTableName = 'DWH_tbl_Servers'
	SET @CreateTargetTable = 0
	SET @UseAuditTable = 0
	SET @CreateAuditTable = 0
	SET @RemoveNonSourceData = 0
	SET @UseTargetCollation = 1
	SET @ClearTargetTable = 1
	SET @UseAllColumns = 0
	SET @EqualizeTextData = 0
	SET @DebugMode = 1
--**********DEBUG******************************/

SET NOCOUNT ON

Print 'Start ' + Convert(nvarchar(19),GetDate(),120) + N' ' + @SourceSchemaName + '.' + @SourceTableName + ', ' +  @TargetSchemaName + '.' + @TargetTableName

DECLARE @AuditTableName [sysname], @RunTime smalldatetime, @RunTimeChar nvarchar(100)
DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10)
DECLARE @CreateCommand nvarchar(max), @CopyCommand nvarchar(max)
DECLARE @FromClause nvarchar(max), @JoinClause nvarchar(max), @NextJoin nvarchar(1000), @WhereClause nvarchar(max), @SetClause nvarchar(max)
DECLARE @ColName sysname, @PkColName sysname, @DataTypeIn nvarchar(100), @DataTypeOut nvarchar(100)
DECLARE @SourceColumnList nvarchar(max), @TargetColumnList nvarchar(max), @MatchColumnList nvarchar(max), @CopyColumnList nvarchar(max), @PKColumnList nvarchar(max)
DECLARE @RowCounter int

SET @SourceSchemaName = replace(replace(@SourceSchemaName,'[',''),']','')
SET @SourceTableName = replace(replace(@SourceTableName,'[',''),']','')
SET @TargetSchemaName = replace(replace(@TargetSchemaName,'[',''),']','')
SET @TargetTableName = replace(replace(@TargetTableName,'[',''),']','')

SET @AuditTableName = @TargetTableName + '_Audit'
SET @RunTime  =GETDATE()
set @RunTimeChar = replace(replace(replace(replace(convert(char,@RunTime,120),'-',''),' ',''),':',''),'.','')
    
IF object_id('tempdb..#ColumnsIn') IS NOT NULL DROP TABLE #ColumnsIn
IF object_id('tempdb..#ColumnsOut') IS NOT NULL DROP TABLE #ColumnsOut
IF object_id('tempdb..#ColumnsAudit') IS NOT NULL DROP TABLE #ColumnsAudit
IF object_id('tempdb..#CopyColumns') IS NOT NULL DROP TABLE #CopyColumns
IF object_id('tempdb..#MatchColumns') IS NOT NULL DROP TABLE #MatchColumns
IF object_id('tempdb..##TempData') IS NOT NULL DROP TABLE ##TempData
IF object_id('tempdb..#TempColumns') IS NOT NULL DROP TABLE #TempColumns
IF object_id('tempdb..#PKColumns') IS NOT NULL DROP TABLE #PKColumns
IF object_id('tempdb..#CompareColumns') IS NOT NULL DROP TABLE #CompareColumns

--Find columns for Source table
SELECT col.name As colName
	,typ.name as DataType
	,col.max_length
	,col.[precision]
	,col.scale
	,col.is_nullable
	,col.is_computed
	,col.[object_id]
	,col.is_identity
	,idt.seed_value
	,idt.increment_value
	,case when pky.PrimaryKeyColumn is null then 0 else 1 end as pk
INTO #ColumnsIn
FROM sys.columns col
INNER JOIN sys.types typ
	ON col.system_type_id = typ.system_type_id
	AND col.user_type_id = typ.user_type_id
INNER JOIN sys.objects obj
	ON col.[object_id] = obj.[object_id]
INNER JOIN sys.schemas scm
	ON obj.[schema_id] = scm.[schema_id]
LEFT OUTER JOIN sys.identity_columns idt
	ON col.[object_id] = idt.[object_id]
	AND col.name = idt.name
LEFT OUTER JOIN (SELECT KU.TABLE_SCHEMA as SchemaName
					, KU.TABLE_NAME as TableName
					,KU.COLUMN_NAME as PrimaryKeyColumn
				FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
				INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
					ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' 
					AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
				WHERE KU.TABLE_SCHEMA = @SourceSchemaName
					AND KU.TABLE_NAME = @SourceTableName) pky
	ON col.name = pky.PrimaryKeyColumn
WHERE obj.name = @SourceTableName
	AND scm.name = @SourceSchemaName

--If source table has no columns exit
IF (SELECT COUNT(*) FROM #ColumnsIn) = 0 
BEGIN
	print 'Source table does not exist or has no columns. Abort action'
	RETURN
END

--find target table columns
SELECT col.name As colName
	,typ.name as DataType
	,col.max_length
	,col.[precision]
	,col.scale
	,col.is_nullable
	,col.is_computed
	,col.[object_id]
	,col.is_identity
	,idt.seed_value
	,idt.increment_value
	,case when pky.PrimaryKeyColumn is null then 0 else 1 end as pk
INTO #ColumnsOut
FROM sys.columns col
INNER JOIN sys.types typ
	ON col.system_type_id = typ.system_type_id
	AND col.user_type_id = typ.user_type_id
INNER JOIN sys.objects obj
	ON col.[object_id] = obj.[object_id]
INNER JOIN sys.schemas scm
	ON obj.[schema_id] = scm.[schema_id]
LEFT OUTER JOIN sys.identity_columns idt
	ON col.[object_id] = idt.[object_id]
	AND col.name = idt.name
LEFT OUTER JOIN (SELECT KU.TABLE_SCHEMA as SchemaName
					, KU.TABLE_NAME as TableName
					,KU.COLUMN_NAME as PrimaryKeyColumn
				FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
				INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
					ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' 
					AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
				WHERE KU.TABLE_SCHEMA = @TargetSchemaName
					AND KU.TABLE_NAME = @TargetTableName) pky
	ON col.name = pky.PrimaryKeyColumn
WHERE obj.name = @TargetTableName
	AND scm.name = @TargetSchemaName

IF @ClearTargetTable = 0
	BEGIN

		--Determine the Primary Key column(s). 
		--If a primary key is defined in SmartUpdate for both Source and Target, the data must fullfill them both.
		IF @DebugMode = 1 print 'Look for Primary key definition in config.'
		SELECT Distinct ColumnName, DataType
		INTO #PKColumns
		FROM dbo.SmartUpdate
		WHERE ((SchemaName = @SourceSchemaName AND TableName = @SourceTableName)
			OR (SchemaName = @TargetSchemaName AND TableName = @TargetTableName))
			AND PrimaryKey = 1
			AND @RunTime BETWEEN DateStart AND COALESCE(DateStop,@RunTime + 1)
			AND Active = 1
	
		IF (SELECT count(*) FROM #PKColumns) = 0 
			BEGIN
				IF @DebugMode = 1 print 'No Primary key definition available, check source table.'
				--No primary key was defined for this Source or target table in SmartUpdate. use the original Primary Key
				INSERT INTO #PKColumns SELECT [ColName], [DataType] FROM #ColumnsIn WHERE pk = 1
				IF (SELECT count(*) FROM #PKColumns) = 0 
					BEGIN
						IF @DebugMode = 1 print 'No Primary key available in source, check source table identity column.'
						--No primary key was defined for this Source table. use the Identity column
						INSERT INTO #PKColumns SELECT [ColName], [DataType] FROM #ColumnsIn WHERE [is_identity] = 1
						IF (SELECT count(*) FROM #PKColumns) = 0 
							BEGIN
								IF @DebugMode = 1 print 'No identity column available in source, check primary key in target table.'
								--No Identity column was defined for this Source table. use the Primary Key from Target
								INSERT INTO #PKColumns SELECT [ColName], [DataType] FROM #ColumnsOut WHERE [pk] = 1
								IF (SELECT count(*) FROM #PKColumns) = 0 AND @ClearTargetTable = 0
									BEGIN
										print 'No primary key was defined for the Source or Target table. Abort procedure. Use /ClearTargetTable or define PK.'
										RETURN
									END
							END
					END
			END
		--Remove primary key columns that do not exist in both source and target column list
		DELETE FROM #PKColumns
		WHERE ColumnName NOT IN (
								SELECT cin.[ColName] 
								FROM #ColumnsIn cin
								INNER JOIN #ColumnsOut cou
									ON cin.[ColName] = cou.[ColName]
								)

		IF @DebugMode = 1 
		BEGIN
			DECLARE @PrintText nvarchar(1000) = (SELECT STUFF((SELECT ',' + [ColumnName] FROM #PKColumns FOR XML PATH('')) ,1,1,'') AS Txt)
			print 'Primary key columns are: ' + @PrintText
		END
	END

--If target table has no columns create table or exit
IF (SELECT COUNT(*) FROM #ColumnsOut) = 0 
	BEGIN
		IF @CreateTargetTable = 1
			BEGIN
				print 'Create Target Table'
				SET @ClearTargetTable = 1
				DECLARE AllColumn_cursor CURSOR SCROLL FOR
					SELECT #ColumnsIn.[colName], #ColumnsIn.DataType COLLATE DATABASE_DEFAULT
					FROM #ColumnsIn

				SET @RowCounter = 1
				OPEN AllColumn_cursor
				FETCH FIRST FROM AllColumn_cursor into @ColName, @DataTypeIn
				WHILE @@FETCH_STATUS = 0
				BEGIN
					IF @RowCounter > 1 SET @SourceColumnList = COALESCE(@SourceColumnList,'') + ','
					SET @SourceColumnList = COALESCE(@SourceColumnList,'') + '[tbl1].[' + @ColName + ']' --+ @NewLineChar
					IF @UseTargetCollation = 1 AND @DataTypeIn IN ('nchar','nvarchar','char','varchar') SET @SourceColumnList = @SourceColumnList + ' COLLATE Database_Default AS [' + @ColName + ']'
					SET @RowCounter = @RowCounter + 1
					FETCH NEXT FROM AllColumn_cursor into @ColName, @DataTypeIn
				END
				CLOSE AllColumn_cursor
				DEALLOCATE AllColumn_cursor

				SET @CreateCommand = 'SELECT TOP 0 ' + @SourceColumnList + @NewLineChar
				IF NOT (@UseAuditTable = 0 AND @CreateAuditTable = 0) SET @CreateCommand = @CreateCommand + ', @RunTime AS SU_LogDate' + @NewLineChar
				SET @CreateCommand = @CreateCommand + 'INTO ' + @TargetSchemaName + '.' + @TargetTableName + @NewLineChar
				SET @CreateCommand = @CreateCommand + 'FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' AS [tbl1]' + @NewLineChar
				SET @CreateCommand = @CreateCommand + 'LEFT OUTER JOIN ' + @SourceSchemaName + '.' + @SourceTableName + ' AS [tbl2] ON 1=1' + @NewLineChar
				IF @DebugMode = 1 print '@CreateCommand = ' + @CreateCommand
				EXECUTE sp_ExecuteSql @CreateCommand, N'@RunTime smalldatetime', @RunTime = @RunTime
				
				INSERT INTO #ColumnsOut
				SELECT col.name As colName
					,typ.name as DataType
					,col.max_length
					,col.[precision]
					,col.scale
					,col.is_nullable
					,col.is_computed
					,col.[object_id]
					,col.is_identity
					,idt.seed_value
					,idt.increment_value
					,case when pky.PrimaryKeyColumn is null then 0 else 1 end as pk
				FROM sys.columns col
				INNER JOIN sys.types typ
					ON col.system_type_id = typ.system_type_id
					AND col.user_type_id = typ.user_type_id
				INNER JOIN sys.objects obj
					ON col.[object_id] = obj.[object_id]
				INNER JOIN sys.schemas scm
					ON obj.[schema_id] = scm.[schema_id]
				LEFT OUTER JOIN sys.identity_columns idt
					ON col.[object_id] = idt.[object_id]
					AND col.name = idt.name
				LEFT OUTER JOIN (SELECT KU.TABLE_SCHEMA as SchemaName
									, KU.TABLE_NAME as TableName
									,KU.COLUMN_NAME as PrimaryKeyColumn
								FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
								INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
									ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' 
									AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
								WHERE KU.TABLE_SCHEMA = @TargetSchemaName
									AND KU.TABLE_NAME = @TargetTableName) pky
					ON col.name = pky.PrimaryKeyColumn
				WHERE obj.name = @TargetTableName
					AND scm.name = @TargetSchemaName
			END
		ELSE
			BEGIN
				print 'Target table does not exist. Abort action'
				RETURN
			END
	END

IF (SELECT COUNT(*) FROM #ColumnsOut) = 0 
	BEGIN
		print 'Target table does not exist and could not be created. Abort action'
		RETURN
	END

--Create a list of columns to copy from Source to target table
SELECT TOP 0 colName, DataType
INTO #CopyColumns
FROM #ColumnsIn

IF @UseAllColumns = 1
	BEGIN
		IF @DebugMode = 1 print 'Use all columns from source for copy data to target.'
		INSERT INTO #CopyColumns 
		SELECT colName, DataType
		FROM #ColumnsIn
	END

IF (SELECT COUNT(*) FROM #CopyColumns) = 0 
	BEGIN
		IF @DebugMode = 1 print 'Do not use all columns, check for copy columns defined for Source.'
		INSERT INTO #CopyColumns 
		SELECT ColumnName, DataType
		FROM dbo.SmartUpdate
		WHERE SchemaName = @SourceSchemaName
			AND TableName = @SourceTableName
			AND CopyColumn = 1
			AND @RunTime BETWEEN DateStart AND COALESCE(DateStop,@RunTime + 1)
			AND Active = 1
	END

IF (SELECT COUNT(*) FROM #CopyColumns) = 0 
	BEGIN
		IF @DebugMode = 1 print 'No copy columns defined for Source, check to see if there are columns for the target.'
		INSERT INTO #CopyColumns 
		SELECT ColumnName, DataType
		FROM dbo.SmartUpdate
		WHERE SchemaName = @TargetSchemaName
			AND TableName = @TargetTableName
			AND CopyColumn = 1
			AND @RunTime BETWEEN DateStart AND COALESCE(DateStop,@RunTime + 1)
			AND Active = 1
	END

IF (SELECT COUNT(*) FROM #CopyColumns) = 0 
	BEGIN
		IF @DebugMode = 1 print 'No columns defined, use all columns anyway.'
		INSERT INTO #CopyColumns
		SELECT colName, DataType
		FROM #ColumnsIn
	END
ELSE
	BEGIN
		IF @DebugMode = 1 print 'columns have been found in config and/or all columns must all be used. SET @UseAllColumns = 1'
		SET @UseAllColumns = 1
	END

--	BEGIN
--Create a list of matching columns from Source and target table to copy data.
SELECT cop.colName COLLATE DATABASE_DEFAULT AS colName
	,cin.DataType AS DataTypeIn
	,cin.max_length AS max_lengthIn
	,cin.[precision] AS [precisionIn]
	,cin.scale AS scaleIn
	,cout.DataType AS DataTypeOut
	,cout.max_length AS max_lengthOut
	,cout.[precision] AS [precisionOut]
	,cout.scale AS scaleOut
INTO #MatchColumns
FROM #CopyColumns cop
INNER JOIN #ColumnsIn cin
	ON cop.colName = cin.colName
INNER JOIN #ColumnsOut cout
	ON cop.colName = cout.colName
	AND (cop.DataType = cout.DataType OR @UseAllColumns = 1 OR @EqualizeTextData = 1)
--WHERE cin.DataType NOT IN ('text','ntext','image')  --Not all datatypes can be compared.
--	AND cout.DataType NOT IN ('text','ntext','image');

IF (SELECT count(*) FROM #MatchColumns) <> (SELECT count(*) FROM #CopyColumns) AND @UseAllColumns = 1
	BEGIN
		-- If all columns must be used but cannot be matched, abort the procedure and raise an error in the log.
		print N'Not all columns could be matched, abort procedure'
		--RAISERROR (N'Not all columns could be matched, abort procedure',16,1) WITH LOG
		RETURN
	END

IF (SELECT count(*) FROM #MatchColumns) = 0
	BEGIN
		print 'No matching columns found, abort action'
		RETURN
	END

DECLARE matchColumn_cursor CURSOR SCROLL FOR
	SELECT colName
	FROM #MatchColumns

SET @RowCounter = 1
OPEN matchColumn_cursor
FETCH FIRST FROM matchColumn_cursor into @ColName
WHILE @@FETCH_STATUS = 0
BEGIN
	IF @RowCounter > 1 SET @CopyColumnList = COALESCE(@CopyColumnList,'') + ','
	IF @RowCounter > 1 SET @MatchColumnList = COALESCE(@MatchColumnList,'') + ','

	SET @CopyColumnList = COALESCE(@CopyColumnList,'') + (
		SELECT TOP 1 CASE WHEN cin.DataType = cout.DataType AND cin.DataType LIKE '%char' AND @UseTargetCollation = 1  THEN '[' + cin.colName + '] COLLATE DATABASE_DEFAULT'
						WHEN cin.DataType = cout.DataType THEN '[' + cin.colName + ']'
						WHEN cout.DataType IN ('binary','varbinary','bigint','int','smallint','tinyint','money','smallmoney','bit','float','real','timestamp','date','time','datetime','smalldatetime','datetime2','datetimeoffset') THEN 'CAST([' + cin.colName + '] AS ' + cout.DataType + ')'
						WHEN cout.DataType IN ('decimal','numeric') THEN 'CAST([' + cin.colName + '] AS ' + cout.DataType + '(' + CAST(cout.[precision] as nvarchar(10)) + ',' + CAST(cout.scale as nvarchar(10)) + '))'
						WHEN cout.DataType LIKE ('%char') AND @UseTargetCollation = 1 THEN 'CAST([' + cin.colName + '] AS ' + cout.DataType + '(' + CAST(IIF(cout.DataType LIKE 'n%',cout.max_length/2,cout.max_length) as nvarchar(10)) + ')) COLLATE DATABASE_DEFAULT'
						WHEN cout.DataType LIKE ('%char') AND @UseTargetCollation = 0 THEN 'CAST([' + cin.colName + '] AS ' + cout.DataType + '(' + CAST(IIF(cout.DataType LIKE 'n%',cout.max_length/2,cout.max_length) as nvarchar(10)) + '))'
						ELSE '[' + cin.colName + ']'  --hope for implicit coversion.
					END
		FROM #ColumnsIn cin
		INNER JOIN #ColumnsOut cout
			ON cin.colName = cout.colName
			AND (cin.DataType = cout.DataType OR @UseAllColumns = 1 OR @EqualizeTextData = 1)
		WHERE cin.colName = @ColName)

	SET @MatchColumnList = COALESCE(@MatchColumnList,'') + '[' + @ColName + ']'

	SET @RowCounter = @RowCounter + 1
	FETCH NEXT FROM matchColumn_cursor into @ColName
END
CLOSE matchColumn_cursor

IF COALESCE(@MatchColumnList,'') = ''
	BEGIN
		print 'No columns could be matched, abort action'
		RETURN
	END

IF @DebugMode = 1 print '@CopyColumnList = ' + @CopyColumnList
IF @DebugMode = 1 print '@MatchColumnList = ' + @MatchColumnList

IF @ClearTargetTable = 0
	BEGIN
		--Search for compare columns in dbo.SmartUpdate. If no columns exist, use all columns.
		IF @DebugMode = 1 print 'check for compare columns defined for Source.'
		SELECT ColumnName, DataType
		INTO #CompareColumns
		FROM dbo.SmartUpdate
		WHERE SchemaName = @SourceSchemaName
			AND TableName = @SourceTableName
			AND CompareColumn = 1
			AND @RunTime BETWEEN DateStart AND COALESCE(DateStop,@RunTime + 1)
			AND Active = 1

		IF (SELECT COUNT(*) FROM #CompareColumns) = 0 
			BEGIN
				IF @DebugMode = 1 print 'no compare columns defined for Source, check to see if there are columns for the target.'
				INSERT INTO #CompareColumns 
				SELECT ColumnName, DataType
				FROM dbo.SmartUpdate
				WHERE SchemaName = @TargetSchemaName
					AND TableName = @TargetTableName
					AND CompareColumn = 1
					AND @RunTime BETWEEN DateStart AND COALESCE(DateStop,@RunTime + 1)
					AND Active = 1
			END

		IF (SELECT COUNT(*) FROM #CompareColumns) = 0 
			BEGIN
				IF @DebugMode = 1 print 'no columns defined, use all matching columns'
				INSERT INTO #CompareColumns 
				SELECT #ColumnsIn.[colName] COLLATE DATABASE_DEFAULT AS [colName], #ColumnsIn.DataType
				FROM #ColumnsIn
				INNER JOIN #ColumnsOut
					ON #ColumnsIn.colName = #ColumnsOut.colName
					AND (#ColumnsIn.DataType = #ColumnsOut.DataType)
				WHERE #ColumnsIn.DataType NOT IN ('text','ntext','image') --Not all datatypes can be compared.
			END

		IF (SELECT COUNT(*) FROM #CompareColumns) = 0 
			BEGIN
				print 'no matching columns found to compare, abort action'
				Return
			END

		--Build the WHERE clause
		DECLARE CompareColumns_cursor CURSOR SCROLL FOR
			SELECT #CompareColumns.[ColumnName] COLLATE DATABASE_DEFAULT, #CompareColumns.DataType
			FROM #CompareColumns

		SET @RowCounter = 1
		SET @WhereClause = ''
		OPEN CompareColumns_cursor
		FETCH FIRST FROM CompareColumns_cursor into @ColName, @DataTypeIn
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @RowCounter = 1 SET @WhereClause = @WhereClause + 'WHERE '
			IF @RowCounter = 1 AND @SourceFilter IS NOT NULL SET @WhereClause = @WhereClause + 'stn.' + @SourceFilter + ' AND '
			IF @RowCounter = 1 SET @WhereClause = @WhereClause + '('
			IF @RowCounter > 1 SET @WhereClause = @WhereClause + ' OR '

			SET @WhereClause = COALESCE(@WhereClause,'') + (
				SELECT TOP 1 CASE WHEN cin.DataType = cout.DataType AND cin.DataType LIKE '%char' AND @UseTargetCollation = 1 THEN 'COALESCE(stn.[' + cin.colName + '],'''') COLLATE DATABASE_DEFAULT <> COALESCE(ttn.[' + @ColName + '],'''') COLLATE DATABASE_DEFAULT' + @NewLineChar
								WHEN cin.DataType = cout.DataType AND cin.DataType IN ('bigint','int','smallint','tinyint','money','smallmoney','bit','decimal','numeric','float','real','timestamp') THEN 'COALESCE(stn.[' + cin.colName + '],0) <> COALESCE(ttn.[' + @ColName + '],0)' + @NewLineChar
								WHEN cin.DataType = cout.DataType THEN 'COALESCE(stn.[' + cin.colName + '],'''') <> COALESCE(ttn.[' + @ColName + '],'''')' + @NewLineChar
								WHEN cin.DataType IN ('date','time','datetime2','datetimeoffset') AND cout.DataType IN ('date','time','datetime2','datetimeoffset') THEN 'CAST(COALESCE(stn.[' + cin.colName + '],CAST(CAST(0 AS datetime) AS ' + cin.DataType + ')) AS ' + cout.DataType + ') <> CAST(COALESCE(ttn.[' + @ColName + '],CAST(CAST(0 AS datetime) AS ' + cout.DataType + ')) AS ' + cout.DataType + ')' + @NewLineChar
								WHEN cin.DataType IN ('date','time','datetime2','datetimeoffset') AND cout.DataType NOT IN ('date','time','datetime2','datetimeoffset') THEN 'CAST(COALESCE(stn.[' + cin.colName + '],CAST(CAST(0 AS datetime) AS ' + cin.DataType + ')) AS ' + cout.DataType + ') <> CAST(COALESCE(ttn.[' + @ColName + '],CAST(0 AS ' + cout.DataType + ')) AS ' + cout.DataType + ')' + @NewLineChar
								WHEN cin.DataType NOT IN ('date','time','datetime2','datetimeoffset') AND cout.DataType IN ('date','time','datetime2','datetimeoffset') THEN 'CAST(COALESCE(stn.[' + cin.colName + '],CAST(0 AS ' + cin.DataType + ')) AS ' + cout.DataType + ') <> CAST(COALESCE(ttn.[' + @ColName + '],CAST(CAST(0 AS datetime) AS ' + cout.DataType + ')) AS ' + cout.DataType + ')' + @NewLineChar
								WHEN cout.DataType IN ('datetime','smalldatetime') THEN 'CAST(COALESCE(stn.[' + cin.colName + '],CAST(0 AS ' + cin.DataType + ')) AS ' + cout.DataType + ') <> CAST(COALESCE(ttn.[' + @ColName + '],CAST(0 AS ' + cout.DataType + ')) AS ' + cout.DataType + ')' + @NewLineChar
								WHEN cout.DataType IN ('binary','varbinary','bigint','int','smallint','tinyint','money','smallmoney','bit','float','real','timestamp') THEN 'CAST(COALESCE(stn.[' + cin.colName + '],0) AS ' + cout.DataType + ') <> COALESCE(ttn.[' + @ColName + '],0)' + @NewLineChar
								WHEN cout.DataType IN ('decimal','numeric') THEN 'CAST(COALESCE(stn.[' + cin.colName + '],0) AS ' + cout.DataType + '(' + CAST(cout.[precision] as nvarchar(10)) + ',' + CAST(cout.scale as nvarchar(10)) + ')) <> COALESCE(ttn.[' + @ColName + '],0)' + @NewLineChar
								WHEN cout.DataType LIKE ('%char') AND @UseTargetCollation = 1 THEN 'CAST(COALESCE(stn.[' + cin.colName + '],'''') AS nvarchar(' + CAST(cout.max_length as nvarchar(10)) + ')) COLLATE DATABASE_DEFAULT <> COALESCE(ttn.[' + @ColName + '],'''') COLLATE DATABASE_DEFAULT' + @NewLineChar
								WHEN cout.DataType LIKE ('%char') AND @UseTargetCollation = 0 THEN 'CAST(COALESCE(stn.[' + cin.colName + '],'''') AS nvarchar(' + CAST(cout.max_length as nvarchar(10)) + ')) <> COALESCE(ttn.[' + @ColName + '],'''')' + @NewLineChar
								ELSE 'stn.[' + cin.colName + '] <> ttn.[' + @ColName + ']' + @NewLineChar  --hope for correct implicit coversion, if needed, and no NULL values.
							END
			FROM #ColumnsIn cin
			INNER JOIN #ColumnsOut cout
				ON cin.colName = cout.colName
				AND (cin.DataType = cin.DataType OR @UseAllColumns = 1 OR @EqualizeTextData = 1)
			WHERE cin.colName = @ColName)

			--IF @DataTypeIn IN ('bigint','int','smallint','tinyint','money','smallmoney','bit','decimal','numeric','float','real','timestamp')
			--	BEGIN
			--		SET @WhereClause = @WhereClause + 'ISNULL(stn.[' + @ColName + '],0)'
			--	END
			--ELSE
			--	BEGIN
			--		SET @WhereClause = @WhereClause + 'ISNULL(stn.[' + @ColName + '],'''')'
			--	END
			--IF @UseTargetCollation = 1 AND @DataTypeIn IN ('nchar','nvarchar','char','varchar') SET @WhereClause = @WhereClause + ' COLLATE DATABASE_DEFAULT '
			--IF @DataTypeIn IN ('bigint','int','smallint','tinyint','money','smallmoney','bit','decimal','numeric','float','real','timestamp')
			--	BEGIN
			--		SET @WhereClause = @WhereClause + ' <> ISNULL(ttn.[' + @ColName + '],0)'
			--	END
			--ELSE
			--	BEGIN
			--		SET @WhereClause = @WhereClause + ' <> ISNULL(ttn.[' + @ColName + '],'''')'
			--	END
			--IF @UseTargetCollation = 1 AND @DataTypeIn IN ('nchar','nvarchar','char','varchar') SET @WhereClause = @WhereClause + ' COLLATE DATABASE_DEFAULT '
			--SET @WhereClause = @WhereClause + @NewLineChar
			SET @RowCounter = @RowCounter + 1
			FETCH NEXT FROM CompareColumns_cursor into @ColName, @DataTypeIn
		END
		SET @WhereClause = @WhereClause + ')'
		CLOSE CompareColumns_cursor
		DEALLOCATE CompareColumns_cursor
		IF @DebugMode = 1 print '@WhereClause = ' + @WhereClause


		--Build the JOIN clause
		DECLARE pk_cursor CURSOR SCROLL FOR
			SELECT #PKColumns.[ColumnName] COLLATE DATABASE_DEFAULT
			FROM #PKColumns

		SET @RowCounter = 1
		SET @JoinClause = ''
		OPEN pk_cursor
		FETCH FIRST FROM pk_cursor into @ColName
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @RowCounter = 1 SET @JoinClause = @JoinClause + 'ON '
			IF @RowCounter = 1 SET @PkColName = @ColName
			IF @RowCounter > 1 SET @JoinClause = @JoinClause + 'AND '

			SET @JoinClause = COALESCE(@JoinClause,N'') + (
				SELECT TOP 1 CASE WHEN cin.DataType = cout.DataType AND cin.DataType LIKE '%char' AND @UseTargetCollation = 1  THEN 'stn.[' + cin.colName + '] COLLATE DATABASE_DEFAULT = ttn.[' + @ColName + '] COLLATE DATABASE_DEFAULT' + @NewLineChar
								WHEN cin.DataType = cout.DataType THEN 'stn.[' + cin.colName + '] = ttn.[' + @ColName + ']' + @NewLineChar
								WHEN cout.DataType IN ('binary','varbinary','bigint','int','smallint','tinyint','money','smallmoney','bit','float','real','timestamp','date','time','datetime','smalldatetime','datetime2','datetimeoffset') THEN 'CAST(stn.[' + cin.colName + '] AS ' + cout.DataType + ') = ttn.[' + @ColName + ']' + @NewLineChar
								WHEN cout.DataType IN ('decimal','numeric') THEN 'CAST(stn.[' + cin.colName + '] AS ' + cout.DataType + '(' + CAST(cout.[precision] as nvarchar(10)) + ',' + CAST(cout.scale as nvarchar(10)) + ')) = ttn.[' + @ColName + ']' + @NewLineChar
								WHEN cout.DataType LIKE ('%char') AND @UseTargetCollation = 1 THEN 'CAST(stn.[' + cin.colName + '] AS nvarchar(' + CAST(cout.max_length as nvarchar(10)) + ')) COLLATE DATABASE_DEFAULT = ttn.[' + @ColName + '] COLLATE DATABASE_DEFAULT' + @NewLineChar
								WHEN cout.DataType LIKE ('%char') AND @UseTargetCollation = 0 THEN 'CAST(stn.[' + cin.colName + '] AS nvarchar(' + CAST(cout.max_length as nvarchar(10)) + ')) = ttn.[' + @ColName + ']' + @NewLineChar
								ELSE 'stn.[' + cin.colName + '] = ttn.[' + @ColName + ']' + @NewLineChar  --hope for correct implicit coversion, if needed, and no NULL values.
							END
				FROM #ColumnsIn cin
				INNER JOIN #ColumnsOut cout
					ON cin.colName = cout.colName
					AND (cin.DataType = cin.DataType OR @UseAllColumns = 1 OR @EqualizeTextData = 1)
				WHERE cin.colName = @ColName)

			--IF @DataTypeIn LIKE '%char%' AND @UseTargetCollation = 1 
			--	SET @JoinClause = @JoinClause + 'stn.[' + @ColName + '] COLLATE DATABASE_DEFAULT = ttn.[' + @ColName + '] COLLATE DATABASE_DEFAULT' + @NewLineChar
			--ELSE
			--	SET @JoinClause = @JoinClause + 'stn.[' + @ColName + '] = ttn.[' + @ColName + ']' + @NewLineChar
	
			SET @RowCounter = @RowCounter + 1
			FETCH NEXT FROM pk_cursor into @ColName
		END
		CLOSE pk_cursor
		DEALLOCATE pk_cursor
		IF @DebugMode = 1 print '@JoinClause = ' + @JoinClause

	END

		--If Audit table does not exist, create it. Audit table = Target table name with _Audit and a SU_AuditLogDate and SU_AuditAction column
	If @UseAuditTable = 1
		BEGIN
			SELECT col.name As colName
				,typ.name as DataType
				,col.is_nullable
				,col.is_computed
				,col.[object_id]
				,col.is_identity
				,idt.seed_value
				,idt.increment_value
			INTO #ColumnsAudit
			FROM sys.columns col
			INNER JOIN sys.types typ
				ON col.system_type_id = typ.system_type_id
				AND col.user_type_id = typ.user_type_id
			INNER JOIN sys.objects obj
				ON col.[object_id] = obj.[object_id]
			INNER JOIN sys.schemas scm
				ON obj.[schema_id] = scm.[schema_id]
			LEFT OUTER JOIN sys.identity_columns idt
				ON col.[object_id] = idt.[object_id]
				AND col.name = idt.name
			WHERE obj.name = @AuditTableName
				AND scm.name = @TargetSchemaName

			--If Audit table has no columns, create table or do not use auditing
			IF (SELECT COUNT(*) FROM #ColumnsAudit) = 0 
				BEGIN
					IF @CreateAuditTable = 1
						BEGIN
							DECLARE TargetColumn_cursor CURSOR SCROLL FOR
								SELECT #ColumnsOut.[colName], #ColumnsOut.DataType COLLATE DATABASE_DEFAULT
								FROM #ColumnsOut

							SET @RowCounter = 1
							OPEN TargetColumn_cursor
							FETCH FIRST FROM TargetColumn_cursor into @ColName, @DataTypeIn
							WHILE @@FETCH_STATUS = 0
							BEGIN
								IF @RowCounter > 1 SET @TargetColumnList = COALESCE(@TargetColumnList,'') + ','
								SET @TargetColumnList = COALESCE(@TargetColumnList,'') + '[tbl1].[' + @ColName + ']' --+ @NewLineChar
								IF @UseTargetCollation = 1 AND @DataTypeIn IN ('nchar','nvarchar','char','varchar') SET @TargetColumnList = @TargetColumnList + ' COLLATE Database_Default AS [' + @ColName + ']'
								SET @RowCounter = @RowCounter + 1
								FETCH NEXT FROM TargetColumn_cursor into @ColName, @DataTypeIn
							END
							CLOSE TargetColumn_cursor
							DEALLOCATE TargetColumn_cursor
							DECLARE @AuditAction AS nvarchar(100) = ''

							SET @CreateCommand = 'SELECT TOP 0 ' + @TargetColumnList + ', @RunTime AS SU_AuditLogDate, @AuditAction As SU_AuditAction' + @NewLineChar
							SET @CreateCommand = @CreateCommand + 'INTO ' + @TargetSchemaName + '.' + @AuditTableName + @NewLineChar
							SET @CreateCommand = @CreateCommand + 'FROM ' + @TargetSchemaName + '.' + @TargetTableName + ' AS [tbl1]' + @NewLineChar
							SET @CreateCommand = @CreateCommand + 'LEFT OUTER JOIN ' + @TargetSchemaName + '.' + @TargetTableName + ' AS [tbl2] ON 1=1' + @NewLineChar
							IF @DebugMode = 1 print '@CreateCommand = ' + @CreateCommand
							EXECUTE sp_ExecuteSql @CreateCommand, N'@RunTime smalldatetime,@AuditAction nvarchar(100)', @RunTime = @RunTime, @AuditAction = @AuditAction
							print 'Audit Table created'
						END
					ELSE
						BEGIN
							--Cannot use Audit table if it doesn't exist and cannot be created
							print 'No audit tabel available. do not use audit'
							SET @UseAuditTable = 0
						END
				END

		END


		--PHASE 1: Check for old data in target that no longer exists in Source. Remove data from target.
IF @ClearTargetTable = 1
	BEGIN
		SET @UseAuditTable = 0
		IF @DebugMode = 1 print 'removing all old data'
		BEGIN TRY
			--Remove all data at once
			SET @CopyCommand = ''
			SET @CopyCommand = 'TRUNCATE TABLE ' + @TargetSchemaName + '.' + @TargetTableName
			IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
			EXECUTE sp_ExecuteSql @CopyCommand
			print 'Table ' + @TargetSchemaName + '.' + @TargetTableName + ' truncated'
		END TRY
		BEGIN CATCH
			--Data removal failed, probably because of a constraint. Try removing every row.
			SET @CopyCommand = ''
			SET @CopyCommand = 'DELETE FROM ' + @TargetSchemaName + '.' + @TargetTableName
			IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
			EXECUTE sp_ExecuteSql @CopyCommand
			print cast(@@RowCount as nvarchar(10)) + ' rows deleted'
		END CATCH
	END

IF @RemoveNonSourceData = 1 AND @ClearTargetTable = 0
	BEGIN
		--select columns from target left outer join source on matching columns where target PK is null
		SET @FromClause = ''
		SET @FromClause = COALESCE(@FromClause,'') + ' FROM ' + @TargetSchemaName + '.' + @TargetTableName + ' ttn ' + @NewLineChar 
		SET @FromClause = @FromClause + ' LEFT OUTER JOIN ' + @SourceSchemaName + '.' + @SourceTableName + ' stn' + @NewLineChar 
		SET @FromClause = @FromClause + @JoinClause
		IF @sourceFilter IS NOT NULL SET @FromClause = @FromClause + ' AND stn.' + @SourceFilter
		SET @FromClause = @FromClause + 'WHERE stn.[' + @PkColName + '] IS NULL'
		IF @DebugMode = 1 print '@FromClause = ' + @FromClause

		IF @UseAuditTable = 1
			BEGIN
				--select old data in temp table
				IF @DebugMode = 1 print 'Logging old data'
				--log data in Audit table with SU_AuditLogDate = @RunTime and SU_AuditLogAction = 'Delete'
				SET @CopyCommand = ''
				SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @AuditTableName + '(' + @MatchColumnList + ',SU_AuditLogDate,SU_AuditAction' + ')' + @NewLineChar 
				SET @CopyCommand = @CopyCommand + 'SELECT ' + replace(@MatchColumnList,'[','ttn.[') + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''',''' + N'Delete''' + @NewLineChar
				SET @CopyCommand = @CopyCommand + @FromClause
				IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
				EXECUTE sp_ExecuteSql @CopyCommand
				print cast(@@RowCount as nvarchar(10)) + ' delete rows logged to audit table'
			END
		IF @DebugMode = 1 print 'removing old data'
		SET @CopyCommand = ''
		SET @CopyCommand = 'DELETE FROM ' + @TargetSchemaName + '.' + @TargetTableName + @NewLineChar 
		SET @CopyCommand = @CopyCommand + ' WHERE [' + @PkColName + '] IN (SELECT ttn.[' + @PkColName + ']' + @FromClause + ')'
		IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
		EXECUTE sp_ExecuteSql @CopyCommand
		print cast(@@RowCount as nvarchar(10)) + ' rows deleted'
	END
	
--PHASE 2: Check for data that exists in both Source and target and has differences in Matching columns.


IF @ClearTargetTable = 0
	BEGIN

		IF @UseAuditTable = 1
			BEGIN
				IF @DebugMode = 1 print 'Write changed data to audit table'
				SET @CopyCommand = ''
				SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @AuditTableName + '(' + @MatchColumnList + ',SU_AuditLogDate,SU_AuditAction' + ')' + @NewLineChar 
				SET @CopyCommand = @CopyCommand + 'SELECT ' + replace(@MatchColumnList,'[','ttn.[') + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''',''' + N'Update''' + @NewLineChar
				SET @CopyCommand = @CopyCommand + 'FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' stn' + @NewLineChar
				SET @CopyCommand = @CopyCommand + 'INNER JOIN ' + @TargetSchemaName + '.' + @TargetTableName + ' ttn' + @NewLineChar
				SET @CopyCommand = @CopyCommand + @JoinClause
				SET @CopyCommand = @CopyCommand + @WhereClause

				IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
				EXECUTE sp_ExecuteSql @CopyCommand
				print cast(@@RowCount as nvarchar(10)) + ' update rows logged to audit table'
			END

		IF @DebugMode = 1 print 'update changed data in target table to new data'
		SET @CopyCommand = ''
		SET @CopyCommand = 'UPDATE ' + @TargetSchemaName + '.' + @TargetTableName + @NewLineChar 
		SET @CopyCommand = @CopyCommand + 'SET ' + @NewLineChar

		SET @RowCounter = 1
		SET @SetClause = ''
		OPEN matchColumn_cursor
		FETCH FIRST FROM matchColumn_cursor into @ColName
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @ColName NOT IN (SELECT #PKColumns.[ColumnName] COLLATE DATABASE_DEFAULT	FROM #PKColumns)
				BEGIN
					IF @RowCounter > 1 SET @SetClause = COALESCE(@SetClause,'') + ','
					SET @SetClause = COALESCE(@SetClause,'') + @TargetSchemaName + '.' + @TargetTableName + '.[' + @ColName + ']= stn.[' + @ColName + ']' + @NewLineChar
					SET @RowCounter = @RowCounter + 1
				END
			FETCH NEXT FROM matchColumn_cursor into @ColName
		END
		CLOSE matchColumn_cursor
		-- IF @SetClause = '' Then all columns must be in the primary key.

		SET @CopyCommand = @CopyCommand + @SetClause
		SET @CopyCommand = @CopyCommand + 'FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' stn' + @NewLineChar
		SET @CopyCommand = @CopyCommand + 'INNER JOIN ' + @TargetSchemaName + '.' + @TargetTableName + ' ttn' + @NewLineChar
		SET @CopyCommand = @CopyCommand + @JoinClause
		SET @CopyCommand = @CopyCommand + @WhereClause

		IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
		IF @SetClause <> '' EXECUTE sp_ExecuteSql @CopyCommand
		print cast(@@RowCount as nvarchar(10)) + ' rows updated'
	END

--PHASE 3: Insert new data into Target table

		--select columns from target left outer join source on matching columns where target PK is null
SET @FromClause = ''
SET @FromClause = COALESCE(@FromClause,'') + ' FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' stn ' + @NewLineChar 
SET @FromClause = @FromClause + ' LEFT OUTER JOIN ' + @TargetSchemaName + '.' + @TargetTableName + ' ttn' + @NewLineChar 
SET @FromClause = @FromClause + @JoinClause
SET @FromClause = @FromClause + 'WHERE ttn.[' + @PkColName + '] IS NULL'
IF @SourceFilter IS NOT NULL SET @FromClause = @FromClause + ' AND stn.' + @SourceFilter

IF COALESCE(@FromClause,'') = '' AND @ClearTargetTable = 1
BEGIN
	SET @FromClause = ''
	SET @FromClause = COALESCE(@FromClause,'') + ' FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' stn ' + @NewLineChar 
	IF @SourceFilter IS NOT NULL SET @FromClause = @FromClause + ' WHERE stn.' + @SourceFilter
END
IF @DebugMode = 1 print '@FromClause = ' + @FromClause

IF (SELECT COUNT(*) FROM #ColumnsOut WHERE colName = 'SU_LogDate') = 0
	BEGIN
		IF @UseAuditTable = 1
			BEGIN
				IF @DebugMode = 1 print 'Write new data to audit table'
				SET @CopyCommand = ''
				SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @AuditTableName + '(' + @MatchColumnList + ',SU_AuditLogDate,SU_AuditAction' + ')' + @NewLineChar 
				SET @CopyCommand = @CopyCommand + 'SELECT ' + replace(@MatchColumnList,'[','stn.[') + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''' AS [SU_AuditLogDate],''' + N'Insert'' AS [SU_AuditAction]' + @NewLineChar
				SET @CopyCommand = @CopyCommand + @FromClause
				IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
				EXECUTE sp_ExecuteSql @CopyCommand
				print cast(@@RowCount as nvarchar(10)) + ' insert rows logged in audit table'
			END
	END

IF @DebugMode = 1 print 'Write new data to Target table'
SET @CopyCommand = ''
SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @TargetTableName + '(' + @MatchColumnList 
IF (SELECT COUNT(*) FROM #ColumnsOut WHERE colName = 'SU_LogDate') > 0 SET @CopyCommand = @CopyCommand + ',[SU_LogDate]'
SET @CopyCommand = @CopyCommand + ')' + @NewLineChar 
SET @CopyCommand = @CopyCommand + 'SELECT '
IF @UseDistinct = 1 SET @CopyCommand = @CopyCommand + 'DISTINCT '
SET @CopyCommand = @CopyCommand + replace(@CopyColumnList,'[','stn.[') + @NewLineChar

IF (SELECT COUNT(*) FROM #ColumnsOut WHERE colName = 'SU_LogDate') > 0 SET @CopyCommand = @CopyCommand + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''' AS [SU_LogDate]' + @NewLineChar
SET @CopyCommand = @CopyCommand + @FromClause
IF @DebugMode = 1 print '@CopyCommand = ' + @CopyCommand
EXECUTE sp_ExecuteSql @CopyCommand
print cast(@@RowCount as nvarchar(10)) + ' rows inserted'

--Cleanup
DEALLOCATE matchColumn_cursor

Print 'End ' + Convert(nvarchar(19),GetDate(),120) + N' ' + @SourceSchemaName + '.' + @SourceTableName + ', ' +  @TargetSchemaName + '.' + @TargetTableName
