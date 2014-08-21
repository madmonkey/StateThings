using StateInterface.Designer.Model;
using StateInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class FieldCatalogModel
    {
        public string InitialData { get; set; }
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public IEnumerable<FieldCatalogItemModel> Fields { get; set; }

        public FieldsRequestModel FieldsRequest { get; set; }
        public string GetFieldsUrl { get; set; }
        public string FieldDetailsUrl { get; set; }
        public string DesignHomeUrl { get; set; }
        public FieldCatalogModel(User user, IEnumerable<RecordsCenter> recordsCenters)
        {
            FieldsRequest = new FieldsRequestModel();
            Fields = new List<FieldCatalogItemModel>();
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}