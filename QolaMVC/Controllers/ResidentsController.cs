﻿using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class ResidentsController : Controller
    {
        // GET: Residents
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string term)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            List<dynamic> l_Json = new List<dynamic>();
            var residents = ResidentsDAL.GetResidentSearchByHomeId(home.Id, term, 'N');

            foreach(var r in residents)
            {
                dynamic l_J = new System.Dynamic.ExpandoObject();
                l_J.label = r.ShortName;
                l_J.FirstName = r.FirstName;
                l_J.LastName = r.LastName;
                l_J.SuiteNo = r.SuiteNo;
                l_J.value = r.ID;
                l_J.image = r.ResidentImage;
                l_Json.Add(l_J);
            }

            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }
    }
}