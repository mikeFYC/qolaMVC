using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QolaMVC.DAL;
using QolaMVC.Models;

namespace QolaMVC.Controllers
{
    public class AllergyController : Controller
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

            //List<NEW_AllergyModel> l_Model = AllergyDAL.GetAllAllergy();
            List<NEW_AllergyModel> l_Model;


            if (search == null || search == "")
            {
                //l_Model = AllergyDAL.GetAllergy_By_Column(column, value);
                l_Model = AllergyDAL.GetAllergy_mike(string.Empty);
                TempData["search"] = "";
            }
            //List<NEW_SpecialDietModel> l_Model = SpecialDietDAL.GetAllSpecialDiet();
            else
            {
                //l_Model = AllergyDAL.GetAllergy_By_Search(search);
                l_Model = AllergyDAL.GetAllergy_mike(search);
                TempData["search"] = search;
            }
            TempData["start"] = "1";
            return View(l_Model);
            //return View();
        }

        [HttpGet]
        public ActionResult GetAllergyList(int index,string search)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            List<NEW_AllergyModel> l_Model;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if (search == null || search == "")
            {
                l_Model = AllergyDAL.GetAllergy_mike(string.Empty);
            }
            else
            {
                l_Model = AllergyDAL.GetAllergy_mike(search);
            }
            List<dynamic> l_Json = new List<dynamic>();

            for (int a = (index - 1) * 50; a < index * 50; a++)
            {
                if (a<l_Model.Count())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.ID = l_Model[a].Id;
                    l_J.name = l_Model[a].Allergy_Name;
                    l_J.category = l_Model[a].Category;
                    l_Json.Add(l_J);
                }

            }
            return Json(l_Json, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddAllergy()
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
        public ActionResult AddAllergy(NEW_AllergyModel data)
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
                AllergyDAL.AddAllergy(data,user.ID);
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
        public ActionResult EditAllergy(int id)
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

            return View(AllergyDAL.GetAllergy_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAllergy(int id, NEW_AllergyModel data)
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
                AllergyDAL.EditAllergy(data, id);
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
        public ActionResult DeleteAllergy(int id)
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

            return View(AllergyDAL.GetAllergy_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteAllergy(int id, FormCollection collection)
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

                AllergyDAL.DeleteAllergy(id);
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
