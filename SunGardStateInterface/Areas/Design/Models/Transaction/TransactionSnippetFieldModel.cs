using System;
using StateInterface.Designer.Model;
using StateInterface.Properties;
using System.ComponentModel;

namespace StateInterface.Areas.Design.Models
{
    public class TransactionSnippetFieldModel
    {
        public string RecordsCenterName { get; set; }
        public int SnippetId { get; set; }
        public int Id { get; set; }
        public string TagName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string ToolTip { get; set; }
        public bool MakeUpperCase { get; set; }
        public int Length { get; set; }
        public int? PadCharacterDec { get; set; }
        public int? TrimInputToLength { get; set; }
        public string DefaultValue { get; set; }
        public string TransformFormat { get; set; }
        public string Field { get; set; }
        public int Frequency { get; set; }
        public string Separator { get; set; }
        public string InitialData { get; set; }
        public bool AcceptCarriageReturns { get; set; }

        public TransactionSnippetFieldModel(TransactionSnippetField field)
        {
            Id = field.Id;
            TagName = field.TagName;
            Prefix = field.Prefix;
            Suffix = field.Suffix;
            ToolTip = field.ToolTip;
            MakeUpperCase = field.MakeUpperCase;
            Length = field.Length;
            PadCharacterDec = field.PadCharacterDec;
            TrimInputToLength = field.TrimInputToLength;
            DefaultValue = field.DefaultValue;
            TransformFormat = field.TransformFormat;
            Field = FieldType(field.FormatMask);
            Frequency = field.Frequency;
            Separator = field.Separator;
            AcceptCarriageReturns = field.AcceptCarriageReturns;
        }

        public TransactionSnippetFieldModel()
        { }

        public void Validate()
        {
            CanSaveBaseProperties();
            CanAssignTextProperties(); //Upper & CarriageReturn
            CanAssignLength(); //Length
            CanHaveFrequencySeparator(); //Frequency and Separator
            CanHaveDefaultValue(); //Default Values
            CanHaveTransformFormat(); //Transform Format
            CanHaveTrimInputToLength(); //TrimInput
            CanHavePadCharacter(); //pad character
        }

        private void CanHavePadCharacter()
        {
            switch (FormatMask)
            {
                
                case FormatMaskType.Date:
                case FormatMaskType.SystemDate:
                if(PadCharacterDec > 0)
                    {
                        PadCharacterDec = null;
                    }
                    break;
            }
        }

