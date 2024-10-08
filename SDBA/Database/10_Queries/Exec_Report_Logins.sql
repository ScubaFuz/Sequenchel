DECLARE @Recipient varchar(250)
DECLARE @ExceptionList varchar(250)
DECLARE @Separator nchar(1)
DECLARE @MailStats bit
DECLARE @SqlVersion int
DECLARE @IncludeHigherVersions int
SET @Recipient = '<Recipient>'
SET @ExceptionList = '<ExceptionList>'
SET @Separator = '<Separator>'
SET @MailStats = <MailStats>
SET @SqlVersion = <SqlVersion>
SET @IncludeHigherVersions = <IncludeHigherSqlVersions>
EXECUTE [dbo].[usp_Report_Logins] 
   @Recipient
  ,@ExceptionList
  ,@Separator
  ,@MailStats
  ,@SqlVersion
  ,@IncludeHigherVersions
;
