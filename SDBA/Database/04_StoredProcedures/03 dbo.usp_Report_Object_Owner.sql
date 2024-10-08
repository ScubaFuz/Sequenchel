CREATE PROCEDURE [dbo].[usp_Report_Object_Owner]
	@Recipient varchar(250) = 'Screen',
	@ExceptionList varchar(250) = NULL,	--The Servers NOT to process in a single quoted, comma seperated list
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
-- Procedure to retreive all object owners from all objects in all databases on all linked servers.
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-30	BT		Initial version
-- 1.1		2008-10-16	BT		Replaced Accetable Owners list with server, database, object, start and stop date specific list
-- 2.0		2011-10-21	BT		Added mail-recipient: 'screen' for screen output
-- 2.1		2012-07-24	BT		Replaced master users with Schema
-- 2.2		2014-12-24	BT		Added MailStats option
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
-- 3.1		2015-06-01	BT		Added the command to correct the owner of a database to 'sa' over Linked Server connection.
-- 3.2		2015-06-12	BT		Added a table to store the results in.
-- 3.3		2016-10-29	BT		Allowed for logging of DBNull values for Object Owner
-- ****************************************************************************

DECLARE @linkedServer		nvarchar(255),
		@Database	nvarchar(255),
		@command1	nvarchar(2000),
		@command2	nvarchar(2000),
		@command3	nvarchar(2000),
		@mail_text	nvarchar(4000),
		@subject	varchar(200),
		@Count		INT
DECLARE @RunTime smalldatetime
DECLARE @LocalServer nvarchar(255)

SET @RunTime = Getdate()
PRINT @RunTime

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))

-- Create a temporary table to hold the results
IF OBJECT_ID('tempdb..##ObjOwner') IS NOT NULL DROP TABLE ##ObjOwner
CREATE TABLE ##ObjOwner ([LinkedServer] nvarchar(255),
	[DatabaseName] nvarchar(255) not null,
	[ObjectName] nvarchar(255) not null,
	[Owner] nvarChar(255),
	[Type] nvarchar(20)
	)

