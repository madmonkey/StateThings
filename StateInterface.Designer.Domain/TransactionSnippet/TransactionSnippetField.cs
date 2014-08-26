using StateInterface.Designer.Model.Properties;
using System;

namespace StateInterface.Designer.Model
{
    public class TransactionSnippetField : IValidate
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
            MustHaveSeparator();
        }

        private void MustHaveSeparator()
        {
            if(Frequency >= 1)
            {
                if(string.IsNullOrEmpty(Separator))
                {
                    throw new ApplicationException(Resources.MustHaveSeparator);
                }
            }
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
                        throw new ApplicationException(string.Format(Resources.CanHaveTransformFormatString, FormatMask.ToString()));
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
                        throw new ApplicationException(string.Format(Resources.CanHaveDefaultValue, FormatMask.ToString()));
                    }
                    break;

            }
        }

        private void CanStringPropertiesBeSaved()
        {
            if (string.IsNullOrWhiteSpace(TagName) || TagName.Length > 50)
            {
                throw new ApplicationException(string.Format( Resources.PropertyCannotBeSaved, "TagName", "cannot be blank or greater than 50 characters"));
            }
            if (!string.IsNullOrWhiteSpace(Prefix) && Prefix.Length > 250)
            {
                throw new ArgumentException(string.Format(Resources.PropertyCannotBeSaved, "Prefix", "cannot greater than 250 characters"));
            }
            if (!string.IsNullOrWhiteSpace(Suffix) && Suffix.Length > 250)
            {
                throw new ArgumentException(string.Format(Resources.PropertyCannotBeSaved, "Suffix", "cannot greater than 250 characters"));
            }
            if (!string.IsNullOrWhiteSpace(ToolTip) && ToolTip.Length > 100)
            {
                throw new ArgumentException(string.Format(Resources.PropertyCannotBeSaved, "ToolTip", "cannot greater than 100 characters"));
            }
            if (!string.IsNullOrWhiteSpace(DefaultValue) && DefaultValue.Length > 50)
            {
                throw new ArgumentException(string.Format(Resources.PropertyCannotBeSaved, "DefaultValue", "cannot greater than 50 characters"));
            }
            if (!string.IsNullOrWhiteSpace(TransformFormat) && TransformFormat.Length > 50)
            {
                throw new ArgumentException(string.Format(Resources.PropertyCannotBeSaved, "TransformFormat", "cannot greater than 50 characters"));
            }
            if (!string.IsNullOrWhiteSpace(Separator) && Separator.Length > 5)
            {
                throw new ArgumentException(string.Format(Resources.PropertyCannotBeSaved, "Separator", "cannot greater than 5 characters"));
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
                        throw new ArgumentException(string.Format(Resources.CanHaveFrequency, FormatMask.ToString()));
                    }
                    else if (!string.IsNullOrWhiteSpace(Separator))
                    {
                        throw new ArgumentException(string.Format(Resources.CanHaveSeparator, FormatMask.ToString()));
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
                    throw new ArgumentException(string.Format(Resources.CanLengthBeInferred, "Social Security Number"));
                }
            }
            if (FormatMask == FormatMaskType.Date)
            {
                if (Length != TransformFormat.Length)
                {
                    throw new ArgumentException(string.Format(Resources.CanLengthBeInferred, "Date"));
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
                        throw new ArgumentException(string.Format(Resources.CanTrimInputToLength, FormatMask.ToString()));
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
                        throw new ArgumentException(string.Format(Resources.CanAcceptCarriageReturns, FormatMask.ToString()));
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
                        throw new ArgumentException(string.Format(Resources.CanMakeUpperCase,FormatMask.ToString()));
                    }
                    break;
            }
        }
    }
}
