using Designer.Tasks;
using ServiceStack.Text;
using StateInterface.Areas.Design.Models;
using StateInterface.Controllers;
using StateInterface.Designer;
using StateInterface.Designer.Model;
using StateInterface.Models;
using StateInterface.Properties;
using System.Collections.Generic;
using System.Web.Mvc;

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
                    DesignHomeUrl = Url.Action("Index", "Home")
                };

            model.InitialData = JsonSerializer.SerializeToString(model);
            ViewBag.Title = "Field Design";
            return View(new ResponseModel<FieldCatalogModel>(model));
        }

        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Title = "Field Help";
            return View();
        }

        [HttpGet]
        public ActionResult Details(string recordsCenterName, string tagName)
        {
            //todo: add validation (consider user vs. system) - recordcenter and tagname exist, msg if not
            var field = _designerTasks.GetField(User.Identity.Name, recordsCenterName, tagName);
            var formsUsing = _designerTasks.GetFormProjectionsUsingField(User.Identity.Name, field);
            var model = new FieldDetailsModel(field, formsUsing, Url.Action("Details", "Form"))
                {
                    DesignHomeUrl = Url.Action("Index", "Home"),
                    FieldsHomeUrl = Url.Action("Index"),
                    FieldHelpUrl = Url.Action("Help")
                };

            model.InitialData = JsonSerializer.SerializeToString(model);
            ViewBag.Title = string.Format("{0} - {1}", field.TagName, field.RecordsCenter.Name);
            return View(new ResponseModel<FieldDetailsModel>(model));
        }
        [HttpPost]
        public ActionResult GetFields(FieldsParametersModel parameters)
        {
            if (parameters == null)
            {
                throw new ViewModelValidationException(Resources.ParentIdInvalid);
            }
            parameters.Validate();
            List<CatalogItemModel> fieldCatalogItemModels = getFieldModels(parameters.RecordsCenterName);
            return Json(new ResponseModel<List<CatalogItemModel>>(fieldCatalogItemModels));
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
                            Description = string.IsNullOrWhiteSpace(field.Description) ? field.ToolTip : field.Description,
                            DetailsUrl = string.Format("{0}/{1}/{2}", Url.Action("Details"), recordsCenter.Name, field.TagName)
                        });
                }
                return catalogItems;
            }
            throw new ObjectNotFoundException(string.Format(Resources.RecordsCenterNotFound,recordsCenterName.ToUpper()));
        }
    }
}