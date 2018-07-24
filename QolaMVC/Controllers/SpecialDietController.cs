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
        public ActionResult List()
        {
            List<NEW_SpecialDietModel> l_Model = SpecialDietDAL.GetAllSpecialDiet();
            return View(l_Model);
            //return View();
        }

        // GET: Suite/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Suite/Create
        public ActionResult AddSpecialDiet()
        {
            return View();
        }

        // POST: Suite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpecialDiet(NEW_SpecialDietModel data)
        {
            if (ModelState.IsValid)
            {
                SpecialDietDAL.AddSpecialDiet(data);
                return RedirectToAction("List");
            }
            else
            return RedirectToAction("List");

        }

        // GET: Suite/Edit/5
        public ActionResult EditSpecialDiet(int id)
        {
            return View(SpecialDietDAL.GetSpecialDiet_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSpecialDiet(int id, NEW_SpecialDietModel data)
        {
            if (ModelState.IsValid)
            {
                SpecialDietDAL.EditSpecialDiet(data, id);
                return RedirectToAction("List");
            }
            else
                return RedirectToAction("List");
        }

        // GET: Suite/Delete/5
        public ActionResult DeleteSpecialDiet(int id)
        {
            return View(SpecialDietDAL.GetSpecialDiet_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteSpecialDiet(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                SpecialDietDAL.DeleteSpecialDiet(id);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
