using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Areas.Certify.Models
{
    public class OpenIssuesRequestFormModel
    {
        public string FormId { get; set; }
        public string Description { get; set; }
        public IList<OpenIssuesCriterionModel> Criteria { get; set; }
        public string FormDetailsUrl { get; set; }

        public OpenIssuesRequestFormModel()
        {
            Criteria = new List<OpenIssuesCriterionModel>();
        }
        public OpenIssuesRequestFormModel(RequestForm form, IEnumerable<TestCase> failedForRequestForm, string formDetailsUrl, string updateFormUrl)
            : this()
        {
            FormId = form.FormId;
            Description = form.Description;

            updateFormUrl = updateFormUrl.Replace("#", string.Format("/{0}#", form.FormId));

            var criteria = failedForRequestForm.Select(x => x.Criteria).Distinct();
            foreach (var criterion in criteria)
            {
                var failedForCriterion = failedForRequestForm
                    .Where(x => x.Criteria.Id == criterion.Id);

                var openIssuesCriterion = new OpenIssuesCriterionModel(criterion, failedForCriterion, updateFormUrl);
                Criteria.Add(openIssuesCriterion);
            }

            FormDetailsUrl = string.Format("{0}/{1}", formDetailsUrl, FormId);
        }
    }
}
