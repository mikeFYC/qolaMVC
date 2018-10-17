using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static QolaMVC.Constants.EnumerationTypes;

namespace QolaMVC.Models
{

        public class UserModel
        {
            #region "Users"
            #region "Fields"
            private int _id;
            private string _home;
            private string _firstName;
            private string _lastName;
            private int _userType;
            private string _userName;
            private string _password;
            private string _oldPassword;
            private string _address;
            private string _city;
            private string _postalCode;
            private string _province;
            private string _email;
            private string _workPhone;
            private int _ext;
            private string _homePhone;
            private string _mobile;
            private AvailabilityStatus _status = AvailabilityStatus.A;
            private DateTime _modifiedOn;
            private int _modifiedBy;
            private string _userTypeName;
            private string _homeName;
            private string _country;
            private string _userFirstLastName;
            private bool _rememberMe = false;
            #endregion
            #region "Properties"

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public string Home
            {
                get { return _home; }
                set { _home = value; }
            }

            public string FirstName
            {
                get { return _firstName; }
                set { _firstName = value; }
            }

            public string LastName
            {
                get { return _lastName; }
                set { _lastName = value; }
            }

            public string Name
            {
                get { return _userFirstLastName; }
                set { _userFirstLastName = value; }
            }

            public int UserType
            {
                get { return _userType; }
                set { _userType = value; }
            }

            public string UserName
            {
                get { return _userName; }
                set { _userName = value; }
            }

            public string Password
            {
                get { return _password; }
                set { _password = value; }
            }

            public string OldPassword
            {
                get { return _oldPassword; }
                set { _oldPassword = value; }
            }

            public string Address
            {
                get { return _address; }
                set { _address = value; }
            }

            public string City
            {
                get { return _city; }
                set { _city = value; }
            }

            public string PostalCode
            {
                get { return _postalCode; }
                set { _postalCode = value; }
            }

            public string Province
            {
                get { return _province; }
                set { _province = value; }
            }

            public string Email
            {
                get { return _email; }
                set { _email = value; }
            }

            public string WorkPhone
            {
                get { return _workPhone; }
                set { _workPhone = value; }
            }

            public int Ext
            {
                get { return _ext; }
                set { _ext = value; }
            }

            public string HomePhone
            {
                get { return _homePhone; }
                set { _homePhone = value; }
            }

            public string Mobile
            {
                get { return _mobile; }
                set { _mobile = value; }
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

            public int ModifiedBy
            {
                get { return _modifiedBy; }
                set { _modifiedBy = value; }
            }

            public string UserTypeName
            {
                get { return _userTypeName; }
                set { _userTypeName = value; }
            }

            public string HomeName
            {
                get { return _homeName; }
                set { _homeName = value; }
            }

            public string Country
            {
                get { return _country; }
                set { _country = value; }
            }
            public bool RememberMe
            {
                get { return _rememberMe; }
                set { _rememberMe = value; }
            }
        #endregion
        #endregion
    }
    }
