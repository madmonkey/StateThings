using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using Designer.Tasks;
using StateInterface.Properties;
using StateInterface.Designer.Model;
using StateInterface.Controllers;

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
            var recordCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));

            var model = new ListCatalogModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    GetListsUrl = Url.Action("GetLists"),
                    ListDetailsUrl = Url.Action("Details"),
                    CatalogItems = getCatalogItemModels(user.CurrentRecordsCenter.Name),
                    DesignHomeUrl = Url.Action("Index", "Home")
                };

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "List Design";
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string recordsCenterName, string listName)
        {
            var user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));

            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName, StringComparison.CurrentCultureIgnoreCase));

            var list = _designerTasks.GetList(new TaskParameter<ListByName>(User.Identity.Name, new ListByName(recordsCenter.Id, listName)));//recordsCenter.Id, listName
            var formFieldsUsing = _designerTasks.GetFormFieldProjectionsUsingOptionList(new TaskParameter<OptionList>(User.Identity.Name, list));
            var listModel = new OptionListModel(list, formFieldsUsing, Url.Action("Details", "Form"));
            listModel.DesignHomeUrl = Url.Action("Index", "Home");
            listModel.ListsHomeUrl = Url.Action("Index");

            listModel.InitialData = JsonConvert.SerializeObject(listModel);

            ViewBag.Title = string.Format("{0} - {1}", listModel.ListName, listModel.RecordsCenterName);
            return View(listModel);
        }

        [HttpPost]
        public ActionResult GetLists(ListsRequestModel request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RecordsCenterName))
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }

            List<CatalogItemModel> listModels = getCatalogItemModels(request.RecordsCenterName);

            return Json(listModels);
        }

        private List<CatalogItemModel> getCatalogItemModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName));
            if (recordsCenter != null)
            {
                var lists = _designerTasks.GetListProjections(new TaskParameter<RecordsCenterId>(User.Identity.Name) { Parameters = new RecordsCenterId(recordsCenter.Id) });
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
            throw new StateInterfaceParameterValidationException(Resources.RecordsCenterNotFound);
        }
    }
}