-- First get all online databases from all servers with their owners
SET @command1 = 'SELECT	name COLLATE DATABASE_DEFAULT as DatabaseName,
				name COLLATE DATABASE_DEFAULT as ObjectName,
				suser_sname(sid),
				''''Database Owner'''' 
				FROM master.dbo.sysdatabases
				WHERE CAST(DatabasePropertyEx(name,''''Status'''') AS nvarchar(15)) = ''''ONLINE'''''
--print  @command1

-- Execute the command on all servers (excluding the provided list)
EXEC usp_Enum_Servers 'Q','master',@command1,'##ObjOwner(DatabaseName,ObjectName,Owner,Type)',1,@ExceptionList,@Separator,@SqlVersion,@IncludeHigherVersions

-- Next loop through all databases ...
DECLARE list_cursor CURSOR FOR
	SELECT LinkedServer, DatabaseName 
	FROM   ##ObjOwner
	WHERE  DatabaseName = ObjectName
	ORDER BY LinkedServer,DatabaseName ASC
-- 
OPEN list_cursor

FETCH NEXT FROM list_cursor into @linkedServer,@Database

WHILE @@FETCH_STATUS = 0
BEGIN

	-- ...to find all objects and their owners, where the owner is not dbo.
	SET @command2 = '
		SELECT 	''''' + @linkedServer + ''''' AS LinkedServer,
		''''' + @Database + ''''' AS DatabaseName,
		sobj.name COLLATE DATABASE_DEFAULT as ObjectName, 
		ISNULL(ssch.name COLLATE DATABASE_DEFAULT,
			ISNULL(susr.name COLLATE DATABASE_DEFAULT,
				ISNULL(USER_NAME(sobj.uid),''''Undefined''''))) AS owner,
		CASE 
			WHEN ssch.schema_id > 0 THEN ''''Schema''''
			WHEN susr.isapprole = 1
				OR susr.issqlrole = 1 THEN ''''Role''''
			WHEN susr.isntname = 1
				OR susr.isntgroup = 1
				OR susr.isntuser = 1 THEN ''''Domain User or Group''''
			WHEN susr.issqluser = 1 THEN ''''SQL User''''
			ELSE ''''Undefined''''
		END AS [Type]
		FROM ' + QuoteName(@Database) + '.dbo.sysobjects sobj 
		LEFT OUTER JOIN ' + QuoteName(@Database) + '.dbo.sysusers susr ON sobj.uid = susr.uid 
		LEFT OUTER JOIN ' + QuoteName(@Database) + '.sys.schemas ssch ON sobj.[uid] = ssch.[schema_id]
		WHERE  sobj.xtype COLLATE DATABASE_DEFAULT IN (''''TF'''', ''''V'''',''''TR'''',''''U'''',''''P'''',''''FN'''',''''IF'''')
		AND category !=2
		AND (susr.name COLLATE DATABASE_DEFAULT IS NULL
			OR susr.name COLLATE DATABASE_DEFAULT != ''''dbo'''')'

	IF @linkedServer != @LocalServer
		SET @command3 = 'INSERT INTO ##ObjOwner select * from OPENQUERY (' + QUOTENAME(@linkedServer) +','' '+@Command2+' '' )'
	IF @linkedServer = @LocalServer
		SET @command3 = 'INSERT INTO ##ObjOwner ' + REPLACE(@Command2,'''''','''')
	--print @command3	
	BEGIN TRY
		EXEC sp_executesql @command3;
	END TRY
	BEGIN CATCH
		print 'error occured on server: ' + @linkedServer
	END CATCH
FETCH NEXT FROM list_cursor into @linkedServer,@Database
END

CLOSE list_cursor
DEALLOCATE list_cursor


--Clean up the results
DELETE ##ObjOwner
FROM (
select obj.* from ##ObjOwner obj
left outer join [dbo].tbl_accowners acc 
	on obj.LinkedServer like acc.LinkedServer
	and obj.databasename like acc.databasename
	and obj.objectname like acc.objectname
	and obj.owner like acc.owner
	and getdate() between DateStart and isnull(DateStop,getdate()+1)
Where acc.owner is not null
) objTemp
WHERE ##ObjOwner.LinkedServer = objTemp.LinkedServer
	AND ##ObjOwner.DatabaseName = objTemp.DatabaseName
	AND ##ObjOwner.ObjectName = objTemp.ObjectName
	AND ##ObjOwner.Owner = objTemp.Owner



/*** Close all records for Linked Servers that no longer exist ***/
Print 'Close all records for Linked Servers that no longer exist'
Update [dbo].[tbl_ObjectOwners]
SET DateStop = @RunTime
WHERE [dbo].[tbl_ObjectOwners].[Linkedserver] NOT IN 
				(SELECT [name] COLLATE DATABASE_DEFAULT as [Linkedserver]
					FROM   sys.servers 
					WHERE  [is_linked]=1 
					UNION SELECT @LocalServer as [Linkedserver])
	AND [dbo].[tbl_ObjectOwners].[DateStop] IS NULL

/*** insert missing records into [tbl_ObjectOwners], using timestamp ***/
PRINT 'insert missing records into [tbl_ObjectOwners], using timestamp'
INSERT INTO [dbo].[tbl_ObjectOwners] ([LinkedServer],[DatabaseName],[ObjectName],[Owner],[Type],[DateStart])
SELECT [LinkedServer],[DataBaseName],[ObjectName],COALESCE([Owner],'DBNull') AS [Owner],[Type],@RunTime as DateStart from ##ObjOwner tmp
WHERE NOT EXISTS (SELECT org.* FROM dbo.[tbl_ObjectOwners] org 
					WHERE org.[LinkedServer] = tmp.[LinkedServer]
					AND org.[DataBaseName] = tmp.[DataBaseName]
					AND org.[ObjectName] = tmp.[ObjectName]
					AND org.[Owner] = COALESCE(tmp.[Owner],'DBNull')
					AND org.[DateStop] is NULL)

/*** Close all records in [tbl_ObjectOwners] that do not exist in ##ObjOwner ***/
PRINT 'Close all records in [tbl_ObjectOwners] that do not exist in ##ObjOwner'
Update [dbo].[tbl_ObjectOwners]
SET [DateStop] = @RunTime
FROM
	(SELECT org.[LinkedServer],org.[DataBaseName],org.[ObjectName],org.[Owner] FROM [dbo].[tbl_ObjectOwners] org
		LEFT OUTER JOIN ##ObjOwner tmp
		ON org.[LinkedServer] = tmp.[LinkedServer]
			AND org.[DataBaseName] = tmp.[DataBaseName]
			AND org.[ObjectName] = tmp.[ObjectName]
			AND org.[Owner] = COALESCE(tmp.[Owner],'DBNull')
		WHERE tmp.[LinkedServer] IS NULL 
			AND tmp.[DataBaseName] IS NULL) Q
WHERE [dbo].[tbl_ObjectOwners].[LinkedServer] = Q.[LinkedServer]
	AND [dbo].[tbl_ObjectOwners].[DataBaseName] = Q.[DataBaseName]
	AND [dbo].[tbl_ObjectOwners].[ObjectName] = Q.[ObjectName]
	AND [dbo].[tbl_ObjectOwners].[Owner] = COALESCE(Q.[Owner],'DBNull')
	AND [dbo].[tbl_ObjectOwners].[DateStop] IS NULL
	AND [dbo].[tbl_ObjectOwners].[LinkedServer] COLLATE DATABASE_DEFAULT IN (SELECT [LinkedServer] FROM [dbo].[tbl_Servers] WHERE [Available] = 1 AND [Active] = 1 AND [MonitorContent] = 1)

/*** Delete all from ##ObjOwner that is identical in tbl_ObjectOwners ***/
PRINT 'Delete all from ##ObjOwner that is identical in tbl_ObjectOwners'
DELETE  ##ObjOwner 
FROM [dbo].[tbl_ObjectOwners] org
WHERE org.[LinkedServer] = ##ObjOwner.[LinkedServer]
	AND org.[DataBaseName] = ##ObjOwner.[DataBaseName]
	AND org.[ObjectName] = ##ObjOwner.[ObjectName]
	AND org.[Owner] = COALESCE(##ObjOwner.[Owner],'DBNull')
	AND org.[DateStop] IS NULL

/*** Close all records in tbl_ObjectOwners that still exist in ##ObjOwner for they have changed ***/
PRINT 'Close all records in tbl_ObjectOwners that still exist in ##ObjOwner for they have changed'
Update [dbo].[tbl_ObjectOwners]
SET [DateStop] = @RunTime
FROM
	(SELECT org.* FROM [dbo].[tbl_ObjectOwners] org
	INNER JOIN ##ObjOwner tmp
		ON org.[LinkedServer] = tmp.[LinkedServer]
		AND org.[DataBaseName] = tmp.[DataBaseName]
		AND org.[ObjectName] = tmp.[ObjectName]
		AND org.[Owner] = tmp.[Owner]) Q
WHERE [dbo].[tbl_ObjectOwners].[LinkedServer] = Q.[LinkedServer]
	AND [dbo].[tbl_ObjectOwners].[DataBaseName] = Q.[DataBaseName]
	AND [dbo].[tbl_ObjectOwners].[ObjectName] = Q.[ObjectName]
	AND [dbo].[tbl_ObjectOwners].[Owner] = COALESCE(Q.[Owner],'DBNull')
	AND [dbo].[tbl_ObjectOwners].[DateStop] IS NULL

/*** insert changed records into tbl_Databases, using timestamp ***/
PRINT 'insert changed records into tbl_Databases, using timestamp'
INSERT INTO [dbo].[tbl_ObjectOwners] ([LinkedServer],[DatabaseName],[ObjectName],[Owner],[Type],[DateStart])
SELECT [LinkedServer],[DataBaseName],[ObjectName],COALESCE([Owner],'DBNull') AS [Owner],[Type],@RunTime as DateStart from ##ObjOwner tmp
WHERE NOT EXISTS (SELECT org.* FROM dbo.[tbl_ObjectOwners] org 
					WHERE org.[LinkedServer] = tmp.[LinkedServer]
					AND org.[DataBaseName] = tmp.[DataBaseName]
					AND org.[ObjectName] = tmp.[ObjectName]
					AND org.[Owner] = COALESCE(tmp.[Owner],'DBNull')
					AND org.[DateStop] is NULL)


PRINT 'Send an email with all new or updated records'
SELECT @Count = count(*) FROM ##ObjOwner

	IF @count > 0 -- Rows exist. Mail the results
 		BEGIN
			SET @mail_text =  'SELECT substring ([LinkedServer],0,30) as [LinkedServer], 
				substring([DatabaseName],0,30) as [DatabaseName],
				substring([ObjectName],0,50) as [ObjectName],
				substring([Owner],0,40) as [Owner],
				substring([Type],0,25) as [Type],
				CONVERT(varchar(19),[DateStart],121) AS [DateStart],
				CONVERT(varchar(19),[DateStop],121) AS [DateStop],
				CASE WHEN [Type] = ''Database Owner'' THEN ''EXEC (''''USE ['' + [DatabaseName] + '']; EXEC dbo.sp_changedbowner @loginame = N''''''''sa'''''''', @map = false;'''') AT ['' + [LinkedServer] + ''] '' ELSE '''' END AS CorrectCommand
				FROM [dbo].[tbl_ObjectOwners]
				WHERE [DateStart] = '''+CONVERT(nchar(19),@RunTime,121)+''' OR [DateStop] = '''+CONVERT(nchar(19),@RunTime,121)+''' 
				ORDER BY [LinkedServer], [Databasename], [ObjectName]'
			SET @subject = 'Send by: ' + @LocalServer + '. Database objects not owned by dbo'

			IF @Recipient = 'Screen'  --Send no mail, just display on screen'
			BEGIN
				EXEC sp_executesql @mail_text;
			END
			ELSE
			BEGIN
				EXEC msdb..sp_send_dbmail 
					@recipients = @Recipient,
					@subject = @subject,
					@query_result_width=1000,
					@query=@mail_text,
					@query_result_header = 1
			END
		END

	IF @count = 0 AND @MailStats = 1 --No rows exist. Mail that no information is available.
 		BEGIN
			SET @subject = 'Sent by: ' + @LocalServer + '. Database objects not owned by dbo'

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

IF OBJECT_ID('tempdb..##ObjOwner') IS NOT NULL DROP TABLE ##ObjOwner;
