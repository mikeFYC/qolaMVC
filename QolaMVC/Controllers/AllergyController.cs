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
        public ActionResult List(string column = "*", string value = "0")
        {
            //List<NEW_AllergyModel> l_Model = AllergyDAL.GetAllAllergy();
            List<NEW_AllergyModel> l_Model = AllergyDAL.GetAllergy_By_Column(column, value);
            return View(l_Model);
            //return View();
        }

        // GET: Suite/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Suite/Create
        public ActionResult AddAllergy()
        {
            return View();
        }

        // POST: Suite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAllergy(NEW_AllergyModel data)
        {
            if (ModelState.IsValid)
            {
                AllergyDAL.AddAllergy(data);
                return RedirectToAction("List");
            }
            else
            return RedirectToAction("List");

        }

        // GET: Suite/Edit/5
        public ActionResult EditAllergy(int id)
        {
            return View(AllergyDAL.GetAllergy_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAllergy(int id, NEW_AllergyModel data)
        {
            if (ModelState.IsValid)
            {
                AllergyDAL.EditAllergy(data, id);
                return RedirectToAction("List");
            }
            else
                return RedirectToAction("List");
        }

        // GET: Suite/Delete/5
        public ActionResult DeleteAllergy(int id)
        {
            return View(AllergyDAL.GetAllergy_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteAllergy(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                AllergyDAL.DeleteAllergy(id);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
