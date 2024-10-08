CREATE PROCEDURE [dbo].[usp_Report_DataSpaces] 
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
-- Purpose	Get free space from databases
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2011-10-26	BT		Initial version
-- 2.0		2014-11-26	BT		Added MonitorContent check.
-- 2.1		2014-12-24	BT		Added MailStats option
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
-- 3.0		2015-03-23	BT		Made the configuration flexible by putting parameters in the configuration table.
-- 3.1		2015-04-15	BT		Added SQL version 12 (2014)
-- 3.1		2015-06-22	BT		Fixed the configuration parameter names
-- 3.5		2019-05-24	BT		Replaced fixed version (limit) with flexible upper limit
-- ****************************************************************************

--*************************
--  DEBUG
--*************************
--	DECLARE @Recipient varchar(250),
--			@ExceptionList varchar(250)

--	SET @Recipient = 'bart@thicor.com'
--	SET @ExceptionList = NULL
--*************************

DECLARE @RunTime smalldatetime
DECLARE @RC int
DECLARE @version int
DECLARE @PrnLine nvarchar(4000)
DECLARE @cmd1 nvarchar(4000)
--DECLARE @cmd2 nvarchar(1000)
DECLARE @LinkedServer sysname
DECLARE @sql nvarchar(2000),
		@select nvarchar(1000)
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))
SET @RunTime = Getdate()
--PRINT @RunTime

