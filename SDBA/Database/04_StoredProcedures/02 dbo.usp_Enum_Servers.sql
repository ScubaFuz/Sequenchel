CREATE  PROCEDURE [dbo].[usp_Enum_Servers]
	@Type nchar(1) = 'Q',					-- Should be 'S' (Stored Procedure) or 'Q' (Query)
	@Database nvarchar(20) = NULL,			-- The name of the database to perform the action against
	@Command nvarchar(4000) = NULL,			-- The command to execute
	@Table nvarchar(250) = NULL,			-- The table to store the results in ##Temp or ##Temp(col2,Col3)
	@ServerInclude bit = 0,					-- Indicates if the servername must be included in the results table (column ServerName)
	@ExceptionList nvarchar(250) = NULL,	-- The Servers not to process in a single quoted, comma(?) seperated list
	@Separator nchar(1) = ',',				-- Separator character for @ExceptionList
	@SqlVersion int = 0,					-- The SQL Server version to execute the command against. 0 = All; 8 = 2000; 9 = 2005
	@IncludeHigherVersions bit = 1			-- Include higher SQL versions than the SQL version given.

WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Run a query or stored procedure on multuple servers
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-30	BT		Initial version
-- 2.0		2011-10-25	BT		Replaced #serverlist with udf_CsvToVarchar
-- 2.1		2012-07-23	BT		Replaced ='Sql Server' with LIKE 'Sql_Server'
-- 2.5		2014-11-26	BT		Added MonitorContent check.
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- ****************************************************************************

set nocount on
DECLARE @RC int
DECLARE @PrnLine nvarchar(4000)
DECLARE @cmd nvarchar(4000)
DECLARE @cmdTable nvarchar(4000)
DECLARE @linkedServer SYSNAME
DECLARE @Version INT
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))

IF (SELECT CURSOR_STATUS('global','srv_cursor')) >= -1
 BEGIN
  IF (SELECT CURSOR_STATUS('global','srv_cursor')) > -1
   BEGIN
    CLOSE srv_cursor
   END
 DEALLOCATE srv_cursor
END

DECLARE srv_cursor CURSOR FOR
	SELECT [name] COLLATE DATABASE_DEFAULT
	FROM   sys.servers 
	WHERE  is_linked=1 
		--AND [product] LIKE 'Sql_Server'
		AND [name] COLLATE DATABASE_DEFAULT NOT IN (SELECT * FROM [dbo].udf_CsvToVarchar(@ExceptionList,@Separator))
		AND [name] COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM [dbo].[tbl_Servers] WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	UNION SELECT @LocalServer 
	WHERE @LocalServer NOT IN (SELECT * FROM [dbo].udf_CsvToVarchar(@ExceptionList,@Separator))
		AND @LocalServer COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM [dbo].[tbl_Servers] WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	ORDER BY [name] COLLATE DATABASE_DEFAULT ASC

-- 
OPEN srv_cursor

FETCH NEXT FROM srv_cursor into @linkedServer

WHILE @@FETCH_STATUS = 0
BEGIN
	print '---------------------------------------------'
	print CONVERT(nvarchar(20),GETDATE(),120) + ' SERVER: '+ @linkedServer
	print '---------------------------------------------'
	EXEC @RC = [dbo].[usp_Test_LinkedServer] @linkedServer, @Version OUTPUT
	IF @RC=0 AND (@SqlVersion = 0 OR @SqlVersion = @Version OR (@SqlVersion <= @Version AND @IncludeHigherVersions = 1)) 
		begin
			IF @Type = 'Q' AND @linkedServer != @LocalServer
				SET @cmd = ' select * from OPENQUERY (' + QUOTENAME(@linkedServer) +','''+@Command+' '' )'
			IF @Type = 'Q' AND @linkedServer = @LocalServer
				SET @cmd = ' ' + REPLACE(@Command,'''''','''')
			IF @Type = 'S' AND @linkedServer != @LocalServer
				SET @cmd = ' exec '+QUOTENAME(@linkedServer)+'.'+QUOTENAME(@Database)+'.[dbo].'+@Command
			IF @Type = 'S' AND @linkedServer = @LocalServer
				SET @cmd = ' exec '+QUOTENAME(@Database)+'.[dbo].'+@Command
			IF @Type != 'Q' and @type != 'S'
				BEGIN
					Print 'Type is not valid'
					CLOSE srv_cursor
					DEALLOCATE srv_cursor
					RETURN
				END
			IF @table is not null
				BEGIN
					set @cmd = 'insert into ' + @table + @cmd
				END
			BEGIN TRY
				EXEC sp_executesql @cmd
			END TRY
			BEGIN CATCH
				print 'There was an error executing the command. Possibly the server has a wrong version for the command.'
				print @cmd
			END CATCH
			IF @ServerInclude = 1 and @table is not null
				BEGIN
					SET @cmdTable = 'UPDATE '+left(@Table,PATINDEX ('%(%', @Table)-1)+' SET LinkedServer = '''+@linkedServer+''' WHERE LinkedServer IS NULL'
					--PRINT @cmd3
					EXEC sp_executesql @cmdTable
				END
		end
	else
	--	odear, server not available........
	begin
		SELECT @PrnLine = CONVERT(nvarchar(20),GETDATE(),120) + ' Linked server '+@linkedServer+' NOT available or wrong version'
		print  @PrnLine
	end
   	FETCH NEXT FROM srv_cursor into @linkedServer
END

CLOSE srv_cursor
DEALLOCATE srv_cursor;
