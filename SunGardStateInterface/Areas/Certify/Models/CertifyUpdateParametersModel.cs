
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
                throw new StateInterfaceParameterValidationException(Resources.ParameterInvalid);
            }
            if (CategoryId == 0)
            {
                throw new StateInterfaceParameterValidationException(Resources.ParameterInvalid);
            }
        }
    }
}