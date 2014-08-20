using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Model.Projections
{
    public class RequestFormDetailProjection
    {
        public RequestFormDetailProjection()
        {
            this.Applications = new List<Application>();
        }
        public virtual int Id { get; set; }
        public virtual string FormId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual RecordsCenter RecordsCenter { get; set; }
        public virtual IList<TransactionProjection> Transactions { get; set; }
        public virtual IList<RequestFormCategoryProjection> RequestFormCategories { get; set; }
        public virtual IList<Application> Applications { get; set; }
    }
}
