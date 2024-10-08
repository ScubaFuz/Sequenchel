CREATE PROCEDURE [dbo].[usp_Report_Errorlogs]
	@Recipient varchar(250) = 'Screen',
	@ExceptionList varchar(250) = NULL,	--The Servers not to process in a single quoted, comma seperated list
	@Separator nchar(1) = ',',			-- Separator character for csv
	@MonitorErrorLogs BIT = 1,			--If this value is 1 then collect new errors. Otherwise only report
	@MaxArchiveLogs int = 1,			-- The maximum (=6) of archived (old) logfiles to crawl. 0 = only current log 
	@MailStats bit = 0,
	@SqlVersion int = 0,				-- The SQL Version to execute this command to (0 = All)
	@IncludeHigherVersions bit = 1			-- Include higher SQL versions than the SQL version given.
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Report all collected errors
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-30	BT		Initial version
-- 2.0		2014-12-24	BT		Added MailStats option
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
-- 3.2		2015-06-19	BT		Disabled the update for the LastErrorMessage. (moved to monitor)
-- ****************************************************************************
SET nocount on
IF @MonitorErrorLogs = 1 EXEC usp_Monitor_Errorlogs @ExceptionList, @Separator, @MaxArchiveLogs, @SqlVersion, @IncludeHigherVersions

declare @count int
declare @mail_qry nvarchar(4000),
		@subject nvarchar(200)
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))

select @count =  count(*)
	FROM [dbo].tbl_ErrorLogsAll a
	WHERE a.logdate > (SELECT DateChange FROM dbo.tbl_Configuration WHERE ConfigName = 'LastLogMessage')

IF @count > 0 
	BEGIN
		SET @mail_qry = N'SELECT LinkedServer,logdate,logtext FROM [' + DB_NAME() + '].dbo.vw_RecentErrors
				ORDER BY LinkedServer,LogDate,fulltext,ord'

		set @subject = N'Send by: ' + @LocalServer + '. Overview errorlogs'

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
				@query_result_header = 0
		END

		--UPDATE dbo.tbl_Configuration set DateChange = (SELECT max(LogDate) FROM dbo.tbl_ErrorLogsAll) WHERE ConfigName = 'LastLogMessage'
	END

IF @count = 0 AND @MailStats = 1
	BEGIN
		set @subject = N'Sent by: ' + @LocalServer + '. Overview errorlogs'
		EXEC msdb..sp_send_dbmail 
			@recipients = @Recipient,
			@subject = @subject,
			@body = N'No usefull information to report.'
	END
;