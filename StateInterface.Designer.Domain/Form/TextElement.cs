using StateInterface.Designer.Model;
using System.Xml.Serialization;
using System;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class TextElement : FormElement
    {
        public virtual Field Field { get; set; }

        public TextElement()
                :base()
        {}
    }
}
