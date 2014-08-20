using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model
{
    public class MessageVariation
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public string Description { get; set; }
        public Message Message { get; set; }
        public bool IsStandardSet { get; set; }
    }
}
