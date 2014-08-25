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
            var recordCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            var user = _designerTasks.GetUser(User.Identity.Name);
            var model = new FieldCatalogModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    CatalogItems = getFieldModels(user.CurrentRecordsCenter.Name),
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
            var field = _designerTasks.GetField(User.Identity.Name, recordsCenterName, tagName);
            var formsUsing = _designerTasks.GetFormProjectionsUsingField(User.Identity.Name, field);
            var model = new FieldDetailsModel(field, formsUsing, Url.Action("Details", "Form"));
            model.DesignHomeUrl = Url.Action("Index", "Home");
            model.FieldsHomeUrl = Url.Action("Index");

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = string.Format("{0} - {1}", field.TagName, field.RecordsCenter.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetFields(FieldsParametersModel parameters)
        {
            if (parameters == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParentIdInvalid);
            }

            parameters.Validate();

            List<CatalogItemModel> fieldCatalogItemModels = getFieldModels(parameters.RecordsCenterName);

            return Json(fieldCatalogItemModels);
        }
        private List<CatalogItemModel> getFieldModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name,recordsCenterName);
            if (recordsCenter != null)
            {
                var fields = _designerTasks.GetFieldCatalogItems(User.Identity.Name, recordsCenterName);
                var catalogItems = new List<CatalogItemModel>();
                foreach (var field in fields)
                {
                    catalogItems.Add(new CatalogItemModel()
                        {
                            Name = field.TagName,
                            Description = field.Description,
                            DetailsUrl = string.Format("{0}/{1}/{2}", Url.Action("Details"), recordsCenter.Name, field.TagName)
                        });
                }
                return catalogItems;
            }

            throw new StateInterfaceParameterValidationException(Resources.RecordsCenterNotFound);
        }
    }
}