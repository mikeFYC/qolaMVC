IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_HSEP_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_HSEP_Detail]
GO
CREATE TABLE [dbo].[tbl_AB_HSEP_Detail](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	ActivityName [nvarchar](200) not null,
	DateOfTeaching [datetime] null,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_Excercise_Activity_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_Excercise_Activity_Detail]
GO
CREATE TABLE [dbo].[tbl_AB_Excercise_Activity_Detail](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	ActivityName [nvarchar](200) not null,
	WeekId int not null,
	Sunday [bit] null,
	Monday [bit] null,
	Tuesday [bit] null,
	Wednesday [bit] null,
	Thursday [bit] null,
	Friday [bit] null,
	Saturday [bit] null,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_Excercise_Activity_Summary]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_Excercise_Activity_Summary]
GO
CREATE TABLE [dbo].[tbl_AB_Excercise_Activity_Summary](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	BaselineDate [nvarchar](200) null,
	BaselineTug [nvarchar](200) null,
	BaselineVPS [nvarchar](200) NULL,
	TMonthDate [nvarchar](200) null,
	TMonthTug [nvarchar](200) null,
	TMonthVPS [nvarchar](200) NULL,
	SMonthDate [nvarchar](200) null,
	SMonthTug [nvarchar](200) null,
	SMonthVPS [nvarchar](200) NULL,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO



--create Head to toe assessment table
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_Admission_Head_To_Toe_Assessment]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_Admission_Head_To_Toe_Assessment]
GO
CREATE TABLE [dbo].[tbl_AB_Admission_Head_To_Toe_Assessment](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	dtmDate [datetime] NOT NULL,
	AdmissionStatus bit,
	ReturnedFromHospital [varchar](max) NULL,
	DiagnosisFromHospital [varchar](max) NULL,
	Medications [varchar](max) NULL,
	BP [nvarchar](10) null,
	BPLocation [nvarchar](20) null,
	RedialPulse [nvarchar](10) null,
	PulseLocation [nvarchar](20) null,
	Temp [nvarchar](10) null,
	TempLocation [nvarchar](20) null,
	Resp [nvarchar](10) null,
	RespLocation [nvarchar](20) null,
	SP02 [nvarchar](10) null,
	SP02Location [nvarchar](20) null,
	Person [nvarchar](10) null,
	Place [nvarchar](10) null,
	strTime [nvarchar](10) null,
	Speech [nvarchar](10) null,
	PrimaryLanguage [nvarchar](10) null,
	PulpilsEquals [nvarchar](10) null,
	PulpilsReactive [nvarchar](10) null,
	Eyes [nvarchar](10) null,
	GeneralFace [nvarchar](10) null,
	DateEntered datetime,
	EnteredBy int,
	PRIMARY KEY (Id)
	)
GO




--create table for family conference Notes

--create table for bowel movement tracking
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_FamilyConverenceNotes]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_FamilyConverenceNotes]
GO
CREATE TABLE [dbo].[tbl_AB_FamilyConverenceNotes](
	Id int NOT NULL Identity,
	ResidentId int NOT NULL,
	dtmDate datetime,
	SuiteNumber nvarchar(10) not null,
	PHN nvarchar(20),
	CareAndGCD nvarchar(max),
	Medication nvarchar(max),
	Recreation nvarchar(max),
	Diet nvarchar(max),
	Comments nvarchar(max),
	Goals nvarchar(max),
	Present1 nvarchar(255),
	Present2 nvarchar(255),
	Present3 nvarchar(255),
	DateEntered datetime,
	EnteredBy int not null
	PRIMARY KEY (Id)
	)
GO




--create table for bowel movement tracking
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_BowelMovementTracking]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_BowelMovementTracking]
GO
CREATE TABLE [dbo].[tbl_AB_BowelMovementTracking](
	Id int NOT NULL Identity,
	ResidentId int NOT NULL,
	strType nvarchar(15) NOT NULL,
	ObservedBy nvarchar(15) NOT NULL,
	Initials nvarchar(15) NOT NULL,
	EnteredBy int not null,
	strPeriod nvarchar(20),
	dtmTimeStamp datetime
	PRIMARY KEY (Id)
	)

	
