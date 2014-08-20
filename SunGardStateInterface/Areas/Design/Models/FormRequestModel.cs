using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design
{
    public class RequestFormRequestModel
    {
        public int RecordsCenterId { get; set; }
        public string FormId { get; set; }

        public void Validate()
        {
            if (RecordsCenterId == 0 || string.IsNullOrWhiteSpace(FormId) == true)
            {
                throw new StateInterfaceParameterValidationException("Invalid parameters in FormRequestModel");
            }
        }
    }
}