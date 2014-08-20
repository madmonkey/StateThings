using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StateInterface.Model;

namespace SunGardStateInterface.Models.StateInterface
{
    public class StateInterfaceViewModel
    {
        public string RecordsCenterModalMode { get; set; }
        public string EditUrl { get; set; }
        public string AddUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string ManageUrl { get; set; }
        public List<RecordsCenterModel> RecordsCenters { get; set; }
        public RecordsCenterModel SelectedRecordsCenter { get; set; }
        public string InitialData { get; set; }
        public StateInterfaceViewModel(IEnumerable<RecordsCenter> recordsCenters) : this()
        {
            SelectedRecordsCenter = new RecordsCenterModel();
            foreach (var item in recordsCenters)
            {
                RecordsCenters.Add(new RecordsCenterModel(item));
            }
        }

        public StateInterfaceViewModel()
        {
            RecordsCenters = new List<RecordsCenterModel>();
        }
    }
}