IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_AddBowelMovement]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_AddBowelMovement]
GO
CREATE PROCEDURE [dbo].[spAB_AddBowelMovement]
@ResidentId int,
@Type nvarchar(15),
@ObservedBy nvarchar(15),
@Initials nvarchar(15),
@EnteredBy int,
@Period nvarchar(20)
AS
--20180425 chime created
BEGIN
	INSERT INTO [dbo].[tbl_AB_BowelMovementTracking] (ResidentId, strType, ObservedBy, Initials, EnteredBy, strPeriod, dtmTimeStamp) 
	VALUES (@ResidentId, @Type, @ObservedBy, @Initials, @EnteredBy, @Period, getdate())
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_GetBowelMovement]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_GetBowelMovement]
GO
CREATE PROCEDURE [dbo].[spAB_GetBowelMovement]
@ResidentId int
AS
--20180425 chime created
BEGIN
	SELECT 
		BM.Id,
		BM.ResidentId,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name,
		ResidentSuiteNo = S.fd_suite_no,
		BM.strType,
		BM.ObservedBy,
		BM.Initials,
		BM.EnteredBy,
		EnteredByUserName = (U.fd_first_name + ' ' + U.fd_last_name),
		BM.strPeriod,
		BM.dtmTimeStamp
	FROM [tbl_AB_BowelMovementTracking] BM 
	LEFT OUTER JOIN [tbl_Resident] R ON
	BM.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = BM.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE BM.ResidentId = @ResidentId
END
GO

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_FamilyConferenceNote]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_FamilyConferenceNote]
GO
CREATE PROCEDURE [dbo].[spAB_FamilyConferenceNote]
@ResidentId int,
@dtmDate datetime,
@SuiteNumber nvarchar(10),
@PHN nvarchar(20) = null,
@CareAndGCD nvarchar(max) = null,
@Medication nvarchar(max) = null,
@Recreation nvarchar(max) = null,
@Diet nvarchar(max) = null,
@Comments nvarchar(max) = null,
@Goals nvarchar(max) = null,
@Present1 nvarchar(255) = null,
@Present2 nvarchar(255) = null,
@Present3 nvarchar(255) = null,
@DateEntered datetime = null,
@EnteredBy int
AS
--20180501 chime created
BEGIN
	INSERT INTO [dbo].[tbl_AB_FamilyConverenceNotes] (ResidentId, dtmDate, SuiteNumber, PHN, CareAndGCD, Medication, Recreation, Diet, Comments, Goals,
	Present1, Present2, Present3, DateEntered, EnteredBy) 
	VALUES (@ResidentId, @dtmDate, @SuiteNumber, @PHN, @CareAndGCD, @Medication, @Recreation, @Diet, @Comments, @Goals,
	@Present1, @Present2, @Present3, @DateEntered, @EnteredBy)
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_GetFamilyConferenceNotes]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_GetFamilyConferenceNotes]
GO
CREATE PROCEDURE [dbo].[spAB_GetFamilyConferenceNotes]
@ResidentId int
AS
--20180425 chime created
BEGIN
	SELECT 
		FCN.Id,
		ResidentId = R.fd_id,
		ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name,
		FCN.dtmDate,
		FCN.SuiteNumber,
		FCN.PHN,
		FCN.CareAndGCD,
		FCN.Medication,
		FCN.Recreation,
		FCN.Diet,
		FCN.Comments,
		FCN.Goals,
		FCN.Present1,
		FCN.Present2,
		FCN.Present3,
		FCN.DateEntered,
		FCN.EnteredBy
	FROM [tbl_AB_FamilyConverenceNotes] FCN 
	LEFT OUTER JOIN [tbl_Resident] R ON
	FCN.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = FCN.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE FCN.ResidentId = @ResidentId
