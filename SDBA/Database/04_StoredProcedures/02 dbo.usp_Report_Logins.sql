CREATE PROCEDURE [dbo].[usp_Report_Logins] 
	@Recipient varchar(250) = 'Screen',
	@ExceptionList varchar(250) = NULL,
	@Separator nchar(1) = ',',			-- Separator character for csv
	@MailStats bit = 0,
	@SqlVersion int = 0,				-- The SQL Version to execute this command to (0 = All)
	@IncludeHigherVersions bit = 1			-- Include higher SQL versions than the SQL version given.
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Get information about all existing (or missing) Logins
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2015-01-16	BT		Initial version
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
--			2015-01-29	BT		Made serveral fixes
--			2015-02-04	BT		Do not close records for servers that are not found or monitored
-- 3.0		2015-03-30	BT		Corrected enclosing qoutes for some circumstances.
-- 3.1		2015-06-10	BT		Added closure for logins that no longer have a Linked Server
-- 3.5		2019-05-24	BT		Replaced fixed version (limit) with flexible upper limit
-- ****************************************************************************

-- ******* DEBUG ******************
--	DECLARE @Recipient varchar(250),
--			@ExceptionList varchar(250)

--	SET @Recipient = 'Screen'
--	SET @ExceptionList = NULL
-- ******* END DEBUG ******************

DECLARE @RunTime smalldatetime
DECLARE @RC int
DECLARE @version int
DECLARE @PrnLine nvarchar(4000)
DECLARE @cmd1 nvarchar(3000)
DECLARE @cmd2 nvarchar(3000)
DECLARE @LinkedServer sysname
DECLARE @DataSource nvarchar(100)
DECLARE @PromotionOn bit
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))
SET @RunTime = Getdate()
--PRINT @RunTime

IF object_id('tempdb..#UserLogins') IS NOT NULL DROP TABLE #UserLogins
IF object_id('tempdb..#srvLogins') IS NOT NULL DROP TABLE #srvLogins
CREATE TABLE #srvLogins(
	[LinkedServer] [nvarchar](255) NOT NULL,
	[sid] [varbinary](85) NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[DefaultDB] [nvarchar](255) NULL,
	[denylogin] [int] NOT NULL,
	[hasaccess] [int] NOT NULL,
	[isntname] [int] NOT NULL,
	[sysadmin] [int] NULL,
	[securityadmin] [int] NULL,
	[serveradmin] [int] NULL,
	[setupadmin] [int] NULL,
	[processadmin] [int] NULL,
	[diskadmin] [int] NULL,
	[dbcreator] [int] NULL,
	[bulkadmin] [int] NULL,
	[principal_id] [int] NOT NULL,
	[type_desc] [nvarchar](60) NULL,
	[is_disabled] [bit] NULL
)

IF object_id('tempdb..#dbUsers') IS NOT NULL DROP TABLE #dbUsers
CREATE TABLE #dbUsers(
	[LinkedServer] [nvarchar](255) NOT NULL,
	[uid] [int] NOT NULL,
	[sid] [varbinary](85) NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[DatabaseName] [nvarchar](255) NULL,
	[hasdbaccess] [int] NOT NULL,
	[default_schema_name] [nvarchar](255) NULL,
	[RoleName] [nvarchar](255) NOT NULL
)

