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

			--return the id of the record just inserted
	SELECT Id = @@IDENTITY

END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_UnusualIncident]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_UnusualIncident]
GO
CREATE PROCEDURE [dbo].[spAB_Get_UnusualIncident]
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


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_UnusualIncident_SectionG]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_UnusualIncident_SectionG]
GO
CREATE PROCEDURE [dbo].[spAB_Add_UnusualIncident_SectionG]
@ResidentId int,
@IncidentId [int],
@Notify [nvarchar](200) = null,
@Name [nvarchar](200) = null,
@Date [nvarchar](200) = null,
@ByWhom [nvarchar](200) = null,
@Via [nvarchar](200) = null,
@EnteredBy int
AS
--20180610 chime created
BEGIN
	INSERT INTO [tbl_AB_UnusualIncident_SectionG] (ResidentId, IncidentId, Notify, strName, dtmDate, ByWhom, Via, EnteredBy, DateEntered) 
	VALUES (@ResidentId, @IncidentId, @Notify, @Name, @Date, @ByWhom, @Via, @EnteredBy, GETDATE())

END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_UnusualIncident_SectionG]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_UnusualIncident_SectionG]
GO
CREATE PROCEDURE [dbo].[spAB_Get_UnusualIncident_SectionG]
@IncidentId int
AS
--20180507 chime created
BEGIN
	SELECT
	Id,
	ResidentId,
	IncidentId, 
	Notify, 
	strName, 
	dtmDate, 
	ByWhom, 
	Via,
	EnteredBy,
	DateEntered
	FROM [tbl_AB_UnusualIncident_SectionG]
	WHERE IncidentId = @IncidentId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_OLD]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_OLD]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_OLD]
