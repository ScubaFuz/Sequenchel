BEGIN TRANSACTION;

EXECUTE sp_rename N'dbo.tbl_DiskSpaces.FreeSpace', N'FreeSpaceMB', 'COLUMN';

ALTER TABLE dbo.tbl_DiskSpaces ADD
	[LogicalName] [nvarchar](128) NULL,
	[file_system_type] [nvarchar](128) NULL,
	[TotalSizeMB] [int] NULL,
	[supports_compression] [bit] NULL,
	[supports_alternate_streams] [bit] NULL,
	[supports_sparse_files] [bit] NULL,
	[is_read_only] [bit] NULL,
	[is_compressed] [bit] NULL
;
ALTER TABLE dbo.tbl_DiskSpaces SET (LOCK_ESCALATION = TABLE)
;
COMMIT
;