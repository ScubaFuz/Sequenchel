CREATE PROCEDURE [dbo].[usp_SmartUpdate] 
--declare
	@SourceSchemaName [sysname]
	,@SourceTableName [sysname]
	,@TargetSchemaName [sysname]
	,@TargetTableName [sysname]
	,@CreateTargetTable [bit] = 0
	,@UseAuditTable [bit] = 1
	,@CreateAuditTable [bit] = 0
	,@RemoveNonSourceData [bit] = 1
	,@UseTargetCollation [bit] = 0
WITH ENCRYPTION
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Update a target table with all new / changed / deleted data from a Source table
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2016-02-19	BT		Initial version
-- 
-- ****************************************************************************

/**********DEBUG******************************
	SET @SourceSchemaName = 'dbo'
	SET @SourceTableName = 'tbl_Servers'
	SET @TargetSchemaName = 'dbo'
	SET @TargetTableName = 'DWH_tbl_Servers'
	SET @CreateTargetTable = 1
	SET @UseAuditTable = 1
	SET @CreateAuditTable = 1
	SET @RemoveNonSourceData = 1
	SET @UseTargetCollation = 1
**********DEBUG******************************/

SET NOCOUNT ON

Print 'Start ' + Convert(nvarchar(19),GetDate(),120) + N' ' + @SourceSchemaName + '.' + @SourceTableName + ', ' +  @TargetSchemaName + '.' + @TargetTableName

DECLARE @AuditTableName [sysname], @RunTime smalldatetime, @RunTimeChar nvarchar(100)
DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10)
DECLARE @CreateCommand nvarchar(max), @CopyCommand nvarchar(max)
DECLARE @FromClause nvarchar(1000), @JoinClause nvarchar(max), @WhereClause nvarchar(max), @SetClause nvarchar(max)
DECLARE @ColName sysname, @PkColName sysname, @DataType nvarchar(100)
DECLARE @SourceColumnList nvarchar(max), @TargetColumnList nvarchar(max), @MatchColumnList nvarchar(max), @PKColumnList nvarchar(max)
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
IF object_id('tempdb..##TempData') IS NOT NULL DROP TABLE ##TempData
IF object_id('tempdb..#TempColumns') IS NOT NULL DROP TABLE #TempColumns
IF object_id('tempdb..#PKColumns') IS NOT NULL DROP TABLE #PKColumns
IF object_id('tempdb..#CompareColumns') IS NOT NULL DROP TABLE #CompareColumns

--Find columns for Source table
SELECT col.name As colName
	,typ.name as DataType
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
	RETURN
END

--find target table columns
SELECT col.name As colName
	,typ.name as DataType
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

