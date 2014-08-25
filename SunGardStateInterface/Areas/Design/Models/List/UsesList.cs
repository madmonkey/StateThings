using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class UsesList
    {
        public string FormFieldName { get; set; }
        public string RequestFormName { get; set; }

        public string FormDetailsUrl { get; set; }

        public UsesList(FormFieldProjection projection, string formDetailsUrl)
        {
            FormFieldName = projection.FieldTagName;
            RequestFormName = projection.RequestFormId;
            FormDetailsUrl = string.Format("{0}/{1}", formDetailsUrl, RequestFormName);
        }
    }
}