END
GO

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_AddHeadToToeAssessment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_AddHeadToToeAssessment]
GO
CREATE PROCEDURE [dbo].[spAB_AddHeadToToeAssessment]
@ResidentId int,
@dtmDate datetime,
@AdmissionStatus nvarchar(20),
@ReturnedFromHospital [varchar](max) = NULL,
@DiagnosisFromHospital [varchar](max) = NULL,
@Medications [varchar](max) = NULL,
@BP [nvarchar](10) = null,
@BPLocation [nvarchar](20) = null,
@RedialPulse [nvarchar](10) = null,
@PulseLocation [nvarchar](20) = null,
@Temp [nvarchar](10) = null,
@TempLocation [nvarchar](20) = null,
@Resp [nvarchar](10) = null,
@RespLocation [nvarchar](20) = null,
@SP02 [nvarchar](10) = null,
@SP02Location [nvarchar](20) = null,
@Person [nvarchar](10) = null,
@Place [nvarchar](10) = null,
@strTime [nvarchar](10) = null,
@Speech [nvarchar](10) = null,
@PrimaryLanguage [nvarchar](10) = null,
@PulpilsEquals [nvarchar](10) = null,
@PulpilsReactive [nvarchar](10) = null,
@Eyes [nvarchar](10) = null,
@GeneralFace [nvarchar](10) = null,
@EnteredBy int
AS
--20180506 chime created
BEGIN
	
	--ALTER TABLE [tbl_AB_Admission_Head_To_Toe_Assessment]
	--ALTER COLUMN AdmissionStatus nvarchar(100);

	INSERT INTO [dbo].[tbl_AB_Admission_Head_To_Toe_Assessment] (ResidentId, dtmDate, AdmissionStatus, ReturnedFromHospital, DiagnosisFromHospital, Medications, BP, BPLocation, RedialPulse, PulseLocation, Temp,
				TempLocation, Resp, RespLocation, SP02, SP02Location, Person, Place, strTime, Speech, PrimaryLanguage, PulpilsEquals, PulpilsReactive, Eyes, GeneralFace, DateEntered, EnteredBy) 
	VALUES (@ResidentId, @dtmDate, @AdmissionStatus, @ReturnedFromHospital, @DiagnosisFromHospital, @Medications, @BP, @BPLocation, @RedialPulse, @PulseLocation, @Temp, @TempLocation, @Resp, @RespLocation,
			@SP02, @SP02Location, @Person, @Place, @strTime, @Speech, @PrimaryLanguage, @PulpilsEquals, @PulpilsReactive, @Eyes, @GeneralFace, getdate(), @EnteredBy)
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Admission_Head_To_Toe_Assessments_By_ResidentId]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Admission_Head_To_Toe_Assessments_By_ResidentId]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Admission_Head_To_Toe_Assessments_By_ResidentId]
@ResidentId int
AS
--20180506 chime created
BEGIN
	SELECT 
		Id,
		ResidentId = R.fd_id,
		ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name, 
		SuiteNumber = S.fd_suite_no,
		dtmDate, 
		AdmissionStatus, 
		ReturnedFromHospital, 
		DiagnosisFromHospital, 
		Medications, 
		BP, 
		BPLocation, 
		RedialPulse, 
		PulseLocation, 
		Temp,
		TempLocation, 
		Resp, 
		RespLocation, 
		SP02, 
		SP02Location, 
		Person, 
		Place, 
		strTime, 
		Speech, 
		PrimaryLanguage, 
		PulpilsEquals, 
		PulpilsReactive, 
		Eyes, 
		GeneralFace, 
		DateEntered, 
		EnteredBy,
		EnteredByName = U.fd_first_name + ' ' + U.fd_last_name
	FROM [tbl_AB_Admission_Head_To_Toe_Assessment] HTT 
	LEFT OUTER JOIN [tbl_Resident] R ON
	HTT.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = HTT.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE HTT.ResidentId = @ResidentId
END
GO

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Excercise_Activity_Summary]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Summary]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Summary]
@ResidentId int,
@BaselineDate [nvarchar](200) = null,
@BaselineTug [nvarchar](200) = null,
@BaselineVPS [nvarchar](200) = null,
@TMonthDate [nvarchar](200) = null,
@TMonthTug [nvarchar](200) = null,
@TMonthVPS [nvarchar](200) = null,
@SMonthDate [nvarchar](200) = null,
@SMonthTug [nvarchar](200) = null,
@SMonthVPS [nvarchar](200) = null,
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [dbo].[tbl_AB_Excercise_Activity_Summary] (ResidentId, BaselineDate, BaselineTug, BaselineVPS, TMonthDate, TMonthTug, TMonthVPS, SMonthDate, SMonthTug, SMonthVPS, DateEntered, EnteredBy) 
	VALUES (@ResidentId, @BaselineDate, @BaselineTug, @BaselineVPS, @TMonthDate, @TMonthTug, @TMonthVPS, @SMonthDate, @SMonthTug, @SMonthVPS, getdate(), @EnteredBy)
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Excercise_Activity_Summary]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Summary]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Summary]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT 
		Id,
		ResidentId = R.fd_id,
		ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name, 
		BaselineDate,
		BaselineTug,
		BaselineVPS,
		TMonthDate,
		TMonthTug,
		TMonthVPS,
		SMonthDate,
		SMonthTug,
		SMonthVPS,
		DateEntered, 
		EnteredBy = U.fd_id,
		EnteredByName = U.fd_first_name + ' ' + U.fd_last_name
	FROM [tbl_AB_Excercise_Activity_Summary] HTT 
	LEFT OUTER JOIN [tbl_Resident] R ON
	HTT.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = HTT.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE HTT.ResidentId = @ResidentId
