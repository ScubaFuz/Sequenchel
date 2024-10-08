CREATE PROCEDURE [dbo].[usp_BackupHandle]
	@Action nvarchar(10),
	@Database nvarchar(100),
	@Path nvarchar(100),
	@DateTimeStamp nchar(13)
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose		
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2012-02-24	BT		Initial version
-- ****************************************************************************

print @Action
IF @Action = 'CREATE'
	BEGIN
		DECLARE @cmd nvarchar(1000)
		If RIGHT(@Path,1) <> '\' SET @Path = @Path + '\'
		SET @cmd = 'BACKUP DATABASE [' + @Database + '] TO  DISK = N''' + @Path + @Database + '_' + RTrim(@DateTimeStamp) + '.bak'' WITH NOFORMAT, NOINIT,  NAME = N''' + @Database + '-Full Database Backup'', SKIP, NOREWIND, NOUNLOAD,  STATS = 10'
		print @cmd
		EXEC sp_executesql @cmd

	END

;