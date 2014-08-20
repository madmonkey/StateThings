using System;
using SunGardStateInterface;

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

        public void Validate()
        {
            if (CriteriaId == 0)
            {
                throw new StateInterfaceParameterValidationException("Invalid CriteriaId");
            }

            if (string.IsNullOrWhiteSpace(TestCaseId))
            {
                throw new StateInterfaceParameterValidationException("Invalid TestCaseId");
            }

            DateTime entryDate;
            if( DateTime.TryParse(EntryDate, out entryDate) == false)
            {
                throw new StateInterfaceParameterValidationException("Invalid EntryDate");
            }

            if (entryDate > DateTime.Now)
            {
                throw new StateInterfaceParameterValidationException("Invalid EntryDate");
            }
        }
    }
}