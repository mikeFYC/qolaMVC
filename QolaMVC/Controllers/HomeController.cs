using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
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
            var identity = (ClaimsPrincipal) Thread.CurrentPrincipal;
            var l_Name = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).SingleOrDefault();
            var l_UserId = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                               .Select(c => c.Value).SingleOrDefault();

            if(l_Name != null && l_UserId != null)
            {
                HomeDAL DALHome = new HomeDAL();
                List<HomeModel> l_Homes = DALHome.GetHomes();
                UserModel l_User = UserDAL.GetUserById(Convert.ToInt32(l_UserId));

                ViewBag.User = l_User;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult ManageResidents()
        {
            return View();
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