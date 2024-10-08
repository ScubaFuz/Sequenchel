CREATE PROCEDURE [dbo].[usp_ImportXML]
	 @SrcPath nvarchar(510)
	,@ImportFolder bit = 0
	,@TargetTable sysname = NULL
	,@ArchivePath nvarchar(510)
	,@DeleteSourceFile bit = 0
	,@ErrorPath nvarchar(510)
	,@ClearTargetTable bit = 0
WITH ENCRYPTION
AS

--*******************************************************
-- Author: Bart Thieme
-- Purpose: Import .xml file into xml table
--*******************************************************
-- Date			Version	Author	Remarks
-- 2017-01-22	1.0		BT		Initial version
--*******************************************************

PRINT 'procedure [dbo].[usp_ImportXML] started'

SET ANSI_WARNINGS OFF
SET NOCOUNT ON
DECLARE @FileNames nvarchar(510), @Command nvarchar(2000)
DECLARE @RunTime SMALLDATETIME, @ErrCount int, @OfferCount int
DECLARE @FilesInAFolder TABLE  (FileNames nvarchar(510))
DECLARE @Command2 nvarchar(2000)
DECLARE @Command3 nvarchar(2000)

SET @ErrCount = 0

IF @TargetTable IS NULL return

IF @ClearTargetTable = 1 
BEGIN
SET @Command = N'TRUNCATE TABLE ' + @TargetTable
	EXECUTE sp_ExecuteSql @command
END

IF @ImportFolder = 1
	BEGIN
		SET @Command = N'dir ' + @SrcPath + N' /b /s '
		INSERT INTO @FilesInAFolder
		EXEC MASTER..xp_cmdshell @Command
	END
ELSE
	BEGIN
		INSERT INTO @FilesInAFolder
		SELECT @SrcPath
	END

DELETE FROM @FilesInAFolder
WHERE FileNames IS NULL
	OR Right(FileNames,4) <> '.xml'

DECLARE @FilesToProcess TABLE  (FileNames VARCHAR(255))
INSERT INTO @FilesToProcess
SELECT TOP 1000 FileNames FROM @FilesInAFolder
WHERE Right(FileNames,4) = '.xml'

DELETE FROM @FilesInAFolder
WHERE FileNames IN
	(SELECT FileNames FROM @FilesToProcess ftp)

SET @RunTime = getdate()

WHILE (SELECT COUNT(*) FROM @FilesToProcess) > 0
BEGIN
	SET @FileNames = (SELECT TOP 1 FileNames FROM @FilesToProcess)
	print @FileNames

BEGIN TRY
	EXEC(N'
	INSERT INTO ' + @TargetTable + N'(FileName, DateImport, XmlData)

	SELECT ''' + @FileNames + N''', ''' + @RunTime + N''', CONVERT(xml, xmlData, 2) 
	FROM
	(
		SELECT  * 
		FROM    OPENROWSET (BULK ''' + @FileNames + N''' , SINGLE_BLOB) AS XMLDATA
	) AS FileImport (XMLDATA)
	')
	DELETE FROM @FilesToProcess
	WHERE FileNames = @FileNames

	IF @ArchivePath IS NOT NULL
		BEGIN
			SET @Command3 = N'move ' + @FileNames + N' ' + @ArchivePath  + N'\' + REPLACE(CONVERT(nvarchar(16),@RunTime,126),':','') + N'_Archive_'+ CAST(@ErrCount AS nvarchar(5)) + N'_' + RIGHT(@FileNames,PATINDEX('%\%',REVERSE(@FileNames))-1)  
			print @Command3
			EXEC MASTER..xp_cmdshell @Command3
		END
	IF @ArchivePath IS NULL AND @DeleteSourceFile = 1
		BEGIN
			SET @Command2 = N'del ' + @FileNames
			print @Command2
			EXEC MASTER..xp_cmdshell @Command2
		END
END TRY
BEGIN CATCH
	SET @ErrCount = @ErrCount + 1
	DELETE FROM @FilesToProcess
	WHERE FileNames = @FileNames

	IF @ErrorPath IS NOT NULL
	BEGIN
		SET @Command3 = N'move ' + @FileNames + ' ' + @ErrorPath  + N'\' + REPLACE(CONVERT(nvarchar(16),@RunTime,126),':','') + N'_Error_'+ CAST(@ErrCount AS nvarchar(5)) + N'_' + RIGHT(@FileNames,PATINDEX('%\%',REVERSE(@FileNames))-1)  
		print @Command3
		EXEC MASTER..xp_cmdshell @Command3
	END
END CATCH

END

IF @ErrCount > 0
	PRINT 'procedure [dbo].[usp_ImportXML] completed with ' + CAST(@ErrCount AS nvarchar(100)) + ' errors'
ELSE
	PRINT 'procedure [dbo].[usp_ImportXML] completed succesfully'
