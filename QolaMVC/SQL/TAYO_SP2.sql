USE [QOLAProductionFinals]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_suite]    Script Date: 24/07/2018 17:11:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_by_column_new_tbl_suite]
(
@column varchar(50),
@value varchar(50)
)  
as    
Begin    
if(@column='*')
    select *    
    from new_tbl_suite a

	else if(@column='0')
	select *    
    from new_tbl_suite a 
	where suite_no=@value

	else if(@column='1')
	select *    
    from new_tbl_suite a 
	where floor_no=@value
End 

GO
/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_allergy]    Script Date: 24/07/2018 18:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_by_column_new_tbl_allergy]
(
@column varchar(50),
@value varchar(50)
)  
as    
Begin    
if(@column='*')
    select *    
    from new_tbl_allergy a

	else if(@column='0')
	select *    
    from new_tbl_allergy a 
	where allergy_name=@value

	else if(@column='1')
	select *    
    from new_tbl_allergy a 
	where category=@value
End 



GO
/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_allergy]    Script Date: 24/07/2018 18:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_by_column_new_tbl_special_diet]
(
@column varchar(50),
@value varchar(50)
)  
as    
Begin    
if(@column='*')
    select *    
    from new_tbl_special_diet a

	else if(@column='0')
	select *    
    from new_tbl_special_diet a 
	where name=@value
End 



GO
/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_allergy]    Script Date: 24/07/2018 18:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_by_column_new_tbl_venue]
(
@column varchar(50),
@value varchar(50)
)  
as    
Begin    
if(@column='*')
    select *    
    from new_tbl_venue a

	else if(@column='0')
	select *    
    from new_tbl_venue a 
	where home=@value

	else if(@column='1')
	select *    
    from new_tbl_venue a 
	where code=@value

	else if(@column='2')
	select *    
    from new_tbl_venue a 
	where venue=@value

End 

GO
/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_allergy]    Script Date: 24/07/2018 18:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_by_column_new_tbl_dine_time]
(
@column varchar(50),
@value varchar(50)
)  
as    
Begin    
if(@column='*')
    select *    
    from new_tbl_dine_time a

	else if(@column='0')
	select *    
    from new_tbl_dine_time a 
	where dinetime=@value

	else if(@column='1')
	select *    
    from new_tbl_dine_time a 
	where shortname=@value

End 
