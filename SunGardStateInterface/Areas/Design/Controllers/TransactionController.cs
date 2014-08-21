using Designer.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;
using StateInterface.Designer.Model;
using StateInterface.Properties;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StateInterface.Areas.Design.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IDesignerTasks _designerTasks;
        public TransactionController(IDesignerTasks designerTasks)
        {
            _designerTasks = designerTasks;
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
            var recordCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            User user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));

            var model = new SnippetModel(user, recordCenters);
            model.TransactionSnippets = getSnippetModels(user.CurrentRecordsCenter.Name);

            model.DesignHomeUrl = Url.Action("Index", "Home");
            model.GetSnippetsUrl = Url.Action("GetSnippets");
            model.SnippetDetailsUrl = Url.Action("Details");
            model.CreateSnippetUrl = Url.Action("CreateSnippet");
            model.DeleteSnippetUrl = Url.Action("DeleteSnippet");
            model.RecordsCenterSelector.SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" });
            model.CanDesignManage = user.CanDesignManage;

            model.InitialData = JsonConvert.SerializeObject(model);

            ViewBag.Title = "Transaction Design";
            return View(model);
        }
        [HttpPost]
        public ActionResult GetSnippets(SnippetsParameterModel parameter)
        {
            if (parameter == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
            }
            parameter.Validate();

            var transactionSnippets = getSnippetModels(parameter.RecordsCenterName);
            
            return Json(transactionSnippets);
        }

        private List<TransactionSnippetModel> getSnippetModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenterByName(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenterName) });
            var snippets = _designerTasks.GetTransactionSnippets(new TaskParameter<RecordsCenterId>(User.Identity.Name, new RecordsCenterId(recordsCenter.Id)));
            var transactionSnippets = new List<TransactionSnippetModel>();
            foreach (var snippet in snippets)
            {
                var snippetModel = new TransactionSnippetModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues());
                snippetModel.SnippetDetailsUrl = string.Format("{0}/{1}/{2}", Url.Action("Details"), recordsCenter.Name, snippetModel.TokenName);//String.Concat(Url.Action("Details"), "/", recordsCenter.Name, "/", snippetModel.TokenName);
                transactionSnippets.Add(snippetModel);
            }
            return transactionSnippets;
        }
        [HttpGet]
        public ActionResult Details(string recordsCenterName, string tokenName)
        {
            if ((string.IsNullOrWhiteSpace(recordsCenterName)) || (string.IsNullOrWhiteSpace(tokenName)))
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
            }
            var recordsCenter = _designerTasks.GetRecordsCenterByName(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenterName) });
            if (recordsCenter != null)
            {
                var snippet = _designerTasks.GetTransactionSnippet(new TaskParameter<SnippetFieldByToken>(User.Identity.Name,new SnippetFieldByToken(recordsCenter.Id, tokenName)));//recordsCenter.Id, tokenName
                if (snippet != null)
                {
                    var transactionSnippet = new TransactionSnippetModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues())
                    {
                        RecordsCenterName = recordsCenter.Name
                    };
                    transactionSnippet.DesignHomeUrl = Url.Action("Index", "Home");
                    transactionSnippet.TransactionsHomeUrl = Url.Action("Index");

                    User user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));
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
        public ActionResult CreateSnippet(SnippetParameterModel snippetParameter)
        {
            return CreateOrUpdateSnippet(snippetParameter, false); 
        }
        [HttpPost]
        public ActionResult UpdateSnippet(SnippetParameterModel snippetParameter)
        {
            return CreateOrUpdateSnippet(snippetParameter, true);
        }
        [HttpPost]
        public ActionResult CreateSnippetField(TransactionSnippetFieldModel snippetFieldParameter)
        {
            return CreateOrUpdateSnippetField(snippetFieldParameter);
        }
        [HttpPost]
        public ActionResult UpdateSnippetField(TransactionSnippetFieldModel snippetFieldParameter)
        {
            return CreateOrUpdateSnippetField(snippetFieldParameter);
        }
        [HttpPost]
        public ActionResult DeleteSnippet(int snippetId)
        {
            var user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));
            if (user.CanDesignManage)
            {
                if (snippetId <= 0)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterInvalid);
                }
                var transactionSnippet = new TransactionSnippetModel(_designerTasks.DeleteTransactionSnippet(new TaskParameter<Snippet>(User.Identity.Name,new Snippet(snippetId))), TransactionSnippetFieldTypeHelper.TypeValues());
                setProperties(transactionSnippet, string.Empty, user);
                transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                return Json(transactionSnippet);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }
        [HttpPost]
        public ActionResult DeleteSnippetField(TransactionSnippetFieldModel snippetFieldParameter)
        {
            User user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));
            if (user.CanDesignManage)
            {
                if (snippetFieldParameter == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }
                var snippet = _designerTasks.DeleteTransactionSnippetField(new TaskParameter<SnippetField>(User.Identity.Name,new SnippetField(snippetFieldParameter.SnippetId, snippetFieldParameter.Id)));//snippetFieldParameter.SnippetId, snippetFieldParameter.Id
                var transactionSnippet = new TransactionSnippetModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues());
                setProperties(transactionSnippet, snippetFieldParameter.RecordsCenterName, user);
                transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                return Json(transactionSnippet);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }
        private ActionResult CreateOrUpdateSnippetField(TransactionSnippetFieldModel snippetFieldParameter)
        {
            User user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));
            if (user.CanDesignManage)
            {
                if (snippetFieldParameter == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }
                snippetFieldParameter.Validate();
                try
                {
                    var snippet = _designerTasks.UpdateTransactionSnippetField(new TaskParameter<SnippetFieldDetail>(User.Identity.Name,new SnippetFieldDetail(snippetFieldParameter.SnippetId, snippetFieldParameter.ToDomainModel())));//snippetFieldParameter.SnippetId, snippetFieldParameter.ToDomainModel()
                    var transactionSnippet = new TransactionSnippetModel(snippet, TransactionSnippetFieldTypeHelper.TypeValues());
                    setProperties(transactionSnippet, snippetFieldParameter.RecordsCenterName, user);
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
        private ActionResult CreateOrUpdateSnippet(SnippetParameterModel snippetParameter, bool updateExisting = true)
        {
            User user = _designerTasks.GetUser(new TaskParameter(User.Identity.Name));
            if (user.CanDesignManage)
            {
                if (snippetParameter == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.ParameterEmpty);
                }
                
                snippetParameter.Validate(user);
                TransactionSnippet snippet = _designerTasks.GetTransactionSnippet(new TaskParameter<Snippet>(User.Identity.Name,new Snippet(snippetParameter.Id)));    
                if(updateExisting && snippet == null)
                {
                    throw new StateInterfaceParameterValidationException(Resources.SnippetNotFound);
                }
                if(!updateExisting)
                {
                    snippet = new TransactionSnippet()
                        {
                            Id = snippetParameter.Id,
                            RecordsCenter = _designerTasks.GetRecordsCenterByName(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(snippetParameter.RecordsCenterName) }),
                            Created = DateTime.UtcNow
                        };
                }
                snippet.TokenName = snippetParameter.Name;
                snippet.Description = snippetParameter.Description;
                snippet.TransactionDefinition = snippetParameter.Definition;
                snippet.Criteria = snippetParameter.Criteria;
                snippet.IncludePrefixAndSuffix = snippetParameter.IncludePrefixAndSuffix;                    

                var transactionSnippet = new TransactionSnippetModel(_designerTasks.UpdateTransactionSnippet(new TaskParameter<TransactionSnippet>(User.Identity.Name,snippet)), TransactionSnippetFieldTypeHelper.TypeValues());                    
                setProperties(transactionSnippet, snippetParameter.RecordsCenterName, user);
                transactionSnippet.InitialData = JsonConvert.SerializeObject(transactionSnippet);
                return Json(transactionSnippet);
            }
            throw new System.Web.Http.HttpResponseException(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
        }
        private TransactionSnippetModel setProperties(TransactionSnippetModel snippet, string recordsCenterName, User user)
        {
            snippet.RecordsCenterName = recordsCenterName;
            snippet.CanDesignManage = user.CanDesignManage;            
            snippet.SnippetDetailsUrl = String.Concat(Url.Action("Details"), "/", recordsCenterName, "/", snippet.TokenName);
            snippet.UpdateSnippetUrl = Url.Action("UpdateSnippet");
            snippet.UpdateSnippetFieldUrl = Url.Action("UpdateSnippetField");
            snippet.DeleteSnippetFieldUrl = Url.Action("DeleteSnippetField");
            return snippet;
        }

    }
}