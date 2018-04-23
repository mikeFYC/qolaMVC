using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    /*
     * Qola 4.0.0
     * Created: 27-03-2018 by Chime
     * This class is responsible for storing the different user rights
     * Features will have user rights for users to have access to them. User rights are assigned to user types
     */
    public class UserRightsModel
    {
        public int Id { get; set; }
        public string UserRight { get; set; }

        public UserRightsModel(int p_Id, string p_UserRight)
        {
            Id = p_Id;
            UserRight = p_UserRight;
        }
    }
}