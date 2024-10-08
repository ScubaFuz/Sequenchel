CREATE PROCEDURE [dbo].[usp_DefragIndexes]
	@LinkedServer sysname = NULL,				--Server to process
	@Database sysname,							--Database to process
	@MaxTime int = 60,							--Maximum time before not starting on a new index
	@DefragLimit int = 5,						--Lower limit for Index Defrag
	@ReindexLimit int = 30,						--Lower limit for Index rebuild
	@IndexOnline int = 0,						--for index rebuild to happen online (only SQL Enterprise)
	@InventoryOnly int = 0,						--If set to 1 only the list will be created without defragging
	@Exceptionlist nvarchar(2000) = NULL,		--Table names not to process
	@Separator nchar(1) = ','					--Separator character for csv
--WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Defrag Indexes based on fragmentation level
-- ****************************************************************************
-- Version	Date		Author	Description
-- ******	**********	******	*********************************************
-- 1.0		2010-11-01	BT		Initial version
-- 1.1		2010-11-15	BT		Small improvements to make the exception list working
-- 1.2		2010-11-15	BT		Added StartDate and StopDate to prevent old indexes from being processed
-- 2.0		2010-11-16	BT		Replaced Index exceptionlist with Table exceptionlist
-- 2.1		2010-11-22	BT		Added TRY-CATCH within WHILE loop because of missing (temporary) index.
-- 2.5		2012-02-08	BT		Replaced function CsvToVarChar with udf_CsvToVarChar
--								Updated: EXECUTE sp_executesql @SQLstring, N'@OfflineColumns int', @OfflineColumns
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 3.0		2015-10-08	BT		Used max fragmentation to account for multiple Index Levels
-- ****************************************************************************
SET NOCOUNT ON
IF @LinkedServer IS NULL SET @LinkedServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(128))

DECLARE @SQLString nvarchar(4000), @RunTime datetime, @EndTime datetime, @Now datetime
DECLARE	@SchemaName sysname,
	@TableObjectID int,
	@TableName sysname,
	@IndexObjectID int,
	@IndexName sysname,
	@OfflineColumns int,
	@IndexLocks bit,
	@IndexRows int,
	@FragBefore int,
	@FragAfter int,
	@ProcessStart datetime,
	@IndexCount int,
	@MessageText nvarchar(4000)
declare @Exceptions table (Exception nvarchar(50) NOT NULL)

--******************************************
-- DEBUG
--******************************************
--DECLARE @Database sysname,
--	@MaxTime int,
--	@DefragLimit int,
--	@ReindexLimit int,
--	@IndexOnline int,
--	@Exceptionlist nvarchar(2000)
--
--set @Database = 'Messagent'
--set @MaxTime = 1
--set @DefragLimit = 5
--set @ReindexLimit = 30
--set @IndexOnline = 0
--set @Exceptionlist = NULL
--******************************************

/*** Set runtime values ***/
SET @RunTime = GetDate()
SET @EndTime = dateadd(minute,@MaxTime,@RunTime)

--print '@RunTime = ' + convert(varchar(20),@RunTime,120)
--print '@EndTime = ' + convert(varchar(20),@EndTime,120)

INSERT INTO @Exceptions SELECT * FROM udf_CsvToVarChar(@Exceptionlist,@Separator)
--SELECT Exception FROM @Exceptions

/*** Retrieve all indexes for given Database ***/
Print 'Retrieve all indexes for given Database'
IF object_id('tempdb..#Indexes') IS NOT NULL DROP TABLE #Indexes
CREATE TABLE #Indexes (
	LinkedServer sysname,
	DatabaseName sysname,
	SchemaName sysname,
	TableObjectID int,
	TableName sysname,
	IndexObjectID int,
	IndexName sysname,
	IndexRows int,
	IndexLocks bit,
	OfflineColums int,
	FragBefore int,
	ProcessStart datetime
	)

SET @SQLstring = '
	INSERT INTO #Indexes
	SELECT ''' + @LinkedServer + ''' AS [LinkedServer],
		''' + @Database + ''' AS [DatabaseName], 
		sch.name AS [SchemaName],
		obj.[object_id] AS [TableObjectID], 
		obj.[name] AS [TableName], 
		ind.[index_id] AS [IndexObjectID], 
		ind.[name] AS [IndexName], 
		NULL AS [IndexRows],
		ind.[allow_page_locks] AS IndexLocks,
		NULL AS [OfflineColums],
		NULL AS [FragBefore], 
		@RunTime AS [ProcessStart]
	FROM ' + @Database + '.sys.indexes ind
	INNER JOIN ' + @Database + '.sys.objects obj
		ON ind.[object_id] = obj.[object_id]
	INNER JOIN ' + @Database + '.sys.schemas sch
		ON obj.schema_id = sch.schema_id
	WHERE ind.type IN (1,2)
		AND obj.[type] = ''U''
		AND ind.[is_disabled] = 0
	'

