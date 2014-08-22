using StateInterface.Designer.Model;
using StateInterface.Models;
using System.Collections.Generic;

namespace StateInterface.Areas.Design.Models
{
    public class FormCatalogModel
    {
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public List<CatalogItemModel> CatalogItems { get; set; }

        public string InitialData { get; set; }
        public FormsRequestModel FormsRequest { get; set; }
        public string GetFormsUrl { get; set; }
        public string FormDetailsUrl { get; set; }
        public string DesignHomeUrl { get; set; }

        public FormCatalogModel()
        {
            CatalogItems = new List<CatalogItemModel>();
            FormsRequest = new FormsRequestModel();
        }
        public FormCatalogModel(User user, IEnumerable<RecordsCenter> recordsCenters)
            :this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}