@ResidentId int
AS
--20180610 chime created
BEGIN
	SELECT
	CP.Id,
	CP.ResidentId,
	CP.Assessed,
	CP.LevelOfCare,
	CP.CompleteStatus,
	VitalSigns.BP_Systolic,
	VitalSigns.BP_Diastolic,
	VitalSigns.BP_DateCompleted,
	VitalSigns.Temperature,
	VitalSigns.Temp_DateCompleted,
	VitalSigns.WeightLBS,
	VitalSigns.Weight_DateCompleted,
	VitalSigns.Height_Feet,
	VitalSigns.Height_Inches,
	VitalSigns.Height_DateCompleted,
	VitalSigns.Pulse,
	VitalSigns.Pulse_DateCompleted,
	VitalSigns.PulseRegular,
	PersonalHygiene.AMCare,
	PersonalHygiene.PMCare,
	PersonalHygiene.Bathing,
	PersonalHygiene.AM_AssistedBy,
	PersonalHygiene.PM_AssistedBy,
	PersonalHygiene.Bathing_AssistedBy,
	PersonalHygiene.AM_AgencyName,
	PersonalHygiene.PM_AgencyName,
	PersonalHygiene.Bathing_AgencyName,
	PersonalHygiene.AM_PreferredTime,
	PersonalHygiene.PM_PreferredTime,
	PersonalHygiene.Bathing_PreferredTime,
	PersonalHygiene.AM_PreferredType,
	PersonalHygiene.PM_PreferredType,
	PersonalHygiene.Bathing_PreferredType,
	PersonalHygiene.PreferredDays,
	AW.Dressing,
	AW.Dressing_PreferredTime,
	AW.NailCare,
	AW.NailCare_PreferredTime,
	AW.Shaving,
	AW.Shaving_PreferredTime,
	AW.FootCare,
	AW.FootCare_PreferredTime,
	AW.OralHygiene,
	AW.OralHygiene_PreferredTime,
	AW.Teeth,
	CPM.Mobility,
	CPM.Transfers,
	CPM.MechanicalLift,
	CPM.Lift,
	CPM.Walker,
	CPM.Walker_Type,
	CPM.WheelChair,
	CPM.WheelChair_Type,
	CPM.Cane,
	CPM.Cane_Type,
	CPM.Scooter,
	CPM.Scooter_Type,
	CPM.PT,
	CPM.PT_Frequency,
	CPM.PT_Provider,
	CPM.OT,
	CPM.OT_Frequency,
	CPM.OT_Provider,
	CPS.SafetyPASD,
	CPS.Other,
	CPS.Rails,
	CPS.NightOnly,
	CPME.BreakFast,
	CPME.Lunch,
	CPME.Dinner,
	CPB.Behavior,
	CPB.HarmToSelf,
	CPB.smoker,
	CPB.RiskOfWandering,
	CPB.CognitiveStatus,
	CPB.OtherInfo,
	CPCF.CognitiveFunction,
	CPO.IsPerson,
	CPO.IsPlace,
	CPO.IsTime,
	CPO.IsDementiaCare,
	CPN.NutritionStatus,
	CPN.Risk,
	CPN.AssistiveDevices,
	CPN.Texture,
	CPN.Other,
	CPN.Diet,
	CPN.OtherDiet,
	CPN.Notes,
	CPN.Allergies,
	CPMeals.Appetite,
	CPMeals.BreakFast,
	CPMeals.Lunch,
	CPMeals.Dinner,
	CPE.Bladder,
	CPE.Bowel,
	CPE.NameCode,
	CPE.ContinenceProducts,
	CPE.AssistiveDevices,
	CPE.Supplier,
	CPE.AssessmentCompletedBy,
	CPE.AssessmentDate,
	CPT.Bathroom,
	CPT.Commode,
	CPT.Bedpan,
	CPT.Toileting,
	CPMedication.Assistance,
	CPMedication.Administration,
	CPMedication.CompletedBy,
	CPMedication.Agency,
	CPMedication.Pharmacy,
	CPMedication.Allergies,
	CPSA.Vision,
	CPSA.Hearing,
	CPSA.Communication,
	CPSA.Notes,
	CPWC.WoundCare,
	CPWC.AssistedBy,
	CPWC.Agency,
	CPSC.SkinCare,
	CPSC.SpecialTreatments,
	CPSN.Oxygen,
	CPSN.Oxygen_Supplier,
	CPSN.Oxygen_Rate,
	CPSN.Oxygen_Notes,
	CPSN.CPAP,
	CPSN.CPAP_Supplier,
	CPSN.CPAP_Notes,
	CPSE.SpecialEquipment,
	CPSE.Details,
	CPFS.FamilyMeeting,
	CPFS.FamilyInvolvment,
	CPI.TB,
	CPI.TB_Date,
	CPI.ChestXRay,
	CPI.ChestXRay_Date,
	CPI.Pneumonia,
	CPI.Pneumonia_Date,
	CPI.FluVaccine,
	CPI.FluVaccine_Date,
	CPI.Tetanus,
	CPI.Tetanus_Date,
	CPID.MRSA,
	CPID.MRSA_Diagnosed_Date,
	CPID.MRSA_Resolved_Date,
	CPID.VRE,
	CPID.VRE_Diagnosed_Date,
	CPID.VRE_Resolved_Date,
	CPID.CDiff,
	CPID.CDiff_Diagnosed_Date,
	CPID.CDiff_Resolved_Date,
	CPID.Other,
	CPID.Other_Diagnosed_Date,
	CPID.Other_Resolved_Date,

	CP.EnteredBy,
	CP.DateEntered
	FROM 
		[tbl_AB_CarePlan] CP 
	LEFT OUTER JOIN 
		[tbl_AB_CarePlan_VitalSigns] VitalSigns ON
		VitalSigns.ResidentId = CP.ResidentId AND
		VitalSigns.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_PersonalHygiene] PersonalHygiene ON
		PersonalHygiene.ResidentId = CP.ResidentId AND
		PersonalHygiene.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_AssistanceWith] AW ON
		AW.ResidentId = CP.ResidentId AND
		AW.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Mobility] CPM ON
		CPM.ResidentId = CP.ResidentId AND
		CPM.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Safety] CPS ON
		CPS.ResidentId = CP.ResidentId AND
		CPS.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_MealEscort] CPME ON
		CPME.ResidentId = CP.ResidentId AND
		CPME.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Behaviour] CPB ON
		CPB.ResidentId = CP.ResidentId AND
		CPB.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_CognitiveFunction] CPCF ON
		CPCF.ResidentId = CP.ResidentId AND
		CPCF.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Orientation] CPO ON
		CPO.ResidentId = CP.ResidentId AND
		CPO.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Nutrition] CPN ON
		CPN.ResidentId = CP.ResidentId AND
		CPN.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Meals] CPMeals ON
		CPMeals.ResidentId = CP.ResidentId AND
		CPMeals.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Elimination] CPE ON
		CPE.ResidentId = CP.ResidentId AND
		CPE.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Toileting] CPT ON
		CPT.ResidentId = CP.ResidentId AND
		CPT.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Medication] CPMedication ON
		CPMedication.ResidentId = CP.ResidentId AND
		CPMedication.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_SensoryAbilities] CPSA ON
		CPSA.ResidentId = CP.ResidentId AND
		CPSA.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_WoundCare] CPWC ON
		CPWC.ResidentId = CP.ResidentId AND
		CPWC.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_SkinCare] CPSC ON
		CPSC.ResidentId = CP.ResidentId AND
		CPSC.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_SpecialNeeds] CPSN ON
		CPSN.ResidentId = CP.ResidentId AND
		CPSN.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_SpecialEquipment] CPSE ON
		CPSE.ResidentId = CP.ResidentId AND
		CPSE.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_FamilySupport] CPFS ON
		CPFS.ResidentId = CP.ResidentId AND
		CPFS.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_Immunization] CPI ON
		CPI.ResidentId = CP.ResidentId AND
		CPI.CarePlanId = CP.Id
	LEFT OUTER JOIN
		[tbl_AB_CarePlan_InfectiousDiseases] CPID ON
		CPID.ResidentId = CP.ResidentId AND
		CPID.CarePlanId = CP.Id
		
	WHERE CP.ResidentId = @ResidentId
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare]
@ResidentId int
AS
--20180610 chime created
BEGIN
	SELECT
	CP.Id,
	CP.ResidentId,
	CP.Assessed,
	CP.LevelOfCare,
	CP.CompleteStatus,
	CP.EnteredBy,
	CP.DateEntered
	FROM 
		[tbl_AB_CarePlan] CP 
	LEFT OUTER JOIN 
		[tbl_AB_CarePlan_VitalSigns] VitalSigns ON
		VitalSigns.ResidentId = CP.ResidentId AND
		VitalSigns.CarePlanId = CP.Id
	
	WHERE CP.ResidentId = @ResidentId
