USE [QOLAProductionFinals]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_new_tbl_suite]    Script Date: 24/07/2018 09:31:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_add_new_tbl_allergy]
(
@allergy_name varchar(100),
@category varchar(50)
)   
as    
Begin   
INSERT INTO [dbo].[new_tbl_allergy]
           ([allergy_name]
		   ,category)
     VALUES
           (@allergy_name
		   ,@category)
	     
End 


GO
/****** Object:  StoredProcedure [dbo].[sp_add_new_tbl_suite]    Script Date: 24/07/2018 09:31:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_delete_new_tbl_allergy]
(
@id int
)   
as    
Begin   
DELETE FROM [dbo].[new_tbl_allergy]
WHERE
[id]=@id
End


GO
/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_suite]    Script Date: 24/07/2018 09:35:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_by_id_new_tbl_allergy]
(
@id int
)  
as    
Begin    
    select *    
    from new_tbl_allergy a 
	where a.id=@id
End 


GO
/****** Object:  StoredProcedure [dbo].[sp_get_new_tbl_suite]    Script Date: 24/07/2018 09:36:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_new_tbl_allergy]
/*(
@layer_id int,
@ref_id int 
)  */ 
as    
Begin    
    select *    
    from new_tbl_allergy a 
	/*where a.layer_id=@layer_id and a.ref_id=@ref_id   */
End 

GO
/****** Object:  StoredProcedure [dbo].[sp_update_new_tbl_suite]    Script Date: 24/07/2018 09:36:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_update_new_tbl_allergy]
(
@ID int,
@allergy_name varchar(100),
@category varchar(50)
)   
as    
Begin   
UPDATE [dbo].[new_tbl_allergy]
SET
[allergy_name]=@allergy_name,
[category]=@category

WHERE
[id]=@ID

End 


GO
/****** Object:  StoredProcedure [dbo].[sp_add_new_tbl_suite]    Script Date: 24/07/2018 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_add_new_tbl_suite]
(
@Home varchar(200),
@suite_no int,
@floor_no int,
@no_of_rooms int
)   
as    
Begin   
INSERT INTO [dbo].[new_tbl_suite]
           ([Home]
           ,[suite_no]
           ,[floor_no]
           ,[no_of_rooms])
     VALUES
           (@Home
           , @suite_no
           ,@floor_no
           ,@no_of_rooms)
		     
End 

GO
/****** Object:  StoredProcedure [dbo].[sp_update_new_tbl_suite]    Script Date: 24/07/2018 11:42:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_update_new_tbl_suite]
(
@ID int,
@Home varchar(200),
@suite_no int,
@floor_no int,
@no_of_rooms int
)   
as    
Begin   
UPDATE [dbo].[new_tbl_suite]
SET
[Home]=@Home
,[suite_no]=@suite_no
,[floor_no]=@floor_no
,[no_of_rooms]=@no_of_rooms
WHERE
[id]=@ID

End 

GO
/****** Object:  StoredProcedure [dbo].[sp_get_new_tbl_suite]    Script Date: 24/07/2018 11:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_new_tbl_suite]
/*(
@layer_id int,
@ref_id int 
)  */ 
as    
Begin    
    select *    
    from new_tbl_suite a 
	/*where a.layer_id=@layer_id and a.ref_id=@ref_id   */
End 

GO
/****** Object:  StoredProcedure [dbo].[sp_get_by_id_new_tbl_suite]    Script Date: 24/07/2018 11:43:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_by_id_new_tbl_suite]
(
@id int
)  
as    
Begin    
    select *    
    from new_tbl_suite a 
	where a.id=@id
End 

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_new_tbl_suite]    Script Date: 24/07/2018 11:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_delete_new_tbl_suite]
(
@id int
)   
as    
Begin   
DELETE FROM [dbo].[new_tbl_suite]
WHERE
[id]=@id
End 