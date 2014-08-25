using System;
using StateInterface.Properties;

namespace StateInterface.Areas.Design.Models
{
    public class SnippetsParametersModel
    {
        public string RecordsCenterName { get; set; }

        public SnippetsParametersModel()
        {
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(RecordsCenterName))
            {
                throw new ApplicationException(Resources.RecordsCenterInvalid);
            }
        }
    }
}