/****** Object:  Table [dbo].[tbl_AB_Dietary_Assessment]    Script Date: 4/24/2018 10:06:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_Dietary_Assessment]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_Dietary_Assessment]
GO
CREATE TABLE [dbo].[tbl_AB_Dietary_Assessment](
	[fd_id] [int] NOT NULL,
	[fd_resident_id] [int] NOT NULL,
	[fd_sl_no] [int] NOT NULL,
	[fd_special_diat] [varchar](100) NULL,
	[fd_allergy] [varchar](100) NULL,
	[fd_likes] [varchar](500) NULL,
	[fd_dislikes] [varchar](500) NULL,
	[fd_modified_by] [int] NOT NULL,
	[fd_modified_on] [datetime] NOT NULL,
	[fd_nutritional] [varchar](100) NULL,
	[fd_appetite] [varchar](100) NULL,
	[fd_risk_assistive_device] [varchar](50) NULL,
	[fd_nutrition] [varchar](100) NULL,
	[fd_nutrition_diet_other] [varchar](20) NULL,
	[fd_nutrition_texture] [varchar](20) NULL,
	[fd_nutrition_diet_note] [varchar](200) NULL,
	[fd_view] [char](1) NULL,
	[fd_viewed_by] [int] NULL,
	[fd_viewed_on] [datetime] NULL,
	[fd_known_allergy] [char](1) NULL,
	[fd_nutrition_different] [varchar](100) NULL,
 CONSTRAINT [PK_tbl_Dietary_Assessment] PRIMARY KEY CLUSTERED 
(
	[fd_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_AB_Dietary_Assessment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Dietary_Assessment_tbl_Resident] FOREIGN KEY([fd_resident_id])
REFERENCES [dbo].[tbl_Resident] ([fd_id])
GO

ALTER TABLE [dbo].[tbl_AB_Dietary_Assessment] CHECK CONSTRAINT [FK_tbl_Dietary_Assessment_tbl_Resident]
GO

ALTER TABLE [dbo].[tbl_AB_Dietary_Assessment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Dietary_Assessment_tbl_User] FOREIGN KEY([fd_modified_by])
REFERENCES [dbo].[tbl_User] ([fd_id])
GO

ALTER TABLE [dbo].[tbl_AB_Dietary_Assessment] CHECK CONSTRAINT [FK_tbl_Dietary_Assessment_tbl_User]
GO


	
GO
--create progress notes table
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_Progress_Notes]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_Progress_Notes]
GO
CREATE TABLE [dbo].[tbl_AB_Progress_Notes](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	dtmDate [datetime] NOT NULL,
	Title [varchar](100) NOT NULL,
	Note [varchar](max) NULL,
	Status [char](1) NOT NULL,
	ModifiedBy [int] NOT NULL,
	ModifiedOn [datetime] NOT NULL,
	Category [tinyint] NULL,
	RemainIn [tinyint] NULL,
	AcknowledgedBy [int] NULL,
	AcknowledgedOn [datetime] NULL,
	ActionNote [varchar](300) NULL,
	FallDateType [char](1) NULL,
	FallDate [datetime] NULL,
	Location [varchar](100) NULL,
	WitnessType [char](1) NULL,
	WitnessFall [varchar](100) NULL,
	UnWitnesType [char](1) NULL,
	IncidentReport [char](1) NULL
	PRIMARY KEY (Id)
	)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_DietaryAssessment]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_DietaryAssessment]
GO
CREATE TABLE [dbo].[tbl_AB_DietaryAssessment](
	Id int NOT NULL Identity,
	ResidentId int NOT NULL,
	NutritionalStatus nvarchar(200) NULL,
	Risk nvarchar(200) NULL,
	AssistiveDevices nvarchar(200) NULL,
	Texture nvarchar(200) NULL,
	Apetite nvarchar(200) NULL,
	Other nvarchar(200) NULL,
	Likes nvarchar(max) NULL,
	DisLikes nvarchar(max) NULL,
	Notes nvarchar(max) NULL,
	EnteredBy int not null,
	DateEntered datetime
	PRIMARY KEY (Id)
	)
	GO

	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_Diet]') AND type in (N'U'))
	DROP TABLE [dbo].[tbl_AB_Diet]
	GO
	CREATE TABLE [dbo].[tbl_AB_Diet](
	Id int NOT NULL Identity,
	ResidentId int NOT NULL,
	AssessmentId int NOT NULL,
	Diet nvarchar(200) NOT NULL,
	EnteredBy int not null,
	DateEntered datetime
	PRIMARY KEY (Id)
	)
	GO


	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_DietaryAssessment_Allergy]') AND type in (N'U'))
	DROP TABLE [dbo].[tbl_AB_DietaryAssessment_Allergy]
	GO
	CREATE TABLE [dbo].[tbl_AB_DietaryAssessment_Allergy](
	Id int NOT NULL Identity,
	ResidentId int NOT NULL,
	AssessmentId int,
	AllergyId int,
	Allergy nvarchar(200) NOT NULL,
	EnteredBy int not null,
	DateEntered datetime
	PRIMARY KEY (Id)
	)
	GO

	
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_UnusualIncident]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_UnusualIncident]
GO
CREATE TABLE [dbo].[tbl_AB_UnusualIncident](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	strLocation [nvarchar](200) null,
	Employee [nvarchar](200) null,
	Dept [nvarchar](200) null,
	Visitor [nvarchar](200) null,
	Room [nvarchar](200) null,
	Other [nvarchar](200) null,
	WasWitnessed [nvarchar](200) null,
	WitnessName [nvarchar](200) null,
	IsFall bit null,
	IsElopement bit null,
	ElopementValue nvarchar(50) null,
	IsUnusualBehavior bit null,
	UnusualBehaviorvalue nvarchar(50),
	IsPhysicalInjury bit null,
	PhysicalInjuryValue nvarchar(50) null,
	IsPropertyLoss bit null,
	PropertyLossValue nvarchar(50) null,
	IsSuspicious bit null,
	SuspicionValue nvarchar(50) null,
	IsTreatment bit null,
	TreatmentValue nvarchar(50) null,
	IsOther bit null,
	SectionD nvarchar(max) null,
	SectionE nvarchar(max) null,
	SectionF nvarchar(max) null,
	SectionH nvarchar(max) null,
	IncidentDocumented nvarchar(20) null,
	ChangesMade nvarchar(20) null,
	ReferralConsult nvarchar(20) null,
	OHSCommitteeInformed nvarchar(20) null,
	RecordTrackingForm nvarchar(20) null,
	IncidentInformation nvarchar(20) null,
	SectionJ nvarchar(max) null,

	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

	
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_UnusualIncident_SectionG]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_UnusualIncident_SectionG]
GO
CREATE TABLE [dbo].[tbl_AB_UnusualIncident_SectionG](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	IncidentId [int] NOT null,
	Notify [nvarchar](200) null,
	strName [nvarchar](200) null,
	dtmDate [nvarchar](200) null,
	ByWhom [nvarchar](200) null,
	Via [nvarchar](200) null,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO