BEGIN TRANSACTION;

--ALTER TABLE dbo.[tbl_ObjectOwners] ADD
--	[CorrectCommand]  AS (case when [Type]='Database Owner' then ((('EXEC (''USE ['+[DatabaseName])+']; EXEC dbo.sp_changedbowner @loginame = N''''sa'''', @map = false;'') AT [')+[LinkedServer])+'] ' else '' end)
--;
--ALTER TABLE dbo.[tbl_ObjectOwners] SET (LOCK_ESCALATION = TABLE)
--;
COMMIT
;