using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Model.Projections
{
    public class RequestFormCategoryProjection
    {
        public virtual int Id { get; set; }
        public virtual RequestFormProjection RequestForm { get; set; }
        public virtual CategoryProjection Category { get; set; }
    }
}
