using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Model;

namespace SunGardStateInterface.Models.StateInterface
{
    public class RecordsCenterViewModel
    {
        public string AddUrl { get; set; }
        public string EditUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string ManageUrl { get; set; }
        public string InitialData { get; set; }
        public RecordsCenterModel RecordsCenter { get; set; }
        public RecordsCenterModel SelectedRecordsCenter { get; set; }
        public CategoryModel CategoryForEdit { get; set; }

        public RecordsCenterViewModel(RecordsCenter recordsCenter)
        {
            RecordsCenter = new RecordsCenterModel(recordsCenter);
            SelectedRecordsCenter = new RecordsCenterModel();
            CategoryForEdit = new CategoryModel();
        }

        public RecordsCenterViewModel()
        {
        }
    }
}