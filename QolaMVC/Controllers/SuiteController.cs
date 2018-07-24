using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QolaMVC.DAL;
using QolaMVC.Models;

namespace QolaMVC.Controllers
{
    public class SuiteController : Controller
    {
        private QOLAProductionFinalsEntities db = new QOLAProductionFinalsEntities();
        // GET: Suite
        public ActionResult List(string column="*", string value="0")
        {
            //List<NEW_SuiteModel> l_Model = SuiteDAL.GetAllSuite();
            List<NEW_SuiteModel> l_Model = SuiteDAL.GetSuite_By_Column(column, value);
            return View(l_Model);
            //return View();
        }

        public ActionResult GetListByColumn(string column, string value)
        {
            List<NEW_SuiteModel> l_Model = SuiteDAL.GetSuite_By_Column(column, value);
            return View(l_Model);
            //return View();
        }

        // GET: Suite/Create
        public ActionResult AddSuite()
        {
            return View();
        }

        // POST: Suite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSuite(NEW_SuiteModel add_tbl_suite)
        {
            if (ModelState.IsValid)
            {
                SuiteDAL.AddSuite(add_tbl_suite);
                return RedirectToAction("List");
            }
            else
            return RedirectToAction("List");

        }

        // GET: Suite/Edit/5
        public ActionResult EditSuite(int id)
        {
            return View(SuiteDAL.GetSuite_By_Id(id));
            //List<NEW_SuiteModel> l_Model = SuiteDAL.GetSuite_By_Id(id);
            //return View(l_Model);
            //return View();
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSuite(int id, NEW_SuiteModel edit_tbl_suite)
        {
            if (ModelState.IsValid)
            {
                SuiteDAL.EditSuite(edit_tbl_suite, id);
                return RedirectToAction("List");
            }
            else
                return RedirectToAction("List");
        }

        // GET: Suite/Delete/5
        public ActionResult DeleteSuite(int id)
        {
            return View(SuiteDAL.GetSuite_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteSuite(int id, FormCollection collection)
        {
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
    }
}
