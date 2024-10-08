SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_DataSpaces]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_DataSpaces](
		[DataSpaceID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NOT NULL,
		[DatabaseName] [nvarchar](255) NOT NULL,
		[LogicalName] [nvarchar](255) NOT NULL,
		[FileID] [smallint] NULL,
		[File_Size_MB] [decimal](12, 2) NULL,
		[Space_Used_MB] [decimal](12, 2) NULL,
		[Free_Space_MB] [decimal](12, 2) NULL,
		[Free_Space_Prc] [decimal](12, 2) NULL,
		[Growth] [int] NULL,
		[Perc] [bit] NOT NULL,
		[PercGrowth] [decimal](12, 2) NULL,
		[FileName] [nvarchar](255) NOT NULL,
		[LogDate] [datetime] NOT NULL CONSTRAINT [DF_tbl_DataSpaces_LogDate]  DEFAULT (getdate())
	) ON [PRIMARY]
END;
