using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using StateInterface.Controllers;
using StateInterface.Designer.Model;
using StateInterface.Properties;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StateInterface.Areas.Design.Controllers
{
    public class TransactionController : StateConnectContollerBase
    {
        public TransactionController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }

        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Title = "Transaction Snippet Help";
            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            User user = _designerTasks.GetUser(User.Identity.Name);

            var model = new TransactionSnippetCatalogModel(user, recordCenters)
                {
                    RecordsCenterSelector = { SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }) },
                    CatalogItems = getCatalogItemModels(user.CurrentRecordsCenter.Name),
                    DesignHomeUrl = Url.Action("Index", "Home"),
                    GetSnippetsUrl = Url.Action("GetSnippets"),
                    CreateSnippetUrl = Url.Action("CreateSnippet"),
                    DeleteSnippetUrl = Url.Action("DeleteSnippet"),
                    CanDesignManage = user.CanDesignManage
                };

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "Transaction Design";
            return View(model);
        }
        [HttpPost]
        public ActionResult GetSnippets(SnippetsParametersModel parameters)
        {
            if (parameters == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
            }
            parameters.Validate();

            List<CatalogItemModel> transactionSnippets = getCatalogItemModels(parameters.RecordsCenterName);
            
            return Json(transactionSnippets);
        }

        private List<CatalogItemModel> getCatalogItemModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, recordsCenterName);
            if (recordsCenter != null)
            {
                var snippets = _designerTasks.GetTransactionSnippets(User.Identity.Name, recordsCenter.Id);
                var catalogItems = new List<CatalogItemModel>();
                foreach (var snippet in snippets)
                {
                    catalogItems.Add(new CatalogItemModel
                        {
                            Name = snippet.TokenName,
                            Description = snippet.Description,
                            DetailsUrl = string.Format("{0}/{1}/{2}", Url.Action("Details"), recordsCenter.Name, snippet.TokenName)
                        });
                }
                return catalogItems; 
            }

            throw new StateInterfaceParameterValidationException(Resources.RecordsCenterNotFound);
        }
        [HttpGet]
        public ActionResult Details(string recordsCenterName, string tokenName)
        {
            if ((string.IsNullOrWhiteSpace(recordsCenterName)) || (string.IsNullOrWhiteSpace(tokenName)))
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
            }
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, recordsCenterName);
            if (recordsCenter != null)
            {
                var snippet = _designerTasks.GetTransactionSnippet(User.Identity.Name, recordsCenter.Id, tokenName);
                if (snippet != null)
                {
                    var transactionSnippet = new TransactionSnippetDetailsModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues())
                    {
                        RecordsCenterName = recordsCenter.Name,
                        DesignHomeUrl = Url.Action("Index", "Home"),
                        TransactionsHomeUrl = Url.Action("Index")
                    };

                    var user = _designerTasks.GetUser(User.Identity.Name);
                    setProperties(transactionSnippet, recordsCenter.Name, user);
                    transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);

                    ViewBag.Title = string.Format("{0} - {1}", transactionSnippet.TokenName, transactionSnippet.RecordsCenterName);
                    return View(transactionSnippet);
                }
                throw new StateInterfaceParameterValidationException(Resources.SnippetNotFound);
            }
            throw new StateInterfaceParameterValidationException(Resources.RecordsCenterNotFound);
        }
        [HttpPost]
        public ActionResult CreateSnippet(SnippetParametersModel snippetParameters)
        {
            User user = _designerTasks.GetUser(User.Identity.Name);
            if (user.CanDesignManage)
            {
                if (snippetParameters == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }

                snippetParameters.Validate(user);
                var snippet = new TransactionSnippet
                    {
                        Id = snippetParameters.Id,
                        RecordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, snippetParameters.RecordsCenterName),
                        Created = DateTime.UtcNow,
                        TokenName = snippetParameters.Name,
                        Description = snippetParameters.Description
                    };

                var transactionSnippet = new TransactionSnippetDetailsModel(_designerTasks.UpdateTransactionSnippet(User.Identity.Name, snippet), TransactionSnippetFieldTypeHelper.TypeValues());
                var catalogItem = new CatalogItemModel
                {
                    Name = transactionSnippet.TokenName,
                    Description = transactionSnippet.Description,
                    DetailsUrl = string.Format("{0}/{1}/{2}", Url.Action("Details"), user.CurrentRecordsCenter.Name, transactionSnippet.TokenName)
                };

                return Json(catalogItem);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }
        [HttpPost]
        public ActionResult UpdateSnippet(SnippetParametersModel snippetParameters)
        {
            User user = _designerTasks.GetUser(User.Identity.Name);
            if (user.CanDesignManage)
            {
                if (snippetParameters == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }

                snippetParameters.Validate(user);
                var snippet = _designerTasks.GetTransactionSnippet(User.Identity.Name, snippetParameters.Id);
                if (snippet == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.SnippetNotFound);
                }
                snippet.TokenName = snippetParameters.Name;
                snippet.Description = snippetParameters.Description;
                snippet.TransactionDefinition = snippetParameters.Definition;
                snippet.Criteria = snippetParameters.Criteria;
                snippet.IncludePrefixAndSuffix = snippetParameters.IncludePrefixAndSuffix;
                var transactionSnippet = new TransactionSnippetDetailsModel(_designerTasks.UpdateTransactionSnippet(User.Identity.Name, snippet), TransactionSnippetFieldTypeHelper.TypeValues());
                setProperties(transactionSnippet, snippetParameters.RecordsCenterName, user);
                transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                return Json(transactionSnippet);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }
        [HttpPost]
        public ActionResult CreateSnippetField(TransactionSnippetFieldModel snippetFieldRequest)
        {
            return CreateOrUpdateSnippetField(snippetFieldRequest);
        }
        [HttpPost]
        public ActionResult UpdateSnippetField(TransactionSnippetFieldModel snippetFieldRequest)
        {
            return CreateOrUpdateSnippetField(snippetFieldRequest);
        }
        [HttpPost]
        public ActionResult DeleteSnippet(int snippetId)
        {
            var user = _designerTasks.GetUser(User.Identity.Name);
            if (user.CanDesignManage)
            {
                if (snippetId <= 0)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterInvalid);
                }
                var transactionSnippet = new TransactionSnippetDetailsModel(_designerTasks.DeleteTransactionSnippet(User.Identity.Name, snippetId), TransactionSnippetFieldTypeHelper.TypeValues());
                setProperties(transactionSnippet, string.Empty, user);
                transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                return Json(transactionSnippet);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }
        [HttpPost]
        public ActionResult DeleteSnippetField(TransactionSnippetFieldModel snippetFieldRequest)
        {
            User user = _designerTasks.GetUser(User.Identity.Name);
            if (user.CanDesignManage)
            {
                if (snippetFieldRequest == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }
                var snippet = _designerTasks.DeleteTransactionSnippetField(User.Identity.Name, snippetFieldRequest.SnippetId, snippetFieldRequest.Id);
                var transactionSnippet = new TransactionSnippetDetailsModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues());
                setProperties(transactionSnippet, snippetFieldRequest.RecordsCenterName, user);
                transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                return Json(transactionSnippet);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }

        private ActionResult CreateOrUpdateSnippetField(TransactionSnippetFieldModel snippetFieldRequest)
        {
            User user = _designerTasks.GetUser(User.Identity.Name);
            if (user.CanDesignManage)
            {
                if (snippetFieldRequest == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }
                snippetFieldRequest.Validate();
                try
                {
                    var transactionSnippet = _designerTasks.UpdateTransactionSnippetField(User.Identity.Name, snippetFieldRequest.SnippetId, snippetFieldRequest.ToDomainModel());
                    var transactionSnippetModel = new TransactionSnippetDetailsModel(transactionSnippet, TransactionSnippetFieldTypeHelper.TypeValues());
                    setProperties(transactionSnippetModel, snippetFieldRequest.RecordsCenterName, user);
                    transactionSnippetModel.InitialData = JsonConvert.SerializeObject(transactionSnippet);

                    return Json(transactionSnippetModel);
                }
                catch (ArgumentException)
                {
                    throw new StateInterfaceParameterValidationException(Resources.SnippetFieldNameAlreadyExists);
                }
                catch (KeyNotFoundException)
                {
                    throw new StateInterfaceParameterValidationException(Resources.SnippetNotFound);
                }
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }

        private void setProperties(TransactionSnippetDetailsModel snippet, string recordsCenterName, User user)
        {
            snippet.RecordsCenterName = recordsCenterName;
            snippet.CanDesignManage = user.CanDesignManage;            
            snippet.SnippetHelpUrl = Url.Action("Help");
            snippet.UpdateSnippetUrl = Url.Action("UpdateSnippet");
            snippet.UpdateSnippetFieldUrl = Url.Action("UpdateSnippetField");
            snippet.DeleteSnippetFieldUrl = Url.Action("DeleteSnippetField");
            snippet.DesignHomeUrl = Url.Action("Index", "Home");
            snippet.TransactionsHomeUrl = Url.Action("Index");
        }
    }
}