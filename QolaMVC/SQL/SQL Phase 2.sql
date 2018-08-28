
--IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Activity_Category]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
--DROP PROCEDURE [dbo].[spAB_Add_Activity_Category]
--GO
--CREATE PROCEDURE [dbo].[spAB_Add_Activity_Category]
--@CategoryNameEnglish nvarchar(200),
--@CategoryNameFrench nvarchar(200),
--@MemoryCareColor nvarchar(200)
--AS
----20180712 chime created
--BEGIN
--	INSERT INTO tbl_AB_ActivityCategory (CategoryNameEnglish, CategoryNameFrench, MemoryCareColor) VALUES (@CategoryNameEnglish, @CategoryNameFrench, @MemoryCareColor)
--END
--GO



--IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Activity]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
--DROP PROCEDURE [dbo].[spAB_Add_Activity]
--GO
--CREATE PROCEDURE [dbo].[spAB_Add_Activity]
--@CategoryId int,
--@ActivityNameEnglish nvarchar(200),
--@ActivityNameFrench nvarchar(200),
--@ActivityColor nvarchar(200),
--@FunPicture nvarchar(200),
--@Province nvarchar(200),
--@ShowInAssessment bit,
--@ActivityDisplayTitle nvarchar(200)
--AS
----20180712 chime created
--BEGIN
--	INSERT INTO tbl_AB_Activity (CategoryId, ActivityNameEnglish, ActivityNameFrench, ActivityColor, FunPicture, Province, ShowInAssessment, ActivityDisplayTitle) 
--	VALUES (@CategoryId, @ActivityNameEnglish, @ActivityNameFrench, @ActivityColor, @FunPicture, @Province, @ShowInAssessment, @ActivityDisplayTitle)
--END
--GO



--IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_ActivityAssessment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
--DROP PROCEDURE [dbo].[spAB_Add_ActivityAssessment]
--GO
--CREATE PROCEDURE [dbo].[spAB_Add_ActivityAssessment]
--@ActivityId int,
--@IsP bit,
--@IsC bit,
--@IsW bit,
--@ResidentId int,
--@DateEntered datetime
--AS
----20180712 chime created
--BEGIN
--	INSERT INTO tbl_AB_ActivityAssessment (ActivityId, IsP, IsC, IsW, ResidentId, DateEntered ) 
--	VALUES (@ActivityId, @IsP, @IsC, @IsW, @ResidentId, @DateEntered )
--END
--GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_All_Activity_Category]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_All_Activity_Category]
GO
CREATE PROCEDURE [dbo].[spAB_Get_All_Activity_Category]
AS
--20180715 chime created
BEGIN
	SELECT
		Id,
		CategoryNameEnglish,
		CategoryNameFrench,
		MemoryCareColor 
	FROM tbl_AB_ActivityCategory
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_All_Activity]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_All_Activity]
GO
CREATE PROCEDURE [dbo].[spAB_Get_All_Activity]
AS
--20180715 chime created
BEGIN
	SELECT
		A.Id,
		CategoryId = AC.Id,
		CategoryNameFrench = Ac.CategoryNameFrench,
		CategoryNameEnglish = Ac.CategoryNameEnglish,
		ActivityNameEnglish,
		ActivityNameFrench,
		ActivityColor,
		FunPicture,
		Province,
		ShowInAssessment,
		ActivityDisplayTitle
	FROM tbl_AB_Activity A
	LEFT OUTER JOIN tbl_AB_ActivityCategory AC ON
	AC.Id = A.CategoryId
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Activity_By_Category_Id]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Activity_By_Category_Id]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Activity_By_Category_Id]
@CategoryId int
AS
--20180715 chime created
BEGIN
	SELECT
		Id,
		CategoryId,
		ActivityNameEnglish,
		ActivityNameFrench,
		ActivityColor,
		FunPicture,
		Province,
		ShowInAssessment,
		ActivityDisplayTitle
	FROM tbl_AB_Activity
	WHERE CategoryId = @CategoryId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Resident_ActivityAssessment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Resident_ActivityAssessment]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Resident_ActivityAssessment]
@ResidentId int,
@AssessmentId int
AS
--20180715 chime created
BEGIN
	SELECT
		Id,
		AssessmentId,
		ActivityId,
		ActivityCategoryId,
		IsP,
		IsC,
		IsW,
		ResidentId,
		DateEntered
	FROM 
		dbo.[tbl_AB_ActivityAssessment] 
	WHERE 
		ResidentId = @ResidentId 
		AND
		AssessmentId = @AssessmentId
