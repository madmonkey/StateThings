using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Areas.Certify.Models
{
    public class OpenIssuesModel
    {
        public IList<OpenIssuesApplicationModel> Applications { get; set; }
        public RecordsCenterModel RecordsCenter { get; set; }
        public int TotalFailedTestCases { get; set; }

        public string InitialData { get; set; }

        public OpenIssuesModel()
        {
            Applications = new List<OpenIssuesApplicationModel>();
        }
        public OpenIssuesModel(RecordsCenter recordsCenter, IEnumerable<TestCase> failedTestCases, string formDetailsUrl, string updateFormUrl)
            : this()
        {
            RecordsCenter = new RecordsCenterModel(recordsCenter);

            TotalFailedTestCases = failedTestCases.Count();

            formDetailsUrl = string.Format("{0}/{1}", formDetailsUrl, recordsCenter.Name);
            updateFormUrl = string.Format("{0}/{1}", updateFormUrl, recordsCenter.Name);

            var applications = failedTestCases.Select(x => x.Application).Distinct();

            foreach (var application in applications)
            {
                var failedForApplication = failedTestCases
                    .Where(x => x.Application.Id == application.Id);

                var openIssuesApplication = new OpenIssuesApplicationModel(application, failedForApplication, formDetailsUrl, updateFormUrl);
                Applications.Add(openIssuesApplication);
            }
        }
    }
}
