using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Certify.Models
{
    public class StatisticsApplicationModel
    {
        public string Name { get; set; }
        public List<StatisticsCategoryModel> Categories { get; set; }
        public StatisticsDetailsModel Statistics { get; set; }

        public StatisticsApplicationModel(StatisticsApplication application)
        {
            Name = application.Name;
            Categories = new List<StatisticsCategoryModel>();
            foreach (var category in application.Categories.OrderBy(x => x.Name))
            {
                Categories.Add(new StatisticsCategoryModel(category));
            }
            Statistics = new StatisticsDetailsModel(application.Statistics);
        }
    }
}