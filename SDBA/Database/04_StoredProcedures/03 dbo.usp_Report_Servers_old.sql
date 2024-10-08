CREATE PROCEDURE [dbo].[usp_Report_Servers] 
	@Recipient varchar(250) = 'Screen',
	@ExceptionList varchar(250) = NULL,
	@Separator nchar(1) = ',',			-- Separator character for csv
	@MailStats bit = 0,
	@SqlVersion int = 0,				-- The SQL Version to execute this command to (0 = All)
	@IncludeHigherVersions bit = 1		-- Include higher SQL versions than the SQL version given.
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Get information about all existing (or missing) servers
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2014-08-27	BT		Initial version
-- 2.0		2014-11-26	BT		Added MonitorServer check
-- 2.1		2014-12-24	BT		Added MailStats option
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
-- 2.8		2015-02-12	BT		Changed initial LinkedServer selection
-- 3.0		2015-03-16	BT		Fixed some minor bugs excluding old servers
-- 3.2		2015-06-17	BT		Added WindowsVersion
-- ****************************************************************************

-- ******* DEBUG ******************
--	DECLARE @Recipient varchar(250),
--			@ExceptionList varchar(250)

--	SET @Recipient = 'Screen'
--	SET @ExceptionList = NULL
-- ******* END DEBUG ******************

-- ******* DEFAULTS ******************
IF @Recipient IS NULL SET @Recipient = 'Screen'
IF @SqlVersion IS NULL SET @SqlVersion = 0
IF @MailStats IS NULL SET @MailStats = 0
IF @Separator IS NULL SET @Separator = ','
-- ******* END DEFAULTS **************

DECLARE @RunTime smalldatetime
DECLARE @RC int
DECLARE @version int
DECLARE @PrnLine nvarchar(4000)
DECLARE @cmd1 nvarchar(2000)
DECLARE @cmd2 nvarchar(2000)
DECLARE @InstanceName sysname
DECLARE @DataSource nvarchar(100)
DECLARE @PromotionOn bit
DECLARE @linkedServer nvarchar(255)
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))
SET @RunTime = Getdate()
--PRINT @RunTime

IF object_id('tempdb..#Servers') IS NOT NULL DROP TABLE #Servers
CREATE TABLE #Servers(
	[LinkedServer] [nvarchar](255) NOT NULL,
	[LocalServerName] [nvarchar](255) NOT NULL,
	[MachineName] [nvarchar](255) NULL,
	[Domain] [nvarchar](255) NULL,
	[InstanceName] [nvarchar](255) NOT NULL,
	[Edition] [nvarchar](255) NULL,
	[SQLVersionLong] [nvarchar](25) NULL,
	[SPLevel] [nvarchar](15) NULL,
	[Collation] [nvarchar](255) NULL,
	[IsClustered] [bit] NULL,
	[WindowsAuthentication] [bit] NULL,
	[FileStreamEnabled] [bit] NULL,
	[Logical_CPU_Count] [int] NULL,
	[Hyperthread_Ratio] [int] NULL,
	[Physical_CPU_Count] [int] NULL,
	[Physical_Memory_MB] [int] NULL,
	[IP_Address] [nvarchar](15) NULL,
	[Port] [int] NULL)

IF object_id('tempdb..##Msver') IS NOT NULL DROP TABLE ##Msver
CREATE TABLE ##Msver (
	LinkedServer nvarchar(255) NULL, 
	Indexnr int NOT NULL, 
	Name nvarchar(255) NOT NULL, 
	Internal_Value bigint NULL, 
	Character_Value nvarchar(255) NULL)

SET @cmd2 = 'xp_msver'
EXEC usp_Enum_Servers 'S','master',@cmd2,'##Msver(indexnr,Name,Internal_Value,Character_Value)',1,@ExceptionList,@Separator,@SqlVersion,@IncludeHigherVersions


IF (SELECT CURSOR_STATUS('global','srv_cursor')) >= -1
	BEGIN
		IF (SELECT CURSOR_STATUS('global','srv_cursor')) > -1
			BEGIN
				CLOSE srv_cursor
			END
		DEALLOCATE srv_cursor
	END

