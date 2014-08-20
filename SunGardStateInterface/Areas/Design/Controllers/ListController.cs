﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StateInterface.Designer.Model;
using Newtonsoft.Json;
using StateInterface.Areas.Design.Models;

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
			var recordCenters = _designerTasks.GetRecordsCenters();
            var user = _designerTasks.GetUser(User.Identity.Name);

			var model = new ListModel(user, recordCenters, Url.Action("GetLists"), Url.Action("Details"));
            model.Lists = getListModels(user.CurrentRecordsCenter.Name);
            model.RecordsCenterSelector.SetRecordsCenterUrl = Url.Action("SetRecordsCenter", "Home", new { Area = "" }); 
            
            model.InitialData = JsonConvert.SerializeObject(model);

			return View(model);
		}

		[HttpGet]
		public ActionResult Details(string recordsCenterName, string listName)
		{
            User user = _designerTasks.GetUser(System.Web.HttpContext.Current.User.Identity.Name);

			var recordsCenter = _designerTasks.GetRecordsCenters().FirstOrDefault(x => x.Name.Equals(recordsCenterName, StringComparison.CurrentCultureIgnoreCase));

			var list = _designerTasks.GetList(recordsCenter.Id, listName);
            var formFieldsUsing = _designerTasks.GetFormFieldProjectionsUsingOptionList(list);
            var listModel = new OptionListModel(list, formFieldsUsing);
			
			listModel.CanDesignManage = user.CanDesignManage;   
			listModel.InitialData = JsonConvert.SerializeObject(listModel);

			ViewBag.Title = listModel.ListName;

			return View(listModel);
		}

		[HttpPost]
		public ActionResult GetLists(GetListsParametersModel parameters)
		{
			if (parameters == null || string.IsNullOrWhiteSpace(parameters.RecordsCenterName))
			{
				throw new ApplicationException("Invalid parameters in GetLists");
			}

            List<ListCatalogProjectionModel> listModels = getListModels(parameters.RecordsCenterName);

			return Json(listModels);
		}

        private List<ListCatalogProjectionModel> getListModels(string recordsCenterName)
        {
            var recordsCenter = _designerTasks.GetRecordsCenters().FirstOrDefault(x => x.Name.Equals(recordsCenterName));
            var lists = _designerTasks.GetListProjections(recordsCenter.Id);

            List<ListCatalogProjectionModel> listModels = new List<ListCatalogProjectionModel>();
            foreach (var list in lists)
            {
                listModels.Add(new ListCatalogProjectionModel(list, Url.Action("Details") + "/" + recordsCenter.Name));
            }
            return listModels;
        }
	}
}