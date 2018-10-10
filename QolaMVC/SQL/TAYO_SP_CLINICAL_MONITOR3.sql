USE [QOLAProductionFinals]
GO

/****** Object:  StoredProcedure [dbo].[sp_add_new_tbl_postfall_partAPage2]    Script Date: 9/5/2018 10:17:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_add_new_tbl_postfall_partAPage2]
(
			@linkid int=null
           ,@c_longsound_normal nvarchar(50)=null
           ,@c_longsound_describe nvarchar(50)=null
           ,@c_longsound_equal nvarchar(50)=null
              ,@c_c nvarchar(50)=null
           ,@c_c_other nvarchar(50)=null
           ,@c_heartsound nvarchar(50)=null
           ,@c_heartsound_describe nvarchar(50)=null
           ,@a_soft nvarchar(50)=null
           ,@a_soft_describe nvarchar(50)=null
           ,@a_pful nvarchar(50)=null
           ,@a_pful_describe nvarchar(50)=null
           ,@a_bowelsound nvarchar(50)=null
           ,@a_lastbowel_date nvarchar(50)=null
           ,@a_voidingnormal nvarchar(50)=null
           ,@a_voidingnormal_describe nvarchar(50)=null
           ,@a_voidingnormal1 nvarchar(50)=null
           ,@a_voidingnormal_pads nvarchar(50)=null
           ,@a_voidingnormal2 nvarchar(50)=null
           ,@edema_feet_normal nvarchar(50)=null
           ,@edema_feet_describe nvarchar(50)=null
           ,@edema_feet1 nvarchar(50)=null
           ,@edema_hands_normal nvarchar(50)=null
           ,@edema_hands_describe nvarchar(50)=null
           ,@edema_hands1 nvarchar(50)=null
           ,@edema_other nvarchar(50)=null
           ,@edema_other_describe nvarchar(50)=null
           ,@skin_feet nvarchar(50)=null
           ,@skin_feet_describe nvarchar(50)=null
           ,@skin_rashes nvarchar(50)=null
           ,@skin_redness nvarchar(50)=null
           ,@skin_bruising nvarchar(50)=null
            ,@skin_openareas nvarchar(50)=null
           ,@skin_desc_abnormal nvarchar(50)=null
           ,@skin_wounddressing nvarchar(50)=null
           ,@skin_desc nvarchar(50)=null
           ,@p_residentp nvarchar(50)=null
           ,@p_residentp_desc nvarchar(50)=null
           ,@p_pscale nvarchar(50)=null
           ,@p_aching nvarchar(50)=null
           ,@p_sharp nvarchar(50)=null
           ,@p_dull nvarchar(50)=null
           ,@p_radiating nvarchar(50)=null
           ,@p_where nvarchar(50)=null
           ,@p_whatmakes_better nvarchar(50)=null
           ,@p_whatmakes_worst nvarchar(50)=null
           ,@p_interface_adl nvarchar(50)=null
           ,@p_describe nvarchar(50)=null
           ,@p_other nvarchar(50)=null
           ,@completed_by nvarchar(50)=null
) 
as    
Begin   
   if exists(Select * From new_tbl_postfallpartA_page2 Where linkid = @linkid)
      BEGIN
	     update new_tbl_postfallpartA_page2 
		 set 
		 c_longsound_normal = @c_longsound_normal
		 ,c_longsound_describe=@c_longsound_describe
		 ,c_longsound_equal=@c_longsound_equal
		 ,c_c=@c_c
		  ,c_c_other=@c_c_other
 		 ,c_heartsound=@c_heartsound
		 ,c_heartsound_describe=@c_heartsound_describe
		 ,a_soft=@a_soft
		 ,a_soft_describe=@a_soft_describe
		 ,a_pful=@a_pful
		 ,a_pful_describe=@a_pful_describe
		 ,a_bowelsound=@a_bowelsound
		 ,a_lastbowel_date=@a_lastbowel_date
		 ,a_voidingnormal=@a_voidingnormal
		 ,a_voidingnormal_describe=@a_voidingnormal_describe
		 ,a_voidingnormal1=@a_voidingnormal1


		 ,a_voidingnormal_pads = @a_voidingnormal_pads
		 ,a_voidingnormal2=@a_voidingnormal2
		 ,edema_feet_normal=@edema_feet_normal
		 ,edema_feet_describe=@edema_feet_describe
		  ,edema_feet1=@edema_feet1
 		 ,edema_hands_normal=@edema_hands_normal
		 ,edema_hands_describe=@edema_hands_describe
		 ,edema_hands1=@edema_hands1
		 ,edema_other=@edema_other
		 ,edema_other_describe=@edema_other_describe
		 ,skin_feet=@skin_feet
		 ,skin_feet_describe=@skin_feet_describe
		 ,skin_rashes=@skin_rashes
		 ,skin_redness=@skin_redness

		     ,[skin_bruising]=@skin_bruising
             ,[skin_openareas]=@skin_openareas
               ,[skin_desc_abnormal]=@skin_desc_abnormal
            ,[skin_wounddressing]=@skin_wounddressing
               ,[skin_desc]=@skin_desc
            ,[p_residentp]=@p_residentp
               ,[p_residentp_desc]=@p_residentp_desc
           ,[p_pscale]=@p_pscale
              ,[p_aching]=@p_aching
              ,[p_sharp]=@p_sharp
             ,[p_dull]=@p_dull
             ,[p_radiating]=@p_radiating
             ,[p_where]=@p_where
             ,[p_whatmakes_better]=@p_whatmakes_better
             ,[p_whatmakes_worst]=@p_whatmakes_worst
               ,[p_interface_adl]=@p_interface_adl
             ,[p_describe]=@p_describe
             ,[p_other]=@p_other
              ,[completed_by]=@completed_by


		 where linkid = @linkid
      
 	  END

	  ELSE
	  BEGIN
INSERT INTO [dbo].[new_tbl_postfallpartA_page2]
           ([linkid]
           ,[c_longsound_normal]
           ,[c_longsound_describe]
           ,[c_longsound_equal]
           ,[c_c]
            ,[c_c_other]
           ,[c_heartsound]
           ,[c_heartsound_describe]
           ,[a_soft]
           ,[a_soft_describe]
           ,[a_pful]
           ,[a_pful_describe]
           ,[a_bowelsound]
           ,[a_lastbowel_date]
           ,[a_voidingnormal]
           ,[a_voidingnormal_describe]
           ,[a_voidingnormal1]
           ,[a_voidingnormal_pads]
           ,[a_voidingnormal2]
           ,[edema_feet_normal]
           ,[edema_feet_describe]
           ,[edema_feet1]
           ,[edema_hands_normal]
           ,[edema_hands_describe]
           ,[edema_hands1]
           ,[edema_other]
           ,[edema_other_describe]
           ,[skin_feet]
           ,[skin_feet_describe]
           ,[skin_rashes]
           ,[skin_redness]
           ,[skin_bruising]
           ,[skin_openareas]
           ,[skin_desc_abnormal]
           ,[skin_wounddressing]
           ,[skin_desc]
           ,[p_residentp]
           ,[p_residentp_desc]
           ,[p_pscale]
           ,[p_aching]
           ,[p_sharp]
           ,[p_dull]
           ,[p_radiating]
           ,[p_where]
           ,[p_whatmakes_better]
           ,[p_whatmakes_worst]
           ,[p_interface_adl]
           ,[p_describe]
           ,[p_other]
           ,[completed_by])
		   VALUES
		   (
			@linkid
           ,@c_longsound_normal
           ,@c_longsound_describe
           ,@c_longsound_equal
           ,@c_c
           ,@c_c_other
           ,@c_heartsound
           ,@c_heartsound_describe
           ,@a_soft
           ,@a_soft_describe
           ,@a_pful
           ,@a_pful_describe
           ,@a_bowelsound
           ,@a_lastbowel_date
           ,@a_voidingnormal
           ,@a_voidingnormal_describe
           ,@a_voidingnormal1
           ,@a_voidingnormal_pads
           ,@a_voidingnormal2
           ,@edema_feet_normal
           ,@edema_feet_describe
           ,@edema_feet1
           ,@edema_hands_normal
           ,@edema_hands_describe
           ,@edema_hands1
           ,@edema_other
           ,@edema_other_describe
           ,@skin_feet
           ,@skin_feet_describe
           ,@skin_rashes
           ,@skin_redness
           ,@skin_bruising
           ,@skin_openareas
           ,@skin_desc_abnormal
           ,@skin_wounddressing
           ,@skin_desc
           ,@p_residentp
           ,@p_residentp_desc
           ,@p_pscale
           ,@p_aching
           ,@p_sharp
           ,@p_dull
           ,@p_radiating
           ,@p_where
           ,@p_whatmakes_better
           ,@p_whatmakes_worst
           ,@p_interface_adl
           ,@p_describe
           ,@p_other
           ,@completed_by
		   )
End
End 
GO


