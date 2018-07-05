	
GO
CREATE TABLE [dbo].[tbl_AB_CarePlan](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	Assessed [nvarchar](200) null,
	LevelOfCare [nvarchar](200) null,
	CompleteStatus [nvarchar](20) null,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

	
GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_VitalSigns](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	BP_Systolic [nvarchar](20) null,
	BP_Diastolic [nvarchar](20) null,
	BP_DateCompleted nvarchar(20) null,

	Temperature [nvarchar](20) null,
	Temp_DateCompleted nvarchar(20) null,

	WeightLBS [nvarchar](20) null,
	Weight_DateCompleted nvarchar(20) null,
	
	Height_Feet [nvarchar](20) null,
	Height_Inches [nvarchar](20) null,
	Height_DateCompleted nvarchar(20) null,

	Pulse [nvarchar](20) null,
	Pulse_DateCompleted nvarchar(20) null,
	PulseRegular nvarchar(20) null,

	EnteredBy [int] null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

	
GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_PersonalHygiene](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	AMCare [nvarchar](20) null,
	PMCare [nvarchar](20) null,
	Bathing nvarchar(20) null,

	AM_AssistedBy [nvarchar](20) null,
	PM_AssistedBy nvarchar(20) null,
	Bathing_AssistedBy nvarchar(20) null,

	AM_AgencyName [nvarchar](200) null,
	PM_AgencyName nvarchar(200) null,
	Bathing_AgencyName nvarchar(200) null,

	AM_PreferredTime [nvarchar](20) null,
	PM_PreferredTime nvarchar(20) null,
	Bathing_PreferredTime nvarchar(20) null,

	AM_PreferredType [nvarchar](20) null,
	PM_PreferredType nvarchar(20) null,
	Bathing_PreferredType nvarchar(20) null,

	PreferredDays nvarchar(max) null,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

	
GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_AssistanceWith](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Dressing [nvarchar](50) null,
	Dressing_PreferredTime [nvarchar](20) null,
	
	NailCare [nvarchar](50) null,
	NailCare_PreferredTime [nvarchar](20) null,
	
	Shaving [nvarchar](50) null,
	Shaving_PreferredTime [nvarchar](20) null,
	
	FootCare [nvarchar](50) null,
	FootCare_PreferredTime [nvarchar](20) null,
	
	OralHygiene [nvarchar](50) null,
	OralHygiene_PreferredTime [nvarchar](20) null,
	
	Teeth nvarchar(max) null,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Mobility](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Mobility [nvarchar](50) null,
	Transfers [nvarchar](20) null,
	
	MechanicalLift [nvarchar](50) null,
	Lift [nvarchar](20) null,
	
	Walker[nvarchar](50) null,
	Walker_Type [nvarchar](20) null,
	
	WheelChair [nvarchar](50) null,
	WheelChair_Type [nvarchar](20) null,
	
	Cane [nvarchar](50) null,
	Cane_Type [nvarchar](20) null,
	
	Scooter [nvarchar](50) null,
	Scooter_Type [nvarchar](20) null,
	
	PT [nvarchar](50) null,
	PT_Frequency [nvarchar](50) null,
	PT_Provider [nvarchar](50) null,
	
	OT [nvarchar](50) null,
	OT_Frequency [nvarchar](50) null,
	OT_Provider [nvarchar](50) null,

	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO



GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Safety](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	SafetyPASD [nvarchar](50) null,
	Other [nvarchar](20) null,
	
	Rails [nvarchar](50) null,
	NightOnly bit null,

	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_MealEscort](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	BreakFast [nvarchar](20) null,
	Lunch [nvarchar](20) null,
	Dinner [nvarchar](20) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Behaviour](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Behavior [nvarchar](max) null,
	HarmToSelf bit null,
	smoker bit null,
	RiskOfWandering bit null,
	CognitiveStatus [nvarchar](20) null,
	OtherInfo nvarchar(max) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_CognitiveFunction](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	CognitiveFunction [nvarchar](max) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Orientation](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	IsPerson bit null,
	IsPlace bit null,
	IsTime bit null,
	IsDementiaCare bit null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Nutrition](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	NutritionStatus [nvarchar](20) null,
	Risk [nvarchar](20) null,
	AssistiveDevices [nvarchar](20) null,
	Texture [nvarchar](20) null,
	Other [nvarchar](20) null,
	Diet [nvarchar](max) null,
	OtherDiet [nvarchar](20) null,
	Notes [nvarchar](max) null,
	Allergies nvarchar(max) null,
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Meals](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Appetite [nvarchar](20) null,
	BreakFast [nvarchar](20) null,
	Lunch [nvarchar](20) null,
	Dinner [nvarchar](20) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Elimination](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Bladder [nvarchar](max) null,
	Bowel [nvarchar](max) null,
	
	NameCode [nvarchar](100) null,
	ContinenceProducts [nvarchar](100) null,
	AssistiveDevices [nvarchar](100) null,
	Supplier [nvarchar](100) null,
	AssessmentCompletedBy [nvarchar](50) null,
	AssessmentDate [nvarchar](20) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO

GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Toileting](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Bathroom [nvarchar](100) null,
	Commode [nvarchar](100) null,
	Bedpan [nvarchar](100) null,

	Toileting [nvarchar](20) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Medication](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Assistance [nvarchar](50) null,
	Administration [nvarchar](20) null,
	CompletedBy [nvarchar](20) null,
	Agency [nvarchar](100) null,
	Pharmacy [nvarchar](100) null,
	Allergies [nvarchar](max) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_SensoryAbilities](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Vision [nvarchar](200) null,
	Hearing [nvarchar](200) null,
	Communication [nvarchar](200) null,
	Notes [nvarchar](max) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_WoundCare](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	WoundCare [nvarchar](20) null,
	AssistedBy [nvarchar](100) null,
	Agency [nvarchar](200) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_SkinCare](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	SkinCare [nvarchar](20) null,
	SpecialTreatments [nvarchar](100) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_SpecialNeeds](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	Oxygen [nvarchar](20) null,
	Oxygen_Supplier [nvarchar](100) null,
	Oxygen_Rate [nvarchar](100) null,
	Oxygen_Notes [nvarchar](200) null,
	
	CPAP [nvarchar](20) null,
	CPAP_Supplier [nvarchar](100) null,
	CPAP_Notes [nvarchar](200) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_SpecialEquipment](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	SpecialEquipment [nvarchar](200) null,
	Details [nvarchar](200) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_FamilySupport](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	FamilyMeeting [nvarchar](20) null,
	FamilyInvolvment [nvarchar](20) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_Immunization](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	TB [nvarchar](20) null,
	TB_Date [nvarchar](20) null,
	
	ChestXRay [nvarchar](20) null,
	ChestXRay_Date [nvarchar](20) null,
	
	Pneumonia [nvarchar](20) null,
	Pneumonia_Date [nvarchar](20) null,
	
	FluVaccine [nvarchar](20) null,
	FluVaccine_Date [nvarchar](20) null,
	
	Tetanus [nvarchar](20) null,
	Tetanus_Date [nvarchar](20) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO


GO
CREATE TABLE [dbo].[tbl_AB_CarePlan_InfectiousDiseases](
	Id [int] IDENTITY(1,1) NOT NULL,
	ResidentId [int] NOT NULL,
	CarePlanId [int] NOT NULL,
	
	MRSA [nvarchar](20) null,
	MRSA_Diagnosed_Date [nvarchar](20) null,
	MRSA_Resolved_Date [nvarchar](20) null,
	
	VRE [nvarchar](20) null,
	VRE_Diagnosed_Date [nvarchar](20) null,
	VRE_Resolved_Date [nvarchar](20) null,
	
	CDiff [nvarchar](20) null,
	CDiff_Diagnosed_Date [nvarchar](20) null,
	CDiff_Resolved_Date [nvarchar](20) null,
	
	Other [nvarchar](20) null,
	Other_Diagnosed_Date [nvarchar](20) null,
	Other_Resolved_Date [nvarchar](20) null,
	
	EnteredBy [int] not null,
	DateEntered [datetime],
	PRIMARY KEY (Id)
	)
GO