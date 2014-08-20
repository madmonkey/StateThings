// ReSharper disable once CheckNamespace
namespace StateInterface.Areas.Design.Models
{
    using StateInterface.Designer.Model;
        using System;
        using System.Collections.Generic;
        using System.Globalization;
        using System.Linq;

        public class ControlModel
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public string Id { get; set; }
            public string Caption { get; set; }
            public string Description { get; set; }
            public int Length { get; set; }
            public int Row { get; set; }
            public int RowSpan { get; set; }
            public int Column { get; set; }
            public int ColSpan { get; set; }
            public string Default { get; set; }
            public string Value { get; set; }
            public string Style { get; set; }
            public string OptionListTier { get; set; }
            public string ParentId { get; set; }
            public List<OptionLinkage> OptionList { get; set; }
            

        }

        public class OptionLinkage
        {
            public OptionLinkage(string code, string description, string parentValue)
            {
                this.Code = code;
                this.Description = description;
                this.ParentLink = parentValue;
            }
            public string Code { get; set; }
            public string Description { get; set; }
            public string ParentLink { get; set; }

            public override string ToString()
            {
                return string.Format("Code [{0}]: Desc[{1}]: Parent[{2}]", Code, Description, ParentLink);
            }
        }
        public class ControlsModel
        {
            private readonly List<ControlModel> controls = new List<ControlModel>();

            public IEnumerable<ControlModel> Controls
            {
                get { return controls.OrderBy(r => r.Row).ThenBy(c => c.Column).ToList(); }
            }

            public ControlsModel(RequestForm requestForm)
            {
                this.controls = new List<ControlModel>();
                foreach (var field in requestForm.FieldElements)
                {
                    var control = new ControlModel
                    {
                        Type = field.FormField.OptionList==null? field.FormField.Field.FormatMask.ToString().ToLower():"select",
                        Id = field.FormField.Id.ToString(CultureInfo.InvariantCulture),
                        Caption = field.FormField.Field.ToolTip ?? string.Empty,
                        ColSpan = field.ColumnSpan,
                        Column = field.InColumn + 1,
                        Default = field.FormField.DefaultValue ?? string.Empty,
                        Value = field.FormField.DefaultValue ?? string.Empty,
                        Description = field.FormField.Field.Description ?? string.Empty,
                        Length = field.FormField.Length,
                        Name = field.FormField.Field.TagName ?? string.Empty,
                        Row = field.InRow + 1,
                        RowSpan = field.RowSpan,
                        Style = new StyleModel(field.Style.TextSize,
                            field.Style.IsBold,
                            field.Style.IsItalic,
                            field.Style.HorzAlignment,
                            field.Style.VertAlignment).ToString(),
                        
                    };
                    if (field.FormField.OptionList != null)
                    {
                        control.OptionList = TransformOptionList(field);
                        control.OptionListTier = field.FormField.OptionListTier.Name;
                        control.ParentId = field.FormField.ParentFormField != null
                            ? field.FormField.ParentFormField.Id.ToString(CultureInfo.InvariantCulture)
                            : string.Empty;

                    }
                    controls.Add(control);
                }

                foreach (var label in requestForm.LabelElements)
                {
                    this.controls.Add(new ControlModel
                    {
                        Type = "label",
                        Caption = label.Text ?? string.Empty,
                        ColSpan = label.ColumnSpan,
                        Column = label.InColumn + 1,
                        Row = label.InRow + 1,
                        RowSpan = label.RowSpan,
                        Style = new StyleModel(label.Style.TextSize,
                            label.Style.IsBold,
                            label.Style.IsItalic,
                            label.Style.HorzAlignment,
                            label.Style.VertAlignment).ToString()
                    });
                }
            }

            private static List<OptionLinkage> TransformOptionList(FieldElement field)
            {
                Console.WriteLine(field.FormField.ToString());
                var list = new List<OptionLinkage>();
                if (field.FormField.ParentFormField == null)
                {
                    list = (from oli in field.FormField.OptionList.OptionListItems
                            orderby oli.Sequence
                            select new OptionLinkage(oli.Code,
                                string.IsNullOrEmpty(oli.Description)
                                    ? string.Empty
                                    : oli.Description,
                                string.Empty)).ToList();
                }
                else
                {
                    var parent = field.FormField.ParentFormField;
                    //need to set a blank "Select One" for every parent value
                    foreach (var item in parent.OptionListTier.OptionList.OptionListItems.OrderBy(x=> x.Sequence))
                    {
                        list.Insert(0, new OptionLinkage("", "Select One", item.Code));
                        list.AddRange(item.OptionListItems.OrderBy(y => y.Sequence).Select(child => new OptionLinkage(child.Code, child.Description, item.Code)));
                    }
                }
                list.Insert(0, new OptionLinkage("", "Select One", ""));
                return list;
            }
        }
    
}