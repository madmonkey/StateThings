using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Designer.Model
{
    public class StatisticsApplication
    {
        public string Name { get; set; }
        public IList<StatisticsCategory> Categories { get; set; }
        public StatisticsDetails Statistics { get; set; }

        public StatisticsApplication(string name)
        {
            Name = name;
            Categories = new List<StatisticsCategory>();
            Statistics = new StatisticsDetails();
        }

        public void CalculateQaStatistics()
        {
            foreach (var category in Categories)
            {
                Statistics.TotalTestCases += category.Statistics.TotalTestCases;
                Statistics.CountCurrentUnitTestPassed += category.Statistics.CountCurrentUnitTestPassed;
                Statistics.CountCurrentUnitTestFailed += category.Statistics.CountCurrentUnitTestFailed;
                Statistics.CountCurrentVerifyPassed += category.Statistics.CountCurrentVerifyPassed;
                Statistics.CountCurrentVerifyFailed += category.Statistics.CountCurrentVerifyFailed;
                Statistics.CountCurrentCertifyPassed += category.Statistics.CountCurrentCertifyPassed;
                Statistics.CountCurrentCertifyFailed += category.Statistics.CountCurrentCertifyFailed;
                foreach (var activity in category.Statistics.Activities)
                {
                    Statistics.Activities.Add(activity);
                }
            }

            Statistics.CalculateData();
        }
    }
}
