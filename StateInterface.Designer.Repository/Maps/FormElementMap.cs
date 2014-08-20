using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class FormElementMap : ClassMap<FormElement>
    {
        public FormElementMap()
        {
            Id(x => x.Id);
            Map(x => x.Sequence);
            Map(x => x.InRow);
            Map(x => x.InColumn);
            Map(x => x.ColumnSpan);
            Map(x => x.RowSpan);
            Map(x => x.Lines);
            Component(x => x.Style);
        }
    }
}
