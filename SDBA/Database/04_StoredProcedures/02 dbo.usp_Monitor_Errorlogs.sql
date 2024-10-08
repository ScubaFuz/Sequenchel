CREATE PROCEDURE [dbo].[usp_Monitor_Errorlogs] 
	@ExceptionList varchar(250) = NULL,	--The Servers not to process in a single quoted, comma seperated list
	@Separator nchar(1) = ',',			-- Separator character for csv
	@MaxArchiveLogs int = 1,			--The maximum of archived (old) logfiles to crawl. 0 = only current log
	@SqlVersion int = 0,				-- The SQL Version to execute this command to (0 = All)
	@IncludeHigherVersions bit = 1		-- Include higher SQL versions than the SQL version given.
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Get all errors from all SQL Servers except thos in AcceptableErrors table
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-08-29	BT		Initial version
-- 1.1		2011-10-25	BT		Bart Thieme	Replaced #serverlist with udf_CsvToVarchar
-- 1.2		2011-11-23	BT		Added MaxArchiveLogs to limit the (initial) amount of errors
-- 1.3		2012-07-23	BT		Replaced ='Sql Server' with LIKE 'Sql_Server'
-- 2.0		2014-11-26	BT		Added MonitorContent check.
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
-- 3.1		2015-06-12	BT		Added an extra step to remove unwanted error messages in case the filtering was unsuccesfull
-- 3.1		2015-06-19	BT		Added the UPDATE step for LastLogMessage (moved from Error-reporting)
-- 3.5		2019-05-24	BT		Replaced fixed version (limit) with flexible upper limit
-- ****************************************************************************
set nocount on
DECLARE @LastRun datetime
DECLARE @PreviousRun nvarchar(20)
DECLARE @RunTime nvarchar(20)
DECLARE @RC int
DECLARE @version int
DECLARE @PrnLine nvarchar(4000)
DECLARE @cmd1 nvarchar(255)
DECLARE @cmd2 nvarchar(255)
DECLARE @linkedServer sysname
DECLARE @Archive nvarchar(5)
DECLARE @errortext varchar(4000)
DECLARE @rownumber int
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))

SELECT @PreviousRun = CONVERT(nvarchar(20),(SELECT [DateChange] FROM [dbo].[tbl_Configuration] WHERE [ConfigName] = 'lastErrorCheck'),120)
--Print '@PreviousRun = ' + CAST(@PreviousRun as nvarchar(20))
RAISERROR ('PreviousRun = %s',0,1,@PreviousRun) WITH NOWAIT
UPDATE [dbo].[tbl_Configuration] set [DateChange] = (SELECT max(LogDate) FROM [dbo].[tbl_ErrorLogsAll]) WHERE [ConfigName] = 'LastLogMessage'

CREATE TABLE #ErrorLogs2000 (
	RowNumber int IDENTITY (1, 1) NOT NULL ,
	ErrorText varchar (4000) NULL ,
	ContinuationRow bit NULL )

CREATE TABLE #ErrorLogs2005  (
	RowNumber int IDENTITY (1, 1) NOT NULL ,
	logdate datetime ,
	ProcName varchar(10) ,
	LogText varchar(4000) )

CREATE TABLE #ErrorLogAll (LinkedServer nvarchar(255),
                           LogDate datetime,
                           LogText varchar(4000))

CREATE TABLE #Arc (Archive int,
					dd varchar(30), 
					LFS int)

DECLARE srv_cursor CURSOR FOR
	SELECT [name] COLLATE DATABASE_DEFAULT
	FROM   sys.servers 
	WHERE  is_linked=1 
		--AND [product] LIKE 'Sql_Server'
		AND [name] COLLATE DATABASE_DEFAULT NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
		AND [name] COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM [dbo].[tbl_Servers] WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	UNION SELECT @LocalServer 
	WHERE @LocalServer NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
		AND @LocalServer COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM [dbo].[tbl_Servers] WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	ORDER BY [name] COLLATE DATABASE_DEFAULT ASC

