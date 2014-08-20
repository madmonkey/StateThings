
namespace StateInterface.Areas.Certify.Models
{
    public class FieldValidationCriteriaModel
    {
        public bool IsRequired { get; set; }
        public string FieldTagName { get; set; }
        public string FieldToolTip { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
        public FieldValidationCriteriaModel()
        {
        }
    }
}