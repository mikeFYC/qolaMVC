using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QolaMVC.DAL;
using QolaMVC.Models;

namespace QolaMVC.Controllers
{
    public class SpecialDietController : Controller
    {
        // GET: Suite
        public ActionResult List(string search, string column = "*", string value = "0")
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

            List<NEW_SpecialDietModel> l_Model;
            if(search==null || search == "")
            {
                l_Model = SpecialDietDAL.GetSpecialDiet_By_Column(column, value);
            }
            //List<NEW_SpecialDietModel> l_Model = SpecialDietDAL.GetAllSpecialDiet();
            else
            {
                l_Model = SpecialDietDAL.GetSpecialDiet_By_Search(search);
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
        public ActionResult AddSpecialDiet()
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
        public ActionResult AddSpecialDiet(NEW_SpecialDietModel data)
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
                SpecialDietDAL.AddSpecialDiet(data);
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
        public ActionResult EditSpecialDiet(int id)
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

            return View(SpecialDietDAL.GetSpecialDiet_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSpecialDiet(int id, NEW_SpecialDietModel data)
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
                SpecialDietDAL.EditSpecialDiet(data, id);
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
        public ActionResult DeleteSpecialDiet(int id)
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

            return View(SpecialDietDAL.GetSpecialDiet_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteSpecialDiet(int id, FormCollection collection)
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

                SpecialDietDAL.DeleteSpecialDiet(id);
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
