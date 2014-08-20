using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Areas.Certify.Models
{
    public class OpenIssuesCriterionModel
    {
        public string Name { get; set; }
        public string TransactionName { get; set; }
        public IList<OpenIssuesTestCaseModel> TestCases { get; set; }

        public OpenIssuesCriterionModel()
        {
            TestCases = new List<OpenIssuesTestCaseModel>();
        }
        public OpenIssuesCriterionModel(Criteria criterion, IEnumerable<TestCase> failedForCriterion, string updateFormUrl)
            : this()
        {
            Name = criterion.CriteriaName;
            TransactionName = criterion.CriteriaName != criterion.Transaction.TransactionName
                ? string.Format("({0})", criterion.Transaction.TransactionName)
                : string.Empty;

            updateFormUrl = string.Format("{0}{1}", updateFormUrl, criterion.CriteriaName.Replace(" ", "-"));

            foreach (var testCase in failedForCriterion)
            {
                TestCases.Add(new OpenIssuesTestCaseModel(testCase, criterion.QAActions.OrderBy(x => x.OccurredAt).Last(), updateFormUrl)); 
            }
        }
    }
}
