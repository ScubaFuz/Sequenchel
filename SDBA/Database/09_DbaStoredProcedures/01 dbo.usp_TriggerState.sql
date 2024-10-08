CREATE PROCEDURE [dbo].[usp_TriggerState]  
           @TblName   VARCHAR(128), 
           @State BIT = 1 
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
SET NOCOUNT OFF
DECLARE @SQLState VARCHAR(500) 
IF @State = 0 
        BEGIN 
             SET @SQLState = 'ALTER TABLE '+ @TblName + ' DISABLE TRIGGER ALL' 
        END 
ELSE 
   BEGIN 
             SET @SQLState = 'ALTER TABLE ' + @TblName + ' ENABLE TRIGGER ALL' 
   END 
print @SQLstate
EXEC (@SQLState)
;