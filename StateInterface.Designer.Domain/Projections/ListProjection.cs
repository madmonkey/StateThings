using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StateInterface.Designer.Model.Projections
{
    public class ListProjection
    {
        public virtual int Id { get; set; }
        public virtual string ListName { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Updated { get; set; }
        public virtual RecordsCenter RecordsCenter { get; set; }
    }
}