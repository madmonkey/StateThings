using System.Collections.Generic;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Model
{
    public class StatisticsRecordsCenter
    {
        public RecordsCenter RecordsCenter { get; set; }
        public IList<StatisticsApplication> Applications { get; set; }
        public StatisticsDetails Statistics { get; set; }
        public StatisticsRecordsCenter(RecordsCenter recordsCenter)
        {
            RecordsCenter = recordsCenter;
            Applications = new List<StatisticsApplication>();
            Statistics = new StatisticsDetails();
        }
        public void CalculateQaStatistics()
        {
            foreach (var application in Applications)
            {
                Statistics.TotalTestCases += application.Statistics.TotalTestCases;
                Statistics.CountCurrentUnitTestPassed += application.Statistics.CountCurrentUnitTestPassed;
                Statistics.CountCurrentUnitTestFailed += application.Statistics.CountCurrentUnitTestFailed;
                Statistics.CountCurrentVerifyPassed += application.Statistics.CountCurrentVerifyPassed;
                Statistics.CountCurrentVerifyFailed += application.Statistics.CountCurrentVerifyFailed;
                Statistics.CountCurrentCertifyPassed += application.Statistics.CountCurrentCertifyPassed;
                Statistics.CountCurrentCertifyFailed += application.Statistics.CountCurrentCertifyFailed;
                foreach (var activity in application.Statistics.Activities)
                {
                    Statistics.Activities.Add(activity);
                }
            }

            Statistics.CalculateData();
        }
    }
}