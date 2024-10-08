CREATE PROCEDURE [dbo].[usp_Report_Databases] 
	@Recipient varchar(250) = 'Screen',
	@ExceptionList varchar(250) = NULL,
	@Separator nchar(1) = ',',			-- Separator character for csv
	@MailStats bit = 0,
	@SqlVersion int = 0,				-- The SQL Version to execute this command to (0 = All)
	@IncludeHigherVersions bit = 1			-- Include higher SQL versions than the SQL version given.
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Get information about all existing (or missing) databases
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-30	BT		Initial version
-- 2.0		2012-07-23	BT		Replaced #Serverlist with udf_CsvToVarchar
-- 2.1		2014-11-26	BT		Added MonitorContent check.
-- 2.2		2014-12-24	BT		Added MailStats option
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
--			2015-02-04	BT		Do not close records for servers that are not found or monitored
-- 3.1		2015-04-15	BT		Added SQL verion 12
--			2015-06-10	BT		Added closure for databases that no longer have a Linked Server
--			2015-06-11	BT		Fixed monitoring properties that change and then change back
-- 3.2		2015-08-13	BT		Added lots of fields to the monitoring
-- 3.3		2015-10-09	BT		Added Database restore information
-- 3.5		2019-05-24	BT		Replaced fixed version (limit) with flexible upper limit
-- ****************************************************************************

-- ******* DEBUG ******************
--	DECLARE @Recipient varchar(250),
--			@ExceptionList varchar(250)

--	SET @Recipient = 'bart@thicor.com'
--	SET @ExceptionList = NULL
-- ******* END DEBUG ******************

DECLARE @RunTime smalldatetime
DECLARE @RC int
DECLARE @version int
DECLARE @PrnLine nvarchar(4000)
DECLARE @cmd1 nvarchar(4000)
--DECLARE @cmd2 nvarchar(1000)
DECLARE @LinkedServer sysname
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))

SET @RunTime = Getdate()
PRINT @RunTime

IF object_id('tempdb..#Databases') IS NOT NULL DROP TABLE #Databases
CREATE TABLE #Databases(
	[LinkedServer] [nvarchar](255) NULL,
	[DatabaseName] [sysname] NOT NULL,
	[LocalID] [int] NULL,
	[DateCreated] [datetime] NULL,
	[CompatibilityLevel] [tinyint] NULL,
	[Collation] [sysname] NULL,
	[UserAccess] [nvarchar](60) NULL,
	[IsReadOnly] [bit] NULL,
	[AutoClose] [bit] NULL,
	[AutoShrink] [bit] NULL,
	[DBStatus] [nvarchar](60) NULL,
	[IsStandBy] [bit] NULL,
	[SnapshotIsolation] [nvarchar](60) NULL,
	[ReadCommittedSnapshot] [bit] NULL,
	[RecoveryModel] [nvarchar](60) NULL,
	[PageVerify] [nvarchar](60) NULL,
	[AutoCreateStats] [bit] NULL,
	[AutoUpdateStats] [bit] NULL,
	[AutoUpdateStatsAsync] [bit] NULL,
	[QuotedIdentifier] [bit] NULL,
	[FulltextEnabled] [bit] NULL,
	[BrokerEnabled] [bit] NULL,
	[RestoreDate] [datetime],
	[RestoredBy] [nvarchar](128),
	[RestoreType] [nchar](1))

DECLARE srv_cursor CURSOR FOR
	SELECT [name] COLLATE DATABASE_DEFAULT
	FROM   sys.servers 
	WHERE  is_linked=1 
		--AND [product] LIKE 'Sql_Server'
		AND [name] COLLATE DATABASE_DEFAULT NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
		AND [name] COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM [dbo].tbl_Servers WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	UNION SELECT @LocalServer 
	WHERE @LocalServer NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
		AND @LocalServer COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM [dbo].tbl_Servers WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	ORDER BY [name] COLLATE DATABASE_DEFAULT ASC
