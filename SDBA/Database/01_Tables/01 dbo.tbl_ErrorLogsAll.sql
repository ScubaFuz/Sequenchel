SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ErrorLogsAll]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_ErrorLogsAll](
		[ErrorID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NULL,
		[LogDate] [datetime] NOT NULL CONSTRAINT [DF_tbl_ErrorLogsAll_LogDate]  DEFAULT (getdate()),
		[LogText] [varchar](4000) NULL
	) ON [PRIMARY]
END;
