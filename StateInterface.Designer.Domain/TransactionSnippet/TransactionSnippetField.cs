using System;

namespace StateInterface.Designer.Model
{
    public class TransactionSnippetField
    {
        public virtual int Id { get; set; }
        public virtual string TagName{ get; set; }
        public virtual string Prefix { get; set; }
        public virtual string Suffix { get; set; }
        public virtual string ToolTip { get; set; }
        public virtual bool MakeUpperCase { get; set; }
        public virtual bool AcceptCarriageReturns { get; set; }
        public virtual int Length { get; set; }
        public virtual int? PadCharacterDec { get; set; }
        public virtual int? TrimInputToLength { get; set; }
        public virtual string DefaultValue { get; set; }
        public virtual string TransformFormat { get; set; }
        public virtual FormatMaskType FormatMask { get; set; }
        public virtual int Frequency { get; set; }
        public virtual string Separator { get; set; }

        public virtual void IsValid()
        {
            CanStringPropertiesBeSaved();
            CanMakeUpperCase();
            CanAcceptCarriageReturns();
            CanLengthBeInferred();
            CanHaveFrequencySeparator();
            CanTrimInputToLength();
            CanHaveDefaultValue();
            CanHaveTransformFormatString();
        }
        private void CanHaveTransformFormatString()
        {
            switch (FormatMask)
            {
                case FormatMaskType.Text:
                case FormatMaskType.Numeric:
                case FormatMaskType.SSN:
                case FormatMaskType.Counter:
                    if (!string.IsNullOrWhiteSpace(TransformFormat))
                    {
                        throw new ApplicationException("TransformFormat");
                    }
                    break;
            }
        }

        private void CanHaveDefaultValue()
        {
            switch (FormatMask)
            {
                case FormatMaskType.Date:
                case FormatMaskType.SSN:
                case FormatMaskType.Counter:
                    if (!string.IsNullOrWhiteSpace(DefaultValue))
                    {
                        throw new ApplicationException("DefaultValue");
                    }
                    break;

            }
        }

        private void CanStringPropertiesBeSaved()
        {
            if (string.IsNullOrWhiteSpace(TagName) || TagName.Length > 50)
            {
                throw new ApplicationException("TagName");
            }
            if (!string.IsNullOrWhiteSpace(TransformFormat) && TransformFormat.Length > 2000)
            {
                throw new ArgumentException("TransformFormat");
            }
            if (!string.IsNullOrWhiteSpace(Prefix) && Prefix.Length > 250)
            {
                throw new ArgumentException("Prefix");
            }
            if (!string.IsNullOrWhiteSpace(Suffix) && Suffix.Length > 250)
            {
                throw new ArgumentException("Suffix");
            }
            if (!string.IsNullOrWhiteSpace(ToolTip) && ToolTip.Length > 100)
            {
                throw new ArgumentException("ToolTip");
            }
            if (!string.IsNullOrWhiteSpace(DefaultValue) && DefaultValue.Length > 50)
            {
                throw new ArgumentException("DefaultValue");
            }
            if (!string.IsNullOrWhiteSpace(TransformFormat) && TransformFormat.Length > 50)
            {
                throw new ArgumentException("TransformFormat");
            }
            if (!string.IsNullOrWhiteSpace(Separator) && Separator.Length > 5)
            {
                throw new ArgumentException("Separator");
            }
        }

        private void CanHaveFrequencySeparator()
        {
            switch (FormatMask)
            {
                case FormatMaskType.Counter:
                case FormatMaskType.SystemDate:
                    if (Frequency > 0)
                    {
                        throw new ArgumentException("Frequency");
                    }
                    else if (!string.IsNullOrWhiteSpace(Separator))
                    {
                        throw new ArgumentException("Separator");
                    }
                    break;
            }
        }

        private void CanLengthBeInferred()
        {
            if (FormatMask == FormatMaskType.SSN)
            {
                if (Length != 9)
                {
                    throw new ArgumentException("Length");
                }
            }
            if (FormatMask == FormatMaskType.Date)
            {
                if (Length != TransformFormat.Length)
                {
                    throw new ArgumentException("Length");
                }
            }
        }

        private void CanTrimInputToLength()
        {
            switch (FormatMask)
            {
                case FormatMaskType.Numeric:
                case FormatMaskType.Date:
                case FormatMaskType.SSN:
                case FormatMaskType.Counter:
                case FormatMaskType.Convert:
                case FormatMaskType.SystemDate:
                case FormatMaskType.StateProvinceRegion:
                    if (TrimInputToLength > 0)
                    {
                        throw new ArgumentException("TrimInputToLength");
                    }
                    break;
            }
        }

        private void CanAcceptCarriageReturns()
        {
            switch (FormatMask)
            {

                case FormatMaskType.Numeric:
                case FormatMaskType.Date:
                case FormatMaskType.SSN:
                case FormatMaskType.Counter:
                case FormatMaskType.Name:
                case FormatMaskType.SystemDate:
                case FormatMaskType.StateProvinceRegion:
                    if (AcceptCarriageReturns)
                    {
                        throw new ArgumentException("AcceptCarriageReturns");
                    }
                    break;
            }
        }

        private void CanMakeUpperCase()
        {
            switch (FormatMask)
            {
                case FormatMaskType.Numeric:
                case FormatMaskType.Date:
                case FormatMaskType.SSN:
                case FormatMaskType.Counter:
                case FormatMaskType.SystemDate:
                    if (MakeUpperCase)
                    {
                        throw new ArgumentException("MakeUpperCase");
                    }
                    break;
            }
        }
    }
}
