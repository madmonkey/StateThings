
using System;
using System.Collections.Generic;
namespace StateInterface.Designer.Model
{
    public class QAStatistics
    {
        public int TotalTestCases { get; set; }
        public int CountCurrentUnitTestPassed { get; set; }
        public int CountCurrentUnitTestFailed { get; set; }
        public int CountCurrentVerifyPassed { get; set; }
        public int CountCurrentVerifyFailed { get; set; }
        public int CountCurrentCertifyPassed { get; set; }
        public int CountCurrentCertifyFailed { get; set; }
        public double PercentUnitTested { get { return (double)CountCurrentUnitTestPassed / (double)TotalTestCases; } }
        public double PercentVerified { get { return (double)CountCurrentVerifyPassed / (double)TotalTestCases; } }
        public double PercentCertified { get { return (double)CountCurrentCertifyPassed / (double)TotalTestCases; } }
        public QAStatistics()
        {
        }
        public void AddTo(QAStatistics qaStatistics)
        {
            this.TotalTestCases += qaStatistics.TotalTestCases;
            this.CountCurrentUnitTestPassed += qaStatistics.CountCurrentUnitTestPassed;
            this.CountCurrentUnitTestFailed += qaStatistics.CountCurrentUnitTestFailed;
            this.CountCurrentVerifyPassed += qaStatistics.CountCurrentVerifyPassed;
            this.CountCurrentVerifyFailed += qaStatistics.CountCurrentVerifyFailed;
            this.CountCurrentCertifyPassed += qaStatistics.CountCurrentCertifyPassed;
            this.CountCurrentCertifyFailed += qaStatistics.CountCurrentCertifyFailed;
        }
    }
}
