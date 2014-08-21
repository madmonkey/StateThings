using System;
using StateInterface.Properties;

namespace StateInterface.Areas.Design.Models
{
    public class SnippetsRequestModel
    {
        public string RecordsCenterName { get; set; }

        public SnippetsRequestModel()
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