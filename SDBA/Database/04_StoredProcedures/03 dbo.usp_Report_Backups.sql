CREATE PROCEDURE [dbo].[usp_Report_Backups]
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
-- Purpose	Get backup information from all databases and email to Recipient
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-08-29	BT		Initial version
-- 1.5		2008-09-18	BT		excluded impossible backups (TRAN when changed from FULL to SIMPLE)
--			2008-09-18	BT		excluded backups without database, older than 7 days
-- 1.6		2008-09-25	BT		Exclude foreign database names, these are the result of manual restore actions from a different server
--								Exclude databases that are not ONLINE
-- 2.0		2011-11-24	BT		Add output to screen option (Recepient = 'Screen')
-- 2.1		2014-12-24	BT		Added MailStats option
-- 2.2		2014.12.29	BT		Backup Type DIFF / FULL / FILE are considered equals.
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
--			2015-02-04	BT		Do not close records for servers that are not found or monitored
-- 3.1		2015-06-10	BT		Added closure for backups that no longer have a Linked Server
-- ****************************************************************************

DECLARE @command nvarchar(4000)
DECLARE @RunTime smalldatetime
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))
SET @RunTime = Getdate()

IF OBJECT_ID('tempdb..##backups') IS NOT NULL DROP TABLE ##backups
create table ##backups (
	[ServerName] [nvarchar](255) NULL,
	[DatabaseNameM] [nvarchar](255) NULL,
	[DatabaseNameB] [nvarchar](255) NULL,
	[RecoveryModel] [nvarchar](15) NULL,
	[BackupType] [nvarchar](8) NULL,
	[LastBackup] [smalldatetime] NULL,
	[Location] [nvarchar](250) NULL
	)

