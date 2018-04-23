using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QolaMVC.Models;

namespace QolaMVC.DAL
{
    public class HomeDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public HomeDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public List<HomeModel> GetHomes()
        {
            SqlConnection l_Conn = new SqlConnection(_ConnectionString);
            try
            {
                List<HomeDAL> l_Collection = new List<HomeDAL>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("Get_Home", l_Conn);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}