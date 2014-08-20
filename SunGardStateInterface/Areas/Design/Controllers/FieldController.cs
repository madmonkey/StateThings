using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            ViewBag.Title = "Field Design";

            var recordCenters = _designerTasks.GetRecordsCenters();
            var user = _designerTasks.GetUser(User.Identity.Name);

            var model = new FieldCatalogModel(user, recordCenters, Url.Action("GetFields"), Url.Action("Details"));
            model.RecordsCenterSelector.SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" });
            model.Fields = getFieldModels(user.CurrentRecordsCenter.Name);

            model.InitialData = JsonConvert.SerializeObject(model);

            return View(model);
        }
        [HttpGet]
        public ActionResult Details(string recordsCenterName, string tagName)
        {
            //todo: add validation (consider user vs. system) - recordcenter and tagname exist, msg if not
            var field = _designerTasks.GetField(recordsCenterName, tagName);
            var formsUsing = _designerTasks.GetFormProjectionsUsingField(field);
            var model = new FieldDetailsModel(field, formsUsing);

            ViewBag.Title = field.TagName + " - " + field.RecordsCenter.Name;

            model.InitialData = JsonConvert.SerializeObject(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetFields(GetFieldsParametersModel parameters)
        {
            if (parameters == null)
            {
                throw new StateInterfaceParameterValidationException("Invalid parameters in GetFields");
            }

            parameters.Validate();

            List<FieldCatalogItemModel> fieldCatalogItemModels = getFieldModels(parameters.RecordsCenterName);

            return Json(fieldCatalogItemModels);
        }
        private List<FieldCatalogItemModel> getFieldModels(string recordsCenterName)
        {
            var fields = _designerTasks.GetFieldCatalogItems(recordsCenterName);

            List<FieldCatalogItemModel> fieldCatalogItemModels = new List<FieldCatalogItemModel>();
            foreach (var field in fields)
            {
                fieldCatalogItemModels.Add(new FieldCatalogItemModel(field, Url.Action("Details") + "/" + recordsCenterName));
            }
            return fieldCatalogItemModels;
        }
    }
}