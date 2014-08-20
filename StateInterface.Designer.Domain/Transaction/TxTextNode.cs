using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class TxTextNode : TxNode
    {
        public virtual TextNodeType TextNodeType { get; set; }

        public virtual string Text { get; set; }

        public TxTextNode()
            :base()
        {}
        public TxTextNode(TxTextNode sourceTxTextNode)
            : base(sourceTxTextNode)
        {
            TextNodeType = sourceTxTextNode.TextNodeType;
            Text = sourceTxTextNode.Text;
        }
    }
}
