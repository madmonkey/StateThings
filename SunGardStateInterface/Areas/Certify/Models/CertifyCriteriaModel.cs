using System.Collections.Generic;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyCriteriaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShowFieldToolTips { get; set; }
        public List<TestCaseModel> TestCases { get; set; }
        public CertifyCriteriaModel()
        {
            TestCases = new List<TestCaseModel>();
        }
        public CertifyCriteriaModel(Criteria criteria, Application application)
            : this()
        {
            Id = criteria.Id;
            Name = criteria.CriteriaName;
            Description = criteria.Description;

            buildTestCases(criteria, application);
        }

        private void buildTestCases(Criteria criteria, Application application)
        {
            foreach (var testCase in criteria.GetTestCases(application))
            {
                var testCaseModel = new TestCaseModel(testCase);

                TestCases.Add(testCaseModel);
            }
        }
    }
}