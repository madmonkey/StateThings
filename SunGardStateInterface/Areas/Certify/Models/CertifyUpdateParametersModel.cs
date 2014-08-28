
using StateInterface.Properties;
namespace StateInterface.Areas.Certify.Models
{
    public class CertifyUpdateParametersModel
    {
        public string RecordsCenterName { get; set; }
        public int CategoryId { get; set; }
        public CertifyUpdateParametersModel()
        {
        }
        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(RecordsCenterName))
            {
                throw new ViewModelValidationException(Resources.ParameterInvalid);
            }
            if (CategoryId == 0)
            {
                throw new ViewModelValidationException(Resources.ParameterInvalid);
            }
        }
    }
}