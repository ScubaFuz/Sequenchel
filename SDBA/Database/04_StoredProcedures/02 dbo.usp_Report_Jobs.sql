CREATE PROCEDURE [dbo].[usp_Report_Jobs]
	@Recipient nvarchar(250) = 'Screen',
	@ExceptionList varchar(250) = NULL,
	@Separator nchar(1) = ',',				-- Separator character for csv
	@MailStats bit = 0,
	@SqlVersion int = 0,		-- The SQL Version to execute this command to (0 = All)
	@IncludeHigherVersions bit = 1			-- Include higher SQL versions than the SQL version given.
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Get job information from all servers and email to Recipient
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2015-10-30	BT		Initial version
-- 3.4.0	2015-11-20	BT		Changed order and improved closing old stuff
-- 3.5		2019-05-24	BT		Replaced fixed version (limit) with flexible upper limit
-- ****************************************************************************

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

IF OBJECT_ID('tempdb..##Jobs') IS NOT NULL DROP TABLE ##Jobs
CREATE TABLE ##Jobs(
	[LinkedServer] [nvarchar](255) NOT NULL,
	[JobName] [nvarchar](255) NULL,
	[JobOwner] [nvarchar](255) NULL,
	[TimeRun] [datetime2] NULL,
	[JobStatus] [nvarchar](20) NULL,
	[JobOutcome] [nvarchar](20) NULL
)


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
				IF @version >= 9 and @LinkedServer != @LocalServer
					BEGIN
						SET @cmd1 = 
							'INSERT INTO ##Jobs
							EXEC (''
							USE msdb
							;WITH CTE_MostRecentJobRun AS  
							(  
							-- For each job get the most recent run (this will be the one where Rnk=1)  
							SELECT job_id,run_status,run_date,run_time  
							,RANK() OVER (PARTITION BY job_id ORDER BY run_date DESC,run_time DESC) AS Rnk  
							FROM sysjobhistory  
							WHERE step_id=0  
							)  
							SELECT '''''+@LinkedServer+''''' AS LinkedServer
							,sjs.[name] AS [JobName]
							,sps.[name] AS [JobOwner]
							,DATEADD(S,(mrr.[run_time]/10000)*60*60 /* hours */  
								+((mrr.[run_time] - (mrr.[run_time]/10000) * 10000)/100) * 60 /* mins */  
								+ (mrr.[run_time] - (mrr.[run_time]/100) * 100)  /* secs */,  
								CONVERT(DATETIME,RTRIM(mrr.[run_date]),113)) AS [TimeRun] 
							,CASE sjs.[enabled] WHEN 1 THEN ''''Enabled''''  
									ELSE ''''Disabled''''  
								END AS [JobStatus]
							,CASE WHEN mrr.run_status=0 THEN ''''Failed''''
									WHEN mrr.run_status=1 THEN ''''Succeeded''''
									WHEN mrr.run_status=2 THEN ''''Retry''''
									WHEN mrr.run_status=3 THEN ''''Cancelled''''
									ELSE ''''Unknown''''  
								END AS [JobOutcome]
							FROM CTE_MostRecentJobRun mrr  
							INNER JOIN sysjobs sjs  
								ON mrr.job_id=sjs.job_id  
							INNER JOIN sys.server_principals sps
								ON sjs.owner_sid = sps.sid
							WHERE mrr.[Rnk]=1 ''
							) AT ['+@LinkedServer+']'
					END
				IF @version >= 9 and @LinkedServer = @LocalServer
					BEGIN
						SET @cmd1 = 
							'USE msdb
							;WITH CTE_MostRecentJobRun AS  
							(  
							-- For each job get the most recent run (this will be the one where Rnk=1)  
							SELECT job_id,run_status,run_date,run_time  
							,RANK() OVER (PARTITION BY job_id ORDER BY run_date DESC,run_time DESC) AS Rnk  
							FROM sysjobhistory  
							WHERE step_id=0  
							)  
							INSERT INTO ##Jobs
							SELECT '''+@LinkedServer+''' AS LinkedServer
							,sjs.[name] AS [JobName]
							,sps.[name] AS [JobOwner]
							,DATEADD(S,(mrr.[run_time]/10000)*60*60 /* hours */  
								+((mrr.[run_time] - (mrr.[run_time]/10000) * 10000)/100) * 60 /* mins */  
								+ (mrr.[run_time] - (mrr.[run_time]/100) * 100)  /* secs */,  
								CONVERT(DATETIME,RTRIM(mrr.[run_date]),113)) AS [TimeRun] 
							,CASE sjs.[enabled] WHEN 1 THEN ''Enabled''  
									ELSE ''Disabled''  
								END AS [JobStatus]
							,CASE WHEN mrr.run_status=0 THEN ''Failed''
									WHEN mrr.run_status=1 THEN ''Succeeded''
									WHEN mrr.run_status=2 THEN ''Retry''
									WHEN mrr.run_status=3 THEN ''Cancelled''
									ELSE ''Unknown''  
								END AS [JobOutcome]
							FROM CTE_MostRecentJobRun mrr  
							INNER JOIN sysjobs sjs  
								ON mrr.job_id=sjs.job_id  
							INNER JOIN sys.server_principals sps
								ON sjs.owner_sid = sps.sid
							WHERE mrr.[Rnk]=1 '
					END

				IF @version >= 9
				BEGIN
					--PRINT @cmd1
					EXEC sp_executesql @cmd1
				END

			END
		ELSE
		--	odear, server not available........
			BEGIN
				INSERT INTO dbo.tbl_ErrorLogsAll SELECT @LinkedServer,Getdate(),'ReportJobs: Linked server '+@LinkedServer+' NOT available or wrong version'
				SELECT @PrnLine = 'Linked server '+@LinkedServer+' NOT available or wrong version'
				print  @PrnLine
			END
   	FETCH NEXT FROM srv_cursor into @LinkedServer
