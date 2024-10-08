CREATE PROCEDURE [dbo].[usp_Enable_all_BusinessRules]
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
begin
  exec usp_enable_all_const_in_db
  exec usp_enable_all_Triggers_in_db
  select 'NOT enabled triggers: ',count(*) from sys.triggers where is_disabled = 1
  select name from sys.triggers where is_disabled = 1
  select 'NOT enabled foreign keys: ',count(*) from sys.foreign_keys where is_disabled = 1
  select name from sys.foreign_keys where is_disabled = 1
end
;