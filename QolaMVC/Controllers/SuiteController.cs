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
    public class SuiteController : Controller
    {
        // GET: Suite
        public ActionResult List(string search,string column="*", string value="0")
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
            List<NEW_SuiteModel> l_Model;
            if (search==null || search == "")
            {
                l_Model = SuiteDAL.GetSuite_mike(string.Empty);
                TempData["search"] = "";
            }
            //List<NEW_SuiteModel> l_Model = SuiteDAL.GetAllSuite();
            else
            {
                l_Model = SuiteDAL.GetSuite_mike(search);
                TempData["search"] = search;
            }
            TempData["start"] = "1";
            return View(l_Model);
            
        }

        public ActionResult GetListByColumn(string column, string value)
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

            List<NEW_SuiteModel> l_Model = SuiteDAL.GetSuite_By_Column(column, value);
            return View(l_Model);
            //return View();
        }

        // GET: Suite/Create
        public ActionResult AddSuite()
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
        public ActionResult AddSuite(NEW_SuiteModel add_tbl_suite)
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
                SuiteDAL.AddSuite(add_tbl_suite,user.ID);
                return RedirectToAction("List");
            }
            else
            return RedirectToAction("List");

        }

        // GET: Suite/Edit/5
        public ActionResult EditSuite(int id)
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
            return View(SuiteDAL.GetSuite_By_Id(id));
            //List<NEW_SuiteModel> l_Model = SuiteDAL.GetSuite_By_Id(id);
            //return View(l_Model);
            //return View();
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSuite(int id, NEW_SuiteModel suite)
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
                SuiteDAL.EditSuite(suite, id);
                return RedirectToAction("List");
            }
            else
                return RedirectToAction("List");
        }

        // GET: Suite/Delete/5
        public ActionResult DeleteSuite(int id)
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

            return View(SuiteDAL.GetSuite_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteSuite(int id, FormCollection collection)
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

                SuiteDAL.DeleteSuite(id);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetSuiteList(int index, string search)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            List<NEW_SuiteModel> l_Model;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if (search == null || search == "")
            {
                l_Model = SuiteDAL.GetSuite_mike(string.Empty);
            }
            //List<NEW_SuiteModel> l_Model = SuiteDAL.GetAllSuite();
            else
            {
                l_Model = SuiteDAL.GetSuite_mike(search);
            }
            List<dynamic> l_Json = new List<dynamic>();

            for (int a = (index - 1) * 50; a < index * 50; a++)
            {
                if (a < l_Model.Count())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.ID = l_Model[a].Id;
                    l_J.Home = l_Model[a].Home;
                    l_J.HomeID = l_Model[a].HomeID;
                    l_J.Suite_No = l_Model[a].Suite_No;
                    l_J.Floor_No = l_Model[a].Floor_No;
                    l_J.No_Of_Rooms = l_Model[a].No_Of_Rooms;
                    l_Json.Add(l_J);
                }

            }
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }


    }
}
