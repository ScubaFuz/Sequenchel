CREATE PROCEDURE [dbo].[usp_enable_all_Triggers_in_db]  
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
  exec sp_MSforeachtable 'usp_TriggerState ''?'' , 1'
;