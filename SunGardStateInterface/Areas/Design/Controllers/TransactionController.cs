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
                    SnippetDetailsUrl = Url.Action("Details"),
                    CreateSnippetUrl = Url.Action("CreateSnippet"),
                    DeleteSnippetUrl = Url.Action("DeleteSnippet"),
                    CanDesignManage = user.CanDesignManage
                };

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "Transaction Design";
            return View(model);
        }
        [HttpPost]
        public ActionResult GetSnippets(SnippetsRequestModel request)
        {
            if (request == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
            }
            request.Validate();

            List<CatalogItemModel> transactionSnippets = getCatalogItemModels(request.RecordsCenterName);
            
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
                    catalogItems.Add(new CatalogItemModel()
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
                    var transactionSnippet = new TransactionSnippetModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues())
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
        public ActionResult CreateSnippet(SnippetRequestModel snippetRequest)
        {
            return CreateOrUpdateSnippet(snippetRequest, false);
        }
        [HttpPost]
        public ActionResult UpdateSnippet(SnippetRequestModel snippetRequest)
        {
            return CreateOrUpdateSnippet(snippetRequest);
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
                var transactionSnippet = new TransactionSnippetModel(_designerTasks.DeleteTransactionSnippet(User.Identity.Name, snippetId), TransactionSnippetFieldTypeHelper.TypeValues());
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
                var transactionSnippet = new TransactionSnippetModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues());
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
                    var snippet = _designerTasks.UpdateTransactionSnippetField(User.Identity.Name, snippetFieldRequest.SnippetId, snippetFieldRequest.ToDomainModel());
                    var transactionSnippet = new TransactionSnippetModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues());
                    setProperties(transactionSnippet, snippetFieldRequest.RecordsCenterName, user);
                    transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                    return Json(transactionSnippet);
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

        private ActionResult CreateOrUpdateSnippet(SnippetRequestModel snippetRequest, bool updateExisting = true)
        {
            User user = _designerTasks.GetUser(User.Identity.Name);
            if (user.CanDesignManage)
            {
                if (snippetRequest == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }
                
                snippetRequest.Validate(user);
                TransactionSnippet snippet = _designerTasks.GetTransactionSnippet(User.Identity.Name, snippetRequest.Id);
                if (updateExisting && snippet == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.SnippetNotFound);
                }
                if (!updateExisting)
                {
                    snippet = new TransactionSnippet
                        {
                            Id = snippetRequest.Id,
                            RecordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, snippetRequest.RecordsCenterName),
                            Created = DateTime.UtcNow
                        };
                }
                snippet.TokenName = snippetRequest.Name;
                snippet.Description = snippetRequest.Description;
                snippet.TransactionDefinition = snippetRequest.Definition;
                snippet.Criteria = snippetRequest.Criteria;
                snippet.IncludePrefixAndSuffix = snippetRequest.IncludePrefixAndSuffix;

                var transactionSnippet = new TransactionSnippetModel(_designerTasks.UpdateTransactionSnippet(User.Identity.Name, snippet), TransactionSnippetFieldTypeHelper.TypeValues());
                setProperties(transactionSnippet, snippetRequest.RecordsCenterName, user);
                transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                return Json(transactionSnippet);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }

        private void setProperties(TransactionSnippetModel snippet, string recordsCenterName, User user)
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