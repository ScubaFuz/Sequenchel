SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Jobs]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_Jobs](
		[JobID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NOT NULL,
		[JobName] [nvarchar](255) NULL,
		[JobOwner] [nvarchar](255) NULL,
		[TimeRun] [datetime2](7) NULL,
		[JobStatus] [nvarchar](20) NULL,
		[JobOutcome] [nvarchar](20) NULL,
		[DateStart] [smalldatetime] NOT NULL DEFAULT (getdate()),
		[DateStop] [smalldatetime] NULL
	) ON [PRIMARY]
END;