--If target table has no columns create table or exit
IF (SELECT COUNT(*) FROM #ColumnsOut) = 0 
	BEGIN
		IF @CreateTargetTable = 1
			BEGIN
				print 'Create Target Table'
				DECLARE AllColumn_cursor CURSOR SCROLL FOR
					SELECT #ColumnsIn.[colName], #ColumnsIn.DataType COLLATE DATABASE_DEFAULT
					FROM #ColumnsIn

				SET @RowCounter = 1
				OPEN AllColumn_cursor
				FETCH FIRST FROM AllColumn_cursor into @ColName, @DataType
				WHILE @@FETCH_STATUS = 0
				BEGIN
					IF @RowCounter > 1 SET @SourceColumnList = COALESCE(@SourceColumnList,'') + ','
					SET @SourceColumnList = COALESCE(@SourceColumnList,'') + '[tbl1].[' + @ColName + ']' --+ @NewLineChar
					IF @UseTargetCollation = 1 AND @DataType IN ('nchar','nvarchar','char','varchar') SET @SourceColumnList = @SourceColumnList + ' COLLATE Database_Default AS [' + @ColName + ']'
					SET @RowCounter = @RowCounter + 1
					FETCH NEXT FROM AllColumn_cursor into @ColName, @DataType
				END
				CLOSE AllColumn_cursor
				DEALLOCATE AllColumn_cursor

				SET @CreateCommand = 'SELECT TOP 0 ' + @SourceColumnList + @NewLineChar
				IF NOT (@UseAuditTable = 0 AND @CreateAuditTable = 0) SET @CreateCommand = @CreateCommand + ', @RunTime AS SU_LogDate' + @NewLineChar
				SET @CreateCommand = @CreateCommand + 'INTO ' + @TargetSchemaName + '.' + @TargetTableName + @NewLineChar
				SET @CreateCommand = @CreateCommand + 'FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' AS [tbl1]' + @NewLineChar
				SET @CreateCommand = @CreateCommand + 'LEFT OUTER JOIN ' + @SourceSchemaName + '.' + @SourceTableName + ' AS [tbl2] ON 1=1' + @NewLineChar
				EXECUTE sp_ExecuteSql @CreateCommand, N'@RunTime smalldatetime', @RunTime = @RunTime
				
				INSERT INTO #ColumnsOut
				SELECT col.name As colName
					,typ.name as DataType
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

--Create a list of matching columns from Source and target table
DECLARE matchColumn_cursor CURSOR SCROLL FOR
	SELECT #ColumnsIn.[colName] COLLATE DATABASE_DEFAULT
	FROM #ColumnsIn
	INNER JOIN #ColumnsOut
		ON #ColumnsIn.colName = #ColumnsOut.colName
		AND #ColumnsIn.DataType = #ColumnsOut.DataType

SET @RowCounter = 1
OPEN matchColumn_cursor
FETCH FIRST FROM matchColumn_cursor into @ColName
WHILE @@FETCH_STATUS = 0
BEGIN
	IF @RowCounter > 1 SET @MatchColumnList = COALESCE(@MatchColumnList,'') + ','
	SET @MatchColumnList = COALESCE(@MatchColumnList,'') + '[' + @ColName + ']' --+ @NewLineChar
	SET @RowCounter = @RowCounter + 1
	FETCH NEXT FROM matchColumn_cursor into @ColName
END
CLOSE matchColumn_cursor

IF COALESCE(@MatchColumnList,'') = ''
	BEGIN
		print 'No matching columns found, abort action'
		RETURN
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
						FETCH FIRST FROM TargetColumn_cursor into @ColName, @DataType
						WHILE @@FETCH_STATUS = 0
						BEGIN
							IF @RowCounter > 1 SET @TargetColumnList = COALESCE(@TargetColumnList,'') + ','
							SET @TargetColumnList = COALESCE(@TargetColumnList,'') + '[tbl1].[' + @ColName + ']' --+ @NewLineChar
							IF @UseTargetCollation = 1 AND @DataType IN ('nchar','nvarchar','char','varchar') SET @TargetColumnList = @TargetColumnList + ' COLLATE Database_Default AS [' + @ColName + ']'
							SET @RowCounter = @RowCounter + 1
							FETCH NEXT FROM TargetColumn_cursor into @ColName, @DataType
						END
						CLOSE TargetColumn_cursor
						DEALLOCATE TargetColumn_cursor
						print 'Create Audit Table'
						DECLARE @AuditAction AS nvarchar(100) = ''

						SET @CreateCommand = 'SELECT TOP 0 ' + @TargetColumnList + ', @RunTime AS SU_AuditLogDate, @AuditAction As SU_AuditAction' + @NewLineChar
						SET @CreateCommand = @CreateCommand + 'INTO ' + @TargetSchemaName + '.' + @AuditTableName + @NewLineChar
						SET @CreateCommand = @CreateCommand + 'FROM ' + @TargetSchemaName + '.' + @TargetTableName + ' AS [tbl1]' + @NewLineChar
						SET @CreateCommand = @CreateCommand + 'LEFT OUTER JOIN ' + @TargetSchemaName + '.' + @TargetTableName + ' AS [tbl2] ON 1=1' + @NewLineChar
						EXECUTE sp_ExecuteSql @CreateCommand, N'@RunTime smalldatetime,@AuditAction nvarchar(100)', @RunTime = @RunTime, @AuditAction = @AuditAction
					END
				ELSE
					BEGIN
						--Cannot use Audit table if it doesn't exist and cannot be created
						print 'No audit tabel available. do not use audit'
						SET @UseAuditTable = 0
					END
			END

	END

--Determine the PrimayKey column(s). 
--If a primary key is defined in SmartUpdate for both Source and Target, the data must fullfill them both.
SELECT Distinct ColumnName
INTO #PKColumns
FROM dbo.SmartUpdate
WHERE ((SchemaName = @SourceSchemaName AND TableName = @SourceTableName)
	OR (SchemaName = @TargetSchemaName AND TableName = @TargetTableName))
	AND PrimaryKey = 1
	AND @RunTime BETWEEN DateStart AND COALESCE(DateStop,@RunTime + 1)
	AND Active = 1
	
IF (SELECT COUNT(*) FROM #PKColumns) = 0 
	BEGIN
		--No primary key was defined for this Source or target table in SmartUpdate. use the original Primary Key
		INSERT INTO #PKColumns SELECT ColName FROM #ColumnsIn WHERE pk = 1
		IF (SELECT COUNT(*) FROM #PKColumns) = 0 
			BEGIN
				--No primary key was defined for this Source table. use the Identity column
				INSERT INTO #PKColumns SELECT ColName FROM #ColumnsIn WHERE is_identity = 1
				IF (SELECT COUNT(*) FROM #PKColumns) = 0 
					BEGIN
						--No Identity column was defined for this Source table. use the Primary Key from Target
						INSERT INTO #PKColumns SELECT ColName FROM #ColumnsOut WHERE pk = 1
						IF (SELECT COUNT(*) FROM #PKColumns) = 0 
							BEGIN
								print 'No primary key was defined for the Source or Target table. Abort procedure'
								RETURN
							END
					END
			END
	END

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
	SET @JoinClause = @JoinClause + 'stn.[' + @ColName + '] = ttn.[' + @ColName + ']' + @NewLineChar
	
	SET @RowCounter = @RowCounter + 1
	FETCH NEXT FROM pk_cursor into @ColName
END
CLOSE pk_cursor
--print '@JoinClause = ' + @JoinClause

--Search for compare columns in dbo.SmartUpdate. If no columns exist, use all columns.
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
		print 'no columns defined for Source, check to see if there are columns for the target.'
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
		print 'no columns defined, use all matching columns'
		INSERT INTO #CompareColumns 
		SELECT #ColumnsIn.[colName] COLLATE DATABASE_DEFAULT AS [colName], #ColumnsIn.DataType
		FROM #ColumnsIn
		INNER JOIN #ColumnsOut
			ON #ColumnsIn.colName = #ColumnsOut.colName
			AND #ColumnsIn.DataType = #ColumnsOut.DataType 
		WHERE #ColumnsIn.DataType NOT IN ('text','ntext','image') --Not all datatypes can be compared.
	END

DECLARE CompareColumns_cursor CURSOR SCROLL FOR
	SELECT #CompareColumns.[ColumnName] COLLATE DATABASE_DEFAULT, #CompareColumns.DataType
	FROM #CompareColumns

SET @RowCounter = 1
SET @WhereClause = ''
OPEN CompareColumns_cursor
FETCH FIRST FROM CompareColumns_cursor into @ColName, @DataType
WHILE @@FETCH_STATUS = 0
BEGIN
	IF @RowCounter = 1 SET @WhereClause = @WhereClause + 'WHERE '
	IF @RowCounter > 1 SET @WhereClause = @WhereClause + 'OR '
	IF @DataType IN ('bigint','int','smallint','tinyint','money','smallmoney','bit','decimal','numeric','float','real','timestamp')
		BEGIN
			SET @WhereClause = @WhereClause + 'ISNULL(stn.[' + @ColName + '],0)'
		END
	ELSE
		BEGIN
			SET @WhereClause = @WhereClause + 'ISNULL(stn.[' + @ColName + '],'''')'
		END
	IF @UseTargetCollation = 1 AND @DataType IN ('nchar','nvarchar','char','varchar') SET @WhereClause = @WhereClause + ' COLLATE DATABASE_DEFAULT '
	IF @DataType IN ('bigint','int','smallint','tinyint','money','smallmoney','bit','decimal','numeric','float','real','timestamp')
		BEGIN
			SET @WhereClause = @WhereClause + ' <> ISNULL(ttn.[' + @ColName + '],0)'
		END
	ELSE
		BEGIN
			SET @WhereClause = @WhereClause + ' <> ISNULL(ttn.[' + @ColName + '],'''')'
		END
	IF @UseTargetCollation = 1 AND @DataType IN ('nchar','nvarchar','char','varchar') SET @WhereClause = @WhereClause + ' COLLATE DATABASE_DEFAULT '
	SET @WhereClause = @WhereClause + @NewLineChar
	SET @RowCounter = @RowCounter + 1
	FETCH NEXT FROM CompareColumns_cursor into @ColName, @DataType
END
CLOSE CompareColumns_cursor
--print '@WhereClause = ' + @WhereClause

--PHASE 1: Check for old data in target that no longer exists in Source. Remove data from target.
IF @RemoveNonSourceData = 1
	BEGIN
		--select columns from target left outer join source on matching columns where target PK is null
		SET @FromClause = ''
		SET @FromClause = COALESCE(@FromClause,'') + ' FROM ' + @TargetSchemaName + '.' + @TargetTableName + ' ttn ' + @NewLineChar 
		SET @FromClause = @FromClause + ' LEFT OUTER JOIN ' + @SourceSchemaName + '.' + @SourceTableName + ' stn' + @NewLineChar 
		SET @FromClause = @FromClause + @JoinClause
		SET @FromClause = @FromClause + 'WHERE stn.[' + @PkColName + '] IS NULL'
--print '@FromClause = ' + @FromClause

		IF @UseAuditTable = 1
			BEGIN
				--select old data in temp table
				print 'Logging old data'
				--log data in Audit table with SU_AuditLogDate = @RunTime and SU_AuditLogAction = 'Delete'
				SET @CopyCommand = ''
				SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @AuditTableName + '(' + @MatchColumnList + ',SU_AuditLogDate,SU_AuditAction' + ')' + @NewLineChar 
				SET @CopyCommand = @CopyCommand + 'SELECT ' + replace(@MatchColumnList,'[','ttn.[') + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''',''' + N'Delete''' + @NewLineChar
				SET @CopyCommand = @CopyCommand + @FromClause
--print '@CopyCommand = ' + @CopyCommand
				EXECUTE sp_ExecuteSql @CopyCommand
			END
		print 'removing old data'
		SET @CopyCommand = ''
		SET @CopyCommand = 'DELETE FROM ' + @TargetSchemaName + '.' + @TargetTableName + @NewLineChar 
		SET @CopyCommand = @CopyCommand + ' WHERE [' + @PkColName + '] IN (SELECT ttn.[' + @PkColName + ']' + @FromClause + ')'
--print '@CopyCommand = ' + @CopyCommand
		EXECUTE sp_ExecuteSql @CopyCommand
	END
	
--PHASE 2: Check for data that exists in both Source and target and has differences in Matching columns.

IF @UseAuditTable = 1
	BEGIN
		print 'Write old data to audit table'
		SET @CopyCommand = ''
		SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @AuditTableName + '(' + @MatchColumnList + ',SU_AuditLogDate,SU_AuditAction' + ')' + @NewLineChar 
		SET @CopyCommand = @CopyCommand + 'SELECT ' + replace(@MatchColumnList,'[','ttn.[') + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''',''' + N'Update''' + @NewLineChar
		SET @CopyCommand = @CopyCommand + 'FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' stn' + @NewLineChar
		SET @CopyCommand = @CopyCommand + 'INNER JOIN ' + @TargetSchemaName + '.' + @TargetTableName + ' ttn' + @NewLineChar
		SET @CopyCommand = @CopyCommand + @JoinClause
		SET @CopyCommand = @CopyCommand + @WhereClause

--print '@CopyCommand = ' + @CopyCommand
		EXECUTE sp_ExecuteSql @CopyCommand
	END
print 'update old data in target table to new data'
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

--print '@CopyCommand = ' + @CopyCommand
IF @SetClause <> '' EXECUTE sp_ExecuteSql @CopyCommand

--PHASE 3: Insert new data into Target table

		--select columns from target left outer join source on matching columns where target PK is null
SET @FromClause = ''
SET @FromClause = COALESCE(@FromClause,'') + ' FROM ' + @SourceSchemaName + '.' + @SourceTableName + ' stn ' + @NewLineChar 
SET @FromClause = @FromClause + ' LEFT OUTER JOIN ' + @TargetSchemaName + '.' + @TargetTableName + ' ttn' + @NewLineChar 
SET @FromClause = @FromClause + @JoinClause
SET @FromClause = @FromClause + 'WHERE ttn.[' + @PkColName + '] IS NULL'

IF (SELECT COUNT(*) FROM #ColumnsOut WHERE colName = 'SU_LogDate') = 0
	BEGIN
		IF @UseAuditTable = 1
			BEGIN
				print 'Write new data to audit table'

				SET @CopyCommand = ''
				SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @AuditTableName + '(' + @MatchColumnList + ',SU_AuditLogDate,SU_AuditAction' + ')' + @NewLineChar 
				SET @CopyCommand = @CopyCommand + 'SELECT ' + replace(@MatchColumnList,'[','stn.[') + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''' AS [SU_AuditLogDate],''' + N'Insert'' AS [SU_AuditAction]' + @NewLineChar
				SET @CopyCommand = @CopyCommand + @FromClause
--print '@CopyCommand = ' + @CopyCommand
				EXECUTE sp_ExecuteSql @CopyCommand
			END
	END

print 'Write new data to Target table'
SET @CopyCommand = ''
SET @CopyCommand = 'INSERT INTO ' + @TargetSchemaName + '.' + @TargetTableName + '(' + @MatchColumnList 
IF (SELECT COUNT(*) FROM #ColumnsOut WHERE colName = 'SU_LogDate') > 0 SET @CopyCommand = @CopyCommand + ',[SU_LogDate]'
SET @CopyCommand = @CopyCommand + ')' + @NewLineChar 
SET @CopyCommand = @CopyCommand + 'SELECT ' + replace(@MatchColumnList,'[','stn.[') + @NewLineChar
IF (SELECT COUNT(*) FROM #ColumnsOut WHERE colName = 'SU_LogDate') > 0 SET @CopyCommand = @CopyCommand + ',''' + CONVERT(nvarchar(20),@RunTime,120) + ''' AS [SU_LogDate]' + @NewLineChar
SET @CopyCommand = @CopyCommand + @FromClause
--print '@CopyCommand = ' + @CopyCommand
EXECUTE sp_ExecuteSql @CopyCommand


--Cleanup
DEALLOCATE matchColumn_cursor
DEALLOCATE CompareColumns_cursor
DEALLOCATE pk_cursor

Print 'End ' + Convert(nvarchar(19),GetDate(),120) + N' ' + @SourceSchemaName + '.' + @SourceTableName + ', ' +  @TargetSchemaName + '.' + @TargetTableName
