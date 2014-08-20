using System.Web.Routing;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StateInterface.Properties;
using Designer.Tasks;


namespace StateInterface.Areas.Design.Controllers
{
    public class FormController : Controller
    {
        private IDesignerTasks _designerTasks;
        public FormController(IDesignerTasks designerTasks)
        {
            _designerTasks = designerTasks;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var user = _designerTasks.GetUser(User.Identity.Name);

            var model = new FormModel(user, recordCenters);
            model.GetFormsUrl = Url.Action("GetForms");
            model.FormDetailsUrl = Url.Action("Details");
            model.DesignHomeUrl = Url.Action("Index", "Home");
            model.RequestForms = getRequestFormModels(user.CurrentRecordsCenter.Name);
            model.RecordsCenterSelector.SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" });

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "Form Design";
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(string recordsCenterName, string formId)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName, StringComparison.CurrentCultureIgnoreCase));

            var requestForm = _designerTasks.GetForm(recordsCenter.Id, formId);

            var availableApplications = _designerTasks.GetApplications();
            var formModel = new RequestFormModel(requestForm, Url.Action("Preview", "Layout"), Url.Action("Details", "List"), Url.Action("Details", "Field"), availableApplications);

            User user = _designerTasks.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
            formModel.CanDesignManage = user.CanDesignManage;
            formModel.UpdateApplicationsAssociationUrl = Url.Action("UpdateFormApplications", new { });
            formModel.DesignHomeUrl = Url.Action("Index", "Home");
            formModel.FormsHomeUrl = Url.Action("Index");

            formModel.InitialData = JsonConvert.SerializeObject(formModel);

            ViewBag.Title = string.Format("{0} - {1}", formModel.FormId, formModel.RecordsCenterName);
            return View(formModel);
        }
        [HttpPost]
        public ActionResult GetForms(GetFormsParametersModel parameters)
        {
            if (parameters == null || string.IsNullOrWhiteSpace(parameters.RecordsCenterName))
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }

            List<RequestFormCatalogProjectionModel> requestFormModels = getRequestFormModels(parameters.RecordsCenterName);

            return Json(requestFormModels);
        }
        [HttpGet]
        public ActionResult Help()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateFormApplications(PostApplicationParametersModel parameters)
        {
            //todo: unauthorized :: (elmah)
            if (_designerTasks.GetUser(System.Web.HttpContext.Current.User.Identity.Name).CanDesignManage)
            {
                if (parameters != null)
                {
                    parameters.Validate();
                    var requestForm = _designerTasks.GetForm(parameters.RecordsCenterId, parameters.FormId);
                    if (requestForm != null)
                    {
                        requestForm.Applications.Clear();
                        foreach (var application in parameters.Applications)
                        {
                            if (application.IsSelected)
                            {
                                requestForm.Applications.Add(_designerTasks.GetApplications().Where(x => x.Id == application.Id).FirstOrDefault());
                            }
                        }
                        return Json(new PostApplicationParametersModel(_designerTasks.UpdateRequestForm(requestForm), _designerTasks.GetApplications()));
                    }
                    throw new ApplicationException(Resources.ApplicationAssociationNotFound);
                }
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized)); 
            
        }
        private List<RequestFormCatalogProjectionModel> getRequestFormModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName));
            var requestForms = _designerTasks.GetFormProjections(new TaskParameter<RecordsCenterId>(User.Identity.Name){Parameters = new RecordsCenterId(recordsCenter.Id)});

            List<RequestFormCatalogProjectionModel> requestFormModels = new List<RequestFormCatalogProjectionModel>();
            foreach (var requestForm in requestForms)
            {
                requestFormModels.Add(new RequestFormCatalogProjectionModel(requestForm, Url.Action("Details") + "/" + recordsCenter.Name));
            }
            return requestFormModels;
        }
    }
}