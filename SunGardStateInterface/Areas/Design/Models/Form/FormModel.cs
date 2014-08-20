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
        public GetFormsParametersModel GetFormsParameters { get; set; }
        public string GetFormsUrl { get; set; }
        public string FormDetailsUrl { get; set; }
        public string FormEditUrl { get; set; }

        public FormModel()
        {
            GetFormsParameters = new GetFormsParametersModel();
            RequestForms = new List<RequestFormCatalogProjectionModel>();
        }
        public FormModel(User user, IEnumerable<RecordsCenter> recordsCenters, string getFormsUrl, string formDetailsUrl, string formEditUrl)
            :this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
            GetFormsUrl = getFormsUrl;
            FormDetailsUrl = formDetailsUrl;
            FormEditUrl = formEditUrl;
        }
    }
}