END

CLOSE srv_cursor
DEALLOCATE srv_cursor


/*** Close all records for Linked Servers that no longer exist ***/
Print 'Close all records for Linked Servers that no longer exist'
Update [dbo].[tbl_Jobs]
SET DateStop = @RunTime
WHERE [dbo].[tbl_jobs].[LinkedServer] NOT IN 
				(SELECT [name] COLLATE DATABASE_DEFAULT as [Linkedserver]
					FROM   sys.servers 
					WHERE  [is_linked]=1 
					UNION SELECT @LocalServer as [Linkedserver])
	AND [dbo].[tbl_jobs].[DateStop] IS NULL

/*** Close all records in tbl_Jobs that do not exist in ##Jobs ***/
PRINT 'Close all records in tbl_Jobs that do not exist in ##Jobs'
Update [dbo].tbl_Jobs
SET DateStop = @RunTime
FROM
	(SELECT org.[LinkedServer], org.[JobName], org.[JobOwner], org.[JobStatus], org.[JobOutcome] FROM [dbo].tbl_Jobs org
		LEFT OUTER JOIN ##Jobs tmp
		ON org.[LinkedServer] = tmp.[LinkedServer]
			AND org.[JobName] = tmp.[JobName]
			AND org.[JobStatus] = tmp.[JobStatus]
			AND org.[JobOutcome] = tmp.[JobOutcome]
		WHERE tmp.[LinkedServer] IS NULL) Q
WHERE [dbo].tbl_Jobs.[LinkedServer] = Q.[LinkedServer]
	AND [dbo].tbl_Jobs.[JobName] = Q.[JobName]
	AND [dbo].tbl_Jobs.[JobStatus] = Q.[JobStatus]
	AND [dbo].tbl_Jobs.[JobOutcome] = Q.[JobOutcome]
	AND [dbo].tbl_Jobs.[DateStop] IS NULL

