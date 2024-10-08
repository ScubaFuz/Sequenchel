CREATE VIEW [dbo].[vw_DataSpaces]
AS

-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2015-07-20	BT		Initial version
-- ****************************************************************************

SELECT [DataSpaceID]
	,[LinkedServer]
	,[DatabaseName]
	,[LogicalName]
	,[FileID]
	,[File_Size_MB]
	,[Space_Used_MB]
	,[Free_Space_MB]
	,[Free_Space_Prc]
	,[Growth]
	,[Perc]
	,[PercGrowth]
	,[FileName]
	,[LogDate]
	,dbo.udf_CorrectCommand([LinkedServer],[DatabaseName],[LogicalName],[File_Size_MB],[Free_Space_MB],[Perc],[Growth]) AS CorrectCommand
  FROM [dbo].[tbl_DataSpaces]
;
