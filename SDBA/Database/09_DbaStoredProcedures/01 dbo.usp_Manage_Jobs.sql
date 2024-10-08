CREATE PROC [dbo].[usp_Manage_Jobs] 
	@Enable int = 0
WITH ENCRYPTION
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 01		2011-10-10	BT		Initial version
--
-- ****************************************************************************
SET NOCOUNT ON

DECLARE @job_name SYSNAME
DECLARE @job_id UNIQUEIDENTIFIER

DECLARE disable_jobs CURSOR FOR
SELECT Cast(name as SYSNAME)
FROM msdb.dbo.sysjobs
ORDER BY name

SET @job_id = NULL

OPEN disable_jobs
FETCH NEXT FROM disable_jobs INTO @job_name

WHILE @@FETCH_STATUS = 0
BEGIN


EXEC msdb.dbo.sp_verify_job_identifiers '@job_name', '@job_id', @job_name OUTPUT, @job_id OUTPUT

EXEC msdb.dbo.sp_update_job @job_id, @enabled = @Enable

SET @job_id = NULL

FETCH NEXT FROM disable_jobs INTO @job_name

END

CLOSE disable_jobs
DEALLOCATE disable_jobs

RETURN
;