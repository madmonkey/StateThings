using Designer.Tasks;
using StateInterface.Designer.Model;
using System.Web.Mvc;

namespace StateInterface.Areas.Certify.Controllers
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
            return View();
        }
    }
}
