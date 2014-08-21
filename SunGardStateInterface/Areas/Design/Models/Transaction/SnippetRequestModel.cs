using System;
using StateInterface.Properties;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Design.Models
{
    public class SnippetRequestModel
    {
        public string RecordsCenterName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Definition { get; set; }
        public string Criteria { get; set; }
        public bool IncludePrefixAndSuffix { get; set; }

        public SnippetRequestModel()
        {
        }
        
        public void Validate(User currentUser)
        {
            
            if (string.IsNullOrEmpty(RecordsCenterName))
            {
                this.RecordsCenterName = currentUser.CurrentRecordsCenter.Name;
                //throw new ApplicationException(Resources.RecordsCenterInvalid);
            }
            if (Id < 0)
            {
                throw new ApplicationException(Resources.IdInvalid);
            }
            if (string.IsNullOrEmpty(Name))
            {
                throw new ApplicationException(Resources.SnippetNameInvalid);
            }
            if (string.IsNullOrEmpty(Description) || Description.Length > 100)
            {
                throw new ApplicationException(Resources.SnippetDescriptionInvalid);
            }
            if (!string.IsNullOrEmpty(Criteria) && Criteria.Length > 512)
            {
                throw new ApplicationException(Resources.SnippetCriteriaInvalid);
            }
        }
    }
}