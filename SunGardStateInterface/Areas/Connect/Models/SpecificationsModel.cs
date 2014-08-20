using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Connect.Models
{
    public class SpecificationsModel
    {
        public List<RecordsCenterModel> RecordsCenters { get; set; }
        public List<CategoryModel> Categories { get; set; }

        public FormsRequestParametersModel FormsRequestParameters { get; set; }
        public string GetFormsUrl { get; set; }
        public string InitialData { get; set; }

        public SpecificationsModel()
        {
            FormsRequestParameters = new FormsRequestParametersModel();
            RecordsCenters = new List<RecordsCenterModel>();
            Categories = new List<CategoryModel>();
        }

        public SpecificationsModel(IEnumerable<RecordsCenter> recordsCenters, string getFormsUrl)
            :this()
        {
            foreach (var recordsCenter in recordsCenters)
            {
                RecordsCenters.Add(new RecordsCenterModel(recordsCenter));
            }
            GetFormsUrl = getFormsUrl;
        }
    }
}