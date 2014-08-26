using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using StateInterface.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateInterface.Areas.Design.Controllers
{
    [Authorize]
    public class HomeController : StateConnectContollerBase
    {
        public HomeController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {

        }
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            var user = _designerTasks.GetUser(User.Identity.Name);

            var homeModel = new HomeModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    GetFormUrl = Url.Action("Index", "Form")
                };

            homeModel.InitialData = JsonConvert.SerializeObject(homeModel);

            ViewBag.Title = "Designer Home";
            return View(homeModel);
        }
    }
}
