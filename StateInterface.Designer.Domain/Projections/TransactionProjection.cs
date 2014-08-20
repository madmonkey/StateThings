using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Model.Projections
{
    public class TransactionProjection
    {
        public virtual int Id { get; set; }
        public virtual string TransactionName { get; set; }
        public virtual string Description { get; set; }
        public virtual int Sequence { get; set; }
        public virtual RequestFormProjection RequestForm { get; set; }
        public virtual IList<Criteria> Criterion { get; set; }
        
    }
}
