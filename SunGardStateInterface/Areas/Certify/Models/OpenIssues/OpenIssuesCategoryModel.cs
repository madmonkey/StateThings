using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Areas.Certify.Models
{
    public class OpenIssuesCategoryModel
    {
        public string Name { get; set; }
        public IList<OpenIssuesRequestFormModel> Forms { get; set; }

        public OpenIssuesCategoryModel()
        {
            Forms = new List<OpenIssuesRequestFormModel>();
        }
        public OpenIssuesCategoryModel(Category category, IEnumerable<TestCase> failedForCategory, string formDetailsUrl, string updateFormUrl)
            : this()
        {
            Name = category.Name;

            var requestForms = failedForCategory.Select(x => x.Criteria.Transaction.RequestForm).Distinct();
            foreach (var requestForm in requestForms)
            {
                var failedForRequestForm = failedForCategory
                    .Where(x => x.Criteria.Transaction.RequestForm.Id == requestForm.Id);

                var openIssuesRequestForm = new OpenIssuesRequestFormModel(requestForm, failedForRequestForm, formDetailsUrl, updateFormUrl);
                Forms.Add(openIssuesRequestForm);
            }
        }
    }
}
