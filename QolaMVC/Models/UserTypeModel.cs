using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    /*
     * Qola 4.0.0
     * Created: 26-03-2018 by Chime
     * This class is responsible for storing the different user types
     * Every user will belong to a usertype group. User rights are assigned to user types
     */
    public class UserTypeModel
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public List<string> UserRights { get; set; }

        public UserTypeModel(int p_Id, string p_UserType)
        {
            Id = p_Id;
            UserType = p_UserType;
            //UserRights = p_UserRights;
        }
    }
}