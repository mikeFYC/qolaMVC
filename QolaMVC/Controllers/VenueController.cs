using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QolaMVC.DAL;
using QolaMVC.Models;

namespace QolaMVC.Controllers
{
    public class VenueController : Controller
    {
        // GET: Suite
        public ActionResult List(string column = "*", string value = "0")
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            
            //List<NEW_VenueModel> l_Model = VenueDAL.GetAllVenue();
            List<NEW_VenueModel> l_Model = VenueDAL.GetVenue_By_Column(column, value);
            return View(l_Model);
            //return View();
        }

        // GET: Suite/Details/5
        public ActionResult Details(int id)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View();
        }

        // GET: Suite/Create
        public ActionResult AddVenue()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<HomeModel> l_Homes = HomeDAL.GetHomeCollections();
            ViewBag.Homes = l_Homes;

            return View();
        }

        // POST: Suite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVenue(NEW_VenueModel data)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            if (ModelState.IsValid)
            {
                VenueDAL.AddVenue(data);
                return RedirectToAction("List");
            }
            else
            return RedirectToAction("List");

        }

        // GET: Suite/Edit/5
        public ActionResult EditVenue(int id)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(VenueDAL.GetVenue_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVenue(int id, NEW_VenueModel data)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            if (ModelState.IsValid)
            {
                VenueDAL.EditVenue(data, id);
                return RedirectToAction("List");
            }
            else
                return RedirectToAction("List");
        }

        // GET: Suite/Delete/5
        public ActionResult DeleteVenue(int id)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(VenueDAL.GetVenue_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteVenue(int id, FormCollection collection)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            try
            {
                // TODO: Add delete logic here

                VenueDAL.DeleteVenue(id);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
