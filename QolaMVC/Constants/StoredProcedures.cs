using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Constants
{
    public class StoredProcedureName
    {

        public const string USP_ADD_HOME = "Add_Home";
        public const string USP_GET_HOME = "Get_Home";
        public const string USP_GET_PROVINCE_HOMES = "Get_Province_Homes";
        public const string USP_UPDATE_HOME = "Update_Home";
        public const string USP_REMOVE_HOME = "Remove_Home";
        public const string USP_GET_HOME_BY_ID = "Get_Home_By_Id";
        public const string USP_GET_HOME_BY_PROVINCE = "Get_Home_By_Provice";
        public const string USP_GET_PROVINCE = "Get_Province";
        public const string USP_GET_HOME_BY_USER = "Get_Home_By_User";
        public const string USP_VALIDATE_HOME_BY_USERID = "Validate_Home_By_UserId";


        public const string USP_ADD_SUITE = "Add_suite";
        public const string USP_GET_SUITE = "Get_suite";
        public const string USP_UPDATE_SUITE = "Update_Suite";
        public const string USP_REMOVE_SUITE = "Remove_suite";
        public const string USP_GET_SUITE_BY_ID = "Get_Suite_By_Id";
        public const string USP_GET_SUITE_BY_HOME_ID = "Get_Suite_By_home_Id";
        public const string USP_GET_AVAILABLE_SUITE_BY_HOME_ID = "Get_Available_Suite_By_home_Id";
        public const string USP_GET_AVAILABLE_WITH_EXISTS_SUITE = "Get_Available_Suite_By_home_Id_and_Resident";
        public const string USP_GET_AVAILABLE_SUITE_BY_HOME_ID_FOR_UPDATE = "Get_Available_Suite_By_home_Id_For_Update";


        public const string USP_ADD_ALLERGIES = "Add_Allergies";
        public const string USP_UPDATE_ALLERGIES = "Update_Allergies";
        public const string USP_REMOVE_ALLERGIES = "Remove_Allergies";
        public const string USP_GET_ALLERGIES = "Get_Allergies";
        public const string USP_GET_ALLERGIES_BY_ID = "Get_Allergies_By_Id";
        public const string USP_GET_ALLERGIES_BY_CATEGORY = "Get_Allergies_By_Category";
        public const string USP_GET_MEDICAL_ALLERGIES_BY_KEY = "Get_Allergy_Search_By_Id_Key";
        public const string USP_ADD_ALLERGIES_FROM_DIETARY_ASSESSMENT = "Add_Allergies_From_Dietary_Assessment";


        public const string USP_ADD_SPECIAL_DIET = "Add_SpecialDiet";
        public const string USP_UPDATE_SPECIAL_DIET = "Update_SpecialDiet";
        public const string USP_REMOVE_SPECIAL_DIET = "Remove_SpecialDiet";
        public const string USP_GET_SPECIAL_DIET = "Get_SpecialDiet";
        public const string USP_GET_SPECIAL_DIET_BY_ID = "Get_SpecialDiet_By_Id";
        public const string USP_GET_SPECIAL_DIET_BY_KEY = "Get_SpecialDiet_Search_By_Id_Key";
        public const string USP_ADD_SPECIAL_DIET_FROM_DIETARY_ASSESSMENT = "Add_SpecialDiet_From_Dietary_Assessment";


        public const string USP_ADD_USER_IP_MAC = "Add_User_IP_Mac";
        public const string USP_UPDATE_USER_IP_MAC = "Update_User_IP_Mac";
        public const string USP_REMOVE_USER_IP_MAC = "Remove_User_IP_Mac";
        public const string USP_GET_USER_IP_MAC = "Get_User_IP_Mac";
        public const string USP_GET_USER_IP_MAC_BY_ID = "Get_User_IP_Mac_By_Id";


        public const string USP_ADD_USER = "Add_User";
        public const string USP_UPDATE_USER = "Update_User";
        public const string USP_UPDATE_USER_WITHOUT_PASSWORD = "Update_User_Without_Password";
        public const string USP_REMOVE_USER = "Remove_User";
        public const string USP_GET_USER = "Get_User";
        public const string USP_GET_USER_BY_ID = "Get_User_By_Id";
        public const string USP_RESET_USER_PASSWORD = "Reset_User_Password";
        public const string USP_GET_BY_USER_ACTIVE = "Get_User_Active";
        public const string USP_GET_USER_BY_USERNAME_PASSWORD = "Get_User_By_Username_Password";
        public const string USP_GET_ACTIVITY_DIRECTOR_BY_HOME_ID = "Get_Activity_Director_By_Home_Id";
        public const string USP_GET_ACTIVITY_DIRECTOR_BY_HOME_ID_AND_ACTIVITY_ID = "Get_Activity_Director_By_HomeId_And_Activity_Id";
        public const string USP_GET_USER_BY_HOME = "Get_User_By_Home";


        public const string USP_ADD_RESIDENT = "Add_Resident";
        public const string USP_UPDATE_RESIDENT = "Update_Resident";
        public const string USP_REMOVE_RESIDENT = "Remove_Resident";
        public const string USP_GET_RESIDENT = "Get_Resident";
        public const string USP_GET_RESIDENT_BY_ID = "Get_Resident_By_Id";
        public const string USP_GET_ACTIVE_RESIDENT_BY_ID = "Get_Active_Resident_By_Id";
        public const string USP_GET_INACTIVE_RESIDENT_BY_ID = "Get_InActive_Resident_By_Id";
        public const string USP_GET_RESIDENT_BY_SUITID_OR_NAME = "Get_Resident_By_SuitId_Or_Name";
        public const string USP_GET_RESIDENT_SEARCH_BY_HOME_ID_KEY = "Get_Resident_Search_By_Home_Id_Key";
        public const string USP_GET_EXISTS_PRESCRIPTION_ID = "Get_Exists_prescription_Id";
        public const string USP_GET_EXISTS_GOAL_SHEET_ID = "Get_Exists_GoalSheet_Id";
        public const string USP_GET_EXISTS_FALL_RISK_ASSESS_ID = "Get_Exists_Fall_Risk_Assess_Id";
        public const string USP_GET_EXISTS_PLAN_OF_CARE_ID = "Get_Exists_Plan_Of_Care_Id";
        public const string USP_GET_EXISTS_DIETARY_ASSESS_ID = "Get_Exists_Dietary_Assessment_Id";
        public const string USP_GET_RESIDENT_NAME_BY_HOME_ID = "Get_ResidentName_By_Home_Id";
        public const string USP_GET_DS_MEDICAL_ALLAERGY_BY_RESIDENT = "Get_DS_Medial_Allergy_By_Resident_Id";
        public const string USP_GET_EXISTS_STATUS_FOR_RESIDENT_OTHERSTABS = "Get_Exists_Status_For_Resident_OtherTabs";
        public const string USP_UPDATE_IMAGEPATH_BY_RESIDENTID = "Update_ImagePath_By_ResidentId";
        public const string USP_GET_RESIDENTLIST_BY_FLOOR = "Get_ResidentList_By_Floor";
        public const string USP_GET_EMERGENCY_RESIDENT_DETAILS = "GET_Emergency_Resident_Details";
        public const string USP_GET_RESIDENT_ATTENDANCE_SEARCH_BY_HOME_ID_KEY = "Get_Resident_Attendance_Search_By_Home_Id_Key";


        public const string USP_ADD_RESIDENT_GENERAL_INFO = "Add_Resident_General_Info";
        public const string USP_UPDATE_RESIDENT_GENERAL_INFO = "Update_Resident_General_Info";
        public const string USP_UPDATE_RESIDENT_EMERGENCE_CONTACT = "Update_Resident_Emergency_Contact";
        public const string USP_UPDATE_RESIDENT_MEDICAL_INFO = "Update_Resident_Medical_Info";
        public const string USP_GET_EMERGENCY_CONTACT_DETAILS_BY_RESIDENT_ID = "Get_Emergency_Contact_Details_by_Resident_Id";
        public const string USP_UPDATE_RESIDENT_EMAIL = "Update_Resident_Email";
        public const string USP_UPDATE_RESIDENT_MEDICAL_ALLERGY = "Update_Resident_Medical_Allergy";


        public const string USP_ADD_RxPRESCRIPTION = "Add_Prescription";
        public const string USP_UPDATE_RxPRESCRIPTION = "Update_Prescription";
        public const string USP_GET_RxPRESCRIPTION_BY_ID = "Get_Prescription_By_Id";
        public const string USP_GET_PRESCRIPTION_BY_DATE = "Get_Prescription";


        public const string USP_ADD_PRESCRIPTION_AND_DETAILS = "Add_Prescription_And_Details";
        public const string USP_UPDATE_PRESCRIPTION_STATUS = "Update_Prescription_Status";
        public const string USP_GET_PRESCRIPTION_HISTORY_BY_RESIDENT = "Get_prescription_history_by_resident_id";
        public const string USP_GET_PRESCRIPTION_DETAILS_RECEIVE_BY_RESIDENT = "Get_Prescription_Details_By_Resident";
        public const string USP_GET_PRESCRIPTION_MODIFICATION_BY_RESIDENT = "Get_Prescription_history_Modification_By_Resident";
        public const string USP_GET_PRESCRIPTION_BY_PRESCRIPTION_ID = "Get_Prescription_By_Prescription_Id";
        public const string USP_GET_ALLERGYA_AND_DIET_BY_RESIDENT_ID = "Get_Allergy_And_Diet_By_Resident_Id";


        public const string USP_GET_MEDICATION_DETAIL_BY_RESIDENT_ID = "Get_Medication_Detail_By_Resident_Id";
        public const string USP_GET_MEDICATION_DETAIL_BY_RESIDENT_ID_DRUG_ID = "Get_Medication_Detail_By_Resident_Id_DrugId";


        public const string USP_GET_MARS_REPORTS = "Get_MAR_Details_Report";


        public const string USP_ADD_DEMENTIA_QUESTION = "Add_Dementia_Question";
        public const string USP_GET_DEMENTIA_QUESTION = "Get_Dementia_Question";
        public const string USP_REMOVE_DEMENTIA_QUESTION = "Remove_Dementia_Question";
        public const string USP_GET_DEMENTIA_QUESTION_BY_ID = "Get_Dementia_Question_By_Id";
        public const string USP_UPDATE_DEMENTIA_QUESTION = "Update_Dementia_Question";


        public const string USP_ADD_MEDICATION_ADMINISTRATION = "Add_Medication_Administration";


        public const string USP_ADD_DIETARY_ACCESSMENT = "Add_Dietary_Assessment";
        public const string USP_GET_DIETARY_ACCESSMENT_BY_RESIDENT_ID = "Get_Dietary_Assessment_By_Resident_Id";
        public const string USP_GET_DIETARY_ACCESSMENT_DATES_BY_RESIDENT_ID = "Get_Dietary_Assessment_Dates_By_Resident_Id";
        public const string USP_GET_DIETARY_ACCESSMENT_BY_ID = "Get_Dietary_Assessment_By_Id";
        public const string USP_GET_RESIDENT_SPECIAL_DIET = "Get_Resident_Special_Diet";
        public const string USP_GET_RESIDENT_DIET_ALLERGY = "Get_Resident_Diet_Allergy";
        public const string USP_GET_RESIDENT_MEDICAL_ALLERGY = "Get_Resident_Medical_Allergy";
        public const string USP_UPDATE_DIETARY_VIEW_STATUS = "Update_Dietary_View_Status";


        public const string USP_ADD_GOAL_SHEET = "Add_Goal_sheet";
        public const string USP_UPDATE_GOAL_SHEET = "Update_Goal_sheet";
        public const string USP_GET_GOAL_SHEET_COLLECTIONS_BY_RESIDENT = "Get_Goal_sheet_By_ResidentId";
        public const string USP_GET_RESIDENT_GOAL_DETAILS = "Get_Resident_Goal_Details";
        public const string USP_ADD_GOAL_SHEET_DETAILS = "Add_Goal_sheet_Details";
        public const string USP_ADD_GOAL = "Add_Goal";
        public const string USP_UPDATE_GOAL_SHEET_ACTION_DETAILS = "Update_Goal_sheet_Action_Details";
        public const string USP_ADD_GOAL_SHEET_FOLLOWUP = "Add_Goal_Sheet_FollowUp";
        public const string USP_ADD_GOAL_SHEET_NEEDS = "Add_Goal_Sheet_Needs";
        public const string USP_GET_RESIDENT_FAMILY_PACKAGE_GOAL_DETAILS = "Get_Resident_Family_Package_Goal_Details";
        public const string USP_GET_SPECIFIC_GOAL_BY_RESIDENTID = "Get_Specific_Goal_By_ResidentId";
        public const string USP_GET_SPECIFIC_GOAL_DETAILS = "Get_Specific_Goal_Details";

        public const string USP_ADD_FALL_RISK_ASSESSMENT = "Add_Fall_Risk_Assessment";
        public const string USP_GET_FALL_RISK_ASSESSMENT_BY_RESIDENT = "Get_FallRiskAssess_Dates_by_resident_id";
        public const string USP_GET_FALL_RISK_ASSESSMENT_BY_ID = "Get_FallRiskAssess_History_by_resident_id";
        public const string USP_GET_FALL_RISK_ASSESSMENT_FOR_CARE_PLAN_BY_RESIDENT_ID = "Get_Fall_Risk_Assessment_For_Care_Plan_By_Resident_Id";
        public const string USP_GET_FALL_RISK_ASSESSMENT_BY_HOME_PRINT = "Get_FallRisk_Assessment_By_Home_Print";

        public const string USP_ADD_PLAN_OF_CARE = "Add_Plan_Of_Care";
        public const string USP_ADD_IMMUNIZATION = "Add_Immunization";
        public const string USP_GET_PLANOFCARE_ASSESSMENTDATE = "Get_PlanOfcare_AssessmentDate_By_resident_Id";
        public const string USP_GET_PLANOFCARE = "Get_PlanOfCare_By_Id";
        public const string USP_GET_PLANOFCARE_RESIDENT_ID = "Get_PlanOfCare_By_Resident_Id";
        public const string USP_GET_NUTRITION_DIETS_BY_RESIDENT_ID = "Get_Nutrition_Diets_By_Resident_Id";
        public const string USP_GET_PLANOFCARE_RESIDENT_ID_FOR_RCA = "Get_PlanOfCare_By_Resident_Id_For_RCA";
        public const string USP_GET_CARE_PLAN_P2 = "Get_Care_Plan_P2";
        public const string USP_ADD_CARE_PLAN_P2 = "Add_Care_Plan_P2";
        public const string USP_GET_CAREPLAN_P2_ASSESSMENTDATE_BY_RESIDENT_ID = "Get_CarePlan_P2_AssessmentDate_By_resident_Id";
        public const string USP_GET_CARE_PLAN_P2_BY_RESIDENTID_AND_DATE = "Get_Care_Plan_P2_By_ResidentID_And_Date";
        public const string USP_UPDATE_PLAN_OF_CARE = "Update_Plan_Of_Care";


        public const string USP_ADD_DRUG = "Add_Drug";
        public const string USP_GET_DRUG = "Get_Drug";
        public const string USP_GET_DRUG_BY_ID = "Get_Drug_By_Id";
        public const string USP_REMOVE_DRUG = "Remove_Drug";
        public const string USP_UPDATE_DRUG = "Update_Drug";
        public const string USP_GET_DRUG_SEARCH_BY_FORM_ID_KEY = "Get_Drugs_Search_By_Form_Id_Key";
        public const string USP_ADD_DRUG_FROM_MEDICATION = "Add_Drug_From_Medication";


        public const string USP_ADD_DEMENTIA_CHECKLIST = "Add_Dementia_CheckList";
        public const string USP_ADD_DEMENTIA_CHECKLIST_DATES_BY_ID = "Get_Dementia_List_Dates_By_Id";
        public const string USP_ADD_DEMENTIA_CHECKLIST_BY_ID = "Get_Dementia_List_By_Id";


        public const string USP_ADD_ACTIVITY = "Add_Activity";
        public const string USP_UPDATE_ACTIVITY = "Update_Activity";
        public const string USP_REMOVE_ACTIVITY = "Remove_Activity";
        public const string USP_GET_ACTIVITY_BY_ID = "Get_Activity_By_Id";
        public const string USP_GET_ACTIVITY = "Get_Activity";
        public const string USP_GET_ACTIVITY_FOR_ASSESSMENT = "Get_Activity_For_Assessment";
        public const string USP_GET_ACTIVITY_NAME = "Get_Activity_Name";
        public const string USP_GET_ACTIVITY_BY_ACTIVITY_CATEGORY = "Get_Activity_By_category_Id";
        public const string USP_GET_ACTIVITY_ASSESSMENT_REPORT_BY_ID = "Get_Activity_Assessment_Report_By_ID";



        public const string USP_ADD_ACTIVITY_CATEGORY = "Add_Activity_Category";
        public const string USP_UPDATE_ACTIVITY_CATEGORY = "Update_Activity_Category";
        public const string USP_REMOVE_ACTIVITY_CATEGORY = "Remove_Activity_Category";
        public const string USP_GET_ACTIVITY_CATEGORY = "Get_Activity_Category";
        public const string USP_GET_ACTIVITY_CATEGORY_BY_ID = "Get_Activity_Category_By_Id";


        public const string USP_ADD_FORM = "Add_Form";
        public const string USP_UPDATE_FORM = "Update_Form";
        public const string USP_GET_FORM = "Get_Form_Name";
        public const string USP_GET_FORM_BY_ID = "Get_Form_By_Id";
        public const string USP_REMOVE_FORM = "Remove_Form";


        public const string USP_ADD_ACTIVITY_EVENTS = "Add_Activity_Events";
        public const string USP_ADD_ALL_ACTIVITY_EVENTS_FROM_PREVIOUS_MONTH = "Add_All_Activity_Events_From_Previous_Month";
        public const string USP_ADD_ACTIVITY_EVENTS_FROM_PREVIOUS_MONTH = "Add_Activity_Events_From_Previous_Month";
        public const string USP_UPDATE_ACTIVITY_EVENTS = "Update_Activity_Events";
        public const string USP_GET_ACTIVITY_EVENTS_BY_ID = "Get_Activity_Events_By_Id";
        public const string USP_GET_ACTIVITY_EVENTS_DETAILS_BY_ACTIVITY_EVENT_ID = "Get_Activity_Events_Details_By_Activity_Event_Id";
        public const string USP_REMOVE_ACTIVITY_EVENTS = "Remove_Activity_Events";
        public const string USP_GET_ACTIVITY_EVENTS_NOTE_SEARCH_BY_KEY = "Get_Activity_Events_Note_Search_By_Key";
        public const string USP_REMOVE_ACTIVITY_EVENTS_FOR_MONTH = "Remove_Activity_Events_For_Month";

        public const string USP_GET_ACTIVITY_CALENDAR = "Get_Activity_Calendar";
        public const string USP_GET_ACTIVITY_CALENDAR_BY_RESIDENT_ID = "Get_Activity_Calendar_By_Resident_Id";
        public const string USP_GET_BULK_ACTIVITY_EVENT_ADD_COUNT = "Get_Bulk_Activity_Event_Add_Count";
        public const string USP_ADD_ACTIVITY_CALENDAR_NAMES = "Add_Activity_Calendar_Names";
        public const string USP_GET_ACTIVITY_CALENDAR_NAMES = "Get_Activity_Calendar_Names";


        public const string USP_ADD_INITIAL_ACTIVITY_ASSESSMENT = "Add_Initial_Activity_Assessment";
        public const string USP_INITIAL_ACTIVITY_ASSESSMENT_DATES_BY_RESIDENT_ID = "Get_Initial_Activity_Assessment_Dates_By_Resident_Id";
        public const string USP_INITIAL_ACTIVITY_ASSESSMENT_BY_ASSESSMENT_ID = "Get_Initial_Activity_Assessment_By_Assessment_Id";
        public const string USP_GET_ACTIVITY_ASSESSMENT_BY_ID = "Get_Activity_Assessment_By_Id";


        public const string USP_ADD_ACTIVITY_EVENT_ATTENDANCE = "Add_Activity_Event_Attendance";
        public const string USP_UPDATE_ACTIVITY_EVENT_ATTENDANCE = "Update_Activity_Event_Attendance";
        public const string USP_GET_ACTIVITY_EVENT_ATTENDANCE_BY_ID = "Get_Activity_Event_Attendance_By_Id";
        public const string USP_GET_ACTIVITY_RESIDENT_BY_ACTIVITY_EVENT_ID = "Get_Activity_Resident_By_Activity_Event_Id";

        public const string USP_ADD_VENUE = "Add_Venue";
        public const string USP_GET_VENUE = "Get_Venue";
        public const string USP_GET_VENUE_BY_ID = "Get_Venue_By_Id";
        public const string USP_REMOVE_VENUE = "Remove_Venue";
        public const string USP_UPDATE_VENUE = "Update_Venue";


        public const string USP_ADD_PROGRESS_NOTES = "Add_Progress_Notes";
        public const string USP_GET_PROGRESS_NOTES = "Get_Progress_Notes";
        public const string USP_REMOVE_PROGRESS_NOTES = "Remove_Progress_Notes";
        public const string USP_GET_PROGRESS_NOTES_BY_ID = "Get_Progress_Notes_By_Id";
        public const string USP_UPDATE_PROGRESS_NOTES = "Update_Progress_Notes";
        public const string USP_GET_PROGRESS_NOTES_ACKNOWLEDGEMENT = "Get_Progress_Notes_Acknowledgement";
        public const string USP_ADD_PROGRESS_NOTES_ACKNOWLEDGEMENT = "Add_Progress_Notes_Acknowledgement";
        public const string USP_ADD_PROGRESS_NOTES_ACKNOWLEDGED = "Add_Progress_Notes_Acknowledged";

        public const string USP_GET_PERSONAL_CALENDAR_BY_RESIDENT_ID = "Get_Personal_Calendar_By_Resident_Id";


        public const string USP_GET_ALERT_BY_HOME_ID = "Get_Alerts_By_Home_Id";
        public const string USP_GET_ALERT_DETAILS_BY_TYPE = "Get_Alert_Details_By_Type";

        public const string USP_GET_GET_TO_DO_LIST = "Get_To_Do_List";

        public const string USP_ADD_DASHBOARD = "Add_DashBoard";
        public const string USP_GET_DASHBOARD_BY_HOME_ID_AND_DATE = "Get_DashBoard_By_HomeId_And_Date";



        public const string USP_GET_CURRENT_VERSION = "Get_Current_Version";

        public const string USP_ADD_SUITE_HANDLER = "Add_Suite_Handler";
        public const string USP_GET_SUITE_HANDLER_BY_RESIDENT_ID = "Get_Suite_Handler_By_Resident_Id";
        public const string USP_GET_SUITE_HANDLER_STATUS = "Get_Suite_Handler_Status";
        public const string USP_GET_RESIDENT_SUITE_DETAILS = "Get_Resident_Suite_Details";
        public const string USP_GET_RESIDENT_SUITE_REPORT = "Get_Resident_Suite_Report";


        public const string USP_GET_ASSESSMENT_TRACKING_REPORT_BY_HOME_ID = "Get_Assessment_Tracking_Report";
        public const string USP_GET_OCCUPANCY_REPORT = "Get_Occupancy_Report";
        public const string USP_GET_RESIDENT_FALL_REPORT = "Get_Resident_Fall_Report";
        public const string USP_GET_DIETARY_REPORT_BY_HOME = "Get_Dietary_Report_By_Home";
        public const string USP_GET_DAILY_CENSUS_REPORT = "Get_Daily_Census_Report";
        public const string USP_GET_RESIDENT_FALL_RISK_REPORT = "Get_Resident_Fall_Risk_Report";
        public const string USP_Get_ACTIVITY_ATTENDANCE_SUMMARY_REPORT_BY_HOME = "Get_Activity_Attendance_Summary_Report_By_Home";
        public const string USP_GET_DINE_ABSENTEES_SUMMARY_REPORT = "Get_Dine_Absentees_Summary_Report";
        public const string USP_GET_MOVEOUT_RESIDENT_REPORT = "Get_MoveOut_Resident_Report";
        public const string USP_GET_ACTIVITY_DINE_ATTENDANCE_DAILY_REPORT = "Get_Activity_Dine_Attendance_Daily_Report";
        public const string USP_GET_HOSPITAL_VISIT_SURVEY_REPORT = "Get_Hospital_Visit_Survey_Report";
        public const string USP_GET_FALL_RISK_SURVEY_REPORT = "Get_Fall_Risk_Survey_Report";
        public const string USP_GET_FALL_RISK_SURVEY_REPORT_V1 = "Get_Fall_Risk_Survey_Report_V1";
        public const string USP_GET_DIETARY_ASSESSMENT_BY_HOME_PRINT = "Get_Dietary_Assessment_By_Home_Print";
        public const string USP_GET_ACTIVITY_ASSESSMENT_BY_HOME_PRINT = "Get_Activity_Assessment_By_Home_Print";
        public const string USP_GET_ACTIVITY_ASSESSMENT_REPORT_BY_HOME = "Get_Activity_Assessment_Report_By_Home";

        public const string USP_ADD_DINE_TIME = "Add_Dine_Time";
        public const string USP_UPDATE_DINE_TIME = "Update_Dine_Time";
        public const string USP_GET_DINE_TIME = "Get_Dine_Time";
        public const string USP_GET_DINE_TIME_BY_ID = "Get_Dine_Time_By_Id";
        public const string USP_REMOVE_DINE_TIME = "Remove_Dine_Time";
        public const string USP_GET_DINE_TIME_BY_HOME = "Get_Dine_Time_By_Home";



        public const string USP_GET_DINE_ATTENDANCE = "Get_Dine_Attendance";
        public const string USP_GET_RESIDENT_BY_DINE_ATTENDANCE_ID = "Get_Resident_By_Dine_Attendance_Id";
        public const string USP_ADD_DINE_ATTENDANCE = "Add_Dine_Attendance";
        public const string USP_GET_DINE_ATTENDANCE_REPORT_BY_HOME_ID = "Get_Dine_Attendance_Report_By_Home_Id";
        public const string USP_GET_DINE_ATTENDANCE_ID = "Get_Dine_Attendance_Id";


        public const string USP_GET_ACTIVITY_ATTENDANCE_REPORT = "Get_Activity_Attendance_Report";
        public const string USP_GET_ACTIVITY_EVENT_ATTENDANCE_BY_HOME = "Get_Activity_Event_Attendance_By_Home";
        public const string USP_GET_ACTIVITY_EVENT_ATTENDEES = "Get_Activity_Event_Attendees";
        public const string USP_GET_ACTIVITY_ATTENDANCE_ID = "Get_Activity_Attendance_Id";


        public const string USP_GET_AVERAGE_AGE_REPORT = "Get_Average_Age_Report";


        public const string USP_ADD_FOOD = "Add_Food";
        public const string USP_ADD_FOOD_MASTER_FOR_MEAL = "Add_Food_Master_For_Meal";
        public const string USP_GET_FOOD = "Get_Food";
        public const string USP_GET_FOOD_BY_ID = "Get_Food_By_Id";
        public const string USP_REMOVE_FOOD = "Remove_Food";
        public const string USP_UPDATE_FOOD = "Update_Food";
        public const string USP_GET_FOOD_BY_DINE_TIME = "Get_Food_By_Dine_Time";


        public const string USP_GET_FOOD_CONNECTING_WORD = "Get_Food_Connecting_Word";
        public const string USP_GET_MEAL_CALENDAR_BY_WEEK = "Get_Meal_Calendar_By_Week";
        public const string USP_ADD_MEAL_CALENDAR_MENU = "Add_Meal_Calendar_Menu";
        public const string USP_GET_MEAL_MENU_BY_ID = "Get_Meal_Menu_By_Id";
        public const string USP_UPDATE_MEAL_CALENDAR_NAME_AND_NOTES = "Update_Meal_Calendar_Name_And_Notes";
        public const string USP_GET_MEAL_CALENDAR_NAME_BY_HOME = "Get_Meal_Calendar_Name_By_Home";
        public const string USP_ADD_MEAL_CALENDAR_PREVIOUS = "Add_Meal_Calendar_Previous";


        public const string USP_ADD_DIRECTION = "Add_Direction";
        public const string USP_UPDATE_DIRECTION = "Update_Direction";
        public const string USP_GET_DIRECTION = "Get_Direction";
        public const string USP_GET_DIRECTION_BY_ID = "Get_Direction_By_Id";
        public const string USP_REMOVE_DIRECTION = "Remove_Direction";
        public const string USP_GET_DIRECTION_BY_HOME = "Get_Direction_By_Home";
        public const string USP_GET_DIRECTION_SEARCH_BY_KEY = "Get_Direction_Search_By_Key";


        public const string USP_ADD_ACTIVITY_DISPLAY_NAME = "Add_Activity_Display_Name";
        public const string USP_ADD_ACTIVITY_DISPLAY_NAME_FROM_CALENDAR = "Add_Activity_Display_Name_From_Calendar";
        public const string USP_UPDATE_ACTIVITY_DISPLAY_NAME = "Update_Activity_Display_Name";
        public const string USP_REMOVE_ACTIVITY_DISPLAY_NAME = "Remove_Activity_Display_Name";
        public const string USP_GET_ACTIVITY_DISPLAY_NAME_BY_ID = "Get_Activity_Display_Name_By_Id";
        public const string USP_GET_ACTIVITY_DISPLAY_NAME = "Get_Activity_Display_Name";
        public const string USP_GET_ALERT_ACTIVITY_DISPLAY_NAME = "Get_Alert_Activity_Display_Name";
        public const string USP_APPROVE_BY_ACTIVITY_DISPLAY_NAME = "Approve_By_Activity_Display_Name";


        public const string USP_GET_RESIDENT_BIRTH_DAY_CALENDAR_BY_HOME_ID = "Get_Resident_Birth_Day_Calendar_By_Home_Id";


        public const string USP_GET_GRAPH_DETAILS = "Get_Graph_Details";
        public const string USP_GET_GRAPH_DETAILS_COUNT = "Get_Graph_Details_Count";

        public const string USP_GET_ACTIVITY_EVENT_ATTENDANCE_BY_PROVINCE = "Get_Activity_Event_Attendance_By_Province";
        public const string USP_GET_DINE_ATTENDANCE_BY_PROVINCE = "Get_Dine_Attendance_By_Province";


        public const string USP_GET_NURSE_CALENDAR_DETAIL = "Get_Nurse_Calendar_By_Home_Id";
        public const string USP_GET_NURSE_CALENDAR_SHOW_ASSESSED_BY_HOME_ID = "Get_Nurse_Calendar_Show_Assessed_By_Home_Id";
        public const string USP_GET_NURSE_DAILY_REPORT = "Get_Nurse_Daily_Report";
        public const string USP_GET_NURSE_NOTES = "Get_Nurse_Note";



        public const string USP_GET_RESIDENT_DAILY_REPORT = "Get_Resident_Daily_Report";


        public const string USP_ADD_RESIDENT_AWAY_SCHEDULE = "Add_Resident_Away_Schedule";
        public const string USP_UPDATE_RESIDENT_AWAY_SCHEDULE = "Update_Resident_Away_Schedule";
        public const string USP_GET_RESIDENT_AWAY_SCHEDULE = "Get_Resident_Away_Schedule";


        public const string USP_ADD_PASS_TIMES = "Add_Pass_Times";
        public const string USP_UPDATE_PASS_TIMES = "Update_Pass_Times";
        public const string USP_GET_PASS_TIMES = "Get_Pass_Times";
        public const string USP_REMOVE_PASS_TIMES = "Remove_Pass_Times";


        public const string USP_ADD_BATHING_ATTENDANCE = "Add_Bathing_Attendance";

        public const string USP_GET_SATISFACTION_SURVEY_QUESTIONS = "Get_Satisfaction_Survey_Questions";
        public const string USP_ADD_RESIDENT_SATISFACTION_SURVEY = "Add_Resident_Satisfaction_Survey";
        public const string USP_GET_SATISFACTION_SURVEY_REPORT_DETAILS = "Get_Satisfaction_Survey_Report_Details";
        public const string USP_GET_SATISFACTION_SURVEY_ASSESSMENTDATE_BY_RESIDENT_ID = "Get_Satisfaction_Survey_AssessmentDate_By_resident_Id";
        public const string USP_GET_SATISFACTION_SURVEY_REPORT_PRINT = "Get_Satisfaction_Survey_Report_Print";
        public const string USP_GET_SATISFACTION_SURVEY_MAILIDS = "Get_Satisfaction_Survey_MailIds";

        public const string USP_GET_DOCTOR_APPPOINMENT_CALENDAR = "Get_Doctor_Apppoinment_Calendar";
        public const string USP_ADD_DOCTOR_APPPOINMENT = "Add_Doctor_Appointment";
        public const string USP_UPDATE_DOCTOR_APPPOINMENT = "Update_Doctor_Appointment";
        public const string USP_GET_DOCTOR_APPOINTMENT_BY_ID = "Get_Doctor_Appointment_By_Id";

        public const string USP_ADD_DOCTOR_RESIDENT_COMMUNICATION = "Add_Doctor_Resident_Communication";
        public const string USP_UPADET_DOCTOR_RESIDENT_COMMUNICATION = "Update_Doctor_Resident_Communication";
        public const string USP_REMOVE_DOCTOR_RESIDENT_COMMUNICATION = "Remove_Doctor_Resident_Communication";
        public const string USP_GET_DOCTOR_RESIDENT_COMMUNICATION = "Get_Doctor_Resident_Communication";
        public const string USP_GET_DOCTOR_VIEW_PROGRESS_NOTE = "Get_Doctor_View_Progress_Note";

        public const string USP_ADD_VITAL_SIGN_LOG = "Add_Vital_Sign_Log";
        public const string USP_GET_VITAL_SIGN_LOG = "Get_Vital_Sign_Log";
        public const string USP_GET_VITAL_SIGN_LOG_BY_RESIDENT_ID = "Get_Vital_Sign_Log_By_Resident_Id";
        public const string USP_GET_VITAL_SIGN_LOG_PRINT = "Get_Vital_Sign_Log_Print";
        public const string USP_GET_VITAL_SIGN_LOG__FLOW_SHEET_PRINT = "Get_Vital_Sign_Log_Flow_Sheet_Print";

        //Sam
        public const string USP_GET_RESIDENT_GENERALINFO_EXISTS = "Get_Resident_GeneralInfo_Exists";
        public const string USP_GET_TEMP_RESIDENT_BY_ID = "Get_Temp_Resident_By_Id";
        public const string USP_GET_RESIDENT_BY_ID_WITHOUT_RESIDENT_STATUS = "Get_Resident_By_Id_Without_Resident_Status";
        public const string USP_GET_PROFILE_RECOGNITION_RESIDENT = "Get_Profile_Recognition_Resident";
        public const string USP_GET_HEAD_OFFICE_USER = "Get_HeadOffice_User";

        public const string USP_GET_Resident_Activity_Interest_Report = "Get_Resident_Activity_Interest_Report";

        //Alberta SP
        public const string USP_ADD_BOWEL_MOVEMENT_ASSESSMENT = "spAB_AddBowelMovement";
        public const string USP_GET_BOWEL_MOVEMENT_ASSESSMENT = "spAB_GetBowelMovement";

        public const string USP_ADD_FAMILY_CONFERENCE_NOTES = "spAB_FamilyConferenceNote";
        public const string USP_GET_FAMILY_CONFERENCE_NOTES = "spAB_GetFamilyConferenceNotes";

        public const string USP_ADD_ADMISSION_HEADTOTOE_ASSESSMENT = "spAB_AddHeadToToeAssessment";
        public const string USP_GET_ADMISSION_HEADTOTOE_ASSESSMENT = "spAB_Get_Admission_Head_To_Toe_Assessments_By_ResidentId";

        public const string USP_ADD_EXCERCISE_ACTIVITY_SUMMARY = "spAB_Add_Excercise_Activity_Summary";
        public const string USP_GET_EXCERCISE_ACTIVITY_SUMMARY = "spAB_Get_Excercise_Activity_Summary";

        public const string USP_ADD_EXCERCISE_ACTIVITY_DETAIL = "spAB_Add_Excercise_Activity_Detail";
        public const string USP_GET_EXCERCISE_ACTIVITY_DETAIL = "spAB_Get_Excercise_Activity_Detail";

        public const string USP_ADD_HSEP_DETAIL = "spAB_Add_HSEP_Detail";
        public const string USP_GET_HSEP_DETAIL = "spAB_Get_HSEP_Detail";
        
    }
}