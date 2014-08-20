using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class FieldCatalogItemModel
    {
        public string TagName { get; set; }
        public string ToolTip { get; set; }
        public string Description { get; set; }
        public string DetailsUrl { get; set; }
        public FieldCatalogItemModel(Field field, string detailsUrl)
        {
            //Id = field.Id;
            TagName = field.TagName;
            ToolTip = field.ToolTip;
            Description = field.Description;
            DetailsUrl = string.Format("{0}/{1}", detailsUrl, field.TagName); 
        }
    }
}