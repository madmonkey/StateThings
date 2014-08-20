using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using StateInterface.Designer.Model.Helper;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class TxFieldNode : TxNode, IDataErrorInfo
    {
        private string _trimInputToLengthPropertyName;

        public TxFieldNode()
        {
            _trimInputToLengthPropertyName = PropertyHelper.GetPropertyName((TxFieldNode item) => item.TrimInputToLength);
        }

        public TxFieldNode(FormField formField)
            : this()
        {
            FormField = formField;
            Prefix = formField.Field.Prefix;
            Suffix = formField.Field.Suffix;
            TransformFormat = formField.Field.TransformFormat;
        }

        public TxFieldNode(TxFieldNode source)
            : base(source)
        {
            PadCharacter = source.PadCharacter;
            TrimInputToLength = source.TrimInputToLength;
            Prefix = source.Prefix;
            Suffix = source.Suffix;
            TransformFormat = source.TransformFormat;
        }
        public TxFieldNode(TxFieldNode sourceTxNode, IEnumerable<FormField> newFormFields)
            : base(sourceTxNode)
        {
            PadCharacter = sourceTxNode.PadCharacter;
            TrimInputToLength = sourceTxNode.TrimInputToLength;
            Prefix = sourceTxNode.Prefix;
            Suffix = sourceTxNode.Suffix;
            TransformFormat = sourceTxNode.TransformFormat;

            FormField = newFormFields.Where(x => x.Field.TagName == sourceTxNode.FormField.Field.TagName).FirstOrDefault();
        }

        public virtual short? PadCharacter { get; set; }

        public virtual int? TrimInputToLength { get; set; }

        public virtual FormField FormField { get; set; }

        public virtual string Prefix { get; set; }

        public virtual string Suffix { get; set; }

        public virtual string TransformFormat { get; set; }

        #region IDataErrorInfo Members

        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string columnName]
        {
            get
            {
                if (columnName == _trimInputToLengthPropertyName)
                {
                    if (TrimInputToLength < 1)
                    {
                        return "Trim length must be greater than zero";
                    }
                    if (TrimInputToLength > FormField.Field.Length)
                    {
                        return string.Format("Trim length must be less than length ({0})", FormField.Field.Length);
                    }
                }

                return null;
            }
        }

        #endregion
    }
}