/*** Update timestamp on existing records ***/
PRINT 'Update timestamp on existing records'
UPDATE tbl_Jobs
SET TimeRun = tmp.[TimeRun]
FROM 
	(SELECT [LinkedServer]
		,[JobName]
		,[JobOwner]
		,[TimeRun]
		,[JobStatus]
		,[JobOutcome]
	FROM ##Jobs) tmp
WHERE tmp.[LinkedServer] = [dbo].[tbl_Jobs].[LinkedServer]
	AND tmp.[JobName] = [dbo].[tbl_Jobs].[JobName]
	AND tmp.[JobOwner] = [dbo].[tbl_Jobs].[JobOwner]
	AND tmp.[JobStatus] = [dbo].[tbl_Jobs].[JobStatus]
	AND tmp.[JobOutcome] = [dbo].[tbl_Jobs].[JobOutcome]
	AND [dbo].[tbl_Jobs].[DateStop] IS NULL
	
/*** insert missing records into tbl_Jobs, using timestamp ***/
PRINT 'insert missing records into tbl_Jobs, using timestamp'
INSERT INTO [dbo].[tbl_Jobs] ([LinkedServer],[JobName],[JobOwner],[TimeRun],[JobStatus],[JobOutcome],[DateStart])
SELECT [LinkedServer],[JobName],[JobOwner],[TimeRun],[JobStatus],[JobOutcome],@RunTime as DateStart from ##Jobs tmp
WHERE NOT EXISTS (SELECT org.* FROM [dbo].tbl_Jobs org 
					WHERE org.[LinkedServer] = tmp.[LinkedServer]
					AND org.[JobName] = tmp.[JobName]
					AND org.[JobOwner] = tmp.[JobOwner]
					AND org.[JobStatus] = tmp.[JobStatus]
					AND org.[JobOutcome] = tmp.[JobOutcome]
					AND org.DateStop is NULL)


/*** Send an email with all new or updated records ***/
PRINT 'Send an email with all new or updated records'

declare @count1 int
declare @mail_qry nvarchar(4000),
		@subject nvarchar(200),
		@body nvarchar(1000)
	
select @count1 =  count(*)
	FROM [dbo].[tbl_Jobs]
	WHERE DateStart = @RunTime OR DateStop = @RunTime
		OR [JobOutcome] = 'Failed'

IF @count1 > 0 
	BEGIN
		SET @body = N'Overview jobs
		1. Jobs that have changed since last monitoring round
		2. Separation line
		3. Jobs that have failed before and are still failing now
		'
		SET @mail_qry = N'
			SELECT substring([LinkedServer],0,30) AS [LinkedServer]
					,substring([JobName],0,40) AS [JobName]
					,substring([JobOwner],0,40) AS [JobOwner]
					,substring([JobStatus],0,15) AS [JobStatus]
					,CONVERT(varchar(19),[TimeRun],121) AS [TimeRun]
					,substring([JobOutcome],0,15) AS [JobOutcome]
					,CONVERT(varchar(19),[DateStart],121) AS [DateStart]
					,CONVERT(varchar(19),[DateStop],121) AS [DateStop]
				FROM [' + DB_NAME() + '].[dbo].[tbl_Jobs]			
				WHERE [DateStart] = (select MAX(datestart) from [' + DB_NAME() + '].[dbo].[tbl_jobs]) 
					OR [DateStop] = (select MAX(datestart) from [' + DB_NAME() + '].[dbo].[tbl_jobs])
					OR ([JobStatus] = ''Enabled'' AND [JobOutcome] = ''Failed'' AND [DateStop] IS NULL)
				UNION SELECT ''-----------------------------''
							,''---------------------------------------''
							,''---------------------------------------''
							,''---------------''
							,''-------------------''
							,''---------------''
							,(select dateadd(minute,-1,MAX(datestart)) 
								from [' + DB_NAME() + '].[dbo].[tbl_jobs] 
								)
							,NULL
				ORDER BY [DateStop] DESC, [DateStart] DESC, [LinkedServer] ASC, [JobName] ASC'
			--print @mail_qry

		IF @Recipient = 'Screen'  --Send no mail, just display on screen'
		BEGIN
			EXEC sp_executesql @mail_qry;
		END
		ELSE
		BEGIN
			set @subject = N'Send by: ' + @LocalServer + '. Overview Server Jobs'
			EXEC msdb..sp_send_dbmail 
				@recipients = @Recipient,
				@subject = @subject,
				@query_result_width=1000,
				@query=@mail_qry,
				@query_result_header = 1,
				@body = @body
		END
	END

IF @count1 = 0 AND @MailStats = 1
	BEGIN
		IF @Recipient = 'Screen'  --Send no mail, just display on screen'
		BEGIN
			SELECT N'No usefull information to report.'
		END
		ELSE
		BEGIN
			set @subject = N'Sent by: ' + @LocalServer + '. Overview Server Jobs'
			EXEC msdb..sp_send_dbmail 
				@recipients = @Recipient,
				@subject = @subject,
				@body = N'No usefull information to report.'
		END
	END

/*** Cleanup ***/
PRINT 'Cleanup'
DROP TABLE ##Jobs;
