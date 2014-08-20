using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class TxFieldNodeModel : TxNodeModel
    {
        public FormFieldModel FormField { get; set; }
        public TxFieldNodeModel(TxFieldNode txNode)
        {
            Id = txNode.Id;
            Sequence = txNode.Sequence;
            FormField = new FormFieldModel(txNode.FormField);

            DisplayValue = txNode.Prefix + FormField.Name + txNode.Suffix;
            IsField = true;
        }
    }
}