OPEN srv_cursor
	FETCH NEXT FROM srv_cursor into @linkedServer
	WHILE @@FETCH_STATUS = 0
	BEGIN
		--print '---------------------------------------------'
		--print CONVERT(nvarchar(20),GETDATE(),120) + ' SERVER: '+ @linkedServer
		--print '---------------------------------------------'
		SET @RunTime = CONVERT(nvarchar(20),GETDATE(),120)
		RAISERROR ('Server: %s  %s.',0,1,@linkedServer,@RunTime) WITH NOWAIT
		
		EXEC @RC = [dbo].[usp_Test_LinkedServer] @linkedServer, @version OUTPUT
		PRINT 'Stored Procedure: dbo.usp_Test_LinkedServer'
		--SELECT @PrnLine = '	Return Code = ' + CONVERT(nvarchar, @RC)
		--PRINT @PrnLine
		RAISERROR ('Return Code = %d.',0,1,@RC) WITH NOWAIT
		IF @RC=0 AND (@version = @SqlVersion OR @SqlVersion = 0 OR (@SqlVersion <= @Version AND @IncludeHigherVersions = 1))
		BEGIN
			SET @cmd1='exec ['+@linkedServer+'].[master].[dbo].[sp_enumerrorlogs]'
			--print @cmd1
			insert into #arc  exec sp_executesql @cmd1
			DECLARE arc_cursor CURSOR FOR
				select Archive 
				from   #arc
				order by Archive asc
			OPEN arc_cursor
			fetch next from arc_cursor into @Archive
			WHILE @@FETCH_STATUS = 0 AND @Archive <= @MaxArchiveLogs
			BEGIN	
				set @cmd2='exec ['+@linkedServer+'].[master].[dbo].[sp_readerrorlog] '+@Archive+', DEFAULT, DEFAULT, DEFAULT'
				--print @cmd2
				IF @version = 8
				BEGIN
					insert into #ErrorLogs2000  exec sp_executesql @cmd2

					SET @errortext = null
					SET @rownumber = null
			
					DECLARE error1_cursor CURSOR FOR
						SELECT errortext, rownumber FROM #ErrorLogs2000
						WHERE substring(ErrorText,34,6000-34) like  'Error:%' or errortext like '%warning:%'
						ORDER BY RowNumber
			
					OPEN error1_cursor
						FETCH NEXT FROM error1_cursor INTO @errortext, @rownumber
						WHILE @@FETCH_STATUS = 0
						BEGIN
							update #ErrorLogs2000 set continuationrow = 1
							where rownumber = @rownumber + 1
		   					FETCH NEXT FROM error1_cursor INTO @errortext, @rownumber
						END
					CLOSE error1_cursor
					DEALLOCATE error1_cursor

					DECLARE error2_cursor CURSOR FOR
						SELECT errortext, rownumber FROM #ErrorLogs2000
						WHERE continuationrow = 1
						ORDER BY RowNumber
			
					OPEN error2_cursor
						FETCH NEXT FROM error2_cursor INTO @errortext, @rownumber
						WHILE @@FETCH_STATUS = 0
						BEGIN
							if isdate(substring(@errortext,1,22)) = 1
								set @errortext = ', '+substring(@errortext,34,6000-34)
							update #ErrorLogs2000 set errortext = errortext + @errortext
							where rownumber = @rownumber - 1
		   					FETCH NEXT FROM error2_cursor INTO @errortext, @rownumber
						END
					CLOSE error2_cursor
					DEALLOCATE error2_cursor

					DELETE #ErrorLogs2000 WHERE continuationrow = 1
					INSERT INTO #ErrorLogAll 
						SELECT @linkedServer
							,substring(ErrorText,1,22)
							,substring(ErrorText,34,4000-34)
						FROM #ErrorLogs2000 
						WHERE isdate(substring(ErrorText,1,22)) = 1
			 		TRUNCATE TABLE #ErrorLogs2000
				END
				IF @version >= 9
				BEGIN
					INSERT INTO #ErrorLogs2005  exec sp_executesql @cmd2

					SET @errortext = null
					SET @rownumber = null

					DECLARE error3_cursor CURSOR FOR
						SELECT LogText, rownumber FROM #ErrorLogs2005
						WHERE LogText like 'Error:%'
						ORDER BY RowNumber
			
					OPEN error3_cursor
						FETCH NEXT FROM error3_cursor INTO @errortext, @rownumber
						WHILE @@FETCH_STATUS = 0
						BEGIN
							UPDATE #ErrorLogs2005 SET LogText = @errortext + ', ' + LogText
							WHERE rownumber = @rownumber + 1
							DELETE #ErrorLogs2005 WHERE rownumber = @rownumber
		   					FETCH NEXT FROM error3_cursor INTO @errortext, @rownumber
						END
					CLOSE error3_cursor
					DEALLOCATE error3_cursor

					INSERT INTO #ErrorLogAll 
						SELECT @linkedServer
							,logdate
							,logtext
						FROM #ErrorLogs2005
			 		TRUNCATE TABLE #ErrorLogs2005
				END

				fetch next from arc_cursor into @Archive
			END
			CLOSE arc_cursor
			DEALLOCATE arc_cursor
			TRUNCATE TABLE #arc
	-------------------------------------------------------------------------------------------------------------------------
		END
		ELSE
		--	odear, server not available........
		BEGIN
			INSERT INTO #ErrorLogAll SELECT @linkedServer,Getdate(),'MonitorErrorlogs: Linked server '+@linkedServer+' NOT available or wrong version'
		END
   		FETCH NEXT FROM srv_cursor into @linkedServer
	END
CLOSE srv_cursor
DEALLOCATE srv_cursor

SELECT @LastRun=Getdate()
/** Insert the errors into the destination table while filtering out the errors we don't want **/
INSERT INTO dbo.tbl_ErrorLogsAll([LinkedServer], LogDate, LogText)
(SELECT distinct a.LinkedServer, a.LogDate, a.LogText 
	FROM #ErrorLogAll a
	WHERE a.LogDate > @PreviousRun
	AND NOT EXISTS
		(SELECT *
			FROM tbl_AccErrors c
			WHERE a.LogText LIKE c.ErrorText 
				AND a.LinkedServer LIKE c.LinkedServer
				AND a.logdate between c.DateStart AND ISNULL(c.DateStop,@LastRun+1)
		)
)

/** Delete the errors from the destination table that we don't want in case the filtering wasn't entirely succesfull **/
DELETE err
--SELECT err.[ErrorID], err.[LogText]
FROM [dbo].[tbl_ErrorLogsAll] err
INNER JOIN [dbo].[tbl_AccErrors] acc
ON err.[LogText] Like acc.ErrorText
	AND err.[LinkedServer] LIKE acc.[LinkedServer]
	AND err.[LogDate] between acc.[DateStart] AND COALESCE(acc.[DateStop],@LastRun+1)
WHERE err.[LogDate] > @PreviousRun


UPDATE dbo.tbl_Configuration SET ConfigValue = 1, DateChange = @LastRun WHERE ConfigName = 'LastErrorCheck'

DROP TABLE #ErrorLogs2000
DROP TABLE #ErrorLogs2005
DROP TABLE #ErrorLogAll
DROP TABLE #Arc;
