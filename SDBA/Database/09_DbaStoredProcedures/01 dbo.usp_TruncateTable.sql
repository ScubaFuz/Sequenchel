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

BEGIN TRY
	--Remove all data at once
	SET @SQLState = ''
	SET @SQLState = 'TRUNCATE TABLE ' + @TblName
	print @SQLState
	EXECUTE (@SQLState)
	print 'Table ' + @TblName + ' truncated'
END TRY
BEGIN CATCH
	--Data removal failed, probably because of a constraint. Try removing every row.
	SET @SQLState = ''
	SET @SQLState = 'DELETE FROM ' + @TblName
	print @SQLState
	EXECUTE (@SQLState)
	print cast(@@RowCount as nvarchar(10)) + ' rows deleted'
END CATCH

SET @SQLState = 'DBCC CHECKIDENT ('''+@TblName+''',RESEED,'+@CurSeed+')'
print @SQLState
EXEC (@SQLState)
;