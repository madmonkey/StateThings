using System.Collections.Generic;
using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public class ReportModel
    {
        public List<RecordsCenterModel> RecordsCenters { get; set; }
        public string InitialData { get; set; }
        public string GetCertificationStatusUrl { get; set; }
        public string GetOpenIssuesUrl { get; set; }
        public ReportModel(IEnumerable<RecordsCenter> recordsCenters)
        {
            RecordsCenters = new List<RecordsCenterModel>();

            foreach (var recordsCenter in recordsCenters)
            {
                var recordsCenterModel = new RecordsCenterModel(recordsCenter);

                RecordsCenters.Add(recordsCenterModel);
            }
        }
    }
}