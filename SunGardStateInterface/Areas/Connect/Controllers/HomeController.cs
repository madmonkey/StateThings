using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Connect.Models;
using StateInterface.Controllers;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateInterface.Areas.Connect.Controllers
{
    [Authorize]
    public class HomeController : StateConnectContollerBase
    {
        public HomeController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
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