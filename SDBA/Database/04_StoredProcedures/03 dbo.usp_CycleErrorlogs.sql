CREATE PROCEDURE [dbo].[usp_CycleErrorlogs]
	@ExceptionList varchar(250) = NULL,	--The Servers not to process in a single quoted, (comma) seperated list
	@Separator nchar(1) = ',',			-- Separator character for csv
	@SqlVersion int = 0,				-- The SQL Version to execute this command to (0 = All)
	@IncludeHigherVersions bit = 1			-- Include higher SQL versions than the SQL version given.
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Cycle SQL error logs on all linked servers
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2011-10-26	BT		Initial version
-- 2.6		2015-01-17	BT		Added Separator variable for CSV input
-- 2.7		2015-01-28	BT		Added SQL Version select option
-- ****************************************************************************
set nocount on
declare @command nvarchar(4000)

SET @command = 'sp_cycle_errorlog'
EXEC usp_Enum_Servers 'S','master',@command,NULL,0,@ExceptionList,@Separator,@SqlVersion,@IncludeHigherVersions;


SET @command = 'sp_cycle_agent_errorlog'
EXEC usp_Enum_Servers 'S','master',@command,NULL,0,@ExceptionList,@Separator,@SqlVersion,@IncludeHigherVersions;
