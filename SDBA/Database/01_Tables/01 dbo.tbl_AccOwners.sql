SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AccOwners]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_AccOwners](
		[AccOwnerID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NOT NULL,
		[DatabaseName] [nvarchar](255) NOT NULL,
		[ObjectName] [nvarchar](255) NOT NULL,
		[Owner] [nvarchar](255) NOT NULL,
		[DateStart] [smalldatetime] NOT NULL CONSTRAINT [DF_tbl_AccOwners_DateStart]  DEFAULT (getdate()),
		[DateStop] [smalldatetime] NULL
	) ON [PRIMARY]
END;
