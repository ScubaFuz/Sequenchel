CREATE PROCEDURE [dbo].[usp_Test_LinkedServer] 
			@LinkedServer SYSNAME=NULL ,
			@version      integer= NULL OUT	
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-30	BT		Initial version
-- 2.0		2011-10-23	BT		Added version 10 (and up) detection
-- 2.1		2011-10-25	BT		Replaced ping command with TRY/CATCH
-- 2.5		2014-11-27	BT		Added replace for [ and ]
-- ****************************************************************************

SET nocount on
declare @cmd1 nvarchar(255)
DECLARE @attribute_value char(1)
IF OBJECT_ID('tempdb..#serverinfo') IS NOT NULL DROP TABLE #serverinfo
CREATE TABLE #serverinfo (attribute_id int, 
							attribute_name varchar (60), 
							attribute_value varchar  (255))

BEGIN TRY
		SET @LinkedServer = REPLACE(REPLACE(@LinkedServer,'[',''),']','')
		SET @cmd1='exec ['+@LinkedServer+'].[master].[dbo].[sp_server_info] 500'
		--    print @cmd1
		INSERT INTO #serverinfo  EXEC sp_executesql @cmd1
		SELECT @version = CAST(left(attribute_value,PATINDEX('%.%',attribute_value)-1) as integer) FROM #serverinfo
		RETURN 0 --SUCCESS
END TRY
BEGIN CATCH
		RETURN -1 --ERROR
END CATCH

IF OBJECT_ID('tempdb..#serverinfo') IS NOT NULL DROP TABLE #serverinfo
;