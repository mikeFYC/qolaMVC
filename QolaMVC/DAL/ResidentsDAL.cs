using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using System.Data.SqlTypes;
using QolaMVC.Models;
using static QolaMVC.Constants.EnumerationTypes;
using System.Web.Mvc;

namespace QolaMVC.DAL
{

    public class ResidentsDAL
    {

        public static int[] AddNewResidentGeneralInfo(ResidentModel addResidentGeneralInfo)
        {
            string exception = string.Empty;
            int residentId = 0;
            int[] iResultId = new int[2];

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_RESIDENT_GENERAL_INFO, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //db.AddOutParameter(cmd, "@id", addResidentGeneralInfo.ID);
                l_Cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                l_Cmd.Parameters.AddWithValue("@homeId", addResidentGeneralInfo.Home.Id);
                l_Cmd.Parameters.AddWithValue("@suiteId", addResidentGeneralInfo.SuiteIds);
                l_Cmd.Parameters.AddWithValue("@Occupancy ", addResidentGeneralInfo.Occupancy);
                l_Cmd.Parameters.AddWithValue("@firstname", addResidentGeneralInfo.FirstName);
                l_Cmd.Parameters.AddWithValue("@lastname", addResidentGeneralInfo.LastName);
                l_Cmd.Parameters.AddWithValue("@gender", addResidentGeneralInfo.Gendar);
                l_Cmd.Parameters.AddWithValue("@birthDate", addResidentGeneralInfo.BirthDate);
                l_Cmd.Parameters.AddWithValue("@MInDate", addResidentGeneralInfo.MoveInDate);
                if (Convert.ToDateTime(addResidentGeneralInfo.MoveOutDate) != Convert.ToDateTime("01/01/0001"))
                {
                    l_Cmd.Parameters.AddWithValue("@MOutDate ", addResidentGeneralInfo.MoveOutDate);
                }
                if (Convert.ToDateTime(addResidentGeneralInfo.AdmittedFrom) != Convert.ToDateTime("01/01/0001"))
                {
                    l_Cmd.Parameters.AddWithValue("@admittedFrom ", addResidentGeneralInfo.AdmittedFrom);
                }
                l_Cmd.Parameters.AddWithValue("@birthPlace", addResidentGeneralInfo.BirthPlace);
                l_Cmd.Parameters.AddWithValue("@maritalStatus ", addResidentGeneralInfo.MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@MBHealthNo", addResidentGeneralInfo.MBhealthNumber);
                l_Cmd.Parameters.AddWithValue("@significatOther", addResidentGeneralInfo.SignificatOther);
                l_Cmd.Parameters.AddWithValue("@registeredVoter", addResidentGeneralInfo.RegisteredVoter);
                l_Cmd.Parameters.AddWithValue("@religiousAffiliate", addResidentGeneralInfo.ReligiousAffiliation);
                l_Cmd.Parameters.AddWithValue("@relationshipFamily", addResidentGeneralInfo.RelationshipWithFamily);
                l_Cmd.Parameters.AddWithValue("@educationLevel ", addResidentGeneralInfo.EducationLevel);
                l_Cmd.Parameters.AddWithValue("@abilityToWrite", addResidentGeneralInfo.AbilityToWrite);
                l_Cmd.Parameters.AddWithValue("@abilityToRead", addResidentGeneralInfo.AbilityToRead);
                l_Cmd.Parameters.AddWithValue("@pastOccupationJobs", addResidentGeneralInfo.PastOccupationJobs);
                l_Cmd.Parameters.AddWithValue("@veteran", addResidentGeneralInfo.Vetaran);
                l_Cmd.Parameters.AddWithValue("@PersonalInvolve", addResidentGeneralInfo.PersonalInvolvement);
                l_Cmd.Parameters.AddWithValue("@qolaResident", Convert.ToChar(addResidentGeneralInfo.QolaResident));
                l_Cmd.Parameters.AddWithValue("@phoneNo", addResidentGeneralInfo.Phone);
                l_Cmd.Parameters.AddWithValue("@otherLanguage", addResidentGeneralInfo.OtherLanguage);
                l_Cmd.Parameters.AddWithValue("@handDominance", addResidentGeneralInfo.HandDominance);
                l_Cmd.Parameters.AddWithValue("@assessFrequency", addResidentGeneralInfo.AssFrequency);
                l_Cmd.Parameters.AddWithValue("@status", addResidentGeneralInfo.Status);
                l_Cmd.Parameters.AddWithValue("@createdby", addResidentGeneralInfo.ModifiedBy.ID);
                l_Cmd.Parameters.Add("@suiteHandlerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                if (addResidentGeneralInfo.ShortName != "")
                {
                    l_Cmd.Parameters.AddWithValue("@shortName", Convert.ToString(addResidentGeneralInfo.ShortName));
                }
                if (addResidentGeneralInfo.DNRStatus.ToString() != "\0")
                {
                    l_Cmd.Parameters.AddWithValue("@DNRStatus", addResidentGeneralInfo.DNRStatus);
                }
                if (addResidentGeneralInfo.FullCodeStatus.ToString() != "\0")
                {
                    l_Cmd.Parameters.AddWithValue("@FullCodeStatus", addResidentGeneralInfo.FullCodeStatus);
                }
                DateTime aniversary = addResidentGeneralInfo.AnniversaryDate;
                if (aniversary.Day == 1 && aniversary.Month == 1 && aniversary.Year == 1)
                {
                    l_Cmd.Parameters.AddWithValue("@aniversaryDate", DBNull.Value);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@aniversaryDate", addResidentGeneralInfo.AnniversaryDate);
                }
                l_Cmd.Parameters.AddWithValue("@religiousType", addResidentGeneralInfo.ReligionType);
                l_Cmd.Parameters.AddWithValue("@voterType", addResidentGeneralInfo.VoterType);
                l_Cmd.Parameters.AddWithValue("@readType", addResidentGeneralInfo.ReadType);
                l_Cmd.Parameters.AddWithValue("@writeType", addResidentGeneralInfo.WriteType);

                l_Cmd.Parameters.AddWithValue("@veteranType", addResidentGeneralInfo.VeteranType);
                l_Cmd.Parameters.AddWithValue("@eduType", addResidentGeneralInfo.EducationType);
                l_Cmd.Parameters.AddWithValue("@culturalPreferences", addResidentGeneralInfo.CulturalPreferences);
                residentId = l_Cmd.ExecuteNonQuery();
                if (residentId > 0)
                {
                    iResultId[0] = Convert.ToInt32(l_Cmd.Parameters["@id"].Value);
                    iResultId[1] = Convert.ToInt32(l_Cmd.Parameters["@suiteHandlerId"].Value);

                    addResidentGeneralInfo.ID = Convert.ToInt32(l_Cmd.Parameters["@id"].Value);
                    addResidentGeneralInfo.SuiteHandler = Convert.ToInt32(l_Cmd.Parameters["@suiteHandlerId"].Value);
                }
                return iResultId;
            }
            catch (Exception ex)
            {
                exception = "AddNewResidentGeneralInfo |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void update_checklist(int userid,int residentid)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into [tbl_AB_Admission_checklist] values(@resident,1,@user,@date)";
            cmd.Parameters.AddWithValue("@resident", residentid);
            cmd.Parameters.AddWithValue("@user",userid);
            cmd.Parameters.AddWithValue("@date",DateTime.Now);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            conn.Close();

        }


        public static bool UpdateResidentGeneralInfo(ResidentModel updateResidentGeneralInfo)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_RESIDENT_GENERAL_INFO, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", updateResidentGeneralInfo.ID);
                l_Cmd.Parameters.AddWithValue("@homeId", updateResidentGeneralInfo.Home.Id);
                l_Cmd.Parameters.AddWithValue("@suiteId", updateResidentGeneralInfo.SuiteIds);
                l_Cmd.Parameters.AddWithValue("@Occupancy ", updateResidentGeneralInfo.Occupancy);
                l_Cmd.Parameters.AddWithValue("@firstname", updateResidentGeneralInfo.FirstName);
                l_Cmd.Parameters.AddWithValue("@lastname", updateResidentGeneralInfo.LastName);
                l_Cmd.Parameters.AddWithValue("@gender", updateResidentGeneralInfo.Gendar);
                l_Cmd.Parameters.AddWithValue("@birthDate", updateResidentGeneralInfo.BirthDate);
                l_Cmd.Parameters.AddWithValue("@MInDate", updateResidentGeneralInfo.MoveInDate);

                if (Convert.ToDateTime(updateResidentGeneralInfo.AdmittedFrom) != Convert.ToDateTime("01/01/0001"))
                {
                    l_Cmd.Parameters.AddWithValue("@admittedFrom ", updateResidentGeneralInfo.AdmittedFrom);
                }
                l_Cmd.Parameters.AddWithValue("@birthPlace", updateResidentGeneralInfo.BirthPlace);
                l_Cmd.Parameters.AddWithValue("@maritalStatus ", updateResidentGeneralInfo.MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@MBHealthNo", updateResidentGeneralInfo.MBhealthNumber);
                l_Cmd.Parameters.AddWithValue("@significatOther", updateResidentGeneralInfo.SignificatOther);
                l_Cmd.Parameters.AddWithValue("@registeredVoter", updateResidentGeneralInfo.RegisteredVoter);
                l_Cmd.Parameters.AddWithValue("@religiousAffiliate", updateResidentGeneralInfo.ReligiousAffiliation);
                l_Cmd.Parameters.AddWithValue("@relationshipFamily", updateResidentGeneralInfo.RelationshipWithFamily);
                l_Cmd.Parameters.AddWithValue("@educationLevel ", updateResidentGeneralInfo.EducationLevel);
                l_Cmd.Parameters.AddWithValue("@abilityToWrite", updateResidentGeneralInfo.AbilityToWrite);
                l_Cmd.Parameters.AddWithValue("@abilityToRead", updateResidentGeneralInfo.AbilityToRead);
                l_Cmd.Parameters.AddWithValue("@pastOccupationJobs", updateResidentGeneralInfo.PastOccupationJobs);
                l_Cmd.Parameters.AddWithValue("@veteran", updateResidentGeneralInfo.Vetaran);
                l_Cmd.Parameters.AddWithValue("@PersonalInvolve", updateResidentGeneralInfo.PersonalInvolvement);
                l_Cmd.Parameters.AddWithValue("@phoneNo", updateResidentGeneralInfo.Phone);
                l_Cmd.Parameters.AddWithValue("@otherLanguage", updateResidentGeneralInfo.OtherLanguage);
                l_Cmd.Parameters.AddWithValue("@handDominance", updateResidentGeneralInfo.HandDominance);
                l_Cmd.Parameters.AddWithValue("@modifiedby", updateResidentGeneralInfo.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@qolaResident", Convert.ToChar(updateResidentGeneralInfo.QolaResident));
                l_Cmd.Parameters.AddWithValue("@suiteHandlerId", updateResidentGeneralInfo.SuiteHandler);
                l_Cmd.Parameters.AddWithValue("@DNRStatus", updateResidentGeneralInfo.DNRStatus);
                l_Cmd.Parameters.AddWithValue("@FullCodeStatus", updateResidentGeneralInfo.FullCodeStatus);
                DateTime aniversary = updateResidentGeneralInfo.AnniversaryDate;
                if (aniversary.Day != 1 && aniversary.Month != 1 && aniversary.Year != 1)
                {
                    l_Cmd.Parameters.AddWithValue("@aniversaryDate", updateResidentGeneralInfo.AnniversaryDate);
                }
                l_Cmd.Parameters.AddWithValue("@religiousType", updateResidentGeneralInfo.ReligionType);
                l_Cmd.Parameters.AddWithValue("@voterType", updateResidentGeneralInfo.VoterType);
                l_Cmd.Parameters.AddWithValue("@readType", updateResidentGeneralInfo.ReadType);
                l_Cmd.Parameters.AddWithValue("@writeType", updateResidentGeneralInfo.WriteType);
                l_Cmd.Parameters.AddWithValue("@veteranType", updateResidentGeneralInfo.VeteranType);
                l_Cmd.Parameters.AddWithValue("@eduType", updateResidentGeneralInfo.EducationType);
                l_Cmd.Parameters.AddWithValue("@culturalPreferences", updateResidentGeneralInfo.CulturalPreferences);
                l_Cmd.Parameters.AddWithValue("@fd_Favourite_song", updateResidentGeneralInfo.Favourite_song);
                l_Cmd.Parameters.AddWithValue("@fd_Favourite_movie", updateResidentGeneralInfo.Favourite_movie);
                l_Cmd.Parameters.AddWithValue("@fd_Number_of_children", updateResidentGeneralInfo.Number_of_children);
                l_Cmd.Parameters.AddWithValue("@fd_Number_of_grandchildren", updateResidentGeneralInfo.Number_of_grandchildren);
                l_Cmd.Parameters.AddWithValue("@fd_Favourite_dessert", updateResidentGeneralInfo.Favourite_dessert);
                l_Cmd.Parameters.AddWithValue("@fd_Favourite_drink", updateResidentGeneralInfo.Favourite_drink);
                l_Cmd.Parameters.AddWithValue("@fd_Favourite_flower", updateResidentGeneralInfo.Favourite_flower);
                l_Cmd.Parameters.AddWithValue("@fd_Favourite_pets", updateResidentGeneralInfo.Favourite_pets);
                l_Cmd.Parameters.AddWithValue("@fd_Wakeup_time", updateResidentGeneralInfo.Wakeup_time);
                l_Cmd.Parameters.AddWithValue("@fd_Go_to_bed_at", updateResidentGeneralInfo.Go_to_bed_at);
                l_Cmd.Parameters.AddWithValue("@fd_Favourite_past_time", updateResidentGeneralInfo.Favourite_past_time);


                if (updateResidentGeneralInfo.ShortName != "")
                {
                    l_Cmd.Parameters.AddWithValue("@shortName", updateResidentGeneralInfo.ShortName);
                }
                affected = l_Cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentGeneralInfo |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool UpdateResidentEmergencyContacts(ResidentModel updateResidentEmergencyContacts)
        {
            string exception = string.Empty;
            bool result = false;
            int residentId = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_RESIDENT_EMERGENCE_CONTACT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", updateResidentEmergencyContacts.ID);
                l_Cmd.Parameters.AddWithValue("@contract1", updateResidentEmergencyContacts.Contract1);
                l_Cmd.Parameters.AddWithValue("@address1", updateResidentEmergencyContacts.Address1);
                l_Cmd.Parameters.AddWithValue("@relationship1", updateResidentEmergencyContacts.Relationship1);
                l_Cmd.Parameters.AddWithValue("@homePhone1", updateResidentEmergencyContacts.HomePhone1);
                l_Cmd.Parameters.AddWithValue("@businessPhone1", updateResidentEmergencyContacts.BusinessPhone1);
                l_Cmd.Parameters.AddWithValue("@cellPhone1", updateResidentEmergencyContacts.CellPhone1);
                l_Cmd.Parameters.AddWithValue("@email1", updateResidentEmergencyContacts.Email1);
                l_Cmd.Parameters.AddWithValue("@contract2", updateResidentEmergencyContacts.Contract2);
                l_Cmd.Parameters.AddWithValue("@address2", updateResidentEmergencyContacts.Address2);
                l_Cmd.Parameters.AddWithValue("@relationship2", updateResidentEmergencyContacts.Relationship2);
                l_Cmd.Parameters.AddWithValue("@homePhone2", updateResidentEmergencyContacts.HomePhone2);
                l_Cmd.Parameters.AddWithValue("@businessPhone2", updateResidentEmergencyContacts.BusinessPhone2);
                l_Cmd.Parameters.AddWithValue("@cellPhone2", updateResidentEmergencyContacts.CellPhone2);
                l_Cmd.Parameters.AddWithValue("@email2", updateResidentEmergencyContacts.Email2);
                l_Cmd.Parameters.AddWithValue("@contract3", updateResidentEmergencyContacts.Contract3);
                l_Cmd.Parameters.AddWithValue("@address3", updateResidentEmergencyContacts.Address3);
                l_Cmd.Parameters.AddWithValue("@relationship3", updateResidentEmergencyContacts.Relationship3);
                l_Cmd.Parameters.AddWithValue("@homePhone3", updateResidentEmergencyContacts.HomePhone3);
                l_Cmd.Parameters.AddWithValue("@businessPhone3", updateResidentEmergencyContacts.BusinessPhone3);
                l_Cmd.Parameters.AddWithValue("@cellPhone3", updateResidentEmergencyContacts.CellPhone3);
                l_Cmd.Parameters.AddWithValue("@email3", updateResidentEmergencyContacts.Email3);
                l_Cmd.Parameters.AddWithValue("@insuranceCompany", updateResidentEmergencyContacts.InsuranceCompany);
                l_Cmd.Parameters.AddWithValue("@contractNo ", updateResidentEmergencyContacts.ContractNumber);
                l_Cmd.Parameters.AddWithValue("@groupNo", updateResidentEmergencyContacts.GroupNumber);
                l_Cmd.Parameters.AddWithValue("@poaCare", updateResidentEmergencyContacts.POACare);
                l_Cmd.Parameters.AddWithValue("@poaCareType", updateResidentEmergencyContacts.POACareType);
                l_Cmd.Parameters.AddWithValue("@poaCareTypeStatus", updateResidentEmergencyContacts.POACareStatus);
                l_Cmd.Parameters.AddWithValue("@careHomePhone", updateResidentEmergencyContacts.CareHomePhone);
                l_Cmd.Parameters.AddWithValue("@careWorkPhone", updateResidentEmergencyContacts.CareWorkPhone);
                l_Cmd.Parameters.AddWithValue("@poaCareCellNo", updateResidentEmergencyContacts.CareCellPhone);
                l_Cmd.Parameters.AddWithValue("@poaCareHomeType", updateResidentEmergencyContacts.POACareHomePhoneType);
                l_Cmd.Parameters.AddWithValue("@poaCareBusinessType", updateResidentEmergencyContacts.POACareBusinessPhoneType);
                l_Cmd.Parameters.AddWithValue("@poaCareCellType", updateResidentEmergencyContacts.POACareCellPhoneType);
                l_Cmd.Parameters.AddWithValue("@careEmail", updateResidentEmergencyContacts.CareEmail);
                l_Cmd.Parameters.AddWithValue("@careAddress", updateResidentEmergencyContacts.CareAddress);
                l_Cmd.Parameters.AddWithValue("@careRelationship", updateResidentEmergencyContacts.CareRelationship);
                l_Cmd.Parameters.AddWithValue("@careType", updateResidentEmergencyContacts.CareType);
                l_Cmd.Parameters.AddWithValue("@poaFinance", updateResidentEmergencyContacts.POAFinance);
                l_Cmd.Parameters.AddWithValue("@poaFinanceType", updateResidentEmergencyContacts.POAFinanceType);
                l_Cmd.Parameters.AddWithValue("@poaFinanceTypeStatus", updateResidentEmergencyContacts.POAFinanceStatus);
                l_Cmd.Parameters.AddWithValue("@financeHomePhone", updateResidentEmergencyContacts.FinanceHomePhone);
                l_Cmd.Parameters.AddWithValue("@financeWorkPhone", updateResidentEmergencyContacts.FinanceWorkPhone);
                l_Cmd.Parameters.AddWithValue("@poaFinanceCellNo", updateResidentEmergencyContacts.FinanceCellPhone);
                l_Cmd.Parameters.AddWithValue("@poaFinanceHomeType", updateResidentEmergencyContacts.POAFinanceHomePhoneType);
                l_Cmd.Parameters.AddWithValue("@poaFinanceBusinessType", updateResidentEmergencyContacts.POAFinanceBusinessPhoneType);
                l_Cmd.Parameters.AddWithValue("@poaFinanceCellType", updateResidentEmergencyContacts.POAFinanceCellPhoneType);
                l_Cmd.Parameters.AddWithValue("@financeEmail", updateResidentEmergencyContacts.FinanceEmail);
                l_Cmd.Parameters.AddWithValue("@financeAddress", updateResidentEmergencyContacts.FinanceAddress);
                l_Cmd.Parameters.AddWithValue("@financeRelationship", updateResidentEmergencyContacts.FinanceRelationship);
                l_Cmd.Parameters.AddWithValue("@modifiedby", updateResidentEmergencyContacts.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@callHospital", updateResidentEmergencyContacts.CallHospital);
                l_Cmd.Parameters.AddWithValue("@homePhoneType1", updateResidentEmergencyContacts.HomePhoneType1);
                l_Cmd.Parameters.AddWithValue("@businessPhoneType1", updateResidentEmergencyContacts.BusinessPhoneType1);
                l_Cmd.Parameters.AddWithValue("@cellPhoneType1", updateResidentEmergencyContacts.CellPhoneType1);
                l_Cmd.Parameters.AddWithValue("@homePhoneType2", updateResidentEmergencyContacts.HomePhoneType2);
                l_Cmd.Parameters.AddWithValue("@businessPhoneType2", updateResidentEmergencyContacts.BusinessPhoneType2);
                l_Cmd.Parameters.AddWithValue("@cellPhoneType2", updateResidentEmergencyContacts.CellPhoneType2);
                l_Cmd.Parameters.AddWithValue("@homePhoneType3", updateResidentEmergencyContacts.HomePhoneType3);
                l_Cmd.Parameters.AddWithValue("@businessPhoneType3", updateResidentEmergencyContacts.BusinessPhoneType3);
                l_Cmd.Parameters.AddWithValue("@cellPhoneType3", updateResidentEmergencyContacts.CellPhoneType3);

                l_Cmd.Parameters.AddWithValue("@poaCareType2Status", updateResidentEmergencyContacts.POACareType2Status);
                l_Cmd.Parameters.AddWithValue("@poaCareType3Status", updateResidentEmergencyContacts.POACareType3Status);
                l_Cmd.Parameters.AddWithValue("@poaFinance2TypeStatus", updateResidentEmergencyContacts.POAFinanceType2Status);
                l_Cmd.Parameters.AddWithValue("@poaFinance3TypeStatus", updateResidentEmergencyContacts.POAFinanceType3Status);
                residentId = l_Cmd.ExecuteNonQuery();
                if (residentId > 0)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentEmergencyContacts|" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool UpdateResidentMedicalInfo(ResidentModel updateResidentMedicalInfo)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_RESIDENT_MEDICAL_INFO, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", updateResidentMedicalInfo.ID);
                l_Cmd.Parameters.AddWithValue("@physician", updateResidentMedicalInfo.Physician);
                l_Cmd.Parameters.AddWithValue("@physicianPhone", updateResidentMedicalInfo.PhysicianPhone);
                l_Cmd.Parameters.AddWithValue("@allergies", updateResidentMedicalInfo.Alergies);
                l_Cmd.Parameters.AddWithValue("@healthHistory", updateResidentMedicalInfo.HealthHistory);
                l_Cmd.Parameters.AddWithValue("@AssFreq", updateResidentMedicalInfo.AssFrequency);
                l_Cmd.Parameters.AddWithValue("@qolaResident", Convert.ToChar(updateResidentMedicalInfo.QolaResident));
                l_Cmd.Parameters.AddWithValue("@funeralArguments", updateResidentMedicalInfo.FuneralArguments);
                l_Cmd.Parameters.AddWithValue("@pharmaceSelf", updateResidentMedicalInfo.PharmaceSelf);
                l_Cmd.Parameters.AddWithValue("@pharmaceNursing", updateResidentMedicalInfo.PharmaceNursing);
                l_Cmd.Parameters.AddWithValue("@pharmaceFaxNumber", updateResidentMedicalInfo.PharmaceFaxNumber);
                l_Cmd.Parameters.AddWithValue("@pharmacePhoneNo", updateResidentMedicalInfo.PharmacePhoneNo);
                l_Cmd.Parameters.AddWithValue("@religionContact", updateResidentMedicalInfo.ReligionContact);
                l_Cmd.Parameters.AddWithValue("@religionHomePhone", updateResidentMedicalInfo.ReligionHomePhone);
                l_Cmd.Parameters.AddWithValue("@religionOffice", updateResidentMedicalInfo.ReligionOffice);
                l_Cmd.Parameters.AddWithValue("@deleteRowIds", updateResidentMedicalInfo.DeleteRowAllergyId);
                l_Cmd.Parameters.AddWithValue("@xmlInsertString", new SqlXml(new XmlTextReader(updateResidentMedicalInfo.XMLMedicalAllergyInsert, XmlNodeType.Document, null)));
                l_Cmd.Parameters.AddWithValue("@xmlUpdateString", new SqlXml(new XmlTextReader(updateResidentMedicalInfo.XMLMedicalAllergyUpdate, XmlNodeType.Document, null)));
                l_Cmd.Parameters.AddWithValue("@modifiedby", updateResidentMedicalInfo.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@physicanFax", updateResidentMedicalInfo.PhysicianFaxNo);
                l_Cmd.Parameters.AddWithValue("@currentDiagnose", updateResidentMedicalInfo.CurrentDiagnoses);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentMedicalInfo |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool UpdateResidentMedicalInfo_mike(ResidentModel updateResidentMedicalInfo)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_RESIDENT_MEDICAL_INFO, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", updateResidentMedicalInfo.ID);
                l_Cmd.Parameters.AddWithValue("@physician", updateResidentMedicalInfo.Physician);
                l_Cmd.Parameters.AddWithValue("@physicianPhone", updateResidentMedicalInfo.PhysicianPhone);
                l_Cmd.Parameters.AddWithValue("@allergies", updateResidentMedicalInfo.Alergies);
                l_Cmd.Parameters.AddWithValue("@healthHistory", updateResidentMedicalInfo.HealthHistory);
                l_Cmd.Parameters.AddWithValue("@AssFreq", updateResidentMedicalInfo.AssFrequency);
                l_Cmd.Parameters.AddWithValue("@qolaResident", Convert.ToChar(updateResidentMedicalInfo.QolaResident));
                l_Cmd.Parameters.AddWithValue("@funeralArguments", updateResidentMedicalInfo.FuneralArguments);
                l_Cmd.Parameters.AddWithValue("@pharmaceSelf", updateResidentMedicalInfo.PharmaceSelf);
                l_Cmd.Parameters.AddWithValue("@pharmaceNursing", updateResidentMedicalInfo.PharmaceNursing);
                l_Cmd.Parameters.AddWithValue("@pharmaceFaxNumber", updateResidentMedicalInfo.PharmaceFaxNumber);
                l_Cmd.Parameters.AddWithValue("@pharmacePhoneNo", updateResidentMedicalInfo.PharmacePhoneNo);
                l_Cmd.Parameters.AddWithValue("@religionContact", updateResidentMedicalInfo.ReligionContact);
                l_Cmd.Parameters.AddWithValue("@religionHomePhone", updateResidentMedicalInfo.ReligionHomePhone);
                l_Cmd.Parameters.AddWithValue("@religionOffice", updateResidentMedicalInfo.ReligionOffice);
                l_Cmd.Parameters.AddWithValue("@deleteRowIds", updateResidentMedicalInfo.DeleteRowAllergyId);
                l_Cmd.Parameters.AddWithValue("@xmlInsertString", DBNull.Value);
                l_Cmd.Parameters.AddWithValue("@xmlUpdateString", DBNull.Value);
                l_Cmd.Parameters.AddWithValue("@modifiedby", updateResidentMedicalInfo.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@physicanFax", updateResidentMedicalInfo.PhysicianFaxNo);
                l_Cmd.Parameters.AddWithValue("@currentDiagnose", updateResidentMedicalInfo.CurrentDiagnoses);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentMedicalInfo |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool RemoveResident(int residentId)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_REMOVE_RESIDENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", residentId);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "RemoveResident |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ResidentModel> GetResidentCollections(int homeId)
        {
            string exception = string.Empty;
            Collection<ResidentModel> residents = new Collection<ResidentModel>();
            ResidentModel resident;
            HomeModel home;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);

                if (residentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= residentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        resident = new ResidentModel();
                        resident.ID = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_id"]);
                        resident.SuiteNo = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_suite_no"]);
                        resident.FirstName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_first_name"]);
                        resident.LastName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_last_name"]);
                        resident.Contract1 = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_contact_1"]);
                        resident.CellPhone1 = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_cell_phone_1"]);
                        resident.Physician = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_physician"]);
                        resident.PhysicianPhone = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_physician_phone"]);
                        home = new HomeModel();
                        home.Id = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_home_id"]);
                        resident.Home = home;
                        residents.Add(resident);
                    }
                }
                return residents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentCollections |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static DataTable GetResidentData(int homeId)
        {
            string exception = string.Empty;
            DataTable dtResidents = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);

                if (residentsReceive.Tables[0].Rows.Count > 0)
                {
                    dtResidents = new DataTable();
                    dtResidents = residentsReceive.Tables[0];
                }
                return dtResidents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentData |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static ResidentModel GetResidentById(int residentId, int IsPrint = 1)
        {
            string exception = string.Empty;
            ResidentModel resident = new ResidentModel();
            UserModel user;
            HomeModel home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", residentId);
                l_Cmd.Parameters.AddWithValue("@IsPrint", IsPrint);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables.Count > 0)
                {
                    foreach (DataRow residentTypeRow in residentReceive.Tables[0].Rows)
                    {
                        resident = new ResidentModel();
                        home = new HomeModel();
                        resident.ID = Convert.ToInt32(residentTypeRow["fd_id"]);
                        home.Id = Convert.ToInt32(residentTypeRow["fd_home_id"]);
                        home.Name = residentTypeRow["fd_home_name"].ToString();
                        home.Code = residentTypeRow["fd_code"].ToString();
                        resident.Home = home;
                        resident.SuiteIds = Convert.ToString(residentTypeRow["fd_suite_id"]);
                        resident.SuiteNo = Convert.ToString(residentTypeRow["fd_suite_no"]);
                        resident.MoveInDate = Convert.ToDateTime(residentTypeRow["fd_move_in_date"]);
                        if (residentTypeRow["fd_move_out_date"] == System.DBNull.Value)
                        {
                            resident.MoveOutDate = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.MoveOutDate = Convert.ToDateTime(residentTypeRow["fd_move_out_date"]);
                        }
                        resident.Occupancy = Convert.ToInt32(residentTypeRow["fd_occupancy"]);
                        resident.Age = Convert.ToInt32(residentTypeRow["fd_age"]);
                        resident.FirstName = Convert.ToString(residentTypeRow["fd_first_name"]);
                        resident.LastName = Convert.ToString(residentTypeRow["fd_last_name"]);
                        resident.Gendar = Convert.ToChar(residentTypeRow["fd_gender"]);
                        resident.ResidentImage = Convert.ToString(residentTypeRow["fd_image"]);
                        resident.Phone = Convert.ToString(residentTypeRow["fd_phone"]);
                        resident.BirthDate = Convert.ToDateTime(residentTypeRow["fd_birth_date"]);
                        resident.MBhealthNumber = Convert.ToString(residentTypeRow["fd_MB_health_number"]);

                        if (residentTypeRow["fd_aniversary_date"] == System.DBNull.Value)
                        {
                            resident.AnniversaryDate = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.AnniversaryDate = Convert.ToDateTime(residentTypeRow["fd_aniversary_date"]);
                        }

                        resident.BirthPlace = Convert.ToString(residentTypeRow["fd_birth_place"]);
                        resident.MaritalStatus = Convert.ToInt32(residentTypeRow["fd_marital_status"]);
                        resident.SignificatOther = Convert.ToString(residentTypeRow["fd_significat_other"]);
                        resident.RelationshipWithFamily = Convert.ToString(residentTypeRow["fd_relationship_family"]);
                        resident.RegisteredVoter = Convert.ToString(residentTypeRow["fd_registered_voter"]);
                        resident.Vetaran = Convert.ToString(residentTypeRow["fd_veteran"]);
                        resident.ReligiousAffiliation = Convert.ToString(residentTypeRow["fd_religious_affiliation"]);
                        resident.PersonalInvolvement = Convert.ToString(residentTypeRow["fd_personal_involvement"]);
                        resident.EducationLevel = Convert.ToString(residentTypeRow["fd_education_level"]);
                        resident.AbilityToRead = Convert.ToString(residentTypeRow["fd_ability_to_read"]);
                        resident.AbilityToWrite = Convert.ToString(residentTypeRow["fd_ability_to_write"]);
                        resident.OtherLanguage = Convert.ToString(residentTypeRow["fd_other_language"]);
                        resident.PastOccupationJobs = Convert.ToString(residentTypeRow["fd_past_occupations_jobs"]);
                        if (residentTypeRow["fd_hand_dominance"].ToString() != null && residentTypeRow["fd_hand_dominance"].ToString() != string.Empty)
                        {
                            resident.HandDominance = Convert.ToInt32(residentTypeRow["fd_hand_dominance"]);
                        }
                        else
                        {
                            resident.MaritalStatus = 0;
                        }
                        resident.ShortName = Convert.ToString(residentTypeRow["fd_short_name"] != System.DBNull.Value ? residentTypeRow["fd_short_name"] : "");
                        resident.InsuranceCompany = Convert.ToString(residentTypeRow["fd_insurance_company"]);
                        resident.ContractNumber = Convert.ToString(residentTypeRow["fd_contract_number"]);
                        resident.GroupNumber = Convert.ToString(residentTypeRow["fd_group_number"]);

                        resident.POACare = Convert.ToString(residentTypeRow["fd_POA_care"]);
                        resident.POACareType = Convert.ToInt16(residentTypeRow["fd_POA_care_type"]);

                        resident.POACareStatus = Convert.ToChar(residentTypeRow["fd_POA_care_type_status"]);
                        resident.POACareType2Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_2_status"]);
                        resident.POACareType3Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_3_status"]);
                        resident.CareHomePhone = Convert.ToString(residentTypeRow["fd_care_home_phone_no"]);
                        resident.CareWorkPhone = Convert.ToString(residentTypeRow["fd_care_work_phone_no"]);
                        resident.CareCellPhone = Convert.ToString(residentTypeRow["fd_POA_care_cell_no"]);
                        resident.CareEmail = Convert.ToString(residentTypeRow["fd_care_email"]);
                        resident.POACareHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_home_type"]);
                        resident.POACareBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_business_type"]);
                        resident.POACareCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_cell_type"]);
                        resident.POAFinance = Convert.ToString(residentTypeRow["fd_POA_finance"]);
                        resident.POAFinanceType = Convert.ToInt16(residentTypeRow["fd_POA_finance_type"]);
                        resident.POAFinanceStatus = Convert.ToChar(residentTypeRow["fd_POA_finance_type_status"]);

                        resident.POAFinanceType2Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_2_status"]);
                        resident.POAFinanceType3Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_3_status"]);

                        resident.FinanceHomePhone = Convert.ToString(residentTypeRow["fd_finance_home_phone_no"]);
                        resident.FinanceWorkPhone = Convert.ToString(residentTypeRow["fd_finance_work_phone_no"]);
                        resident.FinanceCellPhone = Convert.ToString(residentTypeRow["fd_POA_finance_cell_no"]);
                        resident.POAFinanceHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_home_type"]);
                        resident.POAFinanceBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_business_type"]);
                        resident.POAFinanceCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_cell_type"]);
                        resident.FinanceEmail = Convert.ToString(residentTypeRow["fd_finance_email"]);

                        resident.Contract1 = Convert.ToString(residentTypeRow["fd_contact_1"]);
                        resident.Address1 = Convert.ToString(residentTypeRow["fd_address_1"]);
                        resident.Relationship1 = Convert.ToString(residentTypeRow["fd_relationship_1"]);
                        resident.HomePhone1 = Convert.ToString(residentTypeRow["fd_home_phone_1"]);
                        resident.BusinessPhone1 = Convert.ToString(residentTypeRow["fd_business_phone_1"]);
                        resident.CellPhone1 = Convert.ToString(residentTypeRow["fd_cell_phone_1"]);
                        resident.HomePhoneType1 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_1"]);
                        resident.BusinessPhoneType1 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_1"]);
                        resident.CellPhoneType1 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_1"]);
                        resident.Email1 = Convert.ToString(residentTypeRow["fd_email_1"]);

                        resident.Contract2 = Convert.ToString(residentTypeRow["fd_contact_2"]);
                        resident.Address2 = Convert.ToString(residentTypeRow["fd_address_2"]);
                        resident.Relationship2 = Convert.ToString(residentTypeRow["fd_relationship_2"]);
                        resident.HomePhone2 = Convert.ToString(residentTypeRow["fd_home_phone_2"]);
                        resident.BusinessPhone2 = Convert.ToString(residentTypeRow["fd_business_phone_2"]);
                        resident.CellPhone2 = Convert.ToString(residentTypeRow["fd_cell_phone_2"]);
                        resident.HomePhoneType2 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_2"]);
                        resident.BusinessPhoneType2 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_2"]);
                        resident.CellPhoneType2 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_2"]);
                        resident.Email2 = Convert.ToString(residentTypeRow["fd_email_2"]);

                        resident.Contract3 = Convert.ToString(residentTypeRow["fd_contact_3"]);
                        resident.Address3 = Convert.ToString(residentTypeRow["fd_address_3"]);
                        resident.Relationship3 = Convert.ToString(residentTypeRow["fd_relationship_3"]);
                        resident.HomePhone3 = Convert.ToString(residentTypeRow["fd_home_phone_3"]);
                        resident.BusinessPhone3 = Convert.ToString(residentTypeRow["fd_business_phone_3"]);
                        resident.CellPhone3 = Convert.ToString(residentTypeRow["fd_cell_phone_3"]);
                        resident.HomePhoneType3 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_3"]);
                        resident.BusinessPhoneType3 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_3"]);
                        resident.CellPhoneType3 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_3"]);
                        resident.Email3 = Convert.ToString(residentTypeRow["fd_email_3"]);

                        resident.Physician = Convert.ToString(residentTypeRow["fd_physician"]);
                        resident.PhysicianPhone = Convert.ToString(residentTypeRow["fd_physician_phone"]);
                        resident.PhysicianFaxNo = Convert.ToString(residentTypeRow["fd_physician_fax_no"]);
                        resident.Alergies = Convert.ToString(residentTypeRow["fd_alergies"]);
                        resident.HealthHistory = Convert.ToString(residentTypeRow["fd_health_history"]);
                        resident.AssFrequency = Convert.ToChar(residentTypeRow["fd_assess_frequency"]);
                        resident.QolaResident = (QolaResident)Convert.ToChar(residentTypeRow["fd_qola_resident"]);
                        resident.AllergiesNames = Convert.ToString(residentTypeRow["fd_allergies_name"]);
                        if (residentTypeRow["fd_status"].ToString() == "A")
                        {
                            resident.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            resident.Status = AvailabilityStatus.I;
                        }
                        user = new UserModel();
                        user.ID = Convert.ToInt32(residentTypeRow["fd_modified_by"]);
                        user.FirstName = Convert.ToString(residentTypeRow["fd_full_code"]);
                        user.LastName = Convert.ToString(residentTypeRow["fd_user_fname"]);
                        user.UserTypeName = Convert.ToString(residentTypeRow["fd_user_type"]);
                        resident.ModifiedBy = user;
                        resident.ModifiedOn = Convert.ToDateTime(residentTypeRow["fd_modified_on"]);
                        if (residentTypeRow["fd_admitted_from"] == System.DBNull.Value)
                        {
                            resident.AdmittedFrom = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.AdmittedFrom = Convert.ToDateTime(residentTypeRow["fd_admitted_from"]);
                        }
                        resident.CareAddress = Convert.ToString(residentTypeRow["fd_care_address"]);
                        resident.CareRelationship = Convert.ToString(residentTypeRow["fd_care_relationship"]);
                        resident.CareType = Convert.ToInt16(residentTypeRow["fd_care_type"]);
                        resident.FinanceAddress = Convert.ToString(residentTypeRow["fd_finance_address"]);
                        resident.FinanceRelationship = Convert.ToString(residentTypeRow["fd_finance_relationship"]);
                        resident.DNR = Convert.ToString(residentTypeRow["fd_DNR"]);
                        resident.FullCode = Convert.ToString(residentTypeRow["fd_full_code"]);
                        resident.FuneralArguments = Convert.ToString(residentTypeRow["fd_funeral_argument"]);
                        resident.PharmaceSelf = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        resident.PharmaceNursing = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.PharmaceFaxNumber = Convert.ToString(residentTypeRow["fd_pharmacy_fax_no"]);
                        resident.PharmacePhoneNo = Convert.ToString(residentTypeRow["fd_pharmacy_phone_no"]);
                        resident.ReligionContact = Convert.ToString(residentTypeRow["fd_religion_contact"]);
                        resident.ReligionHomePhone = Convert.ToString(residentTypeRow["fd_religion_home_phone"]);
                        resident.ReligionOffice = Convert.ToString(residentTypeRow["fd_religon_office"]);
                        resident.ReligiousAffiliation = Convert.ToString(residentTypeRow["fd_religious_affiliation"]);
                        resident.SuiteHandler = Convert.ToInt32(residentTypeRow["fd_suite_handler_id"]);
                        resident.CallHospital = Convert.ToChar(residentTypeRow["fd_call_hospital"]);
                        resident.FullCodeStatus = Convert.ToChar(residentTypeRow["fd_fullcode_status"]);
                        resident.DNRStatus = Convert.ToChar(residentTypeRow["fd_DNR_status"]);
                        string self = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        string nurse = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.ReligionType = Convert.ToInt16(residentTypeRow["fd_religious_affiliation_type"]);
                        resident.ReadType = Convert.ToInt16(residentTypeRow["fd_read_type"]);
                        resident.WriteType = Convert.ToInt16(residentTypeRow["fd_write_type"]);
                        resident.VoterType = Convert.ToString(residentTypeRow["fd_voter_type"]);
                        resident.VeteranType = Convert.ToString(residentTypeRow["fd_veteran_type"]);
                        resident.EducationType = Convert.ToInt16(residentTypeRow["fd_education_type"]);
                        resident.CurrentDiagnoses = Convert.ToString(residentTypeRow["fd_current_diagnoses"]);
                        resident.CulturalPreferences = Convert.ToString(residentTypeRow["fd_cultural_preferences"]);
                        resident.Guid = Convert.ToString(residentTypeRow["fd_GUID"]);
                        resident.PharmacyName = "";
                        if (self.Length > 0 || nurse.Length > 0)
                        {
                            if (self.Length > 0)
                            {
                                resident.PharmacyName = self;
                            }
                            if (nurse.Length > 0)
                            {
                                resident.PharmacyName = nurse;
                            }
                        }

                        resident.Favourite_song = Convert.ToString(residentTypeRow["fd_Favourite_song"]);
                        resident.Favourite_movie = Convert.ToString(residentTypeRow["fd_Favourite_movie"]);
                        resident.Number_of_children = Convert.ToString(residentTypeRow["fd_Number_of_children"]);
                        resident.Number_of_grandchildren = Convert.ToString(residentTypeRow["fd_Number_of_grandchildren"]);
                        resident.Favourite_dessert = Convert.ToString(residentTypeRow["fd_Favourite_dessert"]);
                        resident.Favourite_drink = Convert.ToString(residentTypeRow["fd_Favourite_drink"]);
                        resident.Favourite_flower = Convert.ToString(residentTypeRow["fd_Favourite_flower"]);
                        resident.Favourite_pets = Convert.ToString(residentTypeRow["fd_Favourite_pets"]);
                        resident.Wakeup_time = Convert.ToString(residentTypeRow["fd_Wakeup_time"]);
                        resident.Go_to_bed_at = Convert.ToString(residentTypeRow["fd_Go_to_bed_at"]);
                        resident.Favourite_past_time = Convert.ToString(residentTypeRow["fd_Favourite_past_time"]);
                        resident.Suite_Handler_Notes = Convert.ToString(residentTypeRow["fd_notes"]);
                        resident.Suite_Handler_Status = Convert.ToString(residentTypeRow["suite_handler_status"]);


                    }
                }
                if (resident.MaritalStatus == 1) resident.MaritalStatustext = "Married";
                else if (resident.MaritalStatus == 2) resident.MaritalStatustext = "Widowed";
                else if (resident.MaritalStatus == 3) resident.MaritalStatustext = "Single";
                else if(resident.MaritalStatus == 4) resident.MaritalStatustext = "Divorced";
                else resident.MaritalStatustext = "";
                return resident;
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

        public static ResidentModel GetActiveResidentById(int residentId)
        {
            string exception = string.Empty;
            ResidentModel resident = new ResidentModel();
            UserModel user;
            HomeModel home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_ACTIVE_RESIDENT_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", residentId);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables.Count > 0)
                {
                    foreach (DataRow residentTypeRow in residentReceive.Tables[0].Rows)
                    {
                        resident = new ResidentModel();
                        home = new HomeModel();
                        resident.ID = Convert.ToInt32(residentTypeRow["fd_id"]);
                        home.Id = Convert.ToInt32(residentTypeRow["fd_home_id"]);
                        home.Name = residentTypeRow["fd_home_name"].ToString();
                        home.Code = residentTypeRow["fd_code"].ToString();
                        resident.Home = home;
                        resident.SuiteIds = Convert.ToString(residentTypeRow["fd_suite_id"]);
                        resident.SuiteNo = Convert.ToString(residentTypeRow["fd_suite_no"]);
                        resident.MoveInDate = Convert.ToDateTime(residentTypeRow["fd_move_in_date"]);
                        if (residentTypeRow["fd_move_out_date"] == System.DBNull.Value)
                        {
                            resident.MoveOutDate = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.MoveOutDate = Convert.ToDateTime(residentTypeRow["fd_move_out_date"]);
                        }
                        resident.Occupancy = Convert.ToInt32(residentTypeRow["fd_occupancy"]);
                        resident.Age = Convert.ToInt32(residentTypeRow["fd_age"]);
                        resident.FirstName = Convert.ToString(residentTypeRow["fd_first_name"]);
                        resident.LastName = Convert.ToString(residentTypeRow["fd_last_name"]);
                        resident.Gendar = Convert.ToChar(residentTypeRow["fd_gender"]);
                        resident.ResidentImage = Convert.ToString(residentTypeRow["fd_image"]);
                        resident.Phone = Convert.ToString(residentTypeRow["fd_phone"]);
                        resident.BirthDate = Convert.ToDateTime(residentTypeRow["fd_birth_date"]);
                        resident.MBhealthNumber = Convert.ToString(residentTypeRow["fd_MB_health_number"]);


                        resident.BirthPlace = Convert.ToString(residentTypeRow["fd_birth_place"]);
                        resident.MaritalStatus = Convert.ToInt32(residentTypeRow["fd_marital_status"]);
                        resident.SignificatOther = Convert.ToString(residentTypeRow["fd_significat_other"]);
                        resident.RelationshipWithFamily = Convert.ToString(residentTypeRow["fd_relationship_family"]);
                        resident.RegisteredVoter = Convert.ToString(residentTypeRow["fd_registered_voter"]);
                        resident.Vetaran = Convert.ToString(residentTypeRow["fd_veteran"]);
                        resident.ReligiousAffiliation = Convert.ToString(residentTypeRow["fd_religious_affiliation"]);
                        resident.PersonalInvolvement = Convert.ToString(residentTypeRow["fd_personal_involvement"]);
                        resident.EducationLevel = Convert.ToString(residentTypeRow["fd_education_level"]);
                        resident.AbilityToRead = Convert.ToString(residentTypeRow["fd_ability_to_read"]);
                        resident.AbilityToWrite = Convert.ToString(residentTypeRow["fd_ability_to_write"]);
                        resident.OtherLanguage = Convert.ToString(residentTypeRow["fd_other_language"]);
                        resident.PastOccupationJobs = Convert.ToString(residentTypeRow["fd_past_occupations_jobs"]);
                        if (residentTypeRow["fd_hand_dominance"].ToString() != null && residentTypeRow["fd_hand_dominance"].ToString() != string.Empty)
                        {
                            resident.HandDominance = Convert.ToInt32(residentTypeRow["fd_hand_dominance"]);
                        }
                        else
                        {
                            resident.MaritalStatus = 0;
                        }
                        resident.ShortName = residentTypeRow["fd_short_name"] != System.DBNull.Value ? Convert.ToString(residentTypeRow["fd_short_name"]) : "";
                        resident.InsuranceCompany = Convert.ToString(residentTypeRow["fd_insurance_company"]);
                        resident.ContractNumber = Convert.ToString(residentTypeRow["fd_contract_number"]);
                        resident.GroupNumber = Convert.ToString(residentTypeRow["fd_group_number"]);

                        resident.POACare = Convert.ToString(residentTypeRow["fd_POA_care"]);
                        resident.POACareType = Convert.ToInt16(residentTypeRow["fd_POA_care_type"]);
                        resident.POACareStatus = Convert.ToChar(residentTypeRow["fd_POA_care_type_status"]);
                        resident.POACareType2Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_2_status"]);
                        resident.POACareType3Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_3_status"]);


                        resident.CareHomePhone = Convert.ToString(residentTypeRow["fd_care_home_phone_no"]);
                        resident.CareWorkPhone = Convert.ToString(residentTypeRow["fd_care_work_phone_no"]);
                        resident.CareCellPhone = Convert.ToString(residentTypeRow["fd_POA_care_cell_no"]);
                        resident.POACareHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_home_type"]);
                        resident.POACareBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_business_type"]);
                        resident.POACareCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_cell_type"]);
                        resident.CareEmail = Convert.ToString(residentTypeRow["fd_care_email"]);
                        resident.POAFinance = Convert.ToString(residentTypeRow["fd_POA_finance"]);
                        resident.POAFinanceType = Convert.ToInt16(residentTypeRow["fd_POA_finance_type"]);
                        resident.POAFinanceStatus = Convert.ToChar(residentTypeRow["fd_POA_finance_type_status"]);
                        resident.POAFinanceType2Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_2_status"]);
                        resident.POAFinanceType3Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_3_status"]);

                        resident.FinanceHomePhone = Convert.ToString(residentTypeRow["fd_finance_home_phone_no"]);
                        resident.FinanceWorkPhone = Convert.ToString(residentTypeRow["fd_finance_work_phone_no"]);
                        resident.FinanceCellPhone = Convert.ToString(residentTypeRow["fd_POA_finance_cell_no"]);
                        resident.POAFinanceHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_home_type"]);
                        resident.POAFinanceBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_business_type"]);
                        resident.POAFinanceCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_cell_type"]);
                        resident.FinanceEmail = Convert.ToString(residentTypeRow["fd_finance_email"]);

                        resident.Contract1 = Convert.ToString(residentTypeRow["fd_contact_1"]);
                        resident.Address1 = Convert.ToString(residentTypeRow["fd_address_1"]);
                        resident.Relationship1 = Convert.ToString(residentTypeRow["fd_relationship_1"]);
                        resident.HomePhone1 = Convert.ToString(residentTypeRow["fd_home_phone_1"]);
                        resident.BusinessPhone1 = Convert.ToString(residentTypeRow["fd_business_phone_1"]);
                        resident.CellPhone1 = Convert.ToString(residentTypeRow["fd_cell_phone_1"]);
                        resident.HomePhoneType1 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_1"]);
                        resident.BusinessPhoneType1 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_1"]);
                        resident.CellPhoneType1 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_1"]);
                        resident.Email1 = Convert.ToString(residentTypeRow["fd_email_1"]);

                        resident.Contract2 = Convert.ToString(residentTypeRow["fd_contact_2"]);
                        resident.Address2 = Convert.ToString(residentTypeRow["fd_address_2"]);
                        resident.Relationship2 = Convert.ToString(residentTypeRow["fd_relationship_2"]);
                        resident.HomePhone2 = Convert.ToString(residentTypeRow["fd_home_phone_2"]);
                        resident.BusinessPhone2 = Convert.ToString(residentTypeRow["fd_business_phone_2"]);
                        resident.CellPhone2 = Convert.ToString(residentTypeRow["fd_cell_phone_2"]);
                        resident.HomePhoneType2 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_2"]);
                        resident.BusinessPhoneType2 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_2"]);
                        resident.CellPhoneType2 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_2"]);
                        resident.Email2 = Convert.ToString(residentTypeRow["fd_email_2"]);

                        resident.Contract3 = Convert.ToString(residentTypeRow["fd_contact_3"]);
                        resident.Address3 = Convert.ToString(residentTypeRow["fd_address_3"]);
                        resident.Relationship3 = Convert.ToString(residentTypeRow["fd_relationship_3"]);
                        resident.HomePhone3 = Convert.ToString(residentTypeRow["fd_home_phone_3"]);
                        resident.BusinessPhone3 = Convert.ToString(residentTypeRow["fd_business_phone_3"]);
                        resident.CellPhone3 = Convert.ToString(residentTypeRow["fd_cell_phone_3"]);
                        resident.HomePhoneType3 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_3"]);
                        resident.BusinessPhoneType3 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_3"]);
                        resident.CellPhoneType3 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_3"]);
                        resident.Email3 = Convert.ToString(residentTypeRow["fd_email_3"]);

                        resident.Physician = Convert.ToString(residentTypeRow["fd_physician"]);
                        resident.PhysicianPhone = Convert.ToString(residentTypeRow["fd_physician_phone"]);
                        resident.Alergies = Convert.ToString(residentTypeRow["fd_alergies"]);
                        resident.HealthHistory = Convert.ToString(residentTypeRow["fd_health_history"]);
                        resident.AssFrequency = Convert.ToChar(residentTypeRow["fd_assess_frequency"]);
                        resident.QolaResident = (QolaResident)Convert.ToChar(residentTypeRow["fd_qola_resident"]);
                        resident.AllergiesNames = Convert.ToString(residentTypeRow["fd_allergies_name"]);
                        if (residentTypeRow["fd_status"].ToString() == "A")
                        {
                            resident.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            resident.Status = AvailabilityStatus.I;
                        }
                        user = new UserModel();
                        user.ID = Convert.ToInt32(residentTypeRow["fd_modified_by"]);
                        resident.ModifiedBy = user;

                        if (residentTypeRow["fd_admitted_from"] == System.DBNull.Value)
                        {
                            resident.AdmittedFrom = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.AdmittedFrom = Convert.ToDateTime(residentTypeRow["fd_admitted_from"]);
                        }
                        resident.CareAddress = Convert.ToString(residentTypeRow["fd_care_address"]);
                        resident.CareRelationship = Convert.ToString(residentTypeRow["fd_care_relationship"]);
                        resident.CareType = Convert.ToInt16(residentTypeRow["fd_care_type"]);
                        resident.FinanceAddress = Convert.ToString(residentTypeRow["fd_finance_address"]);
                        resident.FinanceRelationship = Convert.ToString(residentTypeRow["fd_finance_relationship"]);
                        resident.DNR = Convert.ToString(residentTypeRow["fd_DNR"]);
                        resident.FullCode = Convert.ToString(residentTypeRow["fd_full_code"]);
                        resident.FuneralArguments = Convert.ToString(residentTypeRow["fd_funeral_argument"]);
                        resident.PharmaceSelf = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        resident.PharmaceNursing = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.PharmaceFaxNumber = Convert.ToString(residentTypeRow["fd_pharmacy_fax_no"]);
                        resident.PharmacePhoneNo = Convert.ToString(residentTypeRow["fd_pharmacy_phone_no"]);
                        resident.ReligionContact = Convert.ToString(residentTypeRow["fd_religion_contact"]);
                        resident.ReligionHomePhone = Convert.ToString(residentTypeRow["fd_religion_home_phone"]);
                        resident.ReligionOffice = Convert.ToString(residentTypeRow["fd_religon_office"]);

                        resident.SuiteHandler = Convert.ToInt32(residentTypeRow["fd_suite_handler_id"]);
                        resident.CallHospital = Convert.ToChar(residentTypeRow["fd_call_hospital"]);
                        resident.PhysicianFaxNo = Convert.ToString(residentTypeRow["fd_physician_fax_no"]);
                        resident.DNRStatus = Convert.ToChar(residentTypeRow["fd_DNR_status"]);
                        resident.FullCodeStatus = Convert.ToChar(residentTypeRow["fd_fullcode_status"]);
                        resident.BedPosition = Convert.ToChar(residentTypeRow["AS_bed"]);
                        resident.AnniversaryDate = Convert.ToDateTime(residentTypeRow["fd_aniversary_date"]);
                        resident.ReligionType = Convert.ToInt16(residentTypeRow["fd_religious_affiliation_type"]);
                        resident.VoterType = Convert.ToString(residentTypeRow["fd_voter_type"]);
                        resident.ReadType = Convert.ToInt16(residentTypeRow["fd_read_type"]);
                        resident.WriteType = Convert.ToInt16(residentTypeRow["fd_write_type"]);

                        resident.EducationType = Convert.ToInt16(residentTypeRow["fd_education_type"]);
                        resident.VeteranType = Convert.ToString(residentTypeRow["fd_veteran_type"]);
                        string self = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        string nurse = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.CurrentDiagnoses = Convert.ToString(residentTypeRow["fd_current_diagnoses"]);
                        resident.CulturalPreferences = Convert.ToString(residentTypeRow["fd_cultural_preferences"]);

                        resident.PharmacyName = "";
                        if (self.Length > 0 || nurse.Length > 0)
                        {
                            if (self.Length > 0)
                            {
                                resident.PharmacyName = self;
                            }
                            if (nurse.Length > 0)
                            {
                                resident.PharmacyName = nurse;
                            }
                        }


                        resident.Favourite_song = Convert.ToString(residentTypeRow["fd_Favourite_song"]);
                        resident.Favourite_movie = Convert.ToString(residentTypeRow["fd_Favourite_movie"]);
                        resident.Number_of_children = Convert.ToString(residentTypeRow["fd_Number_of_children"]);
                        resident.Number_of_grandchildren = Convert.ToString(residentTypeRow["fd_Number_of_grandchildren"]);
                        resident.Favourite_dessert = Convert.ToString(residentTypeRow["fd_Favourite_dessert"]);
                        resident.Favourite_drink = Convert.ToString(residentTypeRow["fd_Favourite_drink"]);
                        resident.Favourite_flower = Convert.ToString(residentTypeRow["fd_Favourite_flower"]);
                        resident.Favourite_pets = Convert.ToString(residentTypeRow["fd_Favourite_pets"]);
                        resident.Wakeup_time = Convert.ToString(residentTypeRow["fd_Wakeup_time"]);
                        resident.Go_to_bed_at = Convert.ToString(residentTypeRow["fd_Go_to_bed_at"]);
                        resident.Favourite_past_time = Convert.ToString(residentTypeRow["fd_Favourite_past_time"]);

                    }
                }
                return resident;
            }
            catch (Exception ex)
            {
                exception = "GetActiveResidentById |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static ResidentModel GetInActiveResidentById(int residentId)
        {
            string exception = string.Empty;
            ResidentModel resident = new ResidentModel();
            UserModel user;
            HomeModel home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_INACTIVE_RESIDENT_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", residentId);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables.Count > 0)
                {
                    foreach (DataRow residentTypeRow in residentReceive.Tables[0].Rows)
                    {
                        resident = new ResidentModel();
                        home = new HomeModel();
                        resident.ID = Convert.ToInt32(residentTypeRow["fd_id"]);
                        home.Id = Convert.ToInt32(residentTypeRow["fd_home_id"]);
                        home.Name = residentTypeRow["fd_home_name"].ToString();
                        home.Code = residentTypeRow["fd_code"].ToString();
                        resident.Home = home;
                        resident.SuiteIds = Convert.ToString(residentTypeRow["fd_suite_id"]);
                        resident.SuiteNo = Convert.ToString(residentTypeRow["fd_suite_no"]);
                        resident.MoveInDate = Convert.ToDateTime(residentTypeRow["fd_move_in_date"]);
                        if (residentTypeRow["fd_move_out_date"] == System.DBNull.Value)
                        {
                            resident.MoveOutDate = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.MoveOutDate = Convert.ToDateTime(residentTypeRow["fd_move_out_date"]);
                        }
                        resident.Occupancy = Convert.ToInt32(residentTypeRow["fd_occupancy"]);
                        resident.Age = Convert.ToInt32(residentTypeRow["fd_age"]);
                        resident.FirstName = Convert.ToString(residentTypeRow["fd_first_name"]);
                        resident.LastName = Convert.ToString(residentTypeRow["fd_last_name"]);
                        resident.Gendar = Convert.ToChar(residentTypeRow["fd_gender"]);
                        resident.ResidentImage = Convert.ToString(residentTypeRow["fd_image"]);
                        resident.Phone = Convert.ToString(residentTypeRow["fd_phone"]);
                        resident.BirthDate = Convert.ToDateTime(residentTypeRow["fd_birth_date"]);
                        resident.MBhealthNumber = Convert.ToString(residentTypeRow["fd_MB_health_number"]);


                        resident.BirthPlace = Convert.ToString(residentTypeRow["fd_birth_place"]);
                        resident.MaritalStatus = Convert.ToInt32(residentTypeRow["fd_marital_status"]);
                        resident.SignificatOther = Convert.ToString(residentTypeRow["fd_significat_other"]);
                        resident.RelationshipWithFamily = Convert.ToString(residentTypeRow["fd_relationship_family"]);
                        resident.RegisteredVoter = Convert.ToString(residentTypeRow["fd_registered_voter"]);
                        resident.Vetaran = Convert.ToString(residentTypeRow["fd_veteran"]);
                        resident.ReligiousAffiliation = Convert.ToString(residentTypeRow["fd_religious_affiliation"]);
                        resident.PersonalInvolvement = Convert.ToString(residentTypeRow["fd_personal_involvement"]);
                        resident.EducationLevel = Convert.ToString(residentTypeRow["fd_education_level"]);
                        resident.AbilityToRead = Convert.ToString(residentTypeRow["fd_ability_to_read"]);
                        resident.AbilityToWrite = Convert.ToString(residentTypeRow["fd_ability_to_write"]);
                        resident.OtherLanguage = Convert.ToString(residentTypeRow["fd_other_language"]);
                        resident.PastOccupationJobs = Convert.ToString(residentTypeRow["fd_past_occupations_jobs"]);
                        if (residentTypeRow["fd_hand_dominance"].ToString() != null && residentTypeRow["fd_hand_dominance"].ToString() != string.Empty)
                        {
                            resident.HandDominance = Convert.ToInt32(residentTypeRow["fd_hand_dominance"]);
                        }
                        else
                        {
                            resident.MaritalStatus = 0;
                        }
                        resident.InsuranceCompany = Convert.ToString(residentTypeRow["fd_insurance_company"]);
                        resident.ContractNumber = Convert.ToString(residentTypeRow["fd_contract_number"]);
                        resident.GroupNumber = Convert.ToString(residentTypeRow["fd_group_number"]);

                        resident.POACare = Convert.ToString(residentTypeRow["fd_POA_care"]);
                        resident.CareHomePhone = Convert.ToString(residentTypeRow["fd_care_home_phone_no"]);
                        resident.CareWorkPhone = Convert.ToString(residentTypeRow["fd_care_work_phone_no"]);
                        resident.CareEmail = Convert.ToString(residentTypeRow["fd_care_email"]);
                        resident.POAFinance = Convert.ToString(residentTypeRow["fd_POA_finance"]);
                        resident.FinanceHomePhone = Convert.ToString(residentTypeRow["fd_finance_home_phone_no"]);
                        resident.FinanceWorkPhone = Convert.ToString(residentTypeRow["fd_finance_work_phone_no"]);
                        resident.FinanceEmail = Convert.ToString(residentTypeRow["fd_finance_email"]);

                        resident.Contract1 = Convert.ToString(residentTypeRow["fd_contact_1"]);
                        resident.Address1 = Convert.ToString(residentTypeRow["fd_address_1"]);
                        resident.Relationship1 = Convert.ToString(residentTypeRow["fd_relationship_1"]);
                        resident.HomePhone1 = Convert.ToString(residentTypeRow["fd_home_phone_1"]);
                        resident.BusinessPhone1 = Convert.ToString(residentTypeRow["fd_business_phone_1"]);
                        resident.CellPhone1 = Convert.ToString(residentTypeRow["fd_cell_phone_1"]);


                        resident.Email1 = Convert.ToString(residentTypeRow["fd_email_1"]);

                        resident.Contract2 = Convert.ToString(residentTypeRow["fd_contact_2"]);
                        resident.Address2 = Convert.ToString(residentTypeRow["fd_address_2"]);
                        resident.Relationship2 = Convert.ToString(residentTypeRow["fd_relationship_2"]);
                        resident.HomePhone2 = Convert.ToString(residentTypeRow["fd_home_phone_2"]);
                        resident.BusinessPhone2 = Convert.ToString(residentTypeRow["fd_business_phone_2"]);
                        resident.CellPhone2 = Convert.ToString(residentTypeRow["fd_cell_phone_2"]);
                        resident.Email2 = Convert.ToString(residentTypeRow["fd_email_2"]);

                        resident.Contract3 = Convert.ToString(residentTypeRow["fd_contact_3"]);
                        resident.Address3 = Convert.ToString(residentTypeRow["fd_address_3"]);
                        resident.Relationship3 = Convert.ToString(residentTypeRow["fd_relationship_3"]);
                        resident.HomePhone3 = Convert.ToString(residentTypeRow["fd_home_phone_3"]);
                        resident.BusinessPhone3 = Convert.ToString(residentTypeRow["fd_business_phone_3"]);
                        resident.CellPhone3 = Convert.ToString(residentTypeRow["fd_cell_phone_3"]);
                        resident.Email3 = Convert.ToString(residentTypeRow["fd_email_3"]);

                        resident.Physician = Convert.ToString(residentTypeRow["fd_physician"]);
                        resident.PhysicianPhone = Convert.ToString(residentTypeRow["fd_physician_phone"]);
                        resident.Alergies = Convert.ToString(residentTypeRow["fd_alergies"]);
                        resident.HealthHistory = Convert.ToString(residentTypeRow["fd_health_history"]);
                        resident.AssFrequency = Convert.ToChar(residentTypeRow["fd_assess_frequency"]);
                        resident.QolaResident = (QolaResident)Convert.ToChar(residentTypeRow["fd_qola_resident"]);
                        resident.AllergiesNames = Convert.ToString(residentTypeRow["fd_allergies_name"]);
                        if (residentTypeRow["fd_status"].ToString() == "A")
                        {
                            resident.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            resident.Status = AvailabilityStatus.I;
                        }
                        user = new UserModel();
                        user.ID = Convert.ToInt32(residentTypeRow["fd_modified_by"]);
                        resident.ModifiedBy = user;

                        if (residentTypeRow["fd_admitted_from"] == System.DBNull.Value)
                        {
                            resident.AdmittedFrom = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.AdmittedFrom = Convert.ToDateTime(residentTypeRow["fd_admitted_from"]);
                        }
                        resident.CareAddress = Convert.ToString(residentTypeRow["fd_care_address"]);
                        resident.CareRelationship = Convert.ToString(residentTypeRow["fd_care_relationship"]);
                        resident.CareType = Convert.ToInt16(residentTypeRow["fd_care_type"]);
                        resident.FinanceAddress = Convert.ToString(residentTypeRow["fd_finance_address"]);
                        resident.FinanceRelationship = Convert.ToString(residentTypeRow["fd_finance_relationship"]);
                        resident.DNR = Convert.ToString(residentTypeRow["fd_DNR"]);
                        resident.FullCode = Convert.ToString(residentTypeRow["fd_full_code"]);
                        resident.FuneralArguments = Convert.ToString(residentTypeRow["fd_funeral_argument"]);
                        resident.PharmaceSelf = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        resident.PharmaceNursing = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.PharmaceFaxNumber = Convert.ToString(residentTypeRow["fd_pharmacy_fax_no"]);
                        resident.PharmacePhoneNo = Convert.ToString(residentTypeRow["fd_pharmacy_phone_no"]);
                        resident.ReligionContact = Convert.ToString(residentTypeRow["fd_religion_contact"]);
                        resident.ReligionHomePhone = Convert.ToString(residentTypeRow["fd_religion_home_phone"]);
                        resident.ReligionOffice = Convert.ToString(residentTypeRow["fd_religon_office"]);

                        resident.SuiteHandler = Convert.ToInt32(residentTypeRow["fd_suite_handler_id"]);
                        resident.CallHospital = Convert.ToChar(residentTypeRow["fd_call_hospital"]);
                        resident.CulturalPreferences = Convert.ToString(residentTypeRow["fd_cultural_preferences"]);
                        resident.Guid = Convert.ToString(residentTypeRow["fd_GUID"]);

                        resident.Favourite_song = Convert.ToString(residentTypeRow["fd_Favourite_song"]);
                        resident.Favourite_movie = Convert.ToString(residentTypeRow["fd_Favourite_movie"]);
                        resident.Number_of_children = Convert.ToString(residentTypeRow["fd_Number_of_children"]);
                        resident.Number_of_grandchildren = Convert.ToString(residentTypeRow["fd_Number_of_grandchildren"]);
                        resident.Favourite_dessert = Convert.ToString(residentTypeRow["fd_Favourite_dessert"]);
                        resident.Favourite_drink = Convert.ToString(residentTypeRow["fd_Favourite_drink"]);
                        resident.Favourite_flower = Convert.ToString(residentTypeRow["fd_Favourite_flower"]);
                        resident.Favourite_pets = Convert.ToString(residentTypeRow["fd_Favourite_pets"]);
                        resident.Wakeup_time = Convert.ToString(residentTypeRow["fd_Wakeup_time"]);
                        resident.Go_to_bed_at = Convert.ToString(residentTypeRow["fd_Go_to_bed_at"]);
                        resident.Favourite_past_time = Convert.ToString(residentTypeRow["fd_Favourite_past_time"]);
                    }
                }
                return resident;
            }
            catch (Exception ex)
            {
                exception = "GetInActiveResidentById |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ResidentModel> GetResidentNamesByHomeId(int homeId)
        {
            string exception = string.Empty, sResidentName = string.Empty;
            Collection<ResidentModel> residents = new Collection<ResidentModel>();
            ResidentModel resident;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_NAME_BY_HOME_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);
                if ((residentsReceive != null) && residentsReceive.Tables[0].Rows.Count > 0)
                {
                    DataView dvResident = residentsReceive.Tables[0].DefaultView;

                    DataTable dtResident = dvResident.ToTable();

                    for (int index = 0; index <= dtResident.Rows.Count - 1; index++)
                    {
                        resident = new ResidentModel();
                        resident.ID = Convert.ToInt32(dtResident.Rows[index]["fd_id"]);
                        sResidentName = Convert.ToString(dtResident.Rows[index]["fd_last_name"] + ", " + dtResident.Rows[index]["fd_first_name"]);
                        sResidentName = dtResident.Rows[index]["fd_qola_resident"].ToString() == "Y" ? sResidentName + " *" : sResidentName;
                        resident.AwayStatus = Convert.ToChar(dtResident.Rows[index]["As_away_status"]);
                        resident.ResidentName = sResidentName;
                        residents.Add(resident);
                    }
                }
                return residents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentNamesByHomeId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ResidentModel> GetResidentListByFloor(int homeId)
        {
            string exception = string.Empty, sResidentName = string.Empty;
            Collection<ResidentModel> residents = new Collection<ResidentModel>();
            ResidentModel resident;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENTLIST_BY_FLOOR, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);
                if ((residentsReceive != null) && residentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= residentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        resident = new ResidentModel();
                        resident.ID = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_id"]);
                        resident.SuiteHandler = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_floor"]);
                        sResidentName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_last_name"] + ", " + residentsReceive.Tables[0].Rows[index]["fd_first_name"]);
                        sResidentName = residentsReceive.Tables[0].Rows[index]["fd_qola_resident"].ToString() == "Y" ? sResidentName + " *" : sResidentName;
                        resident.AwayStatus = Convert.ToChar(residentsReceive.Tables[0].Rows[index]["As_away_status"]);
                        resident.ResidentName = sResidentName;
                        residents.Add(resident);
                    }
                }
                return residents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentNamesByHomeId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ResidentModel> GetResidentBySuitIdOrName(int homeId, string suitIdOrName, char status)
        {
            string exception = string.Empty;
            Collection<ResidentModel> residents = new Collection<ResidentModel>();
            ResidentModel resident;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_BY_SUITID_OR_NAME, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@searchId", suitIdOrName);
                l_Cmd.Parameters.AddWithValue("@status", status);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow residentTypeRow in residentReceive.Tables[0].Rows)
                    {
                        resident = new ResidentModel();
                        resident.ID = Convert.ToInt32(residentTypeRow["fd_id"]);
                        resident.FirstName = residentTypeRow["fd_first_name"].ToString();
                        resident.LastName = residentTypeRow["fd_last_name"].ToString();
                        resident.Gendar = Convert.ToChar(residentTypeRow["fd_gender"]);
                        resident.SuiteNo = residentTypeRow["fd_suite_no"].ToString();
                        residents.Add(resident);
                    }
                }
                return residents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentBySuitIdOrName |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet GetMARReport(int residentId, DateTime FromDate, DateTime ToDate)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_MARS_REPORTS, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", residentId);
                l_Cmd.Parameters.AddWithValue("@fromDate", FromDate);
                l_Cmd.Parameters.AddWithValue("@todate", ToDate);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "GetResidentBySuitIdOrName |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static DataSet dsIsCheckPrescription(int Rid)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXISTS_PRESCRIPTION_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", Rid);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "dsIsCheckPrescription |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static DataSet dsIsResidentForGoalSheet(int Rid)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXISTS_GOAL_SHEET_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", Rid);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "dsIsResidentForGoalSheet |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static DataSet dsIsResidentForFallRiskAssess(int Rid)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXISTS_FALL_RISK_ASSESS_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", Rid);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "dsIsResidentForFallRiskAssess |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static DataSet dsIsResidentForPlanOfCare(int Rid)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXISTS_PLAN_OF_CARE_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", Rid);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "dsIsResidentForPlanOfCare |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static DataSet dsIsForDietaryAssessment(int Rid)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXISTS_DIETARY_ASSESS_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", Rid);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "dsIsForDietaryAssessment |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet dsMedicalAllergyReceiveByResidentId(int RId)
        {
            string exception = string.Empty;
            DataSet dsReceive = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_DS_MEDICAL_ALLAERGY_BY_RESIDENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", RId);
                dsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsReceive);
                return dsReceive;
            }
            catch (Exception ex)
            {
                exception = "dsMedicalAllergyReceiveByResidentId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ResidentModel> GetResidentSearchByHomeId(int homeId, string Key, char status)
        {
            string exception = string.Empty;
            Collection<ResidentModel> residents = new Collection<ResidentModel>();
            ResidentModel resident;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_SEARCH_BY_HOME_ID_KEY, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@key", Key);
                l_Cmd.Parameters.AddWithValue("@status", status);
                l_Cmd.CommandTimeout = 900;
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);
                if (residentsReceive != null && residentsReceive.Tables.Count > 0 && residentsReceive.Tables[0].Rows.Count > 0)
                {
                    if(status == 'N')
                    {
                        for (int index = 0; index <= residentsReceive.Tables[0].Rows.Count - 1; index++)
                        {
                            resident = new ResidentModel();
                            resident.ID = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_id"]);
                            resident.FirstName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_first_name"]);
                            resident.LastName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_last_name"]);
                            resident.ResidentImage = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_image"]);
                            resident.ShortName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["searchId"]);

                            resident.ShortName2 = Convert.ToString(residentsReceive.Tables[0].Rows[index]["searchId_2"]);

                            resident.QolaResident = (QolaResident)Convert.ToChar(residentsReceive.Tables[0].Rows[index]["fd_qola_resident"]);
                            residents.Add(resident);
                        }
                    }
                    else
                    {
                        for (int index = 0; index <= residentsReceive.Tables[0].Rows.Count - 1; index++)
                        {
                            resident = new ResidentModel();
                            resident.ID = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_id"]);
                            resident.ShortName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["searchId"]);
                            resident.QolaResident = (QolaResident)Convert.ToChar(residentsReceive.Tables[0].Rows[index]["fd_qola_resident"]);
                            residents.Add(resident);
                        }
                    }
                }
                return residents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentSearchByHomeId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ResidentModel> GetResident_Archive_SearchByHomeId(int homeId, string Key)
        {
            string exception = string.Empty;
            Collection<ResidentModel> residents = new Collection<ResidentModel>();
            ResidentModel resident;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("Get_Resident_Archive_Search_By_Home_Id_Key", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@key", Key);
                l_Cmd.CommandTimeout = 900;
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);
                if (residentsReceive != null && residentsReceive.Tables.Count > 0 && residentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= residentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        resident = new ResidentModel();
                        resident.ID = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_id"]);
                        resident.FirstName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_first_name"]);
                        resident.LastName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_last_name"]);
                        resident.ResidentImage = Convert.ToString(residentsReceive.Tables[0].Rows[index]["fd_image"]);
                        resident.ShortName = Convert.ToString(residentsReceive.Tables[0].Rows[index]["searchId"]);
                        resident.QolaResident = (QolaResident)Convert.ToChar(residentsReceive.Tables[0].Rows[index]["fd_qola_resident"]);
                        resident.pass_away = Convert.ToString(residentsReceive.Tables[0].Rows[index]["pass_away"]);
                        residents.Add(resident);
                    }
                }
                return residents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentSearchByHomeId |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static bool UpdateImagePathByResidentId(int iResidentId, string sImagePath)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_IMAGEPATH_BY_RESIDENTID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", iResidentId);
                l_Cmd.Parameters.AddWithValue("@imagePath", sImagePath);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateImagePathByResidentId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static DataSet GetStatusForResidentOtherTabs(int Rid)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXISTS_STATUS_FOR_RESIDENT_OTHERSTABS, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", Rid);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "GetStatusForResidentOtherTabs |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static DataSet GetResidentListForNavigation(int homeId, string suitIdOrName, char status)
        {
            string exception = string.Empty;
            DataSet residentReceive = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_BY_SUITID_OR_NAME, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@searchId", suitIdOrName);
                l_Cmd.Parameters.AddWithValue("@status", status);
                residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                return residentReceive;
            }
            catch (Exception ex)
            {
                exception = "GetResidentListForNavigation |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet GetEmergencyResidentDetails(int homeId, string sResidentCare)
        {
            string exception = string.Empty;
            DataSet residentReceive = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EMERGENCY_RESIDENT_DETAILS, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                //l_Cmd.Parameters.AddWithValue("@residentCare", sResidentCare);
                residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                return residentReceive;
            }
            catch (Exception ex)
            {
                exception = "GetEmergencyResidentDetails |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static ResidentModel GetEmergencyContactDetailsByResidentId(int iResidentId)
        {
            string exception = string.Empty;
            ResidentModel l_Residents = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EMERGENCY_CONTACT_DETAILS_BY_RESIDENT_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", iResidentId);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables[0].Rows.Count > 0)
                {
                    DataTable residentTypeRow = residentReceive.Tables[0];
                    l_Residents = new ResidentModel();
                    l_Residents.CallHospital = Convert.ToChar(residentTypeRow.Rows[0]["fd_call_hospital"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_call_hospital"].ToString() : "N");
                    l_Residents.Contract1 = residentTypeRow.Rows[0]["fd_contact_1"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_contact_1"].ToString() : "";
                    l_Residents.Contract2 = residentTypeRow.Rows[0]["fd_contact_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_contact_2"].ToString() : "";
                    l_Residents.Contract3 = residentTypeRow.Rows[0]["fd_contact_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_contact_3"].ToString() : "";
                    l_Residents.Address1 = residentTypeRow.Rows[0]["fd_address_1"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_address_1"].ToString() : "";
                    l_Residents.Address2 = residentTypeRow.Rows[0]["fd_address_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_address_2"].ToString() : "";
                    l_Residents.Address3 = residentTypeRow.Rows[0]["fd_address_3"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_address_3"].ToString() : "";
                    l_Residents.Relationship1 = residentTypeRow.Rows[0]["fd_relationship_1"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_relationship_1"].ToString() : "";
                    l_Residents.Relationship2 = residentTypeRow.Rows[0]["fd_relationship_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_relationship_2"].ToString() : "";
                    l_Residents.Relationship3 = residentTypeRow.Rows[0]["fd_relationship_3"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_relationship_3"].ToString() : "";
                    l_Residents.HomePhone1 = residentTypeRow.Rows[0]["fd_home_phone_1"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_home_phone_1"].ToString() : "";
                    l_Residents.HomePhone2 = residentTypeRow.Rows[0]["fd_home_phone_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_home_phone_2"].ToString() : "";
                    l_Residents.HomePhone3 = residentTypeRow.Rows[0]["fd_home_phone_3"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_home_phone_3"].ToString() : "";
                    l_Residents.BusinessPhone1 = residentTypeRow.Rows[0]["fd_business_phone_1"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_business_phone_1"].ToString() : "";
                    l_Residents.BusinessPhone2 = residentTypeRow.Rows[0]["fd_business_phone_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_business_phone_2"].ToString() : "";
                    l_Residents.BusinessPhone3 = residentTypeRow.Rows[0]["fd_business_phone_3"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_business_phone_3"].ToString() : "";
                    l_Residents.CellPhone1 = residentTypeRow.Rows[0]["fd_cell_phone_1"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_cell_phone_1"].ToString() : "";
                    l_Residents.CellPhone2 = residentTypeRow.Rows[0]["fd_cell_phone_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_cell_phone_2"].ToString() : "";
                    l_Residents.CellPhone3 = residentTypeRow.Rows[0]["fd_cell_phone_3"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_cell_phone_3"].ToString() : "";
                    l_Residents.Email1 = residentTypeRow.Rows[0]["fd_email_1"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_email_1"].ToString() : "";
                    l_Residents.Email2 = residentTypeRow.Rows[0]["fd_email_2"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_email_2"].ToString() : "";
                    l_Residents.Email3 = residentTypeRow.Rows[0]["fd_email_3"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_email_3"].ToString() : "";
                    l_Residents.InsuranceCompany = residentTypeRow.Rows[0]["fd_insurance_company"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_insurance_company"].ToString() : "";
                    l_Residents.ContractNumber = residentTypeRow.Rows[0]["fd_contract_number"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_contract_number"].ToString() : "";
                    l_Residents.GroupNumber = residentTypeRow.Rows[0]["fd_group_number"] != System.DBNull.Value ? residentTypeRow.Rows[0]["fd_group_number"].ToString() : "";
                }
                return l_Residents;
            }
            catch (Exception ex)
            {
                exception = "GetEmergencyContactDetailsByResidentId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool UpdateResidentEmail(ResidentModel updateResident)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_RESIDENT_EMAIL, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", updateResident.ID);
                l_Cmd.Parameters.AddWithValue("@Email1", updateResident.Email1);
                l_Cmd.Parameters.AddWithValue("@modifiedBy", updateResident.ModifiedBy.ID);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateResident Email |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet GetNurseCalenderDailyEmailReport(string sfromdate)
        {
            DataSet ds = null;
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_NURSE_DAILY_REPORT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (sfromdate != "")
                {
                    l_Cmd.Parameters.AddWithValue("@reportDate", sfromdate);
                }

                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "GetNurseCalenderDailyEmailReport |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool UpdateResidentMedicalAllergy(ResidentModel updateResidentMedicalInfo)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_RESIDENT_MEDICAL_ALLERGY, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", updateResidentMedicalInfo.ID);
                l_Cmd.Parameters.AddWithValue("@deleteRowIds", updateResidentMedicalInfo.DeleteRowAllergyId);
                l_Cmd.Parameters.AddWithValue("@xmlInsertString", new SqlXml(new XmlTextReader(updateResidentMedicalInfo.XMLMedicalAllergyInsert, XmlNodeType.Document, null)));
                l_Cmd.Parameters.AddWithValue("@xmlUpdateString", new SqlXml(new XmlTextReader(updateResidentMedicalInfo.XMLMedicalAllergyUpdate, XmlNodeType.Document, null)));
                l_Cmd.Parameters.AddWithValue("@modifiedby", updateResidentMedicalInfo.ModifiedBy.ID);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentMedicalAllergy |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static List<DietaryAssessmentModel> GetMedicallAllergyListByResidentId(int iResidentId)
        {
            string exception = string.Empty;
            List<DietaryAssessmentModel> liDietAllergy = new List<DietaryAssessmentModel>();
            DietaryAssessmentModel assessment;
            AllergiesModel allergies;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_MEDICAL_ALLERGY, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", iResidentId);
                DataSet DitAssesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(DitAssesReceive);
                if (DitAssesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= DitAssesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        assessment = new DietaryAssessmentModel();
                        allergies = new AllergiesModel();
                        allergies.ID = Convert.ToInt32(DitAssesReceive.Tables[0].Rows[index]["fd_allergy_id"]);
                        allergies.Name = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["AllergyName"]);
                        assessment.DietAllergyNote = Convert.ToString(DitAssesReceive.Tables[0].Rows[index]["fd_note"]);
                        assessment.Allergy = allergies;
                        liDietAllergy.Add(assessment);
                    }
                }
                return liDietAllergy;
            }
            catch (Exception ex)
            {
                exception = "GetMedicallAllergyListByResidentId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ResidentModel> GetResidentAttendanceSearchByHomeId(int homeId, string Key, char status)
        {
            string exception = string.Empty;
            Collection<ResidentModel> residents = new Collection<ResidentModel>();
            ResidentModel resident;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_ATTENDANCE_SEARCH_BY_HOME_ID_KEY, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@key", Key);
                l_Cmd.Parameters.AddWithValue("@status", status);
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);
                if (residentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= residentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        resident = new ResidentModel();
                        resident.ID = Convert.ToInt32(residentsReceive.Tables[0].Rows[index]["fd_id"]);
                        resident.InsuranceCompany = Convert.ToString(residentsReceive.Tables[0].Rows[index]["searchId"]);
                        resident.QolaResident = (QolaResident)Convert.ToChar(residentsReceive.Tables[0].Rows[index]["fd_qola_resident"]);
                        residents.Add(resident);
                    }
                }
                return residents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentAttendancewSearchByHomeId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static int[] GetResidentGeneralInfoExists(ResidentModel addResidentGeneralInfo)
        {
            string exception = string.Empty;
            int residentId = 0;
            int[] iResultId = new int[2];

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_GENERALINFO_EXISTS, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output; // addResidentGeneralInfo.ID);
                l_Cmd.Parameters.AddWithValue("@homeId", addResidentGeneralInfo.Home.Id);
                l_Cmd.Parameters.AddWithValue("@suiteId", addResidentGeneralInfo.SuiteIds);
                l_Cmd.Parameters.AddWithValue("@Occupancy ", addResidentGeneralInfo.Occupancy);
                l_Cmd.Parameters.AddWithValue("@firstname", addResidentGeneralInfo.FirstName);
                l_Cmd.Parameters.AddWithValue("@lastname", addResidentGeneralInfo.LastName);
                l_Cmd.Parameters.AddWithValue("@gender", addResidentGeneralInfo.Gendar);
                l_Cmd.Parameters.AddWithValue("@birthDate", addResidentGeneralInfo.BirthDate);
                l_Cmd.Parameters.AddWithValue("@MInDate", addResidentGeneralInfo.MoveInDate);
                if (Convert.ToDateTime(addResidentGeneralInfo.MoveOutDate) != Convert.ToDateTime("01/01/0001"))
                {
                    l_Cmd.Parameters.AddWithValue("@MOutDate ", addResidentGeneralInfo.MoveOutDate);
                }
                if (Convert.ToDateTime(addResidentGeneralInfo.AdmittedFrom) != Convert.ToDateTime("01/01/0001"))
                {
                    l_Cmd.Parameters.AddWithValue("@admittedFrom ", addResidentGeneralInfo.AdmittedFrom);
                }
                l_Cmd.Parameters.AddWithValue("@birthPlace", addResidentGeneralInfo.BirthPlace);
                l_Cmd.Parameters.AddWithValue("@maritalStatus ", addResidentGeneralInfo.MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@MBHealthNo", addResidentGeneralInfo.MBhealthNumber);
                l_Cmd.Parameters.AddWithValue("@significatOther", addResidentGeneralInfo.SignificatOther);
                l_Cmd.Parameters.AddWithValue("@registeredVoter", addResidentGeneralInfo.RegisteredVoter);
                l_Cmd.Parameters.AddWithValue("@religiousAffiliate", addResidentGeneralInfo.ReligiousAffiliation);
                l_Cmd.Parameters.AddWithValue("@relationshipFamily", addResidentGeneralInfo.RelationshipWithFamily);
                l_Cmd.Parameters.AddWithValue("@educationLevel ", addResidentGeneralInfo.EducationLevel);
                l_Cmd.Parameters.AddWithValue("@abilityToWrite", addResidentGeneralInfo.AbilityToWrite);
                l_Cmd.Parameters.AddWithValue("@pastOccupationJobs", addResidentGeneralInfo.PastOccupationJobs);
                l_Cmd.Parameters.AddWithValue("@veteran", addResidentGeneralInfo.Vetaran);
                l_Cmd.Parameters.AddWithValue("@PersonalInvolve", addResidentGeneralInfo.PersonalInvolvement);
                l_Cmd.Parameters.AddWithValue("@qolaResident", Convert.ToChar(addResidentGeneralInfo.QolaResident));
                l_Cmd.Parameters.AddWithValue("@phoneNo", addResidentGeneralInfo.Phone);
                l_Cmd.Parameters.AddWithValue("@otherLanguage", addResidentGeneralInfo.OtherLanguage);
                l_Cmd.Parameters.AddWithValue("@handDominance", addResidentGeneralInfo.HandDominance);
                l_Cmd.Parameters.AddWithValue("@assessFrequency", addResidentGeneralInfo.AssFrequency);
                l_Cmd.Parameters.AddWithValue("@status", addResidentGeneralInfo.Status);
                l_Cmd.Parameters.AddWithValue("@createdby", addResidentGeneralInfo.ModifiedBy.ID);
                l_Cmd.Parameters.Add("@suiteHandlerId", SqlDbType.Int).Direction = ParameterDirection.Output; // addResidentGeneralInfo.SuiteHandler
                if (addResidentGeneralInfo.ShortName != "")
                {
                    l_Cmd.Parameters.AddWithValue("@shortName", Convert.ToString(addResidentGeneralInfo.ShortName));
                }
                l_Cmd.Parameters.AddWithValue("@DNRStatus", addResidentGeneralInfo.DNRStatus);
                l_Cmd.Parameters.AddWithValue("@FullCodeStatus", addResidentGeneralInfo.FullCodeStatus);
                DateTime aniversary = addResidentGeneralInfo.AnniversaryDate;
                if (aniversary.Day != 1 && aniversary.Month != 1 && aniversary.Year != 1)
                {
                    l_Cmd.Parameters.AddWithValue("@aniversaryDate", addResidentGeneralInfo.AnniversaryDate);
                }
                l_Cmd.Parameters.AddWithValue("@religiousType", addResidentGeneralInfo.ReligionType);
                l_Cmd.Parameters.AddWithValue("@voterType", addResidentGeneralInfo.VoterType);
                l_Cmd.Parameters.AddWithValue("@readType", addResidentGeneralInfo.ReadType);
                l_Cmd.Parameters.AddWithValue("@writeType", addResidentGeneralInfo.WriteType);

                l_Cmd.Parameters.AddWithValue("@veteranType", addResidentGeneralInfo.VeteranType);
                l_Cmd.Parameters.AddWithValue("@eduType", addResidentGeneralInfo.EducationType);
                l_Cmd.Parameters.AddWithValue("@culturalPreferences", addResidentGeneralInfo.CulturalPreferences);
                residentId = l_Cmd.ExecuteNonQuery();
                if (residentId > 0)
                {
                    iResultId[0] = Convert.ToInt32(l_Cmd.Parameters["@id"].Value);
                    iResultId[1] = Convert.ToInt32(l_Cmd.Parameters["@suiteHandlerId"].Value);

                    addResidentGeneralInfo.ID = Convert.ToInt32(l_Cmd.Parameters["@id"].Value);
                    addResidentGeneralInfo.SuiteHandler = Convert.ToInt32(l_Cmd.Parameters["@suiteHandlerId"].Value);
                }
                return iResultId;
            }
            catch (Exception ex)
            {
                exception = "GetResidentGeneralInfoExists |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataTable GetResidentDataExists(int residentId)
        {
            string exception = string.Empty;
            DataTable dtResidents = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", residentId);
                DataSet residentsReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentsReceive);
                if (residentsReceive.Tables[0].Rows.Count > 0)
                {
                    dtResidents = new DataTable();
                    dtResidents = residentsReceive.Tables[0];
                }
                return dtResidents;
            }
            catch (Exception ex)
            {
                exception = "GetResidentDataExists |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static ResidentModel GetTempResidentById(int residentId)
        {
            string exception = string.Empty;
            ResidentModel resident = new ResidentModel();
            UserModel user;
            HomeModel home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_TEMP_RESIDENT_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", residentId);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables.Count > 0)
                {
                    foreach (DataRow residentTypeRow in residentReceive.Tables[0].Rows)
                    {
                        resident = new ResidentModel();
                        home = new HomeModel();
                        resident.ID = Convert.ToInt32(residentTypeRow["fd_id"]);
                        home.Id = Convert.ToInt32(residentTypeRow["fd_home_id"]);
                        home.Name = residentTypeRow["fd_home_name"].ToString();
                        home.Code = residentTypeRow["fd_code"].ToString();
                        resident.Home = home;
                        resident.SuiteIds = Convert.ToString(residentTypeRow["fd_suite_id"]);
                        resident.SuiteNo = Convert.ToString(residentTypeRow["fd_suite_no"]);
                        resident.MoveInDate = Convert.ToDateTime(residentTypeRow["fd_move_in_date"]);
                        if (residentTypeRow["fd_move_out_date"] == System.DBNull.Value)
                        {
                            resident.MoveOutDate = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.MoveOutDate = Convert.ToDateTime(residentTypeRow["fd_move_out_date"]);
                        }
                        resident.Occupancy = Convert.ToInt32(residentTypeRow["fd_occupancy"]);
                        resident.Age = Convert.ToInt32(residentTypeRow["fd_age"]);
                        resident.FirstName = Convert.ToString(residentTypeRow["fd_first_name"]);
                        resident.LastName = Convert.ToString(residentTypeRow["fd_last_name"]);
                        resident.Gendar = Convert.ToChar(residentTypeRow["fd_gender"]);
                        resident.ResidentImage = Convert.ToString(residentTypeRow["fd_image"]);
                        resident.Phone = Convert.ToString(residentTypeRow["fd_phone"]);
                        resident.BirthDate = Convert.ToDateTime(residentTypeRow["fd_birth_date"]);
                        resident.MBhealthNumber = Convert.ToString(residentTypeRow["fd_MB_health_number"]);
                        
                        resident.BirthPlace = Convert.ToString(residentTypeRow["fd_birth_place"]);
                        resident.MaritalStatus = Convert.ToInt32(residentTypeRow["fd_marital_status"]);
                        resident.SignificatOther = Convert.ToString(residentTypeRow["fd_significat_other"]);
                        resident.RelationshipWithFamily = Convert.ToString(residentTypeRow["fd_relationship_family"]);
                        resident.RegisteredVoter = Convert.ToString(residentTypeRow["fd_registered_voter"]);
                        resident.Vetaran = Convert.ToString(residentTypeRow["fd_veteran"]);
                        resident.ReligiousAffiliation = Convert.ToString(residentTypeRow["fd_religious_affiliation"]);
                        resident.PersonalInvolvement = Convert.ToString(residentTypeRow["fd_personal_involvement"]);
                        resident.EducationLevel = Convert.ToString(residentTypeRow["fd_education_level"]);
                        resident.AbilityToRead = Convert.ToString(residentTypeRow["fd_ability_to_read"]);
                        resident.AbilityToWrite = Convert.ToString(residentTypeRow["fd_ability_to_write"]);
                        resident.OtherLanguage = Convert.ToString(residentTypeRow["fd_other_language"]);
                        resident.PastOccupationJobs = Convert.ToString(residentTypeRow["fd_past_occupations_jobs"]);
                        if (residentTypeRow["fd_hand_dominance"].ToString() != null && residentTypeRow["fd_hand_dominance"].ToString() != string.Empty)
                        {
                            resident.HandDominance = Convert.ToInt32(residentTypeRow["fd_hand_dominance"]);
                        }
                        else
                        {
                            resident.MaritalStatus = 0;
                        }
                        resident.ShortName = residentTypeRow["fd_short_name"] != System.DBNull.Value ? Convert.ToString(residentTypeRow["fd_short_name"]) : "";
                        resident.InsuranceCompany = Convert.ToString(residentTypeRow["fd_insurance_company"]);
                        resident.ContractNumber = Convert.ToString(residentTypeRow["fd_contract_number"]);
                        resident.GroupNumber = Convert.ToString(residentTypeRow["fd_group_number"]);

                        resident.POACare = Convert.ToString(residentTypeRow["fd_POA_care"]);
                        resident.POACareType = Convert.ToInt16(residentTypeRow["fd_POA_care_type"]);
                        resident.POACareStatus = Convert.ToChar(residentTypeRow["fd_POA_care_type_status"]);
                        resident.POACareType2Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_2_status"]);
                        resident.POACareType3Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_3_status"]);
                        
                        resident.CareHomePhone = Convert.ToString(residentTypeRow["fd_care_home_phone_no"]);
                        resident.CareWorkPhone = Convert.ToString(residentTypeRow["fd_care_work_phone_no"]);
                        resident.CareCellPhone = Convert.ToString(residentTypeRow["fd_POA_care_cell_no"]);
                        resident.POACareHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_home_type"]);
                        resident.POACareBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_business_type"]);
                        resident.POACareCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_cell_type"]);
                        resident.CareEmail = Convert.ToString(residentTypeRow["fd_care_email"]);
                        resident.POAFinance = Convert.ToString(residentTypeRow["fd_POA_finance"]);
                        resident.POAFinanceType = Convert.ToInt16(residentTypeRow["fd_POA_finance_type"]);
                        resident.POAFinanceStatus = Convert.ToChar(residentTypeRow["fd_POA_finance_type_status"]);
                        resident.POAFinanceType2Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_2_status"]);
                        resident.POAFinanceType3Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_3_status"]);

                        resident.FinanceHomePhone = Convert.ToString(residentTypeRow["fd_finance_home_phone_no"]);
                        resident.FinanceWorkPhone = Convert.ToString(residentTypeRow["fd_finance_work_phone_no"]);
                        resident.FinanceCellPhone = Convert.ToString(residentTypeRow["fd_POA_finance_cell_no"]);
                        resident.POAFinanceHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_home_type"]);
                        resident.POAFinanceBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_business_type"]);
                        resident.POAFinanceCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_cell_type"]);
                        resident.FinanceEmail = Convert.ToString(residentTypeRow["fd_finance_email"]);

                        resident.Contract1 = Convert.ToString(residentTypeRow["fd_contact_1"]);
                        resident.Address1 = Convert.ToString(residentTypeRow["fd_address_1"]);
                        resident.Relationship1 = Convert.ToString(residentTypeRow["fd_relationship_1"]);
                        resident.HomePhone1 = Convert.ToString(residentTypeRow["fd_home_phone_1"]);
                        resident.BusinessPhone1 = Convert.ToString(residentTypeRow["fd_business_phone_1"]);
                        resident.CellPhone1 = Convert.ToString(residentTypeRow["fd_cell_phone_1"]);
                        resident.HomePhoneType1 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_1"]);
                        resident.BusinessPhoneType1 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_1"]);
                        resident.CellPhoneType1 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_1"]);
                        resident.Email1 = Convert.ToString(residentTypeRow["fd_email_1"]);

                        resident.Contract2 = Convert.ToString(residentTypeRow["fd_contact_2"]);
                        resident.Address2 = Convert.ToString(residentTypeRow["fd_address_2"]);
                        resident.Relationship2 = Convert.ToString(residentTypeRow["fd_relationship_2"]);
                        resident.HomePhone2 = Convert.ToString(residentTypeRow["fd_home_phone_2"]);
                        resident.BusinessPhone2 = Convert.ToString(residentTypeRow["fd_business_phone_2"]);
                        resident.CellPhone2 = Convert.ToString(residentTypeRow["fd_cell_phone_2"]);
                        resident.HomePhoneType2 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_2"]);
                        resident.BusinessPhoneType2 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_2"]);
                        resident.CellPhoneType2 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_2"]);
                        resident.Email2 = Convert.ToString(residentTypeRow["fd_email_2"]);

                        resident.Contract3 = Convert.ToString(residentTypeRow["fd_contact_3"]);
                        resident.Address3 = Convert.ToString(residentTypeRow["fd_address_3"]);
                        resident.Relationship3 = Convert.ToString(residentTypeRow["fd_relationship_3"]);
                        resident.HomePhone3 = Convert.ToString(residentTypeRow["fd_home_phone_3"]);
                        resident.BusinessPhone3 = Convert.ToString(residentTypeRow["fd_business_phone_3"]);
                        resident.CellPhone3 = Convert.ToString(residentTypeRow["fd_cell_phone_3"]);
                        resident.HomePhoneType3 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_3"]);
                        resident.BusinessPhoneType3 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_3"]);
                        resident.CellPhoneType3 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_3"]);
                        resident.Email3 = Convert.ToString(residentTypeRow["fd_email_3"]);

                        resident.Physician = Convert.ToString(residentTypeRow["fd_physician"]);
                        resident.PhysicianPhone = Convert.ToString(residentTypeRow["fd_physician_phone"]);
                        resident.Alergies = Convert.ToString(residentTypeRow["fd_alergies"]);
                        resident.HealthHistory = Convert.ToString(residentTypeRow["fd_health_history"]);
                        resident.AssFrequency = Convert.ToChar(residentTypeRow["fd_assess_frequency"]);
                        resident.QolaResident = (QolaResident)Convert.ToChar(residentTypeRow["fd_qola_resident"]);
                        resident.AllergiesNames = Convert.ToString(residentTypeRow["fd_allergies_name"]);
                        if (residentTypeRow["fd_status"].ToString() == "A")
                        {
                            resident.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            resident.Status = AvailabilityStatus.I;
                        }
                        user = new UserModel();
                        user.ID = Convert.ToInt32(residentTypeRow["fd_modified_by"]);
                        resident.ModifiedBy = user;

                        if (residentTypeRow["fd_admitted_from"] == System.DBNull.Value)
                        {
                            resident.AdmittedFrom = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.AdmittedFrom = Convert.ToDateTime(residentTypeRow["fd_admitted_from"]);
                        }
                        resident.CareAddress = Convert.ToString(residentTypeRow["fd_care_address"]);
                        resident.CareRelationship = Convert.ToString(residentTypeRow["fd_care_relationship"]);
                        resident.CareType = Convert.ToInt16(residentTypeRow["fd_care_type"]);
                        resident.FinanceAddress = Convert.ToString(residentTypeRow["fd_finance_address"]);
                        resident.FinanceRelationship = Convert.ToString(residentTypeRow["fd_finance_relationship"]);
                        resident.DNR = Convert.ToString(residentTypeRow["fd_DNR"]);
                        resident.FullCode = Convert.ToString(residentTypeRow["fd_full_code"]);
                        resident.FuneralArguments = Convert.ToString(residentTypeRow["fd_funeral_argument"]);
                        resident.PharmaceSelf = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        resident.PharmaceNursing = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.PharmaceFaxNumber = Convert.ToString(residentTypeRow["fd_pharmacy_fax_no"]);
                        resident.PharmacePhoneNo = Convert.ToString(residentTypeRow["fd_pharmacy_phone_no"]);
                        resident.ReligionContact = Convert.ToString(residentTypeRow["fd_religion_contact"]);
                        resident.ReligionHomePhone = Convert.ToString(residentTypeRow["fd_religion_home_phone"]);
                        resident.ReligionOffice = Convert.ToString(residentTypeRow["fd_religon_office"]);

                        resident.SuiteHandler = Convert.ToInt32(residentTypeRow["fd_suite_handler_id"]);
                        resident.CallHospital = Convert.ToChar(residentTypeRow["fd_call_hospital"]);
                        resident.PhysicianFaxNo = Convert.ToString(residentTypeRow["fd_physician_fax_no"]);
                        resident.DNRStatus = Convert.ToChar(residentTypeRow["fd_DNR_status"]);
                        resident.FullCodeStatus = Convert.ToChar(residentTypeRow["fd_fullcode_status"]);
                        resident.BedPosition = Convert.ToChar(residentTypeRow["AS_bed"]);
                        resident.AnniversaryDate = Convert.ToDateTime(residentTypeRow["fd_aniversary_date"]);
                        resident.ReligionType = Convert.ToInt16(residentTypeRow["fd_religious_affiliation_type"]);
                        resident.VoterType = Convert.ToString(residentTypeRow["fd_voter_type"]);
                        resident.ReadType = Convert.ToInt16(residentTypeRow["fd_read_type"]);
                        resident.WriteType = Convert.ToInt16(residentTypeRow["fd_write_type"]);

                        resident.EducationType = Convert.ToInt16(residentTypeRow["fd_education_type"]);
                        resident.VeteranType = Convert.ToString(residentTypeRow["fd_veteran_type"]);
                        string self = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        string nurse = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.CurrentDiagnoses = Convert.ToString(residentTypeRow["fd_current_diagnoses"]);
                        resident.CulturalPreferences = Convert.ToString(residentTypeRow["fd_cultural_preferences"]);

                        resident.PharmacyName = "";
                        if (self.Length > 0 || nurse.Length > 0)
                        {
                            if (self.Length > 0)
                            {
                                resident.PharmacyName = self;
                            }
                            if (nurse.Length > 0)
                            {
                                resident.PharmacyName = nurse;
                            }
                        }

                    }
                }
                return resident;
            }
            catch (Exception ex)
            {
                exception = "GetActiveResidentById |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static ResidentModel GetResidentByIdWithoutResidentStatus(int residentId)
        {
            string exception = string.Empty;
            ResidentModel resident = new ResidentModel();
            UserModel user;
            HomeModel home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_BY_ID_WITHOUT_RESIDENT_STATUS, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", residentId);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables.Count > 0)
                {
                    foreach (DataRow residentTypeRow in residentReceive.Tables[0].Rows)
                    {
                        resident = new ResidentModel();
                        home = new HomeModel();
                        resident.ID = Convert.ToInt32(residentTypeRow["fd_id"]);
                        home.Id = Convert.ToInt32(residentTypeRow["fd_home_id"]);
                        home.Name = residentTypeRow["fd_home_name"].ToString();
                        home.Code = residentTypeRow["fd_code"].ToString();
                        resident.Home = home;
                        resident.SuiteIds = Convert.ToString(residentTypeRow["fd_suite_id"]);
                        resident.SuiteNo = Convert.ToString(residentTypeRow["fd_suite_no"]);
                        resident.MoveInDate = Convert.ToDateTime(residentTypeRow["fd_move_in_date"]);
                        if (residentTypeRow["fd_move_out_date"] == System.DBNull.Value)
                        {
                            resident.MoveOutDate = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.MoveOutDate = Convert.ToDateTime(residentTypeRow["fd_move_out_date"]);
                        }
                        resident.Occupancy = Convert.ToInt32(residentTypeRow["fd_occupancy"]);
                        resident.Age = Convert.ToInt32(residentTypeRow["fd_age"]);
                        resident.FirstName = Convert.ToString(residentTypeRow["fd_first_name"]);
                        resident.LastName = Convert.ToString(residentTypeRow["fd_last_name"]);
                        resident.Gendar = Convert.ToChar(residentTypeRow["fd_gender"]);
                        resident.ResidentImage = Convert.ToString(residentTypeRow["fd_image"]);
                        resident.Phone = Convert.ToString(residentTypeRow["fd_phone"]);
                        resident.BirthDate = Convert.ToDateTime(residentTypeRow["fd_birth_date"]);
                        resident.MBhealthNumber = Convert.ToString(residentTypeRow["fd_MB_health_number"]);


                        resident.BirthPlace = Convert.ToString(residentTypeRow["fd_birth_place"]);
                        resident.MaritalStatus = Convert.ToInt32(residentTypeRow["fd_marital_status"]);
                        resident.SignificatOther = Convert.ToString(residentTypeRow["fd_significat_other"]);
                        resident.RelationshipWithFamily = Convert.ToString(residentTypeRow["fd_relationship_family"]);
                        resident.RegisteredVoter = Convert.ToString(residentTypeRow["fd_registered_voter"]);
                        resident.Vetaran = Convert.ToString(residentTypeRow["fd_veteran"]);
                        resident.ReligiousAffiliation = Convert.ToString(residentTypeRow["fd_religious_affiliation"]);
                        resident.PersonalInvolvement = Convert.ToString(residentTypeRow["fd_personal_involvement"]);
                        resident.EducationLevel = Convert.ToString(residentTypeRow["fd_education_level"]);
                        resident.AbilityToRead = Convert.ToString(residentTypeRow["fd_ability_to_read"]);
                        resident.AbilityToWrite = Convert.ToString(residentTypeRow["fd_ability_to_write"]);
                        resident.OtherLanguage = Convert.ToString(residentTypeRow["fd_other_language"]);
                        resident.PastOccupationJobs = Convert.ToString(residentTypeRow["fd_past_occupations_jobs"]);
                        if (residentTypeRow["fd_hand_dominance"].ToString() != null && residentTypeRow["fd_hand_dominance"].ToString() != string.Empty)
                        {
                            resident.HandDominance = Convert.ToInt32(residentTypeRow["fd_hand_dominance"]);
                        }
                        else
                        {
                            resident.MaritalStatus = 0;
                        }
                        resident.ShortName = Convert.ToString(residentTypeRow["fd_short_name"] != System.DBNull.Value ? residentTypeRow["fd_short_name"] : "");
                        resident.InsuranceCompany = Convert.ToString(residentTypeRow["fd_insurance_company"]);
                        resident.ContractNumber = Convert.ToString(residentTypeRow["fd_contract_number"]);
                        resident.GroupNumber = Convert.ToString(residentTypeRow["fd_group_number"]);

                        resident.POACare = Convert.ToString(residentTypeRow["fd_POA_care"]);
                        resident.POACareType = Convert.ToInt16(residentTypeRow["fd_POA_care_type"]);

                        resident.POACareStatus = Convert.ToChar(residentTypeRow["fd_POA_care_type_status"]);
                        resident.POACareType2Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_2_status"]);
                        resident.POACareType3Status = Convert.ToChar(residentTypeRow["fd_POA_care_type_3_status"]);
                        resident.CareHomePhone = Convert.ToString(residentTypeRow["fd_care_home_phone_no"]);
                        resident.CareWorkPhone = Convert.ToString(residentTypeRow["fd_care_work_phone_no"]);
                        resident.CareCellPhone = Convert.ToString(residentTypeRow["fd_POA_care_cell_no"]);
                        resident.CareEmail = Convert.ToString(residentTypeRow["fd_care_email"]);
                        resident.POACareHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_home_type"]);
                        resident.POACareBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_business_type"]);
                        resident.POACareCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_care_cell_type"]);
                        resident.POAFinance = Convert.ToString(residentTypeRow["fd_POA_finance"]);
                        resident.POAFinanceType = Convert.ToInt16(residentTypeRow["fd_POA_finance_type"]);
                        resident.POAFinanceStatus = Convert.ToChar(residentTypeRow["fd_POA_finance_type_status"]);

                        resident.POAFinanceType2Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_2_status"]);
                        resident.POAFinanceType3Status = Convert.ToChar(residentTypeRow["fd_POA_finance_type_3_status"]);

                        resident.FinanceHomePhone = Convert.ToString(residentTypeRow["fd_finance_home_phone_no"]);
                        resident.FinanceWorkPhone = Convert.ToString(residentTypeRow["fd_finance_work_phone_no"]);
                        resident.FinanceCellPhone = Convert.ToString(residentTypeRow["fd_POA_finance_cell_no"]);
                        resident.POAFinanceHomePhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_home_type"]);
                        resident.POAFinanceBusinessPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_business_type"]);
                        resident.POAFinanceCellPhoneType = Convert.ToInt16(residentTypeRow["fd_POA_finance_cell_type"]);
                        resident.FinanceEmail = Convert.ToString(residentTypeRow["fd_finance_email"]);

                        resident.Contract1 = Convert.ToString(residentTypeRow["fd_contact_1"]);
                        resident.Address1 = Convert.ToString(residentTypeRow["fd_address_1"]);
                        resident.Relationship1 = Convert.ToString(residentTypeRow["fd_relationship_1"]);
                        resident.HomePhone1 = Convert.ToString(residentTypeRow["fd_home_phone_1"]);
                        resident.BusinessPhone1 = Convert.ToString(residentTypeRow["fd_business_phone_1"]);
                        resident.CellPhone1 = Convert.ToString(residentTypeRow["fd_cell_phone_1"]);
                        resident.HomePhoneType1 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_1"]);
                        resident.BusinessPhoneType1 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_1"]);
                        resident.CellPhoneType1 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_1"]);
                        resident.Email1 = Convert.ToString(residentTypeRow["fd_email_1"]);

                        resident.Contract2 = Convert.ToString(residentTypeRow["fd_contact_2"]);
                        resident.Address2 = Convert.ToString(residentTypeRow["fd_address_2"]);
                        resident.Relationship2 = Convert.ToString(residentTypeRow["fd_relationship_2"]);
                        resident.HomePhone2 = Convert.ToString(residentTypeRow["fd_home_phone_2"]);
                        resident.BusinessPhone2 = Convert.ToString(residentTypeRow["fd_business_phone_2"]);
                        resident.CellPhone2 = Convert.ToString(residentTypeRow["fd_cell_phone_2"]);
                        resident.HomePhoneType2 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_2"]);
                        resident.BusinessPhoneType2 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_2"]);
                        resident.CellPhoneType2 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_2"]);
                        resident.Email2 = Convert.ToString(residentTypeRow["fd_email_2"]);

                        resident.Contract3 = Convert.ToString(residentTypeRow["fd_contact_3"]);
                        resident.Address3 = Convert.ToString(residentTypeRow["fd_address_3"]);
                        resident.Relationship3 = Convert.ToString(residentTypeRow["fd_relationship_3"]);
                        resident.HomePhone3 = Convert.ToString(residentTypeRow["fd_home_phone_3"]);
                        resident.BusinessPhone3 = Convert.ToString(residentTypeRow["fd_business_phone_3"]);
                        resident.CellPhone3 = Convert.ToString(residentTypeRow["fd_cell_phone_3"]);
                        resident.HomePhoneType3 = Convert.ToInt16(residentTypeRow["fd_home_phone_type_3"]);
                        resident.BusinessPhoneType3 = Convert.ToInt16(residentTypeRow["fd_business_phone_type_3"]);
                        resident.CellPhoneType3 = Convert.ToInt16(residentTypeRow["fd_cell_phone_type_3"]);
                        resident.Email3 = Convert.ToString(residentTypeRow["fd_email_3"]);

                        resident.Physician = Convert.ToString(residentTypeRow["fd_physician"]);
                        resident.PhysicianPhone = Convert.ToString(residentTypeRow["fd_physician_phone"]);
                        resident.PhysicianFaxNo = Convert.ToString(residentTypeRow["fd_physician_fax_no"]);
                        resident.Alergies = Convert.ToString(residentTypeRow["fd_alergies"]);
                        resident.HealthHistory = Convert.ToString(residentTypeRow["fd_health_history"]);
                        resident.AssFrequency = Convert.ToChar(residentTypeRow["fd_assess_frequency"]);
                        resident.QolaResident = (QolaResident)Convert.ToChar(residentTypeRow["fd_qola_resident"]);
                        resident.AllergiesNames = Convert.ToString(residentTypeRow["fd_allergies_name"]);
                        if (residentTypeRow["fd_status"].ToString() == "A")
                        {
                            resident.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            resident.Status = AvailabilityStatus.I;
                        }
                        user = new UserModel();
                        user.ID = Convert.ToInt32(residentTypeRow["fd_modified_by"]);
                        user.FirstName = Convert.ToString(residentTypeRow["fd_full_code"]);
                        user.LastName = Convert.ToString(residentTypeRow["fd_user_fname"]);
                        user.UserTypeName = Convert.ToString(residentTypeRow["fd_user_type"]);
                        resident.ModifiedBy = user;
                        resident.ModifiedOn = Convert.ToDateTime(residentTypeRow["fd_modified_on"]);
                        if (residentTypeRow["fd_admitted_from"] == System.DBNull.Value)
                        {
                            resident.AdmittedFrom = Convert.ToDateTime("01/01/0001");
                        }
                        else
                        {
                            resident.AdmittedFrom = Convert.ToDateTime(residentTypeRow["fd_admitted_from"]);
                        }
                        resident.CareAddress = Convert.ToString(residentTypeRow["fd_care_address"]);
                        resident.CareRelationship = Convert.ToString(residentTypeRow["fd_care_relationship"]);
                        resident.CareType = Convert.ToInt16(residentTypeRow["fd_care_type"]);
                        resident.FinanceAddress = Convert.ToString(residentTypeRow["fd_finance_address"]);
                        resident.FinanceRelationship = Convert.ToString(residentTypeRow["fd_finance_relationship"]);
                        resident.DNR = Convert.ToString(residentTypeRow["fd_DNR"]);
                        resident.FullCode = Convert.ToString(residentTypeRow["fd_full_code"]);
                        resident.FuneralArguments = Convert.ToString(residentTypeRow["fd_funeral_argument"]);
                        resident.PharmaceSelf = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        resident.PharmaceNursing = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.PharmaceFaxNumber = Convert.ToString(residentTypeRow["fd_pharmacy_fax_no"]);
                        resident.PharmacePhoneNo = Convert.ToString(residentTypeRow["fd_pharmacy_phone_no"]);
                        resident.ReligionContact = Convert.ToString(residentTypeRow["fd_religion_contact"]);
                        resident.ReligionHomePhone = Convert.ToString(residentTypeRow["fd_religion_home_phone"]);
                        resident.ReligionOffice = Convert.ToString(residentTypeRow["fd_religon_office"]);
                        resident.ReligiousAffiliation = Convert.ToString(residentTypeRow["fd_religious_affiliation"]);
                        resident.SuiteHandler = Convert.ToInt32(residentTypeRow["fd_suite_handler_id"]);
                        resident.CallHospital = Convert.ToChar(residentTypeRow["fd_call_hospital"]);
                        resident.FullCodeStatus = Convert.ToChar(residentTypeRow["fd_fullcode_status"]);
                        resident.DNRStatus = Convert.ToChar(residentTypeRow["fd_DNR_status"]);
                        string self = Convert.ToString(residentTypeRow["fd_pharmacy_self"]);
                        string nurse = Convert.ToString(residentTypeRow["fd_pharmacy_nursing"]);
                        resident.ReligionType = Convert.ToInt16(residentTypeRow["fd_religious_affiliation_type"]);
                        resident.ReadType = Convert.ToInt16(residentTypeRow["fd_read_type"]);
                        resident.WriteType = Convert.ToInt16(residentTypeRow["fd_write_type"]);
                        resident.VoterType = Convert.ToString(residentTypeRow["fd_voter_type"]);
                        resident.VeteranType = Convert.ToString(residentTypeRow["fd_veteran_type"]);
                        resident.EducationType = Convert.ToInt16(residentTypeRow["fd_education_type"]);
                        resident.CurrentDiagnoses = Convert.ToString(residentTypeRow["fd_current_diagnoses"]);
                        resident.CulturalPreferences = Convert.ToString(residentTypeRow["fd_cultural_preferences"]);
                        resident.Guid = Convert.ToString(residentTypeRow["fd_GUID"]);
                        resident.PharmacyName = "";
                        if (self.Length > 0 || nurse.Length > 0)
                        {
                            if (self.Length > 0)
                            {
                                resident.PharmacyName = self;
                            }
                            if (nurse.Length > 0)
                            {
                                resident.PharmacyName = nurse;
                            }
                        }
                    }
                }
                return resident;
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

        public static DataTable Get_Profile_Recognition_Resident(ResidentModel addResidentGeneralInfo)
        {
            string exception = string.Empty;
            DataTable dtResidents = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_PROFILE_RECOGNITION_RESIDENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@firstname", addResidentGeneralInfo.FirstName);
                l_Cmd.Parameters.AddWithValue("@lastname", addResidentGeneralInfo.LastName);
                l_Cmd.Parameters.AddWithValue("@gender", addResidentGeneralInfo.Gendar);
                l_Cmd.Parameters.AddWithValue("@birthDate", addResidentGeneralInfo.BirthDate);
                l_Cmd.Parameters.AddWithValue("@maritalStatus ", addResidentGeneralInfo.MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@MBHealthNo", addResidentGeneralInfo.MBhealthNumber);
                DataSet residentReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(residentReceive);
                if ((residentReceive != null) & residentReceive.Tables.Count > 0)
                {
                    dtResidents = new DataTable();
                    dtResidents = residentReceive.Tables[0];
                }
                return dtResidents;
            }
            catch (Exception ex)
            {
                exception = "GetGet_Profile_Recognition_Resident |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<int> GetAvailableSuitesNumber(int homeId, DateTime dateinput, int occupancynumber)
        {
            string exception = string.Empty;
            Collection<int> residents = new Collection<int>();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);

            SqlDataAdapter l_DA = new SqlDataAdapter();
            SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_AVAILABLE_SUITE_BY_HOME_ID, l_Conn);
            l_Conn.Open();
            l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            l_Cmd.Parameters.AddWithValue("@homeID", homeId);
            l_Cmd.Parameters.AddWithValue("@occupancy", occupancynumber);
            l_Cmd.Parameters.AddWithValue("@moveInDate", dateinput);
            DataTable residentReceive = new DataTable();
            l_DA.SelectCommand = l_Cmd;
            l_DA.Fill(residentReceive);
            for (int i = 0; i <= residentReceive.Rows.Count - 1; i++)
            {
                var cell = residentReceive.Rows[i][0];
                if (int.TryParse(cell.ToString(), out int n) == true)
                    residents.Add(int.Parse(cell.ToString()));
                
            }
            return residents;
            
        }


        public static void SetUp_ResidentModel_ListItems(ResidentModel AAA)
        {
            AAA.MaritalStatusList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "1", Text = "Married" },
                new SelectListItem { Value = "2", Text = "Widowed" },
                new SelectListItem { Value = "3", Text = "Single" },
                new SelectListItem { Value = "4", Text = "Divorced" },
            };
            AAA.OccupancyList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "1", Text = "Single" },
                new SelectListItem { Value = "2", Text = "Double" },
                new SelectListItem { Value = "3", Text = "Triple" },
            };
            AAA.SuiteNoList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
            };
            AAA.ReligiousAffiliationList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "Prefer not to say", Text = "Prefer not to say" },
                new SelectListItem { Value = "Christian", Text = "Christian" },
                new SelectListItem { Value = "Jewish", Text = "Jewish" },
                new SelectListItem { Value = "Islamic", Text = "Islamic" },
                new SelectListItem { Value = "Hindu", Text = "Hindu" },
                new SelectListItem { Value = "Buddhist", Text = "Buddhist" },
                new SelectListItem { Value = "Other", Text = "Other" },
            };
            AAA.AbilityToReadList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "Very Well", Text = "Very Well" },
                new SelectListItem { Value = "Good", Text = "Good" },
                new SelectListItem { Value = "Poor", Text = "Poor" },
                new SelectListItem { Value = "With Cueing", Text = "With Cueing" },
                new SelectListItem { Value = "No", Text = "No" },
            };
            AAA.Number_of_childrenList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
            };
            AAA.Number_of_grandchildrenList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
            };
            AAA.PhoneTypeList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "1", Text = "Home" },
                new SelectListItem { Value = "2", Text = "Work" },
                new SelectListItem { Value = "3", Text = "Mobile" },
                new SelectListItem { Value = "4", Text = "Other" },
            };
            AAA.HandDominanceList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "1", Text = "Left" },
                new SelectListItem { Value = "2", Text = "Right" },
            };
            AAA.AbilityToWriteList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "Very Well", Text = "Very Well" },
                new SelectListItem { Value = "Good", Text = "Good" },
                new SelectListItem { Value = "Poor", Text = "Poor" },
                new SelectListItem { Value = "With Cueing", Text = "With Cueing" },
                new SelectListItem { Value = "No", Text = "No" },
            };
            AAA.EducationLevelList = new[]{
                new SelectListItem { Value = "", Text = "-- Select --" },
                new SelectListItem { Value = "Prefer not to say", Text = "Prefer not to say" },
                new SelectListItem { Value = "High School Diploma", Text = "High School Diploma" },
                new SelectListItem { Value = "Some School Diploma", Text = "Some School Diploma" },
                new SelectListItem { Value = "Vocational/Technical Training", Text = "Vocational/Technical Training" },
                new SelectListItem { Value = "Some College/ University", Text = "Some College/ University" },
                new SelectListItem { Value = "College/ University Graduate", Text = "College/ University Graduate" },
                new SelectListItem { Value = "Other", Text = "Other" },
            };
        }

        public static void UpdateResidentGeneralInfo1(ResidentModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", p_Model.Home.Id);
                l_Cmd.Parameters.AddWithValue("@suiteId", p_Model.SuiteIds);
                l_Cmd.Parameters.AddWithValue("@Occupancy ", p_Model.Occupancy);
                l_Cmd.Parameters.AddWithValue("@birthPlace", p_Model.BirthPlace);
                l_Cmd.Parameters.AddWithValue("@maritalStatus ", p_Model.MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@MBHealthNo", p_Model.MBhealthNumber);
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentGeneralInfo1 |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void UpdateResidentGeneralInfo2(ResidentModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", p_Model.Home.Id);
                l_Cmd.Parameters.AddWithValue("@suiteId", p_Model.SuiteIds);
                l_Cmd.Parameters.AddWithValue("@Occupancy ", p_Model.Occupancy);
                l_Cmd.Parameters.AddWithValue("@birthPlace", p_Model.BirthPlace);
                l_Cmd.Parameters.AddWithValue("@maritalStatus ", p_Model.MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@MBHealthNo", p_Model.MBhealthNumber);
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentGeneralInfo1 |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void UpdateResidentGeneralInfo3(ResidentModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", p_Model.Home.Id);
                l_Cmd.Parameters.AddWithValue("@suiteId", p_Model.SuiteIds);
                l_Cmd.Parameters.AddWithValue("@Occupancy ", p_Model.Occupancy);
                l_Cmd.Parameters.AddWithValue("@birthPlace", p_Model.BirthPlace);
                l_Cmd.Parameters.AddWithValue("@maritalStatus ", p_Model.MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@MBHealthNo", p_Model.MBhealthNumber);
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentGeneralInfo1 |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }






    }

}