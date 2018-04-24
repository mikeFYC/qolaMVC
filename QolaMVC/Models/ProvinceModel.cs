using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ProvinceModel
    {
        #region "Province"
        #region "Fields"
        private int _id;
        private string _name;
        #endregion
        #region "Properties"


        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion
        #endregion
    }
}