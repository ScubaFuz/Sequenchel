CREATE PROCEDURE [dbo].[usp_Enable_all_const_in_db]  
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
 @command1 = 'usp_ConstraintState ''?'' , 1'
;