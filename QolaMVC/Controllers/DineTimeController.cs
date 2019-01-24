using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QolaMVC.DAL;
using QolaMVC.Models;

namespace QolaMVC.Controllers
{
    public class DineTimeController : Controller
    {
        // GET: Suite
        public ActionResult List(string search,string column = "*", string value = "0")
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

            List<NEW_DineTimeModel> l_Model;
            // List<NEW_DineTimeModel> l_Model = DineTimeDAL.GetAllDineTime();
            if (search == null || search == "")
            {
                l_Model = DineTimeDAL.GetDineTime_By_Column(column, value);
            }
            //List<NEW_SpecialDietModel> l_Model = SpecialDietDAL.GetAllSpecialDiet();
            else
            {
                l_Model = DineTimeDAL.GetDineTime_By_Search(search);
            }
            
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
        public ActionResult AddDineTime()
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

        // POST: Suite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDineTime(NEW_DineTimeModel data)
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
                DineTimeDAL.AddDineTime(data);
                TempData["notice"] = "Add Successfully";
                return RedirectToAction("List");
            }
            else
            {
                TempData["notice"] = "Add Failed";
                return RedirectToAction("List");
            }
            

        }

        // GET: Suite/Edit/5
        public ActionResult EditDineTime(int id)
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

            return View(DineTimeDAL.GetDineTime_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDineTime(int id, NEW_DineTimeModel data)
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
                DineTimeDAL.EditDineTime(data, id);
                TempData["notice"] = "Edit Successfully";
                return RedirectToAction("List");
            }
            else
            {
                TempData["notice"] = "Edit Failed";
                return RedirectToAction("List");
            }
                
        }

        // GET: Suite/Delete/5
        public ActionResult DeleteDineTime(int id)
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

            return View(DineTimeDAL.GetDineTime_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteDineTime(int id, FormCollection collection)
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

                DineTimeDAL.DeleteDineTime(id);
                TempData["notice"] = "Delete Successfully";
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
