using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var context = Request.GetOwinContext();
            //var authManager = context.Authentication;

            //var identity = (ClaimsIdentity)authManager.User.Identity;
            //IEnumerable<Claim> claims = identity.Claims;

            //var l_Name = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
            //       .Select(c => c.Value).SingleOrDefault();
            //var l_UserId = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            //                   .Select(c => c.Value).SingleOrDefault();

            var user = (UserModel)TempData["User"];

            if (user != null)
            {
                Collection<HomeModel> l_Homes = HomeDAL.GetHomeFill(user.ID, 1);// Convert.ToInt32(l_UserId));
                UserModel l_User = UserDAL.GetUserById(user.ID);// Convert.ToInt32(l_UserId));
                TempData["User"] = l_User;

                ViewBag.User = l_User;
                TempData.Keep("User");
                return View(l_Homes.Where(m => m.ProvinceName == "Alberta"));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Menu(int p_HomeId)
        {
            var user = (UserModel)TempData["User"];
            TempData["Home"] = HomeDAL.GetHomeById(p_HomeId);
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            HomeModel l_Home = HomeDAL.GetHomeById(p_HomeId);
            return View(l_Home);
        }

        public ActionResult ResidentMenu(int p_ResidentId)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = ResidentsDAL.GetResidentById(p_ResidentId);
            var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");

            ViewBag.Message = TempData["Message"];

            TempData["Resident"] = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;
            
            ViewBag.ProgressNotes = progressNotes;

            return View(resident);
        }
        public ActionResult ManageResidents(int p_HomeId)
        {
            var l_Residents = ResidentsDAL.GetResidentCollections(p_HomeId);
            return View(l_Residents);
        }

        public ActionResult AddNewResident()
        {
            return View();
        }

        public ActionResult ActivityCalendar()
        {
            List<ActivityEventModel> data = new List<ActivityEventModel> {
             new ActivityEventModel {
                    ProgramName = "Turtle Walk",
                    Comments = "Night out with turtles",
                    ProgramStartTime = new DateTime(2016, 6, 2, 3, 0, 0),
                    ProgramEndTime = new DateTime(2016, 6, 2, 4, 0, 0),
                    IsAllDay = true
             },
             new ActivityEventModel {
                    ProgramName = "Winter Sleepers",
                    Comments = "Long sleep during winter season",
                    ProgramStartTime = new DateTime(2016, 6, 3, 1, 0, 0),
                    ProgramEndTime = new DateTime(2016, 6, 3, 2, 0, 0)
             },
             new ActivityEventModel {
                    ProgramName = "Estivation",
                    Comments = "Sleeping in hot season",
                    ProgramStartTime = new DateTime(2016, 6, 4, 3, 0, 0),
                    ProgramEndTime = new DateTime(2016, 6, 4, 4, 0, 0)
             }
            };
            return View(data);
        }
    }
}