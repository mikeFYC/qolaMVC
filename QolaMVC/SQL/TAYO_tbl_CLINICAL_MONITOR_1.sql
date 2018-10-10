USE [QOLAProductionFinals]
GO

/****** Object:  Table [dbo].[new_tbl_postfall_clinial_monitoring_part]    Script Date: 9/5/2018 10:12:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_postfall_clinial_monitoring_part](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pf_clinical_monitoring_part] [varchar](50) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK_new_tbl_postfall_clinial_monitoring_part] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[new_tbl_postfall_clinial_monitoring_part] ADD  CONSTRAINT [DF_new_tbl_postfall_clinial_monitoring_part_created_at]  DEFAULT (getdate()) FOR [created_at]
GO