DECLARE srv_cursor CURSOR FOR
	SELECT [name] COLLATE DATABASE_DEFAULT
	FROM   sys.servers 
	WHERE  is_linked=1 
		--AND [product] LIKE 'Sql_Server'
		AND [name] COLLATE DATABASE_DEFAULT NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
		AND [name] COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM tbl_Servers WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	UNION SELECT @LocalServer 
	WHERE @LocalServer NOT IN (SELECT * FROM dbo.udf_CsvToVarchar(@ExceptionList,@Separator))
		AND @LocalServer COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM tbl_Servers WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)
	ORDER BY [name] COLLATE DATABASE_DEFAULT ASC

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
			IF @version IN (10) and @LinkedServer != @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #srvLogins SELECT * from openquery(['+@LinkedServer+'], ''
					SELECT '''''+@LinkedServer+''''' AS LinkedServer
						,sp.sid
						,sp.name AS UserName
						,sp.default_database_name AS DefaultDB
						,CASE WHEN spm.state_desc=''''DENY'''' THEN 1 ELSE 0 END AS denylogin
						,CASE WHEN spm.state_desc=''''GRANT'''' THEN 1 ELSE 0 END AS hasaccess
						,CASE WHEN sp.type_desc IN (''''WINDOWS_GROUP'''',''''WINDOWS_LOGIN'''') THEN 1 ELSE 0 END AS isntname
						,COALESCE(x.sysadmin, 0) AS sysadmin
						,COALESCE(x.securityadmin, 0) AS securityadmin
						,COALESCE(x.serveradmin, 0) AS serveradmin
						,COALESCE(x.setupadmin, 0) AS setupadmin
						,COALESCE(x.processadmin, 0) AS processadmin
						,COALESCE(x.diskadmin, 0) AS diskadmin
						,COALESCE(x.dbcreator, 0) AS dbcreator
						,COALESCE(x.bulkadmin, 0) AS bulkadmin
						,sp.principal_id
						,sp.type_desc
						,sp.is_disabled
					FROM sys.server_principals sp
					LEFT OUTER JOIN sys.sql_logins sl 
						ON sp.principal_id = sl.principal_id
					LEFT OUTER JOIN sys.server_permissions spm 
						ON sp.principal_id = spm.grantee_principal_id 
						AND spm.type = ''''COSQ''''
					CROSS APPLY (SELECT MAX(CASE WHEN isp.name = ''''sysadmin'''' THEN 1 ELSE 0 END) AS sysadmin
						,MAX(CASE WHEN isp.name = ''''securityadmin'''' THEN 1 ELSE 0 END) AS securityadmin
						,MAX(CASE WHEN isp.name = ''''serveradmin'''' THEN 1 ELSE 0 END) AS serveradmin
						,MAX(CASE WHEN isp.name = ''''setupadmin'''' THEN 1 ELSE 0 END) AS setupadmin
						,MAX(CASE WHEN isp.name = ''''processadmin'''' THEN 1 ELSE 0 END) AS processadmin
						,MAX(CASE WHEN isp.name = ''''diskadmin'''' THEN 1 ELSE 0 END) AS diskadmin
						,MAX(CASE WHEN isp.name = ''''dbcreator'''' THEN 1 ELSE 0 END) AS dbcreator
						,MAX(CASE WHEN isp.name = ''''bulkadmin'''' THEN 1 ELSE 0 END) AS bulkadmin
					FROM sys.server_role_members srm
					INNER JOIN sys.server_principals isp 
						ON srm.role_principal_id = isp.principal_id
					WHERE sp.principal_id = srm.member_principal_id) x
					WHERE sp.type_desc <> ''''SERVER_ROLE''''
						AND sp.[name] NOT LIKE ''''#%''''
						AND sp.[name] NOT LIKE ''''NT %'''' '' )'

				SET @cmd2 = 'INSERT INTO #dbUsers
					EXEC (''EXEC sp_MSforeachdb
					''''USE [?]
					SELECT '''''''''+@LinkedServer+''''''''' AS LinkedServer
						,d.principal_id AS uid
						,d.sid
						,d.[name] AS UserName
						,DB_NAME() AS DatabaseName 
						,CASE WHEN x.state_desc = ''''''''GRANT'''''''' THEN 1 ELSE 0 END AS hasdbaccess
						,d.default_schema_name
						,grp.[name] AS RoleName
					FROM sys.database_principals d
					INNER JOIN sys.database_role_members drm
						ON d.principal_id = drm.member_principal_id
					INNER JOIN sys.database_principals grp
						ON drm.role_principal_id = grp.principal_id
					OUTER APPLY (SELECT dp.state_desc
					FROM sys.database_permissions dp
					WHERE d.principal_id = dp.grantee_principal_id
						AND permission_name = ''''''''CONNECT'''''''') x
					WHERE d.principal_id > ''''''''6''''''''
						AND d.type <> ''''''''R''''''''
						AND d.[name] NOT LIKE ''''''''#%''''''''
					'''' '') AT ['+@LinkedServer+']'

			END
			IF @version >= 11 and @LinkedServer != @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #srvLogins SELECT * from openquery(['+@LinkedServer+'], ''
					SELECT '''''+@LinkedServer+''''' AS LinkedServer
						,sp.sid
						,sp.name AS UserName
						,sp.default_database_name AS DefaultDB
						,CASE WHEN spm.state_desc=''''DENY'''' THEN 1 ELSE 0 END AS denylogin
						,CASE WHEN spm.state_desc=''''GRANT'''' THEN 1 ELSE 0 END AS hasaccess
						,CASE WHEN sp.type_desc IN (''''WINDOWS_GROUP'''',''''WINDOWS_LOGIN'''') THEN 1 ELSE 0 END AS isntname
						,COALESCE(x.sysadmin, 0) AS sysadmin
						,COALESCE(x.securityadmin, 0) AS securityadmin
						,COALESCE(x.serveradmin, 0) AS serveradmin
						,COALESCE(x.setupadmin, 0) AS setupadmin
						,COALESCE(x.processadmin, 0) AS processadmin
						,COALESCE(x.diskadmin, 0) AS diskadmin
						,COALESCE(x.dbcreator, 0) AS dbcreator
						,COALESCE(x.bulkadmin, 0) AS bulkadmin
						,sp.principal_id
						,sp.type_desc
						,sp.is_disabled
					FROM sys.server_principals sp
					LEFT OUTER JOIN sys.sql_logins sl 
						ON sp.principal_id = sl.principal_id
					LEFT OUTER JOIN sys.server_permissions spm 
						ON sp.principal_id = spm.grantee_principal_id 
						AND spm.type = ''''COSQ''''
					CROSS APPLY (SELECT MAX(CASE WHEN isp.name = ''''sysadmin'''' THEN 1 ELSE 0 END) AS sysadmin
						,MAX(CASE WHEN isp.name = ''''securityadmin'''' THEN 1 ELSE 0 END) AS securityadmin
						,MAX(CASE WHEN isp.name = ''''serveradmin'''' THEN 1 ELSE 0 END) AS serveradmin
						,MAX(CASE WHEN isp.name = ''''setupadmin'''' THEN 1 ELSE 0 END) AS setupadmin
						,MAX(CASE WHEN isp.name = ''''processadmin'''' THEN 1 ELSE 0 END) AS processadmin
						,MAX(CASE WHEN isp.name = ''''diskadmin'''' THEN 1 ELSE 0 END) AS diskadmin
						,MAX(CASE WHEN isp.name = ''''dbcreator'''' THEN 1 ELSE 0 END) AS dbcreator
						,MAX(CASE WHEN isp.name = ''''bulkadmin'''' THEN 1 ELSE 0 END) AS bulkadmin
					FROM sys.server_role_members srm
					INNER JOIN sys.server_principals isp 
						ON srm.role_principal_id = isp.principal_id
					WHERE sp.principal_id = srm.member_principal_id) x
					WHERE sp.type_desc <> ''''SERVER_ROLE''''
						AND sp.[name] NOT LIKE ''''#%''''
						AND sp.[name] NOT LIKE ''''NT %'''' '' )'

				SET @cmd2 = 'INSERT INTO #dbUsers
					EXEC (''EXEC sp_MSforeachdb
					''''USE [?]
					SELECT '''''''''+@LinkedServer+''''''''' AS LinkedServer
						,d.principal_id AS uid
						,d.sid
						,d.[name] AS UserName
						,DB_NAME() AS DatabaseName 
						,CASE WHEN x.state_desc = ''''''''GRANT'''''''' THEN 1 ELSE 0 END AS hasdbaccess
						,d.default_schema_name
						,grp.[name] AS RoleName
					FROM sys.database_principals d
					INNER JOIN sys.database_role_members drm
						ON d.principal_id = drm.member_principal_id
					INNER JOIN sys.database_principals grp
						ON drm.role_principal_id = grp.principal_id
					OUTER APPLY (SELECT dp.state_desc
					FROM sys.database_permissions dp
					WHERE d.principal_id = dp.grantee_principal_id
						AND permission_name = ''''''''CONNECT'''''''') x
					WHERE d.principal_id <> ''''''''1''''''''
						AND d.authentication_type_desc <> ''''''''NONE''''''''
						AND d.[name] NOT LIKE ''''''''#%''''''''
					'''' '') AT ['+@LinkedServer+']'

			END
			IF @version IN (10) and @LinkedServer = @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #srvLogins
					SELECT '''+@LinkedServer+''' AS LinkedServer
						,sp.sid
						,sp.name AS UserName
						,sp.default_database_name AS DefaultDB
						,CASE WHEN spm.state_desc=''DENY'' THEN 1 ELSE 0 END AS denylogin
						,CASE WHEN spm.state_desc=''GRANT'' THEN 1 ELSE 0 END AS hasaccess
						,CASE WHEN sp.type_desc IN (''WINDOWS_GROUP'',''WINDOWS_LOGIN'') THEN 1 ELSE 0 END AS isntname
						,COALESCE(x.sysadmin, 0) AS sysadmin
						,COALESCE(x.securityadmin, 0) AS securityadmin
						,COALESCE(x.serveradmin, 0) AS serveradmin
						,COALESCE(x.setupadmin, 0) AS setupadmin
						,COALESCE(x.processadmin, 0) AS processadmin
						,COALESCE(x.diskadmin, 0) AS diskadmin
						,COALESCE(x.dbcreator, 0) AS dbcreator
						,COALESCE(x.bulkadmin, 0) AS bulkadmin
						,sp.principal_id
						,sp.type_desc
						,sp.is_disabled
					FROM sys.server_principals sp
					LEFT OUTER JOIN sys.sql_logins sl 
						ON sp.principal_id = sl.principal_id
					LEFT OUTER JOIN sys.server_permissions spm 
						ON sp.principal_id = spm.grantee_principal_id 
						AND spm.type = ''COSQ''
					CROSS APPLY (SELECT MAX(CASE WHEN isp.name = ''sysadmin'' THEN 1 ELSE 0 END) AS sysadmin
						,MAX(CASE WHEN isp.name = ''securityadmin'' THEN 1 ELSE 0 END) AS securityadmin
						,MAX(CASE WHEN isp.name = ''serveradmin'' THEN 1 ELSE 0 END) AS serveradmin
						,MAX(CASE WHEN isp.name = ''setupadmin'' THEN 1 ELSE 0 END) AS setupadmin
						,MAX(CASE WHEN isp.name = ''processadmin'' THEN 1 ELSE 0 END) AS processadmin
						,MAX(CASE WHEN isp.name = ''diskadmin'' THEN 1 ELSE 0 END) AS diskadmin
						,MAX(CASE WHEN isp.name = ''dbcreator'' THEN 1 ELSE 0 END) AS dbcreator
						,MAX(CASE WHEN isp.name = ''bulkadmin'' THEN 1 ELSE 0 END) AS bulkadmin
					FROM sys.server_role_members srm
					INNER JOIN sys.server_principals isp 
						ON srm.role_principal_id = isp.principal_id
					WHERE sp.principal_id = srm.member_principal_id) x
					WHERE sp.type_desc <> ''SERVER_ROLE''
						AND sp.[name] NOT LIKE ''#%''
						AND sp.[name] NOT LIKE ''NT %'' '

				SET @cmd2 = 'INSERT INTO #dbUsers
					EXEC sp_MSforeachdb
					''USE [?]
					SELECT '''''+@LinkedServer+''''' AS LinkedServer
						,d.principal_id AS uid
						,d.sid
						,d.[name] AS UserName
						,DB_NAME() AS DatabaseName 
						,CASE WHEN x.state_desc = ''''GRANT'''' THEN 1 ELSE 0 END AS hasdbaccess
						,d.default_schema_name
						,grp.[name] AS RoleName
					FROM sys.database_principals d
					INNER JOIN sys.database_role_members drm
						ON d.principal_id = drm.member_principal_id
					INNER JOIN sys.database_principals grp
						ON drm.role_principal_id = grp.principal_id
					OUTER APPLY (SELECT dp.state_desc
					FROM sys.database_permissions dp
					WHERE d.principal_id = dp.grantee_principal_id
						AND permission_name = ''''CONNECT'''') x
					WHERE d.principal_id > ''''6''''
						AND d.type <> ''''R''''
						AND d.[name] NOT LIKE ''''#%'''' '' '
			END
			IF @version >= 11 and @LinkedServer = @LocalServer
			BEGIN
				SET @cmd1 = 'INSERT INTO #srvLogins
					SELECT '''+@LinkedServer+''' AS LinkedServer
						,sp.sid
						,sp.name AS UserName
						,sp.default_database_name AS DefaultDB
						,CASE WHEN spm.state_desc=''DENY'' THEN 1 ELSE 0 END AS denylogin
						,CASE WHEN spm.state_desc=''GRANT'' THEN 1 ELSE 0 END AS hasaccess
						,CASE WHEN sp.type_desc IN (''WINDOWS_GROUP'',''WINDOWS_LOGIN'') THEN 1 ELSE 0 END AS isntname
						,COALESCE(x.sysadmin, 0) AS sysadmin
						,COALESCE(x.securityadmin, 0) AS securityadmin
						,COALESCE(x.serveradmin, 0) AS serveradmin
						,COALESCE(x.setupadmin, 0) AS setupadmin
						,COALESCE(x.processadmin, 0) AS processadmin
						,COALESCE(x.diskadmin, 0) AS diskadmin
						,COALESCE(x.dbcreator, 0) AS dbcreator
						,COALESCE(x.bulkadmin, 0) AS bulkadmin
						,sp.principal_id
						,sp.type_desc
						,sp.is_disabled
					FROM sys.server_principals sp
					LEFT OUTER JOIN sys.sql_logins sl 
						ON sp.principal_id = sl.principal_id
					LEFT OUTER JOIN sys.server_permissions spm 
						ON sp.principal_id = spm.grantee_principal_id 
						AND spm.type = ''COSQ''
					CROSS APPLY (SELECT MAX(CASE WHEN isp.name = ''sysadmin'' THEN 1 ELSE 0 END) AS sysadmin
						,MAX(CASE WHEN isp.name = ''securityadmin'' THEN 1 ELSE 0 END) AS securityadmin
						,MAX(CASE WHEN isp.name = ''serveradmin'' THEN 1 ELSE 0 END) AS serveradmin
						,MAX(CASE WHEN isp.name = ''setupadmin'' THEN 1 ELSE 0 END) AS setupadmin
						,MAX(CASE WHEN isp.name = ''processadmin'' THEN 1 ELSE 0 END) AS processadmin
						,MAX(CASE WHEN isp.name = ''diskadmin'' THEN 1 ELSE 0 END) AS diskadmin
						,MAX(CASE WHEN isp.name = ''dbcreator'' THEN 1 ELSE 0 END) AS dbcreator
						,MAX(CASE WHEN isp.name = ''bulkadmin'' THEN 1 ELSE 0 END) AS bulkadmin
					FROM sys.server_role_members srm
					INNER JOIN sys.server_principals isp 
						ON srm.role_principal_id = isp.principal_id
					WHERE sp.principal_id = srm.member_principal_id) x
					WHERE sp.type_desc <> ''SERVER_ROLE''
						AND sp.[name] NOT LIKE ''#%''
						AND sp.[name] NOT LIKE ''NT %'' '

				SET @cmd2 = 'INSERT INTO #dbUsers
					EXEC sp_MSforeachdb
					''USE [?]
					SELECT '''''+@LinkedServer+''''' AS LinkedServer
						,d.principal_id AS uid
						,d.sid
						,d.[name] AS UserName
						,DB_NAME() AS DatabaseName 
						,CASE WHEN x.state_desc = ''''GRANT'''' THEN 1 ELSE 0 END AS hasdbaccess
						,d.default_schema_name
						,grp.[name] AS RoleName
					FROM sys.database_principals d
					INNER JOIN sys.database_role_members drm
						ON d.principal_id = drm.member_principal_id
					INNER JOIN sys.database_principals grp
						ON drm.role_principal_id = grp.principal_id
					OUTER APPLY (SELECT dp.state_desc
					FROM sys.database_permissions dp
					WHERE d.principal_id = dp.grantee_principal_id
						AND permission_name = ''''CONNECT'''') x
					WHERE d.principal_id <> ''''1''''
						AND d.authentication_type_desc <> ''''NONE''''
						AND d.[name] NOT LIKE ''''#%'''' '' '

			END
			IF  @version >= 10
			BEGIN
				--print @cmd1
				EXEC sp_executesql @cmd1
				--print @cmd2
				EXEC sp_executesql @cmd2
			END
		END

		ELSE
		--	odear, server not available........
			BEGIN
				INSERT INTO dbo.tbl_ErrorLogsAll SELECT @LinkedServer,Getdate(),'ReportLogins: Linked server '+@LinkedServer+' NOT available or wrong version'
				SELECT @PrnLine = 'Linked server '+@LinkedServer+' NOT available or wrong version'
				print  @PrnLine
			END
   	FETCH NEXT FROM srv_cursor into @LinkedServer
END

CLOSE srv_cursor
DEALLOCATE srv_cursor

SELECT DISTINCT COALESCE(srv.[LinkedServer],db.[LinkedServer]) AS LinkedServer
	,db.[uid]
	,COALESCE(srv.[sid],db.[sid]) AS [sid]
	,COALESCE(srv.[UserName],db.[UserName]) AS UserName
	,srv.[DefaultDB]
	,srv.[denylogin]
	,srv.[hasaccess]
	,srv.[isntname]
	,srv.[sysadmin]
	,srv.[securityadmin]
	,srv.[serveradmin]
	,srv.[setupadmin]
	,srv.[processadmin]
	,srv.[diskadmin]
	,srv.[dbcreator]
	,srv.[bulkadmin]
	,srv.[principal_id]
	,srv.[type_desc]
	,srv.[is_disabled]
	,db.[hasdbaccess]
	,db.[DatabaseName]
	,db.[default_schema_name]
	,STUFF((SELECT ',' + dbi.[RoleName] AS [text()]
		FROM #dbUsers dbi
		WHERE dbi.[sid] = srv.[sid]
			AND dbi.DatabaseName = db.DatabaseName
			AND dbi.LinkedServer = db.LinkedServer
		FOR XML PATH('')
		), 1, 1, '' ) AS [RoleName]
	,@RunTime AS DateStart
INTO #UserLogins
FROM #srvLogins srv
FULL OUTER JOIN  #dbUsers db
	ON srv.sid = db.sid
	AND srv.LinkedServer = db.LinkedServer


/*** Close all records for Linked Servers that no longer exist ***/
Print 'Close all records for Linked Servers that no longer exist'
Update [dbo].[tbl_Logins]
SET DateStop = @RunTime
WHERE [dbo].[tbl_Logins].[Linkedserver] NOT IN 
				(SELECT [name] COLLATE DATABASE_DEFAULT as [Linkedserver]
					FROM   sys.servers 
					WHERE  [is_linked]=1 
					UNION SELECT @LocalServer as [Linkedserver])
	AND [dbo].[tbl_Logins].[DateStop] IS NULL

/*** insert missing records into tbl_Logins, using timestamp ***/
PRINT 'insert missing records into tbl_Logins, using timestamp'
INSERT INTO [dbo].[tbl_Logins]
	([LinkedServer],[uid],[sid],[UserName],[DefaultDB],[denylogin],[hasaccess],[isntname],[sysadmin]
	,[securityadmin],[serveradmin],[setupadmin],[processadmin],[diskadmin],[dbcreator],[bulkadmin]
	,[principal_id],[type_desc],[is_disabled],[hasdbaccess],[DatabaseName],[default_schema_name]
	,[RoleName],[DateStart],[DateStop])
SELECT [LinkedServer],[uid],[sid],UserName,[DefaultDB],[denylogin],[hasaccess],[isntname],[sysadmin]
	,[securityadmin],[serveradmin],[setupadmin],[processadmin],[diskadmin],[dbcreator],[bulkadmin]
	,[principal_id],[type_desc],[is_disabled],[hasdbaccess],[DatabaseName],[default_schema_name]
	,[RoleName],[DateStart],NULL AS DateStop
FROM #UserLogins tmp
WHERE NOT EXISTS (SELECT org.* FROM tbl_Logins org 
					WHERE org.[LinkedServer] = tmp.[LinkedServer]
					AND org.[UserName] = tmp.[UserName]
					AND COALESCE(org.[DatabaseName],'') = COALESCE(tmp.[DatabaseName],'')
					AND org.DateStop is NULL)

/*** Close all records in tbl_Logins that do not exist in #temp ***/
PRINT 'Close all records in tbl_Logins that do not exist in #temp'
Update [dbo].tbl_Logins
SET DateStop = @RunTime
FROM
	(SELECT org.[LinkedServer], org.[UserName] FROM [dbo].tbl_Logins org
		LEFT OUTER JOIN #UserLogins tmp
		ON org.[LinkedServer] = tmp.[LinkedServer]
			AND org.[UserName] = tmp.[UserName]
		WHERE tmp.[LinkedServer] IS NULL 
			AND tmp.[UserName] IS NULL) Q
WHERE [dbo].tbl_Logins.[LinkedServer] = Q.[LinkedServer]
	AND [dbo].tbl_Logins.[UserName] = Q.[UserName]
	AND [dbo].tbl_Logins.DateStop IS NULL
	AND [dbo].tbl_Logins.[LinkedServer] COLLATE DATABASE_DEFAULT IN (SELECT LinkedServer FROM [dbo].tbl_Servers WHERE Available = 1 AND Active = 1 AND MonitorContent = 1)

/*** Delete all from #UserLogins that is identical in tbl_Logins ***/
PRINT 'Delete all from #UserLogins that is identical in tbl_Logins'
DELETE  #UserLogins 
FROM [dbo].tbl_Logins org
WHERE org.[LinkedServer] = #UserLogins.[LinkedServer]
	AND org.[UserName] = #UserLogins.[UserName]
	AND COALESCE(org.[sysadmin],'') = COALESCE(#UserLogins.[sysadmin],'')
	AND COALESCE(org.[DatabaseName],'') = COALESCE(#UserLogins.[DatabaseName],'')
	AND COALESCE(org.[RoleName],'') = COALESCE(#UserLogins.[RoleName],'')

/*** Close all records in tbl_Logins that still exist in #UserLogins for they have changed ***/
PRINT 'Close all records in tbl_Logins that still exist in #UserLogins for they have changed'
Update [dbo].tbl_Logins
SET DateStop = @RunTime
FROM
	(SELECT org.* FROM [dbo].tbl_Logins org
	INNER JOIN #UserLogins tmp
		ON org.[LinkedServer] = tmp.[LinkedServer]
		AND org.[UserName] = tmp.[UserName]
		AND COALESCE(org.[DatabaseName],'') = COALESCE(tmp.[DatabaseName],'')) Q
WHERE [dbo].tbl_Logins.[LinkedServer] = Q.[LinkedServer]
	AND [dbo].tbl_Logins.[UserName] = Q.[UserName]
	AND COALESCE([dbo].tbl_Logins.[DatabaseName],'') = COALESCE(Q.[DatabaseName],'')
	AND [dbo].tbl_Logins.DateStop IS NULL

/*** insert changed records into tbl_Logins, using timestamp ***/
PRINT 'insert changed records into tbl_Logins, using timestamp'
INSERT INTO [dbo].[tbl_Logins]
	([LinkedServer],[uid],[sid],[UserName],[DefaultDB],[denylogin],[hasaccess],[isntname],[sysadmin]
	,[securityadmin],[serveradmin],[setupadmin],[processadmin],[diskadmin],[dbcreator],[bulkadmin]
	,[principal_id],[type_desc],[is_disabled],[hasdbaccess],[DatabaseName],[default_schema_name]
	,[RoleName],[DateStart],[DateStop])
SELECT [LinkedServer],[uid],[sid],UserName,[DefaultDB],[denylogin],[hasaccess],[isntname],[sysadmin]
	,[securityadmin],[serveradmin],[setupadmin],[processadmin],[diskadmin],[dbcreator],[bulkadmin]
	,[principal_id],[type_desc],[is_disabled],[hasdbaccess],[DatabaseName],[default_schema_name]
	,[RoleName],[DateStart],NULL AS DateStop
FROM #UserLogins tmp
WHERE NOT EXISTS (SELECT org.* FROM [dbo].tbl_Logins org 
					WHERE org.[LinkedServer] = tmp.[LinkedServer]
					AND org.[UserName] = tmp.[UserName]
					AND COALESCE(org.[DatabaseName],'') = COALESCE(tmp.[DatabaseName],'')
					AND org.DateStop is NULL)

/*** Cleanup ***/
PRINT 'Cleanup'
IF object_id('tempdb..#UserLogins') IS NOT NULL DROP TABLE #UserLogins
IF object_id('tempdb..#srvLogins') IS NOT NULL DROP TABLE #srvLogins
IF object_id('tempdb..#dbUsers') IS NOT NULL DROP TABLE #dbUsers

/*** Send an email with all new or updated records ***/
PRINT 'Send an email with all new or updated records'

declare @count int
declare @mail_qry nvarchar(4000),
		@subject     varchar(200)
	
select @count =  count(*)
	FROM [dbo].tbl_Logins
	WHERE DateStart = @RunTime OR DateStop = @RunTime

IF @count > 0 
	BEGIN
		SET @mail_qry = 'SELECT CONVERT(Varchar(5),[LoginID]) AS [ID]
			  ,substring([LinkedServer],0,30) AS [LinkedServer]
			  ,substring([UserName],0,40) AS [UserName]
			  ,CONVERT(varchar(4),[sysadmin]) AS [sysadmin]
			  ,COALESCE([DatabaseName],'''') AS [DatabaseName]
			  ,substring(COALESCE([RoleName],''''),0,50) AS [RoleName]
			  ,CONVERT(varchar(19),[DateStart],121) AS [DateStart]
			  ,CONVERT(varchar(19),[DateStop],121) AS [DateStop]
		FROM [' + DB_NAME() + '].[dbo].[tbl_Logins]			
		WHERE DateStart = '''+CONVERT(nchar(19),@RunTime,121)+''' OR DateStop = '''+CONVERT(nchar(19),@RunTime,121)+''' 
		ORDER BY [LinkedServer], UserName, DateStart'

		set @subject = 'Send by: ' + @LocalServer + '. Overview Logins'

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
				@query_result_header = 1
		END
	END

IF @count = 0 AND @MailStats = 1
	BEGIN
		set @subject = 'Sent by: ' + @LocalServer + '. Overview Logins'

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
;