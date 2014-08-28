using Designer.Tasks;
using ServiceStack.Text;
using StateInterface.Areas.Design.Models;
using StateInterface.Controllers;
using StateInterface.Models;
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

            homeModel.InitialData = JsonSerializer.SerializeToString(homeModel);
            ViewBag.Title = "Designer Home";
            return View(new ResponseModel<HomeModel>(homeModel));
        }
    }
}
