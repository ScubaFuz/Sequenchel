BEGIN TRANSACTION;

ALTER TABLE dbo.tbl_Databases ADD
	[LocalID] [int] NULL,
	[DateCreated] [datetime] NULL,
	[CompatibilityLevel] [tinyint] NULL,
	[Collation] [sysname] NULL,
	[IsReadOnly] [bit] NULL,
	[IsStandBy] [bit] NULL,
	[SnapshotIsolation] [nvarchar](60) NULL,
	[ReadCommittedSnapshot] [bit] NULL,
	[PageVerify] [nvarchar](60) NULL,
	[AutoCreateStats] [bit] NULL,
	[AutoUpdateStats] [bit] NULL,
	[AutoUpdateStatsAsync] [bit] NULL,
	[FulltextEnabled] [bit] NULL,
	[BrokerEnabled] [bit] NULL,
	[Application] [nvarchar](255) NULL,
	[Owner] [nvarchar](255) NULL
;
ALTER TABLE dbo.tbl_DiskSpaces SET (LOCK_ESCALATION = TABLE)
;
COMMIT
;