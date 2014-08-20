using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class HeaderTextNode : HeaderNode
    {
        private string _text;
        private TextNodeType _textNodeType;

        public virtual TextNodeType TextNodeType
        {
            get { return _textNodeType; }
            set
            {
                _textNodeType = value;
                RaisePropertyChanged(() => TextNodeType);
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }
    }
}
