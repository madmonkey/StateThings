using System.Linq;
using System.Collections.Generic;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public class StatisticsRecordsCenterModel
    {
        public RecordsCenterModel RecordsCenter { get; set; }
        public List<StatisticsApplicationModel> Applications { get; set; }
        public StatisticsDetailsModel Statistics { get; set; }
        public string GetAverageUrl { get; set; }
        public string InitialData { get; set; }
        public StatisticsRecordsCenterModel(StatisticsRecordsCenter recordsCenter)
        {
            RecordsCenter = new RecordsCenterModel(recordsCenter.RecordsCenter);
            Applications = new List<StatisticsApplicationModel>();

            foreach (var application in recordsCenter.Applications.OrderBy(x => x.Name))
            {
                Applications.Add(new StatisticsApplicationModel(application));
            }

            Statistics = new StatisticsDetailsModel(recordsCenter.Statistics);
        }
    }
}