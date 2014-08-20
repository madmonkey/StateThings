using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public abstract class HeaderNode : EntityBase
    {
        private int _id;
        private int _sequence;

        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        public virtual int Sequence
        {
            get { return _sequence; }
            set
            {
                _sequence = value;
                RaisePropertyChanged(() => Sequence);
            }
        }

        public HeaderNode()
        {
        }
    }
}