END
GO
	

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_VitalSigns]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_VitalSigns]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_VitalSigns]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	VitalSigns.Id,
	VitalSigns.ResidentId,
	VitalSigns.CarePlanId,
	VitalSigns.BP_Systolic,
	VitalSigns.BP_Diastolic,
	VitalSigns.BP_DateCompleted,
	VitalSigns.Temperature,
	VitalSigns.Temp_DateCompleted,
	VitalSigns.WeightLBS,
	VitalSigns.Weight_DateCompleted,
	VitalSigns.Height_Feet,
	VitalSigns.Height_Inches,
	VitalSigns.Height_DateCompleted,
	VitalSigns.Pulse,
	VitalSigns.Pulse_DateCompleted,
	VitalSigns.PulseRegular
	FROM 
		[tbl_AB_CarePlan_VitalSigns] VitalSigns 
	WHERE VitalSigns.CarePlanId = @CarePlanId
END
GO


	

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_PersonalHygiene]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_PersonalHygiene]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_PersonalHygiene]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	PersonalHygiene.Id,
	PersonalHygiene.ResidentId,
	PersonalHygiene.CarePlanId,
	PersonalHygiene.AMCare,
	PersonalHygiene.PMCare,
	PersonalHygiene.Bathing,
	PersonalHygiene.AM_AssistedBy,
	PersonalHygiene.PM_AssistedBy,
	PersonalHygiene.Bathing_AssistedBy,
	PersonalHygiene.AM_AgencyName,
	PersonalHygiene.PM_AgencyName,
	PersonalHygiene.Bathing_AgencyName,
	PersonalHygiene.AM_PreferredTime,
	PersonalHygiene.PM_PreferredTime,
	PersonalHygiene.Bathing_PreferredTime,
	PersonalHygiene.AM_PreferredType,
	PersonalHygiene.PM_PreferredType,
	PersonalHygiene.Bathing_PreferredType,
	PersonalHygiene.PreferredDays	
	FROM 
		[tbl_AB_CarePlan_PersonalHygiene] PersonalHygiene 
	WHERE PersonalHygiene.CarePlanId = @CarePlanId
