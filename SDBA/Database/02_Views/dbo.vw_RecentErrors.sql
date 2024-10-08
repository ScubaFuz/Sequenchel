CREATE VIEW [dbo].[vw_RecentErrors]
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-30	BT		Initial version
-- 3.1		2015-06-19	BT		Added GetDate in case Datechange is NULL
-- ****************************************************************************
SELECT LinkedServer,logdate,logtext,fulltext,ord FROM 
				(
				SELECT SUBSTRING(a.LinkedServer,1,20) LinkedServer,a.logdate logdate ,1 ord , SUBSTRING(a.logtext,1,120) logtext, logtext fulltext
						 FROM 	[dbo].[tbl_ErrorLogsAll] a 
							WHERE a.logdate > COALESCE((SELECT DateChange FROM [dbo].[tbl_Configuration] WHERE ConfigName = 'LastLogMessage'),GETDATE()-1)
				union all 
				SELECT SUBSTRING(a.LinkedServer,1,20) LinkedServer,a.logdate logdate ,2 ord , SUBSTRING(a.logtext,121,120) logtext, logtext fulltext
						 FROM 	[dbo].[tbl_ErrorLogsAll] a
							WHERE a.logdate > COALESCE((SELECT DateChange FROM [dbo].[tbl_Configuration] WHERE ConfigName = 'LastLogMessage'),GETDATE()-1)
											  and RTRIM(SUBSTRING(a.logtext,121,120)) <> ''
				union all 
				SELECT SUBSTRING(a.LinkedServer,1,20) LinkedServer,a.logdate logdate ,3 ord , SUBSTRING(a.logtext,241,120) logtext, logtext fulltext
						 FROM 	[dbo].[tbl_ErrorLogsAll] a 
							WHERE a.logdate > COALESCE((SELECT DateChange FROM [dbo].[tbl_Configuration] WHERE ConfigName = 'LastLogMessage'),GETDATE()-1)
											  and RTRIM(SUBSTRING(a.logtext,241,120)) <> ''
				) [log];
