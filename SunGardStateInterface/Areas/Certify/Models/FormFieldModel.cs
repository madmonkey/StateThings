using StateInterface.Designer.Model;
using System;

namespace StateInterface.Areas.Certify.Models
{
    public class FormFieldModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Frequency { get; set; }
        public string Separator { get; set; }
        public string DefaultValue { get; set; }
        public bool IsHiddenField { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string OptionListName { get; set; }
        public string Tooltip { get; set; }
        public string Description { get; set; }


        public FormFieldModel(FormField formField)
        {
            Id = formField.Id;
            Name = formField.Field.TagName;
            Length = formField.Length;
            Prefix = formField.Field.Prefix;
            Suffix = formField.Field.Suffix;
            Frequency = formField.Frequency;
            Separator = formField.Separator;
            DefaultValue = formField.DefaultValue;
            IsHiddenField = formField.IsHiddenField;
            Type = formField.Field.FormatMask.ToString();
            Format = formField.Field.TransformFormat;
            Tooltip = formField.Field.ToolTip;
            Description = formField.Field.Description;

            if (formField.OptionList != null)
            {
                OptionListName = formField.OptionList.ListName;
            }
            else
            {
                OptionListName = String.Empty;
            }
        }
    }
}