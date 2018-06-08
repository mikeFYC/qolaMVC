USE [test_qola]
GO

/****** Object:  StoredProcedure [dbo].[spAB_Get_CarePlan]    Script Date: 2018-06-08 4:01:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAB_Get_CarePlan]
@ResidentId int
AS
--20180507 chime created
BEGIN
	SELECT
	Id,
	AssessedStatus, 
	LevelOfCare, 
	Bpsystolic, 
	BpDiastolic, 
	BpDateCompleted, 
	Temperature, 
	TemperatureDateCompleted, 
	Weight_Lbs,
	 WeightDateCompleted, 
	 HeightCentimeters, 
	 HeightInches, 
	 HeightDateCompleted, 
	 Pulse, 
	 PulsedateCompleted, 
	 Oxygen_02, 
	 Oxygen_02_rate, 
	 ResidentId, 
	 date_added
	FROM tbl_CarePlan
	WHERE ResidentId = @ResidentId
END
GO