END
GO
	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_AssistanceWith]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_AssistanceWith]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_AssistanceWith]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	AW.Id,
	AW.ResidentId,
	AW.CarePlanId,
	AW.Dressing,
	AW.Dressing_PreferredTime,
	AW.NailCare,
	AW.NailCare_PreferredTime,
	AW.Shaving,
	AW.Shaving_PreferredTime,
	AW.FootCare,
	AW.FootCare_PreferredTime,
	AW.OralHygiene,
	AW.OralHygiene_PreferredTime,
	AW.Teeth
	FROM 
		[tbl_AB_CarePlan_AssistanceWith] AW 
	WHERE AW.CarePlanId = @CarePlanId
END
GO


	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Mobility]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Mobility]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Mobility]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPM.Id,
	CPM.ResidentId,
	CPM.CarePlanId,
	CPM.Mobility,
	CPM.Transfers,
	CPM.MechanicalLift,
	CPM.Lift,
	CPM.Walker,
	CPM.Walker_Type,
	CPM.WheelChair,
	CPM.WheelChair_Type,
	CPM.Cane,
	CPM.Cane_Type,
	CPM.Scooter,
	CPM.Scooter_Type,
	CPM.PT,
	CPM.PT_Frequency,
	CPM.PT_Provider,
	CPM.OT,
	CPM.OT_Frequency,
	CPM.OT_Provider
	FROM 
		[tbl_AB_CarePlan_Mobility] CPM 
	WHERE CPM.CarePlanId = @CarePlanId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Safety]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Safety]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Safety]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPS.Id,
	CPS.ResidentId,
	CPS.CarePlanId,
	CPS.SafetyPASD,
	CPS.Other,
	CPS.Rails,
	CPS.NightOnly
	FROM 
		[tbl_AB_CarePlan_Safety] CPS 
	WHERE CPS.CarePlanId = @CarePlanId
END
GO

	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_MealEscort]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_MealEscort]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_MealEscort]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPME.Id,
	CPME.ResidentId,
	CPME.CarePlanId,
	CPME.BreakFast,
	CPME.Lunch,
	CPME.Dinner
	FROM 
		[tbl_AB_CarePlan_MealEscort] CPME 
	WHERE CPME.CarePlanId = @CarePlanId
END
GO
	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Behaviour]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Behaviour]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Behaviour]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPB.Id,
	CPB.ResidentId,
	CPB.CarePlanId,
	CPB.Behavior,
	CPB.HarmToSelf,
	CPB.smoker,
	CPB.RiskOfWandering,
	CPB.CognitiveStatus,
	CPB.OtherInfo
	FROM 
		[tbl_AB_CarePlan_Behaviour] CPB 
	WHERE CPB.CarePlanId = @CarePlanId
END
GO
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_CognitiveFunction]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_CognitiveFunction]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_CognitiveFunction]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPCF.Id,
	CPCF.ResidentId,
	CPCF.CarePlanId,
	CPCF.CognitiveFunction
	FROM 
		[tbl_AB_CarePlan_CognitiveFunction] CPCF 
	WHERE CPCF.CarePlanId = @CarePlanId
END
GO

	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Orientation]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Orientation]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Orientation]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPO.Id,
	CPO.ResidentId,
	CPO.CarePlanId,
	CPO.IsPerson,
	CPO.IsPlace,
	CPO.IsTime,
	CPO.IsDementiaCare
	FROM 
		[tbl_AB_CarePlan_Orientation] CPO 
	WHERE CPO.CarePlanId = @CarePlanId
END
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Nutrition]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Nutrition]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Nutrition]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPN.Id,
	CPN.ResidentId,
	CPN.CarePlanId,
	CPN.NutritionStatus,
	CPN.Risk,
	CPN.AssistiveDevices,
	CPN.Texture,
	CPN.Other,
	CPN.Diet,
	CPN.OtherDiet,
	CPN.Notes,
	CPN.Allergies
	FROM 
		[tbl_AB_CarePlan_Nutrition] CPN 
	WHERE CPN.CarePlanId = @CarePlanId
END
GO

		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Meals]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Meals]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Meals]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPMeals.Id,
	CPMeals.ResidentId,
	CPMeals.CarePlanId,
	CPMeals.Appetite,
	CPMeals.BreakFast,
	CPMeals.Lunch,
	CPMeals.Dinner
	FROM 
		[tbl_AB_CarePlan_Meals] CPMeals 
	WHERE CPMeals.CarePlanId = @CarePlanId
