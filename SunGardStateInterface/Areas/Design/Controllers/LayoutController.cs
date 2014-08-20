// ReSharper disable once CheckNamespace
namespace StateInterface.Areas.Design.Controllers
{
    using StateInterface.Areas.Design.Models;
    using StateInterface.Designer.Model;
    using System.Linq;
    using System.Web.Mvc;

    public class LayoutController : Controller
    {
        private readonly IDesignerTasks _designerTasks;

        public LayoutController(IDesignerTasks designerTasks)
        {
            this._designerTasks = designerTasks;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Preview(string recordsCenterName, string formId)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters().FirstOrDefault(x => x.Name == recordsCenterName);

            ViewBag.Title = string.Format("{0} - {1} Preview", recordsCenter != null ? recordsCenter.Name : "Not Found", formId);

            if (recordsCenter != null)
            {
                var requestForm = _designerTasks.GetForm(recordsCenter.Id, formId);

                return View(new ControlsModel(requestForm).Controls);
            }
            return View(); //
        }
    }
}