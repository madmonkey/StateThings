using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public class CertifyFormFieldModel
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public string ToolTip { get; set; }
        public CertifyFormFieldModel()
        {
        }
        public CertifyFormFieldModel(FormField formField)
            : this()
        {
            Id = formField.Id;
            TagName = formField.Field.TagName;
            ToolTip = formField.Field.ToolTip;
        }
    }
}