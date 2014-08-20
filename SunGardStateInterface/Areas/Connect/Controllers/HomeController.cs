using Newtonsoft.Json;
using StateInterface.Areas.Connect.Models;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateInterface.Areas.Connect.Controllers
{
    public class HomeController : Controller
    {
        private IDesignerTasks _designerTasks;
        public HomeController(IDesignerTasks designerTasks)
        {
            _designerTasks = designerTasks;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var homeModel = new HomeModel(Url.Action("Specifications"));

            homeModel.InitialData = JsonConvert.SerializeObject(homeModel);
            return View(homeModel);
        }
    }
}