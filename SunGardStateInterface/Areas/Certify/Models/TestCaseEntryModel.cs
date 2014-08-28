using System;
using SunGardStateInterface;
using StateInterface.Designer.Model;
using StateInterface.Designer;
using StateInterface.Properties;

namespace StateInterface.Areas.Certify.Models
{
    public class TestCaseEntryModel
    {
        public string FormId { get; set; }
        public int CriteriaId { get; set; }
        public string TestCaseId { get; set; }
        public string Name { get; set; }
        public string EntryDate { get; set; }
        public string Note { get; set; }
        public string CurrentStage { get; set; }
        public bool HasPassed { get; set; }
        public TestCaseEntryModel()
        {
            EntryDate = DateTime.Now.ToString("d");
        }

        public void Validate(User currentUser, bool isReadOperation = false)
        {
            if (!isReadOperation && !currentUser.CanDesignManage)
            {
                throw new SecurityAccessDeniedException(Resources.UserIsUnauthorized);
            }

            if (CriteriaId == 0)
            {
                throw new ViewModelValidationException("Invalid CriteriaId");
            }

            if (string.IsNullOrWhiteSpace(TestCaseId))
            {
                throw new ViewModelValidationException("Invalid TestCaseId");
            }

            DateTime entryDate;
            if( DateTime.TryParse(EntryDate, out entryDate) == false)
            {
                throw new ViewModelValidationException("Invalid EntryDate");
            }

            if (entryDate > DateTime.Now)
            {
                throw new ViewModelValidationException("Invalid EntryDate");
            }
        }
    }
}