END
GO


	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Elimination]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Elimination]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Elimination]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPE.Id,
	CPE.ResidentId,
	CPE.CarePlanId,
	CPE.Bladder,
	CPE.Bowel,
	CPE.NameCode,
	CPE.ContinenceProducts,
	CPE.AssistiveDevices,
	CPE.Supplier,
	CPE.AssessmentCompletedBy,
	CPE.AssessmentDate
	FROM 
		[tbl_AB_CarePlan_Elimination] CPE 
	WHERE CPE.CarePlanId = @CarePlanId
END
GO

	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Toileting]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Toileting]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Toileting]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPT.Id,
	CPT.ResidentId,
	CPT.CarePlanId,
	CPT.Bathroom,
	CPT.Commode,
	CPT.Bedpan,
	CPT.Toileting
	FROM 
		[tbl_AB_CarePlan_Toileting] CPT 
	WHERE CPT.CarePlanId = @CarePlanId
END
GO

	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Medication]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Medication]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Medication]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPMedication.Id,
	CPMedication.ResidentId,
	CPMedication.CarePlanId,
	CPMedication.Assistance,
	CPMedication.Administration,
	CPMedication.CompletedBy,
	CPMedication.Agency,
	CPMedication.Pharmacy,
	CPMedication.Allergies
	FROM 
		[tbl_AB_CarePlan_Medication] CPMedication 
	WHERE CPMedication.CarePlanId = @CarePlanId
END
GO
	

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_SensoryAbilities]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_SensoryAbilities]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_SensoryAbilities]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPSA.Id,
	CPSA.ResidentId,
	CPSA.CarePlanId,
	CPSA.Vision,
	CPSA.Hearing,
	CPSA.Communication,
	CPSA.Notes
	FROM 
		[tbl_AB_CarePlan_SensoryAbilities] CPSA 
	WHERE CPSA.CarePlanId = @CarePlanId
END
GO
	

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_WoundCare]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_WoundCare]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_WoundCare]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPWC.Id,
	CPWC.ResidentId,
	CPWC.CarePlanId,
	CPWC.WoundCare,
	CPWC.AssistedBy,
	CPWC.Agency
	FROM 
		[tbl_AB_CarePlan_WoundCare] CPWC 
	WHERE CPWC.CarePlanId = @CarePlanId
END
GO

		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_SkinCare]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_SkinCare]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_SkinCare]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPSC.Id,
	CPSC.ResidentId,
	CPSC.CarePlanId,
	CPSC.SkinCare,
	CPSC.SpecialTreatments
	FROM 
		[tbl_AB_CarePlan_SkinCare] CPSC 
	WHERE CPSC.CarePlanId = @CarePlanId
END
GO
	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_SpecialNeeds]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_SpecialNeeds]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_SpecialNeeds]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPSN.Id,
	CPSN.ResidentId,
	CPSN.CarePlanId,
	CPSN.Oxygen,
	CPSN.Oxygen_Supplier,
	CPSN.Oxygen_Rate,
	CPSN.Oxygen_Notes,
	CPSN.CPAP,
	CPSN.CPAP_Supplier,
	CPSN.CPAP_Notes
	FROM 
		[tbl_AB_CarePlan_SpecialNeeds] CPSN 
	WHERE CPSN.CarePlanId = @CarePlanId
END
GO

		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_SpecialEquipment]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_SpecialEquipment]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_SpecialEquipment]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPSE.Id,
	CPSE.ResidentId,
	CPSE.CarePlanId,
	CPSE.SpecialEquipment,
	CPSE.Details
	FROM 
		[tbl_AB_CarePlan_SpecialEquipment] CPSE 
	WHERE CPSE.CarePlanId = @CarePlanId
END
GO	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_FamilySupport]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_FamilySupport]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_FamilySupport]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPFS.Id,
	CPFS.ResidentId,
	CPFS.CarePlanId,
	CPFS.FamilyMeeting,
	CPFS.FamilyInvolvment
	FROM 
		[tbl_AB_CarePlan_FamilySupport] CPFS 
	WHERE CPFS.CarePlanId = @CarePlanId
