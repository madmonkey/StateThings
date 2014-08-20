using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Model.Projections
{
    public class CategoryProjection
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set;}
        //public virtual IList<RequestFormCategoryProjection> RequestFormCategories {get; set;}
    }
}
