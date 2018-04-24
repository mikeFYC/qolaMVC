using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeDAL DALHome = new HomeDAL();
            List<HomeModel> l_Homes = DALHome.GetHomes();
            UserModel l_User = UserDAL.GetUserById(78);
            return View();
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