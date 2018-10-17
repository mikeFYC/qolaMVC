using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class AllergiesModel
    {
        #region "Allergies"

        #region "Fields"

        private int _id;
        private string _name;
        private int _category;
        private UserModel _modifiedBy;
        private DateTime _modifiedOn;

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

        public int Catogery
        {
            get { return _category; }
            set { _category = value; }
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
        #endregion

        #endregion
    }
}