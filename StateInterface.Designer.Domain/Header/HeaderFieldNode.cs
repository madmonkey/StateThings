using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class HeaderFieldNode : HeaderNode
    {
        private Field _field;
        private string _prefix;
        private string _suffix;
        private short? _padCharacter;
        private int _length;

        public HeaderFieldNode()
        {
        }

        public HeaderFieldNode(Field field)
        {
            _field = field;
        }

        public virtual Field Field
        {
            get { return _field; }
            set
            {
                _field = value;
                RaisePropertyChanged(() => Field);
            }
        }

        public virtual string Prefix
        {
            get { return _prefix; }
            set
            {
                _prefix = value;
                RaisePropertyChanged(() => Prefix);
            }
        }

        public virtual string Suffix
        {
            get { return _suffix; }
            set
            {
                _suffix = value;
                RaisePropertyChanged(() => Suffix);
            }
        }

        public virtual int Length
        {
            get { return _length; }
            set
            {
                _length = value;
                RaisePropertyChanged(() => Length);
            }
        }

        public virtual short? PadCharacter
        {
            get { return _padCharacter; }
            set
            {
                _padCharacter = value;
                RaisePropertyChanged(() => PadCharacter);
            }
        }
    }
}
