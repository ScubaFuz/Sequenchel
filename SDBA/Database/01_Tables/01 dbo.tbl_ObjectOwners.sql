SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ObjectOwners]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_ObjectOwners](
		[ObjectOwnerID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NOT NULL,
		[DatabaseName] [nvarchar](255) NOT NULL,
		[ObjectName] [nvarchar](255) NOT NULL,
		[Owner] [nvarchar](255) NOT NULL,
		[Type] nvarchar(20) NULL,
		[DateStart] [smalldatetime] NOT NULL CONSTRAINT [DF_tbl_ObjectOwners_DateStart]  DEFAULT (getdate()),
		[DateStop] [smalldatetime] NULL
	) ON [PRIMARY]
END;
