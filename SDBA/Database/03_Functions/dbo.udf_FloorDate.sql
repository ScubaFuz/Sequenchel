CREATE FUNCTION [dbo].[udf_FloorDate]
( 
    @dt DATETIME 
) 
RETURNS DATETIME 
AS 
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Returns a given date without it's time!
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 01		2008-09-30	BT		Initial version
--
-- ****************************************************************************

BEGIN 
    set @dt = convert(datetime,convert(int,@dt))
    RETURN @dt 
END;
