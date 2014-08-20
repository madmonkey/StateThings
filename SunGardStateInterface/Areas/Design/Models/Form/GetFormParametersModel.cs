using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class GetFormParametersModel
    {
        public int RecordsCenterId { get; set; }
        public string FormId { get; set; }
        public GetFormParametersModel()
        {
        }
    }
}