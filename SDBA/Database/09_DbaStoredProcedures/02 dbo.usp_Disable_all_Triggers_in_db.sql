CREATE PROCEDURE [dbo].[usp_Disable_all_Triggers_in_db]  
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
  EXEC sp_MsForEachTable 'usp_TriggerState ''?'' , 0'
;