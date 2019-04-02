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
    public class DocumentsDAL
    {
        public static int AddResidentDocument(Documents p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_Add_ResidentDocument", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.ResidentId);
                l_Cmd.Parameters.AddWithValue("@DocumentMasterId", p_Model.DocumentType.Id);
                l_Cmd.Parameters.AddWithValue("@Title", p_Model.Title);
                l_Cmd.Parameters.AddWithValue("@FileName", p_Model.FileName);
                l_Cmd.Parameters.AddWithValue("@FilePath", p_Model.FilePath);
                l_Cmd.Parameters.AddWithValue("@UserId", p_Model.CreatedByUserId);

                SqlParameter returnVal = l_Cmd.Parameters.Add("tempid", SqlDbType.Int);
                returnVal.Direction = ParameterDirection.ReturnValue;
                l_Cmd.ExecuteNonQuery();
                return Convert.ToInt32(returnVal.Value);
            }
            catch (Exception ex)
            {
                exception = "AddResidentDocument |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static int UpdateResidentDocument(Documents p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_Update_ResidentDocument", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@DocumentId", p_Model.Id);
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.ResidentId);
                l_Cmd.Parameters.AddWithValue("@DocumentMasterId", p_Model.DocumentType.Id);
                l_Cmd.Parameters.AddWithValue("@Title", p_Model.Title);
                l_Cmd.Parameters.AddWithValue("@FileName", p_Model.FileName);
                l_Cmd.Parameters.AddWithValue("@FilePath", p_Model.FilePath);
                l_Cmd.Parameters.AddWithValue("@UserId", p_Model.CreatedByUserId);

                l_Cmd.ExecuteNonQuery();
                return Convert.ToInt32(p_Model.Id);
            }
            catch (Exception ex)
            {
                exception = "UpdateResidentDocument |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<Documents> GetDocumentListByResident(int residentId, int? documentTypeId)
        {
            string exception = string.Empty;
            Collection<Documents> p_DocumentsList = new Collection<Documents>();
            Documents l_DocumentsList;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_Get_All_Documents_By_Resident", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentId);
                l_Cmd.Parameters.AddWithValue("@DocumentTypeId", documentTypeId);
                DataSet documentsList = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(documentsList);

                if (documentsList.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= documentsList.Tables[0].Rows.Count - 1; index++)
                    {
                        l_DocumentsList = new Documents();
                        l_DocumentsList.Id = Convert.ToInt32(documentsList.Tables[0].Rows[index]["Id"]);
                        l_DocumentsList.ResidentId = Convert.ToInt32(documentsList.Tables[0].Rows[index]["ResidentId"]);
                        l_DocumentsList.DocumentType = new DocumentTypeModel { Id = Convert.ToInt32(documentsList.Tables[0].Rows[index]["DocumentTypeId"]), Type = Convert.ToString(documentsList.Tables[0].Rows[index]["DocumentTypeName"]) };
                        l_DocumentsList.Title = Convert.ToString(documentsList.Tables[0].Rows[index]["Title"]);
                        l_DocumentsList.FileName = Convert.ToString(documentsList.Tables[0].Rows[index]["FileName"]);
                        l_DocumentsList.FilePath = Convert.ToString(documentsList.Tables[0].Rows[index]["FilePath"]);
                        l_DocumentsList.CreatedByUserId = (!string.IsNullOrEmpty(Convert.ToString(documentsList.Tables[0].Rows[index]["CreatedByUserId"]))) ? Convert.ToInt32(documentsList.Tables[0].Rows[index]["CreatedByUserId"]) : (int?)null;
                        l_DocumentsList.CreatedByUserName = Convert.ToString(documentsList.Tables[0].Rows[index]["CreatedByUserName"]);
                        l_DocumentsList.ModifiedByUserId = (!string.IsNullOrEmpty(Convert.ToString(documentsList.Tables[0].Rows[index]["ModifiedByUserId"]))) ? Convert.ToInt32(documentsList.Tables[0].Rows[index]["ModifiedByUserId"]) : (int?)null;
                        l_DocumentsList.ModifiedByUserName = Convert.ToString(documentsList.Tables[0].Rows[index]["ModifiedByUserName"]);
                        l_DocumentsList.CreatedOn = (!string.IsNullOrEmpty(Convert.ToString(documentsList.Tables[0].Rows[index]["CreatedOn"]))) ? Convert.ToString(documentsList.Tables[0].Rows[index]["CreatedOn"]) : string.Empty;
                        l_DocumentsList.ModifiedOn = (!string.IsNullOrEmpty(Convert.ToString(documentsList.Tables[0].Rows[index]["ModifiedOn"]))) ? Convert.ToString(documentsList.Tables[0].Rows[index]["ModifiedOn"]) : string.Empty;

                        p_DocumentsList.Add(l_DocumentsList);
                    }
                }
                return p_DocumentsList;
            }
            catch (Exception ex)
            {
                exception = ".GetAllActivityCategory\n" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static string DeleteDocument(int p_DocumentId)
        {
            string exception = "Success";

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_Delete_Document", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@DocumentId", p_DocumentId);
                l_Cmd.ExecuteNonQuery();
                return exception;
            }
            catch (Exception ex)
            {
                exception = "DeleteDocument |" + ex.ToString();
                return exception;
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Documents GetDocumentById(int p_DocumentId)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                Documents l_Model = new Documents();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_Get_Document_By_Id", l_Conn);
                l_Cmd.Parameters.AddWithValue("@DocumentId", p_DocumentId);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.ResidentId = Convert.ToInt32(l_Reader["ResidentId"]);
                    l_Model.DocumentType = new DocumentTypeModel { Id = Convert.ToInt32(l_Reader["DocumentTypeId"]), Type = Convert.ToString(l_Reader["DocumentTypeName"]) };
                    l_Model.Title = Convert.ToString(l_Reader["Title"]);
                    l_Model.FileName = Convert.ToString(l_Reader["FileName"]);
                    l_Model.FilePath = Convert.ToString(l_Reader["FilePath"]);
                    l_Model.CreatedByUserId = (!string.IsNullOrEmpty(Convert.ToString(l_Reader["CreatedByUserId"]))) ? Convert.ToInt32(l_Reader["CreatedByUserId"]) : (int?)null;
                    l_Model.CreatedByUserName = Convert.ToString(l_Reader["CreatedByUserName"]);
                    l_Model.ModifiedByUserId = (!string.IsNullOrEmpty(Convert.ToString(l_Reader["ModifiedByUserId"]))) ? Convert.ToInt32(l_Reader["ModifiedByUserId"]) : (int?)null;
                    l_Model.ModifiedByUserName = Convert.ToString(l_Reader["ModifiedByUserName"]);
                    l_Model.CreatedOn = (!string.IsNullOrEmpty(Convert.ToString(l_Reader["CreatedOn"]))) ? Convert.ToString(l_Reader["CreatedOn"]) : string.Empty;
                    l_Model.ModifiedOn = (!string.IsNullOrEmpty(Convert.ToString(l_Reader["ModifiedOn"]))) ? Convert.ToString(l_Reader["ModifiedOn"]) : string.Empty;
                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetDocumentById\n" + ex.Message);
            }
        }
    }
}