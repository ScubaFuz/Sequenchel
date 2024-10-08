SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Logging]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_Logging](
		[LogID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED  NOT NULL,
		[LogDate] [datetime] NOT NULL CONSTRAINT [DF_tbl_Logging_LogDate]  DEFAULT (getdate()),
		[Logtext] [nvarchar](2000) NULL,
		[ClientPC] [nchar](50) NULL
	) ON [PRIMARY]
END
