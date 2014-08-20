using System.Collections.Generic;
using StateInterface.Designer.Model;
using System.Linq;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyApplicationModel
    {
        public string ApplicationName { get; set; }
        public string RecordsCenterName { get; set; }
        public QAStatusModel QAStatus { get; set; }
        public List<CertifyRequestFormModel> Forms { get; set; }
        public CertifyApplicationModel()
        {
            Forms = new List<CertifyRequestFormModel>();
        }
        public CertifyApplicationModel(string recordsCenterName, IEnumerable<RequestForm> requestForms, Application application, string formDetailsUrl, string updateFormCertificationUrl)
            : this()
        {
            RecordsCenterName = recordsCenterName;
            ApplicationName = application.Name;
            QAStatus = new QAStatusModel(requestForms.Where(x => x.Applications.Any(y=> y.Id == application.Id)), application);

            foreach (var requestForm in requestForms)
            {
                if (requestForm.Applications.Any(x => x.Id == application.Id))
                {
                    var requestFormModel = new CertifyRequestFormModel(requestForm, application, formDetailsUrl, updateFormCertificationUrl);

                    Forms.Add(requestFormModel);
                }
            }
        }
    }
}