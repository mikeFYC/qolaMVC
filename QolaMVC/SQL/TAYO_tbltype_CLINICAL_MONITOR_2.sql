USE [QOLAProductionFinals]
GO

/****** Object:  UserDefinedTableType [dbo].[new_tbl_postfall_clinial_monitoring_details_a2_Type]    Script Date: 9/5/2018 10:15:11 PM ******/
CREATE TYPE [dbo].[new_tbl_postfall_clinial_monitoring_details_a2_Type] AS TABLE(
	[linkid] [int] NOT NULL,
	[tableid] [int] NULL,
	[guid] [nvarchar](50) NULL,
	[category] [nvarchar](50) NULL,
	[firstcheck] [nvarchar](50) NULL,
	[fourhourfirstcheck] [nvarchar](50) NULL,
	[fourhoursecondcheck] [nvarchar](50) NULL,
	[fourhoursthirdcheck] [varchar](50) NULL,
	[fourhoursforthcheck] [nvarchar](50) NULL,
	[threehoursfirstcheck] [nvarchar](50) NULL,
	[threehourssecondcheck] [nvarchar](50) NULL,
	[threehoursthirdcheck] [nvarchar](50) NULL,
	[threehoursforthcheck] [nvarchar](50) NULL,
	[threehoursfifthcheck] [nvarchar](50) NULL,
	[threehourssixthcheck] [nvarchar](50) NULL
)
GO