END
GO	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Immunization]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Immunization]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Immunization]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPI.Id,
	CPI.ResidentId,
	CPI.CarePlanId,
	CPI.TB,
	CPI.TB_Date,
	CPI.ChestXRay,
	CPI.ChestXRay_Date,
	CPI.Pneumonia,
	CPI.Pneumonia_Date,
	CPI.FluVaccine,
	CPI.FluVaccine_Date,
	CPI.Tetanus,
	CPI.Tetanus_Date
	FROM 
		[tbl_AB_CarePlan_Immunization] CPI 
	WHERE CPI.CarePlanId = @CarePlanId
END
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_InfectiousDiseases]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_InfectiousDiseases]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_InfectiousDiseases]
@CarePlanId int
AS
--20180610 chime created
BEGIN
	SELECT
	CPID.Id,
	CPID.ResidentId,
	CPID.CarePlanId,
	CPID.MRSA,
	CPID.MRSA_Diagnosed_Date,
	CPID.MRSA_Resolved_Date,
	CPID.VRE,
	CPID.VRE_Diagnosed_Date,
	CPID.VRE_Resolved_Date,
	CPID.CDiff,
	CPID.CDiff_Diagnosed_Date,
	CPID.CDiff_Resolved_Date,
	CPID.Other,
	CPID.Other_Diagnosed_Date,
	CPID.Other_Resolved_Date
	FROM 
		[tbl_AB_CarePlan_InfectiousDiseases] CPID 
	WHERE CPID.CarePlanId = @CarePlanId
END
GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Add_PlanOfCare]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Add_PlanOfCare]
GO
CREATE PROCEDURE [dbo].[spAB_Add_PlanOfCare]
@ResidentId int,
@Assessed nvarchar(50),
@LevelOfCare nvarchar(50),
@CompleteStatus nvarchar(20),
@EnteredBy int
AS
--20180610 chime created
BEGIN
	INSERT INTO tbl_ab_careplan (ResidentId, Assessed, LevelOfCare, CompleteStatus, EnteredBy, DateEntered) VALUES (@ResidentId, @Assessed, @LevelOfCare, @CompleteStatus, @EnteredBy, getdate())
END
GO
	

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_VitalSigns]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_VitalSigns]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_VitalSigns]
@CarePlanId int,
@ResidentId int,
@BP_Systolic nvarchar(20),
@BP_Diastolic nvarchar(20),
@BP_DateCompleted nvarchar(20),
@Temperature nvarchar(20),
@Temp_DateCompleted nvarchar(20),
@WeightLBS nvarchar(20),
@Weight_DateCompleted nvarchar(20),
@Height_Feet nvarchar(20),
@Height_Inches nvarchar(20),
@Height_DateCompleted nvarchar(20),
@Pulse nvarchar(20),
@Pulse_DateCompleted nvarchar(20),
@PulseRegular nvarchar(20)
AS
--20180610 chime created
BEGIN
	INSERT INTO tbl_AB_CarePlan_VitalSigns (ResidentId, CarePlanId, BP_Systolic, BP_Diastolic, BP_DateCompleted, Temperature, Temp_DateCompleted, WeightLBS, Weight_DateCompleted, Height_Feet,
				Height_Inches, Height_DateCompleted, Pulse, Pulse_DateCompleted, PulseRegular)
	VALUES
	(@ResidentId, @CarePlanId, @BP_Systolic, @BP_Diastolic, @BP_DateCompleted, @Temperature, @Temp_DateCompleted, @WeightLBS, @Weight_DateCompleted, @Height_Feet,
				@Height_Inches, @Height_DateCompleted, @Pulse, @Pulse_DateCompleted, @PulseRegular)
