using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public abstract class TxNodeModel
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string DisplayValue { get; set; }
        public bool IsField { get; set; }
    }
}