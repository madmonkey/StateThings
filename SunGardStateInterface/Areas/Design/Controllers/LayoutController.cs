// ReSharper disable once CheckNamespace
using Designer.Tasks;
using StateInterface.Areas.Design.Models;
using StateInterface.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace StateInterface.Areas.Design.Controllers
{
    [Authorize]
    public class LayoutController : StateConnectContollerBase
    {
        public LayoutController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Preview(string recordsCenterName, string formId)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name == recordsCenterName);

            ViewBag.Title = string.Format("{0} - {1} Preview", recordsCenter != null ? recordsCenter.Name : "Not Found", formId);

            if (recordsCenter != null)
            {
                var requestForm = _designerTasks.GetForm(new TaskParameter<FormById>(User.Identity.Name, new FormById(recordsCenter.Id, formId)));//recordsCenter.Id, formId

                return View(new ControlsModel(requestForm).Controls);
            }
            return View(); //
        }
    }
}