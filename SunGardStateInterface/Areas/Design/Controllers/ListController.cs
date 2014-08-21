using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StateInterface.Designer.Model;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using Designer.Tasks;
using StateInterface.Properties;

namespace StateInterface.Areas.Design.Controllers
{
    public class ListController : Controller
    {
        private IDesignerTasks _designerTasks;
        public ListController(IDesignerTasks designerTasks)
        {
            _designerTasks = designerTasks;
        }
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));

            var model = new ListModel(user, recordCenters);
            model.GetListsUrl = Url.Action("GetLists");
            model.ListDetailsUrl = Url.Action("Details");
            model.Lists = getListModels(user.CurrentRecordsCenter.Name);
            model.RecordsCenterSelector.SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" });
            model.DesignHomeUrl = Url.Action("Index", "Home");

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "List Design";
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string recordsCenterName, string listName)
        {
            User user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));

            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName, StringComparison.CurrentCultureIgnoreCase));

            var list = _designerTasks.GetList(new TaskParameter<ListByName>(User.Identity.Name, new ListByName(recordsCenter.Id, listName)));//recordsCenter.Id, listName
            var formFieldsUsing = _designerTasks.GetFormFieldProjectionsUsingOptionList(new TaskParameter<OptionList>(User.Identity.Name, list));
            var listModel = new OptionListModel(list, formFieldsUsing, Url.Action("Details", "Form"));
            listModel.DesignHomeUrl = Url.Action("Index", "Home");
            listModel.ListsHomeUrl = Url.Action("Index");

            listModel.CanDesignManage = user.CanDesignManage;
            listModel.InitialData = JsonConvert.SerializeObject(listModel);

            ViewBag.Title = string.Format("{0} - {1}", listModel.ListName, listModel.RecordsCenterName);
            return View(listModel);
        }

        [HttpPost]
        public ActionResult GetLists(GetListsParametersModel parameters)
        {
            if (parameters == null || string.IsNullOrWhiteSpace(parameters.RecordsCenterName))
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }

            List<ListCatalogProjectionModel> listModels = getListModels(parameters.RecordsCenterName);

            return Json(listModels);
        }

        private List<ListCatalogProjectionModel> getListModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName));
            var lists = _designerTasks.GetListProjections(new TaskParameter<RecordsCenterId>(User.Identity.Name) { Parameters = new RecordsCenterId(recordsCenter.Id) });

            List<ListCatalogProjectionModel> listModels = new List<ListCatalogProjectionModel>();
            foreach (var list in lists)
            {
                listModels.Add(new ListCatalogProjectionModel(list, Url.Action("Details") + "/" + recordsCenter.Name));
            }
            return listModels;
        }
    }
}