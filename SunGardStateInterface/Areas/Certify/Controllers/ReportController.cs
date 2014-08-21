using System;
using System.Globalization;
using Newtonsoft.Json;
using StateInterface.Areas.Certify.Models;
using StateInterface.Designer.Model;
using System.Web.Mvc;
using Designer.Tasks;
using StateInterface.Properties;
using StateInterface.Controllers;

namespace StateInterface.Areas.Certify.Controllers
{
    [Authorize]
    public class ReportController : StateConnectContollerBase
    {
        public ReportController(IDesignerTasks designerTasks)
            : base(designerTasks)
        {
        }
        [HttpGet]
        public ActionResult Index()
        {
            var recordsCenters = _designerTasks.GetRecordsCenters(new TaskParameter(User.Identity.Name));
            var reportModel = new ReportModel(recordsCenters)
                {
                    GetCertificationStatusUrl = Url.Action("Status"),
                    GetOpenIssuesUrl = Url.Action("OpenIssues")
                };

            reportModel.InitialData = JsonConvert.SerializeObject(reportModel);
            ViewBag.Title = "Certification";
            return View(reportModel);
        }
        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Title = "Report Help";
            return View();
        }
        [HttpGet]
        public ActionResult Status(string recordsCenterName)
        {
            if (String.IsNullOrEmpty(recordsCenterName))
            {
                throw new StateInterfaceParameterValidationException(Resources.RecordsCenterInvalid);
            }

            var recordsCenter = _designerTasks.GetRecordsCenterByName(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenterName) });

            if (recordsCenter == null)
            {
                throw new StateInterfaceParameterValidationException(string.Format(Resources.RecordsCenterNotFound));
            }

            var statisticsRecordsCenter = _designerTasks.GetStatisticsForRecordsCenter(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenter.Name) });
            var statistics = new StatisticsRecordsCenterModel(statisticsRecordsCenter)
                {
                    GetAverageUrl = Url.Action("GetAverage", new { })
                };

            statistics.InitialData = JsonConvert.SerializeObject(statistics);
            ViewBag.Title = string.Format("Certification Status - {0}", recordsCenter.Name);
            return View(statistics);
        }
        [HttpGet]
        public ActionResult Print(string recordsCenterName)
        {
            if (String.IsNullOrEmpty(recordsCenterName))
            {
                throw new StateInterfaceParameterValidationException(Resources.RecordsCenterInvalid);
            }

            var recordsCenter = _designerTasks.GetRecordsCenterByName(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenterName) });

            if (recordsCenter == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.RecordsCenterNotFound);
            }

            var statisticsRecordsCenter = _designerTasks.GetStatisticsForRecordsCenter(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenter.Name) });
            var statistics = new StatisticsRecordsCenterModel(statisticsRecordsCenter);

            statistics.InitialData = JsonConvert.SerializeObject(statistics);
            ViewBag.Title = string.Format("Certification Report - {0}", recordsCenter.Name);
            return View(statistics);
        }
        [HttpGet]
        public ActionResult OpenIssues(string recordsCenterName)
        {
            if (String.IsNullOrEmpty(recordsCenterName))
            {
                throw new StateInterfaceParameterValidationException(Resources.RecordsCenterInvalid);
            }

            var recordsCenter = _designerTasks.GetRecordsCenterByName(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenterName) });

            if (recordsCenter == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.RecordsCenterNotFound);
            }

            var openIssues = _designerTasks.GetOpenIssues(new TaskParameter<RecordsCenterName>(User.Identity.Name) { Parameters = new RecordsCenterName(recordsCenterName) });
            var openIssuesModel = new OpenIssuesModel(recordsCenter, openIssues, Url.Action("Details", "Form", new { area = "Design" }), Url.Action("UpdateForm", "Update"));

            openIssuesModel.InitialData = JsonConvert.SerializeObject(openIssuesModel);
            ViewBag.Title = string.Format("Open Issues - {0}", recordsCenter.Name);
            return View(openIssuesModel);
        }
        [HttpPost]
        public ActionResult GetAverage(StatisticsModel model)
        {
            if (model == null)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterNull);
            }

            model.Validate();
            string result;
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