DECLARE srv_cursor CURSOR FOR
	SELECT [name] COLLATE DATABASE_DEFAULT as [Name],[data_source],[is_remote_proc_transaction_promotion_enabled]
	FROM   sys.servers 
	WHERE  is_linked=1 
		--AND  product LIKE 'Sql_Server'
		AND [name] COLLATE DATABASE_DEFAULT NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
		AND [name] COLLATE DATABASE_DEFAULT NOT IN (SELECT LinkedServer FROM dbo.tbl_Servers WHERE (MonitorServer = 0 OR Active = 0))
	UNION SELECT @LocalServer COLLATE DATABASE_DEFAULT as [Name],REPLACE(@LocalServer,'\', '.' + DEFAULT_DOMAIN() + '\') as data_source,0 as PromotionOn
			WHERE @LocalServer COLLATE DATABASE_DEFAULT NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
			  AND @LocalServer COLLATE DATABASE_DEFAULT NOT IN (SELECT LinkedServer FROM dbo.tbl_Servers WHERE (MonitorServer = 0 OR Active = 0))
	ORDER BY [name] ASC

UPDATE [tbl_Servers] SET DateStop = @RunTime
WHERE LinkedServer NOT IN (
		SELECT [name] COLLATE DATABASE_DEFAULT as [Name]
		FROM   sys.servers 
		WHERE  is_linked=1)
	AND LinkedServer <> @LocalServer
	AND DateStop IS NULL

OPEN srv_cursor
FETCH NEXT FROM srv_cursor into @linkedServer, @DataSource, @PromotionOn
WHILE @@FETCH_STATUS = 0
BEGIN

	print '---------------------------------------------'
	print 'SERVER: '+ @linkedServer
	print '---------------------------------------------'
		EXEC @RC = [dbo].[usp_Test_LinkedServer] @linkedServer, @version OUTPUT
		PRINT 'Stored Procedure: dbo.usp_Test_LinkedServer'
		SELECT @PrnLine = '	Return Code = ' + CONVERT(nvarchar, @RC)
		PRINT @PrnLine
		IF @RC=0 AND (@SqlVersion IN (@version,0) OR (@SqlVersion <= @Version AND @IncludeHigherVersions = 1))
		BEGIN
			IF EXISTS (SELECT LinkedServer FROM dbo.tbl_Servers WHERE LinkedServer = @linkedServer AND DateStop IS NULL)
				BEGIN
					UPDATE dbo.tbl_Servers SET Available = 1, SQLVersion = @version, DataSource = @DataSource, PromotionOn = @PromotionOn, LastFound = @RunTime WHERE LinkedServer = @linkedServer
				END
			ELSE
				BEGIN
					/*** insert missing records into tbl_Servers, using timestamp ***/
					PRINT 'insert missing records into tbl_Servers, using timestamp'
					--INSERT dbo.tbl_Servers (LinkedServer,Active,Available,SQLVersion,DataSource,PromotionOn,DateStart) VALUES (@linkedServer,1,1,@version,@DataSource,@PromotionOn,@RunTime)
					INSERT INTO [dbo].[tbl_servers]([LinkedServer],[DataSource],[PromotionOn],[SQLVersion],[MonitorServer],[MonitorContent],[AutoAdded],[Active],[Available],[LastFound],[DateStart])
						VALUES (@linkedServer,@DataSource,@PromotionOn,@version,1,1,1,1,1,@RunTime,@RunTime)
				END

		--	IF @version = 8 and @linkedServer != SERVERPROPERTY('ServerName')
		--	BEGIN
		--		SET @cmd1 = 'INSERT INTO #Databases SELECT * from openquery(['+@linkedServer+'], ''
		--			SELECT SERVERPROPERTY('ServerName') as Server, name, 
		--			CONVERT(nvarchar(15),DatabasePropertyEx(name,''''Recovery'''')) as Model ,
		--			CONVERT(bit,DatabasePropertyEx(name,''''IsQuotedIdentifiersEnabled'''')) as QuotedID,
		--			CONVERT(nvarchar(15),DatabasePropertyEx(name,''''Status'''')) as DBStatus,
		--			CONVERT(nvarchar(15),DatabasePropertyEx(name,''''UserAccess'''')) as UserAccess,
		--			CONVERT(bit,DatabasePropertyEx(name,''''IsAutoClose'''')) as AutoClose,
		--			CONVERT(bit,DatabasePropertyEx(name,''''IsAutoShrink'''')) as AutoShrink
		--			from master.dbo.sysdatabases'')'
		--	END
			IF @version IN (9,10) and @linkedServer != @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #Servers SELECT * from openquery(['+@linkedServer+'], ''
					SELECT DISTINCT '''''+@linkedServer+''''' AS LinkedServer
						,@@servername as LocalServerName
						,CAST(SERVERPROPERTY(''''MachineName'''') AS nvarchar(255)) AS MachineName
						,CAST(DEFAULT_DOMAIN() AS nvarchar(255)) AS Domain
						,CAST(SERVERPROPERTY(''''ServerName'''') AS nvarchar(255)) AS InstanceName
						,CAST(SERVERPROPERTY(''''Edition'''') AS nvarchar(255)) AS Edition
						,CAST(SERVERPROPERTY(''''ProductVersion'''') AS nvarchar(255)) AS SQLVerionLong
						,CAST(SERVERPROPERTY(''''ProductLevel'''') AS nvarchar(25)) AS SPLevel
						,CAST(SERVERPROPERTY(''''Collation'''') AS nvarchar(255)) AS Collation
						,CAST(SERVERPROPERTY(''''IsClustered'''') AS bit) AS IsClustered
						,CAST(SERVERPROPERTY(''''IsIntegratedSecurityOnly'''') AS bit) AS WindowsAuthentication
						,CAST(COALESCE(SERVERPROPERTY(''''FilestreamEffectiveLevel''''),0) AS bit) AS FileStreamEnabled
						,sinfo.cpu_count AS [Logical_CPU_Count]
						,sinfo.hyperthread_ratio AS Hyperthread_Ratio
						,sinfo.cpu_count/hyperthread_ratio AS Physical_CPU_Count
						,sinfo.physical_memory_in_bytes/(1024*1024) AS Physical_Memory_in_MB
						,CAST(sconn.local_net_address AS nvarchar(15)) AS IP_Address
						,sconn.local_tcp_port AS Port
					FROM sys.dm_os_sys_info sinfo
					LEFT OUTER JOIN sys.dm_exec_connections sconn
						ON sconn.local_tcp_port IS NOT NULL
						AND CAST(COALESCE(sconn.local_net_address,''''0'''') AS nvarchar(15)) NOT LIKE ''''10.18.%''''
						AND CAST(COALESCE(sconn.local_net_address,''''0'''') AS nvarchar(15)) NOT LIKE ''''127.0.0.1%'''''')'
			END

			IF @version IN (11,12) and @linkedServer != @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #Servers SELECT * from openquery(['+@linkedServer+'], ''
					SELECT DISTINCT '''''+@linkedServer+''''' AS LinkedServer
						,@@servername as LocalServerName
						,CAST(SERVERPROPERTY(''''MachineName'''') AS nvarchar(255)) AS MachineName
						,CAST(DEFAULT_DOMAIN() AS nvarchar(255)) AS Domain
						,CAST(SERVERPROPERTY(''''ServerName'''') AS nvarchar(255)) AS InstanceName
						,CAST(SERVERPROPERTY(''''Edition'''') AS nvarchar(255)) AS Edition
						,CAST(SERVERPROPERTY(''''ProductVersion'''') AS nvarchar(255)) AS SQLVerionLong
						,CAST(SERVERPROPERTY(''''ProductLevel'''') AS nvarchar(25)) AS SPLevel
						,CAST(SERVERPROPERTY(''''Collation'''') AS nvarchar(255)) AS Collation
						,CAST(SERVERPROPERTY(''''IsClustered'''') AS bit) AS IsClustered
						,CAST(SERVERPROPERTY(''''IsIntegratedSecurityOnly'''') AS bit) AS WindowsAuthentication
						,CAST(COALESCE(SERVERPROPERTY(''''FilestreamEffectiveLevel''''),0) AS bit) AS FileStreamEnabled
						,cpu_count AS [Logical_CPU_Count]
						,hyperthread_ratio AS Hyperthread_Ratio
						,cpu_count/hyperthread_ratio AS Physical_CPU_Count
						,physical_memory_kb/1024 AS Physical_Memory_in_MB
						,CAST(sconn.local_net_address AS nvarchar(15)) AS IP_Address
						,sconn.local_tcp_port AS Port
					FROM sys.dm_os_sys_info sinfo
					LEFT OUTER JOIN sys.dm_exec_connections sconn
						ON sconn.local_tcp_port IS NOT NULL
						AND CAST(COALESCE(sconn.local_net_address,''''0'''') AS nvarchar(15)) NOT LIKE ''''10.18.%''''
						AND CAST(COALESCE(sconn.local_net_address,''''0'''') AS nvarchar(15)) NOT LIKE ''''127.0.0.1%'''''')'
			END
			--	IF @version = 8 and @linkedServer = SERVERPROPERTY('ServerName')
		--	BEGIN
		--		SET @cmd1 = 'INSERT INTO #Databases 
		--		SELECT @@servername as Server, name, 
		--			CONVERT(nvarchar(15),DatabasePropertyEx(name,''Recovery'')) as Model ,
		--			CONVERT(bit,DatabasePropertyEx(name,''IsQuotedIdentifiersEnabled'')) as QuotedID,
		--			CONVERT(nvarchar(15),DatabasePropertyEx(name,''Status'')) as DBStatus,
		--			CONVERT(nvarchar(15),DatabasePropertyEx(name,''UserAccess'')) as UserAccess,
		--			CONVERT(bit,DatabasePropertyEx(name,''IsAutoClose'')) as AutoClose,
		--			CONVERT(bit,DatabasePropertyEx(name,''IsAutoShrink'')) as AutoShrink
		--		FROM master.dbo.sysdatabases'
		--	END
			IF @version IN (9,10) and @linkedServer = @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #Servers 
					SELECT DISTINCT '''+@linkedServer+''' AS LinkedServer
						,@@servername as LocalServerName
						,CAST(SERVERPROPERTY(''MachineName'') AS nvarchar(255)) AS MachineName
						,CAST(DEFAULT_DOMAIN() AS nvarchar(255)) AS Domain
						,CAST(SERVERPROPERTY(''ServerName'') AS nvarchar(255)) AS InstanceName
						,CAST(SERVERPROPERTY(''Edition'') AS nvarchar(255)) AS Edition
						,CAST(SERVERPROPERTY(''ProductVersion'') AS nvarchar(255)) AS SQLVerionLong
						,CAST(SERVERPROPERTY(''ProductLevel'') AS nvarchar(25)) AS SPLevel
						,CAST(SERVERPROPERTY(''Collation'') AS nvarchar(255)) AS Collation
						,CAST(SERVERPROPERTY(''IsClustered'') AS bit) AS IsClustered
						,CAST(SERVERPROPERTY(''IsIntegratedSecurityOnly'') AS bit) AS WindowsAuthentication
						,CAST(COALESCE(SERVERPROPERTY(''FilestreamEffectiveLevel''),0) AS bit) AS FileStreamEnabled
						,cpu_count AS [Logical_CPU_Count]
						,hyperthread_ratio AS Hyperthread_Ratio
						,cpu_count/hyperthread_ratio AS Physical_CPU_Count
						,physical_memory_in_bytes/(1024*1024) AS Physical_Memory_in_MB
						,CAST(sconn.local_net_address AS nvarchar(15)) AS IP_Address
						,sconn.local_tcp_port AS Port
					FROM sys.dm_os_sys_info sinfo
					LEFT OUTER JOIN sys.dm_exec_connections sconn
						ON sconn.local_tcp_port IS NOT NULL
						AND CAST(COALESCE(sconn.local_net_address,''0'') AS nvarchar(15)) NOT LIKE ''10.18.%''
						AND CAST(COALESCE(sconn.local_net_address,''0'') AS nvarchar(15)) NOT LIKE ''127.0.0.1%'''
			END

			IF @version IN (11,12) and @linkedServer = @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #Servers 
					SELECT DISTINCT '''+@linkedServer+''' AS LinkedServer
						,@@servername as LocalServerName
						,CAST(SERVERPROPERTY(''MachineName'') AS nvarchar(255)) AS MachineName
						,CAST(DEFAULT_DOMAIN() AS nvarchar(255)) AS Domain
						,CAST(SERVERPROPERTY(''ServerName'') AS nvarchar(255)) AS InstanceName
						,CAST(SERVERPROPERTY(''Edition'') AS nvarchar(255)) AS Edition
						,CAST(SERVERPROPERTY(''ProductVersion'') AS nvarchar(255)) AS SQLVerionLong
						,CAST(SERVERPROPERTY(''ProductLevel'') AS nvarchar(25)) AS SPLevel
						,CAST(SERVERPROPERTY(''Collation'') AS nvarchar(255)) AS Collation
						,CAST(SERVERPROPERTY(''IsClustered'') AS bit) AS IsClustered
						,CAST(SERVERPROPERTY(''IsIntegratedSecurityOnly'') AS bit) AS WindowsAuthentication
						,CAST(COALESCE(SERVERPROPERTY(''FilestreamEffectiveLevel''),0) AS bit) AS FileStreamEnabled
						,cpu_count AS [Logical_CPU_Count]
						,hyperthread_ratio AS Hyperthread_Ratio
						,cpu_count/hyperthread_ratio AS Physical_CPU_Count
						,physical_memory_kb/1024 AS Physical_Memory_in_MB
						,CAST(sconn.local_net_address AS nvarchar(15)) AS IP_Address
						,sconn.local_tcp_port AS Port
					FROM sys.dm_os_sys_info sinfo
					LEFT OUTER JOIN sys.dm_exec_connections sconn
						ON sconn.local_tcp_port IS NOT NULL
						AND CAST(COALESCE(sconn.local_net_address,''0'') AS nvarchar(15)) NOT LIKE ''10.18.%''
						AND CAST(COALESCE(sconn.local_net_address,''0'') AS nvarchar(15)) NOT LIKE ''127.0.0.1%'''
			END

			IF @version IN (9,10,11,12)
			BEGIN
				--PRINT @cmd1
				EXEC sp_executesql @cmd1
			END
		END


		ELSE
		--	odear, server not available........
			BEGIN
				INSERT INTO dbo.tbl_ErrorLogsAll SELECT @linkedServer,Getdate(),'ReportServers: Linked server '+@linkedServer+' NOT available or wrong version'
				SELECT @PrnLine = 'Linked server '+@linkedServer+' NOT available or wrong version'
				print  @PrnLine
				IF EXISTS (SELECT LinkedServer FROM dbo.tbl_Servers WHERE LinkedServer = @linkedServer AND DateStop IS NULL)
					BEGIN
						/*** Close all records in tbl_Servers that do not respond ***/
						UPDATE dbo.tbl_Servers SET Available = 0, PromotionOn = @PromotionOn, DataSource = @DataSource WHERE LinkedServer = @linkedServer
					END
				ELSE
					BEGIN
					INSERT INTO [dbo].[tbl_servers]([LinkedServer],[DataSource],[PromotionOn],[MonitorServer],[MonitorContent],[AutoAdded],[Active],[Available],[DateStart])
						VALUES (@linkedServer,@DataSource,@PromotionOn,1,1,1,1,0,@RunTime)
					END
			END
   	FETCH NEXT FROM srv_cursor into @linkedServer,@DataSource, @PromotionOn
END

CLOSE srv_cursor
DEALLOCATE srv_cursor

--SELECT * FROM #Servers

UPDATE [dbo].[tbl_Servers]
SET [LinkedServer] = COALESCE(#Servers.[LinkedServer],[tbl_Servers].[LinkedServer])
	,[LocalServerName] = COALESCE(#Servers.[LocalServerName],[tbl_Servers].[LocalServerName])
	,[MachineName] = COALESCE(#Servers.[MachineName],[tbl_Servers].[MachineName])
	,[Domain] = COALESCE(#Servers.[Domain],[tbl_Servers].[Domain])
	,[InstanceName] = COALESCE(#Servers.[InstanceName],[tbl_Servers].[InstanceName])
	,[Edition] = COALESCE(#Servers.[Edition],[tbl_Servers].[Edition])
	,[SQLVersionLong] = COALESCE(#Servers.[SQLVersionLong],[tbl_Servers].[SQLVersionLong])
	,[SQLVersionText] = 
		COALESCE(CASE LEFT(#Servers.[SQLVersionLong],4)
			WHEN '8.00' THEN 'SQL Server 2000'
			WHEN '8.0.' THEN 'SQL Server 2000'
			WHEN '9.00' THEN 'SQL Server 2005'
			WHEN '9.0.' THEN 'SQL Server 2005'
			WHEN '10.0' THEN 'SQL Server 2008'
			WHEN '10.5' THEN 'SQL Server 2008 R2'
			WHEN '11.0' THEN 'SQL Server 2012'
			WHEN '12.0' THEN 'SQL Server 2014'
			ELSE NULL
		END,[tbl_Servers].[SQLVersionText],'Unknown')
	,[SPLevel] = COALESCE(#Servers.[SPLevel],[tbl_Servers].[SPLevel])
	,[Collation] = COALESCE(#Servers.[Collation],[tbl_Servers].[Collation])
	,[IsClustered] = COALESCE(#Servers.[IsClustered],[tbl_Servers].[IsClustered])
	,[WindowsAuthentication] = COALESCE(#Servers.[WindowsAuthentication],[tbl_Servers].[WindowsAuthentication])
	,[FileStreamEnabled] = COALESCE(#Servers.[FileStreamEnabled],[tbl_Servers].[FileStreamEnabled])
	,[Logical_CPU_Count] = COALESCE(#Servers.[Logical_CPU_Count],[tbl_Servers].[Logical_CPU_Count])
	,[Hyperthread_Ratio] = COALESCE(#Servers.[Hyperthread_Ratio],[tbl_Servers].[Hyperthread_Ratio])
	,[Physical_CPU_Count] = COALESCE(#Servers.[Physical_CPU_Count],[tbl_Servers].[Physical_CPU_Count])
	,[Physical_Memory_MB] = COALESCE(#Servers.[Physical_Memory_MB],[tbl_Servers].[Physical_Memory_MB])
	,[WindowsVersion] = 
		COALESCE(CASE RTRIM(LEFT(##Msver.[Character_Value],4))
			WHEN '5.0' THEN 'Windows 2000'
			WHEN '5.1' THEN 'Windows XP'
			WHEN '5.2' THEN 'Windows Server 2003 (R2) / XP'
			WHEN '6.0' THEN 'Windows Server 2008 / Vista'
			WHEN '6.1' THEN 'Windows Server 2008 R2 / 7'
			WHEN '6.2' THEN 'Windows Server 2012 / 8'
			WHEN '6.3' THEN 'Windows Server 2012 R2 / 8.1'
			WHEN '10.0' THEN 'Windows Server 2016 / 10'
			ELSE NULL
		END,[tbl_Servers].[WindowsVersion],'Unknown')
	,[IP_Address] = COALESCE(#Servers.[IP_Address],[tbl_Servers].[IP_Address])
	,[Port] = COALESCE(#Servers.[Port],[tbl_Servers].[Port])
FROM #Servers
LEFT OUTER JOIN ##Msver
	ON #Servers.[LinkedServer] = ##Msver.[LinkedServer]
	AND ##Msver.[Name] = 'WindowsVersion'
WHERE [dbo].[tbl_Servers].LinkedServer = #Servers.LinkedServer


/*** Cleanup ***/
PRINT 'Cleanup'
IF object_id('tempdb..#Servers') IS NOT NULL DROP TABLE #Servers
IF object_id('tempdb..#Msver') IS NOT NULL DROP TABLE #Msver

/*** Send an email with all new or updated records ***/
PRINT 'Send an email with all new or updated records'
-- ******* DEBUG ******************
--SELECT * FROM tbl_servers
--WHERE DateStart = @RunTime OR DateStop = @RunTime
--ORDER BY ServerName, DatabaseName, DateStart
-- ******* END DEBUG ******************

declare @count int
declare @mail_qry nvarchar(4000),
		@subject     varchar(200)
	
select @count =  count(*)
	FROM tbl_servers
	WHERE DateStart = @RunTime OR DateStop = @RunTime

IF @count > 0 
	BEGIN
		SET @mail_qry = 'SELECT CONVERT(Varchar(25),[LinkedServer]) AS [LinkedServer]
			  ,[SQLVersion]

			  ,substring([MachineName],0,30) AS [MachineName]
			  ,substring([InstanceName],0,40) AS [InstanceName]
			  ,substring([Edition],0,30) AS [Edition]
			  ,substring([DataSource],0,40) AS [DataSource]
			  ,CONVERT(varchar(19),[DateStart],121) AS [DateStart]
			  ,CONVERT(varchar(19),[DateStop],121) AS [DateStop]
			  ,CONVERT(varchar(5),[Active]) AS [Active]
			  ,CONVERT(varchar(5),[Available]) AS [Available]
		FROM [' + DB_NAME() + '].[dbo].[tbl_servers]			
		WHERE DateStart = '''+CONVERT(nchar(19),@RunTime,121)+''' OR DateStop = '''+CONVERT(nchar(19),@RunTime,121)+''' 
		ORDER BY InstanceName, DateStart'

		set @subject = 'Send by: ' + @LocalServer + '. overview Servers'

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
		set @subject = 'Sent by: ' + @LocalServer + '. overview Servers'

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