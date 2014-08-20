using System.Collections.Generic;
using StateInterface.Designer.Model;
using System.Linq;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyRequestFormModel
    {
        public int Id { get; set; }
        public int RecordsCenterId { get; set; }
        public string FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FormDetailsUrl { get; set; }
        public string UpdateFormCertificationUrl { get; set; }
        public List<CertifyCriteriaModel> Criteria { get; set; }
        public QAStatusModel QAStatus { get; set; }
        public CertifyRequestFormModel()
        {
            Criteria = new List<CertifyCriteriaModel>();
        }
        public CertifyRequestFormModel(RequestForm requestForm, Application application, string formDetailsUrl, string updateFormCertificationUrl)
            : this()
        {
            Id = requestForm.Id;
            UpdateFormCertificationUrl = string.Format("{0}/{1}/{2}", updateFormCertificationUrl, requestForm.RecordsCenter.Name, requestForm.FormId);
            FormDetailsUrl = string.Format("{0}/{1}/{2}", formDetailsUrl, requestForm.RecordsCenter.Name, requestForm.FormId);
            RecordsCenterId = requestForm.RecordsCenter.Id;
            FormId = requestForm.FormId;
            Title = requestForm.Title;
            Description = requestForm.Description;
            QAStatus = new QAStatusModel(requestForm);

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