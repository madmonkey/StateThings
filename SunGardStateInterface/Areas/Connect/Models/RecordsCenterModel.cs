using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Connect.Models
{
    public class RecordsCenterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RecordsCenterModel(RecordsCenter recordsCenter)
        {
            Id = recordsCenter.Id;
            Name = recordsCenter.Name;
        }
    }
}