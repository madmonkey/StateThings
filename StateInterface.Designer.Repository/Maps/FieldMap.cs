using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class FieldMap : ClassMap<Field>
    {
        public FieldMap()
        {
            Id(x => x.Id);
            Map(x => x.Created);
            Map(x => x.Updated);
            Map(x => x.TagName);
            Map(x => x.Description);
            Map(x => x.ToolTip);
            Map(x => x.Length);
            Map(x => x.MakeUpperCase);
            Map(x => x.Prefix);
            Map(x => x.Suffix);
            Map(x => x.DefaultValue);
            Map(x => x.Frequency);
            Map(x => x.Separator);
            Map(x => x.TransformFormat);
            Map(x => x.IsHiddenField);
            Map(x => x.FormatMask, "FormatMask_Id").CustomType<FormatMaskType>();
            Map(x => x.AcceptReturn);
            References(x => x.RecordsCenter);
        }
    }
}
