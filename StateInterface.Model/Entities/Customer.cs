using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Quote> Quotes { get; set; }
    }
}
