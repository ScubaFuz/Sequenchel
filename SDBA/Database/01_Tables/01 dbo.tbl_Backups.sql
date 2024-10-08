SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Backups]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_Backups](
		[BackupID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[ServerName] [nvarchar](255) NULL,
		[DatabaseName] [nvarchar](255) NULL,
		[RecoveryModel] [nvarchar](15) NULL,
		[BackupType] [nvarchar](8) NULL,
		[DateStart] [smalldatetime] NOT NULL CONSTRAINT [DF_tbl_Backups_DateStart]  DEFAULT (getdate()),
		[DateStop] [smalldatetime] NULL,
		[LastBackup] [smalldatetime] NULL,
		[Location] [nvarchar](255) NULL,
		[Comment] [nvarchar](50) NULL
	) ON [PRIMARY]
END;
