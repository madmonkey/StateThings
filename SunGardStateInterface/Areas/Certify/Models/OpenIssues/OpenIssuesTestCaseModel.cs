using StateInterface.Designer.Model;
using StateInterface.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Areas.Certify.Models
{
    public class OpenIssuesTestCaseModel
    {
        public string Name { get; set; }
        public string FailureInfo { get; set; }
        public string Note { get; set; }

        public string UpdateFormUrl { get; set; }

        public OpenIssuesTestCaseModel(TestCase testCase, QAAction qaAction, string updateFormUrl)
        {
            Name = testCase.TestCaseId;
            FailureInfo = string.Format("{0} Failed on {1} by {2}", testCase.QaStatus.QAStage.ToString(), qaAction.OccurredAt.ToString(Resources.DayDateTimeFormat), qaAction.ByUser);
            Note = qaAction.Note;

            UpdateFormUrl = string.Format("{0}{1}", updateFormUrl, testCase.TestCaseId);
        }
    }
}
