
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
