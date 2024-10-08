SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_DiskSpaces]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_DiskSpaces](
		[DriveID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NULL,
		[Drive] [char](1) NULL,
		[LogicalName] nvarchar(128) NULL,
		[file_system_type] nvarchar(128) NULL,
		[TotalSizeMB] [bigint] NULL,
		[FreeSpaceMB] [bigint] NULL,
		[supports_compression] bit NULL,
		[supports_alternate_streams] bit NULL,
		[supports_sparse_files] bit NULL,
		[is_read_only] bit NULL,
		[is_compressed] bit NULL,
		[DateLogged] [smalldatetime] NOT NULL CONSTRAINT [DF_tbl_DiskSpaces_DateLogged]  DEFAULT (getdate())
	 ) ON [PRIMARY]
END;
