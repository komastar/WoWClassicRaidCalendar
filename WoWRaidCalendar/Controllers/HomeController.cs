using Newtonsoft.Json.Linq;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WCRC.Models;
using WCRC.Repository;

namespace WCRC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string GetEvents(string start, string end)
        {
            var results = ResetEventRepo.Get().GetResetEvents(start, end);
            var jsonData = JArray.FromObject(results.ToArray());

            return jsonData.ToString();
        }
    }
}