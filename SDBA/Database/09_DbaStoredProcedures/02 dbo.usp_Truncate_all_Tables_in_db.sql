CREATE PROCEDURE [dbo].[usp_Truncate_all_Tables_in_db]  
WITH ENCRYPTION
AS 
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 01		2008-09-30	BT		Initial version
--
-- ****************************************************************************
EXEC sp_MSforeachtable 
 @command1 = "usp_TruncateTable '?'"
;