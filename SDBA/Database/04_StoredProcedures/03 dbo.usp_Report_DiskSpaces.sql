CREATE PROCEDURE [dbo].[usp_Report_DiskSpaces]
	@Recipient varchar(250) = 'Screen',
	@ExceptionList varchar(250) = NULL,	--The Servers not to process in a single quoted, comma seperated list
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
-- Purpose	Collect diskspace information from multuple servers
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-09	BT		Initial version
-- 2.0		2008-09-26	BT		Added comparison function and email functionality
--								Replaced serverloop with usp_Enum_Servers procedure
-- 2.1		2008-10-02	BT		Replaced mail query with view
-- 2.2		2014-12-24	BT		Added MailStats option
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
-- ****************************************************************************

set nocount on
DECLARE @command nvarchar(4000)
DECLARE @RunTime smalldatetime
DECLARE @LocalServer nvarchar(255)

SET @LocalServer = CAST(SERVERPROPERTY('ServerName') AS nvarchar(255))
SET @RunTime = Getdate()

IF object_id('tempdb..##drives1') IS NOT NULL DROP TABLE ##drives1
CREATE TABLE ##drives1 (LinkedServer nvarchar(255) NULL
						,Drive char(1) NOT NULL
						,FreeSpaceMB bigint NULL)

SET @command = 'xp_fixeddrives'
EXEC usp_Enum_Servers 'S','master',@command,'##drives1(Drive,FreeSpaceMB)',1,@ExceptionList,@Separator,@SqlVersion,@IncludeHigherVersions

IF object_id('tempdb..##drives2') IS NOT NULL DROP TABLE ##drives2
CREATE TABLE ##drives2 (LinkedServer nvarchar(255) NULL
						,Drive char(1) NOT NULL
						,LogicalName nvarchar(128) NULL
						,file_system_type nvarchar(128) NULL
						,TotalSizeMB bigint NULL
						,FreeSpaceMB bigint NULL
						,supports_compression bit NULL
						,supports_alternate_streams bit NULL
						,supports_sparse_files bit NULL
						,is_read_only bit NULL
						,is_compressed bit NULL)

IF @SqlVersion < 10 
BEGIN
	SET @SqlVersion = 10
	SET @IncludeHigherVersions = 1
END

SET @command = 'SELECT DISTINCT 
		LEFT(dvs.volume_mount_point,1) AS Drive
		,dvs.logical_volume_name AS LogicalName
		,dvs.file_system_type
		,CONVERT(BIGINT,dvs.total_bytes/1024/1024) AS TotalSizeMB
		,CONVERT(BIGINT,dvs.available_bytes/1024/1024) AS FreeSpaceMB
		,dvs.supports_compression
		,dvs.supports_alternate_streams
		,dvs.supports_sparse_files
		,dvs.is_read_only
		,dvs.is_compressed
	FROM sys.master_files mf
	CROSS APPLY sys.dm_os_volume_stats(mf.database_id, mf.FILE_ID) dvs'
EXEC usp_Enum_Servers 'Q','master',@command,'##drives2(Drive,LogicalName,file_system_type,TotalSizeMB,FreeSpaceMB,supports_compression,supports_alternate_streams,supports_sparse_files,is_read_only,is_compressed)',1,@ExceptionList,@Separator,@SqlVersion,@IncludeHigherVersions

/*** insert all records into tbl_DiskSpaces, using timestamp ***/
PRINT 'insert missing records into tbl_DiskSpaces, using timestamp'
INSERT INTO [dbo].tbl_DiskSpaces (LinkedServer,Drive,LogicalName,file_system_type,TotalSizeMB,FreeSpaceMB,supports_compression
								,supports_alternate_streams,supports_sparse_files,is_read_only,is_compressed,DateLogged)
SELECT COALESCE(drv1.LinkedServer,drv2.LinkedServer) As LinkedServer
	,COALESCE(drv1.Drive,drv2.Drive) AS Drive
	,drv2.LogicalName
	,drv2.file_system_type
	,drv2.TotalSizeMB
	,COALESCE(drv1.FreeSpaceMB,drv2.FreeSpaceMB) AS FreeSpaceMB
	,drv2.supports_compression
	,drv2.supports_alternate_streams
	,drv2.supports_sparse_files
	,drv2.is_read_only
	,drv2.is_compressed
	,@RunTime AS DateLogged 
FROM ##drives1 drv1
FULL OUTER JOIN ##drives2 drv2
	ON drv1.LinkedServer = drv2.LinkedServer
	AND drv1.Drive = drv2.Drive

IF object_id('tempdb..##drives') IS NOT NULL DROP TABLE ##drives
--select * from tbl_DiskSpaces

/*** send an email with estimates before disk full time ***/

declare @mail_qry nvarchar(4000),
		@subject     varchar(200),
		@Message varchar (1000),
		@Number int

SELECT @Number = count(*) FROM dbo.vw_DiskUseDaysLeft

IF @Number > 0
BEGIN
	SET @mail_qry = 'SELECT * FROM [' + DB_NAME() + '].dbo.vw_DiskUseDaysLeft'
	--print @mail_qry
	SET @subject = 'Send by: ' + @LocalServer + '. Overview Disk Spaces'
	SET @Message='Explanation of the fields

	LinkedServer	Name of the server the drive is on 
	Drive		Name of the Drive
	MaxDate	The last day measured
	FreeMax	Free space on the last day measured
	Min8Date	Oldest day measured not older than 8 days
	Free8		Free space on the Min8Date
	Days8		Number of days between Min8Date and MaxDate
	Grow8		Growth between Min8Date and MaxDate
	DaysLeft8	Number of days left in the same growth rate
	Min30Date	Oldest day measured not older than 30 days
	Free30	Free space on the Min30Date
	Days30	Number of days between Min30Date and MaxDate
	Grow30	Growth between Min30Date and MaxDate
	DaysLeft30	Number of days left in the same growth rate

	Only rows with daysLeft values less than 100 days are shown
	If there are no rows, there is no usefull information to report.
	'

	IF @Recipient = 'Screen'  --Send no mail, just display on screen'
	BEGIN
		EXEC sp_executesql @mail_qry;
	END
	ELSE
	BEGIN
		EXEC msdb..sp_send_dbmail 
			@recipients = @Recipient,
			@subject = @subject,
			@body=@Message,
			@attach_query_result_as_file = 1,
			@query_attachment_filename = 'DiskSpaces.csv',
			@query_result_separator = '	',
			@query_result_width=1000,
			@query=@mail_qry,
			@query_result_header = 1
	END
END

IF @Number = 0 AND @MailStats = 1
	BEGIN
		set @subject = 'Sent by: ' + @LocalServer + '. Overview Disc Spaces'

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