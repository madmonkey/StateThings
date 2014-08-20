using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Areas.Certify.Models
{
    public class OpenIssuesApplicationModel
    {
        public string Name { get; set; }
        public IList<OpenIssuesCategoryModel> Categories { get; set; }

        public OpenIssuesApplicationModel()
        {
            Categories = new List<OpenIssuesCategoryModel>();
        }
        public OpenIssuesApplicationModel(Application application, IEnumerable<TestCase> failedForApplication, string formDetailsUrl, string updateFormUrl)
            : this()
        {
            Name = application.Name;

            updateFormUrl = string.Format("{0}#{1}tc_", updateFormUrl, application.Name);

            var categories = failedForApplication
                .SelectMany(x => x.Criteria.Transaction.RequestForm.RequestFormCategories
                    .Select(y => y.Category))
                .Distinct();
            foreach (var category in categories)
            {
                var failedForCategory = failedForApplication
                    .Where(x => x.Criteria.Transaction.RequestForm.RequestFormCategories
                        .Any(y => y.Category.Id == category.Id));

                var openIssuesCategory = new OpenIssuesCategoryModel(category, failedForCategory, formDetailsUrl, updateFormUrl);
                Categories.Add(openIssuesCategory);
            }
        }
    }
}
