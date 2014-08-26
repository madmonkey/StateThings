using StateInterface.Designer.Model;
using StateInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class HomeModel
    {
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public string InitialData { get; set; }
        public string GetFormUrl { get; set; }

        public HomeModel()
        {
        }

        public HomeModel(User user, IEnumerable<RecordsCenter> recordsCenters)
            : this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}