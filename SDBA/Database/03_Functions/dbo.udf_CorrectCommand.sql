CREATE FUNCTION [dbo].[udf_CorrectCommand] 
	(@LinkedServer nvarchar(128)
	,@DatabaseName nvarchar(128)
	,@logicalName nvarchar(128)
	,@File_Size_MB int
	,@Free_Space_MB int
	,@Perc bit
	,@Growth int
	)
RETURNS nvarchar(255)
AS
-- *****************************************************************************
-- Auteur       : Bart Thieme
-- Doel         : Build Correction Command
-- *****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	************************************************
-- 1.0		2015-04-24	BT		Initial version
-- *****************************************************************************

BEGIN
	DECLARE @LowerLimit int, @UpperLimit int
	DECLARE @SmallGrowth int, @MediumGrowth int, @LargeGrowth int, @MinFreeSpace int, @MinPercGrowth int--, @FreeSpaceMin int
	DECLARE @cmd nvarchar(255), @cmdBefore nvarchar(255), @cmdSize nvarchar(25), @cmdGrowth nvarchar(25), @cmdAfter nvarchar(150)
 
	SET @LowerLimit = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'LowerLimit')
	SET @UpperLimit = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'UpperLimit')
	SET @SmallGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'SmallGrowth')
	SET @MediumGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'MediumGrowth')
	SET @LargeGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'LargeGrowth')
	SET @MinFreeSpace = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'MinFreeSpace')
	SET @MinPercGrowth = (SELECT [ConfigValue] FROM [tbl_Configuration] WHERE [Category] = 'MonitorDataSpaces' AND [ConfigName] = 'MinPercGrowth')
 
	IF @LowerLimit IS NULL OR isnumeric(@LowerLimit) = 0 SET @LowerLimit = 5000
	IF @UpperLimit IS NULL OR isnumeric(@UpperLimit) = 0 SET @UpperLimit = 10000
	IF @SmallGrowth IS NULL OR isnumeric(@SmallGrowth) = 0 SET @SmallGrowth = 100
	IF @MediumGrowth IS NULL OR isnumeric(@MediumGrowth) = 0 SET @MediumGrowth = 500
	IF @LargeGrowth IS NULL OR isnumeric(@LargeGrowth) = 0 SET @LargeGrowth = 1000
	IF @MinFreeSpace IS NULL OR isnumeric(@MinFreeSpace) = 0 SET @MinFreeSpace = 0.5
	IF @MinPercGrowth IS NULL OR isnumeric(@MinPercGrowth) = 0 SET @MinPercGrowth = 10
 
 
	SET @cmdBefore = 'EXEC (''ALTER DATABASE [' + @DatabaseName + '] MODIFY FILE ( NAME = N'''''+ @logicalName + '''''' 
	SET @cmdSize = 
		CASE 
			WHEN @File_Size_MB > @UpperLimit AND @Free_Space_MB < @SmallGrowth THEN ',SIZE =' + CAST(@File_Size_MB + @LargeGrowth AS nvarchar(10)) + 'MB'
			WHEN @File_Size_MB < @SmallGrowth THEN  ',SIZE =' + CAST(@SmallGrowth AS nvarchar(10)) + 'MB'
			ELSE ''
		END 
	SET @cmdGrowth = 
		CASE
			WHEN @Perc = 1 AND @Growth < @MinPercGrowth THEN ',FILEGROWTH = ' + CAST(@MinPercGrowth AS nvarchar(10)) + '%'
			WHEN @Perc = 0 AND @File_Size_MB < @LowerLimit AND @Growth < @SmallGrowth THEN ',FILEGROWTH = ' + CAST(@SmallGrowth AS nvarchar(10)) + 'MB'
			WHEN @Perc = 0 AND @File_Size_MB BETWEEN @LowerLimit AND  @UpperLimit AND @Growth < @MediumGrowth THEN ',FILEGROWTH = ' + CAST(@MediumGrowth AS nvarchar(10)) + 'MB'
			WHEN @Perc = 0 AND @File_Size_MB > @UpperLimit AND @Growth < @LargeGrowth THEN ',FILEGROWTH = ' + CAST(@LargeGrowth AS nvarchar(10)) + 'MB'
			ELSE ''
		END 
	SET @cmdAfter = ')'') AT [' + @LinkedServer + ']' 

	IF @cmdSize = '' AND @cmdGrowth = ''
		SET @cmd = NULL
	ELSE
		SET @cmd = @cmdBefore + @cmdSize + @cmdGrowth + @cmdAfter

	RETURN @cmd
END
;
