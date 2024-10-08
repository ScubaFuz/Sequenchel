SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Databases]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_Databases](
		[DatabaseID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NOT NULL,
		[DatabaseName] [sysname] NOT NULL,
		[LocalID] [int] NULL,
		[DateCreated] [datetime] NULL,
		[CompatibilityLevel] [tinyint] NULL,
		[Collation] [sysname] NULL,
		[UserAccess] [nvarchar](60) NULL,
		[IsReadOnly] [bit] NULL,
		[AutoClose] [bit] NULL,
		[AutoShrink] [bit] NULL,
		[DBStatus] [nvarchar](60) NULL,
		[IsStandBy] [bit] NULL,
		[SnapshotIsolation] [nvarchar](60) NULL,
		[ReadCommittedSnapshot] [bit] NULL,
		[RecoveryModel] [nvarchar](60) NULL,
		[PageVerify] [nvarchar](60) NULL,
		[AutoCreateStats] [bit] NULL,
		[AutoUpdateStats] [bit] NULL,
		[AutoUpdateStatsAsync] [bit] NULL,
		[QuotedIdentifier] [bit] NULL,
		[FulltextEnabled] [bit] NULL,
		[BrokerEnabled] [bit] NULL,
		[RestoreDate] [datetime] NULL,
		[RestoredBy] [nvarchar](128) NULL,
		[RestoreType] [char](1) NULL,
		[Application] [nvarchar](255) NULL,
		[Owner] [nvarchar](255) NULL,
		[DateStart] [smalldatetime] NOT NULL CONSTRAINT [DF_tbl_Databases_DateStart]  DEFAULT (getdate()),
		[DateStop] [smalldatetime] NULL
	) ON [PRIMARY]
END;
