USE [QOLAProductionFinals]
GO

/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_postfall_clinialpage2parta]    Script Date: 9/5/2018 10:18:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_get_by_id_new_tbl_postfall_clinialpage2parta]
(
@linkid int
)  
as    
Begin    
    select *    
     from new_tbl_postfallpartA_page2 a
	 where 
   	 a.linkid=@linkid
  	
End 
  
SET ANSI_NULLS ON
GO


