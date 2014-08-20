using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model
{
    public class Quote
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public ICollection<RecordsCenter> RecordCenters { get; set; }
        public ICollection<MessageSelection> SelectedMessages { get; set; }
    }
}
