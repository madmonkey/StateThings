using System.Collections.Generic;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public class StatisticsCategoryModel
    {
        public string Name { get; set; }
        public List<StatisticsRequestFormModel> Forms { get; set; }
        public StatisticsDetailsModel Statistics { get; set; }

        public StatisticsCategoryModel(StatisticsCategory category)
        {
            Name = category.Name;
            Forms = new List<StatisticsRequestFormModel>();
            foreach (var form in category.Forms)
            {
                Forms.Add(new StatisticsRequestFormModel(form));
            }
            Statistics = new StatisticsDetailsModel(category.Statistics);
        }

    }
}