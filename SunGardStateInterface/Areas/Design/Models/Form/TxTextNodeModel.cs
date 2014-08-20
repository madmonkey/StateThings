using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class TxTextNodeModel : TxNodeModel
    {
        public string Text { get; set; }
        public TextNodeType TextNodeType { get; set; }
        public TxTextNodeModel(TxTextNode txNode)
        {
            Id = txNode.Id;
            Sequence = txNode.Sequence;
            Text = txNode.Text;
            TextNodeType = txNode.TextNodeType;

            DisplayValue = Text;
            IsField = false;
        }
    }
}