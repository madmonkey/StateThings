using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class RequestFormCategoryModel
    {
        public string Name { get; set; }
        public RequestFormCategoryModel(Category category)
        {
            Name = category.Name;
        }
    }
}