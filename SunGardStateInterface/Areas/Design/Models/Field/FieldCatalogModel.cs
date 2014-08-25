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
        public IEnumerable<CatalogItemModel> CatalogItems { get; set; }

        public FieldsParametersModel FieldsParameters { get; set; }
        public string GetFieldsUrl { get; set; }
        public string FieldDetailsUrl { get; set; }
        public string DesignHomeUrl { get; set; }
        public FieldCatalogModel(User user, IEnumerable<RecordsCenter> recordsCenters)
        {
            CatalogItems = new List<CatalogItemModel>();
            FieldsParameters = new FieldsParametersModel();
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}