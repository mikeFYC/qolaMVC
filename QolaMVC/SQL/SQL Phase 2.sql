
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
	WHERE AC.Id IS NOT NULL
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
		CheckedValue,
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
@ActivityNameFrench nvarchar(200) = null,
@ActivityColor nvarchar(200) = null,
@FunPicture nvarchar(200) = null,
@Province nvarchar(200),
@ShowInAssessment bit,
@ActivityDisplayTitle nvarchar(200) = null
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
@AssessmentId int,
@CheckedValue nvarchar(3),
@ResidentId int,
@DateEntered datetime
AS
--20180712 chime created
--20180829 chimne added assessmentId
BEGIN
	INSERT INTO tbl_AB_ActivityAssessment (ActivityId, CheckedValue, ResidentId, DateEntered, AssessmentId ) 
	VALUES (@ActivityId, @CheckedValue, @ResidentId, @DateEntered, @AssessmentId )
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
	INSERT INTO tbl_AB_ActivityAssessment_Store (ResidentId, EnteredBy, DateEntered ) 
	VALUES (@ResidentId, @EnteredBy, getdate() )

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
	SELECT Id, ResidentId, EnteredBy, DateEntered=isnull(DateEntered, getdate()) FROM tbl_AB_ActivityAssessment_Store where ResidentId=@ResidentId
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
		AA.Id,
		ActivityId,
		A.ActivityDisplayTitle,
		A.ActivityNameEnglish,
		A.ActivityNameFrench,
		A.CategoryId,
		ActivityCategoryId, 
		Checkedvalue,
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



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Activity_Events]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Activity_Events]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Activity_Events]
AS
--20180912 chime created
BEGIN
	SELECT
		Id,
		ActivityId,
		EventTitle,
		StartDate,
		EndDate,
		StartTime,
		EndTime,
		note
	FROM 
		tbl_AB_ActivityEvents
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Activity_Events]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Activity_Events]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Activity_Events]
@ActivityId int,
@EventTitle nvarchar(100),
@StartDate datetime,
@EndDate datetime,
@StartTime nvarchar(20),
@EndTime nvarchar(20),
@note nvarchar(max) = ''
AS
--20180912 chime created
BEGIN
	INSERT INTO tbl_AB_ActivityEvents (ActivityId, EventTitle, StartDate, EndDate, StartTime, EndTime, note)
	VALUES (@ActivityId, @EventTitle, @StartDate, @EndDate, @StartTime, @EndTime, @note)

