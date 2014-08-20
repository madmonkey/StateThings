using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageKey { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public ICollection<MessageVariation> MessagesVariations { get; set; }
    }
}
