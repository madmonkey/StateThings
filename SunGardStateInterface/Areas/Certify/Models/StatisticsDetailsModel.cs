using System;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public class StatisticsDetailsModel
    {
        public double AverageCertified { get; set; }
        public double AverageVerified { get; set; }
        public double AverageUnitTested { get; set; }
        public string EstimationCompletionDate { get; set; }
        public string VerificationCompletionDate { get; set; }
        public string CertificationCompletionDate { get; set; }
        public int TotalTestCases { get; set; }
        public int CountCurrentUnitTestPassed { get; set; }
        public int CountCurrentUnitTestFailed { get; set; }
        public int CountCurrentVerifyPassed { get; set; }
        public int CountCurrentVerifyFailed { get; set; }
        public int CountCurrentCertifyPassed { get; set; }
        public int CountCurrentCertifyFailed { get; set; }
        public int CountUnUnitTestedTestCases { get; set; }
        public int CountUnVerifiedTestCases { get; set; }
        public int CountUnCertifiedTestCases { get; set; }
        public string PercentCertified { get; set; }
        public string PercentVerified { get; set; }
        public string PercentUnitTested { get; set; }

        public StatisticsDetailsModel(StatisticsDetails statisticsDetails)
        {
            TotalTestCases = statisticsDetails.TotalTestCases;
            CountCurrentUnitTestPassed = statisticsDetails.CountCurrentUnitTestPassed;
            CountCurrentUnitTestFailed = statisticsDetails.CountCurrentUnitTestFailed;
            CountCurrentVerifyPassed = statisticsDetails.CountCurrentVerifyPassed;
            CountCurrentVerifyFailed = statisticsDetails.CountCurrentVerifyFailed;
            CountCurrentCertifyPassed = statisticsDetails.CountCurrentCertifyPassed;
            CountCurrentCertifyFailed = statisticsDetails.CountCurrentCertifyFailed;
            CountUnUnitTestedTestCases = statisticsDetails.CountUnUnitTestedTestCases;
            CountUnVerifiedTestCases = statisticsDetails.CountUnVerifiedTestCases;
            CountUnCertifiedTestCases = statisticsDetails.CountUnCertifiedTestCases;
            PercentUnitTested = string.Format("{0}%", statisticsDetails.PercentUnitTested);
            PercentVerified = string.Format("{0}%", statisticsDetails.PercentVerified);
            PercentCertified = string.Format("{0}%", statisticsDetails.PercentCertified);
            AverageUnitTested = statisticsDetails.AverageUnitTested;
            AverageVerified = statisticsDetails.AverageVerified;
            AverageCertified = statisticsDetails.AverageCertified;
            VerificationCompletionDate = statisticsDetails.VerificationCompletionDate == DateTime.MinValue 
                ? string.Empty : statisticsDetails.VerificationCompletionDate.ToShortDateString();
            CertificationCompletionDate = statisticsDetails.CertificationCompletionDate == DateTime.MinValue 
                ? string.Empty : statisticsDetails.CertificationCompletionDate.ToShortDateString();
        }
    }
}