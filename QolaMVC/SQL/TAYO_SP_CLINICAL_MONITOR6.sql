USE [QOLAProductionFinals]
GO

/****** Object:  StoredProcedure [dbo].[sp_update_new_tbl_postfall_clinial_monitoring_details_a1]    Script Date: 9/5/2018 10:19:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_update_new_tbl_postfall_clinial_monitoring_details_a1]
(
@a1_Type new_tbl_postfall_clinial_monitoring_details_a1_Type READONLY
,@a2_Type new_tbl_postfall_clinial_monitoring_details_a1_Type READONLY
,@a3_Type new_tbl_postfall_clinial_monitoring_details_a1_Type READONLY
 ,@tempid varchar(50)
) 
as    
Begin   
	 UPDATE [new_tbl_postfall_clinial_monitoring_details_a1]
		 SET 
           [firstcheck]=r.firstcheck
           ,[onehourfirstcheck]=r.onehourfirstcheck
           ,[onehoursecondcheck]=r.onehoursecondcheck
           ,[threehoursfirstcheck]=r.threehoursfirstcheck
           ,[threehourssecondcheck]=r.threehourssecondcheck
           ,[threehoursthirdcheck]=r.threehoursthirdcheck
           ,[fourtyeighthoursfirstcheck]=r.fourtyeighthoursfirstcheck
           ,[fourtyeighthourssecondcheck]=r.fourtyeighthourssecondcheck
           ,[fourtyeighthoursthirdcheck]=r.fourtyeighthoursthirdcheck
           ,[fourtyeighthoursfourthcheck]=r.fourtyeighthoursfourthcheck
           ,[fourtyeighthoursfifthcheck]=r.fourtyeighthoursfifthcheck
		 from new_tbl_postfall_clinial_monitoring_details_a1 l
		join @a1_Type r on r.category=l.category and r.linkid=l.linkid and r.tableid=l.tableid
		 --Where new_tbl_postfall_clinial_monitoring_details_a1.guid = r.guid

		 UPDATE [new_tbl_postfall_clinial_monitoring_details_a1]
		 SET 
           [firstcheck]=r.firstcheck
           ,[onehourfirstcheck]=r.onehourfirstcheck
           ,[onehoursecondcheck]=r.onehoursecondcheck
           ,[threehoursfirstcheck]=r.threehoursfirstcheck
           ,[threehourssecondcheck]=r.threehourssecondcheck
           ,[threehoursthirdcheck]=r.threehoursthirdcheck
           ,[fourtyeighthoursfirstcheck]=r.fourtyeighthoursfirstcheck
           ,[fourtyeighthourssecondcheck]=r.fourtyeighthourssecondcheck
           ,[fourtyeighthoursthirdcheck]=r.fourtyeighthoursthirdcheck
           ,[fourtyeighthoursfourthcheck]=r.fourtyeighthoursfourthcheck
           ,[fourtyeighthoursfifthcheck]=r.fourtyeighthoursfifthcheck
			From new_tbl_postfall_clinial_monitoring_details_a1 l
		 join @a2_Type r on r.category=l.category and r.linkid=l.linkid and r.tableid=l.tableid

		 UPDATE [new_tbl_postfall_clinial_monitoring_details_a1]
		 SET 
           [firstcheck]=r.firstcheck
           ,[onehourfirstcheck]=r.onehourfirstcheck
           ,[onehoursecondcheck]=r.onehoursecondcheck
           ,[threehoursfirstcheck]=r.threehoursfirstcheck
           ,[threehourssecondcheck]=r.threehourssecondcheck
           ,[threehoursthirdcheck]=r.threehoursthirdcheck
           ,[fourtyeighthoursfirstcheck]=r.fourtyeighthoursfirstcheck
           ,[fourtyeighthourssecondcheck]=r.fourtyeighthourssecondcheck
           ,[fourtyeighthoursthirdcheck]=r.fourtyeighthoursthirdcheck
           ,[fourtyeighthoursfourthcheck]=r.fourtyeighthoursfourthcheck
           ,[fourtyeighthoursfifthcheck]=r.fourtyeighthoursfifthcheck
		 from new_tbl_postfall_clinial_monitoring_details_a1 l
		 join @a3_Type r on r.category=l.category and r.linkid=l.linkid and r.tableid=l.tableid


		 return @tempid;
End 
GO


