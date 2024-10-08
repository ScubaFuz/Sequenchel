DECLARE @Recipient varchar(250)
DECLARE @ExceptionList varchar(250)
DECLARE @Separator nchar(1)
DECLARE @MonitorErrorLogs bit
DECLARE @MaxArchiveLogs int
DECLARE @MailStats bit
DECLARE @SqlVersion int
DECLARE @IncludeHigherVersions bit
SET @Recipient = '<Recipient>'
SET @ExceptionList = '<ExceptionList>'
SET @Separator = '<Separator>'
SET @MailStats = <MailStats>
SET @MonitorErrorLogs = 1
SET @MaxArchiveLogs = 1
SET @SqlVersion = <SqlVersion>
SET @IncludeHigherVersions = <IncludeHigherSqlVersions>
EXECUTE [dbo].[usp_Report_Errorlogs] 
   @Recipient
  ,@ExceptionList
  ,@Separator
  ,@MonitorErrorLogs
  ,@MaxArchiveLogs
  ,@MailStats
  ,@SqlVersion
  ,@IncludeHigherVersions
;
