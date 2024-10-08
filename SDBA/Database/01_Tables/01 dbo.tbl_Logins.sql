SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Logins]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_Logins](
		[LoginID] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		[LinkedServer] [nvarchar](255) NOT NULL,
		[uid] [int] NULL,
		[sid] [varbinary](85) NULL,
		[UserName] [nvarchar](255) NULL,
		[DefaultDB] [nvarchar](255) NULL,
		[denylogin] [int] NULL,
		[hasaccess] [int] NULL,
		[isntname] [int] NULL,
		[sysadmin] [int] NULL,
		[securityadmin] [int] NULL,
		[serveradmin] [int] NULL,
		[setupadmin] [int] NULL,
		[processadmin] [int] NULL,
		[diskadmin] [int] NULL,
		[dbcreator] [int] NULL,
		[bulkadmin] [int] NULL,
		[principal_id] [int] NULL,
		[type_desc] [nvarchar](60) NULL,
		[is_disabled] [bit] NULL,
		[hasdbaccess] [int] NULL,
		[DataBaseName] [nvarchar](255) NULL,
		[default_schema_name] [nvarchar](255) NULL,
		[RoleName] [nvarchar](1000) NULL,
		[DateStart] [smalldatetime] NOT NULL DEFAULT (getdate()),
		[DateStop] [smalldatetime] NULL
	) ON [PRIMARY]
END;
