DECLARE @ExceptionList varchar(250)
DECLARE @Separator nchar(1)
DECLARE @SqlVersion int
DECLARE @IncludeHigherVersions int
SET @ExceptionList = '<ExceptionList>'
SET @Separator = '<Separator>'
SET @SqlVersion = <SqlVersion>
SET @IncludeHigherVersions = <IncludeHigherSqlVersions>
EXECUTE [dbo].[usp_CycleErrorlogs] 
   @Recipient
  ,@ExceptionList
  ,@Separator
  ,@MailStats
  ,@SqlVersion
  ,@IncludeHigherVersions
;
