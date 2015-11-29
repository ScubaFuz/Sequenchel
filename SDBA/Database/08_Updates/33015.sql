BEGIN TRANSACTION;

ALTER TABLE dbo.tbl_Databases ADD
	[RestoreDate] [datetime] NULL,
	[RestoredBy] [nvarchar](128) NULL,
	[RestoreType] [char](1) NULL
;
ALTER TABLE dbo.tbl_DiskSpaces SET (LOCK_ESCALATION = TABLE)
;
COMMIT
;