END
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Activity_Category]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Activity_Category]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Activity_Category]
@CategoryNameEnglish nvarchar(200),
@CategoryNameFrench nvarchar(200),
@MemoryCareColor nvarchar(200)
AS
--20180712 chime created
BEGIN
	INSERT INTO tbl_AB_ActivityCategory (CategoryNameEnglish, CategoryNameFrench, MemoryCareColor) VALUES (@CategoryNameEnglish, @CategoryNameFrench, @MemoryCareColor)
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Activity]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Activity]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Activity]
@CategoryId int,
@ActivityNameEnglish nvarchar(200),
@ActivityNameFrench nvarchar(200),
@ActivityColor nvarchar(200),
@FunPicture nvarchar(200),
@Province nvarchar(200),
@ShowInAssessment bit,
@ActivityDisplayTitle nvarchar(200)
AS
--20180712 chime created
BEGIN
	INSERT INTO tbl_AB_Activity (CategoryId, ActivityNameEnglish, ActivityNameFrench, ActivityColor, FunPicture, Province, ShowInAssessment, ActivityDisplayTitle) 
	VALUES (@CategoryId, @ActivityNameEnglish, @ActivityNameFrench, @ActivityColor, @FunPicture, @Province, @ShowInAssessment, @ActivityDisplayTitle)
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_ActivityAssessment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_ActivityAssessment]
GO
CREATE PROCEDURE [dbo].[spAB_Add_ActivityAssessment]
@ActivityId int,
@IsP bit,
@IsC bit,
@IsW bit,
@ResidentId int,
@DateEntered datetime
AS
--20180712 chime created
BEGIN
	INSERT INTO tbl_AB_ActivityAssessment (ActivityId, IsP, IsC, IsW, ResidentId, DateEntered ) 
	VALUES (@ActivityId, @IsP, @IsC, @IsW, @ResidentId, @DateEntered )
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_ActivityAssessmentStore]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_ActivityAssessmentStore]
GO
CREATE PROCEDURE [dbo].[spAB_Add_ActivityAssessmentStore]
@ResidentId int,
@EnteredBy int
AS
--20180718 chime created
BEGIN
	INSERT INTO tbl_AB_ActivityAssessment_Store (ResidentId, EnteredBy ) 
	VALUES (@ResidentId, @EnteredBy )

	SELECT AssessmentStoreId = @@IDENTITY
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_ActivityAssessmentStore]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_ActivityAssessmentStore]
GO
CREATE PROCEDURE [dbo].[spAB_Get_ActivityAssessmentStore]
@ResidentId int
AS
--20180718 chime created
BEGIN
	SELECT Id, ResidentId, EnteredBy, DateEntered FROM tbl_AB_ActivityAssessment_Store where ResidentId=@ResidentId
END
GO




IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_ActivityAssessmentByAssessmentId]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_ActivityAssessmentByAssessmentId]
GO
CREATE PROCEDURE [dbo].[spAB_Get_ActivityAssessmentByAssessmentId]
@AssessmentId int
AS
--20180814 chime created
BEGIN
	SELECT 
		ActivityId,
		A.ActivityDisplayTitle,
		A.ActivityNameEnglish,
		A.ActivityNameFrench,
		A.CategoryId,
		ActivityCategoryId, 
		IsP, 
		IsC, 
		IsW, 
		ResidentId, 
		AssessmentId, 
		DateEntered  
	FROM 
		tbl_AB_ActivityAssessment AA
	LEFT OUTER JOIN 
		tbl_AB_Activity A 
	ON
		AA.ActivityId = A.Id
	WHERE 
		AssessmentId = @AssessmentId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Activity_By_Id]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Activity_By_Id]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Activity_By_Id]
@ActivityId int
AS
--20180828 chime created
BEGIN
	SELECT
		A.Id,
		CategoryId = AC.Id,
		CategoryNameFrench = Ac.CategoryNameFrench,
		CategoryNameEnglish = Ac.CategoryNameEnglish,
		ActivityNameEnglish,
		ActivityNameFrench,
		ActivityColor,
		FunPicture,
		Province,
		ShowInAssessment,
		ActivityDisplayTitle
	FROM tbl_AB_Activity A
	LEFT OUTER JOIN tbl_AB_ActivityCategory AC ON
	AC.Id = A.CategoryId
	WHERE A.Id = @ActivityId
END
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Update_Activity]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Update_Activity]
GO
CREATE PROCEDURE [dbo].[spAB_Update_Activity]
@ActivityId int,
@CategoryId int,
@ActivityNameEnglish nvarchar(200),
@ActivityNameFrench nvarchar(200),
@ActivityColor nvarchar(200),
@FunPicture nvarchar(200),
@Province nvarchar(200),
@ShowInAssessment bit,
@ActivityDisplayTitle nvarchar(200)
AS
--20180828 chime created
BEGIN
	UPDATE tbl_AB_Activity 
	SET 
		CategoryId = @CategoryId, 
		ActivityNameEnglish = @ActivityNameEnglish, 
		ActivityNameFrench = @ActivityNameFrench, 
		ActivityColor = @ActivityColor, 
		FunPicture = @FunPicture, 
		Province = @Province, 
		ShowInAssessment = @ShowInAssessment, 
		ActivityDisplayTitle = @ActivityDisplayTitle
	WHERE
		Id = @ActivityId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Delete_Activity]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Delete_Activity]
GO
CREATE PROCEDURE [dbo].[spAB_Delete_Activity]
@ActivityId int
AS
--20180828 chime created
BEGIN
	DELETE FROM
		tbl_AB_Activity
	WHERE
		Id = @ActivityId
END
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Update_Activity_Category]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Update_Activity_Category]
GO
CREATE PROCEDURE [dbo].[spAB_Update_Activity_Category]
@CategoryId int,
@CategoryNameEnglish nvarchar(200),
@CategoryNameFrench nvarchar(200),
@MemoryCareColor nvarchar(200)
AS
--20180828 chime created
BEGIN
	UPDATE
		tbl_AB_ActivityCategory 
	SET 
		CategoryNameEnglish= @CategoryNameEnglish, 
		CategoryNameFrench = @CategoryNameFrench, 
		MemoryCareColor = @MemoryCareColor
	WHERE
		Id = @CategoryId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Activity_Category_By_Id]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Activity_Category_By_Id]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Activity_Category_By_Id]
@CategoryId int
AS
--20180828 chime created
BEGIN
	SELECT
		Id,
		CategoryNameEnglish,
		CategoryNameFrench,
		MemoryCareColor 
	FROM 
		tbl_AB_ActivityCategory
	WHERE 
		Id = @CategoryId
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Delete_Activity_Category]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Delete_Activity_Category]
GO
CREATE PROCEDURE [dbo].[spAB_Delete_Activity_Category]
@CategoryId int
AS
--20180828 chime created
BEGIN
	DELETE FROM
		tbl_AB_ActivityCategory
	WHERE 
		Id = @CategoryId
END
GO