END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Fall_RiskAssessment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Fall_RiskAssessment]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Fall_RiskAssessment]
@Id int,
@FallHistory_IsTwoOrMore bit,
@FallHistory_IsOneOrTwo bit,
@Neurological_IsCVA bit,
@Neurological_IsParkinsons bit,
@Neurological_IsAlzheimers bit,
@Neurological_IsSeizureDisorder bit,
@Neurological_IsOther bit,
@Other_IsDiabetes bit,
@Other_IsOsteoporosis bit,
@Other_IsPosturalHypotension bit,
@Other_IsSyncope bit,
@Incontinence_IsBowel bit,
@Incontinence_IsBladder bit,
@Incontinence_IsTransfer bit,
@Incontinence_IsUnsteady bit,
@Medication_IsCardiac bit,
@Medication_IsDiuretics bit,
@Medication_IsNarcotics bit,
@Medication_IsAnalgesics bit,
@Medication_IsSedatives bit,
@Medication_IsAntiAnxiety bit,
@Medication_IsLaxatives bit,
@MentalStatus_IsConfused bit,
@MentalStatus_IsResidentNonCompliance bit,
@Orthopedic_IsRecent bit,
@Orthopedic_IsCast bit,
@Orthopedic_IsAmputation bit,
@Orthopedic_IsSevere bit,
@Sensory_IsDecreasedVision bit,
@Sensory_IsDecreasedHearing bit,
@Sensory_IsAphasia bit,
@Assistive_IsWheelChair bit,
@Assistive_IsWalker bit,
@Assistive_IsCane bit,
@TotalScore int,
@RiskLevel nvarchar(10),
@ResidentId int,
@DateEntered datetime,
@EnteredBy int
AS
--20180913 chime created
BEGIN
	INSERT INTO tbl_AB_FallRiskAssessment (FallHistory_IsTwoOrMore, FallHistory_IsOneOrTwo, Neurological_IsCVA, Neurological_IsParkinsons, Neurological_IsAlzheimers, Neurological_IsSeizureDisorder,
											Neurological_IsOther, Other_IsDiabetes, Other_IsOsteoporosis, Other_IsPosturalHypotension, Other_IsSyncope, Incontinence_IsBowel, Incontinence_IsBladder,
											Incontinence_IsTransfer, Incontinence_IsUnsteady, Medication_IsCardiac, Medication_IsDiuretics, Medication_IsNarcotics, Medication_IsAnalgesics, Medication_IsSedatives,
											Medication_IsAntiAnxiety, Medication_IsLaxatives, MentalStatus_IsConfused, MentalStatus_IsResidentNonCompliance, Orthopedic_IsRecent, Orthopedic_IsCast,
											Orthopedic_IsAmputation, Orthopedic_IsSevere, Sensory_IsDecreasedVision, Sensory_IsDecreasedHearing, Sensory_IsAphasia, Assistive_IsWheelChair,
											Assistive_IsWalker, Assistive_IsCane, TotalScore, RiskLevel, ResidentId, DateEntered, EnteredBy)
	VALUES (@FallHistory_IsTwoOrMore, @FallHistory_IsOneOrTwo, @Neurological_IsCVA, @Neurological_IsParkinsons, @Neurological_IsAlzheimers, @Neurological_IsSeizureDisorder,
			@Neurological_IsOther, @Other_IsDiabetes, @Other_IsOsteoporosis, @Other_IsPosturalHypotension, @Other_IsSyncope, @Incontinence_IsBowel, @Incontinence_IsBladder,
			@Incontinence_IsTransfer, @Incontinence_IsUnsteady, @Medication_IsCardiac, @Medication_IsDiuretics, @Medication_IsNarcotics, @Medication_IsAnalgesics, @Medication_IsSedatives,
			@Medication_IsAntiAnxiety, @Medication_IsLaxatives, @MentalStatus_IsConfused, @MentalStatus_IsResidentNonCompliance, @Orthopedic_IsRecent, @Orthopedic_IsCast,
			@Orthopedic_IsAmputation, @Orthopedic_IsSevere, @Sensory_IsDecreasedVision, @Sensory_IsDecreasedHearing, @Sensory_IsAphasia, @Assistive_IsWheelChair,
			@Assistive_IsWalker, @Assistive_IsCane, @TotalScore, @RiskLevel, @ResidentId, @DateEntered, @EnteredBy)

END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Fall_RiskAssessment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Fall_RiskAssessment]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Fall_RiskAssessment]
@ResidentId int
AS
--20180917 chime created
BEGIN
	SELECT
		Id,
		FallHistory_IsTwoOrMore, 
		FallHistory_IsOneOrTwo, 
		Neurological_IsCVA, 
		Neurological_IsParkinsons, 
		Neurological_IsAlzheimers, 
		Neurological_IsSeizureDisorder,
		Neurological_IsOther, 
		Other_IsDiabetes, 
		Other_IsOsteoporosis, 
		Other_IsPosturalHypotension, 
		Other_IsSyncope, 
		Incontinence_IsBowel, 
		Incontinence_IsBladder,
		Incontinence_IsTransfer, 
		Incontinence_IsUnsteady, 
		Medication_IsCardiac, 
		Medication_IsDiuretics, 
		Medication_IsNarcotics, 
		Medication_IsAnalgesics, 
		Medication_IsSedatives,
		Medication_IsAntiAnxiety, 
		Medication_IsLaxatives, 
		MentalStatus_IsConfused, 
		MentalStatus_IsResidentNonCompliance, 
		Orthopedic_IsRecent, 
		Orthopedic_IsCast,
		Orthopedic_IsAmputation, 
		Orthopedic_IsSevere, 
		Sensory_IsDecreasedVision, 
		Sensory_IsDecreasedHearing, 
		Sensory_IsAphasia, 
		Assistive_IsWheelChair,
		Assistive_IsWalker, 
		Assistive_IsCane, 
		TotalScore, 
		RiskLevel, 
		ResidentId, 
		DateEntered, 
		EnteredBy
	FROM 
		tbl_AB_FallRiskAssessment
	WHERE
		ResidentId = @ResidentId
END
GO
