using StateInterface.Designer.Model;
using System.Xml.Serialization;
using System;
using System.Diagnostics;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public abstract class FormElement : EntityBase
    {
        public virtual int Lines { get; set; }

        public virtual int RowSpan { get; set; }

        public virtual int Id { get; set; }

        public virtual int Sequence { get; set; }

        public virtual string ElementId { get; set; }

        public virtual int InRow { get; set; }

        public virtual int InColumn { get; set; }

        public virtual int ColumnSpan { get; set; }

        public virtual StyleDefinition Style { get; set; }

        public FormElement()
        {
            ElementId = Guid.NewGuid().ToString();
            Style = new StyleDefinition();

            // Not on the form
            ResetPlacement();
        }
        public FormElement(FormElement sourceFormElement)
            :this()
        {
            Lines = sourceFormElement.Lines;
            RowSpan = sourceFormElement.RowSpan;
            //Id - provided by DB
            Sequence = sourceFormElement.Sequence;
            //ElementId - default contructor
            //todo: should ElementId be copied - i think it should be uniuqe
            InRow = sourceFormElement.InRow;
            InColumn = sourceFormElement.InColumn;
            ColumnSpan = sourceFormElement.ColumnSpan;
            Style = new StyleDefinition(sourceFormElement.Style); 
        }

        public virtual void ResetPlacement()
        {
            InColumn = -1;
            InRow = -1;
            ColumnSpan = 0;
            Lines = 1;
        }
    }
}
