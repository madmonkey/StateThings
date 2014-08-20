using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public abstract class TxNode : EntityBase
    {
        public virtual int Id { get; set; }

        public virtual int Sequence { get; set; }

        public TxNode()
        {}
        
        public TxNode(TxNode sourceTxNode)
        {
            Sequence = sourceTxNode.Sequence;
        }
    }
}
