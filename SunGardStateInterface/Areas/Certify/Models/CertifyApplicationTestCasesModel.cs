using System.Collections.Generic;
using StateInterface.Designer.Model;
using System.Linq;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyApplicationTestCasesModel
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public List<CertifyCriteriaModel> Criteria { get; set; }
        public CertifyApplicationTestCasesModel()
        {
            Criteria = new List<CertifyCriteriaModel>();
        }
        public CertifyApplicationTestCasesModel(RequestForm requestForm, Application application)
            : this()
        {
            ApplicationId = application.Id;
            ApplicationName = application.Name;

            foreach (var transaction in requestForm.Transactions)
            {
                foreach (var criteria in transaction.Criterion)
                {
                    var certifyCriteriaModel = new CertifyCriteriaModel(criteria, application);
                    Criteria.Add(certifyCriteriaModel);
                }
            }
        }
    }
}