IF object_id('tempdb..##TempResult') IS NOT NULL DROP TABLE ##TempResult
CREATE TABLE [##TempResult](
	[LinkedServer] [nvarchar](255) NOT NULL,
	[DatabaseName] [nvarchar](255) NOT NULL,
	[LogicalName] [nvarchar](255) NOT NULL,
	[FileID] [smallint] NULL,
	[File_Size_MB] [decimal](12, 2) NULL,
	[Space_Used_MB] [decimal](12, 2) NULL,
	[Free_Space_MB] [decimal](12, 2) NULL,
	[Free_Space_Prc] [decimal](12, 2) NULL,
	[Growth] [int] NULL,
	[Perc] [bit] NOT NULL,
	[PercGrowth] [decimal](12, 2) NULL,
	[FileName] [nvarchar](255) NOT NULL
	);


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
						SET @cmd1 = 
							'INSERT INTO ##TempResult
							EXEC (''exec sp_MSforeachdb @command1 = ''''
							USE [?];

							select
								'''''''''+@LinkedServer+''''''''' AS LinkedServer,
								db_name() AS DatabaseName,
								a.NAME AS LogicalName,
								a.FILEID,
								[File_Size_MB] = 
									convert(decimal(12,2),round(a.size/128.000,2)),
								[Space_Used_MB] =
									convert(decimal(12,2),round(fileproperty(a.name,''''''''SpaceUsed'''''''')/128.000,2)),
								[Free_Space_MB] =
									convert(decimal(12,2),round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) ,
								[Free_Space_Prc] =
									convert(decimal(12,2),(round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) / (round(a.size/128.000,2))*100) ,
								[Growth] = 
									CASE WHEN a.status & 0x100000 = 0
										THEN convert(decimal(12,0),round(a.Growth/128.000,2))
										ELSE a.Growth
									END,
								CASE WHEN a.status & 0x100000 = 0
									THEN 0
									ELSE 1
									END AS Perc,
								CASE WHEN a.status & 0x100000 = 0
									THEN convert(decimal(12,2),((a.growth/128) / (a.size/128.000)) * 100)
									ELSE a.growth
								END AS PercGrowth,
								a.FileName
							FROM dbo.sysfiles a
							''''''
							) AT [' + @LinkedServer + ']'
					END
				IF @version >= 9 and @LinkedServer != @LocalServer
					BEGIN
						SET @cmd1 = 
							'INSERT INTO ##TempResult
							EXEC (''exec sp_MSforeachdb @command1 = ''''
							USE [?];

							select
								'''''''''+@LinkedServer+''''''''' AS LinkedServer,
								db_name() AS DatabaseName,
								a.NAME AS LogicalName,
								a.FILEID,
								[File_Size_MB] = 
									convert(decimal(12,2),round(a.size/128.000,2)),
								[Space_Used_MB] =
									convert(decimal(12,2),round(fileproperty(a.name,''''''''SpaceUsed'''''''')/128.000,2)),
								[Free_Space_MB] =
									convert(decimal(12,2),round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) ,
								[Free_Space_Prc] =
									convert(decimal(12,2),(round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) / (round(a.size/128.000,2))*100) ,
								CASE WHEN d.is_percent_growth=0
									THEN d.growth/128
									ELSE d.growth
								END AS Growth,
								d.is_percent_growth AS PERC,
								CASE WHEN d.is_percent_growth=0
									THEN convert(decimal(12,2),((d.growth/128) / (round(a.size/128.000,2))) * 100)
									ELSE d.growth
								END AS PercGrowth,
								a.FILENAME
							FROM sys.sysfiles a
							INNER JOIN sys.database_files d
								ON a.name = d.name
							''''''
							) AT [' + @LinkedServer + ']'
					END
				IF @version = 8 and @LinkedServer = @LocalServer
					BEGIN
						SET @cmd1 = 
							'INSERT INTO ##TempResult
							EXEC (''exec sp_MSforeachdb @command1 = ''''
							USE [?];

							select
								'''''''''+@LinkedServer+''''''''' AS LinkedServer,
								db_name() AS DatabaseName,
								a.NAME AS LogicalName,
								a.FILEID,
								[File_Size_MB] = 
									convert(decimal(12,2),round(a.size/128.000,2)),
								[Space_Used_MB] =
									convert(decimal(12,2),round(fileproperty(a.name,''''''''SpaceUsed'''''''')/128.000,2)),
								[Free_Space_MB] =
									convert(decimal(12,2),round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) ,
								[Free_Space_Prc] =
									convert(decimal(12,2),(round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) / (round(a.size/128.000,2))*100) ,
								[Growth] = 
									CASE WHEN a.status & 0x100000 = 0
										THEN convert(decimal(12,0),round(a.GROWTH/128.000,2))
										ELSE a.growth
									END,
								CASE WHEN a.status & 0x100000 = 0
									THEN 0
									ELSE 1
									END AS Perc,
								CASE WHEN a.status & 0x100000 = 0
									THEN convert(decimal(12,2),((a.growth/128) / (a.size/128.000)) * 100)
									ELSE a.growth
								END AS PercGrowth,
								a.FILENAME
							FROM dbo.sysfiles a
							''''''
							)'
					END
				IF @version >= 9 and @LinkedServer = @LocalServer
					BEGIN
						SET @cmd1 = 
							'INSERT INTO ##TempResult
							EXEC (''exec sp_MSforeachdb @command1 = ''''
							USE [?];

							select
								'''''''''+@LinkedServer+''''''''' AS LinkedServer,
								db_name() AS DatabaseName,
								a.NAME AS LogicalName,
								a.FILEID,
								[File_Size_MB] = 
									convert(decimal(12,2),round(a.size/128.000,2)),
								[Space_Used_MB] =
									convert(decimal(12,2),round(fileproperty(a.name,''''''''SpaceUsed'''''''')/128.000,2)),
								[Free_Space_MB] =
									convert(decimal(12,2),round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) ,
								[Free_Space_Prc] =
									convert(decimal(12,2),(round((a.size-fileproperty(a.name,''''''''SpaceUsed''''''''))/128.000,2)) / (round(a.size/128.000,2))*100) ,
								CASE WHEN d.is_percent_growth=0
									THEN d.growth/128
									ELSE d.growth
								END AS Growth,
								d.is_percent_growth AS PERC,
								CASE WHEN d.is_percent_growth=0
									THEN convert(decimal(12,2),((d.growth/128) / (round(a.size/128.000,2))) * 100)
									ELSE d.growth
								END AS PercGrowth,
								a.FILENAME
							FROM sys.sysfiles a
							INNER JOIN sys.database_files d
								ON a.name = d.name
							''''''
							)'
					END
				--PRINT @cmd1
				IF @version >= 8
				BEGIN
					--PRINT @cmd1
					EXEC sp_executesql @cmd1
				END
			END
		ELSE
		--	odear, server not available........
			BEGIN
				INSERT INTO dbo.tbl_ErrorLogsAll SELECT @LinkedServer,Getdate(),'ReportDataspaces: Linked server '+@LinkedServer+' NOT available or wrong version'
				SELECT @PrnLine = 'Linked server '+@LinkedServer+' NOT available or wrong version'
				print  @PrnLine
			END
   	FETCH NEXT FROM srv_cursor into @LinkedServer
