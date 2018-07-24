using System;
using System.Collections.Generic;
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
        public ActionResult List()
        {
            List<NEW_VenueModel> l_Model = VenueDAL.GetAllVenue();
            return View(l_Model);
            //return View();
        }

        // GET: Suite/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Suite/Create
        public ActionResult AddVenue()
        {
            return View();
        }

        // POST: Suite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVenue(NEW_VenueModel data)
        {
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
            return View(VenueDAL.GetVenue_By_Id(id));
        }

        // POST: Suite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVenue(int id, NEW_VenueModel data)
        {
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
            return View(VenueDAL.GetVenue_By_Id(id));
        }

        // POST: Suite/Delete/5
        [HttpPost]
        public ActionResult DeleteVenue(int id, FormCollection collection)
        {
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
