using System.Collections.Generic;

namespace StateInterface.Designer.Model
{
    public class StatisticsCategory
    {
        public string Name { get; set; }
        public List<RequestForm> Forms { get; set; }
        public StatisticsDetails Statistics { get; set; }

        public StatisticsCategory(string name)
        {
            Name = name;
            Forms = new List<RequestForm>();
            Statistics = new StatisticsDetails();
        }
        public void CalculateQaStatistics()
        {
            foreach (var form in Forms)
            {
                var statistics = form.GetQaStatisticsDetails();

                Statistics.TotalTestCases += statistics.TotalTestCases;
                Statistics.CountCurrentUnitTestPassed += statistics.CountCurrentUnitTestPassed;
                Statistics.CountCurrentUnitTestFailed += statistics.CountCurrentUnitTestFailed;
                Statistics.CountCurrentVerifyPassed += statistics.CountCurrentVerifyPassed;
                Statistics.CountCurrentVerifyFailed += statistics.CountCurrentVerifyFailed;
                Statistics.CountCurrentCertifyPassed += statistics.CountCurrentCertifyPassed;
                Statistics.CountCurrentCertifyFailed += statistics.CountCurrentCertifyFailed;

                foreach (var activity in statistics.Activities)
                {
                    Statistics.Activities.Add(activity);
                }
            }

            Statistics.CalculateData();
        }

        public void CalculateQaStatistics(Application application)
        {
            foreach (var form in Forms)
            {
                var statistics = form.GetQaStatisticsDetails(application);

                Statistics.TotalTestCases += statistics.TotalTestCases;
                Statistics.CountCurrentUnitTestPassed += statistics.CountCurrentUnitTestPassed;
                Statistics.CountCurrentUnitTestFailed += statistics.CountCurrentUnitTestFailed;
                Statistics.CountCurrentVerifyPassed += statistics.CountCurrentVerifyPassed;
                Statistics.CountCurrentVerifyFailed += statistics.CountCurrentVerifyFailed;
                Statistics.CountCurrentCertifyPassed += statistics.CountCurrentCertifyPassed;
                Statistics.CountCurrentCertifyFailed += statistics.CountCurrentCertifyFailed;

                foreach (var activity in statistics.Activities)
                {
                    Statistics.Activities.Add(activity);
                }
            }

            Statistics.CalculateData();
        }
    }
}