-- 
OPEN srv_cursor
FETCH NEXT FROM srv_cursor into @LinkedServer
WHILE @@FETCH_STATUS = 0
BEGIN

	print '---------------------------------------------'
	print 'SERVER: '+ @LinkedServer
	print '---------------------------------------------'
		EXEC @RC = [dbo].[usp_Test_LinkedServer] @LinkedServer, @version OUTPUT
		PRINT 'Stored Procedure: dbo.usp_Test_LinkedServer'
		SELECT @PrnLine = '	Return Code = ' + CONVERT(nvarchar, @RC)
		PRINT @PrnLine
		IF @RC=0 AND (@version = @SqlVersion OR @SqlVersion = 0 OR (@SqlVersion <= @Version AND @IncludeHigherVersions = 1))
		BEGIN
			IF @version = 8 and @LinkedServer != @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #Databases SELECT * from openquery(['+@LinkedServer+'], ''
					SELECT '''''+@LinkedServer+''''' as LinkedServer, 
						name AS DatabaseName, 
						NULL AS LocalID,
						NULL AS DateCreated,
						NULL AS CompatibilityLevel,
						NULL AS Collation,
						CONVERT(nvarchar(15),DatabasePropertyEx(name,''''UserAccess'''')) AS UserAccess,
						NULL AS IsReadOnly,
						CONVERT(bit,DatabasePropertyEx(name,''''IsAutoClose'''')) AS AutoClose,
						CONVERT(bit,DatabasePropertyEx(name,''''IsAutoShrink'''')) AS AutoShrink,
						CONVERT(nvarchar(15),DatabasePropertyEx(name,''''Status'''')) AS DBStatus,
						NULL AS IsStandBy,
						NULL AS SnapshotIsolation,
						NULL as ReadCommittedSnapshot,
						CONVERT(nvarchar(15),DatabasePropertyEx(name,''''Recovery'''')) AS RecoveryModel,
						NULL AS PageVerify,
						NULL AS AutoCreateStats,
						NULL AS AutoUpdateStats,
						NULL AS AutoUpdateStatsAsync,
						CONVERT(bit,DatabasePropertyEx(name,''''IsQuotedIdentifiersEnabled'''')) as QuotedIdentifier,
						NULL AS FulltextEnabled,
						NULL AS BrokerEnabled,
						NULL AS RestoreDate,
						NULL AS RestoredBy,
						NULL As RestoreType
					from master.dbo.sysdatabases'')'
			END
			IF @version >= 9 and @LinkedServer != @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #Databases SELECT * from openquery(['+@LinkedServer+'], ''
					WITH LastRestore AS (
					SELECT max([restore_date]) AS restore_date, [destination_database_name] 
					FROM msdb.dbo.[restorehistory] res1
					GROUP BY [destination_database_name]
					)
					SELECT 
						'''''+@LinkedServer+''''' AS LinkedServer, 
						dat.[name] AS DatabaseName,
						dat.[database_id] AS LocalID,
						dat.create_date AS DateCreated,
						dat.[compatibility_level] AS CompatibilityLevel,
						dat.[collation_name] AS Collation,
						dat.[user_access_desc] AS UserAccess,
						dat.[is_read_only] AS IsReadOnly,
						dat.[is_auto_close_on] AS AutoClose,
						dat.[is_auto_shrink_on] AS AutoShrink,
						dat.[state_desc] AS DBStatus,
						dat.[is_in_standby] AS IsStandBy,
						dat.[snapshot_isolation_state_desc] AS SnapshotIsolation,
						dat.[is_read_committed_snapshot_on] AS ReadCommittedSnapshot,
						dat.[recovery_model_desc] AS RecoveryModel,
						dat.[page_verify_option_desc] AS PageVerify,
						dat.[is_auto_create_stats_on] AS AutoCreateStats,
						dat.[is_auto_update_stats_on] AS AutoUpdateStats,
						dat.[is_auto_update_stats_async_on] AS AutoUpdateStatsAsync,
						dat.[is_quoted_identifier_on] AS QuotedIdentifier,
						dat.[is_fulltext_enabled] AS FulltextEnabled,
						dat.[is_broker_enabled] AS BrokerEnabled,
						res.[restore_date] AS RestoreDate,
						res.[user_name] AS RestoredBy,
						res.[restore_type] As RestoreType
					FROM master.sys.databases dat
					LEFT OUTER JOIN LastRestore lst
						ON dat.[name] = lst.[destination_database_name]
					LEFT OUTER JOIN msdb.dbo.[restorehistory] res
						ON lst.[destination_database_name] = res.[destination_database_name]
						AND lst.[restore_date] = res.[restore_date]
					'')'
			END
			IF @version = 8 and @LinkedServer = @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #Databases 
					SELECT '''+@LinkedServer+''' as LinkedServer, 
						name AS DatabaseName, 
						NULL AS LocalID,
						NULL AS DateCreated,
						NULL AS CompatibilityLevel,
						NULL AS Collation,
						CONVERT(nvarchar(15),DatabasePropertyEx(name,''UserAccess'')) AS UserAccess,
						NULL AS IsReadOnly,
						CONVERT(bit,DatabasePropertyEx(name,''IsAutoClose'')) AS AutoClose,
						CONVERT(bit,DatabasePropertyEx(name,''IsAutoShrink'')) AS AutoShrink,
						CONVERT(nvarchar(15),DatabasePropertyEx(name,''Status'')) AS DBStatus,
						NULL AS IsStandBy,
						NULL AS SnapshotIsolation,
						NULL as ReadCommittedSnapshot,
						CONVERT(nvarchar(15),DatabasePropertyEx(name,''Recovery'')) AS RecoveryModel,
						NULL AS PageVerify,
						NULL AS AutoCreateStats,
						NULL AS AutoUpdateStats,
						NULL AS AutoUpdateStatsAsync,
						CONVERT(bit,DatabasePropertyEx(name,''IsQuotedIdentifiersEnabled'')) as QuotedIdentifier,
						NULL AS FulltextEnabled,
						NULL AS BrokerEnabled,
						NULL AS RestoreDate,
						NULL AS RestoredBy,
						NULL As RestoreType
					FROM master.dbo.sysdatabases'
			END
			IF @version >= 9 and @LinkedServer = @LocalServer
			BEGIN
				SET @cmd1 = ' 
					WITH LastRestore AS (
					SELECT max([restore_date]) AS restore_date, [destination_database_name] 
					FROM msdb.dbo.[restorehistory] res1
					GROUP BY [destination_database_name]
					)
					INSERT INTO #Databases
					SELECT 
						'''+@LinkedServer+''' AS LinkedServer, 
						dat.[name] AS DatabaseName,
						dat.[database_id] AS LocalID,
						dat.create_date AS DateCreated,
						dat.[compatibility_level] AS CompatibilityLevel,
						dat.[collation_name] AS Collation,
						dat.[user_access_desc] AS UserAccess,
						dat.[is_read_only] AS IsReadOnly,
						dat.[is_auto_close_on] AS AutoClose,
						dat.[is_auto_shrink_on] AS AutoShrink,
						dat.[state_desc] AS DBStatus,
						dat.[is_in_standby] AS IsStandBy,
						dat.[snapshot_isolation_state_desc] AS SnapshotIsolation,
						dat.[is_read_committed_snapshot_on] AS ReadCommittedSnapshot,
						dat.[recovery_model_desc] AS RecoveryModel,
						dat.[page_verify_option_desc] AS PageVerify,
						dat.[is_auto_create_stats_on] AS AutoCreateStats,
						dat.[is_auto_update_stats_on] AS AutoUpdateStats,
						dat.[is_auto_update_stats_async_on] AS AutoUpdateStatsAsync,
						dat.[is_quoted_identifier_on] AS QuotedIdentifier,
						dat.[is_fulltext_enabled] AS FulltextEnabled,
						dat.[is_broker_enabled] AS BrokerEnabled,
						res.[restore_date] AS RestoreDate,
						res.[user_name] AS RestoredBy,
						res.[restore_type] As RestoreType
					FROM master.sys.databases dat
					LEFT OUTER JOIN LastRestore lst
						ON dat.[name] = lst.[destination_database_name]
					LEFT OUTER JOIN msdb.dbo.[restorehistory] res
						ON lst.[destination_database_name] = res.[destination_database_name]
						AND lst.[restore_date] = res.[restore_date]
					'
			END
			IF @version >= 9
			BEGIN
				PRINT @cmd1
				EXEC sp_executesql @cmd1
			END
		END


		ELSE
		--	odear, server not available........
			BEGIN
				INSERT INTO dbo.tbl_ErrorLogsAll SELECT @LinkedServer,Getdate(),'ReportDatabases: Linked server '+@LinkedServer+' NOT available or wrong version'
				SELECT @PrnLine = 'Linked server '+@LinkedServer+' NOT available or wrong version'
				print  @PrnLine
			END
   	FETCH NEXT FROM srv_cursor into @LinkedServer
END

CLOSE srv_cursor
DEALLOCATE srv_cursor

/*** Close all records for Linked Servers that no longer exist ***/
Print 'Close all records for Linked Servers that no longer exist'
Update [dbo].[tbl_Databases]
SET DateStop = @RunTime
WHERE [dbo].[tbl_Databases].[Linkedserver] NOT IN 
				(SELECT [name] COLLATE DATABASE_DEFAULT as [Linkedserver]
					FROM   sys.servers 
					WHERE  [is_linked]=1 
					UNION SELECT @LocalServer as [Linkedserver])
	AND [dbo].[tbl_Databases].[DateStop] IS NULL

/*** insert missing records into tbl_Databases, using timestamp ***/
PRINT 'insert missing records into tbl_Databases, using timestamp'
INSERT INTO [dbo].[tbl_Databases]
	([LinkedServer],[DatabaseName],[LocalID],[DateCreated],[CompatibilityLevel],[Collation]
	,[UserAccess],[IsReadOnly],[AutoClose],[AutoShrink],[DBStatus],[IsStandBy],[SnapshotIsolation]
	,[ReadCommittedSnapshot],[RecoveryModel],[PageVerify],[AutoCreateStats],[AutoUpdateStats]
	,[AutoUpdateStatsAsync],[QuotedIdentifier],[FulltextEnabled],[BrokerEnabled]
	,[RestoreDate],[RestoredBy],[RestoreType],[DateStart],[DateStop])
SELECT 
	 [LinkedServer],[DatabaseName],[LocalID],[DateCreated],[CompatibilityLevel],[Collation]
	,[UserAccess],[IsReadOnly],[AutoClose],[AutoShrink],[DBStatus],[IsStandBy],[SnapshotIsolation]
	,[ReadCommittedSnapshot],[RecoveryModel],[PageVerify],[AutoCreateStats],[AutoUpdateStats]
	,[AutoUpdateStatsAsync],[QuotedIdentifier],[FulltextEnabled],[BrokerEnabled]
	,[RestoreDate],[RestoredBy],[RestoreType],@RunTime as [DateStart],NULL AS [DateStop]
FROM #Databases tmp
WHERE NOT EXISTS (SELECT org.* FROM dbo.tbl_databases org 
					WHERE org.[LinkedServer] = tmp.[LinkedServer]
					AND org.[DatabaseName] = tmp.[DatabaseName]
					AND org.[DateStop] is NULL)

/*** Close all records in tbl_Databases that do not exist in #Databases ***/
PRINT 'Close all records in tbl_Databases that do not exist in #Databases'
Update dbo.tbl_Databases
SET DateStop = @RunTime
FROM
	(SELECT org.[LinkedServer], org.[DatabaseName] FROM [dbo].[tbl_Databases] org
		LEFT OUTER JOIN #databases tmp
		ON org.[LinkedServer] = tmp.[LinkedServer]
			AND org.[DatabaseName] = tmp.[DatabaseName]
		WHERE tmp.[LinkedServer] IS NULL 
			AND tmp.[DatabaseName] IS NULL) Q
WHERE tbl_Databases.[LinkedServer] = Q.[LinkedServer]
	AND tbl_Databases.[DatabaseName] = Q.[DatabaseName]
	AND tbl_Databases.[DateStop] IS NULL
	AND tbl_Databases.[LinkedServer] COLLATE DATABASE_DEFAULT IN (SELECT [LinkedServer] FROM [dbo].[tbl_Servers] WHERE [Available] = 1 AND [Active] = 1 AND [MonitorContent] = 1)

/*** Update non-monitored fields in case they changed ***/
PRINT 'Update non-monitored fields in case they changed'
UPDATE [dbo].[tbl_Databases]
   SET [LocalID] = #databases.LocalID
      ,[DateCreated] = #databases.DateCreated
      ,[CompatibilityLevel] = #databases.CompatibilityLevel
      ,[Collation] = #databases.Collation
      --,[UserAccess] = #databases.UserAccess
      ,[IsReadOnly] = #databases.IsReadOnly
      --,[AutoClose] = #databases.AutoClose
      --,[AutoShrink] = #databases.AutoShrink
      --,[DBStatus] = #databases.DBStatus
      ,[IsStandBy] = #databases.IsStandBy
      ,[SnapshotIsolation] = #databases.SnapshotIsolation
      ,[ReadCommittedSnapshot] = #databases.ReadCommittedSnapshot
      --,[RecoveryModel] = #databases.RecoveryModel
      ,[PageVerify] = #databases.PageVerify
      ,[AutoCreateStats] = #databases.AutoCreateStats
      ,[AutoUpdateStats] = #databases.AutoUpdateStats
      ,[AutoUpdateStatsAsync] = #databases.AutoUpdateStatsAsync
      --,[QuotedIdentifier] = #databases.QuotedIdentifier
      ,[FulltextEnabled] = #databases.FulltextEnabled
      ,[BrokerEnabled] = #databases.BrokerEnabled
	  ,[RestoreDate] =  #databases.[RestoreDate]
	  ,[RestoredBy] =  #databases.[RestoredBy]
	  ,[RestoreType] =  #databases.[RestoreType]
FROM #databases
WHERE tbl_Databases.[LinkedServer] = #databases.[LinkedServer]
	AND tbl_Databases.[DatabaseName] = #databases.[DatabaseName]
	AND tbl_Databases.RecoveryModel = #databases.RecoveryModel
	AND tbl_Databases.QuotedIdentifier = #databases.QuotedIdentifier
	AND tbl_Databases.DBStatus = #databases.DBStatus
	AND tbl_Databases.UserAccess = #databases.UserAccess
	AND tbl_Databases.AutoClose = #databases.AutoClose
	AND tbl_Databases.Autoshrink = #databases.Autoshrink
	AND tbl_Databases.[DateStop] IS NULL

/*** Delete all from #Databases that is identical in tbl_Databases ***/
PRINT 'Delete all from #Databases that is identical in tbl_Databases'
DELETE  #Databases 
FROM [dbo].tbl_Databases org
WHERE org.Linkedserver = #databases.LinkedServer
	AND org.DatabaseName = #databases.DatabaseName
	AND org.RecoveryModel = #databases.RecoveryModel
	AND org.QuotedIdentifier = #databases.QuotedIdentifier
	AND org.DBStatus = #databases.DBStatus
	AND org.UserAccess = #databases.UserAccess
	AND org.AutoClose = #databases.AutoClose
	AND org.Autoshrink = #databases.Autoshrink
	AND org.DateSTop IS NULL

/*** Close all records in tbl_Databases that still exist in #Databases for they have changed ***/
PRINT 'Close all records in tbl_Databases that still exist in #Databases for they have changed'
Update [dbo].tbl_Databases
SET DateStop = @RunTime
FROM
	(SELECT org.* FROM [dbo].tbl_Databases org
	INNER JOIN #Databases tmp
		ON org.Linkedserver = tmp.Linkedserver
		AND org.DataBaseName = tmp.DataBaseName) Q
WHERE tbl_Databases.Linkedserver = Q.Linkedserver
	AND tbl_Databases.databasename = Q.databasename
	AND tbl_Databases.DateStop IS NULL

/*** insert changed records into tbl_Databases, using timestamp ***/
PRINT 'insert changed records into tbl_Databases, using timestamp'
INSERT INTO [dbo].[tbl_Databases]
	([LinkedServer],[DatabaseName],[LocalID],[DateCreated],[CompatibilityLevel],[Collation]
	,[UserAccess],[IsReadOnly],[AutoClose],[AutoShrink],[DBStatus],[IsStandBy],[SnapshotIsolation]
	,[ReadCommittedSnapshot],[RecoveryModel],[PageVerify],[AutoCreateStats],[AutoUpdateStats]
	,[AutoUpdateStatsAsync],[QuotedIdentifier],[FulltextEnabled],[BrokerEnabled]
	,[RestoreDate],[RestoredBy],[RestoreType],[DateStart],[DateStop])
SELECT 
	 [LinkedServer],[DatabaseName],[LocalID],[DateCreated],[CompatibilityLevel],[Collation]
	,[UserAccess],[IsReadOnly],[AutoClose],[AutoShrink],[DBStatus],[IsStandBy],[SnapshotIsolation]
	,[ReadCommittedSnapshot],[RecoveryModel],[PageVerify],[AutoCreateStats],[AutoUpdateStats]
	,[AutoUpdateStatsAsync],[QuotedIdentifier],[FulltextEnabled],[BrokerEnabled]
	,[RestoreDate],[RestoredBy],[RestoreType],@RunTime as [DateStart],NULL AS [DateStop]
FROM #Databases tmp
WHERE NOT EXISTS (SELECT org.* FROM dbo.tbl_databases org 
					WHERE org.[LinkedServer] = tmp.[LinkedServer]
					AND org.[DatabaseName] = tmp.[DatabaseName]
					AND org.[DateStop] is NULL)

/*** Cleanup ***/
PRINT 'Cleanup'
IF object_id('tempdb..#Databases') IS NOT NULL DROP TABLE #Databases

/*** Send an email with all new or updated records ***/
PRINT 'Send an email with all new or updated records'
-- ******* DEBUG ******************
--SELECT * FROM tbl_databases
--WHERE DateStart = @RunTime OR DateStop = @RunTime
--ORDER BY ServerName, DatabaseName, DateStart
-- ******* END DEBUG ******************

declare @count int
declare @mail_qry nvarchar(4000),
		@subject     varchar(200)
	
select @count =  count(*)
	FROM [dbo].tbl_databases
	WHERE DateStart = @RunTime OR DateStop = @RunTime

IF @count > 0 
	BEGIN
		SET @mail_qry = 'SELECT CONVERT(Varchar(5),[DatabaseID]) AS [ID]
			  ,substring([Linkedserver],0,30) AS [Linkedserver]
			  ,substring([DatabaseName],0,40) AS [DatabaseName]
			  ,substring([RecoveryModel],0,7) AS [RecoveryModel]
			  ,CONVERT(varchar(19),[DateStart],121) AS [DateStart]
			  ,CONVERT(varchar(19),[DateStop],121) AS [DateStop]
			  ,CONVERT(varchar(3),[QuotedIdentifier]) AS [QuotedIdentifier]
			  ,substring([DBStatus],0,7) AS [DBStatus]
			  ,substring([UserAccess],0,8) AS [UserAccess]
			  ,CONVERT(varchar(5),[AutoClose]) AS [AutoClose]
			  ,CONVERT(varchar(5),[AutoShrink]) AS [AutoShrink]
		FROM [' + DB_NAME() + '].[dbo].[tbl_Databases]			
		WHERE DateStart = '''+CONVERT(nchar(19),@RunTime,121)+''' OR DateStop = '''+CONVERT(nchar(19),@RunTime,121)+''' 
		ORDER BY Linkedserver, DatabaseName, DateStart'

		set @subject = 'Send by: ' + @LocalServer + '. Overview Databases'

		IF @Recipient = 'Screen'  --Send no mail, just display on screen'
		BEGIN
			EXEC sp_executesql @mail_qry;
		END
		ELSE
		BEGIN
			EXEC msdb..sp_send_dbmail 
				@recipients = @Recipient,
				@subject = @subject,
				@query_result_width=1000,
				@query=@mail_qry,
				@query_result_header = 1
		END
	END

IF @count = 0 AND @MailStats = 1
	BEGIN
		set @subject = 'Sent by: ' + @LocalServer + '. Overview Databases'

		IF @Recipient = 'Screen'  --Send no mail, just display on screen'
		BEGIN
			SELECT 'No usefull information to report.'
		END
		ELSE
		BEGIN
			EXEC msdb..sp_send_dbmail 
				@recipients = @Recipient,
				@subject = @subject,
				@body='No usefull information to report.'
		END
	END
;