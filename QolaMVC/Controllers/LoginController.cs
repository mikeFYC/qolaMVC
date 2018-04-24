using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Auth(FormCollection p_Collection)
        {
            var l_User = new UserModel();
            TryUpdateModel(l_User, p_Collection);

            string l_Password = BitConverter.ToString(new SHA1Managed().ComputeHash(new UTF8Encoding().GetBytes(l_User.Password)));

            if (UserDAL.GetUserByUsernameAndPassword(l_User.UserName, l_Password, string.Empty, string.Empty).ID != 0)
            {
                return Redirect("/Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}