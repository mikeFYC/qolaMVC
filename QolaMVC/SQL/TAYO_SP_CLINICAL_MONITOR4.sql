USE [QOLAProductionFinals]
GO

/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_postfall_clinial_monitoring_details_a1]    Script Date: 9/5/2018 10:18:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_get_by_id_new_tbl_postfall_clinial_monitoring_details_a1]
(
@linkid int
--,@table_id int
,@pf_clinical_monitoring_part nvarchar(50)
--,@category nvarchar(50)
--,@cur_date datetime
)  
as    
Begin    
    select *    
    from new_tbl_postfall_clinial_monitoring_part a, new_tbl_postfall_clinial_monitoring_details_a1 b 
	where 
	a.pf_clinical_monitoring_part=@pf_clinical_monitoring_part
	--and a.table_id=@table_id
	--and a.id=@id
	--and b.category=@category
	--and a.created_at=@cur_date
	 and a.id=b.linkid 
	 and b.linkid = @linkid

	     select * from new_tbl_postfall_clinial_monitoring_part a
		 where a.pf_clinical_monitoring_part=@pf_clinical_monitoring_part
  	
End 
  
SET ANSI_NULLS ON
GO


