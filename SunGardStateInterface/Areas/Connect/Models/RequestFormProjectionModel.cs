using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Connect.Models
{
    public class RequestFormProjectionModel
    {
        public string FormId { get; set; }
        public string Title { get; set; }
        public string FormSpecUrl { get; set; }
        public RequestFormProjectionModel(RequestFormProjection requestFormProjection, string formSpecUrl)
        {
            FormId = requestFormProjection.FormId;
            Title = requestFormProjection.Title;
            FormSpecUrl = string.Format("{0}/{1}", formSpecUrl, FormId);
        }
    }
}