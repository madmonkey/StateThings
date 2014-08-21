using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Connect.Models;
using StateInterface.Controllers;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var recordsCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));

            var specificationsModel = new SpecificationsModel(recordsCenters, Url.Action("GetForms"));

            specificationsModel.InitialData = JsonConvert.SerializeObject(specificationsModel);
            return View(specificationsModel);
        }
        [HttpPost]
        public ActionResult GetForms(FormsRequestParametersModel formsRequest)
        {
            var categories = _designerTasks.GetCategories(new TaskParameter(User.Identity.Name));
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Id == formsRequest.RecordsCenterId);
            var formProjections = _designerTasks.GetFormProjections(new TaskParameter<RecordsCenterId>(User.Identity.Name) { Parameters = new RecordsCenterId(formsRequest.RecordsCenterId) });

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

            return Json(categoryModels);
        }
    }
}