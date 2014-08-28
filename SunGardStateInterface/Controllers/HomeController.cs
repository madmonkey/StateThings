using Designer.Tasks;
using StateInterface.Designer.Model;
using StateInterface.Models;
using SunGardStateInterface.Models;
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
            _designerTasks.SetRecordsCenterForUser(User.Identity.Name, model.RecordsCenterName);
            return Json(new ResponseModel<RecordsCenterParametersModel>(model));
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
