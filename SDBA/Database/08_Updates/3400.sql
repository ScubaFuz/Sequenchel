BEGIN TRANSACTION;

ALTER TABLE dbo.tbl_Servers ADD
	[MinServerMemory] [bigint] NULL,
	[MaxServerMemory] [bigint] NULL
;
ALTER TABLE dbo.tbl_Servers SET (LOCK_ESCALATION = TABLE)
;
COMMIT
;