END
GO


	

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_PersonalHygiene]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_PersonalHygiene]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_PersonalHygiene]
@CarePlanId int,
@ResidentId int,
@AMCare nvarchar(20),
@PMCare nvarchar(20),
@Bathing nvarchar(20),
@AM_AssistedBy nvarchar(20),
@PM_AssistedBy nvarchar(20),
@Bathing_AssistedBy nvarchar(20),
@AM_AgencyName nvarchar(20),
@PM_AgencyName nvarchar(20),
@Bathing_AgencyName nvarchar(20),
@AM_PreferredTime nvarchar(20),
@PM_PreferredTime nvarchar(20),
@Bathing_PreferredTime nvarchar(20),
@AM_PreferredType nvarchar(20),
@PM_PreferredType nvarchar(20),
@Bathing_PreferredType nvarchar(20),
@PreferredDays nvarchar(200)
AS
--20180610 chime created
BEGIN
	INSERT INTO tbl_AB_CarePlan_PersonalHygiene (ResidentId, CarePlanId, AMCare, PMCare, Bathing, AM_AssistedBy, PM_AssistedBy, Bathing_AssistedBy, AM_AgencyName, PM_AgencyName, Bathing_AgencyName,
					AM_PreferredTime, PM_PreferredTime, Bathing_PreferredTime, AM_PreferredType, PM_PreferredType, Bathing_PreferredType, PreferredDays	)
	VALUES
	(@ResidentId, @CarePlanId, @AMCare, @PMCare, @Bathing, @AM_AssistedBy, @PM_AssistedBy, @Bathing_AssistedBy, @AM_AgencyName, @PM_AgencyName, @Bathing_AgencyName,
					@AM_PreferredTime, @PM_PreferredTime, @Bathing_PreferredTime, @AM_PreferredType, @PM_PreferredType, @Bathing_PreferredType, @PreferredDays	)
END
GO
	
		
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_AssistanceWith]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_AssistanceWith]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_AssistanceWith]
@CarePlanId int,
@ResidentId int,
@Dressing nvarchar(20),
@Dressing_PreferredTime nvarchar(20),
@NailCare nvarchar(20),
@NailCare_PreferredTime nvarchar(20),
@Shaving nvarchar(20),
@Shaving_PreferredTime nvarchar(20),
@FootCare nvarchar(20),
@FootCare_PreferredTime nvarchar(20),
@OralHygiene nvarchar(20),
@OralHygiene_PreferredTime nvarchar(20),
@Teeth nvarchar(200)
AS
--20180610 chime created
BEGIN
	INSERT INTO tbl_AB_CarePlan_AssistanceWith (ResidentId, CarePlanId, Dressing, Dressing_PreferredTime, NailCare, NailCare_PreferredTime, Shaving, Shaving_PreferredTime, FootCare, FootCare_PreferredTime,
			OralHygiene, OralHygiene_PreferredTime, Teeth )
	VALUES (@ResidentId, @CarePlanId, @Dressing, @Dressing_PreferredTime, @NailCare, @NailCare_PreferredTime, @Shaving, @Shaving_PreferredTime, @FootCare, @FootCare_PreferredTime,
			@OralHygiene, @OralHygiene_PreferredTime, @Teeth )
END
GO


	
		

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[spAB_Get_PlanOfCare_Mobility]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[spAB_Get_PlanOfCare_Mobility]
GO
CREATE PROCEDURE [dbo].[spAB_Get_PlanOfCare_Mobility]
@CarePlanId int,
@ResidentId INT,
@Mobility nvarchar(20),
@Transfers nvarchar(20),
@MechanicalLift nvarchar(20),
@Lift nvarchar(20),
@Walker nvarchar(20),
@Walker_Type nvarchar(20),
@WheelChair nvarchar(20),
@WheelChair_Type nvarchar(20),
@Cane nvarchar(20),
@Cane_Type nvarchar(20),
@Scooter nvarchar(20),
@Scooter_Type nvarchar(20),
@PT nvarchar(20),
@PT_Frequency nvarchar(20),
@PT_Provider nvarchar(20),
@OT nvarchar(20),
@OT_Frequency nvarchar(20),
@OT_Provider nvarchar(20)
AS
--20180610 chime created
BEGIN
	INSERT INTO tbl_AB_CarePlan_Mobility (ResidentId, CarePlanId, Mobility, Transfers, MechanicalLift, Lift, Walker, Walker_Type, WheelChair, WheelChair_Type, Cane, Cane_Type, Scooter, Scooter_Type,
				PT, PT_Frequency, PT_Provider, OT, OT_Frequency, OT_Provider)
	VALUES(@ResidentId, @CarePlanId, @Mobility, @Transfers, @MechanicalLift, @Lift, @Walker, @Walker_Type, @WheelChair, @WheelChair_Type, @Cane, @Cane_Type, @Scooter, @Scooter_Type,
				@PT, @PT_Frequency, @PT_Provider, @OT, @OT_Frequency, @OT_Provider)
END
GO
