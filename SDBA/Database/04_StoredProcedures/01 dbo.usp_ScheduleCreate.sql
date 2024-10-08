CREATE PROCEDURE [dbo].[usp_ScheduleCreate]
	@JobName nvarchar(128) = NULL
	,@SqlCommand nvarchar(max) = NULL
	,@DatabaseName nvarchar(128) = NULL
	,@OutputPath nvarchar(255) = NULL
	,@FreqType int = 1 -- 1 Once, 4 Daily, 8 Weekly
	,@FreqInterval int = 0 -- 1 Sunday, 4 Monday, .... 64 Saturday
	,@FreqSubType int = 1 --1 occurs once, 4 every x minutes, 8 every x hours
	,@FreqSubTypeInt int = 0 -- number of minutes / hours for recurrence
	,@StartDate int = NULL --YYYYMMDD
	,@StartTime int = NULL --HHMMSS
	,@EndTime int = NULL --HHMMSS
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Create Scheduled JOb
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2015-04-02	BT		Initial Version
-- ****************************************************************************
 
--SET @FreqType = 8 -- 1 Once, 4 Daily, 8 Weekly
--SET @FreqInterval = 79 -- 1 Sunday, 4 Monday, .... 64 Saturday
--SET @FreqSubType = 8 --1 occurs once, 4 every x minutes, 8 every x hours
--SET @FreqSubTypeInt = 4 -- number of minutes / hours for recurrence
--SET @StartDate = 20150401 --YYYYMMDD
--SET @StartTime = 083000 --HHMMSS
--SET @EndTime = 183000 --HHMMSS
 
DECLARE @StepName nvarchar(128)
SET @StepName = @JobName + 'Step'
DECLARE @ScheduleName nvarchar(128)
SET @ScheduleName = @JobName + 'Run'
SET @OutputPath = @OutputPath + '\' + @JobName + '.log'
 
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Sequenchel' AND category_class=1)
       BEGIN
             EXEC msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Sequenchel'
       END;
EXEC  msdb.dbo.sp_add_job
       @job_name=@JobName,
       @description=N'Sequenchel',
       @category_name=N'Sequenchel',
       @owner_login_name=N'sa';
IF @SqlCommand <> '-1'
	BEGIN
		EXEC msdb.dbo.sp_add_jobstep
			   @job_name=@JobName,
			   @step_name=@StepName,
			   @subsystem=N'TSQL',
			   @command=@SqlCommand,
			   @database_name=@DatabaseName,
			   @output_file_name=@OutputPath;
	END
EXEC msdb.dbo.sp_add_jobschedule
       @job_name=@JobName,
       @name=@ScheduleName,
       @freq_type=@FreqType,
       @freq_interval=@FreqInterval,
       @freq_subday_type=@FreqSubType,
       @freq_subday_interval=@FreqSubTypeInt,
       @active_start_date=@StartDate,
       @active_start_time=@StartTime,
       @active_end_time=@EndTime,
       @freq_recurrence_factor = 1;
 
EXEC msdb.dbo.sp_add_jobserver @job_name=@JobName, @server_name = N'(local)';