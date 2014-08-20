using StateInterface.Designer.Model;
using System.Collections.Generic;
using System.Linq;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Areas.Certify.Models
{
    public class RequestFormProjectionModel
    {
        public int Id { get; set; }
        public string FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public RequestFormProjectionModel(RequestForm requestForm)
        {
            Id = requestForm.Id;
            FormId = requestForm.FormId;
            Title = requestForm.Title;
            Description = requestForm.Description;
        }
    }
}