CREATE  PROCEDURE [dbo].[usp_Disable_all_BusinessRules]
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
  exec usp_disable_all_const_in_db
  exec usp_disable_all_Triggers_in_db
  select 'NOT disabled triggers: ',count(*) from sys.triggers where is_disabled = 0
  select name from sys.triggers where is_disabled = 0
  select 'NOT disabled foreign keys: ',count(*) from sys.foreign_keys where is_disabled = 0
  select name from sys.foreign_keys where is_disabled = 0
end
;