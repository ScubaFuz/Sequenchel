SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_IndexList]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_IndexList](
		[IndexID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED  NOT NULL,
		[LinkedServer] [nvarchar](255) NOT NULL,
		[DatabaseName] [nvarchar](255) NOT NULL,
		[SchemaName] [nvarchar](255) NOT NULL,
		[TableObjectID] [int] NULL,
		[TableName] [nvarchar](255) NOT NULL,
		[IndexObjectID] [int] NULL,
		[IndexName] [nvarchar](255) NOT NULL,
		[IndexRows] [int] NULL,
		[IndexLocks] [bit] NULL,
		[OfflineColumns] [int] NULL,
		[FragBefore] [int] NULL,
		[FragAfter] [int] NULL,
		[DateStart] [datetime] NOT NULL CONSTRAINT [DF_tbl_IndexList_DateStart]  DEFAULT (getdate()),
		[DateStop] [datetime] NULL,
		[ProcessStart] [datetime] NULL,
		[ProcessStop] [datetime] NULL
	) ON [PRIMARY]
END;
