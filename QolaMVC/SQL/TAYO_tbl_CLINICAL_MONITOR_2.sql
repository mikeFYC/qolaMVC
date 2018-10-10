USE [QOLAProductionFinals]
GO

/****** Object:  Table [dbo].[new_tbl_postfallpartA_page2]    Script Date: 9/5/2018 10:13:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[new_tbl_postfallpartA_page2](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[linkid] [int] NULL,
	[c_longsound_normal] [nvarchar](50) NULL,
	[c_longsound_describe] [nvarchar](50) NULL,
	[c_longsound_equal] [nvarchar](50) NULL,
	[c_c] [nvarchar](50) NULL,
	[c_c_other] [nvarchar](50) NULL,
	[c_heartsound] [nvarchar](50) NULL,
	[c_heartsound_describe] [nvarchar](50) NULL,
	[a_soft] [nvarchar](50) NULL,
	[a_soft_describe] [nvarchar](50) NULL,
	[a_pful] [nvarchar](50) NULL,
	[a_pful_describe] [nvarchar](50) NULL,
	[a_bowelsound] [nvarchar](50) NULL,
	[a_lastbowel_date] [nvarchar](50) NULL,
	[a_voidingnormal] [nvarchar](50) NULL,
	[a_voidingnormal_describe] [nvarchar](50) NULL,
	[a_voidingnormal1] [nvarchar](50) NULL,
	[a_voidingnormal_pads] [nvarchar](50) NULL,
	[a_voidingnormal2] [nvarchar](50) NULL,
	[edema_feet_normal] [nvarchar](50) NULL,
	[edema_feet_describe] [nvarchar](50) NULL,
	[edema_feet1] [nvarchar](50) NULL,
	[edema_hands_normal] [nvarchar](50) NULL,
	[edema_hands_describe] [nvarchar](50) NULL,
	[edema_hands1] [nvarchar](50) NULL,
	[edema_other] [nvarchar](50) NULL,
	[edema_other_describe] [nvarchar](50) NULL,
	[skin_feet] [nvarchar](50) NULL,
	[skin_feet_describe] [nvarchar](50) NULL,
	[skin_rashes] [nvarchar](50) NULL,
	[skin_redness] [nvarchar](50) NULL,
	[skin_bruising] [nvarchar](50) NULL,
	[skin_openareas] [nvarchar](50) NULL,
	[skin_desc_abnormal] [nvarchar](50) NULL,
	[skin_wounddressing] [nvarchar](50) NULL,
	[skin_desc] [nvarchar](50) NULL,
	[p_residentp] [nvarchar](50) NULL,
	[p_residentp_desc] [nvarchar](50) NULL,
	[p_pscale] [nvarchar](50) NULL,
	[p_aching] [nvarchar](50) NULL,
	[p_sharp] [nvarchar](50) NULL,
	[p_dull] [nvarchar](50) NULL,
	[p_radiating] [nvarchar](50) NULL,
	[p_where] [nvarchar](50) NULL,
	[p_whatmakes_better] [nvarchar](50) NULL,
	[p_whatmakes_worst] [nvarchar](50) NULL,
	[p_interface_adl] [nvarchar](50) NULL,
	[p_describe] [nvarchar](50) NULL,
	[p_other] [nvarchar](50) NULL,
	[completed_by] [nvarchar](50) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_new_tbl_postfallpartA_page2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[new_tbl_postfallpartA_page2] ADD  CONSTRAINT [DF_new_tbl_postfallpartA_page2_created_at]  DEFAULT (getdate()) FOR [create_date]
GO