END

CLOSE srv_cursor
DEALLOCATE srv_cursor

--/*** insert records into tbl_DataSpaces, using timestamp ***/
--PRINT 'insert records into tbl_DataSpaces, using timestamp'

INSERT INTO [dbo].[tbl_DataSpaces]
		([LinkedServer]
		,[DatabaseName]
		,[LogicalName]
		,[FileID]
		,[File_Size_MB]
		,[Space_Used_MB]
		,[Free_Space_MB]
		,[Free_Space_Prc]
		,[Growth]
		,[Perc]
		,[PercGrowth]
		,[FileName]
		,[LogDate])
	SELECT
		[LinkedServer],
		[DatabaseName],
		[LogicalName],
		[FileID],
		[File_Size_MB],
		[Space_Used_MB],
		[Free_Space_MB],
		[Free_Space_Prc],
		[Growth],
		[Perc],
		[PercGrowth],
		[FileName],
		@RunTime
    FROM [##TempResult] tmp
--	WHERE NOT EXISTS (SELECT org.* FROM tbl_DataSpaces org 
--						WHERE org.[SERVERNAME] = tmp.[SERVERNAME]
--						AND org.[DATABASENAME] = tmp.[DATABASENAME]
--						AND org.[LOGICALNAME] = tmp.[LOGICALNAME])
--

--/*** Cleanup ***/
--PRINT 'Cleanup'
--DROP TABLE #Databases
--DROP TABLE #ServerList
--
/*** Send an email ***/
PRINT 'Send an email'

declare @count int
declare @mail_qry nvarchar(4000),
		@subject     varchar(200),
		@Message varchar (1000)

DECLARE @LowerLimit int, @UpperLimit int
DECLARE @SmallGrowth int, @MediumGrowth int, @LargeGrowth int, @MinFreeSpace int, @MinPercGrowth int--, @FreeSpaceMin int

SET @LowerLimit = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'LowerLimit')
SET @UpperLimit = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'UpperLimit')
SET @SmallGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'SmallGrowth')
SET @MediumGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'MediumGrowth')
SET @LargeGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'LargeGrowth')
SET @MinFreeSpace = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'MinFreeSpace')
SET @MinPercGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'MinPercGrowth')

IF @LowerLimit IS NULL OR isnumeric(@LowerLimit) = 0 SET @LowerLimit = 5000
IF @UpperLimit IS NULL OR isnumeric(@UpperLimit) = 0 SET @UpperLimit = 10000
IF @SmallGrowth IS NULL OR isnumeric(@SmallGrowth) = 0 SET @SmallGrowth = 100
IF @MediumGrowth IS NULL OR isnumeric(@MediumGrowth) = 0 SET @MediumGrowth = 500
IF @LargeGrowth IS NULL OR isnumeric(@LargeGrowth) = 0 SET @LargeGrowth = 1000
IF @MinFreeSpace IS NULL OR isnumeric(@MinFreeSpace) = 0 SET @MinFreeSpace = 2
IF @MinPercGrowth IS NULL OR isnumeric(@MinPercGrowth) = 0 SET @MinPercGrowth = 10

SELECT @count = count(*) 
FROM [dbo].[tbl_DataSpaces]
WHERE [LogDate] = (Select max([LogDate]) from [tbl_DataSpaces])
	AND (([Free_Space_MB] < @SmallGrowth 
			AND [File_Size_MB] > @UpperLimit)
		OR ([Perc] = 1
			AND [PercGrowth] < @MinPercGrowth
			)
		OR ([Perc] = 0
			AND (([File_Size_MB] < @LowerLimit AND [Growth] < @SmallGrowth)
				OR ([File_Size_MB] BETWEEN @LowerLimit AND  @UpperLimit AND [Growth] < @MediumGrowth)
				OR ([File_Size_MB] > @UpperLimit AND [Growth] < @LargeGrowth)
				)
			)
		)
--print @count

