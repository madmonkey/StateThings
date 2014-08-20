using System;
using StateInterface.Designer.Model;
using System.Collections.Generic;

namespace StateInterface.Areas.Certify.Models
{
    public class QAStatusModel
    {
        public int TotalTestCases { get; set; }
        public string PercentUnitTested { get; set; }
        public string PercentVerified { get; set; }
        public string PercentCertified { get; set; }
        public int CountCurrentUnitTestPassed { get; set; }
        public int CountCurrentUnitTestFailed { get; set; }
        public int CountCurrentVerifyPassed { get; set; }
        public int CountCurrentVerifyFailed { get; set; }
        public int CountCurrentCertifyPassed { get; set; }
        public int CountCurrentCertifyFailed { get; set; }
        public QAStatusModel()
        {
        }
        public QAStatusModel(IEnumerable<RequestForm> requestForms, Application application)
            : this()
        {
            QAStatistics qaStatistics = new QAStatistics();
            foreach (var requestForm in requestForms)
            {
                var formQaStatistics = requestForm.GetQAStatistics(application);
                qaStatistics.AddTo(formQaStatistics);
            }
            populateQAStats(qaStatistics);
        }
        public QAStatusModel(RequestForm requestForm)
            : this()
        {
            var qaStatistics = requestForm.GetQAStatistics();
            populateQAStats(qaStatistics);
        }

        private void populateQAStats(QAStatistics qaStatistics)
        {
            TotalTestCases = qaStatistics.TotalTestCases;
            CountCurrentUnitTestPassed = qaStatistics.CountCurrentUnitTestPassed;
            CountCurrentUnitTestFailed = qaStatistics.CountCurrentUnitTestFailed;
            CountCurrentVerifyPassed = qaStatistics.CountCurrentVerifyPassed;
            CountCurrentVerifyFailed = qaStatistics.CountCurrentVerifyFailed;
            CountCurrentCertifyPassed = qaStatistics.CountCurrentCertifyPassed;
            CountCurrentCertifyFailed = qaStatistics.CountCurrentCertifyFailed;

            PercentUnitTested = string.Format("{0}%",
                Math.Round(Math.Truncate(qaStatistics.PercentUnitTested * 100.0), 2, MidpointRounding.ToEven));

            PercentVerified = string.Format("{0}%",
                Math.Round(Math.Truncate(qaStatistics.PercentVerified * 100.0), 2, MidpointRounding.ToEven));

            PercentCertified = string.Format("{0}%",
                Math.Round(Math.Truncate(qaStatistics.PercentCertified * 100.0), 2, MidpointRounding.ToEven));
        }
    }
}