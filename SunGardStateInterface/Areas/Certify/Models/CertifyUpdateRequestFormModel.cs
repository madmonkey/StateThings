using System.Collections.Generic;
using StateInterface.Designer.Model;
using System.Linq;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyUpdateRequestFormModel
    {
        public int Id { get; set; }
        public int RecordsCenterId { get; set; }
        public string FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FormDetailsUrl { get; set; }
        public string UpdateFormCertificationUrl { get; set; }
        public List<CertifyApplicationTestCasesModel> Applications { get; set; }
        public QAStatusModel QAStatus { get; set; }
        public CertifyUpdateRequestFormModel()
        {
            Applications = new List<CertifyApplicationTestCasesModel>();
        }
        public CertifyUpdateRequestFormModel(RequestForm requestForm, string formDetailsUrl, string updateFormCertificationUrl)
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

            foreach (var application in requestForm.Applications)
            {
                var certifyApplicationTestCasesModel = new CertifyApplicationTestCasesModel(requestForm, application);
                Applications.Add(certifyApplicationTestCasesModel);
            }
        }
    }
}