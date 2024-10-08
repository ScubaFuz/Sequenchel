CREATE PROCEDURE [dbo].[usp_ConstraintState]  
              @TblName   VARCHAR(128), 
              @State BIT = 1 
WITH ENCRYPTION
AS 
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Turn state of constraints On of Off
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 01		2008-09-30	BT		Initial version
--
-- ****************************************************************************
SET NOCOUNT OFF
DECLARE @SQLState VARCHAR(500) 
IF @State = 0 
  BEGIN 
    SET @SQLState = 'ALTER TABLE '+ @TblName + ' NOCHECK CONSTRAINT ALL' 
  END 
ELSE 
  BEGIN 
  	SET @SQLState = 'ALTER TABLE ' + @TblName + ' CHECK CONSTRAINT ALL' 
  END 
print @SQLstate
EXEC (@SQLState)
;