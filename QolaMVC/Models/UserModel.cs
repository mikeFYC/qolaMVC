using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    /*
     * Qola 4.0.0
     * Created: 27-03-2018 by Chime
     * This class is responsible for storing the different users
     * Every user will belong to a usertype group. User rights are assigned to user types
     */
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        public int UserType { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public bool IsAuthenticated { get; set; }

        public UserModel(int p_UserId, string p_UserName, string p_FirstName, string p_LastName, string p_MiddleName, string p_Title,
                        int p_UserType, DateTime p_LastLogin, DateTime p_DateCreated, int p_CreatedBy, bool p_IsAuthenticated)
        {
            UserId = p_UserId;
            UserName = p_UserName;
            FirstName = p_FirstName;
            LastName = p_LastName;
            MiddleName = p_MiddleName;
            Title = p_Title;
            UserType = p_UserType;
            LastLogin = p_LastLogin;
            DateCreated = p_DateCreated;
            CreatedBy = p_CreatedBy;
            IsAuthenticated = p_IsAuthenticated;
        }
    }
}