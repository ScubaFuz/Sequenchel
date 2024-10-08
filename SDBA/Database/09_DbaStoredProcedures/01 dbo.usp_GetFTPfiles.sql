CREATE PROCEDURE [dbo].[usp_GetFTPfiles]
	@FTPServer varchar(128) = '<FtpServer>',		-- FTP Server name or IP-address
	@FTPUser varchar(128) = NULL,
	@FTPPwd varchar(128) = NULL,
	@SourcePath varchar(128) = '<SourcePath>',		-- Source Path
	@SourceFiles varchar(128) = '<SourceFiles>',	-- Source Filename
	@DestPath varchar(128) = '<DestPath>',			-- Destination path. Blank for root directory.
	@FTPMode varchar(10) = '<FtpMode>',				-- ascii, binary or blank for default.
	@Remove bit = 0									-- 1 = Delete sourcefile(s) after copy, 0 = leave file
WITH ENCRYPTION
AS

-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Copy/FTP one or multiple files from an FTP server
-- ****************************************************************************
-- Version	Date		Author	Description
-- ******	**********	******	***********************************************
-- 1.0		2010-11-01	BT		Initial version
-- 2.0		2016-01-06	BT		Added Username and Password as input variables
--								Added configure options
-- ****************************************************************************

--exec sp_configure 'show advanced options', 1;
--reconfigure;
--exec sp_configure 'xp_cmdshell', 1;
--reconfigure;

SET NOCOUNT ON

-- FTP attributes.
IF @FTPUser IS NULL SET @FTPUser = '<FtpUser>'
-- Do not save the password in clear text
IF @FTPPwd IS NULL SET @FTPPwd = '<FtpPassword>'		

DECLARE @cmd varchar(1000)
DECLARE @workfile varchar(128)
DECLARE @nowstr varchar(25)

-- Get the %TEMP% environment variable.
DECLARE @tempdir varchar(128)
CREATE TABLE #tempvartable(info VARCHAR(1000))
INSERT #tempvartable EXEC master..xp_cmdshell 'echo %temp%'
SET @tempdir = (SELECT top 1 info FROM #tempvartable)
IF RIGHT(@tempdir, 1) <> '\' SET @tempdir = @tempdir + '\'
DROP TABLE #tempvartable

-- Generate @workfile
SET @nowstr = replace(replace(convert(varchar(30), GETDATE(), 121), ' ', '_'), ':', '-')
SET @workfile = 'FTP_SPID' + convert(varchar(128), @@spid) + '_' + @nowstr + '.txt'

-- Deal with special chars for echo commands.
select @FTPServer = replace(replace(replace(@FTPServer, '|', '^|'),'<','^<'),'>','^>')
select @FTPUser = replace(replace(replace(@FTPUser, '|', '^|'),'<','^<'),'>','^>')
select @FTPPwd = replace(replace(replace(@FTPPwd, '|', '^|'),'<','^<'),'>','^>')
select @SourcePath = replace(replace(replace(@SourcePath, '|', '^|'),'<','^<'),'>','^>')
IF RIGHT(@DestPath, 1) = '\' SET @DestPath = LEFT(@DestPath, LEN(@DestPath)-1)

-- Build the FTP script file.
select @cmd = 'echo ' + 'open ' + @FTPServer + ' > ' + @tempdir + @workfile
select @cmd
EXEC master..xp_cmdshell @cmd
select @cmd = 'echo ' + @FTPUser + '>> ' + @tempdir + @workfile
select @cmd
EXEC master..xp_cmdshell @cmd
select @cmd = 'echo ' + @FTPPwd + '>> ' + @tempdir + @workfile
select @cmd
EXEC master..xp_cmdshell @cmd
--select @cmd = 'echo ' + 'prompt ' + ' >> ' + @tempdir + @workfile
--select @cmd
--EXEC master..xp_cmdshell @cmd
IF LEN(@FTPMode) > 0
BEGIN
	select @cmd = 'echo ' + @FTPMode + ' >> ' + @tempdir + @workfile
select @cmd
	EXEC master..xp_cmdshell @cmd
END
select @cmd = 'echo ' + 'lcd ' + @DestPath + ' >> ' + @tempdir + @workfile
select @cmd
EXEC master..xp_cmdshell @cmd
IF LEN(@SourcePath) > 0
BEGIN
	select @cmd = 'echo ' + 'cd ' + @SourcePath + ' >> ' + @tempdir + @workfile
	EXEC master..xp_cmdshell @cmd
END
--IF RIGHT(@SourcePath, 1) <> '/' SET @SourcePath = @SourcePath + '/'
select @cmd = 'echo ' + 'mget ' + @SourceFiles + ' >> ' + @tempdir + @workfile
select @cmd
EXEC master..xp_cmdshell @cmd

If @Remove = 1
BEGIN
	--select @cmd = 'echo ' + 'prompt' + ' >> ' + @tempdir + @workfile
	--select @cmd
	--EXEC master..xp_cmdshell @cmd
	select @cmd = 'echo ' + 'mdelete ' + @SourceFiles + ' ^|yes >> ' + @tempdir + @workfile
	select @cmd
	EXEC master..xp_cmdshell @cmd
END

select @cmd = 'echo ' + 'quit' + ' >> ' + @tempdir + @workfile
select @cmd
EXEC master..xp_cmdshell @cmd

-- Execute the FTP command via script file.
select @cmd = 'ftp -i -s:' + @tempdir + @workfile
select @cmd
create table #a (id int identity(1,1), s varchar(1000))
insert #a
EXEC master..xp_cmdshell @cmd
select id, ouputtmp = s from #a

-- Clean up.
drop table #a
select @cmd = 'del ' + @tempdir + @workfile
select @cmd
EXEC master..xp_cmdshell @cmd;

--exec sp_configure 'xp_cmdshell', 0;
--reconfigure;
--exec sp_configure 'show advanced options', 0;
--reconfigure;
