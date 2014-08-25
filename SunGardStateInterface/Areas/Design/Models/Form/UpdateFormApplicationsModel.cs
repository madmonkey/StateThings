using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class UpdateFormApplicationsModel
    {
        public string RecordsCenterName { get; set; }
        public string FormId { get; set; }
        public List<SelectItemModel> Applications { get; set; }

        public UpdateFormApplicationsModel()
        {
            Applications = new List<SelectItemModel>();
        }
        public UpdateFormApplicationsModel(RequestForm requestForm, IEnumerable<Application> applications)
            : this()
        {
            this.RecordsCenterName = requestForm.RecordsCenter.Name;
            this.FormId = requestForm.FormId;
            foreach (var application in applications)
            {
                this.Applications.Add(new SelectItemModel()
                {
                    Id = application.Id,
                    Name = application.Name,
                    Description = application.Description,
                    IsSelected = requestForm.Applications.Any(x => x.Id == application.Id)
                });
            }
        }
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(FormId))
            {
                throw new StateInterfaceParameterValidationException("Invalid FormId");
            }

            if (string.IsNullOrEmpty(RecordsCenterName))
            {
                throw new StateInterfaceParameterValidationException("Invalid RecordCenter");
            }

            if (Applications == null)
            {
                throw new StateInterfaceParameterValidationException("Invalid Application Association");
            }
        }
    }
}