using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class UsesField
    {
        public string RequestFormName { get; set; }
        public string FormDetailsUrl { get; set; }

        public UsesField(RequestFormProjection projection, string formDetailsUrl)
        {
            RequestFormName = projection.FormId;

            FormDetailsUrl = string.Format("{0}/{1}", formDetailsUrl, RequestFormName);
        }
    }
}