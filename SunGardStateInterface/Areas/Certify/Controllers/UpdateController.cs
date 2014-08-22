﻿using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Certify.Models;
using StateInterface.Areas.Design;
using StateInterface.Controllers;
using StateInterface.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StateInterface.Areas.Certify.Controllers
{
    [Authorize]
    public class UpdateController : StateConnectContollerBase
    {
        public UpdateController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordsCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            var categories = _designerTasks.GetCategories(User.Identity.Name);
            var user = _designerTasks.GetUser(User.Identity.Name);

            var model = new CertifyUpdateModel(user, recordsCenters, categories)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    GetFormsUrl = Url.Action("GetForms")
                };

            model.InitialData = JsonConvert.SerializeObject(model);
            ViewBag.Title = string.Format("Certification Update - {0}", user.CurrentRecordsCenter.Name);

            return View(model);
        }
        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Title = "Certification Help";
            return View();
        }
        [HttpGet]
        public ActionResult UpdateForm(string recordsCenter, string formId)
        {
            var rc = _designerTasks.GetRecordsCenters(User.Identity.Name).FirstOrDefault(x => x.Name.Equals(recordsCenter, StringComparison.CurrentCultureIgnoreCase));

            var requestForm = _designerTasks.GetForm(User.Identity.Name, rc.Id, formId);

            var model = new CertifyUpdateFormModel(requestForm, Url.Action("Details", "Form", new { area = "Design" }), Url.Action("UpdateForm"))
                {
                    ResetTestCaseUrl = Url.Action("ResetTestCase"),
                    UpdateTestCaseUrl = Url.Action("UpdateTestCase"),
                    GetFormQAStateUrl = Url.Action("GetFormQAState")
                };

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = string.Format("{0} Certification - {1}", formId, rc.Name);

            return View(model);
        }
        [HttpPost]
        public ActionResult GetFormQAState(RequestFormRequestModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
                }

                var requestForm = _designerTasks.GetForm(User.Identity.Name, model.RecordsCenterId, model.FormId);

                var qaStatusModel = new QAStatusModel(requestForm);

                return Json(qaStatusModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult GetForms(CertifyUpdateParametersModel model)
        {
            if (model == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
            }

            model.Validate();

            var certifyApplicationModels = new List<CertifyApplicationModel>();

            var applications = _designerTasks.GetApplications(User.Identity.Name);
            var requestForms = _designerTasks.GetForms(User.Identity.Name, model.RecordsCenterName, model.CategoryId);

            foreach (var requestForm in requestForms)
            {
                requestForm.GenerateTestCases();
            }

            foreach (var application in applications)
            {
                var certifyApplicationModel = new CertifyApplicationModel(model.RecordsCenterName, requestForms, application, Url.Action("Details", "Form", new { area = "Design" }), Url.Action("UpdateForm"));

                if (certifyApplicationModel.Forms.Any())
                {
                    certifyApplicationModels.Add(certifyApplicationModel);
                }
            }

            return Json(certifyApplicationModels); 
        }
        [HttpPost]
        public ActionResult UpdateTestCase(TestCaseEntryModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
                }

                model.Validate();

                //var testCase = _designerTasks.UpdateTestCase(model.CriteriaId, model.TestCaseId, DateTime.UtcNow, model.Note, User.Identity.Name, model.HasPassed);

                var testCase = _designerTasks.UpdateTestCase(User.Identity.Name, model.CriteriaId, model.TestCaseId, DateTime.UtcNow, model.Note, model.HasPassed);

                var requestForm = testCase.Criteria.Transaction.RequestForm;

                var requestFormModel = new CertifyUpdateRequestFormModel(requestForm, Url.Action("Details", "Form", new { area = "Design" }), Url.Action("UpdateForm"));

                return Json(requestFormModel);
            }
            catch (StateInterfaceParameterValidationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult ResetTestCase(TestCaseEntryModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
                }

                model.Validate();

                //var testCase = _designerTasks.ResetTestCase(model.CriteriaId, model.TestCaseId, DateTime.UtcNow, model.Note, User.Identity.Name);

                var testCase = _designerTasks.ResetTestCase(User.Identity.Name, model.CriteriaId, model.TestCaseId, DateTime.UtcNow, model.Note);

                var requestForm = testCase.Criteria.Transaction.RequestForm;

                var requestFormModel = new CertifyUpdateRequestFormModel(requestForm, Url.Action("Details", "Form", new { area = "Design" }), Url.Action("UpdateForm"));

                return Json(requestFormModel);
            }
            catch (StateInterfaceParameterValidationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}