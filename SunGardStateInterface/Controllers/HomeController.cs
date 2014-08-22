using Designer.Tasks;
using StateInterface.Designer.Model;
using SunGardStateInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateInterface.Controllers
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
            return View();
        }
        [HttpPost]
        public ActionResult SetRecordsCenter(RecordsCenterParametersModel model)
        {
            //User.Identity.Name, model.RecordsCenterName
            _designerTasks.SetRecordsCenterForUser(User.Identity.Name, model.RecordsCenterName);

            return Json(model);
        }
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}
