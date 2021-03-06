﻿using StateInterface.Areas.Design.Models;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StateInterface.Properties;
using Designer.Tasks;
using StateInterface.Controllers;
using StateInterface.Designer;
using ServiceStack.Text;
using StateInterface.Models;

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
            var recordCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            var user = _designerTasks.GetUser(User.Identity.Name);
            var model = new RequestFormCatalogModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    CatalogItems = getCatalogItemModels(user.CurrentRecordsCenter.Name),
                    GetFormsUrl = Url.Action("GetForms"),
                    DesignHomeUrl = Url.Action("Index", "Home"),
                };

            model.InitialData = JsonSerializer.SerializeToString(model);
            ViewBag.Title = "Form Design";
            return View(new ResponseModel<RequestFormCatalogModel>(model));
        }
        [HttpGet]
        public ActionResult Details(string recordsCenterName, string formId)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters(User.Identity.Name).FirstOrDefault(x => x.Name.Equals(recordsCenterName, StringComparison.CurrentCultureIgnoreCase));
            if (recordsCenter != null)
            {
                var requestForm = _designerTasks.GetForm(User.Identity.Name, recordsCenter.Id, formId);
                var availableApplications = _designerTasks.GetApplications(User.Identity.Name);
                var availableCategories = _designerTasks.GetCategories(User.Identity.Name);
                User user = _designerTasks.GetUser(User.Identity.Name);
                var formModel = new RequestFormDetailsModel(requestForm, Url.Action("Details", "List"), Url.Action("Details", "Field"), availableApplications, availableCategories)
                {
                    FormHelpUrl = Url.Action("Help"),
                    PreviewFormUrl = string.Format("{0}/{1}/{2}", Url.Action("Preview", "Layout"), requestForm.RecordsCenter.Name, requestForm.FormId),
                    CanDesignManage = user.CanDesignManage,
                    UpdateApplicationsAssociationUrl = Url.Action("UpdateFormApplications", new { }),
                    UpdateCategoriesAssociationUrl = Url.Action("UpdateFormCategories", new { }),
                    DesignHomeUrl = Url.Action("Index", "Home"),
                    FormsHomeUrl = Url.Action("Index")
                };

                formModel.InitialData = JsonSerializer.SerializeToString(formModel);
                ViewBag.Title = string.Format("{0} - {1}", formModel.FormId, formModel.RecordsCenterName);
                return View(new ResponseModel<RequestFormDetailsModel>(formModel));
            }
            throw new ViewModelValidationException(Resources.RecordsCenterInvalid);
        }
        [HttpPost]
        public ActionResult GetForms(FormsParametersModel parameters)
        {
            if (parameters == null || string.IsNullOrWhiteSpace(parameters.RecordsCenterName))
            {
                throw new ViewModelValidationException(Resources.ParameterInvalid);
            }
            List<CatalogItemModel> requestFormModels = getCatalogItemModels(parameters.RecordsCenterName);
            return Json(new ResponseModel<List<CatalogItemModel>>(requestFormModels));
        }
        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Title = "Form Help";
            return View();
        }
        [HttpPost]
        public ActionResult UpdateFormApplications(UpdateFormApplicationsModel model)
        {
            if (model != null)
            {
                model.Validate(_designerTasks.GetUser(User.Identity.Name), false);
                var selectedApplicationIds = model.Applications.Where(x => x.IsSelected == true).Select(x => x.Id);
                var requestForm = _designerTasks.UpdateRequestFormApplications(User.Identity.Name,
                    model.RecordsCenterName, model.FormId, selectedApplicationIds);
                var applications = _designerTasks.GetApplications(User.Identity.Name);
                model = new UpdateFormApplicationsModel(requestForm, applications);
                return Json(new ResponseModel<UpdateFormApplicationsModel>(model));
            }
            throw new ViewModelValidationException(Resources.ParameterInvalid);
        }
        public ActionResult UpdateFormCategories(UpdateRequestFormCategoriesModel model)
        {
            if (model != null)
            {
                model.Validate(_designerTasks.GetUser(User.Identity.Name), false);
                var selectedCategoryIds = model.Categories.Where(x => x.IsSelected == true).Select(x => x.Id);
                var requestForm = _designerTasks.UpdateRequestFormCategrories(User.Identity.Name, model.RecordsCenterName, model.FormId, selectedCategoryIds);
                var categories = _designerTasks.GetCategories(User.Identity.Name);
                model = new UpdateRequestFormCategoriesModel(requestForm, categories);
                return Json(new ResponseModel<UpdateRequestFormCategoriesModel>(model));
            }
            throw new ViewModelValidationException(Resources.ParameterInvalid);
        }

        private List<CatalogItemModel> getCatalogItemModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, recordsCenterName);
            if (recordsCenter != null)
            {
                var requestForms = _designerTasks.GetFormProjections(User.Identity.Name, recordsCenter.Id);
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

            throw new ObjectNotFoundException(string.Format(Resources.RecordsCenterNotFound,recordsCenterName.ToUpper()));
        }
    }
}