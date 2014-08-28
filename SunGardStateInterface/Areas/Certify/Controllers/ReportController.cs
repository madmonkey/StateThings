using Designer.Tasks;
using ServiceStack.Text;
using StateInterface.Areas.Certify.Models;
using StateInterface.Controllers;
using StateInterface.Designer;
using StateInterface.Designer.Model;
using StateInterface.Models;
using StateInterface.Properties;
using System.Globalization;
using System.Web.Mvc;

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
            var recordsCenters = _designerTasks.GetRecordsCenters(User.Identity.Name);
            var reportModel = new ReportModel(recordsCenters)
                {
                    GetCertificationStatusUrl = Url.Action("Status"),
                    GetOpenIssuesUrl = Url.Action("OpenIssues")
                };
            reportModel.InitialData = JsonSerializer.SerializeToString(reportModel);
            ViewBag.Title = "Certification";
            return View(new ResponseModel<ReportModel>(reportModel));
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
            if (string.IsNullOrEmpty(recordsCenterName))
            {
                throw new ViewModelValidationException(Resources.RecordsCenterInvalid);
            }
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, recordsCenterName);
            if (recordsCenter == null)
            {
                throw new ObjectNotFoundException(string.Format(Resources.RecordsCenterNotFound, recordsCenterName.ToUpper()));
            }
            var statisticsRecordsCenter = _designerTasks.GetStatisticsForRecordsCenter(User.Identity.Name,recordsCenter.Name);
            var statistics = new StatisticsRecordsCenterModel(statisticsRecordsCenter)
                {
                    GetAverageUrl = Url.Action("GetAverage", new { })
                };
            statistics.InitialData = JsonSerializer.SerializeToString(statistics);
            ViewBag.Title = string.Format("Certification Status - {0}", recordsCenter.Name);
            return View(new ResponseModel<StatisticsRecordsCenterModel>(statistics));
        }
        [HttpGet]
        public ActionResult Print(string recordsCenterName)
        {
            if (string.IsNullOrEmpty(recordsCenterName))
            {
                throw new ViewModelValidationException(Resources.RecordsCenterInvalid);
            }
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name, recordsCenterName);
            if (recordsCenter == null)
            {
                throw new ObjectNotFoundException(string.Format(Resources.RecordsCenterNotFound, recordsCenterName.ToUpper()));
            }
            var statisticsRecordsCenter = _designerTasks.GetStatisticsForRecordsCenter(User.Identity.Name, recordsCenter.Name);
            var statistics = new StatisticsRecordsCenterModel(statisticsRecordsCenter);
            statistics.InitialData = JsonSerializer.SerializeToString(statistics);
            ViewBag.Title = string.Format("Certification Report - {0}", recordsCenter.Name);
            return View(new ResponseModel<StatisticsRecordsCenterModel>(statistics));
        }
        [HttpGet]
        public ActionResult OpenIssues(string recordsCenterName)
        {
            if (string.IsNullOrEmpty(recordsCenterName))
            {
                throw new ViewModelValidationException(Resources.RecordsCenterInvalid);
            }
            var recordsCenter = _designerTasks.GetRecordsCenterByName(User.Identity.Name,recordsCenterName);
            if (recordsCenter == null)
            {
                throw new ObjectNotFoundException(string.Format(Resources.RecordsCenterNotFound, recordsCenterName.ToUpper()));
            }
            var openIssues = _designerTasks.GetOpenIssues(User.Identity.Name, recordsCenterName);
            var openIssuesModel = new OpenIssuesModel(recordsCenter, openIssues, Url.Action("Details", "Form", new { area = "Design" }), Url.Action("UpdateForm", "Update"));
            openIssuesModel.InitialData = JsonSerializer.SerializeToString(openIssuesModel);
            ViewBag.Title = string.Format("Open Issues - {0}", recordsCenter.Name);
            return View(new ResponseModel<OpenIssuesModel>(openIssuesModel));
        }
        [HttpPost]
        public ActionResult GetAverage(StatisticsModel model)
        {
            if (model == null)
            {
                throw new ViewModelValidationException(Resources.ParameterNull);
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
            return Json(new ResponseModel<string>(result));
        }
    }
}
