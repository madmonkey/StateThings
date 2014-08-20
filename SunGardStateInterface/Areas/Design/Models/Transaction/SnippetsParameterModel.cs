using System;
using StateInterface.Properties;

namespace StateInterface.Areas.Design.Models
{
    public class SnippetsParameterModel
    {
        public string RecordsCenterName { get; set; }

        public SnippetsParameterModel()
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