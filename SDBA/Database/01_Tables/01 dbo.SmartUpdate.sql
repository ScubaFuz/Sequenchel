SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmartUpdate]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[SmartUpdate](
		[SmartUpdateId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
		[DataBaseName] [nvarchar](256) NOT NULL,
		[SchemaName] [nvarchar](256) NOT NULL,
		[TableName] [nvarchar](256) NOT NULL,
		[ColumnName] [nvarchar](256) NOT NULL,
		[DataType] [nvarchar](100) NULL,
		[PrimaryKey] [bit] NOT NULL DEFAULT ((0)),
		[CopyColumn] [bit] NOT NULL DEFAULT ((1)),
		[CompareColumn] [bit] NOT NULL DEFAULT ((0)),
		[DateStart] [date] NOT NULL DEFAULT (GetDate()),
		[DateStop] [date] NULL,
		[Active] [bit] NOT NULL DEFAULT ((1)),
	) ON [PRIMARY]
END;
