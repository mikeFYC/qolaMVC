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
        public ActionResult List(string column = "*", string value = "0")
        {
            // List<NEW_DineTimeModel> l_Model = DineTimeDAL.GetAllDineTime();
            List<NEW_DineTimeModel> l_Model = DineTimeDAL.GetDineTime_By_Column(column, value);
            return View(l_Model);
            //return View();
        }

        // GET: Suite/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Suite/Create
        public ActionResult AddDineTime()
        {
            return View();
        }

        // POST: Suite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDineTime(NEW_DineTimeModel data)
        {
            if (ModelState.IsValid)
            {
                DineTimeDAL.AddDineTime(data);
                return RedirectToAction("List");
            }
            else
            return RedirectToAction("List");

        }

        // GET: Suite/Edit/5
        public ActionResult EditDineTime(int id)
        {
            return View(DineTimeDAL.GetDineTime_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDineTime(int id, NEW_DineTimeModel data)
        {
            if (ModelState.IsValid)
            {
                DineTimeDAL.EditDineTime(data, id);
                return RedirectToAction("List");
            }
            else
                return RedirectToAction("List");
        }

        // GET: Suite/Delete/5
        public ActionResult DeleteDineTime(int id)
        {
            return View(DineTimeDAL.GetDineTime_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteDineTime(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                DineTimeDAL.DeleteDineTime(id);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
