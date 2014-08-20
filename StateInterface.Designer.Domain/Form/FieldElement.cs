using StateInterface.Designer.Model;
using System.Xml.Serialization;
using System;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class FieldElement : FormElement
    {
        public virtual FormField FormField { get; set; }

        public FieldElement()
            :base()
        {}
        public FieldElement(FieldElement sourceFieldElement, FormField newFormField)
            : base(sourceFieldElement)
        {
            FormField = newFormField;
        }
    }
}
