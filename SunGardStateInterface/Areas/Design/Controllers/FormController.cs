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
using StateInterface.Controllers;

namespace StateInterface.Areas.Design.Controllers
{
    [Authorize]
    public class FormController : StateConnectContollerBase
    {
        public FormController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));

            var model = new FormCatalogModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    CatalogItems = getCatalogItemModels(user.CurrentRecordsCenter.Name),
                    GetFormsUrl = Url.Action("GetForms"),
                    FormDetailsUrl = Url.Action("Details"),
                    DesignHomeUrl = Url.Action("Index", "Home"),
                };

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "Form Design";
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(string recordsCenterName, string formId)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName, StringComparison.CurrentCultureIgnoreCase));

            if (recordsCenter != null)
            {
                var requestForm = _designerTasks.GetForm(new TaskParameter<FormById>(User.Identity.Name, new FormById(recordsCenter.Id, formId)));
                var availableApplications = _designerTasks.GetApplications(new TaskParameter(User.Identity.Name));
                User user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));

                var formModel = new RequestFormModel(requestForm, Url.Action("Details", "List"), Url.Action("Details", "Field"), availableApplications)
                {
                    FormHelpUrl = Url.Action("Help"),
                    PreviewFormUrl = string.Format("{0}/{1}/{2}", Url.Action("Preview", "Layout"), requestForm.RecordsCenter.Name, requestForm.FormId),
                    CanDesignManage = user.CanDesignManage,
                    UpdateApplicationsAssociationUrl = Url.Action("UpdateFormApplications", new { }),
                    DesignHomeUrl = Url.Action("Index", "Home"),
                    FormsHomeUrl = Url.Action("Index")
                };

                formModel.InitialData = JsonConvert.SerializeObject(formModel);

                ViewBag.Title = string.Format("{0} - {1}", formModel.FormId, formModel.RecordsCenterName);
                return View(formModel);
            }
            throw new StateInterfaceParameterValidationException(Resources.RecordsCenterInvalid);
        }
        [HttpPost]
        public ActionResult GetForms(FormsRequestModel request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RecordsCenterName))
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }

            List<CatalogItemModel> requestFormModels = getCatalogItemModels(request.RecordsCenterName);

            return Json(requestFormModels);
        }
        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Title = "Form Help";
            return View();
        }
        [HttpPost]
        public ActionResult UpdateFormApplications(PostApplicationRequestModel request)
        {
            //todo: unauthorized :: (elmah)
            if (_designerTasks.GetUser(new TaskParameter(User.Identity.Name)).CanDesignManage)
            {
                if (request != null)
                {
                    request.Validate();
                    var recordsCenter = _designerTasks.GetRecordsCenterByName(new TaskParameter<RecordsCenterName>(User.Identity.Name, new RecordsCenterName(request.RecordsCenterName)));
                    var requestForm = _designerTasks.GetForm(new TaskParameter<FormById>(User.Identity.Name, new FormById(recordsCenter.Id, request.FormId)));
                    if (requestForm != null)
                    {
                        requestForm.Applications.Clear();
                        foreach (var application in request.Applications)
                        {
                            if (application.IsSelected)
                            {
                                requestForm.Applications.Add(_designerTasks.GetApplications(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Id == application.Id));
                            }
                        }
                        return Json(new PostApplicationRequestModel(_designerTasks.UpdateRequestForm(new TaskParameter<RequestForm>(User.Identity.Name, requestForm)),
                            _designerTasks.GetApplications(new TaskParameter(User.Identity.Name))));
                    }
                    throw new ApplicationException(Resources.ApplicationAssociationNotFound);
                }
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized));

        }
        private List<CatalogItemModel> getCatalogItemModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name)).FirstOrDefault(x => x.Name.Equals(recordsCenterName));
            if (recordsCenter != null)
            {
                var requestForms = _designerTasks.GetFormProjections(new TaskParameter<RecordsCenterId>(User.Identity.Name) { Parameters = new RecordsCenterId(recordsCenter.Id) });
                var catalogItems = new List<CatalogItemModel>();
                foreach (var requestForm in requestForms)
                {
                    catalogItems.Add(new CatalogItemModel()
                        {
                            Name = requestForm.FormId,
                            Description = requestForm.Description,
                            DetailsUrl = string.Format("{0}/{1}/{2}", Url.Action("Details"), recordsCenter.Name, requestForm.FormId)
                        });
                }
                return catalogItems;
            }

            throw new StateInterfaceParameterValidationException(Resources.RecordsCenterNotFound);
        }
    }
}