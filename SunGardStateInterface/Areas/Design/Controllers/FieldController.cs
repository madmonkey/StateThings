using System.Linq;
using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using StateInterface.Properties;
using StateInterface.Designer.Model;
using StateInterface.Controllers;

namespace StateInterface.Areas.Design.Controllers
{
    [Authorize]
    public class FieldController : StateConnectContollerBase
    {
        public FieldController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));
            var model = new FieldCatalogModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    Fields = getFieldModels(user.CurrentRecordsCenter.Name),
                    GetFieldsUrl = Url.Action("GetFields"),
                    FieldDetailsUrl = Url.Action("Details"),
                    DesignHomeUrl = Url.Action("Index", "Home")
                };

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "Field Design";
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(string recordsCenterName, string tagName)
        {
            //todo: add validation (consider user vs. system) - recordcenter and tagname exist, msg if not
            var field = _designerTasks.GetField(new TaskParameter<FieldByTag>(User.Identity.Name, new FieldByTag(recordsCenterName, tagName)));//recordsCenterName, tagName
            var formsUsing = _designerTasks.GetFormProjectionsUsingField(new TaskParameter<Field>(User.Identity.Name, field));
            var model = new FieldDetailsModel(field, formsUsing, Url.Action("Details", "Form"));
            model.DesignHomeUrl = Url.Action("Index", "Home");
            model.FieldsHomeUrl = Url.Action("Index");

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = string.Format("{0} - {1}", field.TagName, field.RecordsCenter.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetFields(FieldsRequestModel request)
        {
            if (request == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParentIdInvalid);
            }

            request.Validate();

            List<FieldCatalogItemModel> fieldCatalogItemModels = getFieldModels(request.RecordsCenterName);

            return Json(fieldCatalogItemModels);
        }
        private List<FieldCatalogItemModel> getFieldModels(string recordsCenterName)
        {
            var fields = _designerTasks.GetFieldCatalogItems(new TaskParameter<RecordsCenterName>(User.Identity.Name, new RecordsCenterName(recordsCenterName)));
            return fields.Select(field => new FieldCatalogItemModel(field, Url.Action("Details") + "/" + recordsCenterName)).ToList();
        }
    }
}