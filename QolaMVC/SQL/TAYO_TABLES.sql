USE [QOLAProductionFinals]
GO

/****** Object:  Table [dbo].[new_tbl_allergy]    Script Date: 24/07/2018 11:45:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_allergy](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[allergy_name] [varchar](100) NULL,
	[category] [varchar](50) NULL,
 CONSTRAINT [PK_new_tbl_allergy] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[new_tbl_dine_time]    Script Date: 24/07/2018 11:45:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_dine_time](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dinetime] [varchar](50) NULL,
	[shortname] [varchar](50) NULL,
 CONSTRAINT [PK_new_tbl_dine_time] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_special_diet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
 CONSTRAINT [PK_new_tbl_special_diet] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_suite](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Home] [varchar](200) NULL,
	[suite_no] [int] NULL,
	[floor_no] [int] NULL,
	[no_of_rooms] [int] NULL,
 CONSTRAINT [PK_new_tbl_suite] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_venue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[home] [varchar](100) NULL,
	[code] [varchar](50) NULL,
	[venue] [varchar](200) NULL,
 CONSTRAINT [PK_new_tbl_venue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

