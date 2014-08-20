using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class ListCatalogProjectionModel
    {        
        public string ListName { get; set; }
        public string ListDetailsUrl { get; set; }
        public ListCatalogProjectionModel(ListProjection list, string listDetailsUrl)
        {            
            ListName = list.ListName;
            ListDetailsUrl = string.Format("{0}/{1}", listDetailsUrl, list.ListName);
        }
    }
}