END
GO

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Excercise_Activity_Detail]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Detail]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Detail]
@ResidentId int,
@ActivityName [nvarchar](200),
@WeekId int,
@Sunday [bit] null,
@Monday [bit] null,
@Tuesday [bit] null,
@Wednesday [bit] null,
@Thursday [bit] null,
@Friday [bit] null,
@Saturday [bit] null,
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [dbo].[tbl_AB_Excercise_Activity_Detail] (ResidentId, ActivityName, WeekId, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, DateEntered, EnteredBy) 
	VALUES (@ResidentId, @ActivityName, @WeekId, @Sunday, @Monday, @Tuesday, @Wednesday, @Thursday, @Friday, @Saturday, getdate(), @EnteredBy)
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Excercise_Activity_Detail]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Detail]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Detail]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT 
		Id,
		ResidentId = R.fd_id,
		ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name, 
		SuiteNumber = S.fd_suite_no,
		ActivityName, 
		WeekId, 
		Sunday, 
		Monday, 
		Tuesday, 
		Wednesday, 
		Thursday, 
		Friday, 
		Saturday,
		DateEntered, 
		EnteredBy = U.fd_id,
		EnteredByName = U.fd_first_name + ' ' + U.fd_last_name
	FROM [tbl_AB_Excercise_Activity_Detail] HTT 
	LEFT OUTER JOIN [tbl_Resident] R ON
	HTT.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = HTT.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE HTT.ResidentId = @ResidentId
END
GO


GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_HSEP_Detail]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_HSEP_Detail]
GO
CREATE PROCEDURE [dbo].[spAB_Add_HSEP_Detail]
@ResidentId int,
@ActivityName [nvarchar](200),
@DateOfTeaching [datetime] = null,
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [dbo].[tbl_AB_HSEP_Detail] (ResidentId, ActivityName, DateOfTeaching, DateEntered, EnteredBy) 
	VALUES (@ResidentId, @ActivityName, @DateOfTeaching, getdate(), @EnteredBy)
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_HSEP_Detail]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_HSEP_Detail]
GO
CREATE PROCEDURE [dbo].[spAB_Get_HSEP_Detail]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT 
		Id,
		ResidentId = R.fd_id,
		ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name, 
		SuiteNumber = S.fd_suite_no,
		ActivityName, 
		DateOfTeaching, 
		EnteredBy = U.fd_id,
		EnteredByName = U.fd_first_name + ' ' + U.fd_last_name
	FROM [tbl_AB_HSEP_Detail] HTT 
	LEFT OUTER JOIN [tbl_Resident] R ON
	HTT.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = HTT.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE HTT.ResidentId = @ResidentId
END
GO



GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Excercise_Activity_Summary]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Summary]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Summary]
@ResidentId int,
@BaselineDate [nvarchar](200) = null,
@BaselineTug [nvarchar](200) = null,
@BaselineVPS [nvarchar](200) = NULL,
@TMonthDate [nvarchar](200) = null,
@TMonthTug [nvarchar](200) = null,
@TMonthVPS [nvarchar](200) = NULL,
@SMonthDate [nvarchar](200) = null,
@SMonthTug [nvarchar](200) = null,
@SMonthVPS [nvarchar](200) = NULL,
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [dbo].[tbl_AB_Excercise_Activity_Summary] (ResidentId, BaselineDate, BaselineTug, BaselineVPS, TMonthDate, TMonthTug, TMonthVPS, SMonthDate, SMonthTug, SMonthVPS, DateEntered, EnteredBy) 
	VALUES (@ResidentId, @BaselineDate, @BaselineTug, @BaselineVPS, @TMonthDate, @TMonthTug, @TMonthVPS, @SMonthDate, @SMonthTug, @SMonthVPS, getdate(), @EnteredBy)
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Excercise_Activity_Summary]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Summary]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Summary]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT 
		Id,
		ResidentId = R.fd_id,
		ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name, 
		SuiteNumber = S.fd_suite_no,
		BaselineDate,
		BaselineTug,
		BaselineVPS,
		TMonthDate,
		TMonthTug,
		TMonthVPS,
		SMonthDate,
		SMonthTug,
		SMonthVPS,
		SuiteNumber = S.fd_suite_no,
		DateEntered, 
		EnteredBy = U.fd_id,
		EnteredByName = U.fd_first_name + ' ' + U.fd_last_name
	FROM [tbl_AB_Excercise_Activity_Summary] HTT 
	LEFT OUTER JOIN [tbl_Resident] R ON
	HTT.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = HTT.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE HTT.ResidentId = @ResidentId
END
GO
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_Excercise_Activity_Summary]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Summary]
GO
CREATE PROCEDURE [dbo].[spAB_Add_Excercise_Activity_Summary]
@ResidentId int,
@BaselineDate [nvarchar](200) = null,
@BaselineTug [nvarchar](200) = null,
@BaselineVPS [nvarchar](200) = NULL,
@TMonthDate [nvarchar](200) = null,
@TMonthTug [nvarchar](200) = null,
@TMonthVPS [nvarchar](200) = NULL,
@SMonthDate [nvarchar](200) = null,
@SMonthTug [nvarchar](200) = null,
@SMonthVPS [nvarchar](200) = NULL,
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [dbo].[tbl_AB_Excercise_Activity_Summary] (ResidentId, BaselineDate, BaselineTug, BaselineVPS, TMonthDate, TMonthTug, TMonthVPS, SMonthDate, SMonthTug, SMonthVPS, DateEntered, EnteredBy) 
	VALUES (@ResidentId, @BaselineDate, @BaselineTug, @BaselineVPS, @TMonthDate, @TMonthTug, @TMonthVPS, @SMonthDate, @SMonthTug, @SMonthVPS, getdate(), @EnteredBy)
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Excercise_Activity_Summary]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Summary]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Excercise_Activity_Summary]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT 
		Id,
		ResidentId = R.fd_id,
		ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
		ResidentFirstName = R.fd_first_name,
		ResidentLastName = R.fd_last_name, 
		SuiteNumber = S.fd_suite_no,
		BaselineDate,
		BaselineTug,
		BaselineVPS,
		TMonthDate,
		TMonthTug,
		TMonthVPS,
		SMonthDate,
		SMonthTug,
		SMonthVPS,
		SuiteNumber = S.fd_suite_no,
		DateEntered, 
		EnteredBy = U.fd_id,
		EnteredByName = U.fd_first_name + ' ' + U.fd_last_name
	FROM [tbl_AB_Excercise_Activity_Summary] HTT 
	LEFT OUTER JOIN [tbl_Resident] R ON
	HTT.ResidentId = R.fd_id
	LEFT OUTER JOIN [tbl_User] U ON
	U.fd_id = HTT.EnteredBy
	LEFT OUTER JOIN [tbl_Suite] S ON
	S.fd_id = R.fd_suite_id
	WHERE HTT.ResidentId = @ResidentId
END
GO


