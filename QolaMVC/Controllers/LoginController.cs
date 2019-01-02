using Microsoft.Owin.Security;
using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace QolaMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string p_Message = "")
        {
            if (p_Message != string.Empty)
            {
                ViewBag.ErrorMessage = p_Message;
            }
            if (Session["language"] == null || Session["language"].ToString() == "")
            {
                Session["language"] = "en";
            }

            return View();
        }


        public ActionResult ChangeLanguage(string LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        public void ChangeLanguage2(string LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            if (LanguageAbbrevation == "" || LanguageAbbrevation==null || LanguageAbbrevation == "en")
            {
                Session["Language"] = "en";
            }
            else if (LanguageAbbrevation == "fr")
            {
                Session["Language"] = "fr";
            }

        }

        [WebMethod(EnableSession = true)]
        public string setLanguageId(string sLanguageId)
        {
            string result = string.Empty;

            System.Web.HttpContext.Current.Session["language"] = sLanguageId;

            return result;
        }


        [HttpPost]
        public ActionResult Auth_Decom(FormCollection p_Collection)
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
                string l_ErrorMessage = "Invalid Username of password";
                return RedirectToAction("Index", new { p_Message = l_ErrorMessage } );
            }
        }

        [HttpPost]
        public ActionResult Auth(UserModel model, string ReturnUrl)
        {
            string l_Password = BitConverter.ToString(new SHA1Managed().ComputeHash(new UTF8Encoding().GetBytes(model.Password)));

            var user = UserDAL.GetUserByUsernameAndPassword(model.UserName, l_Password, string.Empty, string.Empty);

            //check username and password from database, naive checking: 
            //password should be in SHA
            if (user.ID != 0)
            {
                var claims = new[] {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                // can add more claims
            };
                TempData["User"] = user;
                
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                // Add roles into claims
                    //var roles = _roleService.GetByUserId(user.Id);
                    //if (roles.Any())
                    //{
                    //    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r.Name));
                    //    identity.AddClaims(roleClaims);
                    //}

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                
                authManager.SignIn(new AuthenticationProperties
                { IsPersistent = model.RememberMe }, identity);
                Thread.CurrentPrincipal = claimsPrincipal;

                return RedirectToAction("Index", "Home");
            }
            // login failed.   
            string l_ErrorMessage = "Invalid Username or password";
            return RedirectToAction("Index", new { p_Message = l_ErrorMessage });
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login");
        }
    }
}