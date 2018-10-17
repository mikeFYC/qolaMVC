using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static QolaMVC.Constants.EnumerationTypes;

namespace QolaMVC.Models
{
    public class SuiteModel
    {
        #region "Users"
        #region "Fields"
        private int _id;
        private HomeModel _home;
        private string _suiteNo;
        private int _noOfRooms;
        private int _floor;
        private AvailabilityStatus _status = AvailabilityStatus.A;
        private DateTime _modifiedOn;
        private UserModel _modifiedBy;
        #endregion
        #region "Properties"

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public HomeModel Home
        {
            get { return _home; }
            set { _home = value; }
        }

        public string SuiteNo
        {
            get { return _suiteNo; }
            set { _suiteNo = value; }
        }

        public int NoOfRooms
        {
            get { return _noOfRooms; }
            set { _noOfRooms = value; }
        }

        public int Floor
        {
            get { return _floor; }
            set { _floor = value; }
        }

        public AvailabilityStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        public UserModel ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        #endregion
        #endregion
    }
}