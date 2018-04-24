using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QolaMVC.Constants
{
    public class ConnectionString
    {
        public static string PROD { get { return ConfigurationManager.ConnectionStrings["prod"].ConnectionString; } }
        public static string DEV { get { return ConfigurationManager.ConnectionStrings["dev"].ConnectionString; } }
    }
}