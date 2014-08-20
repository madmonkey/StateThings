using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class FormFieldMap : ClassMap<FormField>
    {
        public FormFieldMap()
        {
            Id(x => x.Id);
            Map(x => x.Frequency);
            Map(x => x.IsHiddenField);
            Map(x => x.Length);
            Map(x => x.DefaultValue);
            Map(x => x.Separator);
            Map(x => x.Sequence);
            References(x => x.OptionList);
            References(x => x.OptionListTier);
            References(x => x.Field).Not.LazyLoad();
            References(x => x.Requestform);
            References(x => x.ParentFormField);
        }
    }
}