        private void CanHaveTrimInputToLength()
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
                    if(TrimInputToLength !=null || TrimInputToLength != 0)
                    {
                        TrimInputToLength = null;
                    }
                    break;
            }
        }

        private void CanHaveTransformFormat()
        {
            switch (FormatMask)
            {
                case FormatMaskType.Numeric:
                case FormatMaskType.SSN:
                case FormatMaskType.Counter:
                case FormatMaskType.Text:
                if(!string.IsNullOrWhiteSpace(TransformFormat))
                {
                    TransformFormat = string.Empty;
                }
                break;
            }
        }

        private void CanSaveBaseProperties()
        {
            if (string.IsNullOrWhiteSpace(RecordsCenterName))
            {
                throw new ApplicationException(Resources.RecordsCenterInvalid);
            }
            if (SnippetId < 1)
            {
                throw new ApplicationException(Resources.ParentIdInvalid);
            }
            if (Id <= -1)
            {
                throw new ApplicationException(Resources.IdInvalid);
            }
            if (string.IsNullOrWhiteSpace(TagName) || TagName.Length > 50)
            {
                throw new ApplicationException(Resources.NameInvalid);
            }
            if (!string.IsNullOrWhiteSpace(TransformFormat) && TransformFormat.Length > 2000)
            {
                throw new ApplicationException(Resources.SnippetFormatInvalid);
            }
            if (!string.IsNullOrWhiteSpace(Prefix) && Prefix.Length > 250)
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            if (!string.IsNullOrWhiteSpace(Suffix) && Suffix.Length > 250)
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            if (!string.IsNullOrWhiteSpace(ToolTip) && ToolTip.Length > 100)
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            if (!string.IsNullOrWhiteSpace(DefaultValue) && DefaultValue.Length > 50)
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            if (!string.IsNullOrWhiteSpace(TransformFormat) && TransformFormat.Length > 50)
            {
                throw new ApplicationException(Resources.ParameterInvalid);
            }
            if (!string.IsNullOrWhiteSpace(Separator) && Separator.Length > 5)
            {
                throw new ApplicationException(Resources.ParameterInvalid);
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
                        Frequency = 0;
                    }
                    if (!string.IsNullOrWhiteSpace(Separator))
                    {
                        Separator = string.Empty;
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
                        DefaultValue = string.Empty;
                    }
                    break;

            }
        }

        private void CanAssignLength()
        {
            switch (FormatMask)
            {
                case FormatMaskType.SSN:
                    if (FormatMask == FormatMaskType.SSN)
                    {
                        if (Length != 9)
                        {
                            Length = 9;
                        }
                    }
                    break;
                case FormatMaskType.Date:
                    if (FormatMask == FormatMaskType.Date)
                    {
                        if (Length != TransformFormat.Length)
                        {
                            Length = TransformFormat.Length; //since something the user cannot manually mess up
                        }
                    }
                    break;
                
            }
        }
        private void CanAssignTextProperties()
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
                    MakeUpperCase = false;
                }
                break;
            }
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
                        AcceptCarriageReturns = false;
                    }
                    break;
            }
        }

        private string FieldType(FormatMaskType formatMaskType)
        {
            switch (formatMaskType)
            {
                case FormatMaskType.Numeric:
                    this.Field = "Numeric";
                    break;
                case FormatMaskType.Date:
                    this.Field = "Date";
                    break;
                case FormatMaskType.SSN:
                    this.Field = "SSN";
                    break;
                case FormatMaskType.Counter:
                    this.Field = "Counter";
                    break;
                case FormatMaskType.Name:
                    this.Field = "Name";
                    break;
                case FormatMaskType.Convert:
                    this.Field = "Convert";
                    break;
                case FormatMaskType.Text:
                    this.Field = "Text";
                    break;
                case FormatMaskType.SystemDate:
                    this.Field = "SystemDate";
                    break;
                case FormatMaskType.StateProvinceRegion:
                    this.Field = "StateProvinceRegion";
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            return this.Field;
        }

        private FormatMaskType FormatMask
        {
            get
            {
                switch (this.Field)
                {
                    case "Numeric":
                        return FormatMaskType.Numeric;
                    case "Date":
                        return FormatMaskType.Date;
                    case "SSN":
                        return FormatMaskType.SSN;
                    case "Counter":
                        return FormatMaskType.Counter;
                    case "Name":
                        return FormatMaskType.Name;
                    case "Convert":
                        return FormatMaskType.Convert;
                    case "Text":
                        return FormatMaskType.Text;
                    case "SystemDate":
                        return FormatMaskType.SystemDate;
                    case "StateProvinceRegion":
                        return FormatMaskType.StateProvinceRegion;
                    default:
                        throw new InvalidCastException();
                }
            } 
        }

        
        public TransactionSnippetField ToDomainModel()
        {
            var field = new TransactionSnippetField();
            //SetDefaults();
            field.DefaultValue = this.DefaultValue;
            field.FormatMask = this.FormatMask;
            field.Frequency = this.Frequency;
            field.Id = this.Id;
            field.Length = this.Length;
            field.MakeUpperCase = this.MakeUpperCase;
            field.PadCharacterDec = this.PadCharacterDec;
            field.Prefix = this.Prefix;
            field.Separator = this.Separator;
            field.Suffix = this.Suffix;
            field.TagName = this.TagName;
            field.ToolTip = this.ToolTip;
            field.TransformFormat = this.TransformFormat;
            field.TrimInputToLength = this.TrimInputToLength;
            field.AcceptCarriageReturns = this.AcceptCarriageReturns;
            return field;
        }
    }
}