-- Select all database backups from the target server
SET @command = 
'SELECT isnull(y.server_name,CAST(SERVERPROPERTY(''''ServerName'''') AS nvarchar(255))) as ServerName,
	x.name as databaseNameM, y.database_name as DatabaseNameB,
	CAST(DatabasePropertyEx(x.name,''''Recovery'''') AS nvarchar(15)) as RecoveryModel ,
	case y.type 
		when ''''D'''' then ''''FULL BCK''''
		when ''''I'''' then ''''DIFF BCK''''
		when ''''L'''' then ''''TRAN BCK''''
		when ''''F'''' then ''''FILE BCK'''' 
		else ''''UNKNOWN!'''' 
	end as BackupType, 
	y.backup_finish_date as LastBackup,
	y.Bck_Location as Location
FROM master.dbo.sysdatabases x
FULL OUTER JOIN
(SELECT max_bck.server_name,max_bck.database_name,max_bck.backup_finish_date,max_bck.type,
	left(fam.physical_device_name,len(fam.physical_device_name)-patindex(''''%\%'''',reverse(fam.physical_device_name))+1) AS Bck_Location
FROM (select bak.server_name,bak.database_name,max(bak.backup_finish_date) as backup_finish_date,bak.type 
		from msdb.dbo.backupset bak
		group by bak.server_name,bak.database_name,bak.type) max_bck
INNER JOIN msdb.dbo.backupset bak
ON max_bck.backup_finish_date = bak.backup_finish_date 
	and max_bck.type = bak.type
	and max_bck.database_name = bak.database_name
INNER JOIN msdb.dbo.backupmediafamily fam
ON bak.media_set_id = fam.media_set_id
) y
ON x.name = y.database_name
WHERE isnull(x.name,y.database_name) !=''''tempdb''''
	AND (y.server_name IS NULL OR y.server_name = CAST(SERVERPROPERTY(''''ServerName'''') AS nvarchar(255)))
	AND CAST(DatabasePropertyEx(x.name,''''Status'''') AS nvarchar(15)) = ''''ONLINE''''
ORDER BY ServerName,isnull(x.name,y.database_name),type'

-- Execute the command on all servers (excluding the provided list)
EXEC usp_Enum_Servers 'Q','msdb',@command,'##backups',0,@ExceptionList,@Separator,@SqlVersion,@IncludeHigherVersions

--select * from ##backups

/*** Close all records for Linked Servers that no longer exist ***/
Print 'Close all records for Linked Servers that no longer exist'
Update [dbo].[tbl_Backups]
SET DateStop = @RunTime
WHERE [dbo].[tbl_Backups].[ServerName] NOT IN 
				(SELECT [name] COLLATE DATABASE_DEFAULT as [Linkedserver]
					FROM   sys.servers 
					WHERE  [is_linked]=1 
					UNION SELECT @LocalServer as [Linkedserver])
	AND [dbo].[tbl_Backups].[DateStop] IS NULL

/*** First remove impossible backups ***/
PRINT 'Remove impossible backups'
DELETE FROM ##backups
WHERE RecoveryModel = 'SIMPLE' AND BackupType = 'TRAN BCK'

/*** insert missing records into tbl_Backups, using timestamp ***/
PRINT 'insert missing records into tbl_Backups, using timestamp'
INSERT INTO [dbo].tbl_Backups (ServerName,DataBaseName,RecoveryModel,BackupType,LastBackup,Location, DateStart,Comment)
SELECT ServerName,databaseNameM,RecoveryModel,BackupType,LastBackup,Location,@RunTime as DateStart, 'Newly found backup' from ##backups tmp
WHERE NOT EXISTS (SELECT org.* FROM [dbo].tbl_Backups org 
					WHERE org.ServerName = tmp.ServerName
					AND org.databasename = tmp.DatabaseNameM
					AND org.BackupType = tmp.BackupType
					--AND org.Location = tmp.Location
					AND org.DateStop is NULL)
	AND DatabaseNameM IS NOT NULL
	AND DatabaseNameB IS NOT NULL

/*** Close all records in tbl_Backups that do not exist in ##backups ***/
PRINT 'Close all records in tbl_Backups that do not exist in ##backups'
Update [dbo].tbl_Backups
SET DateStop = @RunTime, Comment = 'Backup not Found'
FROM
	(SELECT org.ServerName, org.databasename, org.BackupType, org.Location FROM [dbo].tbl_Backups org
		LEFT OUTER JOIN ##backups tmp
		ON org.ServerName = tmp.ServerName
			AND org.databasename = tmp.databasenameB
			AND org.BackupType = tmp.BackupType
			--AND org.Location = tmp.Location
		WHERE tmp.ServerName IS NULL 
			AND tmp.databasenameB IS NULL
			AND tmp.BackupType IS NULL) Q
WHERE [dbo].tbl_Backups.ServerName = Q.ServerName
	AND [dbo].tbl_Backups.databasename = Q.databasename
	AND [dbo].tbl_Backups.BackupType = Q.BackupType
	--AND [dbo].tbl_Backups.Location = Q.Location
	AND [dbo].tbl_Backups.DateStop IS NULL
	AND [dbo].tbl_Backups.ServerName COLLATE DATABASE_DEFAULT IN (SELECT [InstanceName] FROM [dbo].tbl_Servers WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)

/*** Update LastBackup Date & Time for backups that are OK ***/
PRINT 'Update LastBackup Date & Time for backups that are OK'
UPDATE [dbo].tbl_backups SET [dbo].tbl_backups.LastBackup = bak.LastBackup
	,[dbo].tbl_backups.Location = bak.Location
FROM ##backups bak
WHERE [dbo].tbl_backups.ServerName = bak.ServerName
	AND [dbo].tbl_backups.databasename = bak.DatabaseNameB
	AND [dbo].tbl_backups.RecoveryModel = bak.RecoveryModel
	AND [dbo].tbl_backups.BackupType = bak.BackupType
	--AND [dbo].tbl_backups.Location = bak.Location
	AND bak.LastBackup > @RunTime - 1

/*** Delete all from ##backups that is identical in tbl_Backups and backup not too old***/
PRINT 'Delete all from ##backups that is identical in tbl_Backups'
DELETE  ##backups 
FROM [dbo].tbl_Backups ORG
WHERE ORG.ServerName = ##backups.ServerName
	AND ORG.databasename = ##backups.DatabaseNameB
	AND ORG.RecoveryModel = ##backups.RecoveryModel
	AND (ORG.BackupType = ##backups.BackupType
		OR (ORG.BackupType IN ('DIFF BCK','FULL BCK','FILE BCK') AND ##backups.BackupType IN ('DIFF BCK','FULL BCK','FILE BCK')))
	--AND ORG.Location = ##backups.Location
	AND (##backups.LastBackup > @RunTime - 1 OR ORG.LastBackup > @RunTime - 1)

/*** Close all records in tbl_Backups that still exist in ##backups for they have changed ***/
PRINT 'Close all records in tbl_Backups that still exist in ##backups for they have changed'
Update [dbo].tbl_Backups
SET DateStop = @RunTime, Comment = 'Properties changed'
FROM
	(SELECT org.* FROM [dbo].tbl_Backups org
	INNER JOIN ##backups tmp
		ON org.ServerName = tmp.ServerName
		AND org.DataBaseName = tmp.DataBaseNameB
		AND ORG.BackupType = tmp.BackupType
		--AND ORG.Location = tmp.Location
		AND tmp.LastBackup > @RunTime - 1) Q
WHERE [dbo].tbl_Backups.ServerName = Q.ServerName
	AND [dbo].tbl_Backups.databasename = Q.databasename
	AND [dbo].tbl_Backups.DateStop IS NULL
	AND [dbo].tbl_Backups.BackupType = Q.BackupType
	--AND [dbo].tbl_Backups.Location = Q.Location

/*** insert changed records into tbl_Backups, using timestamp ***/
PRINT 'insert changed records into tbl_Backups, using timestamp'
INSERT INTO [dbo].tbl_Backups (ServerName,DataBaseName,RecoveryModel,BackupType,LastBackup,Location, DateStart, Comment)
SELECT ServerName,databaseNameB,RecoveryModel,BackupType,LastBackup,Location,@RunTime as DateStart, 'Properties changed' from ##backups tmp
WHERE NOT EXISTS (SELECT org.* FROM [dbo].tbl_Backups org 
					WHERE org.ServerName = tmp.ServerName
					AND org.databasename = DatabaseNameB
					AND org.BackupType = tmp.BackupType
					--AND org.Location = tmp.Location
					AND org.DateStop is NULL)
	AND tmp.DatabaseNameB IS NOT NULL
	AND tmp.LastBackup > @RunTime - 1

/*** Delete all from ##backups that have been updated to tbl_Backups ***/
PRINT 'Delete all from ##backups that have been updated to tbl_Backups'
DELETE  ##backups 
FROM [dbo].tbl_Backups ORG
WHERE ORG.ServerName = ##backups.ServerName
	AND ORG.databasename = ##backups.DatabaseNameB
	AND ORG.RecoveryModel = ##backups.RecoveryModel
	AND ORG.BackupType = ##backups.BackupType
	--AND ORG.Location = ##backups.Location
	AND ORG.DateStart = @RunTime

/*** Send an email with all new or updated records ***/
PRINT 'Send an email with all new or updated records'

declare @count1 int
declare @count2 int
declare @mail_qry nvarchar(4000),
		@subject     varchar(200)
	
select @count1 =  count(*)
	FROM [dbo].tbl_Backups
	WHERE DateStart = @RunTime OR DateStop = @RunTime

select @count2 =  count(*)
	FROM ##backups

IF @count1 > 0 OR @Count2 > 0
	BEGIN
		SET @mail_qry = '
			SELECT substring([ServerName],0,30) AS [ServerName]
					,substring([DatabaseName],0,40) AS [DatabaseName]
					,substring([RecoveryModel],0,13) AS [RecoveryModel]
					,substring([BackupType],0,10) AS [BackupType]
					,CONVERT(varchar(19),[DateStart],121) AS [DateStart]
					,CONVERT(varchar(19),[DateStop],121) AS [DateStop]
					,CONVERT(varchar(19),[LastBackup],121) AS [LastBackup]
					,CONVERT(varchar(20),[Location]) AS [Location]
					,CONVERT(varchar(25),[Comment]) AS [Comment]
				FROM [' + DB_NAME() + '].[dbo].[tbl_Backups]			
				WHERE (DateStart = '''+CONVERT(nchar(19),@RunTime,121)+''' OR DateStop = '''+CONVERT(nchar(19),@RunTime,121)+''' )
					AND NOT (RecoveryModel = ''SIMPLE'' AND BackupType = ''TRAN BCK'')
			UNION SELECT substring([ServerName],0,30) AS [ServerName]
					,substring([DatabaseNameM],0,40) AS [DatabaseName]
					,substring([RecoveryModel],0,13) AS [RecoveryModel]
					,substring([BackupType],0,10) AS [BackupType]
					,NULL AS [DateStart]
					,NULL AS [DateStop]
					,CONVERT(varchar(19),[LastBackup],121) AS [LastBackup]
					,CONVERT(varchar(20),[Location]) AS [Location]
					,CASE BackupType 
						WHEN ''UNKNOWN!'' THEN CONVERT(varchar(25),''No backup'')
						ELSE CONVERT(varchar(25),''Backup old'')
					 END  AS [Comment]
				FROM ##backups
				WHERE NOT ([DatabaseNameM] IS NULL AND [RecoveryModel] IS NULL)
			UNION SELECT substring([ServerName],0,30) AS [ServerName]
					,substring([DatabaseNameB],0,40) AS [DatabaseName]
					,substring([RecoveryModel],0,13) AS [RecoveryModel]
					,substring([BackupType],0,10) AS [BackupType]
					,NULL AS [DateStart]
					,NULL AS [DateStop]
					,CONVERT(varchar(19),[LastBackup],121) AS [LastBackup]
					,CONVERT(varchar(20),[Location]) AS [Location]
					,''Backup without database''  AS [Comment]
				FROM ##backups
				WHERE [DatabaseNameM] IS NULL 
					AND [RecoveryModel] IS NULL
					AND [LastBackup] > '''+CONVERT(nchar(19),@RunTime-7,121)+'''
			ORDER BY ServerName, DatabaseName, DateStart' 
		--print @mail_qry

		IF @Recipient = 'Screen'  --Send no mail, just display on screen'
		BEGIN
			EXEC sp_executesql @mail_qry;
		END
		ELSE
		BEGIN
			set @subject = 'Send by: ' + @LocalServer + '. Overview Database Backups'
			EXEC msdb..sp_send_dbmail 
				@recipients = @Recipient,
				@subject = @subject,
				@query_result_width=1000,
				@query=@mail_qry,
				@query_result_header = 1
		END
	END

IF @count1 = 0 AND @Count2 = 0 AND @MailStats = 1
	BEGIN
		IF @Recipient = 'Screen'  --Send no mail, just display on screen'
		BEGIN
			SELECT 'No usefull information to report.'
		END
		ELSE
		BEGIN
			set @subject = 'Sent by: ' + @LocalServer + '. Overview Database Backups'
			EXEC msdb..sp_send_dbmail 
				@recipients = @Recipient,
				@subject = @subject,
				@body='No usefull information to report.'
		END
	END

/*** Cleanup ***/
PRINT 'Cleanup'
DROP TABLE ##backups;
