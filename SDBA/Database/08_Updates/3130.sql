BEGIN TRANSACTION;

ALTER TABLE dbo.tbl_Servers ADD
	[WindowsVersion] [nvarchar](255) NULL
;
ALTER TABLE dbo.tbl_Servers SET (LOCK_ESCALATION = TABLE)
;
COMMIT
;