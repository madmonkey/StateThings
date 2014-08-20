using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateInterface.Areas.Design.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var homeModel = new HomeModel(Url.Action("Index", "Form"));

            homeModel.InitialData = JsonConvert.SerializeObject(homeModel);
            
            ViewBag.Title = "Designer Home";
            return View(homeModel);
        }
    }
}