--print @SQLstring
EXECUTE sp_executesql @SQLstring, N'@RunTime datetime', @RunTime
--SELECT * FROM #Indexes where TableObjectID = 1861841945

/*** insert missing records into tbl_IndexList, using timestamp ***/
Print 'insert missing records into tbl_IndexList, using timestamp'

INSERT INTO [dbo].[tbl_IndexList]
	([LinkedServer]
	,[DatabaseName]
	,[SchemaName]
	,[TableObjectID]
	,[TableName]
	,[IndexObjectID]
	,[IndexName]
	,[IndexRows]
	,[IndexLocks]
	,[FragBefore]
	,[DateStart]
	,[ProcessStart])
SELECT ind.LinkedServer,
	ind.DatabaseName,
	ind.SchemaName,
	ind.TableObjectID,
	ind.TableName,
	ind.IndexObjectID,
	ind.IndexName,
	ind.IndexRows,
	ind.IndexLocks,
	ind.FragBefore,
	@RunTime,
	ind.ProcessStart
FROM #Indexes ind
LEFT OUTER JOIN dbo.tbl_IndexList ixl
	ON ind.LinkedServer = ixl.LinkedServer
	AND ind.DatabaseName = ixl.DatabaseName
	AND ind.SchemaName = ixl.SchemaName
	AND ind.TableObjectID = ixl.TableObjectID
	AND ind.IndexObjectID = ixl.IndexObjectID
	AND isnull(ixl.DateStop,0) = 0
WHERE isnull(ixl.TableObjectID,0) = 0

/*** Close all records in tbl_IndexList that do not exist in #Indexes ***/
PRINT 'Close all records in tbl_IndexList that do not exist in #Indexes'

Update [dbo].tbl_IndexList
SET DateStop = @Runtime
FROM
	(SELECT org.LinkedServer, org.DatabaseName, org.TableName, org.IndexName, org.IndexLocks 
		FROM [dbo].tbl_IndexList org
		LEFT OUTER JOIN #Indexes tmp
			ON org.LinkedServer = tmp.LinkedServer
			AND org.DatabaseName = tmp.DatabaseName
			AND org.TableName = tmp.TableName
			AND org.IndexName = tmp.IndexName
			AND isnull(org.IndexLocks,-1) = isnull(tmp.IndexLocks,-1)
		WHERE org.LinkedServer = @LinkedServer
			AND org.DatabaseName = @Database
			AND isnull(tmp.DatabaseName,'') = ''
			AND isnull(org.DateStop,0) = 0) Q
WHERE [dbo].tbl_IndexList.LinkedServer = Q.LinkedServer
	AND [dbo].tbl_IndexList.DatabaseName = Q.DatabaseName
	AND [dbo].tbl_IndexList.TableName = Q.TableName
	AND [dbo].tbl_IndexList.IndexName = Q.IndexName
	AND isnull([dbo].tbl_IndexList.DateStop,0) = 0


SELECT @IndexCount = count(*)
FROM [dbo].tbl_IndexList 
WHERE LinkedServer = @LinkedServer
	AND DatabaseName = @Database 
	AND isnull(DateStop,GetDate()) > @RunTime
Print 'Number of indexes to process is: ' + convert(varchar(10),@IndexCount)

