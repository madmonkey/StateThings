using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class FieldsRequestModel
    {
        public string RecordsCenterName { get; set; }

        public FieldsRequestModel()
        {
        }

        //todo: add validate method

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(RecordsCenterName))
            {
                throw new StateInterfaceParameterValidationException("Record Center Name is null.");
            }
        }
    }
}