IF @count > 0 
	BEGIN
		SET @mail_qry = '
			SELECT substring([LinkedServer],0,30) AS [LinkedServer]
			,substring([DatabaseName],0,40) AS [DatabaseName]
			,substring([LogicalName],0,40) AS [LogicalName]
			,CONVERT(nvarchar(6),[FileID]) AS [FileID]
			,CONVERT(nvarchar(12),[File_Size_MB]) AS [File_Size_MB]
			,CONVERT(nvarchar(12),[Space_Used_MB]) AS [Space_Used_MB]
			,CONVERT(nvarchar(12),[Free_Space_MB]) AS [Free_Space_MB]
			,CONVERT(nvarchar(12),[Free_Space_Prc]) AS [Free_Space_Prc]
			,CONVERT(nvarchar(12),[Growth]) AS [Growth]
			,CONVERT(nvarchar(12),[Perc]) AS [Perc]
			,CONVERT(nvarchar(12),[PercGrowth]) AS [PercGrowth]
			,substring([FileName],0,50) AS [FileName]
			,CONVERT(nvarchar(20),[LogDate]) AS [LogDate]
            ,' + DB_NAME() + '.dbo.udf_CorrectCommand([LinkedServer],[DatabaseName],[LogicalName],[File_Size_MB],[Free_Space_MB],[Perc],[Growth]) AS CorrectCommand
			FROM [' + DB_NAME() + '].[dbo].[tbl_DataSpaces]
			WHERE [LogDate] = (Select max([LogDate]) from [' + DB_NAME() + '].[dbo].[tbl_DataSpaces])
				AND (
					([Free_Space_MB] < ' + CAST(@SmallGrowth AS nvarchar(6)) + ' 
						AND [File_Size_MB] > ' + CAST(@UpperLimit AS nvarchar(10)) + ')
					OR ([Perc] = 1
						AND [PercGrowth] < ' + CAST(@MinPercGrowth AS nvarchar(6)) + '
						)
					OR ([Perc] = 0
						AND (([File_Size_MB] < ' + CAST(@LowerLimit AS nvarchar(10)) + ' AND [Growth] < ' + CAST(@SmallGrowth AS nvarchar(10)) + ')
							OR ([File_Size_MB] BETWEEN ' + CAST(@LowerLimit AS nvarchar(10)) + ' AND  ' + CAST(@UpperLimit AS nvarchar(10)) + ' AND [Growth] < ' + CAST(@MediumGrowth AS nvarchar(10)) + ')
							OR ([File_Size_MB] > ' + CAST(@UpperLimit AS nvarchar(10)) + ' AND [Growth] < ' + CAST(@LargeGrowth AS nvarchar(10)) + ')
							)
						)
					)'
		--print @mail_qry

		SET @subject = 'Send by: ' + @LocalServer + '. Overview DataSpaces'
		SET @Message='Explanation of the fields

		LinkedServer	Name of the server the drive is on 
		DatabaseName		Name of the Database
		LogicalName	The logical name of the datafile in SQL Server
		FileID	The ID of the fle in SQL Server
		File_Size_MB	The total size of the file in MB on disk
		Space_Used_MB	The amount of MBs used by data in the datafile
		Free_Space_MB	The amount of MBs still free within the datafile
		Free_Space_PRC	The percentage of free space within the datafile
		Growth	The MBs or Percentage of automatic growth for the file
		Perc	Autogrowth in percentage (1) or MBs (0)
		PercGrowth	The actual growth percentage for growth versus filesize
		FileName	The name of the physical file on disk
		Logdate	The date this file & datasize as recorded
		CorrectCommand	The command you need to correct filesize and growth

		Only rows with values that do not comply with the rules are shown.
		If there are no rows, there is no usefull information to report.
		'

		IF @Recipient = 'Screen'  --Send no mail, just display on screen'
			BEGIN
				EXEC sp_executesql @mail_qry;
			END
		ELSE
			BEGIN
				EXEC msdb..sp_send_dbmail 
				@recipients = @Recipient,
				@subject = @subject,
				@body=@Message,
				@attach_query_result_as_file = 1,
				@query_attachment_filename = 'DataSpaces.csv',
				@query_result_separator = '	',
				@query_result_width=1000,
				@query=@mail_qry,
				@query_result_header = 1
			END
	END

IF @count = 0 AND @MailStats = 1
	BEGIN
		SET @subject = 'Sent by: ' + @LocalServer + '. Overview DataSpaces'

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
