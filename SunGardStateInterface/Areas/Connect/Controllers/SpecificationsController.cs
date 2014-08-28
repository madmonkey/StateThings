using Designer.Tasks;
using ServiceStack.Text;
using StateInterface.Areas.Connect.Models;
using StateInterface.Controllers;
using StateInterface.Designer.Model;
using StateInterface.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StateInterface.Areas.Connect.Controllers
{
    [Authorize]
    public class SpecificationsController : StateConnectContollerBase
    {
        public SpecificationsController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordsCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            var specificationsModel = new SpecificationsModel(recordsCenters, Url.Action("GetForms"));
            specificationsModel.InitialData = JsonSerializer.SerializeToString(specificationsModel);
            return View(new ResponseModel<SpecificationsModel>(specificationsModel));
        }
        [HttpPost]
        public ActionResult GetForms(FormsRequestParametersModel formsRequest)
        {
            var categories = _designerTasks.GetCategories(User.Identity.Name);
            var recordsCenter = _designerTasks.GetRecordsCenters(User.Identity.Name).FirstOrDefault(x => x.Id == formsRequest.RecordsCenterId);
            var formProjections = _designerTasks.GetFormProjections(User.Identity.Name, formsRequest.RecordsCenterId);
            List<CategoryModel> categoryModels = new List<CategoryModel>();
            foreach (var category in categories.OrderBy(x => x.Name))
            {
                var forms = formProjections.Where(x => x.Categories.Any(y => y.Name == category.Name));
                if (forms.Any())
                {
                    categoryModels.Add(new CategoryModel(
                        category,
                        forms,
                        string.Format("{0}/{1}", Url.Action("Details", "Form", new { area = "Design" }), recordsCenter.Name)
                    ));
                }
            }
            var uncategorizedForms = formProjections.Where(x => !x.Categories.Any());
            if (uncategorizedForms.Any())
            {
                categoryModels.Add(new CategoryModel(
                        "Uncategorized",
                        uncategorizedForms,
                        string.Format("{0}/{1}", Url.Action("Details", "Form", new { area = "Design" }), recordsCenter.Name)
                    ));
            }
            return Json(new ResponseModel<List<CategoryModel>>(categoryModels));
        }
    }
}