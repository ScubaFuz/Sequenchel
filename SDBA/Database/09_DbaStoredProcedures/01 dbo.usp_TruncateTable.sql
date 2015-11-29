CREATE PROCEDURE [dbo].[usp_TruncateTable]  
              @TblName   VARCHAR(128)
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
declare @CurSeed  VARCHAR(10)
set @CurSeed = cast(IDENT_SEED(@TblName)-1 as varchar(10))
print @TblName
print @CurSeed
SET @SQLState = 'DELETE FROM '+ @TblName
print @SQLstate
EXEC (@SQLState) 
SET @SQLstate = 'DBCC CHECKIDENT ('''+@TblName+''',RESEED,'+@CurSeed+')'
print @SQLstate
EXEC (@SQLState)
;