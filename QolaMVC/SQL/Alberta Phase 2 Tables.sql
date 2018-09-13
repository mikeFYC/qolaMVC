
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