using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Connect.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public List<RequestFormProjectionModel> Forms {get; set;}

        public CategoryModel()
        {
            Forms = new List<RequestFormProjectionModel>();
        }
        public CategoryModel(Category category, IEnumerable<RequestFormProjection> formProjections, string formSpecUrl)
            : this()
        {
            Name = category.Name;

            foreach (var form in formProjections.OrderBy(x => x.FormId))
            {
                Forms.Add(new RequestFormProjectionModel(form, formSpecUrl));
            }
        }

        public CategoryModel(string categoryName, IEnumerable<RequestFormProjection> formProjections, string formSpecUrl)
            : this()
        {
            Name = categoryName;

            foreach (var form in formProjections.OrderBy(x => x.FormId))
            {
                Forms.Add(new RequestFormProjectionModel(form, formSpecUrl));
            }
        }
    }
}