using StateInterface.Designer.Model;
using SunGardStateInterface.Models;
using System.Collections.Generic;
using System.Linq;

namespace StateInterface.Models
{
    public class RecordsCenterSelectorModel
    {
        public string SetRecordsCenterUrl { get; set; }
        public string SelectedRecordsCenterName { get; set; }
        public List<NameValueModel> RecordsCenters { get; set; }
        public RecordsCenterSelectorModel(User user, IEnumerable<RecordsCenter> recordsCenters)
        {
            RecordsCenters = new List<NameValueModel>();
            SelectedRecordsCenterName = user.CurrentRecordsCenter.Name;

            foreach (var recordsCenter in recordsCenters.OrderBy(x => x.Name))
            {
                var nameValueModel = new NameValueModel()
                {
                    Id = recordsCenter.Id,
                    Name = recordsCenter.Name,
                    Value = recordsCenter.Id.ToString()
                };
                RecordsCenters.Add(nameValueModel);
            }
        }
    }
}