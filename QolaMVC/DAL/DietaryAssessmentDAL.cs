using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Xml;
using System.Data.SqlTypes;
using QolaMVC.Models;
using System.Data.SqlClient;

namespace QolaMVC.DAL
{
    #region "DietaryAssessment"

    public class DietaryAssessmentDAL
    {
        #region "Method"

        public static int AddNewDietaryAssessment(DietaryAssessmentModel addDietAssess)
        {
            string exception = string.Empty;
            int allergiesId = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_DIETARY_ACCESSMENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                l_Cmd.Parameters.AddWithValue("@residentid", addDietAssess.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@likes", addDietAssess.Likes);
                l_Cmd.Parameters.AddWithValue("@disLikes", addDietAssess.DisLikes);
                l_Cmd.Parameters.AddWithValue("@createdby", addDietAssess.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@xmlStringDietAllergy", new SqlXml(new XmlTextReader(addDietAssess.XMLStringDietAllergy, XmlNodeType.Document, null)));
                l_Cmd.Parameters.AddWithValue("@xmlStringSplDiet", new SqlXml(new XmlTextReader(addDietAssess.XMLStringSplDiet, XmlNodeType.Document, null)));
                l_Cmd.Parameters.AddWithValue("@note", addDietAssess.Note);
                l_Cmd.Parameters.AddWithValue("@assistiveNote", addDietAssess.AssistiveNote);
                l_Cmd.Parameters.AddWithValue("@dietNote", addDietAssess.DietNote);
                l_Cmd.Parameters.AddWithValue("@dietOtherNote", addDietAssess.DietOtherNote);

                l_Cmd.Parameters.AddWithValue("@nutrition", addDietAssess.Nutrition);
                l_Cmd.Parameters.AddWithValue("@nutritional", addDietAssess.Nutritional);
                l_Cmd.Parameters.AddWithValue("@nutritionDifferent", addDietAssess.NutritionDifferent);
                l_Cmd.Parameters.AddWithValue("@appetite", addDietAssess.Appitite);
                l_Cmd.Parameters.AddWithValue("@viewFlag", addDietAssess.ViewStatus);
                l_Cmd.Parameters.AddWithValue("@noAllergy", addDietAssess.NoKnownAllergy);
                allergiesId = l_Cmd.ExecuteNonQuery();
                if (allergiesId != 0)
                {
                    allergiesId = Convert.ToInt32(l_Cmd.Parameters["@id"].Value);
                }
                return allergiesId;
            }
            catch (Exception ex)
            {
                exception = "AddNewDietaryAssessment |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static DietaryAssessmentModel GetDietaryAssessmentByResidentId(int residentId)
        {
            string exception = string.Empty;
            DietaryAssessmentModel dietaryAssess = new DietaryAssessmentModel();
            AllergiesModel allergy = new AllergiesModel();
            SpecialDietModel specialDiet = new SpecialDietModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_DIETARY_ACCESSMENT_BY_RESIDENT_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentId);
                DataSet DietaryAssessReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(DietaryAssessReceive);

                if ((DietaryAssessReceive != null) & DietaryAssessReceive.Tables.Count > 0)
                {
                    foreach (DataRow DietAssessRow in DietaryAssessReceive.Tables[0].Rows)
                    {
                        dietaryAssess = new DietaryAssessmentModel();
                        allergy = new AllergiesModel();
                        dietaryAssess.ID = Convert.ToInt32(DietAssessRow["fd_id"]);
                        dietaryAssess.strAllergy = Convert.ToString(DietAssessRow["fd_allergy"]);
                        if (DietAssessRow["fd_allergy_name"] == System.DBNull.Value)
                        {
                            allergy.Name = "";
                        }
                        else
                        {
                            allergy.Name = Convert.ToString(DietAssessRow["fd_allergy_name"]);
                        }
                        dietaryAssess.Allergy = allergy;
                        dietaryAssess.strSpecialDiet = Convert.ToString(DietAssessRow["fd_special_diat"]);
                        specialDiet = new SpecialDietModel();
                        if (DietAssessRow["fd_dietary_name"] == System.DBNull.Value)
                        {
                            specialDiet.Name = "";
                        }
                        else
                        {
                            specialDiet.Name = Convert.ToString(DietAssessRow["fd_dietary_name"]);
                        }
                        dietaryAssess.SpecialDiet = specialDiet;
                        dietaryAssess.Likes = Convert.ToString(DietAssessRow["fd_likes"]);
                        dietaryAssess.DisLikes = Convert.ToString(DietAssessRow["fd_dislikes"]);

                        dietaryAssess.Nutritional = Convert.ToString(DietAssessRow["fd_nutritional"]);
                        dietaryAssess.Nutrition = Convert.ToString(DietAssessRow["fd_nutrition"]);
                        dietaryAssess.Appitite = Convert.ToString(DietAssessRow["fd_appetite"]);

                        dietaryAssess.DietNote = Convert.ToString(DietAssessRow["fd_nutrition_diet_other"]);
                        dietaryAssess.DietOtherNote = Convert.ToString(DietAssessRow["fd_nutrition_texture"]);
                        dietaryAssess.Note = Convert.ToString(DietAssessRow["fd_nutrition_diet_note"]);
                        dietaryAssess.AssistiveNote = Convert.ToString(DietAssessRow["fd_risk_assistive_device"]);
                    }
                }
                return dietaryAssess;
            }
            catch (Exception ex)
            {
                exception = "GetResidentById |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static List<DietaryAssessmentModel> GetDietaryAssessmentListByResidentId(int DietaryAssessmentId)
        {
            string exception = string.Empty;
            List<DietaryAssessmentModel> lidietaryAssessment = new List<DietaryAssessmentModel>();
            DietaryAssessmentModel dietaryAssessment;
            UserModel objUser;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_DIETARY_ACCESSMENT_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Id", DietaryAssessmentId);
                DataSet DitAssesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(DitAssesReceive);

                if (DitAssesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= DitAssesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        dietaryAssessment = new DietaryAssessmentModel();
                        objUser = new UserModel();
                        dietaryAssessment.ID = Convert.ToInt32(DitAssesReceive.Tables[0].Rows[index]["fd_id"]);
                        dietaryAssessment.Likes = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_likes"]);
                        dietaryAssessment.DisLikes = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_dislikes"]);
                        objUser.ID = Convert.ToInt32(DitAssesReceive.Tables[0].Rows[index]["fd_user_id"]);
                        objUser.FirstName = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_first_name"]);
                        objUser.LastName = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_last_name"]);
                        objUser.UserTypeName = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_user_type"]);
                        dietaryAssessment.ModifiedOn = Convert.ToDateTime(DitAssesReceive.Tables[0].Rows[index]["fd_modified_on"]);
                        dietaryAssessment.ModifiedBy = objUser;

                        dietaryAssessment.Nutritional = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_nutritional"]);
                        dietaryAssessment.Nutrition = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_nutrition"]);
                        dietaryAssessment.Appitite = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_appetite"]);

                        dietaryAssessment.DietNote = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_nutrition_diet_other"]);
                        dietaryAssessment.DietOtherNote = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_nutrition_texture"]);
                        dietaryAssessment.Note = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_nutrition_diet_note"]);
                        dietaryAssessment.AssistiveNote = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_risk_assistive_device"]);
                        dietaryAssessment.NoKnownAllergy = Convert.ToChar(DitAssesReceive.Tables[0].Rows[index]["fd_known_allergy"]);
                        lidietaryAssessment.Add(dietaryAssessment);
                    }
                }
                return lidietaryAssessment;
            }
            catch (Exception ex)
            {
                exception = "GetSpecialDietSearch |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static List<DietaryAssessmentModel> GetSpecialDietListByResidentId(int DietaryAssessmentId)
        {
            string exception = string.Empty;
            List<DietaryAssessmentModel> liSpecialDiet = new List<DietaryAssessmentModel>();
            DietaryAssessmentModel dietaryAssessment;
            SpecialDietModel specialDiet;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_SPECIAL_DIET, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Id", DietaryAssessmentId);
                DataSet DitAssesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(DitAssesReceive);

                if (DitAssesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= DitAssesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        dietaryAssessment = new DietaryAssessmentModel();
                        specialDiet = new SpecialDietModel();
                        specialDiet.ID = Convert.ToInt32(DitAssesReceive.Tables[0].Rows[index]["fd_SpecialDiet_id"]);
                        specialDiet.Name = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["SpecialDietName"]);
                        dietaryAssessment.SplDietNote = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_note"]);
                        dietaryAssessment.SpecialDiet = specialDiet;
                        liSpecialDiet.Add(dietaryAssessment);
                    }
                }
                return liSpecialDiet;
            }
            catch (Exception ex)
            {
                exception = "GetSpecialDietListByResidentId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static List<DietaryAssessmentModel> GetDietAllergyListByResidentId(int DietaryAssessmentId)
        {
            string exception = string.Empty;
            List<DietaryAssessmentModel> liDietAllergy = new List<DietaryAssessmentModel>();
            DietaryAssessmentModel dietaryAssessment;
            AllergiesModel allergies;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_DIET_ALLERGY, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Id", DietaryAssessmentId);
                DataSet DitAssesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(DitAssesReceive);

                if (DitAssesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= DitAssesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        dietaryAssessment = new DietaryAssessmentModel();
                        allergies = new AllergiesModel();
                        allergies.ID = Convert.ToInt32(DitAssesReceive.Tables[0].Rows[index]["fd_allergy_id"]);
                        allergies.Name = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["AllergyName"]);
                        dietaryAssessment.DietAllergyNote = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_note"]);
                        dietaryAssessment.Allergy = allergies;
                        liDietAllergy.Add(dietaryAssessment);
                    }
                }
                return liDietAllergy;
            }
            catch (Exception ex)
            {
                exception = "GetDietAllergyListByResidentId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static Collection<DietaryAssessmentModel> GetDietaryAssessmentDatesByResId(int residentId)
        {
            string exception = string.Empty;
            Collection<DietaryAssessmentModel> dietaryAssessmentDates = new Collection<DietaryAssessmentModel>();
            DietaryAssessmentModel dietaryAssessment;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_DIETARY_ACCESSMENT_DATES_BY_RESIDENT_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", residentId);
                DataSet dementiaList = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dementiaList);

                if (dementiaList != null)
                {
                    if (dementiaList.Tables[0].Rows.Count > 0)
                    {
                        for (int index = 0; index <= dementiaList.Tables[0].Rows.Count - 1; index++)
                        {
                            dietaryAssessment = new DietaryAssessmentModel();
                            dietaryAssessment.ID = Convert.ToInt32(dementiaList.Tables[0].Rows[index]["fd_id"]);
                            dietaryAssessment.ModifiedOn = Convert.ToDateTime(dementiaList.Tables[0].Rows[index]["fd_modified_on"]);
                            dietaryAssessmentDates.Add(dietaryAssessment);
                        }
                    }
                }
                return dietaryAssessmentDates;
            }
            catch (Exception ex)
            {
                exception = "GetDietaryAssessmentDatesByResId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool UpdateDietaryAssessmentStatus(int iDietaryId, int iViewId, char viewFlag = 'V')
        {
            string exception = string.Empty;
            bool isUpdated = false;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_DIETARY_VIEW_STATUS, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@dietaryId", iDietaryId);
                l_Cmd.Parameters.AddWithValue("@viewFlag", viewFlag);
                l_Cmd.Parameters.AddWithValue("@viewId", iViewId);

                int allergiesId = l_Cmd.ExecuteNonQuery();
                if (allergiesId != 0)
                {
                    isUpdated = true;
                }
                return isUpdated;
            }
            catch (Exception ex)
            {
                exception = "UpdateDietaryAssessmentStatus |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        #endregion
        #region "Dietary Report"
        public static DataSet GetDietaryReportByHomeId(int iHomeId, int iAllergyId, int iSepcialAllergyId, int iFloorId, int iTypeId, string sNutritionValues)
        {
            string exception = string.Empty;
            DataSet dsDietary = new DataSet();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_DIETARY_REPORT_BY_HOME, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeID", iHomeId);
                l_Cmd.Parameters.AddWithValue("@allergyId", iAllergyId);
                l_Cmd.Parameters.AddWithValue("@specialId", iSepcialAllergyId);
                l_Cmd.Parameters.AddWithValue("@floor", iFloorId);
                l_Cmd.Parameters.AddWithValue("@typeId", iTypeId);
                if (sNutritionValues != "" && sNutritionValues != "0")
                {
                    l_Cmd.Parameters.AddWithValue("@typeValues", sNutritionValues);
                }
                dsDietary = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsDietary);
                return dsDietary;
            }
            catch (Exception ex)
            {
                exception = "GetDietaryReportByHomeId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        #endregion "Dietary Report"

        #region "Dietary Floor Wise Report"
        public static DataSet GetDietaryAssessmentFloorWise(int iHomeId, int iFloorId)
        {
            string exception = string.Empty;
            DataSet dsDietaryFloor = new DataSet();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_DIETARY_ASSESSMENT_BY_HOME_PRINT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", iHomeId);
                l_Cmd.Parameters.AddWithValue("@floorNo", iFloorId);
                dsDietaryFloor = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsDietaryFloor);
                return dsDietaryFloor;
            }
            catch (Exception ex)
            {
                exception = "GetDietaryFloorReportByHomeId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        #endregion "Dietary Floor Wise Report"

    }
    #endregion
}
