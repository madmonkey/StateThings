using StateInterface.Designer.Model;
using StateInterface.Areas.Design;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyUpdateFormModel
    {
        public int RecordsCenterId { get; set; }
        public string UpdateTestCaseUrl { get; set; }
        public string ResetTestCaseUrl { get; set; }
        public string GetFormQAStateUrl { get; set; }
        public string InitialData { get; set; }
        public CertifyUpdateRequestFormModel RequestForm { get; set; }
        public TestCaseEntryModel SelectedTestCase { get; set; }
        public RequestFormRequestModel RequestFormRequest { get; set; }
        
        public CertifyUpdateFormModel()
        {
            RequestForm = new CertifyUpdateRequestFormModel();
            SelectedTestCase = new TestCaseEntryModel();
            RequestFormRequest = new RequestFormRequestModel();
        }
        public CertifyUpdateFormModel(RequestForm requestForm, string previewFormUrl, string updateFormCertificationUrl)
            : this()
        {
            RecordsCenterId = requestForm.RecordsCenter.Id;
            RequestForm = new CertifyUpdateRequestFormModel(requestForm, previewFormUrl, updateFormCertificationUrl);
        }
    }
}