USE [QOLAProductionFinals]
GO

/****** Object:  UserDefinedTableType [dbo].[new_tbl_postfall_clinial_monitoring_details_a1_Type]    Script Date: 9/5/2018 10:13:58 PM ******/
CREATE TYPE [dbo].[new_tbl_postfall_clinial_monitoring_details_a1_Type] AS TABLE(
	[linkid] [int] NULL,
	[tableid] [int] NULL,
	[guid] [nvarchar](50) NULL,
	[category] [nvarchar](50) NULL,
	[firstcheck] [nvarchar](50) NULL,
	[onehourfirstcheck] [nvarchar](50) NULL,
	[onehoursecondcheck] [nvarchar](50) NULL,
	[threehoursfirstcheck] [nvarchar](50) NULL,
	[threehourssecondcheck] [nvarchar](50) NULL,
	[threehoursthirdcheck] [nvarchar](50) NULL,
	[fourtyeighthoursfirstcheck] [nvarchar](50) NULL,
	[fourtyeighthourssecondcheck] [nvarchar](50) NULL,
	[fourtyeighthoursthirdcheck] [nvarchar](50) NULL,
	[fourtyeighthoursfourthcheck] [nvarchar](50) NULL,
	[fourtyeighthoursfifthcheck] [nvarchar](50) NULL
)
GO


