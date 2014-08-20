using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StateInterface.Areas.Certify.Models;
using StateInterface.Designer.Model;
using System.Linq;
using System.Web.Mvc;
using StateInterface.Designer.Model.Projections;
using Designer.Tasks;

namespace StateInterface.Areas.Certify.Controllers
{
    public class ReportController : Controller
    {
        private IDesignerTasks _designerTasks;
        public ReportController(IDesignerTasks designerTasks)
        {
            _designerTasks = designerTasks;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordsCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var reportModel = new ReportModel(recordsCenters);
            reportModel.GetCertificationStatusUrl = Url.Action("Status");
            reportModel.GetOpenIssuesUrl = Url.Action("OpenIssues");

            reportModel.InitialData = JsonConvert.SerializeObject(reportModel);
            return View(reportModel);
        }
        [HttpGet]
        public ActionResult Help()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Status(string recordsCenterName)
        {
            if (String.IsNullOrEmpty(recordsCenterName))
            {
                throw new StateInterfaceParameterValidationException("RecordsCenterName was not passed in");
            }

            var recordsCenter = _designerTasks.GetRecordsCenterByName(recordsCenterName);

            if (recordsCenter == null)
            {
                throw new StateInterfaceParameterValidationException(string.Format("RecordsCenter is null. The records center {0} does not exist.", recordsCenterName));
            }

            var statisticsRecordsCenter = _designerTasks.GetStatisticsForRecordsCenter(recordsCenter.Name);
            var statistics = new StatisticsRecordsCenterModel(statisticsRecordsCenter);

            statistics.GetAverageUrl = Url.Action("GetAverage", new { });
            statistics.InitialData = JsonConvert.SerializeObject(statistics);
            return View(statistics);
        }
        [HttpGet]
        public ActionResult Print(string recordsCenterName)
        {
            if (String.IsNullOrEmpty(recordsCenterName))
            {
                throw new StateInterfaceParameterValidationException("RecordsCenterName was not passed in");
            }

            var recordsCenter = _designerTasks.GetRecordsCenterByName(recordsCenterName);

            if (recordsCenter == null)
            {
                throw new StateInterfaceParameterValidationException(string.Format("RecordsCenter is null. The records center {0} does not exist.", recordsCenterName));
            }

            var statisticsRecordsCenter = _designerTasks.GetStatisticsForRecordsCenter(recordsCenter.Name);
            var statistics = new StatisticsRecordsCenterModel(statisticsRecordsCenter);

            statistics.InitialData = JsonConvert.SerializeObject(statistics);
            return View(statistics);
        }
        [HttpGet]
        public ActionResult OpenIssues(string recordsCenterName)
        {
            if (String.IsNullOrEmpty(recordsCenterName))
            {
                throw new StateInterfaceParameterValidationException("RecordsCenterName was not passed in");
            }

            var recordsCenter = _designerTasks.GetRecordsCenterByName(recordsCenterName);

            if (recordsCenter == null)
            {
                throw new StateInterfaceParameterValidationException(string.Format("RecordsCenter is null. The records center {0} does not exist.", recordsCenterName));
            }

            var openIssues = _designerTasks.GetOpenIssues(recordsCenterName);
            var openIssuesModel = new OpenIssuesModel(recordsCenter, openIssues, Url.Action("Details", "Form", new { area = "Design" }) , Url.Action("UpdateForm", "Update"));

            openIssuesModel.InitialData = JsonConvert.SerializeObject(openIssuesModel);
            return View(openIssuesModel);
        }
        [HttpPost]
        public ActionResult GetAverage(StatisticsModel model)
        {
            if (model == null)
            {
                throw new StateInterfaceParameterValidationException("Model is null.");
            }

            model.Validate();
            var result = string.Empty;
            if (model.IsAverageInput)
            {
                var calculatedDate = StatisticsDetails.CalculateEstimatedDate(model.Average, model.TestCases);
                result = calculatedDate.ToShortDateString();
            }
            else
            {
                var calculatedAverage = StatisticsDetails.CalculateEstimatedAverage(model.CompletedDate, model.TestCases);
                result = calculatedAverage.ToString(CultureInfo.InvariantCulture);
            }
            return Json(new { text = result });
        }
    }
}
