DECLARE @LinkedServer sysname
DECLARE @Database sysname
DECLARE @MaxTime int
DECLARE @DefragLimit int
DECLARE @ReindexLimit int
DECLARE @IndexOnline int
DECLARE @InventoryOnly int
DECLARE @Exceptionlist nvarchar(2000)
DECLARE @Separator nchar(1)
SET @ExceptionList = '<ExceptionList>'
SET @Separator = '<Separator>'
SET @LinkedServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))
SET @Database = DB_NAME()
SET @MaxTime = 60
SET @DefragLimit = 5
SET @ReindexLimit = 30
SET @IndexOnline = 0
SET @InventoryOnly = 0
EXECUTE [dbo].[usp_DefragIndexes] 
   @LinkedServer
  ,@Database
  ,@MaxTime
  ,@DefragLimit
  ,@ReindexLimit
  ,@IndexOnline
  ,@InventoryOnly
  ,@Exceptionlist
  ,@Separator
;

