using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class PostApplicationParametersModel
    {
        public int RecordsCenterId { get; set; }
        public string FormId { get; set; }
        public List<ApplicationModel> Applications { get; set; }

        public PostApplicationParametersModel()
        {
            Applications = new List<ApplicationModel>();
        }
        public PostApplicationParametersModel (RequestForm applicationFormProjection, IEnumerable<Application> availableApplications)
	    {
            Applications = new List<ApplicationModel>();
            this.RecordsCenterId = applicationFormProjection.RecordsCenter.Id;
            this.FormId = applicationFormProjection.FormId;
            foreach (var application in availableApplications)
	        {
                this.Applications.Add(new ApplicationModel(application) 
                { 
                    IsSelected = applicationFormProjection.Applications.Any(x=> x.Id ==application.Id) 
                });
	        }
	    }
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(FormId))
            {
                throw new StateInterfaceParameterValidationException("Invalid FormId");
            }

            if (RecordsCenterId == 0)
            {
                throw new StateInterfaceParameterValidationException("Invalid RecordCenter");
            }

            if(Applications == null)
            {
                throw new StateInterfaceParameterValidationException("Invalid Application Association");
            }
        }
    }
}