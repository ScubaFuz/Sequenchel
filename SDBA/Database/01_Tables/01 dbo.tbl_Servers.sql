SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Config]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_Servers](
		[ServerID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NULL,
		[LocalServerName] [nvarchar](255) NULL,
		[DataSource] [nvarchar](255) NULL,
		[PromotionOn] [bit] NULL,
		[MachineName] [nvarchar](255) NULL,
		[Domain] [nvarchar](255) NULL,
		[InstanceName] [nvarchar](255) NULL,
		[Edition] [nvarchar](255) NULL,
		[SQLServerComponents] [nvarchar](255) NULL,
		[SQLVersion] [nvarchar](15) NULL,
		[SQLVersionLong] [nvarchar](25) NULL,
		[SQLVersionText] [nvarchar](100) NULL,
		[SQLServerLicenseType] [nvarchar](255) NULL,
		[SPLevel] [nvarchar](15) NULL,
		[Collation] [nvarchar](255) NULL,
		[IsClustered] [bit] NULL,
		[WindowsAuthentication] [bit] NULL,
		[FileStreamEnabled] [bit] NULL,
		[WindowsVersion] [nvarchar](255) NULL,
		[IP_Address] [nvarchar](15) NULL,
		[Port] [int] NULL,
		[Logical_CPU_Count] [int] NULL,
		[Hyperthread_Ratio] [int] NULL,
		[Physical_CPU_Count] [int] NULL,
		[Physical_Memory_MB] [bigint] NULL,
		[MinServerMemory] [bigint] NULL,
		[MaxServerMemory] [bigint] NULL,
		[Owner] [nvarchar](255) NULL,
		[Application] [nvarchar](255) NULL,
		[Location] [nvarchar](255) NULL,
		[Remarks] [nvarchar](255) NULL,
		[MonitorServer] [bit] NULL,
		[MonitorContent] [bit] NULL,
		[AutoAdded] [bit] NULL,
		[Active] [bit] NULL,
		[Available] [bit] NULL,
		[LastFound] [smalldatetime] NULL,
		[DateStart] [smalldatetime] NOT NULL  CONSTRAINT [DF_tbl_Servers_DateStart]  DEFAULT (getdate()),
		[DateStop] [smalldatetime] NULL
	) ON [PRIMARY]
END;
