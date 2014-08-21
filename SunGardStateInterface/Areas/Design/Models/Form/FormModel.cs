using StateInterface.Designer.Model;
using StateInterface.Models;
using System.Collections.Generic;

namespace StateInterface.Areas.Design.Models
{
    public class FormModel
    {
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public List<RequestFormCatalogProjectionModel> RequestForms { get; set; }

        public string InitialData { get; set; }
        public FormsRequestModel FormsRequest { get; set; }
        public string GetFormsUrl { get; set; }
        public string FormDetailsUrl { get; set; }
        public string DesignHomeUrl { get; set; }

        public FormModel()
        {
            FormsRequest = new FormsRequestModel();
            RequestForms = new List<RequestFormCatalogProjectionModel>();
        }
        public FormModel(User user, IEnumerable<RecordsCenter> recordsCenters)
            :this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}