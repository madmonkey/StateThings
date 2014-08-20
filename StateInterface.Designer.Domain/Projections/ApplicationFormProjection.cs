using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Model
{
    public class ApplicationFormProjection
    {
        public virtual int Id { get; set; }
        public virtual string FormId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual RecordsCenter RecordsCenter { get; set; }
        public virtual IList<RequestFormCategoryProjection> RequestFormCategories { get; set; }
        public virtual IList<Application> Applications { get; set; }
    }
}
