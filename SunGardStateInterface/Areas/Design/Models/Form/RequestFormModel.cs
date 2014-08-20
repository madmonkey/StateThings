using System.Threading;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class RequestFormModel
    {
        public int Id { get; set; }
        public string RecordsCenterName { get; set; }

        public string Version { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public string FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<FormFieldModel> FormFields { get; set; }

        public List<RequestFormCategoryModel> Associations { get; set; }

        public List<ApplicationModel> Applications { get; set; }
        public List<ApplicationModel> ApplicationsForEdit { get; set; }
        public string SubmissionMode { get; set; }
        public List<TransactionModel> Transactions { get; set; }

        public string InitialData { get; set; }
        //public string GetFormUrl { get; set; }
        public string PreviewFormUrl { get; set; }
        public bool CanDesignManage { get; set; }
        public string UpdateApplicationsAssociationUrl { get; set; }
        public string DesignHomeUrl { get; set; }
        public string FormsHomeUrl { get; set; }

        public RequestFormModel(RequestForm requestForm,
            string previewFormUrl, string listDetailsUrl, string fieldDetailsUrl,
            IEnumerable<Application> availableApplications)
            : this(requestForm, previewFormUrl, listDetailsUrl, fieldDetailsUrl)
        {
            var selectedApplications = this.Applications.ToList();
            this.Applications = new List<ApplicationModel>();
            foreach (var application in availableApplications)
            {
                this.Applications.Add(new ApplicationModel(application) { IsSelected = selectedApplications.Any(x => x.Id == application.Id) });
            }
        }
        public RequestFormModel(RequestForm requestForm, string previewFormUrl, string listDetailsUrl, string fieldDetailsUrl)
        {
            Id = requestForm.Id;
            RecordsCenterName = requestForm.RecordsCenter.Name;

            Version = requestForm.Version;
            Created = requestForm.Created;
            Updated = requestForm.Updated;

            FormId = requestForm.FormId;
            Title = requestForm.Title;
            Description = requestForm.Description;

            listDetailsUrl = string.Format("{0}/{1}", listDetailsUrl, requestForm.RecordsCenter.Name);
            fieldDetailsUrl = string.Format("{0}/{1}", fieldDetailsUrl, requestForm.RecordsCenter.Name);

            FormFields = new List<FormFieldModel>();
            foreach (FormField formField in requestForm.FormFields.OrderBy(x => x.Sequence))
            {
                FormFields.Add(new FormFieldModel(formField, listDetailsUrl, fieldDetailsUrl));
            }

            Associations = new List<RequestFormCategoryModel>();
            foreach (Category category in requestForm.Categories.OrderBy(x => x.Name))
            {
                Associations.Add(new RequestFormCategoryModel(category));
            }

            Applications = new List<ApplicationModel>();
            ApplicationsForEdit = new List<ApplicationModel>();
            foreach (Application app in requestForm.Applications.OrderBy(x => x.Name))
            {
                Applications.Add(new ApplicationModel(app));
            }

            SubmissionMode = requestForm.SubmissionMode.ToString();

            Transactions = new List<TransactionModel>();
            foreach (var transaction in requestForm.Transactions.OrderBy(x => x.Sequence))
            {
                var transactionViewModel = new TransactionModel(transaction);

                Transactions.Add(transactionViewModel);
            }

            //todo: move out into controller?
            PreviewFormUrl = string.Format("{0}/{1}/{2}", previewFormUrl, requestForm.RecordsCenter.Name, requestForm.FormId);
        }
    }
}