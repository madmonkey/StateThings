using Designer.Tasks;
using ServiceStack.Text;
using StateInterface.Areas.Connect.Models;
using StateInterface.Controllers;
using StateInterface.Models;
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
            homeModel.InitialData = JsonSerializer.SerializeToString(homeModel);
            return View(new ResponseModel<HomeModel>(homeModel));
        }
    }
}