using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StateInterface.Properties;

namespace StateInterface.Areas.Design.Controllers
{
    public class FieldController : Controller
    {
        private IDesignerTasks _designerTasks;
        public FieldController(IDesignerTasks designerTasks)
        {
            _designerTasks = designerTasks;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));
            var model = new FieldCatalogModel(user, recordCenters);
            model.RecordsCenterSelector.SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" });
            model.Fields = getFieldModels(user.CurrentRecordsCenter.Name);
            model.GetFieldsUrl = Url.Action("GetFields");
            model.FieldDetailsUrl = Url.Action("Details");
            model.DesignHomeUrl = Url.Action("Index", "Home");

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
        public ActionResult GetFields(GetFieldsParametersModel parameters)
        {
            if (parameters == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParentIdInvalid);
            }

            parameters.Validate();

            List<FieldCatalogItemModel> fieldCatalogItemModels = getFieldModels(parameters.RecordsCenterName);

            return Json(fieldCatalogItemModels);
        }
        private List<FieldCatalogItemModel> getFieldModels(string recordsCenterName)
        {
            var fields = _designerTasks.GetFieldCatalogItems(new TaskParameter<RecordsCenterName>(User.Identity.Name, new RecordsCenterName(recordsCenterName)));

            List<FieldCatalogItemModel> fieldCatalogItemModels = new List<FieldCatalogItemModel>();
            foreach (var field in fields)
            {
                fieldCatalogItemModels.Add(new FieldCatalogItemModel(field, Url.Action("Details") + "/" + recordsCenterName));
            }
            return fieldCatalogItemModels;
        }
    }
}