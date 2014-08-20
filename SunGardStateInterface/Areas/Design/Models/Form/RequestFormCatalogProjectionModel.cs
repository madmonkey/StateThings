using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class RequestFormCatalogProjectionModel
    {
        public string FormId { get; set; }
        public string Title { get; set; }
        public string FormDetailsUrl { get; set; }
        public RequestFormCatalogProjectionModel(RequestFormProjection requestForm, string formDetailsUrl)
        {
            FormId = requestForm.FormId;
            Title = requestForm.Title;
            FormDetailsUrl = string.Format("{0}/{1}", formDetailsUrl, requestForm.FormId);
        }
    }
}