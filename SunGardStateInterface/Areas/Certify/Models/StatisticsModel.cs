
using System;

namespace StateInterface.Areas.Certify.Models
{
    public class StatisticsModel
    {
        public int RecordsCenter { get; set; }
        public bool IsAverageInput { get; set; }
        public string CompletedDate { get; set; }
        public int TestCases { get; set; }
        public double Average { get; set; }
        
        public void Validate()
        {
            if (IsAverageInput)
            {
                if (Average <= 0)
                {
                    throw new ViewModelValidationException("Invalid Average");
                }
            }
            else
            {
                DateTime entryDate;
                if (DateTime.TryParse(CompletedDate, out entryDate) == false)
                {
                    throw new ViewModelValidationException("Invalid entry date");
                }

                if (entryDate < DateTime.Now.Date)
                {
                    throw new ViewModelValidationException("Invalid entry date");
                }
            }
        }
    }
}