/****** Object:  StoredProcedure [dbo].[Get_Resident_Search_By_Home_Id_Key]    Script Date: 2018-05-07 4:30:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Get_Resident_Search_By_Home_Id_Key]
@homeId INT,
@key varchar(200),
@status char(1)                                    
AS  
-- exec Get_Resident_Search_By_Home_Id_Key 2,'5','A'
BEGIN  
	IF @status = 'O'
		SELECT DISTINCT R.fd_id
		,SUBSTRING(( SELECT ',' + (CAST(S.fd_suite_no AS VARCHAR)) AS [text()] 
			FROM tbl_Suite S(NOLOCK)
			INNER JOIN tbl_Suite_Handler ST ON S.fd_id = ST.fd_suite_id 
			AND CAST(ST.fd_move_in_date AS DATE) <= (CAST(GETDATE() AS DATE))
			--AND (ST.fd_move_out_date IS NULL OR (CAST(GETDATE() AS DATE) BETWEEN CAST(ST.fd_move_in_date AS DATE) AND CAST(ST.fd_move_out_date AS DATE)))
			AND ST.fd_resident_id = R.fd_id
			WHERE ST.fd_home_id = @homeId
			FOR XML PATH ('')),2,100)+ ', '+ R.fd_last_name + ', '+ R.fd_first_name AS searchId
		,R.fd_qola_resident 
		FROM tbl_Resident R
		LEFT JOIN tbl_Suite_Handler ST on R.fd_id = ST.fd_resident_id 
		INNER JOIN tbl_Suite S ON S.fd_id = ST.fd_suite_id
		WHERE ST.fd_home_id = @homeId
		AND CAST(ST.fd_move_in_date AS DATE) <= (CAST(GETDATE() AS DATE))
		AND (ST.fd_move_out_date IS NULL OR (CAST(GETDATE() AS DATE) BETWEEN CAST(ST.fd_move_in_date AS DATE) AND CAST(ST.fd_move_out_date AS DATE)))
		AND (R.fd_first_name LIKE @key + '%' OR R.fd_last_name LIKE @key + '%' OR S.fd_suite_no LIKE @key + '%')
		ORDER BY searchId
	ELSE IF @status = 'A'
		SELECT DISTINCT R.fd_id
		,SUBSTRING(( SELECT ',' + (CAST(S.fd_suite_no AS VARCHAR)) AS [text()] 
			FROM tbl_Suite S(NOLOCK)
			INNER JOIN tbl_Suite_Handler ST ON S.fd_id = ST.fd_suite_id 
			AND CAST(ST.fd_move_in_date AS DATE) <= (CAST(GETDATE() AS DATE))
			AND (ST.fd_move_out_date IS NULL OR (CAST(GETDATE() AS DATE) BETWEEN CAST(ST.fd_move_in_date AS DATE) AND CAST(ST.fd_move_out_date AS DATE)))
			AND ST.fd_resident_id = R.fd_id
			WHERE ST.fd_home_id = @homeId
			FOR XML PATH ('')),2,100)+ ', '+ R.fd_last_name + ', '+ R.fd_first_name AS searchId
		,R.fd_qola_resident 
		FROM tbl_Resident R
		LEFT JOIN tbl_Suite_Handler ST on R.fd_id = ST.fd_resident_id
		INNER JOIN tbl_Suite S ON S.fd_id = ST.fd_suite_id
		WHERE ST.fd_home_id = @homeId AND ST.fd_status != 5 
		AND CAST(ST.fd_move_in_date AS DATE) <= (CAST(GETDATE() AS DATE))
		AND (ST.fd_move_out_date IS NULL OR (CAST(GETDATE() AS DATE) BETWEEN CAST(ST.fd_move_in_date AS DATE) AND CAST(ST.fd_move_out_date AS DATE)))
		AND (ST.fd_pass_away_date IS NULL OR CAST(ST.fd_pass_away_date AS DATE ) > CAST(GETDATE() AS DATE))
		AND (R.fd_first_name LIKE @key + '%' OR R.fd_last_name LIKE @key + '%' OR S.fd_suite_no LIKE @key + '%')
		ORDER BY searchId 
	ELSE IF @status = 'I'
		SELECT DISTINCT R.fd_id
		,[dbo].[fun_Get_Resident_Suite_Numbers_By_ID](R.fd_id,'I') + ', '+ R.fd_last_name + ', '+ R.fd_first_name AS searchId
		,R.fd_qola_resident 
		FROM tbl_Resident R
		LEFT JOIN tbl_Suite_Handler ST on R.fd_id = ST.fd_resident_id
		INNER JOIN tbl_Suite S ON S.fd_id = ST.fd_suite_id
		WHERE ST.fd_home_id = @homeId
		AND CAST(ST.fd_move_in_date AS DATE) < (CAST(GETDATE() AS DATE))
		AND (ST.fd_move_out_date IS NOT NULL AND (CAST(GETDATE() AS DATE)> CAST(ST.fd_move_out_date AS DATE)))
		AND (R.fd_first_name LIKE @key + '%' OR R.fd_last_name LIKE @key + '%' OR S.fd_suite_no LIKE @key + '%')
		AND R.fd_id not in (select DISTINCT fd_resident_id from tbl_Suite_Handler 
		WHERE (fd_move_out_date is null or cast(fd_move_out_date as date) >= cast(getdate() as date)))
		ORDER BY searchId 	
	ELSE IF @status = 'N' --alberta search same as O
		SELECT DISTINCT R.fd_id,
		S.fd_suite_no,
		R.fd_first_name,
		R.fd_last_name,
		R.fd_qola_resident,
		R.fd_image
		,SUBSTRING(( SELECT ',' + (CAST(S.fd_suite_no AS VARCHAR)) AS [text()] 
			FROM tbl_Suite S(NOLOCK)
			INNER JOIN tbl_Suite_Handler ST ON S.fd_id = ST.fd_suite_id 
			AND CAST(ST.fd_move_in_date AS DATE) <= (CAST(GETDATE() AS DATE))
			--AND (ST.fd_move_out_date IS NULL OR (CAST(GETDATE() AS DATE) BETWEEN CAST(ST.fd_move_in_date AS DATE) AND CAST(ST.fd_move_out_date AS DATE)))
			AND ST.fd_resident_id = R.fd_id
			WHERE ST.fd_home_id = @homeId
			FOR XML PATH ('')),2,100)+ ', '+ R.fd_last_name + ', '+ R.fd_first_name AS searchId
		,R.fd_qola_resident 
		FROM tbl_Resident R
		LEFT JOIN tbl_Suite_Handler ST on R.fd_id = ST.fd_resident_id 
		INNER JOIN tbl_Suite S ON S.fd_id = ST.fd_suite_id
		WHERE ST.fd_home_id = @homeId
		AND CAST(ST.fd_move_in_date AS DATE) <= (CAST(GETDATE() AS DATE))
		--AND (ST.fd_move_out_date IS NULL OR (CAST(GETDATE() AS DATE) BETWEEN CAST(ST.fd_move_in_date AS DATE) AND CAST(ST.fd_move_out_date AS DATE)))
		AND (R.fd_first_name LIKE @key + '%' OR R.fd_last_name LIKE @key + '%' OR S.fd_suite_no LIKE @key + '%')
		ORDER BY searchId	
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_DietaryAssessment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_DietaryAssessment]
GO
CREATE PROCEDURE [dbo].[spAB_Add_DietaryAssessment]
@ResidentId int,
@NutritionalStatus nvarchar(200) = NULL,
@Risk nvarchar(200) = NULL,
@AssistiveDevices nvarchar(200) = NULL,
@Texture nvarchar(200) = NULL,
@Apetite nvarchar(200) = NULL,
@Other nvarchar(200) = NULL,
@Likes nvarchar(max) = NULL,
@DisLikes nvarchar(max) = NULL,
@Notes nvarchar(max) = NULL,
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [tbl_AB_DietaryAssessment] (ResidentId, NutritionalStatus, Risk, AssistiveDevices, Texture, Apetite, Other, Likes, DisLikes, Notes, EnteredBy, DateEntered) 
	VALUES (@ResidentId, @NutritionalStatus, @Risk, @AssistiveDevices, @Texture, @Apetite, @Other, @Likes, @DisLikes, @Notes, @EnteredBy, GETDATE())

	SELECT @@IDENTITY as Id
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_DietaryAssessment_Diets]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_DietaryAssessment_Diets]
GO
CREATE PROCEDURE [dbo].[spAB_Add_DietaryAssessment_Diets]
@ResidentId int,
@AssessmentId int,
@Diet nvarchar(200),
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [tbl_AB_Diet] (ResidentId, AssessmentId, Diet, EnteredBy, DateEntered) 
	VALUES (@ResidentId, @AssessmentId, @Diet, @EnteredBy, GETDATE())

END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_DietaryAssessment_Allergies]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_DietaryAssessment_Allergies]
GO
CREATE PROCEDURE [dbo].[spAB_Add_DietaryAssessment_Allergies]
@ResidentId int,
@AssessmentId int,
@AllergyId int,
@Allergy nvarchar(200),
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [tbl_AB_DietaryAssessment_Allergy] (ResidentId, AssessmentId, Allergy, AllergyId, EnteredBy, DateEntered) 
	VALUES (@ResidentId, @AssessmentId, @Allergy, @AllergyId, @EnteredBy, GETDATE())

END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Resident_DietaryAssessments]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessments]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessments]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT
	Id,
	ResidentId,
	NutritionalStatus,
	Risk,
	AssistiveDevices,
	Texture,
	Other,
	Likes,
	DisLikes,
	Notes,
	EnteredBy,
	DateEntered
	FROM tbl_AB_DietaryAssessment
	WHERE ResidentId = @ResidentId
END
GO




IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Resident_DietaryAssessmentDiets]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessmentDiets]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessmentDiets]
@ResidentId int,
@AssessmentId int
AS
--20180507 chime created
BEGIN
	SELECT
	Id,
	ResidentId,
	AssessmentId,
	Diet,
	EnteredBy,
	DateEntered
	FROM [tbl_AB_Diet]
	WHERE ResidentId = @ResidentId AND AssessmentId = @AssessmentId
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Resident_DietaryAssessmentAllergies]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessmentAllergies]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessmentAllergies]
@ResidentId int,
@AssessmentId int
AS
--20180507 chime created
BEGIN
	SELECT
	Id,
	ResidentId,
	AssessmentId,
	AllergyId,
	Allergy,
	EnteredBy,
	DateEntered
	FROM [tbl_AB_DietaryAssessment_Allergy]
	WHERE ResidentId = @ResidentId AND AssessmentId = @AssessmentId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_DietaryAssessmentAllergies]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_DietaryAssessmentAllergies]
GO
CREATE PROCEDURE [dbo].[spAB_Get_DietaryAssessmentAllergies]
@HomeId int
AS
--20180602 chime created
BEGIN
	SELECT
	Id,
	ResidentId,
	ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
	Suite = S.fd_suite_no,
	AssessmentId,
	AllergyId,
	Allergy,
	EnteredBy,
	DateEntered
	FROM [tbl_AB_DietaryAssessment_Allergy] A
	LEFT OUTER JOIN tbl_Resident R ON
	R.fd_id = A.ResidentId
	LEFT OUTER JOIN tbl_Suite S ON
	S.fd_id = R.fd_suite_id
	WHERE R.fd_home_id = @HomeId
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_DietaryAssessmentDiets]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_DietaryAssessmentDiets]
GO
CREATE PROCEDURE [dbo].[spAB_Get_DietaryAssessmentDiets]
@HomeId int
AS
--20180507 chime created
BEGIN
	SELECT
	Id,
	ResidentId,
	ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
	Suite = S.fd_suite_no,
	AssessmentId,
	Diet,
	EnteredBy,
	DateEntered
	FROM [tbl_AB_Diet] D
	LEFT OUTER JOIN tbl_Resident R ON
	R.fd_id = D.ResidentId
	LEFT OUTER JOIN tbl_Suite S ON
	S.fd_id = R.fd_suite_id
	WHERE R.fd_home_id = @HomeId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_DietaryAssessment_Likes]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_DietaryAssessment_Likes]
GO
CREATE PROCEDURE [dbo].[spAB_Get_DietaryAssessment_Likes]
@HomeId int
AS
--20180507 chime created
BEGIN
	SELECT
	ResidentId,
	ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
	Suite = S.fd_suite_no,
	Likes = Max(Likes),
	DateEntered = max(DateEntered)
	FROM [tbl_AB_DietaryAssessment] D
	LEFT OUTER JOIN tbl_Resident R ON
	R.fd_id = D.ResidentId
	LEFT OUTER JOIN tbl_Suite S ON
	S.fd_id = R.fd_suite_id
	WHERE R.fd_home_id = @HomeId
	group by ResidentId, R.fd_first_name, R.fd_last_name, S.fd_suite_no
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_DietaryAssessment_DisLikes]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_DietaryAssessment_DisLikes]
GO
CREATE PROCEDURE [dbo].[spAB_Get_DietaryAssessment_DisLikes]
@HomeId int
AS
--20180507 chime created
BEGIN
	SELECT
	ResidentId,
	ResidentName = R.fd_first_name + ' ' + R.fd_last_name,
	Suite = S.fd_suite_no,
	DisLikes = max(DisLikes),
	DateEntered = max(DateEntered)
	FROM [tbl_AB_DietaryAssessment] D
	LEFT OUTER JOIN tbl_Resident R ON
	R.fd_id = D.ResidentId
	LEFT OUTER JOIN tbl_Suite S ON
	S.fd_id = R.fd_suite_id
	WHERE R.fd_home_id = @HomeId
	group by ResidentId, R.fd_first_name, R.fd_last_name, S.fd_suite_no
END
GO




IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_UnusualIncident]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_UnusualIncident]
GO
CREATE PROCEDURE [dbo].[spAB_Add_UnusualIncident]
@ResidentId int,
@strLocation [nvarchar](200) = null,
@Employee [nvarchar](200) = null,
@Dept [nvarchar](200) = null,
@Visitor [nvarchar](200) = null,
@Room [nvarchar](200) = null,
@Other [nvarchar](200) = null,
@WasWitnessed [nvarchar](200) = null,
@WitnessName [nvarchar](200) = null,
@IsFall bit null,
@IsElopement bit null,
@ElopementValue nvarchar(50) = null,
@IsUnusualBehavior bit null,
@UnusualBehaviorvalue nvarchar(50),
@IsPhysicalInjury bit null,
@PhysicalInjuryValue nvarchar(50) = null,
@IsPropertyLoss bit null,
@PropertyLossValue nvarchar(50) = null,
@IsSuspicious bit null,
@SuspicionValue nvarchar(50) = null,
@IsTreatment bit null,
@TreatmentValue nvarchar(50) = null,
@IsOther bit null,
@SectionD nvarchar(max) = null,
@SectionE nvarchar(max) = null,
@SectionF nvarchar(max) = null,
@SectionH nvarchar(max) = null,
@IncidentDocumented nvarchar(20) = null,
@ChangesMade nvarchar(20) = null,
@ReferralConsult nvarchar(20) = null,
@OHSCommitteeInformed nvarchar(20) = null,
@RecordTrackingForm nvarchar(20) = null,
@IncidentInformation nvarchar(20) = null,
@SectionJ nvarchar(max) = null,
@EnteredBy int
AS
--20180507 chime created
BEGIN
	INSERT INTO [tbl_AB_UnusualIncident] (ResidentId, strLocation, Employee, Dept, Visitor, Room, Other, WasWitnessed, WitnessName, IsFall, IsElopement, ElopementValue, IsUnusualBehavior,
											UnusualBehaviorvalue, IsPhysicalInjury, PhysicalInjuryValue, IsPropertyLoss, PropertyLossValue, IsSuspicious, SuspicionValue, IsTreatment,
											TreatmentValue, IsOther, SectionD, SectionE, SectionF, SectionH, IncidentDocumented, ChangesMade, ReferralConsult, OHSCommitteeInformed,
											RecordTrackingForm, IncidentInformation, SectionJ, EnteredBy, DateEntered) 
	VALUES (@ResidentId, @strLocation, @Employee, @Dept, @Visitor, @Room, @Other, @WasWitnessed, @WitnessName, @IsFall, @IsElopement, @ElopementValue, @IsUnusualBehavior,
				@UnusualBehaviorvalue, @IsPhysicalInjury, @PhysicalInjuryValue, @IsPropertyLoss, @PropertyLossValue, @IsSuspicious, @SuspicionValue, @IsTreatment,
				@TreatmentValue, @IsOther, @SectionD, @SectionE, @SectionF, @SectionH, @IncidentDocumented, @ChangesMade, @ReferralConsult, @OHSCommitteeInformed,
				@RecordTrackingForm, @IncidentInformation, @SectionJ, @EnteredBy, GETDATE())

END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_Resident_DietaryAssessments]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessments]
GO
CREATE PROCEDURE [dbo].[spAB_Get_Resident_DietaryAssessments]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT
	Id,
	ResidentId,
	strLocation, 
	Employee, 
	Dept, 
	Visitor, 
	Room, 
	Other, 
	WasWitnessed, 
	WitnessName, 
	IsFall, 
	IsElopement, 
	ElopementValue, 
	IsUnusualBehavior,
	UnusualBehaviorvalue, 
	IsPhysicalInjury, 
	PhysicalInjuryValue, 
	IsPropertyLoss, 
	PropertyLossValue, 
	IsSuspicious, 
	SuspicionValue, 
	IsTreatment,
	TreatmentValue, 
	IsOther, 
	SectionD, 
	SectionE, 
	SectionF, 
	SectionH, 
	IncidentDocumented, 
	ChangesMade, 
	ReferralConsult, 
	OHSCommitteeInformed,
	RecordTrackingForm, 
	IncidentInformation, 
	SectionJ,
	EnteredBy,
	DateEntered
	FROM [tbl_AB_UnusualIncident]
	WHERE ResidentId = @ResidentId
END
GO