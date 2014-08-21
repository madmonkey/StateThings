using Designer.Tasks;
using StateInterface.Controllers;
using StateInterface.Designer.Model;
using System.Web.Mvc;

namespace StateInterface.Areas.Certify.Controllers
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
    }
}
