USE [QOLAProductionFinals]
GO

/****** Object:  StoredProcedure [dbo].[sp_add_new_tbl_postfall_clinial_monitoring_details_a1]    Script Date: 9/5/2018 10:16:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_add_new_tbl_postfall_clinial_monitoring_details_a1]
(
@new_tbl_postfall_clinial_monitoring_details_a1_Type new_tbl_postfall_clinial_monitoring_details_a1_Type READONLY
,@new_tbl_postfall_clinial_monitoring_details_a2_Type new_tbl_postfall_clinial_monitoring_details_a1_Type READONLY
,@new_tbl_postfall_clinial_monitoring_details_a3_Type new_tbl_postfall_clinial_monitoring_details_a1_Type READONLY

,@part nvarchar(50)
,@linkid int
) 
as    
Begin   
DECLARE @tempId INT

INSERT INTO [dbo].[new_tbl_postfall_clinial_monitoring_part]
           ([pf_clinical_monitoring_part])
		   VALUES
		   (
		   @part
		   )
SELECT @tempId = SCOPE_IDENTITY()
INSERT INTO [dbo].[new_tbl_postfall_clinial_monitoring_details_a1]
           ([linkid]
		   ,[tableid]
		   ,[guid]
           ,[category]
           ,[firstcheck]
           ,[onehourfirstcheck]
           ,[onehoursecondcheck]
           ,[threehoursfirstcheck]
           ,[threehourssecondcheck]
           ,[threehoursthirdcheck]
           ,[fourtyeighthoursfirstcheck]
           ,[fourtyeighthourssecondcheck]
           ,[fourtyeighthoursthirdcheck]
           ,[fourtyeighthoursfourthcheck]
           ,[fourtyeighthoursfifthcheck])
     select 
           linkid
		   ,tableid
		   ,guid
           ,category
		   ,firstcheck
		   ,onehourfirstcheck
		   ,onehoursecondcheck
		   ,threehoursfirstcheck
		   ,threehourssecondcheck
		   ,threehoursthirdcheck
		   ,fourtyeighthoursfirstcheck
		   ,fourtyeighthourssecondcheck
		   ,fourtyeighthoursthirdcheck
		   ,fourtyeighthoursfourthcheck
		   ,fourtyeighthoursfifthcheck 
		   FROM @new_tbl_postfall_clinial_monitoring_details_a1_Type

		   INSERT INTO [dbo].[new_tbl_postfall_clinial_monitoring_details_a1]
           ([linkid]
		   ,[tableid]
		   ,[guid]
           ,[category]
           ,[firstcheck]
           ,[onehourfirstcheck]
           ,[onehoursecondcheck]
           ,[threehoursfirstcheck]
           ,[threehourssecondcheck]
           ,[threehoursthirdcheck]
           ,[fourtyeighthoursfirstcheck]
           ,[fourtyeighthourssecondcheck]
           ,[fourtyeighthoursthirdcheck]
           ,[fourtyeighthoursfourthcheck]
           ,[fourtyeighthoursfifthcheck])
     select 
           linkid
		   ,tableid
		   ,guid
           ,category
		   ,firstcheck
		   ,onehourfirstcheck
		   ,onehoursecondcheck
		   ,threehoursfirstcheck
		   ,threehourssecondcheck
		   ,threehoursthirdcheck
		   ,fourtyeighthoursfirstcheck
		   ,fourtyeighthourssecondcheck
		   ,fourtyeighthoursthirdcheck
		   ,fourtyeighthoursfourthcheck
		   ,fourtyeighthoursfifthcheck 
		   FROM @new_tbl_postfall_clinial_monitoring_details_a2_Type

		   INSERT INTO [dbo].[new_tbl_postfall_clinial_monitoring_details_a1]
           ([linkid]
		   ,[tableid]
		   ,[guid]
           ,[category]
           ,[firstcheck]
           ,[onehourfirstcheck]
           ,[onehoursecondcheck]
           ,[threehoursfirstcheck]
           ,[threehourssecondcheck]
           ,[threehoursthirdcheck]
           ,[fourtyeighthoursfirstcheck]
           ,[fourtyeighthourssecondcheck]
           ,[fourtyeighthoursthirdcheck]
           ,[fourtyeighthoursfourthcheck]
           ,[fourtyeighthoursfifthcheck])
     select 
           linkid
		   ,tableid
		   ,guid
           ,category
		   ,firstcheck
		   ,onehourfirstcheck
		   ,onehoursecondcheck
		   ,threehoursfirstcheck
		   ,threehourssecondcheck
		   ,threehoursthirdcheck
		   ,fourtyeighthoursfirstcheck
		   ,fourtyeighthourssecondcheck
		   ,fourtyeighthoursthirdcheck
		   ,fourtyeighthoursfourthcheck
		   ,fourtyeighthoursfifthcheck 
		   FROM @new_tbl_postfall_clinial_monitoring_details_a3_Type

		 UPDATE [new_tbl_postfall_clinial_monitoring_details_a1]
		 SET linkid=@tempId
		 where linkid=@linkid


		 return @tempid;

		 
End 
GO


