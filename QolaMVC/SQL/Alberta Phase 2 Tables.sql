
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_ActivityCategory]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_ActivityCategory]
GO

CREATE TABLE [dbo].[tbl_AB_ActivityCategory](
	Id [int] IDENTITY(1,1) NOT NULL,
	CategoryNameEnglish nvarchar(200),
	CategoryNameFrench nvarchar(200),
	MemoryCareColor nvarchar(200),
	PRIMARY KEY (Id)
	)
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_Activity]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_Activity]
GO

CREATE TABLE [dbo].[tbl_AB_Activity](
	Id [int] IDENTITY(1,1) NOT NULL,
	CategoryId int,
	ActivityNameEnglish nvarchar(200),
	ActivityNameFrench nvarchar(200),
	ActivityColor nvarchar(200),
	FunPicture nvarchar(200),
	Province nvarchar(200),
	ShowInAssessment bit,
	ActivityDisplayTitle nvarchar(200),
	PRIMARY KEY (Id)
	)
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_ActivityAssessment]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_ActivityAssessment]
GO

CREATE TABLE [dbo].[tbl_AB_ActivityAssessment](
	Id [int] IDENTITY(1,1) NOT NULL,
	AssessmentId int,
	ActivityId int,
	ActivityCategoryId int,
	SuggestedActivity nvarchar(max),
	CheckedValue nvarchar(3),
	ResidentId int,
	DateEntered datetime,
	PRIMARY KEY (Id)
	)
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_ActivityAssessment_Store]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_ActivityAssessment_Store]
GO

CREATE TABLE [dbo].[tbl_AB_ActivityAssessment_Store](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId int,
	EnteredBy int,
	DateEntered datetime,
	PRIMARY KEY (Id)
	)
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_ActivityEvents]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_ActivityEvents]
GO

CREATE TABLE [dbo].[tbl_AB_ActivityEvents](
	Id [int] IDENTITY(1,1) NOT NULL,
	ActivityId int,
	EventTitle nvarchar(200),
	StartDate datetime,
	EndDate datetime,
	StartTime nvarchar(20),
	EndTime nvarchar(20),
	note nvarchar(max),
	PRIMARY KEY (Id)
	)
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_FallRiskAssessment]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_FallRiskAssessment]
GO

CREATE TABLE [dbo].[tbl_AB_FallRiskAssessment](
	Id [int] IDENTITY(1,1) NOT NULL,
	FallHistory_IsTwoOrMore bit,
	FallHistory_IsOneOrTwo bit,
	Neurological_IsCVA bit,
	Neurological_IsParkinsons bit,
	Neurological_IsAlzheimers bit,
	Neurological_IsSeizureDisorder bit,
	Neurological_IsOther bit,
	Other_IsDiabetes bit,
	Other_IsOsteoporosis bit,
	Other_IsPosturalHypotension bit,
	Other_IsSyncope bit,
	Incontinence_IsBowel bit,
	Incontinence_IsBladder bit,
	Incontinence_IsTransfer bit,
	Incontinence_IsUnsteady bit,
	Medication_IsCardiac bit,
	Medication_IsDiuretics bit,
	Medication_IsNarcotics bit,
	Medication_IsAnalgesics bit,
	Medication_IsSedatives bit,
	Medication_IsAntiAnxiety bit,
	Medication_IsLaxatives bit,
	MentalStatus_IsConfused bit,
	MentalStatus_IsResidentNonCompliance bit,
	Orthopedic_IsRecent bit,
	Orthopedic_IsCast bit,
	Orthopedic_IsAmputation bit,
	Orthopedic_IsSevere bit,
	Sensory_IsDecreasedVision bit,
	Sensory_IsDecreasedHearing bit,
	Sensory_IsAphasia bit,
	Assistive_IsWheelChair bit,
	Assistive_IsWalker bit,
	Assistive_IsCane bit,
	TotalScore int,
	RiskLevel nvarchar(10),
	ResidentId int,
	DateEntered datetime,
	EnteredBy int,
	PRIMARY KEY (Id)
	)
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_ActivityEvents_C2]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_ActivityEvents_C2]
GO

CREATE TABLE [dbo].[tbl_AB_ActivityEvents_C2](
	Id [int] IDENTITY(1,1) NOT NULL,
	ActivityId int,
	EventTitle nvarchar(200),
	StartDate datetime,
	EndDate datetime,
	StartTime nvarchar(20),
	EndTime nvarchar(20),
	note nvarchar(max),
	PRIMARY KEY (Id)
	)
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_ActivityEvents_C3]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_ActivityEvents_C3]
GO

CREATE TABLE [dbo].[tbl_AB_ActivityEvents_C3](
	Id [int] IDENTITY(1,1) NOT NULL,
	ActivityId int,
	EventTitle nvarchar(200),
	StartDate datetime,
	EndDate datetime,
	StartTime nvarchar(20),
	EndTime nvarchar(20),
	note nvarchar(max),
	PRIMARY KEY (Id)
	)
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_AB_ActivityEvents_C4]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_AB_ActivityEvents_C4]
GO

CREATE TABLE [dbo].[tbl_AB_ActivityEvents_C4](
	Id [int] IDENTITY(1,1) NOT NULL,
	ActivityId int,
	EventTitle nvarchar(200),
	StartDate datetime,
	EndDate datetime,
	StartTime nvarchar(20),
	EndTime nvarchar(20),
	note nvarchar(max),
	PRIMARY KEY (Id)
	)
GO

