USE [QOLAProductionFinals]
GO

/****** Object:  Table [dbo].[new_tbl_postfall_clinial_monitoring_details_a1]    Script Date: 9/5/2018 10:10:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_postfall_clinial_monitoring_details_a1](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[linkid] [int] NOT NULL,
	[tableid] [int] NULL,
	[guid] [varchar](50) NULL,
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
	[fourtyeighthoursfifthcheck] [nvarchar](50) NULL,
	[created_at] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[new_tbl_postfall_clinial_monitoring_details_a1] ADD  CONSTRAINT [DF_new_tbl_vitalsigns_1_created_at]  DEFAULT (getdate()) FOR [created_at]
GO


