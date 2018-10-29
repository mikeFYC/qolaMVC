 using System;
using Dapper;  
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QolaMVC.Models;
using static QolaMVC.Constants.EnumerationTypes;


namespace QolaMVC.DAL
{
    public class Postfall_Clinial_Monitoring_PartDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public Postfall_Clinial_Monitoring_PartDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public static List<NEW_Postfall_Clinial_Monitoring_PartModel> GetAllPostfall_clinial_monitoring_part()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_Postfall_Clinial_Monitoring_PartModel> l_Collection = new List<NEW_Postfall_Clinial_Monitoring_PartModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_new_tbl_postfall_clinial_monitoring_part", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_Postfall_Clinial_Monitoring_PartModel l_Model = new NEW_Postfall_Clinial_Monitoring_PartModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Pf_Clinical_Monitoring_Part = Convert.ToString(l_Reader["Pf_Clinical_Monitoring_Part"]);
                    l_Model.Table_id = Convert.ToString(l_Reader["Table_id"]);
                    l_Model.Created_at = Convert.ToDateTime(l_Reader["Created_at"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllPostfall_clinial_monitoring_part\n" + ex.Message);
            }
        }

        public static string AddNewPostfall_clinial_monitoring_partA1(string storedP, string part, int linkid, DataTable dt1, DataTable dt2, DataTable dt3)
        {
            string exception = string.Empty;

            SqlConnection cnn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
           //sp_add_new_tbl_postfall_clinial_monitoring_details_a1
           SqlCommand cmd = new SqlCommand(storedP, cnn);
           cnn.Open();
           cmd.CommandType = System.Data.CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@part", part);
           cmd.Parameters.AddWithValue("@linkid", linkid);
           cmd.Parameters.Add("@new_tbl_postfall_clinial_monitoring_details_a1_Type", SqlDbType.Structured).SqlValue = dt1;
           cmd.Parameters.Add("@new_tbl_postfall_clinial_monitoring_details_a2_Type", SqlDbType.Structured).SqlValue = dt2;
           cmd.Parameters.Add("@new_tbl_postfall_clinial_monitoring_details_a3_Type", SqlDbType.Structured).SqlValue = dt3;
           
            SqlParameter returnVal=cmd.Parameters.Add("tempid", SqlDbType.Int);
            returnVal.Direction = ParameterDirection.ReturnValue;
           cmd.ExecuteNonQuery();
            return Convert.ToString(returnVal.Value);

            }
            catch (Exception ex)
            {
                exception = "AddNewPostfall_clinial_monitoring_partA1 |" + ex.ToString();
                //Log.Write(exception);
                throw;
                return exception;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static string EditNewPostfall_clinial_monitoring_partA1(string storedP, string tempid, DataTable dt1, DataTable dt2, DataTable dt3)
        {
            string exception = string.Empty;

            SqlConnection cnn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
           SqlCommand cmd = new SqlCommand(storedP, cnn);
           cnn.Open();
           cmd.CommandType = System.Data.CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@tempid", tempid);
           cmd.Parameters.Add("@a1_Type", SqlDbType.Structured).SqlValue = dt1;
           cmd.Parameters.Add("@a2_Type", SqlDbType.Structured).SqlValue = dt2;
           cmd.Parameters.Add("@a3_Type", SqlDbType.Structured).SqlValue = dt3;
           
            SqlParameter returnVal=cmd.Parameters.Add("tempid", SqlDbType.Int);
            returnVal.Direction = ParameterDirection.ReturnValue;
           cmd.ExecuteNonQuery();
            return Convert.ToString(returnVal.Value);

            }
            catch (Exception ex)
            {
                exception = "EditNewPostfall_clinial_monitoring_partA1 |" + ex.ToString();
                //Log.Write(exception);
                throw;
                return exception;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static string AddNewPostfall_clinial_monitoring_partB1(string storedP, string part, int linkid, DataTable dt1)
        {
            string exception = string.Empty;

            SqlConnection cnn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
           SqlCommand cmd = new SqlCommand(storedP, cnn);
           cnn.Open();
           cmd.CommandType = System.Data.CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@part", part);
           cmd.Parameters.AddWithValue("@linkid", linkid);
           cmd.Parameters.Add("@new_tbl_postfall_clinial_monitoring_details_a1_Type", SqlDbType.Structured).SqlValue = dt1;
           
            SqlParameter returnVal=cmd.Parameters.Add("tempid", SqlDbType.Int);
            returnVal.Direction = ParameterDirection.ReturnValue;
           cmd.ExecuteNonQuery();
            return Convert.ToString(returnVal.Value);

            }
            catch (Exception ex)
            {
                exception = "AddNewPostfall_clinial_monitoring_partA1 |" + ex.ToString();
                //Log.Write(exception);
                throw;
                return exception;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static string EditNewPostfall_clinial_monitoring_partB1(string storedP, string tempid, DataTable dt1)
        {
            string exception = string.Empty;

            SqlConnection cnn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
           SqlCommand cmd = new SqlCommand(storedP, cnn);
           cnn.Open();
           cmd.CommandType = System.Data.CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@tempid", tempid);
           cmd.Parameters.Add("@a1_Type", SqlDbType.Structured).SqlValue = dt1;
           
            SqlParameter returnVal=cmd.Parameters.Add("tempid", SqlDbType.Int);
            returnVal.Direction = ParameterDirection.ReturnValue;
           cmd.ExecuteNonQuery();
            return Convert.ToString(returnVal.Value);

            }
            catch (Exception ex)
            {
                exception = "EditNewPostfall_clinial_monitoring_partB1 |" + ex.ToString();
                //Log.Write(exception);
                throw;
                return exception;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static void AddPartAPage2(MasterDetails p_Model, string c_c,string edema_hands1,string edema_feet1, int linkid)
         {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_add_new_tbl_postfall_partAPage2", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@linkid", linkid);
                l_Cmd.Parameters.AddWithValue("@c_longsound_normal", p_Model.c_longsound_normal);
                l_Cmd.Parameters.AddWithValue("@c_longsound_describe", p_Model.c_longsound_describe);
                l_Cmd.Parameters.AddWithValue("@c_longsound_equal", p_Model.c_longsound_equal);
                l_Cmd.Parameters.AddWithValue("@c_c", c_c);
                l_Cmd.Parameters.AddWithValue("@c_c_other", p_Model.c_c_other);
                l_Cmd.Parameters.AddWithValue("@c_heartsound", p_Model.c_heartsound);
                l_Cmd.Parameters.AddWithValue("@c_heartsound_describe", p_Model.c_heartsound_describe);
                l_Cmd.Parameters.AddWithValue("@a_soft", p_Model.a_soft);
                l_Cmd.Parameters.AddWithValue("@a_soft_describe", p_Model.a_soft_describe);
                l_Cmd.Parameters.AddWithValue("@a_pful", p_Model.a_pful);
                l_Cmd.Parameters.AddWithValue("@a_pful_describe", p_Model.a_pful_describe);
                l_Cmd.Parameters.AddWithValue("@a_bowelsound", p_Model.a_bowelsound);
                l_Cmd.Parameters.AddWithValue("@a_lastbowel_date", p_Model.a_lastbowel_date);
                l_Cmd.Parameters.AddWithValue("@a_voidingnormal", p_Model.a_voidingnormal);
                l_Cmd.Parameters.AddWithValue("@a_voidingnormal_describe", p_Model.a_voidingnormal_describe);
                l_Cmd.Parameters.AddWithValue("@a_voidingnormal1", p_Model.a_voidingnormal1);
                l_Cmd.Parameters.AddWithValue("@a_voidingnormal_pads", p_Model.a_voidingnormal_pads);
                l_Cmd.Parameters.AddWithValue("@a_voidingnormal2", p_Model.a_voidingnormal2);
                l_Cmd.Parameters.AddWithValue("@edema_feet_normal", p_Model.edema_feet_normal);
                l_Cmd.Parameters.AddWithValue("@edema_feet_describe", p_Model.edema_feet_describe);
                l_Cmd.Parameters.AddWithValue("@edema_feet1", edema_feet1);
                l_Cmd.Parameters.AddWithValue("@edema_hands_normal", p_Model.edema_hands_normal);
                l_Cmd.Parameters.AddWithValue("@edema_hands_describe", p_Model.edema_hands_describe);
                l_Cmd.Parameters.AddWithValue("@edema_hands1", edema_hands1);
                l_Cmd.Parameters.AddWithValue("@edema_other", p_Model.edema_other);
                l_Cmd.Parameters.AddWithValue("@edema_other_describe", p_Model.edema_other_describe);
                l_Cmd.Parameters.AddWithValue("@skin_feet", p_Model.skin_feet);
                l_Cmd.Parameters.AddWithValue("@skin_feet_describe", p_Model.skin_feet_describe);
                l_Cmd.Parameters.AddWithValue("@skin_rashes", p_Model.skin_rashes);
                l_Cmd.Parameters.AddWithValue("@skin_redness", p_Model.skin_redness);
                l_Cmd.Parameters.AddWithValue("@skin_bruising", p_Model.skin_bruising);
                l_Cmd.Parameters.AddWithValue("@skin_openareas", p_Model.skin_openareas);
                l_Cmd.Parameters.AddWithValue("@skin_desc_abnormal", p_Model.skin_desc_abnormal);

                l_Cmd.Parameters.AddWithValue("@skin_wounddressing", p_Model.skin_wounddressing);
                l_Cmd.Parameters.AddWithValue("@skin_desc", p_Model.skin_desc);
                l_Cmd.Parameters.AddWithValue("@p_residentp", p_Model.p_residentp);
                l_Cmd.Parameters.AddWithValue("@p_residentp_desc", p_Model.p_residentp_desc);
                l_Cmd.Parameters.AddWithValue("@p_pscale", p_Model.p_pscale);
                l_Cmd.Parameters.AddWithValue("@p_aching", p_Model.p_aching);
                l_Cmd.Parameters.AddWithValue("@p_sharp", p_Model.p_sharp);
                l_Cmd.Parameters.AddWithValue("@p_dull", p_Model.p_dull);
                l_Cmd.Parameters.AddWithValue("@p_radiating", p_Model.p_radiating);
                l_Cmd.Parameters.AddWithValue("@p_where", p_Model.p_where);
                l_Cmd.Parameters.AddWithValue("@p_whatmakes_better", p_Model.p_whatmakes_better);
                l_Cmd.Parameters.AddWithValue("@p_whatmakes_worst", p_Model.p_whatmakes_worst);
                l_Cmd.Parameters.AddWithValue("@p_interface_adl", p_Model.p_interface_adl);
                l_Cmd.Parameters.AddWithValue("@p_describe", p_Model.p_describe);
                l_Cmd.Parameters.AddWithValue("@p_other", p_Model.p_other);
                l_Cmd.Parameters.AddWithValue("@completed_by", p_Model.completed_by);
                l_Cmd.ExecuteNonQuery();

            } 
            catch (Exception ex)
            {
                exception = "AddAllergy |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
     public  IEnumerable<MasterDetails> GetPostfall_clinial_monitoring_details_a1_by_id(int linkid, string pf_clinical_monitoring_part)
        {
             SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                MasterDetails ObjMaster  = new MasterDetails();
                l_Conn.Open();
                    
                DynamicParameters param=new DynamicParameters();
                param.Add("@pf_clinical_monitoring_part", pf_clinical_monitoring_part);   
                param.Add("@linkid", linkid);  

                var objDetails = SqlMapper.QueryMultiple(l_Conn, "sp_get_by_id_new_tbl_postfall_clinial_monitoring_details_a1",param,commandType: CommandType.StoredProcedure);
         
                ObjMaster.A1Model = objDetails.Read<NEW_Postfall_Clinial_Monitoring_Part_Details_A1Model>().ToList();  
                ObjMaster.SplitMonitoring = objDetails.Read<NEW_Postfall_Clinial_Monitoring_SplitModel>().ToList();  
            
                List<MasterDetails> CustomerObj = new List<MasterDetails>();  
                CustomerObj.Add(ObjMaster);  
                l_Conn.Close();  
              
                return CustomerObj; 
               
        }
            catch (Exception ex)
            {
                throw new Exception(".GetPostfall_clinial_monitoring_details_a1_by_id\n" + ex.Message);
            }
        }






        public static MasterDetails GetPartAPage2(int linkid)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                MasterDetails l_Model = new MasterDetails();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_id_new_tbl_postfall_clinialpage2parta", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@linkid", linkid);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                l_Model.id=Convert.ToInt32(l_Reader["id"]);
               l_Model.linkid=Convert.ToInt32(l_Reader["linkid"]);
                l_Model.c_longsound_normal=Convert.ToString(l_Reader["c_longsound_normal"]);
                 l_Model.c_longsound_describe=Convert.ToString(l_Reader["c_longsound_describe"]);
                 l_Model.c_longsound_equal=Convert.ToString(l_Reader["c_longsound_equal"]);
                 l_Model.c_c=Convert.ToString(l_Reader["c_c"]);
                 l_Model.c_c_other=Convert.ToString(l_Reader["c_c_other"]);
                  l_Model.c_heartsound=Convert.ToString(l_Reader["c_heartsound"]);
                 l_Model.c_heartsound_describe=Convert.ToString(l_Reader["c_heartsound_describe"]);
                 l_Model.a_soft=Convert.ToString(l_Reader["a_soft"]);
               l_Model.a_soft_describe=Convert.ToString(l_Reader["a_soft_describe"]);
                l_Model.a_pful=Convert.ToString(l_Reader["a_pful"]);
                l_Model.a_pful_describe=Convert.ToString(l_Reader["a_pful_describe"]);
                l_Model.a_bowelsound=Convert.ToString(l_Reader["a_bowelsound"]);
                 l_Model.a_lastbowel_date=Convert.ToString(l_Reader["a_lastbowel_date"]);
                 l_Model.a_voidingnormal=Convert.ToString(l_Reader["a_voidingnormal"]);
                 l_Model.a_voidingnormal_describe=Convert.ToString(l_Reader["a_voidingnormal_describe"]);
                l_Model.a_voidingnormal1=Convert.ToString(l_Reader["a_voidingnormal1"]);
                 l_Model.a_voidingnormal_pads=Convert.ToString(l_Reader["a_voidingnormal_pads"]);
                 l_Model.a_voidingnormal2=Convert.ToString(l_Reader["a_voidingnormal2"]);
                 l_Model.edema_feet_normal=Convert.ToString(l_Reader["edema_feet_normal"]);
                l_Model.edema_feet_describe=Convert.ToString(l_Reader["edema_feet_describe"]);
                 l_Model.edema_feet1=Convert.ToString(l_Reader["edema_feet1"]);
                  l_Model.edema_hands_normal=Convert.ToString(l_Reader["edema_hands_normal"]);
              l_Model.edema_hands_describe=Convert.ToString(l_Reader["edema_hands_describe"]);
               l_Model.edema_hands1=Convert.ToString(l_Reader["edema_hands1"]);
                 l_Model.edema_other=Convert.ToString(l_Reader["edema_other"]);
                l_Model.edema_other_describe=Convert.ToString(l_Reader["edema_other_describe"]);
             l_Model.skin_feet=Convert.ToString(l_Reader["skin_feet"]);
                l_Model.skin_feet_describe=Convert.ToString(l_Reader["skin_feet_describe"]);
             l_Model.skin_rashes=Convert.ToString(l_Reader["skin_rashes"]);
                l_Model.skin_redness=Convert.ToString(l_Reader["skin_redness"]);
                 l_Model.skin_bruising=Convert.ToString(l_Reader["skin_bruising"]);
                 l_Model.skin_openareas=Convert.ToString(l_Reader["skin_openareas"]);
                l_Model.skin_desc_abnormal=Convert.ToString(l_Reader["skin_desc_abnormal"]);

                l_Model.skin_wounddressing=Convert.ToString(l_Reader["skin_wounddressing"]);
                 l_Model.skin_desc=Convert.ToString(l_Reader["skin_desc"]);
                 l_Model.p_residentp=Convert.ToString(l_Reader["p_residentp"]);
                 l_Model.p_residentp_desc=Convert.ToString(l_Reader["p_residentp_desc"]);
                l_Model.p_pscale=Convert.ToString(l_Reader["p_pscale"]);
                 l_Model.p_aching=Convert.ToString(l_Reader["p_aching"]);
                 l_Model.p_sharp=Convert.ToString(l_Reader["p_sharp"]);
                l_Model.p_dull=Convert.ToString(l_Reader["p_dull"]);
                 l_Model.p_radiating=Convert.ToString(l_Reader["p_radiating"]);
                 l_Model.p_where=Convert.ToString(l_Reader["p_where"]);
                l_Model.p_whatmakes_better=Convert.ToString(l_Reader["p_whatmakes_better"]);
                 l_Model.p_whatmakes_worst=Convert.ToString(l_Reader["p_whatmakes_worst"]);
                 l_Model.p_interface_adl=Convert.ToString(l_Reader["p_interface_adl"]);
                 l_Model.p_describe=Convert.ToString(l_Reader["p_describe"]);
                l_Model.p_other=Convert.ToString(l_Reader["p_other"]);
                  l_Model.completed_by=Convert.ToString(l_Reader["completed_by"]);
                    
                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetPartAPage2\n" + ex.Message);
            }
        }





        public static void DeleteSuite(int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_delete_new_tbl_suite", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "DeleteSuite |" + ex.ToString();
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