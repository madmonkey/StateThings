using StateInterface.Designer.Model;
using StateInterface.Models;
using SunGardStateInterface.Models;
using System.Collections.Generic;
using System.Linq;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyUpdateModel
    {
        public string InitialData { get; set; }
        public string GetFormsUrl { get; set; }
        public List<CertifyApplicationModel> Applications { get; set; }
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public List<NameValueModel> CategoryOptions { get; set; }
        public CertifyUpdateParametersModel CertificationParameters { get; set; }
        
        public CertifyUpdateModel()
        {
            Applications = new List<CertifyApplicationModel>();
            CategoryOptions = new List<NameValueModel>();
            CertificationParameters = new CertifyUpdateParametersModel();
        }
        public CertifyUpdateModel(User user, IEnumerable<StateInterface.Designer.Model.RecordsCenter> recordsCenters, 
            IEnumerable<StateInterface.Designer.Model.Category> categories)
            : this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);

            foreach (var category in categories.OrderBy(x=> x.Name))
            {
                var nameValueModel = new NameValueModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Value = category.Id.ToString()
                };
                CategoryOptions.Add(nameValueModel);
            }
        }
    }
}