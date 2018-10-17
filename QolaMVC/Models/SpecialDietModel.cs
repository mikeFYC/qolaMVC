using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class SpecialDietModel
    {
        #region "SpecialDiet"

        #region "Fields"

        private int _id;
        private string _name;
        private UserModel _modifiedBy;
        private DateTime _modifiedOn;
        private string _sid;
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

        public UserModel ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }
        public string sId
        {
            get { return _sid; }
            set { _sid = value; }
        }
        #endregion

        #endregion
    }
}