
using System;
namespace StateInterface.Designer.Model
{
    [Serializable]
    public class LabelElement : FormElement
    {
        public virtual string Text { get; set; }

        public LabelElement()
            : base()
        {}
        public LabelElement(LabelElement sourceLabelElement)
            : base(sourceLabelElement)
        {
            Text = sourceLabelElement.Text;
        }
    }
}
