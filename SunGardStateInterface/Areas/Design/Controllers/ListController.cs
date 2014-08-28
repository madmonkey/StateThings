using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StateInterface.Areas.Design.Models;
using Designer.Tasks;
using StateInterface.Properties;
using StateInterface.Designer.Model;
using StateInterface.Controllers;
using StateInterface.Designer;
using ServiceStack.Text;
using StateInterface.Models;

namespace StateInterface.Areas.Design.Controllers
{
    [Authorize]
    public class ListController : StateConnectContollerBase
    {
        public ListController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            var user = _designerTasks.GetUser(User.Identity.Name);
            var model = new OptionListCatalogModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    GetListsUrl = Url.Action("GetLists"),
                    CatalogItems = getCatalogItemModels(user.CurrentRecordsCenter.Name),
                    DesignHomeUrl = Url.Action("Index", "Home")
                };

            model.InitialData = JsonSerializer.SerializeToString(model);
            ViewBag.Title = "List Design";
            return View(new ResponseModel<OptionListCatalogModel>(model));
        }

        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Title = "List Help";
            return View();
        }

        [HttpGet]
        public ActionResult Details(string recordsCenterName, string listName)
        {
            var user = _designerTasks.GetUser(User.Identity.Name);
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, recordsCenterName);
            var list = _designerTasks.GetList(User.Identity.Name, recordsCenter.Id, listName);
            var formFieldsUsing = _designerTasks.GetFormFieldProjectionsUsingOptionList(User.Identity.Name, list);
            var listModel = new OptionListDetailsModel(list, formFieldsUsing, Url.Action("Details", "Form"))
                {
                    DesignHomeUrl = Url.Action("Index", "Home"),
                    ListsHomeUrl = Url.Action("Index"),
                    ListHelpUrl = Url.Action("Help")
                };
            listModel.InitialData = JsonSerializer.SerializeToString(listModel);
            ViewBag.Title = string.Format("{0} - {1}", listModel.ListName, listModel.RecordsCenterName);
            return View(new ResponseModel<OptionListDetailsModel>(listModel));
        }

        [HttpPost]
        public ActionResult GetLists(ListsParametersModel parameters)
        {
            if (parameters == null || string.IsNullOrWhiteSpace(parameters.RecordsCenterName))
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            List<CatalogItemModel> listModels = getCatalogItemModels(parameters.RecordsCenterName);
            return Json(new ResponseModel<List<CatalogItemModel>>(listModels));
        }

        private List<CatalogItemModel> getCatalogItemModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, recordsCenterName);
            if (recordsCenter != null)
            {
                var lists = _designerTasks.GetListProjections(User.Identity.Name,recordsCenter.Id);
                var catalogItems = new List<CatalogItemModel>();
                foreach (var list in lists)
                {
                    catalogItems.Add(new CatalogItemModel()
                        {
                            Name = list.ListName,
                            Description = string.Empty,
                            DetailsUrl = string.Format("{0}/{1}/{2}", Url.Action("Details"), recordsCenter.Name, list.ListName)
                        });
                }
                return catalogItems;
            }
            throw new ObjectNotFoundException(string.Format(Resources.RecordsCenterNotFound,recordsCenterName.ToUpper()));
        }
    }
}