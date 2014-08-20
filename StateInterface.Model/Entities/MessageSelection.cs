using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model
{
    public class MessageSelection
    {
        public int Id { get; set; }
        public Message Message { get; set; }
        public bool ForRMS { get; set; }
        public bool ForCAD { get; set; }
        public bool ForMobile { get; set; }
        public Quote Quote { get; set; }
    }
}
