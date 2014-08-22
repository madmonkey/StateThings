using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;
using StateInterface.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class FieldDetailsModel
    {
        public int Id { get; set; }
        public string RecordsCenterName { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
        public string ToolTip { get; set; }
        public string LastUpdated { get; set; }
        public bool AcceptReturn { get; set; }
        public string DefaultValue { get; set; }
        public string Type { get; set; }
        public int Frequency { get; set; }
        public bool IsHiddenField { get; set; }
        public int Length { get; set; }
        public bool MakeUppercase { get; set; }
        public string Prefix { get; set; }
        public string Separator { get; set; }
        public string Suffix { get; set; }
        public string TransformFormat { get; set; }
        public List<UsesField> FormsUsing { get; set; }

        public string InitialData { get; set; }
        public string DesignHomeUrl { get; set; }
        public string FieldsHomeUrl { get; set; }

        public FieldDetailsModel(Field field, IEnumerable<RequestFormProjection> formsUsing, string formDetailsUrl)
        {
            Id = field.Id;
            TagName = field.TagName;
            Description = field.Description;
            IsHiddenField = field.IsHiddenField;
       
            RecordsCenterName = field.RecordsCenter.Name;

            LastUpdated = field.Updated.HasValue
                ? field.Updated.Value.ToString(Resources.DateTimeFormat)
                : field.Created.ToString(Resources.DateTimeFormat);

            ToolTip = field.ToolTip;
            Type = field.FormatMask.ToString();
            Length = field.Length;
            Frequency = field.Frequency;
            Separator = field.Separator;
            Prefix = field.Prefix;
            Suffix = field.Suffix;
            DefaultValue = field.DefaultValue;
            TransformFormat = field.TransformFormat;
            AcceptReturn = field.AcceptReturn;
            MakeUppercase = field.MakeUpperCase;

            FormsUsing = new List<UsesField>();
            foreach (var uses in formsUsing)
            {
                FormsUsing.Add(new UsesField(uses, string.Format("{0}/{1}", formDetailsUrl, RecordsCenterName)));
            }

            //todo: Add makeuppercase?
        }
    }
}