SET @Now = GetDate()
/*** Process the remaining Indexes one at the time ***/
WHILE @EndTime > @Now
	BEGIN
		Select top 1 @SchemaName = idl.SchemaName,
			@TableObjectID = idl.TableObjectID,
			@TableName = idl.TableName,
			@IndexObjectID = idl.IndexObjectID,
			@IndexName = idl.IndexName,
			@ProcessStart = idl.ProcessStart
		FROM dbo.tbl_IndexList idl
		LEFT OUTER JOIN @Exceptions exc
			ON idl.TableName LIKE exc.Exception
		WHERE exc.Exception IS NULL
			AND DatabaseName = @Database
			AND isnull(DateStop,GetDate()) > @RunTime
		ORDER BY ProcessStart 


		--print '@ProcessStart = ' + convert(varchar(20),@ProcessStart,120)
		IF @ProcessStart > @RunTime RETURN
		IF isnull(@IndexName,'') = '' return

		Print 'Processing Index ' + @LinkedServer + '.' + @Database + '.' + @SchemaName + '.' + @TableName + '.' + @IndexName

		SET @SQLstring = '
			SELECT @OfflineColumns  = count(*)
			FROM ' + @Database + '.information_schema.COLUMNS 
			WHERE table_name = ''' + @TableName + '''
				AND (data_type in (''text'', ''ntext'', ''image'', ''xml'')
					OR data_type in (''varchar'', ''nvarchar'', ''varbinary'') AND isnull(character_maximum_length,-1) < 0)
		'
		EXECUTE sp_executesql @SQLstring, N'@OfflineColumns int', @OfflineColumns

		BEGIN TRY
			SELECT @FragBefore = max(avg_fragmentation_in_percent)
			FROM sys.dm_db_index_physical_stats ( 
				 DB_ID(@Database)
				,@TableObjectID
				,@IndexObjectID
				,NULL
				,'DETAILED'
				)
		END TRY
		BEGIN CATCH
			UPDATE dbo.tbl_IndexList
			SET DateStop = @RunTime
			WHERE LinkedServer = @LinkedServer
				AND DatabaseName = @Database 
				AND TableObjectID = @TableObjectID 
				AND IndexObjectID = @IndexObjectID

			CONTINUE
		END CATCH

		UPDATE dbo.tbl_IndexList
		SET OfflineColumns = @OfflineColumns, FragBefore = @FragBefore, ProcessStart = GetDate()
		WHERE LinkedServer = @LinkedServer
			AND DatabaseName = @Database 
			AND TableObjectID = @TableObjectID 
			AND IndexObjectID = @IndexObjectID

		IF @InventoryOnly = 0
			BEGIN
				IF @IndexOnline = 1 AND (isnull(@OfflineColumns,0) > 0 OR isnull(@IndexLocks,1) = 0)
					BEGIN
						SET @MessageText = 'This index cannot be defragmented online: '  + @LinkedServer + '.' + @Database + '.' + @SchemaName + '.' + @TableName + '.' + @IndexName
						RAISERROR (@MessageText , 0, 1) WITH NOWAIT
						CONTINUE
					END
				IF @FragBefore > @ReindexLimit OR isnull(@OfflineColumns,0) > 0
					BEGIN
						print 'Rebuilding Index ' + @LinkedServer + '.' + @Database + '.' + @SchemaName + '.' + @TableName + '.' + @IndexName
						BEGIN TRY
							SET @SQLString = 'ALTER INDEX ' + @IndexName + '
							ON ' + @Database + '.' + @SchemaName + '.' + @TableName + '
							REBUILD ' 
							IF @IndexOnline = 1 AND isnull(@OfflineColumns,0) = 0 SET @SQLString = @SQLString + ' WITH (ONLINE = ON) '
							EXECUTE sp_executesql @SQLString
						END TRY
						BEGIN CATCH
							print @@error
						END CATCH

					END
				ELSE IF @FragBefore > @DefragLimit AND @FragBefore < @ReindexLimit AND isnull(@OfflineColumns,0) = 0
					BEGIN
						print 'Defragging Index ' + @LinkedServer + '.' + @Database + '.' + @SchemaName + '.' + @TableName + '.' + @IndexName
						BEGIN TRY
							SET @SQLString = 'ALTER INDEX ' + @IndexName + '
							ON ' + @Database + '.' + @SchemaName + '.' + @TableName + '
							REORGANIZE '
							EXECUTE sp_executesql @SQLString
						END TRY
						BEGIN CATCH
							print @@error
						END CATCH

					END

				SELECT @FragAfter = max(avg_fragmentation_in_percent)
				FROM sys.dm_db_index_physical_stats ( 
					 DB_ID(@Database)
					,@TableObjectID
					,@IndexObjectID
					,NULL
					,'DETAILED'
					)

				UPDATE dbo.tbl_IndexList
				SET FragAfter = @FragAfter, ProcessStop = GetDate()
				WHERE LinkedServer = @LinkedServer
					AND DatabaseName = @Database 
					AND TableObjectID = @TableObjectID 
					AND IndexObjectID = @IndexObjectID
			END
		SET @Now = GetDate()
	END;
--Print 'Finished'
